//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using DemoProject.Services.Abstract.Master.Address;
//using DemoProject.Services.Abstract.Parameter.Master;
//using DemoProject.Services.ViewModel.Master.Address;
//using DemoProject.WebUI.Controllers;
//using DemoProject.WebUI.Infrastructure.CustomException;
//using DemoProject.Services.Constants;

//namespace DemoProject.UnitTests.Controllers
//{
//    [TestClass]
//    public class VillageControllerTest
//    {
//        //[TestMethod]
//        //public async Task Can_Amend_Get_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        VillageTownCityViewModel villageViewModel = null;

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //        mockVillage.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(villageViewModel));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Amend The Village 
//        //        ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_Amend_Post_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create,  AreaTypeId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  EducationLevelId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  FamilySystemId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "M", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();
//        //        mockVillage.Setup(p => p.Amend(villageViewModel)).Returns(Task.FromResult(false));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Amend The Village
//        //        ActionResult actionResult = await target.Amend(villageViewModel, "Amend");

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.Amend(villageViewModel));

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_Amend_Valid_Entry()
//        //{
//        //    // Arrange - Create The Village
//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    mockVillage.Setup(p => p.Amend(villageViewModel)).Returns(Task.FromResult(true));

//        //    var mockControllerContext = new Mock<ControllerContext>();
//        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //    // Arrange - Create The controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //    {
//        //        ControllerContext = mockControllerContext.Object
//        //    };

//        //    // Act - Try To Amend The Village
//        //    ActionResult actionResult = await target.Amend(villageViewModel, "Amend");

//        //    // Assert - Check That The Repository Was Called
//        //    mockVillage.Verify(m => m.Amend(villageViewModel));

//        //    // Assert - Check That The Method Result Type
//        //    Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        //}

//        //[TestMethod]
//        //public async Task Can_Create_Post_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();
//        //        mockVillage.Setup(p => p.Save(villageViewModel)).Returns(Task.FromResult(false));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Create The Village
//        //        ActionResult actionResult = await target.Create(villageViewModel);

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.Save(villageViewModel));

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_Create_Valid_Entry()
//        //{
//        //    // Arrange - Create The Village
//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();


//        //    mockVillage.Setup(p => p.Save(villageViewModel)).Returns(Task.FromResult(true));

//        //    var mockControllerContext = new Mock<ControllerContext>();
//        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

//        //    // Arrange - Create The controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //    {
//        //        ControllerContext = mockControllerContext.Object
//        //    };

//        //    // Act - Try To Save The Village
//        //    ActionResult actionResult = await target.Create(villageViewModel);

//        //    // Assert - Check That The Repository Was Called
//        //    mockVillage.Verify(m => m.Save(villageViewModel));

//        //    // Assert - Check That The Method Result Type
//        //    Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        //}

//        //[TestMethod]
//        //public async Task Can_Delete_Get_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        VillageTownCityViewModel villageViewModel = null;

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //        mockVillage.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(villageViewModel));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Delete The Village
//        //        ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_Delete_Post_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Delete, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //        mockVillage.Setup(p => p.Delete(villageViewModel)).Returns(Task.FromResult(false));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Delete The Village
//        //        ActionResult actionResult = await target.Amend(villageViewModel, "Delete");

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.Delete(villageViewModel));

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_Delete_Valid_Entry()
//        //{
//        //    // Arrange - Create The Village
//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Delete, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    mockVillage.Setup(p => p.Delete(villageViewModel)).Returns(Task.FromResult(true));

//        //    var mockControllerContext = new Mock<ControllerContext>();
//        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //    // Arrange - Create The controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //    {
//        //        ControllerContext = mockControllerContext.Object
//        //    };
//        //    var mockUrlHelper = new Mock<UrlHelper>();
//        //    mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
//        //    target.Url = mockUrlHelper.Object;

//        //    // Act - Try To Delete The Village
//        //    ActionResult actionResult = await target.Amend(villageViewModel, "Delete");

//        //    // Assert - Check That The Repository Was Called
//        //    mockVillage.Verify(m => m.Delete(villageViewModel));

//        //    // Assert - Check That The Method Result Type
//        //    Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        //}

