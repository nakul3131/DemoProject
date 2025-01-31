using DemoProject.Services.Abstract.Account.GL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class TransactionGeneralLedgerViewModel
    {
        public long PrmKey { get; set; }


        public int TransactionMasterPrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        [StringLength(1500)]
        public string Particulars { get; set; }

        public decimal Amount { get; set; }

        public bool IsCredit { get; set; }

        [StringLength(1500)]
        public string Narration { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // TransactionGeneralLedgerMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long TransactionGeneralLedgerPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
       

        // Dropdown
        public Guid GeneralLedgerId { get; set; }

        public Guid BusinessOfficeId { get; set; }
        public Guid PersonId { get; set; }
       
        public decimal AdmissionFees { get; set; }
        public decimal Charges1 { get; set; }
        public decimal GSTAmount { get; set; }
        public decimal CessAmount { get; set; }

    }
}
