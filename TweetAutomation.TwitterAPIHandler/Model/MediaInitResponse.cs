using System;
using System.Collections.Generic;
using System.Text;

namespace TweetAutomation.TwitterAPIHandler.Model
{
  [Serializable]
  public class MediaInitResponse
  {
    public long media_id { get; set; }
    public string media_id_string { get; set; }
    public long expires_after_secs { get; set; }
  }
}
