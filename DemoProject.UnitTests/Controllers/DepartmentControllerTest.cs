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
    public class DepartmentControllerTest
    {
        [TestMethod]
        public async Task Can_Amend_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Department
                DepartmentViewModel departmentViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(departmentViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The Department 
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
                // Arrange - Create The Department
                DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.Amend(departmentViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The Department
                ActionResult actionResult = await target.Amend(departmentViewModel, "Amend");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Amend(departmentViewModel));

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
            // Arrange - Create The Department
            DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            mock.Setup(p => p.Amend(departmentViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            DepartmentController target = new DepartmentController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Amend The Department
            ActionResult actionResult = await target.Amend(departmentViewModel, "Amend");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Amend(departmentViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Create_Post_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Department
                DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.Save(departmentViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Create The Department
                ActionResult actionResult = await target.Create(departmentViewModel);

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Save(departmentViewModel));

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
            // Arrange - Create The Department
            DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            mock.Setup(p => p.Save(departmentViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            DepartmentController target = new DepartmentController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Save The Department
            ActionResult actionResult = await target.Create(departmentViewModel);

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Save(departmentViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Delete_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Department
                DepartmentViewModel departmentViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(departmentViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Delete The Department
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
                // Arrange - Create The Department
                DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Delete, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.Delete(departmentViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The Department
                ActionResult actionResult = await target.Amend(departmentViewModel, "Delete");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Delete(departmentViewModel));

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
            // Arrange - Create The Department
            DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Delete, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            mock.Setup(p => p.Delete(departmentViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            DepartmentController target = new DepartmentController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };
            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Amend The Department
            ActionResult actionResult = await target.Amend(departmentViewModel, "Delete");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Delete(departmentViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Modify_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Department
                DepartmentViewModel departmentViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(departmentViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Modify The Department
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
                // Arrange - Create The Department
                DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.Modify(departmentViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Modify The Department
                ActionResult actionResult = await target.Modify(departmentViewModel);

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Modify(departmentViewModel));

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
            // Arrange - Create The Department
            DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            mock.Setup(p => p.Modify(departmentViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            DepartmentController target = new DepartmentController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Save The Department
            ActionResult actionResult = await target.Modify(departmentViewModel);

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Modify(departmentViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Reject_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Department
                DepartmentViewModel departmentViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(departmentViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Reject The Department
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
                // Arrange - Create The Department
                DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.Reject(departmentViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Department
                ActionResult actionResult = await target.Verify(departmentViewModel, "Reject");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Reject(departmentViewModel));

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
            // Arrange - Create The Department
            DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            mock.Setup(p => p.Reject(departmentViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)6);

            // Arrange - Create The controller
            DepartmentController target = new DepartmentController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Verify The Department
            ActionResult actionResult = await target.Verify(departmentViewModel, "Reject");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Reject(departmentViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_RejectedIndex_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Department
                IEnumerable<DepartmentViewModel> departmentViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.GetIndexOfRejectedEntries()).Returns(Task.FromResult(departmentViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Department
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
                // Arrange - Create The Department
                IEnumerable<DepartmentViewModel> departmentViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult(departmentViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Department
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
                // Arrange - Create The Department
                IEnumerable<DepartmentViewModel> departmentViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.GetIndexOfVerifiedEntries()).Returns(Task.FromResult(departmentViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Department
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
                // Arrange - Create The Department
                DepartmentViewModel departmentViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(departmentViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Department
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
                // Arrange - Create The Department
                DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

                mock.Setup(p => p.Verify(departmentViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DepartmentController target = new DepartmentController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Department
                ActionResult actionResult = await target.Verify(departmentViewModel, "Verify");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Verify(departmentViewModel));

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
            // Arrange - Create The Department
            DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            mock.Setup(p => p.Verify(departmentViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            DepartmentController target = new DepartmentController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Verify The Department
            ActionResult actionResult = await target.Verify(departmentViewModel, "Verify");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Verify(departmentViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Amend_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            // Arrange - Create The controller
            DepartmentController target = new DepartmentController(mock.Object);

            // Arrange - Create The Department
            DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Amend The Department
            ActionResult actionResult = await target.Amend(departmentViewModel, "Amend");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Amend(It.IsAny<DepartmentViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Create_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            // Arrange - Create The controller
            DepartmentController target = new DepartmentController(mock.Object);

            // Arrange - Create The Department
            DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Save The Department
            ActionResult actionResult = await target.Create(departmentViewModel);

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Save(It.IsAny<DepartmentViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Delete_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            // Arrange - Create The controller
            DepartmentController target = new DepartmentController(mock.Object);

            // Arrange - Create The Department
            DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Delete, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Amend The Department
            ActionResult actionResult = await target.Amend(departmentViewModel, "Delete");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Delete(It.IsAny<DepartmentViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Modify_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            // Arrange - Create The controller
            DepartmentController target = new DepartmentController(mock.Object);

            // Arrange - Create The Department
            DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Save The Designation
            ActionResult actionResult = await target.Modify(departmentViewModel);

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Modify(It.IsAny<DepartmentViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Reject_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            // Arrange - Create The controller
            DepartmentController target = new DepartmentController(mock.Object);

            // Arrange - Create The Department
            DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Reject The Department
            ActionResult actionResult = await target.Verify(departmentViewModel, "Reject");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Reject(It.IsAny<DepartmentViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Verify_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            // Arrange - Create The controller
            DepartmentController target = new DepartmentController(mock.Object);

            // Arrange - Create The Department
            DepartmentViewModel departmentViewModel = new DepartmentViewModel { PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Public Accounts Department", NameOnReport = "Public Accounts Department", Objective = "Collection", ActivationDate = new DateTime(01 / 01 / 2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective = "उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Verify The Department
            ActionResult actionResult = await target.Verify(departmentViewModel, "Verify");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Verify(It.IsAny<DepartmentViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task RejectedIndex_Contains_AllRejected_Entries()
        {
            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            var IndexOfRejectedEntries = new DepartmentViewModel[]
                                                {
                                                   new DepartmentViewModel{ PrmKey = 11, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  NameOfDepartment = "Public Accounts Department", AliasName = "Mngr", NameOnReport = "Public Accounts Department",  Objective="Collection",  ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, ActivationStatus = "A", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "Spelling Mistake In Name Of Designation (i.e. 'r' is Missing In Manager)", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective="उद्देश",TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक",DepartmentTranslationPrmKey = 11, NameOfUser = "Administrator"},
                                                   new DepartmentViewModel{ PrmKey = 22, DepartmentId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"),  NameOfDepartment = "Deposit Accounts Department", AliasName = "Deposit Accounts Department", NameOnReport = "Deposit Accounts Department", Objective="Collection",  ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, ActivationStatus = "A", DepartmentModificationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01/01/2020), DepartmentPrmKey = 2, UserProfilePrmKey = 2, UserAction = StringLiteralValue.Reject, Remark = "Spelling Mistake In Name Of Designation (i.e. 'l' is Missing In Analyst)", DepartmentTranslationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "ठेव विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "ठेव अहवालासाठी नाव", TransObjective="उद्देश",TransNote = "टिप",TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 22, NameOfUser = "Super User"},
                                                   new DepartmentViewModel{ PrmKey = 33, DepartmentId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"),  NameOfDepartment = "Debt Department Department", AliasName = "Debt Department Department", NameOnReport = "Debt Department Department", Objective="Collection",  ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, ActivationStatus = "A", DepartmentModificationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01/01/2020), DepartmentPrmKey = 3, UserProfilePrmKey = 3, UserAction = StringLiteralValue.Reject, Remark = "'Officer' Word Missing In Name Of Designation", DepartmentTranslationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "कर्ज विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "कर्ज अहवालासाठी नाव", TransObjective="उद्देश",TransNote = "टिप",TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 33, NameOfUser = "Super User"},
                                                }.ToList();

            mock.Setup(m => m.GetIndexOfRejectedEntries()).Returns(Task.FromResult<IEnumerable<DepartmentViewModel>>(IndexOfRejectedEntries));

            // Arrange - create the controller
            DepartmentController target = new DepartmentController(mock.Object);

            // Action -target the controller
            var result = await target.RejectedIndex() as ViewResult;

            // Assert 
            Assert.AreEqual(IndexOfRejectedEntries.Count, 3);
            Assert.AreEqual("R", IndexOfRejectedEntries[0].EntryStatus);
            Assert.AreEqual("R", IndexOfRejectedEntries[1].EntryStatus);
            Assert.AreEqual("R", IndexOfRejectedEntries[2].EntryStatus);
        }

        [TestMethod]
        public async Task UnverifiedIndex_Contains_AllUnVerified_Entries()
        {
            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            var IndexOfUnVerifiedEntries = new DepartmentViewModel[]
                                                {
                                                   new DepartmentViewModel{ PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  NameOfDepartment = "Public Accounts Department", AliasName = "Mngr", NameOnReport = "Public Accounts Department", Objective="Collection", ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01/01/2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective="उद्देश",TransNote = "टिप",TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "User1"},
                                                   new DepartmentViewModel{ PrmKey = 2, DepartmentId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"),  NameOfDepartment = "Deposit Accounts Department", AliasName = "Deposit Accounts Department", NameOnReport = "Deposit Accounts Department", Objective="Collection", ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), DepartmentPrmKey = 2, UserProfilePrmKey = 2, UserAction = StringLiteralValue.Create, Remark = "None", DepartmentTranslationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "ठेव विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "ठेव अहवालासाठी नाव",TransObjective="उद्देश", TransNote = "टिप",TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 2, NameOfUser = "User2"},
                                                   new DepartmentViewModel{ PrmKey = 3, DepartmentId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"),  NameOfDepartment = "Debt Department Department", AliasName = "Debt Department Department", NameOnReport = "Debt Department Department", Objective="Collection",  ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake",  EntryStatus = StringLiteralValue.Create, ActivationStatus = "I", DepartmentModificationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), DepartmentPrmKey = 3, UserProfilePrmKey = 3, UserAction = StringLiteralValue.Create, Remark = "None", DepartmentTranslationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "कर्ज विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "कर्ज अहवालासाठी नाव",TransObjective="उद्देश", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक",DepartmentTranslationPrmKey = 3, NameOfUser = "User3"},
                                                }.ToList();

            mock.Setup(m => m.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult<IEnumerable<DepartmentViewModel>>(IndexOfUnVerifiedEntries));

            // Arrange - create the controller
            DepartmentController target = new DepartmentController(mock.Object);

            // Action -target the controller
            var result = await target.UnverifiedIndex() as ViewResult;

            // Assert 
            Assert.AreEqual(IndexOfUnVerifiedEntries.Count, 3);
            Assert.AreEqual("W", IndexOfUnVerifiedEntries[0].EntryStatus);
            Assert.AreEqual("W", IndexOfUnVerifiedEntries[1].EntryStatus);
            Assert.AreEqual("W", IndexOfUnVerifiedEntries[2].EntryStatus);
        }

        [TestMethod]
        public async Task VerifiedIndex_Contains_AllVerified_Entries()
        {
            // Arrange - Create The Mock Repository
            Mock<IDepartmentRepository> mock = new Mock<IDepartmentRepository>();

            var IndexOfVerifiedEntries = new DepartmentViewModel[]
                                                {
                                                   new DepartmentViewModel{ PrmKey = 1, DepartmentId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Public Accounts Department", AliasName = "Mngr", NameOnReport = "Public Accounts Department",  Objective="Collection", ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, ActivationStatus = "A", DepartmentModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), DepartmentPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", DepartmentTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "लोकलेखा विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "लोकलेखा अहवालासाठी नाव", TransObjective="उद्देश",TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 1, NameOfUser = "Administrator"},
                                                   new DepartmentViewModel{ PrmKey = 2, DepartmentId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Deposit Accounts Department", AliasName = "Deposit Accounts Department", NameOnReport = "Deposit Accounts Department",  Objective="Collection",  ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, ActivationStatus = "A", DepartmentModificationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01/01/2020), DepartmentPrmKey = 2, UserProfilePrmKey = 2, UserAction = StringLiteralValue.Verify, Remark = "None", DepartmentTranslationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "ठेव विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "ठेव अहवालासाठी नाव", TransObjective="उद्देश", TransNote = "टिप",  TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 2, NameOfUser = "User1"},
                                                   new DepartmentViewModel{ PrmKey = 3, DepartmentId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfDepartment = "Debt Department Department", AliasName = "Debt Department Department", NameOnReport = "Debt Department Department", Objective="Collection", ActivationDate = new DateTime(01/01/2020), ExpiryDate = null, CloseDate = null, Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, ActivationStatus = "A", DepartmentModificationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), DepartmentPrmKey = 3, UserProfilePrmKey = 3, UserAction = StringLiteralValue.Verify, Remark = "None", DepartmentTranslationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfDepartment = "कर्ज विभाग", TransAliasName = "उर्फनाव", TransNameOnReport = "कर्ज अहवालासाठी नाव", TransObjective="उद्देश",TransNote = "टिप",TransReasonForModification = "डेटा एंट्री चूक", DepartmentTranslationPrmKey = 3, NameOfUser = "User2"},
                                                }.ToList();

            mock.Setup(m => m.GetIndexOfVerifiedEntries()).Returns(Task.FromResult<IEnumerable<DepartmentViewModel>>(IndexOfVerifiedEntries));

            // Arrange - create the controller
            DepartmentController target = new DepartmentController(mock.Object);

            // Action -target the controller
            var result = await target.VerifiedIndex() as ViewResult;

            // Assert 
            Assert.AreEqual(IndexOfVerifiedEntries.Count, 3);
            Assert.AreEqual("P", IndexOfVerifiedEntries[0].EntryStatus);
            Assert.AreEqual("P", IndexOfVerifiedEntries[1].EntryStatus);
            Assert.AreEqual("P", IndexOfVerifiedEntries[2].EntryStatus);
        }
    }
}
