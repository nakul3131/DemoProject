'use strict';
$(document).ready(function ()
{
    // @@@@@@@@@@ Data Table Related letible Declaration
    debugger;
    let result = true;
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
    let rowNum = 0;

    //Person Borrowing Detail
    let nameOfOrganization = '';
    let transNameOfOrganization = '';
    let transBranch;
    let matureDate = '';
    let loanDetails = '';
    let transLoanDetails = '';
    let mortgageDetails = '';
    let transMortgageDetails = '';
    let mortgageAmount = 0;
    let sanctionLoanAmount = 0;
    let installmentAmount = 0;
    let loanBalanceAmount = 0;
    let overduesInstallment;
    let overduesAmount = 0;
    let isTakingAnyCourtAction = false;
    let courtCaseType = '';
    let courtCaseTypeText = '';
    let courtCaseStage;
    let courtCaseStageText = '';
    let filingDate = '';
    let filingNumber = 0;
    let cNRNumber = 0;
    let openingDate = '';
    let closeDate = '';
    let registrationDate = '';
    let branch = '';
    let referenceNumber = '';
    let registrationNumber = '';
    let note = '';
    let transNote = '';
    let reasonForModification = '';

    // Create DataTables
    let borrowingDetailDataTable = CreateDataTable('borrowing-detail');


    // Page Loading Default Values (Usually For Amend)
    SetPageLoadingDefaultValues();


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Set Page Loading Default Values @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    function SetPageLoadingDefaultValues()
    {
        debugger;
        EnableTakingAnyCourtActionClickEventFunction();
    }

    // Update max attribute based on Mortgage Amount
    $('#mortgage-amount').focusout(function ()
    {
        $('#sanction-loan-amount').attr('max', $(this).val() );

        // Clear Value Sanction Loan Amount
        $('#sanction-loan-amount').val('');
    });

    // Update max attribute based on Sanction Price
    $('#sanction-loan-amount').focusout(function () {
        debugger;
        let sanctionAmount = parseFloat($('#sanction-loan-amount').val());
        if (isNaN(sanctionAmount) === true) {
            sanctionAmount = 0;
        }
        $('#installment-amount').attr('max', sanctionAmount);
        // Clear Value Of Installment  Amount
        $('#installment-amount').val('');
    });

    // Opening Date
    $('#activation-open-date').focusout(function ()
    {
        $('#activation-filing-date').val('');
        $('#close-date-borrowing').val('');
        EnableTakingAnyCourtActionClickEventFunction();
    });

    // Filling Date
    $('#activation-filing-date').click(function ()
    {
        let openingDate = new Date($('#activation-open-date').val());

        openingDate.setDate(openingDate.getDate() + 1);

        $('#activation-filing-date').attr('min', GetInputDateFormat(openingDate));

        $('#expiry-filing-date').val('');
    });

    // Registration Date
    $('#expiry-filing-date').click(function ()
    {
        debugger;
        let filingDate = new Date($('#activation-filing-date').val());

        $('#expiry-filing-date').attr('min', GetInputDateFormat(filingDate));

        let maxRegistrationDate = new Date(filingDate.setMonth(filingDate.getMonth() + 1));

        if (maxRegistrationDate > new Date())
        {
            maxRegistrationDate = new Date();
        }

        $('#expiry-filing-date').attr('max', GetInputDateFormat(maxRegistrationDate));
    });

    // Function to enable/disable based on close-date-borrowing value
    function EnableTakingAnyCourtActionClickEventFunction()
    {
         let closeDate = $('#close-date-borrowing').val();
         let today = new Date();
         let openDate = $('#activation-open-date').val();
         today = GetInputDateFormat(today);

         if (closeDate !== '' || openDate === today)
         {
            $('#taking-any-court-action-block').addClass('d-none');
            $('#enable-taking-any-court-action').prop('checked', false)
            $('#enable-taking-any-court-action').prop('disabled', true)

        } else {
            $('#enable-taking-any-court-action').prop('disabled', false).removeClass('d-none');
         }
    }

    // Add event listener for click
    $('#close-date-borrowing').focusout(function ()
    {
        EnableTakingAnyCourtActionClickEventFunction();  
    }); 
        
    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Borrowing Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-borrowing-detail-dt').click(function ()
    {
        debugger;
        event.preventDefault();

        // For Court Case
        SetToggleSwitchBasedAccordions();
        EnableTakingAnyCourtActionClickEventFunction();
        SetModalTitle('borrowing-detail', 'Add');

    });

    // DataTable Edit Button 
    $('#btn-edit-borrowing-detail-dt').click(function ()
    {
        debugger;
        SetModalTitle('borrowing-detail', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#borrowing-detail-modal').modal();

            let tmpDate = new Date();

            columnValues = $('#btn-edit-borrowing-detail-dt').data('rowindex');
            id = $('#borrowing-detail-modal').attr('id');

            // Set Opening, Maturity, Close Date, Filling Date
            openingDate = new Date(columnValues[6]);
            matureDate = new Date(columnValues[7]);
            closeDate = new Date(columnValues[8]);

            tmpDate = new Date(columnValues[6]);
            tmpDate.setDate(tmpDate.getDate() + 1);

            $('#expiry-open-date').attr('min', GetInputDateFormat(tmpDate))
            $('#close-date-borrowing').attr('min', GetInputDateFormat(tmpDate))
            $('#activation-filing-date').attr('min', GetInputDateFormat(tmpDate));


            // Set Filling, Registration, Date
            filingDate = new Date(columnValues[22]);
            registrationDate = new Date(columnValues[24]);

            // Set Min Of Registration Date
            tmpDate = new Date(columnValues[22]);
            tmpDate.setDate(tmpDate.getDate());

            $('#expiry-filing-date').attr('min', GetInputDateFormat(tmpDate));

            // Set Max Of Registration Date
            tmpDate = new Date(columnValues[22]);
            tmpDate.setDate(tmpDate.getDate() + 30);

            $('#expiry-filing-date').attr('max', GetInputDateFormat(tmpDate));

            // Set Maximum Attributes Sanction - Mortgage            
            $('#sanction-loan-amount').attr('max', columnValues[13]);

            // Set Maximum Attributes Installment - Sanction
            $('#installment-amount').attr('max', columnValues[14]);

            $('#name-of-organization', myModal).val(columnValues[1]);
            $('#trans-name-of-organization', myModal).val(columnValues[2]);
            $('#branch', myModal).val(columnValues[3]);
            $('#trans-branch', myModal).val(columnValues[4]);
            $('#reference-number', myModal).val(columnValues[5]);
            $('#activation-open-date', myModal).val(GetInputDateFormat(openingDate));
            $('#expiry-open-date', myModal).val(GetInputDateFormat(matureDate));
            $('#close-date-borrowing', myModal).val(GetInputDateFormat(closeDate));
            $('#loan-details', myModal).val(columnValues[9]);
            $('#trans-loan-details', myModal).val(columnValues[10]);
            $('#mortgage-details', myModal).val(columnValues[11]);
            $('#trans-mortgage-details', myModal).val(columnValues[12]);
            $('#mortgage-amount', myModal).val(columnValues[13]);
            $('#sanction-loan-amount', myModal).val(columnValues[14]);
            $('#installment-amount', myModal).val(columnValues[15]);
            $('#loan-balance-amount', myModal).val(columnValues[16]);
            $('#overdues-installment', myModal).val(columnValues[17]);
            $('#overdues-amount', myModal).val(columnValues[18]);

            $('#enable-taking-any-court-action', myModal).prop('checked', columnValues[19].toString().toLowerCase() === 'true' ? true : false);

            $('#court-case-type-id', myModal).val(columnValues[20]);
            $('#activation-filing-date', myModal).val(GetInputDateFormat(filingDate));
            $('#filing-number', myModal).val(columnValues[23]);
            $('#expiry-filing-date', myModal).val(GetInputDateFormat(registrationDate));
            $('#court-case-registration-number', myModal).val(columnValues[25]);
            $('#cnr-number', myModal).val(columnValues[26]);
            $('#court-case-stage-id', myModal).val(columnValues[27]);

            $('#note-borrowing-detail', myModal).val(columnValues[29]);
            $('#trans-note-borrowing-detail', myModal).val(columnValues[30]);
            $('#reason-for-modification-borrowing-detail', myModal).val(columnValues[31]);

            // For Court Case
            SetToggleSwitchBasedAccordions();

            EnableTakingAnyCourtActionClickEventFunction();

            // Show Modals
            $('#borrowing-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-borrowing-detail-edit-dt').addClass('read-only');
            $('#borrowing-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-borrowing-detail-modal').click(function (event) {
        debugger;
        if (IsValidBorrowingModal()) {
            row = borrowingDetailDataTable.row.add([
                tag,
                nameOfOrganization,
                transNameOfOrganization,
                branch,
                transBranch,
                referenceNumber,
                openingDate,
                matureDate,
                closeDate,
                loanDetails,
                transLoanDetails,
                mortgageDetails,
                transMortgageDetails,
                mortgageAmount,
                sanctionLoanAmount,
                installmentAmount,
                loanBalanceAmount,
                overduesInstallment,
                overduesAmount,
                isTakingAnyCourtAction,
                courtCaseType,
                courtCaseTypeText,
                filingDate,
                filingNumber,
                registrationDate,
                registrationNumber,
                cNRNumber,
                courtCaseStage,
                courtCaseStageText,
                note,
                transNote,
                reasonForModification
            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            $('#borrowing-detail-data-table-error').addClass('d-none');

            HideColumnsborrowingDetailDataTable();

            borrowingDetailDataTable.columns.adjust().draw();
 
            $('#borrowing-detail-modal').modal('hide');

            EnableNewOperation('borrowing-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-borrowing-detail-modal').click(function (event) {
        $('#select-all-borrowing-detail').prop('checked', false);
        if (IsValidBorrowingModal()) {
            borrowingDetailDataTable.row(selectedRowIndex).data([
                tag,
                nameOfOrganization,
                transNameOfOrganization,
                branch,
                transBranch,
                referenceNumber,
                openingDate,
                matureDate,
                closeDate,
                loanDetails,
                transLoanDetails,
                mortgageDetails,
                transMortgageDetails,
                mortgageAmount,
                sanctionLoanAmount,
                installmentAmount,
                loanBalanceAmount,
                overduesInstallment,
                overduesAmount,
                isTakingAnyCourtAction,
                courtCaseType,
                courtCaseTypeText,
                filingDate,
                filingNumber,
                registrationDate,
                registrationNumber,
                cNRNumber,
                courtCaseStage,
                courtCaseStageText,
                note,
                transNote,
                reasonForModification
            ]).draw();

            HideColumnsborrowingDetailDataTable();

            borrowingDetailDataTable.columns.adjust().draw();

            $('#borrowing-detail-modal').modal('hide');

            EnableNewOperation('borrowing-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-borrowing-detail-dt').click(function (event)
    {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-borrowing-detail tbody input[type="checkbox"]:checked').each(function () {
                    borrowingDetailDataTable.row($("#tbl-borrowing-detail tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-borrowing-detail-dt').data('rowindex');
                    EnableNewOperation('borrowing-detail');

                    $('#select-all-borrowing-detail').prop('checked', false);
                    //if (!borrowingDetailDataTable.data().any())
                    //    $('#borrowing-detail-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-borrowing-detail').click(function ()
    {
        if ($(this).prop('checked'))
        {
            $('#tbl-borrowing-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = borrowingDetailDataTable.row(row).index();

                rowData = (borrowingDetailDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-borrowing-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('borrowing-detail');
            });
        }
        else
        {
            EnableNewOperation('borrowing-detail');

            $('#tbl-borrowing-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-borrowing-detail tbody').click('input[type=checkbox]', function () {
        $('#tbl-borrowing-detail input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = borrowingDetailDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (borrowingDetailDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('borrowing-detail');

                    $('#btn-update-borrowing-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-borrowing-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-borrowing-detail-dt').data('rowindex', arr);
                    $('#select-all-borrowing-detail').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-borrowing-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('borrowing-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('borrowing-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('borrowing-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-borrowing-detail').prop('checked', true);
        else
            $('#select-all-borrowing-detail').prop('checked', false);
    });

    // Validate Borrowing Module
    function IsValidBorrowingModal() {
        debugger;
        result = true;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        nameOfOrganization = $('#name-of-organization').val();
        transNameOfOrganization = $('#trans-name-of-organization').val();
        branch = $('#branch').val();
        transBranch = $('#trans-branch').val();
        referenceNumber = $('#reference-number').val();
        openingDate = $('#activation-open-date').val();
        matureDate = $('#expiry-open-date').val();
        closeDate = $('#close-date-borrowing').val();
        loanDetails = $('#loan-details').val();
        transLoanDetails = $('#trans-loan-details').val();
        mortgageDetails = $('#mortgage-details').val();
        transMortgageDetails = $('#trans-mortgage-details').val();
        mortgageAmount = parseFloat($('#mortgage-amount').val());
        sanctionLoanAmount = parseFloat($('#sanction-loan-amount').val());
        installmentAmount = parseFloat($('#installment-amount').val());
        loanBalanceAmount = parseFloat($('#loan-balance-amount').val());
        overduesInstallment = parseFloat($('#overdues-installment').val());
        overduesAmount = parseFloat($('#overdues-amount').val());
        isTakingAnyCourtAction = $('#enable-taking-any-court-action').is(':checked');
        note = $('#note-borrowing-detail').val();
        transNote = $('#trans-note-borrowing-detail').val();
        reasonForModification = $('#reason-for-modification-borrowing-detail').val();

        debugger;
        EnableTakingAnyCourtActionClickEventFunction();
        // Is Taking Any Court Action
        if (isTakingAnyCourtAction) {
            courtCaseType = $('#court-case-type-id option:selected').val();
            courtCaseTypeText = $('#court-case-type-id option:selected').text();
            registrationDate = $('#expiry-filing-date').val();
            filingDate = $('#activation-filing-date').val();
            courtCaseStage = $('#court-case-stage-id option:selected').val();
            courtCaseStageText = $('#court-case-stage-id option:selected').text();
            filingNumber = $('#filing-number').val();
            registrationNumber = $('#court-case-registration-number').val();
            cNRNumber = $('#cnr-number').val();
        }
        else {
            courtCaseType = '00000000-0000-0000-0000-000000000000';
            courtCaseTypeText = 'None';
            courtCaseStage = '00000000-0000-0000-0000-000000000000';
            courtCaseStageText = 'None';
            registrationDate = '1900/01/01';
            filingDate = '1900/01/01';
            cNRNumber = 'None';
            filingNumber = 'None';
            registrationNumber = 'None';
        }

        //Set Default Value if Empty
        if (note === '') {
            $('#note-borrowing-detail').val('None');
            note = 'None';
        }

        // TransNote
        if (transNote === '') {
            $('#trans-note-borrowing-detail').val('None');
            transNote = 'None';
        }

        // Reason For Modification
        if (reasonForModification === '') {
            $('#reason-for-modification-borrowing-detail').val('None');
            reasonForModification = 'None';
        }


        // NameOfOrganization
        minimumLength = parseInt($('#name-of-organization').attr('minlength'));
        maximumLength = parseInt($('#name-of-organization').attr('maxlength'));

        if (parseInt(nameOfOrganization.length) < parseInt(minimumLength) || parseInt(nameOfOrganization.length) > parseInt(maximumLength)) {
            result = false;
            $('#name-of-organization-error').removeClass('d-none');
        }

        // TransNameOfOrganization
        maximumLength = parseInt($('#trans-name-of-organization').attr('maxlength'));

        if (parseInt(transNameOfOrganization.length) === 0 || parseInt(transNameOfOrganization.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-name-of-organization-error').removeClass('d-none');
        }

        // Branch
        minimumLength = parseInt($('#branch').attr('minlength'));
        maximumLength = parseInt($('#branch').attr('maxlength'));

        if (parseInt(branch.length) < parseInt(minimumLength) || parseInt(branch.length) > parseInt(maximumLength)) {
            result = false;
            $('#branch-error').removeClass('d-none');
        }

        // TransBranch
        maximumLength = parseInt($('#trans-branch').attr('maxlength'));

        if (parseInt(transBranch.length) === 0 || parseInt(transBranch.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-branch-error').removeClass('d-none');
        }

        // ReferenceNumber
        minimumLength = parseInt($('#reference-number').attr('minlength'));
        maximumLength = parseInt($('#reference-number').attr('maxlength'));

        if (parseInt(referenceNumber.length) < parseInt(minimumLength) || parseInt(referenceNumber.length) > parseInt(maximumLength)) {
            result = false;
            $('#reference-number-error').removeClass('d-none');
        }

        // Opening Date
        if (IsValidInputDate('#activation-open-date') === false) {
            result = false;
            $('#activation-open-date-error').removeClass('d-none');
        }

        if (IsValidInputDate('#expiry-open-date') === false) {
            result = false;
            $('#expiry-open-date-error').removeClass('d-none');
        }

        if (closeDate === '') {
            // Close Date can be empty, clear error
            $('#close-date-borrowing-error').addClass('d-none');
        } else if (closeDate < openingDate) {
            result = false;
            $('#close-date-borrowing-error').removeClass('d-none');
        } 

        // Loan Details
        minimumLength = parseInt($('#loan-details').attr('minlength'));
        maximumLength = parseInt($('#loan-details').attr('maxlength'));

        if (parseInt(loanDetails.length) < parseInt(minimumLength) || parseInt(loanDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#loan-details-error').removeClass('d-none');
        }

        // Translation Of LoanDetails
        maximumLength = parseInt($('#trans-loan-details').attr('maxlength'));

        if (parseInt(transLoanDetails.length) === 0 || parseInt(transLoanDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-loan-details-error').removeClass('d-none');
        }

        // Mortgage Details
        minimumLength = parseInt($('#mortgage-details').attr('minlength'));
        maximumLength = parseInt($('#mortgage-details').attr('maxlength'));

        if (parseInt(mortgageDetails.length) < parseInt(minimumLength) || parseInt(mortgageDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#mortgage-details-error').removeClass('d-none');
        }

        // Translation Of Mortgage Details
        maximumLength = parseInt($('#trans-mortgage-details').attr('maxlength'));

        if (parseInt(transMortgageDetails.length) === 0 || parseInt(transMortgageDetails.length) > parseInt(maximumLength)) {
            result = false;
            $('#trans-mortgage-details-error').removeClass('d-none');
        }

        // Mortgage Amount
        if (isNaN(mortgageAmount) === false) {
            minimum = parseFloat($('#mortgage-amount').attr('min'));
            maximum = parseFloat($('#mortgage-amount').attr('max'));

            if (parseFloat(mortgageAmount) < parseFloat(minimum) || parseFloat(mortgageAmount) > parseFloat(maximum)) {
                result = false;
                $('#mortgage-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#mortgage-amount-error').removeClass('d-none');
        }

        // Sanction Loan Amount
        if (isNaN(sanctionLoanAmount) === false) {
            minimum = parseFloat($('#sanction-loan-amount').attr('min'));
            maximum = parseFloat($('#sanction-loan-amount').attr('max'));

            if (parseFloat(sanctionLoanAmount) < parseFloat(minimum) || parseFloat(sanctionLoanAmount) > parseFloat(maximum)) {
                result = false;
                $('#sanction-loan-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#sanction-loan-amount-error').removeClass('d-none');
        }

        // Installment Amount
        if (isNaN(installmentAmount) === false) {
            minimum = parseFloat($('#installment-amount').attr('min'));
            maximum = parseFloat($('#installment-amount').attr('max'));

            if (parseFloat(installmentAmount) < parseFloat(minimum) || parseFloat(installmentAmount) > parseFloat(maximum)) {
                result = false;
                $('#installment-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#installment-amount-error').removeClass('d-none');
        }

        // Loan Balance Amount
        if (isNaN(loanBalanceAmount) === false) {
            minimum = parseFloat($('#loan-balance-amount').attr('min'));
            maximum = parseFloat($('#loan-balance-amount').attr('max'));

            if (parseFloat(loanBalanceAmount) < parseFloat(minimum) || parseFloat(loanBalanceAmount) > parseFloat(maximum)) {
                result = false;
                $('#loan-balance-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#loan-balance-amount-error').removeClass('d-none');
        }

        // Overdues Installment
        if (isNaN(overduesInstallment) === false) {
            minimum = parseFloat($('#overdues-installment').attr('min'));
            maximum = parseFloat($('#overdues-installment').attr('max'));

            if (parseFloat(overduesInstallment) < parseFloat(minimum) || parseFloat(overduesInstallment) > parseFloat(maximum)) {
                result = false;
                $('#overdues-installment-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#overdues-installment-error').removeClass('d-none');
        }

        //overdues Amount
        if (isNaN(overduesAmount) === false) {
            minimum = parseFloat($('#overdues-amount').attr('min'));
            maximum = parseFloat($('#overdues-amount').attr('max'));

            if (parseFloat(overduesAmount) < parseFloat(minimum) || parseFloat(overduesAmount) > parseFloat(maximum)) {
                result = false;
                $('#overdues-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#overdues-amount-error').removeClass('d-none');
        }

        // If taking court action, perform validation checks
        if (isTakingAnyCourtAction) {
            // CourtCaseType
            if ($('#court-case-type-id').prop('selectedIndex') < 1) {
                result = false;
                $('#court-case-type-id-error').removeClass('d-none');
            }

            // FilingDate
            if (IsValidInputDate('#activation-filing-date') === false) {
                result = false;
                $('#activation-filing-date-error').removeClass('d-none');
            }

            // RegistrationDate
            if (IsValidInputDate('#expiry-filing-date') === false) {
                result = false;
                $('#expiry-filing-date-error').removeClass('d-none');
            }
           
            // CourtCaseStage
            if ($('#court-case-stage-id').prop('selectedIndex') < 1) {
                result = false;
                $('#court-case-stage-id-error').removeClass('d-none');
            }

            // FilingNumber
            minimumLength = parseInt($('#filing-number').attr('minlength'));
            maximumLength = parseInt($('#filing-number').attr('maxlength'));

            if (parseInt(filingNumber.length) < parseInt(minimumLength) || parseInt(filingNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#filing-number-error').removeClass('d-none');
            }

            // RegistrationNumber
            minimumLength = parseInt($('#court-case-registration-number').attr('minlength'));
            maximumLength = parseInt($('#court-case-registration-number').attr('maxlength'));

            if (parseInt(registrationNumber.length) < parseInt(minimumLength) || parseInt(registrationNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#court-case-registration-number-error').removeClass('d-none');
            }

            // CNRNumber
            minimumLength = parseInt($('#cnr-number').attr('minlength'));
            maximumLength = parseInt($('#cnr-number').attr('maxlength'));

            if (parseInt(cNRNumber.length) < parseInt(minimumLength) || parseInt(cNRNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#cnr-number-error').removeClass('d-none');
            }
        }
        else {
            // If not taking any court action, set default values
            courtCaseType = '00000000-0000-0000-0000-000000000000';
            courtCaseTypeText = 'None';
            courtCaseStage = '00000000-0000-0000-0000-000000000000';
            courtCaseStageText = 'None';
            registrationDate = '1900/01/01';
            filingDate = '1900/01/01';
            cNRNumber = 'None';
            filingNumber = 'None';
            registrationNumber = 'None';
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsborrowingDetailDataTable() {
         borrowingDetailDataTable.column(20).visible(false);
        borrowingDetailDataTable.column(27).visible(false);
    }

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;

        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let personBorrowingDetailArray = new Array();

            // Create Array For person borrowing detail Table To Pass Data
            debugger
            if (!$('#heading-borrowing-detail').hasClass('d-none')) {
                if (borrowingDetailDataTable.data().any()) {
                    debugger;
                    $('#borrowing-detail-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-borrowing-detail > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (borrowingDetailDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                debugger;
                                personBorrowingDetailArray.push(
                                {
                                    'NameOfOrganization': columnValues[1],
                                    'TransNameOfOrganization': columnValues[2],
                                    'Branch': columnValues[3],
                                    'TransBranch': columnValues[4],
                                    'ReferenceNumber': columnValues[5],
                                    'OpeningDate': columnValues[6],
                                    'MatureDate': columnValues[7],
                                    'CloseDate': columnValues[8],
                                    'LoanDetails': columnValues[9],
                                    'TransLoanDetails': columnValues[10],
                                    'MortgageDetails': columnValues[11],
                                    'TransMortgageDetails': columnValues[12],
                                    'MortgageAmount': columnValues[13],
                                    'SanctionLoanAmount': columnValues[14],
                                    'InstallmentAmount': columnValues[15],
                                    'LoanBalanceAmount': columnValues[16],
                                    'OverduesInstallment': columnValues[17],
                                    'OverduesAmount': columnValues[18],
                                    'IsTakingAnyCourtAction': columnValues[19],
                                    'CourtCaseTypeId': columnValues[20],
                                    'FilingDate': columnValues[22],
                                    'FilingNumber': columnValues[23],
                                    'RegistrationDate': columnValues[24],
                                    'RegistrationNumber': columnValues[25],
                                    'CNRNumber': columnValues[26],
                                    'CourtCaseStageId': columnValues[27],
                                    'Note': columnValues[29],
                                    'TransNote': columnValues[30],
                                    'ReasonForModification': columnValues[31]
                                });
                            }
                            else
                                return false;
                        });
                    }

                }
                else {
                    //$('#borrowing-detail-data-table-error').removeClass('d-none');
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
                    data: {
                        '_borrowingDetail': personBorrowingDetailArray
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
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
