using System;

namespace TweetAutomation.UserInterface.Model
{
  [Serializable]
  public class TweetRecord
  {
    public int ID { get; set; }
    public string Tweet { get; set; }
    public DateTime DateObject { get; set; }
    public string DateString { get; set; }
    public DateTime TimeObject { get; set; }
    public string TimeString { get; set; }
    public DateTime DateTimeCombined { get; set; }
    public string Status { get; set; }

    public TweetRecord(
      int id, string tweet, DateTime date, DateTime time)
    {
      ID = id;
      Tweet = tweet;
      DateObject = date;
      DateString = date.ToString("MM/dd/yyyy");
      TimeObject = time;
      TimeString = time.ToString("hh:mm tt");
      DateTimeCombined = date.Date + time.TimeOfDay;
      Status = null;
    }
  }
}
