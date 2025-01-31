using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.Services.ViewModel.Account.Parameter
{
    public class DepositSchemeParameterViewModel
    {
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public DepositSchemeParameterViewModel()
        {
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
        }

        public byte PrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool EnableLockInPeriodParameter { get; set; }

        public bool EnableTenureListParameter { get; set; }

        public bool EnableApplicationParameter { get; set; }

        public bool EnableDepositCertificateParameter { get; set; }

        public bool EnablePassbookParameter { get; set; }

        public bool EnableAgentIncentiveParameter { get; set; }

        public bool EnableBankingChannelParameter { get; set; }

        public bool EnableBusinessOfficeParameter { get; set; }

        public bool EnableSmsServiceParameter { get; set; }

        public bool EnableEmailServiceParameter { get; set; }

        public bool EnableNoticeScheduleParameter { get; set; }

        public bool EnableClosingChargesParameter { get; set; }

        public bool EnableTargetGroupParameter { get; set; }

        public bool EnableTargetEstimationParameter { get; set; }

        public bool EnableNumberOfTransactionLimitParameter { get; set; }

        public bool EnableTransactionAmountLimitParameter { get; set; }

        public bool EnableReportFormatParameter { get; set; }

        public bool EnableLimitParameter { get; set; }


        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // DepositeSchemeparameterMakeChekar 
        public DateTime EntryDateTime { get; set; }

        public byte DepositSchemeParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }


        // Other
        public string NameOfUser { get; set; }

        // Enable Banking Channel Parameter
        public bool HasCoreBankingFeature
        {
            get
            {
                return configurationDetailRepository.HasCoreBankingFeature();
            }
        }

        // SMS and Email Hide Show
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

        // Other
        public byte NumberOfBranches
        {
            get
            {
                return configurationDetailRepository.GetNumberOfBranches();
            }
        }
    }
}
