using TweetAutomation.UserInterface.Database;

namespace TweetAutomation.UserInterface.Model
{
  public interface ITweetsRepository
  {
    void Append(Tweet record);
    /*void Migrate(Tweets db);*/
    void Delete(int id);
  }
}
