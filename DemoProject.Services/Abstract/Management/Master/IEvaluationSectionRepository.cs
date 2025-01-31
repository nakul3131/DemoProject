using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IEvaluationSectionRepository
    {
        // Amend EvaluationSection Delete Entry - If Entry Rejected
        Task<bool> Amend(EvaluationSectionViewModel _contentItemViewModel);

        // Delete EvaluationSection - Only For Rejected Entry
        Task<bool> Delete(EvaluationSectionViewModel _contentItemViewModel);

        List<SelectListItem> EvaluationSectionDropdownList { get; }

        // Return Rejected Entries
        Task<IEnumerable<EvaluationSectionViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From EvaluationSection Table Which Are Not Authorized
        Task<IEnumerable<EvaluationSectionViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From EvaluationSection Table For Modification
        Task<IEnumerable<EvaluationSectionViewModel>> GetIndexOfVerifiedEntries();

        short GetPrmKeyById(Guid _EvaluationSectionId);

        // Return Rejected Entry
        Task<EvaluationSectionViewModel> GetRejectedEntry(Guid _contentItemId);

        bool GetUniqueEvaluationSectionName(string _nameOfEvaluationSection);

        // Return Record From EvaluationSection Table By Given Parameter (i.e. EvaluationSectionId)
        Task<EvaluationSectionViewModel> GetUnVerifiedEntry(Guid _contentItemId);

        // Return Record From EvaluationSection Table By Given Parameter (i.e. EvaluationSectionId)
        Task<EvaluationSectionViewModel> GetVerifiedEntry(Guid _contentItemId);

        // Save EvaluationSection Modification New Entry
        Task<bool> Modify(EvaluationSectionViewModel _contentItemViewModel);

        // Reject EvaluationSection Entry
        Task<bool> Reject(EvaluationSectionViewModel _contentItemViewModel);

        // Save EvaluationSection New Entry
        Task<bool> Save(EvaluationSectionViewModel _contentItemViewModel);

        // Authorize EvaluationSection Entry
        Task<bool> Verify(EvaluationSectionViewModel _contentItemViewModel);

    }
}
