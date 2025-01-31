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
using DemoProject.Services.Abstract.Management.Conference;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class LoanCustomerAccountDetailViewModel
    {
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly IMinuteOfMeetingAgendaRepository minuteOfMeetingAgendaRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public LoanCustomerAccountDetailViewModel()
        {
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
            minuteOfMeetingAgendaRepository = DependencyResolver.Current.GetService<IMinuteOfMeetingAgendaRepository>();
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            securityDetailRepository = DependencyResolver.Current.GetService<ISecurityDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
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
                return mlDetailRepository.TranslateInRegionalLanguage("Guardian Nominee Full Address");
            }
        }

        [StringLength(100)]
        public string GuardianNomineeFullAddressPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Guardian Nominee Full Address");
            }
        }

        [StringLength(100)]
        public string GuardianNomineeContactDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Guardian Nominee Contact Details");
            }
        }

        [StringLength(100)]
        public string GuardianNomineeContactDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Guardian Nominee Contact Details");
            }
        }

        public List<SelectListItem> GetCashCreditLoanDocumentDropdownList
        {

            get
            {
                return accountDetailRepository.CashCreditLoanDocumentDropdownList;
            }
        }

        [StringLength(100)]
        public string BranchPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Branch");
            }
        }

        [StringLength(100)]
        public string BranchInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Branch");
            }
        }

        [StringLength(100)]
        public string LoanDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Loan Details");
            }
        }

        [StringLength(100)]
        public string LoanDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Loan Details");
            }
        }

        [StringLength(100)]
        public string MortgageDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Mortgage Details");
            }
        }

        [StringLength(100)]
        public string MortgageDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Mortgage Details");
            }
        }

        [StringLength(100)]
        public string NameOfOrganizationInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Organization");
            }
        }

        [StringLength(100)]
        public string NameOfOrganizationPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Organization");
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
        public string OtherNameOfInstituteInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Other Name Of Institute");
            }
        }

        [StringLength(100)]
        public string OtherNameOfInstitutePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Other Name Of Institute");
            }
        }


        [StringLength(100)]
        public string OtherInstituteContactDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Other Institute Contact Details");
            }
        }

        [StringLength(100)]
        public string OtherInstituteContactDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Other Institute Contact Details");
            }
        }

        [StringLength(100)]
        public string OtherInstituteAddressDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Other Institute Address Details");
            }
        }

        [StringLength(100)]
        public string OtherInstituteAddressDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Other Institute Address Details");
            }
        }

        [StringLength(100)]
        public string OtherFeesDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Other Fees Details");
            }
        }

        [StringLength(100)]
        public string OtherFeesDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Other Fees Details");
            }
        }

        [StringLength(100)]
        public string ContactPersonNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Contact Person Name");
            }
        }

        [StringLength(100)]
        public string ContactPersonNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Contact Person Name");
            }
        }

        [StringLength(100)]
        public string ContactPersonContactDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Contact Person Contact Details");
            }
        }

        [StringLength(100)]
        public string ContactPersonContactDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Contact Person Contact Details");
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

        public List<SelectListItem> CourtCaseStageDropdownList
        {
            get
            {
                return personDetailRepository.CourtCaseStageDropdownList;
            }

        }

        public List<SelectListItem> CourtCaseTypeDropdownList
        {
            get
            {
                return personDetailRepository.CourtCaseTypeDropdownList;
            }

        }

        public List<SelectListItem> MinuteOfMeetingAgendaDropdownList
        {
            get
            {
                return accountDetailRepository.MinuteOfMeetingAgendaDropdownList;
            }
        }

        public List<SelectListItem> LoanReasonDropdownList
        {
            get
            {
                return accountDetailRepository.LoanReasonDropdownList;
            }
        }

        public List<SelectListItem> GoldOrnamentDropdownList
        {
            get
            {
                return accountDetailRepository.GoldOrnamentDropdownList;
            }
        }

        public List<SelectListItem> BusinessOfficeDropdownList
        {
            get
            {
                return accountDetailRepository.AuthorizedBusinessOfficeDropdownList;
            }
        }

        public List<SelectListItem> GeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.GLParentDropdownList;
            }
        }

        public List<SelectListItem> GuarantorDropdownList
        {
            get
            {
                return accountDetailRepository.GLParentDropdownList;
            }
        }

        public List<SelectListItem> PersonDropdownList
        {
            get
            {
                return personDetailRepository.PersonDropdownList;
            }
        }

        public List<SelectListItem> JewelAssayerDropdownList
        {
            get
            {
                return personDetailRepository.JewelAssayerDropdownList;
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

        public IEnumerable<SelectListItem> ContactTypeDropdownList
        {
            get
            {
                return personDetailRepository.ContactTypeDropdownList;
            }
        }

        public IEnumerable<SelectListItem> CommunicationMediaDropdownList
        {
            get
            {
                return managementDetailRepository.CommunicationMediaDropdownList;
            }
        }

        public List<SelectListItem> CurrencyDropdownList
        {
            get
            {
                return accountDetailRepository.CurrencyDropdownList;
            }
        }

        public List<SelectListItem> ConsumerDurableItemBrandDropdownList
        {
            get
            {
                return accountDetailRepository.ConsumerDurableItemBrandDropdownList;
            }
        }
        public List<SelectListItem> ConsumerDurableSupplierDropdownList
        {
            get
            {
                return accountDetailRepository.ConsumerDurableSupplierDropdownList;
            }
        }

        public List<SelectListItem> LoanSchemeDropdownList(Guid _loanTypeId)
        {
            return accountDetailRepository.GetLoanSchemeDropdownListByLoanTypeId(_loanTypeId);
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

        public List<SelectListItem> ScheduleDropdownList
        {
            get
            {
                return enterpriseDetailRepository.ScheduleDropdownList;
            }
        }

        public List<SelectListItem> MetalPurityDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "24 Carat",
                        Value = "24K"
                    },
                    new SelectListItem
                    {
                        Text = "22 Carat",
                        Value = "22K"
                    },
                    new SelectListItem
                    {
                        Text = "18 Carat",
                        Value = "18K"
                    },
                    new SelectListItem
                    {
                        Text = "14 Carat",
                        Value = "14K"
                    },
                    new SelectListItem
                    {
                        Text = "10 Carat",
                        Value = "10K"
                    }
                };
            }
        }

        public List<SelectListItem> PermitTypeDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "National Permit" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("National Permit"),
                        Value = "NTP"
                    },

                    new SelectListItem
                    {
                        Text = "State Permit" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("State Permit"),
                        Value = "STP"
                    },

                    new SelectListItem
                    {
                        Text ="Temporary Permit" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Temporary Permit"),
                        Value ="TMP"
                    },

                    new SelectListItem
                    {
                        Text ="Tourist Permit" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Tourist Permit"),
                        Value ="TRP"
                    },

                    new SelectListItem
                    {
                        Text="Contract Permit" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Contract Permit"),
                        Value ="CNP"
                    },

                    new SelectListItem
                    {
                        Text="Goods Carrier Permit" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Goods Carrier Permit"),
                        Value ="GCP "
                    }
                };
            }
        }

        public List<SelectListItem> TypeOfCoverageDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Third-Party",
                        Value = "TPR"
                    },
                    new SelectListItem
                    {
                        Text="Comprehensive",
                        Value = "CMP"
                    },
                    new SelectListItem
                    {
                        Text="Standalone Own Damage",
                        Value = "SOD"
                    },
                    new SelectListItem
                    {
                        Text="Pay As You Drive Insurance",
                        Value = "PAY"
                    },
                };
            }
        }

        public List<SelectListItem> PaymentModeDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Same Bank Transfer"+ " --> " + mlDetailRepository.TranslateInRegionalLanguage("Same Bank Transfer"),
                        Value = "SBN"
                    },
                    new SelectListItem
                    {
                        Text="Cheque"+ " --> " + mlDetailRepository.TranslateInRegionalLanguage("Cheque"),
                        Value = "CHQ"
                    },
                    new SelectListItem
                    {
                        Text="Cash"+ " --> " + mlDetailRepository.TranslateInRegionalLanguage("Cash"),
                        Value = "CAS"
                    },
                    new SelectListItem
                    {
                        Text="Online Payment"+ " --> " + mlDetailRepository.TranslateInRegionalLanguage("Online Payment"),
                        Value = "ONL"
                    },
                };
            }
        }

        public List<SelectListItem> PaymentFrequencyDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Trip"+ " --> " + mlDetailRepository.TranslateInRegionalLanguage("Trip"),
                        Value = "TPR"
                    },
                    new SelectListItem
                    {
                        Text="Weekly"+ " --> " + mlDetailRepository.TranslateInRegionalLanguage("Weekly"),
                        Value = "WKL"
                    },
                    new SelectListItem
                    {
                        Text="Bi – Weekly"+ " --> " + mlDetailRepository.TranslateInRegionalLanguage("Bi – Weekly"),
                        Value = "BWK"
                    },
                    new SelectListItem
                    {
                        Text="Monthly"+ " --> " + mlDetailRepository.TranslateInRegionalLanguage("Monthly"),
                        Value = "MON"
                    },
                    new SelectListItem
                    {
                        Text="Semi Annually"+ " --> " + mlDetailRepository.TranslateInRegionalLanguage("Semi Annually"),
                        Value = "SMA"
                    },
                    new SelectListItem
                    {
                        Text="Annually"+ " --> " + mlDetailRepository.TranslateInRegionalLanguage("Annually"),
                        Value = "ANL "
                    },
                };
            }
        }

        public List<SelectListItem> ContractNatureDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Transportation of Goods" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Transportation of Goods"),
                        Value = "TRG"
                    },
                 new SelectListItem
                    {
                        Text="Passenger Services" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Passenger Services"),
                        Value = "PGS"
                    },
                 new SelectListItem
                    {
                        Text="Courier/Delivery Services" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Courier/Delivery Services"),
                        Value = "CRS"
                    },
                  new SelectListItem
                    {
                        Text="Long-Term Leasing" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Long-Term Leasing"),
                        Value = "LTL"
                    },
                  new SelectListItem
                    {
                        Text="Others (specify)" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Others (specify)"),
                        Value = "OTH"
                    },
                };
            }
        }

        //VehicleConditionDropdownList
        public List<SelectListItem> VehicleConditionDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Average" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Average"),
                        Value = "AVR"
                    },
                    new SelectListItem
                    {
                        Text="Below Average" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Below Average"),
                        Value = "BAV"
                    },
                    new SelectListItem
                    {
                        Text="Excellent" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Excellent"),
                        Value = "EXC"
                    },
                    new SelectListItem
                    {
                        Text="Good" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Good"),
                        Value = "GOD"
                    },
                    new SelectListItem
                    {
                        Text="Not Functional" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Not Functional"),
                        Value = "NFN"
                    }
                };
            }
        }

        //VehicleInteriorConditionDropdownList
        public List<SelectListItem> VehicleInteriorConditionDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Average" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Average"),
                        Value = "AVR"
                    },
                    new SelectListItem
                    {
                        Text="Below Average" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Below Average"),
                        Value = "BAV"
                    },
                    new SelectListItem
                    {
                        Text="Excellent" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Excellent"),
                        Value = "EXC"
                    },
                    new SelectListItem
                    {
                        Text="Good" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Good"),
                        Value = "GOD"
                    },
                    new SelectListItem
                    {
                        Text="Poor" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Poor"),
                        Value = "POR"
                    }
                };
            }
        }

        //VehicleInsuranceStatusDropdownList
        public List<SelectListItem> VehicleInsuranceStatusDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Expired Insurance" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Expired Insurance"),
                        Value = "EXP"
                    },
                    new SelectListItem
                    {
                        Text="Fully Insured" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Fully Insured"),
                        Value = "FIN"
                    },
                    new SelectListItem
                    {
                        Text="Insurance Pending" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Insurance Pending"),
                        Value = "INP"
                    },
                    new SelectListItem
                    {
                        Text="Insurance Transfer In Progress" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Insurance Transfer In Progress"),
                        Value = "INT"
                    },
                    new SelectListItem
                    {
                        Text="Not Insured" + " -->" + mlDetailRepository.TranslateInRegionalLanguage("Not Insured"),
                        Value = "NOI"
                    }

                };
            }
        }

        public List<SelectListItem> DemandDepositCustomerDropdownList
        {
            get
            {
                return accountDetailRepository.DemandDepositAccountHolderDropdownList;
            }
        }

        public List<SelectListItem> TenureListDropdownList
        {
            get
            {
                return accountDetailRepository.TenureListDropdownList;
            }
        }

        public List<SelectListItem> TransactionTypeDropdownList
        {
            get
            {
                return accountDetailRepository.TransactionTypeDropdownList;
            }
        }

        public List<SelectListItem> VehicleMakeDropdownList
        {
            get
            {
                return accountDetailRepository.VehicleMakeDropdownList;
            }
        }

        public List<SelectListItem> VehicleModelDropdownList
        {
            get
            {
                return accountDetailRepository.VehicleModelDropdownList;
            }
        }

        public List<SelectListItem> VehicleVariantDropdownList
        {
            get
            {
                return accountDetailRepository.VehicleVariantDropdownList;
            }
        }

        public List<SelectListItem> VehicleSupplierDropdownList
        {
            get
            {
                return accountDetailRepository.VehicleSupplierDropdownList;
            }
        }

        public List<SelectListItem> InvestigationOfficerDropdownList
        {
            get
            {
                return managementDetailRepository.InvestigationOfficerDropdownList;
            }
        }

        public List<SelectListItem> InsuranceCompanyDropdownList
        {
            get
            {
                return personDetailRepository.InsuranceCompanyDropdownList;
            }
        }

        public List<SelectListItem> LoanTypeDropdownList
        {
            get
            {
                return accountDetailRepository.LoanTypeDropdownList;
            }
        }

        public List<SelectListItem> NoticeTypeDropdownList
        {
            get
            {
                return managementDetailRepository.NoticeTypeDropdownList;
            }
        }

        public List<SelectListItem> SchemeLoanAccountParameterDropdownList
        {
            get
            {
                return accountDetailRepository.SchemeLoanAccountParameterDropdownList;
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

        public List<SelectListItem> AddressTypeDropdownList
        {
            get
            {
                return personDetailRepository.AddressTypeDropdownList;
            }
        }

        public List<SelectListItem> CityDropdownList
        {
            get
            {
                return personDetailRepository.CityDropdownList;
            }
        }

        public List<SelectListItem> PhotoTypeDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Regular",
                        Value = "RGL"
                    },
                    new SelectListItem
                    {
                        Text="Weighing Photo",
                        Value = "WGH"
                    },
                    new SelectListItem
                    {
                        Text="Damage Photo",
                        Value = "DMG"
                    },
                    new SelectListItem
                    {
                        Text="Westage Photo",
                        Value = "WST"
                    },
                    new SelectListItem
                    {
                        Text="Diamond Photo",
                        Value = "DMN"
                    },
                    new SelectListItem
                    {
                        Text="Ownership Proof",
                        Value = "OWN"
                    },
                    new SelectListItem
                    {
                        Text="Other",
                        Value = "OTH"
                    }
                };
            }
        }

        //public List<SelectListItem> PropertyOwnershipStatusDropdownList
        //{
        //    get
        //    {
        //        return new List<SelectListItem>
        //        {
        //            new SelectListItem
        //            {
        //                Text="First-time buyer (for new home purchases)" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("First-time buyer (for new home purchases)"),
        //                Value = "FTB"
        //            },
        //         new SelectListItem
        //            {
        //                Text="Existing homeowner (for refinancing or second property purchase)" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Existing homeowner (for refinancing or second property purchase)"),
        //                Value = "EHO"
        //            },
        //         new SelectListItem
        //            {
        //                Text="Co-ownership (if purchasing jointly with another individual)" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Co-ownership (if purchasing jointly with another individual)"),
        //                Value = "COS"
        //            },
        //          new SelectListItem
        //            {
        //                Text="Other" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Other"),
        //                Value = "OTR"
        //            },

        //        };
        //    }
        //}

        public List<SelectListItem> PropertyOwnershipStatusDropdownList
        {
            get
            {
                return new List<SelectListItem>
         {
             new SelectListItem
             {
                 Text="Leased" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Leased"),
                 Value = "LEA"
             },
          new SelectListItem
             {
                 Text="Mortgaged" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Mortgaged"),
                 Value = "MRT"
             },
          new SelectListItem
             {
                 Text="Owned" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Owned"),
                 Value = "OWN"
             },
           new SelectListItem
             {
                 Text="Other" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Other"),
                 Value = "OTH"
             },

         };
            }
        }



        //public List<SelectListItem> PropertyConditionDropdownList
        //{
        //    get
        //    {
        //        return new List<SelectListItem>
        //        {
        //            new SelectListItem
        //            {
        //                Text="New/Brand New (for newly constructed homes)" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("New/Brand New (for newly constructed homes)"),
        //                Value = "NEW"
        //            },

        //         new SelectListItem
        //            {
        //                Text="Like New (recently renovated or in excellent condition)" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Like New (recently renovated or in excellent condition)"),
        //                Value = "LNW"
        //            },

        //         new SelectListItem
        //            {
        //                Text="Good Condition (standard wear and tear, well-maintained)" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Good Condition (standard wear and tear, well-maintained)"),
        //                Value = "GCN"
        //            },

        //          new SelectListItem
        //            {
        //                Text="Fair Condition (may need repairs or updates)" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Fair Condition (may need repairs or updates)"),
        //                Value = "FCN"
        //            },

        //            new SelectListItem
        //            {
        //                Text="Poor Condition (significant repairs required)" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Poor Condition (significant repairs required)"),
        //                Value = "PCN"
        //            },

        //        };
        //    }
        //}

        public List<SelectListItem> PropertyConditionDropdownList
        {
            get
            {
                return new List<SelectListItem>
        {
            new SelectListItem
            {
                Text="New" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("New"),
                Value = "NEW"
            },

            new SelectListItem
            {
                Text="Good" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Good"),
                Value = "GUD"
            },

            new SelectListItem
            {
                Text="Needs Repair" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Fair Condition (may need repairs or updates)"),
                Value = "NRP"
            },

        };
            }
        }


        //PropertyTypeDropdownList
        //public List<SelectListItem> PropertyTypeDropdownList
        //{
        //    get
        //    {
        //        return new List<SelectListItem>
        //        {
        //            new SelectListItem
        //            {
        //                Text="House" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("House"),
        //                Value = "HOS"
        //            },
        //            new SelectListItem
        //            {
        //                Text="Apartment" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Apartment"),
        //                Value = "APT"
        //            },
        //            new SelectListItem
        //            {
        //                Text="Land (Vacant plot)" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Land (Vacant plot)"),
        //                Value = "LND"
        //            },
        //            new SelectListItem
        //            {
        //                Text="Commercial Property (if applicable)" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Commercial Property (if applicable)"),
        //                Value = "CPT"
        //            },
        //            new SelectListItem
        //            {
        //                Text="Other" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Other"),
        //                Value = "OTR"
        //            },

        //        };
        //    }
        //}

        //PropertyTypeDropdownList
        public List<SelectListItem> PropertyTypeDropdownList
        {
            get
            {
                return new List<SelectListItem>
        {
            new SelectListItem
            {
                Text="Commercial" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Commercial"),
                Value = "COM"
            },
            new SelectListItem
            {
                Text="Industrial" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Industrial"),
                Value = "IND"
            },
            new SelectListItem
            {
                Text="Residential" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Residential"),
                Value = "RES"
            },
            new SelectListItem
            {
                Text="Other" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Other"),
                Value = "OTH"
            },

        };
            }
        }



        //PropertyUsageDropdownList   OCU - For Occupied, RNT - For Rented, VCT - For Vacant.
        public List<SelectListItem> PropertyUsageDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Occupied" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Occupied"),
                        Value = "OCU"
                    },
                    new SelectListItem
                    {
                        Text="Rented" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Rented"),
                        Value = "RNT"
                    },
                    new SelectListItem
                    {
                        Text="Vacant" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Vacant)"),
                        Value = "VCT"
                    },
                };
            }
        }

        //CountryDropdownList
        public List<SelectListItem> CountryDropdownList
        {
            get
            {
                return personDetailRepository.CountryDropdownList;
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

        public List<SelectListItem> ColourDropdownList
        {
            get
            {
                return accountDetailRepository.ColourDropdownList;
            }
        }

        public string NameOfUser
        {
            get
            {
                return accountDetailRepository.GetUserNameByUserProfilePrmKey((short)HttpContext.Current.Session["UserProfilePrmKey"]);

            }
        }

        public bool IsAuthorizedUser
        {
            get
            {
                return accountDetailRepository.GetAuthorizedUserStatusByPrmKey((short)HttpContext.Current.Session["UserProfilePrmKey"]);

            }
        }

    }
}
