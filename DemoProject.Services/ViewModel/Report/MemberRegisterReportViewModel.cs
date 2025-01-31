using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Report
{
    public class MemberRegisterReportViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public MemberRegisterReportViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
        }

        public short LanguagePrmkey { get; set; }

        public int CustomerAccountPrmkey { get; set; }

        public Guid CustomerAccountId { get; set; }

        public Guid RegionalLanguageId { get; set; }
        
        // DropdownList
        public List<SelectListItem> CustomerAccountDropdownList
        {
            get
            {
                return accountDetailRepository.CustomerAccountDropdownList;
            }
        }

        public List<SelectListItem> LanguageDropdownList
        {
            get
            {
                return configurationDetailRepository.LanguageDropdownList;
            }
        }
    }
}
