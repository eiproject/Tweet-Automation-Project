using System;
using System.Collections.Generic;
using System.Text;
using TweetAutomation.TwitterAPIHandler.Model;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface.DataAccessOnline
{
  class CredentialsAdapter
  {
    public TwitterAPIHandler.Model.Credentials Adaptee(Model.Credentials source)
    {
      return new TwitterAPIHandler.Model.Credentials()
      {
        ConsumerKey = source.ConsumerKey,
        ConsumerSecret = source.ConsumerSecret,
        AccessTokenKey = source.AccessTokenKey,
        AccessTokenSecret = source.AccessTokenSecret
      };
    }
  }
}
