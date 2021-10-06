using System;
using System.Net.Http;
using System.Text.Json;
using TweetAutomation.TwitterAPIHandler.Business;
using TweetAutomation.TwitterAPIHandler.Model;

namespace TweetAutomation.TwitterAPIHandler
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Twitter API Handler Project!");
      /*Credentials crededentials = new Credentials()
      {
        ConsumerKey = MyCredential.CK,
        ConsumerSecret = MyCredential.CS,
        AccessTokenKey = MyCredential.ATK,
        AccessTokenSecret = MyCredential.ATS
      };
      string file = "C:/Lab-Formulatrix/Open-Source/TweetAutomation/asset/img/TweetAutomation-Logo.png";
      UploadMedia api = new UploadMedia(crededentials);

      HttpResponseMessage response = api.Init(file);
      string JsonString = response.Content.ReadAsStringAsync().Result;
      MediaInitResponse media = JsonSerializer.Deserialize<MediaInitResponse>(JsonString);

      api.Append(media.media_id_string, file);
      HttpResponseMessage finalize = api.Finalize(media.media_id_string);
      HttpResponseMessage status = api.Status(media.media_id_string);

      Twitter apitwt = new Twitter(crededentials);
      apitwt.Tweet("Test", "1445633225448034304");
      Console.WriteLine("Done!" + api);*/
      Console.ReadKey();
    }
  }
}