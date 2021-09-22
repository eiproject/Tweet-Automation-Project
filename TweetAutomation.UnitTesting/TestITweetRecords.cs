using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TweetAutomation.UserInterface.Model;

namespace UnitTesting
{
  class TestITweetRecords
  {
    ITweetRecords _tweetRecords;
    [SetUp]
    public void Setup()
    {
      _tweetRecords = new TweetRecords();
    }

    [Test]
    [TestCaseSource("TestCaseSourceData")]
    public void DoAddRecordToRecords_ShouldBePass(
      int id, string tweet, DateTime date, DateTime time)
    {
      TweetRecord record = new TweetRecord(
        id, tweet, date, time);
      _tweetRecords.Add(record);
      Assert.AreEqual(1, _tweetRecords.Records.Count);
    }

    [Test]
    [TestCaseSource("TestCaseSourceData")]
    public void DoDeleteRecordToRecords_ShouldBePass(
      int id, string tweet, DateTime date, DateTime time)
    {
      TweetRecord record = new TweetRecord(
        id, tweet, date, time);
      _tweetRecords.Add(record);
      _tweetRecords.Delete(id);
      Assert.AreEqual(0, _tweetRecords.Records.Count);
    }

    private static IEnumerable<TestCaseData> TestCaseSourceData()
    {
      yield return new TestCaseData(0, "Test tweet", new DateTime(2021, 09, 20), new DateTime(2021, 09, 20, 23, 59, 00));
      yield return new TestCaseData(14354, null, new DateTime(2021, 09, 20), new DateTime(2021, 09, 20, 23, 59, 00));
      yield return new TestCaseData(234, null, null, null);
    }
  }
}
