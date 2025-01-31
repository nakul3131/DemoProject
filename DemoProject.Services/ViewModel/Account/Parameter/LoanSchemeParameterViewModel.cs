using DemoProject.Services.Abstract.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Account.Parameter
{
    public class LoanSchemeParameterViewModel
    {
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public LoanSchemeParameterViewModel()
        {
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
        }

        // LoanSchemeParameter

        public byte PrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool EnableLockInPeriodParameter { get; set; }

        public bool EnableTenureListParameter { get; set; }

        public bool EnableApplicationParameter { get; set; }

        public bool EnablePassbookParameter { get; set; }

        public bool EnableDocumentParameter { get; set; }

        public bool EnableLoanPreFullPaymentParameter { get; set; }

        public bool EnableLoanPrePartPaymentParameter { get; set; }

        public bool EnableLoanInterestRebateParameter { get; set; }

        public bool EnableLoanFunderParameter { get; set; }

        public bool EnableLoanDistributorParameter { get; set; }

        public bool EnableRecoveryOfficerIncentiveParameter { get; set; }

        public bool EnableBusinessOfficeParameter { get; set; }

        public bool EnableBankingChannelParameter { get; set; }

        public bool EnableTargetGroupParameter { get; set; }

        public bool EnableSmsServiceParameter { get; set; }

        public bool EnableEmailServiceParameter { get; set; }

        public bool EnableNoticeScheduleParameter { get; set; }

        public bool EnableReportFormatParameter { get; set; }

        public bool EnableTargetEstimationParameter { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // LoanSchemeParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public byte LoanSchemeParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        public byte NumberOfBranches
        {
            get
            {
                return configurationDetailRepository.GetNumberOfBranches();
            }
        }

        public bool HasCoreBankingFeature
        {
            get
            {
                return configurationDetailRepository.HasCoreBankingFeature();
            }
        }

        public bool IsEnabledSmsService
        {
            get
            {
                return configurationDetailRepository.IsEnabledSmsService();
            }
        }

        public bool IsEnabledEmailService
        {
            get
            {
                return configurationDetailRepository.IsEnabledEmailService();
            }
        }

        public string NameOfUser { get; set; }
    }
}
