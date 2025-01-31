using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;

//Modified By Dhanashri Wagh 19/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonCourtCaseRepository
    {
        // Amend PersonCourtCase Entry
        Task<bool> Amend(PersonCourtCaseViewModel _personCourtCaseViewModel);


        Task<bool> Modify(PersonCourtCaseViewModel _personCourtCaseViewModel);

        // Authorize PersonCourtCase Entry
        Task<bool> VerifyRejectDelete(PersonCourtCaseViewModel _personCourtCaseViewModel, string _entryType);

        //Get  Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonCourtCaseViewModel>> GetEntries(long _personPrmKey , string _entryType);

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);

    }
}