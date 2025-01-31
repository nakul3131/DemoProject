using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountDetailViewModel
    {
        // CustomerAccountDetail

        public long PrmKey { get; set; }

        public Guid CustomerAccountDetailId { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public short CurrencyPrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // CustomerAccountDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long CustomerAccountDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        [StringLength(100)]
        public string NameOfScheme { get; set; }

        [StringLength(3)]
        public string DepositType { get; set; }

        public Guid BusinessOfficeId { get; set; }

        [StringLength(100)]
        public string NameOfBusinessOffice { get; set; }

        public Guid GeneralLedgerId { get; set; }

        [StringLength(100)]
        public string NameOfGL { get; set; }

        public Guid PersonId { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        public Guid PersonId1 { get; set; }

        public Guid CurrencyId { get; set; }

        public Guid LoanTypeId { get; set; }

        public Guid SchemeId { get; set; }

        public short _businessOfficePrmKey { get; set; }

        public long _personPrmKey { get; set; }

        public List<SelectListItem> AuthorizedBusinessOfficeDropdownList1 { get; set; }

        public List<SelectListItem> listItem = new List<SelectListItem>();

    }
}
