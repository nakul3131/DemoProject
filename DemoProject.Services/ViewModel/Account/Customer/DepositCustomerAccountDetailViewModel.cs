using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class DepositCustomerAccountDetailViewModel
    {
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;

        public DepositCustomerAccountDetailViewModel()
        {
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            securityDetailRepository = DependencyResolver.Current.GetService<ISecurityDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
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

        public List<SelectListItem> ChequeBookDropdownList
        {
            get
            {
                return accountDetailRepository.ChequeBookDropdownList;
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

        public List<SelectListItem> JointAccountHolderTypeDropdownList
        {
            get
            {
                return accountDetailRepository.JointAccountHolderTypeDropdownList;
            }
        }

        public List<SelectListItem> PersonInfoNumbersDropdownList
        {
            get
            {
                return personDetailRepository.PersonInfoNumbersDropdownList;
            }
        }

        public List<SelectListItem> FamilyRelationDropdownList
        {
            get
            {
                return personDetailRepository.RelationDropdownList;
            }
        }

        public List<SelectListItem> RenewTypeDropdownList
        {
            get
            {
                return accountDetailRepository.RenewTypeDropdownList;
            }
        }

        public List<SelectListItem> MaturityInstructionDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Do Not Renew ---> " + mlDetailRepository.TranslateInRegionalLanguage("Do Not Renew"),
                        Value = "DNR"
                    },
                    new SelectListItem
                    {
                        Text="Renew Principal And Interest ---> " + mlDetailRepository.TranslateInRegionalLanguage("Renew Principal And Interest"),
                        Value = "RNW"
                    },
                    new SelectListItem
                    {
                        Text="Renew Principal Only ---> " + mlDetailRepository.TranslateInRegionalLanguage("Renew Principal Only"),
                        Value = "RPO"
                    }
            };
            }
        }

        public List<SelectListItem> InterestPayOutFrequencyDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Anually ---> " + mlDetailRepository.TranslateInRegionalLanguage("Anually"),
                        Value = "ANL"
                    },
                    new SelectListItem
                    {
                        Text="At Maturity ---> " + mlDetailRepository.TranslateInRegionalLanguage("At Maturity"),
                        Value = "MAT"
                    },
                    new SelectListItem
                    {
                        Text="Monthly --> " + mlDetailRepository.TranslateInRegionalLanguage("Monthly"),
                        Value = "MON"
                    },
                    new SelectListItem
                    {
                        Text="Semi Annually ---> " + mlDetailRepository.TranslateInRegionalLanguage("Semi Annually"),
                        Value = "SAN"
                    },
                    new SelectListItem
                    {
                        Text="Quarterly ---> " + mlDetailRepository.TranslateInRegionalLanguage("Quarterly"),
                        Value = "QRT"
                    }
            };
            }
        }

        public List<SelectListItem> AddressTypeDropdownList
        {
            get
            {
                return personDetailRepository.AddressTypeDropdownList;
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

        public List<SelectListItem> InstallmentFrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.InstallmentFrequencyDropdownList;
            }
        }

        public List<SelectListItem> TransactionTypeDropdownList
        {
            get
            {
                return accountDetailRepository.TransactionTypeDropdownList;
            }
        }

        public List<SelectListItem> SweepOutFrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.SweepOutFrequencyDropdownList;
            }
        }

        public List<SelectListItem> CustomerAccountTypeDropdownList
        {
            get
            {
                return accountDetailRepository.CustomerAccountTypeDropdownList;
            }
        }

        public List<SelectListItem> BusinessOfficeDropdownList
        {
            get
            {
                return accountDetailRepository.AuthorizedBusinessOfficeDropdownList;
            }
        }

        public List<SelectListItem> DepositTypeDropdownList
        {
            get
            {
                return new List<SelectListItem>{
                new SelectListItem{
                    Text="Demand Deposit",
                    Value = "DMN"
                },
                  new SelectListItem{
                    Text="Fixed Deposit",
                    Value = "FDP"
                },
                  new SelectListItem{
                    Text="Recurring Deposit",
                    Value = "REC"
                },
                  new SelectListItem{
                    Text="Public Provident Fund",
                    Value = "PPF"
                }
            };
            }
        }

        public List<SelectListItem> StatementFrequencyDropdownList
        {
            get
            {
                return new List<SelectListItem>{
                new SelectListItem{
                    Text="Daily",
                    Value = "DLY"
                },
                  new SelectListItem{
                    Text="Weekly",
                    Value = "WKL"
                },
                  new SelectListItem{
                    Text="Monthly",
                    Value = "MON"
                },
                  new SelectListItem{
                    Text="Quarterly",
                    Value = "QRT"
                },
                  new SelectListItem{
                    Text="Semi Annually",
                    Value = "SAN"
                },
                  new SelectListItem{
                    Text="Anually",
                    Value = "ANL"
                }
            };
            }
        }

        public List<SelectListItem> GeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.GLParentDropdownList;
            }
        }

        public List<SelectListItem> CurrencyDropdownList
        {
            get
            {
                return accountDetailRepository.CurrencyDropdownList;
            }
        }

        public List<SelectListItem> SchemeDropdownList
        {
            get
            {
                return accountDetailRepository.SharesCapitalSchemeDropdownList;
            }
        }

        public List<SelectListItem> DepositSchemeDropdownList
        {
            get
            {
                return accountDetailRepository.DepositSchemeDropdownList;
            }
        }

        public List<SelectListItem> DemandDepositCustomerDropdownList
        {
            get
            {
                return accountDetailRepository.DemandDepositAccountHolderDropdownList;
            }
        }

        public List<SelectListItem> AccountOperationModeDropdownList
        {
            get
            {
                return accountDetailRepository.AccountOperationModeDropdownList;
            }
        }

        public IEnumerable<SelectListItem> PersonInfoNumbersAgeAbove18DropdownList
        {
            get
            {
                return personDetailRepository.PersonInfoNumbersAgeAbove18DropdownList;
            }
        }

        public IEnumerable<SelectListItem> DocumentDropdownList
        {
            get
            {
                return personDetailRepository.DocumentDropdownList;
            }
        }

        public List<SelectListItem> AgentDropdownList
        {
            get
            {
                return personDetailRepository.AgentDropdownList;
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
