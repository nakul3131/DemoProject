//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using DemoProject.Services.ViewModel.Security.Password;
//using DemoProject.WebUI.Controllers;
//using DemoProject.Services.Constants;
//using DemoProject.Services.Abstract.Security.Master;

//namespace DemoProject.UnitTests.Controllers
//{
//    [TestClass]
//    public class PasswordPolicyControllerTest
//    {
//        [TestMethod]
//        public async Task Can_Create_Valid_Entry()
//        {
//            // Arrange - Create The PasswordPolicy
//            PasswordPolicyViewModel passwordPolicyViewModel = new PasswordPolicyViewModel { PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch", Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1, EntryDateTime = new DateTime(01 / 01 / 2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            mock.Setup(p => p.Save(passwordPolicyViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)11);


//            // Arrange - Create The controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            // Act - Try To Save The PasswordPolicy
//            ActionResult actionResult = await target.Create(passwordPolicyViewModel);

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Save(passwordPolicyViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Create_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            // Arrange - Create The controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object);

//            // Arrange - Create The PasswordPolicy
//            PasswordPolicyViewModel passwordPolicyViewModel = new PasswordPolicyViewModel { PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch", Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1, EntryDateTime = new DateTime(01 / 01 / 2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Save The PasswordPolicy
//            ActionResult actionResult = await target.Create(passwordPolicyViewModel);

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Save(It.IsAny<PasswordPolicyViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Can_Verify_Valid_Entry()
//        {
//            // Arrange - Create The PasswordPolicy
//            PasswordPolicyViewModel passwordPolicyViewModel = new PasswordPolicyViewModel { PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch", Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1, EntryDateTime = new DateTime(01 / 01 / 2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            mock.Setup(p => p.Verify(passwordPolicyViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);


//            // Arrange - Create The controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            // Act - Try To Save The PasswordPolicy
//            ActionResult actionResult = await target.Verify(passwordPolicyViewModel, "Verify");

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Verify(passwordPolicyViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Verify_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            // Arrange - Create The controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object);

//            // Arrange - Create The PasswordPolicy
//            PasswordPolicyViewModel passwordPolicyViewModel = new PasswordPolicyViewModel { PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch", Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1, EntryDateTime = new DateTime(01 / 01 / 2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Verify The PasswordPolicy
//            ActionResult actionResult = await target.Verify(passwordPolicyViewModel, "Verify");

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Verify(It.IsAny<PasswordPolicyViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Can_Reject_Valid_Entry()
//        {
//            // Arrange - Create The PasswordPolicy
//            PasswordPolicyViewModel passwordPolicyViewModel = new PasswordPolicyViewModel { PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch", Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1, EntryDateTime = new DateTime(01 / 01 / 2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", NameOfUser = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            mock.Setup(p => p.Reject(passwordPolicyViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);


//            // Arrange - Create The controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            // Act - Try To Save The PasswordPolicy
//            ActionResult actionResult = await target.Verify(passwordPolicyViewModel, "Reject");

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Reject(passwordPolicyViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Reject_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            // Arrange - Create The controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object);

//            // Arrange - Create The PasswordPolicy
//            PasswordPolicyViewModel passwordPolicyViewModel = new PasswordPolicyViewModel { PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch", Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1, EntryDateTime = new DateTime(01 / 01 / 2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", NameOfUser = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Reject The PasswordPolicy
//            ActionResult actionResult = await target.Verify(passwordPolicyViewModel, "Reject");

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Reject(It.IsAny<PasswordPolicyViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Can_Amend_Valid_Entry()
//        {
//            // Arrange - Create The PasswordPolicy
//            PasswordPolicyViewModel passwordPolicyViewModel = new PasswordPolicyViewModel { PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch", Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1, EntryDateTime = new DateTime(01 / 01 / 2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = "A", Remark = "None", NameOfUser = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            mock.Setup(p => p.Amend(passwordPolicyViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//            // Arrange - Create The controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };

//            // Act - Try To Amend The PasswordPolicy
//            ActionResult actionResult = await target.Amend(passwordPolicyViewModel, "Amend");

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Amend(passwordPolicyViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Amend_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            // Arrange - Create The controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object);

//            // Arrange - Create The PasswordPolicy
//            PasswordPolicyViewModel passwordPolicyViewModel = new PasswordPolicyViewModel { PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch", Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1, EntryDateTime = new DateTime(01 / 01 / 2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = "A", Remark = "None", NameOfUser = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Amend The PasswordPolicy
//            ActionResult actionResult = await target.Amend(passwordPolicyViewModel, "Amend");

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Amend(It.IsAny<PasswordPolicyViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Can_Delete_Valid_Entry()
//        {
//            // Arrange - Create The PasswordPolicy
//            PasswordPolicyViewModel passwordPolicyViewModel = new PasswordPolicyViewModel { PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch", Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = "D", ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1, EntryDateTime = new DateTime(01 / 01 / 2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = "D", Remark = "None", NameOfUser = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            mock.Setup(p => p.Delete(passwordPolicyViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//            // Arrange - Create The controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };
//            // Act - Try To Amend The PasswordPolicy
//            ActionResult actionResult = await target.Amend(passwordPolicyViewModel, "Delete");

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Delete(passwordPolicyViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Delete_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            // Arrange - Create The controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object);

//            // Arrange - Create The PasswordPolicy
//            PasswordPolicyViewModel passwordPolicyViewModel = new PasswordPolicyViewModel { PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch", Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = "D", ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1, EntryDateTime = new DateTime(01 / 01 / 2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = "D", Remark = "None", NameOfUser = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Amend The PasswordPolicy
//            ActionResult actionResult = await target.Amend(passwordPolicyViewModel, "Delete");

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Delete(It.IsAny<PasswordPolicyViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Can_Modify_Valid_Entry()
//        {
//            // Arrange - Create The PasswordPolicy
//            PasswordPolicyViewModel passwordPolicyViewModel = new PasswordPolicyViewModel { PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch", Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1, EntryDateTime = new DateTime(01 / 01 / 2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "Administrator" };

//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            mock.Setup(p => p.Modify(passwordPolicyViewModel)).Returns(Task.FromResult(true));

//            var mockControllerContext = new Mock<ControllerContext>();
//            mockControllerContext.Setup(p => p.HttpContext.Session["UserProfilePrmKey"]).Returns((short)9);

//            // Arrange - Create The controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object)
//            {
//                ControllerContext = mockControllerContext.Object
//            };
//            // Act - Try To Save The PasswordPolicy
//            ActionResult actionResult = await target.Modify(passwordPolicyViewModel);

//            // Assert - Check That The Repository Was Called
//            mock.Verify(m => m.Modify(passwordPolicyViewModel));

//            // Assert - Check That The Method Result Type
//            Assert.IsNotInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task Cannot_Modify_InValid_Entry()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            // Arrange - Create The controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object);

//            // Arrange - Create The PasswordPolicy
//            PasswordPolicyViewModel passwordPolicyViewModel = new PasswordPolicyViewModel { PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01 / 01 / 2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch", Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1, EntryDateTime = new DateTime(01 / 01 / 2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "Administrator" };

//            // Arrange - Add  An Error To The Model State
//            target.ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record");

//            // Act - Try To Save The PasswordPolicy
//            ActionResult actionResult = await target.Modify(passwordPolicyViewModel);

//            // Assert - Check That The Repository Was Not Called
//            mock.Verify(m => m.Modify(It.IsAny<PasswordPolicyViewModel>()), Times.Never());

//            // Assert - Check That The Method Result Type
//            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
//        }

//        [TestMethod]
//        public async Task RejectedIndex_Contains_AllRejected_Entries()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            var IndexOfRejectedEntries = new PasswordPolicyViewModel[]
//                                                {
//                                                   new PasswordPolicyViewModel{ PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01/01/2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch",Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1,  EntryDateTime = new DateTime(01/01/2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Reject, Remark = "None", NameOfUser = "Administrator"},
//                                                   new PasswordPolicyViewModel{ PrmKey = 2, PasswordPolicyId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01/01/2020), NameOfPasswordPolicy = "Analyst", AliasName = "Anlyst", NameOnReport = "Analyst",Note = "None", MinimumPasswordLength = 2, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 1, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = true, ModificationNumber =0, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 2, EntryDateTime = new DateTime(01/01/2020), PasswordPolicyPrmKey = 2, UserProfilePrmKey = 2, UserAction = StringLiteralValue.Reject, Remark = "None", NameOfUser = "User1"},
//                                                   new PasswordPolicyViewModel{ PrmKey = 3, PasswordPolicyId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01/01/2020), NameOfPasswordPolicy = "Executive", AliasName = "CEO", NameOnReport = "Executive",Note = "None", MinimumPasswordLength = 3, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 2, ReusePreviousPassword = 1, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified =false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Reject, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 2, EntryDateTime = new DateTime(01/01/2020), PasswordPolicyPrmKey = 3, UserProfilePrmKey = 3, UserAction = StringLiteralValue.Reject, Remark = "None", NameOfUser = "User2"},
//                                                }.ToList();

//            mock.Setup(m => m.GetIndexOfRejectedEntries()).Returns(Task.FromResult<IEnumerable<PasswordPolicyViewModel>>(IndexOfRejectedEntries));

//            // Arrange - create the controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object);

//            // Action -target the controller
//            var result = await target.RejectedIndex() as ViewResult;

//            // Assert 
//            Assert.AreEqual(IndexOfRejectedEntries.Count, 3);
//            Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[0].EntryStatus);
//            Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[1].EntryStatus);
//            Assert.AreEqual(StringLiteralValue.Reject, IndexOfRejectedEntries[2].EntryStatus);
//        }

//        [TestMethod]
//        public async Task UnVerifiedIndex_Contains_AllUnVerified_Entries()
//        {
//            // Arrange - Create The Mock Repository
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            var IndexOfUnVerifiedEntries = new PasswordPolicyViewModel[]
//                                                {
//                                                   new PasswordPolicyViewModel{ PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01/01/2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch",Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1,  EntryDateTime = new DateTime(01/01/2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "Administrator"},
//                                                   new PasswordPolicyViewModel{ PrmKey = 2, PasswordPolicyId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01/01/2020), NameOfPasswordPolicy = "Analyst", AliasName = "Anlyst", NameOnReport = "Analyst",Note = "None", MinimumPasswordLength = 2, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 1, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = true, ModificationNumber =0, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 2, EntryDateTime = new DateTime(01/01/2020), PasswordPolicyPrmKey = 2, UserProfilePrmKey = 2, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "User1"},
//                                                   new PasswordPolicyViewModel{ PrmKey = 3, PasswordPolicyId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01/01/2020), NameOfPasswordPolicy = "Executive", AliasName = "CEO", NameOnReport = "Executive",Note = "None", MinimumPasswordLength = 3, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 2, ReusePreviousPassword = 1, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified =false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Create, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 2, EntryDateTime = new DateTime(01/01/2020), PasswordPolicyPrmKey = 3, UserProfilePrmKey = 3, UserAction = StringLiteralValue.Create, Remark = "None", NameOfUser = "User2"},
//                                               }.ToList();

//            mock.Setup(m => m.GetIndexOfUnVerifiedEntries()).Returns(Task.FromResult<IEnumerable<PasswordPolicyViewModel>>(IndexOfUnVerifiedEntries));

//            // Arrange - create the controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object);

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
//            Mock<IPasswordPolicyRepository> mock = new Mock<IPasswordPolicyRepository>();

//            var IndexOfVerifiedEntries = new PasswordPolicyViewModel[]
//                                                {
//                                                   new PasswordPolicyViewModel{ PrmKey = 1, PasswordPolicyId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01/01/2020), NameOfPasswordPolicy = "Manager", AliasName = "Mngr", NameOnReport = "Branch",Note = "None", MinimumPasswordLength = 1, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 2, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 1,  EntryDateTime = new DateTime(01/01/2020), PasswordPolicyPrmKey = 1, UserProfilePrmKey = 1, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "Administrator"},
//                                                   new PasswordPolicyViewModel{ PrmKey = 2, PasswordPolicyId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01/01/2020), NameOfPasswordPolicy = "Analyst", AliasName = "Anlyst", NameOnReport = "Analyst",Note = "None", MinimumPasswordLength = 2, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 1, ReusePreviousPassword = 1, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified = true, ModificationNumber =0, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("22223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 2, EntryDateTime = new DateTime(01/01/2020), PasswordPolicyPrmKey = 2, UserProfilePrmKey = 2, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "User1"},
//                                                   new PasswordPolicyViewModel{ PrmKey = 3, PasswordPolicyId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), EffectiveDate = new DateTime(01/01/2020), NameOfPasswordPolicy = "Executive", AliasName = "CEO", NameOnReport = "Executive",Note = "None", MinimumPasswordLength = 3, MaximumPasswordLength = 1, MinimumNumberOfUpperCaseCharacters = 1, MinimumNumberOfLowerCaseCharacters = 1, MinimumNumberOfSpecialCaseCharacters = 2, MinimumNumberOfNumericCharacters = 1, MinimumNumberOfRepetitiveCharacters = 1, ForcePasswordChangeAfterDays = 2, ReusePreviousPassword = 1, MinimumDaysForReusePreviousPassword = 1, PasswordExpiryAlertDays = 1, IsModified =false, ModificationNumber = 0, EntryStatus = StringLiteralValue.Verify, ActivationStatus = "INA", PasswordPolicyModificationId = Guid.Parse("33223344-5566-7788-99AA-BBCCDDEEFF00"), PasswordPolicyModificationPrmKey = 2, EntryDateTime = new DateTime(01/01/2020), PasswordPolicyPrmKey = 3, UserProfilePrmKey = 3, UserAction = StringLiteralValue.Verify, Remark = "None", NameOfUser = "User2"},
//                                                }.ToList();

//            mock.Setup(m => m.GetIndexOfVerifiedEntries()).Returns(Task.FromResult<IEnumerable<PasswordPolicyViewModel>>(IndexOfVerifiedEntries));

//            // Arrange - create the controller
//            PasswordPolicyController target = new PasswordPolicyController(mock.Object);

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
