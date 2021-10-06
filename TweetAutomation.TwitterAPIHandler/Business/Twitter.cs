using MimeMapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TweetAutomation.LoggingSystem.BusinessLogic;
using TweetAutomation.TwitterAPIHandler.Model;

namespace TweetAutomation.TwitterAPIHandler.Business
{
  public class Twitter : ITwitter
  {
    private object _lockerSendRequest = new object();
    private readonly DateTime epochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private LogRepository _logger = LogRepository.LogInstance();
    private HMACSHA1 _sigHasher;
    private Credentials _credentials;
    private const string _twitterApiBaseUrl = "https://api.twitter.com/1.1/";
    private const string _twitterApiUploadUrl = "https://upload.twitter.com/1.1/";
    private string _consumerKey;
    private string _consumerSecret;
    private string _accessTokenKey;
    private string _accessTokenSecret;

    public Twitter(Credentials credentials)
    {
      _logger.Update("DEBUG", "Creating Twitter API instance.");
      _consumerKey = credentials.ConsumerKey;
      _consumerSecret = credentials.ConsumerSecret;
      _accessTokenKey = credentials.AccessTokenKey;
      _accessTokenSecret = credentials.AccessTokenSecret;
      _credentials = credentials;

      CreateSigHasher();
    }

    public HttpStatusCode Tweet(string text)
    {
      lock (_lockerSendRequest)
      {
        int maxLength = 20;
        if (text.Length < maxLength) maxLength = text.Length;
        _logger.Update("DEBUG", $"Sending Tweet. {text.Substring(0, maxLength)}");
        var data = new Dictionary<string, string> {
          { "status", text },
          { "trim_user", "1" }
        };

        return SendRequest(_twitterApiBaseUrl + "statuses/update.json", data).StatusCode;
      }
    }

    public HttpStatusCode Tweet(string text, string filePath)
    {
      lock (_lockerSendRequest)
      {
        int maxLength = 20;
        if (text.Length < maxLength) maxLength = text.Length;
        _logger.Update("DEBUG", $"Twitter Tweet. {text.Substring(0, maxLength)}");

        HttpResponseMessage response = Init(filePath);
        string JsonString = response.Content.ReadAsStringAsync().Result;
        _logger.Update("DEBUG", $"Twitter Tweet. {JsonString}");
        MediaInitResponse media = JsonSerializer.Deserialize<MediaInitResponse>(JsonString);
        List<HttpResponseMessage> append = Append(media.media_id_string, filePath);
        HttpResponseMessage finalize = Finalize(media.media_id_string);
        HttpResponseMessage status = Status(media.media_id_string);

        _logger.Update("DEBUG", $"Twitter Tweet. {media.media_id_string}");

        var data = new Dictionary<string, string> {
          { "status", text },
          { "trim_user", "1" },
          { "media_ids", media.media_id_string }
        };

        return SendRequest(_twitterApiBaseUrl + "statuses/update.json", data).StatusCode;
      }
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

      return SendRequest(_twitterApiUploadUrl + "media/upload.json", data);
    }

    public List<HttpResponseMessage> Append(string mediaID, string mediaPath)
    {
      List<HttpResponseMessage> responses = new List<HttpResponseMessage>();
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
          responses.Add(response);
          chunkID++;
        }
      }
      return responses;
    }

    public HttpResponseMessage Finalize(string mediaID)
    {
      var data = new Dictionary<string, string> {
            { "command", "FINALIZE" },
            { "media_id", $"{mediaID}" }
        };
      return SendRequest(_twitterApiUploadUrl + "media/upload.json", data);
    }

    public HttpResponseMessage Status(string mediaID)
    {
      var data = new Dictionary<string, string> {
            { "command", "STATUS" },
            { "media_id", $"{mediaID}" }
        };
      return SendRequest(_twitterApiUploadUrl + "media/upload.json", data);
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
        return SendRequest(_twitterApiUploadUrl + "media/upload.json", data);
      }
    }

    private void CreateSigHasher()
    {
      _sigHasher = new HMACSHA1(new ASCIIEncoding().GetBytes(
        string.Format("{0}&{1}", _consumerSecret, _accessTokenSecret)));
    }

    private HttpResponseMessage SendRequest(
      string fullUrl, Dictionary<string, string> data)
    {
      // var fullUrl = _twitterApiBaseUrl + url;

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

      return SendRequest(fullUrl, oAuthHeader, formData);
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

    private HttpResponseMessage SendRequest(
      string fullUrl, string oAuthHeader, FormUrlEncodedContent formData)
    {
      using (var http = new HttpClient())
      {
        HttpResponseMessage response = new HttpResponseMessage();
        http.DefaultRequestHeaders.Add("Authorization", oAuthHeader);
        try
        {
          var httpResp = http.PostAsync(fullUrl, formData);
          response = httpResp.Result;

          var respBody = response.Content.ReadAsStringAsync();
          _logger.Update("DEBUG", $"Twitter SendRequest. {respBody.Result.ToString()}");
          Console.WriteLine(respBody.Result.ToString());
        }
        catch (SocketException error)
        {
          _logger.Update("ERROR", error.GetType().Name + " " + error.Message);
          response.StatusCode = HttpStatusCode.RequestTimeout;
        }
        catch (HttpRequestException error)
        {
          _logger.Update("ERROR", error.GetType().Name + " " + error.Message);
          response.StatusCode = HttpStatusCode.RequestTimeout;
        }
        return response;
      }
    }
  }
}
