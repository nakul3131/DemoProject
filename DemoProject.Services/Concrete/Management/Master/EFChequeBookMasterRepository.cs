using AutoMapper;
using DemoProject.Domain.Entities.Management.Master;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFChequeBookMasterRepository : IChequeBookMasterRepository
    {
        private readonly EFDbContext context;

        public EFChequeBookMasterRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(ChequeBookMasterViewModel _chequebookmasterViewModel)
        {
            try
            {
                // Set Default Value
                _chequebookmasterViewModel.EntryDateTime = DateTime.Now;
                _chequebookmasterViewModel.EntryStatus = StringLiteralValue.Amend;
                _chequebookmasterViewModel.Remark = "None";
                _chequebookmasterViewModel.UserAction = StringLiteralValue.Amend;
                _chequebookmasterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                // ChequeBookMaster
                ChequeBookMaster chequebookmasterForAmend = Mapper.Map<ChequeBookMaster>(_chequebookmasterViewModel);
                ChequeBookMasterMakerChecker chequebookmasterMakerCheckerForAmend = Mapper.Map<ChequeBookMasterMakerChecker>(_chequebookmasterViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                chequebookmasterForAmend.PrmKey = _chequebookmasterViewModel.ChequeBookMasterPrmKey;

                // ChequeBookMaster
                context.ChequeBookMasterMakerCheckers.Attach(chequebookmasterMakerCheckerForAmend);
                context.Entry(chequebookmasterMakerCheckerForAmend).State = EntityState.Added;
                chequebookmasterForAmend.ChequeBookMasterMakerCheckers.Add(chequebookmasterMakerCheckerForAmend);

                context.ChequeBookMasters.Attach(chequebookmasterForAmend);
                context.Entry(chequebookmasterForAmend).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(ChequeBookMasterViewModel _chequebookmasterViewModel)
        {
            try
            {
                // Set Default Value
                _chequebookmasterViewModel.EntryDateTime = DateTime.Now;
                _chequebookmasterViewModel.Remark = "None";
                _chequebookmasterViewModel.UserAction = StringLiteralValue.Delete;
                _chequebookmasterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                ChequeBookMasterMakerChecker chequebookmasterMakerChecker = Mapper.Map<ChequeBookMasterMakerChecker>(_chequebookmasterViewModel);

                // ChequeBookMaster
                context.ChequeBookMasterMakerCheckers.Attach(chequebookmasterMakerChecker);
                context.Entry(chequebookmasterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<ChequeBookMasterViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ChequeBookMasterViewModel>("SELECT * FROM dbo.GetChequeBookMasterEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ChequeBookMasterViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ChequeBookMasterViewModel>("SELECT * FROM dbo.GetChequeBookMasterEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ChequeBookMasterViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ChequeBookMasterViewModel>("SELECT * FROM dbo.GetChequeBookMasterEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ChequeBookMasterViewModel> GetRejectedEntry(Guid _chequebookmasterId)
        {
            try
            {
                return await context.Database.SqlQuery<ChequeBookMasterViewModel>("SELECT * FROM dbo.GetChequeBookMasterEntry (@ChequeBookMasterId, @EntriesType)", new SqlParameter("@ChequeBookMasterId", _chequebookmasterId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ChequeBookMasterViewModel> GetUnVerifiedEntry(Guid _chequebookmasterId)
        {
            try
            {
                return await context.Database.SqlQuery<ChequeBookMasterViewModel>("SELECT * FROM dbo.GetChequeBookMasterEntry (@ChequeBookMasterId, @EntriesType)", new SqlParameter("@ChequeBookMasterId", _chequebookmasterId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ChequeBookMasterViewModel> GetVerifiedEntry(Guid _chequebookmasterId)
        {
            try
            {
                return await context.Database.SqlQuery<ChequeBookMasterViewModel>("SELECT * FROM dbo.GetChequeBookMasterEntry (@ChequeBookMasterId, @EntriesType)", new SqlParameter("@ChequeBookMasterId", _chequebookmasterId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(ChequeBookMasterViewModel _chequebookmasterViewModel)
        {
            try
            {
                // Set Default Value
                _chequebookmasterViewModel.EntryDateTime = DateTime.Now;
                _chequebookmasterViewModel.UserAction = StringLiteralValue.Reject;
                _chequebookmasterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                ChequeBookMasterMakerChecker chequebookmasterMakerChecker = Mapper.Map<ChequeBookMasterMakerChecker>(_chequebookmasterViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                // DesignationMakerChecker
                context.ChequeBookMasterMakerCheckers.Attach(chequebookmasterMakerChecker);
                context.Entry(chequebookmasterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Save(ChequeBookMasterViewModel _chequebookmasterViewModel)
        {
            try
            {
                // Set Default Value
                _chequebookmasterViewModel.EntryDateTime = DateTime.Now;
                _chequebookmasterViewModel.EntryStatus = StringLiteralValue.Create;
                _chequebookmasterViewModel.Remark = "None";
                _chequebookmasterViewModel.UserAction = StringLiteralValue.Create;
                _chequebookmasterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // ChequeBookMaster
                _chequebookmasterViewModel.Status = "USD";
                ChequeBookMaster chequebookmaster = Mapper.Map<ChequeBookMaster>(_chequebookmasterViewModel);
                ChequeBookMasterMakerChecker chequebookmasterMakerChecker = Mapper.Map<ChequeBookMasterMakerChecker>(_chequebookmasterViewModel);

                context.ChequeBookMasterMakerCheckers.Attach(chequebookmasterMakerChecker);
                context.Entry(chequebookmasterMakerChecker).State = EntityState.Added;
                chequebookmaster.ChequeBookMasterMakerCheckers.Add(chequebookmasterMakerChecker);

                context.ChequeBookMasters.Attach(chequebookmaster);
                context.Entry(chequebookmaster).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(ChequeBookMasterViewModel _chequebookmasterViewModel)
        {
            try
            {
                // Set Default Value
                _chequebookmasterViewModel.EntryDateTime = DateTime.Now;
                _chequebookmasterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                _chequebookmasterViewModel.UserAction = StringLiteralValue.Verify;

                ChequeBookMasterMakerChecker chequebookmasterMakerChecker = Mapper.Map<ChequeBookMasterMakerChecker>(_chequebookmasterViewModel);

                // ChequeBookMasterMakerChecker
                context.ChequeBookMasterMakerCheckers.Attach(chequebookmasterMakerChecker);
                context.Entry(chequebookmasterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
    }
}
