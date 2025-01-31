using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Account.GL;

namespace DemoProject.Services.Abstract.Account.GL
{
    public interface IGeneralLedgerRepository
    {
        //List<SelectListItem> GLParentDropdownList { get; }

        //List<SelectListItem> GetBusinessOfficeGeneralLedgerDropdownList { get; }

        // Amend GeneralLedger Delete Entry - If Entry Rejected
        Task<bool> Amend(GeneralLedgerViewModel _generalLedgerViewModel);

        Task<bool> GetSessionValues(short _generalLedgerPrmKey, string _entryType);

        Task<GeneralLedgerViewModel> GetGeneralLedgerEntry(Guid _generalLedgerId, string _entryType);

        Task<IEnumerable<GeneralLedgerIndexViewModel>> GetGeneralLedgerIndex(string _entryType);

        List<SelectListItem> GLParentDropdownList { get; }

        short GetCashGeneralLedgerPrmKey();

        // Get Cash General Ledger PrmKey
        //short GetCashGeneralLedgerPrmKey();

        // Return Rejected Entries
        //Task<IEnumerable<GeneralLedgerViewModel>> GetIndexOfRejectedEntries();

        //// Return Valid List From GeneralLedger Table Which Are Not Authorized
        //Task<IEnumerable<GeneralLedgerViewModel>> GetIndexOfUnVerifiedEntries();

        //// Return Valid List From GeneralLedger Table For Modification
        //Task<IEnumerable<GeneralLedgerViewModel>> GetIndexOfVerifiedEntries();

        short GetPrmKeyById(Guid _GeneralLedgerId);

        //List<SelectListItem> GeneralLedgerDropdownList { get; }

        // Return Rejected Entry
        //Task<GeneralLedgerViewModel> GetRejectedEntry(Guid _generalLedgerId);

        //bool GetUniqueGLName(string _nameOfGL);

        //// Return Record From GeneralLedger Table By Given Parameter (i.e. GeneralLedgerId)
        //Task<GeneralLedgerViewModel> GetUnVerifiedEntry(Guid _generalLedgerId);

        //// Return Record From GeneralLedger Table By Given Parameter (i.e. GeneralLedgerId)
        //Task<GeneralLedgerViewModel> GetVerifiedEntry(Guid _generalLedgerId);

        // Save GeneralLedger Modification New Entry
        Task<bool> Modify(GeneralLedgerViewModel _generalLedgerViewModel);

        // Reject GeneralLedger Entry
        Task<bool> VerifyRejectDelete(GeneralLedgerViewModel _generalLedgerViewModel, string _entryType);

        // Save GeneralLedger New Entry
        Task<bool> Save(GeneralLedgerViewModel _generalLedgerViewModel);
    }
}
