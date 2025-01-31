using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeGeneralLedgerViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;

        public SchemeGeneralLedgerViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
        }
        //GeneralLedgerSchemeViewModel

        public short PrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short SchemePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

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

        //GeneralLedgerSchemeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeGeneralLedgerPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Scheme

        public Guid GeneralLedgerId { get; set; }

        [StringLength(100)]
        public string NameOfGeneralLedger { get; set; }

        // List<SelectListItem> For Dropdownlist

        public List<SelectListItem> GeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.GeneralLedgerDropdownList;
            }
        }
    }
}
