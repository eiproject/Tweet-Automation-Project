namespace TweetAutomation.UserInterface.Model
{
  public enum TweetStatus
  {
    None = -1,
    Error = 0,
    Success = 1,
    Credential_Error = 2,
    Request_Timeout = 3,
    Forbidden = 4,
    Bad_Request = 5,
    Time_Error = 6,
    Tweet_Null = 7,      
    Unknown = 8,
    Starting = 9,
    On_Queue = 10
  }
}
