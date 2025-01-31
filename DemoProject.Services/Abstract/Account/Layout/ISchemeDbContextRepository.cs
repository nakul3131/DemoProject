using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Layout;

namespace DemoProject.Services.Abstract.Account.Layout
{
    public interface ISchemeDbContextRepository
    {
        bool AttachSharesCapitalSchemeData(SharesCapitalSchemeViewModel _sharesCapitalSchemeViewModel, string _entryType);

        bool AttachSchemeAccountParameterData(SchemeAccountParameterViewModel _schemeAccountParameterViewModel, string _entryType);

        bool AttachSchemeSharesCapitalAccountParameterData(SchemeSharesCapitalAccountParameterViewModel _schemeSharesCapitalAccountParameterViewModel, string _entryType);

        bool AttachSchemeCustomerAccountNumberData(SchemeCustomerAccountNumberViewModel _schemeCustomerAccountNumberViewModel, string _entryType);

        bool AttachSchemeSharesCertificateParameterData(SchemeSharesCertificateParameterViewModel _schemeSharesCertificateParameterViewModel, string _entryType);

        bool AttachSchemeApplicationParameterData(SchemeApplicationParameterViewModel _schemeApplicationParameterViewModel, string _entryType);

        bool AttachSchemeAccountBankingChannelParameterData(SchemeAccountBankingChannelParameterViewModel _schemeAccountBankingChannelParameterViewModel, string _entryType);

        bool AttachSchemeSharesCapitalDividendParameterData(SchemeSharesCapitalDividendParameterViewModel _schemeSharesCapitalDividendParameterViewModel, string _entryType);

        bool AttachSchemeClosingChargesData(SchemeClosingChargesViewModel _schemeClosingChargesViewModel, string _entryType);

        bool AttachSchemeSharesTransferChargesData(SchemeSharesTransferChargesViewModel _schemeSharesTransferChargesViewModel, string _entryType);

        bool AttachSchemeNoticeScheduleData(SchemeNoticeScheduleViewModel _schemeNoticeScheduleViewModel, string _entryType);

        bool AttachSchemeReportFormatData(SchemeReportFormatViewModel _schemeReportFormatViewModel, string _entryType);

        bool AttachSchemeEstimateTargetData(SchemeEstimateTargetViewModel _schemeEstimateTargetViewModel, string _entryType);

        bool AttachSchemeGeneralLedgerData(SchemeGeneralLedgerViewModel schemeGeneralLedgerViewModel, string _entryType);

        bool AttachSchemeLimitData(SchemeLimitViewModel _schemeLimitViewModel, string _entryType);

        bool AttachSchemeBusinessOfficeData(SchemeBusinessOfficeViewModel _schemeBusinessOfficeViewModel, string _entryType);


        // Deposit Scheme 
        bool AttachDepositSchemeData(DepositSchemeViewModel _depositSchemeViewModel, string _entryType);

        bool AttachSchemeDepositAccountParameterData(SchemeDepositAccountParameterViewModel _schemeDepositAccountParameterViewModel, string _entryType);

        bool AttachSchemeDepositInstallmentParameterData(SchemeDepositInstallmentParameterViewModel _schemeDepositInstallmentParameterViewModel, string _entryType);

        bool AttachSchemeDepositAgentParameterData(SchemeDepositAgentParameterViewModel _schemeDepositAgentParameterViewModel, string _entryType);

        bool AttachSchemeDepositAgentIncentiveData(SchemeDepositAgentIncentiveViewModel _schemeDepositAgentIncentiveViewModel, string _entryType);

        bool AttachSchemeDepositInterestParameterData(SchemeDepositInterestParameterViewModel _schemeDepositInterestParameterViewModel, string _entryType);

        bool AttachSchemeDepositInterestProvisionParameterData(SchemeDepositInterestProvisionParameterViewModel _schemeDepositInterestProvisionParameterViewModel, string _entryType);

        bool AttachSchemeNumberOfTransactionLimitData(SchemeNumberOfTransactionLimitViewModel _schemeNumberOfTransactionLimitViewModel, string _entryType);

        bool AttachSchemeTransactionAmountLimitData(SchemeTransactionAmountLimitViewModel _schemeTransactionAmountLimitViewModel, string _entryType);

        bool AttachSchemeDepositCertificateParameterData(SchemeDepositCertificateParameterViewModel _schemeDepositCertificateParameterViewModel, string _entryType);

        bool AttachSchemeDemandDepositDetailData(SchemeDemandDepositDetailViewModel _schemeDemandDepositDetailViewModel, string _entryType);

        bool AttachSchemeFixedDepositParameterData(SchemeFixedDepositParameterViewModel _schemeFixedDepositParameterViewModel, string _entryType);

        bool AttachSchemeDepositPledgeLoanParameterData(SchemeDepositPledgeLoanParameterViewModel _schemeDepositPledgeLoanParameterViewModel, string _entryType);

        bool AttachSchemeDepositAccountRenewalParameterData(SchemeDepositAccountRenewalParameterViewModel _schemeDepositAccountRenewalParameterViewModel, string _entryType);

        bool AttachSchemePassbookData(SchemePassbookViewModel _schemePassbookViewModel, string _entryType);

        bool AttachSchemeDepositAccountClosureParameterData(SchemeDepositAccountClosureParameterViewModel _schemeDepositAccountClosureParameterViewModel, string _entryType);

        bool AttachSchemeTenureListData(SchemeTenureListViewModel _schemeTenureListViewModel, string _entryType);

        bool AttachSchemeTenureData(SchemeTenureViewModel schemeTenureViewModel, string _entryType);

        // Loan Scheme 
        bool AttachLoanSchemeData(LoanSchemeViewModel _loanSchemeViewModel, string _entryType);

