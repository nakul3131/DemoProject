using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonAdditionalIncomeDetailRepository
    {
        // Amend PersonBankDetail Entry
        Task<bool> Amend(PersonAdditionalIncomeDetailViewModel _personAdditionalIncomeDetailViewModel);

        
        Task<bool> Modify(PersonAdditionalIncomeDetailViewModel _personAdditionalIncomeDetailViewModel);

        
        // Authorize PersonBankDetail Entry
        Task<bool> VerifyRejectDelete(PersonAdditionalIncomeDetailViewModel _personAdditionalIncomeDetailViewModel,string _entryType);


        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonAdditionalIncomeDetailViewModel>> GetEntries(long _personPrmKey,string _entryType);

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);

    }
}
