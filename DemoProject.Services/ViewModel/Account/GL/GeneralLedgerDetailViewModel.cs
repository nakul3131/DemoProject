using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.Account.GL
{
    public class GeneralLedgerDetailViewModel
    {
        private readonly IGeneralLedgerRepository generalLedgerRepository;
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly IBusinessOfficeRepository businessOfficeRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IDepositSchemeRepository depositSchemeRepository;

        public GeneralLedgerDetailViewModel()
        {
            generalLedgerRepository = DependencyResolver.Current.GetService<IGeneralLedgerRepository>();
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
            businessOfficeRepository = DependencyResolver.Current.GetService<IBusinessOfficeRepository>();
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
            depositSchemeRepository = DependencyResolver.Current.GetService<IDepositSchemeRepository>();
        }

        // Translation In Regional
        [StringLength(100)]
        public string NameOfGLInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of GL");
            }
        }

        [StringLength(100)]
        public string NameOfGLPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of GL");
            }
        }

        [StringLength(100)]
        public string AliasNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Alias Name");
            }
        }

        [StringLength(100)]
        public string AliasNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Alias Name");
            }
        }

        [StringLength(100)]
        public string NameOnReportInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name On Report");
            }
        }

        [StringLength(100)]
        public string NameOnReportPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name On Report");
            }
        }

        [StringLength(100)]
        public string ParentGLDescriptionInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Parent GL Description");
            }
        }

        [StringLength(100)]
        public string ParentGLDescriptionPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Parent GL Description");
            }
        }

        [StringLength(100)]
        public string NoteInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Note");
            }
        }

        [StringLength(100)]
        public string NotePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Note");
            }
        }

        [StringLength(100)]
        public string ReasonForModificationInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Reason For Modification");
            }
        }

        [StringLength(100)]
        public string ReasonForModificationPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Reason For Modification");
            }
        }

        // List<SelectListItem> For Dropdownlist
        public List<SelectListItem> AccountClassDropdownList
        {
            get
            {
                return accountDetailRepository.AccountClassDropdownList;
            }
        }

        public List<SelectListItem> GLParentDropdownList
        {
            get
            {
                return accountDetailRepository.GLParentDropdownList;
            }
        }

        public List<SelectListItem> BusinessOfficeDropdownList
        {
            get
            {
                return enterpriseDetailRepository.BusinessOfficeDropdownList;
            }
        }

        public List<SelectListItem> CurrencyDropdownList
        {
            get
            {
                return accountDetailRepository.CurrencyDropdownList;
            }
        }

        public List<SelectListItem> CustomerTypeDropdownList
        {
            get
            {
                return accountDetailRepository.CustomerTypeDropdownList;
            }
        }

        public List<SelectListItem> FundTransferFrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.FundTransferFrequencyDropdownList;
            }
        }

       

        public List<SelectListItem> DesignationDropdownListForEmployeeCatgory
        {
            get
            {
                return managementDetailRepository.EmployeeDesignationDropdownList;
            }
        }

        public List<SelectListItem> GenderDropdownList
        {
            get
            {
                return personDetailRepository.GenderDropdownList;
            }
        }

        public List<SelectListItem> SchemeDropdownList
        {
            get
            {
                return depositSchemeRepository.SchemeDropdownList;
            }
        }

        public List<SelectListItem> TransactionTypeDropdownList
        {
            get
            {
                return accountDetailRepository.TransactionTypeDropdownList;
            }
        }
    }
}
