using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;
//Modified By Dhanashri Wagh 19/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonContactDetailsRepository
    {
        // Amend PersonContactDetail Entry
        Task<bool> Amend(PersonContactDetailViewModel _personContactDetailViewModel);

        // Modify PersonContactDetail Entry
        Task<bool> Modify(PersonContactDetailViewModel _personContactDetailViewModel);

        // Verify Reject Delete PersonContactDetail Entry
        Task<bool> VerifyRejectDelete(PersonContactDetailViewModel _personContactDetailViewModel, string _entryType);

        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonContactDetailViewModel>> GetEntries(long _personPrmKey ,string _entryType);

       
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);
}
}
