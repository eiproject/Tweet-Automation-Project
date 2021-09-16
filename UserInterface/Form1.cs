﻿using System;
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
    TweetRecordFactory _factory;
    public TweetAutomationFrom() {
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
      // loggerText.Text = TimePicker.Value.ToString("dd MM yyyy || HH : mm");
      loggerText.Text = (DatePicker.Value - DateTime.Now).Days.ToString();
      TweetRecord record = _factory.Create(
        TweetText.Text, DatePicker.Value, TimePicker.Value
        );
      _records.Add(record);
      TweetDataGrid.Rows.Insert(0, 
        record.ID, record.Tweet, record.DateString,
        record.TimeString, record.Status, "Delete");
    }

    private async Task CreateTweetAsync(string text) {
      var response = await _twitter.Tweet(text);
      Console.WriteLine(response);
    }

    private void TweetDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {
      var buttonValue = TweetDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? "null";
      if (TweetDataGrid.Columns[e.ColumnIndex].Name != "Delete" && buttonValue.ToString().ToLower() == "delete") {
        if (MessageBox.Show(
          "Are you sure want to delete this record ?",
          "Message",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          TweetDataGrid.Rows.Remove(TweetDataGrid.CurrentRow);
      }
    }
  }
}