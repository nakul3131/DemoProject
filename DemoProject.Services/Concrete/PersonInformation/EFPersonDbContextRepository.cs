using AutoMapper;
using DemoProject.Domain.Entities.PersonInformation;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.PersonInformation.Parameter
{
    public class EFPersonDbContextRepository : IPersonDbContextRepository
    {
        private readonly EFDbContext context;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IPersonInformationDetailRepository personInformationDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        private Person person = new Person();
        PersonAgricultureAsset personAgricultureAsset = new PersonAgricultureAsset();
        PersonMovableAsset personMovableAsset = new PersonMovableAsset();
        PersonFinancialAsset personFinancialAsset = new PersonFinancialAsset();
        PersonImmovableAsset personImmovableAsset = new PersonImmovableAsset();
        PersonMachineryAsset personMachineryAsset = new PersonMachineryAsset();
        PersonBankDetail personBankDetail = new PersonBankDetail();
        PersonIncomeTaxDetail personIncomeTaxDetail = new PersonIncomeTaxDetail();
        PersonKYCDetail personKYCDetail = new PersonKYCDetail();
        PersonGSTRegistrationDetail personGSTRegistrationDetail = new PersonGSTRegistrationDetail();
        PersonGroup personGroup = new PersonGroup();

        private long personPrmKey = 0;
        private EntityState entityState;
        PersonViewModel personViewModel = new PersonViewModel();

        public EFPersonDbContextRepository(RepositoryConnection _connection, IConfigurationDetailRepository _configurationDetailRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IPersonInformationDetailRepository _personInformationDetailRepository, IPersonDetailRepository _personDetailRepository, IManagementDetailRepository _managementDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, IAccountDetailRepository _accountDetailRepository)
        {
            context = _connection.EFDbContext;
            configurationDetailRepository = _configurationDetailRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            personInformationDetailRepository = _personInformationDetailRepository;
            accountDetailRepository = _accountDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            managementDetailRepository = _managementDetailRepository;
            personDetailRepository = _personDetailRepository;
        }


        //TO STORE IMAGES IN LOCAL STORAGE
        // Create List For Local Storage Path (Which Stored In Database) Of Above Files (i.e. filePathList)
        // It Is Mandatory To Maintain Same Sequence Of filePathList Or localStorageFilePathList To Get Accurate Record.
        // Create List httpPostedFileBaseList For New Uploaded Files

        List<string> filePathList = new List<string>();
        List<string> localStorageFilePathList = new List<string>();
        List<HttpPostedFileBase> httpPostedFileBaseList = new List<HttpPostedFileBase>();

        //TO STORE NEW IMAGES AND DELETE OLD IMAGES IN LOCAL STORAGE
        List<string> oldRecord = new List<string>();
        List<string> localStorageFilePathListForOldRecord = new List<string>();
        List<HttpPostedFileBase> httpPostedFileBaseListForOldRecord = new List<HttpPostedFileBase>();

        public bool AttachPersonData(PersonViewModel _personViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personViewModel, _entryType);

                Person person = Mapper.Map<Person>(_personViewModel);
                PersonMakerChecker personMakerChecker = Mapper.Map<PersonMakerChecker>(_personViewModel);

                PersonTranslation personTranslation = Mapper.Map<PersonTranslation>(_personViewModel);
                PersonTranslationMakerChecker personTranslationMakerChecker = Mapper.Map<PersonTranslationMakerChecker>(_personViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    if (person.FullName == null)
                    {
                        person.FullName = person.FirstName + " " + person.MiddleName + " " + person.LastName;

                        personTranslation.TransFullName = personTranslation.TransFirstName + " " + personTranslation.TransMiddleName + " " + personTranslation.TransLastName;
                    }

                    personPrmKey = _personViewModel.PersonPrmKey;
                    person.PrmKey = personPrmKey;
                    personTranslation.PrmKey = _personViewModel.PersonTranslationPrmKey;
                    if (personTranslation.TransNote == null)
                    {
                        personTranslation.TransNote = "None";
                    }
                    context.People.Attach(person);
                    context.Entry(person).State = entityState;

                    context.PersonMakerCheckers.Attach(personMakerChecker);
                    context.Entry(personMakerChecker).State = EntityState.Added;
                    person.PersonMakerCheckers.Add(personMakerChecker);

                    context.PersonTranslations.Attach(personTranslation);
                    context.Entry(personTranslation).State = entityState;
                    person.PersonTranslations.Add(personTranslation);

                    context.PersonTranslationMakerCheckers.Attach(personTranslationMakerChecker);
                    context.Entry(personTranslationMakerChecker).State = EntityState.Added;
                    personTranslation.PersonTranslationMakerCheckers.Add(personTranslationMakerChecker);
                }
                else
                {
                    context.PersonMakerCheckers.Attach(personMakerChecker);
                    context.Entry(personMakerChecker).State = EntityState.Added;

                    context.PersonTranslationMakerCheckers.Attach(personTranslationMakerChecker);
                    context.Entry(personTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonMasterData(PersonMasterViewModel _personMasterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personMasterViewModel, _entryType);

                PersonModification personModification = Mapper.Map<PersonModification>(_personMasterViewModel);
                PersonModificationMakerChecker personModificationMakerChecker = Mapper.Map<PersonModificationMakerChecker>(_personMasterViewModel);

                PersonTranslation personTranslation = Mapper.Map<PersonTranslation>(_personMasterViewModel);
                PersonTranslationMakerChecker personTranslationMakerChecker = Mapper.Map<PersonTranslationMakerChecker>(_personMasterViewModel);

                person.FullName = person.FirstName + " " + person.MiddleName + " " + person.LastName;

                personTranslation.TransFullName = personTranslation.TransFirstName + " " + personTranslation.TransMiddleName + " " + personTranslation.TransLastName;

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    personPrmKey = _personMasterViewModel.PersonPrmKey;
                    personTranslation.PrmKey = _personMasterViewModel.PersonTranslationPrmKey;

                    if (personTranslation.TransNote == null)
                    {
                        personTranslation.TransNote = "None";
                    }

                    context.PersonModifications.Attach(personModification);
                    context.Entry(personModification).State = entityState;

                    context.PersonModificationMakerCheckers.Attach(personModificationMakerChecker);
                    context.Entry(personModificationMakerChecker).State = EntityState.Added;
                    personModification.PersonModificationMakerCheckers.Add(personModificationMakerChecker);

                    context.PersonTranslations.Attach(personTranslation);
                    context.Entry(personTranslation).State = entityState;
                    person.PersonTranslations.Add(personTranslation);

                    context.PersonTranslationMakerCheckers.Attach(personTranslationMakerChecker);
                    context.Entry(personTranslationMakerChecker).State = EntityState.Added;
                    personTranslation.PersonTranslationMakerCheckers.Add(personTranslationMakerChecker);
                }
                else
                {
                    context.PersonModificationMakerCheckers.Attach(personModificationMakerChecker);
                    context.Entry(personModificationMakerChecker).State = EntityState.Added;

                    context.PersonTranslationMakerCheckers.Attach(personTranslationMakerChecker);
                    context.Entry(personTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonPrefixData(PersonPrefixViewModel _personPrefixViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personPrefixViewModel, _entryType);
                if (_personPrefixViewModel.PrefixPrmKey == 0)
                {
                    _personPrefixViewModel.PrefixPrmKey = personDetailRepository.GetPrefixPrmKeyById(_personPrefixViewModel.PrefixId);
                }
                PersonPrefix personPrefix = Mapper.Map<PersonPrefix>(_personPrefixViewModel);
                PersonPrefixMakerChecker personPrefixMakerChecker = Mapper.Map<PersonPrefixMakerChecker>(_personPrefixViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    personPrefix.PersonPrmKey = personPrmKey;

                    context.PersonPrefixes.Attach(personPrefix);
                    context.Entry(personPrefix).State = entityState;
                    person.PersonPrefixes.Add(personPrefix);

                    context.PersonPrefixMakerCheckers.Attach(personPrefixMakerChecker);
                    context.Entry(personPrefixMakerChecker).State = EntityState.Added;
                    personPrefix.PersonPrefixMakerCheckers.Add(personPrefixMakerChecker);
                }
                else
                {
                    context.PersonPrefixMakerCheckers.Attach(personPrefixMakerChecker);
                    context.Entry(personPrefixMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonAdditionalDetailData(PersonAdditionalDetailViewModel _personAdditionalDetailViewModel, string _entryType)
        {
            try
            {
                PersonViewModel personViewModel = new PersonViewModel();
                if (_personAdditionalDetailViewModel.MaritalStatusPrmKey == 0)
                    _personAdditionalDetailViewModel.MaritalStatusPrmKey = personDetailRepository.GetMaritalStatusPrmKeyById(_personAdditionalDetailViewModel.MaritalStatusId);

                configurationDetailRepository.SetDefaultValues(_personAdditionalDetailViewModel, _entryType);

                _personAdditionalDetailViewModel.BirthCityPrmKey = personDetailRepository.GetCenterPrmKeyById(_personAdditionalDetailViewModel.BirthCityId);
                _personAdditionalDetailViewModel.GenderPrmKey = personDetailRepository.GetGenderPrmKeyById(_personAdditionalDetailViewModel.GenderId);
                _personAdditionalDetailViewModel.BloodGroupPrmKey = personDetailRepository.GetBloodGroupPrmKeyById(_personAdditionalDetailViewModel.BloodGroupId);
                _personAdditionalDetailViewModel.CastCategoryPrmKey = personDetailRepository.GetCastCategoryPrmKeyById(_personAdditionalDetailViewModel.CastCategoryId);
                _personAdditionalDetailViewModel.EducationalQualificationPrmKey = personDetailRepository.GetEducationQualificationPrmKeyById(_personAdditionalDetailViewModel.EducationalQualificationId);
                _personAdditionalDetailViewModel.OccupationPrmKey = personDetailRepository.GetOccupationPrmKeyById(_personAdditionalDetailViewModel.OccupationId);
                _personAdditionalDetailViewModel.PersonTypePrmKey = personDetailRepository.GetPersonTypePrmKeyById(_personAdditionalDetailViewModel.PersonTypeId);
                _personAdditionalDetailViewModel.PhysicalStatusPrmKey = personDetailRepository.GetPhysicalStatusPrmKeyById(_personAdditionalDetailViewModel.PhysicalStatusId);
                _personAdditionalDetailViewModel.PovertyStatusPrmKey = personDetailRepository.GetPovertyStatusPrmKeyById(_personAdditionalDetailViewModel.PovertyStatusId);
                _personAdditionalDetailViewModel.PersonCategoryPrmKey = personDetailRepository.GetPersonCategoryPrmKeyById(_personAdditionalDetailViewModel.PersonCategoryId);
                string marritalStatus = personDetailRepository.GetSysNameOfMaritalStatusById(_personAdditionalDetailViewModel.MaritalStatusId);

                string personType = personDetailRepository.GetSysNameOfPersonTypeById(_personAdditionalDetailViewModel.PersonTypeId);

                if (marritalStatus != "MARRID")
                {
                    _personAdditionalDetailViewModel.LifePartnerName = "None";
                    _personAdditionalDetailViewModel.TransLifePartnerName = "None";
                    _personAdditionalDetailViewModel.LifePartnerMaidenName = "None";
                    _personAdditionalDetailViewModel.TransLifePartnerMaidenName = "None";
                }

                if (personType != "INDVL" && personType != null)
                {
                    _personAdditionalDetailViewModel.GenderPrmKey = GetGenderPrmKey();
                    _personAdditionalDetailViewModel.BirthCityPrmKey = 0;
                    _personAdditionalDetailViewModel.BloodGroupPrmKey = 0;
                    _personAdditionalDetailViewModel.MaritalStatusPrmKey = 0;
                    _personAdditionalDetailViewModel.PovertyStatusPrmKey = 0;
                    _personAdditionalDetailViewModel.PhysicalStatusPrmKey = 0;
                    _personAdditionalDetailViewModel.CastCategoryPrmKey = 0;
                    _personAdditionalDetailViewModel.EducationalQualificationPrmKey = 0;
                    _personAdditionalDetailViewModel.OccupationPrmKey = 0;

                }

                PersonAdditionalDetail personAdditionalDetail = Mapper.Map<PersonAdditionalDetail>(_personAdditionalDetailViewModel);
                PersonAdditionalDetailMakerChecker personAdditionalDetailMakerChecker = Mapper.Map<PersonAdditionalDetailMakerChecker>(_personAdditionalDetailViewModel);

                PersonAdditionalDetailTranslation personAdditionalDetailTranslation = Mapper.Map<PersonAdditionalDetailTranslation>(_personAdditionalDetailViewModel);
                PersonAdditionalDetailTranslationMakerChecker personAdditionalDetailTranslationMakerChecker = Mapper.Map<PersonAdditionalDetailTranslationMakerChecker>(_personAdditionalDetailViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    if (personAdditionalDetail.PersonPrmKey == 0)
                    {
                        personAdditionalDetail.PersonPrmKey = personPrmKey;
                    }

                    personAdditionalDetailTranslation.PrmKey = _personAdditionalDetailViewModel.PersonAdditionalDetailTranslationPrmKey;

                    if (personAdditionalDetailTranslation.TransNote == null)
                    {
                        personAdditionalDetailTranslation.TransNote = "None";
                    }
                    context.PersonAdditionalDetails.Attach(personAdditionalDetail);
                    context.Entry(personAdditionalDetail).State = entityState;

                    context.PersonAdditionalDetailMakerCheckers.Attach(personAdditionalDetailMakerChecker);
                    context.Entry(personAdditionalDetailMakerChecker).State = EntityState.Added;
                    personAdditionalDetail.PersonAdditionalDetailMakerCheckers.Add(personAdditionalDetailMakerChecker);

                    context.PersonAdditionalDetailTranslations.Attach(personAdditionalDetailTranslation);
                    context.Entry(personAdditionalDetailTranslation).State = entityState;
                    personAdditionalDetail.PersonAdditionalDetailTranslations.Add(personAdditionalDetailTranslation);

                    context.PersonAdditionalDetailTranslationMakerCheckers.Attach(personAdditionalDetailTranslationMakerChecker);
                    context.Entry(personAdditionalDetailTranslationMakerChecker).State = EntityState.Added;
                    personAdditionalDetailTranslation.PersonAdditionalDetailTranslationMakerCheckers.Add(personAdditionalDetailTranslationMakerChecker);
                }

                else
                {
                    context.PersonAdditionalDetailMakerCheckers.Attach(personAdditionalDetailMakerChecker);
                    context.Entry(personAdditionalDetailMakerChecker).State = EntityState.Added;

                    context.PersonAdditionalDetailTranslationMakerCheckers.Attach(personAdditionalDetailTranslationMakerChecker);
                    context.Entry(personAdditionalDetailTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonHomeBranchData(PersonHomeBranchViewModel _personHomeBranchViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personHomeBranchViewModel, _entryType);

                _personHomeBranchViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_personHomeBranchViewModel.HomeBranchId);
                _personHomeBranchViewModel.PersonRegionalLanguagePrmKey = configurationDetailRepository.GetLanguagePrmKeyById(_personHomeBranchViewModel.LanguageId);


                PersonHomeBranch personHomeBranch = Mapper.Map<PersonHomeBranch>(_personHomeBranchViewModel);
                PersonHomeBranchMakerChecker personHomeBranchMakerChecker = Mapper.Map<PersonHomeBranchMakerChecker>(_personHomeBranchViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    if (personHomeBranch.PersonPrmKey == 0)
                    {
                        personHomeBranch.PersonPrmKey = personPrmKey;
                    }

                    context.PersonHomeBranches.Attach(personHomeBranch);
                    context.Entry(personHomeBranch).State = entityState;
                    person.PersonHomeBranches.Add(personHomeBranch);

                    context.PersonHomeBranchMakerCheckers.Attach(personHomeBranchMakerChecker);
                    context.Entry(personHomeBranchMakerChecker).State = EntityState.Added;
                    personHomeBranch.PersonHomeBranchMakerCheckers.Add(personHomeBranchMakerChecker);
                }
                else
                {
                    context.PersonHomeBranchMakerCheckers.Attach(personHomeBranchMakerChecker);
                    context.Entry(personHomeBranchMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachForeignerPersonData(ForeignerViewModel _foreignerViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_foreignerViewModel, _entryType);

               
                ForeignerPerson foreignerPerson = Mapper.Map<ForeignerPerson>(_foreignerViewModel);
                ForeignerPersonMakerChecker foreignerPersonMakerChecker = Mapper.Map<ForeignerPersonMakerChecker>(_foreignerViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    if (foreignerPerson.PersonPrmKey == 0)
                    {
                        foreignerPerson.PersonPrmKey = personPrmKey;
                    }

                    context.ForeignerPersons.Attach(foreignerPerson);
                    context.Entry(foreignerPerson).State = entityState;
                    person.ForeignerPersons.Add(foreignerPerson);

                    context.ForeignerPersonMakerCheckers.Attach(foreignerPersonMakerChecker);
                    context.Entry(foreignerPersonMakerChecker).State = EntityState.Added;
                    foreignerPerson.ForeignerPersonMakerCheckers.Add(foreignerPersonMakerChecker);
                }
                else
                {
                    context.ForeignerPersonMakerCheckers.Attach(foreignerPersonMakerChecker);
                    context.Entry(foreignerPersonMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachGuardianPersonData(GuardianPersonViewModel _guardianPersonViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_guardianPersonViewModel, _entryType);
                _guardianPersonViewModel.RelationPrmKey = personDetailRepository.GetRelationPrmKeyById(_guardianPersonViewModel.RelationId);
                if(_guardianPersonViewModel.PersonInformationNumber > 0)
                {
                  _guardianPersonViewModel.GuardianFullName = "None";
                  _guardianPersonViewModel.FullAddress = "None";
                  _guardianPersonViewModel.TransFullAddress = "None";
                  _guardianPersonViewModel.TransGuardianFullName = "None";
                }
                GuardianPerson guardianPerson = Mapper.Map<GuardianPerson>(_guardianPersonViewModel);
                GuardianPersonMakerChecker guardianPersonMakerChecker = Mapper.Map<GuardianPersonMakerChecker>(_guardianPersonViewModel);

                GuardianPersonTranslation guardianPersonTranslation = Mapper.Map<GuardianPersonTranslation>(_guardianPersonViewModel);
                GuardianPersonTranslationMakerChecker guardianPersonTranslationMakerChecker = Mapper.Map<GuardianPersonTranslationMakerChecker>(_guardianPersonViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;


                    if (guardianPerson.PersonPrmKey == 0)
                    {
                        guardianPerson.PersonPrmKey = personPrmKey;
                    }

                    guardianPersonTranslation.PrmKey = _guardianPersonViewModel.GuardianPersonTranslationPrmKey;
                    if (guardianPersonTranslation.TransNote == null)
                    {
                        guardianPersonTranslation.TransNote = "None";
                    }
                    context.GuardianPersons.Attach(guardianPerson);
                    context.Entry(guardianPerson).State = entityState;

                    context.GuardianPersonMakerCheckers.Attach(guardianPersonMakerChecker);
                    context.Entry(guardianPersonMakerChecker).State = EntityState.Added;
                    guardianPerson.GuardianPersonMakerCheckers.Add(guardianPersonMakerChecker);

                    context.GuardianPersonTranslations.Attach(guardianPersonTranslation);
                    context.Entry(guardianPersonTranslation).State = entityState;
                    guardianPerson.GuardianPersonTranslations.Add(guardianPersonTranslation);

                    context.GuardianPersonTranslationMakerCheckers.Attach(guardianPersonTranslationMakerChecker);
                    context.Entry(guardianPersonTranslationMakerChecker).State = EntityState.Added;
                    guardianPersonTranslation.GuardianPersonTranslationMakerCheckers.Add(guardianPersonTranslationMakerChecker);
                }
                else
                {
                    context.GuardianPersonMakerCheckers.Attach(guardianPersonMakerChecker);
                    context.Entry(guardianPersonMakerChecker).State = EntityState.Added;

                    context.GuardianPersonTranslationMakerCheckers.Attach(guardianPersonTranslationMakerChecker);
                    context.Entry(guardianPersonTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonCommoditiesAssetData(PersonCommoditiesAssetViewModel _personCommoditiesAssetViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personCommoditiesAssetViewModel, _entryType);

                PersonCommoditiesAsset personCommoditiesAsset = Mapper.Map<PersonCommoditiesAsset>(_personCommoditiesAssetViewModel);
                PersonCommoditiesAssetMakerChecker personCommoditiesAssetMakerChecker = Mapper.Map<PersonCommoditiesAssetMakerChecker>(_personCommoditiesAssetViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    if (personCommoditiesAsset.PersonPrmKey == 0)
                    {
                        personCommoditiesAsset.PersonPrmKey = personPrmKey;
                    }

                    context.PersonCommoditiesAssets.Attach(personCommoditiesAsset);
                    context.Entry(personCommoditiesAsset).State = entityState;
                    person.PersonCommoditiesAssets.Add(personCommoditiesAsset);

                    context.PersonCommoditiesAssetMakerCheckers.Attach(personCommoditiesAssetMakerChecker);
                    context.Entry(personCommoditiesAssetMakerChecker).State = EntityState.Added;
                    personCommoditiesAsset.PersonCommoditiesAssetMakerCheckers.Add(personCommoditiesAssetMakerChecker);
                }
                else
                {
                    context.PersonCommoditiesAssetMakerCheckers.Attach(personCommoditiesAssetMakerChecker);
                    context.Entry(personCommoditiesAssetMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonEmploymentDetailData(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personEmploymentDetailViewModel, _entryType);

                _personEmploymentDetailViewModel.EmploymentTypePrmKey = managementDetailRepository.GetEmploymentTypePrmKeyById(_personEmploymentDetailViewModel.EmploymentTypeId);
                _personEmploymentDetailViewModel.EmployerNaturePrmKey = managementDetailRepository.GetEmployerNaturePrmKeyById(_personEmploymentDetailViewModel.NatureOfEmployerId);
                _personEmploymentDetailViewModel.DesignationPrmKey = managementDetailRepository.GetDesignationPrmKeyById(_personEmploymentDetailViewModel.DesignationId);
                _personEmploymentDetailViewModel.EmployerCityPrmKey = personDetailRepository.GetCenterPrmKeyById(_personEmploymentDetailViewModel.CityId);

                PersonEmploymentDetail personEmploymentDetail = Mapper.Map<PersonEmploymentDetail>(_personEmploymentDetailViewModel);
                PersonEmploymentDetailMakerChecker personEmploymentDetailMakerChecker = Mapper.Map<PersonEmploymentDetailMakerChecker>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailTranslation personEmploymentDetailTranslation = Mapper.Map<PersonEmploymentDetailTranslation>(_personEmploymentDetailViewModel);
                PersonEmploymentDetailTranslationMakerChecker personEmploymentDetailTranslationMakerChecker = Mapper.Map<PersonEmploymentDetailTranslationMakerChecker>(_personEmploymentDetailViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    if (personEmploymentDetail.PersonPrmKey == 0)
                    {
                        personEmploymentDetail.PersonPrmKey = personPrmKey;
                    }

                    personEmploymentDetailTranslation.PrmKey = _personEmploymentDetailViewModel.PersonEmploymentDetailTranslationPrmKey;

                    context.PersonEmploymentDetails.Attach(personEmploymentDetail);
                    context.Entry(personEmploymentDetail).State = entityState;

                    context.PersonEmploymentDetailMakerCheckers.Attach(personEmploymentDetailMakerChecker);
                    context.Entry(personEmploymentDetailMakerChecker).State = EntityState.Added;
                    personEmploymentDetail.PersonEmploymentDetailMakerCheckers.Add(personEmploymentDetailMakerChecker);

                    context.PersonEmploymentDetailTranslations.Attach(personEmploymentDetailTranslation);
                    context.Entry(personEmploymentDetailTranslation).State = entityState;
                    personEmploymentDetail.PersonEmploymentDetailTranslations.Add(personEmploymentDetailTranslation);

                    context.PersonEmploymentDetailTranslationMakerCheckers.Attach(personEmploymentDetailTranslationMakerChecker);
                    context.Entry(personEmploymentDetailTranslationMakerChecker).State = EntityState.Added;
                    personEmploymentDetailTranslation.PersonEmploymentDetailTranslationMakerCheckers.Add(personEmploymentDetailTranslationMakerChecker);
                }
                else
                {
                    context.PersonEmploymentDetailMakerCheckers.Attach(personEmploymentDetailMakerChecker);
                    context.Entry(personEmploymentDetailMakerChecker).State = EntityState.Added;

                    context.PersonEmploymentDetailTranslationMakerCheckers.Attach(personEmploymentDetailTranslationMakerChecker);
                    context.Entry(personEmploymentDetailTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonAdditionalIncomeDetailData(PersonAdditionalIncomeDetailViewModel _personAdditionalIncomeDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personAdditionalIncomeDetailViewModel, _entryType);
                _personAdditionalIncomeDetailViewModel.IncomeSourcePrmKey = personDetailRepository.GetIncomeSourcePrmKeyById(_personAdditionalIncomeDetailViewModel.IncomeSourceId);

                PersonAdditionalIncomeDetail personAdditionalIncomeDetail = Mapper.Map<PersonAdditionalIncomeDetail>(_personAdditionalIncomeDetailViewModel);
                PersonAdditionalIncomeDetailMakerChecker personAdditionalIncomeDetailMakerChecker = Mapper.Map<PersonAdditionalIncomeDetailMakerChecker>(_personAdditionalIncomeDetailViewModel);

                if (_entryType == StringLiteralValue.Create)
                {

                    if (personAdditionalIncomeDetail.PersonPrmKey == 0)
                    {
                        personAdditionalIncomeDetail.PersonPrmKey = personPrmKey;
                    }

                    context.PersonAdditionalIncomeDetails.Attach(personAdditionalIncomeDetail);
                    context.Entry(personAdditionalIncomeDetail).State = EntityState.Added;
                    person.PersonAdditionalIncomeDetails.Add(personAdditionalIncomeDetail);

                    context.PersonAdditionalIncomeDetailMakerCheckers.Attach(personAdditionalIncomeDetailMakerChecker);
                    context.Entry(personAdditionalIncomeDetailMakerChecker).State = EntityState.Added;
                    personAdditionalIncomeDetail.PersonAdditionalIncomeDetailMakerCheckers.Add(personAdditionalIncomeDetailMakerChecker);
                }

                else
                {
                    context.PersonAdditionalIncomeDetailMakerCheckers.Attach(personAdditionalIncomeDetailMakerChecker);
                    context.Entry(personAdditionalIncomeDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonAddressData(PersonAddressViewModel _personAddressViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personAddressViewModel, _entryType);

                _personAddressViewModel.AddressTypePrmKey = personDetailRepository.GetAddressTypePrmKeyById(_personAddressViewModel.AddressTypeId);
                _personAddressViewModel.CityPrmKey = personDetailRepository.GetCenterPrmKeyById(_personAddressViewModel.CityId);
                _personAddressViewModel.ResidenceTypePrmKey = personDetailRepository.GetResidenceTypePrmKeyById(_personAddressViewModel.ResidenceTypeId);
                _personAddressViewModel.OwnershipTypePrmKey = personDetailRepository.GetOwnershipTypePrmKeyById(_personAddressViewModel.OwnershipTypeId);

                PersonAddress personAddress = Mapper.Map<PersonAddress>(_personAddressViewModel);
                PersonAddressMakerChecker personAddressMakerChecker = Mapper.Map<PersonAddressMakerChecker>(_personAddressViewModel);

                PersonAddressTranslation personAddressTranslation = Mapper.Map<PersonAddressTranslation>(_personAddressViewModel);
                PersonAddressTranslationMakerChecker personAddressTranslationMakerChecker = Mapper.Map<PersonAddressTranslationMakerChecker>(_personAddressViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personAddress.PersonPrmKey == 0)
                    {
                        personAddress.PersonPrmKey = personPrmKey;
                    }

                    context.PersonAddresses.Attach(personAddress);
                    context.Entry(personAddress).State = EntityState.Added;
                    person.PersonAddresses.Add(personAddress);

                    context.PersonAddressMakerCheckers.Attach(personAddressMakerChecker);
                    context.Entry(personAddressMakerChecker).State = EntityState.Added;
                    personAddress.PersonAddressMakerCheckers.Add(personAddressMakerChecker);

                    context.PersonAddressTranslations.Attach(personAddressTranslation);
                    context.Entry(personAddressTranslation).State = EntityState.Added;
                    personAddress.PersonAddressTranslations.Add(personAddressTranslation);

                    context.PersonAddressTranslationMakerCheckers.Attach(personAddressTranslationMakerChecker);
                    context.Entry(personAddressTranslationMakerChecker).State = EntityState.Added;
                    personAddressTranslation.PersonAddressTranslationMakerCheckers.Add(personAddressTranslationMakerChecker);


                }
                else
                {
                    context.PersonAddressMakerCheckers.Attach(personAddressMakerChecker);
                    context.Entry(personAddressMakerChecker).State = EntityState.Added;

                    context.PersonAddressTranslationMakerCheckers.Attach(personAddressTranslationMakerChecker);
                    context.Entry(personAddressTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonAgricultureAssetData(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personAgricultureAssetViewModel, _entryType);
                _personAgricultureAssetViewModel.AgricultureLandTypePrmKey = accountDetailRepository.GetAgricultureLandTypePrmKeyById(_personAgricultureAssetViewModel.AgricultureLandTypeId);
                _personAgricultureAssetViewModel.OwnershipTypePrmKey = personDetailRepository.GetOwnershipTypePrmKeyById(_personAgricultureAssetViewModel.OwnershipTypeId);

                 personAgricultureAsset = Mapper.Map<PersonAgricultureAsset>(_personAgricultureAssetViewModel);
                PersonAgricultureAssetMakerChecker personAgricultureAssetMakerChecker = Mapper.Map<PersonAgricultureAssetMakerChecker>(_personAgricultureAssetViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    _personAgricultureAssetViewModel.PersonAgricultureAssetDocumentPrmKey = 0;

                    if (personAgricultureAsset.PersonPrmKey == 0)
                    {
                        personAgricultureAsset.PersonPrmKey = personPrmKey;
                    }

                    context.PersonAgricultureAssets.Attach(personAgricultureAsset);
                    context.Entry(personAgricultureAsset).State = EntityState.Added;
                    person.PersonAgricultureAssets.Add(personAgricultureAsset);

                    context.PersonAgricultureAssetMakerCheckers.Attach(personAgricultureAssetMakerChecker);
                    context.Entry(personAgricultureAssetMakerChecker).State = EntityState.Added;
                    personAgricultureAsset.PersonAgricultureAssetMakerCheckers.Add(personAgricultureAssetMakerChecker);
                }

                else
                {
                    context.PersonAgricultureAssetMakerCheckers.Attach(personAgricultureAssetMakerChecker);
                    context.Entry(personAgricultureAssetMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonAgricultureAssetDocumentData(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel, string _localStoragePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personAgricultureAssetViewModel, _entryType);


                PersonAgricultureAssetDocument personAgricultureAssetDocument = Mapper.Map<PersonAgricultureAssetDocument>(_personAgricultureAssetViewModel);
                PersonAgricultureAssetDocumentMakerChecker personAgricultureAssetDocumentMakerChecker = Mapper.Map<PersonAgricultureAssetDocumentMakerChecker>(_personAgricultureAssetViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personAgricultureAssetDocument.PrmKey == 0)
                    personAgricultureAssetDocument.PrmKey = _personAgricultureAssetViewModel.PersonAgricultureAssetDocumentPrmKey;

                    context.PersonAgricultureAssetDocuments.Attach(personAgricultureAssetDocument);
                    context.Entry(personAgricultureAssetDocument).State = EntityState.Added;
                    personAgricultureAsset.PersonAgricultureAssetDocuments.Add(personAgricultureAssetDocument);

                    context.PersonAgricultureAssetDocumentMakerCheckers.Attach(personAgricultureAssetDocumentMakerChecker);
                    context.Entry(personAgricultureAssetDocumentMakerChecker).State = EntityState.Added;
                    personAgricultureAssetDocument.PersonAgricultureAssetDocumentMakerCheckers.Add(personAgricultureAssetDocumentMakerChecker);

                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image when PhotoUpload is Optional.
                    if ((_oldFileName != _personAgricultureAssetViewModel.NameOfFile && _oldFileName != "None") || _personAgricultureAssetViewModel.FileCaption == "NotApplicable")
                    {
                        string serverDestinationPath = " ";

                        // Get Destination Path From Database
                        string destinationPath = _localStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }

                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        _personAgricultureAssetViewModel.LocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                        oldRecord.Add("OldRecord");
                        localStorageFilePathListForOldRecord.Add(_personAgricultureAssetViewModel.LocalStoragePath);
                        httpPostedFileBaseListForOldRecord.Add(_personAgricultureAssetViewModel.PhotoPathAgree);
                    }
                }
                else
                {
                    context.PersonAgricultureAssetDocumentMakerCheckers.Attach(personAgricultureAssetDocumentMakerChecker);
                    context.Entry(personAgricultureAssetDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonBankDetailData(PersonBankDetailViewModel _personBankDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personBankDetailViewModel, _entryType);
                _personBankDetailViewModel.BankBranchPrmKey = enterpriseDetailRepository.GetBankBranchPrmKeyById(_personBankDetailViewModel.BankBranchId);
                _personBankDetailViewModel.PersonBankDetailPrmKey = _personBankDetailViewModel.PrmKey;

                personBankDetail = Mapper.Map<PersonBankDetail>(_personBankDetailViewModel);
                PersonBankDetailMakerChecker personBankDetailMakerChecker = Mapper.Map<PersonBankDetailMakerChecker>(_personBankDetailViewModel);


                if (_entryType == StringLiteralValue.Create)
                {
                    if (personBankDetail.PersonPrmKey == 0)
                    {
                        personBankDetail.PersonPrmKey = personPrmKey;
                    }
                    context.PersonBankDetails.Attach(personBankDetail);
                    context.Entry(personBankDetail).State = EntityState.Added;
                    person.PersonBankDetails.Add(personBankDetail);

                    context.PersonBankDetailMakerCheckers.Attach(personBankDetailMakerChecker);
                    context.Entry(personBankDetailMakerChecker).State = EntityState.Added;
                    personBankDetail.PersonBankDetailMakerCheckers.Add(personBankDetailMakerChecker);
                }

                else
                {
                    context.PersonBankDetailMakerCheckers.Attach(personBankDetailMakerChecker);
                    context.Entry(personBankDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonBankDetailDocumentData(PersonBankDetailViewModel _personBankDetailViewModel, string _localStoragePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personBankDetailViewModel, _entryType);

                PersonBankDetailDocument personBankDetailDocument = Mapper.Map<PersonBankDetailDocument>(_personBankDetailViewModel);
                PersonBankDetailDocumentMakerChecker personBankDetailDocumentMakerChecker = Mapper.Map<PersonBankDetailDocumentMakerChecker>(_personBankDetailViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    personBankDetailDocumentMakerChecker.PersonBankDetailDocumentPrmKey = 0;

                    context.PersonBankDetailDocuments.Attach(personBankDetailDocument);
                    context.Entry(personBankDetailDocument).State = EntityState.Added;
                    personBankDetail.PersonBankDetailDocuments.Add(personBankDetailDocument);

                    context.PersonBankDetailDocumentMakerCheckers.Attach(personBankDetailDocumentMakerChecker);
                    context.Entry(personBankDetailDocumentMakerChecker).State = EntityState.Added;
                    personBankDetailDocument.PersonBankDetailDocumentMakerCheckers.Add(personBankDetailDocumentMakerChecker);

                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image when PhotoUpload is Optional.
                    if ((_oldFileName != _personBankDetailViewModel.NameOfFile && _oldFileName != "None") || _personBankDetailViewModel.FileCaption == "NotApplicable")
                    {
                        string serverDestinationPath = " ";

                        // Get Destination Path From Database
                        string destinationPath = _localStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }

                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        _personBankDetailViewModel.LocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                        oldRecord.Add("OldRecord");
                        localStorageFilePathListForOldRecord.Add(_personBankDetailViewModel.LocalStoragePath);
                        httpPostedFileBaseListForOldRecord.Add(_personBankDetailViewModel.PhotoPathBank);
                    }
                }
                else
                {
                    context.PersonBankDetailDocumentMakerCheckers.Attach(personBankDetailDocumentMakerChecker);
                    context.Entry(personBankDetailDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonGroupData(PersonGroupViewModel _personGroupViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personGroupViewModel, _entryType);

                personGroup = Mapper.Map<PersonGroup>(_personGroupViewModel);
                PersonGroupMakerChecker personGroupMakerChecker = Mapper.Map<PersonGroupMakerChecker>(_personGroupViewModel);
                
                if (personGroup.HasAnyAssociatedCompanies == false)
                {
                    personGroup.AssociatedCompaniesList = "None";
                }

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    if (personGroup.PersonPrmKey == 0)
                    {
                        personGroup.PersonPrmKey = personPrmKey;
                    }

                    context.PersonGroups.Attach(personGroup);
                    context.Entry(personGroup).State = entityState;
                    person.PersonGroups.Add(personGroup);

                    context.PersonGroupMakerCheckers.Attach(personGroupMakerChecker);
                    context.Entry(personGroupMakerChecker).State = EntityState.Added;
                    personGroup.PersonGroupMakerCheckers.Add(personGroupMakerChecker);
                }

                else
                {
                    context.PersonGroupMakerCheckers.Attach(personGroupMakerChecker);
                    context.Entry(personGroupMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


        public bool AttachPersonGroupMasterData(PersonGroupMasterViewModel _personGroupMasterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personGroupMasterViewModel, _entryType);

                personGroup = Mapper.Map<PersonGroup>(_personGroupMasterViewModel);
                PersonGroupMakerChecker personGroupMakerChecker = Mapper.Map<PersonGroupMakerChecker>(_personGroupMasterViewModel);

                PersonModification personModification = Mapper.Map<PersonModification>(_personGroupMasterViewModel);
                PersonModificationMakerChecker personModificationMakerChecker = Mapper.Map<PersonModificationMakerChecker>(_personGroupMasterViewModel);

                PersonTranslation personTranslation = Mapper.Map<PersonTranslation>(_personGroupMasterViewModel);
                PersonTranslationMakerChecker personTranslationMakerChecker = Mapper.Map<PersonTranslationMakerChecker>(_personGroupMasterViewModel);

                string fullName = personModification.FullName;

                string[] parts = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string transFullName = personTranslation.TransFullName;

                string[] transParts = transFullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


                // personTranslation.PrmKey = 2;

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;
                    personModification.FirstName = parts[1];
                    personModification.LastName = parts[0];
                    personModification.MiddleName = parts[2];
                    personModification.MotherName = "None";
                    personModification.MothersMaidenName = "None";
                    personModification.ReasonForModification = "None";
                    personModification.DateOfBirth = new DateTime(1900, 01, 01, 0, 0, 0, 0);
                    personModification.DateOfBirthOnDocument = new DateTime(1900, 01, 01, 0, 0, 0, 0);
                    personTranslation.TransFirstName = transParts[1];
                    personTranslation.TransLastName = transParts[0];
                    personTranslation.TransMiddleName = transParts[2];
                    personTranslation.TransModificationNumber = 0;
                    personTranslation.TransMotherName = "None";
                    personTranslation.TransMothersMaidenName = "None";
                    personTranslation.TransNote = "None";
                    personTranslation.LanguagePrmKey = 2;
                    if (personGroup.PersonPrmKey == 0)
                    {
                        personGroup.PersonPrmKey = personPrmKey;
                    }
                    personModification.PrmKey = _personGroupMasterViewModel.PersonModificationPrmKey;
                    personTranslation.PrmKey = _personGroupMasterViewModel.PersonTranslationPrmKey;
                    context.PersonGroups.Attach(personGroup);
                    context.Entry(personGroup).State = entityState;
                    person.PersonGroups.Add(personGroup);

                    context.PersonGroupMakerCheckers.Attach(personGroupMakerChecker);
                    context.Entry(personGroupMakerChecker).State = EntityState.Added;
                    personGroup.PersonGroupMakerCheckers.Add(personGroupMakerChecker);

                    context.PersonModifications.Attach(personModification);
                    context.Entry(personModification).State = entityState;

                    context.PersonModificationMakerCheckers.Attach(personModificationMakerChecker);
                    context.Entry(personModificationMakerChecker).State = EntityState.Added;
                    personModification.PersonModificationMakerCheckers.Add(personModificationMakerChecker);

                    context.PersonTranslations.Attach(personTranslation);
                    context.Entry(personTranslation).State = entityState;
                    person.PersonTranslations.Add(personTranslation);

                    context.PersonTranslationMakerCheckers.Attach(personTranslationMakerChecker);
                    context.Entry(personTranslationMakerChecker).State = EntityState.Added;
                    personTranslation.PersonTranslationMakerCheckers.Add(personTranslationMakerChecker);
                }

                else
                {
                    context.PersonGroupMakerCheckers.Attach(personGroupMakerChecker);
                    context.Entry(personGroupMakerChecker).State = EntityState.Added;

                    context.PersonModificationMakerCheckers.Attach(personModificationMakerChecker);
                    context.Entry(personModificationMakerChecker).State = EntityState.Added;

                    context.PersonTranslationMakerCheckers.Attach(personTranslationMakerChecker);
                    context.Entry(personTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonGroupAuthorizedSignatoryData(PersonGroupAuthorizedSignatoryViewModel _personGroupAuthorizedSignatoryViewModel, string _localStoragePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personGroupAuthorizedSignatoryViewModel, _entryType);

                //_personGroupAuthorizedSignatoryViewModel.PersonGroupAuthorizedSignatoryPrmKey = _personGroupAuthorizedSignatoryViewModel.PrmKey;
                _personGroupAuthorizedSignatoryViewModel.DesignationPrmKey = enterpriseDetailRepository.GetDesignationPrmKeyById(_personGroupAuthorizedSignatoryViewModel.DesignationId);

                PersonGroupAuthorizedSignatory personGroupAuthorizedSignatory = Mapper.Map<PersonGroupAuthorizedSignatory>(_personGroupAuthorizedSignatoryViewModel);
                PersonGroupAuthorizedSignatoryMakerChecker personGroupAuthorizedSignatoryMakerChecker = Mapper.Map<PersonGroupAuthorizedSignatoryMakerChecker>(_personGroupAuthorizedSignatoryViewModel);

                PersonGroupAuthorizedSignatoryTranslation personGroupAuthorizedSignatoryTranslation = Mapper.Map<PersonGroupAuthorizedSignatoryTranslation>(_personGroupAuthorizedSignatoryViewModel);
                PersonGroupAuthorizedSignatoryTranslationMakerChecker personGroupAuthorizedSignatoryTranslationMakerChecker = Mapper.Map<PersonGroupAuthorizedSignatoryTranslationMakerChecker>(_personGroupAuthorizedSignatoryViewModel);


                if (_entryType == StringLiteralValue.Create)
                {

                    //personGroupAuthorizedSignatory.PersonGroupPrmKey = personPrmKey;

                    context.PersonGroupAuthorizedSignatories.Attach(personGroupAuthorizedSignatory);
                    context.Entry(personGroupAuthorizedSignatory).State = EntityState.Added;
                    personGroup.PersonGroupAuthorizedSignatories.Add(personGroupAuthorizedSignatory);

                    context.PersonGroupAuthorizedSignatoryMakerCheckers.Attach(personGroupAuthorizedSignatoryMakerChecker);
                    context.Entry(personGroupAuthorizedSignatoryMakerChecker).State = EntityState.Added;
                    personGroupAuthorizedSignatory.PersonGroupAuthorizedSignatoryMakerCheckers.Add(personGroupAuthorizedSignatoryMakerChecker);

                    context.PersonGroupAuthorizedSignatoryTranslations.Attach(personGroupAuthorizedSignatoryTranslation);
                    context.Entry(personGroupAuthorizedSignatoryTranslation).State = EntityState.Added;
                    personGroupAuthorizedSignatory.PersonGroupAuthorizedSignatoryTranslations.Add(personGroupAuthorizedSignatoryTranslation);

                    context.PersonGroupAuthorizedSignatoryTranslationMakerCheckers.Attach(personGroupAuthorizedSignatoryTranslationMakerChecker);
                    context.Entry(personGroupAuthorizedSignatoryTranslationMakerChecker).State = EntityState.Added;
                    personGroupAuthorizedSignatoryTranslation.PersonGroupAuthorizedSignatoryTranslationMakerCheckers.Add(personGroupAuthorizedSignatoryTranslationMakerChecker);

                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image When PhotoUpload is Optional.
                    if ((_oldFileName != _personGroupAuthorizedSignatoryViewModel.SignNameOfFile && _oldFileName != "None") || _personGroupAuthorizedSignatoryViewModel.SignFileCaption == "NotApplicable")
                    {
                        string serverDestinationPath = " ";

                        // Get Destination Path From Database
                        string destinationPath = _localStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }

                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        _personGroupAuthorizedSignatoryViewModel.SignLocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                        oldRecord.Add("OldRecord");
                        localStorageFilePathListForOldRecord.Add(_personGroupAuthorizedSignatoryViewModel.SignLocalStoragePath);
                        httpPostedFileBaseListForOldRecord.Add(_personGroupAuthorizedSignatoryViewModel.PhotoPathSign);
                    }
                }

                else
                {
                    context.PersonGroupAuthorizedSignatoryMakerCheckers.Attach(personGroupAuthorizedSignatoryMakerChecker);
                    context.Entry(personGroupAuthorizedSignatoryMakerChecker).State = EntityState.Added;

                    context.PersonGroupAuthorizedSignatoryTranslationMakerCheckers.Attach(personGroupAuthorizedSignatoryTranslationMakerChecker);
                    context.Entry(personGroupAuthorizedSignatoryTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


        public bool AttachPersonBoardOfDirectorRelationData(PersonBoardOfDirectorRelationViewModel _personBoardOfDirectorRelationViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personBoardOfDirectorRelationViewModel, _entryType);
                _personBoardOfDirectorRelationViewModel.BoardOfDirectorPrmKey = managementDetailRepository.GetBoardOfDirectorPrmKeyById(_personBoardOfDirectorRelationViewModel.BoardOfDirectorId);
                _personBoardOfDirectorRelationViewModel.RelationPrmKey = personDetailRepository.GetRelationPrmKeyById(_personBoardOfDirectorRelationViewModel.RelationId);

                PersonBoardOfDirectorRelation personBoardOfDirectorRelation = Mapper.Map<PersonBoardOfDirectorRelation>(_personBoardOfDirectorRelationViewModel);
                PersonBoardOfDirectorRelationMakerChecker personBoardOfDirectorRelationMakerChecker = Mapper.Map<PersonBoardOfDirectorRelationMakerChecker>(_personBoardOfDirectorRelationViewModel);

                if (_entryType == StringLiteralValue.Create)
                {                
                    if (personBoardOfDirectorRelation.PersonPrmKey == 0)
                    {
                        personBoardOfDirectorRelation.PersonPrmKey = personPrmKey;
                    }

                    context.PersonBoardOfDirectorRelations.Attach(personBoardOfDirectorRelation);
                    context.Entry(personBoardOfDirectorRelation).State = EntityState.Added;
                    person.PersonBoardOfDirectorRelations.Add(personBoardOfDirectorRelation);

                    context.PersonBoardOfDirectorRelationMakerCheckers.Attach(personBoardOfDirectorRelationMakerChecker);
                    context.Entry(personBoardOfDirectorRelationMakerChecker).State = EntityState.Added;
                    personBoardOfDirectorRelation.PersonBoardOfDirectorRelationMakerCheckers.Add(personBoardOfDirectorRelationMakerChecker);
                }
                else
                {
                    context.PersonBoardOfDirectorRelationMakerCheckers.Attach(personBoardOfDirectorRelationMakerChecker);
                    context.Entry(personBoardOfDirectorRelationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonBorrowingDetailData(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personBorrowingDetailViewModel, _entryType);

                PersonBorrowingDetail personBorrowingDetail = Mapper.Map<PersonBorrowingDetail>(_personBorrowingDetailViewModel);
                PersonBorrowingDetailMakerChecker personBorrowingDetailMakerChecker = Mapper.Map<PersonBorrowingDetailMakerChecker>(_personBorrowingDetailViewModel);

                PersonBorrowingDetailTranslation personBorrowingDetailTranslation = Mapper.Map<PersonBorrowingDetailTranslation>(_personBorrowingDetailViewModel);
                PersonBorrowingDetailTranslationMakerChecker personBorrowingDetailTranslationMakerChecker = Mapper.Map<PersonBorrowingDetailTranslationMakerChecker>(_personBorrowingDetailViewModel);


                if (_entryType == StringLiteralValue.Create)
                {
                    // IF IsTakingAnyCourtAction is True Then Add Court Case Details Else Not Add Court Case Details.
                    if (personBorrowingDetail.IsTakingAnyCourtAction == true)
                    {
                        if (personBorrowingDetail.PersonPrmKey == 0)
                        {
                            personBorrowingDetail.PersonPrmKey = personPrmKey;
                        }

                        personBorrowingDetail.CourtCaseStagePrmKey = personDetailRepository.GetCourtCaseStagePrmKeyById(_personBorrowingDetailViewModel.CourtCaseStageId);
                        personBorrowingDetail.CourtCaseTypePrmKey = personDetailRepository.GetCourtCaseTypePrmKeyById(_personBorrowingDetailViewModel.CourtCaseTypeId);

                        personBorrowingDetailTranslation.PrmKey = 0;
                        context.PersonBorrowingDetails.Attach(personBorrowingDetail);
                        context.Entry(personBorrowingDetail).State = EntityState.Added;
                        person.PersonBorrowingDetails.Add(personBorrowingDetail);

                        context.PersonBorrowingDetailMakerCheckers.Attach(personBorrowingDetailMakerChecker);
                        context.Entry(personBorrowingDetailMakerChecker).State = EntityState.Added;
                        personBorrowingDetail.PersonBorrowingDetailMakerCheckers.Add(personBorrowingDetailMakerChecker);

                        context.PersonBorrowingDetailTranslations.Attach(personBorrowingDetailTranslation);
                        context.Entry(personBorrowingDetailTranslation).State = EntityState.Added;
                        personBorrowingDetail.PersonBorrowingDetailTranslations.Add(personBorrowingDetailTranslation);

                        context.PersonBorrowingDetailTranslationMakerCheckers.Attach(personBorrowingDetailTranslationMakerChecker);
                        context.Entry(personBorrowingDetailTranslationMakerChecker).State = EntityState.Added;
                        personBorrowingDetailTranslation.PersonBorrowingDetailTranslationMakerCheckers.Add(personBorrowingDetailTranslationMakerChecker);

                    }
                    else
                    {
                        if (personBorrowingDetail.PersonPrmKey == 0)
                        {
                            personBorrowingDetail.PersonPrmKey = personPrmKey;
                        }

                        personBorrowingDetailTranslation.PrmKey = 0;
                        personBorrowingDetail.CourtCaseStagePrmKey = 0;
                        personBorrowingDetail.CourtCaseTypePrmKey = 0;
                        context.PersonBorrowingDetails.Attach(personBorrowingDetail);
                        context.Entry(personBorrowingDetail).State = EntityState.Added;
                        person.PersonBorrowingDetails.Add(personBorrowingDetail);

                        context.PersonBorrowingDetailMakerCheckers.Attach(personBorrowingDetailMakerChecker);
                        context.Entry(personBorrowingDetailMakerChecker).State = EntityState.Added;
                        personBorrowingDetail.PersonBorrowingDetailMakerCheckers.Add(personBorrowingDetailMakerChecker);

                        context.PersonBorrowingDetailTranslations.Attach(personBorrowingDetailTranslation);
                        context.Entry(personBorrowingDetailTranslation).State = EntityState.Added;
                        personBorrowingDetail.PersonBorrowingDetailTranslations.Add(personBorrowingDetailTranslation);

                        context.PersonBorrowingDetailTranslationMakerCheckers.Attach(personBorrowingDetailTranslationMakerChecker);
                        context.Entry(personBorrowingDetailTranslationMakerChecker).State = EntityState.Added;
                        personBorrowingDetailTranslation.PersonBorrowingDetailTranslationMakerCheckers.Add(personBorrowingDetailTranslationMakerChecker);

                    }

                }
                else
                {
                    context.PersonBorrowingDetailMakerCheckers.Attach(personBorrowingDetailMakerChecker);
                    context.Entry(personBorrowingDetailMakerChecker).State = EntityState.Added;

                    context.PersonBorrowingDetailTranslationMakerCheckers.Attach(personBorrowingDetailTranslationMakerChecker);
                    context.Entry(personBorrowingDetailTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonChronicDiseaseData(PersonChronicDiseaseViewModel _personChronicDiseaseViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personChronicDiseaseViewModel, _entryType);
                _personChronicDiseaseViewModel.DiseasePrmKey = personDetailRepository.GetDiseasePrmKeyById(_personChronicDiseaseViewModel.DiseaseId);

                PersonChronicDisease personChronicDisease = Mapper.Map<PersonChronicDisease>(_personChronicDiseaseViewModel);
                PersonChronicDiseaseMakerChecker personChronicDiseaseMakerChecker = Mapper.Map<PersonChronicDiseaseMakerChecker>(_personChronicDiseaseViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personChronicDisease.PersonPrmKey == 0)
                    {
                        personChronicDisease.PersonPrmKey = personPrmKey;
                    }

                    context.PersonChronicDiseases.Attach(personChronicDisease);
                    context.Entry(personChronicDisease).State = EntityState.Added;
                    person.PersonChronicDiseases.Add(personChronicDisease);

                    context.PersonChronicDiseaseMakerCheckers.Attach(personChronicDiseaseMakerChecker);
                    context.Entry(personChronicDiseaseMakerChecker).State = EntityState.Added;
                    personChronicDisease.PersonChronicDiseaseMakerCheckers.Add(personChronicDiseaseMakerChecker);
                }
                else
                {
                    context.PersonChronicDiseaseMakerCheckers.Attach(personChronicDiseaseMakerChecker);
                    context.Entry(personChronicDiseaseMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonContactDetailData(PersonContactDetailViewModel _personContactDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personContactDetailViewModel, _entryType);
                _personContactDetailViewModel.ContactTypePrmKey = personDetailRepository.GetContactTypePrmKeyById(_personContactDetailViewModel.ContactTypeId);

                PersonContactDetail personContactDetail = Mapper.Map<PersonContactDetail>(_personContactDetailViewModel);
                PersonContactDetailMakerChecker personContactDetailMakerChecker = Mapper.Map<PersonContactDetailMakerChecker>(_personContactDetailViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personContactDetail.PersonPrmKey == 0)
                    {
                        personContactDetail.PersonPrmKey = personPrmKey;
                    }

                    context.PersonContactDetails.Attach(personContactDetail);
                    context.Entry(personContactDetail).State = EntityState.Added;
                    person.PersonContactDetails.Add(personContactDetail);

                    context.PersonContactDetailMakerCheckers.Attach(personContactDetailMakerChecker);
                    context.Entry(personContactDetailMakerChecker).State = EntityState.Added;
                    personContactDetail.PersonContactDetailMakerCheckers.Add(personContactDetailMakerChecker);
                }
                else
                {
                    context.PersonContactDetailMakerCheckers.Attach(personContactDetailMakerChecker);
                    context.Entry(personContactDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonCourtCaseData(PersonCourtCaseViewModel _personCourtCaseViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personCourtCaseViewModel, _entryType);

                _personCourtCaseViewModel.CourtCaseStagePrmKey = personDetailRepository.GetCourtCaseStagePrmKeyById(_personCourtCaseViewModel.CourtCaseStageId);
                _personCourtCaseViewModel.CourtCaseTypePrmKey = personDetailRepository.GetCourtCaseTypePrmKeyById(_personCourtCaseViewModel.CourtCaseTypeId);

                PersonCourtCase personCourtCase = Mapper.Map<PersonCourtCase>(_personCourtCaseViewModel);
                PersonCourtCaseMakerChecker personCourtCaseMakerChecker = Mapper.Map<PersonCourtCaseMakerChecker>(_personCourtCaseViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personCourtCase.PersonPrmKey == 0)
                    {
                        personCourtCase.PersonPrmKey = personPrmKey;
                    }

                    context.PersonCourtCases.Attach(personCourtCase);
                    context.Entry(personCourtCase).State = EntityState.Added;
                    person.PersonCourtCases.Add(personCourtCase);

                    context.PersonCourtCaseMakerCheckers.Attach(personCourtCaseMakerChecker);
                    context.Entry(personCourtCaseMakerChecker).State = EntityState.Added;
                    personCourtCase.PersonCourtCaseMakerCheckers.Add(personCourtCaseMakerChecker);
                }

                else
                {
                    context.PersonCourtCaseMakerCheckers.Attach(personCourtCaseMakerChecker);
                    context.Entry(personCourtCaseMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonCreditRatingData(PersonCreditRatingViewModel _personCreditRatingViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personCreditRatingViewModel, _entryType);
                _personCreditRatingViewModel.CreditBureauAgencyPrmKey = accountDetailRepository.GetCreditBureauAgencyPrmKeyById(_personCreditRatingViewModel.CreditBureauAgencyId);

                PersonCreditRating personCreditRating = Mapper.Map<PersonCreditRating>(_personCreditRatingViewModel);
                PersonCreditRatingMakerChecker personCreditRatingMakerChecker = Mapper.Map<PersonCreditRatingMakerChecker>(_personCreditRatingViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personCreditRating.PersonPrmKey == 0)
                    {
                        personCreditRating.PersonPrmKey = personPrmKey;
                    }

                    context.PersonCreditRatings.Attach(personCreditRating);
                    context.Entry(personCreditRating).State = EntityState.Added;
                    person.PersonCreditRatings.Add(personCreditRating);

                    context.PersonCreditRatingMakerCheckers.Attach(personCreditRatingMakerChecker);
                    context.Entry(personCreditRatingMakerChecker).State = EntityState.Added;
                    personCreditRating.PersonCreditRatingMakerCheckers.Add(personCreditRatingMakerChecker);
                }
                else
                {
                    context.PersonCreditRatingMakerCheckers.Attach(personCreditRatingMakerChecker);
                    context.Entry(personCreditRatingMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonFamilyDetailData(PersonFamilyDetailViewModel _personFamilyDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personFamilyDetailViewModel, _entryType);


                _personFamilyDetailViewModel.RelationPrmKey = personDetailRepository.GetRelationPrmKeyById(_personFamilyDetailViewModel.RelationId);
                _personFamilyDetailViewModel.OccupationPrmKey = personDetailRepository.GetOccupationPrmKeyById(_personFamilyDetailViewModel.OccupationId);

                PersonFamilyDetail personFamilyDetail = Mapper.Map<PersonFamilyDetail>(_personFamilyDetailViewModel);
                PersonFamilyDetailMakerChecker personFamilyDetailMakerChecker = Mapper.Map<PersonFamilyDetailMakerChecker>(_personFamilyDetailViewModel);

                PersonFamilyDetailTranslation personFamilyDetailTranslation = Mapper.Map<PersonFamilyDetailTranslation>(_personFamilyDetailViewModel);
                PersonFamilyDetailTranslationMakerChecker personFamilyDetailTranslationMakerChecker = Mapper.Map<PersonFamilyDetailTranslationMakerChecker>(_personFamilyDetailViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personFamilyDetail.PersonPrmKey == 0)
                    {
                        personFamilyDetail.PersonPrmKey = personPrmKey;
                    }

                    context.PersonFamilyDetails.Attach(personFamilyDetail);
                    context.Entry(personFamilyDetail).State = EntityState.Added;
                    person.PersonFamilyDetails.Add(personFamilyDetail);

                    context.PersonFamilyDetailMakerCheckers.Attach(personFamilyDetailMakerChecker);
                    context.Entry(personFamilyDetailMakerChecker).State = EntityState.Added;
                    personFamilyDetail.PersonFamilyDetailMakerCheckers.Add(personFamilyDetailMakerChecker);

                    context.PersonFamilyDetailTranslations.Attach(personFamilyDetailTranslation);
                    context.Entry(personFamilyDetailTranslation).State = EntityState.Added;
                    personFamilyDetail.PersonFamilyDetailTranslations.Add(personFamilyDetailTranslation);

                    context.PersonFamilyDetailTranslationMakerCheckers.Attach(personFamilyDetailTranslationMakerChecker);
                    context.Entry(personFamilyDetailTranslationMakerChecker).State = EntityState.Added;
                    personFamilyDetailTranslation.PersonFamilyDetailTranslationMakerCheckers.Add(personFamilyDetailTranslationMakerChecker);
                }
                else
                {
                    context.PersonFamilyDetailMakerCheckers.Attach(personFamilyDetailMakerChecker);
                    context.Entry(personFamilyDetailMakerChecker).State = EntityState.Added;

                    context.PersonFamilyDetailTranslationMakerCheckers.Attach(personFamilyDetailTranslationMakerChecker);
                    context.Entry(personFamilyDetailTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonFinancialAssetData(PersonFinancialAssetViewModel _personFinancialAssetViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personFinancialAssetViewModel, _entryType);
                _personFinancialAssetViewModel.FinancialAssetTypePrmKey = accountDetailRepository.GetFinancialAssetTypePrmKeyById(_personFinancialAssetViewModel.FinancialAssetTypeId);
                _personFinancialAssetViewModel.FinancialOrganizationTypePrmKey = enterpriseDetailRepository.GetFinancialOrganizationTypePrmKeyById(_personFinancialAssetViewModel.FinancialOrganizationTypeId);

                 personFinancialAsset = Mapper.Map<PersonFinancialAsset>(_personFinancialAssetViewModel);
                PersonFinancialAssetMakerChecker personFinancialAssetMakerChecker = Mapper.Map<PersonFinancialAssetMakerChecker>(_personFinancialAssetViewModel);

                PersonFinancialAssetTranslation personFinancialAssetTranslation = Mapper.Map<PersonFinancialAssetTranslation>(_personFinancialAssetViewModel);
                PersonFinancialAssetTranslationMakerChecker personFinancialAssetTranslationMakerChecker = Mapper.Map<PersonFinancialAssetTranslationMakerChecker>(_personFinancialAssetViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personFinancialAsset.PersonPrmKey == 0)
                    {
                        personFinancialAsset.PersonPrmKey = personPrmKey;
                    }
                    context.PersonFinancialAssets.Attach(personFinancialAsset);
                    context.Entry(personFinancialAsset).State = EntityState.Added;
                    person.PersonFinancialAssets.Add(personFinancialAsset);

                    context.PersonFinancialAssetMakerCheckers.Attach(personFinancialAssetMakerChecker);
                    context.Entry(personFinancialAssetMakerChecker).State = EntityState.Added;
                    personFinancialAsset.PersonFinancialAssetMakerCheckers.Add(personFinancialAssetMakerChecker);

                    context.PersonFinancialAssetTranslations.Attach(personFinancialAssetTranslation);
                    context.Entry(personFinancialAssetTranslation).State = EntityState.Added;
                    personFinancialAsset.PersonFinancialAssetTranslations.Add(personFinancialAssetTranslation);

                    context.PersonFinancialAssetTranslationMakerCheckers.Attach(personFinancialAssetTranslationMakerChecker);
                    context.Entry(personFinancialAssetTranslationMakerChecker).State = EntityState.Added;
                    personFinancialAssetTranslation.PersonFinancialAssetTranslationMakerCheckers.Add(personFinancialAssetTranslationMakerChecker);
                }
                else
                {
                    context.PersonFinancialAssetMakerCheckers.Attach(personFinancialAssetMakerChecker);
                    context.Entry(personFinancialAssetMakerChecker).State = EntityState.Added;

                    context.PersonFinancialAssetTranslationMakerCheckers.Attach(personFinancialAssetTranslationMakerChecker);
                    context.Entry(personFinancialAssetTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonFinancialAssetDocumentData(PersonFinancialAssetViewModel _personFinancialAssetViewModel, string _localStoragePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personFinancialAssetViewModel, _entryType);

                //PersonFinancialAsset
                PersonFinancialAssetDocument personFinancialAssetDocument = Mapper.Map<PersonFinancialAssetDocument>(_personFinancialAssetViewModel);
                PersonFinancialAssetDocumentMakerChecker financialAssetDocumentMakerChecker = Mapper.Map<PersonFinancialAssetDocumentMakerChecker>(_personFinancialAssetViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    context.PersonFinancialAssetDocuments.Attach(personFinancialAssetDocument);
                    context.Entry(personFinancialAssetDocument).State = EntityState.Added;
                    personFinancialAsset.PersonFinancialAssetDocuments.Add(personFinancialAssetDocument);

                    context.PersonFinancialAssetDocumentMakerCheckers.Attach(financialAssetDocumentMakerChecker);
                    context.Entry(financialAssetDocumentMakerChecker).State = EntityState.Added;
                    personFinancialAssetDocument.PersonFinancialAssetDocumentMakerCheckers.Add(financialAssetDocumentMakerChecker);

                    // @@@ Check in main person page and delete Existance Condition @@@@//
                    //Check Existence Of Document
                    if(_personFinancialAssetViewModel.PersonFinancialAssetDocumentPrmKey > 0)
                    {
                        //Delete Old Image When New Image Uploaded Or Deleted Existing Image When PhotoUpload is Optional.
                        if ((_oldFileName != _personFinancialAssetViewModel.NameOfFile && _oldFileName != "None") || _personFinancialAssetViewModel.FileCaption=="NotApplicable")
                        {
                            string serverDestinationPath = " ";

                            // Get Destination Path From Database
                            string destinationPath = _localStoragePath;

                            // Check if the destination path contains a tilde ('~') operator.
                            if (destinationPath.IndexOf('~') > -1)
                            {
                                serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                            }

                            // Combine Destination Path with the encrypted file name to get the Full Destination Path
                            _personFinancialAssetViewModel.LocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                            oldRecord.Add("OldRecord");
                            localStorageFilePathListForOldRecord.Add(_personFinancialAssetViewModel.LocalStoragePath);
                            httpPostedFileBaseListForOldRecord.Add(_personFinancialAssetViewModel.PhotoPathFinance);
                        }
                    }
                    
                }
                else
                {
                    context.PersonFinancialAssetDocumentMakerCheckers.Attach(financialAssetDocumentMakerChecker);
                    context.Entry(financialAssetDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonKYCData(PersonKYCDocumentViewModel _personKYCDocumentViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personKYCDocumentViewModel, _entryType);
                _personKYCDocumentViewModel.DocumentDocumentTypePrmKey = personDetailRepository.GetDocumentDocumentTypePrmKeyById(_personKYCDocumentViewModel.DocumentDocumentTypeId);

                personKYCDetail = Mapper.Map<PersonKYCDetail>(_personKYCDocumentViewModel);
                PersonKYCDetailMakerChecker personKYCDetailMakerChecker = Mapper.Map<PersonKYCDetailMakerChecker>(_personKYCDocumentViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personKYCDetail.PersonPrmKey == 0)
                    {
                        personKYCDetail.PersonPrmKey = personPrmKey;
                    }
                    personKYCDetail.PrmKey = 0;
                    personKYCDetailMakerChecker.PersonKYCDetailPrmKey = 0;
                    context.PersonKYCDetails.Attach(personKYCDetail);
                    context.Entry(personKYCDetail).State = EntityState.Added;
                    person.PersonKYCDetails.Add(personKYCDetail);

                    context.PersonKYCDetailMakerCheckers.Attach(personKYCDetailMakerChecker);
                    context.Entry(personKYCDetailMakerChecker).State = EntityState.Added;
                    personKYCDetail.PersonKYCDetailMakerCheckers.Add(personKYCDetailMakerChecker);
                }
                else
                {
                    context.PersonKYCDetailMakerCheckers.Attach(personKYCDetailMakerChecker);
                    context.Entry(personKYCDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonKYCDocumentData(PersonKYCDocumentViewModel _personKYCDocumentViewModel, string _localStoragePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personKYCDocumentViewModel, _entryType);


                PersonKYCDetailDocument personKYCDetailDocument = Mapper.Map<PersonKYCDetailDocument>(_personKYCDocumentViewModel);
                PersonKYCDetailDocumentMakerChecker personKYCDetailDocumentMakerChecker = Mapper.Map<PersonKYCDetailDocumentMakerChecker>(_personKYCDocumentViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    context.PersonKYCDetailDocuments.Attach(personKYCDetailDocument);
                    context.Entry(personKYCDetailDocument).State = EntityState.Added;
                    personKYCDetail.PersonKYCDetailDocuments.Add(personKYCDetailDocument);

                    context.PersonKYCDetailDocumentMakerCheckers.Attach(personKYCDetailDocumentMakerChecker);
                    context.Entry(personKYCDetailDocumentMakerChecker).State = EntityState.Added;
                    personKYCDetailDocument.PersonKYCDetailDocumentMakerCheckers.Add(personKYCDetailDocumentMakerChecker);

                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image When PhotoUpload is Optional.
                    if (_oldFileName != _personKYCDocumentViewModel.NameOfFile && _oldFileName != null)
                    {
                        string serverDestinationPath = " ";

                        // Get Destination Path From Database
                        string destinationPath = _localStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }

                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        _personKYCDocumentViewModel.LocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                        oldRecord.Add("OldRecord");
                        localStorageFilePathListForOldRecord.Add(_personKYCDocumentViewModel.LocalStoragePath);
                        httpPostedFileBaseListForOldRecord.Add(_personKYCDocumentViewModel.PhotoPathKYC);
                    }

                }
                else
                {
                    context.PersonKYCDetailDocumentMakerCheckers.Attach(personKYCDetailDocumentMakerChecker);
                    context.Entry(personKYCDetailDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonGSTRegistrationDetailData(PersonGSTRegistrationDetailViewModel _personGSTRegistrationDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personGSTRegistrationDetailViewModel, _entryType);

                // Get PrmKey By Id
                _personGSTRegistrationDetailViewModel.GSTRegistrationTypePrmKey = accountDetailRepository.GetGSTRegistrationTypePrmKeyById(_personGSTRegistrationDetailViewModel.GSTRegistrationTypeId);
                _personGSTRegistrationDetailViewModel.GSTReturnPeriodicityPrmKey = accountDetailRepository.GetGSTReturnPeriodicityPrmKeyById(_personGSTRegistrationDetailViewModel.GSTReturnPeriodicityId);
                _personGSTRegistrationDetailViewModel.StatePrmKey = personDetailRepository.GetCenterPrmKeyById(_personGSTRegistrationDetailViewModel.StateId);

                 personGSTRegistrationDetail = Mapper.Map<PersonGSTRegistrationDetail>(_personGSTRegistrationDetailViewModel);
                PersonGSTRegistrationDetailMakerChecker personGSTRegistrationDetailMakerChecker = Mapper.Map<PersonGSTRegistrationDetailMakerChecker>(_personGSTRegistrationDetailViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    if (personGSTRegistrationDetail.PersonPrmKey == 0)
                    {
                        personGSTRegistrationDetail.PersonPrmKey = personPrmKey;
                    }
                    context.PersonGSTRegistrationDetails.Attach(personGSTRegistrationDetail);
                    context.Entry(personGSTRegistrationDetail).State = entityState;
                    person.PersonGSTRegistrationDetails.Add(personGSTRegistrationDetail);

                    context.PersonGSTRegistrationDetailMakerCheckers.Attach(personGSTRegistrationDetailMakerChecker);
                    context.Entry(personGSTRegistrationDetailMakerChecker).State = EntityState.Added;
                    personGSTRegistrationDetail.PersonGSTRegistrationDetailMakerCheckers.Add(personGSTRegistrationDetailMakerChecker);
                }


                else
                {
                    context.PersonGSTRegistrationDetailMakerCheckers.Attach(personGSTRegistrationDetailMakerChecker);
                    context.Entry(personGSTRegistrationDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonGSTReturnDocumentData(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel, string _localStoragePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personGSTReturnDocumentViewModel, _entryType);

                PersonGSTReturnDocument personGSTReturnDocument = Mapper.Map<PersonGSTReturnDocument>(_personGSTReturnDocumentViewModel);
                PersonGSTReturnDocumentMakerChecker personGSTReturnDocumentMakerChecker = Mapper.Map<PersonGSTReturnDocumentMakerChecker>(_personGSTReturnDocumentViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    
                    personGSTReturnDocument.PrmKey = 0;
                    personGSTReturnDocumentMakerChecker.PersonGSTReturnDocumentPrmKey = 0;

                    context.PersonGSTReturnDocuments.Attach(personGSTReturnDocument);
                    context.Entry(personGSTReturnDocument).State = EntityState.Added;
                    personGSTRegistrationDetail.PersonGSTReturnDocuments.Add(personGSTReturnDocument);

                    context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerChecker);
                    context.Entry(personGSTReturnDocumentMakerChecker).State = EntityState.Added;
                    personGSTReturnDocument.PersonGSTReturnDocumentMakerCheckers.Add(personGSTReturnDocumentMakerChecker);

                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image When PhotoUpload is Optional.
                    if (_oldFileName != _personGSTReturnDocumentViewModel.NameOfFile && _oldFileName != null)
                    {
                        string serverDestinationPath = " ";

                        // Get Destination Path From Database
                        string destinationPath = _localStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }

                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        _personGSTReturnDocumentViewModel.LocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                        oldRecord.Add("OldRecord");
                        localStorageFilePathListForOldRecord.Add(_personGSTReturnDocumentViewModel.LocalStoragePath);
                        httpPostedFileBaseListForOldRecord.Add(_personGSTReturnDocumentViewModel.PhotoPathGst);
                    }
                }
                else
                {
                    context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerChecker);
                    context.Entry(personGSTReturnDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonImmovableAssetData(PersonImmovableAssetViewModel _personImmovableAssetViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personImmovableAssetViewModel, _entryType);
                //Get PrmKey By Id
                _personImmovableAssetViewModel.ResidenceTypePrmKey = personDetailRepository.GetResidenceTypePrmKeyById(_personImmovableAssetViewModel.ResidenceTypeId);
                _personImmovableAssetViewModel.OwnershipTypePrmKey = personDetailRepository.GetOwnershipTypePrmKeyById(_personImmovableAssetViewModel.OwnershipTypeId);

                 personImmovableAsset = Mapper.Map<PersonImmovableAsset>(_personImmovableAssetViewModel);
                PersonImmovableAssetMakerChecker personImmovableAssetMakerChecker = Mapper.Map<PersonImmovableAssetMakerChecker>(_personImmovableAssetViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personImmovableAsset.PersonPrmKey == 0)
                    {
                        personImmovableAsset.PersonPrmKey = personPrmKey;
                    }
                    context.PersonImmovableAssets.Attach(personImmovableAsset);
                    context.Entry(personImmovableAsset).State = EntityState.Added;
                    person.PersonImmovableAssets.Add(personImmovableAsset);

                    context.PersonImmovableAssetMakerCheckers.Attach(personImmovableAssetMakerChecker);
                    context.Entry(personImmovableAssetMakerChecker).State = EntityState.Added;
                    personImmovableAsset.PersonImmovableAssetMakerCheckers.Add(personImmovableAssetMakerChecker);
                }
                else
                {
                    context.PersonImmovableAssetMakerCheckers.Attach(personImmovableAssetMakerChecker);
                    context.Entry(personImmovableAssetMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonImmovableAssetDocumentData(PersonImmovableAssetViewModel _personImmovableAssetViewModel, string _localStoragePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personImmovableAssetViewModel, _entryType);


                PersonImmovableAssetDocument personImmovableAssetDocument = Mapper.Map<PersonImmovableAssetDocument>(_personImmovableAssetViewModel);
                PersonImmovableAssetDocumentMakerChecker personImmovableAssetDocumentMakerChecker = Mapper.Map<PersonImmovableAssetDocumentMakerChecker>(_personImmovableAssetViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personImmovableAsset.PersonPrmKey == 0)
                    {
                        personImmovableAsset.PersonPrmKey = personPrmKey;
                    }
                    personImmovableAssetDocumentMakerChecker.PersonImmovableAssetDocumentPrmKey = 0;
                    context.PersonImmovableAssetDocuments.Attach(personImmovableAssetDocument);
                    context.Entry(personImmovableAssetDocument).State = EntityState.Added;
                    personImmovableAsset.PersonImmovableAssetDocuments.Add(personImmovableAssetDocument);

                    context.PersonImmovableAssetDocumentMakerCheckers.Attach(personImmovableAssetDocumentMakerChecker);
                    context.Entry(personImmovableAssetDocumentMakerChecker).State = EntityState.Added;
                    personImmovableAssetDocument.PersonImmovableAssetDocumentMakerCheckers.Add(personImmovableAssetDocumentMakerChecker);

                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image When PhotoUpload is Optional.
                    if ((_oldFileName != _personImmovableAssetViewModel.NameOfFile && _oldFileName != "None") || _personImmovableAssetViewModel.FileCaption == "NotApplicable")
                    {
                        string serverDestinationPath = " ";

                        // Get Destination Path From Database
                        string destinationPath = _localStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }

                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        _personImmovableAssetViewModel.LocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                        oldRecord.Add("OldRecord");
                        localStorageFilePathListForOldRecord.Add(_personImmovableAssetViewModel.LocalStoragePath);
                        httpPostedFileBaseListForOldRecord.Add(_personImmovableAssetViewModel.PhotoPathImmovable);
                    }
                }
                else
                {
                    context.PersonImmovableAssetDocumentMakerCheckers.Attach(personImmovableAssetDocumentMakerChecker);
                    context.Entry(personImmovableAssetDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonIncomeTaxDetailData(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personIncomeTaxDetailViewModel, _entryType);

                 personIncomeTaxDetail = Mapper.Map<PersonIncomeTaxDetail>(_personIncomeTaxDetailViewModel);
                PersonIncomeTaxDetailMakerChecker personIncomeTaxDetailMakerChecker = Mapper.Map<PersonIncomeTaxDetailMakerChecker>(_personIncomeTaxDetailViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personIncomeTaxDetail.PersonPrmKey == 0)
                    {
                        personIncomeTaxDetail.PersonPrmKey = personPrmKey;
                    }

                    context.PersonIncomeTaxDetails.Attach(personIncomeTaxDetail);
                    context.Entry(personIncomeTaxDetail).State = EntityState.Added;
                    person.PersonIncomeTaxDetails.Add(personIncomeTaxDetail);

                    context.PersonIncomeTaxDetailMakerCheckers.Attach(personIncomeTaxDetailMakerChecker);
                    context.Entry(personIncomeTaxDetailMakerChecker).State = EntityState.Added;
                    personIncomeTaxDetail.PersonIncomeTaxDetailMakerCheckers.Add(personIncomeTaxDetailMakerChecker);
                }
                else
                {
                    context.PersonIncomeTaxDetailMakerCheckers.Attach(personIncomeTaxDetailMakerChecker);
                    context.Entry(personIncomeTaxDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonIncomeTaxDocumentData(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _localStoragePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personIncomeTaxDetailViewModel, _entryType);


                PersonIncomeTaxDetailDocument personIncomeTaxDetailDocument = Mapper.Map<PersonIncomeTaxDetailDocument>(_personIncomeTaxDetailViewModel);
                PersonIncomeTaxDetailDocumentMakerChecker personIncomeTaxDetailDocumentMakerChecker = Mapper.Map<PersonIncomeTaxDetailDocumentMakerChecker>(_personIncomeTaxDetailViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personIncomeTaxDetail.PersonPrmKey == 0)
                    {
                        personIncomeTaxDetail.PersonPrmKey = personPrmKey;
                    }
                    personIncomeTaxDetailDocumentMakerChecker.PersonIncomeTaxDetailDocumentPrmKey = 0;
                    context.PersonIncomeTaxDetailDocuments.Attach(personIncomeTaxDetailDocument);
                    context.Entry(personIncomeTaxDetailDocument).State = EntityState.Added;
                    personIncomeTaxDetail.PersonIncomeTaxDetailDocuments.Add(personIncomeTaxDetailDocument);

                    context.PersonIncomeTaxDetailDocumentMakerCheckers.Attach(personIncomeTaxDetailDocumentMakerChecker);
                    context.Entry(personIncomeTaxDetailDocumentMakerChecker).State = EntityState.Added;
                    personIncomeTaxDetailDocument.PersonIncomeTaxDetailDocumentMakerCheckers.Add(personIncomeTaxDetailDocumentMakerChecker);

                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image When PhotoUpload is Optional.
                    if ((_oldFileName != _personIncomeTaxDetailViewModel.NameOfFile && _oldFileName != "None") || _personIncomeTaxDetailViewModel.FileCaption == "NotApplicable")
                    {
                        string serverDestinationPath = " ";

                        // Get Destination Path From Database
                        string destinationPath = _localStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }

                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        _personIncomeTaxDetailViewModel.LocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                        oldRecord.Add("OldRecord");
                        localStorageFilePathListForOldRecord.Add(_personIncomeTaxDetailViewModel.LocalStoragePath);
                        httpPostedFileBaseListForOldRecord.Add(_personIncomeTaxDetailViewModel.PhotoPathTax);
                    }
                }
                else
                {
                    context.PersonIncomeTaxDetailDocumentMakerCheckers.Attach(personIncomeTaxDetailDocumentMakerChecker);
                    context.Entry(personIncomeTaxDetailDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }





        public bool AttachPersonInsuranceDetailData(PersonInsuranceDetailViewModel _PersonInsuranceDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_PersonInsuranceDetailViewModel, _entryType);
                //Get PrmKeyBy Id
                _PersonInsuranceDetailViewModel.InsuranceTypePrmKey = personDetailRepository.GetInsuranceTypePrmKeyById(_PersonInsuranceDetailViewModel.InsuranceTypeId);
                _PersonInsuranceDetailViewModel.InsuranceCompanyPrmKey = personDetailRepository.GetInsuranceCompanyPrmKeyById(_PersonInsuranceDetailViewModel.InsuranceCompanyId);

                PersonInsuranceDetail personInsuranceDetail = Mapper.Map<PersonInsuranceDetail>(_PersonInsuranceDetailViewModel);
                PersonInsuranceDetailMakerChecker personInsuranceDetailMakerChecker = Mapper.Map<PersonInsuranceDetailMakerChecker>(_PersonInsuranceDetailViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personInsuranceDetail.PersonPrmKey == 0)
                    {
                        personInsuranceDetail.PersonPrmKey = personPrmKey;
                    }

                    context.PersonInsuranceDetails.Attach(personInsuranceDetail);
                    context.Entry(personInsuranceDetail).State = EntityState.Added;
                    person.PersonInsuranceDetails.Add(personInsuranceDetail);

                    context.PersonInsuranceDetailMakerCheckers.Attach(personInsuranceDetailMakerChecker);
                    context.Entry(personInsuranceDetailMakerChecker).State = EntityState.Added;
                    personInsuranceDetail.PersonInsuranceDetailMakerCheckers.Add(personInsuranceDetailMakerChecker);

                }
                else
                {
                    context.PersonInsuranceDetailMakerCheckers.Attach(personInsuranceDetailMakerChecker);
                    context.Entry(personInsuranceDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonMachineryAssetData(PersonMachineryAssetViewModel _personMachineryAssetViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personMachineryAssetViewModel, _entryType);

                 personMachineryAsset = Mapper.Map<PersonMachineryAsset>(_personMachineryAssetViewModel);
                PersonMachineryAssetMakerChecker personMachineryAssetMakerChecker = Mapper.Map<PersonMachineryAssetMakerChecker>(_personMachineryAssetViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personMachineryAsset.PersonPrmKey == 0)
                    {
                        personMachineryAsset.PersonPrmKey = personPrmKey;
                    }

                    context.PersonMachineryAssets.Attach(personMachineryAsset);
                    context.Entry(personMachineryAsset).State = EntityState.Added;
                    person.PersonMachineryAssets.Add(personMachineryAsset);

                    context.PersonMachineryAssetMakerCheckers.Attach(personMachineryAssetMakerChecker);
                    context.Entry(personMachineryAssetMakerChecker).State = EntityState.Added;
                    personMachineryAsset.PersonMachineryAssetMakerCheckers.Add(personMachineryAssetMakerChecker);
                }
                else
                {
                    context.PersonMachineryAssetMakerCheckers.Attach(personMachineryAssetMakerChecker);
                    context.Entry(personMachineryAssetMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonMachineryAssetDocumentData(PersonMachineryAssetViewModel _personMachineryAssetViewModel, string _localStoragePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personMachineryAssetViewModel, _entryType);

                PersonMachineryAssetDocument personMachineryAssetDocument = Mapper.Map<PersonMachineryAssetDocument>(_personMachineryAssetViewModel);
                PersonMachineryAssetDocumentMakerChecker personMachineryAssetDocumentMakerChecker = Mapper.Map<PersonMachineryAssetDocumentMakerChecker>(_personMachineryAssetViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if(personMachineryAssetDocument.PrmKey == 0)
                    personMachineryAssetDocument.PrmKey = 0;
                    personMachineryAssetDocumentMakerChecker.PersonMachineryAssetDocumentPrmKey = 0;

                    context.PersonMachineryAssetDocuments.Attach(personMachineryAssetDocument);
                    context.Entry(personMachineryAssetDocument).State = EntityState.Added;
                    personMachineryAsset.PersonMachineryAssetDocuments.Add(personMachineryAssetDocument);

                    context.PersonMachineryAssetDocumentMakerCheckers.Attach(personMachineryAssetDocumentMakerChecker);
                    context.Entry(personMachineryAssetDocumentMakerChecker).State = EntityState.Added;
                    personMachineryAssetDocument.PersonMachineryAssetDocumentMakerCheckers.Add(personMachineryAssetDocumentMakerChecker);

                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image When PhotoUpload is Optional.
                    if ((_oldFileName != _personMachineryAssetViewModel.NameOfFile && _oldFileName != "None") || _personMachineryAssetViewModel.FileCaption == "NotApplicable")
                    {
                        string serverDestinationPath = " ";

                        // Get Destination Path From Database
                        string destinationPath = _localStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }

                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        _personMachineryAssetViewModel.LocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                        oldRecord.Add("OldRecord");
                        localStorageFilePathListForOldRecord.Add(_personMachineryAssetViewModel.LocalStoragePath);
                        httpPostedFileBaseListForOldRecord.Add(_personMachineryAssetViewModel.PhotoPathMachinery);
                    }
                }
                else
                {
                    context.PersonMachineryAssetDocumentMakerCheckers.Attach(personMachineryAssetDocumentMakerChecker);
                    context.Entry(personMachineryAssetDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonMovableAssetData(PersonMovableAssetViewModel _personMovableAssetViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personMovableAssetViewModel, _entryType);
                // Get PrmKey By Id
               // _personMovableAssetViewModel.VechicleModelPrmKey = accountDetailRepository.GetVehicleModelPrmKeyById(_personMovableAssetViewModel.VehicleModelId);
                _personMovableAssetViewModel.VehicleVariantPrmKey = accountDetailRepository.GetVehicleVariantPrmKeyById(_personMovableAssetViewModel.VehicleVariantId);

                 personMovableAsset = Mapper.Map<PersonMovableAsset>(_personMovableAssetViewModel);
                PersonMovableAssetMakerChecker personMovableAssetMakerChecker = Mapper.Map<PersonMovableAssetMakerChecker>(_personMovableAssetViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personMovableAsset.PersonPrmKey == 0)
                    {
                        personMovableAsset.PersonPrmKey = personPrmKey;
                    }
                    context.PersonMovableAssets.Attach(personMovableAsset);
                    context.Entry(personMovableAsset).State = EntityState.Added;
                    person.PersonMovableAssets.Add(personMovableAsset);

                    context.PersonMovableAssetMakerCheckers.Attach(personMovableAssetMakerChecker);
                    context.Entry(personMovableAssetMakerChecker).State = EntityState.Added;
                    personMovableAsset.PersonMovableAssetMakerCheckers.Add(personMovableAssetMakerChecker);
                }
                else
                {
                    context.PersonMovableAssetMakerCheckers.Attach(personMovableAssetMakerChecker);
                    context.Entry(personMovableAssetMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonMovableAssetDocumentData(PersonMovableAssetViewModel _personMovableAssetViewModel, string _localStoragePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personMovableAssetViewModel, _entryType);


                PersonMovableAssetDocument personMovableAssetDocument = Mapper.Map<PersonMovableAssetDocument>(_personMovableAssetViewModel);
                PersonMovableAssetDocumentMakerChecker personMovableAssetDocumentMakerChecker = Mapper.Map<PersonMovableAssetDocumentMakerChecker>(_personMovableAssetViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    personMovableAssetDocument.PrmKey = 0;
                    personMovableAssetDocumentMakerChecker.PersonMovableAssetDocumentPrmKey = 0;

                    context.PersonMovableAssetDocuments.Attach(personMovableAssetDocument);
                    context.Entry(personMovableAssetDocument).State = EntityState.Added;
                    personMovableAsset.PersonMovableAssetDocuments.Add(personMovableAssetDocument);

                    context.PersonMovableAssetDocumentMakerCheckers.Attach(personMovableAssetDocumentMakerChecker);
                    context.Entry(personMovableAssetDocumentMakerChecker).State = EntityState.Added;
                    personMovableAssetDocument.PersonMovableAssetDocumentMakerCheckers.Add(personMovableAssetDocumentMakerChecker);

                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image When PhotoUpload is Optional.
                    if ((_oldFileName != _personMovableAssetViewModel.NameOfFile && _oldFileName != "None") || _personMovableAssetViewModel.FileCaption == "NotApplicable")
                    {
                        string serverDestinationPath = " ";

                        // Get Destination Path From Database
                        string destinationPath = _localStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }

                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        _personMovableAssetViewModel.LocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                        oldRecord.Add("OldRecord");
                        localStorageFilePathListForOldRecord.Add(_personMovableAssetViewModel.LocalStoragePath);
                        httpPostedFileBaseListForOldRecord.Add(_personMovableAssetViewModel.PhotoPathMovable);
                    }
                }
                else
                {
                    context.PersonMovableAssetDocumentMakerCheckers.Attach(personMovableAssetDocumentMakerChecker);
                    context.Entry(personMovableAssetDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonPhotoSignData(PersonPhotoSignViewModel _personPhotoSignViewModel, string _entryType)
        {
            try
            {
                long personPhotoSignPrmKey = _personPhotoSignViewModel.PersonPhotoSignPrmKey;
                configurationDetailRepository.SetDefaultValues(_personPhotoSignViewModel, _entryType);

                PersonPhotoSign personPhotoSign = Mapper.Map<PersonPhotoSign>(_personPhotoSignViewModel);
                PersonPhotoSignMakerChecker personPhotoSignMakerChecker = Mapper.Map<PersonPhotoSignMakerChecker>(_personPhotoSignViewModel);


                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    if (personPhotoSign.PersonPrmKey == 0)
                    {
                        personPhotoSign.PersonPrmKey = personPrmKey;
                    }
                    
                        string oldPhotoFileName = GetOldPhotoFileName(personPhotoSignPrmKey);
                        string oldSignFileName = GetOldSignFileName(personPhotoSignPrmKey);
                        string oldPhotoLocalStoragePath = GetOldPhotoLocalStoragePath(personPhotoSignPrmKey);
                        string oldSignLocalStoragePath = GetOldSignLocalStoragePath(personPhotoSignPrmKey);

                        if ((oldPhotoFileName != personPhotoSign.PhotoNameOfFile && oldPhotoLocalStoragePath != null) /*|| _personPhotoSignViewModel.PhotoFileCaption == "NotApplicable"*/)
                        {
                            string serverDestinationPath = " ";

                            // Check if the destination path contains a tilde ('~') operator.
                            if (oldPhotoLocalStoragePath.IndexOf('~') > -1)
                            {
                                serverDestinationPath = HttpContext.Current.Server.MapPath(oldPhotoLocalStoragePath);
                            }

                            oldRecord.Add("OldRecord");

                            localStorageFilePathListForOldRecord.Add(serverDestinationPath);
                            httpPostedFileBaseListForOldRecord.Add(_personPhotoSignViewModel.PhotoPath);
                        }

                        if ((oldSignFileName != personPhotoSign.SignNameOfFile && oldSignLocalStoragePath != null) /*|| _personPhotoSignViewModel.SignFileCaption == "NotApplicable"*/)
                        {
                            string serverDestinationPath = " ";

                            // Check if the destination path contains a tilde ('~') operator.
                            if (oldSignLocalStoragePath.IndexOf('~') > -1)
                            {
                                serverDestinationPath = HttpContext.Current.Server.MapPath(oldSignLocalStoragePath);
                            }

                            oldRecord.Add("OldRecord");
                            localStorageFilePathListForOldRecord.Add(serverDestinationPath);
                            httpPostedFileBaseListForOldRecord.Add(_personPhotoSignViewModel.SignPath);
                        }
                    
                        context.PersonPhotoSigns.Attach(personPhotoSign);
                        context.Entry(personPhotoSign).State = entityState;
                        person.PersonPhotoSigns.Add(personPhotoSign);

                        context.PersonPhotoSignMakerCheckers.Attach(personPhotoSignMakerChecker);
                        context.Entry(personPhotoSignMakerChecker).State = EntityState.Added;
                        personPhotoSign.PersonPhotoSignMakerCheckers.Add(personPhotoSignMakerChecker);
                }

                else
                {
                    context.PersonPhotoSignMakerCheckers.Attach(personPhotoSignMakerChecker);
                    context.Entry(personPhotoSignMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //Get Old PhotoFileName 
        string GetOldPhotoFileName(long _personPhotoSignPrmKey)
        {
            return context.PersonPhotoSigns
                   .Where(c => c.PrmKey == _personPhotoSignPrmKey)
                   .Select(c => c.PhotoNameOfFile).FirstOrDefault();
        }

        //Get Old SignFileName
        string GetOldSignFileName(long _personPhotoSignPrmKey)
        {
            return context.PersonPhotoSigns
                   .Where(c => c.PrmKey == _personPhotoSignPrmKey)
                   .Select(c => c.SignNameOfFile).FirstOrDefault();
        }

        //Get Old PhotoLocalStoragePath
        string GetOldPhotoLocalStoragePath(long _personPhotoSignPrmKey)
        {
            return context.PersonPhotoSigns
                   .Where(c => c.PrmKey == _personPhotoSignPrmKey)
                   .Select(c => c.PhotoLocalStoragePath).FirstOrDefault();
        }

        //Get Old SignLocalStoragePath
        string GetOldSignLocalStoragePath(long _personPhotoSignPrmKey)
        {
            return context.PersonPhotoSigns
                   .Where(c => c.PrmKey == _personPhotoSignPrmKey)
                   .Select(c => c.SignLocalStoragePath).FirstOrDefault();
        }

        public bool AttachPersonSMSAlertData(PersonSMSAlertViewModel _personSMSAlertViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personSMSAlertViewModel, _entryType);
                //Get PrmKey By Id
                _personSMSAlertViewModel.PersonInformationParameterNoticeTypePrmKey = personDetailRepository.GetPersonInformationParameterNoticeTypePrmKeyByNoticeTypeId(_personSMSAlertViewModel.PersonInformationParameterNoticeTypeId);
                _personSMSAlertViewModel.AppLanguagePrmKey = configurationDetailRepository.GetAppLanguagePrmKeyById(_personSMSAlertViewModel.AppLanguageId);

                PersonSMSAlert personSMSAlert = Mapper.Map<PersonSMSAlert>(_personSMSAlertViewModel);
                PersonSMSAlertMakerChecker personSMSAlertMakerChecker = Mapper.Map<PersonSMSAlertMakerChecker>(_personSMSAlertViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personSMSAlert.PersonPrmKey == 0)
                    {
                        personSMSAlert.PersonPrmKey = personPrmKey;
                    }

                    context.PersonSMSAlertes.Attach(personSMSAlert);
                    context.Entry(personSMSAlert).State = EntityState.Added;
                    person.PersonSMSAlertes.Add(personSMSAlert);

                    context.PersonSMSAlertMakerCheckers.Attach(personSMSAlertMakerChecker);
                    context.Entry(personSMSAlertMakerChecker).State = EntityState.Added;
                    personSMSAlert.PersonSMSAlertMakerCheckers.Add(personSMSAlertMakerChecker);
                }
                else
                {
                    context.PersonSMSAlertMakerCheckers.Attach(personSMSAlertMakerChecker);
                    context.Entry(personSMSAlertMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonSocialMediaData(PersonSocialMediaViewModel _personSocialMediaViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personSocialMediaViewModel, _entryType);
                // Get PrmKey By Id Of All Dropdowns
                _personSocialMediaViewModel.SocialMediaPrmKey = managementDetailRepository.GetSocialMediaPrmKeyById(_personSocialMediaViewModel.SocialMediaId);

                PersonSocialMedia personSocialMedia = Mapper.Map<PersonSocialMedia>(_personSocialMediaViewModel);
                PersonSocialMediaMakerChecker personSocialMediaMakerChecker = Mapper.Map<PersonSocialMediaMakerChecker>(_personSocialMediaViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personSocialMedia.PersonPrmKey == 0)
                    {
                        personSocialMedia.PersonPrmKey = personPrmKey;
                    }

                    context.PersonSocialMedias.Attach(personSocialMedia);
                    context.Entry(personSocialMedia).State = EntityState.Added;
                    person.PersonSocialMedias.Add(personSocialMedia);

                    context.PersonSocialMediaMakerCheckers.Attach(personSocialMediaMakerChecker);
                    context.Entry(personSocialMediaMakerChecker).State = EntityState.Added;
                    personSocialMedia.PersonSocialMediaMakerCheckers.Add(personSocialMediaMakerChecker);
                }
                else
                {
                    context.PersonSocialMediaMakerCheckers.Attach(personSocialMediaMakerChecker);
                    context.Entry(personSocialMediaMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //Remove Else Code After Checking
        public bool AttachAgricultureAssetDocumentInLocalStorage(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel,string _agricultureAssetDocumentLocalStoragePath, IEnumerable<PersonAgricultureAssetViewModel> _personAgricultureAssetViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    
                        string serverDestinationPath = " ";

                        // Encrypt Filename With Extension
                        _personAgricultureAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personAgricultureAssetViewModel.PhotoPathAgree.FileName);

                        // Get Destination Path From Database
                        string destinationPath = _agricultureAssetDocumentLocalStoragePath;

                        // Check if the destination path contains a tilde ('~')
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }
                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        string fullDestinationPath = Path.Combine(serverDestinationPath, _personAgricultureAssetViewModel.NameOfFile);
                    
                        // Add New Uploaded Path to filePathList for tracking
                        filePathList.Add("NewUpload");

                        // Add PhotoPath Object Value In httpPostedFileBaseList
                        httpPostedFileBaseList.Add(_personAgricultureAssetViewModel.PhotoPathAgree);
                        
                        // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                        localStorageFilePathList.Add(fullDestinationPath);

                        string localStoragePath = destinationPath + "/" + _personAgricultureAssetViewModel.NameOfFile;

                        // Save the **virtual path** to the database or DataTable
                        _personAgricultureAssetViewModel.LocalStoragePath = localStoragePath;

                }
                //else
                //{
                //    // Get File Details From Database
                //    IEnumerable<PersonAgricultureAssetViewModel> personAgricultureAssetViewModels = (from a in _personAgricultureAssetViewModelList
                //                                                                                     where a.PersonAgricultureAssetDocumentPrmKey == _personAgricultureAssetViewModel.PersonAgricultureAssetDocumentPrmKey
                //                                                                                     select a).ToList();

                //    foreach (PersonAgricultureAssetViewModel personAgricultureAssetViewModel in personAgricultureAssetViewModels)
                //    {
                //        _personAgricultureAssetViewModel.PhotoCopy = personAgricultureAssetViewModel.PhotoCopy;

                //        // Check Existance Of File 
                //        FileInfo file = new FileInfo(personAgricultureAssetViewModel.LocalStoragePath);

                //        if (file.Exists)
                //        {
                //            // Encrypt Filename With Extension
                //            _personAgricultureAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                //            // Combine Local Storage Path With File Name
                //            _personAgricultureAssetViewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), _personAgricultureAssetViewModel.NameOfFile);

                //            // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                //            filePathList.Add(personAgricultureAssetViewModel.LocalStoragePath);

                //            // Add null In httpPostedFileBaseList (Because Of Old File)
                //            httpPostedFileBaseList.Add(null);

                //            // Add New Generated Local Storage Path Which Has Stored In Database.
                //            localStorageFilePathList.Add(_personAgricultureAssetViewModel.LocalStoragePath);
                //        }
                //    }
                //}

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachAgricultureAssetDocumentInDatabaseStorage(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel, IEnumerable<PersonAgricultureAssetViewModel> _personAgricultureAssetViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {

                    Stream photostream = _personAgricultureAssetViewModel.PhotoPathAgree.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((int)photostream.Length);
                    _personAgricultureAssetViewModel.PhotoCopy = imagecode;

                    //_personAgricultureAssetViewModel.NameOfFile = "None";
                    _personAgricultureAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personAgricultureAssetViewModel.PhotoPathAgree.FileName);

                    _personAgricultureAssetViewModel.LocalStoragePath = "None";
                }
                else
                {
                    IEnumerable<PersonAgricultureAssetViewModel> personAgricultureAssetViewModels = (from a in _personAgricultureAssetViewModelList
                                                                                                     where a.PersonAgricultureAssetDocumentPrmKey == _personAgricultureAssetViewModel.PersonAgricultureAssetDocumentPrmKey
                                                                                                     select a).ToList();

                    foreach (PersonAgricultureAssetViewModel personAgricultureAssetViewModel in personAgricultureAssetViewModels)
                    {
                        _personAgricultureAssetViewModel.PhotoCopy = personAgricultureAssetViewModel.PhotoCopy;
                        _personAgricultureAssetViewModel.NameOfFile = personAgricultureAssetViewModel.NameOfFile;
                        _personAgricultureAssetViewModel.LocalStoragePath = personAgricultureAssetViewModel.LocalStoragePath;
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

        public bool AttachBankDetailDocumentInLocalStorage(PersonBankDetailViewModel _personBankDetailViewModel,string _bankStatementLocalStoragePath ,IEnumerable<PersonBankDetailViewModel> _personBankDetailViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                       string serverDestinationPath = " ";

                        // Encrypt Filename With Extension
                        _personBankDetailViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personBankDetailViewModel.PhotoPathBank.FileName);

                        // Get Destination Path From Database
                        string destinationPath = _bankStatementLocalStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }

                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        string fullDestinationPath = Path.Combine(serverDestinationPath, _personBankDetailViewModel.NameOfFile);
                    
                        // Add New Uploaded Path to filePathList for tracking
                        filePathList.Add("NewUpload");

                        // Add PhotoPath Object Value In httpPostedFileBaseList
                        httpPostedFileBaseList.Add(_personBankDetailViewModel.PhotoPathBank);

                        // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                        localStorageFilePathList.Add(fullDestinationPath);

                        string localStoragePath = destinationPath + "/" + _personBankDetailViewModel.NameOfFile;

                        // Save the **virtual path** to the database or DataTable
                        _personBankDetailViewModel.LocalStoragePath = localStoragePath;
                }



                //else
                //{
                //    // Get File Details From Database
                //    IEnumerable<PersonBankDetailViewModel> personBankDetailViewModels = (from a in _personBankDetailViewModelList
                //                                                                         where a.PersonBankDetailPrmKey == _personBankDetailViewModel.PersonBankDetailPrmKey
                //                                                                         select a).ToList();

                //    foreach (PersonBankDetailViewModel personBankDetailViewModel in personBankDetailViewModels)
                //    {
                //        _personBankDetailViewModel.BankStatementPhotoCopy = personBankDetailViewModel.BankStatementPhotoCopy;

                //        // Check Existance Of File 
                //        FileInfo file = new FileInfo(personBankDetailViewModel.BankStatementLocalStoragePath);

                //        if (file.Exists)
                //        {
                //            // Encrypt Filename With Extension
                //            _personBankDetailViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                //            // Combine Local Storage Path With File Name
                //            _personBankDetailViewModel.BankStatementLocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), _personBankDetailViewModel.NameOfFile);

                //            // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                //            filePathList.Add(personBankDetailViewModel.BankStatementLocalStoragePath);

                //            // Add null In httpPostedFileBaseList (Because Of Old File)
                //            httpPostedFileBaseList.Add(null);

                //            // Add New Generated Local Storage Path Which Has Stored In Database.
                //            localStorageFilePathList.Add(_personBankDetailViewModel.BankStatementLocalStoragePath);
                //        }
                //        else
                //        {
                //            _personBankDetailViewModel.NameOfFile = "None";
                //            _personBankDetailViewModel.BankStatementLocalStoragePath = "None";
                //        }
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachBankDetailDocumentInDatabaseStorage(PersonBankDetailViewModel _personBankDetailViewModel, IEnumerable<PersonBankDetailViewModel> _personBankDetailViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    _personBankDetailViewModel.BankBranchPrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_personBankDetailViewModel.BankBranchId);

                    Stream photostream = _personBankDetailViewModel.PhotoPathBank.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _personBankDetailViewModel.PhotoCopy = imagecode;

                    _personBankDetailViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personBankDetailViewModel.PhotoPathBank.FileName);
                    _personBankDetailViewModel.LocalStoragePath = "None";
                }
                else
                {
                    IEnumerable<PersonBankDetailViewModel> personBankDetailViewModels = (from a in _personBankDetailViewModelList
                                                                                         where a.PersonBankDetailPrmKey == _personBankDetailViewModel.PersonBankDetailPrmKey
                                                                                         select a).ToList();

                    foreach (PersonBankDetailViewModel personBankDetailViewModel in personBankDetailViewModels)
                    {
                        _personBankDetailViewModel.PhotoCopy = personBankDetailViewModel.PhotoCopy;
                        _personBankDetailViewModel.NameOfFile = personBankDetailViewModel.NameOfFile;
                        _personBankDetailViewModel.LocalStoragePath = personBankDetailViewModel.LocalStoragePath;
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

        public bool AttachGroupAuthorizedSignatoryInLocalStorage(PersonGroupAuthorizedSignatoryViewModel _personGroupAuthorizedSignatoryViewModel,string _signDocumentLocalStoragePath, IEnumerable<PersonGroupAuthorizedSignatoryViewModel> _personGroupAuthorizedSignatoryViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                      string serverDestinationPath = " ";
                       
                        // Encrypt Filename With Extension
                        _personGroupAuthorizedSignatoryViewModel.SignNameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personGroupAuthorizedSignatoryViewModel.PhotoPathSign.FileName);

                        // Get Destination Path From Database
                        string destinationPath = _signDocumentLocalStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }
                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        string fullDestinationPath = Path.Combine(serverDestinationPath, _personGroupAuthorizedSignatoryViewModel.SignNameOfFile);
                    
                        // Add New Uploaded Path to filePathList for tracking
                        filePathList.Add("NewUpload");

                        // Add PhotoPath Object Value In httpPostedFileBaseList
                        httpPostedFileBaseList.Add(_personGroupAuthorizedSignatoryViewModel.PhotoPathSign);

                        // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                        localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _personGroupAuthorizedSignatoryViewModel.SignNameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _personGroupAuthorizedSignatoryViewModel.SignLocalStoragePath = localStoragePath;

                }


                //else
                //{
                //    // Get File Details From Database
                //    IEnumerable<PersonGroupAuthorizedSignatoryViewModel> personGroupAuthorizedSignatoryViewModels = (from a in _personGroupAuthorizedSignatoryViewModelList
                //                                                                                                     where a.PersonGroupAuthorizedSignatoryPrmKey == _personGroupAuthorizedSignatoryViewModel.PersonGroupAuthorizedSignatoryPrmKey
                //                                                                                                     select a).ToList();

                //    foreach (PersonGroupAuthorizedSignatoryViewModel personGroupAuthorizedSignatoryViewModel in personGroupAuthorizedSignatoryViewModels)
                //    {
                //        _personGroupAuthorizedSignatoryViewModel.Sign = personGroupAuthorizedSignatoryViewModel.Sign;

                //        // Check Existance Of File 
                //        FileInfo file = new FileInfo(personGroupAuthorizedSignatoryViewModel.SignLocalStoragePath);

                //        if (file.Exists)
                //        {
                //            // Encrypt Filename With Extension
                //            _personGroupAuthorizedSignatoryViewModel.SignNameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                //            // Combine Local Storage Path With File Name
                //            _personGroupAuthorizedSignatoryViewModel.SignLocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), _personGroupAuthorizedSignatoryViewModel.SignNameOfFile);

                //            // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                //            filePathList.Add(personGroupAuthorizedSignatoryViewModel.SignLocalStoragePath);

                //            // Add null In httpPostedFileBaseList (Because Of Old File)
                //            httpPostedFileBaseList.Add(null);

                //            // Add New Generated Local Storage Path Which Has Stored In Database.
                //            localStorageFilePathList.Add(_personGroupAuthorizedSignatoryViewModel.SignLocalStoragePath);
                //        }
                //        else
                //        {
                //            _personGroupAuthorizedSignatoryViewModel.SignNameOfFile = "None";
                //            _personGroupAuthorizedSignatoryViewModel.SignLocalStoragePath = "None";
                //        }
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachGroupAuthorizedSignatoryInDatabaseStorage(PersonGroupAuthorizedSignatoryViewModel _personGroupAuthorizedSignatoryViewModel, IEnumerable<PersonGroupAuthorizedSignatoryViewModel> _personGroupAuthorizedSignatoryViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {

                    Stream photostream = _personGroupAuthorizedSignatoryViewModel.PhotoPathSign.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _personGroupAuthorizedSignatoryViewModel.Sign = imagecode;

                    _personGroupAuthorizedSignatoryViewModel.SignNameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personGroupAuthorizedSignatoryViewModel.PhotoPathSign.FileName);
                    _personGroupAuthorizedSignatoryViewModel.SignLocalStoragePath = "None";
                }
                else
                {
                    IEnumerable<PersonGroupAuthorizedSignatoryViewModel> personGroupAuthorizedSignatoryViewModels = (from a in _personGroupAuthorizedSignatoryViewModelList
                                                                                                                     where a.PersonGroupAuthorizedSignatoryPrmKey == _personGroupAuthorizedSignatoryViewModel.PersonGroupAuthorizedSignatoryPrmKey
                                                                                                                     select a).ToList();

                    foreach (PersonGroupAuthorizedSignatoryViewModel personGroupAuthorizedSignatoryViewModel in personGroupAuthorizedSignatoryViewModels)
                    {
                        _personGroupAuthorizedSignatoryViewModel.Sign = personGroupAuthorizedSignatoryViewModel.Sign;
                        _personGroupAuthorizedSignatoryViewModel.SignNameOfFile = personGroupAuthorizedSignatoryViewModel.SignNameOfFile;
                        _personGroupAuthorizedSignatoryViewModel.SignLocalStoragePath = personGroupAuthorizedSignatoryViewModel.SignLocalStoragePath;
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


        public bool AttachFinancialAssetDocumentInLocalStorage(PersonFinancialAssetViewModel _personFinancialAssetViewModel, string _financialAssetDocumentLocalStoragePath, IEnumerable<PersonFinancialAssetViewModel> _personFinancialAssetViewModelList, string _entryType)
        {
            try
            {
                        string serverDestinationPath = " ";

                        // Encrypt Filename With Extension
                        _personFinancialAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personFinancialAssetViewModel.PhotoPathFinance.FileName);

                        // Get Destination Path From Database
                        string destinationPath = _financialAssetDocumentLocalStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }
                            // Combine Destination Path with the encrypted file name to get the Full Destination Path
                            string fullDestinationPath = Path.Combine(serverDestinationPath, _personFinancialAssetViewModel.NameOfFile);

                            // Add New Uploaded Path to filePathList for tracking
                            filePathList.Add("NewUpload");

                             // Add PhotoPath Object Value In httpPostedFileBaseList
                             httpPostedFileBaseList.Add(_personFinancialAssetViewModel.PhotoPathFinance);

                              // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                             localStorageFilePathList.Add(fullDestinationPath);

                            string localStoragePath= destinationPath + "/" + _personFinancialAssetViewModel.NameOfFile;
                            
                             // Save the **virtual path** to the database or DataTable
                            _personFinancialAssetViewModel.LocalStoragePath = localStoragePath; 
                

                //else
                //{
                //    // Get File Details From Database
                //    IEnumerable<PersonFinancialAssetViewModel> personFinancialAssetViewModels = (from a in _personFinancialAssetViewModelList
                //                                                                                 where a.PersonFinancialAssetDocumentPrmKey == _personFinancialAssetViewModel.PersonFinancialAssetDocumentPrmKey
                //                                                                                 select a).ToList();
                //    foreach (PersonFinancialAssetViewModel personFinancialAssetViewModel in personFinancialAssetViewModels)
                //    {
                //        _personFinancialAssetViewModel.PhotoCopy = personFinancialAssetViewModel.PhotoCopy;

                //        // Check Existance Of File 
                //        FileInfo file = new FileInfo(personFinancialAssetViewModel.LocalStoragePath);

                //        if (file.Exists)
                //        {
                //            // Encrypt Filename With Extension
                //            _personFinancialAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                //            // Combine Local Storage Path With File Name
                //            _personFinancialAssetViewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), _personFinancialAssetViewModel.NameOfFile);

                //            // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                //            filePathList.Add(personFinancialAssetViewModel.LocalStoragePath);

                //            // Add null In httpPostedFileBaseList (Because Of Old File)
                //            httpPostedFileBaseList.Add(null);

                //            // Add New Generated Local Storage Path Which Has Stored In Database.
                //            localStorageFilePathList.Add(_personFinancialAssetViewModel.LocalStoragePath);
                //        }
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


        public bool AttachFinancialAssetDocumentInDatabaseStorage(PersonFinancialAssetViewModel _personFinancialAssetViewModel, IEnumerable<PersonFinancialAssetViewModel> _personFinancialAssetViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    Stream photostream = _personFinancialAssetViewModel.PhotoPathFinance.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _personFinancialAssetViewModel.PhotoCopy = imagecode;

                    _personFinancialAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personFinancialAssetViewModel.PhotoPathFinance.FileName);

                    _personFinancialAssetViewModel.LocalStoragePath = "None";
                }
                else
                {

                    IEnumerable<PersonFinancialAssetViewModel> personFinancialAssetViewModels = (from a in _personFinancialAssetViewModelList
                                                                                                 where a.PersonFinancialAssetDocumentPrmKey == _personFinancialAssetViewModel.PersonFinancialAssetDocumentPrmKey
                                                                                                 select a).ToList();

                    foreach (PersonFinancialAssetViewModel personFinancialAssetViewModel in personFinancialAssetViewModels)
                    {
                        _personFinancialAssetViewModel.PhotoCopy = personFinancialAssetViewModel.PhotoCopy;
                        _personFinancialAssetViewModel.NameOfFile = personFinancialAssetViewModel.NameOfFile;
                        _personFinancialAssetViewModel.LocalStoragePath = personFinancialAssetViewModel.LocalStoragePath;
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

        public bool AttachKYCDocumentInLocalStorage(PersonKYCDocumentViewModel _personKYCDocumentViewModel,string _kYCDocumentLocalStoragePath, IEnumerable<PersonKYCDocumentViewModel> _personKYCDocumentViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _personKYCDocumentViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personKYCDocumentViewModel.PhotoPathKYC.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _kYCDocumentLocalStoragePath;

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _personKYCDocumentViewModel.NameOfFile);

                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_personKYCDocumentViewModel.PhotoPathKYC);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _personKYCDocumentViewModel.NameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _personKYCDocumentViewModel.LocalStoragePath = localStoragePath;
                }

                //else
                //{
                //    IEnumerable<PersonKYCDocumentViewModel> personKYCDocumentViewModels = (from a in _personKYCDocumentViewModelList
                //                                                                           where a.PersonKYCDocumentPrmKey == _personKYCDocumentViewModel.PersonKYCDocumentPrmKey
                //                                                                           select a).ToList();

                //    foreach (PersonKYCDocumentViewModel personKYCDocumentViewModel in personKYCDocumentViewModels)
                //    {
                //        _personKYCDocumentViewModel.DocumentPhotoCopy = personKYCDocumentViewModel.DocumentPhotoCopy;

                //        // Check Existance Of File 
                //        FileInfo file = new FileInfo(personKYCDocumentViewModel.LocalStoragePath);

                //        if (file.Exists)
                //        {
                //            // Encrypt Filename With Extension
                //            _personKYCDocumentViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                //            // Combine Local Storage Path With File Name
                //            _personKYCDocumentViewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), _personKYCDocumentViewModel.NameOfFile);

                //            // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                //            filePathList.Add(personKYCDocumentViewModel.LocalStoragePath);

                //            // Add null In httpPostedFileBaseList (Because Of Old File)
                //            httpPostedFileBaseList.Add(null);

                //            // Add New Generated Local Storage Path Which Has Stored In Database.
                //            localStorageFilePathList.Add(_personKYCDocumentViewModel.LocalStoragePath);
                //        }
                //        else
                //        {
                //            _personKYCDocumentViewModel.NameOfFile = "None";
                //            _personKYCDocumentViewModel.LocalStoragePath = "None";
                //        }
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachKYCDocumentInDatabaseStorage(PersonKYCDocumentViewModel _personKYCDocumentViewModel, IEnumerable<PersonKYCDocumentViewModel> _personKYCDocumentViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    Stream photostream = _personKYCDocumentViewModel.PhotoPathKYC.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _personKYCDocumentViewModel.PhotoCopy = imagecode;

                    _personKYCDocumentViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personKYCDocumentViewModel.PhotoPathKYC.FileName);

                    _personKYCDocumentViewModel.LocalStoragePath = "None";
                }
                else
                {

                    IEnumerable<PersonKYCDocumentViewModel> personKYCDocumentViewModels = (from a in _personKYCDocumentViewModelList
                                                                                           where a.PersonKYCDetailDocumentPrmKey == _personKYCDocumentViewModel.PersonKYCDetailDocumentPrmKey
                                                                                           select a).ToList();

                    foreach (PersonKYCDocumentViewModel personKYCDocumentViewModel in personKYCDocumentViewModels)
                    {
                        _personKYCDocumentViewModel.PhotoCopy = personKYCDocumentViewModel.PhotoCopy;
                        _personKYCDocumentViewModel.NameOfFile = personKYCDocumentViewModel.NameOfFile;
                        _personKYCDocumentViewModel.LocalStoragePath = personKYCDocumentViewModel.LocalStoragePath;
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

        public bool AttachGSTDocumentInLocalStorage(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel,string _gSTDocumentLocalStoragePath, IEnumerable<PersonGSTReturnDocumentViewModel> _personGSTReturnDocumentViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _personGSTReturnDocumentViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personGSTReturnDocumentViewModel.PhotoPathGst.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _gSTDocumentLocalStoragePath;

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _personGSTReturnDocumentViewModel.NameOfFile);
                    
                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_personGSTReturnDocumentViewModel.PhotoPathGst);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _personGSTReturnDocumentViewModel.NameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _personGSTReturnDocumentViewModel.LocalStoragePath = localStoragePath;
                }

                //else
                //{
                //    IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModels = (from a in _personGSTReturnDocumentViewModelList
                //                                                                                       where a.PersonGSTReturnDocumentPrmKey == _personGSTReturnDocumentViewModel.PersonGSTReturnDocumentPrmKey
                //                                                                                       select a).ToList();

                //    foreach (PersonGSTReturnDocumentViewModel personGSTReturnDocumentViewModel in personGSTReturnDocumentViewModels)
                //    {
                //        _personGSTReturnDocumentViewModel.PhotoCopy = personGSTReturnDocumentViewModel.PhotoCopy;

                //        // Check Existance Of File 
                //        FileInfo file = new FileInfo(personGSTReturnDocumentViewModel.LocalStoragePath);

                //        file.Encrypt();
                //        if (file.Exists)
                //        {
                //            // Encrypt Filename With Extension
                //            _personGSTReturnDocumentViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                //            // Combine Local Storage Path With File Name
                //            _personGSTReturnDocumentViewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), _personGSTReturnDocumentViewModel.NameOfFile);

                //            // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                //            filePathList.Add(personGSTReturnDocumentViewModel.LocalStoragePath);

                //            // Add null In httpPostedFileBaseList (Because Of Old File)
                //            httpPostedFileBaseList.Add(null);

                //            // Add New Generated Local Storage Path Which Has Stored In Database.
                //            localStorageFilePathList.Add(_personGSTReturnDocumentViewModel.LocalStoragePath);
                //        }
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachGSTDocumentInDatabaseStorage(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel, IEnumerable<PersonGSTReturnDocumentViewModel> _personGSTReturnDocumentViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    Stream photostream = _personGSTReturnDocumentViewModel.PhotoPathGst.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _personGSTReturnDocumentViewModel.PhotoCopy = imagecode;

                    _personGSTReturnDocumentViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personGSTReturnDocumentViewModel.PhotoPathGst.FileName);

                    _personGSTReturnDocumentViewModel.LocalStoragePath = "None";
                }
                else
                {
                    IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModels = (from a in _personGSTReturnDocumentViewModelList
                                                                                                       where a.PersonGSTReturnDocumentPrmKey == _personGSTReturnDocumentViewModel.PersonGSTReturnDocumentPrmKey
                                                                                                       select a).ToList();

                    foreach (PersonGSTReturnDocumentViewModel personGSTReturnDocumentViewModel in personGSTReturnDocumentViewModels)
                    {
                        _personGSTReturnDocumentViewModel.PhotoCopy = personGSTReturnDocumentViewModel.PhotoCopy;
                        _personGSTReturnDocumentViewModel.NameOfFile = personGSTReturnDocumentViewModel.NameOfFile;
                        _personGSTReturnDocumentViewModel.LocalStoragePath = personGSTReturnDocumentViewModel.LocalStoragePath;
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

        public bool AttachImmovableAssetDocumentInLocalStorage(PersonImmovableAssetViewModel _personImmovableAssetViewModel,string _immovableAssetDocumentLocalStoragePath, IEnumerable<PersonImmovableAssetViewModel> _personImmovableAssetViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _personImmovableAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personImmovableAssetViewModel.PhotoPathImmovable.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _immovableAssetDocumentLocalStoragePath;

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _personImmovableAssetViewModel.NameOfFile);
                    
                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_personImmovableAssetViewModel.PhotoPathImmovable);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _personImmovableAssetViewModel.NameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _personImmovableAssetViewModel.LocalStoragePath = localStoragePath;
                }

                //else
                //{
                //    IEnumerable<PersonImmovableAssetViewModel> personImmovableAssetViewModels = (from a in _personImmovableAssetViewModelList
                //                                                                                 where a.PersonImmovableAssetDocumentPrmKey == _personImmovableAssetViewModel.PersonImmovableAssetDocumentPrmKey
                //                                                                                 select a).ToList();

                //    foreach (PersonImmovableAssetViewModel personImmovableAssetViewModel in personImmovableAssetViewModels)
                //    {
                //        _personImmovableAssetViewModel.PhotoCopy = personImmovableAssetViewModel.PhotoCopy;

                //        // Check Existance Of File 
                //        FileInfo file = new FileInfo(personImmovableAssetViewModel.LocalStoragePath);

                //        file.Encrypt();
                //        if (file.Exists)
                //        {
                //            // Encrypt Filename With Extension
                //            _personImmovableAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                //            // Combine Local Storage Path With File Name
                //            _personImmovableAssetViewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), _personImmovableAssetViewModel.NameOfFile);

                //            // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                //            filePathList.Add(personImmovableAssetViewModel.LocalStoragePath);

                //            // Add null In httpPostedFileBaseList (Because Of Old File)
                //            httpPostedFileBaseList.Add(null);

                //            // Add New Generated Local Storage Path Which Has Stored In Database.
                //            localStorageFilePathList.Add(_personImmovableAssetViewModel.LocalStoragePath);
                //        }
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachImmovableAssetDocumentInDatabaseStorage(PersonImmovableAssetViewModel _personImmovableAssetViewModel, IEnumerable<PersonImmovableAssetViewModel> _personImmovableAssetViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    Stream photostream = _personImmovableAssetViewModel.PhotoPathImmovable.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _personImmovableAssetViewModel.PhotoCopy = imagecode;

                    _personImmovableAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personImmovableAssetViewModel.PhotoPathImmovable.FileName);

                    _personImmovableAssetViewModel.LocalStoragePath = "None";
                }
                else
                {
                    IEnumerable<PersonImmovableAssetViewModel> personImmovableAssetViewModels = (from a in _personImmovableAssetViewModelList
                                                                                                 where a.PersonImmovableAssetDocumentPrmKey == _personImmovableAssetViewModel.PersonImmovableAssetDocumentPrmKey
                                                                                                 select a).ToList();

                    foreach (PersonImmovableAssetViewModel personImmovableAssetViewModel in personImmovableAssetViewModels)
                    {
                        _personImmovableAssetViewModel.PhotoCopy = personImmovableAssetViewModel.PhotoCopy;
                        _personImmovableAssetViewModel.NameOfFile = personImmovableAssetViewModel.NameOfFile;
                        _personImmovableAssetViewModel.LocalStoragePath = personImmovableAssetViewModel.LocalStoragePath;
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

        public bool AttachIncomeTaxDetailDocumentInLocalStorage(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel,string _incomeTaxDocumentLocalStoragePath, IEnumerable<PersonIncomeTaxDetailViewModel> _personIncomeTaxDetailViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _personIncomeTaxDetailViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personIncomeTaxDetailViewModel.PhotoPathTax.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _incomeTaxDocumentLocalStoragePath;

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _personIncomeTaxDetailViewModel.NameOfFile);
                    
                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_personIncomeTaxDetailViewModel.PhotoPathTax);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _personIncomeTaxDetailViewModel.NameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _personIncomeTaxDetailViewModel.LocalStoragePath = localStoragePath;
                }
                ////else
                ////{
                ////    IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModels = (from a in _personIncomeTaxDetailViewModelList
                ////                                                                                   where a.PersonIncomeTaxDetailPrmKey == _personIncomeTaxDetailViewModel.PersonIncomeTaxDetailPrmKey
                ////                                                                                   select a).ToList();

                ////    foreach (PersonIncomeTaxDetailViewModel personIncomeTaxDetailViewModel in personIncomeTaxDetailViewModels)
                ////    {
                ////        _personIncomeTaxDetailViewModel.AssesmentOrderPhotoCopy = personIncomeTaxDetailViewModel.AssesmentOrderPhotoCopy;

                ////        // Check Existance Of File 
                ////        FileInfo file = new FileInfo(personIncomeTaxDetailViewModel.AssesmentOrderLocalStoragePath);

                ////        if (file.Exists)
                ////        {
                ////            // Encrypt Filename With Extension
                ////            _personIncomeTaxDetailViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                ////            // Combine Local Storage Path With File Name
                ////            _personIncomeTaxDetailViewModel.AssesmentOrderLocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), _personIncomeTaxDetailViewModel.NameOfFile);

                ////            // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                ////            filePathList.Add(personIncomeTaxDetailViewModel.AssesmentOrderLocalStoragePath);

                ////            // Add null In httpPostedFileBaseList (Because Of Old File)
                ////            httpPostedFileBaseList.Add(null);

                ////            // Add New Generated Local Storage Path Which Has Stored In Database.
                ////            localStorageFilePathList.Add(_personIncomeTaxDetailViewModel.AssesmentOrderLocalStoragePath);
                ////        }
                ////        else
                ////        {
                ////            _personIncomeTaxDetailViewModel.NameOfFile = "None";
                ////            _personIncomeTaxDetailViewModel.AssesmentOrderLocalStoragePath = "None";
                ////        }
                ////    }
                ////}

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachIncomeTaxDetailDocumentInDatabaseStorage(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, IEnumerable<PersonIncomeTaxDetailViewModel> _personIncomeTaxDetailViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    Stream photostream = _personIncomeTaxDetailViewModel.PhotoPathTax.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _personIncomeTaxDetailViewModel.PhotoCopy = imagecode;

                    _personIncomeTaxDetailViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personIncomeTaxDetailViewModel.PhotoPathTax.FileName);

                    _personIncomeTaxDetailViewModel.LocalStoragePath = "None";
                }
                else
                {
                    IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModels = (from a in _personIncomeTaxDetailViewModelList
                                                                                                   where a.PersonIncomeTaxDetailPrmKey == _personIncomeTaxDetailViewModel.PersonIncomeTaxDetailPrmKey
                                                                                                   select a).ToList();

                    foreach (PersonIncomeTaxDetailViewModel personIncomeTaxDetailViewModel in personIncomeTaxDetailViewModels)
                    {
                        _personIncomeTaxDetailViewModel.PhotoCopy = personIncomeTaxDetailViewModel.PhotoCopy;
                        _personIncomeTaxDetailViewModel.NameOfFile = personIncomeTaxDetailViewModel.NameOfFile;
                        _personIncomeTaxDetailViewModel.LocalStoragePath = personIncomeTaxDetailViewModel.LocalStoragePath;
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

        public bool AttachMachineryAssetDocumentInLocalStorage(PersonMachineryAssetViewModel _personMachineryAssetViewModel,string _machineryAssetDocumentLocalStoragePath, IEnumerable<PersonMachineryAssetViewModel> _personMachineryAssetViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _personMachineryAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personMachineryAssetViewModel.PhotoPathMachinery.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _machineryAssetDocumentLocalStoragePath;

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _personMachineryAssetViewModel.NameOfFile);
                    
                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_personMachineryAssetViewModel.PhotoPathMachinery);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _personMachineryAssetViewModel.NameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _personMachineryAssetViewModel.LocalStoragePath = localStoragePath;
                }

                //else
                //{
                //    IEnumerable<PersonMachineryAssetViewModel> personMachineryAssetViewModels = (from a in _personMachineryAssetViewModelList
                //                                                                                 where a.PersonMachineryAssetDocumentPrmKey == _personMachineryAssetViewModel.PersonMachineryAssetDocumentPrmKey
                //                                                                                 select a).ToList();

                //    foreach (PersonMachineryAssetViewModel personMachineryAssetViewModel in personMachineryAssetViewModels)
                //    {
                //        _personMachineryAssetViewModel.PhotoCopy = personMachineryAssetViewModel.PhotoCopy;

                //        // Check Existance Of File 
                //        FileInfo file = new FileInfo(personMachineryAssetViewModel.LocalStoragePath);

                //        file.Encrypt();
                //        if (file.Exists)
                //        {
                //            // Encrypt Filename With Extension
                //            _personMachineryAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                //            // Combine Local Storage Path With File Name
                //            _personMachineryAssetViewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), _personMachineryAssetViewModel.NameOfFile);

                //            // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                //            filePathList.Add(personMachineryAssetViewModel.LocalStoragePath);

                //            // Add null In httpPostedFileBaseList (Because Of Old File)
                //            httpPostedFileBaseList.Add(null);

                //            // Add New Generated Local Storage Path Which Has Stored In Database.
                //            localStorageFilePathList.Add(_personMachineryAssetViewModel.LocalStoragePath);
                //        }
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachMachineryAssetDocumentInDatabaseStorage(PersonMachineryAssetViewModel _personMachineryAssetViewModel, IEnumerable<PersonMachineryAssetViewModel> _personMachineryAssetViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    Stream photostream = _personMachineryAssetViewModel.PhotoPathMachinery.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _personMachineryAssetViewModel.PhotoCopy = imagecode;

                    _personMachineryAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personMachineryAssetViewModel.PhotoPathMachinery.FileName);

                    _personMachineryAssetViewModel.LocalStoragePath = "None";
                }
                else
                {
                    IEnumerable<PersonMachineryAssetViewModel> personMachineryAssetViewModels = (from a in _personMachineryAssetViewModelList
                                                                                                 where a.PersonMachineryAssetDocumentPrmKey == _personMachineryAssetViewModel.PersonMachineryAssetDocumentPrmKey
                                                                                                 select a).ToList();

                    foreach (PersonMachineryAssetViewModel personMachineryAssetViewModel in personMachineryAssetViewModels)
                    {
                        _personMachineryAssetViewModel.PhotoCopy = personMachineryAssetViewModel.PhotoCopy;
                        _personMachineryAssetViewModel.NameOfFile = personMachineryAssetViewModel.NameOfFile;
                        _personMachineryAssetViewModel.LocalStoragePath = personMachineryAssetViewModel.LocalStoragePath;
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

        public bool AttachMovableAssetDocumentInLocalStorage(PersonMovableAssetViewModel _personMovableAssetViewModel,string _movableAssetDocumentLocalStoragePath, IEnumerable<PersonMovableAssetViewModel> _personMovableAssetViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _personMovableAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personMovableAssetViewModel.PhotoPathMovable.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _movableAssetDocumentLocalStoragePath;

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _personMovableAssetViewModel.NameOfFile);
                    
                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_personMovableAssetViewModel.PhotoPathMovable);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _personMovableAssetViewModel.NameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _personMovableAssetViewModel.LocalStoragePath = localStoragePath;
                }

                //else
                //{
                //    IEnumerable<PersonMovableAssetViewModel> personMovableAssetViewModels = (from a in _personMovableAssetViewModelList
                //                                                                             where a.PersonMovableAssetDocumentPrmKey == _personMovableAssetViewModel.PersonMovableAssetDocumentPrmKey
                //                                                                             select a).ToList();

                //    foreach (PersonMovableAssetViewModel personMovableAssetViewModel in personMovableAssetViewModels)
                //    {
                //        _personMovableAssetViewModel.PhotoCopy = personMovableAssetViewModel.PhotoCopy;

                //        // Check Existance Of File 
                //        FileInfo file = new FileInfo(personMovableAssetViewModel.LocalStoragePath);

                //        if (file.Exists)
                //        {
                //            // Encrypt Filename With Extension
                //            _personMovableAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                //            // Combine Local Storage Path With File Name
                //            _personMovableAssetViewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), _personMovableAssetViewModel.NameOfFile);

                //            // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                //            filePathList.Add(personMovableAssetViewModel.LocalStoragePath);

                //            // Add null In httpPostedFileBaseList (Because Of Old File)
                //            httpPostedFileBaseList.Add(null);

                //            // Add New Generated Local Storage Path Which Has Stored In Database.
                //            localStorageFilePathList.Add(_personMovableAssetViewModel.LocalStoragePath);
                //        }
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachMovableAssetDocumentInDatabaseStorage(PersonMovableAssetViewModel _personMovableAssetViewModel, IEnumerable<PersonMovableAssetViewModel> _personMovableAssetViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    Stream photostream = _personMovableAssetViewModel.PhotoPathMovable.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _personMovableAssetViewModel.PhotoCopy = imagecode;

                    _personMovableAssetViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personMovableAssetViewModel.PhotoPathMovable.FileName);

                    _personMovableAssetViewModel.LocalStoragePath = "None";
                }
                else
                {
                    IEnumerable<PersonMovableAssetViewModel> personMovableAssetViewModels = (from a in _personMovableAssetViewModelList
                                                                                             where a.PersonMovableAssetDocumentPrmKey == _personMovableAssetViewModel.PersonMovableAssetDocumentPrmKey
                                                                                             select a).ToList();

                    foreach (PersonMovableAssetViewModel personMovableAssetViewModel in personMovableAssetViewModels)
                    {
                        _personMovableAssetViewModel.PhotoCopy = personMovableAssetViewModel.PhotoCopy;
                        _personMovableAssetViewModel.NameOfFile = personMovableAssetViewModel.NameOfFile;
                        _personMovableAssetViewModel.LocalStoragePath = personMovableAssetViewModel.LocalStoragePath;
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

        public bool AttachPhotoDocumentInLocalStorage(PersonPhotoSignViewModel _personPhotoSignViewModel, string _photoDocumentLocalStoragePath, PersonPhotoSignViewModel personPhotoSignViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    //if (_entryType == StringLiteralValue.Amend && _personPhotoSignViewModel.PhotoPath != null)
                    //{
                    //    oldRecord.Add("OldRecord");
                    //    string oldPhotoPath = HttpContext.Current.Server.MapPath(personPhotoSignViewModel.PhotoLocalStoragePath);
                    //    localStorageFilePathListForOldRecord.Add(oldPhotoPath);
                    //    httpPostedFileBaseListForOldRecord.Add(personPhotoSignViewModel.PhotoPath);
                    //}

                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _personPhotoSignViewModel.PhotoNameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personPhotoSignViewModel.PhotoPath.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _photoDocumentLocalStoragePath;

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _personPhotoSignViewModel.PhotoNameOfFile);

                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_personPhotoSignViewModel.PhotoPath);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _personPhotoSignViewModel.PhotoNameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _personPhotoSignViewModel.PhotoLocalStoragePath = localStoragePath;
                }

                //else
                //{
                //    _personPhotoSignViewModel.PhotoCopy = personPhotoSignViewModel.PhotoCopy;

                //    // Check Existance Of File 
                //    FileInfo file = new FileInfo(personPhotoSignViewModel.PhotoLocalStoragePath);
                //    File.Delete(personPhotoSignViewModel.PhotoLocalStoragePath);
                //    if (file.Exists)
                //    {
                //        // Encrypt Filename With Extension
                //        _personPhotoSignViewModel.PhotoNameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                //        // Combine Local Storage Path With File Name
                //        _personPhotoSignViewModel.PhotoLocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), personPhotoSignViewModel.PhotoNameOfFile);

                //        // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                //        filePathList.Add(personPhotoSignViewModel.PhotoLocalStoragePath);

                //        // Add null In httpPostedFileBaseList (Because Of Old File)
                //        httpPostedFileBaseList.Add(null);

                //        // Add New Generated Local Storage Path Which Has Stored In Database.
                //        localStorageFilePathList.Add(_personPhotoSignViewModel.PhotoLocalStoragePath);

                //    }

                //}

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }
        public bool AttachPhotoDocumentInDatabaseStorage(PersonPhotoSignViewModel _personPhotoSignViewModel, PersonPhotoSignViewModel personPhotoSignViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    Stream photostream = _personPhotoSignViewModel.PhotoPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _personPhotoSignViewModel.PhotoCopy = imagecode;

                    _personPhotoSignViewModel.PhotoNameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personPhotoSignViewModel.PhotoPath.FileName);

                    _personPhotoSignViewModel.PhotoLocalStoragePath = "None";
                }
                else
                {

                    _personPhotoSignViewModel.PhotoCopy = personPhotoSignViewModel.PhotoCopy;
                    _personPhotoSignViewModel.PhotoNameOfFile = personPhotoSignViewModel.PhotoNameOfFile;
                    _personPhotoSignViewModel.PhotoLocalStoragePath = personPhotoSignViewModel.PhotoLocalStoragePath;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSignDocumentInLocalStorage(PersonPhotoSignViewModel _personPhotoSignViewModel, string _signDocumentLocalStoragePath, PersonPhotoSignViewModel personPhotoSignViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    //if (_entryType == StringLiteralValue.Amend && _personPhotoSignViewModel.SignPath != null)
                    //{
                    //    oldRecord.Add("OldRecord");
                    //    string oldSignpath = HttpContext.Current.Server.MapPath(personPhotoSignViewModel.SignLocalStoragePath);
                    //    localStorageFilePathListForOldRecord.Add(oldSignpath);
                    //    httpPostedFileBaseListForOldRecord.Add(personPhotoSignViewModel.SignPath);
                    //}
                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _personPhotoSignViewModel.SignNameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personPhotoSignViewModel.SignPath.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _signDocumentLocalStoragePath;

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _personPhotoSignViewModel.SignNameOfFile);

                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_personPhotoSignViewModel.SignPath);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _personPhotoSignViewModel.SignNameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _personPhotoSignViewModel.SignLocalStoragePath = localStoragePath;
                }
                //else
                //{
                //    _personPhotoSignViewModel.PhotoCopy = personPhotoSignViewModel.PhotoCopy;

                //    // Check Existance Of File 
                //    FileInfo file = new FileInfo(personPhotoSignViewModel.PhotoLocalStoragePath);

                //    if (file.Exists)
                //    {
                //        // Encrypt Filename With Extension
                //        _personPhotoSignViewModel.SignNameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                //        // Combine Local Storage Path With File Name
                //        _personPhotoSignViewModel.SignLocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), personPhotoSignViewModel.SignNameOfFile);

                //        // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                //        filePathList.Add(personPhotoSignViewModel.SignLocalStoragePath);

                //        // Add null In httpPostedFileBaseList (Because Of Old File)
                //        httpPostedFileBaseList.Add(null);

                //        // Add New Generated Local Storage Path Which Has Stored In Database.
                //        localStorageFilePathList.Add(_personPhotoSignViewModel.SignLocalStoragePath);

                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSignDocumentInDatabaseStorage(PersonPhotoSignViewModel _personPhotoSignViewModel, PersonPhotoSignViewModel personPhotoSignViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    Stream photostream = _personPhotoSignViewModel.SignPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _personPhotoSignViewModel.PersonSign = imagecode;


                    _personPhotoSignViewModel.SignNameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personPhotoSignViewModel.SignPath.FileName);

                    _personPhotoSignViewModel.SignLocalStoragePath = "None";
                }
                else
                {
                    _personPhotoSignViewModel.PersonSign = personPhotoSignViewModel.PersonSign;
                    _personPhotoSignViewModel.SignNameOfFile = personPhotoSignViewModel.SignNameOfFile;
                    _personPhotoSignViewModel.SignLocalStoragePath = personPhotoSignViewModel.SignLocalStoragePath;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public byte GetGenderPrmKey()
        {
            return context.Genders
                .Where(c => c.SysNameOfGender == "OTH")
                .Select(c => c.PrmKey).FirstOrDefault();
        }

        private bool SaveLocalStorageDocument()
        {
            try
            {
                for (byte i = 0; i < filePathList.Count; i++)
                {
                    //If New File Uploaded
                    if (filePathList[i] == "NewUpload")
                    {
                        //New Uploaded File Copy Uploaded File To Destination Folder
                        httpPostedFileBaseList[i].SaveAs(localStorageFilePathList[i]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }

        private bool DeleteLocalStorageDocument()
        {
            try
            {
                for (byte i = 0; i < filePathList.Count; i++)
                {
                    //If New File Uploaded
                    if (filePathList[i] == "NewUpload")
                    {
                        if (File.Exists(localStorageFilePathList[i]))
                            File.Delete(localStorageFilePathList[i]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }
        
        //private bool ResetOldLocalStorageDocument()
        //{
        //    try
        //    {
        //        for (byte i = 0; i < oldRecord.Count; i++)
        //        {
        //            //If New File Uploaded
        //            if (oldRecord[i] == "OldRecord")
        //            {
        //                //New Uploaded File Copy Uploaded File To Destination Folder
        //                httpPostedFileBaseListForOldRecord[i].SaveAs(localStorageFilePathListForOldRecord[i]);
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.Message;
        //        return false;
        //    }
        //}

        public bool DeleteOldLocalStorageDocument()
        {
            try
            {
                for (byte i = 0; i < oldRecord.Count; i++)
                {
                    //If New File Uploaded
                    if (oldRecord[i] == "OldRecord")
                    {
                        if (File.Exists(localStorageFilePathListForOldRecord[i]))
                            File.Delete(localStorageFilePathListForOldRecord[i]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }


        public string GetFullFilePath(string _fullPath, string _nameOfFile)
        {
            string serverDestinationPath = " ";
            // Get Destination Path From Database
            string destinationPath = _fullPath;

            // Check if the destination path contains a tilde ('~') operator.
            if (destinationPath.IndexOf('~') > -1)
            {
                serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
            }

            return serverDestinationPath;
        }

        public bool FileExist(string _fullFilePath)
        {
            bool result = false;

            if (File.Exists(_fullFilePath))
            {
                result = true;
            }
            return result;
        }

        public async Task<bool> SaveData()
        {
            try
            {
                SaveLocalStorageDocument();
                //DeleteOldLocalStorageDocument();
                await context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                DeleteLocalStorageDocument();
                //ResetOldLocalStorageDocument();
                return false;
            }
        }

       }
}
