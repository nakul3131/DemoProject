using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.Abstract.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonDetailViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IPersonInformationParameterNoticeTypeRepository personInformationParameterNoticeTypeRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;

        public PersonDetailViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            personInformationParameterNoticeTypeRepository = DependencyResolver.Current.GetService<IPersonInformationParameterNoticeTypeRepository>();
            securityDetailRepository = DependencyResolver.Current.GetService<ISecurityDetailRepository>();
        }

        // Translation In Regional        
        [StringLength(100)]
        public string FirstNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("First Name");
            }
        }

        [StringLength(100)]
        public string FirstNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter First Name");
            }
        }

        [StringLength(100)]
        public string MiddleNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Middle Name");
            }
        }

        [StringLength(100)]
        public string MiddleNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Middle Name");
            }
        }

        [StringLength(100)]
        public string LastNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Last Name");
            }
        }

        [StringLength(100)]
        public string LastNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Last Name");
            }
        }

        [StringLength(100)]
        public string FullNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Full Name");
            }
        }

        [StringLength(100)]
        public string FullNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Full Name");
            }
        }

        [StringLength(100)]
        public string MotherNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Mother Name");
            }
        }

        [StringLength(100)]
        public string MotherNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Mother Name");
            }
        }

        [StringLength(100)]
        public string MothersMaidenNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Mothers Maiden Name");
            }
        }

        [StringLength(100)]
        public string MothersMaidenNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Mothers Maiden Name");
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
       
        //PrefixViewModel
        [StringLength(100)]
        public string PrefixInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Prefix");
            }
        }

        [StringLength(100)]
        public string PrefixPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Prefix");
            }
        }

        //EmployementViewModel
        [StringLength(100)]
        public string NameOfEmployerInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Employer");
            }
        }

        [StringLength(100)]
        public string NameOfEmployerPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Employer");
            }
        }

        [StringLength(100)]
        public string EPFNumberInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("EPF Number");
            }
        }

        [StringLength(100)]
        public string EPFNumberPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter EPF Number");
            }
        }

        [StringLength(100)]
        public string EmployerNatureOtherDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Employer Nature Other Details");
            }
        }

        [StringLength(100)]
        public string EmployerNatureOtherDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Employer Nature Other Details");
            }
        }

        [StringLength(100)]
        public string EmployerAddressDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Employer Address Details");
            }
        }

        [StringLength(100)]
        public string EmployerAddressDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Employer Address Details");
            }
        }

        [StringLength(100)]
        public string EmployerContactDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Employer Contact Details");
            }
        }

        [StringLength(100)]
        public string EmployerContactDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Employer Contact Details");
            }
        }

        //GuardianViewModel
        [StringLength(100)]
        public string GuardianFullNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Full Name");
            }
        }

        [StringLength(100)]
        public string GuardianFullNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Full Name");
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

        //AdditionalDetail

        [StringLength(100)]
        public string LifePartnerNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Life Partner Name");
            }
        }

        [StringLength(100)]
        public string LifePartnerNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Life Partner Name");
            }
        }

        [StringLength(100)]
        public string LifePartnerMaidenNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Life Partner Maiden Name");
            }
        }

        [StringLength(100)]
        public string LifePartnerMaidenNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Life Partner Maiden Name");
            }
        }

        [StringLength(100)]
        public string PoliticialBackgroundDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Politicial Background Details");
            }
        }

        [StringLength(100)]
        public string PoliticialBackgroundDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Politicial Background Details");
            }
        }

        [StringLength(100)]
        public string VIPBackgroundDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("VIP Background Details");
            }
        }

        [StringLength(100)]
        public string VIPBackgroundDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter VIP Background Details");
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
        public string BranchInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Branch");
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
        public string FullNameOfFamilyMemberInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Full Name Of Family Member");
            }
        }

        [StringLength(100)]
        public string FullNameOfFamilyMemberPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Full Name Of Family Member");
            }
        }

        [StringLength(100)]
        public string NameOfFinancialOrganizationInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Financial Organization");
            }
        }

        [StringLength(100)]
        public string NameOfFinancialOrganizationPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Financial Organization");
            }
        }

        [StringLength(100)]
        public string NameOfBranchInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Branch");
            }
        }

        [StringLength(100)]
        public string NameOfBranchPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Branch");
            }
        }

        [StringLength(100)]
        public string AddressDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Address Details");
            }
        }

        [StringLength(100)]
        public string AddressDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Address Details");
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
        public string FinancialAssetDescriptionInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Financial Asset Description");
            }
        }

        [StringLength(100)]
        public string FinancialAssetDescriptionPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Financial Asset Description");
            }
        }

        [StringLength(100)]
        public string ReferenceNumberInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Reference Number");
            }
        }

        [StringLength(100)]
        public string ReferenceNumberPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Reference Number");
            }
        }

        [StringLength(100)]
        public string FurnitureFullDetailsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Furniture Full Details");
            }
        }
        
        [StringLength(100)]
        public string FurnitureFullDetailsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Furniture Full Details");
            }
        }

        public string FullNameOfAuthorizedPersonInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Full Name Of Authorized Person");
            }
        }

        [StringLength(100)]
        public string FullNameOfAuthorizedPersonPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Full Name Of Authorized Person");
            }
        }

        public string AuthorizedPersonAddressDetailInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Authorized Person Address Detail");
            }
        }

        [StringLength(100)]
        public string AuthorizedPersonAddressDetailPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Authorized Person Address Detail");
            }
        }

        public string AuthorizedPersonContactDetailInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Authorized Person Contact Detail");
            }
        }

        [StringLength(100)]
        public string AuthorizedPersonContactDetailPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Authorized Person Contact Detail");
            }
        }

        public string StrengthsFactorsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Strengths Factors");
            }
        }

        [StringLength(100)]
        public string StrengthsFactorsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Strengths Factors");
            }
        }

        public string WeaknessesFactorsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Weaknesses Factors");
            }
        }

        [StringLength(100)]
        public string WeaknessesFactorsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Weaknesses Factors");
            }
        }

        public string OpportunitiesFactorsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Opportunities Factors");
            }
        }

        [StringLength(100)]
        public string OpportunitiesFactorsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Opportunities Factors");
            }
        }

        public string ThreatsFactorsInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Threats Factors");
            }
        }

        [StringLength(100)]
        public string ThreatsFactorsPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Threats Factors");
            }
        }

        public string PastCreditHistoryInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Past Credit History");
            }
        }

        [StringLength(100)]
        public string PastCreditHistoryPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Past Credit History");
            }
        }

        public string LegalAndRegulatoryComplianceInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Legal And Regulatory Compliance");
            }
        }

        [StringLength(100)]
        public string LegalAndRegulatoryCompliancePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Legal And Regulatory Compliance");
            }
        }


        // DropdownList
        public List<SelectListItem> PrefixDropdownList
        {
            get
            {
                return personDetailRepository.PrefixDropdownList;
            }
        }

        public List<SelectListItem> OccupationDropdownList
        {
            get
            {
                return personDetailRepository.OccupationDropdownList;
            }
        }

        public List<SelectListItem> EmploymentTypeDropdownList
        {
            get
            {
                return managementDetailRepository.EmploymentTypeDropdownList;
            }
        }

        public List<SelectListItem> EmployerNatureDropdownList
        {
            get
            {
                return managementDetailRepository.EmployerNatureDropdownList;
            }
        }

        public List<SelectListItem> CityDropdownList
        {
            get
            {
                return personDetailRepository.CityDropdownList;
            }
        }

        public List<SelectListItem> EmployeeDesignationDropdownList
        {
            get
            {
                return managementDetailRepository.EmployeeDesignationDropdownList;
            }
        }

        public List<SelectListItem> RelationDropdownList
        {
            get
            {
                return personDetailRepository.RelationDropdownList;
            }
        }

        public List<SelectListItem> VillageTownCityDropdownList
        {
            get
            {
                return personDetailRepository.VillageTownCityDropdownList;
            }

        }

        public List<SelectListItem> GenderDropdownList
        {
            get
            {
                return personDetailRepository.GenderDropdownList;
            }
        }

        public List<SelectListItem> BloodGroupDropdownList
        {
            get
            {
                return personDetailRepository.BloodGroupDropdownList;
            }
        }

        public List<SelectListItem> MaritalStatusDropdownList
        {
            get
            {
                return personDetailRepository.MaritalStatusDropdownList;
            }
        }

        public List<SelectListItem> PersonTypeDropdownList
        {
            get
            {
                return personDetailRepository.PersonTypeDropdownList;
            }
        }

        public List<SelectListItem> PovertyStatusDropdownList
        {
            get
            {
                return personDetailRepository.PovertyStatusDropdownList;
            }
        }

        public List<SelectListItem> PhysicalStatusDropdownList
        {
            get
            {
                return personDetailRepository.PhysicalStatusDropdownList;
            }
        }

        public List<SelectListItem> CastCategoryDropdownList
        {
            get
            {
                return personDetailRepository.CastCategoryDropdownList;
            }

        }

        public List<SelectListItem> EducationQualificationDropdownList
        {
            get
            {
                return personDetailRepository.EducationQualificationDropdownList;
            }
        }

        public List<SelectListItem> NatureOfEmployerDropdownList
        {
            get
            {
                return personDetailRepository.NatureOfEmployerDropdownList;
            }
        }

        public List<SelectListItem> DesignationDropdownList
        {
            get
            {
                return managementDetailRepository.EmployeeDesignationDropdownList;
            }
        }

        public List<SelectListItem> PersonCategoryDropdownList
        {
            get
            {
                return personDetailRepository.PersonCategoryDropdownList;
            }
        }

        public List<SelectListItem> IncomeSourceDropdownList
        {
            get
            {
                return personDetailRepository.IncomeSourceDropdownList;
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

        public List<SelectListItem> AgricultureLandTypeDropdownList
        {
            get
            {
                return accountDetailRepository.AgricultureLandTypeDropdownList;
            }
        }

        public List<SelectListItem> BankDropdownList
        {
            get
            {
                return enterpriseDetailRepository.BankDropdownList;
            }
        }

        public List<SelectListItem> AuthorizedBusinessOfficeDropdownList
        {
            get
            {
                return accountDetailRepository.AuthorizedBusinessOfficeDropdownList;
            }
        }

        public List<SelectListItem> BoardOfDirectorDropdownList
        {
            get
            {
                return managementDetailRepository.BoardOfDirectorDropdownList;
            }
        }

        public List<SelectListItem> FamilyRelationDropdownList
        {
            get
            {
                return personDetailRepository.FamilyRelationDropdownList;
            }
        }

        public List<SelectListItem> CourtCaseTypeDropdownList
        {
            get
            {
                return personDetailRepository.CourtCaseTypeDropdownList;
            }

        }

        public List<SelectListItem> CourtCaseStageDropdownList
        {
            get
            {
                return personDetailRepository.CourtCaseStageDropdownList;
            }

        }

        public List<SelectListItem> DiseaseDropdownList
        {
            get
            {
                return personDetailRepository.DiseaseDropdownList;
            }
        }

        public List<SelectListItem> ContactTypeDropdownList
        {
            get
            {
                return personDetailRepository.ContactTypeDropdownList;
            }

        }

        public List<SelectListItem> CreditBureauAgencyDropdownList
        {
            get
            {
                return accountDetailRepository.CreditBureauAgencyDropdownList;
            }
        }

        public List<SelectListItem> FinancialAssetTypeDropdownList
        {
            get
            {
                return accountDetailRepository.FinancialAssetTypeDropdownList;
            }
        }

        public List<SelectListItem> FinancialOrganizationTypeDropdownList
        {
            get
            {
                return enterpriseDetailRepository.FinancialOrganizationTypeDropdownList;
            }
        }

        public List<SelectListItem> FurnitureAssetTypeDropdownList
        {
            get
            {
                return accountDetailRepository.FurnitureAssetTypeDropdownList;
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

        public List<SelectListItem> IdentificationDropdownList
        {
            get
            {
                return personDetailRepository.IdentificationDropdownList;
            }
        }

        public List<SelectListItem> InsuranceTypeDropdownList
        {
            get
            {
                return personDetailRepository.InsuranceTypeDropdownList;
            }
        }

        public List<SelectListItem> InsuranceCompanyDropdownList
        {
            get
            {
                return personDetailRepository.InsuranceCompanyDropdownList;
            }
        }

        public List<SelectListItem> PersonDocumentTypeDropdownList
        {
            get
            {
                return personDetailRepository.PersonDocumentTypeDropdownList;
            }
        }

        public List<SelectListItem> VehicleBodyTypeDropdownList
        {
            get
            {
                return accountDetailRepository.VehicleBodyTypeDropdownList;
            }
        }

        public List<SelectListItem> VehicleModelDropdownList
        {
            get
            {
                return accountDetailRepository.VehicleModelDropdownList;
            }
        }

        public List<SelectListItem> VehicleMakeDropdownList
        {
            get
            {
                return accountDetailRepository.VehicleMakeDropdownList;
            }
        }

        public List<SelectListItem> VehicleVariantDropdownList
        {
            get
            {
                return accountDetailRepository.VehicleVariantDropdownList;
            }
        }

        public List<SelectListItem> AppLanguageDropdownList
        {
            get
            {
                return configurationDetailRepository.AppLanguageDropdownList;
            }
        }

        public List<SelectListItem> PersonInformationParameterDropdownList
        {
            get
            {
                return personDetailRepository.PersonInformationParameterDropdownList;
            }
        }

        public List<SelectListItem> SocialMediaDropdownList
        {
            get
            {
                return managementDetailRepository.SocialMediaDropdownList;
            }
        }

        public List<SelectListItem> LanguageDropdownList
        {
            get
            {
                return configurationDetailRepository.LanguageDropdownList;
            }
        }
        
        public List<SelectListItem> BusinessOfficeDropdownList
        {
            get
            {
                return enterpriseDetailRepository.BusinessOfficeDropdownList;
            }
        }


        //BusinessNatureDropdownList
        public List<SelectListItem> BusinessNatureDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Agricultural Businesses" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Agricultural Businesses"),
                        Value = "AGR"
                    },
                    new SelectListItem
                    {
                        Text="Creative and Media Businesses" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Creative and Media Businesses"),
                        Value = "MED"
                    },
                    new SelectListItem
                    {
                        Text="Digital and Technology-Based Businesses" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Digital and Technology-Based Businesses"),
                        Value = "DGT"
                    },
                    new SelectListItem
                    {
                        Text="Export-Oriented Businesses" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Export-Oriented Businesses"),
                        Value = "EPT"
                    },
                    new SelectListItem
                    {
                        Text="Financial Businesses" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Financial Businesses"),
                        Value = "FIN"
                    },
                    new SelectListItem
                    {
                        Text="Franchise Businesses" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Franchise Businesses"),
                        Value = "FRN"
                    },
                    new SelectListItem
                    {
                        Text="Hospitality and Tourism Businesses" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Hospitality and Tourism Businesses"),
                        Value = "HTR"
                    },
                    new SelectListItem
                    {
                        Text="Manufacturing Businesses" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Manufacturing Businesses"),
                        Value = "MNF"
                    },
                    new SelectListItem
                    {
                        Text="Non-Profit and Social Enterprises" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Non-Profit and Social Enterprises"),
                        Value = "NPR"
                    },
                    new SelectListItem
                    {
                        Text="Real Estate and Construction Businesses" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Real Estate and Construction Businesses"),
                        Value = "RES"
                    },
                    new SelectListItem
                    {
                        Text="Service Businesses" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Service Businesses"),
                        Value = "SER"
                    },
                    new SelectListItem
                    {
                        Text="Trading Businesses" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Trading Businesses"),
                        Value = "TRD"
                    },
                };
            }
        }

        public List<SelectListItem> BusinessTypeDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text="Branch Office, Liaison Office, and Project Office" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Branch Office, Liaison Office, and Project Office"),
                        Value = "BRO"
                    },
                    new SelectListItem
                    {
                        Text="Cooperative Society" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Cooperative Society"),
                        Value = "COP"
                    },
                    new SelectListItem
                    {
                        Text="Hindu Undivided Family" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Hindu Undivided Family"),
                        Value = "HUF"
                    },
                    new SelectListItem
                    {
                        Text="Joint Venture" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Joint Venture"),
                        Value = "JVN"
                    },
                    new SelectListItem
                    {
                        Text="Limited Liability Partnership" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Limited Liability Partnership"),
                        Value = "LLP"
                    },
                    new SelectListItem
                    {
                        Text="Non-Governmental / Profit Organization" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Sole Proprietorship"),
                        Value = "NGO"
                    },
                    new SelectListItem
                    {
                        Text="One Person Company" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("One Person Company"),
                        Value = "OPC"
                    },
                    new SelectListItem
                    {
                        Text="Partnership" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Partnership"),
                        Value = "PNT"
                    },
                    new SelectListItem
                    {
                        Text="Private Limited Company" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Private Limited Company"),
                        Value = "PVT"
                    },
                    new SelectListItem
                    {
                        Text="Public Limited Company" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Public Limited Company"),
                        Value = "PLC"
                    },
                    new SelectListItem
                    {
                        Text="Sole Proprietorship" + " --> " + mlDetailRepository.TranslateInRegionalLanguage("Sole Proprietorship"),
                        Value = "PRP"
                    },
                };
            }
        }

    }
}
