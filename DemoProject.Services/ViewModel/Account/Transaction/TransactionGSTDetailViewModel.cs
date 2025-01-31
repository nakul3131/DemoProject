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
    public class TransactionGSTDetailViewModel
    {
        public long PrmKey { get; set; }

        public long TransactionGeneralLedgerPrmKey { get; set; }

        [Required]
        [StringLength(20)]
        public string InvoiceNumber { get; set; }

        public decimal TaxableAmount { get; set; }

        public decimal GSTRate { get; set; }

        public decimal CGSTRate { get; set; }

        public decimal SGSTRate { get; set; }

        public decimal IGSTRate { get; set; }

        public decimal GSTAmount { get; set; }

        public decimal CessRate { get; set; }

        public decimal CessAmount { get; set; }

        public bool IsApplicableForReverseCharge { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
        // TransactionGSTDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long TransactionGSTDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }


    }
}
