'use strict'
$(document).ready(function ()
{
    const DISABLE = 'D';
    const MANDATORY = 'M';

    let isDbRecord = false;
    let isChangedPhoto = false;
    let personMovableAssetDocumentPrmKey = 0;
    let fileNameDocument = '';
    let localStoragePath = '';

    // Global 
    // Common Variables
    let result = true;
    let minimum = 0;
    let maximum = 0;
    let maximumLength = 0;
    let selectedDocumentObject;
    let today;
    let rowNum = 0;
    let listItemCount = 0;

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
    let registrationDate = '';
    let registrationNumber = 0;
    let currentMarketValue = 0;
    let hasAnyMortgage;

    //Movable Asset
    let vehicleMakeId = '';
    let vehicleMakeIdText = '';
    let vehicleModelId = '';
    let vehicleModelIdText = '';
    let vehicleletiant = '';
    let vehicleletiantText = '';
    let numberOfOwners = '';
    let manufacturingYear;
    let dateOfPurchase;
    let purchasePrice = 0;
    let ownershipPercentage = 0;
    let isOwnershipDeceased = false;
    let movableAssetRegistrationDate = '';
    let movableDateOfPurchase = '';
    let letiantList = '';
    let PrmKey = 0;
    let i;
    let letiant = '';
    let vehicleModelEditedId = '';
    let vehicleVariantEditedId = '';

    let dropDownListItemCount = 0;
    let dropdownListItems = '';

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

    let lastVehicleMakeSelectedValue = '';
    let lastVehicleModelSelectedValue = '';

    //  ************** Create Data Table  **************

    let movableDataTable = CreateDataTable('movable-asset');

    // *************** C A L L   P A G E   L O A D I N G   D E F A U L T   V A L U E S ***************
    SetPageLoadingDefaultValues();

    function SetPageLoadingDefaultValues()
    {
        debugger;
        if (($('#movable-asset-document-upload').val()) === MANDATORY)
        {
            $('#movable-file-uploader').attr('required');
            $('#file-caption-movable').attr('required');
            $('#movable-file-uploader').addClass('mandatory-mark');
            $('#file-caption-movable').addClass('mandatory-mark');
        }
        else
        {
            $('#movable-file-uploader').removeAttr('required');
            $('#file-caption-movable').removeAttr('required');
            $('#movable-file-uploader').removeClass('mandatory-mark');
            $('#file-caption-movable').removeClass('mandatory-mark');
        }
    }

    function SetVehicleModelDropdownList()
    {
        $.get('/DynamicDropdownList/GetVehicleModelDropdownListByVehicleMakeId', { _vehicleMakeId: vehicleMakeId, async: false }, function (data) {
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Valid Vehicle Model --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#vehicle-model-id').html(dropdownListItems);

            listItemCount = $('#vehicle-model-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1)
            {
                $('#vehicle-model-id').prop('selectedIndex', 1);
            }
            else
            {
                if (vehicleModelEditedId !== '')
                {
                    $('#vehicle-model-id').val(vehicleModelEditedId);
                    SetVehicleVariantDropdownList();
                }
            }
        });
    }

    function SetVehicleVariantDropdownList()
    {
        $.get('/DynamicDropdownList/GetVehicleVariantDropdownListByVehicleModelId', { _vehicleModelId: vehicleModelId, async: false }, function (data)
        {
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Valid Vehicle Variant --- </option>';

            $.each(data, function (index, selectListItemObj)
            {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#vehicle-variant-id').html(dropdownListItems);

            listItemCount = $('#vehicle-variant-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1)
            {
                $('#vehicle-variant-id').prop('selectedIndex', 1);
            }
            else
            {
                if (vehicleVariantEditedId !== '')
                {
                    $('#vehicle-variant-id').val(vehicleVariantEditedId);
                }
            }
        });
    }

    function ManufacturingYearMovableAssetFocusOutEventFunction()
    {
        debugger;
        let today = new Date();

        // Get the manufacturing year from the input field
        let purchaseYear = parseInt($('#manufacturing-year-movable-asset').val());
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

        $('#date-of-purchase-movable-asset').attr('min', GetInputDateFormat(minPurchaseDate));
        $('#date-of-purchase-movable-asset').attr('max', GetInputDateFormat(maxPurchaseDate));
    }

    function IsValidRegistrationNumber()
    {
        debugger;
        let regNumber = $("#registration-number-movable-asset").val();
        let regExp = /^[A-Z]{2}[0-9]{2}[A-Z]{1,2}[0-9]{4}$/;

        if (regExp.test(regNumber))
        {
            $('#registration-number-movable-asset-error').addClass('d-none');
        }
        else
        {
            result = false;
            $('#registration-number-movable-asset-error').removeClass('d-none');
        }
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Number Of Owners - Clear Dependent Data
    $('#number-of-owners-movable-asset').focusout(function ()
    {
        // Clear Dependent Data
        $('#manufacturing-year-movable-asset').val('');
        $('#date-of-purchase-movable-asset').val('');
        $('#registration-date-movable-asset').val('');
    });

    // Purchase Date
    $('#date-of-purchase-movable-asset').click(function ()
    {
        $('#registration-date-movable-asset').val('');

    });

    function RegistrationDateClickEventFunction()
    {
        // Validate Date If Owner Is 2 Or More - 
        // If 1 - Date Of Purchase Always Earlier Or Same Than Registration
        // If More Than 1 - Date Of Purchase Always Higer Than Registration

        let today = new Date();

        if (parseInt($('#number-of-owners-movable-asset').val()) === 1)
        {
            let dateOfPurchase = new Date($('#date-of-purchase-movable-asset').val());

            // Allow 10 Years Gap Between Purchase And Registration
            let maxRegistrationDate = new Date(dateOfPurchase);
            maxRegistrationDate.setFullYear(maxRegistrationDate.getFullYear() + 10);

            // If Max Registration Date Is Larger Than Today
            if (maxRegistrationDate > today) {
                maxRegistrationDate = today;
            }

            $('#registration-date-movable-asset').attr('min', GetInputDateFormat(dateOfPurchase));
            $('#registration-date-movable-asset').attr('max', GetInputDateFormat(maxRegistrationDate));
        }
        else
        {
            // For More Than 1 Owner Registration Date Must Earlier Than Purchase Date
            let dateOfPurchase = new Date($('#date-of-purchase-movable-asset').val());

            // Allow Older Date Upto Manufacture Year
            let minRegistrationDate = new Date($('#manufacturing-year-movable-asset').val() + '-01-01');

            $('#registration-date-movable-asset').attr('min', GetInputDateFormat(minRegistrationDate));
            $('#registration-date-movable-asset').attr('max', GetInputDateFormat(dateOfPurchase));
        }
    }

    // Registration Date
    $('#registration-date-movable-asset').click(function ()
    {
        RegistrationDateClickEventFunction();
    });

    // Purchase Price
    $('#purchase-price-movable-asset').focusout(function ()
    {
        $('#current-market-value-movable-asset').attr('max', $(this).val());
    });

    // Manufacturing Year
    $('#manufacturing-year-movable-asset').focusout(function ()
    {
        ManufacturingYearMovableAssetFocusOutEventFunction();
        $('#date-of-purchase-movable-asset').val('');
    });

    // Vehicle Make
    $('#vehicle-make-id').focusout(function ()
    {
        let currentValue = $(this).val();

        vehicleMakeId = $(this).val();
        //$('#vehicle-model-id').val('');

        if (currentValue !== lastVehicleMakeSelectedValue)
        {
            $('.switch-input').prop('checked', false);
            $('select').not('#vehicle-make-id').prop('selectedIndex', 0);
            $('input[type="number"], input[type="date"], textarea').val('');
            $('input[type="text"]').not('#checker-remark, #maker-remark , #o-remark').val('');
            $('#movable-file-uploader').val('');
            $('#movable-file-uploader-image-preview').attr('src', '');
            $('.modal-input-error').addClass('d-none');
        }

        SetVehicleModelDropdownList();
    });

    // Vehicle Model
    $('#vehicle-model-id').focusout(function () {

        let currentValue = $(this).val();

        vehicleModelId = currentValue;

        //$('#vehicle-model-id').val('');

        if (currentValue !== lastVehicleModelSelectedValue) {
            $('.switch-input').prop('checked', false);
            $('select').not('#vehicle-make-id, #vehicle-model-id').prop('selectedIndex', 0);
            $('input[type="number"], input[type="date"], textarea').val('');
            $('input[type="text"]').not('#checker-remark, #maker-remark , #o-remark').val('');
            $('#movable-file-uploader').val('');
            $('#movable-file-uploader-image-preview').attr('src', '');
            $('.modal-input-error').addClass('d-none');
        }

        SetVehicleVariantDropdownList();
    });

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme Document - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Attach FileUploader Control

    function AttachFileUploader()
    {
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
    $('.doc-upload').change(function ()
    {
        debugger;
        let docInput = '';
        let myId = $('.doc-upload').attr('id');

        // Document
        switch (myId) {
            case 'movable-file-uploader':
                docInput = 'MovableAsset';
                break;
            default:
                docInput = 'None';

        }

        // $('#document-file-uploader-error').addClass('d-none');
        $('#' + myId + '-error').addClass('d-none');

        if (this.files.length > 0) {
            const uploadFile = this.files[0];
            $('#file-caption-movable').val('');
            // Upload File
            if (IsValidFile(docInput, uploadFile)) {
                let reader = new FileReader();

                reader.onload = function (e) {
                    $('#movable-file-uploader-image-preview').attr('src', e.target.result);
                }

                reader.readAsDataURL(uploadFile);
            }
        }
        else {
            $('#movable-file-uploader-image-preview').attr('src', '');
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
                    $('#movable-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + 'Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#movable-file-uploader-error').removeClass('d-none');

                    $('#movable-file-uploader-image-preview').attr('src', '#');
                    $('#movable-file-uploader').val('');

                    result = false;
                }
            }
            else {
                debugger;
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

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Movable Asset - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-movable-asset-dt').click(function ()
    {
        event.preventDefault();

        isDbRecord = false;

        lastVehicleMakeSelectedValue = '';
        lastVehicleModelSelectedValue = '';
        vehicleModelEditedId = '';
        vehicleVariantEditedId = '';

        vehicleMakeId = '';
        vehicleModelId = '';

        personMovableAssetDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';

        SetModalTitle('movable-asset', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-movable-asset-dt').click(function ()
    {
        debugger;
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('movable-asset', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#movable-asset-modal').modal();

            let tmpDate = new Date();

            columnValues = $('#btn-edit-movable-asset-dt').data('rowindex');

            movableDateOfPurchase = new Date(columnValues[9]);
            movableAssetRegistrationDate = new Date(columnValues[10]);

            lastVehicleMakeSelectedValue = columnValues[1];
            lastVehicleModelSelectedValue = columnValues[3];
            vehicleModelEditedId = columnValues[3];
            vehicleVariantEditedId = columnValues[5];

            vehicleMakeId = columnValues[1];
            vehicleModelId = columnValues[3];

            tmpDate = new Date(columnValues[9]);
            tmpDate.setDate(tmpDate.getDate());

            $('#vehicle-make-id', myModal).val(columnValues[1]);

            SetVehicleModelDropdownList();

            $('#vehicle-model-id', myModal).val(columnValues[3]);

            SetVehicleVariantDropdownList();

            $('#current-market-value-movable-asset').attr('max', columnValues[12]);

            $('#vehicle-variant-id', myModal).val(columnValues[5]);
            $('#number-of-owners-movable-asset', myModal).val(columnValues[7]);
            $('#manufacturing-year-movable-asset', myModal).val(columnValues[8]);
            $('#date-of-purchase-movable-asset', myModal).val(GetInputDateFormat(movableDateOfPurchase));
            $('#registration-date-movable-asset', myModal).val(GetInputDateFormat(movableAssetRegistrationDate));
            $('#registration-number-movable-asset', myModal).val(columnValues[11]);
            $('#purchase-price-movable-asset', myModal).val(columnValues[12]);
            $('#current-market-value-movable-asset', myModal).val(columnValues[13]);
            $('#ownership-percentage-movable-asset', myModal).val(columnValues[14]);

            $('#has-any-mortgage-movable').prop('checked', columnValues[15].toString().toLowerCase() === 'true' ? true : false);

            $('#is-ownership-deceased-movable').prop('checked', columnValues[16].toString().toLowerCase() === 'true' ? true : false);

            $('#file-caption-movable', myModal).val(columnValues[19]);
            $('#note-movable-asset', myModal).val(columnValues[20]);
            $('#reason-for-modification-movable-asset', myModal).val(columnValues[21]);

            fileUploader = $('#' + $(columnValues[17]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'movable-file-uploader';

            // columnValues[4] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[17]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[17]).attr('class') === 'db-record' ? true : false;

            // columnValues[5] - Image Tag Html
            filePath = $('#' + $(columnValues[18]).attr('id')).attr('src');

            fileNameDocument = columnValues[22];
            personMovableAssetDocumentPrmKey = columnValues[23];
            localStoragePath = columnValues[24];

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                AttachFileUploader();
            }

            ManufacturingYearMovableAssetFocusOutEventFunction();
            RegistrationDateClickEventFunction();

            // Auto Called Vehicle Variant DropdownList
            SetVehicleModelDropdownList();

            $('#movable-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#movable-asset-modal').modal('show');
        }
        else {
            $('#btn-edit-movable-asset-edit-dt').addClass('read-only');
            $('#movable-asset-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-movable-asset-modal').click(function (event) {
        debugger;
        if (IsValidMovableAssetModal())
        {
            debugger;
            row = movableDataTable.row.add([
                tag,
                vehicleMakeId,
                vehicleMakeIdText,
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
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                personMovableAssetDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            $('#movable-asset-data-table-error').addClass('d-none');

            HideColumnsMovableAssetDataTable();

            movableDataTable.columns.adjust().draw();

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
                vehicleMakeId,
                vehicleMakeIdText,
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
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                personMovableAssetDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
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
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-movable-asset tbody input[type="checkbox"]:checked').each(function () {
                    movableDataTable.row($('#tbl-movable-asset tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-movable-asset-dt').data('rowindex');
                    EnableNewOperation('movable-asset');

                    $('#select-all-movable-asset').prop('checked', false);
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

                    EnableEditDeleteOperation('turn-over-limit');

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
        debugger;
        result = true;

        counter++;
        fileUploaderId = 'data-table-movable-file-uploader' + counter;
        fileId = "photo-Id" + counter;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        vehicleMakeId = $('#vehicle-make-id option:selected').val();
        vehicleMakeIdText = $('#vehicle-make-id option:selected').text();
        vehicleModelId = $('#vehicle-model-id option:selected').val();
        vehicleModelIdText = $('#vehicle-model-id option:selected').text(); vehicleletiant = $('#vehicle-variant-id option:selected').val();
        vehicleletiantText = $('#vehicle-variant-id option:selected').text();
        numberOfOwners = parseInt($('#number-of-owners-movable-asset').val());
        manufacturingYear = parseInt($('#manufacturing-year-movable-asset').val());
        dateOfPurchase = $('#date-of-purchase-movable-asset').val();
        registrationDate = $('#registration-date-movable-asset').val();
        registrationNumber = $('#registration-number-movable-asset').val();
        purchasePrice = parseFloat($('#purchase-price-movable-asset').val());
        currentMarketValue = parseFloat($('#current-market-value-movable-asset').val());
        ownershipPercentage = parseFloat($('#ownership-percentage-movable-asset').val());
        isOwnershipDeceased = $('#is-ownership-deceased-movable').is(':checked') ? true : false;
        hasAnyMortgage = $('#has-any-mortgage-movable').is(':checked') ? true : false;
        note = $('#note-movable-asset').val();
        PrmKey = 0;
        filePath = $('#movable-file-uploader-image-preview').prop('src');
        fileUploader = $('#movable-file-uploader').get(0);
        fileCaption = $('#file-caption-movable').val();
        reasonForModification = $('#reason-for-modification-movable-asset').val().trim();

        if (isDbRecord === false || isChangedPhoto === true)
        {
            filePath = $('#movable-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        }
        else
        {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        //Set Default Value if Empty
        if (note === '') {
            $('#note-movable-asset').val('None');
            note = 'None';
        }
            
        if (fileCaption === '') {
            $('#file-caption-movable').val('None');
            fileCaption = 'None';
        }

        
        if (reasonForModification === '') {
            $('#reason-for-modification-movable-asset').val('None');
            reasonForModification = 'None';
        }

        if ($('#vehicle-make-id').prop('selectedIndex') < 1) {
            result = false;
            $('#vehicle-make-id-error').removeClass('d-none');
        }

        //vehicle model Id
        if ($('#vehicle-model-id').prop('selectedIndex') < 1) {
            result = false;
            $('#vehicle-model-id-error').removeClass('d-none');
        }

        //Vehicle Varient Id
        if ($('#vehicle-variant-id').prop('selectedIndex') < 1) {
            result = false;
            $('#vehicle-variant-id-error').removeClass('d-none');
        }

        // number of Owners
        if (isNaN(numberOfOwners) === false) {
            minimum = parseInt($('#number-of-owners-movable-asset').attr('min'));
            maximum = parseInt($('#number-of-owners-movable-asset').attr('max'));
            if (parseInt(numberOfOwners) < parseInt(minimum) || parseInt(numberOfOwners) > parseInt(maximum)) {
                result = false;
                $('#number-of-owners-movable-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#number-of-owners-movable-asset-error').removeClass('d-none');
        }

        // Get the current year
        let currentYear = new Date().getFullYear();

        // Calculate the minimum allowed year
        let minAllowedYear = currentYear - 50;

        if (isNaN(manufacturingYear) === false) {
            if (parseInt(manufacturingYear) < parseInt(minAllowedYear) || parseInt(manufacturingYear) > parseInt(currentYear)) {
                result = false;
                $('#manufacturing-year-movable-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#manufacturing-year-movable-asset-error').removeClass('d-none');
        }

        //date of purchase
        let isValidDateOfPurchase = IsValidInputDate('#date-of-purchase-movable-asset');

        if (isValidDateOfPurchase === false) {
            result = false;
            $('#date-of-purchase-movable-asset-error').removeClass('d-none');
        }

        let isValidRegistrationDate = IsValidInputDate('#registration-date-movable-asset');

        if (isValidRegistrationDate === false) {
            result = false;
            $('#registration-date-movable-asset-error').removeClass('d-none');
        }

        IsValidRegistrationNumber();

        //purchase price
        if (isNaN(purchasePrice) === false) {
            minimum = parseFloat($('#purchase-price-movable-asset').attr('min'));
            maximum = parseFloat($('#purchase-price-movable-asset').attr('max'));

            if (parseFloat(purchasePrice) < parseFloat(minimum) || parseFloat(purchasePrice) > parseFloat(maximum)) {
                result = false;
                $('#purchase-price-movable-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#purchase-price-movable-asset-error').removeClass('d-none');
        }

        //current Market Value
        if (isNaN(currentMarketValue) === false) {
            minimum = parseFloat($('#current-market-value-movable-asset').attr('min'));
            maximum = parseFloat($('#current-market-value-movable-asset').attr('max'));

            if (parseFloat(currentMarketValue) < parseFloat(minimum) || parseFloat(currentMarketValue) > parseFloat(maximum)) {
                result = false;
                $('#current-market-value-movable-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#current-market-value-movable-asset-error').removeClass('d-none');
        }

        //ownership percentage
        if (isNaN(ownershipPercentage) === false) {
            minimum = parseFloat($('#ownership-percentage-movable-asset').attr('min'));
            maximum = parseFloat($('#ownership-percentage-movable-asset').attr('max'));

            if (parseFloat(ownershipPercentage) < parseFloat(minimum) || parseFloat(ownershipPercentage) > parseFloat(maximum)) {
                result = false;
                $('#ownership-percentage-movable-asset-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#ownership-percentage-movable-asset-error').removeClass('d-none');
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.MovableAssetDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#movable-file-uploader-error').removeClass('d-none');
                }
            }
            else {
                let photoSrc = $('#movable-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
        }

        if (isNaN(fileCaption.length) === false) {
            maximumLength = parseInt($('#file-caption-movable').attr('maxlength'));

            if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
                result = false;
                $('#file-caption-movable-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#file-caption-movable-error').removeClass('d-none');
        }


        if (result) {
            $('#movable-asset-data-table-error').addClass('d-none');
        }
        else {
            $('#movable-asset-data-table-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsMovableAssetDataTable() {
        movableDataTable.column(1).visible(false);
        movableDataTable.column(3).visible(false);
        movableDataTable.column(5).visible(false);
        movableDataTable.column(22).visible(false);
        movableDataTable.column(23).visible(false);
        movableDataTable.column(24).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function ()
    {
        let isValidAllInputs = true;

        //if ($('form').valid()
        if ($('form').valid())
        {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let personFormData;
            personFormData = new FormData($("#form")[0]);

            // Create Array For person movable asset Table To Pass Data
            if (movableDataTable.data().any())
            {
                if (isValidAllInputs)
                {
                    $('#tbl-movable-asset > tbody > tr').each(function (i)
                    {
                        currentRow = $(this).closest('tr');

                        columnValues = (movableDataTable.row(currentRow).data());

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues === 'undefined' && columnValues === null)
                        {
                            return false;
                        }
                        else
                        {
                            personFormData.append("_movableAsset[" + i + "].VehicleVariantId", columnValues[5]);
                            personFormData.append("_movableAsset[" + i + "].NumberOfOwners", columnValues[7]);
                            personFormData.append("_movableAsset[" + i + "].ManufacturingYear", columnValues[8]);
                            personFormData.append("_movableAsset[" + i + "].PurchaseDate", columnValues[9]);
                            personFormData.append("_movableAsset[" + i + "].RegistrationDate", columnValues[10]);
                            personFormData.append("_movableAsset[" + i + "].RegistrationNumber", columnValues[11]);
                            personFormData.append("_movableAsset[" + i + "].PurchasePrice", columnValues[12]);
                            personFormData.append("_movableAsset[" + i + "].CurrentMarketValue", columnValues[13]);
                            personFormData.append("_movableAsset[" + i + "].OwnershipPercentage", columnValues[14]);
                            personFormData.append("_movableAsset[" + i + "].HasAnyMortgage", columnValues[15]);
                            personFormData.append("_movableAsset[" + i + "].IsOwnershipDeceased", columnValues[16]);
                            personFormData.append("_movableAsset[" + i + "].PhotoPathMovable", $(currentRow).find("TD").find("input[type='file']").get(0).files[0]);
                            personFormData.append("_movableAsset[" + i + "].FileCaption", columnValues[19]);
                            personFormData.append("_movableAsset[" + i + "].Note", columnValues[20]);
                            personFormData.append("_movableAsset[" + i + "].ReasonForModification", columnValues[21]);
                            personFormData.append("_movableAsset[" + i + "].NameOfFile", columnValues[22]);
                            personFormData.append("_movableAsset[" + i + "].PersonMovableAssetDocumentPrmKey", columnValues[23]);
                            personFormData.append("_movableAsset[" + i + "].LocalStoragePath", columnValues[24]);
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
                        alert('An Error Has Occured In Person movable Asset DataTable!!! Error Message - ' + error.toString());
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


