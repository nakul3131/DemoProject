using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanPaymentReminderParameterViewModel
    {
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

        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanPaymentReminderParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
