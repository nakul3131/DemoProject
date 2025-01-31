using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;
//Created By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonGroupMasterRepository
    {
        // Amend PersonBankDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonGroupMasterViewModel _personGroupMasterViewModel);

        // Save PersonBankDetail Modification New Entry
        Task<bool> Modify(PersonGroupMasterViewModel _personGroupMasterViewModel);

        // Authorize PersonBankDetail Entry
        Task<bool> VerifyRejectDelete(PersonGroupMasterViewModel _personGroupMasterViewModel, string _entryType);

        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get  Entries By PersonPrmKey
        Task<IEnumerable<PersonGroupAuthorizedSignatoryViewModel>> GetEntries(long _personPrmKey,string _entryType);
        
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);

        //Return PersonGroupMaster Entry
        Task<PersonGroupMasterViewModel> GetPersonGroupMasterEntry(long _personGroupPrmKey, string _entryType);

        //Return PersonGroupMaster Session Value
        Task<bool> GetPersonGroupMasterSessionValues(PersonGroupMasterViewModel _personGroupMasterViewModel, string _entryType);
    }
}
