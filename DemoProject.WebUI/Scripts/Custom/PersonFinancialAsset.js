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
    let minimum;
    let maximum;
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
    let fileCaption = '';
    let fileUploaderId = '';
    let fileId = '';
    // ****** New Changes
    let counter = 100;
    let files;
    let personFinancialAssetDocumentPrmKey;
    let fileNameDocument = '';
    let localStoragePath;


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
    let openingDate = '';
    let maturityDate = '';
    let hasAnyMortgage;
    let note = '';
    let transNote = '';
    let reasonForModification = '';
    let financialAssetOpeningDate = '';
    let financialAssetMatureDate = '';
    let lastSelectedValue = '';

    // *************** C A L L   P A G E   L O A D I N G   D E F A U L T   V A L U E S ***************
    SetPageLoadingDefaultValues();

    function SetPageLoadingDefaultValues() {
        debugger;
        if (($('#financial-asset-document-upload').val()) === MANDATORY) {
            $('#finance-file-uploader').attr('required');
            $('#file-caption-finance').attr('required');
            $('#finance-file-uploader').addClass('mandatory-mark');
            $('#file-caption-finance').addClass('mandatory-mark');
        }
        else {
            $('#finance-file-uploader').removeAttr('required');
            $('#file-caption-finance').removeAttr('required');
            $('#finance-file-uploader').removeClass('mandatory-mark');
            $('#file-caption-finance').removeClass('mandatory-mark');
        }
    }

    // M A I N     P A G E     I N P U T     V A L I D A T I O N 

    // Create DataTables
    let financialDataTable = CreateDataTable('financial-asset');

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

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
    $('.doc-upload').change(function ()
    {
        debugger;
        let docInput = '';
        let myId = $('.doc-upload').attr('id');

        // Document
        switch (myId)
        {
            case 'finance-file-uploader':
                docInput = 'FinancialAsset';
                break;
            default:
                docInput = 'None';
        }

        // $('#document-file-uploader-error').addClass('d-none');
        $('#' + myId + '-error').addClass('d-none');

        if (this.files.length > 0)
        {
            const uploadFile = this.files[0];

            $('#file-caption-finance').val('');

            // Upload File
            if (IsValidFile(docInput, uploadFile))
            {
                let reader = new FileReader();

                reader.onload = function (e)
                {
                    $('#finance-file-uploader-image-preview').attr('src', e.target.result);
                }

                reader.readAsDataURL(uploadFile);
            }
        }
        else
        {
            $('#finance-file-uploader-image-preview').attr('src', '');
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
            if (_inputSource === 'Document')
            {
                validDocumentFileExtensions = (selectedDocumentObject[0].EnableDocumentUploadInDb ? selectedDocumentObject[0].DocumentAllowedFileFormatsForDb : selectedDocumentObject[0].DocumentAllowedFileFormatsForLocalStorage).toLowerCase().replace('.', '');
                maxDocumentFileSize = selectedDocumentObject[0].EnableDocumentUploadInDb ? selectedDocumentObject[0].MaximumFileSizeForDocumentUploadInDb : selectedDocumentObject[0].MaximumFileSizeForDocumentUploadInLocalStorage;

                if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize)) {
                    $('#finance-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + 'Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#finance-file-uploader-error').removeClass('d-none');

                    $('#finance-file-uploader-image-preview').attr('src', '#');
                    $('#finance-file-uploader').val('');

                    result = false;
                }
            }
            else {
                debugger;
                let uploaderId = _inputSource.replace('Asset', '').toLowerCase();

                if (_inputSource === 'FinancialAsset') {
                    uploaderId = 'finance';
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

    // Clear the monthly interest income amount when invested amount changes
    $('#invested-amount').focusout(function () {
        debugger;
        let investedAmount = parseFloat($(this).val());

        // Set max interest to 20% of invested amount
        let maxInterest = investedAmount * 0.2 ;
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

     $('#financial-organization-type').focusout(function ()
     {
         let currentValue = $(this).val();

         // If the value has changed from the initial value, clear the related fields
         if (currentValue !== lastSelectedValue)
         {
             $('.switch-input').prop('checked', false);
             $('select').not('#financial-organization-type').prop('selectedIndex', 0);
             $('input[type="number"], input[type="date"], textarea').val('');
             $('input[type="text"]').not('#checker-remark, #maker-remark, #o-remark').val('');
             $('#finance-file-uploader').val('');
             $('#finance-file-uploader-image-preview').attr('src', '');
         }

         // Update lastSelectedValue to the current value for subsequent changes
         lastSelectedValue = currentValue;
    });

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Financial Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-financial-asset-dt').click(function ()
    {
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

        if (isChecked)
        {
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
            $('#file-caption-finance', myModal).val(columnValues[25]);

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
            if (fileUploader.files.length > 0)
            {
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
        if (IsValidFinancialModal())
        {
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
        fileCaption = $('#file-caption-finance').val();

        if (isDbRecord === false || isChangedPhoto === true)
        {
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

        if (fileCaption === '')
        {
            $('#file-caption-finance').val('None');
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
        if (fileUploader.files.length === 0)
        {
            if (personInformationParameterViewModel.FinancialAssetDocumentUpload === MANDATORY)
            {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true)
                {
                    result = false;
                    $('#finance-file-uploader-error').removeClass('d-none');
                }
            }
            // ****** New Changes
            else
            {
                let photoSrc = $('#finance-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2)
                {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }                
            }
            // ***********
        }

        //filecaption
        maximumLength = parseInt($('#file-caption-finance').attr('maxlength'));

        if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#file-caption-finance-error').removeClass('d-none');
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

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@


    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;

        let personFormData;
        personFormData = new FormData($("#form")[0]);

        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // Create Array For person financial asset Table To Pass Data
            if (!$('#heading-financial-asset').hasClass('d-none')) {
                debugger;
                if (financialDataTable.data().any())
                {
                    if (isValidAllInputs)
                    {
                        $('#tbl-financial-asset > tbody > tr').each(function (i)
                        {
                            currentRow = $(this).closest('tr');

                            columnValues = (financialDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null)
                            {
                                return false;
                            }
                            else
                            {
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