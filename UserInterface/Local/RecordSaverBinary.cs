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
    private bool _overwrite = false;
    internal RecordSaverBinary(TweetRecords records, string filePath) {
      _records = records;
      _filePath = filePath;
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
        TweetRecords readResult = null;
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        if (stream.Length != 0) readResult = (TweetRecords)binaryFormatter.Deserialize(stream);
        return readResult;
      }
    }

    internal void UpdateBinary<TweetRecords>(TweetRecords objectToWrite)
    {
      using (Stream stream = File.Open(_filePath, FileMode.Create))
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
