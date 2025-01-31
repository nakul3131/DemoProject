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
    public class EFCustomerPreOwnedVehicleLoanInspectionRepository : ICustomerPreOwnedVehicleLoanInspectionRepository
    {
        private readonly EFDbContext context;

        public EFCustomerPreOwnedVehicleLoanInspectionRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(CustomerPreOwnedVehicleLoanInspectionViewModel _customerPreOwnedVehicleLoanInspectionViewModel)
        {
            try
            {
                // Set Default Value
                _customerPreOwnedVehicleLoanInspectionViewModel.EntryDateTime = DateTime.Now;
                _customerPreOwnedVehicleLoanInspectionViewModel.EntryStatus = StringLiteralValue.Amend;
                //_customerPreOwnedVehicleLoanInspectionViewModel.ReasonForModification = "None";
                _customerPreOwnedVehicleLoanInspectionViewModel.UserAction = StringLiteralValue.Amend;
                _customerPreOwnedVehicleLoanInspectionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerPreOwnedVehicleLoanInspection customerPreOwnedVehicleLoanInspection = Mapper.Map<CustomerPreOwnedVehicleLoanInspection>(_customerPreOwnedVehicleLoanInspectionViewModel);
                CustomerPreOwnedVehicleLoanInspectionMakerChecker customerPreOwnedVehicleLoanInspectionMakerChecker = Mapper.Map<CustomerPreOwnedVehicleLoanInspectionMakerChecker>(_customerPreOwnedVehicleLoanInspectionViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                customerPreOwnedVehicleLoanInspection.PrmKey = _customerPreOwnedVehicleLoanInspectionViewModel.CustomerPreOwnedVehicleLoanInspectionPrmKey;

                //CustomerPreOwnedVehicleLoanInspection
                context.CustomerPreOwnedVehicleLoanInspections.Attach(customerPreOwnedVehicleLoanInspection);
                context.Entry(customerPreOwnedVehicleLoanInspection).State = EntityState.Modified;

                //CustomerPreOwnedVehicleLoanInspectionMakerChecker
                context.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Attach(customerPreOwnedVehicleLoanInspectionMakerChecker);
                context.Entry(customerPreOwnedVehicleLoanInspectionMakerChecker).State = EntityState.Added;
                customerPreOwnedVehicleLoanInspection.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Add(customerPreOwnedVehicleLoanInspectionMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(CustomerPreOwnedVehicleLoanInspectionViewModel _customerPreOwnedVehicleLoanInspectionViewModel)
        {
            try
            {
                // Set Default Value
                _customerPreOwnedVehicleLoanInspectionViewModel.EntryDateTime = DateTime.Now;
                _customerPreOwnedVehicleLoanInspectionViewModel.UserAction = StringLiteralValue.Delete;
                _customerPreOwnedVehicleLoanInspectionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                CustomerPreOwnedVehicleLoanInspectionMakerChecker customerPreOwnedVehicleLoanInspectionMakerChecker = Mapper.Map<CustomerPreOwnedVehicleLoanInspectionMakerChecker>(_customerPreOwnedVehicleLoanInspectionViewModel);

                //CustomerPreOwnedVehicleLoanInspectionMakerChecker
                context.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Attach(customerPreOwnedVehicleLoanInspectionMakerChecker);
                context.Entry(customerPreOwnedVehicleLoanInspectionMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Modify(CustomerPreOwnedVehicleLoanInspectionViewModel _customerPreOwnedVehicleLoanInspectionViewModel)
        {
            try
            {
                // Set Default Value
                _customerPreOwnedVehicleLoanInspectionViewModel.EntryDateTime = DateTime.Now;
                _customerPreOwnedVehicleLoanInspectionViewModel.EntryStatus = StringLiteralValue.Create;
                _customerPreOwnedVehicleLoanInspectionViewModel.Remark = "None";
                _customerPreOwnedVehicleLoanInspectionViewModel.UserAction = StringLiteralValue.Create;
                _customerPreOwnedVehicleLoanInspectionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerPreOwnedVehicleLoanInspection customerPreOwnedVehicleLoanInspection = Mapper.Map<CustomerPreOwnedVehicleLoanInspection>(_customerPreOwnedVehicleLoanInspectionViewModel);
                CustomerPreOwnedVehicleLoanInspectionMakerChecker customerPreOwnedVehicleLoanInspectionMakerChecker = Mapper.Map<CustomerPreOwnedVehicleLoanInspectionMakerChecker>(_customerPreOwnedVehicleLoanInspectionViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //CustomerPreOwnedVehicleLoanInspection
                context.CustomerPreOwnedVehicleLoanInspections.Attach(customerPreOwnedVehicleLoanInspection);
                context.Entry(customerPreOwnedVehicleLoanInspection).State = EntityState.Added;

                //CustomerPreOwnedVehicleLoanInspectionMakerChecker
                context.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Attach(customerPreOwnedVehicleLoanInspectionMakerChecker);
                context.Entry(customerPreOwnedVehicleLoanInspectionMakerChecker).State = EntityState.Added;
                customerPreOwnedVehicleLoanInspection.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Add(customerPreOwnedVehicleLoanInspectionMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(CustomerPreOwnedVehicleLoanInspectionViewModel _customerPreOwnedVehicleLoanInspectionViewModel)
        {
            try
            {
                // Set Default Value
                _customerPreOwnedVehicleLoanInspectionViewModel.EntryDateTime = DateTime.Now;
                _customerPreOwnedVehicleLoanInspectionViewModel.UserAction = StringLiteralValue.Reject;
                _customerPreOwnedVehicleLoanInspectionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                CustomerPreOwnedVehicleLoanInspectionMakerChecker customerPreOwnedVehicleLoanInspectionMakerChecker = Mapper.Map<CustomerPreOwnedVehicleLoanInspectionMakerChecker>(_customerPreOwnedVehicleLoanInspectionViewModel);

                //CustomerPreOwnedVehicleLoanInspectionMakerChecker
                context.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Attach(customerPreOwnedVehicleLoanInspectionMakerChecker);
                context.Entry(customerPreOwnedVehicleLoanInspectionMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(CustomerPreOwnedVehicleLoanInspectionViewModel _customerPreOwnedVehicleLoanInspectionViewModel)
        {
            try
            {
                // Set Default Value
                _customerPreOwnedVehicleLoanInspectionViewModel.EntryDateTime = DateTime.Now;
                _customerPreOwnedVehicleLoanInspectionViewModel.EntryStatus = StringLiteralValue.Create;
                _customerPreOwnedVehicleLoanInspectionViewModel.Remark = "None";
                //_customerPreOwnedVehicleLoanInspectionViewModel.ReasonForModification = "None";
                _customerPreOwnedVehicleLoanInspectionViewModel.UserAction = StringLiteralValue.Create;
                _customerPreOwnedVehicleLoanInspectionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerPreOwnedVehicleLoanInspection customerPreOwnedVehicleLoanInspection = Mapper.Map<CustomerPreOwnedVehicleLoanInspection>(_customerPreOwnedVehicleLoanInspectionViewModel);
                CustomerPreOwnedVehicleLoanInspectionMakerChecker customerPreOwnedVehicleLoanInspectionMakerChecker = Mapper.Map<CustomerPreOwnedVehicleLoanInspectionMakerChecker>(_customerPreOwnedVehicleLoanInspectionViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //CustomerPreOwnedVehicleLoanInspection
                context.CustomerPreOwnedVehicleLoanInspections.Attach(customerPreOwnedVehicleLoanInspection);
                context.Entry(customerPreOwnedVehicleLoanInspection).State = EntityState.Added;

                //CustomerPreOwnedVehicleLoanInspectionMakerChecker
                context.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Attach(customerPreOwnedVehicleLoanInspectionMakerChecker);
                context.Entry(customerPreOwnedVehicleLoanInspectionMakerChecker).State = EntityState.Added;
                customerPreOwnedVehicleLoanInspection.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Add(customerPreOwnedVehicleLoanInspectionMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(CustomerPreOwnedVehicleLoanInspectionViewModel _customerPreOwnedVehicleLoanInspectionViewModel)
        {
            try
            {
                // Assign MDF Status To EntryStatus For Performing Modify Operation

                CustomerPreOwnedVehicleLoanInspectionViewModel customerPreOwnedVehicleLoanInspectionViewModelForModify = await GetVerifiedEntry(_customerPreOwnedVehicleLoanInspectionViewModel.CustomerLoanAccountPrmKey);
                if (customerPreOwnedVehicleLoanInspectionViewModelForModify != null)
                {
                    // Set Default Value
                    customerPreOwnedVehicleLoanInspectionViewModelForModify.EntryDateTime = DateTime.Now;
                    customerPreOwnedVehicleLoanInspectionViewModelForModify.UserAction = StringLiteralValue.Modify;
                    customerPreOwnedVehicleLoanInspectionViewModelForModify.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    CustomerPreOwnedVehicleLoanInspectionMakerChecker customerPreOwnedVehicleLoanInspectionMakerCheckerForModify = Mapper.Map<CustomerPreOwnedVehicleLoanInspectionMakerChecker>(customerPreOwnedVehicleLoanInspectionViewModelForModify);

                    //CustomerPreOwnedVehicleLoanInspectionMakerChecker
                    context.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Attach(customerPreOwnedVehicleLoanInspectionMakerCheckerForModify);
                    context.Entry(customerPreOwnedVehicleLoanInspectionMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify New Record
                // Set Default Value
                _customerPreOwnedVehicleLoanInspectionViewModel.EntryDateTime = DateTime.Now;
                _customerPreOwnedVehicleLoanInspectionViewModel.UserAction = StringLiteralValue.Verify;
                _customerPreOwnedVehicleLoanInspectionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerPreOwnedVehicleLoanInspectionMakerChecker customerPreOwnedVehicleLoanInspectionMakerChecker = Mapper.Map<CustomerPreOwnedVehicleLoanInspectionMakerChecker>(_customerPreOwnedVehicleLoanInspectionViewModel);

                //CustomerPreOwnedVehicleLoanInspectionMakerCheckers
                context.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Attach(customerPreOwnedVehicleLoanInspectionMakerChecker);
                context.Entry(customerPreOwnedVehicleLoanInspectionMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }


        public async Task<CustomerPreOwnedVehicleLoanInspectionViewModel> GetRejectedEntry(int _CustomerVehicleLoanCollateralDetailPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerPreOwnedVehicleLoanInspectionViewModel>("SELECT * FROM dbo.GetCustomerPreOwnedVehicleLoanInspectionEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerPreOwnedVehicleLoanInspectionPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerPreOwnedVehicleLoanInspectionPrmKey", _CustomerVehicleLoanCollateralDetailPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerPreOwnedVehicleLoanInspectionViewModel> GetUnVerifiedEntry(int _CustomerVehicleLoanCollateralDetailPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerPreOwnedVehicleLoanInspectionViewModel>("SELECT * FROM dbo.GetCustomerPreOwnedVehicleLoanInspectionEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerPreOwnedVehicleLoanInspectionPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerPreOwnedVehicleLoanInspectionPrmKey", _CustomerVehicleLoanCollateralDetailPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerPreOwnedVehicleLoanInspectionViewModel> GetVerifiedEntry(int _CustomerVehicleLoanCollateralDetailPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerPreOwnedVehicleLoanInspectionViewModel>("SELECT * FROM dbo.GetCustomerPreOwnedVehicleLoanInspectionEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerPreOwnedVehicleLoanInspectionPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerPreOwnedVehicleLoanInspectionPrmKey", _CustomerVehicleLoanCollateralDetailPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerPreOwnedVehicleLoanInspectionViewModel> GetViewModelForCreate(int CustomerVehicleLoanCollateralDetailPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerPreOwnedVehicleLoanInspectionViewModel>("SELECT * FROM dbo.GetCustomerPreOwnedVehicleLoanInspectionEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", CustomerVehicleLoanCollateralDetailPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<IEnumerable<CustomerPreOwnedVehicleLoanInspectionViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerPreOwnedVehicleLoanInspectionViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerPreOwnedVehicleLoanInspectionCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerPreOwnedVehicleLoanInspectionViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerPreOwnedVehicleLoanInspectionViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerPreOwnedVehicleLoanInspectionCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerPreOwnedVehicleLoanInspectionViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerPreOwnedVehicleLoanInspectionViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerPreOwnedVehicleLoanInspectionCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerPreOwnedVehicleLoanInspectionViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerPreOwnedVehicleLoanInspectionViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerPreOwnedVehicleLoanInspectionCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }



       

    }
}
