using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class DepartmentViewModel
    {
        // Department

        public short PrmKey { get; set; }

        public Guid DepartmentId { get; set; }

        [StringLength(100)]
        public string NameOfDepartment { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(100)]
        public string NameOnReport { get; set; }

        [StringLength(4000)]
        public string Objective { get; set; }

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

        // DepartmentMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short DepartmentPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DepartmentModification

        public Guid DepartmentModificationId { get; set; }

        public short ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // DepartmentModificationMakerChecker

        public short DepartmentModificationPrmKey { get; set; }

        // DepartmentTranslation

        public Guid DepartmentTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public short TransModificationNumber { get; set; }

        [StringLength(100)]
        public string TransNameOfDepartment { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(4000)]
        public string TransObjective { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        // DepartmentTranslationMakerChecker

        public short DepartmentTranslationPrmKey { get; set; }

        // Other

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}