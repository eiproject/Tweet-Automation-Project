using System;
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
    public TweetAutomationFrom()
    {
      InitializeComponent();

      _twitter = new Twitter();
      _records = new TweetRecords();
      _factory = new TweetRecordFactory(_records);
      _tweetRecordsSaver = new RecordSaverBinary(_tweetRecordsBinaryFilepath);
      _credentialSaver = new CredentialSaverBinary(_credentialsBinaryFilepath);

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
      SaveCredential();
    }

    private void ButtonClear(object sender, EventArgs e)
    {
      ClearTwitterAPIForm();
      _credentialSaver.Delete();
    }

    private void ButtonSend(object sender, EventArgs e)
    {
      SaveCredential();
      _twitter.SetCredential(
        ConsumerKey.Text, ConsumerSecret.Text,
        AccessTokenKey.Text, AccessTokenSecret.Text);

      CreateDataFrameRecord();
      // _ = CreateTweetAsync(TweetText.Text);
    }

    private void SaveCredential()
    {
      _credentialSaver.UpdateBinary(
        new Credentials()
        {
          ConsumerKey = ConsumerKey.Text,
          ConsumerSecret = ConsumerSecret.Text,
          AccessTokenKey = AccessTokenKey.Text,
          AccessTokenSecret = AccessTokenSecret.Text
        });
    }

    private void UpdateCredentialsWithSavedBinary()
    {
      Credentials credentials = (Credentials)_credentialSaver.Read<Credentials>();
      if (credentials != null)
      {
        ConsumerKey.Text = credentials.ConsumerKey;
        ConsumerSecret.Text = credentials.ConsumerSecret;
        AccessTokenKey.Text = credentials.AccessTokenKey;
        AccessTokenSecret.Text = credentials.AccessTokenSecret;
      }
    }

    private void ClearTwitterAPIForm()
    {
      ConsumerKey.Clear();
      ConsumerSecret.Clear();
      AccessTokenKey.Clear();
      AccessTokenSecret.Clear();
    }

    private void CreateDataFrameRecord()
    {
      TweetRecord record = _factory.Create(
        TweetText.Text, DatePicker.Value, TimePicker.Value
        );
      _records.Add(record);
      _tweetRecordsSaver.UpdateBinary(_records);

      UpdateDataGrid(record);
      SetUpTimerAndSendTweet(record);
    }

    private void UpdateDataGridWithSavedBinary(ITweetRecords records)
    {
      foreach (TweetRecord record in records.Records)
      {
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
      if (timeToGo < TimeSpan.Zero)
      {
        return;
      }
      timer = new System.Threading.Timer(async x =>
      {
        await CreateTweetAsync(record);
        ChangeStatusToSuccess(record);
      }, null, timeToGo, System.Threading.Timeout.InfiniteTimeSpan);
    }

    private async Task CreateTweetAsync(TweetRecord record)
    {
      string response = await _twitter.Tweet(record.Tweet);
      loggerText.Invoke(new Action(() => loggerText.Text = response));
      Console.WriteLine(response);
    }

    private void ChangeStatusToSuccess(TweetRecord record)
    {
      int rowCount = TweetDataGrid.Rows.Count;
      for (int i = 0; i < rowCount - 1; i++)
      {
        if (TweetDataGrid.Rows[i].Cells[0].Value.ToString() == record.ID.ToString())
        {
          TweetDataGrid.Rows[i].Cells[4].Value = "Success";
          record.Status = "Success";
          // loggerText.Invoke(new Action(() => loggerText.Text = "Success"));
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
