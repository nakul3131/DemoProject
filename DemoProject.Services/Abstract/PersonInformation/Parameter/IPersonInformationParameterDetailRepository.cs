using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;

namespace DemoProject.Services.Abstract.PersonInformation.Parameter
{
    public interface IPersonInformationParameterDetailRepository
    {
        PersonInformationParameterViewModel GetDocumentValidations();

        // Return Autherize Entry
        Task<IEnumerable<PersonInformationParameterViewModel>> GetPersonInformationParameterIndex();

        // Return PersonInformationParameter Entry
        Task<PersonInformationParameterViewModel> GetPersonInformationParameterEntry(string _entryType);

        //PersonInformationParameterDocumentType
        // Return PersonInformationParameterDocumentTypes Entries
        Task<IEnumerable<PersonInformationParameterDocumentTypeViewModel>> GetPersonInformationParameterDocumentTypeEntries(string _entryType);

        // Return PersonInformationParameterDocumentTypes Entry
        Task<PersonInformationParameterDocumentTypeViewModel> GetPersonInformationParameterDocumentTypeEntry(string _entryType);

        //PersonInformationParameterNoticeType
        Task<IEnumerable<PersonInformationParameterNoticeTypeViewModel>> GetPersonInformationParameterNoticeTypeEntries(string _entryType);

        // Return PersonInformationParameterNoticeTypeViewModel (Used For Reject View)
        Task<PersonInformationParameterNoticeTypeViewModel> GetPersonInformationParameterNoticeTypeEntry(string _entryType);

        //  Set Default Value Of PersonInformationParameter Full Page
        void GetPersonInformationParameterAllDefaultValues(PersonInformationParameterViewModel _personInformationParameterViewModel, string _entryType);

        //  Set Default Value Of Data Table
        void GetPersonInformationParameterDocumentTypeDefaultValues(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel, string _entryType, byte _personInformationParameterPrmKey);

        //  Set Default Value Of Data Table
        void GetPersonInformationParameterNoticeDefaultValues(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel, string _entryType, byte _personInformationParameterPrmKey);

        // Get SMSAlert Setting
        bool IsEnableSMSAlert();

        // Is Required KYC Document Upload
        bool IsRequiredKYCDocumentUpload();
    }
}
