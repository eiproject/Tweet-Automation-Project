using System;

namespace TweetAutomation.UserInterface.Model
{
  [Serializable]
  public class Credentials
  {
    public string ConsumerKey { get; set; }
    public string ConsumerSecret { get; set; }
    public string AccessTokenKey { get; set; }
    public string AccessTokenSecret { get; set; }
  }
}
