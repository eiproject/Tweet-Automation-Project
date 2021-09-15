using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UserInterface.Business {
  static class Encryption {
    internal static string Encrypt(
        this string clearText,
        string optionalEntropy = null,
        DataProtectionScope scope = DataProtectionScope.CurrentUser) {
      if (clearText == null)
        throw new ArgumentNullException("clearText");
      byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
      byte[] entropyBytes = string.IsNullOrEmpty(optionalEntropy)
          ? null
          : Encoding.UTF8.GetBytes(optionalEntropy);
      byte[] encryptedBytes = ProtectedData.Protect(clearBytes, entropyBytes, scope);
      return Convert.ToBase64String(encryptedBytes);
    }

    internal static string Decrypt(
        this string encryptedText,
        string optionalEntropy = null,
        DataProtectionScope scope = DataProtectionScope.CurrentUser) {
      if (encryptedText == null)
        throw new ArgumentNullException("encryptedText");
      byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
      byte[] entropyBytes = string.IsNullOrEmpty(optionalEntropy)
          ? null
          : Encoding.UTF8.GetBytes(optionalEntropy);
      byte[] clearBytes = ProtectedData.Unprotect(encryptedBytes, entropyBytes, scope);
      return Encoding.UTF8.GetString(clearBytes);
    }
  }
}
