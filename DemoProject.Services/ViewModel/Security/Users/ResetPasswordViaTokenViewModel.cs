using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Security.Users
{
    //
    //  Summary:
    //          It is not part of the domain model. It is just a convenient class for passing data between the controller
    //          and the view. To emphasize this, I put this class in the PathPranali.WebUI project to keep it separate from 
    //          the domain model classes.
    public class ResetPasswordViaTokenViewModel
    {
        public ResetPasswordViaTokenViewModel()
        {
            MobileOTP = "0";
            EmailVCode = "0";
            UserPassword = "1";
            ConfirmPassword = "1";
        }

        [MaxLength(20, ErrorMessage = "We Found Throttle Requests From You, If You Do Same One More Time, Your Device Will Be Blocked")]
        public string MobileOTP { get; set; }

        [MaxLength(20, ErrorMessage = "We Found Throttle Requests From You, If You Do Same One More Time, Your Device Will Be Blocked")]
        public string EmailVCode { get; set; }

        [MaxLength(50)]
        public string UserPassword { get; set; }

        [MaxLength(50)]
        [Compare("UserPassword", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}