//        //[TestMethod]
//        //public async Task Can_Modify_Get_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        VillageTownCityViewModel villageViewModel = null;

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //        mockVillage.Setup(p => p.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(villageViewModel));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Modify The Village
//        //        ActionResult actionResult = await target.Modify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_Modify_Post_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "MDF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "MDF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //        mockVillage.Setup(p => p.Modify(villageViewModel)).Returns(Task.FromResult(false));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Modify The Village
//        //        ActionResult actionResult = await target.Modify(villageViewModel);

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.Modify(villageViewModel));

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_Modify_Valid_Entry()
//        //{
//        //    // Arrange - Create The Village
//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "MDF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "MDF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    mockVillage.Setup(p => p.Modify(villageViewModel)).Returns(Task.FromResult(true));

//        //    var mockControllerContext = new Mock<ControllerContext>();
//        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //    // Arrange - Create The controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //    {
//        //        ControllerContext = mockControllerContext.Object
//        //    };

//        //    // Act - Try To Modify The Village
//        //    ActionResult actionResult = await target.Modify(villageViewModel);

//        //    // Assert - Check That The Repository Was Called
//        //    mockVillage.Verify(m => m.Modify(villageViewModel));

//        //    // Assert - Check That The Method Result Type
//        //    Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        //}

//        //[TestMethod]
//        //public async Task Can_Reject_Get_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        VillageTownCityViewModel villageViewModel = null;

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //        mockVillage.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(villageViewModel));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Reject The Village
//        //        ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_Reject_Post_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //        mockVillage.Setup(p => p.Reject(villageViewModel)).Returns(Task.FromResult(false));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Reject The Village
//        //        ActionResult actionResult = await target.Verify(villageViewModel, "Reject");

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.Reject(villageViewModel));

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_Reject_Valid_Entry()
//        //{
//        //    // Arrange - Create The Village
//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    mockVillage.Setup(p => p.Reject(villageViewModel)).Returns(Task.FromResult(true));

//        //    var mockControllerContext = new Mock<ControllerContext>();
//        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //    // Arrange - Create The controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //    {
//        //        ControllerContext = mockControllerContext.Object
//        //    };
//        //    var mockUrlHelper = new Mock<UrlHelper>();
//        //    mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
//        //    target.Url = mockUrlHelper.Object;

//        //    // Act - Try To Reject The Village
//        //    ActionResult actionResult = await target.Verify(villageViewModel, "Reject");

//        //    // Assert - Check That The Repository Was Called
//        //    mockVillage.Verify(m => m.Reject(villageViewModel));

//        //    // Assert - Check That The Method Result Type
//        //    Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        //}

//        //[TestMethod]
//        //public async Task Can_RejectedIndex_Get_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        IEnumerable<VillageTownCityViewModel> villageViewModel = null;

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //        mockVillage.Setup(p => p.GetIndexOfRejectedEntries()).Returns(Task.FromResult(villageViewModel));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Reject The Village
//        //        ActionResult actionResult = await target.RejectedIndex();

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.GetIndexOfRejectedEntries());

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_UnverifiedIndex_Get_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        IEnumerable<VillageTownCityViewModel> villageViewModel = null;

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //        mockVillage.Setup(p => p.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult(villageViewModel));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Verify The Village
//        //        ActionResult actionResult = await target.UnverifiedIndex();

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.GetIndexOfUnVerifiedEntries());

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_VerifiedIndex_Get_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        IEnumerable<VillageTownCityViewModel> villageViewModel = null;

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //        mockVillage.Setup(p => p.GetIndexOfVerifiedEntries()).Returns(Task.FromResult(villageViewModel));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Verify The Village
//        //        ActionResult actionResult = await target.VerifiedIndex();

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.GetIndexOfVerifiedEntries());

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_Verify_Get_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        VillageTownCityViewModel villageViewModel = null;

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //        mockVillage.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(villageViewModel));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Verify The Village
//        //        ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_Verify_Post_Method_Throws_Exception()
//        //{
//        //    var expectedException = new DatabaseException();

//        //    try
//        //    {
//        //        // Arrange - Create The Village
//        //        VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //        // Arrange - Create The Mock Repository
//        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //        Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //        Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //        Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //        Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //        Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //        mockVillage.Setup(p => p.Verify(villageViewModel)).Returns(Task.FromResult(false));

//        //        var mockControllerContext = new Mock<ControllerContext>();
//        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //        // Arrange - Create The controller
//        //        VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //        {
//        //            ControllerContext = mockControllerContext.Object
//        //        };

//        //        // Act - Try To Verify The Village
//        //        ActionResult actionResult = await target.Verify(villageViewModel, "Verify");

//        //        // Assert - Check That The Repository Was Called
//        //        mockVillage.Verify(m => m.Verify(villageViewModel));

//        //        Assert.Fail("An exception was not thrown as expected.");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
//        //        if (e.GetType() == typeof(AssertFailedException)) throw;

