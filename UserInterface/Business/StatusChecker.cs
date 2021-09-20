using System;
using System.Collections.Generic;
using System.Text;
using UserInterface.Model;

namespace UserInterface.Business
{
  public class StatusChecker : IStatusChecker
  {
    private const string _defaultStatus = "On Queue";
    public StatusChecker() { }

    public void CheckStatus(TweetRecord record)
    {
      if(record.Status.ToLower() != "success")
      {
        SetDefault(record);
        CheckTimeError(record);
        CheckNull(record);
      }
    }

    public void ChangeToSuccess(TweetRecord record)
    {
      record.Status = "Success";
    }

    private void SetDefault(TweetRecord record)
    {
      record.Status = _defaultStatus;
    }

    private void CheckNull(TweetRecord record)
    {
      string tweet = record.Tweet;
      if (tweet == null || tweet == "" || tweet == " ") record.Status = "Tweet Null";
    }

    private void CheckTimeError(TweetRecord record)
    {
      int dateDifference = (record.DateObject - DateTime.Now).Days;
      double timeDifference = (record.TimeObject- DateTime.Now).TotalMinutes;
      if (dateDifference < 0 || timeDifference < 0) record.Status = "Time Error";
    }
  }
}
