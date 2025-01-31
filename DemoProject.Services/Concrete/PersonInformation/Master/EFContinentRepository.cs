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
    public class EFContinentRepository : IContinentRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonDetailRepository personDetailRepository;

        public EFContinentRepository(RepositoryConnection _connection, IPersonDetailRepository _personDetailRepository)
        {
            context = _connection.EFDbContext;
            personDetailRepository = _personDetailRepository;
        }

        public async Task<bool> Amend(ContinentViewModel _continentViewModel)
        {
            try
            {
                // Set Default Value
                _continentViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _continentViewModel.EntryDateTime = DateTime.Now;
                _continentViewModel.EntryStatus = StringLiteralValue.Amend;
                _continentViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _continentViewModel.ReasonForModification = "None";
                _continentViewModel.TransReasonForModification = "None";
                _continentViewModel.UserAction = StringLiteralValue.Amend;
                _continentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get ParentPrmKey If Centeategory Is Subcontinent
                if (_continentViewModel.CenterCategoryPrmKey == 11)
                {
                    _continentViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_continentViewModel.ParentCenterId);
                }
                else
                {
                    _continentViewModel.ParentCenterPrmKey = 0;
                }

                // Mapping 
                // Center
                Center centerForAmend = Mapper.Map<Center>(_continentViewModel);
                CenterMakerChecker centerMakerCheckerForAmend = Mapper.Map<CenterMakerChecker>(_continentViewModel);

                // CenterModification
                CenterModification centerModificationForAmend = Mapper.Map<CenterModification>(_continentViewModel);
                CenterModificationMakerChecker centerModificationMakerCheckerForAmend = Mapper.Map<CenterModificationMakerChecker>(_continentViewModel);

                // CenterTranslation
                CenterTranslation centerTranslationForAmend = Mapper.Map<CenterTranslation>(_continentViewModel);
                CenterTranslationMakerChecker centerTranslationMakerCheckerForAmend = Mapper.Map<CenterTranslationMakerChecker>(_continentViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                centerForAmend.PrmKey = _continentViewModel.CenterPrmKey;
                centerModificationForAmend.PrmKey = _continentViewModel.CenterModificationPrmKey;
                centerTranslationForAmend.PrmKey = _continentViewModel.CenterTranslationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_continentViewModel.CenterModificationPrmKey == 0)
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

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(ContinentViewModel _continentViewModel)
        {
            try
            {
                // set Default Value
                _continentViewModel.EntryDateTime = DateTime.Now;
                _continentViewModel.Remark = "None";
                _continentViewModel.UserAction = StringLiteralValue.Delete;
                _continentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_continentViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_continentViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_continentViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_continentViewModel.CenterModificationPrmKey == 0)
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

                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<ContinentViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ContinentViewModel>("SELECT * FROM dbo.GetContinentEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<ContinentViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ContinentViewModel>("SELECT * FROM dbo.GetContinentEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<ContinentViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ContinentViewModel>("SELECT * FROM dbo.GetContinentEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<ContinentViewModel> GetRejectedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<ContinentViewModel>("SELECT * FROM dbo.GetContinentEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
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

        public Guid GetContinentIdByPrmKey(int _prmKey)
        {
            return context.Centers
                    .Where(c => c.PrmKey == _prmKey)
                    .Select(c => c.CenterId).FirstOrDefault();
        }

        public async Task<ContinentViewModel> GetUnVerifiedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<ContinentViewModel>("SELECT * FROM dbo.GetContinentEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ContinentViewModel> GetVerifiedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<ContinentViewModel>("SELECT * FROM dbo.GetContinentEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(ContinentViewModel _continentViewModel)
        {
            try
            {
                // Set Default Value
                _continentViewModel.EntryDateTime = DateTime.Now;
                _continentViewModel.EntryStatus = StringLiteralValue.Create;
                _continentViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _continentViewModel.Remark = "None";
                _continentViewModel.UserAction = StringLiteralValue.Create;
                _continentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_continentViewModel.CenterCategoryPrmKey == 11)
                {
                    _continentViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_continentViewModel.ParentCenterId);
                }
                else
                {
                    _continentViewModel.ParentCenterPrmKey = 0;
                }

                // CenterModification
                CenterModification centerModification = Mapper.Map<CenterModification>(_continentViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_continentViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_continentViewModel);

                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_continentViewModel);
                centerTranslationMakerChecker.CenterTranslationPrmKey = 0;
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

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(ContinentViewModel _continentViewModel)
        {
            try
            {
                // Set Default Value
                _continentViewModel.EntryDateTime = DateTime.Now;
                _continentViewModel.UserAction = StringLiteralValue.Reject;
                _continentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_continentViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_continentViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_continentViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_continentViewModel.CenterModificationPrmKey == 0)
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
        public async Task<bool> Save(ContinentViewModel _continentViewModel)
        {
            try
            {
                // Set Default Value
                _continentViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _continentViewModel.EntryDateTime = DateTime.Now;
                _continentViewModel.EntryStatus = StringLiteralValue.Create;
                _continentViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _continentViewModel.ReasonForModification = "None";
                _continentViewModel.TransReasonForModification = "None";
                _continentViewModel.Remark = "None";
                _continentViewModel.UserAction = StringLiteralValue.Create;
                _continentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_continentViewModel.CenterCategoryPrmKey == 11)
                {
                    _continentViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_continentViewModel.ParentCenterId);
                }
                else
                {
                    _continentViewModel.ParentCenterPrmKey = 0;
                }

                // Mapping
                // Center
                Center center = Mapper.Map<Center>(_continentViewModel);
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_continentViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_continentViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_continentViewModel);

                // CenterTranslation
                context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                context.Entry(centerTranslationMakerChecker).State = EntityState.Added;
                centerTranslation.CenterTranslationMakerCheckers.Add(centerTranslationMakerChecker);

                context.CenterTranslations.Attach(centerTranslation);
                context.Entry(centerTranslation).State = EntityState.Added;
                center.CenterTranslations.Add(centerTranslation);

                // Center
                context.CenterMakerCheckers.Attach(centerMakerChecker);
                context.Entry(centerMakerChecker).State = EntityState.Added;
                center.CenterMakerCheckers.Add(centerMakerChecker);

                context.Centers.Attach(center);
                context.Entry(center).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(ContinentViewModel _continentViewModel)
        {
            try
            {
                // Set Default Value
                _continentViewModel.EntryDateTime = DateTime.Now;
                _continentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _continentViewModel.CenterId = GetContinentIdByPrmKey(_continentViewModel.CenterPrmKey);

                if (_continentViewModel.CenterModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    ContinentViewModel continentViewModelForModify = await GetVerifiedEntry(_continentViewModel.CenterId);

                    // Set Default Value
                    continentViewModelForModify.UserAction = StringLiteralValue.Modify;
                    continentViewModelForModify.UserProfilePrmKey = _continentViewModel.UserProfilePrmKey;

                    // CenterTranslation
                    CenterTranslationMakerChecker centerTranslationMakerCheckerForModify = Mapper.Map<CenterTranslationMakerChecker>(continentViewModelForModify);

                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerCheckerForModify);
                    context.Entry(centerTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (continentViewModelForModify.IsModified == true)
                    {
                        CenterModificationMakerChecker centerModificationMakerCheckerForModify = Mapper.Map<CenterModificationMakerChecker>(continentViewModelForModify);

                        context.CenterModificationMakerCheckers.Attach(centerModificationMakerCheckerForModify);
                        context.Entry(centerModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _continentViewModel.UserAction = StringLiteralValue.Verify;

                    CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_continentViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_continentViewModel);

                    // CenterModificationMakerChecker
                    context.CenterModificationMakerCheckers.Attach(centerModificationMakerChecker);
                    context.Entry(centerModificationMakerChecker).State = EntityState.Added;

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                    context.Entry(centerTranslationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    _continentViewModel.UserAction = StringLiteralValue.Verify;

                    CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_continentViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_continentViewModel);

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                    context.Entry(centerTranslationMakerChecker).State = EntityState.Added;

                    // CenterMakerChecker
                    context.CenterMakerCheckers.Attach(centerMakerChecker);
                    context.Entry(centerMakerChecker).State = EntityState.Added;

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