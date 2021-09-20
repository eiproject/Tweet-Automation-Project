using System;
using System.Collections.Generic;
using System.Text;
using UserInterface.Model;

namespace UserInterface.Business
{
  public interface IStatusChecker
  {
    void CheckStatus(TweetRecord record);
    void ChangeToSuccess(TweetRecord record);
  }
}
