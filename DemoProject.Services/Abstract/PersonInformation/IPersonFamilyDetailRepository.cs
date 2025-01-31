using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonFamilyDetailRepository
    {
        // Amend PersonFamilyDetail Entry
        Task<bool> Amend(PersonFamilyDetailViewModel _personFamilyDetailViewModel);

        Task<bool> Modify(PersonFamilyDetailViewModel _personFamilyDetailViewModel);
        
        // Authorize PersonFamilyDetail Entry
        Task<bool> VerifyRejectDelete(PersonFamilyDetailViewModel _personFamilyDetailViewModel ,string _entryType);


        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonFamilyDetailViewModel>> GetEntries(long _personPrmKey,string _entryType);

         // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);




    }
}
