using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Account.Parameter
{


    public class ByLawsLoanScheduleParameterViewModel
    {

        private readonly IAccountDetailRepository accountDetailRepository;

        public ByLawsLoanScheduleParameterViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();

        }
        //ByLawsLoanScheduleParameter
        public short PrmKey { get; set; }

        public byte LoanTypePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte SequenceNumber { get; set; }

        [StringLength(50)]
        public string ScheduleTitle { get; set; }

        public decimal MinimumWorkingCapital { get; set; }

        public decimal MaximumWorkingCapital { get; set; }

        public byte MinimumTenure { get; set; }

        public byte MaximumTenure { get; set; }

        public bool EnableTenureAlteration { get; set; }

        public decimal MinimumSanctionLoanAmountLimit { get; set; }

        public decimal MaximumSanctionLoanAmountLimit { get; set; }

        //public decimal SanctionLoanAmountLimit { get; set; }

        public decimal CollateralMultipleOfLoanAmount { get; set; }

        public decimal MarginPercentage { get; set; }

        [StringLength(1)]
        public string SanctionLoanAmountLimitPriority { get; set; }

        public decimal MinimumLoanAmountLimitForIndividual { get; set; }

        public decimal MaximumLoanAmountLimitForIndividual { get; set; }

        public decimal SelfFundPercentageForMaximumLoanAmountLimitOfIndividuals { get; set; }

        public decimal MinimumLoanAmountLimitForGroup { get; set; }

        public decimal MaximumLoanAmountLimitForGroup { get; set; }

        public decimal SelfFundPercentageForMaximumLoanAmountLimitOfGroup { get; set; }

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


        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }


        //ByLawsLoanScheduleParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short ByLawsLoanScheduleParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public IEnumerable<OrganizationLoanTypeViewModel> OrganizationLoanTypeViewModel { get; set; }

        // List<SelectListItem> For Dropdownlist
        public List<SelectListItem> AccountClassDropdownList
        {
            get
            {
                return accountDetailRepository.AccountClassDropdownList;
            }
        }
    }
}
