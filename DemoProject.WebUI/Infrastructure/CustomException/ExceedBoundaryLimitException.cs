using System;

namespace DemoProject.WebUI.Infrastructure.CustomException
{
    public class ExceedBoundaryLimitException : ApplicationException
    {
        public override string Message
        {
            get
            {
                return "Branch Boundary Limit Exceed";
            }
        }
    }
}