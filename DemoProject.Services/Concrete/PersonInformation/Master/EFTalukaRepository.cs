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
using DemoProject.Domain.Entities.PersonInformation.Master;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.Concrete.PersonInformation.Master
{
    public class EFTalukaRepository : ITalukaRepository
    {
        private readonly EFDbContext context;
        private readonly IDistrictRepository districtRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ICenterISOCodeRepository centerISOCodeRepository;

        public EFTalukaRepository(RepositoryConnection _connection, IDistrictRepository _districtRepository, ICenterISOCodeRepository _centerISOCodeRepository, IPersonDetailRepository _personDetailRepository)
        {
            context = _connection.EFDbContext;
            districtRepository = _districtRepository;
            centerISOCodeRepository = _centerISOCodeRepository;
            personDetailRepository = _personDetailRepository;
        }

        public async Task<bool> Amend(TalukaViewModel _talukaViewModel)
        {
            try
            {
                // Set Default Value
                _talukaViewModel.ActivationStatus = StringLiteralValue.Active;
                _talukaViewModel.EntryDateTime = DateTime.Now;
                _talukaViewModel.EntryStatus = StringLiteralValue.Amend;
                _talukaViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _talukaViewModel.ReasonForModification = "None";
                _talukaViewModel.UserAction = StringLiteralValue.Amend;
                _talukaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _talukaViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _talukaViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Amend;
                _talukaViewModel.CenterIsoCodeViewModel.ReasonForModification = "None";
                _talukaViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Amend;
                _talukaViewModel.CenterIsoCodeViewModel.Remark = _talukaViewModel.Remark;
                _talukaViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Set ReferenceKey As PrmKey To Normal Tables
                _talukaViewModel.CenterIsoCodeViewModel.CenterPrmKey = _talukaViewModel.CenterPrmKey;

                // Get PrmKey By Id
                if (_talukaViewModel.CenterCategoryPrmKey == 4)
                {
                    _talukaViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_talukaViewModel.ParentCenterSubDivisionOfficeId);
                }

                if (_talukaViewModel.CenterCategoryPrmKey == 5)
                {
                    _talukaViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_talukaViewModel.ParentCenterDistrictId);
                }

                // Mapping 
                // Center
                Center centerForAmend = Mapper.Map<Center>(_talukaViewModel);
                CenterMakerChecker centerMakerCheckerForAmend = Mapper.Map<CenterMakerChecker>(_talukaViewModel);

                // CenterModification
                CenterModification centerModificationForAmend = Mapper.Map<CenterModification>(_talukaViewModel);
                CenterModificationMakerChecker centerModificationMakerCheckerForAmend = Mapper.Map<CenterModificationMakerChecker>(_talukaViewModel);

                // CenterTranslation
                CenterTranslation centerTranslationForAmend = Mapper.Map<CenterTranslation>(_talukaViewModel);
                CenterTranslationMakerChecker centerTranslationMakerCheckerForAmend = Mapper.Map<CenterTranslationMakerChecker>(_talukaViewModel);

                // CenterISOCode
                CenterISOCode centerISOCodeForAmend = Mapper.Map<CenterISOCode>(_talukaViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerCheckerForAmend = Mapper.Map<CenterISOCodeMakerChecker>(_talukaViewModel.CenterIsoCodeViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                centerForAmend.PrmKey = _talukaViewModel.CenterPrmKey;
                centerModificationForAmend.PrmKey = _talukaViewModel.CenterModificationPrmKey;
                centerTranslationForAmend.PrmKey = _talukaViewModel.CenterTranslationPrmKey;
                centerISOCodeForAmend.PrmKey = _talukaViewModel.CenterIsoCodeViewModel.CenterISOCodePrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_talukaViewModel.CenterModificationPrmKey == 0)
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
                    // CenterModification
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

                // CenterISOCode
                context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerCheckerForAmend);
                context.Entry(centerISOCodeMakerCheckerForAmend).State = EntityState.Added;
                centerISOCodeForAmend.CenterISOCodeMakerCheckers.Add(centerISOCodeMakerCheckerForAmend);

                context.CenterISOCodes.Attach(centerISOCodeForAmend);
                context.Entry(centerISOCodeForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(TalukaViewModel _talukaViewModel)
        {
            try
            {
                // Set Default Value
                _talukaViewModel.EntryDateTime = DateTime.Now;
                _talukaViewModel.UserAction = StringLiteralValue.Delete;
                _talukaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //  CenterIsoCodeViewModel
                _talukaViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _talukaViewModel.CenterIsoCodeViewModel.Remark = _talukaViewModel.Remark;
                _talukaViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Delete;
                _talukaViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_talukaViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_talukaViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_talukaViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_talukaViewModel.CenterIsoCodeViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_talukaViewModel.CenterModificationPrmKey == 0)
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

                // CenterISOCode
                context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;

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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetTalukaEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetTalukaEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetTalukaEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<TalukaViewModel> GetRejectedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<TalukaViewModel>("SELECT * FROM dbo.GetTalukaEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey)
        {
            bool status;
            if (context.Centers.Where(p => p.NameOfCenter == NameOfCenter && p.CenterCategoryPrmKey == CenterCategoryPrmKey).Select(p => p.PrmKey).FirstOrDefault() > 0)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;

        }

        public Guid GetTalukaIdByPrmKey(int _prmKey)
        {
            return context.Centers
                    .Where(c => c.PrmKey == _prmKey)
                    .Select(c => c.CenterId).FirstOrDefault();
        }

        public async Task<TalukaViewModel> GetUnVerifiedEntry(Guid _centerId)
        {
            try
            {
                var a = await context.Database.SqlQuery<TalukaViewModel>("SELECT * FROM dbo.GetTalukaEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<TalukaViewModel> GetVerifiedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<TalukaViewModel>("SELECT * FROM dbo.GetTalukaEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(TalukaViewModel _talukaViewModel)
        {
            try
            {
                // Set Default Value
                _talukaViewModel.CenterTranslationPrmKey = 0;
                _talukaViewModel.CenterModificationPrmKey = 0;
                _talukaViewModel.EntryDateTime = DateTime.Now;
                _talukaViewModel.EntryStatus = StringLiteralValue.Create;
                _talukaViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _talukaViewModel.Remark = "None";
                _talukaViewModel.UserAction = StringLiteralValue.Create;
                _talukaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _talukaViewModel.CenterIsoCodeViewModel.CenterISOCodePrmKey = 0;
                _talukaViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _talukaViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Create;
                _talukaViewModel.CenterIsoCodeViewModel.Remark = "None";
                _talukaViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Create;
                _talukaViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Set ReferenceKey As PrmKey To Normal Tables
                _talukaViewModel.CenterIsoCodeViewModel.CenterPrmKey = _talukaViewModel.CenterPrmKey;

                // Get PrmKey By Id
                if (_talukaViewModel.CenterCategoryPrmKey == 4)
                {
                    _talukaViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_talukaViewModel.ParentCenterSubDivisionOfficeId);
                }

                if (_talukaViewModel.CenterCategoryPrmKey == 5)
                {
                    _talukaViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_talukaViewModel.ParentCenterDistrictId);
                }

                // CenterModification
                CenterModification centerModification = Mapper.Map<CenterModification>(_talukaViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_talukaViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_talukaViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_talukaViewModel);

                // CenterISOCode
                CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_talukaViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_talukaViewModel.CenterIsoCodeViewModel);

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

                // CenterISOCode
                context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;
                centerISOCode.CenterISOCodeMakerCheckers.Add(centerISOCodeMakerChecker);

                context.CenterISOCodes.Attach(centerISOCode);
                context.Entry(centerISOCode).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(TalukaViewModel _talukaViewModel)
        {
            try
            {
                // Set Default Value.
                _talukaViewModel.EntryDateTime = DateTime.Now;
                _talukaViewModel.UserAction = StringLiteralValue.Reject;
                _talukaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _talukaViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _talukaViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Reject;
                _talukaViewModel.CenterIsoCodeViewModel.Remark = _talukaViewModel.Remark;
                _talukaViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_talukaViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_talukaViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_talukaViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_talukaViewModel.CenterIsoCodeViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_talukaViewModel.CenterModificationPrmKey == 0)
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

                // CenterISOCodeMakerChecker
                context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;

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
        public async Task<bool> Save(TalukaViewModel _talukaViewModel)
        {
            try
            {
                // Set Default Value
                _talukaViewModel.ActivationStatus = StringLiteralValue.Active;
                _talukaViewModel.EntryDateTime = DateTime.Now;
                _talukaViewModel.EntryStatus = StringLiteralValue.Create;
                _talukaViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _talukaViewModel.ReasonForModification = "None";
                _talukaViewModel.Remark = "None";
                _talukaViewModel.UserAction = StringLiteralValue.Create;
                _talukaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _talukaViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _talukaViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Create;
                _talukaViewModel.CenterIsoCodeViewModel.ReasonForModification = "None";
                _talukaViewModel.CenterIsoCodeViewModel.Remark = "None";
                _talukaViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Create;
                _talukaViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                if (_talukaViewModel.CenterCategoryPrmKey == 4)
                {
                    _talukaViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_talukaViewModel.ParentCenterSubDivisionOfficeId);
                }

                if (_talukaViewModel.CenterCategoryPrmKey == 5)
                {
                    _talukaViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_talukaViewModel.ParentCenterDistrictId);
                }

                // Mapping
                // Center
                Center center = Mapper.Map<Center>(_talukaViewModel);
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_talukaViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_talukaViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_talukaViewModel);

                // CenterISOCode
                CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_talukaViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_talukaViewModel.CenterIsoCodeViewModel);

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

                // CenterISOCode
                context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;
                centerISOCode.CenterISOCodeMakerCheckers.Add(centerISOCodeMakerChecker);

                context.CenterISOCodes.Attach(centerISOCode);
                context.Entry(centerISOCode).State = EntityState.Added;
                center.CenterISOCodes.Add(centerISOCode);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(TalukaViewModel _talukaViewModel)
        {
            try
            {
                // Set Default Value
                _talukaViewModel.EntryDateTime = DateTime.Now;
                _talukaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _talukaViewModel.CenterId = GetTalukaIdByPrmKey(_talukaViewModel.CenterPrmKey);

                // CenterIsoCodeViewModel
                _talukaViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _talukaViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;
                _talukaViewModel.CenterIsoCodeViewModel.Remark = _talukaViewModel.Remark;
                _talukaViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_talukaViewModel.CenterModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    TalukaViewModel talukaViewModelForModify = await GetVerifiedEntry(_talukaViewModel.CenterId);
                    CenterIsoCodeViewModel centerIsoCodeViewModelForModify = await centerISOCodeRepository.GetVerifiedEntry(_talukaViewModel.CenterPrmKey);

                    // Set Default Value
                    talukaViewModelForModify.UserAction = StringLiteralValue.Modify;
                    talukaViewModelForModify.UserProfilePrmKey = _talukaViewModel.UserProfilePrmKey;

                    centerIsoCodeViewModelForModify.UserAction = StringLiteralValue.Modify;
                    centerIsoCodeViewModelForModify.UserProfilePrmKey = _talukaViewModel.UserProfilePrmKey;

                    // Mapping
                    CenterTranslationMakerChecker centerTranslationMakerCheckerForModify = Mapper.Map<CenterTranslationMakerChecker>(talukaViewModelForModify);
                    CenterISOCodeMakerChecker centerISOCodeMakerCheckerForModify = Mapper.Map<CenterISOCodeMakerChecker>(centerIsoCodeViewModelForModify);

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerCheckerForModify);
                    context.Entry(centerTranslationMakerCheckerForModify).State = EntityState.Added;

                    // CenterISOCodeMakerChecker
                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerCheckerForModify);
                    context.Entry(centerISOCodeMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (talukaViewModelForModify.IsModified == true)
                    {
                        CenterModificationMakerChecker centerModificationMakerCheckerForModify = Mapper.Map<CenterModificationMakerChecker>(talukaViewModelForModify);

                        context.CenterModificationMakerCheckers.Attach(centerModificationMakerCheckerForModify);
                        context.Entry(centerModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _talukaViewModel.UserAction = StringLiteralValue.Verify;
                    _talukaViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;

                    CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_talukaViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_talukaViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_talukaViewModel.CenterIsoCodeViewModel);

                    // CenterModificationMakerChecker
                    context.CenterModificationMakerCheckers.Attach(centerModificationMakerChecker);
                    context.Entry(centerModificationMakerChecker).State = EntityState.Added;

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                    context.Entry(centerTranslationMakerChecker).State = EntityState.Added;

                    // CenterISOCodeMakerChecker
                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                    context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;

                }
                else
                {
                    // Set Default Value
                    _talukaViewModel.UserAction = StringLiteralValue.Verify;

                    // CenterIsoCodeViewModel
                    _talukaViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                    _talukaViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;
                    _talukaViewModel.CenterIsoCodeViewModel.Remark = _talukaViewModel.Remark;
                    _talukaViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_talukaViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_talukaViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_talukaViewModel.CenterIsoCodeViewModel);

                    // CenterMakerChecker
                    context.CenterMakerCheckers.Attach(centerMakerChecker);
                    context.Entry(centerMakerChecker).State = EntityState.Added;

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                    context.Entry(centerTranslationMakerChecker).State = EntityState.Added;

                    // CenterISOCodeMakerChecker
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
    }
}