using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterAPIHandler.Business;
using UserInterface.Business;
using UserInterface.Local;
using UserInterface.Model;

namespace UserInterface
{
  public partial class TweetAutomationFrom : Form
  {
    private const string _filepath = "TweetRecords.bin";
    private CredentialSetting credentialSetting = new CredentialSetting();
    private Twitter _twitter;
    private TweetRecords _records;
    private TweetRecordFactory _factory;
    private RecordSaverBinary _saver;
    public TweetAutomationFrom()
    {
      InitializeComponent();
      ReloadCredential();

      _twitter = new Twitter();
      _records = new TweetRecords();
      _factory = new TweetRecordFactory(_records);
      _saver = new RecordSaverBinary(_records, _filepath);

      _saver.CreateFileIfNotExist();
      _records.Update(_saver.Read<TweetRecords>().Records);

      TweetDataGrid.AutoGenerateColumns = false;
      DatePicker.Value = DateTime.Now;
      DatePicker.MinDate = DateTime.Now;
      TimePicker.Value = DateTime.Now;
    }

    private void ButtonSave(object sender, EventArgs e)
    {
      credentialSetting.ConsumerKeySetting = ConsumerKey.Text;
      credentialSetting.ConsumerSecretSetting = ConsumerSecret.Text;
      credentialSetting.AccessTokenKeySetting = AccessTokenKey.Text;
      credentialSetting.AccessTokenSecretSetting = AccessTokenSecret.Text;
      credentialSetting.Save();
    }

    private void ButtonClear(object sender, EventArgs e)
    {
      ClearTwitterAPIForm();
      credentialSetting.Reset();
    }

    private void ButtonSend(object sender, EventArgs e)
    {
      _twitter.SetCredential(
        ConsumerKey.Text, ConsumerSecret.Text,
        AccessTokenKey.Text, AccessTokenSecret.Text);

      CreateDataFrameRecord();
      // _ = CreateTweetAsync(TweetText.Text);
    }

    private void ReloadCredential()
    {
      ConsumerKey.Text = credentialSetting.ConsumerKeySetting;
      ConsumerSecret.Text = credentialSetting.ConsumerSecretSetting;
      AccessTokenKey.Text = credentialSetting.AccessTokenKeySetting;
      AccessTokenSecret.Text = credentialSetting.AccessTokenSecretSetting;
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
      _saver.UpdateBinary(_records);
      
      TweetDataGrid.Rows.Insert(0,
        record.ID, record.Tweet, record.DateString,
        record.TimeString, record.Status, "Delete");
      SetUpTimerAndSendTweet(record);
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
          TweetDataGrid.Rows.Remove(TweetDataGrid.CurrentRow);
      }
    }
  }
}
