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
$(document).ready(function ()
{
    const INDIVIDUAL = 'INDVL';
    let sysNameOfPersonType = '';

    // DropdownList Variables
    let personDropdownListData = '';
    let personDropdownListDataForFamily = '';

    const ADDRESS_TYPE_DROPDOWN_LIST = $('#address-type-id').html();
    //const DOCUMENT_TYPE_DROPDOWN_LIST = $('#document-document-type-id').html();
    // Array
    let finalDropdownListArray = [];

    // Count
    let dropDownListItemCount = 0;

    
    let identityDocumentType = '';
    // @@@@@@@@@@ Data Table Related letible Declaration
    
    let tag = '';
    let id = '';
    let myModal;
    let rowNum = 0;
    let selectedRowIndex;
    let row;
    let today;
    let age;
    let rowData;
    let checked;
    let columnValues;
    let maritalStatusId;
    let occupationId;
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let meridian;
    let hours;
    let minutes;
    let minimum;
    let maximum;
    let minimumLength = 0;
    let maximumLength = 0;
    let arr = new Array();

    let _occupationType;
    let _maritalStatus;

    let fullname = '';
    let personInfoNumber = 0;
    let inEnglishName = '';
    let inMarathiName = '';

    //PersonAddress
    let result = true;
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
    let isVerified = true;
    let note = '';
    let panCardNumber = '';
    let transNote = '';
    let reasonForModification = '';
    let hasDivClass;
    let personAddressPrmKey = 0;
    let editedAddressTypeId = '';
    let document_Type = '';
    let personTypeId = '';

    //PersonContact
    let contactType;
    let contactTypeText = '';
    let fieldValue;
    let verificationCode = '';
    let contactDetailPrmKey = 0;
    let entryStatus = false;
    let isDuplicateContact = false;
    let isMobile = false;
    let isEmail = false;
    let contact_Type = '';

    //guardian Person
    let guardianPersonInfoId = '';
    let filteredData;
    let filteredDocumentData;

    //PersonKYCDocument
    let kYCDateOfIssueDate = '';
    let kYCExpiryDate = '';
    let kYCDateOfRequestDate = '';
    let kYCDateOfExpectingSubmitDate = '';
    let kYCDateOfSubmitDate = '';
    let storagePathId = '';
    let docPath = '';
    let files;
    let photoinput;
    let documentUploadStatusText;
    let photoPathKYCId = '';
    let photoSrc;
    let storagePathInput = '';
    let dt;
    let j;
    let f;
    let e;
    let rvisibility;
    let document;
    let documentText = '';
    let documentDocumentType;
    let documentDocumentTypeText = '';
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
    let photo;
    let photoPathKYC = '';
    let dochtml;
    let dochtml1;
    let fileCaption;
    let PrmKey = 0;
    let path;
    let i;
    let newID = '';
    let photoID = '';
    let documentList = '';
    let regexForPanCard;
    let regexForAdharCard;
    let documentId = '';
    let isDuplicateSequenceNumber = false;
    let isDuplicateDocument = false;
    let editedSequenceNumber = 0;
    let editedDocument = 0;

    //Person Family Detail
    let personInformationNumber = '';
    let personInformationNumberText = '';
    let fullNameOfFamilyMember = '';
    let transFullNameOfFamilyMember = '';
    let relation;
    let relationText = '';
    let birthDate = '';
    let occupation = '';
    let occupationText = '';
    let income = 0;
    let familyDetailsBirthDate = '';
    let PersonInfoId = '';

    //Board Of Director Authorized Signatory
    let authorizedPersonInformationNumber = '';
    let authorizedPersonInformationNumberText = '';
    let photoPathSign = '';
    let PhotoPathSignId = '';
    let designationId = '';
    let designationIdText = '';
    let fullNameOfAuthorizedPerson = '';
    let transfullNameOfAuthorizedPerson = '';
    let authorizedPersonAddressDetail = '';
    let transAuthorizedPersonAddressDetail = '';
    let authorizedPersonContactDetail = '';
    let transAuthorizedPersonContactDetail = '';
    let IsAuthorizedSignatory = false;

    //Board Of Director Relation
    let boardofdirector = '';
    let boardofdirectorText = '';

    //Person Bank Detail
    let PhotoPathBankId = '';
    let accountopeningDate = '';
    let accountclosingDate = '';
    let bankId = '';
    let bankText = '';
    let bankBranch;
    let bankBranchText = '';
    let accountNumber = 0;
    let openingDate = '';
    let closeDate = '';
    let isDefaultBankForTransaction = false;
    let photoPathBank;
    let branchList;
    let branch;
    let bankBranchId = '';
    let prmKey = 0;

    //GST Registration
    let assessmentYear = 0;
    let taxAmount = 0;
    let photoPathGst;
    let photoPathGstId = '';

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
    let PolicyNumber = 0;
    let PolicyPremium;
    let PolicySumAssured;
    let OverduesPremium;
    let HasAnyMortgage;
    let month;
    let year;
    let day;

    //Financial Asset
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
    let hasAnyMortgage;
    let photoPathFinance;
    let financialAssetOpeningDate = '';
    let financialAssetMatureDate = '';
    let photoPathFinanceId = '';
    let financialOrganizationType = '';
    let financialAssetTypeId = '';

    //Person Movable Asset
    let vehicleModelId = '';
    let vehicleModelIdText = '';
    let vehicleletiant;
    let vehicleletiantText = '';
    let manufacturingYear;
    let numberOfOwners = 0;
    let registrationDate = '';
    let registrationNumber = '';
    let dateOfPurchase = '';
    let purchasePrice = '';
    let ownershipPercentage = 0;
    let isOwnershipDeceased = false;
    let photoPathMovable;
    let movableAssetRegistrationDate = '';
    let movableDateOfPurchase = '';
    let letiantList = '';
    let photoPathMovableId = '';
    let letiant = '';
    let inputvalues = 0;
    let date = '';
    let registrationYear;
    let regex;
    let vehicleletiantId = '';

    //Person Immovable Asset
    let photoPathImmovable;
    let photoPathImmovableId = '';
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
    let residenceTypeId = '';
    let ownershipTypeId = '';

    //Person Agriculture Asset
    let agricultureLandTypeId = '';
    let agricultureLandTypeText = '';
    let agricultureLandDescription = '';
    let groupNumber = 0;
    let volume = 0;
    let isOnlyRainFedTypeIrrigation = false;
    let hasCanalRiverIrrigationSource;
    let hasWellsIrrigationSource;
    let hasFarmLakeSource;
    let annualIncomeFromLand;
    let hasAnyCourtCase = false;
    let courtCaseFullDetails = '';
    let photoPathAgree;
    let start = '';
    let photoPathAgreeId = '';
    let mul;
    let end;
    let oldId;
    let slicedFile;

    //person Machinery Asset
    let nameOfMachinery = '';
    let machineryFullDetails = '';
    let photoPathMachinery;
    let machineryDateOfPurchase = '';
    let photoPathMachineryId = '';

    // income Detail
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
    let fillingDate = '';
    let sactionAmount = 0;
    let closingDate = '';

    // Credit Rating
    let effectiveDate = '';
    let agency;
    let agencyText = '';
    let score = 0;

    // Court Case
    let courtCaseTypeId = '';
    let courtCaseTypeIdText = '';
    let cnrNumber = 0;
    let amountOfDecree = 0;
    let collateralAmount = 0;
    let courtCaseStageId = '';
    let courtCaseStageIdText = '';
    let creditRatingEffectiveDate;

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
    
    // Create DataTables
    let addressDataTable = CreateDataTable('person-address');
    let contactDataTable = CreateDataTable('contact');
    let personKycDataTable = CreateDataTable('kyc-document');
    let personFamilyDataTable = CreateDataTable('family-detail');
    let boardOfDirectorAuthorizedDataTable = CreateDataTable('authorized-signatory');
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
    let borrowingDataTable = CreateDataTable('borrowing-detail');
    let creditDataTable = CreateDataTable('credit-rating');
    let courtCaseDataTable = CreateDataTable('court-case');
    let socialMediaDataTable = CreateDataTable('social-media');
    let smsAlertDataTable = CreateDataTable('sms-alert');
    let incomeTaxDataTable = CreateDataTable('income-tax');

    SetPageLoadingDefaultValues();


    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Define the GSTRegistration function
    function GSTRegistration() {
        let fullInput = ''; // Define fullInput variable outside the event handlers

        // Validation GST Registration
        $('#gst-registration-number').keydown(function (e) {
            
            let input = $(this).val().toUpperCase();
            let errorMessage = '';

            // Capture the key that was pressed
            let inputChar = String.fromCharCode(e.which).toUpperCase();

            // Combine current input with the new character for validation
            fullInput = input + inputChar; // Update fullInput


            // Check for backspace key
            if (e.key === 'Backspace') {
                // If backspace is pressed, clear any errors and allow default behavior
                $('#gst-registration-number-error').addClass('d-none');
                return true;
            }

            // Check for arrow keys and allow default behavior
            if (e.which === 37 || e.which === 39) { // Left or Right Arrow
                return true;
            }

            // If the input length is exactly 2, insert the PAN format
            if (input.length === 2) {
                input += panCardNumber;
                $(this).val(input);
                fullInput = input + inputChar; // Update fullInput accordingly
            }

            // Check if the input exceeds 15 characters
            if (fullInput.length > 15) {
                errorMessage = 'GST Number accepts only 15 characters';
                e.preventDefault();
            }

            // First two positions should be digits
            if (input.length < 2 && !/^\d*$/.test(fullInput) && !/\d/.test((e.key))) {
                errorMessage = 'First 2 positions should be digits';
                e.preventDefault();
            }

            // Third to Eleventh positions should follow the PAN format (AAAAA0000A)
            if (fullInput.length > 2 && fullInput.length <= 12) {
                if (!/^[A-Z]{5}[0-9]{4}[A-Z]$/.test(fullInput.slice(2))) {
                    errorMessage = '3rd to 11th positions should follow the PAN format (AAAAA0000A)';
                    e.preventDefault();
                }
            }

            // Thirteenth position should be a digit
            if (fullInput.length === 13 && !/^\d$/.test(inputChar) && !/\d/.test((e.key))) {
                errorMessage = '13th character must be a digit';
                e.preventDefault();
            }

            // Fourteenth position should be "Z"
            if (fullInput.length === 14 && inputChar !== 'Z') {
                errorMessage = '14th character must be "Z"';
                e.preventDefault();
            }

            // Fifteenth position should be a checksum digit
            if (fullInput.length === 15 && !/^\d$/.test(inputChar) && !/\d/.test((e.key))) {
                errorMessage = '15th character must be a digit';
                e.preventDefault();
            }

            // Display the error message if any
            if (errorMessage) {
                $('#gst-registration-number-error').text(errorMessage).removeClass('d-none');
                return false;
            }

            // Clear error message if no errors
            $('#gst-registration-number-error').addClass('d-none');
        });

        // Upper Case Letter
        $('#gst-registration-number').focusout(function () {
            let uppercaseValue = $(this).val().toUpperCase();
            $(this).val(uppercaseValue);
        });


        // Show Error Message
        $('#gst-registration-number').focusout(function () {
            let input = $(this).val();
            let errorMessage = '';

            if (!fullInput || input.length < 15) {
                errorMessage = 'Gst Number Accept Only 15 characters';
            }

            // Display the error message if any
            if (errorMessage !== "") {
                $('#gst-registration-number-error').text(errorMessage).removeClass('d-none');
            } else {
                $('#gst-registration-number-error').addClass('d-none');
            }
        }); let previousValue = '';

        // Clear Value After Remove Only
        $('#gst-registration-number').keydown(function (e) {
            if (e.keyCode === 8 || e.keyCode === 46) {
                let currentValue = $(this).val();
                if (currentValue.length < previousValue.length) {
                    $(this).val(previousValue);
                }
            }
        });
    }

    // Call GSTRegistration function with PAN card number
    GSTRegistration();


    // Function to validate file input and display preview image
    function validateFile(input, imgPreview, fileType) {
        
        const storagePath = input.value;

        // Check if a file is uploaded
        if (!storagePath) {
            alert("Please upload an image");
            imgPreview.attr("src", "");
            return;
        }

        // Define variables for maximum file size and valid file formats
        let maxFileSize, validFileFormats;

        // Function to handle file types based on parameters
        function handleFileType(type, params) {
            

            // Determine if local storage is enabled for the file type
            const enableLocalStorage = params[`Enable${type}UploadInLocalStorage`];

            // Set maxFileSize and validFileFormats based on storage type
            if (enableLocalStorage) {
                maxFileSize = params[`MaximumFileSizeFor${type}UploadInLocalStorage`];
                validFileFormats = params[`${type}AllowedFileFormatsForLocalStorage`];
            } else {
                maxFileSize = params[`MaximumFileSizeFor${type}UploadInDb`];
                validFileFormats = params[`${type}AllowedFileFormatsForDb`];
            }
        }

        // Mapping of file types to document types
        const fileTypeMapping = {
            'SignPath': 'SignDocument',
            'PhotoPath': 'PhotoDocument',
            'PhotoPathBank': 'BankStatement',
            'PhotoPathFinance': 'FinancialAssetDocument',
            'PhotoPathMovable': 'MovableAssetDocument',
            'PhotoPathImmovable': 'ImmovableAssetDocument',
            'PhotoPathAgree': 'AgricultureAssetDocument',
            'PhotoPathMachinery': 'MachineryAssetDocument',
            'PhotoPathTax': 'IncomeTaxDocument',
            'PhotoPathKYC': 'KYCDocument',
            'PhotoPathGst': 'GSTDocument'
        };

        // Get the document type based on the file type
        const documentType = fileTypeMapping[fileType];

        // Check if document type exists
        
        if (!documentType) {
            alert('File Not Found !!!');
            imgPreview.attr("src", "");
            input.value = "";
            return;
        }

        // Handle file type based on document type and parameters
        handleFileType(documentType, personInformationParameterViewModel);

        // Check if a file is selected
        if (input.files.length === 0) {
            alert("Please select a file");
            imgPreview.attr("src", "");
            input.value = "";
            return false;
        }

        // Check if file size exceeds the maximum allowed size
        
        if ((input.files[0].size / 1024) >= maxFileSize) {
            alert(`File size exceeds the maximum allowed size of ${maxFileSize} KB`);
            imgPreview.attr("src", "");
            input.value = "";
            return;
        }

        // Get the file extension and valid extensions
        const fileExtension = storagePath.split('.').pop().toLowerCase();
        const validExtensions = validFileFormats.split(',').map(ext => ext.trim().toLowerCase());

        // Check if the file format is valid
        
        if (!validExtensions.includes(fileExtension)) {
            alert(`Invalid file format. Allowed formats are: ${validFileFormats}`);
            imgPreview.attr("src", "");
            input.value = "";
            return;
        }

        // Read the file and display it in the image preview
        const reader = new FileReader();
        reader.onload = (e) => {
            imgPreview.attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }

    // Event listener for change event on file input elements
    const fileInputMapping = {
        'person-photo-path': { fileType: 'PhotoPath', previewId: '#photo-path-image-preview' },
        'sign-path': { fileType: 'SignPath', previewId: '#sign-path-image-preview' },
        'photo-path-bank': { fileType: 'PhotoPathBank', previewId: '#photo-path-bank-image-preview' },
        'photo-path-gst': { fileType: 'PhotoPathGst', previewId: '#photo-path-gst-image-preview' },
        'photo-path-tax': { fileType: 'PhotoPathTax', previewId: '#photo-path-tax-image-preview' },
        'photo-path-kyc': { fileType: 'PhotoPathKYC', previewId: '#photo-path-kyc-image-preview' },
        'photo-path-machinery': { fileType: 'PhotoPathMachinery', previewId: '#photo-path-machinery-image-preview' },
        'photo-path-immovable': { fileType: 'PhotoPathImmovable', previewId: '#photo-path-immovable-image-preview' },
        'photo-path-finance': { fileType: 'PhotoPathFinance', previewId: '#photo-path-finance-image-preview' },
        'photo-path-movable': { fileType: 'PhotoPathMovable', previewId: '#photo-path-movable-image-preview' },
        'photo-path-agree': { fileType: 'PhotoPathAgree', previewId: '#photo-path-agree-image-preview' }
    };

    // Iterate over each key in the fileInputMapping object
    Object.keys(fileInputMapping).forEach(inputId => {
        // Add an event listener for the change event on the input element with the corresponding id
        $(`#${inputId}`).change(function (event) {
            
            const mapping = fileInputMapping[inputId];
            // Check if there's a valid mapping for the inputId
            if (!mapping) {
                alert('Invalid input element');
                $(this).attr("src", "");
                this.value = "";
                return;
            }
            // If there's a valid mapping, retrieve the fileType and previewId from the mapping
            const fileType = mapping.fileType;
            const previewId = mapping.previewId;

            // Select the image preview element using its id
            const imgPreview = $(previewId);
            validateFile(this, imgPreview, fileType);
        });
    });

    let previousDocumentType = ''; // Track the previous document type

    // Handle keyup event
    $('#document-number-kyc-document').keypress(function () {
        debugger;
        ValidationForDocumentTypes();
    });

    // Handle focusout event
    $('#document-number-kyc-document').focusout(function () {
        debugger;
        $('#gst-registration-number').val('');

        let inputValue = $(this).val();
        let documentID = $('#document-document-type-id').val();

        $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: documentID, async: false }, function (identityDocumentType) {
            debugger;
            validateAndClearError(inputValue, identityDocumentType);
        });

    });

    // Handle change event on document type selection
    $('#document-document-type-id').focusout(function () {
        
        //ValidationForDocumentTypes();

        let documentID = $(this).val();
        let inputValue = $('#document-number-kyc-document').val();

        $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: documentID, async: false }, function (identityDocumentType) {
            
            validateAndClearError(inputValue, identityDocumentType);
        });
    });

    function ValidationForDocumentTypes() {
        
        let documentID = $('#document-document-type-id').val();

        $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: documentID, async: false }, function (identityDocumentType) {
            debugger;
            if (identityDocumentType !== previousDocumentType) {
                // Clear previous validation
                $('#document-number-kyc-document').removeAttr('minlength').removeAttr('maxlength');
                $('#kyc-document-number-error').addClass('d-none');

            }

            // Update previous document type
            previousDocumentType = identityDocumentType;

            //when Caps Lock is ON then It accepts Alphabets for resolve this problem bellow
            // AADHAAR Card
            if (identityDocumentType === 'AADHAAR') {
                let inputValue = $('#document-number-kyc-document').val().replace(/\D/g, '').replace(/(.{4})/g, '$1 ').trim();

                $(this).attr('minlength', 14);
                $(this).attr('maxlength', 14);

                if (inputValue.length === 14) {
                    $('#kyc-document-number-error').addClass('d-none');
                } else {
                    $('#kyc-document-number-error').html('Please Enter A Valid Aadhar Number (e.g., 2341 1234 4567)');
                    $('#kyc-document-number-error').removeClass('d-none');

                    inputValue = inputValue.slice(0, 14);
                }

                $('#document-number-kyc-document').val(inputValue);
            }

            // Pan Card
            if (identityDocumentType === 'PAN') {
                let inputValue = $('#document-number-kyc-document').val();

                // Remove any non-alphanumeric characters and convert to uppercase
                let validInput = inputValue.replace(/[^a-zA-Z0-9]/g, '').toUpperCase();

                // Extract and format parts of the input
                let letters = validInput.slice(0, 5).replace(/[^A-Z]/g, ''); // First 5 letters (alphabetic)
                let digits = validInput.slice(5, 9).replace(/[^0-9]/g, ''); // Next 4 digits (numeric)
                let lastLetter = validInput.slice(9).replace(/[^A-Z]/g, ''); // Last letter (alphabetic)

                // Construct formatted value
                let formattedValue = letters + digits + lastLetter;

                // Ensure the formatted value is exactly 10 characters long
                if (formattedValue.length > 10) {
                    formattedValue = formattedValue.slice(0, 10);
                } else if (formattedValue.length < 10) {
                    // Pad with spaces if the length is less than 10
                    formattedValue = formattedValue.padEnd(10, ' ');
                }

                $('#document-number-kyc-document').val(formattedValue.trim()); // Remove any trailing spaces

                // Validate length and format, and show/hide error message
                const panRegex = /^[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
                if (panRegex.test(formattedValue)) {
                    $('#kyc-document-number-error').addClass('d-none');
                } else {
                    $('#kyc-document-number-error').html('Please Enter A Valid PAN Number (e.g., ABCDE1234F)');
                    $('#kyc-document-number-error').removeClass('d-none');
                }
            }

            // Voting Card
            if (identityDocumentType === 'VOTER') {

                let inputValue = $('#document-number-kyc-document').val().replace(/[^a-zA-Z0-9]/g, '');

                let firstThreeLetters = inputValue.slice(0, 3).replace(/[^a-zA-Z]/g, '').toUpperCase();
                let lastDigits = inputValue.slice(3, 10).replace(/[^0-9]/g, '');

                let formattedValue = firstThreeLetters + lastDigits;

                $(this).attr('minlength', 10);
                $(this).attr('maxlength', 10);

                if (inputValue.length <= 10) {

                    $('#document-number-kyc-document').val(formattedValue);

                    if (formattedValue.length === 10)
                        $('#kyc-document-number-error').addClass('d-none');
                    else {
                        $('#kyc-document-number-error').html('Please Enter A Valid Voter ID(e.g., ABC1234567)');
                        $('#kyc-document-number-error').removeClass('d-none');
                    }

                } else {
                    $('#document-number-kyc-document').val(formattedValue);
                }
            }

            // DRIVING License
            if (identityDocumentType === 'DRIVING') {

                let inputValue = $('#document-number-kyc-document').val().replace(/[^a-zA-Z0-9]/g, '');

                let firstTwoLetters = inputValue.slice(0, 2).replace(/[^a-zA-Z]/g, '').toUpperCase();
                let middleDigits = inputValue.slice(2, 4).replace(/[^0-9]/g, '');
                let space = " ";
                let lastDigits = inputValue.slice(4, 15).replace(/[^0-9]/g, '');

                let formattedValue = firstTwoLetters + middleDigits + space + lastDigits;

                $(this).attr('minlength', 16);
                $(this).attr('maxlength', 16);

                if (inputValue.length <= 15) {

                    $('#document-number-kyc-document').val(formattedValue);

                    if (formattedValue.length === 16)
                        $('#kyc-document-number-error').addClass('d-none');
                    else {
                        $('#kyc-document-number-error').html('Please Enter A Valid Driving License Number (e.g.,MH12 12345678901)');
                        $('#kyc-document-number-error').removeClass('d-none');
                    }
                } else {
                    $('#document-number-kyc-document').val(formattedValue.toUpperCase());
                }
            }

            //PassPort
            if (identityDocumentType === 'PASSPORT') {

                let inputValue = $('#document-number-kyc-document').val().replace(/[^a-zA-Z0-9]/g, '');

                let firstLetter = inputValue.slice(0, 1).replace(/[^a-zA-Z]/g, '').toUpperCase();
                let lastDigits = inputValue.slice(1, 8).replace(/[^0-9]/g, '');

                let formattedValue = firstLetter + lastDigits;

                $(this).attr('minlength', 8);
                $(this).attr('maxlength', 8);

                if (inputValue.length <= 8) {

                    $('#document-number-kyc-document').val(formattedValue);

                    if (formattedValue.length === 8)
                        $('#kyc-document-number-error').addClass('d-none');
                    else {
                        $('#kyc-document-number-error').html('Please Enter A Valid Passport Number (e.g., A1234567)');
                        $('#kyc-document-number-error').removeClass('d-none');
                    }

                } else {
                    $('#document-number-kyc-document').val(formattedValue.toUpperCase());
                }
            }

            // RATION
            if (identityDocumentType === 'RATION') {

                let inputValue = $('#document-number-kyc-document').val().replace(/[^a-zA-Z0-9]/g, '');

                let lastDigits = inputValue.slice(0, 10).replace(/[^0-9]/g, '');
                let formattedValue = lastDigits;

                $(this).attr('minlength', 10);
                $(this).attr('maxlength', 10);

                if (inputValue.length <= 10) {

                    $('#document-number-kyc-document').val(formattedValue);

                    if (formattedValue.length === 10)
                        $('#kyc-document-number-error').addClass('d-none');
                    else {
                        $('#kyc-document-number-error').html('Please Enter A Valid Ration Card Number(e.g., 3412345678)');
                        $('#kyc-document-number-error').removeClass('d-none');
                    }

                } else {
                    $('#document-number-kyc-document').val(formattedValue.toUpperCase());
                }
            }

            // Birth Certificate
            if (identityDocumentType === 'BIRTHCRTF') {
                let inputValue = $('#document-number-kyc-document').val();

                // Set maxlength to 50
                $('#document-number-kyc-document').attr('maxlength', 50);

                // Convert inputValue to uppercase
                inputValue = inputValue.toUpperCase();

                // Trim the input value to exactly 50 characters if it exceeds the limit
                if (inputValue.length > 50) {
                    inputValue = inputValue.substring(0, 50);
                }

                // Set the modified value back to the input field
                $('#document-number-kyc-document').val(inputValue);
            }

            // Overseas Citizenship Of India Document
            if (identityDocumentType === 'OVRSCTZ') {
                let inputValue = $('#document-number-kyc-document').val();

                // Set  maxlength to 50
                $('#document-number-kyc-document').attr('maxlength', 50);

                // Trim the input value to exactly 50 characters if it exceeds the limit
                if (inputValue.length > 50) {
                    inputValue = inputValue.substring(0, 50);
                    $('#document-number-kyc-document').val(inputValue);
                }
            }

            // Transfer/School leaving/Matriculation Certificate
            if (identityDocumentType === 'SCHOLLCRTF') {
                let inputValue = $('#document-number-kyc-document').val();

                // Set  maxlength to 50
                $('#document-number-kyc-document').attr('maxlength', 50);

                // Trim the input value to exactly 50 characters if it exceeds the limit
                if (inputValue.length > 50) {
                    inputValue = inputValue.substring(0, 50);
                    $('#document-number-kyc-document').val(inputValue);
                }
            }

        });
    }

    function validateAndClearError(inputValue, documentType) {
        
        let errorMessage = '';
        let isValid = true;

        if (documentType === 'AADHAAR') {
            if (inputValue.length !== 14) {
                errorMessage = 'Please Enter A Valid Aadhar Number (e.g., 2341 1234 4567)';
                isValid = false;
            }
        }
        else if (documentType === 'PAN') {
            const panRegex = /^[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
            if (!panRegex.test(inputValue)) {
                errorMessage = 'Please Enter A Valid PAN Number (e.g., ABCDE1234F)';
                isValid = false;
            }
        }
        else if (documentType === 'VOTER') {
            if (inputValue.length !== 10) {
                errorMessage = 'Please Enter A Valid Voter ID (e.g., ABC1234567)';
                isValid = false;
            }
        }
        else if (documentType === 'DRIVING') {
            if (inputValue.length !== 16) {
                errorMessage = 'Please Enter A Valid Driving License Number (e.g., MH12 12345678901)';
                isValid = false;
            }
        }
        else if (documentType === 'PASSPORT') {
            if (inputValue.length !== 8) {
                errorMessage = 'Please Enter A Valid Passport Number (e.g., A1234567)';
                isValid = false;
            }
        }
        else if (documentType === 'RATION') {
            if (inputValue.length !== 10) {
                errorMessage = 'Please Enter A Valid Ration Card Number (e.g., 3412345678)';
                isValid = false;
            }
        }

        

        if (isValid) {
            $('#kyc-document-number-error').addClass('d-none');
            $('#kyc-number-error').addClass('d-none');
        }
        else
            $('#document-number-kyc-document').val('');

        //else {
        //    //$('#kyc-document-number-error').html(errorMessage).removeClass('d-none');
        //    $('#kyc-number-error').removeClass('d-none');
        //}
    }

    $("#name-as-per-document-kyc-document").on('input', function () {
        let inputVal = $(this).val();
        let filteredVal = inputVal.replace(/[^a-zA-Z\s]/g, '');
        $(this).val(filteredVal);
    });

    $('#enable-any-court-case').change(function () {
        
        if ($(this).is(':checked', true)) {
            $('#court-case-full-details-error').addClass('d-none');
        }
    });


    $('#enable-taking-any-court-action').change(function () {
        
        if ($(this).is(':checked', true)) {
            $('#court-case-type-id-error').addClass('d-none');
            $('#filing-date-error').addClass('d-none');
            $('#filing-number-error').addClass('d-none');
            $('#registration-date-error').addClass('d-none');
            $('#registration-number-error').addClass('d-none');
            $('#cnr-number-error').addClass('d-none');
            $('#court-case-stage-id-error').addClass('d-none');
        }
    });

    $('#document-type-id-kyc-document').focusout(function () {
        
        //Kyc Values Get Clear
        $('#document-document-type-id').val('');
        $('#name-as-per-document-kyc-document').val('');
        $('#document-number-kyc-document').val('');
        $('#sequence-number-kyc-document').val('');
        $('#date-of-issue-kyc-document').val('');
        $('#date-of-expiry-kyc-document').val('');
        $('#issuing-authority-kyc-document').val('');
        $('#place-of-issue-kyc-document').val('');
        $('#date-of-request').val('');
        $('#date-of-expecting-submit').val('');
        $('#date-of-submit').val('');
        $('.document-upload-status').prop('checked', false);
        $('#photo-path-kyc').val('');
        $('#photo-path-kyc-image-preview').attr('src', '');
        $('#file-caption-kyc').val('');
        $('#note-kyc-document').val('');
        $('.modal-input-error').addClass('d-none');

    });

    //Validation Identity Document Type
    $('#document-document-type-id').focusout(function () {
        
        $('#name-as-per-document-kyc-document').val('');
        $('#sequence-number-kyc-document').val('');
        $('#date-of-issue-kyc-document').val('');
        $('#date-of-expiry-kyc-document').val('');
        $('#issuing-authority-kyc-document').val('');
        $('#place-of-issue-kyc-document').val('');
        $('#date-of-request').val('');
        $('#date-of-expecting-submit').val('');
        $('#date-of-submit').val('');
        $('.document-upload-status').prop('checked', false);
        $('#photo-path-kyc').val('');
        $('#photo-path-kyc-image-preview').attr('src', '');
        $('#file-caption-kyc').val('');
        $('#note-kyc-document').val('');
        $('.modal-input-error').addClass('d-none');

        //GST Value Get Clear
        $('.gst-registration-input').val('');
        $('#enable-gst-return-document').prop('checked', false);
        $('#is-applicable-eway-bill').prop('checked', false);
        $('#gst-return-document-block').addClass('d-none');
        gstDataTable.clear().draw();
    });

    $('#court-case-type-id').focusout(function () {
        $('#filing-date').val('');
        $('#filing-number').val('');
        $('#registration-date').val('');
        $('#registration-number').val('');
        $('#cnr-number').val('');
        $('#court-case-stage-id').val('');
        $('#note-borrowing-detail').val('');
    });

    $('#court-case-types-id').focusout(function () {
        $('#filing-dates').val('');
        $('#filing-numbers').val('');
        $('#registration-dates').val('');
        $('#registration-numbers').val('');
        $('#cnr-number-case').val('');
        $('#amount-of-decree').val('');
        $('#collateral-amount').val('');
        $('#court-cases-stage-id').val('');
        $('#note-court-case').val('');
        $('.modal-input-error').addClass('d-none');
    });

    //validation for only Create
    function validationForGST() {
        
        let documentId = $('#document-document-type-id').val();
        identityDocumentType = documentId;
        $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: documentId }, function (identityDocumentType) {
            
            if (identityDocumentType === 'PAN')
                $('#enable-gst-registration-details-input').removeClass('read-only');
            else
                $('#enable-gst-registration-details-input').addClass('read-only');
        })
    }

    //validation for only Amend and Verify
    //function validationForGSTVerify() {
    //    
    //    $('#tbl-kyc-document > tbody > tr').each(function () {
    //        
    //        const currentRow = $(this).closest('tr');
    //        const columnValues = personKycDataTable.row(currentRow).data();

    //        // Handling Code If Row Is Undefined Or Null
    //        if (typeof columnValues !== 'undefined' && columnValues !== null) {
    //            
    //            $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: columnValues[3], async: false }, function (data) {
    //                
    //                if (data === 'PAN') {
    //                    $('#enable-gst-registration-details-input').removeClass('read-only');
    //                } else {
    //                   $('#enable-gst-registration-details-input').addClass('read-only');
    //                }
    //            });
    //        }
    //        return false;
    //    });
    //}

    $('#registration-number-movable-asset').keypress(function (e) {
        let inputValue = e.target.value.replace(/[^a-zA-Z0-9]/g, '');

        let firstTwoLetters = inputValue.slice(0, 2).replace(/[^a-zA-Z]/g, '').toUpperCase();
        let middleDigits = inputValue.slice(2, 4).replace(/[^0-9]/g, '');
        let space = " ";
        let lastTwoLetters = inputValue.slice(4, 6).replace(/[^a-zA-Z]/g, '').toUpperCase();
        let lastDigits = inputValue.slice(6, 10).replace(/[^0-9]/g, '');

        let formattedValue = firstTwoLetters;
        if (middleDigits) formattedValue += space + middleDigits;
        if (lastTwoLetters) formattedValue += space + lastTwoLetters;
        if (lastDigits) formattedValue += space + lastDigits;

        $(this).attr('minlength', 13);
        $(this).attr('maxlength', 13);

        if (inputValue.length <= 10) {
            e.target.value = formattedValue;

            if (formattedValue.replace(/ /g, '').length === 10)
                $('#registration-number-movable-asset-error').addClass('d-none');
            else {
                $('#registration-number-movable-asset-error').html('Please Enter A Valid Registration Number (e.g., MH 12 AB 1234)');
                $('#registration-number-movable-asset-error').removeClass('d-none');
            }
        } else {
            e.target.value = formattedValue.toUpperCase();
        }
    });

    
    // Nominee Guardian Person AutoComplete 

    $('#person-information-number-guardian').autocomplete({
        source: function (request, response) {
            $.get('/PersonChildAction/GetPersonAutoCompleteList', { '_inputString': request.term })
                .done(function (data) {
                    if (data.length > 0) {
                        let results = $.map(data, function (item) {
                            let arry = item.split('-');
                            let NameOfPersonType = arry[3];
                            let fullname = arry[0] + " --> " + arry[1];
                            let personInfoNumber = arry[5];
                            let PersonId = arry[6] + "-" + arry[7] + "-" + arry[8] + "-" + arry[9] + "-" + arry[10];
                            let InEnglishName = arry[0];
                            let InMarathiName = arry[1];
                            return { label: fullname, value: personInfoNumber, InEnglishName, InMarathiName };
                        });
                        response(results);
                    } else {
                        response([{ label: 'No Records Found', value: -1 }]);
                    }
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    alert(jqXHR.responseText);
                });
        },
        focus: function (event, ui) {
            event.preventDefault();
        },
        appendTo: "#guardian-person-information",
        minLength: 3,
        scroll: true,
        autoFocus: true,
        select: function (event, ui) {
            if (ui.item.value == -1) {
                $('#person-information-number-guardian').val('');
                $('#guardian-member-name').removeClass('d-none');
                $('#guardian-pin').val('None');
                return false; // Prevent selection of "No Records Found"
            } else {
                $('#person-information-number-guardian').val(ui.item.label);
                $('#guardian-pin').val(ui.item.value);
                guardianPersonInfoId = ui.item.value;
                if (guardianPersonInfoId !== '') {
                    $('#guardian-member-name').addClass('d-none');
                    $('#guardian-person-information').removeClass('d-none');
                }
                return false; // Prevent default behavior
            }
        }
    });

    $('#guardian-full-name').focusout(function () {
        let gurdianFullName = $('#guardian-full-name').val();

        if ((gurdianFullName !== 'None') && (gurdianFullName.length > 3)) {
            $('#person-information-number-guardian').prop('selectedIndex', 0);

            $('#guardian-person-information').addClass('d-none');
            $('#guardian-pin').val('None');
        } else {
            $('#guardian-person-information').removeClass('d-none');
        }
    });

    $('#person-information-number-guardian').focusout(function () {
        let personInformationNumberGuardian = $('#person-information-number-guardian').val();

        // Clear the input if no item from the list is selected
        let isSelected = false;
        $('#person-information-number-guardian').autocomplete("widget").find("li").each(function () {
            if ($(this).text() === personInformationNumberGuardian) {
                isSelected = true;
                return false; // break the loop
            }
        });

        if (!isSelected) {
            $('#person-information-number-guardian').val('');
            $('#guardian-member-name').removeClass('d-none');
            $('#guardian-person-information').removeClass('d-none');
        } else {
            $('#guardian-member-name').addClass('d-none');
            $('.guardian-member').val('None');
        }
    });


    // Person Family AutoComplete 
    
    $('#family-person-information-number').autocomplete({
        source: function (request, response) {
            $.get('/PersonChildAction/GetPersonAutoCompleteList', { '_inputString': request.term })
                .done(function (data) {
                    let result = [];
                    if (data.length > 0) {
                        response($.map(data, function (item) {
                            let arry = item.split('-');
                            let fullname = arry[0] + " --> " + arry[1];
                            let personInfoNumber = arry[5];
                            let InEnglishName = arry[0];
                            let InMarathiName = arry[1];
                            return { label: fullname, value: personInfoNumber, InEnglishName, InMarathiName };
                        }));
                    } else {
                        response([{ label: 'No Records Found', value: -1 }]);
                    }
                })
                .fail(function (jqXHR) {
                    alert(jqXHR.responseText);
                });
        },
        focus: function (event, ui) {
            event.preventDefault();
            // Prevent action when "No Records Found" is focused
            if (ui.item.value === -1) {
                $('#family-person-information-number').val('');
                $('#family-member-name').removeClass('d-none');
                $('#family-pin').val('None');
            }
        },
        appendTo: "#person-information-number-input",
        minLength: 3,
        scroll: true,
        autoFocus: true,
        select: function (event, ui) {
            // Prevent action when "No Records Found" is selected
            if (ui.item.value === -1) {
                $('#family-person-information-number').val('');
                $('#family-pin').val('None');
                $('#family-member-name').removeClass('d-none');
                return false;
            }

            $('#family-person-information-number').val(ui.item.label);
            $('#family-pin').val(ui.item.value);
            personInformationNumber = ui.item.value;
            personInformationNumberText = ui.item.label;

            if (personInformationNumber !== '') {
                $('#family-member-name').addClass('d-none');
                $('#person-information-number-input').removeClass('d-none');
            }

            return false;
        }
    }).focus(function (event, ui) {
        personInformationNumber = '',
        personInformationNumberText = '';

        $(this).autocomplete('search');
    });

    // Validation Family Member
    $('#full-name-of-family-member').focusout(function () {
        
        let fullNameOfFamily = $('#full-name-of-family-member').val();

        if ((fullNameOfFamily !== 'None') && (fullNameOfFamily.length > 3)) {
            $('#family-person-information-number').prop('selectedIndex', 0);
            $('#person-information-number-input').addClass('d-none');
            $('#person-information-number-input').val('None');
        } else {
            $('#person-information-number-input').removeClass('d-none');
        }
    });

    $('#family-person-information-number').focusout(function () {
        
        let familyPersonInformationNumber = $('#family-person-information-number').val();

        // Check if the value is not from the autocomplete list
        let isInAutocomplete = false;
        $('#family-person-information-number').autocomplete("widget").children("li").each(function () {
            if ($(this).text() === familyPersonInformationNumber) {
                isInAutocomplete = true;
                return false;
            }
        });

        if (!isInAutocomplete) {
            // Clear the input if the typed value is not in the autocomplete list
            $('#family-person-information-number').val('');
            $('#family-pin').val('None');
            $('#family-member-name').removeClass('d-none');
        } else if ((familyPersonInformationNumber !== 'None') && (familyPersonInformationNumber.length > 3)) {
            $('#family-member-name').addClass('d-none');
            $('#family-member-name').val('None')
        }
    });

    // Person Authorized AutoComplete 
    
    $('#authorized-person-information-number').autocomplete({
        source: function (request, response) {
            $.get('/PersonChildAction/GetPersonAutoCompleteList', { '_inputString': request.term })
                .done(function (data) {
                    let result = [];
                    if (data.length > 0) {
                        response($.map(data, function (item) {
                            let arry = item.split('-');
                            let fullname = arry[0] + " --> " + arry[1];
                            let personInfoNumber = arry[5];
                            let InEnglishName = arry[0];
                            let InMarathiName = arry[1];
                            return { label: fullname, value: personInfoNumber, InEnglishName, InMarathiName };
                        }));
                    } else {
                        response([{ label: 'No Records Found', value: -1 }]);
                    }
                })
                .fail(function (jqXHR) {
                    alert(jqXHR.responseText);
                });
        },
        focus: function (event, ui) {
            event.preventDefault();
            // Prevent action when "No Records Found" is focused
            if (ui.item.value === -1) {
                $('#authorized-person-information-number').val('');
                $('#authorized-member-name').removeClass('d-none');
                $('#authorized-pin').val('None');
            }
        },
        appendTo: "#authorized-person-information-number-input",
        minLength: 3,
        scroll: true,
        autoFocus: true,
        select: function (event, ui) {
            // Prevent action when "No Records Found" is selected
            if (ui.item.value === -1) {
                $('#authorized-person-information-number').val('');
                $('#authorized-pin').val('None');
                $('#authorized-member-name').removeClass('d-none');
                return false;
            }

            $('#authorized-person-information-number').val(ui.item.label);
            $('#authorized-pin').val(ui.item.value);
            authorizedPersonInformationNumber = ui.item.value;
            authorizedPersonInformationNumberText = ui.item.label;

            if (authorizedPersonInformationNumber !== '') {
                $('#authorized-member-name').addClass('d-none');
                $('#authorized-person-information-number-input').removeClass('d-none');
            }

            return false;
        }
    }).focus(function (event, ui) {
        authorizedPersonInformationNumber = '',
        authorizedPersonInformationNumberText = '';

        $(this).autocomplete('search');
    });

    $('#authorized-person-information-number').focusout(function () {
        
        let authorizedPersonInformationNumber = $('#authorized-person-information-number').val();

        // Check if the value is not from the autocomplete list
        let isInAutocomplete = false;
        $('#authorized-person-information-number').autocomplete("widget").children("li").each(function () {
            if ($(this).text() === authorizedPersonInformationNumber) {
                isInAutocomplete = true;
                return false;
            }
        });

        if (!isInAutocomplete) {
            // Clear the input if the typed value is not in the autocomplete list
            $('#authorized-person-information-number').val('');
            $('#authorized-pin').val('None');
            $('#authorized-member-name').removeClass('d-none');
        } else if ((authorizedPersonInformationNumber !== 'None') && (authorizedPersonInformationNumber.length > 3)) {
            $('#authorized-member-name').addClass('d-none');
            $('#authorized-member-name').val('None')
        }
    });


    // Validation Authorized Member
    $('#full-name-of-authorized-member').focusout(function () {
        
        let fullNameOfAuthorized = $('#full-name-of-authorized-member').val();

        if ((fullNameOfAuthorized !== 'None') && (fullNameOfAuthorized.length > 3)) {
            $('#authorized-person-information-number').prop('selectedIndex', 0);
            $('#authorized-person-information-number-input').addClass('d-none');
            $('#authorized-person-information-number-input').val('None');
        } else {
            $('#authorized-person-information-number-input').removeClass('d-none');
        }
    });

    function ClearModalInputs() {

        if (hasAnyCourtCase === true)
            $('#any-court-case-block').removeClass('d-none');
        else
            $('#any-court-case-block').addClass('d-none');

        if (isTakingAnyCourtAction === true)
            $('#taking-any-court-action-block').removeClass('d-none');
        else
            $('#taking-any-court-action-block').addClass('d-none');
    }

    // This code triggers when the focus is removed from the VIP rank input field
    $('#vip-rank').focusout(function () {
        let VIPRank = parseInt($('#vip-rank').val());

        // Check if the entered VIP rank is a valid number greater than 0
        if (!isNaN(VIPRank) && VIPRank > 0 && VIPRank < 11) {
            $('.vip-background-details').removeClass('d-none');
        } else {
            $('.vip-background-details').addClass('d-none');
            $('#vip-background-details').val('None');
            $('#trans-vip-background-details').val('None');
        }
    });

    //Person Information Number
    $('#person-information-number').focusout(function (event) {
        let personInformationNumber = $('#person-information-number').val();

        $.get('/PersonChildAction/GetUniqueInfoNumberStatus', { _personInformationNumber: personInformationNumber, async: false }, function (data) {
            debugger
            if (data)
                $('#person-information-number-error').addClass('d-none');
            else
                $('#person-information-number-error').removeClass('d-none');
        });

    });

    // Event listener for when birth city dropdown loses focus
    $('#birth-city').focusout(function () {
        
        let city = $(this).val();
        HandleCitySelection(city);
    });

    // Function to check city selection and update UI
    function HandleCitySelection(city) {
        
        if (city !== 0) {
            $.get('/PersonChildAction/GetRegionalCountryCity', { _centerId: city }, function (data, textStatus, jqXHR) {
                // Function to handle visibility of foreigner details based on data
                
                if (data.length === 0)
                    $('.foreigner-details-accordian').addClass('d-none');
                else
                    $('.foreigner-details-accordian').addClass('d-none');
            });
        } else
            $('.foreigner-details-accordian').addClass('d-none');
    }


    // Update min date whenever registrations date changes
    $('#registrations-date').click(function () {
        
        updateMinDate();
    });

    // GST registrationDate Min Date 1 July 2017...
    function updateMinDate() {
        
        let registrationDate = $('#registrations-date').val();
        let minDate = new Date('2017-07-01'); // Minimum allowed date ('1 July 2017')

        let selectedDate = new Date($('#registrations-date').val());
        // Disable dates before '1 July 2017'
        if (selectedDate.getTime() > minDate.getTime()) {
            $('#registrations-date').prop('min', $('#registrations-date').val());
        } else {
            $('#registrations-date').prop('min', '2017-07-01');
            $('#registrations-date').val(''); // Reset the value if it's before '1 July 2017'
        }
    }


    // Event listener for when marital status dropdown changes
    $('#marital-status').focusout(function () {
        
        let maritalStatusId = $(this).val();
        CheckMaritalStatus(maritalStatusId);
        MaritalStatus(maritalStatusId);

        $('#life-partner-name').val('');
        $('#trans-life-partner-name').val('');
        $('#life-partner-maiden-name').val('');
        $('#trans-life-partner-maiden-name').val('');
        $('#date-of-marriage').val('');
    });

    // Function to check marital status and update UI
    function CheckMaritalStatus(maritalStatusId) {
        
        maritalStatusId = $('#marital-status').val();
        if (maritalStatusId !== 0) {
            $.get('/PersonChildAction/GetSysNameOfMaritalStatus', { _maritalStatusId: maritalStatusId, async: false }, function (data) {
                
                if (data === 'MARRID')
                    $('#married-status').removeClass('d-none');
                else
                    $('#married-status').addClass('d-none');
            });
        } else
            $('#married-status').addClass('d-none');
    }

    function MaritalStatus(maritalStatusId) {
        maritalStatusId = $('#marital-status').val();
        $.get('/PersonChildAction/GetSysNameOfMaritalStatus', { _maritalStatusId: maritalStatusId, async: false }, function (data, textStatus, jqXHR) {
            _maritalStatus = data;
            IsValidAdditionalDetailsAccordionInputs(_maritalStatus);
        });
    }

    $('#dob').focusout(function () {
        
        let dateOfBirth = $('#dob').val();
        CalculateAge(dateOfBirth);
        
        //[Modify By : SS 12/09/2024]
        $('.guardian-details-input').val('');
        $('.guardian-member').val('');
        $('.guardian-details-input').prop('checked', false);
    });
    // Function to calculate age based on date of birth
    function CalculateAge(dateOfBirth) {
        
        const today = new Date();
        const birthDate = new Date(dateOfBirth);
        let age = today.getFullYear() - birthDate.getFullYear();
        const month = today.getMonth() - birthDate.getMonth();

        // If the birth month hasn't occurred yet in the current year, subtract one year from age
        if (month < 0 || (month === 0 && today.getDate() < birthDate.getDate())) {
            age--;
        }

        GuardianVisibility(age);
        UpdateMaritalStatus(age);
    }

    // Function to show or hide guardian element based on age
    function GuardianVisibility(age) {
        
        if (age < 18) {
            $('.guardian').removeClass('d-none');
        } else {
            $('.guardian').addClass('d-none');
        }

    }


    // Event listener for focusout event on input field with id 'dob'
    $('#dob').focusout(function () {
        
        // Retrieve the date of birth value from the input field
        let dob = $(this).val();
        $('#dob-on-document').val(dob);
        //Modify By -- Sagar Kare
        // Convert the DOB to a Date object
        const birthDate = new Date(dob);
        const today = new Date();

        // Calculate max and min birth dates based on the birth date
        let maxBirthDate = new Date(birthDate);
        maxBirthDate.setFullYear(birthDate.getFullYear() + 5);

        let minBirthDate = new Date(birthDate);
        minBirthDate.setFullYear(birthDate.getFullYear() - 5);

        // Check if the calculated maxBirthDate exceeds today's date
        if (maxBirthDate > today) {
            maxBirthDate = today;
        }

        // Set the min and max attributes
        $('#dob-on-document').attr('min', GetInputDateFormat(minBirthDate));
        $('#dob-on-document').attr('max', GetInputDateFormat(maxBirthDate));

        if (dob === '') {
            $('#marital-status').prop('selectedIndex', 0);
            $('#marital-status').attr('disabled', 'disabled');
            $('#married-status').addClass('d-none');
            GuardianVisibility(age);
            $('.guardian-details-input').val('');
            $('.guardian-member').val('');
            $('.guardian-details-input').prop('checked', false);
        }
            //[Modify By :SS 12/09/2024]
        else {
            // Calculate age based on dob
            let today = new Date();
            let birthDate = new Date(dob);
            let age = today.getFullYear() - birthDate.getFullYear();
            let monthDiff = today.getMonth() - birthDate.getMonth();

            if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
                age--;
            }

            // If age is greater than 16, set #marital-status's selectedIndex to 0
            if (age >= 16) {
                $('#marital-status').prop('selectedIndex', 0);
            }

            GuardianVisibility(age);  // Update GuardianVisibility with the current age
            CheckMaritalStatus(maritalStatusId);
        }
    });

    function UpdateMaritalStatus(age) {
        
        let maritalStatus = $('#marital-status');

        if (age < 16) {
            if (maritalStatus.length) { // Check if the element exists
                maritalStatus.prop('selectedIndex', 5);
                maritalStatus.attr('disabled', 'disabled'); // Disable the dropdown
                maritalStatus.css('backgroundColor', 'transparent');
                $('#married-status').addClass('d-none');
            }
        }
        else {
            //maritalStatus.prop('selectedIndex', 0);
            maritalStatus.removeAttr('disabled'); // Clear disabling of the dropdown
            maritalStatus.css('backgroundColor', 'transparent');
            maritalStatus.css('visibility', 'visible'); // Ensure the select is visible
        }
    }

    // Validation threshold-limit
    $('#threshold-limit').focusout(function (e) {
        let amt = parseInt($(this).val());
        if ((amt === 2000000) || (amt === 4000000)) {
            $('#threshold-limit-error').addClass('d-none');
            $(this).val(amt);
        }
        else
            $('#threshold-limit-error').removeClass('d-none');
    })

    // Event listener for when occupation dropdown changes
    $('#occupation-id-drop').focusout(function () {
        
        let occupationId = $(this).val();
        CheckOccupation(occupationId);
        Occupation(occupationId);
        $('#employer-name').val('');
        $('#trans-employer-name').val('');
        $('#date-of-incorporation').val('');
        $('#employment-type-id').val('');
        $('#nature-of-employer-id').val('');
        $('#employer-nature-other-details').val('');
        $('#trans-employer-nature-other-details').val('');
        $('#employer-address-details').val('');
        $('#trans-employer-address-details').val('');
        $('#employer-contact-details').val('');
        $('#trans-employer-contact-details').val('');
        $('#employer-city-id').val('');
        $('#designation-id').val('');
        $('#annual-income').val('');
        $('#epf-number').val('');
        $('#trans-epf-number').val('');
        $('#employed-since').val('');
        $('#note-person-employment-detail').val('');
        $('#trans-note-person-employment-detail').val('');

    });

    // Function to check occupation status and update UI
    function CheckOccupation(occupationId) {
        
        occupationId = $('#occupation-id-drop').val();
        $.get('/PersonChildAction/GetSysNameOfOccupation', { _occupationId: occupationId, async: false }, function (data) {
            
            if (data === 'SLRD') {
                $('.occupation').removeClass('d-none');
                $('#enable-employee-input').removeClass('d-none');
            } else {
                $('.occupation').addClass('d-none');
                $('#enable-employee-input').addClass('d-none');
            }
        });
    }

    // Function to check occupation status and update UI
    function Occupation(occupationId) {
        
        occupationId = $('#occupation-id-drop').val();
        $.get('/PersonChildAction/GetSysNameOfOccupation', { _occupationId: occupationId, async: false }, function (data, textStatus, jqXHR) {
            
            _occupationType = data;
            IsValidAdditionalDetailsAccordionInputs(_occupationType);
        });
    }

    //Validation Policy Sum Assured
    $('#policy-sum-assured').focusout(function () {
        
        let policyPremium = parseFloat($('#policy-premium').val());
        let value = policyPremium * 2;
        $('#policy-sum-assured').attr('min', value);

    });

    //[Modify By Suraj Sonawane 13/09/2024]
    $('#field-value').keyup(function () {
        
        let contactID = $('#contact-type').val();

        $.get('/PersonChildAction/GetSysNameOfContactTypeById', { _contactTypeId: contactID, async: false }, function (data) {
            contact_Type = data;
        });

        ValidationForContactTypes(contact_Type);
        validateContactAndClearError(contact_Type);
    });

    $('#field-value').focusout(function () {
        
        validateContactAndClearError(contact_Type);
    });

    function ValidationForContactTypes(contact_Type) {
        

        if (contact_Type === 'Home')
        {
            let inputValue = $('#field-value').val();

            // Set the maxlength attribute to 50
            $('#field-value').attr('maxlength', 50);

            // Optional: If you want to enforce the length limit immediately, you can trim the input value
            if (inputValue.length > 50) {
                $('#field-value').val(inputValue.substring(0, 50));
            }
        } else {
            // Optionally reset maxlength for other contact types
            $('#field-value').attr('maxlength', 320);
        }

        if (contact_Type === 'Main')
        {
            let inputValue = $('#field-value').val();

            // Set the maxlength attribute to 50
            $('#field-value').attr('maxlength', 50);

            // Optional: If you want to enforce the length limit immediately, you can trim the input value
            if (inputValue.length > 50) {
                $('#field-value').val(inputValue.substring(0, 50));
            }
        } else {
            // Optionally reset maxlength for other contact types
            $('#field-value').attr('maxlength', 320);
        }

        if (contact_Type === 'Other')
        {
            let inputValue = $('#field-value').val();

            // Set the maxlength attribute to 50
            $('#field-value').attr('maxlength', 50);

            // Optional: If you want to enforce the length limit immediately, you can trim the input value
            if (inputValue.length > 50) {
                $('#field-value').val(inputValue.substring(0, 50));
            }
        } else {
            // Optionally reset maxlength for other contact types
            $('#field-value').attr('maxlength', 320);
        }

        if (contact_Type === 'Work')
        {
            let inputValue = $('#field-value').val();

            // Set the maxlength attribute to 50
            $('#field-value').attr('maxlength', 50);

            // Optional: If you want to enforce the length limit immediately, you can trim the input value
            if (inputValue.length > 50) {
                $('#field-value').val(inputValue.substring(0, 50));
            }
        } else {
            // Optionally reset maxlength for other contact types
            $('#field-value').attr('maxlength', 320);
        }

        //Toll Free Number
        if (contact_Type === 'TollFreeNumber')
        {
            $('#field-value').attr({
                'type': 'number',
                'minlength': 15,
                'maxlength': 15
            });

            //let inputValue = $('#field-value').val();

            //if (inputValue.length === 15)
            //    $('#field-value-error').addClass('d-none');
            //else {
            //    $('#field-value-error').removeClass('d-none');
            //}
        }

            //Toll Number
        else if (contact_Type === 'TollNumber')
        {
            $('#field-value').attr({
                'type': 'number',
                'minlength': 15,
                'maxlength': 15  
            });

            //let inputValue = $('#field-value').val();

            //if (inputValue.length === 15)
            //    $('#field-value-error').addClass('d-none');
            //else {
            //    $('#field-value-error').removeClass('d-none');
            //}
        }

            //Miss Call Number
        else if (contact_Type === 'MisscallNumber')
        {
            $('#field-value').attr({
                'type': 'number',
                'minlength': 10,
                'maxlength': 10
            });

            //let inputValue = $('#field-value').val();

            //if (inputValue.length === 10)
            //    $('#field-value-error').addClass('d-none');
            //else {
            //    $('#field-value-error').removeClass('d-none');
            //}
        }

            //What’s App Number
        else if (contact_Type === 'WhatsAppNumber')
        {
            
            $('#field-value').attr({
                'type': 'number',
                'minlength': 10,
                'maxlength': 10
            });

            //let inputValue = $('#field-value').val();

            //if (inputValue.length === 10)
            //    $('#field-value-error').addClass('d-none');
            //else {
            //    $('#field-value-error').removeClass('d-none');
            //}
        }

            //Pager Number
        else if (contact_Type === 'Pager')
        {
            $('#field-value').attr({
                'type': 'number',
                'minlength': 14,
                'maxlength': 14
            });

            //let inputValue = $('#field-value').val();

            //if (inputValue.length === 14)
            //    $('#field-value-error').addClass('d-none');
            //else {
            //    $('#field-value-error').removeClass('d-none');
            //}
        }

            //Home Fax
        else if (contact_Type === 'HomeFax')
        {
            $('#field-value').attr({
                'type': 'number',
                'minlength': 15,
                'maxlength': 15
            });

            //let inputValue = $('#field-value').val();

            //    if (inputValue.length === 15)
            //        $('#field-value-error').addClass('d-none');
            //    else {
            //        $('#field-value-error').removeClass('d-none');
            //    }
        }

            //Work Fax
        else if (contact_Type === 'WorkFax')
        {
            $('#field-value').attr({
                'type': 'number',
                'minlength': 15,
                'maxlength': 15
            });

            //let inputValue = $('#field-value').val();

            //if (inputValue.length === 15)
            //    $('#field-value-error').addClass('d-none');
            //else {
            //    $('#field-value-error').removeClass('d-none');
            //}
        }

            //Work Email
        else if (contact_Type === 'WorkEmail') {
            let email = $('#field-value').val();
            let emailRegex = /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/;

            if (emailRegex.test(email)) {
                $('#field-value-error').addClass('d-none');
                // You can proceed with form submission or other actions here
            } else {
                $('#field-value-error').removeClass('d-none');
            }
        }

            //Home Email
        else if (contact_Type === 'HomeMail') {
            let email = $('#field-value').val();
            let emailRegex = /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/;

            if (emailRegex.test(email)) {
                $('#field-value-error').addClass('d-none');
                // You can proceed with form submission or other actions here
            } else {
                $('#field-value-error').removeClass('d-none');
            }
        }

            //Other Mail
        else if (contact_Type === 'OtherMail') {
            let email = $('#field-value').val();
            let emailRegex = /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/;

            if (emailRegex.test(email)) {
                $('#field-value-error').addClass('d-none');
                // You can proceed with form submission or other actions here
            } else {
                $('#field-value-error').removeClass('d-none');
            }
        }

    }

    function validateContactAndClearError(contact_Type) {
        
        let inputValue = $('#field-value').val();

        let isValid = true;

        if (contact_Type === 'Home') {

            // [Modify By : SS 12/09/2024]
            $('#field-value').attr('maxlength', 50);

            // Optional: If you want to enforce the length limit immediately, you can trim the input value
            if (inputValue.length > 50) {
                $('#field-value').val(inputValue.substring(0, 50));
            }
        }

        if (contact_Type === 'Main') {

            // [Modify By : SS 12/09/2024]
            $('#field-value').attr('maxlength', 50);

            // Optional: If you want to enforce the length limit immediately, you can trim the input value
            if (inputValue.length > 50) {
                $('#field-value').val(inputValue.substring(0, 50));
            }
        }

        if (contact_Type === 'Other') {

            // [Modify By : SS 12/09/2024]
            $('#field-value').attr('maxlength', 50);

            // Optional: If you want to enforce the length limit immediately, you can trim the input value
            if (inputValue.length > 50) {
                $('#field-value').val(inputValue.substring(0, 50));
            }
        }


        if (contact_Type === 'Work') {
            if (inputValue.length > 50) {
                //errorMessage = 'Please Enter A Valid Toll Free Number (e.g., 123456789012345)';
                isValid = false;
            }
        }

        if (contact_Type === 'TollFreeNumber') {
            let maxLength = 15;
            let currentVal = $('#field-value').val();

            // If the length exceeds the max, trim the value
            if (currentVal.length > maxLength) {
                $('#field-value').val(currentVal.substring(0, maxLength));
            }
        }

        else if (contact_Type === 'TollNumber') {
            let maxLength = 15;
            let currentVal = $('#field-value').val();

            // If the length exceeds the max, trim the value
            if (currentVal.length > maxLength) {
                $('#field-value').val(currentVal.substring(0, maxLength));
            }
        }

        else if (contact_Type === 'MisscallNumber') {

            let maxLength = 10;
            let currentVal = $('#field-value').val();

            // If the length exceeds the max, trim the value
            if (currentVal.length > maxLength) {
                $('#field-value').val(currentVal.substring(0, maxLength));
            }
        }

        else if (contact_Type === 'WhatsAppNumber') {
            
            let maxLength = 10;
            let currentVal = $('#field-value').val();

            // If the length exceeds the max, trim the value
            if (currentVal.length > maxLength) {
                $('#field-value').val(currentVal.substring(0, maxLength));
            }
        }

        else if (contact_Type === 'Pager') {
            let maxLength = 14;
            let currentVal = $('#field-value').val();

            // If the length exceeds the max, trim the value
            if (currentVal.length > maxLength) {
                $('#field-value').val(currentVal.substring(0, maxLength));
            }
        }

        else if (contact_Type === 'HomeFax') {
            let maxLength = 15;
            let currentVal = $('#field-value').val();

            // If the length exceeds the max, trim the value
            if (currentVal.length > maxLength) {
                $('#field-value').val(currentVal.substring(0, maxLength));
            }
        }

        else if (contact_Type === 'WorkFax') {
            let maxLength = 15;
            let currentVal = $('#field-value').val();

            // If the length exceeds the max, trim the value
            if (currentVal.length > maxLength) {
                $('#field-value').val(currentVal.substring(0, maxLength));
            }
        }

        else if (contact_Type === 'WorkEmail') {
            inputValue = $('#field-value').val().toString().toLowerCase();
            $('#field-value').val(inputValue);
        }

        else if (contact_Type === 'HomeMail') {
            inputValue = $('#field-value').val().toString().toLowerCase();
            $('#field-value').val(inputValue);
        }

        else if (contact_Type === 'OtherMail') {
            inputValue = $('#field-value').val().toString().toLowerCase();
            $('#field-value').val(inputValue);
        }

        if (isValid)
            $('#field-value-error').addClass('d-none');
        else
            $('#field-value-error').removeClass('d-none');
    }

    // Additional detail EPF Validation
    $('#epf-number').focusout(function () {
        
        // Regular expression for EPF number (Example format: MH/BAN/0001234/5678901)
        let epfRegex = /^[A-Z]{2}\/[A-Z]{3}\/\d{7}\/\d{7}$|^[A-Z]{3}\d{2}\d{7}\d{7}$/;
        let epfNumber = $('#epf-number').val().trim();

        // Trim the EPF number to a maximum of 22 characters
        epfNumber = epfNumber.substring(0, 22);
        $('#epf-number').val(epfNumber); // Update the input field with the trimmed value

        if (epfRegex.test(epfNumber))
            $('#epf-number-error').addClass('d-none');
        else
            $('#epf-number-error').removeClass('d-none');
    });


    // Contact Type Validation
    $('#field-value').focusout(function (event) {

        // If Contact Type Is Mobile
        if (isMobile) {
            $('#verification-code').val('');

            if ($('#field-value').val().length === 10) {
                $('#field-value-mobile-error').addClass('d-none');
                // Check Whether Enter Mobile Number Is Existed Or Not
                filteredData = contactDataTable
                                   .rows()
                                   .indexes()
                                   .filter(function (value, index) {
                                       return contactDataTable.row(value).data()[3] == $('#field-value').val();
                                   });

                if (contactDataTable.rows(filteredData).count() > 0) {
                    isDuplicateContact = true;
                    $('#field-value-duplicate-error').removeClass('d-none');
                }
                else {
                    $('#field-value-duplicate-error').addClass('d-none');
                    isDuplicateContact = false;
                }

                if (!isDuplicateContact) {
                    $('#send-code').removeClass('d-none');
                    $('#resend').addClass('d-none');
                    $('#field-value-error').addClass('d-none');
                }
            }
            else {
                $('#send-code').addClass('d-none');
                $('#field-value-error').addClass('d-none');
                $('#field-value-mobile-error').removeClass('d-none');
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

    // Verification Code Visibility & Input Type Visibility Based On Contact Type
    $('#contact-type').focusout(function (event) {

        $('#field-value').val('');
        $('.modal-input-error').addClass('d-none');

        contactType = $('#contact-type option:selected').text();
        isMobile = contactType.includes('Mobile');
        isEmail = contactType.includes('Email');

        // Add Field Value Attribute Type As Number For Mobile Contact Type
        if (isMobile) {
            $('#field-value').attr('type', 'number');
            $('.is-verified-field').addClass('d-none');
            $('#send-code').removeClass('d-none');
            $('.verification-code').removeClass('d-none');
        }
        else {
            $('#field-value-mobile-error').addClass('d-none');
            $('#field-value').removeAttr('type');
            $('#send-code').addClass('d-none');
            $('.is-verified-field').removeClass('d-none');
            $('#resend').addClass('d-none');
            $('.verification-code').addClass('d-none');
            $('#verification-code').val('0');
        }
    });

    // SMS Service Detail Input Validation
    $('.sms-service-input').focusout(function () {
        if (IsValidSMSServiceDetailAccordionInputs())
            $('#sms-service-accordion-error').addClass('d-none');
    });

    // Email Service Detail Input Validation
    $('.email-service-input').focusout(function () {
        if (IsValidEmailServiceDetailAccordionInputs())
            $('#email-service-accordion-error').addClass('d-none');
    });

    //Document Type Id Kyc Document
    $('#document-type-id-kyc-document').focusout(function () {
        
        $.get('/PersonChildAction/GetDocumentDropdownList', { _documentTypeId: $('#document-type-id-kyc-document').val(), async: false }, function (data, textStatus, jqXHR) {
            
            $('#document-document-type-id').empty();
            documentList = '<option value="0">-- Select document --</option>';
            for (i = 0; i < data.length; i++) {
                documentList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
            }
            $('#document-document-type-id').append(documentList);
        });

    });

    //Document Type Id Kyc Document
    $('#document-document-type-id').focusout(function () {
        
        let filteredDocumentData = personKycDataTable
                .rows()
                .indexes()
                .filter(function (value, index) {
                    return personKycDataTable.row(value).data()[3] == $('#document-document-type-id').val();
                });

        if (personKycDataTable.rows(filteredDocumentData).count() > 0 && editedDocument !== $('#document-document-type-id').val()) {
            $('#document-document-id-error').removeClass('d-none');
        }
        else
            $('#document-document-id-error').addClass('d-none');

    });

    $('#sequence-number-kyc-document').focusout(function () {

        let filteredData = personKycDataTable
            .rows()
            .indexes()
            .filter(function (value, index) {
                return personKycDataTable.row(value).data()[7] == $('#sequence-number-kyc-document').val();
            });

        if (personKycDataTable.rows(filteredData).count() > 0 && editedSequenceNumber !== $('#sequence-number-kyc-document').val())
            $('#sequence-number-kyc-error').removeClass('d-none');
        else
            $('#sequence-number-kyc-error').addClass('d-none');
    });

    //Bank Id
    $('#bank-id').focusout(function (event) {
        branchList = '<option value="">-- Select Branch --</option>';
        branch = $('#bank-branch-id');
        branch.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
        bankId = $('#bank-id').val();
        $('#account-number').val('');
        $('#opening-date').val('');
        $('#is-default-bank-transaction').prop('checked', false);
        $('#photo-path-bank').val('');
        $('#photo-path-bank-image-preview').attr('src', '');
        $('#file-caption-bank').val('');
        $('#note-bank-detail').val('');
        $('.modal-input-error').addClass('d-none');

        $.get('/DynamicDropdownList/GetBankBranchDropdownListByBankId', { _bankId: bankId, async: false }, function (data, textStatus, jqXHR) {
            $('#bank-branch-id').empty();
            for (i = 0; i < data.length; i++) {
                branchList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
            }
            $('#bank-branch-id').append(branchList);
        });

    });

    $('#purchase-price-movable-asset').focusout(function () {
        
        let purchasePrice = parseFloat($('#purchase-price-movable-asset').val());
        let currentMarketPrice = parseFloat($('#current-market-value-movable-asset').val());

        if (!isNaN(purchasePrice)) {
            if (!isNaN(currentMarketPrice) && currentMarketPrice < purchasePrice) {
                $('#current-market-value-movable-asset').val(currentMarketPrice);
            } else {
                $('#current-market-value-movable-asset').val('');
            }
        } else {
            $('#current-market-value-movable-asset').val(''); // Clear monthly Interest is empty
        }
    });

    $('#purchase-price-machinery-asset').focusout(function () {

        
        let purchasePrice = parseFloat($('#purchase-price-machinery-asset').val());
        let currentMarketPrice = parseFloat($('#current-market-value-machinery-asset').val());

        if (!isNaN(purchasePrice)) {
            if (!isNaN(currentMarketPrice) && currentMarketPrice < purchasePrice) {
                $('#current-market-value-machinery-asset').val(currentMarketPrice);
            } else {
                $('#current-market-value-machinery-asset').val('');
            }
        } else {
            $('#current-market-value-machinery-asset').val(''); // Clear monthly Interest is empty
        }
    });

    $('#mortgage-amount').focusout(function () {
        
        let mortgageAmount = parseFloat($('#mortgage-amount').val());
        let sanctionLoanAmount = parseFloat($('#sanction-loan-amount').val());

        if (!isNaN(mortgageAmount)) {
            if (!isNaN(sanctionLoanAmount) && sanctionLoanAmount < mortgageAmount) {
                $('#sanction-loan-amount').val(sanctionLoanAmount);
            } else {
                $('#sanction-loan-amount').val('');
            }
        } else {
            $('#sanction-loan-amount').val('');
        }
    });

    $('#sanction-loan-amount').focusout(function () {
        
        $('#sanction-loan-amount').attr('max', $('#mortgage-amount').val());
    });

    $('#installment-amount').focusout(function () {
        
        $('#installment-amount').attr('max', $('#sanction-loan-amount').val());
    });

    $('#current-market-value-movable-asset').focusout(function () {
        $('#current-market-value-movable-asset').attr('max', $('#purchase-price-movable-asset').val());
    });

    $('#current-market-value-machinery-asset').focusout(function () {
        $('#current-market-value-machinery-asset').attr('max', $('#purchase-price-machinery-asset').val());
    });

    $('#document-document-type-id').focusout(function () {
        $('#document-number-kyc-document').val('');
    });


    $.get('/PersonChildAction/GetPersonAddressDetailEntryStatus', function (data, textStatus, jqXHR) {
        let personInformationDocumentParameter = data;
    });

    let guardianPersonInfo = $('#guardian-pin').val();
    if (guardianPersonInfo !== '') {
        $('#person-information-number-guardian').val(guardianPersonInfo);
    }

    let familyPersonInfo = $('#family-pin').val();
    if (familyPersonInfo !== '') {
        $('#family-person-information-number').val(familyPersonInfo);
    }

    // Validation Vehicle 
    $('#vehicle-model-id').focusout(function (event) {
        letiant = $('#vehicle-variant-id');
        vehicleModelId = $(this).val(); // Using $(this) instead of selecting again
        letiant.empty().append('<option selected disabled>Loading.....</option>'); // Simplified option creation

        // Making AJAX request
        $.get('/DynamicDropdownList/GetVehicleVariantDropdownListByVehicleModelId', { _vehicleModelId: vehicleModelId }, function (data) {
            letiant.empty(); // Clearing options before appending new ones
            letiantList = '<option value="">-- Select Vehicle Variant --</option>';

            // Building options
            $.each(data, function (index, item) {
                letiantList += `<option value="${item.Value}">${item.Text}</option>`; // Using template literals for readability
            });

            letiant.append(letiantList); // Appending options
        });

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
        $('#photo-path-movable').val('');
        $('#photo-path-movable-image-preview').attr('src', '');
        $('#file-caption-movable').val('');
        $('#note-movable-asset').val('');
        $('.modal-input-error').addClass('d-none');
    });

    //Monthly Interest Income Amount
    $('#monthly-interest-income-amount').focusout(function () {
        
        let monthlyInterest = parseFloat($(this).val());
        let investedAmount = parseFloat($('#invested-amount').val());

        if (!isNaN(investedAmount)) {
            monthlyInterest = investedAmount * 0.2;
            $('#monthly-interest-income-amount').val(monthlyInterest);
        } else {
            $('#monthly-interest-income-amount').val(''); // Clear monthly Interest is empty
        }
    });

    $('#filing-dates').click(function () {
        $('#registration-dates').val('');
    });

    //Court Case Accordion registration Date
    $('#registration-dates').click(function () {
        
        let fillingDates = new Date($('#filing-dates').val());
        let today = new Date();  // Get today's date

        // Calculate maximum allowable registration date
        let maxRegistrationDates = new Date(today);

        // Calculate minimum allowable registration date
        let minRegistrationDates = new Date(fillingDates);

        // If filling date is in a past month, set maxRegistrationDate to end of that month
        if (fillingDates < today && fillingDates.getMonth() < today.getMonth()) {
            maxRegistrationDates.setMonth(fillingDates.getMonth() + 1);
            maxRegistrationDates.setDate(0); // Set to last day of the month
        }

        //Modify By Sagar Kare 11-09-2024
        // Set the maximum allowable registration date
        $('#registration-dates').attr('max', GetInputDateFormat(maxRegistrationDates));

        // Set the minimum allowable registration date
        $('#registration-dates').attr('min', GetInputDateFormat(minRegistrationDates));
    });

    //Borrowing Detail Accordion registration Date
    $('#registration-date').click(function () {
        
        let fillingDate = new Date($('#filing-date').val());
        let today = new Date();  // Get today's date

        // Calculate maximum allowable registration date
        let maxRegistrationDate = new Date(today);

        // Calculate minimum allowable registration date
        let minRegistrationDate = new Date(fillingDate);

        // If filling date is in a past month, set maxRegistrationDate to end of that month
        if (fillingDate < today && fillingDate.getMonth() < today.getMonth()) {
            maxRegistrationDate.setMonth(fillingDate.getMonth() + 1);
            maxRegistrationDate.setDate(0); // Set to last day of the month
        }

        // Set the maximum allowable registration date
        $('#registration-date').attr('max', GetInputDateFormat(maxRegistrationDate));

        // Set the minimum allowable registration date
        $('#registration-date').attr('min', GetInputDateFormat(minRegistrationDate));
    });

    $('#date-of-purchase-movable-asset').click(function () {
        $('#registration-date-movable-asset').val('');
    });

    $('#registration-date-movable-asset').click(function () {
        
        let dateOfPurchase = new Date($('#date-of-purchase-movable-asset').val());
        let registrationDate = new Date($(this).val());

        // Calculate minimum allowable registration date
        let minRegistrationDate = new Date(dateOfPurchase);
        minRegistrationDate.setDate(dateOfPurchase.getDate());
        $('#registration-date-movable-asset').attr('min', minRegistrationDate.toISOString().split('T')[0]);
    });

    $('#date-of-request').click(function () {
        $('#date-of-expecting-submit').val('');
        $('#date-of-submit').val('');
    });

    $('#start-date').click(function () {
        $('#maturity-date-person-insurance').val('');
    });

    $('#opening-dates').click(function () {
        $('#maturity-date').val('');
    });

    $('#date-of-expecting-submit').click(function () {
        
        let dateOfRequest = new Date($('#date-of-request').val());
        let dateOfExpectingSubmit = new Date($(this).val());

        // Calculate minimum allowable dateOfExpectingSubmit date
        let minDateOfExpectingSubmit = new Date(dateOfRequest);
        minDateOfExpectingSubmit.setDate(dateOfRequest.getDate());
        $('#date-of-expecting-submit').attr('min', minDateOfExpectingSubmit.toISOString().split('T')[0]);
    });

    $('#date-of-submit').click(function () {
        
        let dateOfRequest = new Date($('#date-of-request').val());
        let dateOfSubmit = new Date($(this).val());

        // Calculate minimum allowable dateOfExpectingSubmit date
        let minDateOfSubmit = new Date(dateOfRequest);
        minDateOfSubmit.setDate(dateOfRequest.getDate());
        $('#date-of-submit').attr('min', minDateOfSubmit.toISOString().split('T')[0]);
    });

    //Carpet Area Validation
    $('#carpet-area').focusout(function () {
        
        $('#carpet-area').attr('max', $('#construction-area').val() - 1);
        $('#carpet-area').attr('min', 20);
    });

    // Sms Send - Display SMS Delilety Status
    $('#send-code').click(function (event) {
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

    // Contact Type Validation
    $('#field-value').focusout(function (event) {
        // If Contact Type Is Mobile
        if (isMobile) {
            $('#verification-code').val('');

            if ($('#field-value').val().length === 10) {
                // Check Whether Enter Mobile Number Is Existed Or Not For Mobile Contact Type
                let filteredData = contactDataTable
                                    .rows()
                                    .indexes()
                                    .filter(function (value, index) {
                                        return contactDataTable.row(value).data()[3] == $('#field-value').val() && contactDataTable.row(value).data()[2].includes('Mobile');
                                    });

                if (contactDataTable.rows(filteredData).count() > 0) {
                    isDuplicateContact = true;
                    $('#field-value-duplicate-error').removeClass('d-none');
                }
                else {
                    $('#field-value-duplicate-error').addClass('d-none');
                    isDuplicateContact = false;
                }

                if (!isDuplicateContact) {
                    $('#send-code').removeClass('d-none');
                    $('#resend').addClass('d-none');
                    $('#field-value-error').addClass('d-none');
                }
            }
            else {
                $('#send-code').addClass('d-none');
                $('#field-value-error').removeClass('d-none');
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

    // Verification Code Visibility & Input Type Visibility Based On Contact Type
    $('#contact-type').focusout(function (event) {
        $('#field-value').val('');

        contactType = $('#contact-type option:selected').text();
        isMobile = contactType.includes('Mobile');
        isEmail = contactType.includes('Email');

        // Add Field Value Attribute Type As Number For Mobile Contact Type
        if (isMobile) {
            $('#field-value').attr('type', 'number');
            $('.is-verified-field').addClass('d-none');
            $('#send-code').removeClass('d-none');
            $('.verification-code').removeClass('d-none');
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

    function ResendSMS() {
        let mobileNumber = $('#field-value').val()

        $.get('/SMS/ReSendTeleVerificataionToken', { MobileNumber: mobileNumber, async: false }, function (data, textStatus, jqXHR) {
            if (data === 'success') {
                $(".link").fadeOut('slow').delay(30000).fadeIn("slow");
                $("#myToast").toast('show').css({ "z-index": "100", 'margin-top': "1%" });
            }
        });

    }

    $('#address-type-id').focusout(function () {
        $('#flat-door-no').val('');
        $('#trans-flat-door-no').val('');
        $('#building-name').val('');
        $('#trans-building-name').val('');
        $('#road-name').val('');
        $('#trans-road-name').val('');
        $('#area-name').val('');
        $('#trans-area-name').val('');
        $('#city-id').val('');
        $('#residence-type-id').val('');
        $('#residence-ownership-id').val('');
        $('#is-verified-address').prop('checked', false);
        $('#note-address').val('');
        $('#trans-note-address').val('');
        $('.modal-input-error').addClass('d-none');
    });

    $('#contact-type').focusout(function () {
        $('#field-value').val('');
        $('#is-verified').prop('checked', false);
        $('#note-contact-detail').val('');
    });

    $('#home-branch-id').focusout(function () {
        $('#language-id').val('');
        $('#activation-date-home-branch').val('');
        $('#note-home-branch').val('');
    });

    $('#disease-id').focusout(function () {
        $('#other-detail').val('');
        $('#note-chronic-disease').val('');
    });

    $('#social-media-id').focusout(function () {
        $('#social-media-link').val('');
        $('#other-details-social-media').val('');
        $('#note-social-media').val('');
    });

    $('#insurance-type-id').focusout(function () {
        $('#insurance-company-id').val('');
        $('#start-date').val('');
        $('#maturity-date-person-insurance').val('');
        $('#policy-number').val('');
        $('#policy-premium').val('');
        $('#policy-sum-assured').val('');
        $('#overdues-premium').val('');
        $('#has-any-mortgage-insurance').prop('checked', false);
        $('#note-insurance-detail').val('');
        $('.modal-input-error').addClass('d-none');
    });

    $('#financial-organization-type').focusout(function () {
        $('#insurance-company-id').val('');
        $('#name-of-financial-organization').val('');
        $('#trans-name-of-financial-organization').val('');
        $('#name-of-branch').val('');
        $('#trans-name-of-branch').val('');
        $('#address-details').val('');
        $('#trans-address-details').val('');
        $('#contact-details').val('');
        $('#trans-contact-details').val('');
        $('#opening-dates').val('');
        $('#maturity-date').val('');
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
        $('#file-caption-finance').val('');
        $('#note-financial-asset').val('');
        $('#trans-note-financial-asset').val('');
        $('.modal-input-error').addClass('d-none');
    });

    $('#agriculture-land-type-id').focusout(function () {
        $('#agriculture-land-description').val('');
        $('#survey-number').val('');
        $('#group-number').val('');
        $('#start-area-of-land').val('');
        $('#volume').val('');
        $('#ownership-type-id').val('');
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
        $('.modal-input-error').addClass('d-none');
    });

    $('#date-of-issue-kyc-document').click(function () {
        $('#date-of-expiry-kyc-document').val('');
    });

    $('#open-date').click(function () {
        $('#mature-date').val('');
    });

    $('#filing-date').click(function () {
        $('#registration-date').val('');
    });

    $('#enable-gst-return-document').change(function () {
        gstDataTable.clear().draw();
        $('#gst-registration-accordion-error').addClass('d-none');
    });

    $('#state-id').focusout(function () {
        
        $('#gst-registration-type-id').val('');
        $('#applicable-from').val('');
        $('#gst-return-periodicity-id').val('');
        $('#threshold-limit').val('');
        $('#gst-registration-number').val('');
        $('#registrations-date').val('');
        $('#gst-return-document-block').addClass('d-none');;
        $('#is-applicable-eway-bill').prop('checked', false);

        $('#enable-gst-return-document').prop('checked', false);
        gstDataTable.clear().draw();

        //let enableGstReturnDocument = $('#enable-gst-return-document').prop('checked');

        //if (enableGstReturnDocument == true) {
        //    $('#enable-gst-return-document').prop('checked', false);
        //    $('#gst-return-document-block').hide();
        //    gstDataTable.clear().draw();
        //} else if (enableGstReturnDocument) {
        //    $('#enable-gst-return-document').prop('checked', true);
        //    $('#gst-return-document-block').show();
        //}

        $('#note-gst-document').val('');
    });

    //[Modify By Suraj Sonawane 13/09/2024]
    $('#enable-authorized-signatory').change(function () {
        $('#photo-path-sign').val('');
        $('#photo-path-sign-image-preview').attr('src', '');
        $('#file-caption-sign').val('');
        $('#note-board-of-director-authorized').val('');
    });


    //$('.upper-case').on('input paste', function () {
    //    // Use a timeout to ensure that the paste event has completed
    //    setTimeout(() => {
    //        // Get the current value and convert it to uppercase
    //        let uppercaseValue = $(this).val().toUpperCase();
    //        // Set the uppercase value back to the input field
    //        $(this).val(uppercaseValue);
    //    }, 0);
    //});

    $('.upper-case').on('keyup paste', function () {
        let $this = $(this);
        let maxLength = $this.attr('maxlength');

        // Convert the current value to uppercase and enforce maxlength
        let uppercaseValue = $this.val().toUpperCase().slice(0, maxLength);
        // Set the uppercase value back to the input field
        $this.val(uppercaseValue);
    });


    // @@@@@@@@@@@@@@@@@@@@@@@@@@   VALIDATION FOCUSOUT FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Enable Home Branch Accordion Input Validation
    $('.home-branch-input').focusout(function () {
        if (IsValidHomeBranchAccordionInputs())
            $('#home-branch-accordion-error').addClass('d-none');
    });

    // Enable Additional Details Accordion Input Validation
    $('.additional-details-input').focusout(function () {
        
        if (IsValidAdditionalDetailsAccordionInputs())
            $('#person-additional-details-accordion-error').addClass('d-none');
    });

    // Enable Guardian Details Accordion Input Validation
    $('.guardian-details-input').focusout(function () {
        if (IsValidGuardianAccordionInputs())
            $('#guardian-details-accordion-error').addClass('d-none');
    });

    // Enable Person Photo Sign Accordion Input Validation
    $('.person-photo-sign-input').focusout(function () {
        
        IsValidPhotoSignAccordionInputs();
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


    // @@@@@@@@@@@@@@@@@@@@@@@@@@   VALIDATION FUNCTIONS FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    //Modify By---Sagar Kare -09-09-20224
    // 1. Home Branch Accordion Input Validation
    function IsValidHomeBranchAccordionInputs() {
        
        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-home-branch');

        result = true;


        //Activation Date
        if (isValidActivationDate === false) {
            result = false;
        }

        //Home Branch Id
        if (parseInt($('#home-branch-id').prop('selectedIndex')) < 1) {
            result = false;
        }

        //Language Id
        if (parseInt($('#language-id').prop('selectedIndex')) < 1) {
            result = false;
        }

        // Show or hide error message based on validation result
        if (result)
            $('#home-branch-accordion-error').addClass('d-none');
        else
            $('#home-branch-accordion-error').removeClass('d-none');

        return result;
    }

    // 2.Additional Details Accordion Input Validation
    function IsValidAdditionalDetailsAccordionInputs() {
        
        _occupationType;
        _maritalStatus;

        let lifePartnerName = $('#life-partner-name').val();
        let transLifePartnerName = $('#trans-life-partner-name').val();
        let lifePartnerMaidenName = $('#life-partner-maiden-name').val();
        let transLifePartnerMaidenName = $('#trans-life-partner-maiden-name').val();
        let dateOfMarriage = $('#date-of-marriage').val();

        let isEmployee = $('#enable-employee').is(':checked');
        let nameOfEmployer = $('#employer-name').val();
        let transNameOfEmployer = $('#trans-employer-name').val();
        let dateOfIncorporation = $('#date-of-incorporation').val();
        let annualIncome = parseFloat($('#annual-income').val());
        let epfNumber = $('#epf-number').val();
        let transEpfNumber = $('#trans-epf-number').val();
        let employedSince = parseFloat($('#employed-since').val());

        let employerNatureOtherDetails = $('#employer-nature-other-details').val();
        let transEmployerNatureOtherDetails = $('#trans-employer-nature-other-details').val();

        let employerAddressDetails = $('#employer-address-details').val();
        let transEmployerAddressDetails = $('#trans-employer-address-details').val();

        let employerContactDetails = $('#employer-contact-details').val();
        let transEmployerContactDetails = $('#trans-employer-contact-details').val();

        let employmentTypeId = $('#employment-type-id option:selected').val();
        let employmentTypeIdText = $('#employment-type-id option:selected').text();

        let natureOfEmployerId = $('#nature-of-employer-id option:selected').val();
        let natureOfEmployerIdText = $('#nature-of-employer-id option:selected').text();

        let employerCityId = $('#employer-city-id option:selected').val();
        let employerCityIdText = $('#employer-city-id option:selected').text();

        let designationId = $('#designation-id option:selected').val();
        let designationIdText = $('#designation-id option:selected').text();

        let maritalStatusId = $('#marital-status').val();
        let occupationId = $('#occupation-id-drop').val();

        let note = $('#note-person-additional-detail').val();
        let transNote = $('#trans-note-person-additional-detail').val();

        let noteEmployement = $('#note-person-employment-detail').val();
        let transNoteEmployement = $('#trans-note-person-employment-detail').val();

        let isSubmitedForm60 = $('#is-submited-form-60').is(':checked') ? true : false;
        let isIncomeTaxPayer = $('#is-income-tax-payer').is(':checked') ? true : false;
        let isSubmitedForm15G = $('#is-submited-form-15g').is(':checked') ? true : false;
        let isSubmitedForm15H = $('#is-submited-form-15h').is(':checked') ? true : false;
        let isGSTTaxPayer = $('#is-gst-tax-payer').is(':checked') ? true : false;

        let enablePoliticion = $('#enable-politician').is(':checked');
        let politicialBackgroundDetails = $('#politicial-background-details').val();
        let transPoliticialBackgroundDetails = $('#trans-politicial-background-details').val();

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

        let vipRank = $('#vip-rank').val();

        result = true;

        // Check if 'person-category-id' dropdown has a valid selection (not the default)
        if (parseInt($('#person-category-id').prop('selectedIndex')) < 1) {
            result = false;
        }

        // Check if 'birth-city' has the 'False' class and its dropdown has a valid selection
        if ($('#birth-city').hasClass('d-none') === false && parseInt($('#birth-city').prop('selectedIndex')) < 1) {
            result = false;
        }

        // Check if 'maritalStatusId' has the 'False' class and its dropdown has a valid selection
        if ($(maritalStatusId).hasClass('d-none') === false && parseInt($(maritalStatusId).prop('selectedIndex')) < 1) {
            result = false;
        }

        // Check if 'blood-group-id' has the 'False' class and its dropdown has a valid selection
        if ($('#blood-group-id').hasClass('d-none') === false && parseInt($('#blood-group-id').prop('selectedIndex')) < 1) {
            result = false;
        }

        // Check if 'poverty-status-id' has the 'False' class and its dropdown has a valid selection
        if ($('#poverty-status-id').hasClass('d-none') === false && parseInt($('#poverty-status-id').prop('selectedIndex')) < 1) {
            result = false;
        }

        // Check if 'physical-status-id' has the 'False' class and its dropdown has a valid selection
        if ($('#physical-status-id').hasClass('d-none') === false && parseInt($('#physical-status-id').prop('selectedIndex')) < 1) {
            result = false;
        }

        // Check if 'cast-category-id' has the 'False' class and its dropdown has a valid selection
        if ($('#cast-category-id').hasClass('d-none') === false && parseInt($('#cast-category-id').prop('selectedIndex')) < 1) {
            result = false;
        }

        // Check if 'educational-qualification-id' has the 'False' class and its dropdown has a valid selection
        if ($('#educational-qualification-id').hasClass('d-none') === false && parseInt($('#educational-qualification-id').prop('selectedIndex')) < 1) {
            result = false;
        }

        // Check if 'vipRank' is a valid number and not empty
        if (isNaN(vipRank) || vipRank === '') {
            result = false;
        }

        // Check if 'gender-id' has the 'False' class and its dropdown has a valid selection
        if ($('#gender-id').hasClass('d-none') === false && parseInt($('#gender-id').prop('selectedIndex')) < 1) {
            result = false;
        }

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        if (transNote === '')
            transNote = 'None';

        //if (_maritalStatus != '') {

        if (_maritalStatus === "MARRID") {

            // Gst Life Partner Name
            if (isNaN(lifePartnerName.length) === false) {
                minimumLength = parseInt($('#life-partner-name').attr('minlength'));
                maximumLength = parseInt($('#life-partner-name').attr('maxlength'));

                if (parseInt(lifePartnerName.length) < parseInt(minimumLength) || parseInt(lifePartnerName.length) > parseInt(maximumLength)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Trans Life Partner Name
            if (isNaN(transLifePartnerName.length) === false) {
                minimumLength = parseInt($('#trans-life-partner-name').attr('minlength'));
                maximumLength = parseInt($('#trans-life-partner-name').attr('maxlength'));

                if (parseInt(transLifePartnerName.length) < parseInt(minimumLength) || parseInt(transLifePartnerName.length) > parseInt(maximumLength)) {
                    result = false;
                }
            } else {
                result = false;
            }

            //Life Partner Maiden Name
            if (isNaN(lifePartnerMaidenName.length) === false) {
                minimumLength = parseInt($('#life-partner-maiden-name').attr('minlength'));
                maximumLength = parseInt($('#life-partner-maiden-name').attr('maxlength'));

                if (parseInt(lifePartnerMaidenName.length) < parseInt(minimumLength) || parseInt(lifePartnerMaidenName.length) > parseInt(maximumLength)) {
                    result = false;
                }
            } else {
                result = false;
            }

            //Trans Life Partner Maiden Name
            if (isNaN(transLifePartnerMaidenName.length) === false) {
                minimumLength = parseInt($('#trans-life-partner-maiden-name').attr('minlength'));
                maximumLength = parseInt($('#trans-life-partner-maiden-name').attr('maxlength'));

                if (parseInt(transLifePartnerMaidenName.length) < parseInt(minimumLength) || parseInt(transLifePartnerMaidenName.length) > parseInt(maximumLength)) {
                    result = false;
                }
            } else {
                result = false;
            }

            if (dateOfMarriage === false) {
                result = false;
            }
        }

        if (_occupationType !== '') {

            if (_occupationType === "SLRD") {
                if (isEmployee === false) {


                    // Validate Name of Employer
                    if (isNaN(nameOfEmployer.length) === false) {
                        minimumLength = parseInt($('#employer-name').attr('minlength'));
                        maximumLength = parseInt($('#employer-name').attr('maxlength'));

                        if (parseInt(nameOfEmployer.length) < parseInt(minimumLength) || parseInt(nameOfEmployer.length) > parseInt(maximumLength)) {
                            result = false;
                        }
                    } else {
                        result = false;
                    }

                    // Validate Trans Name of Employer
                    if (isNaN(transNameOfEmployer.length) === false) {
                        minimumLength = parseInt($('#trans-employer-name').attr('minlength'));
                        maximumLength = parseInt($('#trans-employer-name').attr('maxlength'));

                        if (parseInt(transNameOfEmployer.length) < parseInt(minimumLength) || parseInt(transNameOfEmployer.length) > parseInt(maximumLength)) {
                            result = false;
                        }
                    } else {
                        result = false;
                    }


                    let isValidDateOfIncorporation = IsValidInputDate('#date-of-incorporation');

                    if (isValidDateOfIncorporation === false) {
                        result = false;
                    }

                    if (parseInt($('#employment-type-id').prop('selectedIndex')) < 1) {
                        result = false;
                    }

                    if (parseInt($('#nature-of-employer-id').prop('selectedIndex')) < 1) {
                        result = false;
                    }

                    if (parseInt($('#employer-city-id').prop('selectedIndex')) < 1) {
                        result = false;
                    }

                    if (parseInt($('#designation-id').prop('selectedIndex')) < 1) {
                        result = false;
                    }

                    // Annual Income
                    if (isNaN(annualIncome) === false) {
                        minimum = parseFloat($('#annual-income').attr('min'));
                        maximum = parseFloat($('#annual-income').attr('max'));

                        if (parseFloat(annualIncome) < parseFloat(minimum) || parseFloat(annualIncome) > parseFloat(maximum))
                            result = false;
                    }
                    else
                        result = false;

                    // Validate EPF Number
                    if (isNaN(epfNumber.length) === false) {
                        minimumLength = parseInt($('#epf-number').attr('minlength'));
                        maximumLength = parseInt($('#epf-number').attr('maxlength'));

                        if (parseInt(epfNumber.length) < parseInt(minimumLength) || parseInt(epfNumber.length) > parseInt(maximumLength)) {
                            result = false;
                        }
                    } else {
                        result = false;
                    }

                    // Validate EPF Number
                    if (isNaN(transEpfNumber.length) === false) {
                        minimumLength = parseInt($('#trans-epf-number').attr('minlength'));
                        maximumLength = parseInt($('#trans-epf-number').attr('maxlength'));

                        if (parseInt(transEpfNumber.length) < parseInt(minimumLength) || parseInt(transEpfNumber.length) > parseInt(maximumLength)) {
                            result = false;
                        }
                    } else {
                        result = false;
                    }



                    // Annual Income
                    if (isNaN(employedSince) === false) {
                        minimum = parseFloat($('#employed-since').attr('min'));
                        maximum = parseFloat($('#employed-since').attr('max'));
                        if (parseFloat(employedSince) < parseFloat(minimum) || parseFloat(employedSince) > parseFloat(maximum))
                            result = false;
                    }
                    else
                        result = false;


                    // Validate Employer Nature Other Details
                    if (isNaN(employerNatureOtherDetails.length) === false) {
                        minimumLength = parseInt($('#employer-nature-other-details').attr('minlength'));
                        maximumLength = parseInt($('#employer-nature-other-details').attr('maxlength'));

                        if (parseInt(employerNatureOtherDetails.length) < parseInt(minimumLength) || parseInt(employerNatureOtherDetails.length) > parseInt(maximumLength)) {
                            result = false;
                        }
                    } else {
                        result = false;
                    }

                    // Validate Trans Employer Nature Other Details
                    if (isNaN(transEmployerNatureOtherDetails.length) === false) {
                        minimumLength = parseInt($('#trans-employer-nature-other-details').attr('minlength'));
                        maximumLength = parseInt($('#trans-employer-nature-other-details').attr('maxlength'));

                        if (parseInt(transEmployerNatureOtherDetails.length) < parseInt(minimumLength) || parseInt(transEmployerNatureOtherDetails.length) > parseInt(maximumLength)) {
                            result = false;
                        }
                    } else {
                        result = false;
                    }

                    // Validate Employer Address Details
                    if (isNaN(employerAddressDetails.length) === false) {
                        minimumLength = parseInt($('#employer-address-details').attr('minlength'));
                        maximumLength = parseInt($('#employer-address-details').attr('maxlength'));

                        if (parseInt(employerAddressDetails.length) < parseInt(minimumLength) || parseInt(employerAddressDetails.length) > parseInt(maximumLength)) {
                            result = false;
                        }
                    } else {
                        result = false;
                    }

                    // Validate Trans Employer Address Details
                    if (isNaN(transEmployerAddressDetails.length) === false) {
                        minimumLength = parseInt($('#trans-employer-address-details').attr('minlength'));
                        maximumLength = parseInt($('#trans-employer-address-details').attr('maxlength'));

                        if (parseInt(transEmployerAddressDetails.length) < parseInt(minimumLength) || parseInt(transEmployerAddressDetails.length) > parseInt(maximumLength)) {
                            result = false;
                        }
                    } else {
                        result = false;
                    }

                    // Validate Employer Contact Details
                    if (isNaN(employerContactDetails.length) === false) {
                        minimumLength = parseInt($('#employer-contact-details').attr('minlength'));
                        maximumLength = parseInt($('#employer-contact-details').attr('maxlength'));

                        if (parseInt(employerContactDetails.length) < parseInt(minimumLength) || parseInt(employerContactDetails.length) > parseInt(maximumLength)) {
                            result = false;
                        }
                    } else {
                        result = false;
                    }

                    // Validate Trans Employer Contact Details
                    if (isNaN(transEmployerContactDetails.length) === false) {
                        minimumLength = parseInt($('#trans-employer-contact-details').attr('minlength'));
                        maximumLength = parseInt($('#trans-employer-contact-details').attr('maxlength'));

                        if (parseInt(transEmployerContactDetails.length) < parseInt(minimumLength) || parseInt(transEmployerContactDetails.length) > parseInt(maximumLength)) {
                            result = false;
                        }
                    }

                    else {
                        result = false;
                    }

                }
            }
        } else
            result = false;

        if (enablePoliticion === true) {

            // Validate Politicial Background Details
            if (isNaN(politicialBackgroundDetails.length) === false) {
                minimumLength = parseInt($('#politicial-background-details').attr('minlength'));
                maximumLength = parseInt($('#politicial-background-details').attr('maxlength'));

                if (parseInt(politicialBackgroundDetails.length) < parseInt(minimumLength) || parseInt(politicialBackgroundDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }
            } else {
                result = false;
            }

            // Validate Trans Politicial Background Details
            if (isNaN(transPoliticialBackgroundDetails.length) === false) {
                minimumLength = parseInt($('#trans-politicial-background-details').attr('minlength'));
                maximumLength = parseInt($('#trans-politicial-background-details').attr('maxlength'));

                if (parseInt(transPoliticialBackgroundDetails.length) < parseInt(minimumLength) || parseInt(transPoliticialBackgroundDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }
            } else {
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
        else
            result = false;

        if (result)
            $('#person-additional-details-accordion-error').addClass('d-none');
        else
            $('#person-additional-details-accordion-error').removeClass('d-none');

        return result;
    }

    // 3.Guardian Details Accordion Input Validation
    function IsValidGuardianAccordionInputs() {
        
        result = true;

        let personInformationNumberGuardian = guardianPersonInfoId;
        let personInformationNumberGuardianText = $('#person-information-number-guardian option:selected').text();
        let fullName = $('#guardian-full-name').val();
        let transFullName = $('#trans-guardian-full-name').val();
        let FullAddress = $('#full-address').val();
        let transFullAddress = $('#trans-full-address').val();
        let note = $('#note-guardian').val();
        let transNote = $('#trans-note-guardian').val();

        //let isSelectedPersonInformationNumberGuardian = false;

        if (!$('#heading-guardian-details').hasClass('d-none')) {

            //Person Information Number Id
            if (parseInt($('#person-information-number-guardian').prop('selectedIndex')) < 1) {
                result = false;
            }

            //Relation Guardian Id
            if (parseInt($('#relation-guardian-id').prop('selectedIndex')) < 1) {
                result = false;
            }

            // Age Proof Submission Status 
            if ($('.age-proof-submission-status:checked').length === 0) {
                result = false;
            }

            // Set Default Value, If Empty
            if (note === '')
                note = 'None';

            if (transNote === '')
                transNote = 'None';

            //if (personInformationNumberGuardian === '') {
            //    $('#guardian-person-information-error').removeClass('d-none');
            //} else {
            //    $('#guardian-person-information-error').addClass('d-none');
            //}

            let isExistingPerson = $('#person-information-number-guardian').hasClass('d-none');

            // Assign Default Values If Guardian Is Existing Customer
            if (!isExistingPerson) {
                fullName = 'None';
                transFullName = 'None';
                FullAddress = 'None';
                transFullAddress = 'None';
                personInformationNumberGuardianText = $('#person-information-number-guardian').val();
            }
            else {
                fullName = $('#guardian-full-name').val();
                transFullName = $('#trans-guardian-full-name').val();
                FullAddress = $('#full-address').val();
                transFullAddress = $('#trans-full-address').val();
                personInformationNumberGuardianText = 'None';
                personInformationNumberGuardian = 0;
            }

            if (fullName === '') {
                result = false;
            }

            if (transFullName === '') {
                result = false;
            }

            if (FullAddress === '') {
                result = false;
            }

            if (transFullAddress === '') {
                result = false;
            }
            

            ////Validation For  Person Information Number
            //if (personInformationNumberGuardian == '' || personInformationNumberGuardian == 'None') {
            //    fullName = $('#guardian-full-name').val();
            //    transFullName = $('#trans-guardian-full-name').val();
            //    FullAddress = $('#full-address').val();
            //    transFullAddress = $('#trans-full-address').val();
            //}
            //else {
            //    fullName = 'None';
            //    transFullName = 'None';
            //    FullAddress = 'None';
            //    transFullAddress = 'None';
            //}
            //if (personInformationNumberGuardian == '' || typeof personInformationNumberGuardian == 'undefined')
            //    personInformationNumberGuardian = 'None';

            //// Check Whether Person Information Number Selected For Nominee Or Not?
            //if (personInformationNumberGuardian == 'None' || typeof personInformationNumberGuardian == 'undefined') {
            //    isSelectedPersonInformationNumberGuardian = false;
            //} else {
            //    isSelectedPersonInformationNumberGuardian = true;
            //}

            //if (!$('#guardian-person-information').hasClass('d-none')) {
            //    if (isSelectedPersonInformationNumberGuardian == false) {
            //        result = false;
            //        $('#guardian-person-information-error').removeClass('d-none');
            //    } else {
            //        $('#guardian-person-information-error').addClass('d-none');
            //    }
            //}
            //else {
            //    personInformationNumberGuardianText = 'None';
            //}

           
        }
        if (result) {
            $('#guardian-details-accordion-error').addClass('d-none');
        } else {
            $('#guardian-details-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // 4.Photo Sign Accordion Input Validation
    function IsValidPhotoSignAccordionInputs() {
        
        let result = true;

        if ($('#enable-authorized-signatory').is(':checked')) {

            let personPhotoPath = $('#person-photo-path').val();
            let signPath = $('#sign-path').val();

            if (personPhotoPath === '') {
                result = false;
            }

            if (signPath === '') {
                result = false;
            }
        }

        if (result)
            $('#person-photo-sign-accordion-error').addClass('d-none');
        else
            $('#person-photo-sign-accordion-error').removeClass('d-none');

        return result;
    }

    // 5.Person GST Registration Accordion Input Validation
    function IsValidGstRegistrationAccordionInputs() {
        
        result = true;

        let thresholdLimit = parseInt($('#threshold-limit').val());
        let taxAmount = parseFloat($('#tax-amount').val());
        let gstRegistration = $('#gst-registration-number').val();

        let uploadGSTReturnDocument = $('#enable-gst-return-document').is(':checked') ? true : false;

        // Set the minimum date attribute to '2017-07-01'
        let applicableFrom = $('#applicable-from').val('2017-07-01');
        $('#applicable-from').attr('read-only', true);

        let registrationDate = $('#registrations-date').val();

        //state Id
        if (parseInt($('#state-id').prop('selectedIndex')) < 1) {
            result = false;
        }

        //Gst Registration Type Id
        if (parseInt($('#gst-registration-type-id').prop('selectedIndex')) < 1) {
            result = false;
        }

        //Activation Date
        if (applicableFrom === false) {
            result = false;
        }

        //state Id
        if (parseInt($('#gst-return-periodicity-id').prop('selectedIndex')) < 1) {
            result = false;
        }

        // Threshold Limit
        if (isNaN(thresholdLimit) === false) {
            minimum = parseInt($('#threshold-limit').attr('min'));
            maximum = parseInt($('#threshold-limit').attr('max'));

            if (parseInt(thresholdLimit) < 2000000 || parseInt(thresholdLimit) > 4000000)
                result = false;
        }
        else
            result = false;

        // Gst Registration
        if (isNaN(gstRegistration.length) === false) {
            minimumLength = parseInt($('#gst-registration-number').attr('minlength'));
            maximumLength = parseInt($('#gst-registration-number').attr('maxlength'));

            if (parseInt(gstRegistration.length) < parseInt(minimumLength) || parseInt(gstRegistration.length) > parseInt(maximumLength)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        //Registration Date
        if (registrationDate === false) {
            result = false;
        }

        if (result)
            $('#gst-registration-details-accordion-error').addClass('d-none');
        else
            $('#gst-registration-details-accordion-error').removeClass('d-none');

        return result;
    }

    // 5.Person Commodities Asset Accordion Input Validation
    function IsValidCommoditiesAssetAccordionInputs() {
        
        let goldOrnament = parseFloat($('#gold-ornaments').val());
        let silverOrnament = parseFloat($('#silver-ornaments').val());
        let platinumOrnament = parseFloat($('#platinum-ornaments').val());
        let diamondInGoldOrnament = parseInt($('#number-of-diamonds-in-gold-ornaments').val());
        result = true;

        if (!$('#heading-commodities-asset').hasClass('d-none')) {

            // Gold Ornament
            if (isNaN(goldOrnament) === false) {
                minimum = parseFloat($('#gold-ornaments').attr('min'));
                maximum = parseFloat($('#gold-ornaments').attr('max'));

                if (parseFloat(goldOrnament) < parseFloat(minimum) || parseFloat(goldOrnament) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            // Silver Ornament
            if (isNaN(silverOrnament) === false) {
                minimum = parseFloat($('#silver-ornaments').attr('min'));
                maximum = parseFloat($('#silver-ornaments').attr('max'));

                if (parseFloat(silverOrnament) < parseFloat(minimum) || parseFloat(silverOrnament) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            // Platinum Ornament
            if (isNaN(platinumOrnament) === false) {
                minimum = parseFloat($('#platinum-ornaments').attr('min'));
                maximum = parseFloat($('#platinum-ornaments').attr('max'));

                if (parseFloat(platinumOrnament) < parseFloat(minimum) || parseFloat(platinumOrnament) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            // Diamond Gold Ornament
            if (isNaN(diamondInGoldOrnament) === false) {
                minimum = parseInt($('#number-of-diamonds-in-gold-ornaments').attr('min'));
                maximum = parseInt($('#number-of-diamonds-in-gold-ornaments').attr('max'));

                if (parseInt(diamondInGoldOrnament) < parseInt(minimum) || parseInt(diamondInGoldOrnament) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

        }

        if (result)
            $('#commodities-asset-accordion-error').addClass('d-none');
        else
            $('#commodities-asset-accordion-error').removeClass('d-none');

        return result;
    }

    $('#person-type-id').focusout(function () {
        debugger;
        CheckPersonType(personTypeId);

        // Mark Out Select All Check Box Of All Datatables.
        $('input[name="select-all"]').prop('checked', false);

        // Clear Accordion Title Error Messages
        $('.accordion-title-error').addClass('d-none');

        // Make False All Toggle Switch
        $('.switch-input').prop('checked', false);

        //Clear all number fiels and textarea
        $('input[type="number"]').val('');

        //Clear all TextArea 
        $('textarea').val('');

        // Make False All Toggle Switch
        $('.switch-input').prop('checked', false);

        $('input[type="text"]').val('');

        $('input[type="date"]').val('');

        // Clear dropdowns and MultiSelect to the first option except Loan Type
        $('select').not('#person-type-id').prop('selectedIndex', 0);

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
        boardOfDirectorAuthorizedDataTable.clear().draw();
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
        borrowingDataTable.clear().draw();
        creditDataTable.clear().draw();
        courtCaseDataTable.clear().draw();
        socialMediaDataTable.clear().draw();
        smsAlertDataTable.clear().draw();
        incomeTaxDataTable.clear().draw();

        $('#enable-gst-registration-details-input').addClass('read-only');
        $('#enable-gst-registration-details').prop('checked', false);

        // Hide All Accordion Based On Toggle Switch s
        SetToggleSwitchBasedAccordions();

    });

    function CheckPersonType(personTypeId) {
        debugger;
        personTypeId = $('#person-type-id').val();

        if (personTypeId !== '') {
            $.get('/PersonChildAction/GetSysNameOfPersonTypeById', { _personTypeId: personTypeId, async: false }, function (data) {
                debugger;
                sysNameOfPersonType = data;

                if (sysNameOfPersonType == INDIVIDUAL) {
                    $('.individual').removeClass('d-none');
                    $('.group').addClass('d-none');
                    $('.occupation').addClass('d-none');
                } else {
                    $('.individual').addClass('d-none');
                    $('.group').removeClass('d-none');
                    $('.occupation').addClass('d-none');
                    $('.guardian').addClass('d-none');
                }
            });
        }
        else {
            $('.individual').addClass('d-none');
            $('.group').addClass('d-none');
            $('.occupation').addClass('d-none');
        }
    }


    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Address detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// #################   Person  Address Detail - DataTable Code 

    // DataTable Add Button 
    $('#btn-add-person-address-dt').click(function () {
        
        event.preventDefault();
        editedAddressTypeId = '';
        SetAddressTypeUniqueDropdownList();
        SetModalTitle('person-address', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-person-address-dt').click(function () {
        
        SetModalTitle('person-address', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-person-address-dt').data('rowindex');
            id = $('#person-address-modal').attr('id');
            editedAddressTypeId = columnValues[1];

            myModal = $('#' + id).modal();

            //// Display Value In Modal Inputs
            // Get Person Address EntryStatus
            $.get('/PersonChildAction/GetDocumentValidations', { _personAddressDetailPrmKey: columnValues[21], async: false }, function (data, textStatus, jqXHR) {
                entryStatus = data;
            });

            if ((columnValues[21] !== 0) && (entryStatus === 'VRF'))
                $('.person-address-details').addClass('read-only');
            else
                $('.person-address-details').removeClass('read-only');

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
            $('#residence-ownership-id', myModal).val(columnValues[15]);
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

            // Hide Table Required Data Error Message
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
                    if (!addressDataTable.data().any())
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
        enable - gst - registration - Details - input
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
        residenceOwnership = $('#residence-ownership-id option:selected').val();
        residenceOwnershipText = $('#residence-ownership-id option:selected').text();
        note = $('#note-address').val();
        transNote = $('#trans-note-address').val();
        reasonForModification = $('#reason-for-modification-address').val();
        hasDivClass = $('#address-div').hasClass('d-none');
        personAddressPrmKey = 0;

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        if (transNote === '')
            transNote = 'None';
        /// **********
        if (hasDivClass === true) {
            reasonForModification = 'None';
        }
        else {
            if (reasonForModification == '')
                reasonForModification = 'None';
        }

        //Validation Address Type
        if (addressType === '') {
            result = false;
            $('#address-type-id-error').removeClass('d-none')
        } else
            $('#address-type-id-error').addClass('d-none')

        // ******** minimumLength  maximumLength
        //Validation FlatDoor No Min Length  And Max Length 

        minimumLength = parseInt($('#flat-door-no').attr('minlength'));
        maximumLength = parseInt($('#flat-door-no').attr('maxlength'));

        if (flatDoorNo === '' || parseInt(flatDoorNo.length) < parseInt(minimumLength) || parseInt(flatDoorNo.length) > parseInt(maximumLength)) {
            result = false;
            $('#flat-door-no-error').removeClass('d-none');
        }
        else
            $('#flat-door-no-error').addClass('d-none');


        //Validation Trans FlatDoor No
        if (transFlatDoorNo === '') {
            result = false;
            $('#trans-flat-door-no-error').removeClass('d-none')
        }
        else
            $('#trans-flat-door-no-error').addClass('d-none')


        //Validation Building Name
        minimumLength = parseInt($('#building-name').attr('minlength'));
        maximumLength = parseInt($('#building-name').attr('maxlength'));

        if (buildingName === '' || parseInt(buildingName.length) < parseInt(minimumLength) || parseInt(buildingName.length) > parseInt(maximumLength)) {
            result = false;
            $('#building-name-error').removeClass('d-none');
        } else
            $('#building-name-error').addClass('d-none');


        //Validation Trans Building Name
        if (transBuildingName === '') {
            result = false;
            $('#trans-building-name-error').removeClass('d-none')
        } else
            $('#trans-building-name-error').addClass('d-none')

        //Validation Road Name
        minimumLength = parseInt($('#road-name').attr('minlength'));
        maximumLength = parseInt($('#road-name').attr('maxlength'));

        if (roadName === '' || parseInt(roadName.length) < parseInt(minimumLength) || parseInt(roadName.length) > parseInt(maximumLength)) {
            result = false;
            $('#road-name-error').removeClass('d-none');
        } else
            $('#road-name-error').addClass('d-none');

        //Validation Road Name
        if (transRoadName === '') {
            result = false;
            $('#trans-road-name-error').removeClass('d-none')
        } else
            $('#trans-road-name-error').addClass('d-none')


        //Validation Area Name
        minimumLength = parseInt($('#area-name').attr('minlength'));
        maximumLength = parseInt($('#area-names').attr('maxlength'));

        if (areaName === '' || parseInt(areaName.length) < parseInt(minimumLength) || parseInt(areaName.length) > parseInt(maximumLength)) {
            result = false;
            $('#area-name-error').removeClass('d-none');
        } else
            $('#area-name-error').addClass('d-none');

        //Validation Trans Area Name
        if (transAreaName === '') {
            result = false;
            $('#trans-area-name-error').removeClass('d-none')
        } else
            $('#trans-area-name-error').addClass('d-none')

        //Validation City
        if (city === '') {
            result = false;
            $('#city-id-error').removeClass('d-none');
        } else
            $('#city-id-error').addClass('d-none');

        //Validation Residence Type
        if (residenceType === '') {
            result = false;
            $('#residence-type-id-error').removeClass('d-none');
        } else
            $('#residence-type-id-error').addClass('d-none');

        //Validation Residence Ownership
        if (residenceOwnership === '') {
            result = false;
            $('#residence-ownership-id-error').removeClass('d-none')
        } else
            $('#residence-ownership-id-error').addClass('d-none')

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

    // Address Type Unique Dropdown
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


    // @@@@@@@@@@@@@@@@@@@@@@   Contact Details - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-contact-dt').click(function () {
        event.preventDefault();
        $('#send-code').addClass('d-none');
        $('#resend').addClass('d-none');
        $('.verification-code').addClass('d-none');
        SetModalTitle('contact', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-contact-dt').click(function () {
        SetModalTitle('contact', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-contact-dt').data('rowindex');
            id = $('#contact-modal').attr('id');
            myModal = $('#' + id).modal();

            $.get('/PersonChildAction/GetPersonContactDetailEntryStatus', { _personContactDetailPrmKey: columnValues[8], async: false }, function (data, textStatus, jqXHR) {
                entryStatus = data;
            });

            //// Display Value In Modal Inputs
            isMobile = columnValues[2].includes('Mobile');
            isEmail = columnValues[2].includes('Email');

            $('#resend').addClass('d-none');

            if ((columnValues[8] !== 0) && (columnValues[4] === true)) {
                $('#contact-modal').modal('hide');
            }
            else {
                if ((columnValues[8] !== 0) && entryStatus === 'VRF') {
                    $('#contact-detail-new').addClass('read-only');
                }
                else {
                    $('#contact-detail-new').removeClass('read-only');
                }

                $('#contact-modal').modal('show');
            }

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

            if (columnValues[4] === 'True' || columnValues[4] === 'Yes') {
                $('#is-verified').prop('checked', true)
            }
            else {
                $('#is-verified').prop('checked', false)
            }

            $('#verification-code', myModal).val('');
            $('#note-contact-detail', myModal).val(columnValues[6]);

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
        
        if (IsValidContactDataTableModal()) {
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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-contact tbody input[type="checkbox"]:checked').each(function () {
                    contactDataTable.row($('#tbl-contact tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-contact-dt').data('rowindex');
                    EnableNewOperation('contact');

                    $('#select-all-contact').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (!contactDataTable.data().any())
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

            // [Old Code]

            //if (contactDetailPrmKey > 0)
            //    EnableDeleteOperation('contact');
            //else
            //    EnableEditDeleteOperation('contact');
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
        
        result = true;

        // Check For Duplicate Contact Number
        if (!isDuplicateContact) {
            // Get Modal Inputs In Local letiable
            tag = '<input type="checkbox" name="select-all" class="checks"/>';
            contactType = $('#contact-type option:selected').val();
            contactTypeText = $('#contact-type option:selected').text();
            fieldValue = $('#field-value').val();
            isVerified = $('input[name="PersonContactDetailViewModel.IsVerified"]').is(':checked') ? 'True' : "False";
            note = $('#note-contact-detail').val();
            verificationCode = $('#verification-code').val();
            personAddressPrmKey = 0;
            reasonForModification = $('#reason-for-modification-contact').val();
            hasDivClass = $('#contact-div').hasClass('d-none');

            // Set Default Value, If Empty
            if (note === '')
                note = 'None';

            if (contactType === '') {
                result = false;
                $('#contact-type-error').removeClass('d-none');
            }
            else
                $('#contact-type-error').addClass('d-none');


            // Validate If Contact Type Is Mobile
            if (isMobile) {
                // Define a regular expression pattern for a 10-digit mobile number
                let regex = /^\d{10}$/;
                let verificationCode = $('#verification-code').val();

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
                    $('#field-value-error').removeClass('d-none');
                } else {
                    verificationCode = '0';
                    $('#verification-code').val(verificationCode);
                }
            }

            let inputValue = $('#field-value').val();
            let contactTypes = $('#contact-type').val();

            $.get('/PersonChildAction/GetSysNameOfContactTypeById', { _contactTypeId: contactTypes, async: false }, function (data) {
                
                contact_Type = data;
            });

            //Toll Free Number
            if (contact_Type === 'TollFreeNumber') {
                
                if (inputValue.length < 15 || inputValue.length > 15) {
                    result = false;
                    $('#field-value-error').removeClass('d-none');
                }
                else {
                    $('#field-value-error').addClass('d-none');
                }

            }

                //Toll Number
            else if (contact_Type === 'TollNumber') {
                
                if (inputValue.length < 15 || inputValue.length > 15) {
                    result = false;
                    $('#field-value-error').removeClass('d-none');
                }
                else {
                    $('#field-value-error').addClass('d-none');
                }

            }

                //Miss Call Number
            else if (contact_Type === 'MisscallNumber') {
                
                if (inputValue.length < 10 || inputValue.length > 10) {
                    result = false;
                    $('#field-value-error').removeClass('d-none');
                }
                else {
                    $('#field-value-error').addClass('d-none');
                }


            }

                //What’s App Number
            else if (contact_Type === 'WhatsAppNumber') {
                
                if (inputValue.length < 10 || inputValue.length > 10) {
                    result = false;
                    $('#field-value-error').removeClass('d-none');
                }
                else {
                    $('#field-value-error').addClass('d-none');
                }

            }

                //Pager Number
            else if (contact_Type === 'Pager') {
                
                if (inputValue.length < 14 || inputValue.length > 14) {
                    result = false;
                    $('#field-value-error').removeClass('d-none');
                }
                else
                    $('#field-value-error').addClass('d-none');
            }

                //Home Fax
            else if (contact_Type === 'HomeFax') {
                
                if (inputValue.length < 15 || inputValue.length > 15) {
                    result = false;
                    $('#field-value-error').removeClass('d-none');
                }
                else
                    $('#field-value-error').addClass('d-none');
            }

                //Work Fax
            else if (contact_Type === 'WorkFax') {
                
                if (inputValue.length < 15 || inputValue.length > 15) {
                    result = false;
                    $('#field-value-error').removeClass('d-none');
                }
                else
                    $('#field-value-error').addClass('d-none');
            }

                //Work Email
            else if (contact_Type === 'WorkEmail') {

                let email = $('#field-value').val();
                let emailRegex = /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/;


                if (emailRegex.test(email)) {
                    $('#field-value-error').addClass('d-none');
                    // You can proceed with form submission or other actions here
                } else {
                    result = false;
                    $('#field-value-error').removeClass('d-none');
                }
            }

                //Home Email
            else if (contact_Type === 'HomeMail') {

                let email = $('#field-value').val();
                let emailRegex = /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/;


                if (emailRegex.test(email)) {
                    $('#field-value-error').addClass('d-none');
                    // You can proceed with form submission or other actions here
                } else {
                    result = false;
                    $('#field-value-error').removeClass('d-none');
                }
            }

                //Other Mail
            else if (contact_Type === 'OtherMail') {

                let email = $('#field-value').val();
                //let emailRegex = /^(?!.*[._\-]{2})[a-zA-Z0-9](?:[a-zA-Z0-9._\-]{0,62}[a-zA-Z0-9])?@[a-zA-Z0-9](?:[a-zA-Z0-9\-]{0,190}[a-zA-Z0-9])?\.[a-zA-Z]{1,63}$/;
                let emailRegex = /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/;

                if (emailRegex.test(email)) {
                    $('#field-value-error').addClass('d-none');
                    // You can proceed with form submission or other actions here
                } else {
                    result = false;
                    $('#field-value-error').removeClass('d-none');
                }
            }

        }
        else
            result = false;

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
    $('#btn-add-kyc-document-dt').click(function () {
        
        editedSequenceNumber = 0;
        editedDocument = 0;
        event.preventDefault();
        $('#kyc-document-number-error').addClass('d-none').html('');
        SetModalTitle('kyc-document', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-kyc-document-dt').click(function () {
        
        SetModalTitle('kyc-document', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-kyc-document-dt').data('rowindex');
            id = $('#kyc-document-modal').attr('id');

            myModal = $('#' + id).modal();

            kYCDateOfIssueDate = new Date(columnValues[8]);
            kYCExpiryDate = new Date(columnValues[9]);
            kYCDateOfRequestDate = new Date(columnValues[12]);
            kYCDateOfExpectingSubmitDate = new Date(columnValues[13]);
            kYCDateOfSubmitDate = new Date(columnValues[14]);

            $.get('/PersonChildAction/GetDocumentDropdownList', { _documentTypeId: columnValues[1], async: false }, function (data, textStatus, jqXHR) {
                $('#document-document-type-id').empty();
                documentList = '<option value="0">---Select document---</option>';
                for (i = 0; i < data.length; i++) {
                    documentList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
                }
                $('#document-document-type-id').append(documentList);
                $('#document-document-type-id').find("option[value='" + columnValues[3] + "']").prop('selected', true);
            });

            // Display Value In Modal Inputs
            $('#document-type-id-kyc-document', myModal).val(columnValues[1]);
            $('#document-document-type-id', myModal).val(columnValues[3]);
            $('#name-as-per-document-kyc-document', myModal).val(columnValues[5]);
            $('#document-number-kyc-document', myModal).val(columnValues[6]);
            $('#sequence-number-kyc-document', myModal).val(columnValues[7]);
            $('#date-of-issue-kyc-document', myModal).val(GetInputDateFormat(kYCDateOfIssueDate));
            $('#date-of-expiry-kyc-document', myModal).val(GetInputDateFormat(kYCExpiryDate));
            $('#issuing-authority-kyc-document', myModal).val(columnValues[10]);
            $('#place-of-issue-kyc-document', myModal).val(columnValues[11]);
            $('#date-of-request', myModal).val(GetInputDateFormat(kYCDateOfRequestDate));
            $('#date-of-expecting-submit', myModal).val(GetInputDateFormat(kYCDateOfExpectingSubmitDate));
            $('#date-of-submit', myModal).val(GetInputDateFormat(kYCDateOfSubmitDate));
            documentUploadStatus = columnValues[15].split('--->');

            editedDocument = columnValues[3];
            editedSequenceNumber = columnValues[7];

            // Display Value In Modal Inputs
            $("input[name='PersonKYCDocumentViewModel.DocumentUploadStatus'][value=" + columnValues[15] + "]").prop('checked', true);

            storagePathInput = columnValues[17];

            storagePathId = $(storagePathInput).attr('id');
            docPath = $('#' + storagePathId).get(0);
            files = docPath.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            photoPathKYC = $('#photo-path-kyc').get(0);
            photoPathKYC.files = dt.files;

            photoinput = columnValues[18];
            photoPathKYCId = $(photoinput).attr('id');
            photoSrc = $('#' + photoPathKYCId).attr('src');
            $('#photo-path-kyc-image-preview').attr('src', photoSrc);

            $('#file-caption-kyc', myModal).val(columnValues[19]);
            $('#note-kyc-document', myModal).val(columnValues[20]);
            $('#reason-for-modification-kyc-document', myModal).val(columnValues[21]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-kyc-document-edit-dt').addClass('read-only');
            $('#kyc-document-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-kyc-document-modal').click(function (event) {
        
        counter++;
        i = counter;
        newID = 'photoPathKYC' + i;
        photoID = 'PhotoId' + i;

        if (IsValidKycDocumentModal()) {
            row = personKycDataTable.row.add([
                        tag,
                        document,
                        documentText,
                        documentDocumentType,
                        documentDocumentTypeText,
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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        reasonForModification,
                        PrmKey
            ]).draw();

            files = photoPathKYC.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);
            if (photoPathKYC.files.length !== 0) {
                docPath.files = dt.files;
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            //Hide DropDown Values
            $('#document-document-type-id').find("option[value='" + documentDocumentType + "']").hide();
            
            //Show GstRegistrationDetail Accordian when PanCard Added in Identification

            $('#tbl-kyc-document > tbody > tr').each(function () {
                currentRow = $(this).closest('tr');
                columnValues = (personKycDataTable.row(currentRow).data());
                // Handling Code If Row Is Undefined Or Null
                if (typeof columnValues !== 'undefined' && columnValues !== null) {
                    $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: columnValues[3], async: false }, function (data) {
                        
                        if (data === 'PAN') {
                            $('#enable-gst-registration-details-input').removeClass('read-only');

                            panCardNumber = documentNumber;

                            $('#registrations-number').val(documentNumber);
                            $('#collapse-person-kyc-document-validations span').html('');
                        }
                        else {
                            $('#enable-gst-registration-details-input').addClass('read-only');
                            $('.gst-registration-input').val('');
                            $('#is-applicable-eway-bill').prop('checked', false);
                            $('#enable-gst-return-document').prop('checked', false);

                        }
                    });
                }
                else
                    return false;
            });
            $('#enable-gst-registration-details-input').addClass('read-only');

            // Error Message In Span
            $('#kyc-document-data-table-error').addClass('d-none');
            HideColumnsKycDocumentDataTable();

            personKycDataTable.columns.adjust().draw();

            ClearModal('kyc-document');

            $('#kyc-document-modal').modal('hide');

            EnableNewOperation('kyc-document');
        }
    });

    // Modal update Button Event
    $('#btn-update-kyc-document-modal').click(function (event) {
        
        $('#select-all-kyc-document').prop('checked', false);
        if (IsValidKycDocumentModal()) {
            personKycDataTable.row(selectedRowIndex).data([

                          tag,
                          document,
                          documentText,
                          documentDocumentType,
                          documentDocumentTypeText,
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
                          dochtml,
                          dochtml1,
                          fileCaption,
                          note,
                          reasonForModification,
                          PrmKey
            ]).draw();

            files = photoPathKYC.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);

            if (photoPathKYC.files.length !== 0) {
                docPath.files = dt.files;
            }


            HideColumnsKycDocumentDataTable();



            personKycDataTable.columns.adjust().draw();

            columnValues = $('#btn-update-kyc-document').data('rowindex');

            $('#tbl-kyc-document > tbody > tr').each(function () {
                currentRow = $(this).closest('tr');
                columnValues = (personKycDataTable.row(currentRow).data());
                // Handling Code If Row Is Undefined Or Null
                if (typeof columnValues !== 'undefined' && columnValues !== null) {
                    $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: columnValues[3], async: false }, function (data) {
                        
                        if (data === 'PAN') {
                            $('#enable-gst-registration-details-input').removeClass('read-only');

                            panCardNumber = documentNumber;

                            $('#registrations-number').val(documentNumber);
                            $('#collapse-person-kyc-document-validations span').html('');
                        }
                        else {
                            $('#enable-gst-registration-details-input').addClass('read-only');
                            $('#enable-gst-registration-details').prop('checked', false);
                            $('#gst-registration-details-block').addClass('d-none');
                            $('.gst-registration-input').val('');
                            $('#is-applicable-eway-bill').prop('checked', false);
                            $('#enable-gst-return-document').prop('checked', false);
                            $('#gst-return-document-block').addClass('d-done');

                        }
                    });
                }
                else
                    return false;
            });

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

                    //    arr.map(function (obj) {
                    //     if (obj.arrayCloumn1.includes('ea674271-8f28-4efe-b728-36a036b8ec7f')) {
                    //                        $('#enable-gst-registration-Details-input').addClass('d-none');
                    //                        panCardNumber = '';
                    //                        $('#registrations-number').val('');
                    //                        $('input[name="PersonGSTRegistrationDetailViewModel.UploadGSTReturnDocument"]').prop('checked', false);
                    //                        $('#gst-return-document-block').addClass('d-none');
                    //                        gstDataTable.clear().draw();
                    //                        $("#collapse-person-kyc-document-validations span").html('');
                    //                        $("#collapse-person-kyc-document-validations span").append('<i class="fa fa-info-circle">&nbsp;&nbsp;PanCard are required For GST Registration Detail</i>').css({ "color": "blue", "font-size": "0.9rem" });
                    //}
                    //});

                     EnableNewOperation('kyc-document');
                  $('#select-all-kyc-document').prop('checked', false);
                    // Add Required Error Message, If Table Has Not Any Record
                    if (!personKycDataTable.data().any())
                    $('#kyc-document-data-table-error').removeClass('d-none');

            $('#enable-gst-registration-details-input').addClass('read-only');
            $('#enable-gst-registration-details').prop('checked', false);
            $('#gst-registration-details-block').addClass('d-none');

            $('#tbl-kyc-document > tbody > tr').each(function () {
                currentRow = $(this).closest('tr');
                columnValues = (personKycDataTable.row(currentRow).data());

                    // Handling Code If Row Is Undefined Or Null
                if (typeof columnValues !== 'undefined' && columnValues !== null) {
                    $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: columnValues[3], async: false }, function (data) {
                        if (data === 'PAN') {
                    $('#enable-gst-registration-details-input').removeClass('read-only');


                            panCardNumber = documentDocumentType;

                            $('#registrations-number').val('');
                            $('#collapse-person-kyc-document-validations span').html('');
                }
                });
                }
                    return false;
                });

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
        
        result = true;

        counter++;
        i = counter;
        newID = 'photoPathKYC' + i;
        photoID = 'PhotoId' + i;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        document = $('#document-type-id-kyc-document option:selected').val();
        documentText = $('#document-type-id-kyc-document option:selected').text();
        documentDocumentType = $('#document-document-type-id option:selected').val();
        documentDocumentTypeText = $('#document-document-type-id option:selected').text();
        nameAsPerDocument = $('#name-as-per-document-kyc-document').val();
        documentNumber = $('#document-number-kyc-document').val();
        sequenceNumber = $('#sequence-number-kyc-document').val();
        dateOfIssue = $('#date-of-issue-kyc-document').val();
        dateOfExpiry = $('#date-of-expiry-kyc-document').val();
        issuingAuthority = $('#issuing-authority-kyc-document').val();
        placeOfIssue = $('#place-of-issue-kyc-document').val();
        dateOfRequest = $('#date-of-request').val();
        dateOfExpectingSubmit = $('#date-of-expecting-submit').val();
        dateOfSubmit = $('#date-of-submit').val();
        documentUploadStatus = $('.document-upload-status:checked').val();
        documentUploadStatusText = $('.document-upload-status:checked').next('label').text();
        photo = $('#photo-path-kyc-image-preview').prop('src');
        photoPathKYC = $('#photo-path-kyc').get(0);
        dochtml = '<input type="file", id="' + newID + '", class="new-record", name = "DocPath", disabled="true"/>';
        dochtml1 = '<img src="' + photo + '", width="50", height="50", id= "' + photoID + '" />';
        fileCaption = $('#file-caption-kyc').val();
        note = $('#note-kyc-document').val();
        reasonForModification = $('#reason-for-modification-kyc-document').val();
        hasDivClass = $('#kyc-document-div').hasClass('d-none');
        PrmKey = 0;

        filename = $('input[type=file]').val().split('\\').pop();


        if (note === '')
            note = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }


        let docKycUpload = $('#kyc-document-upload').val();

        if (docKycUpload === 'M') {
            path = $('#photo-path-kyc').val();
            fileCaption;

        } else if (docKycUpload === 'O') {
            if (photo === '' && fileCaption === '') {
                path = 'None';
                fileCaption = 'None';
            }
            else if (photo === '') {
                path = 'None';
                fileCaption;
            }
            else if (fileCaption === '') {
                path = $('#photo-path-kyc').val();
                fileCaption = 'None';
            }
            else if (photo !== '' && fileCaption !== '') {
                path = $('#photo-path-kyc').val();
                fileCaption;
            }
        }
        else if (docFinancekUpload === 'D') {
            if (photo === '') {
                path = 'None';
                fileCaption = 'None';
            }
        }


        let filteredDocumentData = personKycDataTable
                 .rows()
                 .indexes()
                 .filter(function (value, index) {
                     return personKycDataTable.row(value).data()[3] == $('#document-document-type-id').val();
                 });

        if (personKycDataTable.rows(filteredDocumentData).count() > 0 && editedDocument !== $('#document-document-type-id').val()) {
            isDuplicateDocument = true;
            result = false;
            $('#document-document-id-error').removeClass('d-none');
        }
        else {
            isDuplicateDocument = false;
            $('#document-document-id-error').addClass('d-none');
        }


        let filteredData = personKycDataTable
            .rows()
            .indexes()
            .filter(function (value, index) {
                return personKycDataTable.row(value).data()[7] == $('#sequence-number-kyc-document').val();
            });

        if (personKycDataTable.rows(filteredData).count() > 0 && editedSequenceNumber !== $('#sequence-number-kyc-document').val()) {
            isDuplicateSequenceNumber = true;
            result = false;
            $('#sequence-number-kyc-error').removeClass('d-none');
        }
        else {
            isDuplicateSequenceNumber = false;
            $('#sequence-number-kyc-error').addClass('d-none');
        }

        if (document === '' || document.length < 36) {
            result = false;
            $('#document-type-id-kyc-document-error').removeClass('d-none');
        }
        else
            $('#document-type-id-kyc-document-error').addClass('d-none');

        if (typeof documentDocumentType === 'undefined' || documentDocumentType === null || isDuplicateDocument == true || documentDocumentType == 0) {
            result = false;
            $('#document-document-type-id-error').removeClass('d-none');
        }
        else
            $('#document-document-type-id-error').addClass('d-none');

        if (nameAsPerDocument === '' || nameAsPerDocument.length < 5 || nameAsPerDocument.length > 150) {
            result = false;
            $('#name-as-per-document-kyc-document-error').removeClass('d-none');
        }
        else
            $('#name-as-per-document-kyc-document-error').addClass('d-none');

        if (documentNumber === '' || documentNumber.length > 50) {
            result = false;
            $('#kyc-number-error').removeClass('d-none');
        }
        else
            $('#kyc-number-error').addClass('d-none');


        let input = $('#document-number-kyc-document').val();
        let documentID = $('#document-document-type-id').val();

        $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: documentID, async: false }, function (data) {
            
            document_Type = data;

            //AADHAAR Card
            if (document_Type === 'AADHAAR') {
                
                if (input.length < 14) {
                    result = false;
                    $('#kyc-number-error').removeClass('d-none');
                }
                else
                    $('#kyc-number-error').addClass('d-none');
            }

                // Pan Card
            else if (document_Type === 'PAN') {
                
                if (input.length < 10) {
                    result = false;
                    $('#kyc-number-error').removeClass('d-none');
                }
                else
                    $('#kyc-number-error').addClass('d-none');
            }
                // Voting Card
            else if (document_Type === 'VOTER') {
                
                if (input.length < 10) {
                    result = false;
                    $('#kyc-number-error').removeClass('d-none');
                }
                else
                    $('#kyc-number-error').addClass('d-none');
            }
                // DRIVING License
            else if (document_Type === 'DRIVING') {
                
                if (input.length < 16) {
                    result = false;
                    $('#kyc-number-error').removeClass('d-none');
                }
                else
                    $('#kyc-number-error').addClass('d-none');
            }
                //PassPort
            else if (document_Type === 'PASSPORT') {
                
                if (input.length < 8) {
                    result = false;
                    $('#kyc-number-error').removeClass('d-none');
                }
                else
                    $('#kyc-number-error').addClass('d-none');
            }
                //RATION
            else if (document_Type === 'RATION') {
                
                if (input.length < 10) {
                    result = false;
                    $('#kyc-number-error').removeClass('d-none');
                }
                else
                    $('#kyc-number-error').addClass('d-none');
            }
        });


        if (sequenceNumber === '' || isDuplicateSequenceNumber === true || sequenceNumber.length < 1 || sequenceNumber.length > 255) {
            result = false;
            $('#sequence-number-kyc-document-error').removeClass('d-none');
        }
        else
            $('#sequence-number-kyc-document-error').addClass('d-none');


        let isValidDateOfIssue = IsValidInputDate('#date-of-issue-kyc-document');

        if (!isValidDateOfIssue) {
            result = false;
            $('#date-of-issue-kyc-document-error').removeClass('d-none');
        } else
            $('#date-of-issue-kyc-document-error').addClass('d-none');


        let isValidDateOfExpiry = IsValidInputDate('#date-of-expiry-kyc-document');

        if (!isValidDateOfExpiry) {
            result = false;
            $('#date-of-expiry-kyc-document-error').removeClass('d-none');
        } else
            $('#date-of-expiry-kyc-document-error').addClass('d-none');


        if (issuingAuthority === '' || issuingAuthority.length < 3 || issuingAuthority.length > 100) {
            result = false;
            $('#issuing-authority-kyc-document-error').removeClass('d-none');
        }
        else
            $('#issuing-authority-kyc-document-error').addClass('d-none');

        if (placeOfIssue === '' || placeOfIssue.length < 3 || placeOfIssue.length > 100) {
            result = false;
            $('#place-of-issue-kyc-document-error').removeClass('d-none');
        }
        else
            $('#place-of-issue-kyc-document-error').addClass('d-none');

        let isValiddateOfRequest = IsValidInputDate('#date-of-request');

        if (!isValiddateOfRequest) {
            result = false;
            $('#date-of-request-error').removeClass('d-none');
        }
        else
            $('#date-of-request-error').addClass('d-none');

        let isValidDateOfExpectingSubmit = IsValidInputDate('#date-of-expecting-submit');

        if (!isValidDateOfExpectingSubmit) {
            result = false;
            $('#date-of-expecting-submit-error').removeClass('d-none');
        }
        else
            $('#date-of-expecting-submit-error').addClass('d-none');

        let isValiddateOfSubmit = IsValidInputDate('#date-of-submit');

        if (!isValiddateOfSubmit) {
            result = false;
            $('#date-of-submit-error').removeClass('d-none');
        }
        else
            $('#date-of-submit-error').addClass('d-none');

        if (documentUploadStatusText === '') {
            result = false;
            $('#document-upload-status-error').removeClass('d-none');
        }
        else
            $('#document-upload-status-error').addClass('d-none');

        if (path === '') {
            result = false;
            $('#photo-path-kyc-error').removeClass('d-none');
        }
        else
            $('#photo-path-kyc-error').addClass('d-none');

        if (fileCaption === '') {
            result = false;
            $('#file-caption-kyc-error').removeClass('d-none');
        }
        else
            $('#file-caption-kyc-error').addClass('d-none');

        return result;
    }

    function HideColumnsKycDocumentDataTable() {
        personKycDataTable.column(1).visible(false);
        personKycDataTable.column(3).visible(false);
        personKycDataTable.column(15).visible(false);
        personKycDataTable.column(21).visible(false);
        personKycDataTable.column(22).visible(false);
    }
    

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Family Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // ClearFamilyModalInputs();
    ClearModal('family-detail');

    // DataTable Add Button 
    $('#btn-add-family-detail-dt').click(function () {
        
        //Modify By --- Sagar Kare 
        $('#person-information-number-input').removeClass('d-none');
        $('#family-member-name').removeClass('d-none');
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
        SetModalTitle('family-detail', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-family-detail-dt').data('rowindex');
            id = $('#family-detail-modal').attr('id');
            myModal = $('#' + id).modal();

            familyDetailsBirthDate = new Date(columnValues[7]);
            personInformationNumber = columnValues[1];
            personInformationNumberText = columnValues[2];
            $('#family-person-information-number', myModal).val(columnValues[2]);
            $('#full-name-of-family-member', myModal).val(columnValues[3]);
            $('#trans-full-name-of-family-member', myModal).val(columnValues[4]);
            $('#relations-id', myModal).val(columnValues[5]);
            $('#birth-date-family-member', myModal).val(GetInputDateFormat(familyDetailsBirthDate));
            $('#occupation-id', myModal).val(columnValues[8]);
            $('#income', myModal).val(columnValues[10]);
            $('#note-family-detail', myModal).val(columnValues[11]);
            $('#trans-note-family-detail', myModal).val(columnValues[12]);
            $('#reason-for-modification-family-detail', myModal).val(columnValues[13]);

            // Show Modals
            myModal.modal({ show: true });
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

            ClearModal('family-detail');

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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-family-detail tbody input[type="checkbox"]:checked').each(function () {
                 personFamilyDataTable.row($('#tbl-family-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-family-detail-dt').data('rowindex');
                  EnableNewOperation('family-detail');

                  $('#select-all-family-detail').prop('checked', false);
                    // Add Required Error Message, If Table Has Not Any Record
                    if (!personFamilyDataTable.data().any())
                    $('#family-detail-data-table-error').removeClass('d-none');

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
            EnableNewOperation('relation');

            $('#tbl-relation tbody input[type="checkbox"]').each(function () {
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
        
        result = true;
        let isSelectedPersonInformationNumber = false;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        fullNameOfFamilyMember = $('#full-name-of-family-member').val();
        transFullNameOfFamilyMember = $('#trans-full-name-of-family-member').val();
        relation = $('#relations-id option:selected').val();
        relationText = $('#relations-id option:selected').text();
        birthDate = $('#birth-date-family-member').val();
        occupation = $('#occupation-id option:selected').val();
        occupationText = $('#occupation-id option:selected').text();
        income = $('#income').val();
        note = $('#note-family-detail').val();
        transNote = $('#trans-note-family-detail').val();
        reasonForModification = $('#reason-for-modification-family-detail').val();
        //rvisibility = 0;
        hasDivClass = $('#family-detail-div').hasClass('d-none');

        result = true;

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (transNote === '')
            transNote = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }

        //Modify By --- Sagar Kare 
        //Validatio For  Person Information Number
        if (personInformationNumber == '' || personInformationNumber == 'None') {
            fullNameOfFamilyMember = $('#full-name-of-family-member').val();
            transFullNameOfFamilyMember = $('#trans-full-name-of-family-member').val();
        }
        else {
            fullNameOfFamilyMember = 'None';
            transFullNameOfFamilyMember = 'None';
        }
        if (personInformationNumber == '' || typeof personInformationNumber == 'undefined')
            personInformationNumber = 'None';

        // Check Whether Person Information Number Selected For Nominee Or Not?
        if (personInformationNumber == 'None' || typeof personInformationNumber == 'undefined') {
            isSelectedPersonInformationNumber = false;
        } else {
            isSelectedPersonInformationNumber = true;
        }

        if (!$('#person-information-number-input').hasClass('d-none')) {
            if (isSelectedPersonInformationNumber == false) {
                result = false;
                $('#family-person-information-number-error').removeClass('d-none');
            } else {
                $('#family-person-information-number-error').addClass('d-none');
            }
        }
        else {
            personInformationNumberText = 'None';
        }

        if (isSelectedPersonInformationNumber == false) {
            if (fullNameOfFamilyMember === '' || fullNameOfFamilyMember.length > 150) {
                result = false;
                $('#full-name-of-family-member-error').removeClass('d-none');
            }
            else
                $('#full-name-of-family-member-error').addClass('d-none');

            if (transFullNameOfFamilyMember === '' || transFullNameOfFamilyMember.length > 150) {
                result = false;
                $('#trans-full-name-of-family-member-error').removeClass('d-none');
            }
            else
                $('#trans-full-name-of-family-member-error').addClass('d-none');
        }


        if (relation.trim().length < 36) {
            result = false;
            $('#relations-id-error').removeClass('d-none');
        }
        else
            $('#relations-id-error').addClass('d-none');

        let isValidBirthDate = IsValidInputDate('#birth-date-family-member');

        if (!isValidBirthDate) {
            result = false;
            $('#birth-date-family-member-error').removeClass('d-none');
        }
        else
            $('#birth-date-family-member-error').addClass('d-none');

        if (occupation === '') {
            result = false;
            $('#occupation-id-error').removeClass('d-none');
        }
        else
            $('#occupation-id-error').addClass('d-none');

        if (income === '' || income < 0 || income > 999999999) {
            result = false;
            $('#income-error').removeClass('d-none');
        }
        else
            $('#income-error').addClass('d-none');

        return result;

    }

    function HideColumnsFamilyDetailsDataTable() {
        personFamilyDataTable.column(1).visible(false);
        personFamilyDataTable.column(5).visible(false);
        personFamilyDataTable.column(8).visible(false);
        personFamilyDataTable.column(13).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Person Group Authorized Signatory - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearModal for authorized-signatory
    ClearModal('authorized-signatory');

    // DataTable Add Button 
    $('#btn-add-authorized-signatory-dt').click(function () {
        
        //Modify By --- Suraj Sonawane[13/09/2024] 
        if (IsAuthorizedSignatory === true) {
            $('#authorized-signatory-block').removeClass('d-none');
        } else {
            $('#authorized-signatory-block').addClass('d-none');
        }

        $('#authorized-information-number-input').removeClass('d-none');
        $('#authorized-member-name').removeClass('d-none');

        // Display Alert Message Only When Modal Present On Page (i.e. Create And Amend) Hide On Verify
        if ($('#authorized-signatory-detail-modal').length) {
            authorizedPersonInformationNumber = ''
            authorizedPersonInformationNumberText = '';
        }
        event.preventDefault();
        SetModalTitle('authorized-signatory', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-authorized-signatory-dt').click(function () {
        
        SetModalTitle('authorized-signatory', 'Edit');

        //Modify By --- Suraj Sonawane[13/09/2024] 
        if (IsAuthorizedSignatory === true) {
            $('#authorized-signatory-block').removeClass('d-none');
        } else {
            $('#authorized-signatory-block').addClass('d-none');
        }

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-authorized-signatory-dt').data('rowindex');
            id = $('#authorized-signatory-modal').attr('id');
            myModal = $('#' + id).modal();

            authorizedPersonInformationNumber = columnValues[1];
            authorizedPersonInformationNumberText = columnValues[2];
            $('#authorized-person-information-number', myModal).val(columnValues[2]);

            $('#full-name-of-authorized-member', myModal).val(columnValues[3]);
            $('#trans-full-name-of-authorized-member', myModal).val(columnValues[4]);
            $('#authorized-person-address-detail', myModal).val(columnValues[5]);
            $('#trans-authorized-person-address-detail', myModal).val(columnValues[6]);
            $('#authorized-person-contact-detail', myModal).val(columnValues[7]);
            $('#trans-authorized-person-contact-detail', myModal).val(columnValues[8]);
            $('#board-of-director-designation-id', myModal).val(columnValues[9]);
            $('#enable-authorized-signatory').prop('checked', columnValues[11].toString().toLowerCase() === 'true' ? true : false);

            storagePathInput = columnValues[12];

            storagePathId = $(storagePathInput).attr('id');
            docPath = $('#' + storagePathId).get(0);
            files = docPath.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            photoPathSign = $('#photo-path-sign').get(0);
            photoPathSign.files = dt.files;

            photoinput = columnValues[13];
            PhotoPathSignId = $(photoinput).attr('id');
            photoSrc = $('#' + PhotoPathSignId).attr('src');
            $('#photo-path-sign-image-preview').attr('src', photoSrc);
            $('#file-caption-sign', myModal).val(columnValues[14]);
            $('#note-board-of-director-authorized', myModal).val(columnValues[15]);


            // Collect rows where columnValues[7] is "True"
            const trueValues = $('#tbl-authorized-signatory > tbody > tr')
                .map(function () {
                    const columnValues = boardOfDirectorAuthorizedDataTable.row($(this)).data();
                    //Modify By :Suraj Sonawane [13/09/2024] 'True' to true
                    return columnValues && columnValues[11] === true ? columnValues : null;
                })
                .get()
                .filter(Boolean);

            // Show or hide the heading based on whether any trueValues were found
            $('#authorized-signatory-block').toggleClass('d-none', trueValues.length === 0);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-authorized-signatory-edit-dt').addClass('read-only');
            $('#authorized-signatory-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-authorized-signatory-modal').click(function (event) {
        
        if (IsValidAuthorizedSignatoryModal()) {
            row = boardOfDirectorAuthorizedDataTable.row.add([
                        tag,
                        authorizedPersonInformationNumber,
                        authorizedPersonInformationNumberText,
                        fullNameOfAuthorizedPerson,
                        transfullNameOfAuthorizedPerson,
                        authorizedPersonAddressDetail,
                        transAuthorizedPersonAddressDetail,
                        authorizedPersonContactDetail,
                        transAuthorizedPersonContactDetail,
                        designationId,
                        designationIdText,
                        IsAuthorizedSignatory,
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
            ]).draw();

            files = photoPathSign.files;

            // Check if files are present
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        )
                    );
                }

                // Handle the case where the photoPath document is empty
                docPath = $('#' + newID).get(0);
                if (docPath) {
                    // Set files only if photoPathDocument has files
                    if (files.length !== 0) {
                        docPath.files = dt.files;
                    }
                }
            } else {
                // Handle the case where no files are uploaded
                docPath = $('#' + newID).get(0);
                if (docPath) {
                    // Reset files to empty if no files are uploaded
                    docPath.files = new DataTransfer().files;
                }
            }
            $('#authorized-accordion-error').addClass('d-none');

            HideColumnsAuthorizedSignatoryDataTable();

            boardOfDirectorAuthorizedDataTable.columns.adjust().draw();

            ClearModal('authorized-signatory');

            $('#authorized-signatory-modal').modal('hide');

            EnableNewOperation('authorized-signatory');
        }
    });

    // Modal update Button Event
    $('#btn-update-authorized-signatory-modal').click(function (event) {
        $('#select-all-authorized-signatory').prop('checked', false);
        if (IsValidAuthorizedSignatoryModal()) {
            boardOfDirectorAuthorizedDataTable.row(selectedRowIndex).data([
                        tag,
                        authorizedPersonInformationNumber,
                        authorizedPersonInformationNumberText,
                        fullNameOfAuthorizedPerson,
                        transfullNameOfAuthorizedPerson,
                        authorizedPersonAddressDetail,
                        transAuthorizedPersonAddressDetail,
                        authorizedPersonContactDetail,
                        transAuthorizedPersonContactDetail,
                        designationId,
                        designationIdText,
                        IsAuthorizedSignatory,
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,

            ]).draw();


            files = photoPathSign.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);

            if (photoPathSign.files.length !== 0) {
                docPath.files = dt.files;
            }

            HideColumnsAuthorizedSignatoryDataTable();

            boardOfDirectorAuthorizedDataTable.columns.adjust().draw();

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

                    boardOfDirectorAuthorizedDataTable.row($('#tbl-authorized-signatory tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-authorized-signatory-dt').data('rowindex');

                  EnableNewOperation('authorized-signatory');

                  $('#select-all-authorized-signatory').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!boardOfDirectorAuthorizedDataTable.data().any())
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

                selectedRowIndex = boardOfDirectorAuthorizedDataTable.row(row).index();

                rowData = boardOfDirectorAuthorizedDataTable.row(selectedRowIndex).data();
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
                selectedRowIndex = boardOfDirectorAuthorizedDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (boardOfDirectorAuthorizedDataTable.row(selectedRowIndex).data());

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
        
        let isSelectedPersonInformationNumberAuthorized = false;

        result = true;

        counter++;
        i = counter;
        newID = 'photoPathSign' + i;
        photoID = 'PhotoId' + i;
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
        IsAuthorizedSignatory = $('#enable-authorized-signatory').is(':checked') ? true : false;
        photo = $('#photo-path-sign-image-preview').attr('src');
        photoPathSign = $('#photo-path-sign').get(0);
        dochtml = '<input type="file", id="' + newID + '", class="new-record", name = "DocPath", disabled="true"/>';
        dochtml1 = '<img src="' + photo + '", width="50", height="50", id= "' + photoID + '" />';
        fileCaption = $('#file-caption-sign').val();
        note = $('#note-board-of-director-authorized').val();
        //Modify By Suraj Sonawane [13/09/2024] for Sign Error msg
        path = $('#photo-path-sign').val();
        //Set Default Value if Empty
        if (note === '')
            note = 'None';


        if (designationId === '') {
            result = false;
            $('#board-of-director-designation-id-error').removeClass('d-none');
        }
        else
            $('#board-of-director-designation-id-error').addClass('d-none');
        //Modify By --- Sagar Kare 
        //Modify By --- Dhanshri Wagh -authorizedPersonAddressDetail & authorizedPersonContactDetail On 16/09/2024
        //Validation For Authorized Person In formationNumber
        if (authorizedPersonInformationNumber == '' || authorizedPersonInformationNumber == 'None') {
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
        if (authorizedPersonInformationNumber == '' || typeof authorizedPersonInformationNumber == 'undefined')
            authorizedPersonInformationNumber = 'None';

        // Check Whether Person Information Number Selected For Nominee Or Not?
        if (authorizedPersonInformationNumber == 'None' || typeof authorizedPersonInformationNumber == 'undefined') {
            isSelectedPersonInformationNumberAuthorized = false;
        } else {
            isSelectedPersonInformationNumberAuthorized = true;
        }

        if (!$('#authorized-person-information-number-input').hasClass('d-none')) {
            if (isSelectedPersonInformationNumberAuthorized == false) {
                result = false;
                $('#authorized-person-information-number-error').removeClass('d-none');
            } else {
                $('#authorized-person-information-number-error').addClass('d-none');
            }
        }
        else {
            authorizedPersonInformationNumberText = 'None';
        }

        if (isSelectedPersonInformationNumberAuthorized == false) {

            //Full Name Of Authorized Person
            if (fullNameOfAuthorizedPerson === '' || fullNameOfAuthorizedPerson.length > 150) {
                result = false;
                $('#full-name-of-authorized-member-error').removeClass('d-none');
            }
            else
                $('#full-name-of-authorized-member-error').addClass('d-none');

            //TransFullNameOfAuthorizedPerson
            if (transfullNameOfAuthorizedPerson === '' || transfullNameOfAuthorizedPerson.length > 150) {
                result = false;
                $('#trans-full-name-of-authorized-member-error').removeClass('d-none');
            }
            else
                $('#trans-full-name-of-authorized-member-error').addClass('d-none');
           
            //AuthorizedPersonAddressDetail
            if (authorizedPersonAddressDetail === '' || authorizedPersonAddressDetail.length > 1500) {
                result = false;
                $('#authorized-person-address-detail-error').removeClass('d-none');
            }
            else
                $('#authorized-person-address-detail-error').addClass('d-none');

            //TransAuthorizedPersonAddressDetail
            if (transAuthorizedPersonAddressDetail === '' || transAuthorizedPersonAddressDetail.length > 1500) {
                result = false;
                $('#trans-authorized-person-address-detail-error').removeClass('d-none');
            }
            else
                $('#trans-authorized-person-address-detail-error').addClass('d-none');
            
            
            //AuthorizedPersonContactDetail
            if (authorizedPersonContactDetail === '' || authorizedPersonContactDetail.length > 1500) {
                result = false;
                $('#authorized-person-contact-detail-error').removeClass('d-none');
            }
            else
                $('#authorized-person-contact-detail-error').addClass('d-none');

            //TransAuthorizedPersonContactDetail
            if (transAuthorizedPersonContactDetail === '' || transAuthorizedPersonContactDetail.length > 1500) {
                result = false;
                $('#trans-authorized-person-contact-detail-error').removeClass('d-none');
            }
            else
                $('#trans-authorized-person-contact-detail-error').addClass('d-none');

        }

        if (IsAuthorizedSignatory === true) {
            if (path === '') {
                result = false;
                $('#photo-path-sign-error').removeClass('d-none');
            }
            else
                $('#photo-path-sign-error').addClass('d-none');

            if (fileCaption === '') {
                result = false;
                $('#file-caption-sign-error').removeClass('d-none');
            }
            else
                $('#file-caption-sign-error').addClass('d-none');
        }
        else {
            path = 'None';
            fileCaption = 'None';
            $('#photo-path-sign-error').addClass('d-none');
            $('#file-caption-sign-error').addClass('d-none');
        }

        return result;
    }

    function HideColumnsAuthorizedSignatoryDataTable() {
        boardOfDirectorAuthorizedDataTable.column(1).visible(false);
        boardOfDirectorAuthorizedDataTable.column(9).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Board of Director Relation - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearBoardOfDirectorModalInputs();
    ClearModal('relation');

    // DataTable Add Button 
    $('#btn-add-relation-dt').click(function () {

        event.preventDefault();

        SetModalTitle('relation', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-relation-dt').click(function () {
        SetModalTitle('relation', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-relation-dt').data('rowindex');
            id = $('#relation-modal').attr('id');
            myModal = $('#' + id).modal();

            $('#board-of-director-id', myModal).val(columnValues[1]);
            $('#relation-id', myModal).val(columnValues[3]);
            $('#note-board-of-director', myModal).val(columnValues[5]);
            $('#reason-for-modification-board-of-director', myModal).val(columnValues[6]);

            // Show Modals
            myModal.modal({ show: true });
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

            ClearModal('relation');

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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-relation tbody input[type="checkbox"]:checked').each(function () {
                 boardOfDirectorDataTable.row($('#tbl-relation tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-relation-dt').data('rowindex');
                  EnableNewOperation('relation');

                  $('#select-all-relation').prop('checked', false);
                    // Add Required Error Message, If Table Has Not Any Record
                    if (!boardOfDirectorDataTable.data().any())
                    $('#relation-accordion-error').removeClass('d-none');

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
        //let rvisibility = 0;
        hasDivClass = $('#board-of-director-div').hasClass('d-none');

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }

        if (boardofdirector === '') {
            result = false;
            $('#board-of-director-id-error').removeClass('d-none');
        }
        else
            $('#board-of-director-id-error').addClass('d-none');

        if (relation === '') {
            result = false;
            $('#relation-id-error').removeClass('d-none');
        }
        else
            $('#relation-id-error').addClass('d-none');

        return result;
    }

    function HideColumnsRelationDataTable() {
        boardOfDirectorDataTable.column(1).visible(false);
        boardOfDirectorDataTable.column(3).visible(false);
        boardOfDirectorDataTable.column(6).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Bank Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearBankModalInputs();
    ClearModal('bank-detail');

    // DataTable Add Button 
    $('#btn-add-bank-detail-dt').click(function () {

        event.preventDefault();

        if (($('#bank-statement-upload').val()) === 'M') {
            $('#photo-path-bank').addClass('mandatory-mark');
            $('#file-caption-bank').addClass('mandatory-mark');
        } else {
            $('#photo-path-bank').removeClass('mandatory-mark');
            $('#file-caption-bank').removeClass('mandatory-mark');
        }

        SetModalTitle('bank-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-bank-detail-dt').click(function () {


        SetModalTitle('bank-detail', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-bank-detail-dt').data('rowindex');
            id = $('#bank-detail-modal').attr('id');
            myModal = $('#' + id).modal();

            accountopeningDate = new Date(columnValues[6]);
            accountclosingDate = new Date(columnValues[7]);

            $.get('/DynamicDropdownList/GetBankBranchDropdownListByBankId', { _bankId: columnValues[1], async: false }, function (data, textStatus, jqXHR) {
                $('#bank-branch-id').empty();
                branchList = '<option value="">--- Select Name Of Bank Branch ---</option>';
                for (i = 0; i < data.length; i++) {
                    branchList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
                }
                $('#bank-branch-id').append(branchList);
                $('#bank-branch-id').find("option[value='" + columnValues[3] + "']").prop("selected", true);

            });


            $('#bank-id', myModal).val(columnValues[1]);
            $('#bank-branch-id', myModal).val(columnValues[3]);
            $('#account-number', myModal).val(columnValues[5]);
            $('#opening-date', myModal).val(GetInputDateFormat(accountopeningDate));
            $('#close-date', myModal).val(GetInputDateFormat(accountclosingDate));

            $('#is-default-bank-transaction', myModal).prop('checked', columnValues[8].toString().toLowerCase() === 'true' ? true : false);

            storagePathInput = columnValues[9];

            storagePathId = $(storagePathInput).attr('id');
            docPath = $('#' + storagePathId).get(0);
            files = docPath.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            photoPathBank = $('#photo-path-bank').get(0);
            photoPathBank.files = dt.files;

            photoinput = columnValues[10];
            PhotoPathBankId = $(photoinput).attr('id');
            photoSrc = $('#' + PhotoPathBankId).attr('src');
            $('#photo-path-bank-image-preview').attr('src', photoSrc);

            $('#file-caption-bank', myModal).val(columnValues[11]);

            $('#note-bank-detail', myModal).val(columnValues[12]);

            $('#reason-for-modification-bank-detail', myModal).val(columnValues[13]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-bank-detail-edit-dt').addClass('read-only');
            $('#bank-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-bank-detail-modal').click(function (event) {
        
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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        reasonForModification,
                        PrmKey
            ]).draw();

            //files = photoPathBank.files;
            //if (files.length !== 0) {
            //    dt = new DataTransfer();
            //    for (j = 0; j < files.length; j++) {
            //        f = files[j];
            //        dt.items.add(
            //            new File(
            //                [f.slice(0, f.size, f.type)],
            //                f.name
            //            ));
            //    }
            //}

            //docPath = $('#' + newID).get(0);
            //if (photoPathBank.files.length !== 0) {
            //    docPath.files = dt.files;
            //}


            files = photoPathBank.files;

            // Check if files are present
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        )
                    );
                }

                // Handle the case where the photoPath document is empty
                docPath = $('#' + newID).get(0);
                if (docPath) {
                    // Set files only if photoPathDocument has files
                    if (files.length !== 0) {
                        docPath.files = dt.files;
                    }
                }
            } else {
                // Handle the case where no files are uploaded
                docPath = $('#' + newID).get(0);
                if (docPath) {
                    // Reset files to empty if no files are uploaded
                    docPath.files = new DataTransfer().files;
                }
            }



            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);
            $('#bank-detail-data-table-error').addClass('d-none');


            HideColumnsBankDetailDataTable();

            bankDataTable.columns.adjust().draw();

            ClearModal('bank-detail');

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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        reasonForModification,
                        PrmKey
            ]).draw();

            files = photoPathBank.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);

            if (photoPathBank.files.length !== 0) {
                docPath.files = dt.files;
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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-bank-detail tbody input[type="checkbox"]:checked').each(function () {
                 bankDataTable.row($('#tbl-bank-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-bank-detail-dt').data('rowindex');
                  EnableNewOperation('bank-detail');

                  $('#select-all-bank-detail').prop('checked', false);
                   if (!bankDataTable.data().any())
                    $('#bank-detail-data-table-error').removeClass('d-none');

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
        i = counter;
        newID = 'photoPathBank' + i;
        photoID = 'PhotoId' + i;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        bankId = $('#bank-id option:selected').val();
        bankText = $('#bank-id option:selected').text();
        bankBranch = $('#bank-branch-id option:selected').val();
        bankBranchText = $('#bank-branch-id option:selected').text();
        accountNumber = $('#account-number').val();
        openingDate = $('#opening-date').val();
        closeDate = $('#close-date').val();
        isDefaultBankForTransaction = $('#is-default-bank-transaction').is(':checked') ? "True" : "False";
        photo = $('#photo-path-bank-image-preview').attr('src');
        photoPathBank = $('#photo-path-bank').get(0);
        dochtml = '<input type="file", id="' + newID + '", class="new-record", name = "DocPath", disabled="true"/>';
        dochtml1 = '<img src="' + photo + '", width="50", height="50", id= "' + photoID + '" />';
        fileCaption = $('#file-caption-bank').val();
        note = $('#note-bank-detail').val();
        reasonForModification = $('#reason-for-modification-bank-detail').val();
        rvisibility = 0;
        hasDivClass = $('#bank-detail-div').hasClass('d-none');

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        //if (fileCaption == '')
        //    fileCaption = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }

        PrmKey = 0;

        let docBankUpload = $('#bank-statement-upload').val();

        if (docBankUpload === 'M') {
            path = $('#photo-path-bank').val();
            fileCaption;

        } else if (docBankUpload === 'O') {
            if (photo === '' && fileCaption == '') {
                path = 'None';
                fileCaption = 'None';
            }
            else if (photo === '') {
                path = 'None';
                fileCaption;
            }
            else if (fileCaption === '') {
                path = $('#photo-path-bank').val();
                fileCaption = 'None';
            }
            else if (photo !== '' && fileCaption !== '') {
                path = $('#photo-path-bank').val();
                fileCaption;
            }
        }
        else if (docBankUpload === 'D') {
            path = 'None';
            fileCaption = 'None';
        }

        if (bankId === '') {
            result = false;
            $('#bank-id-error').removeClass('d-none');
        }
        else
            $('#bank-id-error').addClass('d-none');

        if (typeof bankBranch === 'undefined' || bankBranch === null || bankBranch === 0) {
            result = false;
            $('#bank-branch-id-error').removeClass('d-none');
        }
        else
            $('#bank-branch-id-error').addClass('d-none');

        if (accountNumber === '' || accountNumber.length < 1 || accountNumber.length > 30) {
            result = false;
            $('#account-number-error').removeClass('d-none');
        }
        else
            $('#account-number-error').addClass('d-none');

        let isValidOpeningDate = IsValidInputDate('#opening-date');

        if (!isValidOpeningDate) {
            result = false;
            $('#opening-date-error').removeClass('d-none');
        }
        else
            $('#opening-date-error').addClass('d-none');

        if (isDefaultBankForTransaction === '') {
            result = false;
            $('#is-default-bank-transaction-error').removeClass('d-none');
        }
        else
            $('#is-default-bank-transaction-error').addClass('d-none');

        if (path === '') {
            result = false;
            $('#photo-path-bank-error').removeClass('d-none');
        }
        else
            $('#photo-path-bank-error').addClass('d-none');

        if (fileCaption === '') {
            result = false;
            $('#file-caption-bank-error').removeClass('d-none');
        }
        else
            $('#file-caption-bank-error').addClass('d-none');

        return result;
    }

    function HideColumnsBankDetailDataTable() {
        bankDataTable.column(1).visible(false);
        bankDataTable.column(3).visible(false);
        bankDataTable.column(7).visible(false);
        bankDataTable.column(13).visible(false);
        bankDataTable.column(14).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person GST Registration - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearGSTModalInputs();
    ClearModal('gst-registration');

    // DataTable Add Button 
    $('#btn-add-gst-registration-dt').click(function () {
        event.preventDefault();

        SetModalTitle('gst-registration', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-gst-registration-dt').click(function () {
        SetModalTitle('gst-registration', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-gst-registration-dt').data('rowindex');
            id = $('#gst-registration-modal').attr('id');
            myModal = $('#' + id).modal();

            $('#assessment-year', myModal).val(columnValues[1]);
            $('#tax-amount', myModal).val(columnValues[2]);
            storagePathInput = columnValues[3];

            storagePathId = $(storagePathInput).attr('id');
            docPath = $('#' + storagePathId).get(0);
            files = docPath.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            photoPathGst = $('#photo-path-gst').get(0);
            photoPathGst.files = dt.files;

            photoinput = columnValues[4];
            photoPathGstId = $(photoinput).attr('id');
            photoSrc = $('#' + photoPathGstId).attr('src');
            $('#photo-path-gst-image-preview').attr('src', photoSrc);

            $('#file-caption-gst', myModal).val(columnValues[5]);
            $('#note-gst-document', myModal).val(columnValues[6]);
            $('#reason-for-modification-gst-registration', myModal).val(columnValues[7]);

            // Show Modals
            myModal.modal({ show: true });
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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        reasonForModification,
                        PrmKey
            ]).draw();

            files = photoPathGst.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);
            if (photoPathGst.files.length !== 0) {
                docPath.files = dt.files;
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);
            $('#gst-registration-accordion-error').addClass('d-none');

            HideColumnsGSTDataTable();

            gstDataTable.columns.adjust().draw();

            ClearModal('gst-registration');

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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        reasonForModification,
                        PrmKey
            ]).draw();

            files = photoPathGst.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);

            if (photoPathGst.files.length !== 0) {
                docPath.files = dt.files;
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
        result = true;
        counter++;

        i = counter;
        newID = 'Path' + i;
        photoID = 'Photo' + i;
        oldId = 'photoPathGst' + (i - 1);
        columnValues = $('#btn-update-gst-registration-document').data('rowindex');

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        assessmentYear = $('#assessment-year').val();
        taxAmount = $('#tax-amount').val();
        photo = $('#photo-path-gst-image-preview').attr('src');
        photoPathGst = $('#photo-path-gst').get(0);
        dochtml = '<input type="file", id="' + newID + '", name = "DocPath", disabled="true"/>';
        dochtml1 = '<img src="' + photo + '", width="50", height="50", id= "' + photoID + '" />';
        fileCaption = $('#file-caption-gst').val();
        note = $('#note-gst-document').val();
        reasonForModification = $('#reason-for-modification-gst-registration').val();
        rvisibility = 0;
        hasDivClass = $('#gst-registration-div').hasClass('d-none');

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (fileCaption === '')
            fileCaption = 'None';

        if (($('#gst-document-upload').val()) === 'M') {
            path = $('#photo-path-gst').val().trim();
        } else {
            path = 'None';
        }

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }

        if (assessmentYear === '') {
            result = false;
            $('#assessment-year-error').removeClass('d-none');
        }
        else
            $('#assessment-year-error').addClass('d-none');

        if (taxAmount === '' || taxAmount < 0 || taxAmount > 999999999) {
            result = false;
            $('#tax-amount-error').removeClass('d-none');
        }
        else
            $('#tax-amount-error').addClass('d-none');

        if (path === '') {
            result = false;
            $('#photo-path-gst-error').removeClass('d-none');
        }
        else
            $('#photo-path-gst-error').addClass('d-none');

        return result;
    }

    function HideColumnsGSTDataTable() {
        gstDataTable.column(7).visible(false);
        gstDataTable.column(8).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Chronic Disease - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('chronic-disease');

    // DataTable Add Button 
    $('#btn-add-chronic-disease-dt').click(function () {

        event.preventDefault();

        SetModalTitle('chronic-disease', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-chronic-disease-dt').click(function () {
        SetModalTitle('chronic-disease', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-chronic-disease-dt').data('rowindex');
            id = $('#chronic-disease-modal').attr('id');
            myModal = $('#' + id).modal();

            $('#disease-id', myModal).val(columnValues[1]);
            $('#other-detail', myModal).val(columnValues[3]);
            $('#note-chronic-disease', myModal).val(columnValues[4]);
            $('#reason-for-modification-chronic-disease', myModal).val(columnValues[5]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-chronic-disease-edit-dt').addClass('read-only');
            $('#chronic-disease-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-chronic-disease-modal').click(function (event) {
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

            ClearModal('chronic-disease');

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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-chronic-disease tbody input[type="checkbox"]:checked').each(function () {
                 chronicDataTable.row($('#tbl-chronic-disease tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-chronic-disease-dt').data('rowindex');
                  EnableNewOperation('chronic-disease');

                  $('#select-all-chronic-disease').prop('checked', false);
                    if (!chronicDataTable.data().any())
                    $('#chronic-disease-data-table-error').removeClass('d-none');

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
        rvisibility = 0;
        hasDivClass = $('#chronic-div').hasClass('d-none');

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }

        if (disease === '') {
            result = false;
            $('#disease-id-error').removeClass('d-none');
        }
        else
            $('#disease-id-error').addClass('d-none');

        minimumLength = parseInt($('#other-detail').attr('minlength'));
        maximumLength = parseInt($('#other-detail').attr('maxlength'));

        if (otherDetails === '' || parseInt(otherDetails.length) < parseInt(minimumLength) || parseInt(otherDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#other-detail-error').removeClass('d-none');
        } else
            $('#other-detail-error').addClass('d-none');


        return result;
    }

    function HideColumnsChronicDiseaseDataTable() {
        chronicDataTable.column(1).visible(false);
        chronicDataTable.column(5).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Insurance Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('insurance-detail');

    // DataTable Add Button 
    $('#btn-add-insurance-detail-dt').click(function () {

        event.preventDefault();

        SetModalTitle('insurance-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-insurance-detail-dt').click(function () {
        SetModalTitle('insurance-detail', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-insurance-detail-dt').data('rowindex');
            id = $('#insurance-detail-modal').attr('id');
            myModal = $('#' + id).modal();

            startDate = new Date(columnValues[5]),
                month = '' + (startDate.getMonth() + 1),
                day = '' + startDate.getDate(),
                year = startDate.getFullYear();
            if (month.length < 2) month = '0' + month;
            if (day.length < 2) day = '0' + day;
            startDate = [year, month, day].join('-');

            maturityDate = new Date(columnValues[6]),
                month = '' + (maturityDate.getMonth() + 1),
                day = '' + maturityDate.getDate(),
                year = maturityDate.getFullYear();
            if (month.length < 2) month = '0' + month;
            if (day.length < 2) day = '0' + day;
            maturityDate = [year, month, day].join('-');

            closeDate = new Date(columnValues[7]),
                month = '' + (closeDate.getMonth() + 1),
                day = '' + closeDate.getDate(),
                year = closeDate.getFullYear();
            if (month.length < 2) month = '0' + month;
            if (day.length < 2) day = '0' + day;
            closeDate = [year, month, day].join('-');

            $('#insurance-type-id', myModal).val(columnValues[1]);
            $('#insurance-company-id', myModal).val(columnValues[3]);
            $('#start-date', myModal).val(startDate);
            $('#maturity-date-person-insurance', myModal).val(maturityDate);
            $('#close-date-person-insurance', myModal).val(closeDate);
            $('#policy-number', myModal).val(columnValues[8]);
            $('#policy-premium', myModal).val(columnValues[9]);
            $('#policy-sum-assured', myModal).val(columnValues[10]);
            $('#overdues-premium', myModal).val(columnValues[11]);
            $('#has-any-mortgage-insurance', myModal).val(columnValues[12]);
            $('#note-insurance-detail', myModal).val(columnValues[13]);
            $('#reason-for-modification-insurance-detail', myModal).val(columnValues[14]);

            if (columnValues[12] === 'True') {
                $('#has-any-mortgage-insurance').prop('checked', true);
            }
            else {
                $('#has-any-mortgage-insurance').prop('checked', false);
            }

            // Show Modals
            myModal.modal({ show: true });
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
                        PolicyNumber,
                        PolicyPremium,
                        PolicySumAssured,
                        OverduesPremium,
                        hasAnyMortgage,
                        note,
                        reasonForModification
            ]).draw();

            // Error Message In Span
            $('#insurance-detail-data-table-error').addClass('d-none');

            HideColumnsInsuranceDataTable();

            insuranceDataTable.columns.adjust().draw();

            ClearModal('insurance-detail');

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
                                PolicyNumber,
                                PolicyPremium,
                                PolicySumAssured,
                                OverduesPremium,
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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-insurance-detail tbody input[type="checkbox"]:checked').each(function () {
                 insuranceDataTable.row($('#tbl-insurance-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-insurance-detail-dt').data('rowindex');
                  EnableNewOperation('insurance-detail');

                  $('#select-all-insurance-detail').prop('checked', false);
                   if (!insuranceDataTable.data().any())
                    $('#insurance-detail-data-table-error').removeClass('d-none');

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
        
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        insuranceType = $('#insurance-type-id option:selected').val();
        insuranceTypeText = $('#insurance-type-id option:selected').text();
        insuranceCompany = $('#insurance-company-id option:selected').val();
        insuranceCompanyText = $('#insurance-company-id option:selected').text();
        startDate = $('#start-date').val();
        maturityDate = $('#maturity-date-person-insurance').val();
        closeDate = $('#close-date-person-insurance').val();
        PolicyNumber = $('#policy-number').val();
        PolicyPremium = $('#policy-premium').val();
        PolicySumAssured = $('#policy-sum-assured').val();
        OverduesPremium = $('#overdues-premium').val();
        hasAnyMortgage = $('#has-any-mortgage-insurance').is(':checked') ? "True" : "False";

        note = $('#note-insurance-detail').val();
        reasonForModification = $('#reason-for-modification-insurance-detail').val();
        rvisibility = 0;
        hasDivClass = $('#insurance-div').hasClass('d-none');


        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification == '')
                reasonForModification = 'None';
        }

        if (insuranceType === '') {
            result = false;
            $('#insurance-type-id-error').removeClass('d-none');
        }
        else
            $('#insurance-type-id-error').addClass('d-none');

        if (insuranceCompany === '') {
            result = false;
            $('#insurance-company-id-error').removeClass('d-none');
        }
        else
            $('#insurance-company-id-error').addClass('d-none');

        let isValidStartDate = IsValidInputDate('#start-date');

        if (!isValidStartDate) {
            result = false;
            $('#start-date-error').removeClass('d-none');
        }
        else
            $('#start-date-error').addClass('d-none');

        let isValidMaturityDate = IsValidInputDate('#maturity-date-person-insurance');

        if (!isValidMaturityDate) {
            result = false;
            $('#maturity-date-person-insurance-error').removeClass('d-none');
        }
        else
            $('#maturity-date-person-insurance-error').addClass('d-none');

        if (PolicyNumber === '' || PolicyNumber.length < 3 || PolicyNumber.length > 50) {
            result = false;
            $('#policy-number-error').removeClass('d-none');
        } else
            $('#policy-number-error').addClass('d-none');

        if (PolicyPremium === '' || PolicyPremium < 100 || PolicyPremium > 999999) {
            result = false;
            $('#policy-premium-error').removeClass('d-none');
        } else
            $('#policy-premium-error').addClass('d-none');

        let sum = Math.floor(PolicyPremium * 2);
        if (PolicySumAssured === '' || PolicySumAssured < sum || PolicySumAssured > 999999999) {
            result = false;
            $('#policy-sum-assured-error').removeClass('d-none');
        } else {
            $('#policy-sum-assured-error').addClass('d-none');
        }

        if (OverduesPremium === '' || OverduesPremium < 0 || OverduesPremium > 9999) {
            result = false;
            $('#overdues-premium-error').removeClass('d-none');
        }
        else
            $('#overdues-premium-error').addClass('d-none');
        return result;
    }

    function HideColumnsInsuranceDataTable() {
        insuranceDataTable.column(1).visible(false);
        insuranceDataTable.column(3).visible(false);
        insuranceDataTable.column(7).visible(false);
        insuranceDataTable.column(14).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Financial Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Clear contact ModalInputs();
    ClearModal('financial-asset');

    // DataTable Add Button 
    $('#btn-add-financial-asset-dt').click(function () {
        event.preventDefault();

        if (($('#financial-asset-document-upload').val()) === 'M') {
            $('#photo-path-finance').addClass('mandatory-mark');
            $('#file-caption-finance').addClass('mandatory-mark');
        } else {
            $('#photo-path-finance').removeClass('mandatory-mark');
            $('#file-caption-finance').removeClass('mandatory-mark');
        }

        SetModalTitle('financial-asset', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-financial-asset-dt').click(function () {
        SetModalTitle('financial-asset', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-financial-asset-dt').data('rowindex');
            id = $('#financial-asset-modal').attr('id');
            myModal = $('#' + id).modal();

            financialAssetOpeningDate = new Date(columnValues[11]);
            financialAssetMatureDate = new Date(columnValues[12]);

            $('#financial-organization-type', myModal).val(columnValues[1]);
            $('#name-of-financial-organization', myModal).val(columnValues[3]);
            $('#trans-name-of-financial-organization', myModal).val(columnValues[4]);
            $('#name-of-branch', myModal).val(columnValues[5]);
            $('#trans-name-of-branch', myModal).val(columnValues[6]);
            $('#address-details', myModal).val(columnValues[7]);
            $('#trans-address-details', myModal).val(columnValues[8]);
            $('#contact-details', myModal).val(columnValues[9]);
            $('#trans-contact-details', myModal).val(columnValues[10]);
            $('#opening-dates', myModal).val(GetInputDateFormat(financialAssetOpeningDate));
            $('#maturity-date', myModal).val(GetInputDateFormat(financialAssetMatureDate));
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

            $('#has-any-mortgage-financial', myModal).val(columnValues[22]);

            if (columnValues[22] === 'True')
                $('#has-any-mortgage-financial').prop('checked', true);
            else
                $('#has-any-mortgage-financial').prop('checked', false);

            storagePathInput = columnValues[23];

            storagePathId = $(storagePathInput).attr('id');
            docPath = $('#' + storagePathId).get(0);
            files = docPath.files;

            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            photoPathFinance = $('#photo-path-finance').get(0);
            photoPathFinance.files = dt.files;

            photoinput = columnValues[24];
            photoPathFinanceId = $(photoinput).attr('id');
            photoSrc = $('#' + photoPathFinanceId).attr('src');
            $('#photo-path-finance-image-preview').attr('src', photoSrc);

            $('#file-caption-finance', myModal).val(columnValues[25]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-financial-asset-dt').addClass('read-only');
            $('#financial-asset-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-financial-asset-modal').click(function (event) {
        
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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        transNote,
                        reasonForModification,
                        prmKey
            ]).draw();

            files = photoPathFinance.files;

            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);
            if (photoPathFinance.files.length !== 0) {
                docPath.files = dt.files;
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            // Error Message In Span
            $('#financial-asset-data-table-error').addClass('d-none');

            HideColumnsFinancialDataTable();

            financialDataTable.columns.adjust().draw();

            ClearModal('financial-asset');

            $('#financial-asset-modal').modal('hide');

            EnableNewOperation('financial-asset');
        }
    });

    // Modal update Button Event
    $('#btn-update-financial-asset-modal').click(function (event) {
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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        transNote,
                        reasonForModification,
                        prmKey
            ]).draw();

            files = photoPathFinance.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);

            if (photoPathFinance.files.length !== 0) {
                docPath.files = dt.files;
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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-financial-asset tbody input[type="checkbox"]:checked').each(function () {
                 financialDataTable.row($('#tbl-financial-asset tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-financial-asset-dt').data('rowindex');
                  EnableNewOperation('financial-asset');

                  $('#select-all-financial-asset').prop('checked', false);
                   if (!financialDataTable.data().any())
                    $('#financial-asset-data-table-error').removeClass('d-none');

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
        
        result = true;
        counter++;
        i = counter;
        newID = 'photoPathFinance' + i;
        photoID = 'PhotoId' + i;

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
        openingDate = $('#opening-dates').val();
        maturityDate = $('#maturity-date').val();
        financialAssetType = $('#financial-asset-type option:selected').val();
        financialAssetTypeText = $('#financial-asset-type option:selected').text();
        financialAssetDescription = $('#financial-asset-description').val();
        transFinancialAssetDescription = $('#trans-financial-asset-description').val();
        referenceNumber = $('#references-number').val();
        transReferenceNumber = $('#trans-references-number').val();
        investedAmount = $('#invested-amount').val();
        monthlyInterestIncomeAmount = parseFloat($('#monthly-interest-income-amount').val());
        currentMarketValue = $('#current-market-values').val();
        hasAnyMortgage = $('input[name="PersonFinancialAssetViewModel.HasAnyMortgage"]').is(':checked') ? "True" : "False";
        photo = $('#photo-path-finance-image-preview').attr('src');
        photoPathFinance = $('#photo-path-finance').get(0);
        dochtml = '<input type="file", id="' + newID + '", class="new-record", name = "DocPath", disabled="true"/>';
        dochtml1 = '<img src="' + photo + '", width="50", height="50", id= "' + photoID + '" />';
        note = $('#note-financial-asset').val();
        transNote = $('#trans-note-financial-asset').val();
        reasonForModification = $('#reason-for-modification-financial-asset').val();
        fileCaption = $('#file-caption-finance').val();
        rvisibility = 0;
        hasDivClass = $('#financial-div').hasClass('d-none');

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (transNote === '')
            transNote = 'None';


        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }

        prmKey = 0;

        let docFinancekUpload = $('#financial-asset-document-upload').val();

        if (docFinancekUpload === 'M') {
            path = $('#photo-path-finance').val();
            fileCaption;

        } else if (docFinancekUpload === 'O') {
            if (photo === '' && fileCaption === '') {
                path = 'None';
                fileCaption = 'None';
            }
            else if (photo === '') {
                path = 'None';
                fileCaption;
            }
            else if (fileCaption === '') {
                path = $('#photo-path-finance').val();
                fileCaption = 'None';
            }
            else if (photo !== '' && fileCaption !== '') {
                path = $('#photo-path-finance').val();
                fileCaption;
            }
        }
        else if (docFinancekUpload === 'D') {
            if (photo === '') {
                path = 'None';
                fileCaption = 'None';
            }
        }


        if (financialOrganizationTypeId === '') {
            result = false;
            $('#financial-organization-type-error').removeClass('d-none');
        }
        else
            $('#financial-organization-type-error').addClass('d-none');

        if (nameOfFinancialOrganization === '' || nameOfFinancialOrganization.length < 3 || nameOfFinancialOrganization.length > 150) {
            result = false;
            $('#name-of-financial-organization-error').removeClass('d-none');
        } else
            $('#name-of-financial-organization-error').addClass('d-none');

        if (transNameOfFinancialOrganization === '' || transNameOfFinancialOrganization.length > 150) {
            result = false;
            $('#trans-name-of-financial-organization-error').removeClass('d-none');
        }
        else
            $('#trans-name-of-financial-organization-error').addClass('d-none');


        if (nameOfBranch === '' || nameOfBranch.length < 3 || nameOfBranch.length > 50) {
            result = false;
            $('#name-of-branch-error').removeClass('d-none');
        }
        else
            $('#name-of-branch-error').addClass('d-none');

        if (transNameOfBranch === '' || transNameOfBranch.length > 50) {
            result = false;
            $('#trans-name-of-branch-error').removeClass('d-none');
        }
        else
            $('#trans-name-of-branch-error').addClass('d-none');

        if (addressDetails === '' || addressDetails.length > 1500) {
            result = false;
            $('#address-details-error').removeClass('d-none');
        }
        else
            $('#address-details-error').addClass('d-none');

        if (transAddressDetails === '' || transAddressDetails.length > 1500) {
            result = false;
            $('#trans-address-details-error').removeClass('d-none');
        }
        else
            $('#trans-address-details-error').addClass('d-none');

        if (contactDetails === '' || contactDetails.length > 500) {
            result = false;
            $('#contact-details-error').removeClass('d-none');
        }
        else
            $('#contact-details-error').addClass('d-none');

        if (transContactDetails === '' || transContactDetails.length > 500) {
            result = false;
            $('#trans-contact-details-error').removeClass('d-none');
        }
        else
            $('#trans-contact-details-error').addClass('d-none');

        let isValidOpeningDate = IsValidInputDate('#opening-dates');

        if (!isValidOpeningDate) {
            result = false;
            $('#opening-dates-error').removeClass('d-none');
        }
        else
            $('#opening-dates-error').addClass('d-none');

        let isValidMaturityDate = IsValidInputDate('#maturity-date');

        if (!isValidMaturityDate) {
            result = false;
            $('#maturity-date-error').removeClass('d-none');
        }
        else
            $('#maturity-date-error').addClass('d-none');

        if (financialAssetType === '') {
            result = false;
            $('#financial-asset-type-error').removeClass('d-none');
        }
        else
            $('#financial-asset-type-error').addClass('d-none');

        if (financialAssetDescription === '' || financialAssetDescription.length > 1500) {
            result = false;
            $('#financial-asset-description-error').removeClass('d-none');
        }
        else
            $('#financial-asset-description-error').addClass('d-none');

        if (transFinancialAssetDescription === '' || transFinancialAssetDescription.length > 1500) {
            result = false;
            $('#trans-financial-asset-description-error').removeClass('d-none');
        }
        else
            $('#trans-financial-asset-description-error').addClass('d-none');

        if (referenceNumber === '' || referenceNumber.length > 50) {
            result = false;
            $('#references-number-error').removeClass('d-none');
        }
        else
            $('#references-number-error').addClass('d-none');

        if (transReferenceNumber === '' || transReferenceNumber.length > 50) {
            result = false;
            $('#trans-references-number-error').removeClass('d-none');
        }
        else
            $('#trans-references-number-error').addClass('d-none');

        if (investedAmount === '' || investedAmount < 1 || investedAmount > 999999999) {
            result = false;
            $('#invested-amount-error').removeClass('d-none');
        }
        else
            $('#invested-amount-error').addClass('d-none');

        //let amount = Math.floor(investedAmount * 0.2);
        let amount = investedAmount * 0.2;

        if (isNaN(monthlyInterestIncomeAmount) || parseFloat(monthlyInterestIncomeAmount) < 0 || parseFloat(monthlyInterestIncomeAmount) > amount) {
            result = false;
            $('#monthly-interest-income-amount-error').removeClass('d-none');
        }
        else
            $('#monthly-interest-income-amount-error').addClass('d-none');

        if (currentMarketValue === '' || currentMarketValue < 1 || currentMarketValue > 999999999) {
            result = false;
            $('#current-market-values-error').removeClass('d-none');
        }
        else
            $('#current-market-values-error').addClass('d-none');

        if (path === '') {
            result = false;
            $('#photo-path-finance-error').removeClass('d-none');
        }
        else
            $('#photo-path-finance-error').addClass('d-none');

        if (fileCaption === '') {
            result = false;
            $('#file-caption-finance-error').removeClass('d-none');
        }
        else
            $('#file-caption-finance-error').addClass('d-none');

        return result;

    }

    function HideColumnsFinancialDataTable() {
        financialDataTable.column(1).visible(false);
        financialDataTable.column(13).visible(false);
        financialDataTable.column(28).visible(false);
        financialDataTable.column(29).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Movable Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('movable-asset');

    // DataTable Add Button 
    $('#btn-add-movable-asset-dt').click(function () {
        //Clear Purchase Dates //Modify By -- Sagar Kare
        $('#purchase-price-movable-asset').val('').removeAttr('min max').blur();

        event.preventDefault();

        if (($('#movable-asset-document-upload').val()) === 'M') {
            $('#photo-path-movable').addClass('mandatory-mark');
            $('#file-caption-movable').addClass('mandatory-mark');
        } else {
            $('#photo-path-movable').removeClass('mandatory-mark');
            $('#file-caption-movable').removeClass('mandatory-mark');
        }

        SetModalTitle('movable-asset', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-movable-asset-dt').click(function () {
        SetModalTitle('movable-asset', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-movable-asset-dt').data('rowindex');
            id = $('#movable-asset-modal').attr('id');
            myModal = $('#' + id).modal();

            movableAssetRegistrationDate = new Date(columnValues[8]);
            movableDateOfPurchase = new Date(columnValues[7]);

            $.get('/DynamicDropdownList/GetVehicleVariantDropdownListByVehicleModelId', { _vehicleModelId: columnValues[1], async: false }, function (data, textStatus, jqXHR) {
                $('#vehicle-variant-id').empty();
                letiantList = '<option value="">-- Select Vehicle Variant --</option>';
                for (i = 0; i < data.length; i++) {
                    letiantList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
                }
                $('#vehicle-variant-id').append(letiantList);
                $('#vehicle-variant-id').find("option[ value ='" + columnValues[3] + "' ]").prop('selected', true);

            });

            $('#vehicle-model-id', myModal).val(columnValues[1]);
            $('#vehicle-letiant-id', myModal).val(columnValues[3]);
            $('#number-of-owners-movable-asset', myModal).val(columnValues[5]);
            $('#manufacturing-year-movable-asset', myModal).val(columnValues[6]);
            $('#date-of-purchase-movable-asset', myModal).val(GetInputDateFormat(movableDateOfPurchase));
            $('#registration-date-movable-asset', myModal).val(GetInputDateFormat(movableAssetRegistrationDate));
            $('#registration-number-movable-asset', myModal).val(columnValues[9]);
            $('#purchase-price-movable-asset', myModal).val(columnValues[10]);
            $('#current-market-value-movable-asset', myModal).val(columnValues[11]);
            $('#ownership-percentage-movable-asset', myModal).val(columnValues[12]);
            $('#has-any-mortgage-movable', myModal).val(columnValues[13]);
            if (columnValues[13] === 'True') {
                $('#has-any-mortgage-movable').prop('checked', true);
            }
            else {
                $('#has-any-mortgage-movable').prop('checked', false);
            }
            $('#is-ownership-deceased-movable', myModal).val(columnValues[14]);
            if (columnValues[14] === 'True') {
                $('#is-ownership-deceased-movable').prop('checked', true);
            }
            else {
                $('#is-ownership-deceased-movable').prop('checked', false);
            }

            storagePathInput = columnValues[15];

            storagePathId = $(storagePathInput).attr('id');
            docPath = $('#' + storagePathId).get(0);
            files = docPath.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            photoPathMovable = $('#photo-path-movable').get(0);
            photoPathMovable.files = dt.files;

            photoinput = columnValues[16];
            photoPathMovableId = $(photoinput).attr('id');
            photoSrc = $('#' + photoPathMovableId).attr('src');
            $('#photo-path-movable-image-preview').attr('src', photoSrc);

            $('#file-caption-movable', myModal).val(columnValues[17]);
            $('#note-movable-asset', myModal).val(columnValues[18]);
            $('#reason-for-modification-movable-asset', myModal).val(columnValues[19]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-movable-asset-edit-dt').addClass('read-only');
            $('#movable-asset-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-movable-asset-modal').click(function (event) {
        
        if (IsValidMovableAssetModal()) {
            row = movableDataTable.row.add([
                        tag,
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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        reasonForModification,
                        PrmKey
            ]).draw();

            files = photoPathMovable.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);
            if (photoPathMovable.files.length !== 0) {
                docPath.files = dt.files;
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            $('#movable-asset-data-table-error').addClass('d-none');

            HideColumnsMovableAssetDataTable();

            movableDataTable.columns.adjust().draw();

            ClearModal('movable-asset');

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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        reasonForModification,
                        PrmKey
            ]).draw();

            files = photoPathMovable.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);

            if (photoPathMovable.files.length !== 0) {
                docPath.files = dt.files;
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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-movable-asset tbody input[type="checkbox"]:checked').each(function () {
                 movableDataTable.row($('#tbl-movable-asset tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-movable-asset-dt').data('rowindex');
                  EnableNewOperation('movable-asset');

                  $('#select-all-movable-asset').prop('checked', false);
                   if (!movableDataTable.data().any())
                    $('#movable-asset-data-table-error').removeClass('d-none');

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

                    EnableEditDeleteOperation('movable-asset');

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
        
        result = true;

        counter++;
        i = counter;
        newID = 'photoPathMovable' + i;
        photoID = 'PhotoId' + i;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        vehicleModelId = $('#vehicle-model-id option:selected').val();
        vehicleModelIdText = $('#vehicle-model-id option:selected').text();
        vehicleletiant = $('#vehicle-variant-id option:selected').val();
        vehicleletiantText = $('#vehicle-variant-id option:selected').text();
        numberOfOwners = parseInt($('#number-of-owners-movable-asset').val());
        manufacturingYear = $('#manufacturing-year-movable-asset').val();
        dateOfPurchase = $('#date-of-purchase-movable-asset').val();
        registrationDate = $('#registration-date-movable-asset').val();
        registrationNumber = $('#registration-number-movable-asset').val();
        purchasePrice = $('#purchase-price-movable-asset').val();
        currentMarketValue = $('#current-market-value-movable-asset').val();
        ownershipPercentage = $('#ownership-percentage-movable-asset').val();
        isOwnershipDeceased = $('#is-ownership-deceased-movable').is(':checked') ? 'True' : 'False';
        hasAnyMortgage = $('#has-any-mortgage-movable').is(':checked') ? 'True' : 'False';
        photo = $('#photo-path-movable-image-preview').attr('src');
        photoPathMovable = $('#photo-path-movable').get(0);
        dochtml = '<input type="file", id="' + newID + '", class="new-record", name = "DocPath", disabled="true"/>'
        dochtml1 = '<img src="' + photo + '", width="50", height="50", id= "' + photoID + '" />'
        fileCaption = $('#file-caption-movable').val();
        assetFullDescription = $('#asset-full-description').val();
        PrmKey = 0;

        let docMovableUpload = $('#movable-asset-document-upload').val();

        if (docMovableUpload === 'M') {
            path = $('#photo-path-movable').val();
            fileCaption;

        } else if (docMovableUpload === 'O') {
            if (photo === '' && fileCaption === '') {
                path = 'None';
                fileCaption = 'None';
            }
            else if (photo === '') {
                path = 'None';
                fileCaption;
            }
            else if (fileCaption === '') {
                path = $('#photo-path-movable').val();
                fileCaption = 'None';
            }
            else if (photo !== '' && fileCaption !== '') {
                path = $('#photo-path-movable').val();
                fileCaption;
            }
        }
        else if (docMovableUpload === 'D') {
            if (photo === '') {
                path = 'None';
                fileCaption = 'None';
            }
        }


        note = $('#note-movable-asset').val();
        reasonForModification = $('#reason-for-modification-movable-asset').val().trim();
        rvisibility = 0;
        hasDivClass = $('#movable-asset-div').hasClass('d-none');

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (assetFullDescription === '')
            assetFullDescription = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification == '')
                reasonForModification = 'None';
        }

        if (vehicleModelId === '') {
            result = false;
            $('#vehicle-model-id-error').removeClass('d-none');
        }
        else
            $('#vehicle-model-id-error').addClass('d-none');

        if (typeof vehicleletiant === 'undefined' || vehicleletiant === null || vehicleletiant === 0) {
            result = false;
            $('#vehicle-variant-id-error').removeClass('d-none');
        }
        else
            $('#vehicle-variant-id-error').addClass('d-none');

        if (isNaN(numberOfOwners) || parseInt(numberOfOwners) < parseInt(minimum) || parseInt(numberOfOwners) > parseInt(maximum)) {
            result = false;
            $('#number-of-owners-movable-asset-error').removeClass('d-none');
        }
        else
            $('#number-of-owners-movable-asset-error').addClass('d-none');

        // Get the current year
        let currentYear = new Date().getFullYear();

        // Calculate the minimum allowed year
        let minAllowedYear = currentYear - 50;

        // Generate allowed years list dynamically
        let allowedYears = [];
        for (let i = minAllowedYear; i <= currentYear; i++) {
            allowedYears.push(i.toString());
        }

        // Assuming assessmentYear is a string letiable containing the year
        if (manufacturingYear === '' || !allowedYears.includes(manufacturingYear)) {
            result = false;
            $('#manufacturing-year-movable-asset-error').removeClass('d-none');
        } else
            $('#manufacturing-year-movable-asset-error').addClass('d-none');

        minimum = $('#number-of-owners-movable-asset').attr('min');
        maximum = $('#number-of-owners-movable-asset').attr('max');


        let isValidDateOfPurchase = IsValidInputDate('#date-of-purchase-movable-asset');

        if (!isValidDateOfPurchase) {
            result = false;
            $('#date-of-purchase-movable-asset-error').removeClass('d-none');
        }
        else
            $('#date-of-purchase-movable-asset-error').addClass('d-none');


        let isValidRegistrationDate = IsValidInputDate('#registration-date-movable-asset');

        if (!isValidRegistrationDate) {
            result = false;
            $('#registration-date-movable-asset-error').removeClass('d-none');
        }
        else
            $('#registration-date-movable-asset-error').addClass('d-none');

        // Regular expression for validation
        let regex = /^[A-Za-z]{2}[ -][0-9]{1,2}(?: [A-Za-z]{0,2})?(?: [A-Za-z]{0,3})? [0-9]{4}$/;

        // Check if registrationNumber is empty or doesn't match the regex
        if (registrationNumber === '' || !regex.test(registrationNumber)) {
            result = false;
            $('#registration-number-movable-asset-error').removeClass('d-none');
        } else
            $('#registration-number-movable-asset-error').addClass('d-none');


        if (purchasePrice === '') {
            result = false;
            $('#purchase-price-movable-asset-error').removeClass('d-none');
        }
        else
            $('#purchase-price-movable-asset-error').addClass('d-none');

        if (currentMarketValue === '' || currentMarketValue > purchasePrice) {
            result = false;
            $('#current-market-value-movable-asset-error').removeClass('d-none');
        }
        else
            $('#current-market-value-movable-asset-error').addClass('d-none');

        if (ownershipPercentage === '') {
            result = false;
            $('#ownership-percentage-movable-asset-error').removeClass('d-none');
        }
        else
            $('#ownership-percentage-movable-asset-error').addClass('d-none');

        if (path === '') {
            result = false;
            $('#photo-path-movable-error').removeClass('d-none');
        }
        else
            $('#photo-path-movable-error').addClass('d-none');

        if (fileCaption === '') {
            result = false;
            $('#file-caption-movable-error').removeClass('d-none');
        }
        else
            $('#file-caption-movable-error').addClass('d-none');

        return result;
    }

    function HideColumnsMovableAssetDataTable() {
        movableDataTable.column(1).visible(false);
        movableDataTable.column(3).visible(false);
        movableDataTable.column(19).visible(false);
        movableDataTable.column(20).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Immovable Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('immovable-asset');

    // DataTable Add Button 
    $('#btn-add-immovable-asset-dt').click(function () {

        event.preventDefault();

        if (($('#immovable-asset-document-upload').val()) === 'M') {
            $('#photo-path-immovable').addClass('mandatory-mark');
            $('#file-caption-immovable').addClass('mandatory-mark');
        } else {
            $('#photo-path-immovable').removeClass('mandatory-mark');
            $('#file-caption-immovable').removeClass('mandatory-mark');
        }

        SetModalTitle('immovable-asset', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-immovable-asset-dt').click(function () {
        
        SetModalTitle('immovable-asset', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-immovable-asset-dt').data('rowindex');
            id = $('#immovable-asset-modal').attr('id');
            myModal = $('#' + id).modal();

            $('#survey-numbers', myModal).val(columnValues[1]);
            $('#city-survey-numbers', myModal).val(columnValues[2]);
            $('#number', myModal).val(columnValues[3]);
            $('#area-of-land-immovable', myModal).val(columnValues[4]);

            $('#construction-area', myModal).val(columnValues[5]);
            $('#carpet-area', myModal).val(columnValues[6]);
            $('#current-market-value-immovable', myModal).val(columnValues[7]);
            $('#annual-rent-income', myModal).val(columnValues[8]);
            $('#residence-types-id', myModal).val(columnValues[9]);
            $('#ownership-types-id', myModal).val(columnValues[11]);
            $('#ownership-percentage-immovable-asset', myModal).val(columnValues[13]);

            $('#is-constructed', myModal).prop('checked', columnValues[14].toString().toLowerCase() === 'true' ? true : false);

            $('#has-any-mortgage-immovable', myModal).prop('checked', columnValues[15].toString().toLowerCase() === 'true' ? true : false);

            $('#is-ownership-deceased-immovable', myModal).prop('checked', columnValues[16].toString().toLowerCase() === 'true' ? true : false);

            storagePathInput = columnValues[17];

            storagePathId = $(storagePathInput).attr('id');
            docPath = $('#' + storagePathId).get(0);
            files = docPath.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            photoPathImmovable = $('#photo-path-immovable').get(0);
            photoPathImmovable.files = dt.files;

            photoinput = columnValues[18];
            photoPathImmovableId = $(photoinput).attr('id');
            photoSrc = $('#' + photoPathImmovableId).attr('src')
            $('#photo-path-immovable-image-preview').attr('src', photoSrc);
            $('#file-caption-immovable', myModal).val(columnValues[19]);
            $('#asset-full-description', myModal).val(columnValues[20]);
            $('#note-immovable-asset', myModal).val(columnValues[21]);
            $('#reason-for-modification-immovable-asset', myModal).val(columnValues[22]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-immovable-asset-edit-dt').addClass('read-only');
            $('#immovable-asset-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-immovable-asset-modal').click(function (event) {
        
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
                                dochtml,
                                dochtml1,
                                fileCaption,
                                assetFullDescription,
                                note,
                                reasonForModification,
                                prmKey
            ]).draw();

            files = photoPathImmovable.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);
            if (photoPathImmovable.files.length !== 0) {
                docPath.files = dt.files;
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);
            $('#immovable-asset-data-table-error').addClass('d-none');

            HideColumnsImmovableDataTable();

            immovableDataTable.columns.adjust().draw();

            ClearModal('immovable-asset');

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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        assetFullDescription,
                        note,
                        reasonForModification,
                        prmKey
            ]).draw();

            files = photoPathImmovable.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);

            if (photoPathImmovable.files.length !== 0) {
                docPath.files = dt.files;
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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-immovable-asset tbody input[type="checkbox"]:checked').each(function () {
                 immovableDataTable.row($('#tbl-immovable-asset tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-immovable-asset-dt').data('rowindex');
                  EnableNewOperation('immovable-asset');

                  $('#select-all-immovable-asset').prop('checked', false);
                   if (!immovableDataTable.data().any())
                    $('#immovable-asset-data-table-error').removeClass('d-none');

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
        i = counter;
        newID = 'photoPathImmovable' + i;
        photoID = 'PhotoId' + i;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        surveyNumber = $('#survey-numbers').val();
        citySurveyNumber = $('#city-survey-numbers').val();
        number = $('#number').val().trim();
        areaOfLand = $('#area-of-land-immovable').val();
        constructionArea = $('#construction-area').val();
        carpetArea = $('#carpet-area').val();
        currentMarketValue = $('#current-market-value-immovable').val();
        annualRentIncome = $('#annual-rent-income').val();
        residenceType = $('#residence-types-id option:selected').val();
        residenceTypeText = $('#residence-types-id option:selected').text();
        ownershipType = $('#ownership-types-id option:selected').val();
        ownershipTypeText = $('#ownership-types-id option:selected').text();
        ownershipPercentage = $('#ownership-percentage-immovable-asset').val();
        isConstructed = $('#is-constructed').is(':checked') ? 'True' : 'False';
        hasAnyMortgage = $('#has-any-mortgage-immovable').is(':checked') ? 'True' : 'False';
        isOwnershipDeceased = $('#is-ownership-deceased-immovable').is(':checked') ? 'True' : 'False';
        photo = $('#photo-path-immovable-image-preview').attr('src');
        photoPathImmovable = $('#photo-path-immovable').get(0);
        dochtml = '<input type="file", id="' + newID + '", class="new-record", name = "DocPath", disabled="true"/>';
        dochtml1 = '<img src="' + photo + '", width="50", height="50", id= "' + photoID + '" />';
        fileCaption = $('#file-caption-immovable').val();
        assetFullDescription = $('#asset-full-description').val();
        note = $('#note-immovable-asset').val();
        reasonForModification = $('#reason-for-modification-immovable-asset').val();
        rvisibility = 0;
        hasDivClass = $('#income-tax-div').hasClass('d-none');

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        //if (fileCaption == '')
        //    fileCaption = 'None';

        if (assetFullDescription === '')
            assetFullDescription = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }
        prmKey = 0;

        //if (($('#immovable-asset-document-upload').val()) == 'M') {
        //    
        //    path = $('#photo-path-immovable').val();
        //} else {
        //    path = 'None';
        //}

        let docImmovableUpload = $('#immovable-asset-document-upload').val();

        if (docImmovableUpload === 'M') {
            path = $('#photo-path-immovable').val();
            fileCaption;

        } else if (docImmovableUpload === 'O') {
            if (photo === '' && fileCaption === '') {
                path = 'None';
                fileCaption = 'None';
            }
            else if (photo === '') {
                path = 'None';
                fileCaption;
            }
            else if (fileCaption === '') {
                path = $('#photo-path-immovable').val();
                fileCaption = 'None';
            }
            else if (photo !== '' && fileCaption !== '') {
                path = $('#photo-path-immovable').val();
                fileCaption;
            }
        }
        else if (docImmovableUpload === 'D') {
            if (photo === '') {
                path = 'None';
                fileCaption = 'None';
            }
        }


        if (surveyNumber === '' || surveyNumber.length < 3 || surveyNumber.length > 50) {
            result = false;
            $('#survey-numbers-error').removeClass('d-none');
        }
        else
            $('#survey-numbers-error').addClass('d-none');

        if (citySurveyNumber === '' || citySurveyNumber.length < 3 || citySurveyNumber.length > 50) {
            result = false;
            $('#city-survey-numbers-error').removeClass('d-none');
        }
        else
            $('#city-survey-numbers-error').addClass('d-none');

        if (number === '' || number.length < 3 || number.length > 50) {
            result = false;
            $('#number-error').removeClass('d-none');
        }
        else
            $('#number-error').addClass('d-none');

        if (areaOfLand === '' || areaOfLand < 0 || areaOfLand > 9999) {
            result = false;
            $('#area-of-land-immovable-error').removeClass('d-none');
        }
        else
            $('#area-of-land-immovable-error').addClass('d-none');

        if (constructionArea === '' || constructionArea < 50 || constructionArea > 999999) {
            result = false;
            $('#construction-area-error').removeClass('d-none');
        }
        else
            $('#construction-area-error').addClass('d-none');

        if (carpetArea === '' || carpetArea < 20 || carpetArea > constructionArea - 1) {
            result = false;
            $('#carpet-area-error').removeClass('d-none');
        }
        else
            $('#carpet-area-error').addClass('d-none');


        if (currentMarketValue === '' || currentMarketValue < 1 || currentMarketValue > 999999999) {
            result = false;
            $('#current-market-value-immovable-error').removeClass('d-none');
        }
        else
            $('#current-market-value-immovable-error').addClass('d-none');

        if (annualRentIncome === '' || annualRentIncome < 0 || annualRentIncome > 9999999) {
            result = false;
            $('#annual-rent-income-error').removeClass('d-none');
        }
        else
            $('#annual-rent-income-error').addClass('d-none');

        if (residenceType === '') {
            result = false;
            $('#residence-types-id-error').removeClass('d-none');
        }
        else
            $('#residence-types-id-error').addClass('d-none');

        if (ownershipType === '') {
            result = false;
            $('#ownership-types-id-error').removeClass('d-none');
        }
        else
            $('#ownership-types-id-error').addClass('d-none');

        if (ownershipPercentage === '' || ownershipPercentage < 0 || ownershipPercentage > 100) {
            result = false;
            $('#ownership-percentage-immovable-asset-error').removeClass('d-none');
        }
        else
            $('#ownership-percentage-immovable-asset-error').addClass('d-none');

        if (path === '') {
            result = false;
            $('#photo-path-immovable-error').removeClass('d-none');
        }
        else
            $('#photo-path-immovable-error').addClass('d-none');

        if (fileCaption === '') {
            result = false;
            $('#file-caption-immovable-error').removeClass('d-none');
        }
        else
            $('#file-caption-immovable-error').addClass('d-none');

        return result;
    }

    function HideColumnsImmovableDataTable() {
        immovableDataTable.column(9).visible(false);
        immovableDataTable.column(11).visible(false);
        immovableDataTable.column(22).visible(false);
        immovableDataTable.column(23).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Agriculture Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('agriculture-asset');

    // DataTable Add Button 
    $('#btn-add-agriculture-asset-dt').click(function () {
        
        event.preventDefault();

        $('#any-court-case-block').addClass('d-none');

         SetModalTitle('agriculture-asset', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-agriculture-asset-dt').click(function () {
        
        SetModalTitle('agriculture-asset', 'Edit');

        if (hasAnyCourtCase === true) {
            $('#any-court-case-block').removeClass('d-none');
        } else {
            $('#any-court-case-block').addClass('d-none');
        }

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-agriculture-asset-dt').data('rowindex');
            id = $('#agriculture-asset-modal').attr('id');
            myModal = $('#' + id).modal();

            $('#agriculture-land-type-id', myModal).val(columnValues[1]);
            $('#agriculture-land-description', myModal).val(columnValues[3]);
            $('#survey-number', myModal).val(columnValues[4]);
            $('#group-number', myModal).val(columnValues[5]);
            $('#start-area-of-land', myModal).val(columnValues[6]);
            $('#volume', myModal).val(columnValues[7]);
            $('#ownership-type-id', myModal).val(columnValues[8]);
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

            // Assuming columnValues is an array containing DOM elements or jQuery objects
            storagePathInput = columnValues[21];

            // Retrieve the ID of the file input element
            storagePathId = $(storagePathInput).attr('id');

            // Get the DOM element of the file input using its ID
            docPath = $('#' + storagePathId).get(0);

            // Retrieve the files from the file input
            files = docPath.files;

            // If there are files selected
            if (files.length !== 0) {
                // Create a new DataTransfer object
                dt = new DataTransfer();
                // Iterate through each file
                for (let j = 0; j < files.length; j++) {
                    let f = files[j];
                    dt.items.add(new File([f.slice(0, f.size, f.type)], f.name));
                }
            }


            photoPathAgree = $('#photo-path-agree').get(0);
            photoPathAgree.files = dt.files;

            photoinput = columnValues[22];
            photoPathAgreeId = $(photoinput).attr('id');
            photoSrc = $('#' + photoPathAgreeId).attr('src')
            $('#photo-path-agree-image-preview').attr('src', photoSrc);

            $('#file-caption', myModal).val(columnValues[23]);
            $('#note-agriculture-asset', myModal).val(columnValues[24]);
            $('#reason-for-modification-agriculture-asset', myModal).val(columnValues[25]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-agriculture-asset-edit-dt').addClass('read-only');
            $('#agriculture-asset-modal').modal('hide');
        }
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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        reasonForModification,
                        prmKey
            ]).draw();

            files = photoPathAgree.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(new File([f.slice(0, f.size, f.type)], f.name));
                }
            }

            docPath = $('#' + newID).get(0);
            if (photoPathAgree.files.length !== 0) {
                docPath.files = dt.files;
            }


            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);
            $('#agriculture-asset-data-table-error').addClass('d-none');
            HideColumnsAgricultureDataTable();

            agricultureDataTable.columns.adjust().draw();

            ClearModal('agriculture-asset');

            $('#agriculture-asset-modal').modal('hide');

            EnableNewOperation('agriculture-asset');
        }
    });

    // Modal update Button Event
    $('#btn-update-agriculture-asset-modal').click(function (event) {

        oldId = "photoPathAgree" + (i - 1);
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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        reasonForModification,
                        prmKey
            ]).draw();

            // Get the files selected in the photoPathAgree file input
            files = photoPathAgree.files;

            // Check if there are files selected
            if (files.length !== 0) {
                dt = new DataTransfer();

                for (j = 0; j < files.length; j++) {
                    // Get the current file
                    f = files[j];
                    slicedFile = new File([f.slice(0, f.size, f.type)], f.name);
                    // Add the sliced file to the DataTransfer object
                    dt.items.add(slicedFile);
                }
            }
            docPath = $('#' + newID).get(0);

            // Check if there are files selected in photoPathAgree
            if (photoPathAgree.files.length !== 0) {
                docPath.files = dt.files;
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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-agriculture-asset tbody input[type="checkbox"]:checked').each(function () {
                 agricultureDataTable.row($('#tbl-agriculture-asset tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-agriculture-asset-dt').data('rowindex');
                  EnableNewOperation('agriculture-asset');

                  $('#select-all-agriculture-asset').prop('checked', false);
                    if (!agricultureDataTable.data().any())
                    $('#agriculture-asset-data-table-error').removeClass('d-none');

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
        i = counter;
        newID = "photoPathAgree" + i;
        photoID = "PhotoId" + i;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        agricultureLandTypeId = $('#agriculture-land-type-id option:selected').val();
        agricultureLandTypeText = $('#agriculture-land-type-id option:selected').text();
        agricultureLandDescription = $('#agriculture-land-description').val();
        surveyNumber = $('#survey-number').val();
        groupNumber = $('#group-number').val();
        areaOfLand = $('#start-area-of-land').val();
        volume = $('#volume').val();
        ownershipTypeId = $('#ownership-type-id option:selected').val();
        ownershipTypeText = $('#ownership-type-id option:selected').text();
        ownershipPercentage = $('#ownership-percentage-agriculture-asset').val();
        currentMarketValue = $('#end-current-market-value').val();
        annualIncomeFromLand = $('#annual-income-from-land').val();
        hasAnyCourtCase = $('#enable-any-court-case').is(':checked') ? true : false;
        courtCaseFullDetails = $('#court-case-full-details').val();
        isOnlyRainFedTypeIrrigation = $('#is-only-rain-fed-type-irrigation').is(':checked') ? true : false;
        hasCanalRiverIrrigationSource = $('#has-canal-river-irrigation-source').is(':checked') ? true : false;
        hasWellsIrrigationSource = $('#has-wells-irrigation-source').is(':checked') ? true : false;
        hasFarmLakeSource = $('#has-farm-lake-source').is(':checked') ? true : false;
        hasAnyMortgage = $('#has-any-mortgage').is(':checked') ? true : false;
        isOwnershipDeceased = $('#is-ownership-deceased').is(':checked') ? true : false;
        photo = $('#photo-path-agree-image-preview').attr('src');
        photoPathAgree = $('#photo-path-agree').get(0);
        dochtml = '<input type="file", id="' + newID + '", class="new-record", name = "DocPath", disabled="true"/>'
        dochtml1 = '<img src="' + photo + '", width="50", height="50", id= "' + photoID + '" />'
        fileCaption = $("#file-caption").val();
        note = $('#note-agriculture-asset').val();
        reasonForModification = $('#reason-for-modification-agriculture-asset').val();
        prmKey = 0;


        if (($('#agriculture-asset-document-upload').val()) === 'M') {
            path = $('#photo-path-agree').val();
        } else {
            path = 'None';
        }

        rvisibility = 0;
        hasDivClass = $('#agriculture-div').hasClass('d-none');

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (fileCaption === '')
            fileCaption = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }

        if (agricultureLandTypeId === '') {
            result = false;
            $('#agriculture-land-type-id-error').removeClass('d-none');
        }
        else
            $('#agriculture-land-type-id-error').addClass('d-none');

        if (agricultureLandDescription === '' || agricultureLandDescription.length < 3 || agricultureLandDescription.length > 1500) {
            result = false;
            $('#agriculture-land-description-error').removeClass('d-none');
        } else
            $('#agriculture-land-description-error').addClass('d-none');

        if (surveyNumber === '' || surveyNumber.length < 3 || surveyNumber.length > 50) {
            result = false;
            $('#survey-number-error').removeClass('d-none');
        } else
            $('#survey-number-error').addClass('d-none');

        if (groupNumber === '' || groupNumber.length < 3 || groupNumber.length > 50) {
            result = false;
            $('#group-number-error').removeClass('d-none');
        }
        else
            $('#group-number-error').addClass('d-none');



        if (areaOfLand === '' || areaOfLand < 0 || areaOfLand > 9999) {
            result = false;
            $('#start-area-of-land-error').removeClass('d-none');
        }
        else
            $('#start-area-of-land-error').addClass('d-none');

        if (volume === '' || volume < 0 || volume > 9999) {
            result = false;
            $('#volume-error').removeClass('d-none');
        }
        else
            $('#volume-error').addClass('d-none');

        if (ownershipTypeId === '') {
            result = false;
            $('#ownership-type-id-error').removeClass('d-none');
        }
        else
            $('#ownership-type-id-error').addClass('d-none');

        if (ownershipPercentage === '' || ownershipPercentage < 0 || ownershipPercentage > 100) {
            result = false;
            $('#ownership-percentage-agriculture-asset-error').removeClass('d-none');
        } else
            $('#ownership-percentage-agriculture-asset-error').addClass('d-none');

        if (currentMarketValue === '' || currentMarketValue < 1 || currentMarketValue > 999999999) {
            result = false;
            $('#end-current-market-value-error').removeClass('d-none');
        }
        else
            $('#end-current-market-value-error').addClass('d-none');

        if (annualIncomeFromLand === '' || annualIncomeFromLand < 0 || annualIncomeFromLand > 999999999) {
            result = false;
            $('#annual-income-from-land-error').removeClass('d-none');
        }
        else
            $('#annual-income-from-land-error').addClass('d-none');


        if (hasAnyCourtCase === true) {
            if (courtCaseFullDetails === '' || courtCaseFullDetails.length > 2500) {
                result = false;
                $('#court-case-full-details-error').removeClass('d-none');
            } else {
                $('#court-case-full-details-error').addClass('d-none');
            }
        } else if (hasAnyCourtCase === false) { // Correct syntax for else if
            courtCaseFullDetails = 'None';
        }

        //if (path == '') {
        //    $('#photo-path-agree-error').removeClass('d-none');
        //}
        //else
        //    $('#photo-path-agree-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsAgricultureDataTable() {
        agricultureDataTable.column(1).visible(false);
        agricultureDataTable.column(8).visible(false);
        agricultureDataTable.column(25).visible(false);
        agricultureDataTable.column(26).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Machinery Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('machinery-asset');

    // DataTable Add Button 
    $('#btn-add-machinery-asset-dt').click(function () {
        //Clear Purchase Dates //Modify By -- Sagar Kare
        $('#date-of-purchase-machinery-asset').val('').removeAttr('min max').blur();

        event.preventDefault();

        if (($('#machinery-asset-document-upload').val()) === 'M') {
            $('#photo-path-machinery').addClass('mandatory-mark');
            $('#file-caption-machinery').addClass('mandatory-mark');
        } else {
            $('#photo-path-machinery').removeClass('mandatory-mark');
            $('#file-caption-machinery').removeClass('mandatory-mark');
        }

        SetModalTitle('machinery-asset', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-machinery-asset-dt').click(function () {
        
        SetModalTitle('machinery-asset', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-machinery-asset-dt').data('rowindex');
            id = $('#machinery-asset-modal').attr('id');
            myModal = $('#' + id).modal();

            machineryDateOfPurchase = new Date(columnValues[4]);

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

            storagePathInput = columnValues[12];

            storagePathId = $(storagePathInput).attr('id');
            docPath = $('#' + storagePathId).get(0);
            files = docPath.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            photoPathMachinery = $('#photo-path-machinery').get(0);
            photoPathMachinery.files = dt.files;

            photoinput = columnValues[13];
            photoPathMachineryId = $(photoinput).attr('id');
            photoSrc = $('#' + photoPathMachineryId).attr('src')
            $('#photo-path-machinery-image-preview').attr('src', photoSrc);

            $('#file-caption-machinery', myModal).val(columnValues[14]);
            $('#note-machinery-asset', myModal).val(columnValues[15]);
            $('#reason-for-modification-machinery-asset', myModal).val(columnValues[16]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-machinery-asset-edit-dt').addClass('read-only');
            $('#machinery-asset-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-machinery-asset-modal').click(function (event) {
        if (IsValidMachineryModal()) {
            
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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        reasonForModification,
                        PrmKey
            ]).draw();

            files = photoPathMachinery.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);
            if (photoPathMachinery.files.length !== 0) {
                docPath.files = dt.files;
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);
            $('#machinery-asset-data-table-error').addClass('d-none');

            HideColumnsMachineryDataTable();

            machineryDataTable.columns.adjust().draw();

            ClearModal('machinery-asset');

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
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
                        reasonForModification,
                        PrmKey
            ]).draw();

            files = photoPathMachinery.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);

            if (photoPathMachinery.files.length !== 0) {
                docPath.files = dt.files;
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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-machinery-asset tbody input[type="checkbox"]:checked').each(function () {
                 machineryDataTable.row($('#tbl-machinery-asset tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-machinery-asset-dt').data('rowindex');
                  EnableNewOperation('machinery-asset');

                  $('#select-all-machinery-asset').prop('checked', false);
                   if (!machineryDataTable.data().any())
                    $('#machinery-asset-data-table-error').removeClass('d-none');

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
        
        result = true;
        counter++;
        i = counter;
        newID = 'photoPathMachinery' + i;
        photoID = 'PhotoId' + i;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        nameOfMachinery = $('#name-of-machinery').val();
        machineryFullDetails = $('#machinery-full-details').val();
        manufacturingYear = $('#manufacturing-year-machinery-asset').val();
        dateOfPurchase = $('#date-of-purchase-machinery-asset').val();
        numberOfOwners = $('#number-of-owners-machinery-asset').val();
        referenceNumber = $('#reference-number-machinery-asset').val();
        purchasePrice = $('#purchase-price-machinery-asset').val();
        currentMarketValue = $('#current-market-value-machinery-asset').val();
        ownershipPercentage = $('#ownership-percentage-machinery-asset').val();
        hasAnyMortgage = $('input[name="PersonMachineryAssetViewModel.HasAnyMortgage"]').is(':checked') ? true : false;
        isOwnershipDeceased = $('input[name="PersonMachineryAssetViewModel.IsOwnershipDeceased"]').is(':checked') ? true : false;
        photo = $('#photo-path-machinery-image-preview').attr('src');
        photoPathMachinery = $('#photo-path-machinery').get(0);
        dochtml = '<input type="file", id="' + newID + '", class="new-record", name = "DocPath", disabled="true"/>';
        dochtml1 = '<img src="' + photo + '", width="50", height="50", id= "' + photoID + '" />';
        fileCaption = $('#file-caption-machinery').val();
        note = $('#note-machinery-asset').val();
        reasonForModification = $('#reason-for-modification-machinery-asset').val();
        rvisibility = 0;
        hasDivClass = $('#machinery-div').hasClass('d-none');

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        //if (fileCaption == '')
        //    fileCaption = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }

        PrmKey = 0;

        //if (($('#machinery-asset-document-upload').val()) == 'M') {
        //    path = $('#photo-path-machinery').val();
        //} else {
        //    path = 'None';
        //}

        let docMachineryUpload = $('#machinery-asset-document-upload').val();

        if (docMachineryUpload === 'M') {
            path = $('#photo-path-machinery').val();
            fileCaption;

        } else if (docMachineryUpload === 'O') {
            if (photo === '' && fileCaption === '') {
                path = 'None';
                fileCaption = 'None';
            }
            else if (photo === '') {
                path = 'None';
                fileCaption;
            }
            else if (fileCaption === '') {
                path = $('#photo-path-machinery').val();
                fileCaption = 'None';
            }
            else if (photo !== '' && fileCaption !== '') {
                path = $('#photo-path-machinery').val();
                fileCaption;
            }
        }
        else if (docMachineryUpload === 'D') {
            if (photo === '') {
                path = 'None';
                fileCaption = 'None';
            }
        }


        if (nameOfMachinery === '' || nameOfMachinery.length < 3 || nameOfMachinery.length > 500) {
            result = false;
            $('#name-of-machinery-error').removeClass('d-none');
        } else
            $('#name-of-machinery-error').addClass('d-none');


        if (machineryFullDetails === '' || machineryFullDetails.length > 1500) {
            result = false;
            $('#machinery-full-details-error').removeClass('d-none');
        }
        else
            $('#machinery-full-details-error').addClass('d-none');

        // Get the current year
        let currentYear = new Date().getFullYear();

        // Calculate the minimum allowed year
        let minAllowedYear = currentYear - 50;

        // Generate allowed years list dynamically
        let allowedYears = [];
        for (let i = minAllowedYear; i <= currentYear; i++) {
            allowedYears.push(i.toString());
        }

        // Assuming assessmentYear is a string letiable containing the year
        if (manufacturingYear === '' || !allowedYears.includes(manufacturingYear)) {
            result = false;
            $('#manufacturing-year-machinery-asset-error').removeClass('d-none');
        } else
            $('#manufacturing-year-machinery-asset-error').addClass('d-none');

        let isValidDateOfPurchase = IsValidInputDate('#date-of-purchase-machinery-asset');

        if (!isValidDateOfPurchase) {
            result = false;
            $('#date-of-purchase-machinery-asset-error').removeClass('d-none');
        }
        else
            $('#date-of-purchase-machinery-asset-error').addClass('d-none');

        if (numberOfOwners === '' || parseInt(numberOfOwners) < 0 || parseInt(numberOfOwners) > 19) {
            result = false;
            $('#number-of-owners-machinery-asset-error').removeClass('d-none');
        }
        else
            $('#number-of-owners-machinery-asset-error').addClass('d-none');

        if (referenceNumber === '' || referenceNumber.length > 50) {
            result = false;
            $('#reference-number-machinery-asset-error').removeClass('d-none');
        }
        else
            $('#reference-number-machinery-asset-error').addClass('d-none');


        if (purchasePrice === '' || purchasePrice < 1 || purchasePrice > 999999999) {
            result = false;
            $('#purchase-price-machinery-asset-error').removeClass('d-none');
        } else
            $('#purchase-price-machinery-asset-error').addClass('d-none');

        if (currentMarketValue === '' || currentMarketValue < 1 || currentMarketValue > purchasePrice) {
            result = false;
            $('#current-market-value-machinery-asset-error').removeClass('d-none');
        }
        else
            $('#current-market-value-machinery-asset-error').addClass('d-none');

        if (ownershipPercentage === '' || ownershipPercentage < 0 || ownershipPercentage > 100) {
            result = false;
            $('#ownership-percentage-machinery-asset-error').removeClass('d-none');
        }
        else
            $('#ownership-percentage-machinery-asset-error').addClass('d-none');

        if (path === '') {
            result = false;
            $('#photo-path-machinery-error').removeClass('d-none');
        }
        else
            $('#photo-path-machinery-error').addClass('d-none');

        if (fileCaption === '') {
            result = false;
            $('#file-caption-machinery-error').removeClass('d-none');
        }
        else
            $('#file-caption-machinery-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsMachineryDataTable() {
        machineryDataTable.column(16).visible(false);
        machineryDataTable.column(17).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Additional Income Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('income-detail')

    // DataTable Add Button 
    $('#btn-add-income-detail-dt').click(function () {
        event.preventDefault();

        SetModalTitle('income-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-income-detail-dt').click(function () {
        SetModalTitle('income-detail', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-income-detail-dt').data('rowindex');
            id = $('#income-detail-modal').attr('id');
            myModal = $('#' + id).modal();


            $('#income-source-id', myModal).val(columnValues[1]);
            $('#annual-incomes', myModal).val(columnValues[3]);
            $('#other-details', myModal).val(columnValues[4]);
            $('#note-income-detail', myModal).val(columnValues[5]);
            $('#reason-for-modification-income-detail', myModal).val(columnValues[6]);

            // Show Modals
            myModal.modal({ show: true });
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

            ClearModal('income-detail');

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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-income-detail tbody input[type="checkbox"]:checked').each(function () {
                    incomeDatatable.row($('#tbl-income-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-income-detail-dt').data('rowindex');
                    EnableNewOperation('income-detail');

                    $('#select-all-income-detail').prop('checked', false);
                    if (!incomeDatatable.data().any())
                    $('#income-details-data-table-error').removeClass('d-none');

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
        
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        incomeSource = $('#income-source-id option:selected').val();
        incomeSourceText = $('#income-source-id option:selected').text();
        annualIncome = $('#annual-incomes').val();
        otherDetails = $('#other-details').val();
        note = $('#note-income-detail').val();
        reasonForModification = $('#reason-for-modification-income-detail').val();

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification == '')
                reasonForModification = 'None';
        }

        if (incomeSource === '') {
            result = false;
            $('#income-source-id-error').removeClass('d-none');
        }
        else
            $('#income-source-id-error').addClass('d-none');

        if (annualIncome === '' || parseFloat(annualIncome) < 0 || parseFloat(annualIncome) > 999999999) {
            result = false;
            $('#annual-incomes-error').removeClass('d-none');
        } else
            $('#annual-incomes-error').addClass('d-none');

        if (otherDetails === '' || otherDetails.length > 500) {
            result = false;
            $('#other-details-error').removeClass('d-none');
        }
        else
            $('#other-details-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsIncomeDetailDatatable() {
        incomeDatatable.column(1).visible(false);
        incomeDatatable.column(6).visible(false);

    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Borrowing Details - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('borrowing-detail');

    // DataTable Add Button 
    $('#btn-add-borrowing-detail-dt').click(function () {
        
        //Clear Registration Dates //Modify By -- Sagar Kare
        $('#registration-date').val('').removeAttr('min max').blur();
        $('#taking-any-court-action-block').addClass('d-none');
        event.preventDefault();

        SetModalTitle('borrowing-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-borrowing-detail-dt').click(function () {
        
        SetModalTitle('borrowing-detail', 'Edit');

        if (isTakingAnyCourtAction === true) {
            $('#taking-any-court-action-block').removeClass('d-none');
        } else {
            $('#taking-any-court-action-block').addClass('d-none');
        }

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-borrowing-detail-dt').data('rowindex');
            id = $('#borrowing-detail-modal').attr('id');
            myModal = $('#' + id).modal();

            openingDate = new Date(columnValues[6]);
            matureDate = new Date(columnValues[7]);
            closeDate = new Date(columnValues[8]);
            filingDate = new Date(columnValues[22]);
            registrationDate = new Date(columnValues[24]);


            $('#name-of-organization', myModal).val(columnValues[1]);
            $('#trans-name-of-organization', myModal).val(columnValues[2]);
            $('#branch', myModal).val(columnValues[3]);
            $('#trans-branch', myModal).val(columnValues[4]);
            $('#reference-number', myModal).val(columnValues[5]);
            $('#open-date', myModal).val(GetInputDateFormat(openingDate));
            $('#mature-date', myModal).val(GetInputDateFormat(matureDate));
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
            $('#filing-date', myModal).val(GetInputDateFormat(filingDate));
            $('#filing-number', myModal).val(columnValues[23]);
            $('#registration-date', myModal).val(GetInputDateFormat(registrationDate));
            $('#registration-number', myModal).val(columnValues[25]);
            $('#cnr-number', myModal).val(columnValues[26]);
            $('#court-case-stage-id', myModal).val(columnValues[27]);

            $('#note-borrowing-detail', myModal).val(columnValues[29]);
            $('#trans-note-borrowing-detail', myModal).val(columnValues[30]);
            $('#reason-for-modification-borrowing-detail', myModal).val(columnValues[31]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-borrowing-detail-edit-dt').addClass('read-only');
            $('#borrowing-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-borrowing-detail-modal').click(function (event) {
        
        if (IsValidBorrowingModal()) {
            row = borrowingDataTable.row.add([
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

            HideColumnsBorrowingDataTable();

            borrowingDataTable.columns.adjust().draw();

            ClearModal('borrowing-detail');

            $('#borrowing-detail-modal').modal('hide');

            EnableNewOperation('borrowing-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-borrowing-detail-modal').click(function (event) {
        $('#select-all-borrowing-detail').prop('checked', false);
        if (IsValidBorrowingModal()) {
            borrowingDataTable.row(selectedRowIndex).data([
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

            HideColumnsBorrowingDataTable();

            borrowingDataTable.columns.adjust().draw();

            $('#borrowing-detail-modal').modal('hide');

            EnableNewOperation('borrowing-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-borrowing-detail-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-borrowing-detail tbody input[type="checkbox"]:checked').each(function () {
                 borrowingDataTable.row($("#tbl-borrowing-detail tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-borrowing-detail-dt').data('rowindex');
                  EnableNewOperation('borrowing-detail');

                  $('#select-all-borrowing-detail').prop('checked', false);
                    if (!borrowingDataTable.data().any())
                    $('#borrowing-detail-data-table-error').removeClass('d-none');

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

                selectedRowIndex = borrowingDataTable.row(row).index();

                rowData = (borrowingDataTable.row(selectedRowIndex).data());

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
                selectedRowIndex = borrowingDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (borrowingDataTable.row(selectedRowIndex).data());

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
        if (checked.length === 0)
            EnableNewOperation('borrowing-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
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

    // Validate Fund Module
    function IsValidBorrowingModal() {
        
        result = true;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        nameOfOrganization = $('#name-of-organization').val();
        transNameOfOrganization = $('#trans-name-of-organization').val();
        branch = $('#branch').val();
        transBranch = $('#trans-branch').val();
        referenceNumber = $('#reference-number').val();
        openingDate = $('#open-date').val();
        matureDate = $('#mature-date').val();
        closeDate = $('#close-date-borrowing').val();
        loanDetails = $('#loan-details').val();
        transLoanDetails = $('#trans-loan-details').val();
        mortgageDetails = $('#mortgage-details').val();
        transMortgageDetails = $('#trans-mortgage-details').val();
        mortgageAmount = $('#mortgage-amount').val();
        sanctionLoanAmount = $('#sanction-loan-amount').val();
        installmentAmount = $('#installment-amount').val();
        loanBalanceAmount = $('#loan-balance-amount').val();
        overduesInstallment = $('#overdues-installment').val();
        overduesAmount = $('#overdues-amount').val();
        isTakingAnyCourtAction = $('#enable-taking-any-court-action').is(':checked');
        
        if (isTakingAnyCourtAction) {
            courtCaseType = $('#court-case-type-id option:selected').val();
            courtCaseTypeText = $('#court-case-type-id option:selected').text();
            registrationDate = $('#registration-date').val();
            filingDate = $('#filing-date').val();
            courtCaseStage = $('#court-case-stage-id option:selected').val();
            courtCaseStageText = $('#court-case-stage-id option:selected').text();
            filingNumber = $('#filing-number').val();
            registrationNumber = $('#registration-number').val();
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

        note = $('#note-borrowing-detail').val();
        transNote = $('#trans-note-borrowing-detail').val();
        reasonForModification = $('#reason-for-modification-borrowing-detail').val();
        rvisibility = 0;
        hasDivClass = $('#borrowing-div').hasClass('d-none');

        //Set Default Value if Empty
        if (note === '' || transNote === '') {
            note = 'None';
            transNote = 'None';
        }

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }
        if (nameOfOrganization === '' || nameOfOrganization.length < 3 || nameOfOrganization.length > 150) {
            result = false;
            $('#name-of-organization-error').removeClass('d-none');
        } else
            $('#name-of-organization-error').addClass('d-none');

        if (transNameOfOrganization === '' || transNameOfOrganization.length > 150) {
            result = false;
            $('#trans-name-of-organization-error').removeClass('d-none');
        } else
            $('#trans-name-of-organization-error').addClass('d-none');


        if (branch === '' || branch.length < 3 || branch.length > 50) {
            result = false;
            $('#branch-error').removeClass('d-none');
        } else
            $('#branch-error').addClass('d-none');

        if (transBranch === '' || transBranch.length > 50) {
            result = false;
            $('#trans-branch-error').removeClass('d-none');
        }
        else
            $('#trans-branch-error').addClass('d-none');


        if (referenceNumber === '' || referenceNumber.length < 3 || referenceNumber.length > 50) {
            result = false;
            $('#reference-number-error').removeClass('d-none');
        } else
            $('#reference-number-error').addClass('d-none');

        let isValidOpeningDate = IsValidInputDate('#open-date');

        if (!isValidOpeningDate) {
            result = false;
            $('#open-date-error').removeClass('d-none');
        }
        else
            $('#open-date-error').addClass('d-none');

        let isValidMatureDate = IsValidInputDate('#mature-date');

        if (!isValidMatureDate) {
            result = false;
            $('#mature-date-error').removeClass('d-none');
        }
        else
            $('#mature-date-error').addClass('d-none');


        if (loanDetails === '' || loanDetails.length < 3 || loanDetails.length > 1500) {
            result = false;
            $('#loan-details-error').removeClass('d-none');
        } else
            $('#loan-details-error').addClass('d-none');

        if (transLoanDetails === '' || transLoanDetails.length > 1500) {
            result = false;
            $('#trans-loan-details-error').removeClass('d-none');
        }
        else
            $('#trans-loan-details-error').addClass('d-none');


        if (mortgageDetails === '' || mortgageDetails.length < 3 || mortgageDetails.length > 1500) {
            result = false;
            $('#mortgage-details-error').removeClass('d-none');
        } else
            $('#mortgage-details-error').addClass('d-none');

        if (transMortgageDetails === '' || transMortgageDetails.length > 1500) {
            result = false;
            $('#trans-mortgage-details-error').removeClass('d-none');
        }
        else
            $('#trans-mortgage-details-error').addClass('d-none');



        // Validate CollectionAmount
        if (mortgageAmount === '' || parseFloat(mortgageAmount) < 1000 || parseFloat(mortgageAmount) > 999999999) {
            result = false;
            $('#mortgage-amount-error').removeClass('d-none');
        } else
            $('#mortgage-amount-error').addClass('d-none');

        if (sanctionLoanAmount === '' || parseFloat(sanctionLoanAmount) < 1000 || parseFloat(sanctionLoanAmount) > mortgageAmount) {
            result = false;
            $('#sanction-loan-amount-error').removeClass('d-none');
        } else
            $('#sanction-loan-amount-error').addClass('d-none');



        if (installmentAmount === '' || parseFloat(installmentAmount) < 0 || parseFloat(installmentAmount) > sanctionLoanAmount) {
            result = false;
            $('#installment-amount-error').removeClass('d-none');
        }
        else
            $('#installment-amount-error').addClass('d-none');


        if (loanBalanceAmount === '' || parseFloat(loanBalanceAmount) < 1 || parseFloat(loanBalanceAmount) > 999999999) {
            result = false;
            $('#loan-balance-amount-error').removeClass('d-none');
        } else
            $('#loan-balance-amount-error').addClass('d-none');

        if (overduesInstallment === '' || parseInt(overduesInstallment) < 1 || parseInt(overduesInstallment) > 199) {
            result = false;
            $('#overdues-installment-error').removeClass('d-none');
        }
        else
            $('#overdues-installment-error').addClass('d-none');




        if (overduesAmount === '' || isNaN(overduesAmount) || overduesAmount < 1 || overduesAmount > 999999999) {
            result = false;
            $('#overdues-amount-error').removeClass('d-none');
        } else
            $('#overdues-amount-error').addClass('d-none');


        if (isTakingAnyCourtAction) {
            // If taking court action, perform validation checks
            result = true;

            if (courtCaseType === '') {
                result = false;
                $('#court-case-type-id-error').removeClass('d-none');
            } else {
                $('#court-case-type-id-error').addClass('d-none');
            }

            let isValidRegistrationDate = IsValidInputDate('#registration-date');

            if (!isValidRegistrationDate) {
                result = false;
                $('#registration-date-error').removeClass('d-none');
            } else {
                $('#registration-date-error').addClass('d-none');
            }

            let isValidFilingDate = IsValidInputDate('#filing-date');

            if (!isValidFilingDate) {
                result = false;
                $('#filing-date-error').removeClass('d-none');
            } else {
                $('#filing-date-error').addClass('d-none');
            }

            if (courtCaseStage === '' || courtCaseStageText === '') {
                result = false;
                $('#court-case-stage-id-error').removeClass('d-none');
            } else {
                $('#court-case-stage-id-error').addClass('d-none');
            }

            if (filingNumber === '' || filingNumber.length < 3 || filingNumber.length > 50) {
                result = false;
                $('#filing-number-error').removeClass('d-none');
            } else {
                $('#filing-number-error').addClass('d-none');
            }

            if (registrationNumber === '' || registrationNumber.length < 3 || registrationNumber.length > 50) {
                result = false;
                $('#registration-number-error').removeClass('d-none');
            } else {
                $('#registration-number-error').addClass('d-none');
            }

            if (cNRNumber === '' || cNRNumber.length < 3 || cNRNumber.length > 50) {
                result = false;
                $('#cnr-number-error').removeClass('d-none');
            } else {
                $('#cnr-number-error').addClass('d-none');
            }
        } else {
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
    function HideColumnsBorrowingDataTable() {
        borrowingDataTable.column(8).visible(false);
        borrowingDataTable.column(20).visible(false);
        borrowingDataTable.column(27).visible(false);
        borrowingDataTable.column(31).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Credit Ratings - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('credit-rating');

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
            columnValues = $('#btn-edit-credit-rating-dt').data('rowindex');
            id = $('#credit-rating-modal').attr('id');
            myModal = $('#' + id).modal();

            creditRatingEffectiveDate = new Date(columnValues[1]);

            $('#effective-date', myModal).val(GetInputDateFormat(creditRatingEffectiveDate));
            $('#credit-bureau-agency-id', myModal).val(columnValues[2]);
            $('#score', myModal).val(columnValues[4]);
            $('#note-credit-rating', myModal).val(columnValues[5]);
            $('#reason-for-modification-credit-rating', myModal).val(columnValues[6]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-credit-rating-edit-dt').addClass('read-only');
            $('#credit-rating-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-credit-rating-modal').click(function (event) {
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

            ClearModal('credit-rating');

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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-credit-rating tbody input[type="checkbox"]:checked').each(function () {
                    creditDataTable.row($('#tbl-credit-rating tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-credit-rating-dt').data('rowindex');
                    EnableNewOperation('credit-rating');

                    $('#select-all-credit-rating').prop('checked', false);
                     if (!creditDataTable.data().any())
                    $('#credit-rating-data-table-error').removeClass('d-none');

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
                EnableDeleteOperation('credit-rating')
            });
        }
        else {
            EnableNewOperation('credit-rating')

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
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        effectiveDate = $('#effective-date').val();
        agency = $('#credit-bureau-agency-id option:selected').val();
        agencyText = $('#credit-bureau-agency-id option:selected').text();
        score = $('#score').val();
        note = $('#note-credit-rating').val();
        reasonForModification = $('#reason-for-modification-credit-rating').val();
        rvisibility = 0;
        hasDivClass = $('#credit-div').hasClass('d-none');
        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }
        if (agency === '') {
            result = false;
            $('#credit-bureau-agency-id-error').removeClass('d-none');
        }
        else
            $('#credit-bureau-agency-id-error').addClass('d-none');

        if (score === '' || isNaN(score) || score < 0 || score > 900) {
            result = false;
            $('#score-error').removeClass('d-none');
        } else
            $('#score-error').addClass('d-none');

        let isValidEffectiveDate = IsValidInputDate('#effective-date');

        if (!isValidEffectiveDate) {
            result = false;
            $('#effective-date-error').removeClass('d-none');
        }
        else
            $('#effective-date-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsCreditDataTable() {
        creditDataTable.column(2).visible(false);
        creditDataTable.column(6).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Person Court Case - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('court-case');

    // DataTable Add Button 
    $('#btn-add-court-case-dt').click(function () {
        //Clear Registration Dates //Modify By -- Sagar Kare
        $('#registration-dates').val('').removeAttr('min max').blur();

        event.preventDefault();

        SetModalTitle('court-case', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-court-case-dt').click(function () {
        SetModalTitle('court-case', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-court-case-dt').data('rowindex');
            id = $('#court-case-modal').attr('id');
            myModal = $('#' + id).modal();

            let courtcasefillingDate = new Date(columnValues[3]);
            let courtcaseregistrationDate = new Date(columnValues[5]);


            $('#court-case-types-id', myModal).val(columnValues[1]);
            $('#filing-dates', myModal).val(GetInputDateFormat(courtcasefillingDate));
            $('#filing-numbers', myModal).val(columnValues[4]);
            $('#registration-dates', myModal).val(GetInputDateFormat(courtcaseregistrationDate));
            $('#registration-numbers', myModal).val(columnValues[6]);
            $('#cnr-number-case', myModal).val(columnValues[7]);
            $('#amount-of-decree', myModal).val(columnValues[8]);
            $('#collateral-amount', myModal).val(columnValues[9]);
            $('#court-cases-stage-id', myModal).val(columnValues[10]);
            $('#note-court-case', myModal).val(columnValues[12]);
            $('#reason-for-modification-court-case', myModal).val(columnValues[13]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-court-case-edit-dt').addClass('read-only');
            $('#court-case-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-court-case-modal').click(function (event) {
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

            ClearModal('court-case');

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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-court-case tbody input[type="checkbox"]:checked').each(function () {
                    courtCaseDataTable.row($('#tbl-court-case tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-court-case-dt').data('rowindex');
                    EnableNewOperation('court-case');

                    $('#select-all-court-case').prop('checked', false);
                    if (!courtCaseDataTable.data().any())
                    $('#court-case-data-table-error').removeClass('d-none');

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
                EnableDeleteOperation('court-case')
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
        filingDate = $('#filing-dates').val();
        filingNumber = $('#filing-numbers').val();
        registrationDate = $('#registration-dates').val();
        registrationNumber = $('#registration-numbers').val();
        cnrNumber = $('#cnr-number-case').val();
        amountOfDecree = $('#amount-of-decree').val();
        collateralAmount = $('#collateral-amount').val();
        courtCaseStageId = $('#court-cases-stage-id option:selected').val();
        courtCaseStageIdText = $('#court-cases-stage-id option:selected').text();
        note = $('#note-court-case').val().trim();
        reasonForModification = $('#reason-for-modification-court-case').val();
        rvisibility = 0;
        hasDivClass = $('#court-case-div').hasClass('d-none');

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }

        if (courtCaseTypeId === '') {
            result = false;
            $('#court-case-types-id-error').removeClass('d-none');
        }
        else
            $('#court-case-types-id-error').addClass('d-none');

        let isValidFilingDate = IsValidInputDate('#filing-dates');

        if (isValidFilingDate === false) {
            result = false;
            $('#filing-dates-error').removeClass('d-none');
        }
        else
            $('#filing-dates-error').addClass('d-none');

        minimumLength = parseInt($('#filing-numbers').attr('minlength'));
        maximumLength = parseInt($('#filing-numbers').attr('maxlength'));

        if (filingNumber === '' || filingNumber.length < parseInt(minimumLength) || filingNumber.length > parseInt(maximumLength)) {
            result = false;
            $('#filing-numbers-error').removeClass('d-none');
        } else
            $('#filing-numbers-error').addClass('d-none');

        let isValidRegistrationDate = IsValidInputDate('#registration-dates');

        if (isValidRegistrationDate === false) {
            result = false;
            $('#registration-dates-error').removeClass('d-none');
        }
        else
            $('#registration-dates-error').addClass('d-none');

        minimumLength = parseInt($('#registration-numbers').attr('minlength'));
        maximumLength = parseInt($('#registration-numbers').attr('maxlength'));

        if (registrationNumber === '' || registrationNumber.length < parseInt(minimumLength) || registrationNumber.length > parseInt(maximumLength)) {
            result = false;
            $('#registration-numbers-error').removeClass('d-none');
        } else {
            $('#registration-numbers-error').addClass('d-none');
        }

        minimumLength = parseInt($('#cnr-number-case').attr('minlength'));
        maximumLength = parseInt($('#cnr-number-case').attr('maxlength'));

        if (cnrNumber === '' || cnrNumber.length < parseInt(minimumLength) || cnrNumber.length > parseInt(maximumLength)) {
            result = false;
            $('#cnr-number-case-error').removeClass('d-none');
        } else
            $('#cnr-number-case-error').addClass('d-none');

        amountOfDecree = parseFloat(amountOfDecree);

        minimum = parseFloat($('#amount-of-decree').attr('min'));
        maximum = parseFloat($('#amount-of-decree').attr('max'));

        if (isNaN(amountOfDecree) || amountOfDecree < parseFloat(minimum) || amountOfDecree > parseFloat(maximum)) {
            result = false;
            $('#amount-of-decree-error').removeClass('d-none');
        } else
            $('#amount-of-decree-error').addClass('d-none');

        collateralAmount = parseFloat(collateralAmount);

        minimum = parseFloat($('#collateral-amount').attr('min'));
        maximum = parseFloat($('#collateral-amount').attr('max'));

        if (isNaN(collateralAmount) || collateralAmount < parseFloat(minimum) || collateralAmount > parseFloat(maximum)) {
            result = false;
            $('#collateral-amount-error').removeClass('d-none');
        } else
            $('#collateral-amount-error').addClass('d-none');

        if (courtCaseStageId === '') {
            result = false;
            $('#court-cases-stage-id-error').removeClass('d-none');
        }
        else
            $('#court-cases-stage-id-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsCourtCaseDataTable() {
        courtCaseDataTable.column(1).visible(false);
        courtCaseDataTable.column(10).visible(false);
        courtCaseDataTable.column(13).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@   Person Income Tax Detail  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-income-tax-dt').click(function () {

        event.preventDefault();
        ClearMandatoryMark();
        SetModalTitle('income-tax', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-income-tax-dt').click(function () {
        
        SetModalTitle('income-tax', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-income-tax-dt').data('rowindex');
            id = $('#income-tax-modal').attr('id');
            myModal = $('#' + id).modal();
            // Display Value In Modal Inputs
            $('#assessments-year-income-tax', myModal).val(columnValues[1]);
            $('#tax-amounts', myModal).val(columnValues[2]);
            let storagePathInput = columnValues[3];

            let storagePathId = $(storagePathInput).attr('id');
            let docPath = $('#' + storagePathId).get(0);
            let files = docPath.files;
            if (files.length !== 0) {
                let dt = new DataTransfer();
                for (let j = 0; j < files.length; j++) {
                    let f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            photoPathTax = $('#photo-path-tax').get(0);
            photoPathTax.files = dt.files;

            let photoinput = columnValues[4];
            let photoPathTaxId = $(photoinput).attr('id');
            let photoSrc = $('#' + photoPathTaxId).attr('src');
            $('#photo-path-tax-image-preview').attr('src', photoSrc);

            $('#file-caption-tax', myModal).val(columnValues[5]);
            $('#note-income-tax-detail', myModal).val(columnValues[6]);
            $('#reason-for-modification-tax-detail', myModal).val(columnValues[7]);
            // Show Modals
            myModal.modal({ show: true });
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
                dochtml,
                dochtml1,
                fileCaption,
                note,
                reasonForModification,
                PrmKey
            ]).draw();

            files = photoPathTax.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (var j = 0; j < files.length; j++) {
                    var f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }
            else {
                value: 'None'
            }

            docPath = $("#" + newID).get(0);
            if (photoPathTax.files.length !== 0) {
                docPath.files = dt.files;
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            $('#income-tax-data-table-error').addClass('d-none');

            HideColumnsIncomeTaxDataTable();

            incomeTaxDataTable.columns.adjust().draw();

            ClearModal('income-tax');

            $('#income-tax-modal').modal('hide');

            EnableNewOperation('income-tax');
        }
    });

    // Modal update Button Event
    $('#btn-update-income-tax-modal').click(function (event) {
        
        let oldId = 'photoPathTax' + (i - 1);
        $('#select-all-income-tax').prop('checked', false);
        if (IsValidIncomeTaxModal()) {
            incomeTaxDataTable.row(selectedRowIndex).data([
                tag,
                assessmentYear,
                taxAmount,
                dochtml,
                dochtml1,
                fileCaption,
                note,
                reasonForModification,
                PrmKey
            ]).draw();

            files = photoPathTax.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);

            if (photoPathTax.files.length !== 0)
                docPath.files = dt.files;

            HideColumnsIncomeTaxDataTable();

            incomeTaxDataTable.columns.adjust().draw();

            $('#income-tax-modal').modal('hide');

            EnableNewOperation('income-tax');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-income-tax-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-income-tax tbody input[type="checkbox"]:checked').each(function () {
                    incomeTaxDataTable.row($('#tbl-income-tax tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-income-tax-dt').data('rowindex');
                    EnableNewOperation('income-tax');

                    $('#select-all-income-tax').prop('checked', false);

                    if (!incomeTaxDataTable.data().any())
                    $('#income-tax-data-table-error').removeClass('d-none');

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
        
        result = true;
        counter++;
        i = counter;
        newID = 'photoPathTax' + i;
        photoID = 'PhotoId' + i;

        // Get Modal Inputs In Local letiable
        $('#select-all-asset-document').prop('checked', false);

        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        assessmentYear = $('#assessments-year-income-tax').val();
        taxAmount = $('#tax-amounts').val();
        photo = $('#photo-path-tax-image-preview').attr('src');
        photoPathTax = $('#photo-path-tax').get(0);
        dochtml = '<input type="file", id="' + newID + '", name = "DocPath", disabled="true"/>';
        dochtml1 = '<img src="' + photo + '", width="50", height="50", id= "' + photoID + '" />';
        fileCaption = $('#file-caption-tax').val();
        note = $('#note-income-tax-detail').val();
        reasonForModification = $('#reason-for-modification-tax-detail').val();
        rvisibility = 0;
        hasDivClass = $('#income-tax-div').hasClass('d-none');

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        let docIncomeTaxUpload = $('#income-tax-detail-upload').val();

        if (docIncomeTaxUpload === 'M') {
            path = $('#photo-path-tax').val();
            fileCaption;

        } else if (docIncomeTaxUpload === 'O') {
            if (photo === '' && fileCaption === '') {
                path = 'None';
                fileCaption = 'None';
            }
            else if (photo === '') {
                path = 'None';
                fileCaption;
            }
            else if (fileCaption === '') {
                path = $('#photo-path-tax').val();
                fileCaption = 'None';
            }
            else if (photo !== '' && fileCaption !== '') {
                path = $('#photo-path-tax').val();
                fileCaption;
            }
        }
        else if (docIncomeTaxUpload === 'D') {
            if (photo === '') {
                path = 'None';
                fileCaption = 'None';
            }
        }


        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification === '')
                reasonForModification = 'None';
        }

        PrmKey = 0;
        if (($('#income-tax-detail-upload').val()) === 'M') {
            path = $('#photo-path-tax').val();
        } else {
            path = 'None';
        }

        // Get the current year
        let currentYear = new Date().getFullYear();

        // Calculate the minimum allowed year
        let minAllowedYear = currentYear - 20;

        // Generate allowed years list dynamically

        // Generate allowed years list dynamically
        const allowedYears = Array.from({ length: currentYear - minAllowedYear + 1 }, (element, index) => (minAllowedYear + index).toString());

        // Assuming assessmentYear is a string letiable containing the year
        if (assessmentYear === '' || !allowedYears.includes(assessmentYear)) {
            result = false;
            $('#assessments-year-income-tax-error').removeClass('d-none');
        } else {
            $('#assessments-year-income-tax-error').addClass('d-none');
        }

        if (taxAmount === '' || isNaN(taxAmount) || taxAmount < 0 || taxAmount > 999999999) {
            result = false;
            $('#tax-amounts-error').removeClass('d-none');
        } else
            $('#tax-amounts-error').addClass('d-none');


        if (path === '') {
            result = false;
            $('#photo-path-tax-error').removeClass('d-none');
        }
        else
            $('#photo-path-tax-error').addClass('d-none');

        if (fileCaption === '') {
            result = false;
            $('#file-caption-tax-error').removeClass('d-none');
        }
        else
            $('#file-caption-tax-error').addClass('d-none');

        return result;

    }

    // Hide Unnecessary Columns
    function HideColumnsIncomeTaxDataTable() {
        incomeTaxDataTable.column(7).visible(false);
        incomeTaxDataTable.column(8).visible(false);
    }

    $('.doc-upload').focusout(function () {
        
        ClearMandatoryMark();
    });

    // Movable Asset manufacturing-year-movable-asset
    $('#manufacturing-year-movable-asset').focusout(function () {
        
        let manufacturingYear = parseInt($('#manufacturing-year-movable-asset').val());
        let dateOfPurchase = $('#date-of-purchase-movable-asset');
        let today = new Date();
        let currentYear = today.getFullYear();
        let fiftyYearsAgo = today.getFullYear() - 50;

        if (!isNaN(manufacturingYear)) {
            if (manufacturingYear < fiftyYearsAgo || manufacturingYear > currentYear) {
                // Disable the date picker if the manufacturing year is more than 50 years ago
                dateOfPurchase.val(''); // Clear the value
                dateOfPurchase.attr('disabled', true);
            } else {
                dateOfPurchase.attr('disabled', false); // Enable the date picker

                let minDate = new Date(manufacturingYear, 0, 1); // 1st Jan of the manufacturing year
                let maxDate;

                let tenYearsLater = new Date(manufacturingYear + 10, 11, 31); // 31st Dec of the 10th year

                // Set maxDate as today's date if the manufacturing year is 2024 or earlier
                if (manufacturingYear <= today.getFullYear()) {
                    maxDate = today < tenYearsLater ? today : tenYearsLater;
                } else {
                    maxDate = tenYearsLater;
                }

                //// Convert dates to yyyy-mm-dd format for input[type=date]
                //let minDateFormatted = minDate.toISOString().split('T')[0];
                //let maxDateFormatted = maxDate.toISOString().split('T')[0];

                // Convert dates to yyyy-mm-dd format for input[type=date] without timezone issues
                let minDateFormatted = `${minDate.getFullYear()}-${String(minDate.getMonth() + 1).padStart(2, '0')}-${String(minDate.getDate()).padStart(2, '0')}`;
                let maxDateFormatted = `${maxDate.getFullYear()}-${String(maxDate.getMonth() + 1).padStart(2, '0')}-${String(maxDate.getDate()).padStart(2, '0')}`;


                // Set the min and max attributes
                dateOfPurchase.attr('min', minDateFormatted);
                dateOfPurchase.attr('max', maxDateFormatted);

                // Clear the current value if it's outside the new range
                let currentVal = dateOfPurchase.val();
                if (currentVal && (currentVal < minDateFormatted || currentVal > maxDateFormatted)) {
                    dateOfPurchase.val(''); // Clear if out of range
                }
            }
        } else {
            // Remove min and max attributes and enable the input if the input is not a valid year
            dateOfPurchase.removeAttr('min').removeAttr('max').attr('disabled', false);
        }
    });

    // Movable Asset date-of-purchase-movable-asset
    $('#date-of-purchase-movable-asset').click(function () {
        
        let manufacturingYear = parseInt($('#manufacturing-year-movable-asset').val());
        let dateOfPurchase = $('#date-of-purchase-movable-asset');

        let minDate = new Date(manufacturingYear, 0, 1); // 1st Jan of the manufacturing year
        let maxDate;

        let tenYearsLater = new Date(manufacturingYear + 10, 11, 31); // 31st Dec of the 10th year

        // Set maxDate as today's date if the manufacturing year is 2024 or earlier
        if (manufacturingYear <= today.getFullYear()) {
            maxDate = today < tenYearsLater ? today : tenYearsLater;
        } else {
            maxDate = tenYearsLater;
        }

        let minDateFormatted = `${minDate.getFullYear()}-${String(minDate.getMonth() + 1).padStart(2, '0')}-${String(minDate.getDate()).padStart(2, '0')}`;
        let maxDateFormatted = `${maxDate.getFullYear()}-${String(maxDate.getMonth() + 1).padStart(2, '0')}-${String(maxDate.getDate()).padStart(2, '0')}`;


        // Set the min and max attributes
        dateOfPurchase.attr('min', minDateFormatted);
        dateOfPurchase.attr('max', maxDateFormatted);
    });

    //  Machinery Asset manufacturing-year-machinery-asset
    $('#manufacturing-year-machinery-asset').focusout(function () {
        
        let manufacturingYear = parseInt($('#manufacturing-year-machinery-asset').val());
        let dateOfPurchase = $('#date-of-purchase-machinery-asset');
        let today = new Date();
        let currentYear = today.getFullYear();
        let fiftyYearsAgo = today.getFullYear() - 50;

        if (!isNaN(manufacturingYear)) {
            if (manufacturingYear < fiftyYearsAgo || manufacturingYear > currentYear) {
                // Disable the date picker if the manufacturing year is more than 50 years ago
                dateOfPurchase.val(''); // Clear the value
                dateOfPurchase.attr('disabled', true);
            } else {
                dateOfPurchase.attr('disabled', false); // Enable the date picker

                let minDate = new Date(manufacturingYear, 0, 1); // 1st Jan of the manufacturing year
                let maxDate;

                let tenYearsLater = new Date(manufacturingYear + 10, 11, 31); // 31st Dec of the 10th year

                // Set maxDate as today's date if the manufacturing year is 2024 or earlier
                if (manufacturingYear <= today.getFullYear()) {
                    maxDate = today < tenYearsLater ? today : tenYearsLater;
                } else {
                    maxDate = tenYearsLater;
                }

                //// Convert dates to yyyy-mm-dd format for input[type=date]
                //let minDateFormatted = minDate.toISOString().split('T')[0];
                //let maxDateFormatted = maxDate.toISOString().split('T')[0];

                // Convert dates to yyyy-mm-dd format for input[type=date] without timezone issues
                let minDateFormatted = `${minDate.getFullYear()}-${String(minDate.getMonth() + 1).padStart(2, '0')}-${String(minDate.getDate()).padStart(2, '0')}`;
                let maxDateFormatted = `${maxDate.getFullYear()}-${String(maxDate.getMonth() + 1).padStart(2, '0')}-${String(maxDate.getDate()).padStart(2, '0')}`;


                // Set the min and max attributes
                dateOfPurchase.attr('min', minDateFormatted);
                dateOfPurchase.attr('max', maxDateFormatted);

                // Clear the current value if it's outside the new range
                let currentVal = dateOfPurchase.val();
                if (currentVal && (currentVal < minDateFormatted || currentVal > maxDateFormatted)) {
                    dateOfPurchase.val(''); // Clear if out of range
                }
            }
        } else {
            // Remove min and max attributes and enable the input if the input is not a valid year
            dateOfPurchase.removeAttr('min').removeAttr('max').attr('disabled', false);
        }
    });

    //Machineri Asset date-of-purchase-machinery-asset
    $('#date-of-purchase-machinery-asset').click(function () {
        
        let manufacturingYear = parseInt($('#manufacturing-year-movable-asset').val());
        let dateOfPurchase = $('#date-of-purchase-machinery-asset');

        let minDate = new Date(manufacturingYear, 0, 1); // 1st Jan of the manufacturing year
        let maxDate;

        let tenYearsLater = new Date(manufacturingYear + 10, 11, 31); // 31st Dec of the 10th year

        // Set maxDate as today's date if the manufacturing year is 2024 or earlier
        if (manufacturingYear <= today.getFullYear()) {
            maxDate = today < tenYearsLater ? today : tenYearsLater;
        } else {
            maxDate = tenYearsLater;
        }

        let minDateFormatted = `${minDate.getFullYear()}-${String(minDate.getMonth() + 1).padStart(2, '0')}-${String(minDate.getDate()).padStart(2, '0')}`;
        let maxDateFormatted = `${maxDate.getFullYear()}-${String(maxDate.getMonth() + 1).padStart(2, '0')}-${String(maxDate.getDate()).padStart(2, '0')}`;


        // Set the min and max attributes
        dateOfPurchase.attr('min', minDateFormatted);
        dateOfPurchase.attr('max', maxDateFormatted);
    });

    $('#income-source-id').focusout(function () {
        $('#annual-incomes').val('');
        $('#other-details').val('');
        $('#note-income-detail').val('');
        $('.modal-input-error').addClass('d-none');
    });


    // Regex for alphanumeric characters, dashes, underscores, and spaces
    let regex_alphanumericwithdashunderscorespace = /^[a-z0-9\-_ ]*$/i;

    $('#account-number').keypress(function (e) {
        
        if (!e.key.match(regex_alphanumericwithdashunderscorespace)) {
            e.preventDefault();
            return false;
        } else {
            // Replace multiple spaces with a single space
            $(this).val($(this).val().replace(/\s+/g, ' '));
        }
    });



    function ClearMandatoryMark() {

        let incomeTaxDocumentUpload = $('#income-tax-detail-upload').val()

        if (incomeTaxDocumentUpload === 'M') {
            $('#photo-path-tax').addClass('mandatory-mark');
        }
        else
            $('#photo-path-tax').removeClass('mandatory-mark');;
    }

    /// @@@@@@@@@@@@@@@@@@@@@@   Social Media  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('social-media');

    // DataTable Add Button 
    $('#btn-add-social-media-dt').click(function () {

        event.preventDefault();

        SetModalTitle('social-media', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-social-media-dt').click(function () {
        SetModalTitle('social-media', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-social-media-dt').data('rowindex');
            id = $('#social-media-modal').attr('id');
            myModal = $('#' + id).modal();
            $('#social-media-id', myModal).val(columnValues[1]);
            $('#social-media-link', myModal).val(columnValues[3]);
            $('#other-details-social-media', myModal).val(columnValues[4]);
            $('#note-social-media', myModal).val(columnValues[5]);
            $('#reason-for-modification-social-media', myModal).val(columnValues[6]);
            // Show Modals
            myModal.modal({ show: true });
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

            ClearModal('social-media');

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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-social-media tbody input[type="checkbox"]:checked').each(function () {
                    socialMediaDataTable.row($('#tbl-social-media tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-social-media-dt').data('rowindex');
                    EnableNewOperation('social-media');

                    $('#select-all-social-media').prop('checked', false);
                   if (!socialMediaDataTable.data().any())
                    $('#social-media-data-table-error').removeClass('d-none');

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
        rvisibility = 0;
        hasDivClass = $('#social-media-div').hasClass('d-none');

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        if (socialMediaLink === '')
            socialMediaLink = 'None';

        if (otherDetails === '')
            otherDetails = 'None';

        if (hasDivClass === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
            if (reasonForModification == '')
                reasonForModification = 'None';
        }

        if (socialMediaId === '') {
            result = false;
            $('#social-media-id-error').removeClass('d-none');
        }
        else
            $('#social-media-id-error').addClass('d-none');


        return result;

    }

    // Hide Unnecessary Columns
    function HideColumnsSocialMediaDataTable() {
        socialMediaDataTable.column(1).visible(false);
        socialMediaDataTable.column(6).visible(false);
    }

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

    // ClearFundModalInputs();
    ClearModal('sms-alert');

    // DataTable Add Button 
    $('#btn-add-sms-alert-dt').click(function () {

        event.preventDefault();
        $('.field_wrapper1').html('');
        SetModalTitle('sms-alert', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-sms-alert-dt').click(function () {
        
        SetModalTitle('sms-alert', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-sms-alert-dt').data('rowindex');
            id = $('#sms-alert-modal').attr('id');
            myModal = $('#' + id).modal();

            [time, meridian] = columnValues[5].split(' ');
            [hours, minutes] = time.split(':');
            if (hours === '12') {
                hours = '00';
            }
            if (meridian === 'PM')
                hours = parseInt(hours, 10) + 12;
            sendingTime = hours + ':' + minutes;

            $('#alert-type-id', myModal).val(columnValues[1]);
            $('#notice-language-id', myModal).val(columnValues[3]);
            $('#sending-time', myModal).val(sendingTime);
            $('#note-sms-alert', myModal).val(columnValues[6]);
            $('#reason-for-modification-sms-alert', myModal).val(columnValues[7]);
            $('.field_wrapper1').html('');
            // Show Modals
            myModal.modal({ show: true });
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

            ClearModal('sms-alert');

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
            if (confirm('Are You Sure To Delete This Row?')) {
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
        rvisibility = 0;
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

                //$('#sending-time-error1').addClass('d-none');

            }
            //else {
            //    $('#sending-time-error1').removeClass('d-none');
            //    result = false;
            //}
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
        if (note === '')
            note = 'None';

        if (ss === true) {
            reasonForModification = 'None';
            rvisibility = 0;
        }
        else {
            rvisibility = 1;
        }

        if (personInformationParameterNoticeTypeId === '') {
            result = false;
            $('#alert-type-id-error').removeClass('d-none');
        }
        else
            $('#alert-type-id-error').addClass('d-none');

        if (appLanguageId === '') {
            result = false;
            $('#notice-language-id-error').removeClass('d-none');
        }
        else
            $('#notice-language-id-error').addClass('d-none');

        if (sendingTime === '') {
            result = false;

            $('#sending-time-error').removeClass('d-none');
        }
        else
            $('#sending-time-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsSmsAlertDataTable() {
        smsAlertDataTable.column(1).visible(false);
        smsAlertDataTable.column(3).visible(false);
        smsAlertDataTable.column(7).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Set Page Loading Default Values @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    function SetPageLoadingDefaultValues() {
        

        CheckPersonType(personTypeId);


        $('#tbl-authorized-signatory tbody input[type="checkbox"]').each(function () {
            
            $(this).prop('checked', true);

            row = $(this).closest('tr');

            selectedRowIndex = boardOfDirectorAuthorizedDataTable.row(row).index();

            rowData = (boardOfDirectorAuthorizedDataTable.row(selectedRowIndex).data());

            arr.push({ arrayCloumn1: rowData[5] });
            if (rowData[5] === "True") {
                $('#heading-person-photo-sign').removeClass('d-none');
            }
            else {
                $('#heading-person-photo-sign').addClass('d-none');

            }
        });

        // Get Person Dropdown For Guardian
        $.get('/DynamicDropdownList/GetPersonDropdownListForGuardian', function (data) {
            personDropdownListDataForFamily = data;
        });

        // Select Default Record, If Dropdown Has Only One Record
        let listItemCount = $('#home-branch-id > option').not(':first').length;

        if ($('#dob').val() !== '')
            $('#marital-status').attr('disabled', false);
        else
            $('#marital-status').attr('disabled', true);


        // Select Default First Record, If Dropdown Has Only One Record
        if (listItemCount === 1) {
            $('#home-branch-id').prop('selectedIndex', 1);
            $('#home-branch-id').change();
        }

        //'vip-rank' and convert it to an integer
        let VIPRank = parseInt($('#vip-rank').val());
        if (isNaN(VIPRank) || VIPRank <= 0) {
            $('.vip-background-details').addClass('d-none');
            $('#vip-background-details, #trans-vip-background-details').val('None');
        }
        else
            $('.vip-background-details').removeClass('d-none');

        ClearModalInputs();

        // Call the function marital status 
        CheckMaritalStatus(maritalStatusId);

        // Call the function Guardain
        let dateOfBirth = $('#dob').val();
        // Function to calculate age based on date of birth
        CalculateAge(dateOfBirth);

        // Call the function Occupation
        CheckOccupation(occupationId);

        // Call the function City
        HandleCitySelection();

        // Call the function GST 
        validationForGST();

        // Call the function GST for amend and verify 
        let registrationNumber = $('#gst-registration-number').val(); // Get the value correctly
        if (registrationNumber.length === 15) {
            // Check the checkbox
            $('#enable-gst-registration-details').prop('checked', true);
        } else {
            // Uncheck the checkbox
            $('#enable-gst-registration-details').prop('checked', false);
        }

        // Call the function GST for amend and verify 
        //validationForGSTVerify();

        // Call the function KYC DoumentType
        ValidationForDocumentTypes();

        // Call the function Contact ContactType
        ValidationForContactTypes();

        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();

    }


    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@


    $('#btnsave').click(function () {
        
        let isValidAllInputs = true;
        //let personId = $('#person-id option:selected').val();
        //if ($('form').valid() && isValidPancardNumber && isValidAadharNumber)
        if ($('form').valid()) {
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

            let personFormData
            personFormData = new FormData($("#form")[0]);

            // Accordion 3 - Home Branch Validation, If Enable
            if (!$('#heading-home-branch').hasClass('d-none')) {
                if (!IsValidHomeBranchAccordionInputs())
                    isValidAllInputs = false;
            }

            // Accordion 4 - Person Additional Detail Validation, If Enable
            if (!IsValidAdditionalDetailsAccordionInputs())
                isValidAllInputs = false;

            // Accordion 5 - Foreigner Person Validation, If Enable
            //if (!IsValidForeignerAccordionInputs())
            //   isValidAllInputs = false;

            // Accordion 5 - Guardian Person Validation, If Enable
            if (!$('#heading-guardian-details').hasClass('d-none')) {
                if (!IsValidGuardianAccordionInputs())
                    isValidAllInputs = false;
            }

            // Accordion 6 - Photo Sign Validation, If Enable
            if (!$('#heading-person-photo-sign').hasClass('d-none')) {
                if (!IsValidPhotoSignAccordionInputs())
                    isValidAllInputs = false;
            }

            // Accordion 6 - Person gst-registration Validation, If Enable
            if ($('#enable-gst-registration-Details').is(':checked')) {
                if (!IsValidGstRegistrationAccordionInputs())
                    isValidAllInputs = false;
            }

            // Accordion 6 - Person Commodities Asset Validation, If Enable
            if ($('#heading-commodities-asset').hasClass('d-none')) {
                IsValidCommoditiesAssetAccordionInputs()
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
                                personFormData.append("_PersonKYCDocument[" + i + "].DocumentTypeId", columnValues[1]);
                                personFormData.append("_PersonKYCDocument[" + i + "].DocumentDocumentTypeId", columnValues[3]);
                                personFormData.append("_PersonKYCDocument[" + i + "].NameAsPerDocument", columnValues[5]);
                                personFormData.append("_PersonKYCDocument[" + i + "].DocumentNumber", columnValues[6]);
                                personFormData.append("_PersonKYCDocument[" + i + "].SequenceNumber", columnValues[7]);
                                personFormData.append("_PersonKYCDocument[" + i + "].DateOfIssue", columnValues[8]);
                                personFormData.append("_PersonKYCDocument[" + i + "].DateOfExpiry", columnValues[9]);
                                personFormData.append("_PersonKYCDocument[" + i + "].IssuingAuthority", columnValues[10]);
                                personFormData.append("_PersonKYCDocument[" + i + "].PlaceOfIssue", columnValues[11]);
                                personFormData.append("_PersonKYCDocument[" + i + "].DateOfRequest", columnValues[12]);
                                personFormData.append("_PersonKYCDocument[" + i + "].DateOfExpectingSubmit", columnValues[13]);
                                personFormData.append("_PersonKYCDocument[" + i + "].DateOfSubmit", columnValues[14]);
                                personFormData.append("_PersonKYCDocument[" + i + "].DocumentUploadStatus", columnValues[15]);
                                personFormData.append("_PersonKYCDocument[" + i + "].PhotoPathKYC", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_PersonKYCDocument[" + i + "].LocalStoragePath", $('#KycStoragePath').val());
                                personFormData.append("_PersonKYCDocument[" + i + "].FileCaption", columnValues[19]);
                                personFormData.append("_PersonKYCDocument[" + i + "].Note", columnValues[20]);
                                personFormData.append("_PersonKYCDocument[" + i + "].ReasonForModification", columnValues[21]);
                                personFormData.append("_PersonKYCDocument[" + i + "].PersonKYCDocumentPrmKey", columnValues[22]);
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
            if ($('#heading-family-detail').hasClass('d-none')) {
                
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
                                    'personInformationNumber': columnValues[1],
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

            // Create Array For person board of director Authorized Table To Pass Data
            if (!$('#heading-person-group-authorized-signatory').hasClass('d-none')) {
                if (boardOfDirectorAuthorizedDataTable.data().any()) {
                    
                    $('#authorized-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {
                        
                        $('#tbl-authorized-signatory > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (boardOfDirectorAuthorizedDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }
                            else {
                                
                                let row = $(this);
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].PersonInformationNumber", columnValues[1]);
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].FullNameOfAuthorizedPerson", columnValues[3]);
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].TransFullNameOfAuthorizedPerson", columnValues[4]);
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].AuthorizedPersonAddressDetail", columnValues[5]);
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].TransAuthorizedPersonAddressDetail", columnValues[6]);
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].AuthorizedPersonContactDetail", columnValues[7]);
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].TransAuthorizedPersonContactDetail", columnValues[8]);
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].DesignationId", columnValues[9]);
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].IsAuthorizedSignatory", columnValues[11]);
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].PhotoPathSign", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].SignLocalStoragePath", $('#SignStoragePath').val());
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].FileCaption", columnValues[14]);
                                personFormData.append("_GroupAuthorizedSignatory[" + i + "].Note", columnValues[15]);
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
                //    $('#relation-accordion-error').removeClass('d-none');
                //    isValidAllInputs = false;

                //}
            }

            // Create Array For person bank detail Table To Pass Data
            if ($('#heading-bank-detail').hasClass('d-none')) {
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
                                personFormData.append("_PersonBankDetail[" + i + "].BankId", columnValues[1]);
                                personFormData.append("_PersonBankDetail[" + i + "].BankBranchId", columnValues[3]);
                                personFormData.append("_PersonBankDetail[" + i + "].AccountNumber", columnValues[5]);
                                personFormData.append("_PersonBankDetail[" + i + "].OpeningDate", columnValues[6]);
                                personFormData.append("_PersonBankDetail[" + i + "].CloseDate", columnValues[7]);
                                personFormData.append("_PersonBankDetail[" + i + "].IsDefaultBankForTransaction", columnValues[8]);
                                personFormData.append("_PersonBankDetail[" + i + "].PhotoPathBank", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_PersonBankDetail[" + i + "].BankStatementLocalStoragePath", $('#StoragePath').val());
                                personFormData.append("_PersonBankDetail[" + i + "].FileCaption", columnValues[11]);
                                personFormData.append("_PersonBankDetail[" + i + "].Note", columnValues[12]);
                                personFormData.append("_PersonBankDetail[" + i + "].ReasonForModification", columnValues[13]);
                                personFormData.append("_PersonBankDetail[" + i + "].PersonBankDetailPrmKey", columnValues[14]);
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
                
                if (gstDataTable.data().any()) {

                    $('#gst-registration-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {
                        
                        $('#tbl-gst-registration > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (gstDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }
                            else {
                                let row = $(this);
                                personFormData.append("_PersonGSTRegistrationDetail[" + i + "].AssessmentYear", columnValues[1]);
                                personFormData.append("_PersonGSTRegistrationDetail[" + i + "].TaxAmount", columnValues[2]);
                                personFormData.append("_PersonGSTRegistrationDetail[" + i + "].PhotoPathGst", $(row).find('TD').find('input[type="file"]').get(0).files[0]);
                                personFormData.append("_PersonGSTRegistrationDetail[" + i + "].GSTReturnStatementLocalStoragePath", $('#GSTStoragePath').val());
                                personFormData.append("_PersonGSTRegistrationDetail[" + i + "].FileCaption", columnValues[5]);
                                personFormData.append("_PersonGSTRegistrationDetail[" + i + "].Note", columnValues[6]);
                                personFormData.append("_PersonGSTRegistrationDetail[" + i + "].ReasonForModification", columnValues[7]);
                                personFormData.append("_PersonGSTRegistrationDetail[" + i + "].PersonGSTReturnDocumentPrmKey", columnValues[8]);
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
            if ($('#heading-chronic-disease').hasClass('d-none')) {
                
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

            // Create Array For person insurance detail Table To Pass Data
            if ($('#heading-insurance-detail').hasClass('d-none')) {
                
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
            if ($('#heading-financial-asset').hasClass('d-none')) {
                
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
                                personFormData.append("_PersonFinancialAsset[" + i + "].FinancialOrganizationTypeId", columnValues[1]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].NameOfFinancialOrganization", columnValues[3]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].TransNameOfFinancialOrganization", columnValues[4]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].NameOfBranch", columnValues[5]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].TransNameOfBranch", columnValues[6]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].AddressDetails", columnValues[7]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].TransAddressDetails", columnValues[8]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].ContactDetails", columnValues[9]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].TransContactDetails", columnValues[10]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].OpeningDate", columnValues[11]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].MaturityDate", columnValues[12]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].FinancialAssetTypeId", columnValues[13]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].FinancialAssetDescription", columnValues[15]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].TransFinancialAssetDescription", columnValues[16]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].ReferenceNumber", columnValues[17]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].TransReferenceNumber", columnValues[18]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].InvestedAmount", columnValues[19]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].MonthlyInterestIncomeAmount", columnValues[20]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].CurrentMarketValue", columnValues[21]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].HasAnyMortgage", columnValues[22]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].PhotoPathFinance", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].LocalStoragePath", $('#FinanceStoragePath').val());
                                personFormData.append("_PersonFinancialAsset[" + i + "].FileCaption", columnValues[25]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].Note", columnValues[26]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].TransNote", columnValues[27]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].ReasonForModification", columnValues[28]);
                                personFormData.append("_PersonFinancialAsset[" + i + "].PersonFinancialAssetDocumentPrmKey", columnValues[29]);
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
            if ($('#heading-movable-asset').hasClass('d-none')) {
                
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
                                let row = $(this);

                                personFormData.append("_PersonMovableAsset[" + i + "].VehicleModelId", columnValues[1]);
                                personFormData.append("_PersonMovableAsset[" + i + "].VehicleVariantId", columnValues[3]);
                                personFormData.append("_PersonMovableAsset[" + i + "].NumberOfOwners", columnValues[5]);
                                personFormData.append("_PersonMovableAsset[" + i + "].ManufacturingYear", columnValues[6]);
                                personFormData.append("_PersonMovableAsset[" + i + "].PurchaseDate", columnValues[7]);
                                personFormData.append("_PersonMovableAsset[" + i + "].RegistrationDate", columnValues[8]);
                                personFormData.append("_PersonMovableAsset[" + i + "].RegistrationNumber", columnValues[9]);
                                personFormData.append("_PersonMovableAsset[" + i + "].PurchasePrice", columnValues[10]);
                                personFormData.append("_PersonMovableAsset[" + i + "].CurrentMarketValue", columnValues[11]);
                                personFormData.append("_PersonMovableAsset[" + i + "].OwnershipPercentage", columnValues[12]);
                                personFormData.append("_PersonMovableAsset[" + i + "].HasAnyMortgage", columnValues[13]);
                                personFormData.append("_PersonMovableAsset[" + i + "].IsOwnershipDeceased", columnValues[14]);
                                personFormData.append("_PersonMovableAsset[" + i + "].FileCaption", columnValues[17]);
                                personFormData.append("_PersonMovableAsset[" + i + "].PhotoPathMovable", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_PersonMovableAsset[" + i + "].LocalStoragePath", $('#MovableStoaragePath').val());
                                personFormData.append("_PersonMovableAsset[" + i + "].Note", columnValues[18]);
                                personFormData.append("_PersonMovableAsset[" + i + "].ReasonForModification", columnValues[19]);
                                personFormData.append("_PersonMovableAsset[" + i + "].PersonMovableAssetDocumentPrmKey", columnValues[20]);
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
            if ($('#heading-immovable-asset').hasClass('d-none')) {
                
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
                                let row = $(this);

                                personFormData.append("_PersonImmovableAsset[" + i + "].SurveyNumber", columnValues[1]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].CitySurveyNumber", columnValues[2]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].OtherNumber", columnValues[3]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].AreaOfLand", columnValues[4]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].ConstructionArea", columnValues[5]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].CarpetArea", columnValues[6]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].CurrentMarketValue", columnValues[7]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].AnnualRentIncome", columnValues[8]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].ResidenceTypeId", columnValues[9]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].OwnershipTypeId", columnValues[11]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].OwnershipPercentage", columnValues[13]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].IsConstructed", columnValues[14]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].HasAnyMortgage", columnValues[15]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].IsOwnershipDeceased", columnValues[16]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].PhotoPathImmovable", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].LocalStoragePath", $('#ImmovableStoaragePath').val());
                                personFormData.append("_PersonImmovableAsset[" + i + "].FileCaption", columnValues[19]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].AssetFullDescription", columnValues[20]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].Note", columnValues[21]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].ReasonForModification", columnValues[22]);
                                personFormData.append("_PersonImmovableAsset[" + i + "].PersonImmovableAssetDocumentPrmKey", columnValues[23]);
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
            if ($('#heading-agriculture-asset').hasClass('d-none')) {
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

                                personFormData.append("_PersonAgricultureAsset[" + i + "].AgricultureLandTypeId", columnValues[1]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].AgricultureLandDescription", columnValues[3]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].SurveyNumber", columnValues[4]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].GroupNumber", columnValues[5]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].AreaOfLand", columnValues[6]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].Volume", columnValues[7]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].OwnershipTypeId", columnValues[8]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].OwnershipPercentage", columnValues[10]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].CurrentMarketValue", columnValues[11]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].AnnualIncomeFromLand", columnValues[12]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].HasAnyCourtCase", columnValues[13]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].CourtCaseFullDetails", columnValues[14]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].IsOnlyRainFedTypeIrrigation", columnValues[15]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].HasCanalRiverIrrigationSource", columnValues[16]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].HasWellsIrrigationSource", columnValues[17]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].HasFarmLakeSource", columnValues[18]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].HasAnyMortgage", columnValues[19]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].IsOwnershipDeceased", columnValues[20]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].PhotoPathAgree", $(row).find('TD').find('input[type="file" ]').get(0).files[0]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].LocalStoragePath", $('#AgricultureStoaragePath').val());
                                personFormData.append("_PersonAgricultureAsset[" + i + "].FileCaption", columnValues[23]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].Note", columnValues[24]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].ReasonForModification", columnValues[25]);
                                personFormData.append("_PersonAgricultureAsset[" + i + "].PersonAgricultureAssetDocumentPrmKey", columnValues[26]);
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
            if ($('#heading-machinery-asset').hasClass('d-none')) {
                
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
                                row = $(this);
                                personFormData.append("_PersonMachineryAsset[" + i + "].NameOfMachinery", columnValues[1]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].MachineryFullDetails", columnValues[2]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].ManufacturingYear", columnValues[3]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].DateOfPurchase", columnValues[4]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].NumberOfOwners", columnValues[5]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].ReferenceNumber", columnValues[6]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].PurchasePrice", columnValues[7]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].CurrentMarketValue", columnValues[8]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].OwnershipPercentage", columnValues[9]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].HasAnyMortgage", columnValues[10]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].IsOwnershipDeceased", columnValues[11]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].PhotoPathMachinery", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].LocalStoragePath", $('#MachieneryStoaragePath').val());
                                personFormData.append("_PersonMachineryAsset[" + i + "].Note", columnValues[15]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].ReasonForModification", columnValues[16]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].FileCaption", columnValues[14]);
                                personFormData.append("_PersonMachineryAsset[" + i + "].PersonMachineryAssetDocumentPrmKey", columnValues[17]);
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
            if ($('#heading-income-details').hasClass('d-none')) {
                
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

            // Create Array For person borrowing detail Table To Pass Data
            if ($('#heading-borrowing-detail').hasClass('d-none')) {
                debugger
                if (borrowingDataTable.data().any()) {

                    $('#borrowing-detail-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-borrowing-detail > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (borrowingDataTable.row(currentRow).data());

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
            if ($('#heading-credit-rating').hasClass('d-none')) {
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
            if ($('#heading-court-case').hasClass('d-none')) {
                
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
            if ($('#heading-income-tax').hasClass('d-none')) {
                
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
            if ($('#heading-sms-alert').hasClass('d-none')) {
                
                if (smsAlertDataTable.data().any()) {
                    
                    $('#sms-alert-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-sms-alert > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (smsAlertDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                personSMSAlertArray.push(
                                {
                                    'PersonInformationParameterNoticeTypeId': columnValues[1],
                                    'AppLanguageId': columnValues[3],
                                    'SendingTime': columnValues[5],
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
            if ($('#heading-social-media').hasClass('d-none')) {
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
                        '_personAddress': personAddressArray,
                        '_personBoardOfDirectorRelation': personBoardOfDirectorRelationArray,
                        '_personCreditRating': personCreditRatingArray,
                        '_personBorrowingDetail': personBorrowingDetailArray,
                        '_personChronicDisease': personChronicDiseaseArray,
                        '_personContactDetail': contactDetailArray,
                        '_personCourtCase': personCourtCaseArray,
                        '_personFamilyDetail': personFamilyDetailArray,
                        '_personIncomeDetails': personAdditionalIncomeDetailArray,
                        '_personInsurance': personInsuranceDetailArray,
                        '_PersonSMSAlert': personSMSAlertArray,
                        '_personSocialMedia': personSocialMediaArray
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
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