using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TweetAutomation.UserInterface
{
  public partial class AboutForm : Form
  {
    public AboutForm()
    {
      InitializeComponent();
    }

    private void ClickOfficialSite(object sender, LinkLabelLinkClickedEventArgs e)
    {
      string url = "https://github.com/eiproject/Tweet-Automation-Project";
      ProcessStartInfo sInfo = new ProcessStartInfo(url)
      {
        UseShellExecute = true,
        Verb = "open"
      };
      Process.Start(sInfo);
    }
  }
}
