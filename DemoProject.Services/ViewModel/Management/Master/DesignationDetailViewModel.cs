using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;

namespace DemoProject.Services.ViewModel.Master.General
{

    public class DesignationDetailViewModel
    {
        private readonly IMLDetailRepository iMLDetailRepository;

        public DesignationDetailViewModel()
        {
            iMLDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        // Translation In Regional

        [StringLength(100)]
        public string NameOfDesignationInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Name Of Designation");
            }
        }

        [StringLength(100)]
        public string NameOfDesignationPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Name Of Designation");
            }
        }

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

    }
}
