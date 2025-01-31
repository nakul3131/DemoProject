using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;

namespace DemoProject.Services.Abstract.PersonInformation.Parameter
{
    public interface IPersonInformationParameterNoticeTypeRepository
    {
        // Amend PersonInformationParameterNoticeType Entry
        Task<bool> Amend(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel);

        // Delete PersonInformationParameterNoticeType
        Task<bool> Delete(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel);

        Task<bool> GetSessionValues(string _entryType);

        Task<PersonInformationParameterNoticeTypeViewModel> GetPersonInformationParameterNoticeTypeEntry(string _entryType);

        Task<IEnumerable<PersonInformationParameterNoticeTypeViewModel>> GetPersonInformationParameterNoticeTypeIndex();

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending();

        // Reject PersonInformationParameterNoticeType Entry
        Task<bool> Reject(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel);

        // Save PersonInformationParameterNoticeType New Entry
        Task<bool> Save(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel);

        // Authorize PersonInformationParameterNoticeType Entry
        Task<bool> Verify(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel);
    }
}