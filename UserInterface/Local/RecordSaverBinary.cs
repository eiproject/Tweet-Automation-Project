using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UserInterface.Model;

namespace UserInterface.Local
{
  public class RecordSaverBinary : ISaverBinary
  {
    private string _filePath;
    private bool _overwrite = false;
    public RecordSaverBinary(string filePath) {
      _filePath = filePath;
    }

    public void CreateFileIfNotExist()
    {
      if (!File.Exists(_filePath) || _overwrite)
        using (File.Create(_filePath))
        {

        }
    }

    public object Read<TweetRecord>()
    {
      using (Stream stream = File.Open(_filePath, FileMode.Open))
      {
        TweetRecords readResult = null;
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        if (stream.Length != 0) readResult = (TweetRecords)binaryFormatter.Deserialize(stream);
        return readResult;
      }
    }

    public void UpdateBinary<TweetRecords>(TweetRecords objectToWrite)
    {
      using (Stream stream = File.Open(_filePath, FileMode.Create))
      {
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        binaryFormatter.Serialize(stream, objectToWrite);
      }
    }

    public void Delete()
    {

    }
  }
}
