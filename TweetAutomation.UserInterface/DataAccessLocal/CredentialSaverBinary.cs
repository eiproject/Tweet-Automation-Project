using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TweetAutomation.LoggingSystem.BusinessLogic;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface.DataAccessLocal
{
  public class CredentialSaverBinary : ISaverBinary
  {
    private LogRepository _logger = LogRepository.LogInstance();
    private object _readLoker = new object();
    private object _updateLoker = new object();
    private string _filePath;
    private bool _overwrite = false;
    public CredentialSaverBinary(string filePath)
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

    public object Read<T>()
    {
      lock (_readLoker)
      {
        Credentials readResult = null;
        try
        {
          using (Stream stream = File.Open(_filePath, FileMode.Open))
          {
            var binaryFormatter = new BinaryFormatter();
            if (stream.Length != 0)
              readResult = (Credentials)binaryFormatter.Deserialize(stream);
          }
        }
        catch (SerializationException error)
        {
          _logger.Update("ERROR", error.GetType().Name + " " + error.Message);
        }
        catch (InvalidCastException error)
        {
          _logger.Update("ERROR", error.GetType().Name + " " + error.Message);
        }
        if (readResult == null) CreateNewBinary();
        return readResult;
      }
    }

    private void CreateNewBinary()
    {
      using (File.Create(_filePath))
      {

      }
    }

    public void UpdateBinary<T>(T objectToWrite)
    {
      lock (_updateLoker)
      {
        using (Stream stream = File.Open(_filePath, FileMode.Create))
        {
          var binaryFormatter = new BinaryFormatter();
          binaryFormatter.Serialize(stream, objectToWrite);
        }
      }
    }

    public void DeleteBinaryFile()
    {
      if (File.Exists(_filePath))
        File.Delete(_filePath);
    }
  }
}
