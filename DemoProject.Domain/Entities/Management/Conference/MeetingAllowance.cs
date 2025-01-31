using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Management.SystemEntity;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MeetingAllowance")]
    public partial class MeetingAllowance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeetingAllowance()
        {
            MeetingAllowanceAmounts = new HashSet<MeetingAllowanceAmount>();
            MeetingAllowanceMakerCheckers = new HashSet<MeetingAllowanceMakerChecker>();
            MeetingAllowanceModifications = new HashSet<MeetingAllowanceModification>();
            MeetingAllowanceTranslations = new HashSet<MeetingAllowanceTranslation>();
        }

        [Key]
        public int PrmKey { get; set; }

        public byte MeetingTypePrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfAllowance { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string ShortDescription { get; set; }

        public bool IsDailyAllowance { get; set; }

        public bool IsRequiredBill { get; set; }

        public decimal MinimumAllowanceAmount { get; set; }

        public decimal MaximumAllowanceAmount { get; set; }

        public decimal DefaultAllowanceAmount { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public virtual MeetingType MeetingType { get; set; } 

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingAllowanceAmount> MeetingAllowanceAmounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingAllowanceMakerChecker> MeetingAllowanceMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingAllowanceModification> MeetingAllowanceModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingAllowanceTranslation> MeetingAllowanceTranslations { get; set; }
    }
}
