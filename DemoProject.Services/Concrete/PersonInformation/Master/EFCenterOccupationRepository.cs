using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation.Master;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.PersonInformation.Master;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Domain.Entities.PersonInformation.Master;

namespace DemoProject.Services.Concrete.PersonInformation.Master
{
    public class EFCenterOccupationRepository : ICenterOccupationRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonDetailRepository personInformationDetailRepository;

        public EFCenterOccupationRepository(RepositoryConnection _connection, IPersonDetailRepository _personInformationDetailRepository)
        {
            context = _connection.EFDbContext;
            personInformationDetailRepository = _personInformationDetailRepository;
        }

        public async Task<bool> Amend(CenterOccupationViewModel _centerOccupationViewModel)
        {
            try
            {
                IEnumerable<CenterOccupationViewModel> centerOccupationList = await GetRejectedEntries(_centerOccupationViewModel.CenterPrmKey);

                foreach (CenterOccupationViewModel centerOccupationViewModel in centerOccupationList)
                {
                    // Set Default Value
                    centerOccupationViewModel.EntryDateTime = DateTime.Now;
                    centerOccupationViewModel.UserAction = StringLiteralValue.Amend;
                    centerOccupationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(centerOccupationViewModel);

                    //CenterOccupationMakerChecker
                    context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                    context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
                }

                // Set Default Value
                _centerOccupationViewModel.EntryDateTime = DateTime.Now;
                _centerOccupationViewModel.EntryStatus = StringLiteralValue.Create;
                _centerOccupationViewModel.PrmKey = 0;
                _centerOccupationViewModel.CenterOccupationPrmKey = 0;
                _centerOccupationViewModel.ReasonForModification = "None";
                _centerOccupationViewModel.UserAction = StringLiteralValue.Create;
                _centerOccupationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                foreach (Guid OccupationId in _centerOccupationViewModel.SelectedOccupationId)
                {
                    //Mapping
                    CenterOccupation centerOccupation = Mapper.Map<CenterOccupation>(_centerOccupationViewModel);
                    centerOccupation.OccupationPrmKey = personInformationDetailRepository.GetOccupationPrmKeyById(OccupationId);
                    CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(_centerOccupationViewModel);

                    //CenterOccupation
                    context.CenterOccupations.Attach(centerOccupation);
                    context.Entry(centerOccupation).State = EntityState.Added;

                    //CenterOccupationMakerChecker
                    context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                    context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
                    centerOccupation.CenterOccupationMakerCheckers.Add(centerOccupationMakerChecker);

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

        public async Task<bool> Delete(CenterOccupationViewModel _centerOccupationViewModel)
        {
            try
            {

                IEnumerable<CenterOccupationViewModel> centerOccupationList = await GetRejectedEntries(_centerOccupationViewModel.CenterPrmKey);

                foreach (CenterOccupationViewModel centerOccupationViewModel in centerOccupationList)
                {
                    // Set Default Value
                    centerOccupationViewModel.EntryDateTime = DateTime.Now;
                    centerOccupationViewModel.UserAction = StringLiteralValue.Delete;
                    centerOccupationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(centerOccupationViewModel);

                    //CenterOccupationMakerChecker
                    context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                    context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
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

        public async Task<IEnumerable<CenterOccupationViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CenterOccupationViewModel>("SELECT * FROM dbo.GetCenterEntriesOfOccupation(@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterOccupationViewModel>> GetIndexOfUnverifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CenterOccupationViewModel>("SELECT * FROM dbo.GetCenterEntriesOfOccupation(@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterOccupationViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CenterOccupationViewModel>("SELECT * FROM dbo.GetCenterEntriesForOccupationCRUDOperation (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterOccupationViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<CenterOccupationViewModel>("SELECT * FROM dbo.GetCenterEntriesOfOccupation( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterOccupationViewModel>> GetRejectedEntries(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterOccupationViewModel>("SELECT * FROM dbo.GetCenterOccupationEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterOccupationViewModel>> GetUnverifiedEntries(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterOccupationViewModel>("SELECT * FROM dbo.GetCenterOccupationEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterOccupationViewModel>> GetVerifiedEntries(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterOccupationViewModel>("SELECT * FROM dbo.GetCenterOccupationEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterOccupationViewModel> GetViewModelForCreate(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterOccupationViewModel>("SELECT * FROM dbo.GetCenterOccupationEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterOccupationViewModel> GetViewModelForReject(short _centerPrmKey)
        {
            CenterOccupationViewModel centerOccupationViewModel = new CenterOccupationViewModel();

            try
            {
                centerOccupationViewModel = await context.Database.SqlQuery<CenterOccupationViewModel>("SELECT * FROM dbo.GetCenterOccupationEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();

                IEnumerable<CenterOccupationViewModel> centerOccupationList = await GetRejectedEntries(_centerPrmKey);
                short i = 0;
                Guid[] a = new Guid[50];
                foreach (CenterOccupationViewModel centerOccupationViewModel1 in centerOccupationList)
                {
                    a[i] = centerOccupationViewModel1.OccupationId;

                    i++;
                }
                centerOccupationViewModel.SelectedOccupationId = a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }

            return centerOccupationViewModel;
        }

        public async Task<CenterOccupationViewModel> GetViewModelForUnverified(short _centerPrmKey)
        {
            CenterOccupationViewModel centerOccupationViewModel = new CenterOccupationViewModel();

            try
            {
                centerOccupationViewModel = await context.Database.SqlQuery<CenterOccupationViewModel>("SELECT * FROM dbo.GetCenterOccupationEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();

                IEnumerable<CenterOccupationViewModel> centerOccupationList = await GetUnverifiedEntries(_centerPrmKey);
                short i = 0;
                Guid[] a = new Guid[50];
                foreach (CenterOccupationViewModel centerOccupationViewModel1 in centerOccupationList)
                {
                    a[i] = centerOccupationViewModel1.OccupationId;

                    i++;
                }
                centerOccupationViewModel.SelectedOccupationId = a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }

            return centerOccupationViewModel;
        }

        public async Task<CenterOccupationViewModel> GetViewModelForVerified(short _centerPrmKey)
        {
            CenterOccupationViewModel centerOccupationViewModel = new CenterOccupationViewModel();

            try
            {
                centerOccupationViewModel = await context.Database.SqlQuery<CenterOccupationViewModel>("SELECT * FROM dbo.GetCenterOccupationEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();

                IEnumerable<CenterOccupationViewModel> centerOccupationList = await GetVerifiedEntries(_centerPrmKey);
                short i = 0;
                Guid[] a = new Guid[50];
                foreach (CenterOccupationViewModel centerOccupationViewModel1 in centerOccupationList)
                {
                    a[i] = centerOccupationViewModel1.OccupationId;

                    i++;
                }
                centerOccupationViewModel.SelectedOccupationId = a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }

            return centerOccupationViewModel;
        }

        public async Task<bool> Reject(CenterOccupationViewModel _centerOccupationViewModel)
        {
            try
            {
                IEnumerable<CenterOccupationViewModel> centerOccupationList = await GetUnverifiedEntries(_centerOccupationViewModel.CenterPrmKey);

                foreach (CenterOccupationViewModel centerOccupationViewModel in centerOccupationList)
                {
                    // Set Default Value
                    centerOccupationViewModel.EntryDateTime = DateTime.Now;
                    centerOccupationViewModel.UserAction = StringLiteralValue.Reject;
                    centerOccupationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(centerOccupationViewModel);

                    context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                    context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Save(CenterOccupationViewModel _centerOccupationViewModel)
        {
            try
            {
                // Set Default Value
                _centerOccupationViewModel.EntryDateTime = DateTime.Now;
                _centerOccupationViewModel.EntryStatus = StringLiteralValue.Create;
                _centerOccupationViewModel.ReasonForModification = "None";
                _centerOccupationViewModel.Remark = "None";
                _centerOccupationViewModel.UserAction = StringLiteralValue.Create;
                _centerOccupationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                foreach (Guid OccupationId in _centerOccupationViewModel.SelectedOccupationId)
                {
                    //Mapping 
                    CenterOccupation centerOccupation = Mapper.Map<CenterOccupation>(_centerOccupationViewModel);
                    centerOccupation.OccupationPrmKey = personInformationDetailRepository.GetOccupationPrmKeyById(OccupationId);

                    CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(_centerOccupationViewModel);

                    //CenterOccupation
                    context.CenterOccupations.Attach(centerOccupation);
                    context.Entry(centerOccupation).State = EntityState.Added;

                    //CenterOccupationMakerCheckers
                    context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                    context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
                    centerOccupation.CenterOccupationMakerCheckers.Add(centerOccupationMakerChecker);

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

        public async Task<bool> Modify(CenterOccupationViewModel _centerOccupationViewModel)
        {
            try
            {
                // Set Default Value
                _centerOccupationViewModel.EntryDateTime = DateTime.Now;
                _centerOccupationViewModel.EntryStatus = StringLiteralValue.Create;
                _centerOccupationViewModel.ReasonForModification = "None";
                _centerOccupationViewModel.Remark = "None";
                _centerOccupationViewModel.UserAction = StringLiteralValue.Create;
                _centerOccupationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                foreach (Guid OccupationId in _centerOccupationViewModel.SelectedOccupationId)
                {
                    //Mapping
                    CenterOccupation centerOccupation = Mapper.Map<CenterOccupation>(_centerOccupationViewModel);
                    centerOccupation.OccupationPrmKey = personInformationDetailRepository.GetOccupationPrmKeyById(OccupationId);

                    CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(_centerOccupationViewModel);

                    //CenterOccupation
                    context.CenterOccupations.Attach(centerOccupation);
                    context.Entry(centerOccupation).State = EntityState.Added;

                    //CenterOccupationMakerCheckers
                    context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                    context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
                    centerOccupation.CenterOccupationMakerCheckers.Add(centerOccupationMakerChecker);

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

        public async Task<bool> Verify(CenterOccupationViewModel _centerOccupationViewModel)
        {
            try
            {
                // Modify old Entry - Get VerifiedEntry 
                IEnumerable<CenterOccupationViewModel> centerOccupationListForModify = await GetVerifiedEntries(_centerOccupationViewModel.CenterPrmKey);
                foreach (CenterOccupationViewModel viewModel in centerOccupationListForModify)
                {
                    // Set UserAction As Modify
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mappping
                    CenterOccupationMakerChecker centerOccupationMakerCheckerForModify = Mapper.Map<CenterOccupationMakerChecker>(viewModel);

                    //CenterOccupationMakerChecker
                    context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerCheckerForModify);
                    context.Entry(centerOccupationMakerCheckerForModify).State = EntityState.Added;
                }

                // Verify New Entry
                IEnumerable<CenterOccupationViewModel> centerOccupationList = await GetUnverifiedEntries(_centerOccupationViewModel.CenterPrmKey);

                foreach (CenterOccupationViewModel centerOccupationViewModel in centerOccupationList)
                {
                    // Set UserAction As Verify
                    centerOccupationViewModel.EntryDateTime = DateTime.Now;
                    centerOccupationViewModel.UserAction = StringLiteralValue.Verify;
                    centerOccupationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(centerOccupationViewModel);

                    //CenterOccupationMakerChecker
                    context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                    context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
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

    }
}
