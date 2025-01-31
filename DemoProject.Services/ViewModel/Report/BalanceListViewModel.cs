using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.AppSupported;
using DemoProject.Services.Abstract.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Report
{
    public class BalanceListViewModel
    {
        private readonly IAppSupportedLanguageRepository appSupportedLanguageRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        public BalanceListViewModel()
        {
            appSupportedLanguageRepository = DependencyResolver.Current.GetService<IAppSupportedLanguageRepository>();
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
        
        }

        public Guid BusinessOfficeId { get; set; }

        public Guid GeneralLedgerId { get; set; }

        public Guid SchemeId { get; set; }

        public Guid RegionalLanguageId { get; set; }

        public int BranchName { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte MemberTypePrmKey{ get; set; }

        public byte MemberType { get; set; }

        public short SchemePrmKey { get; set; }

        [StringLength(50)]
        public string GroupBy { get; set; }

        [StringLength(50)]
        public string SortBy { get; set; }

        public bool IsAscending { get; set; }

        public decimal FromBalance { get; set; }

        public decimal ToBalance { get; set; }

        public DateTime? FromAccountOpeningDate { get; set; }

        public DateTime? ToAccountOpeningDate { get; set; }

        public byte LanguagePrmkey { get; set; }

        public List<SelectListItem> LanguageDropdownList
        {
            get
            {
                return appSupportedLanguageRepository.AppSupportedLanguageDropdownList;
            }
        }

        public List<SelectListItem> AuthorizedBusinessOfficeDropdownList
        {
            get
            {
                return accountDetailRepository.AuthorizedBusinessOfficeDropdownList;
            }
        }


    }
}

