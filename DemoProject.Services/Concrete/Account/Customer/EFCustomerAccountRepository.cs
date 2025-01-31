using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Domain.Entities.Account.Customer;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerAccountRepository : ICustomerAccountRepository
    {
        private readonly EFDbContext context;
        public readonly IGeneralLedgerRepository generalLedgerRepository;
        public readonly IPersonRepository personRepository;

        public EFCustomerAccountRepository(RepositoryConnection _connection, IGeneralLedgerRepository _generalLedgerRepository, IPersonRepository _personRepository)
        {
            context = _connection.EFDbContext;
            generalLedgerRepository = _generalLedgerRepository;
            personRepository = _personRepository;
        }


        public short GetCustomerAccountSchemePrmKeyById(Guid _customerAccountId)
        {
            long customerAccountPrmKey = GetPrmKeyById(_customerAccountId);

            return (from a in context.CustomerAccounts
                    join d in context.CustomerAccountDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on a.PrmKey equals d.CustomerAccountPrmKey into ad
                    from d in ad.DefaultIfEmpty()
                    where (a.PrmKey == customerAccountPrmKey)
                        && (a.EntryStatus == StringLiteralValue.Verify)
                    select (d.SchemePrmKey)).FirstOrDefault();
        }

        public long GetPrmKeyById(Guid _customerAccountId)
        {
            var a = context.CustomerAccounts
                    .Where(c => c.CustomerAccountId == _customerAccountId)
                    .Select(c => c.PrmKey).FirstOrDefault();
            return a;
        }


        public async Task<bool> Amend(CustomerAccountViewModel _customerAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountViewModel.EntryDateTime = DateTime.Now;
                _customerAccountViewModel.EntryStatus = StringLiteralValue.Amend;
                _customerAccountViewModel.Remark = "None";
                _customerAccountViewModel.UserAction = StringLiteralValue.Amend;
                _customerAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns

                // Mapping
                // CustomerAccount
                CustomerAccount customerAccountForAmend = Mapper.Map<CustomerAccount>(_customerAccountViewModel);
                CustomerAccountMakerChecker customerAccountMakerCheckerForAmend = Mapper.Map<CustomerAccountMakerChecker>(_customerAccountViewModel);


                // Set ReferenceKey As PrmKey To Every Object
                customerAccountForAmend.PrmKey = _customerAccountViewModel.CustomerAccountPrmKey;

                // Check Entry Existance In Modification Table Or Main Table
                // CustomerAccount
                context.CustomerAccountMakerCheckers.Attach(customerAccountMakerCheckerForAmend);
                context.Entry(customerAccountMakerCheckerForAmend).State = EntityState.Added;
                customerAccountForAmend.CustomerAccountMakerCheckers.Add(customerAccountMakerCheckerForAmend);

                context.CustomerAccounts.Attach(customerAccountForAmend);
                context.Entry(customerAccountForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(CustomerAccountViewModel _customerAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountViewModel.EntryDateTime = DateTime.Now;
                _customerAccountViewModel.Remark = "None";
                _customerAccountViewModel.UserAction = StringLiteralValue.Delete;
                _customerAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // Mapping
                CustomerAccountMakerChecker customerAccountMakerChecker = Mapper.Map<CustomerAccountMakerChecker>(_customerAccountViewModel);

                //CustomerAccountMakerChecker
                context.CustomerAccountMakerCheckers.Attach(customerAccountMakerChecker);
                context.Entry(customerAccountMakerChecker).State = EntityState.Added;

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
        public async Task<bool> Modify(CustomerAccountViewModel _customerAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _customerAccountViewModel.EntryDateTime = DateTime.Now;
                _customerAccountViewModel.EntryStatus = StringLiteralValue.Create;
                _customerAccountViewModel.Remark = "None";
                _customerAccountViewModel.ReasonForModification = "None";
                _customerAccountViewModel.UserAction = StringLiteralValue.Create;
                _customerAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns 

                // Mapping
                //CustomerAccountModification
                CustomerAccountModification customerAccountModification = Mapper.Map<CustomerAccountModification>(_customerAccountViewModel);
                CustomerAccountModificationMakerChecker customerAccountModificationMakerChecker = Mapper.Map<CustomerAccountModificationMakerChecker>(_customerAccountViewModel);

                //CustomerAccountModification
                context.CustomerAccountModificationMakerCheckers.Attach(customerAccountModificationMakerChecker);
                context.Entry(customerAccountModificationMakerChecker).State = EntityState.Added;
                customerAccountModification.CustomerAccountModificationMakerCheckers.Add(customerAccountModificationMakerChecker);

                context.CustomerAccountModifications.Attach(customerAccountModification);
                context.Entry(customerAccountModification).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(CustomerAccountViewModel _customerAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountViewModel.EntryDateTime = DateTime.Now;
                _customerAccountViewModel.UserAction = StringLiteralValue.Reject;
                _customerAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _customerAccountViewModel.Remark = "None";

                // Mapping
                CustomerAccountMakerChecker customerAccountMakerChecker = Mapper.Map<CustomerAccountMakerChecker>(_customerAccountViewModel);

                //CustomerAccountMakerChecker
                context.CustomerAccountMakerCheckers.Attach(customerAccountMakerChecker);
                context.Entry(customerAccountMakerChecker).State = EntityState.Added;

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
        public async Task<bool> Save(CustomerAccountViewModel _customerAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountViewModel.EntryDateTime = DateTime.Now;
                _customerAccountViewModel.EntryStatus = StringLiteralValue.Create;
                _customerAccountViewModel.Remark = "None";
                _customerAccountViewModel.UserAction = StringLiteralValue.Create;
                _customerAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns

                // Mapping
                // CustomerAccount
                CustomerAccount customerAccount = Mapper.Map<CustomerAccount>(_customerAccountViewModel);
                CustomerAccountMakerChecker customerAccountMakerChecker = Mapper.Map<CustomerAccountMakerChecker>(_customerAccountViewModel);

                // customerAccount
                context.CustomerAccountMakerCheckers.Attach(customerAccountMakerChecker);
                context.Entry(customerAccountMakerChecker).State = EntityState.Added;
                customerAccount.CustomerAccountMakerCheckers.Add(customerAccountMakerChecker);

                context.CustomerAccounts.Attach(customerAccount);
                context.Entry(customerAccount).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(CustomerAccountViewModel _customerAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountViewModel.EntryDateTime = DateTime.Now;
                _customerAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _customerAccountViewModel.Remark = "None";

                _customerAccountViewModel.UserAction = StringLiteralValue.Verify;

                CustomerAccountMakerChecker customerAccountMakerChecker = Mapper.Map<CustomerAccountMakerChecker>(_customerAccountViewModel);

                //CustomerAccountMakerChecker
                context.CustomerAccountMakerCheckers.Attach(customerAccountMakerChecker);
                context.Entry(customerAccountMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }


        public async Task<DepositCustomerAccountViewModel> GetRejectedEntry(Guid _customerAccountId)
        {
            DepositCustomerAccountViewModel CustomerAccountViewModel = new DepositCustomerAccountViewModel();
            try
            {
                CustomerAccountViewModel = await context.Database.SqlQuery<DepositCustomerAccountViewModel>("SELECT * FROM dbo.GetCustomerAccountEntry (@CustomerAccountId, @EntriesType)", new SqlParameter("@CustomerAccountId", _customerAccountId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
            return CustomerAccountViewModel;
        }

        public async Task<DepositCustomerAccountViewModel> GetUnVerifiedEntry(Guid _customerAccountId)
        {
            DepositCustomerAccountViewModel CustomerAccountViewModel = new DepositCustomerAccountViewModel();

            try
            {
                CustomerAccountViewModel = await context.Database.SqlQuery<DepositCustomerAccountViewModel>("SELECT * FROM dbo.GetCustomerAccountEntry (@CustomerAccountId, @EntriesType)", new SqlParameter("@CustomerAccountId", _customerAccountId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
            return CustomerAccountViewModel;
        }

        public async Task<DepositCustomerAccountViewModel> GetVerifiedEntry(Guid _customerAccountId)
        {
            DepositCustomerAccountViewModel CustomerAccountViewModel = new DepositCustomerAccountViewModel();
            try
            {
                CustomerAccountViewModel = await context.Database.SqlQuery<DepositCustomerAccountViewModel>("SELECT * FROM dbo.GetCustomerAccountEntry (@CustomerAccountId, @EntriesType)", new SqlParameter("@CustomerAccountId", _customerAccountId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
            return CustomerAccountViewModel;
        }


        public async Task<IEnumerable<CustomerAccountViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerAccountViewModel>("SELECT * FROM dbo.GetCustomerAccountEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerAccountViewModel>("SELECT * FROM dbo.GetCustomerDepositAccountEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerAccountViewModel>("SELECT * FROM dbo.GetCustomerAccountEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

     
        public List<SelectListItem> CustomerAccountDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var a = (from c in context.CustomerAccounts
                             join cd in context.CustomerAccountDetails on c.PrmKey equals cd.CustomerAccountPrmKey into cad
                             from cd in cad.DefaultIfEmpty()
                             where (c.EntryStatus.Equals(StringLiteralValue.Verify))
                             && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                             && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                             //&& (cd.PersonPrmKey.Equals(personPrmKey))
                             //&& (c.PrmKey.Equals(cd.CustomerAccountPrmKey))

                             || (c.EntryStatus.Equals(StringLiteralValue.Verify))
                             && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                             && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                             //&& (cd.PersonPrmKey.Equals(personPrmKey))
                             //&& (c.PrmKey.Equals(cd.CustomerAccountPrmKey))
                             && (c.IsModified.Equals(false))

                             orderby c.AccountNumber
                             select new SelectListItem
                             {
                                 Value = c.CustomerAccountId.ToString(),
                                 Text = c.AccountNumber.ToString() /*((mf.NameOfCenter.Equals(null)) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()))*/
                             }).ToList();
                    return a;
                }

                var b = (from c in context.CustomerAccounts
                         join cd in context.CustomerAccountDetails on c.PrmKey equals cd.CustomerAccountPrmKey into cad
                         from cd in cad.DefaultIfEmpty()
                         where (c.EntryStatus.Equals(StringLiteralValue.Verify))
                         && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                         && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                         //&& (cd.PersonPrmKey.Equals(personPrmKey))
                         && (c.PrmKey.Equals(cd.CustomerAccountPrmKey))

                         || (c.EntryStatus.Equals(StringLiteralValue.Verify))
                         && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                         && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                         //&& (cd.PersonPrmKey.Equals(personPrmKey))
                         && (c.PrmKey.Equals(cd.CustomerAccountPrmKey))
                         && (c.IsModified.Equals(false))

                         orderby c.AccountNumber
                         select new SelectListItem
                         {
                             Value = c.CustomerAccountId.ToString(),
                             Text = cd.CustomerAccountPrmKey.ToString()
                         }).ToList();
                return b;
            }
        }

        public List<SelectListItem> CustomerWithJointAccountDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var a = (from c in context.CustomerAccounts
                             join cd in context.CustomerAccountDetails on c.PrmKey equals cd.CustomerAccountPrmKey into cad
                             from cd in cad.DefaultIfEmpty()
                             where (c.EntryStatus.Equals(StringLiteralValue.Verify))
                             && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                             && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                             //&& (cd.PersonPrmKey.Equals(personPrmKey))
                             //&& (c.PrmKey.Equals(cd.CustomerAccountPrmKey))

                             || (c.EntryStatus.Equals(StringLiteralValue.Verify))
                             && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                             && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                             //&& (cd.PersonPrmKey.Equals(personPrmKey))
                             //&& (c.PrmKey.Equals(cd.CustomerAccountPrmKey))
                             && (c.IsModified.Equals(false))

                             orderby c.AccountNumber
                             select new SelectListItem
                             {
                                 Value = c.CustomerAccountId.ToString(),
                                 Text = c.AccountNumber.ToString() /*((mf.NameOfCenter.Equals(null)) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()))*/
                             }).ToList();
                    return a;
                }

                var b = (from c in context.CustomerAccounts
                         join cd in context.CustomerAccountDetails on c.PrmKey equals cd.CustomerAccountPrmKey into cad
                         from cd in cad.DefaultIfEmpty()
                         where (c.EntryStatus.Equals(StringLiteralValue.Verify))
                         && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                         && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                         //&& (cd.PersonPrmKey.Equals(personPrmKey))
                         && (c.PrmKey.Equals(cd.CustomerAccountPrmKey))

                         || (c.EntryStatus.Equals(StringLiteralValue.Verify))
                         && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                         && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                         //&& (cd.PersonPrmKey.Equals(personPrmKey))
                         && (c.PrmKey.Equals(cd.CustomerAccountPrmKey))
                         && (c.IsModified.Equals(false))

                         orderby c.AccountNumber
                         select new SelectListItem
                         {
                             Value = c.CustomerAccountId.ToString(),
                             Text = cd.CustomerAccountPrmKey.ToString()
                         }).ToList();
                return b;
            }
        }

        public List<SelectListItem> GetCustomerAccountDropdownList(short _generalLedgerPrmKey, long _personPrmKey)
        {
            return (from c in context.CustomerAccounts
                    join d in context.CustomerAccountDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals d.CustomerAccountPrmKey into cd
                    from d in cd.DefaultIfEmpty()
                    where (c.ActivationStatus == StringLiteralValue.Active && c.EntryStatus == StringLiteralValue.Verify)
                    && (d.PersonPrmKey == _personPrmKey && d.GeneralLedgerPrmKey == _generalLedgerPrmKey)
                    orderby c.AccountNumber
                    select new SelectListItem
                    {
                        Value = c.CustomerAccountId.ToString(),
                        Text = c.AccountNumber.ToString() /*((mf.NameOfCenter.Equals(null)) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()))*/
                    }).ToList();
        } 

        public List<SelectListItem> GetCustomerJointAccountDropdownList(short _generalLedgerPrmKey, long _personPrmKey)
        {
            return (from c in context.CustomerAccounts
                    join d in context.CustomerAccountDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals d.CustomerAccountPrmKey into cd
                    from d in cd.DefaultIfEmpty()
                    join j in context.CustomerJointAccountHolders.Where(j => j.ActivationStatus == StringLiteralValue.Active && j.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals j.CustomerAccountPrmKey into cj
                    from j in cj.DefaultIfEmpty()
                    where (c.ActivationStatus == StringLiteralValue.Active && c.EntryStatus == StringLiteralValue.Verify)
                    && (j.PersonPrmKey == _personPrmKey && d.GeneralLedgerPrmKey == _generalLedgerPrmKey)
                    orderby c.AccountNumber
                    select new SelectListItem

                    {
                        Value = c.CustomerAccountId.ToString(),
                        Text = c.AccountNumber.ToString() /*((mf.NameOfCenter.Equals(null)) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()))*/
                    }).ToList();
        }

        public List<SelectListItem> GetCustomerWithJointAccountDropdownList(Guid _generalLedgerId, Guid _personId)
        {
            short _generalLedgerPrmKey = generalLedgerRepository.GetPrmKeyById(_generalLedgerId);
            long _personPrmKey = personRepository.GetPrmKeyById(_personId);

            List<SelectListItem> selectCustomerListItems = new List<SelectListItem>();
            List<SelectListItem> selectJointCustomerListItems = new List<SelectListItem>();

            selectCustomerListItems = GetCustomerAccountDropdownList(_generalLedgerPrmKey, _personPrmKey);
            selectJointCustomerListItems = GetCustomerJointAccountDropdownList(_generalLedgerPrmKey, _personPrmKey);

            return selectCustomerListItems.ToList().Union(selectJointCustomerListItems.ToList())
                    .OrderBy(c => c.Value).ToList();
        }

       
    }
}
