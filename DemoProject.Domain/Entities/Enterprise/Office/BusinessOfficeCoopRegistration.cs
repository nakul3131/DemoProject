using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeCoopRegistration")]
    public partial class BusinessOfficeCoopRegistration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeCoopRegistration()
        {
            BusinessOfficeCoopRegistrationMakerCheckers = new HashSet<BusinessOfficeCoopRegistrationMakerChecker>();
            BusinessOfficeCoopRegistrationTranslations = new HashSet<BusinessOfficeCoopRegistrationTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ApprovalDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required]
        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        public short NumericCode { get; set; }

        [Required]
        [StringLength(20)]
        public string AlphaNumericCode { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeCoopRegistrationMakerChecker> BusinessOfficeCoopRegistrationMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeCoopRegistrationTranslation> BusinessOfficeCoopRegistrationTranslations { get; set; }
    }
}
