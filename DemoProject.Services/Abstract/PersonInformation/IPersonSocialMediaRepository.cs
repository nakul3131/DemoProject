using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonSocialMediaRepository
    {
        // Amend PersonSocialMedia Entry
        Task<bool> Amend(PersonSocialMediaViewModel _personSocialMediaViewModel);

        
        Task<bool> Modify(PersonSocialMediaViewModel _personSocialMediaViewModel);

        // Authorize PersonSocialMedia Entry
        Task<bool> VerifyRejectDelete(PersonSocialMediaViewModel _personSocialMediaViewModel ,string _entryType);


        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonSocialMediaViewModel>> GetEntries(long _personPrmKey, string _entryType);
       
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);

    }
}