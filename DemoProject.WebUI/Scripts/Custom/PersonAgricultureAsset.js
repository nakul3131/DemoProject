'use strict'
$(document).ready(function ()
{

    const MANDATORY = 'M';

    // Global 
    // Common Variables
    let result = true;
    let minimum = 0;
    let maximum = 0;
    let minimumLength = 0;
    let maximumLength = 0;
    let selectedDocumentObject;
    let rowNum = 0;

    let isDbRecord = false;
    let isChangedPhoto = false;
    let personAgricultureAssetDocumentPrmKey;
    let fileNameDocument = '';
    let localStoragePath = '';

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
    let surveyNumber = 0;
    let areaOfLand;
    let ownershipTypeText = '';
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
    let prmKey = 0;
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
    let fileCaption = '';
    let fileUploaderId = '';
    let fileId = '';
    let lastSelectedValue = '';

    let counter = 100;

    //  ************** Create Data Table  **************

    let agricultureDataTable = CreateDataTable('agriculture-asset');

    SetPageLoadingDefaultValues();

    function SetPageLoadingDefaultValues()
    {
        if (($('#agriculture-asset-document-upload').val()) === MANDATORY)
        {
            $('#agriculture-file-uploader').attr('required');
            $('#file-caption').attr('required');
            $('#agriculture-file-uploader').addClass('mandatory-mark');
            $('#file-caption').addClass('mandatory-mark');
        }
        else
        {
            $('#agriculture-file-uploader').removeAttr('required');
            $('#file-caption').removeAttr('required');
            $('#agriculture-file-uploader').removeClass('mandatory-mark');
            $('#file-caption').removeClass('mandatory-mark');
        }

    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@
    
    //agriculture asset dropdown
    $('#agriculture-land-type-id').focusout(function ()
    {
        let currentValue = $(this).val();

        if (currentValue !== lastSelectedValue)
        {
            $('input[type="date"], input[type="number"], textarea').val('');
            $('input[type="text"]').not('#checker-remark, #maker-remark, #o-remark').val('');

            $('.switch-input').prop('checked', false);
            $('select').not('#agriculture-land-type-id').prop('selectedIndex', 0);

            $('#agriculture-file-uploader').val('');
            $('#agriculture-file-uploader-image-preview').attr('src', '');

            $('.modal-input-error').addClass('d-none');
        }

        lastSelectedValue = currentValue;
    });

    $('#enable-any-court-case').change(function ()
    {
        if ($(this).is(':checked', true))
        {
            $('#court-case-full-details-error').addClass('d-none');
        }
    });

    // ###########################  U S E R      D E F I N E D       F U N C T I O N S    ###########################   

    function AttachFileUploader() {
        debugger;
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

            case 'agriculture-file-uploader':
                docInput = 'AgricultureAsset';
                break;

            default:
                docInput = 'None';
        }

        // $('#document-file-uploader-error').addClass('d-none');
        $('#' + myId + '-error').addClass('d-none');

        if (this.files.length > 0) {
            const uploadFile = this.files[0];
            $('#file-caption').val('');
            // Upload File
            if (IsValidFile(docInput, uploadFile)) {
                let reader = new FileReader();

                reader.onload = function (e) {
                    $('#agriculture-file-uploader-image-preview').attr('src', e.target.result);
                }

                reader.readAsDataURL(uploadFile);
            }
        }
        else {
            $('#agriculture-file-uploader-image-preview').attr('src', '');
        }

    });

    // Validate Uploaded File
    function IsValidFile(_inputSource, _uploadFile)
    {
        debugger;
        let result = true;
        let isUploadInLocalStorage = false;

        //new change
        isChangedPhoto = true;

        if (_uploadFile)
        {
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

                if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize))
                {
                    $('#agriculture-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + 'Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#agriculture-file-uploader-error').removeClass('d-none');

                    $('#agriculture-file-uploader-image-preview').attr('src', '#');
                    $('#agriculture-file-uploader').val('');

                    result = false;
                }
            }
            else
            {
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

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Agriculture Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-agriculture-asset-dt').click(function ()
    {
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
    $('#btn-edit-agriculture-asset-dt').click(function ()
    {
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('agriculture-asset', 'Edit');

        if (hasAnyCourtCase === true) {
            $('#any-court-case-block').removeClass('d-none');
        } else
        {
            $('#any-court-case-block').addClass('d-none');
        }

        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
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

            $('#file-caption', myModal).val(columnValues[23]);

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
        else
        {
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
        ownershipTypeId = $('#ownership-type-id option:selected').val();
        ownershipTypeText = $('#ownership-type-id option:selected').text();
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
        fileCaption = $('#file-caption').val();
        
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
            $('#file-caption').val('None');
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
        if ($('#ownership-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#ownership-type-id-error').removeClass('d-none');
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
            maximumLength = parseInt($('#file-caption').attr('maxlength'));

            if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
                result = false;
                $('#file-caption-agriculture-error').removeClass('d-none');
            }
       
        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsAgricultureDataTable() {
        agricultureDataTable.column(1).visible(false);
        agricultureDataTable.column(8).visible(false);
        agricultureDataTable.column(26).visible(false);
        agricultureDataTable.column(27).visible(false);
        agricultureDataTable.column(28).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;

        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let personFormData;
            personFormData = new FormData($("#form")[0]);

            // Create Array For person agriculture asset Table To Pass Data

            if (agricultureDataTable.data().any()) {
                debugger
                if (isValidAllInputs) {

                    $('#tbl-agriculture-asset > tbody > tr').each(function (i) {
                        currentRow = $(this).closest('tr');

                        columnValues = (agricultureDataTable.row(currentRow).data());

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues === 'undefined' && columnValues === null) {
                            return false;
                        }
                        else
                        {
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
                        alert('An Error Has Occured In Person Agriculture Asset DataTable!!! Error Message - ' + error.toString());
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


