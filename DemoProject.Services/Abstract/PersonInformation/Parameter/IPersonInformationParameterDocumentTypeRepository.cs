using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;

namespace DemoProject.Services.Abstract.PersonInformation.Parameter
{
    public interface IPersonInformationParameterDocumentTypeRepository
    {
        // Amend PersonInformationParameterDocumentType Entry
        Task<bool> Amend(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel);

        // Delete PersonInformationParameterDocumentType
        Task<bool> Delete(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel);


        Task<bool> GetSessionValues(string _entryType);

        // Return PersonInformationParameterDocumentType Entry
        Task<PersonInformationParameterDocumentTypeViewModel> GetPersonInformationParameterDocumentTypeEntry(string _entryType);

        Task<IEnumerable<PersonInformationParameterDocumentTypeViewModel>> GetPersonInformationParameterDocumentTypeIndex();
        
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending();

        // Reject PersonInformationParameterDocumentType Entry
        Task<bool> Reject(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel);

        // Save PersonInformationParameterDocumentType New Entry
        Task<bool> Save(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel);

        // Authorize PersonInformationParameterDocumentType Entry
        Task<bool> Verify(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel);
    }
}