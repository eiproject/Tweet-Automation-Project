namespace TweetAutomation.LoggingSystem.BusinessLogic
{
  public interface ILogRepository
  {
    void Create();
    void Read();
    void Update(string status, string log);
    void Delete();
  }
}
