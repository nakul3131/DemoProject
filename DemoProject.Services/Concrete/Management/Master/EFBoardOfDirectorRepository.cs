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
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Domain.Entities.Management.Master;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Abstract.Account.Customer;
using System.Web.Mvc;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFBoardOfDirectorRepository : IBoardOfDirectorRepository
    {
        private readonly EFDbContext context;

        private readonly ICustomerAccountRepository customerAccountRepository;
        private readonly IManagementDetailRepository managementDetailRepository;

        public EFBoardOfDirectorRepository(RepositoryConnection _connection, ICustomerAccountRepository _customerAccountRepository, IManagementDetailRepository _managementDetailRepository)
        {
            context = _connection.EFDbContext;
            customerAccountRepository = _customerAccountRepository;
            managementDetailRepository = _managementDetailRepository;
        }

        public async Task<bool> Amend(BoardOfDirectorViewModel _boardOfDirectorViewModel)
        {
            try
            {
                // Set Default Value
                _boardOfDirectorViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _boardOfDirectorViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorViewModel.EntryStatus = StringLiteralValue.Amend;
                _boardOfDirectorViewModel.Remark = "None";
                _boardOfDirectorViewModel.UserAction = StringLiteralValue.Amend;
                _boardOfDirectorViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _boardOfDirectorViewModel.CustomerAccountPrmKey = customerAccountRepository.GetPrmKeyById(_boardOfDirectorViewModel.CustomerAccountId);
                _boardOfDirectorViewModel.DesignationPrmKey = managementDetailRepository.GetDesignationPrmKeyById(_boardOfDirectorViewModel.DesignationId);

                // Mapping 
                // BoardOfDirector
                BoardOfDirector boardOfDirector = Mapper.Map<BoardOfDirector>(_boardOfDirectorViewModel);
                BoardOfDirectorMakerChecker boardOfDirectorMakerChecker = Mapper.Map<BoardOfDirectorMakerChecker>(_boardOfDirectorViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                boardOfDirector.PrmKey = _boardOfDirectorViewModel.BoardOfDirectorPrmKey;

                // BoardOfDirector
                context.BoardOfDirectorMakerCheckers.Attach(boardOfDirectorMakerChecker);
                context.Entry(boardOfDirectorMakerChecker).State = EntityState.Added;
                boardOfDirector.BoardOfDirectorMakerCheckers.Add(boardOfDirectorMakerChecker);

                context.BoardOfDirectors.Attach(boardOfDirector);
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

        public async Task<bool> Delete(BoardOfDirectorViewModel _boardOfDirectorViewModel)
        {
            try
            {
                // Set Default Value
                _boardOfDirectorViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorViewModel.Remark = "None";
                _boardOfDirectorViewModel.UserAction = StringLiteralValue.Delete;
                _boardOfDirectorViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                BoardOfDirectorMakerChecker boardOfDirectorMakerChecker = Mapper.Map<BoardOfDirectorMakerChecker>(_boardOfDirectorViewModel);

                //BoardOfDirector
                context.BoardOfDirectorMakerCheckers.Attach(boardOfDirectorMakerChecker);
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

        public List<SelectListItem> BoardOfDirectorDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.BoardOfDirectors
                            join t in context.CustomerAccountDetails.Where(t => t.EntryStatus == StringLiteralValue.Verify) on r.CustomerAccountPrmKey equals t.CustomerAccountPrmKey into rt
                            from t in rt.DefaultIfEmpty()
                            join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify && p.ActivationStatus == StringLiteralValue.Active) on t.PersonPrmKey equals p.PrmKey  into tp
                            from p in tp.DefaultIfEmpty()
                            /*where (r.BoardOfDirectorId.Equals(p.PersonId))*/   // Chairman Designation PrmKey = 1
                            select new SelectListItem
                            {
                                Value = r.BoardOfDirectorId.ToString(),
                                Text = p.FullName.Trim()
                            }).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from r in context.BoardOfDirectors
                        join t in context.CustomerAccountDetails.Where(t => t.EntryStatus == StringLiteralValue.Verify) on r.CustomerAccountPrmKey equals t.CustomerAccountPrmKey into rt
                        from t in rt.DefaultIfEmpty()
                        join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify && p.ActivationStatus == StringLiteralValue.Active) on t.PersonPrmKey equals p.PrmKey into tp
                        from p in tp.DefaultIfEmpty()
                            /*where (r.BoardOfDirectorId.Equals(p.PersonId))*/   // Chairman Designation PrmKey = 1
                        select new SelectListItem
                        {
                            Value = r.BoardOfDirectorId.ToString(),
                            Text = p.FullName.Trim()
                        }).ToList();
            }
        }

        public async Task<BoardOfDirectorViewModel> GetActiveEntry()
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorViewModel>("SELECT * FROM dbo.GetBoardOfDirectorEntry(@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public long GetChairmanPersonPrmKey()
        {
            return (from b in context.BoardOfDirectors
                    join c in context.CustomerAccounts.Where(c => c.EntryStatus == StringLiteralValue.Verify && c.ActivationStatus == StringLiteralValue.Active) on b.CustomerAccountPrmKey equals c.PrmKey into bc
                    from c in bc.DefaultIfEmpty()
                    join d in context.CustomerAccountDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals d.CustomerAccountPrmKey into cd
                    from d in cd.DefaultIfEmpty()
                    where (b.DesignationPrmKey.Equals(1))   // Chairman Designation PrmKey = 1
                    select (d.PersonPrmKey)).FirstOrDefault();
        }

        public long GetViceChairmanPersonPrmKey()
        {
            return (from b in context.BoardOfDirectors
                    join c in context.CustomerAccounts.Where(c => c.EntryStatus == StringLiteralValue.Verify && c.ActivationStatus == StringLiteralValue.Active) on b.CustomerAccountPrmKey equals c.PrmKey into bc
                    from c in bc.DefaultIfEmpty()
                    join d in context.CustomerAccountDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals d.CustomerAccountPrmKey into cd
                    from d in cd.DefaultIfEmpty()
                    where (b.DesignationPrmKey.Equals(2))   // ViceChairman Designation PrmKey = 2
                    select (d.PersonPrmKey)).FirstOrDefault();
        }

        public async Task<IEnumerable<BoardOfDirectorViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorViewModel>("SELECT * FROM dbo.GetBoardOfDirectorEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<BoardOfDirectorViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorViewModel>("SELECT * FROM dbo.GetBoardOfDirectorEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<BoardOfDirectorViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorViewModel>("SELECT * FROM dbo.GetBoardOfDirectorEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<BoardOfDirectorViewModel> GetRejectedEntry(Guid _BoardOfDirectorId)
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorViewModel>("SELECT * FROM dbo.GetBoardOfDirectorEntry (@BoardOfDirectorId,@EntryType)", new SqlParameter("@BoardOfDirectorId", _BoardOfDirectorId), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<BoardOfDirectorViewModel> GetUnverifiedEntry(Guid _BoardOfDirectorId)
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorViewModel>("SELECT * FROM dbo.GetBoardOfDirectorEntry (@BoardOfDirectorId,@EntryType)", new SqlParameter("@BoardOfDirectorId", _BoardOfDirectorId), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<BoardOfDirectorViewModel> GetVerifiedEntry(Guid _BoardOfDirectorId)
        {
            try
            {
                return await context.Database.SqlQuery<BoardOfDirectorViewModel>("SELECT * FROM dbo.GetBoardOfDirectorEntry (@BoardOfDirectorId,@EntryType)", new SqlParameter("@BoardOfDirectorId", _BoardOfDirectorId), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<bool> Reject(BoardOfDirectorViewModel _boardOfDirectorViewModel)
        {
            try
            {
                // Set Default Value
                _boardOfDirectorViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorViewModel.UserAction = StringLiteralValue.Reject;
                _boardOfDirectorViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                BoardOfDirectorMakerChecker boardOfDirectorMakerChecker = Mapper.Map<BoardOfDirectorMakerChecker>(_boardOfDirectorViewModel);

                //BoardOfDirector
                context.BoardOfDirectorMakerCheckers.Attach(boardOfDirectorMakerChecker);
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

        public async Task<bool> Save(BoardOfDirectorViewModel _boardOfDirectorViewModel)
        {
            try
            {
                // Set Default Value
                _boardOfDirectorViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _boardOfDirectorViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorViewModel.EntryStatus = StringLiteralValue.Create;
                _boardOfDirectorViewModel.Remark = "None";
                _boardOfDirectorViewModel.UserAction = StringLiteralValue.Create;
                _boardOfDirectorViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Get Prmkey By Id
                _boardOfDirectorViewModel.CustomerAccountPrmKey = customerAccountRepository.GetPrmKeyById(_boardOfDirectorViewModel.CustomerAccountId);
                _boardOfDirectorViewModel.DesignationPrmKey = managementDetailRepository.GetDesignationPrmKeyById(_boardOfDirectorViewModel.DesignationId);

                //Mapping
                //BoardOfDirector
                BoardOfDirector boardOfDirector = Mapper.Map<BoardOfDirector>(_boardOfDirectorViewModel);
                BoardOfDirectorMakerChecker boardOfDirectorMakerChecker = Mapper.Map<BoardOfDirectorMakerChecker>(_boardOfDirectorViewModel);

                //BoardOfDirector
                context.BoardOfDirectorMakerCheckers.Attach(boardOfDirectorMakerChecker);
                context.Entry(boardOfDirectorMakerChecker).State = EntityState.Added;
                boardOfDirector.BoardOfDirectorMakerCheckers.Add(boardOfDirectorMakerChecker);

                context.BoardOfDirectors.Attach(boardOfDirector);
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

        public async Task<bool> Verify(BoardOfDirectorViewModel _boardOfDirectorViewModel)
        {
            try
            {
                // Verify New Record
                // Set Default Value
                _boardOfDirectorViewModel.EntryDateTime = DateTime.Now;
                _boardOfDirectorViewModel.UserAction = StringLiteralValue.Verify;
                _boardOfDirectorViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                BoardOfDirectorMakerChecker boardOfDirectorMakerChecker = Mapper.Map<BoardOfDirectorMakerChecker>(_boardOfDirectorViewModel);

                //BoardOfDirector
                context.BoardOfDirectorMakerCheckers.Attach(boardOfDirectorMakerChecker);
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
