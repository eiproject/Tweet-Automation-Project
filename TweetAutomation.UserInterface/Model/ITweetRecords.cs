using System.Collections.Generic;

namespace TweetAutomation.UserInterface.Model
{
  public interface ITweetRecords
  {
    List<TweetRecord> Records { get; }
    void Add(TweetRecord record);
    void Update(TweetRecords lastTweetList);
    void Delete(int id);
  }
}
