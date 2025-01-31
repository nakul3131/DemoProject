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
    public class EFCustomerLoanAccountVehicleInsuranceDetailRepository : ICustomerLoanAccountVehicleInsuranceDetailRepository
    {
        private readonly EFDbContext context;

        public EFCustomerLoanAccountVehicleInsuranceDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(CustomerVehicleLoanInsuranceDetailViewModel _customerVehicleLoanInsuranceDetailViewModel)
        {
            try
            {
                // Set Default Value
                _customerVehicleLoanInsuranceDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanInsuranceDetailViewModel.EntryStatus = StringLiteralValue.Amend;
                //_customerVehicleLoanInsuranceDetailViewModel.ReasonForModification = "None";
                _customerVehicleLoanInsuranceDetailViewModel.UserAction = StringLiteralValue.Amend;
                _customerVehicleLoanInsuranceDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerVehicleLoanInsuranceDetail customerVehicleLoanInsuranceDetail = Mapper.Map<CustomerVehicleLoanInsuranceDetail>(_customerVehicleLoanInsuranceDetailViewModel);
                CustomerVehicleLoanInsuranceDetailMakerChecker customerVehicleLoanInsuranceDetailMakerChecker = Mapper.Map<CustomerVehicleLoanInsuranceDetailMakerChecker>(_customerVehicleLoanInsuranceDetailViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                customerVehicleLoanInsuranceDetail.PrmKey = _customerVehicleLoanInsuranceDetailViewModel.CustomerVehicleLoanInsuranceDetailPrmKey;

                //CustomerLoanAccountVehicleInsuranceDetail
                context.CustomerVehicleLoanInsuranceDetails.Attach(customerVehicleLoanInsuranceDetail);
                context.Entry(customerVehicleLoanInsuranceDetail).State = EntityState.Modified;

                //CustomerLoanAccountVehicleInsuranceDetailMakerChecker
                context.CustomerVehicleLoanInsuranceDetailMakerCheckers.Attach(customerVehicleLoanInsuranceDetailMakerChecker);
                context.Entry(customerVehicleLoanInsuranceDetailMakerChecker).State = EntityState.Added;
                customerVehicleLoanInsuranceDetail.CustomerVehicleLoanInsuranceDetailMakerCheckers.Add(customerVehicleLoanInsuranceDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(CustomerVehicleLoanInsuranceDetailViewModel _customerVehicleLoanInsuranceDetailViewModel)
        {
            try
            {
                // Set Default Value
                _customerVehicleLoanInsuranceDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanInsuranceDetailViewModel.UserAction = StringLiteralValue.Delete;
                _customerVehicleLoanInsuranceDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                CustomerVehicleLoanInsuranceDetailMakerChecker customerVehicleLoanInsuranceDetailMakerChecker = Mapper.Map<CustomerVehicleLoanInsuranceDetailMakerChecker>(_customerVehicleLoanInsuranceDetailViewModel);

                //CustomerLoanAccountVehicleInsuranceDetailMakerChecker
                context.CustomerVehicleLoanInsuranceDetailMakerCheckers.Attach(customerVehicleLoanInsuranceDetailMakerChecker);
                context.Entry(customerVehicleLoanInsuranceDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Modify(CustomerVehicleLoanInsuranceDetailViewModel _customerVehicleLoanInsuranceDetailViewModel)
        {
            try
            {
                // Set Default Value
                _customerVehicleLoanInsuranceDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanInsuranceDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _customerVehicleLoanInsuranceDetailViewModel.Remark = "None";
                _customerVehicleLoanInsuranceDetailViewModel.UserAction = StringLiteralValue.Create;
                _customerVehicleLoanInsuranceDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerVehicleLoanInsuranceDetail customerVehicleLoanInsuranceDetail = Mapper.Map<CustomerVehicleLoanInsuranceDetail>(_customerVehicleLoanInsuranceDetailViewModel);
                CustomerVehicleLoanInsuranceDetailMakerChecker customerVehicleLoanInsuranceDetailMakerChecker = Mapper.Map<CustomerVehicleLoanInsuranceDetailMakerChecker>(_customerVehicleLoanInsuranceDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //CustomerLoanAccountVehicleInsuranceDetail
                context.CustomerVehicleLoanInsuranceDetails.Attach(customerVehicleLoanInsuranceDetail);
                context.Entry(customerVehicleLoanInsuranceDetail).State = EntityState.Added;

                //CustomerLoanAccountVehicleInsuranceDetailMakerChecker
                context.CustomerVehicleLoanInsuranceDetailMakerCheckers.Attach(customerVehicleLoanInsuranceDetailMakerChecker);
                context.Entry(customerVehicleLoanInsuranceDetailMakerChecker).State = EntityState.Added;
                customerVehicleLoanInsuranceDetail.CustomerVehicleLoanInsuranceDetailMakerCheckers.Add(customerVehicleLoanInsuranceDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(CustomerVehicleLoanInsuranceDetailViewModel _customerVehicleLoanInsuranceDetailViewModel)
        {
            try
            {
                // Set Default Value
                _customerVehicleLoanInsuranceDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanInsuranceDetailViewModel.UserAction = StringLiteralValue.Reject;
                _customerVehicleLoanInsuranceDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                CustomerVehicleLoanInsuranceDetailMakerChecker customerVehicleLoanInsuranceDetailMakerChecker = Mapper.Map<CustomerVehicleLoanInsuranceDetailMakerChecker>(_customerVehicleLoanInsuranceDetailViewModel);

                //CustomerLoanAccountVehicleInsuranceDetailMakerChecker
                context.CustomerVehicleLoanInsuranceDetailMakerCheckers.Attach(customerVehicleLoanInsuranceDetailMakerChecker);
                context.Entry(customerVehicleLoanInsuranceDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(CustomerVehicleLoanInsuranceDetailViewModel _customerVehicleLoanInsuranceDetailViewModel)
        {
            try
            {
                // Set Default Value
                _customerVehicleLoanInsuranceDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanInsuranceDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _customerVehicleLoanInsuranceDetailViewModel.Remark = "None";
                //_customerVehicleLoanInsuranceDetailViewModel.ReasonForModification = "None";
                _customerVehicleLoanInsuranceDetailViewModel.UserAction = StringLiteralValue.Create;
                _customerVehicleLoanInsuranceDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerVehicleLoanInsuranceDetail customerVehicleLoanInsuranceDetail = Mapper.Map<CustomerVehicleLoanInsuranceDetail>(_customerVehicleLoanInsuranceDetailViewModel);
                CustomerVehicleLoanInsuranceDetailMakerChecker customerVehicleLoanInsuranceDetailMakerChecker = Mapper.Map<CustomerVehicleLoanInsuranceDetailMakerChecker>(_customerVehicleLoanInsuranceDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //CustomerLoanAccountVehicleInsuranceDetail
                context.CustomerVehicleLoanInsuranceDetails.Attach(customerVehicleLoanInsuranceDetail);
                context.Entry(customerVehicleLoanInsuranceDetail).State = EntityState.Added;

                //CustomerLoanAccountVehicleInsuranceDetailMakerChecker
                context.CustomerVehicleLoanInsuranceDetailMakerCheckers.Attach(customerVehicleLoanInsuranceDetailMakerChecker);
                context.Entry(customerVehicleLoanInsuranceDetailMakerChecker).State = EntityState.Added;
                customerVehicleLoanInsuranceDetail.CustomerVehicleLoanInsuranceDetailMakerCheckers.Add(customerVehicleLoanInsuranceDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(CustomerVehicleLoanInsuranceDetailViewModel _customerVehicleLoanInsuranceDetailViewModel)
        {
            try
            {
                // Assign MDF Status To EntryStatus For Performing Modify Operation

                CustomerVehicleLoanInsuranceDetailViewModel customerLoanAccountVehicleInsuranceDetailViewModelForModify = await GetVerifiedEntry(_customerVehicleLoanInsuranceDetailViewModel.CustomerLoanAccountPrmKey);
                if (customerLoanAccountVehicleInsuranceDetailViewModelForModify != null)
                {
                    // Set Default Value
                    customerLoanAccountVehicleInsuranceDetailViewModelForModify.EntryDateTime = DateTime.Now;
                    customerLoanAccountVehicleInsuranceDetailViewModelForModify.UserAction = StringLiteralValue.Modify;
                    customerLoanAccountVehicleInsuranceDetailViewModelForModify.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    CustomerVehicleLoanInsuranceDetailMakerChecker customerLoanAccountVehicleInsuranceDetailMakerCheckerForModify = Mapper.Map<CustomerVehicleLoanInsuranceDetailMakerChecker>(customerLoanAccountVehicleInsuranceDetailViewModelForModify);

                    //CustomerLoanAccountVehicleInsuranceDetailMakerChecker
                    context.CustomerVehicleLoanInsuranceDetailMakerCheckers.Attach(customerLoanAccountVehicleInsuranceDetailMakerCheckerForModify);
                    context.Entry(customerLoanAccountVehicleInsuranceDetailMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify New Record
                // Set Default Value
                _customerVehicleLoanInsuranceDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanInsuranceDetailViewModel.UserAction = StringLiteralValue.Verify;
                _customerVehicleLoanInsuranceDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerVehicleLoanInsuranceDetailMakerChecker customerVehicleLoanInsuranceDetailMakerChecker = Mapper.Map<CustomerVehicleLoanInsuranceDetailMakerChecker>(_customerVehicleLoanInsuranceDetailViewModel);

                //CustomerLoanAccountVehicleInsuranceDetailMakerCheckers
                context.CustomerVehicleLoanInsuranceDetailMakerCheckers.Attach(customerVehicleLoanInsuranceDetailMakerChecker);
                context.Entry(customerVehicleLoanInsuranceDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }


        public async Task<CustomerVehicleLoanInsuranceDetailViewModel> GetRejectedEntry(int _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanInsuranceDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountVehicleInsuranceDetailEntryByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerVehicleLoanInsuranceDetailViewModel> GetUnVerifiedEntry(int _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanInsuranceDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountVehicleInsuranceDetailEntryByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerVehicleLoanInsuranceDetailViewModel> GetVerifiedEntry(int _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanInsuranceDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountVehicleInsuranceDetailEntryByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerVehicleLoanInsuranceDetailViewModel> GetViewModelForCreate(int _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanInsuranceDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountVehicleInsuranceDetailEntryByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<IEnumerable<CustomerVehicleLoanInsuranceDetailViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanInsuranceDetailViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerLoanAccountVehicleInsuranceDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerVehicleLoanInsuranceDetailViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanInsuranceDetailViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerLoanAccountVehicleInsuranceDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerVehicleLoanInsuranceDetailViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanInsuranceDetailViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerLoanAccountVehicleInsuranceDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerVehicleLoanInsuranceDetailViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanInsuranceDetailViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerLoanAccountVehicleInsuranceDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }







    }
}
