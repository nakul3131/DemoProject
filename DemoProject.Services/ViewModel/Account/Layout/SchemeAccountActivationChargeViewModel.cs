using DemoProject.Services.Abstract.Account.SystemEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeAccountActivationChargeViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;

        public SchemeAccountActivationChargeViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
        }

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte MinimumDeactiveDuration { get; set; }

        public byte MaximumDeactiveDuration { get; set; }
        
        [StringLength(1)]
        public string DeactivationDurationUnit { get; set; }

        public decimal ChargesAmount { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }

        //SchemeAccountActivationChargeMakerChecker
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short SchemeAccountActivationChargePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        // Scheme

        public Guid SchemeId { get; set; }

        [StringLength(100)]
        public string NameOfScheme { get; set; }

        //Other

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
        
        // Dropdown

        public Guid GeneralLedgerId { get; set; }

        public List<SelectListItem> GLParentDropdownList
        {
            get
            {
                return accountDetailRepository.GLParentDropdownList;
            }
        }

    }
}
