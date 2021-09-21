﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UserInterface.Model;

namespace UserInterface.Business
{
  public interface IStatusChecker
  {
    void CheckStatus(TweetRecord record);
    void CheckStatusOfSendImmediately(TweetRecord record);
    void ChangeStatusByResponse(TweetRecord record, HttpStatusCode response);
  }
}
