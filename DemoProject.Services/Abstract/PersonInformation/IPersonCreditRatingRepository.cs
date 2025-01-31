using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonCreditRatingRepository
    {
        // Amend PersonCreditRating Entry
        Task<bool> Amend(PersonCreditRatingViewModel _personCreditRatingViewModel);

        Task<bool> Modify(PersonCreditRatingViewModel _personCreditRatingViewModel);
        
        // Authorize PersonCreditRating Entry
        Task<bool> VerifyRejectDelete(PersonCreditRatingViewModel _personCreditRatingViewModel ,string _entryType);

        //Get  Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonCreditRatingViewModel>> GetEntries(long _personPrmKey ,string _entryType);

       
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);


    }
}
