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

    // @@@@@@@@@@ Data Table Related letible Declaration

    const OTHER_INCOME = 'Other Income';
    const OTHER_INCOME_TEXT = 'Other Income ---> इतर उत्पन्न';

    let result = true;

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
    let note;
    let minimum;
    let maximum;
    let maximumLength = 0;
    let arr = new Array();

    // income Detail
    let incomeSource = '';
    let incomeSourceText = '';
    let reasonForModification;
    let otherDetails = '';
    let annualIncome = 0;

    // Create DataTables
    let incomeDatatable = CreateDataTable('income-detail');

    // Page Loading Default Values (Usually For Amend)
    SetPageLoadingDefaultValues();

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Validation Income Soure Id
    $('#income-source-id').change(function () {
        debugger
        IncomeSourceChange();
    });

    // Function to handle the change event for the income source dropdown
    function IncomeSourceChange() {
        debugger;
        // Get the selected text from the dropdown
         incomeSourceText = $('#income-source-id option:selected').text();

        // Check if the selected text is 'Other Income'
        if (incomeSourceText === OTHER_INCOME || incomeSourceText === OTHER_INCOME_TEXT) {
            $('#other-details-input').removeClass('d-none');
        } else {
            $('#other-details').val('None');
            $('#other-details-input').addClass('d-none');
            $('#other-details-error').addClass('d-none');
        }
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Additional Income Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-income-detail-dt').click(function () {
        debugger
        event.preventDefault();
        $('#other-details-input').addClass('d-none');

        SetModalTitle('income-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-income-detail-dt').click(function () {
        debugger
        SetModalTitle('income-detail', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#income-detail-modal').modal();

            columnValues = $('#btn-edit-income-detail-dt').data('rowindex');
            $('#income-source-id', myModal).val(columnValues[1]);
            $('#annual-incomes', myModal).val(columnValues[3]);
            $('#other-details', myModal).val(columnValues[4]);
            $('#note-income-detail', myModal).val(columnValues[5]);
            $('#reason-for-modification-income-detail', myModal).val(columnValues[6]);

            debugger
            // Check if the selected text is 'Other Income'
            if (columnValues[2] === OTHER_INCOME || columnValues[2] === OTHER_INCOME_TEXT) {
                $('#other-details-input').removeClass('d-none');
            }
            else {
                $('#other-details-input').addClass('d-none');
                $('#other-details').val('None');
                $('#other-details-error').addClass('d-none');
            }

            // Show Modals
            $('#income-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-income-detail-edit-dt').addClass('read-only');
            $('#income-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-income-detail-modal').click(function (event) {

        if (IsValidIncomeDetailModal()) {
            row = incomeDatatable.row.add([
                tag,
                incomeSource,
                incomeSourceText,
                annualIncome,
                otherDetails,
                note,
                reasonForModification
            ]).draw();

            // Error Message In Span
            $('#income-details-data-table-error').addClass('d-none');

            HideColumnsIncomeDetailDatatable();

            incomeDatatable.columns.adjust().draw();
 
            $('#income-detail-modal').modal('hide');

            EnableNewOperation('income-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-income-detail-modal').click(function (event) {
        let b = $('#btn-edit-income-detail-dt').attr('rowindex');
        $('#select-all-income-detail').prop('checked', false);
        if (IsValidIncomeDetailModal()) {
            incomeDatatable.row(selectedRowIndex).data([
                tag,
                incomeSource,
                incomeSourceText,
                annualIncome,
                otherDetails,
                note,
                reasonForModification

            ]).draw();
            // Error Message In Span
            $('#income-detail-validation span').html('');

            HideColumnsIncomeDetailDatatable();

            incomeDatatable.columns.adjust().draw();

            $('#income-detail-modal').modal('hide');

            EnableNewOperation('income-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-income-detail-dt').click(function (event) {

        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-income-detail tbody input[type="checkbox"]:checked').each(function () {
                    incomeDatatable.row($('#tbl-income-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-income-detail-dt').data('rowindex');
                    EnableNewOperation('income-detail');

                    $('#select-all-income-detail').prop('checked', false);
                    //if (!incomeDatatable.data().any())
                    //$('#income-details-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -   
    $('#select-all-income-detail').click(function () {

        if ($(this).prop('checked')) {
            $('#tbl-income-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = incomeDatatable.row(row).index();

                rowData = (incomeDatatable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-income-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('income-detail');
            });
        }
        else {
            EnableNewOperation('income-detail');

            $('#tbl-income-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-income-detail tbody').click('input[type=checkbox]', function () {

        $('#tbl-income-detail input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = incomeDatatable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (incomeDatatable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('income-detail');

                    $('#btn-update-income-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-income-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-income-detail-dt').data('rowindex', arr);
                    $('#select-all-income-detail').data('rowindex', arr);
                }
            }
        });


        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-income-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('income-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('income-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('income-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-income-detail').prop('checked', true);
        else
            $('#select-all-income-detail').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidIncomeDetailModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        incomeSource = $('#income-source-id option:selected').val();
        incomeSourceText = $('#income-source-id option:selected').text();
        annualIncome = parseFloat($('#annual-incomes').val());
        otherDetails = $('#other-details').val();
        note = $('#note-income-detail').val();
        reasonForModification = $('#reason-for-modification-income-detail').val();

        //Set Default Value if Empty
        if (note === '') {
            $('#note-income-detail').val('None');
            note = 'None';
        }
        if (reasonForModification === '') {
            $('#reason-for-modification-income-detail').val('None');
            reasonForModification = 'None';
        }
        if ($('#other-details-input').hasClass('d-none')) {
            if (!otherDetails)
            { // This also checks for empty strings
                otherDetails = 'None';
            }
        }
        // Check if the selected text is 'Other Income'
        if (incomeSourceText === OTHER_INCOME || incomeSourceText === OTHER_INCOME_TEXT) {
            $('#other-details-input').removeClass('d-none');
        } else {
            $('#other-details').val('None');
            $('#other-details-input').addClass('d-none');
            $('#other-details-error').addClass('d-none');
        }

        // DocumentTypeId
        if ($('#income-source-id').prop('selectedIndex') < 1) {
            result = false;
            $('#income-source-id-error').removeClass('d-none');
        }

        // Regular expression to match up to 18 digits before decimal and 2 digits after
        if (isNaN(annualIncome) === false) {
            minimum = parseFloat($('#annual-incomes').attr('min'));
            maximum = parseFloat($('#annual-incomes').attr('max'));

            if (parseFloat(annualIncome) < parseFloat(minimum) || parseFloat(annualIncome) > parseFloat(maximum)) {
                result = false;
                $('#annual-incomes-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#annual-incomes-error').removeClass('d-none');
        }

        //reference Number
        maximumLength = parseInt($('#other-details').attr('maxlength'));

        if (parseInt(otherDetails.length) === 0 || parseInt(otherDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#other-details-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsIncomeDetailDatatable() {
        incomeDatatable.column(1).visible(false);

    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Set Page Loading Default Values @@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    function SetPageLoadingDefaultValues() {
        debugger
        IncomeSourceChange();
    }

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {

        debugger;
        let isValidAllInputs = true;
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.

            let personAdditionalIncomeDetailArray = new Array();


            // Create Array For person income detail Table To Pass Data
            if (!$('#heading-income-details').hasClass('d-none')) {

                if (incomeDatatable.data().any()) {

                    $('#income-details-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-income-detail > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (incomeDatatable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                personAdditionalIncomeDetailArray.push(
                                {
                                    'IncomeSourceId': columnValues[1],
                                    'AnnualIncome': columnValues[3],
                                    'OtherDetails': columnValues[4],
                                    'Note': columnValues[5],
                                    'ReasonForModification': columnValues[6]
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    //$('#income-details-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            debugger
            // Call Controller Save Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: saveDataTableURL,
                    type: 'POST',
                    async: false,
                    data: { '_additionalIncomeDetail': personAdditionalIncomeDetailArray, },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                        ok;
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