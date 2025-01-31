using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoProject.Domain.Entities.Account.Master;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.ViewModel.Custom;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account
{
    public class EFAccountDetailRepository : IAccountDetailRepository
    {
        private readonly EFDbContext context;

        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;

        public EFAccountDetailRepository(RepositoryConnection _connection, IEnterpriseDetailRepository _enterpriseDetailRepository)
        {
            context = _connection.EFDbContext;
            enterpriseDetailRepository = _enterpriseDetailRepository;
        }

        public bool HasAccessOfAllBusinessOffice(short _userProfilePrmKey)
        {
            short homeBranchRoleProfilePrmKey = GetUserHomeBranchRoleProfilePrmKey(_userProfilePrmKey);

            return context.RoleProfiles
                    .Where(r => r.PrmKey == homeBranchRoleProfilePrmKey && r.EntryStatus == StringLiteralValue.Verify && r.ActivationStatus == StringLiteralValue.Active)
                    .Select(r => r.IsAllowAllAccessForBusinessOffice).FirstOrDefault();
        }

        public bool HasAccessOfAllGeneralLedger(short _userProfilePrmKey)
        {
            short homeBranchRoleProfilePrmKey = GetUserHomeBranchRoleProfilePrmKey(_userProfilePrmKey);

            return context.RoleProfiles
                    .Where(r => r.PrmKey == homeBranchRoleProfilePrmKey && r.EntryStatus == StringLiteralValue.Verify && r.ActivationStatus == StringLiteralValue.Active)
                    .Select(r => r.IsAllowAllAccessForGeneralLedger).FirstOrDefault();
        }

        public bool HasAccessOfAllTransaction(short _userProfilePrmKey)
        {
            short homeBranchRoleProfilePrmKey = GetUserHomeBranchRoleProfilePrmKey(_userProfilePrmKey);

            return context.RoleProfiles
                    .Where(r => r.PrmKey == homeBranchRoleProfilePrmKey && r.EntryStatus == StringLiteralValue.Verify && r.ActivationStatus == StringLiteralValue.Active)
                    .Select(r => r.IsAllowAllTransactions).FirstOrDefault();
        }

        public bool IsAnySharesApplicationPending()
        {
            int prmkey = context.SharesApplications
                            .Where(s => s.EntryStatus == StringLiteralValue.Verify && s.ApplicationStatus == StringLiteralValue.Pending)
                            .Select(s => s.PrmKey).FirstOrDefault();
            if (prmkey > 0)
                return true;
            else
                return false;
        }

        public bool IsUniqueDepositSchemeName(string _nameOfScheme)
        {
            bool status;
            // Get Shares Scheme Type PrmKey
            byte schemeTypePrmkey = (context.SchemeTypes.Where(s => s.SystemName == "Deposit").Select(s => s.PrmKey).FirstOrDefault());

            if (context.Schemes.Where(s => s.NameOfScheme == _nameOfScheme && s.EntryStatus == StringLiteralValue.Verify && s.SchemeTypePrmKey == schemeTypePrmkey).Select(s => s.PrmKey).FirstOrDefault() > 0)
                //Already registered  
                status = false;
            else
                //Available to use  
                status = true;

            return status;
        }

        public bool IsUniqueLoanSchemeName(string _nameOfScheme)
        {
            bool status;
            // Get Shares Scheme Type PrmKey
            byte schemeTypePrmkey = (context.SchemeTypes.Where(s => s.SystemName == "Loan").Select(s => s.PrmKey).FirstOrDefault());

            if (context.Schemes.Where(s => s.NameOfScheme == _nameOfScheme && s.EntryStatus == StringLiteralValue.Verify && s.SchemeTypePrmKey == schemeTypePrmkey).Select(s => s.PrmKey).FirstOrDefault() > 0)
                //Already registered  
                status = false;
            else
                //Available to use  
                status = true;

            return status;
        }

        public bool IsUniqueSharesCapitalSchemeName(string _nameOfScheme)
        {
            bool status;
            // Get Shares Scheme Type PrmKey
            byte schemeTypePrmkey = (context.SchemeTypes.Where(s => s.SystemName == "Shares").Select(s => s.PrmKey).FirstOrDefault());

            if (context.Schemes.Where(s => s.NameOfScheme == _nameOfScheme && s.EntryStatus == StringLiteralValue.Verify && s.SchemeTypePrmKey == schemeTypePrmkey).Select(s => s.PrmKey).FirstOrDefault() > 0)
                //Already registered  
                status = false;
            else
                //Available to use  
                status = true;

            return status;
        }

        public bool IsUniqueEmployeeCode(string _employeeCode)
        {
            bool status;

            if (context.Employees.Where(s => s.EmployeeCode == _employeeCode).Select(s => s.PrmKey).FirstOrDefault() > 0)
                //Already registered  
                status = false;
            else
                //Available to use  
                status = true;

            return status;
        }

        public bool IsUniqueUserProfileName(string _nameOfUserProfile)
        {
            bool status;

            if (context.UserProfiles.Where(s => s.NameOfUserProfile == _nameOfUserProfile).Select(s => s.PrmKey).FirstOrDefault() > 0)
                //Already registered  
                status = false;
            else
                //Available to use  
                status = true;

            return status;
        }

        public bool IsUniqueNameOfVehicleMake(string _nameOfVehicleMake)
        {
            bool status;

            if (context.VehicleMakes.Where(s => s.NameOfVehicleMake == _nameOfVehicleMake).Select(s => s.PrmKey).FirstOrDefault() > 0)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }

        public decimal GetGoldLoanRateByPurity(string _purity)
        {
            return context.GoldLoanRates
                    .Where(g => g.Purity == _purity && g.EntryStatus == StringLiteralValue.Verify)
                    .Select(g => g.LoanAmountPerGram).FirstOrDefault();
        }

        public bool IsDuplicatePolicyNumber(string _inputedPolicyNumber)
        {
            var a = context.CustomerVehicleLoanInsuranceDetails
                .Where(s => s.EntryStatus == StringLiteralValue.Verify && s.PolicyNumber == _inputedPolicyNumber)
                .Select(s => s.PrmKey).FirstOrDefault();

            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public bool IsValidAccountNumber(Guid _schemeId, int _accountNumber)
        //{
        //    try
        //    {
        //        short schemePrmKey = GetSchemePrmKeyById(_schemeId);

        //        return context.Database.SqlQuery<bool>("SELECT dbo.IsValidAccountNumber (@SchemePrmKey, @BusinessOfficePrmKey, @AccountNumber)", new SqlParameter("@SchemePrmKey", schemePrmKey), new SqlParameter("@BusinessOfficePrmKey", (short)HttpContext.Current.Session["UserHomeBranchPrmKey"]), new SqlParameter("AccountNumber", _accountNumber)).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message;

        //        return false;
        //    }
        //}

        //public bool IsValidMemberNumber(Guid _schemeId, int _memberNumber)
        //{
        //    try
        //    {
        //        short schemePrmKey = GetSchemePrmKeyById(_schemeId);

        //        return context.Database.SqlQuery<bool>("SELECT dbo.IsValidMemberNumber (@SchemePrmKey, @BusinessOfficePrmKey, @MemberNumber)", new SqlParameter("@SchemePrmKey", schemePrmKey), new SqlParameter("@BusinessOfficePrmKey", (short)HttpContext.Current.Session["UserHomeBranchPrmKey"]), new SqlParameter("MemberNumber", _memberNumber)).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message;

        //        return false;
        //    }
        //}

        public bool IsVisibleSharesApplicationNumber(Guid _schemeId)
        {
            try
            {
                short schemePrmKey = GetSchemePrmKeyById(_schemeId);

                bool isEnableAutoApplicationNumber = context.SchemeApplicationParameters
                                                    .Where(a => a.SchemePrmKey == schemePrmKey && a.EntryStatus == StringLiteralValue.Verify)
                                                    .Select(a => a.EnableCustomizeApplicationNumber).FirstOrDefault();

                return isEnableAutoApplicationNumber ? false : true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public bool IsValidSharesApplicationNumber(Guid _schemeId, int _applicationNumber)
        {
            try
            {
                short schemePrmKey = GetSchemePrmKeyById(_schemeId);

                return context.Database.SqlQuery<bool>("SELECT dbo.IsValidApplicationNumber (@SchemePrmKey, @BusinessOfficePrmKey, @GLPrmKey, @ApplicationNumber)", new SqlParameter("@SchemePrmKey", schemePrmKey), new SqlParameter("@BusinessOfficePrmKey", (short)HttpContext.Current.Session["UserHomeBranchPrmKey"]), new SqlParameter("@GLPrmKey", "0"), new SqlParameter("@ApplicationNumber", _applicationNumber)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public long GetPersonPrmKeyByPersonId(Guid _personId)
        {
            return context.People
            .Where(x => x.PersonId == _personId)
            .Select(x => x.PrmKey).FirstOrDefault();
        }

        //public List<SelectListItem> GetFixedDepositCustomerListByPersonPrmKey(Guid _personId)
        //{
        //    long _personPrmKey = GetPersonPrmKeyByPersonId(_personId);

        //    short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

        //    // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
        //    if (regionalLanguagePrmKey != 1)
        //    {
        //        return (from v in context.CustomerAccountDetails
        //                join g in context.SchemeGeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify) on v.GeneralLedgerPrmKey equals g.PrmKey into gl
        //                from g in gl.DefaultIfEmpty()
        //                join d in context.SchemeDepositAccountParameters.Where(d => d.EntryStatus == StringLiteralValue.Verify) on g.SchemePrmKey equals d.PrmKey into da
        //                from d in da.DefaultIfEmpty()
        //                join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify) on v.PersonPrmKey equals p.PrmKey into dp
        //                from p in dp.DefaultIfEmpty()
        //                join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
        //                from m in pm.DefaultIfEmpty()
        //                join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify && t.LanguagePrmKey == regionalLanguagePrmKey) on p.PrmKey equals t.PersonPrmKey into pt
        //                from t in pt.DefaultIfEmpty()
        //                join c in context.CustomerAccounts.Where(c => c.EntryStatus == StringLiteralValue.Verify) on v.CustomerAccountPrmKey equals c.PrmKey into ct
        //                from c in ct.DefaultIfEmpty()
        //                where (v.PersonPrmKey == _personPrmKey)
        //                 && (v.EntryStatus == StringLiteralValue.Verify)
        //                 && (d.DepositType == StringLiteralValue.FixedDeposit)
        //                 && (d.IsAvailablePledgeLoan == true)
        //                 && (t.LanguagePrmKey == regionalLanguagePrmKey)
        //                select new SelectListItem
        //                {
        //                    Value = c.CustomerAccountId.ToString(),
        //                    Text = (c.AccountNumber + p.FullName ?? m.FullName.Trim()) + " ---> " + (t.TransFullName.Trim() ?? "")
        //                }).Distinct().OrderBy(l => l.Text).ToList();
        //    }

        //    // Default List In Default Language (i.e. English)
        //    return (from v in context.CustomerAccountDetails
        //            join g in context.SchemeGeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify) on v.GeneralLedgerPrmKey equals g.PrmKey into gl
        //            from g in gl.DefaultIfEmpty()
        //            join d in context.SchemeDepositAccountParameters.Where(d => d.EntryStatus == StringLiteralValue.Verify) on g.SchemePrmKey equals d.PrmKey into da
        //            from d in da.DefaultIfEmpty()
        //            join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify) on v.PersonPrmKey equals p.PrmKey into dp
        //            from p in dp.DefaultIfEmpty()
        //            join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
        //            from m in pm.DefaultIfEmpty()
        //            join c in context.CustomerAccounts.Where(c => c.EntryStatus == StringLiteralValue.Verify) on v.CustomerAccountPrmKey equals c.PrmKey into ct
        //            from c in ct.DefaultIfEmpty()
        //            where (v.PersonPrmKey == _personPrmKey)
        //            && (d.DepositType == StringLiteralValue.FixedDeposit)
        //             && (v.EntryStatus == StringLiteralValue.Verify)
        //             && (d.IsAvailablePledgeLoan == true)
        //            select new SelectListItem
        //            {
        //                Value = c.CustomerAccountId.ToString(),
        //                Text = (c.AccountNumber + p.FullName ?? m.FullName.Trim())
        //            }).Distinct().OrderBy(l => l.Text).ToList();
        //}
        public List<SelectListItem> GetDepositAccountDropdownListByScheme(Guid _schemeId, Guid _personId)
        {
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            IEnumerable<DbQueryDropdownListViewModel> dropdownListViewModel = context.Database.SqlQuery<DbQueryDropdownListViewModel>("SELECT * FROM dbo.GetDropdownListOfDepositAccountForLoanAgainstDeposit (@SchemeId, @PersonId, @RegionalLanguagePrmKey)", new SqlParameter("@SchemeId", _schemeId), new SqlParameter("@PersonId", _personId), new SqlParameter("@RegionalLanguagePrmKey", regionalLanguagePrmKey)).Distinct().ToList();

            // Map the results to SelectListItem
            var selectListItems = dropdownListViewModel.Select(p => new SelectListItem
            {
                Value = p.ValueId.ToString(),
                Text = p.ValueText
            }).ToList();

            return selectListItems;
        }

        public byte GetAccountOperationModePrmKeyById(Guid _accountOperationModeId)
        {
            return context.AccountOperationModes
                    .Where(a => a.AccountOperationModeId == _accountOperationModeId)
                    .Select(a => a.PrmKey).FirstOrDefault();
        }

        public byte GetAgricultureLandTypePrmKeyById(Guid _agricultureLandTypeId)
        {
            return context.AgricultureLandTypes
                    .Where(a => a.AgricultureLandTypeId == _agricultureLandTypeId)
                    .Select(a => a.PrmKey).FirstOrDefault();
        }

        public byte GetBalanceTypePrmKeyById(Guid _balanceTypeId)
        {
            return context.BalanceTypes
                    .Where(b => b.BalanceTypeId == _balanceTypeId)
                    .Select(b => b.PrmKey).FirstOrDefault();
        }

        public byte GetChargesTypePrmKeyById(Guid _chargesApplyingTypeId)
        {
            return context.ChargesTypes
                    .Where(c => c.ChargesTypeId == _chargesApplyingTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetCashCreditDocumentTypePrmKey()
        {
            return context.DocumentTypes
                    .Where(dt => dt.SysNameOfDocumentType == "CCLOAN")
                    .Select(dt => dt.PrmKey).FirstOrDefault();
        }

        public short GetMemberAdmissionFeeAccountClassPrmKey()
        {
            return context.AccountClasses
                .Where(x => x.AccountClassCode == StringLiteralValue.MemberAdmissionFee)
                .Select(x => x.PrmKey).FirstOrDefault();
        }

        public short GetConsumerDurableLoanItemPrmKeyById(Guid _consumerDurableItemId)
        {
            return context.ConsumerDurableItems
                    .Where(c => c.ConsumerDurableItemId == _consumerDurableItemId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public decimal GetConsumerLoanMarginBySchemePrmKey(short _schemePrmKey, Guid _consumerDurableItemId)
        {
            short _consumerDurableItemPrmKey = GetConsumerDurableLoanItemPrmKeyById(_consumerDurableItemId);
            try
            {
                return context.SchemeConsumerDurableLoanItems.Where(a => a.EntryStatus == StringLiteralValue.Verify && a.SchemePrmKey == _schemePrmKey && a.ConsumerDurableItemPrmKey == _consumerDurableItemPrmKey)
                       .Select(a => a.Margin).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return 0;
            }
        }

        public short GetEducationalCoursePrmKeyById(Guid _educationalCourseId)
        {
            return context.EducationalCourses
                    .Where(c => c.EducationalCourseId == _educationalCourseId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetInstitutePrmKeyById(Guid _instituteId)
        {
            return context.Institutes
                    .Where(c => c.InstituteId == _instituteId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetCreditBureauAgencyPrmKeyById(Guid _creditBureauAgencyId)
        {
            return context.CreditBureauAgencies
                    .Where(c => c.CreditBureauAgencyId == _creditBureauAgencyId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetCustomerTypePrmKeyById(Guid _customerTypeId)
        {
            return context.CustomerTypes
                    .Where(c => c.CustomerTypeId == _customerTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetCustomerAccountTypePrmKeyById(Guid _customerAccountTypeId)
        {
            return context.CustomerAccountTypes
                    .Where(c => c.CustomerAccountTypeId == _customerAccountTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetDocumentPrmKeyId(Guid _documentId)
        {
            return context.Documents
                    .Where(f => f.DocumentId == _documentId)
                    .Select(f => f.PrmKey).FirstOrDefault();
        }

        public byte GetDaysInYearPrmKeyById(Guid _daysInYearId)
        {
            return context.DaysInYears
                    .Where(t => t.DaysInYearId == _daysInYearId)
                    .Select(t => t.PrmKey).FirstOrDefault();
        }

        public byte GetDenominationPrmKey(Guid _denominationId)
        {
            return context.Denominations
                    .Where(d => d.DenominationId == _denominationId)
                    .Select(d => d.PrmKey).FirstOrDefault();
        }

        public byte GetDividendCalculationMethodPrmKeyById(Guid _dividendCalculationMethodId)
        {
            return context.DividendCalculationMethods
                    .Where(c => c.DividendCalculationMethodId == _dividendCalculationMethodId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetDocumentTypePrmKeyBySysName(string _sysName)
        {
            return context.DocumentTypes
                    .Where(d => d.SysNameOfDocumentType == _sysName)
                    .Select(d => d.PrmKey).FirstOrDefault();
        }

        public byte GetFinancialAssetTypePrmKeyById(Guid _financialAssetTypeId)
        {
            return context.FinancialAssetTypes
                    .Where(f => f.FinancialAssetTypeId == _financialAssetTypeId)
                    .Select(f => f.PrmKey).FirstOrDefault();
        }

        public byte GetGSTReturnPeriodicityPrmKeyById(Guid _gSTReturnPeriodicitysId)
        {
            return context.GSTReturnPeriodicities
                    .Where(a => a.GSTReturnPeriodicityId == _gSTReturnPeriodicitysId)
                    .Select(a => a.PrmKey).FirstOrDefault();
        }

        public byte GetInstallmentFrequencyPrmKeyById(Guid _installmentFrequencyId)
        {
            return context.InstallmentFrequencies
                    .Where(i => i.InstallmentFrequencyId == _installmentFrequencyId)
                    .Select(i => i.PrmKey).FirstOrDefault();
        }

        public byte GetInterestCalculationFrequencyPrmKeyById(Guid _interestCalculationFrequencyId)
        {
            return context.InterestCalculationFrequencies
                    .Where(t => t.InterestCalculationFrequencyId == _interestCalculationFrequencyId)
                    .Select(t => t.PrmKey).FirstOrDefault();
        }

        public byte GetInterestCompoundingFrequencyPrmKeyById(Guid _interestCompoundingFrequencyId)
        {
            return context.InterestCompoundingFrequencies
                    .Where(t => t.InterestCompoundingFrequencyId == _interestCompoundingFrequencyId)
                    .Select(t => t.PrmKey).FirstOrDefault();
        }

        public byte GetInterestRateChargedDurationPrmKeyById(Guid _interestRateChargedDurationId)
        {
            return context.InterestRateChargedDurations
                    .Where(i => i.InterestRateChargedDurationId == _interestRateChargedDurationId)
                    .Select(i => i.PrmKey).FirstOrDefault();
        }

        public byte GetInterestMethodPrmKeyById(Guid _interestMethodId)
        {
            return context.InterestMethods
                    .Where(t => t.InterestMethodId == _interestMethodId)
                    .Select(t => t.PrmKey).FirstOrDefault();
        }

        public byte GetInterestRebateApplyFrequencyPrmKeyById(Guid _interestRebateApplyFrequencyId)
        {
            return context.InterestRebateApplyFrequencies
                    .Where(t => t.InterestRebateApplyFrequencyId == _interestRebateApplyFrequencyId)
                    .Select(t => t.PrmKey).FirstOrDefault();
        }

        public byte GetInterestRebateCriteriaPrmKeyById(Guid _interestRebateCriteriaId)
        {
            return context.InterestRebateCriterias
                    .Where(t => t.InterestRebateCriteriaId == _interestRebateCriteriaId)
                    .Select(t => t.PrmKey).FirstOrDefault();
        }

        public byte GetJointAccountHolderTypePrmKeyById(Guid _jointAccountHolderTypeId)
        {
            return context.JointAccountHolderTypes
                    .Where(j => j.JointAccountHolderTypeId == _jointAccountHolderTypeId)
                    .Select(j => j.PrmKey).FirstOrDefault();
        }

        public byte GetLendingChargesBasePrmKeyById(Guid _lendingChargesBaseId)
        {
            return context.LendingChargesBases
                    .Where(c => c.LendingChargesBaseId == _lendingChargesBaseId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetLendingInterestPostingFrequencyPrmKeyById(Guid _lendingInterestPostingFrequencyId)
        {
            return context.LendingInterestPostingFrequencies
                    .Where(t => t.LendingInterestPostingFrequencyId == _lendingInterestPostingFrequencyId)
                    .Select(t => t.PrmKey).FirstOrDefault();
        }

        public byte GetLendingRepaymentsInterestCalculationPrmKeyById(Guid _lendingRepaymentsInterestCalculationId)
        {
            return context.LendingRepaymentsInterestCalculations
                    .Where(t => t.LendingRepaymentsInterestCalculationId == _lendingRepaymentsInterestCalculationId)
                    .Select(t => t.PrmKey).FirstOrDefault();
        }

        public byte GetLoanTypePrmKeyById(Guid _loanTypeId)
        {
            return context.LoanTypes
                    .Where(c => c.LoanTypeId == _loanTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetMemberTypePrmKeyById(Guid _memberTypeId)
        {
            return context.MemberTypes
                    .Where(m => m.MemberTypeId == _memberTypeId)
                    .Select(m => m.PrmKey).FirstOrDefault();
        }

        public byte GetMemberTypePrmKeyBySysName(string _memberTypeSysName)
        {
            return context.MemberTypes
                    .Where(m => m.SysNameOfMemberType == _memberTypeSysName)
                    .Select(m => m.PrmKey).FirstOrDefault();
        }

        public int GetMinuteOfMeetingAgendaPrmKeyById(Guid _minuteOfMeetingAgendaId)
        {
            return context.MinuteOfMeetingAgendas
                    .Where(m => m.MinuteOfMeetingAgendaId == _minuteOfMeetingAgendaId)
                    .Select(m => m.PrmKey).FirstOrDefault();
        }

        public byte GetRenewTypePrmKeyById(Guid _renewTypeId)
        {
            return context.RenewTypes
                    .Where(r => r.RenewTypeId == _renewTypeId)
                    .Select(r => r.PrmKey).FirstOrDefault();
        }

        public byte GetRepaymentIntervalFrequencyPrmKeyById(Guid _repaymentIntervalFrequency)
        {
            return context.RepaymentIntervalFrequencies
                    .Where(r => r.RepaymentIntervalFrequencyId == _repaymentIntervalFrequency)
                    .Select(r => r.PrmKey).FirstOrDefault();
        }

        public byte GetSchemeTypePrmKeyById(Guid _schemeTypeId)
        {
            return context.SchemeTypes
                    .Where(c => c.SchemeTypeId == _schemeTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetSweepOutFrequencyPrmKeyById(Guid _sweepOutFrequencyId)
        {
            return context.SweepOutFrequencies
                    .Where(s => s.SweepOutFrequencyId == _sweepOutFrequencyId)
                    .Select(s => s.PrmKey).FirstOrDefault();
        }

        public byte GetTransactionTypePrmKeyById(Guid _transactionTypeId)
        {
            return context.TransactionTypes
                    .Where(t => t.TransactionTypeId == _transactionTypeId)
                    .Select(t => t.PrmKey).FirstOrDefault();
        }

        public byte GetTargetGroupPrmKeyById(Guid _targetGroupId)
        {
            return context.TargetGroups
                    .Where(c => c.TargetGroupId == _targetGroupId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetVehicleBodyTypePrmKeyById(Guid _vehicleBodyTypeId)
        {
            return context.VehicleBodyTypes
                    .Where(c => c.VehicleBodyTypeId == _vehicleBodyTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetVehicleTypePrmKeyById(Guid _vehicleTypeId)
        {
            return context.VehicleTypes
                    .Where(c => c.VehicleTypeId == _vehicleTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public bool GetAuthorizedUserStatusByPrmKey(short _userProfilePrmKey)
        {
            short specialPermissionPrmKey = GetSpecialPermissionPrmKey(_userProfilePrmKey);
            string specialPermissionCode = GetSpecialPermissionCodeByPrmKey(specialPermissionPrmKey);
            bool result = false;
            if (specialPermissionCode == "UpdShrDedc")
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public short GetSpecialPermissionPrmKey(short _userProfilePrmKey)
        {
            return context.UserProfileSpecialPermissions
                    .Where(t => t.UserProfilePrmKey == _userProfilePrmKey && t.EntryStatus == StringLiteralValue.Verify)
                    .Select(t => t.SpecialPermissionPrmKey).FirstOrDefault();
        }


        public string GetSpecialPermissionCodeByPrmKey(short specialPermissionPrmKey)
        {
            return context.SpecialPermissions
                    .Where(t => t.PrmKey == specialPermissionPrmKey)
                    .Select(t => t.SpecialPermissionCode).FirstOrDefault();
        }

        public string GetUserNameByUserProfilePrmKey(short _userProfilePrmKey)
        {
            return context.UserProfiles
                    .Where(t => t.PrmKey == _userProfilePrmKey)
                    .Select(t => t.NameOfUserProfile).FirstOrDefault();
        }

        public short MembershipAgeForResignMembership()
        {
            return context.SharesCapitalByLawsParameters
                    .Where(s => s.EntryStatus == StringLiteralValue.Verify)
                    .Select(s => s.MembershipAgeForResignMembership).FirstOrDefault();
        }

        public short GetGeneralLedgerPrmKeyById(Guid _GeneralLedgerId)
        {
            return context.GeneralLedgers
                    .Where(c => c.GeneralLedgerId == _GeneralLedgerId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetGSTRegistrationTypePrmKeyById(Guid _gSTRegistrationTypesId)
        {
            return context.GSTRegistrationTypes
                    .Where(a => a.GSTRegistrationTypeId == _gSTRegistrationTypesId)
                    .Select(a => a.PrmKey).FirstOrDefault();
        }

        public short GetCurrencyPrmKeyById(Guid _currencyId)
        {
            return context.Currencies
                    .Where(c => c.CurrencyId == _currencyId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetFrequencyPrmKeyById(Guid _frequencyId)
        {
            return context.Frequencies
                    .Where(f => f.FrequencyId == _frequencyId)
                    .Select(f => f.PrmKey).FirstOrDefault();
        }

        public short GetFundPrmKeyById(Guid _fundCategoryId)
        {
            return context.Funds
                    .Where(f => f.FundId == _fundCategoryId)
                    .Select(f => f.PrmKey).FirstOrDefault();
        }

        public short GetLoanReasonPrmKeyById(Guid _loanReasonId)
        {
            return context.LoanReasons
                    .Where(c => c.LoanReasonId == _loanReasonId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetLoanRecoveryActionPrmKeyById(Guid _loanRecoveryActionId)
        {
            return context.LoanRecoveryActions
                    .Where(c => c.LoanRecoveryActionId == _loanRecoveryActionId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetVehicleMakePrmKeyById(Guid _vehicleMakeId)
        {
            return context.VehicleMakes
                    .Where(c => c.VehicleMakeId == _vehicleMakeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetVehicleModelPrmKeyById(Guid _VehicleModelId)
        {
            return context.VehicleModels
                    .Where(c => c.VehicleModelId == _VehicleModelId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetVehicleVariantPrmKeyById(Guid _vehicleVariantId)
        {
            return context.VehicleVariants
                    .Where(c => c.VehicleVariantId == _vehicleVariantId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public  short GetVehicleColourPrmKeyById(Guid _colourId)
        {
            return context.Colours
                    .Where(c => c.ColourId == _colourId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetSchemePrmKeyById(Guid _schemeId)
        {
            return context.Schemes
                    .Where(c => c.SchemeId == _schemeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetGoldOrnamentPrmKeyById(Guid _goldOrnamentId)
        {
            return context.GoldOrnaments
                    .Where(c => c.GoldOrnamentId == _goldOrnamentId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetConsumerDurableItemBrandPrmKeyById(Guid _consumerDurableItemBrandId)
        {
            return context.ConsumerDurableItemBrands
                    .Where(c => c.ConsumerDurableItemBrandId == _consumerDurableItemBrandId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetConsumerDurableItemPrmKeyById(Guid _consumerDurableItemId)
        {
            return context.ConsumerDurableItems
                    .Where(c => c.ConsumerDurableItemId == _consumerDurableItemId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }
      
        public int GetCustomerDepositAccountPrmKeyByCustomerAccountId(Guid _customerAccountId)
        {
            long customerAccountPrmKey = GetCustomerAccountPrmKeyById(_customerAccountId);
            
            return context.CustomerDepositAccounts
                    .Where(c => c.CustomerAccountPrmKey == customerAccountPrmKey)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetAccountClassPrmKeyById(Guid _accountClassId)
        {
            return context.AccountClasses
                    .Where(a => a.AccountClassId == _accountClassId)
                    .Select(a => a.PrmKey).FirstOrDefault();
        }

        public short GetChequeBookMasterPrmKeyById(Guid _chequeBookMasterId)
        {
            return context.ChequeBookMasters
                    .Where(c => c.ChequeBookMasterId == _chequeBookMasterId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetTimePeriodForNewCustomerAccountFlag(short _shcemePrmkey)
        {
            return context.SchemeAccountParameters
                    .Where(s => s.SchemePrmKey == _shcemePrmkey && s.EntryStatus == StringLiteralValue.Verify)
                    .Select(s => s.TimePeriodForNewCustomerFlag).FirstOrDefault();
        }

        public short GetUserHomeBranchPrmKey(short _userProfilePrmKey)
        {
            return context.UserProfileHomeBusinessOffices
                    .Where(u => u.UserProfilePrmKey == _userProfilePrmKey && u.EntryStatus == StringLiteralValue.Verify && u.ActivationStatus == StringLiteralValue.Active)
                    .Select(u => u.BusinessOfficePrmKey).FirstOrDefault();
        }

        public short GetUserHomeBranchRoleProfilePrmKey(short _userProfilePrmKey)
        {
            short homeBranchPrmKey = GetUserHomeBranchPrmKey(_userProfilePrmKey);

            return context.UserRoleProfiles
                    .Where(u => u.UserProfilePrmKey == _userProfilePrmKey && u.BusinessOfficePrmKey == homeBranchPrmKey && u.EntryStatus == StringLiteralValue.Verify && u.ActivationStatus == StringLiteralValue.Active)
                    .Select(u => u.RoleProfilePrmKey).FirstOrDefault();
        }

        public int GetCurrentGoldLoanRatePrmKey(string _purity)
        {
            return context.GoldLoanRates
                    .Where(g => g.Purity == _purity && g.EntryStatus == StringLiteralValue.Verify)
                    .OrderByDescending(g => g.EffectiveDate).ThenByDescending(g => g.PrmKey)
                    .Select(g => g.PrmKey).FirstOrDefault();
        }

        public int GetVehicleSupplierPrmKeyById(Guid VehicleSupplierId)
        {
            return context.VehicleSuppliers
                    .Where(c => c.VehicleSupplierId == VehicleSupplierId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public long GetCustomerAccountNumberById(Guid _customerAccountId)
        {
            return context.CustomerAccounts
                    .Where(c => c.CustomerAccountId == _customerAccountId)
                    .Select(c => c.AccountNumber).FirstOrDefault();
        }

        public long GetCustomerAccountPrmKeyById(Guid _customerAccountId)
        {
            return context.CustomerAccounts
                    .Where(c => c.CustomerAccountId == _customerAccountId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

      
        //public long GetCustomerAccountNomineePrmKeyById(Guid _customerAccountNomineeId)
        //{
        //    var a = context.CustomerAccountNominees
        //            .Where(c => c.CustomerAccountNomineeId == _customerAccountNomineeId)
        //            .Select(c => c.PrmKey).FirstOrDefault();
        //    return a;
        //}


        public decimal GetMaximumSharesHolidingLimitAmount()
        {
            return context.SharesCapitalByLawsParameters
                            .Where(s => s.EntryStatus == StringLiteralValue.Verify)
                            .Select(s => s.MaximumSharesHolidingLimitAmount).FirstOrDefault();
        }

        public decimal GetMaximumSharesHoldingLimitPercentage()
        {
            var a = context.SharesCapitalByLawsParameters
                            .Where(s => s.EntryStatus == StringLiteralValue.Verify)
                            .Select(s => s.MaximumSharesHolidingLimitPercentage).FirstOrDefault();
            return a;
        }

        public decimal AggregateSharesWithdrawalLimit()
        {
            return context.SharesCapitalByLawsParameters
                    .Where(s => s.EntryStatus == StringLiteralValue.Verify)
                    .Select(s => s.AggregateSharesWithdrawalLimit).FirstOrDefault();
        }


        public decimal GetAccountOpeningAmount(short schemePrmKey)
        {
            //short schemePrmKey = GetSchemePrmKeyById(_schemeId);

            return context.SchemeDemandDepositDetails
                    .Where(s => s.SchemePrmKey == schemePrmKey && s.EntryStatus == StringLiteralValue.Verify)
                    .Select(s => s.InitialAccountOpeningAmount).FirstOrDefault();
        }

        public short GetMinimumNumberOfShares(short _schemePrmKey)
        {
            return context.SchemeSharesCapitalAccountParameters
                .Where(s => s.SchemePrmKey == _schemePrmKey)
                .Select(s => s.MinimumNumberOfShares).FirstOrDefault();
        }
        public short GetMaximumNumberOfShares(short _schemePrmKey)
        {
            return context.SchemeSharesCapitalAccountParameters
                .Where(s => s.SchemePrmKey == _schemePrmKey)
                .Select(s => s.MaximumNumberOfShares).FirstOrDefault();
        }
        public bool GetEnableAutoCertificateNumber(short _schemePrmKey)
        {
            return context.SchemeSharesCertificateParameters
                .Where(s => s.SchemePrmKey == _schemePrmKey)
                .Select(s => s.EnableAutoCertificateNumber).FirstOrDefault();
        }

        // MinimumBalanceTypePrmKey - 1
        public decimal GetDemandDepositMinimumBalanceAmount(short _schemePrmKey)
        {
            return context.SchemeDemandDepositDetails
                    .Where(s => s.SchemePrmKey == _schemePrmKey && s.BalanceTypePrmKey == 1 && s.EntryStatus == StringLiteralValue.Verify)
                    .Select(c => c.BalanceAmount).FirstOrDefault();
        }

        public string GetAuditClass(byte _previousYear)
        {
            int currentFinancialYear = GetCurrentFinancialYearStartDate().Year;

            return context.OrganizationAuditClasses
                    .Where(o => o.FinancialYear == (currentFinancialYear - _previousYear))
                    .Select(o => o.AuditClass).FirstOrDefault();
        }

        // Consider K1 Schedule As 1, K2 As 2........K6 As 6
        //public string GetVehicleTypeByVehicleVariantId(Guid _vehicleTypeId)
        //{
        //    short vehiclevariantPrmKey = GetVehicleVariantPrmKeyById(_vehicleTypeId);

        //    return "None";
        //    // ************* Update  Later
        //    //return (from v in context.VehicleVariants
        //    //        join m in context.VehicleModels .Where(m=> m.EntryStatus == EntryStatus.Verified) on v.VehicleModelPrmKey equals m.PrmKey
        //    //        join vd in context.VehicleDetails on m.VehicleModelPrmKey equals vd.VehicleModelPrmKey
        //    //        join vbt in context.VehicleBodyTypes on vd.VehicleBodyTypePrmKey equals vbt.PrmKey
        //    //        join vt in context.VehicleTypes on vbt.VehicleTypePrmKey equals vt.PrmKey into bt
        //    //        from vt in bt.DefaultIfEmpty()
        //    //        where b.PrmKey == vehiclevariantPrmKey
        //    //        select vt.NameOfVehicleType).FirstOrDefault();
        //}

        public byte GetVehicleTypePrmKey(Guid _vehicleModelId)
        {
            byte vehicleBodyTypePrmKey = GetVehicleBodyTypePrmKeyByVehicleModelId(_vehicleModelId);
            return GetVehicleTypePrmKeyByVehicleBodyTypePrmKey(vehicleBodyTypePrmKey);

        }

        public byte GetVehicleBodyTypePrmKeyByVehicleModelId(Guid _vehicleModelId)
        {
            return context.VehicleModels
            .Where(x => x.VehicleModelId == _vehicleModelId)
            .Select(x => x.VehicleBodyTypePrmKey).FirstOrDefault();
        }

        public byte GetVehicleTypePrmKeyByVehicleBodyTypePrmKey(byte _vehicleBodyTypePrmKey)
        {
            return context.VehicleBodyTypes
            .Where(x => x.PrmKey == _vehicleBodyTypePrmKey)
            .Select(x => x.VehicleTypePrmKey).FirstOrDefault();
        }

        public string GetSysNameOfVehicleType(byte _vehicleTypePrmKey)
        {
            return context.VehicleTypes
                    .Where(t => t.PrmKey == _vehicleTypePrmKey)
                    .Select(t => t.SysNameOfVehicleType).FirstOrDefault();
        }


        public string GetRenewTypeSysNameById(Guid _renewTypeId)
        {
            short rollOverTypePrmKey = GetRenewTypePrmKeyById(_renewTypeId);

            return (from r in context.RenewTypes
                    where r.PrmKey == rollOverTypePrmKey
                    select r.SysNameOfRenewType).FirstOrDefault();
        }

        public string GetSysNameOfInterestMethodTypeById(Guid _interestMethodId)
        {
            return context.InterestMethods
                      .Where(c => c.InterestMethodId == _interestMethodId)
                      .Select(c => c.SysNameOfInterestMethod).FirstOrDefault();

        }

        public string GetSysNameOfSchemeTypeByGeneralLedgerId(Guid _generalLedgerId)
        {
            short generalLedgerPrmKey = GetGeneralLedgerPrmKeyById(_generalLedgerId);

            return (from g in context.SchemeGeneralLedgers
                    join s in context.Schemes.Where(s => s.EntryStatus == StringLiteralValue.Verify) on g.SchemePrmKey equals s.PrmKey into gs
                    from s in gs.DefaultIfEmpty()
                    join t in context.SchemeTypes.Where(t => t.ActivationStatus == StringLiteralValue.Active) on s.SchemeTypePrmKey equals t.PrmKey into st
                    from t in st.DefaultIfEmpty()
                    where (g.GeneralLedgerPrmKey == generalLedgerPrmKey)
                    && (g.EntryStatus == StringLiteralValue.Verify)
                    select (t.SystemName)).FirstOrDefault();
        }

        public string GetSysNameOfTimePeriodUnitById(Guid _timePeriodUnitId)
        {
            return context.TimePeriodUnits
                    .Where(t => t.TimePeriodUnitId == _timePeriodUnitId)
                    .Select(t => t.SysNameOfUnit).FirstOrDefault();
        }

        public string GetSysNameOfTimePeriodUnitByPrmKey(byte _timePeriodUnitPrmKey)
        {
            return context.TimePeriodUnits
                    .Where(t => t.PrmKey == _timePeriodUnitPrmKey)
                    .Select(t => t.SysNameOfUnit).FirstOrDefault();
        }

        public string GetEligibilityForGuarantor(Guid _schemeId)
        {
            short schemePrmKey = GetSchemePrmKeyById(_schemeId);

            return context.SchemeLoanAccountParameters
                        .Where(l => l.SchemePrmKey == schemePrmKey && l.EntryStatus == StringLiteralValue.Verify)
                        .Select(l => l.EligibilityForGuarantor).FirstOrDefault();
        }

        //public string GetPreOwnedVehiclePhotoUploadBySchemeId(Guid _schemeId)
        //{
        //    try
        //    {
        //        short schemePrmKey = GetSchemePrmKeyById(_schemeId);

        //        return context.SchemeVehicleLoanParameters
        //                            .Where(a => a.SchemePrmKey == schemePrmKey && a.EntryStatus == StringLiteralValue.Verify)
        //                            .Select(a => a.PreOwnedVehiclePhotoUpload).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message;

        //        return null;
        //    }
        //}

        public string GetSysNameOfLoanTypeByLoanTypeId(Guid _loanTypeId)
        {
            return context.LoanTypes
                      .Where(c => c.LoanTypeId == _loanTypeId)
                      .Select(c => c.SysNameOfLoanType).FirstOrDefault();

        }

        public string GetSysNameOfLoanTypeBySchemePrmKey(short _schemePrmKey)
        {
            byte loanTypePrmKey = GetLoanTypePrmKeyBySchemePrmKey(_schemePrmKey);

            return context.LoanTypes
                      .Where(c => c.PrmKey == loanTypePrmKey)
                      .Select(c => c.SysNameOfLoanType).FirstOrDefault();

        }
        
        public byte GetLoanTypePrmKeyBySchemePrmKey(short _schemePrmKey)
        {
            return context.SchemeLoanAccountParameters
                .Where(c => c.SchemePrmKey == _schemePrmKey)
                .Select(c => c.LoanTypePrmKey).FirstOrDefault();
        }

        public string GetNameOfColourByColourId(Guid _colourId)
        {
            return context.Colours
                      .Where(c => c.ColourId == _colourId)
                      .Select(c => c.NameOfColour).FirstOrDefault();
        }

        public string GetNameOfInstituteByInstituteId(Guid _instituteId)
        {
            return context.Institutes
                      .Where(c => c.InstituteId == _instituteId)
                      .Select(c => c.NameOfInstitute).FirstOrDefault();
        }

        public string GetSysNameOfOccupationById(Guid _occupationId)
        {
            return context.Occupations
                .Where(o => o.OccupationId == _occupationId)
                .Select(o => o.SysNameOfOccupation).FirstOrDefault();
        }

        public DateTime GetCurrentFinancialYearStartDate()
        {
            return context.FinancialYears
                    .Where(f => f.IsCurrent == true && f.IsClosed == false)
                    .Select(f => f.StartDate).FirstOrDefault();
        }

        public DateTime GetPreviousClosingFinancialYearEndDate()
        {
            return context.FinancialYears
                     .Where(f => f.IsClosed == true)
                     .OrderByDescending(f => f.EndDate)
                     .Select(f => f.EndDate).FirstOrDefault();
        }
       
        //public GoldLoanRate GetGoldLoanRate(DateTime _dateTime, string _purity)
        //{
        //    return context.GoldLoanRates
        //            .Where(g => g.Purity == _purity && g.EffectiveDate <= _dateTime && g.EntryStatus == StringLiteralValue.Verify)
        //            .OrderByDescending(g => g.EffectiveDate).ThenByDescending(g => g.PrmKey)
        //            .FirstOrDefault();
        //}

        public SchemeClosingChargesViewModel GetSchemeClosingCharges(short _schemePrmKey)
        {
            try
            {
                return context.Database.SqlQuery<SchemeClosingChargesViewModel>("SELECT * FROM dbo.GetSchemeClosingChargesEntriesBySchemePrmKey (@SchemePrmkey, @EntryType)", new SqlParameter("@SchemePrmkey", _schemePrmKey)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public SchemeSharesTransferChargesViewModel GetSchemeSharesTransferCharges(short _schemePrmKey)
        {
            try
            {
                return context.Database.SqlQuery<SchemeSharesTransferChargesViewModel>("SELECT * FROM dbo.GetSchemeSharesTransferChargesEntriesBySchemePrmKey (@SchemePrmkey, @EntryType)", new SqlParameter("@SchemePrmkey", _schemePrmKey)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //public SchemeVehicleLoanParameterViewModel GetSchemeVehicleLoanParameterValidations(Guid _schemeId)
        //{
        //    short schemePrmKey = GetSchemePrmKeyById(_schemeId);
        //    var documentValidations = context.SchemeVehicleLoanParameters.Where((a => a.EntryStatus == StringLiteralValue.Verify && a.SchemePrmKey == schemePrmKey))
        //                              .Select(x => new SchemeVehicleLoanParameterViewModel
        //                              {
        //                                  PreOwnedVehiclePhotoUpload = x.PreOwnedVehiclePhotoUpload,
        //                                  MaximumFileSizeForPreOwnedVehiclePhotoUploadInLocalStorage = x.MaximumFileSizeForPreOwnedVehiclePhotoUploadInLocalStorage,
        //                                  PreOwnedVehiclePhotoAllowedFileFormatsForLocalStorage = x.PreOwnedVehiclePhotoAllowedFileFormatsForLocalStorage,
        //                                  MaximumFileSizeForPreOwnedVehiclePhotoUploadInDb = x.MaximumFileSizeForPreOwnedVehiclePhotoUploadInDb,
        //                                  PreOwnedVehiclePhotoAllowedFileFormatsForDb = x.PreOwnedVehiclePhotoAllowedFileFormatsForDb,
        //                                  EnablePreOwnedVehiclePhotoUploadInDb = x.EnablePreOwnedVehiclePhotoUploadInDb,
        //                                  EnablePreOwnedVehiclePhotoUploadInLocalStorage = x.EnablePreOwnedVehiclePhotoUploadInLocalStorage,
        //                              }).FirstOrDefault();

        //    return documentValidations;
        //}

        //Dropdown List

        public short GetConsumerItemSupplierPersonCategoryPrmKey()
        {
            return context.PersonCategories
                    .Where(c => c.SysNameOfPersonCategory == StringLiteralValue.ConsumerSupplier)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public List<SelectListItem> GetConsumerDurableLoanItemDropdownListBySchemePrmKey(short _schemePrmKey)
        {
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {

                return (from s in context.SchemeConsumerDurableLoanItems
                        where (s.SchemePrmKey == _schemePrmKey)
                        && (s.EntryStatus == StringLiteralValue.Verify)
                        join c in context.ConsumerDurableItems.Where(c => c.EntryStatus == EntryStatus.Verified) on s.ConsumerDurableItemPrmKey equals c.PrmKey into sc
                        from c in sc.DefaultIfEmpty()
                        join mf in context.ConsumerDurableItemModifications.Where(mf => mf.EntryStatus == EntryStatus.Verified) on c.PrmKey equals mf.ConsumerDurableItemPrmKey into cmf
                        from mf in cmf.DefaultIfEmpty()
                        join t in context.ConsumerDurableItemTranslations.Where(t => t.EntryStatus == EntryStatus.Verified) on c.PrmKey equals t.ConsumerDurableItemPrmKey into ct
                        from t in ct.DefaultIfEmpty()

                        select new SelectListItem
                        {
                            Value = c.ConsumerDurableItemId.ToString(),
                            Text = (mf.NameOfItem ?? c.NameOfItem.Trim()) + " ---> " + (t.TransNameOfItem.Trim() ?? "")
                        }).Distinct().ToList();

            }

            // Default List In Default Language (i.e. English)
            return (from s in context.SchemeConsumerDurableLoanItems
                    where (s.SchemePrmKey == _schemePrmKey)
                    && (s.EntryStatus == StringLiteralValue.Verify)
                    join c in context.ConsumerDurableItems.Where(c => c.EntryStatus == EntryStatus.Verified) on s.ConsumerDurableItemPrmKey equals c.PrmKey into sc
                    from c in sc.DefaultIfEmpty()
                    join mf in context.ConsumerDurableItemModifications.Where(mf => mf.EntryStatus == EntryStatus.Verified) on c.PrmKey equals mf.ConsumerDurableItemPrmKey into cmf
                    from mf in cmf.DefaultIfEmpty()
                    select new SelectListItem
                    {
                        Value = c.ConsumerDurableItemId.ToString(),
                        Text = (mf.NameOfItem ?? c.NameOfItem.Trim())
                    }).Distinct().ToList();
        }

        public List<SelectListItem> ConsumerDurableItemBrandDropdownList
        {
            get
            {
                return (from c in context.ConsumerDurableItemBrands
                        where (c.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = c.ConsumerDurableItemBrandId.ToString(),
                            Text = (c.NameOfBrand.Trim())
                        }).Distinct().ToList();
            }
        }

        public List<SelectListItem> ConsumerDurableSupplierDropdownList
        {
            get
            {
                short supplierPrmKey = GetConsumerItemSupplierPersonCategoryPrmKey();
                return (from c in context.PersonCategories
                        where (c.PrmKey == supplierPrmKey)
                        join p in context.PersonAdditionalDetails.Where(p => p.EntryStatus == EntryStatus.Verified) on c.PrmKey equals p.PersonCategoryPrmKey into cp
                        from p in cp.DefaultIfEmpty()
                        join l in context.People.Where(l => l.EntryStatus == EntryStatus.Verified && l.ActivationStatus == StringLiteralValue.Active) on p.PersonPrmKey equals l.PrmKey into pl
                        from l in pl.DefaultIfEmpty()
                        select new SelectListItem
                        {
                            Value = l.PersonId.ToString(),
                            Text = (l.FullName.Trim())
                        }).Distinct().ToList();
            }
        }
       
        public List<SelectListItem> AccountClassDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from a in context.AccountClasses
                            join t in context.AccountClassTranslations on a.PrmKey equals t.AccountClassPrmKey
                            where a.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = a.AccountClassId.ToString(),
                                Text = a.NameOfAccountClass.Trim() + " ---> " + t.TransNameOfAccountClass.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from a in context.AccountClasses
                        where a.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = a.AccountClassId.ToString(),
                            Text = a.NameOfAccountClass
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> AccountClosingChargesGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.AccountClosingCharges);
            }
        }

        public List<SelectListItem> AccountTransferChargesGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.AccountTransferCharges);
            }
        }

        public List<SelectListItem> AccountOperationModeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from b in context.AccountOperationModes
                            join t in context.AccountOperationModeTranslations on b.PrmKey equals t.AccountOperationModePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (b.ActivationStatus == StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = b.AccountOperationModeId.ToString(),
                                Text = b.NameOfAccountOperationMode.Trim() + " ---> " + t.TransNameOfAccountOperationMode.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from b in context.AccountOperationModes
                        where (b.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = b.AccountOperationModeId.ToString(),
                            Text = (b.NameOfAccountOperationMode.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> AgricultureLandTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from a in context.AgricultureLandTypes
                            join t in context.AgricultureLandTypeTranslations on a.PrmKey equals t.AgricultureLandTypePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (a.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = a.AgricultureLandTypeId.ToString(),
                                Text = (a.NameOfAgricultureLandType.Trim() + " ---> " + ((t.TransNameOfAgricultureLandType.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from a in context.AgricultureLandTypes
                        where (a.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = a.AgricultureLandTypeId.ToString(),
                            Text = (a.NameOfAgricultureLandType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> AuthorizedTransactionTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // Get UserProfilePrmKey
                short userProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                
                string sysNameOfBusinessOfficeType = enterpriseDetailRepository.GetSysNameOfBusinessOfficeTypeByPrmKey((short)HttpContext.Current.Session["UserHomeBranchPrmKey"]);

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (HasAccessOfAllTransaction(userProfilePrmKey))
                {
                    if(regionalLanguagePrmKey != 1)
                    {
                        return (from ty in context.TransactionTypes
                                join t in context.TransactionTypeTranslations on ty.PrmKey equals t.TransactionTypePrmKey into tt
                                from t in tt.DefaultIfEmpty()
                                where (ty.AvailableFor == StringLiteralValue.All || ty.AvailableFor == sysNameOfBusinessOfficeType)
                                select new SelectListItem
                                {
                                    Value = ty.TransactionTypeId.ToString(),
                                    Text = (ty.NameOfTransactionType.Trim() + " ---> " + ((t.TransNameOfTransactionType.Trim()) ?? ""))
                                }).Distinct().OrderBy(l => l.Text).ToList();
                    }

                    return (from ty in context.TransactionTypes
                            where (ty.AvailableFor == StringLiteralValue.All || ty.AvailableFor == sysNameOfBusinessOfficeType)
                            select new SelectListItem
                            {
                                Value = ty.TransactionTypeId.ToString(),
                                Text = (ty.NameOfTransactionType.Trim())
                            }).Distinct().OrderBy(l => l.Text).ToList();

                }
                else
                {
                    if (regionalLanguagePrmKey != 1)
                    {
                        return (from u in context.UserProfileTransactionLimits
                                join ty in context.TransactionTypes.Where(ty => ty.ActivationStatus == StringLiteralValue.Active) on u.TransactionTypePrmKey equals ty.PrmKey into ut
                                from ty in ut.DefaultIfEmpty()
                                join t in context.TransactionTypeTranslations on ty.PrmKey equals t.TransactionTypePrmKey into tt
                                from t in tt.DefaultIfEmpty()
                                where (u.UserProfilePrmKey == userProfilePrmKey)
                                && (u.EntryStatus == (StringLiteralValue.Verify))
                                && (u.ActivationStatus == StringLiteralValue.Active)
                                && (ty.AvailableFor == StringLiteralValue.All || ty.AvailableFor == sysNameOfBusinessOfficeType)
                                select new SelectListItem
                                {
                                    Value = ty.TransactionTypeId.ToString(),
                                    Text = (ty.NameOfTransactionType.Trim() + " ---> " + ((t.TransNameOfTransactionType.Trim()) ?? ""))
                                }).Distinct().OrderBy(l => l.Text).ToList();
                    }

                    // Default List In Default Language (i.e. English)
                    return (from u in context.UserProfileTransactionLimits
                            join ty in context.TransactionTypes.Where(ty => ty.ActivationStatus == StringLiteralValue.Active) on u.TransactionTypePrmKey equals ty.PrmKey
                            where (u.UserProfilePrmKey == userProfilePrmKey)
                                    && (u.EntryStatus == (StringLiteralValue.Verify))
                                && (u.ActivationStatus == StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = ty.TransactionTypeId.ToString(),
                                Text = (ty.NameOfTransactionType.Trim())
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }
            }
        }

        public List<SelectListItem> BalanceTypeDropdownList
        {
            get
            {
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from b in context.BalanceTypes
                            join t in context.BalanceTypeTranslations on b.PrmKey equals t.BalanceTypePrmKey
                            where b.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = b.BalanceTypeId.ToString(),
                                Text = b.NameOfBalanceType.Trim() + " ---> " + t.TransNameOfBalanceType.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from b in context.BalanceTypes
                        where b.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = b.BalanceTypeId.ToString(),
                            Text = b.NameOfBalanceType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> BusinessOfficeGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.BusinessOffice);
            }
        }

        public List<SelectListItem> CashCreditLoanDocumentDropdownList
        {
            get
            {
                // Get Cash Credit Loan Type Document PrmKey
                byte cashCreditDocumentTypePrmKey = GetCashCreditDocumentTypePrmKey();

                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.DocumentDocumentTypes
                            join dt in context.DocumentTypes.Where(dt => dt.ActivationStatus == StringLiteralValue.Active) on d.DocumentTypePrmKey equals dt.PrmKey into ddt
                            from dt in ddt.DefaultIfEmpty()
                            join dm in context.Documents.Where(dm => dm.ActivationStatus == StringLiteralValue.Active) on d.DocumentPrmKey equals dm.PrmKey into ddm
                            from dm in ddm.DefaultIfEmpty()
                            join t in context.DocumentTranslations on dm.PrmKey equals t.DocumentPrmKey into dmt
                            from t in dmt.DefaultIfEmpty()
                            where (d.EntryStatus == StringLiteralValue.Verify)
                            && (dt.PrmKey == cashCreditDocumentTypePrmKey)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = dm.DocumentId.ToString(),
                                Text = (dm.NameOfDocument.Trim() + " ---> " + (t.TransNameOfDocument.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from d in context.DocumentDocumentTypes
                        join dt in context.DocumentTypes.Where(dt => dt.ActivationStatus == StringLiteralValue.Active) on d.DocumentTypePrmKey equals dt.PrmKey into ddt
                        from dt in ddt.DefaultIfEmpty()
                        join dm in context.Documents.Where(dm => dm.ActivationStatus == StringLiteralValue.Active) on d.DocumentPrmKey equals dm.PrmKey into ddm
                        from dm in ddm.DefaultIfEmpty()
                        where (d.EntryStatus == StringLiteralValue.Verify)
                        && (dt.PrmKey == cashCreditDocumentTypePrmKey)
                        select new SelectListItem
                        {
                            Value = dm.DocumentId.ToString(),
                            Text = dm.NameOfDocument.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> ChargesTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.ChargesTypes
                            join t in context.ChargesTypeTranslations on d.PrmKey equals t.ChargesTypePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (d.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = d.ChargesTypeId.ToString(),
                                Text = (d.NameOfChargesType.Trim() + " ---> " + (t.TransNameOfChargesType.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.ChargesTypes
                        where (d.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = d.ChargesTypeId.ToString(),
                            Text = d.NameOfChargesType.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> ChequeBookDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.ChequeBookMasters
                            where (c.EntryStatus == (StringLiteralValue.Verify))
                            select new SelectListItem
                            {
                                Value = c.ChequeBookMasterId.ToString(),
                                Text = c.ChequeBookNumber.ToString()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from c in context.ChequeBookMasters
                        where (c.EntryStatus == (StringLiteralValue.Verify))
                        select new SelectListItem
                        {
                            Value = c.ChequeBookMasterId.ToString(),
                            Text = c.ChequeBookNumber.ToString()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> ChequeReturnReasonDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.ChequeReturnReasons
                            join t in context.ChequeReturnReasonTranslations on c.PrmKey equals t.ChequeReturnReasonPrmKey into ht
                            from t in ht.DefaultIfEmpty()
                            where (c.ActivationStatus == StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = c.ChequeReturnReasonId.ToString(),
                                Text = (c.NameOfChequeReturnReason.Trim() + " ---> " + (t.TransNameOfChequeReturnReason.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from c in context.ChequeReturnReasons
                        where (c.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = c.ChequeReturnReasonId.ToString(),
                            Text = (c.NameOfChequeReturnReason.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> ConsumerDurableItemDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from v in context.ConsumerDurableItems
                            join mf in context.ConsumerDurableItemModifications on v.PrmKey equals mf.ConsumerDurableItemPrmKey into vm
                            from mf in vm.DefaultIfEmpty()
                            join t in context.ConsumerDurableItemTranslations on v.PrmKey equals t.ConsumerDurableItemPrmKey into vt
                            from t in vt.DefaultIfEmpty()
                            where (v.EntryStatus == (StringLiteralValue.Verify)
                                    && (mf.EntryStatus == (StringLiteralValue.Verify) || mf.EntryStatus == null)
                                    && (t.EntryStatus == (StringLiteralValue.Verify) || t.EntryStatus == null)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = v.ConsumerDurableItemId.ToString(),
                                Text = (mf.NameOfItem ?? v.NameOfItem.Trim()) + " ---> " + (t.TransNameOfItem.Trim() ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from v in context.ConsumerDurableItems
                        join mf in context.ConsumerDurableItemModifications on v.PrmKey equals mf.ConsumerDurableItemPrmKey into vm
                        from mf in vm.DefaultIfEmpty()
                        where (v.EntryStatus == (StringLiteralValue.Verify)
                                && (mf.EntryStatus == (StringLiteralValue.Verify) || mf.EntryStatus == null))
                        select new SelectListItem
                        {
                            Value = v.ConsumerDurableItemId.ToString(),
                            Text = ((mf.NameOfItem) ?? v.NameOfItem.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> CreditBureauAgencyDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from c in context.CreditBureauAgencies
                            join t in context.CreditBureauAgencyTranslations on c.PrmKey equals t.CreditBureauAgencyPrmKey
                            where c.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = c.CreditBureauAgencyId.ToString(),
                                Text = c.NameOfCreditBureauAgency.Trim() + " ---> " + t.TransNameOfCreditBureauAgency.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.CreditBureauAgencies
                        where c.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = c.CreditBureauAgencyId.ToString(),
                            Text = c.NameOfCreditBureauAgency
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> CurrencyDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Currencies
                            join t in context.CurrencyTranslations on c.PrmKey equals t.CurrencyPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = c.CurrencyId.ToString(),
                                Text = (c.NameOfCurrency.Trim() + " ---> " + (t.TransNameOfCurrency.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from c in context.Currencies
                        where (c.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = c.CurrencyId.ToString(),
                            Text = (c.NameOfCurrency.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> CustomerTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.CustomerTypes
                            join t in context.CustomerTypeTranslations on c.PrmKey equals t.CustomerTypePrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = c.CustomerTypeId.ToString(),
                                Text = (c.NameOfCustomerType.Trim() + " ---> " + ((t.TransNameOfCustomerType.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from c in context.CustomerTypes
                        where (c.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = c.CustomerTypeId.ToString(),
                            Text = (c.NameOfCustomerType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
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
                             where (c.EntryStatus == (StringLiteralValue.Verify))
                             && (c.ActivationStatus == StringLiteralValue.Active)
                             && (cd.EntryStatus == (StringLiteralValue.Verify) || cd.EntryStatus == null)
                             //&& (cd.PersonPrmKey.Equals(personPrmKey))
                             //&& (c.PrmKey.Equals(cd.CustomerAccountPrmKey))

                             || (c.EntryStatus == (StringLiteralValue.Verify))
                             && (c.ActivationStatus == StringLiteralValue.Active)
                             && (cd.EntryStatus == (StringLiteralValue.Verify) || cd.EntryStatus == null)
                             //&& (cd.PersonPrmKey.Equals(personPrmKey))
                             //&& (c.PrmKey.Equals(cd.CustomerAccountPrmKey))
                             && (c.IsModified == (false))

                             select new SelectListItem
                             {
                                 Value = c.CustomerAccountId.ToString(),
                                 Text = c.AccountNumber.ToString() /*((mf.NameOfCenter == null) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter == null ? " " : t.TransNameOfCenter.Trim()) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter == null ? " " : t.TransNameOfCenter.Trim()))*/
                             }).Distinct().OrderBy(l => l.Text).ToList();
                    return a;
                }

                var b = (from c in context.CustomerAccounts
                         join cd in context.CustomerAccountDetails on c.PrmKey equals cd.CustomerAccountPrmKey into cad
                         from cd in cad.DefaultIfEmpty()
                         where (c.EntryStatus == (StringLiteralValue.Verify))
                         && (c.ActivationStatus == StringLiteralValue.Active)
                         && (cd.EntryStatus == (StringLiteralValue.Verify) || cd.EntryStatus == null)
                         //&& (cd.PersonPrmKey.Equals(personPrmKey))
                         && (c.PrmKey == (cd.CustomerAccountPrmKey))

                         || (c.EntryStatus == (StringLiteralValue.Verify))
                         && (c.ActivationStatus == StringLiteralValue.Active)
                         && (cd.EntryStatus == (StringLiteralValue.Verify) || cd.EntryStatus == null)
                         //&& (cd.PersonPrmKey.Equals(personPrmKey))
                         && (c.PrmKey == (cd.CustomerAccountPrmKey))
                         && (c.IsModified == (false))

                         select new SelectListItem
                         {
                             Value = c.CustomerAccountId.ToString(),
                             Text = cd.CustomerAccountPrmKey.ToString()
                         }).Distinct().OrderBy(l => l.Text).ToList();
                return b;
            }
        }

        public List<SelectListItem> DaysInYearDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from d in context.DaysInYears
                            join t in context.DaysInYearTranslations on d.PrmKey equals t.DaysInYearPrmKey
                            where (d.ActivationStatus == StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = d.DaysInYearId.ToString(),
                                Text = d.Title.Trim() + " ---> " + t.TransTitle.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from d in context.DaysInYears
                        where (d.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = d.DaysInYearId.ToString(),
                            Text = d.Title.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

      
        //public List<SelectListItem> CashCreditGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.CashCreditLoan);
        //    }
        //}

        public List<SelectListItem> EducationalLoanGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.EducationalLoan);
            }
        }

        //public List<SelectListItem> GoldLoanGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.GoldLoan);
        //    }
        //}

        //public List<SelectListItem> ConsumerDurableLoanGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.ConsumerDurableLoan);
        //    }
        //}

        //public List<SelectListItem> GuarantorLoanGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.GuarantorLoan);
        //    }
        //}

        //public List<SelectListItem> HomeLoanGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.HomeLoan);
        //    }
        //}

        //public List<SelectListItem> LoanAgainstFixedDepositGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.LoanAgainstDeposit);
        //    }
        //}

        //public List<SelectListItem> LoanAgainstPropertyGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.LoanAgainstProperty);
        //    }
        //}

        //public List<SelectListItem> VehicleLoanGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.VehicleLoan);
        //    }
        //}

        //public List<SelectListItem> BusinessLoanGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.ShortTermBusinessLoan);
        //    }
        //}

        public List<SelectListItem> DemandDepositAccountHolderDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from s in context.SchemeDemandDepositDetails
                            join d in context.CustomerAccountDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on s.SchemePrmKey equals d.SchemePrmKey into sd
                            from d in sd.DefaultIfEmpty()
                            join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify) on d.PersonPrmKey equals p.PrmKey into dp
                            from p in dp.DefaultIfEmpty()
                            join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
                            from m in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify && t.LanguagePrmKey == regionalLanguagePrmKey) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            join c in context.CustomerAccounts.Where(c => c.ActivationStatus == StringLiteralValue.Active && c.EntryStatus == StringLiteralValue.Verify) on d.CustomerAccountPrmKey equals c.PrmKey into dc
                            from c in dc.DefaultIfEmpty()
                            where (s.EntryStatus == StringLiteralValue.Verify)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = c.CustomerAccountId.ToString(),
                                Text = (m.FullName ?? p.FullName.Trim()) + " ---> " + ((t.TransFullName.Trim()) ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from s in context.SchemeDemandDepositDetails
                        join d in context.CustomerAccountDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on s.SchemePrmKey equals d.SchemePrmKey into sd
                        from d in sd.DefaultIfEmpty()
                        join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify) on d.PersonPrmKey equals p.PrmKey into dp
                        from p in dp.DefaultIfEmpty()
                        join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
                        from m in pm.DefaultIfEmpty()
                        join c in context.CustomerAccounts.Where(c => c.ActivationStatus == StringLiteralValue.Active && c.EntryStatus == StringLiteralValue.Verify) on d.CustomerAccountPrmKey equals c.PrmKey into dc
                        from c in dc.DefaultIfEmpty()
                        where (s.EntryStatus == StringLiteralValue.Verify)
                        select new SelectListItem
                        {
                            Value = c.CustomerAccountId.ToString(),
                            Text = m.FullName ?? p.FullName.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> DepositSchemeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from sc in context.SchemeDepositAccountParameters
                            join s in context.Schemes.Where(s => s.ActivationStatus == StringLiteralValue.Active && s.EntryStatus == StringLiteralValue.Verify) on sc.SchemePrmKey equals s.PrmKey into scs
                            from s in scs.DefaultIfEmpty()
                            join t in context.SchemeTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on s.PrmKey equals t.SchemePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (sc.EntryStatus == (StringLiteralValue.Verify))
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = s.SchemeId.ToString(),
                                Text = (s.NameOfScheme.Trim() + " ---> " + (t.TransNameOfScheme.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from sc in context.SchemeDepositAccountParameters
                        join s in context.Schemes.Where(s => s.ActivationStatus == StringLiteralValue.Active && s.EntryStatus == StringLiteralValue.Verify) on sc.SchemePrmKey equals s.PrmKey into scs
                        from s in scs.DefaultIfEmpty()
                        where (s.EntryStatus == (StringLiteralValue.Verify))
                        && (s.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = s.SchemeId.ToString(),
                            Text = (s.NameOfScheme.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> DepositGeneralLedgerAvailableForPledgeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from s in context.SchemeDepositAccountParameters
                            join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on s.SchemePrmKey equals gs.SchemePrmKey into sg
                            from gs in sg.DefaultIfEmpty()
                            join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gsg
                            from g in gsg.DefaultIfEmpty()
                            join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                            from mf in hm.DefaultIfEmpty()
                            join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals t.GeneralLedgerPrmKey into ht
                            from t in ht.DefaultIfEmpty()
                            where (s.DepositType != "DMN" || s.DepositType != "PPF")
                            &&  (s.IsAvailablePledgeLoan == true)
                            && (s.EntryStatus == StringLiteralValue.Verify)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = g.GeneralLedgerId.ToString(),
                                Text = (mf.NameOfGL ?? g.NameOfGL.Trim()) + " ---> " + ((t.TransNameOfGL.Trim()) ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from s in context.SchemeDepositAccountParameters
                        join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on s.SchemePrmKey equals gs.SchemePrmKey into sg
                        from gs in sg.DefaultIfEmpty()
                        join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gsg
                        from g in gsg.DefaultIfEmpty()
                        join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                        from mf in hm.DefaultIfEmpty()
                        where (s.DepositType != "DMN" || s.DepositType != "PPF")
                        && (s.EntryStatus == StringLiteralValue.Verify)
                        && (s.IsAvailablePledgeLoan == true)
                        select new SelectListItem
                        {
                            Value = g.GeneralLedgerId.ToString(),
                            Text = ((mf.NameOfGL.Trim()) ?? g.NameOfGL.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

        }

        public List<SelectListItem> CustomerAccountTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1) 
                if (regionalLanguagePrmKey != 1)
                {
                    return (from b in context.CustomerAccountTypes
                            where (b.ActivationStatus == StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = b.CustomerAccountTypeId.ToString(),
                                Text = (b.NameOfCustomerAccountType.Trim()),
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from b in context.CustomerAccountTypes
                        where (b.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = b.CustomerAccountTypeId.ToString(),
                            Text = (b.NameOfCustomerAccountType.Trim()),
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> DividendCalculationMethodDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.DividendCalculationMethods
                            join t in context.DividendCalculationMethodTranslations on d.PrmKey equals t.DividendCalculationMethodPrmKey
                            where d.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = d.DividendCalculationMethodId.ToString(),
                                Text = d.NameOfMethod.Trim() + " ---> " + t.TransNameOfMethod.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from d in context.DividendCalculationMethods
                        where d.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = d.DividendCalculationMethodId.ToString(),
                            Text = d.NameOfMethod.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> FinancialAssetTypeDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.FinancialAssetTypes
                            join t in context.FinancialAssetTypeTranslations on r.PrmKey equals t.FinancialAssetTypePrmKey
                            where r.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = r.FinancialAssetTypeId.ToString(),
                                Text = r.NameOfFinancialAssetType.Trim() + " ---> " + t.TransNameOfFinancialAsset.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from r in context.FinancialAssetTypes
                        where r.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = r.FinancialAssetTypeId.ToString(),
                            Text = r.NameOfFinancialAssetType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> FineInterestReceivedOnLoanGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.FineInterestReceivedOnLoan);
            }
        }

        public List<SelectListItem> FrequencyDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from f in context.Frequencies
                            join t in context.FrequencyTranslations on f.PrmKey equals t.FrequencyPrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (f.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = f.FrequencyId.ToString(),
                                Text = (f.NameOfFrequency.Trim() + " ---> " + (t.TransNameOfFrequency.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from f in context.Frequencies
                        where (f.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = f.FrequencyId.ToString(),
                            Text = f.NameOfFrequency.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> FundDropdownList
        {
            get
            {
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                if (regionalLanguagePrmKey != 1)
                {
                    return (from f in context.Funds
                            join t in context.FundTranslations on f.PrmKey equals t.FundPrmKey into ft
                            from t in ft.DefaultIfEmpty()
                            where (f.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = f.FundId.ToString(),
                                Text = (f.NameOfFund.Trim() + " ---> " + (t.TransNameOfFund.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from f in context.Funds
                        where (f.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = f.FundId.ToString(),
                            Text = f.NameOfFund
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> FundTransferFrequencyDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from f in context.FundTransferFrequencies
                            join t in context.FundTransferFrequencyTranslations on f.PrmKey equals t.FundTransferFrequencyPrmKey into ft
                            from t in ft.DefaultIfEmpty()
                            where (f.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = f.FundTransferFrequencyId.ToString(),
                                Text = (f.NameOfFundTransferFrequency.Trim() + " ---> " + (t.TransNameOfFundTransferFrequency.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from f in context.FundTransferFrequencies
                        where (f.ActivationStatus == StringLiteralValue.Active)
                                && (f.ActivationStatus == StringLiteralValue.Active)
                                && (f.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = f.FundTransferFrequencyId.ToString(),
                            Text = f.NameOfFundTransferFrequency.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> FurnitureAssetTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from f in context.FurnitureAssetTypes
                            join t in context.FurnitureAssetTypeTranslations on f.PrmKey equals t.FurnitureAssetTypePrmKey into ft
                            from t in ft.DefaultIfEmpty()
                            where (f.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = f.FurnitureAssetTypeId.ToString(),
                                Text = (f.NameOfFurnitureAssetType.Trim() + " ---> " + (t.TransNameOfFurnitureAssetType.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from f in context.FurnitureAssetTypes
                        where (f.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = f.FurnitureAssetTypeId.ToString(),
                            Text = (f.NameOfFurnitureAssetType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> GeneralLedgerDropdownList
        {
            get
            {
                return (from e in context.GeneralLedgers

                        select new SelectListItem
                        {
                            Value = e.GeneralLedgerId.ToString(),
                            Text = e.NameOfGL
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> GLParentDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from g in context.GeneralLedgers
                            join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into gm
                            from mf in gm.DefaultIfEmpty()
                            join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals t.GeneralLedgerPrmKey into ht
                            from t in ht.DefaultIfEmpty()
                            where (g.EntryStatus == (StringLiteralValue.Verify))
                                    && (g.ActivationStatus == StringLiteralValue.Active)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = g.GeneralLedgerId.ToString(),
                                Text = (mf.NameOfGL ?? g.NameOfGL.Trim()) + " ---> " + (t.TransNameOfGL.Trim() ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from g in context.GeneralLedgers
                        join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into gm
                        from mf in gm.DefaultIfEmpty()
                        join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals t.GeneralLedgerPrmKey into ht
                        from t in ht.DefaultIfEmpty()
                        where (g.EntryStatus == (StringLiteralValue.Verify))
                                && (g.ActivationStatus == StringLiteralValue.Active)
                                && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = g.GeneralLedgerId.ToString(),
                            Text = ((mf.NameOfGL) ?? g.NameOfGL.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> InterestPaidOnDepositGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.InterestPaidOnDeposit);
            }
        }

        public List<SelectListItem> InterestReceivedOnLoanGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.InterestReceivedOnLoan);
            }
        }

        public List<SelectListItem> AgentCommissionGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.AgentCommission);
            }
        }

        public List<SelectListItem> DepositInterestProvisionGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.DepositInterestProvision);
            }
        }

        public List<SelectListItem> LoanInterestProvisonGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.LoanInterestProvision);
            }
        }

        public List<SelectListItem> GoldOrnamentDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from g in context.GoldOrnaments
                            join mf in context.GoldOrnamentModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GoldOrnamentPrmKey into gm
                            from mf in gm.DefaultIfEmpty()
                            join t in context.GoldOrnamentTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals t.GoldOrnamentPrmKey into gt
                            from t in gt.DefaultIfEmpty()
                            where (g.EntryStatus == (StringLiteralValue.Verify))
                            && (g.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = g.GoldOrnamentId.ToString(),
                                Text = (mf.NameOfGoldOrnament ?? g.NameOfGoldOrnament.Trim()) + " ---> " + ((t.TransNameOfGoldOrnament.Trim()) ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from g in context.GoldOrnaments
                        join mf in context.GoldOrnamentModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GoldOrnamentPrmKey into gm
                        from mf in gm.DefaultIfEmpty()
                        where (g.EntryStatus == (StringLiteralValue.Verify))
                        && (g.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = g.GoldOrnamentId.ToString(),
                            Text = ((mf.GoldOrnament == null) ? g.NameOfGoldOrnament.Trim() : mf.NameOfGoldOrnament)
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> InstallmentFrequencyDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from a in context.InstallmentFrequencies
                            join t in context.InstallmentFrequencyTranslations on a.PrmKey equals t.InstallmentFrequencyPrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (a.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = a.InstallmentFrequencyId.ToString(),
                                Text = (a.NameOfFrequency.Trim() + " ---> " + ((t.TransNameOfFrequency.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from a in context.InstallmentFrequencies
                        where (a.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = a.InstallmentFrequencyId.ToString(),
                            Text = (a.NameOfFrequency.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> GSTRegistrationTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from g in context.GSTRegistrationTypes
                            join t in context.GSTRegistrationTypeTranslations on g.PrmKey equals t.GSTRegistrationTypePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (g.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = g.GSTRegistrationTypeId.ToString(),
                                Text = (g.NameOfGSTRegistrationType.Trim() + " ---> " + ((t.TransNameOfGSTRegistrationType.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from g in context.GSTRegistrationTypes
                        where (g.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = g.GSTRegistrationTypeId.ToString(),
                            Text = (g.NameOfGSTRegistrationType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> GSTReturnPeriodicityDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from g in context.GSTReturnPeriodicities
                            join t in context.GSTReturnPeriodicityTranslations on g.PrmKey equals t.GSTReturnPeriodicityPrmKey into gt
                            from t in gt.DefaultIfEmpty()
                            where (g.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = g.GSTReturnPeriodicityId.ToString(),
                                Text = (g.NameOfGSTReturnPeriodicity.Trim() + " ---> " + ((t.TransNameOfGSTReturnPeriodicity.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from g in context.GSTReturnPeriodicities
                        where (g.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = g.GSTReturnPeriodicityId.ToString(),
                            Text = (g.NameOfGSTReturnPeriodicity.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> InterestCalculationFrequencyDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from f in context.InterestCalculationFrequencies
                            join t in context.InterestCalculationFrequencyTranslations on f.PrmKey equals t.InterestCalculationFrequencyPrmKey into ft
                            from t in ft.DefaultIfEmpty()
                            where ((f.ActivationStatus == StringLiteralValue.Active)
                                    || (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = f.InterestCalculationFrequencyId.ToString(),
                                Text = (f.NameOfFrequency.Trim() + " ---> " + ((t.TransNameOfFrequency.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from f in context.InterestCalculationFrequencies
                        where (f.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = f.InterestCalculationFrequencyId.ToString(),
                            Text = (f.NameOfFrequency)
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> InterestCompoundingFrequencyDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from f in context.InterestCompoundingFrequencies
                            join t in context.InterestCompoundingFrequencyTranslations on f.PrmKey equals t.InterestCompoundingFrequencyPrmKey into ft
                            from t in ft.DefaultIfEmpty()
                            where ((f.ActivationStatus == StringLiteralValue.Active)
                                    || (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = f.InterestCompoundingFrequencyId.ToString(),
                                Text = (f.NameOfFrequency.Trim() + " ---> " + ((t.TransNameOfFrequency.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from f in context.InterestCompoundingFrequencies
                        where (f.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = f.InterestCompoundingFrequencyId.ToString(),
                            Text = (f.NameOfFrequency)
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> InterestRateChargedDurationDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    // Default List In Defaul Language (i.e. English)
                    return (from i in context.InterestRateChargedDurations
                            join t in context.InterestRateChargedDurationTranslations on i.PrmKey equals t.InterestRateChargedDurationPrmKey into it
                            from t in it.DefaultIfEmpty()
                            where i.ActivationStatus == StringLiteralValue.Active
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = i.InterestRateChargedDurationId.ToString(),
                                Text = (i.Title.Trim() + " ---> " + ((t.TransTitle.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from i in context.InterestRateChargedDurations
                        where i.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = i.InterestRateChargedDurationId.ToString(),
                            Text = i.Title.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> InterestRateTypeDropdownList
        {
            get
            {
                // Default List In Defaul Language (i.e. English)
                return (from i in context.InterestRateTypes
                        where i.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = i.InterestRateTypeId.ToString(),
                            Text = i.NameOfInterestRateType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> InterestRebateCriteriaDropdownList
        {
            get
            {
                //// If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                //if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                //{
                //    return (from i in context.InterestRebateCriterias
                //            join t in context.InterestRebateCriteriaTranslations on i.PrmKey equals t.InterestRebateCriteriaPrmKey
                //            where (i.ActivationStatus == StringLiteralValue.Active)
                //            select new SelectListItem
                //            {
                //                Value = i.InterestRebateCriteriaId.ToString(),
                //                Text = i.SysNameOfCriteria.Trim() + " ---> " + t.TransSysNameOfCriteria.Trim()
                //            }).Distinct().OrderBy(l => l.Text).ToList();

                //}

                // Default List In Default Language (i.e. English)
                return (from d in context.InterestRebateCriterias
                        where (d.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = d.InterestRebateCriteriaId.ToString(),
                            Text = d.SysNameOfCriteria.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> InterestTypeDropdownList
        {
            get
            {
                // Default List In Defaul Language (i.e. English)
                return (from i in context.InterestTypes
                        where i.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = i.InterestTypeId.ToString(),
                            Text = i.NameOfInterestType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> InterestMethodDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.InterestMethods
                            join t in context.InterestMethodTranslations on d.PrmKey equals t.InterestMethodPrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (d.ActivationStatus == StringLiteralValue.Active)
                                    || (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = d.InterestMethodId.ToString(),
                                Text = (d.NameOfInterestMethod + " ---> " + t.TransNameOfMethod ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.InterestMethods
                        where (d.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = d.InterestMethodId.ToString(),
                            Text = d.NameOfInterestMethod.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> InterestRebateApplyFrequencyDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from i in context.InterestRebateApplyFrequencies
                            join t in context.InterestRebateApplyFrequencyTranslations on i.PrmKey equals t.InterestRebateApplyFrequencyPrmKey into it
                            from t in it.DefaultIfEmpty()
                            where (i.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = i.InterestRebateApplyFrequencyId.ToString(),
                                Text = (i.NameOfFrequency.Trim() + " ---> " + ((t.TransNameOfFrequency.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from i in context.InterestRebateApplyFrequencies
                        where (i.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = i.InterestRebateApplyFrequencyId.ToString(),
                            Text = i.NameOfFrequency.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> JointAccountHolderTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from j in context.JointAccountHolderTypes
                            join t in context.JointAccountHolderTypeTranslations on j.PrmKey equals t.JointAccountHolderTypePrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            where (j.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = j.JointAccountHolderTypeId.ToString(),
                                Text = (j.NameOfJointAccountHolderType.Trim() + " ---> " + (t.TransNameOfJointAccountHolderType.Trim() ?? "")),
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from j in context.JointAccountHolderTypes
                        where (j.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = j.JointAccountHolderTypeId.ToString(),
                            Text = j.NameOfJointAccountHolderType.Trim(),
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> LendingChargesBaseDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from l in context.LendingChargesBases
                            join c in context.ChargesBases.Where(c => c.ActivationStatus == StringLiteralValue.Active) on l.ChargesBasePrmKey equals c.PrmKey
                            join t in context.ChargesBaseTranslations on c.PrmKey equals t.ChargesBasePrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            join s in context.LendingChargesBases.Where(s => s.ActivationStatus == StringLiteralValue.Active) on c.PrmKey equals s.ChargesBasePrmKey into st
                            from s in st.DefaultIfEmpty()
                            where (c.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            && (s.ActivationStatus == StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = s.LendingChargesBaseId.ToString(),
                                Text = (c.NameChargesBase.Trim() + " ---> " + ((t.TransNameOfChargesType.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from l in context.LendingChargesBases
                        join c in context.ChargesBases.Where(c => c.ActivationStatus == StringLiteralValue.Active) on l.ChargesBasePrmKey equals c.PrmKey
                        join t in context.ChargesBaseTranslations on c.PrmKey equals t.ChargesBasePrmKey into ct
                        from t in ct.DefaultIfEmpty()
                        join s in context.LendingChargesBases.Where(s => s.ActivationStatus == StringLiteralValue.Active) on c.PrmKey equals s.ChargesBasePrmKey into st
                        from s in st.DefaultIfEmpty()
                        where (l.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = l.LendingChargesBaseId.ToString(),
                            Text = c.NameChargesBase
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> LendingInterestMethodDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {

                    return (from l in context.LendingInterestMethods
                            join i in context.InterestMethods.Where(i => i.ActivationStatus == StringLiteralValue.Active) on l.InterestMethodPrmKey equals i.PrmKey into li
                            from i in li.DefaultIfEmpty()
                            join t in context.InterestMethodTranslations on i.PrmKey equals t.InterestMethodPrmKey into it
                            from t in it.DefaultIfEmpty()
                            where (l.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = l.LendingInterestMethodId.ToString(),
                                Text = (i.NameOfInterestMethod.Trim() + " ---> " + t.TransNameOfMethod ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();

                }

                // Default List In Default Language (i.e. English)
                return (from l in context.LendingInterestMethods
                        join i in context.InterestMethods.Where(i => i.ActivationStatus == StringLiteralValue.Active) on l.InterestMethodPrmKey equals i.PrmKey into li
                        from i in li.DefaultIfEmpty()
                        where (l.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = l.LendingInterestMethodId.ToString(),
                            Text = i.NameOfInterestMethod
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> LendingInterestPostingFrequencyDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from d in context.LendingInterestPostingFrequencies
                            join t in context.LendingInterestPostingFrequencyTranslations on d.PrmKey equals t.LendingInterestPostingFrequencyPrmKey
                            where (d.ActivationStatus == StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = d.LendingInterestPostingFrequencyId.ToString(),
                                Text = d.NameOfFrequency.Trim() + " ---> " + t.TransNameOfFrequency.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from d in context.LendingInterestPostingFrequencies
                        where (d.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = d.LendingInterestPostingFrequencyId.ToString(),
                            Text = d.NameOfFrequency.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> LendingRepaymentsInterestCalculationDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.LendingRepaymentsInterestCalculations
                            join t in context.LendingRepaymentsInterestCalculationTranslations on d.PrmKey equals t.LendingRepaymentsInterestCalculationPrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (d.ActivationStatus == StringLiteralValue.Active)
                                    || (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = d.LendingRepaymentsInterestCalculationId.ToString(),
                                Text = (d.NameOfEvent.Trim() + " ---> " + ((t.TransNameOfEvent.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.LendingRepaymentsInterestCalculations
                        where (d.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = d.LendingRepaymentsInterestCalculationId.ToString(),
                            Text = d.NameOfEvent.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> LoanChargesGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.LoanCharges);
            }
        }

        // ********************************* UPDATE
        //public List<SelectListItem> LoanGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
        //        short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

        //        // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
        //        if (regionalLanguagePrmKey != 1)
        //        {
        //            return (from g in context.GeneralLedgers
        //                    join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into gm
        //                    from mf in gm.DefaultIfEmpty()
        //                    join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify && t.LanguagePrmKey == regionalLanguagePrmKey) on g.PrmKey equals t.GeneralLedgerPrmKey into gt
        //                    from t in gt.DefaultIfEmpty()
        //                    join a in context.AccountClasses.Where(a => a.AccountClassCode.Contains(StringLiteralValue.LoanAccountClassCodes)) on g.AccountClassPrmKey equals a.PrmKey into ag
        //                    from a in ag.DefaultIfEmpty()
        //                    where (g.EntryStatus == StringLiteralValue.Verify)
        //                            && (g.ActivationStatus == StringLiteralValue.Active)
        //                    select new SelectListItem
        //                    {
        //                        Value = g.GeneralLedgerId.ToString(),
        //                        Text = ((mf.NameOfGL == null) ? g.NameOfGL.Trim() + " ---> " + (t.TransNameOfGL == null ? " " : t.TransNameOfGL.Trim()) : mf.NameOfGL + " ---> " + (t.TransNameOfGL == null ? " " : t.TransNameOfGL.Trim()))
        //                    }).Distinct().OrderBy(l => l.Text).ToList();
        //        }

        //        // Default List In Default Language (i.e. English)
        //        return (from g in context.GeneralLedgers
        //                join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into gm
        //                from mf in gm.DefaultIfEmpty()
        //                join a in context.AccountClasses.Where(a => a.AccountClassCode == StringLiteralValue.Loan) on g.AccountClassPrmKey equals a.PrmKey into ag
        //                from a in ag.DefaultIfEmpty()
        //                where (g.EntryStatus == StringLiteralValue.Verify)
        //                && (g.ActivationStatus == StringLiteralValue.Active)
        //                select new SelectListItem
        //                {
        //                    Value = g.GeneralLedgerId.ToString(),
        //                    Text = mf.NameOfGL == null ? g.NameOfGL.Trim() : mf.NameOfGL
        //                }).Distinct().OrderBy(l => l.Text).ToList();

        //    }
        //}

        public List<SelectListItem> InterestRebateGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.InterestRebate);
            }
        }

        public List<SelectListItem> LoanRecoveryActionDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from l in context.LoanRecoveryActions
                            join t in context.LoanRecoveryActionTranslations on l.PrmKey equals t.LoanRecoveryActionPrmKey into lt
                            from t in lt.DefaultIfEmpty()
                            where (l.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = l.LoanRecoveryActionId.ToString(),
                                Text = (l.NameOfLoanRecoveryAction.Trim() + " ---> " + ((t.TransNameOfLoanRecoveryAction.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from l in context.LoanRecoveryActions
                        where (l.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = l.LoanRecoveryActionId.ToString(),
                            Text = (l.NameOfLoanRecoveryAction.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> LoanReasonDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from l in context.LoanReasons
                            join t in context.LoanReasonTranslations on l.PrmKey equals t.LoanReasonPrmKey into lt
                            from t in lt.DefaultIfEmpty()
                            where (l.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = l.LoanReasonId.ToString(),
                                Text = (l.NameOfLoanReason.Trim() + " ---> " + ((t.TransNameOfLoanReason.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from l in context.LoanReasons
                        where (l.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = l.LoanReasonId.ToString(),
                            Text = l.NameOfLoanReason.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> LoanTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    // Default List In Defaul Language (i.e. English)
                    return (from l in context.LoanTypes
                            join t in context.LoanTypeTranslations on l.PrmKey equals t.LoanTypePrmKey into lt
                            from t in lt.DefaultIfEmpty()
                            where (l.ActivationStatus == StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = l.LoanTypeId.ToString(),
                                Text = (l.NameOfLoanType.Trim() + " ---> " + ((t.TransNameOfLoanType.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from l in context.LoanTypes
                        where (l.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = l.LoanTypeId.ToString(),
                            Text = l.NameOfLoanType.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> OrganizationLoanTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    // Default List In Defaul Language (i.e. English)
                    return (from l in context.LoanTypes
                            join t in context.LoanTypeTranslations on l.PrmKey equals t.LoanTypePrmKey into lt
                            from t in lt.DefaultIfEmpty()
                            join o in context.OrganizationLoanTypes on l.PrmKey equals o.LoanTypePrmKey

                            //join a in context.OrganizationLoanTypeTranslations on o.PrmKey equals a.OrganizationLoanTypePrmKey into ah
                            //from a in ah.DefaultIfEmpty()

                            where (o.EntryStatus == (StringLiteralValue.Verify))
                            select new SelectListItem
                            {
                                Value = l.LoanTypeId.ToString(),
                                Text = (l.NameOfLoanType.Trim() + " ---> " + ((t.TransNameOfLoanType.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from l in context.LoanTypes
                        join o in context.OrganizationLoanTypes on l.PrmKey equals o.LoanTypePrmKey into ht
                        from o in ht.DefaultIfEmpty()
                        where (o.EntryStatus == (StringLiteralValue.Verify))
                        select new SelectListItem
                        {
                            Value = l.LoanTypeId.ToString(),
                            Text = l.NameOfLoanType.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> MemberTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from m in context.MemberTypes
                            join t in context.MemberTypeTranslations on m.PrmKey equals t.MemberTypePrmKey into mt
                            from t in mt.DefaultIfEmpty()
                            where (m.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = m.MemberTypeId.ToString(),
                                Text = (m.NameOfMemberType.Trim() + " ---> " + ((t.TransNameOfMemberType.Trim()) ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from m in context.MemberTypes
                        where (m.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = m.MemberTypeId.ToString(),
                            Text = m.NameOfMemberType.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> MinuteOfMeetingAgendaDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                int meetingPrmKey = (from m in context.Meetings
                                     where (m.EntryStatus == StringLiteralValue.Verify)
                                     select (m.PrmKey)
                                    ).FirstOrDefault();

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from a in context.Agendas
                            join mf in context.AgendaModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on a.PrmKey equals mf.AgendaPrmKey into amf
                            from mf in amf.DefaultIfEmpty()
                            join t in context.AgendaTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on a.PrmKey equals t.AgendaPrmKey into at
                            from t in at.DefaultIfEmpty()
                            join ma in context.MeetingAgendas.Where(ma => ma.EntryStatus == StringLiteralValue.Verify) on a.PrmKey equals ma.AgendaPrmKey into ama
                            from ma in ama.DefaultIfEmpty()
                            join mm in context.MinuteOfMeetingAgendas.Where(mm => mm.EntryStatus == StringLiteralValue.Verify) on ma.PrmKey equals mm.MeetingAgendaPrmKey into mma
                            from mm in mma.DefaultIfEmpty()
                            where (a.ActivationStatus == StringLiteralValue.Active)
                                    && (a.EntryStatus == StringLiteralValue.Verify)
                                    && (ma.MeetingPrmKey == meetingPrmKey)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = mm.MinuteOfMeetingAgendaId.ToString(),
                                Text = (mf.NameOfAgenda ?? a.NameOfAgenda.Trim()) + " ---> " + (t.TransNameOfAgenda.Trim() ?? "")
                            }).ToList();
                }

                return (from a in context.Agendas
                        join mf in context.AgendaModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on a.PrmKey equals mf.AgendaPrmKey into amf
                        from mf in amf.DefaultIfEmpty()
                        join ma in context.MeetingAgendas.Where(ma => ma.EntryStatus == StringLiteralValue.Verify) on a.PrmKey equals ma.AgendaPrmKey into ama
                        from ma in ama.DefaultIfEmpty()
                        join mm in context.MinuteOfMeetingAgendas.Where(mm => mm.EntryStatus == StringLiteralValue.Verify) on ma.PrmKey equals mm.MeetingAgendaPrmKey into mma
                        from mm in mma.DefaultIfEmpty()
                        where (a.ActivationStatus == StringLiteralValue.Active)
                                && (a.EntryStatus == StringLiteralValue.Verify)
                                && (ma.MeetingPrmKey == meetingPrmKey)
                        select new SelectListItem
                        {
                            Value = mm.MinuteOfMeetingAgendaId.ToString(),
                            Text = ((mf.NameOfAgenda) ?? a.NameOfAgenda.Trim())
                        }).ToList();
            }
        }

        public List<SelectListItem> PayInPayOutModeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from p in context.PayInPayOutModes
                            join t in context.PayInPayOutModeTranslations on p.PrmKey equals t.PayInPayOutModePrmKey into ht
                            from t in ht.DefaultIfEmpty()
                            where (p.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = p.PayInPayOutModeId.ToString(),
                                Text = (p.NameOfPayInPayOutMode.Trim() + " ---> " + (t.TransNameOfPayInPayOutMode.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.PayInPayOutModes
                        where (p.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = p.PayInPayOutModeId.ToString(),
                            Text = p.NameOfPayInPayOutMode.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PaymentCardDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from p in context.PaymentCards
                            join t in context.PaymentCardTranslations on p.PrmKey equals t.PaymentCardPrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (p.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = p.PaymentCardId.ToString(),
                                Text = (p.NameOfPaymentCard.Trim() + " ---> " + (t.TransNameOfPaymentCard.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from p in context.PaymentCards
                        where (p.ActivationStatus == (StringLiteralValue.Verify))
                        select new SelectListItem
                        {
                            Value = p.PaymentCardId.ToString(),
                            Text = p.NameOfPaymentCard.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> RenewTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from r in context.RenewTypes
                            where (r.ActivationStatus == StringLiteralValue.Active && r.PrmKey > 0)
                            select new SelectListItem
                            {
                                Value = r.RenewTypeId.ToString(),
                                Text = (r.NameOfRenewType.Trim())
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from r in context.RenewTypes
                        where (r.ActivationStatus == StringLiteralValue.Active && r.PrmKey > 0)
                        select new SelectListItem
                        {
                            Value = r.RenewTypeId.ToString(),
                            Text = (r.NameOfRenewType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> RepaymentIntervalFrequencyDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.RepaymentIntervalFrequencies
                            join t in context.RepaymentIntervalFrequencyTranslations on r.PrmKey equals t.RepaymentIntervalFrequencyPrmKey
                            where r.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = r.RepaymentIntervalFrequencyId.ToString(),
                                Text = r.NameOfFrequency.Trim() + " ---> " + t.TransNameOfFrequency.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from r in context.RepaymentIntervalFrequencies
                        where r.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = r.RepaymentIntervalFrequencyId.ToString(),
                            Text = r.NameOfFrequency
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SchemeTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from s in context.SchemeTypes
                            join t in context.SchemeTypeTranslations on s.PrmKey equals t.SchemeTypePrmKey into st
                            from t in st.DefaultIfEmpty()
                            where (s.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = s.SchemeTypeId.ToString(),
                                Text = (s.NameOfSchemeType.Trim() + " ---> " + (t.TransNameOfSchemeType.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from s in context.SchemeTypes
                        where (s.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = s.SchemeTypeId.ToString(),
                            Text = (s.NameOfSchemeType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SweepOutFrequencyDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from s in context.SweepOutFrequencies
                            join t in context.SweepOutFrequencyTranslations on s.PrmKey equals t.SweepOutFrequencyPrmKey into st
                            from t in st.DefaultIfEmpty()
                            where (s.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = s.SweepOutFrequencyId.ToString(),
                                Text = (s.NameOfFrequency.Trim() + " ---> " + (t.TransNameOfSweepOutFrequency.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from s in context.SweepOutFrequencies
                        where (s.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = s.SweepOutFrequencyId.ToString(),
                            Text = s.NameOfFrequency.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SharesCapitalSchemeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from sc in context.SchemeSharesCapitalAccountParameters
                            join s in context.Schemes.Where(s => s.ActivationStatus == StringLiteralValue.Active && s.EntryStatus == StringLiteralValue.Verify) on sc.SchemePrmKey equals s.PrmKey into scs
                            from s in scs.DefaultIfEmpty()
                            join t in context.SchemeTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on s.PrmKey equals t.SchemePrmKey into st
                            from t in st.DefaultIfEmpty()
                            where (sc.EntryStatus == StringLiteralValue.Verify)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = s.SchemeId.ToString(),
                                Text = (s.NameOfScheme.Trim() + " ---> " + (t.TransNameOfScheme.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from sc in context.SchemeSharesCapitalAccountParameters
                        join s in context.Schemes.Where(s => s.ActivationStatus == StringLiteralValue.Active && s.EntryStatus == StringLiteralValue.Verify) on sc.SchemePrmKey equals s.PrmKey into scs
                        from s in scs.DefaultIfEmpty()
                        where (s.EntryStatus == StringLiteralValue.Verify)
                        && (s.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = s.SchemeId.ToString(),
                            Text = (s.NameOfScheme.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SharesApplicationDropdownList
        {
            get
            {
                // Default List In Defaul Language (i.e. English)
                return (from s in context.SharesApplications
                        join mf in context.SharesApplicationModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on s.PrmKey equals mf.SharesApplicationPrmKey into sm
                        from mf in sm.DefaultIfEmpty()
                        join sd in context.SharesApplicationDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on s.PrmKey equals sd.SharesApplicationPrmKey into sds
                        from sd in sds.DefaultIfEmpty()
                        join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify) on sd.PersonPrmKey equals p.PrmKey into sdp
                        from p in sdp.DefaultIfEmpty()
                        join pm in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals pm.PersonPrmKey into pmd
                        from pm in pmd.DefaultIfEmpty()
                        join pt in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals pt.PersonPrmKey into ptr
                        from pt in ptr.DefaultIfEmpty()
                        where (s.EntryStatus == StringLiteralValue.Verify
                                && (mf.EntryStatus == StringLiteralValue.Verify || mf.EntryStatus == null))
                        select new SelectListItem
                        {
                            Value = s.ApplicationNumber.ToString(),
                            Text = ((pm.FullName) ?? p.FullName) + "--->" + (pt.TransFullName ?? "") + "--->" + s.ApplicationNumber.ToString()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SharesCapitalGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.SharesCapital);
            }
        }

        public List<SelectListItem> SharesTransferChargesGeneralLedgerDropdownList
        {
            get
            {
                return GetGeneralLedgerDropdownListByAccountClassCode(StringLiteralValue.SharesTransferCharges);
            }
        }

        public List<SelectListItem> TransactionTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from ty in context.TransactionTypes
                            join t in context.TransactionTypeTranslations on ty.PrmKey equals t.TransactionTypePrmKey into tt
                            from t in tt.DefaultIfEmpty()
                            where (ty.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = ty.TransactionTypeId.ToString(),
                                Text = (ty.NameOfTransactionType.Trim() + " ---> " + (t.TransNameOfTransactionType.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from ty in context.TransactionTypes
                        where (ty.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = ty.TransactionTypeId.ToString(),
                            Text = (ty.NameOfTransactionType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> TargetGroupDropdownList
        {
            get
            {

                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from g in context.TargetGroups
                            join t in context.TargetGroupTranslations on g.PrmKey equals t.TargetGroupPrmKey
                            where g.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = g.TargetGroupId.ToString(),
                                Text = g.NameOfTargetGroup.Trim() + " ---> " + t.TransNameOfTargetGroup.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from g in context.TargetGroups
                        where g.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = g.TargetGroupId.ToString(),
                            Text = g.NameOfTargetGroup.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> TenureListDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from ty in context.SchemeTenureLists
                            join t in context.SchemeTenureListTranslations on ty.PrmKey equals t.SchemeTenureListPrmKey into tt
                            from t in tt.DefaultIfEmpty()
                            where (ty.EntryStatus == StringLiteralValue.Verify)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = ty.SchemeTenureListId.ToString(),
                                Text = (ty.TenureText.Trim() + " ---> " + (t.TransTenureText.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from ty in context.SchemeTenureLists
                        where (ty.EntryStatus == StringLiteralValue.Verify)
                        select new SelectListItem
                        {
                            Value = ty.SchemeTenureListId.ToString(),
                            Text = (ty.TenureText.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

       
        public List<SelectListItem> VehicleBodyTypeDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from v in context.VehicleBodyTypes
                            join t in context.VehicleBodyTypeTranslations on v.PrmKey equals t.VehicleBodyTypePrmKey
                            where (v.ActivationStatus == StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = v.VehicleBodyTypeId.ToString(),
                                Text = v.NameOfBodyType.Trim() + " ---> " + t.TransNameOfBodyType.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from v in context.VehicleBodyTypes

                        select new SelectListItem
                        {
                            Value = v.VehicleBodyTypeId.ToString(),
                            Text = v.NameOfBodyType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> VehicleTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from v in context.VehicleTypes
                            join t in context.VehicleTypeTranslations on v.PrmKey equals t.VehicleTypePrmKey into vt
                            from t in vt.DefaultIfEmpty()
                            where (v.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = v.VehicleTypeId.ToString(),
                                Text = (v.NameOfVehicleType.Trim() + " ---> " + (t.TransNameOfVehicleType.Trim() ?? ""))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from v in context.VehicleTypes
                        where (v.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = v.VehicleTypeId.ToString(),
                            Text = v.NameOfVehicleType.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> VehicleMakeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from v in context.VehicleMakes
                            join mf in context.VehicleMakeModifications .Where(mf => mf.EntryStatus == EntryStatus.Verified) on v.PrmKey equals mf.VehicleMakePrmKey into bm
                            from mf in bm.DefaultIfEmpty()
                            join t in context.VehicleMakeTranslations .Where(t => t.EntryStatus == EntryStatus.Verified) on v.PrmKey equals t.VehicleMakePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (v.EntryStatus == StringLiteralValue.Verify)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = v.VehicleMakeId.ToString(),
                                Text = (mf.NameOfVehicleMake ?? v.NameOfVehicleMake.Trim()) + " ---> " + (t.TransNameOfVehicleMake ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from v in context.VehicleMakes
                        join mf in context.VehicleMakeModifications .Where(mf => mf.EntryStatus == EntryStatus.Verified) on v.PrmKey equals mf.VehicleMakePrmKey into bm
                        from mf in bm.DefaultIfEmpty()
                        where (v.EntryStatus == StringLiteralValue.Verify)
                        select new SelectListItem
                        {
                            Value = v.VehicleMakeId.ToString(),
                            Text = (mf.NameOfVehicleMake ?? v.NameOfVehicleMake.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> VehicleModelDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from v in context.VehicleModels
                            join mf in context.VehicleModelModifications on v.PrmKey equals mf.VehicleModelPrmKey into vm
                            from mf in vm.DefaultIfEmpty()
                            join t in context.VehicleModelTranslations on v.PrmKey equals t.VehicleModelPrmKey into vt
                            from t in vt.DefaultIfEmpty()
                            where (v.EntryStatus == StringLiteralValue.Verify
                                    && (mf.EntryStatus == StringLiteralValue.Verify || mf.EntryStatus == null)
                                    && (t.EntryStatus == StringLiteralValue.Verify || t.EntryStatus == null)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = v.VehicleModelId.ToString(),
                                Text = (mf.NameOfVehicleModel ?? v.NameOfVehicleModel.Trim()) + " ---> " + (t.TransNameOfVehicleModel ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from v in context.VehicleModels
                        join mf in context.VehicleModelModifications on v.PrmKey equals mf.VehicleModelPrmKey into vm
                        from mf in vm.DefaultIfEmpty()
                        where (v.EntryStatus == StringLiteralValue.Verify
                                && (mf.EntryStatus == StringLiteralValue.Verify || mf.EntryStatus == null))
                        select new SelectListItem
                        {
                            Value = v.VehicleModelId.ToString(),
                            Text = ((mf.NameOfVehicleModel) ?? v.NameOfVehicleModel.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> VehicleVariantDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from v in context.VehicleVariants
                            join mf in context.VehicleVariantModifications on v.PrmKey equals mf.VehicleVariantPrmKey into vm
                            from mf in vm.DefaultIfEmpty()
                            join t in context.VehicleVariantTranslations on v.PrmKey equals t.VehicleVariantPrmKey into vt
                            from t in vt.DefaultIfEmpty()
                            where (v.EntryStatus == StringLiteralValue.Verify && v.ActivationStatus == StringLiteralValue.Active
                                    && (mf.EntryStatus == StringLiteralValue.Verify || mf.EntryStatus == null)
                                    && (t.EntryStatus == StringLiteralValue.Verify || t.EntryStatus == null)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = v.VehicleVariantId.ToString(),
                                Text = (mf.NameOfVariant ?? v.NameOfVariant.Trim()) + " ---> " + (t.TransNameOfVariant.Trim() ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from v in context.VehicleVariants
                        join mf in context.VehicleVariantModifications on v.PrmKey equals mf.VehicleVariantPrmKey into vm
                        from mf in vm.DefaultIfEmpty()
                        where (v.EntryStatus == StringLiteralValue.Verify
                                && (mf.EntryStatus == StringLiteralValue.Verify || mf.EntryStatus == null))
                        select new SelectListItem
                        {
                            Value = v.VehicleVariantId.ToString(),
                            Text = ((mf.NameOfVariant) ?? v.NameOfVariant.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> VehicleSupplierDropdownList
        {
            get
            {

                //Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var ps = (from vs in context.VehicleSuppliers
                              join v in context.People on vs.PersonPrmKey equals v.PrmKey
                              join mf in context.PersonModifications on v.PrmKey equals mf.PersonPrmKey into vm
                              from mf in vm.DefaultIfEmpty()
                              join t in context.PersonTranslations on v.PrmKey equals t.PersonPrmKey into vt
                              from t in vt.DefaultIfEmpty()
                              where (v.EntryStatus == StringLiteralValue.Verify && v.ActivationStatus == StringLiteralValue.Active
                                      && (mf.EntryStatus == StringLiteralValue.Verify || mf.EntryStatus == null)
                                      && (t.EntryStatus == StringLiteralValue.Verify || t.EntryStatus == null)
                                      && (t.LanguagePrmKey == regionalLanguagePrmKey))
                              select new SelectListItem
                              {
                                  Value = vs.VehicleSupplierId.ToString(),
                                  Text = (mf.FullName ?? v.FullName.Trim()) + " ---> " + (t.TransFullName.Trim() ?? "")
                              }).Distinct().OrderBy(l => l.Text).ToList();
                    return ps;
                }

                // Default List In Defaul Language (i.e. English)
                var ss = (from j in context.VehicleSuppliers

                          join t in context.People on j.PersonPrmKey equals t.PrmKey into pt
                          from t in pt.DefaultIfEmpty()
                          where (t.EntryStatus == StringLiteralValue.Verify)
                          select new SelectListItem
                          {
                              Value = j.VehicleSupplierId.ToString(),
                              Text = t.FullName
                          }).Distinct().OrderBy(l => l.Text).ToList();
                return ss;
            }
        }

        public List<SelectListItem> ColourDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Colours
                            join ct in context.ColourTranslations on c.PrmKey equals ct.ColourPrmKey into cm
                            from ct in cm.DefaultIfEmpty()
                            where (c.ActivationStatus == StringLiteralValue.Active)
                            && (ct.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = c.ColourId.ToString(),
                                Text = (c.NameOfColour.Trim()) + " ---> " + (ct.TransNameOfColour ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Colours
                        where (c.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = c.ColourId.ToString(),
                            Text = (c.NameOfColour.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> InstituteDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from a in context.Institutes
                            join t in context.InstituteTranslations on a.PrmKey equals t.InstitutePrmKey
                            where a.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = a.InstituteId.ToString(),
                                Text = a.NameOfInstitute.Trim() + " ---> " + t.TransNameOfInstitute.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from a in context.Institutes
                        where a.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = a.InstituteId.ToString(),
                            Text = a.NameOfInstitute
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> EducationalCourseDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from a in context.EducationalCourses
                            join t in context.EducationalCourseTranslations on a.PrmKey equals t.EducationalCoursePrmKey
                            where a.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = a.EducationalCourseId.ToString(),
                                Text = a.NameOfCourse.Trim() + " ---> " + t.TransNameOfCourse.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from a in context.EducationalCourses
                        where a.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = a.EducationalCourseId.ToString(),
                            Text = a.NameOfCourse
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SchemeLoanAccountParameterDropdownList
        {
            get
            {
                // Default List In Defaul Language (i.e. English)
                return (from sc in context.SchemeLoanAccountParameters
                        join s in context.Schemes on sc.SchemePrmKey equals s.PrmKey into scs
                        from s in scs.DefaultIfEmpty()
                        where (sc.EntryStatus == StringLiteralValue.Verify
                                && (s.EntryStatus == StringLiteralValue.Verify))
                        select new SelectListItem
                        {
                            Value = s.SchemeId.ToString(),
                            Text = s.NameOfScheme
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> GetLoanSchemeDropdownListByLoanTypeId(Guid _loanTypeId)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from l in context.LoanTypes
                        join a in context.SchemeLoanAccountParameters.Where(a => a.EntryStatus == StringLiteralValue.Verify) on l.PrmKey equals a.LoanTypePrmKey into al
                        from a in al.DefaultIfEmpty()
                        join s in context.Schemes.Where(s => s.EntryStatus == StringLiteralValue.Verify) on a.SchemePrmKey equals s.PrmKey into als
                        from s in als.DefaultIfEmpty()
                        join t in context.SchemeTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on s.PrmKey equals t.SchemePrmKey into bt
                        from t in bt.DefaultIfEmpty()
                        where (l.LoanTypeId == _loanTypeId)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = s.SchemeId.ToString(),
                            Text = (s.NameOfScheme.Trim() + " ---> " + (t.TransNameOfScheme.Trim() ?? ""))
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            // Default List In Defaul Language (i.e. English)
            return (from l in context.LoanTypes
                    join a in context.SchemeLoanAccountParameters.Where(a => a.EntryStatus == StringLiteralValue.Verify) on l.PrmKey equals a.LoanTypePrmKey into al
                    from a in al.DefaultIfEmpty()
                    join s in context.Schemes.Where(s => s.EntryStatus == StringLiteralValue.Verify) on a.SchemePrmKey equals s.PrmKey into als
                    from s in als.DefaultIfEmpty()
                    where (l.LoanTypeId == _loanTypeId)
                    select new SelectListItem
                    {
                        Value = s.SchemeId.ToString(),
                        Text = (s.NameOfScheme.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> GetVehicleCompanyDropdownListByVehicleTypeId(short _schemePrmKey)
        {
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            IEnumerable<DbQueryDropdownListViewModel> dropdownListViewModel = context.Database.SqlQuery<DbQueryDropdownListViewModel>("SELECT * FROM dbo.GetDropdownListOfVehicleCompanyBySchemeAndPurpose (@SchemeId, @RegionalLanguagePrmKey)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("@RegionalLanguagePrmKey", regionalLanguagePrmKey)).Distinct().ToList();

            // Map the results to SelectListItem
            var selectListItems = dropdownListViewModel.Select(p => new SelectListItem
            {
                Value = p.ValueId.ToString(),
                Text = p.ValueText
            }).ToList();

            return selectListItems;
        }

        // new code
        public List<SelectListItem> GetVehicleModelDropdownListByVehicleMakeId(Guid _vehicleMakeId)
        {
            short vehicleMakePrmKey = GetVehicleMakePrmKeyById(_vehicleMakeId);

            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                // Get All Valid Selectlist From VehiclModels            
                return (from v in context.VehicleModels
                        join mf in context.VehicleModelModifications .Where(mf => mf.EntryStatus == EntryStatus.Verified)  on v.PrmKey equals mf.VehicleModelPrmKey into vmf
                        from mf in vmf.DefaultIfEmpty()
                        join t in context.VehicleModelTranslations .Where(t => t.EntryStatus == EntryStatus.Verified) on v.PrmKey equals t.VehicleModelPrmKey into vt
                        from t in vt.DefaultIfEmpty()
                        where (v.VehicleMakePrmKey == vehicleMakePrmKey)
                        && (v.EntryStatus == StringLiteralValue.Verify)
                        && (v.ActivationStatus == StringLiteralValue.Active)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = v.VehicleModelId.ToString(),
                            Text = (mf.NameOfVehicleModel ?? v.NameOfVehicleModel.Trim()) + " ---> " + (t.TransNameOfVehicleModel.Trim() ?? "")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            // Default List In Default Language (i.e. English)
            return (from v in context.VehicleModels
                    join mf in context.VehicleModelModifications.Where(mf => mf.EntryStatus == EntryStatus.Verified) on v.PrmKey equals mf.VehicleModelPrmKey into vmf
                    from mf in vmf.DefaultIfEmpty()
                    where (v.VehicleMakePrmKey == vehicleMakePrmKey)
                    && (v.EntryStatus == StringLiteralValue.Verify)
                    && (v.ActivationStatus == StringLiteralValue.Active)
                    select new SelectListItem
                    {
                        Value = v.VehicleModelId.ToString(),
                        Text = ((mf.NameOfVehicleModel) ?? v.NameOfVehicleModel.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> GetVehicleVariantDropdownListByVehicleModelId(Guid _vehicleModelId)
        {
            List<SelectListItem> SectionNames = new List<SelectListItem>();
            short vehicleModelPrmKey = GetVehicleModelPrmKeyById(_vehicleModelId);

            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                // Get All Valid Selectlist From VehicleVarients            
                return (from v in context.VehicleVariants
                        join mf in context.VehicleVariantModifications.Where(mf => mf.EntryStatus == EntryStatus.Verified) on v.PrmKey equals mf.VehicleVariantPrmKey into vm
                        from mf in vm.DefaultIfEmpty()
                        join t in context.VehicleVariantTranslations.Where(t => t.EntryStatus == EntryStatus.Verified) on v.PrmKey equals t.VehicleVariantPrmKey into ht
                        from t in ht.DefaultIfEmpty()
                        where (v.VehicleModelPrmKey == vehicleModelPrmKey)
                              && (v.EntryStatus == StringLiteralValue.Verify)
                              && (v.ActivationStatus == StringLiteralValue.Active)
                              && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = v.VehicleVariantId.ToString(),
                            Text = (mf.NameOfVariant ?? v.NameOfVariant.Trim()) + " ---> " + (t.TransNameOfVariant.Trim() ?? "")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            // Default List In Default Language (i.e. English)
            return (from v in context.VehicleVariants
                    join mf in context.VehicleVariantModifications.Where(mf => mf.EntryStatus == EntryStatus.Verified) on v.PrmKey equals mf.VehicleVariantPrmKey into vm
                    from mf in vm.DefaultIfEmpty()
                    where (v.VehicleModelPrmKey == vehicleModelPrmKey)
                          && (v.EntryStatus == StringLiteralValue.Verify)
                          && (v.ActivationStatus == StringLiteralValue.Active)
                    select new SelectListItem
                    {
                        Value = v.VehicleVariantId.ToString(),
                        Text = ((mf.NameOfVariant) ?? v.NameOfVariant.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> GetAuthorizedLoanGLDropdownListForAccountOpening(Guid _businessOfficeId, Guid _loanTypeId)
        {
            byte loanTypePrmKey = GetLoanTypePrmKeyById(_loanTypeId);

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // Get UserProfilePrmKey
            short userProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

            short businessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeId);

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                if (HasAccessOfAllGeneralLedger(userProfilePrmKey))
                {

                    return (from l in context.SchemeLoanAccountParameters
                            join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on l.SchemePrmKey equals gs.SchemePrmKey into gc
                            from gs in gc.DefaultIfEmpty()
                            join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gl
                            from g in gl.DefaultIfEmpty()
                            join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                            from mf in hm.DefaultIfEmpty()
                            join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals t.GeneralLedgerPrmKey into ht
                            from t in ht.DefaultIfEmpty()
                            join b in context.GeneralLedgerBusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
                            from b in bg.DefaultIfEmpty()
                            where (l.LoanTypePrmKey == loanTypePrmKey)
                            && (l.EntryStatus == StringLiteralValue.Verify)
                            && (b.BusinessOfficePrmKey == businessOfficePrmKey)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = g.GeneralLedgerId.ToString(),
                                Text = (mf.NameOfGL ?? g.NameOfGL.Trim()) + " ---> " + (t.TransNameOfGL.Trim() ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from l in context.SchemeLoanAccountParameters
                        join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on l.SchemePrmKey equals gs.SchemePrmKey into gc
                        from gs in gc.DefaultIfEmpty()
                        join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gl
                        from g in gl.DefaultIfEmpty()
                        join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                        from mf in hm.DefaultIfEmpty()
                        join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals t.GeneralLedgerPrmKey into ht
                        from t in ht.DefaultIfEmpty()
                        join u in context.UserProfileGeneralLedgers.Where(u => u.EntryStatus == StringLiteralValue.Verify) on gs.GeneralLedgerPrmKey equals u.GeneralLedgerPrmKey into gu
                        from u in gu.DefaultIfEmpty()
                        join b in context.GeneralLedgerBusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
                        from b in bg.DefaultIfEmpty()
                        where (l.LoanTypePrmKey == loanTypePrmKey)
                        && (l.EntryStatus == StringLiteralValue.Verify)
                        && (b.BusinessOfficePrmKey == businessOfficePrmKey)
                        && (u.UserProfilePrmKey == userProfilePrmKey)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = g.GeneralLedgerId.ToString(),
                            Text = (mf.NameOfGL ?? g.NameOfGL.Trim()) + " ---> " + (t.TransNameOfGL.Trim() ?? "")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            if (HasAccessOfAllGeneralLedger(userProfilePrmKey))
            {
                return (from l in context.SchemeLoanAccountParameters
                        join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on l.SchemePrmKey equals gs.SchemePrmKey into gc
                        from gs in gc.DefaultIfEmpty()
                        join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gl
                        from g in gl.DefaultIfEmpty()
                        join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                        from mf in hm.DefaultIfEmpty()
                        join b in context.GeneralLedgerBusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
                        from b in bg.DefaultIfEmpty()
                        where (l.LoanTypePrmKey == loanTypePrmKey)
                        && (l.EntryStatus == StringLiteralValue.Verify)
                        && (b.BusinessOfficePrmKey == businessOfficePrmKey)
                        select new SelectListItem
                        {
                            Value = g.GeneralLedgerId.ToString(),
                            Text = ((mf.NameOfGL.Trim()) ?? g.NameOfGL.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
            // Default List In Default Language (i.e. English)
            return (from l in context.SchemeLoanAccountParameters
                    join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on l.SchemePrmKey equals gs.SchemePrmKey into gc
                    from gs in gc.DefaultIfEmpty()
                    join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gl
                    from g in gl.DefaultIfEmpty()
                    join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                    from mf in hm.DefaultIfEmpty()
                    join u in context.UserProfileGeneralLedgers.Where(u => u.EntryStatus == StringLiteralValue.Verify) on gs.GeneralLedgerPrmKey equals u.GeneralLedgerPrmKey into gu
                    from u in gu.DefaultIfEmpty()
                    join b in context.GeneralLedgerBusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
                    from b in bg.DefaultIfEmpty()
                    where (l.LoanTypePrmKey == loanTypePrmKey)
                    && (l.EntryStatus == StringLiteralValue.Verify)
                    && (b.BusinessOfficePrmKey == businessOfficePrmKey)
                    && (u.UserProfilePrmKey == userProfilePrmKey)
                    select new SelectListItem
                    {
                        Value = g.GeneralLedgerId.ToString(),
                        Text = ((mf.NameOfGL.Trim()) ?? g.NameOfGL.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> GetAuthorizedSharesCapitalGLDropdownListForAccountOpening(Guid _businessOfficeId)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // Get UserProfilePrmKey
            short userProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

            short businessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeId);

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                if (HasAccessOfAllGeneralLedger(userProfilePrmKey))
                {

                    return (from s in context.SchemeSharesCapitalAccountParameters
                            join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on s.SchemePrmKey equals gs.SchemePrmKey into sg
                            from gs in sg.DefaultIfEmpty()
                            join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gn
                            from g in gn.DefaultIfEmpty()
                            join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                            from mf in hm.DefaultIfEmpty()
                            join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals t.GeneralLedgerPrmKey into ht
                            from t in ht.DefaultIfEmpty()
                            join b in context.GeneralLedgerBusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
                            from b in bg.DefaultIfEmpty()
                            where (s.EntryStatus == StringLiteralValue.Verify)
                            && (b.BusinessOfficePrmKey == businessOfficePrmKey)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = g.GeneralLedgerId.ToString(),
                                Text = (mf.NameOfGL ?? g.NameOfGL.Trim()) + " ---> " + (t.TransNameOfGL.Trim() ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }
                return (from s in context.SchemeSharesCapitalAccountParameters
                        join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on s.SchemePrmKey equals gs.SchemePrmKey into sg
                        from gs in sg.DefaultIfEmpty()
                        join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gn
                        from g in gn.DefaultIfEmpty()
                        join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                        from mf in hm.DefaultIfEmpty()
                        join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals t.GeneralLedgerPrmKey into ht
                        from t in ht.DefaultIfEmpty()
                        join u in context.UserProfileGeneralLedgers.Where(u => u.EntryStatus == StringLiteralValue.Verify) on gs.GeneralLedgerPrmKey equals u.GeneralLedgerPrmKey into gu
                        from u in gu.DefaultIfEmpty()
                        join b in context.GeneralLedgerBusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
                        from b in bg.DefaultIfEmpty()
                        where (s.EntryStatus == StringLiteralValue.Verify)
                        && (b.BusinessOfficePrmKey == businessOfficePrmKey)
                        && (u.UserProfilePrmKey == userProfilePrmKey)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = g.GeneralLedgerId.ToString(),
                            Text = (mf.NameOfGL ?? g.NameOfGL.Trim()) + " ---> " + (t.TransNameOfGL.Trim() ?? "")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            if (HasAccessOfAllGeneralLedger(userProfilePrmKey))
            {

                // Default List In Default Language (i.e. English)
                return (from s in context.SchemeSharesCapitalAccountParameters
                        join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on s.SchemePrmKey equals gs.SchemePrmKey into sg
                        from gs in sg.DefaultIfEmpty()
                        from g in context.GeneralLedgers
                        join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                        from mf in hm.DefaultIfEmpty()
                        join b in context.GeneralLedgerBusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
                        from b in bg.DefaultIfEmpty()
                        where (s.EntryStatus == StringLiteralValue.Verify)
                        && (b.BusinessOfficePrmKey == businessOfficePrmKey)

                        select new SelectListItem
                        {
                            Value = g.GeneralLedgerId.ToString(),
                            Text = ((mf.NameOfGL) ?? g.NameOfGL.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
            return (from s in context.SchemeSharesCapitalAccountParameters
                    join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on s.SchemePrmKey equals gs.SchemePrmKey into sg
                    from gs in sg.DefaultIfEmpty()
                    from g in context.GeneralLedgers
                    join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                    from mf in hm.DefaultIfEmpty()
                    join u in context.UserProfileGeneralLedgers.Where(u => u.EntryStatus == StringLiteralValue.Verify) on gs.GeneralLedgerPrmKey equals u.GeneralLedgerPrmKey into gu
                    from u in gu.DefaultIfEmpty()
                    join b in context.GeneralLedgerBusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
                    from b in bg.DefaultIfEmpty()
                    where (s.EntryStatus == StringLiteralValue.Verify)
                    && (b.BusinessOfficePrmKey == businessOfficePrmKey)
                    && (u.UserProfilePrmKey == userProfilePrmKey)
                    select new SelectListItem
                    {
                        Value = g.GeneralLedgerId.ToString(),
                        Text = ((mf.NameOfGL) ?? g.NameOfGL.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> GetAuthorizedDepositGLDropdownListForAccountOpening(Guid _businessOfficeId, string _depositType)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // Get UserProfilePrmKey
            short userProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

            // Get BusinessOfficePrmKey
            short businessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeId);

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                //if(securityDetailRepository.HasAccessOfAllGeneralLedger(userProfilePrmKey))
                if (HasAccessOfAllGeneralLedger(userProfilePrmKey))
                {
                    return (from s in context.SchemeDepositAccountParameters
                            join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on s.SchemePrmKey equals gs.SchemePrmKey into sg
                            from gs in sg.DefaultIfEmpty()
                            join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gsg
                            from g in gsg.DefaultIfEmpty()
                            join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                            from mf in hm.DefaultIfEmpty()
                            join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals t.GeneralLedgerPrmKey into ht
                            from t in ht.DefaultIfEmpty()
                            join b in context.GeneralLedgerBusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
                            from b in bg.DefaultIfEmpty()
                            where (s.DepositType == _depositType)
                            && (s.EntryStatus == StringLiteralValue.Verify)
                            && (b.BusinessOfficePrmKey == businessOfficePrmKey)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = g.GeneralLedgerId.ToString(),
                                Text = (mf.NameOfGL ?? g.NameOfGL.Trim()) + " ---> " + (t.TransNameOfGL.Trim() ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from s in context.SchemeDepositAccountParameters
                        join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on s.SchemePrmKey equals gs.SchemePrmKey into sg
                        from gs in sg.DefaultIfEmpty()
                        join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gsg
                        from g in gsg.DefaultIfEmpty()
                        join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                        from mf in hm.DefaultIfEmpty()
                        join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals t.GeneralLedgerPrmKey into ht
                        from t in ht.DefaultIfEmpty()
                        join u in context.UserProfileGeneralLedgers.Where(u => u.EntryStatus == StringLiteralValue.Verify) on gs.GeneralLedgerPrmKey equals u.GeneralLedgerPrmKey into gu
                        from u in gu.DefaultIfEmpty()
                        join b in context.GeneralLedgerBusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
                        from b in bg.DefaultIfEmpty()
                        where (s.DepositType == _depositType)
                        && (s.EntryStatus == StringLiteralValue.Verify)
                        && (b.BusinessOfficePrmKey == businessOfficePrmKey)
                        && (u.UserProfilePrmKey == userProfilePrmKey)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = g.GeneralLedgerId.ToString(),
                            Text = (mf.NameOfGL ?? g.NameOfGL.Trim()) + " ---> " + (t.TransNameOfGL.Trim() ?? "")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }


            // Default List In Default Language (i.e. English)
            //if(securityDetailRepository.HasAccessOfAllGeneralLedger(userProfilePrmKey))
            if (HasAccessOfAllGeneralLedger(userProfilePrmKey))
            {
                return (from s in context.SchemeDepositAccountParameters
                        join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on s.SchemePrmKey equals gs.SchemePrmKey into sg
                        from gs in sg.DefaultIfEmpty()
                        join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gsg
                        from g in gsg.DefaultIfEmpty()
                        join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                        from mf in hm.DefaultIfEmpty()
                        join b in context.GeneralLedgerBusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
                        from b in bg.DefaultIfEmpty()
                        where (s.DepositType == _depositType)
                        && (s.EntryStatus == StringLiteralValue.Verify)
                        && (b.BusinessOfficePrmKey == businessOfficePrmKey)
                        select new SelectListItem
                        {
                            Value = g.GeneralLedgerId.ToString(),
                            Text = ((mf.NameOfGL.Trim()) ?? g.NameOfGL.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            return (from s in context.SchemeDepositAccountParameters
                    join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on s.SchemePrmKey equals gs.SchemePrmKey into sg
                    from gs in sg.DefaultIfEmpty()
                    join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gsg
                    from g in gsg.DefaultIfEmpty()
                    join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
                    from mf in hm.DefaultIfEmpty()
                    join u in context.UserProfileGeneralLedgers.Where(u => u.EntryStatus == StringLiteralValue.Verify) on gs.GeneralLedgerPrmKey equals u.GeneralLedgerPrmKey into gu
                    from u in gu.DefaultIfEmpty()
                    join b in context.GeneralLedgerBusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
                    from b in bg.DefaultIfEmpty()
                    where (s.DepositType == _depositType)
                    && (s.EntryStatus == StringLiteralValue.Verify)
                    && (b.BusinessOfficePrmKey == businessOfficePrmKey)
                    && (u.UserProfilePrmKey == userProfilePrmKey)
                    select new SelectListItem
                    {
                        Value = g.GeneralLedgerId.ToString(),
                        Text = ((mf.NameOfGL.Trim()) ?? g.NameOfGL.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> AuthorizedBusinessOfficeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // Get UserProfilePrmKey
                short userProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    if (HasAccessOfAllBusinessOffice(userProfilePrmKey))
                    {

                        return (from b in context.BusinessOffices
                                join t in context.BusinessOfficeTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on b.PrmKey equals t.BusinessOfficePrmKey into tt
                                from t in tt.DefaultIfEmpty()
                                where (b.EntryStatus.Equals(StringLiteralValue.Verify))
                                && (b.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                orderby b.NameOfBusinessOffice
                                select new SelectListItem
                                {
                                    Value = b.BusinessOfficeId.ToString(),
                                    Text = (b.NameOfBusinessOffice.Trim() + " ---> " + (t.TransNameOfBusinessOffice.Equals(null) ? " " : t.TransNameOfBusinessOffice.Trim()))
                                }).ToList();
                    }
                    return (from ub in context.UserProfileBusinessOffices
                            join b in context.BusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on ub.BusinessOfficePrmKey equals b.PrmKey
                            join t in context.BusinessOfficeTranslations on b.PrmKey equals t.BusinessOfficePrmKey into tt
                            from t in tt.DefaultIfEmpty()
                            where (ub.UserProfilePrmKey == userProfilePrmKey)
                                    && (ub.EntryStatus.Equals(StringLiteralValue.Verify))
                                    && (ub.ActivationStatus.Equals(StringLiteralValue.Active))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby b.NameOfBusinessOffice
                            select new SelectListItem
                            {
                                Value = b.BusinessOfficeId.ToString(),
                                Text = (b.NameOfBusinessOffice.Trim() + " ---> " + (t.TransNameOfBusinessOffice.Equals(null) ? " " : t.TransNameOfBusinessOffice.Trim()))
                            }).ToList();
                }

                if (HasAccessOfAllBusinessOffice(userProfilePrmKey))
                {

                    // Default List In Default Language (i.e. English)
                    return (from b in context.BusinessOffices
                            where (b.EntryStatus.Equals(StringLiteralValue.Verify))
                            && (b.ActivationStatus.Equals(StringLiteralValue.Active))
                            orderby b.NameOfBusinessOffice
                            select new SelectListItem
                            {
                                Value = b.BusinessOfficeId.ToString(),
                                Text = (b.NameOfBusinessOffice.Trim())
                            }).ToList();
                }
                // Default List In Default Language (i.e. English)
                return (from ub in context.UserProfileBusinessOffices
                        join b in context.BusinessOffices.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active) on ub.BusinessOfficePrmKey equals b.PrmKey
                        where (ub.UserProfilePrmKey == userProfilePrmKey)
                                && (ub.EntryStatus.Equals(StringLiteralValue.Verify))
                                && (ub.ActivationStatus.Equals(StringLiteralValue.Active))
                        orderby b.NameOfBusinessOffice
                        select new SelectListItem
                        {
                            Value = b.BusinessOfficeId.ToString(),
                            Text = b.NameOfBusinessOffice.Trim()
                        }).ToList();
            }
        }

        //public List<SelectListItem> GetAuthorizedDepositGeneralLedgerDropdownList(Guid _businessOfficeId, string _depositType)
        //{
        //    // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
        //    short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

        //    // Get UserProfilePrmKey
        //    short userProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

        //    // Get BusinessOfficePrmKey
        //    short businessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeId);

        //    // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
        //    if (regionalLanguagePrmKey != 1)
        //    {
        //        return (from s in context.SchemeDepositAccountParameters
        //                join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on s.SchemePrmKey equals gs.SchemePrmKey into sg
        //                from gs in sg.DefaultIfEmpty()
        //                join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gsg
        //                from g in gsg.DefaultIfEmpty()
        //                join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
        //                from mf in hm.DefaultIfEmpty()
        //                join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals t.GeneralLedgerPrmKey into ht
        //                from t in ht.DefaultIfEmpty()
        //                join u in context.UserProfileTransactionLimits.Where(u => u.UserProfilePrmKey == userProfilePrmKey && u.EntryStatus == StringLiteralValue.Verify) on gs.GeneralLedgerPrmKey equals u.GeneralLedgerPrmKey into gu
        //                from u in gu.DefaultIfEmpty()
        //                join b in context.BusinessOfficeTransactionLimits.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active && b.BusinessOfficePrmKey == businessOfficePrmKey) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
        //                from b in bg.DefaultIfEmpty()
        //                where (s.DepositType == _depositType)
        //                && (s.EntryStatus == StringLiteralValue.Verify)
        //                && (t.LanguagePrmKey == regionalLanguagePrmKey)
        //                select new SelectListItem
        //                {
        //                    Value = g.GeneralLedgerId.ToString(),
        //                    Text = (mf.NameOfGL ?? g.NameOfGL.Trim()) + " ---> " + (t.TransNameOfGL.Trim() ?? "")
        //                }).Distinct().OrderBy(l => l.Text).ToList();
        //    }

        //    // Default List In Default Language (i.e. English)
        //    return (from s in context.SchemeDepositAccountParameters
        //            join gs in context.SchemeGeneralLedgers.Where(gs => gs.EntryStatus == StringLiteralValue.Verify && gs.ActivationStatus == StringLiteralValue.Active) on s.SchemePrmKey equals gs.SchemePrmKey into sg
        //            from gs in sg.DefaultIfEmpty()
        //            join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on gs.GeneralLedgerPrmKey equals g.PrmKey into gsg
        //            from g in gsg.DefaultIfEmpty()
        //            join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into hm
        //            from mf in hm.DefaultIfEmpty()
        //            join u in context.UserProfileTransactionLimits.Where(u => u.UserProfilePrmKey == userProfilePrmKey && u.EntryStatus == StringLiteralValue.Verify) on gs.GeneralLedgerPrmKey equals u.GeneralLedgerPrmKey into gu
        //            from u in gu.DefaultIfEmpty()
        //            join b in context.BusinessOfficeTransactionLimits.Where(b => b.EntryStatus == StringLiteralValue.Verify && b.ActivationStatus == StringLiteralValue.Active && b.BusinessOfficePrmKey == businessOfficePrmKey) on g.PrmKey equals b.GeneralLedgerPrmKey into bg
        //            from b in bg.DefaultIfEmpty()
        //            where (s.DepositType == _depositType)
        //            && (s.EntryStatus == StringLiteralValue.Verify)
        //            select new SelectListItem
        //            {
        //                Value = g.GeneralLedgerId.ToString(),
        //                Text = ((mf.NameOfGL.Trim()) ?? g.NameOfGL.Trim())
        //            }).Distinct().OrderBy(l => l.Text).ToList();
        //}

        public List<SelectListItem> GetCustomerSavingAccountDropdownList(long _personPrmKey)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from s in context.SchemeDemandDepositDetails
                        join d in context.CustomerAccountDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on s.SchemePrmKey equals d.SchemePrmKey into sd
                        from d in sd.DefaultIfEmpty()
                        join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify) on d.PersonPrmKey equals p.PrmKey into dp
                        from p in dp.DefaultIfEmpty()
                        join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
                        from m in pm.DefaultIfEmpty()
                        join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify && t.LanguagePrmKey == regionalLanguagePrmKey) on p.PrmKey equals t.PersonPrmKey into pt
                        from t in pt.DefaultIfEmpty()
                        join c in context.CustomerAccounts.Where(c => c.ActivationStatus == StringLiteralValue.Active && c.EntryStatus == StringLiteralValue.Verify) on d.CustomerAccountPrmKey equals c.PrmKey into dc
                        from c in dc.DefaultIfEmpty()
                        where (d.PersonPrmKey == _personPrmKey)
                        && (s.EntryStatus == StringLiteralValue.Verify)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = c.CustomerAccountId.ToString(),
                            Text = (m.FullName ?? p.FullName.Trim()) + " ---> " + (t.TransFullName.Trim() ?? "")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }


            return (from s in context.SchemeDemandDepositDetails
                    join d in context.CustomerAccountDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on s.SchemePrmKey equals d.SchemePrmKey into sd
                    from d in sd.DefaultIfEmpty()
                    join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify) on d.PersonPrmKey equals p.PrmKey into dp
                    from p in dp.DefaultIfEmpty()
                    join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
                    from m in pm.DefaultIfEmpty()
                    join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify && t.LanguagePrmKey == regionalLanguagePrmKey) on p.PrmKey equals t.PersonPrmKey into pt
                    from t in pt.DefaultIfEmpty()
                    join c in context.CustomerAccounts.Where(c => c.ActivationStatus == StringLiteralValue.Active && c.EntryStatus == StringLiteralValue.Verify) on d.CustomerAccountPrmKey equals c.PrmKey into dc
                    from c in dc.DefaultIfEmpty()
                    where (d.PersonPrmKey == _personPrmKey)
                    && (s.EntryStatus == StringLiteralValue.Verify)
                    select new SelectListItem
                    {
                        Value = c.CustomerAccountId.ToString(),
                        Text = ((m.FullName) ?? p.FullName.Trim() + " ---> ") + " -- > " + c.AccountNumber.ToString()
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> GetSchemeDropdownListByGeneralLedgerId(Guid _generalLedgerId)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from g in context.GeneralLedgers
                        join l in context.SchemeGeneralLedgers.Where(l => l.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals l.GeneralLedgerPrmKey into gl
                        from l in gl.DefaultIfEmpty()
                        join s in context.Schemes.Where(s => s.EntryStatus == StringLiteralValue.Verify) on l.SchemePrmKey equals s.PrmKey into gs
                        from s in gs.DefaultIfEmpty()
                        join t in context.SchemeTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on s.PrmKey equals t.SchemePrmKey into bt
                        from t in bt.DefaultIfEmpty()
                        where (g.GeneralLedgerId == _generalLedgerId)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = s.SchemeId.ToString(),
                            Text = (s.NameOfScheme.Trim() + " ---> " + (t.TransNameOfScheme.Trim() ?? ""))
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            return (from g in context.GeneralLedgers
                    join l in context.SchemeGeneralLedgers.Where(l => l.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals l.GeneralLedgerPrmKey into gl
                    from l in gl.DefaultIfEmpty()
                    join s in context.Schemes.Where(s => s.EntryStatus == StringLiteralValue.Verify) on l.SchemePrmKey equals s.PrmKey into gs
                    from s in gs.DefaultIfEmpty()
                    where (g.GeneralLedgerId == _generalLedgerId)
                    select new SelectListItem
                    {
                        Value = s.SchemeId.ToString(),
                        Text = (s.NameOfScheme.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> GetDocumentDropdownListByLoanType(string _sysNameLoanType)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // Get Cash Credit Loan Type Document PrmKey
            byte documentTypePrmKey = GetDocumentTypePrmKeyBySysName(_sysNameLoanType);

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from d in context.DocumentDocumentTypes
                        join dt in context.DocumentTypes.Where(dt => dt.ActivationStatus == StringLiteralValue.Active) on d.DocumentTypePrmKey equals dt.PrmKey into ddt
                        from dt in ddt.DefaultIfEmpty()
                        join dm in context.Documents.Where(dm => dm.ActivationStatus == StringLiteralValue.Active) on d.DocumentPrmKey equals dm.PrmKey into ddm
                        from dm in ddm.DefaultIfEmpty()
                        join t in context.DocumentTranslations on dm.PrmKey equals t.DocumentPrmKey into dmt
                        from t in dmt.DefaultIfEmpty()
                        where (d.EntryStatus == StringLiteralValue.Verify)
                        && (dt.PrmKey == documentTypePrmKey)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = dm.DocumentId.ToString(),
                            Text = (dm.NameOfDocument.Trim() + " ---> " + (t.TransNameOfDocument.Trim() ?? ""))
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            return (from d in context.DocumentDocumentTypes
                    join dt in context.DocumentTypes.Where(dt => dt.ActivationStatus == StringLiteralValue.Active) on d.DocumentTypePrmKey equals dt.PrmKey into ddt
                    from dt in ddt.DefaultIfEmpty()
                    join dm in context.Documents.Where(dm => dm.ActivationStatus == StringLiteralValue.Active) on d.DocumentPrmKey equals dm.PrmKey into ddm
                    from dm in ddm.DefaultIfEmpty()
                    where (d.EntryStatus == StringLiteralValue.Verify)
                    && (dt.PrmKey == documentTypePrmKey)
                    select new SelectListItem
                    {
                        Value = dm.DocumentId.ToString(),
                        Text = dm.NameOfDocument.Trim()
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> GetDocumentDropdownListBySchemeId(Guid _schemeId)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            short schemePrmKey = GetSchemePrmKeyById(_schemeId);

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from s in context.SchemeDocuments
                        join d in context.Documents.Where(d => d.ActivationStatus == StringLiteralValue.Active) on s.DocumentPrmKey equals d.PrmKey into sd
                        from d in sd.DefaultIfEmpty()
                        join t in context.DocumentTranslations on d.PrmKey equals t.DocumentPrmKey into dt
                        from t in dt.DefaultIfEmpty()
                        where (s.SchemePrmKey == schemePrmKey)
                        && (s.EntryStatus == StringLiteralValue.Verify)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = d.DocumentId.ToString(),
                            Text = d.NameOfDocument.Trim() + " ---> " + t.TransNameOfDocument.Trim() ?? ""
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            return (from s in context.SchemeDocuments
                    join d in context.Documents.Where(d => d.ActivationStatus == StringLiteralValue.Active) on s.DocumentPrmKey equals d.PrmKey into sd
                    from d in sd.DefaultIfEmpty()
                    where (s.SchemePrmKey == schemePrmKey)
                    && (s.EntryStatus == StringLiteralValue.Verify)
                    select new SelectListItem
                    {
                        Value = d.DocumentId.ToString(),
                        Text = (d.NameOfDocument.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> GetGuarantorDropdownListBySchemeId(Guid _schemeId)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            short schemePrmKey = GetSchemePrmKeyById(_schemeId);

            SchemeLoanAccountParameterViewModel schemeLoanAccountParameterViewModel = context.Database.SqlQuery<SchemeLoanAccountParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanAccountParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", schemePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefault();

            string guarantorEligibility = schemeLoanAccountParameterViewModel.EligibilityForGuarantor;

            byte memberTypePrmKey = GetMemberTypePrmKeyBySysName(guarantorEligibility);

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                // All Persons Are Applicable
                if (guarantorEligibility == StringLiteralValue.All)
                {
                    return (from p in context.People
                            join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
                            from m in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify && t.LanguagePrmKey == regionalLanguagePrmKey) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            join s in context.PersonStatuses.Where(s => s.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals s.PersonPrmKey into ps
                            from s in ps.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            && (s.GuarantorForNumberOfLoans < schemeLoanAccountParameterViewModel.NumberOfLoansLimitForSameGuarantors)
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = m.FullName ?? p.FullName.Trim() + " ---> " + t.TransFullName ?? ""
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Ordinary & Associate Member Are Applicable
                if (guarantorEligibility == StringLiteralValue.OrdinaryMember)
                {
                    return (from p in context.People
                            join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
                            from m in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            join s in context.PersonStatuses on p.PrmKey equals s.PersonPrmKey into ps
                            from s in ps.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                            && (s.MemberTypePrmKey == memberTypePrmKey)
                            && (s.GuarantorForNumberOfLoans < schemeLoanAccountParameterViewModel.NumberOfLoansLimitForSameGuarantors)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = m.FullName ?? p.FullName.Trim() + " ---> " + t.TransFullName ?? ""
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Active Member Are Applicable
                if (guarantorEligibility == StringLiteralValue.ActiveMember)
                {
                    return (from p in context.People
                            join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
                            from m in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            join s in context.PersonStatuses on p.PrmKey equals s.PersonPrmKey into ps
                            from s in ps.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                            && (s.IsActiveMember == true)
                            && (s.GuarantorForNumberOfLoans < schemeLoanAccountParameterViewModel.NumberOfLoansLimitForSameGuarantors)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = m.FullName ?? p.FullName.Trim() + " ---> " + t.TransFullName ?? ""
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Any Member
                if (guarantorEligibility == StringLiteralValue.AnyMember)
                {
                    return (from p in context.People
                            join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
                            from m in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            join s in context.PersonStatuses on p.PrmKey equals s.PersonPrmKey into ps
                            from s in ps.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                            && (s.MemberTypePrmKey != 0)
                            && (s.GuarantorForNumberOfLoans < schemeLoanAccountParameterViewModel.NumberOfLoansLimitForSameGuarantors)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = m.FullName ?? p.FullName.Trim() + " ---> " + t.TransFullName ?? ""
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Any Member
                if (guarantorEligibility == StringLiteralValue.AnyMember)
                {
                    return (from p in context.People
                            join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
                            from m in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            join s in context.PersonStatuses on p.PrmKey equals s.PersonPrmKey into ps
                            from s in ps.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                            && (s.IsDepositor == true)
                            && (s.GuarantorForNumberOfLoans < schemeLoanAccountParameterViewModel.NumberOfLoansLimitForSameGuarantors)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = (m.FullName ?? p.FullName.Trim()) + " ---> " + (t.TransFullName ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }
            }

            return (from p in context.People
                    join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
                    from m in pm.DefaultIfEmpty()
                    where (p.EntryStatus == StringLiteralValue.Verify)
                    select new SelectListItem
                    {
                        Value = p.PersonId.ToString(),
                        Text = ((m.FullName) ?? p.FullName.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        //public List<SelectListItem> GetSchemeTenureDropdownListBySchemeId(Guid _schemeId)
        //{
        //    short schemePrmKey = GetSchemePrmKeyById(_schemeId);

        //    // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
        //    short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

        //    // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
        //    if (regionalLanguagePrmKey != 1)
        //    {
        //        return (from l in context.SchemeTenureLists
        //                join t in context.SchemeTenureListTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on l.PrmKey equals t.SchemeTenureListPrmKey into lt
        //                from t in lt.DefaultIfEmpty()
        //                where (l.SchemePrmKey == schemePrmKey)
        //                && (l.EntryStatus == StringLiteralValue.Verify)
        //                && (t.LanguagePrmKey == regionalLanguagePrmKey)
        //                select new SelectListItem
        //                {
        //                    Value = l.SchemeTenureListId.ToString(),
        //                    Text = (l.TenureText.Trim() + " ---> " + (t.TransTenureText.Trim() ?? ""))
        //                }).Distinct().OrderBy(l => l.Text).ToList();
        //    }

        //    return (from l in context.SchemeTenureLists
        //            where (l.SchemePrmKey == schemePrmKey)
        //            && (l.EntryStatus == StringLiteralValue.Verify)
        //            select new SelectListItem
        //            {
        //                Value = l.SchemeTenureListId.ToString(),
        //                Text = l.TenureText.Trim()
        //            }).Distinct().OrderBy(l => l.Text).ToList();
        //}

        // General Ledger Dropdown List

        //public List<SelectListItem> DemandDepositGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        short demandDepositAccountClassPrmKey = (context.AccountClasses
        //                                            .Where(a => a.AccountClassCode == StringLiteralValue.DemandDeposit)
        //                                            .Select(a => a.PrmKey).FirstOrDefault());

        //        return GetGeneralLedgerDropdownListByParentAccountClassPrmKey(demandDepositAccountClassPrmKey);
        //    }
        //}

        //public List<SelectListItem> TermDepositGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        short termDepositAccountClassPrmKey = (context.AccountClasses
        //                            .Where(a => a.AccountClassCode == StringLiteralValue.TermDeposit)
        //                            .Select(a => a.PrmKey).FirstOrDefault());

        //        return GetGeneralLedgerDropdownListByParentAccountClassPrmKey(termDepositAccountClassPrmKey);
        //    }
        //}

        //public List<SelectListItem> RecurringDepositGeneralLedgerDropdownList
        //{
        //    get
        //    {
        //        short recurringDepositAccountClassPrmKey = (context.AccountClasses
        //                            .Where(a => a.AccountClassCode == StringLiteralValue.RecurringDeposit)
        //                            .Select(a => a.PrmKey).FirstOrDefault());

        //        return GetGeneralLedgerDropdownListByParentAccountClassPrmKey(recurringDepositAccountClassPrmKey);
        //    }
        //}

        // **** Code Optimese By Latest --- Do For Above All Methods
        public List<SelectListItem> GetGeneralLedgerDropdownListByAccountClassCode(string _accountClassCode)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from g in context.GeneralLedgers
                        join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify)
                            on g.PrmKey equals mf.GeneralLedgerPrmKey into gm
                        from mf in gm.DefaultIfEmpty()
                        join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify)
                            on g.PrmKey equals t.GeneralLedgerPrmKey into gt
                        from t in gt.DefaultIfEmpty()
                        join a in context.AccountClasses.Where(a => a.ActivationStatus == StringLiteralValue.Active)
                            on g.AccountClassPrmKey equals a.PrmKey into ag
                        from a in ag.DefaultIfEmpty()
                        where g.EntryStatus == StringLiteralValue.Verify
                              && g.ActivationStatus == StringLiteralValue.Active
                              && a.AccountClassCode == _accountClassCode  // Direct comparison for exact match
                              && t.LanguagePrmKey == regionalLanguagePrmKey
                        select new SelectListItem
                        {
                            Value = g.GeneralLedgerId.ToString(),
                            Text = (mf.NameOfGL ?? g.NameOfGL.Trim()) + " ---> " + (t.TransNameOfGL ?? "")
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }

            // Default List In Default Language (i.e. English)
            return (from g in context.GeneralLedgers
                    join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify)
                        on g.PrmKey equals mf.GeneralLedgerPrmKey into gm
                    from mf in gm.DefaultIfEmpty()
                    join a in context.AccountClasses.Where(a => a.ActivationStatus == StringLiteralValue.Active)
                        on g.AccountClassPrmKey equals a.PrmKey into ag
                    from a in ag.DefaultIfEmpty()
                    where g.EntryStatus == StringLiteralValue.Verify
                          && g.ActivationStatus == StringLiteralValue.Active
                          && a.AccountClassCode == _accountClassCode  // Direct comparison for exact match
                    select new SelectListItem
                    {
                        Value = g.GeneralLedgerId.ToString(),
                        Text = mf.NameOfGL ?? g.NameOfGL.Trim()  // Use null-coalescing operator for null handling
                    }).Distinct()
                      .OrderBy(l => l.Text)
                      .ToList();
        }

        public List<SelectListItem> GetGeneralLedgerDropdownListByParentAccountClassPrmKey(string _accountClassCode)
        {
            short _parentAccountClassPrmKey= (context.AccountClasses
                                    .Where(a => a.AccountClassCode == _accountClassCode)
                                    .Select(a => a.PrmKey).FirstOrDefault());

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from g in context.GeneralLedgers
                        join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals mf.GeneralLedgerPrmKey into gm
                        from mf in gm.DefaultIfEmpty()
                        join t in context.GeneralLedgerTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on g.PrmKey equals t.GeneralLedgerPrmKey into gt from t in gt.DefaultIfEmpty()
                        join a in context.AccountClasses.Where(a => a.ActivationStatus == StringLiteralValue.Active) on g.AccountClassPrmKey equals a.PrmKey into ag from a in ag.DefaultIfEmpty()
                        where g.EntryStatus == StringLiteralValue.Verify
                              && g.ActivationStatus == StringLiteralValue.Active
                              && a.ParentPrmKey == _parentAccountClassPrmKey
                              && t.LanguagePrmKey == regionalLanguagePrmKey
                        select new SelectListItem
                        {
                            Value = g.GeneralLedgerId.ToString(),
                            Text = (mf.NameOfGL ?? g.NameOfGL.Trim()) + " ---> " + (t.TransNameOfGL ?? "")  // Using == operator for null handling
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            // Default List In Default Language (i.e. English)
            return (from g in context.GeneralLedgers
                    join mf in context.GeneralLedgerModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify)
                        on g.PrmKey equals mf.GeneralLedgerPrmKey into gm
                    from mf in gm.DefaultIfEmpty()
                    join a in context.AccountClasses.Where(a => a.ActivationStatus == StringLiteralValue.Active)
                        on g.AccountClassPrmKey equals a.PrmKey into ag
                    from a in ag.DefaultIfEmpty()
                    where g.EntryStatus == StringLiteralValue.Verify
                          && g.ActivationStatus == StringLiteralValue.Active
                        && a.ParentPrmKey == _parentAccountClassPrmKey
                    select new SelectListItem
                    {
                        Value = g.GeneralLedgerId.ToString(),
                        Text = mf.NameOfGL ?? g.NameOfGL.Trim()  // Using ?? to check for null values.
                    }).Distinct()
                      .OrderBy(l => l.Text)
                      .ToList();
        }

        //Educational Course Dropdown
        public List<SelectListItem> GetEducationalCourseDropdownListBySchemePrmKey(short _schemePrmKey, bool _isApplicableAllUniversities)
        {
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                if (_isApplicableAllUniversities)
                {
                    return (from e in context.EducationalCourses
                            join t in context.EducationalCourseTranslations on e.PrmKey equals t.EducationalCoursePrmKey into vef
                            from t in vef.DefaultIfEmpty()
                            where (e.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = e.EducationalCourseId.ToString(),
                                Text = (e.NameOfCourse ?? e.NameOfCourse.Trim()) + " ---> " + (t.TransNameOfCourse.Trim() ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Get All Valid Selectlist From Educatonal Course            
                return (from e in context.EducationalCourses
                        join t in context.EducationalCourseTranslations on e.PrmKey equals t.EducationalCoursePrmKey into vef
                        from t in vef.DefaultIfEmpty()
                        join s in context.SchemeEducationalCourses.Where(s => s.EntryStatus == EntryStatus.Verified) on e.PrmKey equals s.EducationalCoursePrmKey into vmt
                        from s in vmt.DefaultIfEmpty()
                        where (s.SchemePrmKey == _schemePrmKey)
                       && (e.ActivationStatus == StringLiteralValue.Active)
                       && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = e.EducationalCourseId.ToString(),
                            Text = (e.NameOfCourse ?? e.NameOfCourse.Trim()) + " ---> " + (t.TransNameOfCourse.Trim() ?? "")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            // Default List In Default Language (i.e. English)
            if (_isApplicableAllUniversities)
            {
                return (from e in context.EducationalCourses
                        join s in context.SchemeEducationalCourses.Where(s => s.EntryStatus == EntryStatus.Verified) on e.PrmKey equals s.EducationalCoursePrmKey into vmt
                        from s in vmt.DefaultIfEmpty()
                        where (e.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = e.EducationalCourseId.ToString(),
                            Text = (e.NameOfCourse ?? e.NameOfCourse.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
            return (from e in context.EducationalCourses
                    join s in context.SchemeEducationalCourses.Where(s => s.EntryStatus == EntryStatus.Verified) on e.PrmKey equals s.EducationalCoursePrmKey into vmt
                    from s in vmt.DefaultIfEmpty()
                    where (s.SchemePrmKey == _schemePrmKey)
                   && (e.ActivationStatus == StringLiteralValue.Active)
                    select new SelectListItem
                    {
                        Value = e.EducationalCourseId.ToString(),
                        Text = (e.NameOfCourse ?? e.NameOfCourse.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        // Institute Dropdown
        public List<SelectListItem> GetInstituteDropdownListBySchemePrmKey(short _schemePrmKey, bool _isApplicableAllCourse)
        {
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                if (_isApplicableAllCourse)
                {
                    return (from e in context.Institutes
                            join t in context.InstituteTranslations on e.PrmKey equals t.InstitutePrmKey into vef
                            from t in vef.DefaultIfEmpty()
                            where (e.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = e.InstituteId.ToString(),
                                Text = (e.NameOfInstitute ?? e.NameOfInstitute.Trim()) + " ---> " + (t.TransNameOfInstitute.Trim() ?? "")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Get All Valid Selectlist From Institutes            
                return (from e in context.Institutes
                        join t in context.InstituteTranslations on e.PrmKey equals t.InstitutePrmKey into vef
                        from t in vef.DefaultIfEmpty()
                        join s in context.SchemeInstitutes.Where(s => s.EntryStatus == EntryStatus.Verified) on e.PrmKey equals s.InstitutePrmKey into vmt
                        from s in vmt.DefaultIfEmpty()
                        where (s.SchemePrmKey == _schemePrmKey)
                       && (e.ActivationStatus == StringLiteralValue.Active)
                       && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = e.InstituteId.ToString(),
                            Text = (e.NameOfInstitute ?? e.NameOfInstitute.Trim()) + " ---> " + (t.TransNameOfInstitute.Trim() ?? "")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            if (_isApplicableAllCourse)
            {
                // Default List In Default Language (i.e. English)
                return (from e in context.Institutes
                        join s in context.SchemeInstitutes.Where(s => s.EntryStatus == EntryStatus.Verified) on e.PrmKey equals s.InstitutePrmKey into vmt
                        from s in vmt.DefaultIfEmpty()
                        where (e.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = e.InstituteId.ToString(),
                            Text = (e.NameOfInstitute ?? e.NameOfInstitute.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
            // Default List In Default Language (i.e. English)
            return (from e in context.Institutes
                    join s in context.SchemeInstitutes.Where(s => s.EntryStatus == EntryStatus.Verified) on e.PrmKey equals s.InstitutePrmKey into vmt
                    from s in vmt.DefaultIfEmpty()
                    where (s.SchemePrmKey == _schemePrmKey)
                   && (e.ActivationStatus == StringLiteralValue.Active)
                    select new SelectListItem
                    {
                        Value = e.InstituteId.ToString(),
                        Text = (e.NameOfInstitute ?? e.NameOfInstitute.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }
        // Shifted To EFPersonDetailRepository

        //public List<SelectListItem> GetPersonDropdownListForLoanAccountOpening(short _schemePrmkey)
        //{
        //    short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

        //    List<SelectListItem> selectListItem = new List<SelectListItem>();

        //    SchemeLoanAccountParameterViewModel schemeLoanAccountParameterViewModel = new SchemeLoanAccountParameterViewModel();

        //    schemeLoanAccountParameterViewModel = context.Database.SqlQuery<SchemeLoanAccountParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanAccountParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmkey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).Distinct().FirstOrDefault();
        //    string sysNameOfMemberType;

        //    if (schemeLoanAccountParameterViewModel.IsRequiredOrdinaryMembership)
        //    {
        //        sysNameOfMemberType = "ORD";
        //    }
        //    else
        //    {
        //        sysNameOfMemberType = "NOM";
        //    }


        //    byte memberTypePrmKey = GetMemberTypePrmKeyBySysName(sysNameOfMemberType);

        //    short savingAccountClassPrmKey = GetSavingAccountClassPrmKey();

        //    byte minimumAge = schemeLoanAccountParameterViewModel.MinimumAge;
        //    byte maximumAge = schemeLoanAccountParameterViewModel.MaximumAge;


        //    // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
        //    if (regionalLanguagePrmKey != 1)
        //    {
        //        selectListItem = (from p in context.People
        //                join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
        //                from mf in pm.DefaultIfEmpty()
        //                join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
        //                from t in pt.DefaultIfEmpty()
        //                join s in context.PersonStatuses.Where(s => s.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals s.PersonPrmKey into ps
        //                from s in ps.DefaultIfEmpty()
        //                join c in context.CustomerAccountDetails.Where(c => c.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals c.PersonPrmKey into pc
        //                from c in pc.DefaultIfEmpty()
        //                join g in context.GeneralLedgers .Where(g=>g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on c.GeneralLedgerPrmKey equals g.PrmKey into cg
        //                from g in cg.DefaultIfEmpty()
        //                where (p.EntryStatus == StringLiteralValue.Verify)
        //                && (p.ActivationStatus == StringLiteralValue.Active)                      
        //                && (t.LanguagePrmKey == regionalLanguagePrmKey)
        //                && (s.MemberTypePrmKey == memberTypePrmKey)
        //                select new SelectListItem
        //                {
        //                    Value = p.PersonId.ToString(),
        //                    Text = (mf.FullName ?? p.FullName.Trim()) + " ---> " + (t.TransFullName.Trim() ?? " ")
        //                }).Distinct().OrderBy(l => l.Text).ToList();
        //    }

        //    // Default List In Default Language (i.e. English)
        //    selectListItem =  (from p in context.People
        //            join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
        //            from mf in pm.DefaultIfEmpty()
        //            join s in context.PersonStatuses.Where(s => s.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals s.PersonPrmKey into ps
        //            from s in ps.DefaultIfEmpty()
        //            join c in context.CustomerAccountDetails.Where(c => c.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals c.PersonPrmKey into pc
        //            from c in pc.DefaultIfEmpty()
        //            where (p.EntryStatus == StringLiteralValue.Verify)
        //            && (p.ActivationStatus == StringLiteralValue.Active)
        //            && (s.MemberTypePrmKey == memberTypePrmKey)
        //            where (p.EntryStatus == StringLiteralValue.Verify)
        //            && (p.ActivationStatus == StringLiteralValue.Active)
        //            select new SelectListItem
        //            {
        //                Value = p.PersonId.ToString(),
        //                Text = ((mf.FullName) ?? p.FullName.Trim())
        //            }).Distinct().OrderBy(l => l.Text).ToList();

        //    // Default List In Default Language (i.e. English)
        //    var ab = (from p in context.People
        //              join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
        //              from mf in pm.DefaultIfEmpty()
        //              join s in context.PersonStatuses.Where(s => s.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals s.PersonPrmKey into ps
        //              from s in ps.DefaultIfEmpty()
        //              join c in context.CustomerAccountDetails.Where(c => c.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals c.PersonPrmKey into pc
        //              from c in pc.DefaultIfEmpty()
        //              join g in context.GeneralLedgers.Where(g => g.EntryStatus == StringLiteralValue.Verify && g.ActivationStatus == StringLiteralValue.Active) on c.GeneralLedgerPrmKey equals g.PrmKey into cg
        //              from g in cg.DefaultIfEmpty()
        //              where (p.EntryStatus == StringLiteralValue.Verify)
        //              && (p.ActivationStatus == StringLiteralValue.Active)
        //              && (s.MemberTypePrmKey == memberTypePrmKey)
        //              where (p.EntryStatus == StringLiteralValue.Verify)
        //              && (p.ActivationStatus == StringLiteralValue.Active)
        //              select new
        //              {
        //                  Value = p.PersonId.ToString(),
        //                  Text = ((mf.FullName) ?? p.FullName.Trim()),
        //                  GeneralLedger = c.GeneralLedgerPrmKey,
        //                  AccountClass = g.AccountClassPrmKey,
        //                  Age = configurationDetailRepository.GetAge(p.DateOfBirthOnDocument)
        //              }).Distinct().OrderBy(l => l.Text).ToList().AsQueryable();


        //    // Get Only Saving List
        //    if (schemeLoanAccountParameterViewModel.IsRequiredSavingAccount)
        //    {
        //        ab = ab.Where(l => l.AccountClass == savingAccountClassPrmKey);
        //    }

        //    // Get List Between Age
        //    if(schemeLoanAccountParameterViewModel.MaximumAge > 0)
        //    {
        //        ab = ab.Where(l => l.Age >= schemeLoanAccountParameterViewModel.MinimumAge && l.Age <= schemeLoanAccountParameterViewModel.MaximumAge);
        //    }

        //    selectListItem = (List<SelectListItem>) from a in ab
        //                     select new SelectListItem
        //                     {
        //                         Value = a.Value.ToString(),
        //                         Text = a.Text
        //                     };

        //    return selectListItem;
        //}
    }
}
