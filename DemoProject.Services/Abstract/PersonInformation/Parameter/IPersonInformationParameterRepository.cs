using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;

namespace DemoProject.Services.Abstract.PersonInformation.Parameter
{
    public interface IPersonInformationParameterRepository
    {
        // Amend Address Parameter Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonInformationParameterViewModel _personInformationParameterViewModel);

        // Delete Address Parameter - Only For Rejected Entry
        Task<bool> Delete(PersonInformationParameterViewModel _personInformationParameterViewModel);

        Task<bool> GetSessionValues(string _entryType);

        // Return PersonInformationParameter Entry
        Task<PersonInformationParameterViewModel> GetPersonInformationParameterEntry(string _entryType);

        Task<IEnumerable<PersonInformationParameterViewModel>> GetPersonInformationParameterIndex();

        Task<bool> IsAnyAuthorizationPending();

        // Reject Address Parameter Entry
        Task<bool> Reject(PersonInformationParameterViewModel _personInformationParameterViewModel);

        // Save Address Parameter New Entry
        Task<bool> Save(PersonInformationParameterViewModel _personInformationParameterViewModel);

        // Authorize Address Parameter Entry
        Task<bool> Verify(PersonInformationParameterViewModel _personInformationParameterViewModel);
    }
}