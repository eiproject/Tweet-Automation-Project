using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace TweetAutomationProject.Business {
  sealed class CredentialSetting : ApplicationSettingsBase {
    [UserScopedSetting()]
    public String ConsumerKeySetting {
      get {
        return (((String)this["ConsumerKeySetting"]) == null)
      ? null : ((String)this["ConsumerKeySetting"]).Decrypt();
      }
      set { this["ConsumerKeySetting"] = value.Encrypt(); }
    }

    [UserScopedSetting()]
    public String ConsumerSecretSetting {
      get {
        return (((String)this["ConsumerSecretSetting"]) == null)
      ? null : ((String)this["ConsumerSecretSetting"]).Decrypt();
      }
      set { this["ConsumerSecretSetting"] = value.Encrypt(); }
    }

    [UserScopedSetting()]
    public String AccessTokenKeySetting {
      get {
        return (((String)this["AccessTokenKeySetting"]) == null)
      ? null : ((String)this["AccessTokenKeySetting"]).Decrypt();
      }
      set { this["AccessTokenKeySetting"] = value.Encrypt(); }
    }

    [UserScopedSetting()]
    public String AccessTokenSecretSetting {
      get {
        return (((String)this["AccessTokenSecretSetting"]) == null)
      ? null : ((String)this["AccessTokenSecretSetting"]).Decrypt();
      }
      set { this["AccessTokenSecretSetting"] = value.Encrypt(); }
    }
  }
}
