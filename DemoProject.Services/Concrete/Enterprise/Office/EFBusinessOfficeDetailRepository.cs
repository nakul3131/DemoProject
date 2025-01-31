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
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFBusinessOfficeDetailRepository : IBusinessOfficeDetailRepository
    {
        private readonly EFDbContext context;

        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IOfficeDetailRepository officeDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public EFBusinessOfficeDetailRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IConfigurationDetailRepository _configurationDetailRepository,
                                                IEnterpriseDetailRepository _enterpriseDetailRepository, IPersonDetailRepository _personDetailRepository, IOfficeDetailRepository _officeDetailRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            personDetailRepository = _personDetailRepository;
            officeDetailRepository = _officeDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
        }


        public async Task<bool> Amend(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeDetailViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeDetailViewModel.EntryStatus = StringLiteralValue.Amend;
                _businessOfficeDetailViewModel.ReasonForModification = "None";
                _businessOfficeDetailViewModel.Remark = "None";
                _businessOfficeDetailViewModel.UserAction = StringLiteralValue.Amend;
                _businessOfficeDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //_businessOfficeDetailViewModel.BusinessOfficeTypePrmKey = enterpriseDetailRepository.GetPrmKeyById(_businessOfficeDetailViewModel.BusinessOfficeTypeId);
                //_businessOfficeDetailViewModel.BusinessNaturePrmKey = enterpriseDetailRepository.GetPrmKeyById(_businessOfficeDetailViewModel.BusinessNatureId);
                _businessOfficeDetailViewModel.CenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_businessOfficeDetailViewModel.CenterId);
                //_businessOfficeDetailViewModel.LocalCurrencyPrmKey = accountDetailRepository.GetPrmKeyById(_businessOfficeDetailViewModel.LocalCurrencyId);
                _businessOfficeDetailViewModel.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_businessOfficeDetailViewModel.GeneralLedgerId);
                _businessOfficeDetailViewModel.OfficeSchedulePrmKey = enterpriseDetailRepository.GetOfficeSchedulePrmKeyById(_businessOfficeDetailViewModel.OfficeScheduleId);
                //_businessOfficeDetailViewModel.RegionalLanguagePrmKey = appSupportedLanguageRepository.GetPrmKeyById(_businessOfficeDetailViewModel.RegionalLanguageId);

                BusinessOfficeDetail businessOfficeDetail = Mapper.Map<BusinessOfficeDetail>(_businessOfficeDetailViewModel);

                _businessOfficeDetailViewModel.PrmKey = 0;
                BusinessOfficeDetailMakerChecker businessOfficeDetailMakerChecker = Mapper.Map<BusinessOfficeDetailMakerChecker>(_businessOfficeDetailViewModel);

                context.BusinessOfficeDetailMakerCheckers.Attach(businessOfficeDetailMakerChecker);
                context.Entry(businessOfficeDetailMakerChecker).State = EntityState.Added;
                businessOfficeDetail.BusinessOfficeDetailMakerCheckers.Add(businessOfficeDetailMakerChecker);

                context.BusinessOfficeDetails.Attach(businessOfficeDetail);
                context.Entry(businessOfficeDetail).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeDetailViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeDetailViewModel.Remark = "None";
                _businessOfficeDetailViewModel.UserAction = StringLiteralValue.Delete;
                _businessOfficeDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeDetailMakerChecker businessOfficeDetailMakerChecker = Mapper.Map<BusinessOfficeDetailMakerChecker>(_businessOfficeDetailViewModel);

                context.BusinessOfficeDetailMakerCheckers.Attach(businessOfficeDetailMakerChecker);
                context.Entry(businessOfficeDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Modify(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeDetailViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _businessOfficeDetailViewModel.ReasonForModification = "None";
                _businessOfficeDetailViewModel.Remark = "None";
                _businessOfficeDetailViewModel.UserAction = StringLiteralValue.Create;
                _businessOfficeDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                _businessOfficeDetailViewModel.BusinessOfficeTypePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeDetailViewModel.BusinessOfficeTypeId);
                _businessOfficeDetailViewModel.BusinessNaturePrmKey = enterpriseDetailRepository.GetBusinessNaturePrmKeyById(_businessOfficeDetailViewModel.BusinessNatureId);
                _businessOfficeDetailViewModel.CenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_businessOfficeDetailViewModel.CenterId);
                _businessOfficeDetailViewModel.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_businessOfficeDetailViewModel.LocalCurrencyId);
                _businessOfficeDetailViewModel.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_businessOfficeDetailViewModel.GeneralLedgerId);
                _businessOfficeDetailViewModel.OfficeSchedulePrmKey = enterpriseDetailRepository.GetOfficeSchedulePrmKeyById(_businessOfficeDetailViewModel.OfficeScheduleId);
                _businessOfficeDetailViewModel.LanguagePrmKey = configurationDetailRepository.GetLanguagePrmKeyById(_businessOfficeDetailViewModel.RegionalLanguageId);

                BusinessOfficeDetail businessOfficeDetail = Mapper.Map<BusinessOfficeDetail>(_businessOfficeDetailViewModel);

                BusinessOfficeDetailMakerChecker businessOfficeDetailMakerChecker = Mapper.Map<BusinessOfficeDetailMakerChecker>(_businessOfficeDetailViewModel);

                context.BusinessOfficeDetailMakerCheckers.Attach(businessOfficeDetailMakerChecker);
                context.Entry(businessOfficeDetailMakerChecker).State = EntityState.Added;
                businessOfficeDetail.BusinessOfficeDetailMakerCheckers.Add(businessOfficeDetailMakerChecker);

                context.BusinessOfficeDetails.Attach(businessOfficeDetail);
                context.Entry(businessOfficeDetail).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeDetailViewModel.PrmKey = 0;
                _businessOfficeDetailViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeDetailViewModel.UserAction = StringLiteralValue.Reject;
                _businessOfficeDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeDetailMakerChecker businessOfficeDetailMakerChecker = Mapper.Map<BusinessOfficeDetailMakerChecker>(_businessOfficeDetailViewModel);

                context.BusinessOfficeDetailMakerCheckers.Attach(businessOfficeDetailMakerChecker);
                context.Entry(businessOfficeDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel)
        {
            try
            {
                // Set Default Value
                _businessOfficeDetailViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _businessOfficeDetailViewModel.ReasonForModification = "None";
                _businessOfficeDetailViewModel.Remark = "None";
                _businessOfficeDetailViewModel.UserAction = StringLiteralValue.Create;
                _businessOfficeDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                _businessOfficeDetailViewModel.BusinessOfficeTypePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeDetailViewModel.BusinessOfficeTypeId);
                _businessOfficeDetailViewModel.BusinessNaturePrmKey = enterpriseDetailRepository.GetBusinessNaturePrmKeyById(_businessOfficeDetailViewModel.BusinessNatureId);
                _businessOfficeDetailViewModel.CenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_businessOfficeDetailViewModel.CenterId);
                _businessOfficeDetailViewModel.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_businessOfficeDetailViewModel.LocalCurrencyId);
                _businessOfficeDetailViewModel.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_businessOfficeDetailViewModel.GeneralLedgerId);
                _businessOfficeDetailViewModel.OfficeSchedulePrmKey = enterpriseDetailRepository.GetOfficeSchedulePrmKeyById(_businessOfficeDetailViewModel.OfficeScheduleId);
                _businessOfficeDetailViewModel.LanguagePrmKey = configurationDetailRepository.GetLanguagePrmKeyById(_businessOfficeDetailViewModel.RegionalLanguageId);

                BusinessOfficeDetail businessOfficeDetail = Mapper.Map<BusinessOfficeDetail>(_businessOfficeDetailViewModel);

                BusinessOfficeDetailMakerChecker businessOfficeDetailMakerChecker = Mapper.Map<BusinessOfficeDetailMakerChecker>(_businessOfficeDetailViewModel);

                context.BusinessOfficeDetailMakerCheckers.Attach(businessOfficeDetailMakerChecker);
                context.Entry(businessOfficeDetailMakerChecker).State = EntityState.Added;
                businessOfficeDetail.BusinessOfficeDetailMakerCheckers.Add(businessOfficeDetailMakerChecker);

                context.BusinessOfficeDetails.Attach(businessOfficeDetail);
                context.Entry(businessOfficeDetail).State = EntityState.Added;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel)
        {
            try
            {
                // First Modify Record - Get Active Record (i.e. Whose Entry Status is Verified)                 
                BusinessOfficeDetailViewModel businessOfficeDetailViewModelOldEntry = await officeDetailRepository.GetBusinessOfficeDetailEntry(_businessOfficeDetailViewModel.BusinessOfficePrmKey, StringLiteralValue.Verify);

                if (businessOfficeDetailViewModelOldEntry != null)
                {
                    // Set Default Value
                    businessOfficeDetailViewModelOldEntry.EntryDateTime = DateTime.Now;
                    businessOfficeDetailViewModelOldEntry.UserAction = StringLiteralValue.Modify;
                    businessOfficeDetailViewModelOldEntry.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    BusinessOfficeDetailMakerChecker businessOfficeDetailMakerCheckerForModify = Mapper.Map<BusinessOfficeDetailMakerChecker>(businessOfficeDetailViewModelOldEntry);

                    context.BusinessOfficeDetailMakerCheckers.Attach(businessOfficeDetailMakerCheckerForModify);
                    context.Entry(businessOfficeDetailMakerCheckerForModify).State = EntityState.Added;
                }

                //Verify New Record
                // Set Default Value
                _businessOfficeDetailViewModel.EntryDateTime = DateTime.Now;
                _businessOfficeDetailViewModel.UserAction = StringLiteralValue.Verify;
                _businessOfficeDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                BusinessOfficeDetailMakerChecker businessOfficeDetailMakerChecker = Mapper.Map<BusinessOfficeDetailMakerChecker>(_businessOfficeDetailViewModel);

                context.BusinessOfficeDetailMakerCheckers.Attach(businessOfficeDetailMakerChecker);
                context.Entry(businessOfficeDetailMakerChecker).State = EntityState.Added;

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
