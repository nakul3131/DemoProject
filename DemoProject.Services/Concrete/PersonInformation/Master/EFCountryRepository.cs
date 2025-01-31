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
using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.Abstract.PersonInformation.Master;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Domain.Entities.PersonInformation.Master;

namespace DemoProject.Services.Concrete.PersonInformation.Master
{
    public class EFCountryRepository : ICountryRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ICenterISOCodeRepository centerISOCodeRepository;
        private readonly ICountryAdditionalDetailRepository countryAdditionalDetailRepository;
        private readonly IOrganizationRepository organizationRepository;

        public EFCountryRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IPersonDetailRepository _personDetailRepository, ICenterISOCodeRepository _centerISOCodeRepository, ICountryAdditionalDetailRepository _countryAdditionalDetailRepository, IOrganizationRepository _organizationRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            personDetailRepository = _personDetailRepository;
            centerISOCodeRepository = _centerISOCodeRepository;
            countryAdditionalDetailRepository = _countryAdditionalDetailRepository;
            organizationRepository = _organizationRepository;
        }

        public async Task<bool> Amend(CountryViewModel _countryViewModel)
        {
            try
            {
                // Set Default Value
                _countryViewModel.ActivationStatus = StringLiteralValue.Active;
                _countryViewModel.CenterCategoryPrmKey = 10;
                _countryViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.EntryStatus = StringLiteralValue.Amend;
                _countryViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _countryViewModel.ReasonForModification = "None";
                _countryViewModel.UserAction = StringLiteralValue.Amend;
                _countryViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _countryViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Amend;
                _countryViewModel.CenterIsoCodeViewModel.ReasonForModification = "None";
                _countryViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Amend;
                _countryViewModel.CenterIsoCodeViewModel.Remark = _countryViewModel.Remark;
                _countryViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CountryAdditionalDetailViewModel
                _countryViewModel.CountryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.CountryAdditionalDetailViewModel.EntryStatus = StringLiteralValue.Amend;
                _countryViewModel.CountryAdditionalDetailViewModel.ReasonForModification = "None";
                _countryViewModel.CountryAdditionalDetailViewModel.UserAction = StringLiteralValue.Amend;
                _countryViewModel.CountryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _countryViewModel.CountryAdditionalDetailViewModel.Remark = _countryViewModel.Remark;

                // Set ReferenceKey As PrmKey To Normal Tables
                _countryViewModel.CenterIsoCodeViewModel.CenterPrmKey = _countryViewModel.CenterPrmKey;
                _countryViewModel.CountryAdditionalDetailViewModel.CenterPrmKey = _countryViewModel.CenterPrmKey;

                // Get PrmKey By Id Of All Dropdowns
                _countryViewModel.CountryAdditionalDetailViewModel.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_countryViewModel.CountryAdditionalDetailViewModel.CurrencyId);
                _countryViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_countryViewModel.ParentCenterId);
                _countryViewModel.CountryAdditionalDetailViewModel.WorldWideTimeZonePrmKey = personDetailRepository.GetWorldWideTimeZonePrmKeyById(_countryViewModel.CountryAdditionalDetailViewModel.WorldWideTimeZoneId);

                // Mapping 
                // Center
                Center centerForAmend = Mapper.Map<Center>(_countryViewModel);
                CenterMakerChecker centerMakerCheckerForAmend = Mapper.Map<CenterMakerChecker>(_countryViewModel);

                // CenterModification
                CenterModification centerModificationForAmend = Mapper.Map<CenterModification>(_countryViewModel);
                CenterModificationMakerChecker centerModificationMakerCheckerForAmend = Mapper.Map<CenterModificationMakerChecker>(_countryViewModel);

                // CenterTranslation
                CenterTranslation centerTranslationForAmend = Mapper.Map<CenterTranslation>(_countryViewModel);
                CenterTranslationMakerChecker centerTranslationMakerCheckerForAmend = Mapper.Map<CenterTranslationMakerChecker>(_countryViewModel);

                // CenterISOCode
                CenterISOCode centerISOCodeForAmend = Mapper.Map<CenterISOCode>(_countryViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerCheckerForAmend = Mapper.Map<CenterISOCodeMakerChecker>(_countryViewModel.CenterIsoCodeViewModel);

                // CountryAdditionalDetail
                CountryAdditionalDetail countryAdditionalDetailForAmend = Mapper.Map<CountryAdditionalDetail>(_countryViewModel.CountryAdditionalDetailViewModel);
                CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerCheckerForAmend = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryViewModel.CountryAdditionalDetailViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                centerForAmend.PrmKey = _countryViewModel.CenterPrmKey;
                centerModificationForAmend.PrmKey = _countryViewModel.CenterModificationPrmKey;
                centerTranslationForAmend.PrmKey = _countryViewModel.CenterTranslationPrmKey;
                centerISOCodeForAmend.PrmKey = _countryViewModel.CenterIsoCodeViewModel.CenterISOCodePrmKey;
                countryAdditionalDetailForAmend.PrmKey = _countryViewModel.CountryAdditionalDetailViewModel.CountryAdditionalDetailPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_countryViewModel.CenterModificationPrmKey == 0)
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

                // CountryAdditionDetail
                context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerCheckerForAmend);
                context.Entry(countryAdditionalDetailMakerCheckerForAmend).State = EntityState.Added;
                countryAdditionalDetailForAmend.CountryAdditionalDetailMakerCheckers.Add(countryAdditionalDetailMakerCheckerForAmend);

                context.CountryAdditionalDetails.Attach(countryAdditionalDetailForAmend);
                context.Entry(countryAdditionalDetailForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(CountryViewModel _countryViewModel)
        {
            try
            {
                // Set Default Value 
                _countryViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.UserAction = StringLiteralValue.Delete;
                _countryViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //  CenterIsoCodeViewModel
                _countryViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.CenterIsoCodeViewModel.Remark = _countryViewModel.Remark;
                _countryViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Delete;
                _countryViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //  CountryAdditionalDetailViewModel
                _countryViewModel.CountryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.CountryAdditionalDetailViewModel.Remark = _countryViewModel.Remark;
                _countryViewModel.CountryAdditionalDetailViewModel.UserAction = StringLiteralValue.Delete;
                _countryViewModel.CountryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_countryViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_countryViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_countryViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_countryViewModel.CenterIsoCodeViewModel);
                CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerChecker = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryViewModel.CountryAdditionalDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_countryViewModel.CenterModificationPrmKey == 0)
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

                // CountryAdditionalDetail
                context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerChecker);
                context.Entry(countryAdditionalDetailMakerChecker).State = EntityState.Added;

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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetCountryEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetCountryEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
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
                return await context.Database.SqlQuery<CenterIndexViewModel>("SELECT * FROM dbo.GetCountryEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<CountryViewModel> GetRejectedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<CountryViewModel>("SELECT * FROM dbo.GetCountryEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();

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
            if (context.Centers.Where(p => p.NameOfCenter == NameOfCenter && p.CenterCategoryPrmKey == 10).Select(p => p.PrmKey).FirstOrDefault() > 0)
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

        public async Task<CountryViewModel> GetUnVerifiedEntry(Guid _centerId)
        {
            try
            {
                var a = await context.Database.SqlQuery<CountryViewModel>("SELECT * FROM dbo.GetCountryEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CountryViewModel> GetVerifiedEntry(Guid _centerId)
        {
            try
            {
                return await context.Database.SqlQuery<CountryViewModel>("SELECT * FROM dbo.GetCountryEntry (@CenterId, @EntryType)", new SqlParameter("@CenterId", _centerId), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(CountryViewModel _countryViewModel)
        {
            try
            {
                // Set Default Value
                _countryViewModel.CenterTranslationPrmKey = 0;
                _countryViewModel.CenterModificationPrmKey = 0;
                _countryViewModel.CenterCategoryPrmKey = 10;
                _countryViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.EntryStatus = StringLiteralValue.Create;
                _countryViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _countryViewModel.Remark = "None";
                _countryViewModel.UserAction = StringLiteralValue.Create;
                _countryViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _countryViewModel.CenterIsoCodeViewModel.CenterISOCodePrmKey = 0;
                _countryViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Create;
                _countryViewModel.CenterIsoCodeViewModel.Remark = "None";
                _countryViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Create;
                _countryViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CountryAdditionalDetailViewModel
                _countryViewModel.CountryAdditionalDetailViewModel.CountryAdditionalDetailPrmKey = 0;
                _countryViewModel.CountryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.CountryAdditionalDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _countryViewModel.CountryAdditionalDetailViewModel.Remark = "None";
                _countryViewModel.CountryAdditionalDetailViewModel.UserAction = StringLiteralValue.Create;
                _countryViewModel.CountryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Set ReferenceKey As PrmKey To Normal Tables
                _countryViewModel.CenterIsoCodeViewModel.CenterPrmKey = _countryViewModel.CenterPrmKey;
                _countryViewModel.CountryAdditionalDetailViewModel.CenterPrmKey = _countryViewModel.CenterPrmKey;

                // Get PrmKey By Id Of All Dropdowns 
                _countryViewModel.CountryAdditionalDetailViewModel.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_countryViewModel.CountryAdditionalDetailViewModel.CurrencyId);
                _countryViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_countryViewModel.ParentCenterId);
                _countryViewModel.CountryAdditionalDetailViewModel.WorldWideTimeZonePrmKey = personDetailRepository.GetWorldWideTimeZonePrmKeyById(_countryViewModel.CountryAdditionalDetailViewModel.WorldWideTimeZoneId);

                // CenterModification
                CenterModification centerModification = Mapper.Map<CenterModification>(_countryViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_countryViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_countryViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_countryViewModel);

                // CenterISOCode
                CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_countryViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_countryViewModel.CenterIsoCodeViewModel);

                // CountryAdditionalDetail
                CountryAdditionalDetail countryAdditionalDetail = Mapper.Map<CountryAdditionalDetail>(_countryViewModel.CountryAdditionalDetailViewModel);
                CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerChecker = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryViewModel.CountryAdditionalDetailViewModel);

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

                // CountryAdditionalDetail
                context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerChecker);
                context.Entry(countryAdditionalDetailMakerChecker).State = EntityState.Added;
                countryAdditionalDetail.CountryAdditionalDetailMakerCheckers.Add(countryAdditionalDetailMakerChecker);

                context.CountryAdditionalDetails.Attach(countryAdditionalDetail);
                context.Entry(countryAdditionalDetail).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(CountryViewModel _countryViewModel)
        {
            try
            {
                // Set Default Value.
                _countryViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.UserAction = StringLiteralValue.Reject;
                _countryViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _countryViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Reject;
                _countryViewModel.CenterIsoCodeViewModel.Remark = _countryViewModel.Remark;
                _countryViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _countryViewModel.CountryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.CountryAdditionalDetailViewModel.UserAction = StringLiteralValue.Reject;
                _countryViewModel.CountryAdditionalDetailViewModel.Remark = _countryViewModel.Remark;
                _countryViewModel.CountryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_countryViewModel);
                CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_countryViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_countryViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_countryViewModel.CenterIsoCodeViewModel);
                CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerChecker = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryViewModel.CountryAdditionalDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_countryViewModel.CenterModificationPrmKey == 0)
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

                // CountryAdditionalDetailMakerChecker
                context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerChecker);
                context.Entry(countryAdditionalDetailMakerChecker).State = EntityState.Added;

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
        public async Task<bool> Save(CountryViewModel _countryViewModel)
        {
            try
            {
                // Set Default Value
                _countryViewModel.ActivationStatus = StringLiteralValue.Active;
                _countryViewModel.CenterCategoryPrmKey = 10;
                _countryViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.EntryStatus = StringLiteralValue.Create;
                _countryViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _countryViewModel.ReasonForModification = "None";
                _countryViewModel.Remark = "None";
                _countryViewModel.UserAction = StringLiteralValue.Create;
                _countryViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _countryViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.CenterIsoCodeViewModel.EntryStatus = StringLiteralValue.Create;
                _countryViewModel.CenterIsoCodeViewModel.ReasonForModification = "None";
                _countryViewModel.CenterIsoCodeViewModel.Remark = "None";
                _countryViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Create;
                _countryViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CountryAdditionalDetailViewModel
                _countryViewModel.CountryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.CountryAdditionalDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _countryViewModel.CountryAdditionalDetailViewModel.ReasonForModification = "None";
                _countryViewModel.CountryAdditionalDetailViewModel.Remark = "None";
                _countryViewModel.CountryAdditionalDetailViewModel.UserAction = StringLiteralValue.Create;
                _countryViewModel.CountryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _countryViewModel.CountryAdditionalDetailViewModel.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_countryViewModel.CountryAdditionalDetailViewModel.CurrencyId);
                _countryViewModel.ParentCenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_countryViewModel.ParentCenterId);
                _countryViewModel.CountryAdditionalDetailViewModel.WorldWideTimeZonePrmKey = personDetailRepository.GetWorldWideTimeZonePrmKeyById(_countryViewModel.CountryAdditionalDetailViewModel.WorldWideTimeZoneId);

                // Mapping
                // Center
                Center center = Mapper.Map<Center>(_countryViewModel);
                CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_countryViewModel);

                // CenterTranslation
                CenterTranslation centerTranslation = Mapper.Map<CenterTranslation>(_countryViewModel);
                CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_countryViewModel);

                // CenterISOCode
                CenterISOCode centerISOCode = Mapper.Map<CenterISOCode>(_countryViewModel.CenterIsoCodeViewModel);
                CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_countryViewModel.CenterIsoCodeViewModel);

                // CountryAdditionalDetail
                CountryAdditionalDetail countryAdditionalDetail = Mapper.Map<CountryAdditionalDetail>(_countryViewModel.CountryAdditionalDetailViewModel);
                CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerChecker = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryViewModel.CountryAdditionalDetailViewModel);

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

                // CountryAdditionalDetail
                context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerChecker);
                context.Entry(countryAdditionalDetailMakerChecker).State = EntityState.Added;
                countryAdditionalDetail.CountryAdditionalDetailMakerCheckers.Add(countryAdditionalDetailMakerChecker);

                context.CountryAdditionalDetails.Attach(countryAdditionalDetail);
                context.Entry(countryAdditionalDetail).State = EntityState.Added;
                center.CountryAdditionalDetails.Add(countryAdditionalDetail);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(CountryViewModel _countryViewModel)
        {
            try
            {
                // Set Default Value
                _countryViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _countryViewModel.CenterId = GetCenterIdByPrmKey(_countryViewModel.CenterPrmKey);

                // CenterIsoCodeViewModel
                _countryViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;
                _countryViewModel.CenterIsoCodeViewModel.Remark = _countryViewModel.Remark;
                _countryViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CenterIsoCodeViewModel
                _countryViewModel.CountryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                _countryViewModel.CountryAdditionalDetailViewModel.UserAction = StringLiteralValue.Verify;
                _countryViewModel.CountryAdditionalDetailViewModel.Remark = _countryViewModel.Remark;
                _countryViewModel.CountryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_countryViewModel.CenterModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    CountryViewModel countryViewModelForModify = await GetVerifiedEntry(_countryViewModel.CenterId);
                    CenterIsoCodeViewModel centerIsoCodeViewModelForModify = await centerISOCodeRepository.GetVerifiedEntry(_countryViewModel.CenterPrmKey);
                    CountryAdditionalDetailViewModel countryAdditionalDetailViewModelForModify = await countryAdditionalDetailRepository.GetVerifiedEntry(_countryViewModel.CenterPrmKey);

                    // Set Default Value
                    countryViewModelForModify.UserAction = StringLiteralValue.Modify;
                    countryViewModelForModify.UserProfilePrmKey = _countryViewModel.UserProfilePrmKey;

                    centerIsoCodeViewModelForModify.UserAction = StringLiteralValue.Modify;
                    centerIsoCodeViewModelForModify.UserProfilePrmKey = _countryViewModel.UserProfilePrmKey;

                    countryAdditionalDetailViewModelForModify.UserAction = StringLiteralValue.Modify;
                    countryAdditionalDetailViewModelForModify.UserProfilePrmKey = _countryViewModel.UserProfilePrmKey;

                    // Mapping
                    CenterTranslationMakerChecker centerTranslationMakerCheckerForModify = Mapper.Map<CenterTranslationMakerChecker>(countryViewModelForModify);
                    CenterISOCodeMakerChecker centerISOCodeMakerCheckerForModify = Mapper.Map<CenterISOCodeMakerChecker>(centerIsoCodeViewModelForModify);
                    CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerCheckerForModify = Mapper.Map<CountryAdditionalDetailMakerChecker>(countryAdditionalDetailViewModelForModify);

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerCheckerForModify);
                    context.Entry(centerTranslationMakerCheckerForModify).State = EntityState.Added;

                    // CenterISOCodeMakerChecker
                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerCheckerForModify);
                    context.Entry(centerISOCodeMakerCheckerForModify).State = EntityState.Added;

                    // CountryAdditionalDetailMakerChecker
                    context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerCheckerForModify);
                    context.Entry(countryAdditionalDetailMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (countryViewModelForModify.IsModified == true)
                    {
                        CenterModificationMakerChecker centerModificationMakerCheckerForModify = Mapper.Map<CenterModificationMakerChecker>(countryViewModelForModify);

                        context.CenterModificationMakerCheckers.Attach(centerModificationMakerCheckerForModify);
                        context.Entry(centerModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _countryViewModel.UserAction = StringLiteralValue.Verify;
                    _countryViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;
                    _countryViewModel.CountryAdditionalDetailViewModel.UserAction = StringLiteralValue.Verify;

                    CenterModificationMakerChecker centerModificationMakerChecker = Mapper.Map<CenterModificationMakerChecker>(_countryViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_countryViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_countryViewModel.CenterIsoCodeViewModel);
                    CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerChecker = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryViewModel.CountryAdditionalDetailViewModel);

                    // CenterModificationMakerChecker
                    context.CenterModificationMakerCheckers.Attach(centerModificationMakerChecker);
                    context.Entry(centerModificationMakerChecker).State = EntityState.Added;

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                    context.Entry(centerTranslationMakerChecker).State = EntityState.Added;

                    // CenterISOCodeMakerChecker
                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                    context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;

                    // CountryAdditionalDetailMakerChecker
                    context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerChecker);
                    context.Entry(countryAdditionalDetailMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // Set Default Value
                    _countryViewModel.UserAction = StringLiteralValue.Verify;

                    // CenterIsoCodeViewModel
                    _countryViewModel.CenterIsoCodeViewModel.EntryDateTime = DateTime.Now;
                    _countryViewModel.CenterIsoCodeViewModel.UserAction = StringLiteralValue.Verify;
                    _countryViewModel.CenterIsoCodeViewModel.Remark = _countryViewModel.Remark;
                    _countryViewModel.CenterIsoCodeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // CountryAdditionalDetailViewModel
                    _countryViewModel.CountryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                    _countryViewModel.CountryAdditionalDetailViewModel.UserAction = StringLiteralValue.Verify;
                    _countryViewModel.CountryAdditionalDetailViewModel.Remark = _countryViewModel.Remark;
                    _countryViewModel.CountryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CenterMakerChecker centerMakerChecker = Mapper.Map<CenterMakerChecker>(_countryViewModel);
                    CenterTranslationMakerChecker centerTranslationMakerChecker = Mapper.Map<CenterTranslationMakerChecker>(_countryViewModel);
                    CenterISOCodeMakerChecker centerISOCodeMakerChecker = Mapper.Map<CenterISOCodeMakerChecker>(_countryViewModel.CenterIsoCodeViewModel);
                    CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerChecker = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryViewModel.CountryAdditionalDetailViewModel);

                    // CenterMakerChecker
                    context.CenterMakerCheckers.Attach(centerMakerChecker);
                    context.Entry(centerMakerChecker).State = EntityState.Added;

                    // CenterTranslationMakerChecker
                    context.CenterTranslationMakerCheckers.Attach(centerTranslationMakerChecker);
                    context.Entry(centerTranslationMakerChecker).State = EntityState.Added;

                    // CenterISOCodeMakerChecker
                    context.CenterISOCodeMakerCheckers.Attach(centerISOCodeMakerChecker);
                    context.Entry(centerISOCodeMakerChecker).State = EntityState.Added;

                    // CountryAdditionalDetailMakerChecker
                    context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerChecker);
                    context.Entry(countryAdditionalDetailMakerChecker).State = EntityState.Added;
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