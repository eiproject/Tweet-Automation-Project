using System;
using TweetAutomation.UserInterface.Database;

namespace TweetAutomation.UserInterface.Model
{
  public class TweetsRepository : ITweetsRepository
  {
    private Tweets _dbInstance;
    public TweetsRepository(Tweets dbInstance)
    {
      _dbInstance = dbInstance;
    }

    public void Append(Tweet record)
    {
      _dbInstance.Records.Add(record);
    }

    public void Delete(int id)
    {
      foreach (Tweet record in _dbInstance.Records)
      {
        if (record.ID == id)
        {
          _dbInstance.Records.Remove(record);
          return;
        }
      }
    }
  }
}
