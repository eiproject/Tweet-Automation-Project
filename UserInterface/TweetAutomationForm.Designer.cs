
using System.Windows.Forms;

namespace UserInterface
{
  partial class TweetAutomationFrom
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TweetAutomationFrom));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ConsumerKey = new System.Windows.Forms.TextBox();
      this.ConsumerSecret = new System.Windows.Forms.TextBox();
      this.CredentialLabel = new System.Windows.Forms.Label();
      this.SaveButton = new System.Windows.Forms.Button();
      this.ClearButton = new System.Windows.Forms.Button();
      this.AccessTokenSecret = new System.Windows.Forms.TextBox();
      this.AccessTokenKey = new System.Windows.Forms.TextBox();
      this.TweetText = new System.Windows.Forms.TextBox();
      this.TweetLabel = new System.Windows.Forms.Label();
      this.SendButton = new System.Windows.Forms.Button();
      this.TweetDataGrid = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
      this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tweet = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.force_tweet_now = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.remove = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.DatePicker = new System.Windows.Forms.DateTimePicker();
      this.TimePicker = new System.Windows.Forms.DateTimePicker();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.loggerText = new System.Windows.Forms.Label();
      this.SendImmediatelyCheckBox = new System.Windows.Forms.CheckBox();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TweetDataGrid)).BeginInit();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(959, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitButtonStripMenuItem);
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.aboutToolStripMenuItem.Text = "About";
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutButtonStripMenuItem);
      // 
      // ConsumerKey
      // 
      this.ConsumerKey.Location = new System.Drawing.Point(623, 56);
      this.ConsumerKey.Name = "ConsumerKey";
      this.ConsumerKey.PlaceholderText = "Consumer Key";
      this.ConsumerKey.Size = new System.Drawing.Size(302, 23);
      this.ConsumerKey.TabIndex = 1;
      // 
      // ConsumerSecret
      // 
      this.ConsumerSecret.Location = new System.Drawing.Point(623, 86);
      this.ConsumerSecret.Name = "ConsumerSecret";
      this.ConsumerSecret.PlaceholderText = "Consumer Secret";
      this.ConsumerSecret.Size = new System.Drawing.Size(302, 23);
      this.ConsumerSecret.TabIndex = 2;
      // 
      // CredentialLabel
      // 
      this.CredentialLabel.AutoSize = true;
      this.CredentialLabel.Location = new System.Drawing.Point(622, 38);
      this.CredentialLabel.Name = "CredentialLabel";
      this.CredentialLabel.Size = new System.Drawing.Size(147, 15);
      this.CredentialLabel.TabIndex = 3;
      this.CredentialLabel.Text = "Your Twitter API Credential";
      // 
      // SaveButton
      // 
      this.SaveButton.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.SaveButton.Location = new System.Drawing.Point(622, 174);
      this.SaveButton.Name = "SaveButton";
      this.SaveButton.Size = new System.Drawing.Size(75, 23);
      this.SaveButton.TabIndex = 4;
      this.SaveButton.Text = "Save";
      this.SaveButton.UseVisualStyleBackColor = false;
      this.SaveButton.Click += new System.EventHandler(this.ButtonSave);
      // 
      // ClearButton
      // 
      this.ClearButton.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.ClearButton.Location = new System.Drawing.Point(703, 174);
      this.ClearButton.Name = "ClearButton";
      this.ClearButton.Size = new System.Drawing.Size(75, 23);
      this.ClearButton.TabIndex = 5;
      this.ClearButton.Text = "Clear";
      this.ClearButton.UseVisualStyleBackColor = false;
      this.ClearButton.Click += new System.EventHandler(this.ButtonClear);
      // 
      // AccessTokenSecret
      // 
      this.AccessTokenSecret.Location = new System.Drawing.Point(623, 145);
      this.AccessTokenSecret.Name = "AccessTokenSecret";
      this.AccessTokenSecret.PlaceholderText = "Access Token Secret";
      this.AccessTokenSecret.Size = new System.Drawing.Size(302, 23);
      this.AccessTokenSecret.TabIndex = 7;
      // 
      // AccessTokenKey
      // 
      this.AccessTokenKey.Location = new System.Drawing.Point(623, 115);
      this.AccessTokenKey.Name = "AccessTokenKey";
      this.AccessTokenKey.PlaceholderText = "Access Token Key";
      this.AccessTokenKey.Size = new System.Drawing.Size(302, 23);
      this.AccessTokenKey.TabIndex = 6;
      // 
      // TweetText
      // 
      this.TweetText.Location = new System.Drawing.Point(37, 56);
      this.TweetText.MaxLength = 250;
      this.TweetText.Multiline = true;
      this.TweetText.Name = "TweetText";
      this.TweetText.PlaceholderText = "Enter your tweet here...";
      this.TweetText.Size = new System.Drawing.Size(399, 112);
      this.TweetText.TabIndex = 8;
      // 
      // TweetLabel
      // 
      this.TweetLabel.AutoSize = true;
      this.TweetLabel.Location = new System.Drawing.Point(37, 38);
      this.TweetLabel.Name = "TweetLabel";
      this.TweetLabel.Size = new System.Drawing.Size(64, 15);
      this.TweetLabel.TabIndex = 9;
      this.TweetLabel.Text = "Your Tweet";
      // 
      // SendButton
      // 
      this.SendButton.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.SendButton.Location = new System.Drawing.Point(37, 174);
      this.SendButton.Name = "SendButton";
      this.SendButton.Size = new System.Drawing.Size(75, 23);
      this.SendButton.TabIndex = 10;
      this.SendButton.Text = "Send";
      this.SendButton.UseVisualStyleBackColor = false;
      this.SendButton.Click += new System.EventHandler(this.ButtonSend);
      // 
      // TweetDataGrid
      // 
      this.TweetDataGrid.AllowUserToResizeColumns = false;
      this.TweetDataGrid.AllowUserToResizeRows = false;
      this.TweetDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.TweetDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewButtonColumn1});
      this.TweetDataGrid.Location = new System.Drawing.Point(37, 231);
      this.TweetDataGrid.Name = "TweetDataGrid";
      this.TweetDataGrid.RowTemplate.Height = 25;
      this.TweetDataGrid.Size = new System.Drawing.Size(888, 150);
      this.TweetDataGrid.TabIndex = 11;
      this.TweetDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TweetDataGrid_CellContentClick);
      // 
      // dataGridViewTextBoxColumn1
      // 
      this.dataGridViewTextBoxColumn1.HeaderText = "ID";
      this.dataGridViewTextBoxColumn1.MinimumWidth = 50;
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.Width = 50;
      // 
      // dataGridViewTextBoxColumn2
      // 
      this.dataGridViewTextBoxColumn2.HeaderText = "Tweet";
      this.dataGridViewTextBoxColumn2.MinimumWidth = 395;
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.Width = 395;
      // 
      // dataGridViewTextBoxColumn3
      // 
      this.dataGridViewTextBoxColumn3.HeaderText = "Date";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      // 
      // dataGridViewTextBoxColumn4
      // 
      this.dataGridViewTextBoxColumn4.HeaderText = "Time";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      // 
      // dataGridViewTextBoxColumn5
      // 
      this.dataGridViewTextBoxColumn5.HeaderText = "Status";
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      // 
      // dataGridViewButtonColumn1
      // 
      this.dataGridViewButtonColumn1.HeaderText = "Action";
      this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
      this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewButtonColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.dataGridViewButtonColumn1.Text = "Remove";
      // 
      // id
      // 
      this.id.HeaderText = "No.";
      this.id.Name = "id";
      this.id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.id.Width = 40;
      // 
      // tweet
      // 
      this.tweet.HeaderText = "Tweet";
      this.tweet.MinimumWidth = 320;
      this.tweet.Name = "tweet";
      this.tweet.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.tweet.Width = 320;
      // 
      // date
      // 
      this.date.HeaderText = "Date";
      this.date.MinimumWidth = 100;
      this.date.Name = "date";
      this.date.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      // 
      // time
      // 
      this.time.HeaderText = "Time";
      this.time.MinimumWidth = 85;
      this.time.Name = "time";
      this.time.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.time.Width = 85;
      // 
      // status
      // 
      this.status.HeaderText = "Status";
      this.status.MinimumWidth = 100;
      this.status.Name = "status";
      this.status.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      // 
      // force_tweet_now
      // 
      this.force_tweet_now.HeaderText = "Tweet Now";
      this.force_tweet_now.MinimumWidth = 100;
      this.force_tweet_now.Name = "force_tweet_now";
      this.force_tweet_now.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      // 
      // remove
      // 
      this.remove.HeaderText = "Remove";
      this.remove.MinimumWidth = 100;
      this.remove.Name = "remove";
      this.remove.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      // 
      // DatePicker
      // 
      this.DatePicker.Location = new System.Drawing.Point(441, 73);
      this.DatePicker.MinDate = new System.DateTime(2021, 9, 15, 0, 0, 0, 0);
      this.DatePicker.Name = "DatePicker";
      this.DatePicker.Size = new System.Drawing.Size(174, 23);
      this.DatePicker.TabIndex = 12;
      this.DatePicker.Value = new System.DateTime(2021, 9, 15, 0, 0, 0, 0);
      // 
      // TimePicker
      // 
      this.TimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
      this.TimePicker.Location = new System.Drawing.Point(441, 121);
      this.TimePicker.Name = "TimePicker";
      this.TimePicker.ShowUpDown = true;
      this.TimePicker.Size = new System.Drawing.Size(174, 23);
      this.TimePicker.TabIndex = 0;
      this.TimePicker.Value = new System.DateTime(2021, 9, 15, 0, 0, 0, 0);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(442, 55);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(66, 15);
      this.label1.TabIndex = 13;
      this.label1.Text = "Target Date";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(442, 103);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(68, 15);
      this.label2.TabIndex = 14;
      this.label2.Text = "Target Time";
      // 
      // loggerText
      // 
      this.loggerText.AutoSize = true;
      this.loggerText.Location = new System.Drawing.Point(37, 416);
      this.loggerText.Name = "loggerText";
      this.loggerText.Size = new System.Drawing.Size(44, 15);
      this.loggerText.TabIndex = 16;
      this.loggerText.Text = "Logger";
      // 
      // SendImmediatelyCheckBox
      // 
      this.SendImmediatelyCheckBox.AutoSize = true;
      this.SendImmediatelyCheckBox.Location = new System.Drawing.Point(441, 152);
      this.SendImmediatelyCheckBox.Name = "SendImmediatelyCheckBox";
      this.SendImmediatelyCheckBox.Size = new System.Drawing.Size(121, 19);
      this.SendImmediatelyCheckBox.TabIndex = 18;
      this.SendImmediatelyCheckBox.Text = "Send Immediately";
      this.SendImmediatelyCheckBox.UseVisualStyleBackColor = true;
      // 
      // TweetAutomationFrom
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(959, 490);
      this.Controls.Add(this.SendImmediatelyCheckBox);
      this.Controls.Add(this.loggerText);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.TimePicker);
      this.Controls.Add(this.DatePicker);
      this.Controls.Add(this.TweetDataGrid);
      this.Controls.Add(this.SendButton);
      this.Controls.Add(this.TweetLabel);
      this.Controls.Add(this.TweetText);
      this.Controls.Add(this.AccessTokenSecret);
      this.Controls.Add(this.AccessTokenKey);
      this.Controls.Add(this.ClearButton);
      this.Controls.Add(this.SaveButton);
      this.Controls.Add(this.CredentialLabel);
      this.Controls.Add(this.ConsumerSecret);
      this.Controls.Add(this.ConsumerKey);
      this.Controls.Add(this.menuStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.MaximizeBox = false;
      this.Name = "TweetAutomationFrom";
      this.Text = "Tweet Automation";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TweetDataGrid)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.TextBox ConsumerKey;
    private System.Windows.Forms.TextBox ConsumerSecret;
    private System.Windows.Forms.Label CredentialLabel;
    private System.Windows.Forms.Button SaveButton;
    private System.Windows.Forms.Button ClearButton;
    private System.Windows.Forms.TextBox AccessTokenSecret;
    private System.Windows.Forms.TextBox AccessTokenKey;
    private System.Windows.Forms.TextBox TweetText;
    private System.Windows.Forms.Label TweetLabel;
    private System.Windows.Forms.Button SendButton;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.DataGridView TweetDataGrid;
    private System.Windows.Forms.DataGridViewTextBoxColumn id;
    private System.Windows.Forms.DataGridViewTextBoxColumn tweet;
    private System.Windows.Forms.DataGridViewTextBoxColumn date;
    private System.Windows.Forms.DataGridViewTextBoxColumn time;
    private System.Windows.Forms.DataGridViewTextBoxColumn status;
    private System.Windows.Forms.DataGridViewTextBoxColumn force_tweet_now;
    private System.Windows.Forms.DataGridViewTextBoxColumn remove;
    private System.Windows.Forms.DateTimePicker DatePicker;
    private System.Windows.Forms.DateTimePicker TimePicker;
    private Label label1;
    private Label label2;
    private Label loggerText;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewButtonColumn dataGridViewButtonColumn1;
    private CheckBox SendImmediatelyCheckBox;
  }
}

