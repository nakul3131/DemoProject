using AutoMapper;
using DemoProject.Domain.Entities.Account.Customer;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Account.Customer
{
   public class EFCustomerLoanFieldInvestigationRepository : ICustomerLoanFieldInvestigationRepository
    {
        private readonly EFDbContext context;

        public EFCustomerLoanFieldInvestigationRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(CustomerLoanFieldInvestigationViewModel _customerVehicleLoanCollateralDetailViewModel)
        {
            try
            {
                // Set Default Value
                _customerVehicleLoanCollateralDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanCollateralDetailViewModel.EntryStatus = StringLiteralValue.Amend;
                //_customerVehicleLoanCollateralDetailViewModel.ReasonForModification = "None";
                _customerVehicleLoanCollateralDetailViewModel.UserAction = StringLiteralValue.Amend;
                _customerVehicleLoanCollateralDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerLoanFieldInvestigation customerVehicleLoanCollateralDetail = Mapper.Map<CustomerLoanFieldInvestigation>(_customerVehicleLoanCollateralDetailViewModel);
                CustomerLoanFieldInvestigationMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerLoanFieldInvestigationMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                customerVehicleLoanCollateralDetail.PrmKey = _customerVehicleLoanCollateralDetailViewModel.CustomerLoanFieldInvestigationPrmKey;

                //CustomerLoanFieldInvestigation
                context.CustomerLoanFieldInvestigations.Attach(customerVehicleLoanCollateralDetail);
                context.Entry(customerVehicleLoanCollateralDetail).State = EntityState.Modified;

                //CustomerLoanFieldInvestigationMakerChecker
                context.CustomerLoanFieldInvestigationMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
                context.Entry(customerVehicleLoanCollateralDetailMakerChecker).State = EntityState.Added;
                customerVehicleLoanCollateralDetail.CustomerLoanFieldInvestigationMakerCheckers.Add(customerVehicleLoanCollateralDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(CustomerLoanFieldInvestigationViewModel _customerVehicleLoanCollateralDetailViewModel)
        {
            try
            {
                // Set Default Value
                _customerVehicleLoanCollateralDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanCollateralDetailViewModel.UserAction = StringLiteralValue.Delete;
                _customerVehicleLoanCollateralDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                CustomerLoanFieldInvestigationMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerLoanFieldInvestigationMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                //CustomerLoanFieldInvestigationMakerChecker
                context.CustomerLoanFieldInvestigationMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
                context.Entry(customerVehicleLoanCollateralDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<CustomerLoanFieldInvestigationViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanFieldInvestigationViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerLoanFieldInvestigationCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerLoanFieldInvestigationViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanFieldInvestigationViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerLoanFieldInvestigationCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerLoanFieldInvestigationViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanFieldInvestigationViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerLoanFieldInvestigationCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerLoanFieldInvestigationViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanFieldInvestigationViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerLoanFieldInvestigationCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerLoanFieldInvestigationViewModel> GetViewModelForCreate(int _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanFieldInvestigationViewModel>("SELECT * FROM dbo.GetCustomerLoanFieldInvestigationEntryByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerLoanFieldInvestigationViewModel> GetRejectedEntry(int _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanFieldInvestigationViewModel>("SELECT * FROM dbo.GetCustomerLoanFieldInvestigationEntryByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerLoanFieldInvestigationViewModel> GetUnVerifiedEntry(int _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanFieldInvestigationViewModel>("SELECT * FROM dbo.GetCustomerLoanFieldInvestigationEntryByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerLoanFieldInvestigationViewModel> GetVerifiedEntry(int _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanFieldInvestigationViewModel>("SELECT * FROM dbo.GetCustomerLoanFieldInvestigationEntryByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(CustomerLoanFieldInvestigationViewModel _customerVehicleLoanCollateralDetailViewModel)
        {
            try
            {
                // Set Default Value
                _customerVehicleLoanCollateralDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanCollateralDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _customerVehicleLoanCollateralDetailViewModel.Remark = "None";
                _customerVehicleLoanCollateralDetailViewModel.UserAction = StringLiteralValue.Create;
                _customerVehicleLoanCollateralDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerLoanFieldInvestigation customerVehicleLoanCollateralDetail = Mapper.Map<CustomerLoanFieldInvestigation>(_customerVehicleLoanCollateralDetailViewModel);
                CustomerLoanFieldInvestigationMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerLoanFieldInvestigationMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //CustomerLoanFieldInvestigation
                context.CustomerLoanFieldInvestigations.Attach(customerVehicleLoanCollateralDetail);
                context.Entry(customerVehicleLoanCollateralDetail).State = EntityState.Added;

                //CustomerLoanFieldInvestigationMakerChecker
                context.CustomerLoanFieldInvestigationMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
                context.Entry(customerVehicleLoanCollateralDetailMakerChecker).State = EntityState.Added;
                customerVehicleLoanCollateralDetail.CustomerLoanFieldInvestigationMakerCheckers.Add(customerVehicleLoanCollateralDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(CustomerLoanFieldInvestigationViewModel _customerVehicleLoanCollateralDetailViewModel)
        {
            try
            {
                // Set Default Value
                _customerVehicleLoanCollateralDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanCollateralDetailViewModel.UserAction = StringLiteralValue.Reject;
                _customerVehicleLoanCollateralDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                CustomerLoanFieldInvestigationMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerLoanFieldInvestigationMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                //CustomerLoanFieldInvestigationMakerChecker
                context.CustomerLoanFieldInvestigationMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
                context.Entry(customerVehicleLoanCollateralDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(CustomerLoanFieldInvestigationViewModel _customerVehicleLoanCollateralDetailViewModel)
        {
            try
            {
                // Set Default Value
                _customerVehicleLoanCollateralDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanCollateralDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _customerVehicleLoanCollateralDetailViewModel.Remark = "None";
                //_customerVehicleLoanCollateralDetailViewModel.ReasonForModification = "None";
                _customerVehicleLoanCollateralDetailViewModel.UserAction = StringLiteralValue.Create;
                _customerVehicleLoanCollateralDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerLoanFieldInvestigation customerVehicleLoanCollateralDetail = Mapper.Map<CustomerLoanFieldInvestigation>(_customerVehicleLoanCollateralDetailViewModel);
                CustomerLoanFieldInvestigationMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerLoanFieldInvestigationMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //CustomerLoanFieldInvestigation
                context.CustomerLoanFieldInvestigations.Attach(customerVehicleLoanCollateralDetail);
                context.Entry(customerVehicleLoanCollateralDetail).State = EntityState.Added;

                //CustomerLoanFieldInvestigationMakerChecker
                context.CustomerLoanFieldInvestigationMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
                context.Entry(customerVehicleLoanCollateralDetailMakerChecker).State = EntityState.Added;
                customerVehicleLoanCollateralDetail.CustomerLoanFieldInvestigationMakerCheckers.Add(customerVehicleLoanCollateralDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(CustomerLoanFieldInvestigationViewModel _customerVehicleLoanCollateralDetailViewModel)
        {
            try
            {
                // Assign MDF Status To EntryStatus For Performing Modify Operation

                CustomerLoanFieldInvestigationViewModel customerVehicleLoanCollateralDetailViewModelForModify = await GetVerifiedEntry(_customerVehicleLoanCollateralDetailViewModel.CustomerLoanAccountPrmKey);
                if (customerVehicleLoanCollateralDetailViewModelForModify != null)
                {
                    // Set Default Value
                    customerVehicleLoanCollateralDetailViewModelForModify.EntryDateTime = DateTime.Now;
                    customerVehicleLoanCollateralDetailViewModelForModify.UserAction = StringLiteralValue.Modify;
                    customerVehicleLoanCollateralDetailViewModelForModify.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    CustomerLoanFieldInvestigationMakerChecker customerVehicleLoanCollateralDetailMakerCheckerForModify = Mapper.Map<CustomerLoanFieldInvestigationMakerChecker>(customerVehicleLoanCollateralDetailViewModelForModify);

                    //CustomerLoanFieldInvestigationMakerChecker
                    context.CustomerLoanFieldInvestigationMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerCheckerForModify);
                    context.Entry(customerVehicleLoanCollateralDetailMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify New Record
                // Set Default Value
                _customerVehicleLoanCollateralDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanCollateralDetailViewModel.UserAction = StringLiteralValue.Verify;
                _customerVehicleLoanCollateralDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerLoanFieldInvestigationMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerLoanFieldInvestigationMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                //CustomerLoanFieldInvestigationMakerCheckers
                context.CustomerLoanFieldInvestigationMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
                context.Entry(customerVehicleLoanCollateralDetailMakerChecker).State = EntityState.Added;

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
