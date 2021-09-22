using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UserInterface.Model;

namespace UserInterface.Business
{
  public class StatusChecker : IStatusChecker
  {
    public StatusChecker() { }

    public void CheckStatus(TweetRecord record)
    {
      if (record.Status != "Success")
      {
        SetDefault(record);
        CheckTimeError(record);
        CheckNull(record);
      }
    }

    public void CheckStatusOfSendImmediately(TweetRecord record)
    {
      if (record.Status != "Success")
      {
        SetDefaultSendImmediately(record);
        CheckNull(record);
      }
    }

    public void ChangeStatusByResponse(TweetRecord record, HttpStatusCode response)
    {
      if (response == HttpStatusCode.OK)
      {
        record.Status = "Success";
      }
      else if (response == HttpStatusCode.Unauthorized)
      {
        record.Status = "Credential Error";
      }
      else if (response == HttpStatusCode.RequestTimeout)
      {
        record.Status = "Request Timeout";
      }
      else if (response == HttpStatusCode.Forbidden)
      {
        record.Status = "Forbidden";
      }
      else if (response == HttpStatusCode.BadRequest)
      {
        record.Status = "Bad Request";
      }
      else if (response == HttpStatusCode.RequestTimeout)
      {
        record.Status = "Timeout";
      }
      else
      {
        record.Status = "Unknown Error";
      }
    }

    private void SetDefault(TweetRecord record)
    {
      record.Status = "On Queue";
    }

    private void SetDefaultSendImmediately(TweetRecord record)
    {
      record.Status = "Starting";
    }

    private void CheckNull(TweetRecord record)
    {
      string tweet = record.Tweet;
      if (tweet == null || tweet == "" || tweet == " ") record.Status = "Tweet Null";
    }

    private void CheckTimeError(TweetRecord record)
    {
      int dateDifference = (record.DateObject - DateTime.Now).Days;
      double timeDifference = (record.TimeObject - DateTime.Now).TotalMinutes;
      if (dateDifference < 0 || timeDifference < 0) record.Status = "Time Error";
    }
  }
}
