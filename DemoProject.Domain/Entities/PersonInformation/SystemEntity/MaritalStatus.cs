using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("MaritalStatus")]
    public partial class MaritalStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaritalStatus()
        {
            PersonAdditionalDetails = new HashSet<PersonAdditionalDetail>();
            MaritalStatusTranslations = new HashSet<MaritalStatusTranslation>();
        }

        [Key]
        public byte PrmKey { get; set; }

        public Guid MaritalStatusId { get; set; }

        [Required]
        [StringLength(6)]
        public string SysNameOfMaritalStatus { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfMaritalStatus { get; set; }

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
        public virtual ICollection<PersonAdditionalDetail> PersonAdditionalDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaritalStatusTranslation> MaritalStatusTranslations { get; set; }
    }
}
