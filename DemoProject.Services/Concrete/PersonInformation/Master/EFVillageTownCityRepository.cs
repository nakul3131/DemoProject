using AutoMapper;
using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.PersonInformation.Master;
using DemoProject.Services.Abstract.PersonInformation.Master;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Domain.Entities.PersonInformation.Master;

namespace DemoProject.Services.Concrete.PersonInformation.Master
{
    public class EFVillageTownCityRepository : IVillageTownCityRepository
    {
        private readonly EFDbContext context;

        private readonly ICenterDemographicDetailRepository centerDemographicDetailRepository;
        private readonly ICenterOccupationRepository centerOccuptionRepository;
        private readonly ICenterTradingEntityDetailsRepository centerTradingDetailsRepository;
        private readonly IPersonDetailRepository personInformationDetailRepository;
        private readonly ITalukaRepository talukaRepository;

        public EFVillageTownCityRepository(ICenterDemographicDetailRepository _centerDemographicDetailRepository, ICenterOccupationRepository _centerOccuptionRepository, ICenterTradingEntityDetailsRepository _centerTradingDetailsRepository,
            IPersonDetailRepository _personInformationDetailRepository, RepositoryConnection _connection, ITalukaRepository _talukaRepository)
        {
            context = _connection.EFDbContext;
            centerDemographicDetailRepository = _centerDemographicDetailRepository;
            centerOccuptionRepository = _centerOccuptionRepository;
            centerTradingDetailsRepository = _centerTradingDetailsRepository;
            personInformationDetailRepository = _personInformationDetailRepository;
            talukaRepository = _talukaRepository;
        }

