using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.Layout;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("InterestCalculationFrequency")]
    public partial class InterestCalculationFrequency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InterestCalculationFrequency()
        {
            InterestCalculationFrequencyTranslations = new HashSet<InterestCalculationFrequencyTranslation>();
            SchemeLoanInterestProvisionParameters = new HashSet<SchemeLoanInterestProvisionParameter>();
        }

        [Key]
        public byte PrmKey { get; set; }

        public Guid InterestCalculationFrequencyId { get; set; }

        [Required]
        [StringLength(1500)]
        public string NameOfFrequency { get; set; }

        public short CompoundedTimeInYear { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InterestCalculationFrequencyTranslation> InterestCalculationFrequencyTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanInterestProvisionParameter> SchemeLoanInterestProvisionParameters { get; set; }        
    }
}
