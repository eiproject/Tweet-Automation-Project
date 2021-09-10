using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TweetAutomationProject.Business;

namespace TweetAutomationProject {
  public partial class TweetAutomationFrom : Form {
    private CredentialSetting credentialSetting = new CredentialSetting();
    public TweetAutomationFrom() {
      InitializeComponent();
      ReloadCredential();
    }

    private void ReloadCredential() {
      ConsumerKey.Text = credentialSetting.ConsumerKeySetting;
      ConsumerSecret.Text = credentialSetting.ConsumerSecretSetting;
      AccessTokenKey.Text = credentialSetting.AccessTokenKeySetting;
      AccessTokenSecret.Text = credentialSetting.AccessTokenSecretSetting;
    }
    private void button_save(object sender, EventArgs e) {
      credentialSetting.ConsumerKeySetting = ConsumerKey.Text;
      credentialSetting.ConsumerSecretSetting = ConsumerSecret.Text;
      credentialSetting.AccessTokenKeySetting = AccessTokenKey.Text;
      credentialSetting.AccessTokenSecretSetting = AccessTokenSecret.Text;
      credentialSetting.Save();
    }

    private void button_clear(object sender, EventArgs e) {
      ConsumerKey.Clear();
      ConsumerSecret.Clear();
      AccessTokenKey.Clear();
      AccessTokenSecret.Clear();
      credentialSetting.Reset();
    }

    private void button_send(object sender, EventArgs e) {

    }
  }
}
