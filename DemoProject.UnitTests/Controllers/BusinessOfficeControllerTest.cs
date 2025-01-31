using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.WebUI.Controllers;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Constants;

namespace DemoProject.UnitTests.Controllers
{
    [TestClass]
    public class BusinessOfficeControllerTest
    { 
    //{
    //    [TestMethod]
    //    public async Task Can_Amend_Get_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The   BusinessOffice
    //            BusinessOfficeViewModel businessOfficeViewModel = null;

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(businessOfficeViewModel));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Amend The BusinessOffice 
    //            ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_Amend_Post_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "ABC",
    //                AlternateBusinessOfficeCode = "Abc123",
    //                NameOfBusinessOffice = "Dahiwadi Branch",
    //                AliasName = "Dahiwadi Branch",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //                NameOnReport = "Dahiwadi Branch",
    //                OpeningDate = new DateTime(01 / 01 / 2020),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Create,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Two",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Fifty",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "8421591819",
    //                AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 23,
    //                PopulationOfTheCommandArea = 230,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "8421591918",
    //                TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MNP102312",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "GJP199865",
    //                UniformBusinessOfficeCode1 = 21,
    //                UniformBusinessOfficeCode2 = 31,
    //                BusinessOfficeCodeByRBI = 51,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP021",
    //                AlphaNumericTelexAddress = "MNCD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "दहीवडी शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवालासाठी नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "Administrator",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            };

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.Amend(businessOfficeViewModel)).Returns(Task.FromResult(false));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Amend The BusinessOffice
    //            ActionResult actionResult = await target.Amend(businessOfficeViewModel, "Amend");

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.Amend(businessOfficeViewModel));

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_Amend_Valid_Entry()
    //    {
    //        // Arrange - Create The BusinessOffice
    //        BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //        {
    //            PrmKey = 1,
    //            BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCode = "ABC",
    //            AlternateBusinessOfficeCode = "Abc123",
    //            NameOfBusinessOffice = "Dahiwadi Branch",
    //            AliasName = "Public",
    //            NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //            NameOnReport = "Public Accounts",
    //            OpeningDate = new DateTime(01 / 01 / 2020),
    //            CloseDate = null,
    //            Note = "None",
    //            BusinessOfficeStatusForCoreOperation = "1",
    //            IsModified = false,
    //            EntryStatus = StringLiteralValue.Create,
    //            ActivationStatus = "I",
    //            BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ApprovalDate = new DateTime(01 / 01 / 2021),
    //            RegistrationDate = new DateTime(01 / 02 / 2021),
    //            RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            ReferenceNumber = "Two",
    //            CoopNumericCode = 2,
    //            CoopAlphaNumericCode = "Fifty",
    //            BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCoopRegistrationPrmKey = 1,
    //            LanguagePrmKey = 2,
    //            TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            TransReferenceNumber = "दोन",
    //            TransCoopAlphaNumericCode = "पन्नास",
    //            TransNote = "काहीही नाही",
    //            TransReasonForModification = "डेटा एंट्री चूक",
    //            BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ContactDetails = "8421591819",
    //            AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //            CenterPrmKey = 1,
    //            OfficeSchedulePrmKey = 1,
    //            BusinessOfficeTypePrmKey = 1,
    //            BusinessNaturePrmKey = 1,
    //            RegionalLanguagePrmKey = 1,
    //            CommandAreaRadius = 23,
    //            PopulationOfTheCommandArea = 230,
    //            GeneralLedgerPrmKey = 1,
    //            ParentBusinessOfficePrmKey = 0,
    //            ClearingBusinessOfficePrmKey = 0,
    //            DefaultCurrencyPrmKey = 1,
    //            BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeDetailPrmKey = 1,
    //            TransContactDetails = "8421591918",
    //            TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //            BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ModificationNumber = 0,
    //            ReasonForModification = "Data Entry Mistake",
    //            BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RBIReferenceNumber = "MNP102312",
    //            RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //            RBILicenseNumber = "GJP199865",
    //            UniformBusinessOfficeCode1 = 21,
    //            UniformBusinessOfficeCode2 = 31,
    //            BusinessOfficeCodeByRBI = 51,
    //            MICRCode = 1221,
    //            IFSCCode = "SBIN000452",
    //            AlphaNumericSWIFTAddress = "ITBP021",
    //            AlphaNumericTelexAddress = "MNCD100",
    //            BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //            BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeRBIRegistrationPrmKey = 1,
    //            TransRBIReferenceNumber = "POK1551",
    //            TransAlphaNumericSWIFTAddress = "MTBL0100",
    //            TransAlphaNumericTelexAddress = "OACD200",
    //            TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //            BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            TransModificationNumber = 2,
    //            TransNameOfBusinessOffice = "दहीवडी शाखा",
    //            TransAliasName = "उर्फनाव",
    //            TransNameOnReport = "अहवालासाठी नाव",
    //            EntryDateTime = DateTime.Now,
    //            UserProfilePrmKey = 0,
    //            UserAction = StringLiteralValue.Create,
    //            Remark = "None",
    //            BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //            BusinessOfficeModificationPrmKey = 2,
    //            BusinessOfficeDetailTranslationPrmKey = 3,
    //            BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //            BusinessOfficeTranslationPrmKey = 5,
    //            NameOfUser = "Administrator",
    //            MakerEntryDateTime = DateTime.Now,
    //            CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficePrmKey = 1
    //        };

    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        mock.Setup(p => p.Amend(businessOfficeViewModel)).Returns(Task.FromResult(true));

    //        var mockControllerContext = new Mock<ControllerContext>();
    //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //        // Arrange - Create The controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //        {
    //            ControllerContext = mockControllerContext.Object
    //        };

    //        // Act - Try To Amend The BusinessOffice
    //        ActionResult actionResult = await target.Amend(businessOfficeViewModel, "Amend");

    //        // Assert - Check That The Repository Was Called
    //        mock.Verify(m => m.Amend(businessOfficeViewModel));

    //        // Assert - Check That The Method Result Type
    //        Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
    //    }

    //    [TestMethod]
    //    public async Task Can_Create_Post_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "ABC",
    //                AlternateBusinessOfficeCode = "Abc123",
    //                NameOfBusinessOffice = "Dahiwadi Branch",
    //                AliasName = "Public",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //                NameOnReport = "Public Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2020),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Create,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Two",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Fifty",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "8421591819",
    //                AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 23,
    //                PopulationOfTheCommandArea = 230,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "8421591918",
    //                TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MNP102312",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "GJP199865",
    //                UniformBusinessOfficeCode1 = 21,
    //                UniformBusinessOfficeCode2 = 31,
    //                BusinessOfficeCodeByRBI = 51,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP021",
    //                AlphaNumericTelexAddress = "MNCD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "दहीवडी शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवालासाठी नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "Administrator",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            };

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.Save(businessOfficeViewModel)).Returns(Task.FromResult(false));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Create The BusinessOffice
    //            ActionResult actionResult = await target.Create(businessOfficeViewModel);

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.Save(businessOfficeViewModel));

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_Create_Valid_Entry()
    //    {
    //        // Arrange - Create The BusinessOffice
    //        BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //        {
    //            PrmKey = 1,
    //            BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCode = "ABC",
    //            AlternateBusinessOfficeCode = "Abc123",
    //            NameOfBusinessOffice = "Dahiwadi Branch",
    //            AliasName = "Public",
    //            NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //            NameOnReport = "Public Accounts",
    //            OpeningDate = new DateTime(01 / 01 / 2020),
    //            CloseDate = null,
    //            Note = "None",
    //            BusinessOfficeStatusForCoreOperation = "1",
    //            IsModified = false,
    //            EntryStatus = StringLiteralValue.Create,
    //            ActivationStatus = "I",
    //            BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ApprovalDate = new DateTime(01 / 01 / 2021),
    //            RegistrationDate = new DateTime(01 / 02 / 2021),
    //            RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            ReferenceNumber = "Two",
    //            CoopNumericCode = 2,
    //            CoopAlphaNumericCode = "Fifty",
    //            BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCoopRegistrationPrmKey = 1,
    //            LanguagePrmKey = 2,
    //            TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            TransReferenceNumber = "दोन",
    //            TransCoopAlphaNumericCode = "पन्नास",
    //            TransNote = "काहीही नाही",
    //            TransReasonForModification = "डेटा एंट्री चूक",
    //            BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ContactDetails = "8421591819",
    //            AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //            CenterPrmKey = 1,
    //            OfficeSchedulePrmKey = 1,
    //            BusinessOfficeTypePrmKey = 1,
    //            BusinessNaturePrmKey = 1,
    //            RegionalLanguagePrmKey = 1,
    //            CommandAreaRadius = 23,
    //            PopulationOfTheCommandArea = 230,
    //            GeneralLedgerPrmKey = 1,
    //            ParentBusinessOfficePrmKey = 0,
    //            ClearingBusinessOfficePrmKey = 0,
    //            DefaultCurrencyPrmKey = 1,
    //            BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeDetailPrmKey = 1,
    //            TransContactDetails = "8421591918",
    //            TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //            BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ModificationNumber = 0,
    //            ReasonForModification = "Data Entry Mistake",
    //            BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RBIReferenceNumber = "MNP102312",
    //            RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //            RBILicenseNumber = "GJP199865",
    //            UniformBusinessOfficeCode1 = 21,
    //            UniformBusinessOfficeCode2 = 31,
    //            BusinessOfficeCodeByRBI = 51,
    //            MICRCode = 1221,
    //            IFSCCode = "SBIN000452",
    //            AlphaNumericSWIFTAddress = "ITBP021",
    //            AlphaNumericTelexAddress = "MNCD100",
    //            BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //            BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeRBIRegistrationPrmKey = 1,
    //            TransRBIReferenceNumber = "POK1551",
    //            TransAlphaNumericSWIFTAddress = "MTBL0100",
    //            TransAlphaNumericTelexAddress = "OACD200",
    //            TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //            BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            TransModificationNumber = 2,
    //            TransNameOfBusinessOffice = "दहीवडी शाखा",
    //            TransAliasName = "उर्फनाव",
    //            TransNameOnReport = "अहवालासाठी नाव",
    //            EntryDateTime = DateTime.Now,
    //            UserProfilePrmKey = 0,
    //            UserAction = StringLiteralValue.Create,
    //            Remark = "None",
    //            BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //            BusinessOfficeModificationPrmKey = 2,
    //            BusinessOfficeDetailTranslationPrmKey = 3,
    //            BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //            BusinessOfficeTranslationPrmKey = 5,
    //            NameOfUser = "Administrator",
    //            MakerEntryDateTime = DateTime.Now,
    //            CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficePrmKey = 1
    //        };

    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        mock.Setup(p => p.Save(businessOfficeViewModel)).Returns(Task.FromResult(true));

    //        var mockControllerContext = new Mock<ControllerContext>();
    //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

    //        // Arrange - Create The controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //        {
    //            ControllerContext = mockControllerContext.Object
    //        };

    //        // Act - Try To Save The BusinessOffice
    //        ActionResult actionResult = await target.Create(businessOfficeViewModel);

    //        // Assert - Check That The Repository Was Called
    //        mock.Verify(m => m.Save(businessOfficeViewModel));

    //        // Assert - Check That The Method Result Type
    //        Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
    //    }

    //    [TestMethod]
    //    public async Task Can_Delete_Get_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            BusinessOfficeViewModel businessOfficeViewModel = null;

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(businessOfficeViewModel));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Delete The BusinessOffice
    //            ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_Delete_Post_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "ABC",
    //                AlternateBusinessOfficeCode = "Abc123",
    //                NameOfBusinessOffice = "Dahiwadi Branch",
    //                AliasName = "Public",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //                NameOnReport = "Public Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2020),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Delete,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Two",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Fifty",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "8421591819",
    //                AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 23,
    //                PopulationOfTheCommandArea = 230,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "8421591918",
    //                TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MNP102312",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "GJP199865",
    //                UniformBusinessOfficeCode1 = 21,
    //                UniformBusinessOfficeCode2 = 31,
    //                BusinessOfficeCodeByRBI = 51,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP021",
    //                AlphaNumericTelexAddress = "MNCD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "दहीवडी शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवालासाठी नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "Administrator",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            };

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.Delete(businessOfficeViewModel)).Returns(Task.FromResult(false));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Delete The BusinessOffice
    //            ActionResult actionResult = await target.Amend(businessOfficeViewModel, "Delete");

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.Delete(businessOfficeViewModel));

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_Delete_Valid_Entry()
    //    {
    //        // Arrange - Create The BusinessOffice
    //        BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //        {
    //            PrmKey = 1,
    //            BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCode = "ABC",
    //            AlternateBusinessOfficeCode = "Abc123",
    //            NameOfBusinessOffice = "Dahiwadi Branch",
    //            AliasName = "Public",
    //            NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //            NameOnReport = "Public Accounts",
    //            OpeningDate = new DateTime(01 / 01 / 2020),
    //            CloseDate = null,
    //            Note = "None",
    //            BusinessOfficeStatusForCoreOperation = "1",
    //            IsModified = false,
    //            EntryStatus = StringLiteralValue.Delete,
    //            ActivationStatus = "I",
    //            BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ApprovalDate = new DateTime(01 / 01 / 2021),
    //            RegistrationDate = new DateTime(01 / 02 / 2021),
    //            RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            ReferenceNumber = "Two",
    //            CoopNumericCode = 2,
    //            CoopAlphaNumericCode = "Fifty",
    //            BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCoopRegistrationPrmKey = 1,
    //            LanguagePrmKey = 2,
    //            TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            TransReferenceNumber = "दोन",
    //            TransCoopAlphaNumericCode = "पन्नास",
    //            TransNote = "काहीही नाही",
    //            TransReasonForModification = "डेटा एंट्री चूक",
    //            BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ContactDetails = "8421591819",
    //            AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //            CenterPrmKey = 1,
    //            OfficeSchedulePrmKey = 1,
    //            BusinessOfficeTypePrmKey = 1,
    //            BusinessNaturePrmKey = 1,
    //            RegionalLanguagePrmKey = 1,
    //            CommandAreaRadius = 23,
    //            PopulationOfTheCommandArea = 230,
    //            GeneralLedgerPrmKey = 1,
    //            ParentBusinessOfficePrmKey = 0,
    //            ClearingBusinessOfficePrmKey = 0,
    //            DefaultCurrencyPrmKey = 1,
    //            BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeDetailPrmKey = 1,
    //            TransContactDetails = "8421591918",
    //            TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //            BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ModificationNumber = 0,
    //            ReasonForModification = "Data Entry Mistake",
    //            BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RBIReferenceNumber = "MNP102312",
    //            RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //            RBILicenseNumber = "GJP199865",
    //            UniformBusinessOfficeCode1 = 21,
    //            UniformBusinessOfficeCode2 = 31,
    //            BusinessOfficeCodeByRBI = 51,
    //            MICRCode = 1221,
    //            IFSCCode = "SBIN000452",
    //            AlphaNumericSWIFTAddress = "ITBP021",
    //            AlphaNumericTelexAddress = "MNCD100",
    //            BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //            BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeRBIRegistrationPrmKey = 1,
    //            TransRBIReferenceNumber = "POK1551",
    //            TransAlphaNumericSWIFTAddress = "MTBL0100",
    //            TransAlphaNumericTelexAddress = "OACD200",
    //            TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //            BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            TransModificationNumber = 2,
    //            TransNameOfBusinessOffice = "दहीवडी शाखा",
    //            TransAliasName = "उर्फनाव",
    //            TransNameOnReport = "अहवालासाठी नाव",
    //            EntryDateTime = DateTime.Now,
    //            UserProfilePrmKey = 0,
    //            UserAction = StringLiteralValue.Delete,
    //            Remark = "None",
    //            BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //            BusinessOfficeModificationPrmKey = 2,
    //            BusinessOfficeDetailTranslationPrmKey = 3,
    //            BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //            BusinessOfficeTranslationPrmKey = 5,
    //            NameOfUser = "Administrator",
    //            MakerEntryDateTime = DateTime.Now,
    //            CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficePrmKey = 1
    //        };

    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        mock.Setup(p => p.Delete(businessOfficeViewModel)).Returns(Task.FromResult(true));

    //        var mockControllerContext = new Mock<ControllerContext>();
    //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //        // Arrange - Create The controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //        {
    //            ControllerContext = mockControllerContext.Object
    //        };
    //        var mockUrlHelper = new Mock<UrlHelper>();
    //        mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
    //        target.Url = mockUrlHelper.Object;

    //        // Act - Try To Delete The BusinessOffice
    //        ActionResult actionResult = await target.Amend(businessOfficeViewModel, "Delete");

    //        // Assert - Check That The Repository Was Called
    //        mock.Verify(m => m.Delete(businessOfficeViewModel));

    //        // Assert - Check That The Method Result Type
    //        Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
    //    }

    //    [TestMethod]
    //    public async Task Can_Modify_Get_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            BusinessOfficeViewModel businessOfficeViewModel = null;

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(businessOfficeViewModel));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Modify The BusinessOffice
    //            ActionResult actionResult = await target.Modify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_Modify_Post_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "ABC",
    //                AlternateBusinessOfficeCode = "Abc123",
    //                NameOfBusinessOffice = "Dahiwadi Branch",
    //                AliasName = "Public",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //                NameOnReport = "Public Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2020),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Create,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Two",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Fifty",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "8421591819",
    //                AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 23,
    //                PopulationOfTheCommandArea = 230,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "8421591918",
    //                TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MNP102312",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "GJP199865",
    //                UniformBusinessOfficeCode1 = 21,
    //                UniformBusinessOfficeCode2 = 31,
    //                BusinessOfficeCodeByRBI = 51,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP021",
    //                AlphaNumericTelexAddress = "MNCD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "दहीवडी शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवालासाठी नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "Administrator",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            };

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.Modify(businessOfficeViewModel)).Returns(Task.FromResult(false));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Modify The BusinessOffice
    //            ActionResult actionResult = await target.Modify(businessOfficeViewModel);

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.Modify(businessOfficeViewModel));

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_Modify_Valid_Entry()
    //    {
    //        // Arrange - Create The BusinessOffice
    //        BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //        {
    //            PrmKey = 1,
    //            BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCode = "ABC",
    //            AlternateBusinessOfficeCode = "Abc123",
    //            NameOfBusinessOffice = "Dahiwadi Branch",
    //            AliasName = "Dahiwadi Branch",
    //            NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //            NameOnReport = "Public Accounts",
    //            OpeningDate = new DateTime(01 / 01 / 2020),
    //            CloseDate = null,
    //            Note = "None",
    //            BusinessOfficeStatusForCoreOperation = "1",
    //            IsModified = false,
    //            EntryStatus = StringLiteralValue.Create,
    //            ActivationStatus = "I",
    //            BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ApprovalDate = new DateTime(01 / 01 / 2021),
    //            RegistrationDate = new DateTime(01 / 02 / 2021),
    //            RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            ReferenceNumber = "Two",
    //            CoopNumericCode = 2,
    //            CoopAlphaNumericCode = "Fifty",
    //            BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCoopRegistrationPrmKey = 1,
    //            LanguagePrmKey = 2,
    //            TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            TransReferenceNumber = "दोन",
    //            TransCoopAlphaNumericCode = "पन्नास",
    //            TransNote = "काहीही नाही",
    //            TransReasonForModification = "डेटा एंट्री चूक",
    //            BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ContactDetails = "8421591819",
    //            AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //            CenterPrmKey = 1,
    //            OfficeSchedulePrmKey = 1,
    //            BusinessOfficeTypePrmKey = 1,
    //            BusinessNaturePrmKey = 1,
    //            RegionalLanguagePrmKey = 1,
    //            CommandAreaRadius = 23,
    //            PopulationOfTheCommandArea = 230,
    //            GeneralLedgerPrmKey = 1,
    //            ParentBusinessOfficePrmKey = 0,
    //            ClearingBusinessOfficePrmKey = 0,
    //            DefaultCurrencyPrmKey = 1,
    //            BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeDetailPrmKey = 1,
    //            TransContactDetails = "8421591918",
    //            TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //            BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ModificationNumber = 0,
    //            ReasonForModification = "Data Entry Mistake",
    //            BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RBIReferenceNumber = "MNP102312",
    //            RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //            RBILicenseNumber = "GJP199865",
    //            UniformBusinessOfficeCode1 = 21,
    //            UniformBusinessOfficeCode2 = 31,
    //            BusinessOfficeCodeByRBI = 51,
    //            MICRCode = 1221,
    //            IFSCCode = "SBIN000452",
    //            AlphaNumericSWIFTAddress = "ITBP021",
    //            AlphaNumericTelexAddress = "MNCD100",
    //            BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //            BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeRBIRegistrationPrmKey = 1,
    //            TransRBIReferenceNumber = "POK1551",
    //            TransAlphaNumericSWIFTAddress = "MTBL0100",
    //            TransAlphaNumericTelexAddress = "OACD200",
    //            TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //            BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            TransModificationNumber = 2,
    //            TransNameOfBusinessOffice = "दहीवडी शाखा",
    //            TransAliasName = "उर्फनाव",
    //            TransNameOnReport = "अहवालासाठी नाव",
    //            EntryDateTime = DateTime.Now,
    //            UserProfilePrmKey = 0,
    //            UserAction = StringLiteralValue.Create,
    //            Remark = "None",
    //            BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //            BusinessOfficeModificationPrmKey = 2,
    //            BusinessOfficeDetailTranslationPrmKey = 3,
    //            BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //            BusinessOfficeTranslationPrmKey = 5,
    //            NameOfUser = "Administrator",
    //            MakerEntryDateTime = DateTime.Now,
    //            CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficePrmKey = 1
    //        };

    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        mock.Setup(p => p.Modify(businessOfficeViewModel)).Returns(Task.FromResult(true));

    //        var mockControllerContext = new Mock<ControllerContext>();
    //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //        // Arrange - Create The controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //        {
    //            ControllerContext = mockControllerContext.Object
    //        };

    //        // Act - Try To Modify The BusinessOffice
    //        ActionResult actionResult = await target.Modify(businessOfficeViewModel);

    //        // Assert - Check That The Repository Was Called
    //        mock.Verify(m => m.Modify(businessOfficeViewModel));

    //        // Assert - Check That The Method Result Type
    //        Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
    //    }

    //    [TestMethod]
    //    public async Task Can_Reject_Get_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            BusinessOfficeViewModel businessOfficeViewModel = null;

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(businessOfficeViewModel));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Reject The BusinessOffice
    //            ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_Reject_Post_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "ABC",
    //                AlternateBusinessOfficeCode = "Abc123",
    //                NameOfBusinessOffice = "Dahiwadi Branch",
    //                AliasName = "Dahiwadi Branch",
    //                NameOfBusinessOfficeForThirdPartyInterface = "दहीवडी शाखा",
    //                NameOnReport = "Public Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2020),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Reject,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Two",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Fifty",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "8421591819",
    //                AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 23,
    //                PopulationOfTheCommandArea = 230,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "8421591918",
    //                TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MNP102312",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "GJP199865",
    //                UniformBusinessOfficeCode1 = 21,
    //                UniformBusinessOfficeCode2 = 31,
    //                BusinessOfficeCodeByRBI = 51,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP021",
    //                AlphaNumericTelexAddress = "MNCD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "दहीवडी शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवालासाठी नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Reject,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "Administrator",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            };

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.Reject(businessOfficeViewModel)).Returns(Task.FromResult(false));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Reject The BusinessOffice
    //            ActionResult actionResult = await target.Verify(businessOfficeViewModel, "Reject");

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.Reject(businessOfficeViewModel));

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_Reject_Valid_Entry()
    //    {
    //        // Arrange - Create The BusinessOffice
    //        BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //        {
    //            PrmKey = 1,
    //            BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCode = "ABC",
    //            AlternateBusinessOfficeCode = "Abc123",
    //            NameOfBusinessOffice = "Dahiwadi Branch",
    //            AliasName = "Dahiwadi Branch",
    //            NameOfBusinessOfficeForThirdPartyInterface = "दहीवडी शाखा",
    //            NameOnReport = "Public Accounts",
    //            OpeningDate = new DateTime(01 / 01 / 2020),
    //            CloseDate = null,
    //            Note = "None",
    //            BusinessOfficeStatusForCoreOperation = "1",
    //            IsModified = false,
    //            EntryStatus = StringLiteralValue.Reject,
    //            ActivationStatus = "I",
    //            BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ApprovalDate = new DateTime(01 / 01 / 2021),
    //            RegistrationDate = new DateTime(01 / 02 / 2021),
    //            RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            ReferenceNumber = "Two",
    //            CoopNumericCode = 2,
    //            CoopAlphaNumericCode = "Fifty",
    //            BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCoopRegistrationPrmKey = 1,
    //            LanguagePrmKey = 2,
    //            TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            TransReferenceNumber = "दोन",
    //            TransCoopAlphaNumericCode = "पन्नास",
    //            TransNote = "काहीही नाही",
    //            TransReasonForModification = "डेटा एंट्री चूक",
    //            BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ContactDetails = "8421591819",
    //            AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //            CenterPrmKey = 1,
    //            OfficeSchedulePrmKey = 1,
    //            BusinessOfficeTypePrmKey = 1,
    //            BusinessNaturePrmKey = 1,
    //            RegionalLanguagePrmKey = 1,
    //            CommandAreaRadius = 23,
    //            PopulationOfTheCommandArea = 230,
    //            GeneralLedgerPrmKey = 1,
    //            ParentBusinessOfficePrmKey = 0,
    //            ClearingBusinessOfficePrmKey = 0,
    //            DefaultCurrencyPrmKey = 1,
    //            BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeDetailPrmKey = 1,
    //            TransContactDetails = "8421591918",
    //            TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //            BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ModificationNumber = 0,
    //            ReasonForModification = "Data Entry Mistake",
    //            BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RBIReferenceNumber = "MNP102312",
    //            RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //            RBILicenseNumber = "GJP199865",
    //            UniformBusinessOfficeCode1 = 21,
    //            UniformBusinessOfficeCode2 = 31,
    //            BusinessOfficeCodeByRBI = 51,
    //            MICRCode = 1221,
    //            IFSCCode = "SBIN000452",
    //            AlphaNumericSWIFTAddress = "ITBP021",
    //            AlphaNumericTelexAddress = "MNCD100",
    //            BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //            BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeRBIRegistrationPrmKey = 1,
    //            TransRBIReferenceNumber = "POK1551",
    //            TransAlphaNumericSWIFTAddress = "MTBL0100",
    //            TransAlphaNumericTelexAddress = "OACD200",
    //            TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //            BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            TransModificationNumber = 2,
    //            TransNameOfBusinessOffice = "दहीवडी शाखा",
    //            TransAliasName = "उर्फनाव",
    //            TransNameOnReport = "अहवालासाठी नाव",
    //            EntryDateTime = DateTime.Now,
    //            UserProfilePrmKey = 0,
    //            UserAction = StringLiteralValue.Create,
    //            Remark = "None",
    //            BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //            BusinessOfficeModificationPrmKey = 2,
    //            BusinessOfficeDetailTranslationPrmKey = 3,
    //            BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //            BusinessOfficeTranslationPrmKey = 5,
    //            NameOfUser = "Administrator",
    //            MakerEntryDateTime = DateTime.Now,
    //            CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficePrmKey = 1
    //        };

    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        mock.Setup(p => p.Reject(businessOfficeViewModel)).Returns(Task.FromResult(true));

    //        var mockControllerContext = new Mock<ControllerContext>();
    //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //        // Arrange - Create The controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //        {
    //            ControllerContext = mockControllerContext.Object
    //        };
    //        var mockUrlHelper = new Mock<UrlHelper>();
    //        mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
    //        target.Url = mockUrlHelper.Object;

    //        // Act - Try To Verify The BusinessOffice
    //        ActionResult actionResult = await target.Verify(businessOfficeViewModel, "Reject");

    //        // Assert - Check That The Repository Was Called
    //        mock.Verify(m => m.Reject(businessOfficeViewModel));

    //        // Assert - Check That The Method Result Type
    //        Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
    //    }

    //    [TestMethod]
    //    public async Task Can_RejectedIndex_Get_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModel = null;

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.GetIndexOfRejectedEntries()).Returns(Task.FromResult(businessOfficeViewModel));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Verify The BusinessOffice
    //            ActionResult actionResult = await target.RejectedIndex();

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.GetIndexOfRejectedEntries());

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_UnverifiedIndex_Get_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModel = null;

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult(businessOfficeViewModel));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Verify The BusinessOffice
    //            ActionResult actionResult = await target.UnverifiedIndex();

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.GetIndexOfUnVerifiedEntries());

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_VerifiedIndex_Get_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModel = null;

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.GetIndexOfVerifiedEntries()).Returns(Task.FromResult(businessOfficeViewModel));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Verify The BusinessOffice
    //            ActionResult actionResult = await target.VerifiedIndex();

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.GetIndexOfVerifiedEntries());

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_Verify_Get_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            BusinessOfficeViewModel businessOfficeViewModel = null;

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(businessOfficeViewModel));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Verify The BusinessOffice
    //            ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_Verify_Post_Method_Throws_Exception()
    //    {
    //        var expectedException = new DatabaseException();

    //        try
    //        {
    //            // Arrange - Create The BusinessOffice
    //            BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "ABC",
    //                AlternateBusinessOfficeCode = "Abc123",
    //                NameOfBusinessOffice = "Dahiwadi Branch",
    //                AliasName = "Dahiwadi Branch",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //                NameOnReport = "Public Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2020),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Create,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Two",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Fifty",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "8421591819",
    //                AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 23,
    //                PopulationOfTheCommandArea = 230,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "8421591918",
    //                TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MNP102312",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "GJP199865",
    //                UniformBusinessOfficeCode1 = 21,
    //                UniformBusinessOfficeCode2 = 31,
    //                BusinessOfficeCodeByRBI = 51,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP021",
    //                AlphaNumericTelexAddress = "MNCD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "दहीवडी शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवालासाठी नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "Administrator",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            };

    //            // Arrange - Create The Mock Repository
    //            Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //            Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //            mock.Setup(p => p.Verify(businessOfficeViewModel)).Returns(Task.FromResult(false));

    //            var mockControllerContext = new Mock<ControllerContext>();
    //            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //            // Arrange - Create The controller
    //            BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //            {
    //                ControllerContext = mockControllerContext.Object
    //            };

    //            // Act - Try To Verify The BusinessOffice
    //            ActionResult actionResult = await target.Verify(businessOfficeViewModel, "Verify");

    //            // Assert - Check That The Repository Was Called
    //            mock.Verify(m => m.Verify(businessOfficeViewModel));

    //            Assert.Fail("An exception was not thrown as expected.");
    //        }
    //        catch (Exception e)
    //        {
    //            // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
    //            if (e.GetType() == typeof(AssertFailedException)) throw;

    //            // Assert - Check That The Exception Type And Message
    //            Assert.AreEqual(expectedException.GetType(), e.GetType());
    //            Assert.AreEqual(expectedException.Message, e.Message);
    //        }
    //    }

    //    [TestMethod]
    //    public async Task Can_Verify_Valid_Entry()
    //    {
    //        // Arrange - Create The BusinessOffice
    //        BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //        {
    //            PrmKey = 1,
    //            BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCode = "ABC",
    //            AlternateBusinessOfficeCode = "Abc123",
    //            NameOfBusinessOffice = "Dahiwadi Branch",
    //            AliasName = "Dahiwadi Branch",
    //            NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //            NameOnReport = "Public Accounts",
    //            OpeningDate = new DateTime(01 / 01 / 2020),
    //            CloseDate = null,
    //            Note = "None",
    //            BusinessOfficeStatusForCoreOperation = "1",
    //            IsModified = false,
    //            EntryStatus = StringLiteralValue.Create,
    //            ActivationStatus = "I",
    //            BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ApprovalDate = new DateTime(01 / 01 / 2021),
    //            RegistrationDate = new DateTime(01 / 02 / 2021),
    //            RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            ReferenceNumber = "Two",
    //            CoopNumericCode = 2,
    //            CoopAlphaNumericCode = "Fifty",
    //            BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCoopRegistrationPrmKey = 1,
    //            LanguagePrmKey = 2,
    //            TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            TransReferenceNumber = "दोन",
    //            TransCoopAlphaNumericCode = "पन्नास",
    //            TransNote = "काहीही नाही",
    //            TransReasonForModification = "डेटा एंट्री चूक",
    //            BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ContactDetails = "8421591819",
    //            AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //            CenterPrmKey = 1,
    //            OfficeSchedulePrmKey = 1,
    //            BusinessOfficeTypePrmKey = 1,
    //            BusinessNaturePrmKey = 1,
    //            RegionalLanguagePrmKey = 1,
    //            CommandAreaRadius = 23,
    //            PopulationOfTheCommandArea = 230,
    //            GeneralLedgerPrmKey = 1,
    //            ParentBusinessOfficePrmKey = 0,
    //            ClearingBusinessOfficePrmKey = 0,
    //            DefaultCurrencyPrmKey = 1,
    //            BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeDetailPrmKey = 1,
    //            TransContactDetails = "8421591918",
    //            TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //            BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ModificationNumber = 0,
    //            ReasonForModification = "Data Entry Mistake",
    //            BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RBIReferenceNumber = "MNP102312",
    //            RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //            RBILicenseNumber = "GJP199865",
    //            UniformBusinessOfficeCode1 = 21,
    //            UniformBusinessOfficeCode2 = 31,
    //            BusinessOfficeCodeByRBI = 51,
    //            MICRCode = 1221,
    //            IFSCCode = "SBIN000452",
    //            AlphaNumericSWIFTAddress = "ITBP021",
    //            AlphaNumericTelexAddress = "MNCD100",
    //            BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //            BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeRBIRegistrationPrmKey = 1,
    //            TransRBIReferenceNumber = "POK1551",
    //            TransAlphaNumericSWIFTAddress = "MTBL0100",
    //            TransAlphaNumericTelexAddress = "OACD200",
    //            TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //            BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            TransModificationNumber = 2,
    //            TransNameOfBusinessOffice = "दहीवडी शाखा",
    //            TransAliasName = "उर्फनाव",
    //            TransNameOnReport = "अहवालासाठी नाव",
    //            EntryDateTime = DateTime.Now,
    //            UserProfilePrmKey = 0,
    //            UserAction = StringLiteralValue.Create,
    //            Remark = "None",
    //            BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //            BusinessOfficeModificationPrmKey = 2,
    //            BusinessOfficeDetailTranslationPrmKey = 3,
    //            BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //            BusinessOfficeTranslationPrmKey = 5,
    //            NameOfUser = "Administrator",
    //            MakerEntryDateTime = DateTime.Now,
    //            CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficePrmKey = 1
    //        };

    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        mock.Setup(p => p.Verify(businessOfficeViewModel)).Returns(Task.FromResult(true));

    //        var mockControllerContext = new Mock<ControllerContext>();
    //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

    //        // Arrange - Create The controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object)
    //        {
    //            ControllerContext = mockControllerContext.Object
    //        };

    //        var mockUrlHelper = new Mock<UrlHelper>();
    //        mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
    //        target.Url = mockUrlHelper.Object;

    //        // Act - Try To Verify The BusinessOffice
    //        ActionResult actionResult = await target.Verify(businessOfficeViewModel, "Verify");

    //        // Assert - Check That The Repository Was Called
    //        mock.Verify(m => m.Verify(businessOfficeViewModel));

    //        // Assert - Check That The Method Result Type
    //        Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
    //    }

    //    [TestMethod]
    //    public async Task Cannot_Amend_InValid_Entry()
    //    {
    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        // Arrange - Create The controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object);

    //        // Arrange - Create The BusinessOffice
    //        BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //        {
    //            PrmKey = 1,
    //            BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCode = "ABC",
    //            AlternateBusinessOfficeCode = "Abc123",
    //            NameOfBusinessOffice = "Dahiwadi Branch",
    //            AliasName = "Dahiwadi Branch",
    //            NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //            NameOnReport = "Public Accounts",
    //            OpeningDate = new DateTime(01 / 01 / 2020),
    //            CloseDate = null,
    //            Note = "None",
    //            BusinessOfficeStatusForCoreOperation = "1",
    //            IsModified = false,
    //            EntryStatus = StringLiteralValue.Create,
    //            ActivationStatus = "I",
    //            BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ApprovalDate = new DateTime(01 / 01 / 2021),
    //            RegistrationDate = new DateTime(01 / 02 / 2021),
    //            RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            ReferenceNumber = "Two",
    //            CoopNumericCode = 2,
    //            CoopAlphaNumericCode = "Fifty",
    //            BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCoopRegistrationPrmKey = 1,
    //            LanguagePrmKey = 2,
    //            TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            TransReferenceNumber = "दोन",
    //            TransCoopAlphaNumericCode = "पन्नास",
    //            TransNote = "काहीही नाही",
    //            TransReasonForModification = "डेटा एंट्री चूक",
    //            BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ContactDetails = "8421591819",
    //            AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //            CenterPrmKey = 1,
    //            OfficeSchedulePrmKey = 1,
    //            BusinessOfficeTypePrmKey = 1,
    //            BusinessNaturePrmKey = 1,
    //            RegionalLanguagePrmKey = 1,
    //            CommandAreaRadius = 23,
    //            PopulationOfTheCommandArea = 230,
    //            GeneralLedgerPrmKey = 1,
    //            ParentBusinessOfficePrmKey = 0,
    //            ClearingBusinessOfficePrmKey = 0,
    //            DefaultCurrencyPrmKey = 1,
    //            BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeDetailPrmKey = 1,
    //            TransContactDetails = "8421591918",
    //            TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //            BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ModificationNumber = 0,
    //            ReasonForModification = "Data Entry Mistake",
    //            BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RBIReferenceNumber = "MNP102312",
    //            RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //            RBILicenseNumber = "GJP199865",
    //            UniformBusinessOfficeCode1 = 21,
    //            UniformBusinessOfficeCode2 = 31,
    //            BusinessOfficeCodeByRBI = 51,
    //            MICRCode = 1221,
    //            IFSCCode = "SBIN000452",
    //            AlphaNumericSWIFTAddress = "ITBP021",
    //            AlphaNumericTelexAddress = "MNCD100",
    //            BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //            BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeRBIRegistrationPrmKey = 1,
    //            TransRBIReferenceNumber = "POK1551",
    //            TransAlphaNumericSWIFTAddress = "MTBL0100",
    //            TransAlphaNumericTelexAddress = "OACD200",
    //            TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //            BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            TransModificationNumber = 2,
    //            TransNameOfBusinessOffice = "दहीवडी शाखा",
    //            TransAliasName = "उर्फनाव",
    //            TransNameOnReport = "अहवालासाठी नाव",
    //            EntryDateTime = DateTime.Now,
    //            UserProfilePrmKey = 0,
    //            UserAction = StringLiteralValue.Create,
    //            Remark = "None",
    //            BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //            BusinessOfficeModificationPrmKey = 2,
    //            BusinessOfficeDetailTranslationPrmKey = 3,
    //            BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //            BusinessOfficeTranslationPrmKey = 5,
    //            NameOfUser = "Administrator",
    //            MakerEntryDateTime = DateTime.Now,
    //            CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficePrmKey = 1
    //        };

    //        // Arrange - Add  An Error To The Model State
    //        target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

    //        // Act - Try To Amend The BusinessOffice
    //        ActionResult actionResult = await target.Amend(businessOfficeViewModel, "Amend");

    //        // Assert - Check That The Repository Was Not Called
    //        mock.Verify(m => m.Amend(It.IsAny<BusinessOfficeViewModel>()), Times.Never());

    //        // Assert - Check That The Method Result Type
    //        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
    //    }

    //    [TestMethod]
    //    public async Task Cannot_Create_InValid_Entry()
    //    {
    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        // Arrange - Create The controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object);

    //        // Arrange - Create The BusinessOffice
    //        BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //        {
    //            PrmKey = 1,
    //            BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCode = "ABC",
    //            AlternateBusinessOfficeCode = "Abc123",
    //            NameOfBusinessOffice = "Dahiwadi Branch",
    //            AliasName = "Dahiwadi Branch",
    //            NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //            NameOnReport = "Public Accounts",
    //            OpeningDate = new DateTime(01 / 01 / 2020),
    //            CloseDate = null,
    //            Note = "None",
    //            BusinessOfficeStatusForCoreOperation = "1",
    //            IsModified = false,
    //            EntryStatus = StringLiteralValue.Create,
    //            ActivationStatus = "I",
    //            BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ApprovalDate = new DateTime(01 / 01 / 2021),
    //            RegistrationDate = new DateTime(01 / 02 / 2021),
    //            RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            ReferenceNumber = "Two",
    //            CoopNumericCode = 2,
    //            CoopAlphaNumericCode = "Fifty",
    //            BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCoopRegistrationPrmKey = 1,
    //            LanguagePrmKey = 2,
    //            TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            TransReferenceNumber = "दोन",
    //            TransCoopAlphaNumericCode = "पन्नास",
    //            TransNote = "काहीही नाही",
    //            TransReasonForModification = "डेटा एंट्री चूक",
    //            BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ContactDetails = "8421591819",
    //            AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //            CenterPrmKey = 1,
    //            OfficeSchedulePrmKey = 1,
    //            BusinessOfficeTypePrmKey = 1,
    //            BusinessNaturePrmKey = 1,
    //            RegionalLanguagePrmKey = 1,
    //            CommandAreaRadius = 23,
    //            PopulationOfTheCommandArea = 230,
    //            GeneralLedgerPrmKey = 1,
    //            ParentBusinessOfficePrmKey = 0,
    //            ClearingBusinessOfficePrmKey = 0,
    //            DefaultCurrencyPrmKey = 1,
    //            BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeDetailPrmKey = 1,
    //            TransContactDetails = "8421591918",
    //            TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //            BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ModificationNumber = 0,
    //            ReasonForModification = "Data Entry Mistake",
    //            BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RBIReferenceNumber = "MNP102312",
    //            RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //            RBILicenseNumber = "GJP199865",
    //            UniformBusinessOfficeCode1 = 21,
    //            UniformBusinessOfficeCode2 = 31,
    //            BusinessOfficeCodeByRBI = 51,
    //            MICRCode = 1221,
    //            IFSCCode = "SBIN000452",
    //            AlphaNumericSWIFTAddress = "ITBP021",
    //            AlphaNumericTelexAddress = "MNCD100",
    //            BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //            BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeRBIRegistrationPrmKey = 1,
    //            TransRBIReferenceNumber = "POK1551",
    //            TransAlphaNumericSWIFTAddress = "MTBL0100",
    //            TransAlphaNumericTelexAddress = "OACD200",
    //            TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //            BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            TransModificationNumber = 2,
    //            TransNameOfBusinessOffice = "दहीवडी शाखा",
    //            TransAliasName = "उर्फनाव",
    //            TransNameOnReport = "अहवालासाठी नाव",
    //            EntryDateTime = DateTime.Now,
    //            UserProfilePrmKey = 0,
    //            UserAction = StringLiteralValue.Create,
    //            Remark = "None",
    //            BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //            BusinessOfficeModificationPrmKey = 2,
    //            BusinessOfficeDetailTranslationPrmKey = 3,
    //            BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //            BusinessOfficeTranslationPrmKey = 5,
    //            NameOfUser = "Administrator",
    //            MakerEntryDateTime = DateTime.Now,
    //            CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficePrmKey = 1
    //        };

    //        // Arrange - Add  An Error To The Model State
    //        target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

    //        // Act - Try To Save The BusinessOffice
    //        ActionResult actionResult = await target.Create(businessOfficeViewModel);

    //        // Assert - Check That The Repository Was Not Called
    //        mock.Verify(m => m.Save(It.IsAny<BusinessOfficeViewModel>()), Times.Never());

    //        // Assert - Check That The Method Result Type
    //        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
    //    }

    //    [TestMethod]
    //    public async Task Cannot_Delete_InValid_Entry()
    //    {
    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        // Arrange - Create The controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object);

    //        // Arrange - Create The BusinessOffice
    //        BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //        {
    //            PrmKey = 1,
    //            BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCode = "ABC",
    //            AlternateBusinessOfficeCode = "Abc123",
    //            NameOfBusinessOffice = "Dahiwadi Branch",
    //            AliasName = "Dahiwadi Branch",
    //            NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //            NameOnReport = "Public Accounts",
    //            OpeningDate = new DateTime(01 / 01 / 2020),
    //            CloseDate = null,
    //            Note = "None",
    //            BusinessOfficeStatusForCoreOperation = "1",
    //            IsModified = false,
    //            EntryStatus = StringLiteralValue.Create,
    //            ActivationStatus = "I",
    //            BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ApprovalDate = new DateTime(01 / 01 / 2021),
    //            RegistrationDate = new DateTime(01 / 02 / 2021),
    //            RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            ReferenceNumber = "Two",
    //            CoopNumericCode = 2,
    //            CoopAlphaNumericCode = "Fifty",
    //            BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCoopRegistrationPrmKey = 1,
    //            LanguagePrmKey = 2,
    //            TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            TransReferenceNumber = "दोन",
    //            TransCoopAlphaNumericCode = "पन्नास",
    //            TransNote = "काहीही नाही",
    //            TransReasonForModification = "डेटा एंट्री चूक",
    //            BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ContactDetails = "8421591819",
    //            AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //            CenterPrmKey = 1,
    //            OfficeSchedulePrmKey = 1,
    //            BusinessOfficeTypePrmKey = 1,
    //            BusinessNaturePrmKey = 1,
    //            RegionalLanguagePrmKey = 1,
    //            CommandAreaRadius = 23,
    //            PopulationOfTheCommandArea = 230,
    //            GeneralLedgerPrmKey = 1,
    //            ParentBusinessOfficePrmKey = 0,
    //            ClearingBusinessOfficePrmKey = 0,
    //            DefaultCurrencyPrmKey = 1,
    //            BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeDetailPrmKey = 1,
    //            TransContactDetails = "8421591918",
    //            TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //            BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ModificationNumber = 0,
    //            ReasonForModification = "Data Entry Mistake",
    //            BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RBIReferenceNumber = "MNP102312",
    //            RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //            RBILicenseNumber = "GJP199865",
    //            UniformBusinessOfficeCode1 = 21,
    //            UniformBusinessOfficeCode2 = 31,
    //            BusinessOfficeCodeByRBI = 51,
    //            MICRCode = 1221,
    //            IFSCCode = "SBIN000452",
    //            AlphaNumericSWIFTAddress = "ITBP021",
    //            AlphaNumericTelexAddress = "MNCD100",
    //            BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //            BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeRBIRegistrationPrmKey = 1,
    //            TransRBIReferenceNumber = "POK1551",
    //            TransAlphaNumericSWIFTAddress = "MTBL0100",
    //            TransAlphaNumericTelexAddress = "OACD200",
    //            TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //            BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            TransModificationNumber = 2,
    //            TransNameOfBusinessOffice = "दहीवडी शाखा",
    //            TransAliasName = "उर्फनाव",
    //            TransNameOnReport = "अहवालासाठी नाव",
    //            EntryDateTime = DateTime.Now,
    //            UserProfilePrmKey = 0,
    //            UserAction = StringLiteralValue.Create,
    //            Remark = "None",
    //            BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //            BusinessOfficeModificationPrmKey = 2,
    //            BusinessOfficeDetailTranslationPrmKey = 3,
    //            BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //            BusinessOfficeTranslationPrmKey = 5,
    //            NameOfUser = "Administrator",
    //            MakerEntryDateTime = DateTime.Now,
    //            CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficePrmKey = 1
    //        };

    //        // Arrange - Add  An Error To The Model State
    //        target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

    //        // Act - Try To Amend The BusinessOffice
    //        ActionResult actionResult = await target.Amend(businessOfficeViewModel, "Delete");

    //        // Assert - Check That The Repository Was Not Called
    //        mock.Verify(m => m.Delete(It.IsAny<BusinessOfficeViewModel>()), Times.Never());

    //        // Assert - Check That The Method Result Type
    //        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
    //    }

    //    [TestMethod]
    //    public async Task Cannot_Modify_InValid_Entry()
    //    {
    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        // Arrange - Create The controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object);

    //        // Arrange - Create The BusinessOffice
    //        BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //        {
    //            PrmKey = 1,
    //            BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCode = "ABC",
    //            AlternateBusinessOfficeCode = "Abc123",
    //            NameOfBusinessOffice = "Dahiwadi Branch",
    //            AliasName = "Dahiwadi Branch",
    //            NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //            NameOnReport = "Public Accounts",
    //            OpeningDate = new DateTime(01 / 01 / 2020),
    //            CloseDate = null,
    //            Note = "None",
    //            BusinessOfficeStatusForCoreOperation = "1",
    //            IsModified = false,
    //            EntryStatus = StringLiteralValue.Create,
    //            ActivationStatus = "I",
    //            BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ApprovalDate = new DateTime(01 / 01 / 2021),
    //            RegistrationDate = new DateTime(01 / 02 / 2021),
    //            RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            ReferenceNumber = "Two",
    //            CoopNumericCode = 2,
    //            CoopAlphaNumericCode = "Fifty",
    //            BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCoopRegistrationPrmKey = 1,
    //            LanguagePrmKey = 2,
    //            TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            TransReferenceNumber = "दोन",
    //            TransCoopAlphaNumericCode = "पन्नास",
    //            TransNote = "काहीही नाही",
    //            TransReasonForModification = "डेटा एंट्री चूक",
    //            BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ContactDetails = "8421591819",
    //            AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //            CenterPrmKey = 1,
    //            OfficeSchedulePrmKey = 1,
    //            BusinessOfficeTypePrmKey = 1,
    //            BusinessNaturePrmKey = 1,
    //            RegionalLanguagePrmKey = 1,
    //            CommandAreaRadius = 23,
    //            PopulationOfTheCommandArea = 230,
    //            GeneralLedgerPrmKey = 1,
    //            ParentBusinessOfficePrmKey = 0,
    //            ClearingBusinessOfficePrmKey = 0,
    //            DefaultCurrencyPrmKey = 1,
    //            BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeDetailPrmKey = 1,
    //            TransContactDetails = "8421591918",
    //            TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //            BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ModificationNumber = 0,
    //            ReasonForModification = "Data Entry Mistake",
    //            BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RBIReferenceNumber = "MNP102312",
    //            RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //            RBILicenseNumber = "GJP199865",
    //            UniformBusinessOfficeCode1 = 21,
    //            UniformBusinessOfficeCode2 = 31,
    //            BusinessOfficeCodeByRBI = 51,
    //            MICRCode = 1221,
    //            IFSCCode = "SBIN000452",
    //            AlphaNumericSWIFTAddress = "ITBP021",
    //            AlphaNumericTelexAddress = "MNCD100",
    //            BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //            BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeRBIRegistrationPrmKey = 1,
    //            TransRBIReferenceNumber = "POK1551",
    //            TransAlphaNumericSWIFTAddress = "MTBL0100",
    //            TransAlphaNumericTelexAddress = "OACD200",
    //            TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //            BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            TransModificationNumber = 2,
    //            TransNameOfBusinessOffice = "दहीवडी शाखा",
    //            TransAliasName = "उर्फनाव",
    //            TransNameOnReport = "अहवालासाठी नाव",
    //            EntryDateTime = DateTime.Now,
    //            UserProfilePrmKey = 0,
    //            UserAction = StringLiteralValue.Create,
    //            Remark = "None",
    //            BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //            BusinessOfficeModificationPrmKey = 2,
    //            BusinessOfficeDetailTranslationPrmKey = 3,
    //            BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //            BusinessOfficeTranslationPrmKey = 5,
    //            NameOfUser = "Administrator",
    //            MakerEntryDateTime = DateTime.Now,
    //            CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficePrmKey = 1
    //        };

    //        // Arrange - Add  An Error To The Model State
    //        target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

    //        // Act - Try To Modify The BusinessOffice
    //        ActionResult actionResult = await target.Modify(businessOfficeViewModel);

    //        // Assert - Check That The Repository Was Not Called
    //        mock.Verify(m => m.Modify(It.IsAny<BusinessOfficeViewModel>()), Times.Never());

    //        // Assert - Check That The Method Result Type
    //        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
    //    }

    //    [TestMethod]
    //    public async Task Cannot_Reject_InValid_Entry()
    //    {
    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        // Arrange - Create The controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object);

    //        // Arrange - Create The BusinessOffice
    //        BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //        {
    //            PrmKey = 1,
    //            BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCode = "ABC",
    //            AlternateBusinessOfficeCode = "Abc123",
    //            NameOfBusinessOffice = "Dahiwadi Branch",
    //            AliasName = "Dahiwadi Branch",
    //            NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //            NameOnReport = "Public Accounts",
    //            OpeningDate = new DateTime(01 / 01 / 2020),
    //            CloseDate = null,
    //            Note = "None",
    //            BusinessOfficeStatusForCoreOperation = "1",
    //            IsModified = false,
    //            EntryStatus = StringLiteralValue.Create,
    //            ActivationStatus = "I",
    //            BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ApprovalDate = new DateTime(01 / 01 / 2021),
    //            RegistrationDate = new DateTime(01 / 02 / 2021),
    //            RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            ReferenceNumber = "Two",
    //            CoopNumericCode = 2,
    //            CoopAlphaNumericCode = "Fifty",
    //            BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCoopRegistrationPrmKey = 1,
    //            LanguagePrmKey = 2,
    //            TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            TransReferenceNumber = "दोन",
    //            TransCoopAlphaNumericCode = "पन्नास",
    //            TransNote = "काहीही नाही",
    //            TransReasonForModification = "डेटा एंट्री चूक",
    //            BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ContactDetails = "8421591819",
    //            AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //            CenterPrmKey = 1,
    //            OfficeSchedulePrmKey = 1,
    //            BusinessOfficeTypePrmKey = 1,
    //            BusinessNaturePrmKey = 1,
    //            RegionalLanguagePrmKey = 1,
    //            CommandAreaRadius = 23,
    //            PopulationOfTheCommandArea = 230,
    //            GeneralLedgerPrmKey = 1,
    //            ParentBusinessOfficePrmKey = 0,
    //            ClearingBusinessOfficePrmKey = 0,
    //            DefaultCurrencyPrmKey = 1,
    //            BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeDetailPrmKey = 1,
    //            TransContactDetails = "8421591918",
    //            TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //            BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ModificationNumber = 0,
    //            ReasonForModification = "Data Entry Mistake",
    //            BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RBIReferenceNumber = "MNP102312",
    //            RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //            RBILicenseNumber = "GJP199865",
    //            UniformBusinessOfficeCode1 = 21,
    //            UniformBusinessOfficeCode2 = 31,
    //            BusinessOfficeCodeByRBI = 51,
    //            MICRCode = 1221,
    //            IFSCCode = "SBIN000452",
    //            AlphaNumericSWIFTAddress = "ITBP021",
    //            AlphaNumericTelexAddress = "MNCD100",
    //            BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //            BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeRBIRegistrationPrmKey = 1,
    //            TransRBIReferenceNumber = "POK1551",
    //            TransAlphaNumericSWIFTAddress = "MTBL0100",
    //            TransAlphaNumericTelexAddress = "OACD200",
    //            TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //            BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            TransModificationNumber = 2,
    //            TransNameOfBusinessOffice = "दहीवडी शाखा",
    //            TransAliasName = "उर्फनाव",
    //            TransNameOnReport = "अहवालासाठी नाव",
    //            EntryDateTime = DateTime.Now,
    //            UserProfilePrmKey = 0,
    //            UserAction = StringLiteralValue.Create,
    //            Remark = "None",
    //            BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //            BusinessOfficeModificationPrmKey = 2,
    //            BusinessOfficeDetailTranslationPrmKey = 3,
    //            BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //            BusinessOfficeTranslationPrmKey = 5,
    //            NameOfUser = "Administrator",
    //            MakerEntryDateTime = DateTime.Now,
    //            CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficePrmKey = 1
    //        };

    //        // Arrange - Add  An Error To The Model State
    //        target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

    //        // Act - Try To Reject The BusinessOffice
    //        ActionResult actionResult = await target.Verify(businessOfficeViewModel, "Reject");

    //        // Assert - Check That The Repository Was Not Called
    //        mock.Verify(m => m.Reject(It.IsAny<BusinessOfficeViewModel>()), Times.Never());

    //        // Assert - Check That The Method Result Type
    //        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
    //    }

    //    [TestMethod]
    //    public async Task Cannot_Verify_InValid_Entry()
    //    {
    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        // Arrange - Create The controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object);

    //        // Arrange - Create The BusinessOffice
    //        BusinessOfficeViewModel businessOfficeViewModel = new BusinessOfficeViewModel
    //        {
    //            PrmKey = 1,
    //            BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCode = "ABC",
    //            AlternateBusinessOfficeCode = "Abc123",
    //            NameOfBusinessOffice = "Dahiwadi Branch",
    //            AliasName = "Dahiwadi Branch",
    //            NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //            NameOnReport = "Public Accounts",
    //            OpeningDate = new DateTime(01 / 01 / 2020),
    //            CloseDate = null,
    //            Note = "None",
    //            BusinessOfficeStatusForCoreOperation = "1",
    //            IsModified = false,
    //            EntryStatus = StringLiteralValue.Create,
    //            ActivationStatus = "I",
    //            BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ApprovalDate = new DateTime(01 / 01 / 2021),
    //            RegistrationDate = new DateTime(01 / 02 / 2021),
    //            RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            ReferenceNumber = "Two",
    //            CoopNumericCode = 2,
    //            CoopAlphaNumericCode = "Fifty",
    //            BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeCoopRegistrationPrmKey = 1,
    //            LanguagePrmKey = 2,
    //            TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //            TransReferenceNumber = "दोन",
    //            TransCoopAlphaNumericCode = "पन्नास",
    //            TransNote = "काहीही नाही",
    //            TransReasonForModification = "डेटा एंट्री चूक",
    //            BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ContactDetails = "8421591819",
    //            AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //            CenterPrmKey = 1,
    //            OfficeSchedulePrmKey = 1,
    //            BusinessOfficeTypePrmKey = 1,
    //            BusinessNaturePrmKey = 1,
    //            RegionalLanguagePrmKey = 1,
    //            CommandAreaRadius = 23,
    //            PopulationOfTheCommandArea = 230,
    //            GeneralLedgerPrmKey = 1,
    //            ParentBusinessOfficePrmKey = 0,
    //            ClearingBusinessOfficePrmKey = 0,
    //            DefaultCurrencyPrmKey = 1,
    //            BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeDetailPrmKey = 1,
    //            TransContactDetails = "8421591918",
    //            TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //            BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ModificationNumber = 0,
    //            ReasonForModification = "Data Entry Mistake",
    //            BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RBIReferenceNumber = "MNP102312",
    //            RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //            RBILicenseNumber = "GJP199865",
    //            UniformBusinessOfficeCode1 = 21,
    //            UniformBusinessOfficeCode2 = 31,
    //            BusinessOfficeCodeByRBI = 51,
    //            MICRCode = 1221,
    //            IFSCCode = "SBIN000452",
    //            AlphaNumericSWIFTAddress = "ITBP021",
    //            AlphaNumericTelexAddress = "MNCD100",
    //            BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //            BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficeRBIRegistrationPrmKey = 1,
    //            TransRBIReferenceNumber = "POK1551",
    //            TransAlphaNumericSWIFTAddress = "MTBL0100",
    //            TransAlphaNumericTelexAddress = "OACD200",
    //            TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //            BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            TransModificationNumber = 2,
    //            TransNameOfBusinessOffice = "दहीवडी शाखा",
    //            TransAliasName = "उर्फनाव",
    //            TransNameOnReport = "अहवालासाठी नाव",
    //            EntryDateTime = DateTime.Now,
    //            UserProfilePrmKey = 0,
    //            UserAction = StringLiteralValue.Create,
    //            Remark = "None",
    //            BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //            BusinessOfficeModificationPrmKey = 2,
    //            BusinessOfficeDetailTranslationPrmKey = 3,
    //            BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //            BusinessOfficeTranslationPrmKey = 5,
    //            NameOfUser = "Administrator",
    //            MakerEntryDateTime = DateTime.Now,
    //            CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //            BusinessOfficePrmKey = 1
    //        };

    //        // Arrange - Add  An Error To The Model State
    //        target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

    //        // Act - Try To Verify The BusinessOffice
    //        ActionResult actionResult = await target.Verify(businessOfficeViewModel, "Verify");

    //        // Assert - Check That The Repository Was Not Called
    //        mock.Verify(m => m.Verify(It.IsAny<BusinessOfficeViewModel>()), Times.Never());

    //        // Assert - Check That The Method Result Type
    //        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
    //    }

    //    [TestMethod]
    //    public async Task RejectedIndex_Contains_AllRejected_Entries()
    //    {
    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        var IndexOfRejectedEntries = new BusinessOfficeViewModel[]
    //                                            {
    //                                              new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "ABC",
    //                AlternateBusinessOfficeCode = "Abc123",
    //                NameOfBusinessOffice = "Dahiwadi Branch",
    //                AliasName = "Dahiwadi Branch",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //                NameOnReport = "Public Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2020),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Reject,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Two",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Fifty",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "8421591819",
    //                AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 23,
    //                PopulationOfTheCommandArea = 230,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "8421591918",
    //                TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MNP102312",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "GJP199865",
    //                UniformBusinessOfficeCode1 = 21,
    //                UniformBusinessOfficeCode2 = 31,
    //                BusinessOfficeCodeByRBI = 51,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP021",
    //                AlphaNumericTelexAddress = "MNCD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "दहीवडी शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवालासाठी नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "Administrator",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            },
    //                                              new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "PQR",
    //                AlternateBusinessOfficeCode = "Pqr123",
    //                NameOfBusinessOffice = "Dahiwadi Branch",
    //                AliasName = "Dahiwadi Branch",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //                NameOnReport = "Private Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2021),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Reject,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Four",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Hundred",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "8421551818",
    //                AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 25,
    //                PopulationOfTheCommandArea = 250,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "8421551717",
    //                TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MKC100313",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "ERP199865",
    //                UniformBusinessOfficeCode1 = 11,
    //                UniformBusinessOfficeCode2 = 22,
    //                BusinessOfficeCodeByRBI = 33,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP0210",
    //                AlphaNumericTelexAddress = "MNPD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12012",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "BRC7890",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "दहीवडी शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवालाचे नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "User",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            },
    //                                              new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "XYX",
    //                AlternateBusinessOfficeCode = "Xyz1230",
    //                NameOfBusinessOffice = "Phaltan Branch",
    //                AliasName = "Phaltan Branch",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Phaltan Branch",
    //                NameOnReport = "Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2020),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Reject,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Nine",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Ninety",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "9422591819",
    //                AddressDetails = "A/p Phaltan Tal-Phaltan Dist - Satara ",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 23,
    //                PopulationOfTheCommandArea = 230,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "9422591918",
    //                TransAddressDetails = "मु.पो.फलटण तालुका फलटण जिल्हा सातारा",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MNP102312",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "GJP199865",
    //                UniformBusinessOfficeCode1 = 21,
    //                UniformBusinessOfficeCode2 = 31,
    //                BusinessOfficeCodeByRBI = 51,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP021",
    //                AlphaNumericTelexAddress = "MNCD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "फलटण शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवाल नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "Local User",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            },
    //                                            }.ToList();

    //        mock.Setup(m => m.GetIndexOfRejectedEntries()).Returns(Task.FromResult<IEnumerable<BusinessOfficeViewModel>>(IndexOfRejectedEntries));

    //        // Arrange - create the controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object);

    //        // Action -target the controller
    //        var result = await target.RejectedIndex() as ViewResult;

    //        // Assert 
    //        Assert.AreEqual(IndexOfRejectedEntries.Count, 3);
    //        Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[0].EntryStatus);
    //        Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[1].EntryStatus);
    //        Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[2].EntryStatus);
    //    }

    //    [TestMethod]
    //    public async Task UnverifiedIndex_Contains_AllUnVerified_Entries()
    //    {
    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        var IndexOfUnVerifiedEntries = new BusinessOfficeViewModel[]
    //                                                    {
    //                                                        new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "ABC",
    //                AlternateBusinessOfficeCode = "Abc123",
    //                NameOfBusinessOffice = "Dahiwadi Branch",
    //                AliasName = "Dahiwadi Branch",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //                NameOnReport = "Public Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2020),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Create,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Two",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Fifty",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "8421591819",
    //                AddressDetails = "A/p Lonand Tal-Khandala Dist-Satara",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 23,
    //                PopulationOfTheCommandArea = 230,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "8421591918",
    //                TransAddressDetails = "मु.पो.लोणंद तालुका खंडाळा जिल्हा सातारा",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MNP102312",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "GJP199865",
    //                UniformBusinessOfficeCode1 = 21,
    //                UniformBusinessOfficeCode2 = 31,
    //                BusinessOfficeCodeByRBI = 51,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP021",
    //                AlphaNumericTelexAddress = "MNCD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "दहीवडी शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवालासाठी नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "Administrator",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            },
    //                                                        new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "PQR",
    //                AlternateBusinessOfficeCode = "Pqr123",
    //                NameOfBusinessOffice = "Dahiwadi Branch",
    //                AliasName = "Dahiwadi Branch",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //                NameOnReport = "Private Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2021),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Create,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Four",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Hundred",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "8421551818",
    //                AddressDetails = "A/p Lonand",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 25,
    //                PopulationOfTheCommandArea = 250,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "8421551717",
    //                TransAddressDetails = "मु.पो.लोणंद",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MKC100313",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "ERP199865",
    //                UniformBusinessOfficeCode1 = 11,
    //                UniformBusinessOfficeCode2 = 22,
    //                BusinessOfficeCodeByRBI = 33,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP0210",
    //                AlphaNumericTelexAddress = "MNPD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12012",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "BRC7890",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "दहीवडी शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवालाचे नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "User",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            },
    //                                                        new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "XYX",
    //                AlternateBusinessOfficeCode = "Xyz1230",
    //                NameOfBusinessOffice = "Dahiwadi Branch",
    //                AliasName = "Dahiwadi Branch",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //                NameOnReport = "Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2020),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Create,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Nine",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Ninety",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "9422591819",
    //                AddressDetails = "A/p Phaltan",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 23,
    //                PopulationOfTheCommandArea = 230,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "9422591918",
    //                TransAddressDetails = "मु.पो.फलटण",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MNP102312",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "GJP199865",
    //                UniformBusinessOfficeCode1 = 21,
    //                UniformBusinessOfficeCode2 = 31,
    //                BusinessOfficeCodeByRBI = 51,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP021",
    //                AlphaNumericTelexAddress = "MNCD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "दहीवडी शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवाल नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "Local User",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            },
    //                                                    }.ToList();

    //        mock.Setup(m => m.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult<IEnumerable<BusinessOfficeViewModel>>(IndexOfUnVerifiedEntries));

    //        // Arrange - create the controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object);

    //        // Action -target the controller
    //        var result = await target.UnverifiedIndex() as ViewResult;

    //        // Assert 
    //        Assert.AreEqual(IndexOfUnVerifiedEntries.Count, 3);
    //        Assert.AreEqual(StringLiteralValue.Create, IndexOfUnVerifiedEntries[0].EntryStatus);
    //        Assert.AreEqual(StringLiteralValue.Create, IndexOfUnVerifiedEntries[1].EntryStatus);
    //        Assert.AreEqual(StringLiteralValue.Create, IndexOfUnVerifiedEntries[2].EntryStatus);
    //    }

    //    [TestMethod]
    //    public async Task VerifiedIndex_Contains_AllVerified_Entries()
    //    {
    //        // Arrange - Create The Mock Repository
    //        Mock<IBusinessOfficeRepository> mock = new Mock<IBusinessOfficeRepository>();
    //        Mock<IBusinessOfficePasswordPolicyRepository> mock1 = new Mock<IBusinessOfficePasswordPolicyRepository>();

    //        var IndexOfVerifiedEntries = new BusinessOfficeViewModel[]
    //                                                    {
    //                                                        new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "ABC",
    //                AlternateBusinessOfficeCode = "Abc123",
    //                NameOfBusinessOffice = "Dahiwadi Branch",
    //                AliasName = "Dahiwadi Branch",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Dahiwadi Branch",
    //                NameOnReport = "Public Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2020),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Verify,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Two",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Fifty",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "8421591819",
    //                AddressDetails = "A/p Pune",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 23,
    //                PopulationOfTheCommandArea = 230,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "8421591918",
    //                TransAddressDetails = "मु.पो.पुणे",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MNP102312",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "GJP199865",
    //                UniformBusinessOfficeCode1 = 21,
    //                UniformBusinessOfficeCode2 = 31,
    //                BusinessOfficeCodeByRBI = 51,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP021",
    //                AlphaNumericTelexAddress = "MNCD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "दहीवडी शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवालासाठी नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "Administrator",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            },
    //                                                        new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "PQR",
    //                AlternateBusinessOfficeCode = "Pqr123",
    //                NameOfBusinessOffice = "Lonand Branch",
    //                AliasName = "Lonand Branch",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Lonand Branch",
    //                NameOnReport = "Private Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2021),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Verify,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Four",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Hundred",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "8421551818",
    //                AddressDetails = "A/p Lonand",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 25,
    //                PopulationOfTheCommandArea = 250,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "8421551717",
    //                TransAddressDetails = "मु.पो.लोणंद",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MKC100313",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "ERP199865",
    //                UniformBusinessOfficeCode1 = 11,
    //                UniformBusinessOfficeCode2 = 22,
    //                BusinessOfficeCodeByRBI = 33,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP0210",
    //                AlphaNumericTelexAddress = "MNPD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12012",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "BRC7890",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "लोणंद शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवालाचे नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "User",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            },
    //                                                        new BusinessOfficeViewModel
    //            {
    //                PrmKey = 1,
    //                BusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCode = "XYX",
    //                AlternateBusinessOfficeCode = "Xyz1230",
    //                NameOfBusinessOffice = "Phaltan Branch",
    //                AliasName = "Phaltan Branch",
    //                NameOfBusinessOfficeForThirdPartyInterface = "Phaltan Branch",
    //                NameOnReport = "Accounts",
    //                OpeningDate = new DateTime(01 / 01 / 2020),
    //                CloseDate = null,
    //                Note = "None",
    //                BusinessOfficeStatusForCoreOperation = "1",
    //                IsModified = false,
    //                EntryStatus = StringLiteralValue.Verify,
    //                ActivationStatus = "I",
    //                BusinessOfficeCoopRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ApprovalDate = new DateTime(01 / 01 / 2021),
    //                RegistrationDate = new DateTime(01 / 02 / 2021),
    //                RegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                ReferenceNumber = "Nine",
    //                CoopNumericCode = 2,
    //                CoopAlphaNumericCode = "Ninety",
    //                BusinessOfficeCoopRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeCoopRegistrationPrmKey = 1,
    //                LanguagePrmKey = 2,
    //                TransRegistrationNumber = "SUR/BSI/ARSR/R/1742",
    //                TransReferenceNumber = "दोन",
    //                TransCoopAlphaNumericCode = "पन्नास",
    //                TransNote = "काहीही नाही",
    //                TransReasonForModification = "डेटा एंट्री चूक",
    //                BusinessOfficeDetailId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ContactDetails = "9422591819",
    //                AddressDetails = "A/p Phaltan",
    //                CenterPrmKey = 1,
    //                OfficeSchedulePrmKey = 1,
    //                BusinessOfficeTypePrmKey = 1,
    //                BusinessNaturePrmKey = 1,
    //                RegionalLanguagePrmKey = 1,
    //                CommandAreaRadius = 23,
    //                PopulationOfTheCommandArea = 230,
    //                GeneralLedgerPrmKey = 1,
    //                ParentBusinessOfficePrmKey = 0,
    //                ClearingBusinessOfficePrmKey = 0,
    //                DefaultCurrencyPrmKey = 1,
    //                BusinessOfficeDetailTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeDetailPrmKey = 1,
    //                TransContactDetails = "9422591918",
    //                TransAddressDetails = "मु.पो.फलटण",
    //                BusinessOfficeModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ModificationNumber = 0,
    //                ReasonForModification = "Data Entry Mistake",
    //                BusinessOfficeRBIRegistrationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RBIReferenceNumber = "MNP102312",
    //                RBIApprovalDate = new DateTime(10 / 02 / 2021),
    //                RBILicenseNumber = "GJP199865",
    //                UniformBusinessOfficeCode1 = 21,
    //                UniformBusinessOfficeCode2 = 31,
    //                BusinessOfficeCodeByRBI = 51,
    //                MICRCode = 1221,
    //                IFSCCode = "SBIN000452",
    //                AlphaNumericSWIFTAddress = "ITBP021",
    //                AlphaNumericTelexAddress = "MNCD100",
    //                BusinessOfficeUniqueIdentityNumberForATM = "UAE12021",
    //                BusinessOfficeRBIRegistrationTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficeRBIRegistrationPrmKey = 1,
    //                TransRBIReferenceNumber = "POK1551",
    //                TransAlphaNumericSWIFTAddress = "MTBL0100",
    //                TransAlphaNumericTelexAddress = "OACD200",
    //                TransBusinessOfficeUniqueIdentityNumberForATM = "ARC789",
    //                BusinessOfficeTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                TransModificationNumber = 2,
    //                TransNameOfBusinessOffice = "फलटण शाखा",
    //                TransAliasName = "उर्फनाव",
    //                TransNameOnReport = "अहवाल नाव",
    //                EntryDateTime = DateTime.Now,
    //                UserProfilePrmKey = 0,
    //                UserAction = StringLiteralValue.Create,
    //                Remark = "None",
    //                BusinessOfficeCoopRegistrationTranslationPrmKey = 2,
    //                BusinessOfficeModificationPrmKey = 2,
    //                BusinessOfficeDetailTranslationPrmKey = 3,
    //                BusinessOfficeRBIRegistrationTranslationPrmKey = 4,
    //                BusinessOfficeTranslationPrmKey = 5,
    //                NameOfUser = "Local User",
    //                MakerEntryDateTime = DateTime.Now,
    //                CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                OfficeScheduleId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessOfficeTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                MLBusinessNatureId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                RegionalLanguageId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                GeneralLedgerId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ParentBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                ClearingBusinessOfficeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                DefaultCurrencyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
    //                BusinessOfficePrmKey = 1
    //            },
    //                                                    }.ToList();

    //        mock.Setup(m => m.GetIndexOfVerifiedEntries()).Returns(Task.FromResult<IEnumerable<BusinessOfficeViewModel>>(IndexOfVerifiedEntries));

    //        // Arrange - create the controller
    //        BusinessOfficeController target = new BusinessOfficeController(mock.Object, mock1.Object);


    //        // Action -target the controller
    //        var result = await target.VerifiedIndex() as ViewResult;

    //        // Assert 
    //        Assert.AreEqual(IndexOfVerifiedEntries.Count, 3);
    //        Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[0].EntryStatus);
    //        Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[1].EntryStatus);
    //        Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[2].EntryStatus);
    //    }
    //}
    }
}