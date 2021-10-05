using System;
using System.Collections.Generic;
using System.Text;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface.Database
{
  [Serializable]
  public class Tweets
  {
    private static Tweets _instance = null;
    private static object _locker = new object();
    private List<Tweet> _records;
    private Tweets()
    {
      _records = new List<Tweet>();
    }
    public List<Tweet> Records { get { return _records; } }
    public static Tweets GetInstance()
    {
      lock (_locker)
      {
        if (_instance == null)
        {
          _instance = new Tweets();
        }
        return _instance;
      }
    }

    public void Migration(Tweets migrationData)
    {
      _instance = migrationData;
    }
  }
}
