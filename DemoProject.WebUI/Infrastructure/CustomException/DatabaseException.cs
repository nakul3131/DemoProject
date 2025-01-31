using System;

namespace DemoProject.WebUI.Infrastructure.CustomException
{
    public class DatabaseException : ApplicationException
    {
        public override string Message
        {
            get
            {
                return "Failed To Retrieve Data From Database. Data Base Error Detected";
            }
        }

    }
}