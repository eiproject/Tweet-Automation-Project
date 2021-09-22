using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterAPIHandler.Business;
using TwitterAPIHandler.Model;
using UserInterface.Business;
using UserInterface.Factory;
using UserInterface.Local;
using UserInterface.Model;

namespace UserInterface
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
    public TweetAutomationFrom()
    {
      InitializeComponent();

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
      DatePicker.Value = DateTime.Now;
      DatePicker.MinDate = DateTime.Now;
      TimePicker.Value = DateTime.Now;

#if DEBUG
      loggerText.Visible = true;
#else
      loggerText.Visible = false;
#endif
    }

    #region All Button Click Event
    private void ExitButtonStripMenuItem(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void AboutButtonStripMenuItem(object sender, EventArgs e)
    {
      AboutForm about = new AboutForm();
      about.ShowDialog();
    }

    private void ButtonSave(object sender, EventArgs e)
    {
      UpdateCredentials();
      SaveCredentialToBinaryFile();
    }

    private void ButtonClear(object sender, EventArgs e)
    {
      ClearTwitterAPICredentialForm();
      UpdateCredentials();
      _credentialSaver.Delete();
    }

    private void ButtonSend(object sender, EventArgs e)
    {
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
      _credentialSaver.UpdateBinary(_credentials);
    }

    private void UpdateCredentials()
    {
      _credentials.ConsumerKey = ConsumerKey.Text;
      _credentials.ConsumerSecret = ConsumerSecret.Text;
      _credentials.AccessTokenKey = AccessTokenKey.Text;
      _credentials.AccessTokenSecret = AccessTokenSecret.Text;
    }

    private void ClearTwitterAPICredentialForm()
    {
      ConsumerKey.Clear();
      ConsumerSecret.Clear();
      AccessTokenKey.Clear();
      AccessTokenSecret.Clear();
    }

    #endregion

    #region Tweet Command
    private void PlaceRequestOnQueue()
    {
      TweetRecord record =
        _factory.Create(TweetText.Text, DatePicker.Value, TimePicker.Value);

      _statusChecker.CheckStatus(record);
      UpdateDataFrameRecord(record);
      SetUpTimerAndSendTweet(record);
      TweetText.Clear();
    }

    private void SendRequestImmediately()
    {
      ITwitter twtAPI = new Twitter(_credentials);

      TweetRecord record =
        _factory.Create(TweetText.Text, DateTime.Now, DateTime.Now);
      _statusChecker.CheckStatusOfSendImmediately(record);
      UpdateDataFrameRecord(record);

      if (record.Status != "Starting") return;

      Task.Factory.StartNew(async () =>
      {
        try
        {
          HttpStatusCode response = await SendTweetAsync(twtAPI, record);
          _statusChecker.ChangeStatusByResponse(record, response);
          UpdateStatusOnDataGrid(record);
          _tweetRecordsSaver.UpdateBinary(_records);
        }
        catch (Exception e)
        {
          // Console.WriteLine(e.ToString());
        }
      });
    }

    private void SetUpTimerAndSendTweet(TweetRecord record)
    {
      ITwitter twtAPI = new Twitter(_credentials);

      System.Threading.Timer timer;
      TimeSpan timeToGo = record.DateTimeCombined - DateTime.Now;
      if (timeToGo < TimeSpan.Zero) return;

      timer = new System.Threading.Timer(async x =>
      {
        HttpStatusCode response = await SendTweetAsync(twtAPI, record);
        loggerText.Invoke(new Action(() => loggerText.Text = response.ToString()));
        _statusChecker.ChangeStatusByResponse(record, response);
        UpdateStatusOnDataGrid(record);
        _tweetRecordsSaver.UpdateBinary(_records);
      }, null, timeToGo, System.Threading.Timeout.InfiniteTimeSpan);
    }

    private async Task<HttpStatusCode> SendTweetAsync(
      ITwitter twitterAPI, TweetRecord record)
    {
      HttpStatusCode response = await twitterAPI.Tweet(record.Tweet);
      loggerText.Invoke(new Action(() => loggerText.Text = response.ToString()));

      return response;
    }


    #endregion

    #region Data Grid
    private void UpdateDataGridWithSavedBinary(ITweetRecords records)
    {
      foreach (TweetRecord record in records.Records)
      {
        _statusChecker.CheckStatus(record);
        InsertRecordToDataGrid(record);
        SetUpTimerAndSendTweet(record);
      }
    }

    private void UpdateDataFrameRecord(TweetRecord record)
    {
      _records.Add(record);
      _tweetRecordsSaver.UpdateBinary(_records);

      InsertRecordToDataGrid(record);
    }


    private void InsertRecordToDataGrid(TweetRecord record)
    {
      TweetDataGrid.Rows.Insert(0,
        record.ID, record.Tweet, record.DateString,
        record.TimeString, record.Status, "Delete");
    }

    private void UpdateStatusOnDataGrid(TweetRecord record)
    {
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
  }
}
