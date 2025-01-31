//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using DemoProject.WebUI.Controllers;
//using DemoProject.WebUI.Infrastructure.CustomException;
//using DemoProject.Services.Constants;
//using DemoProject.Services.Abstract.Enterprise.Establishment;
//using DemoProject.Services.ViewModel.Enterprise.Establishment;

//namespace DemoProject.UnitTests.Controllers
//{
//    [TestClass]
//    public class AuthorizedSharesCapitalControllerTest
//    {
//        [TestMethod]
//        public async Task Can_Amend_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The AuthorizedSharesCapital
//                AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//                mock.Setup(p => p.GetRejectedEntry()).Returns(Task.FromResult(AuthorizedSharesCapitalViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Amend The AuthorizedSharesCapital
//                ActionResult actionResult = await target.Amend();

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetRejectedEntry());

//                Assert.Fail("An exception was not thrown as expected.");
//            }
//            catch (Exception e)
//            {
//                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//                if (e.GetType() == typeof(AssertFailedException)) throw;

//                // Assert - Check That The Exception Type And Message
//                Assert.AreEqual(expectedException.GetType(), e.GetType());
//                Assert.AreEqual(expectedException.Message, e.Message);
//            }
//        }

//        [TestMethod]
//        public async Task Can_Amend_Post_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The AuthorizedSharesCapital
//                AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel();

//                // Arrange - Create The Mock Repository
//                Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//                mock.Setup(p => p.Amend(AuthorizedSharesCapitalViewModel)).Returns(Task.FromResult(false));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Amend The AuthorizedSharesCapital
//                ActionResult actionResult = await target.Amend(AuthorizedSharesCapitalViewModel, "Amend");

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.Amend(AuthorizedSharesCapitalViewModel));

//                Assert.Fail("An exception was not thrown as expected.");
//            }
//            catch (Exception e)
//            {
//                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//                if (e.GetType() == typeof(AssertFailedException)) throw;

//                // Assert - Check That The Exception Type And Message
//                Assert.AreEqual(expectedException.GetType(), e.GetType());
//                Assert.AreEqual(expectedException.Message, e.Message);
//            }
//        }

//        [TestMethod]
//        public async Task Can_Amend_Valid_Entry()
//        {
//            // Arrange - Create The AuthorizedSharesCapital
//            AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel { PrmKey = 1, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 01 / 2019), AuthorizedSharesCapitalAmount = 1000000, ReferenceNumber = "Two", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//            mock.Setup(p => p.Amend(AuthorizedSharesCapitalViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//            // Arrange - Create The controller
//            AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            // Act - Try To Amend The AuthorizedSharesCapital
//            ActionResult actionResult = await target.Amend(AuthorizedSharesCapitalViewModel, "Amend");

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Amend(AuthorizedSharesCapitalViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Can_Delete_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The AuthorizedSharesCapital
//                AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//                mock.Setup(p => p.GetRejectedEntry()).Returns(Task.FromResult(AuthorizedSharesCapitalViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Delete The AuthorizedSharesCapital
//                ActionResult actionResult = await target.Amend();

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetRejectedEntry());

//                Assert.Fail("An exception was not thrown as expected.");
//            }
//            catch (Exception e)
//            {
//                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//                if (e.GetType() == typeof(AssertFailedException)) throw;

//                // Assert - Check That The Exception Type And Message
//                Assert.AreEqual(expectedException.GetType(), e.GetType());
//                Assert.AreEqual(expectedException.Message, e.Message);
//            }
//        }

//        [TestMethod]
//        public async Task Can_Delete_Post_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The AuthorizedSharesCapital
//                AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel { PrmKey = 1, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 01 / 2019), AuthorizedSharesCapitalAmount = 1000000, ReferenceNumber = "Two", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Delete, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Administrator" };

//                // Arrange - Create The Mock Repository
//                Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//                mock.Setup(p => p.Delete(AuthorizedSharesCapitalViewModel)).Returns(Task.FromResult(false));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Delete The AuthorizedSharesCapital
//                ActionResult actionResult = await target.Amend(AuthorizedSharesCapitalViewModel, "Delete");

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.Delete(AuthorizedSharesCapitalViewModel));

//                Assert.Fail("An exception was not thrown as expected.");
//            }
//            catch (Exception e)
//            {
//                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//                if (e.GetType() == typeof(AssertFailedException)) throw;

