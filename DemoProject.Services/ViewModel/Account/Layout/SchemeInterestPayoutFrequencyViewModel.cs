using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeInterestPayoutFrequencyViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;

        public SchemeInterestPayoutFrequencyViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
        }

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short FrequencyPrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeInterestPayoutFrequencyMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeInterestPayoutFrequencyPrmKey { get; set; }

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

        public Guid FrequencyId { get; set; }

        [StringLength(100)]
        public string NameOfFrequency { get; set; }

        public List<SelectListItem> FrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.FrequencyDropdownList;
            }
        }
    }
}
