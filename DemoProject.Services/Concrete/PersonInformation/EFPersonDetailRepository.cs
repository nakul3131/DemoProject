using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.ViewModel.Custom;
using DemoProject.Services.ViewModel.PersonInformation.Master;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonDetailRepository : IPersonDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;

        public EFPersonDetailRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
        }

        public byte GetAddressTypePrmKeyById(Guid _addressTypeId)
        {
            return context.AddressTypes
                    .Where(c => c.AddressTypeId == _addressTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetAreaTypePrmKeyById(Guid _areaTypeId)
        {
            return context.AreaTypes
                    .Where(a => a.AreaTypeId == _areaTypeId)
                    .Select(a => a.PrmKey).FirstOrDefault();
        }

        public int GetAgentPrmKeyById(Guid _agentId)
        {
            return context.Agents
                    .Where(a => a.AgentId == _agentId)
                    .Select(a => a.PrmKey).FirstOrDefault();
        }

        public byte GetBloodGroupPrmKeyById(Guid _bloodGroupId)
        {
            return context.BloodGroups
                    .Where(b => b.BloodGroupId == _bloodGroupId)
                    .Select(b => b.PrmKey).FirstOrDefault();
        }

        public short GetCastCategoryPrmKeyById(Guid _castCategoryId)
        {
            return context.CastCategories
                    .Where(c => c.CastCategoryId == _castCategoryId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetCenterCategoryPrmKeyById(Guid _centerCategoryId)
        {
            return context.CenterCategories
                    .Where(c => c.CenterCategoryId == _centerCategoryId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetCenterPrmKeyById(Guid _centerId)
        {
            var a = context.Centers
                    .Where(c => c.CenterId == _centerId)
                    .Select(c => c.PrmKey).FirstOrDefault();
            return a;
        }

        public byte GetContactGroupPrmKeyById(Guid _contactGroupId)
        {
            return context.ContactGroups
                    .Where(c => c.ContactGroupId == _contactGroupId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetContactTypePrmKeyById(Guid _contactTypeId)
        {
            return context.ContactTypes
                    .Where(c => c.ContactTypeId == _contactTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetMatchGSTNumberByCenterId(Guid _stateId)
        {
            var centerPrmKey = context.Centers.Where(c => c.CenterId == _stateId).Select(c => c.PrmKey).FirstOrDefault();

            var iSONumericCode = context.CenterISOCodes.Where(c => c.CenterPrmKey == centerPrmKey).Select(c => c.ISONumericCode).FirstOrDefault();

            return iSONumericCode;
        }

        public int GetlistofOccupation(Guid _occupationId)
        {
            return context.Occupations.Where(c => c.OccupationId == _occupationId).Select(c => c.ParentOccupationPrmKey).FirstOrDefault();
        }

        public string GetListOfMaritalStatus(Guid _maritalStatusId)
        {
            return context.MaritalStatuses.Where(c => c.MaritalStatusId == _maritalStatusId).Select(c => c.SysNameOfMaritalStatus).FirstOrDefault();
        }

        public short GetCountryPrmKeyById(Guid _centerId)
        {
            return (from c in context.Centers
                    join t in context.Centers.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.ParentCenterPrmKey equals t.PrmKey into ct
                    from t in ct.DefaultIfEmpty()
                    join sd in context.Centers.Where(sd => sd.EntryStatus == StringLiteralValue.Verify) on t.ParentCenterPrmKey equals sd.PrmKey into tsd
                    from sd in tsd.DefaultIfEmpty()
                    join ds in context.Centers.Where(ds => ds.EntryStatus == StringLiteralValue.Verify) on sd.ParentCenterPrmKey equals ds.PrmKey into sds
                    from ds in sds.DefaultIfEmpty()
                    join dv in context.Centers.Where(dv => dv.EntryStatus == StringLiteralValue.Verify) on ds.ParentCenterPrmKey equals dv.PrmKey into dsv
                    from dv in dsv.DefaultIfEmpty()
                    join st in context.Centers.Where(st => st.EntryStatus == StringLiteralValue.Verify) on dv.ParentCenterPrmKey equals st.PrmKey into dvs
                    from st in dvs.DefaultIfEmpty()
                    join cnt in context.Centers.Where(cnt => cnt.EntryStatus == StringLiteralValue.Verify) on st.ParentCenterPrmKey equals cnt.PrmKey into stc
                    from cnt in stc.DefaultIfEmpty()
                    where (c.CenterId == _centerId)
                    select cnt.PrmKey).FirstOrDefault();
        }

        public byte GetCourtCaseStagePrmKeyById(Guid _courtCaseStageId)
        {
            return context.CourtCaseStages
                    .Where(c => c.CourtCaseStageId == _courtCaseStageId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetCourtCaseTypePrmKeyById(Guid _courtCaseTypeId)
        {
            return context.CourtCaseTypes
                    .Where(c => c.CourtCaseTypeId == _courtCaseTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetDirectionPrmKeyById(Guid _directionId)
        {
            return context.Directions
                    .Where(d => d.DirectionId == _directionId)
                    .Select(d => d.PrmKey).FirstOrDefault();
        }

        public short GetDiseasePrmKeyById(Guid _diseaseId)
        {
            return context.Diseases
                    .Where(c => c.DiseaseId == _diseaseId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetDocumentDocumentTypePrmKeyById(Guid _documentDocumentTypeId)
        {
            return context.DocumentDocumentTypes
                    .Where(c => c.DocumentDocumentTypeId == _documentDocumentTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetDocumentPrmKeyById(Guid _documentId)
        {
            return context.Documents
                    .Where(c => c.DocumentId == _documentId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetDocumentTypePrmKeyById(Guid _documentTypeId)
        {
            return context.DocumentTypes
                    .Where(d => d.DocumentTypeId == _documentTypeId)
                    .Select(d => d.PrmKey).FirstOrDefault();
        }

        public byte GetEducationLevelPrmKeyById(Guid _educationLevelId)
        {
            return context.EducationLevels
                    .Where(d => d.EducationLevelId == _educationLevelId)
                    .Select(d => d.PrmKey).FirstOrDefault();
        }

        public short GetEducationQualificationPrmKeyById(Guid _educationQualificationId)
        {
            return context.EducationQualifications
                    .Where(c => c.EducationQualificationId == _educationQualificationId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetFamilySystemPrmKeyById(Guid _familySystemId)
        {
            return context.FamilySystems
                    .Where(f => f.FamilySystemId == _familySystemId)
                    .Select(f => f.PrmKey).FirstOrDefault();
        }

        public byte GetGenderPrmKeyById(Guid _genderId)
        {
            return context.Genders
                    .Where(g => g.GenderId == _genderId)
                    .Select(g => g.PrmKey).FirstOrDefault();
        }

        public byte GetGuardianTypePrmKeyById(Guid _guardianTypeId)
        {
            return context.GuardianTypes
                    .Where(g => g.GuardianTypeId == _guardianTypeId)
                    .Select(g => g.PrmKey).FirstOrDefault();
        }

        public byte GetIdentificationPrmKeyById(Guid _IdentificationId)
        {
            return context.Identifications
                    .Where(a => a.IdentificationId == _IdentificationId)
                    .Select(a => a.PrmKey).FirstOrDefault();
        }

        public string GetSysNameOfIdentificationByIdentificationId(Guid _IdentificationId)
        {
            return context.Identifications
                    .Where(a => a.IdentificationId == _IdentificationId)
                    .Select(a => a.SysNameOfIdentification).FirstOrDefault();
        }

        public string GetSysNameOfDocumentByDocumentId(Guid _documentTypeId)
        {
            int documentPrmkey = context.DocumentDocumentTypes
                    .Where(c => c.DocumentDocumentTypeId == _documentTypeId)
                    .Select(c => c.DocumentPrmKey).FirstOrDefault();

            return context.Documents
                    .Where(d => d.PrmKey == documentPrmkey)
                    .Select(d => d.SysNameOfDocument).FirstOrDefault();
        }

        public string GetSysNameOfMaritalStatusById(Guid _marritalStatusId)
        {
            return context.MaritalStatuses
                      .Where(c => c.MaritalStatusId == _marritalStatusId)
                      .Select(c => c.SysNameOfMaritalStatus).FirstOrDefault();

        }
        public short GetIncomeSourcePrmKeyById(Guid _incomeSourceId)
        {
            return context.IncomeSources
                    .Where(c => c.IncomeSourceId == _incomeSourceId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetInsuranceCompanyPrmKeyById(Guid _insuranceCompanyId)
        {
            return context.InsuranceCompanies
                    .Where(m => m.InsuranceCompanyId == _insuranceCompanyId)
                    .Select(m => m.PrmKey).FirstOrDefault();
        }

        public byte GetInsuranceTypePrmKeyById(Guid _insuranceTypeId)
        {
            return context.InsuranceTypes
                    .Where(m => m.InsuranceTypeId == _insuranceTypeId)
                    .Select(m => m.PrmKey).FirstOrDefault();
        }

        public short GetJewelAssayerPrmKeyById(Guid _jewelAssayerId)
        {
            return context.JewelAssayers
                    .Where(j => j.JewelAssayerId == _jewelAssayerId)
                    .Select(j => j.PrmKey).FirstOrDefault();
        }

        public byte GetLocalGovernmentPrmKeyById(Guid _localGovernmentId)
        {
            return context.LocalGovernments
                    .Where(d => d.LocalGovernmentId == _localGovernmentId)
                    .Select(d => d.PrmKey).FirstOrDefault();
        }

        public byte GetMaritalStatusPrmKeyById(Guid _maritalStatusId)
        {
            return context.MaritalStatuses
                    .Where(m => m.MaritalStatusId == _maritalStatusId)
                    .Select(m => m.PrmKey).FirstOrDefault();
        }

        public string GetNameOfUserTypeById(Guid _userTypeId)
        {
            return context.UserTypes
                      .Where(c => c.UserTypeId == _userTypeId)
                      .Select(c => c.NameOfUserType).FirstOrDefault();

        }
        public byte GetNatureOfEmployerPrmKeyById(Guid _natureOfEmployerId)
        {
            return context.EmployerNatures
                    .Where(n => n.EmployerNatureId == _natureOfEmployerId)
                    .Select(n => n.PrmKey).FirstOrDefault();
        }

        public short GetOccupationPrmKeyById(Guid _occupationId)
        {
            return context.Occupations
                    .Where(c => c.OccupationId == _occupationId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetOwnershipTypePrmKeyById(Guid _ownershipTypeId)
        {
            return context.OwnershipTypes
                    .Where(r => r.OwnershipTypeId == _ownershipTypeId)
                    .Select(r => r.PrmKey).FirstOrDefault();
        }

        public byte GetPersonCategoryPrmKeyById(Guid _personCategoryId)
        {
            return context.PersonCategories
                    .Where(r => r.PersonCategoryId == _personCategoryId)
                    .Select(r => r.PrmKey).FirstOrDefault();
        }

        public long GetPersonPrmKeyById(Guid _personId)
        {
            return context.People
                    .Where(c => c.PersonId == _personId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetPersonInformationParameterNoticeTypePrmKeyByNoticeTypeId(Guid _noticeTypeId)
        {
            return (from p in context.PersonInformationParameterNoticeTypes
                    join n in context.NoticeTypes.Where(n => n.NoticeTypeId == _noticeTypeId) on p.NoticeTypePrmKey equals n.PrmKey into pn
                    from n in pn.DefaultIfEmpty()
                    where (p.EntryStatus == EntryStatus.Verified)
                    select (p.PrmKey)).FirstOrDefault();
        }

        public byte GetPersonTypePrmKeyById(Guid _personTypeId)
        {
            return context.PersonTypes
                    .Where(r => r.PersonTypeId == _personTypeId)
                    .Select(r => r.PrmKey).FirstOrDefault();
        }

        public short GetPersonDocumentTypePrmKeyByDocumentTypeId(Guid _documentTypeId)  // i.e Person Information Document Type Parameter Id
        {

            return (from p in context.PersonInformationParameterDocumentTypes
                    join d in context.DocumentTypes.Where(d => d.DocumentTypeId == _documentTypeId) on p.DocumentTypePrmKey equals d.PrmKey into pd
                    from d in pd.DefaultIfEmpty()
                    where (p.EntryStatus == EntryStatus.Verified)
                    select (p.PrmKey)).FirstOrDefault();
        }


        public byte GetPhysicalStatusPrmKeyById(Guid _physicalStatusId)
        {
            return context.PhysicalStatuses
                    .Where(p => p.PhysicalStatusId == _physicalStatusId)
                    .Select(p => p.PrmKey).FirstOrDefault();
        }

        public byte GetPovertyStatusPrmKeyById(Guid _povertyStatusId)
        {
            return context.PovertyStatuses
                    .Where(p => p.PovertyStatusId == _povertyStatusId)
                    .Select(p => p.PrmKey).FirstOrDefault();
        }

        public byte GetPrefixPrmKeyById(Guid _prefixId)
        {
            return context.Prefixes
                    .Where(c => c.PrefixId == _prefixId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetRelationPrmKeyById(Guid _relationId)
        {
            return context.Relations
                    .Where(r => r.RelationId == _relationId)
                    .Select(r => r.PrmKey).FirstOrDefault();
        }

        public short GetReligionPrmKeyById(Guid _religionId)
        {
            return context.Religions
                    .Where(c => c.ReligionId == _religionId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetReservationCategoryPrmKeyById(Guid _reservationCategoryId)
        {
            return context.ReservationCategories
                    .Where(c => c.ReservationCategoryId == _reservationCategoryId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetResidenceTypePrmKeyById(Guid _residenceTypeId)
        {
            return context.ResidenceTypes
                    .Where(r => r.ResidenceTypeId == _residenceTypeId)
                    .Select(r => r.PrmKey).FirstOrDefault();
        }

        public short GetTradingEntityPrmKeyById(Guid _tradingEntityId)
        {
            return context.TradingEntities
                    .Where(c => c.TradingEntityId == _tradingEntityId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetWorldWideTimeZonePrmKeyById(Guid _worldWideTimeZoneId)
        {
            return context.WorldWideTimeZones
                    .Where(w => w.WorldWideTimeZoneId == _worldWideTimeZoneId)
                    .Select(d => d.PrmKey).FirstOrDefault();
        }



        public byte GetGovernmentPersonTypePrmKey()
        {
            return context.PersonTypes
                    .Where(r => r.SysNameOfPersonType == "GOVRM")
                    .Select(r => r.PrmKey).FirstOrDefault();
        }

        public short GetOrganizationRegisteredCityPrmKey()
        {
            short result;

            short registerdCityPrmKey = context.Organizations
                                .Where(o => o.EntryStatus == StringLiteralValue.Verify)
                                .Select(o => o.CenterPrmKey).FirstOrDefault();

            byte centerCategoryPrmKey = context.Centers
                                        .Where(c => c.PrmKey == registerdCityPrmKey)
                                        .Select(c => c.CenterCategoryPrmKey).FirstOrDefault();

            if (centerCategoryPrmKey < 2)
            {
                result = registerdCityPrmKey;
            }
            else
            {
                result = context.Centers
                                .Where(c => c.PrmKey == registerdCityPrmKey)
                                .Select(c => c.ParentCenterPrmKey).FirstOrDefault();
            }

            return result;
        }

        public List<string> GetPersonAutoCompleteList(string _inputString)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            List<string> result = new List<string>();
            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                var search = (from p in context.People
                              join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                              from mf in pm.DefaultIfEmpty()
                              join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                              from t in pt.DefaultIfEmpty()
                              join c in context.PersonContactDetails.Where(c => c.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals c.PersonPrmKey into pc
                              from c in pc.DefaultIfEmpty()
                              join k in context.PersonKYCDetails.Where(k => k.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals k.PersonPrmKey into pk
                              from k in pk.DefaultIfEmpty()
                              where (p.EntryStatus == StringLiteralValue.Verify && p.ActivationStatus == StringLiteralValue.Active && t.LanguagePrmKey == regionalLanguagePrmKey)
                                     && (p.FullName.Contains(_inputString) || t.TransFullName.Contains(_inputString) || p.PersonInformationNumber.ToString().Contains(_inputString) || p.DateOfBirth.ToString().Contains(_inputString)
                                          || c.FieldValue.Contains(_inputString) || k.DocumentNumber.Contains(_inputString))
                              orderby p.FullName
                              select new
                              {
                                  p.FullName,
                                  t.TransFullName,
                                  p.DateOfBirth,
                                  p.PersonInformationNumber,
                                  p.PersonId,
                                  c.FieldValue,
                                  k.DocumentNumber,
                              }).Distinct().ToList();
                var list = search.GroupBy(p => p.FullName)
                        .Select(l => new { FullName1 = l.FirstOrDefault().FullName, TransFullName1 = l.FirstOrDefault().TransFullName, DateOfBirth1 = l.FirstOrDefault().DateOfBirth, PersonInformationNumber1 = l.FirstOrDefault().PersonInformationNumber, PersonId1 = l.FirstOrDefault().PersonId, FieldValue1 = l.FirstOrDefault().FieldValue, DocumentNumber1 = l.FirstOrDefault().DocumentNumber }).Distinct().ToList();
                result = list.Select(x => string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}", x.FullName1, x.TransFullName1, x.DateOfBirth1, x.PersonInformationNumber1, x.PersonId1, x.FieldValue1, x.DocumentNumber1)).ToArray().Distinct().ToList();
                return result;
            }

            // Default List In Default Language (i.e. English)
            var search1 = (from p in context.People
                           join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                           from mf in pm.DefaultIfEmpty()
                           join c in context.PersonContactDetails.Where(c => c.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals c.PersonPrmKey into pc
                           from c in pc.DefaultIfEmpty()
                           join k in context.PersonKYCDetails.Where(k => k.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals k.PersonPrmKey into pk
                           from k in pk.DefaultIfEmpty()
                           where (p.EntryStatus == StringLiteralValue.Verify && p.ActivationStatus == StringLiteralValue.Active)
                                  && (p.FullName.Contains(_inputString) || p.PersonInformationNumber.ToString().Contains(_inputString) || p.DateOfBirth.ToString().Contains(_inputString)
                                       || c.FieldValue.Contains(_inputString) || k.DocumentNumber.Contains(_inputString))
                           orderby p.FullName

                           select new
                           {
                               p.FullName,
                               p.DateOfBirth,
                               p.PersonInformationNumber,
                               p.PersonId,
                               c.FieldValue,
                               k.DocumentNumber
                           }).Distinct().ToList();
            var list1 = search1.GroupBy(p => p.FullName)
                        .Select(l => new { FullName1 = l.FirstOrDefault().FullName, DateOfBirth1 = l.FirstOrDefault().DateOfBirth, PersonInformationNumber1 = l.FirstOrDefault().PersonInformationNumber, PersonId1 = l.FirstOrDefault().PersonId, FieldValue1 = l.FirstOrDefault().FieldValue, DocumentNumber1 = l.FirstOrDefault().DocumentNumber }).Distinct().ToList();
            result = list1.Select(x => string.Format("{0}-{1}-{2}-{3}-{4}-{5}", x.FullName1, x.DateOfBirth1, x.PersonInformationNumber1, x.PersonId1, x.FieldValue1, x.DocumentNumber1)).ToArray().Distinct().ToList();
            return result;
        }

        public bool GetPersonInfoNumber(long personInformationNumber)
        {
            bool status;
            if (context.People.Where(p => p.PersonInformationNumber == personInformationNumber).Select(p => p.PrmKey).FirstOrDefault() > 0)
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

        public bool GetBusinessRegistrationNumber(string _businessRegistrationNumber)
        {
            bool status;
            if (context.PersonGroups.Where(p => p.BusinessRegistrationNumber == _businessRegistrationNumber).Select(p => p.PrmKey).FirstOrDefault() > 0)
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

        public bool IsRegionalCountryCity(Guid _centerId)
        {
            //short regionalCountryPrmKey;
            //short countryPrmKey;

            //regionalCountryPrmKey = GetOrganizationRegisteredCityPrmKey();

            //countryPrmKey = GetCountryPrmKeyById(_centerId);

            //if (regionalCountryPrmKey == countryPrmKey)
            //    return true;
            //else
                return false;
        }

        public long GetPersonPrmKeyByPersonInfoNumber(long _PersonInfoNumber)
        {
            return context.People
                    .Where(c => c.PersonInformationNumber == _PersonInfoNumber)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public bool IsPersonRecordModified(long _personPrmKey)
        {
            return context.People
                        .Where(p => p.PrmKey == _personPrmKey && p.EntryStatus == StringLiteralValue.Verify)
                        .Select(p => p.IsModified).FirstOrDefault();
        }

        public bool IsPersonDepositor(long _personPrmKey)
        {
            return context.PersonStatuses
                        .Where(p => p.PrmKey == _personPrmKey && p.EntryStatus == StringLiteralValue.Verify)
                        .Select(p => p.IsDepositor).FirstOrDefault();
        }

        public bool IsPersonBorrower(long _personPrmKey)
        {
            byte personBorrowerStatus =  context.PersonStatuses 
                                        .Where(p => p.PrmKey == _personPrmKey && p.EntryStatus == StringLiteralValue.Verify)
                                        .Select(p => p.BorrowingStatus).FirstOrDefault();

            if (personBorrowerStatus > 0)
                return true;

            return false;
        }

        public bool IsPersonGuarantor(long _personPrmKey)
        {
            byte personGuarantorStatus = context.PersonStatuses
                                        .Where(p => p.PrmKey == _personPrmKey && p.EntryStatus == StringLiteralValue.Verify)
                                        .Select(p => p.BorrowingStatus).FirstOrDefault();

            if (personGuarantorStatus > 0)
                return true;

            return false;
        }

        public Guid GetUnmarriedStatusId()
        {
            return context.MaritalStatuses
                        .Where(m => m.SysNameOfMaritalStatus == StringLiteralValue.Unmarried)
                        .Select(m => m.MaritalStatusId).FirstOrDefault();
        }


        public DateTime GetPersonBirthDateByPrmKey(long _personPrmKey)
        {
            if (IsPersonRecordModified(_personPrmKey))
            {
                return  context.PersonModifications
                            .Where(p => p.PersonPrmKey == _personPrmKey && p.EntryStatus == StringLiteralValue.Verify)
                            .Select(p => p.DateOfBirth).FirstOrDefault();
            }
            else
            {
                return  context.People
                            .Where(p => p.PrmKey == _personPrmKey && p.EntryStatus == StringLiteralValue.Verify)
                            .Select(p => p.DateOfBirth).FirstOrDefault();
            }
        }

        public byte GetPersonCategoryPrmKeyBySysName(string _sysNameOfUserType)
        {
            return context.PersonCategories
                                      .Where(x => x.NameOfPersonCategory == _sysNameOfUserType)
                                      .Select(x => x.PrmKey).FirstOrDefault();

        }

        public int GetPersonCurrentAge(Guid _personId)
        {
            long personPrmKey = GetPersonPrmKeyById(_personId);

            return GetPersonCurrentAgeByPrmKey(personPrmKey);
        }

        public int GetPersonCurrentAge(long _personInfoNumber)
        {
            long personPrmKey = GetPersonPrmKeyByPersonInfoNumber(_personInfoNumber);

            return GetPersonCurrentAgeByPrmKey(personPrmKey);
        }

        private int GetPersonCurrentAgeByPrmKey(long _prmKey)
        {
            DateTime now = DateTime.Now;

            DateTime personBirthDate = GetPersonBirthDateByPrmKey(_prmKey);

            int age = now.Year - personBirthDate.Year;

            if (now.DayOfYear < personBirthDate.DayOfYear)
            {
                age--;
            }

            return age;
        }

        public string GetPersonContactDetailEntryStatus(long _personContactDetailPrmKey)
        {
            return context.PersonContactDetails
                                      .Where(x => x.PrmKey == _personContactDetailPrmKey)
                                      .Select(x => x.EntryStatus).FirstOrDefault();

        }

        public string GetPersonAddressDetailEntryStatus(long _personAddressDetailPrmKey)
        {
            return context.PersonAddresses
                                      .Where(x => x.PrmKey == _personAddressDetailPrmKey)
                                      .Select(x => x.EntryStatus).FirstOrDefault();

        }

        public IEnumerable<DocumentViewModel> DocumentValidations(string _documentTypeIdText)
        {
            var documentValidations = context.Documents
                                      .Where(x => x.NameOfDocument == _documentTypeIdText)
                                      .Select(x => new DocumentViewModel { MaximumFileSize = x.MaximumFileSize, AllowedFileFormats = x.AllowedFileFormats }).Distinct().ToList();

            return documentValidations;
        }

        public string GetSysNameOfOccupationById(Guid _occupationId)
        {
            return context.Occupations
                .Where(o => o.OccupationId == _occupationId)
                .Select(o => o.SysNameOfOccupation).FirstOrDefault();
        }

        public string GetSysNameOfContactTypeById(Guid _contactTypeId)
        {
            return context.ContactTypes
                .Where(c => c.ContactTypeId == _contactTypeId)
                .Select(c => c.SysNameOfContact).FirstOrDefault();
        }
        // DropdownList
        public List<SelectListItem> AddressTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from a in context.AddressTypes
                            join t in context.AddressTypeTranslations on a.PrmKey equals t.AddressTypePrmKey into at
                            from t in at.DefaultIfEmpty()
                            where (a.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = a.AddressTypeId.ToString(),
                                Text = a.NameOfAddressType.Trim() + " ---> " + (t.TransNameOfAddressType.Trim() ?? " ") // Use null-coalescing operator for null handling
                            }).Distinct().OrderBy(l=>l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from a in context.AddressTypes
                        where (a.ActivationStatus== StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = a.AddressTypeId.ToString(),
                            Text = a.NameOfAddressType.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> AreaTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from a in context.AreaTypes
                            join t in context.AreaTypeTranslations on a.PrmKey equals t.AreaTypePrmKey into at
                            from t in at.DefaultIfEmpty()
                            where (a.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby a.NameOfAreaType
                            select new SelectListItem
                            {
                                Value = a.AreaTypeId.ToString(),
                                Text = (a.NameOfAreaType.Trim() + " ---> " + (t.TransNameOfAreaType.Trim() ?? " " )) // Use null-coalescing operator for null handling
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from a in context.AreaTypes
                        join t in context.AreaTypeTranslations on a.PrmKey equals t.AreaTypePrmKey into at
                        from t in at.DefaultIfEmpty()
                        where (a.ActivationStatus == StringLiteralValue.Active)
                        orderby a.NameOfAreaType
                        select new SelectListItem
                        {
                            Value = a.AreaTypeId.ToString(),
                            Text = (a.NameOfAreaType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> AgentDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var a = (from b in context.Agents
                             join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify) on b.PersonPrmKey equals p.PrmKey into bp
                             from p in bp.DefaultIfEmpty()
                             join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
                             from m in pm.DefaultIfEmpty()
                             join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify && t.LanguagePrmKey == regionalLanguagePrmKey) on p.PrmKey equals t.PersonPrmKey into pt
                             from t in pt.DefaultIfEmpty()
                             where (b.EntryStatus == StringLiteralValue.Verify)
                             && (t.LanguagePrmKey == regionalLanguagePrmKey)
                             select new SelectListItem
                             {
                                 Value = b.AgentId.ToString(),
                                 Text = (m.FullName ?? p.FullName.Trim()) + " ---> " + (t.TransFullName.Trim() ?? " ")
                             }).Distinct().OrderBy(l => l.Text).ToList();

                    return a;
                }

                // Default List In Defaul Language (i.e. English)
                return (from b in context.Agents
                        join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify) on b.PersonPrmKey equals p.PrmKey into bp
                        from p in bp.DefaultIfEmpty()
                        join m in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals m.PersonPrmKey into pm
                        from m in pm.DefaultIfEmpty()
                        where (b.EntryStatus == StringLiteralValue.Verify)
                        select new SelectListItem
                        {
                            Value = b.AgentId.ToString(),
                            Text = ((m.FullName) ?? p.FullName.Trim() + " ---> ") // Use null-coalescing operator for null handling
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> BloodGroupDropdownList
        {
            get
            {
                // Default List In Defaul Language (i.e. English)
                return (from b in context.BloodGroups
                        where b.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = b.BloodGroupId.ToString(),
                            Text = b.NameOfBloodGroup
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> CastCategoryDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.CastCategories
                            join t in context.CastCategoryTranslations on c.PrmKey equals t.CastCategoryPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.ActivationStatus == StringLiteralValue.Active)
                                   && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCastCategory
                            select new SelectListItem
                            {
                                Value = c.CastCategoryId.ToString(),
                                Text = (c.NameOfCastCategory.Trim() + " ---> " + (t.TransNameOfCastCategory.Trim() ?? " " )) // Use null-coalescing operator for null handling
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from c in context.CastCategories
                        where (c.ActivationStatus ==StringLiteralValue.Active)
                        orderby c.NameOfCastCategory
                        select new SelectListItem
                        {
                            Value = c.CastCategoryId.ToString(),
                            Text = (c.NameOfCastCategory.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }
        public List<SelectListItem> CenterCategoryDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from c in context.CenterCategories
                            join t in context.CenterCategoryTranslations on c.PrmKey equals t.CenterCategoryPrmKey
                            where c.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = c.CenterCategoryId.ToString(),
                                Text = c.NameOfCenterCategory.Trim() + " --> " + t.TransNameOfCenterCategory.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.CenterCategories
                        where c.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = c.CenterCategoryId.ToString(),
                            Text = c.NameOfCenterCategory
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> CityDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey == 3)
                                    && (c.EntryStatus == StringLiteralValue.Verify)
                                    && (c.ActivationStatus == StringLiteralValue.Active)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = ((mf.NameOfCenter) ?? c.NameOfCenter.Trim()) + " ---> " + (t.TransNameOfCenter.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey ==3)
                                && (c.EntryStatus == StringLiteralValue.Verify)
                                && (c.ActivationStatus == StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter.Trim()) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> ContactGroupDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.ContactGroups
                            join t in context.ContactGroupTranslations on d.PrmKey equals t.ContactGroupPrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (d.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = d.ContactGroupId.ToString(),
                                Text = ((d.NameOfContactGroup.Trim() + " ---> " + ((t.TransNameOfContactGroup.Trim()) ?? " " )))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.ContactGroups
                        where (d.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = d.ContactGroupId.ToString(),
                            Text = (d.NameOfContactGroup.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> ContactTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.ContactTypes
                            join t in context.ContactTypeTranslations on c.PrmKey equals t.ContactTypePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (c.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = c.ContactTypeId.ToString(),
                                Text = (c.NameOfContactType + " ---> " + (t.TransNameOfContactType.Trim() ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.ContactTypes
                        where (c.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = c.ContactTypeId.ToString(),
                            Text = (c.NameOfContactType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> ContinentDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey == 12)   // 12 Is Continent CenterCategory
                            && (c.EntryStatus == StringLiteralValue.Verify)
                            && (c.ActivationStatus ==StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                //Text = ((mf.NameOfCenter.Equals(null)) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Equals(null) ? " " : t.TransNameOfCenter.Trim()))
                                Text = ((mf.NameOfCenter ==null) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Trim() ?? " " ) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Trim() ?? " " ))

                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey ==12)   // 12 Is Continent CenterCategory
                                && (c.EntryStatus ==StringLiteralValue.Verify)
                                && (c.ActivationStatus ==StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            //Text = ((mf.NameOfCenter.Equals(null)) ? c.NameOfCenter.Trim() : mf.NameOfCenter.Trim())
                            Text = ((mf.NameOfCenter.Trim()) ?? c.NameOfCenter.Trim() )

                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> CountryDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey ==10)   // 10 Is Country CenterCategory 
                            && (c.EntryStatus == StringLiteralValue.Verify)
                            && (c.ActivationStatus ==StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = ((mf.NameOfCenter) ?? c.NameOfCenter.Trim()) + " ---> " + (t.TransNameOfCenter.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey ==10)   // 10 Is Country CenterCategory 
                        && (c.EntryStatus ==StringLiteralValue.Verify)
                        && (c.ActivationStatus ==StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter.Trim()) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> CourtCaseStageDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.CourtCaseStages
                            join t in context.CourtCaseStageTranslations on d.PrmKey equals t.CourtCaseStagePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (d.ActivationStatus ==StringLiteralValue.Active
                            && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = d.CourtCaseStageId.ToString(),
                                Text = (d.NameOfCourtCaseStage.Trim() + " ---> " + (t.TransNameOfCourtCaseStage.Trim() ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.CourtCaseStages
                        where (d.ActivationStatus ==StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = d.CourtCaseStageId.ToString(),
                            Text = (d.NameOfCourtCaseStage.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> CourtCaseTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.CourtCaseTypes
                            join t in context.CourtCaseTypeTranslations on d.PrmKey equals t.CourtCaseTypePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (d.ActivationStatus ==StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = d.CourtCaseTypeId.ToString(),
                                Text = (d.NameOfCourtCaseType.Trim() + " ---> " + (t.TransNameOfCourtCaseType.Trim() ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.CourtCaseTypes
                        where (d.ActivationStatus ==StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = d.CourtCaseTypeId.ToString(),
                            Text = (d.NameOfCourtCaseType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> DirectionDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from a in context.Directions
                            join at in context.DirectionTranslations on a.PrmKey equals at.DirectionPrmKey
                            where a.ActivationStatus ==StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = a.DirectionId.ToString(),
                                Text = a.NameOfDirection.Trim() + " --> " + at.TransNameOfDirection.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from a in context.Directions
                        where a.ActivationStatus ==StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = a.DirectionId.ToString(),
                            Text = a.NameOfDirection
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> DiseaseDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.Diseases
                            join t in context.DiseaseTranslations on d.PrmKey equals t.DiseasePrmKey into bt
                            from t in bt.DefaultIfEmpty()

                            where (d.ActivationStatus == StringLiteralValue.Active
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = d.DiseaseId.ToString(),
                                Text = (d.NameOfDisease.Trim() + " ---> " + (t.TransNameOfDisease.Trim() ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.Diseases
                        where (d.ActivationStatus ==StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = d.DiseaseId.ToString(),
                            Text = ((d.NameOfDisease.Trim()))
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> DistrictDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey ==6)    // 6 Is District CenterCategory
                            && (c.EntryStatus ==StringLiteralValue.Verify)
                            && (c.ActivationStatus ==StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = ((mf.NameOfCenter) ?? c.NameOfCenter.Trim()) + " ---> " + (t.TransNameOfCenter.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                        from t in ct.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey ==6)    // 6 Is District CenterCategory
                        && (c.EntryStatus ==StringLiteralValue.Verify)
                        && (c.ActivationStatus ==StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = (((mf.NameOfCenter).Trim()) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> DistrictDropdownListByDivisionId(Guid _divisionId)
        {
            short divisionPrmKey = GetCenterPrmKeyById(_divisionId);

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                        from t in ct.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey ==6)   // 6 Is District CenterCategory 
                        && (c.EntryStatus ==StringLiteralValue.Verify)
                        && (c.ActivationStatus ==StringLiteralValue.Active)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        && (c.ParentCenterPrmKey ==divisionPrmKey)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter ==null) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Trim() ?? " " ) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Trim() ?? " " ))
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            return (from c in context.Centers
                    join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                    from mf in cm.DefaultIfEmpty()
                    join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                    from t in ct.DefaultIfEmpty()
                    where (c.CenterCategoryPrmKey ==6)   // 6 Is District CenterCategory 
                    && (c.EntryStatus ==StringLiteralValue.Verify)
                    && (c.ActivationStatus ==StringLiteralValue.Active)
                    && (mf.EntryStatus ==StringLiteralValue.Verify || mf.EntryStatus ==null)
                    && (c.ParentCenterPrmKey ==divisionPrmKey)
                    orderby c.NameOfCenter
                    select new SelectListItem
                    {
                        Value = c.CenterId.ToString(),
                        Text = ((mf.NameOfCenter.Trim()) ?? c.NameOfCenter.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> DivisionDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey ==7)
                            && (c.EntryStatus ==StringLiteralValue.Verify)
                            && (c.ActivationStatus ==StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = (mf.NameOfCenter ?? c.NameOfCenter.Trim()) + " ---> " + (t.TransNameOfCenter.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey ==7)
                        && (c.EntryStatus == StringLiteralValue.Verify)
                        && (c.ActivationStatus ==StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> DivisionDropdownListByStateId(Guid _stateId)
        {
            short statePrmKey = GetCenterPrmKeyById(_stateId);

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                        from t in ct.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey ==7)   // 7 Is Division CenterCategory 
                        && (c.ParentCenterPrmKey == statePrmKey)
                        && (c.EntryStatus ==StringLiteralValue.Verify)
                        && (c.ActivationStatus ==StringLiteralValue.Active)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter ==null) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Trim() ?? " " ) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Trim() ?? " " ))
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            return (from c in context.Centers
                    join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                    from mf in cm.DefaultIfEmpty()
                    where (c.CenterCategoryPrmKey ==7)   // 7 Is Division CenterCategory 
                    && (c.EntryStatus ==StringLiteralValue.Verify)
                    && (c.ActivationStatus ==StringLiteralValue.Active)
                    && (c.ParentCenterPrmKey == statePrmKey)
                    orderby c.NameOfCenter
                    select new SelectListItem
                    {
                        Value = c.CenterId.ToString(),
                        Text = ((mf.NameOfCenter.Trim()) ?? c.NameOfCenter.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> GetDocumentDropdownEntries(Guid _documentTypeId)
        {
            int documentTypePrmKey = GetDocumentTypePrmKeyById(_documentTypeId);

            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            // Use == instead of Equals While Comparing Numeric Value Like (Integer/Decimal) 
            // Other Wise Get Error - Unable to create a constant value of type 'System.Object'. Only primitive types or enumeration types are supported in this context.
            if (regionalLanguagePrmKey != 1)
            {
                // Get All Valid Selectlist From ByLawsHeaders            
                return (from d in context.Documents
                        join t in context.DocumentTranslations on d.PrmKey equals t.DocumentPrmKey into dt
                        from t in dt.DefaultIfEmpty()
                        join dd in context.DocumentDocumentTypes on d.PrmKey equals dd.DocumentPrmKey into documentDocumentType
                        from dd in documentDocumentType.DefaultIfEmpty()
                        where ((d.ActivationStatus == StringLiteralValue.Active)
                                 && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                 && (dd.DocumentTypePrmKey == documentTypePrmKey)
                                 )
                        orderby d.NameOfDocument
                        select new SelectListItem
                        {
                            Value = dd.DocumentDocumentTypeId.ToString(),
                            Text = (d.NameOfDocument.Trim() + " ---> " + ((t.TransNameOfDocument.Trim()) ?? " " ))
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            // Default List In Default Language (i.e. English)
            return (from d in context.Documents
                    where (d.ActivationStatus ==StringLiteralValue.Active)
                    select new SelectListItem
                    {
                        Value = d.DocumentId.ToString(),
                        Text = (d.NameOfDocument.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();

        }

        public List<SelectListItem> DocumentDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from o in context.Documents
                            join t in context.DocumentTranslations on o.PrmKey equals t.DocumentPrmKey into ot
                            from t in ot.DefaultIfEmpty()
                            where (o.ActivationStatus ==StringLiteralValue.Active
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = o.DocumentId.ToString(),
                                Text = (o.NameOfDocument.Trim() + " ---> " + (t.TransNameOfDocument.Trim() ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from o in context.Documents
                        where (o.ActivationStatus ==StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = o.DocumentId.ToString(),
                            Text = (o.NameOfDocument.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> PersonDocumentDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from d in context.DocumentTypes
                            join t in context.DocumentTypeTranslations on d.PrmKey equals t.DocumentTypePrmKey
                            where d.ActivationStatus ==StringLiteralValue.Active
                            && d.SysNameOfDocumentType == "PERSN"
                            select new SelectListItem
                            {
                                Value = d.DocumentTypeId.ToString(),
                                Text = d.NameOfDocumentType.Trim() + " --> " + t.TransNameOfDocumentType.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.DocumentTypes
                        where d.ActivationStatus ==StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = d.DocumentTypeId.ToString(),
                            Text = d.NameOfDocumentType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> DocumentTypeDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from d in context.DocumentTypes
                            join t in context.DocumentTypeTranslations on d.PrmKey equals t.DocumentTypePrmKey
                            where d.ActivationStatus ==StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = d.DocumentTypeId.ToString(),
                                Text = d.NameOfDocumentType.Trim() + " --> " + t.TransNameOfDocumentType.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.DocumentTypes
                        where d.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = d.DocumentTypeId.ToString(),
                            Text = d.NameOfDocumentType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> EducationLevelDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from a in context.EducationLevels
                            join at in context.EducationLevelTranslations on a.PrmKey equals at.EducationLevelPrmKey
                            where a.ActivationStatus ==StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = a.EducationLevelId.ToString(),
                                Text = a.NameOfEducationLevel.Trim() + " --> " + at.TransNameOfEducationLevel.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from a in context.EducationLevels
                        where a.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = a.EducationLevelId.ToString(),
                            Text = a.NameOfEducationLevel
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> EducationQualificationDropdownList
        {
            get
            { // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from e in context.EducationQualifications
                            join t in context.EducationQualificationTranslations on e.PrmKey equals t.EducationQualificationPrmKey into et
                            from t in et.DefaultIfEmpty()
                            where (e.ActivationStatus == StringLiteralValue.Active)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = e.EducationQualificationId.ToString(),
                                Text = (e.NameOfQualification.Trim() + " ---> " + ((t.TransNameOfQualification.Trim()) ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from e in context.EducationQualifications
                        where (e.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = e.EducationQualificationId.ToString(),
                            Text = (e.NameOfQualification.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> FamilyRelationDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.Relations
                            join t in context.RelationTranslations on r.PrmKey equals t.RelationPrmKey
                            where r.IsConsiderAsFamilyRelation == true
                            select new SelectListItem
                            {
                                Value = r.RelationId.ToString(),
                                Text = r.NameOfRelation.Trim() + " --> " + t.TransNameOfRelation.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from r in context.Relations
                        where r.IsConsiderAsFamilyRelation == true
                        select new SelectListItem
                        {
                            Value = r.RelationId.ToString(),
                            Text = r.NameOfRelation
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> FamilySystemDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from a in context.FamilySystems
                            join at in context.FamilySystemTranslations on a.PrmKey equals at.FamilySystemPrmKey
                            where a.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = a.FamilySystemId.ToString(),
                                Text = a.NameOfFamilySystem.Trim() + " --> " + at.TransNameOfFamilySystem.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from a in context.FamilySystems
                        where a.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = a.FamilySystemId.ToString(),
                            Text = a.NameOfFamilySystem
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> GenderDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from g in context.Genders
                            join t in context.GenderTranslations on g.PrmKey equals t.GenderPrmKey
                            where g.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = g.GenderId.ToString(),
                                Text = g.NameOfGender.Trim() + " --> " + t.TransNameOfGender.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from g in context.Genders
                        where g.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = g.GenderId.ToString(),
                            Text = g.NameOfGender
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> GuardianTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var ss = (from j in context.GuardianTypes
                              join t in context.GuardianTypeTranslations on j.PrmKey equals t.GuardianTypePrmKey into pt
                              from t in pt.DefaultIfEmpty()
                              where (j.ActivationStatus == StringLiteralValue.Active)
                                      && (t.LanguagePrmKey == regionalLanguagePrmKey)
                              orderby j.NameOfGuardianType
                              select new SelectListItem
                              {
                                  Value = j.GuardianTypeId.ToString(),
                                  Text = (j.NameOfGuardianType.Trim() + " ---> " + (t.TransNameOfGuardianType.Trim() ?? " ")),
                              }).Distinct().OrderBy(l => l.Text).ToList();
                    return ss;
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.GuardianTypes
                        where (p.ActivationStatus == StringLiteralValue.Active)
                        orderby p.NameOfGuardianType
                        select new SelectListItem
                        {
                            Value = p.GuardianTypeId.ToString(),
                            Text = p.NameOfGuardianType.Trim(),
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> GovernmentPersonDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                byte governmentPersonTypePrmKey = GetGovernmentPersonTypePrmKey();

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var d = (from p in context.People
                             join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                             from mf in pm.DefaultIfEmpty()
                             join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                             from t in pt.DefaultIfEmpty()
                             join a in context.PersonAdditionalDetails.Where(a => a.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals a.PersonPrmKey into pa
                             from a in pa.DefaultIfEmpty()
                             where (p.EntryStatus == StringLiteralValue.Verify)
                                    && (p.ActivationStatus == StringLiteralValue.Active)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                    && (a.PersonCategoryPrmKey == governmentPersonTypePrmKey)
                             orderby p.FullName
                             select new SelectListItem
                             {
                                 Value = p.PersonInformationNumber.ToString(),
                                 Text = ((mf.FullName ==null) ? p.FullName.Trim() + " ---> " + (t.TransFullName.Trim() ?? " " ) : mf.FullName + " ---> " + (t.TransFullName.Trim() ?? " " ))
                             }).Distinct().OrderBy(l => l.Text).ToList();

                    return d;
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        join a in context.PersonAdditionalDetails.Where(a => a.PersonTypePrmKey == governmentPersonTypePrmKey) on p.PrmKey equals a.PersonPrmKey into pa
                        from a in pa.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                                && (p.ActivationStatus == StringLiteralValue.Active)
                                && (a.PersonTypePrmKey == governmentPersonTypePrmKey)
                        orderby p.FirstName
                        select new SelectListItem
                        {
                            Value = p.PersonInformationNumber.ToString(),
                            Text = ((mf.FullName) ?? p.FullName.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> IdentificationDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from a in context.Identifications
                            join at in context.IdentificationTranslations on a.PrmKey equals at.IdentificationPrmKey
                            where a.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = a.IdentificationId.ToString(),
                                Text = a.NameOfIdentification.Trim() + " --> " + at.TransNameOfIdentification.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from a in context.Identifications
                        where a.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = a.IdentificationId.ToString(),
                            Text = a.NameOfIdentification
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PersonInformationParameterDropdownList
        {
            get
            {
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    // Get All Valid Selectlist From ByLawsHeaders            
                    return (from d in context.NoticeTypes
                            join t in context.NoticeTypeTranslations on d.PrmKey equals t.NoticeTypePrmKey into dt
                            from t in dt.DefaultIfEmpty()
                            join dd in context.PersonInformationParameterNoticeTypes on d.PrmKey equals dd.NoticeTypePrmKey into personinformationNoticeType
                            from dd in personinformationNoticeType.DefaultIfEmpty()
                            where (d.ActivationStatus == StringLiteralValue.Active)
                                     && (dd.EntryStatus == StringLiteralValue.Verify)
                                     && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                     || (d.ActivationStatus == StringLiteralValue.Active)
                                     && (dd.EntryStatus == StringLiteralValue.Verify)
                                     && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby d.NameOfNoticeType
                            select new SelectListItem
                            {
                                Value = d.NoticeTypeId.ToString(),
                                Text = ((d.NameOfNoticeType.Trim() + " ---> " + t.TransNameOfNoticeType))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from d in context.NoticeTypes
                        where (d.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = d.NoticeTypeId.ToString(),
                            Text = ((d.NameOfNoticeType))
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> IncomeSourceDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from b in context.IncomeSources
                            join t in context.IncomeSourceTranslations on b.PrmKey equals t.IncomeSourcePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (b.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = b.IncomeSourceId.ToString(),
                                Text = (b.NameOfIncomeSource.Trim() + " ---> " + ((t.TransNameOfIncomeSource.Trim()) ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from b in context.IncomeSources
                        where (b.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = b.IncomeSourceId.ToString(),
                            Text = (b.NameOfIncomeSource.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> InsuranceCompanyDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from v in context.InsuranceCompanies
                            join t in context.InsuranceCompanyTranslations on v.PrmKey equals t.InsuranceCompanyPrmKey into vt
                            from t in vt.DefaultIfEmpty()
                            where (v.ActivationStatus == StringLiteralValue.Active
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            orderby v.NameOfInsuranceCompany
                            select new SelectListItem
                            {
                                Value = v.InsuranceCompanyId.ToString(),
                                Text = (v.NameOfInsuranceCompany.Trim() + " ---> " + (t.TransNameOfInsuranceCompany.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from v in context.InsuranceCompanies
                        where (v.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = v.InsuranceCompanyId.ToString(),
                            Text = (v.NameOfInsuranceCompany.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> InsuranceTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from v in context.InsuranceTypes
                            join t in context.InsuranceTypeTranslations on v.PrmKey equals t.InsuranceTypePrmKey into vt
                            from t in vt.DefaultIfEmpty()
                            where ((v.ActivationStatus == StringLiteralValue.Active)
                                    || (v.ActivationStatus == StringLiteralValue.Active)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            orderby v.NameOfInsuranceType
                            select new SelectListItem
                            {
                                Value = v.InsuranceTypeId.ToString(),
                                Text = (v.NameOfInsuranceType.Trim() + " ---> " + (t.TransNameOfInsuranceType.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from v in context.InsuranceTypes
                        where (v.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = v.InsuranceTypeId.ToString(),
                            Text = (v.NameOfInsuranceType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> JewelAssayerDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                byte governmentPersonTypePrmKey = GetGovernmentPersonTypePrmKey();

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from j in context.JewelAssayers 
                                join p in context.People .Where(p => p.ActivationStatus == StringLiteralValue.Active && p.EntryStatus == StringLiteralValue.Verify) on j.PersonPrmKey equals p.PrmKey into jp
                                from p in jp.DefaultIfEmpty()
                                join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                                from mf in pm.DefaultIfEmpty()
                                join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                                from t in pt.DefaultIfEmpty()                               
                                where (j.EntryStatus == StringLiteralValue.Verify)
                                    && (j.ActivationStatus == StringLiteralValue.Active)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                             orderby p.FullName
                             select new SelectListItem
                             {
                                 Value = j.JewelAssayerId.ToString(),
                                 Text = ((mf.FullName ==null) ? p.FullName.Trim() + " ---> " + (t.TransFullName.Trim() ?? " " ) : mf.FullName + " ---> " + (t.TransFullName.Trim() ?? " "))
                             }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from j in context.JewelAssayers
                        join p in context.People.Where(p => p.ActivationStatus == StringLiteralValue.Active && p.EntryStatus == StringLiteralValue.Verify) on j.PersonPrmKey equals p.PrmKey into jp
                        from p in jp.DefaultIfEmpty()
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        where (j.EntryStatus == StringLiteralValue.Verify)
                            && (j.ActivationStatus == StringLiteralValue.Active)
                        orderby p.FullName
                        select new SelectListItem
                        {
                            Value = j.JewelAssayerId.ToString(),
                            Text = ((mf.FullName.Trim()) ?? p.FullName.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> LocalGovernmentDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from a in context.LocalGovernments
                            join at in context.LocalGovernmentTranslations on a.PrmKey equals at.LocalGovernmentPrmKey
                            where a.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = a.LocalGovernmentId.ToString(),
                                Text = a.NameOfLocalGovernment.Trim() + " --> " + at.TransNameOfLocalGovernment.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from a in context.LocalGovernments
                        where a.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = a.LocalGovernmentId.ToString(),
                            Text = a.NameOfLocalGovernment
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> MaritalStatusDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from m in context.MaritalStatuses
                            join t in context.MaritalStatusTranslations on m.PrmKey equals t.MaritalStatusPrmKey
                            where m.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = m.MaritalStatusId.ToString(),
                                Text = m.NameOfMaritalStatus.Trim() + " --> " + t.TransNameOfMaritalStatus.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from m in context.MaritalStatuses
                        where m.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = m.MaritalStatusId.ToString(),
                            Text = m.NameOfMaritalStatus
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> NatureOfEmployerDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from n in context.EmployerNatures
                            join t in context.EmployerNatureTranslations on n.PrmKey equals t.EmployerNaturePrmKey
                            where n.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = n.EmployerNatureId.ToString(),
                                Text = n.NameOfEmployerNature.Trim() + " --> " + t.TransNameOfEmployerNature.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from n in context.EmployerNatures
                        where n.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = n.EmployerNatureId.ToString(),
                            Text = n.NameOfEmployerNature
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> OccupationDropdownList
        {
            get
            {// Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from o in context.Occupations
                            join t in context.OccupationTranslations on o.PrmKey equals t.OccupationPrmKey into ot
                            from t in ot.DefaultIfEmpty()
                            where (o.ActivationStatus == StringLiteralValue.Active
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = o.OccupationId.ToString(),
                                Text = (o.NameOfOccupation.Trim() + " ---> " + (t.TransNameOfOccupation.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from o in context.Occupations
                        where (o.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = o.OccupationId.ToString(),
                            Text = (o.NameOfOccupation.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> OwnershipTypeDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.OwnershipTypes
                            join t in context.OwnershipTypeTranslations on r.PrmKey equals t.OwnershipTypePrmKey
                            where r.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = r.OwnershipTypeId.ToString(),
                                Text = r.NameOfOwnershipType.Trim() + " --> " + t.TransNameOfOwnershipType.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from r in context.OwnershipTypes
                        where r.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = r.OwnershipTypeId.ToString(),
                            Text = r.NameOfOwnershipType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> ParentOccupationDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from o in context.Occupations
                            join t in context.OccupationTranslations on o.PrmKey equals t.OccupationPrmKey
                            where (o.ActivationStatus == StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = o.OccupationId.ToString(),
                                Text = o.NameOfOccupation.Trim() + " --> " + t.TransNameOfOccupation.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from o in context.Occupations
                        where (o.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = o.OccupationId.ToString(),
                            Text = o.NameOfOccupation
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PostalOfficeDropdownListByTalukaId(Guid _talukaId)
        {
            short talukaPrmKey = GetCenterPrmKeyById(_talukaId);

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                        from t in ct.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey ==2 || c.CenterCategoryPrmKey == 3)
                                && (c.EntryStatus == StringLiteralValue.Verify)
                                && (c.ActivationStatus == StringLiteralValue.Active)
                                && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                && (c.ParentCenterPrmKey == talukaPrmKey)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = (mf.NameOfCenter ?? c.NameOfCenter.Trim()) + " ---> " + (t.TransNameOfCenter.Trim() ?? " ")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            // Default List In Default Language (i.e. English)
            return (from c in context.Centers
                    join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                    from mf in cm.DefaultIfEmpty()
                    where (c.CenterCategoryPrmKey ==2 || c.CenterCategoryPrmKey ==3)
                            && (c.EntryStatus == StringLiteralValue.Verify)
                            && (c.ActivationStatus == StringLiteralValue.Active)
                            && (mf.EntryStatus == StringLiteralValue.Verify || mf.EntryStatus == null)
                            && (c.ParentCenterPrmKey == talukaPrmKey)
                    orderby c.NameOfCenter
                    select new SelectListItem
                    {
                        Value = c.CenterId.ToString(),
                        Text = ((mf.NameOfCenter.Trim()) ?? c.NameOfCenter.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> PersonDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from p in context.People
                            join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                            from mf in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                            && (p.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = (mf.FullName ?? p.FullName.Trim()) + " ---> " + (t.TransFullName.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                                && (p.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = p.PersonId.ToString(),
                            Text = ((mf.FullName) ?? p.FullName.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PersonDropdownListByUserType(Guid _userTypeId)
        {

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            string _sysNameOfUserType = GetNameOfUserTypeById(_userTypeId);
            byte personCategoryPrmKey = GetPersonCategoryPrmKeyBySysName(_sysNameOfUserType);

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                        from t in pt.DefaultIfEmpty()
                        join s in context.PersonAdditionalDetails on p.PrmKey equals s.PersonPrmKey into pa
                        from s in pa.DefaultIfEmpty()
                        where (s.PersonCategoryPrmKey ==personCategoryPrmKey)
                        && (p.ActivationStatus == StringLiteralValue.Active)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        && (p.EntryStatus == StringLiteralValue.Verify)
                        select new SelectListItem
                        {
                            Value = p.PersonId.ToString(),
                            Text = (mf.FullName ?? p.FullName.Trim()) + " ---> " + (t.TransFullName.Trim() ?? " ")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            // Default List In Default Language (i.e. English)
            return (from p in context.People
                    join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                    from mf in pm.DefaultIfEmpty()
                    join s in context.PersonAdditionalDetails on p.PrmKey equals s.PersonPrmKey into pa
                    from s in pa.DefaultIfEmpty()
                    where (p.EntryStatus == StringLiteralValue.Verify)
                            && (p.ActivationStatus == StringLiteralValue.Active)
                            && (s.PersonCategoryPrmKey == personCategoryPrmKey)
                    select new SelectListItem
                    {
                        Value = p.PersonId.ToString(),
                        Text = ((mf.FullName) ?? p.FullName.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();

        }

        public List<SelectListItem> PersonDocumentTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return  (from p in context.PersonInformationParameterDocumentTypes
                                join d in context.DocumentTypes .Where(d => d.ActivationStatus == StringLiteralValue.Active) on p.DocumentTypePrmKey equals d.PrmKey into pd
                                from d in pd.DefaultIfEmpty()
                                join t in context.DocumentTypeTranslations on d.PrmKey equals t.DocumentTypePrmKey into dt
                                from t in dt.DefaultIfEmpty()
                                where (p.EntryStatus == StringLiteralValue.Verify)
                                &&    (t.LanguagePrmKey == regionalLanguagePrmKey)
                                select new SelectListItem
                                {
                                    Value = d.DocumentTypeId.ToString(),
                                    Text = (d.NameOfDocumentType.Trim() + " ---> " + (t.TransNameOfDocumentType.Trim() ?? " "))
                                }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.PersonInformationParameterDocumentTypes
                        join d in context.DocumentTypes.Where(d => d.ActivationStatus == StringLiteralValue.Active) on p.DocumentTypePrmKey equals d.PrmKey into pd
                        from d in pd.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                        select new SelectListItem
                        {
                            Value = d.DocumentTypeId.ToString(),
                            Text = d.NameOfDocumentType.Trim(),
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> PersonInfoNumbersDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from p in context.People
                            join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                            from mf in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                                    && (p.ActivationStatus == StringLiteralValue.Active)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby p.FirstName
                            select new SelectListItem
                            {
                                Value = p.PersonInformationNumber.ToString(),
                                Text = ((mf.FullName ==null) ? p.FullName.Trim() + " ---> " + (t.TransFullName.Trim() ?? " " ) : mf.FullName + " ---> " + (t.TransFullName.Trim() ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                                && (p.ActivationStatus == StringLiteralValue.Active)
                        orderby p.FirstName
                        select new SelectListItem
                        {
                            Value = p.PersonInformationNumber.ToString(),
                            Text = ((mf.FullName) ?? p.FullName.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PersonInfoNumbersAgeAbove18DropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var d = (from p in context.People
                             join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                             from mf in pm.DefaultIfEmpty()
                             join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                             from t in pt.DefaultIfEmpty()

                                 // Age Calculation Above 18 Years
                                 //let today = DateTime.Today
                             let age = DateTime.Today.Year - p.DateOfBirth.Year
                             let aboveDateOfBirth = age >= 18

                             where (p.EntryStatus == StringLiteralValue.Verify)
                                     && (p.ActivationStatus == StringLiteralValue.Active)
                                     && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                     && (aboveDateOfBirth)
                             orderby p.FirstName

                             select new SelectListItem
                             {
                                 Value = p.PersonInformationNumber.ToString(),
                                 Text = ((mf.FullName ==null) ? p.FullName.Trim() + " ---> " + (t.TransFullName.Trim() ?? " " ) : mf.FullName + " ---> " + (t.TransFullName.Trim() ?? " " ))
                             }).Distinct().OrderBy(l => l.Text).ToList();

                    return d;
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                                && (p.ActivationStatus == StringLiteralValue.Active)
                        orderby p.FirstName
                        select new SelectListItem
                        {
                            Value = p.PersonInformationNumber.ToString(),
                            Text = ((mf.FullName) ?? p.FullName.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PersonMaleDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from p in context.People
                            join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                            from mf in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            join a in context.PersonAdditionalDetails.Where(a => a.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals a.PersonPrmKey into pa
                            from a in pa.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                            && (p.ActivationStatus == StringLiteralValue.Active)
                            && (a.GenderPrmKey == 1)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby p.FirstName
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = ((mf.FullName ==null) ? p.FullName.Trim() + " ---> " + (t.TransFullName.Trim() ?? " " ) : mf.FullName + " ---> " + (t.TransFullName.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        join a in context.PersonAdditionalDetails.Where(a => a.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals a.PersonPrmKey into pa
                        from a in pa.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                        && (p.ActivationStatus == StringLiteralValue.Active)
                        && (a.GenderPrmKey == 1)
                        orderby p.FirstName
                        select new SelectListItem
                        {
                            Value = p.PersonId.ToString(),
                            Text = ((mf.FullName) ?? p.FullName.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PersonFemaleDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from p in context.People
                            join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                            from mf in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            join a in context.PersonAdditionalDetails.Where(a => a.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals a.PersonPrmKey into pa
                            from a in pa.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                            && (p.ActivationStatus == StringLiteralValue.Active)
                            && (a.GenderPrmKey == 2)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby p.FirstName
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = ((mf.FullName ==null) ? p.FullName.Trim() + " ---> " + (t.TransFullName.Trim() ?? " " ) : mf.FullName + " ---> " + (t.TransFullName.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        join a in context.PersonAdditionalDetails.Where(a => a.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals a.PersonPrmKey into pa
                        from a in pa.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                        && (p.ActivationStatus == StringLiteralValue.Active)
                        && (a.GenderPrmKey == 2)
                        orderby p.FirstName
                        select new SelectListItem
                        {
                            Value = p.PersonId.ToString(),
                            Text = ((mf.FullName) ?? p.FullName.Trim() )
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PersonMemberDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from p in context.People
                            join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                            from mf in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            join s in context.PersonStatuses.Where(s => s.MemberTypePrmKey == 1 || s.MemberTypePrmKey == 3) on p.PrmKey equals s.PersonPrmKey into ps
                            from s in ps.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                                    && (p.ActivationStatus == StringLiteralValue.Active)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby p.FirstName
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = ((mf.FullName ==null) ? p.FullName.Trim() + " ---> " + (t.TransFullName.Trim() ?? " " ) : mf.FullName + " ---> " + (t.TransFullName.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        join s in context.PersonStatuses.Where(s => s.MemberTypePrmKey == 1 || s.MemberTypePrmKey == 3) on p.PrmKey equals s.PersonPrmKey into ps
                        from s in ps.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                                && (p.ActivationStatus == StringLiteralValue.Active)
                        orderby p.FirstName
                        select new SelectListItem
                        {
                            Value = p.PersonId.ToString(),
                            Text = ((mf.FullName) ?? p.FullName.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> NonCustomerPersonMemberDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                List<long> nonTermDepositHolderPersonPrmKey = (from c in context.CustomerAccountDetails
                                                               join a in context.CustomerAccounts .Where(a=>a.ActivationStatus ==StringLiteralValue.Active && a.EntryStatus == StringLiteralValue.Verify) on c.CustomerAccountPrmKey equals a.PrmKey into ca
                                                               from a in ca.DefaultIfEmpty()
                                                               join s in context.SchemeDemandDepositDetails.Where(s => s.EntryStatus == StringLiteralValue.Verify) on c.SchemePrmKey equals s.SchemePrmKey into cs
                                                               from s in cs.DefaultIfEmpty()
                                                               where(c.EntryStatus == StringLiteralValue.Verify)
                                                               select c.PersonPrmKey).Distinct().ToList();

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from p in context.People
                            join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                            from mf in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            join s in context.PersonStatuses.Where(s => s.MemberTypePrmKey == 1 || s.MemberTypePrmKey == 3) on p.PrmKey equals s.PersonPrmKey into ps
                            from s in ps.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                                    && (!nonTermDepositHolderPersonPrmKey.Contains(p.PrmKey))
                                    && (p.ActivationStatus == StringLiteralValue.Active)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby p.FirstName
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = ((mf.FullName ==null) ? p.FullName.Trim() + " ---> " + (t.TransFullName.Trim() ?? " " ) : mf.FullName + " ---> " + (t.TransFullName.Trim() ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        join s in context.PersonStatuses.Where(s => s.MemberTypePrmKey == 1 || s.MemberTypePrmKey == 3) on p.PrmKey equals s.PersonPrmKey into ps
                        from s in ps.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                                && (!nonTermDepositHolderPersonPrmKey.Contains(p.PrmKey))
                                && (p.ActivationStatus == StringLiteralValue.Active)
                        orderby p.FirstName
                        select new SelectListItem
                        {
                            Value = p.PersonId.ToString(),
                            Text = ((mf.FullName) ?? p.FullName.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> NonMemberPersonDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                List<long> memberPersonPrmKey = (from c in context.CustomerAccountDetails
                                                               join a in context.CustomerAccounts.Where(a => a.ActivationStatus != StringLiteralValue.Closed && a.EntryStatus == StringLiteralValue.Verify) on c.CustomerAccountPrmKey equals a.PrmKey into ca
                                                               from a in ca.DefaultIfEmpty()
                                                               join s in context.CustomerSharesCapitalAccounts.Where(s => s.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals s.CustomerAccountPrmKey into cs
                                                               from s in cs.DefaultIfEmpty()
                                                               where (c.EntryStatus == StringLiteralValue.Verify)
                                                               select c.PersonPrmKey).Distinct().ToList();

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from p in context.People
                            join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                            from mf in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                                    && (!memberPersonPrmKey.Contains(p.PrmKey))
                                    && (p.ActivationStatus == StringLiteralValue.Active)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby p.FirstName
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = (mf.FullName ?? p.FullName.Trim()) + " ---> " + (t.TransFullName.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        join s in context.PersonStatuses.Where(s => s.MemberTypePrmKey == 1 || s.MemberTypePrmKey == 3) on p.PrmKey equals s.PersonPrmKey into ps
                        from s in ps.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                                && (!memberPersonPrmKey.Contains(p.PrmKey))
                                && (p.ActivationStatus == StringLiteralValue.Active)
                        orderby p.FirstName
                        select new SelectListItem
                        {
                            Value = p.PersonId.ToString(),
                            Text = ((mf.FullName) ?? p.FullName.Trim() )
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> NonDemandDepositorPersonDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                List<long> demandDepositorPersonPrmKey = (from s in context.SchemeDepositAccountParameters 
                                                          join c in context.CustomerAccountDetails .Where(c => c.EntryStatus == StringLiteralValue.Verify) on s.SchemePrmKey equals c.SchemePrmKey into sc
                                                          from c in sc.DefaultIfEmpty()
                                                          join a in context.CustomerAccounts.Where(a => a.ActivationStatus != StringLiteralValue.Closed && a.EntryStatus != StringLiteralValue.Delete) on c.CustomerAccountPrmKey equals a.PrmKey into ca
                                                          from a in ca.DefaultIfEmpty()                                                          
                                                          where (s.DepositType == StringLiteralValue.DemandDeposit && s.EntryStatus == StringLiteralValue.Verify)
                                                          select c.PersonPrmKey == null ? 0 : c.PersonPrmKey).Distinct().ToList();

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from p in context.People
                            join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                            from mf in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                                    && (!demandDepositorPersonPrmKey.Contains(p.PrmKey))
                                    && (p.ActivationStatus == StringLiteralValue.Active)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby p.FirstName
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = (mf.FullName ?? p.FullName.Trim()) + " ---> " + (t.TransFullName.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        join s in context.PersonStatuses.Where(s => s.MemberTypePrmKey == 1 || s.MemberTypePrmKey == 3) on p.PrmKey equals s.PersonPrmKey into ps
                        from s in ps.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                                && (!demandDepositorPersonPrmKey.Contains(p.PrmKey))
                                && (p.ActivationStatus == StringLiteralValue.Active)
                        orderby p.FirstName
                        select new SelectListItem
                        {
                            Value = p.PersonId.ToString(),
                            Text = ((mf.FullName) ?? p.FullName.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PersonMaleMemberDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from p in context.People
                            join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                            from mf in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            join s in context.PersonStatuses on p.PrmKey equals s.PersonPrmKey into ps
                            from s in ps.DefaultIfEmpty()
                            join a in context.PersonAdditionalDetails .Where(a=> a.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals a.PersonPrmKey into pa
                            from a in pa.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                            &&    (p.ActivationStatus == StringLiteralValue.Active)
                            &&    (s.MemberTypePrmKey == 1 || s.MemberTypePrmKey == 2)
                            &&    (a.GenderPrmKey == 1)
                            &&    (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby p.FirstName
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = ((mf.FullName ==null) ? p.FullName.Trim() + " ---> " + (t.TransFullName.Trim() ?? " " ) : mf.FullName + " ---> " + (t.TransFullName.Trim() ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        join s in context.PersonStatuses on p.PrmKey equals s.PersonPrmKey into ps
                        from s in ps.DefaultIfEmpty()
                        join a in context.PersonAdditionalDetails.Where(a => a.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals a.PersonPrmKey into pa
                        from a in pa.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                        &&    (p.ActivationStatus == StringLiteralValue.Active)
                        &&    (s.MemberTypePrmKey == 1 || s.MemberTypePrmKey == 2)
                        &&    (a.GenderPrmKey == 1)
                        orderby p.FirstName
                        select new SelectListItem
                        {
                            Value = p.PersonId.ToString(),
                            Text = ((mf.FullName) ?? p.FullName.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PersonFemaleMemberDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from p in context.People
                            join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                            from mf in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            join s in context.PersonStatuses on p.PrmKey equals s.PersonPrmKey into ps
                            from s in ps.DefaultIfEmpty()
                            join a in context.PersonAdditionalDetails.Where(a => a.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals a.PersonPrmKey into pa
                            from a in pa.DefaultIfEmpty()
                            where (p.EntryStatus == StringLiteralValue.Verify)
                            && (p.ActivationStatus == StringLiteralValue.Active)
                            && (s.MemberTypePrmKey == 1 || s.MemberTypePrmKey == 2)
                            && (a.GenderPrmKey == 2)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby p.FirstName
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = ((mf.FullName ==null) ? p.FullName.Trim() + " ---> " + (t.TransFullName.Trim() ?? " " ) : mf.FullName + " ---> " + (t.TransFullName.Trim() ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from p in context.People
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        join s in context.PersonStatuses on p.PrmKey equals s.PersonPrmKey into ps
                        from s in ps.DefaultIfEmpty()
                        join a in context.PersonAdditionalDetails.Where(a => a.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals a.PersonPrmKey into pa
                        from a in pa.DefaultIfEmpty()
                        where (p.EntryStatus == StringLiteralValue.Verify)
                        && (p.ActivationStatus == StringLiteralValue.Active)
                        && (s.MemberTypePrmKey == 1 || s.MemberTypePrmKey == 2)
                        && (a.GenderPrmKey == 2)
                        orderby p.FirstName
                        select new SelectListItem
                        {
                            Value = p.PersonId.ToString(),
                            Text = ((mf.FullName) ?? p.FullName.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PersonCategoryDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.PersonCategories
                            join t in context.PersonCategoryTranslations on r.PrmKey equals t.PersonCategoryPrmKey
                            where r.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = r.PersonCategoryId.ToString(),
                                Text = r.NameOfPersonCategory.Trim() + " --> " + t.TransNameOfPersonCategory.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from r in context.PersonCategories
                        where r.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = r.PersonCategoryId.ToString(),
                            Text = r.NameOfPersonCategory
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PersonTypeDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.PersonTypes
                            join t in context.PersonTypeTranslations on r.PrmKey equals t.PersonTypePrmKey
                            where r.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = r.PersonTypeId.ToString(),
                                Text = r.NameOfPersonType.Trim() + " --> " + t.TransNameOfPersonType.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from r in context.PersonTypes
                        where r.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = r.PersonTypeId.ToString(),
                            Text = r.NameOfPersonType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PhysicalStatusDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from p in context.PhysicalStatuses
                            join t in context.PhysicalStatusTranslations on p.PrmKey equals t.PhysicalStatusPrmKey
                            where p.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = p.PhysicalStatusId.ToString(),
                                Text = p.NameOfPhysicalStatus.Trim() + " --> " + t.TransNameOfPhysicalStatus.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from p in context.PhysicalStatuses
                        where p.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = p.PhysicalStatusId.ToString(),
                            Text = p.NameOfPhysicalStatus
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PovertyStatusDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from p in context.PovertyStatuses
                            join t in context.PovertyStatusTranslations on p.PrmKey equals t.PovertyStatusPrmKey
                            where p.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = p.PovertyStatusId.ToString(),
                                Text = p.NameOfPovertyStatus.Trim() + " --> " + t.TransNameOfPovertyStatus.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from p in context.PovertyStatuses
                        where p.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = p.PovertyStatusId.ToString(),
                            Text = p.NameOfPovertyStatus
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PrefixDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from p in context.Prefixes
                            join t in context.PrefixTranslations on p.PrmKey equals t.PrefixPrmKey
                            where p.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = p.PrefixId.ToString(),
                                Text = p.NameOfPrefix.Trim() + " --> " + t.TransNameOfPrefix.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from p in context.Prefixes
                        where p.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = p.PrefixId.ToString(),
                            Text = p.NameOfPrefix
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> RelationDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.Relations
                            join t in context.RelationTranslations on r.PrmKey equals t.RelationPrmKey
                            where r.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = r.RelationId.ToString(),
                                Text = r.NameOfRelation.Trim() + " --> " + t.TransNameOfRelation.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from r in context.Relations
                        where r.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = r.RelationId.ToString(),
                            Text = r.NameOfRelation
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> ReligionDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.Religions
                            join rt in context.ReligionTranslations on r.PrmKey equals rt.ReligionPrmKey
                            select new SelectListItem
                            {
                                Value = r.ReligionId.ToString(),
                                Text = r.NameOfReligion.Trim() + " --> " + rt.TransNameOfReligion.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from e in context.Religions

                        select new SelectListItem
                        {
                            Value = e.ReligionId.ToString(),
                            Text = e.NameOfReligion
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> ReservationCategoryDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.ReservationCategories
                            join rt in context.ReservationCategoryTranslations on r.PrmKey equals rt.ReservationCategoryPrmKey
                            select new SelectListItem
                            {
                                Value = r.ReservationCategoryId.ToString(),
                                Text = r.NameOfReservationCategory.Trim() + " --> " + rt.TransNameOfReservationCategory.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                var s = (from e in context.ReservationCategories

                         select new SelectListItem
                         {
                             Value = e.ReservationCategoryId.ToString(),
                             Text = e.NameOfReservationCategory
                         }).Distinct().OrderBy(l => l.Text).ToList();
                return s;
            }
        }

        public List<SelectListItem> ResidenceTypeDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.ResidenceTypes
                            join t in context.ResidenceTypeTranslations on r.PrmKey equals t.ResidenceTypePrmKey
                            where r.ActivationStatus == StringLiteralValue.Active
                            select new SelectListItem
                            {
                                Value = r.ResidenceTypeId.ToString(),
                                Text = r.NameOfResidenceType.Trim() + " --> " + t.TransNameOfResidenceType.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from r in context.ResidenceTypes
                        where r.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = r.ResidenceTypeId.ToString(),
                            Text = r.NameOfResidenceType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SubContinentDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey == 11)   // 11 Is SubContinent CenterCategory
                            && (c.EntryStatus == StringLiteralValue.Verify)
                            && (c.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = ((mf.NameOfCenter ==null) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Trim() ?? " " ) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Trim() ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey == 11)   // 11 Is SubContinent CenterCategory
                        && (c.EntryStatus == StringLiteralValue.Verify)
                        && (c.ActivationStatus == StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SubDivisionOfficeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey == 5)    // 5 Is SubDivisionOffice CenterCategory
                            && (c.EntryStatus == StringLiteralValue.Verify)
                            && (c.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = (mf.NameOfCenter ?? c.NameOfCenter.Trim()) + " ---> " + (t.TransNameOfCenter.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey == 5)    // 5 Is SubDivisionOffice CenterCategory
                        && (c.EntryStatus == StringLiteralValue.Verify)
                        && (c.ActivationStatus == StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SubDivisionOfficeDropdownListByDistrictId(Guid _districtId)
        {
            short districtPrmKey = GetCenterPrmKeyById(_districtId);

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                        from t in ct.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey == 5)   // 5 Is SubDivision CenterCategory 
                        && (c.EntryStatus == StringLiteralValue.Verify)
                        && (c.ActivationStatus == StringLiteralValue.Active)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        && (c.ParentCenterPrmKey == districtPrmKey)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = (mf.NameOfCenter ?? c.NameOfCenter.Trim()) + " ---> " + (t.TransNameOfCenter.Trim() ?? " ")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            // Default List In Default Language (i.e. English)
            return (from c in context.Centers
                    join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                    from mf in cm.DefaultIfEmpty()
                    join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                    from t in ct.DefaultIfEmpty()
                    where (c.CenterCategoryPrmKey ==5)   // 5 Is SubDivision CenterCategory 
                    && (c.ParentCenterPrmKey == districtPrmKey)
                    && (c.EntryStatus == StringLiteralValue.Verify)
                    && (c.ActivationStatus == StringLiteralValue.Active)
                    orderby c.NameOfCenter
                    select new SelectListItem
                    {
                        Value = c.CenterId.ToString(),
                        Text = ((mf.NameOfCenter) ?? c.NameOfCenter.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> StateDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey == 8)    // 8 Is State CenterCategory 
                            && (c.EntryStatus == StringLiteralValue.Verify)
                            && (c.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = (mf.NameOfCenter ?? c.NameOfCenter.Trim()) + " ---> " + (t.TransNameOfCenter.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey == 8)    // 8 Is State CenterCategory 
                        && (c.EntryStatus == StringLiteralValue.Verify)
                        && (c.ActivationStatus == StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter.Trim()) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> StateDropdownListByCountryId(Guid _countryId)
        {
            short countryPrmKey = GetCenterPrmKeyById(_countryId);

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                        from t in ct.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey == 8)   // 8 Is State CenterCategory 
                        && (c.ParentCenterPrmKey == countryPrmKey)
                        && (c.EntryStatus == StringLiteralValue.Verify)
                        && (c.ActivationStatus == StringLiteralValue.Active)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = (mf.NameOfCenter ?? c.NameOfCenter.Trim()) + " ---> " + (t.TransNameOfCenter.Trim() ?? " ")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            // Default List In Default Language (i.e. English)
            return (from c in context.Centers
                    join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                    from mf in cm.DefaultIfEmpty()
                    where (c.CenterCategoryPrmKey ==8)   // 8 Is State CenterCategory 
                    && (c.ParentCenterPrmKey == countryPrmKey)
                    && (c.EntryStatus == StringLiteralValue.Verify)
                    && (c.ActivationStatus == StringLiteralValue.Active)
                    orderby c.NameOfCenter
                    select new SelectListItem
                    {
                        Value = c.CenterId.ToString(),
                        Text = ((mf.NameOfCenter.Trim()) ?? c.NameOfCenter.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> UnionTerritoriesDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey == 9)   // 9 Is UnionTerritories CenterCategory 
                            && (c.EntryStatus == StringLiteralValue.Verify)
                            && (c.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = (mf.NameOfCenter ?? c.NameOfCenter.Trim()) + " ---> " + (t.TransNameOfCenter.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey ==9)    // 9 Is UnionTerritories CenterCategory 
                        && (c.EntryStatus == StringLiteralValue.Verify)
                        && (c.ActivationStatus == StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> StateUnionTerritoriesDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey == 8) || (c.CenterCategoryPrmKey ==9)
                            && (c.EntryStatus == StringLiteralValue.Verify)
                            && (c.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = ((mf.NameOfCenter ==null) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Trim() ?? " " ) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from c in context.Centers
                        join mf in context.CenterModifications on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey == 8) || (c.CenterCategoryPrmKey ==9 )
                        && (c.EntryStatus == StringLiteralValue.Verify)
                        && (c.ActivationStatus == StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter.Trim()) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> TalukaDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey == 4)    // 4 Is Taluka CenterCategory
                            && (c.EntryStatus == StringLiteralValue.Verify)
                            && (c.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = ((mf.NameOfCenter ==null) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Trim() ?? " ") : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey == 4)    // 4 Is Taluka CenterCategory
                        && (c.EntryStatus == StringLiteralValue.Verify)
                        && (c.ActivationStatus == StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter.Trim()) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> TalukaDropdownListBySubDivisionOfficeId(Guid _subDivisionOfficeId)
        {
            short subDivisionOfficePrmKey = GetCenterPrmKeyById(_subDivisionOfficeId);

            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                        from t in ct.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey ==4)   // 4 Is Taluka CenterCategory 
                        && (c.ParentCenterPrmKey == subDivisionOfficePrmKey)
                        && (c.EntryStatus == StringLiteralValue.Verify)
                        && (c.ActivationStatus == StringLiteralValue.Active)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter ==null) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Trim() ?? " " ) : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Trim() ?? " "))
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            // Default List In Default Language (i.e. English)
            return (from c in context.Centers
                    join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                    from mf in cm.DefaultIfEmpty()
                    where (c.CenterCategoryPrmKey == 4)   // 4 Is Taluka CenterCategory 
                    && (c.ParentCenterPrmKey == subDivisionOfficePrmKey)
                    && (c.EntryStatus == StringLiteralValue.Verify)
                    && (c.ActivationStatus == StringLiteralValue.Active)
                    orderby c.NameOfCenter
                    select new SelectListItem
                    {
                        Value = c.CenterId.ToString(),
                        Text = ((mf.NameOfCenter.Trim()) ?? c.NameOfCenter.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> TradingEntityDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from te in context.TradingEntities
                            join t in context.TradingEntityTranslations on te.PrmKey equals t.TradingEntityPrmKey into tt
                            from t in tt.DefaultIfEmpty()

                            where (te.ActivationStatus == StringLiteralValue.Active
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = te.TradingEntityId.ToString(),
                                Text = (te.NameOfTradingEntity.Trim() + " ---> " + ((t.TransNameOfTradingEntity.Trim()) ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from te in context.TradingEntities
                        where (te.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = te.TradingEntityId.ToString(),
                            Text = (te.NameOfTradingEntity.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> TownDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey == 2)
                            && (c.EntryStatus == StringLiteralValue.Verify)
                            && (c.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = (mf.NameOfCenter ?? c.NameOfCenter.Trim()) + " ---> " + (t.TransNameOfCenter.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey == 2)
                        && (c.EntryStatus == StringLiteralValue.Verify)
                        && (c.ActivationStatus == StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter.Trim()) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> VillageDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey == 1)
                            && (c.EntryStatus == StringLiteralValue.Verify)
                            && (c.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = (mf.NameOfCenter ?? c.NameOfCenter.Trim()) + " ---> " + (t.TransNameOfCenter.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey == 1)
                        && (c.EntryStatus == StringLiteralValue.Verify)
                        && (c.ActivationStatus == StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> VillageTownCityDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.Centers
                            join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.CenterTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify && t.LanguagePrmKey == regionalLanguagePrmKey) on c.PrmKey equals t.CenterPrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.CenterCategoryPrmKey == 1 || c.CenterCategoryPrmKey ==2 || c.CenterCategoryPrmKey ==3)
                            && (c.EntryStatus == StringLiteralValue.Verify)
                            && (c.ActivationStatus == StringLiteralValue.Active)
                            orderby c.NameOfCenter
                            select new SelectListItem
                            {
                                Value = c.CenterId.ToString(),
                                Text = ((mf.NameOfCenter ==null) ? c.NameOfCenter.Trim() + " ---> " + (t.TransNameOfCenter.Trim() ?? " ") : mf.NameOfCenter + " ---> " + (t.TransNameOfCenter.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.Centers
                        join mf in context.CenterModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on c.PrmKey equals mf.CenterPrmKey into cm
                        from mf in cm.DefaultIfEmpty()
                        where (c.CenterCategoryPrmKey ==1 || c.CenterCategoryPrmKey ==2 || c.CenterCategoryPrmKey ==3)
                                && (c.EntryStatus == StringLiteralValue.Verify)
                                && (c.ActivationStatus == StringLiteralValue.Active)
                        orderby c.NameOfCenter
                        select new SelectListItem
                        {
                            Value = c.CenterId.ToString(),
                            Text = ((mf.NameOfCenter) ?? c.NameOfCenter.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> WorldWideTimeZoneDropdownList
        {
            get
            {
                return (from w in context.WorldWideTimeZones
                        where w.ActivationStatus == StringLiteralValue.Active
                        select new SelectListItem
                        {
                            Value = w.WorldWideTimeZoneId.ToString(),
                            Text = w.NameOfTimeZone
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> GetPersonDropdownListForSharesAccountOpening(Guid _schemeId)
        {
            return NonMemberPersonDropdownList;
        }

        public List<SelectListItem> GetPersonDropdownListForDemandDepositAccountOpening(Guid _schemeId)
        {
            return NonDemandDepositorPersonDropdownList;
        }


        public List<SelectListItem> GetPersonDropdownListForBusinessLoanAccountOpening(Guid _schemeId)
        {
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            IEnumerable<DbQueryDropdownListViewModel> dropdownListViewModel = context.Database.SqlQuery<DbQueryDropdownListViewModel>("SELECT * FROM dbo.GetDropdownListOfPersonForBusinessLoanAccountOpening (@SchemeId, @RegionalLanguagePrmKey)", new SqlParameter("@SchemeId", _schemeId), new SqlParameter("@RegionalLanguagePrmKey", regionalLanguagePrmKey)).Distinct().ToList();

            // Map the results to SelectListItem
            var selectListItems = dropdownListViewModel.Select(p => new SelectListItem
            {
                Value = p.ValueId.ToString(),
                Text = p.ValueText
            }).ToList();

            return selectListItems;
        }

        public List<SelectListItem> GetPersonDropdownListForLoanAccountOpening(Guid _schemeId)
        {
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            IEnumerable<DbQueryDropdownListViewModel> dropdownListViewModel = context.Database.SqlQuery<DbQueryDropdownListViewModel>("SELECT * FROM dbo.GetDropdownListOfPersonForLoanAccountOpening (@SchemeId, @RegionalLanguagePrmKey)", new SqlParameter("@SchemeId", _schemeId), new SqlParameter("@RegionalLanguagePrmKey", regionalLanguagePrmKey)).Distinct().ToList();

            // Map the results to SelectListItem
            var selectListItems = dropdownListViewModel.Select(p => new SelectListItem
            {
                Value = p.ValueId.ToString(),
                Text = p.ValueText
            }).ToList();

            return selectListItems;
        }



        //public List<SelectListItem> NonCustomerPersonDropdownListBySchemeId(Guid _schemeId)
        //{
        //    short schemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_schemeId);


        //    List<SchemeTargetGroupViewModel>  schemeTargetGroupViewModels = context.Database.SqlQuery<SchemeTargetGroupViewModel>("SELECT * FROM dbo.GetSchemeTargetGroupEntriesBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", schemePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).Distinct().ToList();

        //    if (schemeTargetGroupViewModels is null || schemeTargetGroupViewModels.Count() == 0)
        //    {
        //        return PersonDropdownList;
        //    }
        //    else
        //    {
        //        foreach (SchemeTargetGroupViewModel viewModel in schemeTargetGroupViewModels)
        //        {
        //            if (viewModel.TargetGroupPrmKey == 0)
        //            {
        //                if (viewModel.RequiredMembership == "AMB")
        //                {
        //                    return NonCustomerPersonMemberDropdownList;
        //                }
        //            }

        //            return PersonDropdownList;
        //        }

        //        return PersonDropdownList;
        //    }
        //}

        public List<SelectListItem> PersonDropdownListBySchemeId(Guid _schemeId)
        {
            short schemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_schemeId);

            List<SchemeTargetGroupViewModel>  schemeTargetGroupViewModels = context.Database.SqlQuery<SchemeTargetGroupViewModel>("SELECT * FROM dbo.GetSchemeTargetGroupEntriesBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", schemePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).Distinct().ToList();

            if (schemeTargetGroupViewModels is null || schemeTargetGroupViewModels.Count() == 0)
            {
                return PersonDropdownList;
            }
            else
            {
                foreach (SchemeTargetGroupViewModel viewModel in schemeTargetGroupViewModels)
                {
                    if (viewModel.TargetGroupPrmKey == 0)
                    {
                        if (viewModel.RequiredMembership == "AMB")
                        {
                            return PersonMemberDropdownList;
                        }
                    }

                    return PersonDropdownList;
                }

                return PersonDropdownList;
            }
        }

        public List<SelectListItem> GetGuarantorPersonDropdownListBySchemeId(Guid _schemeId)
        {
            short schemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_schemeId);

            string eligibilityForGuarantor = accountDetailRepository.GetEligibilityForGuarantor(_schemeId);

            // Ordinary Member
            if(eligibilityForGuarantor == "ORD")
            {

            }

            // Active Membery
            if (eligibilityForGuarantor == "ACT")
            {

            }

            // Nominal Member
            if (eligibilityForGuarantor == "NOM")
            {

            }

            // Any
            if (eligibilityForGuarantor == "ANY")
            {

            }

            // Depositor
            if (eligibilityForGuarantor == "DEP")
            {

            }

            // Customer
            if (eligibilityForGuarantor == "CST")
            {

            }

            // All
            if (eligibilityForGuarantor == "ALL")
            {

            }


            List<SchemeTargetGroupViewModel>  schemeTargetGroupViewModels = context.Database.SqlQuery<SchemeTargetGroupViewModel>("SELECT * FROM dbo.GetSchemeTargetGroupEntriesBySchemePrmKey (@SchemePrmKey, @EntriesType)", new SqlParameter("@SchemePrmKey", schemePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).Distinct().ToList();

            if (schemeTargetGroupViewModels is null || schemeTargetGroupViewModels.Count() == 0)
            {
                return PersonDropdownList;
            }
            else
            {
                foreach (SchemeTargetGroupViewModel viewModel in schemeTargetGroupViewModels)
                {
                    if (viewModel.TargetGroupPrmKey == 0)
                    {
                        if (viewModel.RequiredMembership == "AMB")
                        {
                            return PersonMemberDropdownList;
                        }
                    }

                    return PersonDropdownList;
                }

                return PersonDropdownList;
            }
        }

        public List<SelectListItem> GetDocumentDropdownListByDocumentTypePrmKey(short _documentTypePrmKey)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {

                return (from dd in context.DocumentDocumentTypes
                        join d in context.Documents.Where(d => d.ActivationStatus == StringLiteralValue.Active) on dd.DocumentPrmKey equals d.PrmKey into ddt
                        from d in ddt.DefaultIfEmpty()
                        join t in context.DocumentTranslations .Where(t=>t.LanguagePrmKey == regionalLanguagePrmKey) on d.PrmKey equals t.DocumentPrmKey into dt
                        from t in dt.DefaultIfEmpty()
                        where (dd.DocumentTypePrmKey == _documentTypePrmKey)
                        && (dd.EntryStatus == StringLiteralValue.Verify)
                        orderby d.NameOfDocument
                        select new SelectListItem
                        {
                            Value = d.DocumentId.ToString(),
                            Text = (d.NameOfDocument.Trim() + " ---> " + (t.TransNameOfDocument ?? " " ))
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            return (from dd in context.DocumentDocumentTypes
                    join d in context.Documents.Where(d => d.ActivationStatus == StringLiteralValue.Active) on dd.DocumentPrmKey equals d.PrmKey into ddt
                    from d in ddt.DefaultIfEmpty()
                    where (dd.DocumentTypePrmKey == _documentTypePrmKey)
                    && (dd.EntryStatus == StringLiteralValue.Verify)
                    orderby d.NameOfDocument
                    select new SelectListItem
                    {
                        Value = d.DocumentId.ToString(),
                        Text = (d.NameOfDocument.Trim())
                    }).Distinct().OrderBy(l => l.Text).ToList();

        }

        public string GetSysNameOfPersonTypeById(Guid _personTypeId)
        {
            return context.PersonTypes
                 .Where(c => c.PersonTypeId == _personTypeId)
                 .Select(c => c.SysNameOfPersonType).FirstOrDefault();
        }
    }
}
