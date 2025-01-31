using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonInformationDetailRepository : IPersonInformationDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;


        public EFPersonInformationDetailRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IPersonDetailRepository _personDetailRepository, IConfigurationDetailRepository _configurationDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository,
            IManagementDetailRepository _managementDetailRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            personDetailRepository = _personDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            managementDetailRepository = _managementDetailRepository;
        }

        public async Task<IEnumerable<PersonAdditionalIncomeDetailViewModel>> AdditionalIncomeDetailEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonAdditionalIncomeDetailViewModel>("SELECT * FROM dbo.GetPersonAdditionalIncomeDetailEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonAddressViewModel>> AddressEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonAddressViewModel>("SELECT * FROM dbo.GetPersonAddressEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonAgricultureAssetViewModel>> AgricultureAssetEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonAgricultureAssetViewModel> personAgricultureAssetViewModels = await context.Database.SqlQuery<PersonAgricultureAssetViewModel>("SELECT * FROM dbo.GetPersonAgricultureAssetEntriesByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathAgree = objFile;

                }
                return personAgricultureAssetViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonBankDetailViewModel>> BankDetailEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonBankDetailViewModel> personBankDetailViewModels;
                personBankDetailViewModels = await context.Database.SqlQuery<PersonBankDetailViewModel>("SELECT * FROM dbo.GetPersonBankDetailEntriesByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonBankDetailViewModel viewModel in personBankDetailViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathBank = objFile;

                }
                return personBankDetailViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<IEnumerable<PersonGroupAuthorizedSignatoryViewModel>> GroupAuthorizedSignatoryEntries(long _personGroupPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonGroupAuthorizedSignatoryViewModel> PersonGroupAuthorizedSignatoryViewModels;
                PersonGroupAuthorizedSignatoryViewModels = await context.Database.SqlQuery<PersonGroupAuthorizedSignatoryViewModel>("SELECT * FROM dbo.GetPersonGroupAuthorizedSignatoryEntriesByPersonGroupPrmKey (@PersonGroupPrmKey, @EntriesType)", new SqlParameter("@PersonGroupPrmKey", _personGroupPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonGroupAuthorizedSignatoryViewModel viewModel in PersonGroupAuthorizedSignatoryViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.Sign, viewModel.SignNameOfFile);

                    viewModel.PhotoPathSign = objFile;

                }
                return PersonGroupAuthorizedSignatoryViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<IEnumerable<PersonBoardOfDirectorRelationViewModel>> BoardOfDirectorRelationEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonBoardOfDirectorRelationViewModel>("SELECT * FROM dbo.GetPersonBoardOfDirectorRelationEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonBorrowingDetailViewModel>> BorrowingDetailEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonBorrowingDetailViewModel>("SELECT * FROM dbo.GetPersonBorrowingDetailEntriesByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonChronicDiseaseViewModel>> ChronicDiseaseEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonChronicDiseaseViewModel>("SELECT * FROM dbo.GetPersonChronicDiseaseEntriesByPersonPrmKey (@CenterPrmkey, @EntriesType)", new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonContactDetailViewModel>> ContactDetailEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonContactDetailViewModel>("SELECT * FROM dbo.GetPersonContactDetailEntriesByPersonPrmKey (@CenterPrmkey, @EntriesType)", new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonCourtCaseViewModel>> CourtCaseEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonCourtCaseViewModel>("SELECT * FROM dbo.GetPersonCourtCaseEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonCreditRatingViewModel>> CreditRatingEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonCreditRatingViewModel>("SELECT * FROM dbo.GetPersonCreditRatingEntriesByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonFamilyDetailViewModel>> FamilyDetailEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonFamilyDetailViewModel>("SELECT * FROM dbo.GetPersonFamilyDetailEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonFinancialAssetViewModel>> FinancialAssetEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonFinancialAssetViewModel> personFinancialAssetViewModels = await context.Database.SqlQuery<PersonFinancialAssetViewModel>("SELECT * FROM dbo.GetPersonFinancialAssetEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathFinance = objFile;

                }
                return personFinancialAssetViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GSTReturnDocumentEntries(long _personGSTRegistrationDetailPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModels;

                personGSTReturnDocumentViewModels = await context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonGSTReturnDocumentEntriesByPersonGSTRegistrationDetailPrmKey (@PersonGSTRegistrationDetailPrmKey, @EntriesType)", new SqlParameter("@PersonGSTRegistrationDetailPrmKey", _personGSTRegistrationDetailPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTReturnDocumentViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathGst = objFile;

                }
                return personGSTReturnDocumentViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonImmovableAssetViewModel>> ImmovableAssetEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonImmovableAssetViewModel> personImmovableAssetViewModels;

                personImmovableAssetViewModels = await context.Database.SqlQuery<PersonImmovableAssetViewModel>("SELECT * FROM dbo.GetPersonImmovableAssetEntriesByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathImmovable = objFile;

                }
                return personImmovableAssetViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonIncomeTaxDetailViewModel>> IncomeTaxDetailEntries(long _personPrmKey, string _entryType)
        {
            try
            {

                IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModels;

                personIncomeTaxDetailViewModels = await context.Database.SqlQuery<PersonIncomeTaxDetailViewModel>("SELECT * FROM dbo.GetPersonIncomeTaxDetailEntriesByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("@EntriesType", _entryType)).ToListAsync();

                string cntrl = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
                string act = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();

                if (cntrl != "PersonChildAction" && act != "GetIncomeTaxDetailByPersonId")
                {
                    foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModels)
                    {
                        HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                        viewModel.PhotoPathTax = objFile;

                    }
                }

                return personIncomeTaxDetailViewModels;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonInsuranceDetailViewModel>> InsuranceDetailEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonInsuranceDetailViewModel>("SELECT * FROM dbo.GetPersonInsuranceDetailEntriesByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonKYCDocumentViewModel>> KYCDocumentEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonKYCDocumentViewModel> personKYCDocumentViewModels;

                personKYCDocumentViewModels = await context.Database.SqlQuery<PersonKYCDocumentViewModel>("SELECT * FROM dbo.GetPersonKYCDetailEntriesByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonKYCDocumentViewModel viewModel in personKYCDocumentViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathKYC = objFile;
                }
                return personKYCDocumentViewModels;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonMachineryAssetViewModel>> MachineryAssetEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonMachineryAssetViewModel> personMachineryAssetViewModels;

                personMachineryAssetViewModels = await context.Database.SqlQuery<PersonMachineryAssetViewModel>("SELECT * FROM dbo.GetPersonMachineryAssetEntriesByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonMachineryAssetViewModel viewModel in personMachineryAssetViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathMachinery = objFile;

                }
                return personMachineryAssetViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonMovableAssetViewModel>> MovableAssetEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonMovableAssetViewModel> personMachineryAssetViewModels;
                personMachineryAssetViewModels = await context.Database.SqlQuery<PersonMovableAssetViewModel>("SELECT * FROM dbo.GetPersonMovableAssetEntriesByPersonPrmKey (@CenterPrmkey, @EntriesType)", new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonMovableAssetViewModel viewModel in personMachineryAssetViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathMovable = objFile;

                }
                return personMachineryAssetViewModels;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonSMSAlertViewModel>> SMSAlertEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonSMSAlertViewModel>("SELECT * FROM dbo.GetPersonSMSAlertEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonSocialMediaViewModel>> SocialMediaEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonSocialMediaViewModel>("SELECT * FROM dbo.GetPersonSocialMediaEntriesByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonPrefixViewModel> PrefixEntry(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonPrefixViewModel>("SELECT * FROM dbo.GetPersonPrefixEntryByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonHomeBranchViewModel> HomeBranchEntry(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonHomeBranchViewModel>("SELECT * FROM dbo.GetPersonHomeBranchEntryByPersonPrmKey ( @PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonGSTRegistrationDetailViewModel> GSTRegistrationDetailEntry(long _personPrmKey, string _entryType)
        {
            try
            {
                PersonGSTRegistrationDetailViewModel personGSTRegistrationDetailViewModel = await context.Database.SqlQuery<PersonGSTRegistrationDetailViewModel>("SELECT * FROM dbo.GetPersonGSTRegistrationDetailEntryByPersonPrmKey ( @CenterPrmkey, @EntriesType)", new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                if (personGSTRegistrationDetailViewModel != null)
                {
                    List<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModels = new List<PersonGSTReturnDocumentViewModel>();
                    personGSTReturnDocumentViewModels = context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonGSTReturnDocumentEntriesByPersonGSTRegistrationDetailPrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", personGSTRegistrationDetailViewModel.PrmKey), new SqlParameter("EntriesType", _entryType)).ToList();

                    if ((personGSTReturnDocumentViewModels != null) && (personGSTReturnDocumentViewModels.Count() != 0))
                    {
                        personGSTRegistrationDetailViewModel.UploadGSTReturnDocument = true;
                    }
                }
                else
                {
                    personGSTRegistrationDetailViewModel.UploadGSTReturnDocument = false;
                }

                IEnumerable<PersonKYCDocumentViewModel> personKYCDocumentViewModels = await context.Database.SqlQuery<PersonKYCDocumentViewModel>("SELECT * FROM dbo.GetPersonKYCDetailEntriesByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                if (personKYCDocumentViewModels != null)
                {
                    foreach (PersonKYCDocumentViewModel viewModel in personKYCDocumentViewModels)
                    {
                        if (viewModel.NameOfDocument.Contains("Pan"))
                        {
                            personGSTRegistrationDetailViewModel.IsPanCard = true;
                        }
                    }
                }

                return personGSTRegistrationDetailViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonCommoditiesAssetViewModel> CommoditiesAssetEntry(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonCommoditiesAssetViewModel>("SELECT * FROM dbo.GetPersonCommoditiesAssetEntryByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ForeignerViewModel> ForeignerEntry(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<ForeignerViewModel>("SELECT * FROM dbo.GetForeignerPersonEntryByPersonPrmKey ( @PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<GuardianPersonViewModel> GuardianPersonEntry(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<GuardianPersonViewModel>("SELECT * FROM dbo.GetGuardianPersonEntryByPersonPrmKey ( @PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonGroupViewModel> PersonGroupEntry(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonGroupViewModel>("SELECT * FROM dbo.GetPersonGroupEntryByPersonPrmKey ( @PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonPhotoSignViewModel> PhotoSignEntry(long _personPrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<PersonPhotoSignViewModel>("SELECT * FROM dbo.GetPersonPhotoSignEntryByPersonPrmKey ( @PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonAdditionalDetailViewModel> AdditionalDetailEntry(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonAdditionalDetailViewModel>("SELECT * FROM dbo.GetPersonAdditionalDetailEntryByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonEmploymentDetailViewModel> EmploymentDetailEntry(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonEmploymentDetailViewModel>("SELECT * FROM dbo.GetPersonEmploymentDetailEntryByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


    }
}
