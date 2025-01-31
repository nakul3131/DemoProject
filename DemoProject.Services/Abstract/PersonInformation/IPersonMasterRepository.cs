using DemoProject.Services.ViewModel.PersonInformation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonMasterRepository
    {
        Task<bool> Amend(PersonMasterViewModel _personMastViewModel);

        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        bool GetPersonInfoNumber(long personInformationNumber);

        byte GetPrmKeyById(Guid _prefixId);

        long GetPersonPrmKeyById(Guid _personId);

        long GetPersonGroupPrmKeyByPersonPrmKey(long _personPrmKey);

        Task<bool> Modify(PersonMasterViewModel _personMasterViewModel);

        //List<SelectListItem> PrefixDropdownList { get; }

        Task<bool> VerifyRejectDelete(PersonMasterViewModel _personMasterViewModel, string _entryType);
        
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);
    }
}
