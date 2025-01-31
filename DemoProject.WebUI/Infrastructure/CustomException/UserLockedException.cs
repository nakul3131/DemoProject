using System;

namespace DemoProject.WebUI.Infrastructure.CustomException
{
    public class UserLockedException : ApplicationException 
    {
        public override string Message
        {
            get
            {
                return "Your Account Has been Locked due to Maximum Failed Login Attempt.";
            }
        }
    }
}