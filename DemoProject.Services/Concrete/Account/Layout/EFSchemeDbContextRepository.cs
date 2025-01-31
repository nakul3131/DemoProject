using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using DemoProject.Domain.Entities.Account.Layout;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Layout
{
    public class EFSchemeDbContextRepository : ISchemeDbContextRepository
    {
        private readonly EFDbContext context;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        private Scheme scheme = new Scheme();
        private short schemePrmKey = 0;
        private EntityState entityState;

        public EFSchemeDbContextRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IConfigurationDetailRepository _configurationDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, IManagementDetailRepository _managementDetailRepository, IPersonDetailRepository _personDetailRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            managementDetailRepository = _managementDetailRepository;
            personDetailRepository = _personDetailRepository;
        }

        public bool AttachSharesCapitalSchemeData(SharesCapitalSchemeViewModel _sharesCapitalSchemeViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_sharesCapitalSchemeViewModel, _entryType);

                Scheme scheme = Mapper.Map<Scheme>(_sharesCapitalSchemeViewModel);
                SchemeMakerChecker schemeMakerChecker = Mapper.Map<SchemeMakerChecker>(_sharesCapitalSchemeViewModel);

                SchemeTranslation schemeTranslation = Mapper.Map<SchemeTranslation>(_sharesCapitalSchemeViewModel);
                SchemeTranslationMakerChecker schemeTranslationMakerChecker = Mapper.Map<SchemeTranslationMakerChecker>(_sharesCapitalSchemeViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemePrmKey = _sharesCapitalSchemeViewModel.SchemePrmKey;
                    scheme.PrmKey = schemePrmKey;
                    scheme.SchemeTypePrmKey = 1;
                    schemeTranslation.PrmKey = _sharesCapitalSchemeViewModel.SchemeTranslationPrmKey;

                    context.Schemes.Attach(scheme);
                    context.Entry(scheme).State = entityState;

                    context.SchemeMakerCheckers.Attach(schemeMakerChecker);
                    context.Entry(schemeMakerChecker).State = EntityState.Added;
                    scheme.SchemeMakerCheckers.Add(schemeMakerChecker);

                    context.SchemeTranslations.Attach(schemeTranslation);
                    context.Entry(schemeTranslation).State = entityState;
                    scheme.SchemeTranslations.Add(schemeTranslation);

                    context.SchemeTranslationMakerCheckers.Attach(schemeTranslationMakerChecker);
                    context.Entry(schemeTranslationMakerChecker).State = EntityState.Added;
                    schemeTranslation.SchemeTranslationMakerCheckers.Add(schemeTranslationMakerChecker);
                }
                else
                {
                    context.SchemeMakerCheckers.Attach(schemeMakerChecker);
                    context.Entry(schemeMakerChecker).State = EntityState.Added;

                    context.SchemeTranslationMakerCheckers.Attach(schemeTranslationMakerChecker);
                    context.Entry(schemeTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeAccountParameterData(SchemeAccountParameterViewModel _schemeAccountParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeAccountParameterViewModel, _entryType);

                SchemeAccountParameter schemeAccountParameter = Mapper.Map<SchemeAccountParameter>(_schemeAccountParameterViewModel);
                SchemeAccountParameterMakerChecker schemeAccountParameterMakerChecker = Mapper.Map<SchemeAccountParameterMakerChecker>(_schemeAccountParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeAccountParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeAccountParameters.Attach(schemeAccountParameter);
                    context.Entry(schemeAccountParameter).State = entityState;
                    scheme.SchemeAccountParameters.Add(schemeAccountParameter);

                    context.SchemeAccountParameterMakerCheckers.Attach(schemeAccountParameterMakerChecker);
                    context.Entry(schemeAccountParameterMakerChecker).State = EntityState.Added;
                    schemeAccountParameter.SchemeAccountParameterMakerCheckers.Add(schemeAccountParameterMakerChecker);
                }
                else
                {
                    context.SchemeAccountParameterMakerCheckers.Attach(schemeAccountParameterMakerChecker);
                    context.Entry(schemeAccountParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeSharesCapitalAccountParameterData(SchemeSharesCapitalAccountParameterViewModel _schemeSharesCapitalAccountParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeSharesCapitalAccountParameterViewModel, _entryType);

                SchemeSharesCapitalAccountParameter schemeSharesCapitalAccountParameter = Mapper.Map<SchemeSharesCapitalAccountParameter>(_schemeSharesCapitalAccountParameterViewModel);
                SchemeSharesCapitalAccountParameterMakerChecker schemeSharesCapitalAccountParameterMakerChecker = Mapper.Map<SchemeSharesCapitalAccountParameterMakerChecker>(_schemeSharesCapitalAccountParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeSharesCapitalAccountParameter.SchemePrmKey = schemePrmKey;

                    if (_schemeSharesCapitalAccountParameterViewModel.EnableMemberNumberBranchwise == true || _schemeSharesCapitalAccountParameterViewModel.EnableAutoMemberNumber == false)
                        schemeSharesCapitalAccountParameter.MemberNumberMask = "None";
                    else
                        schemeSharesCapitalAccountParameter.MemberNumberMask = string.Join(",", _schemeSharesCapitalAccountParameterViewModel.MaskTypeCharacterForMember);

                    context.SchemeSharesCapitalAccountParameters.Attach(schemeSharesCapitalAccountParameter);
                    context.Entry(schemeSharesCapitalAccountParameter).State = entityState;
                    scheme.SchemeSharesCapitalAccountParameters.Add(schemeSharesCapitalAccountParameter);

                    context.SchemeSharesCapitalAccountParameterMakerCheckers.Attach(schemeSharesCapitalAccountParameterMakerChecker);
                    context.Entry(schemeSharesCapitalAccountParameterMakerChecker).State = EntityState.Added;
                    schemeSharesCapitalAccountParameter.SchemeSharesCapitalAccountParameterMakerCheckers.Add(schemeSharesCapitalAccountParameterMakerChecker);
                }
                else
                {
                    context.SchemeSharesCapitalAccountParameterMakerCheckers.Attach(schemeSharesCapitalAccountParameterMakerChecker);
                    context.Entry(schemeSharesCapitalAccountParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeCustomerAccountNumberData(SchemeCustomerAccountNumberViewModel _schemeCustomerAccountNumberViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeCustomerAccountNumberViewModel, _entryType);

                SchemeCustomerAccountNumber schemeCustomerAccountNumber = Mapper.Map<SchemeCustomerAccountNumber>(_schemeCustomerAccountNumberViewModel);
                SchemeCustomerAccountNumberMakerChecker schemeCustomerAccountNumberMakerChecker = Mapper.Map<SchemeCustomerAccountNumberMakerChecker>(_schemeCustomerAccountNumberViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeCustomerAccountNumber.SchemePrmKey = schemePrmKey;

                    if (_schemeCustomerAccountNumberViewModel.EnableAccountNumberBranchwise == true || _schemeCustomerAccountNumberViewModel.EnableAutoAccountNumber == false)
                        schemeCustomerAccountNumber.AccountNumberMask = "None";
                    else
                        schemeCustomerAccountNumber.AccountNumberMask = string.Join(",", _schemeCustomerAccountNumberViewModel.MaskTypeCharacterForAccount);

                    context.SchemeCustomerAccountNumbers.Attach(schemeCustomerAccountNumber);
                    context.Entry(schemeCustomerAccountNumber).State = entityState;
                    scheme.SchemeCustomerAccountNumbers.Add(schemeCustomerAccountNumber);

                    context.SchemeCustomerAccountNumberMakerCheckers.Attach(schemeCustomerAccountNumberMakerChecker);
                    context.Entry(schemeCustomerAccountNumberMakerChecker).State = EntityState.Added;
                    schemeCustomerAccountNumber.SchemeCustomerAccountNumberMakerCheckers.Add(schemeCustomerAccountNumberMakerChecker);
                }
                else
                {
                    context.SchemeCustomerAccountNumberMakerCheckers.Attach(schemeCustomerAccountNumberMakerChecker);
                    context.Entry(schemeCustomerAccountNumberMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeSharesCertificateParameterData(SchemeSharesCertificateParameterViewModel _schemeSharesCertificateParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeSharesCertificateParameterViewModel, _entryType);

                SchemeSharesCertificateParameter schemeSharesCertificateParameter = Mapper.Map<SchemeSharesCertificateParameter>(_schemeSharesCertificateParameterViewModel);
                SchemeSharesCertificateParameterMakerChecker schemeSharesCertificateParameterMakerChecker = Mapper.Map<SchemeSharesCertificateParameterMakerChecker>(_schemeSharesCertificateParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeSharesCertificateParameter.SchemePrmKey = schemePrmKey;

                    if (_schemeSharesCertificateParameterViewModel.EnableCertificateNumberBranchwise == true || _schemeSharesCertificateParameterViewModel.EnableAutoCertificateNumber == false)
                        schemeSharesCertificateParameter.CertificateNumberMask = "None";
                    else
                        schemeSharesCertificateParameter.CertificateNumberMask = string.Join(",", _schemeSharesCertificateParameterViewModel.MaskTypeCharacterForCertificate);

                    context.SchemeSharesCertificateParameters.Attach(schemeSharesCertificateParameter);
                    context.Entry(schemeSharesCertificateParameter).State = entityState;
                    scheme.SchemeSharesCertificateParameters.Add(schemeSharesCertificateParameter);

                    context.SchemeSharesCertificateParameterMakerCheckers.Attach(schemeSharesCertificateParameterMakerChecker);
                    context.Entry(schemeSharesCertificateParameterMakerChecker).State = EntityState.Added;
                    schemeSharesCertificateParameter.SchemeSharesCertificateParameterMakerCheckers.Add(schemeSharesCertificateParameterMakerChecker);
                }
                else
                {
                    context.SchemeSharesCertificateParameterMakerCheckers.Attach(schemeSharesCertificateParameterMakerChecker);
                    context.Entry(schemeSharesCertificateParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeApplicationParameterData(SchemeApplicationParameterViewModel _schemeApplicationParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeApplicationParameterViewModel, _entryType);

                SchemeApplicationParameter schemeApplicationParameter = Mapper.Map<SchemeApplicationParameter>(_schemeApplicationParameterViewModel);
                SchemeApplicationParameterMakerChecker schemeApplicationParameterMakerChecker = Mapper.Map<SchemeApplicationParameterMakerChecker>(_schemeApplicationParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeApplicationParameter.SchemePrmKey = schemePrmKey;

                    if (_schemeApplicationParameterViewModel.EnableAutoApplicationNumber == false)
                        schemeApplicationParameter.ApplicationNumberMask = "None";
                    else
                        schemeApplicationParameter.ApplicationNumberMask = string.Join(",", _schemeApplicationParameterViewModel.MaskTypeCharacterForApplication);

                    context.SchemeApplicationParameters.Attach(schemeApplicationParameter);
                    context.Entry(schemeApplicationParameter).State = entityState;
                    scheme.SchemeApplicationParameters.Add(schemeApplicationParameter);

                    context.SchemeApplicationParameterMakerCheckers.Attach(schemeApplicationParameterMakerChecker);
                    context.Entry(schemeApplicationParameterMakerChecker).State = EntityState.Added;
                    schemeApplicationParameter.SchemeApplicationParameterMakerCheckers.Add(schemeApplicationParameterMakerChecker);
                }
                else
                {
                    context.SchemeApplicationParameterMakerCheckers.Attach(schemeApplicationParameterMakerChecker);
                    context.Entry(schemeApplicationParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeAccountBankingChannelParameterData(SchemeAccountBankingChannelParameterViewModel _schemeAccountBankingChannelParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeAccountBankingChannelParameterViewModel, _entryType);

                SchemeAccountBankingChannelParameter schemeAccountBankingChannelParameter = Mapper.Map<SchemeAccountBankingChannelParameter>(_schemeAccountBankingChannelParameterViewModel);
                SchemeAccountBankingChannelParameterMakerChecker schemeAccountBankingChannelParameterMakerChecker = Mapper.Map<SchemeAccountBankingChannelParameterMakerChecker>(_schemeAccountBankingChannelParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeAccountBankingChannelParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeAccountBankingChannelParameters.Attach(schemeAccountBankingChannelParameter);
                    context.Entry(schemeAccountBankingChannelParameter).State = entityState;
                    scheme.SchemeAccountBankingChannelParameters.Add(schemeAccountBankingChannelParameter);

                    context.SchemeAccountBankingChannelParameterMakerCheckers.Attach(schemeAccountBankingChannelParameterMakerChecker);
                    context.Entry(schemeAccountBankingChannelParameterMakerChecker).State = EntityState.Added;
                    schemeAccountBankingChannelParameter.SchemeAccountBankingChannelParameterMakerCheckers.Add(schemeAccountBankingChannelParameterMakerChecker);
                }
                else
                {
                    context.SchemeAccountBankingChannelParameterMakerCheckers.Attach(schemeAccountBankingChannelParameterMakerChecker);
                    context.Entry(schemeAccountBankingChannelParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeSharesCapitalDividendParameterData(SchemeSharesCapitalDividendParameterViewModel _schemeSharesCapitalDividendParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeSharesCapitalDividendParameterViewModel, _entryType);

                SchemeSharesCapitalDividendParameter schemeSharesCapitalDividendParameter = Mapper.Map<SchemeSharesCapitalDividendParameter>(_schemeSharesCapitalDividendParameterViewModel);
                SchemeSharesCapitalDividendParameterMakerChecker schemeSharesCapitalDividendParameterMakerChecker = Mapper.Map<SchemeSharesCapitalDividendParameterMakerChecker>(_schemeSharesCapitalDividendParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    schemeSharesCapitalDividendParameter.SchemePrmKey = schemePrmKey;
                    schemeSharesCapitalDividendParameter.DividendCalculationMethodPrmKey = accountDetailRepository.GetDividendCalculationMethodPrmKeyById(_schemeSharesCapitalDividendParameterViewModel.DividendCalculationMethodId);

                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    context.SchemeSharesCapitalDividendParameters.Attach(schemeSharesCapitalDividendParameter);
                    context.Entry(schemeSharesCapitalDividendParameter).State = entityState;
                    scheme.SchemeSharesCapitalDividendParameters.Add(schemeSharesCapitalDividendParameter);

                    context.SchemeSharesCapitalDividendParameterMakerCheckers.Attach(schemeSharesCapitalDividendParameterMakerChecker);
                    context.Entry(schemeSharesCapitalDividendParameterMakerChecker).State = EntityState.Added;
                    schemeSharesCapitalDividendParameter.SchemeSharesCapitalDividendParameterMakerCheckers.Add(schemeSharesCapitalDividendParameterMakerChecker);
                }
                else
                {
                    context.SchemeSharesCapitalDividendParameterMakerCheckers.Attach(schemeSharesCapitalDividendParameterMakerChecker);
                    context.Entry(schemeSharesCapitalDividendParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeClosingChargesData(SchemeClosingChargesViewModel _schemeClosingChargesViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeClosingChargesViewModel, _entryType);

                SchemeClosingCharges schemeClosingCharges = Mapper.Map<SchemeClosingCharges>(_schemeClosingChargesViewModel);
                SchemeClosingChargesMakerChecker schemeClosingChargesMakerChecker = Mapper.Map<SchemeClosingChargesMakerChecker>(_schemeClosingChargesViewModel);

                //Get PrmKey By Id
                schemeClosingCharges.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_schemeClosingChargesViewModel.GeneralLedgerId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeClosingCharges.SchemePrmKey = schemePrmKey;

                    context.SchemeClosingCharges.Attach(schemeClosingCharges);
                    context.Entry(schemeClosingCharges).State = EntityState.Added;
                    scheme.SchemeClosingCharges.Add(schemeClosingCharges);

                    context.SchemeClosingChargesMakerCheckers.Attach(schemeClosingChargesMakerChecker);
                    context.Entry(schemeClosingChargesMakerChecker).State = EntityState.Added;
                    schemeClosingCharges.SchemeClosingChargesMakerCheckers.Add(schemeClosingChargesMakerChecker);
                }
                else
                {
                    context.SchemeClosingChargesMakerCheckers.Attach(schemeClosingChargesMakerChecker);
                    context.Entry(schemeClosingChargesMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeSharesTransferChargesData(SchemeSharesTransferChargesViewModel _schemeSharesTransferChargesViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeSharesTransferChargesViewModel, _entryType);

                SchemeSharesTransferCharges schemeSharesTransferCharges = Mapper.Map<SchemeSharesTransferCharges>(_schemeSharesTransferChargesViewModel);
                SchemeSharesTransferChargesMakerChecker schemeSharesTransferChargesMakerChecker = Mapper.Map<SchemeSharesTransferChargesMakerChecker>(_schemeSharesTransferChargesViewModel);

                //Get PrmKey By Id
                schemeSharesTransferCharges.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_schemeSharesTransferChargesViewModel.GeneralLedgerId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeSharesTransferCharges.SchemePrmKey = schemePrmKey;

                    context.SchemeSharesTransferCharges.Attach(schemeSharesTransferCharges);
                    context.Entry(schemeSharesTransferCharges).State = EntityState.Added;
                    scheme.SchemeSharesTransferCharges.Add(schemeSharesTransferCharges);

                    context.SchemeSharesTransferChargesMakerCheckers.Attach(schemeSharesTransferChargesMakerChecker);
                    context.Entry(schemeSharesTransferChargesMakerChecker).State = EntityState.Added;
                    schemeSharesTransferCharges.SchemeSharesTransferChargesMakerCheckers.Add(schemeSharesTransferChargesMakerChecker);
                }
                else
                {
                    context.SchemeSharesTransferChargesMakerCheckers.Attach(schemeSharesTransferChargesMakerChecker);
                    context.Entry(schemeSharesTransferChargesMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeNoticeScheduleData(SchemeNoticeScheduleViewModel _schemeNoticeScheduleViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeNoticeScheduleViewModel, _entryType);

                SchemeNoticeSchedule schemeNoticeSchedule = Mapper.Map<SchemeNoticeSchedule>(_schemeNoticeScheduleViewModel);
                SchemeNoticeScheduleMakerChecker schemeNoticeScheduleMakerChecker = Mapper.Map<SchemeNoticeScheduleMakerChecker>(_schemeNoticeScheduleViewModel);

                //Get PrmKey By Id
                schemeNoticeSchedule.SchedulePrmKey = enterpriseDetailRepository.GetSchedulePrmKeyById(_schemeNoticeScheduleViewModel.ScheduleId);
                schemeNoticeSchedule.CommunicationMediaPrmKey = managementDetailRepository.GetCommunicationMediaPrmKeyById(_schemeNoticeScheduleViewModel.CommunicationMediaId);
                schemeNoticeSchedule.NoticeTypePrmKey = managementDetailRepository.GetNoticeTypePrmKeyById(_schemeNoticeScheduleViewModel.NoticeTypeId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeNoticeSchedule.SchemePrmKey = schemePrmKey;

                    context.SchemeNoticeSchedules.Attach(schemeNoticeSchedule);
                    context.Entry(schemeNoticeSchedule).State = EntityState.Added;
                    scheme.SchemeNoticeSchedules.Add(schemeNoticeSchedule);

                    context.SchemeNoticeScheduleMakerCheckers.Attach(schemeNoticeScheduleMakerChecker);
                    context.Entry(schemeNoticeScheduleMakerChecker).State = EntityState.Added;
                    schemeNoticeSchedule.SchemeNoticeScheduleMakerCheckers.Add(schemeNoticeScheduleMakerChecker);
                }
                else
                {
                    context.SchemeNoticeScheduleMakerCheckers.Attach(schemeNoticeScheduleMakerChecker);
                    context.Entry(schemeNoticeScheduleMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeReportFormatData(SchemeReportFormatViewModel _schemeReportFormatViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeReportFormatViewModel, _entryType);

                SchemeReportFormat schemeReportFormat = Mapper.Map<SchemeReportFormat>(_schemeReportFormatViewModel);
                SchemeReportFormatMakerChecker schemeReportFormatMakerChecker = Mapper.Map<SchemeReportFormatMakerChecker>(_schemeReportFormatViewModel);

                //Get PrmKey By Id
                schemeReportFormat.ReportTypeFormatPrmKey = configurationDetailRepository.GetReportTypeFormatPrmKeyById(_schemeReportFormatViewModel.ReportTypeFormatId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeReportFormat.SchemePrmKey = schemePrmKey;

                    context.SchemeReportFormats.Attach(schemeReportFormat);
                    context.Entry(schemeReportFormat).State = EntityState.Added;
                    scheme.SchemeReportFormats.Add(schemeReportFormat);

                    context.SchemeReportFormatMakerCheckers.Attach(schemeReportFormatMakerChecker);
                    context.Entry(schemeReportFormatMakerChecker).State = EntityState.Added;
                    schemeReportFormat.SchemeReportFormatMakerCheckers.Add(schemeReportFormatMakerChecker);
                }
                else
                {
                    context.SchemeReportFormatMakerCheckers.Attach(schemeReportFormatMakerChecker);
                    context.Entry(schemeReportFormatMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeEstimateTargetData(SchemeEstimateTargetViewModel _schemeEstimateTargetViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeEstimateTargetViewModel, _entryType);

                SchemeEstimateTarget schemeEstimateTarget = Mapper.Map<SchemeEstimateTarget>(_schemeEstimateTargetViewModel);
                SchemeEstimateTargetMakerChecker schemeEstimateTargetMakerChecker = Mapper.Map<SchemeEstimateTargetMakerChecker>(_schemeEstimateTargetViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    schemeEstimateTarget.SchemePrmKey = schemePrmKey;
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    context.SchemeEstimateTargets.Attach(schemeEstimateTarget);
                    context.Entry(schemeEstimateTarget).State = entityState;
                    scheme.SchemeEstimateTargets.Add(schemeEstimateTarget);

                    context.SchemeEstimateTargetMakerCheckers.Attach(schemeEstimateTargetMakerChecker);
                    context.Entry(schemeEstimateTargetMakerChecker).State = EntityState.Added;
                    schemeEstimateTarget.SchemeEstimateTargetMakerCheckers.Add(schemeEstimateTargetMakerChecker);
                }
                else
                {
                    context.SchemeEstimateTargetMakerCheckers.Attach(schemeEstimateTargetMakerChecker);
                    context.Entry(schemeEstimateTargetMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLimitData(SchemeLimitViewModel _schemeLimitViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLimitViewModel, _entryType);

                SchemeLimit schemeLimit = Mapper.Map<SchemeLimit>(_schemeLimitViewModel);
                SchemeLimitMakerChecker schemeLimitMakerChecker = Mapper.Map<SchemeLimitMakerChecker>(_schemeLimitViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    schemeLimit.SchemePrmKey = schemePrmKey;
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    context.SchemeLimits.Attach(schemeLimit);
                    context.Entry(schemeLimit).State = entityState;
                    scheme.SchemeLimits.Add(schemeLimit);

                    context.SchemeLimitMakerCheckers.Attach(schemeLimitMakerChecker);
                    context.Entry(schemeLimitMakerChecker).State = EntityState.Added;
                    schemeLimit.SchemeLimitMakerCheckers.Add(schemeLimitMakerChecker);
                }
                else
                {
                    context.SchemeLimitMakerCheckers.Attach(schemeLimitMakerChecker);
                    context.Entry(schemeLimitMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeGeneralLedgerData(SchemeGeneralLedgerViewModel _schemeGeneralLedgerViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeGeneralLedgerViewModel, _entryType);

                SchemeGeneralLedger schemeGeneralLedger = Mapper.Map<SchemeGeneralLedger>(_schemeGeneralLedgerViewModel);
                SchemeGeneralLedgerMakerChecker schemeGeneralLedgerMakerChecker = Mapper.Map<SchemeGeneralLedgerMakerChecker>(_schemeGeneralLedgerViewModel);

                //Get PrmKey By Id
                schemeGeneralLedger.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_schemeGeneralLedgerViewModel.GeneralLedgerId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeGeneralLedger.SchemePrmKey = schemePrmKey;

                    context.SchemeGeneralLedgers.Attach(schemeGeneralLedger);
                    context.Entry(schemeGeneralLedger).State = EntityState.Added;
                    scheme.SchemeGeneralLedgers.Add(schemeGeneralLedger);

                    context.SchemeGeneralLedgerMakerCheckers.Attach(schemeGeneralLedgerMakerChecker);
                    context.Entry(schemeGeneralLedgerMakerChecker).State = EntityState.Added;
                    schemeGeneralLedger.SchemeGeneralLedgerMakerCheckers.Add(schemeGeneralLedgerMakerChecker);
                }
                else
                {
                    context.SchemeGeneralLedgerMakerCheckers.Attach(schemeGeneralLedgerMakerChecker);
                    context.Entry(schemeGeneralLedgerMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeBusinessOfficeData(SchemeBusinessOfficeViewModel _schemeBusinessOfficeViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeBusinessOfficeViewModel, _entryType);

                SchemeBusinessOffice schemeBusinessOffice = Mapper.Map<SchemeBusinessOffice>(_schemeBusinessOfficeViewModel);
                SchemeBusinessOfficeMakerChecker schemeBusinessOfficeMakerChecker = Mapper.Map<SchemeBusinessOfficeMakerChecker>(_schemeBusinessOfficeViewModel);

                //Get PrmKey By Id
                schemeBusinessOffice.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_schemeBusinessOfficeViewModel.BusinessOfficeId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeBusinessOffice.SchemePrmKey = schemePrmKey;

                    context.SchemeBusinessOffices.Attach(schemeBusinessOffice);
                    context.Entry(schemeBusinessOffice).State = EntityState.Added;
                    scheme.SchemeBusinessOffices.Add(schemeBusinessOffice);

                    context.SchemeBusinessOfficeMakerCheckers.Attach(schemeBusinessOfficeMakerChecker);
                    context.Entry(schemeBusinessOfficeMakerChecker).State = EntityState.Added;
                    schemeBusinessOffice.SchemeBusinessOfficeMakerCheckers.Add(schemeBusinessOfficeMakerChecker);
                }
                else
                {
                    context.SchemeBusinessOfficeMakerCheckers.Attach(schemeBusinessOfficeMakerChecker);
                    context.Entry(schemeBusinessOfficeMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        // Deposit Scheme
        public bool AttachDepositSchemeData(DepositSchemeViewModel _depositSchemeViewModel, string _entryType)
        {
            try
            {

                configurationDetailRepository.SetDefaultValues(_depositSchemeViewModel, _entryType);

                Scheme scheme = Mapper.Map<Scheme>(_depositSchemeViewModel);
                SchemeMakerChecker schemeMakerChecker = Mapper.Map<SchemeMakerChecker>(_depositSchemeViewModel);

                SchemeTranslation schemeTranslation = Mapper.Map<SchemeTranslation>(_depositSchemeViewModel);
                SchemeTranslationMakerChecker schemeTranslationMakerChecker = Mapper.Map<SchemeTranslationMakerChecker>(_depositSchemeViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemePrmKey = _depositSchemeViewModel.SchemePrmKey;
                    scheme.PrmKey = schemePrmKey;
                    scheme.SchemeTypePrmKey = 3;
                    schemeTranslation.PrmKey = _depositSchemeViewModel.SchemeTranslationPrmKey;

                    context.Schemes.Attach(scheme);
                    context.Entry(scheme).State = entityState;

                    context.SchemeMakerCheckers.Attach(schemeMakerChecker);
                    context.Entry(schemeMakerChecker).State = EntityState.Added;
                    scheme.SchemeMakerCheckers.Add(schemeMakerChecker);

                    context.SchemeTranslations.Attach(schemeTranslation);
                    context.Entry(schemeTranslation).State = entityState;
                    scheme.SchemeTranslations.Add(schemeTranslation);

                    context.SchemeTranslationMakerCheckers.Attach(schemeTranslationMakerChecker);
                    context.Entry(schemeTranslationMakerChecker).State = EntityState.Added;
                    schemeTranslation.SchemeTranslationMakerCheckers.Add(schemeTranslationMakerChecker);
                }
                else
                {
                    context.SchemeMakerCheckers.Attach(schemeMakerChecker);
                    context.Entry(schemeMakerChecker).State = EntityState.Added;

                    context.SchemeTranslationMakerCheckers.Attach(schemeTranslationMakerChecker);
                    context.Entry(schemeTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeDepositAccountParameterData(SchemeDepositAccountParameterViewModel _schemeDepositAccountParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeDepositAccountParameterViewModel, _entryType);

                SchemeDepositAccountParameter schemeDepositAccountParameter = Mapper.Map<SchemeDepositAccountParameter>(_schemeDepositAccountParameterViewModel);
                SchemeDepositAccountParameterMakerChecker schemeDepositAccountParameterMakerChecker = Mapper.Map<SchemeDepositAccountParameterMakerChecker>(_schemeDepositAccountParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeDepositAccountParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeDepositAccountParameters.Attach(schemeDepositAccountParameter);
                    context.Entry(schemeDepositAccountParameter).State = entityState;
                    scheme.SchemeDepositAccountParameters.Add(schemeDepositAccountParameter);

                    context.SchemeDepositAccountParameterMakerCheckers.Attach(schemeDepositAccountParameterMakerChecker);
                    context.Entry(schemeDepositAccountParameterMakerChecker).State = EntityState.Added;
                    schemeDepositAccountParameter.SchemeDepositAccountParameterMakerCheckers.Add(schemeDepositAccountParameterMakerChecker);
                }
                else
                {
                    context.SchemeDepositAccountParameterMakerCheckers.Attach(schemeDepositAccountParameterMakerChecker);
                    context.Entry(schemeDepositAccountParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeDepositInstallmentParameterData(SchemeDepositInstallmentParameterViewModel _schemeDepositInstallmentParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeDepositInstallmentParameterViewModel, _entryType);

                SchemeDepositInstallmentParameter schemeDepositInstallmentParameter = Mapper.Map<SchemeDepositInstallmentParameter>(_schemeDepositInstallmentParameterViewModel);
                SchemeDepositInstallmentParameterMakerChecker schemeDepositInstallmentParameterMakerChecker = Mapper.Map<SchemeDepositInstallmentParameterMakerChecker>(_schemeDepositInstallmentParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeDepositInstallmentParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeDepositInstallmentParameters.Attach(schemeDepositInstallmentParameter);
                    context.Entry(schemeDepositInstallmentParameter).State = entityState;
                    scheme.SchemeDepositInstallmentParameters.Add(schemeDepositInstallmentParameter);

                    context.SchemeDepositInstallmentParameterMakerCheckers.Attach(schemeDepositInstallmentParameterMakerChecker);
                    context.Entry(schemeDepositInstallmentParameterMakerChecker).State = EntityState.Added;
                    schemeDepositInstallmentParameter.SchemeDepositInstallmentParameterMakerCheckers.Add(schemeDepositInstallmentParameterMakerChecker);
                }
                else
                {
                    context.SchemeDepositInstallmentParameterMakerCheckers.Attach(schemeDepositInstallmentParameterMakerChecker);
                    context.Entry(schemeDepositInstallmentParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeDepositAgentParameterData(SchemeDepositAgentParameterViewModel _schemeDepositAgentParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeDepositAgentParameterViewModel, _entryType);

                SchemeDepositAgentParameter schemeDepositAgentParameter = Mapper.Map<SchemeDepositAgentParameter>(_schemeDepositAgentParameterViewModel);
                SchemeDepositAgentParameterMakerChecker schemeDepositAgentParameterMakerChecker = Mapper.Map<SchemeDepositAgentParameterMakerChecker>(_schemeDepositAgentParameterViewModel);

                // Get PrmKey By Id
                schemeDepositAgentParameter.AgentCommissionGeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_schemeDepositAgentParameterViewModel.AgentCommissionGeneralLedgerId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeDepositAgentParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeDepositAgentParameters.Attach(schemeDepositAgentParameter);
                    context.Entry(schemeDepositAgentParameter).State = entityState;
                    scheme.SchemeDepositAgentParameters.Add(schemeDepositAgentParameter);

                    context.SchemeDepositAgentParameterMakerCheckers.Attach(schemeDepositAgentParameterMakerChecker);
                    context.Entry(schemeDepositAgentParameterMakerChecker).State = EntityState.Added;
                    schemeDepositAgentParameter.SchemeDepositAgentParameterMakerCheckers.Add(schemeDepositAgentParameterMakerChecker);
                }
                else
                {
                    context.SchemeDepositAgentParameterMakerCheckers.Attach(schemeDepositAgentParameterMakerChecker);
                    context.Entry(schemeDepositAgentParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeDepositAgentIncentiveData(SchemeDepositAgentIncentiveViewModel _schemeDepositAgentIncentiveViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeDepositAgentIncentiveViewModel, _entryType);

                SchemeDepositAgentIncentive schemeDepositAgentIncentive = Mapper.Map<SchemeDepositAgentIncentive>(_schemeDepositAgentIncentiveViewModel);
                SchemeDepositAgentIncentiveMakerChecker schemeDepositAgentIncentiveMakerChecker = Mapper.Map<SchemeDepositAgentIncentiveMakerChecker>(_schemeDepositAgentIncentiveViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeDepositAgentIncentive.SchemePrmKey = schemePrmKey;

                    context.SchemeDepositAgentIncentives.Attach(schemeDepositAgentIncentive);
                    context.Entry(schemeDepositAgentIncentive).State = EntityState.Added;
                    scheme.SchemeDepositAgentIncentives.Add(schemeDepositAgentIncentive);

                    context.SchemeDepositAgentIncentiveMakerCheckers.Attach(schemeDepositAgentIncentiveMakerChecker);
                    context.Entry(schemeDepositAgentIncentiveMakerChecker).State = EntityState.Added;
                    schemeDepositAgentIncentive.SchemeDepositAgentIncentiveMakerCheckers.Add(schemeDepositAgentIncentiveMakerChecker);
                }
                else
                {
                    context.SchemeDepositAgentIncentiveMakerCheckers.Attach(schemeDepositAgentIncentiveMakerChecker);
                    context.Entry(schemeDepositAgentIncentiveMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeDepositInterestParameterData(SchemeDepositInterestParameterViewModel _schemeDepositInterestParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeDepositInterestParameterViewModel, _entryType);

                SchemeDepositInterestParameter schemeDepositInterestParameter = Mapper.Map<SchemeDepositInterestParameter>(_schemeDepositInterestParameterViewModel);
                SchemeDepositInterestParameterMakerChecker schemeDepositInterestParameterMakerChecker = Mapper.Map<SchemeDepositInterestParameterMakerChecker>(_schemeDepositInterestParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    // Get PrmKey By Id
                    schemeDepositInterestParameter.InterestMethodPrmKey = accountDetailRepository.GetInterestMethodPrmKeyById(_schemeDepositInterestParameterViewModel.InterestMethodId);
                    schemeDepositInterestParameter.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_schemeDepositInterestParameterViewModel.GeneralLedgerId);
                    schemeDepositInterestParameter.InterestRateChargedDurationPrmKey = accountDetailRepository.GetInterestRateChargedDurationPrmKeyById(_schemeDepositInterestParameterViewModel.InterestRateChargedDurationId);

                    // If Interest Payout Day Is Customise Then Get Value Which Inputed In InterestPayoutDayOther
                    if (schemeDepositInterestParameter.InterestPayoutDay == "CST")
                        schemeDepositInterestParameter.InterestPayoutDay = _schemeDepositInterestParameterViewModel.InterestPayoutDayOther.ToString();

                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeDepositInterestParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeDepositInterestParameters.Attach(schemeDepositInterestParameter);
                    context.Entry(schemeDepositInterestParameter).State = entityState;
                    scheme.SchemeDepositInterestParameters.Add(schemeDepositInterestParameter);

                    context.SchemeDepositInterestParameterMakerCheckers.Attach(schemeDepositInterestParameterMakerChecker);
                    context.Entry(schemeDepositInterestParameterMakerChecker).State = EntityState.Added;
                    schemeDepositInterestParameter.SchemeDepositInterestParameterMakerCheckers.Add(schemeDepositInterestParameterMakerChecker);
                }
                else
                {
                    context.SchemeDepositInterestParameterMakerCheckers.Attach(schemeDepositInterestParameterMakerChecker);
                    context.Entry(schemeDepositInterestParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeDepositInterestProvisionParameterData(SchemeDepositInterestProvisionParameterViewModel _schemeDepositInterestProvisionParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeDepositInterestProvisionParameterViewModel, _entryType);

                SchemeDepositInterestProvisionParameter schemeDepositInterestProvisionParameter = Mapper.Map<SchemeDepositInterestProvisionParameter>(_schemeDepositInterestProvisionParameterViewModel);
                SchemeDepositInterestProvisionParameterMakerChecker schemeDepositInterestProvisionParameterMakerChecker = Mapper.Map<SchemeDepositInterestProvisionParameterMakerChecker>(_schemeDepositInterestProvisionParameterViewModel);

                //Get PrmKey By Id
                schemeDepositInterestProvisionParameter.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_schemeDepositInterestProvisionParameterViewModel.GeneralLedgerId);
                schemeDepositInterestProvisionParameter.InterestCalculationFrequencyPrmKey = accountDetailRepository.GetInterestCalculationFrequencyPrmKeyById(_schemeDepositInterestProvisionParameterViewModel.InterestCalculationFrequencyId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeDepositInterestProvisionParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeDepositInterestProvisionParameters.Attach(schemeDepositInterestProvisionParameter);
                    context.Entry(schemeDepositInterestProvisionParameter).State = entityState;
                    scheme.SchemeDepositInterestProvisionParameters.Add(schemeDepositInterestProvisionParameter);

                    context.SchemeDepositInterestProvisionParameterMakerCheckers.Attach(schemeDepositInterestProvisionParameterMakerChecker);
                    context.Entry(schemeDepositInterestProvisionParameterMakerChecker).State = EntityState.Added;
                    schemeDepositInterestProvisionParameter.SchemeDepositInterestProvisionParameterMakerCheckers.Add(schemeDepositInterestProvisionParameterMakerChecker);
                }
                else
                {
                    context.SchemeDepositInterestProvisionParameterMakerCheckers.Attach(schemeDepositInterestProvisionParameterMakerChecker);
                    context.Entry(schemeDepositInterestProvisionParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeNumberOfTransactionLimitData(SchemeNumberOfTransactionLimitViewModel _schemeNumberOfTransactionLimitViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeNumberOfTransactionLimitViewModel, _entryType);

                SchemeNumberOfTransactionLimit schemeNumberOfTransactionLimit = Mapper.Map<SchemeNumberOfTransactionLimit>(_schemeNumberOfTransactionLimitViewModel);
                SchemeNumberOfTransactionLimitMakerChecker schemeNumberOfTransactionLimitMakerChecker = Mapper.Map<SchemeNumberOfTransactionLimitMakerChecker>(_schemeNumberOfTransactionLimitViewModel);

                //Get PrmKey By Id
                schemeNumberOfTransactionLimit.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(_schemeNumberOfTransactionLimitViewModel.TransactionTypeId);
                schemeNumberOfTransactionLimit.TimePeriodUnitPrmKey = configurationDetailRepository.GetTimePeriodUnitPrmKeyById(_schemeNumberOfTransactionLimitViewModel.TimePeriodUnitId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeNumberOfTransactionLimit.SchemePrmKey = schemePrmKey;

                    context.SchemeNumberOfTransactionLimits.Attach(schemeNumberOfTransactionLimit);
                    context.Entry(schemeNumberOfTransactionLimit).State = EntityState.Added;
                    scheme.SchemeNumberOfTransactionLimits.Add(schemeNumberOfTransactionLimit);

                    context.SchemeNumberOfTransactionLimitMakerCheckers.Attach(schemeNumberOfTransactionLimitMakerChecker);
                    context.Entry(schemeNumberOfTransactionLimitMakerChecker).State = EntityState.Added;
                    schemeNumberOfTransactionLimit.SchemeNumberOfTransactionLimitMakerCheckers.Add(schemeNumberOfTransactionLimitMakerChecker);
                }
                else
                {
                    context.SchemeNumberOfTransactionLimitMakerCheckers.Attach(schemeNumberOfTransactionLimitMakerChecker);
                    context.Entry(schemeNumberOfTransactionLimitMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeTransactionAmountLimitData(SchemeTransactionAmountLimitViewModel _schemeTransactionAmountLimitViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeTransactionAmountLimitViewModel, _entryType);

                SchemeTransactionAmountLimit schemeTransactionAmountLimit = Mapper.Map<SchemeTransactionAmountLimit>(_schemeTransactionAmountLimitViewModel);
                SchemeTransactionAmountLimitMakerChecker schemeTransactionAmountLimitMakerChecker = Mapper.Map<SchemeTransactionAmountLimitMakerChecker>(_schemeTransactionAmountLimitViewModel);

                //Get PrmKey By Id
                schemeTransactionAmountLimit.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(_schemeTransactionAmountLimitViewModel.TransactionTypeId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeTransactionAmountLimit.SchemePrmKey = schemePrmKey;

                    context.SchemeTransactionAmountLimits.Attach(schemeTransactionAmountLimit);
                    context.Entry(schemeTransactionAmountLimit).State = EntityState.Added;
                    scheme.SchemeTransactionAmountLimits.Add(schemeTransactionAmountLimit);

                    context.SchemeTransactionAmountLimitMakerCheckers.Attach(schemeTransactionAmountLimitMakerChecker);
                    context.Entry(schemeTransactionAmountLimitMakerChecker).State = EntityState.Added;
                    schemeTransactionAmountLimit.SchemeTransactionAmountLimitMakerCheckers.Add(schemeTransactionAmountLimitMakerChecker);
                }
                else
                {
                    context.SchemeTransactionAmountLimitMakerCheckers.Attach(schemeTransactionAmountLimitMakerChecker);
                    context.Entry(schemeTransactionAmountLimitMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeDepositCertificateParameterData(SchemeDepositCertificateParameterViewModel _schemeDepositCertificateParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeDepositCertificateParameterViewModel, _entryType);

                SchemeDepositCertificateParameter schemeDepositCertificateParameter = Mapper.Map<SchemeDepositCertificateParameter>(_schemeDepositCertificateParameterViewModel);
                SchemeDepositCertificateParameterMakerChecker schemeDepositCertificateParameterMakerChecker = Mapper.Map<SchemeDepositCertificateParameterMakerChecker>(_schemeDepositCertificateParameterViewModel);

                // Multi Select Value For Dropdown               
                if (_schemeDepositCertificateParameterViewModel.EnableAutoCertificateNumber == false)
                {
                    schemeDepositCertificateParameter.CertificateNumberMask = "None";
                }
                else
                {
                    schemeDepositCertificateParameter.CertificateNumberMask = string.Join(",", _schemeDepositCertificateParameterViewModel.MaskTypeCharacterForCertificate);
                }

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeDepositCertificateParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeDepositCertificateParameters.Attach(schemeDepositCertificateParameter);
                    context.Entry(schemeDepositCertificateParameter).State = entityState;
                    scheme.SchemeDepositCertificateParameters.Add(schemeDepositCertificateParameter);

                    context.SchemeDepositCertificateParameterMakerCheckers.Attach(schemeDepositCertificateParameterMakerChecker);
                    context.Entry(schemeDepositCertificateParameterMakerChecker).State = EntityState.Added;
                    schemeDepositCertificateParameter.SchemeDepositCertificateParameterMakerCheckers.Add(schemeDepositCertificateParameterMakerChecker);
                }
                else
                {
                    context.SchemeDepositCertificateParameterMakerCheckers.Attach(schemeDepositCertificateParameterMakerChecker);
                    context.Entry(schemeDepositCertificateParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeDemandDepositDetailData(SchemeDemandDepositDetailViewModel _schemeDemandDepositDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeDemandDepositDetailViewModel, _entryType);

                SchemeDemandDepositDetail schemeDemandDepositDetail = Mapper.Map<SchemeDemandDepositDetail>(_schemeDemandDepositDetailViewModel);
                SchemeDemandDepositDetailMakerChecker schemeDemandDepositDetailMakerChecker = Mapper.Map<SchemeDemandDepositDetailMakerChecker>(_schemeDemandDepositDetailViewModel);

                // Get PrmKey By ID
                schemeDemandDepositDetail.BalanceTypePrmKey = accountDetailRepository.GetBalanceTypePrmKeyById(_schemeDemandDepositDetailViewModel.BalanceTypeId);
                schemeDemandDepositDetail.TimePeriodUnitPrmKey = configurationDetailRepository.GetTimePeriodUnitPrmKeyById(_schemeDemandDepositDetailViewModel.TimePeriodUnitId);

                if (_schemeDemandDepositDetailViewModel.EnableSweepOut == false)
                {
                    schemeDemandDepositDetail.SweepOutFrequencyPrmKey = 5;
                }
                else
                {
                    schemeDemandDepositDetail.SweepOutFrequencyPrmKey = accountDetailRepository.GetSweepOutFrequencyPrmKeyById(_schemeDemandDepositDetailViewModel.SweepOutFrequencyId);
                }

                // If EnablePhotoSign False then Set Default Values 
                if (_schemeDemandDepositDetailViewModel.EnablePhotoSign == false)
                {
                    // Photo Document Upload
                    schemeDemandDepositDetail.PhotoDocumentUpload = "N";
                    schemeDemandDepositDetail.PhotoDocumentLocalStoragePath = "None";
                    schemeDemandDepositDetail.PhotoDocumentAllowedFileFormatsForDb = "None";
                    schemeDemandDepositDetail.PhotoDocumentAllowedFileFormatsForLocalStorage = "None";

                    // Sign Document Upload
                    schemeDemandDepositDetail.SignDocumentUpload = "N";
                    schemeDemandDepositDetail.SignDocumentLocalStoragePath = "None";
                    schemeDemandDepositDetail.SignDocumentAllowedFileFormatsForDb = "None";
                    schemeDemandDepositDetail.SignDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    // Multi Select Value For Dropdown Photo Document Upload And Sign Document Upload
                    if (_schemeDemandDepositDetailViewModel.PhotoDocumentUpload != StringLiteralValue.Disable)
                    {
                        if (_schemeDemandDepositDetailViewModel.EnablePhotoDocumentUploadInDb)
                        {
                            schemeDemandDepositDetail.PhotoDocumentAllowedFileFormatsForDb = string.Join(",", _schemeDemandDepositDetailViewModel.PhotoDocumentFormatTypeIdForDatabase);
                            schemeDemandDepositDetail.PhotoDocumentAllowedFileFormatsForLocalStorage = "None";
                            schemeDemandDepositDetail.PhotoDocumentLocalStoragePath = "None";
                        }
                        else
                        {
                            schemeDemandDepositDetail.PhotoDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _schemeDemandDepositDetailViewModel.PhotoDocumentFormatTypeIdForLocalStorage);
                            schemeDemandDepositDetail.PhotoDocumentAllowedFileFormatsForDb = "None";
                        }
                    }
                    else
                    {
                        schemeDemandDepositDetail.PhotoDocumentAllowedFileFormatsForDb = "None";
                        schemeDemandDepositDetail.PhotoDocumentAllowedFileFormatsForLocalStorage = "None";
                        schemeDemandDepositDetail.PhotoDocumentLocalStoragePath = "None";
                    }

                    // Multi Select Value For Dropdown Sign Document Upload
                    if (_schemeDemandDepositDetailViewModel.SignDocumentUpload != StringLiteralValue.Disable)
                    {
                        if (_schemeDemandDepositDetailViewModel.EnableSignDocumentUploadInDb)
                        {
                            schemeDemandDepositDetail.SignDocumentAllowedFileFormatsForDb = string.Join(",", _schemeDemandDepositDetailViewModel.SignDocumentFormatTypeIdForDatabase);
                            schemeDemandDepositDetail.SignDocumentAllowedFileFormatsForLocalStorage = "None";
                            schemeDemandDepositDetail.SignDocumentLocalStoragePath = "None";
                        }
                        else
                        {
                            schemeDemandDepositDetail.SignDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _schemeDemandDepositDetailViewModel.SignDocumentFormatTypeIdForLocalStorage);
                            schemeDemandDepositDetail.SignDocumentAllowedFileFormatsForDb = "None";
                        }
                    }
                    else
                    {
                        schemeDemandDepositDetail.SignDocumentAllowedFileFormatsForDb = "None";
                        schemeDemandDepositDetail.SignDocumentAllowedFileFormatsForLocalStorage = "None";
                        schemeDemandDepositDetail.SignDocumentLocalStoragePath = "None";
                    }
                }


                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeDemandDepositDetail.SchemePrmKey = schemePrmKey;

                    context.SchemeDemandDepositDetails.Attach(schemeDemandDepositDetail);
                    context.Entry(schemeDemandDepositDetail).State = entityState;
                    scheme.SchemeDemandDepositDetails.Add(schemeDemandDepositDetail);

                    context.SchemeDemandDepositDetailMakerCheckers.Attach(schemeDemandDepositDetailMakerChecker);
                    context.Entry(schemeDemandDepositDetailMakerChecker).State = EntityState.Added;
                    schemeDemandDepositDetail.SchemeDemandDepositDetailMakerCheckers.Add(schemeDemandDepositDetailMakerChecker);
                }
                else
                {
                    context.SchemeDemandDepositDetailMakerCheckers.Attach(schemeDemandDepositDetailMakerChecker);
                    context.Entry(schemeDemandDepositDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeFixedDepositParameterData(SchemeFixedDepositParameterViewModel _schemeFixedDepositParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeFixedDepositParameterViewModel, _entryType);

                SchemeFixedDepositParameter schemeFixedDepositParameter = Mapper.Map<SchemeFixedDepositParameter>(_schemeFixedDepositParameterViewModel);
                SchemeFixedDepositParameterMakerChecker schemeFixedDepositParameterMakerChecker = Mapper.Map<SchemeFixedDepositParameterMakerChecker>(_schemeFixedDepositParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeFixedDepositParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeFixedDepositParameters.Attach(schemeFixedDepositParameter);
                    context.Entry(schemeFixedDepositParameter).State = entityState;
                    scheme.SchemeFixedDepositParameters.Add(schemeFixedDepositParameter);

                    context.SchemeFixedDepositParameterMakerCheckers.Attach(schemeFixedDepositParameterMakerChecker);
                    context.Entry(schemeFixedDepositParameterMakerChecker).State = EntityState.Added;
                    schemeFixedDepositParameter.SchemeFixedDepositParameterMakerCheckers.Add(schemeFixedDepositParameterMakerChecker);
                }
                else
                {
                    context.SchemeFixedDepositParameterMakerCheckers.Attach(schemeFixedDepositParameterMakerChecker);
                    context.Entry(schemeFixedDepositParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeDepositPledgeLoanParameterData(SchemeDepositPledgeLoanParameterViewModel _schemeDepositPledgeLoanParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeDepositPledgeLoanParameterViewModel, _entryType);

                SchemeDepositPledgeLoanParameter schemeDepositPledgeLoanParameter = Mapper.Map<SchemeDepositPledgeLoanParameter>(_schemeDepositPledgeLoanParameterViewModel);
                SchemeDepositPledgeLoanParameterMakerChecker schemeDepositPledgeLoanParameterMakerChecker = Mapper.Map<SchemeDepositPledgeLoanParameterMakerChecker>(_schemeDepositPledgeLoanParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeDepositPledgeLoanParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeDepositPledgeLoanParameters.Attach(schemeDepositPledgeLoanParameter);
                    context.Entry(schemeDepositPledgeLoanParameter).State = entityState;
                    scheme.SchemeDepositPledgeLoanParameters.Add(schemeDepositPledgeLoanParameter);

                    context.SchemeDepositPledgeLoanParameterMakerCheckers.Attach(schemeDepositPledgeLoanParameterMakerChecker);
                    context.Entry(schemeDepositPledgeLoanParameterMakerChecker).State = EntityState.Added;
                    schemeDepositPledgeLoanParameter.SchemeDepositPledgeLoanParameterMakerCheckers.Add(schemeDepositPledgeLoanParameterMakerChecker);
                }
                else
                {
                    context.SchemeDepositPledgeLoanParameterMakerCheckers.Attach(schemeDepositPledgeLoanParameterMakerChecker);
                    context.Entry(schemeDepositPledgeLoanParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeDepositAccountRenewalParameterData(SchemeDepositAccountRenewalParameterViewModel _schemeDepositAccountRenewalParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeDepositAccountRenewalParameterViewModel, _entryType);

                SchemeDepositAccountRenewalParameter schemeDepositAccountRenewalParameter = Mapper.Map<SchemeDepositAccountRenewalParameter>(_schemeDepositAccountRenewalParameterViewModel);
                SchemeDepositAccountRenewalParameterMakerChecker schemeDepositAccountRenewalParameterMakerChecker = Mapper.Map<SchemeDepositAccountRenewalParameterMakerChecker>(_schemeDepositAccountRenewalParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeDepositAccountRenewalParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeDepositAccountRenewalParameters.Attach(schemeDepositAccountRenewalParameter);
                    context.Entry(schemeDepositAccountRenewalParameter).State = entityState;
                    scheme.SchemeDepositAccountRenewalParameters.Add(schemeDepositAccountRenewalParameter);

                    context.SchemeDepositAccountRenewalParameterMakerCheckers.Attach(schemeDepositAccountRenewalParameterMakerChecker);
                    context.Entry(schemeDepositAccountRenewalParameterMakerChecker).State = EntityState.Added;
                    schemeDepositAccountRenewalParameter.SchemeDepositAccountRenewalParameterMakerCheckers.Add(schemeDepositAccountRenewalParameterMakerChecker);
                }
                else
                {
                    context.SchemeDepositAccountRenewalParameterMakerCheckers.Attach(schemeDepositAccountRenewalParameterMakerChecker);
                    context.Entry(schemeDepositAccountRenewalParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemePassbookData(SchemePassbookViewModel _schemePassbookViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemePassbookViewModel, _entryType);

                SchemePassbook schemePassbook = Mapper.Map<SchemePassbook>(_schemePassbookViewModel);
                SchemePassbookMakerChecker schemePassbookMakerChecker = Mapper.Map<SchemePassbookMakerChecker>(_schemePassbookViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemePassbook.SchemePrmKey = schemePrmKey;

                    if (_schemePassbookViewModel.EnablePassbookNumberBranchwise == true || _schemePassbookViewModel.EnableAutoPassbookNumber == false)
                        schemePassbook.PassbookNumberMask = "None";
                    else
                        schemePassbook.PassbookNumberMask = string.Join(",", _schemePassbookViewModel.MaskTypeCharacterForPassbook);


                    context.SchemePassbooks.Attach(schemePassbook);
                    context.Entry(schemePassbook).State = entityState;
                    scheme.SchemePassbooks.Add(schemePassbook);

                    context.SchemePassbookMakerCheckers.Attach(schemePassbookMakerChecker);
                    context.Entry(schemePassbookMakerChecker).State = EntityState.Added;
                    schemePassbook.SchemePassbookMakerCheckers.Add(schemePassbookMakerChecker);
                }
                else
                {
                    context.SchemePassbookMakerCheckers.Attach(schemePassbookMakerChecker);
                    context.Entry(schemePassbookMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeDepositAccountClosureParameterData(SchemeDepositAccountClosureParameterViewModel _schemeDepositAccountClosureParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeDepositAccountClosureParameterViewModel, _entryType);

                SchemeDepositAccountClosureParameter schemeDepositAccountClosureParameter = Mapper.Map<SchemeDepositAccountClosureParameter>(_schemeDepositAccountClosureParameterViewModel);
                SchemeDepositAccountClosureParameterMakerChecker schemeDepositAccountClosureParameterMakerChecker = Mapper.Map<SchemeDepositAccountClosureParameterMakerChecker>(_schemeDepositAccountClosureParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeDepositAccountClosureParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeDepositAccountClosureParameters.Attach(schemeDepositAccountClosureParameter);
                    context.Entry(schemeDepositAccountClosureParameter).State = entityState;
                    scheme.SchemeDepositAccountClosureParameters.Add(schemeDepositAccountClosureParameter);

                    context.SchemeDepositAccountClosureParameterMakerCheckers.Attach(schemeDepositAccountClosureParameterMakerChecker);
                    context.Entry(schemeDepositAccountClosureParameterMakerChecker).State = EntityState.Added;
                    schemeDepositAccountClosureParameter.SchemeDepositAccountClosureParameterMakerCheckers.Add(schemeDepositAccountClosureParameterMakerChecker);
                }
                else
                {
                    context.SchemeDepositAccountClosureParameterMakerCheckers.Attach(schemeDepositAccountClosureParameterMakerChecker);
                    context.Entry(schemeDepositAccountClosureParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeTenureListData(SchemeTenureListViewModel _schemeTenureListViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeTenureListViewModel, _entryType);

                SchemeTenureList schemeTenureList = Mapper.Map<SchemeTenureList>(_schemeTenureListViewModel);
                SchemeTenureListMakerChecker schemeTenureListMakerChecker = Mapper.Map<SchemeTenureListMakerChecker>(_schemeTenureListViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeTenureList.SchemePrmKey = schemePrmKey;

                    context.SchemeTenureLists.Attach(schemeTenureList);
                    context.Entry(schemeTenureList).State = EntityState.Added;
                    scheme.SchemeTenureLists.Add(schemeTenureList);

                    context.SchemeTenureListMakerCheckers.Attach(schemeTenureListMakerChecker);
                    context.Entry(schemeTenureListMakerChecker).State = EntityState.Added;
                    schemeTenureList.SchemeTenureListMakerCheckers.Add(schemeTenureListMakerChecker);
                }
                else
                {
                    context.SchemeTenureListMakerCheckers.Attach(schemeTenureListMakerChecker);
                    context.Entry(schemeTenureListMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeTenureData(SchemeTenureViewModel _schemeTenureViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeTenureViewModel, _entryType);

                SchemeTenure schemeTenure = Mapper.Map<SchemeTenure>(_schemeTenureViewModel);
                SchemeTenureMakerChecker schemeTenureMakerChecker = Mapper.Map<SchemeTenureMakerChecker>(_schemeTenureViewModel);

                // Get PrmKey By ID
                schemeTenure.TimePeriodUnitPrmKey = configurationDetailRepository.GetTimePeriodUnitPrmKeyById(_schemeTenureViewModel.TimePeriodUnitId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeTenure.SchemePrmKey = schemePrmKey;

                    context.SchemeTenures.Attach(schemeTenure);
                    context.Entry(schemeTenure).State = entityState;
                    scheme.SchemeTenures.Add(schemeTenure);

                    context.SchemeTenureMakerCheckers.Attach(schemeTenureMakerChecker);
                    context.Entry(schemeTenureMakerChecker).State = EntityState.Added;
                    schemeTenure.SchemeTenureMakerCheckers.Add(schemeTenureMakerChecker);
                }
                else
                {
                    context.SchemeTenureMakerCheckers.Attach(schemeTenureMakerChecker);
                    context.Entry(schemeTenureMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        // Loan Scheme 
        public bool AttachLoanSchemeData(LoanSchemeViewModel _loanSchemeViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_loanSchemeViewModel, _entryType);

                Scheme scheme = Mapper.Map<Scheme>(_loanSchemeViewModel);
                SchemeMakerChecker schemeMakerChecker = Mapper.Map<SchemeMakerChecker>(_loanSchemeViewModel);

                SchemeTranslation schemeTranslation = Mapper.Map<SchemeTranslation>(_loanSchemeViewModel);
                SchemeTranslationMakerChecker schemeTranslationMakerChecker = Mapper.Map<SchemeTranslationMakerChecker>(_loanSchemeViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemePrmKey = _loanSchemeViewModel.SchemePrmKey;
                    scheme.PrmKey = schemePrmKey;
                    scheme.SchemeTypePrmKey = 4;
                    schemeTranslation.PrmKey = _loanSchemeViewModel.SchemeTranslationPrmKey;

                    context.Schemes.Attach(scheme);
                    context.Entry(scheme).State = entityState;

                    context.SchemeMakerCheckers.Attach(schemeMakerChecker);
                    context.Entry(schemeMakerChecker).State = EntityState.Added;
                    scheme.SchemeMakerCheckers.Add(schemeMakerChecker);

                    context.SchemeTranslations.Attach(schemeTranslation);
                    context.Entry(schemeTranslation).State = entityState;
                    scheme.SchemeTranslations.Add(schemeTranslation);

                    context.SchemeTranslationMakerCheckers.Attach(schemeTranslationMakerChecker);
                    context.Entry(schemeTranslationMakerChecker).State = EntityState.Added;
                    schemeTranslation.SchemeTranslationMakerCheckers.Add(schemeTranslationMakerChecker);
                }
                else
                {
                    context.SchemeMakerCheckers.Attach(schemeMakerChecker);
                    context.Entry(schemeMakerChecker).State = EntityState.Added;

                    context.SchemeTranslationMakerCheckers.Attach(schemeTranslationMakerChecker);
                    context.Entry(schemeTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanAccountParameterData(SchemeLoanAccountParameterViewModel _schemeLoanAccountParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanAccountParameterViewModel, _entryType);

                SchemeLoanAccountParameter schemeLoanAccountParameter = Mapper.Map<SchemeLoanAccountParameter>(_schemeLoanAccountParameterViewModel);
                SchemeLoanAccountParameterMakerChecker schemeLoanAccountParameterMakerChecker = Mapper.Map<SchemeLoanAccountParameterMakerChecker>(_schemeLoanAccountParameterViewModel);

                if (_schemeLoanAccountParameterViewModel.EnableGuarantorDetail == false)
                {
                    schemeLoanAccountParameter.EligibilityForGuarantor = "ALL";
                }

                schemeLoanAccountParameter.LoanTypePrmKey = accountDetailRepository.GetLoanTypePrmKeyById(_schemeLoanAccountParameterViewModel.LoanTypeId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanAccountParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanAccountParameters.Attach(schemeLoanAccountParameter);
                    context.Entry(schemeLoanAccountParameter).State = entityState;
                    scheme.SchemeLoanAccountParameters.Add(schemeLoanAccountParameter);

                    context.SchemeLoanAccountParameterMakerCheckers.Attach(schemeLoanAccountParameterMakerChecker);
                    context.Entry(schemeLoanAccountParameterMakerChecker).State = EntityState.Added;
                    schemeLoanAccountParameter.SchemeLoanAccountParameterMakerCheckers.Add(schemeLoanAccountParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanAccountParameterMakerCheckers.Attach(schemeLoanAccountParameterMakerChecker);
                    context.Entry(schemeLoanAccountParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeDocumentData(SchemeDocumentViewModel _schemeDocumentViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeDocumentViewModel, _entryType);

                SchemeDocument schemeDocument = Mapper.Map<SchemeDocument>(_schemeDocumentViewModel);
                SchemeDocumentMakerChecker schemeDocumentMakerChecker = Mapper.Map<SchemeDocumentMakerChecker>(_schemeDocumentViewModel);

                //Get PrmKey By Id
                schemeDocument.DocumentPrmKey = personDetailRepository.GetDocumentPrmKeyById(_schemeDocumentViewModel.DocumentId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeDocument.SchemePrmKey = schemePrmKey;

                    context.SchemeDocuments.Attach(schemeDocument);
                    context.Entry(schemeDocument).State = EntityState.Added;
                    scheme.SchemeDocuments.Add(schemeDocument);

                    context.SchemeDocumentMakerCheckers.Attach(schemeDocumentMakerChecker);
                    context.Entry(schemeDocumentMakerChecker).State = EntityState.Added;
                    schemeDocument.SchemeDocumentMakerCheckers.Add(schemeDocumentMakerChecker);
                }
                else
                {
                    context.SchemeDocumentMakerCheckers.Attach(schemeDocumentMakerChecker);
                    context.Entry(schemeDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeTargetGroupData(SchemeTargetGroupViewModel _schemeTargetGroupViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeTargetGroupViewModel, _entryType);

                //Get PrmKey By Id
                _schemeTargetGroupViewModel.TargetGroupPrmKey = accountDetailRepository.GetTargetGroupPrmKeyById(_schemeTargetGroupViewModel.TargetGroupId);
                _schemeTargetGroupViewModel.OccupationPrmKey = personDetailRepository.GetOccupationPrmKeyById(_schemeTargetGroupViewModel.OccupationId);
                _schemeTargetGroupViewModel.GenderPrmKey = personDetailRepository.GetGenderPrmKeyById(_schemeTargetGroupViewModel.GenderId);


                SchemeTargetGroup schemeTargetGroup = Mapper.Map<SchemeTargetGroup>(_schemeTargetGroupViewModel);
                SchemeTargetGroupMakerChecker schemeTargetGroupMakerChecker = Mapper.Map<SchemeTargetGroupMakerChecker>(_schemeTargetGroupViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeTargetGroup.SchemePrmKey = schemePrmKey;


                    context.SchemeTargetGroups.Attach(schemeTargetGroup);
                    context.Entry(schemeTargetGroup).State = EntityState.Added;
                    scheme.SchemeTargetGroups.Add(schemeTargetGroup);


                    context.SchemeTargetGroupMakerCheckers.Attach(schemeTargetGroupMakerChecker);
                    context.Entry(schemeTargetGroupMakerChecker).State = EntityState.Added;
                    schemeTargetGroup.SchemeTargetGroupMakerCheckers.Add(schemeTargetGroupMakerChecker);

                }
                else
                {
                    context.SchemeTargetGroupMakerCheckers.Attach(schemeTargetGroupMakerChecker);
                    context.Entry(schemeTargetGroupMakerChecker).State = EntityState.Added;
                }

                // For Gender Target Group
                if (_schemeTargetGroupViewModel.GenderPrmKey > 0)
                {
                    SchemeTargetGroupGender schemeTargetGroupGender = Mapper.Map<SchemeTargetGroupGender>(_schemeTargetGroupViewModel);
                    SchemeTargetGroupGenderMakerChecker schemeTargetGroupGenderMakerChecker = Mapper.Map<SchemeTargetGroupGenderMakerChecker>(_schemeTargetGroupViewModel);

                    if (_entryType == StringLiteralValue.Create)
                    {
                        context.SchemeTargetGroupGenders.Attach(schemeTargetGroupGender);
                        context.Entry(schemeTargetGroupGender).State = EntityState.Added;
                        schemeTargetGroup.SchemeTargetGroupGenders.Add(schemeTargetGroupGender);

                        context.SchemeTargetGroupGenderMakerCheckers.Attach(schemeTargetGroupGenderMakerChecker);
                        context.Entry(schemeTargetGroupGenderMakerChecker).State = EntityState.Added;
                        schemeTargetGroupGender.SchemeTargetGroupGenderMakerCheckers.Add(schemeTargetGroupGenderMakerChecker);
                    }
                    else
                    {

                        context.SchemeTargetGroupGenderMakerCheckers.Attach(schemeTargetGroupGenderMakerChecker);
                        context.Entry(schemeTargetGroupGenderMakerChecker).State = EntityState.Added;
                    }
                }

                //For Occupation Target Group
                if (_schemeTargetGroupViewModel.OccupationPrmKey > 0)
                {
                    SchemeTargetGroupOccupation schemeTargetGroupOccupation = Mapper.Map<SchemeTargetGroupOccupation>(_schemeTargetGroupViewModel);
                    SchemeTargetGroupOccupationMakerChecker schemeTargetGroupOccupationMakerChecker = Mapper.Map<SchemeTargetGroupOccupationMakerChecker>(_schemeTargetGroupViewModel);

                    if (_entryType == StringLiteralValue.Create)
                    {
                        context.SchemeTargetGroupOccupations.Attach(schemeTargetGroupOccupation);
                        context.Entry(schemeTargetGroupOccupation).State = EntityState.Added;
                        schemeTargetGroup.SchemeTargetGroupOccupations.Add(schemeTargetGroupOccupation);

                        context.SchemeTargetGroupOccupationMakerCheckers.Attach(schemeTargetGroupOccupationMakerChecker);
                        context.Entry(schemeTargetGroupOccupationMakerChecker).State = EntityState.Added;
                        schemeTargetGroupOccupation.SchemeTargetGroupOccupationMakerCheckers.Add(schemeTargetGroupOccupationMakerChecker);
                    }
                    else

                    {
                        context.SchemeTargetGroupOccupationMakerCheckers.Attach(schemeTargetGroupOccupationMakerChecker);
                        context.Entry(schemeTargetGroupOccupationMakerChecker).State = EntityState.Added;
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanRepaymentScheduleParameterData(SchemeLoanRepaymentScheduleParameterViewModel _schemeLoanRepaymentScheduleParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanRepaymentScheduleParameterViewModel, _entryType);

                SchemeLoanRepaymentScheduleParameter schemeLoanRepaymentScheduleParameter = Mapper.Map<SchemeLoanRepaymentScheduleParameter>(_schemeLoanRepaymentScheduleParameterViewModel);
                SchemeLoanRepaymentScheduleParameterMakerChecker schemeLoanRepaymentScheduleParameterMakerChecker = Mapper.Map<SchemeLoanRepaymentScheduleParameterMakerChecker>(_schemeLoanRepaymentScheduleParameterViewModel);

                //get Prmkey By Id
                schemeLoanRepaymentScheduleParameter.RepaymentIntervalFrequencyPrmKey = accountDetailRepository.GetRepaymentIntervalFrequencyPrmKeyById(_schemeLoanRepaymentScheduleParameterViewModel.RepaymentIntervalFrequencyId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanRepaymentScheduleParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanRepaymentScheduleParameters.Attach(schemeLoanRepaymentScheduleParameter);
                    context.Entry(schemeLoanRepaymentScheduleParameter).State = entityState;
                    scheme.SchemeLoanRepaymentScheduleParameters.Add(schemeLoanRepaymentScheduleParameter);

                    context.SchemeLoanRepaymentScheduleParameterMakerCheckers.Attach(schemeLoanRepaymentScheduleParameterMakerChecker);
                    context.Entry(schemeLoanRepaymentScheduleParameterMakerChecker).State = EntityState.Added;
                    schemeLoanRepaymentScheduleParameter.SchemeLoanRepaymentScheduleParameterMakerCheckers.Add(schemeLoanRepaymentScheduleParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanRepaymentScheduleParameterMakerCheckers.Attach(schemeLoanRepaymentScheduleParameterMakerChecker);
                    context.Entry(schemeLoanRepaymentScheduleParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanSettlementAccountParameterData(SchemeLoanSettlementAccountParameterViewModel _schemeLoanSettlementAccountParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanSettlementAccountParameterViewModel, _entryType);

                SchemeLoanSettlementAccountParameter schemeLoanSettlementAccountParameter = Mapper.Map<SchemeLoanSettlementAccountParameter>(_schemeLoanSettlementAccountParameterViewModel);
                SchemeLoanSettlementAccountParameterMakerChecker schemeLoanSettlementAccountParameterMakerChecker = Mapper.Map<SchemeLoanSettlementAccountParameterMakerChecker>(_schemeLoanSettlementAccountParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanSettlementAccountParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanSettlementAccountParameters.Attach(schemeLoanSettlementAccountParameter);
                    context.Entry(schemeLoanSettlementAccountParameter).State = entityState;
                    scheme.SchemeLoanSettlementAccountParameters.Add(schemeLoanSettlementAccountParameter);

                    context.SchemeLoanSettlementAccountParameterMakerCheckers.Attach(schemeLoanSettlementAccountParameterMakerChecker);
                    context.Entry(schemeLoanSettlementAccountParameterMakerChecker).State = EntityState.Added;
                    schemeLoanSettlementAccountParameter.SchemeLoanSettlementAccountParameterMakerCheckers.Add(schemeLoanSettlementAccountParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanSettlementAccountParameterMakerCheckers.Attach(schemeLoanSettlementAccountParameterMakerChecker);
                    context.Entry(schemeLoanSettlementAccountParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanInterestParameterData(SchemeLoanInterestParameterViewModel _schemeLoanInterestParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanInterestParameterViewModel, _entryType);

                SchemeLoanInterestParameter schemeLoanInterestParameter = Mapper.Map<SchemeLoanInterestParameter>(_schemeLoanInterestParameterViewModel);
                SchemeLoanInterestParameterMakerChecker schemeLoanInterestParameterMakerChecker = Mapper.Map<SchemeLoanInterestParameterMakerChecker>(_schemeLoanInterestParameterViewModel);

                //Get PrmKey By Id
                schemeLoanInterestParameter.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_schemeLoanInterestParameterViewModel.GeneralLedgerId);
                schemeLoanInterestParameter.InterestMethodPrmKey = accountDetailRepository.GetInterestMethodPrmKeyById(_schemeLoanInterestParameterViewModel.InterestMethodId);
                schemeLoanInterestParameter.LendingInterestPostingFrequencyPrmKey = accountDetailRepository.GetLendingInterestPostingFrequencyPrmKeyById(_schemeLoanInterestParameterViewModel.LendingInterestPostingFrequencyId);
                schemeLoanInterestParameter.InterestRateChargedDurationPrmKey = accountDetailRepository.GetInterestRateChargedDurationPrmKeyById(_schemeLoanInterestParameterViewModel.InterestRateChargedDurationId);
                schemeLoanInterestParameter.DaysInYearPrmKey = accountDetailRepository.GetDaysInYearPrmKeyById(_schemeLoanInterestParameterViewModel.DaysInYearId);
                schemeLoanInterestParameter.LendingRepaymentsInterestCalculationPrmKey = accountDetailRepository.GetLendingRepaymentsInterestCalculationPrmKeyById(_schemeLoanInterestParameterViewModel.LendingRepaymentsInterestCalculationId);
                string interestMethodType = accountDetailRepository.GetSysNameOfInterestMethodTypeById(_schemeLoanInterestParameterViewModel.InterestMethodId);

                if (interestMethodType != "COMPOUND")
                    schemeLoanInterestParameter.InterestCompoundingFrequencyPrmKey = 1;
                else
                    schemeLoanInterestParameter.InterestCompoundingFrequencyPrmKey = accountDetailRepository.GetInterestCompoundingFrequencyPrmKeyById(_schemeLoanInterestParameterViewModel.InterestCompoundingFrequencyId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanInterestParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanInterestParameters.Attach(schemeLoanInterestParameter);
                    context.Entry(schemeLoanInterestParameter).State = entityState;
                    scheme.SchemeLoanInterestParameters.Add(schemeLoanInterestParameter);

                    context.SchemeLoanInterestParameterMakerCheckers.Attach(schemeLoanInterestParameterMakerChecker);
                    context.Entry(schemeLoanInterestParameterMakerChecker).State = EntityState.Added;
                    schemeLoanInterestParameter.SchemeLoanInterestParameterMakerCheckers.Add(schemeLoanInterestParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanInterestParameterMakerCheckers.Attach(schemeLoanInterestParameterMakerChecker);
                    context.Entry(schemeLoanInterestParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


        public bool AttachSchemeLoanSanctionAuthorityData(SchemeLoanSanctionAuthorityViewModel _schemeLoanSanctionAuthorityViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanSanctionAuthorityViewModel, _entryType);
                SchemeLoanSanctionAuthority schemeLoanSanctionAuthority = Mapper.Map<SchemeLoanSanctionAuthority>(_schemeLoanSanctionAuthorityViewModel);
                SchemeLoanSanctionAuthorityMakerChecker schemeLoanSanctionAuthorityMakerChecker = Mapper.Map<SchemeLoanSanctionAuthorityMakerChecker>(_schemeLoanSanctionAuthorityViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanSanctionAuthority.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanSanctionAuthorities.Attach(schemeLoanSanctionAuthority);
                    context.Entry(schemeLoanSanctionAuthority).State = entityState;
                    scheme.SchemeLoanSanctionAuthorities.Add(schemeLoanSanctionAuthority);

                    context.SchemeLoanSanctionAuthorityMakerCheckers.Attach(schemeLoanSanctionAuthorityMakerChecker);
                    context.Entry(schemeLoanSanctionAuthorityMakerChecker).State = EntityState.Added;
                    schemeLoanSanctionAuthority.SchemeLoanSanctionAuthorityMakerCheckers.Add(schemeLoanSanctionAuthorityMakerChecker);
                }
                else
                {
                    context.SchemeLoanSanctionAuthorityMakerCheckers.Attach(schemeLoanSanctionAuthorityMakerChecker);
                    context.Entry(schemeLoanSanctionAuthorityMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeCashCreditLoanParameterData(SchemeCashCreditLoanParameterViewModel _schemeCashCreditLoanParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeCashCreditLoanParameterViewModel, _entryType);
                SchemeCashCreditLoanParameter schemeCashCreditLoanParameter = Mapper.Map<SchemeCashCreditLoanParameter>(_schemeCashCreditLoanParameterViewModel);
                SchemeCashCreditLoanParameterMakerChecker schemeCashCreditLoanParameterMakerChecker = Mapper.Map<SchemeCashCreditLoanParameterMakerChecker>(_schemeCashCreditLoanParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeCashCreditLoanParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeCashCreditLoanParameters.Attach(schemeCashCreditLoanParameter);
                    context.Entry(schemeCashCreditLoanParameter).State = entityState;
                    scheme.SchemeCashCreditLoanParameters.Add(schemeCashCreditLoanParameter);

                    context.SchemeCashCreditLoanParameterMakerCheckers.Attach(schemeCashCreditLoanParameterMakerChecker);
                    context.Entry(schemeCashCreditLoanParameterMakerChecker).State = EntityState.Added;
                    schemeCashCreditLoanParameter.SchemeCashCreditLoanParameterMakerCheckers.Add(schemeCashCreditLoanParameterMakerChecker);
                }
                else
                {
                    context.SchemeCashCreditLoanParameterMakerCheckers.Attach(schemeCashCreditLoanParameterMakerChecker);
                    context.Entry(schemeCashCreditLoanParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeEducationLoanParameterData(SchemeEducationLoanParameterViewModel _schemeEducationLoanParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeEducationLoanParameterViewModel, _entryType);
                SchemeEducationLoanParameter schemeEducationLoanParameter = Mapper.Map<SchemeEducationLoanParameter>(_schemeEducationLoanParameterViewModel);
                SchemeEducationLoanParameterMakerChecker schemeEducationLoanParameterMakerChecker = Mapper.Map<SchemeEducationLoanParameterMakerChecker>(_schemeEducationLoanParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeEducationLoanParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeEducationLoanParameters.Attach(schemeEducationLoanParameter);
                    context.Entry(schemeEducationLoanParameter).State = entityState;
                    scheme.SchemeEducationLoanParameters.Add(schemeEducationLoanParameter);

                    context.SchemeEducationLoanParameterMakerCheckers.Attach(schemeEducationLoanParameterMakerChecker);
                    context.Entry(schemeEducationLoanParameterMakerChecker).State = EntityState.Added;
                    schemeEducationLoanParameter.SchemeEducationLoanParameterMakerCheckers.Add(schemeEducationLoanParameterMakerChecker);
                }
                else
                {
                    context.SchemeEducationLoanParameterMakerCheckers.Attach(schemeEducationLoanParameterMakerChecker);
                    context.Entry(schemeEducationLoanParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeEducationalCourseData(SchemeEducationalCourseViewModel _schemeEducationalCourseViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeEducationalCourseViewModel, _entryType);
                SchemeEducationalCourse schemeEducationalCourse = Mapper.Map<SchemeEducationalCourse>(_schemeEducationalCourseViewModel);
                SchemeEducationalCourseMakerChecker schemeEducationalCourseMakerChecker = Mapper.Map<SchemeEducationalCourseMakerChecker>(_schemeEducationalCourseViewModel);
                schemeEducationalCourse.EducationalCoursePrmKey = accountDetailRepository.GetEducationalCoursePrmKeyById(_schemeEducationalCourseViewModel.EducationalCourseId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeEducationalCourse.SchemePrmKey = schemePrmKey;

                    context.SchemeEducationalCourses.Attach(schemeEducationalCourse);
                    context.Entry(schemeEducationalCourse).State = EntityState.Added;
                    scheme.SchemeEducationalCourses.Add(schemeEducationalCourse);

                    context.SchemeEducationalCourseMakerCheckers.Attach(schemeEducationalCourseMakerChecker);
                    context.Entry(schemeEducationalCourseMakerChecker).State = EntityState.Added;
                    schemeEducationalCourse.SchemeEducationalCourseMakerCheckers.Add(schemeEducationalCourseMakerChecker);
                }
                else
                {
                    context.SchemeEducationalCourseMakerCheckers.Attach(schemeEducationalCourseMakerChecker);
                    context.Entry(schemeEducationalCourseMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeInstituteData(SchemeInstituteViewModel _schemeInstituteViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeInstituteViewModel, _entryType);
                SchemeInstitute schemeInstitute = Mapper.Map<SchemeInstitute>(_schemeInstituteViewModel);
                SchemeInstituteMakerChecker schemeInstituteMakerChecker = Mapper.Map<SchemeInstituteMakerChecker>(_schemeInstituteViewModel);
                schemeInstitute.InstitutePrmKey = accountDetailRepository.GetInstitutePrmKeyById(_schemeInstituteViewModel.InstituteId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeInstitute.SchemePrmKey = schemePrmKey;

                    context.SchemeInstitutes.Attach(schemeInstitute);
                    context.Entry(schemeInstitute).State = EntityState.Added;
                    scheme.SchemeInstitutes.Add(schemeInstitute);

                    context.SchemeInstituteMakerCheckers.Attach(schemeInstituteMakerChecker);
                    context.Entry(schemeInstituteMakerChecker).State = EntityState.Added;
                    schemeInstitute.SchemeInstituteMakerCheckers.Add(schemeInstituteMakerChecker);
                }
                else
                {
                    context.SchemeInstituteMakerCheckers.Attach(schemeInstituteMakerChecker);
                    context.Entry(schemeInstituteMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


        public bool AttachSchemeLoanPaymentReminderParameterData(SchemeLoanPaymentReminderParameterViewModel _schemeLoanPaymentReminderParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanPaymentReminderParameterViewModel, _entryType);
                SchemeLoanPaymentReminderParameter schemeLoanPaymentReminderParameter = Mapper.Map<SchemeLoanPaymentReminderParameter>(_schemeLoanPaymentReminderParameterViewModel);
                SchemeLoanPaymentReminderParameterMakerChecker schemeLoanPaymentReminderParameterMakerChecker = Mapper.Map<SchemeLoanPaymentReminderParameterMakerChecker>(_schemeLoanPaymentReminderParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanPaymentReminderParameter.SchemePrmKey = schemePrmKey;
                    // schemeLoanPaymentReminderParameter.StartDaysAfterOverduePaymentDate = 123;
                    context.SchemeLoanPaymentReminderParameters.Attach(schemeLoanPaymentReminderParameter);
                    context.Entry(schemeLoanPaymentReminderParameter).State = entityState;
                    scheme.SchemeLoanPaymentReminderParameters.Add(schemeLoanPaymentReminderParameter);

                    context.SchemeLoanPaymentReminderParameterMakerCheckers.Attach(schemeLoanPaymentReminderParameterMakerChecker);
                    context.Entry(schemeLoanPaymentReminderParameterMakerChecker).State = EntityState.Added;
                    schemeLoanPaymentReminderParameter.SchemeLoanPaymentReminderParameterMakerCheckers.Add(schemeLoanPaymentReminderParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanPaymentReminderParameterMakerCheckers.Attach(schemeLoanPaymentReminderParameterMakerChecker);
                    context.Entry(schemeLoanPaymentReminderParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemePreownedVehicleLoanParameterData(SchemePreownedVehicleLoanParameterViewModel _schemePreownedVehicleLoanParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemePreownedVehicleLoanParameterViewModel, _entryType);
                SchemePreownedVehicleLoanParameter schemePreownedVehicleLoanParameter = Mapper.Map<SchemePreownedVehicleLoanParameter>(_schemePreownedVehicleLoanParameterViewModel);
                SchemePreownedVehicleLoanParameterMakerChecker schemePreownedVehicleLoanParameterMakerChecker = Mapper.Map<SchemePreownedVehicleLoanParameterMakerChecker>(_schemePreownedVehicleLoanParameterViewModel);
                schemePreownedVehicleLoanParameter.VehicleTypePrmKey = accountDetailRepository.GetVehicleTypePrmKeyById(_schemePreownedVehicleLoanParameterViewModel.VehicleTypeId);

                // Multi Select Value For Dropdown 
                if (_schemePreownedVehicleLoanParameterViewModel.PhotoUpload == "D")
                {
                    schemePreownedVehicleLoanParameter.AllowedFileFormatsForDb = "None";
                    schemePreownedVehicleLoanParameter.AllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    if (_schemePreownedVehicleLoanParameterViewModel.EnablePhotoUploadInDb == false)
                    {
                        schemePreownedVehicleLoanParameter.AllowedFileFormatsForDb = "None";
                        schemePreownedVehicleLoanParameter.AllowedFileFormatsForLocalStorage = string.Join(",", _schemePreownedVehicleLoanParameterViewModel.AllowedFileFormatsForLocalStorage);
                    }
                    else
                    {
                        schemePreownedVehicleLoanParameter.AllowedFileFormatsForLocalStorage = "None";
                        schemePreownedVehicleLoanParameter.AllowedFileFormatsForDb = string.Join(",", _schemePreownedVehicleLoanParameterViewModel.AllowedFileFormatsForDb);
                    }
                }

                if (_entryType == StringLiteralValue.Create)
                {
                    schemePreownedVehicleLoanParameter.SchemePrmKey = schemePrmKey;

                    context.SchemePreownedVehicleLoanParameters.Attach(schemePreownedVehicleLoanParameter);
                    context.Entry(schemePreownedVehicleLoanParameter).State = EntityState.Added;
                    scheme.SchemePreownedVehicleLoanParameters.Add(schemePreownedVehicleLoanParameter);

                    context.SchemePreownedVehicleLoanParameterMakerCheckers.Attach(schemePreownedVehicleLoanParameterMakerChecker);
                    context.Entry(schemePreownedVehicleLoanParameterMakerChecker).State = EntityState.Added;
                    schemePreownedVehicleLoanParameter.SchemePreownedVehicleLoanParameterMakerCheckers.Add(schemePreownedVehicleLoanParameterMakerChecker);
                }
                else
                {
                    context.SchemePreownedVehicleLoanParameterMakerCheckers.Attach(schemePreownedVehicleLoanParameterMakerChecker);
                    context.Entry(schemePreownedVehicleLoanParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeVehicleTypeLoanParameterData(SchemeVehicleTypeLoanParameterViewModel _schemeVehicleTypeLoanParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeVehicleTypeLoanParameterViewModel, _entryType);
                SchemeVehicleTypeLoanParameter schemeVehicleTypeLoanParameter = Mapper.Map<SchemeVehicleTypeLoanParameter>(_schemeVehicleTypeLoanParameterViewModel);
                SchemeVehicleTypeLoanParameterMakerChecker schemeVehicleTypeLoanParameterMakerChecker = Mapper.Map<SchemeVehicleTypeLoanParameterMakerChecker>(_schemeVehicleTypeLoanParameterViewModel);
                schemeVehicleTypeLoanParameter.VehicleTypePrmKey = accountDetailRepository.GetVehicleTypePrmKeyById(_schemeVehicleTypeLoanParameterViewModel.VehicleTypeId);


                // Multi Select Value For Dropdown 
                if (_schemeVehicleTypeLoanParameterViewModel.PhotoUpload == "D")
                {
                    schemeVehicleTypeLoanParameter.AllowedFileFormatsForDb = "None";
                    schemeVehicleTypeLoanParameter.AllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    if (_schemeVehicleTypeLoanParameterViewModel.EnablePhotoUploadInDb == false)
                    {
                        schemeVehicleTypeLoanParameter.AllowedFileFormatsForDb = "None";
                        schemeVehicleTypeLoanParameter.AllowedFileFormatsForLocalStorage = string.Join(",", _schemeVehicleTypeLoanParameterViewModel.AllowedFileFormatsForLocalStorage);
                    }
                    else
                    {
                        schemeVehicleTypeLoanParameter.AllowedFileFormatsForLocalStorage = "None";
                        schemeVehicleTypeLoanParameter.AllowedFileFormatsForDb = string.Join(",", _schemeVehicleTypeLoanParameterViewModel.AllowedFileFormatsForDb);
                    }
                }

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeVehicleTypeLoanParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeVehicleTypeLoanParameters.Attach(schemeVehicleTypeLoanParameter);
                    context.Entry(schemeVehicleTypeLoanParameter).State = EntityState.Added;
                    scheme.SchemeVehicleTypeLoanParameters.Add(schemeVehicleTypeLoanParameter);

                    context.SchemeVehicleTypeLoanParameterMakerCheckers.Attach(schemeVehicleTypeLoanParameterMakerChecker);
                    context.Entry(schemeVehicleTypeLoanParameterMakerChecker).State = EntityState.Added;
                    schemeVehicleTypeLoanParameter.SchemeVehicleTypeLoanParameterMakerCheckers.Add(schemeVehicleTypeLoanParameterMakerChecker);
                }
                else
                {
                    context.SchemeVehicleTypeLoanParameterMakerCheckers.Attach(schemeVehicleTypeLoanParameterMakerChecker);
                    context.Entry(schemeVehicleTypeLoanParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeConsumerDurableLoanItemData(SchemeConsumerDurableLoanItemViewModel _schemeConsumerDurableLoanItemViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeConsumerDurableLoanItemViewModel, _entryType);
                SchemeConsumerDurableLoanItem schemeConsumerDurableLoanItem = Mapper.Map<SchemeConsumerDurableLoanItem>(_schemeConsumerDurableLoanItemViewModel);
                SchemeConsumerDurableLoanItemMakerChecker schemeConsumerDurableLoanItemMakerChecker = Mapper.Map<SchemeConsumerDurableLoanItemMakerChecker>(_schemeConsumerDurableLoanItemViewModel);
                schemeConsumerDurableLoanItem.ConsumerDurableItemPrmKey = accountDetailRepository.GetConsumerDurableLoanItemPrmKeyById(_schemeConsumerDurableLoanItemViewModel.ConsumerDurableItemId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeConsumerDurableLoanItem.SchemePrmKey = schemePrmKey;

                    context.SchemeConsumerDurableLoanItems.Attach(schemeConsumerDurableLoanItem);
                    context.Entry(schemeConsumerDurableLoanItem).State = EntityState.Added;
                    scheme.SchemeConsumerDurableLoanItems.Add(schemeConsumerDurableLoanItem);

                    context.SchemeConsumerDurableLoanItemMakerCheckers.Attach(schemeConsumerDurableLoanItemMakerChecker);
                    context.Entry(schemeConsumerDurableLoanItemMakerChecker).State = EntityState.Added;
                    schemeConsumerDurableLoanItem.SchemeConsumerDurableLoanItemMakerCheckers.Add(schemeConsumerDurableLoanItemMakerChecker);
                }
                else
                {
                    context.SchemeConsumerDurableLoanItemMakerCheckers.Attach(schemeConsumerDurableLoanItemMakerChecker);
                    context.Entry(schemeConsumerDurableLoanItemMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

       
        //SchemeLoanAgainstDepositParameter
        public bool AttachSchemeLoanAgainstDepositParameterData(SchemeLoanAgainstDepositParameterViewModel _schemeLoanAgainstDepositParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanAgainstDepositParameterViewModel, _entryType);

                SchemeLoanAgainstDepositParameter schemeLoanAgainstDepositParameter = Mapper.Map<SchemeLoanAgainstDepositParameter>(_schemeLoanAgainstDepositParameterViewModel);
                SchemeLoanAgainstDepositParameterMakerChecker schemeLoanAgainstDepositParameterMakerChecker = Mapper.Map<SchemeLoanAgainstDepositParameterMakerChecker>(_schemeLoanAgainstDepositParameterViewModel);
                schemeLoanAgainstDepositParameter.InterestCalculationFrequencyPrmKey = accountDetailRepository.GetInterestCalculationFrequencyPrmKeyById(_schemeLoanAgainstDepositParameterViewModel.InterestCalculationFrequencyId);
                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanAgainstDepositParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanAgainstDepositParameters.Attach(schemeLoanAgainstDepositParameter);
                    context.Entry(schemeLoanAgainstDepositParameter).State = entityState;
                    scheme.SchemeLoanAgainstDepositParameters.Add(schemeLoanAgainstDepositParameter);

                    context.SchemeLoanAgainstDepositParameterMakerCheckers.Attach(schemeLoanAgainstDepositParameterMakerChecker);
                    context.Entry(schemeLoanAgainstDepositParameterMakerChecker).State = EntityState.Added;
                    schemeLoanAgainstDepositParameter.SchemeLoanAgainstDepositParameterMakerCheckers.Add(schemeLoanAgainstDepositParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanAgainstDepositParameterMakerCheckers.Attach(schemeLoanAgainstDepositParameterMakerChecker);
                    context.Entry(schemeLoanAgainstDepositParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanAgainstDepositGeneralLedgerData(SchemeLoanAgainstDepositGeneralLedgerViewModel _schemeLoanAgainstDepositGeneralLedgerViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanAgainstDepositGeneralLedgerViewModel, _entryType);

                SchemeLoanAgainstDepositGeneralLedger schemeLoanAgainstDepositGeneralLedger = Mapper.Map<SchemeLoanAgainstDepositGeneralLedger>(_schemeLoanAgainstDepositGeneralLedgerViewModel);
                SchemeLoanAgainstDepositGeneralLedgerMakerChecker schemeLoanAgainstDepositGeneralLedgerMakerChecker = Mapper.Map<SchemeLoanAgainstDepositGeneralLedgerMakerChecker>(_schemeLoanAgainstDepositGeneralLedgerViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    //  schemeDepositPledgeLoanGeneralLedger.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanAgainstDepositGeneralLedgers.Attach(schemeLoanAgainstDepositGeneralLedger);
                    context.Entry(schemeLoanAgainstDepositGeneralLedger).State = EntityState.Added;

                    context.SchemeLoanAgainstDepositGeneralLedgerMakerCheckers.Attach(schemeLoanAgainstDepositGeneralLedgerMakerChecker);
                    context.Entry(schemeLoanAgainstDepositGeneralLedgerMakerChecker).State = EntityState.Added;
                    schemeLoanAgainstDepositGeneralLedger.SchemeLoanAgainstDepositGeneralLedgerMakerCheckers.Add(schemeLoanAgainstDepositGeneralLedgerMakerChecker);
                }
                else
                {
                    context.SchemeLoanAgainstDepositGeneralLedgerMakerCheckers.Attach(schemeLoanAgainstDepositGeneralLedgerMakerChecker);
                    context.Entry(schemeLoanAgainstDepositGeneralLedgerMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeHomeLoanData(SchemeHomeLoanViewModel _schemeHomeLoanViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeHomeLoanViewModel, _entryType);
                SchemeHomeLoan schemeHomeLoan = Mapper.Map<SchemeHomeLoan>(_schemeHomeLoanViewModel);
                SchemeHomeLoanMakerChecker schemeHomeLoanMakerChecker = Mapper.Map<SchemeHomeLoanMakerChecker>(_schemeHomeLoanViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeHomeLoan.SchemePrmKey = schemePrmKey;

                    context.SchemeHomeLoans.Attach(schemeHomeLoan);
                    context.Entry(schemeHomeLoan).State = entityState;
                    scheme.SchemeHomeLoans.Add(schemeHomeLoan);

                    context.SchemeHomeLoanMakerCheckers.Attach(schemeHomeLoanMakerChecker);
                    context.Entry(schemeHomeLoanMakerChecker).State = EntityState.Added;
                    schemeHomeLoan.SchemeHomeLoanMakerCheckers.Add(schemeHomeLoanMakerChecker);
                }
                else
                {
                    context.SchemeHomeLoanMakerCheckers.Attach(schemeHomeLoanMakerChecker);
                    context.Entry(schemeHomeLoanMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanAgainstPropertyData(SchemeLoanAgainstPropertyViewModel _schemeLoanAgainstPropertyViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanAgainstPropertyViewModel, _entryType);
                SchemeLoanAgainstProperty schemeLoanAgainstProperty = Mapper.Map<SchemeLoanAgainstProperty>(_schemeLoanAgainstPropertyViewModel);
                SchemeLoanAgainstPropertyMakerChecker schemeLoanAgainstPropertyMakerChecker = Mapper.Map<SchemeLoanAgainstPropertyMakerChecker>(_schemeLoanAgainstPropertyViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanAgainstProperty.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanAgainstProperties.Attach(schemeLoanAgainstProperty);
                    context.Entry(schemeLoanAgainstProperty).State = entityState;
                    scheme.SchemeLoanAgainstProperties.Add(schemeLoanAgainstProperty);

                    context.SchemeLoanAgainstPropertyMakerCheckers.Attach(schemeLoanAgainstPropertyMakerChecker);
                    context.Entry(schemeLoanAgainstPropertyMakerChecker).State = EntityState.Added;
                    schemeLoanAgainstProperty.SchemeLoanAgainstPropertyMakerCheckers.Add(schemeLoanAgainstPropertyMakerChecker);
                }
                else
                {
                    context.SchemeLoanAgainstPropertyMakerCheckers.Attach(schemeLoanAgainstPropertyMakerChecker);
                    context.Entry(schemeLoanAgainstPropertyMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeBusinessLoanData(SchemeBusinessLoanViewModel _schemeBusinessLoanViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeBusinessLoanViewModel, _entryType);
                SchemeBusinessLoan schemeBusinessLoan = Mapper.Map<SchemeBusinessLoan>(_schemeBusinessLoanViewModel);
                SchemeBusinessLoanMakerChecker schemeBusinessLoanMakerChecker = Mapper.Map<SchemeBusinessLoanMakerChecker>(_schemeBusinessLoanViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeBusinessLoan.SchemePrmKey = schemePrmKey;

                    context.SchemeBusinessLoans.Attach(schemeBusinessLoan);
                    context.Entry(schemeBusinessLoan).State = entityState;
                    scheme.SchemeBusinessLoans.Add(schemeBusinessLoan);

                    context.SchemeBusinessLoanMakerCheckers.Attach(schemeBusinessLoanMakerChecker);
                    context.Entry(schemeBusinessLoanMakerChecker).State = EntityState.Added;
                    schemeBusinessLoan.SchemeBusinessLoanMakerCheckers.Add(schemeBusinessLoanMakerChecker);
                }
                else
                {
                    context.SchemeBusinessLoanMakerCheckers.Attach(schemeBusinessLoanMakerChecker);
                    context.Entry(schemeBusinessLoanMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }
        public bool AttachSchemeInterestRateData(SchemeInterestRateViewModel _schemeInterestRateViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeInterestRateViewModel, _entryType);

                SchemeInterestRate schemeInterestRate = Mapper.Map<SchemeInterestRate>(_schemeInterestRateViewModel);
                SchemeInterestRateMakerChecker schemeInterestRateMakerChecker = Mapper.Map<SchemeInterestRateMakerChecker>(_schemeInterestRateViewModel);
                schemeInterestRate.EffectiveDate = DateTime.Now;

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeInterestRate.SchemePrmKey = schemePrmKey;

                    context.SchemeInterestRates.Attach(schemeInterestRate);
                    context.Entry(schemeInterestRate).State = entityState;
                    scheme.SchemeInterestRates.Add(schemeInterestRate);

                    context.SchemeInterestRateMakerCheckers.Attach(schemeInterestRateMakerChecker);
                    context.Entry(schemeInterestRateMakerChecker).State = EntityState.Added;
                    schemeInterestRate.SchemeInterestRateMakerCheckers.Add(schemeInterestRateMakerChecker);
                }
                else
                {
                    context.SchemeInterestRateMakerCheckers.Attach(schemeInterestRateMakerChecker);
                    context.Entry(schemeInterestRateMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanFineInterestParameterData(SchemeLoanFineInterestParameterViewModel _schemeLoanFineInterestParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanFineInterestParameterViewModel, _entryType);
              /*  if (_schemeLoanFineInterestParameterViewModel.EnableFineOnMaturity == false)
                {
                    _schemeLoanFineInterestParameterViewModel.FineDays = 1;
                }

                if (_schemeLoanFineInterestParameterViewModel.EnableFineOnMissedInstallment == false)
                {
                    _schemeLoanFineInterestParameterViewModel.NumberOfMissedInstallment = 1;
                }*/

                SchemeLoanFineInterestParameter schemeLoanFineInterestParameter = Mapper.Map<SchemeLoanFineInterestParameter>(_schemeLoanFineInterestParameterViewModel);
                SchemeLoanFineInterestParameterMakerChecker schemeLoanFineInterestParameterMakerChecker = Mapper.Map<SchemeLoanFineInterestParameterMakerChecker>(_schemeLoanFineInterestParameterViewModel);

                //Get PrmKey By Id
                schemeLoanFineInterestParameter.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_schemeLoanFineInterestParameterViewModel.GeneralLedgerId);
                schemeLoanFineInterestParameter.InterestMethodPrmKey = accountDetailRepository.GetInterestMethodPrmKeyById(_schemeLoanFineInterestParameterViewModel.InterestMethodId);
                string interestMethodType = accountDetailRepository.GetSysNameOfInterestMethodTypeById(_schemeLoanFineInterestParameterViewModel.InterestMethodId);

                if (interestMethodType != "FLAT")
                {
                    schemeLoanFineInterestParameter.InterestRateChargedDurationPrmKey = accountDetailRepository.GetInterestRateChargedDurationPrmKeyById(_schemeLoanFineInterestParameterViewModel.InterestRateChargedDurationId);
                    schemeLoanFineInterestParameter.DaysInYearPrmKey = accountDetailRepository.GetDaysInYearPrmKeyById(_schemeLoanFineInterestParameterViewModel.DaysInYearId);
                    schemeLoanFineInterestParameter.LendingRepaymentsInterestCalculationPrmKey = accountDetailRepository.GetLendingRepaymentsInterestCalculationPrmKeyById(_schemeLoanFineInterestParameterViewModel.LendingRepaymentsInterestCalculationId);
                }
                else
                {
                    schemeLoanFineInterestParameter.InterestRateChargedDurationPrmKey = 1;
                    schemeLoanFineInterestParameter.DaysInYearPrmKey = 1;
                    schemeLoanFineInterestParameter.LendingRepaymentsInterestCalculationPrmKey = 1;
                }

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanFineInterestParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanFineInterestParameters.Attach(schemeLoanFineInterestParameter);
                    context.Entry(schemeLoanFineInterestParameter).State = entityState;
                    scheme.SchemeLoanFineInterestParameters.Add(schemeLoanFineInterestParameter);

                    context.SchemeLoanFineInterestParameterMakerCheckers.Attach(schemeLoanFineInterestParameterMakerChecker);
                    context.Entry(schemeLoanFineInterestParameterMakerChecker).State = EntityState.Added;
                    schemeLoanFineInterestParameter.SchemeLoanFineInterestParameterMakerCheckers.Add(schemeLoanFineInterestParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanFineInterestParameterMakerCheckers.Attach(schemeLoanFineInterestParameterMakerChecker);
                    context.Entry(schemeLoanFineInterestParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanInterestProvisionParameterData(SchemeLoanInterestProvisionParameterViewModel _schemeLoanInterestProvisionParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanInterestProvisionParameterViewModel, _entryType);

                SchemeLoanInterestProvisionParameter schemeLoanInterestProvisionParameter = Mapper.Map<SchemeLoanInterestProvisionParameter>(_schemeLoanInterestProvisionParameterViewModel);
                SchemeLoanInterestProvisionParameterMakerChecker schemeLoanInterestProvisionParameterMakerChecker = Mapper.Map<SchemeLoanInterestProvisionParameterMakerChecker>(_schemeLoanInterestProvisionParameterViewModel);

                //Get PrmKey By Id
                schemeLoanInterestProvisionParameter.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_schemeLoanInterestProvisionParameterViewModel.GeneralLedgerId);
                schemeLoanInterestProvisionParameter.InterestCalculationFrequencyPrmKey = accountDetailRepository.GetInterestCalculationFrequencyPrmKeyById(_schemeLoanInterestProvisionParameterViewModel.InterestCalculationFrequencyId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanInterestProvisionParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanInterestProvisionParameters.Attach(schemeLoanInterestProvisionParameter);
                    context.Entry(schemeLoanInterestProvisionParameter).State = entityState;
                    scheme.SchemeLoanInterestProvisionParameters.Add(schemeLoanInterestProvisionParameter);

                    context.SchemeLoanInterestProvisionParameterMakerCheckers.Attach(schemeLoanInterestProvisionParameterMakerChecker);
                    context.Entry(schemeLoanInterestProvisionParameterMakerChecker).State = EntityState.Added;
                    schemeLoanInterestProvisionParameter.SchemeLoanInterestProvisionParameterMakerCheckers.Add(schemeLoanInterestProvisionParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanInterestProvisionParameterMakerCheckers.Attach(schemeLoanInterestProvisionParameterMakerChecker);
                    context.Entry(schemeLoanInterestProvisionParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanDistributorParameterData(SchemeLoanDistributorParameterViewModel _schemeLoanDistributorParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanDistributorParameterViewModel, _entryType);

                SchemeLoanDistributorParameter schemeLoanDistributorParameter = Mapper.Map<SchemeLoanDistributorParameter>(_schemeLoanDistributorParameterViewModel);
                SchemeLoanDistributorParameterMakerChecker schemeLoanDistributorParameterMakerChecker = Mapper.Map<SchemeLoanDistributorParameterMakerChecker>(_schemeLoanDistributorParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanDistributorParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanDistributorParameters.Attach(schemeLoanDistributorParameter);
                    context.Entry(schemeLoanDistributorParameter).State = entityState;
                    scheme.SchemeLoanDistributorParameters.Add(schemeLoanDistributorParameter);

                    context.SchemeLoanDistributorParameterMakerCheckers.Attach(schemeLoanDistributorParameterMakerChecker);
                    context.Entry(schemeLoanDistributorParameterMakerChecker).State = EntityState.Added;
                    schemeLoanDistributorParameter.SchemeLoanDistributorParameterMakerCheckers.Add(schemeLoanDistributorParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanDistributorParameterMakerCheckers.Attach(schemeLoanDistributorParameterMakerChecker);
                    context.Entry(schemeLoanDistributorParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanArrearParameterData(SchemeLoanArrearParameterViewModel _schemeLoanArrearParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanArrearParameterViewModel, _entryType);

                SchemeLoanArrearParameter schemeLoanArrearParameter = Mapper.Map<SchemeLoanArrearParameter>(_schemeLoanArrearParameterViewModel);
                SchemeLoanArrearParameterMakerChecker schemeLoanArrearParameterMakerChecker = Mapper.Map<SchemeLoanArrearParameterMakerChecker>(_schemeLoanArrearParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanArrearParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanArrearParameters.Attach(schemeLoanArrearParameter);
                    context.Entry(schemeLoanArrearParameter).State = entityState;
                    scheme.SchemeLoanArrearParameters.Add(schemeLoanArrearParameter);

                    context.SchemeLoanArrearParameterMakerCheckers.Attach(schemeLoanArrearParameterMakerChecker);
                    context.Entry(schemeLoanArrearParameterMakerChecker).State = EntityState.Added;
                    schemeLoanArrearParameter.SchemeLoanArrearParameterMakerCheckers.Add(schemeLoanArrearParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanArrearParameterMakerCheckers.Attach(schemeLoanArrearParameterMakerChecker);
                    context.Entry(schemeLoanArrearParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanChargesParameterData(SchemeLoanChargesParameterViewModel _schemeLoanChargesParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanChargesParameterViewModel, _entryType);

                SchemeLoanChargesParameter schemeLoanChargesParameter = Mapper.Map<SchemeLoanChargesParameter>(_schemeLoanChargesParameterViewModel);
                SchemeLoanChargesParameterMakerChecker schemeLoanChargesParameterMakerChecker = Mapper.Map<SchemeLoanChargesParameterMakerChecker>(_schemeLoanChargesParameterViewModel);

                //Get PrmKey By Id
                schemeLoanChargesParameter.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_schemeLoanChargesParameterViewModel.GeneralLedgerId);
                schemeLoanChargesParameter.LendingChargesBasePrmKey = accountDetailRepository.GetLendingChargesBasePrmKeyById(_schemeLoanChargesParameterViewModel.LendingChargesBaseId);
                schemeLoanChargesParameter.ChargesTypePrmKey = accountDetailRepository.GetChargesTypePrmKeyById(_schemeLoanChargesParameterViewModel.ChargesTypeId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeLoanChargesParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanChargesParameters.Attach(schemeLoanChargesParameter);
                    context.Entry(schemeLoanChargesParameter).State = EntityState.Added;
                    scheme.SchemeLoanChargesParameters.Add(schemeLoanChargesParameter);

                    context.SchemeLoanChargesParameterMakerCheckers.Attach(schemeLoanChargesParameterMakerChecker);
                    context.Entry(schemeLoanChargesParameterMakerChecker).State = EntityState.Added;
                    schemeLoanChargesParameter.SchemeLoanChargesParameterMakerCheckers.Add(schemeLoanChargesParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanChargesParameterMakerCheckers.Attach(schemeLoanChargesParameterMakerChecker);
                    context.Entry(schemeLoanChargesParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanInterestRebateParameterData(SchemeLoanInterestRebateParameterViewModel _schemeLoanInterestRebateParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanInterestRebateParameterViewModel, _entryType);

                SchemeLoanInterestRebateParameter schemeLoanInterestRebateParameter = Mapper.Map<SchemeLoanInterestRebateParameter>(_schemeLoanInterestRebateParameterViewModel);
                SchemeLoanInterestRebateParameterMakerChecker schemeLoanInterestRebateParameterMakerChecker = Mapper.Map<SchemeLoanInterestRebateParameterMakerChecker>(_schemeLoanInterestRebateParameterViewModel);

                //Get PrmKey By ID
                schemeLoanInterestRebateParameter.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_schemeLoanInterestRebateParameterViewModel.GeneralLedgerId);
                schemeLoanInterestRebateParameter.InterestRebateCriteriaPrmKey = accountDetailRepository.GetInterestRebateCriteriaPrmKeyById(_schemeLoanInterestRebateParameterViewModel.InterestRebateCriteriaId);
                schemeLoanInterestRebateParameter.InterestRebateApplyFrequencyPrmKey = accountDetailRepository.GetInterestRebateApplyFrequencyPrmKeyById(_schemeLoanInterestRebateParameterViewModel.InterestRebateApplyFrequencyId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanInterestRebateParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanInterestRebateParameters.Attach(schemeLoanInterestRebateParameter);
                    context.Entry(schemeLoanInterestRebateParameter).State = entityState;
                    scheme.SchemeLoanInterestRebateParameters.Add(schemeLoanInterestRebateParameter);

                    context.SchemeLoanInterestRebateParameterMakerCheckers.Attach(schemeLoanInterestRebateParameterMakerChecker);
                    context.Entry(schemeLoanInterestRebateParameterMakerChecker).State = EntityState.Added;
                    schemeLoanInterestRebateParameter.SchemeLoanInterestRebateParameterMakerCheckers.Add(schemeLoanInterestRebateParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanInterestRebateParameterMakerCheckers.Attach(schemeLoanInterestRebateParameterMakerChecker);
                    context.Entry(schemeLoanInterestRebateParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanInstallmentParameterData(SchemeLoanInstallmentParameterViewModel _schemeLoanInstallmentParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanInstallmentParameterViewModel, _entryType);

                SchemeLoanInstallmentParameter schemeLoanInstallmentParameter = Mapper.Map<SchemeLoanInstallmentParameter>(_schemeLoanInstallmentParameterViewModel);
                SchemeLoanInstallmentParameterMakerChecker schemeLoanInstallmentParameterMakerChecker = Mapper.Map<SchemeLoanInstallmentParameterMakerChecker>(_schemeLoanInstallmentParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanInstallmentParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanInstallmentParameters.Attach(schemeLoanInstallmentParameter);
                    context.Entry(schemeLoanInstallmentParameter).State = entityState;
                    scheme.SchemeLoanInstallmentParameters.Add(schemeLoanInstallmentParameter);

                    context.SchemeLoanInstallmentParameterMakerCheckers.Attach(schemeLoanInstallmentParameterMakerChecker);
                    context.Entry(schemeLoanInstallmentParameterMakerChecker).State = EntityState.Added;
                    schemeLoanInstallmentParameter.SchemeLoanInstallmentParameterMakerCheckers.Add(schemeLoanInstallmentParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanInstallmentParameterMakerCheckers.Attach(schemeLoanInstallmentParameterMakerChecker);
                    context.Entry(schemeLoanInstallmentParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanFunderParameterData(SchemeLoanFunderParameterViewModel _schemeLoanFunderParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanFunderParameterViewModel, _entryType);

                SchemeLoanFunderParameter schemeLoanFunderParameter = Mapper.Map<SchemeLoanFunderParameter>(_schemeLoanFunderParameterViewModel);
                SchemeLoanFunderParameterMakerChecker schemeLoanFunderParameterMakerChecker = Mapper.Map<SchemeLoanFunderParameterMakerChecker>(_schemeLoanFunderParameterViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanFunderParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanFunderParameters.Attach(schemeLoanFunderParameter);
                    context.Entry(schemeLoanFunderParameter).State = entityState;
                    scheme.SchemeLoanFunderParameters.Add(schemeLoanFunderParameter);

                    context.SchemeLoanFunderParameterMakerCheckers.Attach(schemeLoanFunderParameterMakerChecker);
                    context.Entry(schemeLoanFunderParameterMakerChecker).State = EntityState.Added;
                    schemeLoanFunderParameter.SchemeLoanFunderParameterMakerCheckers.Add(schemeLoanFunderParameterMakerChecker);
                }
                else
                {
                    context.SchemeLoanFunderParameterMakerCheckers.Attach(schemeLoanFunderParameterMakerChecker);
                    context.Entry(schemeLoanFunderParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanOverduesActionData(SchemeLoanOverduesActionViewModel _schemeLoanOverduesActionViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanOverduesActionViewModel, _entryType);

                SchemeLoanOverduesAction schemeLoanOverduesAction = Mapper.Map<SchemeLoanOverduesAction>(_schemeLoanOverduesActionViewModel);
                SchemeLoanOverduesActionMakerChecker schemeLoanOverduesActionMakerChecker = Mapper.Map<SchemeLoanOverduesActionMakerChecker>(_schemeLoanOverduesActionViewModel);

                //Get PrmKey By Id
                schemeLoanOverduesAction.LoanRecoveryActionPrmKey = accountDetailRepository.GetLoanRecoveryActionPrmKeyById(_schemeLoanOverduesActionViewModel.LoanRecoveryActionId);

                if (_entryType == StringLiteralValue.Create)
                {
                    schemeLoanOverduesAction.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanOverduesActions.Attach(schemeLoanOverduesAction);
                    context.Entry(schemeLoanOverduesAction).State = EntityState.Added;
                    scheme.SchemeLoanOverduesActions.Add(schemeLoanOverduesAction);

                    context.SchemeLoanOverduesActionMakerCheckers.Attach(schemeLoanOverduesActionMakerChecker);
                    context.Entry(schemeLoanOverduesActionMakerChecker).State = EntityState.Added;
                    schemeLoanOverduesAction.SchemeLoanOverduesActionMakerCheckers.Add(schemeLoanOverduesActionMakerChecker);
                }
                else
                {
                    context.SchemeLoanOverduesActionMakerCheckers.Attach(schemeLoanOverduesActionMakerChecker);
                    context.Entry(schemeLoanOverduesActionMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemePreFullPaymentParameterData(SchemeLoanPreFullPaymentParameterViewModel _schemePreFullPaymentParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemePreFullPaymentParameterViewModel, _entryType);

                SchemeLoanPreFullPaymentParameter schemePreFullPaymentParameter = Mapper.Map<SchemeLoanPreFullPaymentParameter>(_schemePreFullPaymentParameterViewModel);
                SchemeLoanPreFullPaymentParameterMakerChecker schemePreFullPaymentParameterMakerChecker = Mapper.Map<SchemeLoanPreFullPaymentParameterMakerChecker>(_schemePreFullPaymentParameterViewModel);

                //Set Default value
                schemePreFullPaymentParameter.PreFullPaymentPenaltyCalculationMethodPrmKey = accountDetailRepository.GetInterestMethodPrmKeyById(_schemePreFullPaymentParameterViewModel.InterestMethodId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemePreFullPaymentParameter.SchemePrmKey = schemePrmKey;

                    context.SchemePreFullPaymentParameters.Attach(schemePreFullPaymentParameter);
                    context.Entry(schemePreFullPaymentParameter).State = entityState;
                    scheme.SchemePreFullPaymentParameters.Add(schemePreFullPaymentParameter);

                    context.SchemePreFullPaymentParameterMakerCheckers.Attach(schemePreFullPaymentParameterMakerChecker);
                    context.Entry(schemePreFullPaymentParameterMakerChecker).State = EntityState.Added;
                    schemePreFullPaymentParameter.SchemePreFullPaymentParameterMakerCheckers.Add(schemePreFullPaymentParameterMakerChecker);
                }
                else
                {
                    context.SchemePreFullPaymentParameterMakerCheckers.Attach(schemePreFullPaymentParameterMakerChecker);
                    context.Entry(schemePreFullPaymentParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemePrePartPaymentParameterData(SchemeLoanPrePartPaymentParameterViewModel _schemePrePartPaymentParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemePrePartPaymentParameterViewModel, _entryType);

                SchemeLoanPrePartPaymentParameter schemePrePartPaymentParameter = Mapper.Map<SchemeLoanPrePartPaymentParameter>(_schemePrePartPaymentParameterViewModel);
                SchemeLoanPrePartPaymentParameterMakerChecker schemePrePartPaymentParameterMakerChecker = Mapper.Map<SchemeLoanPrePartPaymentParameterMakerChecker>(_schemePrePartPaymentParameterViewModel);

                //Get PrmKey By Id
                schemePrePartPaymentParameter.PrePartPaymentPenaltyCalculationMethodPrmKey = accountDetailRepository.GetInterestMethodPrmKeyById(_schemePrePartPaymentParameterViewModel.InterestMethodId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemePrePartPaymentParameter.SchemePrmKey = schemePrmKey;

                    context.SchemePrePartPaymentParameters.Attach(schemePrePartPaymentParameter);
                    context.Entry(schemePrePartPaymentParameter).State = entityState;
                    scheme.SchemePrePartPaymentParameters.Add(schemePrePartPaymentParameter);

                    context.SchemePrePartPaymentParameterMakerCheckers.Attach(schemePrePartPaymentParameterMakerChecker);
                    context.Entry(schemePrePartPaymentParameterMakerChecker).State = EntityState.Added;
                    schemePrePartPaymentParameter.SchemePrePartPaymentParameterMakerCheckers.Add(schemePrePartPaymentParameterMakerChecker);
                }
                else
                {
                    context.SchemePrePartPaymentParameterMakerCheckers.Attach(schemePrePartPaymentParameterMakerChecker);
                    context.Entry(schemePrePartPaymentParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeLoanAgreementNumberData(SchemeLoanAgreementNumberViewModel _schemeLoanAgreementNumberViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeLoanAgreementNumberViewModel, _entryType);

                SchemeLoanAgreementNumber schemeLoanAgreementNumber = Mapper.Map<SchemeLoanAgreementNumber>(_schemeLoanAgreementNumberViewModel);
                SchemeLoanAgreementNumberMakerChecker schemeLoanAgreementNumberMakerChecker = Mapper.Map<SchemeLoanAgreementNumberMakerChecker>(_schemeLoanAgreementNumberViewModel);

                // Multi Select Value For Dropdown 
                if (_schemeLoanAgreementNumberViewModel.EnableAutoAgreementNumber == false)
                {
                    schemeLoanAgreementNumber.AgreementNumberMask = "None";
                }
                else
                {
                    schemeLoanAgreementNumber.AgreementNumberMask = string.Join(",", _schemeLoanAgreementNumberViewModel.MaskTypeCharacterForAgreement);
                }

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeLoanAgreementNumber.SchemePrmKey = schemePrmKey;

                    context.SchemeLoanAgreementNumbers.Attach(schemeLoanAgreementNumber);
                    context.Entry(schemeLoanAgreementNumber).State = entityState;
                    scheme.SchemeLoanAgreementNumbers.Add(schemeLoanAgreementNumber);

                    context.SchemeLoanAgreementNumberMakerCheckers.Attach(schemeLoanAgreementNumberMakerChecker);
                    context.Entry(schemeLoanAgreementNumberMakerChecker).State = EntityState.Added;
                    schemeLoanAgreementNumber.SchemeLoanAgreementNumberMakerCheckers.Add(schemeLoanAgreementNumberMakerChecker);
                }
                else
                {
                    context.SchemeLoanAgreementNumberMakerCheckers.Attach(schemeLoanAgreementNumberMakerChecker);
                    context.Entry(schemeLoanAgreementNumberMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSchemeGoldLoanParameterData(SchemeGoldLoanParameterViewModel _schemeGoldLoanParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_schemeGoldLoanParameterViewModel, _entryType);

                SchemeGoldLoanParameter schemeGoldLoanParameter = Mapper.Map<SchemeGoldLoanParameter>(_schemeGoldLoanParameterViewModel);
                SchemeGoldLoanParameterMakerChecker schemeGoldLoanParameterMakerChecker = Mapper.Map<SchemeGoldLoanParameterMakerChecker>(_schemeGoldLoanParameterViewModel);

                // Multi Select Value For Dropdown 
                if (_schemeGoldLoanParameterViewModel.EnableGoldPhoto == false)
                {
                    schemeGoldLoanParameter.GoldPhotoUpload = StringLiteralValue.Disable;
                    schemeGoldLoanParameter.GoldPhotoAllowedFileFormatsForDb = "None";
                    schemeGoldLoanParameter.GoldPhotoAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    if (_schemeGoldLoanParameterViewModel.EnableGoldPhotoUploadInDb == false)
                    {
                        schemeGoldLoanParameter.GoldPhotoAllowedFileFormatsForDb = "None";
                        schemeGoldLoanParameter.GoldPhotoAllowedFileFormatsForLocalStorage = string.Join(",", _schemeGoldLoanParameterViewModel.GoldPhotoAllowedFileFormatIdForLocalStorage);
                    }
                    else
                    {
                        schemeGoldLoanParameter.GoldPhotoAllowedFileFormatsForLocalStorage = "None";
                        schemeGoldLoanParameter.GoldPhotoAllowedFileFormatsForDb = string.Join(",", _schemeGoldLoanParameterViewModel.GoldPhotoAllowedFileFormatIdForDb);
                    }
                }

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    schemeGoldLoanParameter.SchemePrmKey = schemePrmKey;

                    context.SchemeGoldLoanParameters.Attach(schemeGoldLoanParameter);
                    context.Entry(schemeGoldLoanParameter).State = entityState;
                    scheme.SchemeGoldLoanParameters.Add(schemeGoldLoanParameter);

                    context.SchemeGoldLoanParameterMakerCheckers.Attach(schemeGoldLoanParameterMakerChecker);
                    context.Entry(schemeGoldLoanParameterMakerChecker).State = EntityState.Added;
                    schemeGoldLoanParameter.SchemeGoldLoanParameterMakerCheckers.Add(schemeGoldLoanParameterMakerChecker);
                }
                else
                {
                    context.SchemeGoldLoanParameterMakerCheckers.Attach(schemeGoldLoanParameterMakerChecker);
                    context.Entry(schemeGoldLoanParameterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public async Task<bool> SaveData()
        {
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }
    }
}
