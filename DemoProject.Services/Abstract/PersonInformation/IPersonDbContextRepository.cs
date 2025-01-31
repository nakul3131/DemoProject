using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonDbContextRepository
    {
        bool AttachPersonData(PersonViewModel _personViewModel, string _entryType);

        bool AttachPersonMasterData(PersonMasterViewModel _personMasterViewModel, string _entryType);

        bool AttachPersonPrefixData(PersonPrefixViewModel _personPrefixViewModel, string _entryType);

        bool AttachPersonAdditionalDetailData(PersonAdditionalDetailViewModel _personAdditionalDetailViewModel, string _entryType);

        bool AttachPersonHomeBranchData(PersonHomeBranchViewModel _personHomeBranchViewModel, string _entryType);

        bool AttachForeignerPersonData(ForeignerViewModel _foreignerViewModel, string _entryType);

        bool AttachGuardianPersonData(GuardianPersonViewModel _guardianPersonViewModel, string _entryType);

        bool AttachPersonCommoditiesAssetData(PersonCommoditiesAssetViewModel _personCommoditiesAssetViewModel, string _entryType);

        bool AttachPersonEmploymentDetailData(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel, string _entryType);

        bool AttachPersonAdditionalIncomeDetailData(PersonAdditionalIncomeDetailViewModel _personAdditionalIncomeDetailViewModel, string _entryType);

        bool AttachPersonAddressData(PersonAddressViewModel _personAddressViewModel, string _entryType);

        bool AttachPersonAgricultureAssetData(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel, string _entryType);

        bool AttachPersonAgricultureAssetDocumentData(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel, string _localStoragePath, string _oldFileName, string _entryType);

        bool AttachPersonBankDetailData(PersonBankDetailViewModel _personBankDetailViewModel, string _entryType);

        bool AttachPersonBankDetailDocumentData(PersonBankDetailViewModel _personBankDetailViewModel, string _localStoragePath, string _oldFileName, string _entryType);

        bool AttachPersonGroupData(PersonGroupViewModel _personGroupViewModel, string _entryType);

        bool AttachPersonGroupMasterData(PersonGroupMasterViewModel _personGroupMasterViewModel, string _entryType);

        bool AttachPersonGroupAuthorizedSignatoryData(PersonGroupAuthorizedSignatoryViewModel _personGroupAuthorizedSignatoryViewModel, string _localStoragePath, string _oldFileName, string _entryType);

        bool AttachPersonBoardOfDirectorRelationData(PersonBoardOfDirectorRelationViewModel _personBoardOfDirectorRelationViewModel, string _entryType);

        bool AttachPersonBorrowingDetailData(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel, string _entryType);

        bool AttachPersonChronicDiseaseData(PersonChronicDiseaseViewModel _personChronicDiseaseViewModel, string _entryType);

        bool AttachPersonContactDetailData(PersonContactDetailViewModel _personContactDetailViewModel, string _entryType);

        bool AttachPersonCourtCaseData(PersonCourtCaseViewModel _personCourtCaseViewModel, string _entryType);

        bool AttachPersonCreditRatingData(PersonCreditRatingViewModel _personCreditRatingViewModel, string _entryType);

        bool AttachPersonFamilyDetailData(PersonFamilyDetailViewModel _personFamilyDetailViewModel, string _entryType);

        bool AttachPersonFinancialAssetData(PersonFinancialAssetViewModel _personFinancialAssetViewModel, string _entryType);

        bool AttachPersonFinancialAssetDocumentData(PersonFinancialAssetViewModel _personFinancialAssetViewModel, string _localStoragePath, string _oldFileName, string _entryType);

        bool AttachPersonKYCData(PersonKYCDocumentViewModel _personKYCDocumentViewModel, string _entryType);

        bool AttachPersonKYCDocumentData(PersonKYCDocumentViewModel _personKYCDocumentViewModel, string _localStoragePath, string _oldFileName, string _entryType);

        bool AttachPersonGSTRegistrationDetailData(PersonGSTRegistrationDetailViewModel _personGSTRegistrationDetailViewModel, string _entryType);

        bool AttachPersonGSTReturnDocumentData(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel, string _localStoragePath, string _oldFileName, string _entryType);

        bool AttachPersonImmovableAssetData(PersonImmovableAssetViewModel _personImmovableAssetViewModel, string _entryType);

        bool AttachPersonImmovableAssetDocumentData(PersonImmovableAssetViewModel _personImmovableAssetViewModel, string _localStoragePath, string _oldFileName, string _entryType);

        bool AttachPersonIncomeTaxDetailData(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _entryType);

        bool AttachPersonIncomeTaxDocumentData(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _localStoragePath, string _oldFileName, string _entryType);

        bool AttachPersonInsuranceDetailData(PersonInsuranceDetailViewModel _PersonInsuranceDetailViewModel, string _entryType);

        bool AttachPersonMachineryAssetData(PersonMachineryAssetViewModel _personMachineryAssetViewModel, string _entryType);

        bool AttachPersonMachineryAssetDocumentData(PersonMachineryAssetViewModel _personMachineryAssetViewModel, string _localStoragePath, string _oldFileName, string _entryType);

        bool AttachPersonMovableAssetData(PersonMovableAssetViewModel _personMovableAssetViewModel, string _entryType);

        bool AttachPersonMovableAssetDocumentData(PersonMovableAssetViewModel _personMovableAssetViewModel, string _localStoragePath, string _oldFileName, string _entryType);

        bool AttachPersonPhotoSignData(PersonPhotoSignViewModel _personPhotoSignViewModel, string _entryType);

        bool AttachPersonSMSAlertData(PersonSMSAlertViewModel _personSMSAlertViewModel, string _entryType);

        bool AttachPersonSocialMediaData(PersonSocialMediaViewModel _personSocialMediaViewModel, string _entryType);

        bool AttachAgricultureAssetDocumentInLocalStorage(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel, string _agricultureAssetDocumentLocalStoragePath, IEnumerable<PersonAgricultureAssetViewModel> _personAgricultureAssetViewModelList, string _entryType);

        bool AttachAgricultureAssetDocumentInDatabaseStorage(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel, IEnumerable<PersonAgricultureAssetViewModel> _personAgricultureAssetViewModelList, string _entryType);

        bool AttachBankDetailDocumentInLocalStorage(PersonBankDetailViewModel _personBankDetailViewModel, string _bankStatementLocalStoragePath, IEnumerable<PersonBankDetailViewModel> _personBankDetailViewModelList, string _entryType);

        bool AttachBankDetailDocumentInDatabaseStorage(PersonBankDetailViewModel _personBankDetailViewModel, IEnumerable<PersonBankDetailViewModel> _personBankDetailViewModelList, string _entryType);

        bool AttachGroupAuthorizedSignatoryInLocalStorage(PersonGroupAuthorizedSignatoryViewModel _personGroupAuthorizedSignatoryViewModel, string _signDocumentLocalStoragePath, IEnumerable<PersonGroupAuthorizedSignatoryViewModel> _personGroupAuthorizedSignatoryViewModelList, string _entryType);

        bool AttachGroupAuthorizedSignatoryInDatabaseStorage(PersonGroupAuthorizedSignatoryViewModel _personGroupAuthorizedSignatoryViewModel, IEnumerable<PersonGroupAuthorizedSignatoryViewModel> _personGroupAuthorizedSignatoryViewModelList, string _entryType);

        bool AttachFinancialAssetDocumentInLocalStorage(PersonFinancialAssetViewModel _personFinancialAssetViewModel, string _financialAssetDocumentLocalStoragePath, IEnumerable<PersonFinancialAssetViewModel> _personFinancialAssetViewModelList, string _entryType);

        bool AttachFinancialAssetDocumentInDatabaseStorage(PersonFinancialAssetViewModel _personFinancialAssetViewModel, IEnumerable<PersonFinancialAssetViewModel> _personFinancialAssetViewModelList, string _entryType);

        bool AttachKYCDocumentInLocalStorage(PersonKYCDocumentViewModel _personKYCDocumentViewModel, string _kYCDocumentLocalStoragePath, IEnumerable<PersonKYCDocumentViewModel> _personKYCDocumentViewModelList, string _entryType);

        bool AttachKYCDocumentInDatabaseStorage(PersonKYCDocumentViewModel _personKYCDocumentViewModel, IEnumerable<PersonKYCDocumentViewModel> _personKYCDocumentViewModelList, string _entryType);

        bool AttachGSTDocumentInLocalStorage(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel, string _gSTDocumentLocalStoragePath, IEnumerable<PersonGSTReturnDocumentViewModel> _personGSTReturnDocumentViewModelList, string _entryType);

        bool AttachGSTDocumentInDatabaseStorage(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel, IEnumerable<PersonGSTReturnDocumentViewModel> _personGSTReturnDocumentViewModelList, string _entryType);

        bool AttachImmovableAssetDocumentInLocalStorage(PersonImmovableAssetViewModel _personImmovableAssetViewModel, string _immovableAssetDocumentLocalStoragePath, IEnumerable<PersonImmovableAssetViewModel> _personImmovableAssetViewModelList, string _entryType);

        bool AttachImmovableAssetDocumentInDatabaseStorage(PersonImmovableAssetViewModel _personImmovableAssetViewModel, IEnumerable<PersonImmovableAssetViewModel> _personImmovableAssetViewModelList, string _entryType);

        bool AttachIncomeTaxDetailDocumentInLocalStorage(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _incomeTaxDocumentLocalStoragePath, IEnumerable<PersonIncomeTaxDetailViewModel> _personIncomeTaxDetailViewModelList, string _entryType);

        bool AttachIncomeTaxDetailDocumentInDatabaseStorage(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, IEnumerable<PersonIncomeTaxDetailViewModel> _personIncomeTaxDetailViewModelList, string _entryType);

        bool AttachMachineryAssetDocumentInLocalStorage(PersonMachineryAssetViewModel _personMachineryAssetViewModel, string _machineryAssetDocumentLocalStoragePath, IEnumerable<PersonMachineryAssetViewModel> _personMachineryAssetViewModelList, string _entryType);

        bool AttachMachineryAssetDocumentInDatabaseStorage(PersonMachineryAssetViewModel _personMachineryAssetViewModel, IEnumerable<PersonMachineryAssetViewModel> _personMachineryAssetViewModelList, string _entryType);

        bool AttachMovableAssetDocumentInLocalStorage(PersonMovableAssetViewModel _personMovableAssetViewModel, string _movableAssetDocumentLocalStoragePath, IEnumerable<PersonMovableAssetViewModel> _personMovableAssetViewModelList, string _entryType);

        bool AttachMovableAssetDocumentInDatabaseStorage(PersonMovableAssetViewModel _personMovableAssetViewModel, IEnumerable<PersonMovableAssetViewModel> _personMovableAssetViewModelList, string _entryType);

        bool AttachPhotoDocumentInLocalStorage(PersonPhotoSignViewModel _personPhotoSignViewModel, string _photoDocumentLocalStoragePath, PersonPhotoSignViewModel personPhotoSignViewModel, string _entryType);

        bool AttachPhotoDocumentInDatabaseStorage(PersonPhotoSignViewModel _personPhotoSignViewModel, PersonPhotoSignViewModel personPhotoSignViewModel, string _entryType);

        bool AttachSignDocumentInLocalStorage(PersonPhotoSignViewModel _personPhotoSignViewModel, string _signDocumentLocalStoragePath, PersonPhotoSignViewModel personPhotoSignViewModel, string _entryType);

        bool AttachSignDocumentInDatabaseStorage(PersonPhotoSignViewModel _personPhotoSignViewModel, PersonPhotoSignViewModel personPhotoSignViewModel, string _entryType);

        //Added by Rahul Kharat Date - 21.10.2024 Time : 05:50 PM

        string GetFullFilePath(string _fullPath, string _nameOfFile);

        bool FileExist(string _fullFilePath);
        bool DeleteOldLocalStorageDocument();
        //-----------------------------------------//

        Task<bool> SaveData();
    }
}
