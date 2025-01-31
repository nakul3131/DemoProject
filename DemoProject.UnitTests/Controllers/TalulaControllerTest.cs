using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoProject.UnitTests.Controllers
{
    [TestClass]
    public class TalulaControllerTest
    {
        //[TestMethod]
        //public async Task Can_Amend_Get_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        TalukaViewModel talukaViewModel = null;

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(talukaViewModel));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Amend The Taluka 
        //        ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_Amend_Post_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.Amend(talukaViewModel)).Returns(Task.FromResult(false));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Amend The Taluka
        //        ActionResult actionResult = await target.Amend(talukaViewModel, "Amend");

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.Amend(talukaViewModel));

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_Amend_Valid_Entry()
        //{
        //    // Arrange - Create The Taluka
        //    TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14, ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    mockTaluka.Setup(p => p.Amend(talukaViewModel)).Returns(Task.FromResult(true));

        //    var mockControllerContext = new Mock<ControllerContext>();
        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //    // Arrange - Create The controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //    {
        //        ControllerContext = mockControllerContext.Object
        //    };

        //    // Act - Try To Amend The Taluka
        //    ActionResult actionResult = await target.Amend(talukaViewModel, "Amend");

        //    // Assert - Check That The Repository Was Called
        //    mockTaluka.Verify(m => m.Amend(talukaViewModel));

        //    // Assert - Check That The Method Result Type
        //    Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        //}
        
        //[TestMethod]
        //public async Task Can_Create_Post_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.Save(talukaViewModel)).Returns(Task.FromResult(false));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Create The Taluka
        //        ActionResult actionResult = await target.Create(talukaViewModel);

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.Save(talukaViewModel));

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_Create_Valid_Entry()
        //{
        //    // Arrange - Create The Taluka
        //    TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    mockTaluka.Setup(p => p.Save(talukaViewModel)).Returns(Task.FromResult(true));

        //    var mockControllerContext = new Mock<ControllerContext>();
        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);

        //    // Arrange - Create The controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //    {
        //        ControllerContext = mockControllerContext.Object
        //    };

        //    // Act - Try To Save The Taluka
        //    ActionResult actionResult = await target.Create(talukaViewModel);

        //    // Assert - Check That The Repository Was Called
        //    mockTaluka.Verify(m => m.Save(talukaViewModel));

        //    // Assert - Check That The Method Result Type
        //    Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        //}

        //[TestMethod]
        //public async Task Can_Delete_Get_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        TalukaViewModel talukaViewModel = null;

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(talukaViewModel));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Delete The Taluka
        //        ActionResult actionResult = await target.Amend(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.GetRejectedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_Delete_Post_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Delete, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.Delete(talukaViewModel)).Returns(Task.FromResult(false));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Delete The Taluka
        //        ActionResult actionResult = await target.Amend(talukaViewModel, "Delete");

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.Delete(talukaViewModel));

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_Delete_Valid_Entry()
        //{
        //    // Arrange - Create The Taluka
        //    TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Delete, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    mockTaluka.Setup(p => p.Delete(talukaViewModel)).Returns(Task.FromResult(true));

        //    var mockControllerContext = new Mock<ControllerContext>();
        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //    // Arrange - Create The controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //    {
        //        ControllerContext = mockControllerContext.Object
        //    };
        //    var mockUrlHelper = new Mock<UrlHelper>();
        //    mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
        //    target.Url = mockUrlHelper.Object;

        //    // Act - Try To Delete The Taluka
        //    ActionResult actionResult = await target.Amend(talukaViewModel, "Delete");

        //    // Assert - Check That The Repository Was Called
        //    mockTaluka.Verify(m => m.Delete(talukaViewModel));

        //    // Assert - Check That The Method Result Type
        //    Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        //}
        
        //[TestMethod]
        //public async Task Can_Modify_Get_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        TalukaViewModel talukaViewModel = null;

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(talukaViewModel));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Modify The Taluka
        //        ActionResult actionResult = await target.Modify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.GetVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_Modify_Post_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.Modify(talukaViewModel)).Returns(Task.FromResult(false));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Modify The Taluka
        //        ActionResult actionResult = await target.Modify(talukaViewModel);

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.Modify(talukaViewModel));

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_Modify_Valid_Entry()
        //{
        //    // Arrange - Create The Taluka
        //    TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    mockTaluka.Setup(p => p.Modify(talukaViewModel)).Returns(Task.FromResult(true));

        //    var mockControllerContext = new Mock<ControllerContext>();
        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //    // Arrange - Create The controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //    {
        //        ControllerContext = mockControllerContext.Object
        //    };

        //    // Act - Try To Modify The Taluka
        //    ActionResult actionResult = await target.Modify(talukaViewModel);

        //    // Assert - Check That The Repository Was Called
        //    mockTaluka.Verify(m => m.Modify(talukaViewModel));

        //    // Assert - Check That The Method Result Type
        //    Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        //}

        //[TestMethod]
        //public async Task Can_Reject_Get_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        TalukaViewModel talukaViewModel = null;

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(talukaViewModel));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Reject The Taluka
        //        ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_Reject_Post_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.Reject(talukaViewModel)).Returns(Task.FromResult(false));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Reject The Taluka
        //        ActionResult actionResult = await target.Verify(talukaViewModel, "Reject");

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.Reject(talukaViewModel));

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_Reject_Valid_Entry()
        //{
        //    // Arrange - Create The Taluka
        //    TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    mockTaluka.Setup(p => p.Reject(talukaViewModel)).Returns(Task.FromResult(true));

        //    var mockControllerContext = new Mock<ControllerContext>();
        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //    // Arrange - Create The controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //    {
        //        ControllerContext = mockControllerContext.Object
        //    };
        //    var mockUrlHelper = new Mock<UrlHelper>();
        //    mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
        //    target.Url = mockUrlHelper.Object;

        //    // Act - Try To Reject The Taluka
        //    ActionResult actionResult = await target.Verify(talukaViewModel, "Reject");

        //    // Assert - Check That The Repository Was Called
        //    mockTaluka.Verify(m => m.Reject(talukaViewModel));

        //    // Assert - Check That The Method Result Type
        //    Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        //}

        //[TestMethod]
        //public async Task Can_RejectedIndex_Get_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        IEnumerable<TalukaViewModel> talukaViewModel = null;

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.GetIndexOfRejectedEntries()).Returns(Task.FromResult(talukaViewModel));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Reject The Taluka
        //        ActionResult actionResult = await target.RejectedIndex();

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.GetIndexOfRejectedEntries());

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}
        
        //[TestMethod]
        //public async Task Can_UnverifiedIndex_Get_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        IEnumerable<TalukaViewModel> talukaViewModel = null;

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();


        //        mockTaluka.Setup(p => p.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult(talukaViewModel));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Verify The Taluka
        //        ActionResult actionResult = await target.UnverifiedIndex();

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.GetIndexOfUnVerifiedEntries());

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_VerifiedIndex_Get_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        IEnumerable<TalukaViewModel> talukaViewModel = null;

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.GetIndexOfVerifiedEntries()).Returns(Task.FromResult(talukaViewModel));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Verify The Taluka
        //        ActionResult actionResult = await target.VerifiedIndex();

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.GetIndexOfVerifiedEntries());

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_Verify_Get_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        TalukaViewModel talukaViewModel = null;

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Returns(Task.FromResult(talukaViewModel));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Verify The Taluka
        //        ActionResult actionResult = await target.Verify(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"));

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.GetUnVerifiedEntry(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00")));

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_Verify_Post_Method_Throws_Exception()
        //{
        //    var expectedException = new DatabaseException();

        //    try
        //    {
        //        // Arrange - Create The Taluka
        //        TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //        // Arrange - Create The Mock Repository
        //        Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //        Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //        Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //        mockTaluka.Setup(p => p.Verify(talukaViewModel)).Returns(Task.FromResult(false));

        //        var mockControllerContext = new Mock<ControllerContext>();
        //        mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //        // Arrange - Create The controller
        //        TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //        {
        //            ControllerContext = mockControllerContext.Object
        //        };

        //        // Act - Try To Verify The Taluka
        //        ActionResult actionResult = await target.Verify(talukaViewModel, "Verify");

        //        // Assert - Check That The Repository Was Called
        //        mockTaluka.Verify(m => m.Verify(talukaViewModel));

        //        Assert.Fail("An exception was not thrown as expected.");
        //    }
        //    catch (Exception e)
        //    {
        //        // If The Exception Thrown Was From The Assert.Fail, Then Rethrow.
        //        if (e.GetType() == typeof(AssertFailedException)) throw;

        //        // Assert - Check That The Exception Type And Message
        //        Assert.AreEqual(expectedException.GetType(), e.GetType());
        //        Assert.AreEqual(expectedException.Message, e.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task Can_Verify_Valid_Entry()
        //{
        //    // Arrange - Create The Taluka
        //    TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    mockTaluka.Setup(p => p.Verify(talukaViewModel)).Returns(Task.FromResult(true));

        //    var mockControllerContext = new Mock<ControllerContext>();
        //    mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

        //    // Arrange - Create The controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object)
        //    {
        //        ControllerContext = mockControllerContext.Object
        //    };

        //    var mockUrlHelper = new Mock<UrlHelper>();
        //    mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<string>(), It.IsAny<object>()));
        //    target.Url = mockUrlHelper.Object;

        //    // Act - Try To Verify The Taluka
        //    ActionResult actionResult = await target.Verify(talukaViewModel, "Verify");

        //    // Assert - Check That The Repository Was Called
        //    mockTaluka.Verify(m => m.Verify(talukaViewModel));

        //    // Assert - Check That The Method Result Type
        //    Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
        //}
        
        //[TestMethod]
        //public async Task Cannot_Amend_InValid_Entry()
        //{
        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    // Arrange - Create The controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object);

        //    // Arrange - Create The Taluka
        //    TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //    // Arrange - Add  An Error To The Model State
        //    target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

        //    // Act - Try To Amend The Taluka
        //    ActionResult actionResult = await target.Amend(talukaViewModel, "Amend");

        //    // Assert - Check That The Repository Was Not Called
        //    mockTaluka.Verify(m => m.Amend(It.IsAny<TalukaViewModel>()), Times.Never());

        //    // Assert - Check That The Method Result Type
        //    Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        //}
        
        //[TestMethod]
        //public async Task Cannot_Create_InValid_Entry()
        //{
        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    // Arrange - Create The controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object);

        //    // Arrange - Create The Taluka
        //    TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //    // Arrange - Add  An Error To The Model State
        //    target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

        //    // Act - Try To Save The Taluka
        //    ActionResult actionResult = await target.Create(talukaViewModel);

        //    // Assert - Check That The Repository Was Not Called
        //    mockTaluka.Verify(m => m.Save(It.IsAny<TalukaViewModel>()), Times.Never());

        //    // Assert - Check That The Method Result Type
        //    Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        //}

        //[TestMethod]
        //public async Task Cannot_Delete_InValid_Entry()
        //{
        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    // Arrange - Create The controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object);

        //    // Arrange - Create The Taluka
        //    TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Delete, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Delete, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //    // Arrange - Add  An Error To The Model State
        //    target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

        //    // Act - Try To Amend The Taluka
        //    ActionResult actionResult = await target.Amend(talukaViewModel, "Delete");

        //    // Assert - Check That The Repository Was Not Called
        //    mockTaluka.Verify(m => m.Delete(It.IsAny<TalukaViewModel>()), Times.Never());

        //    // Assert - Check That The Method Result Type
        //    Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        //}
        
        //[TestMethod]
        //public async Task Cannot_Modify_InValid_Entry()
        //{
        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    // Arrange - Create The controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object);

        //    // Arrange - Create The Taluka
        //    TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //    // Arrange - Add  An Error To The Model State
        //    target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

        //    // Act - Try To Modify The Taluka
        //    ActionResult actionResult = await target.Modify(talukaViewModel);

        //    // Assert - Check That The Repository Was Not Called
        //    mockTaluka.Verify(m => m.Modify(It.IsAny<TalukaViewModel>()), Times.Never());

        //    // Assert - Check That The Method Result Type
        //    Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        //}

        //[TestMethod]
        //public async Task Cannot_Reject_InValid_Entry()
        //{
        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    // Arrange - Create The controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object);

        //    // Arrange - Create The Taluka
        //    TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //    // Arrange - Add  An Error To The Model State
        //    target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

        //    // Act - Try To Reject The Taluka
        //    ActionResult actionResult = await target.Verify(talukaViewModel, "Reject");

        //    // Assert - Check That The Repository Was Not Called
        //    mockTaluka.Verify(m => m.Reject(It.IsAny<TalukaViewModel>()), Times.Never());

        //    // Assert - Check That The Method Result Type
        //    Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        //}
        
        //[TestMethod]
        //public async Task Cannot_Verify_InValid_Entry()
        //{
        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    // Arrange - Create The controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object);

        //    // Arrange - Create The Taluka
        //    TalukaViewModel talukaViewModel = new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now };

        //    // Arrange - Add  An Error To The Model State
        //    target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

        //    // Act - Try To Verify The Taluka
        //    ActionResult actionResult = await target.Verify(talukaViewModel, "Verify");

        //    // Assert - Check That The Repository Was Not Called
        //    mockTaluka.Verify(m => m.Verify(It.IsAny<TalukaViewModel>()), Times.Never());

        //    // Assert - Check That The Method Result Type
        //    Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        //}
        
        //[TestMethod]
        //public async Task RejectedIndex_Contains_AllRejected_Entries()
        //{
        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    var IndexOfRejectedEntries = new TalukaViewModel[]
        //                                        {
        //                                           new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
        //                                           new TalukaViewModel { PrmKey = 2, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", NameOnReport = "Pune", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransNameOnReport = "पुणे", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "User", MakerEntryDateTime = DateTime.Now },
        //                                           new TalukaViewModel { PrmKey = 3, NameOfCenter = "Mumbai", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Mumbai", NameOnReport = "Mumbai", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Reject, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "मुंबई", TransAliasName = "मुंबई", TransNameOnReport = "मुंबई", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Super User", MakerEntryDateTime = DateTime.Now }
        //                                        }.ToList();

        //    mockTaluka.Setup(m => m.GetIndexOfRejectedEntries()).Returns(Task.FromResult<IEnumerable<TalukaViewModel>>(IndexOfRejectedEntries));

        //    // Arrange - create the controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object);

        //    // Action -target the controller
        //    var result = await target.RejectedIndex() as ViewResult;

        //    // Assert 
        //    Assert.AreEqual(IndexOfRejectedEntries.Count, 3);
        //    Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[0].EntryStatus);
        //    Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[1].EntryStatus);
        //    Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[2].EntryStatus);
        //}

        //[TestMethod]
        //public async Task UnverifiedIndex_Contains_AllUnverified_Entries()
        //{
        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    var IndexOfUnverifiedEntries = new TalukaViewModel[]
        //                                         {
        //                                           new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
        //                                           new TalukaViewModel { PrmKey = 2, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", NameOnReport = "Pune", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransNameOnReport = "पुणे", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "User", MakerEntryDateTime = DateTime.Now },
        //                                           new TalukaViewModel { PrmKey = 3, NameOfCenter = "Mumbai", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Mumbai", NameOnReport = "Mumbai", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Create, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "मुंबई", TransAliasName = "मुंबई", TransNameOnReport = "मुंबई", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Super User", MakerEntryDateTime = DateTime.Now }
        //                                        }.ToList();

        //    mockTaluka.Setup(m => m.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult<IEnumerable<TalukaViewModel>>(IndexOfUnverifiedEntries));

        //    // Arrange - create the controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object);

        //    // Action -target the controller
        //    var result = await target.UnverifiedIndex() as ViewResult;

        //    // Assert 
        //    Assert.AreEqual(IndexOfUnverifiedEntries.Count, 3);
        //    Assert.AreEqual(StringLiteralValue.Create, IndexOfUnverifiedEntries[0].EntryStatus);
        //    Assert.AreEqual(StringLiteralValue.Create, IndexOfUnverifiedEntries[1].EntryStatus);
        //    Assert.AreEqual(StringLiteralValue.Create, IndexOfUnverifiedEntries[2].EntryStatus);
        //}

        //[TestMethod]
        //public async Task VerifiedIndex_Contains_AllVerified_Entries()
        //{
        //    // Arrange - Create The Mock Repository
        //    Mock<IAddressParameterRepository> mockAddressParametr = new Mock<IAddressParameterRepository>();
        //    Mock<IVillageTownCityRepository> mockCenter = new Mock<IVillageTownCityRepository>();
        //    Mock<ITalukaRepository> mockTaluka = new Mock<ITalukaRepository>();

        //    var IndexOfVerifiedEntries = new TalukaViewModel[]
        //                                         {
        //                                           new TalukaViewModel { PrmKey = 1, NameOfCenter = "Satara", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Satara", NameOnReport = "Satara", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "सातारा", TransAliasName = "सातारा", TransNameOnReport = "सातारा", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Administrator", MakerEntryDateTime = DateTime.Now },
        //                                           new TalukaViewModel { PrmKey = 2, NameOfCenter = "Pune", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Pune", NameOnReport = "Pune", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "पुणे", TransAliasName = "पुणे", TransNameOnReport = "पुणे", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "User", MakerEntryDateTime = DateTime.Now },
        //                                           new TalukaViewModel { PrmKey = 3, NameOfCenter = "Mumbai", CenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), AliasName = "Mumbai", NameOnReport = "Mumbai", CenterCategoryPrmKey = 14,  ParentCenterPrmKey = 0, ParentCenterId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), Note = "None", IsModified = false, EntryStatus = StringLiteralValue.Verify, CenterTranslationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),  CenterPrmKey = 1, LanguagePrmKey = 2, TransModificationNumber = 0, TransNameOfCenter = "मुंबई", TransAliasName = "मुंबई", TransNameOnReport = "मुंबई", TransNote = "काहीही नाही",  CenterModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), ModificationNumber = 0, ReasonForModification = "Data Entry Mistake", EntryDateTime = DateTime.Now, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", CenterTranslationPrmKey = 1, CenterModificationPrmKey = 1,  NameOfUser = "Super User", MakerEntryDateTime = DateTime.Now }
        //                                        }.ToList();

        //    mockTaluka.Setup(m => m.GetIndexOfVerifiedEntries()).Returns(Task.FromResult<IEnumerable<TalukaViewModel>>(IndexOfVerifiedEntries));

        //    // Arrange - create the controller
        //    TalukaController target = new TalukaController(mockAddressParametr.Object, mockCenter.Object, mockTaluka.Object);

        //    // Action -target the controller
        //    var result = await target.VerifiedIndex() as ViewResult;

        //    // Assert 
        //    Assert.AreEqual(IndexOfVerifiedEntries.Count, 3);
        //    Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[0].EntryStatus);
        //    Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[1].EntryStatus);
        //    Assert.AreEqual(StringLiteralValue.Verify, IndexOfVerifiedEntries[2].EntryStatus);
        //}
    }
}
