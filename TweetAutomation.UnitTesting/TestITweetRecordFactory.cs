using NUnit.Framework;
using System;
using System.Collections.Generic;
using TweetAutomation.UserInterface.Database;
using TweetAutomation.UserInterface.Factory;
using TweetAutomation.UserInterface.Model;

namespace UnitTesting
{
  public class TestITweetRecordFactory
  {
    ITweetRecordFactory _factory;

    [SetUp]
    public void Setup()
    {
      _factory = new TweetRecordFactory(Tweets.GetInstance());
    }

    [Test]
    [TestCaseSource("TestCaseSourceData")]
    public void CreateRecord_ShouldBeCreated(string tweet, DateTime date, DateTime time, bool isImmediately, string imagePath)
    {
      Tweet record = _factory.Create(tweet, date, time, isImmediately, imagePath);
      Assert.AreEqual(record.ID, 0);
      Assert.AreEqual(record.FullText, tweet);
      Assert.AreEqual(record.DateObject, date);
      Assert.AreEqual(record.DateString, "09/20/2021");
      Assert.AreEqual(record.TimeObject, time);
      Assert.AreEqual(record.TimeString, "11:59 PM");
      Assert.AreEqual(record.DateTimeCombined, date.Date + time.TimeOfDay);
      Assert.AreEqual(record.Status, null);
    }

    private static IEnumerable<TestCaseData> TestCaseSourceData()
    {
      yield return new TestCaseData("Test tweet", new DateTime(2021, 09, 20), new DateTime(2021, 09, 20, 23, 59, 00));
      yield return new TestCaseData(null, new DateTime(2021, 09, 20), new DateTime(2021, 09, 20, 23, 59, 00));
    }
  }
}