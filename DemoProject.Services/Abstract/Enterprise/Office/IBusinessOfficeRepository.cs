using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Enterprise.Office;

namespace DemoProject.Services.Abstract.Enterprise.Office
{
    public interface IBusinessOfficeRepository
    {
        // Amend BusinessOffice Delete Entry - If Entry Rejected
        Task<bool> Amend(BusinessOfficeViewModel _businessOfficeViewModel);

        bool GetUniqueBusinessOfficeName(string _nameOfBusinessOffice);

        Task<bool> GetSessionValues(short _businessOfficePrmKey, string _entryType);

        int GetCountOfBusinessOffice();

        int GetCountOfAppConfig();

        Task<BusinessOfficeViewModel> GetBusinessOfficeEntry(Guid _businessOfficeId, string _entryType);

        Task<IEnumerable<BusinessOfficeIndexViewModel>> GetBusinessOfficeIndex(string _entryType);

        // Save BusinessOffice New Entry
        Task<bool> Save(BusinessOfficeViewModel _businessOfficeViewModel);

        // Save BusinessOffice Modification New Entry
        Task<bool> Modify(BusinessOfficeViewModel _businessOfficeViewModel);

        // Reject BusinessOffice Entry
        Task<bool> VerifyRejectDelete(BusinessOfficeViewModel _businessOfficeViewModel, string _entryType);

    }
}