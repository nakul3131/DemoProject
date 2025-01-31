using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.Services.Abstract.Account.Parameter
{
    public interface ITransactionParameterRepository
    {
        List<SelectListItem> GetGLAndAccountNumber(Guid _PersonId);

        List<SelectListItem> GetAccountNumber(Guid _PersonId);

        int GetSchemeId(Guid _accountno);

        IEnumerable<string> GetAccountByhand();

        List<string> GetPersonAutoCompleteList(string _inputString);
        List<string> GetPersonAutoCompleteList();

        IEnumerable<string> GetAccountCustomerName(string _inputString);

        // Amend Address Parameter Delete Entry - If Entry Rejected
        Task<bool> Amend(TransactionParameterViewModel _addressParameterViewModel);

        // Delete Address Parameter - Only For Rejected Entry
        Task<bool> Delete(TransactionParameterViewModel _addressParameterViewModel);

        // Return Current Active Entry
        Task<TransactionParameterViewModel> GetActiveEntry();

        // Return Autherize Entry
        Task<IEnumerable<TransactionParameterViewModel>> GetTransactionParameterIndex();

        // Return Rejected Entry
        Task<TransactionParameterViewModel> GetRejectedEntry();

        // Return UnAuthorized Entry
        Task<TransactionParameterViewModel> GetUnVerifiedEntry();

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending();

        // Reject Address Parameter Entry
        Task<bool> Reject(TransactionParameterViewModel _addressParameterViewModel);

        // Save Address Parameter New Entry
        Task<bool> Save(TransactionParameterViewModel _addressParameterViewModel);

        // Authorize Address Parameter Entry
        Task<bool> Verify(TransactionParameterViewModel _addressParameterViewModel);
    }
}
