using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.Management.Conference;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class SharesCapitalCustomerAccountDetailViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IMinuteOfMeetingAgendaRepository minuteOfMeetingAgendaRepository;
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;

        public SharesCapitalCustomerAccountDetailViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            minuteOfMeetingAgendaRepository = DependencyResolver.Current.GetService<IMinuteOfMeetingAgendaRepository>();
            securityDetailRepository = DependencyResolver.Current.GetService<ISecurityDetailRepository>();
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
        }

        // Translation In Regional
        [StringLength(100)]
        public string NameOfNomineeInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Nominee");
            }
        }

        [StringLength(100)]
        public string NameOfNomineePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Nominee");
            }
        }

        [StringLength(100)]
        public string FullAddressInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Full Address");
            }
        }

        [StringLength(100)]
        public string FullAddressPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Full Address");
            }
        }

        [StringLength(100)]
        public string ContactDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Contact Details");
            }
        }

        [StringLength(100)]
        public string ContactDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Contact Details");
            }
        }

        [StringLength(100)]
        public string GuardianFullNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Guardian Full Name");
            }
        }

        [StringLength(100)]
        public string GuardianFullNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Guardian Full Name");
            }
        }

        [StringLength(100)]
        public string GuardianNomineeFullAddressInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Guardian Full Address");
            }
        }

        [StringLength(100)]
        public string GuardianNomineeFullAddressPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Guardian Full Address Details");
            }
        }

        [StringLength(100)]
        public string GuardianNomineeContactDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Guardian Contact Details");
            }
        }

        [StringLength(100)]
        public string GuardianNomineeContactDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Guardian Contact Details");
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

        //AddressViewModel
        [StringLength(100)]
        public string FlatDoorNoInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Flat Door No.");
            }
        }

        [StringLength(100)]
        public string FlatDoorNoPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Flat Door No.");
            }
        }

        [StringLength(100)]
        public string NameOfBuildingInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Building");
            }
        }

        [StringLength(100)]
        public string NameOfBuildingPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Building");
            }
        }

        [StringLength(100)]
        public string NameOfRoadInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Road");
            }
        }

        [StringLength(100)]
        public string NameOfRoadPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Road");
            }
        }

        [StringLength(100)]
        public string NameOfAreaInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Area");
            }
        }

        [StringLength(100)]
        public string NameOfAreaPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Area");
            }
        }

        [StringLength(100)]
        public string BusinessOfficeInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Branch");
            }
        }


        public List<SelectListItem> ResidenceTypeDropdownList
        {
            get
            {
                return personDetailRepository.ResidenceTypeDropdownList;
            }
        }

        public List<SelectListItem> OwnershipTypeDropdownList
        {
            get
            {
                return personDetailRepository.OwnershipTypeDropdownList;
            }

        }

        public List<SelectListItem> CityDropdownList
        {
            get
            {
                return personDetailRepository.CityDropdownList;
            }
        }

        public IEnumerable<SelectListItem> ContactTypeDropdownList
        {
            get
            {
                return personDetailRepository.ContactTypeDropdownList;
            }
        }

        public List<SelectListItem> PersonDropdownList
        {
            get
            {
                return personDetailRepository.PersonDropdownList;
            }
        }

        public IEnumerable<SelectListItem> PersonInfoNumbersDropdownList
        {
            get
            {
                return personDetailRepository.PersonInfoNumbersDropdownList;
            }
        }

        public IEnumerable<SelectListItem> PersonInfoNumbersAgeAbove18DropdownList
        {
            get
            {
                return personDetailRepository.PersonInfoNumbersAgeAbove18DropdownList;
            }
        }

        public List<SelectListItem> CurrencyDropdownList
        {
            get
            {
                return accountDetailRepository.CurrencyDropdownList;
            }
        }


        public List<SelectListItem> JointAccountHolderTypeDropdownList
        {
            get
            {
                return accountDetailRepository.JointAccountHolderTypeDropdownList;
            }
        }

        public List<SelectListItem> FamilyRelationDropdownList
        {
            get
            {
                return personDetailRepository.RelationDropdownList;
            }
        }

        public List<SelectListItem> GuardianTypeDropdownList
        {
            get
            {
                return personDetailRepository.GuardianTypeDropdownList;
            }
        }

        public List<SelectListItem> FrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.FrequencyDropdownList;
            }
        }

        public List<SelectListItem> TransactionTypeDropdownList
        {
            get
            {
                return accountDetailRepository.TransactionTypeDropdownList;
            }
        }

        public List<SelectListItem> MinuteOfMeetingAgendaDropdownList
        {
            get
            {
                return accountDetailRepository.MinuteOfMeetingAgendaDropdownList;
            }
        }

        public List<SelectListItem> AuthorizedBusinessOfficeDropdownList
        {
            get
            {
                return accountDetailRepository.AuthorizedBusinessOfficeDropdownList;
            }
        }

        public List<SelectListItem> SharesCapitalSchemeDropdownList
        {
            get
            {
                return accountDetailRepository.SharesCapitalSchemeDropdownList;
            }
        }

        public List<SelectListItem> AddressTypeDropdownList
        {
            get
            {
                return personDetailRepository.AddressTypeDropdownList;
            }
        }

        public List<SelectListItem> GeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.GLParentDropdownList;
            }
        }

        public List<SelectListItem> CommunicationMediaDropdownList
        {
            get
            {
                return managementDetailRepository.CommunicationMediaDropdownList;
            }
        }

        public List<SelectListItem> NoticeTypeDropdownList
        {
            get
            {
                return managementDetailRepository.NoticeTypeDropdownList;
            }
        }

        public List<SelectListItem> ScheduleDropdownList
        {
            get
            {
                return enterpriseDetailRepository.ScheduleDropdownList;
            }
        }

        public string AllowLastPastDays
        {
            get
            {
                short allowBackDate = securityDetailRepository.GetPastDaysPermissionForTransaction((short)HttpContext.Current.Session["UserProfilePrmKey"], accountDetailRepository.GetPreviousClosingFinancialYearEndDate());
                DateTime dateTime = DateTime.Now.AddDays(-allowBackDate);
                return String.Format("{0:yyyy-MM-dd}", dateTime);
            }
        }

        public bool HasBranch
        {
            get
            {
                return configurationDetailRepository.GetNumberOfBranches() < 1 ? false : true;
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
