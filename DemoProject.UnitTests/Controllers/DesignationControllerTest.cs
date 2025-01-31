using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Master.General;
using DemoProject.Services.ViewModel.Master.General;
using DemoProject.WebUI.Controllers;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Abstract.Management.Master;

namespace DemoProject.UnitTests.Controllers
{
    [TestClass]
    public class DesignationControllerTest
    {

        [TestMethod]
        public async Task Can_Amend_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Designation
                DesignationViewModel designationViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(designationViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The Designation 
                ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The Designation
                DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.Amend(designationViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The Designation
                ActionResult actionResult = await target.Amend(designationViewModel, "Amend");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Amend(designationViewModel));

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
            // Arrange - Create The Designation
            DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            mock.Setup(p => p.Amend(designationViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DesignationController target = new DesignationController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Amend The Designation
            ActionResult actionResult = await target.Amend(designationViewModel, "Amend");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Amend(designationViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Create_Post_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Designation
                DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.Save(designationViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Create The Designation
                ActionResult actionResult = await target.Create(designationViewModel);

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Save(designationViewModel));

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
        public async Task Can_Create_Valid_Entry()
        {
            // Arrange - Create The Designation
            DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            mock.Setup(p => p.Save(designationViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            DesignationController target = new DesignationController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Save The Designation
            ActionResult actionResult = await target.Create(designationViewModel);

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Save(designationViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Delete_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Designation
                DesignationViewModel designationViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(designationViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Delete The Designation
                ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The Designation
                DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Delete, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.Delete(designationViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Delete The Designation
                ActionResult actionResult = await target.Amend(designationViewModel, "Delete");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Delete(designationViewModel));

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
            // Arrange - Create The Designation
            DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Delete, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            mock.Setup(p => p.Delete(designationViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DesignationController target = new DesignationController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };
            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Amend The Designation
            ActionResult actionResult = await target.Amend(designationViewModel, "Delete");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Delete(designationViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Modify_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Designation
                DesignationViewModel designationViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(designationViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Modify The Designation
                ActionResult actionResult = await target.Modify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The Designation
                DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.Modify(designationViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Modify The Designation
                ActionResult actionResult = await target.Modify(designationViewModel);

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Modify(designationViewModel));

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
            // Arrange - Create The Designation
            DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            mock.Setup(p => p.Modify(designationViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DesignationController target = new DesignationController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Modify The Designation
            ActionResult actionResult = await target.Modify(designationViewModel);

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Modify(designationViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Reject_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Designation
                DesignationViewModel designationViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(designationViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Reject The Designation
                ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The Designation
                DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.Reject(designationViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Reject The Designation
                ActionResult actionResult = await target.Verify(designationViewModel, "Reject");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Reject(designationViewModel));

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
            // Arrange - Create The Designation
            DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            mock.Setup(p => p.Reject(designationViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DesignationController target = new DesignationController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };
            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Verify The Designation
            ActionResult actionResult = await target.Verify(designationViewModel, "Reject");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Reject(designationViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_RejectedIndex_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Designation
                IEnumerable<DesignationViewModel> designationViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.GetIndexOfRejectedEntries()).Returns(Task.FromResult(designationViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Designation
                ActionResult actionResult = await target.RejectedIndex();

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetIndexOfRejectedEntries());

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
        public async Task Can_UnverifiedIndex_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Designation
                IEnumerable<DesignationViewModel> designationViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult(designationViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Designation
                ActionResult actionResult = await target.UnverifiedIndex();

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetIndexOfUnVerifiedEntries());

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
        public async Task Can_VerifiedIndex_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Designation
                IEnumerable<DesignationViewModel> designationViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.GetIndexOfVerifiedEntries()).Returns(Task.FromResult(designationViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Designation
                ActionResult actionResult = await target.VerifiedIndex();

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetIndexOfVerifiedEntries());

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
        public async Task Can_Verify_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Designation
                DesignationViewModel designationViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(designationViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Designation
                ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The Designation
                DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

                mock.Setup(p => p.Verify(designationViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DesignationController target = new DesignationController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Designation
                ActionResult actionResult = await target.Verify(designationViewModel, "Verify");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Verify(designationViewModel));

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
            // Arrange - Create The Designation
            DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            mock.Setup(p => p.Verify(designationViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DesignationController target = new DesignationController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Verify The Designation
            ActionResult actionResult = await target.Verify(designationViewModel, "Verify");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Verify(designationViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Amend_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            // Arrange - Create The controller
            DesignationController target = new DesignationController(mock.Object);

            // Arrange - Create The Designation
            DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Amend The Designation
            ActionResult actionResult = await target.Amend(designationViewModel, "Amend");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Amend(It.IsAny<DesignationViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Create_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            // Arrange - Create The controller
            DesignationController target = new DesignationController(mock.Object);

            // Arrange - Create The Designation
            DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Save The Designation
            ActionResult actionResult = await target.Create(designationViewModel);

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Save(It.IsAny<DesignationViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Delete_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            // Arrange - Create The controller
            DesignationController target = new DesignationController(mock.Object);

            // Arrange - Create The Designation
            DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Delete, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Amend The Designation
            ActionResult actionResult = await target.Amend(designationViewModel, "Delete");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Delete(It.IsAny<DesignationViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Modify_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            // Arrange - Create The controller
            DesignationController target = new DesignationController(mock.Object);

            // Arrange - Create The Designation
            DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Save The Designation
            ActionResult actionResult = await target.Modify(designationViewModel);

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Modify(It.IsAny<DesignationViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Reject_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            // Arrange - Create The controller
            DesignationController target = new DesignationController(mock.Object);

            // Arrange - Create The Designation
            DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Reject The Designation
            ActionResult actionResult = await target.Verify(designationViewModel, "Reject");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Reject(It.IsAny<DesignationViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Verify_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            // Arrange - Create The controller
            DesignationController target = new DesignationController(mock.Object);

            // Arrange - Create The Designation
            DesignationViewModel designationViewModel = new DesignationViewModel { PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01 / 01 / 2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Verify The Designation
            ActionResult actionResult = await target.Verify(designationViewModel, "Verify");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Verify(It.IsAny<DesignationViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task RejectedIndex_Contains_AllRejected_Entries()
        {
            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            var IndexOfRejectedEntries = new DesignationViewModel[]
                                                {
                                                   new DesignationViewModel{ PrmKey = 11, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manage", AliasName = "Mngr", NameOnReport = "Manager", SequenceNumber = 1, ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "Data Entry Mistake", EntryDateTime = new DateTime(01/01/2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "Spelling Mistake In Name Of Designation (i.e. 'r' is Missing In Manager)", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 11, NameOfUser = "Administrator"},
                                                   new DesignationViewModel{ PrmKey = 22, DesignationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Anayst", AliasName = "Anlyst", NameOnReport = "Analyst", SequenceNumber = 2, ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "Data Entry Mistake", EntryDateTime = new DateTime(01/01/2020), DesignationPrmKey = 2, UserProfilePrmKey = 2, UserAction = StringLiteralValue.Reject, Remark = "Spelling Mistake In Name Of Designation (i.e. 'l' is Missing In Analyst)", DesignationTranslationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "विश्लेषक", TransAliasName = "None", TransNameOnReport = "विश्लेषक", TransNote = "None", DesignationTranslationPrmKey = 22, NameOfUser = "Super User"},
                                                   new DesignationViewModel{ PrmKey = 33, DesignationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Cheif Executive", AliasName = "CEO", NameOnReport = "Cheif Executive Officer", SequenceNumber = 3, ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "Data Entry Mistake", EntryDateTime = new DateTime(01/01/2020), DesignationPrmKey = 3, UserProfilePrmKey = 3, UserAction = StringLiteralValue.Reject, Remark = "'Officer' Word Missing In Name Of Designation", DesignationTranslationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "मुख्य कार्यकारी अधिकारी", TransAliasName = "None", TransNameOnReport = "मुख्य कार्यकारी अधिकारी", TransNote = "None", DesignationTranslationPrmKey = 33, NameOfUser = "Super User"},
                                                }.ToList();

            mock.Setup(m => m.GetIndexOfRejectedEntries()).Returns(Task.FromResult<IEnumerable<DesignationViewModel>>(IndexOfRejectedEntries));

            // Arrange - create the controller
            DesignationController target = new DesignationController(mock.Object);

            // Action -target the controller
            var result = await target.RejectedIndex() as ViewResult;

            // Assert 
            Assert.AreEqual(IndexOfRejectedEntries.Count, 3);
            Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[0].EntryStatus);
            Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[1].EntryStatus);
            Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[2].EntryStatus);
        }

        [TestMethod]
        public async Task UnverifiedIndex_Contains_AllUnVerified_Entries()
        {
            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            var IndexOfUnVerifiedEntries = new DesignationViewModel[]
                                                {
                                                   new DesignationViewModel{ PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "Data Entry Mistake", EntryDateTime = new DateTime(01/01/2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "User1"},
                                                   new DesignationViewModel{ PrmKey = 2, DesignationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Analyst", AliasName = "Anlyst", NameOnReport = "Analyst", SequenceNumber = 2, ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "Data Entry Mistake", EntryDateTime = new DateTime(01/01/2020), DesignationPrmKey = 2, UserProfilePrmKey = 2, UserAction = StringLiteralValue.Create, Remark = "None", DesignationTranslationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "विश्लेषक", TransAliasName = "None", TransNameOnReport = "विश्लेषक", TransNote = "None", DesignationTranslationPrmKey = 2, NameOfUser = "User2"},
                                                   new DesignationViewModel{ PrmKey = 3, DesignationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Cheif Executive Officer", AliasName = "CEO", NameOnReport = "Cheif Executive Officer", SequenceNumber = 3, ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01/01/2020), DesignationPrmKey = 3, UserProfilePrmKey = 3, UserAction = StringLiteralValue.Create, Remark = "None", DesignationTranslationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "मुख्य कार्यकारी अधिकारी", TransAliasName = "None", TransNameOnReport = "मुख्य कार्यकारी अधिकारी", TransNote = "None", DesignationTranslationPrmKey = 3, NameOfUser = "User3"},
                                                }.ToList();

            mock.Setup(m => m.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult<IEnumerable<DesignationViewModel>>(IndexOfUnVerifiedEntries));

            // Arrange - create the controller
            DesignationController target = new DesignationController(mock.Object);

            // Action -target the controller
            var result = await target.UnverifiedIndex() as ViewResult;

            // Assert 
            Assert.AreEqual(IndexOfUnVerifiedEntries.Count, 3);
            Assert.AreEqual(StringLiteralValue.Create, IndexOfUnVerifiedEntries[0].EntryStatus);
            Assert.AreEqual(StringLiteralValue.Create, IndexOfUnVerifiedEntries[1].EntryStatus);
            Assert.AreEqual(StringLiteralValue.Create, IndexOfUnVerifiedEntries[2].EntryStatus);
        }

        [TestMethod]
        public async Task VerifiedIndex_Contains_AllVerified_Entries()
        {
            // Arrange - Create The Mock Repository
            Mock<IDesignationRepository> mock = new Mock<IDesignationRepository>();

            var IndexOfVerifiedEntries = new DesignationViewModel[]
                                                {
                                                   new DesignationViewModel{ PrmKey = 1, DesignationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Manager", AliasName = "Mngr", NameOnReport = "Branch Manager", SequenceNumber = 1, ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01/01/2020), UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", DesignationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "व्यवस्थापक", TransAliasName = "None", TransNameOnReport = "व्यवस्थापक", TransNote = "None", DesignationTranslationPrmKey = 1, NameOfUser = "Administrator"},
                                                   new DesignationViewModel{ PrmKey = 2, DesignationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Analyst", AliasName = "Anlyst", NameOnReport = "Analyst", SequenceNumber = 2, ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01/01/2020), DesignationPrmKey = 2, UserProfilePrmKey = 2, UserAction = StringLiteralValue.Verify, Remark = "None", DesignationTranslationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "विश्लेषक", TransAliasName = "None", TransNameOnReport = "विश्लेषक", TransNote = "None", DesignationTranslationPrmKey = 2, NameOfUser = "User1"},
                                                   new DesignationViewModel{ PrmKey = 3, DesignationId = Guid.Parse("1133223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDesignation = "Cheif Executive Officer", AliasName = "CEO", NameOnReport = "Cheif Executive Officer", SequenceNumber = 3, ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", DesignationModificationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), ReasonForModification = "", EntryDateTime = new DateTime(01/01/2020), DesignationPrmKey = 3, UserProfilePrmKey = 3, UserAction = StringLiteralValue.Verify, Remark = "None", DesignationTranslationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDesignation = "मुख्य कार्यकारी अधिकारी", TransAliasName = "None", TransNameOnReport = "मुख्य कार्यकारी अधिकारी", TransNote = "None", DesignationTranslationPrmKey = 3, NameOfUser = "User2"},
                                                }.ToList();

            mock.Setup(m => m.GetIndexOfVerifiedEntries()).Returns(Task.FromResult<IEnumerable<DesignationViewModel>>(IndexOfVerifiedEntries));

            // Arrange - create the controller
            DesignationController target = new DesignationController(mock.Object);

            // Action -target the controller
            var result = await target.VerifiedIndex() as ViewResult;

            // Assert 
            Assert.AreEqual(IndexOfVerifiedEntries.Count, 3);
            Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[0].EntryStatus);
            Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[1].EntryStatus);
            Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[2].EntryStatus);
        }

    }
}
