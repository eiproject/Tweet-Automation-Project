using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface.BusinessLogic
{
  public interface IStatusChecker
  {
    void CheckStatus(Tweet record);
    void CheckStatusOfSendImmediately(Tweet record);
    void ChangeStatusByResponse(Tweet record, HttpStatusCode response);
  }
}