//                // Assert - Check That The Exception Type And Message
//                Assert.AreEqual(expectedException.GetType(), e.GetType());
//                Assert.AreEqual(expectedException.Message, e.Message);
//            }
//        }

//        [TestMethod]
//        public async Task Can_Delete_Valid_Entry()
//        {
//            // Arrange - Create The AuthorizedSharesCapital
//            AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel { PrmKey = 1, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 01 / 2019), AuthorizedSharesCapitalAmount = 1000000, ReferenceNumber = "Two", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Delete, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//            mock.Setup(p => p.Delete(AuthorizedSharesCapitalViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//            // Arrange - Create The controller
//            AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            var mockUrlHelper = new Mock<UrlHelper>();
//            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
//            target.Url = mockUrlHelper.Object;

//            // Act - Try To Delete The AuthorizedSharesCapital
//            ActionResult actionResult = await target.Amend(AuthorizedSharesCapitalViewModel, "Delete");

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Delete(AuthorizedSharesCapitalViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Can_Modify_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The AuthorizedSharesCapital
//                AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//                mock.Setup(p => p.GetActiveEntry()).Returns(Task.FromResult(AuthorizedSharesCapitalViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Modify The AuthorizedSharesCapital
//                ActionResult actionResult = await target.Modify();

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetActiveEntry());

//                Assert.Fail("An exception was not thrown as expected.");
//            }
//            catch (Exception e)
//            {
//                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//                if (e.GetType() == typeof(AssertFailedException)) throw;

//                // Assert - Check That The Exception Type And Message
//                Assert.AreEqual(expectedException.GetType(), e.GetType());
//                Assert.AreEqual(expectedException.Message, e.Message);
//            }
//        }

//        [TestMethod]
//        public async Task Can_Modify_Post_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The AuthorizedSharesCapital
//                AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel();

//                // Arrange - Create The Mock Repository
//                Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//                mock.Setup(p => p.Save(AuthorizedSharesCapitalViewModel)).Returns(Task.FromResult(false));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Modify The AuthorizedSharesCapital
//                ActionResult actionResult = await target.Modify(AuthorizedSharesCapitalViewModel);

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.Save(AuthorizedSharesCapitalViewModel));

//                Assert.Fail("An exception was not thrown as expected.");
//            }
//            catch (Exception e)
//            {
//                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//                if (e.GetType() == typeof(AssertFailedException)) throw;

//                // Assert - Check That The Exception Type And Message
//                Assert.AreEqual(expectedException.GetType(), e.GetType());
//                Assert.AreEqual(expectedException.Message, e.Message);
//            }
//        }

//        [TestMethod]
//        public async Task Can_Modify_Valid_Entry()
//        {
//            // Arrange - Create The AuthorizedSharesCapital
//            AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel { PrmKey = 1, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 01 / 2019), AuthorizedSharesCapitalAmount = 1000000, ReferenceNumber = "Two", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//            mock.Setup(p => p.Save(AuthorizedSharesCapitalViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

//            // Arrange - Create The controller
//            AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            // Act - Try To Modify The AuthorizedSharesCapital
//            ActionResult actionResult = await target.Modify(AuthorizedSharesCapitalViewModel);

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Save(AuthorizedSharesCapitalViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Can_Reject_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The AuthorizedSharesCapital
//                AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//                mock.Setup(p => p.GetUnAuthorizedEntry()).Returns(Task.FromResult(AuthorizedSharesCapitalViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Reject The AuthorizedSharesCapital
//                ActionResult actionResult = await target.Verify();

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetUnAuthorizedEntry());

//                Assert.Fail("An exception was not thrown as expected.");
//            }
//            catch (Exception e)
//            {
//                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//                if (e.GetType() == typeof(AssertFailedException)) throw;

//                // Assert - Check That The Exception Type And Message
//                Assert.AreEqual(expectedException.GetType(), e.GetType());
//                Assert.AreEqual(expectedException.Message, e.Message);
//            }
//        }

//        [TestMethod]
//        public async Task Can_Reject_Post_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The AuthorizedSharesCapital
//                AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel();

//                // Arrange - Create The Mock Repository
//                Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//                mock.Setup(p => p.Reject(AuthorizedSharesCapitalViewModel)).Returns(Task.FromResult(false));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Reject The AuthorizedSharesCapital
//                ActionResult actionResult = await target.Verify(AuthorizedSharesCapitalViewModel, "Reject");

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.Reject(AuthorizedSharesCapitalViewModel));

