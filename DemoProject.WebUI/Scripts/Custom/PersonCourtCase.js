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
    // @@@@@@@@@@ Data Table Related letible Declaration

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
    let minimum;
    let maximum;
    let minimumLength = 0;
    let maximumLength = 0;
    let arr = new Array();

    // Court Case
    let courtCaseTypeId = '';
    let courtCaseTypeIdText = '';
    let cnrNumber = 0;
    let amountOfDecree = 0;
    let collateralAmount = 0;
    let courtCaseStageId = '';
    let courtCaseStageIdText = '';
    let result = true;
    let note = '';
    let filingDate = '';
    let filingNumber = 0;
    let registrationDate = '';
    let registrationNumber = '';
    let reasonForModification = '';
    let lastSelectedValue = '';

    // Create DataTables

    let courtCaseDataTable = CreateDataTable('court-case');

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    //Court Case Dropdown focusout value get clear
    $('#court-case-types-id').focusout(function () {
        const currentValue = $(this).val();

        if (currentValue !== lastSelectedValue) {
            $('select').not('#court-case-types-id').prop('selectedIndex', 0);
            $('input[type="date"],input[type="number"], textarea').val('');
            $('input[type="text"]').not('#checker-remark, #maker-remark, #o-remark').val('');
            $('.modal-input-error').addClass('d-none');
        }

        lastSelectedValue = currentValue;
    });

    //Filing Date
    $('#activation-filing-dates').click(function ()
    {
        $('#expiry-filing-dates').val('');
    });

    // Registration Date
    $('#expiry-filing-dates').click(function ()
    {
        let today = new Date();

        let filingDate = new Date($('#activation-filing-dates').val());

        $('#expiry-filing-dates').attr('min', GetInputDateFormat(filingDate));

        let maxRegistrationDate = new Date(filingDate);
        maxRegistrationDate.setMonth(maxRegistrationDate.getMonth() + 1);

        if (maxRegistrationDate > today)
        {
            maxRegistrationDate = today;
        }

        $('#expiry-filing-dates').attr('max', GetInputDateFormat(maxRegistrationDate));
    });

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Court Case - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-court-case-dt').click(function () {
        event.preventDefault();

        lastSelectedValue = '';

        SetModalTitle('court-case', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-court-case-dt').click(function ()
    {
        debugger;

        SetModalTitle('court-case', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#court-case-modal').modal();

            let tmpDate = new Date();

            columnValues = $('#btn-edit-court-case-dt').data('rowindex');

            let courtcasefillingDate = new Date(columnValues[3]);
            let courtcaseregistrationDate = new Date(columnValues[5]);

            tmpDate = new Date(columnValues[3]);
            tmpDate.setDate(tmpDate.getDate());

            $('#expiry-filing-dates').attr('min', GetInputDateFormat(tmpDate));

            // Set Max Of Registration Date
            tmpDate = new Date(columnValues[5]);
            tmpDate.setDate(tmpDate.getDate() + 30);

            if (tmpDate > new Date()) {
                $('#expiry-filing-dates').attr('max', GetInputDateFormat(new Date()));
            }
            else {
                $('#expiry-filing-dates').attr('max', GetInputDateFormat(tmpDate));
            }

            lastSelectedValue = columnValues[1];

            $('#court-case-types-id', myModal).val(columnValues[1]);
            $('#activation-filing-dates', myModal).val(GetInputDateFormat(courtcasefillingDate));
            $('#filing-numbers', myModal).val(columnValues[4]);
            $('#expiry-filing-dates', myModal).val(GetInputDateFormat(courtcaseregistrationDate));
            $('#registration-numbers', myModal).val(columnValues[6]);
            $('#cnr-number-case', myModal).val(columnValues[7]);
            $('#amount-of-decree', myModal).val(columnValues[8]);
            $('#collateral-amount', myModal).val(columnValues[9]);
            $('#court-cases-stage-id', myModal).val(columnValues[10]);
            $('#note-court-case', myModal).val(columnValues[12]);
            $('#reason-for-modification-court-case', myModal).val(columnValues[13]);

            // Show Modals
            $('#court-case-modal').modal('show');
        }
        else {
            $('#btn-edit-court-case-edit-dt').addClass('read-only');
            $('#court-case-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-court-case-modal').click(function (event) {
        debugger;
        if (IsValidCourtCaseModal()) {
            row = courtCaseDataTable.row.add([
                tag,
                courtCaseTypeId,
                courtCaseTypeIdText,
                filingDate,
                filingNumber,
                registrationDate,
                registrationNumber,
                cnrNumber,
                amountOfDecree,
                collateralAmount,
                courtCaseStageId,
                courtCaseStageIdText,
                note,
                reasonForModification,
            ]).draw();

            // Error Message In Span
            $('#court-case-data-table-error').addClass('d-none');

            HideColumnsCourtCaseDataTable();

            courtCaseDataTable.columns.adjust().draw();

            $('#court-case-modal').modal('hide');

            EnableNewOperation('court-case');
        }
    });

    // Modal update Button Event
    $('#btn-update-court-case-modal').click(function (event) {
        let b = $('#btn-edit-court-case-dt').attr('rowindex');
        $('#select-all-court-case').prop('checked', false);
        if (IsValidCourtCaseModal()) {
            courtCaseDataTable.row(selectedRowIndex).data([
                tag,
                courtCaseTypeId,
                courtCaseTypeIdText,
                filingDate,
                filingNumber,
                registrationDate,
                registrationNumber,
                cnrNumber,
                amountOfDecree,
                collateralAmount,
                courtCaseStageId,
                courtCaseStageIdText,
                note,
                reasonForModification
            ]).draw();
            // Error Message In Span
            $('#court-case-validation span').html('');

            HideColumnsCourtCaseDataTable();

            courtCaseDataTable.columns.adjust().draw();

            $('#court-case-modal').modal('hide');

            EnableNewOperation('court-case');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-court-case-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-court-case tbody input[type="checkbox"]:checked').each(function () {
                    courtCaseDataTable.row($('#tbl-court-case tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-court-case-dt').data('rowindex');
                    EnableNewOperation('court-case');

                    $('#select-all-court-case').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-court-case').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-court-case tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = courtCaseDataTable.row(row).index();

                rowData = (courtCaseDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-court-case-dt').data('rowindex', arr);
                EnableDeleteOperation('court-case');
            });
        }
        else {
            EnableNewOperation('court-case')

            $('#tbl-court-case tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-court-case tbody').click('input[type=checkbox]', function () {
        $('#tbl-court-case input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = courtCaseDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (courtCaseDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('court-case');

                    $('#btn-update-court-case-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-court-case-dt').data('rowindex', rowData);
                    $('#btn-delete-court-case-dt').data('rowindex', arr);
                    $('#select-all-court-case').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-court-case tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('court-case');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('court-case');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('court-case');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-court-case').prop('checked', true);
        else
            $('#select-all-court-case').prop('checked', false);
    });

    // Validate Court Case Module
    function IsValidCourtCaseModal() {
        result = true;
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        courtCaseTypeId = $('#court-case-types-id option:selected').val();
        courtCaseTypeIdText = $('#court-case-types-id option:selected').text();
        filingDate = $('#activation-filing-dates').val();
        filingNumber = $('#filing-numbers').val();
        registrationDate = $('#expiry-filing-dates').val();
        registrationNumber = $('#registration-numbers').val();
        cnrNumber = $('#cnr-number-case').val();
        amountOfDecree = parseFloat($('#amount-of-decree').val());
        collateralAmount = parseFloat($('#collateral-amount').val());
        courtCaseStageId = $('#court-cases-stage-id option:selected').val();
        courtCaseStageIdText = $('#court-cases-stage-id option:selected').text();
        note = $('#note-court-case').val().trim();
        reasonForModification = $('#reason-for-modification-court-case').val();

        //Set Default Value if Empty
        if (note === '') {
            $('#note-court-case').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-court-case').val('None');
            reasonForModification = 'None';
        }

        //court case types
        if ($('#court-case-types-id').prop('selectedIndex') < 1) {
            result = false;
            $('#court-case-types-id-error').removeClass('d-none');
        }
        
        //Filing Date
        let isValidFilingDate = IsValidInputDate('#activation-filing-dates');

        if (isValidFilingDate === false) {
            result = false;
            $('#activation-filing-dates-error').removeClass('d-none');
        }
       
        //filing Number
        if (isNaN(filingNumber.length) === false)
        {
            minimumLength = parseInt($('#filing-numbers').attr('minlength'));
            maximumLength = parseInt($('#filing-numbers').attr('maxlength'));

            if (parseInt(filingNumber.length) < parseInt(minimumLength) || parseInt(filingNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#filing-numbers-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#filing-numbers-error').removeClass('d-none');
        }

        let isValidRegistrationDate = IsValidInputDate('#expiry-filing-dates');

        //Registration Date
        if (isValidRegistrationDate === false) {
            result = false;
            $('#expiry-filing-dates-error').removeClass('d-none');
        }
        
       //registration Number
       if (isNaN(registrationNumber.length) === false)
        {
            minimumLength = parseInt($('#registration-numbers').attr('minlength'));
            maximumLength = parseInt($('#registration-numbers').attr('maxlength'));

            if (parseInt(registrationNumber.length) < parseInt(minimumLength) || parseInt(registrationNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#registration-numbers-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#registration-numbers-error').removeClass('d-none');
        }
      
        //cnr Number
        if (isNaN(cnrNumber.length) === false)
        {
            minimumLength = parseInt($('#cnr-number-case').attr('minlength'));
            maximumLength = parseInt($('#cnr-number-case').attr('maxlength'));

            if (parseInt(cnrNumber.length) < parseInt(minimumLength) || parseInt(cnrNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#cnr-number-case-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#cnr-number-case-error').removeClass('d-none');
        }
        
        //amount Of Decree
        if (isNaN(amountOfDecree) === false)
        {
             minimum = parseFloat($('#amount-of-decree').attr('min'));
             maximum = parseFloat($('#amount-of-decree').attr('max'));

            if (parseFloat(amountOfDecree) < parseFloat(minimum) || parseFloat(amountOfDecree) > parseFloat(maximum)) {
                result = false;
                $('#amount-of-decree-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#amount-of-decree-error').removeClass('d-none');
        }

        //collateral Amount
        if (isNaN(collateralAmount) === false)
        {
             minimum = parseFloat($('#collateral-amount').attr('min'));
             maximum = parseFloat($('#collateral-amount').attr('max'));

            if (parseFloat(collateralAmount) < parseFloat(minimum) || parseFloat(collateralAmount) > parseFloat(maximum)) {
                result = false;
                $('#collateral-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#collateral-amount-error').removeClass('d-none');
        }

        //court case stage
        if ($('#court-cases-stage-id').prop('selectedIndex') < 1) {
            result = false;
            $('#court-cases-stage-id-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsCourtCaseDataTable() {
        courtCaseDataTable.column(1).visible(false);
        courtCaseDataTable.column(10).visible(false);
    }

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        let isValidAllInputs = true;

        //if ($('form').valid() 
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let personCourtCaseArray = new Array();

            // Create Array For person court case Table To Pass Data
            if (courtCaseDataTable.data().any()) {

                if (isValidAllInputs) {

                    $('#tbl-court-case > tbody > tr').each(function () {
                        currentRow = $(this).closest('tr');

                        columnValues = (courtCaseDataTable.row(currentRow).data());

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues !== 'undefined' && columnValues !== null) {
                            personCourtCaseArray.push(
                            {
                                'CourtCaseTypeId': columnValues[1],
                                'FilingDate': columnValues[3],
                                'FilingNumber': columnValues[4],
                                'RegistrationDate': columnValues[5],
                                'RegistrationNumber': columnValues[6],
                                'CNRNumber': columnValues[7],
                                'AmountOfDecree': columnValues[8],
                                'CollateralAmount': columnValues[9],
                                'CourtCaseStageId': columnValues[10],
                                'Note': columnValues[12],
                                'ReasonForModification': columnValues[13]
                            });
                        }
                        else
                            return false;
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
                    data: {
                        '_courtCase': personCourtCaseArray,
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Person Court Case DataTable!!! Error Message - ' + error.toString());
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