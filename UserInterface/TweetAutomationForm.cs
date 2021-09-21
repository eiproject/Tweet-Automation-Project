﻿using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterAPIHandler.Business;
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
    private ITwitter _twitter;
    private ITweetRecords _records;
    private ITweetRecordFactory _factory;
    private ISaverBinary _tweetRecordsSaver;
    private ISaverBinary _credentialSaver;
    private IStatusChecker _statusChecker;
    private Credentials _credentials;
    public TweetAutomationFrom()
    {
      InitializeComponent();

      _twitter = new Twitter();
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
    }

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
      ClearTwitterAPIForm();
      UpdateCredentials();
      _credentialSaver.Delete();
    }

    private void ButtonSend(object sender, EventArgs e)
    {
      UpdateCredentials();
      SaveCredentialToBinaryFile();
      UpdateTwitterAPICredentials(_credentials);
      if (SendImmediatelyRadio.Checked == true)
      {
        SendImmediately();
      }
      else
      {
        PlaceOnQueue();
      }
    }

    private void PlaceOnQueue()
    {
      TweetRecord record =
        _factory.Create(TweetText.Text, DatePicker.Value, TimePicker.Value);

      _statusChecker.CheckStatus(record);
      CreateDataFrameRecord(record);
      SetUpTimerAndSendTweet(record);
      TweetText.Clear();
    }

    private void SendImmediately()
    {
      TweetRecord record =
        _factory.Create(TweetText.Text, DateTime.Now, DateTime.Now);

      CreateDataFrameRecord(record);
      Task.Factory.StartNew(async () =>
      {
        loggerText.Invoke(new Action(() => loggerText.Text = "Task running..."));
        HttpStatusCode response = await SendTweetAsync(record);
        loggerText.Invoke(new Action(() => loggerText.Text = response.ToString()));
        ChangeStatusToSuccess(record);
        _tweetRecordsSaver.UpdateBinary(_records);
      });
    }

    private void UpdateCredentials()
    {
      _credentials.ConsumerKey = ConsumerKey.Text;
      _credentials.ConsumerSecret = ConsumerSecret.Text;
      _credentials.AccessTokenKey = AccessTokenKey.Text;
      _credentials.AccessTokenSecret = AccessTokenSecret.Text;
    }

    private void SaveCredentialToBinaryFile()
    {
      _credentialSaver.UpdateBinary(_credentials);
    }

    private void UpdateTwitterAPICredentials(Credentials credential)
    {
      _twitter.SetCredential(
        credential.ConsumerKey, credential.ConsumerSecret,
        credential.AccessTokenKey, credential.AccessTokenSecret);

    }

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

    private void ClearTwitterAPIForm()
    {
      ConsumerKey.Clear();
      ConsumerSecret.Clear();
      AccessTokenKey.Clear();
      AccessTokenSecret.Clear();
    }

    private void CreateDataFrameRecord(TweetRecord record)
    {
      _records.Add(record);
      _tweetRecordsSaver.UpdateBinary(_records);

      UpdateDataGrid(record);
    }

    private void UpdateDataGridWithSavedBinary(ITweetRecords records)
    {
      foreach (TweetRecord record in records.Records)
      {
        _statusChecker.CheckStatus(record);
        UpdateDataGrid(record);
        SetUpTimerAndSendTweet(record);
      }
    }
    private void UpdateDataGrid(TweetRecord record)
    {
      TweetDataGrid.Rows.Insert(0,
        record.ID, record.Tweet, record.DateString,
        record.TimeString, record.Status, "Delete");
    }

    private void SetUpTimerAndSendTweet(TweetRecord record)
    {
      System.Threading.Timer timer;
      TimeSpan timeToGo = record.DateTimeCombined - DateTime.Now;
      if (timeToGo < TimeSpan.Zero) return;

      timer = new System.Threading.Timer(async x =>
      {
        await SendTweetAsync(record);
        ChangeStatusToSuccess(record);
        _tweetRecordsSaver.UpdateBinary(_records);
      }, null, timeToGo, System.Threading.Timeout.InfiniteTimeSpan);
    }

    private async Task<HttpStatusCode> SendTweetAsync(TweetRecord record)
    {
      HttpStatusCode response = await _twitter.Tweet(record.Tweet);
      Console.WriteLine(response);

      return response;
    }

    private void ChangeStatusToSuccess(TweetRecord record)
    {
      int rowCount = TweetDataGrid.Rows.Count;
      for (int i = 0; i < rowCount - 1; i++)
      {
        if (TweetDataGrid.Rows[i].Cells[0].Value.ToString() == record.ID.ToString())
        {
          TweetDataGrid.Rows[i].Cells[4].Value = "Success";
          _statusChecker.ChangeToSuccess(record);
        }
      }
    }

    private void TweetDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
  }
}
