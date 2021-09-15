using System;
using System.Collections.Generic;
using System.Text;

namespace UserInterface.Model {
  class TweetRecord {
    public int No { get; set; }
    public string Tweet { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public string Status { get; set; }

    public TweetRecord(int no, string tweet, string date, string time, string status) {
      No = no;
      Tweet = tweet;
      Date = date;
      Time = time;
      Status = status;
    }
  }
}
