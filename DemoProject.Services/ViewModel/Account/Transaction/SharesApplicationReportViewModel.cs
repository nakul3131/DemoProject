using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class SharesApplicationReportViewModel
    {

        private readonly ISharesApplicationRepository sharesApplicationRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public SharesApplicationReportViewModel()
        {
            sharesApplicationRepository = DependencyResolver.Current.GetService<ISharesApplicationRepository>();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
        }

        public short LanguagePrmkey { get; set; }

        public int SharesApplicationPrmkey { get; set; }

        public long ApplicationNumber { get; set; }

        public Guid SharesApplicationId { get; set; }

        public Guid RegionalLanguageId { get; set; }



        // DropdownList
        //public List<SelectListItem> SharesApplicationDropdownList
        //{
        //    get
        //    {
        //        return sharesApplicationRepository.SharesApplicationDropdownList;
        //    }
        //}

        public List<SelectListItem> LanguageDropdownList
        {
            get
            {
                return configurationDetailRepository.AppLanguageDropdownList;
            }
        }

    }
}
