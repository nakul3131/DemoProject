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
    let tag = '';
    let id = '';
    let myModal;
    let rowNum = 0;
    let selectedRowIndex;
    let row;
    let rowData;
    let checked;
    let columnValues;
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let minimum = 0;
    let maximum = 0;
    let maximumLength = 0;
    let arr = new Array();

    // Document
    let fileUploaderInput;
    let fileObj;
    let filePath = '';
    let fileUploader;
    let fileUploaderInputHtml = '';
    let imageTagHtml = '';
    let fileCaption;
    let fileUploaderId = '';
    let fileId = '';
    let counter = 100;
    let files;
    let selectedDocumentObject;
    let personIncomeTaxDetailDocumentPrmKey;
    let fileNameDocument;
    let localStoragePath;


    //Income Tax Detail
    let assessmentYear = 0;
    let taxAmount;
    let note;
    let reasonForModification;

    // *************** C A L L   P A G E   L O A D I N G   D E F A U L T   V A L U E S ***************
    SetPageLoadingDefaultValues();

    function SetPageLoadingDefaultValues() {
        debugger;
        if (($('#income-tax-detail-upload').val()) === MANDATORY) {
            $('#income-tax-file-uploader').attr('required');
            $('#file-caption-tax').attr('required');
            $('#income-tax-file-uploader').addClass('mandatory-mark');
            $('#file-caption-tax').addClass('mandatory-mark');
        } else {
            $('#income-tax-file-uploader').removeClass('mandatory-mark');
            $('#file-caption-tax').removeClass('mandatory-mark');
            $('#income-tax-file-uploader').removeAttr('required');
            $('#file-caption-tax').removeAttr('required');
        }
    }

    // Create DataTables
    let incomeTaxDataTable = CreateDataTable('income-tax');


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
            case 'income-tax-file-uploader':
                docInput = 'IncomeTax';
                break;
            default:
                docInput = 'None';
        }

        // $('#document-file-uploader-error').addClass('d-none');
        $('#' + myId + '-error').addClass('d-none');

        if (this.files.length > 0) {
            const uploadFile = this.files[0];
            $('#file-caption-tax').val('');
            // Upload File
            if (IsValidFile(docInput, uploadFile)) {
                let reader = new FileReader();

                reader.onload = function (e) {
                    $('#income-tax-file-uploader-image-preview').attr('src', e.target.result);
                }

                reader.readAsDataURL(uploadFile);
            }
        }
        else {
            $('#income-tax-file-uploader-image-preview').attr('src', '');
        }

    });

    // Validate Uploaded File
    function IsValidFile(_inputSource, _uploadFile) {
        debugger;
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

            // Document
            if (_inputSource === 'Document') {
                validDocumentFileExtensions = (selectedDocumentObject[0].EnableDocumentUploadInDb ? selectedDocumentObject[0].DocumentAllowedFileFormatsForDb : selectedDocumentObject[0].DocumentAllowedFileFormatsForLocalStorage).toLowerCase().replace('.', '');
                maxDocumentFileSize = selectedDocumentObject[0].EnableDocumentUploadInDb ? selectedDocumentObject[0].MaximumFileSizeForDocumentUploadInDb : selectedDocumentObject[0].MaximumFileSizeForDocumentUploadInLocalStorage;

                if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize)) {
                    $('#income-tax-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + 'Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#income-tax-file-uploader-error').removeClass('d-none');

                    $('#income-tax-file-uploader-image-preview').attr('src', '#');
                    $('#income-tax-file-uploader').val('');

                    result = false;
                }
            }
            else {
                let uploaderId = _inputSource.replace('Asset', '').toLowerCase();

                if (_inputSource === 'IncomeTax') {
                    uploaderId = 'income-tax';
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
        }

        return result;
    }

    /// @@@@@@@@@@@@@@@@@@@@@@   Person Income Tax Detail  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

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
            $('#file-caption-tax', myModal).val(columnValues[5]);
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
        fileCaption = $('#file-caption-tax').val();
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

        if (reasonForModification === '') {
            $('#reason-for-modification-tax-detail').val('None');
            reasonForModification = 'None';
        }

        if (fileCaption === '') {
            $('#file-caption-tax').val('None');
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
        maximumLength = parseInt($('#file-caption-tax').attr('maxlength'));

        if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#file-caption-tax-error').removeClass('d-none');
        }

        return result;

    }

    // Hide Unnecessary Columns
    function HideColumnsIncomeTaxDataTable() {
        incomeTaxDataTable.column(8).visible(false);
        incomeTaxDataTable.column(9).visible(false);
        incomeTaxDataTable.column(10).visible(false);
    }


    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@


    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            let personFormData;
            personFormData = new FormData($("#form")[0]);

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
                                personFormData.append("_incomeTaxDetail[" + i + "].LocalStoragePath", columnValues[10]); 
                            }
                        });
                    }
                }
                else {
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