using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Domain.Entities.Management.Master;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFBoardOfDirectorPowerAndFunctionRepository : IBoardOfDirectorPowerAndFunctionRepository
    {
        private readonly EFDbContext context;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IPowerAndFunctionRepository powerAndFunctionRepository;

        public EFBoardOfDirectorPowerAndFunctionRepository(RepositoryConnection _connection, IManagementDetailRepository _managementDetailRepository, IPowerAndFunctionRepository _powerAndFunctionRepository)
        {
            context = _connection.EFDbContext;
            managementDetailRepository = _managementDetailRepository;
            powerAndFunctionRepository = _powerAndFunctionRepository;
        }

        public async Task<bool> Amend(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel)
        {
            try
            {
                // Set Default Value
                _boardOfDirectorPowerAndFunctionViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _boardOfDirectorPowerAndFunctionViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorPowerAndFunctionViewModel.EntryStatus = StringLiteralValue.Amend;
                _boardOfDirectorPowerAndFunctionViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _boardOfDirectorPowerAndFunctionViewModel.Remark = "None";
                _boardOfDirectorPowerAndFunctionViewModel.UserAction = StringLiteralValue.Amend;
                _boardOfDirectorPowerAndFunctionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _boardOfDirectorPowerAndFunctionViewModel.BoardOfDirectorPrmKey = managementDetailRepository.GetBoardOfDirectorPrmKeyById(_boardOfDirectorPowerAndFunctionViewModel.BoardOfDirectorId);
                _boardOfDirectorPowerAndFunctionViewModel.PowerAndFunctionPrmKey = powerAndFunctionRepository.GetPrmKeyById(_boardOfDirectorPowerAndFunctionViewModel.PowerAndFunctionId);

                BoardOfDirectorPowerAndFunction boardOfDirector = Mapper.Map<BoardOfDirectorPowerAndFunction>(_boardOfDirectorPowerAndFunctionViewModel);
                BoardOfDirectorPowerAndFunctionMakerChecker boardOfDirectorMakerChecker = Mapper.Map<BoardOfDirectorPowerAndFunctionMakerChecker>(_boardOfDirectorPowerAndFunctionViewModel);

                BoardOfDirectorPowerAndFunctionTranslation boardOfDirectorTranslation = Mapper.Map<BoardOfDirectorPowerAndFunctionTranslation>(_boardOfDirectorPowerAndFunctionViewModel);
                boardOfDirectorTranslation.PrmKey = _boardOfDirectorPowerAndFunctionViewModel.BoardOfDirectorPowerAndFunctionTranslationPrmKey;

                BoardOfDirectorPowerAndFunctionTranslationMakerChecker directorTranslationMakerChecker = Mapper.Map<BoardOfDirectorPowerAndFunctionTranslationMakerChecker>(_boardOfDirectorPowerAndFunctionViewModel);

                context.BoardOfDirectorPowerAndFunctionTranslationMakerCheckers.Attach(directorTranslationMakerChecker);
                context.Entry(directorTranslationMakerChecker).State = EntityState.Added;
                boardOfDirectorTranslation.BoardOfDirectorPowerAndFunctionTranslationMakerCheckers.Add(directorTranslationMakerChecker);

                context.BoardOfDirectorPowerAndFunctionTranslations.Attach(boardOfDirectorTranslation);
                context.Entry(boardOfDirectorTranslation).State = EntityState.Modified;

                context.BoardOfDirectorPowerAndFunctionMakerCheckers.Attach(boardOfDirectorMakerChecker);
                context.Entry(boardOfDirectorMakerChecker).State = EntityState.Added;
                boardOfDirector.BoardOfDirectorPowerAndFunctionMakerCheckers.Add(boardOfDirectorMakerChecker);

                context.BoardOfDirectorPowerAndFunctions.Attach(boardOfDirector);
                context.Entry(boardOfDirector).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel)
        {
            try
            {
                // Set Default Value
                _boardOfDirectorPowerAndFunctionViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorPowerAndFunctionViewModel.Remark = "None";
                _boardOfDirectorPowerAndFunctionViewModel.UserAction = StringLiteralValue.Delete;
                _boardOfDirectorPowerAndFunctionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BoardOfDirectorPowerAndFunctionMakerChecker boardOfDirectorMakerChecker = Mapper.Map<BoardOfDirectorPowerAndFunctionMakerChecker>(_boardOfDirectorPowerAndFunctionViewModel);

                BoardOfDirectorPowerAndFunctionTranslationMakerChecker directorTranslationMakerChecker = Mapper.Map<BoardOfDirectorPowerAndFunctionTranslationMakerChecker>(_boardOfDirectorPowerAndFunctionViewModel);

                context.BoardOfDirectorPowerAndFunctionTranslationMakerCheckers.Attach(directorTranslationMakerChecker);
                context.Entry(directorTranslationMakerChecker).State = EntityState.Added;

                context.BoardOfDirectorPowerAndFunctionMakerCheckers.Attach(boardOfDirectorMakerChecker);
                context.Entry(boardOfDirectorMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<BoardOfDirectorPowerAndFunctionViewModel> GetActiveEntry()
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorPowerAndFunctionViewModel>("SELECT * FROM dbo.GetBoardOfDirectorPowerAndFunctionEntry(@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BoardOfDirectorPowerAndFunctionViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorPowerAndFunctionViewModel>("SELECT * FROM dbo.GetBoardOfDirectorPowerAndFunctionEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<BoardOfDirectorPowerAndFunctionViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorPowerAndFunctionViewModel>("SELECT * FROM dbo.GetBoardOfDirectorPowerAndFunctionEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<BoardOfDirectorPowerAndFunctionViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorPowerAndFunctionViewModel>("SELECT * FROM dbo.GetBoardOfDirectorPowerAndFunctionEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<BoardOfDirectorPowerAndFunctionViewModel> GetRejectedEntry(Guid _BoardOfDirectorPowerAndFunctionId)
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorPowerAndFunctionViewModel>("SELECT * FROM dbo.GetBoardOfDirectorPowerAndFunctionEntry (@BoardOfDirectorPowerAndFunctionId)", new SqlParameter("@BoardOfDirectorPowerAndFunctionId", _BoardOfDirectorPowerAndFunctionId)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<BoardOfDirectorPowerAndFunctionViewModel> GetUnverifiedEntry(Guid _BoardOfDirectorPowerAndFunctionId)
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorPowerAndFunctionViewModel>("SELECT * FROM dbo.GetBoardOfDirectorPowerAndFunctionEntry (@BoardOfDirectorPowerAndFunctionId)", new SqlParameter("@BoardOfDirectorPowerAndFunctionId", _BoardOfDirectorPowerAndFunctionId)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<bool> Reject(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel)
        {
            try
            {
                // SetDefault Value
                _boardOfDirectorPowerAndFunctionViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorPowerAndFunctionViewModel.UserAction = StringLiteralValue.Reject;
                _boardOfDirectorPowerAndFunctionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BoardOfDirectorPowerAndFunctionMakerChecker boardOfDirectorMakerChecker = Mapper.Map<BoardOfDirectorPowerAndFunctionMakerChecker>(_boardOfDirectorPowerAndFunctionViewModel);

                BoardOfDirectorPowerAndFunctionTranslationMakerChecker directorTranslationMakerChecker = Mapper.Map<BoardOfDirectorPowerAndFunctionTranslationMakerChecker>(_boardOfDirectorPowerAndFunctionViewModel);

                context.BoardOfDirectorPowerAndFunctionTranslationMakerCheckers.Attach(directorTranslationMakerChecker);
                context.Entry(directorTranslationMakerChecker).State = EntityState.Added;

                context.BoardOfDirectorPowerAndFunctionMakerCheckers.Attach(boardOfDirectorMakerChecker);
                context.Entry(boardOfDirectorMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel)
        {
            try
            {
                // Set Default Value
                _boardOfDirectorPowerAndFunctionViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _boardOfDirectorPowerAndFunctionViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorPowerAndFunctionViewModel.EntryStatus = StringLiteralValue.Create;
                _boardOfDirectorPowerAndFunctionViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _boardOfDirectorPowerAndFunctionViewModel.Remark = "None";
                _boardOfDirectorPowerAndFunctionViewModel.UserAction = StringLiteralValue.Create;
                _boardOfDirectorPowerAndFunctionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _boardOfDirectorPowerAndFunctionViewModel.BoardOfDirectorPrmKey = managementDetailRepository.GetBoardOfDirectorPrmKeyById(_boardOfDirectorPowerAndFunctionViewModel.BoardOfDirectorId);
                _boardOfDirectorPowerAndFunctionViewModel.PowerAndFunctionPrmKey = powerAndFunctionRepository.GetPrmKeyById(_boardOfDirectorPowerAndFunctionViewModel.PowerAndFunctionId);

                BoardOfDirectorPowerAndFunction boardOfDirector = Mapper.Map<BoardOfDirectorPowerAndFunction>(_boardOfDirectorPowerAndFunctionViewModel);
                BoardOfDirectorPowerAndFunctionMakerChecker boardOfDirectorMakerChecker = Mapper.Map<BoardOfDirectorPowerAndFunctionMakerChecker>(_boardOfDirectorPowerAndFunctionViewModel);

                BoardOfDirectorPowerAndFunctionTranslation boardOfDirectorTranslation = Mapper.Map<BoardOfDirectorPowerAndFunctionTranslation>(_boardOfDirectorPowerAndFunctionViewModel);
                BoardOfDirectorPowerAndFunctionTranslationMakerChecker directorTranslationMakerChecker = Mapper.Map<BoardOfDirectorPowerAndFunctionTranslationMakerChecker>(_boardOfDirectorPowerAndFunctionViewModel);

                context.BoardOfDirectorPowerAndFunctionTranslationMakerCheckers.Attach(directorTranslationMakerChecker);
                context.Entry(directorTranslationMakerChecker).State = EntityState.Added;
                boardOfDirectorTranslation.BoardOfDirectorPowerAndFunctionTranslationMakerCheckers.Add(directorTranslationMakerChecker);

                context.BoardOfDirectorPowerAndFunctionTranslations.Attach(boardOfDirectorTranslation);
                context.Entry(boardOfDirectorTranslation).State = EntityState.Added;
                boardOfDirector.BoardOfDirectorPowerAndFunctionTranslations.Add(boardOfDirectorTranslation);

                context.BoardOfDirectorPowerAndFunctionMakerCheckers.Attach(boardOfDirectorMakerChecker);
                context.Entry(boardOfDirectorMakerChecker).State = EntityState.Added;
                boardOfDirector.BoardOfDirectorPowerAndFunctionMakerCheckers.Add(boardOfDirectorMakerChecker);

                context.BoardOfDirectorPowerAndFunctions.Attach(boardOfDirector);
                context.Entry(boardOfDirector).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel)
        {
            try
            {

                // Insert New Verified Entry
                // Set Default Value
                _boardOfDirectorPowerAndFunctionViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorPowerAndFunctionViewModel.UserAction = StringLiteralValue.Verify;
                _boardOfDirectorPowerAndFunctionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BoardOfDirectorPowerAndFunctionMakerChecker boardOfDirectorMakerChecker = Mapper.Map<BoardOfDirectorPowerAndFunctionMakerChecker>(_boardOfDirectorPowerAndFunctionViewModel);

                BoardOfDirectorPowerAndFunctionTranslationMakerChecker directorTranslationMakerChecker = Mapper.Map<BoardOfDirectorPowerAndFunctionTranslationMakerChecker>(_boardOfDirectorPowerAndFunctionViewModel);

                context.BoardOfDirectorPowerAndFunctionTranslationMakerCheckers.Attach(directorTranslationMakerChecker);
                context.Entry(directorTranslationMakerChecker).State = EntityState.Added;

                context.BoardOfDirectorPowerAndFunctionMakerCheckers.Attach(boardOfDirectorMakerChecker);
                context.Entry(boardOfDirectorMakerChecker).State = EntityState.Added;

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
