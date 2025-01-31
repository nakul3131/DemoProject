using System;

namespace DemoProject.WebUI.Infrastructure.CustomException
{
    public class AccessDeniedException : ApplicationException 
    {
        public override string Message
        {
            get
            {
                return "Access Denied !! Failed To Retrieve Data From Database. Record Not Accessible";
            }
        }
    }
}