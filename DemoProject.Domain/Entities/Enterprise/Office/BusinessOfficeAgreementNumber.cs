using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.GL;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeAgreementNumber")]
    public partial class BusinessOfficeAgreementNumber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeAgreementNumber()
        {
            BusinessOfficeAgreementNumberMakerCheckers = new HashSet<BusinessOfficeAgreementNumberMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool EnableAutoAgreementNumber { get; set; }

        [Required]
        [StringLength(25)]
        public string AgreementNumberMask { get; set; }

        public int StartAgreementNumber { get; set; }

        public int EndAgreementNumber { get; set; }

        public int AgreementNumberIncrementBy { get; set; }

        public bool EnableRandomAgreementNumber { get; set; }

        public bool EnableCustomizeAgreementNumber { get; set; }

        public bool EnableReGenerateUnusedAgreementNumber { get; set; }

        public bool EnableDigitalCodeForAgreementNumber { get; set; }

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
        public virtual ICollection<BusinessOfficeAgreementNumberMakerChecker> BusinessOfficeAgreementNumberMakerCheckers { get; set; }
    }
}
