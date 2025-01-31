using System;

namespace DemoProject.WebUI.Infrastructure.CustomException
{
    public class UserAlreadyLoggedInException : ApplicationException 
    {
        public override string Message
        {
            get
            {
                return "User Already Logged In.";
            }
        }
    }
}