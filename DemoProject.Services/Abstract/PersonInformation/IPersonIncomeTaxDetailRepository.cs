using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonIncomeTaxDetailRepository
    {

        // Amend PersonIncomeTaxDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel);

        
        // Save PersonIncomeTaxDetail Modification New Entry
        Task<bool> Modify(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel);

        // Authorize PersonIncomeTaxDetail Entry
        Task<bool> VerifyRejectDelete(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel,string _entryType);


        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonIncomeTaxDetailViewModel>> GetEntries(long _personPrmKey,string _entryType);
        
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);



    }
}
