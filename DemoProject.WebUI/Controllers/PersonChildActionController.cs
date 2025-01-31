using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.Constants;

namespace DemoProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class PersonChildActionController : Controller
    {
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IPersonInformationDetailRepository personInformationDetailRepository;
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;

        public PersonChildActionController(IConfigurationDetailRepository _configurationDetailRepository, IPersonDetailRepository _personDetailRepository, IPersonInformationDetailRepository _personInformationDetailRepository, IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository)
        {
            configurationDetailRepository = _configurationDetailRepository;
            personDetailRepository = _personDetailRepository;
            personInformationDetailRepository = _personInformationDetailRepository;
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
        }

        public async Task<ActionResult> GetCommoditiesAssetByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.CommoditiesAssetEntry(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetChronicDiseaseByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.ChronicDiseaseEntries(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetMachineryAssetByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.MachineryAssetEntries(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAddressDetailByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.AddressEntries(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetImmovableAssetByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.ImmovableAssetEntries(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAgricultureAssetByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.AgricultureAssetEntries(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetBorrowingDetailByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.BorrowingDetailEntries(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetFinancialAssetByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.FinancialAssetEntries(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAdditionalIncomeDetailByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.AdditionalIncomeDetailEntries(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetCourtCaseByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.CourtCaseEntries(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetIncomeTaxDetailByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.IncomeTaxDetailEntries(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAgeFromBirthDate(DateTime _birthDate)
        {
            int age = configurationDetailRepository.GetAge(_birthDate);

            return Json(age, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetContactDetailByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.ContactDetailEntries(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPersonAddressDetailEntryStatus(long _personAddressDetailPrmKey)
        {
            string entryStatus = personDetailRepository.GetPersonAddressDetailEntryStatus(_personAddressDetailPrmKey);

            return Json(entryStatus, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPersonContactDetailEntryStatus(long _personContactDetailPrmKey)
        {
            string entryStatus = personDetailRepository.GetPersonContactDetailEntryStatus(_personContactDetailPrmKey);

            return Json(entryStatus, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPersonCurrentAge(long _personInformationNumber)
        {
            int data = personDetailRepository.GetPersonCurrentAge(_personInformationNumber);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRegionalCountryCity(Guid _centerId)
        {
            bool data = personDetailRepository.IsRegionalCountryCity(_centerId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUniqueInfoNumberStatus(long _personInformationNumber)
        {
            bool data = personDetailRepository.GetPersonInfoNumber(_personInformationNumber);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPersonAutoCompleteList(string _inputString)
        {

            var data = personDetailRepository.GetPersonAutoCompleteList(_inputString);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSysNameOfDocumentByDocumentId(Guid _documentTypeId)
        {
            var data = personDetailRepository.GetSysNameOfDocumentByDocumentId(_documentTypeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSysNameOfIdentificationByIdentificationId(Guid _IdentificationId)
        {
            var data = personDetailRepository.GetSysNameOfIdentificationByIdentificationId(_IdentificationId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDocumentValidations()
        {
            var data = personInformationParameterDetailRepository.GetDocumentValidations();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetDocumentDropdownList(Guid _documentTypeId)
        {
            var reportTypeFormat = personDetailRepository.GetDocumentDropdownEntries(_documentTypeId);

            return Json(reportTypeFormat, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMatchGSTNumberByCenterId(Guid _stateId)
        {
            var data = personDetailRepository.GetMatchGSTNumberByCenterId(_stateId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOccupationList(Guid _occupationId)
        {
            int occupationKey = personDetailRepository.GetlistofOccupation(_occupationId);
            return Json(occupationKey, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSysNameOfOccupation(Guid _occupationId)
        {
            string data = personDetailRepository.GetSysNameOfOccupationById(_occupationId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSysNameOfContactTypeById(Guid _contactTypeId)
        {
            string data = personDetailRepository.GetSysNameOfContactTypeById(_contactTypeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSysNameOfPersonTypeById(Guid _personTypeId)
        {
            string data = personDetailRepository.GetSysNameOfPersonTypeById(_personTypeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSysNameOfMaritalStatus(Guid _maritalStatusId)
        {
            string data = personDetailRepository.GetListOfMaritalStatus(_maritalStatusId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUnmarriedStatusId()
        {
            Guid data = personDetailRepository.GetUnmarriedStatusId();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetPhotoSignByPersonId(Guid _personId)
        {
            long personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);

            var data = await personInformationDetailRepository.PhotoSignEntry(personPrmKey, StringLiteralValue.Verify);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsUniqueBusinessRegistrationNumber(string _businessRegistrationNumber)
        {
            bool data = personDetailRepository.GetBusinessRegistrationNumber(_businessRegistrationNumber);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
