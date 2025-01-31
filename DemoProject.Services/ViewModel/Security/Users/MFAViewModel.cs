using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Security.Users
{
    //
    //  Summary:
    //          It is not part of the domain model. It is just a convenient class for passing data between the controller
    //          and the view. To emphasize this, I put this class in the PathPranali.WebUI project to keep it separate from 
    //          the domain model classes.
    public class MFAViewModel
    {
        [Required(ErrorMessage = "Please Enter Mobile OTP")]
        [MaxLength(20, ErrorMessage = "We Found Throttle Requests From You, If You Do Same One More Time, Your Device Will Be Blocked")]
        public string MobileOTP { get; set; }

        [Required(ErrorMessage = "Please Enter Email Verification Code")]
        [MaxLength(20, ErrorMessage = "We Found Throttle Requests From You, If You Do Same One More Time, Your Device Will Be Blocked")]
        public string EmailVCode { get; set; }

        public MFAViewModel()
        {
            MobileOTP = "0";
            EmailVCode = "0";
        }
    }
}
