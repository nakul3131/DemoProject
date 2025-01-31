using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("PersonType")]
    public partial class PersonType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonType()
        {
            PersonTypeTranslations = new HashSet<PersonTypeTranslation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public Guid PersonTypeId { get; set; }

        [Required]
        [StringLength(5)]
        public string SysNameOfPersonType { get; set; }

        [Required]
        [StringLength(1500)]
        public string NameOfPersonType { get; set; }

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
        public virtual ICollection<PersonTypeTranslation> PersonTypeTranslations { get; set; }
    }
}
