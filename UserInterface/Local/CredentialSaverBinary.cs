using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TwitterAPIHandler.Model;
using UserInterface.Model;

namespace UserInterface.Local
{
  public class CredentialSaverBinary : ISaverBinary
  {
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
      using (Stream stream = File.Open(_filePath, FileMode.Open))
      {
        Credentials readResult = new Credentials();
        var binaryFormatter = new BinaryFormatter();
        if (stream.Length != 0) readResult = (Credentials)binaryFormatter.Deserialize(stream);
        return readResult;
      }
    }

    public void UpdateBinary<T>(T objectToWrite)
    {
      using (Stream stream = File.Open(_filePath, FileMode.Create))
      {
        var binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, objectToWrite);
      }
    }

    public void Delete()
    {
      if (File.Exists(_filePath))
        File.Delete(_filePath);
    }
  }
}
