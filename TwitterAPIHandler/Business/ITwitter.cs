using System.Net;
using System.Threading.Tasks;

namespace TwitterAPIHandler.Business
{
  public interface ITwitter
  {
    Task<HttpStatusCode> Tweet(string text);
  }
}
