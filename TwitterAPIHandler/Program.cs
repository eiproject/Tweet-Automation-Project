using System;
using TwitterAPIHandler.Business;

namespace TwitterAPIHandler {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("Twitter API Handler Project!");
      Twitter twt = new Twitter();
      twt.PostTweet();
      Console.ReadLine();
    }
  }
}
