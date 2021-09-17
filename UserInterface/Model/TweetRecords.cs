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

    internal void Update(List<TweetRecord> lastTweetList)
    {
      _records = lastTweetList;

    }
  }
}
