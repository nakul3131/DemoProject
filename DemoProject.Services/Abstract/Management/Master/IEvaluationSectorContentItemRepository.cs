using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IEvaluationSectorContentItemRepository
    {
        // Amend EvaluationSectorContentItem Entry - If Entry Rejected
        Task<bool> Amend(EvaluationSectorContentItemViewModel _evaluationSectorContentItemViewModel);

        // Delete EvaluationSectorContentItem Entry - Only For Rejected Entry
        Task<bool> Delete(EvaluationSectorContentItemViewModel _evaluationSectorContentItemViewModel);

        // Return Rejected Entries
        Task<IEnumerable<EvaluationSectorContentItemViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From EvaluationSectorContentItem Table Which Are Not Authorized
        Task<IEnumerable<EvaluationSectorContentItemViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Empty EvaluationSectorContentItem Table 
        Task<IEnumerable<EvaluationSectorContentItemViewModel>> GetIndexWithCreateModifyOperationStatus();

        List<SelectListItem> EvaluationDropdownListForCreate(Guid _evaluationSectionId);

        List<SelectListItem> EvaluationDropdownListForEdit(Guid _evaluationSectionId, Guid _contentItemId);

        // Return Rejected EvaluationSectorContentItem Entries
        Task<IEnumerable<EvaluationSectorContentItemViewModel>> GetVerifiedEntries(short _evaluationSectionPrmKey);

        // Return Rejected EvaluationSectorContentItem Entries
        Task<IEnumerable<EvaluationSectorContentItemViewModel>> GetRejectedEntries(short _evaluationSectionPrmKey);

        // Return UnVerified EvaluationSectorContentItem Entries
        Task<IEnumerable<EvaluationSectorContentItemViewModel>> GetUnverifiedEntries(short _evaluationSectionPrmKey);

        // Return Empty EvaluationSectorContentItemViewModel (Used For Create)
        Task<EvaluationSectorContentItemViewModel> GetViewModelForCreate(short _evaluationSectionPrmKey);

        // Return EvaluationSectorContentItemViewModel (Used For Reject View)
        Task<EvaluationSectorContentItemViewModel> GetViewModelForReject(short _evaluationSectionPrmKey);

        // Return EvaluationSectorContentItemViewModel (Used For Unverified View)
        Task<EvaluationSectorContentItemViewModel> GetViewModelForUnverified(short _evaluationSectionPrmKey);

        // Reject EvaluationSectorContentItem Entry
        Task<bool> Reject(EvaluationSectorContentItemViewModel _evaluationSectorContentItemViewModel);

        // Save EvaluationSectorContentItem New Entry
        Task<bool> Save(EvaluationSectorContentItemViewModel _evaluationSectorContentItemViewModel);

        // Verify EvaluationSectorContentItem Entry
        Task<bool> Verify(EvaluationSectorContentItemViewModel _evaluationSectorContentItemViewModel);
    }
}
