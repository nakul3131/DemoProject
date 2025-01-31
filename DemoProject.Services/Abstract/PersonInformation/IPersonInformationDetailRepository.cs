using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonInformationDetailRepository
    {
        Task<IEnumerable<PersonAdditionalIncomeDetailViewModel>> AdditionalIncomeDetailEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonAddressViewModel>> AddressEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonAgricultureAssetViewModel>> AgricultureAssetEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonBankDetailViewModel>> BankDetailEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonGroupAuthorizedSignatoryViewModel>> GroupAuthorizedSignatoryEntries(long _personGroupPrmKey, string _entryType);

        Task<IEnumerable<PersonBoardOfDirectorRelationViewModel>> BoardOfDirectorRelationEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonBorrowingDetailViewModel>> BorrowingDetailEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonChronicDiseaseViewModel>> ChronicDiseaseEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonContactDetailViewModel>> ContactDetailEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonCourtCaseViewModel>> CourtCaseEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonCreditRatingViewModel>> CreditRatingEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonFamilyDetailViewModel>> FamilyDetailEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonFinancialAssetViewModel>> FinancialAssetEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GSTReturnDocumentEntries(long _personGSTRegistrationDetailPrmKey, string _entryType);

        Task<IEnumerable<PersonImmovableAssetViewModel>> ImmovableAssetEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonIncomeTaxDetailViewModel>> IncomeTaxDetailEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonInsuranceDetailViewModel>> InsuranceDetailEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonKYCDocumentViewModel>> KYCDocumentEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonMachineryAssetViewModel>> MachineryAssetEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonMovableAssetViewModel>> MovableAssetEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonSMSAlertViewModel>> SMSAlertEntries(long _personPrmKey, string _entryType);

        Task<IEnumerable<PersonSocialMediaViewModel>> SocialMediaEntries(long _personPrmKey, string _entryType);

        Task<PersonPrefixViewModel> PrefixEntry(long _personPrmKey, string _entryType);

        Task<PersonHomeBranchViewModel> HomeBranchEntry(long _personPrmKey, string _entryType);

        Task<PersonGSTRegistrationDetailViewModel> GSTRegistrationDetailEntry(long _personPrmKey, string _entryType);

        Task<PersonCommoditiesAssetViewModel> CommoditiesAssetEntry(long _personPrmKey, string _entryType);

        Task<ForeignerViewModel> ForeignerEntry(long _personPrmKey, string _entryType);

        Task<GuardianPersonViewModel> GuardianPersonEntry(long _personPrmKey, string _entryType);

        Task<PersonGroupViewModel> PersonGroupEntry(long _personPrmKey, string _entryType);

        Task<PersonPhotoSignViewModel> PhotoSignEntry(long _personPrmKey, string _entryType);

        Task<PersonAdditionalDetailViewModel> AdditionalDetailEntry(long _personPrmKey, string _entryType);

        Task<PersonEmploymentDetailViewModel> EmploymentDetailEntry(long _personPrmKey, string _entryType);
    }
}
