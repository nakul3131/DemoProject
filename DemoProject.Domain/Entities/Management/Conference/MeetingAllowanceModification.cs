using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MeetingAllowanceModification")]
    public partial class MeetingAllowanceModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeetingAllowanceModification()
        {
            MeetingAllowanceModificationMakerCheckers = new HashSet<MeetingAllowanceModificationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int MeetingAllowancePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

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

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingAllowanceModificationMakerChecker> MeetingAllowanceModificationMakerCheckers { get; set; }
    }
}
