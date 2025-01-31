using AutoMapper;
using DemoProject.Domain.Entities.Enterprise.Office;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.Services.Wrapper;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFBusinessOfficeCoopRegistrationRepository : IBusinessOfficeCoopRegistrationRepository
    {
        private readonly EFDbContext context;
        private readonly IOfficeDetailRepository officeDetailRepository;

        public EFBusinessOfficeCoopRegistrationRepository(RepositoryConnection _connection, IOfficeDetailRepository _officeDetailRepository)
        {
            context = _connection.EFDbContext;
            officeDetailRepository = _officeDetailRepository;
        }

        public async Task<bool> Amend(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeCoopRegistrationViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeCoopRegistrationViewModel.EntryStatus = StringLiteralValue.Amend;
                _businessOfficeCoopRegistrationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _businessOfficeCoopRegistrationViewModel.ReasonForModification = "None";
                _businessOfficeCoopRegistrationViewModel.Remark = "None";
                _businessOfficeCoopRegistrationViewModel.UserAction = StringLiteralValue.Amend;
                _businessOfficeCoopRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeCoopRegistration businessOfficeCoopRegistration = Mapper.Map<BusinessOfficeCoopRegistration>(_businessOfficeCoopRegistrationViewModel);

                _businessOfficeCoopRegistrationViewModel.PrmKey = 0;
                BusinessOfficeCoopRegistrationMakerChecker businessOfficeCoopRegistrationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationMakerChecker>(_businessOfficeCoopRegistrationViewModel);

                BusinessOfficeCoopRegistrationTranslation businessOfficeCoopRegistrationTranslation = Mapper.Map<BusinessOfficeCoopRegistrationTranslation>(_businessOfficeCoopRegistrationViewModel);

                BusinessOfficeCoopRegistrationTranslationMakerChecker businessOfficeCoopRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationTranslationMakerChecker>(_businessOfficeCoopRegistrationViewModel);
                businessOfficeCoopRegistrationTranslation.PrmKey = _businessOfficeCoopRegistrationViewModel.BusinessOfficeCoopRegistrationTranslationPrmKey;

                context.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Attach(businessOfficeCoopRegistrationTranslationMakerChecker);
                context.Entry(businessOfficeCoopRegistrationTranslationMakerChecker).State = EntityState.Added;
                businessOfficeCoopRegistrationTranslation.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Add(businessOfficeCoopRegistrationTranslationMakerChecker);

                context.BusinessOfficeCoopRegistrationTranslations.Attach(businessOfficeCoopRegistrationTranslation);
                context.Entry(businessOfficeCoopRegistrationTranslation).State = EntityState.Modified;

                context.BusinessOfficeCoopRegistrationMakerCheckers.Attach(businessOfficeCoopRegistrationMakerChecker);
                context.Entry(businessOfficeCoopRegistrationMakerChecker).State = EntityState.Added;
                businessOfficeCoopRegistration.BusinessOfficeCoopRegistrationMakerCheckers.Add(businessOfficeCoopRegistrationMakerChecker);

                context.BusinessOfficeCoopRegistrations.Attach(businessOfficeCoopRegistration);
                context.Entry(businessOfficeCoopRegistration).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeCoopRegistrationViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeCoopRegistrationViewModel.Remark = "None";
                _businessOfficeCoopRegistrationViewModel.UserAction = StringLiteralValue.Delete;
                _businessOfficeCoopRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeCoopRegistrationMakerChecker businessOfficeCoopRegistrationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationMakerChecker>(_businessOfficeCoopRegistrationViewModel);

                BusinessOfficeCoopRegistrationTranslationMakerChecker businessOfficeCoopRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationTranslationMakerChecker>(_businessOfficeCoopRegistrationViewModel);

                context.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Attach(businessOfficeCoopRegistrationTranslationMakerChecker);
                context.Entry(businessOfficeCoopRegistrationTranslationMakerChecker).State = EntityState.Added;

                context.BusinessOfficeCoopRegistrationMakerCheckers.Attach(businessOfficeCoopRegistrationMakerChecker);
                context.Entry(businessOfficeCoopRegistrationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Modify(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeCoopRegistrationViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeCoopRegistrationViewModel.EntryStatus = StringLiteralValue.Create;
                _businessOfficeCoopRegistrationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _businessOfficeCoopRegistrationViewModel.ReasonForModification = "None";
                _businessOfficeCoopRegistrationViewModel.Remark = "None";
                _businessOfficeCoopRegistrationViewModel.BusinessOfficeCoopRegistrationTranslationPrmKey = 0;
                _businessOfficeCoopRegistrationViewModel.UserAction = StringLiteralValue.Create;
                _businessOfficeCoopRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeCoopRegistration businessOfficeCoopRegistration = Mapper.Map<BusinessOfficeCoopRegistration>(_businessOfficeCoopRegistrationViewModel);

                BusinessOfficeCoopRegistrationMakerChecker businessOfficeCoopRegistrationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationMakerChecker>(_businessOfficeCoopRegistrationViewModel);

                BusinessOfficeCoopRegistrationTranslation businessOfficeCoopRegistrationTranslation = Mapper.Map<BusinessOfficeCoopRegistrationTranslation>(_businessOfficeCoopRegistrationViewModel);

                BusinessOfficeCoopRegistrationTranslationMakerChecker businessOfficeCoopRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationTranslationMakerChecker>(_businessOfficeCoopRegistrationViewModel);

                context.BusinessOfficeCoopRegistrationMakerCheckers.Attach(businessOfficeCoopRegistrationMakerChecker);
                context.Entry(businessOfficeCoopRegistrationMakerChecker).State = EntityState.Added;
                businessOfficeCoopRegistration.BusinessOfficeCoopRegistrationMakerCheckers.Add(businessOfficeCoopRegistrationMakerChecker);

                context.BusinessOfficeCoopRegistrations.Attach(businessOfficeCoopRegistration);
                context.Entry(businessOfficeCoopRegistration).State = EntityState.Added;

                context.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Attach(businessOfficeCoopRegistrationTranslationMakerChecker);
                context.Entry(businessOfficeCoopRegistrationTranslationMakerChecker).State = EntityState.Added;
                businessOfficeCoopRegistrationTranslation.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Add(businessOfficeCoopRegistrationTranslationMakerChecker);

                context.BusinessOfficeCoopRegistrationTranslations.Attach(businessOfficeCoopRegistrationTranslation);
                context.Entry(businessOfficeCoopRegistrationTranslation).State = EntityState.Added;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeCoopRegistrationViewModel.PrmKey = 0;
                _businessOfficeCoopRegistrationViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeCoopRegistrationViewModel.UserAction = StringLiteralValue.Reject;
                _businessOfficeCoopRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeCoopRegistrationMakerChecker businessOfficeCoopRegistrationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationMakerChecker>(_businessOfficeCoopRegistrationViewModel);

                BusinessOfficeCoopRegistrationTranslationMakerChecker businessOfficeCoopRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationTranslationMakerChecker>(_businessOfficeCoopRegistrationViewModel);

                context.BusinessOfficeCoopRegistrationMakerCheckers.Attach(businessOfficeCoopRegistrationMakerChecker);
                context.Entry(businessOfficeCoopRegistrationMakerChecker).State = EntityState.Added;

                context.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Attach(businessOfficeCoopRegistrationTranslationMakerChecker);
                context.Entry(businessOfficeCoopRegistrationTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeCoopRegistrationViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeCoopRegistrationViewModel.EntryStatus = StringLiteralValue.Create;
                _businessOfficeCoopRegistrationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _businessOfficeCoopRegistrationViewModel.ReasonForModification = "None";
                _businessOfficeCoopRegistrationViewModel.Remark = "None";
                _businessOfficeCoopRegistrationViewModel.UserAction = StringLiteralValue.Create;
                _businessOfficeCoopRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeCoopRegistration businessOfficeCoopRegistration = Mapper.Map<BusinessOfficeCoopRegistration>(_businessOfficeCoopRegistrationViewModel);

                BusinessOfficeCoopRegistrationMakerChecker businessOfficeCoopRegistrationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationMakerChecker>(_businessOfficeCoopRegistrationViewModel);

                BusinessOfficeCoopRegistrationTranslation businessOfficeCoopRegistrationTranslation = Mapper.Map<BusinessOfficeCoopRegistrationTranslation>(_businessOfficeCoopRegistrationViewModel);

                BusinessOfficeCoopRegistrationTranslationMakerChecker businessOfficeCoopRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationTranslationMakerChecker>(_businessOfficeCoopRegistrationViewModel);

                context.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Attach(businessOfficeCoopRegistrationTranslationMakerChecker);
                context.Entry(businessOfficeCoopRegistrationTranslationMakerChecker).State = EntityState.Added;
                businessOfficeCoopRegistrationTranslation.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Add(businessOfficeCoopRegistrationTranslationMakerChecker);

                context.BusinessOfficeCoopRegistrationTranslations.Attach(businessOfficeCoopRegistrationTranslation);
                context.Entry(businessOfficeCoopRegistrationTranslation).State = EntityState.Added;
                businessOfficeCoopRegistration.BusinessOfficeCoopRegistrationTranslations.Add(businessOfficeCoopRegistrationTranslation);

                context.BusinessOfficeCoopRegistrationMakerCheckers.Attach(businessOfficeCoopRegistrationMakerChecker);
                context.Entry(businessOfficeCoopRegistrationMakerChecker).State = EntityState.Added;
                businessOfficeCoopRegistration.BusinessOfficeCoopRegistrationMakerCheckers.Add(businessOfficeCoopRegistrationMakerChecker);

                context.BusinessOfficeCoopRegistrations.Attach(businessOfficeCoopRegistration);
                context.Entry(businessOfficeCoopRegistration).State = EntityState.Added;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel)
        {
            try
            {
                // First Modify Record - Get Active Record (i.e. Whose Entry Status is Verified)                 
                BusinessOfficeCoopRegistrationViewModel businessOfficeCoopRegistrationViewModelOldEntry = await officeDetailRepository.GetCoopRegistrationEntry(_businessOfficeCoopRegistrationViewModel.BusinessOfficePrmKey,StringLiteralValue.Verify);
                if (businessOfficeCoopRegistrationViewModelOldEntry != null)
                {
                    // Set Default Value
                    businessOfficeCoopRegistrationViewModelOldEntry.EntryDateTime = DateTime.Now;
                    businessOfficeCoopRegistrationViewModelOldEntry.UserAction = StringLiteralValue.Modify;
                    businessOfficeCoopRegistrationViewModelOldEntry.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    BusinessOfficeCoopRegistrationMakerChecker businessOfficeCoopRegistrationMakerCheckerForModify = Mapper.Map<BusinessOfficeCoopRegistrationMakerChecker>(businessOfficeCoopRegistrationViewModelOldEntry);

                    context.BusinessOfficeCoopRegistrationMakerCheckers.Attach(businessOfficeCoopRegistrationMakerCheckerForModify);
                    context.Entry(businessOfficeCoopRegistrationMakerCheckerForModify).State = EntityState.Added;

                    BusinessOfficeCoopRegistrationTranslationMakerChecker businessOfficeCoopRegistrationTranslationMakerCheckerForModify = Mapper.Map<BusinessOfficeCoopRegistrationTranslationMakerChecker>(businessOfficeCoopRegistrationViewModelOldEntry);

                    context.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Attach(businessOfficeCoopRegistrationTranslationMakerCheckerForModify);
                    context.Entry(businessOfficeCoopRegistrationTranslationMakerCheckerForModify).State = EntityState.Added;
                }

                //Verify New Record
                // Set Default Value
                _businessOfficeCoopRegistrationViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeCoopRegistrationViewModel.UserAction = StringLiteralValue.Verify;
                _businessOfficeCoopRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeCoopRegistrationMakerChecker businessOfficeCoopRegistrationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationMakerChecker>(_businessOfficeCoopRegistrationViewModel);

                BusinessOfficeCoopRegistrationTranslationMakerChecker businessOfficeCoopRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationTranslationMakerChecker>(_businessOfficeCoopRegistrationViewModel);

                context.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Attach(businessOfficeCoopRegistrationTranslationMakerChecker);
                context.Entry(businessOfficeCoopRegistrationTranslationMakerChecker).State = EntityState.Added;

                context.BusinessOfficeCoopRegistrationMakerCheckers.Attach(businessOfficeCoopRegistrationMakerChecker);
                context.Entry(businessOfficeCoopRegistrationMakerChecker).State = EntityState.Added;

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
