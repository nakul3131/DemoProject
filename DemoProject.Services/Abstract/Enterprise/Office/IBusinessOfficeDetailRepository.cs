using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Enterprise.Office;

namespace DemoProject.Services.Abstract.Enterprise.Office
{
    public interface IBusinessOfficeDetailRepository
    {
        // Amend BusinessOfficeDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel);

        // Delete BusinessOfficeDetail - Only For Rejected Entry
        Task<bool> Delete(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel);

        // Save BusinessOfficeDetail Modification New Entry
        Task<bool> Modify(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel);

        // Reject BusinessOfficeDetail Entry
        Task<bool> Reject(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel);

        // Save BusinessOfficeDetail New Entry
        Task<bool> Save(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel);

        // Authorize BusinessOfficeDetail Entry
        Task<bool> Verify(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel);
    }
}
