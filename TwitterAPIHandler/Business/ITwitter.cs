using System.Net;
using System.Threading.Tasks;

namespace TwitterAPIHandler.Business
{
  public interface ITwitter
  {
    void SetCredential(
      string consumerKey, string consumerKeySecret,
      string accessToken, string accessTokenSecret);
    Task<HttpStatusCode> Tweet(string text);
  }
}
