using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanPaymentReminderParameter")]
    public partial class SchemeLoanPaymentReminderParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanPaymentReminderParameter()
        {
            SchemeLoanPaymentReminderParameterMakerCheckers = new HashSet<SchemeLoanPaymentReminderParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnablePaymentDueReminder { get; set; }

        public byte StartDaysBeforePaymentDueDate { get; set; }

        public byte OccursEveryDayForDueReminder { get; set; }

        public bool EnableMissedPaymentReminder { get; set; }

        public byte StartDaysAfterPaymentDueDate { get; set; }

        public byte OccursEveryDayForMissedPaymentReminder { get; set; }

        public byte MaximumMissedPaymentReminders { get; set; }

        public bool EnableOverduesPaymentReminder { get; set; }

        public byte StartDaysAfterOverduePaymentDate { get; set; }

        public byte OccursEveryDayForOverduePaymentReminder { get; set; }

        public byte MaximumOverduePaymentReminders { get; set; }

        public bool EnableNPADeclarationReminder { get; set; }

        public byte StartDaysAfterNPADeclarationDate { get; set; }

        public byte OccursEveryDayForNPADeclarationReminder { get; set; }

        public byte MaximumNPADeclarationReminders { get; set; }

        public bool EnableLegalActivityReminder { get; set; }

        public bool EnableIrregularityReminder { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanPaymentReminderParameterMakerChecker> SchemeLoanPaymentReminderParameterMakerCheckers { get; set; }
    }
}
