using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TweetAutomation.LoggingSystem.BusinessLogic;
using TweetAutomation.TwitterAPIHandler.Business;
using TweetAutomation.UserInterface.BLL;
using TweetAutomation.UserInterface.BusinessLogic;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface.DataAccessOnline
{
  class TwitterAPIAccess
  {
    private IStatusChecker _statusChecker;
    private ITwitter _api;
    private CredentialsAdapter _adapter;
    private LogRepository _logger = LogRepository.LogInstance();
    private DataGridManager _dataGridManager;

    internal TwitterAPIAccess()
    {
      _adapter = new CredentialsAdapter();
      _statusChecker = new StatusChecker(); ;
      _dataGridManager = new DataGridManager();
    }

    public Tweet SendTweet(
      Credentials credentials, Tweet record, DataGridView tweetsDataGrid)
    {
      _logger.Update("DEBUG", "Start sending Tweet.");

      InitializeAPI(credentials);
      CheckInitialStatus(record, tweetsDataGrid);
      if (record.IsImmediately == true)
      {
        if (record.Status == TweetStatus.Starting)
        {
          HttpStatusCode response = SendTweetAsync(record);
          _statusChecker.ChangeStatusByResponse(record, response);
          _dataGridManager.UpdateStatus(tweetsDataGrid, record);
          _logger.Update("DEBUG", $"Done sending Tweet immediately. {record.Status}");
        }
      }
      else
      {
        TimeSpan timeToGo = record.DateTimeCombined - DateTime.Now;
        if (timeToGo > TimeSpan.Zero)
        {
          Thread.Sleep((int)timeToGo.TotalMilliseconds);
          HttpStatusCode response = SendTweetAsync(record);
          _statusChecker.ChangeStatusByResponse(record, response);
          _dataGridManager.UpdateStatus(tweetsDataGrid, record);
        }
      }
      return record;
    }

    private Tweet CheckInitialStatus(Tweet record, DataGridView tweetsDataGrid)
    {
      if (record.IsImmediately == true)
      {
        _statusChecker.CheckStatusOfSendImmediately(record);
        _dataGridManager.UpdateStatus(tweetsDataGrid, record);
      }
      else
      {
        _statusChecker.CheckStatus(record);
        _dataGridManager.UpdateStatus(tweetsDataGrid, record);
      }

      return record;
    }

    private void InitializeAPI(Credentials credentials)
    {
      _api = new Twitter(_adapter.Adaptee(credentials));
    }

    private HttpStatusCode SendTweetAsync(Tweet tweet)
    {
      HttpStatusCode response;
      _logger.Update("DEBUG", $"Calling Twitter API. ID: {tweet.ID}");
      if (tweet.ImagePath == null)
      {
        response = _api.Tweet(tweet.FullText);
      }
      else
      {
        response = _api.Tweet(tweet.FullText, tweet.ImagePath);
      }
      _logger.Update("DEBUG", $"Response sending Tweet async. {response}");

      return response;
    }
  }
}
