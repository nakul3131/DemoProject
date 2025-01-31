using AutoMapper;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Domain.Entities.Enterprise.Office;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Enterprise.Office
{

    public class EFBusinessOfficeRBIRegistrationRepository : IBusinessOfficeRBIRegistrationRepository
    {
        private readonly EFDbContext context;
        private readonly IOfficeDetailRepository officeDetailRepository;

        public EFBusinessOfficeRBIRegistrationRepository(RepositoryConnection _connection, IOfficeDetailRepository _officeDetailRepository)
        {
            context = _connection.EFDbContext;
            officeDetailRepository = _officeDetailRepository;
        }

        public async Task<bool> Amend(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeRBIRegistrationViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeRBIRegistrationViewModel.EntryStatus = StringLiteralValue.Amend;
                _businessOfficeRBIRegistrationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _businessOfficeRBIRegistrationViewModel.ReasonForModification = "None";
                _businessOfficeRBIRegistrationViewModel.Remark = "None";
                _businessOfficeRBIRegistrationViewModel.UserAction = StringLiteralValue.Amend;
                _businessOfficeRBIRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeRBIRegistration businessOfficeRBIRegistration = Mapper.Map<BusinessOfficeRBIRegistration>(_businessOfficeRBIRegistrationViewModel);

                _businessOfficeRBIRegistrationViewModel.PrmKey = 0;
                BusinessOfficeRBIRegistrationMakerChecker businessOfficeRBIRegistrationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                BusinessOfficeRBIRegistrationTranslation businessOfficeRBIRegistrationTranslation = Mapper.Map<BusinessOfficeRBIRegistrationTranslation>(_businessOfficeRBIRegistrationViewModel);

                BusinessOfficeRBIRegistrationTranslationMakerChecker businessOfficeRBIRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationTranslationMakerChecker>(_businessOfficeRBIRegistrationViewModel);
                businessOfficeRBIRegistrationTranslation.PrmKey = _businessOfficeRBIRegistrationViewModel.BusinessOfficeRBIRegistrationTranslationPrmKey;

                context.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Attach(businessOfficeRBIRegistrationTranslationMakerChecker);
                context.Entry(businessOfficeRBIRegistrationTranslationMakerChecker).State = EntityState.Added;
                businessOfficeRBIRegistrationTranslation.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Add(businessOfficeRBIRegistrationTranslationMakerChecker);

                context.BusinessOfficeRBIRegistrationTranslations.Attach(businessOfficeRBIRegistrationTranslation);
                context.Entry(businessOfficeRBIRegistrationTranslation).State = EntityState.Modified;

                context.BusinessOfficeRBIRegistrationMakerCheckers.Attach(businessOfficeRBIRegistrationMakerChecker);
                context.Entry(businessOfficeRBIRegistrationMakerChecker).State = EntityState.Added;
                businessOfficeRBIRegistration.BusinessOfficeRBIRegistrationMakerCheckers.Add(businessOfficeRBIRegistrationMakerChecker);

                context.BusinessOfficeRBIRegistrations.Attach(businessOfficeRBIRegistration);
                context.Entry(businessOfficeRBIRegistration).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeRBIRegistrationViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeRBIRegistrationViewModel.Remark = "None";
                _businessOfficeRBIRegistrationViewModel.UserAction = StringLiteralValue.Delete;
                _businessOfficeRBIRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeRBIRegistrationMakerChecker businessOfficeRBIRegistrationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                BusinessOfficeRBIRegistrationTranslationMakerChecker businessOfficeRBIRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationTranslationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                context.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Attach(businessOfficeRBIRegistrationTranslationMakerChecker);
                context.Entry(businessOfficeRBIRegistrationTranslationMakerChecker).State = EntityState.Added;

                context.BusinessOfficeRBIRegistrationMakerCheckers.Attach(businessOfficeRBIRegistrationMakerChecker);
                context.Entry(businessOfficeRBIRegistrationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Modify(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeRBIRegistrationViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeRBIRegistrationViewModel.EntryStatus = StringLiteralValue.Create;
                _businessOfficeRBIRegistrationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _businessOfficeRBIRegistrationViewModel.ReasonForModification = "None";
                _businessOfficeRBIRegistrationViewModel.Remark = "None";
                _businessOfficeRBIRegistrationViewModel.BusinessOfficeRBIRegistrationTranslationPrmKey = 0;
                _businessOfficeRBIRegistrationViewModel.UserAction = StringLiteralValue.Create;
                _businessOfficeRBIRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeRBIRegistration businessOfficeRBIRegistration = Mapper.Map<BusinessOfficeRBIRegistration>(_businessOfficeRBIRegistrationViewModel);

                BusinessOfficeRBIRegistrationMakerChecker businessOfficeRBIRegistrationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                BusinessOfficeRBIRegistrationTranslation businessOfficeRBIRegistrationTranslation = Mapper.Map<BusinessOfficeRBIRegistrationTranslation>(_businessOfficeRBIRegistrationViewModel);

                BusinessOfficeRBIRegistrationTranslationMakerChecker businessOfficeRBIRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationTranslationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                context.BusinessOfficeRBIRegistrationMakerCheckers.Attach(businessOfficeRBIRegistrationMakerChecker);
                context.Entry(businessOfficeRBIRegistrationMakerChecker).State = EntityState.Added;
                businessOfficeRBIRegistration.BusinessOfficeRBIRegistrationMakerCheckers.Add(businessOfficeRBIRegistrationMakerChecker);

                context.BusinessOfficeRBIRegistrations.Attach(businessOfficeRBIRegistration);
                context.Entry(businessOfficeRBIRegistration).State = EntityState.Added;

                context.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Attach(businessOfficeRBIRegistrationTranslationMakerChecker);
                context.Entry(businessOfficeRBIRegistrationTranslationMakerChecker).State = EntityState.Added;
                businessOfficeRBIRegistrationTranslation.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Add(businessOfficeRBIRegistrationTranslationMakerChecker);

                context.BusinessOfficeRBIRegistrationTranslations.Attach(businessOfficeRBIRegistrationTranslation);
                context.Entry(businessOfficeRBIRegistrationTranslation).State = EntityState.Added;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeRBIRegistrationViewModel.PrmKey = 0;
                _businessOfficeRBIRegistrationViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeRBIRegistrationViewModel.UserAction = StringLiteralValue.Reject;
                _businessOfficeRBIRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeRBIRegistrationMakerChecker businessOfficeRBIRegistrationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                BusinessOfficeRBIRegistrationTranslationMakerChecker businessOfficeRBIRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationTranslationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                context.BusinessOfficeRBIRegistrationMakerCheckers.Attach(businessOfficeRBIRegistrationMakerChecker);
                context.Entry(businessOfficeRBIRegistrationMakerChecker).State = EntityState.Added;

                context.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Attach(businessOfficeRBIRegistrationTranslationMakerChecker);
                context.Entry(businessOfficeRBIRegistrationTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeRBIRegistrationViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeRBIRegistrationViewModel.EntryStatus = StringLiteralValue.Create;
                _businessOfficeRBIRegistrationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _businessOfficeRBIRegistrationViewModel.ReasonForModification = "None";
                _businessOfficeRBIRegistrationViewModel.Remark = "None";
                _businessOfficeRBIRegistrationViewModel.UserAction = StringLiteralValue.Create;
                _businessOfficeRBIRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeRBIRegistration businessOfficeRBIRegistration = Mapper.Map<BusinessOfficeRBIRegistration>(_businessOfficeRBIRegistrationViewModel);

                BusinessOfficeRBIRegistrationMakerChecker businessOfficeRBIRegistrationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                BusinessOfficeRBIRegistrationTranslation businessOfficeRBIRegistrationTranslation = Mapper.Map<BusinessOfficeRBIRegistrationTranslation>(_businessOfficeRBIRegistrationViewModel);

                BusinessOfficeRBIRegistrationTranslationMakerChecker businessOfficeRBIRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationTranslationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                context.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Attach(businessOfficeRBIRegistrationTranslationMakerChecker);
                context.Entry(businessOfficeRBIRegistrationTranslationMakerChecker).State = EntityState.Added;
                businessOfficeRBIRegistrationTranslation.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Add(businessOfficeRBIRegistrationTranslationMakerChecker);

                context.BusinessOfficeRBIRegistrationTranslations.Attach(businessOfficeRBIRegistrationTranslation);
                context.Entry(businessOfficeRBIRegistrationTranslation).State = EntityState.Added;
                businessOfficeRBIRegistration.BusinessOfficeRBIRegistrationTranslations.Add(businessOfficeRBIRegistrationTranslation);

                context.BusinessOfficeRBIRegistrationMakerCheckers.Attach(businessOfficeRBIRegistrationMakerChecker);
                context.Entry(businessOfficeRBIRegistrationMakerChecker).State = EntityState.Added;
                businessOfficeRBIRegistration.BusinessOfficeRBIRegistrationMakerCheckers.Add(businessOfficeRBIRegistrationMakerChecker);

                context.BusinessOfficeRBIRegistrations.Attach(businessOfficeRBIRegistration);
                context.Entry(businessOfficeRBIRegistration).State = EntityState.Added;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel)
        {
            try
            {
                // First Modify Record - Get Active Record (i.e. Whose Entry Status is Verified)                 
                BusinessOfficeRBIRegistrationViewModel businessOfficeRBIRegistrationViewModelOldEntry = await officeDetailRepository.GetRBIRegistrationEntry(_businessOfficeRBIRegistrationViewModel.BusinessOfficePrmKey, StringLiteralValue.Verify);

                if (businessOfficeRBIRegistrationViewModelOldEntry != null)
                {
                    // Set Default Value
                    businessOfficeRBIRegistrationViewModelOldEntry.EntryDateTime = DateTime.Now;
                    businessOfficeRBIRegistrationViewModelOldEntry.UserAction = StringLiteralValue.Modify;
                    businessOfficeRBIRegistrationViewModelOldEntry.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    BusinessOfficeRBIRegistrationMakerChecker businessOfficeRBIRegistrationMakerCheckerForModify = Mapper.Map<BusinessOfficeRBIRegistrationMakerChecker>(businessOfficeRBIRegistrationViewModelOldEntry);

                    context.BusinessOfficeRBIRegistrationMakerCheckers.Attach(businessOfficeRBIRegistrationMakerCheckerForModify);
                    context.Entry(businessOfficeRBIRegistrationMakerCheckerForModify).State = EntityState.Added;

                    BusinessOfficeRBIRegistrationTranslationMakerChecker businessOfficeRBIRegistrationTranslationMakerCheckerForModify = Mapper.Map<BusinessOfficeRBIRegistrationTranslationMakerChecker>(businessOfficeRBIRegistrationViewModelOldEntry);

                    context.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Attach(businessOfficeRBIRegistrationTranslationMakerCheckerForModify);
                    context.Entry(businessOfficeRBIRegistrationTranslationMakerCheckerForModify).State = EntityState.Added;
                }

                //Verify New Record
                // Set Default Value
                _businessOfficeRBIRegistrationViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeRBIRegistrationViewModel.UserAction = StringLiteralValue.Verify;
                _businessOfficeRBIRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeRBIRegistrationMakerChecker businessOfficeRBIRegistrationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                BusinessOfficeRBIRegistrationTranslationMakerChecker businessOfficeRBIRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationTranslationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                context.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Attach(businessOfficeRBIRegistrationTranslationMakerChecker);
                context.Entry(businessOfficeRBIRegistrationTranslationMakerChecker).State = EntityState.Added;

                context.BusinessOfficeRBIRegistrationMakerCheckers.Attach(businessOfficeRBIRegistrationMakerChecker);
                context.Entry(businessOfficeRBIRegistrationMakerChecker).State = EntityState.Added;

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
