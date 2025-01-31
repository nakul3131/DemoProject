using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Report
{
    public class KReportViewModel
    {
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;

        public KReportViewModel()
        {
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
        }

        public short LanguagePrmkey { get; set; }
        
        public Guid RegionalLanguageId { get; set; }

        public long PersonPrmKey { get; set; }

        public Guid PersonId { get; set; }

        public Guid BusinessOfficeId { get; set; }

        public int FromYear { get; set; }

        public int ToYear { get; set; }

        public bool IsBranchWise { get; set; }

        // DropdownList
        public List<SelectListItem> PersonDropdownList
        {
            get
            {
                return personDetailRepository.PersonDropdownList;
            }
        }

        public List<SelectListItem> LanguageDropdownList
        {
            get
            {
                return configurationDetailRepository.AppLanguageDropdownList;
            }
        }

        public List<SelectListItem> BusinessOfficeDropdownList
        {
            get
            {
                return enterpriseDetailRepository.BusinessOfficeDropdownList;
            }
        }
    }
}
