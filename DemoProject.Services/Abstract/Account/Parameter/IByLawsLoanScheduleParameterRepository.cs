using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.ViewModel.Enterprise.Establishment;


namespace DemoProject.Services.Abstract.Account.Parameter
{
    public interface IByLawsLoanScheduleParameterRepository
    {
        // Amend By Laws Loan Schedule Parameter Amend Entry - If Entry Rejected
        Task<bool> Amend(ByLawsLoanScheduleParameterViewModel _byLawsLoanScheduleParameterViewModel);

        // Return Current Active Entry
        Task<ByLawsLoanScheduleParameterViewModel> GetActiveEntry();
        Task<ByLawsLoanScheduleParameterViewModel> GetVerifiedEntry(Guid loanTypeId);

        ByLawsLoanScheduleParameterViewModel GetActiveEntry1();

        ByLawsLoanScheduleParameterViewModel ClearModelStateGetActiveEntry();

        // Return Autherize Entry
        Task<IEnumerable<OrganizationLoanTypeViewModel>> GetByLawsLoanScheduleParameterIndex();

        // Return Rejected Entry
        Task<ByLawsLoanScheduleParameterViewModel> GetRejectedEntry(byte _loanTypePrmKey);

        // Return UnAuthorized Entry
        Task<ByLawsLoanScheduleParameterViewModel> GetUnVerifiedEntry();
        Task<IEnumerable<ByLawsLoanScheduleParameterViewModel>> GetIndexOfModify();

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(byte loanTypePrmKey);

        // Save By Laws Loan Schedule Parameter Scheme Parameter New Entry
        Task<bool> Save(ByLawsLoanScheduleParameterViewModel _byLawsLoanScheduleParameterViewModel);

        // Authorize By Laws Loan Schedule Parameter Entry
        Task<bool> VerifyRejectDelete(ByLawsLoanScheduleParameterViewModel _byLawsLoanScheduleParameterViewModel, string _entryType);


    }
}
