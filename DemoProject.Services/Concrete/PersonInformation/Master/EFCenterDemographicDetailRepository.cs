using AutoMapper;
using System;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Domain.Entities.PersonInformation.Master;
using DemoProject.Services.Abstract.PersonInformation.Master;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Concrete.PersonInformation.Master
{
    public class EFCenterDemographicDetailRepository : ICenterDemographicDetailRepository
    {
        private readonly EFDbContext context;

        private readonly IPersonDetailRepository personInformationDetailRepository;


        public EFCenterDemographicDetailRepository(RepositoryConnection _connection, IPersonDetailRepository _personInformationDetailRepository)
        {
            context = _connection.EFDbContext;
            personInformationDetailRepository = _personInformationDetailRepository;
        }

        public async Task<bool> Amend(CenterDemographicDetailViewModel _centerDemographicDetailViewModel)
        {
            try
            {
                // Set Default Value
                _centerDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                _centerDemographicDetailViewModel.EntryStatus = StringLiteralValue.Amend;
                _centerDemographicDetailViewModel.ReasonForModification = "None";
                _centerDemographicDetailViewModel.UserAction = StringLiteralValue.Amend;
                _centerDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _centerDemographicDetailViewModel.LocalGovernmentPrmKey = personInformationDetailRepository.GetLocalGovernmentPrmKeyById(_centerDemographicDetailViewModel.LocalGovernmentId);
                _centerDemographicDetailViewModel.DirectionPrmKey = personInformationDetailRepository.GetDirectionPrmKeyById(_centerDemographicDetailViewModel.DirectionId);
                _centerDemographicDetailViewModel.AreaTypePrmKey = personInformationDetailRepository.GetAreaTypePrmKeyById(_centerDemographicDetailViewModel.AreaTypeId);
                _centerDemographicDetailViewModel.EducationLevelPrmKey = personInformationDetailRepository.GetEducationLevelPrmKeyById(_centerDemographicDetailViewModel.EducationLevelId);
                _centerDemographicDetailViewModel.FamilySystemPrmKey = personInformationDetailRepository.GetFamilySystemPrmKeyById(_centerDemographicDetailViewModel.FamilySystemId);

                //Mapping
                CenterDemographicDetail centerDemographicDetail = Mapper.Map<CenterDemographicDetail>(_centerDemographicDetailViewModel);
                CenterDemographicDetailMakerChecker centerDemographicDetailMakerChecker = Mapper.Map<CenterDemographicDetailMakerChecker>(_centerDemographicDetailViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                centerDemographicDetail.PrmKey = _centerDemographicDetailViewModel.CenterDemographicDetailPrmKey;

                //CenterDemographicDetail
                context.CenterDemographicDetails.Attach(centerDemographicDetail);
                context.Entry(centerDemographicDetail).State = EntityState.Modified;

                //CenterDemographicDetailMakerChecker
                context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerChecker);
                context.Entry(centerDemographicDetailMakerChecker).State = EntityState.Added;
                centerDemographicDetail.CenterDemographicDetailMakerCheckers.Add(centerDemographicDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(CenterDemographicDetailViewModel _centerDemographicDetailViewModel)
        {
            try
            {
                // Set Default Value
                _centerDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                _centerDemographicDetailViewModel.UserAction = StringLiteralValue.Delete;
                _centerDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                CenterDemographicDetailMakerChecker centerDemographicDetailMakerChecker = Mapper.Map<CenterDemographicDetailMakerChecker>(_centerDemographicDetailViewModel);

                //CenterDemographicDetailMakerChecker
                context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerChecker);
                context.Entry(centerDemographicDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public short GetPrmKeyById(Guid _centerId)
        {
            return context.Centers
                    .Where(c => c.CenterId == _centerId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<IEnumerable<CenterDemographicDetailViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<CenterDemographicDetailViewModel>("SELECT * FROM dbo.GetCenterEntriesOfDemographicDetail(@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterDemographicDetailViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CenterDemographicDetailViewModel>("SELECT * FROM dbo.GetCenterEntriesOfDemographicDetail ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterDemographicDetailViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CenterDemographicDetailViewModel>("SELECT * FROM dbo.GetCenterEntriesOfDemographicDetail( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CenterDemographicDetailViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CenterDemographicDetailViewModel>("SELECT * FROM dbo.GetCenterEntriesForDemographicDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterDemographicDetailViewModel> GetViewModelForCreate(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterDemographicDetailViewModel>("SELECT * FROM dbo.GetCenterDemographicDetailEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterDemographicDetailViewModel> GetViewModelForReject(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterDemographicDetailViewModel>("SELECT * FROM dbo.GetCenterDemographicDetailEntryByCenterPrmKey ( @CenterPrmkey, @EntriesType)", new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterDemographicDetailViewModel> GetViewModelForUnverified(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterDemographicDetailViewModel>("SELECT * FROM dbo.GetCenterDemographicDetailEntryByCenterPrmKey (@CenterPrmkey, @EntriesType)", new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterDemographicDetailViewModel> GetViewModelForVerified(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterDemographicDetailViewModel>("SELECT * FROM dbo.GetCenterDemographicDetailEntryByCenterPrmKey(@CenterPrmkey, @EntriesType)", new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(CenterDemographicDetailViewModel _centerDemographicDetailViewModel)
        {
            try
            {
                // Set Default Value
                _centerDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                _centerDemographicDetailViewModel.UserAction = StringLiteralValue.Reject;
                _centerDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                CenterDemographicDetailMakerChecker centerDemographicDetailMakerChecker = Mapper.Map<CenterDemographicDetailMakerChecker>(_centerDemographicDetailViewModel);

                //CenterDemographicDetailMakerChecker
                context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerChecker);
                context.Entry(centerDemographicDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(CenterDemographicDetailViewModel _centerDemographicDetailViewModel)
        {
            try
            {
                // Set Default Value
                _centerDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                _centerDemographicDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _centerDemographicDetailViewModel.Remark = "None";
                _centerDemographicDetailViewModel.ReasonForModification = "None";
                _centerDemographicDetailViewModel.UserAction = StringLiteralValue.Create;
                _centerDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _centerDemographicDetailViewModel.LocalGovernmentPrmKey = personInformationDetailRepository.GetLocalGovernmentPrmKeyById(_centerDemographicDetailViewModel.LocalGovernmentId);
                _centerDemographicDetailViewModel.DirectionPrmKey = personInformationDetailRepository.GetDirectionPrmKeyById(_centerDemographicDetailViewModel.DirectionId);
                _centerDemographicDetailViewModel.AreaTypePrmKey = personInformationDetailRepository.GetAreaTypePrmKeyById(_centerDemographicDetailViewModel.AreaTypeId);
                _centerDemographicDetailViewModel.EducationLevelPrmKey = personInformationDetailRepository.GetEducationLevelPrmKeyById(_centerDemographicDetailViewModel.EducationLevelId);
                _centerDemographicDetailViewModel.FamilySystemPrmKey = personInformationDetailRepository.GetFamilySystemPrmKeyById(_centerDemographicDetailViewModel.FamilySystemId);

                //Mapping
                CenterDemographicDetail centerDemographicDetail = Mapper.Map<CenterDemographicDetail>(_centerDemographicDetailViewModel);
                CenterDemographicDetailMakerChecker centerDemographicDetailMakerChecker = Mapper.Map<CenterDemographicDetailMakerChecker>(_centerDemographicDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //CenterDemographicDetail
                context.CenterDemographicDetails.Attach(centerDemographicDetail);
                context.Entry(centerDemographicDetail).State = EntityState.Added;

                //CenterDemographicDetailMakerChecker
                context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerChecker);
                context.Entry(centerDemographicDetailMakerChecker).State = EntityState.Added;
                centerDemographicDetail.CenterDemographicDetailMakerCheckers.Add(centerDemographicDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Modify(CenterDemographicDetailViewModel _centerDemographicDetailViewModel)
        {
            try
            {
                // Set Default Value
                _centerDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                _centerDemographicDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _centerDemographicDetailViewModel.Remark = "None";
                _centerDemographicDetailViewModel.ReasonForModification = "None";
                _centerDemographicDetailViewModel.UserAction = StringLiteralValue.Create;
                _centerDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _centerDemographicDetailViewModel.LocalGovernmentPrmKey = personInformationDetailRepository.GetLocalGovernmentPrmKeyById(_centerDemographicDetailViewModel.LocalGovernmentId);
                _centerDemographicDetailViewModel.DirectionPrmKey = personInformationDetailRepository.GetDirectionPrmKeyById(_centerDemographicDetailViewModel.DirectionId);
                _centerDemographicDetailViewModel.AreaTypePrmKey = personInformationDetailRepository.GetAreaTypePrmKeyById(_centerDemographicDetailViewModel.AreaTypeId);
                _centerDemographicDetailViewModel.EducationLevelPrmKey = personInformationDetailRepository.GetEducationLevelPrmKeyById(_centerDemographicDetailViewModel.EducationLevelId);
                _centerDemographicDetailViewModel.FamilySystemPrmKey = personInformationDetailRepository.GetFamilySystemPrmKeyById(_centerDemographicDetailViewModel.FamilySystemId);

                //Mapping
                CenterDemographicDetail centerDemographicDetail = Mapper.Map<CenterDemographicDetail>(_centerDemographicDetailViewModel);
                CenterDemographicDetailMakerChecker centerDemographicDetailMakerChecker = Mapper.Map<CenterDemographicDetailMakerChecker>(_centerDemographicDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //CenterDemographicDetail
                context.CenterDemographicDetails.Attach(centerDemographicDetail);
                context.Entry(centerDemographicDetail).State = EntityState.Added;

                //CenterDemographicDetailMakerChecker
                context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerChecker);
                context.Entry(centerDemographicDetailMakerChecker).State = EntityState.Added;
                centerDemographicDetail.CenterDemographicDetailMakerCheckers.Add(centerDemographicDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(CenterDemographicDetailViewModel _centerDemographicDetailViewModel)
        {
            try
            {
                // Assign MDF Status To EntryStatus For Performing Modify Operation

                CenterDemographicDetailViewModel centerDemographicDetailViewModelForModify = await GetViewModelForVerified(_centerDemographicDetailViewModel.CenterPrmKey);
                if (centerDemographicDetailViewModelForModify != null)
                {
                    // Set Default Value
                    centerDemographicDetailViewModelForModify.EntryDateTime = DateTime.Now;
                    centerDemographicDetailViewModelForModify.UserAction = StringLiteralValue.Modify;
                    centerDemographicDetailViewModelForModify.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    CenterDemographicDetailMakerChecker centerDemographicDetailMakerCheckerForModify = Mapper.Map<CenterDemographicDetailMakerChecker>(centerDemographicDetailViewModelForModify);

                    //CenterDemographicDetailMakerChecker
                    context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerCheckerForModify);
                    context.Entry(centerDemographicDetailMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify New Record
                // Set Default Value
                _centerDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                _centerDemographicDetailViewModel.UserAction = StringLiteralValue.Verify;
                _centerDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CenterDemographicDetailMakerChecker centerDemographicDetailMakerChecker = Mapper.Map<CenterDemographicDetailMakerChecker>(_centerDemographicDetailViewModel);

                //CenterDemographicDetailMakerCheckers
                context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerChecker);
                context.Entry(centerDemographicDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }


        public async Task<CenterDemographicDetailViewModel> GetRejectedEntry(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterDemographicDetailViewModel>("SELECT * FROM dbo.GetCenterDemographicDetailEntryByCenterPrmKey (@CenterPrmKey, @EntryType)", new SqlParameter("@CenterPrmKey", _centerPrmKey), new SqlParameter("EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterDemographicDetailViewModel> GetUnverifiedEntry(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterDemographicDetailViewModel>("SELECT * FROM dbo.GetCenterDemographicDetailEntryByCenterPrmKey (@CenterPrmKey, @EntryType)", new SqlParameter("@CenterPrmKey", _centerPrmKey), new SqlParameter("EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CenterDemographicDetailViewModel> GetVerifiedEntry(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CenterDemographicDetailViewModel>("SELECT * FROM dbo.GetCenterDemographicDetailEntryByCenterPrmKey (@CenterPrmKey, @EntryType)", new SqlParameter("@CenterPrmKey", _centerPrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
