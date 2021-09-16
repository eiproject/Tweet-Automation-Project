using System;
using System.Collections.Generic;
using System.Text;

namespace UserInterface.Model {
  class TweetRecordFactory {
    TweetRecords _records;
    internal TweetRecordFactory(TweetRecords records) {
      _records = records;
    }

    internal TweetRecord Create(
      string tweet, DateTime date, DateTime time) {

      return new TweetRecord(GetLastID(), tweet, date, time, CheckStatus(tweet, date, time));
    }

    private int GetLastID() {
      int id;
      if (_records.Records.Count == 0) {
        id = 0;
      }
      else {
        id = _records.Records[_records.Records.Count - 1].ID + 1;
      }
      return id;
    }

    private string CheckStatus(string tweet, DateTime date, DateTime time) {
      string status = "On Queue";
      int dateDifference = (date - DateTime.Now).Days;
      double timeDifference = (time - DateTime.Now).TotalMinutes;
      if (dateDifference < 0 || timeDifference < 0) status = "Time Error";
      if (tweet == null || tweet == "" || tweet == " ") status = "Tweet null";
      return status;
    }
  }
}
