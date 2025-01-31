using System.Collections.Generic;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class UserProfileDetailViewModel
    {
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;

        public UserProfileDetailViewModel()
        {
            securityDetailRepository = DependencyResolver.Current.GetService<ISecurityDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
        }

        public List<SelectListItem> GroupDropdownList
        {
            get
            {
                return securityDetailRepository.ChecksumAlgorithmDropdownList;
            }
        }

        public List<SelectListItem> ModelMenuDropdownList
        {
            get
            {
                return configurationDetailRepository.ModelMenuDropdownList;
            }
        }

        public List<SelectListItem> PasswordPolicyDropDownList
        {
            get
            {
                return securityDetailRepository.PasswordPolicyDropDownList;
            }
        }

        public List<SelectListItem> PersonDropdownList
        {
            get
            {
                return personDetailRepository.PersonDropdownList;
            }
        }

        public List<SelectListItem> SpecialPermissionDropdownList
        {
            get
            {
                return securityDetailRepository.SpecialPermissionDropdownList;
            }
        }

        public List<SelectListItem> TransactionTypeDropdownList
        {
            get
            {
                return accountDetailRepository.TransactionTypeDropdownList;
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

        public List<SelectListItem> RoleProfileDropDownList
        {
            get
            {
                return securityDetailRepository.RoleProfileDropDownList;
            }
        }

        public List<SelectListItem> UserTypeDropdownList
        {
            get
            {
                return securityDetailRepository.UserTypeDropdownList;
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
