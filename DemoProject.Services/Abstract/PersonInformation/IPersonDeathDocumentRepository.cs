using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonDeathDocumentRepository
    {
        // Return Rejected PersonDeath Entries
        Task<IEnumerable<PersonDeathDocumentViewModel>> GetRejectedEntries(long _personPrmKey);

        // Return UnVerified PersonDeath Entries
        Task<IEnumerable<PersonDeathDocumentViewModel>> GetUnverifiedEntries(long _personPrmKey);

        // Return Verified PersonDeath Entries
        Task<IEnumerable<PersonDeathDocumentViewModel>> GetVerifiedEntries(long _personPrmKey);

    }
}
