using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;

//Modified By Dhanashri Wagh 19/09/20224
namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonChronicDiseaseRepository
    {
        // Amend PersonChronicDisease Entry
        Task<bool> Amend(PersonChronicDiseaseViewModel _personChronicDiseaseViewModel);
        
        Task<bool> Modify(PersonChronicDiseaseViewModel _personChronicDiseaseViewModel);
       
        
        // Authorize PersonAddress Entry
        Task<bool> VerifyRejectDelete(PersonChronicDiseaseViewModel _personChronicDiseaseViewModel, string _entryType);

        //Get  Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonChronicDiseaseViewModel>> GetEntries(long _personPrmKey, string _entryType);
        
         // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);

    }
}