        public async Task<bool> Amend(VillageTownCityViewModel _villageTownCityViewModel)
        {
            try
            {
                // Set Default Value
                _villageTownCityViewModel.ActivationStatus = StringLiteralValue.Active;
                _villageTownCityViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.EntryStatus = StringLiteralValue.Amend;
                _villageTownCityViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _villageTownCityViewModel.ReasonForModification = "None";
                _villageTownCityViewModel.UserAction = StringLiteralValue.Amend;
                _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterDemographicDetailViewModel
                _villageTownCityViewModel.CenterDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.EntryStatus = StringLiteralValue.Amend;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.ReasonForModification = "None";
                _villageTownCityViewModel.CenterDemographicDetailViewModel.UserAction = StringLiteralValue.Amend;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.Remark = _villageTownCityViewModel.Remark;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // For Village Set Pincode 0
                if (_villageTownCityViewModel.CenterCategoryPrmKey == 1)
                {
                    _villageTownCityViewModel.CenterDemographicDetailViewModel.Pincode = 123456;
                    _villageTownCityViewModel.ParentCenterPrmKey = personInformationDetailRepository.GetCenterPrmKeyById(_villageTownCityViewModel.ParentCenterPostId);
                }

                // For City & Town
                if (_villageTownCityViewModel.CenterCategoryPrmKey == 2 || _villageTownCityViewModel.CenterCategoryPrmKey == 3)
                {
                    _villageTownCityViewModel.ParentCenterPrmKey = personInformationDetailRepository.GetCenterPrmKeyById(_villageTownCityViewModel.ParentCenterTalukaId);
                }

                // Get PrmKey By Id Of All Dropdowns
                _villageTownCityViewModel.CenterDemographicDetailViewModel.AreaTypePrmKey = personInformationDetailRepository.GetAreaTypePrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.AreaTypeId);
                _villageTownCityViewModel.CenterDemographicDetailViewModel.DirectionPrmKey = personInformationDetailRepository.GetDirectionPrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.DirectionId);
                _villageTownCityViewModel.CenterDemographicDetailViewModel.EducationLevelPrmKey = personInformationDetailRepository.GetEducationLevelPrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.EducationLevelId);
                _villageTownCityViewModel.CenterDemographicDetailViewModel.FamilySystemPrmKey = personInformationDetailRepository.GetFamilySystemPrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.FamilySystemId);
                _villageTownCityViewModel.CenterDemographicDetailViewModel.LocalGovernmentPrmKey = personInformationDetailRepository.GetLocalGovernmentPrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.LocalGovernmentId);

                // Set ReferenceKey As PrmKey To Normal Tables
                _villageTownCityViewModel.CenterDemographicDetailViewModel.CenterPrmKey = _villageTownCityViewModel.CenterPrmKey;

                // Mapping 
                // Center
                Center centerForAmend = Mapper.Map<Center>(_villageTownCityViewModel);
                CenterMakerChecker centerMakerCheckerForAmend = Mapper.Map<CenterMakerChecker>(_villageTownCityViewModel);

                // CenterModification
                CenterModification centerModificationForAmend = Mapper.Map<CenterModification>(_villageTownCityViewModel);
                CenterModificationMakerChecker centerModificationMakerCheckerForAmend = Mapper.Map<CenterModificationMakerChecker>(_villageTownCityViewModel);

                // CenterTranslation
                CenterTranslation centerTranslationForAmend = Mapper.Map<CenterTranslation>(_villageTownCityViewModel);
                CenterTranslationMakerChecker centerTranslationMakerCheckerForAmend = Mapper.Map<CenterTranslationMakerChecker>(_villageTownCityViewModel);

                // CenterDemographicDetail
                CenterDemographicDetail centerDemographicDetailForAmend = Mapper.Map<CenterDemographicDetail>(_villageTownCityViewModel.CenterDemographicDetailViewModel);
                CenterDemographicDetailMakerChecker centerDemographicDetailMakerCheckerForAmend = Mapper.Map<CenterDemographicDetailMakerChecker>(_villageTownCityViewModel.CenterDemographicDetailViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                centerForAmend.PrmKey = _villageTownCityViewModel.CenterPrmKey;
                centerModificationForAmend.PrmKey = _villageTownCityViewModel.CenterModificationPrmKey;
                centerTranslationForAmend.PrmKey = _villageTownCityViewModel.CenterTranslationPrmKey;
                centerDemographicDetailForAmend.PrmKey = _villageTownCityViewModel.CenterDemographicDetailViewModel.CenterDemographicDetailPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_villageTownCityViewModel.CenterModificationPrmKey == 0)
                {
                    // Center
                    context.CenterMakerCheckers.Attach(centerMakerCheckerForAmend);
                    context.Entry(centerMakerCheckerForAmend).State = EntityState.Added;
                    centerForAmend.CenterMakerCheckers.Add(centerMakerCheckerForAmend);

                    context.Centers.Attach(centerForAmend);
                    context.Entry(centerForAmend).State = EntityState.Modified;
                }
                else
                {
                    // Center Modification 
                    context.CenterModificationMakerCheckers.Attach(centerModificationMakerCheckerForAmend);
                    context.Entry(centerModificationMakerCheckerForAmend).State = EntityState.Added;
                    centerModificationForAmend.CenterModificationMakerCheckers.Add(centerModificationMakerCheckerForAmend);

                    context.CenterModifications.Attach(centerModificationForAmend);
                    context.Entry(centerModificationForAmend).State = EntityState.Modified;
                }

                // CenterTranslation
                context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerCheckerForAmend);
                context.Entry(centerTranslationMakerCheckerForAmend).State = EntityState.Added;
                centerTranslationForAmend.CenterTranslationMakerCheckers.Add(centerTranslationMakerCheckerForAmend);

                context.CenterTranslations.Attach(centerTranslationForAmend);
                context.Entry(centerTranslationForAmend).State = EntityState.Modified;

                // CenterDemographicDetail
                context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerCheckerForAmend);
                context.Entry(centerDemographicDetailMakerCheckerForAmend).State = EntityState.Added;
                centerDemographicDetailForAmend.CenterDemographicDetailMakerCheckers.Add(centerDemographicDetailMakerCheckerForAmend);

                context.CenterDemographicDetails.Attach(centerDemographicDetailForAmend);
                context.Entry(centerDemographicDetailForAmend).State = EntityState.Modified;

                if (_villageTownCityViewModel.IsMandatoryCenterISOCode == "O" || _villageTownCityViewModel.IsMandatoryCenterISOCode == "M")
                {
                    // CenterISOCode
                    CenterISOCode centerISOCodeForAmend = Mapper.Map<CenterISOCode>(_villageTownCityViewModel.CenterIsoCodeViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerCheckerForAmend = Mapper.Map<CenterISOCodeMakerChecker>(_villageTownCityViewModel.CenterIsoCodeViewModel);

                    centerISOCodeForAmend.PrmKey = _villageTownCityViewModel.CenterIsoCodeViewModel.CenterISOCodePrmKey;

                    // CenterISOCode
                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerCheckerForAmend);
                    context.Entry(centerISOCodeMakerCheckerForAmend).State = EntityState.Added;
                    centerISOCodeForAmend.CenterISOCodeMakerCheckers.Add(centerISOCodeMakerCheckerForAmend);

                    context.CenterISOCodes.Attach(centerISOCodeForAmend);
                    context.Entry(centerISOCodeForAmend).State = EntityState.Modified;
                }

                // CenterOccupation - Amend Old Record
                IEnumerable<CenterOccupationViewModel> centerOccupationListForAmend = await centerOccuptionRepository.GetRejectedEntries(_villageTownCityViewModel.CenterPrmKey);

                if (_villageTownCityViewModel.SelectedOccupationId != null)
                {
                    foreach (CenterOccupationViewModel centerOccuptionViewModel in centerOccupationListForAmend)
                    {
                        centerOccuptionViewModel.EntryStatus = StringLiteralValue.Amend;
                        centerOccuptionViewModel.UserAction = StringLiteralValue.Amend;
                        centerOccuptionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterOccupationMakerChecker centerOccupationMakerCheckerForAmend = Mapper.Map<CenterOccupationMakerChecker>(centerOccuptionViewModel);

                        context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerCheckerForAmend);
                        context.Entry(centerOccupationMakerCheckerForAmend).State = EntityState.Added;
                    }

                    // CenterOccupation - Insert New Added Or Amended Occupation (Because Of MultiSelect Session Object Not Required)
                    foreach (Guid categoryId in _villageTownCityViewModel.SelectedOccupationId)
                    {
                        _villageTownCityViewModel.CenterOccupationViewModel.CenterOccupationPrmKey = 0;
                        _villageTownCityViewModel.EntryDateTime = DateTime.Now;
                        _villageTownCityViewModel.EntryStatus = StringLiteralValue.Create;
                        _villageTownCityViewModel.CenterOccupationViewModel.OccupationPrmKey = personInformationDetailRepository.GetOccupationPrmKeyById(categoryId);
                        _villageTownCityViewModel.ReasonForModification = "None";
                        _villageTownCityViewModel.UserAction = StringLiteralValue.Create;
                        _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterOccupation centerOccupation = Mapper.Map<CenterOccupation>(_villageTownCityViewModel);

                        CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(_villageTownCityViewModel);

                        context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                        context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
                        centerOccupation.CenterOccupationMakerCheckers.Add(centerOccupationMakerChecker);

                        context.CenterOccupations.Attach(centerOccupation);
                        context.Entry(centerOccupation).State = EntityState.Added;
                    }
                }

                // CenterTradingEntity - Amend Old Record
                IEnumerable<CenterTradingEntityDetailViewModel> centerTradingEntityDetailViewModelForAmend = await centerTradingDetailsRepository.GetRejectedEntries(_villageTownCityViewModel.CenterPrmKey);

                foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingEntityDetailViewModelForAmend)
                {
                    viewModel.EntryStatus = StringLiteralValue.Amend;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterTradingEntityDetailMakerChecker centerTradingEntityDetailMakerCheckerForAmend = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                    context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingEntityDetailMakerCheckerForAmend);
                    context.Entry(centerTradingEntityDetailMakerCheckerForAmend).State = EntityState.Added;
                }

                // CenterTradingEntity - Add New Amended Entry, Get CenterTradingEntity Details From Session Object
                List<CenterTradingEntityDetailViewModel> centerTradingDetailViewModelList = new List<CenterTradingEntityDetailViewModel>();

                centerTradingDetailViewModelList = (List<CenterTradingEntityDetailViewModel>)HttpContext.Current.Session["CenterTradingEntityDetail"];

                if (centerTradingDetailViewModelList != null)
                {
                    foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingDetailViewModelList)
                    {
                        viewModel.CenterTradingEntityDetailPrmKey = 0;
                        viewModel.CenterPrmKey = _villageTownCityViewModel.CenterPrmKey;
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.EntryStatus = StringLiteralValue.Create;
                        viewModel.ReasonForModification = "None";
                        viewModel.Remark = _villageTownCityViewModel.Remark;
                        viewModel.TradingEntityPrmKey = personInformationDetailRepository.GetTradingEntityPrmKeyById(viewModel.TradingEntityId);
                        viewModel.UserAction = StringLiteralValue.Create;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterTradingEntityDetail centerTradingDetail = Mapper.Map<CenterTradingEntityDetail>(viewModel);

                        CenterTradingEntityDetailMakerChecker centerTradingDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                        context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingDetailMakerChecker);
                        context.Entry(centerTradingDetailMakerChecker).State = EntityState.Added;
                        centerTradingDetail.CenterTradingEntityDetailMakerCheckers.Add(centerTradingDetailMakerChecker);

                        context.CenterTradingEntityDetails.Attach(centerTradingDetail);
                        context.Entry(centerTradingDetail).State = EntityState.Added;
                    }
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

        public async Task<bool> Delete(VillageTownCityViewModel _villageTownCityViewModel)
        {
            try
            {
                // Set Default Value
                _villageTownCityViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.UserAction = StringLiteralValue.Delete;
                _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterDemographicDetailViewModel
                _villageTownCityViewModel.CenterDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.Remark = _villageTownCityViewModel.Remark;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.UserAction = StringLiteralValue.Delete;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_villageTownCityViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_villageTownCityViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_villageTownCityViewModel);
                CenterDemographicDetailMakerChecker centerDemographicDetailMakerChecker = Mapper.Map<CenterDemographicDetailMakerChecker>(_villageTownCityViewModel.CenterDemographicDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_villageTownCityViewModel.CenterModificationPrmKey == 0)
                {
                    // Center
                    context.CenterMakerCheckers.Attach(centerMakerChecker);
                    context.Entry(centerMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // CenterModification  
                    context.CenterModificationMakerCheckers.Attach(centerModificationMakerChecker);
                    context.Entry(centerModificationMakerChecker).State = EntityState.Added;
                }

                // CenterTranslation
                context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                context.Entry(centerTranslationMakerChecker).State = EntityState.Added;

                // CenterDemographicDetail
                context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerChecker);
                context.Entry(centerDemographicDetailMakerChecker).State = EntityState.Added;

                // CenterOccupation
                IEnumerable<CenterOccupationViewModel> centerOccupationList = await centerOccuptionRepository.GetRejectedEntries(_villageTownCityViewModel.CenterPrmKey);

                foreach (CenterOccupationViewModel centerOccuptionViewModel in centerOccupationList)
                {
                    centerOccuptionViewModel.EntryDateTime = DateTime.Now;
                    centerOccuptionViewModel.ReasonForModification = "None";
                    centerOccuptionViewModel.UserAction = StringLiteralValue.Delete;
                    centerOccuptionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(centerOccuptionViewModel);

                    context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                    context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
                }

                // CenterTradingEntityDetail
                List<CenterTradingEntityDetailViewModel> centerTradingDetailViewModelList = new List<CenterTradingEntityDetailViewModel>();

                centerTradingDetailViewModelList = (List<CenterTradingEntityDetailViewModel>)HttpContext.Current.Session["CenterTradingEntityDetail"];

                foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingDetailViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.ReasonForModification = "None";
                    viewModel.Remark = _villageTownCityViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterTradingEntityDetailMakerChecker centerTradingDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                    context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingDetailMakerChecker);
                    context.Entry(centerTradingDetailMakerChecker).State = EntityState.Added;
                }

                // CenterISOCode
                if (_villageTownCityViewModel.IsMandatoryCenterISOCode == "O" || _villageTownCityViewModel.IsMandatoryCenterISOCode == "M")
                {
                    // Set Default Value
                    _villageTownCityViewModel.EntryDateTime = DateTime.Now;
                    _villageTownCityViewModel.UserAction = StringLiteralValue.Delete;
                    _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_villageTownCityViewModel);

                    // CenterISOCode
                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                    context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;
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

        public async Task<IEnumerable<CenterIndexViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetVillageTownCityEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<CenterIndexViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetVillageTownCityEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<CenterIndexViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetVillageTownCityEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<VillageTownCityViewModel> GetRejectedEntry(Guid _centerId)
        {
            VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel();
            try
            {
                villageViewModel = await context.Database.SqlQuery<VillageTownCityViewModel>("SELECT * FROM dbo.GetVillageTownCityEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();

                short prmKey = personInformationDetailRepository.GetCenterPrmKeyById(_centerId);

                IEnumerable<CenterOccupationViewModel> centerOccupationList = await centerOccuptionRepository.GetRejectedEntries(prmKey);

                short i = 0;
                Guid[] a = new Guid[50];

                foreach (CenterOccupationViewModel centerOccuptionViewModel in centerOccupationList)
                {
                    a[i] = centerOccuptionViewModel.OccupationId;

                    i++;
                }

                villageViewModel.SelectedOccupationId = a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
            return villageViewModel;
        }

        public bool GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey)
        {
            bool status;
            if (context.Centers.Where(p => p.NameOfCenter == NameOfCenter && p.CenterCategoryPrmKey == CenterCategoryPrmKey).Select(p => p.PrmKey).FirstOrDefault() > 0)
            {
                // Already registered  
                status = false;
            }
            else
            {
                // Available to use  
                status = true;
            }

            return status;

        }

        public async Task<VillageTownCityViewModel> GetUnVerifiedEntry(Guid _centerId)
        {
            VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel();

            try
            {
                villageViewModel = await context.Database.SqlQuery<VillageTownCityViewModel>("SELECT * FROM dbo.GetVillageTownCityEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();

                short prmKey = personInformationDetailRepository.GetCenterPrmKeyById(_centerId);

                IEnumerable<CenterOccupationViewModel> centerOccupationList = await centerOccuptionRepository.GetUnverifiedEntries(prmKey);

                short i = 0;
                Guid[] a = new Guid[50];

                foreach (CenterOccupationViewModel centerOccuptionViewModel in centerOccupationList)
                {

                    a[i] = centerOccuptionViewModel.OccupationId;

                    i++;
                }

                villageViewModel.SelectedOccupationId = a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }

            return villageViewModel;
        }

        public async Task<VillageTownCityViewModel> GetVerifiedEntry(Guid _centerId)
        {
            VillageTownCityViewModel villageViewModel = new VillageTownCityViewModel();

            try
            {
                villageViewModel = await context.Database.SqlQuery<VillageTownCityViewModel>("SELECT * FROM dbo.GetVillageTownCityEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();

                short prmKey = personInformationDetailRepository.GetCenterPrmKeyById(_centerId);

                IEnumerable<CenterOccupationViewModel> centerOccupationList = await centerOccuptionRepository.GetVerifiedEntries(prmKey);

                short i = 0;
                Guid[] a = new Guid[50];

                foreach (CenterOccupationViewModel centerOccuptionViewModel in centerOccupationList)
                {

                    a[i] = centerOccuptionViewModel.OccupationId;

                    i++;
                }

                villageViewModel.SelectedOccupationId = a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }

            return villageViewModel;
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(VillageTownCityViewModel _villageTownCityViewModel)
        {
            try
            {
                // Set Default Value
                _villageTownCityViewModel.CenterTranslationPrmKey = 0;
                _villageTownCityViewModel.CenterModificationPrmKey = 0;
                _villageTownCityViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.EntryStatus = StringLiteralValue.Create;
                _villageTownCityViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _villageTownCityViewModel.Remark = "None";
                _villageTownCityViewModel.UserAction = StringLiteralValue.Create;
                _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterDemographicDetailViewModel
                _villageTownCityViewModel.CenterDemographicDetailViewModel.CenterDemographicDetailPrmKey = 0;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.ReasonForModification = _villageTownCityViewModel.ReasonForModification;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.Remark = "None";
                _villageTownCityViewModel.CenterDemographicDetailViewModel.UserAction = StringLiteralValue.Create;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _villageTownCityViewModel.CenterDemographicDetailViewModel.CenterPrmKey = _villageTownCityViewModel.CenterPrmKey;

                // For Village Set Pincode 0
                if (_villageTownCityViewModel.CenterCategoryPrmKey == 1)
                {
                    _villageTownCityViewModel.CenterDemographicDetailViewModel.Pincode = 123456;
                    _villageTownCityViewModel.ParentCenterPrmKey = personInformationDetailRepository.GetCenterPrmKeyById(_villageTownCityViewModel.ParentCenterPostId);
                }

                // For City & Town
                if (_villageTownCityViewModel.CenterCategoryPrmKey == 2 || _villageTownCityViewModel.CenterCategoryPrmKey == 3)
                {
                    _villageTownCityViewModel.ParentCenterPrmKey = personInformationDetailRepository.GetCenterPrmKeyById(_villageTownCityViewModel.ParentCenterTalukaId);
                }

                // Get PrmKey By Id Of All Dropdowns
                _villageTownCityViewModel.CenterDemographicDetailViewModel.AreaTypePrmKey = personInformationDetailRepository.GetAreaTypePrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.AreaTypeId);
                _villageTownCityViewModel.CenterDemographicDetailViewModel.DirectionPrmKey = personInformationDetailRepository.GetDirectionPrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.DirectionId);
                _villageTownCityViewModel.CenterDemographicDetailViewModel.EducationLevelPrmKey = personInformationDetailRepository.GetEducationLevelPrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.EducationLevelId);
                _villageTownCityViewModel.CenterDemographicDetailViewModel.FamilySystemPrmKey = personInformationDetailRepository.GetFamilySystemPrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.FamilySystemId);
                _villageTownCityViewModel.CenterDemographicDetailViewModel.LocalGovernmentPrmKey = personInformationDetailRepository.GetLocalGovernmentPrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.LocalGovernmentId);

                // CenterModification
                CenterModification centerModification = Mapper.Map<CenterModification>(_villageTownCityViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_villageTownCityViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_villageTownCityViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_villageTownCityViewModel);

                // CenterDemographicDetail
                CenterDemographicDetail centerDemographicDetail = Mapper.Map<CenterDemographicDetail>(_villageTownCityViewModel.CenterDemographicDetailViewModel);
                CenterDemographicDetailMakerChecker centerDemographicDetailMakerChecker = Mapper.Map<CenterDemographicDetailMakerChecker>(_villageTownCityViewModel.CenterDemographicDetailViewModel);

                // CenterOccupation
                if (_villageTownCityViewModel.SelectedOccupationId != null)
                {
                    foreach (Guid categoryId in _villageTownCityViewModel.SelectedOccupationId)
                    {
                        _villageTownCityViewModel.EntryDateTime = DateTime.Now;
                        _villageTownCityViewModel.EntryStatus = StringLiteralValue.Create;
                        _villageTownCityViewModel.ReasonForModification = _villageTownCityViewModel.ReasonForModification;
                        _villageTownCityViewModel.Remark = "None";
                        _villageTownCityViewModel.UserAction = StringLiteralValue.Create;
                        _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterOccupation centerOccupation = Mapper.Map<CenterOccupation>(_villageTownCityViewModel);
                        CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(_villageTownCityViewModel);

                        // Set ReferenceKey As PrmKey To Every Object
                        centerOccupation.OccupationPrmKey = personInformationDetailRepository.GetOccupationPrmKeyById(categoryId);

                        context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                        context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
                        centerOccupation.CenterOccupationMakerCheckers.Add(centerOccupationMakerChecker);

                        context.CenterOccupations.Attach(centerOccupation);
                        context.Entry(centerOccupation).State = EntityState.Added;
                    }
                }

                // CenterTradingEntityDetail
                List<CenterTradingEntityDetailViewModel> centerTradingEntityDetailViewModelList = new List<CenterTradingEntityDetailViewModel>();

                centerTradingEntityDetailViewModelList = (List<CenterTradingEntityDetailViewModel>)HttpContext.Current.Session["CenterTradingEntityDetail"];

                List<CenterTradingEntityDetail> centerTradingEntityDetailList = new List<CenterTradingEntityDetail>();

                if (centerTradingEntityDetailViewModelList != null)
                {
                    foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingEntityDetailViewModelList)
                    {
                        viewModel.CenterPrmKey = _villageTownCityViewModel.CenterPrmKey;
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.EntryStatus = StringLiteralValue.Create;
                        viewModel.ReasonForModification = _villageTownCityViewModel.ReasonForModification;
                        viewModel.Remark = "None";
                        viewModel.UserAction = StringLiteralValue.Create;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterTradingEntityDetail centerTradingEntityDetail = Mapper.Map<CenterTradingEntityDetail>(viewModel);
                        CenterTradingEntityDetailMakerChecker centerTradingEntityDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                        // Set ReferenceKey As PrmKey To Every Object
                        centerTradingEntityDetail.TradingEntityPrmKey = personInformationDetailRepository.GetTradingEntityPrmKeyById(viewModel.TradingEntityId);

                        context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingEntityDetailMakerChecker);
                        context.Entry(centerTradingEntityDetailMakerChecker).State = EntityState.Added;
                        centerTradingEntityDetail.CenterTradingEntityDetailMakerCheckers.Add(centerTradingEntityDetailMakerChecker);

                        context.CenterTradingEntityDetails.Attach(centerTradingEntityDetail);
                        context.Entry(centerTradingEntityDetail).State = EntityState.Added;
                    }
                }

                // CenterModification
                context.CenterModificationMakerCheckers.Attach(centerModificationMakerChecker);
                context.Entry(centerModificationMakerChecker).State = EntityState.Added;
                centerModification.CenterModificationMakerCheckers.Add(centerModificationMakerChecker);

                context.CenterModifications.Attach(centerModification);
                context.Entry(centerModification).State = EntityState.Added;

                // CenterTranslation
                context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                context.Entry(centerTranslationMakerChecker).State = EntityState.Added;
                centerTranslation.CenterTranslationMakerCheckers.Add(centerTranslationMakerChecker);

                context.CenterTranslations.Attach(centerTranslation);
                context.Entry(centerTranslation).State = EntityState.Added;

                // CenterDemographicDetail
                context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerChecker);
                context.Entry(centerDemographicDetailMakerChecker).State = EntityState.Added;
                centerDemographicDetail.CenterDemographicDetailMakerCheckers.Add(centerDemographicDetailMakerChecker);

                context.CenterDemographicDetails.Attach(centerDemographicDetail);
                context.Entry(centerDemographicDetail).State = EntityState.Added;

                // CenterISOCode
                if (_villageTownCityViewModel.IsMandatoryCenterISOCode == "O" || _villageTownCityViewModel.IsMandatoryCenterISOCode == "M")
                {
                    CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_villageTownCityViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_villageTownCityViewModel);

                    // CenterISOCode
                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                    context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;
                    centerISOCode.CenterISOCodeMakerCheckers.Add(centerISOCodeMakerChecker);

                    context.CenterISOCodes.Attach(centerISOCode);
                    context.Entry(centerISOCode).State = EntityState.Added;
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

        public async Task<bool> Reject(VillageTownCityViewModel _villageTownCityViewModel)
        {
            try
            {
                // Set Default Value
                _villageTownCityViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.UserAction = StringLiteralValue.Reject;
                _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterDemographicDetailViewModel
                _villageTownCityViewModel.CenterDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.UserAction = StringLiteralValue.Reject;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _villageTownCityViewModel.CenterDemographicDetailViewModel.Remark = _villageTownCityViewModel.Remark;

                // Mapping
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_villageTownCityViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_villageTownCityViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_villageTownCityViewModel);
                CenterDemographicDetailMakerChecker centerDemographicDetailMakerChecker = Mapper.Map<CenterDemographicDetailMakerChecker>(_villageTownCityViewModel.CenterDemographicDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_villageTownCityViewModel.CenterModificationPrmKey == 0)
                {
                    // CenterMakerChecker
                    context.CenterMakerCheckers.Attach(centerMakerChecker);
                    context.Entry(centerMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // CenterModificationMakerChecker
                    context.CenterModificationMakerCheckers.Attach(centerModificationMakerChecker);
                    context.Entry(centerModificationMakerChecker).State = EntityState.Added;
                }

                // CenterTranslationMakerChecker
                context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                context.Entry(centerTranslationMakerChecker).State = EntityState.Added;

                // CenterDemographicDetail
                context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerChecker);
                context.Entry(centerDemographicDetailMakerChecker).State = EntityState.Added;

                // CenterOccupation
                IEnumerable<CenterOccupationViewModel> centerOccupationList = await centerOccuptionRepository.GetUnverifiedEntries(_villageTownCityViewModel.CenterPrmKey);

                foreach (CenterOccupationViewModel centerOccuptionViewModel in centerOccupationList)
                {
                    centerOccuptionViewModel.PrmKey = 0;
                    centerOccuptionViewModel.EntryDateTime = DateTime.Now;
                    centerOccuptionViewModel.Remark = _villageTownCityViewModel.Remark;
                    centerOccuptionViewModel.UserAction = StringLiteralValue.Reject;
                    centerOccuptionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(centerOccuptionViewModel);

                    context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                    context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
                }

                // CenterTradingEntityDetail
                List<CenterTradingEntityDetailViewModel> centerTradingDetailViewModelList = new List<CenterTradingEntityDetailViewModel>();

                centerTradingDetailViewModelList = (List<CenterTradingEntityDetailViewModel>)HttpContext.Current.Session["CenterTradingEntityDetail"];

                foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingDetailViewModelList)
                {
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _villageTownCityViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterTradingEntityDetailMakerChecker centerTradingDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                    context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingDetailMakerChecker);
                    context.Entry(centerTradingDetailMakerChecker).State = EntityState.Added;
                }

                // CenterISOCode
                if (_villageTownCityViewModel.IsMandatoryCenterISOCode == "O" || _villageTownCityViewModel.IsMandatoryCenterISOCode == "M")
                {
                    // Set Default Value
                    _villageTownCityViewModel.EntryDateTime = DateTime.Now;
                    _villageTownCityViewModel.UserAction = StringLiteralValue.Reject;
                    _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_villageTownCityViewModel);

                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                    context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;
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

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Save(VillageTownCityViewModel _villageTownCityViewModel)
        {
            try
            {
                // Set Default Value
                _villageTownCityViewModel.ActivationStatus = StringLiteralValue.Active;
                _villageTownCityViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.EntryStatus = StringLiteralValue.Create;
                _villageTownCityViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _villageTownCityViewModel.ReasonForModification = "None";
                _villageTownCityViewModel.Remark = "None";
                _villageTownCityViewModel.UserAction = StringLiteralValue.Create;
                _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterDemographicDetailViewModel
                _villageTownCityViewModel.CenterDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.ReasonForModification = "None";
                _villageTownCityViewModel.CenterDemographicDetailViewModel.Remark = "None";
                _villageTownCityViewModel.CenterDemographicDetailViewModel.UserAction = StringLiteralValue.Create;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _villageTownCityViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Create;
                _villageTownCityViewModel.CenterIsoCodeViewModel.Remark = "None";
                _villageTownCityViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Create;
                _villageTownCityViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterTradingEntityDetailViewModel
                _villageTownCityViewModel.CenterTradingEntityDetailViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.CenterTradingEntityDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _villageTownCityViewModel.CenterTradingEntityDetailViewModel.ReasonForModification = "None";
                _villageTownCityViewModel.CenterTradingEntityDetailViewModel.Remark = "None";
                _villageTownCityViewModel.CenterTradingEntityDetailViewModel.UserAction = StringLiteralValue.Create;
                _villageTownCityViewModel.CenterTradingEntityDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // For Village Set Pincode 0
                if (_villageTownCityViewModel.CenterCategoryPrmKey == 1)
                {
                    _villageTownCityViewModel.CenterDemographicDetailViewModel.Pincode = 123456;
                    _villageTownCityViewModel.ParentCenterPrmKey = personInformationDetailRepository.GetCenterPrmKeyById(_villageTownCityViewModel.ParentCenterPostId);
                }

                // For City & Town
                if (_villageTownCityViewModel.CenterCategoryPrmKey == 2 || _villageTownCityViewModel.CenterCategoryPrmKey == 3)
                {
                    _villageTownCityViewModel.ParentCenterPrmKey = personInformationDetailRepository.GetCenterPrmKeyById(_villageTownCityViewModel.ParentCenterTalukaId);
                }

                // Get PrmKey By Id Of All Dropdowns
                _villageTownCityViewModel.CenterDemographicDetailViewModel.AreaTypePrmKey = personInformationDetailRepository.GetAreaTypePrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.AreaTypeId);
                _villageTownCityViewModel.CenterDemographicDetailViewModel.DirectionPrmKey = personInformationDetailRepository.GetDirectionPrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.DirectionId);
                _villageTownCityViewModel.CenterDemographicDetailViewModel.EducationLevelPrmKey = personInformationDetailRepository.GetEducationLevelPrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.EducationLevelId);
                _villageTownCityViewModel.CenterDemographicDetailViewModel.FamilySystemPrmKey = personInformationDetailRepository.GetFamilySystemPrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.FamilySystemId);
                _villageTownCityViewModel.CenterDemographicDetailViewModel.LocalGovernmentPrmKey = personInformationDetailRepository.GetLocalGovernmentPrmKeyById(_villageTownCityViewModel.CenterDemographicDetailViewModel.LocalGovernmentId);
                //_villageTownCityViewModel.ParentCenterPrmKey = GetPrmKeyById(_villageTownCityViewModel.ParentCenterPostId);

                // Mapping
                // Center
                Center center = Mapper.Map<Center>(_villageTownCityViewModel);
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_villageTownCityViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_villageTownCityViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_villageTownCityViewModel);

                // CenterDemographicDetail
                CenterDemographicDetail centerDemographicDetail = Mapper.Map<CenterDemographicDetail>(_villageTownCityViewModel.CenterDemographicDetailViewModel);
                CenterDemographicDetailMakerChecker centerDemographicDetailMakerChecker = Mapper.Map<CenterDemographicDetailMakerChecker>(_villageTownCityViewModel.CenterDemographicDetailViewModel);

                // CenterOccupation 
                if (_villageTownCityViewModel.SelectedOccupationId != null)
                {
                    foreach (Guid occupationId in _villageTownCityViewModel.SelectedOccupationId)
                    {
                        _villageTownCityViewModel.EntryDateTime = DateTime.Now;
                        _villageTownCityViewModel.EntryStatus = StringLiteralValue.Create;
                        _villageTownCityViewModel.ReasonForModification = "None";
                        _villageTownCityViewModel.Remark = "None";
                        _villageTownCityViewModel.UserAction = StringLiteralValue.Create;
                        _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterOccupation centerOccupation = Mapper.Map<CenterOccupation>(_villageTownCityViewModel);
                        centerOccupation.OccupationPrmKey = personInformationDetailRepository.GetOccupationPrmKeyById(occupationId);

                        CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(_villageTownCityViewModel);

                        context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                        context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
                        centerOccupation.CenterOccupationMakerCheckers.Add(centerOccupationMakerChecker);

                        context.CenterOccupations.Attach(centerOccupation);
                        context.Entry(centerOccupation).State = EntityState.Added;

                        center.CenterOccupations.Add(centerOccupation);
                    }
                }

                // CenterTradingEntityDetail
                List<CenterTradingEntityDetailViewModel> centerTradingEntityDetailViewModelList = new List<CenterTradingEntityDetailViewModel>();

                centerTradingEntityDetailViewModelList = (List<CenterTradingEntityDetailViewModel>)HttpContext.Current.Session["CenterTradingEntityDetail"];

                List<CenterTradingEntityDetail> centerTradingEntityDetailList = new List<CenterTradingEntityDetail>();

                if (centerTradingEntityDetailViewModelList != null)
                {
                    foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingEntityDetailViewModelList)
                    {
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.EntryStatus = StringLiteralValue.Create;
                        viewModel.ReasonForModification = "None";
                        viewModel.Remark = _villageTownCityViewModel.Remark;
                        viewModel.UserAction = StringLiteralValue.Create;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                        viewModel.CenterPrmKey = _villageTownCityViewModel.CenterPrmKey;

                        CenterTradingEntityDetail centerTradingEntityDetail = Mapper.Map<CenterTradingEntityDetail>(viewModel);
                        centerTradingEntityDetail.TradingEntityPrmKey = personInformationDetailRepository.GetTradingEntityPrmKeyById(viewModel.TradingEntityId);

                        CenterTradingEntityDetailMakerChecker centerTradingEntityDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                        context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingEntityDetailMakerChecker);
                        context.Entry(centerTradingEntityDetailMakerChecker).State = EntityState.Added;
                        centerTradingEntityDetail.CenterTradingEntityDetailMakerCheckers.Add(centerTradingEntityDetailMakerChecker);

                        context.CenterTradingEntityDetails.Attach(centerTradingEntityDetail);
                        context.Entry(centerTradingEntityDetail).State = EntityState.Added;

                        center.CenterTradingEntityDetails.Add(centerTradingEntityDetail);
                    }
                }

                // Center
                context.CenterMakerCheckers.Attach(centerMakerChecker);
                context.Entry(centerMakerChecker).State = EntityState.Added;
                center.CenterMakerCheckers.Add(centerMakerChecker);

                context.Centers.Attach(center);
                context.Entry(center).State = EntityState.Added;

                // CenterTranslation
                context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                context.Entry(centerTranslationMakerChecker).State = EntityState.Added;
                centerTranslation.CenterTranslationMakerCheckers.Add(centerTranslationMakerChecker);

                context.CenterTranslations.Attach(centerTranslation);
                context.Entry(centerTranslation).State = EntityState.Added;
                center.CenterTranslations.Add(centerTranslation);

                // CenterDemographicDetail
                context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerChecker);
                context.Entry(centerDemographicDetailMakerChecker).State = EntityState.Added;
                centerDemographicDetail.CenterDemographicDetailMakerCheckers.Add(centerDemographicDetailMakerChecker);

                context.CenterDemographicDetails.Attach(centerDemographicDetail);
                context.Entry(centerDemographicDetail).State = EntityState.Added;
                center.CenterDemographicDetails.Add(centerDemographicDetail);

                // CenterISOCode
                if (_villageTownCityViewModel.IsMandatoryCenterISOCode == "O" || _villageTownCityViewModel.IsMandatoryCenterISOCode == "M")
                {
                    CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_villageTownCityViewModel.CenterIsoCodeViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_villageTownCityViewModel.CenterIsoCodeViewModel);

                    // CenterISOCode
                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                    context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;
                    centerISOCode.CenterISOCodeMakerCheckers.Add(centerISOCodeMakerChecker);

                    context.CenterISOCodes.Attach(centerISOCode);
                    context.Entry(centerISOCode).State = EntityState.Added;
                    center.CenterISOCodes.Add(centerISOCode);
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

        public async Task<bool> Verify(VillageTownCityViewModel _villageTownCityViewModel)
        {
            try
            {
                // Set Default Value
                _villageTownCityViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterDemographicDetailViewModel
                _villageTownCityViewModel.CenterDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.UserAction = StringLiteralValue.Verify;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.Remark = _villageTownCityViewModel.Remark;
                _villageTownCityViewModel.CenterDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_villageTownCityViewModel.CenterModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    VillageTownCityViewModel villageTownCityViewModelForModify = await GetVerifiedEntry(_villageTownCityViewModel.CenterId);
                    CenterDemographicDetailViewModel centerDemographicDetailViewModelForModify = await centerDemographicDetailRepository.GetVerifiedEntry(_villageTownCityViewModel.CenterPrmKey);

                    // Set Default Value
                    villageTownCityViewModelForModify.UserAction = StringLiteralValue.Modify;
                    villageTownCityViewModelForModify.UserProfilePrmKey = _villageTownCityViewModel.UserProfilePrmKey;

                    centerDemographicDetailViewModelForModify.UserAction = StringLiteralValue.Modify;
                    centerDemographicDetailViewModelForModify.UserProfilePrmKey = _villageTownCityViewModel.UserProfilePrmKey;

                    // Mapping
                    CenterTranslationMakerChecker centerTranslationMakerCheckerForModify = Mapper.Map<CenterTranslationMakerChecker>(villageTownCityViewModelForModify);
                    CenterDemographicDetailMakerChecker centerDemographicDetailMakerCheckerForModify = Mapper.Map<CenterDemographicDetailMakerChecker>(centerDemographicDetailViewModelForModify);

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerCheckerForModify);
                    context.Entry(centerTranslationMakerCheckerForModify).State = EntityState.Added;

                    // CenterDemographicDetail
                    context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerCheckerForModify);
                    context.Entry(centerDemographicDetailMakerCheckerForModify).State = EntityState.Added;

                    // Modify (i.e. Old Verified Entries)
                    IEnumerable<CenterOccupationViewModel> centerOccupationListForModify = await centerOccuptionRepository.GetVerifiedEntries(_villageTownCityViewModel.CenterPrmKey);

                    foreach (CenterOccupationViewModel centerOccuptionViewModel in centerOccupationListForModify)
                    {
                        centerOccuptionViewModel.EntryDateTime = DateTime.Now;
                        centerOccuptionViewModel.UserAction = StringLiteralValue.Modify;
                        centerOccuptionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterOccupationMakerChecker centerOccupationalMakerCheckerForModify = Mapper.Map<CenterOccupationMakerChecker>(centerOccuptionViewModel);

                        context.CenterOccupationMakerCheckers.Attach(centerOccupationalMakerCheckerForModify);
                        context.Entry(centerOccupationalMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Modify (i.e. Old Verified Entries)
                    IEnumerable<CenterTradingEntityDetailViewModel> centerTradingDetailViewModelListForModify = await centerTradingDetailsRepository.GetVerifiedEntries(_villageTownCityViewModel.CenterPrmKey);

                    foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingDetailViewModelListForModify)
                    {
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.UserAction = StringLiteralValue.Modify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterTradingEntityDetailMakerChecker centerTradingDetailMakerCheckerForModify = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                        context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingDetailMakerCheckerForModify);
                        context.Entry(centerTradingDetailMakerCheckerForModify).State = EntityState.Added;
                    }

                    // CenterISOCode
                    if (_villageTownCityViewModel.IsMandatoryCenterISOCode == "O" || _villageTownCityViewModel.IsMandatoryCenterISOCode == "M")
                    {
                        _villageTownCityViewModel.EntryDateTime = DateTime.Now;
                        _villageTownCityViewModel.UserAction = StringLiteralValue.Modify;
                        _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterISOCodeMakerChecker centerISOCodeMakerCheckerForModify = Mapper.Map<CenterISOCodeMakerChecker>(villageTownCityViewModelForModify);

                        context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerCheckerForModify);
                        context.Entry(centerISOCodeMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (villageTownCityViewModelForModify.IsModified == true)
                    {
                        CenterModificationMakerChecker centerModificationMakerCheckerForModify = Mapper.Map<CenterModificationMakerChecker>(villageTownCityViewModelForModify);

                        context.CenterModificationMakerCheckers.Attach(centerModificationMakerCheckerForModify);
                        context.Entry(centerModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _villageTownCityViewModel.UserAction = StringLiteralValue.Verify;
                    _villageTownCityViewModel.CenterDemographicDetailViewModel.UserAction = StringLiteralValue.Verify;

                    CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_villageTownCityViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_villageTownCityViewModel);
                    CenterDemographicDetailMakerChecker centerDemographicDetailMakerChecker = Mapper.Map<CenterDemographicDetailMakerChecker>(_villageTownCityViewModel.CenterDemographicDetailViewModel);

                    // CenterModification
                    context.CenterModificationMakerCheckers.Attach(centerModificationMakerChecker);
                    context.Entry(centerModificationMakerChecker).State = EntityState.Added;

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                    context.Entry(centerTranslationMakerChecker).State = EntityState.Added;

                    // CenterDemographicDetail
                    context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerChecker);
                    context.Entry(centerDemographicDetailMakerChecker).State = EntityState.Added;

                    // Modify (i.e. Old Verified Entries)
                    IEnumerable<CenterOccupationViewModel> centerOccupationList = await centerOccuptionRepository.GetUnverifiedEntries(_villageTownCityViewModel.CenterPrmKey);

                    foreach (CenterOccupationViewModel centerOccuptionViewModel in centerOccupationList)
                    {
                        centerOccuptionViewModel.PrmKey = 0;
                        centerOccuptionViewModel.EntryStatus = StringLiteralValue.Verify;
                        centerOccuptionViewModel.Remark = _villageTownCityViewModel.Remark;
                        centerOccuptionViewModel.UserAction = StringLiteralValue.Verify;
                        centerOccuptionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(centerOccuptionViewModel);

                        context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                        context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
                    }

                    // CenterTradingEntityDetail
                    IEnumerable<CenterTradingEntityDetailViewModel> centerTradingDetailViewModelsList = await centerTradingDetailsRepository.GetUnverifiedEntries(_villageTownCityViewModel.CenterPrmKey);

                    foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingDetailViewModelsList)
                    {
                        viewModel.PrmKey = 0;
                        viewModel.EntryStatus = StringLiteralValue.Verify;
                        viewModel.Remark = _villageTownCityViewModel.Remark;
                        viewModel.UserAction = StringLiteralValue.Verify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterTradingEntityDetailMakerChecker centerTradingDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                        context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingDetailMakerChecker);
                        context.Entry(centerTradingDetailMakerChecker).State = EntityState.Added;
                    }

                    // CenterISOCode
                    if (_villageTownCityViewModel.IsMandatoryCenterISOCode == "O" || _villageTownCityViewModel.IsMandatoryCenterISOCode == "M")
                    {
                        _villageTownCityViewModel.EntryStatus = StringLiteralValue.Verify;
                        _villageTownCityViewModel.UserAction = StringLiteralValue.Verify;
                        _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_villageTownCityViewModel);

                        context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                        context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;
                    }
                }

                else
                {
                    _villageTownCityViewModel.UserAction = StringLiteralValue.Verify;
                    _villageTownCityViewModel.CenterDemographicDetailViewModel.EntryDateTime = DateTime.Now;
                    _villageTownCityViewModel.CenterDemographicDetailViewModel.UserAction = StringLiteralValue.Verify;
                    _villageTownCityViewModel.CenterDemographicDetailViewModel.Remark = _villageTownCityViewModel.Remark;
                    _villageTownCityViewModel.CenterDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_villageTownCityViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_villageTownCityViewModel);
                    CenterDemographicDetailMakerChecker centerDemographicDetailMakerChecker = Mapper.Map<CenterDemographicDetailMakerChecker>(_villageTownCityViewModel.CenterDemographicDetailViewModel);

                    // CenterMakerChecker
                    context.CenterMakerCheckers.Attach(centerMakerChecker);
                    context.Entry(centerMakerChecker).State = EntityState.Added;

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                    context.Entry(centerTranslationMakerChecker).State = EntityState.Added;

                    // CenterDemographicDetailMakerChecker
                    context.CenterDemographicDetailMakerCheckers.Attach(centerDemographicDetailMakerChecker);
                    context.Entry(centerDemographicDetailMakerChecker).State = EntityState.Added;

                    // CenterOccupationViewModel
                    IEnumerable<CenterOccupationViewModel> centerOccupationList = await centerOccuptionRepository.GetUnverifiedEntries(_villageTownCityViewModel.CenterPrmKey);

                    foreach (CenterOccupationViewModel centerOccuptionStructureViewModel in centerOccupationList)
                    {
                        centerOccuptionStructureViewModel.PrmKey = 0;
                        centerOccuptionStructureViewModel.EntryDateTime = DateTime.Now;
                        centerOccuptionStructureViewModel.Remark = _villageTownCityViewModel.Remark;
                        centerOccuptionStructureViewModel.UserAction = StringLiteralValue.Verify;
                        centerOccuptionStructureViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterOccupationMakerChecker centerOccupationMakerChecker = Mapper.Map<CenterOccupationMakerChecker>(centerOccuptionStructureViewModel);

                        context.CenterOccupationMakerCheckers.Attach(centerOccupationMakerChecker);
                        context.Entry(centerOccupationMakerChecker).State = EntityState.Added;
                    }

                    // CenterTradingEntityDetail
                    IEnumerable<CenterTradingEntityDetailViewModel> centerTradingDetailViewModelList = await centerTradingDetailsRepository.GetUnverifiedEntries(_villageTownCityViewModel.CenterPrmKey);

                    foreach (CenterTradingEntityDetailViewModel viewModel in centerTradingDetailViewModelList)
                    {
                        viewModel.PrmKey = 0;
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.Remark = _villageTownCityViewModel.Remark;
                        viewModel.UserAction = StringLiteralValue.Verify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterTradingEntityDetailMakerChecker centerTradingDetailMakerChecker = Mapper.Map<CenterTradingEntityDetailMakerChecker>(viewModel);

                        context.CenterTradingEntityDetailMakerCheckers.Attach(centerTradingDetailMakerChecker);
                        context.Entry(centerTradingDetailMakerChecker).State = EntityState.Added;
                    }

                    // CenterISOCode
                    if (_villageTownCityViewModel.IsMandatoryCenterISOCode == "O" || _villageTownCityViewModel.IsMandatoryCenterISOCode == "M")
                    {
                        _villageTownCityViewModel.EntryStatus = StringLiteralValue.Verify;
                        _villageTownCityViewModel.UserAction = StringLiteralValue.Verify;
                        _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_villageTownCityViewModel);

                        context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                        context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;
                    }
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