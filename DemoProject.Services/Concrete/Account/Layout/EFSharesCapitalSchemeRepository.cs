using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Layout
{
    public class EFSharesCapitalSchemeRepository : ISharesCapitalSchemeRepository
    {
        private readonly EFDbContext context;
        private readonly ISharesCapitalSchemeParameterRepository sharesCapitalSchemeParameterRepository;
        private readonly ISchemeDbContextRepository schemeDbContextRepository;
        private readonly ISchemeDetailRepository schemeDetailRepository;

        public EFSharesCapitalSchemeRepository(RepositoryConnection _connection, ISharesCapitalSchemeParameterRepository _sharesCapitalSchemeParameterRepository, ISchemeDbContextRepository _schemeDbContextRepository, ISchemeDetailRepository _sharesCapitalSchemeDetailRepository)
        {
            context = _connection.EFDbContext;
            sharesCapitalSchemeParameterRepository = _sharesCapitalSchemeParameterRepository;
            schemeDbContextRepository = _schemeDbContextRepository;
            schemeDetailRepository = _sharesCapitalSchemeDetailRepository;
        }

        public async Task<bool> Amend(SharesCapitalSchemeViewModel _sharesCapitalSchemeViewModel)
        {
            try
            {
                SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetActiveEntry();

                bool result;

                result = schemeDbContextRepository.AttachSharesCapitalSchemeData(_sharesCapitalSchemeViewModel, StringLiteralValue.Amend);

                // SchemeAccountParameter
                result = schemeDbContextRepository.AttachSchemeAccountParameterData(_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel, StringLiteralValue.Amend);

                // SchemeSharesCapitalAccountParameter
                result = schemeDbContextRepository.AttachSchemeSharesCapitalAccountParameterData(_sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel, StringLiteralValue.Amend);

                // SchemeCustomerAccountNumber
                result = schemeDbContextRepository.AttachSchemeCustomerAccountNumberData(_sharesCapitalSchemeViewModel.SchemeCustomerAccountNumberViewModel, _sharesCapitalSchemeViewModel.SchemeCustomerAccountNumberViewModel.SchemeCustomerAccountNumberPrmKey > 0 ? StringLiteralValue.Amend : StringLiteralValue.Create);

                // SchemeSharesCertificateParameter
                result = schemeDbContextRepository.AttachSchemeSharesCertificateParameterData(_sharesCapitalSchemeViewModel.SchemeSharesCertificateParameterViewModel, _sharesCapitalSchemeViewModel.SchemeSharesCertificateParameterViewModel.SchemeSharesCertificateParameterPrmKey > 0 ? StringLiteralValue.Amend : StringLiteralValue.Create);

                // SchemeApplicationParameter 
                // On Create Enable Or Disable On Amend Then Required To Delete Entry
                if (_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableApplication == true)
                {
                    result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_sharesCapitalSchemeViewModel.SchemeApplicationParameterViewModel, _sharesCapitalSchemeViewModel.SchemeApplicationParameterViewModel.SchemeApplicationParameterPrmKey > 0 ? StringLiteralValue.Amend : StringLiteralValue.Create);
                }
                else 
                    if(_sharesCapitalSchemeViewModel.SchemeApplicationParameterViewModel.SchemeApplicationParameterPrmKey > 0)
                        result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_sharesCapitalSchemeViewModel.SchemeApplicationParameterViewModel, StringLiteralValue.Delete);

                // SchemeAccountBankingChannelParameter 
                // On Create Enable Or Disable On Amend Then Required To Delete Entry
                if (_sharesCapitalSchemeViewModel.SchemeAccountBankingChannelParameterViewModel.EnableBankingChannelParameter == true)
                    result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_sharesCapitalSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, _sharesCapitalSchemeViewModel.SchemeAccountBankingChannelParameterViewModel.SchemeAccountBankingChannelParameterPrmKey > 0 ? StringLiteralValue.Amend : StringLiteralValue.Create);
                else
                {
                    if(_sharesCapitalSchemeViewModel.SchemeAccountBankingChannelParameterViewModel.SchemeAccountBankingChannelParameterPrmKey > 0)
                        result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_sharesCapitalSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, StringLiteralValue.Delete);
                }

                // SchemeSharesCapitalDividendParameter 
                // On Create Enable Or Disable On Amend Then Required To Delete Entry
                if (_sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel.EnableDividend == true)
                {
                    result = schemeDbContextRepository.AttachSchemeSharesCapitalDividendParameterData(_sharesCapitalSchemeViewModel.SchemeSharesCapitalDividendParameterViewModel, _sharesCapitalSchemeViewModel.SchemeSharesCapitalDividendParameterViewModel.SchemeSharesCapitalDividendParameterPrmKey > 0 ? StringLiteralValue.Amend : StringLiteralValue.Create);
                }
                else
                {
                    if (_sharesCapitalSchemeViewModel.SchemeSharesCapitalDividendParameterViewModel.SchemeSharesCapitalDividendParameterPrmKey > 0)
                    {
                        // Set Default Value
                        result = schemeDbContextRepository.AttachSchemeSharesCapitalDividendParameterData(_sharesCapitalSchemeViewModel.SchemeSharesCapitalDividendParameterViewModel, StringLiteralValue.Delete);
                    }
                }

                // SchemeClosingCharges 
                // Old Record Amended For Amened 
                IEnumerable<SchemeClosingChargesViewModel> schemeschemeClosingChargesViewModelListForAmend = await schemeDetailRepository.GetClosingChargesEntries(_sharesCapitalSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                if (schemeschemeClosingChargesViewModelListForAmend != null)
                {
                    foreach (SchemeClosingChargesViewModel viewModel in schemeschemeClosingChargesViewModelListForAmend)
                    {
                        result = schemeDbContextRepository.AttachSchemeClosingChargesData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Record Create For Amened 
                if (_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableClosingCharges == true)
                {
                    List<SchemeClosingChargesViewModel> schemeschemeClosingChargesViewModelList = new List<SchemeClosingChargesViewModel>();
                    schemeschemeClosingChargesViewModelList = (List<SchemeClosingChargesViewModel>)HttpContext.Current.Session["SchemeClosingCharges"];

                    if (schemeschemeClosingChargesViewModelList != null)
                    {
                        foreach (SchemeClosingChargesViewModel viewModel in schemeschemeClosingChargesViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeClosingChargesData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // SchemeSharesTransferCharges 
                // Old Record Amended For Amened 
                IEnumerable<SchemeSharesTransferChargesViewModel> schemeSharesTransferChargesViewModelListForAmend = await schemeDetailRepository.GetSharesTransferChargesEntries(_sharesCapitalSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                if (schemeSharesTransferChargesViewModelListForAmend != null)
                {
                    foreach (SchemeSharesTransferChargesViewModel viewModel in schemeSharesTransferChargesViewModelListForAmend)
                    {
                        result = schemeDbContextRepository.AttachSchemeSharesTransferChargesData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Record Create For Amened 
                if (_sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel.EnableSharesTransferCharges == true)
                {
                    List<SchemeSharesTransferChargesViewModel> schemeSharesTransferChargesViewModelList = new List<SchemeSharesTransferChargesViewModel>();
                    schemeSharesTransferChargesViewModelList = (List<SchemeSharesTransferChargesViewModel>)HttpContext.Current.Session["SchemeSharesTransferCharges"];

                    if (schemeSharesTransferChargesViewModelList != null)
                    {
                        foreach (SchemeSharesTransferChargesViewModel viewModel in schemeSharesTransferChargesViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeSharesTransferChargesData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // SchemeNoticeSchedule
                // Old Record Amended For Amened 
                IEnumerable<SchemeNoticeScheduleViewModel> schemeNoticeScheduleViewModelListForAmend = await schemeDetailRepository.GetNoticeScheduleEntries(_sharesCapitalSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                if (schemeNoticeScheduleViewModelListForAmend.Count() > 0)
                {
                    foreach (SchemeNoticeScheduleViewModel viewModel in schemeNoticeScheduleViewModelListForAmend)
                    {
                        result = schemeDbContextRepository.AttachSchemeNoticeScheduleData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Record Create For Amened 
                if (_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableSmsService == true || _sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                {
                    List<SchemeNoticeScheduleViewModel> schemeNoticeScheduleViewModels = new List<SchemeNoticeScheduleViewModel>();
                    schemeNoticeScheduleViewModels = (List<SchemeNoticeScheduleViewModel>)HttpContext.Current.Session["SchemeNoticeSchedule"];

                    if (schemeNoticeScheduleViewModels != null)
                    {
                        foreach (SchemeNoticeScheduleViewModel viewModel in schemeNoticeScheduleViewModels)
                        {
                            result = schemeDbContextRepository.AttachSchemeNoticeScheduleData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // SchemeReportFormat
                // Old Record Amended For Amened 
                IEnumerable<SchemeReportFormatViewModel> schemeReportFormatViewModelListForAmend = await schemeDetailRepository.GetReportFormatEntries(_sharesCapitalSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                if (schemeReportFormatViewModelListForAmend != null)
                {
                    foreach (SchemeReportFormatViewModel viewModel in schemeReportFormatViewModelListForAmend)
                    {
                        result = schemeDbContextRepository.AttachSchemeReportFormatData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Record Create For Amened 
                if (sharesCapitalSchemeParameterViewModel.EnableReportFormatParameter == true)
                {
                    List<SchemeReportFormatViewModel> schemeReportFormatViewModelList = new List<SchemeReportFormatViewModel>();
                    schemeReportFormatViewModelList = (List<SchemeReportFormatViewModel>)HttpContext.Current.Session["SchemeReportFormat"];

                    if (schemeReportFormatViewModelList != null)
                    {
                        foreach (SchemeReportFormatViewModel viewModel in schemeReportFormatViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeReportFormatData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // SchemeEstimateTarget
                // On Create Enable Or Disable On Amend Then Required To Delete Entry
                if (sharesCapitalSchemeParameterViewModel.EnableTargetEstimationParameter == true)
                {
                    result = schemeDbContextRepository.AttachSchemeEstimateTargetData(_sharesCapitalSchemeViewModel.SchemeEstimateTargetViewModel, _sharesCapitalSchemeViewModel.SchemeEstimateTargetViewModel.SchemeEstimateTargetPrmKey > 0 ? StringLiteralValue.Amend : StringLiteralValue.Create);
                }
                else
                {
                    if(_sharesCapitalSchemeViewModel.SchemeEstimateTargetViewModel.SchemeEstimateTargetPrmKey > 0)
                        result = schemeDbContextRepository.AttachSchemeEstimateTargetData(_sharesCapitalSchemeViewModel.SchemeEstimateTargetViewModel, StringLiteralValue.Delete);
                }

                // SchemeLimit
                // On Create Enable Or Disable On Amend Then Required To Delete Entry
                if (sharesCapitalSchemeParameterViewModel.EnableLimitParameter == true)
                {
                    result = schemeDbContextRepository.AttachSchemeLimitData(_sharesCapitalSchemeViewModel.SchemeLimitViewModel, _sharesCapitalSchemeViewModel.SchemeLimitViewModel.SchemeLimitPrmKey > 0 ?  StringLiteralValue.Amend : StringLiteralValue.Create);
                }
                else
                {
                    if(_sharesCapitalSchemeViewModel.SchemeLimitViewModel.SchemeLimitPrmKey > 0)
                        result = schemeDbContextRepository.AttachSchemeLimitData(_sharesCapitalSchemeViewModel.SchemeLimitViewModel, StringLiteralValue.Delete);
                }

                //SchemeGeneralLedger
                // Old Record Amended For Amened 
                IEnumerable<SchemeGeneralLedgerViewModel> schemeGeneralLedgerViewModelListForAmend = await schemeDetailRepository.GetGeneralLedgerEntries(_sharesCapitalSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                if (schemeGeneralLedgerViewModelListForAmend != null)
                {
                    foreach (SchemeGeneralLedgerViewModel viewModel in schemeGeneralLedgerViewModelListForAmend)
                    {
                        result = schemeDbContextRepository.AttachSchemeGeneralLedgerData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Record
                List<SchemeGeneralLedgerViewModel> schemeGeneralLedgerViewModelList = new List<SchemeGeneralLedgerViewModel>();
                schemeGeneralLedgerViewModelList = (List<SchemeGeneralLedgerViewModel>)HttpContext.Current.Session["SchemeGeneralLedger"];

                if (schemeGeneralLedgerViewModelList != null)
                {
                    foreach (SchemeGeneralLedgerViewModel viewModel in schemeGeneralLedgerViewModelList)
                    {
                        result = schemeDbContextRepository.AttachSchemeGeneralLedgerData(viewModel, StringLiteralValue.Create);
                    }
                }

                //SchemeBusinessOffice
                // Old Record Amended For Amened 
                IEnumerable<SchemeBusinessOfficeViewModel> schemeBusinessOfficeViewModelListForAmend = await schemeDetailRepository.GetBusinessOfficeEntries(_sharesCapitalSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                if (schemeBusinessOfficeViewModelListForAmend != null)
                {
                    foreach (SchemeBusinessOfficeViewModel viewModel in schemeBusinessOfficeViewModelListForAmend)
                    {
                        result = schemeDbContextRepository.AttachSchemeBusinessOfficeData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Record
                List<SchemeBusinessOfficeViewModel> schemeBusinessOfficeViewModelList = new List<SchemeBusinessOfficeViewModel>();
                schemeBusinessOfficeViewModelList = (List<SchemeBusinessOfficeViewModel>)HttpContext.Current.Session["SchemeBusinessOffice"];

                if (schemeBusinessOfficeViewModelList != null)
                {
                    foreach (SchemeBusinessOfficeViewModel viewModel in schemeBusinessOfficeViewModelList)
                    {
                        result = schemeDbContextRepository.AttachSchemeBusinessOfficeData(viewModel, StringLiteralValue.Create);
                    }
                }

                result = await schemeDbContextRepository.SaveData();

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

        public async Task<IEnumerable<SharesCapitalSchemeIndexViewModel>> GetSharesCapitalSchemeIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SharesCapitalSchemeIndexViewModel>("SELECT * FROM dbo.GetSharesCapitalSchemeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> GetSessionValues(short _schemePrmKey, string _entryType)
        {
            try
            {
                HttpContext.Current.Session["SchemeBusinessOffice"] = await schemeDetailRepository.GetBusinessOfficeEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeGeneralLedger"] = await schemeDetailRepository.GetGeneralLedgerEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeNoticeSchedule"] = await schemeDetailRepository.GetNoticeScheduleEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeReportFormat"] = await schemeDetailRepository.GetReportFormatEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeClosingCharges"] = await schemeDetailRepository.GetClosingChargesEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeSharesTransferCharges"] = await schemeDetailRepository.GetSharesTransferChargesEntries(_schemePrmKey, _entryType);
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<SharesCapitalSchemeViewModel> GetSharesCapitalSchemeEntry(Guid _SchemeId, string _entryType)
        {
            try
            {
                SharesCapitalSchemeViewModel sharesCapitalSchemeViewModel = await context.Database.SqlQuery<SharesCapitalSchemeViewModel>("SELECT * FROM dbo.GetSharesCapitalSchemeEntry (@SchemeId, @EntryType)", new SqlParameter("@SchemeId", _SchemeId), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel = await schemeDetailRepository.GetAccountParameterEntry(sharesCapitalSchemeViewModel.SchemePrmKey, _entryType);
                sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel = await schemeDetailRepository.GetSharesCapitalAccountParameterEntry(sharesCapitalSchemeViewModel.SchemePrmKey, _entryType);
                sharesCapitalSchemeViewModel.SchemeCustomerAccountNumberViewModel = await schemeDetailRepository.GetCustomerAccountNumberEntry(sharesCapitalSchemeViewModel.SchemePrmKey, _entryType);
                sharesCapitalSchemeViewModel.SchemeApplicationParameterViewModel = await schemeDetailRepository.GetApplicationParameterEntry(sharesCapitalSchemeViewModel.SchemePrmKey, _entryType);
                sharesCapitalSchemeViewModel.SchemeAccountBankingChannelParameterViewModel = await schemeDetailRepository.GetAccountBankingChannelParameterEntry(sharesCapitalSchemeViewModel.SchemePrmKey, _entryType);
                sharesCapitalSchemeViewModel.SchemeSharesCertificateParameterViewModel = await schemeDetailRepository.GetSharesCertificateParameterEntry(sharesCapitalSchemeViewModel.SchemePrmKey, _entryType);
                sharesCapitalSchemeViewModel.SchemeSharesCapitalDividendParameterViewModel = await schemeDetailRepository.GetSharesCapitalDividendParameterEntry(sharesCapitalSchemeViewModel.SchemePrmKey, _entryType);
                sharesCapitalSchemeViewModel.SchemeEstimateTargetViewModel = await schemeDetailRepository.GetEstimateTargetEntry(sharesCapitalSchemeViewModel.SchemePrmKey, _entryType);
                sharesCapitalSchemeViewModel.SchemeLimitViewModel = await schemeDetailRepository.GetLimitEntry(sharesCapitalSchemeViewModel.SchemePrmKey, _entryType);

                return sharesCapitalSchemeViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool IsUniqueSchemeName(string _nameOfScheme)
        {
            bool status;
            if (context.Schemes.Where(p => p.NameOfScheme == _nameOfScheme).Select(p => p.PrmKey).FirstOrDefault() > 0)
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

       
        public async Task<bool> Save(SharesCapitalSchemeViewModel _sharesCapitalSchemeViewModel)
        {
            try
            {
                SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetActiveEntry();

                bool result;

                result = schemeDbContextRepository.AttachSharesCapitalSchemeData(_sharesCapitalSchemeViewModel, StringLiteralValue.Create);

                if(result)
                    result = schemeDbContextRepository.AttachSchemeAccountParameterData(_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel, StringLiteralValue.Create);

                if (result)
                    result = schemeDbContextRepository.AttachSchemeSharesCapitalAccountParameterData(_sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel, StringLiteralValue.Create);

                if (result)
                    result = schemeDbContextRepository.AttachSchemeCustomerAccountNumberData(_sharesCapitalSchemeViewModel.SchemeCustomerAccountNumberViewModel, StringLiteralValue.Create);

                if (result)
                    result = schemeDbContextRepository.AttachSchemeSharesCertificateParameterData(_sharesCapitalSchemeViewModel.SchemeSharesCertificateParameterViewModel, StringLiteralValue.Create);

                if (result)
                {
                    if (_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableApplication == true)
                    {
                        result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_sharesCapitalSchemeViewModel.SchemeApplicationParameterViewModel, StringLiteralValue.Create);
                    }
                }

                if (result)
                {
                    if (sharesCapitalSchemeParameterViewModel.EnableBankingChannelParameter == true)
                    {
                        result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_sharesCapitalSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, StringLiteralValue.Create);
                    }
                }

                if (result)
                {
                    if (_sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel.EnableDividend == true)
                    {
                        result = schemeDbContextRepository.AttachSchemeSharesCapitalDividendParameterData(_sharesCapitalSchemeViewModel.SchemeSharesCapitalDividendParameterViewModel, StringLiteralValue.Create);
                    }
                }

                if (result)
                {
                    if (_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableClosingCharges == true)
                    {
                        List<SchemeClosingChargesViewModel> schemeschemeClosingChargesViewModelList = new List<SchemeClosingChargesViewModel>();
                        schemeschemeClosingChargesViewModelList = (List<SchemeClosingChargesViewModel>)HttpContext.Current.Session["SchemeClosingCharges"];

                        if (schemeschemeClosingChargesViewModelList != null)
                        {
                            foreach (SchemeClosingChargesViewModel viewModel in schemeschemeClosingChargesViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeClosingChargesData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                if (result)
                {
                    if (_sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel.EnableSharesTransferCharges == true)
                    {
                        List<SchemeSharesTransferChargesViewModel> schemeSharesTransferChargesViewModelList = new List<SchemeSharesTransferChargesViewModel>();
                        schemeSharesTransferChargesViewModelList = (List<SchemeSharesTransferChargesViewModel>)HttpContext.Current.Session["SchemeSharesTransferCharges"];

                        if (schemeSharesTransferChargesViewModelList != null)
                        {
                            foreach (SchemeSharesTransferChargesViewModel viewModel in schemeSharesTransferChargesViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeSharesTransferChargesData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                if (result)
                {
                    if (_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableSmsService == true || _sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                    {
                        // Get SchemeNoticeSchedule Details From Session Object
                        List<SchemeNoticeScheduleViewModel> schemeNoticeScheduleViewModels = new List<SchemeNoticeScheduleViewModel>();
                        schemeNoticeScheduleViewModels = (List<SchemeNoticeScheduleViewModel>)HttpContext.Current.Session["SchemeNoticeSchedule"];

                        if (schemeNoticeScheduleViewModels != null)
                        {
                            foreach (SchemeNoticeScheduleViewModel viewModel in schemeNoticeScheduleViewModels)
                            {
                                result = schemeDbContextRepository.AttachSchemeNoticeScheduleData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                if (result)
                {
                    if (sharesCapitalSchemeParameterViewModel.EnableReportFormatParameter == true)
                    {
                        // SchemeReportFormat Details From Session Object
                        List<SchemeReportFormatViewModel> schemeReportFormatViewModelList = new List<SchemeReportFormatViewModel>();
                        schemeReportFormatViewModelList = (List<SchemeReportFormatViewModel>)HttpContext.Current.Session["SchemeReportFormat"];

                        if (schemeReportFormatViewModelList != null)
                        {
                            foreach (SchemeReportFormatViewModel viewModel in schemeReportFormatViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeReportFormatData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                if (result)
                {
                    if (sharesCapitalSchemeParameterViewModel.EnableTargetEstimationParameter == true)
                    {
                        result = schemeDbContextRepository.AttachSchemeEstimateTargetData(_sharesCapitalSchemeViewModel.SchemeEstimateTargetViewModel, StringLiteralValue.Create);
                    }
                }

                if (result)
                {
                    if (sharesCapitalSchemeParameterViewModel.EnableLimitParameter == true)
                    {
                        result = schemeDbContextRepository.AttachSchemeLimitData(_sharesCapitalSchemeViewModel.SchemeLimitViewModel, StringLiteralValue.Create);
                    }
                }

                if (result)
                {
                    List<SchemeGeneralLedgerViewModel> schemeGeneralLedgerViewModelList = new List<SchemeGeneralLedgerViewModel>();
                    schemeGeneralLedgerViewModelList = (List<SchemeGeneralLedgerViewModel>)HttpContext.Current.Session["SchemeGeneralLedger"];

                    if (schemeGeneralLedgerViewModelList != null)
                    {
                        foreach (SchemeGeneralLedgerViewModel viewModel in schemeGeneralLedgerViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeGeneralLedgerData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                if (result)
                {
                    List<SchemeBusinessOfficeViewModel> schemeBusinessOfficeViewModelList = new List<SchemeBusinessOfficeViewModel>();
                    schemeBusinessOfficeViewModelList = (List<SchemeBusinessOfficeViewModel>)HttpContext.Current.Session["SchemeBusinessOffice"];

                    if (schemeBusinessOfficeViewModelList != null)
                    {
                        foreach (SchemeBusinessOfficeViewModel viewModel in schemeBusinessOfficeViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeBusinessOfficeData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                if (result)
                    result = await schemeDbContextRepository.SaveData();

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

        public async Task<bool> VerifyRejectDelete(SharesCapitalSchemeViewModel _sharesCapitalSchemeViewModel,string _entryType)
        {
            try
            {
                string entriesType;
                if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
                    entriesType = StringLiteralValue.Unverified;
                else
                    entriesType = StringLiteralValue.Reject;

                SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetActiveEntry();

                bool result;

                result = schemeDbContextRepository.AttachSharesCapitalSchemeData(_sharesCapitalSchemeViewModel, _entryType);

                // SchemeAccountParameter
                SchemeAccountParameterViewModel schemeAccountParameterViewModel = await schemeDetailRepository.GetAccountParameterEntry(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);

                if (schemeAccountParameterViewModel != null)
                {
                    result = schemeDbContextRepository.AttachSchemeAccountParameterData(_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel, _entryType);
                }

                // SchemeSharesCapitalAccountParameter
                SchemeSharesCapitalAccountParameterViewModel schemeSharesCapitalAccountParameterViewModel = await schemeDetailRepository.GetSharesCapitalAccountParameterEntry(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);

                if (schemeSharesCapitalAccountParameterViewModel != null)
                {
                    result = schemeDbContextRepository.AttachSchemeSharesCapitalAccountParameterData(_sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel, _entryType);
                }

                // SchemeCustomerAccountNumber
                SchemeCustomerAccountNumberViewModel schemeCustomerAccountNumberViewModel = await schemeDetailRepository.GetCustomerAccountNumberEntry(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);
                if (schemeCustomerAccountNumberViewModel != null)
                {
                    result = schemeDbContextRepository.AttachSchemeCustomerAccountNumberData(_sharesCapitalSchemeViewModel.SchemeCustomerAccountNumberViewModel, _entryType);
                }

                // SchemeSharesCertificateParameter
                SchemeSharesCertificateParameterViewModel schemeSharesCertificateParameterViewModel = await schemeDetailRepository.GetSharesCertificateParameterEntry(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);
                if (schemeSharesCertificateParameterViewModel != null)
                {
                    result = schemeDbContextRepository.AttachSchemeSharesCertificateParameterData(_sharesCapitalSchemeViewModel.SchemeSharesCertificateParameterViewModel, _entryType);
                }

                // SchemeApplicationParameter 
                if (_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableApplication == true)
                {
                    SchemeApplicationParameterViewModel schemeApplicationParameterViewModel = await schemeDetailRepository.GetApplicationParameterEntry(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);
                    if (schemeApplicationParameterViewModel != null)
                    {
                        result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_sharesCapitalSchemeViewModel.SchemeApplicationParameterViewModel, _entryType);
                    }
                }

                // SchemeAccountBankingChannelParameter
                if (sharesCapitalSchemeParameterViewModel.EnableBankingChannelParameter == true)
                {
                    // SchemeAccountBankingChannelParameter
                    SchemeAccountBankingChannelParameterViewModel schemeAccountBankingChannelParameterViewModel = await schemeDetailRepository.GetAccountBankingChannelParameterEntry(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);
                    if (schemeAccountBankingChannelParameterViewModel != null)
                    {
                        result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_sharesCapitalSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, _entryType);
                    }
                }

                // SchemeSharesCapitalDividendParameter 
                if (_sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel.EnableDividend == true)
                {
                    // SchemeSharesCapitalDividendParameter
                    SchemeSharesCapitalDividendParameterViewModel schemeSharesCapitalDividendParameterViewModel = await schemeDetailRepository.GetSharesCapitalDividendParameterEntry(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);
                    if (schemeSharesCapitalDividendParameterViewModel != null)
                    {
                        result = schemeDbContextRepository.AttachSchemeSharesCapitalDividendParameterData(_sharesCapitalSchemeViewModel.SchemeSharesCapitalDividendParameterViewModel, _entryType);
                    }
                }

                // SchemeClosingCharges 
                if (_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableClosingCharges == true)
                {
                    IEnumerable<SchemeClosingChargesViewModel> schemeschemeClosingChargesViewModelList = await schemeDetailRepository.GetClosingChargesEntries(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeschemeClosingChargesViewModelList != null)
                    {
                        foreach (SchemeClosingChargesViewModel viewModel in schemeschemeClosingChargesViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeClosingChargesData(viewModel, _entryType);
                        }
                    }
                }

                // SchemeSharesTransferCharges 
                if (_sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel.EnableSharesTransferCharges == true)
                {
                    IEnumerable<SchemeSharesTransferChargesViewModel> schemeSharesTransferChargesViewModelList = await schemeDetailRepository.GetSharesTransferChargesEntries(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeSharesTransferChargesViewModelList != null)
                    {
                        foreach (SchemeSharesTransferChargesViewModel viewModel in schemeSharesTransferChargesViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeSharesTransferChargesData(viewModel, _entryType);
                        }
                    }
                }

                // SchemeNoticeSchedule
                if (_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableSmsService == true || _sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                {
                    // Get SchemeNoticeSchedule Details From Session Object
                    IEnumerable<SchemeNoticeScheduleViewModel> schemeNoticeScheduleViewModelList = await schemeDetailRepository.GetNoticeScheduleEntries(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeNoticeScheduleViewModelList != null)
                    {
                        foreach (SchemeNoticeScheduleViewModel viewModel in schemeNoticeScheduleViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeNoticeScheduleData(viewModel, _entryType);
                        }
                    }
                }

                // SchemeReportFormat
                if (sharesCapitalSchemeParameterViewModel.EnableReportFormatParameter == true)
                {
                    // SchemeReportFormat Details From Session Object
                    IEnumerable<SchemeReportFormatViewModel> schemeReportFormatViewModelList = await schemeDetailRepository.GetReportFormatEntries(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeReportFormatViewModelList != null)
                    {
                        foreach (SchemeReportFormatViewModel viewModel in schemeReportFormatViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeReportFormatData(viewModel, _entryType);
                        }
                    }
                }

                // SchemeEstimateTarget
                if (sharesCapitalSchemeParameterViewModel.EnableTargetEstimationParameter == true)
                {
                    SchemeEstimateTargetViewModel schemeEstimateTargetViewModel = await schemeDetailRepository.GetEstimateTargetEntry(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);
                    if (schemeEstimateTargetViewModel != null)
                    {
                        result = schemeDbContextRepository.AttachSchemeEstimateTargetData(_sharesCapitalSchemeViewModel.SchemeEstimateTargetViewModel, _entryType);
                    }
                }

                // SchemeLimit
                if (sharesCapitalSchemeParameterViewModel.EnableLimitParameter == true)
                {
                    // SchemeLimit
                    SchemeLimitViewModel schemeLimitViewModel = await schemeDetailRepository.GetLimitEntry(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);
                    if (schemeLimitViewModel != null)
                    {
                        result = schemeDbContextRepository.AttachSchemeLimitData(_sharesCapitalSchemeViewModel.SchemeLimitViewModel, _entryType);
                    }
                }

                // SchemeGeneralLedger
                IEnumerable<SchemeGeneralLedgerViewModel> schemeGeneralLedgerViewModelList = await schemeDetailRepository.GetGeneralLedgerEntries(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);

                if (schemeGeneralLedgerViewModelList != null)
                {
                    foreach (SchemeGeneralLedgerViewModel viewModel in schemeGeneralLedgerViewModelList)
                    {
                        result = schemeDbContextRepository.AttachSchemeGeneralLedgerData(viewModel, _entryType);
                    }
                }

                // SchemeBusinessOffice
                IEnumerable<SchemeBusinessOfficeViewModel> schemeBusinessOfficeViewModelList = await schemeDetailRepository.GetBusinessOfficeEntries(_sharesCapitalSchemeViewModel.SchemePrmKey, entriesType);

                if (schemeBusinessOfficeViewModelList != null)
                {
                    foreach (SchemeBusinessOfficeViewModel viewModel in schemeBusinessOfficeViewModelList)
                    {
                        result = schemeDbContextRepository.AttachSchemeBusinessOfficeData(viewModel, _entryType);
                    }
                }

                result = await schemeDbContextRepository.SaveData();

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
