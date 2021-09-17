using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UserInterface.Model;

namespace UserInterface.Local
{
  internal class RecordSaverBinary
  {
    private TweetRecords _records;
    private string _filePath;
    private bool _overwrite = true;
    internal RecordSaverBinary(TweetRecords records, string filePath) {
      _records = records;
      _filePath = path;
    }
    internal void CreateFileIfNotExist()
    {
      if (!File.Exists(_filePath) || _overwrite)
        using (File.Create(_filePath))
        {

        }
    }

    internal TweetRecords Read<TweetRecord>()
    {
      using (Stream stream = File.Open(_filePath, FileMode.Open))
      {
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        return (TweetRecords)binaryFormatter.Deserialize(stream);
      }
    }

    internal void UpdateBinary<TweetRecords>(TweetRecords objectToWrite)
    {
      using (Stream stream = File.Open(_filePath, FileMode.Append))
      {
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        binaryFormatter.Serialize(stream, objectToWrite);
      }
    }

    internal void Delete()
    {

    }
  }
}
