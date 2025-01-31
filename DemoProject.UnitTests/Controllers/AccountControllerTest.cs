using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Security;
using DemoProject.WebUI.Controllers;

namespace DemoProject.UnitTests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        //[TestMethod]
        //public void Can_Login_With_Valid_User_Credentials()
        //{
        //    // Arrange - create a mock authentication provider
        //    Mock<IAuthProviderRepository> mock = new Mock<IAuthProviderRepository>();
        //    mock.Setup(m => m.IsAuthenticate("Administrator", "1")).Returns(true);

        //    var mockControllerContext = new Mock<ControllerContext>();
        //    mockControllerContext.Setup(context => context.HttpContext.Session["UserProfilePrmKey"]).Returns((short)4);

        //    // Arrange - create the view model
        //    LoginViewModel viewModel = new LoginViewModel()
        //    {
        //        UserName = "Administrator",
        //        Password = "1"
        //    };

        //    // Arrange - create the controller
        //    AccountController target = new AccountController(mock.Object)
        //    {
        //        ControllerContext = mockControllerContext.Object
        //    };

        //    // Act = authenticate using valid credentials
        //    ActionResult result = target.Login(viewModel, "/MyUrl");

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(RedirectResult));
        //    Assert.AreEqual("/MyUrl", ((RedirectResult)result).Url);
        //}

        //[TestMethod]
        //public void Cannot_Login_With_InValid_User_Credentials()
        //{
        //    // Arrange - create a mock authentication provider
        //    Mock<IAuthProviderRepository> mock = new Mock<IAuthProviderRepository>();
        //    mock.Setup(m => m.IsAuthenticate("IncorrectUserName", "IncorrectPassword")).Returns(false);

        //    // Arrange - create the view model
        //    LoginViewModel viewModel = new LoginViewModel()
        //    {
        //        UserName = "IncorrectUserName",
        //        Password = "IncorrectPassword"
        //    };

        //    // Arrange - create the controller
        //    AccountController target = new AccountController(mock.Object);

        //    // Act = authenticate using valid credentials
        //    ActionResult result = target.Login(viewModel, "/MyUrl");

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //    Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        //}

        [TestMethod]
        public void Can_MFA_Enabled_User_Redirected_To_MFA_Page()
        {

        }

        [TestMethod]
        public void Cannot_MFA_Disabled_User_Redirected_To_MFA_Page()
        {

        }

        [TestMethod]
        public void Can_Locked_User_Redirected_To_User_Locked_Page()
        {

        }

        //[TestMethod]
        //public void CanLoginWithValidTokens()
        //{
        //    // Arrange - create a mock TokenAuthenticate provider
        //    Mock<IAuthProviderRepository> mock = new Mock<IAuthProviderRepository>();
        //    mock.Setup(m => m.IsTokenAuthenticate(9, "2222", "3333", 1)).Returns(true);

        //    var mockControllerContext = new Mock<ControllerContext>();
        //    mockControllerContext.Setup(context => context.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //    // Arrange - create the MFAViewModel
        //    MFAViewModel mfaviewModel = new MFAViewModel()
        //    {
        //        MobileOTP = "2222",
        //        EmailVCode = "3333",
        //    };

        //    // Arrange - create the controller
        //    AccountController target = new AccountController(mock.Object)
        //    {
        //        ControllerContext = mockControllerContext.Object
        //    };

        //    // Act = TokenAuthenticate using valid credentials
        //    ActionResult result = target.TokenBasedMFA(mfaviewModel, "/MyUrl");

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(RedirectResult));
        //    Assert.AreEqual("/MyUrl", ((RedirectResult)result).Url);
        //}

        //[TestMethod]
        //public void CanNotLoginWithInValidTokens()
        //{
        //    // Arrange - create a mock TokenAuthenticate provider
        //    Mock<IAuthProviderRepository> mock = new Mock<IAuthProviderRepository>();
        //    mock.Setup(m => m.IsTokenAuthenticate(9, "0000", "0000", 1)).Returns(false);

        //    var mockControllerContext = new Mock<ControllerContext>();
        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //    // Arrange - create the MFAViewModel
        //    MFAViewModel mfaviewModel = new MFAViewModel()
        //    {
        //        MobileOTP = "0000",
        //        EmailVCode = "0000",
        //    };

        //    // Arrange - create the controller
        //    AccountController target = new AccountController(mock.Object)
        //    {
        //        ControllerContext = mockControllerContext.Object
        //    };

        //    // Act = TokenAuthenticate using valid credentials
        //    ActionResult result = target.TokenBasedMFA(mfaviewModel, "/MyUrl");

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //    Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        //}
    }
}
