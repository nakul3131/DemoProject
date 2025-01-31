using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Domain.Entities.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonEmploymentDetailRepository : IPersonEmploymentDetailRepository
    {
        private readonly EFDbContext context;

        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public EFPersonEmploymentDetailRepository(RepositoryConnection _connection, IManagementDetailRepository _managementDetailRepository, IPersonDetailRepository _personDetailRepository)
        {
            context                 = _connection.EFDbContext;
            managementDetailRepository   = _managementDetailRepository;
            personDetailRepository       = _personDetailRepository;
        }

        public async Task<bool> Amend(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel)
        {
            try
            {
                // Set Default Value
                _personEmploymentDetailViewModel.EntryDateTime = DateTime.Now;
                _personEmploymentDetailViewModel.EntryStatus = StringLiteralValue.Amend;
                _personEmploymentDetailViewModel.ReasonForModification = "None";
                _personEmploymentDetailViewModel.Remark = "None";
                _personEmploymentDetailViewModel.UserAction = StringLiteralValue.Amend;
                _personEmploymentDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Get PrmKey By Id
                _personEmploymentDetailViewModel.EmploymentTypePrmKey = managementDetailRepository.GetEmploymentTypePrmKeyById(_personEmploymentDetailViewModel.EmploymentTypeId);
                _personEmploymentDetailViewModel.EmployerNaturePrmKey = managementDetailRepository.GetEmployerNaturePrmKeyById(_personEmploymentDetailViewModel.NatureOfEmployerId);
                _personEmploymentDetailViewModel.DesignationPrmKey = managementDetailRepository.GetDesignationPrmKeyById(_personEmploymentDetailViewModel.DesignationId);
                _personEmploymentDetailViewModel.EmployerCityPrmKey = personDetailRepository.GetCenterPrmKeyById(_personEmploymentDetailViewModel.CityId);

                PersonEmploymentDetail personEmploymentDetail = Mapper.Map<PersonEmploymentDetail>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailMakerChecker personEmploymentDetailMakerChecker = Mapper.Map<PersonEmploymentDetailMakerChecker>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailTranslation personEmploymentDetailTranslation = Mapper.Map<PersonEmploymentDetailTranslation>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailTranslationMakerChecker EmploymentDetailTranslationMakerChecker = Mapper.Map<PersonEmploymentDetailTranslationMakerChecker>(_personEmploymentDetailViewModel);

                context.PersonEmploymentDetailTranslationMakerCheckers.Attach(EmploymentDetailTranslationMakerChecker);
                context.Entry(EmploymentDetailTranslationMakerChecker).State = EntityState.Added;
                personEmploymentDetailTranslation.PersonEmploymentDetailTranslationMakerCheckers.Add(EmploymentDetailTranslationMakerChecker);

                context.PersonEmploymentDetailTranslations.Attach(personEmploymentDetailTranslation);
                context.Entry(personEmploymentDetailTranslation).State = EntityState.Modified;

                context.PersonEmploymentDetailMakerCheckers.Attach(personEmploymentDetailMakerChecker);
                context.Entry(personEmploymentDetailMakerChecker).State = EntityState.Added;
                personEmploymentDetail.PersonEmploymentDetailMakerCheckers.Add(personEmploymentDetailMakerChecker);

                context.PersonEmploymentDetails.Attach(personEmploymentDetail);
                context.Entry(personEmploymentDetail).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel)
        {
            try
            {
                // Set Default Value
                _personEmploymentDetailViewModel.EntryDateTime = DateTime.Now;
                _personEmploymentDetailViewModel.Remark = "None";
                _personEmploymentDetailViewModel.UserAction = StringLiteralValue.Delete;
                _personEmploymentDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                PersonEmploymentDetailMakerChecker personEmploymentDetailMakerChecker = Mapper.Map<PersonEmploymentDetailMakerChecker>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailTranslationMakerChecker personEmploymentDetailTranslationMakerChecker = Mapper.Map<PersonEmploymentDetailTranslationMakerChecker>(_personEmploymentDetailViewModel);

                context.PersonEmploymentDetailTranslationMakerCheckers.Attach(personEmploymentDetailTranslationMakerChecker);
                context.Entry(personEmploymentDetailTranslationMakerChecker).State = EntityState.Added;

                context.PersonEmploymentDetailMakerCheckers.Attach(personEmploymentDetailMakerChecker);
                context.Entry(personEmploymentDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<PersonViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<PersonViewModel>("SELECT * FROM dbo.GetPersonalAdditionalDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PersonViewModel>("SELECT * FROM dbo.GetPersonalAdditionalDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PersonViewModel>("SELECT * FROM dbo.GetPersonalAdditionalDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PersonViewModel>("SELECT * FROM dbo.GetPersonalAdditionalDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonEmploymentDetailViewModel>> GetRejectedEntries(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonEmploymentDetailViewModel>("SELECT * FROM dbo.GetPersonEmploymentDetailEntryByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonEmploymentDetailViewModel>> GetUnverifiedEntries(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonEmploymentDetailViewModel>("SELECT * FROM dbo.GetPersonEmploymentDetailEntryByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonEmploymentDetailViewModel>> GetVerifiedEntries(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonEmploymentDetailViewModel>("SELECT * FROM dbo.GetPersonEmploymentDetailEntryByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonEmploymentDetailViewModel> GetViewModelForCreate(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonEmploymentDetailViewModel>("SELECT * FROM dbo.GetPersonEmploymentDetailEntryByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonEmploymentDetailViewModel> GetViewModelForReject(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonEmploymentDetailViewModel>("SELECT * FROM dbo.GetPersonEmploymentDetailEntryByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonEmploymentDetailViewModel> GetViewModelForUnverified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonEmploymentDetailViewModel>("SELECT * FROM dbo.GetPersonEmploymentDetailEntryByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonEmploymentDetailViewModel> GetViewModelForVerified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonEmploymentDetailViewModel>("SELECT * FROM dbo.GetPersonEmploymentDetailEntryByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel)
        {
            try
            {
                // Set Default Value
                _personEmploymentDetailViewModel.EntryDateTime = DateTime.Now;
                _personEmploymentDetailViewModel.UserAction = StringLiteralValue.Reject;
                _personEmploymentDetailViewModel.Remark = "None";
                _personEmploymentDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                PersonEmploymentDetailMakerChecker personEmploymentDetailMakerChecker = Mapper.Map<PersonEmploymentDetailMakerChecker>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailTranslationMakerChecker personEmploymentDetailTranslationMakerChecker = Mapper.Map<PersonEmploymentDetailTranslationMakerChecker>(_personEmploymentDetailViewModel);

                context.PersonEmploymentDetailTranslationMakerCheckers.Attach(personEmploymentDetailTranslationMakerChecker);
                context.Entry(personEmploymentDetailTranslationMakerChecker).State = EntityState.Added;

                context.PersonEmploymentDetailMakerCheckers.Attach(personEmploymentDetailMakerChecker);
                context.Entry(personEmploymentDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel)
        {
            try
            { 
                // Set Default Value
                _personEmploymentDetailViewModel.EntryDateTime = DateTime.Now;
                _personEmploymentDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _personEmploymentDetailViewModel.ReasonForModification = "None";
                _personEmploymentDetailViewModel.Remark = "None";
                _personEmploymentDetailViewModel.UserAction = StringLiteralValue.Create;
                _personEmploymentDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Get PrmKey By Id
                _personEmploymentDetailViewModel.EmploymentTypePrmKey = managementDetailRepository.GetEmploymentTypePrmKeyById(_personEmploymentDetailViewModel.EmploymentTypeId);
                _personEmploymentDetailViewModel.EmployerNaturePrmKey = managementDetailRepository.GetEmployerNaturePrmKeyById(_personEmploymentDetailViewModel.NatureOfEmployerId);
                _personEmploymentDetailViewModel.DesignationPrmKey = managementDetailRepository.GetDesignationPrmKeyById(_personEmploymentDetailViewModel.DesignationId);
                _personEmploymentDetailViewModel.EmployerCityPrmKey = personDetailRepository.GetCenterPrmKeyById(_personEmploymentDetailViewModel.CityId);

                PersonEmploymentDetail personEmploymentDetail = Mapper.Map<PersonEmploymentDetail>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailMakerChecker personEmploymentDetailMakerChecker = Mapper.Map<PersonEmploymentDetailMakerChecker>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailTranslation personEmploymentDetailTranslation = Mapper.Map<PersonEmploymentDetailTranslation>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailTranslationMakerChecker EmploymentDetailTranslationMakerChecker = Mapper.Map<PersonEmploymentDetailTranslationMakerChecker>(_personEmploymentDetailViewModel);

                context.PersonEmploymentDetailTranslationMakerCheckers.Attach(EmploymentDetailTranslationMakerChecker);
                context.Entry(EmploymentDetailTranslationMakerChecker).State = EntityState.Added;
                personEmploymentDetailTranslation.PersonEmploymentDetailTranslationMakerCheckers.Add(EmploymentDetailTranslationMakerChecker);

                context.PersonEmploymentDetailTranslations.Attach(personEmploymentDetailTranslation);
                context.Entry(personEmploymentDetailTranslation).State = EntityState.Added;
                personEmploymentDetail.PersonEmploymentDetailTranslations.Add(personEmploymentDetailTranslation);

                context.PersonEmploymentDetailMakerCheckers.Attach(personEmploymentDetailMakerChecker);
                context.Entry(personEmploymentDetailMakerChecker).State = EntityState.Added;
                personEmploymentDetail.PersonEmploymentDetailMakerCheckers.Add(personEmploymentDetailMakerChecker);

                context.PersonEmploymentDetails.Attach(personEmploymentDetail);
                context.Entry(personEmploymentDetail).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> SaveModification(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel)
        {
            try
            {
                // Set Default Value
                _personEmploymentDetailViewModel.EntryDateTime = DateTime.Now;
                _personEmploymentDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _personEmploymentDetailViewModel.ReasonForModification = "None";
                _personEmploymentDetailViewModel.Remark = "None";
                _personEmploymentDetailViewModel.UserAction = StringLiteralValue.Create;
                _personEmploymentDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Get PrmKey By Id
                _personEmploymentDetailViewModel.EmploymentTypePrmKey = managementDetailRepository.GetEmploymentTypePrmKeyById(_personEmploymentDetailViewModel.EmploymentTypeId);
                _personEmploymentDetailViewModel.EmployerNaturePrmKey = managementDetailRepository.GetEmployerNaturePrmKeyById(_personEmploymentDetailViewModel.NatureOfEmployerId);
                _personEmploymentDetailViewModel.DesignationPrmKey = managementDetailRepository.GetDesignationPrmKeyById(_personEmploymentDetailViewModel.DesignationId);
                _personEmploymentDetailViewModel.EmployerCityPrmKey = personDetailRepository.GetCenterPrmKeyById(_personEmploymentDetailViewModel.CityId);

                PersonEmploymentDetail personEmploymentDetail = Mapper.Map<PersonEmploymentDetail>(_personEmploymentDetailViewModel);
               
                PersonEmploymentDetailMakerChecker personEmploymentDetailMakerChecker = Mapper.Map<PersonEmploymentDetailMakerChecker>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailTranslation personEmploymentDetailTranslation = Mapper.Map<PersonEmploymentDetailTranslation>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailTranslationMakerChecker EmploymentDetailTranslationMakerChecker = Mapper.Map<PersonEmploymentDetailTranslationMakerChecker>(_personEmploymentDetailViewModel);

                context.PersonEmploymentDetailTranslationMakerCheckers.Attach(EmploymentDetailTranslationMakerChecker);
                context.Entry(EmploymentDetailTranslationMakerChecker).State = EntityState.Added;
                personEmploymentDetailTranslation.PersonEmploymentDetailTranslationMakerCheckers.Add(EmploymentDetailTranslationMakerChecker);

                context.PersonEmploymentDetailTranslations.Attach(personEmploymentDetailTranslation);
                context.Entry(personEmploymentDetailTranslation).State = EntityState.Added;
                personEmploymentDetail.PersonEmploymentDetailTranslations.Add(personEmploymentDetailTranslation);

                context.PersonEmploymentDetailMakerCheckers.Attach(personEmploymentDetailMakerChecker);
                context.Entry(personEmploymentDetailMakerChecker).State = EntityState.Added;
                personEmploymentDetail.PersonEmploymentDetailMakerCheckers.Add(personEmploymentDetailMakerChecker);

                context.PersonEmploymentDetails.Attach(personEmploymentDetail);
                context.Entry(personEmploymentDetail).State = EntityState.Added;
                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel)
        {
            try
            {
                // Assign MDF Status To EntryStatus For Performing Modify Operation
                PersonEmploymentDetailViewModel personEmploymentDetailViewModelForModify = await GetViewModelForVerified(_personEmploymentDetailViewModel.PersonPrmKey);

                // Set Default Value
                personEmploymentDetailViewModelForModify.EntryDateTime = DateTime.Now;
                personEmploymentDetailViewModelForModify.UserAction = StringLiteralValue.Modify;
                personEmploymentDetailViewModelForModify.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                PersonEmploymentDetailMakerChecker personEmploymentDetailMakerCheckerForModify = Mapper.Map<PersonEmploymentDetailMakerChecker>(personEmploymentDetailViewModelForModify);

                PersonEmploymentDetailTranslationMakerChecker personEmploymentDetailTranslationMakerCheckerForModify = Mapper.Map<PersonEmploymentDetailTranslationMakerChecker>(_personEmploymentDetailViewModel);

                context.PersonEmploymentDetailTranslationMakerCheckers.Attach(personEmploymentDetailTranslationMakerCheckerForModify);
                context.Entry(personEmploymentDetailTranslationMakerCheckerForModify).State = EntityState.Added;

                context.PersonEmploymentDetailMakerCheckers.Attach(personEmploymentDetailMakerCheckerForModify);
                context.Entry(personEmploymentDetailMakerCheckerForModify).State = EntityState.Added;

                // Verify New Record
                // Set Default Value
                _personEmploymentDetailViewModel.EntryDateTime = DateTime.Now;
                _personEmploymentDetailViewModel.Remark = "None";
                _personEmploymentDetailViewModel.UserAction = StringLiteralValue.Verify;
                _personEmploymentDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                PersonEmploymentDetailMakerChecker personEmploymentDetailMakerChecker = Mapper.Map<PersonEmploymentDetailMakerChecker>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailTranslationMakerChecker personEmploymentDetailTranslationMakerChecker = Mapper.Map<PersonEmploymentDetailTranslationMakerChecker>(_personEmploymentDetailViewModel);

                context.PersonEmploymentDetailTranslationMakerCheckers.Attach(personEmploymentDetailTranslationMakerChecker);
                context.Entry(personEmploymentDetailTranslationMakerChecker).State = EntityState.Added;

                context.PersonEmploymentDetailMakerCheckers.Attach(personEmploymentDetailMakerChecker);
                context.Entry(personEmploymentDetailMakerChecker).State = EntityState.Added;

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
