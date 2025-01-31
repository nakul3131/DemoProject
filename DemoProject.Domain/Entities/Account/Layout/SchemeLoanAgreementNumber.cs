using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanAgreementNumber")]
    public partial class SchemeLoanAgreementNumber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanAgreementNumber()
        {
            SchemeLoanAgreementNumberMakerCheckers = new HashSet<SchemeLoanAgreementNumberMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnableAgreementNumberBranchwise { get; set; }

        [Required]
        [StringLength(25)]
        public string AgreementNumberMask { get; set; }

        public int StartAgreementNumber { get; set; }

        public int EndAgreementNumber { get; set; }

        public int AgreementNumberIncrementBy { get; set; }

        public bool EnableRandomAgreementNumber { get; set; }

        public bool EnableAutoAgreementNumber { get; set; }

        public bool EnableCustomizeAgreementNumber { get; set; }

        public bool EnableReGenerateUnusedAgreementNumber { get; set; }

        public bool EnableDigitalCodeForAgreementNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanAgreementNumberMakerChecker> SchemeLoanAgreementNumberMakerCheckers { get; set; }
    }
}
