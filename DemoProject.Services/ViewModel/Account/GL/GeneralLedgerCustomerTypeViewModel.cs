using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.Services.ViewModel.Account.GL
{
    public class GeneralLedgerCustomerTypeViewModel
    {
        public short PrmKey { get; set; }

        public Guid GeneralLedgerCustomerTypeId { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte CustomerTypePrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //GeneralLedgerCustomerTypeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short GeneralLedgerCustomerTypePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // CustomerType

        public Guid CustomerTypeId { get; set; }

        public Guid[] MultiCustomerTypeId { get; set; }

        [StringLength(100)]
        public string NameOfCustomerType { get; set; }
    }
}
