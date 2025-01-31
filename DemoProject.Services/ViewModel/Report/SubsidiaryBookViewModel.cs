using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.AppSupported;
using DemoProject.Services.Abstract.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Report
{
  public  class SubsidiaryBookViewModel
    {
        private readonly IAppSupportedLanguageRepository appSupportedLanguageRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        public SubsidiaryBookViewModel()
        {
            appSupportedLanguageRepository = DependencyResolver.Current.GetService<IAppSupportedLanguageRepository>();
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
        }

        public Guid BusinessOfficeId { get; set; }

        public Guid RegionalLanguageId { get; set; }

        public byte LanguagePrmkey { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string GroupBy { get; set; }

        public string SortBy { get; set; }

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

        public bool IsAscending { get; set; }
    }
}
