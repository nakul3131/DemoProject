using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DemoProject.Domain.Entities.Account.Customer;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerAccountNomineeRepository : ICustomerAccountNomineeRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonRepository personRepository;
        private List<SelectListItem> listItems = new List<SelectListItem>();

        public EFCustomerAccountNomineeRepository(RepositoryConnection _connection, IPersonRepository _personRepository)
        {
            context = _connection.EFDbContext;
            personRepository = _personRepository;
        }

        public async Task<bool> Amend(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountNomineeViewModel.ActivationStatus = StringLiteralValue.Active;
                _customerAccountNomineeViewModel.EntryDateTime = DateTime.Now;
                _customerAccountNomineeViewModel.EntryStatus = StringLiteralValue.Amend;
                _customerAccountNomineeViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _customerAccountNomineeViewModel.ReasonForModification = "None";
                _customerAccountNomineeViewModel.Remark = "None";
                _customerAccountNomineeViewModel.UserAction = StringLiteralValue.Amend;
                _customerAccountNomineeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Amend First Old Record
                // CustomerAccountNominee
                IEnumerable<CustomerAccountNomineeViewModel> customerAccountNomineeViewModels = await GetRejectedEntries(_customerAccountNomineeViewModel.CustomerAccountPrmKey);

                foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModels)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    CustomerAccountNomineeMakerChecker customerAccountNomineeMakerChecker = Mapper.Map<CustomerAccountNomineeMakerChecker>(viewModel);

                    // CustomerAccountJointAccountHolderMakerChecker
                    context.CustomerAccountNomineeMakerCheckers.Attach(customerAccountNomineeMakerChecker);
                    context.Entry(customerAccountNomineeMakerChecker).State = EntityState.Added;
                }

                // Insert New Updated Record - Get Record From Session Object
                List<CustomerAccountNomineeViewModel> customerAccountNomineeList = new List<CustomerAccountNomineeViewModel>();
                customerAccountNomineeList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["CustomerAccountNominee"];

                foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeList)
                {
                    // Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Active;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    //viewModel.PersonPrmKey = personRepository.GetPrmKeyById(viewModel.PersonId);


                    // Mapping CustomerAccountNominee
                    CustomerAccountNominee customerAccountNominee = Mapper.Map<CustomerAccountNominee>(_customerAccountNomineeViewModel);
                    CustomerAccountNomineeMakerChecker customerAccountNomineeMakerChecker = Mapper.Map<CustomerAccountNomineeMakerChecker>(_customerAccountNomineeViewModel);

                    // CustomerAccountNomineeGuardian
                    CustomerAccountNomineeGuardian customerAccountNomineeGuardian = Mapper.Map<CustomerAccountNomineeGuardian>(_customerAccountNomineeViewModel);
                    CustomerAccountNomineeGuardianMakerChecker customerAccountNomineeGuardianMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianMakerChecker>(_customerAccountNomineeViewModel);

                    // CustomerAccountNomineeGuardianTranslation
                    CustomerAccountNomineeGuardianTranslation customerAccountNomineeGuardianTranslation = Mapper.Map<CustomerAccountNomineeGuardianTranslation>(_customerAccountNomineeViewModel);
                    CustomerAccountNomineeGuardianTranslationMakerChecker customerAccountNomineeGuardianTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianTranslationMakerChecker>(_customerAccountNomineeViewModel);

                    // Set Default Values
                    customerAccountNominee.CustomerAccountPrmKey = _customerAccountNomineeViewModel.CustomerAccountPrmKey;

                    // CustomerAccountNominee
                    context.CustomerAccountNominees.Attach(customerAccountNominee);
                    context.Entry(customerAccountNominee).State = EntityState.Added;

                    // CustomerAccountNomineeMakerChecker
                    context.CustomerAccountNomineeMakerCheckers.Attach(customerAccountNomineeMakerChecker);
                    context.Entry(customerAccountNomineeMakerChecker).State = EntityState.Added;
                    customerAccountNominee.CustomerAccountNomineeMakerCheckers.Add(customerAccountNomineeMakerChecker);

                    // CustomerAccountNomineeGuardian
                    context.CustomerAccountNomineeGuardians.Attach(customerAccountNomineeGuardian);
                    context.Entry(customerAccountNomineeGuardian).State = EntityState.Added;

                    // CustomerAccountNomineeGuardianMakerChecker
                    context.CustomerAccountNomineeGuardianMakerCheckers.Attach(customerAccountNomineeGuardianMakerChecker);
                    context.Entry(customerAccountNomineeGuardianMakerChecker).State = EntityState.Added;
                    customerAccountNomineeGuardian.CustomerAccountNomineeGuardianMakerCheckers.Add(customerAccountNomineeGuardianMakerChecker);

                    // CustomerAccountNomineeGuardianTranslation
                    context.CustomerAccountNomineeGuardianTranslations.Attach(customerAccountNomineeGuardianTranslation);
                    context.Entry(customerAccountNomineeGuardianTranslation).State = EntityState.Added;

                    // CustomerAccountNomineeGuardianTranslationMakerChecker
                    context.CustomerAccountNomineeGuardianTranslationMakerCheckers.Attach(customerAccountNomineeGuardianTranslationMakerChecker);
                    context.Entry(customerAccountNomineeGuardianTranslationMakerChecker).State = EntityState.Added;
                    customerAccountNomineeGuardianTranslation.CustomerAccountNomineeGuardianTranslationMakerCheckers.Add(customerAccountNomineeGuardianTranslationMakerChecker);
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

        public async Task<bool> Delete(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountNomineeViewModel.EntryDateTime = DateTime.Now;
                _customerAccountNomineeViewModel.ReasonForModification = "None";
                _customerAccountNomineeViewModel.Remark = "None";
                _customerAccountNomineeViewModel.UserAction = StringLiteralValue.Delete;
                _customerAccountNomineeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerNominee
                IEnumerable<CustomerAccountNomineeViewModel> customerAccountNomineeViewModels = await GetVerifiedEntries(_customerAccountNomineeViewModel.CustomerAccountPrmKey);

                foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModels)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerAccountNomineeViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    CustomerAccountNomineeMakerChecker customerAccountNomineeMakerChecker = Mapper.Map<CustomerAccountNomineeMakerChecker>(viewModel);
                    CustomerAccountNomineeTranslationMakerChecker customerAccountNomineeTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeTranslationMakerChecker>(viewModel);
                    CustomerAccountNomineeGuardianMakerChecker customerAccountNomineeGuardianMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianMakerChecker>(viewModel);
                    CustomerAccountNomineeGuardianTranslationMakerChecker customerAccountNomineeGuardianTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianTranslationMakerChecker>(viewModel);

                    // CustomerAccountNomineeMakerChecker
                    context.CustomerAccountNomineeMakerCheckers.Attach(customerAccountNomineeMakerChecker);
                    context.Entry(customerAccountNomineeMakerChecker).State = EntityState.Added;

                    // CustomerAccountNomineeTranslationMakerChecker
                    context.CustomerAccountNomineeTranslationMakerCheckers.Attach(customerAccountNomineeTranslationMakerChecker);
                    context.Entry(customerAccountNomineeTranslationMakerChecker).State = EntityState.Added;

                    // CustomerAccountNomineeGuardianMakerChecker
                    context.CustomerAccountNomineeGuardianMakerCheckers.Attach(customerAccountNomineeGuardianMakerChecker);
                    context.Entry(customerAccountNomineeGuardianMakerChecker).State = EntityState.Added;

                    // CustomerAccountNomineeGuardianTranslationMakerChecker
                    context.CustomerAccountNomineeGuardianTranslationMakerCheckers.Attach(customerAccountNomineeGuardianTranslationMakerChecker);
                    context.Entry(customerAccountNomineeGuardianTranslationMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Modify(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountNomineeViewModel.EntryDateTime = DateTime.Now;
                _customerAccountNomineeViewModel.EntryStatus = StringLiteralValue.Create;
                _customerAccountNomineeViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _customerAccountNomineeViewModel.Remark = "None";
                _customerAccountNomineeViewModel.UserAction = StringLiteralValue.Create;
                _customerAccountNomineeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];


                // CustomerAccountNominee
                List<CustomerAccountNomineeViewModel> customerAccountNomineeList = new List<CustomerAccountNomineeViewModel>();
                customerAccountNomineeList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["CustomerAccountNominee"];

                foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeList)
                {
                    // Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Active;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.CustomerAccountPrmKey = _customerAccountNomineeViewModel.CustomerAccountPrmKey;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    //viewModel.PersonPrmKey = personRepository.GetPrmKeyById(viewModel.PersonId);

                    // Mapping CustomerAccountNominee
                    CustomerAccountNominee customerAccountNominee = Mapper.Map<CustomerAccountNominee>(_customerAccountNomineeViewModel);
                    CustomerAccountNomineeMakerChecker customerAccountNomineeMakerChecker = Mapper.Map<CustomerAccountNomineeMakerChecker>(_customerAccountNomineeViewModel);

                    // CustomerAccountNomineeGuardian
                    CustomerAccountNomineeGuardian customerAccountNomineeGuardian = Mapper.Map<CustomerAccountNomineeGuardian>(_customerAccountNomineeViewModel);
                    CustomerAccountNomineeGuardianMakerChecker customerAccountNomineeGuardianMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianMakerChecker>(_customerAccountNomineeViewModel);

                    // CustomerAccountNomineeGuardianTranslation
                    CustomerAccountNomineeGuardianTranslation customerAccountNomineeGuardianTranslation = Mapper.Map<CustomerAccountNomineeGuardianTranslation>(_customerAccountNomineeViewModel);
                    CustomerAccountNomineeGuardianTranslationMakerChecker customerAccountNomineeGuardianTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianTranslationMakerChecker>(_customerAccountNomineeViewModel);

                    // CustomerAccountNominee
                    context.CustomerAccountNominees.Attach(customerAccountNominee);
                    context.Entry(customerAccountNominee).State = EntityState.Added;

                    // CustomerAccountNomineeMakerChecker
                    context.CustomerAccountNomineeMakerCheckers.Attach(customerAccountNomineeMakerChecker);
                    context.Entry(customerAccountNomineeMakerChecker).State = EntityState.Added;
                    customerAccountNominee.CustomerAccountNomineeMakerCheckers.Add(customerAccountNomineeMakerChecker);

                    // CustomerAccountNomineeGuardian
                    context.CustomerAccountNomineeGuardians.Attach(customerAccountNomineeGuardian);
                    context.Entry(customerAccountNomineeGuardian).State = EntityState.Added;

                    // CustomerAccountNomineeGuardianMakerChecker
                    context.CustomerAccountNomineeGuardianMakerCheckers.Attach(customerAccountNomineeGuardianMakerChecker);
                    context.Entry(customerAccountNomineeGuardianMakerChecker).State = EntityState.Added;
                    customerAccountNomineeGuardian.CustomerAccountNomineeGuardianMakerCheckers.Add(customerAccountNomineeGuardianMakerChecker);

                    // CustomerAccountNomineeGuardianTranslation
                    context.CustomerAccountNomineeGuardianTranslations.Attach(customerAccountNomineeGuardianTranslation);
                    context.Entry(customerAccountNomineeGuardianTranslation).State = EntityState.Added;

                    // CustomerAccountNomineeGuardianTranslationMakerChecker
                    context.CustomerAccountNomineeGuardianTranslationMakerCheckers.Attach(customerAccountNomineeGuardianTranslationMakerChecker);
                    context.Entry(customerAccountNomineeGuardianTranslationMakerChecker).State = EntityState.Added;
                    customerAccountNomineeGuardianTranslation.CustomerAccountNomineeGuardianTranslationMakerCheckers.Add(customerAccountNomineeGuardianTranslationMakerChecker);
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

        public async Task<bool> Reject(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountNomineeViewModel.EntryDateTime = DateTime.Now;
                _customerAccountNomineeViewModel.UserAction = StringLiteralValue.Reject;
                _customerAccountNomineeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerNominee
                List<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = new List<CustomerAccountNomineeViewModel>();
                customerAccountNomineeViewModelList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["CustomerAccountNominee"];

                foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerAccountNomineeViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CustomerAccountNomineeMakerChecker customerAccountNomineeMakerChecker = Mapper.Map<CustomerAccountNomineeMakerChecker>(viewModel);
                    CustomerAccountNomineeTranslationMakerChecker customerAccountNomineeTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeTranslationMakerChecker>(viewModel);
                    CustomerAccountNomineeGuardianMakerChecker customerAccountNomineeGuardianMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianMakerChecker>(viewModel);
                    CustomerAccountNomineeGuardianTranslationMakerChecker customerAccountNomineeGuardianTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianTranslationMakerChecker>(viewModel);

                    context.CustomerAccountNomineeMakerCheckers.Attach(customerAccountNomineeMakerChecker);
                    context.Entry(customerAccountNomineeMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeTranslationMakerCheckers.Attach(customerAccountNomineeTranslationMakerChecker);
                    context.Entry(customerAccountNomineeTranslationMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeGuardianMakerCheckers.Attach(customerAccountNomineeGuardianMakerChecker);
                    context.Entry(customerAccountNomineeGuardianMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeGuardianTranslationMakerCheckers.Attach(customerAccountNomineeGuardianTranslationMakerChecker);
                    context.Entry(customerAccountNomineeGuardianTranslationMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Save(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountNomineeViewModel.ActivationStatus = StringLiteralValue.Active;
                _customerAccountNomineeViewModel.EntryDateTime = DateTime.Now;
                _customerAccountNomineeViewModel.EntryStatus = StringLiteralValue.Create;
                _customerAccountNomineeViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _customerAccountNomineeViewModel.ReasonForModification = "None";
                _customerAccountNomineeViewModel.Remark = "None";
                _customerAccountNomineeViewModel.UserAction = StringLiteralValue.Create;
                _customerAccountNomineeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerAccountNominee
                List<CustomerAccountNomineeViewModel> customerAccountNomineeList = new List<CustomerAccountNomineeViewModel>();
                customerAccountNomineeList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["CustomerAccountNominee"];


                foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeList)
                {
                    // Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Active;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.CustomerAccountPrmKey = _customerAccountNomineeViewModel.CustomerAccountPrmKey;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    //viewModel.PersonPrmKey = personRepository.GetPrmKeyById(viewModel.PersonId);

                    // Mapping CustomerAccountNominee
                    CustomerAccountNominee customerAccountNominee = Mapper.Map<CustomerAccountNominee>(_customerAccountNomineeViewModel);
                    CustomerAccountNomineeMakerChecker customerAccountNomineeMakerChecker = Mapper.Map<CustomerAccountNomineeMakerChecker>(_customerAccountNomineeViewModel);

                    // CustomerAccountNomineeGuardian
                    CustomerAccountNomineeGuardian customerAccountNomineeGuardian = Mapper.Map<CustomerAccountNomineeGuardian>(_customerAccountNomineeViewModel);
                    CustomerAccountNomineeGuardianMakerChecker customerAccountNomineeGuardianMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianMakerChecker>(_customerAccountNomineeViewModel);

                    // CustomerAccountNomineeGuardianTranslation
                    CustomerAccountNomineeGuardianTranslation customerAccountNomineeGuardianTranslation = Mapper.Map<CustomerAccountNomineeGuardianTranslation>(_customerAccountNomineeViewModel);
                    CustomerAccountNomineeGuardianTranslationMakerChecker customerAccountNomineeGuardianTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianTranslationMakerChecker>(_customerAccountNomineeViewModel);

                    // CustomerAccountNominee
                    context.CustomerAccountNominees.Attach(customerAccountNominee);
                    context.Entry(customerAccountNominee).State = EntityState.Added;

                    // CustomerAccountNomineeMakerChecker
                    context.CustomerAccountNomineeMakerCheckers.Attach(customerAccountNomineeMakerChecker);
                    context.Entry(customerAccountNomineeMakerChecker).State = EntityState.Added;
                    customerAccountNominee.CustomerAccountNomineeMakerCheckers.Add(customerAccountNomineeMakerChecker);

                    // CustomerAccountNomineeGuardian
                    context.CustomerAccountNomineeGuardians.Attach(customerAccountNomineeGuardian);
                    context.Entry(customerAccountNomineeGuardian).State = EntityState.Added;

                    // CustomerAccountNomineeGuardianMakerChecker
                    context.CustomerAccountNomineeGuardianMakerCheckers.Attach(customerAccountNomineeGuardianMakerChecker);
                    context.Entry(customerAccountNomineeGuardianMakerChecker).State = EntityState.Added;
                    customerAccountNomineeGuardian.CustomerAccountNomineeGuardianMakerCheckers.Add(customerAccountNomineeGuardianMakerChecker);

                    // CustomerAccountNomineeGuardianTranslation
                    context.CustomerAccountNomineeGuardianTranslations.Attach(customerAccountNomineeGuardianTranslation);
                    context.Entry(customerAccountNomineeGuardianTranslation).State = EntityState.Added;

                    // CustomerAccountNomineeGuardianTranslationMakerChecker
                    context.CustomerAccountNomineeGuardianTranslationMakerCheckers.Attach(customerAccountNomineeGuardianTranslationMakerChecker);
                    context.Entry(customerAccountNomineeGuardianTranslationMakerChecker).State = EntityState.Added;
                    customerAccountNomineeGuardianTranslation.CustomerAccountNomineeGuardianTranslationMakerCheckers.Add(customerAccountNomineeGuardianTranslationMakerChecker);
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

        public async Task<bool> Verify(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountNomineeViewModel.EntryDateTime = DateTime.Now;
                _customerAccountNomineeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerNominee
                IEnumerable<CustomerAccountNomineeViewModel> customerAccountNomineeViewModels = await GetVerifiedEntries(_customerAccountNomineeViewModel.CustomerAccountPrmKey);

                foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModels)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CustomerAccountNomineeMakerChecker customerAccountNomineeMakerChecker = Mapper.Map<CustomerAccountNomineeMakerChecker>(viewModel);
                    CustomerAccountNomineeTranslationMakerChecker customerAccountNomineeTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeTranslationMakerChecker>(viewModel);
                    CustomerAccountNomineeGuardianMakerChecker customerAccountNomineeGuardianMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianMakerChecker>(viewModel);
                    CustomerAccountNomineeGuardianTranslationMakerChecker customerAccountNomineeGuardianTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianTranslationMakerChecker>(viewModel);

                    context.CustomerAccountNomineeMakerCheckers.Attach(customerAccountNomineeMakerChecker);
                    context.Entry(customerAccountNomineeMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeTranslationMakerCheckers.Attach(customerAccountNomineeTranslationMakerChecker);
                    context.Entry(customerAccountNomineeTranslationMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeGuardianMakerCheckers.Attach(customerAccountNomineeGuardianMakerChecker);
                    context.Entry(customerAccountNomineeGuardianMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeGuardianTranslationMakerCheckers.Attach(customerAccountNomineeGuardianTranslationMakerChecker);
                    context.Entry(customerAccountNomineeGuardianTranslationMakerChecker).State = EntityState.Added;
                }

                // CustomerNominee
                List<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = new List<CustomerAccountNomineeViewModel>();
                customerAccountNomineeViewModelList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["CustomerAccountNominee"];

                foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerAccountNomineeViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CustomerAccountNomineeMakerChecker customerAccountNomineeMakerChecker = Mapper.Map<CustomerAccountNomineeMakerChecker>(viewModel);
                    CustomerAccountNomineeTranslationMakerChecker customerAccountNomineeTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeTranslationMakerChecker>(viewModel);
                    CustomerAccountNomineeGuardianMakerChecker customerAccountNomineeGuardianMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianMakerChecker>(viewModel);
                    CustomerAccountNomineeGuardianTranslationMakerChecker customerAccountNomineeGuardianTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianTranslationMakerChecker>(viewModel);

                    context.CustomerAccountNomineeMakerCheckers.Attach(customerAccountNomineeMakerChecker);
                    context.Entry(customerAccountNomineeMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeTranslationMakerCheckers.Attach(customerAccountNomineeTranslationMakerChecker);
                    context.Entry(customerAccountNomineeTranslationMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeGuardianMakerCheckers.Attach(customerAccountNomineeGuardianMakerChecker);
                    context.Entry(customerAccountNomineeGuardianMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeGuardianTranslationMakerCheckers.Attach(customerAccountNomineeGuardianTranslationMakerChecker);
                    context.Entry(customerAccountNomineeGuardianTranslationMakerChecker).State = EntityState.Added;
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

        //public long GetPrmKeyById(Guid _customerAccountNomineeId)
        //{
        //    var a = context.CustomerAccountNominees
        //            .Where(c => c.CustomerAccountNomineeId == _customerAccountNomineeId)
        //            .Select(c => c.PrmKey).FirstOrDefault();
        //    return a;
        //}



        public async Task<IEnumerable<CustomerAccountNomineeViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerAccountNomineeViewModel>("SELECT * FROM dbo.CustomerAccountNomineeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountNomineeViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerAccountNomineeViewModel>("SELECT * FROM dbo.CustomerAccountNomineeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountNomineeViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerAccountNomineeViewModel>("SELECT * FROM dbo.CustomerAccountNomineeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountNomineeViewModel>> GetRejectedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountNomineeViewModel>("SELECT * FROM dbo.GetCustomerAccountNomineeEntriesByCustomerPrmKey (@UserProfilePrmKey, @CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //public async Task<IEnumerable<CustomerAccountNomineeViewModel>> GetUnverifiedEntries(long _customerAccountPrmKey)
        //{
        //    try
        //    {
        //        return await context.Database.SqlQuery<CustomerAccountNomineeViewModel>("SELECT * FROM dbo.GetCustomerAccountNomineeEntriesByCustomerAccountPrmKey (@UserProfilePrmKey, @CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message;

        //        return null;
        //    }
        //}

        public async Task<IEnumerable<CustomerAccountNomineeViewModel>> GetUnVerifiedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountNomineeViewModel>("SELECT * FROM dbo.GetCustomerAccountNomineeEntriesByCustomerPrmKey (@UserProfilePrmKey, @CustomerPrmKey , @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerPrmKey ", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountNomineeViewModel>> GetVerifiedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountNomineeViewModel>("SELECT * FROM dbo.GetCustomerAccountNomineeEntriesByCustomerPrmKey (@UserProfilePrmKey, @CustomerPrmKey , @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerPrmKey ", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        

        public List<SelectListItem> CustomerAccountForNomineeDropdownList(List<string> _personId)
        {
            //Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            //If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                        from t in pt.DefaultIfEmpty()
                        where (_personId.Contains(p.PersonId.ToString()))
                                && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        orderby p.FirstName
                        select new SelectListItem
                        {
                            Value = p.PersonId.ToString(),
                            Text = ((mf.FullName.Equals(null)) ? p.FullName.Trim() + " ---> " + (t.TransFullName.Equals(null) ? " " : t.TransFullName.Trim()) : mf.FullName + " ---> " + (t.TransFullName.Equals(null) ? " " : t.TransFullName.Trim()))
                        }).ToList();

            }

            //Default List In Defaul Language(i.e.English)
            return (from p in context.People
                    join mf in context.PersonModifications on p.PrmKey equals mf.PersonPrmKey into pm
                    from mf in pm.DefaultIfEmpty()
                    where (_personId.Contains(p.PersonId.ToString()))
                    select new SelectListItem
                    {
                        Value = p.PersonId.ToString(),
                        Text = "" //((mf.NameOfCustomerAccount.Equals(null)) ? d.NameOfSharesCapitalCustomerAccount.Trim() : mf.NameOfSharesCapitalCustomerAccount)
                    }).ToList();


        }
    }
}
