using System.Web.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using DevTrends.MvcDonutCaching;
using System.Runtime.Caching;
using System.Web.UI;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.Security.Master;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.ViewModel.Configuration;
using System.Reflection;

namespace DemoProject.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserProfileMenuRepository userProfileMenu;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IAuthProviderRepository authProviderRepository;
        private readonly IUserProfilePasswordRepository userProfilePasswordRepository;
        private readonly IPasswordPolicyRepository passwordPolicy;

        private short userProfilePrmKey;

        public HomeController(IAuthProviderRepository _authProviderRepository, IConfigurationDetailRepository _configurationDetailRepository, IUserProfileMenuRepository _userProfileMenu, IPasswordPolicyRepository _passwordPolicy, IUserProfilePasswordRepository _userProfilePasswordRepository)
        {
            authProviderRepository = _authProviderRepository;
            userProfileMenu = _userProfileMenu;
            configurationDetailRepository = _configurationDetailRepository;
            passwordPolicy = _passwordPolicy;
            userProfilePasswordRepository = _userProfilePasswordRepository;
        }

        // GET: Login
        //[OutputCache(Duration = 120, Location = OutputCacheLocation.Client)]
        [HttpGet]
        [Authorize]
        public ActionResult DashBoard()
        {
            var memoryCache = MemoryCache.Default;
            if (!memoryCache.Contains("section1"))
            {
                var expiration = DateTime.Now.AddMinutes(15);
                var sections = userProfileMenu.GetUserMenus((short)HttpContext.Session["UserProfilePrmKey"]);
                memoryCache.Add("section1", sections, expiration);
            }
            var list = memoryCache.Get("section1", null);
            HttpContext.Session["UserProfileMenu1"] = list;
            userProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
            Session["Userimage"] = authProviderRepository.IsAuthenticateImage(userProfilePrmKey);
            var data = passwordPolicy.GetPasswordPolicyValues(userProfilePrmKey);
            byte passwordExpiryAlertDays = data.PasswordExpiryAlertDays;
            DateTime? passwordExpiryDateTime = userProfilePasswordRepository.GetPasswordExpiryDate(userProfilePrmKey);
            DateTime passwordExpiryAlertDate = (passwordExpiryDateTime.Value.Date).AddDays((-passwordExpiryAlertDays));

            if (passwordExpiryDateTime != null)
            {
                if ((passwordExpiryDateTime.Value.Date - DateTime.Today).Days > 0 && (DateTime.Today >= passwordExpiryAlertDate && DateTime.Today <= passwordExpiryDateTime))
                {
                    TempData["pwdExpireDays"] = (passwordExpiryDateTime.Value.Date - DateTime.Today).Days;
                }
            }
            return View();
        }

        [Authorize]
        [ChildActionOnly]
        [OutputCache(Duration = 120, Location = OutputCacheLocation.Client)]
        public ActionResult MenuPartial()
        {

            return PartialView("_MenuPartial");
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetpublicIp(string ip)
        {
            TempData["Ip"] = ip;
            return Json(null);
        }

        [HttpGet]
        [Authorize]
        [OutputCache(Duration = 120, Location = OutputCacheLocation.Client)]
        public ActionResult Default()
        {
            return View("DashBoard");
        }

        public List<LanguageViewModel> GetLangData()
        {
            var language = new List<LanguageViewModel>()
       {

                new LanguageViewModel {Id=1,LanguageName="English", Code="en"},
                new LanguageViewModel {Id=2,LanguageName="Marathi", Code="म"}
        };
            return language;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetLangDatas()
        {
            try
            {
                var studentlist = (from N in GetLangData()
                                   select new LanguageViewModel { Id = N.Id, LanguageName = N.LanguageName, Code = N.Code }).ToList();
                return Json(studentlist, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetStudentDatas(int id)
        {
            try
            {

                var studentlist = (from N in GetLangData()
                                   where N.Id == id
                                   select new LanguageViewModel { Id = N.Id, LanguageName = N.LanguageName }).ToList();
                return Json(new { data = studentlist, JsonRequestBehavior.AllowGet });

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }

            return View();
        }

        [HttpPost]
        public JsonResult Menusearch(string _inputString)
        {
            var searchlist = configurationDetailRepository.GetSearchQueryList(_inputString);
            return Json(searchlist, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Menus()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Menus(Guid _searchQueryId)
        {
            var searchlist = configurationDetailRepository.GetMenuSearchQueryResultBySearchQueryId(_searchQueryId);
            return Json(new { data = searchlist, JsonRequestBehavior.AllowGet });
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult GetRoute(string _actionname, string _controllername)
        {
            string url = Url.Action(_actionname, _controllername);
            return Json(url, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetRouteMenu()
        {
            List<string> list = new List<string>();
            List<MenusViewModel> data = new List<MenusViewModel> {
            new MenusViewModel { MenuName="Transcation",NameOfController="TransactionMaster",ActionName="Create",Icon="fa fa-list-alt"},
            new MenusViewModel { MenuName="Transcation",NameOfController="TransactionMaster",ActionName="UnverifiedIndex",Icon="fa fa-list-alt"},
            new MenusViewModel { MenuName="State",NameOfController="State",ActionName="UnverifiedIndex",Icon="fa fa-list-alt"},
            };
            foreach (var dr in data)
            {
                string NameOfController = String.Concat(dr.NameOfController, "Controller");

                //// Get the controller type
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var controllerType = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => typeof(Controller).IsAssignableFrom(t) && t.Name == NameOfController).FirstOrDefault();

                if (controllerType != null)
                {
                    //Get the action method
                    var actionMethod = controllerType.GetMethods()
                                                      .FirstOrDefault(m => m.Name.Equals(dr.ActionName, StringComparison.OrdinalIgnoreCase));

                    var routePrefixAttribute = controllerType.GetCustomAttributes(typeof(RoutePrefixAttribute), true)
                                                         .FirstOrDefault() as RoutePrefixAttribute;
                    if (routePrefixAttribute != null && actionMethod != null)
                    {
                        // Get the Route attribute on the action method
                        var routeAttribute = actionMethod.GetCustomAttribute<RouteAttribute>();

                        if (routeAttribute != null)
                        {
                            var spiltstr = routePrefixAttribute.Prefix.Split('/');
                            var MenuName = spiltstr[spiltstr.Length - 1];
                            string anchorTag = $"<a  href='javascript: void(0)'" +
                                               $"onclick='window.location.href = \"/{routePrefixAttribute.Prefix}/{routeAttribute.Template}\";'>" +
                                               "<i class='" + dr.Icon + "'></i>" +
                                               "&nbsp;&nbsp;&nbsp;<b>" + MenuName + "/" + dr.ActionName + " </a>";
                            list.Add(anchorTag);
                            
                        }

                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }

        [AllowAnonymous]
        public ActionResult Exit()
        {
            TempData["TransactionStatus"] = StringLiteralValue.Exit;

            return RedirectToAction("Default", "Home");
        }
    }
}