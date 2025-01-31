using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Enterprise.Schedule;
using DemoProject.Services.ViewModel.Enterprise.Schedule;
using DemoProject.WebUI.Controllers;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Constants;

namespace DemoProject.UnitTests.Controllers
{
    [TestClass]
    public class OfficeScheduleControllerTest
    {
        [TestMethod]
        public async Task Can_Amend_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The OfficeSchedule
                OfficeScheduleViewModel officeScheduleViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(officeScheduleViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The OfficeSchedule 
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
                // Arrange - Create The OfficeSchedule
                OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.Amend(officeScheduleViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The OfficeSchedule
                ActionResult actionResult = await target.Amend(officeScheduleViewModel, "Amend");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Amend(officeScheduleViewModel));

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
            // Arrange - Create The OfficeSchedule
            OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = "A", Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            mock.Setup(p => p.Amend(officeScheduleViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Amend The OfficeSchedule
            ActionResult actionResult = await target.Amend(officeScheduleViewModel, "Amend");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Amend(officeScheduleViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Create_Post_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The OfficeSchedule
                OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.Save(officeScheduleViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Create The OfficeSchedule
                ActionResult actionResult = await target.Create(officeScheduleViewModel,"Save");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Save(officeScheduleViewModel));

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
            // Arrange - Create The OfficeSchedule
            OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            mock.Setup(p => p.Save(officeScheduleViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Save The OfficeSchedule
            ActionResult actionResult = await target.Create(officeScheduleViewModel,"Save");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Save(officeScheduleViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Delete_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The OfficeSchedule
                OfficeScheduleViewModel officeScheduleViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(officeScheduleViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Delete The OfficeSchedule
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
                // Arrange - Create The OfficeSchedule
                OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.Delete(officeScheduleViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The OfficeSchedule
                ActionResult actionResult = await target.Amend(officeScheduleViewModel, "Delete");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Delete(officeScheduleViewModel));

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
            // Arrange - Create The OfficeSchedule
            OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = "D", ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = "D", Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            mock.Setup(p => p.Delete(officeScheduleViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Amend The OfficeSchedule
            ActionResult actionResult = await target.Amend(officeScheduleViewModel, "Delete");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Delete(officeScheduleViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Modify_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The OfficeSchedule
                OfficeScheduleViewModel officeScheduleViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(officeScheduleViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Modify The OfficeSchedule
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
                // Arrange - Create The OfficeSchedule
                OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = "D", ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = "D", Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.Modify(officeScheduleViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Modify The OfficeSchedule
                ActionResult actionResult = await target.Modify(officeScheduleViewModel, "Modify");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Modify(officeScheduleViewModel));

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
            // Arrange - Create The OfficeSchedule
            OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            mock.Setup(p => p.Modify(officeScheduleViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };
            // Act - Try To Save The OfficeSchedule
            ActionResult actionResult = await target.Modify(officeScheduleViewModel, "Modify");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Modify(officeScheduleViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Reject_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The OfficeSchedule
                OfficeScheduleViewModel officeScheduleViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(officeScheduleViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Reject The OfficeSchedule
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
                // Arrange - Create The OfficeSchedule
                OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.Reject(officeScheduleViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The OfficeSchedule
                ActionResult actionResult = await target.Verify(officeScheduleViewModel, "Reject");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Reject(officeScheduleViewModel));

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
            // Arrange - Create The OfficeSchedule
            OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            mock.Setup(p => p.Reject(officeScheduleViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)6);


            // Arrange - Create The controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Save The OfficeSchedule
            ActionResult actionResult = await target.Verify(officeScheduleViewModel, "Reject");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Reject(officeScheduleViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_RejectedIndex_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The OfficeSchedule
                IEnumerable<OfficeScheduleViewModel> officeScheduleViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.GetIndexOfRejectedEntries()).Returns(Task.FromResult(officeScheduleViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The OfficeSchedule
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
                // Arrange - Create The OfficeSchedule
                IEnumerable<OfficeScheduleViewModel> officeScheduleViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult(officeScheduleViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The OfficeSchedule
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
                // Arrange - Create The OfficeSchedule
                IEnumerable<OfficeScheduleViewModel> officeScheduleViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.GetIndexOfVerifiedEntries()).Returns(Task.FromResult(officeScheduleViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The OfficeSchedule
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
                // Arrange - Create The OfficeSchedule
                OfficeScheduleViewModel officeScheduleViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(officeScheduleViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The OfficeSchedule
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

                // Arrange - Create The OfficeSchedule
                OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

                // Arrange - Create The Mock Repository
                Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

                mock.Setup(p => p.Verify(officeScheduleViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                OfficeScheduleController target = new OfficeScheduleController(mock.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The OfficeSchedule
                ActionResult actionResult = await target.Verify(officeScheduleViewModel, "Verify");

                // Assert - Check That The Repository Was Called
                mock.Verify(m => m.Verify(officeScheduleViewModel));

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
            // Arrange - Create The OfficeSchedule
            OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            mock.Setup(p => p.Verify(officeScheduleViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);


            // Arrange - Create The controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Save The OfficeSchedule
            ActionResult actionResult = await target.Verify(officeScheduleViewModel, "Verify");

            // Assert - Check That The Repository Was Called
            mock.Verify(m => m.Verify(officeScheduleViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Amend_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            // Arrange - Create The controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object);

            // Arrange - Create The OfficeSchedule
            OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = "A", Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Amend The OfficeSchedule
            ActionResult actionResult = await target.Amend(officeScheduleViewModel, "Amend");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Amend(It.IsAny<OfficeScheduleViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Create_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            // Arrange - Create The controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object);

            // Arrange - Create The OfficeSchedule
            OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Save The OfficeSchedule
            ActionResult actionResult = await target.Create(officeScheduleViewModel,"Save");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Save(It.IsAny<OfficeScheduleViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Delete_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            // Arrange - Create The controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object);

            // Arrange - Create The OfficeSchedule
            OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = "D", ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = "D", Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Amend The OfficeSchedule
            ActionResult actionResult = await target.Amend(officeScheduleViewModel, "Delete");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Delete(It.IsAny<OfficeScheduleViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Modify_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            // Arrange - Create The controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object);

            // Arrange - Create The OfficeSchedule
            OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Save The OfficeSchedule
            ActionResult actionResult = await target.Modify(officeScheduleViewModel, "Modify");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Modify(It.IsAny<OfficeScheduleViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Reject_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            // Arrange - Create The controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object);

            // Arrange - Create The OfficeSchedule
            OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Reject The OfficeSchedule
            ActionResult actionResult = await target.Verify(officeScheduleViewModel, "Reject");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Reject(It.IsAny<OfficeScheduleViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Verify_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            // Arrange - Create The controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object);

            // Arrange - Create The OfficeSchedule
            OfficeScheduleViewModel officeScheduleViewModel = new OfficeScheduleViewModel { PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule", StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay, OfficeWorkingDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime = Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime = Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime = Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration = Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1 = 1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2 = 1, ModificationNumber = 0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EntryDateTime = new DateTime(01 / 01 / 2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator" };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Verify The OfficeSchedule
            ActionResult actionResult = await target.Verify(officeScheduleViewModel, "Verify");

            // Assert - Check That The Repository Was Not Called
            mock.Verify(m => m.Verify(It.IsAny<OfficeScheduleViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task RejectedIndex_Contains_AllRejected_Entries()
        {
            // Arrange - Create The Mock Repository
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            var IndexOfRejectedEntries = new OfficeScheduleViewModel[]
                                                {
                                                   new OfficeScheduleViewModel{ PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule",  StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay,  OfficeWorkingDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime=Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1=1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2=1, ModificationNumber=0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator"},
                                                   new OfficeScheduleViewModel{ PrmKey = 2, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule",  StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay,  OfficeWorkingDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime=Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1=1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2=1, ModificationNumber=0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator"},
                                                   new OfficeScheduleViewModel{  PrmKey = 3, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule",  StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay,  OfficeWorkingDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime=Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1=1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2=1, ModificationNumber=0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator"},
                                                }.ToList();

            mock.Setup(m => m.GetIndexOfRejectedEntries()).Returns(Task.FromResult<IEnumerable<OfficeScheduleViewModel>>(IndexOfRejectedEntries));

            // Arrange - create the controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object);

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
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            var IndexOfUnVerifiedEntries = new OfficeScheduleViewModel[]
                                                {
                                                   new OfficeScheduleViewModel{ PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule",  StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay,  OfficeWorkingDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime=Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1=1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2=1, ModificationNumber=0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator"},
                                                   new OfficeScheduleViewModel{ PrmKey = 2, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule",  StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay,  OfficeWorkingDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime=Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1=1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2=1, ModificationNumber=0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator"},
                                                   new OfficeScheduleViewModel{ PrmKey = 3, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule",  StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay,  OfficeWorkingDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime=Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1=1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2=1, ModificationNumber=0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator"},
                                                }.ToList();

            mock.Setup(m => m.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult<IEnumerable<OfficeScheduleViewModel>>(IndexOfUnVerifiedEntries));

            // Arrange - create the controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object);

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
            Mock<IOfficeScheduleRepository> mock = new Mock<IOfficeScheduleRepository>();

            var IndexOfVerifiedEntries = new OfficeScheduleViewModel[]
                                                {
                                                   new OfficeScheduleViewModel{  PrmKey = 1, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule",  StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay,  OfficeWorkingDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime=Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1=1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2=1, ModificationNumber=0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator"},
                                                   new OfficeScheduleViewModel{  PrmKey = 2, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule",  StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay,  OfficeWorkingDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime=Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1=1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2=1, ModificationNumber=0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator"},
                                                   new OfficeScheduleViewModel{  PrmKey = 3, OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), NameOfSchedule = "OfficeSchedule", AliasName = "OfficeSchedule", NameOnReport = "OfficeSchedule",  StartTime = Convert.ToDateTime("10:10 PM").TimeOfDay,  OfficeWorkingDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, MorningTeaTimeDuration= Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTime=Convert.ToDateTime("10:10 PM").TimeOfDay, LunchTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTime=Convert.ToDateTime("10:10 PM").TimeOfDay, EveningTeaTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTime=Convert.ToDateTime("10:10 PM").TimeOfDay, DinnerTimeDuration=Convert.ToDateTime("10:10 PM").TimeOfDay, WeeklyHoliday1=1, WeeklyHoliday1Occurance = "ALL", WeeklyHoliday2=1, ModificationNumber=0, WeeklyHoliday2Occurance = "ALL", Note = "None", IsModified = false, ReasonForModification = "Data Entry Mistake", EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", OfficeScheduleModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  EntryDateTime = new DateTime(01/01/2020), OfficeSchedulePrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", OfficeScheduleTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), LanguagePrmKey = 2, TransNameOfSchedule = "कार्यालीन वेळापत्रक", TransAliasName = "कार्यालीन वेळापत्रक", TransNameOnReport = "कार्यालीन वेळापत्रक", TransNote = "टिप", TransReasonForModification = "डेटा एंट्री चूक", OfficeScheduleTranslationPrmKey = 1, NameOfUser = "Administrator"},
                                                }.ToList();

            mock.Setup(m => m.GetIndexOfVerifiedEntries()).Returns(Task.FromResult<IEnumerable<OfficeScheduleViewModel>>(IndexOfVerifiedEntries));

            // Arrange - create the controller
            OfficeScheduleController target = new OfficeScheduleController(mock.Object);

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
