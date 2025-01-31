using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Security.Users
{
    //
    //  Summary:
    //          It is not part of the domain model. It is just a convenient class for passing data between the controller
    //          and the view. To emphasize this, I put this class in the PathPranali.WebUI project to keep it separate from 
    //          the domain model classes.
    public class LoginViewModel
        {
            public string DName { get; set; }
            [Required(ErrorMessage = "Please Enter Your User Name")]
            [MaxLength(50, ErrorMessage = "We Found Throttle Requests From You, If You Do Same One More Time, Your Device Will Be Blocked")]
            //        [CustomDisplayName("DashBoard", "Configuration")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Please Enter Your Password")]
            [MaxLength(50, ErrorMessage = "We Found Throttle Requests From You, If You Do Same One More Time, Your Device Will Be Blocked")]
            //        [Remote("IsUserHasLoginTrouble", "Account", AdditionalFields = "UserName", ErrorMessage = " ")]
            public string Password { get; set; }
        }
}
