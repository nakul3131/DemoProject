using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonDetailRepository
    {
        byte GetAddressTypePrmKeyById(Guid _addressTypeId);
        byte GetAreaTypePrmKeyById(Guid _areaTypeId);
        int GetAgentPrmKeyById(Guid _agentId);
        byte GetBloodGroupPrmKeyById(Guid _bloodGroupId);
        short GetCastCategoryPrmKeyById(Guid _castCategoryId);
        byte GetCenterCategoryPrmKeyById(Guid _centerId);
        byte GetContactGroupPrmKeyById(Guid _contactGroupId);
        short GetCenterPrmKeyById(Guid _CenterId);
        byte GetContactTypePrmKeyById(Guid _contactTypeId);
        string GetSysNameOfPersonTypeById(Guid _personTypeId);
        short GetMatchGSTNumberByCenterId(Guid _stateId);
        int GetlistofOccupation(Guid _occupationId);
        string GetListOfMaritalStatus(Guid _maritalStatusId);
        short GetCountryPrmKeyById(Guid _centerId);
        byte GetCourtCaseStagePrmKeyById(Guid _courtCaseStageId);
        byte GetCourtCaseTypePrmKeyById(Guid _courtCaseTypeId);
        byte GetDirectionPrmKeyById(Guid _directionId);
        short GetDiseasePrmKeyById(Guid _diseaseId);
        short GetDocumentDocumentTypePrmKeyById(Guid _documentDocumentTypeId);
        short GetDocumentPrmKeyById(Guid _documentId);
        byte GetDocumentTypePrmKeyById(Guid _documentTypeId);
        byte GetEducationLevelPrmKeyById(Guid _educationLevelId);
        short GetEducationQualificationPrmKeyById(Guid _educationQualificationId);
        byte GetFamilySystemPrmKeyById(Guid _familySystemId);
        byte GetGenderPrmKeyById(Guid _genderId);
        byte GetGuardianTypePrmKeyById(Guid _guardianTypeId);
        byte GetIdentificationPrmKeyById(Guid _identificationId);
        string GetSysNameOfDocumentByDocumentId(Guid _documentId);
        string GetSysNameOfIdentificationByIdentificationId(Guid _IdentificationId);
        string GetSysNameOfMaritalStatusById(Guid _marritalStatusId);
        short GetIncomeSourcePrmKeyById(Guid _incomeSourceId);
        short GetInsuranceCompanyPrmKeyById(Guid _insuranceCompanyId);
        byte GetInsuranceTypePrmKeyById(Guid _insuranceTypeId);
        short GetJewelAssayerPrmKeyById(Guid _jewelAssayerId);
        byte GetLocalGovernmentPrmKeyById(Guid _localGovernmentId);
        byte GetMaritalStatusPrmKeyById(Guid _maritalStatusId);
        byte GetNatureOfEmployerPrmKeyById(Guid _natureOfEmployerId);
        short GetOccupationPrmKeyById(Guid _OccupationId);
        byte GetOwnershipTypePrmKeyById(Guid _ownershipTypeId);
        byte GetPersonCategoryPrmKeyById(Guid _personCategoryId);
        long GetPersonPrmKeyById(Guid _personId);

        string GetSysNameOfOccupationById(Guid _OccupationId);
        string GetSysNameOfContactTypeById(Guid _contactTypeId);

        short GetPersonInformationParameterNoticeTypePrmKeyByNoticeTypeId(Guid _noticeTypeId);
        short GetPersonDocumentTypePrmKeyByDocumentTypeId(Guid _documentTypeId); // i.e Person Information Document Type Parameter Id
        byte GetPersonTypePrmKeyById(Guid _personTypeId);
        byte GetGovernmentPersonTypePrmKey();
        byte GetPhysicalStatusPrmKeyById(Guid _physicalStatusId);
        byte GetPovertyStatusPrmKeyById(Guid _povertyStatusId);
        byte GetPrefixPrmKeyById(Guid _prefixId);
        byte GetRelationPrmKeyById(Guid _relationId);
        short GetReligionPrmKeyById(Guid _religionId);
        short GetReservationCategoryPrmKeyById(Guid _reservationCategoryId);
        byte GetResidenceTypePrmKeyById(Guid _residenceTypeId);

        short GetTradingEntityPrmKeyById(Guid _TradingEntityId);
        short GetWorldWideTimeZonePrmKeyById(Guid _worldWideTimeZoneId);


        bool IsRegionalCountryCity(Guid _centerId);


        short GetOrganizationRegisteredCityPrmKey();
        bool GetPersonInfoNumber(long personInformationNumber);
        long GetPersonPrmKeyByPersonInfoNumber(long _personInfoNumber);

        bool IsPersonRecordModified(long _personPrmKey);
        bool IsPersonDepositor(long _personPrmKey);
        bool IsPersonBorrower(long _personPrmKey);
        bool IsPersonGuarantor(long _personPrmKey);

        bool GetBusinessRegistrationNumber(string businessRegistrationNumber);

        Guid GetUnmarriedStatusId();

        DateTime GetPersonBirthDateByPrmKey(long _personPrmKey);

        int GetPersonCurrentAge(Guid _personId);

        int GetPersonCurrentAge(long _personInfoNumber);

        IEnumerable<DocumentViewModel> DocumentValidations(string _documentTypeIdText);

        string GetPersonContactDetailEntryStatus(long _personContactDetailPrmKey);

        string GetPersonAddressDetailEntryStatus(long _personContactDetailPrmKey);

        List<string> GetPersonAutoCompleteList(string _inputString);


        List<SelectListItem> AddressTypeDropdownList { get; }
        List<SelectListItem> AreaTypeDropdownList { get; }
        List<SelectListItem> AgentDropdownList { get; }
        List<SelectListItem> BloodGroupDropdownList { get; }        
        List<SelectListItem> CastCategoryDropdownList { get; }
        List<SelectListItem> CenterCategoryDropdownList { get; }
        List<SelectListItem> CityDropdownList { get; }
        List<SelectListItem> ContactGroupDropdownList { get; }
        List<SelectListItem> ContactTypeDropdownList { get; }
        List<SelectListItem> ContinentDropdownList { get; }
        List<SelectListItem> CountryDropdownList { get; }
        List<SelectListItem> CourtCaseStageDropdownList { get; }
        List<SelectListItem> CourtCaseTypeDropdownList { get; }
        List<SelectListItem> DirectionDropdownList { get; }
        List<SelectListItem> DiseaseDropdownList { get; }
        List<SelectListItem> DistrictDropdownList { get; }
        List<SelectListItem> DistrictDropdownListByDivisionId(Guid _divisionId);
        List<SelectListItem> DivisionDropdownList { get; }
        List<SelectListItem> DivisionDropdownListByStateId(Guid _stateId);
        List<SelectListItem> GetDocumentDropdownEntries(Guid _documentTypeId);
        List<SelectListItem> DocumentDropdownList { get; }
        List<SelectListItem> DocumentTypeDropdownList { get; }
        List<SelectListItem> PersonDocumentDropdownList { get; }
        List<SelectListItem> EducationLevelDropdownList { get; }
        List<SelectListItem> EducationQualificationDropdownList { get; }
        List<SelectListItem> FamilySystemDropdownList { get; }
        List<SelectListItem> GenderDropdownList { get; }
        List<SelectListItem> GuardianTypeDropdownList { get; }
        List<SelectListItem> IdentificationDropdownList { get; }
        List<SelectListItem> IncomeSourceDropdownList { get; }
        List<SelectListItem> InsuranceCompanyDropdownList { get; }
        List<SelectListItem> InsuranceTypeDropdownList { get; }
        List<SelectListItem> JewelAssayerDropdownList { get; }
        List<SelectListItem> LocalGovernmentDropdownList { get; }
        List<SelectListItem> MaritalStatusDropdownList { get; }
        List<SelectListItem> NatureOfEmployerDropdownList { get; }
        List<SelectListItem> OccupationDropdownList { get; }
        List<SelectListItem> ParentOccupationDropdownList { get; }
        List<SelectListItem> PostalOfficeDropdownListByTalukaId(Guid _talukaId);
        List<SelectListItem> OwnershipTypeDropdownList { get; }
        List<SelectListItem> PersonCategoryDropdownList { get; }
        List<SelectListItem> PersonInformationParameterDropdownList { get; }
        List<SelectListItem> PersonTypeDropdownList { get; }
        List<SelectListItem> GovernmentPersonDropdownList { get; }
        List<SelectListItem> PersonDropdownList { get; }
        List<SelectListItem> PersonDropdownListByUserType(Guid _userTypeId);
        List<SelectListItem> PersonDocumentTypeDropdownList { get; }
        List<SelectListItem> PersonInfoNumbersDropdownList { get; }
        List<SelectListItem> PersonInfoNumbersAgeAbove18DropdownList { get; }
        List<SelectListItem> PersonMaleDropdownList { get; }
        List<SelectListItem> PersonFemaleDropdownList { get; }
        List<SelectListItem> PersonMemberDropdownList { get; }
        List<SelectListItem> NonMemberPersonDropdownList { get; }
        List<SelectListItem> NonDemandDepositorPersonDropdownList { get; }
        List<SelectListItem> NonCustomerPersonMemberDropdownList { get; }
        List<SelectListItem> PersonMaleMemberDropdownList { get; }
        List<SelectListItem> PersonFemaleMemberDropdownList { get; }      
        List<SelectListItem> PhysicalStatusDropdownList { get; }
        List<SelectListItem> PovertyStatusDropdownList { get; }
        List<SelectListItem> PrefixDropdownList { get; }
        List<SelectListItem> RelationDropdownList { get; }
        List<SelectListItem> FamilyRelationDropdownList { get; }
        List<SelectListItem> ReligionDropdownList { get; }
        List<SelectListItem> ReservationCategoryDropdownList { get; }
        List<SelectListItem> ResidenceTypeDropdownList { get; }
        List<SelectListItem> SubContinentDropdownList { get; }
        List<SelectListItem> SubDivisionOfficeDropdownList { get; }
        List<SelectListItem> SubDivisionOfficeDropdownListByDistrictId(Guid _districtId);
        List<SelectListItem> StateDropdownList { get; }
        List<SelectListItem> StateDropdownListByCountryId(Guid _countryId);
        List<SelectListItem> UnionTerritoriesDropdownList { get; }
        List<SelectListItem> StateUnionTerritoriesDropdownList { get; }
        List<SelectListItem> TalukaDropdownList { get; }
        List<SelectListItem> TalukaDropdownListBySubDivisionOfficeId(Guid _divisionOfficeId);
        List<SelectListItem> TradingEntityDropdownList { get; }
        List<SelectListItem> TownDropdownList { get; }
        List<SelectListItem> VillageDropdownList { get; }
        List<SelectListItem> VillageTownCityDropdownList { get; }
        List<SelectListItem> WorldWideTimeZoneDropdownList { get; }

        List<SelectListItem> GetPersonDropdownListForSharesAccountOpening(Guid _schemeId);
        List<SelectListItem> GetPersonDropdownListForDemandDepositAccountOpening(Guid _schemeId);
        List<SelectListItem> GetPersonDropdownListForBusinessLoanAccountOpening(Guid _schemeId);
        List<SelectListItem> GetPersonDropdownListForLoanAccountOpening(Guid _schemeId);
        //List<SelectListItem> NonCustomerPersonDropdownListBySchemeId(Guid _schemeId);
        List<SelectListItem> GetGuarantorPersonDropdownListBySchemeId(Guid _schemeId);
        List<SelectListItem> GetDocumentDropdownListByDocumentTypePrmKey(short _documentTypePrmKey);
    }
}
