// Document Ready Function
// Whenever you use jQuery to manipulate your web page, you wait until the 
// document ready event has fired. 
// The document ready event signals that the DOM of the page is now ready, 
// so you can manipulate it without worrying that parts of the DOM has not yet been created. 
// The document ready event fires before all images etc. are loaded, 
// but after the whole DOM itself is ready.

// Multiple Document Ready Listeners
// jQuery allows you to register multiple document ready listeners. 
// Just call $(document).ready() multiple times.

// The two listener functions registered in this example will both get called 
// when the DOM is ready. They will get called in the order they were registered.

// Registering multiple document ready event listeners can be really useful 
// if you include HTML pages inside other HTML pages 
// (e.g. using server side include features of your backend / web server). 
// You may need some page initialization to occur both in the outer and inner page. 
// Thus both the outer and inner page can register a document ready listener, and perform the page initialization they both need.

// Nominee Details Show / Hide Based On Selection
//SchemeSharesCertificateParameterViewModel.EnableAutoCertificateNumber
//SchemeSharesCertificateParameterViewModel.EnableDigitalCodeForCertificate

'use strict';
$(document).ready(function () {

    debugger;
    // Document
    let files;
    let selectedDocumentObject;

    let personTypeId = '';
    // DropdownList Variables
    let personDropdownListData = '';
    let personDropdownListDataForFamily = '';
    const BOARD_OF_DROPDOWN_LIST = $('#board-of-director-id').html();
    const ADDRESS_TYPE_DROPDOWN_LIST = $('#address-type-id').html();
    //const DOCUMENT_TYPE_DROPDOWN_LIST = $('#document-document-type-id').html();
    // Array
    let finalDropdownListArray = [];

    const MANDATORY = 'M';
    const DISABLE = 'D';


    let personKYCDetailDocumentPrmKey;
    let personGSTReturnDocumentPrmKey;
    let fileNameDocument = '';
    let localStoragePath;
    let isDbRecord = false;
    let isChangedPhoto = false;
    let lastSelectedValue = '';
    let isEmployee;
    let isChangedPhotoFile = false;
    let isChangedSign = false;

    // Count
    let dropDownListItemCount = 0;


    let identityDocumentType = '';

    // @@@@@@@@@@ Data Table Related letible Declaration
    let tag = '';
    let myModal;
    let id = '';
    let selectedRowIndex = 0;
    let row;
    let rowData = 0;
    let rowNum = 0;
    let checked;
    let columnValues;
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let minimumLength = 0;
    let maximumLength = 0;
    let arr = new Array();
    let entryStatus = false;
    let today;
    let age;
    let fileCaption = '';
    let maritalStatusId;
    let occupationId;
    let meridian;
    let hours;
    let minutes;
    let minimum;
    let maximum;
    let result = true;
    let isVerified = false;
    let filteredData;

    let _occupationType;
    let _maritalStatus;

    let fullname = '';
    let personInfoNumber = 0;
    let inEnglishName = '';
    let inMarathiName = '';

    let personAddressPrmKey = 0;
    let editedAddressTypeId = '';
    let document_Type = '';

    const MARRIED = 'MARRID';

    const SALARIED = 'SLRD';

    const AADHAR_CARD = 'AADHAAR';
    const BIRTH_CERTIFICATE = 'BIRTHCRTF';
    const DRIVING_LICENCE = 'DRIVING';
    const OVERSEAS_CITIZENSHIP_OF_INDIA = 'OVRSCTZ';
    const PAN_CARD = 'PAN';
    const PASSPORT = 'PASSPORT';
    const RATION_CARD = 'RATION';
    const SCHOOL_LEAVING_CERTIFICATE = 'SCHOLLCRTF';
    const VOTER_CARD = 'VOTER';


    let personDropdownListDataForGuardian = '';

    let isValidDocumentBirthdate = true;
    let editedDocumentId = '';
    let kycDocument = '';
    let isVerifyView = false;
    let documentID = '';
    let previousSelectedMaritalStatusId = '';
    let previousSelectedOccupationId = '';
    let previousSelectedDocumentId = '';

    // PersonAddress
    let addressType = '';
    let addressTypeText = '';
    let flatDoorNo = 0;
    let transFlatDoorNo = 0;
    let buildingName = '';
    let transBuildingName = '';
    let roadName = '';
    let transRoadName = '';
    let areaName = '';
    let transAreaName = '';
    let city = '';
    let cityText = '';
    let residenceType = '';
    let residenceTypeText = '';
    let residenceOwnership = '';
    let residenceOwnershipText = '';
    let note = '';
    let panCardNumber = '';
    let transNote = '';
    let reasonForModification = '';


    // guardian Person
    let guardianPersonInformationNumber = '';
    let guardianPersonInformationNumberText = '';

    // PersonKYCDocument
    let kYCDateOfIssueDate = '';
    let kYCExpiryDate = '';
    let kYCDateOfRequestDate = '';
    let kYCDateOfExpectingSubmitDate = '';
    let kYCDateOfSubmitDate = '';
    let documentUploadStatusText;
    let document = '';
    let documentText = '';
    let documentType = '';
    let documentTypeText = '';
    let nameAsPerDocument = '';
    let documentNumber = 0;
    let sequenceNumber = 0;
    let dateOfIssue = '';
    let dateOfExpiry = '';
    let issuingAuthority;
    let placeOfIssue;
    let dateOfRequest = '';
    let dateOfExpectingSubmit = '';
    let dateOfSubmit = '';
    let documentUploadStatus;
    let i;
    let dropdownListItems = '';
    let isDuplicateSequenceNumber = false;
    let isDuplicateDocument = false;
    let editedSequenceNumber = 0;
    let editedDocument = 0;

    // Person Family Detail
    let birthDate = '';
    let fileUploaderId = '';

    // Document
    let fileUploaderInput;
    let fileObj;
    let filePath = '';
    let fileUploader;
    let fileUploaderInputHtml = '';
    let imageTagHtml = '';
    let fileId = '';
    let counter = 100;

    // GST Registration
    let assessmentYear = 0;
    let taxAmount = 0;

    //PersonAddress
    let hasDivClass;

    //PersonContact
    let contactType;
    let contactTypeId;
    let contactTypeText = '';
    let fieldValue;
    let verificationCode = '';
    let isDuplicateContact = false;
    let isMobile = false;
    let isEmail = false;
    let sysNameOfContactType = '';
    let contactDetailPrmKey = 0;
    let editedContactNumber = 0;
    // All Values Get From ContactType And Account Class (Both Table Has Same Values)
    const WHATS_APP_NUMBER = 'WhatsAppNumber';
    const WORK_EMAIL = 'WorkEmail';
    const HOME_MAIL = 'HomeMail';
    const OTHER_MAIL = 'OtherMail';
    const MOBILE = 'Mobile';
    const INDIVIDUAL = 'INDVL';
    let sysNameOfPersonType = '';

    //guardian Person
    let filteredDocumentData;

    //Person Family Detail
    let editedJointAccountPersonId = '';
    let personInformationNumber = 0;
    let personInformationNumberText = '';
    let fullNameOfFamilyMember = '';
    let transFullNameOfFamilyMember = '';
    let relation;
    let relationText = '';
    let occupation = '';
    let occupationText = '';
    let income = 0;
    let familyDetailsBirthDate = '';


    //Person Group Authorized Signatory
    let personGroupAuthorizedSignatoryPrmKey = 0;
    let designationId = '';
    let designationIdText = '';
    let fullNameOfAuthorizedPerson = '';
    let transfullNameOfAuthorizedPerson = '';
    let authorizedPersonAddressDetail = '';
    let transAuthorizedPersonAddressDetail = '';
    let authorizedPersonContactDetail = '';
    let transAuthorizedPersonContactDetail = '';
    let isAuthorizedSignatory = false;

    //Board Of Director Relation
    let boardofdirector = '';
    let boardofdirectorText = '';
    let editedBoardOfDirectorId = '';

    //Person Bank Detail
    let accountopeningDate = '';
    let accountclosingDate = '';
    let bankId = '';
    let bankBranchId = '';
    let bankText = '';
    let bankBranch = '';
    let bankBranchText = '';
    let accountNumber = 0;
    let openingDate = '';
    let closeDate = '';
    let isDefaultBankForTransaction = false;
    let branchDropdownList = '';
    let personBankDetailDocumentPrmKey = 0;

    //Chronic Disease
    let disease = '';
    let diseaseText = '';
    let otherDetails = '';


    //Insurance Detail
    let insuranceType = '';
    let insuranceTypeText = '';
    let insuranceCompany = '';
    let insuranceCompanyText = '';
    let startDate = '';
    let maturityDate = '';
    let policyNumber = 0;
    let policyPremium;
    let policySumAssured;
    let overduesPremium;
    let hasAnyMortgage;
    let editedPolicyNumber = 0;

    //Financial Asset
    let personFinancialAssetDocumentPrmKey;
    let financialOrganizationTypeId = '';
    let financialOrganizationTypeIdText = '';
    let nameOfFinancialOrganization = '';
    let transNameOfFinancialOrganization = '';
    let nameOfBranch = '';
    let transNameOfBranch = '';
    let addressDetails = '';
    let transAddressDetails = '';
    let contactDetails = '';
    let transContactDetails = '';
    let financialAssetType = '';
    let financialAssetTypeText = '';
    let financialAssetDescription;
    let transFinancialAssetDescription;
    let referenceNumber = 0;
    let transReferenceNumber = 0;
    let investedAmount = 0;
    let currentMarketValue = 0;
    let monthlyInterestIncomeAmount = 0;
    let financialAssetOpeningDate = '';
    let financialAssetMatureDate = '';

    //Movable Asset
    let vehicleMakeId = '';
    let vehicleMakeIdText = '';
    let vehicleModelId = '';
    let vehicleModelIdText = '';
    let vehicleletiant = '';
    let vehicleletiantText = '';
    let numberOfOwners = '';
    let manufacturingYear;
    let dateOfPurchase;
    let purchasePrice = 0;
    let ownershipPercentage = 0;
    let isOwnershipDeceased = false;
    let movableAssetRegistrationDate = '';
    let movableDateOfPurchase = '';
    let letiantList = '';
    let letiant = '';
    let vehicleModelEditedId = '';
    let vehicleVariantEditedId = '';
    let personMovableAssetDocumentPrmKey = 0;
    let listItemCount = 0;
    let registrationDate = '';
    let registrationNumber = 0;


    let lastVehicleMakeSelectedValue = '';
    let lastVehicleModelSelectedValue = '';

    //Person Immovable Asset
    let personImmovableAssetDocumentPrmKey;
    let surveyNumber = 0;
    let citySurveyNumber = 0;
    let number = 0;
    let areaOfLand;
    let isConstructed = false;
    let constructionArea;
    let carpetArea;
    let annualRentIncome = 0;
    let ownershipType = '';
    let ownershipTypeText = '';
    let assetFullDescription = '';

    //Person Agriculture Asset
    let agricultureLandTypeId = '';
    let agricultureLandTypeText = '';
    let agricultureLandDescription = '';
    let groupNumber = 0;
    let volume = 0;
    let ownershipTypeId = '';
    let personAgricultureAssetDocumentPrmKey;
    let isOnlyRainFedTypeIrrigation = false;
    let hasCanalRiverIrrigationSource;
    let hasWellsIrrigationSource;
    let hasFarmLakeSource;
    let annualIncomeFromLand;
    let hasAnyCourtCase = false;
    let courtCaseFullDetails = '';
    let prmKey = 0;

    //person Machinery Asset
    let personMachineryAssetDocumentPrmKey;
    let nameOfMachinery = '';
    let machineryFullDetails = '';
    let machineryDateOfPurchase = '';
    today = new Date();


    //Income Tax Detail
    let personIncomeTaxDetailDocumentPrmKey;
    let incomeSource = '';
    let incomeSourceText = '';
    let annualIncome = 0;

    //Person Borrowing Detail
    let nameOfOrganization = '';
    let transNameOfOrganization = '';
    let transBranch;
    let matureDate = '';
    let loanDetails = '';
    let transLoanDetails = '';
    let mortgageDetails = '';
    let transMortgageDetails = '';
    let mortgageAmount = 0;
    let sanctionLoanAmount = 0;
    let installmentAmount = 0;
    let loanBalanceAmount = 0;
    let overduesInstallment;
    let overduesAmount = 0;
    let isTakingAnyCourtAction = false;
    let courtCaseType = '';
    let courtCaseTypeText = '';
    let courtCaseStage;
    let courtCaseStageText = '';
    let filingDate = '';
    let filingNumber = 0;
    let cNRNumber = 0;
    let branch = '';

    // Credit Rating
    let effectiveDate = '';
    let agency;
    let agencyText = '';
    let score = 0;
    let creditRatingEffectiveDate;

    // Court Case
    let courtCaseTypeId = '';
    let courtCaseTypeIdText = '';
    let cnrNumber = 0;
    let amountOfDecree = 0;
    let collateralAmount = 0;
    let courtCaseStageId = '';
    let courtCaseStageIdText = '';

    //Income Tax Detail
    let photoPathTax;

    //Social Media
    let socialMediaId = '';
    let socialMediaIdText = '';
    let socialMediaLink = '';
    let time = 0;

    //sms alert
    let personInformationParameterNoticeTypeId = '';
    let personInformationParameterNoticeTypeIdText = '';
    let appLanguageId = '';
    let appLanguageIdText = '';
    let ss;
    let sendingTime = 0;
    let scheduletime = [];
    // M A I N     P A G E     I N P U T     V A L I D A T I O N 
    let url = window.location.href;
    let index = url.lastIndexOf('/') + 1;
    let filenameWithExtension = url.substr(index);
    let filename = filenameWithExtension.split('?')[0]; // <-- added this line
    let winname = filename;

    debugger;
    // Create DataTables
    let addressDataTable = CreateDataTable('person-address');
    let contactDataTable = CreateDataTable('contact');
    let personKycDataTable = CreateDataTable('kyc-document');
    let personFamilyDataTable = CreateDataTable('family-detail');
    let authorizedSignatoryDataTable = CreateDataTable('authorized-signatory');
    let boardOfDirectorDataTable = CreateDataTable('relation');
    let bankDataTable = CreateDataTable('bank-detail');
    let gstDataTable = CreateDataTable('gst-registration');
    let chronicDataTable = CreateDataTable('chronic-disease');
    let insuranceDataTable = CreateDataTable('insurance-detail');
    let financialDataTable = CreateDataTable('financial-asset');
    let movableDataTable = CreateDataTable('movable-asset');
    let immovableDataTable = CreateDataTable('immovable-asset');
    let agricultureDataTable = CreateDataTable('agriculture-asset');
    let machineryDataTable = CreateDataTable('machinery-asset');
    let incomeDatatable = CreateDataTable('income-detail');
    let borrowingDetailDataTable = CreateDataTable('borrowing-detail');
    let creditDataTable = CreateDataTable('credit-rating');
    let courtCaseDataTable = CreateDataTable('court-case');
    let socialMediaDataTable = CreateDataTable('social-media');
    let smsAlertDataTable = CreateDataTable('sms-alert');
    let incomeTaxDataTable = CreateDataTable('income-tax');
    debugger;
    SetPageLoadingDefaultValues();


    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Attach FileUploader Control
    function AttachFileUploader() {
        let dataTransferObj = new DataTransfer();

        // Get Uploaded Files In File Object
        fileObj = fileUploader.files[0];

        // Add This File Into Data Transfer Object

        //Breakdown of slice Method
        //The slice method creates a new Blob object containing data from a portion of the original file (f). It is a method available on Blob and File objects in JavaScript.
        //    Parameters:
        //0 (start):
        //The starting byte offset of the slice. 0 means the slice starts from the beginning of the file.
        //f.size (end):
        //The ending byte offset of the slice. f.size indicates the end of the file, meaning the slice includes all the data from the start to the end of the file.
        //f.type (content type):
        //An optional parameter that specifies the MIME type of the slice. f.type takes the type of the original file (f). This ensures the newly created slice retains the same content type as the original file.

        dataTransferObj.items.add(new File([fileObj.slice(0, fileObj.size, fileObj.type)], fileObj.name))

        fileUploaderInput = $('#' + fileUploaderId).get(0);

        fileUploaderInput.files = dataTransferObj.files;
    }

    // Document File Uploader
    $('.doc-upload').change(function () {
        debugger;
        let docInput = '';
        let myId = $(this).attr('id');

        // Use switch to determine document type
        switch (myId) {
            case 'bank-file-uploader':
                docInput = 'BankStatement';
                break;
            case 'income-tax-file-uploader':
                docInput = 'IncomeTax';
                break;
            case 'movable-file-uploader':
                docInput = 'MovableAsset';
                break;
            case 'agriculture-file-uploader':
                docInput = 'AgricultureAsset';
                break;
            case 'finance-file-uploader':
                docInput = 'FinancialAsset';
                break;
            case 'machinery-file-uploader':
                docInput = 'MachineryAsset';
                break;
            case 'group-sign-file-uploader':
            case 'person-sign-file-uploader':
                docInput = 'Sign';
                break;
            case 'immovable-file-uploader':
                docInput = 'ImmovableAsset';
                break;
            case 'kyc-file-uploader':
                docInput = 'KYC';
                break;
            case 'gst-file-uploader':
                docInput = 'GST';
                break;
            case 'photo-file-uploader':
                docInput = 'Photo';
                break;
            default:
                docInput = 'None';
                break;
        }

        // $('#document-file-uploader-error').addClass('d-none');
        $('#' + myId + '-error').addClass('d-none');

        if (this.files.length > 0) {
            const uploadFile = this.files[0];
 
            // Upload File
            if (IsValidFile(docInput, uploadFile)) {
                let reader = new FileReader();

                reader.onload = function (e) {
                    $('#' + myId + '-image-preview').attr('src', e.target.result);
                }

                reader.readAsDataURL(uploadFile);
            }
        }
        else {
            $('#' + myId + '-image-preview').attr('src', '');
        }

    });

    // Validate Uploaded File
    function IsValidFile(_inputSource, _uploadFile) {
        debugger;
        let result = true;
        let isUploadInLocalStorage = false;

        isChangedPhoto = true;

        if (_inputSource === 'Photo')
            isChangedPhotoFile = true;

        if (_inputSource === 'Sign')
            isChangedSign = true;

        if (_uploadFile) {
            const fileName = _uploadFile.name;
            const fileSize = Math.round(_uploadFile.size / 1024);
            const fileExtension = fileName.split('.').pop().toLowerCase();

            let validDocumentFileExtensions = '';
            let maxDocumentFileSize = 0;

            let uploaderId = _inputSource.replace('Asset', '').toLowerCase();

            // KYC
            if (_inputSource === 'KYC') {
                uploaderId = 'kyc';
            }

            // GST
            if (_inputSource === 'GST') {
                uploaderId = 'gst';
            }

            // Bank Statement
            if (_inputSource === 'Bank') {
                uploaderId = 'bank';
            }

            // Income Tax
            if (_inputSource === 'IncomeTax') {
                uploaderId = 'income-tax';
            }

            // Movable Asset
            if (_inputSource === 'MovableAsset') {
                uploaderId = 'movable';
            }

            // Agriculture Asset
            if (_inputSource === 'AgricultureAsset') {
                uploaderId = 'agriculture';
            }

            // Financial Asset
            if (_inputSource === 'FinancialAsset') {
                uploaderId = 'finance';
            }

            // Machinery Asset
            if (_inputSource === 'MachineryAsset') {
                uploaderId = 'machinery';
            }

            // Sign
            if (_inputSource === 'Sign')
            {

                // Check Whether Selected Person Type Is Individual Or Group
                if ($('.individual').hasClass('d-none') === true)
                {
                    uploaderId = 'group-sign';
                }
                else
                {
                    uploaderId = 'person-sign';
                }
            }

            // Immovable Asset
            if (_inputSource === 'ImmovableAsset') {
                uploaderId = 'immovable';
            }

            // Photo
            if (_inputSource === 'Photo') {
                uploaderId = 'photo';
            }

            let isUploadInLocalStorage = personInformationParameterViewModel[`Enable${_inputSource}DocumentUploadInLocalStorage`];

            // Get File Formats And File Size By Storage
            if (isUploadInLocalStorage === true) {
                validDocumentFileExtensions = personInformationParameterViewModel[`${_inputSource}DocumentAllowedFileFormatsForLocalStorage`].toLowerCase().replace('.', '');
                maxDocumentFileSize = personInformationParameterViewModel[`MaximumFileSizeFor${_inputSource}DocumentUploadInLocalStorage`];
            } else {
                validDocumentFileExtensions = personInformationParameterViewModel[`${_inputSource}DocumentAllowedFileFormatsForDb`].toLowerCase().replace('.', '');
                maxDocumentFileSize = personInformationParameterViewModel[`MaximumFileSizeFor${_inputSource}DocumentUploadInDb`];
            }

            // Check Valid File Formats Or Size
            if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize)) {
                $('#' + uploaderId + '-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + ' Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                $('#' + uploaderId + '-file-uploader-error').removeClass('d-none');

                $('#' + uploaderId + '-file-uploader-image-preview').attr('src', '#');
                $('#' + uploaderId + '-file-uploader').val('');

                result = false;
            }
            $('#' + uploaderId + '-file-caption').val('');
        }
        return result;
    }

    $('.upper-case').on('keyup paste', function () {
        let $this = $(this);
        let maxLength = $this.attr('maxlength');

        // Convert the current value to uppercase and enforce maxlength
        let uppercaseValue = $this.val().toUpperCase().slice(0, maxLength);
        // Set the uppercase value back to the input field
        $this.val(uppercaseValue);
    });

    $('#person-type-id').focusout(function ()
    {
        if (isVerifyView === false)
      {
        CheckPersonType(personTypeId);

        $('.individual-main-input').val('');

        $('.group-main-input').val('');

        // Mark Out Select All Check Box Of All Datatables.
        $('input[name="select-all"]').prop('checked', false);

        // Clear Accordion Title Error Messages
        $('.accordion-title-error').addClass('d-none');

        // Make False All Toggle Switch
        $('.switch-input').prop('checked', false);

        //Clear all number fiels and textarea
        $('input[type="number"]').val('');

        $('.individual').attr('src', '');

        //Clear all TextArea Except Note and their Translation Field
        $('textarea').val('');
        // Make False All Toggle Switch
        $('.switch-input').prop('checked', false);

        // Clear home branch Input
        $('.home-branch-input').val('');

        // Clear additional details Input
        $('.additional-details-input').val('');

        // Clear guardian details Input
        $('.guardian-details-input').val('');

        // Clear person photo sign Input
        $('.person-photo-sign-input').val('');

        // Clear gst registration Input
        $('.gst-registration-input').val('');

        // Clear commodities asset Input
        $('.commodities-asset-input').val('');


        // Clear All Data Tables
        addressDataTable.clear().draw();
        contactDataTable.clear().draw();
        personKycDataTable.clear().draw();
        personFamilyDataTable.clear().draw();
        authorizedSignatoryDataTable.clear().draw();
        boardOfDirectorDataTable.clear().draw();
        bankDataTable.clear().draw();
        gstDataTable.clear().draw();
        chronicDataTable.clear().draw();
        insuranceDataTable.clear().draw();
        movableDataTable.clear().draw();
        immovableDataTable.clear().draw();
        agricultureDataTable.clear().draw();
        machineryDataTable.clear().draw();
        incomeDatatable.clear().draw();
        borrowingDetailDataTable.clear().draw();
        creditDataTable.clear().draw();
        courtCaseDataTable.clear().draw();
        socialMediaDataTable.clear().draw();
        smsAlertDataTable.clear().draw();
        incomeTaxDataTable.clear().draw();

        $('#enable-gst-registration-details-input').addClass('read-only');
        $('#enable-gst-registration-details').prop('checked', false);

        // Hide All Accordion Based On Toggle Switch s
        SetToggleSwitchBasedAccordions();
    }
    });

    function CheckPersonType(personTypeId) {
        debugger;
        personTypeId = $('#person-type-id').val();
        debugger;
        if (personTypeId !== '') {
            debugger;
            $.get('/PersonChildAction/GetSysNameOfPersonTypeById', { _personTypeId: personTypeId, async: false }, function (data) {
                debugger;
                sysNameOfPersonType = data;
                debugger;
                if (sysNameOfPersonType == INDIVIDUAL) {
                    $('.individual').removeClass('d-none');
                    $('.group').addClass('d-none');
                    //$('#employee-block').addClass('d-none');
                    $('#guardian-detail-card').addClass('d-none');

                } else {
                    $('.individual').addClass('d-none');
                    $('.group').removeClass('d-none');
                    $('#employee-block').addClass('d-none');
                    $('#guardian-detail-card').addClass('d-none');

                }
            });
        }
        else {
            $('.individual').addClass('d-none');
            $('.group').addClass('d-none');
            $('#employee-block').addClass('d-none');
            $('#guardian-detail-card').addClass('d-none');

        }
    }

    //Home Branch Dropdown Focusout
    lastSelectedValue = $('#home-branch-id').val();
    //Clear field Base On Home Branch Dropdown Change
    $('#home-branch-id').focusout(function () {
        debugger;
        let currentValue = $(this).val();

        // If the value has changed from the initial value, clear the related fields
        if (currentValue !== lastSelectedValue) {
            $('#language-id').val('');
            $('#activation-date-home-branch').val('');
            $('#note-home-branch').val('');
        }

        // Update lastSelectedValue to the current value for subsequent changes
        lastSelectedValue = currentValue;
    });

    // Guardian Person AutoComplete 
    $('#guardian-pin').autocomplete(
    {
        minLength: 0,
        appendTo: '#guardian-pin-input',
        scroll: true,
        autoFocus: true,
        source: function (request, response) {
            let responseDropdownListArray = [];
            dropDownListItemCount = finalDropdownListArray.length;

            if (parseInt(dropDownListItemCount) > 0) {
                responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                    if (key.Text.toUpperCase().indexOf(request.term.toUpperCase()) != -1)
                        return { label: key.Text, valueId: key.Value }
                    else
                        return null;
                });

                // The slice() method returns selected elements in an array, as a new array. 
                // The slice() method selects from a given start, up to a (not inclusive) given end.
                response(responseDropdownListArray.slice(0, 10));
            }
            else
                response([{ label: 'No Records Found', value: -1 }]);

        },
        select: function (event, ui) {
            event.preventDefault();
            $('#guardian-pin').val(ui.item.label);
            $('#guardian-pin1').val(ui.item.valueId);
            guardianPersonInformationNumber = ui.item.valueId;
            guardianPersonInformationNumberText = ui.item.label;
        },
    }).focus(function (event, ui) {
        // Clear Selected Guardian Nominee Person
        guardianPersonInformationNumber = '';
        guardianPersonInformationNumberText = '';
        $('#guardian-pin').val('');

        // Assign Array Without Reference  *** Use Slice Method
        finalDropdownListArray = personDropdownListDataForGuardian.slice();

        $(this).autocomplete('search');
    });

    // It Return Age Value
    function CalculateAgeInYears() {
        debugger;
        const today = new Date();
        const birthDateObj = new Date($('#birth-date').val());
        let age = today.getFullYear() - birthDateObj.getFullYear();
        const month = today.getMonth();
        const birthMonth = birthDateObj.getMonth();

        // If the birthday hasn't occurred yet this year, subtract one from the age
        if (parseInt(month) < parseInt(birthMonth) || (parseInt(month) === parseInt(birthMonth) && today.getDate() < birthDateObj.getDate())) {
            age--;
        }

        if (age > 18) {
            $('#guardian-detail-card').addClass('d-none');
            $('.guardian-details-input').val('');
            $('.guardian-details-radio-input').prop('checked', false);
        }
        else {
            $('#guardian-pin-input').removeClass('d-none');
            $('#guardian-detail-card').removeClass('d-none');
            $('#married-status-input').addClass('d-none');
            SetUnmarriedStatusId();
        }

        return age;
    }

    // Address Type Unique Dropdown
    // It Does Not Return Any Value
    function SetAddressTypeUniqueDropdownList() {

        // Show All List Items
        $('#address-type-id').html('');
        $('#address-type-id').append(ADDRESS_TYPE_DROPDOWN_LIST);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-person-address > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (addressDataTable.row(currentRow).data());

            if (typeof myColumnValues !== 'undefined' && myColumnValues !== null) {

                if (myColumnValues[1] !== editedAddressTypeId)
                    $('#address-type-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
    }

    // Set Document Birthdate Based On Birth Date
    // It Return True Value
    function SetDocumentBirthdate() {
            const today = new Date();

            let birthdate = new Date($('#birth-date').val());

            // Consider Difference Between Birthdate And Document Birthdate @ +5, -5 Years
            // For Example - BirthDate - 09.10.2015 Valid Dates - 09.10.2010, 09.10.2020
            // If Current Date - 24.10.2024 And BirthDate - 10.10.2024 Valid Date - 10.10.2019, 24.10.2024
            let maxBirthDate = new Date(birthdate);
            maxBirthDate.setFullYear(birthdate.getFullYear() + 5);

            let minBirthDate = new Date(birthdate);
            minBirthDate.setFullYear(birthdate.getFullYear() - 5);

            // Check if the calculated maxBirthDate exceeds today's date
            if (maxBirthDate > today) {
                maxBirthDate = today;
            }

            // Set the min and max attributes
            $('#document-birth-date').attr('min', GetInputDateFormat(minBirthDate));
            $('#document-birth-date').attr('max', GetInputDateFormat(maxBirthDate));

            return true;
    }

    // It Does Not Return Any Value
    function SetDocumentDropdownList() {
        let kycDocumentTypeId = $('#kyc-document-type-id').val();

        $.get('/PersonChildAction/GetDocumentDropdownList', { _documentTypeId: kycDocumentTypeId, async: false }, function (data, textStatus, jqXHR) {
            $('#kyc-document-id').html('');

            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000">--- Select Document --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#kyc-document-id').append(dropdownListItems);

            dropDownListItemCount = $('#kyc-document-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (dropDownListItemCount == 1) {
                $('#kyc-document-id').prop('selectedIndex', 1);
                $('#kyc-document-id').change();
            }
            else {
                if (editedDocumentId !== '') {
                    $('#kyc-document-id').val(editedDocumentId);
                }
            }
        });
    }

    // It Does Not Return Any Value
    function SetDocumentNumber() {
        $('#kyc-document-number').attr('type', 'text');

        $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: documentID, async: false }, function (document) {
            debugger;
            kycDocument = document;
            // Clear previous validation
            $('#kyc-document-number').removeAttr('minlength').removeAttr('maxlength');
            $('#kyc-document-number-error1').addClass('d-none');

            // AADHAAR Card
            if (document === AADHAR_CARD) {
                $('#kyc-document-number').attr('type', 'number');

                $('#kyc-document-number').attr('minlength', 12);
                $('#kyc-document-number').attr('maxlength', 12);
            }

            // Pan Card
            if (document === PAN_CARD) {
                $('#kyc-document-number').attr('minlength', 10);
                $('#kyc-document-number').attr('maxlength', 10);

                $('#enable-gst-registration-details-input').removeClass('read-only');
            }

            // Voting Card
            if (document === VOTER_CARD) {
                $('#kyc-document-number').attr('minlength', 10);
                $('#kyc-document-number').attr('maxlength', 10);
            }

            // DRIVING License
            // DRIVING License
            if (document === DRIVING_LICENCE) {
                $('#kyc-document-number').attr('minlength', 14);
                $('#kyc-document-number').attr('maxlength', 16);
            }

            //PassPort
            if (document === PASSPORT) {
                $('#kyc-document-number').attr('minlength', 8);
                $('#kyc-document-number').attr('maxlength', 8);
            }

            // RATION
            if (document === RATION_CARD) {
                $('#kyc-document-number').attr('minlength', 11);
                $('#kyc-document-number').attr('maxlength', 11);
            }

            // Birth Certificate
            if (document === BIRTH_CERTIFICATE || document === SCHOOL_LEAVING_CERTIFICATE || document === OVERSEAS_CITIZENSHIP_OF_INDIA) {
                $('#kyc-document-number').attr('minlength', 1);
                $('#kyc-document-number').attr('maxlength', 45);
            }
        });
    }

    // Set GST Registration Details Based On Pan Card Number
    // It Does Not Return Any Value
    function SetGSTRegistrationDetail() {
        let rowCount = 0;
        let isPANCardExists = false;
        let tableRowCount = personKycDataTable.rows().count();

        $('#tbl-kyc-document > tbody > tr').each(function () {
            debugger;
            currentRow = $(this).closest('tr');
            columnValues = (personKycDataTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                debugger;
                $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: columnValues[3], async: false }, function (data) {
                    rowCount = rowCount + 1;
                    if (data === PAN_CARD) {
                        isPANCardExists = true;
                        $('#enable-gst-registration-details-input').removeClass('read-only');
                    }
                    else {
                        // Execute On Only Last Record
                        if (tableRowCount === rowCount) {
                            debugger;
                            if (isPANCardExists === false) {
                                debugger;
                                gstDataTable.clear().draw();
                                $('#enable-gst-registration-details').prop('checked', false);
                                $('#enable-gst-registration-details-input').addClass('read-only');
                                $('#gst-registration-details-block').addClass('d-none');
                                $('#gst-return-document-block').addClass('d-none');
                                $('#gst-registration-accordion-error').addClass('d-none');
                            }
                        }
                    }
                });
            }
            else {
                // Execute On Only Last Record
                if (tableRowCount === rowCount) {
                    debugger;
                    if (isPANCardExists === false) {
                        debugger;
                        gstDataTable.clear().draw();
                        $('#enable-gst-registration-details').prop('checked', false);
                        $('#enable-gst-registration-details-input').addClass('read-only');
                        $('#gst-registration-details-block').addClass('d-none');
                        $('#gst-return-document-block').addClass('d-none');
                        $('#gst-registration-accordion-error').addClass('d-none');
                    }
                }
            }
        });
    }

    // It Does Not Return Any Value
    function SetMaritalStatusDetails() {
        maritalStatusId = $('#marital-status-id').val();

        if ($('#marital-status-id').prop('selectedIndex') > 1) {
            $.get('/PersonChildAction/GetSysNameOfMaritalStatus', { _maritalStatusId: maritalStatusId, async: false }, function (data) {
                if (data === MARRIED) {
                    $('#married-status-input').removeClass('d-none');
                }
                else {
                    $('#married-status-input').addClass('d-none');
                }
            });
        }
        else {
            $('#married-status-input').addClass('d-none');
        }
    }

    // It Does Not Return Any Value
    // It Does Not Return Any Value
    function SetOccupationDetails() {
        debugger;
        occupationId = $('#occupation-id').val();
        $.get('/PersonChildAction/GetSysNameOfOccupation', { _occupationId: occupationId, async: false }, function (data) {
            debugger;
            isEmployee = $('#enable-employee').is(':checked') ? true : false;
            if (data === SALARIED) {
 
                $('#is-employee-input').removeClass('d-none');
                debugger;
                if (isEmployee === false) {
                    debugger;
                    $('#employee-block').removeClass('d-none');
 

                } else {
                    $('#employee-block').addClass('d-none');
 
                }

                IsValidAdditionalDetailsAccordionInputs();

            }
            else {
                $('#employee-block').addClass('d-none');
                $('#is-employee-input').addClass('d-none');
 
            }
        });
    }

    // It Does Not Return Any Value
    function SetUnmarriedStatusId() {
        $.get('/PersonChildAction/GetUnmarriedStatusId', { async: false }, function (data) {
            debugger;
            $('#marital-status-id').val(data);
        });

    }

    // It Return ValidDocumentBirthdate in the form of True or False Value.
    function ValidateDocumentBirthdate() {
        if ($('#document-birth-date').val() !== '') {
            let documentBirthdate = new Date($('#document-birth-date').val());

            let minimumBirthdate = $('#document-birth-date').attr('min');
            let maximumBirthdate = $('#document-birth-date').attr('max');

            if (documentBirthdate < minimumBirthdate || documentBirthdate > maximumBirthdate) {
                isValidDocumentBirthdate = false;
            }
        }
        else {
            isValidDocumentBirthdate = false;
        }

        return isValidDocumentBirthdate;
    }

    // It Return True Value If All Validations pass Otherwise Return False 
    function ValidateGSTNumber() {
        // Regular expression to match the GST number format
        const gstRegex = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[0-9]{1}[A-Z]{1}[0-9]{1}$/;
        const gstNumber = $('#gst-registration-number').val();

        // Extracting the individual parts of the GST number
        const stateCode = gstNumber.substring(0, 2);       // First 2 characters: State code
        const pan = gstNumber.substring(2, 12);             // Next 10 characters: PAN
        const entityCode = gstNumber.substring(12, 13);    // 13th character: Entity code (numeric)
        const registrationType = gstNumber.substring(13, 14); // 14th character: Nature of registration
        const checkDigit = gstNumber.charAt(14);           // 15th character: Check digit

        // Validate the state code (should be a valid two-digit state code in India)
        const validStateCodes = [
            '01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13', '14', '15', '16',
            '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30', '31', '32',
            '33', '34', '35', '36'
        ];

        if (validStateCodes.includes(stateCode) === false) {
            return {
                isValid: false,
                message: 'Inputed State Code Not Fit In GST Number Format.'
            };
        }


        // Validate the PAN (should be a valid PAN structure)
        const panRegex = /^[A-Z]{5}[0-9]{4}[A-Z]{1}$/;

        if (!panRegex.test(pan)) {
            return {
                isValid: false,
                message: 'Inputed PAN Number Not Fit In GST Number Format.'
            };
        }

        // Validate the entity code (should be a numeric digit)
        if (isNaN(entityCode) || entityCode < 0 || entityCode > 9) {
            return {
                isValid: false,
                message: 'Inputed Entity Code Not Fit In GST Number Format.'
            };
        }

        // Validate the registration type (should be a single uppercase letter)
        if (!/^[A-Z]$/.test(registrationType)) {
            return {
                isValid: false,
                message: 'Inputed Registration Type Not Fit In GST Number Format.'
            };
        }

        // Validate the check digit (should be an uppercase letter or number)
        if (!/^[A-Z0-9]$/.test(checkDigit)) {
            return {
                isValid: false,
                message: 'Inputed Check Digit Not Fit In GST Number Format.'
            };
        }

        // Check if the input matches the required GST number format
        if (gstRegex.test(gstNumber) === false) {
            return {
                isValid: false,
                message: 'Invalid GST Number Format. Please Enter A Valid GST Number.'
            };
        }


        // If all validations pass
        return {
            isValid: true,
            message: 'GST number is valid.'
        };
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Hide Guardian Details If User Select Adult Person Name As Nominee (** This List Contains Only Adult Person Name)
    $('#guardian-pin').focusout(function (event) {
        if (!isVerifyView) {
            if (guardianPersonInformationNumber === '') {
                $('#guardian-detail').removeClass('d-none');
                $('.guardian-input').val('');
            }
            else {
                $('#guardian-detail').addClass('d-none');
            }
        }
    });

    // Guardian Full Name
    // Guardian Full Name
    $('#guardian-full-name').focusout(function () {
        let gurdianFullName = $('#guardian-full-name').val();

        if ((gurdianFullName !== 'None') && (gurdianFullName.length > 3)) {
            $('#guardian-pin').val('None');
            guardianPersonInformationNumber = '';
            $('#guardian-pin1').val(0);
            $('#guardian-pin-input').addClass('d-none');
        }
        else {
            $('#guardian-pin-input').removeClass('d-none');
        }
    });


    // VIP RANK
    $('#vip-rank').focusout(function () {
        let vipRank = parseInt($('#vip-rank').val());

        // Check if the entered VIP rank is a valid number greater than 0
        if (isNaN(vipRank) === true || parseInt(vipRank) > 10) {
            $('#vip-rank').val(0);

            vipRank = 0;

            $('#vip-background-details').val('None');
            $('#trans-vip-background-details').val('None');

            $('.vip-background-details').addClass('d-none');
        }
        else {
            if (parseInt(vipRank) > 0) {
                $('.vip-background-details').removeClass('d-none');
            }
            else {
                $('.vip-background-details').addClass('d-none');
            }
        }
    });

    // Marital Status
    $('#marital-status-id').focusout(function () {
        if (isVerifyView === false)
        {
            maritalStatusId = $('#marital-status-id').val();

            if (previousSelectedMaritalStatusId !== maritalStatusId) {
                SetMaritalStatusDetails();
                $('.married-input').val('');
                previousSelectedMaritalStatusId = maritalStatusId;
            }
        }
    });

    // Age Proof Submission Status Radio Button
    $('.age-proof-submission-status').focusout(function () {
        IsValidGuardianAccordionInputs();
    });

    // BirthDate
    $('#birth-date').focusout(function () {
        debugger;
        if (isVerifyView === false) {

            let birthdate = $('#birth-date').val();

            $('#marital-status-id').val('');
            $('.married-input').val('');
            $('#married-status-input').addClass('d-none');
            previousSelectedMaritalStatusId = '';

            if (birthdate !== '') {
                SetDocumentBirthdate();
                $('#document-birth-date').val(new Date(birthdate));
                CalculateAgeInYears();
            }
            else {
                $('#guardian-detail-card').addClass('d-none');
                $('.guardian-details-input').val('');
                $('.guardian-details-radio-input').prop('checked', false);

            }
        }
    });

    // Validate Document Birth Date
    $('#document-birth-date').focusout(function () {
        debugger;
        ValidateDocumentBirthdate();
    });

    // Event listener for when occupation dropdown changes
    $('#occupation-id').focusout(function () {
        debugger;
        if (isVerifyView === false)
        {
            let occupationId = $(this).val();

            if (previousSelectedOccupationId !== occupationId) {
                SetOccupationDetails();

                $('.employer-input').val('');
                $('#enable-employee').prop('checked', false);

                previousSelectedOccupationId = occupationId;
            }
        }
    });

    // Additional detail EPF Validation
    $('#epf-number').focusout(function () {
        // Regular expression for EPF number (Example format: MH/BAN/0001234/5678901)
        let epfRegex = /^[A-Z]{2}\/[A-Z]{3}\/\d{7}\/\d{7}$|^[A-Z]{3}\d{2}\d{7}\d{7}$/;
        let epfNumber = $('#epf-number').val().trim();

        // Trim the EPF number to a maximum of 22 characters
        epfNumber = epfNumber.substring(0, 22);

        $('#epf-number').val(epfNumber); // Update the input field with the trimmed value

        if (epfRegex.test(epfNumber)) {
            $('#epf-number-error').addClass('d-none');
        }
        else {
            $('#epf-number-error').removeClass('d-none');
        }
    });

    $('.additional-details-input').focusout(function () {
        debugger;
        IsValidAdditionalDetailsAccordionInputs();
    });

    // Enable Guardian Details Accordion Input Validation
    $('.guardian-details-input').focusout(function () {
        debugger;
        IsValidGuardianAccordionInputs();
    });

    // Enable GST Registration Accordion Input Validation
    $('.gst-registration-input').focusout(function () {
        IsValidGstRegistrationAccordionInputs();
    });

    // GST Registration Number
    $('#gst-registration-number').focusout(function () {
        let myResult = ValidateGSTNumber();

        // Display the error message if any
        if (myResult.isValid === false) {
            $('#gst-registration-number-error').text(myResult.message).removeClass('d-none');
        } else {
            $('#gst-registration-number-error').addClass('d-none');
        }
    });

    // Handle change event on document type selection
    $('#kyc-document-id').focusout(function () {
        documentID = $('#kyc-document-id').val();

        if (previousSelectedDocumentId !== documentID) {
            $('#kyc-document-number').val('');
            SetDocumentNumber();
            previousSelectedDocumentId = documentID;
        }
    });

    // Handle focusout event
    $('#kyc-document-number').focusout(function () {
        debugger;
        IsValidDocumentNumber();
    });

    // Document Type Id Kyc Document
    $('#kyc-document-type-id').focusout(function () {
        SetDocumentDropdownList();
    });

    $('#kyc-document-sequence-number').focusout(function () {

        let filteredData = personKycDataTable
            .rows()
            .indexes()
            .filter(function (value, index) {
                return personKycDataTable.row(value).data()[7] == $('#kyc-document-sequence-number').val();
            });

        if (personKycDataTable.rows(filteredData).count() > 0 && editedSequenceNumber !== $('#kyc-document-sequence-number').val()) {
            $('#unique-kyc-document-sequence-number-error').removeClass('d-none');
        }
        else {
            $('#unique-kyc-document-sequence-number-error').addClass('d-none');
        }

    });

    $('#enable-gst-return-document').change(function () {
        gstDataTable.clear().draw();
        $('#gst-registration-accordion-error').addClass('d-none');
    });

    $('#kyc-document-date-of-issue').click(function () {
        $('#kyc-document-date-of-expiry').val('');
    });

    // SetDocumentBirthDate
    $('#document-birth-date').click(function () {
        debugger;
        SetDocumentBirthdate();
    });

    $('#applicable-from').click(function () {
        // Set the minimum date attribute to '2017-07-01'
        $('#applicable-from').attr('min', GetInputDateFormat(new Date('2017-07-01')));
    });

    $('#date-of-request').click(function () {
        $('#date-of-expecting-submit').val('');
        $('#date-of-submit').val('');
    });

    $('#date-of-expecting-submit').click(function () {
        let dateOfRequest = new Date($('#date-of-request').val());

        $('#date-of-expecting-submit').attr('min', GetInputDateFormat(dateOfRequest));
    });

    $('#date-of-submit').click(function () {

        let dateOfRequest = new Date($('#date-of-request').val());

        $('#date-of-submit').attr('min', GetInputDateFormat(dateOfRequest));
    });

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   VALIDATION FOCUSOUT FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Enable Home Branch Accordion Input Validation
    $('.home-branch-input').focusout(function () {
        debugger;
        if (IsValidHomeBranchAccordionInputs())
            $('#home-branch-accordion-error').addClass('d-none');
    });

    // Enable Additional Details Accordion Input Validation
    $('.additional-details-input').focusout(function () {

        if (IsValidAdditionalDetailsAccordionInputs())
            $('#person-additional-details-accordion-error').addClass('d-none');
    });

    $('.photo-input').focusout(function () {
        debugger;
        IsValidPhoto();
        IsValidSign();
    });

    //Enable Guardian Details Accordion Input Validation
    $('.guardian-details-input').focusout(function () {
        if (IsValidGuardianAccordionInputs())
            $('#guardian-details-accordion-error').addClass('d-none');
    });

    // Enable GST Registration Accordion Input Validation
    $('.gst-registration-input').focusout(function () {
        if (IsValidGstRegistrationAccordionInputs())
            $('#gst-registration-details-accordion-error').addClass('d-none');
    });

    // Enable Commodities Asset Accordion Input Validation
    $('.commodities-asset-input').focusout(function () {
        if (IsValidCommoditiesAssetAccordionInputs())
            $('#commodities-asset-accordion-error').addClass('d-none');
    });

    let previousDocumentType = ''; // Track the previous document type

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   VALIDATION FUNCTIONS FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // 1. Home Branch Accordion Input Validation
    function IsValidHomeBranchAccordionInputs() {
        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-home-branch');
        let note = $('#note-home-branch').val();
        let reasonForModification = $('reason-for-modification-Home').val();
        result = true;

        //note
        if (note === '') {
            $('#note-home-branch').val('None');
            note = 'None';
        }

        //reason For Modification
        if (reasonForModification === '') {
            $('reason-for-modification-Home').val('None');
            reasonForModification == 'None';
        }

        //Activation Date
        if (isValidActivationDate === false) {
            result = false;
        }

        //Home Branch Id
        if ($('#home-branch-id').prop('selectedIndex') < 1) {
            result = false;
        }

        //Language Id
        if ($('#language-id').prop('selectedIndex') < 1) {
            result = false;
        }

        // Show or hide error message based on validation result
        if (result) {
            $('#home-branch-accordion-error').addClass('d-none');
        }
        else {
            $('#home-branch-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // 2.Additional Details Accordion Input Validation
    function IsValidAdditionalDetailsAccordionInputs() {
        debugger;
        result = true;

        let lifePartnerName = $('#life-partner-name').val();
        let transLifePartnerName = $('#trans-life-partner-name').val();
        let lifePartnerMaidenName = $('#life-partner-maiden-name').val();
        let transLifePartnerMaidenName = $('#trans-life-partner-maiden-name').val();

        let nameOfEmployer = $('#employer-name').val();
        let transNameOfEmployer = $('#trans-employer-name').val();
        let annualIncome = parseFloat($('#annual-income').val());
        let epfNumber = $('#epf-number').val();
        let transEpfNumber = $('#trans-epf-number').val();
        let employedSince = parseInt($('#employed-since').val());

        let employerNatureOtherDetails = $('#employer-nature-other-details').val();
        let transEmployerNatureOtherDetails = $('#trans-employer-nature-other-details').val();

        let employerAddressDetails = $('#employer-address-details').val();
        let transEmployerAddressDetails = $('#trans-employer-address-details').val();

        let employerContactDetails = $('#employer-contact-details').val();
        let transEmployerContactDetails = $('#trans-employer-contact-details').val();

        let note = $('#note-additional-detail').val();
        let transNote = $('#trans-note-additional-detail').val();

        let noteEmployement = $('#note-employment-detail').val();
        let transNoteEmployement = $('#trans-note-employment-detail').val();

        let enablePoliticion = $('#enable-politician').is(':checked');
        let politicialBackgroundDetails = $('#politicial-background-details').val();
        let transPoliticialBackgroundDetails = $('#trans-politicial-background-details').val();
        let reasonForModification = $('#reason-for-modification-additional').val();
        let occupation = $('#occupation-id').val();
        let occupationText = $('#occupation-id option:selected').text();

        let vipRank = $('#vip-rank').val();

        // Set Default Value, If Empty
        if (note === '') {
            note = 'None';
        }

        if (transNote === '') {
            transNote = 'None';
        }

        if (noteEmployement === '') {
            note = 'None';
        }

        if (transNoteEmployement === '') {
            transNote = 'None';
        }

        if (reasonForModification === '') {
            reasonForModification = 'None';
        }

        //person-category dropdown
        if ($('#person-category-id').prop('selectedIndex') < 1) {
            result = false;
        }

        if ($('.individual').hasClass('d-none') === false) {
            //birth-city dropdown 
            if ($('#birth-city-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //blood group dropdown 
            if ($('#blood-group-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //poverty status dropdown
            if ($('#poverty-status-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //physical status dropdown
            if ($('#physical-status-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //marital Status dropdown
            if ($('#marital-status-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //cast category dropdown
            if ($('#cast-category-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //educational qualification dropdown
            if ($('#educational-qualification-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //gender dropdown
            if ($('#gender-id').prop('selectedIndex') < 1) {
                result = false;
            }

            // Validate Occupation 
            if ($('#occupation-id').prop('selectedIndex') < 1) {
                // Validate If Person Is Salaried
                if ($('#employee-block').hasClass('d-none') === false) {

                     // Validate Name of Employer
                    minimumLength = parseInt($('#employer-name').attr('minlength'));
                    maximumLength = parseInt($('#employer-name').attr('maxlength'));

                    if (parseInt(nameOfEmployer.length) < parseInt(minimumLength) || parseInt(nameOfEmployer.length) > parseInt(maximumLength)) {
                        result = false;
                    }

                    // Validate Trans Name of Employer
                    minimumLength = parseInt($('#trans-employer-name').attr('minlength'));
                    maximumLength = parseInt($('#trans-employer-name').attr('maxlength'));

                    if (parseInt(transNameOfEmployer.length) < parseInt(minimumLength) || parseInt(transNameOfEmployer.length) > parseInt(maximumLength)) {
                        result = false;
                    }

                    if (IsValidInputDate('#date-of-incorporation') === false) {
                        result = false;
                    }

                    if ($('#employment-type-id').prop('selectedIndex') < 1) {
                        result = false;
                    }

                    if ($('#nature-of-employer-id').prop('selectedIndex') < 1) {
                        result = false;
                    }

                    if ($('#employer-city-id').prop('selectedIndex') < 1) {
                        result = false;
                    }

                    if ($('#designation-id').prop('selectedIndex') < 1) {
                        result = false;
                    }

                    // Annual Income
                    if (isNaN(annualIncome) === false) {

                        minimum = parseFloat($('#annual-income').attr('min'));
                        maximum = parseFloat($('#annual-income').attr('max'));

                        if (parseFloat(annualIncome) < parseFloat(minimum) || parseFloat(annualIncome) > parseFloat(maximum)) {
                            result = false;
                        }
                    }
                    else {
                        result = false;
                    }

                    // Validate EPF Number
                    minimumLength = parseInt($('#epf-number').attr('minlength'));
                    maximumLength = parseInt($('#epf-number').attr('maxlength'));

                    if (parseInt(epfNumber.length) < parseInt(minimumLength) || parseInt(epfNumber.length) > parseInt(maximumLength)) {
                        result = false;
                    }

                    // Validate EPF Number
                    minimumLength = parseInt($('#trans-epf-number').attr('minlength'));
                    maximumLength = parseInt($('#trans-epf-number').attr('maxlength'));

                    if (parseInt(transEpfNumber.length) < parseInt(minimumLength) || parseInt(transEpfNumber.length) > parseInt(maximumLength)) {
                        result = false;
                    }

                    // Annual Income
                    if (isNaN(employedSince) === false) {

                        minimum = parseFloat($('#employed-since').attr('min'));
                        maximum = parseFloat($('#employed-since').attr('max'));
                        if (parseFloat(employedSince) < parseFloat(minimum) || parseFloat(employedSince) > parseFloat(maximum))
                            result = false;
                    }
                    else {
                        result = false;
                    }

                    // Validate Employer Nature Other Details
                    minimumLength = parseInt($('#employer-nature-other-details').attr('minlength'));
                    maximumLength = parseInt($('#employer-nature-other-details').attr('maxlength'));

                    if (parseInt(employerNatureOtherDetails.length) < parseInt(minimumLength) || parseInt(employerNatureOtherDetails.length) > parseInt(maximumLength)) {
                        result = false;
                    }

                    // Validate Trans Employer Nature Other Details
                    minimumLength = parseInt($('#trans-employer-nature-other-details').attr('minlength'));
                    maximumLength = parseInt($('#trans-employer-nature-other-details').attr('maxlength'));

                    if (parseInt(transEmployerNatureOtherDetails.length) < parseInt(minimumLength) || parseInt(transEmployerNatureOtherDetails.length) > parseInt(maximumLength)) {
                        result = false;
                    }

                    // Validate Employer Address Details
                    minimumLength = parseInt($('#employer-address-details').attr('minlength'));
                    maximumLength = parseInt($('#employer-address-details').attr('maxlength'));

                    if (parseInt(employerAddressDetails.length) < parseInt(minimumLength) || parseInt(employerAddressDetails.length) > parseInt(maximumLength)) {
                        result = false;
                    }

                    // Validate Trans Employer Address Details
                    minimumLength = parseInt($('#trans-employer-address-details').attr('minlength'));
                    maximumLength = parseInt($('#trans-employer-address-details').attr('maxlength'));

                    if (parseInt(transEmployerAddressDetails.length) < parseInt(minimumLength) || parseInt(transEmployerAddressDetails.length) > parseInt(maximumLength)) {
                        result = false;
                    }

                    // Validate Employer Contact Details
                    minimumLength = parseInt($('#employer-contact-details').attr('minlength'));
                    maximumLength = parseInt($('#employer-contact-details').attr('maxlength'));

                    if (parseInt(employerContactDetails.length) < parseInt(minimumLength) || parseInt(employerContactDetails.length) > parseInt(maximumLength)) {
                        result = false;
                    }

                    // Validate Trans Employer Contact Details
                    minimumLength = parseInt($('#trans-employer-contact-details').attr('minlength'));
                    maximumLength = parseInt($('#trans-employer-contact-details').attr('maxlength'));

                    if (parseInt(transEmployerContactDetails.length) < parseInt(minimumLength) || parseInt(transEmployerContactDetails.length) > parseInt(maximumLength)) {
                        result = false;
                    }
                }
             }
            //else {
            //    result = false;
            //}
        }

        // Validate If Person Married
        if ($('#married-status-input').hasClass('d-none') === false) {
            // Life Partner Name
            minimumLength = parseInt($('#life-partner-name').attr('minlength'));
            maximumLength = parseInt($('#life-partner-name').attr('maxlength'));

            if (parseInt(lifePartnerName.length) < parseInt(minimumLength) || parseInt(lifePartnerName.length) > parseInt(maximumLength)) {
                result = false;
            }

            // Trans Life Partner Name
            minimumLength = parseInt($('#trans-life-partner-name').attr('minlength'));
            maximumLength = parseInt($('#trans-life-partner-name').attr('maxlength'));

            if (parseInt(transLifePartnerName.length) < parseInt(minimumLength) || parseInt(transLifePartnerName.length) > parseInt(maximumLength)) {
                result = false;
            }

            //Life Partner Maiden Name
            minimumLength = parseInt($('#life-partner-maiden-name').attr('minlength'));
            maximumLength = parseInt($('#life-partner-maiden-name').attr('maxlength'));

            if (parseInt(lifePartnerMaidenName.length) < parseInt(minimumLength) || parseInt(lifePartnerMaidenName.length) > parseInt(maximumLength)) {
                result = false;
            }

            // Trans Life Partner Maiden Name
            minimumLength = parseInt($('#trans-life-partner-maiden-name').attr('minlength'));
            maximumLength = parseInt($('#trans-life-partner-maiden-name').attr('maxlength'));

            if (parseInt(transLifePartnerMaidenName.length) < parseInt(minimumLength) || parseInt(transLifePartnerMaidenName.length) > parseInt(maximumLength)) {
                result = false;
            }

            if (IsValidInputDate('#date-of-marriage') === false) {
                result = false;
            }
        }

        // Check if 'vipRank' is a valid number and not empty
        if (isNaN(vipRank) === true || parseInt(vipRank) < 0 || parseInt(vipRank) > 11) {
            result = false;
        }

        if (enablePoliticion === true) {

            // Validate Politicial Background Details
            maximumLength = parseInt($('#politicial-background-details').attr('minlength'));
            maximumLength = parseInt($('#politicial-background-details').attr('maxlength'));

            if (parseInt(politicialBackgroundDetails.length) < parseInt(minimumLength) || parseInt(politicialBackgroundDetails.length) > parseInt(maximumLength)) {
                result = false;
            }

            // Validate Trans Politicial Background Details
            maximumLength = parseInt($('#trans-politicial-background-details').attr('minlength'));
            maximumLength = parseInt($('#trans-politicial-background-details').attr('maxlength'));

            if (parseInt(transPoliticialBackgroundDetails.length) < parseInt(minimumLength) || parseInt(transPoliticialBackgroundDetails.length) > parseInt(maximumLength)) {
                result = false;
            }
        }

        // Vip Rank
        if (isNaN(vipRank) === false) {

            minimum = parseInt($('#vip-rank').attr('min'));
            maximum = parseInt($('#vip-rank').attr('max'));

            if (parseInt(vipRank) < parseInt(minimum) || parseInt(vipRank) > parseInt(maximum))
                result = false;
        }
        else {
            result = false;
        }

        if (result) {
            $('#person-additional-details-accordion-error').addClass('d-none');
        }
        else {
            $('#person-additional-details-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // 3.Guardian Details Accordion Input Validation
    function IsValidGuardianAccordionInputs() {
        debugger;
        result = true;

        let fullName = $('#guardian-full-name').val();
        let transFullName = $('#trans-guardian-full-name').val();
        let fullAddress = $('#full-address').val();
        let transFullAddress = $('#trans-full-address').val();
        let note = $('#note-guardian').val();
        let transNote = $('#trans-note-guardian').val();
        let reasonForModification = $('#reason-for-modification-guardian').val();
        if ($('#guardian-detail-card').hasClass('d-none') === false) {

            //Relation Guardian Id
            if ($('#guardian-relation-id').prop('selectedIndex') < 1) {
                result = false;
            }

            // Age Proof Submission Status 
            if ($('.age-proof-submission-status:checked').length === 0) {
                result = false;
            }

            // Set Default Value, If Empty
            if (note === '') {
                note = 'None';
            }

            if (transNote === '') {
                transNote = 'None';
            }

            if (reasonForModification === '') {
                reasonForModification = 'None';
            }

            // Assign Default Values If Guardian Is Existing Customer
            if ($('#guardian-pin-input').hasClass('d-none') === false) {
                if (guardianPersonInformationNumber !== '') {
                    fullName = 'None';
                    transFullName = 'None';
                    fullAddress = 'None';
                    transFullAddress = 'None';
                }
                else {
                    fullName = '';
                    transFullName = '';
                    fullAddress = '';
                    transFullAddress = '';

                    result = false;
                }
            }
            else {
                fullName = $('#guardian-full-name').val();
                transFullName = $('#trans-guardian-full-name').val();
                fullAddress = $('#full-address').val();
                transFullAddress = $('#trans-full-address').val();
                guardianPersonInformationNumberText = 'None';
                guardianPersonInformationNumber = '';

                minimumLength = parseInt($('#guardian-full-name').attr('minlength'));
                maximumLength = parseInt($('#guardian-full-name').attr('maxlength'));

                if (parseInt(fullName.length) < parseInt(minimumLength) || parseInt(fullName.length) > parseInt(maximumLength)) {
                    result = false;
                }

                minimumLength = parseInt($('#trans-guardian-full-name').attr('maxlength'));
                maximumLength = parseInt($('#trans-guardian-full-name').attr('maxlength'));

                if (parseInt(transFullName.length) > parseInt(maximumLength)) {
                    result = false;
                }

                minimumLength = parseInt($('#full-address').attr('minlength'));
                maximumLength = parseInt($('#full-address').attr('maxlength'));

                if (parseInt(fullAddress.length) < parseInt(minimumLength) || parseInt(fullAddress.length) > parseInt(maximumLength)) {
                    result = false;
                }

                minimumLength = parseInt($('#trans-full-address').attr('minlength'));
                maximumLength = parseInt($('#trans-full-address').attr('maxlength'));

                if (parseInt(transFullAddress.length) < parseInt(minimumLength) || parseInt(transFullAddress.length) > parseInt(maximumLength)) {
                    result = false;
                }
            }
        }

        if (result) {
            $('#guardian-details-accordion-error').addClass('d-none');
        } else {
            $('#guardian-details-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // 5.Person GST Registration Accordion Input Validation
    function IsValidGstRegistrationAccordionInputs() {
        debugger;
        result = true;

        let thresholdLimit = parseInt($('#threshold-limit').val());
        let gstRegistration = $('#gst-registration-number').val();

        //state Id
        if ($('#state-id').prop('selectedIndex') < 1) {
            result = false;
        }

        //Gst Registration Type Id
        if ($('#gst-registration-type-id').prop('selectedIndex') < 1) {
            result = false;
        }

        //Applicable From
        if (IsValidInputDate('#applicable-from') === false) {
            result = false;
        }

        //state Id
        if ($('#gst-return-periodicity-id').prop('selectedIndex') < 1) {
            result = false;
        }

        // Threshold Limit
        if (isNaN(thresholdLimit) === false) {

            minimum = parseInt($('#threshold-limit').attr('min'));
            maximum = parseInt($('#threshold-limit').attr('max'));

            if (parseInt(thresholdLimit) < parseInt(minimum) || parseInt(thresholdLimit) > parseInt(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Gst Registration
        if (ValidateGSTNumber() === false) {
            result = false;
        }

        //Registration Date
        if (IsValidInputDate('#registrations-date') === false) {
            result = false;
        }

        if (result) {
            $('#gst-registration-details-accordion-error').addClass('d-none');
        }
        else {
            $('#gst-registration-details-accordion-error').removeClass('d-none');
        }

        return result;
    }

    function IsValidDocumentNumber() {
        let myResult = true;
        let documentNumber = $('#kyc-document-number').val();

        $('#kyc-document-number-error1').addClass('d-none');

        // AADHAAR Card
        if (kycDocument === AADHAR_CARD) {
            // Check if the Aadhaar number is exactly 12 digits long
            if (/^\d{12}$/.test(documentNumber) === false) {
                myResult = false;

                $('#kyc-document-number-error1').html('Invalid Aadhaar Number: It Must Be Exactly 12 Digits. (e.g., 999999999999)');
                $('#kyc-document-number-error1').removeClass('d-none');
            }
        }

        // Pan Card
        if (kycDocument === PAN_CARD) {
            // Regular expression for PAN card validation
            const panPattern = /^[A-Z]{5}\d{4}[A-Z]{1}$/;

            // Check if the PAN card number matches the pattern
            if (panPattern.test(documentNumber) === false) {
                myResult = false;

                $('#kyc-document-number-error1').html('Invalid PAN Card Number: It Must Follow The Format XXXXX9999X.');
                $('#kyc-document-number-error1').removeClass('d-none');
            }
        }

        // Voting Card
        if (kycDocument === VOTER_CARD) {
            // Regular expression for Voter Card number validation
            const voterCardPattern = /^[A-Z]{3}\d{7}$/;

            // Check if the Voter Card number matches the pattern
            if (voterCardPattern.test(documentNumber) === false) {
                myResult = false;

                $('#kyc-document-number-error1').html('Invalid Voter Card number: It Must Follow The Format XXX9999999.');
                $('#kyc-document-number-error1').removeClass('d-none');
            }
        }

        // DRIVING License
        if (kycDocument === DRIVING_LICENCE) {
            // Regular expression for Driving License number validation
            const dlPattern = /^[A-Z]{2}\d{2}\d{4}[A-Z0-9]{1,13}$/;

            // Check if the Driving License number matches the pattern
            if (dlPattern.test(documentNumber) === false) {
                myResult = false;

                $('#kyc-document-number-error1').html('Invalid Driving License number: It must follow the format XX123456789012 (State Code, Serial Number, Series).');
                $('#kyc-document-number-error1').removeClass('d-none');
            }
        }

        //PassPort
        if (kycDocument === PASSPORT) {

            // Regular expression for Passport Number validation
            const passportPattern = /^[A-Z]{1}\d{7}$/;

            // Check if the Passport Number matches the pattern
            if (passportPattern.test(documentNumber) === false) {
                myResult = false;

                $('#kyc-document-number-error1').html('Invalid Passport Number: It Must Follow The Format X1234567X (1 letter, 7 digits).');
                $('#kyc-document-number-error1').removeClass('d-none');
            }
        }

        // RATION
        if (kycDocument === RATION_CARD) {
            // Regular expression for Ration Card number validation (State code + 9 digits)
            const rationCardPattern = /^[A-Z]{2}\d{9}$/;

            // Check if the Ration Card number matches the pattern
            if (rationCardPattern.test(documentNumber) === false) {
                myResult = false;

                $('#kyc-document-number-error1').html('Invalid Ration Card number: It must follow the format XX123456789 (2 letters for state code, followed by 9 digits).');
                $('#kyc-document-number-error1').removeClass('d-none');
            }
        }

        // Birth Certificate
        if (kycDocument === BIRTH_CERTIFICATE || kycDocument === SCHOOL_LEAVING_CERTIFICATE || kycDocument === OVERSEAS_CITIZENSHIP_OF_INDIA) {

            // Trim the input value to exactly 50 characters if it exceeds the limit
            if (documentNumber.length === 0 || documentNumber.length > 50) {
                myResult = false;

                $('#kyc-document-number-error1').html('Invalid Document Number.');
                $('#kyc-document-number-error1').removeClass('d-none');
            }
        }

        return myResult;
    }

    // 4.Photo Sign Accordion Input Validation
    function IsValidPhoto() {
        result = true;
        debugger;
        let fileUploader = $('#photo-file-uploader').get(0);

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            debugger;
            if ($('#photo-document-upload').val() === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isChangedPhotoFile === true) {
                    result = false;
                    $('#photo-file-uploader-error').removeClass('d-none');
                }
            }
                // ****** New Changes
            else {
                let photoSrc = $('#photo-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    $('#photo-file-caption').val('NotApplicable');
                    localStoragePath = 'None';
                }
            }
        }
        else {
            $('#photo-file-caption').val('None');
        }

        return result;
    }

    function IsValidSign() {
        debugger;
        result = true;
        let fileUploader = $('#person-sign-file-uploader').get(0);

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            debugger;
            if ($('#sign-document-upload').val() === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isChangedSign === true) {
                    result = false;
                    $('#person-sign-file-uploader-error').removeClass('d-none');
                }
            }
                // ****** New Changes
            else {
                let photoSrc = $('#person-sign-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    $('#person-sign-file-caption').val('NotApplicable');
                    localStoragePath = 'None';
                }
            }
        }
        else {
            $('#person-sign-file-caption').val('None');
        }
        return result;
    }

    // 5.Person GST Registration Accordion Input Validation
    function IsValidGstRegistrationAccordionInputs() {
        debugger;
        result = true;

        let thresholdLimit = parseInt($('#threshold-limit').val());
        let taxAmount = parseFloat($('#tax-amount').val());
        let gstRegistration = $('#gst-registration-number').val();

        let uploadGSTReturnDocument = $('#enable-gst-return-document').is(':checked') ? true : false;
        let registrationDate = $('#registrations-date').val();

        //state Id
        if ($('#state-id').prop('selectedIndex') < 1) {
            result = false;
        }

        //Gst Registration Type Id
        if ($('#gst-registration-type-id').prop('selectedIndex') < 1) {
            result = false;
        }

        //Applicable From
        let isValidApplicableFrom = IsValidInputDate('#applicable-from');

        if (isValidApplicableFrom === false) {
            result = false;
        }

        //state Id
        if ($('#gst-return-periodicity-id').prop('selectedIndex') < 1) {
            result = false;
        }

        // Threshold Limit
        if (isNaN(thresholdLimit) === false) {

            minimum = parseInt($('#threshold-limit').attr('min'));
            maximum = parseInt($('#threshold-limit').attr('max'));

            if (parseInt(thresholdLimit) < parseInt(minimum) || parseInt(thresholdLimit) > parseInt(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Gst Registration
        if (isNaN(gstRegistration.length) === false) {

            minimumLength = parseInt($('#gst-registration-number').attr('minlength'));
            maximumLength = parseInt($('#gst-registration-number').attr('maxlength'));

            if (parseInt(gstRegistration.length) === 0 || parseInt(gstRegistration.length) < parseInt(minimumLength) || parseInt(gstRegistration.length) < parseInt(maximumLength) || parseInt(gstRegistration.length) > parseInt(maximumLength)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        //Registration Date
        let isValidRegistrationDate = IsValidInputDate('#registrations-date');

        if (isValidRegistrationDate === false) {
            result = false;
        }

        if (result) {
            $('#gst-registration-details-accordion-error').addClass('d-none');
        }
        else {
            $('#gst-registration-details-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // 5.Person Commodities Asset Accordion Input Validation
    function IsValidCommoditiesAssetAccordionInputs() {
        let goldOrnament = parseFloat($('#gold-ornaments').val());
        let silverOrnament = parseFloat($('#silver-ornaments').val());
        let platinumOrnament = parseFloat($('#platinum-ornaments').val());
        let diamondInGoldOrnament = parseInt($('#number-of-diamonds-in-gold-ornaments').val());
        let reasonformodification = $('#reason-for-modification-commodities').val();
        result = true;

        if (reasonformodification === '') {
            $('#reason-for-modification-commodities').val('None');
            reasonformodification = 'None';
        }

        // Gold Ornament
        if (isNaN(goldOrnament) === false) {

            minimum = parseFloat($('#gold-ornaments').attr('min'));
            maximum = parseFloat($('#gold-ornaments').attr('max'));

            if (parseFloat(goldOrnament) < parseFloat(minimum) || parseFloat(goldOrnament) > parseFloat(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Silver Ornament
        if (isNaN(silverOrnament) === false) {

            minimum = parseFloat($('#silver-ornaments').attr('min'));
            maximum = parseFloat($('#silver-ornaments').attr('max'));

            if (parseFloat(silverOrnament) < parseFloat(minimum) || parseFloat(silverOrnament) > parseFloat(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Platinum Ornament
        if (isNaN(platinumOrnament) === false) {

            minimum = parseFloat($('#platinum-ornaments').attr('min'));
            maximum = parseFloat($('#platinum-ornaments').attr('max'));

            if (parseFloat(platinumOrnament) < parseFloat(minimum) || parseFloat(platinumOrnament) > parseFloat(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Diamond Gold Ornament
        if (isNaN(diamondInGoldOrnament) === false) {

            minimum = parseInt($('#number-of-diamonds-in-gold-ornaments').attr('min'));
            maximum = parseInt($('#number-of-diamonds-in-gold-ornaments').attr('max'));

            if (parseInt(diamondInGoldOrnament) < parseInt(minimum) || parseInt(diamondInGoldOrnament) > parseInt(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }


        if (result) {
            $('#commodities-asset-accordion-error').addClass('d-none');
        }
        else {
            $('#commodities-asset-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@ Person  Address Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-person-address-dt').click(function (event) {
        event.preventDefault();
        editedAddressTypeId = '';
        SetAddressTypeUniqueDropdownList();
        SetModalTitle('person-address', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-person-address-dt').click(function () {
        debugger;
        SetModalTitle('person-address', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#person-address-modal').modal();

            columnValues = $('#btn-edit-person-address-dt').data('rowindex');
            editedAddressTypeId = columnValues[1];

            SetAddressTypeUniqueDropdownList();


            // Display Value In Modal Inputs
            $('#address-type-id', myModal).val(columnValues[1]);
            $('#flat-door-no', myModal).val(columnValues[3]);
            $('#trans-flat-door-no', myModal).val(columnValues[4]);
            $('#building-name', myModal).val(columnValues[5]);
            $('#trans-building-name', myModal).val(columnValues[6]);
            $('#road-name', myModal).val(columnValues[7]);
            $('#trans-road-name', myModal).val(columnValues[8]);
            $('#area-name', myModal).val(columnValues[9]);
            $('#trans-area-name', myModal).val(columnValues[10]);
            $('#city-id', myModal).val(columnValues[11]);
            $('#residence-type-id', myModal).val(columnValues[13]);
            $('#ownership-type-id', myModal).val(columnValues[15]);
            $('#note-address', myModal).val(columnValues[17]);
            $('#trans-note-address', myModal).val(columnValues[18]);
            $('#reason-for-modification-address', myModal).val(columnValues[19]);

            personAddressPrmKey = columnValues[20];

            // Show Modals
            $('#person-address-modal').modal('show');
        }
        else {
            $('#btn-edit-person-address-dt').addClass('read-only');
            $('#person-address-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-person-address-modal').click(function (event) {
        debugger;
        if (IsValidAddressDataTableModal()) {
            row = addressDataTable.row.add([
                tag,
                addressType,
                addressTypeText,
                flatDoorNo,
                transFlatDoorNo,
                buildingName,
                transBuildingName,
                roadName,
                transRoadName,
                areaName,
                transAreaName,
                city,
                cityText,
                residenceType,
                residenceTypeText,
                residenceOwnership,
                residenceOwnershipText,
                note,
                transNote,
                reasonForModification,
                personAddressPrmKey,
            ]).draw();

            HideAddressDataTableColumns();

            addressDataTable.columns.adjust().draw();
            $('#address-details-accordion-error').addClass('d-none');
            $('#person-address-modal').modal('hide');

            EnableNewOperation('person-address');
        }
    });

    // Modal update Button Event
    $('#btn-update-person-address-modal').click(function (event) {

        $('#select-all-person-address').prop('checked', false);

        if (IsValidAddressDataTableModal()) {
            addressDataTable.row(selectedRowIndex).data([
                tag,
                addressType,
                addressTypeText,
                flatDoorNo,
                transFlatDoorNo,
                buildingName,
                transBuildingName,
                roadName,
                transRoadName,
                areaName,
                transAreaName,
                city,
                cityText,
                residenceType,
                residenceTypeText,
                residenceOwnership,
                residenceOwnershipText,
                note,
                transNote,
                reasonForModification,
                personAddressPrmKey,
            ]).draw();

            HideAddressDataTableColumns();

            addressDataTable.columns.adjust().draw();

            $('#person-address-modal').modal('hide');

            EnableNewOperation('person-address');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-person-address-dt').click(function (event) {

        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-person-address tbody input[type="checkbox"]:checked').each(function () {
                    addressDataTable.row($('#tbl-person-address tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-person-address-dt').data('rowindex');

                    EnableNewOperation('person-address');

                    SetAddressTypeUniqueDropdownList();

                    $('#select-all-person-address').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (addressDataTable.data().any() === false)
                        $('#address-details-accordion-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-person-address').click(function () {

        if ($(this).prop('checked')) {
            $('#tbl-person-address tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = addressDataTable.row(row).index();

                rowData = (addressDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-person-address-dt').data('rowindex', arr);
                EnableDeleteOperation('person-address')
            });
        }
        else {
            EnableNewOperation('person-address')

            $('#tbl-person-address tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-person-address tbody').click('input[type=checkbox]', function () {
        $('#tbl-person-address tbody input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = addressDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (addressDataTable.row(selectedRowIndex).data());

                    personAddressPrmKey = rowData[20];

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('person-address');

                    $('#btn-update-person-address-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-person-address-dt').data('rowindex', rowData);
                    $('#btn-delete-person-address-dt').data('rowindex', arr);
                    $('#select-all-person-address').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-person-address tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('person-address');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1) {

            //[ Modify by  SS :05/09/2024 Amend operation Edit Button Does not work resolve this problem ]
            EnableEditDeleteOperation('person-address');

            //[ Old Code]

            //if (personAddressPrmKey > 1)
            //    EnableDeleteOperation('person-address');
            //else
            //    EnableEditDeleteOperation('person-address');
        }

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('person-address');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-person-address').prop('checked', true);
        else
            $('#select-all-person-address').prop('checked', false);
    });

    // Validate Agent Incentive Module
    function IsValidAddressDataTableModal() {
        debugger;
        result = true;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        addressType = $('#address-type-id option:selected').val();
        addressTypeText = $('#address-type-id option:selected').text();
        flatDoorNo = $('#flat-door-no').val();
        transFlatDoorNo = $('#trans-flat-door-no').val();
        buildingName = $('#building-name').val();
        transBuildingName = $('#trans-building-name').val();
        roadName = $('#road-name').val();
        transRoadName = $('#trans-road-name').val();
        areaName = $('#area-name').val();
        transAreaName = $('#trans-area-name').val();
        city = $('#city-id option:selected').val();
        cityText = $('#city-id option:selected').text();
        residenceType = $('#residence-type-id option:selected').val();
        residenceTypeText = $('#residence-type-id option:selected').text();
        residenceOwnership = $('#ownership-type-id option:selected').val();
        residenceOwnershipText = $('#ownership-type-id option:selected').text();
        note = $('#note-address').val();
        transNote = $('#trans-note-address').val();
        reasonForModification = $('#reason-for-modification-address').val();
        personAddressPrmKey = 0;

        // Set Default Value, If Empty
        if (note === '') {
            note = 'None';
        }

        if (transNote === '') {
            transNote = 'None';
        }

        if (reasonForModification == '') {
            reasonForModification = 'None';
        }

        //Validation Address Type
        if ($('#address-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#address-type-id-error').removeClass('d-none')
        }

        // ******** minimumLength  maximumLength
        //Validation FlatDoor No Min Length  And Max Length 
        if (isNaN(flatDoorNo.length) === false) {

            minimumLength = parseInt($('#flat-door-no').attr('minlength'));
            maximumLength = parseInt($('#flat-door-no').attr('maxlength'));

            if (parseInt(flatDoorNo.length) < parseInt(minimumLength) || parseInt(flatDoorNo.length) > parseInt(maximumLength)) {
                result = false;
                $('#flat-door-no-error').removeClass('d-none');
            }

        } else {
            result = false;
            $('#flat-door-no-error').addClass('d-none');
        }

        if (isNaN(transFlatDoorNo.length) === false) {

            maximumLength = parseInt($('#trans-flat-door-no').attr('maxlength'));

            if (parseInt(transFlatDoorNo.length) === 0 || parseInt(flatDoorNo.length) > parseInt(maximumLength)) {
                result = false;
                $('#trans-flat-door-no-error').removeClass('d-none');
            }

        } else {
            result = false;
            $('#trans-flat-door-no-error').addClass('d-none');
        }

        //Validation Building Name
        if (isNaN(buildingName.length) === false) {

            minimumLength = parseInt($('#building-name').attr('minlength'));
            maximumLength = parseInt($('#building-name').attr('maxlength'));

            if (parseInt(buildingName.length) === 0 || parseInt(buildingName.length) < parseInt(minimumLength) || parseInt(buildingName.length) > parseInt(maximumLength)) {
                result = false;
                $('#building-name-error').removeClass('d-none');
            }
        } else {
            result = false;
            $('#building-name-error').addClass('d-none');
        }

        //Validation Trans Building Name
        if (isNaN(transBuildingName.length) === false) {

            minimumLength = parseInt($('#trans-building-name').attr('minlength'));
            maximumLength = parseInt($('#trans-building-name').attr('maxlength'));

            if (parseInt(transBuildingName.length) === 0 || parseInt(transBuildingName.length) < parseInt(minimumLength) || parseInt(transBuildingName.length) > parseInt(maximumLength)) {
                result = false;
                $('#trans-building-name-error').removeClass('d-none');
            }
        } else {
            result = false;
            $('#trans-building-name-error').addClass('d-none');
        }

        //Validation Road Name
        if (isNaN(roadName.length) === false) {
            minimumLength = parseInt($('#road-name').attr('minlength'));
            maximumLength = parseInt($('#road-name').attr('maxlength'));

            if (parseInt(roadName.length) === 0 || parseInt(roadName.length) < parseInt(minimumLength) || parseInt(roadName.length) > parseInt(maximumLength)) {
                result = false;
                $('#road-name-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#road-name-error').addClass('d-none');
        }

        //Validation Road Name
        if (isNaN(transRoadName.length) === false) {

            minimumLength = parseInt($('#trans-road-name').attr('minlength'));
            maximumLength = parseInt($('#trans-road-name').attr('maxlength'));

            if (parseInt(transRoadName.length) === 0 || parseInt(transRoadName.length) < parseInt(minimumLength) || parseInt(transRoadName.length) > parseInt(maximumLength)) {
                result = false;
                $('#trans-road-name-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#trans-road-name-error').addClass('d-none');
        }

        //Validation Area Name
        if (isNaN(areaName.length) === false) {

            minimumLength = parseInt($('#area-name').attr('minlength'));
            maximumLength = parseInt($('#area-names').attr('maxlength'));

            if (parseInt(areaName.length) === 0 || parseInt(areaName.length) < parseInt(minimumLength) || parseInt(areaName.length) > parseInt(maximumLength)) {
                result = false;
                $('#area-name-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#area-name-error').removeClass('d-none');
        }

        //Validation Trans Area Name
        if (isNaN(transAreaName.length) === false) {

            minimumLength = parseInt($('#trans-area-name').attr('minlength'));
            maximumLength = parseInt($('#trans-area-name').attr('maxlength'));

            if (parseInt(transAreaName.length) === 0 || parseInt(transAreaName.length) < parseInt(minimumLength) || parseInt(transAreaName.length) > parseInt(maximumLength)) {
                result = false;
                $('#trans-area-name-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#trans-area-name-error').removeClass('d-none');
        }

        //Validation City
        if ($('#city-id').prop('selectedIndex') < 1) {
            result = false;
            $('#city-id-error').removeClass('d-none')
        }

        //Validation Residence Type
        if ($('#residence-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#residence-type-id-error').removeClass('d-none')
        }

        //Validation Residence Ownership
        if ($('#ownership-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#ownership-type-id-error').removeClass('d-none')
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideAddressDataTableColumns() {
        addressDataTable.column(1).visible(false);
        addressDataTable.column(11).visible(false);
        addressDataTable.column(13).visible(false);
        addressDataTable.column(15).visible(false);
        addressDataTable.column(19).visible(false);
        addressDataTable.column(20).visible(false);
    }

    //Address Type Unique Dropdown
    function SetAddressTypeUniqueDropdownList() {

        // Show All List Items
        $('#address-type-id').html('');
        $('#address-type-id').append(ADDRESS_TYPE_DROPDOWN_LIST);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-person-address > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (addressDataTable.row(currentRow).data());

            if (typeof myColumnValues !== 'undefined' && myColumnValues !== null) {

                if (myColumnValues[1] !== editedAddressTypeId)
                    $('#address-type-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
    }

    // @@@@@@@@@@@@@@@@@@@@@@  Validation(Event Handling) Code - Contact Details @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Verification Code Visibility & Input Type Visibility Based On Contact Type
    $('#contact-type').focusout(function (event) {
        debugger;

        let currentValue = $(this).val();

        // If the value has changed from the initial value, clear the related fields
        if (currentValue !== lastSelectedValue) {
            // Clear input fields and reset verification state
            $('#field-value').val('');
            $('#is-verified').prop('checked', false);
            $('#note-contact-detail').val('');
            $('.modal-input-error').addClass('d-none');
        }
        lastSelectedValue = currentValue;

        // Get The Selected Contact Type
        contactTypeId = $('#contact-type option:selected').val();

        // Contact Type Wise Show Hide Inputs
        $.get('/PersonChildAction/GetSysNameOfContactTypeById', { _contactTypeId: contactTypeId, async: false }, function (data, textStatus, jqXHR) {
            debugger;
            sysNameOfContactType = data;
        });

        // Add Field Value Attribute Type As Number For Mobile Contact Type
        contactType = $('#contact-type option:selected').text();
        isMobile = contactType.includes('Mobile');
        isEmail = contactType.includes('Email');

        // Add Field Value Attribute Type As Number For Mobile Contact Type
        if (isMobile) {
            $('#field-value').attr('type', 'number');
            $('.is-verified-field').addClass('d-none');
            $('#send-code').removeClass('d-none');
            $('.verification-code').removeClass('d-none');
            $('#verification-token-error').addClass('d-none');
        }
        else {
            $('#field-value').removeAttr('type');
            $('#send-code').addClass('d-none');
            $('.is-verified-field').removeClass('d-none');
            $('#resend').addClass('d-none');
            $('.verification-code').addClass('d-none');
            $('#verification-code').val('0');
        }
    });

    // Sms Send - Display SMS Delilety Status
    $('#send-code').click(function (event) {
        debugger;
        let _mobileNumber = $('#field-value').val();
        $('#send-code').addClass('d-none');

        $.get('/SMS/SendVerificationToken', { MobileNumber: _mobileNumber, async: false }, function (data, textStatus, jqXHR) {
            if (data === 'success') {
                $('#t-body').removeClass('bg-danger');
                $('#t-body').addClass('bg-success');

                $('#t-icon').removeClass('fa-times');
                $('#t-icon').addClass('fa-check');

                $('#t-text').text('Sms Send Successfully');
            }
            else {
                $('#t-body').addClass('bg-danger');
                $('#t-body').removeClass('bg-success');

                $('#t-icon').addClass('fa-times');
                $('#t-icon').removeClass('fa-check');

                $('#t-text').text('Sms Sending Failed');
            }

            $('#resend').removeClass('d-none');

            // For Display Toaster Message
            $('.link').fadeOut('slow').delay(30000).fadeIn('slow');
            $('#myToast').toast('show').css({ 'z-index': '100', 'margin-top': '1%' });
        });
    });

    // Sms Resend - Display SMS Delilety Status
    $('#resend').click(function (event) {
        let _mobileNumber = $('#field-value').val()

        $.get('/SMS/SendVerificationToken', { MobileNumber: _mobileNumber, async: false }, function (data, textStatus, jqXHR) {
            if (data === 'success') {
                $('#t-body').removeClass('bg-danger');
                $('#t-body').addClass('bg-success');

                $('#t-icon').removeClass('fa-times');
                $('#t-icon').addClass('fa-check');

                $('#t-text').text('Sms Send Successfully');
                $('#send-code').addClass('d-none');
                $('#resend').removeClass('d-none');
            }

            // For Display Toaster Message
            $('.link').fadeOut('slow').delay(30000).fadeIn('slow');
            $('#myToast').toast('show').css({ 'z-index': '100', 'margin-top': '1%' });
        });
    });

    //Field Value Validation Based On Contact Types
    $('#field-value').focusout(function (event) {
        debugger;
        let inputValue = $(this).val();
        let contactTypes = $('#contact-type').val();
        let errorMessage = '';
        // Clear any previous error message
        $('#field-value-error').addClass('d-none');

        // Fetch sysNameOfContactType based on selected contact type
        $.get('/PersonChildAction/GetSysNameOfContactTypeById', { _contactTypeId: contactTypes, async: false }, function (sysNameOfContactType) {

            let validations = {
                [WHATS_APP_NUMBER]: {
                    regex: /^\d{10}$/,
                    message: 'Please Enter Valid 10 Digit Mobile Number For WhatsApp.'
                },
                [MOBILE]: {
                    regex: /^\d{10}$/,
                    message: 'Please Enter Valid 10 Digit Mobile Number.'
                },
                [WORK_EMAIL]: {
                    regex: /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/,
                    message: 'Please Enter Valid Email.'
                },
                [HOME_MAIL]: {
                    regex: /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/,
                    message: 'Please Enter Valid Email.'
                },
                [OTHER_MAIL]: {
                    regex: /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/,
                    message: 'Please Enter Valid Email.'
                }
            };

            // Check the validation rules
            let validation = validations[sysNameOfContactType];
            if (validation && !validation.regex.test(inputValue)) {
                errorMessage = validation.message;
                $('#field-value-error').removeClass('d-none').text(errorMessage);
            } else {
                $('#field-value-error').addClass('d-none');
            }
        });
        // If Contact Type Is Mobile
        if (isMobile) {
            $('#verification-code').val('');

            if ($('#field-value').val().length === 10) {
                // Check Whether Enter Mobile Number Is Existed Or Not
                filteredData = contactDataTable.rows().indexes().filter(function (value, index) {
                    return contactDataTable.row(value).data()[3] == $('#field-value').val();

                });

                if (contactDataTable.rows(filteredData).count() > 0 && editedContactNumber !== $('#field-value').val()) {
                    isDuplicateContact = true;
                    $('#field-value-duplicate-error').removeClass('d-none');
                }
                else {
                    $('#field-value-duplicate-error').addClass('d-none');
                    isDuplicateContact = false;
                }

                if (isDuplicateContact === false) {
                    $('#send-code').removeClass('d-none');
                    $('#resend').addClass('d-none');
                    $('#field-value-error').addClass('d-none');
                }
            }
            else {
                $('#send-code').addClass('d-none');
                $('#field-value-error').addClass('d-none');
            }
        }
        else {
            isDuplicateContact = false;
            $('#field-value-error').addClass('d-none');
            $('#field-value-duplicate-error').addClass('d-none');
            $('#verification-code').val('0');
            $('#send-code').addClass('d-none');
        }
    });

    $('#verification-code').focusout(function () {

        if ($(this).val() > 0)
            $('#verification-token-error').addClass('d-none');
    });

    function ResendSMS() {
        let mobileNumber = $('#field-value').val()
        debugger;
        $.get('/SMS/ReSendTeleVerificataionToken', { MobileNumber: mobileNumber, async: false }, function (data, textStatus, jqXHR) {
            debugger;
            if (data === 'success') {
                $(".link").fadeOut('slow').delay(30000).fadeIn("slow");
                $("#myToast").toast('show').css({ "z-index": "100", 'margin-top': "1%" });
            }
        });

    }

    // @@@@@@@@@@@@@@@@@@@@@@   Contact Details - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-contact-dt').click(function () {
        event.preventDefault();
        editedContactNumber = 0;
        isMobile = false;
        isEmail = false;
        sysNameOfContactType = '';
        $('#field-value-error').text('');
        $('#btn-add-contact-modal').removeClass('read-only');
        $('#send-code').addClass('d-none');
        $('#resend').addClass('d-none');
        $('.verification-code').addClass('d-none');
        SetModalTitle('contact', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-contact-dt').click(function () {
        debugger
        SetModalTitle('contact', 'Edit');
        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-contact-dt').data('rowindex');
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#contact-modal').modal();

            //// Display Value In Modal Inputs
            isMobile = columnValues[2].includes('Mobile');
            isEmail = columnValues[2].includes('Email');

            $('#resend').addClass('d-none');

            $('#send-code').removeClass('d-none');

            // Add Field Value Attribute Type As Number For Mobile Contact Type
            if (isMobile) {
                $('#field-value').attr('type', 'number');
                $('.is-verified-field').addClass('d-none');
                $('#send-code').removeClass('d-none');
                $('.verification-code').removeClass('d-none');
                $('#verification-code').val('');
            }
            else {
                $('#field-value').removeAttr('type');
                $('#send-code').addClass('d-none');
                $('.is-verified-field').removeClass('d-none');
                $('#resend').addClass('d-none');
                $('.verification-code').addClass('d-none');
                $('#verification-code').val('0');
            }

            // Display Value In Modal Inputs
            $('#contact-type', myModal).val(columnValues[1]);
            $('#field-value', myModal).val(columnValues[3]);
            lastSelectedValue = columnValues[1]
            editedContactNumber = columnValues[3];

            // Set Maximum Attributes
            $.get('/PersonChildAction/GetSysNameOfContactTypeById', { _contactTypeId: columnValues[1], async: false }, function (data) {
                debugger
                sysNameOfContactType = data;

                // Set Maximum Attributes
                if (WHATS_APP_NUMBER.includes(sysNameOfContactType)) { inputField.attr({ 'type': 'number', 'maxlength': 10 }); }
            });

            $('#is-verified').prop('checked', columnValues[4].toString().toLowerCase() === 'true' ? true : false);

            $('#verification-code', myModal).val('');
            $('#note-contact-detail', myModal).val(columnValues[6]);
            $('#reason-for-modification-contact', myModal).val(columnValues[7]);

            // Show Modals
            $('#contact-modal').modal('show');
        }
        else {
            $('#btn-edit-contact-dt').addClass('read-only');
            $('#contact-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-contact-modal').click(function (event) {
        $('#verification-token-error').addClass('d-none');

        if (IsValidContactDataTableModal()) {
            $('#btn-add-contact-modal').prop('disabled', true);
            if (isMobile) {
                $.get('/SMS/ValidateVerificationToken', { MobileNumber: fieldValue, Token: verificationCode, async: false }, function (data, textStatus, jqXHR) {
                    if (data) {
                        $('#verification-token-error').addClass('d-none');

                        row = contactDataTable.row.add([
                        tag,
                        contactType,
                        contactTypeText,
                        fieldValue,
                        isVerified,
                        verificationCode,
                        note,
                        reasonForModification,
                        personAddressPrmKey,
                        ]).draw();

                        HideContactDataTableColumns();

                        contactDataTable.columns.adjust().draw();

                        // Hide Table Required Data Error Message
                        $('#contact-accordion-error').addClass('d-none');

                        $('#contact-modal').modal('hide');

                        EnableNewOperation('contact');
                    }
                    else {
                        $('#verification-token-error').removeClass('d-none');
                    }

                    $('#btn-add-contact-modal').prop('disabled', false);
                })
            }
            else {
                $('#verification-token-error').addClass('d-none');

                row = contactDataTable.row.add([
                tag,
                contactType,
                contactTypeText,
                fieldValue,
                isVerified,
                verificationCode,
                note,
                reasonForModification,
                personAddressPrmKey,
                ]).draw();

                HideContactDataTableColumns();

                contactDataTable.columns.adjust().draw();

                // Hide Table Required Data Error Message
                $('#contact-accordion-error').addClass('d-none');

                $('#contact-modal').modal('hide');

                EnableNewOperation('contact');

                $('#btn-add-contact-modal').prop('disabled', false);
            }
        }
    });

    // Modal update Button Event
    $('#btn-update-contact-modal').click(function (event) {
        $('#select-all-contact').prop('checked', false);

        if (IsValidContactDataTableModal()) {
            if (isMobile) {
                $.get('/SMS/ValidateVerificationToken', { MobileNumber: fieldValue, Token: verificationCode, async: false }, function (data, textStatus, jqXHR) {
                    if (data) {
                        $('#verification-token-error').addClass('d-none');

                        row = contactDataTable.row(selectedRowIndex).data([
                        tag,
                        contactType,
                        contactTypeText,
                        fieldValue,
                        isVerified,
                        verificationCode,
                        note,
                        reasonForModification,
                        personAddressPrmKey,
                        ]).draw();

                        HideContactDataTableColumns();

                        contactDataTable.columns.adjust().draw();

                        // Hide Table Required Data Error Message
                        $('#contact-accordion-error').addClass('d-none');

                        $('#contact-modal').modal('hide');

                        EnableNewOperation('contact');
                    }
                    else {
                        $('#verification-token-error').removeClass('d-none');
                    }
                })
            }
            else {
                $('#verification-token-error').addClass('d-none');

                row = contactDataTable.row(selectedRowIndex).data([
                tag,
                contactType,
                contactTypeText,
                fieldValue,
                isVerified,
                verificationCode,
                note,
                reasonForModification,
                personAddressPrmKey,
                ]).draw();

                HideContactDataTableColumns();

                contactDataTable.columns.adjust().draw();

                // Hide Table Required Data Error Message
                $('#contact-accordion-error').addClass('d-none');

                $('#contact-modal').modal('hide');

                EnableNewOperation('contact');
            }
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-contact-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-contact tbody input[type="checkbox"]:checked').each(function () {
                    contactDataTable.row($('#tbl-contact tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-contact-dt').data('rowindex');
                    EnableNewOperation('contact');

                    $('#select-all-contact').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (contactDataTable.data().any() === false)
                        $('#contact-accordion-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#select-all-contact').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-contact tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = contactDataTable.row(row).index();

                rowData = (contactDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-contact-dt').data('rowindex', arr);
                EnableDeleteOperation('contact')
            });
        }
        else {
            EnableNewOperation('contact')

            $('#tbl-contact tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-contact tbody').click('input[type=checkbox]', function () {
        $('#tbl-contact input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');

                selectedRowIndex = contactDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (contactDataTable.row(selectedRowIndex).data());

                    contactDetailPrmKey = rowData[8];

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('contact');

                    $('#btn-update-contact-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-contact-dt').data('rowindex', rowData);
                    $('#btn-delete-contact-dt').data('rowindex', arr);
                    $('#select-all-contact').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-contact tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('contact');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1) {

            //[ Modify by  SS :05/09/2024 Amend operation Edit Button Does not work resolve this problem ]
            EnableEditDeleteOperation('contact');

        }


        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('contact');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-contact').prop('checked', true);
        else
            $('#select-all-contact').prop('checked', false);
    });

    // Validate Contact Module
    function IsValidContactDataTableModal() {
        debugger;
        result = true;
        $('#field-value-error').text('');

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        contactType = $('#contact-type option:selected').val();
        contactTypeText = $('#contact-type option:selected').text();
        fieldValue = $('#field-value').val();

        isVerified = $('#is-verified').is(':checked') ? true : false;
        note = $('#note-contact-detail').val();
        verificationCode = $('#verification-code').val();
        personAddressPrmKey = 0;
        reasonForModification = $('#reason-for-modification-contact').val();

        // Set Default Value, If Empty
        if (note === '') {
            note = 'None';
        }

        if (reasonForModification == '') {
            reasonForModification = 'None';
        }

        //vehicle model Id
        if ($('#contact-type').prop('selectedIndex') < 1) {
            result = false;
            $('#contact-type-error').removeClass('d-none');
        }

        // Validate If Contact Type Is Mobile
        if (isMobile) {
            // Define a regular expression pattern for a 10-digit mobile number
            let regex = /^\d{10}$/;
            let verificationCode = $('#verification-code').val();


            let filteredData = contactDataTable.rows().indexes().filter(function (value, index) {
                return contactDataTable.row(value).data()[3] == $('#field-value').val();
            });

            if (contactDataTable.rows(filteredData).count() > 0 && editedContactNumber !== $('#field-value').val()) {
                isDuplicateContact = true;
                result = false;
                $('#field-value-duplicate-error').removeClass('d-none');
            }
            else {
                isDuplicateContact = false;
                $('#field-value-duplicate-error').addClass('d-none');
            }


            // mobileNumber
            if (!regex.test(fieldValue)) {
                result = false;
                $('#field-value-error').removeClass('d-none');
            }
            else
                $('#field-value-error').addClass('d-none');

            // Mobile OTP Validation
            if (verificationCode === '' || verificationCode === '0') {
                result = false;
                $('#verification-token-error').removeClass('d-none');
            }
        }
        else {
            if (fieldValue === '' || parseInt(fieldValue.length) > 320) {
                result = false;
                $('#verification-token-error').addClass('d-none');
                $('#field-value-error').removeClass('d-none').text('Please Enter Field Value.');

            } else {
                verificationCode = '0';
                $('#verification-code').val(verificationCode);
            }

        }

        let inputValue = $('#field-value').val();
        let contactTypes = $('#contact-type').val();

        $.get('/PersonChildAction/GetSysNameOfContactTypeById', { _contactTypeId: contactTypes, async: false }, function (data) {

            sysNameOfContactType = data;
        });


        // Check if inputValue is empty
        if (inputValue === "") {
            result = false;
            $('#field-value-error').removeClass('d-none').text('Please Enter Field Value.');
        } else {
            // WhatsApp Number Validation
            if (sysNameOfContactType === WHATS_APP_NUMBER) {
                if (!/^\d{10}$/.test(inputValue)) {
                    result = false;
                    $('#field-value-error').removeClass('d-none').text('Please Enter Valid 10 Digit Mobile Number For WhatsApp.');
                } else {
                    $('#field-value-error').addClass('d-none');
                }
            }

                // Mobile Number Validation
            else if (sysNameOfContactType === MOBILE) {
                if (!/^\d{10}$/.test(inputValue)) {
                    result = false;
                    $('#field-value-error').removeClass('d-none').text('Please Enter Valid 10 Digit Mobile Number.');
                } else {
                    $('#field-value-error').addClass('d-none');
                }
            }

                // Home Email
            else if (sysNameOfContactType === HOME_MAIL) {
                let emailRegex = /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/;

                if (emailRegex.test(inputValue)) {
                    $('#field-value-error').addClass('d-none'); // Clear error if valid email is entered
                } else {
                    result = false;
                    $('#field-value-error').removeClass('d-none').text('Please Enter Valid Email.');
                }
            }

                // Other Email Types
            else if (sysNameOfContactType === OTHER_MAIL || sysNameOfContactType === WORK_EMAIL) {
                let emailRegex = /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/;

                if (emailRegex.test(inputValue)) {
                    $('#field-value-error').addClass('d-none'); // Clear error if valid email is entered
                } else {
                    result = false;
                    $('#field-value-error').removeClass('d-none').text('Please Enter Valid Email.');
                }
            }
        }


        return result;
    }

    // Hide Unnecessary Columns
    function HideContactDataTableColumns() {
        contactDataTable.column(1).visible(false);
        contactDataTable.column(7).visible(false);
        contactDataTable.column(8).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person KYC Document - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-kyc-document-dt').click(function (event) {
        debugger;
        editedSequenceNumber = 0;
        editedDocument = 0;
        event.preventDefault();
        isDbRecord = false;
        editedDocumentId = '';
        documentID = '';
        previousSelectedDocumentId = '';
        personKYCDetailDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';

        kycDocument = '';
        $('#kyc-document-number-error1').html('This Is Required.').addClass('d-none');

        SetModalTitle('kyc-document', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-kyc-document-dt').click(function () {
        debugger;
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('kyc-document', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#kyc-document-modal').modal();

            columnValues = $('#btn-edit-kyc-document-dt').data('rowindex');

            kYCDateOfIssueDate = new Date(columnValues[8]);
            kYCExpiryDate = new Date(columnValues[9]);
            kYCDateOfRequestDate = new Date(columnValues[12]);
            kYCDateOfExpectingSubmitDate = new Date(columnValues[13]);
            kYCDateOfSubmitDate = new Date(columnValues[14]);

            $('#date-of-submit').attr('min', GetInputDateFormat(kYCDateOfRequestDate));
            $('#date-of-expecting-submit').attr('min', GetInputDateFormat(kYCDateOfRequestDate));

            documentID = columnValues[3];
            editedDocumentId = columnValues[3];
            previousSelectedDocumentId = columnValues[3];

            SetDocumentNumber();

            // Display Value In Modal Inputs
            $('#kyc-document-type-id', myModal).val(columnValues[1]);
            $('#kyc-document-id', myModal).val(columnValues[3]);
            $('#kyc-document-name', myModal).val(columnValues[5]);
            $('#kyc-document-number', myModal).val(columnValues[6]);
            $('#kyc-document-sequence-number', myModal).val(columnValues[7]);
            $('#kyc-document-date-of-issue', myModal).val(GetInputDateFormat(kYCDateOfIssueDate));
            $('#kyc-document-date-of-expiry', myModal).val(GetInputDateFormat(kYCExpiryDate));
            $('#kyc-document-issuing-authority', myModal).val(columnValues[10]);
            $('#kyc-document-place-of-issue', myModal).val(columnValues[11]);
            $('#date-of-request', myModal).val(GetInputDateFormat(kYCDateOfRequestDate));
            $('#date-of-expecting-submit', myModal).val(GetInputDateFormat(kYCDateOfExpectingSubmitDate));
            $('#date-of-submit', myModal).val(GetInputDateFormat(kYCDateOfSubmitDate));
            documentUploadStatus = columnValues[15].split('--->');

            $('#kyc-file-caption', myModal).val(columnValues[19]);
            $('#note-kyc-document', myModal).val(columnValues[20]);
            $('#reason-for-modification-kyc-document', myModal).val(columnValues[21]);

            SetDocumentDropdownList();
            editedSequenceNumber = columnValues[7];

            // Display Value In Modal Inputs
            $('.document-upload-status[value="' + columnValues[15] + '"]').prop('checked', true);

            fileUploader = $('#' + $(columnValues[17]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'kyc-file-uploader';

            // columnValues[17] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[17]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[17]).attr('class') === 'db-record' ? true : false;

            // columnValues[18] - Image Tag Html
            filePath = $('#' + $(columnValues[18]).attr('id')).attr('src');

            fileNameDocument = columnValues[22];
            personKYCDetailDocumentPrmKey = columnValues[23];
            localStoragePath = columnValues[24];

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                AttachFileUploader();
            }

            $('#kyc-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#kyc-document-modal').modal('show');
        }
        else {
            $('#btn-edit-kyc-document-edit-dt').addClass('read-only');
            $('#kyc-document-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-kyc-document-modal').click(function (event) {
        debugger;
        if (IsValidKycDocumentModal()) {
            row = personKycDataTable.row.add([
                        tag,
                        documentType,
                        documentTypeText,
                        document,
                        documentText,
                        nameAsPerDocument,
                        documentNumber,
                        sequenceNumber,
                        dateOfIssue,
                        dateOfExpiry,
                        issuingAuthority,
                        placeOfIssue,
                        dateOfRequest,
                        dateOfExpectingSubmit,
                        dateOfSubmit,
                        documentUploadStatus,
                        documentUploadStatusText,
                        fileUploaderInputHtml,
                        imageTagHtml,
                        fileCaption,
                        note,
                        reasonForModification,
                        fileNameDocument,
                        personKYCDetailDocumentPrmKey,
                        localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            SetGSTRegistrationDetail();

            // Error Message In Span
            $('#kyc-document-data-table-error').addClass('d-none');

            HideColumnsKycDocumentDataTable();

            personKycDataTable.columns.adjust().draw();

            $('#kyc-document-modal').modal('hide');

            EnableNewOperation('kyc-document');
        }
    });

    // Modal update Button Event
    $('#btn-update-kyc-document-modal').click(function (event) {
        debugger;
        $('#select-all-kyc-document').prop('checked', false);
        if (IsValidKycDocumentModal()) {
            personKycDataTable.row(selectedRowIndex).data([
                          tag,
                          documentType,
                          documentTypeText,
                          document,
                          documentText,
                          nameAsPerDocument,
                          documentNumber,
                          sequenceNumber,
                          dateOfIssue,
                          dateOfExpiry,
                          issuingAuthority,
                          placeOfIssue,
                          dateOfRequest,
                          dateOfExpectingSubmit,
                          dateOfSubmit,
                          documentUploadStatus,
                          documentUploadStatusText,
                          fileUploaderInputHtml,
                          imageTagHtml,
                          fileCaption,
                          note,
                          reasonForModification,
                          fileNameDocument,
                          personKYCDetailDocumentPrmKey,
                          localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideColumnsKycDocumentDataTable();

            personKycDataTable.columns.adjust().draw();

            columnValues = $('#btn-update-kyc-document').data('rowindex');

            SetGSTRegistrationDetail();

            $('#kyc-document-modal').modal('hide');

            EnableNewOperation('kyc-document');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-kyc-document-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-kyc-document tbody input[type="checkbox"]:checked').each(function () {
                    personKycDataTable.row($('#tbl-kyc-document tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-kyc-document-dt').data('rowindex');

                    EnableNewOperation('kyc-document');

                    $('#select-all-kyc-document').prop('checked', false);

                    SetGSTRegistrationDetail();
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-kyc-document').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-kyc-document tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = personKycDataTable.row(row).index();

                rowData = (personKycDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-kyc-document-dt').data('rowindex', arr);
                EnableDeleteOperation('kyc-document');
            });
        }
        else {
            EnableNewOperation('kyc-document');

            $('#tbl-kyc-document tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-kyc-document tbody').click("input[type=checkbox]", function () {

        $('#tbl-kyc-document input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                let row = $(this).closest('tr');
                selectedRowIndex = personKycDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (personKycDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('kyc-document');

                    $('#btn-update-kyc-document-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-kyc-document-dt').data('rowindex', rowData);
                    $('#btn-delete-kyc-document-dt').data('rowindex', arr);
                    $('#select-all-kyc-document').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-kyc-document tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('kyc-document');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('kyc-document');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('kyc-document');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-kyc-document').prop('checked', true);
        else
            $('#select-all-kyc-document').prop('checked', false);
    });

    // Validate Kyc Module
    function IsValidKycDocumentModal() {
        debugger;
        result = true;
        counter++;
        fileUploaderId = 'data-table-kyc-file-uploader' + counter;
        fileId = "photo-Id" + counter;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        documentType = $('#kyc-document-type-id option:selected').val();
        documentTypeText = $('#kyc-document-type-id option:selected').text();
        document = $('#kyc-document-id option:selected').val();
        documentText = $('#kyc-document-id option:selected').text();
        nameAsPerDocument = $('#kyc-document-name').val();
        documentNumber = $('#kyc-document-number').val();
        sequenceNumber = $('#kyc-document-sequence-number').val();
        dateOfIssue = $('#kyc-document-date-of-issue').val();
        dateOfExpiry = $('#kyc-document-date-of-expiry').val();
        issuingAuthority = $('#kyc-document-issuing-authority').val();
        placeOfIssue = $('#kyc-document-place-of-issue').val();
        dateOfRequest = $('#date-of-request').val();
        dateOfExpectingSubmit = $('#date-of-expecting-submit').val();
        dateOfSubmit = $('#date-of-submit').val();
        documentUploadStatus = $('.document-upload-status:checked').val();
        documentUploadStatusText = $('.document-upload-status:checked').next('label').text();
        fileCaption = $('#kyc-file-caption').val();
        note = $('#note-kyc-document').val();
        reasonForModification = $('#reason-for-modification-kyc-document').val();

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#kyc-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';

        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        fileUploader = $('#kyc-file-uploader').get(0);

        //set default values
        if (note === '') {
            note = 'None';
        }

        if (fileCaption === '') {
            fileCaption = 'None';
        }

        if (reasonForModification === '') {
            reasonForModification = 'None';
        }

        let filteredData = personKycDataTable
            .rows()
            .indexes()
            .filter(function (value, index) {
                return personKycDataTable.row(value).data()[7] == $('#kyc-document-sequence-number').val();
            });

        if (personKycDataTable.rows(filteredData).count() > 0 && editedSequenceNumber !== $('#kyc-document-sequence-number').val()) {
            isDuplicateSequenceNumber = true;
            result = false;
            $('#kyc-document-sequence-number-error').removeClass('d-none');
        }
        else {
            isDuplicateSequenceNumber = false;
        }

        //Document type
        if ($('#kyc-document-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#kyc-document-type-id-error').removeClass('d-none');
        }

        //Document Document type
        if ($('#kyc-document-id').prop('selectedIndex') < 1 || isDuplicateDocument == true) {
            result = false;

            if (isDuplicateDocument) {
                $('#document-document-id-error').removeClass('d-none');
            } else {
                $('#kyc-document-id-error').removeClass('d-none');
            }
        }

        //Name As Per Document
        if (isNaN(nameAsPerDocument.length) === false) {

            minimumLength = parseInt($('#kyc-document-name').attr('minlength'));
            maximumLength = parseInt($('#kyc-document-name').attr('maxlength'));

            if (parseInt(nameAsPerDocument.length) < parseInt(minimumLength) || parseInt(nameAsPerDocument.length) > parseInt(maximumLength)) {
                result = false;
                $('#kyc-document-name-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#kyc-document-name-error').removeClass('d-none');
        }

        //Document Number
        // To Validate Document Number It Required To Clear
        previousSelectedDocumentId = '';

        if ($('#kyc-document-id').prop('selectedIndex') > 0) {
            if (IsValidDocumentNumber() === false) {
                result = false;
                $('#kyc-document-number-error1').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#kyc-document-number-error1').removeClass('d-none');
        }

        //Sequence Number
        if (isNaN(sequenceNumber) === false) {

            minimum = parseInt($('#kyc-document-sequence-number').attr('min'));
            maximum = parseInt($('#kyc-document-sequence-number').attr('max'));

            if (sequenceNumber === '' || isDuplicateSequenceNumber === true || parseInt(sequenceNumber) < parseInt(minimum) || parseInt(sequenceNumber) > parseInt(maximum)) {
                result = false;

                if (isDuplicateSequenceNumber) {
                    $('#unique-kyc-document-sequence-number-error').removeClass('d-none');
                    //$('#kyc-document-sequence-number-error').addClass('d-none'); // Hide the general error if duplicate is the problem
                } else {
                    $('#kyc-document-sequence-number-error').removeClass('d-none');
                    //$('#unique-kyc-document-sequence-number-error').addClass('d-none'); // Hide the unique error
                }
            }
        }
        else {
            result = false;
            $('#kyc-document-sequence-number-error').removeClass('d-none');
        }

        //Date Of Issue
        let isValidDateOfIssue = IsValidInputDate('#kyc-document-date-of-issue');

        if (isValidDateOfIssue === false) {
            result = false;
            $('#kyc-document-date-of-issue-error').removeClass('d-none');
        }

        //Date Of Expiry
        let isValidDateOfExpiry = IsValidInputDate('#kyc-document-date-of-expiry');

        if (isValidDateOfExpiry === false) {
            result = false;
            $('#kyc-document-date-of-expiry-error').removeClass('d-none');
        }

        //Issuing Authority

        minimumLength = parseInt($('#kyc-document-issuing-authority').attr('minlength'));
        maximumLength = parseInt($('#kyc-document-issuing-authority').attr('maxlength'));

        if (parseInt(issuingAuthority.length) < parseInt(minimumLength) || parseInt(issuingAuthority.length) > parseInt(maximumLength)) {
            result = false;
            $('#kyc-document-issuing-authority-error').removeClass('d-none');
        }

        //Place Of Issue

        minimumLength = parseInt($('#kyc-document-place-of-issue').attr('minlength'));
        maximumLength = parseInt($('#kyc-document-place-of-issue').attr('maxlength'));

        if (parseInt(placeOfIssue.length) < parseInt(minimumLength) || parseInt(placeOfIssue.length) > parseInt(maximumLength)) {
            result = false;
            $('#kyc-document-place-of-issue-error').removeClass('d-none');
        }

        //Date Of Request
        let isValidDateOfRequest = IsValidInputDate('#date-of-request');

        if (isValidDateOfRequest === false) {
            result = false;
            $('#date-of-request-error').removeClass('d-none');
        }

        //Date Of Expecting Submit
        let isValidDateOfExpectingSubmit = IsValidInputDate('#date-of-expecting-submit');

        if (isValidDateOfExpectingSubmit === false) {
            result = false;
            $('#date-of-expecting-submit-error').removeClass('d-none');
        }
        //Date Of Submit
        let isValidDateOfSubmit = IsValidInputDate('#date-of-submit');

        if (isValidDateOfSubmit === false) {
            result = false;
            $('#date-of-submit-error').removeClass('d-none');
        }

        //Document Upload Status
        if ($('.document-upload-status:checked').length === 0) {
            result = false;
            $('#document-upload-status-error').removeClass('d-none');
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.KYCDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#kyc-file-uploader-error').removeClass('d-none');
                }
            }
            else {
                let photoSrc = $('#kyc-file-uploader-image-preview').attr('src');

                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
        }

        //filecaption
        maximumLength = parseInt($('#kyc-file-caption').attr('maxlength'));

        if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#kyc-file-caption-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsKycDocumentDataTable() {
        personKycDataTable.column(1).visible(false);
        personKycDataTable.column(3).visible(false);
        personKycDataTable.column(15).visible(false);
        personKycDataTable.column(21).visible(false);
        personKycDataTable.column(22).visible(false);
        personKycDataTable.column(23).visible(false);
        personKycDataTable.column(24).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person GST Registration - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-gst-registration-dt').click(function (event) {
        const today = new Date();
        const applicableFromDate = new Date($('#applicable-from').val());

        let maxOldAllowableAssesmentYear = today.getFullYear() - 5;

        if (parseInt(maxOldAllowableAssesmentYear) < applicableFromDate.getFullYear()) {
            maxOldAllowableAssesmentYear = applicableFromDate.getFullYear();
        }

        isDbRecord = false;

        event.preventDefault();

        $('#assessment-year').attr('min', maxOldAllowableAssesmentYear);

        SetModalTitle('gst-registration', 'Add');

        if (($('#gst-document-upload').val()) === MANDATORY) {
            $('#gst-file-uploader').addClass('mandatory-mark');
            $('#file-caption-gst').addClass('mandatory-mark');
        }
        else {
            $('#gst-file-uploader').removeClass('mandatory-mark');
            $('#file-caption-gst').removeClass('mandatory-mark');
        }
    });

    // DataTable Edit Button 
    $('#btn-edit-gst-registration-dt').click(function () {
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('gst-registration', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#gst-registration-modal').modal();

            columnValues = $('#btn-edit-gst-registration-dt').data('rowindex');

            $('#assessment-year', myModal).val(columnValues[1]);
            $('#tax-amount', myModal).val(columnValues[2]);

            $('#gst-file-caption', myModal).val(columnValues[5]);
            $('#note-gst-document', myModal).val(columnValues[6]);
            $('#reason-for-modification-gst-registration', myModal).val(columnValues[7]);

            fileUploader = $('#' + $(columnValues[3]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'gst-file-uploader';

            // columnValues[3] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[3]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[3]).attr('class') === 'db-record' ? true : false;

            // columnValues[4] - Image Tag Html
            filePath = $('#' + $(columnValues[4]).attr('id')).attr('src');

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';
                personGSTReturnDocumentPrmKey = 0;

                AttachFileUploader();
            }

            $('#gst-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#gst-registration-modal').modal('show');
        }
        else {
            $('#btn-edit-gst-registration-edit-dt').addClass('read-only');
            $('#gst-registration-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-gst-registration-modal').click(function (event) {
        if (IsValidGSTModal()) {
            row = gstDataTable.row.add([
                        tag,
                        assessmentYear,
                        taxAmount,
                        fileUploaderInputHtml,
                        imageTagHtml,
                        fileCaption,
                        note,
                        reasonForModification,
                        fileNameDocument,
                        personGSTReturnDocumentPrmKey,
                        localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);
            $('#gst-registration-accordion-error').addClass('d-none');

            HideColumnsGSTDataTable();

            gstDataTable.columns.adjust().draw();

            $('#gst-registration-modal').modal('hide');

            EnableNewOperation('gst-registration');
        }
    });

    // Modal update Button Event
    $('#btn-update-gst-registration-modal').click(function (event) {
        $('#select-all-gst-registration').prop('checked', false);
        if (IsValidGSTModal()) {
            gstDataTable.row(selectedRowIndex).data([
                        tag,
                        assessmentYear,
                        taxAmount,
                        fileUploaderInputHtml,
                        imageTagHtml,
                        fileCaption,
                        note,
                        reasonForModification,
                        fileNameDocument,
                        personGSTReturnDocumentPrmKey,
                        localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideColumnsGSTDataTable();

            gstDataTable.columns.adjust().draw();

            $('#gst-registration-modal').modal('hide');

            EnableNewOperation('gst-registration');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-gst-registration-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-gst-registration tbody input[type="checkbox"]:checked').each(function () {
                 gstDataTable.row($('#tbl-gst-registration tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-gst-registration-dt').data('rowindex');
                  EnableNewOperation('gst-registration');

                  $('#select-all-gst-registration').prop('checked', false);
                    if (!gstDataTable.data().any())
                    $('#gst-registration-accordion-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-gst-registration').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-gst-registration tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = gstDataTable.row(row).index();

                rowData = (gstDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-gst-registration-dt').data('rowindex', arr);
                EnableDeleteOperation('gst-registration')
            });
        }
        else {
            EnableNewOperation('gst-registration')

            $('#tbl-gst-registration tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-gst-registration tbody').click('input[type=checkbox]', function () {
        $('#tbl-gst-registration input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = gstDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (gstDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('gst-registration');

                    $('#btn-update-gst-registration-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-gst-registration-dt').data('rowindex', rowData);
                    $('#btn-delete-gst-registration-dt').data('rowindex', arr);
                    $('#select-all-gst-registration').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-gst-registration tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('gst-registration');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('gst-registration');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('gst-registration');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-gst-registration').prop('checked', true);
        else
            $('#select-all-gst-registration').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidGSTModal() {
        debugger;
        result = true;

        counter++;
        fileUploaderId = 'data-table-gst-file-uploader' + counter;
        fileId = "photo-Id" + counter;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        assessmentYear = parseInt($('#assessment-year').val());
        taxAmount = parseFloat($('#tax-amount').val());
        fileCaption = $('#gst-file-caption').val();
        note = $('#note-gst-document').val();
        reasonForModification = $('#reason-for-modification-gst-registration').val();

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#gst-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        fileUploader = $('#gst-file-uploader').get(0);

        //Set Default Value if Empty
        if (note === '') {
            note = 'None';
        }

        if (fileCaption === '') {
            fileCaption = 'None';
        }

        if (reasonForModification === '') {
            reasonForModification = 'None';
        }

        //Assessment Year
        if (isNaN(assessmentYear) === false) {

            minimum = parseInt($('#assessment-year').attr('min'));
            maximum = parseInt($('#assessment-year').attr('max'));

            if (parseInt(assessmentYear) < parseInt(minimum) || parseInt(assessmentYear) > parseInt(maximum)) {
                result = false;
                $('#assessment-year-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#assessment-year-error').removeClass('d-none');
        }

        //Tax Amount
        if (isNaN(taxAmount) === false) {

            minimum = parseFloat($('#tax-amount').attr('min'));
            maximum = parseFloat($('#tax-amount').attr('max'));

            if (parseFloat(taxAmount) < parseFloat(minimum) || parseFloat(taxAmount) > parseFloat(maximum)) {
                result = false;
                $('#tax-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#tax-amount-error').removeClass('d-none');
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.GSTDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#gst-file-uploader-error').removeClass('d-none');
                }

            }
            else {
                // Don't Change, It Is Refereed For AttachFileUploader()
                fileCaption = 'NotApplicable';
            }
        }

        //filecaption
        maximumLength = parseInt($('#gst-file-caption').attr('maxlength'));

        if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#gst-file-caption-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsGSTDataTable() {
        gstDataTable.column(8).visible(false);
        gstDataTable.column(9).visible(false);
        gstDataTable.column(10).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@   Validation(Event Handling) Code - Family Detail  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Joint Account Person Dropdown List FocusOut Event
    $('#person-information-numbers').focusout(function (event) {
        $(this).val($(this).val().trim());
    });

    // While Adding Nominee Hide Selected Customer Name In Nominee Dropdown List.
    $('#person-information-numbers').autocomplete(
    {
        minLength: 0,
        appendTo: '#person-information',
        scroll: true,
        autoFocus: true,
        source: function (request, response) {
            debugger;
            let responseDropdownListArray = [];
            dropDownListItemCount = finalDropdownListArray.length;

            if (parseInt(dropDownListItemCount) > 0) {
                responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                    if (key.Text.toUpperCase().indexOf(request.term.toUpperCase()) != -1)
                        return { label: key.Text, valueId: key.Value }
                    else
                        return null;
                });

                // The slice() method returns selected elements in an array, as a new array. 
                // The slice() method selects from a given start, up to a (not inclusive) given end.
                response(responseDropdownListArray.slice(0, 10));
            }
            else
                response([{ label: 'No Records Found', value: -1 }]);
        },
        select: function (event, ui) {
            event.preventDefault();
            $('#person-information-numbers').val(ui.item.label);
            personInformationNumber = ui.item.valueId;
            personInformationNumberText = ui.item.label;
            if (personInformationNumber !== '') {
                $('#family-member-name').addClass('d-none');
                $('#person-information').removeClass('d-none');
            }

        },
    }).focus(function (event, ui) {
        debugger;
        personInformationNumber = '';
        personInformationNumberText = '';
        let dataTablePersonIdArray = [];

        // Assign Array Without Reference  *** Use Slice Method
        finalDropdownListArray = personDropdownListDataForFamily.slice();

        dropDownListItemCount = finalDropdownListArray.length;

        // Get Added Person Id For Remove From List
        $('#tbl-family-detail > tbody > tr').each(function () {
            let currentRow = $(this).closest("tr");
            let columnValues = (personFamilyDataTable.row(currentRow).data());

            if (typeof columnValues !== 'undefined' && columnValues != null)
                dataTablePersonIdArray.push({ 'Value': columnValues[1], 'Text': columnValues[2] })
        });

        if (parseInt(dropDownListItemCount) > 0 && parseInt(dataTablePersonIdArray.length) > 0) {
            while (dropDownListItemCount--) {
                // Remove Added Joint Account Person Id From List
                for (let personId of dataTablePersonIdArray)
                {
                    if (finalDropdownListArray[dropDownListItemCount].Value === personId.Value)
                        // splice - remove item from array at a choosen index
                        finalDropdownListArray.splice(dropDownListItemCount, 1);
                }
            }
        }


        $(this).autocomplete('search');
    });

    // Validation Family Member
    $('#full-name-of-family-member').focusout(function () {
        debugger;
        let fullNameOfFamily = $('#full-name-of-family-member').val();

        if ((fullNameOfFamily !== 'None') && (fullNameOfFamily.length > 3)) {
            $('#family-person-information-number').prop('selectedIndex', 0);
            $('#person-information').addClass('d-none');
            $('#person-information').val('None');
        } else {
            $('#person-information').removeClass('d-none');
        }
    });

    $('#person-information-numbers').focusout(function () {
        debugger;
        let familyPersonInformationNumber = $('#person-information-numbers').val();

        // Check if the value is not from the autocomplete list
        let isInAutocomplete = false;
        $('#person-information-numbers').autocomplete("widget").children("li").each(function () {
            if ($(this).text() === familyPersonInformationNumber) {
                isInAutocomplete = true;
                return false;
            }
        });

        if (!isInAutocomplete) {
            // Clear the input if the typed value is not in the autocomplete list
            $('#person-information-numbers').val('');
            $('#family-pin').val('None');
            $('#family-member-name').removeClass('d-none');
        } else if ((familyPersonInformationNumber !== 'None') && (familyPersonInformationNumber.length > 3)) {
            $('#family-member-name').addClass('d-none');
            $('#family-member-name').val('None')
        }
    });

    let familyPersonInfo = $('#family-pin').val();
    if (familyPersonInfo !== '') {
        $('#person-information-numbers').val(familyPersonInfo);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Family Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-family-detail-dt').click(function () {

        //Modify By --- Sagar Kare 
        $('#person-information').removeClass('d-none');
        $('#family-member-name').removeClass('d-none');
        $('#person-information-numbers-error').addClass('d-none');

        // Display Alert Message Only When Modal Present On Page (i.e. Create And Amend) Hide On Verify
        if ($('#family-detail-modal').length) {
            personInformationNumber = ''
            personInformationNumberText = '';
        }
        event.preventDefault();
        SetModalTitle('family-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-family-detail-dt').click(function () {
        debugger
        SetModalTitle('family-detail', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-family-detail-dt').data('rowindex');
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#family-detail-modal').modal();

            familyDetailsBirthDate = new Date(columnValues[7]);
            editedJointAccountPersonId = columnValues[1];

            personInformationNumber = columnValues[1];
            personInformationNumberText = columnValues[2];
            $('#person-information-numbers', myModal).val(columnValues[2]);
            $('#full-name-of-family-member', myModal).val(columnValues[3]);
            $('#trans-full-name-of-family-member', myModal).val(columnValues[4]);
            $('#relations-id', myModal).val(columnValues[5]);
            $('#birth-date-family-member', myModal).val(GetInputDateFormat(familyDetailsBirthDate));
            $('#family-occupation-id', myModal).val(columnValues[8]);
            $('#income', myModal).val(columnValues[10]);
            $('#note-family-detail', myModal).val(columnValues[11]);
            $('#trans-note-family-detail', myModal).val(columnValues[12]);
            $('#reason-for-modification-family-detail', myModal).val(columnValues[13]);
            debugger
            // If Nominee Is Existing Customer i.e. Has Valid Person Information Number
            if ((columnValues[3] == 'None')) {
                $('#family-member-name').addClass('d-none');
                $('#person-information').removeClass('d-none');
            }
            else   // User Enter Manullay Nominee Details 
            {
                $('#family-member-name').removeClass('d-none');
                $('#person-information').addClass('d-none');
            }

            // Show Modals
            $('#family-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-family-detail-edit-dt').addClass('read-only');
            $('#family-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-family-detail-modal').click(function (event) {

        if (IsValidFamilyDetailsModal()) {
            row = personFamilyDataTable.row.add([
                          tag,
                          personInformationNumber,
                          personInformationNumberText,
                          fullNameOfFamilyMember,
                          transFullNameOfFamilyMember,
                          relation,
                          relationText,
                          birthDate,
                          occupation,
                          occupationText,
                          income,
                          note,
                          transNote,
                          reasonForModification
            ]).draw();

            // Error Message In Span
            $('#family-detail-data-table-error').addClass('d-none');

            HideColumnsFamilyDetailsDataTable();

            personFamilyDataTable.columns.adjust().draw();

            $('#family-detail-modal').modal('hide');

            EnableNewOperation('family-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-family-detail-modal').click(function (event) {
        $('#select-all-family-detail').prop('checked', false);
        if (IsValidFamilyDetailsModal()) {
            personFamilyDataTable.row(selectedRowIndex).data([
                                tag,
                                personInformationNumber,
                                personInformationNumberText,
                                fullNameOfFamilyMember,
                                transFullNameOfFamilyMember,
                                relation,
                                relationText,
                                birthDate,
                                occupation,
                                occupationText,
                                income,
                                note,
                                transNote,
                                reasonForModification
            ]).draw();
            // Error Message In Span
            $('#family-detail-validation span').html('');

            HideColumnsFamilyDetailsDataTable();

            personFamilyDataTable.columns.adjust().draw();

            $('#family-detail-modal').modal('hide');

            EnableNewOperation('family-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-family-detail-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-family-detail tbody input[type="checkbox"]:checked').each(function () {
                 personFamilyDataTable.row($('#tbl-family-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-family-detail-dt').data('rowindex');
                  EnableNewOperation('family-detail');

                  $('#select-all-family-detail').prop('checked', false);
                    // Add Required Error Message, If Table Has Not Any Record
                    //if (!personFamilyDataTable.data().any())
                    //$('#family-detail-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-family-detail').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-family-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = personFamilyDataTable.row(row).index();

                rowData = (personFamilyDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-family-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('family-detail');
            });
        }
        else {
            EnableNewOperation('family-detail');

            $('#tbl-family-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-family-detail tbody').click('input[type="checkbox"]', function () {
        $('#tbl-family-detail input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = personFamilyDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (personFamilyDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('family-detail');

                    $('#btn-update-family-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-family-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-family-detail-dt').data('rowindex', arr);
                    $('#select-all-family-detail').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-family-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('family-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('family-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('family-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-family-detail').prop('checked', true);
        else
            $('#select-all-family-detail').prop('checked', false);
    });

    // Validate Family Detail Module
    function IsValidFamilyDetailsModal() {
        debugger;
        result = true;
        let isSelectedPersonInformationNumber = false;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        fullNameOfFamilyMember = $('#full-name-of-family-member').val();
        transFullNameOfFamilyMember = $('#trans-full-name-of-family-member').val();
        relation = $('#relations-id option:selected').val();
        relationText = $('#relations-id option:selected').text();
        birthDate = $('#birth-date-family-member').val();
        occupation = $('#family-occupation-id option:selected').val();
        occupationText = $('#family-occupation-id option:selected').text();
        income = parseFloat($('#income').val());
        note = $('#note-family-detail').val();
        transNote = $('#trans-note-family-detail').val();
        reasonForModification = $('#reason-for-modification-family-detail').val();

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (transNote === '')
            transNote = 'None';


        if (reasonForModification === '')
            reasonForModification = 'None';


        //Modify By --- Sagar Kare 
        //Validatio For  Person Information Number
        if (personInformationNumber === '' || personInformationNumber === 'None') {
            fullNameOfFamilyMember = $('#full-name-of-family-member').val();
            transFullNameOfFamilyMember = $('#trans-full-name-of-family-member').val();
        }
        else {
            fullNameOfFamilyMember = 'None';
            transFullNameOfFamilyMember = 'None';
        }
        if (personInformationNumber === '' || typeof personInformationNumber === 'undefined') {
            personInformationNumber = 'None';
        }

        // Check Whether Person Information Number Selected For Nominee Or Not?
        if (personInformationNumber === 'None' || typeof personInformationNumber === 'undefined') {
            isSelectedPersonInformationNumber = false;
        } else {
            isSelectedPersonInformationNumber = true;
        }

        if ($('#person-information').hasClass('d-none') === false) {
            if (isSelectedPersonInformationNumber === false) {
                result = false;
                $('#person-information-numbers-error').removeClass('d-none');
            } else {
                $('#person-information-numbers-error').addClass('d-none');
            }
        }
        else {
            personInformationNumberText = 'None';
        }

        if (isSelectedPersonInformationNumber === false) {

            //reference Number
            maximumLength = parseInt($('#full-name-of-family-member').attr('maxlength'));

            if (parseInt(fullNameOfFamilyMember.length) === 0 || parseInt(fullNameOfFamilyMember.length) > parseInt(maximumLength)) {
                result = false;
                $('#full-name-of-family-member-error').removeClass('d-none');
            }

            //reference Number
            maximumLength = parseInt($('#trans-full-name-of-family-member').attr('maxlength'));

            if (parseInt(transFullNameOfFamilyMember.length) === 0 || parseInt(transFullNameOfFamilyMember.length) > parseInt(maximumLength)) {
                result = false;
                $('#trans-full-name-of-family-member-error').removeClass('d-none');
            }
        }

        // DocumentTypeId
        if ($('#relations-id').prop('selectedIndex') < 1) {
            result = false;
            $('#relations-id-error').removeClass('d-none');
        }

        //date of Birth
        if (IsValidInputDate('#birth-date-family-member') === false) {
            result = false;
            $('#birth-date-family-member-error').removeClass('d-none');
        }

        // DocumentTypeId
        if ($('#family-occupation-id').prop('selectedIndex') < 1) {
            result = false;
            $('#family-occupation-id-error').removeClass('d-none');
        }

        // Sanction Loan Amount
        if (isNaN(income) === false) {
            minimum = parseFloat($('#income').attr('min'));
            maximum = parseFloat($('#income').attr('max'));

            if (parseFloat(income) < parseFloat(minimum) || parseFloat(income) > parseFloat(maximum)) {
                result = false;
                $('#income-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#income-error').removeClass('d-none');
        }

        return result;

    }

    function HideColumnsFamilyDetailsDataTable() {
        personFamilyDataTable.column(1).visible(false);
        personFamilyDataTable.column(5).visible(false);
        personFamilyDataTable.column(8).visible(false);
        personFamilyDataTable.column(13).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@   Validation(Event Handling) Code - Group Authorized Signatory  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Validation Authorized Signatory On Change Event
    $('#enable-authorized-signatory').change(function () {
        debugger
        EnableAuthorizedSignatory();

    });

    // Function Toggle Enable Authorized Signatory
    function EnableAuthorizedSignatory() {
        let isChecked = $('#enable-authorized-signatory').is(':checked');

        // Toggle the authorized signatory block visibility based on checkbox status
        if (isChecked) {
            $('#authorized-signatory-block').removeClass('d-none');
        } else {
            $('#authorized-signatory-block').addClass('d-none');
            $('#group-sign-file-uploader').val('');
            $('.modal-input-img-preview').attr('src', '');
            $('#group-sign-file-uploader-error').addClass('d-none');
            $('#group-sign-file-caption-error').addClass('d-none');
        }
    }

    //Validation For Establishment Date Focusout Event
    $('#date-of-establishment').focusout(function () {
        debugger;
        ValidateEstablishmentDate();
    });

    // Function On Date Of Establishment
    function ValidateEstablishmentDate() {
        if (isVerifyView === false) {
            debugger;
            let establishmentDate = new Date($('#date-of-establishment').val());
            let currentDate = new Date();
            let fiftyYearsAgo = new Date(currentDate.getFullYear() - 50, currentDate.getMonth(), currentDate.getDate());

            if (establishmentDate < fiftyYearsAgo) {
                $('#date-input').removeClass('d-none');
            } else {
                $('#date-input').addClass('d-none');
            }
        }
    }

    // While Adding Nominee Hide Selected Customer Name In Nominee Dropdown List.
    $('#authorized-person-information-number').autocomplete(
    {
        minLength: 0,
        appendTo: '#authorized-person-information-number-input',
        scroll: true,
        autoFocus: true,
        source: function (request, response) {
            debugger;
            let responseDropdownListArray = [];
            dropDownListItemCount = finalDropdownListArray.length;

            if (parseInt(dropDownListItemCount) > 0) {
                responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                    if (key.Text.toUpperCase().indexOf(request.term.toUpperCase()) != -1)
                        return { label: key.Text, valueId: key.Value }
                    else
                        return null;
                });

                // The slice() method returns selected elements in an array, as a new array. 
                // The slice() method selects from a given start, up to a (not inclusive) given end.
                response(responseDropdownListArray.slice(0, 10));
            }
            else
                response([{ label: 'No Records Found', value: -1 }]);
        },
        select: function (event, ui) {
            event.preventDefault();
            $('#authorized-person-information-number').val(ui.item.label);
            personInformationNumber = ui.item.valueId;
            personInformationNumberText = ui.item.label;
            if (personInformationNumber !== '') {
                $('#authorized-member-name').addClass('d-none');
                $('#authorized-person-information-number-input').removeClass('d-none');
            }

        },
    }).focus(function (event, ui) {
        debugger;
        $('#authorized-person-information-number').val('');
        personInformationNumber = '';
        personInformationNumberText = '';

        let dataTablePersonIdArray = [];

        // Assign Array Without Reference  *** Use Slice Method
        finalDropdownListArray = personDropdownListDataForFamily.slice();

        dropDownListItemCount = finalDropdownListArray.length;

        // Get Added Person Id For Remove From List
        $('#tbl-authorized-signatory > tbody > tr').each(function () {
            let currentRow = $(this).closest("tr");
            let columnValues = (authorizedSignatoryDataTable.row(currentRow).data());

            if (typeof columnValues !== 'undefined' && columnValues != null)
                dataTablePersonIdArray.push({ 'Value': columnValues[1], 'Text': columnValues[2] })
        });

        if (parseInt(dropDownListItemCount) > 0 && parseInt(dataTablePersonIdArray.length) > 0) {
            while (dropDownListItemCount--) {
                // Remove Added Joint Account Person Id From List
                for (let personId of dataTablePersonIdArray)
                {
                    if (finalDropdownListArray[dropDownListItemCount].Value === personId.Value)
                        // splice - remove item from array at a choosen index
                        finalDropdownListArray.splice(dropDownListItemCount, 1);
                }
            }
        }


        $(this).autocomplete('search');
    });

    // Validation Authorized Member
    $('#full-name-of-authorized-member').focusout(function () {
        debugger;
        let fullNameOfFamily = $('#full-name-of-authorized-member').val();

        if ((fullNameOfFamily !== 'None') && (fullNameOfFamily.length > 3)) {
            $('#authorized-person-information-number').prop('selectedIndex', 0);
            $('#authorized-person-information-number-input').addClass('d-none');
            $('#authorized-person-information-number-input').val('None');
        } else {
            $('#authorized-person-information-number-input').removeClass('d-none');
        }
    });

    // Validation Authorized Person information number On Focusout
    // ******* Remove Code Heavy Code Writtened Optimize Following Code
    $('#authorized-person-information-number').focusout(function ()
    {
        debugger;
        $(this).val($(this).val().trim());

        let authorizedInformationNumber = $('#authorized-person-information-number').val();

        // Check if the value is not from the autocomplete list
        let isInAutocomplete = false;
        $('#authorized-person-information-number').autocomplete("widget").children("li").each(function () {
            if ($(this).text() === authorizedInformationNumber) {
                isInAutocomplete = true;
                return false;
            }
        });

        if (!isInAutocomplete) {
            // Clear the input if the typed value is not in the autocomplete list
            $('#person-information-numbers').val('');
            $('#authorized-person-information-number-input').val('None');
            $('#authorized-member-name').removeClass('d-none');
        } else if ((authorizedInformationNumber !== 'None') && (authorizedInformationNumber.length > 3)) {
            $('#authorized-member-name').addClass('d-none');
            $('#authorized-member-name').val('None')
        }
    });

    let authorizedPersonInfo = $('#authorized-person-information-number-input').val();
    if (authorizedPersonInfo !== '') {
        $('#authorized-person-information-number').val(authorizedPersonInfo);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Person Group Authorized Signatory - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-authorized-signatory-dt').click(function () {
        debugger;
        $('#authorized-signatory-block').addClass('d-none');

        $('#authorized-person-information-number-input').removeClass('d-none');
        $('#authorized-member-name').removeClass('d-none');
        $('#authorized-person-information-number-error').addClass('d-none');

        // Display Alert Message Only When Modal Present On Page (i.e. Create And Amend) Hide On Verify
        if ($('#authorized-signatory-modal').length) {
            personInformationNumber = ''
            personInformationNumberText = '';
        }
        event.preventDefault();

        isDbRecord = false;
        isChangedSign = false;
        SetModalTitle('authorized-signatory', 'Add');

        personGroupAuthorizedSignatoryPrmKey = 0;
        localStoragePath = 'None';
        fileNameDocument = 'None';
    });

    // DataTable Edit Button 
    $('#btn-edit-authorized-signatory-dt').click(function () {
        debugger;
        SetModalTitle('authorized-signatory', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-authorized-signatory-dt').data('rowindex');
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#authorized-signatory-modal').modal();

            personInformationNumber = columnValues[1];
            personInformationNumberText = columnValues[2];
            $('#authorized-person-information-number', myModal).val(columnValues[2]);

            $('#full-name-of-authorized-member', myModal).val(columnValues[3]);
            $('#trans-full-name-of-authorized-member', myModal).val(columnValues[4]);
            $('#authorized-person-address-detail', myModal).val(columnValues[5]);
            $('#trans-authorized-person-address-detail', myModal).val(columnValues[6]);
            $('#authorized-person-contact-detail', myModal).val(columnValues[7]);
            $('#trans-authorized-person-contact-detail', myModal).val(columnValues[8]);
            $('#board-of-director-designation-id', myModal).val(columnValues[9]);
            $('#enable-authorized-signatory').prop('checked', columnValues[11].toString().toLowerCase() === 'true' ? true : false);
            $('#group-sign-file-caption', myModal).val(columnValues[14]);
            $('#note-board-of-director-authorized', myModal).val(columnValues[15]);
            $('#trans-note-board-of-director-authorized', myModal).val(columnValues[16]);
            $('#reason-for-modification', myModal).val(columnValues[17]);

            // If Nominee Is Existing Customer i.e. Has Valid Person Information Number
            if ((columnValues[3] == 'None')) {
                $('#authorized-member-name').addClass('d-none');
                $('#authorized-person-information-number-input').removeClass('d-none');
            }
            else   // User Enter Manullay Nominee Details 
            {
                $('#authorized-member-name').removeClass('d-none');
                $('#authorized-person-information-number-input').addClass('d-none');
            }
            debugger
            // Check If the Toggle Switch Authorized Signatory 
            if ($('#enable-authorized-signatory').is(':checked') === true) {
                $('#authorized-signatory-block').removeClass('d-none');
            }
            else {
                $('#authorized-signatory-block').addClass('d-none');
                $('#group-sign-file-uploader').val('');
                $('.modal-input-img-preview').attr('src', '');
                $('#group-sign-file-uploader-error').addClass('d-none');
                $('#group-sign-file-caption-error').addClass('d-none');
                fileNameDocument = 'None';
                localStoragePath = 'None';
            }

            fileUploader = $('#' + $(columnValues[12]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'group-sign-file-uploader';

            // columnValues[21] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[12]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[12]).attr('class') === 'db-record' ? true : false;

            // columnValues[22] - Image Tag Html
            filePath = $('#' + $(columnValues[13]).attr('id')).attr('src');

            fileNameDocument = columnValues[18];
            personGroupAuthorizedSignatoryPrmKey = columnValues[19];
            localStoragePath = columnValues[20];


            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if ($('#authorized-signatory-block').hasClass('d-none') === false) {
                if (fileUploader.files.length > 0) {
                    fileNameDocument = 'None';
                    localStoragePath = 'None';
                    AttachFileUploader();
                }
            }

            $('#group-sign-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#authorized-signatory-modal').modal('show');
        }
        else {
            $('#btn-edit-authorized-signatory-edit-dt').addClass('read-only');
            $('#authorized-signatory-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-authorized-signatory-modal').click(function (event) {
        debugger
        if (IsValidAuthorizedSignatoryModal()) {
            debugger
            row = authorizedSignatoryDataTable.row.add([
                        tag,
                        personInformationNumber,
                        personInformationNumberText,
                        fullNameOfAuthorizedPerson,
                        transfullNameOfAuthorizedPerson,
                        authorizedPersonAddressDetail,
                        transAuthorizedPersonAddressDetail,
                        authorizedPersonContactDetail,
                        transAuthorizedPersonContactDetail,
                        designationId,
                        designationIdText,
                        isAuthorizedSignatory,
                        fileUploaderInputHtml,
                        imageTagHtml,
                        fileCaption,
                        note,
                        transNote,
                        reasonForModification,
                        fileNameDocument,
                        personGroupAuthorizedSignatoryPrmKey,
                        localStoragePath
            ]).draw();
            debugger
            if ($('#authorized-signatory-block').hasClass('d-none') === false) {
                // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
                if ((isDbRecord === false || isChangedSign === true) && (fileCaption !== 'NotApplicable')) {
                    AttachFileUploader();
                }
            }

            $('#authorized-accordion-error').addClass('d-none');

            HideColumnsAuthorizedSignatoryDataTable();

            authorizedSignatoryDataTable.columns.adjust().draw();

            $('#authorized-signatory-modal').modal('hide');

            EnableNewOperation('authorized-signatory');
        }
    });

    // Modal update Button Event
    $('#btn-update-authorized-signatory-modal').click(function (event) {
        $('#select-all-authorized-signatory').prop('checked', false);
        if (IsValidAuthorizedSignatoryModal()) {
            authorizedSignatoryDataTable.row(selectedRowIndex).data([
                        tag,
                        personInformationNumber,
                        personInformationNumberText,
                        fullNameOfAuthorizedPerson,
                        transfullNameOfAuthorizedPerson,
                        authorizedPersonAddressDetail,
                        transAuthorizedPersonAddressDetail,
                        authorizedPersonContactDetail,
                        transAuthorizedPersonContactDetail,
                        designationId,
                        designationIdText,
                        isAuthorizedSignatory,
                        fileUploaderInputHtml,
                        imageTagHtml,
                        fileCaption,
                        note,
                        transNote,
                        reasonForModification,
                        fileNameDocument,
                        personGroupAuthorizedSignatoryPrmKey,
                        localStoragePath
            ]).draw();


            if ($('#authorized-signatory-block').hasClass('d-none') === false) {
                // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
                if ((isDbRecord === false || isChangedSign === true) && (fileCaption !== 'NotApplicable')) {
                    AttachFileUploader();
                }
            }

            HideColumnsAuthorizedSignatoryDataTable();

            authorizedSignatoryDataTable.columns.adjust().draw();

            $('#authorized-signatory-modal').modal('hide');

            EnableNewOperation('authorized-signatory');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-authorized-signatory-dt').click(function (event) {

        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            $('#heading-person-photo-sign').addClass('d-none');

            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-authorized-signatory tbody input[type="checkbox"]:checked').each(function () {

                    authorizedSignatoryDataTable.row($('#tbl-authorized-signatory tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-authorized-signatory-dt').data('rowindex');

                  EnableNewOperation('authorized-signatory');

                  $('#select-all-authorized-signatory').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!authorizedSignatoryDataTable.data().any())
                    $('#authorized-accordion-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-authorized-signatory').click(function () {

        if ($(this).prop('checked')) {

            $('#tbl-authorized-signatory tbody input[type="checkbox"]').each(function () {

                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = authorizedSignatoryDataTable.row(row).index();

                rowData = authorizedSignatoryDataTable.row(selectedRowIndex).data();
                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-authorized-signatory-dt').data('rowindex', arr);
                EnableDeleteOperation('authorized-signatory');
            });
        }
        else {
            EnableNewOperation('authorized-signatory');

            $('#tbl-authorized-signatory tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-authorized-signatory tbody').click("input[type=checkbox]", function () {

        $('#tbl-authorized-signatory input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = authorizedSignatoryDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (authorizedSignatoryDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });


                    EnableEditDeleteOperation('authorized-signatory');

                    $('#btn-update-authorized-signatory-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-authorized-signatory-dt').data('rowindex', rowData);
                    $('#btn-delete-authorized-signatory-dt').data('rowindex', arr);
                    $('#select-all-authorized-signatory').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-authorized-signatory tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('authorized-signatory');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('authorized-signatory');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('authorized-signatory');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-authorized-signatory').prop('checked', true);
        else
            $('#select-all-authorized-signatory').prop('checked', false);
    });

    // Validate authorized-signatory Module
    function IsValidAuthorizedSignatoryModal() {
        debugger;
        let isSelectedPersonInformationNumberAuthorized = false;

        result = true;

        counter++;
        fileUploaderId = 'data-table-group-sign-file-uploader' + counter;
        fileId = "photo-Id" + counter;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        fullNameOfAuthorizedPerson = $('#full-name-of-authorized-member').val();
        transfullNameOfAuthorizedPerson = $('#trans-full-name-of-authorized-member').val();
        authorizedPersonAddressDetail = $('#authorized-person-address-detail').val();
        transAuthorizedPersonAddressDetail = $('#trans-authorized-person-address-detail').val();
        authorizedPersonContactDetail = $('#authorized-person-contact-detail').val();
        transAuthorizedPersonContactDetail = $('#trans-authorized-person-contact-detail').val();
        designationId = $('#board-of-director-designation-id option:selected').val();
        designationIdText = $('#board-of-director-designation-id option:selected').text();
        isAuthorizedSignatory = $('#enable-authorized-signatory').is(':checked') ? true : false;
        fileCaption = $('#group-sign-file-caption').val();
        reasonForModification = $('#reason-for-modification').val();
        note = $('#note-board-of-director-authorized').val();
        transNote = $('#trans-note-board-of-director-authorized').val();

        if (isDbRecord === false || isChangedSign === true) {
            filePath = $('#group-sign-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        fileUploader = $('#group-sign-file-uploader').get(0);

        // Toggle the authorized signatory block visibility based on checkbox status
        if (isAuthorizedSignatory) {
            $('#authorized-signatory-block').removeClass('d-none');
        } else {
            $('#authorized-signatory-block').addClass('d-none');
            $('#group-sign-file-uploader').val('');
            $('.modal-input-img-preview').attr('src', '');
            $('#group-sign-file-uploader-error').addClass('d-none');
            $('#group-sign-file-caption-error').addClass('d-none');
            fileNameDocument = 'None';
            localStoragePath = 'None';
            fileCaption = 'NotApplicable';
        }

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if ((fileCaption === '' || fileCaption === 'NotApplicable') && (isChangedSign === false)) {
            fileCaption = 'NotApplicable';
        }
        else {
            fileCaption = 'None';
        }

        if (transNote === '') {
            transNote = 'None';
        }

        if (reasonForModification === '') {
            reasonForModification = 'None';
        }

        if ($('#board-of-director-designation-id').prop('selectedIndex') < 1) {
            result = false;
            $('#board-of-director-designation-id-error').removeClass('d-none');
        }

        //Modify By --- Sagar Kare 
        //Modify By --- Dhanshri Wagh -authorizedPersonAddressDetail & authorizedPersonContactDetail On 16/09/2024
        //Validation For Authorized Person In formationNumber
        if (personInformationNumber === '' || personInformationNumber === 'None') {
            fullNameOfAuthorizedPerson = $('#full-name-of-authorized-member').val();
            transfullNameOfAuthorizedPerson = $('#trans-full-name-of-authorized-member').val();
            authorizedPersonAddressDetail = $('#authorized-person-address-detail').val();
            transAuthorizedPersonAddressDetail = $('#trans-authorized-person-address-detail').val();
            authorizedPersonContactDetail = $('#authorized-person-contact-detail').val();
            transAuthorizedPersonContactDetail = $('#trans-authorized-person-contact-detail').val();
        }
        else {
            fullNameOfAuthorizedPerson = 'None';
            transfullNameOfAuthorizedPerson = 'None';
            authorizedPersonAddressDetail = 'None';
            transAuthorizedPersonAddressDetail = 'None';
            authorizedPersonContactDetail = 'None';
            transAuthorizedPersonContactDetail = 'None';
        }
        if (personInformationNumber === '' || typeof personInformationNumber === 'undefined')
            personInformationNumber = 'None';

        // Check Whether Person Information Number Selected For Nominee Or Not?
        if (personInformationNumber === 'None' || typeof personInformationNumber === 'undefined') {
            isSelectedPersonInformationNumberAuthorized = false;
        } else {
            isSelectedPersonInformationNumberAuthorized = true;
        }

        if ($('#authorized-person-information-number-input').hasClass('d-none') === false) {
            if (isSelectedPersonInformationNumberAuthorized === false) {
                result = false;
                $('#authorized-person-information-number-error').removeClass('d-none');
            } else {
                $('#authorized-person-information-number-error').addClass('d-none');
            }
        }
        else {
            personInformationNumberText = 'None';
        }

        if (isSelectedPersonInformationNumberAuthorized === false) {

            maximumLength = parseInt($('#full-name-of-authorized-member').attr('maxlength'));

            if (parseInt(fullNameOfAuthorizedPerson.length) === 0 || parseInt(fullNameOfAuthorizedPerson.length) > parseInt(maximumLength)) {
                result = false;
                $('#full-name-of-authorized-member-error').removeClass('d-none');
            }


            maximumLength = parseInt($('#trans-full-name-of-authorized-member').attr('maxlength'));

            if (parseInt(transfullNameOfAuthorizedPerson.length) === 0 || parseInt(transfullNameOfAuthorizedPerson.length) > parseInt(maximumLength)) {
                result = false;
                $('#trans-full-name-of-authorized-member-error').removeClass('d-none');
            }

            //Authorized Person Address Detail
            maximumLength = parseInt($('#authorized-person-address-detail').attr('maxlength'));

            if (parseInt(authorizedPersonAddressDetail.length) === 0 || parseInt(authorizedPersonAddressDetail.length) > parseInt(maximumLength)) {
                result = false;
                $('#authorized-person-address-detail-error').removeClass('d-none');
            }

            //Trans Authorized Person Address Detail
            maximumLength = parseInt($('#trans-authorized-person-address-detail').attr('maxlength'));

            if (parseInt(transAuthorizedPersonAddressDetail.length) === 0 || parseInt(transAuthorizedPersonAddressDetail.length) > parseInt(maximumLength)) {
                result = false;
                $('#trans-authorized-person-address-detail-error').removeClass('d-none');
            }

            //Authorized Person Contact Detail
            maximumLength = parseInt($('#authorized-person-contact-detail').attr('maxlength'));

            if (parseInt(authorizedPersonContactDetail.length) === 0 || parseInt(authorizedPersonContactDetail.length) > parseInt(maximumLength)) {
                result = false;
                $('#authorized-person-contact-detail-error').removeClass('d-none');
            }

            //Trans Authorized Person Contact Detail
            maximumLength = parseInt($('#trans-authorized-person-contact-detail').attr('maxlength'));

            if (parseInt(transAuthorizedPersonContactDetail.length) === 0 || parseInt(transAuthorizedPersonContactDetail.length) > parseInt(maximumLength)) {
                result = false;
                $('#trans-authorized-person-contact-detail-error').removeClass('d-none');
            }
        }

        if (isAuthorizedSignatory === true) {

            // Validate Photo Document
            if (fileUploader.files.length === 0) {
                if (personInformationParameterViewModel.SignDocumentUpload === MANDATORY) {
                    // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                    if (isDbRecord === false || isChangedSign === true) {
                        result = false;
                        $('#group-sign-file-uploader-error').removeClass('d-none');
                    }

                }
                else {
                    let photoSrc = $('#group-sign-file-uploader-image-preview').attr('src');

                    // Don't Change, It Is Refereed For AttachFileUploader()
                    if (photoSrc.toString().length < 2) {
                        fileCaption = 'NotApplicable';
                        localStoragePath = 'None';

                    }
                }
            }

            // file Caption
            maximumLength = parseInt($('#group-sign-file-caption').attr('maxlength'));

            if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
                result = false;
                $('#group-sign-file-caption-error').removeClass('d-none');
                $('#group-sign-file-caption-error').removeClass('d-none');
            }
        }
        else {
            filePath = 'None';
            fileCaption = 'NotApplicable';
            fileNameDocument = 'None';
            localStoragePath = 'None';
            $('#group-sign-file-uploader-error').addClass('d-none');
            $('#group-sign-file-caption-error').addClass('d-none');
        }
        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsAuthorizedSignatoryDataTable() {
        authorizedSignatoryDataTable.column(1).visible(false);
        authorizedSignatoryDataTable.column(9).visible(false);
        authorizedSignatoryDataTable.column(17).visible(false);
        authorizedSignatoryDataTable.column(18).visible(false);
        authorizedSignatoryDataTable.column(19).visible(false);
        authorizedSignatoryDataTable.column(20).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@   Validation(Event Handling) Code - Board of Director Relation  @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-relation-dt').click(function () {
        debugger;
        event.preventDefault();
        editedBoardOfDirectorId = '';
        SetBoardOfDirectorUniqueDropdownList();

        let count = $('#board-of-director-id option').length;
        if (count === 1) {
            alert('Oops! It looks Like There Are No Records Available For Entry At This Time');
        }
        else {
            SetModalTitle('relation', 'Add');
        }

    });

    // DataTable Edit Button 
    $('#btn-edit-relation-dt').click(function () {
        SetModalTitle('relation', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-relation-dt').data('rowindex');
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#relation-modal').modal();

            editedBoardOfDirectorId = columnValues[1];

            SetBoardOfDirectorUniqueDropdownList();

            $('#board-of-director-id', myModal).val(columnValues[1]);
            $('#relation-id', myModal).val(columnValues[3]);
            $('#note-board-of-director', myModal).val(columnValues[5]);
            $('#reason-for-modification-board-of-director', myModal).val(columnValues[6]);

            // Show Modals
            $('#relation-modal').modal('show');
        }
        else {
            $('#btn-edit-relation-edit-dt').addClass('read-only');
            $('#relation-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-relation-modal').click(function (event) {
        if (IsValidRelationModal()) {
            row = boardOfDirectorDataTable.row.add([
                        tag,
                        boardofdirector,
                        boardofdirectorText,
                        relation,
                        relationText,
                        note,
                        reasonForModification
            ]).draw();
            $('#relation-accordion-error').addClass('d-none');

            HideColumnsRelationDataTable();

            boardOfDirectorDataTable.columns.adjust().draw();

            $('#relation-modal').modal('hide');

            EnableNewOperation('relation');
        }
    });

    // Modal update Button Event
    $('#btn-update-relation-modal').click(function (event) {
        $('#select-all-relation').prop('checked', false);
        if (IsValidRelationModal()) {
            boardOfDirectorDataTable.row(selectedRowIndex).data([
                        tag,
                        boardofdirector,
                        boardofdirectorText,
                        relation,
                        relationText,
                        note,
                        reasonForModification
            ]).draw();

            HideColumnsRelationDataTable();

            boardOfDirectorDataTable.columns.adjust().draw();

            $('#relation-modal').modal('hide');

            EnableNewOperation('relation');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-relation-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-relation tbody input[type="checkbox"]:checked').each(function () {
                 boardOfDirectorDataTable.row($('#tbl-relation tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-relation-dt').data('rowindex');
                  EnableNewOperation('relation');
                    SetBoardOfDirectorUniqueDropdownList();

                  $('#select-all-relation').prop('checked', false);
                    // Add Required Error Message, If Table Has Not Any Record

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-relation').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-relation tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = boardOfDirectorDataTable.row(row).index();

                rowData = (boardOfDirectorDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-relation-dt').data('rowindex', arr);
                EnableDeleteOperation('relation');
            });
        }
        else {
            EnableNewOperation('relation');

            $('#tbl-relation tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-relation tbody').click("input[type=checkbox]", function () {
        $('#tbl-relation input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = boardOfDirectorDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (boardOfDirectorDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('relation');

                    $('#btn-update-relation-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-relation-dt').data('rowindex', rowData);
                    $('#btn-delete-relation-dt').data('rowindex', arr);
                    $('#select-all-relation').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-relation tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('relation');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('relation');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('relation');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-relation').prop('checked', true);
        else
            $('#select-all-relation').prop('checked', false);
    });

    // Validate relation Module
    function IsValidRelationModal() {
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        boardofdirector = $('#board-of-director-id option:selected').val();
        boardofdirectorText = $('#board-of-director-id option:selected').text();
        relation = $('#relation-id option:selected').val();
        relationText = $('#relation-id option:selected').text();
        note = $('#note-board-of-director').val();
        reasonForModification = $('#reason-for-modification-board-of-director').val();

        //Set Default Value if Empty
        if (note === '') {
            $('#note-board-of-director').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-board-of-director').val('None');
            reasonForModification = 'None';
        }

        if (($('#board-of-director-id').prop('selectedIndex')) < 1) {
            result = false;
            $('#board-of-director-id-error').removeClass('d-none');
        }

        if (($('#relation-id').prop('selectedIndex')) < 1) {
            result = false;
            $('#relation-id-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsRelationDataTable() {
        boardOfDirectorDataTable.column(1).visible(false);
        boardOfDirectorDataTable.column(3).visible(false);
    }

    function SetBoardOfDirectorUniqueDropdownList() {
        // Show All List Items
        $('#board-of-director-id').html('');
        $('#board-of-director-id').append(BOARD_OF_DROPDOWN_LIST);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-relation > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (boardOfDirectorDataTable.row(currentRow).data());

            if (typeof myColumnValues !== 'undefined' && myColumnValues !== null) {
                if (myColumnValues[1] !== editedBoardOfDirectorId)
                    $('#board-of-director-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Validation Code- Bank Detail @@@@@@@@@@@@@@@@@@@@@@@@@@@


    function SetBranchDropdownList() {
        $.get('/DynamicDropdownList/GetBankBranchDropdownListByBankId', { _bankId: bankId, async: false }, function (data, textStatus, jqXHR) {
            debugger;

            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Branch --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#bank-branch-id').html(dropdownListItems);

            dropDownListItemCount = $('#bank-branch-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (dropDownListItemCount === 1) {
                $('#bank-branch-id').prop('selectedIndex', 1);
            }
            else {
                if (bankBranchId !== '') {
                    $('#bank-branch-id').val(bankBranchId);
                }
            }
        });
    }

    function IsValidBankAccountNumber(_myId) {
        let myResult = true;
        let myErrorId = '#' + _myId + '-error';

        let regExp = new RegExp('^\d{9,18}$');
        // ^ :- Beginning of the string. 
        // [0-9] :- Match any character in the set.
        // {9,18} :- Match Between 9 to 18 of the preceding token.
        // $ :- End of the string.

        // Check if the account number from 9 to 18 digit by RBI
        if (accountNumber.length < 10 || accountNumber.length > 18) {
            $(myErrorId).html('Account Number Length From 9 Digit To 18 Digit.');

            myResult = false;
        }

        // Check if the account number contains only numeric digits
        if (regExp.test(accountNumber)) {
            $(myErrorId).html('Account Number Must Only Contain Digit.');

            myResult = false;
        }

        if (myResult === true) {
            $(myErrorId).addClass('d-none');

        }
        else {
            $(myErrorId).removeClass('d-none');
        }
    }

    $('#bank-id').focusout(function () {
        debugger;
        const currentValue = $(this).val();
        bankId = $(this).val();

        if (currentValue !== lastSelectedValue) {
            $('#account-number').val('');
            $('#activation-open-date-bank').val('');
            $('#expiry-open-date-bank').val('');
            $('#is-default-bank-transaction').prop('checked', false);
            $('#bank-file-uploader').val('');
            $('#bank-file-uploader-image-preview').attr('src', '');
            $('#bank-file-caption').val('');
            $('#note-bank-detail').val('');
        }

        lastSelectedValue = currentValue;
        SetBranchDropdownList();
    });

    $('#account-number').focusout(function () {
        IsValidBankAccountNumber('account-number');
    });


    /// @@@@@@@@@@@@@@@@@@@@@@   Validation(Event Handling) Code - Bank Detail @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-bank-detail-dt').click(function () {
        event.preventDefault();
        isDbRecord = false;
        bankBranchId = '';
        lastSelectedValue = '';

        personBankDetailDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';

        SetModalTitle('bank-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-bank-detail-dt').click(function () {
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('bank-detail', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            let tmpDate = new Date();

            columnValues = $('#btn-edit-bank-detail-dt').data('rowindex');
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#bank-detail-modal').modal();

            tmpDate = new Date(columnValues[6]);
            tmpDate.setDate(tmpDate.getDate() + 1);

            accountopeningDate = new Date(columnValues[6]);
            accountclosingDate = new Date(columnValues[7]);

            bankId = columnValues[1];
            bankBranchId = columnValues[3];
            lastSelectedValue = columnValues[1];

            $('#expiry-open-date').attr('min', GetInputDateFormat(tmpDate))

            $('#bank-id', myModal).val(columnValues[1]);

            SetBranchDropdownList();

            $('#bank-branch-id', myModal).val(columnValues[3]);
            $('#account-number', myModal).val(columnValues[5]);
            $('#activation-open-date-bank', myModal).val(GetInputDateFormat(accountopeningDate));
            $('#expiry-open-date-bank', myModal).val(GetInputDateFormat(accountclosingDate));

            $('#is-default-bank-transaction', myModal).prop('checked', columnValues[8].toString().toLowerCase() === 'true' ? true : false);

            $('#bank-file-caption', myModal).val(columnValues[11]);

            $('#note-bank-detail', myModal).val(columnValues[12]);

            $('#reason-for-modification-bank-detail', myModal).val(columnValues[13]);

            fileUploader = $('#' + $(columnValues[9]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'bank-file-uploader';

            // columnValues[3] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[9]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[9]).attr('class') === 'db-record' ? true : false;

            // columnValues[10] - Image Tag Html
            filePath = $('#' + $(columnValues[10]).attr('id')).attr('src');

            fileNameDocument = columnValues[14];
            personBankDetailDocumentPrmKey = columnValues[15];
            localStoragePath = columnValues[16];

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                AttachFileUploader();
            }

            $('#bank-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#bank-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-bank-detail-edit-dt').addClass('read-only');
            $('#bank-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-bank-detail-modal').click(function (event) {
        debugger;
        if (IsValidBankDetailModal()) {
            row = bankDataTable.row.add([
                    tag,
                    bankId,
                    bankText,
                    bankBranch,
                    bankBranchText,
                    accountNumber,
                    openingDate,
                    closeDate,
                    isDefaultBankForTransaction,
                    fileUploaderInputHtml,
                    imageTagHtml,
                    fileCaption,
                    note,
                    reasonForModification,
                    fileNameDocument,
                    personBankDetailDocumentPrmKey,
                    localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideColumnsBankDetailDataTable();

            bankDataTable.columns.adjust().draw();

            $('#bank-detail-modal').modal('hide');

            EnableNewOperation('bank-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-bank-detail-modal').click(function (event) {
        $('#select-all-bank-detail').prop('checked', false);
        if (IsValidBankDetailModal()) {
            bankDataTable.row(selectedRowIndex).data([
                    tag,
                    bankId,
                    bankText,
                    bankBranch,
                    bankBranchText,
                    accountNumber,
                    openingDate,
                    closeDate,
                    isDefaultBankForTransaction,
                    fileUploaderInputHtml,
                    imageTagHtml,
                    fileCaption,
                    note,
                    reasonForModification,
                    fileNameDocument,
                    personBankDetailDocumentPrmKey,
                    localStoragePath

            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideColumnsBankDetailDataTable();

            bankDataTable.columns.adjust().draw();

            $('#bank-detail-modal').modal('hide');

            EnableNewOperation('bank-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-bank-detail-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-bank-detail tbody input[type="checkbox"]:checked').each(function () {
                 bankDataTable.row($('#tbl-bank-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-bank-detail-dt').data('rowindex');
                  EnableNewOperation('bank-detail');

                  $('#select-all-bank-detail').prop('checked', false);

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-bank-detail').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-bank-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = bankDataTable.row(row).index();

                rowData = (bankDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-bank-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('bank-detail');
            });
        }
        else {
            EnableNewOperation('bank-detail');

            $('#tbl-bank-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-bank-detail tbody').click('input[type=checkbox]', function () {
        $('#tbl-bank-detail input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = bankDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (bankDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('bank-detail');

                    $('#btn-update-bank-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-bank-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-bank-detail-dt').data('rowindex', arr);
                    $('#select-all-bank-detail').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-bank-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('bank-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('bank-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('bank-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-bank-detail').prop('checked', true);
        else
            $('#select-all-bank-detail').prop('checked', false);
    });

    // Validate Bank Module
    function IsValidBankDetailModal() {
        result = true;

        counter++;
        fileUploaderId = "data-table-bank-file-uploader" + counter;
        fileId = "photo-Id" + counter;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        bankId = $('#bank-id option:selected').val();
        bankText = $('#bank-id option:selected').text();
        bankBranch = $('#bank-branch-id option:selected').val();
        bankBranchText = $('#bank-branch-id option:selected').text();
        accountNumber = $('#account-number').val();
        openingDate = $('#activation-open-date-bank').val();
        closeDate = $('#expiry-open-date-bank').val();
        isDefaultBankForTransaction = $('#is-default-bank-transaction').is(':checked') ? true : false;
        fileCaption = $('#bank-file-caption').val();
        note = $('#note-bank-detail').val();
        reasonForModification = $('#reason-for-modification-bank-detail').val();

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#bank-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        fileUploader = $('#bank-file-uploader').get(0);

        //Set Default Value if Empty
        if (note === '') {
            $('#note-bank-detail').val('None');
            note = 'None';
        }

        if (fileCaption === '') {
            $('#bank-file-caption').val('None');
            fileCaption = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-bank-detail').val('None');
            reasonForModification = 'None';
        }

        if ($('#bank-id').prop('selectedIndex') < 1) {
            result = false;
            $('#bank-id-error').removeClass('d-none');
        }

        if ($('#bank-branch-id').prop('selectedIndex') < 1) {
            result = false;
            $('#bank-branch-id-error').removeClass('d-none');
        }

        if (isNaN(accountNumber.length) === false) {

            minimumLength = parseInt($('#account-number').attr('minlength'));
            maximumLength = parseInt($('#account-number').attr('maxlength'));

            if (parseInt(accountNumber.length) < parseInt(minimumLength) || parseInt(accountNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#account-number-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#account-number-error').removeClass('d-none');
        }

        let isValidOpeningDate = IsValidInputDate('#activation-open-date-bank');

        if (isValidOpeningDate === false) {
            result = false;
            $('#activation-open-date-bank-error').removeClass('d-none');
        }

        if (isDefaultBankForTransaction === '') {
            result = false;
            $('#is-default-bank-transaction-error').removeClass('d-none');
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.BankStatementDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#bank-file-uploader-error').removeClass('d-none');
                }

            }
            else {
                // Don't Change, It Is Refereed For AttachFileUploader()
                let photoSrc = $('#bank-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
        }

        // file Caption
        if (isNaN(fileCaption.length) === false) {

            maximumLength = parseInt($('#bank-file-caption').attr('maxlength'));

            if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
                result = false;
                $('#bank-file-caption-error').removeClass('d-none');
            }
        } else {
            result = false;
            $('#bank-file-caption-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsBankDetailDataTable() {
        bankDataTable.column(1).visible(false);
        bankDataTable.column(3).visible(false);
        bankDataTable.column(14).visible(false);
        bankDataTable.column(15).visible(false);
        bankDataTable.column(16).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Validation(Event Handling) Code - Chronic Disease @@@@@@@@@@@@@@@@@@@@@@@@@@@


    //Chronic disease Dropdown focusout values get clear
    $('#disease-id').focusout(function () {
        debugger;
        let currentValue = $(this).val();

        // If the value has changed from the initial value, clear the related fields
        if (currentValue !== lastSelectedValue) {
            $('#other-detail').val('');
            $('#note-chronic-disease').val('');
        }

        lastSelectedValue = currentValue;
    });


    /// @@@@@@@@@@@@@@@@@@@@@@  Person Chronic Disease - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-chronic-disease-dt').click(function () {
        event.preventDefault();
        lastSelectedValue = '';
        SetModalTitle('chronic-disease', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-chronic-disease-dt').click(function () {
        SetModalTitle('chronic-disease', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-chronic-disease-dt').data('rowindex');
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#chronic-disease-modal').modal();

            lastSelectedValue = columnValues[1];

            $('#disease-id', myModal).val(columnValues[1]);
            $('#other-detail', myModal).val(columnValues[3]);
            $('#note-chronic-disease', myModal).val(columnValues[4]);
            $('#reason-for-modification-chronic-disease', myModal).val(columnValues[5]);

            // Show Modals
            $('#chronic-disease-modal').modal('show');
        }
        else {
            $('#btn-edit-chronic-disease-edit-dt').addClass('read-only');
            $('#chronic-disease-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-chronic-disease-modal').click(function (event) {
        debugger;
        if (IsValidChronicDiseaseModal()) {
            row = chronicDataTable.row.add([
                        tag,
                        disease,
                        diseaseText,
                        otherDetails,
                        note,
                        reasonForModification
            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            $('#chronic-disease-data-table-error').addClass('d-none');

            HideColumnsChronicDiseaseDataTable();

            chronicDataTable.columns.adjust().draw();

            $('#chronic-disease-modal').modal('hide');

            EnableNewOperation('chronic-disease');
        }
    });

    // Modal update Button Event
    $('#btn-update-chronic-disease-modal').click(function (event) {
        let b = $('#btn-edit-chronic-disease-dt').attr('rowindex');
        $('#select-all-chronic-disease').prop('checked', false);
        if (IsValidChronicDiseaseModal()) {
            chronicDataTable.row(selectedRowIndex).data([
                        tag,
                        disease,
                        diseaseText,
                        otherDetails,
                        note,
                        reasonForModification
            ]).draw();

            HideColumnsChronicDiseaseDataTable();

            chronicDataTable.columns.adjust().draw();

            $('#chronic-disease-modal').modal('hide');

            EnableNewOperation('chronic-disease');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-chronic-disease-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-chronic-disease tbody input[type="checkbox"]:checked').each(function () {
                 chronicDataTable.row($('#tbl-chronic-disease tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-chronic-disease-dt').data('rowindex');
                  EnableNewOperation('chronic-disease');

                  $('#select-all-chronic-disease').prop('checked', false);

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-chronic-disease').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-chronic-disease tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = chronicDataTable.row(row).index();

                rowData = (chronicDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-chronic-disease-dt').data('rowindex', arr);
                EnableDeleteOperation('chronic-disease');
            });
        }
        else {
            EnableNewOperation('chronic-disease');

            $('#tbl-chronic-disease tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-chronic-disease tbody').click('input[type=checkbox]', function () {
        $('#tbl-chronic-disease input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = chronicDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (chronicDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('chronic-disease');

                    $('#btn-update-chronic-disease-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-chronic-disease-dt').data('rowindex', rowData);
                    $('#btn-delete-chronic-disease-dt').data('rowindex', arr);
                    $('#select-all-chronic-disease').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-chronic-disease tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('chronic-disease');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('chronic-disease');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('chronic-disease');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-chronic-disease').prop('checked', true);
        else
            $('#select-all-chronic-disease').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidChronicDiseaseModal() {
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        disease = $('#disease-id option:selected').val();
        diseaseText = $('#disease-id option:selected').text();
        otherDetails = $('#other-detail').val();
        note = $('#note-chronic-disease').val();
        reasonForModification = $('#reason-for-modification-chronic-disease').val();

        //Set Default Value if Empty
        if (note === '') {
            $('#note-chronic-disease').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-chronic-disease').val('None');
            reasonForModification = 'None';
        }

        if (otherDetails === '') {
            $('#other-detail').val('None');
            otherDetails = 'None';
        }

        if ($('#disease-id').prop('selectedIndex') < 1) {
            result = false;
            $('#disease-id-error').removeClass('d-none');
        }

        //other Details
        if (isNaN(otherDetails.length) === false) {
            maximumLength = parseInt($('#other-detail').attr('maxlength'));

            if (parseInt(otherDetails.length) === 0 || parseInt(otherDetails.length) > parseInt(maximumLength)) {
                result = false;
                $('#other-detail-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#other-detail-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsChronicDiseaseDataTable() {
        chronicDataTable.column(1).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@   Validation(Event Handling) Code - Insurance Detail  @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // Start Date
    $('#activation-date').click(function () {
        $('#close-date-person-insurance').val('');
    });

    // Maturity Date
    $('#expiry-date').click(function () {
        $('#close-date-person-insurance').val('');
    });

    // Update max attribute based on purchase price and clear sanction loan amount
    $('#policy-premium').focusout(function () {
        let policyPremium = parseFloat($('#policy-premium').val());

        // Policy Premium If Empty Or Invalid
        if (isNaN(policyPremium) === true) {
            policyPremium = 0;
        }

        $('#policy-sum-assured').attr('min', policyPremium);

        $('#policy-sum-assured').val('');
    });

    // All Input Filed Values Clear After Change Insurance Type Id
    $('#insurance-type-id').focusout(function () {
        let currentValue = $(this).val();

        if (lastSelectedValue !== currentValue) {
            $('#insurance-company-id').val('');
            $('#activation-date').val('');
            $('#expiry-date').val('');
            $('#policy-number').val('');
            $('#policy-premium').val('');
            $('#policy-sum-assured').val('');
            $('#overdues-premium').val('');
            $('#has-any-mortgage-insurance').prop('checked', false);
            $('#note-insurance-detail').val('');
        }

        lastSelectedValue = currentValue;
    });

    // Validate Unique Policy Number
    $('#policy-number').focusout(function () {
        let filteredData = insuranceDataTable
            .rows()
            .indexes()
            .filter(function (value, index) {
                return insuranceDataTable.row(value).data()[8] == $('#policy-number').val();
            });

        if (insuranceDataTable.rows(filteredData).count() > 0 && editedPolicyNumber !== $('#policy-number').val())
            $('#policy-numbers-error').removeClass('d-none');
        else
            $('#policy-numbers-error').addClass('d-none');
    });


    /// @@@@@@@@@@@@@@@@@@@@@@  Person Insurance Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-insurance-detail-dt').click(function () {

        event.preventDefault();
        editedPolicyNumber = 0
        lastSelectedValue = '';
        SetModalTitle('insurance-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-insurance-detail-dt').click(function () {
        SetModalTitle('insurance-detail', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            let tmpDate = new Date();

            columnValues = $('#btn-edit-insurance-detail-dt').data('rowindex');
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#insurance-detail-modal').modal();


            lastSelectedValue = columnValues[1];

            tmpDate = new Date(columnValues[5]);
            tmpDate.setDate(tmpDate.getDate() + 1);

            startDate = new Date(columnValues[5]);
            maturityDate = new Date(columnValues[6]);
            closeDate = new Date(columnValues[7]);

            $('#expiry-date').attr('min', GetInputDateFormat(tmpDate))
            $('#close-date-person-insurance').attr('min', GetInputDateFormat(tmpDate))

            $('#policy-sum-assured').attr('min', columnValues[9]);

            $('#insurance-type-id', myModal).val(columnValues[1]);
            $('#insurance-company-id', myModal).val(columnValues[3]);
            $('#activation-date', myModal).val(GetInputDateFormat(startDate));
            $('#expiry-date', myModal).val(GetInputDateFormat(maturityDate));
            $('#close-date-person-insurance', myModal).val(GetInputDateFormat(closeDate));
            $('#policy-number', myModal).val(columnValues[8]);
            $('#policy-premium', myModal).val(columnValues[9]);
            $('#policy-sum-assured', myModal).val(columnValues[10]);
            $('#overdues-premium', myModal).val(columnValues[11]);
            $('#has-any-mortgage-insurance', myModal).val(columnValues[12]);
            $('#note-insurance-detail', myModal).val(columnValues[13]);
            $('#reason-for-modification-insurance-detail', myModal).val(columnValues[14]);

            editedPolicyNumber = columnValues[8];

            if (columnValues[12] === 'True') {
                $('#has-any-mortgage-insurance').prop('checked', true);
            }
            else {
                $('#has-any-mortgage-insurance').prop('checked', false);
            }

            // Show Modals
            $('#insurance-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-insurance-detail-edit-dt').addClass('read-only');
            $('#insurance-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-insurance-detail-modal').click(function (event) {
        if (IsValidInsuranceModal()) {
            row = insuranceDataTable.row.add([
                        tag,
                        insuranceType,
                        insuranceTypeText,
                        insuranceCompany,
                        insuranceCompanyText,
                        startDate,
                        maturityDate,
                        closeDate,
                        policyNumber,
                        policyPremium,
                        policySumAssured,
                        overduesPremium,
                        hasAnyMortgage,
                        note,
                        reasonForModification
            ]).draw();

            // Error Message In Span
            $('#insurance-detail-data-table-error').addClass('d-none');

            HideColumnsInsuranceDataTable();

            insuranceDataTable.columns.adjust().draw();

            $('#insurance-detail-modal').modal('hide');

            EnableNewOperation('insurance-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-insurance-detail-modal').click(function (event) {
        $('#select-all-insurance-detail').prop('checked', false);
        if (IsValidInsuranceModal()) {
            insuranceDataTable.row(selectedRowIndex).data([
                                tag,
                                insuranceType,
                                insuranceTypeText,
                                insuranceCompany,
                                insuranceCompanyText,
                                startDate,
                                maturityDate,
                                closeDate,
                                policyNumber,
                                policyPremium,
                                policySumAssured,
                                overduesPremium,
                                hasAnyMortgage,
                                note,
                                reasonForModification,
            ]).draw();
            // Error Message In Span
            $('#insurance-detail-validation span').html('');

            HideColumnsInsuranceDataTable();

            insuranceDataTable.columns.adjust().draw();

            $('#insurance-detail-modal').modal('hide');

            EnableNewOperation('insurance-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-insurance-detail-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-insurance-detail tbody input[type="checkbox"]:checked').each(function () {
                 insuranceDataTable.row($('#tbl-insurance-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-insurance-detail-dt').data('rowindex');
                  EnableNewOperation('insurance-detail');

                  $('#select-all-insurance-detail').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-insurance-detail').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-insurance-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = insuranceDataTable.row(row).index();

                rowData = (insuranceDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-insurance-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('insurance-detail');
            });
        }
        else {
            EnableNewOperation('insurance-detail');

            $('#tbl-insurance-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-insurance-detail tbody').click('input[type=checkbox]', function () {
        $('#tbl-insurance-detail input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = insuranceDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (insuranceDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('insurance-detail');

                    $('#btn-update-insurance-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-insurance-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-insurance-detail-dt').data('rowindex', arr);
                    $('#select-all-insurance-detail').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-insurance-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('insurance-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('insurance-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('insurance-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-insurance-detail').prop('checked', true);
        else
            $('#select-all-insurance-detail').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidInsuranceModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        insuranceType = $('#insurance-type-id option:selected').val();
        insuranceTypeText = $('#insurance-type-id option:selected').text();
        insuranceCompany = $('#insurance-company-id option:selected').val();
        insuranceCompanyText = $('#insurance-company-id option:selected').text();
        startDate = $('#activation-date').val();
        maturityDate = $('#expiry-date').val();
        closeDate = $('#close-date-person-insurance').val();
        policyNumber = $('#policy-number').val();
        policyPremium = parseFloat($('#policy-premium').val());
        policySumAssured = parseFloat($('#policy-sum-assured').val());
        overduesPremium = parseInt($('#overdues-premium').val());
        hasAnyMortgage = $('#has-any-mortgage-insurance').is(':checked') ? "True" : "False";
        note = $('#note-insurance-detail').val();
        reasonForModification = $('#reason-for-modification-insurance-detail').val();

        // Set Default Value, If Empty
        if (note === '') {
            $('#note-insurance-detail').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-insurance-detail').val('None');
            reasonForModification = 'None';
        }

        if (overduesPremium === '') {
            overduesPremium = 0;
        }

        //vehicle model Id
        if ($('#insurance-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#insurance-type-id-error').removeClass('d-none');
        }

        //vehicle model Id
        if ($('#insurance-company-id').prop('selectedIndex') < 1) {
            result = false;
            $('#insurance-company-id-error').removeClass('d-none');
        }

        //date of purchase
        if (IsValidInputDate('#activation-date') === false) {
            result = false;
            $('#activation-date-error').removeClass('d-none');
        }

        const currentDate = new Date();
        let errorMessage = '';

        // Check if the start date field is empty
        if (startDate === '') {
            errorMessage = 'Policy Start Date is required.';
            result = false;
        } else {
            // Check if the date is in the future
            if (startDate > currentDate) {
                errorMessage = 'Start Date cannot be in the future.';
                result = false;
            }
        }

        // Show or hide error message
        if (errorMessage !== '') {
            $('#start-date-error').text(errorMessage).removeClass('d-none');
        } else {
            $('#start-date-error').addClass('d-none');
        }

        // Check if the maturity date field is empty
        if (maturityDate === '') {
            errorMessage = 'Policy Maturity Date is required.';
            result = false;
        } else {
            // Check if the maturity date is earlier than the start date
            if (maturityDate < startDate) {
                errorMessage = 'Maturity Date cannot be earlier than the Policy Start Date.';
                $('#maturity-date-insurance-error').removeClass('d-none');
                result = false;
            }
        }

        // Show or hide error message
        if (errorMessage !== '') {
            $('#expiry-date-error').text(errorMessage).removeClass('d-none');
        } else {

            $('#expiry-date-error').addClass('d-none');
        }

        //date of purchase
        if (IsValidInputDate('#expiry-date') === false) {
            result = false;
            $('#expiry-date-error').removeClass('d-none');
        }
        if (closeDate === '') {
            // Close Date can be empty, clear error
            $('#close-date-person-insurance-error').addClass('d-none');
        } else if (closeDate < startDate) {
            result = false;
            $('#close-date-person-insurance-error').removeClass('d-none');
        } else {
            $('#close-date-person-insurance-error').addClass('d-none');
        }

        //name Of Branch
        minimumLength = parseInt($('#policy-number').attr('minlength'));
        maximumLength = parseInt($('#policy-number').attr('maxlength'));

        if (parseInt(policyNumber.length) < parseInt(minimumLength) || parseInt(policyNumber.length) > parseInt(maximumLength)) {
            result = false;
            $('#policy-number-error').removeClass('d-none');
        }

        let filteredData = insuranceDataTable.rows().indexes().filter(function (value, index) {
            return insuranceDataTable.row(value).data()[8] == $('#policy-number').val();
        });

        if (insuranceDataTable.rows(filteredData).count() > 0 && editedPolicyNumber !== $('#policy-number').val()) {
            isDuplicateSequenceNumber = true;
            result = false;
            $('#policy-numbers-error').removeClass('d-none');
        }
        else {
            isDuplicateSequenceNumber = false;
            $('#policy-numbers-error').addClass('d-none');
        }

        if (isNaN(policyPremium) === false) {
            minimum = parseFloat($('#policy-premium').attr('min'));
            maximum = parseFloat($('#policy-premium').attr('max'));

            if (parseFloat(policyPremium) < parseFloat(minimum) || parseFloat(policyPremium) > parseFloat(maximum)) {
                result = false;
                $('#policy-premium-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#policy-premium-error').removeClass('d-none');
        }

        if (isNaN(policySumAssured) === false) {
            maximum = parseFloat($('#policy-sum-assured').attr('max'));
            minimum = parseFloat($('#policy-sum-assured').attr('min'));
            // Set max attribute for the current market value element
            $('#policy-sum-assured').attr('min', minimum);

            if (parseFloat(policySumAssured) < parseFloat(minimum) || parseFloat(policySumAssured) > parseFloat(maximum)) {
                result = false;
                $('#policy-sum-assured-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#policy-sum-assured-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsInsuranceDataTable() {
        insuranceDataTable.column(1).visible(false);
        insuranceDataTable.column(3).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@   Validation(Event Handling) Code -Financial Asset @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // Clear the monthly interest income amount when invested amount changes
    $('#invested-amount').focusout(function () {
        debugger;
        let investedAmount = parseFloat($(this).val());

        // Set max interest to 20% of invested amount
        let maxInterest = investedAmount * 0.2;
        $('#monthly-interest-income-amount').attr('max', maxInterest);

        // Clear Monthly Interest Income Amount field
        $('#monthly-interest-income-amount').val('');

    });

    // Monthly Interest Income Amount
    $('#monthly-interest-income-amount').focusout(function () {
        let monthlyInterest = parseFloat($(this).val());
        let investedAmount = parseFloat($('#invested-amount').val());

        // Calculate 20% of the invested amount
        let minInterest = 0;
        let maxInterest = investedAmount * 0.2;

        // Check if investedAmount is valid
        if (!isNaN(investedAmount)) {
            // Set min and max attributes dynamically
            $(this).attr('min', minInterest);
            $(this).attr('max', maxInterest);

            // Only update if the field is empty, negative, or out of the allowed range
            if (isNaN(monthlyInterest) || monthlyInterest < minInterest) {
                $(this).val(minInterest);
            } else if (monthlyInterest > maxInterest) {
                $(this).val(maxInterest);
            }

            // Prevent negative zero (-0) by resetting it to positive zero
            if ($(this).val() == '-0') {
                $(this).val(minInterest);
            }
        }
    });

    $('#financial-organization-type').focusout(function () {
        let currentValue = $(this).val();

        // If the value has changed from the initial value, clear the related fields
        if (currentValue !== lastSelectedValue) {
            $('#insurance-company-id').val('');
            $('#name-of-financial-organization').val('');
            $('#trans-name-of-financial-organization').val('');
            $('#name-of-branch').val('');
            $('#trans-name-of-branch').val('');
            $('#address-details').val('');
            $('#trans-address-details').val('');
            $('#contact-details').val('');
            $('#trans-contact-details').val('');
            $('#activation-opening-dates').val('');
            $('#expiry-opening-dates').val('');
            $('#financial-asset-type').val('');
            $('#financial-asset-description').val('');
            $('#trans-financial-asset-description').val('');
            $('#references-number').val('');
            $('#trans-references-number').val('');
            $('#invested-amount').val('');
            $('#monthly-interest-income-amount').val('');
            $('#current-market-values').val('');
            $('#has-any-mortgage-financial').prop('checked', false);
            $('#photo-path-finance').val('');
            $('#photo-path-finance-image-preview').attr('src', '');
            $('#finance-file-caption').val('');
            $('#note-financial-asset').val('');
            $('#trans-note-financial-asset').val('');
            $('#finance-file-uploader').val('');
            $('#finance-file-uploader-image-preview').attr('src', '');
        }

        // Update lastSelectedValue to the current value for subsequent changes
        lastSelectedValue = currentValue;
    });


    /// @@@@@@@@@@@@@@@@@@@@@@  Person Financial Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-financial-asset-dt').click(function () {
        event.preventDefault();
        isDbRecord = false;
        SetModalTitle('financial-asset', 'Add');

        lastSelectedValue = '';

        // ****** New Changes
        personFinancialAssetDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';
        // ****************
    });

    // DataTable Edit Button 
    $('#btn-edit-financial-asset-dt').click(function () {
        debugger;
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('financial-asset', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {

            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#financial-asset-modal').modal();

            // ****** New Changes
            let tmpDate = new Date();

            columnValues = $('#btn-edit-financial-asset-dt').data('rowindex');

            financialAssetOpeningDate = new Date(columnValues[11]);
            financialAssetMatureDate = new Date(columnValues[12]);
            lastSelectedValue = columnValues[1];

            // ****** New Changes
            tmpDate = new Date(columnValues[11]);
            tmpDate.setDate(tmpDate.getDate() + 1);
            // ****** 

            // Set Maximum Attributes
            $('#monthly-interest-income-amount').attr('min', columnValues[20]);
            $('#monthly-interest-income-amount').attr('max', columnValues[20]);

            // ****** New Changes
            $('#expiry-opening-dates').attr('min', GetInputDateFormat(tmpDate))
            // ****** 

            $('#financial-organization-type', myModal).val(columnValues[1]);
            $('#name-of-financial-organization', myModal).val(columnValues[3]);
            $('#trans-name-of-financial-organization', myModal).val(columnValues[4]);
            $('#name-of-branch', myModal).val(columnValues[5]);
            $('#trans-name-of-branch', myModal).val(columnValues[6]);
            $('#address-details', myModal).val(columnValues[7]);
            $('#trans-address-details', myModal).val(columnValues[8]);
            $('#contact-details', myModal).val(columnValues[9]);
            $('#trans-contact-details', myModal).val(columnValues[10]);
            $('#activation-opening-dates', myModal).val(GetInputDateFormat(financialAssetOpeningDate));
            $('#expiry-opening-dates', myModal).val(GetInputDateFormat(financialAssetMatureDate));
            $('#financial-asset-type', myModal).val(columnValues[13]);
            $('#financial-asset-description', myModal).val(columnValues[15]);
            $('#trans-financial-asset-description', myModal).val(columnValues[16]);
            $('#references-number', myModal).val(columnValues[17]);
            $('#trans-references-number', myModal).val(columnValues[18]);
            $('#invested-amount', myModal).val(columnValues[19]);
            $('#monthly-interest-income-amount', myModal).val(columnValues[20]);
            $('#current-market-values', myModal).val(columnValues[21]);
            $('#note-financial-asset', myModal).val(columnValues[26]);
            $('#trans-note-financial-asset', myModal).val(columnValues[27]);

            $('#reason-for-modification-financial-asset', myModal).val(columnValues[28]);

            $('#has-any-mortgage-financial', myModal).prop('checked', columnValues[22].toString().toLowerCase() === 'true' ? true : false);
            $('#finance-file-caption', myModal).val(columnValues[25]);

            fileUploader = $('#' + $(columnValues[23]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'finance-file-uploader';

            // columnValues[23] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[23]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[23]).attr('class') === 'db-record' ? true : false;

            // columnValues[24] - Image Tag Html
            filePath = $('#' + $(columnValues[24]).attr('id')).attr('src');

            fileNameDocument = columnValues[29];
            personFinancialAssetDocumentPrmKey = columnValues[30];
            localStoragePath = columnValues[31];

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                // ****** New Changes  -- Removed DocumentPrmKey           

                AttachFileUploader();
            }

            $('#finance-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#financial-asset-modal').modal('show');
        }
        else {
            $('#btn-edit-financial-asset-dt').addClass('read-only');
            $('#financial-asset-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-financial-asset-modal').click(function (event) {
        debugger;
        if (IsValidFinancialModal()) {
            row = financialDataTable.row.add([
                tag,
                financialOrganizationTypeId,
                financialOrganizationTypeIdText,
                nameOfFinancialOrganization,
                transNameOfFinancialOrganization,
                nameOfBranch,
                transNameOfBranch,
                addressDetails,
                transAddressDetails,
                contactDetails,
                transContactDetails,
                openingDate,
                maturityDate,
                financialAssetType,
                financialAssetTypeText,
                financialAssetDescription,
                transFinancialAssetDescription,
                referenceNumber,
                transReferenceNumber,
                investedAmount,
                monthlyInterestIncomeAmount,
                currentMarketValue,
                hasAnyMortgage,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                transNote,
                reasonForModification,
                fileNameDocument,
                personFinancialAssetDocumentPrmKey,
                localStoragePath
            ]).draw();


            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideColumnsFinancialDataTable();

            financialDataTable.columns.adjust().draw();

            $('#financial-asset-modal').modal('hide');

            EnableNewOperation('financial-asset');
        }
    });

    // Modal update Button Event
    $('#btn-update-financial-asset-modal').click(function (event) {
        debugger;
        $('#select-all-financial-asset').prop('checked', false);
        if (IsValidFinancialModal()) {
            financialDataTable.row(selectedRowIndex).data([
                tag,
                financialOrganizationTypeId,
                financialOrganizationTypeIdText,
                nameOfFinancialOrganization,
                transNameOfFinancialOrganization,
                nameOfBranch,
                transNameOfBranch,
                addressDetails,
                transAddressDetails,
                contactDetails,
                transContactDetails,
                openingDate,
                maturityDate,
                financialAssetType,
                financialAssetTypeText,
                financialAssetDescription,
                transFinancialAssetDescription,
                referenceNumber,
                transReferenceNumber,
                investedAmount,
                monthlyInterestIncomeAmount,
                currentMarketValue,
                hasAnyMortgage,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                transNote,
                reasonForModification,
                fileNameDocument,
                personFinancialAssetDocumentPrmKey,
                localStoragePath
            ]).draw();
            debugger;
            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideColumnsFinancialDataTable();

            financialDataTable.columns.adjust().draw();

            $('#financial-asset-modal').modal('hide');

            EnableNewOperation('financial-asset');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-financial-asset-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-financial-asset tbody input[type="checkbox"]:checked').each(function () {
                    financialDataTable.row($('#tbl-financial-asset tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-financial-asset-dt').data('rowindex');
                    EnableNewOperation('financial-asset');

                    $('#select-all-financial-asset').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-financial-asset').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-financial-asset tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = financialDataTable.row(row).index();

                rowData = (financialDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-financial-asset-dt').data('rowindex', arr);
                EnableDeleteOperation('financial-asset');
            });
        }
        else {
            EnableNewOperation('financial-asset');

            $('#tbl-financial-asset tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-financial-asset tbody').click('input[type=checkbox]', function () {
        $('#tbl-financial-asset input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = financialDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (financialDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('financial-asset');

                    $('#btn-update-financial-asset-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-financial-asset-dt').data('rowindex', rowData);
                    $('#btn-delete-financial-asset-dt').data('rowindex', arr);
                    $('#select-all-financial-asset').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-financial-asset tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('financial-asset');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('financial-asset');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('financial-asset');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-financial-asset').prop('checked', true);
        else
            $('#select-all-financial-asset').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidFinancialModal() {
        debugger;
        result = true;
        counter++;
        fileUploaderId = "data-table-finance-file-uploader" + counter;

        fileId = "photo-Id" + counter;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        financialOrganizationTypeId = $('#financial-organization-type option:selected').val();
        financialOrganizationTypeIdText = $('#financial-organization-type option:selected').text();
        nameOfFinancialOrganization = $('#name-of-financial-organization').val();
        transNameOfFinancialOrganization = $('#trans-name-of-financial-organization').val();
        nameOfBranch = $('#name-of-branch').val();
        transNameOfBranch = $('#trans-name-of-branch').val();
        addressDetails = $('#address-details').val();
        transAddressDetails = $('#trans-address-details').val();
        contactDetails = $('#contact-details').val();
        transContactDetails = $('#trans-contact-details').val();
        openingDate = $('#activation-opening-dates').val();
        maturityDate = $('#expiry-opening-dates').val();
        financialAssetType = $('#financial-asset-type option:selected').val();
        financialAssetTypeText = $('#financial-asset-type option:selected').text();
        financialAssetDescription = $('#financial-asset-description').val();
        transFinancialAssetDescription = $('#trans-financial-asset-description').val();
        referenceNumber = $('#references-number').val();
        transReferenceNumber = $('#trans-references-number').val();
        investedAmount = parseFloat($('#invested-amount').val());
        monthlyInterestIncomeAmount = parseFloat($('#monthly-interest-income-amount').val());
        currentMarketValue = parseFloat($('#current-market-values').val());
        hasAnyMortgage = $('#has-any-mortgage-financial').is(':checked') ? "True" : "False";
        note = $('#note-financial-asset').val();
        transNote = $('#trans-note-financial-asset').val();
        reasonForModification = $('#reason-for-modification-financial-asset').val();
        fileCaption = $('#finance-file-caption').val();

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#finance-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        fileUploader = $('#finance-file-uploader').get(0);

        //Set Default Value if Empty
        if (note === '') {
            $('#note-financial-asset').val('None');
            note = 'None';
        }

        if (transNote === '') {
            $('#trans-note-financial-asset').val('None');
            transNote = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-financial-asset').val('None');
            reasonForModification = 'None';
        }

        if (fileCaption === '') {
            $('#finance-file-caption').val('None');
            fileCaption = 'None';
        }

        if ($('#financial-organization-type').prop('selectedIndex') < 1) {
            result = false;
            $('#financial-organization-type-error').removeClass('d-none');
        }

        //name Of Financial Organization
        minimumLength = parseInt($('#name-of-financial-organization').attr('minlength'));
        maximumLength = parseInt($('#name-of-financial-organization').attr('maxlength'));

        if (parseInt(nameOfFinancialOrganization.length) < parseInt(minimumLength) || parseInt(nameOfFinancialOrganization.length) > parseInt(maximumLength)) {
            result = false;
            $('#name-of-financial-organization-error').removeClass('d-none');
        }

        //trans Name Of Financial Organization
        maximumLength = parseInt($('#trans-name-of-financial-organization').attr('maxlength'));

        if (parseInt(transNameOfFinancialOrganization.length) === 0 || parseInt(transNameOfFinancialOrganization.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-name-of-financial-organization-error').removeClass('d-none');
        }

        //name Of Branch
        minimumLength = parseInt($('#name-of-branch').attr('minlength'));
        maximumLength = parseInt($('#name-of-branch').attr('maxlength'));

        if (parseInt(nameOfBranch.length) < parseInt(minimumLength) || parseInt(nameOfBranch.length) > parseInt(maximumLength)) {
            result = false;
            $('#name-of-branch-error').removeClass('d-none');
        }

        //transNameOfBranch
        maximumLength = parseInt($('#trans-name-of-branch').attr('maxlength'));

        if (parseInt(transNameOfBranch.length) === 0 || parseInt(transNameOfBranch.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-name-of-branch-error').removeClass('d-none');
        }


        //addressDetails
        maximumLength = parseInt($('#address-details').attr('maxlength'));

        if (parseInt(addressDetails.length) === 0 || parseInt(addressDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#address-details-error').removeClass('d-none');
        }

        //transAddressDetails
        maximumLength = parseInt($('#trans-address-details').attr('maxlength'));

        if (parseInt(transAddressDetails.length) === 0 || parseInt(transAddressDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-address-details-error').removeClass('d-none');
        }

        //contact Details
        maximumLength = parseInt($('#contact-details').attr('maxlength'));

        if (parseInt(contactDetails.length) === 0 || parseInt(contactDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#contact-details-error').removeClass('d-none');
        }


        // trans contact Details
        maximumLength = parseInt($('#trans-contact-details').attr('maxlength'));

        if (parseInt(transContactDetails.length) === 0 || parseInt(transContactDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-contact-details-error').removeClass('d-none');
        }


        //opening date
        if (IsValidInputDate('#activation-opening-dates') === false) {
            result = false;
            $('#activation-opening-dates-error').removeClass('d-none');
        }

        //maturity date
        if (IsValidInputDate('#expiry-opening-dates') === false) {
            result = false;
            $('#expiry-opening-dates-error').removeClass('d-none');
        }

        //financial Asset Type
        if ($('#financial-asset-type').prop('selectedIndex') < 1) {
            result = false;
            $('#financial-asset-type-error').removeClass('d-none');
        }

        // financialAssetDescription
        maximumLength = parseInt($('#financial-asset-description').attr('maxlength'));

        if (parseInt(financialAssetDescription.length) === 0 || parseInt(financialAssetDescription.length) > parseInt(maximumLength)) {
            result = false;
            $('#financial-asset-description-error').removeClass('d-none');
        }

        // transFinancialAssetDescription
        maximumLength = parseInt($('#trans-financial-asset-description').attr('maxlength'));

        if (parseInt(transFinancialAssetDescription.length) === 0 || parseInt(transFinancialAssetDescription.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-financial-asset-description-error').removeClass('d-none');
        }

        // referenceNumber
        maximumLength = parseInt($('#references-number').attr('maxlength'));

        if (parseInt(referenceNumber.length) === 0 || parseInt(referenceNumber.length) > parseInt(maximumLength)) {
            result = false;
            $('#references-number-error').removeClass('d-none');
        }

        // transReferenceNumber
        maximumLength = parseInt($('#trans-references-number').attr('maxlength'));

        if (parseInt(transReferenceNumber.length) === 0 || parseInt(transReferenceNumber.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-references-number-error').removeClass('d-none');
        }

        if (isNaN(investedAmount) === false) {
            minimum = parseFloat($('#invested-amount').attr('min'));
            maximum = parseFloat($('#invested-amount').attr('max'));

            if (parseFloat(investedAmount) < parseFloat(minimum) || parseFloat(investedAmount) > parseFloat(maximum)) {
                result = false;
                $('#invested-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#invested-amount-error').removeClass('d-none');
        }

        if (isNaN(monthlyInterestIncomeAmount) === false) {
            minimum = parseFloat($('#monthly-interest-income-amount').attr('min'));
            maximum = parseFloat($('#monthly-interest-income-amount').attr('max'));

            if (parseFloat(monthlyInterestIncomeAmount) < parseFloat(minimum) || parseFloat(monthlyInterestIncomeAmount) > parseFloat(maximum)) {
                result = false;
                $('#monthly-interest-income-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#monthly-interest-income-amount-error').removeClass('d-none');
        }

        //currentMarketValue
        if (isNaN(currentMarketValue) === false) {
            minimum = parseFloat($('#current-market-values').attr('min'));
            maximum = parseFloat($('#current-market-values').attr('max'));

            if (parseFloat(currentMarketValue) < parseFloat(minimum) || parseFloat(currentMarketValue) > parseFloat(maximum)) {
                result = false;
                $('#current-market-values-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#current-market-values-error').removeClass('d-none');
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.FinancialAssetDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#finance-file-uploader-error').removeClass('d-none');
                }
            }
                // ****** New Changes
            else {
                let photoSrc = $('#finance-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
            // ***********
        }

        //filecaption
        maximumLength = parseInt($('#finance-file-caption').attr('maxlength'));

        if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#finance-file-caption-error').removeClass('d-none');
        }

        return result;

    }

    function HideColumnsFinancialDataTable() {
        financialDataTable.column(1).visible(false);
        financialDataTable.column(13).visible(false);
        financialDataTable.column(29).visible(false);
        financialDataTable.column(30).visible(false);
        financialDataTable.column(31).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Validation(Event Handling) Code -Movable Asset @@@@@@@@@@@@@@@@@@@@@@@@@@@


    function SetVehicleModelDropdownList() {
        $.get('/DynamicDropdownList/GetVehicleModelDropdownListByVehicleMakeId', { _vehicleMakeId: vehicleMakeId, async: false }, function (data) {
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Valid Vehicle Model --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#vehicle-model-id').html(dropdownListItems);

            listItemCount = $('#vehicle-model-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#vehicle-model-id').prop('selectedIndex', 1);
            }
            else {
                if (vehicleModelEditedId !== '') {
                    $('#vehicle-model-id').val(vehicleModelEditedId);
                    SetVehicleVariantDropdownList();
                }
            }
        });
    }

    function SetVehicleVariantDropdownList() {
        $.get('/DynamicDropdownList/GetVehicleVariantDropdownListByVehicleModelId', { _vehicleModelId: vehicleModelId, async: false }, function (data) {
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Valid Vehicle Variant --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#vehicle-variant-id').html(dropdownListItems);

            listItemCount = $('#vehicle-variant-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#vehicle-variant-id').prop('selectedIndex', 1);
            }
            else {
                if (vehicleVariantEditedId !== '') {
                    $('#vehicle-variant-id').val(vehicleVariantEditedId);
                }
            }
        });
    }

    function ManufacturingYearMovableAssetFocusOutEventFunction() {
        debugger;
        let today = new Date();

        // Get the manufacturing year from the input field
        let purchaseYear = parseInt($('#manufacturing-year-movable-asset').val());
        let maxPurchaseYear = parseInt(purchaseYear) + 10;

        let minPurchaseDate = new Date(purchaseYear + '-01-01');
        let maxPurchaseDate = new Date(maxPurchaseYear + '-12-31');

        // If Max Purchase Year Is Larger Than Current Year
        if (parseInt(maxPurchaseYear) > parseInt(today.getFullYear())) {
            maxPurchaseYear = today.getFullYear();
        }

        // If Max Purchase Date Is Larger Than Today
        if (maxPurchaseDate > today) {
            maxPurchaseDate = today;
        }

        $('#date-of-purchase-movable-asset').attr('min', GetInputDateFormat(minPurchaseDate));
        $('#date-of-purchase-movable-asset').attr('max', GetInputDateFormat(maxPurchaseDate));
    }

    function IsValidRegistrationNumber() {
        debugger;
        let regNumber = $("#registration-number-movable-asset").val();
        let regExp = /^[A-Z]{2}[0-9]{2}[A-Z]{1,2}[0-9]{4}$/;

        if (regExp.test(regNumber)) {
            $('#registration-number-movable-asset-error').addClass('d-none');
        }
        else {
            result = false;
            $('#registration-number-movable-asset-error').removeClass('d-none');
        }
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Number Of Owners - Clear Dependent Data
    $('#number-of-owners-movable-asset').focusout(function () {
        // Clear Dependent Data
        $('#manufacturing-year-movable-asset').val('');
        $('#date-of-purchase-movable-asset').val('');
        $('#registration-date-movable-asset').val('');
    });

    // Purchase Date
    $('#date-of-purchase-movable-asset').click(function () {
        $('#registration-date-movable-asset').val('');

    });

    function RegistrationDateClickEventFunction() {
        // Validate Date If Owner Is 2 Or More - 
        // If 1 - Date Of Purchase Always Earlier Or Same Than Registration
        // If More Than 1 - Date Of Purchase Always Higer Than Registration

        let today = new Date();

        if (parseInt($('#number-of-owners-movable-asset').val()) === 1) {
            let dateOfPurchase = new Date($('#date-of-purchase-movable-asset').val());

            // Allow 10 Years Gap Between Purchase And Registration
            let maxRegistrationDate = new Date(dateOfPurchase);
            maxRegistrationDate.setFullYear(maxRegistrationDate.getFullYear() + 10);

            // If Max Registration Date Is Larger Than Today
            if (maxRegistrationDate > today) {
                maxRegistrationDate = today;
            }

            $('#registration-date-movable-asset').attr('min', GetInputDateFormat(dateOfPurchase));
            $('#registration-date-movable-asset').attr('max', GetInputDateFormat(maxRegistrationDate));
        }
        else {
            // For More Than 1 Owner Registration Date Must Earlier Than Purchase Date
            let dateOfPurchase = new Date($('#date-of-purchase-movable-asset').val());

            // Allow Older Date Upto Manufacture Year
            let minRegistrationDate = new Date($('#manufacturing-year-movable-asset').val() + '-01-01');

            $('#registration-date-movable-asset').attr('min', GetInputDateFormat(minRegistrationDate));
            $('#registration-date-movable-asset').attr('max', GetInputDateFormat(dateOfPurchase));
        }
    }

    // Registration Date
    $('#registration-date-movable-asset').click(function () {
        RegistrationDateClickEventFunction();
    });

    // Purchase Price
    $('#purchase-price-movable-asset').focusout(function () {
        $('#current-market-value-movable-asset').attr('max', $(this).val());
    });

    // Manufacturing Year
    $('#manufacturing-year-movable-asset').focusout(function () {
        ManufacturingYearMovableAssetFocusOutEventFunction();
        $('#date-of-purchase-movable-asset').val('');
    });

    // Vehicle Make
    $('#vehicle-make-id').focusout(function () {
        let currentValue = $(this).val();

        vehicleMakeId = $(this).val();
        //$('#vehicle-model-id').val('');

        if (currentValue !== lastVehicleMakeSelectedValue) {

            $('#number-of-owners-movable-asset').val('');
            $('#manufacturing-year-movable-asset').val('');
            $('#date-of-purchase-movable-asset').val('');
            $('#registration-date-movable-asset').val('');
            $('#registration-number-movable-asset').val('');
            $('#purchase-price-movable-asset').val('');
            $('#current-market-value-movable-asset').val('');
            $('#ownership-percentage-movable-asset').val('');
            $('#has-any-mortgage-movable').prop('checked', false);
            $('#is-ownership-deceased-movable').prop('checked', false);
            $('#movable-file-caption').val('');
            $('#note-movable-asset').val('');
            $('#movable-file-uploader').val('');
            $('#movable-file-uploader-image-preview').attr('src', '');
            $('.modal-input-error').addClass('d-none');
        }

        SetVehicleModelDropdownList();
    });

    // Vehicle Model
    $('#vehicle-model-id').focusout(function () {

        let currentValue = $(this).val();

        vehicleModelId = currentValue;

        //$('#vehicle-model-id').val('');

        if (currentValue !== lastVehicleModelSelectedValue) {
            $('#number-of-owners-movable-asset').val('');
            $('#manufacturing-year-movable-asset').val('');
            $('#date-of-purchase-movable-asset').val('');
            $('#registration-date-movable-asset').val('');
            $('#registration-number-movable-asset').val('');
            $('#purchase-price-movable-asset').val('');
            $('#current-market-value-movable-asset').val('');
            $('#ownership-percentage-movable-asset').val('');
            $('#has-any-mortgage-movable').prop('checked', false);
            $('#is-ownership-deceased-movable').prop('checked', false);
            $('#movable-file-caption').val('');
            $('#note-movable-asset').val('');
            $('#movable-file-uploader').val('');
            $('#movable-file-uploader-image-preview').attr('src', '');
            $('.modal-input-error').addClass('d-none');
        }

        SetVehicleVariantDropdownList();
    });


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Movable Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-movable-asset-dt').click(function () {
        event.preventDefault();
        isDbRecord = false;
        lastVehicleMakeSelectedValue = '';
        lastVehicleModelSelectedValue = '';
        vehicleModelEditedId = '';
        vehicleVariantEditedId = '';

        vehicleMakeId = '';
        vehicleModelId = '';

        personMovableAssetDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';

        SetModalTitle('movable-asset', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-movable-asset-dt').click(function () {
        debugger;
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('movable-asset', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {

            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#movable-asset-modal').modal();

            let tmpDate = new Date();

            columnValues = $('#btn-edit-movable-asset-dt').data('rowindex');

            movableDateOfPurchase = new Date(columnValues[9]);
            movableAssetRegistrationDate = new Date(columnValues[10]);

            lastVehicleMakeSelectedValue = columnValues[1];
            lastVehicleModelSelectedValue = columnValues[3];
            vehicleModelEditedId = columnValues[3];
            vehicleVariantEditedId = columnValues[5];

            vehicleMakeId = columnValues[1];
            vehicleModelId = columnValues[3];

            tmpDate = new Date(columnValues[9]);
            tmpDate.setDate(tmpDate.getDate());

            $('#vehicle-make-id', myModal).val(columnValues[1]);

            SetVehicleModelDropdownList();

            $('#vehicle-model-id', myModal).val(columnValues[3]);

            SetVehicleVariantDropdownList();

            $('#current-market-value-movable-asset').attr('max', columnValues[12]);

            $('#vehicle-variant-id', myModal).val(columnValues[5]);
            $('#number-of-owners-movable-asset', myModal).val(columnValues[7]);
            $('#manufacturing-year-movable-asset', myModal).val(columnValues[8]);
            $('#date-of-purchase-movable-asset', myModal).val(GetInputDateFormat(movableDateOfPurchase));
            $('#registration-date-movable-asset', myModal).val(GetInputDateFormat(movableAssetRegistrationDate));
            $('#registration-number-movable-asset', myModal).val(columnValues[11]);
            $('#purchase-price-movable-asset', myModal).val(columnValues[12]);
            $('#current-market-value-movable-asset', myModal).val(columnValues[13]);
            $('#ownership-percentage-movable-asset', myModal).val(columnValues[14]);

            $('#has-any-mortgage-movable').prop('checked', columnValues[15].toString().toLowerCase() === 'true' ? true : false);

            $('#is-ownership-deceased-movable').prop('checked', columnValues[16].toString().toLowerCase() === 'true' ? true : false);

            $('#movable-file-caption', myModal).val(columnValues[19]);
            $('#note-movable-asset', myModal).val(columnValues[20]);
            $('#reason-for-modification-movable-asset', myModal).val(columnValues[21]);

            fileUploader = $('#' + $(columnValues[17]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'movable-file-uploader';

            // columnValues[4] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[17]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[17]).attr('class') === 'db-record' ? true : false;

            // columnValues[5] - Image Tag Html
            filePath = $('#' + $(columnValues[18]).attr('id')).attr('src');

            fileNameDocument = columnValues[22];
            personMovableAssetDocumentPrmKey = columnValues[23];
            localStoragePath = columnValues[24];

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                AttachFileUploader();
            }

            ManufacturingYearMovableAssetFocusOutEventFunction();
            RegistrationDateClickEventFunction();

            // Auto Called Vehicle Variant DropdownList
            SetVehicleModelDropdownList();

            $('#movable-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#movable-asset-modal').modal('show');
        }
        else {
            $('#btn-edit-movable-asset-edit-dt').addClass('read-only');
            $('#movable-asset-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-movable-asset-modal').click(function (event) {
        debugger;
        if (IsValidMovableAssetModal()) {
            debugger;
            row = movableDataTable.row.add([
                tag,
                vehicleMakeId,
                vehicleMakeIdText,
                vehicleModelId,
                vehicleModelIdText,
                vehicleletiant,
                vehicleletiantText,
                numberOfOwners,
                manufacturingYear,
                dateOfPurchase,
                registrationDate,
                registrationNumber,
                purchasePrice,
                currentMarketValue,
                ownershipPercentage,
                hasAnyMortgage,
                isOwnershipDeceased,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                personMovableAssetDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            $('#movable-asset-data-table-error').addClass('d-none');

            HideColumnsMovableAssetDataTable();

            movableDataTable.columns.adjust().draw();

            $('#movable-asset-modal').modal('hide');

            EnableNewOperation('movable-asset');
        }
    });

    // Modal update Button Event
    $('#btn-update-movable-asset-modal').click(function (event) {
        $('#select-all-movable-asset').prop('checked', false);
        if (IsValidMovableAssetModal()) {
            movableDataTable.row(selectedRowIndex).data([
                tag,
                vehicleMakeId,
                vehicleMakeIdText,
                vehicleModelId,
                vehicleModelIdText,
                vehicleletiant,
                vehicleletiantText,
                numberOfOwners,
                manufacturingYear,
                dateOfPurchase,
                registrationDate,
                registrationNumber,
                purchasePrice,
                currentMarketValue,
                ownershipPercentage,
                hasAnyMortgage,
                isOwnershipDeceased,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                personMovableAssetDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideColumnsMovableAssetDataTable();

            movableDataTable.columns.adjust().draw();

            $('#movable-asset-modal').modal('hide');

            EnableNewOperation('movable-asset');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-movable-asset-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-movable-asset tbody input[type="checkbox"]:checked').each(function () {
                    movableDataTable.row($('#tbl-movable-asset tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-movable-asset-dt').data('rowindex');
                    EnableNewOperation('movable-asset');

                    $('#select-all-movable-asset').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-movable-asset').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-movable-asset tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = movableDataTable.row(row).index();

                rowData = (movableDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-movable-asset-dt').data('rowindex', arr);
                EnableDeleteOperation('movable-asset');
            });
        }
        else {
            EnableNewOperation('movable-asset');

            $('#tbl-movable-asset tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-movable-asset tbody').click('input[type=checkbox]', function () {
        $('#tbl-movable-asset input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = movableDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (movableDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('turn-over-limit');

                    $('#btn-update-movable-asset-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-movable-asset-dt').data('rowindex', rowData);
                    $('#btn-delete-movable-asset-dt').data('rowindex', arr);
                    $('#select-all-movable-asset').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-movable-asset tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('movable-asset');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('movable-asset');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('movable-asset');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-movable-asset').prop('checked', true);
        else
            $('#select-all-movable-asset').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidMovableAssetModal() {
        debugger;
        result = true;

        counter++;
        fileUploaderId = 'data-table-movable-file-uploader' + counter;
        fileId = "photo-Id" + counter;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        vehicleMakeId = $('#vehicle-make-id option:selected').val();
        vehicleMakeIdText = $('#vehicle-make-id option:selected').text();
        vehicleModelId = $('#vehicle-model-id option:selected').val();
        vehicleModelIdText = $('#vehicle-model-id option:selected').text(); vehicleletiant = $('#vehicle-variant-id option:selected').val();
        vehicleletiantText = $('#vehicle-variant-id option:selected').text();
        numberOfOwners = parseInt($('#number-of-owners-movable-asset').val());
        manufacturingYear = parseInt($('#manufacturing-year-movable-asset').val());
        dateOfPurchase = $('#date-of-purchase-movable-asset').val();
        registrationDate = $('#registration-date-movable-asset').val();
        registrationNumber = $('#registration-number-movable-asset').val();
        purchasePrice = parseFloat($('#purchase-price-movable-asset').val());
        currentMarketValue = parseFloat($('#current-market-value-movable-asset').val());
        ownershipPercentage = parseFloat($('#ownership-percentage-movable-asset').val());
        isOwnershipDeceased = $('#is-ownership-deceased-movable').is(':checked') ? true : false;
        hasAnyMortgage = $('#has-any-mortgage-movable').is(':checked') ? true : false;
        note = $('#note-movable-asset').val();
        prmKey = 0;
        filePath = $('#movable-file-uploader-image-preview').prop('src');
        fileUploader = $('#movable-file-uploader').get(0);
        fileCaption = $('#movable-file-caption').val();
        reasonForModification = $('#reason-for-modification-movable-asset').val().trim();

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#movable-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        //Set Default Value if Empty
        if (note === '') {
            $('#note-movable-asset').val('None');
            note = 'None';
        }

        if (fileCaption === '') {
            $('#movable-file-caption').val('None');
            fileCaption = 'None';
        }


        if (reasonForModification === '') {
            $('#reason-for-modification-movable-asset').val('None');
            reasonForModification = 'None';
        }

        if ($('#vehicle-make-id').prop('selectedIndex') < 1) {
            result = false;
            $('#vehicle-make-id-error').removeClass('d-none');
        }

        //vehicle model Id
        if ($('#vehicle-model-id').prop('selectedIndex') < 1) {
            result = false;
            $('#vehicle-model-id-error').removeClass('d-none');
        }

        //Vehicle Varient Id
        if ($('#vehicle-variant-id').prop('selectedIndex') < 1) {
            result = false;
            $('#vehicle-variant-id-error').removeClass('d-none');
        }

        // number of Owners
        if (isNaN(numberOfOwners) === false) {
            minimum = parseInt($('#number-of-owners-movable-asset').attr('min'));
            maximum = parseInt($('#number-of-owners-movable-asset').attr('max'));
            if (parseInt(numberOfOwners) < parseInt(minimum) || parseInt(numberOfOwners) > parseInt(maximum)) {
                result = false;
                $('#number-of-owners-movable-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#number-of-owners-movable-asset-error').removeClass('d-none');
        }

        // Get the current year
        let currentYear = new Date().getFullYear();

        // Calculate the minimum allowed year
        let minAllowedYear = currentYear - 50;

        if (isNaN(manufacturingYear) === false) {
            if (parseInt(manufacturingYear) < parseInt(minAllowedYear) || parseInt(manufacturingYear) > parseInt(currentYear)) {
                result = false;
                $('#manufacturing-year-movable-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#manufacturing-year-movable-asset-error').removeClass('d-none');
        }

        //date of purchase
        let isValidDateOfPurchase = IsValidInputDate('#date-of-purchase-movable-asset');

        if (isValidDateOfPurchase === false) {
            result = false;
            $('#date-of-purchase-movable-asset-error').removeClass('d-none');
        }

        let isValidRegistrationDate = IsValidInputDate('#registration-date-movable-asset');

        if (isValidRegistrationDate === false) {
            result = false;
            $('#registration-date-movable-asset-error').removeClass('d-none');
        }

        IsValidRegistrationNumber();

        //purchase price
        if (isNaN(purchasePrice) === false) {
            minimum = parseFloat($('#purchase-price-movable-asset').attr('min'));
            maximum = parseFloat($('#purchase-price-movable-asset').attr('max'));

            if (parseFloat(purchasePrice) < parseFloat(minimum) || parseFloat(purchasePrice) > parseFloat(maximum)) {
                result = false;
                $('#purchase-price-movable-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#purchase-price-movable-asset-error').removeClass('d-none');
        }

        //current Market Value
        if (isNaN(currentMarketValue) === false) {
            minimum = parseFloat($('#current-market-value-movable-asset').attr('min'));
            maximum = parseFloat($('#current-market-value-movable-asset').attr('max'));

            if (parseFloat(currentMarketValue) < parseFloat(minimum) || parseFloat(currentMarketValue) > parseFloat(maximum)) {
                result = false;
                $('#current-market-value-movable-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#current-market-value-movable-asset-error').removeClass('d-none');
        }

        //ownership percentage
        if (isNaN(ownershipPercentage) === false) {
            minimum = parseFloat($('#ownership-percentage-movable-asset').attr('min'));
            maximum = parseFloat($('#ownership-percentage-movable-asset').attr('max'));

            if (parseFloat(ownershipPercentage) < parseFloat(minimum) || parseFloat(ownershipPercentage) > parseFloat(maximum)) {
                result = false;
                $('#ownership-percentage-movable-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#ownership-percentage-movable-asset-error').removeClass('d-none');
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.MovableAssetDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#movable-file-uploader-error').removeClass('d-none');
                }
            }
            else {
                let photoSrc = $('#movable-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
        }

        if (isNaN(fileCaption.length) === false) {
            maximumLength = parseInt($('#movable-file-caption').attr('maxlength'));

            if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
                result = false;
                $('#movable-file-caption-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#movable-file-caption-error').removeClass('d-none');
        }


        if (result) {
            $('#movable-asset-data-table-error').addClass('d-none');
        }
        else {
            $('#movable-asset-data-table-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsMovableAssetDataTable() {
        movableDataTable.column(1).visible(false);
        movableDataTable.column(3).visible(false);
        movableDataTable.column(5).visible(false);
        movableDataTable.column(22).visible(false);
        movableDataTable.column(23).visible(false);
        movableDataTable.column(24).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Validation(Event Handling) Code - Immovable Asset @@@@@@@@@@@@@@@@@@@@@@@@@@@


    //Carpet Area Validation
    $('#construction-area').focusout(function () {
        debugger;
        $('#carpet-area').attr('max', $(this).val());

        let constructionArea = parseFloat($('#construction-area').val());
        let carpetArea = parseFloat($('#carpet-area').val());

        if (isNaN(constructionArea) === false) {
            if (!isNaN(carpetArea) && carpetArea < constructionArea) {
                $('#carpet-area').val(carpetArea);
            } else {
                $('#carpet-area').val('');
            }
        } else {
            $('#carpet-area').val(''); // Clear monthly Interest is empty
        }
    });


    /// @@@@@@@@@@@@@@@@@@@@@@  Person Immovable Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-immovable-asset-dt').click(function () {
        event.preventDefault();
        isDbRecord = false;
        SetModalTitle('immovable-asset', 'Add');

        personImmovableAssetDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';

    });

    // DataTable Edit Button 
    $('#btn-edit-immovable-asset-dt').click(function () {

        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('immovable-asset', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {

            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#immovable-asset-modal').modal();

            columnValues = $('#btn-edit-immovable-asset-dt').data('rowindex');

            $('#survey-numbers', myModal).val(columnValues[1]);
            $('#city-survey-numbers', myModal).val(columnValues[2]);
            $('#number', myModal).val(columnValues[3]);
            $('#area-of-land-immovable', myModal).val(columnValues[4]);
            $('#construction-area', myModal).val(columnValues[5]);
            $('#carpet-area').attr('max', columnValues[5]);

            $('#carpet-area', myModal).val(columnValues[6]);
            $('#current-market-value-immovable', myModal).val(columnValues[7]);
            $('#annual-rent-income', myModal).val(columnValues[8]);
            $('#residence-types-id', myModal).val(columnValues[9]);
            $('#ownership-types-id', myModal).val(columnValues[11]);
            $('#ownership-percentage-immovable-asset', myModal).val(columnValues[13]);

            $('#is-constructed', myModal).prop('checked', columnValues[14].toString().toLowerCase() === 'true' ? true : false);

            $('#has-any-mortgage-immovable', myModal).prop('checked', columnValues[15].toString().toLowerCase() === 'true' ? true : false);

            $('#is-ownership-deceased-immovable', myModal).prop('checked', columnValues[16].toString().toLowerCase() === 'true' ? true : false);

            $('#immovable-file-caption', myModal).val(columnValues[19]);

            $('#asset-full-description', myModal).val(columnValues[20]);

            $('#note-immovable-asset', myModal).val(columnValues[21]);

            $('#reason-for-modification-immovable-asset', myModal).val(columnValues[22]);

            fileUploader = $('#' + $(columnValues[17]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'immovable-file-uploader';

            // columnValues[17] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[17]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[17]).attr('class') === 'db-record' ? true : false;

            // columnValues[18] - Image Tag Html
            filePath = $('#' + $(columnValues[18]).attr('id')).attr('src');

            fileNameDocument = columnValues[23];
            personImmovableAssetDocumentPrmKey = columnValues[24];
            localStoragePath = columnValues[25];

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                AttachFileUploader();
            }

            $('#immovable-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#immovable-asset-modal').modal('show');
        }
        else {
            $('#btn-edit-immovable-asset-edit-dt').addClass('read-only');
            $('#immovable-asset-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-immovable-asset-modal').click(function (event) {
        debugger;
        if (IsValidImmovableModal()) {
            row = immovableDataTable.row.add([
                tag,
                surveyNumber,
                citySurveyNumber,
                number,
                areaOfLand,
                constructionArea,
                carpetArea,
                currentMarketValue,
                annualRentIncome,
                residenceType,
                residenceTypeText,
                ownershipType,
                ownershipTypeText,
                ownershipPercentage,
                isConstructed,
                hasAnyMortgage,
                isOwnershipDeceased,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                assetFullDescription,
                note,
                reasonForModification,
                fileNameDocument,
                personImmovableAssetDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);
            $('#immovable-asset-data-table-error').addClass('d-none');

            HideColumnsImmovableDataTable();

            immovableDataTable.columns.adjust().draw();

            $('#immovable-asset-modal').modal('hide');

            EnableNewOperation('immovable-asset');
        }
    });

    // Modal update Button Event
    $('#btn-update-immovable-asset-modal').click(function (event) {
        $('#select-all-immovable-asset').prop('checked', false);
        if (IsValidImmovableModal()) {
            immovableDataTable.row(selectedRowIndex).data([
                tag,
                surveyNumber,
                citySurveyNumber,
                number,
                areaOfLand,
                constructionArea,
                carpetArea,
                currentMarketValue,
                annualRentIncome,
                residenceType,
                residenceTypeText,
                ownershipType,
                ownershipTypeText,
                ownershipPercentage,
                isConstructed,
                hasAnyMortgage,
                isOwnershipDeceased,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                assetFullDescription,
                note,
                reasonForModification,
                fileNameDocument,
                personImmovableAssetDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideColumnsImmovableDataTable();

            immovableDataTable.columns.adjust().draw();

            $('#immovable-asset-modal').modal('hide');

            EnableNewOperation('immovable-asset');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-immovable-asset-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-immovable-asset tbody input[type="checkbox"]:checked').each(function () {
                    immovableDataTable.row($('#tbl-immovable-asset tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-immovable-asset-dt').data('rowindex');
                    EnableNewOperation('immovable-asset');

                    $('#select-all-immovable-asset').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#select-all-immovable-asset').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-immovable-asset tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = immovableDataTable.row(row).index();

                rowData = (immovableDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-immovable-asset-dt').data('rowindex', arr);
                EnableDeleteOperation('immovable-asset');
            });
        }
        else {
            EnableNewOperation('immovable-asset');

            $('#tbl-immovable-asset tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-immovable-asset tbody').click('input[type=checkbox]', function () {
        $('#tbl-immovable-asset input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = immovableDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (immovableDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('immovable-asset');

                    $('#btn-update-immovable-asset-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-immovable-asset-dt').data('rowindex', rowData);
                    $('#btn-delete-immovable-asset-dt').data('rowindex', arr);
                    $('#select-all-immovable-asset').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-immovable-asset tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('immovable-asset');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('immovable-asset');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('immovable-asset');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-immovable-asset').prop('checked', true);
        else
            $('#select-all-immovable-asset').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidImmovableModal() {
        result = true;

        counter++;
        fileUploaderId = 'data-table-immovable-file-uploader' + counter;
        fileId = "photo-Id" + counter;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        surveyNumber = $('#survey-numbers').val();
        citySurveyNumber = $('#city-survey-numbers').val();
        number = $('#number').val().trim();
        areaOfLand = parseFloat($('#area-of-land-immovable').val());
        constructionArea = parseFloat($('#construction-area').val());
        carpetArea = parseFloat($('#carpet-area').val());
        currentMarketValue = parseFloat($('#current-market-value-immovable').val());
        annualRentIncome = parseFloat($('#annual-rent-income').val());
        residenceType = $('#residence-types-id option:selected').val();
        residenceTypeText = $('#residence-types-id option:selected').text();
        ownershipType = $('#ownership-types-id option:selected').val();
        ownershipTypeText = $('#ownership-types-id option:selected').text();
        ownershipPercentage = parseFloat($('#ownership-percentage-immovable-asset').val());
        isConstructed = $('#is-constructed').is(':checked') ? true : false;
        hasAnyMortgage = $('#has-any-mortgage-immovable').is(':checked') ? true : false;
        isOwnershipDeceased = $('#is-ownership-deceased-immovable').is(':checked') ? true : false;
        assetFullDescription = $('#asset-full-description').val();
        note = $('#note-immovable-asset').val();
        reasonForModification = $('#reason-for-modification-immovable-asset').val();

        //filePath = $('#immovable-file-uploader-image-preview').prop('src');
        //fileUploader = $('#immovable-file-uploader').get(0);
        //fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
        //imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        fileCaption = $('#immovable-file-caption').val();

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#immovable-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';

            //fileNameDocument = 'None';
            //localStoragePath = 'None';
        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        fileUploader = $('#immovable-file-uploader').get(0);

        //Set Default Value if Empty
        if (note === '') {
            $('#note-immovable-asset').val('None');
            note = 'None';
        }

        if (assetFullDescription === '') {
            $('#asset-full-description').val('None');
            assetFullDescription = 'None';
        }

        if (fileCaption === '') {
            $('#immovable-file-caption').val('None');
            fileCaption = 'None';
        }

        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.ImmovableAssetDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#immovable-file-uploader-error').removeClass('d-none');
                }
            }
            else {
                let photoSrc = $('#immovable-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
        }

        //survey Number
        if (isNaN(surveyNumber.length) === false) {

            minimumLength = parseInt($('#survey-numbers').attr('minlength'));
            maximumLength = parseInt($('#survey-numbers').attr('maxlength'));

            if (parseInt(surveyNumber.length) < parseInt(minimumLength) || parseInt(surveyNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#survey-numbers-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#survey-numbers-error').removeClass('d-none');
        }

        //city Survey Number
        if (isNaN(citySurveyNumber.length) === false) {

            minimumLength = parseInt($('#city-survey-numbers').attr('minlength'));
            maximumLength = parseInt($('#city-survey-numbers').attr('maxlength'));

            if (parseInt(citySurveyNumber.length) < parseInt(minimumLength) || parseInt(citySurveyNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#city-survey-numbers-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#city-survey-numbers-error').removeClass('d-none');
        }

        // Number
        if (isNaN(number.length) === false) {

            minimumLength = parseInt($('#number').attr('minlength'));
            maximumLength = parseInt($('#number').attr('maxlength'));

            if (parseInt(number.length) < parseInt(minimumLength) || parseInt(number.length) > parseInt(maximumLength)) {
                result = false;
                $('#number-error').removeClass('d-none');
            }

        }
        else {
            result = false;
            $('#number-error').removeClass('d-none');
        }

        // area Of Land
        if (isNaN(areaOfLand) === false) {
            minimum = parseFloat($('#area-of-land-immovable').attr('min'));
            maximum = parseFloat($('#area-of-land-immovable').attr('max'));

            if (parseFloat(areaOfLand) < parseFloat(minimum) || parseFloat(areaOfLand) > parseFloat(maximum)) {
                result = false;
                $('#area-of-land-immovable-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#area-of-land-immovable-error').removeClass('d-none');
        }

        // construction Area
        if (isNaN(constructionArea) === false) {
            minimum = parseFloat($('#construction-area').attr('min'));
            maximum = parseFloat($('#construction-area').attr('max'));

            if (parseFloat(constructionArea) < parseFloat(minimum) || parseFloat(constructionArea) > parseFloat(maximum)) {
                result = false;
                $('#construction-area-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#construction-area-error').removeClass('d-none');
        }

        // carpet Area
        if (isNaN(carpetArea) === false) {
            minimum = parseFloat($('#carpet-area').attr('min'));
            maximum = parseFloat($('#carpet-area').attr('max'));

            if (parseFloat(carpetArea) < parseFloat(minimum) || parseFloat(carpetArea) > parseFloat(maximum)) {
                result = false;
                $('#carpet-area-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#carpet-area-error').removeClass('d-none');
        }

        // current Market Value
        if (isNaN(currentMarketValue) === false) {
            minimum = parseFloat($('#current-market-value-immovable').attr('min'));
            maximum = parseFloat($('#current-market-value-immovable').attr('max'));

            if (parseFloat(currentMarketValue) < parseFloat(minimum) || parseFloat(currentMarketValue) > parseFloat(maximum)) {
                result = false;
                $('#current-market-value-immovable-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#current-market-value-immovable-error').removeClass('d-none');
        }

        // annual Rent Income
        if (isNaN(annualRentIncome) === false) {
            minimum = parseFloat($('#annual-rent-income').attr('min'));
            maximum = parseFloat($('#annual-rent-income').attr('max'));

            if (parseFloat(annualRentIncome) < parseFloat(minimum) || parseFloat(annualRentIncome) > parseFloat(maximum)) {
                result = false;
                $('#annual-rent-income-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#annual-rent-income-error').removeClass('d-none');
        }

        //residence Type
        if ($('#residence-types-id').prop('selectedIndex') < 1) {
            result = false;
            $('#residence-types-id-error').removeClass('d-none');
        }

        //ownership Type
        if ($('#ownership-types-id').prop('selectedIndex') < 1) {
            result = false;
            $('#ownership-types-id-error').removeClass('d-none');
        }

        // ownership Percentage
        if (isNaN(ownershipPercentage) === false) {
            minimum = parseFloat($('#ownership-percentage-immovable-asset').attr('min'));
            maximum = parseFloat($('#ownership-percentage-immovable-asset').attr('max'));

            if (parseFloat(ownershipPercentage) < parseFloat(minimum) || parseFloat(ownershipPercentage) > parseFloat(maximum)) {
                result = false;
                $('#ownership-percentage-immovable-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#ownership-percentage-immovable-asset-error').removeClass('d-none');
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.ImmovableAssetDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#immovable-file-uploader-error').removeClass('d-none');
                }

            }
            else {
                let photoSrc = $('#immovable-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
        }

        // file Caption
        if (isNaN(fileCaption.length) === false) {

            maximumLength = parseInt($('#immovable-file-caption').attr('maxlength'));

            if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
                result = false;
                $('#immovable-file-caption-error').removeClass('d-none');
            }
        } else {
            result = false;
            $('#immovable-file-caption-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsImmovableDataTable() {
        immovableDataTable.column(9).visible(false);
        immovableDataTable.column(11).visible(false);
        immovableDataTable.column(22).visible(false);
        immovableDataTable.column(23).visible(false);
        immovableDataTable.column(24).visible(false);
        immovableDataTable.column(25).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Validation(Event Handling) Code -Immovable Asset @@@@@@@@@@@@@@@@@@@@@@@@@@@


    //agriculture asset dropdown
    $('#agriculture-land-type-id').focusout(function () {
        let currentValue = $(this).val();

        if (currentValue !== lastSelectedValue) {
            $('#agriculture-land-description').val('');
            $('#survey-number').val('');
            $('#group-number').val('');
            $('#start-area-of-land').val('');
            $('#volume').val('');
            $('#agriculture-ownership-type-id').val('');
            $('#ownership-percentage-agriculture-asset').val('');
            $('#end-current-market-value').val('');
            $('#annual-income-from-land').val('');
            $('#enable-any-court-case').prop('checked', false);
            $('#any-court-case-block').addClass('d-none');
            $('#court-case-full-details').val('');
            $('#is-only-rain-fed-type-irrigation').prop('checked', false);
            $('#has-canal-river-irrigation-source').prop('checked', false);
            $('#has-wells-irrigation-source').prop('checked', false);
            $('#has-farm-lake-source').prop('checked', false);
            $('#has-any-mortgage').prop('checked', false);
            $('#is-ownership-deceased').prop('checked', false);
            $('#photo-path-agree').val('');
            $('#photo-path-agree-image-preview').attr('src', '');
            $('#file-caption').val('');
            $('#note-agriculture-asset').val('');
            $('#agriculture-file-uploader').val('');
            $('#agriculture-file-uploader-image-preview').attr('src', '');

        }

        lastSelectedValue = currentValue;
    });

    $('#enable-any-court-case').change(function () {
        if ($(this).is(':checked', true)) {
            $('#court-case-full-details-error').addClass('d-none');
        }
    });



    /// @@@@@@@@@@@@@@@@@@@@@@  Person Agriculture Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-agriculture-asset-dt').click(function () {
        event.preventDefault();
        isDbRecord = false;
        // For Court Case
        SetToggleSwitchBasedAccordions();

        lastSelectedValue = '';

        personAgricultureAssetDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';

        SetModalTitle('agriculture-asset', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-agriculture-asset-dt').click(function () {
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('agriculture-asset', 'Edit');

        if (hasAnyCourtCase === true) {
            $('#any-court-case-block').removeClass('d-none');
        } else {
            $('#any-court-case-block').addClass('d-none');
        }

        isChecked = $('.checks').is(':checked');

        if (isChecked) {

            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#agriculture-asset-modal').modal();

            columnValues = $('#btn-edit-agriculture-asset-dt').data('rowindex');
            lastSelectedValue = columnValues[1];

            $('#agriculture-land-type-id', myModal).val(columnValues[1]);
            $('#agriculture-land-description', myModal).val(columnValues[3]);
            $('#survey-number', myModal).val(columnValues[4]);
            $('#group-number', myModal).val(columnValues[5]);
            $('#start-area-of-land', myModal).val(columnValues[6]);
            $('#volume', myModal).val(columnValues[7]);
            $('#agriculture-ownership-type-id', myModal).val(columnValues[8]);
            $('#ownership-percentage-agriculture-asset', myModal).val(columnValues[10]);
            $('#end-current-market-value', myModal).val(columnValues[11]);
            $('#annual-income-from-land', myModal).val(columnValues[12]);

            $('#enable-any-court-case', myModal).prop('checked', columnValues[13].toString().toLowerCase() === 'true' ? true : false);

            $('#court-case-full-details', myModal).val(columnValues[14]);

            $('#is-only-rain-fed-type-irrigation', myModal).prop('checked', columnValues[15].toString().toLowerCase() === 'true' ? true : false);

            $('#has-canal-river-irrigation-source', myModal).prop('checked', columnValues[16].toString().toLowerCase() === 'true' ? true : false);

            $('#has-wells-irrigation-source', myModal).prop('checked', columnValues[17].toString().toLowerCase() === 'true' ? true : false);

            $('#has-farm-lake-source', myModal).prop('checked', columnValues[18].toString().toLowerCase() === 'true' ? true : false);

            $('#has-any-mortgage', myModal).prop('checked', columnValues[19].toString().toLowerCase() === 'true' ? true : false);

            $('#is-ownership-deceased', myModal).prop('checked', columnValues[20].toString().toLowerCase() === 'true' ? true : false);

            $('#agriculture-file-caption', myModal).val(columnValues[23]);

            $('#note-agriculture-asset', myModal).val(columnValues[24]);

            $('#reason-for-modification-agriculture-asset', myModal).val(columnValues[25]);

            fileUploader = $('#' + $(columnValues[21]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'agriculture-file-uploader';

            // columnValues[21] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[21]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[21]).attr('class') === 'db-record' ? true : false;

            // columnValues[22] - Image Tag Html
            filePath = $('#' + $(columnValues[22]).attr('id')).attr('src');

            fileNameDocument = columnValues[26];
            personAgricultureAssetDocumentPrmKey = columnValues[27];
            localStoragePath = columnValues[28];

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                AttachFileUploader();
            }

            $('#agriculture-file-uploader-image-preview').attr('src', filePath);


            // Show Modals
            $('#agriculture-asset-modal').modal('show');
        }
        else {
            $('#btn-edit-agriculture-asset-edit-dt').addClass('read-only');
            $('#agriculture-asset-modal').modal('hide');
        }

        // For Court Case
        SetToggleSwitchBasedAccordions();
    });

    // Modal Add Button Event
    $('#btn-add-agriculture-asset-modal').click(function (event) {
        if (IsValidAgricultureModal()) {
            row = agricultureDataTable.row.add([
                tag,
                agricultureLandTypeId,
                agricultureLandTypeText,
                agricultureLandDescription,
                surveyNumber,
                groupNumber,
                areaOfLand,
                volume,
                ownershipTypeId,
                ownershipTypeText,
                ownershipPercentage,
                currentMarketValue,
                annualIncomeFromLand,
                hasAnyCourtCase,
                courtCaseFullDetails,
                isOnlyRainFedTypeIrrigation,
                hasCanalRiverIrrigationSource,
                hasWellsIrrigationSource,
                hasFarmLakeSource,
                hasAnyMortgage,
                isOwnershipDeceased,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                personAgricultureAssetDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);
            $('#agriculture-asset-data-table-error').addClass('d-none');
            HideColumnsAgricultureDataTable();

            agricultureDataTable.columns.adjust().draw();

            $('#agriculture-asset-modal').modal('hide');

            EnableNewOperation('agriculture-asset');
        }
    });

    // Modal update Button Event
    $('#btn-update-agriculture-asset-modal').click(function (event) {
        $('#select-all-agriculture-asset').prop('checked', false);
        if (IsValidAgricultureModal()) {
            agricultureDataTable.row(selectedRowIndex).data([
                tag,
                agricultureLandTypeId,
                agricultureLandTypeText,
                agricultureLandDescription,
                surveyNumber,
                groupNumber,
                areaOfLand,
                volume,
                ownershipTypeId,
                ownershipTypeText,
                ownershipPercentage,
                currentMarketValue,
                annualIncomeFromLand,
                hasAnyCourtCase,
                courtCaseFullDetails,
                isOnlyRainFedTypeIrrigation,
                hasCanalRiverIrrigationSource,
                hasWellsIrrigationSource,
                hasFarmLakeSource,
                hasAnyMortgage,
                isOwnershipDeceased,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                personAgricultureAssetDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideColumnsAgricultureDataTable();

            agricultureDataTable.columns.adjust().draw();

            $('#agriculture-asset-modal').modal('hide');

            EnableNewOperation('agriculture-asset');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-agriculture-asset-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-agriculture-asset tbody input[type="checkbox"]:checked').each(function () {
                    agricultureDataTable.row($('#tbl-agriculture-asset tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-agriculture-asset-dt').data('rowindex');
                    EnableNewOperation('agriculture-asset');

                    $('#select-all-agriculture-asset').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-agriculture-asset').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-agriculture-asset tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = agricultureDataTable.row(row).index();

                rowData = (agricultureDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-agriculture-asset-dt').data('rowindex', arr);
                EnableDeleteOperation('agriculture-asset');
            });
        }
        else {
            EnableNewOperation('agriculture-asset');

            $('#tbl-agriculture-asset tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-agriculture-asset tbody').click('input[type=checkbox]', function () {
        $('#tbl-agriculture-asset input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = agricultureDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (agricultureDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('agriculture-asset');

                    $('#btn-update-agriculture-asset-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-agriculture-asset-dt').data('rowindex', rowData);
                    $('#btn-delete-agriculture-asset-dt').data('rowindex', arr);
                    $('#select-all-agriculture-asset').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-agriculture-asset tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('agriculture-asset');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('agriculture-asset');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('agriculture-asset');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-agriculture-asset').prop('checked', true);
        else
            $('#select-all-agriculture-asset').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidAgricultureModal() {
        result = true;
        counter++;
        fileUploaderId = 'data-table-agriculture-file-uploader' + counter;

        fileId = "photo-Id" + counter;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        agricultureLandTypeId = $('#agriculture-land-type-id option:selected').val();
        agricultureLandTypeText = $('#agriculture-land-type-id option:selected').text();
        agricultureLandDescription = $('#agriculture-land-description').val();
        surveyNumber = $('#survey-number').val();
        groupNumber = $('#group-number').val();
        areaOfLand = parseInt($('#start-area-of-land').val());
        volume = parseInt($('#volume').val());
        ownershipTypeId = $('#agriculture-ownership-type-id option:selected').val();
        ownershipTypeText = $('#agriculture-ownership-type-id option:selected').text();
        ownershipPercentage = parseFloat($('#ownership-percentage-agriculture-asset').val());
        currentMarketValue = parseFloat($('#end-current-market-value').val());
        annualIncomeFromLand = parseFloat($('#annual-income-from-land').val());
        hasAnyCourtCase = $('#enable-any-court-case').is(':checked') ? true : false;
        courtCaseFullDetails = $('#court-case-full-details').val();
        isOnlyRainFedTypeIrrigation = $('#is-only-rain-fed-type-irrigation').is(':checked') ? true : false;
        hasCanalRiverIrrigationSource = $('#has-canal-river-irrigation-source').is(':checked') ? true : false;
        hasWellsIrrigationSource = $('#has-wells-irrigation-source').is(':checked') ? true : false;
        hasFarmLakeSource = $('#has-farm-lake-source').is(':checked') ? true : false;
        hasAnyMortgage = $('#has-any-mortgage').is(':checked') ? true : false;
        isOwnershipDeceased = $('#is-ownership-deceased').is(':checked') ? true : false;
        note = $('#note-agriculture-asset').val();
        reasonForModification = $('#reason-for-modification-agriculture-asset').val();
        prmKey = 0;
        //filePath = $('#agriculture-file-uploader-image-preview').prop('src');
        //fileUploader = $('#agriculture-file-uploader').get(0);
        //fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
        //imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        fileCaption = $('#agriculture-file-caption').val();

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#agriculture-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';

            //fileNameDocument = 'None';
            //localStoragePath = 'None';
        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        fileUploader = $('#agriculture-file-uploader').get(0);

        //Set Default Value if Empty
        if (note === '') {
            $('#note-agriculture-asset').val('None');
            note = 'None';
        }

        if (fileCaption === '') {
            $('#agriculture-file-caption').val('None');
            fileCaption = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-agriculture-asset').val('None');
            reasonForModification = 'None';
        }

        //agriculture LandType Id
        if ($('#agriculture-land-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#agriculture-land-type-id-error').removeClass('d-none');
        }

        //agriculture Land Description
        if (isNaN(agricultureLandDescription.length) === false) {

            minimumLength = parseInt($('#agriculture-land-description').attr('minlength'));
            maximumLength = parseInt($('#agriculture-land-description').attr('maxlength'));

            if (parseInt(agricultureLandDescription.length) < parseInt(minimumLength) || parseInt(agricultureLandDescription.length) > parseInt(maximumLength)) {
                result = false;
                $('#agriculture-land-description-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#agriculture-land-description-error').removeClass('d-none');
        }

        if (isNaN(surveyNumber.length) === false) {
            //survey Number
            minimumLength = parseInt($('#survey-number').attr('minlength'));
            maximumLength = parseInt($('#survey-number').attr('maxlength'));

            if (parseInt(surveyNumber.length) < parseInt(minimumLength) || parseInt(surveyNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#survey-number-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#survey-number-error').removeClass('d-none');
        }

        if (isNaN(groupNumber.length) === false) {

            //groupNumber
            minimumLength = parseInt($('#group-number').attr('minlength'));
            maximumLength = parseInt($('#group-number').attr('maxlength'));

            if (parseInt(groupNumber.length) < parseInt(minimumLength) || parseInt(groupNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#group-number-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#group-number-error').removeClass('d-none');
        }

        //area Of Land
        if (isNaN(areaOfLand) === false) {

            minimum = parseInt($('#start-area-of-land').attr('min'));
            maximum = parseInt($('#start-area-of-land').attr('max'));

            if (parseInt(areaOfLand) < parseInt(minimum) || parseInt(areaOfLand) > parseInt(maximum)) {
                result = false;
                $('#start-area-of-land-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#start-area-of-land-error').removeClass('d-none');
        }

        //volume
        if (isNaN(volume) === false) {

            minimum = parseInt($('#volume').attr('min'));
            maximum = parseInt($('#volume').attr('max'));

            if (parseInt(volume) < parseInt(minimum) || parseInt(volume) > parseInt(maximum)) {
                result = false;
                $('#volume-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#volume-error').removeClass('d-none');
        }

        //ownership type id
        if ($('#agriculture-ownership-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#agriculture-ownership-type-id-error').removeClass('d-none');
        }

        //ownershipPercentage
        if (isNaN(ownershipPercentage) === false) {

            minimum = parseFloat($('#ownership-percentage-agriculture-asset').attr('min'));
            maximum = parseFloat($('#ownership-percentage-agriculture-asset').attr('max'));

            if (parseFloat(ownershipPercentage) < parseFloat(minimum) || parseFloat(ownershipPercentage) > parseFloat(maximum)) {
                result = false;
                $('#ownership-percentage-agriculture-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#ownership-percentage-agriculture-asset-error').removeClass('d-none');
        }

        //currentMarketValue
        if (isNaN(currentMarketValue) === false) {

            minimum = parseFloat($('#end-current-market-value').attr('min'));
            maximum = parseFloat($('#end-current-market-value').attr('max'));

            if (parseFloat(currentMarketValue) < parseFloat(minimum) || parseFloat(currentMarketValue) > parseFloat(maximum)) {
                result = false;
                $('#end-current-market-value-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#end-current-market-value-error').removeClass('d-none');
        }

        //annualIncomeFromLand
        if (isNaN(annualIncomeFromLand) === false) {

            minimum = parseFloat($('#annual-income-from-land').attr('min'));
            maximum = parseFloat($('#annual-income-from-land').attr('max'));

            if (parseFloat(annualIncomeFromLand) < parseFloat(minimum) || parseFloat(annualIncomeFromLand) > parseFloat(maximum)) {
                result = false;
                $('#annual-income-from-land-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#annual-income-from-land-error').removeClass('d-none');
        }

        //has Any CourtCase
        if (hasAnyCourtCase === true) {

            if (isNaN(courtCaseFullDetails.length) === false) {

                maximumLength = parseInt($('#court-case-full-details').attr('maxlength'));

                if (parseInt(courtCaseFullDetails === 0) || parseInt(courtCaseFullDetails.length) > parseInt(maximumLength)) {
                    result = false;
                    $('#court-case-full-details-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#court-case-full-details-error').removeClass('d-none');
            }
        }
        else if (hasAnyCourtCase === false) {
            courtCaseFullDetails = 'None';
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.AgricultureAssetDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#agriculture-file-uploader-error').removeClass('d-none');
                }
            }
            else {
                let photoSrc = $('#agriculture-file-uploader-image-preview').attr('src');
                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
        }

        // file Caption
        maximumLength = parseInt($('#agriculture-file-caption').attr('maxlength'));

        if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#agriculture-file-caption-agriculture-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsAgricultureDataTable() {
        agricultureDataTable.column(1).visible(false);
        agricultureDataTable.column(8).visible(false);
        agricultureDataTable.column(25).visible(false);
        agricultureDataTable.column(26).visible(false);
        agricultureDataTable.column(27).visible(false);
        agricultureDataTable.column(28).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Validation(Event Handling) Code -Machinery Asset @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // Purchase Price 
    $('#purchase-price-machinery-asset').focusout(function () {
        let purchasePrice = parseFloat($('#purchase-price-machinery-asset').val());

        if (isNaN(purchasePrice) === true) {
            purchasePrice = 0;
        }

        $('#current-market-value-machinery-asset').attr('max', purchasePrice);
        $('#current-market-value-machinery-asset').val('');
    });

    function ManufacturingYearMachinaryAssetFocusOutEventFunction() {
        debugger;
        let today = new Date();

        // Get the manufacturing year from the input field
        let purchaseYear = parseInt($('#manufacturing-year-machinery-asset').val());
        let maxPurchaseYear = parseInt(purchaseYear) + 10;

        let minPurchaseDate = new Date(purchaseYear + '-01-01');
        let maxPurchaseDate = new Date(maxPurchaseYear + '-12-31');

        // If Max Purchase Year Is Larger Than Current Year
        if (parseInt(maxPurchaseYear) > parseInt(today.getFullYear())) {
            maxPurchaseYear = today.getFullYear();
        }

        // If Max Purchase Date Is Larger Than Today
        if (maxPurchaseDate > today) {
            maxPurchaseDate = today;
        }

        $('#date-of-purchase-machinery-asset').attr('min', GetInputDateFormat(minPurchaseDate));
        $('#date-of-purchase-machinery-asset').attr('max', GetInputDateFormat(maxPurchaseDate));
    }

    // Machinery Asset manufacturing-year-machinery-asset
    $('#manufacturing-year-machinery-asset').focusout(function () {
        ManufacturingYearMachinaryAssetFocusOutEventFunction();
        $('#date-of-purchase-machinery-asset').val('');
    });


    /// @@@@@@@@@@@@@@@@@@@@@@  Person Machinery Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-machinery-asset-dt').click(function () {
        event.preventDefault();
        isDbRecord = false;
        SetModalTitle('machinery-asset', 'Add');

        personMachineryAssetDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';
    });

    // DataTable Edit Button 
    $('#btn-edit-machinery-asset-dt').click(function () {
        debugger;

        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('machinery-asset', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {

            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#machinery-asset-modal').modal();


            columnValues = $('#btn-edit-machinery-asset-dt').data('rowindex');

            machineryDateOfPurchase = new Date(columnValues[4]);

            $('#current-market-value-machinery-asset').attr('max', columnValues[7]);

            $('#name-of-machinery', myModal).val(columnValues[1]);
            $('#machinery-full-details', myModal).val(columnValues[2]);
            $('#manufacturing-year-machinery-asset', myModal).val(columnValues[3]);
            $('#date-of-purchase-machinery-asset', myModal).val(GetInputDateFormat(machineryDateOfPurchase));
            $('#number-of-owners-machinery-asset', myModal).val(columnValues[5]);
            $('#reference-number-machinery-asset', myModal).val(columnValues[6]);
            $('#purchase-price-machinery-asset', myModal).val(columnValues[7]);
            $('#current-market-value-machinery-asset', myModal).val(columnValues[8]);
            $('#ownership-percentage-machinery-asset', myModal).val(columnValues[9]);

            $('#has-any-mortgage-machinery', myModal).prop('checked', columnValues[10].toString().toLowerCase() === 'true' ? true : false);

            $('#is-ownership-deceased-machinery', myModal).prop('checked', columnValues[11].toString().toLowerCase() === 'true' ? true : false);
            $('#machinery-file-caption', myModal).val(columnValues[14]);
            $('#note-machinery-asset', myModal).val(columnValues[15]);
            $('#reason-for-modification-machinery-asset', myModal).val(columnValues[16]);

            fileUploader = $('#' + $(columnValues[12]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'machinery-file-uploader';

            // columnValues[21] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[12]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[12]).attr('class') === 'db-record' ? true : false;

            // columnValues[22] - Image Tag Html
            filePath = $('#' + $(columnValues[13]).attr('id')).attr('src');


            fileNameDocument = columnValues[17];
            personMachineryAssetDocumentPrmKey = columnValues[18];
            localStoragePath = columnValues[19];


            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                AttachFileUploader();
            }

            ManufacturingYearMachinaryAssetFocusOutEventFunction();

            $('#machinery-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#machinery-asset-modal').modal('show');
        }
        else {
            $('#btn-edit-machinery-asset-edit-dt').addClass('read-only');
            $('#machinery-asset-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-machinery-asset-modal').click(function (event) {
        if (IsValidMachineryModal()) {
            debugger;
            row = machineryDataTable.row.add([
                tag,
                nameOfMachinery,
                machineryFullDetails,
                manufacturingYear,
                dateOfPurchase,
                numberOfOwners,
                referenceNumber,
                purchasePrice,
                currentMarketValue,
                ownershipPercentage,
                hasAnyMortgage,
                isOwnershipDeceased,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                personMachineryAssetDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);
            $('#machinery-asset-data-table-error').addClass('d-none');

            HideColumnsMachineryDataTable();

            machineryDataTable.columns.adjust().draw();


            $('#machinery-asset-modal').modal('hide');

            EnableNewOperation('machinery-asset');
        }
    });

    // Modal update Button Event
    $('#btn-update-machinery-asset-modal').click(function (event) {
        $('#select-all-machinery-asset').prop('checked', false);
        if (IsValidMachineryModal()) {
            machineryDataTable.row(selectedRowIndex).data([
                tag,
                nameOfMachinery,
                machineryFullDetails,
                manufacturingYear,
                dateOfPurchase,
                numberOfOwners,
                referenceNumber,
                purchasePrice,
                currentMarketValue,
                ownershipPercentage,
                hasAnyMortgage,
                isOwnershipDeceased,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                personMachineryAssetDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideColumnsMachineryDataTable();

            machineryDataTable.columns.adjust().draw();

            $('#machinery-asset-modal').modal('hide');

            EnableNewOperation('machinery-asset');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-machinery-asset-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-machinery-asset tbody input[type="checkbox"]:checked').each(function () {
                    machineryDataTable.row($('#tbl-machinery-asset tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-machinery-asset-dt').data('rowindex');
                    EnableNewOperation('machinery-asset');

                    $('#select-all-machinery-asset').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-machinery-asset').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-machinery-asset tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = machineryDataTable.row(row).index();

                rowData = (machineryDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-machinery-asset-dt').data('rowindex', arr);
                EnableDeleteOperation('machinery-asset');
            });
        }
        else {
            EnableNewOperation('machinery-asset');

            $('#tbl-machinery-asset tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-machinery-asset tbody').click('input[type=checkbox]', function () {
        $('#tbl-machinery-asset input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = machineryDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (machineryDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('machinery-asset');

                    $('#btn-update-machinery-asset-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-machinery-asset-dt').data('rowindex', rowData);
                    $('#btn-delete-machinery-asset-dt').data('rowindex', arr);
                    $('#select-all-machinery-asset').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-machinery-asset tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('machinery-asset');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('machinery-asset');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('machinery-asset');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-machinery-asset').prop('checked', true);
        else
            $('#select-all-machinery-asset').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidMachineryModal() {
        debugger;
        result = true;
        counter++;
        fileUploaderId = 'data-table-machinery-file-uploader' + counter;
        fileId = "photo-Id" + counter;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        nameOfMachinery = $('#name-of-machinery').val();
        machineryFullDetails = $('#machinery-full-details').val();
        manufacturingYear = parseInt($('#manufacturing-year-machinery-asset').val());
        dateOfPurchase = $('#date-of-purchase-machinery-asset').val();
        numberOfOwners = parseInt($('#number-of-owners-machinery-asset').val());
        referenceNumber = $('#reference-number-machinery-asset').val();
        purchasePrice = parseFloat($('#purchase-price-machinery-asset').val());
        currentMarketValue = parseFloat($('#current-market-value-machinery-asset').val());
        ownershipPercentage = parseFloat($('#ownership-percentage-machinery-asset').val());
        hasAnyMortgage = $('#has-any-mortgage-machinery').is(':checked') ? true : false;
        isOwnershipDeceased = $('#is-ownership-deceased-machinery').is(':checked') ? true : false;
        note = $('#note-machinery-asset').val();
        reasonForModification = $('#reason-for-modification-machinery-asset').val();
        fileCaption = $('#machinery-file-caption').val();

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#machinery-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';

        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }
        fileUploader = $('#machinery-file-uploader').get(0);

        //Set Default Value if Empty
        if (note === '') {
            $('#note-machinery-asset').val('None');
            note = 'None';
        }

        if (fileCaption === '') {
            $('#machinery-file-caption').val('None');
            fileCaption = 'None';
        }

        //name Of Machinery
        minimumLength = parseInt($('#name-of-machinery').attr('minlength'));
        maximumLength = parseInt($('#name-of-machinery').attr('maxlength'));
        if (parseInt(nameOfMachinery.length) < parseInt(minimumLength) || parseInt(nameOfMachinery.length) > parseInt(maximumLength)) {
            result = false;
            $('#name-of-machinery-error').removeClass('d-none');
        }

        //machinery Full Details
        maximumLength = parseInt($('#machinery-full-details').attr('maxlength'));
        if (parseInt(machineryFullDetails.length) === 0 || parseInt(machineryFullDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#machinery-full-details-error').removeClass('d-none');
        }

        // Get the current year
        let currentYear = new Date().getFullYear();

        // Calculate the minimum allowed year
        let minAllowedYear = currentYear - 50;

        if (isNaN(manufacturingYear) === false) {
            if (parseInt(manufacturingYear) < parseInt(minAllowedYear) || parseInt(manufacturingYear) > parseInt(currentYear)) {
                result = false;
                $('#manufacturing-year-machinery-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#manufacturing-year-machinery-asset-error').removeClass('d-none');
        }

        //date of purchase
        if (IsValidInputDate('#date-of-purchase-machinery-asset') === false) {
            result = false;
            $('#date-of-purchase-machinery-asset-error').removeClass('d-none');
        }
        else {
            $('#date-of-purchase-machinery-asset-error').addClass('d-none');
        }

        //number Of Owners
        if (isNaN(numberOfOwners) === false) {
            minimum = parseInt($('#number-of-owners-machinery-asset').attr('min'));
            maximum = parseInt($('#number-of-owners-machinery-asset').attr('max'));
            if (parseInt(numberOfOwners) < parseInt(minimum) || parseInt(numberOfOwners) > parseInt(maximum)) {
                result = false;
                $('#number-of-owners-machinery-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#number-of-owners-machinery-asset-error').removeClass('d-none');
        }

        //reference Number
        maximumLength = parseInt($('#reference-number-machinery-asset').attr('maxlength'));

        if (parseInt(referenceNumber.length) === 0 || parseInt(referenceNumber.length) > parseInt(maximumLength)) {
            result = false;
            $('#reference-number-machinery-asset-error').removeClass('d-none');
        }
        else {
            $('#reference-number-machinery-asset-error').addClass('d-none');
        }

        //purchase Price
        if (isNaN(purchasePrice) === false) {
            minimum = parseFloat($('#purchase-price-machinery-asset').attr('min'));
            maximum = parseFloat($('#purchase-price-machinery-asset').attr('max'));

            if (parseFloat(purchasePrice) < parseFloat(minimum) || parseFloat(purchasePrice) > parseFloat(maximum)) {
                result = false;
                $('#purchase-price-machinery-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#purchase-price-machinery-asset-error').removeClass('d-none');
        }

        //current Market Value
        if (isNaN(currentMarketValue) === false) {
            debugger
            let minimum = parseFloat($('#current-market-value-machinery-asset').attr('min'));
            let maximum = parseFloat($('#current-market-value-machinery-asset').attr('max'));


            if (parseFloat(currentMarketValue) < parseFloat(minimum) || parseFloat(currentMarketValue) > parseFloat(maximum)) {
                result = false;
                $('#current-market-value-machinery-asset-error').removeClass('d-none');
            }
        } else {
            result = false;
            $('#current-market-value-machinery-asset-error').removeClass('d-none');
        }

        //ownership Percentage
        if (isNaN(ownershipPercentage) === false) {
            minimum = parseFloat($('#ownership-percentage-machinery-asset').attr('min'));
            maximum = parseFloat($('#ownership-percentage-machinery-asset').attr('max'));

            if (parseFloat(ownershipPercentage) < parseFloat(minimum) || parseFloat(ownershipPercentage) > parseFloat(maximum)) {
                result = false;
                $('#ownership-percentage-machinery-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#ownership-percentage-machinery-asset-error').removeClass('d-none');
        }

        //filecaption
        maximumLength = parseInt($('#machinery-file-caption').attr('maxlength'));
        if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#machinery-file-caption-error').removeClass('d-none');
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.MachineryAssetDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#machinery-file-uploader-error').removeClass('d-none');
                }
            }
            else {
                let photoSrc = $('#machinery-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsMachineryDataTable() {
        machineryDataTable.column(16).visible(false);
        machineryDataTable.column(17).visible(false);
        machineryDataTable.column(18).visible(false);
        machineryDataTable.column(19).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Validation(Event Handling) Code -Additional Income Detail @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // Validation Income Soure Id
    $('#income-source-id').change(function () {
        debugger
        IncomeSourceChange();

    });

    // Function to handle the change event for the income source dropdown
    function IncomeSourceChange() {
        debugger;
        // Get the selected text from the dropdown
        incomeSourceText = $('#income-source-id option:selected').text();

        // Check if the selected text is 'Other Income'
        if (incomeSourceText === 'Other Income' || incomeSourceText === 'Other Income ---> इतर उत्पन्न') {
            $('#other-details-input').removeClass('d-none');
        } else {
            $('#other-details').val('None');
            $('#other-details-input').addClass('d-none');
            $('#other-details-error').addClass('d-none');
        }
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Person Additional Income Detail - Validations Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-income-detail-dt').click(function () {
        debugger
        event.preventDefault();
        isDbRecord = false;
        $('#other-details-input').addClass('d-none');

        SetModalTitle('income-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-income-detail-dt').click(function () {
        debugger
        SetModalTitle('income-detail', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {

            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#income-detail-modal').modal();

            columnValues = $('#btn-edit-income-detail-dt').data('rowindex');
            $('#income-source-id', myModal).val(columnValues[1]);
            $('#annual-incomes', myModal).val(columnValues[3]);
            $('#other-details', myModal).val(columnValues[4]);
            $('#note-income-detail', myModal).val(columnValues[5]);
            $('#reason-for-modification-income-detail', myModal).val(columnValues[6]);

            debugger
            // Check if the selected text is 'Other Income'
            if (columnValues[2] === 'Other Income' || columnValues[2] === 'Other Income ---> इतर उत्पन्न') {
                $('#other-details-input').removeClass('d-none');

            }
            else {
                $('#other-details-input').addClass('d-none');
                $('#other-details').val('None');
                $('#other-details-error').addClass('d-none');
            }

            // Show Modals
            $('#income-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-income-detail-edit-dt').addClass('read-only');
            $('#income-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-income-detail-modal').click(function (event) {

        if (IsValidIncomeDetailModal()) {
            row = incomeDatatable.row.add([
                tag,
                incomeSource,
                incomeSourceText,
                annualIncome,
                otherDetails,
                note,
                reasonForModification
            ]).draw();

            // Error Message In Span
            $('#income-details-data-table-error').addClass('d-none');

            HideColumnsIncomeDetailDatatable();

            incomeDatatable.columns.adjust().draw();

            $('#income-detail-modal').modal('hide');

            EnableNewOperation('income-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-income-detail-modal').click(function (event) {
        let b = $('#btn-edit-income-detail-dt').attr('rowindex');
        $('#select-all-income-detail').prop('checked', false);
        if (IsValidIncomeDetailModal()) {
            incomeDatatable.row(selectedRowIndex).data([
                tag,
                incomeSource,
                incomeSourceText,
                annualIncome,
                otherDetails,
                note,
                reasonForModification

            ]).draw();
            // Error Message In Span
            $('#income-detail-validation span').html('');

            HideColumnsIncomeDetailDatatable();

            incomeDatatable.columns.adjust().draw();

            $('#income-detail-modal').modal('hide');

            EnableNewOperation('income-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-income-detail-dt').click(function (event) {

        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-income-detail tbody input[type="checkbox"]:checked').each(function () {
                    incomeDatatable.row($('#tbl-income-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-income-detail-dt').data('rowindex');
                    EnableNewOperation('income-detail');

                    $('#select-all-income-detail').prop('checked', false);
                    //if (!incomeDatatable.data().any())
                    //$('#income-details-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -   
    $('#select-all-income-detail').click(function () {

        if ($(this).prop('checked')) {
            $('#tbl-income-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = incomeDatatable.row(row).index();

                rowData = (incomeDatatable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-income-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('income-detail');
            });
        }
        else {
            EnableNewOperation('income-detail');

            $('#tbl-income-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-income-detail tbody').click('input[type=checkbox]', function () {

        $('#tbl-income-detail input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = incomeDatatable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (incomeDatatable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('income-detail');

                    $('#btn-update-income-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-income-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-income-detail-dt').data('rowindex', arr);
                    $('#select-all-income-detail').data('rowindex', arr);
                }
            }
        });


        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-income-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('income-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('income-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('income-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-income-detail').prop('checked', true);
        else
            $('#select-all-income-detail').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidIncomeDetailModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        incomeSource = $('#income-source-id option:selected').val();
        incomeSourceText = $('#income-source-id option:selected').text();
        annualIncome = parseFloat($('#annual-incomes').val());
        otherDetails = $('#other-details').val();
        note = $('#note-income-detail').val();
        reasonForModification = $('#reason-for-modification-income-detail').val();

        //Set Default Value if Empty
        if (note === '') {
            $('#note-income-detail').val('None');
            note = 'None';
        }
        if (reasonForModification === '') {
            $('#reason-for-modification-income-detail').val('None');
            reasonForModification = 'None';
        }
        if ($('#other-details-input').hasClass('d-none')) {
            if (!otherDetails) { // This also checks for empty strings
                otherDetails = 'None';
            }
        }
        // Check if the selected text is 'Other Income'
        if (incomeSourceText === 'Other Income' || incomeSourceText === 'Other Income ---> इतर उत्पन्न') {
            $('#other-details-input').removeClass('d-none');
        } else {
            $('#other-details').val('None');
            $('#other-details-input').addClass('d-none');
            $('#other-details-error').addClass('d-none');
        }

        // DocumentTypeId
        if ($('#income-source-id').prop('selectedIndex') < 1) {
            result = false;
            $('#income-source-id-error').removeClass('d-none');
        }

        // Regular expression to match up to 18 digits before decimal and 2 digits after
        if (isNaN(annualIncome) === false) {
            minimum = parseFloat($('#annual-incomes').attr('min'));
            maximum = parseFloat($('#annual-incomes').attr('max'));

            if (parseFloat(annualIncome) < parseFloat(minimum) || parseFloat(annualIncome) > parseFloat(maximum)) {
                result = false;
                $('#annual-incomes-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#annual-incomes-error').removeClass('d-none');
        }

        //reference Number
        maximumLength = parseInt($('#other-details').attr('maxlength'));

        if (parseInt(otherDetails.length) === 0 || parseInt(otherDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#other-details-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsIncomeDetailDatatable() {
        incomeDatatable.column(1).visible(false);
        incomeDatatable.column(6).visible(false);

    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Validation(Event Handling) Code - Borrowing Details  @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // Update max attribute based on Mortgage Amount
    $('#mortgage-amount').focusout(function () {
        $('#sanction-loan-amount').attr('max', $(this).val());

        // Clear Value Sanction Loan Amount
        $('#sanction-loan-amount').val('');
    });

    // Update max attribute based on Sanction Price
    $('#sanction-loan-amount').focusout(function () {
        debugger;
        let sanctionAmount = parseFloat($('#sanction-loan-amount').val());
        if (isNaN(sanctionAmount) === true) {
            sanctionAmount = 0;
        }
        $('#installment-amount').attr('max', sanctionAmount);
        // Clear Value Of Installment  Amount
        $('#installment-amount').val('');
    });

    // Opening Date
    $('#activation-open-date').focusout(function () {
        $('#activation-filing-date').val('');
        $('#close-date-borrowing').val('');
        EnableTakingAnyCourtActionClickEventFunction();
    });

    // Filling Date
    $('#activation-filing-date').click(function () {
        let openingDate = new Date($('#activation-open-date').val());

        openingDate.setDate(openingDate.getDate() + 1);

        $('#activation-filing-date').attr('min', GetInputDateFormat(openingDate));

        $('#expiry-filing-date').val('');
    });

    // Registration Date
    $('#expiry-filing-date').click(function () {
        debugger;
        let filingDate = new Date($('#activation-filing-date').val());

        $('#expiry-filing-date').attr('min', GetInputDateFormat(filingDate));

        let maxRegistrationDate = new Date(filingDate.setMonth(filingDate.getMonth() + 1));

        if (maxRegistrationDate > new Date()) {
            maxRegistrationDate = new Date();
        }

        $('#expiry-filing-date').attr('max', GetInputDateFormat(maxRegistrationDate));
    });

    // Function to enable/disable based on close-date-borrowing value
    function EnableTakingAnyCourtActionClickEventFunction() {
        let closeDate = $('#close-date-borrowing').val();
        let today = new Date();
        let openDate = $('#activation-open-date').val();
        today = GetInputDateFormat(today);

        if (closeDate !== '' || openDate === today) {
            $('#taking-any-court-action-block').addClass('d-none');
            $('#enable-taking-any-court-action').prop('checked', false)
            $('#enable-taking-any-court-action').prop('disabled', true)

        } else {
            $('#enable-taking-any-court-action').prop('disabled', false).removeClass('d-none');
        }
    }

    // Add event listener for click
    $('#close-date-borrowing').focusout(function () {
        EnableTakingAnyCourtActionClickEventFunction();
    });


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Person Borrowing Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-borrowing-detail-dt').click(function () {
        debugger;
        event.preventDefault();

        // For Court Case
        SetToggleSwitchBasedAccordions();
        EnableTakingAnyCourtActionClickEventFunction();
        SetModalTitle('borrowing-detail', 'Add');

    });

    // DataTable Edit Button 
    $('#btn-edit-borrowing-detail-dt').click(function () {
        debugger;
        SetModalTitle('borrowing-detail', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {

            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#borrowing-detail-modal').modal();

            let tmpDate = new Date();

            columnValues = $('#btn-edit-borrowing-detail-dt').data('rowindex');
            id = $('#borrowing-detail-modal').attr('id');

            // Set Opening, Maturity, Close Date, Filling Date
            openingDate = new Date(columnValues[6]);
            matureDate = new Date(columnValues[7]);
            closeDate = new Date(columnValues[8]);

            tmpDate = new Date(columnValues[6]);
            tmpDate.setDate(tmpDate.getDate() + 1);

            $('#expiry-open-date').attr('min', GetInputDateFormat(tmpDate))
            $('#close-date-borrowing').attr('min', GetInputDateFormat(tmpDate))
            $('#activation-filing-date').attr('min', GetInputDateFormat(tmpDate));


            // Set Filling, Registration, Date
            filingDate = new Date(columnValues[22]);
            registrationDate = new Date(columnValues[24]);

            // Set Min Of Registration Date
            tmpDate = new Date(columnValues[22]);
            tmpDate.setDate(tmpDate.getDate());

            $('#expiry-filing-date').attr('min', GetInputDateFormat(tmpDate));

            // Set Max Of Registration Date
            tmpDate = new Date(columnValues[22]);
            tmpDate.setDate(tmpDate.getDate() + 30);

            $('#expiry-filing-date').attr('max', GetInputDateFormat(tmpDate));

            // Set Maximum Attributes Sanction - Mortgage            
            $('#sanction-loan-amount').attr('max', columnValues[13]);

            // Set Maximum Attributes Installment - Sanction
            $('#installment-amount').attr('max', columnValues[14]);

            $('#name-of-organization', myModal).val(columnValues[1]);
            $('#trans-name-of-organization', myModal).val(columnValues[2]);
            $('#branch', myModal).val(columnValues[3]);
            $('#trans-branch', myModal).val(columnValues[4]);
            $('#reference-number', myModal).val(columnValues[5]);
            $('#activation-open-date', myModal).val(GetInputDateFormat(openingDate));
            $('#expiry-open-date', myModal).val(GetInputDateFormat(matureDate));
            $('#close-date-borrowing', myModal).val(GetInputDateFormat(closeDate));
            $('#loan-details', myModal).val(columnValues[9]);
            $('#trans-loan-details', myModal).val(columnValues[10]);
            $('#mortgage-details', myModal).val(columnValues[11]);
            $('#trans-mortgage-details', myModal).val(columnValues[12]);
            $('#mortgage-amount', myModal).val(columnValues[13]);
            $('#sanction-loan-amount', myModal).val(columnValues[14]);
            $('#installment-amount', myModal).val(columnValues[15]);
            $('#loan-balance-amount', myModal).val(columnValues[16]);
            $('#overdues-installment', myModal).val(columnValues[17]);
            $('#overdues-amount', myModal).val(columnValues[18]);

            $('#enable-taking-any-court-action', myModal).prop('checked', columnValues[19].toString().toLowerCase() === 'true' ? true : false);

            $('#court-case-type-id', myModal).val(columnValues[20]);
            $('#activation-filing-date', myModal).val(GetInputDateFormat(filingDate));
            $('#filing-number', myModal).val(columnValues[23]);
            $('#expiry-filing-date', myModal).val(GetInputDateFormat(registrationDate));
            $('#court-case-registration-number', myModal).val(columnValues[25]);
            $('#cnr-number', myModal).val(columnValues[26]);
            $('#court-case-stage-id', myModal).val(columnValues[27]);

            $('#note-borrowing-detail', myModal).val(columnValues[29]);
            $('#trans-note-borrowing-detail', myModal).val(columnValues[30]);
            $('#reason-for-modification-borrowing-detail', myModal).val(columnValues[31]);

            // For Court Case
            SetToggleSwitchBasedAccordions();

            EnableTakingAnyCourtActionClickEventFunction();

            // Show Modals
            $('#borrowing-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-borrowing-detail-edit-dt').addClass('read-only');
            $('#borrowing-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-borrowing-detail-modal').click(function (event) {
        debugger;
        if (IsValidBorrowingModal()) {
            row = borrowingDetailDataTable.row.add([
                tag,
                nameOfOrganization,
                transNameOfOrganization,
                branch,
                transBranch,
                referenceNumber,
                openingDate,
                matureDate,
                closeDate,
                loanDetails,
                transLoanDetails,
                mortgageDetails,
                transMortgageDetails,
                mortgageAmount,
                sanctionLoanAmount,
                installmentAmount,
                loanBalanceAmount,
                overduesInstallment,
                overduesAmount,
                isTakingAnyCourtAction,
                courtCaseType,
                courtCaseTypeText,
                filingDate,
                filingNumber,
                registrationDate,
                registrationNumber,
                cNRNumber,
                courtCaseStage,
                courtCaseStageText,
                note,
                transNote,
                reasonForModification
            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            $('#borrowing-detail-data-table-error').addClass('d-none');

            HideColumnsborrowingDetailDataTable();

            borrowingDetailDataTable.columns.adjust().draw();

            $('#borrowing-detail-modal').modal('hide');

            EnableNewOperation('borrowing-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-borrowing-detail-modal').click(function (event) {
        $('#select-all-borrowing-detail').prop('checked', false);
        if (IsValidBorrowingModal()) {
            borrowingDetailDataTable.row(selectedRowIndex).data([
                tag,
                nameOfOrganization,
                transNameOfOrganization,
                branch,
                transBranch,
                referenceNumber,
                openingDate,
                matureDate,
                closeDate,
                loanDetails,
                transLoanDetails,
                mortgageDetails,
                transMortgageDetails,
                mortgageAmount,
                sanctionLoanAmount,
                installmentAmount,
                loanBalanceAmount,
                overduesInstallment,
                overduesAmount,
                isTakingAnyCourtAction,
                courtCaseType,
                courtCaseTypeText,
                filingDate,
                filingNumber,
                registrationDate,
                registrationNumber,
                cNRNumber,
                courtCaseStage,
                courtCaseStageText,
                note,
                transNote,
                reasonForModification
            ]).draw();

            HideColumnsborrowingDetailDataTable();

            borrowingDetailDataTable.columns.adjust().draw();

            $('#borrowing-detail-modal').modal('hide');

            EnableNewOperation('borrowing-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-borrowing-detail-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-borrowing-detail tbody input[type="checkbox"]:checked').each(function () {
                    borrowingDetailDataTable.row($("#tbl-borrowing-detail tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-borrowing-detail-dt').data('rowindex');
                    EnableNewOperation('borrowing-detail');

                    $('#select-all-borrowing-detail').prop('checked', false);
                    //if (!borrowingDetailDataTable.data().any())
                    //    $('#borrowing-detail-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-borrowing-detail').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-borrowing-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = borrowingDetailDataTable.row(row).index();

                rowData = (borrowingDetailDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-borrowing-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('borrowing-detail');
            });
        }
        else {
            EnableNewOperation('borrowing-detail');

            $('#tbl-borrowing-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-borrowing-detail tbody').click('input[type=checkbox]', function () {
        $('#tbl-borrowing-detail input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = borrowingDetailDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (borrowingDetailDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('borrowing-detail');

                    $('#btn-update-borrowing-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-borrowing-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-borrowing-detail-dt').data('rowindex', arr);
                    $('#select-all-borrowing-detail').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-borrowing-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('borrowing-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('borrowing-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('borrowing-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-borrowing-detail').prop('checked', true);
        else
            $('#select-all-borrowing-detail').prop('checked', false);
    });

    // Validate Borrowing Module
    function IsValidBorrowingModal() {
        debugger;
        result = true;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        nameOfOrganization = $('#name-of-organization').val();
        transNameOfOrganization = $('#trans-name-of-organization').val();
        branch = $('#branch').val();
        transBranch = $('#trans-branch').val();
        referenceNumber = $('#reference-number').val();
        openingDate = $('#activation-open-date').val();
        matureDate = $('#expiry-open-date').val();
        closeDate = $('#close-date-borrowing').val();
        loanDetails = $('#loan-details').val();
        transLoanDetails = $('#trans-loan-details').val();
        mortgageDetails = $('#mortgage-details').val();
        transMortgageDetails = $('#trans-mortgage-details').val();
        mortgageAmount = parseFloat($('#mortgage-amount').val());
        sanctionLoanAmount = parseFloat($('#sanction-loan-amount').val());
        installmentAmount = parseFloat($('#installment-amount').val());
        loanBalanceAmount = parseFloat($('#loan-balance-amount').val());
        overduesInstallment = parseFloat($('#overdues-installment').val());
        overduesAmount = parseFloat($('#overdues-amount').val());
        isTakingAnyCourtAction = $('#enable-taking-any-court-action').is(':checked');
        note = $('#note-borrowing-detail').val();
        transNote = $('#trans-note-borrowing-detail').val();
        reasonForModification = $('#reason-for-modification-borrowing-detail').val();

        debugger;
        EnableTakingAnyCourtActionClickEventFunction();
        // Is Taking Any Court Action
        if (isTakingAnyCourtAction) {
            courtCaseType = $('#court-case-type-id option:selected').val();
            courtCaseTypeText = $('#court-case-type-id option:selected').text();
            registrationDate = $('#expiry-filing-date').val();
            filingDate = $('#activation-filing-date').val();
            courtCaseStage = $('#court-case-stage-id option:selected').val();
            courtCaseStageText = $('#court-case-stage-id option:selected').text();
            filingNumber = $('#filing-number').val();
            registrationNumber = $('#court-case-registration-number').val();
            cNRNumber = $('#cnr-number').val();
        }
        else {
            courtCaseType = '00000000-0000-0000-0000-000000000000';
            courtCaseTypeText = 'None';
            courtCaseStage = '00000000-0000-0000-0000-000000000000';
            courtCaseStageText = 'None';
            registrationDate = '1900/01/01';
            filingDate = '1900/01/01';
            cNRNumber = 'None';
            filingNumber = 'None';
            registrationNumber = 'None';
        }

        //Set Default Value if Empty
        if (note === '') {
            $('#note-borrowing-detail').val('None');
            note = 'None';
        }

        // TransNote
        if (transNote === '') {
            $('#trans-note-borrowing-detail').val('None');
            transNote = 'None';
        }

        // Reason For Modification
        if (reasonForModification === '') {
            $('#reason-for-modification-borrowing-detail').val('None');
            reasonForModification = 'None';
        }


        // NameOfOrganization
        minimumLength = parseInt($('#name-of-organization').attr('minlength'));
        maximumLength = parseInt($('#name-of-organization').attr('maxlength'));

        if (parseInt(nameOfOrganization.length) < parseInt(minimumLength) || parseInt(nameOfOrganization.length) > parseInt(maximumLength)) {
            result = false;
            $('#name-of-organization-error').removeClass('d-none');
        }

        // TransNameOfOrganization
        maximumLength = parseInt($('#trans-name-of-organization').attr('maxlength'));

        if (parseInt(transNameOfOrganization.length) === 0 || parseInt(transNameOfOrganization.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-name-of-organization-error').removeClass('d-none');
        }

        // Branch
        minimumLength = parseInt($('#branch').attr('minlength'));
        maximumLength = parseInt($('#branch').attr('maxlength'));

        if (parseInt(branch.length) < parseInt(minimumLength) || parseInt(branch.length) > parseInt(maximumLength)) {
            result = false;
            $('#branch-error').removeClass('d-none');
        }

        // TransBranch
        maximumLength = parseInt($('#trans-branch').attr('maxlength'));

        if (parseInt(transBranch.length) === 0 || parseInt(transBranch.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-branch-error').removeClass('d-none');
        }

        // ReferenceNumber
        minimumLength = parseInt($('#reference-number').attr('minlength'));
        maximumLength = parseInt($('#reference-number').attr('maxlength'));

        if (parseInt(referenceNumber.length) < parseInt(minimumLength) || parseInt(referenceNumber.length) > parseInt(maximumLength)) {
            result = false;
            $('#reference-number-error').removeClass('d-none');
        }

        // Opening Date
        if (IsValidInputDate('#activation-open-date') === false) {
            result = false;
            $('#activation-open-date-error').removeClass('d-none');
        }

        if (IsValidInputDate('#expiry-open-date') === false) {
            result = false;
            $('#expiry-open-date-error').removeClass('d-none');
        }

        if (closeDate === '') {
            // Close Date can be empty, clear error
            $('#close-date-borrowing-error').addClass('d-none');
        } else if (closeDate < openingDate) {
            result = false;
            $('#close-date-borrowing-error').removeClass('d-none');
        }

        // Loan Details
        minimumLength = parseInt($('#loan-details').attr('minlength'));
        maximumLength = parseInt($('#loan-details').attr('maxlength'));

        if (parseInt(loanDetails.length) < parseInt(minimumLength) || parseInt(loanDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#loan-details-error').removeClass('d-none');
        }

        // Translation Of LoanDetails
        maximumLength = parseInt($('#trans-loan-details').attr('maxlength'));

        if (parseInt(transLoanDetails.length) === 0 || parseInt(transLoanDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-loan-details-error').removeClass('d-none');
        }

        // Mortgage Details
        minimumLength = parseInt($('#mortgage-details').attr('minlength'));
        maximumLength = parseInt($('#mortgage-details').attr('maxlength'));

        if (parseInt(mortgageDetails.length) < parseInt(minimumLength) || parseInt(mortgageDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#mortgage-details-error').removeClass('d-none');
        }

        // Translation Of Mortgage Details
        maximumLength = parseInt($('#trans-mortgage-details').attr('maxlength'));

        if (parseInt(transMortgageDetails.length) === 0 || parseInt(transMortgageDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-mortgage-details-error').removeClass('d-none');
        }

        // Mortgage Amount
        if (isNaN(mortgageAmount) === false) {
            minimum = parseFloat($('#mortgage-amount').attr('min'));
            maximum = parseFloat($('#mortgage-amount').attr('max'));

            if (parseFloat(mortgageAmount) < parseFloat(minimum) || parseFloat(mortgageAmount) > parseFloat(maximum)) {
                result = false;
                $('#mortgage-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#mortgage-amount-error').removeClass('d-none');
        }

        // Sanction Loan Amount
        if (isNaN(sanctionLoanAmount) === false) {
            minimum = parseFloat($('#sanction-loan-amount').attr('min'));
            maximum = parseFloat($('#sanction-loan-amount').attr('max'));

            if (parseFloat(sanctionLoanAmount) < parseFloat(minimum) || parseFloat(sanctionLoanAmount) > parseFloat(maximum)) {
                result = false;
                $('#sanction-loan-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#sanction-loan-amount-error').removeClass('d-none');
        }

        // Installment Amount
        if (isNaN(installmentAmount) === false) {
            minimum = parseFloat($('#installment-amount').attr('min'));
            maximum = parseFloat($('#installment-amount').attr('max'));

            if (parseFloat(installmentAmount) < parseFloat(minimum) || parseFloat(installmentAmount) > parseFloat(maximum)) {
                result = false;
                $('#installment-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#installment-amount-error').removeClass('d-none');
        }

        // Loan Balance Amount
        if (isNaN(loanBalanceAmount) === false) {
            minimum = parseFloat($('#loan-balance-amount').attr('min'));
            maximum = parseFloat($('#loan-balance-amount').attr('max'));

            if (parseFloat(loanBalanceAmount) < parseFloat(minimum) || parseFloat(loanBalanceAmount) > parseFloat(maximum)) {
                result = false;
                $('#loan-balance-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#loan-balance-amount-error').removeClass('d-none');
        }

        // Overdues Installment
        if (isNaN(overduesInstallment) === false) {
            minimum = parseFloat($('#overdues-installment').attr('min'));
            maximum = parseFloat($('#overdues-installment').attr('max'));

            if (parseFloat(overduesInstallment) < parseFloat(minimum) || parseFloat(overduesInstallment) > parseFloat(maximum)) {
                result = false;
                $('#overdues-installment-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#overdues-installment-error').removeClass('d-none');
        }

        //overdues Amount
        if (isNaN(overduesAmount) === false) {
            minimum = parseFloat($('#overdues-amount').attr('min'));
            maximum = parseFloat($('#overdues-amount').attr('max'));

            if (parseFloat(overduesAmount) < parseFloat(minimum) || parseFloat(overduesAmount) > parseFloat(maximum)) {
                result = false;
                $('#overdues-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#overdues-amount-error').removeClass('d-none');
        }

        // If taking court action, perform validation checks
        if (isTakingAnyCourtAction) {
            // CourtCaseType
            if ($('#court-case-type-id').prop('selectedIndex') < 1) {
                result = false;
                $('#court-case-type-id-error').removeClass('d-none');
            }

            // FilingDate
            if (IsValidInputDate('#activation-filing-date') === false) {
                result = false;
                $('#activation-filing-date-error').removeClass('d-none');
            }

            // RegistrationDate
            if (IsValidInputDate('#expiry-filing-date') === false) {
                result = false;
                $('#expiry-filing-date-error').removeClass('d-none');
            }

            // CourtCaseStage
            if ($('#court-case-stage-id').prop('selectedIndex') < 1) {
                result = false;
                $('#court-case-stage-id-error').removeClass('d-none');
            }

            // FilingNumber
            minimumLength = parseInt($('#filing-number').attr('minlength'));
            maximumLength = parseInt($('#filing-number').attr('maxlength'));

            if (parseInt(filingNumber.length) < parseInt(minimumLength) || parseInt(filingNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#filing-number-error').removeClass('d-none');
            }

            // RegistrationNumber
            minimumLength = parseInt($('#court-case-registration-number').attr('minlength'));
            maximumLength = parseInt($('#court-case-registration-number').attr('maxlength'));

            if (parseInt(registrationNumber.length) < parseInt(minimumLength) || parseInt(registrationNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#court-case-registration-number-error').removeClass('d-none');
            }

            // CNRNumber
            minimumLength = parseInt($('#cnr-number').attr('minlength'));
            maximumLength = parseInt($('#cnr-number').attr('maxlength'));

            if (parseInt(cNRNumber.length) < parseInt(minimumLength) || parseInt(cNRNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#cnr-number-error').removeClass('d-none');
            }
        }
        else {
            // If not taking any court action, set default values
            courtCaseType = '00000000-0000-0000-0000-000000000000';
            courtCaseTypeText = 'None';
            courtCaseStage = '00000000-0000-0000-0000-000000000000';
            courtCaseStageText = 'None';
            registrationDate = '1900/01/01';
            filingDate = '1900/01/01';
            cNRNumber = 'None';
            filingNumber = 'None';
            registrationNumber = 'None';
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsborrowingDetailDataTable() {
        borrowingDetailDataTable.column(20).visible(false);
        borrowingDetailDataTable.column(27).visible(false);
        borrowingDetailDataTable.column(31).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Person Credit Ratings - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-credit-rating-dt').click(function () {
        event.preventDefault();

        SetModalTitle('credit-rating', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-credit-rating-dt').click(function () {
        SetModalTitle('credit-rating', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {

            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#credit-rating-modal').modal();

            columnValues = $('#btn-edit-credit-rating-dt').data('rowindex');

            creditRatingEffectiveDate = new Date(columnValues[1]);

            $('#effective-date', myModal).val(GetInputDateFormat(creditRatingEffectiveDate));
            $('#credit-bureau-agency-id', myModal).val(columnValues[2]);
            $('#score', myModal).val(columnValues[4]);
            $('#note-credit-rating', myModal).val(columnValues[5]);
            $('#reason-for-modification-credit-rating', myModal).val(columnValues[6]);

            // Show Modals
            $('#credit-rating-modal').modal('show');
        }
        else {
            $('#btn-edit-credit-rating-edit-dt').addClass('read-only');
            $('#credit-rating-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-credit-rating-modal').click(function (event) {
        debugger;
        if (IsValidCreditRatingModal()) {
            row = creditDataTable.row.add([
                tag,
                effectiveDate,
                agency,
                agencyText,
                score,
                note,
                reasonForModification
            ]).draw();

            // Error Message In Span
            $('#credit-rating-data-table-error').addClass('d-none');

            HideColumnsCreditDataTable();

            creditDataTable.columns.adjust().draw();

            $('#credit-rating-modal').modal('hide');

            EnableNewOperation('credit-rating');
        }
    });

    // Modal update Button Event
    $('#btn-update-credit-rating-modal').click(function (event) {
        $('#select-all-credit-rating').prop('checked', false);
        if (IsValidCreditRatingModal()) {
            creditDataTable.row(selectedRowIndex).data([
                tag,
                effectiveDate,
                agency,
                agencyText,
                score,
                note,
                reasonForModification
            ]).draw();
            // Error Message In Span
            $('#credit-rating-validation span').html('');

            HideColumnsCreditDataTable();

            creditDataTable.columns.adjust().draw();

            $('#credit-rating-modal').modal('hide');

            EnableNewOperation('credit-rating');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-credit-rating-dt').click(function (event) {

        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-credit-rating tbody input[type="checkbox"]:checked').each(function () {
                    creditDataTable.row($('#tbl-credit-rating tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-credit-rating-dt').data('rowindex');
                    EnableNewOperation('credit-rating');

                    $('#select-all-credit-rating').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-credit-rating').click(function () {

        if ($(this).prop('checked')) {
            $('#tbl-credit-rating tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = creditDataTable.row(row).index();

                rowData = (creditDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-credit-rating-dt').data('rowindex', arr);
                EnableDeleteOperation('credit-rating');
            });
        }
        else {
            EnableNewOperation('credit-rating');

            $('#tbl-credit-rating tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-credit-rating tbody').click('input[type=checkbox]', function () {
        $('#tbl-credit-rating input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = creditDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (creditDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('credit-rating');

                    $('#btn-update-credit-rating-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-credit-rating-dt').data('rowindex', rowData);
                    $('#btn-delete-credit-rating-dt').data('rowindex', arr);
                    $('#select-all-credit-rating').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-credit-rating tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('credit-rating');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('credit-rating');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('credit-rating');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-credit-rating').prop('checked', true);
        else
            $('#select-all-credit-rating').prop('checked', false);
    });

    // Validate Credit Rating Module
    function IsValidCreditRatingModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        effectiveDate = $('#effective-date').val();
        agency = $('#credit-bureau-agency-id option:selected').val();
        agencyText = $('#credit-bureau-agency-id option:selected').text();
        score = parseInt($('#score').val());
        note = $('#note-credit-rating').val();
        reasonForModification = $('#reason-for-modification-credit-rating').val();

        //Set Default Value if Empty
        if (note === '') {
            $('#note-credit-rating').val('None');
            note = 'None';
        }

        //reason For Modification
        if (reasonForModification === '') {
            $('#reason-for-modification-credit-rating').val('None');
            reasonForModification = 'None';
        }

        //credit bureau agency
        if ($('#credit-bureau-agency-id').prop('selectedIndex') < 1) {
            result = false;
            $('#credit-bureau-agency-id-error').removeClass('d-none');
        }

        //score
        if (isNaN(score) === false) {

            minimum = parseInt($('#score').attr('min'));
            maximum = parseInt($('#score').attr('max'));

            if (parseInt(score) < parseInt(minimum) || parseInt(score) > parseInt(maximum)) {
                result = false;
                $('#score-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#score-error').removeClass('d-none');
        }

        //EffectiveDate
        let isValidEffectiveDate = IsValidInputDate('#effective-date');

        if (isValidEffectiveDate === false) {
            result = false;
            $('#effective-date-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsCreditDataTable() {
        creditDataTable.column(2).visible(false);
        creditDataTable.column(6).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@   Validation(Event Handling) Code - Court Case @@@@@@@@@@@@@@@@@@@@@@@@@@@


    //Court Case Dropdown focusout value get clear
    $('#court-case-types-id').focusout(function () {
        const currentValue = $(this).val();

        if (currentValue !== lastSelectedValue) {
            $('#filing-dates').val('');
            $('#filing-numbers').val('');
            $('#registration-dates').val('');
            $('#registration-numbers').val('');
            $('#cnr-number-case').val('');
            $('#amount-of-decree').val('');
            $('#collateral-amount').val('');
            $('#court-cases-stage-id').val('');
            $('#note-court-case').val('');
        }

        lastSelectedValue = currentValue;
    });

    //Filing Date
    $('#activation-filing-dates').click(function () {
        $('#expiry-filing-dates').val('');
    });

    // Registration Date
    $('#expiry-filing-dates').click(function () {
        debugger;
        let today = new Date();

        let filingDate = new Date($('#activation-filing-dates').val());

        $('#expiry-filing-dates').attr('min', GetInputDateFormat(filingDate));

        let maxRegistrationDate = new Date(filingDate);
        maxRegistrationDate.setMonth(maxRegistrationDate.getMonth() + 1);

        if (maxRegistrationDate > today) {
            maxRegistrationDate = today;
        }

        $('#expiry-filing-dates').attr('max', GetInputDateFormat(maxRegistrationDate));
    });


    /// @@@@@@@@@@@@@@@@@@@@@@  Person Court Case - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-court-case-dt').click(function () {
        event.preventDefault();

        lastSelectedValue = '';

        SetModalTitle('court-case', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-court-case-dt').click(function () {
        debugger;

        SetModalTitle('court-case', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {

            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#court-case-modal').modal();

            let tmpDate = new Date();

            columnValues = $('#btn-edit-court-case-dt').data('rowindex');

            let courtcasefillingDate = new Date(columnValues[3]);
            let courtcaseregistrationDate = new Date(columnValues[5]);

            tmpDate = new Date(columnValues[3]);
            tmpDate.setDate(tmpDate.getDate());

            $('#expiry-filing-dates').attr('min', GetInputDateFormat(tmpDate));

            // Set Max Of Registration Date
            tmpDate = new Date(columnValues[5]);
            tmpDate.setDate(tmpDate.getDate() + 30);

            if (tmpDate > new Date()) {
                $('#expiry-filing-dates').attr('max', GetInputDateFormat(new Date()));
            }
            else {
                $('#expiry-filing-dates').attr('max', GetInputDateFormat(tmpDate));
            }

            lastSelectedValue = columnValues[1];

            $('#court-case-types-id', myModal).val(columnValues[1]);
            $('#activation-filing-dates', myModal).val(GetInputDateFormat(courtcasefillingDate));
            $('#filing-numbers', myModal).val(columnValues[4]);
            $('#expiry-filing-dates', myModal).val(GetInputDateFormat(courtcaseregistrationDate));
            $('#registration-numbers', myModal).val(columnValues[6]);
            $('#cnr-number-case', myModal).val(columnValues[7]);
            $('#amount-of-decree', myModal).val(columnValues[8]);
            $('#collateral-amount', myModal).val(columnValues[9]);
            $('#court-cases-stage-id', myModal).val(columnValues[10]);
            $('#note-court-case', myModal).val(columnValues[12]);
            $('#reason-for-modification-court-case', myModal).val(columnValues[13]);

            // Show Modals
            $('#court-case-modal').modal('show');
        }
        else {
            $('#btn-edit-court-case-edit-dt').addClass('read-only');
            $('#court-case-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-court-case-modal').click(function (event) {
        debugger;
        if (IsValidCourtCaseModal()) {
            row = courtCaseDataTable.row.add([
                tag,
                courtCaseTypeId,
                courtCaseTypeIdText,
                filingDate,
                filingNumber,
                registrationDate,
                registrationNumber,
                cnrNumber,
                amountOfDecree,
                collateralAmount,
                courtCaseStageId,
                courtCaseStageIdText,
                note,
                reasonForModification,
            ]).draw();

            // Error Message In Span
            $('#court-case-data-table-error').addClass('d-none');

            HideColumnsCourtCaseDataTable();

            courtCaseDataTable.columns.adjust().draw();

            $('#court-case-modal').modal('hide');

            EnableNewOperation('court-case');
        }
    });

    // Modal update Button Event
    $('#btn-update-court-case-modal').click(function (event) {
        let b = $('#btn-edit-court-case-dt').attr('rowindex');
        $('#select-all-court-case').prop('checked', false);
        if (IsValidCourtCaseModal()) {
            courtCaseDataTable.row(selectedRowIndex).data([
                tag,
                courtCaseTypeId,
                courtCaseTypeIdText,
                filingDate,
                filingNumber,
                registrationDate,
                registrationNumber,
                cnrNumber,
                amountOfDecree,
                collateralAmount,
                courtCaseStageId,
                courtCaseStageIdText,
                note,
                reasonForModification
            ]).draw();
            // Error Message In Span
            $('#court-case-validation span').html('');

            HideColumnsCourtCaseDataTable();

            courtCaseDataTable.columns.adjust().draw();

            $('#court-case-modal').modal('hide');

            EnableNewOperation('court-case');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-court-case-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-court-case tbody input[type="checkbox"]:checked').each(function () {
                    courtCaseDataTable.row($('#tbl-court-case tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-court-case-dt').data('rowindex');
                    EnableNewOperation('court-case');

                    $('#select-all-court-case').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-court-case').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-court-case tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = courtCaseDataTable.row(row).index();

                rowData = (courtCaseDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-court-case-dt').data('rowindex', arr);
                EnableDeleteOperation('court-case');
            });
        }
        else {
            EnableNewOperation('court-case')

            $('#tbl-court-case tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-court-case tbody').click('input[type=checkbox]', function () {
        $('#tbl-court-case input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = courtCaseDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (courtCaseDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('court-case');

                    $('#btn-update-court-case-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-court-case-dt').data('rowindex', rowData);
                    $('#btn-delete-court-case-dt').data('rowindex', arr);
                    $('#select-all-court-case').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-court-case tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('court-case');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('court-case');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('court-case');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-court-case').prop('checked', true);
        else
            $('#select-all-court-case').prop('checked', false);
    });

    // Validate Court Case Module
    function IsValidCourtCaseModal() {
        result = true;
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        courtCaseTypeId = $('#court-case-types-id option:selected').val();
        courtCaseTypeIdText = $('#court-case-types-id option:selected').text();
        filingDate = $('#activation-filing-dates').val();
        filingNumber = $('#filing-numbers').val();
        registrationDate = $('#expiry-filing-dates').val();
        registrationNumber = $('#registration-numbers').val();
        cnrNumber = $('#cnr-number-case').val();
        amountOfDecree = parseFloat($('#amount-of-decree').val());
        collateralAmount = parseFloat($('#collateral-amount').val());
        courtCaseStageId = $('#court-cases-stage-id option:selected').val();
        courtCaseStageIdText = $('#court-cases-stage-id option:selected').text();
        note = $('#note-court-case').val().trim();
        reasonForModification = $('#reason-for-modification-court-case').val();

        //Set Default Value if Empty
        if (note === '') {
            $('#note-court-case').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-court-case').val('None');
            reasonForModification = 'None';
        }

        //court case types
        if ($('#court-case-types-id').prop('selectedIndex') < 1) {
            result = false;
            $('#court-case-types-id-error').removeClass('d-none');
        }

        //Filing Date
        let isValidFilingDate = IsValidInputDate('#activation-filing-dates');

        if (isValidFilingDate === false) {
            result = false;
            $('#activation-filing-dates-error').removeClass('d-none');
        }

        //filing Number
        if (isNaN(filingNumber.length) === false) {
            minimumLength = parseInt($('#filing-numbers').attr('minlength'));
            maximumLength = parseInt($('#filing-numbers').attr('maxlength'));

            if (parseInt(filingNumber.length) < parseInt(minimumLength) || parseInt(filingNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#filing-numbers-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#filing-numbers-error').removeClass('d-none');
        }

        let isValidRegistrationDate = IsValidInputDate('#expiry-filing-dates');

        //Registration Date
        if (isValidRegistrationDate === false) {
            result = false;
            $('#expiry-filing-dates-error').removeClass('d-none');
        }

        //registration Number
        if (isNaN(registrationNumber.length) === false) {
            minimumLength = parseInt($('#registration-numbers').attr('minlength'));
            maximumLength = parseInt($('#registration-numbers').attr('maxlength'));

            if (parseInt(registrationNumber.length) < parseInt(minimumLength) || parseInt(registrationNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#registration-numbers-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#registration-numbers-error').removeClass('d-none');
        }

        //cnr Number
        if (isNaN(cnrNumber.length) === false) {
            minimumLength = parseInt($('#cnr-number-case').attr('minlength'));
            maximumLength = parseInt($('#cnr-number-case').attr('maxlength'));

            if (parseInt(cnrNumber.length) < parseInt(minimumLength) || parseInt(cnrNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#cnr-number-case-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#cnr-number-case-error').removeClass('d-none');
        }

        //amount Of Decree
        if (isNaN(amountOfDecree) === false) {
            minimum = parseFloat($('#amount-of-decree').attr('min'));
            maximum = parseFloat($('#amount-of-decree').attr('max'));

            if (parseFloat(amountOfDecree) < parseFloat(minimum) || parseFloat(amountOfDecree) > parseFloat(maximum)) {
                result = false;
                $('#amount-of-decree-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#amount-of-decree-error').removeClass('d-none');
        }

        //collateral Amount
        if (isNaN(collateralAmount) === false) {
            minimum = parseFloat($('#collateral-amount').attr('min'));
            maximum = parseFloat($('#collateral-amount').attr('max'));

            if (parseFloat(collateralAmount) < parseFloat(minimum) || parseFloat(collateralAmount) > parseFloat(maximum)) {
                result = false;
                $('#collateral-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#collateral-amount-error').removeClass('d-none');
        }

        //court case stage
        if ($('#court-cases-stage-id').prop('selectedIndex') < 1) {
            result = false;
            $('#court-cases-stage-id-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsCourtCaseDataTable() {
        courtCaseDataTable.column(1).visible(false);
        courtCaseDataTable.column(10).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@    Validation(Event Handling) Code - Income Tax Detail  @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-income-tax-dt').click(function () {
        debugger;
        event.preventDefault();
        isDbRecord = false;
        personIncomeTaxDetailDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';

        SetModalTitle('income-tax', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-income-tax-dt').click(function () {

        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('income-tax', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {

            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#income-tax-modal').modal();

            columnValues = $('#btn-edit-income-tax-dt').data('rowindex');
            // Display Value In Modal Inputs
            $('#assessments-year-income-tax', myModal).val(columnValues[1]);
            $('#tax-amounts', myModal).val(columnValues[2]);
            $('#income-tax-file-caption', myModal).val(columnValues[5]);
            $('#note-income-tax-detail', myModal).val(columnValues[6]);
            $('#reason-for-modification-tax-detail', myModal).val(columnValues[7]);

            fileUploader = $('#' + $(columnValues[3]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'income-tax-file-uploader';

            // columnValues[3] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[3]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[3]).attr('class') === 'db-record' ? true : false;

            // columnValues[4] - Image Tag Html
            filePath = $('#' + $(columnValues[4]).attr('id')).attr('src');

            fileNameDocument = columnValues[8];
            personIncomeTaxDetailDocumentPrmKey = columnValues[9];
            localStoragePath = columnValues[10];


            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                AttachFileUploader();
            }

            $('#income-tax-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#income-tax-modal').modal('show');
        }
        else {
            $('#btn-edit-income-tax-edit-dt').addClass('read-only');
            $('#income-tax-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-income-tax-modal').click(function (event) {

        if (IsValidIncomeTaxModal()) {
            row = incomeTaxDataTable.row.add([
                tag,
                assessmentYear,
                taxAmount,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                personIncomeTaxDetailDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            $('#income-tax-data-table-error').addClass('d-none');

            HideColumnsIncomeTaxDataTable();

            incomeTaxDataTable.columns.adjust().draw();

            $('#income-tax-modal').modal('hide');

            EnableNewOperation('income-tax');
        }
    });

    // Modal update Button Event
    $('#btn-update-income-tax-modal').click(function (event) {
        debugger
        $('#select-all-income-tax').prop('checked', false);
        debugger
        if (IsValidIncomeTaxModal()) {
            debugger
            incomeTaxDataTable.row(selectedRowIndex).data([
                tag,
                assessmentYear,
                taxAmount,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                personIncomeTaxDetailDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideColumnsIncomeTaxDataTable();

            incomeTaxDataTable.columns.adjust().draw();
            columnValues = $('#btn-update-income-tax').data('rowindex');

            $('#income-tax-modal').modal('hide');

            EnableNewOperation('income-tax');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-income-tax-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-income-tax tbody input[type="checkbox"]:checked').each(function () {
                    incomeTaxDataTable.row($('#tbl-income-tax tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-income-tax-dt').data('rowindex');
                    EnableNewOperation('income-tax');

                    $('#select-all-income-tax').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-income-tax').click(function () {

        if ($(this).prop('checked')) {
            $('#tbl-income-tax tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = incomeTaxDataTable.row(row).index();

                rowData = (incomeTaxDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-income-tax-dt').data('rowindex', arr);
                EnableDeleteOperation('income-tax')
            });
        }
        else {
            EnableNewOperation('income-tax')

            $('#tbl-income-tax tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-income-tax tbody').click('input[type=checkbox]', function () {
        $('#tbl-income-tax input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = incomeTaxDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (incomeTaxDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('income-tax');

                    $('#btn-update-income-tax-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-income-tax-dt').data('rowindex', rowData);
                    $('#btn-delete-income-tax-dt').data('rowindex', arr);
                    $('#select-all-income-tax').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-income-tax tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('income-tax');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('income-tax');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('income-tax');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-income-tax').prop('checked', true);
        else
            $('#select-all-income-tax').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidIncomeTaxModal() {
        debugger;
        result = true;
        counter++;
        fileUploaderId = 'data-table-income-tax-file-uploader' + counter;
        fileId = "photo-Id" + counter;

        // Get Modal Inputs In Local letiable
        $('#select-all-asset-document').prop('checked', false);

        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        assessmentYear = parseInt($('#assessments-year-income-tax').val());
        taxAmount = parseFloat($('#tax-amounts').val());
        fileCaption = $('#income-tax-file-caption').val();
        note = $('#note-income-tax-detail').val();
        reasonForModification = $('#reason-for-modification-tax-detail').val();

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#income-tax-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';

        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        fileUploader = $('#income-tax-file-uploader').get(0);

        // Set Default Value, If Empty
        if (note === '') {
            $('#note-income-tax-detail').val('None');
            note = 'None';
        }

        if (fileCaption === '') {
            $('#income-tax-file-caption').val('None');
            fileCaption = 'None';
        }

        // Get the current year
        let currentYear = new Date().getFullYear();

        // Calculate the minimum allowed year
        let minAllowedYear = currentYear - 20;

        if (isNaN(assessmentYear) === false) {
            if (parseInt(assessmentYear) < parseInt(minAllowedYear) || parseInt(assessmentYear) > parseInt(currentYear)) {
                result = false;
                $('#assessments-year-income-tax-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#assessments-year-income-tax-error').removeClass('d-none');
        }

        //tax Amount
        if (isNaN(taxAmount) === false) {
            minimum = parseFloat($('#tax-amounts').attr('min'));
            maximum = parseFloat($('#tax-amounts').attr('max'));

            if (parseFloat(taxAmount) < parseFloat(minimum) || parseFloat(taxAmount) > parseFloat(maximum)) {
                result = false;
                $('#tax-amounts-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#tax-amounts-error').removeClass('d-none');
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.IncomeTaxDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#income-tax-file-uploader-error').removeClass('d-none');
                }
            }
            else {
                let photoSrc = $('#income-tax-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
        }

        //filecaption
        maximumLength = parseInt($('#income-tax-file-caption').attr('maxlength'));

        if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#income-tax-file-caption-error').removeClass('d-none');
        }

        return result;

    }

    // Hide Unnecessary Columns
    function HideColumnsIncomeTaxDataTable() {
        incomeTaxDataTable.column(7).visible(false);
        incomeTaxDataTable.column(8).visible(false);
        incomeTaxDataTable.column(9).visible(false);
        incomeTaxDataTable.column(10).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Validation(Event Handling) Code - Social Media @@@@@@@@@@@@@@@@@@@@@@@@@@@

    //clear vlaue
    $('#social-media-id').focusout(function () {
        debugger;
        let currentValue = $(this).val();

        if (currentValue !== lastSelectedValue) {
            $('#social-media-link').val('');
            $('#other-details-social-media').val('');
            $('#note-social-media').val('');
        }
        lastSelectedValue = currentValue;
    });


    /// @@@@@@@@@@@@@@@@@@@@@@   Social Media  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-social-media-dt').click(function () {
        event.preventDefault();

        lastSelectedValue = '';

        SetModalTitle('social-media', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-social-media-dt').click(function () {
        SetModalTitle('social-media', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#social-media-modal').modal();


            columnValues = $('#btn-edit-social-media-dt').data('rowindex');

            lastSelectedValue = columnValues[1];

            $('#social-media-id', myModal).val(columnValues[1]);
            $('#social-media-link', myModal).val(columnValues[3]);
            $('#other-details-social-media', myModal).val(columnValues[4]);
            $('#note-social-media', myModal).val(columnValues[5]);
            $('#reason-for-modification-social-media', myModal).val(columnValues[6]);
            // Show Modals
            $('#social-media-modal').modal('show');
        }
        else {
            $('#btn-edit-social-media-edit-dt').addClass('read-only');
            $('#social-media-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-social-media-modal').click(function (event) {
        if (IsValidSocialMediaModal()) {
            row = socialMediaDataTable.row.add([
                tag,
                socialMediaId,
                socialMediaIdText,
                socialMediaLink,
                otherDetails,
                note,
                reasonForModification

            ]).draw();

            // Error Message In Span
            $('#social-media-data-table-error').addClass('d-none');

            HideColumnsSocialMediaDataTable();

            socialMediaDataTable.columns.adjust().draw();

            $('#social-media-modal').modal('hide');

            EnableNewOperation('social-media');
        }
    });

    // Modal update Button Event
    $('#btn-update-social-media-modal').click(function (event) {
        $('#select-all-social-media').prop('checked', false);
        if (IsValidSocialMediaModal()) {
            socialMediaDataTable.row(selectedRowIndex).data([
                tag,
                socialMediaId,
                socialMediaIdText,
                socialMediaLink,
                otherDetails,
                note,
                reasonForModification
            ]).draw();

            HideColumnsSocialMediaDataTable();

            socialMediaDataTable.columns.adjust().draw();

            $('#social-media-modal').modal('hide');

            EnableNewOperation('social-media');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-social-media-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-social-media tbody input[type="checkbox"]:checked').each(function () {
                    socialMediaDataTable.row($('#tbl-social-media tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-social-media-dt').data('rowindex');
                    EnableNewOperation('social-media');

                    $('#select-all-social-media').prop('checked', false);

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-social-media').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-social-media tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = socialMediaDataTable.row(row).index();

                rowData = (socialMediaDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-social-media-dt').data('rowindex', arr);
                EnableDeleteOperation('social-media');
            });
        }
        else {
            EnableNewOperation('social-media');

            $('#tbl-social-media tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-social-media tbody').click('input[type=checkbox]', function () {
        $('#tbl-social-media input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = socialMediaDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (socialMediaDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('social-media');

                    $('#btn-update-social-media-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-social-media-dt').data('rowindex', rowData);
                    $('#btn-delete-social-media-dt').data('rowindex', arr);
                    $('#select-all-social-media').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-social-media tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('social-media');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('social-media');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('social-media');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-social-media').prop('checked', true);
        else
            $('#select-all-social-media').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidSocialMediaModal() {
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        socialMediaId = $('#social-media-id option:selected').val();
        socialMediaIdText = $('#social-media-id option:selected').text();
        socialMediaLink = $('#social-media-link').val();
        otherDetails = $('#other-details-social-media').val();
        note = $('#note-social-media').val();
        reasonForModification = $('#reason-for-modification-social-media').val();

        // Set Default Value, If Empty
        if (note === '') {
            $('#note-social-media').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-social-media').val('None');
            reasonForModification = 'None';
        }

        if (socialMediaLink === '') {
            socialMediaLink = 'None';
        }

        if (otherDetails === '') {
            otherDetails = 'None';
        }

        //Social media 
        if ($('#social-media-id').prop('selectedIndex') < 1) {
            result = false;
            $('#social-media-id-error').removeClass('d-none');
        }

        //social Media Link
        if (isNaN(socialMediaLink.length) === false) {
            maximumLength = parseInt($('#social-media-link').attr('maxlength'));

            if (parseInt(socialMediaLink.length) > parseInt(maximumLength)) {
                result = false;
                $('#social-media-link-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#social-media-link-error').removeClass('d-none');
        }

        //other Details
        if (isNaN(otherDetails.length) === false) {
            maximumLength = parseInt($('#other-details-social-media').attr('maxlength'));

            if (parseInt(otherDetails.length) > parseInt(maximumLength)) {
                result = false;
                $('#other-details-social-media-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#other-details-social-media-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsSocialMediaDataTable() {
        socialMediaDataTable.column(1).visible(false);
        socialMediaDataTable.column(6).visible(false);
    }



    /// @@@@@@@@@@@@@@@@@@@@@@  Validation(Event Handling) Code -  Scheme SMS Alert @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Function to add a new sending-time textbox
    let maxField = 10; // Input fields increment limitation
    let addButton = $('.add_button'); // Add button selector
    let wrapper = $('.field_wrapper'); // Input field wrapper
    let fieldHTML = '<div class="field_wrapper1"><div class="row"><div class="col-xs-11 col-sm-11 col-md-11" id="mydiv"><div class="form-group"><input type="time" id="virtual" class="form-control schedule-time mandatory-mark" name="field_name[]" value="" placeholder = "Enter Sending Time" required/> <div class="col-xs-1 col-sm-1 col-md-1" id="removebtn" style="margin-right:-6%; margin-top:-8.5%;float:right"><a href="javascript:void(0);" class="remove_button btn btn-danger"><i class="fas fa-minus"></i></a></div><span class="error-time-input-message-new modal-input-error">Please Select Schedule Time & Then Press Add Button Again</span></div></div></div></div></div>'; // New input field html 
    let x = 1; // Initial field counter is 1

    // Once add button is clicked
    $(addButton).click(function () {
        event.preventDefault();
        let time = $('#sending-time').val().trim(); // Get the value of the date input
        let fieldHTMLVal = $('.field_wrapper1 .schedule-time');
        let lastFieldVal = fieldHTMLVal.last().val();
        let lastField = fieldHTMLVal.last();

        // Check if date input is not empty
        if (time !== '') {
            // Check maximum number of input fields
            if (x < maxField) {
                x++; // Increase field counter
                if (lastFieldVal !== '') {
                    lastField.removeClass('time-input-error-new');
                    lastField.siblings('.error-time-input-message-new').hide();
                    $(wrapper).append(fieldHTML);
                }
                else {
                    lastField.addClass('time-input-error-new'); // Add error styling
                    lastField.siblings('.error-time-input-message-new').show();
                    x--;
                    event.preventDefault();
                }
            } else {
                alert('A maximum of ' + maxField + ' fields are allowed to be added.');
            }
        } else {
            $('#sending-time-error').removeClass('d-none').text('Please enter a date before adding a new Schedule Time');
        }
    });

    // Once remove button is clicked
    $(wrapper).on('click', '.remove_button', function (e) {
        e.preventDefault();
        $(this).closest('div.row').remove(); // Remove field html
        x--; // Decrease field counter
    });



    /// @@@@@@@@@@@@@@@@@@@@@@   Scheme SMS Alert  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-sms-alert-dt').click(function () {
        event.preventDefault();

        $('#removebtn').removeClass('read-only');
        $('.field_wrapper1').html('');
        SetModalTitle('sms-alert', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-sms-alert-dt').click(function () {
        SetModalTitle('sms-alert', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {

            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#sms-alert-modal').modal();

            columnValues = $('#btn-edit-sms-alert-dt').data('rowindex');

            [time, meridian] = columnValues[5].split(' ');
            [hours, minutes] = time.split(':');
            if (hours === '12') {
                hours = '00';
            }
            if (meridian === 'PM')
                hours = parseInt(hours, 10) + 12;
            sendingTime = hours + ':' + minutes;
            $('#removebtn').addClass('read-only');

            $('#alert-type-id', myModal).val(columnValues[1]);
            $('#notice-language-id', myModal).val(columnValues[3]);
            $('#sending-time', myModal).val(sendingTime);
            $('#note-sms-alert', myModal).val(columnValues[6]);
            $('#reason-for-modification-sms-alert', myModal).val(columnValues[7]);
            $('.field_wrapper1').html('');
            // Show Modals
            $('#sms-alert-modal').modal('show');
        }
        else {
            $('#btn-edit-sms-alert-edit-dt').addClass('read-only');
            $('#sms-alert-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-sms-alert-modal').click(function (event) {
        if (IsValidSmsAlertModal()) {
            for (var i = 0; i < scheduletime.length; i++) {
                row = smsAlertDataTable.row.add([
                    tag,
                    personInformationParameterNoticeTypeId,
                    personInformationParameterNoticeTypeIdText,
                    appLanguageId,
                    appLanguageIdText,
                    scheduletime[i],
                    note,
                    reasonForModification
                ]).draw();
            }
            // Error Message In Span
            $('#sms-alert-data-table-error').addClass('d-none');

            scheduletime = [];

            HideColumnsSmsAlertDataTable();

            smsAlertDataTable.columns.adjust().draw();

            $('#sms-alert-modal').modal('hide');

            EnableNewOperation('sms-alert');
        }
    });

    // Modal update Button Event
    $('#btn-update-sms-alert-modal').click(function (event) {
        debugger
        $('#select-all-sms-alert').prop('checked', false);
        if (IsValidSmsAlertModal()) {
            for (var i = 0; i < scheduletime.length; i++) {
                smsAlertDataTable.row(selectedRowIndex).data([
                    tag,
                    personInformationParameterNoticeTypeId,
                    personInformationParameterNoticeTypeIdText,
                    appLanguageId,
                    appLanguageIdText,
                    scheduletime[i],
                    note,
                    reasonForModification
                ]).draw();
            }
            HideColumnsSmsAlertDataTable();

            smsAlertDataTable.columns.adjust().draw();

            $('#sms-alert-modal').modal('hide');

            EnableNewOperation('sms-alert');

            scheduletime = [];
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-sms-alert-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-sms-alert tbody input[type="checkbox"]:checked').each(function () {
                    smsAlertDataTable.row($('#tbl-sms-alert tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-sms-alert-dt').data('rowindex');
                    EnableNewOperation('sms-alert');

                    $('#select-all-sms-alert').prop('checked', false);
                    if (!smsAlertDataTable.data().any())
                    $('#sms-alert-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-sms-alert').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-sms-alert tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = smsAlertDataTable.row(row).index();

                rowData = (smsAlertDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-sms-alert-dt').data('rowindex', arr);
                EnableDeleteOperation('sms-alert');
            });
        }
        else {
            EnableNewOperation('sms-alert');

            $('#tbl-sms-alert tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-sms-alert tbody').click('input[type=checkbox]', function () {
        $('#tbl-sms-alert input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = smsAlertDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (smsAlertDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('sms-alert');

                    $('#btn-update-sms-alert-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-sms-alert-dt').data('rowindex', rowData);
                    $('#btn-delete-sms-alert-dt').data('rowindex', arr);
                    $('#select-all-sms-alert').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-sms-alert tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('sms-alert');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('sms-alert');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('sms-alert');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-sms-alert').prop('checked', true);
        else
            $('#select-all-sms-alert').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidSmsAlertModal() {
        result = true;
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        personInformationParameterNoticeTypeId = $('#alert-type-id option:selected').val();
        personInformationParameterNoticeTypeIdText = $('#alert-type-id option:selected').text();
        appLanguageId = $('#notice-language-id option:selected').val();
        appLanguageIdText = $('#notice-language-id option:selected').text();
        sendingTime = $('#sending-time').val();
        note = $('#note-sms-alert').val();
        reasonForModification = $('#reason-for-modification-sms-alert').val();
        ss = $('#sms-div').hasClass('d-none');
        scheduletime = [];

        $('input.schedule-time').each(function () {

            let tt = $(this).val().split(':');

            if (tt !== "") {
                let h = parseInt(tt[0]);
                let m = parseInt(tt[1]);
                let array = [h, m];
                array = tt;
                let ampm = h >= 12 ? 'PM' : 'AM';
                h = h % 12;
                h = h ? +h : 12; // 0 should be 12
                h = h < 10 ? '0' + h : h;
                m = m < 10 ? '0' + m : m; // if minutes less than 10,    add a 0 in front of it ie: 6:6 -> 6:06
                let strTime = h + ':' + m + ' ' + ampm;
                scheduletime.push(strTime);
            }
        });

        let fieldHTMLVal = $('.field_wrapper1 .schedule-time');
        let lastFieldVal = fieldHTMLVal.last().val();
        let lastField = fieldHTMLVal.last();

        if (lastFieldVal === '') {
            lastField.addClass('time-input-error-new'); // Add error styling
            lastField.siblings('.error-time-input-message-new').show();
            result = false;
        }

        // Set Default Value, If Empty
        if (note === '') {
            $('#note-sms-alert').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-sms-alert').val('None');
            reasonForModification = 'None';
        }

        if (ss === true) {
            reasonForModification = 'None';
        }

        //alert type dropdown
        if ($('#alert-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#alert-type-id-error').removeClass('d-none');
        }

        //alert type dropdown
        if ($('#notice-language-id').prop('selectedIndex') < 1) {
            result = false;
            $('#notice-language-id-error').removeClass('d-none');
        }

        //sending Time
        if (sendingTime === '') {
            result = false;
            $('#sending-time-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsSmsAlertDataTable() {
        smsAlertDataTable.column(1).visible(false);
        smsAlertDataTable.column(3).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Set Page Loading Default Values @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    function SetPageLoadingDefaultValues() {

        if (($('#sign-document-upload').val()) === MANDATORY) {
            $('#group-sign-file-uploader').addClass('mandatory-mark');
            $('#group-sign-file-uploader').attr('required');
            $('#group-sign-file-caption').attr('required');
            $('#group-sign-file-caption').addClass('mandatory-mark');
        } else {
            $('#group-sign-file-uploader').removeClass('mandatory-mark');
            $('#group-sign-file-caption').removeClass('mandatory-mark');
            $('#group-sign-file-uploader').removeAttr('required');
            $('#group-sign-file-caption').removeAttr('required');
        }

        //Validation Photo Document On MANDATORY
        if (($('#photo-document-upload').val()) === MANDATORY) {
            $('#photo-file-uploader').addClass('mandatory-mark');
            $('#photo-file-uploader').attr('required');
            $('#photo-file-caption').attr('required');
            $('#photo-file-caption').addClass('mandatory-mark');
        }
        else {
            $('#photo-file-uploader').removeClass('mandatory-mark');
            $('#photo-file-uploader').removeAttr('required');
            $('#photo-file-caption').removeAttr('required');
            $('#photo-file-caption').removeClass('mandatory-mark');
        }

        //Validation Sign Document On MANDATORY
        if (($('#sign-document-upload').val()) === MANDATORY)
        {
            $('#person-sign-file-uploader').addClass('mandatory-mark');
            $('#person-sign-file-uploader').attr('required');
            $('#person-sign-file-caption').attr('required');
            $('#person-sign-file-caption').addClass('mandatory-mark');
        }
        else {
            $('#person-sign-file-uploader').removeClass('mandatory-mark');
            $('#person-sign-file-uploader').removeAttr('required');
            $('#person-sign-file-caption').removeAttr('required');
            $('#person-sign-file-caption').removeClass('mandatory-mark');
        }

        if (($('#bank-statement-upload').val()) === MANDATORY) {
            $('#bank-file-uploader').attr('required');
            $('#bank-file-caption').attr('required');
            $('#bank-file-uploader').addClass('mandatory-mark');
            $('#bank-file-caption').addClass('mandatory-mark');
        }
        else {
            $('#bank-file-uploader').removeAttr('required');
            $('#bank-file-caption').removeAttr('required');
            $('#bank-file-uploader').removeClass('mandatory-mark');
            $('#bank-file-caption').removeClass('mandatory-mark');
        }

        //Validation Asset On MANDATORY
        debugger;
        if (($('#financial-asset-document-upload').val()) === MANDATORY) {
            $('#finance-file-uploader').attr('required');
            $('#finance-file-caption').attr('required');
            $('#finance-file-uploader').addClass('mandatory-mark');
            $('#finance-file-caption').addClass('mandatory-mark');
        }
        else {
            $('#finance-file-uploader').removeAttr('required');
            $('#finance-file-caption').removeAttr('required');
            $('#finance-file-uploader').removeClass('mandatory-mark');
            $('#finance-file-caption').removeClass('mandatory-mark');
        }

        //Validation Movable Asset On MANDATORY
        if (($('#movable-asset-document-upload').val()) === MANDATORY) {
            $('#movable-file-uploader').attr('required');
            $('#movable-file-caption').attr('required');
            $('#movable-file-uploader').addClass('mandatory-mark');
            $('#movable-file-caption').addClass('mandatory-mark');
        }
        else {
            $('#movable-file-uploader').removeAttr('required');
            $('#movable-file-caption').removeAttr('required');
            $('#movable-file-uploader').removeClass('mandatory-mark');
            $('#movable-file-caption').removeClass('mandatory-mark');
        }

        //Validation Immovable Asset On MANDATORY
        debugger;
        if (($('#immovable-asset-upload').val()) === MANDATORY) {
            $('#immovable-file-uploader').attr('required');
            $('#immovable-file-caption').attr('required');
            $('#immovable-file-uploader').addClass('mandatory-mark');
            $('#immovable-file-caption').addClass('mandatory-mark');
        } else {
            $('#immovable-file-uploader').removeClass('mandatory-mark');
            $('#immovable-file-caption').removeClass('mandatory-mark');
            $('#immovable-file-uploader').attr('required');
            $('#immovable-file-caption').attr('required');
        }

        //Validation Immovable Asset On MANDATORY
        if (($('#agriculture-asset-document-upload').val()) === MANDATORY) {
            $('#agriculture-file-uploader').attr('required');
            $('#file-caption').attr('required');
            $('#agriculture-file-uploader').addClass('mandatory-mark');
            $('#agriculture-file-caption').addClass('mandatory-mark');
        }
        else {
            $('#agriculture-file-uploader').removeAttr('required');
            $('#agriculture-file-caption').removeAttr('required');
            $('#agriculture-file-uploader').removeClass('mandatory-mark');
            $('#agriculture-file-caption').removeClass('mandatory-mark');
        }

        //Validation Machinery Asset On MANDATORY
        if (($('#machinery-asset-document-upload').val()) === MANDATORY) {
            $('#machinery-file-uploader').addClass('mandatory-mark');
            $('#machinery-file-uploader').attr('required');
            $('#machinery-file-caption').addClass('mandatory-mark');
            $('#machinery-file-caption').attr('required');
        }
        else {
            $('#machinery-file-uploader').removeAttr('required');
            $('#machinery-file-uploader').removeClass('mandatory-mark');
            $('#machinery-file-caption').removeClass('mandatory-mark');
            $('#machinery-file-caption').removeAttr('required');
        }


        //Validation Income Tax On MANDATORY
        debugger;
        if (($('#income-tax-detail-upload').val()) === MANDATORY) {
            $('#income-tax-file-uploader').attr('required');
            $('#income-tax-file-caption').attr('required');
            $('#income-tax-file-uploader').addClass('mandatory-mark');
            $('#income-tax-file-caption').addClass('mandatory-mark');
        } else {
            $('#income-tax-file-uploader').removeClass('mandatory-mark');
            $('#income-tax-file-caption').removeClass('mandatory-mark');
            $('#income-tax-file-uploader').removeAttr('required');
            $('#income-tax-file-caption').removeAttr('required');
        }


        if (($('#kyc-document-upload').val()) === MANDATORY) {
            $('#kyc-file-uploader').attr('required');
            $('#kyc-file-caption').attr('required');

            $('#kyc-file-uploader').addClass('mandatory-mark');
            $('#kyc-file-caption').addClass('mandatory-mark');
        }
        else {
            $('#kyc-file-uploader').removeAttr('required');
            $('#kyc-file-caption').removeAttr('required');

            $('#kyc-file-uploader').removeClass('mandatory-mark');
            $('#kyc-file-caption').removeClass('mandatory-mark');
        }

        debugger;
        //if ($('#o-remark').val().length === 0)
        //    $('#o-remark').val('None');

        //SetBoardOfDirectorUniqueDropdownList();
        //SetAddressTypeUniqueDropdownList();
        // Get Person Dropdown For Nominee
        $.get('/DynamicDropdownList/GetPersonDropdownListForNominee', function (data) {
            debugger;
            personDropdownListDataForFamily = data;
        });

        // Get Person Dropdown For Nominee
        $.get('/DynamicDropdownList/GetPersonDropdownListForNominee', function (data) {
            debugger;
            personDropdownListDataForFamily = data;
        });

        // Get Person Dropdown For Guardian
        $.get('/DynamicDropdownList/GetPersonDropdownListForGuardian', function (data) {
            personDropdownListDataForFamily = data;
        });

        // Get Person Dropdown For Guardian
        $.get('/DynamicDropdownList/GetPersonDropdownListForGuardian', function (data) {
            personDropdownListDataForGuardian = data;
        });

        debugger;

        // Check Whether Element Exist OR Not - Applicable For Only Amend
        if ($('#guardian-pin1').val() > 0) {
            debugger;
            $('#guardian-detail').addClass('d-none');
            $('#guardian-pin-input').removeClass('d-none');

            let guardianIdValueOnAmend = $('#guardian-pin-value').attr('class').toString().replace('d-none', '');

            let selectedGuardianId = $('#guardian-pin1').val();

            guardianPersonInformationNumber = selectedGuardianId;

            $('#guardian-pin').val(guardianIdValueOnAmend);
        }
        else {
            $('#guardian-pin-input').addClass('d-none');
            $('#guardian-detail').removeClass('d-none');
        }

        // Disalbe Events On Verify View
        if ($('#verify-view').length > 0)
            isVerifyView = true;

        if ($('#amend-view').length > 0)
            isAmendView = true;

        //'vip-rank' and convert it to an integer
        let vipRank = parseInt($('#vip-rank').val());

        if (parseInt(vipRank) === 0) {
            $('.vip-background-details').addClass('d-none');
        }
        else {
            $('.vip-background-details').removeClass('d-none');
        }

        SetGSTRegistrationDetail();
        SetDocumentBirthdate();
        CalculateAgeInYears();
        SetMaritalStatusDetails();
        SetOccupationDetails();


        ResendSMS();

        CheckPersonType(personTypeId);

        //Calling Enable Taking Any CourtAction
        EnableTakingAnyCourtActionClickEventFunction();

        //Calling Function Additional Income
        IncomeSourceChange();

        //Validation Date 
        ValidateEstablishmentDate();

        //Calling Function Enable Authorized Signatory
        EnableAuthorizedSignatory();


        // Call the function GST for amend and verify 
        let registrationNumber = $('#gst-registration-number').val(); // Get the value correctly

        if (registrationNumber.length === 15) {
            // Check the checkbox
            $('#enable-gst-registration-details').prop('checked', true);
        } else {
            // Uncheck the checkbox
            $('#enable-gst-registration-details').prop('checked', false);
        }

        // Select Default Record, If Dropdown Has Only One Record
        let listItemCount = $('#home-branch-id > option').not(':first').length;

        if ($('#dob').val() !== '')
            $('#marital-status').attr('disabled', false);
        else
            $('#marital-status').attr('disabled', true);

        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();
    }


    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@


    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;
        //let personId = $('#person-id option:selected').val();
        //if ($('form').valid() && isValidPancardNumber && isValidAadharNumber)
        if ($('form').valid()) {
            debugger;
            // not add event.preventDefault
            $('.lastrow').remove();


            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let personAddressArray = new Array();
            let personBoardOfDirectorRelationArray = new Array();
            let personBorrowingDetailArray = new Array();
            let personChronicDiseaseArray = new Array();
            let contactDetailArray = new Array();
            let personCourtCaseArray = new Array();
            let personCreditRatingArray = new Array();
            let personFamilyDetailArray = new Array();

            let personAdditionalIncomeDetailArray = new Array();
            let personInsuranceDetailArray = new Array();
            let personSocialMediaArray = new Array();
            let personSMSAlertArray = new Array();

            let personFormData;
            personFormData = new FormData($("#form")[0]);

            // Accordion 3 - Home Branch Validation, If Enable
            if (!$('#heading-home-branch').hasClass('d-none')) {
                if (!IsValidHomeBranchAccordionInputs())
                    isValidAllInputs = false;
            }

            // Accordion 4 - Person Additional Detail Validation, If Enable
            if (IsValidAdditionalDetailsAccordionInputs() === false) {
                isValidAllInputs = false;
            }

            // Accordion 5 - Guardian Person Validation, If Enable
            if ($('#guardian-detail-card').hasClass('d-none') === false && $('#guardian-detail-card').length > 0)
            {
                if (IsValidGuardianAccordionInputs() === false)
                    isValidAllInputs = false;
            }

            // Accordion 6 - Person gst-registration Validation, If Enable
            if ($('#enable-gst-registration-details').is(':checked')) {
                if (IsValidGstRegistrationAccordionInputs() === false)
                    isValidAllInputs = false;
            }


            // Accordion 6 - Photo Sign Validation, If Enable
            if ($('.individual').hasClass('d-none') === false)
            {
                if ($('#heading-person-photo-sign').hasClass('d-none') === false)
                {
                    // Validate photo and sign 
                    if (IsValidPhoto() === false) {
                        isValidAllInputs = false;
                    }

                    if (IsValidSign() === false) {
                        isValidAllInputs = false;
                    }
                }
            }

            // Accordion 6 - Person Commodities Asset Validation, If Enable
            if ($('.individual').hasClass('d-none') === false) {
                if (!$('#heading-commodities-asset').hasClass('d-none')) {
                    IsValidCommoditiesAssetAccordionInputs()
                }
            }

            // Create Array For person address detail Table To Pass Data
            if (!$('#heading-address-details').hasClass('d-none')) {

                if (addressDataTable.data().any()) {

                    $('#address-details-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-person-address > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (addressDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                personAddressArray.push(
                                {
                                    'AddressTypeId': columnValues[1],
                                    'FlatDoorNo': columnValues[3],
                                    'TransFlatDoorNo': columnValues[4],
                                    'NameOfBuilding': columnValues[5],
                                    'TransNameOfBuilding': columnValues[6],
                                    'NameOfRoad': columnValues[7],
                                    'TransNameOfRoad': columnValues[8],
                                    'NameOfArea': columnValues[9],
                                    'TransNameOfArea': columnValues[10],
                                    'CityId': columnValues[11],
                                    'ResidenceTypeId': columnValues[13],
                                    'OwnershipTypeId': columnValues[15],
                                    'Note': columnValues[17],
                                    'TransNote': columnValues[18],
                                    'ReasonForModification': columnValues[19],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#address-details-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // 2. Contact Detail - Create Array For contact Data Table To Pass Data
            if (!$('#heading-contact-details').hasClass('d-none')) {
                if (contactDataTable.data().any()) {
                    $('#contact-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {
                        // Get Data Table Values In Turn Over Limit Array
                        $('#tbl-contact > TBODY > TR').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (contactDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                contactDetailArray.push({
                                    'ContactTypeId': columnValues[1],
                                    'FieldValue': columnValues[3],
                                    'IsVerified': columnValues[4],
                                    'VerificationCode': columnValues[5],
                                    'Note': columnValues[6],
                                    'ReasonForModification': columnValues[7],
                                    'PersonContactDetailPrmKey': columnValues[8],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#contact-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For person address detail Table To Pass Data
            if (!$('#heading-kyc-document').hasClass('d-none')) {
                if (personKycDataTable.data().any()) {

                    $('#kyc-document-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-kyc-document > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (personKycDataTable.row(currentRow).data());

                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }
                            else {

                                let row = $(this);
                                personFormData.append("_kYCDocument[" + i + "].DocumentTypeId", columnValues[1]);
                                personFormData.append("_kYCDocument[" + i + "].DocumentDocumentTypeId", columnValues[3]);
                                personFormData.append("_kYCDocument[" + i + "].NameAsPerDocument", columnValues[5]);
                                personFormData.append("_kYCDocument[" + i + "].DocumentNumber", columnValues[6]);
                                personFormData.append("_kYCDocument[" + i + "].SequenceNumber", columnValues[7]);
                                personFormData.append("_kYCDocument[" + i + "].DateOfIssue", columnValues[8]);
                                personFormData.append("_kYCDocument[" + i + "].DateOfExpiry", columnValues[9]);
                                personFormData.append("_kYCDocument[" + i + "].IssuingAuthority", columnValues[10]);
                                personFormData.append("_kYCDocument[" + i + "].PlaceOfIssue", columnValues[11]);
                                personFormData.append("_kYCDocument[" + i + "].DateOfRequest", columnValues[12]);
                                personFormData.append("_kYCDocument[" + i + "].DateOfExpectingSubmit", columnValues[13]);
                                personFormData.append("_kYCDocument[" + i + "].DateOfSubmit", columnValues[14]);
                                personFormData.append("_kYCDocument[" + i + "].DocumentUploadStatus", columnValues[15]);
                                personFormData.append("_kYCDocument[" + i + "].PhotoPathKYC", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_kYCDocument[" + i + "].FileCaption", columnValues[19]);
                                personFormData.append("_kYCDocument[" + i + "].Note", columnValues[20]);
                                personFormData.append("_kYCDocument[" + i + "].ReasonForModification", columnValues[21]);
                                personFormData.append("_kYCDocument[" + i + "].NameOfFile", columnValues[22]);
                                personFormData.append("_kYCDocument[" + i + "].personKYCDetailDocumentPrmKey", columnValues[23]);
                                personFormData.append("_kYCDocument[" + i + "].LocalStoragePath", columnValues[24]);
                            }

                        });
                    }
                }
                else {
                    $('#kyc-document-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For person family detail Table To Pass Data
            if ($('.individual').hasClass('d-none') === false) {
                if (!$('#heading-family-detail').hasClass('d-none')) {

                    if (personFamilyDataTable.data().any()) {

                        $('#family-detail-data-table-error').addClass('d-none');

                        if (isValidAllInputs) {

                            $('#tbl-family-detail > tbody > tr').each(function () {
                                currentRow = $(this).closest('tr');

                                columnValues = (personFamilyDataTable.row(currentRow).data());

                                // Handling Code If Row Is Undefined Or Null
                                if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                    personFamilyDetailArray.push(
                                     {
                                         'PersonInformationNumber': columnValues[1],
                                         'FullNameOfFamilyMember': columnValues[3],
                                         'TransFullNameOfFamilyMember': columnValues[4],
                                         'RelationId': columnValues[5],
                                         'BirthDate': columnValues[7],
                                         'OccupationId': columnValues[8],
                                         'Income': columnValues[10],
                                         'Note': columnValues[11],
                                         'TransNote': columnValues[12],
                                         'ReasonForModification': columnValues[13],
                                     });
                                }
                                else
                                    return false;
                            });
                        }
                    }
                    //else {
                    //    $('#family-detail-data-table-error').removeClass('d-none');
                    //    isValidAllInputs = false;
                    //}
                }
            }
            // Create Array For person board of director Authorized Table To Pass Data
            if (!$('#heading-person-group-authorized-signatory').hasClass('d-none')) {
                if (authorizedSignatoryDataTable.data().any()) {

                    $('#authorized-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-authorized-signatory > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (authorizedSignatoryDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }
                            else {

                                personFormData.append("_groupAuthorizedSignatory[" + i + "].PersonInformationNumber", columnValues[1]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].FullNameOfAuthorizedPerson", columnValues[3]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].TransFullNameOfAuthorizedPerson", columnValues[4]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].AuthorizedPersonAddressDetail", columnValues[5]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].TransAuthorizedPersonAddressDetail", columnValues[6]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].AuthorizedPersonContactDetail", columnValues[7]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].TransAuthorizedPersonContactDetail", columnValues[8]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].DesignationId", columnValues[9]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].IsAuthorizedSignatory", columnValues[11]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].PhotoPathSign", $(currentRow).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].SignFileCaption", columnValues[14]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].Note", columnValues[15]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].TransNote", columnValues[16]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].ReasonForModification", columnValues[17]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].SignNameOfFile", columnValues[18]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].PersonGroupAuthorizedSignatoryPrmKey", columnValues[19]);
                                personFormData.append("_groupAuthorizedSignatory[" + i + "].SignLocalStoragePath", columnValues[20]);
                            }
                        });
                    }
                } else {
                    $('#authorized-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For person board of director relation Table To Pass Data
            if (!$('#heading-relation').hasClass('d-none')) {

                if (boardOfDirectorDataTable.data().any()) {

                    $('#relation-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {


                        $('#tbl-relation > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (boardOfDirectorDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                personBoardOfDirectorRelationArray.push(
                                 {
                                     'BoardOfDirectorId': columnValues[1],
                                     'RelationId': columnValues[3],
                                     'Note': columnValues[5],
                                     'ReasonForModification': columnValues[6]
                                 });
                            }
                            else
                                return false;
                        });
                    }
                }
                //else {
                //    isValidAllInputs = false;

                //}
            }

            // Create Array For person bank detail Table To Pass Data
            if (!$('#heading-bank-detail').hasClass('d-none')) {
                if (bankDataTable.data().any()) {

                    $('#bank-detail-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-bank-detail > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (bankDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }
                            else {
                                let row = $(this);
                                personFormData.append("_bankDetail[" + i + "].BankId", columnValues[1]);
                                personFormData.append("_bankDetail[" + i + "].BankBranchId", columnValues[3]);
                                personFormData.append("_bankDetail[" + i + "].AccountNumber", columnValues[5]);
                                personFormData.append("_bankDetail[" + i + "].OpeningDate", columnValues[6]);
                                personFormData.append("_bankDetail[" + i + "].CloseDate", columnValues[7]);
                                personFormData.append("_bankDetail[" + i + "].IsDefaultBankForTransaction", columnValues[8]);
                                personFormData.append("_bankDetail[" + i + "].PhotoPathBank", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_bankDetail[" + i + "].FileCaption", columnValues[11]);
                                personFormData.append("_bankDetail[" + i + "].Note", columnValues[12]);
                                personFormData.append("_bankDetail[" + i + "].ReasonForModification", columnValues[13]);
                                personFormData.append("_bankDetail[" + i + "].NameOfFile", columnValues[14]);
                                personFormData.append("_bankDetail[" + i + "].PersonBankDetailDocumentPrmKey", columnValues[15]);
                                personFormData.append("_bankDetail[" + i + "].LocalStoragePath", columnValues[16]);
                            }

                        });
                    }
                }
                //else {
                //    $('#bank-detail-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;
                //}
            }

            // Create Array For person GST registration Table To Pass Data
            if ($('#enable-gst-return-document').is(':checked')) {
                debugger
                if (gstDataTable.data().any()) {
                    debugger
                    $('#gst-registration-accordion-error').addClass('d-none');
                    debugger
                    if (isValidAllInputs) {
                        debugger
                        $('#tbl-gst-registration > tbody > tr').each(function (i) {
                            debugger
                            currentRow = $(this).closest('tr');

                            columnValues = (gstDataTable.row(currentRow).data());
                            debugger
                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }
                            else {
                                debugger
                                let row = $(this);
                                personFormData.append("_gSTRegistrationDetail[" + i + "].AssessmentYear", columnValues[1]);
                                personFormData.append("_gSTRegistrationDetail[" + i + "].TaxAmount", columnValues[2]);
                                personFormData.append("_gSTRegistrationDetail[" + i + "].PhotoPathGst", $(row).find('TD').find('input[type="file"]').get(0).files[0]);
                                personFormData.append("_gSTRegistrationDetail[" + i + "].FileCaption", columnValues[5]);
                                personFormData.append("_gSTRegistrationDetail[" + i + "].Note", columnValues[6]);
                                personFormData.append("_gSTRegistrationDetail[" + i + "].ReasonForModification", columnValues[7]);
                                personFormData.append("_gSTRegistrationDetail[" + i + "].NameOfFile", columnValues[8]);
                                personFormData.append("_gSTRegistrationDetail[" + i + "].PersonGSTReturnDocumentPrmKey", columnValues[9]);
                                personFormData.append("_gSTRegistrationDetail[" + i + "].LocalStoragePath", columnValues[10]);
                            }
                        });
                    }
                }
                else {
                    $('#gst-registration-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;

                }
            }

            // Create Array For person chronic disease Table To Pass Data
            if ($('.individual').hasClass('d-none') === false) {
                if (!$('#heading-chronic-disease').hasClass('d-none')) {

                    if (chronicDataTable.data().any()) {

                        $('#chronic-disease-data-table-error').addClass('d-none');

                        if (isValidAllInputs) {

                            $('#tbl-chronic-disease > tbody > tr').each(function () {
                                currentRow = $(this).closest('tr');

                                columnValues = (chronicDataTable.row(currentRow).data());

                                // Handling Code If Row Is Undefined Or Null
                                if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                    personChronicDiseaseArray.push(
                                      {
                                          'DiseaseId': columnValues[1],
                                          'OtherDetails': columnValues[3],
                                          'Note': columnValues[4],
                                          'ReasonForModification': columnValues[5],

                                      });
                                }
                                else
                                    return false;
                            });
                        }
                    }
                    //else {
                    //    $('#chronic-disease-data-table-error').removeClass('d-none');
                    //    isValidAllInputs = false;

                    //}
                }
            }
            // Create Array For person insurance detail Table To Pass Data
            if (!$('#heading-insurance-detail').hasClass('d-none')) {

                if (insuranceDataTable.data().any()) {

                    $('#insurance-detail-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-insurance-detail > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (insuranceDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                personInsuranceDetailArray.push(
                                {
                                    'InsuranceTypeId': columnValues[1],
                                    'InsuranceCompanyId': columnValues[3],
                                    'StartDate': columnValues[5],
                                    'MaturityDate': columnValues[6],
                                    'CloseDate': columnValues[7],
                                    'PolicyNumber': columnValues[8],
                                    'PolicyPremium': columnValues[9],
                                    'PolicySumAssured': columnValues[10],
                                    'OverduesPremium': columnValues[11],
                                    'HasAnyMortgage': columnValues[12],
                                    'Note': columnValues[13],
                                    'ReasonForModification': columnValues[14]
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                //else {
                //    $('#insurance-detail-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;

                //}
            }

            // Create Array For person financial asset Table To Pass Data
            if (!$('#heading-financial-asset').hasClass('d-none')) {

                if (financialDataTable.data().any()) {

                    $('#financial-asset-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-financial-asset > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (financialDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }
                            else {
                                let row = $(this);
                                personFormData.append("_financialAsset[" + i + "].FinancialOrganizationTypeId", columnValues[1]);
                                personFormData.append("_financialAsset[" + i + "].NameOfFinancialOrganization", columnValues[3]);
                                personFormData.append("_financialAsset[" + i + "].TransNameOfFinancialOrganization", columnValues[4]);
                                personFormData.append("_financialAsset[" + i + "].NameOfBranch", columnValues[5]);
                                personFormData.append("_financialAsset[" + i + "].TransNameOfBranch", columnValues[6]);
                                personFormData.append("_financialAsset[" + i + "].AddressDetails", columnValues[7]);
                                personFormData.append("_financialAsset[" + i + "].TransAddressDetails", columnValues[8]);
                                personFormData.append("_financialAsset[" + i + "].ContactDetails", columnValues[9]);
                                personFormData.append("_financialAsset[" + i + "].TransContactDetails", columnValues[10]);
                                personFormData.append("_financialAsset[" + i + "].OpeningDate", columnValues[11]);
                                personFormData.append("_financialAsset[" + i + "].MaturityDate", columnValues[12]);
                                personFormData.append("_financialAsset[" + i + "].FinancialAssetTypeId", columnValues[13]);
                                personFormData.append("_financialAsset[" + i + "].FinancialAssetDescription", columnValues[15]);
                                personFormData.append("_financialAsset[" + i + "].TransFinancialAssetDescription", columnValues[16]);
                                personFormData.append("_financialAsset[" + i + "].ReferenceNumber", columnValues[17]);
                                personFormData.append("_financialAsset[" + i + "].TransReferenceNumber", columnValues[18]);
                                personFormData.append("_financialAsset[" + i + "].InvestedAmount", columnValues[19]);
                                personFormData.append("_financialAsset[" + i + "].MonthlyInterestIncomeAmount", columnValues[20]);
                                personFormData.append("_financialAsset[" + i + "].CurrentMarketValue", columnValues[21]);
                                personFormData.append("_financialAsset[" + i + "].HasAnyMortgage", columnValues[22]);
                                personFormData.append("_financialAsset[" + i + "].PhotoPathFinance", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_financialAsset[" + i + "].FileCaption", columnValues[25]);
                                personFormData.append("_financialAsset[" + i + "].Note", columnValues[26]);
                                personFormData.append("_financialAsset[" + i + "].TransNote", columnValues[27]);
                                personFormData.append("_financialAsset[" + i + "].ReasonForModification", columnValues[28]);
                                personFormData.append("_financialAsset[" + i + "].NameOfFile", columnValues[29]);
                                personFormData.append("_financialAsset[" + i + "].PersonFinancialAssetDocumentPrmKey", columnValues[30]);
                                personFormData.append("_financialAsset[" + i + "].LocalStoragePath", columnValues[31]);
                            }
                        });
                    }
                }
                //else {
                //    $('#financial-asset-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;


                //}
            }

            // Create Array For person movable asset Table To Pass Data
            if (!$('#heading-movable-asset').hasClass('d-none')) {

                if (movableDataTable.data().any()) {

                    $('#movable-asset-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {
                        debugger
                        $('#tbl-movable-asset > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (movableDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {

                                return false;
                            }
                            else {
                                personFormData.append("_movableAsset[" + i + "].VehicleVariantId", columnValues[5]);
                                personFormData.append("_movableAsset[" + i + "].NumberOfOwners", columnValues[7]);
                                personFormData.append("_movableAsset[" + i + "].ManufacturingYear", columnValues[8]);
                                personFormData.append("_movableAsset[" + i + "].PurchaseDate", columnValues[9]);
                                personFormData.append("_movableAsset[" + i + "].RegistrationDate", columnValues[10]);
                                personFormData.append("_movableAsset[" + i + "].RegistrationNumber", columnValues[11]);
                                personFormData.append("_movableAsset[" + i + "].PurchasePrice", columnValues[12]);
                                personFormData.append("_movableAsset[" + i + "].CurrentMarketValue", columnValues[13]);
                                personFormData.append("_movableAsset[" + i + "].OwnershipPercentage", columnValues[14]);
                                personFormData.append("_movableAsset[" + i + "].HasAnyMortgage", columnValues[15]);
                                personFormData.append("_movableAsset[" + i + "].IsOwnershipDeceased", columnValues[16]);
                                personFormData.append("_movableAsset[" + i + "].PhotoPathMovable", $(currentRow).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_movableAsset[" + i + "].FileCaption", columnValues[19]);
                                personFormData.append("_movableAsset[" + i + "].Note", columnValues[20]);
                                personFormData.append("_movableAsset[" + i + "].ReasonForModification", columnValues[21]);
                                personFormData.append("_movableAsset[" + i + "].NameOfFile", columnValues[22]);
                                personFormData.append("_movableAsset[" + i + "].PersonMovableAssetDocumentPrmKey", columnValues[23]);
                                personFormData.append("_movableAsset[" + i + "].LocalStoragePath", columnValues[24]);
                            }


                        });
                    }
                }
                //else {
                //    $('#movable-asset-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;

                //}
            }

            // Create Array For person immovable asset Table To Pass Data
            if (!$('#heading-immovable-asset').hasClass('d-none')) {

                if (immovableDataTable.data().any()) {

                    $('#immovable-asset-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-immovable-asset > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (immovableDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }

                            else {

                                personFormData.append("_immovableAsset[" + i + "].SurveyNumber", columnValues[1]);
                                personFormData.append("_immovableAsset[" + i + "].CitySurveyNumber", columnValues[2]);
                                personFormData.append("_immovableAsset[" + i + "].OtherNumber", columnValues[3]);
                                personFormData.append("_immovableAsset[" + i + "].AreaOfLand", columnValues[4]);
                                personFormData.append("_immovableAsset[" + i + "].ConstructionArea", columnValues[5]);
                                personFormData.append("_immovableAsset[" + i + "].CarpetArea", columnValues[6]);
                                personFormData.append("_immovableAsset[" + i + "].CurrentMarketValue", columnValues[7]);
                                personFormData.append("_immovableAsset[" + i + "].AnnualRentIncome", columnValues[8]);
                                personFormData.append("_immovableAsset[" + i + "].ResidenceTypeId", columnValues[9]);
                                personFormData.append("_immovableAsset[" + i + "].OwnershipTypeId", columnValues[11]);
                                personFormData.append("_immovableAsset[" + i + "].OwnershipPercentage", columnValues[13]);
                                personFormData.append("_immovableAsset[" + i + "].IsConstructed", columnValues[14]);
                                personFormData.append("_immovableAsset[" + i + "].HasAnyMortgage", columnValues[15]);
                                personFormData.append("_immovableAsset[" + i + "].IsOwnershipDeceased", columnValues[16]);
                                personFormData.append("_immovableAsset[" + i + "].PhotoPathImmovable", $(currentRow).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_immovableAsset[" + i + "].FileCaption", columnValues[19]);
                                personFormData.append("_immovableAsset[" + i + "].AssetFullDescription", columnValues[20]);
                                personFormData.append("_immovableAsset[" + i + "].Note", columnValues[21]);
                                personFormData.append("_immovableAsset[" + i + "].ReasonForModification", columnValues[22]);
                                personFormData.append("_immovableAsset[" + i + "].NameOfFile", columnValues[23]);
                                personFormData.append("_immovableAsset[" + i + "].PersonImmovableAssetDocumentPrmKey", columnValues[24]);
                                personFormData.append("_immovableAsset[" + i + "].LocalStoragePath", columnValues[25]);
                            }

                        });
                    }
                }
                //else {
                //    $('#immovable-asset-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;

                //}
            }

            // Create Array For person agriculture asset Table To Pass Data
            if (!$('#heading-agriculture-asset').hasClass('d-none')) {
                if (agricultureDataTable.data().any()) {

                    $('#agriculture-asset-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-agriculture-asset > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (agricultureDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }
                            else {
                                let row = $(this);
                                personFormData.append("_agricultureAsset[" + i + "].AgricultureLandTypeId", columnValues[1]);
                                personFormData.append("_agricultureAsset[" + i + "].AgricultureLandDescription", columnValues[3]);
                                personFormData.append("_agricultureAsset[" + i + "].SurveyNumber", columnValues[4]);
                                personFormData.append("_agricultureAsset[" + i + "].GroupNumber", columnValues[5]);
                                personFormData.append("_agricultureAsset[" + i + "].AreaOfLand", columnValues[6]);
                                personFormData.append("_agricultureAsset[" + i + "].Volume", columnValues[7]);
                                personFormData.append("_agricultureAsset[" + i + "].OwnershipTypeId", columnValues[8]);
                                personFormData.append("_agricultureAsset[" + i + "].OwnershipPercentage", columnValues[10]);
                                personFormData.append("_agricultureAsset[" + i + "].CurrentMarketValue", columnValues[11]);
                                personFormData.append("_agricultureAsset[" + i + "].AnnualIncomeFromLand", columnValues[12]);
                                personFormData.append("_agricultureAsset[" + i + "].HasAnyCourtCase", columnValues[13]);
                                personFormData.append("_agricultureAsset[" + i + "].CourtCaseFullDetails", columnValues[14]);
                                personFormData.append("_agricultureAsset[" + i + "].IsOnlyRainFedTypeIrrigation", columnValues[15]);
                                personFormData.append("_agricultureAsset[" + i + "].HasCanalRiverIrrigationSource", columnValues[16]);
                                personFormData.append("_agricultureAsset[" + i + "].HasWellsIrrigationSource", columnValues[17]);
                                personFormData.append("_agricultureAsset[" + i + "].HasFarmLakeSource", columnValues[18]);
                                personFormData.append("_agricultureAsset[" + i + "].HasAnyMortgage", columnValues[19]);
                                personFormData.append("_agricultureAsset[" + i + "].IsOwnershipDeceased", columnValues[20]);
                                personFormData.append("_agricultureAsset[" + i + "].PhotoPathAgree", $(row).find('TD').find('input[type="file" ]').get(0).files[0]);
                                personFormData.append("_agricultureAsset[" + i + "].FileCaption", columnValues[23]);
                                personFormData.append("_agricultureAsset[" + i + "].Note", columnValues[24]);
                                personFormData.append("_agricultureAsset[" + i + "].ReasonForModification", columnValues[25]);
                                personFormData.append("_agricultureAsset[" + i + "].NameOfFile", columnValues[26]);
                                personFormData.append("_agricultureAsset[" + i + "].PersonAgricultureAssetDocumentPrmKey", columnValues[27]);
                                personFormData.append("_agricultureAsset[" + i + "].LocalStoragePath", columnValues[28]);
                            }
                        });
                    }
                }
                //else {
                //    $('#agriculture-asset-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;
                //}
            }

            // Create Array For person machinery asset Table To Pass Data
            if (!$('#heading-machinery-asset').hasClass('d-none')) {

                if (machineryDataTable.data().any()) {

                    $('#machinery-asset-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-machinery-asset > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (machineryDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }
                            else {
                                debugger;

                                personFormData.append("_machineryAsset[" + i + "].NameOfMachinery", columnValues[1]);
                                personFormData.append("_machineryAsset[" + i + "].MachineryFullDetails", columnValues[2]);
                                personFormData.append("_machineryAsset[" + i + "].ManufacturingYear", columnValues[3]);
                                personFormData.append("_machineryAsset[" + i + "].DateOfPurchase", columnValues[4]);
                                personFormData.append("_machineryAsset[" + i + "].NumberOfOwners", columnValues[5]);
                                personFormData.append("_machineryAsset[" + i + "].ReferenceNumber", columnValues[6]);
                                personFormData.append("_machineryAsset[" + i + "].PurchasePrice", columnValues[7]);
                                personFormData.append("_machineryAsset[" + i + "].CurrentMarketValue", columnValues[8]);
                                personFormData.append("_machineryAsset[" + i + "].OwnershipPercentage", columnValues[9]);
                                personFormData.append("_machineryAsset[" + i + "].HasAnyMortgage", columnValues[10]);
                                personFormData.append("_machineryAsset[" + i + "].IsOwnershipDeceased", columnValues[11]);
                                personFormData.append("_machineryAsset[" + i + "].PhotoPathMachinery", $(currentRow).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_machineryAsset[" + i + "].Note", columnValues[15]);
                                personFormData.append("_machineryAsset[" + i + "].ReasonForModification", columnValues[16]);
                                personFormData.append("_machineryAsset[" + i + "].FileCaption", columnValues[14]);
                                personFormData.append("_machineryAsset[" + i + "].NameOfFile", columnValues[17]);
                                personFormData.append("_machineryAsset[" + i + "].PersonMachineryAssetDocumentPrmKey", columnValues[18]);
                                personFormData.append("_machineryAsset[" + i + "].LocalStoragePath", columnValues[19]);
                            }
                        });
                    }
                }
                //else {
                //    $('#machinery-asset-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;

                //}
            }

            // Create Array For person income detail Table To Pass Data
            if ($('.individual').hasClass('d-none') === false) {
                if (!$('#heading-income-details').hasClass('d-none')) {

                    if (incomeDatatable.data().any()) {

                        $('#income-details-data-table-error').addClass('d-none');

                        if (isValidAllInputs) {

                            $('#tbl-income-detail > tbody > tr').each(function () {
                                currentRow = $(this).closest('tr');

                                columnValues = (incomeDatatable.row(currentRow).data());

                                // Handling Code If Row Is Undefined Or Null
                                if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                    personAdditionalIncomeDetailArray.push(
                                    {
                                        'IncomeSourceId': columnValues[1],
                                        'AnnualIncome': columnValues[3],
                                        'OtherDetails': columnValues[4],
                                        'Note': columnValues[5],
                                        'ReasonForModification': columnValues[6]
                                    });
                                }
                                else
                                    return false;
                            });
                        }
                    }
                    //else {
                    //    $('#income-details-data-table-error').removeClass('d-none');
                    //    isValidAllInputs = false;
                    //}
                }
            }
            // Create Array For person borrowing detail Table To Pass Data
            if (!$('#heading-borrowing-detail').hasClass('d-none')) {
                debugger
                if (borrowingDetailDataTable.data().any()) {

                    $('#borrowing-detail-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-borrowing-detail > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (borrowingDetailDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                personBorrowingDetailArray.push(
                                {
                                    'NameOfOrganization': columnValues[1],
                                    'TransNameOfOrganization': columnValues[2],
                                    'Branch': columnValues[3],
                                    'TransBranch': columnValues[4],
                                    'ReferenceNumber': columnValues[5],
                                    'OpeningDate': columnValues[6],
                                    'MatureDate': columnValues[7],
                                    'CloseDate': columnValues[8],
                                    'LoanDetails': columnValues[9],
                                    'TransLoanDetails': columnValues[10],
                                    'MortgageDetails': columnValues[11],
                                    'TransMortgageDetails': columnValues[12],
                                    'MortgageAmount': columnValues[13],
                                    'SanctionLoanAmount': columnValues[14],
                                    'InstallmentAmount': columnValues[15],
                                    'LoanBalanceAmount': columnValues[16],
                                    'OverduesInstallment': columnValues[17],
                                    'OverduesAmount': columnValues[18],
                                    'IsTakingAnyCourtAction': columnValues[19],
                                    'CourtCaseTypeId': columnValues[20],
                                    'FilingDate': columnValues[22],
                                    'FilingNumber': columnValues[23],
                                    'RegistrationDate': columnValues[24],
                                    'RegistrationNumber': columnValues[25],
                                    'CNRNumber': columnValues[26],
                                    'CourtCaseStageId': columnValues[27],
                                    'Note': columnValues[29],
                                    'TransNote': columnValues[30],
                                    'ReasonForModification': columnValues[31]
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                //else {
                //    $('#borrowing-detail-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;

                //}
            }

            // Create Array For person credit rating Table To Pass Data
            if (!$('#heading-credit-rating').hasClass('d-none')) {
                debugger
                if (creditDataTable.data().any()) {

                    $('#credit-rating-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-credit-rating > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (creditDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                personCreditRatingArray.push(
                                {
                                    'EffectiveDate': columnValues[1],
                                    'CreditBureauAgencyId': columnValues[2],
                                    'Score': columnValues[4],
                                    'Note': columnValues[5],
                                    'ReasonForModification': columnValues[6]
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                //else {
                //    $('#credit-rating-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;
                //}
            }

            // Create Array For person court case Table To Pass Data
            if (!$('#heading-court-case').hasClass('d-none')) {

                if (courtCaseDataTable.data().any()) {

                    $('#court-case-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-court-case > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (courtCaseDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                personCourtCaseArray.push(
                                {
                                    'CourtCaseTypeId': columnValues[1],
                                    'FilingDate': columnValues[3],
                                    'FilingNumber': columnValues[4],
                                    'RegistrationDate': columnValues[5],
                                    'RegistrationNumber': columnValues[6],
                                    'CNRNumber': columnValues[7],
                                    'AmountOfDecree': columnValues[8],
                                    'CollateralAmount': columnValues[9],
                                    'CourtCaseStageId': columnValues[10],
                                    'Note': columnValues[12],
                                    'ReasonForModification': columnValues[13]
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                //else {
                //    $('#court-case-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;
                //}
            }

            // Create Array For person income tax detail case Table To Pass Data
            if (!$('#heading-income-tax').hasClass('d-none')) {

                if (incomeTaxDataTable.data().any()) {

                    $('#income-tax-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-income-tax > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (incomeTaxDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }
                            else {

                                personFormData.append("_incomeTaxDetail[" + i + "].AssessmentYear", columnValues[1]);
                                personFormData.append("_incomeTaxDetail[" + i + "].TaxAmount", columnValues[2]);
                                personFormData.append("_incomeTaxDetail[" + i + "].PhotoPathTax", $(currentRow).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_incomeTaxDetail[" + i + "].FileCaption", columnValues[5]);
                                personFormData.append("_incomeTaxDetail[" + i + "].Note", columnValues[6]);
                                personFormData.append("_incomeTaxDetail[" + i + "].ReasonForModification", columnValues[7]);
                                personFormData.append("_incomeTaxDetail[" + i + "].NameOfFile", columnValues[8]);
                                personFormData.append("_incomeTaxDetail[" + i + "].PersonIncomeTaxDetailDocumentPrmKey", columnValues[9]);
                                personFormData.append("_incomeTaxDetail[" + i + "].LocalStoragePath", columnValues[10]); personFormData.append("_incomeTaxDetail[" + i + "].LocalStoragePath", columnValues[10]);
                            } {
                                row = $(this);

                                personFormData.append("_PersonIncomeTaxDetail[" + i + "].AssessmentYear", columnValues[1]);
                                personFormData.append("_PersonIncomeTaxDetail[" + i + "].TaxAmount", columnValues[2]);
                                personFormData.append("_PersonIncomeTaxDetail[" + i + "].PhotoPathTax", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_PersonIncomeTaxDetail[" + i + "].AssesmentOrderLocalStoragePath", $('#TaxStoaragePath').val());
                                personFormData.append("_PersonIncomeTaxDetail[" + i + "].FileCaption", columnValues[5]);
                                personFormData.append("_PersonIncomeTaxDetail[" + i + "].Note", columnValues[6]);
                                personFormData.append("_PersonIncomeTaxDetail[" + i + "].ReasonForModification", columnValues[7]);
                                personFormData.append("_PersonIncomeTaxDetail[" + i + "].PersonIncomeTaxDetailPrmKey", columnValues[8]);
                            }
                        });
                    }
                }
                //else {
                //    $('#income-tax-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;
                //}
            }

            // Create Array For person sms alert case Table To Pass Data
            if (!$('#heading-sms-alert').hasClass('d-none')) {

                if (smsAlertDataTable.data().any()) {

                    $('#sms-alert-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-sms-alert > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (smsAlertDataTable.row(currentRow).data());

                            [time, meridian] = columnValues[5].split(' ');
                            [hours, minutes] = time.split(':');

                            if (hours === '12') {
                                hours = '00';
                            }

                            if (meridian === 'PM')
                                hours = parseInt(hours, 10) + 12;

                            sendingTime = hours + ':' + minutes;

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                debugger;
                                personSMSAlertArray.push(
                                    {
                                        'PersonInformationParameterNoticeTypeId': columnValues[1],
                                        'AppLanguageId': columnValues[3],
                                        'SendingTime': sendingTime,
                                        'Note': columnValues[6],
                                        'ReasonForModification': columnValues[7]
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
                //else {
                //    $('#sms-alert-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;
                //}
            }

            // Create Array For person social media case Table To Pass Data
            if (!$('#heading-social-media').hasClass('d-none')) {
                if (socialMediaDataTable.data().any()) {

                    $('#social-media-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-social-media > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (socialMediaDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                personSocialMediaArray.push(
                                {
                                    'SocialMediaId': columnValues[1],
                                    'SocialMediaLink': columnValues[3],
                                    'OtherDetails': columnValues[4],
                                    'Note': columnValues[5],
                                    'ReasonForModification': columnValues[6]
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                //else {
                //    $('#social-media-data-table-error').removeClass('d-none');
                //    isValidAllInputs = false;
                //}
            }

            // Call Controller Save Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: personDataTable,
                    type: 'POST',
                    async: false,
                    data: {
                        '_address': personAddressArray,
                        '_boardOfDirectorRelation': personBoardOfDirectorRelationArray,
                        '_creditRating': personCreditRatingArray,
                        '_borrowingDetail': personBorrowingDetailArray,
                        '_chronicDisease': personChronicDiseaseArray,
                        '_contactDetail': contactDetailArray,
                        '_courtCase': personCourtCaseArray,
                        '_familyDetail': personFamilyDetailArray,
                        '_incomeDetails': personAdditionalIncomeDetailArray,
                        '_insurance': personInsuranceDetailArray,
                        '_sMSAlert': personSMSAlertArray,
                        '_socialMedia': personSocialMediaArray
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                        debugger;
                        PersonImagesDataTable();
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Person DataTable!!! Error Message - ' + error.toString());
                    }
                })
            }

            else {
                alert('Oops, Something Went Wrong!, Please Provide Valid Data........');
                event.preventDefault();
            }

            //PersonAgricultureAsset
            function PersonImagesDataTable() {
                debugger;
                $.ajax({
                    url: PersonImageDataTable,
                    type: 'POST',
                    async: false,
                    data: personFormData,
                    contentType: false, // Not to set any content header 
                    processData: false, // Not to process data 
                    cache: false,
                    //ContentType: "application/json; charset=utf-8",
                    dataType: 'JSON',
                    enctype: 'multipart/form-data',

                    success: function (data) {

                        alert('Ok');
                    },

                    error: function (xhr, status, error) {

                        alert("An Error Has Occured In Images DataTable!!! Error Message - " + error.toString());
                    }
                });
            }

        }

        else {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }
    });
});