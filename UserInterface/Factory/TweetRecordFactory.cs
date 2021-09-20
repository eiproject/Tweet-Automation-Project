using System;
using System.Collections.Generic;
using System.Text;
using UserInterface.Business;
using UserInterface.Model;

namespace UserInterface.Factory
{
  public class TweetRecordFactory : ITweetRecordFactory
  {
    ITweetRecords _records;
    public TweetRecordFactory(ITweetRecords records)
    {
      _records = records;
    }

    public TweetRecord Create(
      string tweet, DateTime date, DateTime time)
    {

      return new TweetRecord(
        GetLastID(),
        tweet,
        date,
        time);
    }

    private int GetLastID()
    {
      int id;
      if (_records.Records.Count == 0)
      {
        id = 0;
      }
      else
      {
        id = _records.Records[_records.Records.Count - 1].ID + 1;
      }
      return id;
    }
  }
}
