using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation;

namespace DemoProject.Domain.Entities.Management.SystemEntity
{
    [Table("EmployerNature")]
    public partial class EmployerNature
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployerNature()
        {
            PersonEmployementDetails = new HashSet<PersonEmploymentDetail>();
            EmployerNatureTranslations = new HashSet<EmployerNatureTranslation>();
        }

        [Key]
        public byte PrmKey { get; set; }

        public Guid EmployerNatureId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfEmployerNature { get; set; }

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
        public virtual ICollection<EmployerNatureTranslation> EmployerNatureTranslations { get; set; }
    }
}
