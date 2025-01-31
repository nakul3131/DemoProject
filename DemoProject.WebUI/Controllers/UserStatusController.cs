using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.ViewModel.Security.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/Enterprise/UserStatus")]
    public class UserStatusController : Controller
    {
        private readonly IUserStatusRepository userStatusRepository;
        private readonly IAuthProviderRepository authProvider;

        public UserStatusController(IUserStatusRepository _userStatusRepository, IAuthProviderRepository _authProvider)
        {
            userStatusRepository = _userStatusRepository;
            authProvider = _authProvider;
        }

        // GET: UserStatus
        [HttpGet]
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Index")]
        public async Task<ActionResult> Index(UserStatusViewModel userStatusViewModel)
        {
            IEnumerable<UserStatusViewModel> userStatusIndex = await userStatusRepository.GetUserStatusIndex(userStatusViewModel.UserProfileStatus, userStatusViewModel.EffectiveDate);
            return View(userStatusIndex);
        }

        [AllowAnonymous]
        public ActionResult ClearUser(short userProfilePrmKey)
        {
            authProvider.ClearUserRecentSession(userProfilePrmKey, "Admin");
            return RedirectToAction("Index", "UserStatus");
        }
    }
}