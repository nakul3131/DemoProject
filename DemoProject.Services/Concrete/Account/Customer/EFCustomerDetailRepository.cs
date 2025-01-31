using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Concrete.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerDetailRepository : ICustomerDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public EFCustomerDetailRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, 
                                        IPersonDetailRepository _personDetailRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            personDetailRepository = _personDetailRepository;
        }



        public async Task<IEnumerable<CustomerAccountBeneficiaryDetailViewModel>> GetBeneficiaryDetailEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountBeneficiaryDetailViewModel>("SELECT * FROM dbo.GetCustomerAccountBeneficiaryDetailEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("@EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountNomineeViewModel>> GetNomineeEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountNomineeViewModel>("SELECT * FROM dbo.GetCustomerAccountNomineeEntriesByCustomerAccountPrmKey ( @CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountNoticeScheduleViewModel>> GetCustomerAccountNoticeScheduleEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountNoticeScheduleViewModel>("SELECT * FROM dbo.GetCustomerAccountNoticeScheduleEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("@EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountStandingInstructionViewModel>> GetStandingInstructionEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountStandingInstructionViewModel>("SELECT * FROM dbo.GetCustomerAccountStandingInstructionEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }

        }

        public async Task<IEnumerable<CustomerAccountTurnOverLimitViewModel>> GetTurnOverLimitEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountTurnOverLimitViewModel>("SELECT * FROM dbo.GetCustomerAccountTurnOverLimitEntriesByCustomerPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("@EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetJointAccountHolderEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerJointAccountHolderViewModel>("SELECT * FROM dbo.GetCustomerJointAccountHolderEntriesByCustomerPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        // Delete After All pages are Complition 29112023

        public CustomerAccountNomineeGuardianViewModel GetNomineeGuardianEntry(long _customerAccountNomineePrmKey, string _entryType)
        {
            try
            {
                return context.Database.SqlQuery<CustomerAccountNomineeGuardianViewModel>("SELECT * FROM dbo.GetCustomerAccountNomineeGuardianEntriesByCustomerAccountNomineePrmKey (@CustomerAccountNomineePrmKey, @EntryType)", new SqlParameter("@CustomerAccountNomineePrmKey", _customerAccountNomineePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public IEnumerable<CustomerAccountNomineeGuardianViewModel> GetNomineeGuardianEntries(long _customerAccountNomineePrmKey, string _entryType)
        {
            try
            {
                var a = context.Database.SqlQuery<CustomerAccountNomineeGuardianViewModel>("SELECT * FROM dbo.GetCustomerAccountNomineeGuardianEntriesByCustomerAccountNomineePrmKey (@CustomerAccountNomineePrmKey, @EntryType)", new SqlParameter("@CustomerAccountNomineePrmKey", _customerAccountNomineePrmKey), new SqlParameter("EntryType", _entryType)).ToList();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerAccountChequeDetailViewModel> GetChequeDetailEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountChequeDetailViewModel>("SELECT * FROM dbo.GetCustomerAccountChequeDetailEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerAccountDetailViewModel> GetAccountDetailEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountDetailViewModel>("SELECT * FROM dbo.GetCustomerAccountDetailEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerAccountEmailServiceViewModel> GetEmailServiceEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountEmailServiceViewModel>("SELECT * FROM dbo.GetCustomerAccountEmailServiceEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerAccountInterestRateViewModel> GetCustomerAccountInterestRateEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountInterestRateViewModel>("SELECT * FROM dbo.GetCustomerAccountInterestRateEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerLoanAgainstPropertyCollateralDetailViewModel> GetCustomerLoanAgainstPropertyCollateralDetailEntry(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerLoanAgainstPropertyCollateralDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAgainstPropertyCollateralDetailEntryByCustomerLoanAccountPrmKey (@CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerBusinessLoanCollateralDetailViewModel> GetCustomerBusinessLoanCollateralDetailEntry(long _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerBusinessLoanCollateralDetailViewModel>("SELECT * FROM dbo.GetCustomerBusinessLoanCollateralDetailEntryByCustomerLoanAccountPrmKey (@CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerAccountPhotoSignViewModel> GetPhotoSignEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountPhotoSignViewModel>("SELECT * FROM dbo.GetCustomerAccountPhotoSignEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerAccountSmsServiceViewModel> GetSmsServiceEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountSmsServiceViewModel>("SELECT * FROM dbo.GetCustomerAccountSmsServiceEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerAccountStandingInstructionViewModel> GetStandingInstructionEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountStandingInstructionViewModel>("SELECT * FROM dbo.GetCustomerAccountStandingInstructionEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        
        public async Task<CustomerAccountSweepDetailViewModel> GetSweepDetailEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountSweepDetailViewModel>("SELECT * FROM dbo.GetCustomerAccountSweepDetailEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerSharesCapitalAccountViewModel> GetSharesCapitalAccountEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerSharesCapitalAccountViewModel>("SELECT * FROM dbo.GetCustomerSharesCapitalAccountEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonEmploymentDetailViewModel> GetPersonEmploymentDetailEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonEmploymentDetailViewModel>("SELECT * FROM dbo.GetPersonEmploymentDetailEntryByCustomerAccountPrmKey (@CustomerAccountPrmKey, @EntryType)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
               
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
       
        public async Task<PersonEmploymentDetailViewModel> GetCustomerAccountEmploymentDetailEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<PersonEmploymentDetailViewModel>("SELECT * FROM dbo.GetPersonEmploymentDetailEntryByCustomerAccountPrmKey (@CustomerAccountPrmKey, @EntryType)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

         public async Task<IEnumerable<PersonAddressViewModel>> GetCustomerAccountAddressDetailEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonAddressViewModel>("SELECT * FROM dbo.GetCustomerAccountAddressDetailEntriesByCustomerAccountPrmKey (@CustomerAccountPrmKey, @EntryType)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonAddressViewModel>> GetPersonAddressDetailEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonAddressViewModel>("SELECT * FROM dbo.GetPersonAddressEntriesByCustomerAccountPrmKey (@CustomerAccountPrmKey, @EntryType)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonContactDetailViewModel>> GetCustomerAccountContactDetailEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonContactDetailViewModel>("SELECT * FROM dbo.GetCustomerAccountContactDetailEntriesByCustomerAccountPrmKey (@CustomerAccountPrmKey, @EntryType)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonContactDetailViewModel>> GetPersonContactDetailEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonContactDetailViewModel>("SELECT * FROM dbo.GetPersonContactDetailEntriesByCustomerAccountPrmKey (@CustomerAccountPrmKey, @EntryType)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonBorrowingDetailViewModel>> GetCustomerAccountBorrowingDetailEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonBorrowingDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountBorrowingDetailEntriesByCustomerAccountPrmKey (@CustomerAccountPrmKey, @EntryType)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonBorrowingDetailViewModel>> GetPersonBorrowingDetailEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonBorrowingDetailViewModel>("SELECT * FROM dbo.GetPersonBorrowingDetailEntriesByCustomerAccountPrmKey (@CustomerAccountPrmKey, @EntryType)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<IEnumerable<PersonCourtCaseViewModel>> GetPersonCourtCaseEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonCourtCaseViewModel>("SELECT * FROM dbo.GetPersonCourtCaseEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonCourtCaseViewModel>> GetCustomerAccountCourtCaseEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonCourtCaseViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountCourtCaseDetailEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonIncomeTaxDetailViewModel>> GetPersonIncomeTaxDetailEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
               // return await context.Database.SqlQuery<PersonIncomeTaxDetailViewModel>("SELECT * FROM dbo.GetPersonIncomeTaxDetailEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModels;

                personIncomeTaxDetailViewModels = await context.Database.SqlQuery<PersonIncomeTaxDetailViewModel>("SELECT * FROM dbo.GetPersonIncomeTaxDetailEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathTax = objFile;

                }
                return personIncomeTaxDetailViewModels;


            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        public async Task<IEnumerable<PersonIncomeTaxDetailViewModel>> GetCustomerAccountIncomeTaxDetailEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModels;

                personIncomeTaxDetailViewModels = await context.Database.SqlQuery<PersonIncomeTaxDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountIncomeTaxDetailEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathTax = objFile;

                }
                return personIncomeTaxDetailViewModels;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonAdditionalIncomeDetailViewModel>> GetPersonAdditionalIncomeDetailEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonAdditionalIncomeDetailViewModel>("SELECT * FROM dbo.GetPersonAdditionalIncomeDetailEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }



        public async Task<IEnumerable<PersonAdditionalIncomeDetailViewModel>> GetCustomerAccountAdditionalIncomeDetailEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<PersonAdditionalIncomeDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountAdditionalIncomeDetailEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
        public async Task<PersonCommoditiesAssetViewModel> GetPersonCommoditiesAssetEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonCommoditiesAssetViewModel>("SELECT * FROM dbo.GetPersonCommoditiesAssetEntryByCustomerAccountPrmkey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
        // Deposit Account
        public async Task<IEnumerable<CustomerAccountDocumentViewModel>> GetDocumentEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                //var a = await context.Database.SqlQuery<CustomerAccountDocumentViewModel>("SELECT * FROM dbo.GetCustomerAccountDocumentEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                //return a; 

                IEnumerable<CustomerAccountDocumentViewModel> customerAccountDocumentViewModels = await context.Database.SqlQuery<CustomerAccountDocumentViewModel>("SELECT * FROM dbo.GetCustomerAccountDocumentEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (CustomerAccountDocumentViewModel viewModel in customerAccountDocumentViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.DocumentPhotoCopy, viewModel.NameOfFile);

                    viewModel.FileUploader = objFile;

                }
                return customerAccountDocumentViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountFacilityViewModel>> GetFacilityEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountFacilityViewModel>("SELECT * FROM dbo.GetCustomerAccountFacilityEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountInterestRateViewModel>> GetInterestRateEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountInterestRateViewModel>("SELECT * FROM dbo.GetCustomerAccountInterestRateEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetDepositAccountAgentEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerDepositAccountAgentViewModel>("SELECT * FROM dbo.GetCustomerDepositAccountAgentEntriesByCustomerDepositAccountPrmKey (@CustomerDepositAccountAgentPrmKey, @EntriesType)", new SqlParameter("@CustomerDepositAccountAgentPrmKey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerTermDepositAccountDetailViewModel> GetTermDepositAccountDetailEntry(long _customerDepositAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerTermDepositAccountDetailViewModel>("SELECT * FROM dbo.GetCustomerTermDepositAccountDetailEntryByCustomerDepositAccountPrmKey (@CustomerTermDepositAccountDetailPrmKey, @EntriesType)", new SqlParameter("@CustomerTermDepositAccountDetailPrmKey", _customerDepositAccountPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        //GetReferencePersonEntries
        public async Task<CustomerDepositAccountViewModel> GetDepositAccountEntry(long _customerAccountPrmKey, string _entryType)
        {
            CustomerDepositAccountViewModel depositCustomerAccountViewModel = new CustomerDepositAccountViewModel();
            try
            {
                depositCustomerAccountViewModel = await context.Database.SqlQuery<CustomerDepositAccountViewModel>("SELECT * FROM dbo.GetCustomerDepositAccountEntryByCustomerAccountPrmKey (@CustomerAccountPrmKey, @EntriesType)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
            return depositCustomerAccountViewModel;
        }

        public async Task<IEnumerable<CustomerAccountReferencePersonDetailViewModel>> GetReferencePersonEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountReferencePersonDetailViewModel>("SELECT * FROM dbo.GetCustomerAccountReferencePersonDetailEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        // CustomerLoanAccount
        public async Task<CustomerAccountInterestRateViewModel> GetInterestRateEntry(long _CustomerAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountInterestRateViewModel>("SELECT * FROM dbo.[GetCustomerAccountInterestRateEntryByCustomerAccountPrmKey] ( @CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _CustomerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerVehicleLoanInsuranceDetailViewModel> GetLoanAccountVehicleInsuranceDetailEntry(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanInsuranceDetailViewModel>("SELECT * FROM dbo.GetCustomerVehicleLoanInsuranceDetailEntryByCustomerLoanAccountPrmKey ( @CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerLoanAccountViewModel> GetLoanAccountEntry(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountEntryByCustomerAccountPrmKey ( @CustomerAccountPrmKey, @EntryType)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerLoanFieldInvestigationViewModel> GetLoanFieldInvestigationEntry(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanFieldInvestigationViewModel>("SELECT * FROM dbo.GetCustomerLoanFieldInvestigationEntryByCustomerLoanAccountPrmKey ( @CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerLoanAccountDebtToIncomeRatioViewModel> GetCustomerLoanAccountDebtToIncomeRatioEntry(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerLoanAccountDebtToIncomeRatioViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountDebtToIncomeRatioEntryByCustomerLoanAccountPrmKey ( @CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<CustomerCashCreditLoanAccountViewModel> GetCustomerCashCreditLoanAccountEntry(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerCashCreditLoanAccountViewModel>("SELECT * FROM dbo.GetCustomerCashCreditLoanAccountEntryByCustomerLoanAccountPrmKey ( @CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
        public async Task<CustomerEducationalLoanDetailViewModel> GetCustomerEducationalLoanDetailEntry(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerEducationalLoanDetailViewModel>("SELECT * FROM dbo.GetCustomerEducationalLoanDetailEntryByCustomerLoanAccountPrmKey ( @CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
        public async Task<CustomerPreOwnedVehicleLoanInspectionViewModel> GetPreOwnedVehicleLoanInspectionEntry(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerPreOwnedVehicleLoanInspectionViewModel>("SELECT * FROM dbo.GetCustomerPreOwnedVehicleLoanInspectionEntryByCustomerLoanAccountPrmKey (@CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerVehicleLoanCollateralDetailViewModel> GetVehicleLoanCollateralDetailEntry(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanCollateralDetailViewModel>("SELECT * FROM dbo.GetCustomerVehicleLoanCollateralDetailEntryByCustomerLoanAccountPrmKey (@CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
        public async Task<CustomerVehicleLoanPermitDetailViewModel> GetCustomerVehicleLoanPermitDetailEntry(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerVehicleLoanPermitDetailViewModel>("Select * FROM dbo.GetCustomerVehicleLoanPermitDetailEntryByCustomerLoanAccountPrmKey ( @CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public Task<CustomerVehicleLoanContractDetailViewModel> GetCustomerVehicleLoanContractDetailEntry(int _customerLaonAccountPrmKey, string _entryType)
        {
            try
            {
                return context.Database.SqlQuery<CustomerVehicleLoanContractDetailViewModel>("SELECT * FROM dbo.GetCustomerVehicleLoanContractDetailEntryByCustomerLoanAccountPrmKey( @CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLaonAccountPrmKey), new SqlParameter("@EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountNoticeScheduleViewModel>> GetNoticeScheduleEntries(long _customerAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerAccountNoticeScheduleViewModel>("SELECT * FROM dbo.GetCustomerAccountNoticeScheduleEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("@EntryType", _entryType)).ToListAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerGoldLoanCollateralDetailViewModel>> GetGoldLoanCollateralDetailEntries(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<CustomerGoldLoanCollateralDetailViewModel> customerGoldLoanCollateralDetailViewModels;
                customerGoldLoanCollateralDetailViewModels = await context.Database.SqlQuery<CustomerGoldLoanCollateralDetailViewModel>("SELECT * FROM dbo.GetCustomerGoldLoanCollateralDetailEntriesByCustomerLoanAccountPrmKey (@CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                foreach (CustomerGoldLoanCollateralDetailViewModel viewModel in customerGoldLoanCollateralDetailViewModels)
                {
                    viewModel.GoldValuationAmount = (viewModel.MetalNetWeight * viewModel.LoanAmountPerGram);
                }
                return customerGoldLoanCollateralDetailViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerGoldLoanCollateralPhotoViewModel>> GetGoldLoanCollateralPhotoEntries(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<CustomerGoldLoanCollateralPhotoViewModel> customerGoldLoanCollateralPhotoViewModels;
                customerGoldLoanCollateralPhotoViewModels = await context.Database.SqlQuery<CustomerGoldLoanCollateralPhotoViewModel>("SELECT * FROM dbo.GetCustomerGoldLoanCollateralPhotoEntriesByCustomerLoanAccountPrmKey (@CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                foreach (CustomerGoldLoanCollateralPhotoViewModel viewModel in customerGoldLoanCollateralPhotoViewModels)
                {
                    //var filename = "purple-asphalt-flowers-background-wallpaper-preview.jpg";
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);
                    viewModel.PhotoPath = objFile;
                }
                return customerGoldLoanCollateralPhotoViewModels;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerConsumerLoanCollateralDetailViewModel>> GetConsumerLoanCollateralDetailEntries(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<CustomerConsumerLoanCollateralDetailViewModel> customerConsumerLoanCollateralDetailViewModels;
                customerConsumerLoanCollateralDetailViewModels = await context.Database.SqlQuery<CustomerConsumerLoanCollateralDetailViewModel>("SELECT * FROM dbo.GetCustomerConsumerLoanCollateralDetailEntriesByCustomerLoanAccountPrmKey (@CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();

                return customerConsumerLoanCollateralDetailViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerLoanAcquaintanceDetailViewModel>> GetAcquaintanceDetailEntries(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<CustomerLoanAcquaintanceDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAcquaintanceDetailEntriesByCustomerLoanAccountPrmKey (@CustomerLoanAccountPrmkey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmkey", _customerLoanAccountPrmKey), new SqlParameter("@EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetLoanAccountGuarantorDetailEntries(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountGuarantorDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountGuarantorDetailEntriesByCustomerLoanAccountPrmKey (@CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

         public async Task<IEnumerable<CustomerLoanAgainstDepositCollateralDetailViewModel>> GetCustomerLoanAgainstDepositCollateralDetailEntries(int _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAgainstDepositCollateralDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAgainstDepositCollateralDetailEntriesByCustomerLoanAccountPrmKey (@CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerVehicleLoanPhotoViewModel>> GetVehicleLoanPhotoEntries(long _customerLoanAccountPrmKey, string _entryType)
        {
            try
            {
               
                IEnumerable<CustomerVehicleLoanPhotoViewModel> customerVehicleLoanPhotoViewModels = await context.Database.SqlQuery<CustomerVehicleLoanPhotoViewModel>("SELECT * FROM dbo.GetCustomerVehicleLoanPhotoEntriesByCustomerLoanAccountPrmKey (@CustomerLoanAccountPrmKey, @EntryType)", new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                foreach (CustomerVehicleLoanPhotoViewModel viewModel in customerVehicleLoanPhotoViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPath = objFile;

                }
                return customerVehicleLoanPhotoViewModels;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool IsMinimumBalanceViolation(long _customerAccountPrmKey, decimal _balanceAmount)
        {
            short schemePrmKey = GetSchemePrmKeyOfCustomerAccount(_customerAccountPrmKey);
            decimal minimumBalanceAmount = accountDetailRepository.GetDemandDepositMinimumBalanceAmount(schemePrmKey);

            if (_balanceAmount < minimumBalanceAmount)
                return true;
            else
                return false;
        }

        public bool IsNewCustomer(long _customerAccountPrmKey)
        {
            short customerAccountage = GetCustomerAccountAge(_customerAccountPrmKey);
            short schemePrmKey = GetSchemePrmKeyOfCustomerAccount(_customerAccountPrmKey);

            short timePeriodForNewAccount = accountDetailRepository.GetTimePeriodForNewCustomerAccountFlag(schemePrmKey);

            if (customerAccountage <= timePeriodForNewAccount)
                return true;

            return false;
        }

        public long GetPersonPrmKeyByCustomerAccountPrmKey(long _customerAccountPrmKey)
        {
            return context.CustomerAccountDetails
                            .Where(a => a.CustomerAccountPrmKey == _customerAccountPrmKey && a.EntryStatus == StringLiteralValue.Verify)
                            .Select(a => a.PersonPrmKey).FirstOrDefault();
        }

        public short GetCustomerAccountAge(long _customerAccountPrmKey)
        {
            DateTime accountOpeningDate = context.CustomerAccounts
                                            .Where(c => c.PrmKey == _customerAccountPrmKey && c.EntryStatus == StringLiteralValue.Verify)
                                            .Select(c => c.AccountOpeningDate).FirstOrDefault();

            return (short)DateTime.Now.Subtract(accountOpeningDate).TotalDays;
        }

        public short GetSchemePrmKeyOfCustomerAccount(long _customerAccountPrmKey)
        {
            return (from d in context.CustomerAccountDetails.Where(d => d.CustomerAccountPrmKey == _customerAccountPrmKey && d.EntryStatus == StringLiteralValue.Verify)
                    select d.SchemePrmKey).FirstOrDefault();
        }

        public int GetTotalNumberOfShares(long _customerAccountPrmKey)
        {
            var tmp = (from a in context.TransactionCustomerAccounts
                       join sc in context.SharesCapitalTransactions.Where(sc => sc.EntryStatus == StringLiteralValue.Verify) on a.PrmKey equals sc.TransactionCustomerAccountPrmKey into asc
                       from sc in asc.DefaultIfEmpty()
                       where (a.CustomerAccountPrmKey == _customerAccountPrmKey) && (a.EntryStatus == StringLiteralValue.Verify)
                       group sc by a.CustomerAccountPrmKey into g
                       select new
                       {
                           TotalNumberOfShares = g.Sum(s => s.NumberOfShares)
                       }).FirstOrDefault();

            return tmp!=null ?(int)tmp.TotalNumberOfShares:1;
        }
        
        public string GetAllSharesCertificateNumbers(long _customerAccountPrmKey)
        {
            List<string> certificateNumbers = (from a in context.TransactionCustomerAccounts
                                               join sc in context.SharesCapitalTransactions.Where(sc => sc.EntryStatus == StringLiteralValue.Verify) on a.PrmKey equals sc.TransactionCustomerAccountPrmKey into asc
                                               from sc in asc.DefaultIfEmpty()
                                               where (a.CustomerAccountPrmKey == _customerAccountPrmKey) && (a.EntryStatus == StringLiteralValue.Verify)
                                               select sc.StartSharesCertificateNumber + " To " + sc.EndSharesCertificateNumber
                       ).ToList();

            return "Certificate Numbers: " + string.Concat(string.Join(", ", certificateNumbers)).Trim();
        }

        public string GetCustomerRegisterdMobileNumber(long _customerAccountPrmKey)
        {
            return (from a in context.CustomerAccountContactDetails
                    join c in context.PersonContactDetails.Where(c => c.EntryStatus == StringLiteralValue.Verify) on a.PersonContactDetailPrmKey equals c.PrmKey into ac
                    from c in ac.DefaultIfEmpty()
                    join t in context.ContactTypes.Where(t => t.ActivationStatus == StringLiteralValue.Active && t.NameOfContactType == "Mobile") on c.ContactTypePrmKey equals t.PrmKey into ct
                    from t in ct.DefaultIfEmpty()
                    where (a.CustomerAccountPrmKey == _customerAccountPrmKey && a.EntryStatus == StringLiteralValue.Verify)
                    select (c.FieldValue)).FirstOrDefault();
        }

        public string IsAllowToCloseSharesCapitalAccount(long _customerAccountPrmKey)
        {
            long personPrmKey = GetPersonPrmKeyByCustomerAccountPrmKey(_customerAccountPrmKey);
            string reason = "";

            // Check Whether Member Is Depositor Borrower, Guarantor, Or Utilizing Any Other Service
            if (personDetailRepository.IsPersonDepositor(personPrmKey))
                reason = "Member Is Depositor";

            if (personDetailRepository.IsPersonBorrower(personPrmKey))
                reason = (reason == "") ? "Member Is Current Borrower" : ", Borrower";

            if (personDetailRepository.IsPersonGuarantor(personPrmKey))
                reason = (reason == "") ? "Member Is Guarantor Of Live Loan Account" : " And Guarantor";

            if (reason == "")
                return "Yes";
            else
                return "No, " + reason + "; Account Cannot Be Closed.";
        }

    }
}
