using System;
using System.Collections.Generic;
using System.Text;

namespace UserInterface.Model
{
  [Serializable]
  public class TweetRecords
  {
    public List<TweetRecord> Records { get { return _records; } }
    private List<TweetRecord> _records;
    public TweetRecords()
    {
      _records = new List<TweetRecord>();
    }
    internal void Add(TweetRecord record)
    {
      _records.Add(record);
    }

    internal void Update(TweetRecords lastTweetList)
    {
      if (lastTweetList != null) _records = lastTweetList.Records;
    }

    internal void Delete(int id)
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
