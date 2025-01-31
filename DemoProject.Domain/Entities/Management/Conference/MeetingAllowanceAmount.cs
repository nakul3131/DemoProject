using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MeetingAllowanceAmount")]
    public partial class MeetingAllowanceAmount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeetingAllowanceAmount()
        {
            MeetingAllowanceAmountMakerCheckers = new HashSet<MeetingAllowanceAmountMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int MeetingAllowancePrmKey { get; set; }

        public int ActClause { get; set; }

        public decimal Amount { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public virtual MeetingAllowance MeetingAllowance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingAllowanceAmountMakerChecker> MeetingAllowanceAmountMakerCheckers { get; set; }
    }
}
