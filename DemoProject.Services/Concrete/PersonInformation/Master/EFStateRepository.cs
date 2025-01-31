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
    public class EFStateRepository : IStateRepository
    {
        private readonly EFDbContext context;
        private readonly ICenterISOCodeRepository centerISOCodeRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public EFStateRepository(RepositoryConnection _connection, ICenterISOCodeRepository _centerISOCodeRepository, IPersonDetailRepository _personDetailRepository)
        {
            context = _connection.EFDbContext;
            centerISOCodeRepository = _centerISOCodeRepository;
            personDetailRepository = _personDetailRepository;
        }

        public async Task<bool> Amend(StateViewModel _stateViewModel)
        {
            try
            {
                // Set Default Value
                _stateViewModel.ActivationStatus = StringLiteralValue.Active;
                _stateViewModel.EntryDateTime = DateTime.Now;
                _stateViewModel.EntryStatus = StringLiteralValue.Amend;
                _stateViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _stateViewModel.ReasonForModification = "None";
                _stateViewModel.UserAction = StringLiteralValue.Amend;
                _stateViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _stateViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _stateViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Amend;
                _stateViewModel.CenterIsoCodeViewModel.ReasonForModification = "None";
                _stateViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Amend;
                _stateViewModel.CenterIsoCodeViewModel.Remark = _stateViewModel.Remark;
                _stateViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _stateViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_stateViewModel.ParentCenterId);

                // Set ReferenceKey As PrmKey To Normal Tables
                _stateViewModel.CenterIsoCodeViewModel.CenterPrmKey = _stateViewModel.CenterPrmKey;

                // Mapping
                // Center
                Center centerForAmend = Mapper.Map<Center>(_stateViewModel);
                CenterMakerChecker centerMakerCheckerForAmend = Mapper.Map<CenterMakerChecker>(_stateViewModel);

                // CenterModification
                CenterModification centerModificationForAmend = Mapper.Map<CenterModification>(_stateViewModel);
                CenterModificationMakerChecker centerModificationMakerCheckerForAmend = Mapper.Map<CenterModificationMakerChecker>(_stateViewModel);

                // CenterTranslation
                CenterTranslation centerTranslationForAmend = Mapper.Map<CenterTranslation>(_stateViewModel);
                CenterTranslationMakerChecker centerTranslationMakerCheckerForAmend = Mapper.Map<CenterTranslationMakerChecker>(_stateViewModel);

                // CenterISOCode
                CenterISOCode centerISOCodeForAmend = Mapper.Map<CenterISOCode>(_stateViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerCheckerForAmend = Mapper.Map<CenterISOCodeMakerChecker>(_stateViewModel.CenterIsoCodeViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                centerForAmend.PrmKey = _stateViewModel.CenterPrmKey;
                centerModificationForAmend.PrmKey = _stateViewModel.CenterModificationPrmKey;
                centerTranslationForAmend.PrmKey = _stateViewModel.CenterTranslationPrmKey;
                centerISOCodeForAmend.PrmKey = _stateViewModel.CenterIsoCodeViewModel.CenterISOCodePrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_stateViewModel.CenterModificationPrmKey == 0)
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

        public async Task<bool> Delete(StateViewModel _stateViewModel)
        {
            try
            {
                // Set Default Value
                _stateViewModel.EntryDateTime = DateTime.Now;
                _stateViewModel.UserAction = StringLiteralValue.Delete;
                _stateViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //  CenterIsoCodeViewModel
                _stateViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _stateViewModel.CenterIsoCodeViewModel.Remark = _stateViewModel.Remark;
                _stateViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Delete;
                _stateViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_stateViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_stateViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_stateViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_stateViewModel.CenterIsoCodeViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_stateViewModel.CenterModificationPrmKey == 0)
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetStateEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetStateEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetStateEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<StateViewModel> GetRejectedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<StateViewModel>("SELECT * FROM dbo.GetStateEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
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

        public async Task<StateViewModel> GetUnVerifiedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<StateViewModel>("SELECT * FROM dbo.GetStateEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<StateViewModel> GetVerifiedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<StateViewModel>("SELECT * FROM dbo.GetStateEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***     
        public async Task<bool> Modify(StateViewModel _stateViewModel)
        {
            try
            {
                // Set Default Value
                _stateViewModel.CenterTranslationPrmKey = 0;
                _stateViewModel.CenterModificationPrmKey = 0;
                _stateViewModel.EntryDateTime = DateTime.Now;
                _stateViewModel.EntryStatus = StringLiteralValue.Create;
                _stateViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _stateViewModel.Remark = "None";
                _stateViewModel.UserAction = StringLiteralValue.Create;
                _stateViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _stateViewModel.CenterIsoCodeViewModel.CenterISOCodePrmKey = 0;
                _stateViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _stateViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Create;
                _stateViewModel.CenterIsoCodeViewModel.Remark = "None";
                _stateViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Create;
                _stateViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Set ReferenceKey As PrmKey To Normal Tables
                _stateViewModel.CenterIsoCodeViewModel.CenterPrmKey = _stateViewModel.CenterPrmKey;

                // Get PrmKey By Id Of All Dropdowns 
                _stateViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_stateViewModel.ParentCenterId);

                // CenterModification
                CenterModification centerModification = Mapper.Map<CenterModification>(_stateViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_stateViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_stateViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_stateViewModel);

                // CenterISOCode
                CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_stateViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_stateViewModel.CenterIsoCodeViewModel);

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

        public async Task<bool> Reject(StateViewModel _stateViewModel)
        {
            try
            {
                // Set Default Value
                _stateViewModel.EntryDateTime = DateTime.Now;
                _stateViewModel.UserAction = StringLiteralValue.Reject;
                _stateViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _stateViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _stateViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Reject;
                _stateViewModel.CenterIsoCodeViewModel.Remark = _stateViewModel.Remark;
                _stateViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_stateViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_stateViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_stateViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_stateViewModel.CenterIsoCodeViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_stateViewModel.CenterModificationPrmKey == 0)
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
        public async Task<bool> Save(StateViewModel _stateViewModel)
        {
            try
            {
                // Set Default Value
                _stateViewModel.ActivationStatus = StringLiteralValue.Active;
                _stateViewModel.EntryDateTime = DateTime.Now;
                _stateViewModel.EntryStatus = StringLiteralValue.Create;
                _stateViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _stateViewModel.ReasonForModification = "None";
                _stateViewModel.Remark = "None";
                _stateViewModel.UserAction = StringLiteralValue.Create;
                _stateViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _stateViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _stateViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Create;
                _stateViewModel.CenterIsoCodeViewModel.ReasonForModification = "None";
                _stateViewModel.CenterIsoCodeViewModel.Remark = "None";
                _stateViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Create;
                _stateViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _stateViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_stateViewModel.ParentCenterId);

                // Mapping
                // Center
                Center center = Mapper.Map<Center>(_stateViewModel);
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_stateViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_stateViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_stateViewModel);

                // CenterISOCode
                CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_stateViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_stateViewModel.CenterIsoCodeViewModel);

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

        public async Task<bool> Verify(StateViewModel _stateViewModel)
        {
            try
            {
                // Set Default Value
                _stateViewModel.EntryDateTime = DateTime.Now;
                _stateViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _stateViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _stateViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;
                _stateViewModel.CenterIsoCodeViewModel.Remark = _stateViewModel.Remark;
                _stateViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_stateViewModel.CenterModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    StateViewModel stateViewModelForModify = await GetVerifiedEntry(_stateViewModel.CenterId);
                    CenterIsoCodeViewModel centerIsoCodeViewModelForModify = await centerISOCodeRepository.GetVerifiedEntry(_stateViewModel.CenterPrmKey);

                    // Set Default Value
                    stateViewModelForModify.UserAction = StringLiteralValue.Modify;
                    stateViewModelForModify.UserProfilePrmKey = _stateViewModel.UserProfilePrmKey;

                    centerIsoCodeViewModelForModify.UserAction = StringLiteralValue.Modify;
                    centerIsoCodeViewModelForModify.UserProfilePrmKey = _stateViewModel.UserProfilePrmKey;

                    // Mapping
                    CenterTranslationMakerChecker centerTranslationMakerCheckerForModify = Mapper.Map<CenterTranslationMakerChecker>(stateViewModelForModify);
                    CenterISOCodeMakerChecker centerISOCodeMakerCheckerForModify = Mapper.Map<CenterISOCodeMakerChecker>(centerIsoCodeViewModelForModify);

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerCheckerForModify);
                    context.Entry(centerTranslationMakerCheckerForModify).State = EntityState.Added;

                    // CenterISOCodeMakerChecker
                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerCheckerForModify);
                    context.Entry(centerISOCodeMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (stateViewModelForModify.IsModified == true)
                    {
                        CenterModificationMakerChecker centerModificationMakerCheckerForModify = Mapper.Map<CenterModificationMakerChecker>(stateViewModelForModify);

                        context.CenterModificationMakerCheckers.Attach(centerModificationMakerCheckerForModify);
                        context.Entry(centerModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _stateViewModel.UserAction = StringLiteralValue.Verify;
                    _stateViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;

                    CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_stateViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_stateViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_stateViewModel.CenterIsoCodeViewModel);

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
                    _stateViewModel.UserAction = StringLiteralValue.Verify;

                    _stateViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                    _stateViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;
                    _stateViewModel.CenterIsoCodeViewModel.Remark = _stateViewModel.Remark;
                    _stateViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_stateViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_stateViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_stateViewModel.CenterIsoCodeViewModel);

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