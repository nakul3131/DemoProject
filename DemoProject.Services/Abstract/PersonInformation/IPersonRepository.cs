using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonRepository
    {
        Task<bool> Amend(PersonViewModel _personViewModel);

        Task<bool> GetSessionValues(PersonViewModel _personViewModel, string _entryType);

        Task<bool> GetPersonMasterSessionValues(PersonMasterViewModel _personMasterViewModel, string _entryType);


        long GetPrmKeyById(Guid _personId);

        //Return Rejected Entries
        Task<IEnumerable<PersonIndexViewModel>> GetPersonIndex(string _entryType);

        //Return Person Entry
        Task<PersonViewModel> GetPersonEntry(Guid _personId, string _entryType);

        //Return PersonMaster Entry
        Task<PersonMasterViewModel> GetPersonMasterEntry(Guid _personId, string _entryType);

        // Save Person New Entry
        Task<bool> Save(PersonViewModel _personViewModel);

        // Authorize Designation Entry
        Task<bool> VerifyRejectDelete(PersonViewModel _personViewModel, string _entryType);
    }
}
