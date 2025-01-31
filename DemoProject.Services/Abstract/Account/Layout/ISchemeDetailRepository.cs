using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.Account.Layout;

namespace DemoProject.Services.Abstract.Account.Layout
{
    public interface ISchemeDetailRepository
    {
        Task<IEnumerable<SchemeBusinessOfficeViewModel>> GetBusinessOfficeEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeChargesDetailViewModel>> GetChargesDetailEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeClosingChargesViewModel>> GetClosingChargesEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeConsumerDurableLoanItemViewModel>> GetSchemeConsumerDurableLoanItemEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeDepositAgentIncentiveViewModel>> GetAgentIncentiveEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeDocumentViewModel>> GetDocumentEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeGeneralLedgerViewModel>> GetGeneralLedgerEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeLoanChargesParameterViewModel>> GetLoanChargesParameterEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeLoanOverduesActionViewModel>> GetLoanOverduesActionEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeNoticeScheduleViewModel>> GetNoticeScheduleEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeNumberOfTransactionLimitViewModel>> GetNumberOfTransactionLimitEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemePreownedVehicleLoanParameterViewModel>> GetSchemePreownedVehicleLoanParameterEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeReportFormatViewModel>> GetReportFormatEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeSharesTransferChargesViewModel>> GetSharesTransferChargesEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeTargetGroupViewModel>> GetTargetGroupEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeTenureListViewModel>> GetTenureListEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeTransactionAmountLimitViewModel>> GetTransactionAmountLimitEntries(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeVehicleTypeLoanParameterViewModel>> GetSchemeVehicleTypeLoanParameterEntries(short _schemePrmKey, string _entryType);


        Task<CustomerDepositAccountOpeningConfigViewModel> GetDepositSchemeConfigDetail(Guid _schemeId);

        Task<CustomerSharesAccountOpeningConfigViewModel> GetSharesCapitalSchemeConfigDetail(Guid _schemeId);

        Task<SchemeAccountBankingChannelParameterViewModel> GetAccountBankingChannelParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeAccountParameterViewModel> GetAccountParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeApplicationParameterViewModel> GetApplicationParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeCashCreditLoanParameterViewModel> GetSchemeCashCreditLoanParameterEntry(short _schemePrmKey, string _entryType);
        
        Task<SchemeEducationLoanParameterViewModel> GetSchemeEducationLoanParameterEntry(short _schemePrmKey, string _entryType);
        Task<IEnumerable<SchemeEducationalCourseViewModel>> GetSchemeEducationalCourseEntries(short _schemePrmKey, string _entryType);
        Task<IEnumerable<SchemeInstituteViewModel>> GetSchemeInstituteEntries(short _schemePrmKey, string _entryType);

        Task<SchemeHomeLoanViewModel> GetSchemeHomeLoanEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanAgainstPropertyViewModel> GetSchemeLoanAgainstPropertyEntry(short _schemePrmKey, string _entryType);

        Task<SchemeBusinessLoanViewModel> GetSchemeBusinessLoanEntry(short _schemePrmKey, string _entryType);

        Task<SchemeCustomerAccountNumberViewModel> GetCustomerAccountNumberEntry(short _schemePrmKey, string _entryType);

        Task<SchemeDemandDepositDetailViewModel> GetDemandDepositDetailEntry(short _schemePrmKey, string _entryType);

        Task<SchemeDepositAccountClosureParameterViewModel> GetAccountClosureParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeDepositAccountParameterViewModel> GetDepositAccountParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeDepositAccountRenewalParameterViewModel> GetAccountRenewalParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeDepositAgentParameterViewModel> GetAgentParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeDepositCertificateParameterViewModel> GetCertificateParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeDepositInstallmentParameterViewModel> GetInstallmentParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeDepositInterestParameterViewModel> GetDepositInterestParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeDepositInterestProvisionParameterViewModel> GetInterestProvisionParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeDepositPledgeLoanParameterViewModel> GetPledgeLoanParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeEstimateTargetViewModel> GetEstimateTargetEntry(short _schemePrmKey, string _entryType);

        Task<SchemeFixedDepositParameterViewModel> GetFixedDepositParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeGoldLoanParameterViewModel> GetGoldLoanParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeInterestRateViewModel> GetInterestRateEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLimitViewModel> GetLimitEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanAccountParameterViewModel> GetLoanAccountParameterEntry(short _schemePrmKey, string _entryType);

        Task<IEnumerable<SchemeLoanAgainstDepositGeneralLedgerViewModel>> GetLoanAgainstDepositGeneralLedgerEntries(short _schemePrmKey, string _entryType);

        Task<SchemeLoanAgainstDepositParameterViewModel> GetLoanAgainstDepositParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanAgreementNumberViewModel> GetLoanAgreementNumberEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanAgreementNumberViewModel> GetAgreementNumberEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanArrearParameterViewModel> GetLoanArrearParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanDistributorParameterViewModel> GetLoanDistributorParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanFineInterestParameterViewModel> GetLoanFineInterestParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanFunderParameterViewModel> GetLoanFunderParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanInstallmentParameterViewModel> GetLoanInstallmentParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanInterestParameterViewModel> GetLoanInterestParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanInterestProvisionParameterViewModel> GetLoanInterestProvisionParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanInterestRebateParameterViewModel> GetLoanInterestRebateParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanPaymentReminderParameterViewModel> GetSchemeLoanPaymentReminderParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanRepaymentScheduleParameterViewModel> GetLoanRepaymentScheduleParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanSanctionAuthorityViewModel> GetLoanSanctionAuthorityEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanSettlementAccountParameterViewModel> GetLoanSettlementAccountParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemePassbookViewModel> GetPassbookEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanPreFullPaymentParameterViewModel> GetPreFullPaymentParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeLoanPrePartPaymentParameterViewModel> GetPrePartPaymentParameterEntry(short _schemePrmKey, string _entryType);

        Task<SchemeSharesCapitalAccountParameterViewModel> GetSharesCapitalAccountParameterEntry(short _SchemePrmKey, string _entryType);

        Task<SchemeSharesCapitalDividendParameterViewModel> GetSharesCapitalDividendParameterEntry(short _SchemePrmKey, string _entryType);

        Task<SchemeSharesCertificateParameterViewModel> GetSharesCertificateParameterEntry(short _SchemePrmKey, string _entryType);

        Task<SchemeTenureViewModel> GetTenureEntry(short _schemePrmKey, string _entryType);

        Task<CustomerLoanAccountOpeningConfigViewModel> GetCustomerLoanAccountConfigDetail(Guid _schemeId);

        Task<SchemeDocumentViewModel> GetDocumentEntry(short _schemePrmKey, short _documentPrmKey, string _entryType);
    }
}
