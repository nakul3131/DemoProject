'use strict';
$(document).ready(function ()
{
    // @@@@@@@@@@ Data Table Related Declaration

    const MANDATORY = 'M';

    let result = true;
    let branchDropdownList = '';
    let dropdownListItems = '';
    let dropDownListItemCount = 0;

    let personBankDetailDocumentPrmKey = 0;
    let fileNameDocument = '';
    let localStoragePath = '';
    let isDbRecord = false;
    let isChangedPhoto = false;
    let lastSelectedValue = '';

    let tag = '';
    let rowNum = 0;
    let selectedRowIndex = 0;
    let checked = false;  
    let isChecked = false;
    let isCheckedAll = false;
    let minimumLength = 0;
    let maximumLength = 0;
    let arr = new Array();

    let row;
    let currentRow;
    let rowData;
    let columnValues;

    let myModal;

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
    let fileCaption = '';
    let note = '';
    let reasonForModification = '';
    let files;
    let i = 0;

    // Document
    let fileUploaderInput;
    let fileObj;
    let filePath = '';
    let fileUploader;
    let fileUploaderInputHtml = '';
    let imageTagHtml = '';
    let fileUploaderId = '';
    let fileId = '';
    let counter = 100;
    let selectedDocumentObject;

    //create Datatable
    let bankDataTable = CreateDataTable('bank-detail');
    
    SetPageLoadingDefaultValues();

    function SetPageLoadingDefaultValues()
    {
        if (($('#bank-statement-upload').val()) === MANDATORY)
        {
            $('#bank-file-uploader').attr('required');
            $('#file-caption-bank').attr('required');
            $('#bank-file-uploader').addClass('mandatory-mark');
            $('#file-caption-bank').addClass('mandatory-mark');
        }
        else
        {
            $('#bank-file-uploader').removeAttr('required');
            $('#file-caption-bank').removeAttr('required');
            $('#bank-file-uploader').removeClass('mandatory-mark');
            $('#file-caption-bank').removeClass('mandatory-mark');
        }
    }

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

    function SetBranchDropdownList()
    {
        $.get('/DynamicDropdownList/GetBankBranchDropdownListByBankId', { _bankId: bankId, async: false }, function (data, textStatus, jqXHR)
        {
            debugger;

            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Branch --- </option>';

            $.each(data, function (index, selectListItemObj)
            {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#bank-branch-id').html(dropdownListItems);

            dropDownListItemCount = $('#bank-branch-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (dropDownListItemCount === 1)
            {
                $('#bank-branch-id').prop('selectedIndex', 1);
            }
            else
            {
                if(bankBranchId !== '')
                {
                    $('#bank-branch-id').val(bankBranchId);
                }
            }
        });
    }

    function IsValidBankAccountNumber(_myId)
    {
        let myResult = true;
        let myErrorId = '#' + _myId + '-error';

        let regExp = new RegExp('^\d{9,18}$');
        // ^ :- Beginning of the string. 
        // [0-9] :- Match any character in the set.
        // {9,18} :- Match Between 9 to 18 of the preceding token.
        // $ :- End of the string.

        // Check if the account number from 9 to 18 digit by RBI
        if (accountNumber.length < 10 || accountNumber.length > 18)
        {
            $(myErrorId).html('Account Number Length From 9 Digit To 18 Digit.');

            myResult = false;
        }

        // Check if the account number contains only numeric digits
        if (regExp.test(accountNumber))
        {
            $(myErrorId).html('Account Number Must Only Contain Digit.');

            myResult = false;
        }

        if(myResult === true)
        {
            $(myErrorId).addClass('d-none');

        }
        else
        {
            $(myErrorId).removeClass('d-none');
        }
    }

    // Document File Uploader
    $('.doc-upload').change(function ()
    {
        debugger;
        let docInput = '';
        let myId = $('.doc-upload').attr('id');

        // Document
        switch (myId)
        {
            case 'bank-file-uploader':
                docInput = 'BankStatement';
                break;
            default:
                docInput = 'None';
        }

        // $('#document-file-uploader-error').addClass('d-none');
        $('#' + myId + '-error').addClass('d-none');

        if (this.files.length > 0)
        {
            const uploadFile = this.files[0];

            $('#file-caption-bank').val('');

            // Upload File
            if (IsValidFile(docInput, uploadFile))
            {
                let reader = new FileReader();

                reader.onload = function (e)
                {
                    $('#bank-file-uploader-image-preview').attr('src', e.target.result);
                }

                reader.readAsDataURL(uploadFile);
            }
        }
        else
        {
            $('#bank-file-uploader-image-preview').attr('src', '');
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
            debugger;
            // .pop(): - Removes and returns the last element from the array, which is the file extension in this case.
            const fileName = _uploadFile.name;
            const fileSize = Math.round(_uploadFile.size / 1024);
            const fileExtension = fileName.split('.').pop().toLowerCase();

            let validDocumentFileExtensions = '';
            let maxDocumentFileSize = 0;

            // Document
            if (_inputSource === 'Document')
            {
                validDocumentFileExtensions = (selectedDocumentObject[0].EnableDocumentUploadInDb ? selectedDocumentObject[0].DocumentAllowedFileFormatsForDb : selectedDocumentObject[0].DocumentAllowedFileFormatsForLocalStorage).toLowerCase().replace('.', '');
                maxDocumentFileSize = selectedDocumentObject[0].EnableDocumentUploadInDb ? selectedDocumentObject[0].MaximumFileSizeForDocumentUploadInDb : selectedDocumentObject[0].MaximumFileSizeForDocumentUploadInLocalStorage;

                if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize)) {
                    $('#bank-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + 'Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#bank-file-uploader-error').removeClass('d-none');

                    $('#bank-file-uploader-image-preview').attr('src', '#');
                    $('#bank-file-uploader').val('');

                    result = false;
                }
            }
            else
            {
                let uploaderId = _inputSource.replace('Asset', '').toLowerCase();

                if (_inputSource === 'BankStatement') {
                    uploaderId = 'bank';
                }

                isUploadInLocalStorage = personInformationParameterViewModel[`Enable${_inputSource}DocumentUploadInLocalStorage`];

                // Get File Formats And File Size By Storage
                if (isUploadInLocalStorage === true)
                {
                    validDocumentFileExtensions = personInformationParameterViewModel[`${_inputSource}DocumentAllowedFileFormatsForLocalStorage`].toLowerCase().replace('.', '');;
                    maxDocumentFileSize = personInformationParameterViewModel[`MaximumFileSizeFor${_inputSource}DocumentUploadInLocalStorage`];
                }
                else
                {
                    validDocumentFileExtensions = personInformationParameterViewModel[`${_inputSource}DocumentAllowedFileFormatsForDb`].toLowerCase().replace('.', '');;
                    maxDocumentFileSize = personInformationParameterViewModel[`MaximumFileSizeFor${_inputSource}DocumentUploadInDb`];
                }

                // Check Valid File Formats Or Size
                if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize))
                {
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

    $('#bank-id').focusout(function ()
    {
        debugger;
        const currentValue = $(this).val();
        bankId = $(this).val();

        if (currentValue !== lastSelectedValue)
        {
            $('.switch-input').prop('checked', false);
            $('select').not('#bank-id').prop('selectedIndex', 0);
            $('input[type="date"], textarea').val('');
            $('input[type="text"]').not('#checker-remark, #maker-remark, #o-remark').val('');
            $('#bank-file-uploader').val('');
            $('#bank-file-uploader-image-preview').attr('src', '');
            $('.modal-input-error').addClass('d-none');
        }

        lastSelectedValue = currentValue;
        SetBranchDropdownList();
    });

    $('#account-number').focusout(function ()
    {
        IsValidBankAccountNumber('account-number');
    });

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Bank Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-bank-detail-dt').click(function (event)
    {
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
    $('#btn-edit-bank-detail-dt').click(function ()
    {
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('bank-detail', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#bank-detail-modal').modal();

            let tmpDate = new Date();

            columnValues = $('#btn-edit-bank-detail-dt').data('rowindex');

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
            $('#activation-open-date', myModal).val(GetInputDateFormat(accountopeningDate));
            $('#expiry-open-date', myModal).val(GetInputDateFormat(accountclosingDate));

            $('#is-default-bank-transaction', myModal).prop('checked', columnValues[8].toString().toLowerCase() === 'true' ? true : false);

            $('#file-caption-bank', myModal).val(columnValues[11]);

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
            if (fileUploader.files.length > 0)
            {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                AttachFileUploader();
            }

            $('#bank-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#bank-detail-modal').modal('show');
        }
        else
        {
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
    function IsValidBankDetailModal()
    {
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
        openingDate = $('#activation-open-date').val();
        closeDate = $('#expiry-open-date').val();
        isDefaultBankForTransaction = $('#is-default-bank-transaction').is(':checked') ? true : false;
        fileCaption = $('#file-caption-bank').val();
        note = $('#note-bank-detail').val();
        reasonForModification = $('#reason-for-modification-bank-detail').val();

        if (isDbRecord === false || isChangedPhoto === true)
        {
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
            $('#file-caption-bank').val('None');
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

        let isValidOpeningDate = IsValidInputDate('#activation-open-date');

        if (isValidOpeningDate === false) {
            result = false;
            $('#activation-open-date-error').removeClass('d-none');
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

            maximumLength = parseInt($('#file-caption-bank').attr('maxlength'));

            if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
                result = false;
                $('#file-caption-bank-error').removeClass('d-none');
            }
        } else {
            result = false;
            $('#file-caption-bank-error').removeClass('d-none');
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

    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;

        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let personFormData;
            personFormData = new FormData($("#form")[0]);

            // Create Array For person bank detail Table To Pass Data
            if (bankDataTable.data().any())
            {             
                if (isValidAllInputs)
                {
                    $('#tbl-bank-detail > tbody > tr').each(function (i)
                    {
                        currentRow = $(this).closest('tr');

                        columnValues = (bankDataTable.row(currentRow).data());

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues === 'undefined' && columnValues === null) {
                            return false;
                        }
                        else
                        {
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
            else {
                isValidAllInputs = false;
            }
            

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
                        alert('An Error Has Occured In Person Bank Detail DataTable!!! Error Message - ' + error.toString());
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
