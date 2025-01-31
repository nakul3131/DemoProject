//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using DemoProject.WebUI.Controllers;
//using DemoProject.WebUI.Infrastructure.CustomException;
//using DemoProject.Services.Constants;
//using DemoProject.Services.ViewModel.Account.Master;
//using DemoProject.Services.Abstract.Account.Master;

//namespace DemoProject.UnitTests.Controllers
//{
//    [TestClass]
//    public class VehicleMakeControllerTest
//    {
//        [TestMethod]
//        public async Task Can_Amend_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The VehicleMake
//                VehicleMakeViewModel vehicleBodyTypeViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//                mock.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(vehicleBodyTypeViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Amend The VehicleMake 
//                ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
//                // Arrange - Create The VehicleMake
//                VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//                mock.Setup(p => p.Amend(vehicleBodyTypeViewModel)).Returns(Task.FromResult(false));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Amend The VehicleMake
//                ActionResult actionResult = await target.Amend(vehicleBodyTypeViewModel, "Amend");

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.Amend(vehicleBodyTypeViewModel));

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
//            // Arrange - Create The VehicleMake
//            VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//            mock.Setup(p => p.Amend(vehicleBodyTypeViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//            // Arrange - Create The controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            // Act - Try To Amend The VehicleMake
//            ActionResult actionResult = await target.Amend(vehicleBodyTypeViewModel, "Amend");

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Amend(vehicleBodyTypeViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }
        
//        [TestMethod]
//        public async Task Can_Create_Post_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The VehicleMake
//                VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//                mock.Setup(p => p.Save(vehicleBodyTypeViewModel)).Returns(Task.FromResult(false));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Create The VehicleMake
//                ActionResult actionResult = await target.Create(vehicleBodyTypeViewModel);

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.Save(vehicleBodyTypeViewModel));

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
//        public async Task Can_Create_Valid_Entry()
//        {
//            // Arrange - Create The VehicleMake
//            VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//            mock.Setup(p => p.Save(vehicleBodyTypeViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

//            // Arrange - Create The controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            // Act - Try To Save The VehicleMake
//            ActionResult actionResult = await target.Create(vehicleBodyTypeViewModel);

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Save(vehicleBodyTypeViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Can_Delete_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The VehicleMake
//                VehicleMakeViewModel vehicleBodyTypeViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//                mock.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(vehicleBodyTypeViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Delete The VehicleMake
//                ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
//                // Arrange - Create The VehicleMake
//                VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Delete, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//               // mock.Setup(p => p.Delete(vehicleBodyTypeViewModel)).Returns(Task.FromResult(false));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Delete The VehicleMake
//                ActionResult actionResult = await target.Amend(vehicleBodyTypeViewModel, "Delete");

//                // Assert - Check That The Repository Was Called
//               // mock.Verify(m => m.Delete(vehicleBodyTypeViewModel));

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
//            // Arrange - Create The VehicleMake
//            VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Delete, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//           // mock.Setup(p => p.Delete(vehicleBodyTypeViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//            // Arrange - Create The controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };
//            var mockUrlHelper = new Mock<UrlHelper>();
//            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
//            target.Url = mockUrlHelper.Object;

//            // Act - Try To Amend The VehicleMake
//            ActionResult actionResult = await target.Amend(vehicleBodyTypeViewModel, "Delete");

//            // Assert - Check That The Repository Was Called
//           // mock.Verify(m => m.Delete(vehicleBodyTypeViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }
        
//        [TestMethod]
//        public async Task Can_Modify_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The VehicleMake
//                VehicleMakeViewModel vehicleBodyTypeViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//                mock.Setup(p => p.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(vehicleBodyTypeViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Modify The VehicleMake
//                ActionResult actionResult = await target.Modify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
//                // Arrange - Create The VehicleMake
//                VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//                mock.Setup(p => p.Modify(vehicleBodyTypeViewModel)).Returns(Task.FromResult(false));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Modify The VehicleMake
//                ActionResult actionResult = await target.Modify(vehicleBodyTypeViewModel);

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.Modify(vehicleBodyTypeViewModel));

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
//            // Arrange - Create The VehicleMake
//            VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//            mock.Setup(p => p.Modify(vehicleBodyTypeViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//            // Arrange - Create The controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            // Act - Try To Modify The VehicleMake
//            ActionResult actionResult = await target.Modify(vehicleBodyTypeViewModel);

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Modify(vehicleBodyTypeViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Can_Reject_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The VehicleMake
//                VehicleMakeViewModel vehicleBodyTypeViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//                mock.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(vehicleBodyTypeViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Reject The VehicleMake
//                ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
//                // Arrange - Create The VehicleMake
//                VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//               // mock.Setup(p => p.Reject(vehicleBodyTypeViewModel)).Returns(Task.FromResult(false));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Reject The VehicleMake
//                ActionResult actionResult = await target.Verify(vehicleBodyTypeViewModel, "Reject");

//                // Assert - Check That The Repository Was Called
//               // mock.Verify(m => m.Reject(vehicleBodyTypeViewModel));

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
//            // Arrange - Create The VehicleMake
//            VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//           // mock.Setup(p => p.Reject(vehicleBodyTypeViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//            // Arrange - Create The controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };
//            var mockUrlHelper = new Mock<UrlHelper>();
//            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
//            target.Url = mockUrlHelper.Object;

//            // Act - Try To Verify The VehicleMake
//            ActionResult actionResult = await target.Verify(vehicleBodyTypeViewModel, "Reject");

//            // Assert - Check That The Repository Was Called
//            //mock.Verify(m => m.Reject(vehicleBodyTypeViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Can_RejectedIndex_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The VehicleMake
//                IEnumerable<VehicleMakeViewModel> vehicleBodyTypeViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//                mock.Setup(p => p.GetIndexOfRejectedEntries()).Returns(Task.FromResult(vehicleBodyTypeViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Verify The VehicleMake
//                ActionResult actionResult = await target.RejectedIndex();

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetIndexOfRejectedEntries());

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
//        public async Task Can_UnverifiedIndex_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The VehicleMake
//                IEnumerable<VehicleMakeViewModel> vehicleBodyTypeViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//                mock.Setup(p => p.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult(vehicleBodyTypeViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Verify The VehicleMake
//                ActionResult actionResult = await target.UnverifiedIndex();

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetIndexOfUnVerifiedEntries());

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
//        public async Task Can_VerifiedIndex_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The VehicleMake
//                IEnumerable<VehicleMakeViewModel> vehicleBodyTypeViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//                mock.Setup(p => p.GetIndexOfVerifiedEntries()).Returns(Task.FromResult(vehicleBodyTypeViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Verify The VehicleMake
//                ActionResult actionResult = await target.VerifiedIndex();

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetIndexOfVerifiedEntries());

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
//        public async Task Can_Verify_Get_Method_Throws_Exception()
//        {
//            var expectedException = new DatabaseException();

