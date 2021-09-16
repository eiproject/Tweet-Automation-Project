using System;
using System.Collections.Generic;
using System.Text;

namespace UserInterface.Model
{
  class TweetRecords
  {
    public List<TweetRecord> Records { get { return _reecords; } }
    private List<TweetRecord> _reecords;
    internal TweetRecords()
    {
      _reecords = new List<TweetRecord>();
    }
    internal void Add(TweetRecord record)
    {
      _reecords.Add(record);
    }
  }
}
