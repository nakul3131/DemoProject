using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeAccountBankingChannelParameter")]
    public partial class SchemeAccountBankingChannelParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeAccountBankingChannelParameter()
        {
            SchemeAccountBankingChannelParameterMakerCheckers = new HashSet<SchemeAccountBankingChannelParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnableInternetBanking { get; set; }

        public bool EnableMobileBanking { get; set; }

        public bool EnableATM { get; set; }

        public bool EnableCDM { get; set; }

        public bool EnableDebitCard { get; set; }

        public bool EnableCreditCard { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeAccountBankingChannelParameterMakerChecker> SchemeAccountBankingChannelParameterMakerCheckers { get; set; }
    }
}