        bool AttachSchemeLoanAccountParameterData(SchemeLoanAccountParameterViewModel _schemeLoanAccountParameterViewModel, string _entryType);

        bool AttachSchemeDocumentData(SchemeDocumentViewModel _schemeDocumentViewModel, string _entryType);

        bool AttachSchemeTargetGroupData(SchemeTargetGroupViewModel _schemeTargetGroupViewModel, string _entryType);

        bool AttachSchemeLoanRepaymentScheduleParameterData(SchemeLoanRepaymentScheduleParameterViewModel _schemeLoanRepaymentScheduleParameterViewModel, string _entryType);

        bool AttachSchemeLoanSettlementAccountParameterData(SchemeLoanSettlementAccountParameterViewModel _schemeLoanSettlementAccountParameterViewModel, string _entryType);

        bool AttachSchemeLoanInterestParameterData(SchemeLoanInterestParameterViewModel _schemeLoanInterestParameterViewModel, string _entryType);


        bool AttachSchemeLoanSanctionAuthorityData(SchemeLoanSanctionAuthorityViewModel _schemeLoanSanctionAuthorityViewModel, string _entryType);

        bool AttachSchemeCashCreditLoanParameterData(SchemeCashCreditLoanParameterViewModel _schemeCashCreditLoanParameterViewModel, string _entryType);
        
        bool AttachSchemeEducationLoanParameterData(SchemeEducationLoanParameterViewModel _schemeEducationLoanParameterViewModel, string _entryType);

        bool AttachSchemeEducationalCourseData(SchemeEducationalCourseViewModel _schemeEducationalCourseViewModel, string _entryType);
        bool AttachSchemeInstituteData(SchemeInstituteViewModel _schemeInstituteViewModelViewModel, string _entryType);

        bool AttachSchemeLoanPaymentReminderParameterData(SchemeLoanPaymentReminderParameterViewModel _schemeLoanPaymentReminderParameterViewModel, string _entryType);

        bool AttachSchemePreownedVehicleLoanParameterData(SchemePreownedVehicleLoanParameterViewModel _schemePreownedVehicleLoanParameterViewModel, string _entryType);
       
        bool AttachSchemeVehicleTypeLoanParameterData(SchemeVehicleTypeLoanParameterViewModel _schemeVehicleTypeLoanParameterViewModel, string _entryType);

        bool AttachSchemeConsumerDurableLoanItemData(SchemeConsumerDurableLoanItemViewModel _schemeConsumerDurableLoanItemViewModel, string _entryType);

        bool AttachSchemeLoanAgainstDepositParameterData(SchemeLoanAgainstDepositParameterViewModel _schemeLoanAgainstDepositParameterViewModel, string _entryType);

        bool AttachSchemeLoanAgainstDepositGeneralLedgerData(SchemeLoanAgainstDepositGeneralLedgerViewModel _schemeLoanAgainstDepositGeneralLedgerViewModel, string _entryType);

        bool AttachSchemeHomeLoanData(SchemeHomeLoanViewModel _schemeHomeLoanViewModel, string _entryType);

        bool AttachSchemeLoanAgainstPropertyData(SchemeLoanAgainstPropertyViewModel _schemeLoanAgainstPropertyViewModel, string _entryType);

        bool AttachSchemeBusinessLoanData(SchemeBusinessLoanViewModel _schemeBusinessLoanViewModel, string _entryType);

        bool AttachSchemeInterestRateData(SchemeInterestRateViewModel _schemeInterestRateViewModel, string _entryType);

        bool AttachSchemeLoanFineInterestParameterData(SchemeLoanFineInterestParameterViewModel _schemeLoanFineInterestParameterViewModel, string _entryType);

        bool AttachSchemeLoanInterestProvisionParameterData(SchemeLoanInterestProvisionParameterViewModel _schemeLoanInterestProvisionParameterViewModel, string _entryType);

        bool AttachSchemeLoanDistributorParameterData(SchemeLoanDistributorParameterViewModel _schemeLoanDistributorParameterViewModel, string _entryType);

        bool AttachSchemeLoanArrearParameterData(SchemeLoanArrearParameterViewModel _schemeLoanArrearParameterViewModel, string _entryType);

        bool AttachSchemeLoanChargesParameterData(SchemeLoanChargesParameterViewModel _schemeLoanChargesParameterViewModel, string _entryType);

        bool AttachSchemeLoanInterestRebateParameterData(SchemeLoanInterestRebateParameterViewModel _schemeLoanInterestRebateParameterViewModel, string _entryType);

        bool AttachSchemeLoanInstallmentParameterData(SchemeLoanInstallmentParameterViewModel _schemeLoanInstallmentParameterViewModel, string _entryType);

        bool AttachSchemeLoanFunderParameterData(SchemeLoanFunderParameterViewModel _schemeLoanFunderParameterViewModel, string _entryType);

        bool AttachSchemeLoanOverduesActionData(SchemeLoanOverduesActionViewModel _schemeLoanOverduesActionViewModel, string _entryType);

        bool AttachSchemePreFullPaymentParameterData(SchemeLoanPreFullPaymentParameterViewModel _schemePreFullPaymentParameterViewModel, string _entryType);

        bool AttachSchemePrePartPaymentParameterData(SchemeLoanPrePartPaymentParameterViewModel _schemePrePartPaymentParameterViewModel, string _entryType);

        bool AttachSchemeLoanAgreementNumberData(SchemeLoanAgreementNumberViewModel _schemeLoanAgreementNumberViewModel, string _entryType);

        bool AttachSchemeGoldLoanParameterData(SchemeGoldLoanParameterViewModel _schemeGoldLoanParameterViewModel, string _entryType);

        Task<bool> SaveData();
    }
}
