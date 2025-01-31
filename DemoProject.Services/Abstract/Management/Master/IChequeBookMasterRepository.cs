using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IChequeBookMasterRepository
    {
        // Amend ChequeBookMaster Delete Entry - If Entry Rejected
        Task<bool> Amend(ChequeBookMasterViewModel _chequebookmasterViewModel);

        // Delete ChequeBookMaster - Only For Rejected Entry
        Task<bool> Delete(ChequeBookMasterViewModel _chequebookmasterViewModel);

        // Return Rejected Entries
        Task<IEnumerable<ChequeBookMasterViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From ChequeBookMaster Table Which Are Not Authorized
        Task<IEnumerable<ChequeBookMasterViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From ChequeBookMaster Table For Modification
        Task<IEnumerable<ChequeBookMasterViewModel>> GetIndexOfVerifiedEntries();

        //Return Rejected Entry
       Task<ChequeBookMasterViewModel> GetRejectedEntry(Guid _ChequeBookMasterId);

        //bool GetUniqueDesignationName(string _nameOfDesignation);

        // Return Record From ChequeBookmaster Table By Given Parameter (i.e. ChequeBookMasterId)
        Task<ChequeBookMasterViewModel> GetUnVerifiedEntry(Guid _chequebookmasterId);

        // Return Record From ChequeBookMaster Table By Given Parameter (i.e. ChequeBookMasterId)
        Task<ChequeBookMasterViewModel> GetVerifiedEntry(Guid _chequebookmasterId);

        // Reject ChequeBookMaster Entry
        Task<bool> Reject(ChequeBookMasterViewModel _chequebookmasterViewModel);

        // Save ChequeBookMaster New Entry
        Task<bool> Save(ChequeBookMasterViewModel _chequebookmasterViewModel);

        // Authorize ChequeBookMaster Entry
        Task<bool> Verify(ChequeBookMasterViewModel _chequebookmasterViewModel);
    }
}
