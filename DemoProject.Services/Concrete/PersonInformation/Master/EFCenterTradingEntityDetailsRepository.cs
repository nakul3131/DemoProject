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
using DemoProject.Domain.Entities.PersonInformation.Master;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.Concrete.PersonInformation.Master
{
    public class EFCenterTradingEntityDetailsRepository : ICenterTradingEntityDetailsRepository
    {
        private readonly EFDbContext context;

        private readonly IPersonDetailRepository personInformationDetailRepository;     

        public EFCenterTradingEntityDetailsRepository(RepositoryConnection _connection, IPersonDetailRepository _personInformationDetailRepository)
        {
            context                 = _connection.EFDbContext;
            personInformationDetailRepository = _personInformationDetailRepository;
        }

        public async Task<bool> Amend(CenterTradingEntityDetailViewModel _centerTradingEntityDetailViewModel)
        {
            try
            {
                IEnumerable<CenterTradingEntityDetailViewModel> tradingEntityDetailViewModelList = await GetRejectedEntries(_centerTradingEntityDetailViewModel.CenterPrmKey);
                foreach (CenterTradingEntityDetailViewModel viewModel in tradingEntityDetailViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    CenterTradingEntityDetailMakerChecker centerTradingEntityDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                    //CenterTradingEntityDetailMakerChecker
                    context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingEntityDetailMakerChecker);
                    context.Entry(centerTradingEntityDetailMakerChecker).State = EntityState.Added;
                }

                // Get Trading Entity Details From Session Object
                List<CenterTradingEntityDetailViewModel> centerTradingEntityDetailViewModelList = new List<CenterTradingEntityDetailViewModel>();

                centerTradingEntityDetailViewModelList = (List<CenterTradingEntityDetailViewModel>)HttpContext.Current.Session["CenterTradingEntityDetail"];

                foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingEntityDetailViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _centerTradingEntityDetailViewModel.Remark;
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    viewModel.TradingEntityPrmKey = personInformationDetailRepository.GetTradingEntityPrmKeyById(viewModel.TradingEntityId);

                    CenterTradingEntityDetail centerTradingEntityDetail = Mapper.Map<CenterTradingEntityDetail>(viewModel);
                    centerTradingEntityDetail.CenterPrmKey = _centerTradingEntityDetailViewModel.CenterPrmKey;

                    CenterTradingEntityDetailMakerChecker centerTradingEntityDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);
                    
                    context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingEntityDetailMakerChecker);
                    context.Entry(centerTradingEntityDetailMakerChecker).State = EntityState.Added;
                    centerTradingEntityDetail.CenterTradingEntityDetailMakerCheckers.Add(centerTradingEntityDetailMakerChecker);

                    context.CenterTradingEntityDetails.Attach(centerTradingEntityDetail);
                    context.Entry(centerTradingEntityDetail).State = EntityState.Added;
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

        public async Task<bool> Delete(CenterTradingEntityDetailViewModel _centerTradingEntityDetailViewModel)
        {
            try
            {

                List<CenterTradingEntityDetailViewModel> centerTradingDetailViewModelList = new List<CenterTradingEntityDetailViewModel>();
                centerTradingDetailViewModelList = (List<CenterTradingEntityDetailViewModel>)HttpContext.Current.Session["CenterTradingEntityDetail"];

                foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingDetailViewModelList)
                {
                    // Set Default Value 
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    CenterTradingEntityDetailMakerChecker centerTradingDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                    //CenterTradingEntityDetailMakerChecker
                    context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingDetailMakerChecker);
                    context.Entry(centerTradingDetailMakerChecker).State = EntityState.Added;
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

        public async Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CenterTradingEntityDetailViewModel>("SELECT * FROM dbo.GetCenterEntriesForTradingEntityCRUDOperation (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CenterTradingEntityDetailViewModel>("SELECT * FROM dbo.GetCenterEntriesForTradingEntityCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CenterTradingEntityDetailViewModel>("SELECT * FROM dbo.GetCenterEntriesForTradingEntityCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<CenterTradingEntityDetailViewModel>("SELECT * FROM dbo.GetCenterEntriesForTradingEntityCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetRejectedEntries(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterTradingEntityDetailViewModel>("SELECT * FROM dbo.GetCenterTradingEntityDetailEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetUnverifiedEntries(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterTradingEntityDetailViewModel>("SELECT * FROM dbo.GetCenterTradingEntityDetailEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetVerifiedEntries(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterTradingEntityDetailViewModel>("SELECT * FROM dbo.GetCenterTradingEntityDetailEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterTradingEntityDetailViewModel> GetViewModelForCreate(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterTradingEntityDetailViewModel>("SELECT * FROM dbo.GetCenterTradingEntityDetailEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterTradingEntityDetailViewModel> GetViewModelForReject(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterTradingEntityDetailViewModel>("SELECT * FROM dbo.GetCenterTradingEntityDetailEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterTradingEntityDetailViewModel> GetViewModelForUnverified(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterTradingEntityDetailViewModel>("SELECT * FROM dbo.GetCenterTradingEntityDetailEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterTradingEntityDetailViewModel> GetViewModelForVerified(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterTradingEntityDetailViewModel>("SELECT * FROM dbo.GetCenterTradingEntityDetailEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(CenterTradingEntityDetailViewModel _centerTradingEntityDetailViewModel)
        {
            try
            {
                List<CenterTradingEntityDetailViewModel> centerTradingEntityDetailViewModelList = new List<CenterTradingEntityDetailViewModel>();
                centerTradingEntityDetailViewModelList = (List<CenterTradingEntityDetailViewModel>)HttpContext.Current.Session["CenterTradingEntityDetail"];

                foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingEntityDetailViewModelList)
                {
                    // Set Dafault Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    CenterTradingEntityDetailMakerChecker centerTradingEntityDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                    //CenterTradingEntityDetailMakerChecker
                    context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingEntityDetailMakerChecker);
                    context.Entry(centerTradingEntityDetailMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Save(CenterTradingEntityDetailViewModel _centerTradingEntityDetailViewModel)
        {
            try
            {
                List<CenterTradingEntityDetailViewModel> centerTradingEntityDetailViewModelList = new List<CenterTradingEntityDetailViewModel>();
                centerTradingEntityDetailViewModelList = (List<CenterTradingEntityDetailViewModel>)HttpContext.Current.Session["CenterTradingEntityDetail"];

                foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingEntityDetailViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.CenterPrmKey = _centerTradingEntityDetailViewModel.CenterPrmKey;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    viewModel.TradingEntityPrmKey = personInformationDetailRepository.GetTradingEntityPrmKeyById(viewModel.TradingEntityId);

                    //Mapping
                    CenterTradingEntityDetail centerTradingEntityDetail = Mapper.Map<CenterTradingEntityDetail>(viewModel);
                    CenterTradingEntityDetailMakerChecker centerTradingEntityDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                    //CenterTradingEntityDetail
                    context.CenterTradingEntityDetails.Attach(centerTradingEntityDetail);
                    context.Entry(centerTradingEntityDetail).State = EntityState.Added;

                    //CenterTradingEntityDetailMakerChecker
                    context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingEntityDetailMakerChecker);
                    context.Entry(centerTradingEntityDetailMakerChecker).State = EntityState.Added;
                    centerTradingEntityDetail.CenterTradingEntityDetailMakerCheckers.Add(centerTradingEntityDetailMakerChecker);

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

        public async Task<bool> Modify(CenterTradingEntityDetailViewModel _centerTradingEntityDetailViewModel)
        {
            try
            {
                // Get Trading Entity Details From Session Object
                List<CenterTradingEntityDetailViewModel> centerTradingEntityDetailViewModelList = new List<CenterTradingEntityDetailViewModel>();
                centerTradingEntityDetailViewModelList = (List<CenterTradingEntityDetailViewModel>)HttpContext.Current.Session["CenterTradingEntityDetail"];

                foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingEntityDetailViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = _centerTradingEntityDetailViewModel.ReasonForModification;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    //Get PrmKey By Id
                    viewModel.TradingEntityPrmKey = personInformationDetailRepository.GetTradingEntityPrmKeyById(viewModel.TradingEntityId);

                    CenterTradingEntityDetail centerTradingEntityDetail = Mapper.Map<CenterTradingEntityDetail>(viewModel);
                    centerTradingEntityDetail.CenterPrmKey = _centerTradingEntityDetailViewModel.CenterPrmKey;

                    CenterTradingEntityDetailMakerChecker centerTradingEntityDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                    context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingEntityDetailMakerChecker);
                    context.Entry(centerTradingEntityDetailMakerChecker).State = EntityState.Added;
                    centerTradingEntityDetail.CenterTradingEntityDetailMakerCheckers.Add(centerTradingEntityDetailMakerChecker);

                    context.CenterTradingEntityDetails.Attach(centerTradingEntityDetail);
                    context.Entry(centerTradingEntityDetail).State = EntityState.Added;
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

        public async Task<bool> Verify(CenterTradingEntityDetailViewModel _centerTradingEntityDetailViewModel)
        {
            try
            {
                IEnumerable<CenterTradingEntityDetailViewModel> tradingEntityDetailViewModelList = await GetVerifiedEntries(_centerTradingEntityDetailViewModel.CenterPrmKey);
                foreach (CenterTradingEntityDetailViewModel viewModel in tradingEntityDetailViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterTradingEntityDetailMakerChecker centerTradingEntityDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                    context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingEntityDetailMakerChecker);
                    context.Entry(centerTradingEntityDetailMakerChecker).State = EntityState.Added;
                }

                List<CenterTradingEntityDetailViewModel> centerTradingEntityDetailViewModelList = new List<CenterTradingEntityDetailViewModel>();
                centerTradingEntityDetailViewModelList = (List<CenterTradingEntityDetailViewModel>)HttpContext.Current.Session["CenterTradingEntityDetail"];

                foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingEntityDetailViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterTradingEntityDetailMakerChecker centerTradingEntityDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                    context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingEntityDetailMakerChecker);
                    context.Entry(centerTradingEntityDetailMakerChecker).State = EntityState.Added;
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
