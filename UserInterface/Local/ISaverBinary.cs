namespace UserInterface.Local
{
  public interface ISaverBinary
  {
    void CreateFileIfNotExist();
    object Read<TweetRecord>();
    void UpdateBinary<T>(T objectToWrite);
    void Delete();
  }
}
