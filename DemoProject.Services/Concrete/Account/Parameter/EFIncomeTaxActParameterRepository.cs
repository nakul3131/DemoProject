using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.Services.Concrete.Account.Parameter
{
    public class EFIncomeTaxActParameterRepository : IIncomeTaxActParameterRepository
    {
        private readonly EFDbContext context;

        public EFIncomeTaxActParameterRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

public async Task<IncomeTaxActParameterViewModel> GetActiveEntry()
    {
        try
        {
            return await context.IncomeTaxActParameters
                .Where(x => x.ActivationStatus == "ACT")
                .Select(x => new IncomeTaxActParameterViewModel
                {
                    PrmKey = x.PrmKey,
                    EffectiveDate = x.EffectiveDate,
                    AcceptanceOfCashLoansOrDeposits = x.AcceptanceOfCashLoansOrDeposits,
                    RepaymentOfCashLoansOrDeposits = x.RepaymentOfCashLoansOrDeposits,
                    CashTransactionLimitsPerDayForIncomeTaxReporting = x.CashTransactionLimitsPerDayForIncomeTaxReporting,
                    CashTransactionLimitsPerYearForIncomeTaxReporting = x.CashTransactionLimitsPerYearForIncomeTaxReporting,
                    CashWithdrawalTDSLimitForFilers = x.CashWithdrawalTDSLimitForFilers,
                    FilersTDSPercentageForCashWithdrawal = x.FilersTDSPercentageForCashWithdrawal,
                    CashWithdrawalTDSLimitForNonFilersSlab1 = x.CashWithdrawalTDSLimitForNonFilersSlab1,
                    NonFilersTDSPercentageForCashWithdrawalSlab1 = x.NonFilersTDSPercentageForCashWithdrawalSlab1,
                    CashWithdrawalTDSLimitForNonFilersSlab2 = x.CashWithdrawalTDSLimitForNonFilersSlab2,
                    NonFilersTDSPercentageForCashWithdrawalSlab2 = x.NonFilersTDSPercentageForCashWithdrawalSlab2,
                    CashPaymentLimitForExpensesAndAssets = x.CashPaymentLimitForExpensesAndAssets,
                    DepositInterestThresholdLimitInFiscalYear = x.DepositInterestThresholdLimitInFiscalYear,
                    DepositInterestThresholdLimitForSeniorCitizenInFiscalYear = x.DepositInterestThresholdLimitForSeniorCitizenInFiscalYear,
                    TDSRateOnDepositInterestForPANHolder = x.TDSRateOnDepositInterestForPANHolder,
                    TDSRateOnDepositInterestForNonPANHolder = x.TDSRateOnDepositInterestForNonPANHolder,
                    Note = x.Note,
                    ActivationStatus = x.ActivationStatus,
                })
                .FirstOrDefaultAsync(); 
            
        }
        catch (Exception ex)
        {
            string error = ex.Message;
            return null; 
        }
    }

}
}
