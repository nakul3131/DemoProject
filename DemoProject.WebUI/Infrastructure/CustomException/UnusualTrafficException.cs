using System;

namespace DemoProject.WebUI.Infrastructure.CustomException
{
    public class UnusualTrafficException : ApplicationException 
    {
        public override string Message
        {
            get
            {
                return "Unusual Traffic From Your Computer Network";
            }
        }
    }
}