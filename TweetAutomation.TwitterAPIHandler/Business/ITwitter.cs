using System.Net;
using System.Threading.Tasks;

namespace TweetAutomation.TwitterAPIHandler.Business
{
  public interface ITwitter
  {
    HttpStatusCode Tweet(string text);
    HttpStatusCode Tweet(string text, string filePath);
  }
}
