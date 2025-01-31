using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.PersonInformation.Master;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
    public class TalukaDetaiViewModel
    {
        private readonly IDistrictRepository districtRepository;
        private readonly ITalukaRepository talukaRepository;
        private readonly IMLDetailRepository iMLDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public TalukaDetaiViewModel()
        {
            districtRepository = DependencyResolver.Current.GetService<IDistrictRepository>();
            talukaRepository = DependencyResolver.Current.GetService<ITalukaRepository>();
            iMLDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
        }
        
        // Translation In Regional
        [StringLength(100)]
        public string AliasNameInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Alias Name");
            }
        }

        [StringLength(100)]
        public string AliasNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Alias Name");
            }
        }

        [StringLength(100)]
        public string NameOnReportInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Name On Report");
            }
        }

        [StringLength(100)]
        public string NameOnReportPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Name On Report");
            }
        }

        [StringLength(100)]
        public string NoteInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Note");
            }
        }

        [StringLength(100)]
        public string NotePlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Note");
            }
        }

        [StringLength(100)]
        public string ReasonForModificationInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Reason For Modification");
            }
        }

        [StringLength(100)]
        public string ReasonForModificationPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Reason For Modification");
            }
        }

        //drop down
        public List<SelectListItem> SubDivisionOfficeDropdownList
        {
            get
            {
                return personDetailRepository.SubDivisionOfficeDropdownList;
            }
        }

        public List<SelectListItem> DistrictDropdownList
        {
            get
            {
                return personDetailRepository.DistrictDropdownList;
            }
        }
    }
}
