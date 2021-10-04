using System;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface.Factory
{
  public interface ITweetRecordFactory
  {
    Tweet Create(string tweet, DateTime date, DateTime time, bool isImmediately);
  }
}
