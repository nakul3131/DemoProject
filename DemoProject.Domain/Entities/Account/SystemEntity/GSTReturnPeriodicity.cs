using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("GSTReturnPeriodicity")]
    public partial class GSTReturnPeriodicity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GSTReturnPeriodicity()
        {
            GSTReturnPeriodicityTranslations = new HashSet<GSTReturnPeriodicityTranslation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public Guid GSTReturnPeriodicityId { get; set; }

        [Required]
        [StringLength(100)]
        public string SysNameOfPeriodicity { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfGSTReturnPeriodicity { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GSTReturnPeriodicityTranslation> GSTReturnPeriodicityTranslations { get; set; }
    }
}
