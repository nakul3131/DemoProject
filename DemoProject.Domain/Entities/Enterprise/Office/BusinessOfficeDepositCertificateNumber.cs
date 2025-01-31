using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.GL;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeDepositCertificateNumber")]
    public partial class BusinessOfficeDepositCertificateNumber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeDepositCertificateNumber()
        {
            BusinessOfficeDepositCertificateNumberMakerCheckers = new HashSet<BusinessOfficeDepositCertificateNumberMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool EnableAutoCertificateNumber { get; set; }

        [Required]
        [StringLength(25)]
        public string CertificateNumberMask { get; set; }

        public int StartCertificateNumber { get; set; }

        public int EndCertificateNumber { get; set; }

        public int CertificateNumberIncrementBy { get; set; }

        public bool EnableReGenerateUnusedCertificateNumber { get; set; }

        public bool EnableRandomCertificateNumber { get; set; }

        public bool EnableCustomizeCertificateNumber { get; set; }

        public bool EnableDigitalCodeForCertificateNumber { get; set; }

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

        public virtual GeneralLedger GeneralLedger { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeDepositCertificateNumberMakerChecker> BusinessOfficeDepositCertificateNumberMakerCheckers { get; set; }
    }
}
