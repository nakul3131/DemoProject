using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Customer;

namespace DemoProject.Services.Concrete.Account.Layout
{
    public class EFLoanSchemeRepository : ILoanSchemeRepository
    {
        private readonly EFDbContext context;

        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ISchemeDetailRepository schemeDetailRepository;
        private readonly ILoanSchemeParameterRepository loanSchemeParameterRepository;
        private readonly ISchemeDbContextRepository schemeDbContextRepository;

        public EFLoanSchemeRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, ISchemeDetailRepository _loanSchemeDetailRepository, ILoanSchemeParameterRepository _loanSchemeParameterRepository, ISchemeDbContextRepository _schemeDbContextRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            schemeDetailRepository = _loanSchemeDetailRepository;
            loanSchemeParameterRepository = _loanSchemeParameterRepository;
            schemeDbContextRepository = _schemeDbContextRepository;
        }

        public async Task<bool> Amend(LoanSchemeViewModel _loanSchemeViewModel)
        {
            try
            {
                LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
                string loanType = accountDetailRepository.GetSysNameOfLoanTypeByLoanTypeId(_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.LoanTypeId);

                bool result;

                result = schemeDbContextRepository.AttachLoanSchemeData(_loanSchemeViewModel, StringLiteralValue.Amend);

                if (result)
                    result = schemeDbContextRepository.AttachSchemeAccountParameterData(_loanSchemeViewModel.SchemeAccountParameterViewModel, StringLiteralValue.Amend);

                if (result)
                    result = schemeDbContextRepository.AttachSchemeLoanAccountParameterData(_loanSchemeViewModel.SchemeLoanAccountParameterViewModel, StringLiteralValue.Amend);

                //AccountBanking 
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeAccountBankingChannelParameterViewModel.SchemeAccountBankingChannelParameterPrmKey > 0)
                    {
                        if (loanSchemeParameterViewModel.EnableBankingChannelParameter == true)
                            result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_loanSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_loanSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (loanSchemeParameterViewModel.EnableBankingChannelParameter == true)
                            result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_loanSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, StringLiteralValue.Create);
                    }
                }

                // SchemeCustomerAccountNumber 
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeCustomerAccountNumberViewModel.SchemeCustomerAccountNumberPrmKey > 0)
                        result = schemeDbContextRepository.AttachSchemeCustomerAccountNumberData(_loanSchemeViewModel.SchemeCustomerAccountNumberViewModel, StringLiteralValue.Amend);
                    else
                        result = schemeDbContextRepository.AttachSchemeCustomerAccountNumberData(_loanSchemeViewModel.SchemeCustomerAccountNumberViewModel, StringLiteralValue.Create);
                }

                //AccountParameter 
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanAgreementNumberViewModel.SchemeLoanAgreementNumberPrmKey > 0)
                    {
                        if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableAgreementNumber == true)
                            result = schemeDbContextRepository.AttachSchemeLoanAgreementNumberData(_loanSchemeViewModel.SchemeLoanAgreementNumberViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeLoanAgreementNumberData(_loanSchemeViewModel.SchemeLoanAgreementNumberViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableAgreementNumber == true)
                            result = schemeDbContextRepository.AttachSchemeLoanAgreementNumberData(_loanSchemeViewModel.SchemeLoanAgreementNumberViewModel, StringLiteralValue.Create);
                    }
                }

                // SchemeTenure
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeTenureViewModel.SchemeTenurePrmKey > 0)
                    {
                        if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnableTenure == true)
                            result = schemeDbContextRepository.AttachSchemeTenureData(_loanSchemeViewModel.SchemeTenureViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeTenureData(_loanSchemeViewModel.SchemeTenureViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnableTenure == true)
                            result = schemeDbContextRepository.AttachSchemeTenureData(_loanSchemeViewModel.SchemeTenureViewModel, StringLiteralValue.Create);
                    }
                }

                //TenureList
                //Amend Old  Record
                if (result)
                {
                    IEnumerable<SchemeTenureListViewModel> schemeTenureListViewModelListForAmend = await schemeDetailRepository.GetTenureListEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);
                    if (schemeTenureListViewModelListForAmend.Count() > 0)
                    {
                        foreach (SchemeTenureListViewModel viewModel in schemeTenureListViewModelListForAmend)
                        {
                            result = schemeDbContextRepository.AttachSchemeTenureListData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    // New Record
                    if (loanSchemeParameterViewModel.EnableTenureListParameter == true)
                    {
                        if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnableTenureList == true)
                        {
                            List<SchemeTenureListViewModel> schemeTenureListViewModelLists = (List<SchemeTenureListViewModel>)HttpContext.Current.Session["SchemeTenureList"];
                            if (schemeTenureListViewModelLists != null)
                            {
                                foreach (SchemeTenureListViewModel viewModel in schemeTenureListViewModelLists)
                                {
                                    result = schemeDbContextRepository.AttachSchemeTenureListData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }
                    }
                }

                // SchemeApplicationParameter
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeApplicationParameterViewModel.SchemeApplicationParameterPrmKey > 0)
                    {
                        if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnableApplication == true)
                            result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_loanSchemeViewModel.SchemeApplicationParameterViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_loanSchemeViewModel.SchemeApplicationParameterViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnableApplication == true)
                            result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_loanSchemeViewModel.SchemeApplicationParameterViewModel, StringLiteralValue.Create);
                    }
                }

                //Document
                //Amend Old  Record
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableDocumentParameter == true)
                    {
                        IEnumerable<SchemeDocumentViewModel> schemeDocumentViewModelListForAmend = await schemeDetailRepository.GetDocumentEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);
                        if (schemeDocumentViewModelListForAmend.Count() > 0)
                        {
                            foreach (SchemeDocumentViewModel viewModel in schemeDocumentViewModelListForAmend)
                            {
                                result = schemeDbContextRepository.AttachSchemeDocumentData(viewModel, StringLiteralValue.Amend);
                            }
                        }
                    }

                    //New Record
                    if (loanSchemeParameterViewModel.EnableDocumentParameter == true)
                    {
                        List<SchemeDocumentViewModel> schemeDocumentViewModelLists = (List<SchemeDocumentViewModel>)HttpContext.Current.Session["SchemeDocument"];
                        if (schemeDocumentViewModelLists != null)
                        {
                            foreach (SchemeDocumentViewModel viewModel in schemeDocumentViewModelLists)
                            {
                                result = schemeDbContextRepository.AttachSchemeDocumentData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //NoticeSchedule
                //Amend Old  Record
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableNoticeScheduleParameter == true)
                    {
                        IEnumerable<SchemeNoticeScheduleViewModel> schemeNoticeScheduleViewModelListForAmend = await schemeDetailRepository.GetNoticeScheduleEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                        if (schemeNoticeScheduleViewModelListForAmend.Count() > 0)
                        {
                            foreach (SchemeNoticeScheduleViewModel viewModel in schemeNoticeScheduleViewModelListForAmend)
                            {
                                result = schemeDbContextRepository.AttachSchemeNoticeScheduleData(viewModel, StringLiteralValue.Amend);
                            }
                        }
                    }

                    //New Record
                    if (loanSchemeParameterViewModel.EnableNoticeScheduleParameter == true)
                    {
                        List<SchemeNoticeScheduleViewModel> schemeNoticeScheduleViewModelLists = (List<SchemeNoticeScheduleViewModel>)HttpContext.Current.Session["SchemeNoticeSchedule"];

                        if (schemeNoticeScheduleViewModelLists != null)
                        {
                            foreach (SchemeNoticeScheduleViewModel viewModel in schemeNoticeScheduleViewModelLists)
                            {
                                result = schemeDbContextRepository.AttachSchemeNoticeScheduleData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }

                }

                //ReportFormat
                //Amend Old  Record
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableReportFormatParameter == true)
                    {
                        IEnumerable<SchemeReportFormatViewModel> schemeReportFormatViewModelListForAmend = await schemeDetailRepository.GetReportFormatEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);
                        if (schemeReportFormatViewModelListForAmend.Count() > 0)
                        {
                            foreach (SchemeReportFormatViewModel viewModel in schemeReportFormatViewModelListForAmend)
                            {
                                result = schemeDbContextRepository.AttachSchemeReportFormatData(viewModel, StringLiteralValue.Amend);
                            }
                        }
                    }

                    // New Record
                    if (loanSchemeParameterViewModel.EnableReportFormatParameter == true)
                    {
                        List<SchemeReportFormatViewModel> schemeReportFormatViewModelLists = (List<SchemeReportFormatViewModel>)HttpContext.Current.Session["SchemeReportFormat"];
                        if (schemeReportFormatViewModelLists != null)
                        {
                            foreach (SchemeReportFormatViewModel viewModel in schemeReportFormatViewModelLists)
                            {
                                result = schemeDbContextRepository.AttachSchemeReportFormatData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //EstimateTarget
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableTargetEstimationParameter == true)
                    {
                        result = schemeDbContextRepository.AttachSchemeEstimateTargetData(_loanSchemeViewModel.SchemeEstimateTargetViewModel, StringLiteralValue.Amend);
                    }
                }

                // SchemeGeneralLedger
                // Old Record Amended For Amened 
                if (result)
                {
                    IEnumerable<SchemeGeneralLedgerViewModel> schemeGeneralLedgerViewModelListForAmend = await schemeDetailRepository.GetGeneralLedgerEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                    if (schemeGeneralLedgerViewModelListForAmend != null)
                    {
                        foreach (SchemeGeneralLedgerViewModel viewModel in schemeGeneralLedgerViewModelListForAmend)
                        {
                            result = schemeDbContextRepository.AttachSchemeGeneralLedgerData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // New Record
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

                //BusinessOffice
                //Amend Old  Record
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableBusinessOfficeParameter == true)
                    {
                        IEnumerable<SchemeBusinessOfficeViewModel> schemeBusinessOfficeViewModelListForAmend = await schemeDetailRepository.GetBusinessOfficeEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);
                        if (schemeBusinessOfficeViewModelListForAmend.Count() > 0)
                        {
                            foreach (SchemeBusinessOfficeViewModel viewModel in schemeBusinessOfficeViewModelListForAmend)
                            {
                                result = schemeDbContextRepository.AttachSchemeBusinessOfficeData(viewModel, StringLiteralValue.Amend);
                            }
                        }
                    }

                    //New Record
                    if (loanSchemeParameterViewModel.EnableBusinessOfficeParameter == true)
                    {
                        List<SchemeBusinessOfficeViewModel> schemeBusinessOfficeViewModelLists = (List<SchemeBusinessOfficeViewModel>)HttpContext.Current.Session["SchemeBusinessOffice"];
                        if (schemeBusinessOfficeViewModelLists != null)
                        {
                            foreach (SchemeBusinessOfficeViewModel viewModel in schemeBusinessOfficeViewModelLists)
                            {
                                result = schemeDbContextRepository.AttachSchemeBusinessOfficeData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //SchemePassbook  
                if (result)
                {
                    if (_loanSchemeViewModel.SchemePassbookViewModel.SchemePassbookPrmKey > 0)
                    {
                        if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == true)
                            result = schemeDbContextRepository.AttachSchemePassbookData(_loanSchemeViewModel.SchemePassbookViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemePassbookData(_loanSchemeViewModel.SchemePassbookViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == true)
                            result = schemeDbContextRepository.AttachSchemePassbookData(_loanSchemeViewModel.SchemePassbookViewModel, StringLiteralValue.Create);
                    }
                }

                //Target Group
                //Amend Old  Record
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableTargetGroupParameter == true)
                    {
                        if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnableTargetGroup == true)
                        {
                            IEnumerable<SchemeTargetGroupViewModel> schemeTargetGroupViewModelListForAmend = await schemeDetailRepository.GetTargetGroupEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);
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
                    if (loanSchemeParameterViewModel.EnableTargetGroupParameter == true)
                    {
                        if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnableTargetGroup == true)
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

                // SchemeLoanRepaymentScheduleParameter   
                if (result)
                {
                    if (loanType != StringLiteralValue.CashCreditLoan)
                    {

                        if (_loanSchemeViewModel.SchemeLoanRepaymentScheduleParameterViewModel.SchemeLoanRepaymentScheduleParameterPrmKey > 0)
                        {
                            if ((loanType != StringLiteralValue.GoldLoan && loanType != StringLiteralValue.LoanAgainstDeposit) || (_loanSchemeViewModel.EnableLoanInstallment == true))
                            {
                                result = schemeDbContextRepository.AttachSchemeLoanRepaymentScheduleParameterData(_loanSchemeViewModel.SchemeLoanRepaymentScheduleParameterViewModel, StringLiteralValue.Amend);
                            }
                            else
                            {
                                result = schemeDbContextRepository.AttachSchemeLoanRepaymentScheduleParameterData(_loanSchemeViewModel.SchemeLoanRepaymentScheduleParameterViewModel, StringLiteralValue.Delete);
                            }
                        }
                        else
                        {
                            if (_loanSchemeViewModel.EnableLoanInstallment == true)
                            {
                                result = schemeDbContextRepository.AttachSchemeLoanRepaymentScheduleParameterData(_loanSchemeViewModel.SchemeLoanRepaymentScheduleParameterViewModel, StringLiteralValue.Create);
                            }
                        }

                    }
                }

                // SchemeLoanSettlementAccountParameter
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanSettlementAccountParameterViewModel.SchemeLoanSettlementAccountParameterPrmKey > 0)
                    {
                        if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableSettlementAccount == true)
                            result = schemeDbContextRepository.AttachSchemeLoanSettlementAccountParameterData(_loanSchemeViewModel.SchemeLoanSettlementAccountParameterViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeLoanSettlementAccountParameterData(_loanSchemeViewModel.SchemeLoanSettlementAccountParameterViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableSettlementAccount == true)
                            result = schemeDbContextRepository.AttachSchemeLoanSettlementAccountParameterData(_loanSchemeViewModel.SchemeLoanSettlementAccountParameterViewModel, StringLiteralValue.Create);
                    }
                }

                // SchemeLoanInterestParameter 
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanInterestParameterViewModel.SchemeLoanInterestParameterPrmKey > 0)
                        result = schemeDbContextRepository.AttachSchemeLoanInterestParameterData(_loanSchemeViewModel.SchemeLoanInterestParameterViewModel, StringLiteralValue.Amend);
                    else
                        result = schemeDbContextRepository.AttachSchemeLoanInterestParameterData(_loanSchemeViewModel.SchemeLoanInterestParameterViewModel, StringLiteralValue.Create);
                }

                // SchemeLoanSanctionAuthority 
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanSanctionAuthorityViewModel.SchemeLoanSanctionAuthorityPrmKey > 0)
                        result = schemeDbContextRepository.AttachSchemeLoanSanctionAuthorityData(_loanSchemeViewModel.SchemeLoanSanctionAuthorityViewModel, StringLiteralValue.Amend);
                    else
                        result = schemeDbContextRepository.AttachSchemeLoanSanctionAuthorityData(_loanSchemeViewModel.SchemeLoanSanctionAuthorityViewModel, StringLiteralValue.Create);
                }

                //LoanPaymentReminderParameter
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanPaymentReminderParameterViewModel.SchemeLoanPaymentReminderParameterPrmKey > 0)
                        result = schemeDbContextRepository.AttachSchemeLoanPaymentReminderParameterData(_loanSchemeViewModel.SchemeLoanPaymentReminderParameterViewModel, StringLiteralValue.Amend);
                    else
                        result = schemeDbContextRepository.AttachSchemeLoanPaymentReminderParameterData(_loanSchemeViewModel.SchemeLoanPaymentReminderParameterViewModel, StringLiteralValue.Create);
                }

                // SchemeLoanFineInterestParameter  
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanFineInterestParameterViewModel.SchemeLoanFineInterestParameterPrmKey > 0)
                    {
                        if (_loanSchemeViewModel.SchemeLoanInterestParameterViewModel.EnableLoanFineInterest == true)
                            result = schemeDbContextRepository.AttachSchemeLoanFineInterestParameterData(_loanSchemeViewModel.SchemeLoanFineInterestParameterViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeLoanFineInterestParameterData(_loanSchemeViewModel.SchemeLoanFineInterestParameterViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_loanSchemeViewModel.SchemeLoanInterestParameterViewModel.EnableLoanFineInterest == true)
                            result = schemeDbContextRepository.AttachSchemeLoanFineInterestParameterData(_loanSchemeViewModel.SchemeLoanFineInterestParameterViewModel, StringLiteralValue.Create);
                    }
                }

                // SchemeLoanInterestParameter   
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanInterestProvisionParameterViewModel.SchemeLoanInterestProvisionParameterPrmKey > 0)
                    {
                        if (_loanSchemeViewModel.SchemeLoanInterestParameterViewModel.EnableLoanInterestProvision == true)
                            result = schemeDbContextRepository.AttachSchemeLoanInterestProvisionParameterData(_loanSchemeViewModel.SchemeLoanInterestProvisionParameterViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeLoanInterestProvisionParameterData(_loanSchemeViewModel.SchemeLoanInterestProvisionParameterViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_loanSchemeViewModel.SchemeLoanInterestParameterViewModel.EnableLoanInterestProvision == true)
                            result = schemeDbContextRepository.AttachSchemeLoanInterestProvisionParameterData(_loanSchemeViewModel.SchemeLoanInterestProvisionParameterViewModel, StringLiteralValue.Create);
                    }
                }

                // SchemeLoanDistributorParameter
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanDistributorParameterViewModel.SchemeLoanDistributorParameterPrmKey > 0)
                    {
                        if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableDistributor == true)
                            result = schemeDbContextRepository.AttachSchemeLoanDistributorParameterData(_loanSchemeViewModel.SchemeLoanDistributorParameterViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeLoanDistributorParameterData(_loanSchemeViewModel.SchemeLoanDistributorParameterViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableDistributor == true)
                            result = schemeDbContextRepository.AttachSchemeLoanDistributorParameterData(_loanSchemeViewModel.SchemeLoanDistributorParameterViewModel, StringLiteralValue.Create);
                    }
                }

                // SchemeLoanArrearParameter 
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanArrearParameterViewModel.SchemeLoanArrearParameterPrmKey > 0)
                        result = schemeDbContextRepository.AttachSchemeLoanArrearParameterData(_loanSchemeViewModel.SchemeLoanArrearParameterViewModel, StringLiteralValue.Amend);
                    else
                        result = schemeDbContextRepository.AttachSchemeLoanArrearParameterData(_loanSchemeViewModel.SchemeLoanArrearParameterViewModel, StringLiteralValue.Create);
                }

                //LoanChargesParameter
                //Amend Old  Record
                if (result)
                {
                    IEnumerable<SchemeLoanChargesParameterViewModel> schemeLoanChargesParameterViewModelListForAmend = await schemeDetailRepository.GetLoanChargesParameterEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);
                    if (schemeLoanChargesParameterViewModelListForAmend.Count() > 0)
                    {
                        foreach (SchemeLoanChargesParameterViewModel viewModel in schemeLoanChargesParameterViewModelListForAmend)
                        {
                            result = schemeDbContextRepository.AttachSchemeLoanChargesParameterData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                    //New Record
                    List<SchemeLoanChargesParameterViewModel> schemeLoanChargesParameterViewModelLists = (List<SchemeLoanChargesParameterViewModel>)HttpContext.Current.Session["SchemeLoanChargesParameter"];
                    if (schemeLoanChargesParameterViewModelLists != null)
                    {
                        foreach (SchemeLoanChargesParameterViewModel viewModel in schemeLoanChargesParameterViewModelLists)
                        {
                            result = schemeDbContextRepository.AttachSchemeLoanChargesParameterData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // SchemeLoanInterestRebateParameter 
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanInterestRebateParameterViewModel.SchemeLoanInterestRebateParameterPrmKey > 0)
                    {
                        if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableRebateInterest == true)
                            result = schemeDbContextRepository.AttachSchemeLoanInterestRebateParameterData(_loanSchemeViewModel.SchemeLoanInterestRebateParameterViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeLoanInterestRebateParameterData(_loanSchemeViewModel.SchemeLoanInterestRebateParameterViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableRebateInterest == true)
                            result = schemeDbContextRepository.AttachSchemeLoanInterestRebateParameterData(_loanSchemeViewModel.SchemeLoanInterestRebateParameterViewModel, StringLiteralValue.Create);
                    }
                }

                //Installment Parameter
                if (result)
                {
                    if (loanType != StringLiteralValue.CashCreditLoan)
                    {

                        if (_loanSchemeViewModel.SchemeLoanInstallmentParameterViewModel.SchemeLoanInstallmentParameterPrmKey > 0)
                        {
                            if ((loanType != StringLiteralValue.GoldLoan && loanType != StringLiteralValue.LoanAgainstDeposit) || (_loanSchemeViewModel.EnableLoanInstallment == true))
                            {
                                result = schemeDbContextRepository.AttachSchemeLoanInstallmentParameterData(_loanSchemeViewModel.SchemeLoanInstallmentParameterViewModel, StringLiteralValue.Amend);
                            }
                            else
                            {
                                result = schemeDbContextRepository.AttachSchemeLoanInstallmentParameterData(_loanSchemeViewModel.SchemeLoanInstallmentParameterViewModel, StringLiteralValue.Delete);
                            }
                        }
                        else
                        {
                            if (_loanSchemeViewModel.EnableLoanInstallment == true)
                            {
                                result = schemeDbContextRepository.AttachSchemeLoanInstallmentParameterData(_loanSchemeViewModel.SchemeLoanInstallmentParameterViewModel, StringLiteralValue.Create);
                            }
                        }

                    }
                }

                //LoanFunder
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanFunderParameterViewModel.SchemeLoanFunderParameterPrmKey > 0)
                    {
                        if (loanSchemeParameterViewModel.EnableLoanFunderParameter == true)
                            result = schemeDbContextRepository.AttachSchemeLoanFunderParameterData(_loanSchemeViewModel.SchemeLoanFunderParameterViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeLoanFunderParameterData(_loanSchemeViewModel.SchemeLoanFunderParameterViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (loanSchemeParameterViewModel.EnableLoanFunderParameter == true)
                            result = schemeDbContextRepository.AttachSchemeLoanFunderParameterData(_loanSchemeViewModel.SchemeLoanFunderParameterViewModel, StringLiteralValue.Create);
                    }

                }

                //Loan Overdues Action
                //Amend Old  Record
                if (result)
                {
                    IEnumerable<SchemeLoanOverduesActionViewModel> schemeLoanOverduesActionViewModelListForAmend = await schemeDetailRepository.GetLoanOverduesActionEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);
                    if (schemeLoanOverduesActionViewModelListForAmend.Count() > 0)
                    {
                        foreach (SchemeLoanOverduesActionViewModel viewModel in schemeLoanOverduesActionViewModelListForAmend)
                        {
                            result = schemeDbContextRepository.AttachSchemeLoanOverduesActionData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    //New Record
                    List<SchemeLoanOverduesActionViewModel> schemeLoanOverduesActionViewModelLists = (List<SchemeLoanOverduesActionViewModel>)HttpContext.Current.Session["SchemeLoanOverduesAction"];
                    if (schemeLoanOverduesActionViewModelLists != null)
                    {
                        foreach (SchemeLoanOverduesActionViewModel viewModel in schemeLoanOverduesActionViewModelLists)
                        {
                            result = schemeDbContextRepository.AttachSchemeLoanOverduesActionData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // SchemePreFullPaymentParameter  
                if (result)
                {
                    if (loanType != StringLiteralValue.CashCreditLoan)
                    {
                        // Check Whether EnablePreFullPayment Is Enabled Or  Not
                        // If Enabled Then Amend Otherwise Delete
                        if (_loanSchemeViewModel.SchemeLoanPreFullPaymentParameterViewModel.SchemeLoanPreFullPaymentParameterPrmKey > 0)
                        {
                            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableForeClosure == true)
                            {
                                result = schemeDbContextRepository.AttachSchemePreFullPaymentParameterData(_loanSchemeViewModel.SchemeLoanPreFullPaymentParameterViewModel, StringLiteralValue.Amend);
                            }
                            else
                            {
                                result = schemeDbContextRepository.AttachSchemePreFullPaymentParameterData(_loanSchemeViewModel.SchemeLoanPreFullPaymentParameterViewModel, StringLiteralValue.Delete);
                            }
                        }
                        else
                        {
                            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableForeClosure == true)
                            {
                                result = schemeDbContextRepository.AttachSchemePreFullPaymentParameterData(_loanSchemeViewModel.SchemeLoanPreFullPaymentParameterViewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // SchemePrePartPaymentParameter
                if (result)
                {
                    if (loanType != StringLiteralValue.CashCreditLoan)
                    {
                        // Check Whether EnablePreFullPayment Is Enabled Or  Not
                        // If Enabled Then Amend Otherwise Delete

                        if (_loanSchemeViewModel.SchemeLoanPrePartPaymentParameterViewModel.SchemeLoanPrePartPaymentParameterPrmKey > 0)
                        {
                            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnablePrePayment == true)
                            {
                                result = schemeDbContextRepository.AttachSchemePrePartPaymentParameterData(_loanSchemeViewModel.SchemeLoanPrePartPaymentParameterViewModel, StringLiteralValue.Amend);
                            }
                            else
                            {
                                result = schemeDbContextRepository.AttachSchemePrePartPaymentParameterData(_loanSchemeViewModel.SchemeLoanPrePartPaymentParameterViewModel, StringLiteralValue.Delete);
                            }
                        }

                        else
                        {
                            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnablePrePayment == true)
                            {
                                result = schemeDbContextRepository.AttachSchemePrePartPaymentParameterData(_loanSchemeViewModel.SchemeLoanPrePartPaymentParameterViewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // Get Loan Type For Valid Tables Insertion
                if (result)
                {
                    // SchemeGoldLoan 
                    if (_loanSchemeViewModel.SchemeGoldLoanParameterViewModel.SchemeGoldLoanParameterPrmKey > 0)
                    {
                        if (loanType == StringLiteralValue.GoldLoan)
                            result = schemeDbContextRepository.AttachSchemeGoldLoanParameterData(_loanSchemeViewModel.SchemeGoldLoanParameterViewModel, StringLiteralValue.Amend);
                        else
                            result = schemeDbContextRepository.AttachSchemeGoldLoanParameterData(_loanSchemeViewModel.SchemeGoldLoanParameterViewModel, StringLiteralValue.Delete);
                    }
                    else
                    {
                        if (loanType == StringLiteralValue.GoldLoan)
                            result = schemeDbContextRepository.AttachSchemeGoldLoanParameterData(_loanSchemeViewModel.SchemeGoldLoanParameterViewModel, StringLiteralValue.Create);
                    }

                    // VehicleTypeLoanParameter amend Old Record
                    if (loanType == StringLiteralValue.VehicleLoan)
                    {
                        IEnumerable<SchemeVehicleTypeLoanParameterViewModel> schemeVehicleTypeLoanParameterViewModelListForAmend = await schemeDetailRepository.GetSchemeVehicleTypeLoanParameterEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                        if (schemeVehicleTypeLoanParameterViewModelListForAmend.Count() > 0)
                        {
                            foreach (SchemeVehicleTypeLoanParameterViewModel viewModel in schemeVehicleTypeLoanParameterViewModelListForAmend)
                            {
                                result = schemeDbContextRepository.AttachSchemeVehicleTypeLoanParameterData(viewModel, StringLiteralValue.Amend);
                            }
                        }

                        //PreownedVehicleLoanParameter
                        IEnumerable<SchemePreownedVehicleLoanParameterViewModel> schemePreownedVehicleLoanParameterViewModelListForAmend = await schemeDetailRepository.GetSchemePreownedVehicleLoanParameterEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                        if (schemePreownedVehicleLoanParameterViewModelListForAmend.Count() > 0)
                        {
                            foreach (SchemePreownedVehicleLoanParameterViewModel viewModel in schemePreownedVehicleLoanParameterViewModelListForAmend)
                            {
                                result = schemeDbContextRepository.AttachSchemePreownedVehicleLoanParameterData(viewModel, StringLiteralValue.Amend);
                            }
                        }


                        // VehicleTypeLoanParameter amend New Record

                        List<SchemeVehicleTypeLoanParameterViewModel> schemeVehicleTypeLoanParameterViewModelList = (List<SchemeVehicleTypeLoanParameterViewModel>)HttpContext.Current.Session["SchemeVehicleTypeLoanParameter"];

                        if (schemeVehicleTypeLoanParameterViewModelList != null)
                        {
                            foreach (SchemeVehicleTypeLoanParameterViewModel viewModel in schemeVehicleTypeLoanParameterViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeVehicleTypeLoanParameterData(viewModel, StringLiteralValue.Create);
                            }
                        }

                        //PreownedVehicleLoanParameter
                        List<SchemePreownedVehicleLoanParameterViewModel> schemePreownedVehicleLoanParameterViewModelList = (List<SchemePreownedVehicleLoanParameterViewModel>)HttpContext.Current.Session["SchemePreownedVehicleLoanParameter"];

                        if (schemePreownedVehicleLoanParameterViewModelList != null)
                        {
                            foreach (SchemePreownedVehicleLoanParameterViewModel viewModel in schemePreownedVehicleLoanParameterViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemePreownedVehicleLoanParameterData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }

                    // SchemeCashCreditLoanParameter 
                    if (result)
                    {
                        if (loanType == StringLiteralValue.CashCreditLoan)
                            result = schemeDbContextRepository.AttachSchemeCashCreditLoanParameterData(_loanSchemeViewModel.SchemeCashCreditLoanParameterViewModel, StringLiteralValue.Amend);
                    }

                    //EducationLoanParameter
                    if (loanType == StringLiteralValue.EducationalLoan)
                    {
                        result = schemeDbContextRepository.AttachSchemeEducationLoanParameterData(_loanSchemeViewModel.SchemeEducationLoanParameterViewModel, StringLiteralValue.Amend);

                        // SchemeEducationalCourse
                        if (_loanSchemeViewModel.SchemeEducationLoanParameterViewModel.IsApplicableAllCourse == true)
                        {
                            IEnumerable<SchemeEducationalCourseViewModel> schemeEducationalCourseViewModelForAmend = await schemeDetailRepository.GetSchemeEducationalCourseEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);
                            if (schemeEducationalCourseViewModelForAmend.Count() > 0)
                            {
                                foreach (SchemeEducationalCourseViewModel viewModel in schemeEducationalCourseViewModelForAmend)
                                {
                                    result = schemeDbContextRepository.AttachSchemeEducationalCourseData(viewModel, StringLiteralValue.Amend);
                                }
                            }
                        }
                        
                            if (_loanSchemeViewModel.SchemeEducationLoanParameterViewModel.IsApplicableAllCourse == true)
                            {
                                List<SchemeEducationalCourseViewModel> schemeEducationalCourseViewModelList = (List<SchemeEducationalCourseViewModel>)HttpContext.Current.Session["EducationalCourse"];

                                if (schemeEducationalCourseViewModelList != null)
                                {
                                    foreach (SchemeEducationalCourseViewModel viewModel in schemeEducationalCourseViewModelList)
                                    {
                                        result = schemeDbContextRepository.AttachSchemeEducationalCourseData(viewModel, StringLiteralValue.Create);
                                    }
                                }
                            }

                        // SchemeInstitute
                        if (_loanSchemeViewModel.SchemeEducationLoanParameterViewModel.IsApplicableAllUniversities == true)
                        {
                            IEnumerable<SchemeInstituteViewModel> schemeInstituteViewModelForAmend = await schemeDetailRepository.GetSchemeInstituteEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);
                            if (schemeInstituteViewModelForAmend.Count() > 0)
                            {
                                foreach (SchemeInstituteViewModel viewModel in schemeInstituteViewModelForAmend)
                                {
                                    result = schemeDbContextRepository.AttachSchemeInstituteData(viewModel, StringLiteralValue.Amend);
                                }
                            }
                        }
                       
                        if (_loanSchemeViewModel.SchemeEducationLoanParameterViewModel.IsApplicableAllUniversities == true)
                            {
                                List<SchemeInstituteViewModel> schemeInstituteViewModelList = (List<SchemeInstituteViewModel>)HttpContext.Current.Session["Institute"];

                                if (schemeInstituteViewModelList != null)
                                {
                                    foreach (SchemeInstituteViewModel viewModel in schemeInstituteViewModelList)
                                    {
                                        result = schemeDbContextRepository.AttachSchemeInstituteData(viewModel, StringLiteralValue.Create);
                                    }
                                }
                            }
                        }
                    

                    //SchemeLoanAgainstDepositParameter
                    if (loanType == StringLiteralValue.LoanAgainstDeposit)
                        {
                            result = schemeDbContextRepository.AttachSchemeLoanAgainstDepositParameterData(_loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel, StringLiteralValue.Amend);

                            // Amend Old Records
                            IEnumerable<SchemeLoanAgainstDepositGeneralLedgerViewModel> schemeLoanAgainstDepositGeneralLedgerViewModelAmend = await schemeDetailRepository.GetLoanAgainstDepositGeneralLedgerEntries(_loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.SchemeLoanAgainstDepositParameterPrmKey, StringLiteralValue.Reject);

                            if (schemeLoanAgainstDepositGeneralLedgerViewModelAmend.Count() > 0)
                            {
                                foreach (SchemeLoanAgainstDepositGeneralLedgerViewModel viewModel in schemeLoanAgainstDepositGeneralLedgerViewModelAmend)
                                {
                                    result = schemeDbContextRepository.AttachSchemeLoanAgainstDepositGeneralLedgerData(viewModel, StringLiteralValue.Amend);
                                }
                            }

                            // Create New Record
                            if (_loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.IsApplicableAllGeneralLedgers == false)
                            {
                                Guid[] generalLeadgerArray = _loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.MultiDepositeGeneralLedgerId;

                                foreach (Guid generalLeadgerId in generalLeadgerArray)
                                {
                                    SchemeLoanAgainstDepositGeneralLedgerViewModel viewModel = new SchemeLoanAgainstDepositGeneralLedgerViewModel
                                    {
                                        GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(generalLeadgerId)
                                    };

                                    viewModel.SchemeLoanAgainstDepositParameterPrmKey = _loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.SchemeLoanAgainstDepositParameterPrmKey;
                                    result = schemeDbContextRepository.AttachSchemeLoanAgainstDepositGeneralLedgerData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }

                        //SchemeConsumerDurableLoanItem amend Old Record
                        if (loanType == StringLiteralValue.ConsumerDurableLoan)
                        {
                            IEnumerable<SchemeConsumerDurableLoanItemViewModel> schemeConsumerDurableLoanItemViewModelListForAmend = await schemeDetailRepository.GetSchemeConsumerDurableLoanItemEntries(_loanSchemeViewModel.SchemePrmKey, StringLiteralValue.Reject);

                            if (schemeConsumerDurableLoanItemViewModelListForAmend.Count() > 0)
                            {
                                foreach (SchemeConsumerDurableLoanItemViewModel viewModel in schemeConsumerDurableLoanItemViewModelListForAmend)
                                {
                                    result = schemeDbContextRepository.AttachSchemeConsumerDurableLoanItemData(viewModel, StringLiteralValue.Amend);
                                }
                            }

                            //SchemeConsumerDurableLoanItem amend new Record

                            List<SchemeConsumerDurableLoanItemViewModel> schemeConsumerDurableLoanItemViewModelList = new List<SchemeConsumerDurableLoanItemViewModel>();
                            schemeConsumerDurableLoanItemViewModelList = (List<SchemeConsumerDurableLoanItemViewModel>)HttpContext.Current.Session["SchemeConsumerDurableLoanItem"];

                            if (schemeConsumerDurableLoanItemViewModelList != null)
                            {
                                foreach (SchemeConsumerDurableLoanItemViewModel viewModel in schemeConsumerDurableLoanItemViewModelList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeConsumerDurableLoanItemData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }

                        // SchemeHomeLoan 
                        if (result)
                        {
                            if (loanType == StringLiteralValue.HomeLoan)
                                result = schemeDbContextRepository.AttachSchemeHomeLoanData(_loanSchemeViewModel.SchemeHomeLoanViewModel, StringLiteralValue.Amend);

                        }
                        // SchemeLoanAgainstProperty 

                        if (result)
                        {
                            if (loanType == StringLiteralValue.LoanAgainstProperty)
                                result = schemeDbContextRepository.AttachSchemeLoanAgainstPropertyData(_loanSchemeViewModel.SchemeLoanAgainstPropertyViewModel, StringLiteralValue.Amend);
                        }

                        // SchemeBusinessLoan 
                        if (result)
                        {
                            if (loanType == StringLiteralValue.ShortTermBusinessLoan)
                                result = schemeDbContextRepository.AttachSchemeBusinessLoanData(_loanSchemeViewModel.SchemeBusinessLoanViewModel, StringLiteralValue.Amend);
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

        public async Task<IEnumerable<LoanSchemeIndexViewModel>> GetLoanSchemeIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<LoanSchemeIndexViewModel>("SELECT * FROM dbo.GetLoanSchemeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<LoanSchemeViewModel> GetLoanSchemeEntry(Guid _schemeId, string _entryType)
        {
            try
            {
                LoanSchemeViewModel loanSchemeViewModel = await context.Database.SqlQuery<LoanSchemeViewModel>("SELECT * FROM dbo.GetLoanSchemeEntry (@SchemeId, @EntriesType)", new SqlParameter("@SchemeId", _schemeId), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                short _schemePrmKey = GetPrmKeyById(_schemeId);
                loanSchemeViewModel.SchemeAccountParameterViewModel = await schemeDetailRepository.GetAccountParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanAccountParameterViewModel = await schemeDetailRepository.GetLoanAccountParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeAccountBankingChannelParameterViewModel = await schemeDetailRepository.GetAccountBankingChannelParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeCustomerAccountNumberViewModel = await schemeDetailRepository.GetCustomerAccountNumberEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeTenureViewModel = await schemeDetailRepository.GetTenureEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeApplicationParameterViewModel = await schemeDetailRepository.GetApplicationParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanAgreementNumberViewModel = await schemeDetailRepository.GetAgreementNumberEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemePassbookViewModel = await schemeDetailRepository.GetPassbookEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanRepaymentScheduleParameterViewModel = await schemeDetailRepository.GetLoanRepaymentScheduleParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanSettlementAccountParameterViewModel = await schemeDetailRepository.GetLoanSettlementAccountParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanInterestParameterViewModel = await schemeDetailRepository.GetLoanInterestParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanFineInterestParameterViewModel = await schemeDetailRepository.GetLoanFineInterestParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanInterestProvisionParameterViewModel = await schemeDetailRepository.GetLoanInterestProvisionParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanDistributorParameterViewModel = await schemeDetailRepository.GetLoanDistributorParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanArrearParameterViewModel = await schemeDetailRepository.GetLoanArrearParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanInterestRebateParameterViewModel = await schemeDetailRepository.GetLoanInterestRebateParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanFunderParameterViewModel = await schemeDetailRepository.GetLoanFunderParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanPreFullPaymentParameterViewModel = await schemeDetailRepository.GetPreFullPaymentParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanPrePartPaymentParameterViewModel = await schemeDetailRepository.GetPrePartPaymentParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeEstimateTargetViewModel = await schemeDetailRepository.GetEstimateTargetEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeGoldLoanParameterViewModel = await schemeDetailRepository.GetGoldLoanParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanInstallmentParameterViewModel = await schemeDetailRepository.GetLoanInstallmentParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanSanctionAuthorityViewModel = await schemeDetailRepository.GetLoanSanctionAuthorityEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeCashCreditLoanParameterViewModel = await schemeDetailRepository.GetSchemeCashCreditLoanParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanPaymentReminderParameterViewModel = await schemeDetailRepository.GetSchemeLoanPaymentReminderParameterEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel = await schemeDetailRepository.GetLoanAgainstDepositParameterEntry(_schemePrmKey, _entryType);

                if (loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel != null)
                {
                    int i = 0;

                    IEnumerable<SchemeLoanAgainstDepositGeneralLedgerViewModel> SchemeLoanAgainstDepositGeneralLedgerViewModelList = await schemeDetailRepository.GetLoanAgainstDepositGeneralLedgerEntries(loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.SchemeLoanAgainstDepositParameterPrmKey, _entryType);

                    Guid[] arr = new Guid[SchemeLoanAgainstDepositGeneralLedgerViewModelList.Count()];

                    foreach (SchemeLoanAgainstDepositGeneralLedgerViewModel viewmodel in SchemeLoanAgainstDepositGeneralLedgerViewModelList)
                    {
                        arr[i] = viewmodel.GeneralLedgerId;
                        i++;
                    }

                    loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.MultiDepositeGeneralLedgerId = arr;
                }

                loanSchemeViewModel.SchemeHomeLoanViewModel = await schemeDetailRepository.GetSchemeHomeLoanEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeLoanAgainstPropertyViewModel = await schemeDetailRepository.GetSchemeLoanAgainstPropertyEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeBusinessLoanViewModel = await schemeDetailRepository.GetSchemeBusinessLoanEntry(_schemePrmKey, _entryType);
                loanSchemeViewModel.SchemeEducationLoanParameterViewModel = await schemeDetailRepository.GetSchemeEducationLoanParameterEntry(_schemePrmKey, _entryType);

                // Managed For Loan Against Fixed Deposit And Gold Loan
                if (loanSchemeViewModel.SchemeLoanInstallmentParameterViewModel != null)
                {
                    loanSchemeViewModel.EnableLoanInstallment = true;
                }

                return loanSchemeViewModel;
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
                HttpContext.Current.Session["SchemeTenureList"] = await schemeDetailRepository.GetTenureListEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeDocument"] = await schemeDetailRepository.GetDocumentEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeGeneralLedger"] = await schemeDetailRepository.GetGeneralLedgerEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeNoticeSchedule"] = await schemeDetailRepository.GetNoticeScheduleEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeReportFormat"] = await schemeDetailRepository.GetReportFormatEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeBusinessOffice"] = await schemeDetailRepository.GetBusinessOfficeEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeTargetGroup"] = await schemeDetailRepository.GetTargetGroupEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeLoanChargesParameter"] = await schemeDetailRepository.GetLoanChargesParameterEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeLoanOverduesAction"] = await schemeDetailRepository.GetLoanOverduesActionEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeVehicleTypeLoanParameter"] = await schemeDetailRepository.GetSchemeVehicleTypeLoanParameterEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemePreownedVehicleLoanParameter"] = await schemeDetailRepository.GetSchemePreownedVehicleLoanParameterEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["SchemeConsumerDurableLoanItem"] = await schemeDetailRepository.GetSchemeConsumerDurableLoanItemEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["EducationalCourse"] = await schemeDetailRepository.GetSchemeEducationalCourseEntries(_schemePrmKey, _entryType);
                HttpContext.Current.Session["Institute"] = await schemeDetailRepository.GetSchemeInstituteEntries(_schemePrmKey, _entryType);


                //if (loanSchemeViewModel.SchemeVehicleTypeLoanParameterViewModel.EnablePhotoUploadInLocalStorage)
                //{
                //    loanSchemeViewModel.SchemeVehicleTypeLoanParameterViewModel.PhotoDocumentFormatTypeIdForDatabase = loanSchemeViewModel.SchemeVehicleTypeLoanParameterViewModel.AllowedFileFormatsForDb.Split(',');
                //}

                //////// Get Multiselect Id's From String (i.e. (Array) PhotoDocumentFormatTypeIdForLocalStorage From (String) PhotoDocumentAllowedFileFormatsForLocalStorage)
                //if (loanSchemeViewModel.SchemeVehicleTypeLoanParameterViewModel.EnablePhotoUploadInLocalStorage)
                //{
                //    loanSchemeViewModel.SchemeVehicleTypeLoanParameterViewModel.PhotoDocumentFormatTypeIdForLocalStorage = loanSchemeViewModel.SchemeVehicleTypeLoanParameterViewModel.AllowedFileFormatsForLocalStorage.Split(',');
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public short GetPrmKeyById(Guid _schemeId)
        {
            return context.Schemes
                    .Where(c => c.SchemeId == _schemeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
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

        public async Task<LoanSchemeViewModel> GetUnVerifiedEntry(Guid _schemeId)
        {
            try
            {
                LoanSchemeViewModel loanSchemeViewModel = await context.Database.SqlQuery<LoanSchemeViewModel>("SELECT * FROM dbo.GetLoanSchemeEntry (@SchemeId, @EntriesType)", new SqlParameter("@SchemeId", _schemeId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();

                return loanSchemeViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<LoanSchemeViewModel> GetVerifiedEntry(Guid _schemeId)
        {
            try
            {
                return await context.Database.SqlQuery<LoanSchemeViewModel>("SELECT * FROM dbo.GetLoanSchemeEntry (@SchemeId, @EntriesType)", new SqlParameter("@SchemeId", _schemeId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Save(LoanSchemeViewModel _loanSchemeViewModel)
        {
            try
            {
                LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
                string loanType = accountDetailRepository.GetSysNameOfLoanTypeByLoanTypeId(_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.LoanTypeId);

                bool result;

                result = schemeDbContextRepository.AttachLoanSchemeData(_loanSchemeViewModel, StringLiteralValue.Create);

                // AccountParameter
                if (result)
                    result = schemeDbContextRepository.AttachSchemeAccountParameterData(_loanSchemeViewModel.SchemeAccountParameterViewModel, StringLiteralValue.Create);

                // LoanAccountParameter
                if (result)
                    result = schemeDbContextRepository.AttachSchemeLoanAccountParameterData(_loanSchemeViewModel.SchemeLoanAccountParameterViewModel, StringLiteralValue.Create);

                //AccountBankingChannelParameter
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableBankingChannelParameter == true)
                        result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(_loanSchemeViewModel.SchemeAccountBankingChannelParameterViewModel, StringLiteralValue.Create);
                }

                // CustomerAccountNumber
                if (result)
                    result = schemeDbContextRepository.AttachSchemeCustomerAccountNumberData(_loanSchemeViewModel.SchemeCustomerAccountNumberViewModel, StringLiteralValue.Create);

                // LoanAgreementNumber 
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableAgreementNumber == true)
                        result = schemeDbContextRepository.AttachSchemeLoanAgreementNumberData(_loanSchemeViewModel.SchemeLoanAgreementNumberViewModel, StringLiteralValue.Create);
                }

                // SchemeTenure   
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnableTenure == true)
                        result = schemeDbContextRepository.AttachSchemeTenureData(_loanSchemeViewModel.SchemeTenureViewModel, StringLiteralValue.Create);
                }

                // SchemeTenureList
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableTenureListParameter == true)
                    {
                        {
                            List<SchemeTenureListViewModel> schemeTenureListViewModelList = (List<SchemeTenureListViewModel>)HttpContext.Current.Session["SchemeTenureList"];
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

                // ApplicationParameter
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableApplicationParameter == true)
                        result = schemeDbContextRepository.AttachSchemeApplicationParameterData(_loanSchemeViewModel.SchemeApplicationParameterViewModel, StringLiteralValue.Create);
                }

                // Document
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableDocumentParameter == true)
                    {
                        List<SchemeDocumentViewModel> schemeDocumentViewModelList = (List<SchemeDocumentViewModel>)HttpContext.Current.Session["SchemeDocument"];

                        if (schemeDocumentViewModelList != null)
                        {
                            foreach (SchemeDocumentViewModel viewModel in schemeDocumentViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeDocumentData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // NoticeSchedule
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableNoticeScheduleParameter == true)
                    {
                        if (loanSchemeParameterViewModel.EnableSmsServiceParameter == true || loanSchemeParameterViewModel.EnableEmailServiceParameter == true)
                        {
                            List<SchemeNoticeScheduleViewModel> schemeNoticeScheduleViewModelList = (List<SchemeNoticeScheduleViewModel>)HttpContext.Current.Session["SchemeNoticeSchedule"];

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

                // ReportFormat
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableReportFormatParameter == true)
                    {
                        List<SchemeReportFormatViewModel> schemeReportFormatViewModelList = (List<SchemeReportFormatViewModel>)HttpContext.Current.Session["SchemeReportFormat"];
                        if (schemeReportFormatViewModelList != null)
                        {
                            foreach (SchemeReportFormatViewModel viewModel in schemeReportFormatViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeReportFormatData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // EstimateTarget
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableTargetEstimationParameter == true)
                        result = schemeDbContextRepository.AttachSchemeEstimateTargetData(_loanSchemeViewModel.SchemeEstimateTargetViewModel, StringLiteralValue.Create);
                }

                // GeneralLedger
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

                // BusinessOffice
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableBusinessOfficeParameter == true)
                    {
                        List<SchemeBusinessOfficeViewModel> schemeBusinessOfficeViewModelList = (List<SchemeBusinessOfficeViewModel>)HttpContext.Current.Session["SchemeBusinessOffice"];

                        if (schemeBusinessOfficeViewModelList != null)
                        {
                            foreach (SchemeBusinessOfficeViewModel viewModel in schemeBusinessOfficeViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeBusinessOfficeData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // Passbook    
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnablePassbookParameter == true)
                        result = schemeDbContextRepository.AttachSchemePassbookData(_loanSchemeViewModel.SchemePassbookViewModel, StringLiteralValue.Create);
                }

                // TargetGroup
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableTargetGroupParameter == true)
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

                // LoanRepaymentScheduleParameter   
                if (result)
                {
                    if (loanType != StringLiteralValue.CashCreditLoan)
                    {
                        if ((loanType != StringLiteralValue.GoldLoan && loanType != StringLiteralValue.LoanAgainstDeposit) || (_loanSchemeViewModel.EnableLoanInstallment == true))
                        {
                            result = schemeDbContextRepository.AttachSchemeLoanRepaymentScheduleParameterData(_loanSchemeViewModel.SchemeLoanRepaymentScheduleParameterViewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // LoanSettlementAccountParameter   
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableSettlementAccount == true)
                        result = schemeDbContextRepository.AttachSchemeLoanSettlementAccountParameterData(_loanSchemeViewModel.SchemeLoanSettlementAccountParameterViewModel, StringLiteralValue.Create);
                }

                // LoanSanctionAuthority
                if (result)
                    result = schemeDbContextRepository.AttachSchemeLoanSanctionAuthorityData(_loanSchemeViewModel.SchemeLoanSanctionAuthorityViewModel, StringLiteralValue.Create);

                // LoanInterestParameter   
                if (result)
                    result = schemeDbContextRepository.AttachSchemeLoanInterestParameterData(_loanSchemeViewModel.SchemeLoanInterestParameterViewModel, StringLiteralValue.Create);

                // LoanFineInterestParameter   
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanInterestParameterViewModel.EnableLoanFineInterest == true)
                        result = schemeDbContextRepository.AttachSchemeLoanFineInterestParameterData(_loanSchemeViewModel.SchemeLoanFineInterestParameterViewModel, StringLiteralValue.Create);
                }

                // LoanFineInterestParameter   
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanInterestParameterViewModel.EnableLoanInterestProvision == true)
                        result = schemeDbContextRepository.AttachSchemeLoanInterestProvisionParameterData(_loanSchemeViewModel.SchemeLoanInterestProvisionParameterViewModel, StringLiteralValue.Create);
                }

                // LoanDistributorParameter
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableLoanDistributorParameter == true)
                        result = schemeDbContextRepository.AttachSchemeLoanDistributorParameterData(_loanSchemeViewModel.SchemeLoanDistributorParameterViewModel, StringLiteralValue.Create);
                }

                // LoanArrearParameter  
                if (result)
                    result = schemeDbContextRepository.AttachSchemeLoanArrearParameterData(_loanSchemeViewModel.SchemeLoanArrearParameterViewModel, StringLiteralValue.Create);

                // LoanChargesParameter
                if (result)
                {
                    List<SchemeLoanChargesParameterViewModel> schemeLoanChargesParameterViewModelList = (List<SchemeLoanChargesParameterViewModel>)HttpContext.Current.Session["SchemeLoanChargesParameter"];
                    if (schemeLoanChargesParameterViewModelList != null)
                    {
                        foreach (SchemeLoanChargesParameterViewModel viewModel in schemeLoanChargesParameterViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeLoanChargesParameterData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // LoanInterestRebateParameter    
                if (result)
                {
                    if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableRebateInterest == true)
                        result = schemeDbContextRepository.AttachSchemeLoanInterestRebateParameterData(_loanSchemeViewModel.SchemeLoanInterestRebateParameterViewModel, StringLiteralValue.Create);
                }

                // LoanInstallmentParameter
                if (result)
                {
                    if (loanType != StringLiteralValue.CashCreditLoan)
                    {
                        if ((loanType != StringLiteralValue.GoldLoan && loanType != StringLiteralValue.LoanAgainstDeposit) || (_loanSchemeViewModel.EnableLoanInstallment == true))
                        {
                            result = schemeDbContextRepository.AttachSchemeLoanInstallmentParameterData(_loanSchemeViewModel.SchemeLoanInstallmentParameterViewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //LoanFunderParameter
                if (result)
                {
                    if (loanSchemeParameterViewModel.EnableLoanFunderParameter == true)
                        result = schemeDbContextRepository.AttachSchemeLoanFunderParameterData(_loanSchemeViewModel.SchemeLoanFunderParameterViewModel, StringLiteralValue.Create);
                }

                // LoanOverduesAction
                if (result)
                {
                    List<SchemeLoanOverduesActionViewModel> schemeLoanOverduesActionViewModelList = (List<SchemeLoanOverduesActionViewModel>)HttpContext.Current.Session["SchemeLoanOverduesAction"];

                    if (schemeLoanOverduesActionViewModelList != null)
                    {
                        foreach (SchemeLoanOverduesActionViewModel viewModel in schemeLoanOverduesActionViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeLoanOverduesActionData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // PreFullPaymentParameter  
                if (loanType != StringLiteralValue.CashCreditLoan)
                {
                    if (result)
                    {
                        if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableForeClosure == true)
                            result = schemeDbContextRepository.AttachSchemePreFullPaymentParameterData(_loanSchemeViewModel.SchemeLoanPreFullPaymentParameterViewModel, StringLiteralValue.Create);
                    }
                }

                // PrePartPaymentParameter 
                if (loanType != StringLiteralValue.CashCreditLoan)
                {
                    if (result)
                    {
                        if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnablePrePayment == true)
                            result = schemeDbContextRepository.AttachSchemePrePartPaymentParameterData(_loanSchemeViewModel.SchemeLoanPrePartPaymentParameterViewModel, StringLiteralValue.Create);
                    }
                }

                //LoanPaymentReminderParameter
                if (result)
                    result = schemeDbContextRepository.AttachSchemeLoanPaymentReminderParameterData(_loanSchemeViewModel.SchemeLoanPaymentReminderParameterViewModel, StringLiteralValue.Create);

                // Get Loan Type For Valid Tables Insertion
                if (result)
                {
                    // GoldLoanParameter 
                    if (loanType == StringLiteralValue.GoldLoan)
                    {
                        if (result)
                            result = schemeDbContextRepository.AttachSchemeGoldLoanParameterData(_loanSchemeViewModel.SchemeGoldLoanParameterViewModel, StringLiteralValue.Create);
                    }

                    //VehicleTypeLoanParameter
                    if (loanType == StringLiteralValue.VehicleLoan)
                    {
                        List<SchemeVehicleTypeLoanParameterViewModel> schemeVehicleTypeLoanParameterViewModelList = (List<SchemeVehicleTypeLoanParameterViewModel>)HttpContext.Current.Session["SchemeVehicleTypeLoanParameter"];

                        if (schemeVehicleTypeLoanParameterViewModelList != null)
                        {
                            foreach (SchemeVehicleTypeLoanParameterViewModel viewModel in schemeVehicleTypeLoanParameterViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeVehicleTypeLoanParameterData(viewModel, StringLiteralValue.Create);
                            }
                        }

                        //PreownedVehicleLoanParameter
                        List<SchemePreownedVehicleLoanParameterViewModel> schemePreownedVehicleLoanParameterViewModelList = (List<SchemePreownedVehicleLoanParameterViewModel>)HttpContext.Current.Session["SchemePreownedVehicleLoanParameter"];

                        if (schemePreownedVehicleLoanParameterViewModelList != null)
                        {
                            foreach (SchemePreownedVehicleLoanParameterViewModel viewModel in schemePreownedVehicleLoanParameterViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemePreownedVehicleLoanParameterData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }

                    //CashCreditLoanParameter
                    if (loanType == StringLiteralValue.CashCreditLoan)
                    {
                        result = schemeDbContextRepository.AttachSchemeCashCreditLoanParameterData(_loanSchemeViewModel.SchemeCashCreditLoanParameterViewModel, StringLiteralValue.Create);
                    }

                    //EducationLoanParameter
                    if (loanType == StringLiteralValue.EducationalLoan)
                    {
                        result = schemeDbContextRepository.AttachSchemeEducationLoanParameterData(_loanSchemeViewModel.SchemeEducationLoanParameterViewModel, StringLiteralValue.Create);

                        if (_loanSchemeViewModel.SchemeEducationLoanParameterViewModel.IsApplicableAllCourse)
                        {
                            //(SchemeEducationalCourse
                            List<SchemeEducationalCourseViewModel> schemeEducationalCourseViewModellList = (List<SchemeEducationalCourseViewModel>)HttpContext.Current.Session["EducationalCourse"];

                            if (schemeEducationalCourseViewModellList != null)
                            {
                                foreach (SchemeEducationalCourseViewModel viewModel in schemeEducationalCourseViewModellList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeEducationalCourseData(viewModel, StringLiteralValue.Create);
                                }
                            }

                        }

                        if (_loanSchemeViewModel.SchemeEducationLoanParameterViewModel.IsApplicableAllUniversities)
                        {
                            //SchemeInstitute
                            List<SchemeInstituteViewModel> schemeInstituteViewModellList = (List<SchemeInstituteViewModel>)HttpContext.Current.Session["Institute"];

                            if (schemeInstituteViewModellList != null)
                            {
                                foreach (SchemeInstituteViewModel viewModel in schemeInstituteViewModellList)
                                {
                                    result = schemeDbContextRepository.AttachSchemeInstituteData(viewModel, StringLiteralValue.Create);
                                }
                            }

                        }
                    }

                    //ConsumerDurableLoanItemViewModel

                    if (loanType == StringLiteralValue.ConsumerDurableLoan)
                    {
                        List<SchemeConsumerDurableLoanItemViewModel> schemeConsumerDurableLoanItemViewModelList = (List<SchemeConsumerDurableLoanItemViewModel>)HttpContext.Current.Session["SchemeConsumerDurableLoanItem"];

                        if (schemeConsumerDurableLoanItemViewModelList != null)
                        {
                            foreach (SchemeConsumerDurableLoanItemViewModel viewModel in schemeConsumerDurableLoanItemViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeConsumerDurableLoanItemData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }

                    // Loan Against Fixed Deposit
                    if (loanType == StringLiteralValue.LoanAgainstDeposit)
                    {
                        //SchemeLoanAgainstDepositParameter
                        result = schemeDbContextRepository.AttachSchemeLoanAgainstDepositParameterData(_loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel, StringLiteralValue.Create);

                        if (_loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.IsApplicableAllGeneralLedgers == false)
                        {
                            Guid[] generalLeadgerArray = _loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.MultiDepositeGeneralLedgerId;

                            foreach (Guid generalLeadgerId in generalLeadgerArray)
                            {
                                // _loanSchemeViewModel.SchemeLoanAgainstDepositGeneralLedgerViewModel.SchemeLoanAgainstDepositPrmKey = _loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.PrmKey;
                                SchemeLoanAgainstDepositGeneralLedgerViewModel viewModel = new SchemeLoanAgainstDepositGeneralLedgerViewModel
                                {
                                    GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(generalLeadgerId)
                                };
                                result = schemeDbContextRepository.AttachSchemeLoanAgainstDepositGeneralLedgerData(viewModel, StringLiteralValue.Create);
                            }
                        }

                    }

                    //SchemeHomeLoan
                    if (loanType == StringLiteralValue.HomeLoan)
                    {
                        result = schemeDbContextRepository.AttachSchemeHomeLoanData(_loanSchemeViewModel.SchemeHomeLoanViewModel, StringLiteralValue.Create);
                    }

                    //SchemeLoanAgainstProperty
                    if (loanType == StringLiteralValue.LoanAgainstProperty)
                    {
                        result = schemeDbContextRepository.AttachSchemeLoanAgainstPropertyData(_loanSchemeViewModel.SchemeLoanAgainstPropertyViewModel, StringLiteralValue.Create);
                    }

                    if (loanType == StringLiteralValue.ShortTermBusinessLoan)
                    {
                        result = schemeDbContextRepository.AttachSchemeBusinessLoanData(_loanSchemeViewModel.SchemeBusinessLoanViewModel, StringLiteralValue.Create);
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

        public async Task<bool> VerifyRejectDelete(LoanSchemeViewModel _loanSchemeViewModel, string _entryType)
        {
            string entriesType;

            if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
                entriesType = StringLiteralValue.Unverified;
            else
                entriesType = StringLiteralValue.Reject;

            try
            {
                LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
                string loanType = accountDetailRepository.GetSysNameOfLoanTypeByLoanTypeId(_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.LoanTypeId);

                bool result;

                result = schemeDbContextRepository.AttachLoanSchemeData(_loanSchemeViewModel, _entryType);

                // SchemeAccountParameter
                if (result)
                {
                    SchemeAccountParameterViewModel schemeAccountParameterViewModel = await schemeDetailRepository.GetAccountParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeAccountParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeAccountParameterData(schemeAccountParameterViewModel, _entryType);

                }

                // SchemeLoanAccountParameter
                if (result)
                {
                    SchemeLoanAccountParameterViewModel schemeLoanAccountParameterViewModel = await schemeDetailRepository.GetLoanAccountParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLoanAccountParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLoanAccountParameterData(schemeLoanAccountParameterViewModel, _entryType);
                }

                // SchemeAccountBankingChannelParameter 
                if (result)
                {
                    SchemeAccountBankingChannelParameterViewModel schemeAccountBankingChannelParameterViewModel = await schemeDetailRepository.GetAccountBankingChannelParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeAccountBankingChannelParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeAccountBankingChannelParameterData(schemeAccountBankingChannelParameterViewModel, _entryType);
                }

                // SchemeCustomerAccountNumber 
                if (result)
                {
                    SchemeCustomerAccountNumberViewModel schemeCustomerAccountNumberViewModel = await schemeDetailRepository.GetCustomerAccountNumberEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeCustomerAccountNumberViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeCustomerAccountNumberData(schemeCustomerAccountNumberViewModel, _entryType);
                }

                // SchemeTenure    
                if (result)
                {
                    SchemeTenureViewModel schemeTenureViewModel = await schemeDetailRepository.GetTenureEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeTenureViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeTenureData(schemeTenureViewModel, _entryType);
                }

                // SchemeTenureList
                if (result)
                {
                    IEnumerable<SchemeTenureListViewModel> schemeTenureListViewModelList = await schemeDetailRepository.GetTenureListEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeTenureListViewModelList != null)
                    {
                        foreach (SchemeTenureListViewModel viewModel in schemeTenureListViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeTenureListData(viewModel, _entryType);
                        }
                    }
                }

                // SchemeApplicationParameter
                if (result)
                {
                    SchemeApplicationParameterViewModel schemeApplicationParameterViewModel = await schemeDetailRepository.GetApplicationParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeApplicationParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeApplicationParameterData(schemeApplicationParameterViewModel, _entryType);
                }

                // schemeLoanAgreementNumber
                if (result)
                {
                    SchemeLoanAgreementNumberViewModel schemeLoanAgreementNumberViewModel = await schemeDetailRepository.GetLoanAgreementNumberEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLoanAgreementNumberViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLoanAgreementNumberData(schemeLoanAgreementNumberViewModel, _entryType);
                }

                // SchemeDocument
                if (result)
                {
                    IEnumerable<SchemeDocumentViewModel> schemeDocumentViewModelList = await schemeDetailRepository.GetDocumentEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeDocumentViewModelList != null)
                    {
                        foreach (SchemeDocumentViewModel viewModel in schemeDocumentViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeDocumentData(viewModel, _entryType);
                        }
                    }
                }

                // SchemeNoticeSchedule
                if (result)
                {
                    IEnumerable<SchemeNoticeScheduleViewModel> schemeNoticeScheduleViewModelList = await schemeDetailRepository.GetNoticeScheduleEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeNoticeScheduleViewModelList != null)
                    {
                        foreach (SchemeNoticeScheduleViewModel viewModel in schemeNoticeScheduleViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeNoticeScheduleData(viewModel, _entryType);
                        }
                    }
                }

                // SchemeReportFormat
                if (result)
                {
                    IEnumerable<SchemeReportFormatViewModel> schemeReportFormatViewModelList = await schemeDetailRepository.GetReportFormatEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeReportFormatViewModelList != null)
                    {
                        foreach (SchemeReportFormatViewModel viewModel in schemeReportFormatViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeReportFormatData(viewModel, _entryType);
                        }
                    }
                }

                // SchemeEstimateTarget
                if (result)
                {
                    SchemeEstimateTargetViewModel schemeEstimateTargetViewModel = await schemeDetailRepository.GetEstimateTargetEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeEstimateTargetViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeEstimateTargetData(schemeEstimateTargetViewModel, _entryType);
                }

                // SchemeGeneralLedger
                if (result)
                {
                    IEnumerable<SchemeGeneralLedgerViewModel> schemeGeneralLedgerViewModelList = await schemeDetailRepository.GetGeneralLedgerEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

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
                    IEnumerable<SchemeBusinessOfficeViewModel> schemeBusinessOfficeViewModelList = await schemeDetailRepository.GetBusinessOfficeEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeBusinessOfficeViewModelList != null)
                    {
                        foreach (SchemeBusinessOfficeViewModel viewModel in schemeBusinessOfficeViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeBusinessOfficeData(viewModel, _entryType);
                        }
                    }
                }

                // SchemePassbook   
                if (result)
                {
                    SchemePassbookViewModel schemePassbookViewModel = await schemeDetailRepository.GetPassbookEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemePassbookViewModel != null)
                        result = schemeDbContextRepository.AttachSchemePassbookData(schemePassbookViewModel, _entryType);
                }

                // SchemeTargetGroup 
                if (result)
                {
                    IEnumerable<SchemeTargetGroupViewModel> schemeTargetGroupViewModelList = await schemeDetailRepository.GetTargetGroupEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeTargetGroupViewModelList != null)
                    {
                        foreach (SchemeTargetGroupViewModel viewModel in schemeTargetGroupViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeTargetGroupData(viewModel, _entryType);
                        }
                    }
                }

                // SchemeLoanRepaymentScheduleParameter   
                if (result)
                {
                    if (loanType != StringLiteralValue.CashCreditLoan)
                    {
                        if ((loanType != StringLiteralValue.GoldLoan && loanType != StringLiteralValue.LoanAgainstDeposit) || (_loanSchemeViewModel.EnableLoanInstallment == true))
                        {
                            SchemeLoanRepaymentScheduleParameterViewModel schemeLoanRepaymentScheduleParameterViewModel = await schemeDetailRepository.GetLoanRepaymentScheduleParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                            if (schemeLoanRepaymentScheduleParameterViewModel != null)
                                result = schemeDbContextRepository.AttachSchemeLoanRepaymentScheduleParameterData(schemeLoanRepaymentScheduleParameterViewModel, _entryType);
                        }
                    }
                }

                // SchemeLoanSettlementAccountParameter   
                if (result)
                {
                    SchemeLoanSettlementAccountParameterViewModel schemeLoanSettlementAccountParameterViewModel = await schemeDetailRepository.GetLoanSettlementAccountParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLoanSettlementAccountParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLoanSettlementAccountParameterData(schemeLoanSettlementAccountParameterViewModel, _entryType);
                }

                // SchemeLoanInterestParameter  
                if (result)
                {
                    SchemeLoanInterestParameterViewModel schemeLoanInterestParameterViewModel = await schemeDetailRepository.GetLoanInterestParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLoanInterestParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLoanInterestParameterData(schemeLoanInterestParameterViewModel, _entryType);
                }

                // SchemeLoanSanctionAuthority 
                if (result)
                {
                    SchemeLoanSanctionAuthorityViewModel schemeLoanSanctionAuthorityViewModel = await schemeDetailRepository.GetLoanSanctionAuthorityEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);
                    if (schemeLoanSanctionAuthorityViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLoanSanctionAuthorityData(schemeLoanSanctionAuthorityViewModel, _entryType);
                }

                //LoanPaymentReminderParameter
                if (result)
                {
                    SchemeLoanPaymentReminderParameterViewModel schemeLoanPaymentReminderParameterViewModel = await schemeDetailRepository.GetSchemeLoanPaymentReminderParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);
                    if (schemeLoanPaymentReminderParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLoanPaymentReminderParameterData(schemeLoanPaymentReminderParameterViewModel, _entryType);
                }

                // SchemeLoanFineInterestParameter  
                if (result)
                {
                    SchemeLoanFineInterestParameterViewModel schemeLoanFineInterestParameterViewModel = await schemeDetailRepository.GetLoanFineInterestParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLoanFineInterestParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLoanFineInterestParameterData(schemeLoanFineInterestParameterViewModel, _entryType);
                }

                // SchemeLoanInterestProvisionParameter  
                if (result)
                {
                    SchemeLoanInterestProvisionParameterViewModel schemeLoanInterestProvisionParameterViewModel = await schemeDetailRepository.GetLoanInterestProvisionParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLoanInterestProvisionParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLoanInterestProvisionParameterData(schemeLoanInterestProvisionParameterViewModel, _entryType);
                }

                // SchemeLoanDistributorParameter
                if (result)
                {
                    SchemeLoanDistributorParameterViewModel schemeLoanDistributorParameterViewModel = await schemeDetailRepository.GetLoanDistributorParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLoanDistributorParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLoanDistributorParameterData(schemeLoanDistributorParameterViewModel, _entryType);
                }

                // SchemeLoanArrearParameter  
                if (result)
                {
                    SchemeLoanArrearParameterViewModel schemeLoanArrearParameterViewModel = await schemeDetailRepository.GetLoanArrearParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLoanArrearParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLoanArrearParameterData(schemeLoanArrearParameterViewModel, _entryType);
                }

                // SchemeLoanChargesParameter
                if (result)
                {
                    IEnumerable<SchemeLoanChargesParameterViewModel> schemeLoanChargesParameterViewModelList = await schemeDetailRepository.GetLoanChargesParameterEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLoanChargesParameterViewModelList != null)
                    {
                        foreach (SchemeLoanChargesParameterViewModel viewModel in schemeLoanChargesParameterViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeLoanChargesParameterData(viewModel, _entryType);
                        }
                    }
                }

                // SchemeLoanInterestRebateParameter   
                if (result)
                {
                    SchemeLoanInterestRebateParameterViewModel schemeLoanInterestRebateParameterViewModel = await schemeDetailRepository.GetLoanInterestRebateParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLoanInterestRebateParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLoanInterestRebateParameterData(schemeLoanInterestRebateParameterViewModel, _entryType);
                }

                // SchemeLoanInstallmentParameter
                if (result)
                {
                    if (loanType != StringLiteralValue.CashCreditLoan)
                    {
                        if ((loanType != StringLiteralValue.GoldLoan && loanType != StringLiteralValue.LoanAgainstDeposit) || (_loanSchemeViewModel.EnableLoanInstallment == true))
                        {
                            SchemeLoanInstallmentParameterViewModel schemeLoanInstallmentParameterViewModel = await schemeDetailRepository.GetLoanInstallmentParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                            if (schemeLoanInstallmentParameterViewModel != null)
                            {
                                result = schemeDbContextRepository.AttachSchemeLoanInstallmentParameterData(schemeLoanInstallmentParameterViewModel, _entryType);
                            }
                        }
                    }
                }

                // SchemeLoanFunderParameter
                if (result)
                {
                    SchemeLoanFunderParameterViewModel schemeLoanFunderParameterViewModel = await schemeDetailRepository.GetLoanFunderParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLoanFunderParameterViewModel != null)
                        result = schemeDbContextRepository.AttachSchemeLoanFunderParameterData(schemeLoanFunderParameterViewModel, _entryType);
                }

                // SchemeLoanOverduesAction
                if (result)
                {
                    IEnumerable<SchemeLoanOverduesActionViewModel> schemeLoanOverduesActionViewModelList = await schemeDetailRepository.GetLoanOverduesActionEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                    if (schemeLoanOverduesActionViewModelList != null)
                    {
                        foreach (SchemeLoanOverduesActionViewModel viewModel in schemeLoanOverduesActionViewModelList)
                        {
                            result = schemeDbContextRepository.AttachSchemeLoanOverduesActionData(viewModel, _entryType);
                        }
                    }
                }

                // SchemePreFullPaymentParameter  
                if (result)
                {
                    if (loanType != StringLiteralValue.CashCreditLoan)
                    {
                        SchemeLoanPreFullPaymentParameterViewModel SchemeLoanPreFullPaymentParameterViewModel = await schemeDetailRepository.GetPreFullPaymentParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                        if (SchemeLoanPreFullPaymentParameterViewModel != null)
                            result = schemeDbContextRepository.AttachSchemePreFullPaymentParameterData(SchemeLoanPreFullPaymentParameterViewModel, _entryType);
                    }
                }

                // SchemePrePartPaymentParameter 
                if (result)
                {
                    if (loanType != StringLiteralValue.CashCreditLoan)
                    {
                        SchemeLoanPrePartPaymentParameterViewModel SchemeLoanPrePartPaymentParameterViewModel = await schemeDetailRepository.GetPrePartPaymentParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                        if (SchemeLoanPrePartPaymentParameterViewModel != null)
                            result = schemeDbContextRepository.AttachSchemePrePartPaymentParameterData(SchemeLoanPrePartPaymentParameterViewModel, _entryType);
                    }
                }

                // Gold Loan
                if (result)
                {
                    // GoldLoanParameter 
                    if (loanType == StringLiteralValue.GoldLoan)
                    {
                        SchemeGoldLoanParameterViewModel schemeGoldLoanParameterViewModel = await schemeDetailRepository.GetGoldLoanParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeGoldLoanParameterViewModel != null)
                            result = schemeDbContextRepository.AttachSchemeGoldLoanParameterData(schemeGoldLoanParameterViewModel, _entryType);
                    }

                    //ConsumerDurableLoanItem
                    if (loanType == StringLiteralValue.ConsumerDurableLoan)
                    {
                        IEnumerable<SchemeConsumerDurableLoanItemViewModel> schemeConsumerDurableLoanItemViewModelList = await schemeDetailRepository.GetSchemeConsumerDurableLoanItemEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeConsumerDurableLoanItemViewModelList != null)
                        {
                            foreach (SchemeConsumerDurableLoanItemViewModel viewModel in schemeConsumerDurableLoanItemViewModelList)
                            {
                                result = schemeDbContextRepository.AttachSchemeConsumerDurableLoanItemData(viewModel, _entryType);
                            }
                        }
                    }

                    //VehicleTypeLoanParameter
                    if (loanType == StringLiteralValue.VehicleLoan)
                    {
                        IEnumerable<SchemeVehicleTypeLoanParameterViewModel> schemeVehicleTypeLoanParameterViewModel = await schemeDetailRepository.GetSchemeVehicleTypeLoanParameterEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeVehicleTypeLoanParameterViewModel != null)
                        {
                            foreach (SchemeVehicleTypeLoanParameterViewModel viewModel in schemeVehicleTypeLoanParameterViewModel)
                            {
                                result = schemeDbContextRepository.AttachSchemeVehicleTypeLoanParameterData(viewModel, _entryType);
                            }
                        }

                        //PreownedVehicleLoanParameter
                        if (result)
                        {
                            IEnumerable<SchemePreownedVehicleLoanParameterViewModel> schemePreownedVehicleLoanParameterViewModel = await schemeDetailRepository.GetSchemePreownedVehicleLoanParameterEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                            if (schemePreownedVehicleLoanParameterViewModel != null)
                            {
                                foreach (SchemePreownedVehicleLoanParameterViewModel viewModel in schemePreownedVehicleLoanParameterViewModel)
                                {
                                    result = schemeDbContextRepository.AttachSchemePreownedVehicleLoanParameterData(viewModel, _entryType);
                                }
                            }
                        }
                    }

                    //LoanAgainstDepositParameter
                    if (loanType == StringLiteralValue.LoanAgainstDeposit)
                    {
                        SchemeLoanAgainstDepositParameterViewModel schemeLoanAgainstDepositParameterViewModel = await schemeDetailRepository.GetLoanAgainstDepositParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);

                        if (schemeLoanAgainstDepositParameterViewModel != null)
                            result = schemeDbContextRepository.AttachSchemeLoanAgainstDepositParameterData(schemeLoanAgainstDepositParameterViewModel, _entryType);

                        // SchemeLoanAgainstDepositGeneralLedger
                        if (_loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.IsApplicableAllGeneralLedgers == false)
                        {
                            IEnumerable<SchemeLoanAgainstDepositGeneralLedgerViewModel> schemeLoanAgainstDepositGeneralLedgerViewModel = await schemeDetailRepository.GetLoanAgainstDepositGeneralLedgerEntries(_loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.SchemeLoanAgainstDepositParameterPrmKey, entriesType);

                            foreach (SchemeLoanAgainstDepositGeneralLedgerViewModel viewModel in schemeLoanAgainstDepositGeneralLedgerViewModel)
                            {
                                result = schemeDbContextRepository.AttachSchemeLoanAgainstDepositGeneralLedgerData(viewModel, _entryType);
                            }
                        }

                    }

                    // SchemeCashCreditLoanParameter
                    if (loanType == StringLiteralValue.CashCreditLoan)
                    {
                        SchemeCashCreditLoanParameterViewModel schemeCashCreditLoanParameterViewModel = await schemeDetailRepository.GetSchemeCashCreditLoanParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);
                        if (schemeCashCreditLoanParameterViewModel != null)
                            result = schemeDbContextRepository.AttachSchemeCashCreditLoanParameterData(schemeCashCreditLoanParameterViewModel, _entryType);
                    }

                    //EducationLoanParameter
                    if (loanType == StringLiteralValue.EducationalLoan)
                    {
                        SchemeEducationLoanParameterViewModel schemeEducationLoanParameterViewModel = await schemeDetailRepository.GetSchemeEducationLoanParameterEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);
                        if (schemeEducationLoanParameterViewModel != null)
                            result = schemeDbContextRepository.AttachSchemeEducationLoanParameterData(schemeEducationLoanParameterViewModel, _entryType);


                        // SchemeEducationalCourse
                        if (_loanSchemeViewModel.SchemeEducationLoanParameterViewModel.IsApplicableAllCourse == true)
                        {
                            IEnumerable<SchemeEducationalCourseViewModel> schemeEducationalCourseViewModel = await schemeDetailRepository.GetSchemeEducationalCourseEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                            foreach (SchemeEducationalCourseViewModel viewModel in schemeEducationalCourseViewModel)
                            {
                                result = schemeDbContextRepository.AttachSchemeEducationalCourseData(viewModel, _entryType);
                            }
                        }

                        // SchemeInstitute
                        if (_loanSchemeViewModel.SchemeEducationLoanParameterViewModel.IsApplicableAllUniversities == true)
                        {
                            IEnumerable<SchemeInstituteViewModel> schemeInstituteViewModel = await schemeDetailRepository.GetSchemeInstituteEntries(_loanSchemeViewModel.SchemePrmKey, entriesType);

                            foreach (SchemeInstituteViewModel viewModel in schemeInstituteViewModel)
                            {
                                result = schemeDbContextRepository.AttachSchemeInstituteData(viewModel, _entryType);
                            }
                        }

                    }

                    // SchemeHomeLoan
                    if (loanType == StringLiteralValue.HomeLoan)
                    {
                        SchemeHomeLoanViewModel schemeHomeLoanViewModel = await schemeDetailRepository.GetSchemeHomeLoanEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);
                        if (schemeHomeLoanViewModel != null)
                            result = schemeDbContextRepository.AttachSchemeHomeLoanData(schemeHomeLoanViewModel, _entryType);
                    }

                    // SchemeLoanAgainstProperty

                    if (loanType == StringLiteralValue.LoanAgainstProperty)
                    {
                        SchemeLoanAgainstPropertyViewModel schemeLoanAgainstPropertyViewModel = await schemeDetailRepository.GetSchemeLoanAgainstPropertyEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);
                        if (schemeLoanAgainstPropertyViewModel != null)
                            result = schemeDbContextRepository.AttachSchemeLoanAgainstPropertyData(schemeLoanAgainstPropertyViewModel, _entryType);
                    }

                    // SchemeBusinessLoan
                    if (loanType == StringLiteralValue.ShortTermBusinessLoan)
                    {
                        SchemeBusinessLoanViewModel schemeBusinessLoanViewModel = await schemeDetailRepository.GetSchemeBusinessLoanEntry(_loanSchemeViewModel.SchemePrmKey, entriesType);
                        if (schemeBusinessLoanViewModel != null)
                            result = schemeDbContextRepository.AttachSchemeBusinessLoanData(schemeBusinessLoanViewModel, _entryType);
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
