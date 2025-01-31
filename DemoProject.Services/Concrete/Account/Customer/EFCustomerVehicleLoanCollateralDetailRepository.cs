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
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerVehicleLoanCollateralDetailRepository : ICustomerVehicleLoanCollateralDetailRepository
    {
        private readonly EFDbContext context;

        public EFCustomerVehicleLoanCollateralDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }


        public int GetPrmKeyByCustomerLoanAccountPrmKey(int _customerLoanAccountPrmKey)
        {
            return context.CustomerVehicleLoanCollateralDetails
                    .Where(c => c.CustomerLoanAccountPrmKey == _customerLoanAccountPrmKey)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }


        public async Task<bool> Amend(CustomerVehicleLoanCollateralDetailViewModel _customerVehicleLoanCollateralDetailViewModel)
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
                CustomerVehicleLoanCollateralDetail customerVehicleLoanCollateralDetail = Mapper.Map<CustomerVehicleLoanCollateralDetail>(_customerVehicleLoanCollateralDetailViewModel);
                CustomerVehicleLoanCollateralDetailMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerVehicleLoanCollateralDetailMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                customerVehicleLoanCollateralDetail.PrmKey = _customerVehicleLoanCollateralDetailViewModel.CustomerVehicleLoanCollateralDetailPrmKey;

                //CustomerVehicleLoanCollateralDetail
                context.CustomerVehicleLoanCollateralDetails.Attach(customerVehicleLoanCollateralDetail);
                context.Entry(customerVehicleLoanCollateralDetail).State = EntityState.Modified;

                //CustomerVehicleLoanCollateralDetailMakerChecker
                context.CustomerVehicleLoanCollateralDetailMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
                context.Entry(customerVehicleLoanCollateralDetailMakerChecker).State = EntityState.Added;
                customerVehicleLoanCollateralDetail.CustomerVehicleLoanCollateralDetailMakerCheckers.Add(customerVehicleLoanCollateralDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(CustomerVehicleLoanCollateralDetailViewModel _customerVehicleLoanCollateralDetailViewModel)
        {
            try
            {
                // Set Default Value
                _customerVehicleLoanCollateralDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanCollateralDetailViewModel.UserAction = StringLiteralValue.Delete;
                _customerVehicleLoanCollateralDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                CustomerVehicleLoanCollateralDetailMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerVehicleLoanCollateralDetailMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                //CustomerVehicleLoanCollateralDetailMakerChecker
                context.CustomerVehicleLoanCollateralDetailMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
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

        public async Task<bool> Modify(CustomerVehicleLoanCollateralDetailViewModel _customerVehicleLoanCollateralDetailViewModel)
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
                CustomerVehicleLoanCollateralDetail customerVehicleLoanCollateralDetail = Mapper.Map<CustomerVehicleLoanCollateralDetail>(_customerVehicleLoanCollateralDetailViewModel);
                CustomerVehicleLoanCollateralDetailMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerVehicleLoanCollateralDetailMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //CustomerVehicleLoanCollateralDetail
                context.CustomerVehicleLoanCollateralDetails.Attach(customerVehicleLoanCollateralDetail);
                context.Entry(customerVehicleLoanCollateralDetail).State = EntityState.Added;

                //CustomerVehicleLoanCollateralDetailMakerChecker
                context.CustomerVehicleLoanCollateralDetailMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
                context.Entry(customerVehicleLoanCollateralDetailMakerChecker).State = EntityState.Added;
                customerVehicleLoanCollateralDetail.CustomerVehicleLoanCollateralDetailMakerCheckers.Add(customerVehicleLoanCollateralDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(CustomerVehicleLoanCollateralDetailViewModel _customerVehicleLoanCollateralDetailViewModel)
        {
            try
            {
                // Set Default Value
                _customerVehicleLoanCollateralDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanCollateralDetailViewModel.UserAction = StringLiteralValue.Reject;
                _customerVehicleLoanCollateralDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                CustomerVehicleLoanCollateralDetailMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerVehicleLoanCollateralDetailMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                //CustomerVehicleLoanCollateralDetailMakerChecker
                context.CustomerVehicleLoanCollateralDetailMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
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

        public async Task<bool> Save(CustomerVehicleLoanCollateralDetailViewModel _customerVehicleLoanCollateralDetailViewModel)
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
                CustomerVehicleLoanCollateralDetail customerVehicleLoanCollateralDetail = Mapper.Map<CustomerVehicleLoanCollateralDetail>(_customerVehicleLoanCollateralDetailViewModel);
                CustomerVehicleLoanCollateralDetailMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerVehicleLoanCollateralDetailMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //CustomerVehicleLoanCollateralDetail
                context.CustomerVehicleLoanCollateralDetails.Attach(customerVehicleLoanCollateralDetail);
                context.Entry(customerVehicleLoanCollateralDetail).State = EntityState.Added;

                //CustomerVehicleLoanCollateralDetailMakerChecker
                context.CustomerVehicleLoanCollateralDetailMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
                context.Entry(customerVehicleLoanCollateralDetailMakerChecker).State = EntityState.Added;
                customerVehicleLoanCollateralDetail.CustomerVehicleLoanCollateralDetailMakerCheckers.Add(customerVehicleLoanCollateralDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(CustomerVehicleLoanCollateralDetailViewModel _customerVehicleLoanCollateralDetailViewModel)
        {
            try
            {
                // Assign MDF Status To EntryStatus For Performing Modify Operation

                CustomerVehicleLoanCollateralDetailViewModel customerVehicleLoanCollateralDetailViewModelForModify = await GetVerifiedEntry(_customerVehicleLoanCollateralDetailViewModel.CustomerLoanAccountPrmKey);
                if (customerVehicleLoanCollateralDetailViewModelForModify != null)
                {
                    // Set Default Value
                    customerVehicleLoanCollateralDetailViewModelForModify.EntryDateTime = DateTime.Now;
                    customerVehicleLoanCollateralDetailViewModelForModify.UserAction = StringLiteralValue.Modify;
                    customerVehicleLoanCollateralDetailViewModelForModify.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    CustomerVehicleLoanCollateralDetailMakerChecker customerVehicleLoanCollateralDetailMakerCheckerForModify = Mapper.Map<CustomerVehicleLoanCollateralDetailMakerChecker>(customerVehicleLoanCollateralDetailViewModelForModify);

                    //CustomerVehicleLoanCollateralDetailMakerChecker
                    context.CustomerVehicleLoanCollateralDetailMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerCheckerForModify);
                    context.Entry(customerVehicleLoanCollateralDetailMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify New Record
                // Set Default Value
                _customerVehicleLoanCollateralDetailViewModel.EntryDateTime = DateTime.Now;
                _customerVehicleLoanCollateralDetailViewModel.UserAction = StringLiteralValue.Verify;
                _customerVehicleLoanCollateralDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerVehicleLoanCollateralDetailMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerVehicleLoanCollateralDetailMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                //CustomerVehicleLoanCollateralDetailMakerCheckers
                context.CustomerVehicleLoanCollateralDetailMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
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


        public async Task<CustomerVehicleLoanCollateralDetailViewModel> GetRejectedEntry(int _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanCollateralDetailViewModel>("SELECT * FROM dbo.GetCustomerVehicleLoanCollateralDetailEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerVehicleLoanCollateralDetailViewModel> GetUnVerifiedEntry(int _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanCollateralDetailViewModel>("SELECT * FROM dbo.GetCustomerVehicleLoanCollateralDetailEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerVehicleLoanCollateralDetailViewModel> GetVerifiedEntry(int _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanCollateralDetailViewModel>("SELECT * FROM dbo.GetCustomerVehicleLoanCollateralDetailEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerVehicleLoanCollateralDetailViewModel> GetViewModelForCreate(int _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanCollateralDetailViewModel>("SELECT * FROM dbo.GetCustomerVehicleLoanCollateralDetailEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<IEnumerable<CustomerVehicleLoanCollateralDetailViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanCollateralDetailViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerVehicleLoanCollateralDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerVehicleLoanCollateralDetailViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanCollateralDetailViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerVehicleLoanCollateralDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerVehicleLoanCollateralDetailViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanCollateralDetailViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerVehicleLoanCollateralDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerVehicleLoanCollateralDetailViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanCollateralDetailViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerVehicleLoanCollateralDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }



        


    }
}
