using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TwitterAPIHandler.Model;

namespace TwitterAPIHandler.Business
{
  public class Twitter : ITwitter
  {
    private readonly DateTime epochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private HMACSHA1 _sigHasher;
    private const string _twitterApiBaseUrl = "https://api.twitter.com/1.1/";
    private string _consumerKey;
    private string _consumerSecret;
    private string _accessTokenKey;
    private string _accessTokenSecret;
    
    public Twitter(Credentials credentials) {
      _consumerKey = credentials.ConsumerKey;
      _consumerSecret = credentials.ConsumerSecret;
      _accessTokenKey = credentials.AccessTokenKey;
      _accessTokenSecret = credentials.AccessTokenSecret;
    }


    public Task<HttpStatusCode> Tweet(string text)
    {
      var data = new Dictionary<string, string> {
            { "status", text },
            { "trim_user", "1" }
        };

      return SendRequest("statuses/update.json", data);
    }
    private void SetCredential()
    {
      _sigHasher = new HMACSHA1(new ASCIIEncoding().GetBytes(
        string.Format("{0}&{1}", _consumerSecret, _accessTokenSecret)));
    }

    private Task<HttpStatusCode> SendRequest(
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

    private async Task<HttpStatusCode> SendRequest(
      string fullUrl, string oAuthHeader, FormUrlEncodedContent formData)
    {
      using (var http = new HttpClient())
      {
        http.DefaultRequestHeaders.Add("Authorization", oAuthHeader);

        HttpResponseMessage httpResp = await http.PostAsync(fullUrl, formData);
        var respBody = await httpResp.Content.ReadAsStringAsync();

        return httpResp.StatusCode;
      }
    }
  }
}
