using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TweetAutomation.LoggingSystem.Business;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface.BLL
{
  public class StatusChecker : IStatusChecker
  {
    private LogRepository _logger = LogRepository.LogInstance();
    public StatusChecker() { }

    public void CheckStatus(Tweet record)
    {
      if (record.Status != "Success")
      {
        SetDefault(record);
        CheckTimeError(record);
        CheckNull(record);
      }
      _logger.Update("DEBUG", $"Status ID: {record.ID} is Status: {record.Status}");
    }

    public void CheckStatusOfSendImmediately(Tweet record)
    {
      if (record.Status != "Success")
      {
        SetDefaultSendImmediately(record);
        CheckNull(record);
      }
      _logger.Update("DEBUG", $"Status ID: {record.ID} is Status: {record.Status}");
    }

    public void ChangeStatusByResponse(Tweet record, HttpStatusCode response)
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

    private void SetDefault(Tweet record)
    {
      record.Status = "On Queue";
    }

    private void SetDefaultSendImmediately(Tweet record)
    {
      record.Status = "Starting";
    }

    private void CheckNull(Tweet record)
    {
      string tweet = record.FullText;
      if (tweet == null || tweet == "" || tweet == " ") record.Status = "Tweet Null";
    }

    private void CheckTimeError(Tweet record)
    {
      int dateDifference = (record.DateObject - DateTime.Now).Days;
      double timeDifference = (record.TimeObject - DateTime.Now).TotalMinutes;
      if (dateDifference < 0 || timeDifference < 0) record.Status = "Time Error";
    }
  }
}
