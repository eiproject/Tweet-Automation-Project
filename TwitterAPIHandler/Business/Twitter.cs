using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace TwitterAPIHandler.Business {
  public class Twitter {
    private HttpClient _client;
    public Twitter() {
      _client = new HttpClient();
    }

    public void PostTweet() {
      Task.Run(() => MainAsync());
    }

    private async Task MainAsync(string ock, string ocs, string ot, string ots) {
      using (var client = new HttpClient()) {
        client.BaseAddress = new Uri("https://api.twitter.com");
        client.DefaultRequestHeaders.Add("oauth_consumer_key", ock);
        client.DefaultRequestHeaders.Add("oauth_consumer_secret", ocs);
        client.DefaultRequestHeaders.Add("oauth_token", ot);
        client.DefaultRequestHeaders.Add("oauth_token_secret", ots);
        client.DefaultRequestHeaders.Accept.Add
          (new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        var content = new FormUrlEncodedContent(new[]
        {
                new KeyValuePair<string, string>("status", "Twitter post using C#. Ignore this.")
            });
        var result = await client.PostAsync("/1.1/statuses/update.json", content);
        string resultContent = await result.Content.ReadAsStringAsync();
        Console.WriteLine(resultContent);
      }
    }
  }
}
