using DemoProject.Services.Abstract.Master.Address;
using DemoProject.Services.Abstract.Parameter.Master;
using DemoProject.Services.ViewModel.Master.Address;
using DemoProject.WebUI.Controllers;
using DemoProject.WebUI.Infrastructure.CustomException;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.UnitTests.Controllers
{
    [TestClass]
    public class DivisionControllerTest
    {
        [TestMethod]
        public async Task Can_Amend_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Division
                DivisionViewModel divisionViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(divisionViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The Division 
                ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The Division
                DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "AMN", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "AMN", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.Amend(divisionViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The Division
                ActionResult actionResult = await target.Amend(divisionViewModel, "Amend");

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.Amend(divisionViewModel));

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
            // Arrange - Create The Division
            DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "AMN", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "AMN", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            mockDivision.Setup(p => p.Amend(divisionViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Amend The Division
            ActionResult actionResult = await target.Amend(divisionViewModel, "Amend");

            // Assert - Check That The Repository Was Called
            mockDivision.Verify(m => m.Amend(divisionViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task Can_Create_Post_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Division
                DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.Save(divisionViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Create The Division
                ActionResult actionResult = await target.Create(divisionViewModel);

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.Save(divisionViewModel));

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
            // Arrange - Create The Division
            DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            mockDivision.Setup(p => p.Save(divisionViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Save The Division
            ActionResult actionResult = await target.Create(divisionViewModel);

            // Assert - Check That The Repository Was Called
            mockDivision.Verify(m => m.Save(divisionViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Delete_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Division
                DivisionViewModel divisionViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(divisionViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Delete The Division
                ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The Division
                DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "DEL", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "DEL", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.Delete(divisionViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Delete The Division
                ActionResult actionResult = await target.Amend(divisionViewModel, "Delete");

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.Delete(divisionViewModel));

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
            // Arrange - Create The Division
            DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "DEL", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "DEL", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            mockDivision.Setup(p => p.Delete(divisionViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
            {
                ControllerContext = mockControllerContext.Object
            };
            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Delete The Division
            ActionResult actionResult = await target.Amend(divisionViewModel, "Delete");

            // Assert - Check That The Repository Was Called
            mockDivision.Verify(m => m.Delete(divisionViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task Can_Modify_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Division
                DivisionViewModel divisionViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(divisionViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Modify The Division
                ActionResult actionResult = await target.Modify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The Division
                DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "MDF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "MDF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.Modify(divisionViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Modify The Division
                ActionResult actionResult = await target.Modify(divisionViewModel);

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.Modify(divisionViewModel));

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
            // Arrange - Create The Division
            DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "MDF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "MDF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            mockDivision.Setup(p => p.Modify(divisionViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Modify The Division
            ActionResult actionResult = await target.Modify(divisionViewModel);

            // Assert - Check That The Repository Was Called
            mockDivision.Verify(m => m.Modify(divisionViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Reject_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Division
                DivisionViewModel divisionViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.GetUnverifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(divisionViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Reject The Division
                ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.GetUnverifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The Division
                DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.Reject(divisionViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Reject The Division
                ActionResult actionResult = await target.Verify(divisionViewModel, "Reject");

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.Reject(divisionViewModel));

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
            // Arrange - Create The Division
            DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            mockDivision.Setup(p => p.Reject(divisionViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
            {
                ControllerContext = mockControllerContext.Object
            };
            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Reject The Division
            ActionResult actionResult = await target.Verify(divisionViewModel, "Reject");

            // Assert - Check That The Repository Was Called
            mockDivision.Verify(m => m.Reject(divisionViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_RejectedIndex_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The Division
                IEnumerable<DivisionViewModel> divisionViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.GetIndexOfRejectedEntries()).Returns(Task.FromResult(divisionViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Reject The Division
                ActionResult actionResult = await target.RejectedIndex();

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.GetIndexOfRejectedEntries());

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
                // Arrange - Create The Division
                IEnumerable<DivisionViewModel> divisionViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult(divisionViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Division
                ActionResult actionResult = await target.UnverifiedIndex();

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.GetIndexOfUnVerifiedEntries());

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
                // Arrange - Create The Division
                IEnumerable<DivisionViewModel> divisionViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.GetIndexOfVerifiedEntries()).Returns(Task.FromResult(divisionViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Division
                ActionResult actionResult = await target.VerifiedIndex();

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.GetIndexOfVerifiedEntries());

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
                // Arrange - Create The Division
                DivisionViewModel divisionViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.GetUnverifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(divisionViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Division
                ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.GetUnverifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The Division
                DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "VRF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

                mockDivision.Setup(p => p.Verify(divisionViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The Division
                ActionResult actionResult = await target.Verify(divisionViewModel, "Verify");

                // Assert - Check That The Repository Was Called
                mockDivision.Verify(m => m.Verify(divisionViewModel));

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
            // Arrange - Create The Division
            DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "VRF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            mockDivision.Setup(p => p.Verify(divisionViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Verify The Division
            ActionResult actionResult = await target.Verify(divisionViewModel, "Verify");

            // Assert - Check That The Repository Was Called
            mockDivision.Verify(m => m.Verify(divisionViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task Cannot_Amend_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            // Arrange - Create The controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object);

            // Arrange - Create The Division
            DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Amend The Division
            ActionResult actionResult = await target.Amend(divisionViewModel, "Amend");

            // Assert - Check That The Repository Was Not Called
            mockDivision.Verify(m => m.Amend(It.IsAny<DivisionViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task Cannot_Create_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            // Arrange - Create The controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object);

            // Arrange - Create The Division
            DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Save The Division
            ActionResult actionResult = await target.Create(divisionViewModel);

            // Assert - Check That The Repository Was Not Called
            mockDivision.Verify(m => m.Save(It.IsAny<DivisionViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Delete_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            // Arrange - Create The controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object);

            // Arrange - Create The Division
            DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "DEL", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "DEL", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Delete The Division
            ActionResult actionResult = await target.Amend(divisionViewModel, "Delete");

            // Assert - Check That The Repository Was Not Called
            mockDivision.Verify(m => m.Delete(It.IsAny<DivisionViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task Cannot_Modify_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            // Arrange - Create The controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object);

            // Arrange - Create The Division
            DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "MDF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "MDF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Modify The Division
            ActionResult actionResult = await target.Modify(divisionViewModel);

            // Assert - Check That The Repository Was Not Called
            mockDivision.Verify(m => m.Modify(It.IsAny<DivisionViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Reject_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            // Arrange - Create The controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object);

            // Arrange - Create The Division
            DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Reject The Division
            ActionResult actionResult = await target.Verify(divisionViewModel, "Reject");

            // Assert - Check That The Repository Was Not Called
            mockDivision.Verify(m => m.Reject(It.IsAny<DivisionViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task Cannot_Verify_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            // Arrange - Create The controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object);

            // Arrange - Create The Division
            DivisionViewModel divisionViewModel = new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "VRF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Verify The Division
            ActionResult actionResult = await target.Verify(divisionViewModel, "Verify");

            // Assert - Check That The Repository Was Not Called
            mockDivision.Verify(m => m.Verify(It.IsAny<DivisionViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task RejectedIndex_Contains_AllRejected_Entries()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            var IndexOfRejectedEntries = new DivisionViewModel[]
                                                {
                                                   new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "REJ", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
                                                   new DivisionViewModel { PrmKey = 2, NameOfCenter = "Nagpur", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Nagpur", OtherNameAsParent = "None", NameOnReport = "Nagpur", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "REJ", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "नागपूर", TransAliasName = "नागपूर", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "नागपूर", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "User", MakerEntryDateTime = DateTime.Now },
                                                   new DivisionViewModel { PrmKey = 3, NameOfCenter = "Amravati", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Amravati", OtherNameAsParent = "None", NameOnReport = "Amravati", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "REJ", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "अमरावती", TransAliasName = "अमरावती", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "अमरावती", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Super User", MakerEntryDateTime = DateTime.Now }
                                                }.ToList();

            mockDivision.Setup(m => m.GetIndexOfRejectedEntries()).Returns(Task.FromResult<IEnumerable<DivisionViewModel>>(IndexOfRejectedEntries));

            // Arrange - create the controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object);

            // Action -target the controller
            var result = await target.RejectedIndex() as ViewResult;

            // Assert 
            Assert.AreEqual(IndexOfRejectedEntries.Count, 3);
            Assert.AreEqual("REJ", IndexOfRejectedEntries[0].EntryStatus);
            Assert.AreEqual("REJ", IndexOfRejectedEntries[1].EntryStatus);
            Assert.AreEqual("REJ", IndexOfRejectedEntries[2].EntryStatus);
        }

        [TestMethod]
        public async Task UnverifiedIndex_Contains_AllUnverified_Entries()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            var IndexOfUnverifiedEntries = new DivisionViewModel[]
                                                 {
                                                   new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
                                                   new DivisionViewModel { PrmKey = 2, NameOfCenter = "Nagpur", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Nagpur", OtherNameAsParent = "None", NameOnReport = "Nagpur", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "नागपूर", TransAliasName = "नागपूर", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "नागपूर", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "User", MakerEntryDateTime = DateTime.Now },
                                                   new DivisionViewModel { PrmKey = 3, NameOfCenter = "Amravati", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Amravati", OtherNameAsParent = "None", NameOnReport = "Amravati", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "अमरावती", TransAliasName = "अमरावती", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "अमरावती", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Super User", MakerEntryDateTime = DateTime.Now }
                                                }.ToList();

            mockDivision.Setup(m => m.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult<IEnumerable<DivisionViewModel>>(IndexOfUnverifiedEntries));

            // Arrange - create the controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object);

            // Action -target the controller
            var result = await target.UnverifiedIndex() as ViewResult;

            // Assert 
            Assert.AreEqual(IndexOfUnverifiedEntries.Count, 3);
            Assert.AreEqual("CRT", IndexOfUnverifiedEntries[0].EntryStatus);
            Assert.AreEqual("CRT", IndexOfUnverifiedEntries[1].EntryStatus);
            Assert.AreEqual("CRT", IndexOfUnverifiedEntries[2].EntryStatus);
        }

        [TestMethod]
        public async Task VerifiedIndex_Contains_AllVerified_Entries()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();

            var IndexOfVerifiedEntries = new DivisionViewModel[]
                                                 {
                                                   new DivisionViewModel { PrmKey = 1, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "None", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
                                                   new DivisionViewModel { PrmKey = 2, NameOfCenter = "Nagpur", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Nagpur", OtherNameAsParent = "None", NameOnReport = "Nagpur", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "नागपूर", TransAliasName = "नागपूर", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "नागपूर", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "User", MakerEntryDateTime = DateTime.Now },
                                                   new DivisionViewModel { PrmKey = 3, NameOfCenter = "Amravati", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Amravati", OtherNameAsParent = "None", NameOnReport = "Amravati", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "अमरावती", TransAliasName = "अमरावती", TransOtherNameAsParent = "काहीही नाही", TransNameOnReport = "अमरावती", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Super User", MakerEntryDateTime = DateTime.Now }
                                                }.ToList();

            mockDivision.Setup(m => m.GetIndexOfVerifiedEntries()).Returns(Task.FromResult<IEnumerable<DivisionViewModel>>(IndexOfVerifiedEntries));

            // Arrange - create the controller
            DivisionController target = new DivisionController(mockAddressParametr.Object, mockCenter.Object, mockDivision.Object);

            // Action -target the controller
            var result = await target.VerifiedIndex() as ViewResult;

            // Assert 
            Assert.AreEqual(IndexOfVerifiedEntries.Count, 3);
            Assert.AreEqual("VRF", IndexOfVerifiedEntries[0].EntryStatus);
            Assert.AreEqual("VRF", IndexOfVerifiedEntries[1].EntryStatus);
            Assert.AreEqual("VRF", IndexOfVerifiedEntries[2].EntryStatus);
        }
    }
}