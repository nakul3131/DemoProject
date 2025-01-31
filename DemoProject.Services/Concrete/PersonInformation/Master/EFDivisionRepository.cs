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
    public class EFDivisionRepository : IDivisionRepository
    {
        private readonly EFDbContext context;
        private readonly IStateRepository stateRepository;
        private readonly ICenterISOCodeRepository centerISOCodeRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public EFDivisionRepository(RepositoryConnection _connection, IStateRepository _stateRepository, ICenterISOCodeRepository _centerISOCodeRepository, IPersonDetailRepository _personDetailRepository)
        {
            context = _connection.EFDbContext;
            stateRepository = _stateRepository;
            centerISOCodeRepository = _centerISOCodeRepository;
            personDetailRepository = _personDetailRepository;

        }

        public async Task<bool> Amend(DivisionViewModel _divisionViewModel)
        {
            try
            {
                // Set Default Value
                _divisionViewModel.ActivationStatus = StringLiteralValue.Active;
                _divisionViewModel.CenterCategoryPrmKey = 7;
                _divisionViewModel.EntryDateTime = DateTime.Now;
                _divisionViewModel.EntryStatus = StringLiteralValue.Amend;
                _divisionViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _divisionViewModel.ReasonForModification = "None";
                _divisionViewModel.UserAction = StringLiteralValue.Amend;
                _divisionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _divisionViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _divisionViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Amend;
                _divisionViewModel.CenterIsoCodeViewModel.ReasonForModification = "None";
                _divisionViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Amend;
                _divisionViewModel.CenterIsoCodeViewModel.Remark = _divisionViewModel.Remark;
                _divisionViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Set ReferenceKey As PrmKey To Normal Tables
                _divisionViewModel.CenterIsoCodeViewModel.CenterPrmKey = _divisionViewModel.CenterPrmKey;

                // Get PrmKey By Id Of All Dropdowns
                _divisionViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_divisionViewModel.ParentCenterId);

                // Mapping
                // Center
                Center centerForAmend = Mapper.Map<Center>(_divisionViewModel);
                CenterMakerChecker centerMakerCheckerForAmend = Mapper.Map<CenterMakerChecker>(_divisionViewModel);

                // CenterModification
                CenterModification centerModificationForAmend = Mapper.Map<CenterModification>(_divisionViewModel);
                CenterModificationMakerChecker centerModificationMakerCheckerForAmend = Mapper.Map<CenterModificationMakerChecker>(_divisionViewModel);

                // CenterTranslation
                CenterTranslation centerTranslationForAmend = Mapper.Map<CenterTranslation>(_divisionViewModel);
                CenterTranslationMakerChecker centerTranslationMakerCheckerForAmend = Mapper.Map<CenterTranslationMakerChecker>(_divisionViewModel);

                // CenterISOCode
                CenterISOCode centerISOCodeForAmend = Mapper.Map<CenterISOCode>(_divisionViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerCheckerForAmend = Mapper.Map<CenterISOCodeMakerChecker>(_divisionViewModel.CenterIsoCodeViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                centerForAmend.PrmKey = _divisionViewModel.CenterPrmKey;
                centerModificationForAmend.PrmKey = _divisionViewModel.CenterModificationPrmKey;
                centerTranslationForAmend.PrmKey = _divisionViewModel.CenterTranslationPrmKey;
                centerISOCodeForAmend.PrmKey = _divisionViewModel.CenterIsoCodeViewModel.CenterISOCodePrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_divisionViewModel.CenterModificationPrmKey == 0)
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

        public async Task<bool> Delete(DivisionViewModel _divisionViewModel)
        {
            try
            {
                // Set Default Value
                _divisionViewModel.EntryDateTime = DateTime.Now;
                _divisionViewModel.UserAction = StringLiteralValue.Delete;
                _divisionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //  CenterIsoCodeViewModel
                _divisionViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _divisionViewModel.CenterIsoCodeViewModel.Remark = _divisionViewModel.Remark;
                _divisionViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Delete;
                _divisionViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_divisionViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_divisionViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_divisionViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_divisionViewModel.CenterIsoCodeViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_divisionViewModel.CenterModificationPrmKey == 0)
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetDivisionEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetDivisionEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetDivisionEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<DivisionViewModel> GetRejectedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<DivisionViewModel>("SELECT * FROM dbo.GetDivisionEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
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
            if (context.Centers.Where(p => p.NameOfCenter == NameOfCenter && p.CenterCategoryPrmKey == 7).Select(p => p.PrmKey).FirstOrDefault() > 0)
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

        public async Task<DivisionViewModel> GetUnverifiedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<DivisionViewModel>("SELECT * FROM dbo.GetDivisionEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<DivisionViewModel> GetVerifiedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<DivisionViewModel>("SELECT * FROM dbo.GetDivisionEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***     
        public async Task<bool> Modify(DivisionViewModel _divisionViewModel)
        {
            try
            {
                // Set Default Value
                _divisionViewModel.CenterTranslationPrmKey = 0;
                _divisionViewModel.CenterModificationPrmKey = 0;
                _divisionViewModel.EntryDateTime = DateTime.Now;
                _divisionViewModel.EntryStatus = StringLiteralValue.Create;
                _divisionViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _divisionViewModel.Remark = "None";
                _divisionViewModel.UserAction = StringLiteralValue.Create;
                _divisionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _divisionViewModel.CenterIsoCodeViewModel.CenterISOCodePrmKey = 0;
                _divisionViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _divisionViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Create;
                _divisionViewModel.CenterIsoCodeViewModel.Remark = "None";
                _divisionViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Create;
                _divisionViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Set ReferenceKey As PrmKey To Normal Tables
                _divisionViewModel.CenterIsoCodeViewModel.CenterPrmKey = _divisionViewModel.CenterPrmKey;

                // Get PrmKey By Id Of All Dropdowns 
                _divisionViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_divisionViewModel.ParentCenterId);

                // CenterModification
                CenterModification centerModification = Mapper.Map<CenterModification>(_divisionViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_divisionViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_divisionViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_divisionViewModel);

                // CenterISOCode
                CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_divisionViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_divisionViewModel.CenterIsoCodeViewModel);

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

        public async Task<bool> Reject(DivisionViewModel _divisionViewModel)
        {
            try
            {
                // Set Default Value
                _divisionViewModel.EntryDateTime = DateTime.Now;
                _divisionViewModel.UserAction = StringLiteralValue.Reject;
                _divisionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _divisionViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _divisionViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Reject;
                _divisionViewModel.CenterIsoCodeViewModel.Remark = _divisionViewModel.Remark;
                _divisionViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_divisionViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_divisionViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_divisionViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_divisionViewModel.CenterIsoCodeViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_divisionViewModel.CenterModificationPrmKey == 0)
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
        public async Task<bool> Save(DivisionViewModel _divisionViewModel)
        {
            try
            {
                // Set Default Value
                _divisionViewModel.ActivationStatus = StringLiteralValue.Active;
                _divisionViewModel.CenterCategoryPrmKey = 7;
                _divisionViewModel.EntryDateTime = DateTime.Now;
                _divisionViewModel.EntryStatus = StringLiteralValue.Create;
                _divisionViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _divisionViewModel.ReasonForModification = "None";
                _divisionViewModel.Remark = "None";
                _divisionViewModel.UserAction = StringLiteralValue.Create;
                _divisionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _divisionViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _divisionViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Create;
                _divisionViewModel.CenterIsoCodeViewModel.ReasonForModification = "None";
                _divisionViewModel.CenterIsoCodeViewModel.Remark = "None";
                _divisionViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Create;
                _divisionViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _divisionViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_divisionViewModel.ParentCenterId);

                // Mapping
                // Center
                Center center = Mapper.Map<Center>(_divisionViewModel);
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_divisionViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_divisionViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_divisionViewModel);

                // CenterISOCode
                CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_divisionViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_divisionViewModel.CenterIsoCodeViewModel);

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

        public async Task<bool> Verify(DivisionViewModel _divisionViewModel)
        {
            try
            {
                // Set Default Value
                _divisionViewModel.EntryDateTime = DateTime.Now;
                _divisionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _divisionViewModel.CenterId = GetCenterIdByPrmKey(_divisionViewModel.CenterPrmKey);

                // CenterIsoCodeViewModel
                _divisionViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _divisionViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;
                _divisionViewModel.CenterIsoCodeViewModel.Remark = _divisionViewModel.Remark;
                _divisionViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_divisionViewModel.CenterModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    DivisionViewModel divisionViewModelForModify = await GetVerifiedEntry(_divisionViewModel.CenterId);
                    CenterIsoCodeViewModel centerIsoCodeViewModelForModify = await centerISOCodeRepository.GetVerifiedEntry(_divisionViewModel.CenterPrmKey);

                    // Set Default Value
                    divisionViewModelForModify.UserAction = StringLiteralValue.Modify;
                    divisionViewModelForModify.UserProfilePrmKey = _divisionViewModel.UserProfilePrmKey;

                    centerIsoCodeViewModelForModify.UserAction = StringLiteralValue.Modify;
                    centerIsoCodeViewModelForModify.UserProfilePrmKey = _divisionViewModel.UserProfilePrmKey;

                    // Mapping
                    CenterTranslationMakerChecker centerTranslationMakerCheckerForModify = Mapper.Map<CenterTranslationMakerChecker>(divisionViewModelForModify);
                    CenterISOCodeMakerChecker centerISOCodeMakerCheckerForModify = Mapper.Map<CenterISOCodeMakerChecker>(centerIsoCodeViewModelForModify);

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerCheckerForModify);
                    context.Entry(centerTranslationMakerCheckerForModify).State = EntityState.Added;

                    // CenterISOCodeMakerChecker
                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerCheckerForModify);
                    context.Entry(centerISOCodeMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (divisionViewModelForModify.IsModified == true)
                    {
                        CenterModificationMakerChecker centerModificationMakerCheckerForModify = Mapper.Map<CenterModificationMakerChecker>(divisionViewModelForModify);

                        context.CenterModificationMakerCheckers.Attach(centerModificationMakerCheckerForModify);
                        context.Entry(centerModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _divisionViewModel.UserAction = StringLiteralValue.Verify;
                    _divisionViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;

                    CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_divisionViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_divisionViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_divisionViewModel.CenterIsoCodeViewModel);

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
                    _divisionViewModel.UserAction = StringLiteralValue.Verify;

                    // CenterIsoCodeViewModel
                    _divisionViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                    _divisionViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;
                    _divisionViewModel.CenterIsoCodeViewModel.Remark = _divisionViewModel.Remark;
                    _divisionViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_divisionViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_divisionViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_divisionViewModel.CenterIsoCodeViewModel);

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