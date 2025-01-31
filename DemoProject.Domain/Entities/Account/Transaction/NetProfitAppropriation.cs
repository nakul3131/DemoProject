using DemoProject.Domain.Entities.Management.Conference;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("NetProfitAppropriation")]
    public partial class NetProfitAppropriation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NetProfitAppropriation()
        {
            NetProfitAppropriationMakerCheckers = new HashSet<NetProfitAppropriationMakerChecker>();
        }

        [Key]
        public byte PrmKey { get; set; }

        public short FinancialYear { get; set; }

        public int MeetingAgendaPrmKey { get; set; }

        public decimal ReserveFundPercentage { get;set;}

        public decimal ReserveFundAmount { get; set; }

        public decimal DividendPercentage { get; set; }

        public decimal DividendAmount { get; set; }

        public decimal ElectionFundPercentage { get; set; }

        public decimal ElectionFundAmount { get; set; }

        public decimal EducationFundPercentage { get; set; }

        public decimal EducationFundAmount { get; set; }

        public decimal InvestmentFluctuationsFundPercentage { get; set; }

        public decimal InvestmentFluctuationsFundAmount { get; set; }

        public decimal TechnologicalDevelopmentFundPercentage { get; set; }

        public decimal TechnologicalDevelopmentFundAmount { get; set; }

        public decimal BuildingFundPercentage { get; set; }

        public decimal BuildingFundAmount { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual MeetingAgenda MeetingAgenda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NetProfitAppropriationMakerChecker> NetProfitAppropriationMakerCheckers { get; set; }
    }
}