//                Assert.Fail("An exception was not thrown as expected.");
//            }
//            catch (Exception e)
//            {
//                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//                if (e.GetType() == typeof(AssertFailedException)) throw;

//                // Assert - Check That The Exception Type And Message
//                Assert.AreEqual(expectedException.GetType(), e.GetType());
//                Assert.AreEqual(expectedException.Message, e.Message);
//            }
//        }

//        [TestMethod]
//        public async Task Can_Reject_Valid_Entry()
//        {
//            // Arrange - Create The AuthorizedSharesCapital
//            AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel { PrmKey = 1, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 01 / 2019), AuthorizedSharesCapitalAmount = 1000000, ReferenceNumber = "Two", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//            mock.Setup(p => p.Reject(AuthorizedSharesCapitalViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//            // Arrange - Create The controller
//            AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            var mockUrlHelper = new Mock<UrlHelper>();
//            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
//            target.Url = mockUrlHelper.Object;

//            // Act - Try To Reject The AuthorizedSharesCapital
//            ActionResult actionResult = await target.Verify(AuthorizedSharesCapitalViewModel, "Reject") as JsonResult;

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Reject(AuthorizedSharesCapitalViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Can_Verify_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The AuthorizedSharesCapital
//                AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//                mock.Setup(p => p.GetUnAuthorizedEntry()).Returns(Task.FromResult(AuthorizedSharesCapitalViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Verify The AuthorizedSharesCapital
//                ActionResult actionResult = await target.Verify();

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetUnAuthorizedEntry());

//                Assert.Fail("An exception was not thrown as expected.");
//            }
//            catch (Exception e)
//            {
//                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//                if (e.GetType() == typeof(AssertFailedException)) throw;

//                // Assert - Check That The Exception Type And Message
//                Assert.AreEqual(expectedException.GetType(), e.GetType());
//                Assert.AreEqual(expectedException.Message, e.Message);
//            }
//        }

//        [TestMethod]
//        public async Task Can_Verify_Post_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The AuthorizedSharesCapital
//                AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel();

//                // Arrange - Create The Mock Repository
//                Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//                mock.Setup(p => p.Verify(AuthorizedSharesCapitalViewModel)).Returns(Task.FromResult(false));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Verify The AuthorizedSharesCapital
//                ActionResult actionResult = await target.Verify(AuthorizedSharesCapitalViewModel, "Verify");

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.Verify(AuthorizedSharesCapitalViewModel));

//                Assert.Fail("An exception was not thrown as expected.");
//            }
//            catch (Exception e)
//            {
//                // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//                if (e.GetType() == typeof(AssertFailedException)) throw;

//                // Assert - Check That The Exception Type And Message
//                Assert.AreEqual(expectedException.GetType(), e.GetType());
//                Assert.AreEqual(expectedException.Message, e.Message);
//            }
//        }

//        [TestMethod]
//        public async Task Can_Verify_Valid_Entry()
//        {
//            // Arrange - Create The AuthorizedSharesCapital
//            AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel { PrmKey = 1, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 01 / 2019), AuthorizedSharesCapitalAmount = 1000000, ReferenceNumber = "Two", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//            mock.Setup(p => p.Verify(AuthorizedSharesCapitalViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//            // Arrange - Create The controller
//            AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            var mockUrlHelper = new Mock<UrlHelper>();
//            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
//            target.Url = mockUrlHelper.Object;

