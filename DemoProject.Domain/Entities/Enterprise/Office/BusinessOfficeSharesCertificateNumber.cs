using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeSharesCertificateNumber")]
    public partial class BusinessOfficeSharesCertificateNumber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeSharesCertificateNumber()
        {
            BusinessOfficeSharesCertificateNumberMakerCheckers = new HashSet<BusinessOfficeSharesCertificateNumberMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public bool EnableAutoSharesCertificateNumber { get; set; }

        [Required]
        [StringLength(25)]
        public string SharesCertificateNumberMask { get; set; }

        public int StartSharesCertificateNumber { get; set; }

        public int EndSharesCertificateNumber { get; set; }

        public int SharesCertificateNumberIncrementBy { get; set; }

        public bool EnableReGenerateUnusedSharesCertificateNumber { get; set; }

        public bool EnableRandomSharesCertificateNumber { get; set; }

        public bool EnableCustomizeSharesCertificateNumber { get; set; }

        public bool EnableDigitalCodeForSharesCertificateNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual BusinessOffice BusinessOffice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeSharesCertificateNumberMakerChecker> BusinessOfficeSharesCertificateNumberMakerCheckers { get; set; }
    }
}
