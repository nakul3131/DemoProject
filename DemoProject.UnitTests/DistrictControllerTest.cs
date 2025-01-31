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
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.UnitTests.Controllers
{
    [TestClass]
    public class DistrictControllerTest
    {

        [TestMethod]
        public async Task Can_Amend_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The District
                DistrictViewModel districtViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(districtViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The District 
                ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The District
                DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "AMN", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "AMN", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.Amend(districtViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Amend The District
                ActionResult actionResult = await target.Amend(districtViewModel, "Amend");

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.Amend(districtViewModel));

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
            // Arrange - Create The District
            DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "AMN", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "AMN", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            mockDistrict.Setup(p => p.Amend(districtViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Amend The District
            ActionResult actionResult = await target.Amend(districtViewModel, "Amend");

            // Assert - Check That The Repository Was Called
            mockDistrict.Verify(m => m.Amend(districtViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task Can_Create_Post_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The District
                DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.Save(districtViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Create The District
                ActionResult actionResult = await target.Create(districtViewModel);

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.Save(districtViewModel));

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
            // Arrange - Create The District
            DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            mockDistrict.Setup(p => p.Save(districtViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

            // Arrange - Create The controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Save The District
            ActionResult actionResult = await target.Create(districtViewModel);

            // Assert - Check That The Repository Was Called
            mockDistrict.Verify(m => m.Save(districtViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Delete_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The District
                DistrictViewModel districtViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(districtViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Delete The District
                ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The District
                DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "VRF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.Delete(districtViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Delete The District
                ActionResult actionResult = await target.Amend(districtViewModel, "Delete");

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.Delete(districtViewModel));

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
            // Arrange - Create The District
            DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "VRF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            mockDistrict.Setup(p => p.Delete(districtViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
            {
                ControllerContext = mockControllerContext.Object
            };
            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Delete The District
            ActionResult actionResult = await target.Amend(districtViewModel, "Delete");

            // Assert - Check That The Repository Was Called
            mockDistrict.Verify(m => m.Delete(districtViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task Can_Modify_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The District
                DistrictViewModel districtViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(districtViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Modify The District
                ActionResult actionResult = await target.Modify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The District
                DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "MDF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "MDF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.Modify(districtViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Modify The District
                ActionResult actionResult = await target.Modify(districtViewModel);

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.Modify(districtViewModel));

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
            // Arrange - Create The District
            DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "MDF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "MDF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            mockDistrict.Setup(p => p.Modify(districtViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            // Act - Try To Modify The District
            ActionResult actionResult = await target.Modify(districtViewModel);

            // Assert - Check That The Repository Was Called
            mockDistrict.Verify(m => m.Modify(districtViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_Reject_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The District
                DistrictViewModel districtViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(districtViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Reject The District
                ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The District
                DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "REJ", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "REJ", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.Reject(districtViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Reject The District
                ActionResult actionResult = await target.Verify(districtViewModel, "Reject");

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.Reject(districtViewModel));

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
            // Arrange - Create The District
            DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "REJ", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "REJ", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            mockDistrict.Setup(p => p.Reject(districtViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
            {
                ControllerContext = mockControllerContext.Object
            };
            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Reject The District
            ActionResult actionResult = await target.Verify(districtViewModel, "Reject");

            // Assert - Check That The Repository Was Called
            mockDistrict.Verify(m => m.Reject(districtViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Can_RejectedIndex_Get_Method_Throws_Exception()
        {
            var expectedException = new DatabaseException();

            try
            {
                // Arrange - Create The District
                IEnumerable<DistrictViewModel> districtViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.GetIndexOfRejectedEntries()).Returns(Task.FromResult(districtViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Reject The District
                ActionResult actionResult = await target.RejectedIndex();

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.GetIndexOfRejectedEntries());

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
                // Arrange - Create The District
                IEnumerable<DistrictViewModel> districtViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();


                mockDistrict.Setup(p => p.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult(districtViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The District
                ActionResult actionResult = await target.UnverifiedIndex();

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.GetIndexOfUnVerifiedEntries());

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
                // Arrange - Create The District
                IEnumerable<DistrictViewModel> districtViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.GetIndexOfVerifiedEntries()).Returns(Task.FromResult(districtViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The District
                ActionResult actionResult = await target.VerifiedIndex();

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.GetIndexOfVerifiedEntries());

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
                // Arrange - Create The District
                DistrictViewModel districtViewModel = null;

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(districtViewModel));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The District
                ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

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
                // Arrange - Create The District
                DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

                // Arrange - Create The Mock Repository
                Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
                Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
                Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

                mockDistrict.Setup(p => p.Verify(districtViewModel)).Returns(Task.FromResult(false));

                var mockControllerContext = new Mock<ControllerContext>();
                mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

                // Arrange - Create The controller
                DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
                {
                    ControllerContext = mockControllerContext.Object
                };

                // Act - Try To Verify The District
                ActionResult actionResult = await target.Verify(districtViewModel, "Verify");

                // Assert - Check That The Repository Was Called
                mockDistrict.Verify(m => m.Verify(districtViewModel));

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
            // Arrange - Create The District
            DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            mockDistrict.Setup(p => p.Verify(districtViewModel)).Returns(Task.FromResult(true));

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

            // Arrange - Create The controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object)
            {
                ControllerContext = mockControllerContext.Object
            };

            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
            target.Url = mockUrlHelper.Object;

            // Act - Try To Verify The District
            ActionResult actionResult = await target.Verify(districtViewModel, "Verify");

            // Assert - Check That The Repository Was Called
            mockDistrict.Verify(m => m.Verify(districtViewModel));

            // Assert - Check That The Method Result Type
            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task Cannot_Amend_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            // Arrange - Create The controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object);

            // Arrange - Create The District
            DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Amend The District
            ActionResult actionResult = await target.Amend(districtViewModel, "Amend");

            // Assert - Check That The Repository Was Not Called
            mockDistrict.Verify(m => m.Amend(It.IsAny<DistrictViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task Cannot_Create_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            // Arrange - Create The controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object);

            // Arrange - Create The District
            DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Save The District
            ActionResult actionResult = await target.Create(districtViewModel);

            // Assert - Check That The Repository Was Not Called
            mockDistrict.Verify(m => m.Save(It.IsAny<DistrictViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Delete_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            // Arrange - Create The controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object);

            // Arrange - Create The District
            DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "VRF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Amend The District
            ActionResult actionResult = await target.Amend(districtViewModel, "Delete");

            // Assert - Check That The Repository Was Not Called
            mockDistrict.Verify(m => m.Delete(It.IsAny<DistrictViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task Cannot_Modify_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            // Arrange - Create The controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object);

            // Arrange - Create The District
            DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "MDF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "MDF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Modify The District
            ActionResult actionResult = await target.Modify(districtViewModel);

            // Assert - Check That The Repository Was Not Called
            mockDistrict.Verify(m => m.Modify(It.IsAny<DistrictViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Cannot_Reject_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            // Arrange - Create The controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object);

            // Arrange - Create The District
            DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "REJ", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "REJ", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Reject The District
            ActionResult actionResult = await target.Verify(districtViewModel, "Reject");

            // Assert - Check That The Repository Was Not Called
            mockDistrict.Verify(m => m.Reject(It.IsAny<DistrictViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task Cannot_Verify_InValid_Entry()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            // Arrange - Create The controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object);

            // Arrange - Create The District
            DistrictViewModel districtViewModel = new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "VRF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

            // Arrange - Add  An Error To The Model State
            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

            // Act - Try To Verify The District
            ActionResult actionResult = await target.Verify(districtViewModel, "Verify");

            // Assert - Check That The Repository Was Not Called
            mockDistrict.Verify(m => m.Verify(It.IsAny<DistrictViewModel>()), Times.Never());

            // Assert - Check That The Method Result Type
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }
        
        [TestMethod]
        public async Task RejectedIndex_Contains_AllRejected_Entries()
        {
            // Arrange - Create The Mock Repository
            Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
            Mock<ICenterRepository> mockCenter = new Mock<ICenterRepository>();
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            var IndexOfRejectedEntries = new DistrictViewModel[]
                                                {
                                                   new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "REJ", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
                                                   new DistrictViewModel { PrmKey = 2, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "North", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "REJ", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "User", MakerEntryDateTime = DateTime.Now },
                                                   new DistrictViewModel { PrmKey = 3, NameOfCenter = "Mumbai", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Mumbai", OtherNameAsParent = "North", NameOnReport = "Mumbai", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "REJ", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "मुंबई", TransAliasName = "मुंबई", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "मुंबई", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Super User", MakerEntryDateTime = DateTime.Now }
                                                }.ToList();

            mockDistrict.Setup(m => m.GetIndexOfRejectedEntries()).Returns(Task.FromResult<IEnumerable<DistrictViewModel>>(IndexOfRejectedEntries));

            // Arrange - create the controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object);

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
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            var IndexOfUnverifiedEntries = new DistrictViewModel[]
                                                 {
                                                   new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
                                                   new DistrictViewModel { PrmKey = 2, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "North", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "User", MakerEntryDateTime = DateTime.Now },
                                                   new DistrictViewModel { PrmKey = 3, NameOfCenter = "Mumbai", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Mumbai", OtherNameAsParent = "North", NameOnReport = "Mumbai", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "CRT", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "मुंबई", TransAliasName = "मुंबई", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "मुंबई", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "CRT", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Super User", MakerEntryDateTime = DateTime.Now }
                                                }.ToList();

            mockDistrict.Setup(m => m.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult<IEnumerable<DistrictViewModel>>(IndexOfUnverifiedEntries));

            // Arrange - create the controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object);

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
            Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();

            var IndexOfVerifiedEntries = new DistrictViewModel[]
                                                 {
                                                   new DistrictViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", OtherNameAsParent = "North", NameOnReport = "Satara", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "सातारा", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "VRF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
                                                   new DistrictViewModel { PrmKey = 2, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", OtherNameAsParent = "North", NameOnReport = "Pune", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "पुणे", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "VRF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "User", MakerEntryDateTime = DateTime.Now },
                                                   new DistrictViewModel { PrmKey = 3, NameOfCenter = "Mumbai", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Mumbai", OtherNameAsParent = "North", NameOnReport = "Mumbai", CenterCategory = 14, Direction = 0, LocalGovernment = 0, PinCode = 0, ParentCenterPrmKey = 0, ISOAlphaNumericCode2 = "A1", ISOAlphaNumericCode3 = "B23", ISONumericCode = 12, OtherCode = "IBKL2240", ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "VRF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterISOCodeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "मुंबई", TransAliasName = "मुंबई", TransOtherNameAsParent = "उत्तर", TransNameOnReport = "मुंबई", TransNote = "काहीही नाही", TransReasonForModification = "Data Entry Mistake", CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "VRF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1, CenterISOCodePrmKey = 1, NameOfUser = "Super User", MakerEntryDateTime = DateTime.Now }
                                                }.ToList();

            mockDistrict.Setup(m => m.GetIndexOfVerifiedEntries()).Returns(Task.FromResult<IEnumerable<DistrictViewModel>>(IndexOfVerifiedEntries));

            // Arrange - create the controller
            DistrictController target = new DistrictController(mockAddressParametr.Object, mockCenter.Object, mockDistrict.Object);

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
