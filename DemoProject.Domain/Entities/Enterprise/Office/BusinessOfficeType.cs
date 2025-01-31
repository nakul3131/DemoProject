using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeType")]
    public partial class BusinessOfficeType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeType()
        {
            BusinessOfficeTypeMakerCheckers = new HashSet<BusinessOfficeTypeMakerChecker>();
            BusinessOfficeTypeTranslations = new HashSet<BusinessOfficeTypeTranslation>();
            BusinessOfficeDetails = new HashSet<BusinessOfficeDetail>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid BusinessOfficeTypeId { get; set; }

        [Required]
        [StringLength(10)]
        public string SysNameOfBusinessOffice { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfBusinessOfficeType { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public byte Code { get; set; }

        [Required]
        [StringLength(2)]
        public string OfficeCategory { get; set; }

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
        public virtual ICollection<BusinessOfficeTypeMakerChecker> BusinessOfficeTypeMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeTypeTranslation> BusinessOfficeTypeTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeDetail> BusinessOfficeDetails { get; set; }
    }
}