//            try
//            {
//                // Arrange - Create The VehicleMake
//                VehicleMakeViewModel vehicleBodyTypeViewModel = null;

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//                mock.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(vehicleBodyTypeViewModel));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Verify The VehicleMake
//                ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

//                // Assert - Check That The Repository Was Called
//                mock.Verify(m => m.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
//                // Arrange - Create The VehicleMake
//                VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//                // Arrange - Create The Mock Repository
//                Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//                //mock.Setup(p => p.Verify(vehicleBodyTypeViewModel)).Returns(Task.FromResult(false));

//                var mockControllerContext = new Mock<ControllerContext>();
//                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//                // Arrange - Create The controller
//                VehicleMakeController target = new VehicleMakeController(mock.Object)
//                {
//                    ControllerContext = mockControllerContext.Object
//                };

//                // Act - Try To Verify The VehicleMake
//                ActionResult actionResult = await target.Verify(vehicleBodyTypeViewModel, "Verify");

//                // Assert - Check That The Repository Was Called
//               // mock.Verify(m => m.Verify(vehicleBodyTypeViewModel));

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
//            // Arrange - Create The VehicleMake
//            VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//           // mock.Setup(p => p.Verify(vehicleBodyTypeViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//            // Arrange - Create The controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            var mockUrlHelper = new Mock<UrlHelper>();
//            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
//            target.Url = mockUrlHelper.Object;

//            // Act - Try To Verify The VehicleMake
//            ActionResult actionResult = await target.Verify(vehicleBodyTypeViewModel, "Verify");

//            // Assert - Check That The Repository Was Called
//           // mock.Verify(m => m.Verify(vehicleBodyTypeViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }
        
//        [TestMethod]
//        public async Task Cannot_Amend_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//            // Arrange - Create The controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object);

//            // Arrange - Create The VehicleMake
//            VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Amend The VehicleMake
//            ActionResult actionResult = await target.Amend(vehicleBodyTypeViewModel, "Amend");

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Amend(It.IsAny<VehicleMakeViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }
        
//        [TestMethod]
//        public async Task Cannot_Create_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//            // Arrange - Create The controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object);

