using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeDepositAgentIncentive")]
    public partial class SchemeDepositAgentIncentive
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeDepositAgentIncentive()
        {
            SchemeDepositAgentIncentiveMakerCheckers = new HashSet<SchemeDepositAgentIncentiveMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public decimal MinimumCollectionAmount { get; set; }

        public decimal MaximumCollectionAmount { get; set; }

        [Required]
        [StringLength(3)]
        public string IncentiveUnit { get; set; }

        public decimal Incentive { get; set; }

        [Required]
        [StringLength(3)]
        public string RoundingMethod { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositAgentIncentiveMakerChecker> SchemeDepositAgentIncentiveMakerCheckers { get; set; }
    }
}
