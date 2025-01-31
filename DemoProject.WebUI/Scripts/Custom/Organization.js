
function GSTRegistration() {

    $('#gst-registration-number').keypress(function (e) {
        debugger;
        let input = $(this).val();
        let errorMessage = ''; // Variable to store the error message


        if (input.length >= 15) {
            errorMessage = 'Gst Number Accept Only 15 characters Or Empty';
            e.preventDefault();
        }

        // a. First two positions will be only digit
        if (input.length < 2 && !/\d/.test((e.key))) {
            e.preventDefault();
            errorMessage = 'First two positions should be digits';
        }

        // b. Third to Sixth positions will be Alphabets
        if (input.length >= 2 && input.length < 7) {
            if (/^[a-zA-Z]+$/.test(e.key)) {
                input += e.key.toUpperCase();
                e.preventDefault();
                $(this).val(input);
            } else {
                errorMessage = 'Third to Sixth positions should be alphabets';
            }
        }

        // Eighth to Eleventh positions will be digit
        if (input.length >= 7 && input.length < 10 && !/\d/.test((e.key))) {
            e.preventDefault();
            errorMessage = 'Eighth to Eleventh positions should be digits';
        }

        if (input.length === 11) {
            if (/^[a-zA-Z]+$/.test(e.key)) {
                input += e.key.toUpperCase();
                e.preventDefault();
                $(this).val(input);
            } else {
                errorMessage = 'Eleventh position should be an alphabet';
            }
        }

        // f. Thirteenth to Fifteenth positions will be alphanumeric
        if (input.length >= 12 && input.length < 15) {
            if (/^[a-zA-Z0-9]+$/.test(e.key)) {
                input += e.key.toUpperCase();
                e.preventDefault();
                $(this).val(input);
            }
            else {
                errorMessage = 'Thirteenth to Fifteenth positions should be alphanumeric';
            }

        }
        // Display the error message if any
        if (errorMessage) {
            $('#gst-registration-number-error').text(errorMessage).removeClass('d-none');
            return false;
        }

        // Clear error message if no errors
        $('#gst-registration-number-error').addClass('d-none');

    });

    $('#gst-registration-number').focusout(function () {
        let input = $(this).val();
        let errorMessage = ''; // Variable to store the error message

        if (input === "") {
            // Hide error message if input is empty
            $('#gst-registration-number-error').addClass('d-none');
            return; // Exit the function
        }


        // Check if the length is less than 10 characters after keyup
        if (input.length < 15) {
            errorMessage = 'Gst Number Accept Only 15 characters Or Empty';
        }

        // Display the error message if any
        if (errorMessage !== "") {
            $('#gst-registration-number-error').removeClass('d-none');
        } else {
            $('#gst-registration-number-error').addClass('d-none');
        }
    });
}

function PanNumber() {
    $('#pan-number').keypress(function (e) {
        debugger;

        let input = $(this).val();
        let errorMessage = ''; // Variable to store the error message


        // Check if the length is more than 10 characters
        if (input.length >= 10) {
            e.preventDefault();
            errorMessage = 'Pan Number Accept Only 10 characters Or Empty';
        }


        // Check if the first 5 characters are alphabets
        if (input.length < 5) {
            if (/^[a-zA-Z]+$/.test(e.key)) {
                input += e.key.toUpperCase();
                e.preventDefault();
                $(this).val(input);
            }
            errorMessage = 'First 5 characters must be alphabets';
        }

        if (input.length >= 5 && input.length <= 8) {
            debugger;

            const digitsIn6to9 = parseInt(input.substring(5, 9) + e.key);
            if (isNaN(digitsIn6to9)) {
                debugger;
                e.preventDefault();
                errorMessage = 'Characters from 6th to 9th must be digits';
            }
            else {
                $(this).val(input + "" + e.key);
                errorMessage = 'Characters from 6th to 9th must be digits';
            }

        }


        let t = (input + "" + e.key).toUpperCase();
        if (t.length == 9 || t.length == 10) {
            debugger;
            // Check if the 10th character is an alphabet
            if (/^\d$/.test(e.key)) {
                errorMessage = '10th character must be an alphabet';
            } else if (/^[a-zA-Z]+$/.test(e.key)) {
                //t += e.key.toUpperCase();
                e.preventDefault();
                $(this).val(t);
            } else {
                errorMessage = 'Invalid character for 10th position';
            }
        }

        // Display the error message if any
        if (errorMessage) {
            $('#pan-number-error').text(errorMessage).removeClass('d-none');
            return false;
        }

        // Clear error message if no errors
        $('#pan-number-error').addClass('d-none');

    });

    $('#pan-number').focusout(function () {
        let input = $(this).val();
        let errorMessage = ''; // Variable to store the error message

        if (input === "") {
            // Hide error message if input is empty
            $('#pan-number-error').addClass('d-none');
            return; // Exit the function
        }


        // Check if the length is less than 10 characters after keyup
        if (input.length < 10) {
            errorMessage = 'Pan Number Accept Only 10 characters Or Empty';
        }

        // Display the error message if any
        if (errorMessage !== "") {
            $('#pan-number-error').removeClass('d-none');
        } else {
            $('#pan-number-error').addClass('d-none');
        }
    });
}


