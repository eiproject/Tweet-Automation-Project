
namespace TweetAutomationProject {
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
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.textBox4 = new System.Windows.Forms.TextBox();
      this.textBox5 = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
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
      this.menuStrip1.Size = new System.Drawing.Size(829, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
      this.editToolStripMenuItem.Text = "Edit";
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(515, 56);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(302, 23);
      this.textBox1.TabIndex = 1;
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(515, 86);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(302, 23);
      this.textBox2.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(516, 35);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(147, 15);
      this.label1.TabIndex = 3;
      this.label1.Text = "Your Twitter API Credential";
      // 
      // button1
      // 
      this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.button1.Location = new System.Drawing.Point(515, 174);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 4;
      this.button1.Text = "Save";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new System.EventHandler(this.button_save);
      // 
      // button2
      // 
      this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.button2.Location = new System.Drawing.Point(596, 174);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 23);
      this.button2.TabIndex = 5;
      this.button2.Text = "Clear";
      this.button2.UseVisualStyleBackColor = false;
      this.button2.Click += new System.EventHandler(this.button_clear);
      // 
      // textBox3
      // 
      this.textBox3.Location = new System.Drawing.Point(516, 145);
      this.textBox3.Name = "textBox3";
      this.textBox3.Size = new System.Drawing.Size(302, 23);
      this.textBox3.TabIndex = 7;
      // 
      // textBox4
      // 
      this.textBox4.Location = new System.Drawing.Point(516, 115);
      this.textBox4.Name = "textBox4";
      this.textBox4.Size = new System.Drawing.Size(302, 23);
      this.textBox4.TabIndex = 6;
      // 
      // textBox5
      // 
      this.textBox5.Location = new System.Drawing.Point(13, 56);
      this.textBox5.Multiline = true;
      this.textBox5.Name = "textBox5";
      this.textBox5.Size = new System.Drawing.Size(399, 112);
      this.textBox5.TabIndex = 8;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 38);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(64, 15);
      this.label2.TabIndex = 9;
      this.label2.Text = "Your Tweet";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(829, 450);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textBox5);
      this.Controls.Add(this.textBox3);
      this.Controls.Add(this.textBox4);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox2);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.Text = "Form1";
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
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.TextBox textBox4;
    private System.Windows.Forms.TextBox textBox5;
    private System.Windows.Forms.Label label2;
  }
}

