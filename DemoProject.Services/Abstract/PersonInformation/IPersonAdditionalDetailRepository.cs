using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonAdditionalDetailRepository
    {
        int GetlistofOccupation(Guid OccupationId);

        string GetListOfMaritalStatus(Guid MaritalStatusId);

        Task<bool> Amend(PersonAdditionalDetailViewModel _personAdditionalDetailViewModel);
        
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);
        
        Task<PersonAdditionalDetailViewModel> GetViewModelForCreate(long _personPrmKey);

        Task<PersonAdditionalDetailViewModel> GetViewModelForReject(long _personPrmKey);

        Task<PersonAdditionalDetailViewModel> GetViewModelForUnverified(long _personPrmKey);

        Task<PersonAdditionalDetailViewModel> GetViewModelForVerified(long _personPrmKey);

        bool IsVIPCustomer(Guid _personId);

        Task<bool> Modify(PersonAdditionalDetailViewModel _personAdditionalDetailViewModel);

        Task<bool> VerifyRejectDelete(PersonAdditionalDetailViewModel _personAdditionalDetailViewModel, string _entryType);
    }
}
