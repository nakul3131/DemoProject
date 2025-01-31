using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonGSTRegistrationDetailRepository
    {
        // Amend PersonGSTRegistrationDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonGSTRegistrationDetailViewModel _personGSTRegistrationDetailViewModel);

         // Save PersonGSTRegistrationDetail Modification New Entry
        Task<bool> Modify(PersonGSTRegistrationDetailViewModel _personGSTRegistrationDetailViewModel);

        // Authorize PersonGSTRegistrationDetail Entry
        Task<bool> VerifyRejectDelete(PersonGSTRegistrationDetailViewModel _personGSTRegistrationDetailViewModel, string _entryType);
       
        //Get  Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Verified Entries By PersonPrmKey
        Task<IEnumerable<PersonGSTRegistrationDetailViewModel>> GetVerifiedEntries(long _personPrmKey);
        
        //Get UnVerified Entries By PersonPrmKey
        Task<IEnumerable<PersonGSTRegistrationDetailViewModel>> GetUnVerifiedEntries(long _personPrmKey);
        
        //Get Rejected Entries By PersonPrmKey
        Task<IEnumerable<PersonGSTRegistrationDetailViewModel>> GetRejectedEntries(long _personPrmKey);

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);

    }
}
