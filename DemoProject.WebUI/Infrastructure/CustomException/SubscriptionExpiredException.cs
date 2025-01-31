using System;

namespace DemoProject.WebUI.Infrastructure.CustomException
{
    public class SubscriptionExpiredException : ApplicationException 
    {
        public override string Message
        {
            get
            {
                return "Your Subscription Has Expired";
            }
        }
    }
}