//            // Act - Try To Verify The AuthorizedSharesCapital
//            ActionResult actionResult = await target.Verify(AuthorizedSharesCapitalViewModel, "Verify") as JsonResult;

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Verify(AuthorizedSharesCapitalViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Amend_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//            // Arrange - Create The controller
//            AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object);

//            // Arrange - Create The AuthorizedSharesCapital
//            AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel { PrmKey = 1, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 01 / 2019), AuthorizedSharesCapitalAmount = 1000000, ReferenceNumber = "Two", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Amend The AuthorizedSharesCapital
//            ActionResult actionResult = await target.Amend(AuthorizedSharesCapitalViewModel, "Amend");

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Amend(It.IsAny<AuthorizedSharesCapitalViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Delete_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//            // Arrange - Create The controller
//            AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object);

//            // Arrange - Create The AuthorizedSharesCapital
//            AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel { PrmKey = 1, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 01 / 2019), AuthorizedSharesCapitalAmount = 1000000, ReferenceNumber = "Two", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Delete, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Delete The AuthorizedSharesCapital
//            ActionResult actionResult = await target.Amend(AuthorizedSharesCapitalViewModel, "Delete");

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Delete(It.IsAny<AuthorizedSharesCapitalViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Modify_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//            // Arrange - Create The controller
//            AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object);

//            // Arrange - Create The AuthorizedSharesCapital
//            AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel { PrmKey = 1, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 01 / 2019), AuthorizedSharesCapitalAmount = 1000000, ReferenceNumber = "Two", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Modify The AuthorizedSharesCapital
//            ActionResult actionResult = await target.Modify(AuthorizedSharesCapitalViewModel);

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Save(It.IsAny<AuthorizedSharesCapitalViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Reject_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//            // Arrange - Create The controller
//            AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object);

//            // Arrange - Create The AuthorizedSharesCapital
//            AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel { PrmKey = 1, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 01 / 2019), AuthorizedSharesCapitalAmount = 1000000, ReferenceNumber = "Two", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Reject The AuthorizedSharesCapital
//            ActionResult actionResult = await target.Verify(AuthorizedSharesCapitalViewModel, "Reject");

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Reject(It.IsAny<AuthorizedSharesCapitalViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Verify_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//            // Arrange - Create The controller
//            AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object);

//            // Arrange - Create The AuthorizedSharesCapital
//            AuthorizedSharesCapitalViewModel AuthorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel { PrmKey = 1, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 01 / 2019), AuthorizedSharesCapitalAmount = 1000000, ReferenceNumber = "Two", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Verify The AuthorizedSharesCapital
//            ActionResult actionResult = await target.Verify(AuthorizedSharesCapitalViewModel, "Verify");

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Verify(It.IsAny<AuthorizedSharesCapitalViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        //[TestMethod]
//        //public async Task VerifiedIndex_Contains_AllVerified_Entries()
//        //{
//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAuthorizedSharesCapitalRepository> mock = new Mock<IAuthorizedSharesCapitalRepository>();

//        //    var IndexOfAuthorizedEntries = new AuthorizedSharesCapitalViewModel[]
//        //                                        {
//        //                                           new AuthorizedSharesCapitalViewModel { PrmKey = 1, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 01 / 2019), AuthorizedSharesCapitalAmount = 1000000, ReferenceNumber = "Two", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = "A", Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Administrator" },
//        //                                           new AuthorizedSharesCapitalViewModel { PrmKey = 2, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 02 / 2019), AuthorizedSharesCapitalAmount = 2000000, ReferenceNumber = "Three", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = "A", Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "User" },
//        //                                           new AuthorizedSharesCapitalViewModel{ PrmKey = 3, AuthorizedSharesCapitalId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, EffectiveDate = new DateTime(01 / 01 / 2020), AuthorizedDate = new DateTime(01 / 03 / 2019), AuthorizedSharesCapitalAmount = 3000000, ReferenceNumber = "Four", ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, EntryDateTime = new DateTime(01 / 01 / 2020), AuthorizedSharesCapitalPrmKey = 1, UserProfilePrmKey = 1, UserAction = "A", Remark = "None", NameOfUser = "None", MakerEntryDateTime = DateTime.Now, NameOfMaker = "Super User" },
//        //                                        }.ToList();

//        //    mock.Setup(m => m.GetAuthorizedSharesCapitalIndex()).Returns(Task.FromResult<IEnumerable<AuthorizedSharesCapitalViewModel>>(IndexOfAuthorizedEntries));

//        //    // Arrange - create the controller
//        //    AuthorizedSharesCapitalController target = new AuthorizedSharesCapitalController(mock.Object);

//        //    // Action -target the controller
//        //    var result = await target.Index() as ViewResult;

//        //    // Assert 
//        //    Assert.AreEqual(IndexOfAuthorizedEntries.Count, 3);
//        //    Assert.AreEqual("VRF", IndexOfAuthorizedEntries[0].EntryStatus);
//        //    Assert.AreEqual("VRF", IndexOfAuthorizedEntries[1].EntryStatus);
//        //    Assert.AreEqual("VRF", IndexOfAuthorizedEntries[2].EntryStatus);
//        //}

//    }
//}
