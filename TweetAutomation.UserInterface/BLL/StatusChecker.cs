using System;
using System.Net;
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
      if (record.IsStatusPermanent != true)
      {
        SetOnQueue(record);
        CheckTimeError(record);
        CheckNull(record);
      }
      _logger.Update("DEBUG", $"Status ID: {record.ID} is Status: {record.Status}");
    }

    public void CheckStatusOfSendImmediately(Tweet record)
    {
      if (record.IsStatusPermanent != true)
      {
        SetStarting(record);
        CheckNull(record);
      }
      _logger.Update("DEBUG", $"Status ID: {record.ID} is Status: {record.Status}");
    }

    public void ChangeStatusByResponse(Tweet record, HttpStatusCode response)
    {
      if (response == HttpStatusCode.OK)
      {
        record.Status = TweetStatus.Success;
      }
      else if (response == HttpStatusCode.Unauthorized)
      {
        record.Status = TweetStatus.Credential_Error;
      }
      else if (response == HttpStatusCode.RequestTimeout)
      {
        record.Status = TweetStatus.Request_Timeout;
      }
      else if (response == HttpStatusCode.Forbidden)
      {
        record.Status = TweetStatus.Forbidden;
      }
      else if (response == HttpStatusCode.BadRequest)
      {
        record.Status = TweetStatus.Bad_Request;
      }
      else
      {
        record.Status = TweetStatus.Unknown;
      }
      record.IsStatusPermanent = true;
    }

    private void SetOnQueue(Tweet record)
    {
      record.Status = TweetStatus.On_Queue;
    }

    private void SetStarting(Tweet record)
    {
      record.Status = TweetStatus.Starting;
    }

    private void CheckNull(Tweet record)
    {
      string tweet = record.FullText;
      if (tweet == null || tweet == "" || tweet == " ") record.Status = TweetStatus.Tweet_Null;
      record.IsStatusPermanent = true;
    }

    private void CheckTimeError(Tweet record)
    {
      TimeSpan timeToGo = record.DateTimeCombined - DateTime.Now;
      if (timeToGo.TotalMilliseconds < 0 ) record.Status = TweetStatus.Time_Error;
      record.IsStatusPermanent = true;
    }
  }
}
