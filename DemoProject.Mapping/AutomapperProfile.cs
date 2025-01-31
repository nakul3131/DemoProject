using AutoMapper;
using DemoProject.Domain.Entities.Enterprise.Establishment;
using DemoProject.Domain.Entities.Enterprise.Office;
using DemoProject.Domain.Entities.Enterprise.Schedule;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.Services.ViewModel.Enterprise.Schedule;
using DemoProject.Services.ViewModel.Parameter.Security;
using DemoProject.Services.ViewModel.Security.Password;
using DemoProject.Services.ViewModel.Account.GL;
using DemoProject.Domain.Entities.Account.GL;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Domain.Entities.PersonInformation;
using DemoProject.Services.ViewModel.Security.UserRoles;
using DemoProject.Domain.Entities.Security.UserRoles;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Domain.Entities.Account.Layout;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.ViewModel.Management.Conference;
using DemoProject.Domain.Entities.Management.Conference;
using DemoProject.Domain.Entities.Account.Customer;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Domain.Entities.Account.Parameter;
using DemoProject.Domain.Entities.Account.Master;
using DemoProject.Domain.Entities.Account.Transaction;
using DemoProject.Services.ViewModel.Account.Master;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.Services.ViewModel.Management.Servant;
using DemoProject.Domain.Entities.Management.Servant;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Domain.Entities.Management.Master;
using DemoProject.Domain.Entities.PersonInformation.Master;
using DemoProject.Domain.Entities.Management.SystemEntity;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.ViewModel.Management.Parameter;
using DemoProject.Domain.Entities.Management.Parameter;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Domain.Entities.PersonInformation.Parameter;
using DemoProject.Domain.Entities.Security.Master;
using DemoProject.Domain.Entities.Security.Parameter;
using DemoProject.Services.ViewModel.Configuration;
using DemoProject.Domain.Entities.Configuration;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ A C C O U N T @@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            // ************************** Customer **************************         
            CreateMap<SharesCapitalCustomerAccountViewModel, CustomerAccount>().ReverseMap();
            CreateMap<SharesCapitalCustomerAccountViewModel, CustomerAccountMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalCustomerAccountViewModel, CustomerAccountModification>().ReverseMap();
            CreateMap<SharesCapitalCustomerAccountViewModel, CustomerAccountModificationMakerChecker>().ReverseMap();

            CreateMap<CustomerSharesCapitalAccountViewModel, CustomerSharesCapitalAccount>().ReverseMap();
            CreateMap<CustomerSharesCapitalAccountViewModel, CustomerSharesCapitalAccountMakerChecker>().ReverseMap();

            CreateMap<DepositCustomerAccountViewModel, CustomerAccount>().ReverseMap();
            CreateMap<DepositCustomerAccountViewModel, CustomerAccountMakerChecker>().ReverseMap();

            CreateMap<DepositCustomerAccountViewModel, CustomerAccountModification>().ReverseMap();
            CreateMap<DepositCustomerAccountViewModel, CustomerAccountModificationMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountDetailViewModel, CustomerAccountDetail>().ReverseMap();
            CreateMap<CustomerAccountDetailViewModel, CustomerAccountDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerDepositAccountViewModel, CustomerDepositAccount>().ReverseMap();
            CreateMap<CustomerDepositAccountViewModel, CustomerDepositAccountMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountNomineeViewModel, CustomerAccountNominee>().ReverseMap();
            CreateMap<CustomerAccountNomineeViewModel, CustomerAccountNomineeMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountNomineeViewModel, CustomerAccountNomineeTranslation>().ReverseMap();
            CreateMap<CustomerAccountNomineeViewModel, CustomerAccountNomineeTranslationMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountNomineeGuardianViewModel, CustomerAccountNomineeGuardian>().ReverseMap();
            CreateMap<CustomerAccountNomineeGuardianMakerChecker, CustomerAccountNomineeGuardianViewModel>().ReverseMap();

            CreateMap<CustomerAccountNomineeGuardianViewModel, CustomerAccountNomineeGuardianTranslation>().ReverseMap();
            CreateMap<CustomerAccountNomineeGuardianViewModel, CustomerAccountNomineeGuardianTranslationMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountNomineeViewModel, CustomerAccountNomineeGuardian>().ReverseMap();
            CreateMap<CustomerAccountNomineeViewModel, CustomerAccountNomineeGuardianMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountNomineeViewModel, CustomerAccountNomineeGuardianTranslation>().ReverseMap();
            CreateMap<CustomerAccountNomineeViewModel, CustomerAccountNomineeGuardianTranslationMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountNoticeScheduleViewModel, CustomerAccountNoticeSchedule>().ReverseMap();
            CreateMap<CustomerAccountNoticeScheduleViewModel, CustomerAccountNoticeScheduleMakerChecker>().ReverseMap();

            CreateMap<CustomerJointAccountHolderViewModel, CustomerJointAccountHolder>().ReverseMap();
            CreateMap<CustomerJointAccountHolderViewModel, CustomerJointAccountHolderMakerChecker>().ReverseMap();

            CreateMap<CustomerTermDepositAccountDetailViewModel, CustomerTermDepositAccountDetail>().ReverseMap();
            CreateMap<CustomerTermDepositAccountDetailViewModel, CustomerTermDepositAccountDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountStandingInstructionViewModel, CustomerAccountStandingInstruction>().ReverseMap();
            CreateMap<CustomerAccountStandingInstructionViewModel, CustomerAccountStandingInstructionMakerChecker>().ReverseMap();

            CreateMap<CustomerDepositAccountAgentViewModel, CustomerDepositAccountAgent>().ReverseMap();
            CreateMap<CustomerDepositAccountAgentViewModel, CustomerDepositAccountAgentMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountTurnOverLimitViewModel, CustomerAccountTurnOverLimit>().ReverseMap();
            CreateMap<CustomerAccountTurnOverLimitViewModel, CustomerAccountTurnOverLimitMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountInterestRateViewModel, CustomerAccountInterestRate>().ReverseMap();
            CreateMap<CustomerAccountInterestRateViewModel, CustomerAccountInterestRateMakerChecker>().ReverseMap();

            CreateMap<CustomerLoanAgainstPropertyCollateralDetailViewModel, CustomerLoanAgainstPropertyCollateralDetail>().ReverseMap();
            CreateMap<CustomerLoanAgainstPropertyCollateralDetailViewModel, CustomerLoanAgainstPropertyCollateralDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerLoanAgainstDepositCollateralDetailViewModel, CustomerLoanAgainstDepositCollateralDetail>().ReverseMap();
            CreateMap<CustomerLoanAgainstDepositCollateralDetailViewModel, CustomerLoanAgainstDepositCollateralDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerBusinessLoanCollateralDetailViewModel, CustomerBusinessLoanCollateralDetail>().ReverseMap();
            CreateMap<CustomerBusinessLoanCollateralDetailViewModel, CustomerBusinessLoanCollateralDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountDocumentViewModel, CustomerAccountDocument>().ReverseMap();
            CreateMap<CustomerAccountDocumentViewModel, CustomerAccountDocumentMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountReferencePersonDetailViewModel, CustomerAccountReferencePersonDetail>().ReverseMap();
            CreateMap<CustomerAccountReferencePersonDetailViewModel, CustomerAccountReferencePersonDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountSweepDetailViewModel, CustomerAccountSweepDetail>().ReverseMap();
            CreateMap<CustomerAccountSweepDetailViewModel, CustomerAccountSweepDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountPhotoSignViewModel, CustomerAccountPhotoSign>().ReverseMap();
            CreateMap<CustomerAccountPhotoSignViewModel, CustomerAccountPhotoSignMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountChequeDetailViewModel, CustomerAccountChequeDetail>().ReverseMap();
            CreateMap<CustomerAccountChequeDetailViewModel, CustomerAccountChequeDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountBeneficiaryDetailViewModel, CustomerAccountBeneficiaryDetail>().ReverseMap();
            CreateMap<CustomerAccountBeneficiaryDetailViewModel, CustomerAccountBeneficiaryDetailMakerChecker>().ReverseMap();

            CreateMap<LoanCustomerAccountViewModel, CustomerAccount>().ReverseMap();
            CreateMap<LoanCustomerAccountViewModel, CustomerAccountMakerChecker>().ReverseMap();

            CreateMap<LoanCustomerAccountViewModel, CustomerAccountModification>().ReverseMap();
            CreateMap<LoanCustomerAccountViewModel, CustomerAccountModificationMakerChecker>().ReverseMap();

            CreateMap<CustomerLoanAccountViewModel, CustomerLoanAccount>().ReverseMap();
            CreateMap<CustomerLoanAccountViewModel, CustomerLoanAccountMakerChecker>().ReverseMap();

            CreateMap<CustomerLoanAccountViewModel, CustomerLoanAccountTranslation>().ReverseMap();
            CreateMap<CustomerLoanAccountViewModel, CustomerLoanAccountTranslationMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountDetailViewModel, CustomerAccountDetail>().ReverseMap();
            CreateMap<CustomerAccountDetailViewModel, CustomerAccountDetailMakerChecker>().ReverseMap();

            CreateMap<PersonContactDetailViewModel, CustomerAccountContactDetail>().ReverseMap();
            CreateMap<PersonContactDetailViewModel, CustomerAccountContactDetailMakerChecker>().ReverseMap();

            CreateMap<PersonAddressViewModel, CustomerAccountAddressDetail>().ReverseMap();
            CreateMap<PersonAddressViewModel, CustomerAccountAddressDetailMakerChecker>().ReverseMap();

            CreateMap<PersonCourtCaseViewModel, CustomerLoanAccountCourtCaseDetail>().ReverseMap();
            CreateMap<PersonCourtCaseViewModel, CustomerLoanAccountCourtCaseDetailMakerChecker>().ReverseMap();

            CreateMap<PersonIncomeTaxDetailViewModel, CustomerLoanAccountIncomeTaxDetail>().ReverseMap();
            CreateMap<PersonIncomeTaxDetailViewModel, CustomerLoanAccountIncomeTaxDetailMakerChecker>().ReverseMap();

            CreateMap<PersonBorrowingDetailViewModel, CustomerLoanAccountBorrowingDetail>().ReverseMap();
            CreateMap<PersonBorrowingDetailViewModel, CustomerLoanAccountBorrowingDetailMakerChecker>().ReverseMap();

            CreateMap<PersonAdditionalIncomeDetailViewModel, CustomerLoanAccountAdditionalIncomeDetail>().ReverseMap();
            CreateMap<PersonAdditionalIncomeDetailViewModel, CustomerLoanAccountAdditionalIncomeDetailMakerChecker>().ReverseMap();
            
            CreateMap<CustomerGoldLoanCollateralDetailViewModel, CustomerGoldLoanCollateralDetail>().ReverseMap();
            CreateMap<CustomerGoldLoanCollateralDetailViewModel, CustomerGoldLoanCollateralDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerGoldLoanCollateralPhotoViewModel, CustomerGoldLoanCollateralPhoto>().ReverseMap();
            CreateMap<CustomerGoldLoanCollateralPhotoViewModel, CustomerGoldLoanCollateralPhotoMakerChecker>().ReverseMap();

            CreateMap<CustomerVehicleLoanCollateralDetailViewModel, CustomerVehicleLoanCollateralDetail>().ReverseMap();
            CreateMap<CustomerVehicleLoanCollateralDetailViewModel, CustomerVehicleLoanCollateralDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerPreOwnedVehicleLoanInspectionViewModel, CustomerPreOwnedVehicleLoanInspection>().ReverseMap();
            CreateMap<CustomerPreOwnedVehicleLoanInspectionViewModel, CustomerPreOwnedVehicleLoanInspectionMakerChecker>().ReverseMap();

            CreateMap<CustomerVehicleLoanInsuranceDetailViewModel, CustomerVehicleLoanInsuranceDetail>().ReverseMap();
            CreateMap<CustomerVehicleLoanInsuranceDetailViewModel, CustomerVehicleLoanInsuranceDetailMakerChecker>().ReverseMap();
            
            CreateMap<CustomerVehicleLoanPermitDetailViewModel, CustomerVehicleLoanPermitDetail>().ReverseMap();
            CreateMap<CustomerVehicleLoanPermitDetailViewModel, CustomerVehicleLoanPermitDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerVehicleLoanContractDetailViewModel, CustomerVehicleLoanContractDetail>().ReverseMap();
            CreateMap<CustomerVehicleLoanContractDetailViewModel, CustomerVehicleLoanContractDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerLoanAcquaintanceDetailViewModel, CustomerLoanAcquaintanceDetail>().ReverseMap();
            CreateMap<CustomerLoanAcquaintanceDetailViewModel, CustomerLoanAcquaintanceDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerConsumerLoanCollateralDetailViewModel, CustomerConsumerLoanCollateralDetail>().ReverseMap();
            CreateMap<CustomerConsumerLoanCollateralDetailViewModel, CustomerConsumerLoanCollateralDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerLoanFieldInvestigationViewModel, CustomerLoanFieldInvestigation>().ReverseMap();
            CreateMap<CustomerLoanFieldInvestigationViewModel, CustomerLoanFieldInvestigationMakerChecker>().ReverseMap();

            CreateMap<CustomerLoanAccountDebtToIncomeRatioViewModel, CustomerLoanAccountDebtToIncomeRatio>().ReverseMap();
            CreateMap<CustomerLoanAccountDebtToIncomeRatioViewModel, CustomerLoanAccountDebtToIncomeRatioMakerChecker>().ReverseMap();

            CreateMap<CustomerJointAccountHolderViewModel, CustomerJointAccountHolder>().ReverseMap();
            CreateMap<CustomerJointAccountHolderViewModel, CustomerJointAccountHolderMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountNomineeViewModel, CustomerAccountNominee>().ReverseMap();
            CreateMap<CustomerAccountNomineeViewModel, CustomerAccountNomineeMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountNomineeViewModel, CustomerAccountNomineeTranslation>().ReverseMap();
            CreateMap<CustomerAccountNomineeViewModel, CustomerAccountNomineeTranslationMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountNomineeGuardianViewModel, CustomerAccountNomineeGuardian>().ReverseMap();
            CreateMap<CustomerAccountNomineeGuardianViewModel, CustomerAccountNomineeGuardianMakerChecker>().ReverseMap();

            CreateMap<CustomerAccountNomineeGuardianViewModel, CustomerAccountNomineeGuardianTranslation>().ReverseMap();
            CreateMap<CustomerAccountNomineeGuardianViewModel, CustomerAccountNomineeGuardianTranslationMakerChecker>().ReverseMap();

            CreateMap<CustomerLoanAccountGuarantorDetailViewModel, CustomerLoanAccountGuarantorDetail>().ReverseMap();
            CreateMap<CustomerLoanAccountGuarantorDetailViewModel, CustomerLoanAccountGuarantorDetailMakerChecker>().ReverseMap();

            CreateMap<CustomerVehicleLoanPhotoViewModel, CustomerVehicleLoanPhoto>().ReverseMap();
            CreateMap<CustomerVehicleLoanPhotoViewModel, CustomerVehicleLoanPhotoMakerChecker>().ReverseMap();

            CreateMap<CustomerCashCreditLoanAccountViewModel, CustomerCashCreditLoanAccount>().ReverseMap();
            CreateMap<CustomerCashCreditLoanAccountViewModel, CustomerCashCreditLoanAccountMakerChecker>().ReverseMap();
            
            CreateMap<CustomerEducationalLoanDetailViewModel, CustomerEducationalLoanDetail>().ReverseMap();
            CreateMap<CustomerEducationalLoanDetailViewModel, CustomerEducationalLoanDetailMakerChecker>().ReverseMap();
            
            CreateMap<CustomerEducationalLoanDetailViewModel, CustomerEducationalLoanDetailTranslation>().ReverseMap();
            CreateMap<CustomerEducationalLoanDetailViewModel, CustomerEducationalLoanDetailTranslationMakerChecker>().ReverseMap();
            
            // ################### Master ##############
            CreateMap<BeneficiaryDetailViewModel, BeneficiaryDetail>().ReverseMap();
            CreateMap<BeneficiaryDetailViewModel, BeneficiaryDetailMakerChecker>().ReverseMap();

            // ************************** FixedAssetItem **************************
            CreateMap<FixedAssetItemViewModel, FixedAssetItem>().ReverseMap();
            CreateMap<FixedAssetItemViewModel, FixedAssetItemMakerChecker>().ReverseMap();

            CreateMap<FixedAssetItemViewModel, FixedAssetItemTranslation>().ReverseMap();
            CreateMap<FixedAssetItemViewModel, FixedAssetItemTranslationMakerChecker>().ReverseMap();

            CreateMap<FixedAssetItemViewModel, FixedAssetItemModification>().ReverseMap();
            CreateMap<FixedAssetItemViewModel, FixedAssetItemModificationMakerChecker>().ReverseMap();

            // ************************** FinancialCycleAndPeriod **************************
            CreateMap<FinancialCycleViewModel, FinancialCycle>().ReverseMap();
            CreateMap<FinancialCycleViewModel, FinancialCycleMakerChecker>().ReverseMap();

            CreateMap<FinancialCycleViewModel, PeriodCode>().ReverseMap();
            CreateMap<FinancialCycleViewModel, PeriodCodeMakerChecker>().ReverseMap();

            CreateMap<PeriodCodeViewModel, PeriodCode>().ReverseMap();
            CreateMap<PeriodCodeViewModel, PeriodCodeMakerChecker>().ReverseMap();

            // ************************** Shares **************************
            CreateMap<SharesApplicationViewModel, SharesApplication>().ReverseMap();
            CreateMap<SharesApplicationViewModel, SharesApplicationMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, SharesApplicationModification>().ReverseMap();
            CreateMap<SharesApplicationViewModel, SharesApplicationModificationMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, SharesApplicationTranslation>().ReverseMap();
            CreateMap<SharesApplicationViewModel, SharesApplicationTranslationMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, SharesApplicationDetail>().ReverseMap();
            CreateMap<SharesApplicationViewModel, SharesApplicationDetailMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationDetailViewModel, SharesApplicationDetail>().ReverseMap();
            CreateMap<SharesApplicationDetailViewModel, SharesApplicationDetailMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, InwardOutwardType>().ReverseMap();
            CreateMap<SharesApplicationViewModel, InwardOutwardTypeMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, InwardOutwardTypeModification>().ReverseMap();
            CreateMap<SharesApplicationViewModel, InwardOutwardTypeModificationMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, InwardOutwardTypeTranslation>().ReverseMap();
            CreateMap<SharesApplicationViewModel, InwardOutwardTypeTranslationMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, InwardTransaction>().ReverseMap();
            CreateMap<SharesApplicationViewModel, InwardTransactionMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, InwardTransactionTranslation>().ReverseMap();
            CreateMap<SharesApplicationViewModel, InwardTransactionTranslationMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, InwardTransactionDetail>().ReverseMap();
            CreateMap<SharesApplicationViewModel, InwardTransactionDetailMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, OutwardTransaction>().ReverseMap();
            CreateMap<SharesApplicationViewModel, OutwardTransactionMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, OutwardTransactionTranslation>().ReverseMap();
            CreateMap<SharesApplicationViewModel, OutwardTransactionTranslationMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, OutwardTransactionDetail>().ReverseMap();
            CreateMap<SharesApplicationViewModel, OutwardTransactionDetailMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, PersonFinancialAsset>().ReverseMap();
            CreateMap<SharesApplicationViewModel, PersonFinancialAssetMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, PersonFinancialAssetTranslation>().ReverseMap();
            CreateMap<SharesApplicationViewModel, PersonFinancialAssetTranslationMakerChecker>().ReverseMap();

            CreateMap<SharesApplicationViewModel, PersonFinancialAssetDocument>().ReverseMap();
            CreateMap<SharesApplicationViewModel, PersonFinancialAssetDocumentMakerChecker>().ReverseMap();


            // ************************** Transaction **************************
            CreateMap<TransactionViewModel, TransactionMaster>().ReverseMap();
            CreateMap<TransactionViewModel, TransactionMasterMakerChecker>().ReverseMap();

            CreateMap<TransactionCustomerAccountViewModel, TransactionCustomerAccount>().ReverseMap();
            CreateMap<TransactionCustomerAccountViewModel, TransactionCustomerAccountMakerChecker>().ReverseMap();

            CreateMap<TransactionGeneralLedgerViewModel, TransactionGeneralLedger>().ReverseMap();
            CreateMap<TransactionGeneralLedgerViewModel, TransactionGeneralLedgerMakerChecker>().ReverseMap();

            CreateMap<TransactionGSTDetailViewModel, TransactionGSTDetail>().ReverseMap();
            CreateMap<TransactionGSTDetailViewModel, TransactionGSTDetailMakerChecker>().ReverseMap();

            CreateMap<TransactionCashDenominationViewModel, TransactionCashDenomination>().ReverseMap();
            CreateMap<TransactionCashDenominationViewModel, TransactionCashDenominationMakerChecker>().ReverseMap();

            CreateMap<TransactionCustomerAccountOtherSubscriptionViewModel, TransactionCustomerAccountOtherSubscription>().ReverseMap();
            CreateMap<TransactionCustomerAccountOtherSubscriptionViewModel, TransactionCustomerAccountOtherSubscriptionMakerChecker>().ReverseMap();

            CreateMap<SharesCessationTransactionViewModel, SharesCessationTransaction>().ReverseMap();
            CreateMap<SharesCessationTransactionViewModel, SharesCessationTransactionMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalTransactionViewModel, SharesCapitalTransaction>().ReverseMap();
            CreateMap<SharesCapitalTransactionViewModel, SharesCapitalTransactionMakerChecker>().ReverseMap();



            CreateMap<OpeningBalanceViewModel, OpeningBalance>().ReverseMap();
            CreateMap<OpeningBalanceViewModel, OpeningBalanceMakerChecker>().ReverseMap();

            CreateMap<OpeningBalanceViewModel, OpeningBalanceShare>().ReverseMap();
            CreateMap<OpeningBalanceViewModel, OpeningBalanceInvestment>().ReverseMap();
            CreateMap<OpeningBalanceViewModel, OpeningBalanceDeposit>().ReverseMap();
            CreateMap<OpeningBalanceViewModel, OpeningBalanceLoan>().ReverseMap();

            // ************************** GL **************************
           
            CreateMap<GeneralLedgerViewModel, GeneralLedger>().ReverseMap();
            CreateMap<GeneralLedgerViewModel, GeneralLedgerMakerChecker>().ReverseMap();

            CreateMap<GeneralLedgerViewModel, GeneralLedgerModification>().ReverseMap();
            CreateMap<GeneralLedgerViewModel, GeneralLedgerModificationMakerChecker>().ReverseMap();

            CreateMap<GeneralLedgerViewModel, GeneralLedgerTranslation>().ReverseMap();
            CreateMap<GeneralLedgerViewModel, GeneralLedgerTranslationMakerChecker>().ReverseMap();

            CreateMap<SchemeGeneralLedgerViewModel, SchemeGeneralLedger>().ReverseMap();
            CreateMap<SchemeGeneralLedgerViewModel, SchemeGeneralLedgerMakerChecker>().ReverseMap();

            CreateMap<GeneralLedgerBusinessOfficeViewModel, GeneralLedgerBusinessOffice>().ReverseMap();
            CreateMap<GeneralLedgerBusinessOfficeViewModel, GeneralLedgerBusinessOfficeMakerChecker>().ReverseMap();

            CreateMap<GeneralLedgerCurrencyViewModel, GeneralLedgerCurrency>().ReverseMap();
            CreateMap<GeneralLedgerCurrencyViewModel, GeneralLedgerCurrencyMakerChecker>().ReverseMap();

            CreateMap<GeneralLedgerCustomerTypeViewModel, GeneralLedgerCustomerType>().ReverseMap();
            CreateMap<GeneralLedgerCustomerTypeViewModel, GeneralLedgerCustomerTypeMakerChecker>().ReverseMap();

            CreateMap<GeneralLedgerTransactionTypeViewModel, GeneralLedgerTransactionType>().ReverseMap();
            CreateMap<GeneralLedgerTransactionTypeViewModel, GeneralLedgerTransactionTypeMakerChecker>().ReverseMap();

            CreateMap<ChequeBookMasterViewModel, ChequeBookMaster>().ReverseMap();
            CreateMap<ChequeBookMasterViewModel, ChequeBookMasterMakerChecker>().ReverseMap();

            // ************************** LayOut **************************
            CreateMap<LoanSchemeViewModel, Scheme>().ReverseMap();
            CreateMap<LoanSchemeViewModel, SchemeMakerChecker>().ReverseMap();

            CreateMap<LoanSchemeViewModel, SchemeTranslation>().ReverseMap();
            CreateMap<LoanSchemeViewModel, SchemeTranslationMakerChecker>().ReverseMap();

            CreateMap<SchemeAccountParameterViewModel, SchemeAccountParameter>().ReverseMap();
            CreateMap<SchemeAccountParameterViewModel, SchemeAccountParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeApplicationParameterViewModel, SchemeApplicationParameter>().ReverseMap();
            CreateMap<SchemeApplicationParameterViewModel, SchemeApplicationParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanAccountParameterViewModel, SchemeLoanAccountParameter>().ReverseMap();
            CreateMap<SchemeLoanAccountParameterViewModel, SchemeLoanAccountParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanArrearParameterViewModel, SchemeLoanArrearParameter>().ReverseMap();
            CreateMap<SchemeLoanArrearParameterViewModel, SchemeLoanArrearParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanInterestParameterViewModel, SchemeLoanInterestParameter>().ReverseMap();
            CreateMap<SchemeLoanInterestParameterViewModel, SchemeLoanInterestParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanSanctionAuthorityViewModel, SchemeLoanSanctionAuthority>().ReverseMap();
            CreateMap<SchemeLoanSanctionAuthorityViewModel, SchemeLoanSanctionAuthorityMakerChecker>().ReverseMap();

            CreateMap<SchemeCashCreditLoanParameterViewModel, SchemeCashCreditLoanParameter>().ReverseMap();
            CreateMap<SchemeCashCreditLoanParameterViewModel, SchemeCashCreditLoanParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeHomeLoanViewModel, SchemeHomeLoan>().ReverseMap();
            CreateMap<SchemeHomeLoanViewModel, SchemeHomeLoanMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanAgainstPropertyViewModel, SchemeLoanAgainstProperty>().ReverseMap();
            CreateMap<SchemeLoanAgainstPropertyViewModel, SchemeLoanAgainstPropertyMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanAgainstDepositParameterViewModel, SchemeLoanAgainstDepositParameter>().ReverseMap();
            CreateMap<SchemeLoanAgainstDepositParameterViewModel, SchemeLoanAgainstDepositParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanAgainstDepositGeneralLedgerViewModel, SchemeLoanAgainstDepositGeneralLedger>().ReverseMap();
            CreateMap<SchemeLoanAgainstDepositGeneralLedgerViewModel, SchemeLoanAgainstDepositGeneralLedgerMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanRecoveryActionViewModel, SchemeLoanRecoveryAction>().ReverseMap();
            CreateMap<SchemeLoanRecoveryActionViewModel, SchemeLoanRecoveryActionMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanPaymentReminderParameterViewModel, SchemeLoanPaymentReminderParameter>().ReverseMap();
            CreateMap<SchemeLoanPaymentReminderParameterViewModel, SchemeLoanPaymentReminderParameterMakerChecker>().ReverseMap();

            CreateMap<SchemePreownedVehicleLoanParameterViewModel, SchemePreownedVehicleLoanParameter>().ReverseMap();
            CreateMap<SchemePreownedVehicleLoanParameterViewModel, SchemePreownedVehicleLoanParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeVehicleTypeLoanParameterViewModel, SchemeVehicleTypeLoanParameter>().ReverseMap();
            CreateMap<SchemeVehicleTypeLoanParameterViewModel, SchemeVehicleTypeLoanParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeConsumerDurableLoanItemViewModel, SchemeConsumerDurableLoanItem>().ReverseMap();
            CreateMap<SchemeConsumerDurableLoanItemViewModel, SchemeConsumerDurableLoanItemMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanFineInterestParameterViewModel, SchemeLoanFineInterestParameter>().ReverseMap();
            CreateMap<SchemeLoanFineInterestParameterViewModel, SchemeLoanFineInterestParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanInterestProvisionParameterViewModel, SchemeLoanInterestProvisionParameter>().ReverseMap();
            CreateMap<SchemeLoanInterestProvisionParameterViewModel, SchemeLoanInterestProvisionParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanRepaymentScheduleParameterViewModel, SchemeLoanRepaymentScheduleParameter>().ReverseMap();
            CreateMap<SchemeLoanRepaymentScheduleParameterViewModel, SchemeLoanRepaymentScheduleParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanSettlementAccountParameterViewModel, SchemeLoanSettlementAccountParameter>().ReverseMap();
            CreateMap<SchemeLoanSettlementAccountParameterViewModel, SchemeLoanSettlementAccountParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanInterestRebateParameterViewModel, SchemeLoanInterestRebateParameter>().ReverseMap();
            CreateMap<SchemeLoanInterestRebateParameterViewModel, SchemeLoanInterestRebateParameterMakerChecker>().ReverseMap();

            CreateMap<SchemePassbookViewModel, SchemePassbook>().ReverseMap();
            CreateMap<SchemePassbookViewModel, SchemePassbookMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanPreFullPaymentParameterViewModel, SchemeLoanPreFullPaymentParameter>().ReverseMap();
            CreateMap<SchemeLoanPreFullPaymentParameterViewModel, SchemeLoanPreFullPaymentParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanPrePartPaymentParameterViewModel, SchemeLoanPrePartPaymentParameter>().ReverseMap();
            CreateMap<SchemeLoanPrePartPaymentParameterViewModel, SchemeLoanPrePartPaymentParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeBusinessLoanViewModel, SchemeBusinessLoan>().ReverseMap();
            CreateMap<SchemeBusinessLoanViewModel, SchemeBusinessLoanMakerChecker>().ReverseMap();

            CreateMap<SchemeTenureViewModel, SchemeTenure>().ReverseMap();
            CreateMap<SchemeTenureViewModel, SchemeTenureMakerChecker>().ReverseMap();

            CreateMap<SchemeAccountBankingChannelParameterViewModel, SchemeAccountBankingChannelParameter>().ReverseMap();
            CreateMap<SchemeAccountBankingChannelParameterViewModel, SchemeAccountBankingChannelParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeBusinessOfficeViewModel, SchemeBusinessOffice>().ReverseMap();
            CreateMap<SchemeBusinessOfficeViewModel, SchemeBusinessOfficeMakerChecker>().ReverseMap();

            CreateMap<SchemeCustomerAccountNumberViewModel, SchemeCustomerAccountNumber>().ReverseMap();
            CreateMap<SchemeCustomerAccountNumberViewModel, SchemeCustomerAccountNumberMakerChecker>().ReverseMap();

            CreateMap<SchemeDocumentViewModel, SchemeDocument>().ReverseMap();
            CreateMap<SchemeDocumentViewModel, SchemeDocumentMakerChecker>().ReverseMap();

            CreateMap<SchemeEstimateTargetViewModel, SchemeEstimateTarget>().ReverseMap();
            CreateMap<SchemeEstimateTargetViewModel, SchemeEstimateTargetMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanChargesParameterViewModel, SchemeLoanChargesParameter>().ReverseMap();
            CreateMap<SchemeLoanChargesParameterViewModel, SchemeLoanChargesParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanDistributorParameterViewModel, SchemeLoanDistributorParameter>().ReverseMap();
            CreateMap<SchemeLoanDistributorParameterViewModel, SchemeLoanDistributorParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanFunderParameterViewModel, SchemeLoanFunderParameter>().ReverseMap();
            CreateMap<SchemeLoanFunderParameterViewModel, SchemeLoanFunderParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanOverduesActionViewModel, SchemeLoanOverduesAction>().ReverseMap();
            CreateMap<SchemeLoanOverduesActionViewModel, SchemeLoanOverduesActionMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanInstallmentParameterViewModel, SchemeLoanInstallmentParameter>().ReverseMap();
            CreateMap<SchemeLoanInstallmentParameterViewModel, SchemeLoanInstallmentParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeNoticeScheduleViewModel, SchemeNoticeSchedule>().ReverseMap();
            CreateMap<SchemeNoticeScheduleViewModel, SchemeNoticeScheduleMakerChecker>().ReverseMap();

            CreateMap<SchemeNumberOfTransactionLimitViewModel, SchemeNumberOfTransactionLimit>().ReverseMap();
            CreateMap<SchemeNumberOfTransactionLimitViewModel, SchemeNumberOfTransactionLimitMakerChecker>().ReverseMap();

            CreateMap<SchemeTenureListViewModel, SchemeTenureList>().ReverseMap();
            CreateMap<SchemeTenureListViewModel, SchemeTenureListMakerChecker>().ReverseMap();

            CreateMap<SchemeTenureListViewModel, SchemeTenureListTranslation>().ReverseMap();
            CreateMap<SchemeTenureListViewModel, SchemeTenureListTranslationMakerChecker>().ReverseMap();

            CreateMap<SchemeReportFormatViewModel, SchemeReportFormat>().ReverseMap();
            CreateMap<SchemeReportFormatViewModel, SchemeReportFormatMakerChecker>().ReverseMap();

            CreateMap<SchemeTargetGroupViewModel, SchemeTargetGroup>().ReverseMap();
            CreateMap<SchemeTargetGroupViewModel, SchemeTargetGroupMakerChecker>().ReverseMap();

            CreateMap<SchemeTargetGroupViewModel, SchemeTargetGroupGender>().ReverseMap();
            CreateMap<SchemeTargetGroupViewModel, SchemeTargetGroupGenderMakerChecker>().ReverseMap();

            CreateMap<SchemeTargetGroupViewModel, SchemeTargetGroupOccupation>().ReverseMap();
            CreateMap<SchemeTargetGroupViewModel, SchemeTargetGroupOccupationMakerChecker>().ReverseMap();

            CreateMap<SchemeTransactionAmountLimitViewModel, SchemeTransactionAmountLimit>().ReverseMap();
            CreateMap<SchemeTransactionAmountLimitViewModel, SchemeTransactionAmountLimitMakerChecker>().ReverseMap();

            CreateMap<SchemeChargesDetailViewModel, SchemeChargesDetail>().ReverseMap();
            CreateMap<SchemeChargesDetailViewModel, SchemeChargesDetailMakerChecker>().ReverseMap();

            CreateMap<SchemeLimitViewModel, SchemeLimit>().ReverseMap();
            CreateMap<SchemeLimitViewModel, SchemeLimitMakerChecker>().ReverseMap();

            CreateMap<SchemeSharesCapitalAccountParameterViewModel, SchemeSharesCapitalAccountParameter>().ReverseMap();
            CreateMap<SchemeSharesCapitalAccountParameterViewModel, SchemeSharesCapitalAccountParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeSharesCapitalDividendParameterViewModel, SchemeSharesCapitalDividendParameter>().ReverseMap();
            CreateMap<SchemeSharesCapitalDividendParameterViewModel, SchemeSharesCapitalDividendParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeSharesCertificateParameterViewModel, SchemeSharesCertificateParameter>().ReverseMap();
            CreateMap<SchemeSharesCertificateParameterViewModel, SchemeSharesCertificateParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeClosingChargesViewModel, SchemeClosingCharges>().ReverseMap();
            CreateMap<SchemeClosingChargesViewModel, SchemeClosingChargesMakerChecker>().ReverseMap();

            CreateMap<SchemeSharesTransferChargesViewModel, SchemeSharesTransferCharges>().ReverseMap();
            CreateMap<SchemeSharesTransferChargesViewModel, SchemeSharesTransferChargesMakerChecker>().ReverseMap();

            //SharesScheme
            CreateMap<SharesCapitalSchemeViewModel, Scheme>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeTranslation>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeTranslationMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeAccountParameter>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeAccountParameterMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeApplicationParameter>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeApplicationParameterMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeBusinessOffice>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeBusinessOfficeMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeChargesDetail>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeChargesDetailMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeCustomerAccountNumber>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeCustomerAccountNumberMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeEstimateTarget>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeEstimateTargetMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeLimit>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeLimitMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeNoticeSchedule>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeNoticeScheduleMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeReportFormat>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeReportFormatMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeSharesCapitalAccountParameter>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeSharesCapitalAccountParameterMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeSharesCapitalDividendParameter>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeSharesCapitalDividendParameterMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeViewModel, SchemeSharesCertificateParameter>().ReverseMap();
            CreateMap<SharesCapitalSchemeViewModel, SchemeSharesCertificateParameterMakerChecker>().ReverseMap();

            //################### DEPOSIT SCHEME ############################ 

            CreateMap<SchemePassbookViewModel, SchemePassbook>().ReverseMap();
            CreateMap<SchemePassbookViewModel, SchemePassbookMakerChecker>().ReverseMap();

            CreateMap<SchemeDepositAccountParameterViewModel, SchemeDepositAccountParameter>().ReverseMap();
            CreateMap<SchemeDepositAccountParameterViewModel, SchemeDepositAccountParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeDepositCertificateParameterViewModel, SchemeDepositCertificateParameter>().ReverseMap();
            CreateMap<SchemeDepositCertificateParameterViewModel, SchemeDepositCertificateParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeDepositAgentIncentiveViewModel, SchemeDepositAgentIncentive>().ReverseMap();
            CreateMap<SchemeDepositAgentIncentiveViewModel, SchemeDepositAgentIncentiveMakerChecker>().ReverseMap();

            CreateMap<SchemeDepositAgentParameterViewModel, SchemeDepositAgentParameter>().ReverseMap();
            CreateMap<SchemeDepositAgentParameterViewModel, SchemeDepositAgentParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeDepositInstallmentParameterViewModel, SchemeDepositInstallmentParameter>().ReverseMap();
            CreateMap<SchemeDepositInstallmentParameterViewModel, SchemeDepositInstallmentParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeDepositInterestParameterViewModel, SchemeDepositInterestParameter>().ReverseMap();
            CreateMap<SchemeDepositInterestParameterViewModel, SchemeDepositInterestParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeLoanAgreementNumberViewModel, SchemeLoanAgreementNumber>().ReverseMap();
            CreateMap<SchemeLoanAgreementNumberViewModel, SchemeLoanAgreementNumberMakerChecker>().ReverseMap();

            CreateMap<SchemeGoldLoanParameterViewModel, SchemeGoldLoanParameter>().ReverseMap();
            CreateMap<SchemeGoldLoanParameterViewModel, SchemeGoldLoanParameterMakerChecker>().ReverseMap();
            
            CreateMap<SchemeEducationLoanParameterViewModel, SchemeEducationLoanParameter>().ReverseMap();
            CreateMap<SchemeEducationLoanParameterViewModel, SchemeEducationLoanParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeInstituteViewModel, SchemeInstitute>().ReverseMap();
            CreateMap<SchemeInstituteViewModel, SchemeInstituteMakerChecker>().ReverseMap();

            CreateMap<SchemeEducationalCourseViewModel, SchemeEducationalCourse>().ReverseMap();
            CreateMap<SchemeEducationalCourseViewModel, SchemeEducationalCourseMakerChecker>().ReverseMap();

            CreateMap<SchemeInterestRateViewModel, SchemeInterestRate>().ReverseMap();
            CreateMap<SchemeInterestRateViewModel, SchemeInterestRateMakerChecker>().ReverseMap();

            CreateMap<SchemeDepositInterestProvisionParameterViewModel, SchemeDepositInterestProvisionParameter>().ReverseMap();
            CreateMap<SchemeDepositInterestProvisionParameterViewModel, SchemeDepositInterestProvisionParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeInterestPayoutFrequencyViewModel, SchemeInterestPayoutFrequency>().ReverseMap();
            CreateMap<SchemeInterestPayoutFrequencyViewModel, SchemeInterestPayoutFrequencyMakerChecker>().ReverseMap();

            CreateMap<SchemeNumberOfTransactionLimitViewModel, SchemeNumberOfTransactionLimit>().ReverseMap();
            CreateMap<SchemeNumberOfTransactionLimitViewModel, SchemeNumberOfTransactionLimitMakerChecker>().ReverseMap();

            CreateMap<SchemePaymentCardFeatureViewModel, SchemePaymentCardFeature>().ReverseMap();
            CreateMap<SchemePaymentCardFeatureViewModel, SchemePaymentCardFeatureMakerChecker>().ReverseMap();

            CreateMap<SchemeTransactionAmountLimitViewModel, SchemeTransactionAmountLimit>().ReverseMap();
            CreateMap<SchemeTransactionAmountLimitViewModel, SchemeTransactionAmountLimitMakerChecker>().ReverseMap();

            CreateMap<SchemeDemandDepositDetailViewModel, SchemeDemandDepositDetail>().ReverseMap();
            CreateMap<SchemeDemandDepositDetailViewModel, SchemeDemandDepositDetailMakerChecker>().ReverseMap();

            CreateMap<SchemeDepositClosingModeViewModel, SchemeDepositClosingMode>().ReverseMap();
            CreateMap<SchemeDepositClosingModeViewModel, SchemeDepositClosingModeMakerChecker>().ReverseMap();

            CreateMap<SchemeTermDepositDetailViewModel, SchemeTermDepositDetail>().ReverseMap();
            CreateMap<SchemeTermDepositDetailViewModel, SchemeTermDepositDetailMakerChecker>().ReverseMap();

            CreateMap<SchemeDepositAccountRenewalParameterViewModel, SchemeDepositAccountRenewalParameter>().ReverseMap();
            CreateMap<SchemeDepositAccountRenewalParameterViewModel, SchemeDepositAccountRenewalParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeDepositPledgeLoanParameterViewModel, SchemeDepositPledgeLoanParameter>().ReverseMap();
            CreateMap<SchemeDepositPledgeLoanParameterViewModel, SchemeDepositPledgeLoanParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeFixedDepositParameterViewModel, SchemeFixedDepositParameter>().ReverseMap();
            CreateMap<SchemeFixedDepositParameterViewModel, SchemeFixedDepositParameterMakerChecker>().ReverseMap();

            CreateMap<SchemeDepositAccountClosureParameterViewModel, SchemeDepositAccountClosureParameter>().ReverseMap();
            CreateMap<SchemeDepositAccountClosureParameterViewModel, SchemeDepositAccountClosureParameterMakerChecker>().ReverseMap();

            // //################### DepositScheme ############################

            CreateMap<DepositSchemeViewModel, Scheme>().ReverseMap();
            CreateMap<DepositSchemeViewModel, SchemeMakerChecker>().ReverseMap();

            CreateMap<DepositSchemeViewModel, SchemeTranslation>().ReverseMap();
            CreateMap<DepositSchemeViewModel, SchemeTranslationMakerChecker>().ReverseMap();

            CreateMap<DepositSchemeViewModel, SchemeAccountParameter>().ReverseMap();
            CreateMap<DepositSchemeViewModel, SchemeAccountParameterMakerChecker>().ReverseMap();

            CreateMap<DepositSchemeViewModel, SchemeCustomerAccountNumber>().ReverseMap();
            CreateMap<DepositSchemeViewModel, SchemeCustomerAccountNumberMakerChecker>().ReverseMap();

            CreateMap<DepositSchemeViewModel, SchemeApplicationParameter>().ReverseMap();
            CreateMap<DepositSchemeViewModel, SchemeApplicationParameterMakerChecker>().ReverseMap();

            CreateMap<DepositSchemeViewModel, SchemeDepositAccountParameter>().ReverseMap();
            CreateMap<DepositSchemeViewModel, SchemeDepositAccountParameterMakerChecker>().ReverseMap();

            CreateMap<DepositSchemeViewModel, SchemeDepositCertificateParameter>().ReverseMap();
            CreateMap<DepositSchemeViewModel, SchemeDepositCertificateParameterMakerChecker>().ReverseMap();

            CreateMap<DepositSchemeViewModel, SchemeDepositInstallmentParameter>().ReverseMap();
            CreateMap<DepositSchemeViewModel, SchemeDepositInstallmentParameterMakerChecker>().ReverseMap();

            CreateMap<DepositSchemeViewModel, SchemeDepositAgentParameter>().ReverseMap();
            CreateMap<DepositSchemeViewModel, SchemeDepositAgentParameterMakerChecker>().ReverseMap();


            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ E N T E R P R I S E @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            // ************************** Capital **************************
            CreateMap<AuthorizedSharesCapitalViewModel, AuthorizedSharesCapital>().ReverseMap();
            CreateMap<AuthorizedSharesCapitalViewModel, AuthorizedSharesCapitalMakerChecker>().ReverseMap();

            // ************************** Establishment **************************
            // ################### Organization ###################
            CreateMap<OrganizationViewModel, Organization>().ReverseMap();
            CreateMap<OrganizationViewModel, OrganizationMakerChecker>().ReverseMap();

            CreateMap<OrganizationViewModel, OrganizationTranslation>().ReverseMap();
            CreateMap<OrganizationViewModel, OrganizationTranslationMakerChecker>().ReverseMap();

            // OrganizationContactDetail
            CreateMap<OrganizationContactDetailViewModel, OrganizationContactDetail>().ReverseMap();
            CreateMap<OrganizationContactDetailViewModel, OrganizationContactDetailMakerChecker>().ReverseMap();

            // OrganizationFund
            CreateMap<OrganizationFundViewModel, OrganizationFund>().ReverseMap();
            CreateMap<OrganizationFundViewModel, OrganizationFundMakerChecker>().ReverseMap();

            CreateMap<OrganizationFundViewModel, OrganizationFundTranslation>().ReverseMap();
            CreateMap<OrganizationFundViewModel, OrganizationFundTranslationMakerChecker>().ReverseMap();

            // OrganizationLoanType
            CreateMap<OrganizationLoanTypeViewModel, OrganizationLoanType>().ReverseMap();
            CreateMap<OrganizationLoanTypeViewModel, OrganizationLoanTypeMakerChecker>().ReverseMap();

            CreateMap<OrganizationLoanTypeViewModel, OrganizationLoanTypeTranslation>().ReverseMap();
            CreateMap<OrganizationLoanTypeViewModel, OrganizationLoanTypeTranslationMakerChecker>().ReverseMap();

            // AuthorizedSharesCapital
            CreateMap<OrganizationViewModel, AuthorizedSharesCapital>().ReverseMap();
            CreateMap<OrganizationViewModel, AuthorizedSharesCapitalMakerChecker>().ReverseMap();

            // OrganizationGSTRegistrationDetail
            CreateMap<OrganizationViewModel, OrganizationGSTRegistrationDetail>().ReverseMap();
            CreateMap<OrganizationViewModel, OrganizationGSTRegistrationDetailMakerChecker>().ReverseMap();

            CreateMap<OrganizationGSTRegistrationDetailViewModel, OrganizationGSTRegistrationDetail>().ReverseMap();
            CreateMap<OrganizationGSTRegistrationDetailViewModel, OrganizationGSTRegistrationDetailMakerChecker>().ReverseMap();

            // *************************************Office******************************            
            CreateMap<BusinessOfficeViewModel, BusinessOffice>().ReverseMap();
            CreateMap<BusinessOfficeViewModel, BusinessOfficeMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeViewModel, BusinessOfficeTranslation>().ReverseMap();
            CreateMap<BusinessOfficeViewModel, BusinessOfficeTranslationMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeDetailViewModel, BusinessOfficeDetail>().ReverseMap();
            CreateMap<BusinessOfficeDetailViewModel, BusinessOfficeDetailMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeViewModel, BusinessOfficeModification>().ReverseMap();
            CreateMap<BusinessOfficeViewModel, BusinessOfficeModificationMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeViewModel, BusinessOfficeCoopRegistration>().ReverseMap();
            CreateMap<BusinessOfficeViewModel, BusinessOfficeCoopRegistrationMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeViewModel, BusinessOfficeCoopRegistrationTranslation>().ReverseMap();
            CreateMap<BusinessOfficeViewModel, BusinessOfficeCoopRegistrationTranslationMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeViewModel, BusinessOfficeRBIRegistration>().ReverseMap();
            CreateMap<BusinessOfficeViewModel, BusinessOfficeRBIRegistrationMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeViewModel, BusinessOfficeRBIRegistrationTranslation>().ReverseMap();
            CreateMap<BusinessOfficeViewModel, BusinessOfficeRBIRegistrationTranslationMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficePersonInformationNumberViewModel, BusinessOfficePersonInformationNumber>().ReverseMap();
            CreateMap<BusinessOfficePersonInformationNumberViewModel, BusinessOfficePersonInformationNumberMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficePassbookNumberViewModel, BusinessOfficePassbookNumber>().ReverseMap();
            CreateMap<BusinessOfficePassbookNumberViewModel, BusinessOfficePassbookNumberMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeSharesCertificateNumberViewModel, BusinessOfficeSharesCertificateNumber>().ReverseMap();
            CreateMap<BusinessOfficeSharesCertificateNumberViewModel, BusinessOfficeSharesCertificateNumberMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeDepositCertificateNumberViewModel, BusinessOfficeDepositCertificateNumber>().ReverseMap();
            CreateMap<BusinessOfficeDepositCertificateNumberViewModel, BusinessOfficeDepositCertificateNumberMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficePersonInformationNumberViewModel, BusinessOfficePersonInformationNumber>().ReverseMap();
            CreateMap<BusinessOfficePersonInformationNumberViewModel, BusinessOfficePersonInformationNumberMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeCustomerNumberViewModel, BusinessOfficeCustomerNumber>().ReverseMap();
            CreateMap<BusinessOfficeCustomerNumberViewModel, BusinessOfficeCustomerNumberMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeMemberNumberViewModel, BusinessOfficeMemberNumber>().ReverseMap();
            CreateMap<BusinessOfficeMemberNumberViewModel, BusinessOfficeMemberNumberMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeTransactionParameterViewModel, BusinessOfficeTransactionParameter>().ReverseMap();
            CreateMap<BusinessOfficeTransactionParameterViewModel, BusinessOfficeTransactionParameterMakerChecker>().ReverseMap();

            // ************************** BusinessOfficeCoopRegistration **************************
            CreateMap<BusinessOfficeCoopRegistrationViewModel, BusinessOfficeCoopRegistration>().ReverseMap();
            CreateMap<BusinessOfficeCoopRegistrationViewModel, BusinessOfficeCoopRegistrationMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeCoopRegistrationViewModel, BusinessOfficeCoopRegistrationTranslation>().ReverseMap();
            CreateMap<BusinessOfficeCoopRegistrationViewModel, BusinessOfficeCoopRegistrationTranslationMakerChecker>().ReverseMap();

            // ************************** BusinessOfficeAccountNumber **************************
            CreateMap<BusinessOfficeAccountNumberViewModel, BusinessOfficeAccountNumber>().ReverseMap();
            CreateMap<BusinessOfficeAccountNumberViewModel, BusinessOfficeAccountNumberMakerChecker>().ReverseMap();

            // ************************** BusinessOfficeAgreementNumber **************************
            CreateMap<BusinessOfficeAgreementNumberViewModel, BusinessOfficeAgreementNumber>().ReverseMap();
            CreateMap<BusinessOfficeAgreementNumberViewModel, BusinessOfficeAgreementNumberMakerChecker>().ReverseMap();

            // ************************** BusinessOfficeApplicationNumber **************************
            CreateMap<BusinessOfficeApplicationNumberViewModel, BusinessOfficeApplicationNumber>().ReverseMap();
            CreateMap<BusinessOfficeApplicationNumberViewModel, BusinessOfficeApplicationNumberMakerChecker>().ReverseMap();

            // ************************** BusinessOfficeSpecialPermission **************************
            CreateMap<BusinessOfficeCurrencyViewModel, BusinessOfficeCurrency>().ReverseMap();
            CreateMap<BusinessOfficeCurrencyViewModel, BusinessOfficeCurrencyMakerChecker>().ReverseMap();

            // ************************** BusinessOfficeMenu **************************
            CreateMap<BusinessOfficeMenuViewModel, BusinessOfficeMenu>().ReverseMap();
            CreateMap<BusinessOfficeMenuViewModel, BusinessOfficeMenuMakerChecker>().ReverseMap();

            // ************************** BusinessOfficeSpecialPermission **************************
            CreateMap<BusinessOfficeSpecialPermissionViewModel, BusinessOfficeSpecialPermission>().ReverseMap();
            CreateMap<BusinessOfficeSpecialPermissionViewModel, BusinessOfficeSpecialPermissionMakerChecker>().ReverseMap();

            // ************************** BusinessOfficeTransactionLimit **************************
            CreateMap<BusinessOfficeTransactionLimitViewModel, BusinessOfficeTransactionLimit>().ReverseMap();
            CreateMap<BusinessOfficeTransactionLimitViewModel, BusinessOfficeTransactionLimitMakerChecker>().ReverseMap();

            // ************************** BusinessOfficePasswordPolicy **************************
            CreateMap<BusinessOfficePasswordPolicyViewModel, BusinessOfficePasswordPolicy>().ReverseMap();
            CreateMap<BusinessOfficePasswordPolicyViewModel, BusinessOfficePasswordPolicyMakerChecker>().ReverseMap();

            // ************************** BusinessOfficeRBIRegistration **************************
            CreateMap<BusinessOfficeRBIRegistrationViewModel, BusinessOfficeRBIRegistration>().ReverseMap();
            CreateMap<BusinessOfficeRBIRegistrationViewModel, BusinessOfficeRBIRegistrationMakerChecker>().ReverseMap();

            CreateMap<BusinessOfficeRBIRegistrationViewModel, BusinessOfficeRBIRegistrationTranslation>().ReverseMap();
            CreateMap<BusinessOfficeRBIRegistrationViewModel, BusinessOfficeRBIRegistrationTranslationMakerChecker>().ReverseMap();

            // ************************** Schedule **************************
            CreateMap<OfficeScheduleViewModel, OfficeSchedule>().ReverseMap();
            CreateMap<OfficeScheduleViewModel, OfficeScheduleMakerChecker>().ReverseMap();

            CreateMap<OfficeScheduleViewModel, OfficeScheduleTranslation>().ReverseMap();
            CreateMap<OfficeScheduleViewModel, OfficeScheduleTranslationMakerChecker>().ReverseMap();

            CreateMap<OfficeScheduleViewModel, OfficeScheduleModification>().ReverseMap();
            CreateMap<OfficeScheduleViewModel, OfficeScheduleModificationMakerChecker>().ReverseMap();

            // ************************** WorkingSchedule **************************
            CreateMap<WorkingScheduleViewModel, WorkingSchedule>().ReverseMap();
            CreateMap<WorkingScheduleViewModel, WorkingScheduleMakerChecker>().ReverseMap();

            CreateMap<WorkingScheduleViewModel, WorkingScheduleTranslation>().ReverseMap();
            CreateMap<WorkingScheduleViewModel, WorkingScheduleTranslationMakerChecker>().ReverseMap();

            CreateMap<WorkingScheduleViewModel, WorkingScheduleModification>().ReverseMap();
            CreateMap<WorkingScheduleViewModel, WorkingScheduleModificationMakerChecker>().ReverseMap();

            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ H u m a n R e s o u r c e @@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            // ################### Servant ####################################################

            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<EmployeeViewModel, EmployeeMakerChecker>().ReverseMap();

            CreateMap<EmployeeViewModel, EmployeeModification>().ReverseMap();
            CreateMap<EmployeeViewModel, EmployeeModificationMakerChecker>().ReverseMap();

            CreateMap<EmployeeDepartmentViewModel, EmployeeDepartment>().ReverseMap();
            CreateMap<EmployeeDepartmentViewModel, EmployeeDepartmentMakerChecker>().ReverseMap();

            CreateMap<EmployeeDesignationViewModel, EmployeeDesignation>().ReverseMap();
            CreateMap<EmployeeDesignationViewModel, EmployeeDesignationMakerChecker>().ReverseMap();

            CreateMap<EmployeeDetailViewModel, EmployeeDetail>().ReverseMap();
            CreateMap<EmployeeDetailViewModel, EmployeeDetailMakerChecker>().ReverseMap();

            CreateMap<EmployeeDocumentViewModel, EmployeeDocument>().ReverseMap();
            CreateMap<EmployeeDocumentViewModel, EmployeeDocumentMakerChecker>().ReverseMap();

            CreateMap<EmployeePerformanceRatingViewModel, EmployeePerformanceRating>().ReverseMap();
            CreateMap<EmployeePerformanceRatingViewModel, EmployeePerformanceRatingMakerChecker>().ReverseMap();

            CreateMap<EmployeePhotoViewModel, EmployeePhoto>().ReverseMap();
            CreateMap<EmployeePhotoViewModel, EmployeePhotoMakerChecker>().ReverseMap();

            CreateMap<EmployeeSalaryStructureViewModel, EmployeeSalaryStructure>().ReverseMap();
            CreateMap<EmployeeSalaryStructureViewModel, EmployeeSalaryStructureMakerChecker>().ReverseMap();

            CreateMap<EmployeeWorkingScheduleViewModel, EmployeeWorkingSchedule>().ReverseMap();
            CreateMap<EmployeeWorkingScheduleViewModel, EmployeeWorkingScheduleMakerChecker>().ReverseMap();

            // ################### ContentItem ###################
            CreateMap<ContentItemViewModel, ContentItem>().ReverseMap();
            CreateMap<ContentItemViewModel, ContentItemMakerChecker>().ReverseMap();

            CreateMap<ContentItemViewModel, ContentItemTranslation>().ReverseMap();
            CreateMap<ContentItemViewModel, ContentItemTranslationMakerChecker>().ReverseMap();

            CreateMap<ContentItemViewModel, ContentItemModification>().ReverseMap();
            CreateMap<ContentItemViewModel, ContentItemModificationMakerChecker>().ReverseMap();

            // ################### EvaluationSection ###################
            CreateMap<EvaluationSectionViewModel, EvaluationSection>().ReverseMap();
            CreateMap<EvaluationSectionViewModel, EvaluationSectionMakerChecker>().ReverseMap();

            CreateMap<EvaluationSectionViewModel, EvaluationSectionTranslation>().ReverseMap();
            CreateMap<EvaluationSectionViewModel, EvaluationSectionTranslationMakerChecker>().ReverseMap();

            CreateMap<EvaluationSectionViewModel, EvaluationSectionModification>().ReverseMap();
            CreateMap<EvaluationSectionViewModel, EvaluationSectionModificationMakerChecker>().ReverseMap();

            // ################### EvaluationSectorContentItem ###################

            CreateMap<EvaluationSectorContentItemViewModel, EvaluationSectorContentItem>().ReverseMap();
            CreateMap<EvaluationSectorContentItemViewModel, EvaluationSectorContentItemMakerChecker>().ReverseMap();

            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Management @@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //****************************** Conference *****************************************
            // ################### Meeting ###################

            CreateMap<MeetingViewModel, Meeting>().ReverseMap();
            CreateMap<MeetingViewModel, MeetingMakerChecker>().ReverseMap();

            CreateMap<MeetingViewModel, MeetingTranslation>().ReverseMap();
            CreateMap<MeetingViewModel, MeetingTranslationMakerChecker>().ReverseMap();

            CreateMap<MeetingViewModel, MeetingModification>().ReverseMap();
            CreateMap<MeetingViewModel, MeetingModificationMakerChecker>().ReverseMap();

            // ################### MeetingAgenda ###################

            CreateMap<MeetingAgendaViewModel, MeetingAgenda>().ReverseMap();
            CreateMap<MeetingAgendaViewModel, MeetingAgendaMakerChecker>().ReverseMap();

            // ################### MeetingAllowance ###################

            CreateMap<MeetingAllowanceViewModel, MeetingAllowance>().ReverseMap();
            CreateMap<MeetingAllowanceViewModel, MeetingAllowanceMakerChecker>().ReverseMap();

            CreateMap<MeetingAllowanceViewModel, MeetingAllowanceTranslation>().ReverseMap();
            CreateMap<MeetingAllowanceViewModel, MeetingAllowanceTranslationMakerChecker>().ReverseMap();

            CreateMap<MeetingAllowanceViewModel, MeetingAllowanceModification>().ReverseMap();
            CreateMap<MeetingAllowanceViewModel, MeetingAllowanceModificationMakerChecker>().ReverseMap();

            // ################### MeetingInviteeBoardOfDirector ###################

            CreateMap<MeetingInviteeBoardOfDirectorViewModel, MeetingInviteeBoardOfDirector>().ReverseMap();
            CreateMap<MeetingInviteeBoardOfDirectorViewModel, MeetingInviteeBoardOfDirectorMakerChecker>().ReverseMap();

            // ################### MeetingInviteeMember ###################

            CreateMap<MeetingInviteeMemberViewModel, MeetingInviteeMember>().ReverseMap();
            CreateMap<MeetingInviteeMemberViewModel, MeetingInviteeMemberMakerChecker>().ReverseMap();

            // ################### MeetingNotice ###################

            CreateMap<MeetingNoticeViewModel, MeetingNotice>().ReverseMap();
            CreateMap<MeetingNoticeViewModel, MeetingNoticeMakerChecker>().ReverseMap();

            // ################### MinuteOfMeetingAgenda ###################

            CreateMap<MinuteOfMeetingAgendaViewModel, MinuteOfMeetingAgenda>().ReverseMap();
            CreateMap<MinuteOfMeetingAgendaViewModel, MinuteOfMeetingAgendaMakerChecker>().ReverseMap();

            // ################### MinuteOfMeetingAgendaSpokesperson ###################

            CreateMap<MinuteOfMeetingAgendaSpokespersonViewModel, MinuteOfMeetingAgendaSpokesperson>().ReverseMap();
            CreateMap<MinuteOfMeetingAgendaSpokespersonViewModel, MinuteOfMeetingAgendaSpokespersonMakerChecker>().ReverseMap();

            CreateMap<MinuteOfMeetingAgendaSpokespersonViewModel, MinuteOfMeetingAgendaSpokespersonTranslation>().ReverseMap();
            CreateMap<MinuteOfMeetingAgendaSpokespersonViewModel, MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker>().ReverseMap();

            // ########################### Notification #############################
            //****************************** Event **********************************

            CreateMap<EventMasterViewModel, EventMaster>().ReverseMap();
            CreateMap<EventMasterViewModel, EventMasterMakerChecker>().ReverseMap();

            CreateMap<EventMasterViewModel, EventMasterTranslation>().ReverseMap();
            CreateMap<EventMasterViewModel, EventMasterTranslationMakerChecker>().ReverseMap();

            CreateMap<EventMasterViewModel, EventMasterModification>().ReverseMap();
            CreateMap<EventMasterViewModel, EventMasterModificationMakerChecker>().ReverseMap();

            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ M A S T E R @@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //######################## Responsibility ##################################

            CreateMap<ResponsibilityViewModel, Responsibility>().ReverseMap();
            CreateMap<ResponsibilityViewModel, ResponsibilityMakerChecker>().ReverseMap();

            CreateMap<ResponsibilityViewModel, ResponsibilityTranslation>().ReverseMap();
            CreateMap<ResponsibilityViewModel, ResponsibilityTranslationMakerChecker>().ReverseMap();

            CreateMap<ResponsibilityViewModel, ResponsibilityModification>().ReverseMap();
            CreateMap<ResponsibilityViewModel, ResponsibilityModificationMakerChecker>().ReverseMap();

            // ************************** Account Class **************************

            // ################### Locality ###################

            CreateMap<CenterViewModel, Center>().ReverseMap();
            CreateMap<CenterViewModel, CenterMakerChecker>().ReverseMap();

            CreateMap<CenterViewModel, CenterTranslation>().ReverseMap();
            CreateMap<CenterViewModel, CenterTranslationMakerChecker>().ReverseMap();

            CreateMap<CenterViewModel, CenterModification>().ReverseMap();
            CreateMap<CenterViewModel, CenterModificationMakerChecker>().ReverseMap();

            CreateMap<CenterViewModel, CenterDemographicDetail>().ReverseMap();
            CreateMap<CenterViewModel, CenterDemographicDetailMakerChecker>().ReverseMap();

            CreateMap<CenterViewModel, CenterISOCode>().ReverseMap();
            CreateMap<CenterViewModel, CenterISOCodeMakerChecker>().ReverseMap();

            CreateMap<CenterViewModel, CenterOccupation>().ReverseMap();
            CreateMap<CenterViewModel, CenterOccupationMakerChecker>().ReverseMap();

            CreateMap<CenterViewModel, CenterTradingEntityDetail>().ReverseMap();
            CreateMap<CenterViewModel, CenterTradingEntityDetailMakerChecker>().ReverseMap();

            CreateMap<CenterViewModel, CountryAdditionalDetail>().ReverseMap();
            CreateMap<CenterViewModel, CountryAdditionalDetailMakerChecker>().ReverseMap();

            // ######################### CenterTradingEntityDetail #####################

            CreateMap<CenterTradingEntityDetailViewModel, CenterTradingEntityDetail>().ReverseMap();
            CreateMap<CenterTradingEntityDetailViewModel, CenterTradingEntityDetailMakerChecker>().ReverseMap();

            // ###################### CenterOccupation #######################

            CreateMap<CenterOccupationViewModel, CenterOccupation>().ReverseMap();
            CreateMap<CenterOccupationViewModel, CenterOccupationMakerChecker>().ReverseMap();

            // ####################### CenterDemographicDetail ########################

            CreateMap<CenterDemographicDetailViewModel, CenterDemographicDetail>().ReverseMap();
            CreateMap<CenterDemographicDetailViewModel, CenterDemographicDetailMakerChecker>().ReverseMap();

            // ######################## CountryAdditionalDetail ###########################

            CreateMap<CountryAdditionalDetailViewModel, CountryAdditionalDetail>().ReverseMap();
            CreateMap<CountryAdditionalDetailViewModel, CountryAdditionalDetailMakerChecker>().ReverseMap();

            CreateMap<CenterIsoCodeViewModel, CenterISOCode>().ReverseMap();
            CreateMap<CenterIsoCodeViewModel, CenterISOCodeMakerChecker>().ReverseMap();

            // ######################### DISTRICT ############################

            CreateMap<DistrictViewModel, Center>().ReverseMap();
            CreateMap<DistrictViewModel, CenterMakerChecker>().ReverseMap();

            CreateMap<DistrictViewModel, CenterTranslation>().ReverseMap();
            CreateMap<DistrictViewModel, CenterTranslationMakerChecker>().ReverseMap();

            CreateMap<DistrictViewModel, CenterModification>().ReverseMap();
            CreateMap<DistrictViewModel, CenterModificationMakerChecker>().ReverseMap();

            CreateMap<DistrictViewModel, CenterISOCode>().ReverseMap();
            CreateMap<DistrictViewModel, CenterISOCodeMakerChecker>().ReverseMap();

            // ################ Dividision########################

            CreateMap<DivisionViewModel, Center>().ReverseMap();
            CreateMap<DivisionViewModel, CenterMakerChecker>().ReverseMap();

            CreateMap<DivisionViewModel, CenterTranslation>().ReverseMap();
            CreateMap<DivisionViewModel, CenterTranslationMakerChecker>().ReverseMap();

            CreateMap<DivisionViewModel, CenterModification>().ReverseMap();
            CreateMap<DivisionViewModel, CenterModificationMakerChecker>().ReverseMap();

            CreateMap<DivisionViewModel, CenterISOCode>().ReverseMap();
            CreateMap<DivisionViewModel, CenterISOCodeMakerChecker>().ReverseMap();

            //################### Taluka ############################

            CreateMap<TalukaViewModel, Center>().ReverseMap();
            CreateMap<TalukaViewModel, CenterMakerChecker>().ReverseMap();

            CreateMap<TalukaViewModel, CenterTranslation>().ReverseMap();
            CreateMap<TalukaViewModel, CenterTranslationMakerChecker>().ReverseMap();

            CreateMap<TalukaViewModel, CenterModification>().ReverseMap();
            CreateMap<TalukaViewModel, CenterModificationMakerChecker>().ReverseMap();

            CreateMap<TalukaViewModel, CenterISOCode>().ReverseMap();
            CreateMap<TalukaViewModel, CenterISOCodeMakerChecker>().ReverseMap();

            // ######################## Continent ##########################

            CreateMap<ContinentViewModel, Center>().ReverseMap();
            CreateMap<ContinentViewModel, CenterMakerChecker>().ReverseMap();

            CreateMap<ContinentViewModel, CenterTranslation>().ReverseMap();
            CreateMap<ContinentViewModel, CenterTranslationMakerChecker>().ReverseMap();

            CreateMap<ContinentViewModel, CenterModification>().ReverseMap();
            CreateMap<ContinentViewModel, CenterModificationMakerChecker>().ReverseMap();

            // ################### Country ###################

            CreateMap<CountryViewModel, Center>().ReverseMap();
            CreateMap<CountryViewModel, CenterMakerChecker>().ReverseMap();

            CreateMap<CountryViewModel, CenterTranslation>().ReverseMap();
            CreateMap<CountryViewModel, CenterTranslationMakerChecker>().ReverseMap();

            CreateMap<CountryViewModel, CenterModification>().ReverseMap();
            CreateMap<CountryViewModel, CenterModificationMakerChecker>().ReverseMap();

            CreateMap<CountryViewModel, CenterISOCode>().ReverseMap();
            CreateMap<CountryViewModel, CenterISOCodeMakerChecker>().ReverseMap();

            CreateMap<CountryViewModel, CountryAdditionalDetail>().ReverseMap();
            CreateMap<CountryViewModel, CountryAdditionalDetailMakerChecker>().ReverseMap();

            // ################### State ###################

            CreateMap<StateViewModel, Center>().ReverseMap();
            CreateMap<StateViewModel, CenterMakerChecker>().ReverseMap();

            CreateMap<StateViewModel, CenterTranslation>().ReverseMap();
            CreateMap<StateViewModel, CenterTranslationMakerChecker>().ReverseMap();

            CreateMap<StateViewModel, CenterModification>().ReverseMap();
            CreateMap<StateViewModel, CenterModificationMakerChecker>().ReverseMap();

            CreateMap<StateViewModel, CenterISOCode>().ReverseMap();
            CreateMap<StateViewModel, CenterISOCodeMakerChecker>().ReverseMap();

            // Village

            CreateMap<VillageTownCityViewModel, Center>().ReverseMap();
            CreateMap<VillageTownCityViewModel, CenterMakerChecker>().ReverseMap();

            CreateMap<VillageTownCityViewModel, CenterTranslation>().ReverseMap();
            CreateMap<VillageTownCityViewModel, CenterTranslationMakerChecker>().ReverseMap();

            CreateMap<VillageTownCityViewModel, CenterModification>().ReverseMap();
            CreateMap<VillageTownCityViewModel, CenterModificationMakerChecker>().ReverseMap();

            CreateMap<VillageTownCityViewModel, CenterISOCode>().ReverseMap();
            CreateMap<VillageTownCityViewModel, CenterISOCodeMakerChecker>().ReverseMap();

            CreateMap<VillageTownCityViewModel, CenterDemographicDetail>().ReverseMap();
            CreateMap<VillageTownCityViewModel, CenterDemographicDetailMakerChecker>().ReverseMap();

            CreateMap<VillageTownCityViewModel, CenterOccupation>().ReverseMap();
            CreateMap<VillageTownCityViewModel, CenterOccupationMakerChecker>().ReverseMap();

            CreateMap<VillageTownCityViewModel, CenterTradingEntityDetail>().ReverseMap();
            CreateMap<VillageTownCityViewModel, CenterTradingEntityDetailMakerChecker>().ReverseMap();

            //CreateMap<VillageTownCityViewModel, CenterDemographicDetailViewModel>();
            //CreateMap<CenterDemographicDetailViewModel, VillageTownCityViewModel>();

            //CreateMap<VillageTownCityViewModel, CenterTradingEntityDetailViewModel>();
            //CreateMap<CenterTradingEntityDetailViewModel, VillageTownCityViewModel>();

            //CreateMap<VillageTownCityViewModel, CenterOccupationViewModel>();
            //CreateMap<CenterOccupationViewModel, VillageTownCityViewModel>();

            // ############### Power And Function #########################

            CreateMap<PowerAndFunctionViewModel, PowerAndFunction>().ReverseMap();
            CreateMap<PowerAndFunctionViewModel, PowerAndFunctionMakerChecker>().ReverseMap();

            CreateMap<PowerAndFunctionViewModel, PowerAndFunctionTranslation>().ReverseMap();
            CreateMap<PowerAndFunctionViewModel, PowerAndFunctionTranslationMakerChecker>().ReverseMap();

            // ############### Board Of Director #########################

            CreateMap<BoardOfDirectorViewModel, BoardOfDirector>().ReverseMap();
            CreateMap<BoardOfDirectorViewModel, BoardOfDirectorMakerChecker>().ReverseMap();

            // ############### Board Of Director Power And Function #########################

            CreateMap<BoardOfDirectorPowerAndFunctionViewModel, BoardOfDirectorPowerAndFunction>().ReverseMap();
            CreateMap<BoardOfDirectorPowerAndFunctionViewModel, BoardOfDirectorPowerAndFunctionMakerChecker>().ReverseMap();

            CreateMap<BoardOfDirectorPowerAndFunctionViewModel, BoardOfDirectorPowerAndFunctionTranslation>().ReverseMap();
            CreateMap<BoardOfDirectorPowerAndFunctionViewModel, BoardOfDirectorPowerAndFunctionTranslationMakerChecker>().ReverseMap();

            // ################### Agenda  ###################

            CreateMap<AgendaViewModel, Agenda>().ReverseMap();
            CreateMap<AgendaViewModel, AgendaMakerChecker>().ReverseMap();

            CreateMap<AgendaViewModel, AgendaTranslation>().ReverseMap();
            CreateMap<AgendaViewModel, AgendaTranslationMakerChecker>().ReverseMap();

            CreateMap<AgendaViewModel, AgendaModification>().ReverseMap();
            CreateMap<AgendaViewModel, AgendaModificationMakerChecker>().ReverseMap();

            CreateMap<AgendaMeetingTypeViewModel, AgendaMeetingType>().ReverseMap();
            CreateMap<AgendaMeetingTypeViewModel, AgendaMeetingTypeMakerChecker>().ReverseMap();

            // ################### Department ###################

            CreateMap<DepartmentViewModel, Department>().ReverseMap();
            CreateMap<DepartmentViewModel, DepartmentMakerChecker>().ReverseMap();

            CreateMap<DepartmentViewModel, DepartmentTranslation>().ReverseMap();
            CreateMap<DepartmentViewModel, DepartmentTranslationMakerChecker>().ReverseMap();

            CreateMap<DepartmentViewModel, DepartmentModification>().ReverseMap();
            CreateMap<DepartmentViewModel, DepartmentModificationMakerChecker>().ReverseMap();

            // ################### Designation ###################

            CreateMap<DesignationViewModel, Designation>().ReverseMap();
            CreateMap<DesignationViewModel, DesignationMakerChecker>().ReverseMap();

            CreateMap<DesignationViewModel, DesignationTranslation>().ReverseMap();
            CreateMap<DesignationViewModel, DesignationTranslationMakerChecker>().ReverseMap();

            CreateMap<DesignationViewModel, DesignationModification>().ReverseMap();
            CreateMap<DesignationViewModel, DesignationModificationMakerChecker>().ReverseMap();

            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Notice @@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            // ################### Schedule  ###################

            CreateMap<ScheduleViewModel, Schedule>().ReverseMap();
            CreateMap<ScheduleViewModel, ScheduleMakerChecker>().ReverseMap();

            CreateMap<ScheduleViewModel, ScheduleTranslation>().ReverseMap();
            CreateMap<ScheduleViewModel, ScheduleTranslationMakerChecker>().ReverseMap();

            CreateMap<ScheduleViewModel, ScheduleModification>().ReverseMap();
            CreateMap<ScheduleViewModel, ScheduleModificationMakerChecker>().ReverseMap();

            CreateMap<ScheduleViewModel, ScheduleFrequency>().ReverseMap();
            CreateMap<ScheduleViewModel, ScheduleFrequencyMakerChecker>().ReverseMap();

            CreateMap<ScheduleFrequencyViewModel, ScheduleFrequency>().ReverseMap();
            CreateMap<ScheduleFrequencyViewModel, ScheduleFrequencyMakerChecker>().ReverseMap();

            // ################### VehicleMake ###################

            CreateMap<VehicleMakeViewModel, VehicleMake>().ReverseMap();
            CreateMap<VehicleMakeViewModel, VehicleMakeMakerChecker>().ReverseMap();

            CreateMap<VehicleMakeViewModel, VehicleMakeTranslation>().ReverseMap();
            CreateMap<VehicleMakeViewModel, VehicleMakeTranslationMakerChecker>().ReverseMap();

            CreateMap<VehicleMakeViewModel, VehicleMakeModification>().ReverseMap();
            CreateMap<VehicleMakeViewModel, VehicleMakeModificationMakerChecker>().ReverseMap();

            // ################### VehicleModel ###################

            CreateMap<VehicleModelViewModel, VehicleModel>().ReverseMap();
            CreateMap<VehicleModelViewModel, VehicleModelMakerChecker>().ReverseMap();

            CreateMap<VehicleModelViewModel, VehicleModelTranslation>().ReverseMap();
            CreateMap<VehicleModelViewModel, VehicleModelTranslationMakerChecker>().ReverseMap();

            CreateMap<VehicleModelViewModel, VehicleModelModification>().ReverseMap();
            CreateMap<VehicleModelViewModel, VehicleModelModificationMakerChecker>().ReverseMap();

            // ################### VehicleVariant ###################

            CreateMap<VehicleVariantViewModel, VehicleVariant>().ReverseMap();
            CreateMap<VehicleVariantViewModel, VehicleVariantMakerChecker>().ReverseMap();

            CreateMap<VehicleVariantViewModel, VehicleVariantTranslation>().ReverseMap();
            CreateMap<VehicleVariantViewModel, VehicleVariantTranslationMakerChecker>().ReverseMap();

            CreateMap<VehicleVariantViewModel, VehicleVariantModification>().ReverseMap();
            CreateMap<VehicleVariantViewModel, VehicleVariantModificationMakerChecker>().ReverseMap();

            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ P A R A M E T E R @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            // ################### ACCOUNT ###################
            // ############################# Deposit Scheme Parameter #############################

            CreateMap<DepositSchemeParameterViewModel, DepositSchemeParameter>().ReverseMap();
            CreateMap<DepositSchemeParameterViewModel, DepositSchemeParameterMakerChecker>().ReverseMap();

            // ############################# LoanSchemeParameter #############################

            CreateMap<LoanSchemeParameterViewModel, LoanSchemeParameter>().ReverseMap();
            CreateMap<LoanSchemeParameterViewModel, LoanSchemeParameterMakerChecker>().ReverseMap();

            CreateMap<SharesCapitalSchemeParameterViewModel, SharesCapitalSchemeParameter>().ReverseMap();
            CreateMap<SharesCapitalSchemeParameterViewModel, SharesCapitalSchemeParameterMakerChecker>().ReverseMap();

            // ################### BoardOfDirector ###################

            CreateMap<BoardOfDirectorParameterViewModel, BoardOfDirectorParameter>().ReverseMap();
            CreateMap<BoardOfDirectorParameterViewModel, BoardOfDirectorParameterMakerChecker>().ReverseMap();

            // ################### Legal ###################     

            CreateMap<SharesCapitalByLawsParameterViewModel, SharesCapitalByLawsParameter>().ReverseMap();
            CreateMap<SharesCapitalByLawsParameterViewModel, SharesCapitalByLawsParameterMakerChecker>().ReverseMap();

            // ############################# Person #############################    

            CreateMap<PersonInformationParameterViewModel, PersonInformationParameter>().ReverseMap();
            CreateMap<PersonInformationParameterViewModel, PersonInformationParameterMakerChecker>().ReverseMap();

              // *** Check For Default Values
            //CreateMap<PersonInformationParameterViewModel, PersonInformationParameter>().ForMember(d => d.ReasonForModification, act => act.MapFrom(t => "None"));


            CreateMap<PersonInformationParameterDocumentTypeViewModel, PersonInformationParameterDocumentType>().ReverseMap();
            CreateMap<PersonInformationParameterDocumentTypeViewModel, PersonInformationParameterDocumentTypeMakerChecker>().ReverseMap();

            CreateMap<PersonInformationParameterNoticeTypeViewModel, PersonInformationParameterNoticeType>().ReverseMap();
            CreateMap<PersonInformationParameterNoticeTypeViewModel, PersonInformationParameterNoticeTypeMakerChecker>().ReverseMap();

            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ P E R S O N      I N F O R M A T I O N @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            CreateMap<PersonViewModel, Person>().ReverseMap();
            CreateMap<PersonViewModel, PersonMakerChecker>().ReverseMap();

            CreateMap<PersonViewModel, PersonTranslation>().ReverseMap();
            CreateMap<PersonViewModel, PersonTranslationMakerChecker>().ReverseMap();

            CreateMap<PersonGroupMasterViewModel, PersonModification>().ReverseMap();
            CreateMap<PersonGroupMasterViewModel, PersonModificationMakerChecker>().ReverseMap();

            CreateMap<PersonGroupMasterViewModel, PersonTranslation>().ReverseMap();
            CreateMap<PersonGroupMasterViewModel, PersonTranslationMakerChecker>().ReverseMap();

            //****************PersonMaster*********************
            CreateMap<PersonMasterViewModel, PersonModification>().ReverseMap();
            CreateMap<PersonMasterViewModel, PersonModificationMakerChecker>().ReverseMap();

            CreateMap<PersonMasterViewModel, PersonTranslation>().ReverseMap();
            CreateMap<PersonMasterViewModel, PersonTranslationMakerChecker>().ReverseMap();


            //****************PersonPrefix*********************

            CreateMap<PersonPrefixViewModel, PersonPrefix>().ReverseMap();
            CreateMap<PersonPrefixViewModel, PersonPrefixMakerChecker>().ReverseMap();

            //***********************PersonRelative****************

            CreateMap<PersonViewModel, PersonRelative>().ReverseMap();
            CreateMap<PersonViewModel, PersonRelativeMakerChecker>().ReverseMap();

            //***********************PersonContactDetail***************

            CreateMap<PersonContactDetailViewModel, PersonContactDetail>().ReverseMap();
            CreateMap<PersonContactDetailViewModel, PersonContactDetailMakerChecker>().ReverseMap();

            CreateMap<PersonCreditRatingViewModel, PersonCreditRating>().ReverseMap();
            CreateMap<PersonCreditRatingViewModel, PersonCreditRatingMakerChecker>().ReverseMap();

            CreateMap<PersonAddressViewModel, PersonAddress>().ReverseMap();
            CreateMap<PersonAddressViewModel, PersonAddressMakerChecker>().ReverseMap();

            CreateMap<PersonAddressViewModel, PersonAddressTranslation>().ReverseMap();
            CreateMap<PersonAddressViewModel, PersonAddressTranslationMakerChecker>().ReverseMap();

            // ############################# Person Commodities Asset #############################

            CreateMap<PersonCommoditiesAssetViewModel, PersonCommoditiesAsset>().ReverseMap();
            CreateMap<PersonCommoditiesAssetViewModel, PersonCommoditiesAssetMakerChecker>().ReverseMap();

            // ############################# Foreigner Person #############################

            CreateMap<ForeignerViewModel, ForeignerPerson>().ReverseMap();
            CreateMap<ForeignerViewModel, ForeignerPersonMakerChecker>().ReverseMap();

            // ############################# Guardian person #############################

            CreateMap<GuardianPersonViewModel, GuardianPerson>().ReverseMap();
            CreateMap<GuardianPersonViewModel, GuardianPersonMakerChecker>().ReverseMap();

            CreateMap<GuardianPersonViewModel, GuardianPersonTranslation>().ReverseMap();
            CreateMap<GuardianPersonViewModel, GuardianPersonTranslationMakerChecker>().ReverseMap();

            // ############################# Person Additional Detail #############################

            CreateMap<PersonAdditionalDetailViewModel, PersonAdditionalDetail>().ReverseMap();
            CreateMap<PersonAdditionalDetailViewModel, PersonAdditionalDetailMakerChecker>().ReverseMap();

            CreateMap<PersonAdditionalDetailViewModel, PersonAdditionalDetailTranslation>().ReverseMap();
            CreateMap<PersonAdditionalDetailViewModel, PersonAdditionalDetailTranslationMakerChecker>().ReverseMap();

            //************************PersonEmployementDetail***************************

            CreateMap<PersonEmploymentDetailViewModel, PersonEmploymentDetail>().ReverseMap();
            CreateMap<PersonEmploymentDetailViewModel, PersonEmploymentDetailMakerChecker>().ReverseMap();

            CreateMap<PersonEmploymentDetailViewModel, PersonEmploymentDetailTranslation>().ReverseMap();
            CreateMap<PersonEmploymentDetailViewModel, PersonEmploymentDetailTranslationMakerChecker>().ReverseMap();
            
            CreateMap<PersonEmploymentDetailViewModel, CustomerAccountEmploymentDetail>().ReverseMap();
            CreateMap<PersonEmploymentDetailViewModel, CustomerAccountEmploymentDetailMakerChecker>().ReverseMap();

            // ############################# Person Additional Income Detail #############################

            CreateMap<PersonAdditionalIncomeDetailViewModel, PersonAdditionalIncomeDetail>().ReverseMap();
            CreateMap<PersonAdditionalIncomeDetailViewModel, PersonAdditionalIncomeDetailMakerChecker>().ReverseMap();

            // ############################# Person Agriculture Asset #############################

            CreateMap<PersonAgricultureAssetViewModel, PersonAgricultureAsset>().ReverseMap();
            CreateMap<PersonAgricultureAssetViewModel, PersonAgricultureAssetMakerChecker>().ReverseMap();

            CreateMap<PersonAgricultureAssetViewModel, PersonAgricultureAssetDocument>().ReverseMap();
            CreateMap<PersonAgricultureAssetViewModel, PersonAgricultureAssetDocumentMakerChecker>().ReverseMap();

            // ############################# Person Bank Detail #############################

            CreateMap<PersonBankDetailViewModel, PersonBankDetail>().ReverseMap();
            CreateMap<PersonBankDetailViewModel, PersonBankDetailMakerChecker>().ReverseMap();

            CreateMap<PersonBankDetailViewModel, PersonBankDetailDocument>().ReverseMap();
            CreateMap<PersonBankDetailViewModel, PersonBankDetailDocumentMakerChecker>().ReverseMap();

            
            // ############################# Person Board Of Director Relation #############################

            CreateMap<PersonBoardOfDirectorRelationViewModel, PersonBoardOfDirectorRelation>().ReverseMap();
            CreateMap<PersonBoardOfDirectorRelationViewModel, PersonBoardOfDirectorRelationMakerChecker>().ReverseMap();

            // ############################# Person Borrowing Detail #############################

            CreateMap<PersonBorrowingDetailViewModel, PersonBorrowingDetail>().ReverseMap();
            CreateMap<PersonBorrowingDetailViewModel, PersonBorrowingDetailMakerChecker>().ReverseMap();

            CreateMap<PersonBorrowingDetailViewModel, PersonBorrowingDetailTranslation>().ReverseMap();
            CreateMap<PersonBorrowingDetailViewModel, PersonBorrowingDetailTranslationMakerChecker>().ReverseMap();

            // ############################# Person Court Case #############################

            CreateMap<PersonCourtCaseViewModel, PersonCourtCase>().ReverseMap();
            CreateMap<PersonCourtCaseViewModel, PersonCourtCaseMakerChecker>().ReverseMap();

            // ############################# Person Chronic Disease #############################

            CreateMap<PersonChronicDiseaseViewModel, PersonChronicDisease>().ReverseMap();
            CreateMap<PersonChronicDiseaseViewModel, PersonChronicDiseaseMakerChecker>().ReverseMap();

            // ############################# PersonDeath #############################

            CreateMap<PersonDeathViewModel, PersonDeath>().ReverseMap();
            CreateMap<PersonDeathViewModel, PersonDeathMakerChecker>().ReverseMap();

            CreateMap<PersonDeathViewModel, PersonDeathDocument>().ReverseMap();
            CreateMap<PersonDeathViewModel, PersonDeathDocumentMakerChecker>().ReverseMap();

            CreateMap<PersonDeathDocumentViewModel, PersonDeathDocument>().ReverseMap();
            CreateMap<PersonDeathDocumentViewModel, PersonDeathDocumentMakerChecker>().ReverseMap();

            // ############################# Person Family Detail #############################

            CreateMap<PersonFamilyDetailViewModel, PersonFamilyDetail>().ReverseMap();
            CreateMap<PersonFamilyDetailViewModel, PersonFamilyDetailMakerChecker>().ReverseMap();

            CreateMap<PersonFamilyDetailViewModel, PersonFamilyDetailTranslation>().ReverseMap();
            CreateMap<PersonFamilyDetailViewModel, PersonFamilyDetailTranslationMakerChecker>().ReverseMap();

            //############################# Person Home Branch #############################

            CreateMap<PersonHomeBranchViewModel, PersonHomeBranch>().ReverseMap();
            CreateMap<PersonHomeBranchViewModel, PersonHomeBranchMakerChecker>().ReverseMap();

            // ############################# Person Immovable Asset #############################

            CreateMap<PersonImmovableAssetViewModel, PersonImmovableAsset>().ReverseMap();
            CreateMap<PersonImmovableAssetViewModel, PersonImmovableAssetMakerChecker>().ReverseMap();

            CreateMap<PersonImmovableAssetViewModel, PersonImmovableAssetDocument>().ReverseMap();
            CreateMap<PersonImmovableAssetViewModel, PersonImmovableAssetDocumentMakerChecker>().ReverseMap();

            // ############################# Person Income Tax Detail #############################

            CreateMap<PersonIncomeTaxDetailViewModel, PersonIncomeTaxDetail>().ReverseMap();
            CreateMap<PersonIncomeTaxDetailViewModel, PersonIncomeTaxDetailMakerChecker>().ReverseMap();

            CreateMap<PersonIncomeTaxDetailViewModel, PersonIncomeTaxDetailDocument>().ReverseMap();
            CreateMap<PersonIncomeTaxDetailViewModel, PersonIncomeTaxDetailDocumentMakerChecker>().ReverseMap();

            //Added By Rahul Kharat Date 18.10.2024 03.56 PM
            // ############################# Person KYCDocument #############################
            CreateMap<PersonKYCDocumentViewModel, PersonKYCDetail>().ReverseMap();
            CreateMap<PersonKYCDocumentViewModel, PersonKYCDetailMakerChecker>().ReverseMap();
            CreateMap<PersonKYCDocumentViewModel, PersonKYCDetailDocument>().ReverseMap();
            CreateMap<PersonKYCDocumentViewModel, PersonKYCDetailDocumentMakerChecker>().ReverseMap();

            //############################# Person Machinary Asset #############################

            CreateMap<PersonMachineryAssetViewModel, PersonMovableAsset>().ReverseMap();
            CreateMap<PersonMachineryAssetViewModel, PersonMachineryAssetMakerChecker>().ReverseMap();

            CreateMap<PersonMachineryAssetViewModel, PersonMachineryAssetDocument>().ReverseMap();
            CreateMap<PersonMachineryAssetViewModel, PersonMachineryAssetDocumentMakerChecker>().ReverseMap();

            //############################# Person Movable Asset #############################

            CreateMap<PersonMovableAssetViewModel, PersonMovableAsset>().ReverseMap();
            CreateMap<PersonMovableAssetViewModel, PersonMovableAssetMakerChecker>().ReverseMap();

            CreateMap<PersonMovableAssetViewModel, PersonMovableAssetDocument>().ReverseMap();
            CreateMap<PersonMovableAssetViewModel, PersonMovableAssetDocumentMakerChecker>().ReverseMap();

            // #############################  PersonInsurance Detail #############################

            CreateMap<PersonInsuranceDetailViewModel, PersonInsuranceDetail>().ReverseMap();
            CreateMap<PersonInsuranceDetailViewModel, PersonInsuranceDetailMakerChecker>().ReverseMap();

            // ############################# Person Financial Asset#############################

            CreateMap<PersonFinancialAssetViewModel, PersonFinancialAsset>().ReverseMap();
            CreateMap<PersonFinancialAssetViewModel, PersonFinancialAssetMakerChecker>().ReverseMap();

            CreateMap<PersonFinancialAssetViewModel, PersonFinancialAssetTranslation>().ReverseMap();
            CreateMap<PersonFinancialAssetViewModel, PersonFinancialAssetTranslationMakerChecker>().ReverseMap();

            CreateMap<PersonFinancialAssetViewModel, PersonFinancialAssetDocument>().ReverseMap();
            CreateMap<PersonFinancialAssetViewModel, PersonFinancialAssetDocumentMakerChecker>().ReverseMap();

            CreateMap<PersonFinancialAssetDocumentViewModel, PersonFinancialAssetDocument>().ReverseMap();
            CreateMap<PersonFinancialAssetDocumentViewModel, PersonFinancialAssetDocumentMakerChecker>().ReverseMap();

            //###########################PersonFinancialAssetBorrowingDetail##########################

            CreateMap<PersonFinancialAssetDocumentViewModel, PersonFinancialAssetBorrowingDetail>().ReverseMap();
            CreateMap<PersonFinancialAssetDocumentViewModel, PersonFinancialAssetBorrowingDetailMakerChecker>().ReverseMap();

            // ############################# Person GST Registration Detail #############################

            CreateMap<PersonGSTRegistrationDetailViewModel, PersonGSTRegistrationDetail>().ReverseMap();
            CreateMap<PersonGSTRegistrationDetailViewModel, PersonGSTRegistrationDetailMakerChecker>().ReverseMap();

            CreateMap<PersonGSTRegistrationDetailViewModel, PersonGSTReturnDocument>().ReverseMap();
            CreateMap<PersonGSTRegistrationDetailViewModel, PersonGSTReturnDocumentMakerChecker>().ReverseMap();

            CreateMap<PersonGSTReturnDocumentViewModel, PersonGSTReturnDocument>().ReverseMap();
            CreateMap<PersonGSTReturnDocumentViewModel, PersonGSTReturnDocumentMakerChecker>().ReverseMap();

            // ############################# PersonMachineryAsset #############################

            CreateMap<PersonMachineryAssetViewModel, PersonMachineryAsset>().ReverseMap();
            CreateMap<PersonMachineryAssetViewModel, PersonMachineryAssetMakerChecker>().ReverseMap();

            // ########################### PersonMachineryAssetDocument ##########################

            CreateMap<PersonMachineryAssetViewModel, PersonMachineryAssetDocument>().ReverseMap();
            CreateMap<PersonMachineryAssetViewModel, PersonMachineryAssetDocumentMakerChecker>().ReverseMap();

            // ############################# person Photo Sign #############################

            CreateMap<PersonPhotoSignViewModel, PersonPhotoSign>().ReverseMap();
            CreateMap<PersonPhotoSignViewModel, PersonPhotoSignMakerChecker>().ReverseMap();

            // ############################# PersonSMSAlert #############################

            CreateMap<PersonSMSAlertViewModel, PersonSMSAlert>().ReverseMap();
            CreateMap<PersonSMSAlertViewModel, PersonSMSAlertMakerChecker>().ReverseMap();

            // ############################# Person Social Media #############################

            CreateMap<PersonSocialMediaViewModel, PersonSocialMedia>().ReverseMap();
            CreateMap<PersonSocialMediaViewModel, PersonSocialMediaMakerChecker>().ReverseMap();

            // ############################# Person GroupAuthorized Signatory #############################

            CreateMap<PersonGroupAuthorizedSignatoryViewModel, PersonGroupAuthorizedSignatory>().ReverseMap();
            CreateMap<PersonGroupAuthorizedSignatoryViewModel, PersonGroupAuthorizedSignatoryMakerChecker>().ReverseMap();
            CreateMap<PersonGroupAuthorizedSignatoryViewModel, PersonGroupAuthorizedSignatoryTranslation>().ReverseMap();
            CreateMap<PersonGroupAuthorizedSignatoryViewModel, PersonGroupAuthorizedSignatoryTranslationMakerChecker>().ReverseMap();

            // ############################# PersonGroup #############################

            CreateMap<PersonGroupMasterViewModel, PersonGroup>().ReverseMap();
            CreateMap<PersonGroupMasterViewModel, PersonGroupMakerChecker>().ReverseMap();

            CreateMap<PersonGroupViewModel, PersonGroup>().ReverseMap();
            CreateMap<PersonGroupViewModel, PersonGroupMakerChecker>().ReverseMap();
            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Security @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            // ############################# UserProfile #############################

            CreateMap<UserProfileViewModel, UserProfile>().ReverseMap();
            CreateMap<UserProfileViewModel, UserProfileMakerChecker>().ReverseMap();

            CreateMap<UserProfileViewModel, UserProfileModification>().ReverseMap();
            CreateMap<UserProfileViewModel, UserProfileModificationMakerChecker>().ReverseMap();

            CreateMap<UserProfileViewModel, UserProfileAccessibility>().ReverseMap();

            CreateMap<UserProfileViewModel, UserProfileBusinessOffice>().ReverseMap();
            CreateMap<UserProfileViewModel, UserProfileBusinessOfficeMakerChecker>().ReverseMap();

            CreateMap<UserProfileBusinessOfficeViewModel, UserProfileBusinessOffice>().ReverseMap();
            CreateMap<UserProfileBusinessOfficeViewModel, UserProfileBusinessOfficeMakerChecker>().ReverseMap();

            CreateMap<UserProfileViewModel, UserProfileCurrency>().ReverseMap();
            CreateMap<UserProfileViewModel, UserProfileCurrencyMakerChecker>().ReverseMap();

            CreateMap<UserProfileCurrencyViewModel, UserProfileCurrency>().ReverseMap();
            CreateMap<UserProfileCurrencyViewModel, UserProfileCurrencyMakerChecker>().ReverseMap();

            CreateMap<UserProfileViewModel, UserProfileGeneralLedger>().ReverseMap();
            CreateMap<UserProfileViewModel, UserProfileGeneralLedgerMakerChecker>().ReverseMap();

            CreateMap<UserProfileGeneralLedgerViewModel, UserProfileGeneralLedger>().ReverseMap();
            CreateMap<UserProfileGeneralLedgerViewModel, UserProfileGeneralLedgerMakerChecker>().ReverseMap();

            CreateMap<UserProfileGroupViewModel, UserProfileGroup>().ReverseMap();
            CreateMap<UserProfileGroupViewModel, UserProfileGroupMakerChecker>().ReverseMap();

            CreateMap<UserProfileHomeBusinessOfficeViewModel, UserProfileHomeBusinessOffice>().ReverseMap();
            CreateMap<UserProfileHomeBusinessOfficeViewModel, UserProfileHomeBusinessOfficeMakerChecker>().ReverseMap();

            CreateMap<UserProfileViewModel, UserProfileIdentity>().ReverseMap();

            CreateMap<UserProfileViewModel, UserProfileLoginDevice>().ReverseMap();
            CreateMap<UserProfileViewModel, UserProfileLoginDeviceMakerChecker>().ReverseMap();

            CreateMap<UserProfileLoginDeviceViewModel, UserProfileLoginDevice>();
            CreateMap<UserProfileLoginDeviceViewModel, UserProfileLoginDeviceMakerChecker>();

            CreateMap<UserProfileViewModel, UserProfileMenu>().ReverseMap();
            CreateMap<UserProfileViewModel, UserProfileMenuMakerChecker>().ReverseMap();

            CreateMap<UserProfileMenuViewModel, UserProfileMenu>().ReverseMap();
            CreateMap<UserProfileMenuViewModel, UserProfileMenuMakerChecker>().ReverseMap();

            CreateMap<UserProfileViewModel, UserProfilePasswordPolicy>().ReverseMap();
            CreateMap<UserProfileViewModel, UserProfilePasswordPolicyMakerChecker>().ReverseMap();

            CreateMap<UserProfilePasswordPolicyViewModel, UserProfilePasswordPolicy>().ReverseMap();
            CreateMap<UserProfilePasswordPolicyViewModel, UserProfilePasswordPolicyMakerChecker>().ReverseMap();

            CreateMap<UserProfileViewModel, UserProfileSpecialPermission>().ReverseMap();
            CreateMap<UserProfileViewModel, UserProfileSpecialPermissionMakerChecker>().ReverseMap();

            CreateMap<UserProfileSpecialPermissionViewModel, UserProfileSpecialPermission>().ReverseMap();
            CreateMap<UserProfileSpecialPermissionViewModel, UserProfileSpecialPermissionMakerChecker>().ReverseMap();

            CreateMap<UserProfileViewModel, UserProfileTransactionLimit>().ReverseMap();
            CreateMap<UserProfileViewModel, UserProfileTransactionLimitMakerChecker>().ReverseMap();

            CreateMap<UserProfileTransactionLimitViewModel, UserProfileTransactionLimit>().ReverseMap();
            CreateMap<UserProfileTransactionLimitViewModel, UserProfileTransactionLimitMakerChecker>().ReverseMap();

            CreateMap<UserProfileViewModel, UserRoleProfile>().ReverseMap();
            CreateMap<UserProfileViewModel, UserRoleProfileMakerChecker>().ReverseMap();

            CreateMap<UserRoleProfileViewModel, UserRoleProfile>().ReverseMap();
            CreateMap<UserRoleProfileViewModel, UserRoleProfileMakerChecker>().ReverseMap();

            CreateMap<EmployeeViewModel, EmployeePhoto>().ReverseMap();
            CreateMap<EmployeeViewModel, EmployeePhotoMakerChecker>().ReverseMap();

            CreateMap<EmployeeSalaryStructureViewModel, EmployeeSalaryStructure>().ReverseMap();
            CreateMap<EmployeeSalaryStructureViewModel, EmployeeSalaryStructureMakerChecker>().ReverseMap();

            CreateMap<EmployeeViewModel, EmployeeWorkingSchedule>().ReverseMap();
            CreateMap<EmployeeViewModel, EmployeeWorkingScheduleMakerChecker>().ReverseMap();

            CreateMap<UserAuthenticationParameterViewModel, UserAuthenticationParameter>().ReverseMap();
            CreateMap<UserAuthenticationParameterViewModel, UserAuthenticationParameterMakerChecker>().ReverseMap();

            // *************************************Users******************************
            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ P A S S W O R D @@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            // ############################# PasswordPolicy #############################

            CreateMap<PasswordPolicyViewModel, PasswordPolicy>().ReverseMap();
            CreateMap<PasswordPolicyViewModel, PasswordPolicyMakerChecker>().ReverseMap();

            CreateMap<PasswordPolicyViewModel, PasswordPolicyModification>().ReverseMap();
            CreateMap<PasswordPolicyViewModel, PasswordPolicyModificationMakerChecker>().ReverseMap();

            // ############################# RoleProfile #############################

            CreateMap<RoleProfileViewModel, RoleProfile>().ReverseMap();
            CreateMap<RoleProfileViewModel, RoleProfileMakerChecker>().ReverseMap();

            CreateMap<RoleProfileViewModel, RoleProfileModification>().ReverseMap();
            CreateMap<RoleProfileViewModel, RoleProfileModificationMakerChecker>().ReverseMap();

            CreateMap<RoleProfileViewModel, RoleProfileTranslation>().ReverseMap();
            CreateMap<RoleProfileViewModel, RoleProfileTranslationMakerChecker>().ReverseMap();

            CreateMap<RoleProfileViewModel, RoleProfileBusinessOffice>().ReverseMap();
            CreateMap<RoleProfileViewModel, RoleProfileBusinessOfficeMakerChecker>().ReverseMap();

            CreateMap<RoleProfileViewModel, RoleProfileGeneralLedger>().ReverseMap();
            CreateMap<RoleProfileViewModel, RoleProfileGeneralLedgerMakerChecker>().ReverseMap();

            CreateMap<RoleProfileViewModel, RoleProfileMenu>().ReverseMap();
            CreateMap<RoleProfileViewModel, RoleProfileMenuMakerChecker>().ReverseMap();

            CreateMap<RoleProfileViewModel, RoleProfileSpecialPermission>().ReverseMap();
            CreateMap<RoleProfileViewModel, RoleProfileSpecialPermissionMakerChecker>().ReverseMap();

            CreateMap<RoleProfileViewModel, RoleProfileTransactionLimit>().ReverseMap();
            CreateMap<RoleProfileViewModel, RoleProfileTransactionLimitMakerChecker>().ReverseMap();

            CreateMap<RoleProfileSpecialPermissionViewModel, RoleProfileSpecialPermission>().ReverseMap();
            CreateMap<RoleProfileSpecialPermissionViewModel, RoleProfileSpecialPermissionMakerChecker>().ReverseMap();

            CreateMap<RoleProfileMenuViewModel, RoleProfileMenu>().ReverseMap();
            CreateMap<RoleProfileMenuViewModel, RoleProfileMenuMakerChecker>().ReverseMap();

            CreateMap<RoleProfileGeneralLedgerViewModel, RoleProfileGeneralLedger>().ReverseMap();
            CreateMap<RoleProfileGeneralLedgerViewModel, RoleProfileGeneralLedgerMakerChecker>().ReverseMap();

            CreateMap<RoleProfileBusinessOfficeViewModel, RoleProfileBusinessOffice>().ReverseMap();
            CreateMap<RoleProfileBusinessOfficeViewModel, RoleProfileBusinessOfficeMakerChecker>().ReverseMap();

            CreateMap<RoleProfileTransactionLimitViewModel, RoleProfileTransactionLimit>().ReverseMap();
            CreateMap<RoleProfileTransactionLimitViewModel, RoleProfileTransactionLimitMakerChecker>().ReverseMap();

            // ########################## CustomerAccountSmsServices ####################
            CreateMap<CustomerAccountSmsServiceViewModel, CustomerAccountSmsService>().ReverseMap();
            CreateMap<CustomerAccountSmsServiceViewModel, CustomerAccountSmsServiceMakerChecker>().ReverseMap();

            // ########################## CustomerAccountEmailServices ####################
            CreateMap<CustomerAccountEmailServiceViewModel, CustomerAccountEmailService>().ReverseMap();
            CreateMap<CustomerAccountEmailServiceViewModel, CustomerAccountEmailServiceMakerChecker>().ReverseMap();
            
            // ############################# Menu Search #############################

            CreateMap<MenuViewModel, SearchQuery>().ReverseMap();

            CreateMap<MenuViewModel, SearchQueryResult>().ReverseMap();

            CreateMap<MenuSearchQueryResultViewModel, MenuSearchQueryResult>().ReverseMap();

            //###########################ByLawsLoanScheduleParameterViewModel################
            CreateMap<ByLawsLoanScheduleParameterViewModel, ByLawsLoanScheduleParameter>().ReverseMap();
            CreateMap<ByLawsLoanScheduleParameterViewModel, ByLawsLoanScheduleParameterMakerChecker>().ReverseMap();


        }
    }
}
