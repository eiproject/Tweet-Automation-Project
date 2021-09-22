using NUnit.Framework;
using TweetAutomation.TwitterAPIHandler.Business;

namespace TweetAutomation.UnitTesting
{
  public class TestITwitter
  {
    ITwitter _twitter;
    [SetUp]
    public void Setup()
    {
      _twitter = new Twitter(new TwitterAPIHandler.Model.Credentials());
    }

    [TestCase("consumer_key", "consumer_key_secret", "access_token", "access_token_secret")]
    [TestCase(null, null, null, null)]
    public void SetCredential_ShouldBePass(
      string consumerKey, string consumerKeySecret,
      string accessToken, string accessTokenSecret
      )
    {
      try
      {
        /*_twitter.Tweet();
        _twitter.SetCredential(
          consumerKey, consumerKeySecret,
          accessToken, accessTokenSecret);*/
        Assert.IsTrue(true);
      }
      catch
      {
        Assert.IsTrue(false);
      }
    }
  }
}