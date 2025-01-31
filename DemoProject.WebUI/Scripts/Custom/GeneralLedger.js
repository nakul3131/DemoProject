'use strict'
$(document).ready(function () {
    let today;
    let month;
    let day;
    let date;
    let year;
    let datepart;
    let divErrorCount = 0;
    let selectedRow;
    let enableMemberNumber;
    let enableAccountNumber;
    let EnableApplication;
    let tenTimeRetailAccountTurnOverLimit = 0;
    let dd;
    let mm;
    let yyyy;
    let today1;
    let today2;

    // @@@@@@@@@@ Data Table Related Varible Declaration
    let tag = '';
    let id;
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

    let arr = new Array();
    let arrayCloumn1;

    //Validation Data Table
    let minimum = 0;
    let maximum = 0;
    let isValidAmountLimit = true;
    let result = true;
    let isValidBackDaysEntry = true;
    let isValidActivationDate;
    let isValidExpiryDate;

    //BusinessOffice
    let businessOfficeId;
    let businessOfficeText;
    let activationDate;
    let expiryDate;
    let closeDate;
    let note;

    //Currency
    let currencyId;
    let currencyText;
    let schemeId;
    let schemeText;
    let transactionTypeId;
    let transactionTypeText;
    let minimumAmountLimit;
    let maximumAmountLimit;
    let allowedMaximumNumberOfBackDaysEntry;
    let customerTypeId;
    let customerTypeText;

    // M A I N     P A G E     I N P U T     V A L I D A T I O N 
    let url = window.location.href;
    let index = url.lastIndexOf('/') + 1;
    let filenameWithExtension = url.substr(index);
    let filename = filenameWithExtension.split('?')[0]; // <-- added this line
    let winname = filename;

    //Data Table Declaration
    let businessOfficeDataTable = CreateDataTable('business-office');
    let currencyDataTable = CreateDataTable('currency');
    let schemeDataTable = CreateDataTable('scheme');
    let transactionTypeDataTable = CreateDataTable('transaction-type');
    let customerTypeDataTable = CreateDataTable('customer-type');

    $('#account-class-id').focusout(function () {
        debugger;
        let selectedAccountClass = $('#account-class-id option:selected').val();
        $.get('/GeneralLedger/GetNumberOfGeneralLegerLimit', { _accountClassId: selectedAccountClass, async: false }, function (data, textStaus, jqXHR) {
            debugger;
            if (data[0] >= data[1]) {
                alert('GeneralLedger Boundary Limit Exceed');
            }
        });
    });

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@ Business Office - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    ClearModal('business-office');

    // DataTable Add Button 
    $('#btn-add-business-office-dt').click(function () {
        event.preventDefault();
        debugger;
        SetModalTitle('business-office', 'Add');

        $('#business-office-id').hide();
        $('#business-office-id').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();
    });

    // DataTable Edit Button 
    $('#btn-edit-business-office-dt').click(function () {
        debugger;
        SetModalTitle('business-office', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-business-office-dt').data('rowindex');
            id = $('#business-office-modal').attr('id');
            myModal = $('#' + id).modal();
            //// Display Value In Modal Inputs
            // Get Only Activation Date
            datepart = columnValues[3].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[3]
            }

            const t = new Date(datepart);

            today = t.toLocaleDateString("en-US");

            const date = ('0' + t.getDate()).slice(-2);
            const month = ('0' + (t.getMonth() + 1)).slice(-2);
            const year = t.getFullYear();

            if (isNaN(year) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                activationDate = [yyyy, mm, dd].join('-');
            }
            else {
                activationDate = [year, month, date].join('-');
            }

            // Get Only Expiry Date
            datepart = columnValues[4].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[4]
            }

            const t1 = new Date(datepart);

            today1 = t1.toLocaleDateString("en-US");

            const date1 = ('0' + t1.getDate()).slice(-2);
            const month1 = ('0' + (t1.getMonth() + 1)).slice(-2);
            const year1 = t1.getFullYear();

            if (isNaN(year1) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                expiryDate = [yyyy, mm, dd].join('-');
            }
            else {
                expiryDate = [year1, month1, date1].join('-');
            }

            // Get Only Close Date
            datepart = columnValues[5].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[5]
            }

            const t2 = new Date(datepart);

            today2 = t2.toLocaleDateString("en-US");

            const date2 = ('0' + t2.getDate()).slice(-2);
            const month2 = ('0' + (t2.getMonth() + 1)).slice(-2);
            const year2 = t2.getFullYear();

            if (isNaN(year2) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                closeDate = [yyyy, mm, dd].join('-');
            }
            else {
                closeDate = [year2, month2, date2].join('-');
            }

            // Display Value In Modal Inputs
            $('#business-office-id').removeAttr('style');
            $('#business-office-id').removeAttr('multiple');
            $('.ms-options-wrap').css('display', 'none');
            $('#business-office-id', myModal).val(columnValues[1]);
            $('#activation-date-business-office', myModal).val(activationDate);
            $('#expiry-date-business-office', myModal).val(expiryDate);
            $('#close-date-business-office', myModal).val(closeDate);
            $('#note-business-office', myModal).val(columnValues[6]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('.btn-edit-business-office-edit-dt').addClass('read-only');
            $('#business-office-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-business-office-modal').click(function (event) {
        let businessOfficeId = [];
        let businessOfficeText = [];

        $('#business-office-id option:selected').each(function () {
            businessOfficeId.push($(this).val());
            businessOfficeText.push($(this).text());
        });
        debugger;
        if (IsValidBusinessOfficeDataTableModal()) {
            for (let i = 0, j = 0; i < businessOfficeId.length, j < businessOfficeText.length; i++, j++) {
                row = businessOfficeDataTable.row.add([
                    tag,
                    businessOfficeId[i],
                    businessOfficeText[j],
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                ]).draw();

                rowNum++;
                row.nodes().to$().attr('id', 'tr' + rowNum);

                // Error Message In Span
                $('#business-office-validation span').html('');

                HideBusinessOfficeDataTableColumns();

                businessOfficeDataTable.columns.adjust().draw();

                ClearModal('business-office');

                $('#business-office-modal').modal('hide');

                EnableNewOperation('business-office');
            }
        }
    });

    // Modal update Button Event
    $('#btn-update-business-office-modal').click(function (event) {

        $('#select-all-business-office').prop('checked', false);
        if (IsValidBusinessOfficeDataTableModal()) {
            businessOfficeDataTable.row($(this).attr('rowindex')).data([
                tag,
                businessOfficeId,
                businessOfficeText,
                activationDate,
                expiryDate,
                closeDate,
                note
            ]).draw();

            // Hide the element with id 'business-office-id'
            $('#business-office-id').hide();
            // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
            $('#business-office-id').attr('multiple', 'multiple');
            // Show elements with class 'ms-options-wrap'
            $('.ms-options-wrap').show();

            // Error Message In Span
            $('#business-office-validation span').html('');

            HideBusinessOfficeDataTableColumns();

            businessOfficeDataTable.columns.adjust().draw();

            $('#business-office-modal').modal('hide');

            EnableNewOperation('business-office');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-business-office-dt').click(function (event) {
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($("#tbl-business-office tbody input[type='checkbox']:checked").each(function () {
                    businessOfficeDataTable.row($("#tbl-business-office tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-business-office-dt').data('rowindex');
                    EnableNewOperation('business-office');

                    $('#select-all-business-office').prop('checked', false);

                    // Hide the element with id 'business-office-id'
                    $('#business-office-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
                    $('#business-office-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();

                    // Display Error, If Table Has Not Any Record
                    if (!businessOfficeDataTable.data().any())
                        $('#business-office-data-table-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-business-office').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-business-office tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = businessOfficeDataTable.row(row).index();

                rowData = (businessOfficeDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-business-office-dt').data('rowindex', arr);
                EnableDeleteOperation('business-office')
            });
        }
        else {
            EnableNewOperation('business-office')

            $('#tbl-business-office tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-business-office tbody').click('input[type="checkbox"]', function () {
        $('#tbl-business-office input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = businessOfficeDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (businessOfficeDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('business-office');

                    $('#btn-update-business-office-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-business-office-dt').data('rowindex', rowData);
                    $('#btn-delete-business-office-dt').data('rowindex', arr);
                    $('#select-all-business-office').data('rowindex', arr);
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
            EnableNewOperation('business-office');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('business-office');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('business-office');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-business-office').prop('checked', true);
        else
            $('#select-all-business-office').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-business-office> tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (businessOfficeDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#business-office-general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate Agent Incentive Module
    function IsValidBusinessOfficeDataTableModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        businessOfficeId = $('#business-office-id option:selected').val();
        businessOfficeText = $('#business-office-id option:selected').text();
        activationDate = $('#activation-date-business-office').val();
        expiryDate = $('#expiry-date-business-office').val();
        closeDate = $('#close-date-business-office').val();
        note = $('#note-business-office').val();

        // Set Default Value, If Empty
        if (note == '')
            note = 'None';

        isValidActivationDate = IsValidInputDate('#activation-date-business-office');
        isValidExpiryDate = IsValidInputDate('#expiry-date-business-office');

        // Validate Modal Inputs
        if (businessOfficeId === '') {
            result = false;
            $('#business-office-id-error').removeClass('d-none');
        }

        if (isValidActivationDate === false) {
            result = false;
            $('#activation-date-business-office-error').removeClass('d-none');
        }

        if (isValidExpiryDate === false) {
            result = false;
            $('#expiry-date-business-office-error').removeClass('d-none');
        }
        if (result) {
            return true;
        }
        else {
            return false;
        }

    }

    // Hide Unnecessary Columns
    function HideBusinessOfficeDataTableColumns() {
        businessOfficeDataTable.column(1).visible(false);
        businessOfficeDataTable.column(5).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Currency - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearGenderModalInputs();
    ClearModal('currency');

    // DataTable Add Button 
    $('#btn-add-currency-dt').click(function () {
        event.preventDefault();
        debugger;
        SetModalTitle('currency', 'Add');

        $('#currency-id').hide();
        $('#currency-id').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();
    });

    // DataTable Edit Button 
    $('#btn-edit-currency-dt').click(function () {
        SetModalTitle('currency', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-currency-dt').data('rowindex');
            id = $('#currency-modal').attr('id');
            myModal = $('#' + id).modal();
            //// Display Value In Modal Inputs
            // Get Only Activation Date
            datepart = columnValues[3].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[3]
            }

            const t = new Date(datepart);

            today = t.toLocaleDateString("en-US");

            const date = ('0' + t.getDate()).slice(-2);
            const month = ('0' + (t.getMonth() + 1)).slice(-2);
            const year = t.getFullYear();

            if (isNaN(year) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                activationDate = [yyyy, mm, dd].join('-');
            }
            else {
                activationDate = [year, month, date].join('-');
            }

            // Get Only Expiry Date
            datepart = columnValues[4].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[4]
            }

            const t1 = new Date(datepart);

            today1 = t1.toLocaleDateString("en-US");

            const date1 = ('0' + t1.getDate()).slice(-2);
            const month1 = ('0' + (t1.getMonth() + 1)).slice(-2);
            const year1 = t1.getFullYear();

            if (isNaN(year1) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                expiryDate = [yyyy, mm, dd].join('-');
            }
            else {
                expiryDate = [year1, month1, date1].join('-');
            }

            // Get Only Close Date
            datepart = columnValues[5].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[5]
            }

            const t2 = new Date(datepart);

            today2 = t2.toLocaleDateString("en-US");

            const date2 = ('0' + t2.getDate()).slice(-2);
            const month2 = ('0' + (t2.getMonth() + 1)).slice(-2);
            const year2 = t2.getFullYear();

            if (isNaN(year2) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                closeDate = [yyyy, mm, dd].join('-');
            }
            else {
                closeDate = [year2, month2, date2].join('-');
            }

            // Display Value In Modal Inputs
            $('#currency-id').removeAttr('style');
            $('#currency-id').removeAttr('multiple');
            $('.ms-options-wrap').css('display', 'none');
            $('#currency-id', myModal).val(columnValues[1]);
            $('#activation-date-currency', myModal).val(activationDate);
            $('#expiry-date-currency', myModal).val(expiryDate);
            $('#close-date-currency', myModal).val(closeDate);
            $('#note-currency', myModal).val(columnValues[6]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('.btn-edit-currency-edit-dt').addClass('read-only');
            $('#currency-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-currency-modal').click(function (event) {
        debugger;
        let currencyId = [];
        let currencyText = [];

        $('#currency-id option:selected').each(function () {
            currencyId.push($(this).val());
            currencyText.push($(this).text());
        });

        if (IsValidGenderDataTableModal()) {
            for (let i = 0, j = 0; i < currencyId.length, j < currencyText.length; i++, j++) {
                row = currencyDataTable.row.add([
                    tag,
                    currencyId[i],
                    currencyText[j],
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                ]).draw();

                rowNum++;
                row.nodes().to$().attr('id', 'tr' + rowNum);

                // Error Message In Span
                $('#currency-validation span').html('');

                HideGenderDataTableColumns();

                currencyDataTable.columns.adjust().draw();

                ClearModal('currency');

                $('#currency-modal').modal('hide');

                EnableNewOperation('currency');
            }
        }
    });

    // Modal update Button Event
    $('#btn-update-currency-modal').click(function (event) {

        $('#select-all-currency').prop('checked', false);
        if (IsValidGenderDataTableModal()) {
            currencyDataTable.row($(this).attr('rowindex')).data([
                tag,
                currencyId,
                currencyText,
                activationDate,
                expiryDate,
                closeDate,
                note
            ]).draw();
            // Error Message In Span
            $('#currency-validation span').html('');

            HideGenderDataTableColumns();

            currencyDataTable.columns.adjust().draw();

            $('#currency-modal').modal('hide');

            EnableNewOperation('currency');

            // Hide the element with id 'business-office-id'
            $('#currency-id').hide();
            // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
            $('#currency-id').attr('multiple', 'multiple');
            // Show elements with class 'ms-options-wrap'
            $('.ms-options-wrap').show();
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-currency-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-currency tbody input[type="checkbox"]:checked').each(function () {
                    currencyDataTable.row($('#tbl-currency tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-currency-dt').data('rowindex');
                    EnableNewOperation('currency');

                    $('#select-all-currency').prop('checked', false);

                    // Hide the element with id 'business-office-id'
                    $('#currency-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
                    $('#currency-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();

                    // Display Error, If Table Has Not Any Record
                    if (!currencyDataTable.data().any())
                        $('#currency-data-table-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-currency').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-currency tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = currencyDataTable.row(row).index();

                rowData = (currencyDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-currency-dt').data('rowindex', arr);
                EnableDeleteOperation('currency')
            });
        }
        else {
            EnableNewOperation('currency')

            $('#tbl-currency tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-currency tbody').click("input[type=checkbox]", function () {
        $('#tbl-currency input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = currencyDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (currencyDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('currency');

                    $('#btn-update-currency-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-currency-dt').data('rowindex', rowData);
                    $('#btn-delete-currency-dt').data('rowindex', arr);
                    $('#select-all-currency').data('rowindex', arr);
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
            EnableNewOperation('currency');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('currency');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('currency');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-currency').prop('checked', true);
        else
            $('#select-all-currency').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-currency> tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (currencyDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#currency-general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate Agent Incentive Module
    function IsValidGenderDataTableModal() {
        debugger;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        currencyId = $('#currency-id option:selected').val();
        currencyText = $('#currency-id option:selected').text();
        activationDate = $('#activation-date-currency').val();
        expiryDate = $('#expiry-date-currency').val();
        closeDate = $('#close-date-currency').val();
        note = $('#note-currency').val();
        result = true;

        isValidActivationDate = IsValidInputDate('#activation-date-currency');
        isValidExpiryDate = IsValidInputDate('#expiry-date-currency');



        // Validate Modal Inputs

        if (currencyId === "") {
            result = false;
            $('#currency-id-error').removeClass('d-none');
        }

        if (isValidActivationDate === false) {
            result = false;
            $('#activation-date-currency-error').removeClass('d-none');
        }

        if (isValidExpiryDate === false) {
            result = false;
            $('#expiry-date-currency-error').removeClass('d-none');
        }

        if (result) {
            return true;
        }
        else {
            return false;
        }

    }

    // Hide Unnecessary Columns
    function HideGenderDataTableColumns() {
        currencyDataTable.column(1).visible(false);
        currencyDataTable.column(5).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Scheme - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearGenderModalInputs();
    ClearModal('scheme');

    // DataTable Add Button 
    $('#btn-add-scheme-dt').click(function () {
        event.preventDefault();
        debugger;
        SetModalTitle('scheme', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-scheme-dt').click(function () {
        SetModalTitle('scheme', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-scheme-dt').data('rowindex');
            id = $('#scheme-modal').attr('id');
            myModal = $('#' + id).modal();
            //// Display Value In Modal Inputs
            // Get Only Activation Date
            datepart = columnValues[3].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[3]
            }

            const t = new Date(datepart);

            today = t.toLocaleDateString("en-US");

            const date = ('0' + t.getDate()).slice(-2);
            const month = ('0' + (t.getMonth() + 1)).slice(-2);
            const year = t.getFullYear();

            if (isNaN(year) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                activationDate = [yyyy, mm, dd].join('-');
            }
            else {
                activationDate = [year, month, date].join('-');
            }

            // Get Only Expiry Date
            datepart = columnValues[4].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[4]
            }

            const t1 = new Date(datepart);

            today1 = t1.toLocaleDateString("en-US");

            const date1 = ('0' + t1.getDate()).slice(-2);
            const month1 = ('0' + (t1.getMonth() + 1)).slice(-2);
            const year1 = t1.getFullYear();

            if (isNaN(year1) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                expiryDate = [yyyy, mm, dd].join('-');
            }
            else {
                expiryDate = [year1, month1, date1].join('-');
            }

            // Get Only Close Date
            datepart = columnValues[5].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[5]
            }

            const t2 = new Date(datepart);

            today2 = t2.toLocaleDateString("en-US");

            const date2 = ('0' + t2.getDate()).slice(-2);
            const month2 = ('0' + (t2.getMonth() + 1)).slice(-2);
            const year2 = t2.getFullYear();

            if (isNaN(year2) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                closeDate = [yyyy, mm, dd].join('-');
            }
            else {
                closeDate = [year2, month2, date2].join('-');
            }

            // Display Value In Modal Inputs
            $('#scheme-id', myModal).val(columnValues[1]);
            $('#activation-date-scheme', myModal).val(activationDate);
            $('#expiry-date-scheme', myModal).val(expiryDate);
            $('#close-date-scheme', myModal).val(closeDate);
            $('#note-scheme', myModal).val(columnValues[6]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('.btn-edit-scheme-edit-dt').addClass('read-only');
            $('#scheme-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-scheme-modal').click(function (event) {
        debugger;
        if (IsValidSchemeDataTableModal()) {
            row = schemeDataTable.row.add([
                tag,
                schemeId,
                schemeText,
                activationDate,
                expiryDate,
                closeDate,
                note,
            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            // Error Message In Span
            $('#scheme-validation span').html('');

            HideSchemeDataTableColumns();

            schemeDataTable.columns.adjust().draw();

            ClearModal('scheme');

            $('#scheme-modal').modal('hide');

            EnableNewOperation('scheme');
        }
    });

    // Modal update Button Event
    $('#btn-update-scheme-modal').click(function (event) {

        $('#select-all-scheme').prop('checked', false);
        if (IsValidSchemeDataTableModal()) {
            schemeDataTable.row($(this).attr('rowindex')).data([
                tag,
                schemeId,
                schemeText,
                activationDate,
                expiryDate,
                closeDate,
                note
            ]).draw();
            // Error Message In Span
            $('#scheme-validation span').html('');

            HideSchemeDataTableColumns();

            schemeDataTable.columns.adjust().draw();

            $('#scheme-modal').modal('hide');

            EnableNewOperation('scheme');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-scheme-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-scheme tbody input[type="checkbox"]:checked').each(function () {
                    schemeDataTable.row($('#tbl-scheme tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-scheme-dt').data('rowindex');
                    EnableNewOperation('scheme');

                    $('#select-all-scheme').prop('checked', false);
                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-scheme').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-scheme tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = schemeDataTable.row(row).index();

                rowData = (schemeDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-scheme-dt').data('rowindex', arr);
                EnableDeleteOperation('scheme')
            });
        }
        else {
            EnableNewOperation('scheme')

            $('#tbl-scheme tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-scheme tbody').click('input[type="checkbox"]', function () {
        $('#tbl-scheme input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = schemeDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (schemeDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('scheme');

                    $('#btn-update-scheme-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-scheme-dt').data('rowindex', rowData);
                    $('#btn-delete-scheme-dt').data('rowindex', arr);
                    $('#select-all-scheme').data('rowindex', arr);
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
            EnableNewOperation('scheme');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('scheme');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('scheme');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-scheme').prop('checked', true);
        else
            $('#select-all-scheme').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-scheme> tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (schemeDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#scheme-general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate Agent Incentive Module
    function IsValidSchemeDataTableModal() {
        debugger;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        schemeId = $('#scheme-id option:selected').val();
        schemeText = $('#scheme-id option:selected').text();
        activationDate = $('#activation-date-scheme').val();
        expiryDate = $('#expiry-date-scheme').val();
        closeDate = $('#close-date-scheme').val();
        note = $('#note-scheme').val();

        isValidActivationDate = IsValidInputDate('#activation-date-scheme');
        isValidExpiryDate = IsValidInputDate('#expiry-date-scheme');
        result = true;

        // Validate Modal Inputs

        if (schemeId === "") {
            result = false;
            $('#scheme-id-error').removeClass('d-none');
        }

        if (isValidActivationDate === false) {
            result = false;
            $('#activation-date-scheme-error').removeClass('d-none');
        }

        if (isValidActivationDate === false) {
            result = false;
            $('#expiry-date-scheme-error').removeClass('d-none');
        }

        if (result) {
            return true;
        }
        else {
            return false;
        }
    }

    // Hide Unnecessary Columns
    function HideSchemeDataTableColumns() {
        schemeDataTable.column(1).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@ Transaction Type - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearGenderModalInputs();
    ClearModal('transaction-type');

    // DataTable Add Button 
    $('#btn-add-transaction-type-dt').click(function () {
        event.preventDefault();
        debugger;
        SetModalTitle('transaction-type', 'Add');

        $('#transaction-type-id').hide();
        $('#transaction-type-id').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();
    });

    // DataTable Edit Button 
    $('#btn-edit-transaction-type-dt').click(function () {
        debugger;
        SetModalTitle('transaction-type', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-transaction-type-dt').data('rowindex');
            id = $('#transaction-type-modal').attr('id');
            myModal = $('#' + id).modal();
            //// Display Value In Modal Inputs
            // Get Only Activation Date
            datepart = columnValues[6].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[6]
            }

            const t = new Date(datepart);

            today = t.toLocaleDateString("en-US");

            const date = ('0' + t.getDate()).slice(-2);
            const month = ('0' + (t.getMonth() + 1)).slice(-2);
            const year = t.getFullYear();

            if (isNaN(year) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                activationDate = [yyyy, mm, dd].join('-');
            }
            else {
                activationDate = [year, month, date].join('-');
            }

            // Get Only Expiry Date
            datepart = columnValues[7].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[7]
            }

            const t1 = new Date(datepart);

            today1 = t1.toLocaleDateString("en-US");

            const date1 = ('0' + t1.getDate()).slice(-2);
            const month1 = ('0' + (t1.getMonth() + 1)).slice(-2);
            const year1 = t1.getFullYear();

            if (isNaN(year1) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                expiryDate = [yyyy, mm, dd].join('-');
            }
            else {
                expiryDate = [year1, month1, date1].join('-');
            }

            // Get Only Close Date
            datepart = columnValues[8].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[8]
            }

            const t2 = new Date(datepart);

            today2 = t2.toLocaleDateString("en-US");

            const date2 = ('0' + t2.getDate()).slice(-2);
            const month2 = ('0' + (t2.getMonth() + 1)).slice(-2);
            const year2 = t2.getFullYear();

            if (isNaN(year2) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                closeDate = [yyyy, mm, dd].join('-');
            }
            else {
                closeDate = [year2, month2, date2].join('-');
            }

            // Display Value In Modal Inputs
            $('#transaction-type-id').removeAttr('style');
            $('#transaction-type-id').removeAttr('multiple');
            $('.ms-options-wrap').css('display', 'none');
            $('#transaction-type-id', myModal).val(columnValues[1]);
            $('#minimum-amount-limit', myModal).val(columnValues[3]);
            $('#maximum-amount-limit', myModal).val(columnValues[4]);
            $('#allowed-max-no-of-back-days-entry', myModal).val(columnValues[5]);
            $('#activation-date-transaction-type', myModal).val(activationDate);
            $('#expiry-date-transaction-type', myModal).val(expiryDate);
            $('#close-date-transaction-type', myModal).val(closeDate);
            $('#note-transaction-type', myModal).val(columnValues[9]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('.btn-edit-transaction-type-dt').addClass('read-only');
            $('#transaction-type-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-transaction-type-modal').click(function (event) {
        debugger;
        let transactionTypeId = [];
        let transactionTypeText = [];

        $('#transaction-type-id option:selected').each(function () {
            transactionTypeId.push($(this).val());
            transactionTypeText.push($(this).text());
        });
        if (IsValidTransactionTypeDataTableModal()) {
            for (let i = 0, j = 0; i < transactionTypeId.length, j < transactionTypeText.length; i++, j++) {
                row = transactionTypeDataTable.row.add([
                    tag,
                    transactionTypeId[i],
                    transactionTypeText[j],
                    minimumAmountLimit,
                    maximumAmountLimit,
                    allowedMaximumNumberOfBackDaysEntry,
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                ]).draw();

                rowNum++;
                row.nodes().to$().attr('id', 'tr' + rowNum);

                // Error Message In Span
                $('#transaction-type-validation span').html('');

                HideTransactionTypeDataTableColumns();

                transactionTypeDataTable.columns.adjust().draw();

                ClearModal('transaction-type');

                $('#transaction-type-modal').modal('hide');

                EnableNewOperation('transaction-type');
            }
        }
    });

    // Modal update Button Event
    $('#btn-update-transaction-type-modal').click(function (event) {

        $('#select-all-transaction-type').prop('checked', false);
        if (IsValidTransactionTypeDataTableModal()) {
            transactionTypeDataTable.row($(this).attr('rowindex')).data([
                tag,
                transactionTypeId,
                transactionTypeText,
                minimumAmountLimit,
                maximumAmountLimit,
                allowedMaximumNumberOfBackDaysEntry,
                activationDate,
                expiryDate,
                closeDate,
                note
            ]).draw();

            // Hide the element with id 'business-office-id'
            $('#transaction-type-id').hide();
            // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
            $('#transaction-type-id').attr('multiple', 'multiple');
            // Show elements with class 'ms-options-wrap'
            $('.ms-options-wrap').show();

            // Error Message In Span
            $('#transaction-type-validation span').html('');

            HideTransactionTypeDataTableColumns();

            transactionTypeDataTable.columns.adjust().draw();

            $('#transaction-type-modal').modal('hide');

            EnableNewOperation('transaction-type');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-transaction-type-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-transaction-type tbody input[type="checkbox"]:checked').each(function () {
                    transactionTypeDataTable.row($('#tbl-transaction-type tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-transaction-type-dt').data('rowindex');
                    EnableNewOperation('transaction-type');

                    $('#select-all-transaction-type').prop('checked', false);

                    // Hide the element with id 'business-office-id'
                    $('#transaction-type-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
                    $('#transaction-type-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();

                    // Display Error, If Table Has Not Any Record
                    if (!transactionTypeDataTable.data().any())
                        $('#transaction-type-data-table-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-transaction-type').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-transaction-type tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = transactionTypeDataTable.row(row).index();

                rowData = (transactionTypeDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-transaction-type-dt').data('rowindex', arr);
                EnableDeleteOperation('transaction-type')
            });
        }
        else {
            EnableNewOperation('transaction-type')

            $('#tbl-transaction-type tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-transaction-type tbody').click('input[type="checkbox"]', function () {
        $('#tbl-transaction-type input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = transactionTypeDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (transactionTypeDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('transaction-type');

                    $('#btn-update-transaction-type-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-transaction-type-dt').data('rowindex', rowData);
                    $('#btn-delete-transaction-type-dt').data('rowindex', arr);
                    $('#select-all-transaction-type').data('rowindex', arr);
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
            EnableNewOperation('transaction-type');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('transaction-type');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('transaction-type');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-transaction-type').prop('checked', true);
        else
            $('#select-all-transaction-type').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-transaction-type> tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (transactionTypeDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#transaction-type-general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate Agent Incentive Module
    function IsValidTransactionTypeDataTableModal() {
        debugger;

        isValidAmountLimit = true;
        isValidBackDaysEntry = true;


        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        transactionTypeId = $('#transaction-type-id option:selected').val();
        transactionTypeText = $('#transaction-type-id option:selected').text();
        minimumAmountLimit = $('#minimum-amount-limit').val();
        maximumAmountLimit = $('#maximum-amount-limit').val();
        allowedMaximumNumberOfBackDaysEntry = $('#allowed-max-no-of-back-days-entry').val();
        activationDate = $('#activation-date-transaction-type').val();
        expiryDate = $('#expiry-date-transaction-type').val();
        closeDate = $('#close-date-transaction-type').val();
        note = $('#note-transaction-type').val();

        // Set Default Value, If Empty
        if (note == '')
            note = 'None';

        // Validate Amount Limit
        if (isNaN(minimumAmountLimit) === false) {
            minimum = parseFloat($('#minimum-amount-limit').attr('min'));
            maximum = parseFloat($('#minimum-amount-limit').attr('max'));

            if (parseFloat(minimumAmountLimit) < parseFloat(minimum) || parseFloat(minimumAmountLimit) > parseFloat(maximum)) {
                isValidAmountLimit = false;
                $('#minimum-amount-limit-error').removeClass('d-none');
            }
        }
        else {
            isValidAmountLimit = false;
            $('#minimum-amount-limit-error').removeClass('d-none');
        }

        if (isNaN(maximumAmountLimit) === false) {
            minimum = parseFloat($('#maximum-amount-limit').attr('min'));
            maximum = parseFloat($('#maximum-amount-limit').attr('max'));

            if (parseFloat(maximumAmountLimit) < parseFloat(minimum) || parseFloat(maximumAmountLimit) > parseFloat(maximum)) {
                isValidAmountLimit = false;
                $('#maximum-amount-limit-error').removeClass('d-none');
            }
        }
        else {
            isValidAmountLimit = false;
            $('#maximum-amount-limit-error').removeClass('d-none');
        }

        // Validate BackDaysEntry
        if (allowedMaximumNumberOfBackDaysEntry == '' || parseInt(allowedMaximumNumberOfBackDaysEntry) < 1 || parseInt(allowedMaximumNumberOfBackDaysEntry) > 100) {
            $('#allowed-max-no-of-back-days-entry-error').removeClass('d-none');
            isValidBackDaysEntry = false;

        } else
            $('#allowed-max-no-of-back-days-entry-error').addClass('d-none');

        isValidActivationDate = IsValidInputDate('#activation-date-transaction-type');
        isValidExpiryDate = IsValidInputDate('#expiry-date-transaction-type');

        // Validate Modal Inputs
        if ((transactionTypeId == "") || isValidAmountLimit == false || isValidBackDaysEntry == false || !isValidActivationDate || !isValidExpiryDate) {

            if (transactionTypeId === "")
                $('#transaction-type-id-error').removeClass('d-none');

            if (isValidActivationDate === false)
                $('#activation-date-transaction-type-error').removeClass('d-none');

            return false;
        }
        else
            return true;
    }

    // Hide Unnecessary Columns
    function HideTransactionTypeDataTableColumns() {
        transactionTypeDataTable.column(1).visible(false);
        transactionTypeDataTable.column(7).visible(false);
        transactionTypeDataTable.column(8).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  CustomerType - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearGenderModalInputs();
    ClearModal('customer-type');

    // DataTable Add Button 
    $('#btn-add-customer-type-dt').click(function () {
        event.preventDefault();
        debugger;
        SetModalTitle('customer-type', 'Add');

        $('#customer-type').hide();
        $('#customer-type').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();
    });

    // DataTable Edit Button 
    $('#btn-edit-customer-type-dt').click(function () {
        SetModalTitle('customer-type', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-customer-type-dt').data('rowindex');
            id = $('#customer-type-modal').attr('id');
            myModal = $('#' + id).modal();
            //// Display Value In Modal Inputs
            // Get Only Activation Date
            datepart = columnValues[3].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[3]
            }

            const t = new Date(datepart);

            today = t.toLocaleDateString("en-US");

            const date = ('0' + t.getDate()).slice(-2);
            const month = ('0' + (t.getMonth() + 1)).slice(-2);
            const year = t.getFullYear();

            if (isNaN(year) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                activationDate = [yyyy, mm, dd].join('-');
            }
            else {
                activationDate = [year, month, date].join('-');
            }

            // Get Only Expiry Date
            datepart = columnValues[4].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[4]
            }

            const t1 = new Date(datepart);

            today1 = t1.toLocaleDateString("en-US");

            const date1 = ('0' + t1.getDate()).slice(-2);
            const month1 = ('0' + (t1.getMonth() + 1)).slice(-2);
            const year1 = t1.getFullYear();

            if (isNaN(year1) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                expiryDate = [yyyy, mm, dd].join('-');
            }
            else {
                expiryDate = [year1, month1, date1].join('-');
            }

            // Get Only Close Date
            datepart = columnValues[5].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[5]
            }

            const t2 = new Date(datepart);

            today2 = t2.toLocaleDateString("en-US");

            const date2 = ('0' + t2.getDate()).slice(-2);
            const month2 = ('0' + (t2.getMonth() + 1)).slice(-2);
            const year2 = t2.getFullYear();

            if (isNaN(year2) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                closeDate = [yyyy, mm, dd].join('-');
            }
            else {
                closeDate = [year2, month2, date2].join('-');
            }

            // Display Value In Modal Inputs
            $('#customer-type-id').removeAttr('style');
            $('#customer-type-id').removeAttr('multiple');
            $('.ms-options-wrap').css('display', 'none');
            $('#customer-type-id', myModal).val(columnValues[1]);
            $('#activation-date-customer-type', myModal).val(activationDate);
            $('#expiry-date-customer-type', myModal).val(expiryDate);
            $('#close-date-customer-type', myModal).val(closeDate);
            $('#note-customer-type', myModal).val(columnValues[6]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('.btn-edit-customer-type-dt').addClass('read-only');
            $('#customer-type-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-customer-type-modal').click(function (event) {
        debugger;
        let customerTypeId = [];
        let customerTypeText = [];

        $('#customer-type-id option:selected').each(function () {
            customerTypeId.push($(this).val());
            customerTypeText.push($(this).text());
        });

        if (IsValidCustomerTypeDataTableModal()) {
            for (let i = 0, j = 0; i < customerTypeId.length, j < customerTypeText.length; i++, j++) {
                row = customerTypeDataTable.row.add([
                    tag,
                    customerTypeId[i],
                    customerTypeText[j],
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                ]).draw();

                rowNum++;
                row.nodes().to$().attr('id', 'tr' + rowNum);

                // Error Message In Span
                $('#customer-type-validation span').html('');

                HideCustomerTypeDataTableColumns();

                customerTypeDataTable.columns.adjust().draw();

                ClearModal('customer-type');

                $('#customer-type-modal').modal('hide');

                EnableNewOperation('customer-type');
            }
        }
    });

    // Modal update Button Event
    $('#btn-update-customer-type-modal').click(function (event) {

        $('#select-all-customer-type').prop('checked', false);
        if (IsValidCustomerTypeDataTableModal()) {
            customerTypeDataTable.row($(this).attr('rowindex')).data([
                tag,
                customerTypeId,
                customerTypeText,
                activationDate,
                expiryDate,
                closeDate,
                note
            ]).draw();

            // Hide the element with id 'customer-type-id'
            $('#customer-type-id').hide();
            // Set the 'multiple' attribute to 'multiple' for the element with id 'customer-type-id'
            $('#customer-type-id').attr('multiple', 'multiple');
            // Show elements with class 'ms-options-wrap'
            $('.ms-options-wrap').show();

            // Error Message In Span
            $('#customer-type-validation span').html('');

            HideCustomerTypeDataTableColumns();

            customerTypeDataTable.columns.adjust().draw();

            $('#customer-type-modal').modal('hide');

            EnableNewOperation('customer-type');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-customer-type-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-customer-type tbody input[type="checkbox"]:checked').each(function () {
                    customerTypeDataTable.row($('#tbl-customer-type tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-customer-type-dt').data('rowindex');
                    EnableNewOperation('customer-type');

                    $('#select-all-customer-type').prop('checked', false);

                    // Hide the element with id 'customer-type-id'
                    $('#customer-type-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'customer-type-id'
                    $('#customer-type-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();

                    // Display Error, If Table Has Not Any Record
                    if (!customerTypeDataTable.data().any())
                        $('#customer-type-data-table-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Customer  Type Datatable
    $('#select-all-customer-type').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-customer-type tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = customerTypeDataTable.row(row).index();

                rowData = (customerTypeDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-customer-type-dt').data('rowindex', arr);
                EnableDeleteOperation('customer-type')
            });
        }
        else {
            EnableNewOperation('customer-type')

            $('#tbl-customer-type tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-customer-type tbody').click('input[type="checkbox"]', function () {
        $('#tbl-customer-type input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = customerTypeDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (customerTypeDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('customer-type');

                    $('#btn-update-customer-type-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-customer-type-dt').data('rowindex', rowData);
                    $('#btn-delete-customer-type-dt').data('rowindex', arr);
                    $('#select-all-customer-type').data('rowindex', arr);
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
            EnableNewOperation('customer-type');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('customer-type');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('customer-type');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-customer-type').prop('checked', true);
        else
            $('#select-all-customer-type').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-customer-type> tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (customerTypeDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#customer-type-general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate Agent Incentive Module
    function IsValidCustomerTypeDataTableModal() {
        debugger;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        customerTypeId = $('#customer-type-id option:selected').val();
        customerTypeText = $('#customer-type-id option:selected').text();
        activationDate = $('#activation-date-customer-type').val();
        expiryDate = $('#expiry-date-customer-type').val();
        closeDate = $('#close-date-customer-type').val();
        note = $('#note-customer-type').val();


        isValidActivationDate = IsValidInputDate('#activation-date-customer-type');
        isValidExpiryDate = IsValidInputDate('#expiry-date-customer-type');


        // Validate Modal Inputs
        if ((customerTypeId == "") || !isValidActivationDate || !isValidExpiryDate) {

            if (customerTypeId == "")
                $('#customer-type-id-error').removeClass('d-none');

            if (!isValidActivationDate)
                $('#activation-date-customer-type-error').removeClass('d-none');

            return false;
        }
        else
            return true;
    }

    // Hide Unnecessary Columns
    function HideCustomerTypeDataTableColumns() {
        customerTypeDataTable.column(1).visible(false);
        customerTypeDataTable.column(5).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;

        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();


            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let businessOffice = new Array();
            let currency = new Array();
            let scheme = new Array();
            let transactionType = new Array();
            let customerType = new Array();

            //CreateDataTable
            businessOfficeDataTable.page.len(-1).draw();
            currencyDataTable.page.len(-1).draw();
            schemeDataTable.page.len(-1).draw();
            customerTypeDataTable.page.len(-1).draw();
            transactionTypeDataTable.page.len(-1).draw();

            // Create Array For Business Office Array Data Table To Pass Data
            if (!$('#heading-business-office').hasClass('d-none')) {
                if (businessOfficeDataTable.data().any()) {
                    $('#business-office-error').addClass('d-none');
                    if (isValidAllInputs) {
                        // Get Data Table Values In Interest PayOut Frequency Array
                        $('#tbl-business-office > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');
                            columnValues = (businessOfficeDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                debugger;
                                businessOffice.push(
                                    {
                                        'BusinessOfficeId': columnValues[1],
                                        'ActivationDate': columnValues[3],
                                        'ExpiryDate': columnValues[4],
                                        'CloseDate': columnValues[5],
                                        'Note': columnValues[6],
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#business-office-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For currency  Array Data Table To Pass Data
            if (!$('#heading-currency').hasClass('d-none')) {
                if (currencyDataTable.data().any()) {
                    $('#currency-error').addClass('d-none');
                    if (isValidAllInputs) {
                        // Get Data Table Values In Interest PayOut Frequency Array
                        $('#tbl-currency > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');
                            columnValues = (currencyDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                currency.push(
                                    {
                                        'CurrencyId': columnValues[1],
                                        'ActivationDate': columnValues[3],
                                        'ExpiryDate': columnValues[4],
                                        'CloseDate': columnValues[5],
                                        'Note': columnValues[6],
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#currency-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }



            // Create Array For scheme Data Table To Pass Data
            //if (isSchemeVisible) {
            //    if (isschemeDataTableValid) {
            //        $('#scheme-data-table-error').addClass('d-none');

            //        // Get Data Table Values In Notice Schedule Array
            //        $('#tbl-scheme tbody tr').each(function () {
            //            currentRow = $(this).closest('tr');
            //            columnValues = (schemeDataTable.row(currentRow).data());

            //            // Handling Code If Row Is Undefined Or Null
            //            if (typeof columnValues != 'undefined' && columnValues != null) {
            //                scheme.push(
            //                    {
            //                        'SchemeId': columnValues[1],
            //                        'ActivationDate': columnValues[3],
            //                        'ExpiryDate': columnValues[4],
            //                        'CloseDate': columnValues[5],
            //                        'Note': columnValues[6],
            //                    });
            //            }
            //            else
            //                return false;
            //        });
            //    }
            //    else
            //        $('#scheme-data-table-error').removeClass('d-none');
            //}


            // Create Array For Transaction Type  Array Data Table To Pass Data
            if (!$('#heading-transaction-type').hasClass('d-none')) {
                if (transactionTypeDataTable.data().any()) {
                    $('#transaction-type-data-table-error').addClass('d-none');
                    if (isValidAllInputs) {
                        // Get Data Table Values In Interest PayOut Frequency Array
                        $('#tbl-transaction-type > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');
                            columnValues = (transactionTypeDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                transactionType.push(
                                    {
                                        'TransactionTypeId': columnValues[1],
                                        'MinimumAmountLimit': columnValues[3],
                                        'MaximumAmountLimit': columnValues[4],
                                        'AllowedMaximumNumberOfBackDaysEntry': columnValues[5],
                                        'ActivationDate': columnValues[6],
                                        'ExpiryDate': columnValues[7],
                                        'CloseDate': columnValues[8],
                                        'Note': columnValues[9],
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#transaction-type-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For Transaction Type  Array Data Table To Pass Data
            if (!$('#heading-customer-type').hasClass('d-none')) {
                if (customerTypeDataTable.data().any()) {
                    $('#customer-type-data-table-error').addClass('d-none');
                    if (isValidAllInputs) {
                        // Get Data Table Values In Interest PayOut Frequency Array
                        $('#tbl-customer-type > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');
                            columnValues = (customerTypeDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                customerType.push(
                                    {
                                        'CustomerTypeId': columnValues[1],
                                        'ActivationDate': columnValues[3],
                                        'ExpiryDate': columnValues[4],
                                        'CloseDate': columnValues[5],
                                        'Note': columnValues[6],
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#customer-type-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Call Cantroller Save Data Table Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: urlForDataTable,
                    type: 'POST',
                    data: { '_businessOffice': businessOffice, '_currency': currency, '_scheme': scheme, '_transactionType': transactionType, '_customerType': customerType },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'json',

                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured While Save Data In SaveDataTable Method!!! Error Message - ' + error.toString());
                    }

                });

            }
            else {
                alert('Oops, Something Went Wrong!, Please Provide Valid Data');
                event.preventDefault();
            }
        }
        else {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }
    });

});