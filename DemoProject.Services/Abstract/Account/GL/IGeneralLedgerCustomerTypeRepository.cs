using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.GL;

namespace DemoProject.Services.Abstract.Account.GL
{
    public interface IGeneralLedgerCustomerTypeRepository
    {
        // Return General Ledger Business Office Rejected Entry
        Task<IEnumerable<GeneralLedgerCustomerTypeViewModel>> GetRejectedGeneralLedgerCustomerTypeEntries(short _generalLedgerPrmKey);

        // Return Unverified Entries of Customer Category
        Task<IEnumerable<GeneralLedgerCustomerTypeViewModel>> GetUnverifiedGeneralLedgerCustomerTypeEntries(short _generalLedgerPrmKey);

        // Return Valid List From General Ledger Customer Category Table For Modification
        Task<IEnumerable<GeneralLedgerCustomerTypeViewModel>> GetVerifiedGeneralLedgerCustomerTypeEntries(short _generalLedgerPrmKey);
    }
}
