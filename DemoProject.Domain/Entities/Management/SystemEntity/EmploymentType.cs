using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation;

namespace DemoProject.Domain.Entities.Management.SystemEntity
{
    [Table("EmploymentType")]
    public partial class EmploymentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmploymentType()
        {
            PersonEmployementDetails = new HashSet<PersonEmploymentDetail>();
            EmploymentTypeTranslations = new HashSet<EmploymentTypeTranslation>();
        }

        [Key]
        public byte PrmKey { get; set; }

        public Guid EmploymentTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfEmploymentType { get; set; }

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
        public virtual ICollection<PersonEmploymentDetail> PersonEmployementDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmploymentTypeTranslation> EmploymentTypeTranslations { get; set; }
    }
}