'use strict'
$(document).ready(function () {
    debugger;

    // ************** D E C L A R A T I O N  ***************

    let id;
    let day;
    let month;
    let datepart;
    let activationDate;
    let expiryDate;
    let closeDate;
    let maturityDate;
    let depositType;

    let minimum;
    let maximum;

    // @@@@@@@@@@ Data Table Related Varible Declaration

    let isValidSchemeName = true;
    let tag = '';
    let myModal;
    let rowNum = 0;
    let selectedRowIndex;
    let row;
    let rowData;
    let checked;
    let columnValues;
    let Expirydate;

    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let result = true;

    let arr = new Array();
    let arrayCloumn1;

    //Contact
    let contactType;
    let fieldValue;
    let contactGroup;
    let contactGroupText;
    let isOpen;
    let contactTypeText;

    //GST Registration
    let state;
    let stateText;
    let gSTRegistrationType;
    let gSTRegistrationTypeText;
    let applicableFrom;
    let gSTReturnPeriodicity;
    let gSTReturnPeriodicityText;
    let isApplicableEWayBill;
    let thresholdLimit;
    let gSTRegistrationNumber;
    let registrationDate;
    let note;
    let reasonForModification;

    //Loan Type
    let accountId;
    let accountIdText;
    let maximumLoanTenure;
    let minimumDownPaymentPercentage;
    let sequenceNumber;
    let sequenceNumberText;
    let transSequenceNumberText;
    let transNote;

    //Fund
    let fundId;
    let fundIdText;

    //Create Data Table Declaration
    let contactDataTable = CreateDataTable('contact');
    let loanTypeDataTable = CreateDataTable('loan-type');
    let fundDataTable = CreateDataTable('fund');
    let gstRegistrationDataTable = CreateDataTable('gst-registration');


    GSTRegistration();
    PanNumber();

    //Pan Number are required For GST Registration Detail
    $('#pan-number').focusout(function () {
        let pancard = $(this).val();

        if (pancard.length == 10) {
            $('.gsthide').removeClass('d-none');
        } else {
            $('.gsthide').addClass('d-none');
        }
    });

    // threshold-limit
    $('#threshold-limit').focusout(function (e) {
        debugger;
        let amt = $(this).val();
        if ((amt == 2000000) || (amt == 4000000)) {
            $('#threshold-limit-error').addClass('d-none');
            $(this).val(amt);
        }
        else
            $('#threshold-limit-error').removeClass('d-none');
    })

    // WebSite Validation
    $('#official-website').focusout(function () {
        debugger;
        let input = $(this).val(); // Convert input to lowercase

        // Convert input to lowercase using a regular expression
        input = input.toLowerCase();

        let maxLength = 150; // Maximum length
        let regex = /^((https?|ftp|smtp):\/\/)?(www\.)?[a-z0-9]+\.[a-z]+(\/[a-zA-Z0-9#]+\/?)*$/;
        $(this).val($(this).val().toLowerCase());

        if (input !== 'none') { // Compare input variable, not $('#official-website').val()
            if (input.length > maxLength) {
                // Show error message for exceeding maximum length
                $('#official-website-error').text('URL exceeds maximum length of 150 characters.');
                $('#official-website-error').removeClass('d-none');
                return false;
            }
            if (!regex.test(input)) {
                // Show error message for invalid URL format
                $('#official-website-error').text('Invalid URL format.');
                $('#official-website-error').removeClass('d-none');
                return false;
            }
            // Hide error message if input is valid
            $('#official-website-error').addClass('d-none');
        }
    });


    //Translation of Number To Word
    function number2text(value) {
        let fraction = Math.round(frac(value) * 100);
        let f_text = "";

        if (fraction > 0) {
            f_text = "AND " + convert_number(fraction) + " Paise";
        }

        return convert_number(value) + " RUPEE " + f_text + " ONLY";
    }

    // Get Remainder
    function frac(f) {
        return f % 1;
    }

    // Convert Number To Word Function
    function convert_number(number) {
        if ((number < 0) || (number > 999999999)) {
            return "NUMBER OUT OF RANGE!";
        }

        // Crore 
        let Gn = Math.floor(number / 10000000);
        number -= Gn * 10000000;

        // Lakhs
        let kn = Math.floor(number / 100000);
        number -= kn * 100000;

        // Thousand 
        let Hn = Math.floor(number / 1000);
        number -= Hn * 1000;

        // Tens (deca)
        let Dn = Math.floor(number / 100);
        number = number % 100;

        // Ones 
        let tn = Math.floor(number / 10);
        let one = Math.floor(number % 10);
        let res = "";

        if (Gn > 0) {
            res += (convert_number(Gn) + " Crore");
        }

        if (kn > 0) {
            res += (((res == "") ? "" : " ") + convert_number(kn) + " Lakh");
        }

        if (Hn > 0) {
            res += (((res == "") ? "" : " ") + convert_number(Hn) + " Thousand");
        }

        if (Dn) {
            res += (((res == "") ? "" : " ") + convert_number(Dn) + " Hundred");
        }

        let ones = Array("", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen");
        let tens = Array("", "", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety");

        if (tn > 0 || one > 0) {
            if (!(res == "")) {
                res += " AND ";
            }

            if (tn < 2) {
                res += ones[tn * 10 + one];
            }
            else {
                res += tens[tn];

                if (one > 0) {
                    res += ("-" + ones[one]);
                }
            }
        }

        if (res == "") {
            res = "zero";
        }

        return res;
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@  FOCUSOUT FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@


    // CooperativeRegistration Accordion Input Validation
    $('.coop-registration-input').focusout(function () {
        if (IsValidCooperativeRegistrationAccordionInputs())
            $('#coop-registration-error').addClass('d-none');
    });

    // Authorized Shares Capital
    $('.authorized-input').focusout(function () {
        if (IsValidAuthorizedSharesCapitalAccordionInputs())
            $('#authorized-shares-capital-error').addClass('d-none');
    });


    // @@@@@@@@@@@@@@@@@@@@@@@@@@   VALIDATION FUNCTIONS FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@


    // 1.CooperativeRegistration Input Validation
    function IsValidCooperativeRegistrationAccordionInputs() {
         debugger;
        // Get values of cooperative registration inputs and trim any leading/trailing spaces
        let coopRegistrationNumber = $('#coop-registration-number').val();
        let transCoopRegistrationNumber = $('#trans-coop-registration-number').val();

         result = true;
 
        //Coop Registration Number
        if (isNaN(coopRegistrationNumber.length) === false && coopRegistrationNumber.length > 0) {
            minimum = $('#coop-registration-number').attr('min');
            maximum = $('#coop-registration-number').attr('max');

            if (coopRegistrationNumber.length < minimum || coopRegistrationNumber.length > maximum)
                result = false;
        }
        else
            result = false;

        //Trans Coop Registration Number
        if (isNaN(transCoopRegistrationNumber.length) === false && transCoopRegistrationNumber.length > 0) {
            minimum = $('#trans-coop-registration-number').attr('min');
            maximum = $('#trans-coop-registration-number').attr('max');

            if (transCoopRegistrationNumber.length < minimum || transCoopRegistrationNumber.length > maximum)
                result = false;
        }
        else
            result = false;


        // Check If Values Is Not Empty
        if ($('#registration-date-coop-registration').val() === '') {
            result = false;
        }

        // Check If Values Is Not Empty
        if ($('#coop-classification').val() === '') {
            result = false;
        }

        // Check If Values Is Not Empty
        if ($('#coop-sub-classification').val() === '') {
            result = false;
        }

        // Check If Values Is Not Empty
        if ($('#coop-date-of-classification-advice').val() === '') {
            result = false;
        }

        // Check If Values Is Not Empty
        if ($('#last-election-date').val() === '') {
            result = false;
        }

        // Based on result, show or hide the error message
        if (result) {
            $('#coop-registration-error').addClass('d-none');
        } else {
            $('#coop-registration-error').removeClass('d-none');
        }

         return result;
    }

    // 1.Authorized Shares Capital Input Validation
    function IsValidAuthorizedSharesCapitalAccordionInputs() {
        debugger;
        let authorizedSharesCapitalAmount = parseFloat($('#authorized-shares-capital-amount').val());
        let referenceNumber = parseInt($('#reference-number').val());

        result = true;

        // Check If Values Is Not Empty
        if ($('#effective-date').val() === '') {
            result = false;
        }

        // Check If Values Is Not Empty
        if ($('#authorized-date').val() === '') {
            result = false;
        }

        // Validation Authorized Shares Capital Amount
        if (isNaN(authorizedSharesCapitalAmount) === false) {
            minimum = parseFloat($('#authorized-shares-capital-amount').attr('min'));
            maximum = parseFloat($('#authorized-shares-capital-amount').attr('max'));

            if (parseFloat(authorizedSharesCapitalAmount) < parseFloat(minimum) || parseFloat(authorizedSharesCapitalAmount) > parseFloat(maximum))
                result = false;
        }
        else
            result = false;

        // Validation Reference Number
        if (isNaN(referenceNumber) === false) {
            minimum = parseInt($('#reference-number').attr('min'));
            maximum = parseInt($('#reference-number').attr('max'));

            if (parseInt(referenceNumber) < parseInt(minimum) || parseInt(referenceNumber) > parseInt(maximum))
                result = false;
        }
        else
            result = false;

        if (result)
            $('#authorized-shares-capital-error').addClass('d-none');
        else
            $('#authorized-shares-capital-error').removeClass('d-none');

        return result;
    }


    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // @@@@@@@@@@@@@@@@@@@@@@   Scheme Contact - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // Clear contact ModalInputs();
    ClearModal('contact');

    // DataTable Add Button 
    $('#btn-add-contact-dt').click(function () {
        debugger;
        event.preventDefault();
        SetModalTitle('contact', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-contact-dt').click(function () {
        SetModalTitle('contact', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-contact-dt').data('rowindex');
            id = $('#contact-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs
            $('#contact-type', myModal).val(columnValues[1]);
            $('#field-value', myModal).val(columnValues[3]);
            $('#contact-group', myModal).val(columnValues[4]);
            $('#is-open', myModal).val(columnValues[6]);

            if (columnValues[6] === 'True') {
                $('#is-open').prop('checked', true);
            }
            else {
                $('#is-open').prop('checked', false);
            }

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-contact-dt').addClass('read-only');
            $('#contact-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-contact-modal').click(function (event) {
        debugger;
        if (IsValidContactDataTableModal()) {

            row = contactDataTable.row.add([
            tag,
            contactType,
            contactTypeText,
            fieldValue,
            contactGroup,
            contactGroupText,
            isOpen,

            ]).draw();

            // Error Message In Span
            $('#contact-error').addClass('d-none');

            HideContactDataTableColumns();

            contactDataTable.columns.adjust().draw();

            ClearModal('contact');

            $('#contact-modal').modal('hide');

            EnableNewOperation('contact');

        }
    });

    // Modal update Button Event
    $('#btn-update-contact-modal').click(function (event) {

        $('#select-all-contact').prop('checked', false);
        if (IsValidContactDataTableModal()) {
            contactDataTable.row(selectedRowIndex).data([
                              tag,
                              contactType,
                              contactTypeText,
                              fieldValue,
                              contactGroup,
                              contactGroupText,
                              isOpen,

            ]).draw();
            // Error Message In Span
            $('#contact-validation span').html('');

            HideContactDataTableColumns();

            contactDataTable.columns.adjust().draw();

            $('#contact-modal').modal('hide');

            EnableNewOperation('contact');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-contact-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-contact tbody input[type="checkbox"]:checked').each(function () {
                    contactDataTable.row($('#tbl-contact tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-contact-dt').data('rowindex');
                    EnableNewOperation('contact');

                    $('#select-all-contact').prop('checked', false);
                     if (!contactDataTable.data().any())
                    $('#contact-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-contact').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-contact tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = contactDataTable.row(row).index();

                rowData = (contactDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-contact-dt').data('rowindex', arr);
                EnableDeleteOperation('contact')
            });
        }
        else {
            EnableNewOperation('contact')

            $('#tbl-contact tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-contact tbody').click("input[type=checkbox]", function () {
        $('#tbl-contact input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = contactDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (contactDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('contact');

                    $('#btn-update-contact-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-contact-dt').data('rowindex', rowData);
                    $('#btn-delete-contact-dt').data('rowindex', arr);
                    $('#select-all-contact').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-contact tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('contact');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('contact');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('contact');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-contact').prop('checked', true);
        else
            $('#select-all-contact').prop('checked', false);
    });

    // Validate Contact Module
    function IsValidContactDataTableModal() {

        result = true;
        debugger;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        contactType = $('#contact-type option:selected').val();
        contactTypeText = $('#contact-type option:selected').text();
        fieldValue = $('#field-value').val();
        contactGroup = $('#contact-group option:selected').val();
        contactGroupText = $('#contact-group option:selected').text();
        isOpen = $('input[name="OrganizationContactDetailViewModel.IsOpen"]').is(':checked') ? "True" : "False";

            if (contactType == '') {
                result = false;
                $('#contact-type-error').removeClass('d-none');
            }
            else
                $('#contact-type-error').removeClass('d-none');

            if (fieldValue === '' || fieldValue.length < 1 || fieldValue.length > 50) {
                result = false;
                $('#field-value-error').removeClass('d-none');
            } else
                $('#field-value-error').addClass('d-none');


            if (contactGroup == '') {
                result = false;
                $('#contact-group-error').removeClass('d-none');
            }  else
                $('#contact-group-error').addClass('d-none');
            return  result ;
    }

    // Hide Unnecessary Columns
    function HideContactDataTableColumns() {
        contactDataTable.column(1).visible(false);
        contactDataTable.column(4).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@  Fund - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@


    ClearModal('fund');

    // DataTable Add Button 
    $('#btn-add-fund-dt').click(function () {

        event.preventDefault();

        SetModalTitle('fund', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-fund-dt').click(function () {

        SetModalTitle('fund', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-fund-dt').data('rowindex');
            id = $('#fund-modal').attr('id');
            myModal = $('#' + id).modal();
            // Get Only Date
            // Get Only Activation Date
            let datepart = columnValues[6].split(' ')[0];

            if (datepart.length = 0) {
                datepart = columnValues[6]
            }

            const t = new Date(datepart);

            today = t.toLocaleDateString("en-US");

            const date = ('0' + t.getDate()).slice(-2);
            const month = ('0' + (t.getMonth() + 1)).slice(-2);
            const year = t.getFullYear();

            if (isNaN(year) == true) {
                // Split Date In Arry
                let arr = datepart.split('-');

                let dd = arr[0];
                let mm = arr[1];
                let yyyy = arr[2];

                activationDate = [yyyy, mm, dd].join('-');
            }
            else
                activationDate = [year, month, date].join('-');


            // Get Only Expiry Date
            datepart = columnValues[7].split(' ')[0];

            if (datepart.length = 0) {
                datepart = columnValues[7]
            }

            const t1 = new Date(datepart);

            let today1 = t1.toLocaleDateString("en-US");

            const date1 = ('0' + t1.getDate()).slice(-2);
            const month1 = ('0' + (t1.getMonth() + 1)).slice(-2);
            const year1 = t1.getFullYear();

            if (isNaN(year1) == true) {
                // Split Date In Arry
                let arr = datepart.split('-');

                let dd = arr[0];
                let mm = arr[1];
                let yyyy = arr[2];

                expiryDate = [yyyy, mm, dd].join('-');
            }
            else
                expiryDate = [year1, month1, date1].join('-');

            // Get Only Close Date
            datepart = columnValues[8].split(' ')[0];

            if (datepart.length = 0) {
                datepart = columnValues[8]
            }

            const t2 = new Date(datepart);

            let today2 = t2.toLocaleDateString("en-US");

            const date2 = ('0' + t2.getDate()).slice(-2);
            const month2 = ('0' + (t2.getMonth() + 1)).slice(-2);
            const year2 = t2.getFullYear();

            if (isNaN(year2) == true) {
                // Split Date In Arry
                let arr = datepart.split('-');

                let dd = arr[0];
                let mm = arr[1];
                let yyyy = arr[2];

                let closeDate = [yyyy, mm, dd].join('-');
            }
            else
                closeDate = [year2, month2, date2].join('-');


            // Display Value In Modal Inputs
            $('#fund-id', myModal).val(columnValues[1]);
            $('#sequence-number', myModal).val(columnValues[3]);
            $('#sequence-number-text', myModal).val(columnValues[4]);
            $('#trans-sequence-number-text', myModal).val(columnValues[5]);
            $('#activation-date-organization-fund', myModal).val(activationDate);
            $('#expiry-date-organization-fund', myModal).val(expiryDate);
            $('#close-date-organization-fund', myModal).val(closeDate);
            $('#note-organization-fund', myModal).val(columnValues[9]);
            $('#trans-note-organization-fund', myModal).val(columnValues[10]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('.btn-edit-fund-edit-dt').addClass('read-only');
            $('#fund-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-fund-modal').click(function (event) {
        if (IsValidFundDataTableModal()) {
            row = fundDataTable.row.add([
                        tag,
                        fundId,
                        fundIdText,
                        sequenceNumber,
                        sequenceNumberText,
                        transSequenceNumber,
                        activationDate,
                        expiryDate,
                        closeDate,
                        note,
                        transNote
            ]).draw();

            // Error Message In Span
            $('#fund-data-table-error').addClass('d-none');

            HideColumnsFundDataTable();

            fundDataTable.columns.adjust().draw();

            ClearModal('fund');

            $('#fund-modal').modal('hide');

            EnableNewOperation('fund');
        }
    });

    // Modal update Button Event
    $('#btn-update-fund-modal').click(function (event) {

        $('#select-all-fund').prop('checked', false);
        if (IsValidFundDataTableModal()) {
            fundDataTable.row(selectedRowIndex).data([
                        tag,
                        fundId,
                        fundIdText,
                        sequenceNumber,
                        sequenceNumberText,
                        transSequenceNumber,
                        activationDate,
                        expiryDate,
                        closeDate,
                        note,
                        transNote
            ]).draw();
             HideColumnsFundDataTable();

            fundDataTable.columns.adjust().draw();

            $('#fund-modal').modal('hide');

            EnableNewOperation('fund');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-fund-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-fund tbody input[type="checkbox"]:checked').each(function () {
                 fundDataTable.row($('#tbl-fund tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-fund-dt').data('rowindex');
                  EnableNewOperation('fund');

                  $('#select-all-fund').prop('checked', false);
                   if (!fundDataTable.data().any())
                    $('#fund-data-table-error').removeClass('d-none');


                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-fund').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-fund tbody input[type="checkbox"]').each(function (index) {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                rowData = (fundDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-fund-dt').data('rowindex', arr);
                EnableDeleteOperation('fund')
            });
        }
        else {
            EnableNewOperation('fund')

            $('#tbl-fund tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-fund tbody').click('input[type="checkbox"]', function () {
        $('#tbl-fund input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = fundDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (fundDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('fund');

                    $('#btn-update-fund-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-fund-dt').data('rowindex', rowData);
                    $('#btn-delete-fund-dt').data('rowindex', arr);
                    $('#select-all-fund').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-fund tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('fund');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('fund');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('fund');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-fund').prop('checked', true);
        else
            $('#select-all-fund').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-fund > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (fundDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#fund-general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate Fund Module
    function IsValidFundDataTableModal() {
        debugger
        result = true;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        fundId = $('#fund-id option:selected').val();
        fundIdText = $('#fund-id option:selected').text();
        sequenceNumber = $('#sequence-number').val();
        sequenceNumberText = $('#sequence-number-text').val();
        transSequenceNumber = $('#trans-sequence-number-text').val();
        activationDate = $('#activation-date-organization-fund').val();
        expiryDate = $('#expiry-date-organization-fund').val();
        closeDate = $('#close-date-organization-fund').val();
        note = $('#note-organization-fund').val();
        transNote = $('#trans-note-organization-fund').val();
        if (note == '') {
            note = 'None';
        }
        if (transNote == '') {
            transNote = 'None';
        }
        let filteredData = fundDataTable
            .rows()
            .indexes()
            .filter(function (value, index) {
                return fundDataTable.row(value).data()[3] == $('#sequence-number').val();
            });

        let isValidActivationDate = IsValidInputDate('#activation-date-organization-fund');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-organization-fund');


        // Validate General Ledger
        if (fundId == '') {
            result = false;
            $('#fund-id-error').removeClass('d-none');
        }
        else
            $('#fund-id-error').addClass('d-none');

        if (sequenceNumber == '' || parseInt(sequenceNumber) < 1 || parseInt(sequenceNumber) > 50) {
            result = false;
            $('#sequence-number-error').removeClass('d-none');
        }
        else
            $('#sequence-number-error').addClass('d-none');


        if (sequenceNumberText === '' || sequenceNumberText.length < 1 || sequenceNumberText.length > 50) {
            result = false;
            $('#sequence-number-text-loan-type-error').removeClass('d-none');
        } else {
            $('#sequence-number-text-loan-type-error').addClass('d-none');
        }

        // Validate Charges
        if (!isValidActivationDate || !isValidExpiryDate) {
            result = false;
            $('#activation-date-organization-fund-error').removeClass('d-none');
        }
        else
            $('#activation-date-organization-fund-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsFundDataTable() {
        fundDataTable.column(1).visible(false);
        fundDataTable.column(8).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@  Loan Type - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@


    ClearModal('loan-type');

    // DataTable Add Button 
    $('#btn-add-loan-type-dt').click(function () {

        event.preventDefault();

        SetModalTitle('loan-type', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-loan-type-dt').click(function () {

        SetModalTitle('loan-type', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-loan-type-dt').data('rowindex');
            id = $('#loan-type-modal').attr('id');
            myModal = $('#' + id).modal();

            // Get Only Date
            // Get Only Activation Date
            datepart = columnValues[8].split(' ')[0];

            if (datepart.length = 0)
                datepart = columnValues[8]

            const tloanType = new Date(datepart);

            let todayloanType = tloanType.toLocaleDateString("en-US");

            const dateloanType = ('0' + tloanType.getDate()).slice(-2);
            const monthloanType = ('0' + (tloanType.getMonth() + 1)).slice(-2);
            const yearloanType = tloanType.getFullYear();

            if (isNaN(yearloanType) == true) {
                // Split Date In Arry
                let arr = datepart.split('-');

                let dd = arr[0];
                let mm = arr[1];
                let yyyy = arr[2];

                activationDate = [yyyy, mm, dd].join('-');
            }
            else
                activationDate = [yearloanType, monthloanType, dateloanType].join('-');

            // Get Only Expiry Date
            datepart = columnValues[9].split(' ')[0];

            if (datepart.length = 0)
                datepart = columnValues[9]

            const tloanType1 = new Date(datepart);

            let todayloanType1 = tloanType1.toLocaleDateString("en-US");

            const dateloanType1 = ('0' + tloanType1.getDate()).slice(-2);
            const monthloanType1 = ('0' + (tloanType1.getMonth() + 1)).slice(-2);
            const yearloanType1 = tloanType1.getFullYear();

            if (isNaN(yearloanType1) == true) {
                // Split Date In Arry
                let arr = datepart.split('-');

                let dd = arr[0];
                let mm = arr[1];
                let yyyy = arr[2];

                expiryDate = [yyyy, mm, dd].join('-');
            }
            else
                expiryDate = [yearloanType1, monthloanType1, dateloanType1].join('-');

            // Get Only Close Date
            datepart = columnValues[10].split(' ')[0];

            if (datepart.length = 0)
                datepart = columnValues[10]

            const tloanType2 = new Date(datepart);

            let todayloanType2 = tloanType2.toLocaleDateString("en-US");

            const dateloanType2 = ('0' + tloanType2.getDate()).slice(-2);
            const monthloanType2 = ('0' + (tloanType2.getMonth() + 1)).slice(-2);
            const yearloanType2 = tloanType2.getFullYear();

            if (isNaN(yearloanType2) == true) {
                // Split Date In Arry
                let arr = datepart.split('-');

                let dd = arr[0];
                let mm = arr[1];
                let yyyy = arr[2];

                closeDate = [yyyy, mm, dd].join('-');
            }
            else
                closeDate = [yearloanType2, monthloanType2, dateloanType2].join('-');

            // Display Value In Modal Inputs
            $('#account-class-id', myModal).val(columnValues[1]);
            $('#maximum-loan-tenure', myModal).val(columnValues[3]);
            $('#minimum-down-payment-percentage', myModal).val(columnValues[4]);
            $('#sequence-number-loan-type', myModal).val(columnValues[5]);
            $('#sequence-number-text-loan-type', myModal).val(columnValues[6]);
            $('#trans-sequence-number-text-loan-type', myModal).val(columnValues[7]);
            $('#activation-date-loan-type', myModal).val(activationDate);
            $('#expiry-date-organization-loan-type', myModal).val(expiryDate);
            $('#close-date-organization-loan-type', myModal).val(closeDate);
            $('#note-organization-loan-type', myModal).val(columnValues[11]);
            $('#trans-note-organization-loan-type', myModal).val(columnValues[12]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-loan-type-dt').addClass('read-only');
            $('#loan-type-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-loan-type-modal').click(function (event) {
        if (IsValidLoanTypeDataTableModal()) {
            row = loanTypeDataTable.row.add([
                          tag,
                          accountId,
                          accountIdText,
                          maximumLoanTenure,
                          minimumDownPaymentPercentage,
                          sequenceNumber,
                          sequenceNumberText,
                          transSequenceNumberText,
                          activationDate,
                          expiryDate,
                          closeDate,
                          note,
                          transNote,
            ]).draw();

            // Error Message In Span
            $('#loan-type-data-table-error').addClass('d-none');

            HideColumnsLoanTypeDataTable();

            loanTypeDataTable.columns.adjust().draw();

            ClearModal('loan-type');

            $('#loan-type-modal').modal('hide');

            EnableNewOperation('loan-type');
        }
    });

    // Modal update Button Event
    $('#btn-update-loan-type-modal').click(function (event) {

        $('#select-all-loan-type').prop('checked', false);
        if (IsValidLoanTypeDataTableModal()) {
            loanTypeDataTable.row(selectedRowIndex).data([
                                tag,
                                accountId,
                                accountIdText,
                                maximumLoanTenure,
                                minimumDownPaymentPercentage,
                                sequenceNumber,
                                sequenceNumberText,
                                transSequenceNumberText,
                                activationDate,
                                expiryDate,
                                closeDate,
                                note,
                                transNote,
            ]).draw();
 
            HideColumnsLoanTypeDataTable();

            loanTypeDataTable.columns.adjust().draw();

            $('#loan-type-modal').modal('hide');

            EnableNewOperation('loan-type');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-loan-type-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-loan-type tbody input[type="checkbox"]:checked').each(function () {
                 loanTypeDataTable.row($('#tbl-loan-type tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-loan-type-dt').data('rowindex');
                  EnableNewOperation('loan-type');

                  $('#select-all-loan-type').prop('checked', false);
                    if (!loanTypeDataTable.data().any())
                    $('#loan-type-data-table-error').removeClass('d-none');


                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-loan-type').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-loan-type tbody input[type="checkbox"]').each(function (index) {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                rowData = (loanTypeDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-loan-type-dt').data('rowindex', arr);
                EnableDeleteOperation('loan-type')
            });
        }
        else {
            EnableNewOperation('loan-type')

            $('#tbl-loan-type tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-loan-type tbody').click('input[type="checkbox"]', function () {
        $('#tbl-loan-type input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = loanTypeDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (loanTypeDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('loan-type');

                    $('#btn-update-loan-type-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-loan-type-dt').data('rowindex', rowData);
                    $('#btn-delete-loan-type-dt').data('rowindex', arr);
                    $('#select-all-loan-type').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-loan-type tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('loan-type');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('loan-type');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('loan-type');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-loan-type').prop('checked', true);
        else
            $('#select-all-loan-type').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-loan-type > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (loanTypeDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#account-class-id').find("option[value='" + columnValues[1] + "']").addClass('d-none');
        else
            return true;
    });

    // Validate Fund Module
    function IsValidLoanTypeDataTableModal() {
        debugger
        result = true;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        accountId = $('#account-class-id option:selected').val();
        accountIdText = $('#account-class-id option:selected').text();
        maximumLoanTenure = $('#maximum-loan-tenure').val();
        minimumDownPaymentPercentage = $('#minimum-down-payment-percentage').val();
        sequenceNumber = $('#sequence-number-loan-type').val();
        sequenceNumberText = $('#sequence-number-text-loan-type').val();
        transSequenceNumberText = $('#trans-sequence-number-text-loan-type').val();
        activationDate = $('#activation-date-loan-type').val();
        expiryDate = $('#expiry-date-organization-loan-type').val();
        closeDate = $('#close-date-organization-loan-type').val();
        note = $('#note-organization-loan-type').val();
        transNote = $('#trans-note-organization-loan-type').val();

        if (note == '') {
            note = 'None';
        }
        if (transNote == '') {
            transNote = 'None';
        }
        columnValues = $('#btn-update-loan-type-modal').data('rowindex');

        let filteredData = loanTypeDataTable
            .rows()
            .indexes()
            .filter(function (value, index) {
                return loanTypeDataTable.row(value).data()[5] == $('#sequence-number-loan-type').val();
            });

        let isValidActivationDate = IsValidInputDate('#activation-date-loan-type');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-organization-loan-type');

        // Validate General Ledger
        if (accountId == '') {
            result = false;
            $('#account-class-id-error').removeClass('d-none');
        }
        else
            $('#account-class-id-error').addClass('d-none');

        // Validate Charges
        if (maximumLoanTenure == '' || parseInt(maximumLoanTenure) < 1 || parseInt(maximumLoanTenure) > 199) {
            result = false;
            $('#maximum-loan-tenure-error').removeClass('d-none');
        }
        else
            $('#maximum-loan-tenure-error').addClass('d-none');


        if (sequenceNumberText == '' || sequenceNumberText.length < 1 || sequenceNumberText.length > 50) {
            result = false;
            $('#sequence-number-text-loan-type-error').removeClass('d-none');
        }
        else
            $('#sequence-number-text-loan-type-error').addClass('d-none');


        if (sequenceNumber == '' || parseInt(sequenceNumber) < 0 || parseInt(sequenceNumber) > 255) {
            result = false;
            $('#sequence-number-loan-type-error').removeClass('d-none');
        }
        else
            $('#sequence-number-loan-type-error').addClass('d-none');

        if (minimumDownPaymentPercentage == '' || parseFloat(minimumDownPaymentPercentage) < 0 || parseFloat(minimumDownPaymentPercentage) > 80) {
            result = false;
            $('#minimum-down-payment-percentage-error').removeClass('d-none');
        }
        else
            $('#minimum-down-payment-percentage-error').addClass('d-none');

        if (!isValidActivationDate) {
            result = false;
            $('#activation-date-loan-type-error').removeClass('d-none');
        } else
            $('#activation-date-loan-type-error').addClass('d-none');

        if (!isValidExpiryDate) {
            result = false;
            $('#expiry-date-organization-loan-type-error').removeClass('d-none');
        } else
            $('#expiry-date-organization-loan-type-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsLoanTypeDataTable() {
        loanTypeDataTable.column(1).visible(false);
        loanTypeDataTable.column(10).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@  GST Registration- DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@


    ClearModal('gst-registration');

    // DataTable Add Button 
    $('#btn-add-gst-registration-dt').click(function () {

        event.preventDefault();

        SetModalTitle('gst-registration', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-gst-registration-dt').click(function () {

        SetModalTitle('gst-registration', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-gst-registration-dt').data('rowindex');
            id = $('#gst-registration-modal').attr('id');
            myModal = $('#' + id).modal();

            // Get Only applicableFrom Date
            let datepart = columnValues[5].split(' ')[0];

            if (datepart.length = 0)
                datepart = columnValues[5]

            const tapplicableFrom = new Date(datepart);

            let todayapplicableFrom = tapplicableFrom.toLocaleDateString("en-US");

            const dateapplicableFrom = ('0' + tapplicableFrom.getDate()).slice(-2);
            const monthapplicableFrom = ('0' + (tapplicableFrom.getMonth() + 1)).slice(-2);
            const yearapplicableFrom = tapplicableFrom.getFullYear();

            if (isNaN(yearapplicableFrom) == true) {
                // Split Date In Arry
                let arr = datepart.split('-');

                let dd = arr[0];
                let mm = arr[1];
                let yyyy = arr[2];

                let applicableFrom = [yyyy, mm, dd].join('-');
            }
            else {
                let applicableFrom = [yearapplicableFrom, monthapplicableFrom, dateapplicableFrom].join('-');
            }

            // Get Only registration Date 
            datepart = columnValues[11].split(' ')[0];

            if (datepart.length = 0)
                datepart = columnValues[11]

            const tregistrationDate1 = new Date(datepart);

            let todayregistrationDate1 = tregistrationDate1.toLocaleDateString("en-US");

            const dateregistrationDate1 = ('0' + tregistrationDate1.getDate()).slice(-2);
            const monthregistrationDate1 = ('0' + (tregistrationDate1.getMonth() + 1)).slice(-2);
            const yearregistrationDate1 = tregistrationDate1.getFullYear();

            if (isNaN(yearregistrationDate1) == true) {
                // Split Date In Arry
                let arr = datepart.split('-');

                let dd = arr[0];
                let mm = arr[1];
                let yyyy = arr[2];

                let registrationDate = [yyyy, mm, dd].join('-');
            }
            else {
                let registrationDate = [yearregistrationDate1, monthregistrationDate1, dateregistrationDate1].join('-');
            }

            // Get Only Close Registration Date
            datepart = columnValues[12].split(' ')[0];

            if (datepart.length = 0)
                datepart = columnValues[12]

            // Get Only Close Registration Date
            const tRegistration2 = new Date(datepart);

            let todayRegistration2 = tRegistration2.toLocaleDateString("en-US");

            const dateRegistration2 = ('0' + tRegistration2.getDate()).slice(-2);
            const monthRegistration2 = ('0' + (tRegistration2.getMonth() + 1)).slice(-2);
            const yearRegistration2 = tRegistration2.getFullYear();

            if (isNaN(yearRegistration2) == true) {
                // Split Date In Arry
                let arr = datepart.split('-');

                let dd = arr[0];
                let mm = arr[1];
                let yyyy = arr[2];

                let closeDateRegistration = [yyyy, mm, dd].join('-');
            }
            else
                closeDateRegistration = [yearRegistration2, monthRegistration2, dateRegistration2].join('-');

            // Display Value In Modal Inputs
            $('#state-id', myModal).val(columnValues[1]);
            $('#gst-registration-type-id', myModal).val(columnValues[3]);
            $('#applicable-from', myModal).val(applicableFrom);
            $('#gst-return-periodicity-id', myModal).val(columnValues[6]);
            $('#isApplicableEWayBill', myModal).val(columnValues[8]);
            $('#threshold-limit', myModal).val(columnValues[9]);
            $('#gst-registration-number', myModal).val(columnValues[10]);
            $('#activation-date-registration-date', myModal).val(columnValues[11]);
            $('#expiry-date-close-date-registration-detail', myModal).val(columnValues[12]);
            $('#note-registration-detail', myModal).val(columnValues[13]);
            $('#reason-for-modification-registration-detail', myModal).val(columnValues[14]);
            if (columnValues[8] == 'True') {
                $('#is-applicable-ewaybill').prop('checked', true);
            }
            else {
                $('#is-applicable-ewaybill').prop('checked', false);
            }

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-gst-registration-dt').addClass('read-only');
            $('#gst-registration-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-gst-registration-modal').click(function (event) {
        if (IsValidGSTRegistrationDataTableModal()) {
            row = gstRegistrationDataTable.row.add([
                             tag,
                             state,
                             stateText,
                             gSTRegistrationType,
                             gSTRegistrationTypeText,
                             applicableFrom,
                             gSTReturnPeriodicity,
                             gSTReturnPeriodicityText,
                             isApplicableEWayBill,
                             thresholdLimit,
                             gSTRegistrationNumber,
                             registrationDate,
                             closeDate,
                             note,
                             reasonForModification
            ]).draw();

            // Error Message In Span
            $('#gst-registration-error').addClass('d-none');

            HideColumnsGSTRegistrationDataTable();

            gstRegistrationDataTable.columns.adjust().draw();

            ClearModal('gst-registration');

            $('#gst-registration-modal').modal('hide');

            EnableNewOperation('gst-registration');
        }
    });

    // Modal update Button Event
    $('#btn-update-gst-registration-modal').click(function (event) {

        $('#select-all-gst-registration').prop('checked', false);
        if (IsValidGSTRegistrationDataTableModal()) {
            gstRegistrationDataTable.row(selectedRowIndex).data([
                             tag,
                             state,
                             stateText,
                             gSTRegistrationType,
                             gSTRegistrationTypeText,
                             applicableFrom,
                             gSTReturnPeriodicity,
                             gSTReturnPeriodicityText,
                             isApplicableEWayBill,
                             thresholdLimit,
                             gSTRegistrationNumber,
                             registrationDate,
                             closeDate,
                             note,
                             reasonForModification
            ]).draw();
 
            HideColumnsGSTRegistrationDataTable();

            gstRegistrationDataTable.columns.adjust().draw();

            $('#gst-registration-modal').modal('hide');

            EnableNewOperation('gst-registration');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-gst-registration-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-gst-registration tbody input[type="checkbox"]:checked').each(function () {
                 gstRegistrationDataTable.row($('#tbl-gst-registration tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-gst-registration-dt').data('rowindex');
                  EnableNewOperation('gst-registration');

                  $('#select-all-gst-registration').prop('checked', false);
                    // Add Required Error Message, If Table Has Not Any Record
                    if (!gstRegistrationDataTable.data().any())
                    $('#gst-registration-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-gst-registration').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-gst-registration tbody input[type="checkbox"]').each(function (index) {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                rowData = (gstRegistrationDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-gst-registration-dt').data('rowindex', arr);
                EnableDeleteOperation('gst-registration')
            });
        }
        else {
            EnableNewOperation('gst-registration')

            $('#tbl-gst-registration tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-gst-registration tbody').click('input[type="checkbox"]', function () {
        $('#tbl-gst-registration input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = gstRegistrationDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (gstRegistrationDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('gst-registration');

                    $('#btn-update-gst-registration-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-gst-registration-dt').data('rowindex', rowData);
                    $('#btn-delete-gst-registration-dt').data('rowindex', arr);
                    $('#select-all-gst-registration').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-gst-registration tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('gst-registration');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('gst-registration');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('gst-registration');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-gst-registration').prop('checked', true);
        else
            $('#select-all-gst-registration').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-gst-registration > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (gstRegistrationDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#state-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate Fund Module
    function IsValidGSTRegistrationDataTableModal() {
        debugger

        result = true; // Assuming initial result is true

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        state = $('#state-id option:selected').val();
        stateText = $('#state-id option:selected').text();
        gSTRegistrationType = $('#gst-registration-type-id option:selected').val();
        gSTRegistrationTypeText = $('#gst-registration-type-id option:selected').text();
        applicableFrom = $('#applicable-from').val();
        gSTReturnPeriodicity = $('#gst-return-periodicity-id option:selected').val();
        gSTReturnPeriodicityText = $('#gst-return-periodicity-id option:selected').text();
        isApplicableEWayBill = $('input[name="OrganizationGSTRegistrationDetailViewModel.IsApplicableEWayBill"]').is(':checked') ? "True" : "False";
        thresholdLimit = $('#threshold-limit').val();
        gSTRegistrationNumber = $('#gst-registration-number').val();
        registrationDate = $('#activation-date-registration-date').val();
        closeDate = $('#expiry-date-close-date-registration-detail').val();
        note = $('#note-registration-detail').val();
        reasonForModification = $('#reason-for-modification-registration-detail').val();

        if (gSTRegistrationNumber === '')
            gSTRegistrationNumber = 'None';

        if (note == '')
            note = 'None';

        if (reasonForModification == '' || reasonForModification == undefined)
            reasonForModification = 'None';

        let isValidRegistrationDate = IsValidInputDate('#activation-date-registration-date');
        let isValidCloseDate = IsValidInputDate('#expiry-date-close-date-registration-detail');

        if (!isValidRegistrationDate) {
            result = false;
            $('#activation-date-registration-date-error').removeClass('d-none');
        } else
            $('#activation-date-registration-date-error').addClass('d-none');

        if (!isValidCloseDate) {
            result = false;
            $('#expiry-date-close-date-registration-detail-error').removeClass('d-none');
        } else
            $('#expiry-date-close-date-registration-detail-error').addClass('d-none');


      
        if (state == '') {
            result = false;
            $('#state-id-error').removeClass('d-none');
        } else
            $('#state-id-error').addClass('d-none');

        if (gSTRegistrationType == '') {
            result = false;
            $('#gst-registration-type-id-error').removeClass('d-none');
        } else
            $('#gst-registration-type-id-error').addClass('d-none');

        if (gSTReturnPeriodicity == '') {
            result = false;
            $('#gst-return-periodicity-id-error').removeClass('d-none');
        } else
            $('#gst-return-periodicity-id-error').addClass('d-none');

        let date = new Date().toISOString().slice(0, -14);
        let today = new Date('2017-07-01');

        let DaysAgo = new Date(today.setDate(today.getDate() - 0)).toISOString().split('T')[0];

        $('#applicable-from').attr('min', DaysAgo);

        applicableFrom = $('#applicable-from').val();
        if (applicableFrom === '') {
            result = false;
            $('#applicable-from-error').removeClass('d-none');
        } else {
            $('#applicable-from-error').addClass('d-none');
        }

        if (thresholdLimit == '') {
            result = false;
            $('#threshold-limit-error').removeClass('d-none');
        } else
            $('#threshold-limit-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsGSTRegistrationDataTable() {
        gstRegistrationDataTable.column(1).visible(false);
        gstRegistrationDataTable.column(3).visible(false);
        gstRegistrationDataTable.column(6).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Handling Save/Submit Click Event
    $('#btnsave').click(function () {
        debugger;

        let isValidAllInputs = true;

        //not add event.preventDefault
        if ($('form').valid()) {

            $('.lastrow').remove();

            // Return List Object, Hence Create Array
            let contactDetailArray = new Array();
            let fundArray = new Array();
            let gSTRegistrationDetailArray = new Array();
            let loanTypeArray = new Array();

            // Accordion 1 - Cooperative Registration Validation, If Enable
            if (!IsValidCooperativeRegistrationAccordionInputs())
                isValidAllInputs = false;

            // Accordion 1 - Cooperative Registration Validation, If Enable
            if (!IsValidAuthorizedSharesCapitalAccordionInputs())
                isValidAllInputs = false;

            // Create Array For ContactDetail Data Table To Pass Data
            if (!$('#heading-contact-detail').is(':checked')) {
                if (contactDataTable.data().any()) {
                    if (isValidAllInputs) {
                        $('#tbl-contact tbody tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (contactDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                contactDetailArray.push(
                            {
                                'ContactTypeId': columnValues[1],
                                'FieldValue': columnValues[3],
                                'ContactGroupId': columnValues[4],
                                'IsOpen': columnValues[6],
                            });
                            }
                            else
                                return false;
                        });

                        $('#contact-error').addClass('d-none');
                    }
                }
                else {
                    $('#contact-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For  organization Fund Data Table To Pass Data
            if (!$('#heading-organization-fund').is(':checked')) {
                if (fundDataTable.data().any()) {
                    $('fund-data-table-error').addClass('d-none');
                    if (isValidAllInputs) {
                        $('#tbl-fund tbody tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (fundDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                fundArray.push(
                            {
                                'FundId': columnValues[1],
                                'SequenceNumber': columnValues[3],
                                'SequenceNumberText': columnValues[4],
                                'TransSequenceNumberText': columnValues[5],
                                'ActivationDate': columnValues[6],
                                'ExpiryDate': columnValues[7],
                                'CloseDate': columnValues[8],
                                'Note': columnValues[9],
                                'TransNote': columnValues[10],

                            });
                            }
                            else
                                return false;
                        });

                    }
                }
                else {
                    $('#fund-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For Gst Registration Data Table To Pass Data
            if (!$('#heading-gst-registration').is(':checked')) {
                if (gstRegistrationDataTable.data().any()) {
                    $('gst-registration-error').addClass('d-none');

                    if (isValidAllInputs) {
                        $('#tbl-gst-registration tbody tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (gstRegistrationDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                gSTRegistrationDetailArray.push({
                                    'StateId': columnValues[1],
                                    'GSTRegistrationTypeId': columnValues[3],
                                    'ApplicableFrom': columnValues[5],
                                    'GSTReturnPeriodicityId': columnValues[6],
                                    'IsApplicableEWayBill': columnValues[8],
                                    'ThresholdLimit': columnValues[9],
                                    'GSTRegistrationNumber': columnValues[10],
                                    'RegistrationDate': columnValues[11],
                                    'CloseDate': columnValues[12],
                                    'Note': columnValues[13],
                                    'ReasonForModification': columnValues[14],
                                });
                            }
                            else
                                return false;
                        });

                    }
                }
                else {
                    $('#gst-registration-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }
 
            // Create Array For Loan Type Data Table To Pass Data
            if (!$('#heading-organization-loan-type').is(':checked')) {
                if (loanTypeDataTable.data().any()) {
                    $('loan-type-data-table-error').addClass('d-none');
                    if (isValidAllInputs) {
                        $('#tbl-loan-type tbody tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (loanTypeDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                loanTypeArray.push({
                                    'LoanTypeId': columnValues[1],
                                    'MaximumLoanTenure': columnValues[3],
                                    'MinimumDownPaymentPercentage': columnValues[4],
                                    'SequenceNumber': columnValues[5],
                                    'SequenceNumberText': columnValues[6],
                                    'TransSequenceNumberText': columnValues[7],
                                    'ActivationDate': columnValues[8],
                                    'ExpiryDate': columnValues[9],
                                    'CloseDate': columnValues[10],
                                    'Note': columnValues[11],
                                    'TransNote': columnValues[12],
                                })
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#loan-type-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            if (isValidAllInputs) {

                // Call Cantroller Save Method 
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: { '_organizationContactDetail': contactDetailArray, '_organizationGSTRegistrationDetail': gSTRegistrationDetailArray, '_organizationFund': fundArray, '_organizationLoanType': loanTypeArray },
                    ContentType: "application/json; charset=utf-8",
                    dataType: "JSON",
                    success: function (data) {
                    },
                })

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