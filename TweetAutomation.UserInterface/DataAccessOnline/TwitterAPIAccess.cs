using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TweetAutomation.LoggingSystem.Business;
using TweetAutomation.TwitterAPIHandler.Business;
using TweetAutomation.UserInterface.BLL;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface.DataAccessOnline
{
  /// <summary>
  /// To-DO
  /// </summary>
  class TwitterAPIAccess
  {
    private IStatusChecker _statusChecker;
    private ITwitter _api;
    private CredentialsAdapter _adapter;
    private LogRepository _logger = LogRepository.LogInstance();

    internal TwitterAPIAccess(
      IStatusChecker statusChecker, CredentialsAdapter adapter)
    {
      _adapter = adapter;
      _statusChecker = statusChecker;
    }

    public async Task<Tweet> SendTweet(Credentials credentials, Tweet record)
    {
      _logger.Update("DEBUG", "Start sending Tweet.");

      InitializeAPI(credentials);
      if (record.IsImmediately == true)
      {
        _statusChecker.CheckStatusOfSendImmediately(record);
        if (record.Status == TweetStatus.Starting)
        {
          HttpStatusCode response = await SendTweetAsync(_api, record);
          _statusChecker.ChangeStatusByResponse(record, response);
          _logger.Update("DEBUG", $"Done sending Tweet immediately. {record.Status}");
        }
      }
      else
      {
        _statusChecker.CheckStatus(record);
        TimeSpan timeToGo = record.DateTimeCombined - DateTime.Now;
        if (timeToGo > TimeSpan.Zero)
        {
          // Not sure. Maybe causing thread error, test this 
          /*Timer timer = new Timer(async x =>
          {*/
          Thread.Sleep((int)timeToGo.TotalMilliseconds);
          HttpStatusCode response = await SendTweetAsync(_api, record);
          _statusChecker.ChangeStatusByResponse(record, response);
          // }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }
      }
      return record;
    }

    private void InitializeAPI(Credentials credentials)
    {
      _api = new Twitter(_adapter.Adaptee(credentials));
    }

    private async Task<HttpStatusCode> SendTweetAsync(ITwitter api, Tweet tweet)
    {
      _logger.Update("DEBUG", $"Calling Twitter API. ID: {tweet.ID}");
      HttpStatusCode response = await api.Tweet(tweet.FullText);
      _logger.Update("DEBUG", $"Response sending Tweet async. {response}");

      return response;
    }
  }
}
