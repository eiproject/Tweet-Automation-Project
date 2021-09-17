using System.Threading.Tasks;

namespace TwitterAPIHandler.Business
{
  public interface ITwitter
  {
    void SetCredential(
      string consumerKey, string consumerKeySecret,
      string accessToken, string accessTokenSecret);
    Task<string> Tweet(string text);
  }
}
