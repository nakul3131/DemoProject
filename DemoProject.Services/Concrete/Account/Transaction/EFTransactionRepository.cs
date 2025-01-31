using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DemoProject.Domain.Entities.Account.Transaction;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Concrete.Account.Customer;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Transaction
{
    public class EFTransactionRepository : ITransactionRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ICustomerAccountRepository customerAccountRepository;
        private readonly ITransactionGeneralLedgerRepository transactionGeneralLedgerRepository;
        private readonly ITransactionCashDenominationRepository transactionCashDenominationRepository;
        private readonly ITransactionDbContextRepository transactionDbContextRepository;
        private readonly ITransactionDetailRepository transactionDetailRepository;


        public EFTransactionRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, ICustomerAccountRepository _customerAccountRepository, 
                                       ITransactionGeneralLedgerRepository _transactionGeneralLedgerRepository, ITransactionCashDenominationRepository _transactionCashDenominationRepository, ITransactionDbContextRepository _transactionDbContextRepository, ITransactionDetailRepository _transactionDetailRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            customerAccountRepository = _customerAccountRepository;
            transactionGeneralLedgerRepository = _transactionGeneralLedgerRepository;
            transactionCashDenominationRepository = _transactionCashDenominationRepository;
            transactionDbContextRepository = _transactionDbContextRepository;
            transactionDetailRepository = _transactionDetailRepository;
        }

        public List<SelectListItem> GetGLAndAccountNumber(Guid _PersonId)
        {
            long personPrmKey = customerAccountRepository.GetPrmKeyById(_PersonId);

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                var a = (from c in context.CustomerAccounts
                         join cd in context.CustomerAccountDetails on c.PrmKey equals cd.CustomerAccountPrmKey into cad
                         from cd in cad.DefaultIfEmpty()

                         join cd1 in context.GeneralLedgers on cd.GeneralLedgerPrmKey equals cd1.PrmKey into cad1
                         from cd1 in cad1.DefaultIfEmpty()

                         where (c.EntryStatus.Equals(StringLiteralValue.Verify))
                         && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                         && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                         && (cd.PersonPrmKey.Equals(personPrmKey))
                         && (c.PrmKey.Equals(cd.CustomerAccountPrmKey))

                         || (c.EntryStatus.Equals(StringLiteralValue.Verify))
                         && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                         && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                         && (cd.PersonPrmKey.Equals(personPrmKey))
                         && (c.PrmKey.Equals(cd.CustomerAccountPrmKey))
                         && (c.IsModified.Equals(false))

                         orderby c.AccountNumber
                         select new SelectListItem
                         {
                             //Value = cd.CustomerAccountDetailId.ToString(),
                             Text = cd1.NameOfGL.ToString() /*((mf.NameOfCenter.Equals(null)) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()))*/
                         }).ToList();
                return a;
            }

            var b = (from c in context.CustomerAccounts
                     join cd in context.CustomerAccountDetails on c.PrmKey equals cd.CustomerAccountPrmKey into cad
                     from cd in cad.DefaultIfEmpty()
                     where (c.EntryStatus.Equals(StringLiteralValue.Verify))
                     && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                     && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                     && (cd.PersonPrmKey.Equals(personPrmKey))

                     || (c.EntryStatus.Equals(StringLiteralValue.Verify))
                     && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                     && (cd.EntryStatus.Equals(StringLiteralValue.Verify) || cd.EntryStatus.Equals(null))
                     && (cd.PersonPrmKey.Equals(personPrmKey))
                     && (c.IsModified.Equals(false))

                     orderby c.AccountNumber
                     select new SelectListItem
                     {
                         Value = c.CustomerAccountId.ToString(),
                         Text = c.AccountNumber.ToString()
                     }).ToList();
            return b;
        }

       
        public async Task<bool> Amend(TransactionViewModel _transactionViewModel)
        {
            try
            {
                // Set Default Value
                _transactionViewModel.EntryDateTime = DateTime.Now;
                _transactionViewModel.EntryStatus = StringLiteralValue.Amend;
                _transactionViewModel.UserAction = StringLiteralValue.Amend;
                _transactionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _transactionViewModel.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(_transactionViewModel.TransactionTypeId);
                //_transactionViewModel.PeriodCodePrmKey = periodCodeRepository.GetPrmKeyById(_transactionViewModel.PeriodCodeId);

                // Mapping 
                // TransactionParameter
                TransactionMaster transactionMaster = Mapper.Map<TransactionMaster>(_transactionViewModel);
                TransactionMasterMakerChecker transactionMasterMakerChecker = Mapper.Map<TransactionMasterMakerChecker>(_transactionViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                //addressParameter.PrmKey = _transactionViewModel.TransactionMasterPrmKey; 

                // TransactionCustomerAccount - Amend Old Record
                //IEnumerable<TransactionCustomerAccountViewModel> transactionCustomerAccountViewModelForAmend = await transactionCustomerAccountRepository.GetRejectedEntries(_transactionViewModel.TransactionMasterPrmKey);

                //foreach (TransactionCustomerAccountViewModel viewModel in transactionCustomerAccountViewModelForAmend)
                //{
                //    // Set Default Value
                //    viewModel.PrmKey = 0;
                //    viewModel.EntryDateTime = DateTime.Now;
                //    viewModel.UserAction = StringLiteralValue.Amend;
                //    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //    // Mapping - TransactionCustomerAccountMakerChecker
                //    TransactionCustomerAccountMakerChecker transactionCustomerAccountMakerCheckerForAmend = Mapper.Map<TransactionCustomerAccountMakerChecker>(viewModel);

                //    context.TransactionCustomerAccountMakerCheckers.Attach(transactionCustomerAccountMakerCheckerForAmend);
                //    context.Entry(transactionCustomerAccountMakerCheckerForAmend).State = EntityState.Added;
                //}

                // TransactionCustomerAccount - Add New Amended Entry, Get TransactionCustomerAccount Details From Session Object
                List<TransactionCustomerAccountViewModel> transactionCustomerAccountViewModelList = new List<TransactionCustomerAccountViewModel>();

                transactionCustomerAccountViewModelList = (List<TransactionCustomerAccountViewModel>)HttpContext.Current.Session["TransactionCustomerAccount"];

                foreach (TransactionCustomerAccountViewModel viewModel in transactionCustomerAccountViewModelList)
                {
                    // Set Default Value
                    viewModel.PrmKey = 0;
                    viewModel.TransactionCustomerAccountPrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Remark = _transactionViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    //viewModel.BusinessOfficePrmKey = _transactionViewModel.BusinessOfficePrmKey;

                    // Get PrmKey By Id Of All Dropdowns
                    //viewModel.BusinessOfficePrmKey = businessOfficeRepository.GetPrmKeyById(viewModel.BusinessOfficeId);
                    // viewModel.CustomerAccountPrmKey = generalLedgerRepository.GetPrmKeyById(viewModel.TransactionCustomerAccountId);

                    // Mapping - TransactionCustomerAccount
                    TransactionCustomerAccount transactionCustomerAccount = Mapper.Map<TransactionCustomerAccount>(viewModel);
                    TransactionCustomerAccountMakerChecker transactionCustomerAccountMakerChecker = Mapper.Map<TransactionCustomerAccountMakerChecker>(viewModel);

                    context.TransactionCustomerAccountMakerCheckers.Attach(transactionCustomerAccountMakerChecker);
                    context.Entry(transactionCustomerAccountMakerChecker).State = EntityState.Added;
                    transactionCustomerAccount.TransactionCustomerAccountMakerCheckers.Add(transactionCustomerAccountMakerChecker);

                    context.TransactionCustomerAccounts.Attach(transactionCustomerAccount);
                    context.Entry(transactionCustomerAccount).State = EntityState.Added;
                }

                // TransactionGeneralLedger - Amend Old Record
                IEnumerable<TransactionGeneralLedgerViewModel> transactionGeneralLedgerViewModelForAmend = await transactionGeneralLedgerRepository.GetRejectedEntries(_transactionViewModel.TransactionMasterPrmKey);

                foreach (TransactionGeneralLedgerViewModel viewModel in transactionGeneralLedgerViewModelForAmend)
                {
                    // Set Default Value
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // TransactionGeneralLedgerMakerChecker
                    TransactionGeneralLedgerMakerChecker transactionGeneralLedgerForAmend = Mapper.Map<TransactionGeneralLedgerMakerChecker>(viewModel);

                    context.TransactionGeneralLedgerMakerCheckers.Attach(transactionGeneralLedgerForAmend);
                    context.Entry(transactionGeneralLedgerForAmend).State = EntityState.Added;
                }

                // TransactionGeneralLedger - Add New Amended Entry, Get TransactionGeneralLedger Details From Session Object
                List<TransactionGeneralLedgerViewModel> transactionGeneralLedgerViewModelList = new List<TransactionGeneralLedgerViewModel>();

                transactionGeneralLedgerViewModelList = (List<TransactionGeneralLedgerViewModel>)HttpContext.Current.Session["TransactionGeneralLedger"];

                foreach (TransactionGeneralLedgerViewModel viewModel in transactionGeneralLedgerViewModelList)
                {
                    // Set Default Value
                    viewModel.PrmKey = 0;
                    viewModel.TransactionGeneralLedgerPrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Remark = _transactionViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    //viewModel.BusinessOfficePrmKey = _businessOfficeViewModel.BusinessOfficePrmKey;

                    // Get PrmKey By Id Of All Dropdowns
                    //viewModel.BusinessOfficePrmKey = businessOfficeRepository.GetPrmKeyById(viewModel.BusinessOfficeId);
                    //viewModel.GeneralLedgerPrmKey = generalLedgerRepository.GetPrmKeyById(viewModel.GeneralLedgerId);

                    // TransactionGeneralLedger
                    TransactionGeneralLedger transactionGeneralLedger = Mapper.Map<TransactionGeneralLedger>(viewModel);
                    TransactionGeneralLedgerMakerChecker transactionGeneralLedgerMakerChecker = Mapper.Map<TransactionGeneralLedgerMakerChecker>(viewModel);

                    context.TransactionGeneralLedgerMakerCheckers.Attach(transactionGeneralLedgerMakerChecker);
                    context.Entry(transactionGeneralLedgerMakerChecker).State = EntityState.Added;
                    transactionGeneralLedger.TransactionGeneralLedgerMakerCheckers.Add(transactionGeneralLedgerMakerChecker);

                    context.TransactionGeneralLedgers.Attach(transactionGeneralLedger);
                    context.Entry(transactionGeneralLedger).State = EntityState.Added;
                }

                // TransactionCashDenomination - Amend Old Record
                IEnumerable<TransactionCashDenominationViewModel> transactionCashDenominationViewModelForAmend = await transactionCashDenominationRepository.GetRejectedEntries(_transactionViewModel.TransactionMasterPrmKey);

                foreach (TransactionCashDenominationViewModel viewModel in transactionCashDenominationViewModelForAmend)
                {
                    // Set Default Value 
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    TransactionCashDenominationMakerChecker transactionCashDenominationMakerCheckerForAmend = Mapper.Map<TransactionCashDenominationMakerChecker>(viewModel);

                    context.TransactionCashDenominationMakerCheckers.Attach(transactionCashDenominationMakerCheckerForAmend);
                    context.Entry(transactionCashDenominationMakerCheckerForAmend).State = EntityState.Added;
                }

                // TransactionCashDenomination - Add New Amended Entry, Get TransactionCashDenomination Details From Session Object
                List<TransactionCashDenominationViewModel> transactionCashDenominationViewModelList = new List<TransactionCashDenominationViewModel>();

                transactionCashDenominationViewModelList = (List<TransactionCashDenominationViewModel>)HttpContext.Current.Session["TransactionCashDenomination"];

                foreach (TransactionCashDenominationViewModel viewModel in transactionCashDenominationViewModelList)
                {
                    // Set Default Value
                    viewModel.PrmKey = 0;
                    viewModel.TransactionCashDenominationPrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Remark = _transactionViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    //viewModel.BusinessOfficePrmKey = _businessOfficeViewModel.BusinessOfficePrmKey;

                    // Get PrmKey By Id Of All Dropdowns
                    //viewModel.DenominationPrmKey = accountDetailRepository.GetPrmKeyById(viewModel.DenominationId);

                    // TransactionCashDenomination
                    TransactionCashDenomination transactionCashDenomination = Mapper.Map<TransactionCashDenomination>(viewModel);
                    TransactionCashDenominationMakerChecker transactionCashDenominationMakerChecker = Mapper.Map<TransactionCashDenominationMakerChecker>(viewModel);

                    context.TransactionCashDenominationMakerCheckers.Attach(transactionCashDenominationMakerChecker);
                    context.Entry(transactionCashDenominationMakerChecker).State = EntityState.Added;
                    transactionCashDenomination.TransactionCashDenominationMakerCheckers.Add(transactionCashDenominationMakerChecker);

                    context.TransactionCashDenominations.Attach(transactionCashDenomination);
                    context.Entry(transactionCashDenomination).State = EntityState.Added;
                }

                // TransactionMaster
                context.TransactionMasterMakerCheckers.Attach(transactionMasterMakerChecker);
                context.Entry(transactionMasterMakerChecker).State = EntityState.Added;
                transactionMaster.TransactionMasterMakerCheckers.Add(transactionMasterMakerChecker);

                context.TransactionMasters.Attach(transactionMaster);
                context.Entry(transactionMaster).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<TransactionViewModel> GetActiveEntry()
        {
            try
            {
                return await context.Database.SqlQuery<TransactionViewModel>("SELECT * FROM dbo.GetTransactionMasterEntry(@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<bool> GetSessionValues(TransactionViewModel _transactionViewModel, string _entryType)
        {
            try
            {

                HttpContext.Current.Session["CustomerAccount"] = await transactionDetailRepository.GetTransactionCustomerAccountEntries(_transactionViewModel.PrmKey, _entryType);

                // List<TransactionCustomerAccountViewModel> transactionCustomerAccountViewModelList = (List<TransactionCustomerAccountViewModel>)HttpContext.Current.Session["CustomerAccount"];
               
                //confirm how to get transactionCustomerAccountPrmKey
                long transactionCustomerAccountPrmKey = 0;

                HttpContext.Current.Session["GeneralLedger"] = await transactionDetailRepository.GetTransactionGeneralLedgerEntries(_transactionViewModel.PrmKey, _entryType);
                
                HttpContext.Current.Session["CustomerAccountOtherSubscription"] = await transactionDetailRepository.GetTransactionCustomerAccountOtherSubscriptionEntries(transactionCustomerAccountPrmKey, _entryType);
                
                HttpContext.Current.Session["SharesCapital"] = await transactionDetailRepository.GetSharesTransactionEntries(transactionCustomerAccountPrmKey, _entryType);
                
                HttpContext.Current.Session["SharesCessation"] = await transactionDetailRepository.GetSharesCessationTransactionEntries(transactionCustomerAccountPrmKey, _entryType);
               
                HttpContext.Current.Session["CashDenomination"] = await transactionDetailRepository.GetTransactionCashDenominationEntries(transactionCustomerAccountPrmKey, _entryType);

                
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<TransactionIndexViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<TransactionIndexViewModel>("SELECT * FROM dbo.GetTransactionMasterEntries(@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<TransactionViewModel>> GetTransactionParameterIndex()
        {
            try
            {
                return await context.Database.SqlQuery<TransactionViewModel>("SELECT * FROM dbo.GetTransactionMasterEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<TransactionViewModel> GetRejectedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<TransactionViewModel>("SELECT * FROM dbo.GetTransactionMasterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<TransactionViewModel> GetUnVerifiedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<TransactionViewModel>("SELECT * FROM dbo.GetTransactionMasterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Save(TransactionViewModel _transactionViewModel)
        {
            try
            {
                bool result = true;

                //TransactionMaster
                if (result)
                {
                    result = transactionDbContextRepository.AttachTransactionData(_transactionViewModel, StringLiteralValue.Create);
                }

                //TransactionCustomerAccount
                if (result)
                {
                    List<TransactionCustomerAccountViewModel> transactionCustomerAccountViewModelList = (List<TransactionCustomerAccountViewModel>)HttpContext.Current.Session["CustomerAccount"];

                    if (transactionCustomerAccountViewModelList != null)
                    {
                        foreach (TransactionCustomerAccountViewModel viewModel in transactionCustomerAccountViewModelList)
                        {
                            result = transactionDbContextRepository.AttachTransactionCustomerAccountData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //if (result)
                //{
                //    List<SharesCessationTransactionViewModel> sharesCessationTransactionViewModels = (List<SharesCessationTransactionViewModel>)HttpContext.Current.Session["SharesCessation"];

                //    if (sharesCessationTransactionViewModels != null)
                //    {
                //        foreach (SharesCessationTransactionViewModel viewModel in sharesCessationTransactionViewModels)
                //        {
                //            result = transactionDbContextRepository.AttachSharesCessationTransactionData(viewModel, StringLiteralValue.Create);
                //        }
                //    }
                //}

                //TransactionGeneralLedger
                if (result)
                {
                    List<TransactionGeneralLedgerViewModel> transactionGeneralLedgerViewModelList = (List<TransactionGeneralLedgerViewModel>)HttpContext.Current.Session["GeneralLedger"];

                    if (transactionGeneralLedgerViewModelList != null)
                    {

                        foreach (TransactionGeneralLedgerViewModel viewModel in transactionGeneralLedgerViewModelList)
                        {
                            result = transactionDbContextRepository.AttachTransactionGeneralLedgerData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //TransactionGSTDetail
                if (result)
                {
                    List<TransactionGSTDetailViewModel> transactionGSTDetailViewModelList = (List<TransactionGSTDetailViewModel>)HttpContext.Current.Session["GSTDetail"];

                    if (transactionGSTDetailViewModelList != null)
                    {

                        foreach (TransactionGSTDetailViewModel viewModel in transactionGSTDetailViewModelList)
                        {
                            result = transactionDbContextRepository.AttachTransactionGSTDetailData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //SharesCapitalTransaction
                if (result)
                {
                    List<SharesCapitalTransactionViewModel> sharesCapitalTransactionViewModels = (List<SharesCapitalTransactionViewModel>)HttpContext.Current.Session["SharesCapital"];

                    if (sharesCapitalTransactionViewModels != null)
                    {
                        foreach (SharesCapitalTransactionViewModel viewModel in sharesCapitalTransactionViewModels)
                        {
                            result = transactionDbContextRepository.AttachSharesTransactionData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //TransactionCashDenomination
                if (result)
                {
                    List<TransactionCashDenominationViewModel> transactionCashDenominationViewModelList = (List<TransactionCashDenominationViewModel>)HttpContext.Current.Session["CashDenomination"];

                    if (transactionCashDenominationViewModelList != null)
                    {
                        foreach (TransactionCashDenominationViewModel viewModel in transactionCashDenominationViewModelList)
                        {
                            result = transactionDbContextRepository.AttachTransactionCashDenominationData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //Final Method
                if (result)
                {
                    result = await transactionDbContextRepository.SaveData();
                }

                //Return Result
                if (result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> VerifyRejectDelete(TransactionViewModel _transactionViewModel, string _entryType)
        {
            string entriesType;

            if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
                entriesType = StringLiteralValue.Unverified;
            else
                entriesType = StringLiteralValue.Reject;
            try
            {
                //LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();

                bool result;

                result = transactionDbContextRepository.AttachTransactionData(_transactionViewModel, _entryType);

                if (result)
                {
                    IEnumerable<TransactionCustomerAccountViewModel> transactionCustomerAccountViewModel = await transactionDetailRepository.GetTransactionCustomerAccountEntries(_transactionViewModel.TransactionMasterPrmKey, entriesType);

                    if (transactionCustomerAccountViewModel != null)
                    {
                        foreach (TransactionCustomerAccountViewModel viewModel in transactionCustomerAccountViewModel)
                        {
                            result = transactionDbContextRepository.AttachTransactionCustomerAccountData(_transactionViewModel.TransactionCustomerAccountViewModel, _entryType);

                        }
                    }
                }

                if (result)
                {
                    IEnumerable<TransactionGeneralLedgerViewModel> transactionCustomerAccountViewModel = await transactionDetailRepository.GetTransactionGeneralLedgerEntries(_transactionViewModel.TransactionMasterPrmKey, entriesType);

                    if (transactionCustomerAccountViewModel != null)
                    {
                        foreach (TransactionGeneralLedgerViewModel viewModel in transactionCustomerAccountViewModel)
                        {
                            result = transactionDbContextRepository.AttachTransactionGeneralLedgerData(_transactionViewModel.TransactionGeneralLedgerViewModel, _entryType);

                        }
                    }
                }

                if (result)
                {
                    IEnumerable<SharesCessationTransactionViewModel> sharesCessationTransactionViewModel = await transactionDetailRepository.GetSharesCessationTransactionEntries(_transactionViewModel.SharesCessationTransactionViewModel.TransactionCustomerAccountPrmKey, entriesType);

                    if (sharesCessationTransactionViewModel != null)
                    {
                        foreach (SharesCessationTransactionViewModel viewModel in sharesCessationTransactionViewModel)
                        {
                            result = transactionDbContextRepository.AttachSharesCessationTransactionData(_transactionViewModel.SharesCessationTransactionViewModel, _entryType);

                        }
                    }
                }

                if (result)
                {
                    IEnumerable<SharesCapitalTransactionViewModel> sharesTransactionViewModel = await transactionDetailRepository.GetSharesTransactionEntries(_transactionViewModel.SharesCapitalTransactionViewModel.TransactionCustomerAccountPrmKey, entriesType);

                    if (sharesTransactionViewModel != null)
                    {
                        foreach (SharesCapitalTransactionViewModel viewModel in sharesTransactionViewModel)
                        {
                            result = transactionDbContextRepository.AttachSharesTransactionData(_transactionViewModel.SharesCapitalTransactionViewModel, _entryType);

                        }
                    }
                }

                if (result)
                {
                    IEnumerable<TransactionCashDenominationViewModel> transactionCashDenominationViewModel = await transactionDetailRepository.GetTransactionCashDenominationEntries(_transactionViewModel.TransactionMasterPrmKey, entriesType);

                    if (transactionCashDenominationViewModel != null)
                    {
                        foreach (TransactionCashDenominationViewModel viewModel in transactionCashDenominationViewModel)
                        {
                            result = transactionDbContextRepository.AttachTransactionCashDenominationData(_transactionViewModel.TransactionCashDenominationViewModel, _entryType);

                        }
                    }
                }

                if (result)
                    result = await transactionDbContextRepository.SaveData();

                if (result)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

    }
}
