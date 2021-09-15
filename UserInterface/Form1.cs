using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterAPIHandler.Business;
using UserInterface.Business;
using UserInterface.Model;

namespace UserInterface {
  public partial class TweetAutomationFrom : Form {
    private CredentialSetting credentialSetting = new CredentialSetting();
    private Twitter _twitter;
    private TweetRecords _records;
    public TweetAutomationFrom() {
      InitializeComponent();
      ReloadCredential();

      _twitter = new Twitter();
      _records = new TweetRecords();

      TweetDataGrid.AutoGenerateColumns = false;
      _records.Records.Add(
        new TweetRecord(1, "a", "b", "c", "d")
        );
      _records.Records.Add(
        new TweetRecord(2, "b", "b", "c", "d")
        );
      TweetDataGrid.Rows.Add("a", "b", "c", "d", "a");
    }

    private void button_save(object sender, EventArgs e) {
      credentialSetting.ConsumerKeySetting = ConsumerKey.Text;
      credentialSetting.ConsumerSecretSetting = ConsumerSecret.Text;
      credentialSetting.AccessTokenKeySetting = AccessTokenKey.Text;
      credentialSetting.AccessTokenSecretSetting = AccessTokenSecret.Text;
      credentialSetting.Save();
    }

    private void button_clear(object sender, EventArgs e) {
      ClearTwitterAPIForm();
      credentialSetting.Reset();
    }

    private void button_send(object sender, EventArgs e) {
      _twitter.SetCredential(
        ConsumerKey.Text, ConsumerSecret.Text,
        AccessTokenKey.Text, AccessTokenSecret.Text);

      CreateDataFrameRecord();
      // _ = CreateTweetAsync(TweetText.Text);
    }

    private void ReloadCredential() {
      ConsumerKey.Text = credentialSetting.ConsumerKeySetting;
      ConsumerSecret.Text = credentialSetting.ConsumerSecretSetting;
      AccessTokenKey.Text = credentialSetting.AccessTokenKeySetting;
      AccessTokenSecret.Text = credentialSetting.AccessTokenSecretSetting;
    }

    private void ClearTwitterAPIForm() {
      ConsumerKey.Clear();
      ConsumerSecret.Clear();
      AccessTokenKey.Clear();
      AccessTokenSecret.Clear();
    }

    private void CreateDataFrameRecord() {
      DataGridViewButtonColumn button = new DataGridViewButtonColumn();
      button.Name = "button";
      button.HeaderText = "Button";
      button.Text = "Button";
      button.UseColumnTextForButtonValue = true; //dont forget this line
      TweetDataGrid.Rows.Insert(1, 1, 2, 3, 4, 5, button);
      // TweetDataGrid.Columns.Add(button);
    }

    private async Task CreateTweetAsync(string text) {
      var response = await _twitter.Tweet(text);
      Console.WriteLine(response);
    }

    private void TweetDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {
      //Check deleted rows
      // loggerText.Text = e.ColumnIndex.ToString();
      if (TweetDataGrid.Columns[e.ColumnIndex].Name != "Delete") {
        if (MessageBox.Show("Are you sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          Console.WriteLine();
        TweetDataGrid.Rows.Remove(TweetDataGrid.CurrentRow);
      }
    }
  }
}
