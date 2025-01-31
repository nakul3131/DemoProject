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
    const MANDATORY = 'M';
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

    const ADDRESS_TYPE_DROPDOWN_LIST = $('#address-type-id').html();

    let personDropdownListDataForGuardian = '';
    let dropDownListItemCount = 0;

    let isValidDocumentBirthdate = true;
    let personKYCDetailDocumentPrmKey = 0;
    let personGSTReturnDocumentPrmKey = 0;
    let fileNameDocument = '';
    let localStoragePath = '';
    let isDbRecord = false;
    let isChangedPhoto = false;
    let editedDocumentId = '';
    let kycDocument = '';
    let isVerifyView = false;
    let documentID = '';
    let isAmendView = false;
    let isModifyView = false;
    let personDropdownListDataForFamily = ''; 

    let identityDocumentType = '';

    // @@@@@@@@@@ Data Table Related letible Declaration

    let tag = '';
    let myModal;
    let rowNum = 0;
    let selectedRowIndex;
    let row;
    let rowData;
    let checked;
    let columnValues;
    let maritalStatusId='';
    let occupationId ='';
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let minimum;
    let maximum;
    let minimumLength = 0;
    let maximumLength = 0;
    let arr = new Array();

    let previousSelectedMaritalStatusId = '';
    let previousSelectedOccupationId = '';
    let previousSelectedDocumentId = ''; 

    let fullname = '';

    // PersonAddress
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
    let note = '';
    let panCardNumber = '';
    let transNote = '';
    let reasonForModification = '';

    let personAddressPrmKey = 0;
    let editedAddressTypeId = '';
    let document_Type = '';
    let entryStatus = false;

    // guardian Person
    let guardianPersonInformationNumber = '';
    let guardianPersonInformationNumberText = '';
    let finalDropdownListArray = [];

    // PersonKYCDocument
    let kYCDateOfIssueDate = '';
    let kYCExpiryDate = '';
    let kYCDateOfRequestDate = '';
    let kYCDateOfExpectingSubmitDate = '';
    let kYCDateOfSubmitDate = '';
    let documentUploadStatusText;
    let document ='';
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
    let fileCaption;
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

    // M A I N     P A G E     I N P U T     V A L I D A T I O N

    // Create DataTables
    let addressDataTable = CreateDataTable('person-address');
    let personKycDataTable = CreateDataTable('kyc-document');
    let gstDataTable = CreateDataTable('gst-registration');

    SetPageLoadingDefaultValues();

    function AttachFileUploader()
    {
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

        // Document type determination
        switch (myId) {
            case 'kyc-file-uploader':
                docInput = 'KYC';
                break;
            case 'gst-file-uploader':
                docInput = 'GST';
                break;
            default:
                docInput = 'None';
        }

        // Hide the error message for the current uploader
        $('#' + myId + '-error').addClass('d-none');

        if (this.files.length > 0) {
            debugger;
            const uploadFile = this.files[0];
            // Upload File
            if (IsValidFile(docInput, uploadFile)) {
                let reader = new FileReader();

                reader.onload = function (e) {
                    // Update the correct image preview based on the document type
                    if (docInput === 'KYC') {
                        $('#kyc-file-uploader-image-preview').attr('src', e.target.result);
                    }
                    else if (docInput === 'GST') {
                        $('#gst-file-uploader-image-preview').attr('src', e.target.result);
                    }
                }

                reader.readAsDataURL(uploadFile);
            }
        } else
        {
            $('#' + myId + '-image-preview').attr('src', '');
        }
    });

    // Validate Uploaded File
    function IsValidFile(_inputSource, _uploadFile)
    {
        debugger;
        let result = true;
        let isUploadInLocalStorage = false;

        isChangedPhoto = true;

        if (_uploadFile)
        {
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
        }

        return result;
    }

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

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   Functions  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // It Return Human Age IN Years
    function CalculateAgeInYears() {
        if (isVerifyView === false)
        {
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
    }

    // Validate Document Number Based On Selected Document
    // Returns True On Valid Number Otherwise False
    function IsValidDocumentNumber()
    {
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
            //^[A-Z]{2}: Starts with two uppercase letters for the state code (e.g., "KA" for Karnataka, "MH" for Maharashtra). \d{2}: Followed by two digits for the RTO code.
            //\d{4}: Next four digits for the year of issuance. [A-Z0-9]{1,13}$: Followed by up to 13 alphanumeric characters for the unique identifier.
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

    // It Sets Unique Address Type Dropdown List Items Based On Address Type Data Table
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
    function SetDocumentBirthdate()
    {
        if (isVerifyView === false) {
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
        }
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
            if (document === PAN_CARD)
            {
                $('#kyc-document-number').attr('minlength', 10);
                $('#kyc-document-number').attr('maxlength', 10);
            }

            // Voting Card
            if (document === VOTER_CARD) {
                $('#kyc-document-number').attr('minlength', 10);
                $('#kyc-document-number').attr('maxlength', 10);
            }

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
    function SetGSTRegistrationDetail()
    {
        let rowCount = 0;
        let isPANCardExists = false;
        let tableRowCount = personKycDataTable.rows().count();

        $('#tbl-kyc-document > tbody > tr').each(function ()
        {
            debugger;
            currentRow = $(this).closest('tr');
            columnValues = (personKycDataTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnValues !== 'undefined' && columnValues !== null)
            {
                debugger;
                $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: columnValues[3], async: false }, function (data)
                {
                    rowCount = rowCount + 1;

                    if (data === PAN_CARD)
                    {
                        isPANCardExists = true;
                        $('#enable-gst-registration-details-input').removeClass('read-only');
                    }
                    else
                    {
                        // Execute On Only Last Record
                        if (tableRowCount === rowCount)
                        {
                            debugger;
                            if (isPANCardExists === false)
                            {
                                debugger;
                                ClearGSTRegistrationDetail();
                            }
                        }
                    }
                });
            }
            else
            {
                // Execute On Only Last Record
                if (tableRowCount === rowCount)
                {
                    debugger;
                    if (isPANCardExists === false)
                    {
                        debugger;
                        ClearGSTRegistrationDetail();
                    }
                }
            }
        });
    }

    // Clear GstRegistration Details
    function ClearGSTRegistrationDetail()
    {
        gstDataTable.clear().draw();
        $('#enable-gst-registration-details').prop('checked', false);
        $('#enable-gst-registration-details-input').addClass('read-only');
        $('#gst-registration-details-block').addClass('d-none');
        $('#gst-return-document-block').addClass('d-none');
        $('#gst-registration-accordion-error').addClass('d-none');
    }

    // It Does Not Return Any Value
    function SetMaritalStatusDetails() {
        if (isVerifyView === false) {
            maritalStatusId = $('#marital-status-id').val();

            if ($('#marital-status-id').prop('selectedIndex') > 0) {
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
    }

    // It Sets Ocuupation Details Input Visiblity Like Salaried, Self Employeed
    // It Does Not Return Any Value
    function SetOccupationDetails()
    {
        if (isVerifyView === false) {
            debugger;
            occupationId = $('#occupation-id').val();
            $.get('/PersonChildAction/GetSysNameOfOccupation', { _occupationId: occupationId, async: false }, function (data) {
                debugger;
                let isEmployee = $('#enable-employee').is(':checked') ? true : false;
                if (data === SALARIED) {
                    $('#is-employee-input').removeClass('d-none');
                    debugger;
                    if (isEmployee === false) {
                        debugger;
                        $('#employee-block').removeClass('d-none');

                    } else {
                        $('#employee-block').addClass('d-none');
                    }
                }
                else {
                    $('#employee-block').addClass('d-none');
                    $('#is-employee-input').addClass('d-none');
                }

                IsValidAdditionalDetailsAccordionInputs();
            });
        }
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
    function ValidateGSTNumber()
    {
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
        if (isVerifyView === false) {
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
    $('#guardian-full-name').focusout(function ()
    {
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
    $('#vip-rank').focusout(function ()
    {
        let vipRank = parseInt($('#vip-rank').val());

        // Check if the entered VIP rank is a valid number greater than 0
        if (isNaN(vipRank) === true || parseInt(vipRank) > 10)
        {
            $('#vip-rank').val(0);

            vipRank = 0;

            $('#vip-background-details').val('None');
            $('#trans-vip-background-details').val('None');

            $('.vip-background-details').addClass('d-none');
        }
        else
        {
            if (parseInt(vipRank) > 0)
            {
                $('.vip-background-details').removeClass('d-none');
            }            
            else
            {
                $('.vip-background-details').addClass('d-none');
            }
        }
    });

    // Marital Status
    $('#marital-status-id').focusout(function ()
    {
        maritalStatusId = $('#marital-status-id').val();

        if (previousSelectedMaritalStatusId !== maritalStatusId)
        {
            SetMaritalStatusDetails();

            $('#life-partner-name').val('');
            $('#trans-life-partner-name').val('');
            $('#life-partner-maiden-name').val('');
            $('#trans-life-partner-maiden-name').val('');
            $('#date-of-marriage').val('');

            previousSelectedMaritalStatusId = maritalStatusId;
        }
    });

    // Age Proof Submission Status Radio Button
    $('.age-proof-submission-status').focusout(function ()
    {
        IsValidGuardianAccordionInputs();
    });

    // BirthDate
    $('#birth-date').focusout(function ()
    {
        let birthdate = $('#birth-date').val();

        $('#marital-status-id').val('');

        if (birthdate !== '')
        {          
            SetDocumentBirthdate();
            $('#document-birth-date').val(new Date(birthdate));
            CalculateAgeInYears();
        }
        else
        {
            $('#guardian-detail-card').addClass('d-none');
            $('.guardian-details-input').val('');
            $('.guardian-details-radio-input').prop('checked', false);
        }
    });

    // Validate Document Birth Date
    $('#document-birth-date').focusout(function ()
    {
        debugger;
        ValidateDocumentBirthdate();
    });

    // Event listener for when occupation dropdown changes
    $('#occupation-id').focusout(function ()
    {
        debugger;
        let occupationId = $(this).val();

        if (previousSelectedOccupationId !== occupationId)
        {
            SetOccupationDetails();

            $('.employer-input').val('');
            $('#enable-employee').prop('checked', false);

            previousSelectedOccupationId = occupationId;
        }
    });

    // Additional detail EPF Validation
    $('#epf-number').focusout(function ()
    {
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
    $('#gst-registration-number').focusout(function ()
    {
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

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   VALIDATION FUNCTIONS FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

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

        //birth-city dropdown 
        if ($('#birth-city-id').prop('selectedIndex') < 1) {
            result = false;
        }

        //marital Status dropdown
        if ($('#marital-status-id').prop('selectedIndex') < 1) {
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

        //cast category dropdown
        if ($('#cast-category-id').prop('selectedIndex') < 1) {
            result = false;
        }

        //educational qualification dropdown
        if ($('#educational-qualification-id').prop('selectedIndex') < 1) {
            result = false;
        }

        // Check if 'vipRank' is a valid number and not empty
        if (isNaN(vipRank) === true || parseInt(vipRank) < 0 || parseInt(vipRank) > 11) {
            result = false;
        }

        //gender dropdown
        if ($('#gender-id').prop('selectedIndex') < 1) {
            result = false;
        }

        // Validate If Person Married
        if ($('#married-status-input').hasClass('d-none') === false)
        {
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

        // Validate Occupation 
        if ($('#occupation-id').prop('selectedIndex') > 1)
        {
            // Validate If Person Is Salaried
            if ($('#employee-block').hasClass('d-none') === false)
            {
                // Validate Name of Employer
                minimumLength = parseInt($('#employer-name').attr('minlength'));
                maximumLength = parseInt($('#employer-name').attr('maxlength'));

                if (parseInt(nameOfEmployer.length) < parseInt(minimumLength) || parseInt(nameOfEmployer.length) > parseInt(maximumLength))
                {
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
        else {
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

        if (result){
            $('#person-additional-details-accordion-error').addClass('d-none');
        }
        else {
            $('#person-additional-details-accordion-error').removeClass('d-none');
        }
            
        return result;
    }

    // 3.Guardian Details Accordion Input Validation
    function IsValidGuardianAccordionInputs()
    {
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
            if ($('#guardian-pin-input').hasClass('d-none') === false)
            {
                if (guardianPersonInformationNumber !== '')
                {
                    fullName = 'None';
                    transFullName = 'None';
                    fullAddress = 'None';
                    transFullAddress = 'None';
                }
                else
                {
                    fullName = '';
                    transFullName = '';
                    fullAddress = '';
                    transFullAddress = '';

                    result = false;
                }
            }
            else
            {
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
        if (ValidateGSTNumber() === false)
        {
            result = false;
        }

        //Registration Date
        if (IsValidInputDate('#registrations-date') === false) {
            result = false;
        }

        if (result){
            $('#gst-registration-details-accordion-error').addClass('d-none');
        }
        else {
            $('#gst-registration-details-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// #################   Person  Address Detail - DataTable Code 

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

        if (isChecked)
        {
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
            if (confirm('Are You Sure To Delete Selected Record?')) {
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
        addressDataTable.column(20).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person KYC Document - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-kyc-document-dt').click(function (event)
    {
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
    $('#btn-edit-kyc-document-dt').click(function ()
    {
        debugger;
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('kyc-document', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
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

            $('#file-caption-kyc', myModal).val(columnValues[19]);
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
        if (IsValidKycDocumentModal())
        {
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
    $('#btn-delete-kyc-document-dt').click(function (event)
    {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked)
        {
            if (confirm('Are You Sure To Delete Selected Record?'))
            {
                if ($('#tbl-kyc-document tbody input[type="checkbox"]:checked').each(function ()
                {
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
    function IsValidKycDocumentModal()
    {
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
        fileCaption = $('#file-caption-kyc').val();
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

        if ($('#kyc-document-id').prop('selectedIndex') > 0)
        {
            if (IsValidDocumentNumber() === false)
            {
                result = false;
                $('#kyc-document-number-error1').removeClass('d-none');
            }
        }
        else
        {
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
        maximumLength = parseInt($('#file-caption-kyc').attr('maxlength'));

        if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#file-caption-kyc-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsKycDocumentDataTable() {
        personKycDataTable.column(1).visible(false);
        personKycDataTable.column(3).visible(false);
        personKycDataTable.column(15).visible(false);
        personKycDataTable.column(22).visible(false);
        personKycDataTable.column(23).visible(false);
        personKycDataTable.column(24).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person GST Registration - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-gst-registration-dt').click(function (event) 
    {
        const today = new Date();
        const applicableFromDate = new Date($('#applicable-from').val());

        let maxOldAllowableAssesmentYear = today.getFullYear() - 5;
        
        if (parseInt(maxOldAllowableAssesmentYear) < applicableFromDate.getFullYear())
        {
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
        if (isChecked)
        {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#gst-registration-modal').modal();

            columnValues = $('#btn-edit-gst-registration-dt').data('rowindex');

            $('#assessment-year', myModal).val(columnValues[1]);
            $('#tax-amount', myModal).val(columnValues[2]);

            $('#file-caption-gst', myModal).val(columnValues[5]);
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
            if (confirm('Are You Sure To Delete Selected Record?'))
            {
                if ($('#tbl-gst-registration tbody input[type="checkbox"]:checked').each(function ()
                {
                    gstDataTable.row($('#tbl-gst-registration tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-gst-registration-dt').data('rowindex');
                    EnableNewOperation('gst-registration');

                    $('#select-all-gst-registration').prop('checked', false);

                    if (!gstDataTable.data().any())
                    {
                        $('#gst-registration-accordion-error').removeClass('d-none');
                        $('#enable-gst-registration-details').prop('checked', false);
                        $('#enable-gst-registration-details-input').addClass('read-only');
                    }                        
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
        fileCaption = $('#file-caption-gst').val();
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
        maximumLength = parseInt($('#file-caption-gst').attr('maxlength'));

        if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#file-caption-gst-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsGSTDataTable() {
        gstDataTable.column(8).visible(false);
        gstDataTable.column(9).visible(false);
        gstDataTable.column(10).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Set Page Loading Default Values @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    function SetPageLoadingDefaultValues()
    {
        debugger;
        if (($('#kyc-document-upload').val()) === MANDATORY)
        {
            $('#kyc-file-uploader').attr('required');
            $('#file-caption-kyc').attr('required');

            $('#kyc-file-uploader').addClass('mandatory-mark');
            $('#file-caption-kyc').addClass('mandatory-mark');
        }
        else
        {
            $('#kyc-file-uploader').removeAttr('required');
            $('#file-caption-kyc').removeAttr('required');

            $('#kyc-file-uploader').removeClass('mandatory-mark');
            $('#file-caption-kyc').removeClass('mandatory-mark');
        }

        // Disable Events On Modify View
        if ($('#modify-view').length > 0)
            isModifyView = true;

        // Disable Events On Verify View
        if ($('#verify-view').length > 0)
            isVerifyView = true;

        // Disable Events On Amend View
        if ($('#amend-view').length > 0)
            isAmendView = true;

        // Check Whether Element Exist OR Not - Applicable For Only Amend
        if ($('#guardian-pin1').val() > 0)
        {
            debugger;
            $('#guardian-detail').addClass('d-none');
            $('#guardian-pin-input').removeClass('d-none');

            let guardianIdValueOnAmend = $('#guardian-pin-value').attr('class').toString().replace('d-none', '');

            let selectedGuardianId = $('#guardian-pin1').val();

            guardianPersonInformationNumber = selectedGuardianId;

            $('#guardian-pin').val(guardianIdValueOnAmend);
        }
        else
        {
            $('#guardian-pin-input').addClass('d-none');
            $('#guardian-detail').removeClass('d-none');
        }

        //'vip-rank' and convert it to an integer
        let vipRank = parseInt($('#vip-rank').val());

        if (parseInt(vipRank) === 0)
        {
            $('.vip-background-details').addClass('d-none');
        }
        else
        {
            $('.vip-background-details').removeClass('d-none');
        }

        SetDocumentBirthdate();
        CalculateAgeInYears();
        SetMaritalStatusDetails();
        SetOccupationDetails();
        SetGSTRegistrationDetail();

        // Call the function GST for amend and verify 
        if (isVerifyView === true || isAmendView === true) //|| isModifyView === true)
        {
            let registrationNumber = $('#gst-registration-number').val(); // Get the value correctly

            if (registrationNumber.length === 15) {
                // Check the checkbox
                $('#enable-gst-registration-details').prop('checked', true);
            } else {
                // Uncheck the checkbox
                $('#enable-gst-registration-details').prop('checked', false);
            }
        }

        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();

        // Get Person Dropdown For Guardian
        $.get('/DynamicDropdownList/GetPersonDropdownListForGuardian', function (data) {
            personDropdownListDataForGuardian = data;
        });
    }

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@
    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;

        //if ($('form').valid() 
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let personAddressArray = new Array();

            let personFormData
            personFormData = new FormData($("#form")[0]);

            // Accordion 4 - Person Additional Detail Validation, If Enable
            if (IsValidAdditionalDetailsAccordionInputs() === false) {
                isValidAllInputs = false;
            }

            // Accordion 5 - Guardian Person Validation, If Enable
            if ($('#guardian-detail-card').hasClass('d-none') === false && $('#guardian-detail-card').length > 0) {
                if (IsValidGuardianAccordionInputs() === false)
                    isValidAllInputs = false;
            }

            // Accordion 6 - Person gst-registration Validation, If Enable
            if ($('#enable-gst-registration-details').is(':checked')) {
                if (IsValidGstRegistrationAccordionInputs() === false)
                    isValidAllInputs = false;
            }

            // Create Array For person address detail Table To Pass Data
            if ($('#heading-address-details').hasClass('d-none') === false) {

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

            // Create Array For person address detail Table To Pass Data
            if ($('#heading-kyc-document').hasClass('d-none') === false) {
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

            // Call Controller Save Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: saveDataTableURL,
                    type: 'POST',
                    async: false,
                    data: {
                        '_address': personAddressArray,
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                        PersonImagesDataTable();
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Person Master DataTable!!! Error Message - ' + error.toString());
                    }
                })
            }

            else {
                alert('Oops, Something Went Wrong!, Please Provide Valid Data........');
                event.preventDefault();
            }

            //PersonAgricultureAsset
            function PersonImagesDataTable()
            {
                $.ajax({
                    url: personImageDataTableURL,
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