using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class TransactionCashDenominationViewModel
    {
        public long PrmKey { get; set; }

        public Guid TransactionCashDenominationId { get; set; }

        public int TransactionMasterPrmKey { get; set; }

        public byte DenominationPrmKey { get; set; }

        public short Pieces { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // TransactionCashDenominationMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long TransactionCashDenominationPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //DropDowns
        public Guid DenominationId { get; set; }
       
        [StringLength(50)]
        public string Title { get; set; }
    }
}
