using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeDepositAccountClosureParameter")]
    public partial class SchemeDepositAccountClosureParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeDepositAccountClosureParameter()
        {
            SchemeDepositAccountClosureParameterMakerCheckers = new HashSet<SchemeDepositAccountClosureParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnablePrematureClosure { get; set; }

        public bool EnableIPenaltyInterestOnPreMatureClosing { get; set; }

        public bool EnableClosureInLockInPeriod { get; set; }

        public bool EnablePrematureClosureBeforeAdvanceDepositPeriod { get; set; }

        public bool EnableExtendMaturity { get; set; }

        public bool EnableInactiveAccountExtendMaturity { get; set; }

        public short MinimumExtendPeriod { get; set; }

        public short MaximumExtendPeriod { get; set; }

        public bool EnableAutoClosureOfInactivityOfAccount { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositAccountClosureParameterMakerChecker> SchemeDepositAccountClosureParameterMakerCheckers { get; set; }
    }
}
