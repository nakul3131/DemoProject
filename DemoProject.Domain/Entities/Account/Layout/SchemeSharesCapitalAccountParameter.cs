using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{

    [Table("SchemeSharesCapitalAccountParameter")]
    public partial class SchemeSharesCapitalAccountParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeSharesCapitalAccountParameter()
        {
            SchemeSharesCapitalAccountParameterMakerCheckers = new HashSet<SchemeSharesCapitalAccountParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnableMemberNumberBranchwise { get; set; }

        public bool EnableDigitalCodeForMemberNumber { get; set; }

        public bool IsVisibleMemberNumber { get; set; }

        [Required]
        [StringLength(25)]
        public string MemberNumberMask { get; set; }

        public bool EnableAutoMemberNumber { get; set; }

        public bool EnableCustomizeMemberNumber { get; set; }

        public int StartMemberNumber { get; set; }

        public int EndMemberNumber { get; set; }

        public int MemberNumberIncrementBy { get; set; }

        public bool EnableRandomMemberNumber { get; set; }

        public bool EnableReGenerateUnusedMemberNumber { get; set; }

        public short MinimumNumberOfShares { get; set; }

        public short MaximumNumberOfShares { get; set; }

        public short DefaultNumberOfShares { get; set; }

        public bool EnableDividend { get; set; }

        public bool EnableSharesTransferCharges { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeSharesCapitalAccountParameterMakerChecker> SchemeSharesCapitalAccountParameterMakerCheckers { get; set; }
    }
}
