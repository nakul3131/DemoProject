using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Account.Master;

namespace DemoProject.Services.Abstract.Account.Master
{
    public interface IBeneficiaryRepository
    {
        // Amend BeneficiaryDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(BeneficiaryDetailViewModel _beneficiaryDetailViewModel);

        // Delete BeneficiaryDetail - Only For Rejected Entry
        Task<bool> Delete(BeneficiaryDetailViewModel _beneficiaryDetailViewModel);

        List<SelectListItem> CustomerAccountTypeDropdownList { get; }

        short GetPrmKeyById(Guid _customerAccountTypeId); 

        // Return Rejected Entries
        Task<IEnumerable<BeneficiaryDetailViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From BeneficiaryDetail Table Which Are Not Authorized
        Task<IEnumerable<BeneficiaryDetailViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From BeneficiaryDetail Table For Modification
        Task<IEnumerable<BeneficiaryDetailViewModel>> GetIndexOfVerifiedEntries();
        
        // Return Rejected Entry
        Task<BeneficiaryDetailViewModel> GetRejectedEntry(Guid _beneficiaryDetailId);
        
        // Return Record From BeneficiaryDetail Table By Given Parameter (i.e. BeneficiaryDetailId)
        Task<BeneficiaryDetailViewModel> GetUnVerifiedEntry(Guid _beneficiaryDetailId);

        // Return Record From BeneficiaryDetail Table By Given Parameter (i.e. BeneficiaryDetailId)
        Task<BeneficiaryDetailViewModel> GetVerifiedEntry(Guid _beneficiaryDetailId);
        
        // Reject BeneficiaryDetail Entry
        Task<bool> Reject(BeneficiaryDetailViewModel _beneficiaryDetailViewModel);

        // Save BeneficiaryDetail New Entry
        Task<bool> Save(BeneficiaryDetailViewModel _beneficiaryDetailViewModel);

        // Authorize BeneficiaryDetail Entry
        Task<bool> Verify(BeneficiaryDetailViewModel _beneficiaryDetailViewModel);
    }
}
