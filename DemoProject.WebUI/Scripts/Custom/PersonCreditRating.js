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
    let selectedRowIndex = 0;
    let row = 0;
    let rowData;
    let checked;
    let columnValues;
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let minimum = 0;
    let maximum = 0;
    let arr = new Array();

    // Credit Rating
    let effectiveDate = '';
    let agency;
    let agencyText = '';
    let score = 0;
    let creditRatingEffectiveDate;
    let result = true;
    let note = '';
    let reasonForModification = '';

    // M A I N     P A G E     I N P U T     V A L I D A T I O N 

    // Create DataTables
    let creditDataTable = CreateDataTable('credit-rating');

    
    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Credit Ratings - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-credit-rating-dt').click(function () {
        event.preventDefault();

        SetModalTitle('credit-rating', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-credit-rating-dt').click(function () {
        SetModalTitle('credit-rating', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#credit-rating-modal').modal();

            columnValues = $('#btn-edit-credit-rating-dt').data('rowindex');

            creditRatingEffectiveDate = new Date(columnValues[1]);

            $('#effective-date', myModal).val(GetInputDateFormat(creditRatingEffectiveDate));
            $('#credit-bureau-agency-id', myModal).val(columnValues[2]);
            $('#score', myModal).val(columnValues[4]);
            $('#note-credit-rating', myModal).val(columnValues[5]);
            $('#reason-for-modification-credit-rating', myModal).val(columnValues[6]);

            // Show Modals
            $('#credit-rating-modal').modal('show');
        }
        else {
            $('#btn-edit-credit-rating-edit-dt').addClass('read-only');
            $('#credit-rating-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-credit-rating-modal').click(function (event) {
        debugger;
        if (IsValidCreditRatingModal()) {
            row = creditDataTable.row.add([
                tag,
                effectiveDate,
                agency,
                agencyText,
                score,
                note,
                reasonForModification
            ]).draw();

            // Error Message In Span
            $('#credit-rating-data-table-error').addClass('d-none');

            HideColumnsCreditDataTable();

            creditDataTable.columns.adjust().draw();

            $('#credit-rating-modal').modal('hide');

            EnableNewOperation('credit-rating');
        }
    });

    // Modal update Button Event
    $('#btn-update-credit-rating-modal').click(function (event) {
        $('#select-all-credit-rating').prop('checked', false);
        if (IsValidCreditRatingModal()) {
            creditDataTable.row(selectedRowIndex).data([
                tag,
                effectiveDate,
                agency,
                agencyText,
                score,
                note,
                reasonForModification
            ]).draw();
            // Error Message In Span
            $('#credit-rating-validation span').html('');

            HideColumnsCreditDataTable();

            creditDataTable.columns.adjust().draw();

            $('#credit-rating-modal').modal('hide');

            EnableNewOperation('credit-rating');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-credit-rating-dt').click(function (event) {

        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-credit-rating tbody input[type="checkbox"]:checked').each(function () {
                    creditDataTable.row($('#tbl-credit-rating tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-credit-rating-dt').data('rowindex');
                    EnableNewOperation('credit-rating');

                    $('#select-all-credit-rating').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-credit-rating').click(function () {

        if ($(this).prop('checked')) {
            $('#tbl-credit-rating tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = creditDataTable.row(row).index();

                rowData = (creditDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-credit-rating-dt').data('rowindex', arr);
                EnableDeleteOperation('credit-rating');
            });
        }
        else {
            EnableNewOperation('credit-rating');

            $('#tbl-credit-rating tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-credit-rating tbody').click('input[type=checkbox]', function () {
        $('#tbl-credit-rating input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = creditDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (creditDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('credit-rating');

                    $('#btn-update-credit-rating-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-credit-rating-dt').data('rowindex', rowData);
                    $('#btn-delete-credit-rating-dt').data('rowindex', arr);
                    $('#select-all-credit-rating').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-credit-rating tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('credit-rating');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('credit-rating');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('credit-rating');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-credit-rating').prop('checked', true);
        else
            $('#select-all-credit-rating').prop('checked', false);
    });

    // Validate Credit Rating Module
    function IsValidCreditRatingModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        effectiveDate = $('#effective-date').val();
        agency = $('#credit-bureau-agency-id option:selected').val();
        agencyText = $('#credit-bureau-agency-id option:selected').text();
        score = parseInt($('#score').val());
        note = $('#note-credit-rating').val();
        reasonForModification = $('#reason-for-modification-credit-rating').val();
        
        //Set Default Value if Empty
        if (note === '') {
            $('#note-credit-rating').val('None');
            note = 'None';
        }

        //reason For Modification
        if (reasonForModification === '') {
            $('#reason-for-modification-credit-rating').val('None');
            reasonForModification = 'None';
        }
        
        //credit bureau agency
        if ($('#credit-bureau-agency-id').prop('selectedIndex') < 1) {
            result = false;
            $('#credit-bureau-agency-id-error').removeClass('d-none');
        }

        //score
        if (isNaN(score) === false) {

            minimum = parseInt($('#score').attr('min'));
            maximum = parseInt($('#score').attr('max'));

            if (parseInt(score) < parseInt(minimum) || parseInt(score) > parseInt(maximum)) {
                result = false;
                $('#score-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#score-error').removeClass('d-none');
        }

        //EffectiveDate
        let isValidEffectiveDate = IsValidInputDate('#effective-date');

        if (isValidEffectiveDate === false) {
            result = false;
            $('#effective-date-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsCreditDataTable() {
        creditDataTable.column(2).visible(false);
    }


    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;
        
        //if ($('form').valid()
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let personCreditRatingArray = new Array();

            // Create Array For person credit rating Table To Pass Data
            if (creditDataTable.data().any()) {
                if (isValidAllInputs) {

                    $('#tbl-credit-rating > tbody > tr').each(function () {
                        currentRow = $(this).closest('tr');

                        columnValues = (creditDataTable.row(currentRow).data());

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues !== 'undefined' && columnValues !== null) {
                            personCreditRatingArray.push(
                            {
                                'EffectiveDate': columnValues[1],
                                'CreditBureauAgencyId': columnValues[2],
                                'Score': columnValues[4],
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
                isValidAllInputs = false;
            }


            // Call Controller Save Method 
            if (isValidAllInputs) {
                debugger;
                $.ajax({
                    url: saveDataTableURL,
                    type: 'POST',
                    async: false,
                    data: {
                        '_creditRating': personCreditRatingArray
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Person Credit Rating DataTable!!! Error Message - ' + error.toString());
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