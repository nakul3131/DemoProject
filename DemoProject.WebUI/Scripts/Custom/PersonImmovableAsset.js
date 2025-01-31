'use strict'
$(document).ready(function ()
{
    const MANDATORY = 'M';

    let isDbRecord = false;
    let isChangedPhoto = false;
    let personImmovableAssetDocumentPrmKey;
    let fileNameDocument = '';
    let localStoragePath;

    // Global 
    // Common Variables
    let result = true;
    let minimum = 0;
    let maximum = 0;
    let minimumLength = 0;
    let maximumLength = 0;
    let selectedDocumentObject;
    let rowNum = 0;

    // @@@@@@@@@@ Data Table Related Varible Declaration
    let tag = '';
    let id = '';
    let myModal;
    let selectedRowIndex;
    let row;
    let rowData;
    let checked;
    let columnValues;
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let arr = new Array();
    let note = '';
    let reasonForModification = '';

    //Person Immovable Asset
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
    let residenceType = '';
    let residenceTypeText = '';
    let currentMarketValue = 0;
    let ownershipPercentage = 0;
    let hasAnyMortgage = false;
    let isOwnershipDeceased = false;

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

    // *************** C A L L   P A G E   L O A D I N G   D E F A U L T   V A L U E S ***************
    SetPageLoadingDefaultValues();

    function SetPageLoadingDefaultValues() {
        debugger;
        if (($('#immovable-asset-upload').val()) === MANDATORY) {
            $('#immovable-file-uploader').attr('required');
            $('#file-caption-immovable').attr('required');
            $('#immovable-file-uploader').addClass('mandatory-mark');
            $('#file-caption-immovable').addClass('mandatory-mark');
        } else {
            $('#immovable-file-uploader').removeClass('mandatory-mark');
            $('#file-caption-immovable').removeClass('mandatory-mark');
            $('#immovable-file-uploader').attr('required');
            $('#file-caption-immovable').attr('required');
        }
    }

    //  ************** Create Data Table  **************
    let immovableDataTable = CreateDataTable('immovable-asset');

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@

    //  ************** Accordion Input Validation  **************

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

    // ****** Remove After Following doc-upload working Successfully Document File Uploader

    // ###########################  U S E R      D E F I N E D       F U N C T I O N S    ###########################   

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

        let docInput = '';
        let myId = $('.doc-upload').attr('id');

        // Document
        switch (myId) {

            case 'immovable-file-uploader':
                docInput = 'ImmovableAsset';
                break;

            default:
                docInput = 'None';
        }

        // $('#document-file-uploader-error').addClass('d-none');
        $('#' + myId + '-error').addClass('d-none');

        if (this.files.length > 0) {
            const uploadFile = this.files[0];
            $('#file-caption-immovable').val('');
            // Upload File
            if (IsValidFile(docInput, uploadFile)) {
                let reader = new FileReader();

                reader.onload = function (e) {
                    $('#immovable-file-uploader-image-preview').attr('src', e.target.result);
                }

                reader.readAsDataURL(uploadFile);
            }
        }
        else {
            $('#immovable-file-uploader-image-preview').attr('src', '');
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

            // Document
            if (_inputSource === 'Document') {
                validDocumentFileExtensions = (selectedDocumentObject[0].EnableDocumentUploadInDb ? selectedDocumentObject[0].DocumentAllowedFileFormatsForDb : selectedDocumentObject[0].DocumentAllowedFileFormatsForLocalStorage).toLowerCase().replace('.', '');
                maxDocumentFileSize = selectedDocumentObject[0].EnableDocumentUploadInDb ? selectedDocumentObject[0].MaximumFileSizeForDocumentUploadInDb : selectedDocumentObject[0].MaximumFileSizeForDocumentUploadInLocalStorage;

                if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize)) {
                    $('#immovable-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + 'Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#immovable-file-uploader-error').removeClass('d-none');

                    $('#immovable-file-uploader-image-preview').attr('src', '#');
                    $('#immovable-file-uploader').val('');

                    result = false;
                }
            }
            else {
                let uploaderId = _inputSource.replace('Asset', '').toLowerCase();
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

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

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

            $('#file-caption-immovable', myModal).val(columnValues[19]);

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
        fileCaption = $('#file-caption-immovable').val();

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
            $('#file-caption-immovable').val('None');
            fileCaption = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-immovable-asset').val('None');
            reasonForModification = 'None';
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

            maximumLength = parseInt($('#file-caption-immovable').attr('maxlength'));

            if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
                result = false;
                $('#file-caption-immovable-error').removeClass('d-none');
            }
        } else {
            result = false;
            $('#file-caption-immovable-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsImmovableDataTable() {
        immovableDataTable.column(9).visible(false);
        immovableDataTable.column(11).visible(false);
        immovableDataTable.column(23).visible(false);
        immovableDataTable.column(24).visible(false);
        immovableDataTable.column(25).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@
    $('#btnsave').click(function () {
        let isValidAllInputs = true;

        //if ($('form').valid()
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let personFormData;
            personFormData = new FormData($("#form")[0]);

            // Create Array For person immovable asset Table To Pass Data
            if (immovableDataTable.data().any()) {

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
                        alert('An Error Has Occured In Person Immovable Asset DataTable!!! Error Message - ' + error.toString());
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


