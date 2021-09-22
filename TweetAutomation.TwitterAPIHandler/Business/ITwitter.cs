using System.Net;
using System.Threading.Tasks;

namespace TweetAutomation.TwitterAPIHandler.Business
{
  public interface ITwitter
  {
    Task<HttpStatusCode> Tweet(string text);
  }
}
