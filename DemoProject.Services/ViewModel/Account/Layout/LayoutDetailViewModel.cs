using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Enterprise.Schedule;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class LayoutDetailViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IMLDetailRepository iMLDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly ISharesCapitalByLawsParameterRepository sharesCapitalByLawsParameterRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IOfficeScheduleRepository officeScheduleRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public LayoutDetailViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            iMLDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            sharesCapitalByLawsParameterRepository = DependencyResolver.Current.GetService<ISharesCapitalByLawsParameterRepository>();
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            officeScheduleRepository = DependencyResolver.Current.GetService<IOfficeScheduleRepository>();
            enterpriseDetailRepository = DependencyResolver.Current.GetService<IEnterpriseDetailRepository>();
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
        }

        // Translation In Regional
        [StringLength(100)]
        public string NameOfSchemeInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Name Of Scheme");
            }
        }

        [StringLength(100)]
        public string NameOfSchemePlaceHolderInRegionalLanguage
        {
            get
            {
                return iMLDetailRepository.TranslateInRegionalLanguage("Enter Name Of Scheme");
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

        //DropDown
        public List<SelectListItem> SchemeTypeDropdownList
        {
            get
            {
                return accountDetailRepository.SchemeTypeDropdownList;
            }
        }

        public List<SelectListItem> MaskTypeDropdownList
        {
            get
            {
                return configurationDetailRepository.MaskTypeDropdownList;
            }
        }

        public List<SelectListItem> ReportTypeFormatDropdownList
        {
            get
            {
                return configurationDetailRepository.ReportTypeFormatDropdownList;
            }
        }

        public List<SelectListItem> CommunicationMediaDropdownList
        {
            get
            {
                return managementDetailRepository.CommunicationMediaDropdownList;
            }
        }

        public List<SelectListItem> EducationalCourseDropdownList
        {
            get
            {
                return accountDetailRepository.EducationalCourseDropdownList;
            }
        }

        public List<SelectListItem> InstituteDropdownList
        {
            get
            {
                return accountDetailRepository.InstituteDropdownList;
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

        public List<SelectListItem> AccountClosingChargesGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.AccountClosingChargesGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> AccountTransferChargesGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.AccountTransferChargesGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> SharesTransferChargesGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.SharesTransferChargesGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> AgentCommissionGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.AgentCommissionGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> TimePeriodUnitDropdownList
        {
            get
            {
                return configurationDetailRepository.TimePeriodUnitDropdownList;
            }
        }

        public List<SelectListItem> FrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.FrequencyDropdownList;
            }
        }

        public List<SelectListItem> InterestTypeDropdownList
        {
            get
            {
                return accountDetailRepository.InterestTypeDropdownList;
            }
        }

        public List<SelectListItem> DepositInterestPaidGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.InterestPaidOnDepositGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> DepositInterestProvisionGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.DepositInterestProvisionGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> LoanInterestProvisionGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.LoanInterestProvisonGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> InterestMethodDropdownList
        {
            get
            {
                return accountDetailRepository.InterestMethodDropdownList;
            }
        }

        public List<SelectListItem> InterestCalculationFrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.InterestCalculationFrequencyDropdownList;
            }
        }

        public List<SelectListItem> InterestRateChargedDurationDropdownList
        {
            get
            {
                return accountDetailRepository.InterestRateChargedDurationDropdownList;
            }
        }

        public List<SelectListItem> LoanFineInterestGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.FineInterestReceivedOnLoanGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> LoanInterestGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.InterestReceivedOnLoanGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> PaymentCardDropdownList
        {
            get
            {
                return accountDetailRepository.PaymentCardDropdownList;
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

        public List<SelectListItem> PayInPayOutModeDropdownList
        {
            get
            {
                return accountDetailRepository.PayInPayOutModeDropdownList;
            }
        }

        public List<SelectListItem> BalanceTypeDropdownList
        {
            get
            {
                return accountDetailRepository.BalanceTypeDropdownList;
            }
        }

        public List<SelectListItem> BusinessOfficeDropdownList
        {
            get
            {
                return enterpriseDetailRepository.BusinessOfficeDropdownList;
            }
        }

        public List<SelectListItem> FileFormatTypes
        {
            get
            {
                return configurationDetailRepository.FileFormatTypes;
            }
        }

        public List<SelectListItem> DividendCalculationMethodDropdownList
        {
            get
            {
                return accountDetailRepository.DividendCalculationMethodDropdownList;
            }
        }

        public List<SelectListItem> ChequeReturnReasonDropdownList
        {
            get
            {
                return accountDetailRepository.ChequeReturnReasonDropdownList;
            }
        }

        public List<SelectListItem> InterestRateTypeDropdownList
        {
            get
            {
                return accountDetailRepository.InterestRateTypeDropdownList;
            }
        }

        public List<SelectListItem> LoanTypeDropdownList
        {
            get
            {
                return accountDetailRepository.LoanTypeDropdownList;
            }
        }

        public List<SelectListItem> OrganizationLoanTypeDropdownList
        {
            get
            {
                return accountDetailRepository.OrganizationLoanTypeDropdownList;
            }
        }

        public List<SelectListItem> EligibilityForGuarantorDropdownList
        {
            get
            {
                return new List<SelectListItem>{
                    new SelectListItem{
                    Text="Active Member" + " --> " + iMLDetailRepository.TranslateInRegionalLanguage("Active Member"),
                    Value = "ACT"
                },
                    new SelectListItem{
                    Text="All" + " --> " + iMLDetailRepository.TranslateInRegionalLanguage("All"),
                    Value = "ALL"
                },
                     new SelectListItem{
                    Text="Any Member" + " --> " + iMLDetailRepository.TranslateInRegionalLanguage("Any Member"),
                    Value = "ANY"
                },
                     new SelectListItem{
                    Text="Depositor" + " --> " + iMLDetailRepository.TranslateInRegionalLanguage("Depositor"),
                    Value = "DEP"
                },
                    new SelectListItem{
                    Text="Nominal Member" + " --> " + iMLDetailRepository.TranslateInRegionalLanguage("Nominal Member"),
                    Value = "NOM"
                },
                new SelectListItem{
                    Text="Ordinary Member" + " --> " + iMLDetailRepository.TranslateInRegionalLanguage("Ordinary Member"),
                    Value = "ORD"
                }
            };
            }
        }

        public List<SelectListItem> DocumentTypeDropdownList
        {
            get
            {
                return personDetailRepository.DocumentTypeDropdownList;
            }
        }

        public List<SelectListItem> TargetGroupDropdownList
        {
            get
            {
                return accountDetailRepository.TargetGroupDropdownList;
            }
        }

        public List<SelectListItem> GenderDropdownList
        {
            get
            {
                return personDetailRepository.GenderDropdownList;
            }
        }

        public List<SelectListItem> OccupationDropdownList
        {
            get
            {
                return personDetailRepository.OccupationDropdownList;
            }
        }

        public List<SelectListItem> CustomerTypeDropdownList
        {
            get
            {
                return accountDetailRepository.CustomerTypeDropdownList;
            }
        }

        public List<SelectListItem> RepaymentIntervalFrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.RepaymentIntervalFrequencyDropdownList;
            }
        }

        public List<SelectListItem> LendingInterestMethodDropdownList
        {
            get
            {
                return accountDetailRepository.LendingInterestMethodDropdownList;
            }
        }

        public List<SelectListItem> DaysInYearDropdownList
        {
            get
            {
                return accountDetailRepository.DaysInYearDropdownList;
            }
        }

        public List<SelectListItem> LendingRepaymentsInterestCalculationDropdownList
        {
            get
            {
                return accountDetailRepository.LendingRepaymentsInterestCalculationDropdownList;
            }
        }

        public List<SelectListItem> LendingInterestPostingFrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.LendingInterestPostingFrequencyDropdownList;
            }
        }

        public List<SelectListItem> InterestCompoundingFrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.InterestCompoundingFrequencyDropdownList;
            }
        }

        public List<SelectListItem> ChargesTypeDropdownList
        {
            get
            {
                return accountDetailRepository.ChargesTypeDropdownList;
            }
        }

        public List<SelectListItem> LendingChargesBaseDropdownList
        {
            get
            {
                return accountDetailRepository.LendingChargesBaseDropdownList;
            }
        }

        public List<SelectListItem> InterestRebateCriteriaDropdownList
        {
            get
            {
                return accountDetailRepository.InterestRebateCriteriaDropdownList;
            }
        }

        public List<SelectListItem> InterestRebateApplyFrequencyDropdownList
        {
            get
            {
                return accountDetailRepository.InterestRebateApplyFrequencyDropdownList;
            }
        }

        public List<SelectListItem> InterestRebateGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.InterestRebateGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> LoanChargesGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.LoanChargesGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> LoanGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.EducationalLoanGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> LoanRecoveryActionDropdownList
        {
            get
            {
                return accountDetailRepository.LoanRecoveryActionDropdownList;
            }
        }

        public List<SelectListItem> DepositeGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.DepositGeneralLedgerAvailableForPledgeDropdownList;
            }
        }

        public List<SelectListItem> SharesCapitalGeneralLedgerDropdownList
        {
            get
            {
                return accountDetailRepository.SharesCapitalGeneralLedgerDropdownList;
            }
        }

        public List<SelectListItem> VehicleTypeDropdownList
        {
            get
            {
                return accountDetailRepository.VehicleTypeDropdownList;
            }
        }

        public List<SelectListItem> ConsumerDurableItemDropdownList
        {
            get
            {
                return accountDetailRepository.ConsumerDurableItemDropdownList;
            }
        }

        // Get Required Values
        public bool HasApplicationNumberBranchWise
        {
            get
            {
                return enterpriseDetailRepository.IsSetApplicationNumberBranchwise();
            }
        }

        public bool HasCustomerAccountNumberBranchWise
        {
            get
            {
                return enterpriseDetailRepository.IsSetCustomerAccountNumberBranchwise();
            }
        }

        public bool HasMemberNumberBranchWise
        {
            get
            {
                return enterpriseDetailRepository.IsSetMemberNumberBranchwise();
            }
        }

        public bool HasSharesCertificateNumberBranchWise
        {
            get
            {
                return enterpriseDetailRepository.IsSetSharesCertificateNumberBranchwise();
            }
        }

        public decimal RetailMaximumSharesHoldingLimitAmount
        {
            get
            {
                return accountDetailRepository.GetMaximumSharesHolidingLimitAmount();
            }
        }

        public decimal CorporateMaximumSharesHoldingLimitAmount
        {
            get
            {
                return accountDetailRepository.GetMaximumSharesHolidingLimitAmount();
            }
        }

        public decimal RetailMaximumSharesHoldingLimitPercentage
        {
            get
            {
                return accountDetailRepository.GetMaximumSharesHoldingLimitPercentage();
            }
        }

        public decimal CorporateMaximumSharesHoldingLimitPercentage
        {
            get
            {
                return accountDetailRepository.GetMaximumSharesHoldingLimitPercentage();
            }
        }

        public decimal RetailAccountTurnOverLimitMaximumSharesHoldingLimi
        {
            get
            {
                return accountDetailRepository.GetMaximumSharesHolidingLimitAmount();
            }
        }

        public decimal CorporateAccountTurnOverLimitMaximumSharesHoldingLimi
        {
            get
            {
                return accountDetailRepository.GetMaximumSharesHolidingLimitAmount();
            }
        }

        public decimal RetailHoldingAmountMaximumSharesHoldingLimi
        {
            get
            {
                return accountDetailRepository.GetMaximumSharesHoldingLimitPercentage();
            }
        }

        public decimal CorporateHoldingAmountMaximumSharesHoldingLimi
        {
            get
            {
                return accountDetailRepository.GetMaximumSharesHoldingLimitPercentage();
            }
        }

        public bool HasCoreBankingFeature
        {
            get
            {
                return configurationDetailRepository.HasCoreBankingFeature();
            }
        }

    }
}

