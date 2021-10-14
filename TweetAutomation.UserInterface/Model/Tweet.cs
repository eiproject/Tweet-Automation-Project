using System;

namespace TweetAutomation.UserInterface.Model
{
  [Serializable]
  public class Tweet
  {
    public int ID { get; set; }
    public string FullText { get; set; }
    public DateTime DateObject { get; set; }
    public string DateString { get; set; }
    public DateTime TimeObject { get; set; }
    public string TimeString { get; set; }
    public DateTime DateTimeCombined { get; set; }
    public TweetStatus Status { get; set; }
    public bool IsImmediately { get; set; }
    public bool IsStatusPermanent { get; set; }
    public string ImagePath { get; set; }

    public Tweet(
      int id, string tweet, DateTime date, DateTime time, bool isImmediately, string imagePath)
    {
      ID = id;
      FullText = tweet;
      DateObject = date;
      DateString = date.ToString("MM/dd/yyyy");
      TimeObject = time;
      TimeString = time.ToString("hh:mm tt");
      DateTimeCombined = date.Date + time.TimeOfDay;
      Status = TweetStatus.None;
      IsImmediately = isImmediately;
      ImagePath = imagePath;
    }
  }
}
