using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TweetAutomation.LoggingSystem.Business;
using TweetAutomation.UserInterface.Database;

namespace TweetAutomation.UserInterface.DataAccessLocal
{
  public class RecordSaverBinary : ISaverBinary
  {
    private LogRepository _logger = LogRepository.LogInstance();
    private object _readLoker = new object();
    private object _updateLoker = new object();
    private string _filePath;
    private bool _overwrite = false;
    public RecordSaverBinary(string filePath)
    {
      _filePath = filePath;
    }

    public void CreateFileIfNotExist()
    {
      if (!File.Exists(_filePath) || _overwrite)
        using (File.Create(_filePath))
        {

        }
    }

    private void ForceCreateNewBinary()
    {
      using (File.Create(_filePath))
      {

      }
    }

    public object Read<T>()
    {
      lock (_readLoker)
      {
        Tweets db = null;
        try
        {
          using (Stream stream = File.Open(_filePath, FileMode.Open))
          {
            var binaryFormatter = new BinaryFormatter();
            if (stream.Length != 0) db = (Tweets)binaryFormatter.Deserialize(stream);
          }
        }
        catch (SerializationException error)
        {
          _logger.Update("ERROR", error.GetType().Name + " " + error.Message);
          ForceCreateNewBinary();
        }
        return db;
      }
    }

    public void UpdateBinary<T>(T db)
    {
      lock (_updateLoker)
      {
        using (Stream stream = File.Open(_filePath, FileMode.Create))
        {
          var binaryFormatter = new BinaryFormatter();
          try
          {
            binaryFormatter.Serialize(stream, db);
          }
          catch (SerializationException error)
          {
            _logger.Update("ERROR", error.GetType().Name + " " + error.Message);
          }
        }
      }
    }

    public void DeleteBinaryFile()
    {
      throw new NotImplementedException();
    }
  }
}
