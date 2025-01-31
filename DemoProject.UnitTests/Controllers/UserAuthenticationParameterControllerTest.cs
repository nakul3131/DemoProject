using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Parameter.Security;
using DemoProject.WebUI.Controllers;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.Security.Parameter;

namespace DemoProject.UnitTests.Controllers
{
    [TestClass]
    public class UserAuthenticationParameterControllerTest
    {
        [TestMethod]
        public async Task Can_Amend_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The UserAuthenticationParameter
                UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

                mock.Setup(p => p.GetRejectedEntry()).Returns(Task.FromResult(userAuthenticationParameterViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The UserAuthenticationParameter
                ActionResult actionResult = await target.Amend();

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetRejectedEntry());

                Assert.Fail("An exception was not thrown as expected.");
            }
            catch (Exception e)
            {
                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
                if (e.GetType() == typeof(AssertFailedException)) throw;

                // Assert - Check That The Exception Type And Message
                Assert.AreEqual(expectedException.GetType(), e.GetType());
                Assert.AreEqual(expectedException.Message, e.Message);
            }
        }

        [TestMethod]
        public async Task Can_Amend_Post_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The UserAuthenticationParameter
                UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 6, SuccessiveInvalidAttempts = 6, CumulativeInvalidAttempts = 66, IntervalOfResettingCumulativeInvalidAttempt = 6, InvalidAttemptLockingTimePeriod = 66, InvalidAttemptLockingTimePeriodIn = "S", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Create, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = "A", Remark = "None", NameOfUser = "User1" };

                // Arrange - Create The Mock Repository
                Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

                mock.Setup(p => p.Amend(userAuthenticationParameterViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The  UserAuthenticationParameter
                ActionResult actionResult = await target.Amend(userAuthenticationParameterViewModel, "Amend");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Amend(userAuthenticationParameterViewModel));

                Assert.Fail("An exception was not thrown as expected.");
            }
            catch (Exception e)
            {
                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
                if (e.GetType() == typeof(AssertFailedException)) throw;

                // Assert - Check That The Exception Type And Message
                Assert.AreEqual(expectedException.GetType(), e.GetType());
                Assert.AreEqual(expectedException.Message, e.Message);
            }
        }

        [TestMethod]
        public async Task Can_Amend_Valid_Entry()
        {
            // Arrange - Create The UserAuthenticationParameter
            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 6, SuccessiveInvalidAttempts = 6, CumulativeInvalidAttempts = 66, IntervalOfResettingCumulativeInvalidAttempt = 6, InvalidAttemptLockingTimePeriod = 66, InvalidAttemptLockingTimePeriodIn = "S", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Create, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = "A", Remark = "None", NameOfUser = "User1" };

            // Arrange - Create The Mock Repository
            Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

            mock.Setup(p => p.Amend(userAuthenticationParameterViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Amend The UserAuthenticationParameter
            ActionResult actionResult = await target.Amend(userAuthenticationParameterViewModel, "Amend");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Amend(userAuthenticationParameterViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Delete_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The UserAuthenticationParameter
                UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

                mock.Setup(p => p.GetRejectedEntry()).Returns(Task.FromResult(userAuthenticationParameterViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Delete The UserAuthenticationParameter
                ActionResult actionResult = await target.Amend();

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetRejectedEntry());

                Assert.Fail("An exception was not thrown as expected.");
            }
            catch (Exception e)
            {
                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
                if (e.GetType() == typeof(AssertFailedException)) throw;

                // Assert - Check That The Exception Type And Message
                Assert.AreEqual(expectedException.GetType(), e.GetType());
                Assert.AreEqual(expectedException.Message, e.Message);
            }
        }

        [TestMethod]
        public async Task Can_Delete_Post_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The UserAuthenticationParameter
                UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 6, SuccessiveInvalidAttempts = 6, CumulativeInvalidAttempts = 66, IntervalOfResettingCumulativeInvalidAttempt = 65, InvalidAttemptLockingTimePeriod = 61, InvalidAttemptLockingTimePeriodIn = "S", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Delete, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", NameOfUser = "User1" };

                // Arrange - Create The Mock Repository
                Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

                mock.Setup(p => p.Delete(userAuthenticationParameterViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Delete The UserAuthenticationParameter
                ActionResult actionResult = await target.Amend(userAuthenticationParameterViewModel, "Delete");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Delete(userAuthenticationParameterViewModel));

                Assert.Fail("An exception was not thrown as expected.");
            }
            catch (Exception e)
            {
                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
                if (e.GetType() == typeof(AssertFailedException)) throw;

                // Assert - Check That The Exception Type And Message
                Assert.AreEqual(expectedException.GetType(), e.GetType());
                Assert.AreEqual(expectedException.Message, e.Message);
            }
        }

        [TestMethod]
        public async Task Can_Delete_Valid_Entry()
        {
            // Arrange - Create The UserAuthenticationParameter
            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 6, SuccessiveInvalidAttempts = 6, CumulativeInvalidAttempts = 66, IntervalOfResettingCumulativeInvalidAttempt = 65, InvalidAttemptLockingTimePeriod = 61, InvalidAttemptLockingTimePeriodIn = "S", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Delete, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", NameOfUser = "User1" };

            // Arrange - Create The Mock Repository
            Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

            //mock.Setup(p => p.Delete(userAuthenticationParameterViewModel)).Returns(Task.FromResult(userAuthenticationParameterViewModel));
            mock.Setup(p => p.Delete(userAuthenticationParameterViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Delete The UserAuthenticationParameter
            ActionResult actionResult = await target.Amend(userAuthenticationParameterViewModel, "Delete");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Delete(userAuthenticationParameterViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Modify_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The UserAuthenticationParameter
                UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

                mock.Setup(p => p.GetActiveEntry()).Returns(Task.FromResult(userAuthenticationParameterViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Modify The UserAuthenticationParameter
                ActionResult actionResult = await target.Modify();

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetActiveEntry());

                Assert.Fail("An exception was not thrown as expected.");
            }
            catch (Exception e)
            {
                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
                if (e.GetType() == typeof(AssertFailedException)) throw;

                // Assert - Check That The Exception Type And Message
                Assert.AreEqual(expectedException.GetType(), e.GetType());
                Assert.AreEqual(expectedException.Message, e.Message);
            }
        }

        [TestMethod]
        public async Task Can_Modify_Post_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The UserAuthenticationParameter
                UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 11, SuccessiveInvalidAttempts = 1, CumulativeInvalidAttempts = 1, IntervalOfResettingCumulativeInvalidAttempt = 5, InvalidAttemptLockingTimePeriod = 1, InvalidAttemptLockingTimePeriodIn = "H", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Create, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

                mock.Setup(p => p.Save(userAuthenticationParameterViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Modify The  UserAuthenticationParameter
                ActionResult actionResult = await target.Modify(userAuthenticationParameterViewModel);

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Save(userAuthenticationParameterViewModel));

                Assert.Fail("An exception was not thrown as expected.");
            }
            catch (Exception e)
            {
                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
                if (e.GetType() == typeof(AssertFailedException)) throw;

                // Assert - Check That The Exception Type And Message
                Assert.AreEqual(expectedException.GetType(), e.GetType());
                Assert.AreEqual(expectedException.Message, e.Message);
            }
        }

        [TestMethod]
        public async Task Can_Modify_Valid_Entry()
        {
            // Arrange - Create The UserAuthenticationParameter
            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 11, SuccessiveInvalidAttempts = 1, CumulativeInvalidAttempts = 1, IntervalOfResettingCumulativeInvalidAttempt = 5, InvalidAttemptLockingTimePeriod = 1, InvalidAttemptLockingTimePeriodIn = "H", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Create, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

            mock.Setup(p => p.Save(userAuthenticationParameterViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Modify The UserAuthenticationParameter
            ActionResult actionResult = await target.Modify(userAuthenticationParameterViewModel);

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Save(userAuthenticationParameterViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Reject_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The UserAuthenticationParameter
                UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

                mock.Setup(p => p.GetUnVerifiedEntry()).Returns(Task.FromResult(userAuthenticationParameterViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Reject The UserAuthenticationParameter
                ActionResult actionResult = await target.Verify();

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetUnVerifiedEntry());

                Assert.Fail("An exception was not thrown as expected.");
            }
            catch (Exception e)
            {
                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
                if (e.GetType() == typeof(AssertFailedException)) throw;

                // Assert - Check That The Exception Type And Message
                Assert.AreEqual(expectedException.GetType(), e.GetType());
                Assert.AreEqual(expectedException.Message, e.Message);
            }
        }

        [TestMethod]
        public async Task Can_Reject_Post_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The UserAuthenticationParameter
                UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 6, SuccessiveInvalidAttempts = 6, CumulativeInvalidAttempts = 66, IntervalOfResettingCumulativeInvalidAttempt = 65, InvalidAttemptLockingTimePeriod = 61, InvalidAttemptLockingTimePeriodIn = "S", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Reject, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", NameOfUser = "User1" };

                // Arrange - Create The Mock Repository
                Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

                mock.Setup(p => p.Reject(userAuthenticationParameterViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Reject The UserAuthenticationParameter
                ActionResult actionResult = await target.Verify(userAuthenticationParameterViewModel, "Reject");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Reject(userAuthenticationParameterViewModel));

                Assert.Fail("An exception was not thrown as expected.");
            }
            catch (Exception e)
            {
                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
                if (e.GetType() == typeof(AssertFailedException)) throw;

                // Assert - Check That The Exception Type And Message
                Assert.AreEqual(expectedException.GetType(), e.GetType());
                Assert.AreEqual(expectedException.Message, e.Message);
            }
        }

        [TestMethod]
        public async Task Can_Reject_Valid_Entry()
        {
            // Arrange - Create The UserAuthenticationParameter
            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 6, SuccessiveInvalidAttempts = 6, CumulativeInvalidAttempts = 66, IntervalOfResettingCumulativeInvalidAttempt = 65, InvalidAttemptLockingTimePeriod = 61, InvalidAttemptLockingTimePeriodIn = "S", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Reject, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", NameOfUser = "User1" };

            // Arrange - Create The Mock Repository
            Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

            mock.Setup(p => p.Reject(userAuthenticationParameterViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Reject The UserAuthenticationParameter
            ActionResult actionResult = await target.Verify(userAuthenticationParameterViewModel, "Reject");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Reject(userAuthenticationParameterViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Verify_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The UserAuthenticationParameter
                UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

                mock.Setup(p => p.GetUnVerifiedEntry()).Returns(Task.FromResult(userAuthenticationParameterViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The UserAuthenticationParameter
                ActionResult actionResult = await target.Verify();

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetUnVerifiedEntry());

                Assert.Fail("An exception was not thrown as expected.");
            }
            catch (Exception e)
            {
                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
                if (e.GetType() == typeof(AssertFailedException)) throw;

                // Assert - Check That The Exception Type And Message
                Assert.AreEqual(expectedException.GetType(), e.GetType());
                Assert.AreEqual(expectedException.Message, e.Message);
            }
        }

        [TestMethod]
        public async Task Can_Verify_Post_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The UserAuthenticationParameter
                UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 6, SuccessiveInvalidAttempts = 6, CumulativeInvalidAttempts = 61, IntervalOfResettingCumulativeInvalidAttempt = 65, InvalidAttemptLockingTimePeriod = 66, InvalidAttemptLockingTimePeriodIn = StringLiteralValue.Create, EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Verify, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "User1" };

                // Arrange - Create The Mock Repository
                Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

                mock.Setup(p => p.Verify(userAuthenticationParameterViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The UserAuthenticationParameter
                ActionResult actionResult = await target.Verify(userAuthenticationParameterViewModel, "Verify");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Verify(userAuthenticationParameterViewModel));

                Assert.Fail("An exception was not thrown as expected.");
            }
            catch (Exception e)
            {
                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
                if (e.GetType() == typeof(AssertFailedException)) throw;

                // Assert - Check That The Exception Type And Message
                Assert.AreEqual(expectedException.GetType(), e.GetType());
                Assert.AreEqual(expectedException.Message, e.Message);
            }
        }

        [TestMethod]
        public async Task Can_Verify_Valid_Entry()
        {
            // Arrange - Create The UserAuthenticationParameter
            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 6, SuccessiveInvalidAttempts = 6, CumulativeInvalidAttempts = 61, IntervalOfResettingCumulativeInvalidAttempt = 65, InvalidAttemptLockingTimePeriod = 66, InvalidAttemptLockingTimePeriodIn = StringLiteralValue.Create, EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Verify, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "User1" };

            // Arrange - Create The Mock Repository
            Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

            mock.Setup(p => p.Verify(userAuthenticationParameterViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Verify The UserAuthenticationParameter
            ActionResult actionResult = await target.Verify(userAuthenticationParameterViewModel, "Verify");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Verify(userAuthenticationParameterViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Amend_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

            // Arrange - Create The controller
            UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object);

            // Arrange - Create The UserAuthenticationParameter
            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 6, SuccessiveInvalidAttempts = 6, CumulativeInvalidAttempts = 61, IntervalOfResettingCumulativeInvalidAttempt = 65, InvalidAttemptLockingTimePeriod = 61, InvalidAttemptLockingTimePeriodIn = "S", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Create, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = "A", Remark = "None", NameOfUser = "User1" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Amend The UserAuthenticationParameter
            ActionResult actionResult = await target.Amend(userAuthenticationParameterViewModel, "Amend");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Amend(It.IsAny<UserAuthenticationParameterViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Delete_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

            // Arrange - Create The controller
            UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object);

            // Arrange - Create The UserAuthenticationParameter
            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 6, SuccessiveInvalidAttempts = 6, CumulativeInvalidAttempts = 61, IntervalOfResettingCumulativeInvalidAttempt = 65, InvalidAttemptLockingTimePeriod = 61, InvalidAttemptLockingTimePeriodIn = "S", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Delete, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", NameOfUser = "User1" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Amend The UserAuthenticationParameter
            ActionResult actionResult = await target.Amend(userAuthenticationParameterViewModel, "Delete");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Delete(It.IsAny<UserAuthenticationParameterViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Modify_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

            // Arrange - Create The controller
            UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object);

            // Arrange - Create The UserAuthenticationParameter
            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 11, SuccessiveInvalidAttempts = 1, CumulativeInvalidAttempts = 1, IntervalOfResettingCumulativeInvalidAttempt = 5, InvalidAttemptLockingTimePeriod = 1, InvalidAttemptLockingTimePeriodIn = "H", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Create, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Modify The UserAuthenticationParameter
            ActionResult actionResult = await target.Modify(userAuthenticationParameterViewModel);

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Save(It.IsAny<UserAuthenticationParameterViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Reject_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

            // Arrange - Create The controller
            UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object);

            // Arrange - Create The UserAuthenticationParameter
            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 6, SuccessiveInvalidAttempts = 6, CumulativeInvalidAttempts = 66, IntervalOfResettingCumulativeInvalidAttempt = 56, InvalidAttemptLockingTimePeriod = 66, InvalidAttemptLockingTimePeriodIn = "S", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Reject, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", NameOfUser = "User1" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Reject The UserAuthenticationParameter
            ActionResult actionResult = await target.Verify(userAuthenticationParameterViewModel, "Reject");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Reject(It.IsAny<UserAuthenticationParameterViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Verify_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

            // Arrange - Create The controller
            UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object);

            // Arrange - Create The UserAuthenticationParameter
            UserAuthenticationParameterViewModel UserAuthenticationParameterViewModel = new UserAuthenticationParameterViewModel { PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 6, SuccessiveInvalidAttempts = 6, CumulativeInvalidAttempts = 61, IntervalOfResettingCumulativeInvalidAttempt = 65, InvalidAttemptLockingTimePeriod = 61, InvalidAttemptLockingTimePeriodIn = "S", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Verify, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "User1" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Verify The UserAuthenticationParameter
            ActionResult actionResult = await target.Verify(UserAuthenticationParameterViewModel, "Verify");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Verify(It.IsAny<UserAuthenticationParameterViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Index_Contains_AllVerified_Entries()
        {
            // Arrange - Create The Mock Repository
            Mock<IUserAuthenticationParameterRepository> mock = new Mock<IUserAuthenticationParameterRepository>();

            var IndexOfVerifiedEntries = new UserAuthenticationParameterViewModel[]
                                                {
                                                   new UserAuthenticationParameterViewModel{ PrmKey = 1, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 11, SuccessiveInvalidAttempts = 1, CumulativeInvalidAttempts = 1, IntervalOfResettingCumulativeInvalidAttempt = 5, InvalidAttemptLockingTimePeriod = 1, InvalidAttemptLockingTimePeriodIn = "H", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Verify, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "Administrator" },
                                                   new UserAuthenticationParameterViewModel{ PrmKey = 2, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 11, SuccessiveInvalidAttempts = 1, CumulativeInvalidAttempts = 1, IntervalOfResettingCumulativeInvalidAttempt = 5, InvalidAttemptLockingTimePeriod = 1, InvalidAttemptLockingTimePeriodIn = "H", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Verify, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "User" },
                                                   new UserAuthenticationParameterViewModel{ PrmKey = 3, UserAuthenticationParameterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), TokenExpiredTime = Convert.ToDateTime("10:10 PM").TimeOfDay, NumOfResendToken = 11, SuccessiveInvalidAttempts = 1, CumulativeInvalidAttempts = 1, IntervalOfResettingCumulativeInvalidAttempt = 5, InvalidAttemptLockingTimePeriod = 1, InvalidAttemptLockingTimePeriodIn = "H", EnableDeviceAuthentication = true, EnableUserNameCaseSensetivity = false, EnablePasswordCaseSensetivity = false, EnableSmartForgotPassword = true, ModificationNumber = 1, EntryStatus = StringLiteralValue.Verify, EntryDateTime = new DateTime(01 / 01 / 2020), UserAuthenticationParameterPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "Super User" },
                                                }.ToList();

            mock.Setup(m => m.GetUserAuthenticationParameterIndex()).Returns(Task.FromResult<IEnumerable<UserAuthenticationParameterViewModel>>(IndexOfVerifiedEntries));

            // Arrange - create the controller
            UserAuthenticationParameterController target = new UserAuthenticationParameterController(mock.Object);

            // Action -target the controller
            var result = await target.Index() as ViewResult;

            // Assert 
            Assert.AreEqual(IndexOfVerifiedEntries.Count, 3);
            Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[0].EntryStatus);
            Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[1].EntryStatus);
            Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[2].EntryStatus);
        }

    }
}