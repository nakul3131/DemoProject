using AutoMapper;
using DemoProject.Domain.Entities.Account.Transaction;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Services.Concrete.Account.Transaction
{
    public class EFOpeningBalanceRepository : IOpeningBalanceRepository
    {
        private readonly EFDbContext context;
        private readonly IGeneralLedgerRepository generalLedgerRepository;
        private readonly IAccountDetailRepository accountDetailRepository;

        public EFOpeningBalanceRepository(RepositoryConnection _connection, IGeneralLedgerRepository _generalLedgerRepository, IAccountDetailRepository _accountDetailRepository)
        {
            context = _connection.EFDbContext;
            generalLedgerRepository = _generalLedgerRepository;
            accountDetailRepository = _accountDetailRepository;
        }

        public async Task<bool> GetOpeningBalanceValues(short _generalLedgerId, string _entryType)
        {
            try
            {
                HttpContext.Current.Session["OpeningBalance"] = await GetOpeningBalance(_generalLedgerId, _entryType);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<byte> GetSchemeType(short _generalLedgerId)
        {
            try
            {
                var schemeType = await context.Database.SqlQuery<byte>("SELECT dbo.GetSchemeType(@GeneralLedgerPrmKey)", new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerId)).FirstOrDefaultAsync();
                return schemeType;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return 0;
            }
        }

        public async Task<string> GetDepositType(short _generalLedgerId)
        {
            try
            {
                var depositType = await context.Database.SqlQuery<string>("SELECT dbo.GetDepositType(@GeneralLedgerPrmKey)", new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerId)).FirstOrDefaultAsync();
                return depositType;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<OpeningBalanceViewModel> GetOpeningBalanceValue(short _generalLedgerid, long _personId)
        {
            try
            {
                OpeningBalanceViewModel openingBalance = await context.Database.SqlQuery<OpeningBalanceViewModel>("SELECT * FROM dbo.GetOpeningBalanceModifyEntry(@GeneralLedgerPrmKey, @CustomerAccountPrmKey)", new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerid), new SqlParameter("CustomerAccountPrmKey", _personId)).FirstOrDefaultAsync();
                return openingBalance;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<OpeningBalanceViewModel>> GetOpeningBalance(short _generalLedgerId, string _entryStatus)
        {
            try
            {
                var openingBalance = await context.Database.SqlQuery<OpeningBalanceViewModel>("SELECT * FROM dbo.GetOpeningBalanceEntry(@GeneralLedgerPrmKey, @EntryType)", new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerId), new SqlParameter("EntryType", _entryStatus)).ToListAsync();
                return openingBalance;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Transaction Table Entries Are Modified After Verification, So For Current Operation(i.e.Create / Modify) Not Required Modify Old Entries***
        public async Task<bool> Amend(OpeningBalanceViewModel _openingBalanceViewModel)
        {
            try
            {
                //Get OpeningBalance From Session Object New Added Record / Updated Record
                IEnumerable<OpeningBalanceViewModel> OpeningBalanceViewModelList = (IEnumerable<OpeningBalanceViewModel>)HttpContext.Current.Session["OpeningBalance"];
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;
                foreach (OpeningBalanceViewModel viewModel in OpeningBalanceViewModelList)
                {
                    //Set Default Value
                    GetOpeningBalanceDefaultValues(viewModel, StringLiteralValue.Amend);
                    OpeningBalanceMakerChecker openingBalanceMakerChecker = Mapper.Map<OpeningBalanceMakerChecker>(viewModel);
                    openingBalanceMakerChecker.OpeningBalancePrmKey = viewModel.OpeningBalancePrmKey;
                    byte schemeTypePrmKey = _openingBalanceViewModel.SchemeTypePrmKey;

                    switch (schemeTypePrmKey)
                    {
                        case 1:
                            OpeningBalanceShare openingBalanceShare = Mapper.Map<OpeningBalanceShare>(viewModel);
                            openingBalanceShare.PrmKey = viewModel.OpeningBalanceSharesPrmKey;
                            context.OpeningBalanceShares.Attach(openingBalanceShare);
                            context.Entry(openingBalanceShare).State = EntityState.Modified;
                            break;

                        case 2:
                            OpeningBalanceInvestment openingBalanceInvestment = Mapper.Map<OpeningBalanceInvestment>(viewModel);
                            openingBalanceInvestment.PrmKey = viewModel.OpeningBalanceInvestmentPrmKey;
                            openingBalanceInvestment.LastProvisionDateOfInvestment = DateTime.Now;
                            context.OpeningBalanceInvestments.Attach(openingBalanceInvestment);
                            context.Entry(openingBalanceInvestment).State = EntityState.Modified;
                            break;

                        case 3:
                            OpeningBalanceDeposit openingBalanceDeposit = Mapper.Map<OpeningBalanceDeposit>(viewModel);
                            openingBalanceDeposit.PrmKey = viewModel.OpeningBalanceDepositPrmKey;
                            openingBalanceDeposit.LastProvisionDateOfDeposit = DateTime.Now;
                            context.OpeningBalanceDeposits.Attach(openingBalanceDeposit);
                            context.Entry(openingBalanceDeposit).State = EntityState.Modified;
                            break;

                        case 4:
                            OpeningBalanceLoan openingBalanceLoan = Mapper.Map<OpeningBalanceLoan>(viewModel);
                            openingBalanceLoan.PrmKey = viewModel.OpeningBalanceLoanPrmKey;
                            openingBalanceLoan.LastProvisionDateOfLoan = DateTime.Now;
                            openingBalanceLoan.PreviousInstallmentDateOfLoan = DateTime.Now;
                            openingBalanceLoan.PreviousInterestDateOfLoan = DateTime.Now;
                            context.OpeningBalanceLoans.Attach(openingBalanceLoan);
                            context.Entry(openingBalanceLoan).State = EntityState.Modified;
                            break;
                        default:
                            break;
                    }

                    context.OpeningBalanceMakerCheckers.Attach(openingBalanceMakerChecker);
                    context.Entry(openingBalanceMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Delete(OpeningBalanceViewModel _openingBalanceViewModel)
        {
            try
            {
                //Get OpeningBalance From Session Object New Added Record / Updated Record
                IEnumerable<OpeningBalanceViewModel> OpeningBalanceViewModelList = (IEnumerable<OpeningBalanceViewModel>)HttpContext.Current.Session["OpeningBalance"];
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;
                foreach (OpeningBalanceViewModel viewModel in OpeningBalanceViewModelList)
                {
                    //Set Default Value
                    GetOpeningBalanceDefaultValues(viewModel, StringLiteralValue.Delete);
                    OpeningBalanceMakerChecker openingBalanceMakerChecker = Mapper.Map<OpeningBalanceMakerChecker>(viewModel);
                    openingBalanceMakerChecker.OpeningBalancePrmKey = viewModel.OpeningBalancePrmKey;
                    openingBalanceMakerChecker.Remark = _openingBalanceViewModel.Remark;
                    byte schemeTypePrmKey = viewModel.SchemeTypePrmKey;

                    switch (schemeTypePrmKey)
                    {
                        case 1:
                            OpeningBalanceShare openingBalanceShare = Mapper.Map<OpeningBalanceShare>(viewModel);
                            openingBalanceShare.PrmKey = viewModel.OpeningBalanceSharesPrmKey;
                            context.OpeningBalanceShares.Attach(openingBalanceShare);
                            context.Entry(openingBalanceShare).State = EntityState.Modified;
                            break;

                        case 2:
                            OpeningBalanceInvestment openingBalanceInvestment = Mapper.Map<OpeningBalanceInvestment>(viewModel);
                            openingBalanceInvestment.PrmKey = viewModel.OpeningBalanceInvestmentPrmKey;
                            openingBalanceInvestment.LastProvisionDateOfInvestment = DateTime.Now;
                            context.OpeningBalanceInvestments.Attach(openingBalanceInvestment);
                            context.Entry(openingBalanceInvestment).State = EntityState.Modified;
                            break;

                        case 3:
                            OpeningBalanceDeposit openingBalanceDeposit = Mapper.Map<OpeningBalanceDeposit>(viewModel);
                            openingBalanceDeposit.PrmKey = viewModel.OpeningBalanceDepositPrmKey;
                            openingBalanceDeposit.LastProvisionDateOfDeposit = DateTime.Now;
                            context.OpeningBalanceDeposits.Attach(openingBalanceDeposit);
                            context.Entry(openingBalanceDeposit).State = EntityState.Modified;
                            break;

                        case 4:
                            OpeningBalanceLoan openingBalanceLoan = Mapper.Map<OpeningBalanceLoan>(viewModel);
                            openingBalanceLoan.PrmKey = viewModel.OpeningBalanceLoanPrmKey;
                            openingBalanceLoan.LastProvisionDateOfLoan = DateTime.Now;
                            openingBalanceLoan.PreviousInstallmentDateOfLoan = DateTime.Now;
                            openingBalanceLoan.PreviousInterestDateOfLoan = DateTime.Now;
                            context.OpeningBalanceLoans.Attach(openingBalanceLoan);
                            context.Entry(openingBalanceLoan).State = EntityState.Modified;
                            break;
                        default:
                            break;
                    }

                    context.OpeningBalanceMakerCheckers.Attach(openingBalanceMakerChecker);
                    context.Entry(openingBalanceMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Save(OpeningBalanceViewModel _openingBalanceViewModel)
        {
            try
            {
                //Get OpeningBalance From Session Object New Added Record / Updated Record
                IEnumerable<OpeningBalanceViewModel> OpeningBalanceViewModelList = (IEnumerable<OpeningBalanceViewModel>)HttpContext.Current.Session["OpeningBalance"];
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;
                foreach (OpeningBalanceViewModel viewModel in OpeningBalanceViewModelList)
                {
                    //Set Default Value
                    GetOpeningBalanceDefaultValues(viewModel, StringLiteralValue.Amend);
                    OpeningBalance openingBalance = Mapper.Map<OpeningBalance>(viewModel);
                    openingBalance.PrmKey = viewModel.OpeningBalancePrmKey;
                    OpeningBalanceMakerChecker openingBalanceMakerChecker = Mapper.Map<OpeningBalanceMakerChecker>(viewModel);
                    openingBalanceMakerChecker.OpeningBalancePrmKey = viewModel.OpeningBalancePrmKey;
                    byte schemeTypePrmKey = _openingBalanceViewModel.SchemeTypePrmKey;

                    switch (schemeTypePrmKey)
                    {
                        case 1:
                            OpeningBalanceShare openingBalanceShare = Mapper.Map<OpeningBalanceShare>(viewModel);
                            openingBalanceShare.PrmKey = viewModel.OpeningBalanceSharesPrmKey;
                            context.OpeningBalanceShares.Attach(openingBalanceShare);
                            context.Entry(openingBalanceShare).State = EntityState.Modified;
                            break;

                        case 2:
                            OpeningBalanceInvestment openingBalanceInvestment = Mapper.Map<OpeningBalanceInvestment>(viewModel);
                            openingBalanceInvestment.PrmKey = viewModel.OpeningBalanceInvestmentPrmKey;
                            openingBalanceInvestment.LastProvisionDateOfInvestment = DateTime.Now;
                            context.OpeningBalanceInvestments.Attach(openingBalanceInvestment);
                            context.Entry(openingBalanceInvestment).State = EntityState.Modified;
                            break;

                        case 3:
                            OpeningBalanceDeposit openingBalanceDeposit = Mapper.Map<OpeningBalanceDeposit>(viewModel);
                            openingBalanceDeposit.PrmKey = viewModel.OpeningBalanceDepositPrmKey;
                            openingBalanceDeposit.LastProvisionDateOfDeposit = DateTime.Now;
                            context.OpeningBalanceDeposits.Attach(openingBalanceDeposit);
                            context.Entry(openingBalanceDeposit).State = EntityState.Modified;
                            break;

                        case 4:
                            OpeningBalanceLoan openingBalanceLoan = Mapper.Map<OpeningBalanceLoan>(viewModel);
                            openingBalanceLoan.PrmKey = viewModel.OpeningBalanceLoanPrmKey;
                            openingBalanceLoan.LastProvisionDateOfLoan = DateTime.Now;
                            openingBalanceLoan.PreviousInstallmentDateOfLoan = DateTime.Now;
                            openingBalanceLoan.PreviousInterestDateOfLoan = DateTime.Now;
                            context.OpeningBalanceLoans.Attach(openingBalanceLoan);
                            context.Entry(openingBalanceLoan).State = EntityState.Modified;
                            break;
                        default:
                            break;
                    }

                    context.OpeningBalanceMakerCheckers.Attach(openingBalanceMakerChecker);
                    context.Entry(openingBalanceMakerChecker).State = EntityState.Added;
                    openingBalance.OpeningBalanceMakerCheckers.Add(openingBalanceMakerChecker);

                    context.OpeningBalances.Attach(openingBalance);
                    context.Entry(openingBalance).State = EntityState.Modified;
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

        public async Task<bool> Modify(OpeningBalanceViewModel _openingBalanceViewModel)
        {
            try
            {
                //Get OpeningBalance From Session Object New Added Record / Updated Record
                IEnumerable<OpeningBalanceViewModel> OpeningBalanceViewModelList = (IEnumerable<OpeningBalanceViewModel>)HttpContext.Current.Session["ModifyOpeningBalance"];

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;
                foreach (OpeningBalanceViewModel viewModel in OpeningBalanceViewModelList)
                {
                    //Set Default Value
                    GetOpeningBalanceDefaultValues(viewModel, StringLiteralValue.Amend);

                    OpeningBalance openingBalance = Mapper.Map<OpeningBalance>(viewModel);
                    OpeningBalanceMakerChecker openingBalanceMakerChecker = Mapper.Map<OpeningBalanceMakerChecker>(viewModel);
                    byte schemeTypePrmKey = _openingBalanceViewModel.SchemeTypePrmKey;


                    switch (schemeTypePrmKey)
                    {
                        case 1:
                            OpeningBalanceShare openingBalanceShare = Mapper.Map<OpeningBalanceShare>(viewModel);
                            //openingBalanceShare.PrmKey = viewModel.OpeningBalanceSharesPrmKey;
                            context.OpeningBalanceShares.Attach(openingBalanceShare);
                            context.Entry(openingBalanceShare).State = EntityState.Added;
                            break;

                        case 2:
                            OpeningBalanceInvestment openingBalanceInvestment = Mapper.Map<OpeningBalanceInvestment>(viewModel);
                            openingBalanceInvestment.PrmKey = viewModel.OpeningBalanceInvestmentPrmKey;
                            openingBalanceInvestment.LastProvisionDateOfInvestment = DateTime.Now;
                            context.OpeningBalanceInvestments.Attach(openingBalanceInvestment);
                            context.Entry(openingBalanceInvestment).State = EntityState.Added;
                            break;

                        case 3:
                            OpeningBalanceDeposit openingBalanceDeposit = Mapper.Map<OpeningBalanceDeposit>(viewModel);
                            openingBalanceDeposit.PrmKey = viewModel.OpeningBalanceDepositPrmKey;
                            openingBalanceDeposit.LastProvisionDateOfDeposit = DateTime.Now;
                            context.OpeningBalanceDeposits.Attach(openingBalanceDeposit);
                            context.Entry(openingBalanceDeposit).State = EntityState.Added;
                            break;

                        case 4:
                            OpeningBalanceLoan openingBalanceLoan = Mapper.Map<OpeningBalanceLoan>(viewModel);
                            openingBalanceLoan.PrmKey = viewModel.OpeningBalanceLoanPrmKey;
                            openingBalanceLoan.LastProvisionDateOfLoan = DateTime.Now;
                            openingBalanceLoan.PreviousInstallmentDateOfLoan = DateTime.Now;
                            openingBalanceLoan.PreviousInterestDateOfLoan = DateTime.Now;
                            context.OpeningBalanceLoans.Attach(openingBalanceLoan);
                            context.Entry(openingBalanceLoan).State = EntityState.Added;
                            break;
                        default:
                            break;
                    }

                    context.OpeningBalanceMakerCheckers.Attach(openingBalanceMakerChecker);
                    context.Entry(openingBalanceMakerChecker).State = EntityState.Added;
                    openingBalance.OpeningBalanceMakerCheckers.Add(openingBalanceMakerChecker);

                    context.OpeningBalances.Attach(openingBalance);
                    context.Entry(openingBalance).State = EntityState.Added;
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

        public async Task<bool> Verify(OpeningBalanceViewModel _openingBalanceViewModel)
        {
            try
            {
                //Get OpeningBalance From Session Object New Added Record / Updated Record
                IEnumerable<OpeningBalanceViewModel> OpeningBalanceViewModelList = (IEnumerable<OpeningBalanceViewModel>)HttpContext.Current.Session["OpeningBalance"];

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;
                foreach (OpeningBalanceViewModel viewModel in OpeningBalanceViewModelList)
                {
                    //Set Default Value
                    GetOpeningBalanceDefaultValues(viewModel, StringLiteralValue.Verify);

                    OpeningBalanceMakerChecker openingBalanceMakerChecker = Mapper.Map<OpeningBalanceMakerChecker>(viewModel);
                    openingBalanceMakerChecker.OpeningBalancePrmKey = viewModel.OpeningBalancePrmKey;
                    openingBalanceMakerChecker.Remark = _openingBalanceViewModel.Remark;

                    byte schemeTypePrmKey = _openingBalanceViewModel.SchemeTypePrmKey;

                    switch (schemeTypePrmKey)
                    {
                        case 1:
                            OpeningBalanceShare openingBalanceShare = Mapper.Map<OpeningBalanceShare>(viewModel);
                            openingBalanceShare.PrmKey = viewModel.OpeningBalanceSharesPrmKey;
                            context.OpeningBalanceShares.Attach(openingBalanceShare);
                            context.Entry(openingBalanceShare).State = EntityState.Modified;
                            break;

                        case 2:
                            OpeningBalanceInvestment openingBalanceInvestment = Mapper.Map<OpeningBalanceInvestment>(viewModel);
                            openingBalanceInvestment.PrmKey = viewModel.OpeningBalanceInvestmentPrmKey;
                            openingBalanceInvestment.LastProvisionDateOfInvestment = DateTime.Now;
                            context.OpeningBalanceInvestments.Attach(openingBalanceInvestment);
                            context.Entry(openingBalanceInvestment).State = EntityState.Modified;
                            break;

                        case 3:
                            OpeningBalanceDeposit openingBalanceDeposit = Mapper.Map<OpeningBalanceDeposit>(viewModel);
                            openingBalanceDeposit.PrmKey = viewModel.OpeningBalanceDepositPrmKey;
                            openingBalanceDeposit.LastProvisionDateOfDeposit = DateTime.Now;
                            context.OpeningBalanceDeposits.Attach(openingBalanceDeposit);
                            context.Entry(openingBalanceDeposit).State = EntityState.Modified;
                            break;

                        case 4:
                            OpeningBalanceLoan openingBalanceLoan = Mapper.Map<OpeningBalanceLoan>(viewModel);
                            openingBalanceLoan.PrmKey = viewModel.OpeningBalanceLoanPrmKey;
                            openingBalanceLoan.LastProvisionDateOfLoan = DateTime.Now;
                            openingBalanceLoan.PreviousInstallmentDateOfLoan = DateTime.Now;
                            openingBalanceLoan.PreviousInterestDateOfLoan = DateTime.Now;
                            context.OpeningBalanceLoans.Attach(openingBalanceLoan);
                            context.Entry(openingBalanceLoan).State = EntityState.Modified;
                            break;
                        default:
                            break;
                    }

                    context.OpeningBalanceMakerCheckers.Attach(openingBalanceMakerChecker);
                    context.Entry(openingBalanceMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Reject(OpeningBalanceViewModel _openingBalanceViewModel)
        {
            try
            {
                //Get OpeningBalance From Session Object New Added Record / Updated Record
                IEnumerable<OpeningBalanceViewModel> OpeningBalanceViewModelList = (IEnumerable<OpeningBalanceViewModel>)HttpContext.Current.Session["OpeningBalance"];
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;
                foreach (OpeningBalanceViewModel viewModel in OpeningBalanceViewModelList)
                {
                    //Set Default Value
                    GetOpeningBalanceDefaultValues(viewModel, StringLiteralValue.Reject);
                    OpeningBalanceMakerChecker openingBalanceMakerChecker = Mapper.Map<OpeningBalanceMakerChecker>(viewModel);
                    openingBalanceMakerChecker.OpeningBalancePrmKey = viewModel.OpeningBalancePrmKey;
                    openingBalanceMakerChecker.Remark = _openingBalanceViewModel.Remark;
                    byte schemeTypePrmKey = _openingBalanceViewModel.SchemeTypePrmKey;

                    switch (schemeTypePrmKey)
                    {
                        case 1:
                            OpeningBalanceShare openingBalanceShare = Mapper.Map<OpeningBalanceShare>(viewModel);
                            openingBalanceShare.PrmKey = viewModel.OpeningBalanceSharesPrmKey;
                            context.OpeningBalanceShares.Attach(openingBalanceShare);
                            context.Entry(openingBalanceShare).State = EntityState.Modified;
                            break;

                        case 2:
                            OpeningBalanceInvestment openingBalanceInvestment = Mapper.Map<OpeningBalanceInvestment>(viewModel);
                            openingBalanceInvestment.PrmKey = viewModel.OpeningBalanceInvestmentPrmKey;
                            openingBalanceInvestment.LastProvisionDateOfInvestment = DateTime.Now;
                            context.OpeningBalanceInvestments.Attach(openingBalanceInvestment);
                            context.Entry(openingBalanceInvestment).State = EntityState.Modified;
                            break;

                        case 3:
                            OpeningBalanceDeposit openingBalanceDeposit = Mapper.Map<OpeningBalanceDeposit>(viewModel);
                            openingBalanceDeposit.PrmKey = viewModel.OpeningBalanceDepositPrmKey;
                            openingBalanceDeposit.LastProvisionDateOfDeposit = DateTime.Now;
                            context.OpeningBalanceDeposits.Attach(openingBalanceDeposit);
                            context.Entry(openingBalanceDeposit).State = EntityState.Modified;
                            break;

                        case 4:
                            OpeningBalanceLoan openingBalanceLoan = Mapper.Map<OpeningBalanceLoan>(viewModel);
                            openingBalanceLoan.PrmKey = viewModel.OpeningBalanceLoanPrmKey;
                            openingBalanceLoan.LastProvisionDateOfLoan = DateTime.Now;
                            openingBalanceLoan.PreviousInstallmentDateOfLoan = DateTime.Now;
                            openingBalanceLoan.PreviousInterestDateOfLoan = DateTime.Now;
                            context.OpeningBalanceLoans.Attach(openingBalanceLoan);
                            context.Entry(openingBalanceLoan).State = EntityState.Modified;
                            break;
                        default:
                            break;
                    }

                    context.OpeningBalanceMakerCheckers.Attach(openingBalanceMakerChecker);
                    context.Entry(openingBalanceMakerChecker).State = EntityState.Added;
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

        public void GetOpeningBalanceDefaultValues(OpeningBalanceViewModel _openingBalanceViewModel, string _entryStatus)
        {
            _openingBalanceViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"]; ;
            _openingBalanceViewModel.EntryDateTime = DateTime.Now;
            _openingBalanceViewModel.EntryStatus = _entryStatus;
            _openingBalanceViewModel.UserAction = _entryStatus;

            if ((_entryStatus == StringLiteralValue.Amend) || (_entryStatus == StringLiteralValue.Modify))
            {
                _openingBalanceViewModel.Remark = "None";
            }
        }

        public List<SelectListItem> GeneralLedgerDropdownList
        {
            get
            {
                var a = (from u in context.GeneralLedgers
                         where (u.ActivationStatus.Equals(StringLiteralValue.Active)
                         && u.EntryStatus.Equals(StringLiteralValue.Verify))
                         orderby u.NameOfGL
                         select new SelectListItem
                         {
                             Value = u.GeneralLedgerId.ToString(),
                             Text = (u.NameOfGL.Trim() + " ---> ")
                         }).ToList();

                return a;
            }
        }

        public List<SelectListItem> CustomerAccountDropdownList(Guid _generalLedgerId)
        {
            try
            {
                int generalLedgerPrmKey = generalLedgerRepository.GetPrmKeyById(_generalLedgerId);
                var a = (from o in context.OpeningBalances
                         join c in context.CustomerAccounts.Where(c => c.EntryStatus == StringLiteralValue.Verify) on o.CustomerAccountPrmKey equals c.PrmKey into oc
                         from c in oc.DefaultIfEmpty()
                         join d in context.CustomerAccountDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals d.CustomerAccountPrmKey into dc
                         from d in dc.DefaultIfEmpty()
                         join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify) on d.PersonPrmKey equals p.PrmKey into pc
                         from p in pc.DefaultIfEmpty()
                         where (o.EntryStatus.Equals(StringLiteralValue.Verify))
                         && (d.GeneralLedgerPrmKey == generalLedgerPrmKey)
                         orderby p.FullName
                         select new SelectListItem
                         {
                             Value = p.PersonId.ToString(),
                             Text = (p.FullName.Trim() + " ---> ")
                         }).Distinct().OrderBy(c => c.Text).ToList();

                return a;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public short GetGeneralLedgerPrmKeyById(Guid _generalLedgerId)
        {
            var a = context.GeneralLedgers
                    .Where(c => c.GeneralLedgerId == _generalLedgerId)
                    .Select(c => c.PrmKey).FirstOrDefault();
            return a;
        }

        public long GetPersonPrmKeyById(Guid _personId)
        {
            var a = context.People
                    .Where(c => c.PersonId == _personId)
                    .Select(c => c.PrmKey).FirstOrDefault();
            return a;
        }
    }
}