//            // Arrange - Create The VehicleMake
//            VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Save The VehicleMake
//            ActionResult actionResult = await target.Create(vehicleBodyTypeViewModel);

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Save(It.IsAny<VehicleMakeViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Delete_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//            // Arrange - Create The controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object);

//            // Arrange - Create The VehicleMake
//            VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Delete, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Amend The VehicleMake
//            ActionResult actionResult = await target.Amend(vehicleBodyTypeViewModel, "Delete");

//            // Assert - Check That The Repository Was Not Called
//           // mock.Verify(m => m.Delete(It.IsAny<VehicleMakeViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }
        
//        [TestMethod]
//        public async Task Cannot_Modify_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//            // Arrange - Create The controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object);

//            // Arrange - Create The VehicleMake
//            VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Save The VehicleMake
//            ActionResult actionResult = await target.Modify(vehicleBodyTypeViewModel);

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Modify(It.IsAny<VehicleMakeViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Reject_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//            // Arrange - Create The controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object);

//            VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Reject The VehicleMake
//            ActionResult actionResult = await target.Verify(vehicleBodyTypeViewModel, "Reject");

//            // Assert - Check That The Repository Was Not Called
//            //mock.Verify(m => m.Reject(It.IsAny<VehicleMakeViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }
        
//        [TestMethod]
//        public async Task Cannot_Verify_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//            // Arrange - Create The controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object);

//            // Arrange - Create The VehicleMake
//            VehicleMakeViewModel vehicleBodyTypeViewModel = new VehicleMakeViewModel { PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Verify The VehicleMake
//            ActionResult actionResult = await target.Verify(vehicleBodyTypeViewModel, "Verify");

//            // Assert - Check That The Repository Was Not Called
//            //mock.Verify(m => m.Verify(It.IsAny<VehicleMakeViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }
        
//        [TestMethod]
//        public async Task RejectedIndex_Contains_AllRejected_Entries()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//            var IndexOfRejectedEntries = new VehicleMakeViewModel[]
//                                                {
//                                                   new VehicleMakeViewModel{ PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" },
//                                                   new VehicleMakeViewModel{ PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" },
//                                                   new VehicleMakeViewModel{ PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" },
//                                                }.ToList();

//            mock.Setup(m => m.GetIndexOfRejectedEntries()).Returns(Task.FromResult<IEnumerable<VehicleMakeViewModel>>(IndexOfRejectedEntries));

//            // Arrange - create the controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object);

//            // Action -target the controller
//            var result = await target.RejectedIndex() as ViewResult;

//            // Assert 
//            Assert.AreEqual(IndexOfRejectedEntries.Count, 3);
//            Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[0].EntryStatus);
//            Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[1].EntryStatus);
//            Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[2].EntryStatus);
//        }

//        [TestMethod]
//        public async Task UnverifiedIndex_Contains_AllUnVerified_Entries()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//            var IndexOfUnVerifiedEntries = new VehicleMakeViewModel[]
//                                                {
//                                                   new VehicleMakeViewModel{ PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" },
//                                                   new VehicleMakeViewModel{ PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" },
//                                                   new VehicleMakeViewModel{ PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" },
//                                                }.ToList();

//            mock.Setup(m => m.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult<IEnumerable<VehicleMakeViewModel>>(IndexOfUnVerifiedEntries));

//            // Arrange - create the controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object);

//            // Action -target the controller
//            var result = await target.UnverifiedIndex() as ViewResult;

//            // Assert 
//            Assert.AreEqual(IndexOfUnVerifiedEntries.Count, 3);
//            Assert.AreEqual(StringLiteralValue.Create, IndexOfUnVerifiedEntries[0].EntryStatus);
//            Assert.AreEqual(StringLiteralValue.Create, IndexOfUnVerifiedEntries[1].EntryStatus);
//            Assert.AreEqual(StringLiteralValue.Create, IndexOfUnVerifiedEntries[2].EntryStatus);
//        }

//        [TestMethod]
//        public async Task VerifiedIndex_Contains_AllVerified_Entries()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IVehicleMakeRepository> mock = new Mock<IVehicleMakeRepository>();

//            var IndexOfVerifiedEntries = new VehicleMakeViewModel[]
//                                                {
//                                                   new VehicleMakeViewModel{ PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" },
//                                                   new VehicleMakeViewModel{ PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" },
//                                                   new VehicleMakeViewModel{ PrmKey = 1, VehicleMakeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfVehicleMake = "Abc", AliasName = "Mngr", NameOnReport = "Branch Manager", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", VehicleMakeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), VehicleMakePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", VehicleMakeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfVehicleMake = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", VehicleMakeTranslationPrmKey = 1, NameOfUser = "Administrator" },
//                                                }.ToList();

//            mock.Setup(m => m.GetIndexOfVerifiedEntries()).Returns(Task.FromResult<IEnumerable<VehicleMakeViewModel>>(IndexOfVerifiedEntries));

//            // Arrange - create the controller
//            VehicleMakeController target = new VehicleMakeController(mock.Object);

//            // Action -target the controller
//            var result = await target.VerifiedIndex() as ViewResult;

//            // Assert 
//            Assert.AreEqual(IndexOfVerifiedEntries.Count, 3);
//            Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[0].EntryStatus);
//            Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[1].EntryStatus);
//            Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[2].EntryStatus);
//        }

//    }
//}
