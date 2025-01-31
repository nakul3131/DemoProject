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
    let result = true;
    let tag = '';
    let id = '';
    let myModal;
    let selectedRowIndex;
    let row;
    let note;
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

    //Insurance Detail
    let insuranceType = '';
    let insuranceTypeText = '';
    let insuranceCompany = '';
    let insuranceCompanyText = '';
    let startDate = '';
    let closeDate = '';
    let maturityDate = '';
    let policyNumber = 0;
    let policyPremium;
    let policySumAssured;
    let overduesPremium;
    let hasAnyMortgage;
    let reasonForModification;
    let isDuplicateSequenceNumber = false;
    let editedPolicyNumber = 0;
    let lastSelectedValue = '';

    // M A I N     P A G E     I N P U T     V A L I D A T I O N 

    let insuranceDataTable = CreateDataTable('insurance-detail');
    
    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Start Date
    $('#activation-date').click(function ()
    {
        $('#close-date-person-insurance').val('');
    });

    // Maturity Date
    $('#expiry-date').click(function () {
        $('#close-date-person-insurance').val('');
    });

    // Update max attribute based on purchase price and clear sanction loan amount
    $('#policy-premium').focusout(function ()
    {
        let policyPremium = parseFloat($('#policy-premium').val());

        // Policy Premium If Empty Or Invalid
        if (isNaN(policyPremium) === true)
        {
            policyPremium = 0;
        }

        $('#policy-sum-assured').attr('min', policyPremium);

        $('#policy-sum-assured').val('');
    });
   
    // All Input Filed Values Clear After Change Insurance Type Id
    $('#insurance-type-id').focusout(function ()
    {
        let currentValue = $(this).val();

        if (lastSelectedValue !== currentValue)
        {
            $('#insurance-company-id').val('');
            $('input[type="date"], input[type="number"], textarea').val('');
            $('input[type="text"]').not('#checker-remark, #maker-remark ,#o-remark').val('');
            $('#has-any-mortgage-insurance').prop('checked', false);
        }

        lastSelectedValue = currentValue;
    });

    // Validate Unique Policy Number
    $('#policy-number').focusout(function () {
        let filteredData = insuranceDataTable
            .rows()
            .indexes()
            .filter(function (value, index) {
                return insuranceDataTable.row(value).data()[8] == $('#policy-number').val();
            });

        if (insuranceDataTable.rows(filteredData).count() > 0 && editedPolicyNumber !== $('#policy-number').val())
            $('#policy-numbers-error').removeClass('d-none');
        else
            $('#policy-numbers-error').addClass('d-none');
    });


    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Insurance Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-insurance-detail-dt').click(function () {

        event.preventDefault();
        editedPolicyNumber = 0
        lastSelectedValue = '';
        SetModalTitle('insurance-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-insurance-detail-dt').click(function ()
    {
        SetModalTitle('insurance-detail', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#insurance-detail-modal').modal();

            let tmpDate = new Date();

            columnValues = $('#btn-edit-insurance-detail-dt').data('rowindex');

            lastSelectedValue = columnValues[1];

            tmpDate = new Date(columnValues[5]);
            tmpDate.setDate(tmpDate.getDate() + 1);

            startDate = new Date(columnValues[5]);
            maturityDate = new Date(columnValues[6]);
            closeDate = new Date(columnValues[7]);

            $('#expiry-date').attr('min', GetInputDateFormat(tmpDate))
            $('#close-date-person-insurance').attr('min', GetInputDateFormat(tmpDate))

            $('#policy-sum-assured').attr('min', columnValues[9]);

            $('#insurance-type-id', myModal).val(columnValues[1]);
            $('#insurance-company-id', myModal).val(columnValues[3]);
            $('#activation-date', myModal).val(GetInputDateFormat(startDate));
            $('#expiry-date', myModal).val(GetInputDateFormat(maturityDate));
            $('#close-date-person-insurance', myModal).val(GetInputDateFormat(closeDate));
            $('#policy-number', myModal).val(columnValues[8]);
            $('#policy-premium', myModal).val(columnValues[9]);
            $('#policy-sum-assured', myModal).val(columnValues[10]);
            $('#overdues-premium', myModal).val(columnValues[11]);
            $('#has-any-mortgage-insurance', myModal).val(columnValues[12]);
            $('#note-insurance-detail', myModal).val(columnValues[13]);
            $('#reason-for-modification-insurance-detail', myModal).val(columnValues[14]);

            editedPolicyNumber = columnValues[8];

            if (columnValues[12] === 'True') {
                $('#has-any-mortgage-insurance').prop('checked', true);
            }
            else {
                $('#has-any-mortgage-insurance').prop('checked', false);
            }

            // Show Modals
            $('#insurance-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-insurance-detail-edit-dt').addClass('read-only');
            $('#insurance-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-insurance-detail-modal').click(function (event) {
        if (IsValidInsuranceModal()) {
            row = insuranceDataTable.row.add([
                        tag,
                        insuranceType,
                        insuranceTypeText,
                        insuranceCompany,
                        insuranceCompanyText,
                        startDate,
                        maturityDate,
                        closeDate,
                        policyNumber,
                        policyPremium,
                        policySumAssured,
                        overduesPremium,
                        hasAnyMortgage,
                        note,
                        reasonForModification
            ]).draw();

            // Error Message In Span
            $('#insurance-detail-data-table-error').addClass('d-none');

            HideColumnsInsuranceDataTable();

            insuranceDataTable.columns.adjust().draw();

            $('#insurance-detail-modal').modal('hide');

            EnableNewOperation('insurance-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-insurance-detail-modal').click(function (event) {
        $('#select-all-insurance-detail').prop('checked', false);
        if (IsValidInsuranceModal()) {
            insuranceDataTable.row(selectedRowIndex).data([
                                tag,
                                insuranceType,
                                insuranceTypeText,
                                insuranceCompany,
                                insuranceCompanyText,
                                startDate,
                                maturityDate,
                                closeDate,
                                policyNumber,
                                policyPremium,
                                policySumAssured,
                                overduesPremium,
                                hasAnyMortgage,
                                note,
                                reasonForModification,
            ]).draw();
            // Error Message In Span
            $('#insurance-detail-validation span').html('');

            HideColumnsInsuranceDataTable();

            insuranceDataTable.columns.adjust().draw();

            $('#insurance-detail-modal').modal('hide');

            EnableNewOperation('insurance-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-insurance-detail-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-insurance-detail tbody input[type="checkbox"]:checked').each(function () {
                 insuranceDataTable.row($('#tbl-insurance-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-insurance-detail-dt').data('rowindex');
                  EnableNewOperation('insurance-detail');

                  $('#select-all-insurance-detail').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-insurance-detail').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-insurance-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = insuranceDataTable.row(row).index();

                rowData = (insuranceDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-insurance-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('insurance-detail');
            });
        }
        else {
            EnableNewOperation('insurance-detail');

            $('#tbl-insurance-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-insurance-detail tbody').click('input[type=checkbox]', function () {
        $('#tbl-insurance-detail input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = insuranceDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (insuranceDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('insurance-detail');

                    $('#btn-update-insurance-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-insurance-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-insurance-detail-dt').data('rowindex', arr);
                    $('#select-all-insurance-detail').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-insurance-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('insurance-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('insurance-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('insurance-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-insurance-detail').prop('checked', true);
        else
            $('#select-all-insurance-detail').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidInsuranceModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        insuranceType = $('#insurance-type-id option:selected').val();
        insuranceTypeText = $('#insurance-type-id option:selected').text();
        insuranceCompany = $('#insurance-company-id option:selected').val();
        insuranceCompanyText = $('#insurance-company-id option:selected').text();
        startDate = $('#activation-date').val();
        maturityDate = $('#expiry-date').val();
        closeDate = $('#close-date-person-insurance').val();
        policyNumber = $('#policy-number').val();
        policyPremium = parseFloat($('#policy-premium').val());
        policySumAssured = parseFloat($('#policy-sum-assured').val());
        overduesPremium = parseInt( $('#overdues-premium').val());
        hasAnyMortgage = $('#has-any-mortgage-insurance').is(':checked') ? "True" : "False";
        note = $('#note-insurance-detail').val();
        reasonForModification = $('#reason-for-modification-insurance-detail').val();

        // Set Default Value, If Empty
        if (note === '') {
            $('#note-insurance-detail').val('None');
            note = 'None';
        }
           
        if (reasonForModification === '') {
            $('#reason-for-modification-insurance-detail').val('None');
            reasonForModification = 'None';
        }

        if (overduesPremium ==='') {
            overduesPremium = 0;
        }

        //vehicle model Id
        if ($('#insurance-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#insurance-type-id-error').removeClass('d-none');
        }

        //vehicle model Id
        if ($('#insurance-company-id').prop('selectedIndex') < 1) {
            result = false;
            $('#insurance-company-id-error').removeClass('d-none');
        }

        //date of purchase
        if (IsValidInputDate('#activation-date') === false) {
            result = false;
            $('#activation-date-error').removeClass('d-none');
        }

        const currentDate = new Date();
        let errorMessage = '';

        // Check if the start date field is empty
        if (startDate === '') {
            errorMessage = 'Policy Start Date is required.';
            result = false;
        } else {
            // Check if the date is in the future
            if (startDate > currentDate) {
                errorMessage = 'Start Date cannot be in the future.';
                result = false;
            }
        }

        // Show or hide error message
        if (errorMessage !== '') {
            $('#start-date-error').text(errorMessage).removeClass('d-none');
        } else {
            $('#start-date-error').addClass('d-none');
        }

        // Check if the maturity date field is empty
        if (maturityDate === '') {
            errorMessage = 'Policy Maturity Date is required.';
            result = false;
        } else {
            // Check if the maturity date is earlier than the start date
            if (maturityDate < startDate) {
                errorMessage = 'Maturity Date cannot be earlier than the Policy Start Date.';
                $('#maturity-date-insurance-error').removeClass('d-none');
                result = false;
            }
        }

        // Show or hide error message
        if (errorMessage !== '') {
            $('#expiry-date-error').text(errorMessage).removeClass('d-none');
        } else {

            $('#expiry-date-error').addClass('d-none');
        }

        //date of purchase
        if (IsValidInputDate('#expiry-date') === false) {
            result = false;
            $('#expiry-date-error').removeClass('d-none');
        }
        if (closeDate === '') {
            // Close Date can be empty, clear error
            $('#close-date-person-insurance-error').addClass('d-none');
        } else if (closeDate < startDate) {
            result = false;
            $('#close-date-person-insurance-error').removeClass('d-none');
        } else {
            $('#close-date-person-insurance-error').addClass('d-none');
        }

        //name Of Branch
        minimumLength = parseInt($('#policy-number').attr('minlength'));
        maximumLength = parseInt($('#policy-number').attr('maxlength'));

        if (parseInt(policyNumber.length) < parseInt(minimumLength) || parseInt(policyNumber.length) > parseInt(maximumLength)) {
            result = false;
            $('#policy-number-error').removeClass('d-none');
        }

        let filteredData = insuranceDataTable.rows().indexes().filter(function (value, index) {
            return insuranceDataTable.row(value).data()[8] == $('#policy-number').val();
        });

        if (insuranceDataTable.rows(filteredData).count() > 0 && editedPolicyNumber !== $('#policy-number').val()) {
            isDuplicateSequenceNumber = true;
            result = false;
            $('#policy-numbers-error').removeClass('d-none');
        }
        else {
            isDuplicateSequenceNumber = false;
            $('#policy-numbers-error').addClass('d-none');
        }

        if (isNaN(policyPremium) === false) {
            minimum = parseFloat($('#policy-premium').attr('min'));
            maximum = parseFloat($('#policy-premium').attr('max'));

            if (parseFloat(policyPremium) < parseFloat(minimum) || parseFloat(policyPremium) > parseFloat(maximum)) {
                result = false;
                $('#policy-premium-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#policy-premium-error').removeClass('d-none');
        }

        if (isNaN(policySumAssured) === false) {
            maximum = parseFloat($('#policy-sum-assured').attr('max'));
            minimum = parseFloat($('#policy-sum-assured').attr('min'));
            // Set max attribute for the current market value element
            $('#policy-sum-assured').attr('min', minimum);

            if (parseFloat(policySumAssured) < parseFloat(minimum) || parseFloat(policySumAssured) > parseFloat(maximum)) {
                result = false;
                $('#policy-sum-assured-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#policy-sum-assured-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsInsuranceDataTable() {
        insuranceDataTable.column(1).visible(false);
        insuranceDataTable.column(3).visible(false);
    }

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {

        debugger;

        let isValidAllInputs = true;
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.

            let personInsuranceDetailArray = new Array();

            // Create Array For person insurance detail Table To Pass Data
            if (!$('#heading-insurance-detail').hasClass('d-none')) {

                if (insuranceDataTable.data().any()) {

                    $('#insurance-detail-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-insurance-detail > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (insuranceDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                personInsuranceDetailArray.push(
                                {
                                    'InsuranceTypeId': columnValues[1],
                                    'InsuranceCompanyId': columnValues[3],
                                    'StartDate': columnValues[5],
                                    'MaturityDate': columnValues[6],
                                    'CloseDate': columnValues[7],
                                    'PolicyNumber': columnValues[8],
                                    'PolicyPremium': columnValues[9],
                                    'PolicySumAssured': columnValues[10],
                                    'OverduesPremium': columnValues[11],
                                    'HasAnyMortgage': columnValues[12],
                                    'Note': columnValues[13],
                                    'ReasonForModification': columnValues[14]
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                   // $('#insurance-detail-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;

                }
            }



            // Call Controller Save Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: saveDataTableURL,
                    type: 'POST',
                    async: false,
                    data: {
                        '_insuranceDetail': personInsuranceDetailArray
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Person Insurance Detail DataTable!!! Error Message - ' + error.toString());
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