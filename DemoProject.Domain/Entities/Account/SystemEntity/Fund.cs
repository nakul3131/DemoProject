using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("Fund")]
    public partial class Fund
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fund()
        {
            FundTranslations = new HashSet<FundTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid FundId { get; set; }

        [Required]
        [StringLength(150)]
        public string NameOfFund { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(150)]
        public string NameOnReport { get; set; }

        public short SequenceNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string SequenceNumberText { get; set; }

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
        public virtual ICollection<FundTranslation> FundTranslations { get; set; }
    }
}
