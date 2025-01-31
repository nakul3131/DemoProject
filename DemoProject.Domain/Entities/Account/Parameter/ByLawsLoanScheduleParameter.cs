using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Parameter
{
    [Table("ByLawsLoanScheduleParameter")]
    public partial class ByLawsLoanScheduleParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ByLawsLoanScheduleParameter()
        {
            ByLawsLoanScheduleParameterMakerCheckers = new HashSet<ByLawsLoanScheduleParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte SequenceNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string ScheduleTitle { get; set; }

        public decimal MinimumWorkingCapital { get; set; }

        public decimal MaximumWorkingCapital { get; set; }

        public byte LoanTypePrmKey { get; set; }

        public byte MinimumTenure { get; set; }

        public byte MaximumTenure { get; set; }

        public bool EnableTenureAlteration { get; set; }

        public decimal MinimumSanctionLoanAmountLimit { get; set; }

        public decimal MaximumSanctionLoanAmountLimit { get; set; }
        
        public decimal CollateralMultipleOfLoanAmount { get; set; }

        public decimal MarginPercentage { get; set; }

        [Required]
        [StringLength(1)]
        public string SanctionLoanAmountLimitPriority { get; set; }

        public decimal MaximumLoanAmountLimitForIndividual { get; set; }

        public decimal MinimumLoanAmountLimitForIndividual { get; set; }

        public decimal SelfFundPercentageForMaximumLoanAmountLimitOfIndividuals { get; set; }

        public decimal MinimumLoanAmountLimitForGroup { get; set; }

        public decimal MaximumLoanAmountLimitForGroup { get; set; }

        public decimal SelfFundPercentageForMaximumLoanAmountLimitOfGroup { get; set; }

        [Required]
        [StringLength(1)]
        public string MaximumLoanAmountLimitPriority { get; set; }

        public bool RequireAClass { get; set; }

        public bool RequireBClass { get; set; }

        public bool RequireCClass { get; set; }

        public bool RequireDClass { get; set; }

        public decimal MinimumInterestRate { get; set; }

        public decimal MaximumInterestRate { get; set; }

        public decimal MinimumFineInterestRate { get; set; }

        public decimal MaximumFineInterestRate { get; set; }

        public decimal MaximumLoanTypePercentageOfTotalLoan { get; set; }

        public bool EnableApplication { get; set; }

        public bool EnablePassbook { get; set; }

        public bool EnableValuation { get; set; }

        public bool EnableInterestCapitalization { get; set; }

        public bool EnableOverDuesCalculationAfterMaturity { get; set; }

        public bool EnableNPACalculation { get; set; }

        public bool IsAllowCrossGuarantor { get; set; }

        public bool IsAllowDefaulterApplicant { get; set; }

        public bool IsAllowDefaulterGuarantor { get; set; }

        public bool EnableGuarantorAlteration { get; set; }

        public bool IsAllowNonMortgageLoanToBoardOfDirector { get; set; }

        public bool IsAllowToBoardOfDirectorsAsGuarantorForNonMortgageLoan { get; set; }

        public bool IsAllowToRenewLoan { get; set; }

        [Required]
        [StringLength(3)]
        public string LoanDisbursementMethod { get; set; }

        public byte MinimumNumberOfGuarantor { get; set; }

        public byte MaximumNumberOfGuarantor { get; set; }

        public byte NumberOfLoanGuarantorFromApplicantFamily { get; set; }

        public byte NumberOfLoanGuarantorFromSameFamily { get; set; }

        public byte MaximumGuarantorLimitForMember { get; set; }

        public byte LoanApplicationRejectionInformDays { get; set; }

        public byte LoanApplicationReconsiderationTimeAfterRejection { get; set; }

        public byte LoanCancellationTimeAfterSanction { get; set; }

        public decimal MaximumLoanPercentageOfTotalLoanForBoardOfDirectors { get; set; }

        public decimal MinimumSanctioningLimitForBranchManager { get; set; }

        public decimal MaximumSanctioningLimitForBranchManager { get; set; }

        public decimal MinimumSanctioningLimitForCommittee { get; set; }

        public decimal MaximumSanctioningLimitForCommittee { get; set; }

        public decimal MinimumSanctioningLimitForBoardOfDirector { get; set; }

        public decimal MaximumSanctioningLimitForBoardOfDirector { get; set; }

        public decimal MinimumSanctioningLimitForCEO { get; set; }

        public decimal MaximumSanctioningLimitForCEO { get; set; }

        public decimal MinimumSanctioningLimitForChairman { get; set; }

        public decimal MaximumSanctioningLimitForChairman { get; set; }


        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        //other

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ByLawsLoanScheduleParameterMakerChecker> ByLawsLoanScheduleParameterMakerCheckers { get; set; }
    }
}
