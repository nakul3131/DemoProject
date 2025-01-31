using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.Services.Concrete.Account.Layout
{
    public class EFDepositSchemeRepository : IDepositSchemeRepository
    {
        private readonly EFDbContext context;
        private readonly ISchemeDetailRepository schemeDetailRepository;
        private readonly ISchemeDbContextRepository schemeDbContextRepository;
        private readonly IDepositSchemeParameterRepository depositSchemeParameterRepository;

        public EFDepositSchemeRepository(RepositoryConnection _connection, ISchemeDetailRepository _schemeDetailRepository, ISchemeDbContextRepository _schemeDbContextRepository, IDepositSchemeParameterRepository _depositSchemeParameterRepository)
        {
            context = _connection.EFDbContext;
            schemeDetailRepository = _schemeDetailRepository;
            schemeDbContextRepository = _schemeDbContextRepository;
            depositSchemeParameterRepository = _depositSchemeParameterRepository;
        }

        public async Task<bool> Amend(DepositSchemeViewModel _depositSchemeViewModel)
        {
            try
            {
                DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();

                bool result;

                result = schemeDbContextRepository.AttachDepositSchemeData(_depositSchemeViewModel, StringLiteralValue.Amend);

                // SchemeAccountParameter
                if (result)
                    result = schemeDbContextRepository.AttachSchemeAccountParameterData(_depositSchemeViewModel.SchemeAccountParameterViewModel, StringLiteralValue.Amend);

                // SchemeDepositAccountParameter
                if (result)
                    result = schemeDbContextRepository.AttachSchemeDepositAccountParameterData(_depositSchemeViewModel.SchemeDepositAccountParameterViewModel, StringLiteralValue.Amend);

                // SchemeCustomerAccountNumber
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeCustomerAccountNumberViewModel.SchemeCustomerAccountNumberPrmKey > 0)
                    {
                        result = schemeDbContextRepository.AttachSchemeCustomerAccountNumberData(_depositSchemeViewModel.SchemeCustomerAccountNumberViewModel, StringLiteralValue.Amend);
                    }
                }

                // SchemeApplicationParameter
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeApplicationParameterViewModel.SchemeApplicationParameterPrmKey > 0)
                    {
                        if (depositSchemeParameterViewModel.EnableApplicationParameter == true)
                        {
                            if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableApplication == true)
                                result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_depositSchemeViewModel.SchemeApplicationParameterViewModel, StringLiteralValue.Amend);
                            else
                                result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_depositSchemeViewModel.SchemeApplicationParameterViewModel, StringLiteralValue.Delete);
                        }
                    }
                    else
                    {
                        if (depositSchemeParameterViewModel.EnableApplicationParameter == true)
                        {
                            if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableApplication == true)
                                result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_depositSchemeViewModel.SchemeApplicationParameterViewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // SchemeAccountBankingChannelParameter
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeAccountBankingChannelParameterViewModel.SchemeAccountBankingChannelParameterPrmKey > 0)
                    {
                        if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableBankingChannel == true)
                            result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_depositSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_depositSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableBankingChannel == true)
                            result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_depositSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, StringLiteralValue.Create);
                    }
                }

                // SchemeDepositInterestParameter
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType != "FDP" || _depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnablePeriodicInterestPayout == false)
                        _depositSchemeViewModel.SchemeDepositInterestParameterViewModel.InterestPayoutDay = "NNN";

                    result = schemeDbContextRepository.AttachSchemeDepositInterestParameterData(_depositSchemeViewModel.SchemeDepositInterestParameterViewModel, StringLiteralValue.Create);
                }

                // SchemeDepositInterestProvisionParameter
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositInterestProvisionParameterViewModel.SchemeDepositInterestProvisionParameterPrmKey > 0)
                    {
                        if (_depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnableInterestProvision == true)
                            result = schemeDbContextRepository.AttachSchemeDepositInterestProvisionParameterData(_depositSchemeViewModel.SchemeDepositInterestProvisionParameterViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeDepositInterestProvisionParameterData(_depositSchemeViewModel.SchemeDepositInterestProvisionParameterViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnableInterestProvision == true)
                            result = schemeDbContextRepository.AttachSchemeDepositInterestProvisionParameterData(_depositSchemeViewModel.SchemeDepositInterestProvisionParameterViewModel, StringLiteralValue.Create);
                    }
                }

                // SchemeNoticeSchedule
                // Old Record Amended For Amened 
                if (result)
                {
                    IEnumerable<SchemeNoticeScheduleViewModel> schemeNoticeScheduleViewModelListForAmend = await schemeDetailRepository.GetNoticeScheduleEntries(_depositSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                    if (schemeNoticeScheduleViewModelListForAmend != null)
                    {
                        foreach (SchemeNoticeScheduleViewModel viewModel in schemeNoticeScheduleViewModelListForAmend)
                        {
                            result = schemeDbContextRepository.AttachSchemeNoticeScheduleData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // New Record Create For Amened 
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableNoticeScheduleParameter == true)
                    {
                        if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableSmsService == true || _depositSchemeViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                        {
                            List<SchemeNoticeScheduleViewModel> schemeNoticeScheduleViewModelList = new List<SchemeNoticeScheduleViewModel>();
                            schemeNoticeScheduleViewModelList = (List<SchemeNoticeScheduleViewModel>)HttpContext.Current.Session["SchemeNoticeSchedule"];

                            if (schemeNoticeScheduleViewModelList != null)
                            {
                                foreach (SchemeNoticeScheduleViewModel viewModel in schemeNoticeScheduleViewModelList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeNoticeScheduleData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }

                    }
                }

                // SchemeEstimateTarget
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableTargetEstimationParameter == true)
                        result = schemeDbContextRepository.AttachSchemeEstimateTargetData(_depositSchemeViewModel.SchemeEstimateTargetViewModel, StringLiteralValue.Amend);
                }

                // SchemeTransactionAmountLimit
                // Old Record Amended For Amened 
                if (result)
                {
                    IEnumerable<SchemeTransactionAmountLimitViewModel> schemeTransactionAmountLimitViewModelListForAmend = await schemeDetailRepository.GetTransactionAmountLimitEntries(_depositSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                    if (schemeTransactionAmountLimitViewModelListForAmend != null)
                    {
                        foreach (SchemeTransactionAmountLimitViewModel viewModel in schemeTransactionAmountLimitViewModelListForAmend)
                        {
                            result = schemeDbContextRepository.AttachSchemeTransactionAmountLimitData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // New Record Create For Amened 
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableTransactionAmountLimitParameter == true)
                    {
                        if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableTransactionAmountLimit == true)
                        {
                            List<SchemeTransactionAmountLimitViewModel> schemeTransactionAmountLimitViewModelList = new List<SchemeTransactionAmountLimitViewModel>();
                            schemeTransactionAmountLimitViewModelList = (List<SchemeTransactionAmountLimitViewModel>)HttpContext.Current.Session["SchemeTransactionAmountLimit"];

                            if (schemeTransactionAmountLimitViewModelList != null)
                            {
                                foreach (SchemeTransactionAmountLimitViewModel viewModel in schemeTransactionAmountLimitViewModelList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeTransactionAmountLimitData(viewModel, StringLiteralValue.Create);
                                }
                            }

                        }
                    }
                }

                // SchemeNumberOfTransactionLimit   
                // Old Record Amended For Amened 
                if (result)
                {
                    IEnumerable<SchemeNumberOfTransactionLimitViewModel> schemeNumberOfTransactionLimitViewModelListForAmend = await schemeDetailRepository.GetNumberOfTransactionLimitEntries(_depositSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                    if (schemeNumberOfTransactionLimitViewModelListForAmend != null)
                    {
                        foreach (SchemeNumberOfTransactionLimitViewModel viewModel in schemeNumberOfTransactionLimitViewModelListForAmend)
                        {
                            result = schemeDbContextRepository.AttachSchemeNumberOfTransactionLimitData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // New Record Create For Amened 
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableTransactionAmountLimitParameter == true)
                    {
                        if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableNumberOfTransactionLimit == true)
                        {
                            List<SchemeNumberOfTransactionLimitViewModel> schemeNumberOfTransactionLimitViewModelList = new List<SchemeNumberOfTransactionLimitViewModel>();
                            schemeNumberOfTransactionLimitViewModelList = (List<SchemeNumberOfTransactionLimitViewModel>)HttpContext.Current.Session["SchemeNumberOfTransactionLimit"];

                            if (schemeNumberOfTransactionLimitViewModelList != null)
                            {
                                foreach (SchemeNumberOfTransactionLimitViewModel viewModel in schemeNumberOfTransactionLimitViewModelList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeNumberOfTransactionLimitData(viewModel, StringLiteralValue.Create);
                                }
                            }

                        }
                    }
                }

                // SchemeReportFormat
                // Old Record Amended For Amened 
                if (result)
                {
                    IEnumerable<SchemeReportFormatViewModel> schemeReportFormatViewModelListForAmend = await schemeDetailRepository.GetReportFormatEntries(_depositSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                    if (schemeReportFormatViewModelListForAmend != null)
                    {
                        foreach (SchemeReportFormatViewModel viewModel in schemeReportFormatViewModelListForAmend)
                        {
                            result = schemeDbContextRepository.AttachSchemeReportFormatData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // New Record Create For Amened 
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableReportFormatParameter == true)
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
                }

                // SchemeTenure
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeTenureViewModel.PrmKey > 0)
                    {
                        if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableTenure == true)
                            result = schemeDbContextRepository.AttachSchemeTenureData(_depositSchemeViewModel.SchemeTenureViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeTenureData(_depositSchemeViewModel.SchemeTenureViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableTenure == true)
                            result = schemeDbContextRepository.AttachSchemeTenureData(_depositSchemeViewModel.SchemeTenureViewModel, StringLiteralValue.Create);
                    }
                }

                // SchemeTenureList
                // Old Record Amended For Amened 
                if (result)
                {
                    IEnumerable<SchemeTenureListViewModel> schemeTenureListViewModelListForAmend = await schemeDetailRepository.GetTenureListEntries(_depositSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                    if (schemeTenureListViewModelListForAmend != null)
                    {
                        foreach (SchemeTenureListViewModel viewModel in schemeTenureListViewModelListForAmend)
                        {
                            result = schemeDbContextRepository.AttachSchemeTenureListData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // New Record Create For Amened 
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableTenureListParameter == true)
                    {
                        if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableTenureList == true)
                        {
                            List<SchemeTenureListViewModel> schemeTenureListViewModelList = new List<SchemeTenureListViewModel>();
                            schemeTenureListViewModelList = (List<SchemeTenureListViewModel>)HttpContext.Current.Session["SchemeTenureList"];

                            if (schemeTenureListViewModelList != null)
                            {
                                foreach (SchemeTenureListViewModel viewModel in schemeTenureListViewModelList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeTenureListData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }
                    }
                }

                // SchemePassbook
                if (result)
                {
                    if (_depositSchemeViewModel.SchemePassbookViewModel.PrmKey > 0)
                    {
                        if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == true)
                            result = schemeDbContextRepository.AttachSchemePassbookData(_depositSchemeViewModel.SchemePassbookViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemePassbookData(_depositSchemeViewModel.SchemePassbookViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == true)
                            result = schemeDbContextRepository.AttachSchemePassbookData(_depositSchemeViewModel.SchemePassbookViewModel, StringLiteralValue.Create);
                    }
                }

                // SchemeGeneralLedger
                // Old Record Amended For Amened 
                if (result)
                {
                    IEnumerable<SchemeGeneralLedgerViewModel> schemeGeneralLedgerViewModelListForAmend = await schemeDetailRepository.GetGeneralLedgerEntries(_depositSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                    if (schemeGeneralLedgerViewModelListForAmend != null)
                    {
                        foreach (SchemeGeneralLedgerViewModel viewModel in schemeGeneralLedgerViewModelListForAmend)
                        {
                            result = schemeDbContextRepository.AttachSchemeGeneralLedgerData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // New Record Create For Amened 
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

                // SchemeBusinessOffice
                // Old Record Amended For Amened 
                if (result)
                {
                    IEnumerable<SchemeBusinessOfficeViewModel> schemeBusinessOfficeViewModelListForAmend = await schemeDetailRepository.GetBusinessOfficeEntries(_depositSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                    if (schemeBusinessOfficeViewModelListForAmend != null)
                    {
                        foreach (SchemeBusinessOfficeViewModel viewModel in schemeBusinessOfficeViewModelListForAmend)
                        {
                            result = schemeDbContextRepository.AttachSchemeBusinessOfficeData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // New Record Create For Amened 
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

                // SchemeClosingcharges
                // Old Record Amended For Amened 
                if (result)
                {
                    IEnumerable<SchemeClosingChargesViewModel> schemeClosingChargesViewModelForAmend = await schemeDetailRepository.GetClosingChargesEntries(_depositSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                    if (schemeClosingChargesViewModelForAmend != null)
                    {
                        foreach (SchemeClosingChargesViewModel viewModel in schemeClosingChargesViewModelForAmend)
                        {
                            result = schemeDbContextRepository.AttachSchemeClosingChargesData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // New Record Create For Amened 
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableClosingChargesParameter == true)
                    {
                        if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableClosingCharges == true)
                        {
                            List<SchemeClosingChargesViewModel> schemeClosingChargesViewModelList = new List<SchemeClosingChargesViewModel>();
                            schemeClosingChargesViewModelList = (List<SchemeClosingChargesViewModel>)HttpContext.Current.Session["SchemeClosingChargesDetail"];

                            if (schemeClosingChargesViewModelList != null)
                            {
                                foreach (SchemeClosingChargesViewModel viewModel in schemeClosingChargesViewModelList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeClosingChargesData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }
                    }
                }

                // DemandDepositType
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "DMN" || _depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "PPF")
                        result = schemeDbContextRepository.AttachSchemeDemandDepositDetailData(_depositSchemeViewModel.SchemeDemandDepositDetailViewModel, StringLiteralValue.Amend);
                }

                // Fixed DepositType
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "FDP")
                    {
                        // SchemeDepositCertificateParameter
                        if (_depositSchemeViewModel.SchemeDepositCertificateParameterViewModel.SchemeDepositCertificateParameterPrmKey > 0)
                        {
                            if (depositSchemeParameterViewModel.EnableDepositCertificateParameter == true)
                                result = schemeDbContextRepository.AttachSchemeDepositCertificateParameterData(_depositSchemeViewModel.SchemeDepositCertificateParameterViewModel, StringLiteralValue.Amend);
                            else
                                result = schemeDbContextRepository.AttachSchemeDepositCertificateParameterData(_depositSchemeViewModel.SchemeDepositCertificateParameterViewModel, StringLiteralValue.Delete);
                        }
                        else
                        {
                            if (depositSchemeParameterViewModel.EnableDepositCertificateParameter == true)
                                result = schemeDbContextRepository.AttachSchemeDepositCertificateParameterData(_depositSchemeViewModel.SchemeDepositCertificateParameterViewModel, StringLiteralValue.Create);
                        }

                        // SchemeFixedDepositParameter
                        if (result)
                            result = schemeDbContextRepository.AttachSchemeFixedDepositParameterData(_depositSchemeViewModel.SchemeFixedDepositParameterViewModel, StringLiteralValue.Amend);
                    }
                }

                // Recurring Deposit Type - SchemeDepositInstallmentParameter  
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "REC")
                        result = schemeDbContextRepository.AttachSchemeDepositInstallmentParameterData(_depositSchemeViewModel.SchemeDepositInstallmentParameterViewModel, StringLiteralValue.Amend);
                }

                // All Tables Other Than Demand Deposit Type 
                // SchemeDepositAgentParameter
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType != "DMN")
                    {
                        if (_depositSchemeViewModel.SchemeDepositAgentParameterViewModel.SchemeDepositAgentParameterPrmKey > 0)
                        {
                            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableAgent == true)
                                result = schemeDbContextRepository.AttachSchemeDepositAgentParameterData(_depositSchemeViewModel.SchemeDepositAgentParameterViewModel, StringLiteralValue.Amend);
                            else
                                result = schemeDbContextRepository.AttachSchemeDepositAgentParameterData(_depositSchemeViewModel.SchemeDepositAgentParameterViewModel, StringLiteralValue.Delete);
                        }
                        else
                        {
                            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableAgent == true)
                                result = schemeDbContextRepository.AttachSchemeDepositAgentParameterData(_depositSchemeViewModel.SchemeDepositAgentParameterViewModel, StringLiteralValue.Create);
                        }

                        // SchemeDepositAgentIncentive
                        // Old Record Amended For Amened 
                        if (result)
                        {
                            IEnumerable<SchemeDepositAgentIncentiveViewModel> schemeDepositAgentIncentiveViewModelListForAmend = await schemeDetailRepository.GetAgentIncentiveEntries(_depositSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                            if (schemeDepositAgentIncentiveViewModelListForAmend.Count() > 0)
                            {
                                foreach (SchemeDepositAgentIncentiveViewModel viewModel in schemeDepositAgentIncentiveViewModelListForAmend)
                                {
                                    result = schemeDbContextRepository.AttachSchemeDepositAgentIncentiveData(viewModel, StringLiteralValue.Amend);
                                }
                            }
                        }

                        // New Record Created
                        if (result)
                        {
                            List<SchemeDepositAgentIncentiveViewModel> schemeDepositAgentIncentiveViewModelList = new List<SchemeDepositAgentIncentiveViewModel>();
                            schemeDepositAgentIncentiveViewModelList = (List<SchemeDepositAgentIncentiveViewModel>)HttpContext.Current.Session["SchemeDepositAgentIncentive"];

                            if (schemeDepositAgentIncentiveViewModelList != null)
                            {
                                foreach (SchemeDepositAgentIncentiveViewModel viewModel in schemeDepositAgentIncentiveViewModelList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeDepositAgentIncentiveData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }

                        // SchemeDepositAccountRenewalParameter
                        if (result)
                        {
                            if (_depositSchemeViewModel.SchemeDepositAccountRenewalParameterViewModel.SchemeDepositAccountRenewalParameterPrmKey > 0)
                            {
                                if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableRenewal == true)
                                {
                                    result = schemeDbContextRepository.AttachSchemeDepositAccountRenewalParameterData(_depositSchemeViewModel.SchemeDepositAccountRenewalParameterViewModel, StringLiteralValue.Amend);
                                }
                                else
                                {
                                    result = schemeDbContextRepository.AttachSchemeDepositAccountRenewalParameterData(_depositSchemeViewModel.SchemeDepositAccountRenewalParameterViewModel, StringLiteralValue.Delete);
                                }
                            }
                            else
                            {
                                if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableRenewal == true)
                                {
                                    result = schemeDbContextRepository.AttachSchemeDepositAccountRenewalParameterData(_depositSchemeViewModel.SchemeDepositAccountRenewalParameterViewModel, StringLiteralValue.Create);
                                }
                            }
                        }

                        // SchemeDepositAccountClosureParameter
                        if (result)
                            result = schemeDbContextRepository.AttachSchemeDepositAccountClosureParameterData(_depositSchemeViewModel.SchemeDepositAccountClosureParameterViewModel, StringLiteralValue.Amend);
                    }
                }

                // SchemeLimit
                if (result)
                    result = schemeDbContextRepository.AttachSchemeLimitData(_depositSchemeViewModel.SchemeLimitViewModel, StringLiteralValue.Amend);

                //Target Group
                //Amend Old  Record
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableTargetGroupParameter == true)
                    {
                        if (_depositSchemeViewModel.SchemeTargetGroupViewModel.PrmKey > 0)
                        {
                                IEnumerable<SchemeTargetGroupViewModel> schemeTargetGroupViewModelListForAmend = await schemeDetailRepository.GetTargetGroupEntries(_depositSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);
                                if (schemeTargetGroupViewModelListForAmend.Count() > 0)
                                {
                                    foreach (SchemeTargetGroupViewModel viewModel in schemeTargetGroupViewModelListForAmend)
                                    {
                                        result = schemeDbContextRepository.AttachSchemeTargetGroupData(viewModel, StringLiteralValue.Amend);
                                    }
                                }
                        }
                    }

                    //New Record
                    if (depositSchemeParameterViewModel.EnableTargetGroupParameter == true)
                    {
                        if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableTargetGroup == true)
                        {
                            List<SchemeTargetGroupViewModel> schemeTargetGroupViewModelLists = (List<SchemeTargetGroupViewModel>)HttpContext.Current.Session["SchemeTargetGroup"];
                            if (schemeTargetGroupViewModelLists != null)
                            {
                                foreach (SchemeTargetGroupViewModel viewModel in schemeTargetGroupViewModelLists)
                                {
                                    result = schemeDbContextRepository.AttachSchemeTargetGroupData(viewModel, StringLiteralValue.Create);
                                }
                            }
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

        public List<SelectListItem> SchemeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.Schemes
                            join t in context.SchemeTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on d.PrmKey equals t.SchemePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                            && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby d.NameOfScheme
                            select new SelectListItem
                            {
                                Value = d.SchemeId.ToString(),
                                Text = (d.NameOfScheme.Trim() + " ---> " + (t.TransNameOfScheme.Equals(null) ? " " : t.TransNameOfScheme.Trim()))
                            }).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.Schemes
                        where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                        && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                        select new SelectListItem
                        {
                            Value = d.SchemeId.ToString(),
                            Text = (d.NameOfScheme.Trim())
                        }).ToList();
            }
        }

        public short GetPrmKeyById(Guid _SchemeId)
        {
            return context.Schemes
                    .Where(c => c.SchemeId == _SchemeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
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

        public async Task<IEnumerable<DepositSchemeIndexViewModel>> GetDepositSchemeIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<DepositSchemeIndexViewModel>("SELECT * FROM dbo.GetDepositSchemeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<DepositSchemeViewModel> GetDepositSchemeEntry(Guid _SchemeId, string _entryType)
        {
            try
            {
                DepositSchemeViewModel depositSchemeViewModel = await context.Database.SqlQuery<DepositSchemeViewModel>("SELECT * FROM dbo.GetDepositSchemeEntry (@SchemeId, @EntriesType)", new SqlParameter("@SchemeId", _SchemeId), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                short _schemePrmKey = GetPrmKeyById(_SchemeId);

                depositSchemeViewModel.SchemeAccountParameterViewModel = await schemeDetailRepository.GetAccountParameterEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeDepositAccountParameterViewModel = await schemeDetailRepository.GetDepositAccountParameterEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeApplicationParameterViewModel = await schemeDetailRepository.GetApplicationParameterEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeCustomerAccountNumberViewModel = await schemeDetailRepository.GetCustomerAccountNumberEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeDepositInstallmentParameterViewModel = await schemeDetailRepository.GetInstallmentParameterEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeDepositAgentParameterViewModel = await schemeDetailRepository.GetAgentParameterEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeDepositCertificateParameterViewModel = await schemeDetailRepository.GetCertificateParameterEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeDemandDepositDetailViewModel = await schemeDetailRepository.GetDemandDepositDetailEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeAccountBankingChannelParameterViewModel = await schemeDetailRepository.GetAccountBankingChannelParameterEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeFixedDepositParameterViewModel = await schemeDetailRepository.GetFixedDepositParameterEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeDepositAccountRenewalParameterViewModel = await schemeDetailRepository.GetAccountRenewalParameterEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeEstimateTargetViewModel = await schemeDetailRepository.GetEstimateTargetEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeDepositAccountClosureParameterViewModel = await schemeDetailRepository.GetAccountClosureParameterEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemePassbookViewModel = await schemeDetailRepository.GetPassbookEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeLimitViewModel = await schemeDetailRepository.GetLimitEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeTenureViewModel = await schemeDetailRepository.GetTenureEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeDepositInterestParameterViewModel = await schemeDetailRepository.GetDepositInterestParameterEntry(_schemePrmKey, _entryType);
                depositSchemeViewModel.SchemeDepositInterestProvisionParameterViewModel = await schemeDetailRepository.GetInterestProvisionParameterEntry(_schemePrmKey, _entryType);


                if (depositSchemeViewModel.SchemeDemandDepositDetailViewModel != null)
                {
                    //// Get Multiselect Id's From String (i.e. (Array) PhotoDocumentFormatTypeIdForDatabase From (String) PhotoDocumentAllowedFileFormatsForDb)
                    if (depositSchemeViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoDocumentUploadInDb)
                    {
                        depositSchemeViewModel.SchemeDemandDepositDetailViewModel.PhotoDocumentFormatTypeIdForDatabase = depositSchemeViewModel.SchemeDemandDepositDetailViewModel.PhotoDocumentAllowedFileFormatsForDb.Split(',');
                    }

                    //// Get Multiselect Id's From String (i.e. (Array) PhotoDocumentFormatTypeIdForLocalStorage From (String) PhotoDocumentAllowedFileFormatsForLocalStorage)
                    if (depositSchemeViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoDocumentUploadInLocalStorage)
                    {
                        depositSchemeViewModel.SchemeDemandDepositDetailViewModel.PhotoDocumentFormatTypeIdForLocalStorage = depositSchemeViewModel.SchemeDemandDepositDetailViewModel.PhotoDocumentAllowedFileFormatsForLocalStorage.Split(',');
                    }

                    //// Get Multiselect Id's From String (i.e. (Array) SignDocumentFormatTypeIdForDatabase From (String) SignDocumentAllowedFileFormatsForDb)
                    if (depositSchemeViewModel.SchemeDemandDepositDetailViewModel.EnableSignDocumentUploadInDb)
                    {
                        depositSchemeViewModel.SchemeDemandDepositDetailViewModel.SignDocumentFormatTypeIdForDatabase = depositSchemeViewModel.SchemeDemandDepositDetailViewModel.SignDocumentAllowedFileFormatsForDb.Split(',');
                    }

                    //// Get Multiselect Id's From String (i.e. (Array) SignDocumentFormatTypeIdForLocalStorage From (String) SignDocumentAllowedFileFormatsForLocalStorage)
                    if (depositSchemeViewModel.SchemeDemandDepositDetailViewModel.EnableSignDocumentUploadInLocalStorage)
                    {
                        depositSchemeViewModel.SchemeDemandDepositDetailViewModel.SignDocumentFormatTypeIdForLocalStorage = depositSchemeViewModel.SchemeDemandDepositDetailViewModel.SignDocumentAllowedFileFormatsForLocalStorage.Split(',');
                    }
                }

                if (depositSchemeViewModel.SchemeDepositInterestParameterViewModel.InterestPayoutDay != "FST" && depositSchemeViewModel.SchemeDepositInterestParameterViewModel.InterestPayoutDay != "LST" && depositSchemeViewModel.SchemeDepositInterestParameterViewModel.InterestPayoutDay != "NNN")
                {
                    depositSchemeViewModel.SchemeDepositInterestParameterViewModel.InterestPayoutDayOther = Convert.ToByte(depositSchemeViewModel.SchemeDepositInterestParameterViewModel.InterestPayoutDay);
                    depositSchemeViewModel.SchemeDepositInterestParameterViewModel.InterestPayoutDay = "CST";
                }

                IEnumerable<SchemeDepositAgentIncentiveViewModel> schemeDepositAgentIncentiveViewModels =  (IEnumerable<SchemeDepositAgentIncentiveViewModel>)HttpContext.Current.Session["SchemeDepositAgentIncentive"];

                if (schemeDepositAgentIncentiveViewModels .Count() > 0)
                {
                    depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableAgentIncentiveParameter = true;
                }

                return depositSchemeViewModel;
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
                HttpContext.Current.Session["SchemeClosingChargesDetail"] = await schemeDetailRepository.GetClosingChargesEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeDepositAgentIncentive"] = await schemeDetailRepository.GetAgentIncentiveEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeNumberOfTransactionLimit"] = await schemeDetailRepository.GetNumberOfTransactionLimitEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeNoticeSchedule"] = await schemeDetailRepository.GetNoticeScheduleEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeReportFormat"] = await schemeDetailRepository.GetReportFormatEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeTenureList"] = await schemeDetailRepository.GetTenureListEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeTransactionAmountLimit"] = await schemeDetailRepository.GetTransactionAmountLimitEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeTargetGroup"] = await schemeDetailRepository.GetTargetGroupEntries(_schemePrmKey, _entryType);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public bool GetUniqueSchemeName(string _nameOfScheme)
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
        
        //*** Transaction Table Entries Are Modified After Verification, So For Current Operation(i.e.Create / Modify) Not Required Modify Old Entries***
        public async Task<bool> Save(DepositSchemeViewModel _depositSchemeViewModel)
        {
            try
            {
                DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();

                bool result;

                result = schemeDbContextRepository.AttachDepositSchemeData(_depositSchemeViewModel, StringLiteralValue.Create);

                // SchemeAccountParameter
                if (result)
                    result = schemeDbContextRepository.AttachSchemeAccountParameterData(_depositSchemeViewModel.SchemeAccountParameterViewModel, StringLiteralValue.Create);

                // SchemeDepositAccountParameter
                if (result)
                    result = schemeDbContextRepository.AttachSchemeDepositAccountParameterData(_depositSchemeViewModel.SchemeDepositAccountParameterViewModel, StringLiteralValue.Create);

                // SchemeCustomerAccountNumber
                if (result)
                    result = schemeDbContextRepository.AttachSchemeCustomerAccountNumberData(_depositSchemeViewModel.SchemeCustomerAccountNumberViewModel, StringLiteralValue.Create);

                // SchemeApplicationParameter
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableApplication == true)
                        result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_depositSchemeViewModel.SchemeApplicationParameterViewModel, StringLiteralValue.Create);
                }

                // SchemeAccountBankingChannelParameter
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableBankingChannel == true)
                        result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_depositSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, StringLiteralValue.Create);
                }

                // SchemeDepositInterestParameter
                if (result)
                {
                    if(_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType != "FDP" || _depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnablePeriodicInterestPayout == false)
                        _depositSchemeViewModel.SchemeDepositInterestParameterViewModel.InterestPayoutDay = "NNN";

                    result = schemeDbContextRepository.AttachSchemeDepositInterestParameterData(_depositSchemeViewModel.SchemeDepositInterestParameterViewModel, StringLiteralValue.Create);
                }

                // SchemeDepositInterestProvisionParameter
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnableInterestProvision == true)
                        result = schemeDbContextRepository.AttachSchemeDepositInterestProvisionParameterData(_depositSchemeViewModel.SchemeDepositInterestProvisionParameterViewModel, StringLiteralValue.Create);
                }

                // SchemeNoticeSchedule
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableSmsService == true || _depositSchemeViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                    {
                        List<SchemeNoticeScheduleViewModel> schemeNoticeScheduleViewModelList = new List<SchemeNoticeScheduleViewModel>();
                        schemeNoticeScheduleViewModelList = (List<SchemeNoticeScheduleViewModel>)HttpContext.Current.Session["SchemeNoticeSchedule"];

                        if (schemeNoticeScheduleViewModelList != null)
                        {
                            foreach (SchemeNoticeScheduleViewModel viewModel in schemeNoticeScheduleViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeNoticeScheduleData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // SchemeEstimateTarget
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableTargetEstimationParameter == true)
                        result = schemeDbContextRepository.AttachSchemeEstimateTargetData(_depositSchemeViewModel.SchemeEstimateTargetViewModel, StringLiteralValue.Create);
                }

                // SchemeTransactionAmountLimit
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableTransactionAmountLimit == true)
                    {
                        List<SchemeTransactionAmountLimitViewModel> schemeTransactionAmountLimitViewModelList = new List<SchemeTransactionAmountLimitViewModel>();
                        schemeTransactionAmountLimitViewModelList = (List<SchemeTransactionAmountLimitViewModel>)HttpContext.Current.Session["SchemeTransactionAmountLimit"];

                        if (schemeTransactionAmountLimitViewModelList != null)
                        {
                            foreach (SchemeTransactionAmountLimitViewModel viewModel in schemeTransactionAmountLimitViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeTransactionAmountLimitData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // SchemeNumberOfTransactionLimit   
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableNumberOfTransactionLimit == true)
                    {
                        List<SchemeNumberOfTransactionLimitViewModel> schemeNumberOfTransactionLimitViewModelList = new List<SchemeNumberOfTransactionLimitViewModel>();
                        schemeNumberOfTransactionLimitViewModelList = (List<SchemeNumberOfTransactionLimitViewModel>)HttpContext.Current.Session["SchemeNumberOfTransactionLimit"];

                        if (schemeNumberOfTransactionLimitViewModelList != null)
                        {
                            foreach (SchemeNumberOfTransactionLimitViewModel viewModel in schemeNumberOfTransactionLimitViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeNumberOfTransactionLimitData(viewModel, StringLiteralValue.Create);
                            }
                        }

                    }
                }

                // SchemeReportFormat
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableReportFormatParameter == true)
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
                }

                // SchemeTenure
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableTenure == true)
                        result = schemeDbContextRepository.AttachSchemeTenureData(_depositSchemeViewModel.SchemeTenureViewModel, StringLiteralValue.Create);
                }

                // SchemeTenureList
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableTenureList == true)
                    {

                        List<SchemeTenureListViewModel> schemeTenureListViewModelList = new List<SchemeTenureListViewModel>();
                        schemeTenureListViewModelList = (List<SchemeTenureListViewModel>)HttpContext.Current.Session["SchemeTenureList"];

                        if (schemeTenureListViewModelList != null)
                        {
                            foreach (SchemeTenureListViewModel viewModel in schemeTenureListViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeTenureListData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // SchemePassbook
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == true)
                        result = schemeDbContextRepository.AttachSchemePassbookData(_depositSchemeViewModel.SchemePassbookViewModel, StringLiteralValue.Create);
                }

                // SchemeGeneralLedger
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

                // SchemeBusinessOffice
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

                // SchemeClosingcharges
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableClosingChargesParameter == true)
                    {
                        if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableClosingCharges == true)
                        {
                            List<SchemeClosingChargesViewModel> schemeClosingChargesViewModelList = new List<SchemeClosingChargesViewModel>();
                            schemeClosingChargesViewModelList = (List<SchemeClosingChargesViewModel>)HttpContext.Current.Session["SchemeClosingChargesDetail"];

                            if (schemeClosingChargesViewModelList != null)
                            {
                                foreach (SchemeClosingChargesViewModel viewModel in schemeClosingChargesViewModelList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeClosingChargesData(viewModel, StringLiteralValue.Create);
                                }
                            }

                        }
                    }

                }

                // DemandDepositType
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "DMN" || _depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "PPF")
                        result = schemeDbContextRepository.AttachSchemeDemandDepositDetailData(_depositSchemeViewModel.SchemeDemandDepositDetailViewModel, StringLiteralValue.Create);
                }

                // Fixed DepositType
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "FDP")
                    {
                        // SchemeDepositCertificateParameter
                        if (depositSchemeParameterViewModel.EnableDepositCertificateParameter == true)
                            result = schemeDbContextRepository.AttachSchemeDepositCertificateParameterData(_depositSchemeViewModel.SchemeDepositCertificateParameterViewModel, StringLiteralValue.Create);

                        // SchemeFixedDepositParameter
                        if (result)
                            result = schemeDbContextRepository.AttachSchemeFixedDepositParameterData(_depositSchemeViewModel.SchemeFixedDepositParameterViewModel, StringLiteralValue.Create);
                    }
                }

                // Recurring DepositType
                if (result)
                {
                    // SchemeDepositInstallmentParameter
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "REC")
                        result = schemeDbContextRepository.AttachSchemeDepositInstallmentParameterData(_depositSchemeViewModel.SchemeDepositInstallmentParameterViewModel, StringLiteralValue.Create);
                }

                // DepositType Other Than Demand Or PPF
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType != "DMN" || _depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType != "PPF")
                    {
                        // SchemeDepositAgentParameter
                        if (result)
                        {
                            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableAgent == true)
                                result = schemeDbContextRepository.AttachSchemeDepositAgentParameterData(_depositSchemeViewModel.SchemeDepositAgentParameterViewModel, StringLiteralValue.Create);
                        }

                        // SchemeDepositAgentIncentive
                        if (result)
                        {
                            List<SchemeDepositAgentIncentiveViewModel> schemeDepositAgentIncentiveViewModelList = new List<SchemeDepositAgentIncentiveViewModel>();
                            schemeDepositAgentIncentiveViewModelList = (List<SchemeDepositAgentIncentiveViewModel>)HttpContext.Current.Session["SchemeDepositAgentIncentive"];

                            if (schemeDepositAgentIncentiveViewModelList != null)
                            {
                                foreach (SchemeDepositAgentIncentiveViewModel viewModel in schemeDepositAgentIncentiveViewModelList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeDepositAgentIncentiveData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }

                        // SchemeDepositAccountRenewalParameter
                        if (result)
                        {
                            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableRenewal == true)
                                result = schemeDbContextRepository.AttachSchemeDepositAccountRenewalParameterData(_depositSchemeViewModel.SchemeDepositAccountRenewalParameterViewModel, StringLiteralValue.Create);
                        }

                        // SchemeDepositAccountClosureParameter
                        if (result)
                            result = schemeDbContextRepository.AttachSchemeDepositAccountClosureParameterData(_depositSchemeViewModel.SchemeDepositAccountClosureParameterViewModel, StringLiteralValue.Create);
                    }
                }

                //Scheme Limit Parameter 
                if (result)
                    result = schemeDbContextRepository.AttachSchemeLimitData(_depositSchemeViewModel.SchemeLimitViewModel, StringLiteralValue.Create);

                // TargetGroup
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableTargetGroupParameter == true)
                    {
                        List<SchemeTargetGroupViewModel> schemeTargetGroupViewModelList = (List<SchemeTargetGroupViewModel>)HttpContext.Current.Session["SchemeTargetGroup"];

                        if (schemeTargetGroupViewModelList != null)
                        {
                            foreach (SchemeTargetGroupViewModel viewModel in schemeTargetGroupViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeTargetGroupData(viewModel, StringLiteralValue.Create);
                            }
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

        public async Task<bool> VerifyRejectDelete(DepositSchemeViewModel _depositSchemeViewModel,string _entryType)
        {
            try
            {
                string entriesType;
                if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
                    entriesType = StringLiteralValue.Unverified;
                else
                    entriesType = StringLiteralValue.Reject;

                DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();

                bool result;

                result = schemeDbContextRepository.AttachDepositSchemeData(_depositSchemeViewModel, _entryType);

                // SchemeAccountParameter
                if (result)
                {
                    SchemeAccountParameterViewModel schemeAccountParameterViewModel = await schemeDetailRepository.GetAccountParameterEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeAccountParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeAccountParameterData(_depositSchemeViewModel.SchemeAccountParameterViewModel, _entryType);
                }

                // SchemeDepositAccountParameter
                if (result)
                {
                    SchemeDepositAccountParameterViewModel schemeDepositAccountParameterViewModel = await schemeDetailRepository.GetDepositAccountParameterEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeDepositAccountParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeDepositAccountParameterData(_depositSchemeViewModel.SchemeDepositAccountParameterViewModel, _entryType);
                }

                // SchemeCustomerAccountNumber
                if (result)
                {
                    SchemeCustomerAccountNumberViewModel schemeCustomerAccountNumberViewModel = await schemeDetailRepository.GetCustomerAccountNumberEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeCustomerAccountNumberViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeCustomerAccountNumberData(_depositSchemeViewModel.SchemeCustomerAccountNumberViewModel, _entryType);
                }

                // SchemeApplicationParameter
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableApplication == true)
                    {
                        SchemeApplicationParameterViewModel schemeApplicationParameterViewModel = await schemeDetailRepository.GetApplicationParameterEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeApplicationParameterViewModel != null)
                        {
                            result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_depositSchemeViewModel.SchemeApplicationParameterViewModel, _entryType);
                        }
                    }
                }

                // SchemeAccountBankingChannelParameter
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableBankingChannel == true)
                    {
                        SchemeAccountBankingChannelParameterViewModel schemeAccountBankingChannelParameterViewModel = await schemeDetailRepository.GetAccountBankingChannelParameterEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeAccountBankingChannelParameterViewModel != null)
                        {
                            result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_depositSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, _entryType);
                        }
                    }
                }

                // SchemeDepositInterestParameter
                if (result)
                {
                    SchemeDepositInterestParameterViewModel schemeDepositInterestParameterViewModel = await schemeDetailRepository.GetDepositInterestParameterEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeDepositInterestParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeDepositInterestParameterData(_depositSchemeViewModel.SchemeDepositInterestParameterViewModel, _entryType);
                }

                // SchemeDepositInterestProvisionParameter
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnableInterestProvision == true)
                    {
                        SchemeDepositInterestProvisionParameterViewModel schemeDepositInterestProvisionParameterViewModel = await schemeDetailRepository.GetInterestProvisionParameterEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeDepositInterestProvisionParameterViewModel != null)
                            result = schemeDbContextRepository.AttachSchemeDepositInterestProvisionParameterData(_depositSchemeViewModel.SchemeDepositInterestProvisionParameterViewModel, _entryType);
                    }
                }

                // SchemeNoticeSchedule
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableSmsService == true || _depositSchemeViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                    {
                        IEnumerable<SchemeNoticeScheduleViewModel> schemeNoticeScheduleViewModelList = await schemeDetailRepository.GetNoticeScheduleEntries(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeNoticeScheduleViewModelList != null)
                        {
                            foreach (SchemeNoticeScheduleViewModel viewModel in schemeNoticeScheduleViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeNoticeScheduleData(viewModel, _entryType);
                            }
                        }
                    }
                }

                // SchemeEstimateTarget
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableTargetEstimationParameter == true)
                    {
                        SchemeEstimateTargetViewModel schemeEstimateTargetViewModel = await schemeDetailRepository.GetEstimateTargetEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeEstimateTargetViewModel != null)
                            result = schemeDbContextRepository.AttachSchemeEstimateTargetData(_depositSchemeViewModel.SchemeEstimateTargetViewModel, _entryType);
                    }
                }

                // SchemeTransactionAmountLimit
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableTransactionAmountLimit == true)
                    {
                        IEnumerable<SchemeTransactionAmountLimitViewModel> schemeTransactionAmountLimitViewModelList = await schemeDetailRepository.GetTransactionAmountLimitEntries(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeTransactionAmountLimitViewModelList != null)
                        {
                            foreach (SchemeTransactionAmountLimitViewModel viewModel in schemeTransactionAmountLimitViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeTransactionAmountLimitData(viewModel, _entryType);
                            }
                        }
                    }
                }

                // SchemeNumberOfTransactionLimit   
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableNumberOfTransactionLimit == true)
                    {
                        IEnumerable<SchemeNumberOfTransactionLimitViewModel> schemeNumberOfTransactionLimitViewModelList = await schemeDetailRepository.GetNumberOfTransactionLimitEntries(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeNumberOfTransactionLimitViewModelList != null)
                        {
                            foreach (SchemeNumberOfTransactionLimitViewModel viewModel in schemeNumberOfTransactionLimitViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeNumberOfTransactionLimitData(viewModel, _entryType);
                            }
                        }
                    }
                }

                // SchemeReportFormat
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableReportFormatParameter == true)
                    {
                        IEnumerable<SchemeReportFormatViewModel> schemeReportFormatViewModelList = await schemeDetailRepository.GetReportFormatEntries(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeReportFormatViewModelList != null)
                        {
                            foreach (SchemeReportFormatViewModel viewModel in schemeReportFormatViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeReportFormatData(viewModel, _entryType);
                            }
                        }
                    }
                }

                // SchemeTenure
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableTenure == true)
                    {
                        SchemeTenureViewModel schemeTenureViewModel = await schemeDetailRepository.GetTenureEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeTenureViewModel != null)
                            result = schemeDbContextRepository.AttachSchemeTenureData(_depositSchemeViewModel.SchemeTenureViewModel, _entryType);
                    }
                }

                // SchemeTenureList
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableTenureList == true)
                    {
                        IEnumerable<SchemeTenureListViewModel> schemeTenureListViewModelList = await schemeDetailRepository.GetTenureListEntries(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeTenureListViewModelList != null)
                        {
                            foreach (SchemeTenureListViewModel viewModel in schemeTenureListViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeTenureListData(viewModel, _entryType);
                            }
                        }
                    }
                }

                // SchemePassbook
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == true)
                    {
                        SchemePassbookViewModel schemePassbookViewModel = await schemeDetailRepository.GetPassbookEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemePassbookViewModel != null)
                            result = schemeDbContextRepository.AttachSchemePassbookData(_depositSchemeViewModel.SchemePassbookViewModel, _entryType);
                    }
                }

                // SchemeGeneralLedger
                if (result)
                {
                    IEnumerable<SchemeGeneralLedgerViewModel> schemeGeneralLedgerViewModelList = await schemeDetailRepository.GetGeneralLedgerEntries(_depositSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeGeneralLedgerViewModelList != null)
                    {
                        foreach (SchemeGeneralLedgerViewModel viewModel in schemeGeneralLedgerViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeGeneralLedgerData(viewModel, _entryType);
                        }
                    }
                }

                // SchemeBusinessOffice
                if (result)
                {
                    IEnumerable<SchemeBusinessOfficeViewModel> schemeBusinessOfficeViewModelList = await schemeDetailRepository.GetBusinessOfficeEntries(_depositSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeBusinessOfficeViewModelList != null)
                    {
                        foreach (SchemeBusinessOfficeViewModel viewModel in schemeBusinessOfficeViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeBusinessOfficeData(viewModel, _entryType);
                        }
                    }
                }

                // SchemeClosingcharges
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableClosingChargesParameter == true)
                    {
                        if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableClosingCharges == true)
                        {
                            IEnumerable<SchemeClosingChargesViewModel> schemeClosingChargesViewModelList = await schemeDetailRepository.GetClosingChargesEntries(_depositSchemeViewModel.SchemePrmKey, entriesType);

                            if (schemeClosingChargesViewModelList != null)
                            {
                                foreach (SchemeClosingChargesViewModel viewModel in schemeClosingChargesViewModelList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeClosingChargesData(viewModel, _entryType);
                                }
                            }
                        }
                    }
                }

                // DemandDepositType
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "DMN" || _depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "PPF")
                    {
                        // SchemeDemandDepositDetail
                        SchemeDemandDepositDetailViewModel schemeDemandDepositDetailViewModel = await schemeDetailRepository.GetDemandDepositDetailEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeDemandDepositDetailViewModel != null)
                            result = schemeDbContextRepository.AttachSchemeDemandDepositDetailData(_depositSchemeViewModel.SchemeDemandDepositDetailViewModel, _entryType);
                    }
                }

                // Fixed DepositType
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "FDP")
                    {
                        // SchemeDepositCertificateParameter
                        if (depositSchemeParameterViewModel.EnableDepositCertificateParameter == true)
                        {
                            SchemeDepositCertificateParameterViewModel schemeDepositCertificateParameterViewModel = await schemeDetailRepository.GetCertificateParameterEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                            if (schemeDepositCertificateParameterViewModel != null)
                            {
                                result = schemeDbContextRepository.AttachSchemeDepositCertificateParameterData(_depositSchemeViewModel.SchemeDepositCertificateParameterViewModel, _entryType);
                            }
                        }

                        // SchemeFixedDepositParameter
                        if (result)
                        {
                            SchemeFixedDepositParameterViewModel schemeFixedDepositParameterViewModel = await schemeDetailRepository.GetFixedDepositParameterEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                            if (schemeFixedDepositParameterViewModel != null)
                                result = schemeDbContextRepository.AttachSchemeFixedDepositParameterData(_depositSchemeViewModel.SchemeFixedDepositParameterViewModel, _entryType);
                        }
                    }
                }

                // Recurring DepositType
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "REC")
                    {
                        // SchemeDepositInstallmentParameter
                        SchemeDepositInstallmentParameterViewModel depositInstallmentParameterViewModel = await schemeDetailRepository.GetInstallmentParameterEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (depositInstallmentParameterViewModel != null)
                            result = schemeDbContextRepository.AttachSchemeDepositInstallmentParameterData(_depositSchemeViewModel.SchemeDepositInstallmentParameterViewModel, _entryType);
                    }
                }

                // Deposit Type Other Than Demand And PPF
                if (result)
                {
                    if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType != "DMN" || _depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType != "PPF")
                    {
                        // SchemeDepositAgentParameter
                        if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableAgent == true)
                        {
                            SchemeDepositAgentParameterViewModel schemeDepositAgentParameterViewModel = await schemeDetailRepository.GetAgentParameterEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                            if (schemeDepositAgentParameterViewModel != null)
                                result = schemeDbContextRepository.AttachSchemeDepositAgentParameterData(_depositSchemeViewModel.SchemeDepositAgentParameterViewModel, _entryType);
                        }

                        // SchemeDepositAgentIncentive
                        if (result)
                        {
                            IEnumerable<SchemeDepositAgentIncentiveViewModel> schemeDepositAgentIncentiveViewModelList = await schemeDetailRepository.GetAgentIncentiveEntries(_depositSchemeViewModel.SchemePrmKey, entriesType);

                            if (schemeDepositAgentIncentiveViewModelList != null)
                            {
                                foreach (SchemeDepositAgentIncentiveViewModel viewModel in schemeDepositAgentIncentiveViewModelList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeDepositAgentIncentiveData(viewModel, _entryType);
                                }
                            }
                        }

                        // SchemeDepositAccountRenewalParameter
                        if (result)
                        {
                            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableRenewal == true)
                            {
                                SchemeDepositAccountRenewalParameterViewModel schemeDepositAccountRenewalParameterViewModel = await schemeDetailRepository.GetAccountRenewalParameterEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                                if (schemeDepositAccountRenewalParameterViewModel != null)
                                    result = schemeDbContextRepository.AttachSchemeDepositAccountRenewalParameterData(_depositSchemeViewModel.SchemeDepositAccountRenewalParameterViewModel, _entryType);
                            }
                        }

                        // SchemeDepositAccountClosureParameter
                        if (result)
                        {
                            SchemeDepositAccountClosureParameterViewModel schemeDepositAccountClosureParameterViewModel = await schemeDetailRepository.GetAccountClosureParameterEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                            if (schemeDepositAccountClosureParameterViewModel != null)
                                result = schemeDbContextRepository.AttachSchemeDepositAccountClosureParameterData(_depositSchemeViewModel.SchemeDepositAccountClosureParameterViewModel, _entryType);
                        }
                    }
                }

                // SchemeLimit
                if (result)
                {
                    SchemeLimitViewModel schemeLimitViewModel = await schemeDetailRepository.GetLimitEntry(_depositSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLimitViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLimitData(_depositSchemeViewModel.SchemeLimitViewModel, _entryType);
                }

                // SchemeTargetGroup 
                if (result)
                {
                    if (depositSchemeParameterViewModel.EnableTargetGroupParameter == true)
                    {
                        IEnumerable<SchemeTargetGroupViewModel> schemeTargetGroupViewModelList = await schemeDetailRepository.GetTargetGroupEntries(_depositSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeTargetGroupViewModelList != null)
                        {
                            foreach (SchemeTargetGroupViewModel viewModel in schemeTargetGroupViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeTargetGroupData(viewModel, _entryType);
                            }
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
    }
}
