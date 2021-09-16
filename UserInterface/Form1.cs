using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterAPIHandler.Business;
using UserInterface.Business;
using UserInterface.Model;

namespace UserInterface
{
  public partial class TweetAutomationFrom : Form
  {
    private CredentialSetting credentialSetting = new CredentialSetting();
    private Twitter _twitter;
    private TweetRecords _records;
    TweetRecordFactory _factory;
    public TweetAutomationFrom()
    {
      InitializeComponent();
      ReloadCredential();

      _twitter = new Twitter();
      _records = new TweetRecords();
      _factory = new TweetRecordFactory(_records);

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
      // loggerText.Text = TimePicker.Value.ToString("dd MM yyyy || HH : mm");
      loggerText.Text = (DatePicker.Value - DateTime.Now).Days.ToString();
      TweetRecord record = _factory.Create(
        TweetText.Text, DatePicker.Value, TimePicker.Value
        );
      _records.Add(record);
      TweetDataGrid.Rows.Insert(0,
        record.ID, record.Tweet, record.DateString,
        record.TimeString, record.Status, "Delete");
      SetUpTimer(record);
    }

    private void SetUpTimer(TweetRecord record)
    {
      System.Threading.Timer timer;
      TimeSpan timeToGo = record.DateTimeCombined - DateTime.Now;
      if (timeToGo < TimeSpan.Zero)
      {
        return;
      }
      timer = new System.Threading.Timer(x =>
      {
        ChangeStatusToSuccess(record);
      }, null, timeToGo, System.Threading.Timeout.InfiniteTimeSpan);
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
          loggerText.Invoke(new Action(() => loggerText.Text = "Success"));
        }
      }
    }

    private async Task CreateTweetAsync(string text)
    {
      var response = await _twitter.Tweet(text);
      Console.WriteLine(response);
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
