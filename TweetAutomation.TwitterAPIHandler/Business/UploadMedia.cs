using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MimeMapping;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TweetAutomation.LoggingSystem.BusinessLogic;
using TweetAutomation.TwitterAPIHandler.Model;

namespace TweetAutomation.TwitterAPIHandler.Business
{
  public class UploadMedia
  {
    private object _lockerSendRequest = new object();
    private readonly DateTime epochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private LogRepository _logger = LogRepository.LogInstance();
    private HMACSHA1 _sigHasher;
    private const string _twitterApiBaseUrl = "https://upload.twitter.com/1.1/";
    private string _consumerKey;
    private string _consumerSecret;
    private string _accessTokenKey;
    private string _accessTokenSecret;

    public UploadMedia(Credentials credentials)
    {
      _logger.Update("DEBUG", "Creating Twitter API instance.");
      _consumerKey = credentials.ConsumerKey;
      _consumerSecret = credentials.ConsumerSecret;
      _accessTokenKey = credentials.AccessTokenKey;
      _accessTokenSecret = credentials.AccessTokenSecret;
      CreateSigHasher();
    }

    public HttpResponseMessage Init(string mediaPath)
    {
      byte[] media = File.ReadAllBytes(mediaPath);
      string mimeType = MimeUtility.GetMimeMapping(mediaPath);
      Console.WriteLine("mimeType " + mimeType);
      var data = new Dictionary<string, string> {
            { "command", "INIT" },
            { "total_bytes", $"{media.Length}" },
            { "media_type", $"{mimeType}" }
        };

      return ConfigureOauth("media/upload.json", data);
    }

    public void Append(string mediaID, string mediaPath)
    {

      const int chunkSize = 40 * 1024; // read the file by chunks
      using (var file = File.OpenRead(mediaPath))
      {
        int bytesRead = 0;
        int chunkID = 0;
        byte[] buffer = new byte[chunkSize];
        
        while ((bytesRead = file.Read(buffer, 0, buffer.Length)) > 0)
        {
          if (bytesRead < chunkSize)
          {
            var lastBuffer = new byte[bytesRead];
            Buffer.BlockCopy(buffer, 0, lastBuffer, 0, bytesRead);
            buffer = new byte[bytesRead];
            buffer = lastBuffer;
          }
          // Console.WriteLine("buffer.Length : " + buffer.Length);
          HttpResponseMessage response = SendAppend(
            buffer,
            mediaID,
            chunkID
            );
          chunkID++;
        }
      }
    }

    public HttpResponseMessage Finalize(string mediaID)
    {
      var data = new Dictionary<string, string> {
            { "command", "FINALIZE" },
            { "media_id", $"{mediaID}" }
        };
      return ConfigureOauth("media/upload.json", data);
    }

    public HttpResponseMessage Status(string mediaID)
    {
      var data = new Dictionary<string, string> {
            { "command", "STATUS" },
            { "media_id", $"{mediaID}" }
        };
      return ConfigureOauth("media/upload.json", data);
    }

    private HttpResponseMessage SendAppend(byte[] chunk, string mediaID, int chunkID)
    {
      lock (_lockerSendRequest)
      {
        string videoBase64 = Convert.ToBase64String(chunk);
        var data = new Dictionary<string, string>
        {
            { "command", "APPEND" },
            { "media_id", $"{mediaID}" },
            { "media_data", $"{videoBase64}" },
            { "segment_index", $"{chunkID}" }
        };
        return ConfigureOauth("media/upload.json", data);
      }
    }

    private void CreateSigHasher()
    {
      _sigHasher = new HMACSHA1(new ASCIIEncoding().GetBytes(
        string.Format("{0}&{1}", _consumerSecret, _accessTokenSecret)));
    }

    private HttpResponseMessage ConfigureOauth(
      string url, Dictionary<string, string> data)
    {
      var fullUrl = _twitterApiBaseUrl + url;

      // Timestamps are in seconds since 1/1/1970.
      var timestamp = (int)((DateTime.UtcNow - epochUtc).TotalSeconds);

      // Add all the OAuth headers we'll need to use when constructing the hash.
      data.Add("oauth_consumer_key", _consumerKey);
      data.Add("oauth_signature_method", "HMAC-SHA1");
      data.Add("oauth_timestamp", timestamp.ToString());
      data.Add("oauth_nonce", "a"); // Required, but Twitter doesn't appear to use it, so "a" will do.
      data.Add("oauth_token", _accessTokenKey);
      data.Add("oauth_version", "1.0");

      // Generate the OAuth signature and add it to our payload.
      data.Add("oauth_signature", GenerateSignature(fullUrl, data));

      // Build the OAuth HTTP Header from the data.
      string oAuthHeader = GenerateOAuthHeader(data);

      // Build the form data (exclude OAuth stuff that's already in the header).
      var formData = new FormUrlEncodedContent(
        data.Where(kvp => !kvp.Key.StartsWith("oauth_")));

      return PostRequest(fullUrl, oAuthHeader, formData);
    }

    private string GenerateSignature(string url, Dictionary<string, string> data)
    {
      var sigString = string.Join(
          "&",
          data
              .Union(data)
              .Select(kvp => string.Format(
                "{0}={1}", Uri.EscapeDataString(kvp.Key),
                Uri.EscapeDataString(kvp.Value)))
              .OrderBy(s => s)
      );

      var fullSigData = string.Format(
          "{0}&{1}&{2}",
          "POST",
          Uri.EscapeDataString(url),
          Uri.EscapeDataString(sigString.ToString())
      );

      return Convert.ToBase64String(_sigHasher.ComputeHash(
        new ASCIIEncoding().GetBytes(fullSigData.ToString())));
    }

    private string GenerateOAuthHeader(Dictionary<string, string> data)
    {
      return "OAuth " + string.Join(
          ", ",
          data
              .Where(kvp => kvp.Key.StartsWith("oauth_"))
              .Select(kvp => string.Format("{0}=\"{1}\"",
              Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
              .OrderBy(s => s)
      );
    }

    private HttpResponseMessage PostRequest(
      string fullUrl, string oAuthHeader, FormUrlEncodedContent formData)
    {
      using (var http = new HttpClient())
      {
        HttpResponseMessage httpResp = new HttpResponseMessage();
        http.DefaultRequestHeaders.Add("Authorization", oAuthHeader);
        try
        {
          var request = http.PostAsync(fullUrl, formData);
          request.Wait();

          httpResp = request.Result;
          var respBody = httpResp.Content.ReadAsStringAsync();

          Console.WriteLine(respBody.Result.ToString());
        }
        catch (SocketException error)
        {
          _logger.Update("ERROR", error.GetType().Name + " " + error.Message);
          httpResp.StatusCode = HttpStatusCode.RequestTimeout;
        }
        catch (HttpRequestException error)
        {
          _logger.Update("ERROR", error.GetType().Name + " " + error.Message);
          httpResp.StatusCode = HttpStatusCode.RequestTimeout;
        }
        return httpResp;
      }
    }
  }
}
