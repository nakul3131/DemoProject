using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("AppConfig")]
    public partial class AppConfig
    {
        [Key]
        public short PrmKey { get; set; }

        public DateTime EffectiveDate { get; set; }

        public short DefaultLanguagePrmKey { get; set; }

        public bool EnableCooperativeRegistration { get; set; }

        public bool EnableRBIRegistration { get; set; }

        public bool EnableMultiCurrency { get; set; }

        public short DefaultCurrencyPrmKey { get; set; }

        public byte NumberOfBranch { get; set; }

        public bool EnablePaymentBranchWise { get; set; }

        public byte MaxNumOfSuspiciousActivityAttempts { get; set; }

        public bool EnableAIBranchWise { get; set; }

        public bool EnableDigitalCodeFacility { get; set; }

        public bool EnableCoreBanking { get; set; }

        public bool EnableSmsService { get; set; }

        public bool EnableEmailService { get; set; }

        //public bool IsBranchwiseDeviceAuthenticationEnabled { get; set; }

        //public bool IsBranchwiseUserAutenticationParameterConfigEnabled { get; set; }
    }
}
