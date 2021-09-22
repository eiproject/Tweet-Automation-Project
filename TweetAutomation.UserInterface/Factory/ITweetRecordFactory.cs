using System;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface.Factory
{
  public interface ITweetRecordFactory
  {
    TweetRecord Create(string tweet, DateTime date, DateTime time);
  }
}
