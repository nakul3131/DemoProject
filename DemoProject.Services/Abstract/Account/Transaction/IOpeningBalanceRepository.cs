using DemoProject.Services.ViewModel.Account.Transaction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.Abstract.Account.Transaction
{
    public interface IOpeningBalanceRepository
    {
        Task<bool> GetOpeningBalanceValues(short _generalLedgerId, string _entryType);

        Task<byte> GetSchemeType(short _generalLedgerId);

        Task<string> GetDepositType(short _generalLedgerId);

        Task<OpeningBalanceViewModel> GetOpeningBalanceValue(short _generalLedgerid, long _personId);

        Task<IEnumerable<OpeningBalanceViewModel>> GetOpeningBalance(short _generalLedgerId, string _entryStatus);

        Task<bool> Amend(OpeningBalanceViewModel _openingBalanceViewModel);

        Task<bool> Delete(OpeningBalanceViewModel _openingBalanceViewModel);

        Task<bool> Save(OpeningBalanceViewModel _openingBalanceViewModel);

        Task<bool> Modify(OpeningBalanceViewModel _openingBalanceViewModel);

        Task<bool> Verify(OpeningBalanceViewModel _openingBalanceViewModel);

        Task<bool> Reject(OpeningBalanceViewModel _openingBalanceViewModel);

        List<SelectListItem> GeneralLedgerDropdownList { get; }

        List<SelectListItem> CustomerAccountDropdownList(Guid _generalLedgerId);

        short GetGeneralLedgerPrmKeyById(Guid _generalLedgerId);

        long GetPersonPrmKeyById(Guid _personId);
    }
}
