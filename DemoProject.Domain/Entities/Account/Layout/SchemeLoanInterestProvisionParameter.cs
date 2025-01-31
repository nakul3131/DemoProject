﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.GL;
using DemoProject.Domain.Entities.Account.SystemEntity;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanInterestProvisionParameter")]
    public partial class SchemeLoanInterestProvisionParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanInterestProvisionParameter()
        {
            SchemeLoanInterestProvisionParameterMakerCheckers = new HashSet<SchemeLoanInterestProvisionParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte InterestCalculationFrequencyPrmKey { get; set; }

        public bool EnableCapitalization { get; set; }

        public bool EnableDueInterestCapitalization { get; set; }

        public bool EnableOverdueAccountInterestCalculation { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        public virtual GeneralLedger GeneralLedger { get; set; }

        public virtual InterestCalculationFrequency InterestCalculationFrequency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanInterestProvisionParameterMakerChecker> SchemeLoanInterestProvisionParameterMakerCheckers { get; set; }
    }
}
