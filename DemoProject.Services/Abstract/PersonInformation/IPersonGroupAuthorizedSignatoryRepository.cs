using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;
//Created By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonGroupAuthorizedSignatoryRepository
    {
        // Amend PersonBankDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonGroupAuthorizedSignatoryViewModel _personGroupAuthorizedSignatoryViewModel);

        // Save PersonBankDetail Modification New Entry
        Task<bool> Modify(PersonGroupAuthorizedSignatoryViewModel _personGroupAuthorizedSignatoryViewModel);

        // Authorize PersonBankDetail Entry
        Task<bool> VerifyRejectDelete(PersonGroupAuthorizedSignatoryViewModel _personGroupAuthorizedSignatoryViewModel, string _entryType);

        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get  Entries By PersonPrmKey
        Task<IEnumerable<PersonGroupAuthorizedSignatoryViewModel>> GetEntries(long _personPrmKey,string _entryType);
        
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);


    }
}