//        //        // Assert - Check That The Exception Type And Message
//        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
//        //        Assert.AreEqual(expectedException.Message, e.Message);
//        //    }
//        //}

//        //[TestMethod]
//        //public async Task Can_Verify_Valid_Entry()
//        //{
//        //    // Arrange - Create The Village
//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    mockVillage.Setup(p => p.Verify(villageViewModel)).Returns(Task.FromResult(true));

//        //    var mockControllerContext = new Mock<ControllerContext>();
//        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//        //    // Arrange - Create The controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object)
//        //    {
//        //        ControllerContext = mockControllerContext.Object
//        //    };

//        //    var mockUrlHelper = new Mock<UrlHelper>();
//        //    mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
//        //    target.Url = mockUrlHelper.Object;

//        //    // Act - Try To Verify The Village
//        //    ActionResult actionResult = await target.Verify(villageViewModel, "Verify");

//        //    // Assert - Check That The Repository Was Called
//        //    mockVillage.Verify(m => m.Verify(villageViewModel));

//        //    // Assert - Check That The Method Result Type
//        //    Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        //}

//        //[TestMethod]
//        //public async Task Cannot_Amend_InValid_Entry()
//        //{
//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    // Arrange - Create The controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object);

//        //    // Arrange - Create The Village
//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "M", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //    // Arrange - Add  An Error To The Model Village
//        //    target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//        //    // Act - Try To Amend The Village
//        //    ActionResult actionResult = await target.Amend(villageViewModel, "Amend");

//        //    // Assert - Check That The Repository Was Not Called
//        //    mockVillage.Verify(m => m.Amend(It.IsAny<VillageTownCityViewModel>()), Times.Never());

//        //    // Assert - Check That The Method Result Type
//        //    Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        //}

//        //[TestMethod]
//        //public async Task Cannot_Create_InValid_Entry()
//        //{
//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    // Arrange - Create The controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object);

//        //    // Arrange - Create The Village
//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //    // Arrange - Add  An Error To The Model Village
//        //    target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//        //    // Act - Try To Save The Village
//        //    ActionResult actionResult = await target.Create(villageViewModel);

//        //    // Assert - Check That The Repository Was Not Called
//        //    mockVillage.Verify(m => m.Save(It.IsAny<VillageTownCityViewModel>()), Times.Never());

//        //    // Assert - Check That The Method Result Type
//        //    Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        //}

//        //[TestMethod]
//        //public async Task Cannot_Delete_InValid_Entry()
//        //{
//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    // Arrange - Create The controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object);

//        //    // Arrange - Create The Village
//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Delete, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //    // Arrange - Add  An Error To The Model Village
//        //    target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//        //    // Act - Try To Amend The Village
//        //    ActionResult actionResult = await target.Amend(villageViewModel, "Delete");

//        //    // Assert - Check That The Repository Was Not Called
//        //    mockVillage.Verify(m => m.Delete(It.IsAny<VillageTownCityViewModel>()), Times.Never());

//        //    // Assert - Check That The Method Result Type
//        //    Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        //}

//        //[TestMethod]
//        //public async Task Cannot_Modify_InValid_Entry()
//        //{
//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    // Arrange - Create The controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object);

//        //    // Arrange - Create The Village
//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = "MDF", CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = "MDF", Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //    // Arrange - Add  An Error To The Model Village
//        //    target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//        //    // Act - Try To Modify The Village
//        //    ActionResult actionResult = await target.Modify(villageViewModel);

//        //    // Assert - Check That The Repository Was Not Called
//        //    mockVillage.Verify(m => m.Modify(It.IsAny<VillageTownCityViewModel>()), Times.Never());

//        //    // Assert - Check That The Method Result Type
//        //    Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        //}

//        //[TestMethod]
//        //public async Task Cannot_Reject_InValid_Entry()
//        //{
//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    // Arrange - Create The controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object);

//        //    // Arrange - Create The Village
//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //    // Arrange - Add  An Error To The Model Village
//        //    target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//        //    // Act - Try To Reject The Village
//        //    ActionResult actionResult = await target.Verify(villageViewModel, "Reject");

//        //    // Assert - Check That The Repository Was Not Called
//        //    mockVillage.Verify(m => m.Reject(It.IsAny<VillageTownCityViewModel>()), Times.Never());

//        //    // Assert - Check That The Method Result Type
//        //    Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        //}

