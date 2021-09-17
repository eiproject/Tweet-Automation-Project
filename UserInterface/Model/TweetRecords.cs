using System;
using System.Collections.Generic;

namespace UserInterface.Model
{
  [Serializable]
  public class TweetRecords : ITweetRecords
  {
    public List<TweetRecord> Records { get { return _records; } }
    private List<TweetRecord> _records;
    public TweetRecords()
    {
      _records = new List<TweetRecord>();
    }
    public void Add(TweetRecord record)
    {
      _records.Add(record);
    }

    public void Update(TweetRecords lastTweetList)
    {
      if (lastTweetList != null) _records = lastTweetList.Records;
    }

    public void Delete(int id)
    {
      foreach (TweetRecord record in _records)
      {
        if (record.ID == id)
        {
          _records.Remove(record);
          return;
        }
      }
    }
  }
}
