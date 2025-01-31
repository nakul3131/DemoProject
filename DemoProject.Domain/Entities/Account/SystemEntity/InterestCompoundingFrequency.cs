using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("InterestCompoundingFrequency")]
    public partial class InterestCompoundingFrequency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InterestCompoundingFrequency()
        {
            InterestCompoundingFrequencyTranslations = new HashSet<InterestCompoundingFrequencyTranslation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public Guid InterestCompoundingFrequencyId { get; set; }

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
        public virtual ICollection<InterestCompoundingFrequencyTranslation> InterestCompoundingFrequencyTranslations { get; set; }
    }
}
