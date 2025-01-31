using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation.Master;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("CenterCategory")]
    public partial class CenterCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CenterCategory()
        {
            Centers = new HashSet<Center>();
            CenterCategoryTranslations = new HashSet<CenterCategoryTranslation>();
        }

        [Key]
        public byte PrmKey { get; set; }

        public Guid CenterCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfCenterCategory { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

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
        public virtual ICollection<Center> Centers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CenterCategoryTranslation> CenterCategoryTranslations { get; set; }
    }
}
