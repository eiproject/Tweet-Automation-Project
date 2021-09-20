using NUnit.Framework;
using System;
using System.Collections.Generic;
using UserInterface.Business;
using UserInterface.Model;

namespace UnitTesting.Example
{
  public class TestIStatusChecker
  {
    IStatusChecker _checker;
    [SetUp]
    public void Setup()
    {
      _checker = new StatusChecker();
    }

    [Test]
    [TestCaseSource("TestCaseSourceData")]
    public void CheckStatus_ShouldBeOnQueue(
      int id, string tweet, DateTime date, DateTime time)
    {
      TweetRecord record = new TweetRecord(
        id, tweet, date, time);
      _checker.CheckStatus(record);
      Assert.AreEqual("On Queue", record.Status);
    }

    private static IEnumerable<TestCaseData> TestCaseSourceData()
    {
      yield return new TestCaseData(0, "Test tweet", new DateTime(2045, 09, 20), new DateTime(2022, 09, 20, 23, 59, 00));
      yield return new TestCaseData(14354, "Another Test tweet", new DateTime(2022, 10, 20), new DateTime(2022, 10, 20, 23, 59, 00));
    }

    [Test]
    [TestCaseSource("TestCaseSourceData2")]
    public void CheckStatus_ShouldBeTimeError(
      int id, string tweet, DateTime date, DateTime time)
    {
      TweetRecord record = new TweetRecord(
        id, tweet, date, time);
      _checker.CheckStatus(record);
      Assert.AreEqual("Time Error", record.Status);
    }

    private static IEnumerable<TestCaseData> TestCaseSourceData2()
    {
      yield return new TestCaseData(0, "Test tweet", new DateTime(2019, 09, 20), new DateTime(2022, 09, 20, 23, 59, 00));
      yield return new TestCaseData(14354, "Another Test tweet", new DateTime(1990, 10, 20), new DateTime(2022, 10, 20, 23, 59, 00));
    }

    [Test]
    [TestCaseSource("TestCaseSourceData3")]
    public void CheckStatus_ShouldBeTweetNull(
      int id, string tweet, DateTime date, DateTime time)
    {
      TweetRecord record = new TweetRecord(
        id, tweet, date, time);
      _checker.CheckStatus(record);
      Assert.AreEqual("Tweet Null", record.Status);
    }

    private static IEnumerable<TestCaseData> TestCaseSourceData3()
    {
      yield return new TestCaseData(0, null, new DateTime(2019, 09, 20), new DateTime(2022, 09, 20, 23, 59, 00));
      yield return new TestCaseData(14354, null, new DateTime(1990, 10, 20), new DateTime(2022, 10, 20, 23, 59, 00));
    }
  }
}