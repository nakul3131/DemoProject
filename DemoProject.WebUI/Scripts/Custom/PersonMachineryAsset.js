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
    debugger;
    const MANDATORY = 'M';

    // @@@@@@@@@@ Data Table Related letible Declaration
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
    let minimumLength = 0;
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
    let fileNameDocument;
    let localStoragePath;
    let personMachineryAssetDocumentPrmKey;
    let selectedDocumentObject;


    //person Machinery Asset
    let nameOfMachinery = '';
    let manufacturingYear = '';
    let machineryFullDetails = '';
    let machineryDateOfPurchase = '';
    let dateOfPurchase = '';
    let numberOfOwners = '';
    let referenceNumber = '';
    let purchasePrice = 0;
    let currentMarketValue = 0;
    let ownershipPercentage = 0;
    let hasAnyMortgage = false;
    let isOwnershipDeceased = false;
    let note = '';
    let reasonForModification = '';

    let today = new Date();

    // *************** C A L L   P A G E   L O A D I N G   D E F A U L T   V A L U E S ***************

    SetPageLoadingDefaultValues();

    function SetPageLoadingDefaultValues() {
        if (($('#machinery-asset-document-upload').val()) === MANDATORY) {
            $('#machinery-file-uploader').addClass('mandatory-mark');
            $('#machinery-file-uploader').attr('required');
            $('#file-caption-machinery').addClass('mandatory-mark');
            $('#file-caption-machinery').attr('required');
        }
        else {
            $('#machinery-file-uploader').removeAttr('required');
            $('#machinery-file-uploader').removeClass('mandatory-mark');
            $('#file-caption-machinery').removeClass('mandatory-mark');
            $('#file-caption-machinery').removeAttr('required');
        }
    }

    // M A I N     P A G E     I N P U T     V A L I D A T I O N 

    // Create DataTables
    let machineryDataTable = CreateDataTable('machinery-asset');

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

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

            case 'machinery-file-uploader':
                docInput = 'MachineryAsset';
                break;

            default:
                docInput = 'None';
        }

        // $('#document-file-uploader-error').addClass('d-none');
        $('#' + myId + '-error').addClass('d-none');
        debugger;
        if (this.files.length > 0) {
            debugger;
            const uploadFile = this.files[0];
            $('#file-caption-machinery').val('');
            // Upload File
            if (IsValidFile(docInput, uploadFile)) {
                let reader = new FileReader();

                reader.onload = function (e) {
                    $('#machinery-file-uploader-image-preview').attr('src', e.target.result);
                }

                reader.readAsDataURL(uploadFile);
            }
        }
        else {
            $('#machinery-file-uploader-image-preview').attr('src', '');
        }

    });

    // Validate Uploaded File
    function IsValidFile(_inputSource, _uploadFile)
    {
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
                    $('#machinery-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + 'Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#machinery-file-uploader-error').removeClass('d-none');

                    $('#machinery-file-uploader-image-preview').attr('src', '#');
                    $('#machinery-file-uploader-uploader').val('');

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

    // Purchase Price 
    $('#purchase-price-machinery-asset').focusout(function ()
    {
        let purchasePrice = parseFloat($('#purchase-price-machinery-asset').val());

        if(isNaN(purchasePrice) === true)
        {
            purchasePrice = 0;
        }

        $('#current-market-value-machinery-asset').attr('max', purchasePrice);
        $('#current-market-value-machinery-asset').val('');
    });

    function ManufacturingYearMachinaryAssetFocusOutEventFunction()
    {
        debugger;
        let today = new Date();

        // Get the manufacturing year from the input field
        let purchaseYear = parseInt($('#manufacturing-year-machinery-asset').val());
        let maxPurchaseYear = parseInt(purchaseYear) + 10;

        let minPurchaseDate = new Date(purchaseYear + '-01-01');
        let maxPurchaseDate = new Date(maxPurchaseYear + '-12-31');

        // If Max Purchase Year Is Larger Than Current Year
        if (parseInt(maxPurchaseYear) > parseInt(today.getFullYear()))
        {
            maxPurchaseYear = today.getFullYear();
        }

        // If Max Purchase Date Is Larger Than Today
        if (maxPurchaseDate > today)
        {
            maxPurchaseDate = today;
        }

        $('#date-of-purchase-machinery-asset').attr('min', GetInputDateFormat(minPurchaseDate));
        $('#date-of-purchase-machinery-asset').attr('max', GetInputDateFormat(maxPurchaseDate));
    }

    // Machinery Asset manufacturing-year-machinery-asset
    $('#manufacturing-year-machinery-asset').focusout(function ()
    {
        ManufacturingYearMachinaryAssetFocusOutEventFunction();
        $('#date-of-purchase-machinery-asset').val('');
    });

    
    /// @@@@@@@@@@@@@@@@@@@@@@  Person Machinery Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
     $('#btn-add-machinery-asset-dt').click(function ()
     {
        event.preventDefault();

        SetModalTitle('machinery-asset', 'Add');

        isDbRecord = false;

        personMachineryAssetDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';
    });

    // DataTable Edit Button 
     $('#btn-edit-machinery-asset-dt').click(function ()
     {
        debugger;

        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('machinery-asset', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
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
            $('#file-caption-machinery', myModal).val(columnValues[14]);
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
            $('#machinery-asset-modal').modal('show');        }
        else
        {
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
        fileCaption = $('#file-caption-machinery').val();

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

        if (reasonForModification === '') {
            $('#reason-for-modification-machinery-asset').val('None');
            reasonForModification = 'None';
        }

        if (fileCaption === '') {
            $('#file-caption-machinery').val('None');
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
        maximumLength = parseInt($('#file-caption-machinery').attr('maxlength'));
        if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#file-caption-machinery-error').removeClass('d-none');
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0)
        {
            if (personInformationParameterViewModel.MachineryAssetDocumentUpload === MANDATORY)
            {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true)
                {
                    result = false;
                    $('#machinery-file-uploader-error').removeClass('d-none');
                }
            }
            else
            {
                let photoSrc = $('#machinery-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2)
                {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsMachineryDataTable() {
        machineryDataTable.column(17).visible(false);
        machineryDataTable.column(18).visible(false);
        machineryDataTable.column(19).visible(false);
    }



    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@


    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;
        if ($('form').valid())
        {
            // not add event.preventDefault
            $('.lastrow').remove();

            let personFormData;
            personFormData = new FormData($("#form")[0]);

            // Create Array For person machinery asset Table To Pass Data
            if (!$('#heading-machinery-asset').hasClass('d-none')) {
                debugger;
                if (machineryDataTable.data().any())
                {

                    $('#machinery-asset-data-table-error').addClass('d-none');

                    if (isValidAllInputs)
                    {
                        debugger;
                        $('#tbl-machinery-asset > tbody > tr').each(function (i)
                        {
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
                    processData: false, // Not to process data 
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