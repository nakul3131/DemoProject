using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonBankDetailRepository
    {
        // Amend PersonBankDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonBankDetailViewModel _personBankDetailViewModel);

        // Save PersonBankDetail Modification New Entry
        Task<bool> Modify(PersonBankDetailViewModel _personBankDetailViewModel);

        // Authorize PersonBankDetail Entry
        Task<bool> VerifyRejectDelete(PersonBankDetailViewModel _personBankDetailViewModel, string _entryType);
        
        //Get Verified Entries By PersonPrmKey
        Task<IEnumerable<PersonBankDetailViewModel>> GetEntries(long _personPrmKey,string _entryType);
        
        //Get  Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);
        
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);


    }
}
