using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class OfficeDetailViewModel
    {
        private readonly IMLDetailRepository iMLDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IPersonDetailRepository personInformationDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;


        public OfficeDetailViewModel()
        {
            iMLDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>(); configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            personInformationDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            securityDetailRepository = DependencyResolver.Current.GetService<ISecurityDetailRepository>();

        }

        // Translation In Regional
        [StringLength(100)]
        public string AlternateBusinessOfficeCodeInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Alternate Business Office Code");
            }
        }

        [StringLength(100)]
        public string AlternateBusinessOfficeCodePlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Alternate Business Office Code");
            }
        }

        [StringLength(100)]
        public string NameOfBusinessOfficeInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Name Of Business Office");
            }
        }

        [StringLength(100)]
        public string NameOfBusinessOfficePlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Name Of Business Office");
            }
        }

        [StringLength(10)]
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
        public string NameOfBusinessOfficeForThirdPartyInterfaceInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Name Of Business Office For Third Party Interface");
            }
        }

        [StringLength(100)]
        public string NameOfBusinessOfficeForThirdPartyInterfacePlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Name Of Business Office For Third Party Interface");
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
        public string ContactDetailsInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Contact Details");
            }
        }

        [StringLength(100)]
        public string ContactDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Contact Details");
            }
        }

        [StringLength(100)]
        public string AddressDetailsInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Address Details");
            }
        }

        [StringLength(100)]
        public string AddressDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Address Details");
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

        //BusinessOfficeCoopRegistrationTranslation
        [StringLength(100)]
        public string RegistrationNumberInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Registration Number");
            }
        }

        [StringLength(100)]
        public string RegistrationNumberPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Registration Number");
            }
        }

        [StringLength(100)]
        public string ReferenceNumberInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Reference Number");
            }
        }

        [StringLength(100)]
        public string ReferenceNumberPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Reference Number");
            }
        }

        [StringLength(100)]
        public string CoopAlphaNumericCodeInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Coop Alpha Numeric Code");
            }
        }

        [StringLength(100)]
        public string CoopAlphaNumericCodePlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Coop Alpha Numeric Code");
            }
        }


        //BusinessOfficeRBIRegistrationTranslation

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
        public string RBILicenseNumberInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("RBI License Number");
            }
        }

        [StringLength(100)]
        public string RBILicenseNumberPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter RBI License Number");
            }
        }

        [StringLength(100)]
        public string AlphaNumericSWIFTAddressInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("RBI Alpha Numeric SWIFT Address");
            }
        }

        [StringLength(100)]
        public string AlphaNumericSWIFTAddressPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter RBI Alpha Numeric SWIFT Address");
            }
        }

        [StringLength(100)]
        public string AlphaNumericTelexAddressInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("RBI Alpha Numeric Telex Address");
            }
        }

        [StringLength(100)]
        public string AlphaNumericTelexAddressPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter RBI Alpha Numeric Telex Address");
            }
        }

        [StringLength(100)]
        public string BusinessOfficeUniqueIdentityNumberForATMInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("RBI Business Office Unique Identity Number For ATM");
            }
        }

        [StringLength(100)]
        public string BusinessOfficeUniqueIdentityNumberForATMPlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter RBI Business Office Unique Identity Number For ATM");
            }
        }


        public bool IsRegisterUnderCooperative
        {
            get
            {
                return configurationDetailRepository.IsRegisteredUnderCooperative();
            }
        }

        public bool IsRegisterUnderRBI
        {
            get
            {
                return configurationDetailRepository.IsRegisteredUnderRBI();
            }
        }

        public List<SelectListItem> BusinessOfficeDropdownList
        {
            get
            {
                return enterpriseDetailRepository.BusinessOfficeDropdownList;
            }
        }

        //BusinessOfficeDetailViewModel

        public List<SelectListItem> CurrencyDropdownList
        {
            get
            {
                return accountDetailRepository.CurrencyDropdownList;
            }
        }

        public List<SelectListItem> OfficeSchedules
        {
            get
            {
                return enterpriseDetailRepository.OfficeSchedulesDropdownList;
            }
        }

        public List<SelectListItem> Languages
        {
            get
            {
                return configurationDetailRepository.AppLanguageDropdownList;
            }
        }

        public List<SelectListItem> BusinessOfficeTypeDropdownList
        {
            get
            {
                return enterpriseDetailRepository.BusinessOfficeTypeDropdownList;
            }
        }

        public List<SelectListItem> BusinessNatureDropdownList
        {
            get
            {
                return enterpriseDetailRepository.BusinessNatureDropdownList;
            }
        }

        public List<SelectListItem> BusinessOfficeGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.BusinessOfficeGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> Centers
        {
            get
            {
                return personInformationDetailRepository.VillageTownCityDropdownList;
            }
        }

        public List<SelectListItem> BusinessOfficeTypes
        {
            get
            {
                return enterpriseDetailRepository.BusinessOfficeTypeDropdownList;
            }
        }

        //BusinessOfficeRBIRegistrationViewModel

        public List<SelectListItem> ChecksumAlgorithmDropdownList
        {
            get
            {
                return securityDetailRepository.ChecksumAlgorithmDropdownList;
            }
        }

        //BusinessOfficeTransactionParameterViewModel
        public List<SelectListItem> FrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.FrequencyDropdownList;
            }
        }

        public List<SelectListItem> MaskTypeDropdownList
        {
            get
            {
                return configurationDetailRepository.MaskTypeDropdownList;
            }
        }

        //
        public List<SelectListItem> PasswordPolicyDropDownList
        {
            get
            {
                return securityDetailRepository.PasswordPolicyDropDownList;
            }
        }

        public List<SelectListItem> MenuDropdownList
        {
            get
            {
                return configurationDetailRepository.ModelMenuDropdownList;
            }
        }

        public List<SelectListItem> SpecialPermissionDropdownList
        {
            get
            {
                return securityDetailRepository.SpecialPermissionDropdownList;
            }
        }

        public List<SelectListItem> GeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.GLParentDropdownList;
            }
        }

        public List<SelectListItem> TransactionTypeDropdownList
        {
            get
            {
                return accountDetailRepository.TransactionTypeDropdownList;
            }
        }

        public bool HasMultiCurrency
        {
            get
            {
                return configurationDetailRepository.HasMultiCurrency();
            }

        }
    }
}
