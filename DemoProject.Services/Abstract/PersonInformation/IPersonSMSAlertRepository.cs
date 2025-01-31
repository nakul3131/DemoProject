using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonSMSAlertRepository
    {
        // Amend PersonSMSAlert Entry
        Task<bool> Amend(PersonSMSAlertViewModel _personBankDetailViewModel);

        Task<bool> Modify(PersonSMSAlertViewModel _personBankDetailViewModel);

        // Authorize PersonSMSAlert Entry
        Task<bool> VerifyRejectDelete(PersonSMSAlertViewModel _personBankDetailViewModel ,string _entryType);

        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonSMSAlertViewModel>> GetEntries(long _personPrmKey , string _entryType);

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);


    }
}
