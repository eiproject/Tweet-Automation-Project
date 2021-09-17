using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UserInterface.Model;

namespace UserInterface.Local
{
  class CredentialSaverBinary
  {
    private string _filePath;
    private bool _overwrite = false;
    internal CredentialSaverBinary(string filePath)
    {
      _filePath = filePath;
    }

    internal void CreateFileIfNotExist()
    {
      if (!File.Exists(_filePath) || _overwrite)
        using (File.Create(_filePath))
        {

        }
    }

    internal Credentials Read<TweetRecord>()
    {
      using (Stream stream = File.Open(_filePath, FileMode.Open))
      {
        Credentials readResult = null;
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        if (stream.Length != 0) readResult = (Credentials)binaryFormatter.Deserialize(stream);
        return readResult;
      }
    }

    internal void UpdateBinary<Credentials>(Credentials objectToWrite)
    {
      using (Stream stream = File.Open(_filePath, FileMode.Create))
      {
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        binaryFormatter.Serialize(stream, objectToWrite);
      }
    }

    internal void Delete()
    {
      if (File.Exists(_filePath))
        File.Delete(_filePath);
    }
  }
}
