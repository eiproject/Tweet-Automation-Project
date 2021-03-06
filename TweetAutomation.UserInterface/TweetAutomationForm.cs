using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TweetAutomation.LoggingSystem.BusinessLogic;
using TweetAutomation.UserInterface.BusinessLogic;
using TweetAutomation.UserInterface.DataAccessLocal;
using TweetAutomation.UserInterface.DataAccessOnline;
using TweetAutomation.UserInterface.Database;
using TweetAutomation.UserInterface.Factory;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface
{
  public partial class TweetAutomationFrom : Form
  {
    private const string _tweetRecordsBinaryFilepath = "Tweets.bin";
    private const string _credentialsBinaryFilepath = "Credentials.bin";
    private ILogRepository _logger = LogRepository.LogInstance();
    private ITweetsRepository _tweetsRepository;
    private ITweetRecordFactory _tweetFactory;
    private ISaverBinary _tweetRecordsSaver;
    private ISaverBinary _credentialSaver;
    private IStatusChecker _statusChecker;
    private Tweets _dbInstance;
    private TwitterAPIAccess _api;

    public TweetAutomationFrom()
    {
      _logger.Update("DEBUG", "New Tweet Automation instance called.");
      InitializeComponent();

      _statusChecker = new StatusChecker();
      _api = new TwitterAPIAccess();

      InitializeDatabase();
      InitializeCustomProperties();

      _tweetsRepository = new TweetsRepository(_dbInstance);
      _tweetFactory = new TweetRecordFactory(_dbInstance);
    }

    private void InitializeDatabase()
    {
      _tweetRecordsSaver = new RecordSaverBinary(_tweetRecordsBinaryFilepath);
      _credentialSaver = new CredentialSaverBinary(_credentialsBinaryFilepath);
      _tweetRecordsSaver.CreateFileIfNotExist();
      _credentialSaver.CreateFileIfNotExist();
      LoadDatabaseInstane();
      UpdateDataGridWithSavedBinary();
      UpdateCredentialsWithSavedBinary();
    }

    private void InitializeCustomProperties()
    {
      TweetDataGrid.AutoGenerateColumns = false;

      DatePicker.Value = DateTime.Today;
      DatePicker.MinDate = DateTime.Today;
      TimePicker.Value = DateTime.Now;

      ConsumerKey.GotFocus += ChangeToNoAsterisk;
      ConsumerKey.LostFocus += ChangeToAsterisk;
      ConsumerSecret.GotFocus += ChangeToNoAsterisk;
      ConsumerSecret.LostFocus += ChangeToAsterisk;
      AccessTokenKey.GotFocus += ChangeToNoAsterisk;
      AccessTokenKey.LostFocus += ChangeToAsterisk;
      AccessTokenSecret.GotFocus += ChangeToNoAsterisk;
      AccessTokenSecret.LostFocus += ChangeToAsterisk;

      // Custom event args
      this.Closing += minimizeToTray;
      tweet_automation_notify.MouseClick += restoreWindow;

#if DEBUG
      loggerText.Visible = false;
#else
      loggerText.Visible = false;
#endif
    }

    private void LoadDatabaseInstane()
    {
      _dbInstance = (Tweets)_tweetRecordsSaver.Read<Tweets>();
      if (_dbInstance == null) _dbInstance = Tweets.GetInstance();
    }

    #region All Buttons Click Event
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
      SaveCredentialToBinary();
    }

    private void ButtonClear(object sender, EventArgs e)
    {
      CredentialsFieldClear();
      _credentialSaver.DeleteBinaryFile();
    }

    private void ButtonSend(object sender, EventArgs e)
    {
      Tweet tweet = GetTweet();
      SaveCredentialToBinary();
      Sendtweet(tweet);
      UpdateDataGridRecord(tweet);
      ClearTweetForm();
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
          _tweetsRepository.Delete(recordID);
          _tweetRecordsSaver.UpdateBinary(_dbInstance);
          TweetDataGrid.Rows.Remove(TweetDataGrid.CurrentRow);
        }
      }
    }

    private void ChooseImageButtonClick(object sender, EventArgs e)
    {
      ChooseImage();
    }

    private void ImageBoxClick(object sender, EventArgs e)
    {
      ChooseImage();
    }
    #endregion

    #region Credential
    private Credentials GetCredentials()
    {
      return new Credentials()
      {
        ConsumerKey = ConsumerKey.Text,
        ConsumerSecret = ConsumerSecret.Text,
        AccessTokenKey = AccessTokenKey.Text,
        AccessTokenSecret = AccessTokenSecret.Text
      };
    }
    private void UpdateCredentialsWithSavedBinary()
    {
      _logger.Update("DEBUG", "Updating credential with saved bin.");
      Credentials loadedCredentials = (Credentials)_credentialSaver.Read<TwitterAPIHandler.Model.Credentials>();
      if (loadedCredentials == null) loadedCredentials = new Credentials();
      ConsumerKey.Text = loadedCredentials.ConsumerKey;
      ConsumerSecret.Text = loadedCredentials.ConsumerSecret;
      AccessTokenKey.Text = loadedCredentials.AccessTokenKey;
      AccessTokenSecret.Text = loadedCredentials.AccessTokenSecret;
    }

    private void SaveCredentialToBinary()
    {
      _logger.Update("DEBUG", "Saving credential to bin.");
      _credentialSaver.UpdateBinary(GetCredentials());
    }

    private void CredentialsFieldClear()
    {
      _logger.Update("DEBUG", "Clear credential form.");
      ConsumerKey.Clear();
      ConsumerSecret.Clear();
      AccessTokenKey.Clear();
      AccessTokenSecret.Clear();
    }

    private void ChangeToAsterisk(object sender, EventArgs e)
    {
      ((TextBox)sender).PasswordChar = '*';
    }

    private void ChangeToNoAsterisk(object sender, EventArgs e)
    {
      ((TextBox)sender).PasswordChar = '\0';
    }

    #endregion

    #region Tweet Command
    private Tweet GetTweet()
    {
      return _tweetFactory.Create(
        TweetText.Text, DatePicker.Value, TimePicker.Value,
        SendImmediatelyCheckBox.Checked, TweetImageBox.ImageLocation);
    }

    private void Sendtweet(Tweet tweet)
    {
      _logger.Update("ACCESS", "Sending Tweet.");
      Task.Factory.StartNew(() =>
      {
        Tweet response = _api.SendTweet(GetCredentials(), tweet, TweetDataGrid);
        UpdateRecords(response);
      });
    }

    private void ChooseImage()
    {
      OpenFileDialog open = new OpenFileDialog();
      open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
      if (open.ShowDialog() == DialogResult.OK)
      {
        TweetImageBox.Image = new Bitmap(open.FileName);
        TweetImageBox.BackColor = Color.WhiteSmoke;
        TweetImageBox.ImageLocation = open.FileName;
        loggerText.Invoke(new Action(() => loggerText.Text = open.FileName));
      }
    }

    private void ClearTweetForm()
    {
      TweetText.Clear();
      TweetImageBox.Image = null;
      TweetImageBox.ImageLocation = null;
      TweetImageBox.BackColor = Color.Gray;
    }

    #endregion

    #region Data Grid
    private void UpdateDataGridWithSavedBinary()
    {
      _logger.Update("DEBUG", "Updating DataGrid with saved binary.");
      foreach (Tweet tweet in _dbInstance.Records)
      {
        _statusChecker.CheckStatus(tweet);
        Sendtweet(tweet);
        InsertRecordToDataGrid(tweet);
      }
    }

    private void UpdateDataGridRecord(Tweet record)
    {
      _logger.Update("DEBUG", $"Updating DataGrid with new record. ID: {record.ID}");
      _tweetsRepository.Append(record);
      _tweetRecordsSaver.UpdateBinary(_dbInstance);

      InsertRecordToDataGrid(record);
    }


    private void InsertRecordToDataGrid(Tweet record)
    {
      _logger.Update("DEBUG", $"Insert record on DataGrid. ID: {record.ID}");
      TweetDataGrid.Rows.Insert(0,
        record.ID, record.FullText, record.DateString,
        record.TimeString, record.Status, "Delete");
    }

    internal void UpdateRecords(Tweet record)
    {
      // UpdateStatusOnDataGrid(record);
      _tweetRecordsSaver.UpdateBinary(_dbInstance);
    }

    private void UpdateStatusOnDataGrid(Tweet record)
    {
      _logger.Update("DEBUG", $"Updating status on DataGrid. ID: {record.ID}");
      int rowCount = TweetDataGrid.Rows.Count;
      for (int i = 0; i < rowCount - 1; i++)
      {
        if (TweetDataGrid.Rows[i].Cells[0].Value.ToString() == record.ID.ToString())
        {
          TweetDataGrid.Rows[i].Cells[4].Value = record.Status.ToString().Replace('_', ' ');
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
      Application.Exit();
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
