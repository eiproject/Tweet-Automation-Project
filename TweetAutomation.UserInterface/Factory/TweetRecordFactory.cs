using System;
using TweetAutomation.UserInterface.Database;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface.Factory
{
  public class TweetRecordFactory : ITweetRecordFactory
  {
    Tweets _db;
    public TweetRecordFactory(Tweets db)
    {
      _db = db;
    }

    public Tweet Create(
      string tweet, DateTime date, DateTime time, bool isImmediately)
    {
      if (isImmediately)
      {
        date = DateTime.Now;
        time = DateTime.Now;
      }
      time = date + time.TimeOfDay;
      return new Tweet(GetLastID(), tweet, date, time, isImmediately);
    }

    private int GetLastID()
    {
      int id;
      if (_db.Records.Count == 0)
      {
        id = 0;
      }
      else
      {
        id = _db.Records[_db.Records.Count - 1].ID + 1;
      }
      return id;
    }
  }
}
