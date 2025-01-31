'use strict';
$(document).ready(function () {
    debugger;
    const MANDATORY = 'M';
    debugger;
    // @@@@@@@@@@ Data Table Related Varible Declaration
    let result = true;
    let isDbRecord = false;
    let isChangedPhoto = false;
    let tag = '';
    let id = '';
    let myModal;
    let rowNum = 0;
    let selectedRowIndex;
    let row;
    let note;
    let rowData;
    let checked;
    let columnValues;
    let isValidEmployeeCode = true;

    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let minimum = 0;
    let maximum = 0;

    let arr = new Array();

    // Document
    let documentId;
    let documentText = '';
    let editedDocumentId = '';

    // Document
    let fileUploaderInput;
    let fileObj;
    let filePath = '';
    let fileUploader;
    let fileUploaderInputHtml = '';
    let imageTagHtml = '';
    let fileCaption = '';
    let fileUploaderId = '';
    let fileId = '';
    // ****** New Changes
    let counter = 100;
    let files;
    let employeeDocumentPrmKey;
    let fileNameDocument = '';
    let localStoragePath;

    //Salary Break up
    let salaryBreakupId = '';
    let salaryBreakupText = '';
    let breakupValue = 0;
    let isPercentage = 0;
    let activationDate = '';
    let expiryDate = '';
    let closeDate = '';
    let reasonForModification;

    const DOCUMENT_TYPE_DROPDOWN_LIST = $('#document-id').html();

    // M A I N     P A G E     I N P U T     V A L I D A T I O N 
    let url = window.location.href;
    let index = url.lastIndexOf('/') + 1;
    let filenameWithExtension = url.substr(index);
    let filename = filenameWithExtension.split('?')[0]; // <-- added this line
    let winname = filename;


    // CreateDataTable
    let employeeSalaryStructureDataTable = CreateDataTable('employee-salary-structure');
    let documentDataTable = CreateDataTable('document');

    // *************** C A L L   P A G E   L O A D I N G   D E F A U L T   V A L U E S ***************

    SetPageLoadingDefaultValues();

    //Set All Required Default Values On Page Loading
    function SetPageLoadingDefaultValues() {
        debugger;

        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();

        SetDateOfJoiningLimits();


        //Validation Photo On MANDATORY
        if (($('#photo-document-upload').val()) === MANDATORY) {
            $('#photo-file-uploader').addClass('mandatory-mark');
        }
        else {
            $('#photo-file-uploader').removeClass('mandatory-mark');
        }

        //Validation Photo Document On MANDATORY
        if (($('#kyc-document-upload').val()) === MANDATORY) {
            $('#document-photo-file-uploader').addClass('mandatory-mark');
        }
        else {
            $('#document-photo-file-uploader').removeClass('mandatory-mark');
        }

    }

    // Document File Uploader
    $('.doc-upload').change(function () {
        debugger;
        let docInput = '';
        let myId = $(this).attr('id');

        // Document type determination
        switch (myId) {
            case 'photo-file-uploader':
                docInput = 'Photo';
                break;
            case 'document-photo-file-uploader':
                docInput = 'KYC';
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
            if (IsValidFile(docInput, uploadFile, myId)) {
                let reader = new FileReader();

                reader.onload = function (e) {
                    // Update the correct image preview based on the document type
                    if (docInput === 'Photo') {
                        $('#photo-file-uploader-image-preview').attr('src', e.target.result);
                    } else if (docInput === 'KYC') {
                        $('#document-photo-file-uploader-image-preview').attr('src', e.target.result);
                    }
                }

                reader.readAsDataURL(uploadFile);
            }
        } else {
            // Clear image preview if no file is selected
            if (docInput === 'Photo') {
                $('#photo-file-uploader-image-preview').attr('src', '');
            } else if (docInput === 'KYC') {
                $('#document-photo-file-uploader-image-preview').attr('src', '');
            }
        }
    });


    // Validate Uploaded File
    function IsValidFile(_inputSource, _uploadFile, myId) {
        debugger;
        let result = true;
        let isUploadInLocalStorage = false;

        isChangedPhoto = true;

        if (_uploadFile) {
            const fileName = _uploadFile.name;
            const fileSize = Math.round(_uploadFile.size / 1024);
            const fileExtension = fileName.split('.').pop().toLowerCase();

            let validDocumentFileExtensions = '';
            let maxDocumentFileSize = 0;

            // Document
            if (_inputSource === 'Document') {
                validDocumentFileExtensions = (selectedDocumentObject[0].EnableDocumentUploadInDb ? selectedDocumentObject[0].DocumentAllowedFileFormatsForDb : selectedDocumentObject[0].DocumentAllowedFileFormatsForLocalStorage).toLowerCase().replace('.', '');
                maxDocumentFileSize = selectedDocumentObject[0].EnableDocumentUploadInDb ? selectedDocumentObject[0].MaximumFileSizeForDocumentUploadInDb : selectedDocumentObject[0].MaximumFileSizeForDocumentUploadInLocalStorage;

                if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize)) {
                    $('#' + myId + '-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + ' Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#' + myId + '-error').removeClass('d-none');

                    // Clear the image preview and input
                    $('#' + myId + '-image-preview').attr('src', '#');
                    $('#' + myId).val('');

                    result = false;
                }
            } else {
                let uploaderId = _inputSource.replace('Asset', '').toLowerCase();
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
                    $('#' + myId + '-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + ' Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#' + myId + '-error').removeClass('d-none');

                    // Clear the image preview and input
                    $('#' + myId + '-image-preview').attr('src', '#');
                    $('#' + myId).val('');

                    result = false;
                }
            }
        }

        return result;
    }

    //Validate Unique Name of Employee Code
    $('#employee-code').focusout(function (event) {
        debugger;
        let employeeCode = $('#employee-code').val();

        $.get('/AccountChildAction/IsUniqueEmployeeCode', { _employeeCode: employeeCode, async: false }, function (data, textStatus, jqXHR) {
            if (data && employeeCode != '') {
                isValidEmployeeCode = true;
                $('#employee-code-error').addClass('d-none');
            }
            else {
                isValidEmployeeCode = false;
                $('#employee-code-error').removeClass('d-none');
            }
        });
    });

    // @@@@@@@@@@@@@@@@@@@@@@@@@@  FOCUSOUT FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@


    // Trigger Validation When The Employee Department Section Is Opened
    $('#collapse-employee-department').on('shown.bs.collapse', () => {
        IsValidEmployeeDepartmentAccordionInputs();
    });

    // Employee Department Accordion Input Validation
    $('.department-input').focusout(function () {
        IsValidEmployeeDepartmentAccordionInputs();
    });

    // Trigger Validation When The Employee Designation Section Is Opened
    $('#collapse-employee-designation').on('shown.bs.collapse', () => {
        IsValidDesignationDepartmentAccordionInputs();
    });

    // Employee Designation Accordion Input Validation
    $('.designation-input').focusout(function () {
        IsValidDesignationDepartmentAccordionInputs();
    });

    // Trigger Validation When The Employee Designation Section Is Opened
    $('#collapse-employee-performance-rating').on('shown.bs.collapse', () => {
        IsValidPerformanceRatingAccordionInputs();
    });

    // Performance Rating Input Designation Accordion Input Validation
    $('.performance-rating-input').focusout(function () {
        IsValidPerformanceRatingAccordionInputs();
    });

    // Trigger Validation When The Employee Working Schedule Section Is Opened
    $('#collapse-employee-working-schedule').on('shown.bs.collapse', () => {
        IsValidEmployeeWorkingScheduleAccordionInputs();
    });

    // Employee Working Schedule Input Accordion Input Validation
    $('.working-schedule-input').focusout(function () {
        IsValidEmployeeWorkingScheduleAccordionInputs();
    });

    // Trigger Validation When The Employee Working Schedule Section Is Opened
    $('#collapse-employee-detail').on('shown.bs.collapse', () => {
        IsValidEmployeeDetailAccordionInputs();
    });

    // Employee Working Schedule Input Accordion Input Validation
    $('.employee-detail-input').focusout(function () {
        IsValidEmployeeDetailAccordionInputs();
    });

    // Employee Working Schedule Input Accordion Input Validation
    $('.photo-input').focusout(function () {
        debugger
        ValidatePhotoPreview();
    });

    // Function to set min and max dates for the Date of Joining field
    function SetDateOfJoiningLimits() {
        debugger
        // Fetch the cooperative registration date from the server
        $.get('/EnterpriseChildAction/GetCoOperativeRegistrationDate', { async: false }, function (data, textStatus, jqXHR) {
            debugger
            // Convert JSON date format to JavaScript Date object
            let jsonDate = data;
            let registrationDate = new Date(parseInt(jsonDate.match(/\d+/)[0], 10));
            let currentDate = new Date();

            // Set the min and max attributes of the date input field
            $('#date-of-joining').attr('min', GetInputDateFormat(registrationDate));
            $('#date-of-joining').attr('max', GetInputDateFormat(currentDate));
        });
    }

    // Call the function on focusout event of the date input field
    $('#date-of-joining').focusout(function () {
        debugger
        $('#date-of-leaving').val('');
        $('#date-of-training-started').val('');
        $('#date-of-training-ended').val('');
        $('#date-of-probation').val('');
        $('#date-of-confirmation').val('');

        SetDateOfJoiningLimits();
    });

    // Call the function on focusout event of the date input field
    $('#date-of-joining').click(function () {
        debugger
        $('#date-of-leaving').val('');
        $('#date-of-training-started').val('');
        $('#date-of-training-ended').val('');
        $('#date-of-probation').val('');
        $('#date-of-confirmation').val('');

        SetDateOfJoiningLimits();
    });

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   FUNCTIONS FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // 1. Employee Department Accordion Input Validation
    function IsValidEmployeeDepartmentAccordionInputs() {
        debugger;
        let result = true;

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-department');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-department');

        // Check if isValidActivationDate is a valid date string
        if (isValidActivationDate === false || isValidActivationDate === '') {
            result = false;
        }

        // Check If Values Is Not Empty
        if (isValidExpiryDate === '')
            result = false;

         // Check If Values Is Not Empty
         if ($('#department-type').prop('selectedIndex') < 1) {
            result = false;
        }

        if (result)
            $('#employee-department-accordion-error').addClass('d-none');
        else
            $('#employee-department-accordion-error').removeClass('d-none');

        return result;
    }

    // 2. Employee Designation Accordion Input Validation
    function IsValidDesignationDepartmentAccordionInputs() {

        let result = true;

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-designation');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-designation');

        // Check if isValidActivationDate is a valid date string
        if (isValidActivationDate === false || isValidActivationDate === '') {
            result = false;
        }

        // Check If Values Is Not Empty
        if (isValidExpiryDate === '')
            result = false;

        // Check If Values Is Not Empty
        if ($('#designation-type').prop('selectedIndex') < 1) {
            result = false;
        }

        if (result)
            $('#employee-designation-accordion-error').addClass('d-none');
        else
            $('#employee-designation-accordion-error').removeClass('d-none');

        return result;
    }

    // 3. Performance Rating Accordion Input Validation
    function IsValidPerformanceRatingAccordionInputs() {

        let result = true;
        let employeeRating = parseFloat($('#employee-rating').val());

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-performance-rating');

        // Check if isValidActivationDate is a valid date string
        if (isValidActivationDate === false || isValidActivationDate === '') {
            result = false;
        }

        // Default For Tenure 
        if (isNaN(employeeRating) === false) {
            minimum = parseInt($('#employee-rating').attr('min'));
            maximum = parseInt($('#employee-rating').attr('max'));

            if (parseInt(employeeRating) < parseInt(minimum) || parseInt(employeeRating) > parseInt(maximum))
                result = false;
        }
        else
            result = false;


        if (result)
            $('#employee-performance-rating-accordion-error').addClass('d-none');
        else
            $('#employee-performance-rating-accordion-error').removeClass('d-none');

        return result;
    }

    // 4. Employee Working Schedule Accordion Input Validation
    function IsValidEmployeeWorkingScheduleAccordionInputs() {

        let result = true;

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-working-schedule');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-working-schedule');

        // Check if isValidActivationDate is a valid date string
        if (isValidActivationDate === false || isValidActivationDate === '') {
            result = false;
        }

        // Check If Values Is Not Empty
        if (isValidExpiryDate === '')
            result = false;

        // Check If Values Is Not Empty
        if ($('#working-schedule-type').prop('selectedIndex') < 1) {
            result = false;
        }

        if (result)
            $('#employee-working-schedule-accordion-error').addClass('d-none');
        else
            $('#employee-working-schedule-accordion-error').removeClass('d-none');

        return result;
    }

    // 5. EEmployee Detail Accordion Input Validation
    function IsValidEmployeeDetailAccordionInputs() {
        debugger;
        let result = true;

        let dateOfJoining = $('#date-of-joining').val();
        let dateOfProbation = $('#date-of-probation').val();
        let dateOfConfirmation = $('#date-of-confirmation').val();
        let dateOfStartedDate = $('#date-of-training-started').val();
        let dateOfTrainingEnded = $('#date-of-training-ended').val();
        let trainingStartedDate = new Date($('#date-of-training-started').val());

        // Convert date strings to Date objects
        let currentDate = new Date();
        let minDate = new Date(dateOfJoining);
        let maxDate = new Date(dateOfJoining);
        let maxTrainingStartedDate = new Date(dateOfJoining);
        let minTrainingStartedDate = new Date(dateOfJoining);
        let minTrainingDate = new Date(dateOfStartedDate);
        let maxTrainingDate = new Date(dateOfStartedDate);
        let minProbationDate = new Date(dateOfProbation);
        let maxProbationDate = new Date(dateOfProbation);
        let confirmationDate = new Date(dateOfConfirmation);

        //Validation For Date Of Probation
        minDate.setFullYear(minDate.getFullYear());
        maxDate.setFullYear(maxDate.getFullYear() + 10);

        // Set the max attribute
        $('#date-of-probation').attr('min', GetInputDateFormat(minDate));
        $('#date-of-probation').attr('max', GetInputDateFormat(maxDate));

        //Validation For Date Of Training Started
        maxTrainingStartedDate.setFullYear(maxTrainingStartedDate.getFullYear() + 2);
        minTrainingStartedDate.setFullYear(minTrainingStartedDate.getFullYear());

        // Set the max attribute
        $('#date-of-training-started').attr('min', GetInputDateFormat(minTrainingStartedDate));
        $('#date-of-training-started').attr('max', GetInputDateFormat(maxTrainingStartedDate));

        //Validation For Date Of Training Ended
        if (!isNaN(minTrainingDate) || !isNaN(maxTrainingDate)) {
            maxTrainingDate.setFullYear(maxTrainingDate.getFullYear() + 1);
            minTrainingDate.setFullYear(minTrainingDate.getFullYear());

            // Set the max attribute
            $('#date-of-training-ended').attr('min', GetInputDateFormat(minTrainingDate));
            $('#date-of-training-ended').attr('max', GetInputDateFormat(maxTrainingDate));
        }

        //Validation For Date Of confirmation
        if (!isNaN(minProbationDate) || !isNaN(maxProbationDate)) {
            // Add 10 years to the probation date
            minProbationDate.setFullYear(minProbationDate.getFullYear());
            maxProbationDate.setFullYear(maxProbationDate.getFullYear() + 10);

            // Set the max attribute
            $('#date-of-confirmation').attr('min', GetInputDateFormat(minProbationDate));
            $('#date-of-confirmation').attr('max', GetInputDateFormat(maxProbationDate));
        }

        // Reset the date if conditions are not met
        if (dateOfStartedDate == '' || dateOfStartedDate < minTrainingStartedDate || dateOfStartedDate > maxTrainingStartedDate) {
            result = false;
        }

        // Reset the date if conditions are not met
        if (dateOfTrainingEnded == '' || dateOfTrainingEnded < minTrainingDate || dateOfTrainingEnded > maxTrainingDate) {
            result = false;
        }

        // Check if dateOfProbation is a valid date string
        if (dateOfProbation === '' || dateOfProbation < minDate || dateOfProbation > maxDate) {
            result = false;
        }

        //// Validate Date Of Confirmation Within Probation Date Range
        if (dateOfConfirmation === '' || dateOfConfirmation < minProbationDate || dateOfConfirmation > maxProbationDate) {
            result = false;

        }

        // Set the max attribute to the current date
        $('#date-of-joining').attr('max', GetInputDateFormat(currentDate));

        // Check if the value is empty or exceeds the maximum date
        if (dateOfJoining === '' || new Date(dateOfJoining) > new Date(currentDate)) {
            result = false;
        }

        // Check If Values Is Not Empty
        if ($('#person-name').prop('selectedIndex') < 1) {
            result = false;
        }

        // Check If Values Is Not Empty
        if ($('#employee-type').prop('selectedIndex') < 1) {
            result = false;
        }

        // Check If Values Is Not Empty
        if ($('#posting-place').prop('selectedIndex') < 1) {
            result = false;
        }

        if (result)
            $('#employee-detail-accordion-error').addClass('d-none');
        else
            $('#employee-detail-accordion-error').removeClass('d-none');

        return result;
    }

    // 6. EEmployee Photo Accordion Input Validation
    function ValidatePhotoPreview() {
        let result = true;
        const photoPreviewSrc = $('#photo-file-uploader-image-preview').attr('src');
        const isMandatory = $('#photo-document-upload').val() === MANDATORY;

        if (isMandatory) {
            if (photoPreviewSrc && photoPreviewSrc !== '#') {
                $('#photo-file-uploader-error').addClass('d-none');
                $('#employee-photo-accordion-error').addClass('d-none');
            } else {
                $('#photo-file-uploader-error').text('Please upload a photo.').removeClass('d-none');
                $('#employee-photo-accordion-error').removeClass('d-none');
                result = false;
            }
        }

        return result;  
    }


    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@  Document - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

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


    // DataTable Add Button 
    $('#btn-add-document-dt').click(function () {
        event.preventDefault();
        editedDocumentId = '';
        employeeDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';

        SetDocumentUniqueDropdownList();

        SetModalTitle('document', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-document-dt').click(function () {
        debugger;
        SetModalTitle('document', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-document-dt').data('rowindex');

            id = $('#document-modal').attr('id');
            myModal = $('#' + id).modal();

            editedDocumentId = columnValues[1];
            debugger;

            $('#document-id', myModal).val(columnValues[1]);

            fileUploader = $('#' + $(columnValues[3]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'document-photo-file-uploader';

            // columnValues[23] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[3]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[3]).attr('class') === 'db-record' ? true : false;

            // columnValues[24] - Image Tag Html
            filePath = $('#' + $(columnValues[4]).attr('id')).attr('src');

            fileNameDocument = columnValues[6];
            employeeDocumentPrmKey = columnValues[7];
            localStoragePath = columnValues[8];

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';
                AttachFileUploader();
            }

            $('#document-photo-file-uploader-image-preview').attr('src', filePath);

            $('#note-document', myModal).val(columnValues[5]);


            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-document-edit-dt').addClass('read-only');
            $('#document-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-document-modal').click(function (event) {
        debugger;
        if (IsValidDocumentDataTableModal()) {
            row = documentDataTable.row.add([
                        tag,
                        documentId,
                        documentText,
                        fileUploaderInputHtml,
                        imageTagHtml,
                        note,
                        fileNameDocument,
                        employeeDocumentPrmKey,
                        localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            $('#document-error').addClass('d-none');

            HideDocumentDataTableColumns();

            documentDataTable.columns.adjust().draw();

            $('#document-modal').modal('hide');

            EnableNewOperation('document');
        }
    });

    // Modal update Button Event
    $('#btn-update-document-modal').click(function (event) {
        $('#select-all-document').prop('checked', false);

        if (IsValidDocumentDataTableModal()) {
            documentDataTable.row(selectedRowIndex).data([
                        tag,
                        documentId,
                        documentText,
                        fileUploaderInputHtml,
                        imageTagHtml,
                        note,
                        fileNameDocument,
                        employeeDocumentPrmKey,
                        localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }


            HideDocumentDataTableColumns();

            documentDataTable.columns.adjust().draw();

            columnValues = $('#btn-update-document').data('rowindex');

            $('#document-modal').modal('hide');

            EnableNewOperation('document');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-document-dt').click(function (event) {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-document tbody input[type="checkbox"]:checked').each(function () {
                    documentDataTable.row($('#tbl-document tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-document-dt').data('rowindex');

                     EnableNewOperation('document');

                    SetDocumentUniqueDropdownList();

                   $('#select-all-document').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!documentDataTable.data().any()) {
                        $('#document-error').removeClass('d-none');
                }
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-document').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-document tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = documentDataTable.row(row).index();

                rowData = (documentDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-document-dt').data('rowindex', arr);
                EnableDeleteOperation('document');
            });
        }
        else {
            EnableNewOperation('document');

            $('#tbl-document tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-document tbody').click("input[type=checkbox]", function () {
        debugger;
        $('#tbl-document input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                debugger;
                let row = $(this).closest('tr');
                selectedRowIndex = documentDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (documentDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('document');

                    $('#btn-update-document-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-document-dt').data('rowindex', rowData);
                    $('#btn-delete-document-dt').data('rowindex', arr);
                    $('#select-all-document').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-document tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('document');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('document');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('document');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-document').prop('checked', true);
        else
            $('#select-all-document').prop('checked', false);
    });

    // Validate Document
    function IsValidDocumentDataTableModal() {
        debugger
        result = true;
        counter++;
        fileUploaderId = "data-table-document-photo-file-uploader" + counter;
        fileId = "photo-Id" + counter;

        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        // Capture the currently selected value and text
        documentId = $('#document-id option:selected').val();
        documentText = $('#document-id option:selected').text();//NameOfDocument; // Get the text of the selected option
        note = $('#note-document').val();

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#document-photo-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        fileUploader = $('#document-photo-file-uploader').get(0);

        if (note === '')
            note = 'None';

        // Validate Document
        if ($('#document-id').prop('selectedIndex') < 1) {
            result = false;
            $('#document-id-error').removeClass('d-none');
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.KYCDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#document-photo-file-uploader-error').removeClass('d-none');
                }
            }
                // ****** New Changes
            else {
                let photoSrc = $('#document-photo-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
            // ***********
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideDocumentDataTableColumns() {
        documentDataTable.column(1).visible(false);
        documentDataTable.column(6).visible(false);
        documentDataTable.column(7).visible(false);
        documentDataTable.column(8).visible(false);
    }

    // Document Unique Dropdown
    function SetDocumentUniqueDropdownList() {
        // Show All List Items
        $('#document-id').html('');
        $('#document-id').append(DOCUMENT_TYPE_DROPDOWN_LIST);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-document > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (documentDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null)
                if (myColumnValues[1] != editedDocumentId)
                    $('#document-id').find('option[value="' + myColumnValues[1] + '"]').remove();
        });
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Salary Structure  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-employee-salary-structure-dt').click(function () {
        debugger;
        event.preventDefault();
        SetModalTitle('employee-salary-structure', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-employee-salary-structure-dt').click(function () {
        SetModalTitle('employee-salary-structure', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-employee-salary-structure-dt').data('rowindex');
            id = $('#employee-salary-structure-modal').attr('id');
            myModal = $('#' + id).modal();
            activationDate = new Date(columnValues[5]);
            expiryDate = new Date(columnValues[6]);
            closeDate = new Date(columnValues[7]);

            // Display Value In Modal Inputs
            $('#salary-break-up', myModal).val(columnValues[1]);
            $('#breakup-value', myModal).val(columnValues[3]);
            $('#is-percentage', myModal).val(columnValues[4]);

            if (columnValues[4] === 'True') {
                $('#is-percentage').prop('checked', true);
            }
            else {
                $('#is-percentage').prop('checked', false);
            }

            $('#activation-date-employee-salary-structure', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-employee-salary-structure', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-employee-salary-structure', myModal).val(GetInputDateFormat(closeDate));
            $('#note-employee-salary-structure', myModal).val(columnValues[8]);
            $('#reason-for-modification-employee-salary-structure', myModal).val(columnValues[9]);

            myModal.modal({ show: true });
        }
        else {
            $('.btn-employee-salary-structure-edit-dt').addClass('read-only');
            $('#employee-salary-structure-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-employee-salary-structure-modal').click(function (event) {
        if (IsValidSalaryStructureDataTableModal()) {
            row = employeeSalaryStructureDataTable.row.add([
                                tag,
                                salaryBreakupId,
                                salaryBreakupText,
                                breakupValue,
                                isPercentage,
                                activationDate,
                                expiryDate,
                                closeDate,
                                note,
                                reasonForModification
            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            // Error Message In Span
            $('#salary-structure-data-table-error').addClass('d-none');

            HideGenderDataTableColumns();

            employeeSalaryStructureDataTable.columns.adjust().draw();

            $('#employee-salary-structure-modal').modal('hide');

            EnableNewOperation('employee-salary-structure');
        }
    });

    // Modal update Button Event
    $('#btn-update-employee-salary-structure-modal').click(function (event) {

        $('#select-all-employee-salary-structure').prop('checked', false);
        if (IsValidSalaryStructureDataTableModal()) {
            employeeSalaryStructureDataTable.row(selectedRowIndex).data([
                                tag,
                                salaryBreakupId,
                                salaryBreakupText,
                                breakupValue,
                                isPercentage,
                                activationDate,
                                expiryDate,
                                closeDate,
                                note,
                                reasonForModification
            ]).draw();

            HideGenderDataTableColumns();

            employeeSalaryStructureDataTable.columns.adjust().draw();

            $('#employee-salary-structure-modal').modal('hide');

            EnableNewOperation('employee-salary-structure');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-employee-salary-structure-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-employee-salary-structure tbody input[type="checkbox"]:checked').each(function () {
                    employeeSalaryStructureDataTable.row($('#tbl-employee-salary-structure tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-employee-salary-structure-dt').data('rowindex');
                    EnableNewOperation('employee-salary-structure');

                    $('#select-all-employee-salary-structure').prop('checked', false);

                      if (!employeeSalaryStructureDataTable.data().any())
                        $('#salary-structure-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherGenderSubscrption Datatable
    $('#select-all-employee-salary-structure').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-employee-salary-structure tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = employeeSalaryStructureDataTable.row(row).index();

                rowData = (employeeSalaryStructureDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-employee-salary-structure-dt').data('rowindex', arr);
                EnableDeleteOperation('employee-salary-structure')
            });
        }
        else {
            EnableNewOperation('employee-salary-structure');

            $('#tbl-employee-salary-structure tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-employee-salary-structure tbody').click("input[type=checkbox]", function () {
        $('#tbl-employee-salary-structure input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = employeeSalaryStructureDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (employeeSalaryStructureDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('employee-salary-structure');

                    $('#btn-update-employee-salary-structure-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-employee-salary-structure-dt').data('rowindex', rowData);
                    $('#btn-delete-employee-salary-structure-dt').data('rowindex', arr);
                    $('#select-all-employee-salary-structure').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('employee-salary-structure');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('employee-salary-structure');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('employee-salary-structure');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-employee-salary-structure').prop('checked', true);
        else
            $('#select-all-employee-salary-structure').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-employee-salary-structure> tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (employeeSalaryStructureDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#employee-salary-structure-general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate SalaryStructure Module
    function IsValidSalaryStructureDataTableModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        salaryBreakupId = $('#salary-break-up option:selected').val();
        salaryBreakupText = $('#salary-break-up option:selected').text();
        breakupValue = parseFloat($('#breakup-value').val());
        isPercentage = $('#is-percentage').is(':checked') ? 'True' : 'False';
        activationDate = $('#activation-date-employee-salary-structure').val();
        expiryDate = $('#expiry-date-employee-salary-structure').val();
        closeDate = $('#close-date-employee-salary-structure').val();
        note = $('#note-employee-salary-structure').val();
        reasonForModification = $('#reason-for-modification-employee-salary-structure').val();

        //Set Default Value if Empty
        if (note === '') {
            note = 'None';
        }
        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-employee-salary-structure');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-employee-salary-structure');

         if ($('#salary-break-up').prop('selectedIndex') < 1) {
            result = false;
            $('#salary-break-up-error').removeClass('d-none');
        }

        if (isNaN(breakupValue) === false) {
            minimum = parseFloat($('#breakup-value').attr('min'));
            maximum = parseFloat($('#breakup-value').attr('max'));

            if (parseFloat(breakupValue) < parseFloat(minimum) || parseFloat(breakupValue) > parseFloat(maximum)) {
                $('#breakup-value-error').removeClass('d-none');
                result = false;
            }
        }
        else {
            $('#breakup-value-error').removeClass('d-none');
            result = false;
        }

        //Activation Date
        if (isValidActivationDate === false) {
            result = false;
            $('#activation-date-employee-salary-structure-error').removeClass('d-none');
        }

        //Expiry Date
        if (isValidExpiryDate === false) {
            result = false;
            $('#expiry-date-employee-salary-structure-error').removeClass('d-none');
        }
      
        return result;
    }

    // Hide Unnecessary Columns
    function HideGenderDataTableColumns() {
        employeeSalaryStructureDataTable.column(1).visible(false);
        employeeSalaryStructureDataTable.column(7).visible(false);
        employeeSalaryStructureDataTable.column(9).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {

        debugger;
        let isValidAllInputs = true;

        if ($('form').valid()) {

            // not add event.preventDefault
            $('.lastrow').remove();

            

            let employeeData;
            // To Pass List Object Parameter, Create Array Objects And Get Values.
            employeeData = new FormData($('#form')[0]);

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let salaryStructureArray = new Array();

            //CreateDataTable
            documentDataTable.page.len(-1).draw();
            employeeSalaryStructureDataTable.page.len(-1).draw();

            // Validate Employee Code
            if (!isValidEmployeeCode) {
                isValidEmployeeCode = false;
                $('#employee-code-error').removeClass('d-none');
            }
            else {
                $('#employee-code-error').addClass('d-none');
            }
            // Normal Accordion 1 - Employee Department Validation Error
            if (!IsValidEmployeeDepartmentAccordionInputs()) {
                isValidAllInputs = false;
            }
            // Normal Accordion 2 - Designation Department Validation Error
            if (!IsValidDesignationDepartmentAccordionInputs()) {
                isValidAllInputs = false;
            }
            // Normal Accordion 3 - Performance Rating Validation Error
            if (!IsValidPerformanceRatingAccordionInputs())
            {
                isValidAllInputs = false;
            }

            // Normal Accordion 4- Employee Working Schedule Validation Error
            if (!IsValidEmployeeWorkingScheduleAccordionInputs())
            {
                isValidAllInputs = false;
            }

            // Normal Accordion 5 - Employee Detail Validation Error
            if (!IsValidEmployeeDetailAccordionInputs())
            {
                isValidAllInputs = false;
            }

            // Normal Accordion 1 - Employee Photo Validation Error
            if (!ValidatePhotoPreview())
            {
                isValidAllInputs = false;
            }

            // Create Array For Employee Document Case Table To Pass Data
            if (!$('#heading-employee-document').hasClass('d-none')) {
                debugger;
                if (documentDataTable.data().any()) {

                    $('#document-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-document > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (documentDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues == 'undefined' && columnValues == null) {
                                return false;
                            }
                            else {
                                row = $(this);
                                employeeData.append("_EmployeeDocument[" + i + "].DocumentId", columnValues[1]);
                                employeeData.append("_EmployeeDocument[" + i + "].DocPath", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                employeeData.append("_EmployeeDocument[" + i + "].Note", columnValues[5]);
                                employeeData.append("_EmployeeDocument[" + i + "].NameOfFile", columnValues[6]);
                                employeeData.append("_EmployeeDocument[" + i + "].EmployeeDocumentPrmKey", columnValues[7]);
                                employeeData.append("_EmployeeDocument[" + i + "].StoragePath", columnValues[8]);

                            }
                        });
                    }
                }
                else {
                    $('#document-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // 2. Contact Detail - Create Array For contact Data Table To Pass Data
            if (!$('#heading-employee-salary-structure').hasClass('d-none')) {
                if (employeeSalaryStructureDataTable.data().any()) {
                    $('#salary-structure-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {
                        // Get Data Table Values In Turn Over Limit Array
                        $('#tbl-employee-salary-structure > TBODY > TR').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (employeeSalaryStructureDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                salaryStructureArray.push({
                                    'SalaryBreakupId': columnValues[1],
                                    'BreakupValue': columnValues[3],
                                    'IsPercentage': columnValues[4],
                                    'ActivationDate': columnValues[5],
                                    'ExpiryDate': columnValues[6],
                                    'CloseDate': columnValues[7],
                                    'Note': columnValues[8],
                                    'ReasonForModification': columnValues[9],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#salary-structure-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Call Controller Save Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: saveDataTables,
                    type: 'POST',
                    async: false,
                    data: {
                        '_salaryStructure': salaryStructureArray,

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
                debugger;
                $.ajax({
                    url: saveDataTableURL,
                    type: 'POST',
                    async: false,
                    data: employeeData,
                    contentType: false,
                    processData: false,
                    cache: false,
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