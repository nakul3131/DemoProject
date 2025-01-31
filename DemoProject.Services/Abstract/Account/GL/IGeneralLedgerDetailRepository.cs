using DemoProject.Services.ViewModel.Account.GL;
using DemoProject.Services.ViewModel.Account.Layout;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.Abstract.Account.GL
{
    public interface IGeneralLedgerDetailRepository
    {
        // Get Cash General Ledger PrmKey
        //short GetCashGeneralLedgerPrmKey();

        //short GetGeneralLedgerPrmKeyById(Guid _GeneralLedgerId);

        short GetBusinessOfficePrmKeyById(Guid _businessOfficeId);

        bool GetUniqueGLName(string _nameOfGL);

        int GetNumberOfGeneralLegerLimit(Guid _accountClassId);

        int GetCountOfGeneralLedger();

        Task<IEnumerable<GeneralLedgerBusinessOfficeViewModel>> GetGeneralLedgerBusinessOfficeEntries(short _generalLedgerPrmKey, string _entryType);

        Task<IEnumerable<GeneralLedgerCurrencyViewModel>> GetGeneralLedgerCurrencyEntries(short _generalLedgerPrmKey, string _entryType);

        Task<IEnumerable<SchemeGeneralLedgerViewModel>> GetGeneralLedgerSchemeEntries(short _generalLedgerPrmKey, string _entryType);

        Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetGeneralLedgerTransactionTypeEntries(short _generalLedgerPrmKey, string _entryType);

        Task<IEnumerable<GeneralLedgerCustomerTypeViewModel>> GetGeneralLedgerCustomerTypeEntries(short _generalLedgerPrmKey, string _entryType);

        List<SelectListItem> GLParentDropdownList { get; }

        List<SelectListItem> GetBusinessOfficeGeneralLedgerDropdownList { get; }

        List<SelectListItem> GeneralLedgerDropdownList { get; }

        //Set Default Value For General Ledger 
        void GetGeneralLedgerAllDefaultValues(GeneralLedgerViewModel _generalLedgerViewModel, string _entryType);

        void GetGeneralLedgerBusinessOfficeDefaultValues(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel, string _entryType, short _generalLedgerPrmKey);

        void GetGeneralLedgerCurrencyDefaultValues(GeneralLedgerCurrencyViewModel _generalLedgerCurrencyViewModel, string _entryType, short _generalLedgerPrmKey);

        void GetGeneralLedgerSchemeDefaultValues(SchemeGeneralLedgerViewModel _generalLedgerSchemeViewModel, string _entryType, short _generalLedgerPrmKey);

        void GetGeneralLedgerTransactionTypeDefaultValues(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel, string _entryType, short _generalLedgerPrmKey);

        void GetGeneralLedgerCustomerTypeDefaultValues(GeneralLedgerCustomerTypeViewModel _generalLedgerCustomerTypeViewModel, string _entryType, short _generalLedgerPrmKey);
    }
}
