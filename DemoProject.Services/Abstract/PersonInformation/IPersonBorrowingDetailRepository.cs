using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonBorrowingDetailRepository
    {
        // Amend PersonBorrowingDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel);

        Task<bool> Modify(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel);

        // Authorize PersonBorrowingDetail Entry
        Task<bool> VerifyRejectDelete(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel , string _entryType);

        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonBorrowingDetailViewModel>> GetEntries(long _personPrmKey ,string _entryType);
       
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);


    }
}
