namespace TweetAutomation.UserInterface.Model
{
  public interface ITweetsRepository
  {
    void Append(Tweet record);
    void Delete(int id);
  }
}
