using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Concrete.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.Services.ViewModel.Custom;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Transaction
{
    public class EFTransactionDetailRepository : ITransactionDetailRepository
    {
        private readonly EFDbContext context;

        public EFTransactionDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }
        public bool EnableCashDenomination()
        {
            return context.TransactionParameters
                    .Where(t => t.EntryStatus == StringLiteralValue.Verify)
                    .Select(t => t.EnableCashDenomination).FirstOrDefault();
        }
        public bool IsTransactionExists(long _customerAccountPrmKey)
        {

            long prmKey = (from a in context.TransactionCustomerAccounts.Where(a => a.EntryStatus == StringLiteralValue.Verify && a.IsCredit == true)
                           select (a.PrmKey)).FirstOrDefault();

            if (prmKey > 0)
                return true;

            return false;
        }


        public decimal GetCurrentYearSharesWithdrawal(short _generalLedgerPrmKey)
        {
            var financialYearDate = (from f in context.FinancialYears .Where(f => f.IsCurrent == true)
                        select new
                        {
                            startDate = f.StartDate,
                            endDate = f.EndDate
                        }).FirstOrDefault();

            var tmp = (from t in context.TrialBalanceSheets
                       where (t.GeneralLedgerPrmKey == _generalLedgerPrmKey && t.EffectiveDate > financialYearDate.startDate && t.EffectiveDate < financialYearDate.endDate)
                       group t by (t.GeneralLedgerPrmKey) into g
                       select new
                       {
                           TotalDebitAmount = g.Sum(s => s.TotalDebitAmount)
                       }).FirstOrDefault();

            return (short)tmp.TotalDebitAmount;
        }

        
        public Guid GetBusinessOfficeIdByPrmKey(short _userHomeBranchPrmKey)
        {
            return context.BusinessOffices
                    .Where(b => b.PrmKey == _userHomeBranchPrmKey && b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active)
                    .Select(u => u.BusinessOfficeId).FirstOrDefault();
        }

        public string GetTransactionType(short _schemePrmKey)
        {
            byte schemeTypePrmkey = (from s in context.Schemes
                                     where (s.PrmKey == _schemePrmKey)
                                     && (s.ActivationStatus == StringLiteralValue.Active)
                                     && (s.EntryStatus == StringLiteralValue.Verify)
                                     select s.SchemeTypePrmKey).FirstOrDefault();

            // SharesCapital
            if (schemeTypePrmkey == 1)
                return StringLiteralValue.SharesCapital;
            
            // Deposit
            if (schemeTypePrmkey == 3)
                return (from s in context.SchemeDepositAccountParameters
                                      where (s.SchemePrmKey == _schemePrmKey)
                                      && (s.EntryStatus == StringLiteralValue.Verify)
                                      select s.DepositType).FirstOrDefault();


            return "None";

        }

        public async Task<TransactionSettingViewModel> GetSharesCapitalTransactionSettingValues(DateTime _transactionDate, Guid _customerAccountId, bool _isCreditTransaction)
        {
            try
            {
                var a= await context.Database.SqlQuery<TransactionSettingViewModel>("SELECT * FROM dbo.GetSharesCapitalCreditTransactionSetting (@TransactionDate, @CustomerAccountId,@IsCreditTransaction)", new SqlParameter("@TransactionDate", _transactionDate), new SqlParameter("@CustomerAccountId", _customerAccountId), new SqlParameter("@IsCreditTransaction", _isCreditTransaction)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            } 
        }


        public async Task<TransactionTypeSettingViewModel> GetTransactionTypeSetting(Guid _transactionTypeId)
        {
            try
            {
                // Get UserProfilePrmKey
                short userProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                var a = await context.Database.SqlQuery<TransactionTypeSettingViewModel>("SELECT * FROM dbo.GetTransactionTypeSetting (@UserProfilePrmKey,@TransactionTypeId)", new SqlParameter("@UserProfilePrmKey", userProfilePrmKey), new SqlParameter("@TransactionTypeId", _transactionTypeId)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<TransactionCustomerAccountViewModel>> GetTransactionCustomerAccountEntries(long _transactionMasterPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<TransactionCustomerAccountViewModel>("SELECT * FROM dbo.GetTransactionCustomerAccountEntriesByTransactionMasterPrmKey (@TransactionMasterPrmKey, @EntryType)", new SqlParameter("@TransactionMasterPrmKey", _transactionMasterPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<TransactionGeneralLedgerViewModel>> GetTransactionGeneralLedgerEntries(long _transactionMasterPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<TransactionGeneralLedgerViewModel>("SELECT * FROM dbo.GetTransactionGeneralLedgerEntriesByTransactionMasterPrmKey (@TransactionMasterPrmKey, @EntryType)", new SqlParameter("@TransactionMasterPrmKey", _transactionMasterPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<TransactionCustomerAccountOtherSubscriptionViewModel>> GetTransactionCustomerAccountOtherSubscriptionEntries(long _transactionCustomerAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<TransactionCustomerAccountOtherSubscriptionViewModel>("SELECT * FROM dbo.GetTransactionCustomerAccountOtherSubscriptionEntriesByTransactionCustomerAccountPrmKey (@TransactionCustomerAccountPrmKey, @EntryType)", new SqlParameter("@TransactionCustomerAccountPrmKey", _transactionCustomerAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SharesCessationTransactionViewModel>> GetSharesCessationTransactionEntries(long _transactionCustomerAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SharesCessationTransactionViewModel>("SELECT * FROM dbo.GetSharesCessationTransactionEntriesByTransactionCustomerAccountPrmKey (@TransactionCustomerAccountPrmKey, @EntryType)", new SqlParameter("@TransactionCustomerAccountPrmKey", _transactionCustomerAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SharesCapitalTransactionViewModel>> GetSharesTransactionEntries(long _transactionCustomerAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SharesCapitalTransactionViewModel>("SELECT * FROM dbo.GetSharesCapitalTransactionEntriesByTransactionCustomerAccountPrmKey (@TransactionCustomerAccountPrmKey, @EntryType)", new SqlParameter("@TransactionCustomerAccountPrmKey", _transactionCustomerAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<TransactionCashDenominationViewModel>> GetTransactionCashDenominationEntries(long _transactionMasterPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<TransactionCashDenominationViewModel>("SELECT * FROM dbo.GetTransactionCashDenominationEntriesByTransactionMasterPrmKey (@TransactionMasterPrmKey, @EntryType)", new SqlParameter("@TransactionMasterPrmKey", _transactionMasterPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public List<SelectListItem> DenominationDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.Denominations

                            where (d.ActivationStatus == StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = d.DenominationId.ToString(),
                                Text = d.Title
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.Denominations
                        where (d.EntryStatus == (StringLiteralValue.Verify))
                                   && (d.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = d.DenominationId.ToString(),
                            Text = d.Title
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> GetGeneralLedgerDropdownListForTransaction(Guid _personId, Guid _businessOfficeId)
        {
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            short userProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

            IEnumerable<DbQueryDropdownListViewModel> dropdownListViewModel = context.Database.SqlQuery<DbQueryDropdownListViewModel>("SELECT * FROM dbo.GetDropdownListOfGeneralLedgerForTransaction ( @PersonId, @BusinessOfficeId, @RegionalLanguagePrmKey,@UserProfilePrmKey)", new SqlParameter("@PersonId", _personId), new SqlParameter("@BusinessOfficeId", _businessOfficeId), new SqlParameter("@RegionalLanguagePrmKey", regionalLanguagePrmKey), new SqlParameter("@UserProfilePrmKey", userProfilePrmKey)).Distinct().ToList();

            // Map the results to SelectListItem
            var selectListItems = dropdownListViewModel.Select(p => new SelectListItem
            {
                Value = p.ValueId.ToString(),
                Text = p.ValueText
            }).ToList();

            return selectListItems;

        }
        public List<SelectListItem> GetPersonDropdownListForTransaction(Guid _businessOfficeId)
        {
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            IEnumerable<DbQueryDropdownListViewModel> dropdownListViewModel = context.Database.SqlQuery<DbQueryDropdownListViewModel>("SELECT * FROM dbo.GetDropdownListOfPersonForTransaction (@BusinessOfficeId, @RegionalLanguagePrmKey)", new SqlParameter("@BusinessOfficeId", _businessOfficeId), new SqlParameter("@RegionalLanguagePrmKey", regionalLanguagePrmKey)).Distinct().ToList();

            // Map the results to SelectListItem
            var selectListItems = dropdownListViewModel.Select(p => new SelectListItem
            {
                Value = p.ValueId.ToString(),
                Text = p.ValueText
            }).ToList();

            return selectListItems;
        }
        public List<SelectListItem> GetAccountNumberDropDownListForTransaction(Guid _personId, Guid _businessOfficeId, Guid _generalLedgerId)
        {
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            IEnumerable<DbQueryDropdownListViewModel> dropdownListViewModel = context.Database.SqlQuery<DbQueryDropdownListViewModel>("SELECT * FROM dbo.GetDropdownListOfAccountNumberForTransaction (@BusinessOfficeId,@GeneralLedgerId, @PersonId,  @RegionalLanguagePrmKey)",  new SqlParameter("@BusinessOfficeId", _businessOfficeId), new SqlParameter("@GeneralLedgerId", _generalLedgerId), new SqlParameter("@PersonId", _personId), new SqlParameter("@RegionalLanguagePrmKey", regionalLanguagePrmKey)).Distinct().ToList();
                                                                                                                                                                                                                                                                   
            // Map the results to SelectListItem
            var selectListItems = dropdownListViewModel.Select(p => new SelectListItem
            {
                Value = p.ValueId.ToString(),
                Text = p.ValueText
            }).ToList();

            return selectListItems;
        }
        public List<SelectListItem> GetBusinessOfficeDropDownListForTransaction()
        {
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            short userProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

            IEnumerable<DbQueryDropdownListViewModel> dropdownListViewModel = context.Database.SqlQuery<DbQueryDropdownListViewModel>("SELECT * FROM dbo.GetDropdownListOfBusinessOfficeForTransaction ( @RegionalLanguagePrmKey,@UserProfilePrmKey)", new SqlParameter("@RegionalLanguagePrmKey", regionalLanguagePrmKey), new SqlParameter("@UserProfilePrmKey", userProfilePrmKey)).Distinct().ToList();

            // Map the results to SelectListItem
            var selectListItems = dropdownListViewModel.Select(p => new SelectListItem
            {
                Value = p.ValueId.ToString(),
                Text = p.ValueText
            }).ToList();

            return selectListItems;
        }
         public List<SelectListItem> GetTransactionTypeDropDownListForTransaction()
        {
            short regionalLanguagePrmKey =(short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            short userProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

            IEnumerable<DbQueryDropdownListViewModel> dropdownListViewModel = context.Database.SqlQuery<DbQueryDropdownListViewModel>("SELECT * FROM dbo.GetDropdownListOfTransactionTypeForTransaction ( @RegionalLanguagePrmKey,@UserProfilePrmKey)",new SqlParameter("@RegionalLanguagePrmKey", regionalLanguagePrmKey),new SqlParameter("@UserProfilePrmKey", userProfilePrmKey)).Distinct().ToList();

            // Map the results to SelectListItem
            var selectListItems = dropdownListViewModel.Select(p => new SelectListItem
            {
                Value = p.ValueId.ToString(),
                Text = p.ValueText
            }).ToList();

            return selectListItems;
        }

    }
}
