
namespace UserInterface {
  partial class TweetAutomationFrom {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
      this.saveCredentialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadCredentialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(761, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveCredentialToolStripMenuItem,
            this.loadCredentialToolStripMenuItem,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem});
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
      this.editToolStripMenuItem.Text = "Edit";
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // ConsumerKey
      // 
      this.ConsumerKey.Location = new System.Drawing.Point(443, 56);
      this.ConsumerKey.Name = "ConsumerKey";
      this.ConsumerKey.PlaceholderText = "Consumer Key";
      this.ConsumerKey.Size = new System.Drawing.Size(302, 23);
      this.ConsumerKey.TabIndex = 1;
      // 
      // ConsumerSecret
      // 
      this.ConsumerSecret.Location = new System.Drawing.Point(443, 86);
      this.ConsumerSecret.Name = "ConsumerSecret";
      this.ConsumerSecret.PlaceholderText = "Consumer Secret";
      this.ConsumerSecret.Size = new System.Drawing.Size(302, 23);
      this.ConsumerSecret.TabIndex = 2;
      // 
      // CredentialLabel
      // 
      this.CredentialLabel.AutoSize = true;
      this.CredentialLabel.Location = new System.Drawing.Point(442, 38);
      this.CredentialLabel.Name = "CredentialLabel";
      this.CredentialLabel.Size = new System.Drawing.Size(147, 15);
      this.CredentialLabel.TabIndex = 3;
      this.CredentialLabel.Text = "Your Twitter API Credential";
      // 
      // SaveButton
      // 
      this.SaveButton.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.SaveButton.Location = new System.Drawing.Point(442, 174);
      this.SaveButton.Name = "SaveButton";
      this.SaveButton.Size = new System.Drawing.Size(75, 23);
      this.SaveButton.TabIndex = 4;
      this.SaveButton.Text = "Save";
      this.SaveButton.UseVisualStyleBackColor = false;
      this.SaveButton.Click += new System.EventHandler(this.button_save);
      // 
      // ClearButton
      // 
      this.ClearButton.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.ClearButton.Location = new System.Drawing.Point(523, 174);
      this.ClearButton.Name = "ClearButton";
      this.ClearButton.Size = new System.Drawing.Size(75, 23);
      this.ClearButton.TabIndex = 5;
      this.ClearButton.Text = "Clear";
      this.ClearButton.UseVisualStyleBackColor = false;
      this.ClearButton.Click += new System.EventHandler(this.button_clear);
      // 
      // AccessTokenSecret
      // 
      this.AccessTokenSecret.Location = new System.Drawing.Point(443, 145);
      this.AccessTokenSecret.Name = "AccessTokenSecret";
      this.AccessTokenSecret.PlaceholderText = "Access Token Secret";
      this.AccessTokenSecret.Size = new System.Drawing.Size(302, 23);
      this.AccessTokenSecret.TabIndex = 7;
      // 
      // AccessTokenKey
      // 
      this.AccessTokenKey.Location = new System.Drawing.Point(443, 115);
      this.AccessTokenKey.Name = "AccessTokenKey";
      this.AccessTokenKey.PlaceholderText = "Access Token Key";
      this.AccessTokenKey.Size = new System.Drawing.Size(302, 23);
      this.AccessTokenKey.TabIndex = 6;
      // 
      // TweetText
      // 
      this.TweetText.Location = new System.Drawing.Point(13, 56);
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
      this.TweetLabel.Location = new System.Drawing.Point(13, 38);
      this.TweetLabel.Name = "TweetLabel";
      this.TweetLabel.Size = new System.Drawing.Size(64, 15);
      this.TweetLabel.TabIndex = 9;
      this.TweetLabel.Text = "Your Tweet";
      // 
      // SendButton
      // 
      this.SendButton.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.SendButton.Location = new System.Drawing.Point(13, 174);
      this.SendButton.Name = "SendButton";
      this.SendButton.Size = new System.Drawing.Size(75, 23);
      this.SendButton.TabIndex = 10;
      this.SendButton.Text = "Send";
      this.SendButton.UseVisualStyleBackColor = false;
      this.SendButton.Click += new System.EventHandler(this.button_send);
      // 
      // saveCredentialToolStripMenuItem
      // 
      this.saveCredentialToolStripMenuItem.Name = "saveCredentialToolStripMenuItem";
      this.saveCredentialToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.saveCredentialToolStripMenuItem.Text = "Save";
      // 
      // loadCredentialToolStripMenuItem
      // 
      this.loadCredentialToolStripMenuItem.Name = "loadCredentialToolStripMenuItem";
      this.loadCredentialToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.loadCredentialToolStripMenuItem.Text = "Load";
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.aboutToolStripMenuItem.Text = "About";
      // 
      // configurationToolStripMenuItem
      // 
      this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
      this.configurationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.configurationToolStripMenuItem.Text = "Config";
      // 
      // TweetAutomationFrom
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(761, 450);
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
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "TweetAutomationFrom";
      this.Text = "Tweet Automation";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
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
    private System.Windows.Forms.ToolStripMenuItem saveCredentialToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem loadCredentialToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
  }
}

