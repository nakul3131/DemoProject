using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.PersonInformation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Enterprise.Establishment
{
    public class OrganizationDetailViewModel
    {
        private readonly IMLDetailRepository iMLDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IOrganizationRepository organizationRepository;

        public OrganizationDetailViewModel()
        {
            iMLDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            organizationRepository = DependencyResolver.Current.GetService<IOrganizationRepository>();
        }

        // Translation In Regional
        [StringLength(100)]
        public string NameOfOrganizationInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Name Of Organization");
            }
        }

        [StringLength(100)]
        public string NameOfOrganizationPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Name Of Organization");
            }
        }

        [StringLength(100)]
        public string ShortNameInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Short Name");
            }
        }

        [StringLength(100)]
        public string ShortNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Short Name");
            }
        }

        [StringLength(100)]
        public string MottoInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Motto");
            }
        }

        [StringLength(100)]
        public string MottoPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Motto");
            }
        }

        [StringLength(100)]
        public string VisionInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Vision");
            }
        }

        [StringLength(100)]
        public string VisionPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Vision");
            }
        }

        [StringLength(100)]
        public string MissionInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Mission");
            }
        }

        [StringLength(100)]
        public string MissionPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Mission");
            }
        }

        [StringLength(100)]
        public string StandardsInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Standards");
            }
        }

        [StringLength(100)]
        public string StandardsPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Standards");
            }
        }

        [StringLength(100)]
        public string CoopRegistrationNumberInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Coop Registration Number");
            }
        }

        [StringLength(100)]
        public string CoopRegistrationNumberPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Coop Registration Number");
            }
        }

        [StringLength(100)]
        public string CoopReferenceNumberInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Coop Reference Number");
            }
        }

        [StringLength(100)]
        public string CoopReferenceNumberPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Coop Reference Number");
            }
        }

        [StringLength(100)]
        public string RBIRegistrationNumberInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("RBI Registration Number");
            }
        }

        [StringLength(100)]
        public string RBIRegistrationNumberPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter RBI Registration Number");
            }
        }

        [StringLength(100)]
        public string RBIReferenceNumberInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("RBI Reference Number");
            }
        }

        [StringLength(100)]
        public string RBIReferenceNumberPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter RBI Reference Number");
            }
        }

        [StringLength(100)]
        public string RegistrationAddressDetailsInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Registration Address Details");
            }
        }

        [StringLength(100)]
        public string RegistrationAddressDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Registration Address Details");
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

        [StringLength(100)]
        public string SequenceNumberTextInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Sequence Number Text");
            }
        }

        [StringLength(100)]
        public string SequenceNumberTextPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Sequence Number Text");
            }
        }

        // List<SelectListItem> For Dropdownlist
        public List<SelectListItem> AreaOfOperationDropdownList
        {
            get
            {
                return enterpriseDetailRepository.AreaOfOperationDropdownList;
            }
        }

        // List<SelectListItem> For Dropdownlist
        public List<SelectListItem> LanguageDropdownList
        {
            get
            {
                return configurationDetailRepository.AppLanguageDropdownList;
            }
        }

        public List<SelectListItem> LoanTypeDropdownList
        {
            get
            {
                return accountDetailRepository.LoanTypeDropdownList;
            }
        }

        public List<SelectListItem> FinancialOrganizationTypeDropdownList
        {
            get
            {
                return enterpriseDetailRepository.FinancialOrganizationTypeDropdownList;
            }

        }

        public List<SelectListItem> MLCoopSocietyClassDropdownList
        {
            get
            {
                return enterpriseDetailRepository.SocietyClassDropdownList;
            }

        }

        public List<SelectListItem> MLCoopSocietySubClassDropdownList
        {
            get
            {
                return enterpriseDetailRepository.SocietySubClassDropdownList;
            }
        }

        public List<SelectListItem> ContactTypeDropdownList
        {
            get
            {
                return personDetailRepository.ContactTypeDropdownList;
            }
        }

        public List<SelectListItem> ContactGroupDropdownList
        {
            get
            {
                return personDetailRepository.ContactGroupDropdownList;
            }
        }

        public List<SelectListItem> VillageTownCityDropdownList
        {
            get
            {
                return personDetailRepository.VillageTownCityDropdownList;
            }

        }

        public List<SelectListItem> FundDropdownList
        {
            get
            {
                return accountDetailRepository.FundDropdownList;
            }
        }

        public List<SelectListItem> StateDropdownList
        {
            get
            {
                return personDetailRepository.StateDropdownList;
            }
        }

        public List<SelectListItem> GSTRegistrationTypeDropdownList
        {
            get
            {
                return accountDetailRepository.GSTRegistrationTypeDropdownList;
            }
        }

        public List<SelectListItem> GSTReturnPeriodicityDropdownList
        {
            get
            {
                return accountDetailRepository.GSTReturnPeriodicityDropdownList;
            }
        }

        // List<SelectListItem> For Dropdown
        public List<SelectListItem> AccountClassDropdownList
        {
            get
            {
                return accountDetailRepository.AccountClassDropdownList;
            }
        }

    }
}
