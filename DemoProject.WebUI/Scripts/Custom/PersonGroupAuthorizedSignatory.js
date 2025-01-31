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
    const MANDATORY = 'M';

    // @@@@@@@@@@ Data Table Related letible Declaration
    debugger;
    let result = true;
    let isDbRecord = false;
    let isChangedPhoto = false;
    let isValidRegistrationNumber = true;
    let tag = '';
    let myModal;
    let selectedRowIndex;
    let row;
    let rowData;
    let checked;
    let columnValues;
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let note = '';
    let transNote = '';
    let personDropdownListDataForFamily = '';
    let maximumLength = 0;
    let arr = new Array();
    // Array
    let finalDropdownListArray = [];

    // Count
    let dropDownListItemCount = 0;

    // Person Group Authorized Signatory
    let fileUploaderInput;
    let fileObj;
    let filePath = '';
    let fileUploader;
    let fileUploaderInputHtml = '';
    let imageTagHtml = '';
    let fileCaption = '';
    let fileUploaderId = '';
    let fileId = '';
    let counter = 100;
    let files;
    let selectedDocumentObject;
    let fileNameDocument = '';
    let personGroupAuthorizedSignatoryPrmKey = 0;
    let localStoragePath = '';

    //Person Group Authorized Signatory
    let personInformationNumber = '';
    let personInformationNumberText = '';
    let designationId = '';
    let designationIdText = '';
    let fullNameOfAuthorizedPerson = '';
    let reasonForModification = '';
    let transfullNameOfAuthorizedPerson = '';
    let authorizedPersonAddressDetail = '';
    let transAuthorizedPersonAddressDetail = '';
    let authorizedPersonContactDetail = '';
    let transAuthorizedPersonContactDetail = '';
    let isAuthorizedSignatory = false;

    // M A I N     P A G E     I N P U T     V A L I D A T I O N 

    // Create DataTables
    let authorizedSignatoryDataTable = CreateDataTable('authorized-signatory');

    // *************** C A L L   P A G E   L O A D I N G   D E F A U L T   V A L U E S ***************

    SetPageLoadingDefaultValues();

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
            $('#sign-file-uploader').val('');
            $('.modal-input-img-preview').attr('src', '');
            $('#sign-file-uploader-error').addClass('d-none');
            $('#file-caption-sign-error').addClass('d-none');
        }
    }

    // Validate Unique Business Registration Number
    $('#business-registration-number').focusout(function (event) {
        debugger;
        let businessRegistrationNumber = $('#business-registration-number').val();

        $.get('/PersonChildAction/IsUniqueBusinessRegistrationNumber', { _businessRegistrationNumber: businessRegistrationNumber, async: false }, function (data, textStatus, jqXHR) {
            if (data) {
                isValidRegistrationNumber = true;
                $('#business-registration-number-error').addClass('d-none');
            }
            else {
                isValidRegistrationNumber = false;
                $('#business-registration-number-error').removeClass('d-none');
            }
        });
    });

    //Validation For Establishment Date Focusout Event
    $('#date-of-establishment').focusout(function () {
        debugger;
        ValidateEstablishmentDate();
    });

    // Function On Date Of Establishment
    function ValidateEstablishmentDate() {
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

    //Function On Photo Validation
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
        let myId = $('.doc-upload').attr('id');

        // Document
        switch (myId) {
            case 'sign-file-uploader':
                docInput = 'Sign';
                break;
            default:
                docInput = 'None';
        }

        // $('#document-file-uploader-error').addClass('d-none');
        $('#' + myId + '-error').addClass('d-none');

        if (this.files.length > 0) {
            const uploadFile = this.files[0];
            $('#file-caption-sign').val('');

            // Upload File
            if (IsValidFile(docInput, uploadFile)) {
                let reader = new FileReader();

                reader.onload = function (e) {
                    $('#sign-file-uploader-image-preview').attr('src', e.target.result);
                }

                reader.readAsDataURL(uploadFile);
            }
        }
    });

    // Validate Uploaded File
    function IsValidFile(_inputSource, _uploadFile) {
        let result = true;
        let isUploadInLocalStorage = false;

        isChangedPhoto = true;

        if (_uploadFile) {
            // .pop(): - Removes and returns the last element from the array, which is the file extension in this case.
            const fileName = _uploadFile.name;
            const fileSize = Math.round(_uploadFile.size / 1024);
            const fileExtension = fileName.split('.').pop().toLowerCase();

            let validDocumentFileExtensions = '';
            let maxDocumentFileSize = 0;

            let uploaderId = _inputSource.replace('Asset', '').toLowerCase();

            if (_inputSource === 'Sign') {
                uploaderId = 'sign';
            }

            isUploadInLocalStorage = personInformationParameterViewModel[`Enable${_inputSource}DocumentUploadInLocalStorage`];

            // Get File Formats And File Size By Storage
            if (isUploadInLocalStorage === true) {
                validDocumentFileExtensions = personInformationParameterViewModel[`${_inputSource}DocumentAllowedFileFormatsForLocalStorage`].toLowerCase().replace('.', '');;
                maxDocumentFileSize = personInformationParameterViewModel[`MaximumFileSizeFor${_inputSource}DocumentUploadInLocalStorage`];
            }
            else {
                validDocumentFileExtensions = personInformationParameterViewModel[`${_inputSource}DocumentAllowedFileFormatsForDb`].toLowerCase().replace('.', '');;
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
        personInformationNumber = '';
        personInformationNumberText = '';
        $('#authorized-person-information-number').val('');
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
    $('#authorized-person-information-number').focusout(function () {
        debugger;
        $(this).val($(this).val().trim());

        let familyPersonInformationNumber = $('#authorized-person-information-number').val();

        // Check if the value is not from the autocomplete list
        let isInAutocomplete = false;
        $('#authorized-person-information-number').autocomplete("widget").children("li").each(function () {
            if ($(this).text() === familyPersonInformationNumber) {
                isInAutocomplete = true;
                return false;
            }
        });

        if (!isInAutocomplete) {
            // Clear the input if the typed value is not in the autocomplete list
            $('#person-information-numbers').val('');
            $('#authorized-person-information-number-input').val('None');
            $('#authorized-member-name').removeClass('d-none');
        } else if ((familyPersonInformationNumber !== 'None') && (familyPersonInformationNumber.length > 3)) {
            $('#authorized-member-name').addClass('d-none');
            $('#authorized-member-name').val('None')
        }
    });

    let familyPersonInfo = $('#authorized-person-information-number-input').val();
    if (familyPersonInfo !== '') {
        $('#authorized-person-information-number').val(familyPersonInfo);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Group Authorized Signatory - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-authorized-signatory-dt').click(function () {
        debugger;

        isDbRecord = false;

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

        SetModalTitle('authorized-signatory', 'Add');

        personGroupAuthorizedSignatoryPrmKey = 0;
        localStoragePath = 'None';
        fileNameDocument = 'None';
    });

    // DataTable Edit Button 
    $('#btn-edit-authorized-signatory-dt').click(function () {
        debugger;
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('authorized-signatory', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#authorized-signatory-modal').modal();

            columnValues = $('#btn-edit-authorized-signatory-dt').data('rowindex');

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
            $('#file-caption-sign', myModal).val(columnValues[14]);
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
                $('#sign-file-uploader').val('');
                $('.modal-input-img-preview').attr('src', '');
                $('#sign-file-uploader-error').addClass('d-none');
                $('#file-caption-sign-error').addClass('d-none');
                fileNameDocument = 'None';
                localStoragePath = 'None';
            }

            fileUploader = $('#' + $(columnValues[12]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'sign-file-uploader';

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

            $('#sign-file-uploader-image-preview').attr('src', filePath);

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
                if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
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
                if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
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

            if (confirm('Are You Sure To Delete Selected Record?')) {
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
        fileUploaderId = 'data-table-sign-file-uploader' + counter;
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
        fileCaption = $('#file-caption-sign').val();
        reasonForModification = $('#reason-for-modification').val();
        note = $('#note-board-of-director-authorized').val();
        transNote = $('#trans-note-board-of-director-authorized').val();

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#sign-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        fileUploader = $('#sign-file-uploader').get(0);

        // Toggle the authorized signatory block visibility based on checkbox status
        if (isAuthorizedSignatory) {
            $('#authorized-signatory-block').removeClass('d-none');
        } else {
            $('#authorized-signatory-block').addClass('d-none');
            $('#sign-file-uploader').val('');
            $('.modal-input-img-preview').attr('src', '');
            $('#sign-file-uploader-error').addClass('d-none');
            $('#file-caption-sign-error').addClass('d-none');
            fileNameDocument = 'None';
            localStoragePath = 'None';
        }

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (transNote === '') {
            transNote = 'None';
        }

        if (reasonForModification === '') {
            reasonForModification = 'None';
        }

        if (fileCaption === '') {
            fileCaption = 'None';
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
                    if (isDbRecord === false || isChangedPhoto === true) {
                        result = false;
                        $('#sign-file-uploader-error').removeClass('d-none');
                    }

                }
                else {
                    let photoSrc = $('#sign-file-uploader-image-preview').attr('src');

                    // Don't Change, It Is Refereed For AttachFileUploader()
                    if (photoSrc.toString().length < 2) {
                        fileCaption = 'NotApplicable';
                        localStoragePath = 'None';

                    }
                }
            }

            // file Caption
            maximumLength = parseInt($('#file-caption-sign').attr('maxlength'));

            if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
                result = false;
                $('#file-caption-sign-error').removeClass('d-none');
                $('#file-caption-sign-error').removeClass('d-none');
            }
        }
        else {
            filePath = 'None';
            fileCaption = 'NotApplicable';
            fileNameDocument = 'None';
            localStoragePath = 'None';
            $('.modal-input-img-preview').attr('src', '');
            $('#sign-file-uploader-error').addClass('d-none');
            $('#file-caption-sign-error').addClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsAuthorizedSignatoryDataTable() {
        authorizedSignatoryDataTable.column(1).visible(false);
        authorizedSignatoryDataTable.column(9).visible(false);
        authorizedSignatoryDataTable.column(18).visible(false);
        authorizedSignatoryDataTable.column(19).visible(false);
        authorizedSignatoryDataTable.column(20).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Set Page Loading Default Values @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    function SetPageLoadingDefaultValues() {
        debugger;
        if (($('#sign-document-upload').val()) === MANDATORY) {
                $('#sign-file-uploader').addClass('mandatory-mark');
                $('#sign-file-uploader').attr('required');
                $('#file-caption-sign').attr('required');
                $('#file-caption-sign').addClass('mandatory-mark');
            } else {
                $('#sign-file-uploader').removeClass('mandatory-mark');
                $('#file-caption-sign').removeClass('mandatory-mark');
                $('#sign-file-uploader').removeAttr('required');
                $('#file-caption-sign').removeAttr('required');
            }

        //Calling Function Enable Authorized Signatory
        EnableAuthorizedSignatory();

        // Get Person Dropdown For Nominee
        $.get('/DynamicDropdownList/GetPersonDropdownListForNominee', function (data) {
            debugger;
            personDropdownListDataForFamily = data;
        });

        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();

        //Validation Date 
        ValidateEstablishmentDate();

    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;
        let personFormData;

        personFormData = new FormData($("#form")[0]);

        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // Validate Scheme Name
            if (!isValidRegistrationNumber) {
                isValidAllInputs = false;
                $('#business-registration-number-error').removeClass('d-none');
            }
            else
                $('#business-registration-number-error').addClass('d-none');


            // Create Array For person board of director Authorized Table To Pass Data
            if (!$('#heading-person-group-authorized-signatory').hasClass('d-none')) {
                if (authorizedSignatoryDataTable.data().any()) {
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


            debugger;
            // Call Controller Save Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: saveDataTableURL,
                    type: 'POST',
                    async: false,
                    data: personFormData,
                    contentType: false,
                    processData: false,
                    cache: false,
                    //ContentType: "application/json; charset=utf-8",
                    dataType: 'JSON',
                    enctype: 'multipart/form-data',

                    success: function (data) {
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
        }

        else {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }
    });
});