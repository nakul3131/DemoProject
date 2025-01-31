using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
     public class ContinentDetailViewModel
     {
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IMLDetailRepository iMLDetailRepository;

        public ContinentDetailViewModel()
        {
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            iMLDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
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

        // Drop Down
        public List<SelectListItem> ContinentDropdownList
        {
            get
            {
                return personDetailRepository.ContinentDropdownList;
            }
        }
    }
}
