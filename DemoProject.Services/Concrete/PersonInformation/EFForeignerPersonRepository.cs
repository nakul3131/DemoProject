using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Domain.Entities.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFForeignerPersonRepository : IForeignerPersonRepository
    {
        private readonly EFDbContext context;
        
        private readonly IPersonMasterRepository personMasterRepository;

        public EFForeignerPersonRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
        }

        public async Task<bool> Amend(ForeignerViewModel _foreignerPersonViewModel)
        {
            try
            {
                // Set Default Value
                _foreignerPersonViewModel.EntryDateTime = DateTime.Now;
                _foreignerPersonViewModel.EntryStatus = StringLiteralValue.Amend;
                _foreignerPersonViewModel.ReasonForModification = "None";
                _foreignerPersonViewModel.Remark = _foreignerPersonViewModel.Remark;
                _foreignerPersonViewModel.UserAction = StringLiteralValue.Amend;
                _foreignerPersonViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _foreignerPersonViewModel.PersonPrmKey = _foreignerPersonViewModel.PersonPrmKey;

                //Mapping
                ForeignerPerson foreignerPerson = Mapper.Map<ForeignerPerson>(_foreignerPersonViewModel);
                ForeignerPersonMakerChecker foreignerPersonMakerChecker = Mapper.Map<ForeignerPersonMakerChecker>(_foreignerPersonViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                foreignerPerson.PrmKey = _foreignerPersonViewModel.ForeignerPersonPrmKey;

                //ForeignerPerson
                context.ForeignerPersons.Attach(foreignerPerson);
                context.Entry(foreignerPerson).State = EntityState.Modified;

                //ForeignerPersonMakerChecker
                context.ForeignerPersonMakerCheckers.Attach(foreignerPersonMakerChecker);
                context.Entry(foreignerPersonMakerChecker).State = EntityState.Added;
                foreignerPerson.ForeignerPersonMakerCheckers.Add(foreignerPersonMakerChecker);
                
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(ForeignerViewModel _foreignerPersonViewModel)
        {
            try
            {
                // Set Default Value
                _foreignerPersonViewModel.EntryDateTime = DateTime.Now;
                _foreignerPersonViewModel.UserAction = StringLiteralValue.Delete;
                _foreignerPersonViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                ForeignerPersonMakerChecker centerTradingDetailMakerChecker = Mapper.Map<ForeignerPersonMakerChecker>(_foreignerPersonViewModel);

                context.ForeignerPersonMakerCheckers.Attach(centerTradingDetailMakerChecker);
                context.Entry(centerTradingDetailMakerChecker).State = EntityState.Added;
                
                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<ForeignerViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ForeignerViewModel>("SELECT * FROM dbo.GetPersonForForeignerPersonCRUDOperation (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ForeignerViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ForeignerViewModel>("SELECT * FROM dbo.GetPersonForForeignerPersonCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PersonViewModel>("SELECT * FROM dbo.GetForeignerPersonCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ForeignerViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<ForeignerViewModel>("SELECT * FROM dbo.GetPersonForForeignerPersonCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //public long GetPrmKeyById(Guid _foreignerPersonId)
        //{
        //    return context.ForeignerPersons
        //            .Where(c => c.ForeignerPersonId == _foreignerPersonId)
        //            .Select(c => c.PrmKey).FirstOrDefault();
        //}

        public async Task<IEnumerable<ForeignerViewModel>> GetRejectedEntries(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<ForeignerViewModel>("SELECT * FROM dbo.GetForeignerPersonEntryByPersonPrmKey (@UserProfilePrmKey, @PersonPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ForeignerViewModel>> GetUnverifiedEntries(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<ForeignerViewModel>("SELECT * FROM dbo.GetForeignerPersonEntryByPersonPrmKey (@UserProfilePrmKey, @PersonPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ForeignerViewModel>> GetVerifiedEntries(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<ForeignerViewModel>("SELECT * FROM dbo.GetForeignerPersonEntryByPersonPrmKey (@UserProfilePrmKey, @PersonPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ForeignerViewModel> GetViewModelForCreate(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<ForeignerViewModel>("SELECT * FROM dbo.GetForeignerPersonEntryByPersonPrmKey ( @PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ForeignerViewModel> GetViewModelForReject(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<ForeignerViewModel>("SELECT * FROM dbo.GetForeignerPersonEntryByPersonPrmKey ( @PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ForeignerViewModel> GetViewModelForUnverified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<ForeignerViewModel>("SELECT * FROM dbo.GetForeignerPersonEntryByPersonPrmKey ( @PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ForeignerViewModel> GetViewModelForVerified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<ForeignerViewModel>("SELECT * FROM dbo.GetForeignerPersonEntryByPersonPrmKey ( @PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(ForeignerViewModel _foreignerPersonViewModel)
        {
            try
            {
                // Set Default Value
                _foreignerPersonViewModel.EntryDateTime = DateTime.Now;
                _foreignerPersonViewModel.EntryStatus = StringLiteralValue.Create;
                _foreignerPersonViewModel.Remark = "None";
                _foreignerPersonViewModel.ReasonForModification = _foreignerPersonViewModel.ReasonForModification;
                _foreignerPersonViewModel.UserAction = StringLiteralValue.Create;
                _foreignerPersonViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _foreignerPersonViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_foreignerPersonViewModel.PersonId);
                
                //Mapping
                ForeignerPerson foreignerPerson = Mapper.Map<ForeignerPerson>(_foreignerPersonViewModel);
                ForeignerPersonMakerChecker foreignerPersonMakerChecker = Mapper.Map<ForeignerPersonMakerChecker>(_foreignerPersonViewModel);
                
                // Save Data In Appropriate Tables By Entity Framework Methods
                //ForeignerPerson
                context.ForeignerPersons.Attach(foreignerPerson);
                context.Entry(foreignerPerson).State = EntityState.Added;

                //ForeignerPersonMakerChecker
                context.ForeignerPersonMakerCheckers.Attach(foreignerPersonMakerChecker);
                context.Entry(foreignerPersonMakerChecker).State = EntityState.Added;
                foreignerPerson.ForeignerPersonMakerCheckers.Add(foreignerPersonMakerChecker);
                
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(ForeignerViewModel _foreignerPersonViewModel)
        {
            try
            {
                // Set Default Value
                _foreignerPersonViewModel.EntryDateTime = DateTime.Now;
                _foreignerPersonViewModel.UserAction = StringLiteralValue.Reject;
                _foreignerPersonViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                ForeignerPersonMakerChecker foreignerPersonViewModelMakerChecker = Mapper.Map<ForeignerPersonMakerChecker>(_foreignerPersonViewModel);

                context.ForeignerPersonMakerCheckers.Attach(foreignerPersonViewModelMakerChecker);
                context.Entry(foreignerPersonViewModelMakerChecker).State = EntityState.Added;
                
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(ForeignerViewModel _foreignerPersonViewModel)
        {
            try
            {
                _foreignerPersonViewModel.EntryDateTime = DateTime.Now;
                _foreignerPersonViewModel.EntryStatus = StringLiteralValue.Create;
                _foreignerPersonViewModel.ReasonForModification = "None";
                _foreignerPersonViewModel.Remark = "None";
                _foreignerPersonViewModel.UserAction = StringLiteralValue.Create;
                _foreignerPersonViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                _foreignerPersonViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_foreignerPersonViewModel.PersonId);
                
                ForeignerPerson foreignerPerson = Mapper.Map<ForeignerPerson>(_foreignerPersonViewModel);
                ForeignerPersonMakerChecker foreignerPersonMakerChecker = Mapper.Map<ForeignerPersonMakerChecker>(_foreignerPersonViewModel);

                //foreignerPerson.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_foreignerPersonViewModel.PersonId);
                    
                    context.ForeignerPersons.Attach(foreignerPerson);
                    context.Entry(foreignerPerson).State = EntityState.Added;

                    context.ForeignerPersonMakerCheckers.Attach(foreignerPersonMakerChecker);
                    context.Entry(foreignerPersonMakerChecker).State = EntityState.Added;
                    foreignerPerson.ForeignerPersonMakerCheckers.Add(foreignerPersonMakerChecker);
                
                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> SaveModification(ForeignerViewModel _foreignerPersonViewModel)
        {
            try
            {
                // Get Trading Entity Details From Session Object
                List<ForeignerViewModel> foreignerPersonViewModelList = new List<ForeignerViewModel>();
                foreignerPersonViewModelList = (List<ForeignerViewModel>)HttpContext.Current.Session["ForeignerPerson"];

                foreach (ForeignerViewModel viewModel in foreignerPersonViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.ReasonForModification = "None";
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    ForeignerPerson foreignerPersonViewModel = Mapper.Map<ForeignerPerson>(viewModel);
                    foreignerPersonViewModel.PersonPrmKey = _foreignerPersonViewModel.PersonPrmKey;

                    ForeignerPersonMakerChecker foreignerPersonViewModelMakerChecker = Mapper.Map<ForeignerPersonMakerChecker>(viewModel);

                    context.ForeignerPersonMakerCheckers.Attach(foreignerPersonViewModelMakerChecker);
                    context.Entry(foreignerPersonViewModelMakerChecker).State = EntityState.Added;
                    foreignerPersonViewModel.ForeignerPersonMakerCheckers.Add(foreignerPersonViewModelMakerChecker);

                    context.ForeignerPersons.Attach(foreignerPersonViewModel);
                    context.Entry(foreignerPersonViewModel).State = EntityState.Added;
                }

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(ForeignerViewModel _foreignerPersonViewModel)
        {
            try
            {
                // Assign MDF Status To EntryStatus For Performing Modify Operation
                ForeignerViewModel foreignerViewModelForModify = await GetViewModelForVerified(_foreignerPersonViewModel.PersonPrmKey);
                if (foreignerViewModelForModify != null)
                {
                    // Set Default Value
                    foreignerViewModelForModify.EntryDateTime = DateTime.Now;
                    foreignerViewModelForModify.UserAction = StringLiteralValue.Modify;
                    foreignerViewModelForModify.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    ForeignerPersonMakerChecker foreignerPersonMakerCheckerForModify = Mapper.Map<ForeignerPersonMakerChecker>(foreignerViewModelForModify);
                    
                    //ForeignerPersonMakerChecker
                    context.ForeignerPersonMakerCheckers.Attach(foreignerPersonMakerCheckerForModify);
                    context.Entry(foreignerPersonMakerCheckerForModify).State = EntityState.Added;
                    
                }

                // Verify New Record
                // Set Default Value
                _foreignerPersonViewModel.EntryDateTime = DateTime.Now;
                _foreignerPersonViewModel.UserAction = StringLiteralValue.Verify;
                _foreignerPersonViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                ForeignerPersonMakerChecker foreignerPersonMakerChecker = Mapper.Map<ForeignerPersonMakerChecker>(_foreignerPersonViewModel);
                
                //ForeignerPersonMakerCheckers
                context.ForeignerPersonMakerCheckers.Attach(foreignerPersonMakerChecker);
                context.Entry(foreignerPersonMakerChecker).State = EntityState.Added;
                
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