//        //[TestMethod]
//        //public async Task Cannot_Verify_InValid_Entry()
//        //{
//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    // Arrange - Create The controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object);

//        //    // Arrange - Create The Village
//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

//        //    // Arrange - Add  An Error To The Model Village
//        //    target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//        //    // Act - Try To Verify The Village
//        //    ActionResult actionResult = await target.Verify(villageViewModel, "Verify");

//        //    // Assert - Check That The Repository Was Not Called
//        //    mockVillage.Verify(m => m.Verify(It.IsAny<VillageTownCityViewModel>()), Times.Never());

//        //    // Assert - Check That The Method Result Type
//        //    Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        //}

//        //[TestMethod]
//        //public async Task RejectedIndex_Contains_AllRejected_Entries()
//        //{
//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    var IndexOfRejectedEntries = new VillageTownCityViewModel[]
//        //                                        {
//        //                                                new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
//        //                                                new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
//        //                                                new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
//        //                                         }.ToList();

//        //    mockVillage.Setup(m => m.GetIndexOfRejectedEntries()).Returns(Task.FromResult<IEnumerable<VillageTownCityViewModel>>(IndexOfRejectedEntries));

//        //    // Arrange - create the controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object);

//        //    // Action -target the controller
//        //    var result = await target.RejectedIndex() as ViewResult;

//        //    // Assert 
//        //    Assert.AreEqual(IndexOfRejectedEntries.Count, 3);
//        //    Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[0].EntryStatus);
//        //    Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[1].EntryStatus);
//        //    Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[2].EntryStatus);
//        //}

//        //[TestMethod]
//        //public async Task UnverifiedIndex_Contains_AllUnverified_Entries()
//        //{
//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    var IndexOfUnverifiedEntries = new VillageTownCityViewModel[]
//        //                                         {
//        //                                                new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
//        //                                                new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
//        //                                                new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
//        //                                         }.ToList();

//        //    mockVillage.Setup(m => m.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult<IEnumerable<VillageTownCityViewModel>>(IndexOfUnverifiedEntries));

//        //    // Arrange - create the controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object);

//        //    // Action -target the controller
//        //    var result = await target.UnverifiedIndex() as ViewResult;

//        //    // Assert 
//        //    Assert.AreEqual(IndexOfUnverifiedEntries.Count, 3);
//        //    Assert.AreEqual(StringLiteralValue.Create, IndexOfUnverifiedEntries[0].EntryStatus);
//        //    Assert.AreEqual(StringLiteralValue.Create, IndexOfUnverifiedEntries[1].EntryStatus);
//        //    Assert.AreEqual(StringLiteralValue.Create, IndexOfUnverifiedEntries[2].EntryStatus);
//        //}

//        //[TestMethod]
//        //public async Task VerifiedIndex_Contains_AllVerified_Entries()
//        //{
//        //    // Arrange - Create The Mock Repository
//        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
//        //    Mock<IVillageTownCityRepository> mockVillage = new Mock<IVillageTownCityRepository>();
//        //    Mock<ICenterTradingEntityDetailsRepository> mockTradingEntity = new Mock<ICenterTradingEntityDetailsRepository>();
//        //    Mock<IDistrictRepository> mockDistrict = new Mock<IDistrictRepository>();
//        //    Mock<IDivisionRepository> mockDivision = new Mock<IDivisionRepository>();
//        //    Mock<IStateRepository> mockState = new Mock<IStateRepository>();
//        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

//        //    VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };
//        //    var IndexOfVerifiedEntries = new VillageTownCityViewModel[]
//        //                                         {
//        //                                                new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
//        //                                                new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
//        //                                                new VillageTownCityViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0,  ParentCenterPostId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
//        //                                         }.ToList();

//        //    mockVillage.Setup(m => m.GetIndexOfVerifiedEntries()).Returns(Task.FromResult<IEnumerable<VillageTownCityViewModel>>(IndexOfVerifiedEntries));

//        //    // Arrange - create the controller
//        //    VillageTownCityController target = new VillageTownCityController(mockAddressParametr.Object, mockTradingEntity.Object, mockDistrict.Object, mockDivision.Object, mockState.Object, mockTaluka.Object, mockVillage.Object);

//        //    // Action -target the controller
//        //    var result = await target.VerifiedIndex() as ViewResult;

//        //    // Assert 
//        //    Assert.AreEqual(IndexOfVerifiedEntries.Count, 3);
//        //    Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[0].EntryStatus);
//        //    Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[1].EntryStatus);
//        //    Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[2].EntryStatus);
//        //}
//    }
//}