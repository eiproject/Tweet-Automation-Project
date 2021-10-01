using System;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using TweetAutomation.LoggingSystem.Business;
using TweetAutomation.TwitterAPIHandler.Business;
using TweetAutomation.TwitterAPIHandler.Model;
using TweetAutomation.UserInterface.Business;
using TweetAutomation.UserInterface.Factory;
using TweetAutomation.UserInterface.Local;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface
{
  public partial class TweetAutomationFrom : Form
  {
    private const string _tweetRecordsBinaryFilepath = "TweetRecords.bin";
    private const string _credentialsBinaryFilepath = "Credentials.bin";
    private ITweetRecords _records;
    private ITweetRecordFactory _factory;
    private ISaverBinary _tweetRecordsSaver;
    private ISaverBinary _credentialSaver;
    private IStatusChecker _statusChecker;
    private Credentials _credentials;
    private LogRepository _logger = LogRepository.LogInstance();
    public TweetAutomationFrom()
    {
      InitializeComponent();
      _logger.Update("DEBUG", "New Tweet Automation instance called.");

      _records = new TweetRecords();
      _factory = new TweetRecordFactory(_records);
      _tweetRecordsSaver = new RecordSaverBinary(_tweetRecordsBinaryFilepath);
      _credentialSaver = new CredentialSaverBinary(_credentialsBinaryFilepath);
      _statusChecker = new StatusChecker();
      _credentials = new Credentials();

      _tweetRecordsSaver.CreateFileIfNotExist();
      _records.Update((TweetRecords)_tweetRecordsSaver.Read<TweetRecords>());
      UpdateDataGridWithSavedBinary(_records);

      _credentialSaver.CreateFileIfNotExist();
      UpdateCredentialsWithSavedBinary();
      TweetDataGrid.AutoGenerateColumns = false;
      DatePicker.Value = DateTime.Today;
      DatePicker.MinDate = DateTime.Today;
      TimePicker.Value = DateTime.Now;

      // Custom event args
      this.Closing += minimizeToTray;
      tweet_automation_notify.MouseClick += restoreWindow;

#if DEBUG
      loggerText.Visible = true;
#else
      loggerText.Visible = false;
#endif
    }

    #region All Button Click Event
    private void ExitButtonStripMenuItem(object sender, EventArgs e)
    {
      _logger.Update("ACCESS", "Exit button clicked.");
      Application.Exit();
    }

    private void AboutButtonStripMenuItem(object sender, EventArgs e)
    {
      _logger.Update("ACCESS", "About button clicked.");
      AboutForm about = new AboutForm();
      about.ShowDialog();
    }

    private void ButtonSave(object sender, EventArgs e)
    {
      _logger.Update("ACCESS", "Saving credential.");
      UpdateCredentials();
      SaveCredentialToBinaryFile();
    }

    private void ButtonClear(object sender, EventArgs e)
    {
      _logger.Update("ACCESS", "Clearing credential.");
      ClearTwitterAPICredentialForm();
      UpdateCredentials();
      _credentialSaver.Delete();
    }

    private void ButtonSend(object sender, EventArgs e)
    {
      _logger.Update("ACCESS", "Sending Tweet.");
      UpdateCredentials();
      SaveCredentialToBinaryFile();
      if (SendImmediatelyCheckBox.Checked == true)
      {
        SendRequestImmediately();
      }
      else
      {
        PlaceRequestOnQueue();
      }
    }

    private void DeleteButton(object sender, DataGridViewCellEventArgs e)
    {
      _logger.Update("ACCESS", "Deleting Tweet.");
      if (e.RowIndex < 0 || e.ColumnIndex < 0) { return; }
      var buttonValue = TweetDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? "null";
      if (TweetDataGrid.Columns[e.ColumnIndex].Name != "Delete" && buttonValue.ToString().ToLower() == "delete")
      {
        if (MessageBox.Show(
          "Are you sure want to delete this record ?",
          "Message",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          int recordID = int.Parse(TweetDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
          _records.Delete(recordID);
          _tweetRecordsSaver.UpdateBinary(_records);
          TweetDataGrid.Rows.Remove(TweetDataGrid.CurrentRow);
        }
      }
    }
    #endregion

    #region Credential
    private void UpdateCredentialsWithSavedBinary()
    {
      _logger.Update("DEBUG", "Updating credential with saved bin.");
      _credentials = (Credentials)_credentialSaver.Read<Credentials>();
      if (_credentials != null)
      {
        ConsumerKey.Text = _credentials.ConsumerKey;
        ConsumerSecret.Text = _credentials.ConsumerSecret;
        AccessTokenKey.Text = _credentials.AccessTokenKey;
        AccessTokenSecret.Text = _credentials.AccessTokenSecret;
      }
    }

    private void SaveCredentialToBinaryFile()
    {
      _logger.Update("DEBUG", "Saving credential to bin.");
      _credentialSaver.UpdateBinary(_credentials);
    }

    private void UpdateCredentials()
    {
      _logger.Update("DEBUG", "Updating credential object.");
      _credentials.ConsumerKey = ConsumerKey.Text;
      _credentials.ConsumerSecret = ConsumerSecret.Text;
      _credentials.AccessTokenKey = AccessTokenKey.Text;
      _credentials.AccessTokenSecret = AccessTokenSecret.Text;
    }

    private void ClearTwitterAPICredentialForm()
    {
      _logger.Update("DEBUG", "Clear credential form.");
      ConsumerKey.Clear();
      ConsumerSecret.Clear();
      AccessTokenKey.Clear();
      AccessTokenSecret.Clear();
    }

    #endregion

    #region Tweet Command
    private void PlaceRequestOnQueue()
    {
      _logger.Update("ACCESS", "Queueing Tweet.");
      TweetRecord record =
        _factory.Create(TweetText.Text, DatePicker.Value, TimePicker.Value);

      _statusChecker.CheckStatus(record);
      UpdateDataGridRecord(record);
      SetUpTimerAndSendTweet(record);
      TweetText.Clear();
    }

    private void SendRequestImmediately()
    {
      _logger.Update("ACCESS", "Sending Tweet immediately.");
      ITwitter twtAPI = new Twitter(_credentials);

      TweetRecord record =
        _factory.Create(TweetText.Text, DateTime.Now, DateTime.Now);
      _statusChecker.CheckStatusOfSendImmediately(record);
      UpdateDataGridRecord(record);

      if (record.Status != "Starting") return;

      Task.Factory.StartNew(async () =>
      {
        try
        {
          HttpStatusCode response = await SendTweetAsync(twtAPI, record);
          _statusChecker.ChangeStatusByResponse(record, response);
          UpdateRecords(record);
        }
        catch (Exception e)
        {
          _logger.Update("ERROR", e.Message);
        }
      });
    }

    private void SetUpTimerAndSendTweet(TweetRecord record)
    {
      try
      {
        _logger.Update("DEBUG", $"Setup timer and sending Tweet. ID: {record.ID}");
        ITwitter twtAPI = new Twitter(_credentials);

        System.Threading.Timer timer;
        TimeSpan timeToGo = record.DateTimeCombined - DateTime.Now;
        if (timeToGo < TimeSpan.Zero) return;

        timer = new System.Threading.Timer(async x =>
        {
          HttpStatusCode response = await SendTweetAsync(twtAPI, record);
          loggerText.Invoke(new Action(() => loggerText.Text = response.ToString()));
          _statusChecker.ChangeStatusByResponse(record, response);
          UpdateRecords(record);
        }, null, timeToGo, System.Threading.Timeout.InfiniteTimeSpan);
      }
      catch (ArgumentOutOfRangeException e)
      {
        _statusChecker.ChangeStatusByResponse(record, HttpStatusCode.Forbidden);
        UpdateRecords(record);
        _logger.Update("ERROR", $"Timer out of range. ID: {record.ID}");
      }
    }

    private async Task<HttpStatusCode> SendTweetAsync(
      ITwitter twitterAPI, TweetRecord record)
    {
      _logger.Update("DEBUG", $"Calling Twitter API. ID: {record.ID}");
      HttpStatusCode response = await twitterAPI.Tweet(record.Tweet);
      loggerText.Invoke(new Action(() => loggerText.Text = response.ToString()));

      return response;
    }


    #endregion

    #region Data Grid
    private void UpdateDataGridWithSavedBinary(ITweetRecords records)
    {
      _logger.Update("DEBUG", "Updating DataGrid with saved binary.");
      foreach (TweetRecord record in records.Records)
      {
        _statusChecker.CheckStatus(record);
        InsertRecordToDataGrid(record);
        SetUpTimerAndSendTweet(record);
      }
    }

    private void UpdateDataGridRecord(TweetRecord record)
    {
      _logger.Update("DEBUG", $"Updating DataGrid with new record. ID: {record.ID}");
      _records.Add(record);
      _tweetRecordsSaver.UpdateBinary(_records);

      InsertRecordToDataGrid(record);
    }


    private void InsertRecordToDataGrid(TweetRecord record)
    {
      _logger.Update("DEBUG", $"Insert record on DataGrid. ID: {record.ID}");
      TweetDataGrid.Rows.Insert(0,
        record.ID, record.Tweet, record.DateString,
        record.TimeString, record.Status, "Delete");
    }

    private void UpdateRecords(TweetRecord record)
    {
      UpdateStatusOnDataGrid(record);
      _tweetRecordsSaver.UpdateBinary(_records);
    }

    private void UpdateStatusOnDataGrid(TweetRecord record)
    {
      _logger.Update("DEBUG", $"Updating status on DataGrid. ID: {record.ID}");
      int rowCount = TweetDataGrid.Rows.Count;
      for (int i = 0; i < rowCount - 1; i++)
      {
        if (TweetDataGrid.Rows[i].Cells[0].Value.ToString() == record.ID.ToString())
        {
          TweetDataGrid.Rows[i].Cells[4].Value = record.Status;
          return;
        }
      }
    }

    #endregion

    #region Tray Icon Control
    private void TrayContextRestore(object sender, EventArgs e)
    {
      tweet_automation_notify.Visible = false;
      this.Show();
    }

    private void TrayContextExit(object sender, EventArgs e)
    {
      this.Close();
    }

    void minimizeToTray(object sender, CancelEventArgs e)
    {
      e.Cancel = true;
      tweet_automation_notify.Visible = true;
      this.Hide();
    }

    void restoreWindow(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        this.Show();
        tweet_automation_notify.Visible = false;
      }
    }
    #endregion
  }
}
