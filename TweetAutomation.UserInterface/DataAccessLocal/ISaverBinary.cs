namespace TweetAutomation.UserInterface.DataAccessLocal
{
  public interface ISaverBinary
  {
    void CreateFileIfNotExist();
    object Read<TweetRecord>();
    void UpdateBinary<T>(T objectToWrite);
    void DeleteBinaryFile();
  }
}
