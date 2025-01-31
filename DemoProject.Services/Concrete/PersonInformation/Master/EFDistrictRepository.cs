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
    public class EFDistrictRepository : IDistrictRepository
    {
        private readonly EFDbContext context;
        private readonly IDivisionRepository divisionRepository;
        private readonly ICenterISOCodeRepository centerISOCodeRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public EFDistrictRepository(RepositoryConnection _connection, IDivisionRepository _divisionRepository, ICenterISOCodeRepository _centerISOCodeRepository, IPersonDetailRepository _personDetailRepository)
        {
            context = _connection.EFDbContext;
            divisionRepository = _divisionRepository;
            centerISOCodeRepository = _centerISOCodeRepository;
            personDetailRepository = _personDetailRepository;
        }

        public async Task<bool> Amend(DistrictViewModel _districtViewModel)
        {
            try
            {
                // Set default Value
                _districtViewModel.ActivationStatus = StringLiteralValue.Active;
                _districtViewModel.CenterCategoryPrmKey = 6;
                _districtViewModel.EntryDateTime = DateTime.Now;
                _districtViewModel.EntryStatus = StringLiteralValue.Amend;
                _districtViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _districtViewModel.ReasonForModification = "None";
                _districtViewModel.UserAction = StringLiteralValue.Amend;
                _districtViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _districtViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _districtViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Amend;
                _districtViewModel.CenterIsoCodeViewModel.ReasonForModification = "None";
                _districtViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Amend;
                _districtViewModel.CenterIsoCodeViewModel.Remark = _districtViewModel.Remark;
                _districtViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdown
                _districtViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_districtViewModel.ParentCenterId);

                // Set ReferenceKey As PrmKey To Normal Tables
                _districtViewModel.CenterIsoCodeViewModel.CenterPrmKey = _districtViewModel.CenterPrmKey;

                // Mapping
                // Center
                Center centerForAmend = Mapper.Map<Center>(_districtViewModel);
                CenterMakerChecker centerMakerCheckerForAmend = Mapper.Map<CenterMakerChecker>(_districtViewModel);

                // CenterModification
                CenterModification centerModificationForAmend = Mapper.Map<CenterModification>(_districtViewModel);
                CenterModificationMakerChecker centerModificationMakerCheckerForAmend = Mapper.Map<CenterModificationMakerChecker>(_districtViewModel);

                // CenterTranslation
                CenterTranslation centerTranslationForAmend = Mapper.Map<CenterTranslation>(_districtViewModel);
                CenterTranslationMakerChecker centerTranslationMakerCheckerForAmend = Mapper.Map<CenterTranslationMakerChecker>(_districtViewModel);

                // CenterISOCode
                CenterISOCode centerISOCodeForAmend = Mapper.Map<CenterISOCode>(_districtViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerCheckerForAmend = Mapper.Map<CenterISOCodeMakerChecker>(_districtViewModel.CenterIsoCodeViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                centerForAmend.PrmKey = _districtViewModel.CenterPrmKey;
                centerModificationForAmend.PrmKey = _districtViewModel.CenterModificationPrmKey;
                centerTranslationForAmend.PrmKey = _districtViewModel.CenterTranslationPrmKey;
                centerISOCodeForAmend.PrmKey = _districtViewModel.CenterIsoCodeViewModel.CenterISOCodePrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_districtViewModel.CenterModificationPrmKey == 0)
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

        public async Task<bool> Delete(DistrictViewModel _districtViewModel)
        {
            try
            {
                // Set Default Value
                _districtViewModel.EntryDateTime = DateTime.Now;
                _districtViewModel.UserAction = StringLiteralValue.Delete;
                _districtViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //  CenterIsoCodeViewModel
                _districtViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _districtViewModel.CenterIsoCodeViewModel.Remark = _districtViewModel.Remark;
                _districtViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Delete;
                _districtViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_districtViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_districtViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_districtViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_districtViewModel.CenterIsoCodeViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_districtViewModel.CenterModificationPrmKey == 0)
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetDistrictEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetDistrictEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetDistrictEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<DistrictViewModel> GetRejectedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<DistrictViewModel>("SELECT * FROM dbo.GetDistrictEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
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
            if (context.Centers.Where(p => p.NameOfCenter == NameOfCenter && p.CenterCategoryPrmKey == 6).Select(p => p.PrmKey).FirstOrDefault() > 0)
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

        public Guid GetCenterIdByPrmKey(int _prmKey)
        {
            return context.Centers
                    .Where(c => c.PrmKey == _prmKey)
                    .Select(c => c.CenterId).FirstOrDefault();
        }

        public async Task<DistrictViewModel> GetUnVerifiedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<DistrictViewModel>("SELECT * FROM dbo.GetDistrictEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<DistrictViewModel> GetVerifiedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<DistrictViewModel>("SELECT * FROM dbo.GetDistrictEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***     
        public async Task<bool> Modify(DistrictViewModel _districtViewModel)
        {
            try
            {
                // Set default Value
                _districtViewModel.CenterTranslationPrmKey = 0;
                _districtViewModel.CenterModificationPrmKey = 0;
                _districtViewModel.EntryDateTime = DateTime.Now;
                _districtViewModel.EntryStatus = StringLiteralValue.Create;
                _districtViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _districtViewModel.UserAction = StringLiteralValue.Create;
                _districtViewModel.Remark = "None";
                _districtViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _districtViewModel.CenterIsoCodeViewModel.CenterISOCodePrmKey = 0;
                _districtViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _districtViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Create;
                _districtViewModel.CenterIsoCodeViewModel.Remark = "None";
                _districtViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Create;
                _districtViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Set ReferenceKey As PrmKey To Normal Tables
                _districtViewModel.CenterIsoCodeViewModel.CenterPrmKey = _districtViewModel.CenterPrmKey;

                // Get PrmKey By Id Of All Dropdowns 
                _districtViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_districtViewModel.ParentCenterId);

                // CenterModification
                CenterModification centerModification = Mapper.Map<CenterModification>(_districtViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_districtViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_districtViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_districtViewModel);

                // CenterISOCode
                CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_districtViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_districtViewModel.CenterIsoCodeViewModel);

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

        public async Task<bool> Reject(DistrictViewModel _districtViewModel)
        {
            try
            {
                // Set Default Value
                _districtViewModel.EntryDateTime = DateTime.Now;
                _districtViewModel.UserAction = StringLiteralValue.Reject;
                _districtViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _districtViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _districtViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Reject;
                _districtViewModel.CenterIsoCodeViewModel.Remark = _districtViewModel.Remark;
                _districtViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_districtViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_districtViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_districtViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_districtViewModel.CenterIsoCodeViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_districtViewModel.CenterModificationPrmKey == 0)
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
        public async Task<bool> Save(DistrictViewModel _districtViewModel)
        {
            try
            {
                // Set default Value
                _districtViewModel.ActivationStatus = StringLiteralValue.Active;
                _districtViewModel.CenterCategoryPrmKey = 6;
                _districtViewModel.EntryDateTime = DateTime.Now;
                _districtViewModel.EntryStatus = StringLiteralValue.Create;
                _districtViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _districtViewModel.ReasonForModification = "None";
                _districtViewModel.Remark = "None";
                _districtViewModel.UserAction = StringLiteralValue.Create;
                _districtViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _districtViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _districtViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Create;
                _districtViewModel.CenterIsoCodeViewModel.ReasonForModification = "None";
                _districtViewModel.CenterIsoCodeViewModel.Remark = "None";
                _districtViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Create;
                _districtViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _districtViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_districtViewModel.ParentCenterId);

                // Mapping
                // Center
                Center center = Mapper.Map<Center>(_districtViewModel);
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_districtViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_districtViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_districtViewModel);

                // CenterISOCode
                CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_districtViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_districtViewModel.CenterIsoCodeViewModel);

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

        public async Task<bool> Verify(DistrictViewModel _districtViewModel)
        {
            try
            {
                // Set Default Value
                _districtViewModel.EntryDateTime = DateTime.Now;
                _districtViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _districtViewModel.CenterId = GetCenterIdByPrmKey(_districtViewModel.CenterPrmKey);

                // CenterIsoCodeViewModel
                _districtViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _districtViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;
                _districtViewModel.CenterIsoCodeViewModel.Remark = _districtViewModel.Remark;
                _districtViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_districtViewModel.CenterModificationPrmKey > 0)
                {
                    // Modify Old Record  
                    DistrictViewModel districtViewModelForModify = await GetVerifiedEntry(_districtViewModel.CenterId);
                    CenterIsoCodeViewModel centerIsoCodeViewModelForModify = await centerISOCodeRepository.GetVerifiedEntry(_districtViewModel.CenterPrmKey);

                    // Set Default Value
                    districtViewModelForModify.UserAction = StringLiteralValue.Modify;
                    districtViewModelForModify.UserProfilePrmKey = _districtViewModel.UserProfilePrmKey;

                    centerIsoCodeViewModelForModify.UserAction = StringLiteralValue.Modify;
                    centerIsoCodeViewModelForModify.UserProfilePrmKey = _districtViewModel.UserProfilePrmKey;

                    // Mapping
                    CenterTranslationMakerChecker centerTranslationMakerCheckerForModify = Mapper.Map<CenterTranslationMakerChecker>(districtViewModelForModify);
                    CenterISOCodeMakerChecker centerISOCodeMakerCheckerForModify = Mapper.Map<CenterISOCodeMakerChecker>(centerIsoCodeViewModelForModify);

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerCheckerForModify);
                    context.Entry(centerTranslationMakerCheckerForModify).State = EntityState.Added;

                    // CenterISOCodeMakerChecker
                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerCheckerForModify);
                    context.Entry(centerISOCodeMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (districtViewModelForModify.IsModified == true)
                    {
                        CenterModificationMakerChecker centerModificationMakerCheckerForModify = Mapper.Map<CenterModificationMakerChecker>(districtViewModelForModify);

                        context.CenterModificationMakerCheckers.Attach(centerModificationMakerCheckerForModify);
                        context.Entry(centerModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _districtViewModel.UserAction = StringLiteralValue.Verify;
                    _districtViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;

                    CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_districtViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_districtViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_districtViewModel.CenterIsoCodeViewModel);

                    // CenterModification
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
                    _districtViewModel.UserAction = StringLiteralValue.Verify;

                    // CenterIsoCodeViewModel
                    _districtViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                    _districtViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;
                    _districtViewModel.CenterIsoCodeViewModel.Remark = _districtViewModel.Remark;
                    _districtViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_districtViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_districtViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_districtViewModel.CenterIsoCodeViewModel);

                    // Center
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