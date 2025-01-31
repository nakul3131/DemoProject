using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Security.UserRoles
{
    public class RoleProfileViewModel
    {
        //RoleProfile
        public short PrmKey { get; set; }

        public Guid RoleProfileId { get; set; }

        [StringLength(20)]
        public string RoleProfileCode { get; set; }

        [StringLength(50)]
        public string NameOfRoleProfile { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(100)]
        public string NameOnReport { get; set; }

        public bool IsFixedRole { get; set; }

        public bool IsCentralizeRole { get; set; }

        public bool IsAllowAllAccessForBusinessOffice { get; set; }

        public bool IsAllowAllAccessForGeneralLedger { get; set; }

        public bool IsAllowAllAccessForMenu { get; set; }

        public bool IsAllowAllAccessForSpecialPermission { get; set; }

        public bool IsAllowAllTransactions { get; set; }

        [StringLength(1)]
        public string GeneralLedgerAccessType { get; set; }

        [StringLength(1)]
        public string BusinessOfficeAccessType { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //RoleProfileMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short RoleProfilePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //RoleProfileModification

        public Guid RoleProfileModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        //RoleProfileModificationMakerChecker

        public short RoleProfileModificationPrmKey { get; set; }

        //RoleProfileTranslation

        public Guid RoleProfileTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(100)]
        public string TransNameOfRoleProfile { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        //RoleProfileTranslationMakerChecker

        public short RoleProfileTranslationPrmKey { get; set; }

        // RoleProfileGeneralLedger 
        public RoleProfileGeneralLedgerViewModel RoleProfileGeneralLedgerViewModel { get; set; }

        // RoleProfileBusinessOffice 
        public RoleProfileBusinessOfficeViewModel RoleProfileBusinessOfficeViewModel { get; set; }

        // RoleProfileTransactionLimit 
        public RoleProfileTransactionLimitViewModel RoleProfileTransactionLimitViewModel { get; set; }

        // RoleProfileMenu 
        public RoleProfileMenuViewModel RoleProfileMenuViewModel { get; set; }

        // RoleProfileSpecialPermission 
        public RoleProfileSpecialPermissionViewModel RoleProfileSpecialPermissionViewModel { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // For SelectListItem

        public Guid[] BusinessOfficeId { get; set; }

        public Guid[] GeneralLedgerId { get; set; }

        public Guid ModelMenuId { get; set; }

        public Guid MainMenuId { get; set; }

        public Guid SubMenuId { get; set; }

        public Guid SpecialPermissionId { get; set; }

        public Guid TransactionTypeId { get; set; }

        public Guid CurrencyId { get; set; }

        public IList<SelectListItem> MenuModels { get; set; }
    }
}
