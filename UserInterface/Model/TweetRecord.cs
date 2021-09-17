using System;

namespace UserInterface.Model
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
      int id, string tweet, DateTime date, DateTime time, string status)
    {
      ID = id;
      Tweet = tweet;
      DateObject = date;
      DateString = date.ToString("dd/MM/yyyy");
      TimeObject = time;
      TimeString = time.ToString("hh:mm tt");
      DateTimeCombined = date.Date + time.TimeOfDay;
      Status = status;
    }
  }
}
