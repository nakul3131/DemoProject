using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("InterestMethod")]
    public partial class InterestMethod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InterestMethod()
        {
            InterestMethodTranslations = new HashSet<InterestMethodTranslation>();
        }

        [Key]
        public byte PrmKey { get; set; }

        public Guid InterestMethodId { get; set; }

        [Required]
        [StringLength(10)]
        public string SysNameOfInterestMethod { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfInterestMethod { get; set; }

        [Required]
        [StringLength(1500)]
        public string Formula { get; set; }

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
        public virtual ICollection<InterestMethodTranslation> InterestMethodTranslations { get; set; }
    }
}
