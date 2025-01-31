using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Layout
{
    public class EFSchemeDetailRepository : ISchemeDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;

        public EFSchemeDetailRepository(IAccountDetailRepository _accountDetailRepository, RepositoryConnection _connection)
        {
            accountDetailRepository = _accountDetailRepository;
            context = _connection.EFDbContext;
        }

        //BusinessOffice
        public async Task<IEnumerable<SchemeBusinessOfficeViewModel>> GetBusinessOfficeEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeBusinessOfficeViewModel>("SELECT * FROM dbo.GetSchemeBusinessOfficeEntriesBySchemePrmKey (@SchemePrmkey, @EntryType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        
        //ChargesDetail
        public async Task<IEnumerable<SchemeChargesDetailViewModel>> GetChargesDetailEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeChargesDetailViewModel>("SELECT * FROM dbo.GetSchemeChargesDetailEntriesBySchemePrmKey (@SchemePrmkey, @EntryType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        // ClosingChargesEntries
        public async Task<IEnumerable<SchemeClosingChargesViewModel>> GetClosingChargesEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeClosingChargesViewModel>("SELECT * FROM dbo.GetSchemeClosingChargesEntriesBySchemePrmKey (@SchemePrmkey, @EntryType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SchemeConsumerDurableLoanItemViewModel>> GetSchemeConsumerDurableLoanItemEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<SchemeConsumerDurableLoanItemViewModel>("SELECT * FROM dbo.GetSchemeConsumerDurableLoanItemEntriesBySchemePrmKey (@SchemePrmkey, @EntriesType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //AgentIncentive
        public async Task<IEnumerable<SchemeDepositAgentIncentiveViewModel>> GetAgentIncentiveEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                
                var a= await context.Database.SqlQuery<SchemeDepositAgentIncentiveViewModel>("SELECT * FROM dbo.GetSchemeDepositAgentIncentiveEntriesBySchemePrmKey (@SchemePrmkey, @EntriesType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SchemeDocumentViewModel>> GetDocumentEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                IEnumerable<SchemeDocumentViewModel> schemeDocumentTypeViewModels;
                schemeDocumentTypeViewModels = await context.Database.SqlQuery<SchemeDocumentViewModel>("SELECT * FROM dbo.GetSchemeDocumentEntriesBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return schemeDocumentTypeViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //GeneralLedger
        public async Task<IEnumerable<SchemeGeneralLedgerViewModel>> GetGeneralLedgerEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeGeneralLedgerViewModel>("SELECT * FROM dbo.GetSchemeGeneralLedgerEntriesBySchemePrmKey (@SchemePrmkey, @EntryType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SchemeLoanChargesParameterViewModel>> GetLoanChargesParameterEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanChargesParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanChargesParameterEntriesBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SchemeLoanOverduesActionViewModel>> GetLoanOverduesActionEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanOverduesActionViewModel>("SELECT * FROM dbo.GetSchemeLoanOverduesActionEntriesBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //NoticeSchedule
        public async Task<IEnumerable<SchemeNoticeScheduleViewModel>> GetNoticeScheduleEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeNoticeScheduleViewModel>("SELECT * FROM dbo.GetSchemeNoticeScheduleEntriesBySchemePrmKey (@SchemePrmkey, @EntryType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //NumberOfTransactionLimit
        public async Task<IEnumerable<SchemeNumberOfTransactionLimitViewModel>> GetNumberOfTransactionLimitEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeNumberOfTransactionLimitViewModel>("SELECT * FROM dbo.GetSchemeNumberOfTransactionLimitEntriesBySchemePrmKey (@SchemePrmkey, @EntriesType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<IEnumerable<SchemePreownedVehicleLoanParameterViewModel>> GetSchemePreownedVehicleLoanParameterEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<SchemePreownedVehicleLoanParameterViewModel>("SELECT * FROM dbo.GetSchemePreownedVehicleLoanParameterEntriesBySchemePrmKey (@SchemePrmkey, @EntriesType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemePreownedVehicleLoanParameterViewModel> GetPreownedVehicleLoanParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemePreownedVehicleLoanParameterViewModel schemePreownedVehicleLoanParameterViewModel = await context.Database.SqlQuery<SchemePreownedVehicleLoanParameterViewModel>("SELECT * FROM dbo.GetSchemePreownedVehicleLoanParameterEntriesBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                schemePreownedVehicleLoanParameterViewModel.PhotoDocumentFormatTypeIdForDatabase = schemePreownedVehicleLoanParameterViewModel.AllowedFileFormatsForDb.Split(',');
                schemePreownedVehicleLoanParameterViewModel.PhotoDocumentFormatTypeIdForLocalStorage = schemePreownedVehicleLoanParameterViewModel.AllowedFileFormatsForLocalStorage.Split(',');

                return schemePreownedVehicleLoanParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //ReportFormat
        public async Task<IEnumerable<SchemeReportFormatViewModel>> GetReportFormatEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeReportFormatViewModel>("SELECT * FROM dbo.GetSchemeReportFormatEntriesBySchemePrmKey (@SchemePrmkey, @EntryType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        // SharesTransferChargesEntries
        public async Task<IEnumerable<SchemeSharesTransferChargesViewModel>> GetSharesTransferChargesEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeSharesTransferChargesViewModel>("SELECT * FROM dbo.GetSchemeSharesTransferChargesEntriesBySchemePrmKey (@SchemePrmkey, @EntryType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SchemeTargetGroupViewModel>> GetTargetGroupEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeTargetGroupViewModel>("SELECT * FROM dbo.GetSchemeTargetGroupEntriesBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //TenureList
        public async Task<IEnumerable<SchemeTenureListViewModel>> GetTenureListEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeTenureListViewModel>("SELECT * FROM dbo.GetSchemeTenureListEntriesBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //TransactionAmountLimit
        public async Task<IEnumerable<SchemeTransactionAmountLimitViewModel>> GetTransactionAmountLimitEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeTransactionAmountLimitViewModel>("SELECT * FROM dbo.GetSchemeTransactionAmountLimitEntriesBySchemePrmKey (@SchemePrmkey, @EntriesType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SchemeVehicleTypeLoanParameterViewModel>> GetSchemeVehicleTypeLoanParameterEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<SchemeVehicleTypeLoanParameterViewModel>("SELECT * FROM dbo.GetSchemeVehicleTypeLoanParameterEntriesBySchemePrmKey (@SchemePrmkey, @EntriesType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerDepositAccountOpeningConfigViewModel> GetDepositSchemeConfigDetail(Guid _schemeId)
        {
            try
            {
                short schemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_schemeId);

                CustomerDepositAccountOpeningConfigViewModel customerDepositAccountOpeningConfigViewModel = new CustomerDepositAccountOpeningConfigViewModel
                {
                    SchemeAccountParameterViewModel = await GetAccountParameterEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeCustomerAccountNumberViewModel = await GetCustomerAccountNumberEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeDepositAccountParameterViewModel = await GetDepositAccountParameterEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeDemandDepositDetailViewModel = await GetDemandDepositDetailEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeFixedDepositParameterViewModel = await GetFixedDepositParameterEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeDepositCertificateParameterViewModel = await GetCertificateParameterEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeDepositInterestParameterViewModel = await GetDepositInterestParameterEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeDepositAccountRenewalParameterViewModel = await GetAccountRenewalParameterEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemePassbookViewModel = await GetPassbookEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeApplicationParameterViewModel = await GetApplicationParameterEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeDepositInstallmentParameterViewModel = await GetInstallmentParameterEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeTenureViewModel = await GetTenureEntry(schemePrmKey, StringLiteralValue.Verify)
                };

                return customerDepositAccountOpeningConfigViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<CustomerSharesAccountOpeningConfigViewModel> GetSharesCapitalSchemeConfigDetail(Guid _schemeId)
        {
            try
            {
                short schemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_schemeId);
                CustomerSharesAccountOpeningConfigViewModel customerSharesAccountOpeningConfigViewModel = new CustomerSharesAccountOpeningConfigViewModel
                {
                    SchemeAccountParameterViewModel = await GetAccountParameterEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeCustomerAccountNumberViewModel = await GetCustomerAccountNumberEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeSharesCapitalAccountParameterViewModel = await GetSharesCapitalAccountParameterEntry(schemePrmKey, StringLiteralValue.Verify),
                    SchemeApplicationParameterViewModel = await GetApplicationParameterEntry(schemePrmKey, StringLiteralValue.Verify),
                };

                return customerSharesAccountOpeningConfigViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }


        //AccountBankingChannelParameter
        public async Task<SchemeAccountBankingChannelParameterViewModel> GetAccountBankingChannelParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeAccountBankingChannelParameterViewModel>("SELECT * FROM dbo.GetSchemeAccountBankingChannelParameterEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //AccountParameter
        public async Task<SchemeAccountParameterViewModel> GetAccountParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeAccountParameterViewModel>("SELECT * FROM dbo.GetSchemeAccountParameterEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //ApplicationParameter
        public async Task<SchemeApplicationParameterViewModel> GetApplicationParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeApplicationParameterViewModel schemeApplicationParameterViewModel = await context.Database.SqlQuery<SchemeApplicationParameterViewModel>("SELECT * FROM dbo.GetSchemeApplicationParameterEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                schemeApplicationParameterViewModel.MaskTypeCharacterForApplication = schemeApplicationParameterViewModel.ApplicationNumberMask.Split(',');

                return schemeApplicationParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeCashCreditLoanParameterViewModel> GetSchemeCashCreditLoanParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeCashCreditLoanParameterViewModel>("SELECT * FROM dbo.GetSchemeCashCreditLoanParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeEducationLoanParameterViewModel> GetSchemeEducationLoanParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeEducationLoanParameterViewModel>("SELECT * FROM dbo.GetSchemeEducationLoanParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //SchemeEducationalCourse
        public async Task<IEnumerable<SchemeEducationalCourseViewModel>> GetSchemeEducationalCourseEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeEducationalCourseViewModel>("SELECT * FROM dbo.GetSchemeEducationalCourseEntriesBySchemePrmKey (@SchemePrmkey, @EntryType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //SchemeInstitute
        public async Task<IEnumerable<SchemeInstituteViewModel>> GetSchemeInstituteEntries(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeInstituteViewModel>("SELECT * FROM dbo.GetSchemeInstituteEntriesBySchemePrmKey (@SchemePrmkey, @EntryType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeLoanAgainstPropertyViewModel> GetSchemeLoanAgainstPropertyEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanAgainstPropertyViewModel>("SELECT * FROM dbo.GetSchemeLoanAgainstPropertyEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeBusinessLoanViewModel> GetSchemeBusinessLoanEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeBusinessLoanViewModel>("SELECT * FROM dbo.GetSchemeBusinessLoanEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeHomeLoanViewModel> GetSchemeHomeLoanEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeHomeLoanViewModel>("SELECT * FROM dbo.GetSchemeHomeLoanEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        //CustomerAccountNumber
        public async Task<SchemeCustomerAccountNumberViewModel> GetCustomerAccountNumberEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeCustomerAccountNumberViewModel schemeCustomerAccountNumberViewModel = await context.Database.SqlQuery<SchemeCustomerAccountNumberViewModel>("SELECT * FROM dbo.GetSchemeCustomerAccountNumberEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                schemeCustomerAccountNumberViewModel.MaskTypeCharacterForAccount = schemeCustomerAccountNumberViewModel.AccountNumberMask.Split(',');

                return schemeCustomerAccountNumberViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //DemandDepositDetail
        public async Task<SchemeDemandDepositDetailViewModel> GetDemandDepositDetailEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<SchemeDemandDepositDetailViewModel>("SELECT * FROM dbo.GetSchemeDemandDepositDetailEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //AccountClosureParameter
        public async Task<SchemeDepositAccountClosureParameterViewModel> GetAccountClosureParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeDepositAccountClosureParameterViewModel depositSchemeViewModel = await context.Database.SqlQuery<SchemeDepositAccountClosureParameterViewModel>("SELECT * FROM dbo.GetSchemeDepositAccountClosureParameterEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                return depositSchemeViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        // DepositAccountParameter
        public async Task<SchemeDepositAccountParameterViewModel> GetDepositAccountParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeDepositAccountParameterViewModel schemeDepositAccountParameterViewModel = await context.Database.SqlQuery<SchemeDepositAccountParameterViewModel>("SELECT * FROM dbo.GetSchemeDepositAccountParameterEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                return schemeDepositAccountParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //AccountRenewalParameter
        public async Task<SchemeDepositAccountRenewalParameterViewModel> GetAccountRenewalParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeDepositAccountRenewalParameterViewModel depositSchemeViewModel = await context.Database.SqlQuery<SchemeDepositAccountRenewalParameterViewModel>("SELECT * FROM dbo.GetSchemeDepositAccountRenewalParameterEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                return depositSchemeViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //AgentParameter
        public async Task<SchemeDepositAgentParameterViewModel> GetAgentParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeDepositAgentParameterViewModel depositSchemeViewModel = await context.Database.SqlQuery<SchemeDepositAgentParameterViewModel>("SELECT * FROM dbo.GetSchemeDepositAgentParameterEntryBySchemePrmKey (@SchemeId, @EntriesType)", new SqlParameter("@SchemeId", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                return depositSchemeViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //CertificateParameter
        public async Task<SchemeDepositCertificateParameterViewModel> GetCertificateParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeDepositCertificateParameterViewModel depositSchemeViewModel = await context.Database.SqlQuery<SchemeDepositCertificateParameterViewModel>("SELECT * FROM dbo.GetSchemeDepositCertificateParameterEntryBySchemePrmKey (@SchemeId, @EntriesType)", new SqlParameter("@SchemeId", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                depositSchemeViewModel.MaskTypeCharacterForCertificate = depositSchemeViewModel.CertificateNumberMask.Split(',');

                return depositSchemeViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //InstallmentParameter
        public async Task<SchemeDepositInstallmentParameterViewModel> GetInstallmentParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeDepositInstallmentParameterViewModel depositSchemeViewModel = await context.Database.SqlQuery<SchemeDepositInstallmentParameterViewModel>("SELECT * FROM dbo.GetSchemeDepositInstallmentParameterEntryBySchemePrmKey (@SchemeId, @EntriesType)", new SqlParameter("@SchemeId", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                return depositSchemeViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //InterestParameter
        public async Task<SchemeDepositInterestParameterViewModel> GetDepositInterestParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<SchemeDepositInterestParameterViewModel>("SELECT * FROM dbo.GetSchemeDepositInterestParameterEntryBySchemePrmKey (@SchemePrmkey, @EntriesType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        //InterestProvisionParameter
        public async Task<SchemeDepositInterestProvisionParameterViewModel> GetInterestProvisionParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeDepositInterestProvisionParameterViewModel>("SELECT * FROM dbo.GetSchemeDepositInterestProvisionParameterEntryBySchemePrmKey (@SchemePrmkey, @EntriesType)", new SqlParameter("@SchemePrmkey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //PledgeLoanParameter
        public async Task<SchemeDepositPledgeLoanParameterViewModel> GetPledgeLoanParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeDepositPledgeLoanParameterViewModel depositSchemeViewModel = await context.Database.SqlQuery<SchemeDepositPledgeLoanParameterViewModel>("SELECT * FROM dbo.GetSchemeDepositPledgeLoanParameterEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                return depositSchemeViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        //EstimateTarget
        public async Task<SchemeEstimateTargetViewModel> GetEstimateTargetEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<SchemeEstimateTargetViewModel>("SELECT * FROM dbo.GetSchemeEstimateTargetEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //FixedDepositParameter
        public async Task<SchemeFixedDepositParameterViewModel> GetFixedDepositParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeFixedDepositParameterViewModel depositSchemeViewModel = await context.Database.SqlQuery<SchemeFixedDepositParameterViewModel>("SELECT * FROM dbo.GetSchemeFixedDepositParameterEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                return depositSchemeViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeGoldLoanParameterViewModel> GetGoldLoanParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeGoldLoanParameterViewModel schemeGoldLoanParameterViewModel = await context.Database.SqlQuery<SchemeGoldLoanParameterViewModel>("SELECT * FROM dbo.GetSchemeGoldLoanParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                if (schemeGoldLoanParameterViewModel != null)
                {
                    schemeGoldLoanParameterViewModel.GoldPhotoAllowedFileFormatIdForDb = schemeGoldLoanParameterViewModel.GoldPhotoAllowedFileFormatsForDb.Split(',');
                    schemeGoldLoanParameterViewModel.GoldPhotoAllowedFileFormatIdForLocalStorage = schemeGoldLoanParameterViewModel.GoldPhotoAllowedFileFormatsForLocalStorage.Split(',');
                }
                return schemeGoldLoanParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
         public async Task<SchemeCashCreditLoanParameterViewModel> GetCashCreditLoanParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeCashCreditLoanParameterViewModel schemeCashCreditLoanParameterViewModel = await context.Database.SqlQuery<SchemeCashCreditLoanParameterViewModel>("SELECT * FROM dbo.GetSchemeCashCreditLoanParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                
                return schemeCashCreditLoanParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeInterestRateViewModel> GetInterestRateEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeInterestRateViewModel>("SELECT * FROM dbo.GetSchemeInterestRateEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Limit
        public async Task<SchemeLimitViewModel> GetLimitEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLimitViewModel>("SELECT * FROM dbo.GetSchemeLimitEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //AccountParameter
        public async Task<SchemeLoanAccountParameterViewModel> GetLoanAccountParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanAccountParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanAccountParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SchemeLoanAgainstDepositGeneralLedgerViewModel>> GetLoanAgainstDepositGeneralLedgerEntries(short _schemeLoanAgainstDepositParameterPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<SchemeLoanAgainstDepositGeneralLedgerViewModel>("SELECT * FROM dbo.GetSchemeLoanAgainstDepositGeneralLedgerEntryBySchemeLoanAgainstDepositParameterPrmKey (@SchemeLoanAgainstDepositParameterPrmKey, @EntryType)", new SqlParameter("@SchemeLoanAgainstDepositParameterPrmKey", _schemeLoanAgainstDepositParameterPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();

                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeLoanAgainstDepositParameterViewModel> GetLoanAgainstDepositParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeLoanAgainstDepositParameterViewModel schemeLoanAgainstDepositParameterViewModel = await context.Database.SqlQuery<SchemeLoanAgainstDepositParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanAgainstDepositParameterEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                return schemeLoanAgainstDepositParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeLoanAgreementNumberViewModel> GetLoanAgreementNumberEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeLoanAgreementNumberViewModel schemeLoanAgreementNumberViewModel = await context.Database.SqlQuery<SchemeLoanAgreementNumberViewModel>("SELECT * FROM dbo.GetSchemeLoanAgreementNumberEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                schemeLoanAgreementNumberViewModel.MaskTypeCharacterForAgreement = schemeLoanAgreementNumberViewModel.AgreementNumberMask.Split(',');

                return schemeLoanAgreementNumberViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //ApplicationParameter
        public async Task<SchemeLoanAgreementNumberViewModel> GetAgreementNumberEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemeLoanAgreementNumberViewModel schemeLoanAgreementNumberViewModel = await context.Database.SqlQuery<SchemeLoanAgreementNumberViewModel>("SELECT * FROM dbo.GetSchemeLoanAgreementNumberEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                if(schemeLoanAgreementNumberViewModel !=null)
                schemeLoanAgreementNumberViewModel.MaskTypeCharacterForAgreement = schemeLoanAgreementNumberViewModel.AgreementNumberMask.Split(',');

                return schemeLoanAgreementNumberViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Arrear
        public async Task<SchemeLoanArrearParameterViewModel> GetLoanArrearParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanArrearParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanArrearParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //LoanDistributor
        public async Task<SchemeLoanDistributorParameterViewModel> GetLoanDistributorParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanDistributorParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanDistributorParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeLoanFineInterestParameterViewModel> GetLoanFineInterestParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanFineInterestParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanFineInterestParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //LoanFunder
        public async Task<SchemeLoanFunderParameterViewModel> GetLoanFunderParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanFunderParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanFunderParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeLoanInstallmentParameterViewModel> GetLoanInstallmentParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanInstallmentParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanInstallmentParameterEntryBySchemePrmKey ( @SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //InterestParameter
        public async Task<SchemeLoanInterestParameterViewModel> GetLoanInterestParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanInterestParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanInterestParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeLoanInterestProvisionParameterViewModel> GetLoanInterestProvisionParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanInterestProvisionParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanInterestProvisionParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeLoanInterestRebateParameterViewModel> GetLoanInterestRebateParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanInterestRebateParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanInterestRebateParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeLoanPaymentReminderParameterViewModel> GetSchemeLoanPaymentReminderParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanPaymentReminderParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanPaymentReminderParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeLoanRepaymentScheduleParameterViewModel> GetLoanRepaymentScheduleParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanRepaymentScheduleParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanRepaymentScheduleParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeLoanSanctionAuthorityViewModel> GetLoanSanctionAuthorityEntry(short _schemePrmKey, string _entryType)

        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanSanctionAuthorityViewModel>("SELECT * FROM dbo.GetSchemeLoanSanctionAuthorityEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeLoanSettlementAccountParameterViewModel> GetLoanSettlementAccountParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanSettlementAccountParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanSettlementAccountParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Passbook
        public async Task<SchemePassbookViewModel> GetPassbookEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                SchemePassbookViewModel schemePassbookViewModel = await context.Database.SqlQuery<SchemePassbookViewModel>("SELECT * FROM dbo.GetSchemePassbookEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                schemePassbookViewModel.MaskTypeCharacterForPassbook = schemePassbookViewModel.PassbookNumberMask.Split(',');
                return schemePassbookViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
        public async Task<SchemeLoanPreFullPaymentParameterViewModel> GetPreFullPaymentParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanPreFullPaymentParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanPreFullPaymentParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeLoanPrePartPaymentParameterViewModel> GetPrePartPaymentParameterEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeLoanPrePartPaymentParameterViewModel>("SELECT * FROM dbo.GetSchemeLoanPrePartPaymentParameterEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeSharesCapitalAccountParameterViewModel> GetSharesCapitalAccountParameterEntry(short _SchemePrmKey, string _entryType)
        {
            try
            {
                SchemeSharesCapitalAccountParameterViewModel schemeSharesCapitalAccountParameterViewModel = await context.Database.SqlQuery<SchemeSharesCapitalAccountParameterViewModel>("SELECT * FROM dbo.GetSchemeSharesCapitalAccountParameterEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _SchemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                schemeSharesCapitalAccountParameterViewModel.MaskTypeCharacterForMember = schemeSharesCapitalAccountParameterViewModel.MemberNumberMask.Split(',');

                return schemeSharesCapitalAccountParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeSharesCapitalDividendParameterViewModel> GetSharesCapitalDividendParameterEntry(short _SchemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeSharesCapitalDividendParameterViewModel>("SELECT * FROM dbo.GetSchemeSharesCapitalDividendParameterEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _SchemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SchemeSharesCertificateParameterViewModel> GetSharesCertificateParameterEntry(short _SchemePrmKey, string _entryType)
        {
            try
            {
                SchemeSharesCertificateParameterViewModel schemeSharesCertificateParameterViewModel = await context.Database.SqlQuery<SchemeSharesCertificateParameterViewModel>("SELECT * FROM dbo.GetSchemeSharesCertificateParameterEntryBySchemePrmKey (@SchemePrmKey, @EntryType)", new SqlParameter("@SchemePrmKey", _SchemePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                schemeSharesCertificateParameterViewModel.MaskTypeCharacterForCertificate = schemeSharesCertificateParameterViewModel.CertificateNumberMask.Split(',');

                return schemeSharesCertificateParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Tenure
        public async Task<SchemeTenureViewModel> GetTenureEntry(short _schemePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SchemeTenureViewModel>("SELECT * FROM dbo.GetSchemeTenureEntryBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerLoanAccountOpeningConfigViewModel> GetCustomerLoanAccountConfigDetail(Guid _schemeId)
        {
            try
            {
                CustomerLoanAccountOpeningConfigViewModel customerLoanAccountOpeningConfigViewModel = new CustomerLoanAccountOpeningConfigViewModel
                {
                    SchemeAccountParameterViewModel = await GetAccountParameterEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemeApplicationParameterViewModel = await GetApplicationParameterEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemeLoanAccountParameterViewModel = await GetLoanAccountParameterEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemeTenureViewModel = await GetTenureEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemeCustomerAccountNumberViewModel = await GetCustomerAccountNumberEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemeLoanAgreementNumberViewModel = await GetLoanAgreementNumberEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemePassbookViewModel = await GetPassbookEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemeGoldLoanParameterViewModel = await GetGoldLoanParameterEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemeDocumentViewModel = await GetDocumentEntries(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemeLoanInterestParameterViewModel = await GetLoanInterestParameterEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemeVehicleTypeLoanParameterViewModel = await GetSchemeVehicleTypeLoanParameterEntries(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemePreownedVehicleLoanParameterViewModels = await GetSchemePreownedVehicleLoanParameterEntries(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemeCashCreditLoanParameterViewModel = await GetSchemeCashCreditLoanParameterEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemeEducationLoanParameterViewModel = await GetSchemeEducationLoanParameterEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemeBusinessLoanViewModel = await GetSchemeBusinessLoanEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify),
                    SchemePreownedVehicleLoanParameterViewModel = await GetPreownedVehicleLoanParameterEntry(accountDetailRepository.GetSchemePrmKeyById(_schemeId), StringLiteralValue.Verify)
                };

                return customerLoanAccountOpeningConfigViewModel;
                // return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }


        public async Task<SchemeDocumentViewModel> GetDocumentEntry(short _schemePrmKey, short _documentPrmKey, string _entryType)
        {
            try
            {
                SchemeDocumentViewModel schemeDocumentViewModel = await context.Database.SqlQuery<SchemeDocumentViewModel>("SELECT * FROM dbo.GetSchemeDocumentEntryByDocumentPrmKey (@SchemePrmKey, @DocumentPrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", _schemePrmKey), new SqlParameter("@DocumentPrmKey", _documentPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                if (schemeDocumentViewModel != null)
                {
                    schemeDocumentViewModel.DocumentAllowedFileFormatsForDbId = schemeDocumentViewModel.DocumentAllowedFileFormatsForDb.Split(',');
                    schemeDocumentViewModel.DocumentAllowedFileFormatsForLocalStorageId = schemeDocumentViewModel.DocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                return schemeDocumentViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

    }
}
