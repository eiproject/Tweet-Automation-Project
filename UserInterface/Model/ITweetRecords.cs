using System.Collections.Generic;

namespace UserInterface.Model
{
  public interface ITweetRecords
  {
    List<TweetRecord> Records { get; }
    void Add(TweetRecord record);
    void Update(TweetRecords lastTweetList);
    void Delete(int id);
  }
}
