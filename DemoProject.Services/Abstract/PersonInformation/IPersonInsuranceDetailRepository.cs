using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonInsuranceDetailRepository
    {
        // Amend PersonInsuranceDetail Entry
        Task<bool> Amend(PersonInsuranceDetailViewModel _personInsuranceDetailViewModel);

        // Save PersonInsuranceDetail Modification New Entry
        Task<bool> Modify(PersonInsuranceDetailViewModel _personInsuranceDetailViewModel);

        // Authorize PersonInsuranceDetail Entry
        Task<bool> VerifyRejectDelete(PersonInsuranceDetailViewModel _personInsuranceDetailViewModel ,string _entryType);

        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonInsuranceDetailViewModel>> GetEntries(long _personPrmKey , string _entryType);

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);



    }
}
