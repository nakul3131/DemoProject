using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Management.Parameter;
using DemoProject.Domain.Entities.Management.Parameter;
using DemoProject.Services.ViewModel.Management.Parameter;

namespace DemoProject.Services.Concrete.Management.Parameter
{
    public class EFBoardOfDirectorParameterRepository : IBoardOfDirectorParameterRepository
    {
        private readonly EFDbContext context;

        private readonly IAssuranceDeedFormatRepository assuranceDeedFormatRepository;

        public EFBoardOfDirectorParameterRepository(RepositoryConnection _connection, IAssuranceDeedFormatRepository _assuranceDeedFormatRepository)
        {
            context             = _connection.EFDbContext;

            assuranceDeedFormatRepository = _assuranceDeedFormatRepository;
        }

        public async Task<bool> Amend(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel)
        {
            try
            {
                // Set Default Value
                _boardOfDirectorParameterViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorParameterViewModel.EntryStatus = StringLiteralValue.Amend;
                _boardOfDirectorParameterViewModel.UserAction = StringLiteralValue.Amend;
                _boardOfDirectorParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _boardOfDirectorParameterViewModel.ReasonForModification = "None";

                // Get PrmKey By Id
                _boardOfDirectorParameterViewModel.AssuranceDeedFormat =assuranceDeedFormatRepository.GetPrmKeyById(_boardOfDirectorParameterViewModel.AssuranceDeedFormatId);

                //Mapping
                BoardOfDirectorParameter boardOfDirectorParameter = Mapper.Map<BoardOfDirectorParameter>(_boardOfDirectorParameterViewModel);
                BoardOfDirectorParameterMakerChecker boardOfDirectorParameterMakerChecker = Mapper.Map<BoardOfDirectorParameterMakerChecker>(_boardOfDirectorParameterViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                boardOfDirectorParameter.PrmKey = _boardOfDirectorParameterViewModel.BoardOfDirectorParameterPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                //BoardOfDirectorParameter
                context.BoardOfDirectorParameterMakerCheckers.Attach(boardOfDirectorParameterMakerChecker);
                context.Entry(boardOfDirectorParameterMakerChecker).State = EntityState.Added;
                boardOfDirectorParameter.BoardOfDirectorParameterMakerCheckers.Add(boardOfDirectorParameterMakerChecker);

                context.BoardOfDirectorParameters.Attach(boardOfDirectorParameter);
                context.Entry(boardOfDirectorParameter).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel)
        {
            try
            {
                // Set Default Value
                _boardOfDirectorParameterViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorParameterViewModel.UserAction = StringLiteralValue.Delete;
                _boardOfDirectorParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                BoardOfDirectorParameterMakerChecker boardOfDirectorParameterMakerChecker = Mapper.Map<BoardOfDirectorParameterMakerChecker>(_boardOfDirectorParameterViewModel);

                //BoardOfDirectorParameter
                context.BoardOfDirectorParameterMakerCheckers.Attach(boardOfDirectorParameterMakerChecker);
                context.Entry(boardOfDirectorParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<BoardOfDirectorParameterViewModel> GetActiveEntry()
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorParameterViewModel>("SELECT * FROM dbo.GetBoardOfDirectorParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<BoardOfDirectorParameterViewModel>> GetBoardOfDirectorParameterIndex()
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorParameterViewModel>("SELECT * FROM dbo.GetBoardOfDirectorParameterEntries (@UserProfilePrmKey,@EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BoardOfDirectorParameterViewModel> GetRejectedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorParameterViewModel>("SELECT * FROM dbo.GetBoardOfDirectorParameterEntry (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BoardOfDirectorParameterViewModel> GetUnverifiedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorParameterViewModel>("SELECT * FROM dbo.GetBoardOfDirectorParameterEntry(@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();

            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> IsAnyAuthorizationPending()
        {
            // Check Created, Amended and rejected entries count
            int count = await context.BoardOfDirectorParameters
                        .Where(u => u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Amend || u.EntryStatus == StringLiteralValue.Reject)
                        .Select(u => u.PrmKey).CountAsync();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Reject(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel)
        {
            try
            {
                // Set Default Value
                _boardOfDirectorParameterViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorParameterViewModel.UserAction = StringLiteralValue.Reject;
                _boardOfDirectorParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                BoardOfDirectorParameterMakerChecker boardOfDirectorParameterMakerChecker = Mapper.Map<BoardOfDirectorParameterMakerChecker>(_boardOfDirectorParameterViewModel);

                //BoardOfDirectorParameter
                context.BoardOfDirectorParameterMakerCheckers.Attach(boardOfDirectorParameterMakerChecker);
                context.Entry(boardOfDirectorParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel)
        {
            try
            {
                // Set Default Value
                _boardOfDirectorParameterViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorParameterViewModel.EntryStatus = StringLiteralValue.Create;
                _boardOfDirectorParameterViewModel.Remark = "None";
                _boardOfDirectorParameterViewModel.UserAction = StringLiteralValue.Create;
                _boardOfDirectorParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _boardOfDirectorParameterViewModel.ReasonForModification = "None";

                // Get PrmKey By Id
                _boardOfDirectorParameterViewModel.AssuranceDeedFormat = assuranceDeedFormatRepository.GetPrmKeyById(_boardOfDirectorParameterViewModel.AssuranceDeedFormatId);

                //Mapping
                BoardOfDirectorParameter boardOfDirectorParameter = Mapper.Map<BoardOfDirectorParameter>(_boardOfDirectorParameterViewModel);
                BoardOfDirectorParameterMakerChecker boardOfDirectorParameterMakerChecker = Mapper.Map<BoardOfDirectorParameterMakerChecker>(_boardOfDirectorParameterViewModel);

                //BoardOfDirectorParameter
                context.BoardOfDirectorParameterMakerCheckers.Attach(boardOfDirectorParameterMakerChecker);
                context.Entry(boardOfDirectorParameterMakerChecker).State = EntityState.Added;
                boardOfDirectorParameter.BoardOfDirectorParameterMakerCheckers.Add(boardOfDirectorParameterMakerChecker);

                context.BoardOfDirectorParameters.Attach(boardOfDirectorParameter);
                context.Entry(boardOfDirectorParameter).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel)
        {
            try
            {
                // First Modify Record - Get Active Record (i.e. Whose Entry Status is Verified)                 
                BoardOfDirectorParameterViewModel boardOfDirectorParameterViewModelOldEntry = await GetActiveEntry();

                if(boardOfDirectorParameterViewModelOldEntry.PrmKey > 0)
                {
                    // Set Default Value
                    boardOfDirectorParameterViewModelOldEntry.EntryDateTime = DateTime.Now;
                    boardOfDirectorParameterViewModelOldEntry.UserAction = StringLiteralValue.Modify;
                    boardOfDirectorParameterViewModelOldEntry.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    BoardOfDirectorParameterMakerChecker boardOfDirectorParameterMakerCheckerForModify = Mapper.Map<BoardOfDirectorParameterMakerChecker>(boardOfDirectorParameterViewModelOldEntry);

                    //BoardOfDirectorParameter
                    context.BoardOfDirectorParameterMakerCheckers.Attach(boardOfDirectorParameterMakerCheckerForModify);
                    context.Entry(boardOfDirectorParameterMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify Record
                // Set Default Value
                _boardOfDirectorParameterViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorParameterViewModel.UserAction = StringLiteralValue.Verify;
                _boardOfDirectorParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                BoardOfDirectorParameterMakerChecker boardOfDirectorParameterMakerChecker = Mapper.Map<BoardOfDirectorParameterMakerChecker>(_boardOfDirectorParameterViewModel);

                //BoardOfDirectorParameter
                context.BoardOfDirectorParameterMakerCheckers.Attach(boardOfDirectorParameterMakerChecker);
                context.Entry(boardOfDirectorParameterMakerChecker).State = EntityState.Added;

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
