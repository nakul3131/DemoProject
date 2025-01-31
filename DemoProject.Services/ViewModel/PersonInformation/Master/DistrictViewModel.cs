using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
    public class DistrictViewModel
    {
        // Center
        public short PrmKey { get; set; }

        public Guid CenterId { get; set; }

        public byte CenterCategoryPrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfCenter { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short ParentCenterPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // CenterMakerCkecker
        public DateTime EntryDateTime { get; set; }

        public short CenterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // CenterModification
        public Guid CenterModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // CenterModificationMakerChecker
        public short CenterModificationPrmKey { get; set; }

        // CenterISOCode 
        public CenterIsoCodeViewModel CenterIsoCodeViewModel { get; set; }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // CenterTranslation 
        public Guid CenterTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfCenter { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        // CenterTranslationMakerChecker
        public short CenterTranslationPrmKey { get; set; }

        // Dropdown
        public Guid ParentCenterId { get; set; }
    }
}