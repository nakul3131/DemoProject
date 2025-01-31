using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonBoardOfDirectorRelationRepository
    {
        // Amend PersonBankDetail Entry
        Task<bool> Amend(PersonBoardOfDirectorRelationViewModel _personBankDetailViewModel);

        // Modify PersonBankDetail Entry
        Task<bool> Modify(PersonBoardOfDirectorRelationViewModel _personBankDetailViewModel);

        // Authorize PersonAddress Entry
        Task<bool> VerifyRejectDelete(PersonBoardOfDirectorRelationViewModel _PersonBoardOfDirectorRelationViewModel, string _entryType);

        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonBoardOfDirectorRelationViewModel>> GetEntries(long _personPrmKey, string _entryType);

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);

    }
}
