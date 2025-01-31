using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("PeriodCode")]
    public partial class PeriodCode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PeriodCode()
        {
            PeriodCodeMakerCheckers = new HashSet<PeriodCodeMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid PeriodCodeId { get; set; }

        public byte FinancialCyclePrmKey { get; set; }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(3)]
        public string PeriodCodeStatus { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual FinancialCycle FinancialCycle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PeriodCodeMakerChecker> PeriodCodeMakerCheckers { get; set; }
    }
}
