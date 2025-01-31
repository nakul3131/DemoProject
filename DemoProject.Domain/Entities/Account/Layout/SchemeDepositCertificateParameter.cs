using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeDepositCertificateParameter")]
    public partial class SchemeDepositCertificateParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeDepositCertificateParameter()
        {
            SchemeDepositCertificateParameterMakerCheckers = new HashSet<SchemeDepositCertificateParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnableCertificateNumberBranchwise { get; set; }

        [Required]
        [StringLength(25)]
        public string CertificateNumberMask { get; set; }

        public bool EnableDigitalCodeForCertificate { get; set; }

        public bool EnableAutoCertificateNumber { get; set; }

        public bool EnableCustomizeCertificateNumber { get; set; }

        public int StartCertificateNumber { get; set; }

        public int EndCertificateNumber { get; set; }

        public int CertificateNumberIncrementBy { get; set; }

        public bool IsRandomlyGeneratedCertificateNumber { get; set; }

        public bool ReGenerateUnusedCertificateNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositCertificateParameterMakerChecker> SchemeDepositCertificateParameterMakerCheckers { get; set; }
    }
}
