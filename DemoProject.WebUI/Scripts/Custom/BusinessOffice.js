'use strict'
// Document Ready Function
$(document).ready(function () {

    const APPLICATION_GENERAL_LEDGER_DROPDOWN_LIST = $('#application-general-ledger-id').html();
    const ACCOUNT_GENERAL_LEDGER_DROPDOWN_LIST = $('#account-general-ledger-id').html();
    const AGREEMENT_GENERAL_LEDGER_DROPDOWN_LIST = $('#agreement-general-ledger-id').html();
    const CERTIFICATE_GENERAL_LEDGER_DROPDOWN_LIST = $('#certificate-general-ledger-id').html();
    const PASSBOOK_GENERAL_LEDGER_DROPDOWN_LIST = $('#passbook-general-ledger-id').html();

    // DECLARATION - OF PAGE GLOBAL VARIABLE
    let note;
    let today;
    let month;
    let date;
    let year;
    let closeDate;
    let datepart;
    let divErrorCount = 0;
    let activationDate;
    let selectedRow;
    let expiryDate;
    let enableMemberNumber;
    let enableAccountNumber;
    let editedAddressTypeId = '';
    let editedApplicationGeneralLedgerId = '';
    let editedAccountGeneralLedgerId = '';
    let editedCertificateGeneralLedgerId = '';

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

    let minimum;
    let maximum;
    let result = true;

    //Password Policy
    let passwordPolicyIdText;
    let reasonForModification;
    let passwordPolicyId;

    //Menu
    let menuId;
    let menuIdText;

    //Special Permission
    let specialPermissionId;
    let specialPermissionIdText;

    //Transaction Limit
    let generalLedgerId;
    let generalLedgerIdText;
    let transactionTypeId;
    let transactionTypeText;
    let currencyId;
    let currencyIdText;
    let minimumAmountLimitForTransaction;
    let maximumAmountLimitForTransaction;
    let maximumNumberOfBackDaysForTransaction;
    let minimumAmountLimitForVerification;
    let maximumAmountLimitForVerification;
    let maximumNumberOfBackDaysForVerification;
    let minimumAmountLimitForAutoVerification;
    let maximumAmountLimitForAutoVerification;
    let maximumNumberOfBackDaysForAutoVerification;
    let enableCashDenomination;

    //Account Number
    let accountGeneralLedgerId;
    let accountGeneralLedgerIdText;
    let startAccountNumber = 0;
    let endAccountNumber = 0;
    let enableAutoAccountNumber = 0;
    let enableReGenerateUnusedAccountNumber;
    let enableRandomAccountNumber;
    let enableCustomizeAccountNumber;
    let enableDigitalCodeForAccountNumber;
    let accountNumberMask;
    let accountNumberMaskText;
    let accountNumberIncrementBy

    //Application Number
    let applicationGeneralLedgerId;
    let applicationGeneralLedgerIdText;
    let startApplicationNumber = 0;
    let endApplicationNumber = 0;
    let enableAutoApplicationNumber = 0;
    let enableReGenerateUnusedApplicationNumber;
    let enableRandomApplicationNumber;
    let enableCustomizeApplicationNumber;
    let enableDigitalCodeForApplicationNumber;
    let applicationNumberMask;
    let applicationNumberMaskText;
    let applicationNumberIncrementBy;

    //Certificate Number
    let certificateGeneralLedgerId;
    let certificateGeneralLedgerIdText;
    let startCertificateNumber = 0;
    let endCertificateNumber = 0;
    let enableAutoCertificateNumber = 0;
    let enableReGenerateUnusedCertificateNumber;
    let enableRandomCertificateNumber;
    let enableCustomizeCertificateNumber;
    let enableDigitalCodeForCertificateNumber;
    let certificateNumberMask;
    let certificateNumberMaskText;
    let certificateNumberIncrementBy;

    //Passbook Number
    let editedPassbookGeneralLedgerId;
    let passbookGeneralLedgerId;
    let passbookGeneralLedgerIdText;
    let startPassbookNumber = 0;
    let endPassbookNumber = 0;
    let enableAutoPassbookNumber = 0;
    let enableReGenerateUnusedPassbookNumber;
    let enableRandomPassbookNumber;
    let enableCustomizePassbookNumber;
    let enableDigitalCodeForPassbookNumber;
    let passbookNumberMask;
    let passbookNumberMaskText;
    let passbookNumberIncrementBy;


    // Create DataTables
    let passwordPolicyDataTable = CreateDataTable('password');
    let menuDataTable = CreateDataTable('menu');
    let specialPermissionDataTable = CreateDataTable('special-permission');
    let transactionLimitDataTable = CreateDataTable('transaction-limit');
    let accountNumberDataTable = CreateDataTable('account-number');
    let certificateNumberDataTable = CreateDataTable('certificate-number');
    let passbookNumberDataTable = CreateDataTable('passbook-number');
    let applicationNumberDataTable = CreateDataTable('application-number');
    let currencyDataTable = CreateDataTable('currency');

    // Load Default Values Of Pages (On Amend, Modify, Verify Operation)
    SetPageLoadingDefaultValues();


    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Attach Change Event Handler To Both Elements
    $('#enable-auto-shares-certificate-number').change(function () {
        IsValidSharesCertificateNumberAccordionInputs();
    });

    // Passbook Detail Accordion Input Validation
    $('.shares-certificate-number-input').focusout(function () {
        IsValidSharesCertificateNumberAccordionInputs();
    });

    // Attach Change Event Handler To Both Elements
    $('#enable-auto-person-information-number').change(function () {
        debugger;
        IsValidPersonInformationNumberAccordionInputs();
    });

    // Passbook Detail Accordion Input Validation
    $('.person-information-number-input').focusout(function () {
        IsValidPersonInformationNumberAccordionInputs();
    });

    // Business Office Detail Accordion Input Validation
    $('.business-office-input').focusout(function () {
        IsValidBusinessOfficeDetailAccordionInputs();
    });

    // Trigger Validation When The Business Office Detail Section Is Opened
    $('#collapse-business-office-detail').on('shown.bs.collapse', () => {
        IsValidBusinessOfficeDetailAccordionInputs();
    });

    // Cooperative Registration Accordion Input Validation
    $('.cooperative-registration-input').focusout(function () {
        IsValidCooperativeRegistrationAccordionInputs();
    });

    // Trigger Validation When The Cooperative Registration Section Is Opened
    $('#collapse-cooperative-registration').on('shown.bs.collapse', () => {
        IsValidCooperativeRegistrationAccordionInputs();
    });

    // Attach Change Event Handler To Both Elements
    $('#enable-auto-member-number').change(function () {
        debugger;
        IsValidMemberNumberAccordionInputs();
    });

    // Member Number Accordion Input Validation
    $('.member-number-input').focusout(function () {
        IsValidMemberNumberAccordionInputs();
    });

    // Attach Change Event Handler To Both Elements
    $('#enable-auto-transaction-number').change(function () {
        debugger;
        IsValidTransactionParameterAccordionInputs();
    });

    // Trigger Validation When The Passbook Number Section Is Opened
    $('#collapse-transaction-parameter').on('shown.bs.collapse', () => {
        IsValidTransactionParameterAccordionInputs();
    });

    // Passbook Detail Accordion Input Validation
    $('.transaction-number-input').focusout(function () {
        IsValidTransactionParameterAccordionInputs();
    });

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   FUNCTIONS FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // 1.Business Office Detail Accordion Input Validation
    function IsValidBusinessOfficeDetailAccordionInputs() {

        let commandAreaRadius = parseInt($('#command-area-radius').val());
        let populationOfTheCommandArea = parseInt($('#population-of-the-command-area').val());
        let generalLedgerGroupCode = $('#general-ledger-group-code').val();

        result = true;

        // Check If Values Is Not Empty
        if ($('#center-id').val() === '')
            result = false;

        // Check If Values Is Not Empty
        if ($('#local-currency-id').val() === '')
            result = false;

        // Check If Values Is Not Empty
        if ($('#office-schedule-dd').val() === '')
            result = false;

        // Check If Values Is Not Empty
        if ($('#business-office-type-id').val() === '')
            result = false;

        // Check If Values Is Not Empty
        if ($('#business-nature-id').val() === '')
            result = false;

        // Check If Values Is Not Empty
        if ($('#regional-language-id').val() === '')
            result = false;

        // Command Area Radius
        if (isNaN(commandAreaRadius) === false) {
            minimum = parseInt($('#command-area-radius').attr('min'));
            maximum = parseInt($('#command-area-radius').attr('max'));

            if (parseInt(commandAreaRadius) < parseInt(minimum) || parseInt(commandAreaRadius) > parseInt(maximum))
                result = false;
        }
        else
            result = false;

        // Population Of The Command Area
        if (isNaN(populationOfTheCommandArea) === false) {
            minimum = parseInt($('#population-of-the-command-area').attr('min'));
            maximum = parseInt($('#population-of-the-command-area').attr('max'));

            if (parseInt(populationOfTheCommandArea) < parseInt(minimum) || parseInt(populationOfTheCommandArea) > parseInt(maximum))
                result = false;
        }
        else
            result = false;


        //Coop Registration Number
        if (isNaN(generalLedgerGroupCode.length) === false && generalLedgerGroupCode.length > 0) {
            minimum = $('#general-ledger-group-code').attr('min');
            maximum = $('#general-ledger-group-code').attr('max');

            if (generalLedgerGroupCode.length < minimum || generalLedgerGroupCode.length > maximum)
                result = false;
        }
        else
            result = false;

        if (result)
            $('#business-office-detail-accordion-error').addClass('d-none');
        else
            $('#business-office-detail-accordion-error').removeClass('d-none');

        return result;
    }

    // 2.Cooperative Registration Accordion Input Validation
    function IsValidCooperativeRegistrationAccordionInputs() {
        result = true;
        debugger;
        // Fetching values from input fields
        let coopRegistrationNumber = $('#coop-registration-number').val();
        let transCoopRegistrationNumber = $('#trans-coop-registration-number').val();

        // Check If Values Is Not Empty
        if ($('#coop-approval-date').val() === '')
            result = false;

        // Check If Values Is Not Empty
        if ($('#coop-approval-date').val() === '')
            result = false;


        // Check If Values Is Not Empty
        if ($('#coop-registration-date').val() === '')
            result = false;

        //Coop Registration Number
        if (isNaN(coopRegistrationNumber.length) === false && coopRegistrationNumber.length > 0) {
            minimum = $('#coop-registration-number').attr('min');
            maximum = $('#coop-registration-number').attr('max');

            if (coopRegistrationNumber.length < minimum || coopRegistrationNumber.length > maximum)
                result = false;
        }
        else
            result = false;

        // Trans Coop Numeric Code
        if (isNaN(transCoopRegistrationNumber.length) === false && transCoopRegistrationNumber.length > 0) {
            minimum = parseInt($('#trans-coop-registration-number').attr('min'));
            maximum = parseInt($('#trans-coop-registration-number').attr('max'));

            if (parseInt(transCoopRegistrationNumber.length) < parseInt(minimum) || parseInt(transCoopRegistrationNumber.length) > parseInt(maximum))
                result = false;
        }
        else
            result = false;

        // Display error message if registration is not valid
        if (result)
            $('#cooperative-registration-accordion-error').addClass('d-none');
        else
            $('#cooperative-registration-accordion-error').removeClass('d-none');

        return result;
    }

    // 3.Person Information Number Accordion Input Validation
    function IsValidPersonInformationNumberAccordionInputs() {
        result = true;
        let multiSelectCount = 0;

        if ($('#enable-auto-person-information-number').is(':checked')) {
            let startPersonInformationNumber = parseInt($('#start-person-information-number').val());
            let endPersonInformationNumber = parseInt($('#end-person-information-number').val());
            let personInformationNumberIncrementBy = parseInt($('#person-information-number-increment-by').val());

            multiSelectCount = parseInt($('#person-information-number-mask option:selected').length);

            // Check For Not A Number (NaN)
            // Person Information Mask Number
            if (isNaN(multiSelectCount) === false) {
                if (parseInt(multiSelectCount) === 0)
                    result = false;
            }
            else
                result = false;

            // Start Person Information Number
            if (isNaN(startPersonInformationNumber) === false) {
                minimum = parseInt($('#start-person-information-number').attr('min'));
                maximum = parseInt($('#start-person-information-number').attr('max'));

                if (parseInt(startPersonInformationNumber) < parseInt(minimum) || parseInt(startPersonInformationNumber) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // End Person Information Number
            if (isNaN(endPersonInformationNumber) === false) {
                minimum = parseInt($('#end-person-information-number').attr('min'));
                maximum = parseInt($('#end-person-information-number').attr('max'));

                if (parseInt(endPersonInformationNumber) < (parseInt(startPersonInformationNumber) + 100) || parseInt(endPersonInformationNumber) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Person Information Number Increment By
            if (isNaN(personInformationNumberIncrementBy) === false) {
                minimum = parseInt($('#person-information-number-increment-by').attr('min'));
                maximum = parseInt($('#person-information-number-increment-by').attr('max'));

                if (parseInt(personInformationNumberIncrementBy) < parseInt(minimum) || parseInt(personInformationNumberIncrementBy) > parseInt((parseInt(endPersonInformationNumber) - parseInt(startPersonInformationNumber)) / 100))
                    result = false;
            }
            else
                result = false;

        }


        if (result)
            $('#person-information-number-accordion-error').addClass('d-none');
        else
            $('#person-information-number-accordion-error').removeClass('d-none');

        return result;
    }

    // 4.Member Number Accordion Input Validation
    function IsValidMemberNumberAccordionInputs() {
        debugger;
        let multiSelectCount = 0;

        let result = true;

        if ($('#enable-auto-member-number').is(':checked')) {
            debugger;
            let startMemberNumber = parseInt($('#start-member-number').val());
            let endMemberNumber = parseInt($('#end-member-number').val());
            let memberNumberIncrementBy = parseInt($('#member-number-increment-by').val());

            multiSelectCount = parseInt($('#member-number-mask option:selected').length);

            // Member Number Mask
            if (multiSelectCount === 0)
                result = false;

            // Start Member Number
            if (!isNaN(startMemberNumber)) {
                let minimum = parseInt($('#start-member-number').attr('min'));
                let maximum = parseInt($('#start-member-number').attr('max'));

                if (startMemberNumber < minimum || startMemberNumber > maximum)
                    result = false;
            } else {
                result = false;
            }

            // End Member Number
            if (!isNaN(endMemberNumber)) {
                let minimum = parseInt($('#end-member-number').attr('min'));
                let maximum = parseInt($('#end-member-number').attr('max'));

                if (endMemberNumber < (startMemberNumber + 100) || endMemberNumber > maximum)
                    result = false;
            } else {
                result = false;
            }

            // Member Number Increment By
            if (!isNaN(memberNumberIncrementBy)) {
                if (memberNumberIncrementBy === 0 || memberNumberIncrementBy > ((endMemberNumber - startMemberNumber) / 100))
                    result = false;
            } else {
                result = false;
            }
        }

        if (result)
            $('#member-number-accordion-error').addClass('d-none');
        else
            $('#member-number-accordion-error').removeClass('d-none');

        return result;
    }

    // 5.Shares Certificate Number Accordion Input Validation
    function IsValidSharesCertificateNumberAccordionInputs() {
        debugger;
        let multiSelectCount = 0;
        let result = true;

        if ($('#enable-auto-shares-certificate-number').is(':checked')) {
            debugger;
            let startSharesCertificateNumber = parseInt($('#start-shares-certificate-number').val());
            let endSharesCertificateNumber = parseInt($('#end-shares-certificate-number').val());
            let sharesCertificateNumberIncrementBy = parseInt($('#shares-certificate-number-increment-by').val());

            multiSelectCount = parseInt($('#shares-certificate-number-mask option:selected').length);

            // Shares Certificate Number Mask
            if (multiSelectCount === 0)
                result = false;

            // Start Shares Certificate Number
            if (isNaN(startSharesCertificateNumber) === false) {
                let minimum = parseInt($('#start-shares-certificate-number').attr('min'));
                let maximum = parseInt($('#start-shares-certificate-number').attr('max'));

                if (startSharesCertificateNumber < minimum || startSharesCertificateNumber > maximum)
                    result = false;
            } else {
                result = false;
            }

            // End Shares Certificate Number
            if (isNaN(endSharesCertificateNumber) === false) {
                let minimum = parseInt($('#end-shares-certificate-number').attr('min'));
                let maximum = parseInt($('#end-shares-certificate-number').attr('max'));

                if (endSharesCertificateNumber < (startSharesCertificateNumber + 100) || endSharesCertificateNumber > maximum)
                    result = false;
            } else {
                result = false;
            }

            // Shares Certificate Number Increment By
            if (isNaN(sharesCertificateNumberIncrementBy) === false) {
                if (sharesCertificateNumberIncrementBy === 0 || sharesCertificateNumberIncrementBy > ((endSharesCertificateNumber - startSharesCertificateNumber) / 100))
                    result = false;
            } else {
                result = false;
            }
        }

        if (result)
            $('#shares-certificate-number-accordion-error').addClass('d-none');
        else
            $('#shares-certificate-number-accordion-error').removeClass('d-none');

        return result;
    }

    // 6.Transaction Detail Accordion Input Validation
    function IsValidTransactionParameterAccordionInputs() {
        debugger;
        result = true;

        // Check If Values Is Not Empty
        if ($('#effective-date').val() === '')
            result = false;

        let multiSelectCount = 0;

        if ($('#enable-auto-transaction-number').is(':checked')) {
            let startTransactionNumber = parseInt($('#start-transaction-number').val());
            let endTransactionNumber = parseInt($('#end-transaction-number').val());
            let transactionNumberIncrementBy = parseInt($('#transaction-number-increment-by').val());

            multiSelectCount = parseInt($('#transaction-number-mask option:selected').length);

            // Check For Not A Number (Nan)
            // Transaction Mask Number
            if (!isNaN(multiSelectCount)) {
                if (parseInt(multiSelectCount) === 0)
                    result = false;
            } else {
                result = false;
            }

            // Start Transaction Number
            if (!isNaN(startTransactionNumber)) {
                minimum = parseInt($('#start-transaction-number').attr('min'));
                maximum = parseInt($('#start-transaction-number').attr('max'));

                if (parseInt(startTransactionNumber) < parseInt(minimum) || parseInt(startTransactionNumber) > parseInt(maximum))
                    result = false;
            } else {
                result = false;
            }

            // End Transaction Number
            if (!isNaN(endTransactionNumber)) {
                minimum = parseInt($('#end-transaction-number').attr('min'));
                maximum = parseInt($('#end-transaction-number').attr('max'));

                if (parseInt(endTransactionNumber) < (parseInt(startTransactionNumber) + 100) || parseInt(endTransactionNumber) > parseInt(maximum))
                    result = false;
            } else {
                result = false;
            }

            // Transaction Number Increment By
            if (!isNaN(transactionNumberIncrementBy)) {
                minimum = parseInt($('#transaction-number-increment-by').attr('min'));
                maximum = parseInt($('#transaction-number-increment-by').attr('max'));

                if (parseInt(transactionNumberIncrementBy) < parseInt(minimum) || parseInt(transactionNumberIncrementBy) > parseInt((parseInt(endTransactionNumber) - parseInt(startTransactionNumber)) / 100))
                    result = false;
            } else {
                result = false;
            }


            // Check if the Transaction Number Reset radio button is selected
            if ($('.transaction-number-reset:checked').next('label').text() === '')
                result = false;

            // Check If Values Is Not Empty
            if ($('#checksum-algorithm-id').val() === '')
                result = false;
        }

        if (result)
            $('#transaction-parameter-accordion-error').addClass('d-none');
        else
            $('#transaction-parameter-accordion-error').removeClass('d-none');

        return result;
    }


    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@  Password Policy - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-password-dt').click(function () {
        event.preventDefault();
        SetModalTitle('password', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-password-dt').click(function () {
        SetModalTitle('password', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-password-dt').data('rowindex');
            id = $('#password-modal').attr('id');
            myModal = $('#' + id).modal();

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
                let arr = datepart.split('-');

                let dd = arr[0];
                let mm = arr[1];
                let yyyy = arr[2];

                activationDate = [yyyy, mm, dd].join('-');
            }
            else
                activationDate = [year, month, date].join('-');


            // Get Only Expiry Date
            datepart = columnValues[4].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[4]
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
            datepart = columnValues[5].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[5]
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

                closeDate = [yyyy, mm, dd].join('-');
            }
            else
                closeDate = [year2, month2, date2].join('-');


            // Display Value In Modal Inputs
            $('#password-policy-id', myModal).val(columnValues[1]);
            $('#activation-date-password-policy', myModal).val(activationDate);
            $('#expiry-date-password-policy', myModal).val(expiryDate);
            $('#close-date-password-policy', myModal).val(closeDate);
            $('#note-password-policy', myModal).val(columnValues[6]);


            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-password-dt').addClass('read-only');
            $('#password-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-password-modal').click(function (event) {
        if (IsValidPasswordPolicyDataTableModal()) {
            row = passwordPolicyDataTable.row.add([
                            tag,
                            passwordPolicyId,
                            passwordPolicyIdText,
                            activationDate,
                            expiryDate,
                            closeDate,
                            note,
            ]).draw();

            // Error Message In Span
            $('#password-data-table-error').addClass('d-none');

            HidePasswordPolicyDataTableColumns();

            passwordPolicyDataTable.columns.adjust().draw();

            ClearModal('password');

            $('#password-modal').modal('hide');

            EnableNewOperation('password');
        }
    });

    // Modal update Button Event
    $('#btn-update-password-modal').click(function (event) {

        $('#select-all-password').prop('checked', false);
        if (IsValidPasswordPolicyDataTableModal()) {
            passwordPolicyDataTable.row(selectedRowIndex).data([
                            tag,
                            passwordPolicyId,
                            passwordPolicyIdText,
                            activationDate,
                            expiryDate,
                            closeDate,
                            note,
            ]).draw();
            // Error Message In Span
            $('#password-validation span').html('');

            HidePasswordPolicyDataTableColumns();

            passwordPolicyDataTable.columns.adjust().draw();

            $('#password-modal').modal('hide');

            EnableNewOperation('password');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-password-dt').click(function (event) {
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($("#tbl-password tbody input[type='checkbox']:checked").each(function () {
                    passwordPolicyDataTable.row($("#tbl-password tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-password-dt').data('rowindex');
                    EnableNewOperation('password');

                    $('#select-all-password').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (!passwordPolicyDataTable.data().any())
                        $('#password-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#select-all-password').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-password tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = passwordPolicyDataTable.row(row).index();

                rowData = (passwordPolicyDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-password-dt').data('rowindex', arr);
                EnableDeleteOperation('password')
            });
        }
        else {
            EnableNewOperation('password')

            $('#tbl-password tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-password tbody').click("input[type=checkbox]", function () {
        $('#tbl-password input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = passwordPolicyDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (passwordPolicyDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('password');

                    $('#btn-update-password-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-password-dt').data('rowindex', rowData);
                    $('#btn-delete-password-dt').data('rowindex', arr);
                    $('#select-all-password').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-password tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('password');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('password');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('password');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-password').prop('checked', true);
        else
            $('#select-all-password').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-password > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (passwordPolicyDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#password-policy-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate  Fund Module
    function IsValidPasswordPolicyDataTableModal() {
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        passwordPolicyId = $('#password-policy-id option:selected').val();
        passwordPolicyIdText = $('#password-policy-id option:selected').text();
        activationDate = $('#activation-date-password-policy').val();
        expiryDate = $('#expiry-date-password-policy').val();
        closeDate = $('#close-date-password-policy').val();
        note = $('#note-password-policy').val();

        if (note == '')
            note = 'None';

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-password-policy');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-password-policy');

        if (passwordPolicyId == '') {
            result = false;
            $('#password-policy-id-error').removeClass('d-none');
        } else
            $('#password-policy-id-error').addClass('d-none');

        if (!isValidActivationDate) {
            result = false;
            $('#activation-date-password-policy-error').removeClass('d-none');
        } else
            $('#activation-date-password-policy-error').addClass('d-none');

        if (!isValidExpiryDate) {
            result = false;
            $('#expiry-date-password-policy-error').removeClass('d-none');
        } else
            $('#expiry-date-password-policy-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HidePasswordPolicyDataTableColumns() {
        passwordPolicyDataTable.column(1).visible(false);
        passwordPolicyDataTable.column(5).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Menu - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-menu-dt').click(function () {
        event.preventDefault();
        SetModalTitle('menu', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-menu-dt').click(function () {
        SetModalTitle('menu', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-menu-dt').data('rowindex');
            id = $('#menu-modal').attr('id');
            myModal = $('#' + id).modal();

            // Get Only Activation Date
            let datepart = columnValues[3].split(' ')[0];

            if (datepart.length == 0)
                datepart = columnValues[3]

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
            datepart = columnValues[4].split(' ')[0];

            if (datepart.length == 0)
                datepart = columnValues[4]


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
            datepart = columnValues[5].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[5]
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

                closeDate = [yyyy, mm, dd].join('-');
            }
            else
                closeDate = [year2, month2, date2].join('-');


            // Display Value In Modal Inputs
            $('#menu-id', myModal).val(columnValues[1]);
            $('#activation-date-menu', myModal).val(activationDate);
            $('#expiry-date-menu', myModal).val(expiryDate);
            $('#close-date-menu', myModal).val(closeDate);
            $('#note-menu', myModal).val(columnValues[6]);
            $('#reason-for-modification-menu', myModal).val(columnValues[7]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-menu-dt').addClass('read-only');
            $('#menu-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-menu-modal').click(function (event) {
        if (IsValidMenuDataTableModal()) {
            row = menuDataTable.row.add([
                          tag,
                          menuId,
                          menuIdText,
                          activationDate,
                          expiryDate,
                          closeDate,
                          note,
                          reasonForModification,
            ]).draw();

            // Error Message In Span
            $('#menu-data-table-error').addClass('d-none');

            HideMenuDataTableColumns();

            menuDataTable.columns.adjust().draw();

            ClearModal('menu');

            $('#menu-modal').modal('hide');

            EnableNewOperation('menu');
        }
    });

    // Modal update Button Event
    $('#btn-update-menu-modal').click(function (event) {

        $('#select-all-menu').prop('checked', false);
        if (IsValidMenuDataTableModal()) {
            menuDataTable.row(selectedRowIndex).data([
                          tag,
                          menuId,
                          menuIdText,
                          activationDate,
                          expiryDate,
                          closeDate,
                          note,
                          reasonForModification,
            ]).draw();
            // Error Message In Span
            $('#menu-validation span').html('');

            HideMenuDataTableColumns();

            menuDataTable.columns.adjust().draw();

            $('#menu-modal').modal('hide');

            EnableNewOperation('menu');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-menu-dt').click(function (event) {
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($("#tbl-menu tbody input[type='checkbox']:checked").each(function () {
                    menuDataTable.row($("#tbl-menu tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-menu-dt').data('rowindex');
                    EnableNewOperation('menu');

                    $('#select-all-menu').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (!menuDataTable.data().any())
                        $('#menu-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Menu Datatable
    $('#select-all-menu').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-menu tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = menuDataTable.row(row).index();

                rowData = (menuDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-menu-dt').data('rowindex', arr);
                EnableDeleteOperation('menu')
            });
        }
        else {
            EnableNewOperation('menu')

            $('#tbl-menu tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-menu tbody').click("input[type=checkbox]", function () {
        $('#tbl-menu input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = menuDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (menuDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('menu');

                    $('#btn-update-menu-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-menu-dt').data('rowindex', rowData);
                    $('#btn-delete-menu-dt').data('rowindex', arr);
                    $('#select-all-menu').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-menu tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('menu');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1) {
            debugger;
            EnableEditDeleteOperation('menu');
        }

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1) {
            debugger;
            EnableDeleteOperation('menu');
        }

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-menu').prop('checked', true);
        else
            $('#select-all-menu').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-menu > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (menuDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#menu-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate  Fund Module
    function IsValidMenuDataTableModal() {
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        menuId = $('#menu-id option:selected').val();
        menuIdText = $('#menu-id option:selected').text();
        activationDate = $('#activation-date-menu').val();
        expiryDate = $('#expiry-date-menu').val();
        closeDate = $('#close-date-menu').val();
        note = $('#note-menu').val();
        reasonForModification = $('#reason-for-modification-menu').val();

        if (note == '')
            note = 'None';

        if (reasonForModification == '')
            reasonForModification = 'None';

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-menu');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-menu');

        if (menuId == "") {
            result = false;
            $('#menu-id-error').removeClass('d-none');
        } else
            $('#menu-id-error').addClass('d-none');

        if (!isValidActivationDate) {
            result = false;
            $('#activation-date-menu-error').removeClass('d-none');
        } else
            $('#activation-date-menu-error').addClass('d-none');

        if (!isValidExpiryDate) {
            result = false;
            $('#expiry-date-menu-error').removeClass('d-none');
        } else
            $('#expiry-date-menu-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideMenuDataTableColumns() {
        menuDataTable.column(1).visible(false);
        menuDataTable.column(5).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Special Permission - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-special-permission-dt').click(function () {
        event.preventDefault();
        SetModalTitle('special-permission', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-special-permission-dt').click(function () {
        debugger;
        SetModalTitle('special-permission', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-special-permission-dt').data('rowindex');
            id = $('#special-permission-modal').attr('id');
            myModal = $('#' + id).modal();

            // Get Only Activation Date
            datepart = columnValues[3].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[3]
            }

            const t = new Date(datepart);

            let today = t.toLocaleDateString("en-US");

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
            datepart = columnValues[4].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[4]
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
            datepart = columnValues[5].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[5]
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

                closeDate = [yyyy, mm, dd].join('-');
            }
            else
                closeDate = [year2, month2, date2].join('-');


            // Display Value In Modal Inputs
            $('#special-permission-id', myModal).val(columnValues[1]);
            $('#activation-date-special-permission', myModal).val(activationDate);
            $('#expiry-date-special-permission', myModal).val(expiryDate);
            $('#close-date-special-permission', myModal).val(closeDate);
            $('#note-special-permission', myModal).val(columnValues[6]);
            $('#reason-for-modification-special-permission', myModal).val(columnValues[7]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-special-permission-dt').addClass('read-only');
            $('#special-permission-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-special-permission-modal').click(function (event) {
        if (IsValidSpecialPermissionDataTableModal()) {
            row = specialPermissionDataTable.row.add([
                        tag,
                        specialPermissionId,
                        specialPermissionIdText,
                        activationDate,
                        expiryDate,
                        closeDate,
                        note,
                        reasonForModification,
            ]).draw();

            // Error Message In Span
            $('#special-permission-data-table-error').addClass('d-none');

            HideSpecialPermissionDataTableColumns();

            specialPermissionDataTable.columns.adjust().draw();

            ClearModal('special-permission');

            $('#special-permission-modal').modal('hide');

            EnableNewOperation('special-permission');
        }
    });

    // Modal update Button Event
    $('#btn-update-special-permission-modal').click(function (event) {

        $('#select-all-special-permission').prop('checked', false);
        if (IsValidSpecialPermissionDataTableModal()) {
            specialPermissionDataTable.row(selectedRowIndex).data([
                        tag,
                        specialPermissionId,
                        specialPermissionIdText,
                        activationDate,
                        expiryDate,
                        closeDate,
                        note,
                        reasonForModification,
            ]).draw();
            // Error Message In Span
            $('#special-permission-validation span').html('');

            HideSpecialPermissionDataTableColumns();

            specialPermissionDataTable.columns.adjust().draw();

            $('#special-permission-modal').modal('hide');

            EnableNewOperation('special-permission');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-special-permission-dt').click(function (event) {
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($("#tbl-special-permission tbody input[type='checkbox']:checked").each(function () {
                    specialPermissionDataTable.row($("#tbl-special-permission tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-special-permission-dt').data('rowindex');
                    EnableNewOperation('special-permission');

                    $('#select-all-special-permission').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (!specialPermissionDataTable.data().any())
                    $('#special-permission-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#select-all-special-permission').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-special-permission tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = specialPermissionDataTable.row(row).index();

                rowData = (specialPermissionDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-special-permission-dt').data('rowindex', arr);
                EnableDeleteOperation('special-permission')
            });
        }
        else {
            EnableNewOperation('special-permission')

            $('#tbl-special-permission tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-special-permission tbody').click("input[type=checkbox]", function () {
        $('#tbl-special-permission input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = specialPermissionDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (specialPermissionDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('special-permission');

                    $('#btn-update-special-permission-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-special-permission-dt').data('rowindex', rowData);
                    $('#btn-delete-special-permission-dt').data('rowindex', arr);
                    $('#select-all-special-permission').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-special-permission tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('special-permission');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('special-permission');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('special-permission');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-special-permission').prop('checked', true);
        else
            $('#select-all-special-permission').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-special-permission > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (specialPermissionDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#special-permission-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate  Fund Module
    function IsValidSpecialPermissionDataTableModal() {
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        specialPermissionId = $('#special-permission-id option:selected').val();
        specialPermissionIdText = $('#special-permission-id option:selected').text();
        activationDate = $('#activation-date-special-permission').val();
        expiryDate = $('#expiry-date-special-permission').val();
        closeDate = $('#close-date-special-permission').val();
        note = $('#note-special-permission').val();
        reasonForModification = $('#reason-for-modification-special-permission').val();

        if (note == '')
            note = 'None';

        if (reasonForModification == '')
            reasonForModification = 'None';
        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-special-permission');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-special-permission');

        if (specialPermissionId == '') {
            result = false;
            $('#special-permission-id-error').removeClass('d-none');
        } else
            $('#special-permission-id-error').addClass('d-none');

        if (!isValidActivationDate) {
            result = false;
            $('#activation-date-special-permission-error').removeClass('d-none');
        } else
            $('#activation-date-special-permission-error').addClass('d-none');

        if (!isValidExpiryDate) {
            result = false;
            $('#expiry-date-special-permission-error').removeClass('d-none');
        } else
            $('#expiry-date-special-permission-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideSpecialPermissionDataTableColumns() {
        specialPermissionDataTable.column(1).visible(false);
        specialPermissionDataTable.column(5).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Transaction Limit - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-transaction-limit-dt').click(function () {
        event.preventDefault();
        SetModalTitle('transaction-limit', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-transaction-limit-dt').click(function () {
        debugger;
        SetModalTitle('transaction-limit', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-transaction-limit-dt').data('rowindex');

            columnValues = $('#btn-edit-transaction-limit-dt').data('rowindex');
            id = $('#transaction-limit-modal').attr('id');
            myModal = $('#' + id).modal();

            // Get Only Activation Date
            datepart = columnValues[17].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[17]
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
            datepart = columnValues[18].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[18]
            }

            const t1 = new Date(datepart);

            let today1 = t1.toLocaleDateString("en-US");

            const date1 = ('0' + t1.getDate()).slice(-2);
            const month1 = ('0' + (t1.getMonth() + 1)).slice(-2);
            const year1 = t1.getFullYear();

            if (isNaN(year1) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                let dd = arr[0];
                let mm = arr[1];
                let yyyy = arr[2];

                expiryDate = [yyyy, mm, dd].join('-');
            }
            else
                expiryDate = [year1, month1, date1].join('-');


            // Get Only Close Date
            datepart = columnValues[19].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[19]
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

                closeDate = [yyyy, mm, dd].join('-');
            }
            else
                closeDate = [year2, month2, date2].join('-');

            debugger;
            // Display Value In Modal Inputs
            $('#general-ledger-id', myModal).val(columnValues[1]);
            $('#transaction-type-id', myModal).val(columnValues[3]);
            $('#currency-id', myModal).val(columnValues[5]);
            $('#minimum-amount-transaction', myModal).val(columnValues[7]);
            $('#maximum-amount-transaction', myModal).val(columnValues[8]);
            $('#maximum-number-transaction', myModal).val(columnValues[9]);
            $('#minimum-amount-verification', myModal).val(columnValues[10]);
            $('#maximum-amount-verification', myModal).val(columnValues[11]);
            $('#maximum-number-verification', myModal).val(columnValues[12]);
            $('#minimum-amount-auto-verification', myModal).val(columnValues[13]);
            $('#maximum-amount-auto-verification', myModal).val(columnValues[14]);
            $('#maximum-number-auto-verification', myModal).val(columnValues[15]);

            if (columnValues[16] === "True") {
                $('#enable-partial-payment').prop("checked", true);
            }
            else {
                $('#enable-partial-payment').prop("checked", false);
            }
            $('#activation-date-transaction-limit', myModal).val(activationDate);
            $('#expiry-date-transaction-limit', myModal).val(expiryDate);
            $('#close-date-transaction-limit', myModal).val(closeDate);
            $('#note-transaction-limit', myModal).val(columnValues[20]);
            $('#reason-for-modification-transaction-limit', myModal).val(columnValues[21]);



            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-transaction-limit-dt').addClass('read-only');
            $('#menu-transaction-limit').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-transaction-limit-modal').click(function (event) {
        debugger;
        if (IsValidTransactionLimitDataTableModal()) {
            row = transactionLimitDataTable.row.add([
                        tag,
                        generalLedgerId,
                        generalLedgerIdText,
                        transactionTypeId,
                        transactionTypeText,
                        currencyId,
                        currencyIdText,
                        minimumAmountLimitForTransaction,
                        maximumAmountLimitForTransaction,
                        maximumNumberOfBackDaysForTransaction,
                        minimumAmountLimitForVerification,
                        maximumAmountLimitForVerification,
                        maximumNumberOfBackDaysForVerification,
                        minimumAmountLimitForAutoVerification,
                        maximumAmountLimitForAutoVerification,
                        maximumNumberOfBackDaysForAutoVerification,
                        enableCashDenomination,
                        activationDate,
                        expiryDate,
                        closeDate,
                        note,
                        reasonForModification,
            ]).draw();

            // Error Message In Span
            $('#transaction-limit-data-table-error').addClass('d-none');

            HideTransactionLimitDataTableColumns();

            transactionLimitDataTable.columns.adjust().draw();

            ClearModal('transaction-limit');

            $('#transaction-limit-modal').modal('hide');

            EnableNewOperation('transaction-limit');
        }
    });

    // Modal update Button Event
    $('#btn-update-transaction-limit-modal').click(function (event) {

        $('#select-all-transaction-limit').prop('checked', false);
        if (IsValidTransactionLimitDataTableModal()) {
            transactionLimitDataTable.row(selectedRowIndex).data([
                        tag,
                        generalLedgerId,
                        generalLedgerIdText,
                        transactionTypeId,
                        transactionTypeText,
                        currencyId,
                        currencyIdText,
                        minimumAmountLimitForTransaction,
                        maximumAmountLimitForTransaction,
                        maximumNumberOfBackDaysForTransaction,
                        minimumAmountLimitForVerification,
                        maximumAmountLimitForVerification,
                        maximumNumberOfBackDaysForVerification,
                        minimumAmountLimitForAutoVerification,
                        maximumAmountLimitForAutoVerification,
                        maximumNumberOfBackDaysForAutoVerification,
                        enableCashDenomination,
                        activationDate,
                        expiryDate,
                        closeDate,
                        note,
                        reasonForModification,
            ]).draw();
            // Error Message In Span
            $('#transaction-limit-validation span').html('');

            HideTransactionLimitDataTableColumns();

            transactionLimitDataTable.columns.adjust().draw();

            $('#transaction-limit-modal').modal('hide');

            EnableNewOperation('transaction-limit');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-transaction-limit-dt').click(function (event) {
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($("#tbl-transaction-limit tbody input[type='checkbox']:checked").each(function () {
                    transactionLimitDataTable.row($("#tbl-transaction-limit tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-transaction-limit-dt').data('rowindex');
                    EnableNewOperation('transaction-limit');

                    $('#select-all-transaction-limit').prop('checked', false);
                    // Display Error, If Table Has Not Any Record
                    if (!transactionLimitDataTable.data().any())
                    $('#transaction-limit-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Menu Datatable
    $('#select-all-transaction-limit').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-transaction-limit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = transactionLimitDataTable.row(row).index();

                rowData = (transactionLimitDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-transaction-limit-dt').data('rowindex', arr);
                EnableDeleteOperation('transaction-limit')
            });
        }
        else {
            EnableNewOperation('transaction-limit')

            $('#tbl-transaction-limit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-transaction-limit tbody').click("input[type=checkbox]", function () {
        $('#tbl-transaction-limit input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = transactionLimitDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (transactionLimitDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('transaction-limit');

                    $('#btn-update-transaction-limit-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-transaction-limit-dt').data('rowindex', rowData);
                    $('#btn-delete-transaction-limit-dt').data('rowindex', arr);
                    $('#select-all-transaction-limit').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-transaction-limit tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('transaction-limit');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('transaction-limit');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('transaction-limit');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-transaction-limit').prop('checked', true);
        else
            $('#select-all-transaction-limit').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-transaction-limit > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (menuDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate  Fund Module
    function IsValidTransactionLimitDataTableModal() {
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        generalLedgerId = $('#general-ledger-id option:selected').val();
        generalLedgerIdText = $('#general-ledger-id option:selected').text();
        transactionTypeId = $('#transaction-type-id option:selected').val();
        transactionTypeText = $('#transaction-type-id option:selected').text();
        currencyId = $('#currency-id option:selected').val();
        currencyIdText = $('#currency-id option:selected').text();
        minimumAmountLimitForTransaction = $('#minimum-amount-transaction').val();
        maximumAmountLimitForTransaction = $('#maximum-amount-transaction').val();
        maximumNumberOfBackDaysForTransaction = $('#maximum-number-transaction').val();
        minimumAmountLimitForVerification = $('#minimum-amount-verification').val();
        maximumAmountLimitForVerification = $('#maximum-amount-verification').val();
        maximumNumberOfBackDaysForVerification = $('#maximum-number-verification').val();
        minimumAmountLimitForAutoVerification = $('#minimum-amount-auto-verification').val();
        maximumAmountLimitForAutoVerification = $('#maximum-amount-auto-verification').val();
        maximumNumberOfBackDaysForAutoVerification = $('#maximum-number-auto-verification').val();

        enableCashDenomination = $('#enable-cash-denomination').is(':checked') ? "True" : "False";
        activationDate = $('#activation-date-transaction-limit').val();
        expiryDate = $('#expiry-date-transaction-limit').val();
        closeDate = $('#close-date-transaction-limit').val();
        note = $('#note-transaction-limit').val();
        reasonForModification = $('#reason-for-modification-transaction-limit').val();

        if (note == '')
            note = 'None';

        if ((reasonForModification == '') || (reasonForModification == undefined))
            reasonForModification = 'None';

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-transaction-limit');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-transaction-limit');

        //General Ledger Id
        if (generalLedgerId == '') {
            result = false;
            $('#general-ledger-id-error').removeClass('d-none');
        } else
            $('#general-ledger-id-error').addClass('d-none');

        //Transaction Type Id
        if (transactionTypeId == '') {
            result = false;
            $('#transaction-type-id-error').removeClass('d-none');
        } else
            $('#transaction-type-id-error').addClass('d-none');

        //Transaction Type Id
        if (currencyId == '') {
            result = false;
            $('#currency-id-error').removeClass('d-none');
        } else
            $('#currency-id-error').addClass('d-none');


        //Minimum Amount Limit For Transaction
        if (minimumAmountLimitForTransaction == '' || parseFloat(minimumAmountLimitForTransaction) < 1 || parseFloat(minimumAmountLimitForTransaction) > 999999999) {
            result = false;
            $('#minimum-amount-transaction-error').removeClass('d-none')
        }
        else
            $('#minimum-amount-transaction-error').addClass('d-none');

        //Maximum Amount Limit For Transaction
        if (maximumAmountLimitForTransaction == '' || parseFloat(maximumAmountLimitForTransaction) < 1 || parseFloat(maximumAmountLimitForTransaction) < parseFloat(minimumAmountLimitForTransaction) || parseFloat(maximumAmountLimitForTransaction) > 999999999) {
            result = false;
            $('#maximum-amount-transaction-error').removeClass('d-none')
        }
        else
            $('#maximum-amount-transaction-error').addClass('d-none');


        //Maximum Number Of Back Days For Transaction
        if (maximumNumberOfBackDaysForTransaction == "" || maximumNumberOfBackDaysForTransaction < 1 || maximumNumberOfBackDaysForTransaction > 365) {
            result = false;
            $('#maximum-number-transaction-error').removeClass('d-none');
        } else
            $('#maximum-number-transaction-error').addClass('d-none');

        //Minimum Amount Limit For Verification
        if (minimumAmountLimitForVerification == '' || parseFloat(minimumAmountLimitForVerification) < 1 || parseFloat(minimumAmountLimitForVerification) > 999999999) {
            result = false;
            $('#minimum-amount-verification-error').removeClass('d-none')
        }
        else
            $('#minimum-amount-verification-error').addClass('d-none');

        //Maximum Amount Limit For Verification
        if (maximumAmountLimitForVerification == '' || parseFloat(maximumAmountLimitForVerification) < 1 || parseFloat(maximumAmountLimitForVerification) < parseFloat(minimumAmountLimitForVerification) || parseFloat(maximumAmountLimitForVerification) > 999999999) {
            result = false;
            $('#maximum-amount-verification-error').removeClass('d-none')
        }
        else
            $('#maximum-amount-verification-error').addClass('d-none');

        //Maximum Number Of Back Days For Transaction
        if (maximumNumberOfBackDaysForVerification == "" || maximumNumberOfBackDaysForVerification < 1 || maximumNumberOfBackDaysForVerification > 365) {
            result = false;
            $('#maximum-number-verification-error').removeClass('d-none');
        } else
            $('#maximum-number-verification-error').addClass('d-none');


        //Minimum Amount Limit For Verification
        if (minimumAmountLimitForAutoVerification == '' || parseFloat(minimumAmountLimitForAutoVerification) < 1 || parseFloat(minimumAmountLimitForAutoVerification) > 999999999) {
            result = false;
            $('#minimum-amount-auto-verification-error').removeClass('d-none')
        }
        else
            $('#minimum-amount-auto-verification-error').addClass('d-none');

        //Maximum Amount Limit For Verification
        if (maximumAmountLimitForAutoVerification == '' || parseFloat(maximumAmountLimitForAutoVerification) < 1 || parseFloat(maximumAmountLimitForAutoVerification) < parseFloat(minimumAmountLimitForAutoVerification) || parseFloat(maximumAmountLimitForAutoVerification) > 999999999) {
            result = false;
            $('#maximum-amount-auto-verification-error').removeClass('d-none');
        }
        else
            $('#maximum-amount-auto-verification-error').addClass('d-none');

        //Maximum Number Of Back Days For Transaction
        if (maximumNumberOfBackDaysForAutoVerification == "" || maximumNumberOfBackDaysForAutoVerification < 1 || maximumNumberOfBackDaysForAutoVerification > 365) {
            result = false;
            $('#maximum-number-auto-verification-error').removeClass('d-none');
        } else
            $('#maximum-number-auto-verification-error').addClass('d-none');



        if (!isValidActivationDate) {
            result = false;
            $('#activation-date-transaction-limit-error').removeClass('d-none');
        } else
            $('#activation-date-transaction-limit-error').addClass('d-none');

        if (!isValidExpiryDate) {
            result = false;
            $('#expiry-date-transaction-limit-error').removeClass('d-none');
        } else
            $('#expiry-date-transaction-limit-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideTransactionLimitDataTableColumns() {
        transactionLimitDataTable.column(1).visible(false);
        transactionLimitDataTable.column(3).visible(false);
        transactionLimitDataTable.column(5).visible(false);
        transactionLimitDataTable.column(19).visible(false);
        transactionLimitDataTable.column(21).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Account Number - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    $('#enable-auto-account-number').change(function () {
        debugger;
        if ($(this).is(':checked', true)) {
            $('#account-number-mask-error').addClass('d-none');
            $('#start-account-number-error').addClass('d-none');
            $('#end-account-number-error').addClass('d-none');
            $('#account-number-increment-by-error').addClass('d-none');
        }
    });

    // DataTable Add Button 
    $('#btn-add-account-number-dt').click(function () {
        debugger;
        event.preventDefault();
        editedAccountGeneralLedgerId = '';

        SetAccountGeneralLedgerTypeUniqueDropdownList();
        SetModalTitle('account-number', 'Add');

        enableAutoAccountNumber = $('#enable-auto-account-number').is(':checked');
        if (!enableAutoAccountNumber)
            $('#auto-account-number-block').addClass('d-none');


    });

    // DataTable Edit Button 
    $('#btn-edit-account-number-dt').click(function () {
        debugger;
        SetModalTitle('account-number', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            debugger;
            columnValues = $('#btn-edit-account-number-dt').data('rowindex');
            id = $('#account-number-modal').attr('id');
            myModal = $('#' + id).modal();

            editedAccountGeneralLedgerId = columnValues[1];
            SetAccountGeneralLedgerTypeUniqueDropdownList();
            // Display Value In Modal Inputs
            $('#account-general-ledger-id', myModal).val(columnValues[1]);


            if (columnValues[4] === 'True')
                $('#enable-regenerate-unused-account-number').prop('checked', true);
            else
                $('#enable-regenerate-unused-account-number').prop('checked', false);

            if (columnValues[3] === true)
                $('#enable-auto-account-number').prop('checked', true);
            else
                $('#enable-auto-account-number').prop('checked', false);

            if (columnValues[5] === 'True')
                $('#enable-random-account-number').prop('checked', true);
            else
                $('#enable-random-account-number').prop('checked', false);

            if (columnValues[6] === 'True')
                $('#enable-customize-account-number').prop('checked', true);
            else
                $('#enable-customize-account-number').prop('checked', false);

            if (columnValues[7] === 'True')
                $('#enable-digital-code-for-account-number').prop('checked', true);
            else
                $('#enable-digital-code-for-account-number').prop('checked', false);

            $('#account-number-mask', myModal).val(columnValues[8]);
            $('#start-account-number', myModal).val(columnValues[10]);
            $('#end-account-number', myModal).val(columnValues[11]);
            $('#account-number-increment-by', myModal).val(columnValues[12]);
            $('#note-account', myModal).val(columnValues[13]);
            $('#reason-for-modification-account', myModal).val(columnValues[14]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-account-number-dt').addClass('read-only');
            $('#account-number-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-account-number-modal').click(function (event) {
        if (IsValidAccountNumberDataTableModal()) {
            debugger;
            row = accountNumberDataTable.row.add([
                        tag,
                        accountGeneralLedgerId,
                         accountGeneralLedgerIdText,
                         enableAutoAccountNumber,
                         enableReGenerateUnusedAccountNumber,
                         enableRandomAccountNumber,
                         enableCustomizeAccountNumber,
                         enableDigitalCodeForAccountNumber,
                         accountNumberMask,
                         accountNumberMaskText,
                         startAccountNumber,
                         endAccountNumber,
                         accountNumberIncrementBy,
                         note,
                         reasonForModification
            ]).draw();

            // Error Message In Span
            $('#account-number-data-table-error').addClass('d-none');

            HideAccountNumberDataTableColumns();

            accountNumberDataTable.columns.adjust().draw();

            ClearModal('account-number');

            $('#account-number-modal').modal('hide');

            EnableNewOperation('account-number');
        }
    });

    // Modal update Button Event
    $('#btn-update-account-number-modal').click(function (event) {

        $('#select-all-account-number').prop('checked', false);
        if (IsValidAccountNumberDataTableModal()) {
            accountNumberDataTable.row(selectedRowIndex).data([
                         tag,
                         accountGeneralLedgerId,
                         accountGeneralLedgerIdText,
                         enableAutoAccountNumber,
                         enableReGenerateUnusedAccountNumber,
                         enableRandomAccountNumber,
                         enableCustomizeAccountNumber,
                         enableDigitalCodeForAccountNumber,
                         accountNumberMask,
                         accountNumberMaskText,
                         startAccountNumber,
                         endAccountNumber,
                         accountNumberIncrementBy,
                         note,
                         reasonForModification

            ]).draw();
            // Error Message In Span
            $('#account-number-validation span').html('');

            HideAccountNumberDataTableColumns();

            accountNumberDataTable.columns.adjust().draw();

            $('#account-number-modal').modal('hide');

            EnableNewOperation('account-number');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-account-number-dt').click(function (event) {
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($("#tbl-account-number tbody input[type='checkbox']:checked").each(function () {
                    accountNumberDataTable.row($("#tbl-account-number tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-account-number-dt').data('rowindex');
                    EnableNewOperation('account-number');

                     SetAccountGeneralLedgerTypeUniqueDropdownList();

                    $('#select-all-account-number').prop('checked', false);
                    // Display Error, If Table Has Not Any Record
                    if (!accountNumberDataTable.data().any())
                    $('#account-number-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Menu Datatable
    $('#select-all-account-number').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-account-number tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = accountNumberDataTable.row(row).index();

                rowData = (accountNumberDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-account-number-dt').data('rowindex', arr);
                EnableDeleteOperation('account-number')
            });
        }
        else {
            EnableNewOperation('account-number')

            $('#tbl-account-number tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-account-number tbody').click("input[type=checkbox]", function () {
        $('#tbl-account-number input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = accountNumberDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (accountNumberDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('account-number');

                    $('#btn-update-account-number-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-account-number-dt').data('rowindex', rowData);
                    $('#btn-delete-account-number-dt').data('rowindex', arr);
                    $('#select-all-account-number').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-account-number tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('account-number');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('account-number');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('account-number');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-account-number').prop('checked', true);
        else
            $('#select-all-account-number').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-account-number > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (accountNumberDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#account-general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate  Fund Module
    function IsValidAccountNumberDataTableModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        accountGeneralLedgerId = $('#account-general-ledger-id option:selected').val();
        accountGeneralLedgerIdText = $('#account-general-ledger-id option:selected').text();
        enableAutoAccountNumber = $('#enable-auto-account-number').is(':checked');
        enableReGenerateUnusedAccountNumber = $('#enable-regenerate-unused-account-number').is(':checked') ? 'True' : 'False';
        enableRandomAccountNumber = $('#enable-random-account-number').is(':checked') ? 'True' : 'False';
        enableCustomizeAccountNumber = $('#enable-customize-account-number').is(':checked') ? 'True' : 'False';
        enableDigitalCodeForAccountNumber = $('#enable-digital-code-for-account-number').is(':checked') ? 'True' : 'False';

        accountNumberMask = $('#account-number-mask option:selected')
         .map(function () {
             return $(this).val();
         }).get().join(',');

        accountNumberMaskText = $('#account-number-mask option:selected')
         .map(function () {
             return $(this).text();
         }).get().join(',');

        startAccountNumber = parseInt($('#start-account-number').val());
        endAccountNumber = parseInt($('#end-account-number').val());
        accountNumberIncrementBy = parseInt($('#account-number-increment-by').val());

        note = $('#note-account').val();
        reasonForModification = $('#reason-for-modification-account').val();


        if (note == '')
            note = 'None';

        if (reasonForModification == '')
            reasonForModification = 'None';

        if (accountGeneralLedgerId == "") {
            result = false;
            $('#account-general-ledger-id-error').removeClass('d-none');
        } else
            $('#account-general-ledger-id-error').addClass('d-none');

        // Check If Auto Account Number  Is Enabled
        if (enableAutoAccountNumber) {

            if (accountNumberMask == "") {
                result = false;
                $('#account-number-mask-error').removeClass('d-none');
            } else
                $('#account-number-mask-error').addClass('d-none');

            // Check if Start Account Number is a valid number
            if (isNaN(startAccountNumber) === false) {

                minimum = parseInt($('#start-account-number').attr('min'));
                maximum = parseInt($('#start-account-number').attr('max'));

                if (parseInt(startAccountNumber) < parseInt(minimum) || parseInt(startAccountNumber) > parseInt(maximum)) {
                    result = false;
                    $('#start-account-number-error').removeClass('d-none');
                } else {
                    $('#start-account-number-error').addClass('d-none');
                }
            }

            else {
                // Show error message if startApplicationNumber is NaN
                result = false;
                $('#start-account-number-error').removeClass('d-none');
            }

            // Check if End Account Number is a valid number
            if (isNaN(endAccountNumber) === false) {

                minimum = parseInt($('#end-account-number').attr('min'));
                maximum = parseInt($('#end-account-number').attr('max'));

                if (parseInt(endAccountNumber) < parseInt(minimum) || parseInt(endAccountNumber) > parseInt(maximum)) {
                    result = false;
                    $('#end-account-number-error').removeClass('d-none');
                }

                else
                    $('#end-account-number-error').addClass('d-none');
            }

            else {
                // Show error message if startApplicationNumber is NaN
                result = false;
                $('#end-account-number-error').removeClass('d-none');
            }

            // Check if Account Number Increment By is a valid number
            if (isNaN(accountNumberIncrementBy) === false) {

                minimum = parseInt($('#account-number-increment-by').attr('min'));
                maximum = parseInt($('#account-number-increment-by').attr('max'));

                if (parseInt(accountNumberIncrementBy) < parseInt(minimum) || parseInt(accountNumberIncrementBy) > parseInt(maximum)) {
                    result = false;
                    $('#account-number-increment-by-error').removeClass('d-none');
                }

                else
                    $('#account-number-increment-by-error').addClass('d-none');
            }

            else {
                // Show error message if startApplicationNumber is NaN
                result = false;
                $('#account-number-increment-by-error').removeClass('d-none');
            }
        }
        else {
            accountNumberMask = '00000000-0000-0000-0000-000000000000';
            accountNumberMaskText = 'None';
            startAccountNumber = 0;
            endAccountNumber = 0;
            accountNumberIncrementBy = 0;
        }

        return result;

    }

    // Hide Unnecessary Columns
    function HideAccountNumberDataTableColumns() {
        accountNumberDataTable.column(1).visible(false);
        accountNumberDataTable.column(8).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Agreement Number - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    $('#enable-auto-agreement-number').change(function () {
        debugger;
        if ($(this).is(':checked', true)) {
            $('#agreement-number-mask-error').addClass('d-none');
            $('#start-agreement-number-error').addClass('d-none');
            $('#end-agreement-number-error').addClass('d-none');
            $('#agreement-number-increment-by-error').addClass('d-none');
        }
    });

    // DataTable Add Button 
    $('#btn-add-agreement-number-dt').click(function () {
        debugger;
        event.preventDefault();
        editedAgreementGeneralLedgerId = '';

        SetAgreementGeneralLedgerTypeUniqueDropdownList();
        SetModalTitle('agreement-number', 'Add');

        enableAutoAgreementNumber = $('#enable-auto-agreement-number').is(':checked');
        if (!enableAutoAgreementNumber)
            $('#auto-agreement-number-block').addClass('d-none');
    });

    // DataTable Edit Button 
    $('#btn-edit-agreement-number-dt').click(function () {
        debugger;
        SetModalTitle('agreement-number', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            debugger;
            columnValues = $('#btn-edit-agreement-number-dt').data('rowindex');
            id = $('#agreement-number-modal').attr('id');
            myModal = $('#' + id).modal();

            editedAgreementGeneralLedgerId = columnValues[1];
            SetAgreementGeneralLedgerTypeUniqueDropdownList();
            // Display Value In Modal Inputs
            $('#agreement-general-ledger-id', myModal).val(columnValues[1]);


            if (columnValues[4] === 'True')
                $('#enable-regenerate-unused-agreement-number').prop('checked', true);
            else
                $('#enable-regenerate-unused-agreement-number').prop('checked', false);

            if (columnValues[3] === true)
                $('#enable-auto-agreement-number').prop('checked', true);
            else
                $('#enable-auto-agreement-number').prop('checked', false);

            if (columnValues[5] === 'True')
                $('#enable-random-agreement-number').prop('checked', true);
            else
                $('#enable-random-agreement-number').prop('checked', false);

            if (columnValues[6] === 'True')
                $('#enable-customize-agreement-number').prop('checked', true);
            else
                $('#enable-customize-agreement-number').prop('checked', false);

            if (columnValues[7] === 'True')
                $('#enable-digital-code-for-agreement-number').prop('checked', true);
            else
                $('#enable-digital-code-for-agreement-number').prop('checked', false);

            $('#agreement-number-mask', myModal).val(columnValues[8]);
            $('#start-agreement-number', myModal).val(columnValues[10]);
            $('#end-agreement-number', myModal).val(columnValues[11]);
            $('#agreement-number-increment-by', myModal).val(columnValues[12]);
            $('#note-agreement', myModal).val(columnValues[13]);
            $('#reason-for-modification-agreement', myModal).val(columnValues[14]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-agreement-number-dt').addClass('read-only');
            $('#agreement-number-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-agreement-number-modal').click(function (event) {
        if (IsValidAgreementNumberDataTableModal()) {
            debugger;
            row = agreementNumberDataTable.row.add([
                tag,
                agreementGeneralLedgerId,
                agreementGeneralLedgerIdText,
                enableAutoAgreementNumber,
                enableReGenerateUnusedAgreementNumber,
                enableRandomAgreementNumber,
                enableCustomizeAgreementNumber,
                enableDigitalCodeForAgreementNumber,
                agreementNumberMask,
                agreementNumberMaskText,
                startAgreementNumber,
                endAgreementNumber,
                agreementNumberIncrementBy,
                note,
                reasonForModification
            ]).draw();

            // Error Message In Span
            $('#agreement-number-data-table-error').addClass('d-none');

            HideAgreementNumberDataTableColumns();

            agreementNumberDataTable.columns.adjust().draw();

            ClearModal('agreement-number');

            $('#agreement-number-modal').modal('hide');

            EnableNewOperation('agreement-number');
        }
    });

    // Modal update Button Event
    $('#btn-update-agreement-number-modal').click(function (event) {

        $('#select-all-agreement-number').prop('checked', false);
        if (IsValidAgreementNumberDataTableModal()) {
            agreementNumberDataTable.row(selectedRowIndex).data([
                tag,
                agreementGeneralLedgerId,
                agreementGeneralLedgerIdText,
                enableAutoAgreementNumber,
                enableReGenerateUnusedAgreementNumber,
                enableRandomAgreementNumber,
                enableCustomizeAgreementNumber,
                enableDigitalCodeForAgreementNumber,
                agreementNumberMask,
                agreementNumberMaskText,
                startAgreementNumber,
                endAgreementNumber,
                agreementNumberIncrementBy,
                note,
                reasonForModification

            ]).draw();
            // Error Message In Span
            $('#agreement-number-validation span').html('');

            HideAgreementNumberDataTableColumns();

            agreementNumberDataTable.columns.adjust().draw();

            $('#agreement-number-modal').modal('hide');

            EnableNewOperation('agreement-number');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-agreement-number-dt').click(function (event) {
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($("#tbl-agreement-number tbody input[type='checkbox']:checked").each(function () {
                    agreementNumberDataTable.row($("#tbl-agreement-number tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-agreement-number-dt').data('rowindex');
                    EnableNewOperation('agreement-number');

                    SetAgreementGeneralLedgerTypeUniqueDropdownList();

                    $('#select-all-agreement-number').prop('checked', false);
                    // Display Error, If Table Has Not Any Record
                    if (!agreementNumberDataTable.data().any())
                        $('#agreement-number-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Menu Datatable
    $('#select-all-agreement-number').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-agreement-number tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = agreementNumberDataTable.row(row).index();

                rowData = (agreementNumberDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-agreement-number-dt').data('rowindex', arr);
                EnableDeleteOperation('agreement-number')
            });
        }
        else {
            EnableNewOperation('agreement-number')

            $('#tbl-agreement-number tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-agreement-number tbody').click("input[type=checkbox]", function () {
        $('#tbl-agreement-number input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = agreementNumberDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (agreementNumberDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('agreement-number');

                    $('#btn-update-agreement-number-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-agreement-number-dt').data('rowindex', rowData);
                    $('#btn-delete-agreement-number-dt').data('rowindex', arr);
                    $('#select-all-agreement-number').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-agreement-number tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('agreement-number');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('agreement-number');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('agreement-number');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-agreement-number').prop('checked', true);
        else
            $('#select-all-agreement-number').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-agreement-number > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (agreementNumberDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#agreement-general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate  Fund Module
    function IsValidAgreementNumberDataTableModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        agreementGeneralLedgerId = $('#agreement-general-ledger-id option:selected').val();
        agreementGeneralLedgerIdText = $('#agreement-general-ledger-id option:selected').text();
        enableAutoAgreementNumber = $('#enable-auto-agreement-number').is(':checked');
        enableReGenerateUnusedAgreementNumber = $('#enable-regenerate-unused-agreement-number').is(':checked') ? 'True' : 'False';
        enableRandomAgreementNumber = $('#enable-random-agreement-number').is(':checked') ? 'True' : 'False';
        enableCustomizeAgreementNumber = $('#enable-customize-agreement-number').is(':checked') ? 'True' : 'False';
        enableDigitalCodeForAgreementNumber = $('#enable-digital-code-for-agreement-number').is(':checked') ? 'True' : 'False';

        agreementNumberMask = $('#agreement-number-mask option:selected')
            .map(function () {
                return $(this).val();
            }).get().join(',');

        agreementNumberMaskText = $('#agreement-number-mask option:selected')
            .map(function () {
                return $(this).text();
            }).get().join(',');

        startAgreementNumber = parseInt($('#start-agreement-number').val());
        endAgreementNumber = parseInt($('#end-agreement-number').val());
        agreementNumberIncrementBy = parseInt($('#agreement-number-increment-by').val());

        note = $('#note-agreement').val();
        reasonForModification = $('#reason-for-modification-agreement').val();


        if (note == '')
            note = 'None';

        if (reasonForModification == '')
            reasonForModification = 'None';

        if (agreementGeneralLedgerId == "") {
            result = false;
            $('#agreement-general-ledger-id-error').removeClass('d-none');
        } else
            $('#agreement-general-ledger-id-error').addClass('d-none');

        // Check If Auto Agreement Number  Is Enabled
        if (enableAutoAgreementNumber) {

            if (agreementNumberMask == "") {
                result = false;
                $('#agreement-number-mask-error').removeClass('d-none');
            } else
                $('#agreement-number-mask-error').addClass('d-none');

            // Check if Start Agreement Number is a valid number
            if (isNaN(startAgreementNumber) === false) {

                minimum = parseInt($('#start-agreement-number').attr('min'));
                maximum = parseInt($('#start-agreement-number').attr('max'));

                if (parseInt(startAgreementNumber) < parseInt(minimum) || parseInt(startAgreementNumber) > parseInt(maximum)) {
                    result = false;
                    $('#start-agreement-number-error').removeClass('d-none');
                } else {
                    $('#start-agreement-number-error').addClass('d-none');
                }
            }

            else {
                // Show error message if startApplicationNumber is NaN
                result = false;
                $('#start-agreement-number-error').removeClass('d-none');
            }

            // Check if End Agreement Number is a valid number
            if (isNaN(endAgreementNumber) === false) {

                minimum = parseInt($('#end-agreement-number').attr('min'));
                maximum = parseInt($('#end-agreement-number').attr('max'));

                if (parseInt(endAgreementNumber) < parseInt(minimum) || parseInt(endAgreementNumber) > parseInt(maximum)) {
                    result = false;
                    $('#end-agreement-number-error').removeClass('d-none');
                }

                else
                    $('#end-agreement-number-error').addClass('d-none');
            }

            else {
                // Show error message if startApplicationNumber is NaN
                result = false;
                $('#end-agreement-number-error').removeClass('d-none');
            }

            // Check if Agreement Number Increment By is a valid number
            if (isNaN(agreementNumberIncrementBy) === false) {

                minimum = parseInt($('#agreement-number-increment-by').attr('min'));
                maximum = parseInt($('#agreement-number-increment-by').attr('max'));

                if (parseInt(agreementNumberIncrementBy) < parseInt(minimum) || parseInt(agreementNumberIncrementBy) > parseInt(maximum)) {
                    result = false;
                    $('#agreement-number-increment-by-error').removeClass('d-none');
                }

                else
                    $('#agreement-number-increment-by-error').addClass('d-none');
            }

            else {
                // Show error message if startApplicationNumber is NaN
                result = false;
                $('#agreement-number-increment-by-error').removeClass('d-none');
            }
        }
        else {
            agreementNumberMask = '00000000-0000-0000-0000-000000000000';
            agreementNumberMaskText = 'None';
            startAgreementNumber = 0;
            endAgreementNumber = 0;
            agreementNumberIncrementBy = 0;
        }

        return result;

    }

    // Hide Unnecessary Columns
    function HideAgreementNumberDataTableColumns() {
        agreementNumberDataTable.column(1).visible(false);
        agreementNumberDataTable.column(8).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Application Number - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#enable-auto-application-number').change(function () {
        debugger;
        if ($(this).is(':checked', true)) {
            $('#application-number-mask-error').addClass('d-none');
            $('#start-application-number-error').addClass('d-none');
            $('#end-application-number-error').addClass('d-none');
            $('#application-number-increment-by-error').addClass('d-none');
        }
    });

    // DataTable Add Button 
    $('#btn-add-application-number-dt').click(function () {
        debugger;
        event.preventDefault();
        editedApplicationGeneralLedgerId = '';

        SetApplicationGeneralLedgerTypeUniqueDropdownList();
        SetModalTitle('application-number', 'Add');

        enableAutoApplicationNumber = $('#enable-auto-application-number').is(':checked');
        if (!enableAutoApplicationNumber)
            $('#auto-application-number-block').addClass('d-none');
    });

    // DataTable Edit Button 
    $('#btn-edit-application-number-dt').click(function () {
        debugger;
        SetModalTitle('application-number', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            debugger;
            columnValues = $('#btn-edit-application-number-dt').data('rowindex');
            id = $('#application-number-modal').attr('id');
            myModal = $('#' + id).modal();

            editedApplicationGeneralLedgerId = columnValues[1];
            SetApplicationGeneralLedgerTypeUniqueDropdownList();
            // Display Value In Modal Inputs
            $('#application-general-ledger-id', myModal).val(columnValues[1]);

            if (columnValues[4] === 'True')
                $('#enable-regenerate-unused-application-number').prop('checked', true);
            else
                $('#enable-regenerate-unused-application-number').prop('checked', false);

            if (columnValues[3] === true)
                $('#enable-auto-application-number').prop('checked', true);
            else
                $('#enable-auto-application-number').prop('checked', false);

            if (columnValues[5] === 'True')
                $('#enable-random-application-number').prop('checked', true);
            else
                $('#enable-random-application-number').prop('checked', false);

            if (columnValues[6] === 'True')
                $('#enable-customize-application-number').prop('checked', true);
            else
                $('#enable-customize-application-number').prop('checked', false);

            if (columnValues[7] === 'True')
                $('#enable-digital-code-for-application-number').prop('checked', true);
            else
                $('#enable-digital-code-for-application-number').prop('checked', false);

            $('#application-number-mask', myModal).val(columnValues[8]);
            $('#start-application-number', myModal).val(columnValues[10]);
            $('#end-application-number', myModal).val(columnValues[11]);
            $('#application-number-increment-by', myModal).val(columnValues[12]);
            $('#note-application', myModal).val(columnValues[13]);
            $('#reason-for-modification-application', myModal).val(columnValues[14]);

            // Show Modals
            myModal.modal({ show: true });
        } else {
            $('#btn-edit-application-number-dt').addClass('read-only');
            $('#application-number-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-application-number-modal').click(function (event) {
        if (IsValidApplicationNumberDataTableModal()) {
            debugger;
            row = applicationNumberDataTable.row.add([
                        tag,
                        applicationGeneralLedgerId,
                         applicationGeneralLedgerIdText,
                         enableAutoApplicationNumber,
                         enableReGenerateUnusedApplicationNumber,
                         enableRandomApplicationNumber,
                         enableCustomizeApplicationNumber,
                         enableDigitalCodeForApplicationNumber,
                         applicationNumberMask,
                         applicationNumberMaskText,
                         startApplicationNumber,
                         endApplicationNumber,
                         applicationNumberIncrementBy,
                         note,
                         reasonForModification
            ]).draw();

            // Error Message In Span
            $('#application-number-data-table-error').addClass('d-none');

            HideApplicationNumberDataTableColumns();

            applicationNumberDataTable.columns.adjust().draw();

            ClearModal('application-number');

            $('#application-number-modal').modal('hide');

            EnableNewOperation('application-number');
        }
    });

    // Modal update Button Event
    $('#btn-update-application-number-modal').click(function (event) {

        $('#select-all-application-number').prop('checked', false);
        if (IsValidApplicationNumberDataTableModal()) {
            applicationNumberDataTable.row(selectedRowIndex).data([
                         tag,
                         applicationGeneralLedgerId,
                         applicationGeneralLedgerIdText,
                         enableAutoApplicationNumber,
                         enableReGenerateUnusedApplicationNumber,
                         enableRandomApplicationNumber,
                         enableCustomizeApplicationNumber,
                         enableDigitalCodeForApplicationNumber,
                         applicationNumberMask,
                         applicationNumberMaskText,
                         startApplicationNumber,
                         endApplicationNumber,
                         applicationNumberIncrementBy,
                         note,
                         reasonForModification
            ]).draw();
            // Error Message In Span
            $('#application-number-validation span').html('');

            HideApplicationNumberDataTableColumns();

            applicationNumberDataTable.columns.adjust().draw();

            $('#application-number-modal').modal('hide');

            EnableNewOperation('application-number');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-application-number-dt').click(function (event) {
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($("#tbl-application-number tbody input[type='checkbox']:checked").each(function () {
                    applicationNumberDataTable.row($("#tbl-application-number tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-application-number-dt').data('rowindex');
                    EnableNewOperation('application-number');

                     SetApplicationGeneralLedgerTypeUniqueDropdownList();

                    $('#select-all-application-number').prop('checked', false);
                    // Display Error, If Table Has Not Any Record
                    if (!applicationNumberDataTable.data().any())
                    $('#application-number-data-table-error').removeClass('d-none');

                }));
            }
        } else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Menu Datatable
    $('#select-all-application-number').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-application-number tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = applicationNumberDataTable.row(row).index();

                rowData = (applicationNumberDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-application-number-dt').data('rowindex', arr);
                EnableDeleteOperation('application-number')
            });
        } else {
            EnableNewOperation('application-number')

            $('#tbl-application-number tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-application-number tbody').click("input[type=checkbox]", function () {
        $('#tbl-application-number input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = applicationNumberDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (applicationNumberDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('application-number');

                    $('#btn-update-application-number-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-application-number-dt').data('rowindex', rowData);
                    $('#btn-delete-application-number-dt').data('rowindex', arr);
                    $('#select-all-application-number').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-application-number tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('application-number');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('application-number');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('application-number');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-application-number').prop('checked', true);
        else
            $('#select-all-application-number').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-application-number > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (applicationNumberDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#application-general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
    });

    // Validate Fund Module
    function IsValidApplicationNumberDataTableModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        applicationGeneralLedgerId = $('#application-general-ledger-id option:selected').val();
        applicationGeneralLedgerIdText = $('#application-general-ledger-id option:selected').text();
        enableAutoApplicationNumber = $('#enable-auto-application-number').is(':checked');
        enableReGenerateUnusedApplicationNumber = $('#enable-regenerate-unused-application-number').is(':checked') ? 'True' : 'False';
        enableRandomApplicationNumber = $('#enable-random-application-number').is(':checked') ? 'True' : 'False';
        enableCustomizeApplicationNumber = $('#enable-customize-application-number').is(':checked') ? 'True' : 'False';
        enableDigitalCodeForApplicationNumber = $('#enable-digital-code-for-application-number').is(':checked') ? 'True' : 'False';

        applicationNumberMask = $('#application-number-mask option:selected')
         .map(function () {
             return $(this).val();
         }).get().join(',');

        applicationNumberMaskText = $('#application-number-mask option:selected')
         .map(function () {
             return $(this).text();
         }).get().join(',');

        startApplicationNumber = parseInt($('#start-application-number').val());
        endApplicationNumber = parseInt($('#end-application-number').val());
        applicationNumberIncrementBy = parseInt($('#application-number-increment-by').val());

        note = $('#note-application').val();
        reasonForModification = $('#reason-for-modification-application').val();

        if (note == '')
            note = 'None';

        if (reasonForModification == '')
            reasonForModification = 'None';

        if (applicationGeneralLedgerId == "") {
            result = false;
            $('#application-general-ledger-id-error').removeClass('d-none');
        } else
            $('#application-general-ledger-id-error').addClass('d-none');

        // Check If Auto Application Number Is Enabled
        if (enableAutoApplicationNumber) {

            if (applicationNumberMask === "") {
                result = false;
                $('#application-number-mask-error').removeClass('d-none');
            } else {
                $('#application-number-mask-error').addClass('d-none');
            }

            // Check if Start Application Number is a valid number
            if (isNaN(startApplicationNumber) === false) {

                minimum = parseInt($('#start-application-number').attr('min'));
                maximum = parseInt($('#start-application-number').attr('max'));

                if (parseInt(startApplicationNumber) < parseInt(minimum) || parseInt(startApplicationNumber) > parseInt(maximum)) {
                    result = false;
                    $('#start-application-number-error').removeClass('d-none');
                } else {
                    $('#start-application-number-error').addClass('d-none');
                }
            } else {
                // Show error message if startApplicationNumber is NaN
                result = false;
                $('#start-application-number-error').removeClass('d-none');
            }

            // Check if End Application Number is a valid number
            if (isNaN(endApplicationNumber) === false) {

                minimum = parseInt($('#end-application-number').attr('min'));
                maximum = parseInt($('#end-application-number').attr('max'));

                if (parseInt(endApplicationNumber) < parseInt(minimum) || parseInt(endApplicationNumber) > parseInt(maximum)) {
                    result = false;
                    $('#end-application-number-error').removeClass('d-none');
                } else {
                    $('#end-application-number-error').addClass('d-none');
                }
            } else {
                // Show error message if endApplicationNumber is NaN
                result = false;
                $('#end-application-number-error').removeClass('d-none');
            }

            // Check if Application Number Increment By is a valid number
            if (isNaN(applicationNumberIncrementBy) === false) {

                minimum = parseInt($('#application-number-increment-by').attr('min'));
                maximum = parseInt($('#application-number-increment-by').attr('max'));

                if (parseInt(applicationNumberIncrementBy) < parseInt(minimum) || parseInt(applicationNumberIncrementBy) > parseInt(maximum)) {
                    result = false;
                    $('#application-number-increment-by-error').removeClass('d-none');
                } else {
                    $('#application-number-increment-by-error').addClass('d-none');
                }
            } else {
                // Show error message if applicationNumberIncrementBy is NaN
                result = false;
                $('#application-number-increment-by-error').removeClass('d-none');
            }
        }

        else {
            applicationNumberMask = '00000000-0000-0000-0000-000000000000';
            applicationNumberMaskText = 'None';
            startApplicationNumber = 0;
            endApplicationNumber = 0;
            applicationNumberIncrementBy = 0;
        }

        return result;

    }

    // Hide Unnecessary Columns
    function HideApplicationNumberDataTableColumns() {
        applicationNumberDataTable.column(1).visible(false);
        applicationNumberDataTable.column(8).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Certificate Number - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#enable-auto-certificate-number').change(function () {
        debugger;
        if ($(this).is(':checked', true)) {
            $('#certificate-number-mask-error').addClass('d-none');
            $('#start-certificate-number-error').addClass('d-none');
            $('#end-certificate-number-error').addClass('d-none');
            $('#certificate-number-increment-by-error').addClass('d-none');
        }
    });

    // DataTable Add Button 
    $('#btn-add-certificate-number-dt').click(function () {
        debugger;
        event.preventDefault();
        editedCertificateGeneralLedgerId = '';

        SetCertificateGeneralLedgerTypeUniqueDropdownList();

        SetModalTitle('certificate-number', 'Add');

        enableAutoCertificateNumber = $('#enable-auto-certificate-number').is(':checked');
        if (!enableAutoCertificateNumber)
            $('#auto-certificate-number-block').addClass('d-none');
    });

    // DataTable Edit Button 
    $('#btn-edit-certificate-number-dt').click(function () {
        debugger;
        SetModalTitle('certificate-number', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            debugger;
            columnValues = $('#btn-edit-certificate-number-dt').data('rowindex');
            id = $('#certificate-number-modal').attr('id');
            myModal = $('#' + id).modal();

            editedCertificateGeneralLedgerId = columnValues[1];
            SetCertificateGeneralLedgerTypeUniqueDropdownList();
            // Display Value In Modal Inputs
            $('#certificate-general-ledger-id', myModal).val(columnValues[1]);

            if (columnValues[4] === 'True')
                $('#enable-regenerate-unused-certificate-number').prop('checked', true);
            else
                $('#enable-regenerate-unused-certificate-number').prop('checked', false);

            if (columnValues[3] === true)
                $('#enable-auto-certificate-number').prop('checked', true);
            else
                $('#enable-auto-certificate-number').prop('checked', false);

            if (columnValues[5] === 'True')
                $('#enable-random-certificate-number').prop('checked', true);
            else
                $('#enable-random-certificate-number').prop('checked', false);

            if (columnValues[6] === 'True')
                $('#enable-customize-certificate-number').prop('checked', true);
            else
                $('#enable-customize-certificate-number').prop('checked', false);

            if (columnValues[7] === 'True')
                $('#enable-digital-code-for-certificate-number').prop('checked', true);
            else
                $('#enable-digital-code-for-certificate-number').prop('checked', false);

            $('#certificate-number-mask', myModal).val(columnValues[8]);
            $('#start-certificate-number', myModal).val(columnValues[10]);
            $('#end-certificate-number', myModal).val(columnValues[11]);
            $('#certificate-number-increment-by', myModal).val(columnValues[12]);
            $('#note-certificate', myModal).val(columnValues[13]);
            $('#reason-for-modification-certificate', myModal).val(columnValues[14]);

            // Show Modals
            myModal.modal({ show: true });
        } else {
            $('#btn-edit-certificate-number-dt').addClass('read-only');
            $('#certificate-number-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-certificate-number-modal').click(function (event) {
        if (IsValidCertificateNumberDataTableModal()) {
            row = certificateNumberDataTable.row.add([
                        tag,
                        certificateGeneralLedgerId,
                        certificateGeneralLedgerIdText,
                        enableAutoCertificateNumber,
                        enableReGenerateUnusedCertificateNumber,
                        enableRandomCertificateNumber,
                        enableCustomizeCertificateNumber,
                        enableDigitalCodeForCertificateNumber,
                        certificateNumberMask,
                        certificateNumberMaskText,
                        startCertificateNumber,
                        endCertificateNumber,
                        certificateNumberIncrementBy,
                        note,
                        reasonForModification
            ]).draw();

            // Error Message In Span
            $('#certificate-number-data-table-error').addClass('d-none');

            HideCertificateNumberDataTableColumns();

            certificateNumberDataTable.columns.adjust().draw();

            ClearModal('certificate-number');

            $('#certificate-number-modal').modal('hide');

            EnableNewOperation('certificate-number');
        }
    });

    // Modal update Button Event
    $('#btn-update-certificate-number-modal').click(function (event) {

        $('#select-all-certificate-number').prop('checked', false);
        if (IsValidCertificateNumberDataTableModal()) {
            certificateNumberDataTable.row(selectedRowIndex).data([
                         tag,
                         certificateGeneralLedgerId,
                         certificateGeneralLedgerIdText,
                         enableAutoCertificateNumber,
                         enableReGenerateUnusedCertificateNumber,
                         enableRandomCertificateNumber,
                         enableCustomizeCertificateNumber,
                         enableDigitalCodeForCertificateNumber,
                         certificateNumberMask,
                         certificateNumberMaskText,
                         startCertificateNumber,
                         endCertificateNumber,
                         certificateNumberIncrementBy,
                         note,
                         reasonForModification
            ]).draw();
            HideCertificateNumberDataTableColumns();

            certificateNumberDataTable.columns.adjust().draw();

            $('#certificate-number-modal').modal('hide');

            EnableNewOperation('certificate-number');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-certificate-number-dt').click(function (event) {
        debugger;
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($("#tbl-certificate-number tbody input[type='checkbox']:checked").each(function () {
                    certificateNumberDataTable.row($("#tbl-certificate-number tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-certificate-number-dt').data('rowindex');
                    EnableNewOperation('certificate-number');

                     SetCertificateGeneralLedgerTypeUniqueDropdownList();

                    $('#select-all-certificate-number').prop('checked', false);
                    // Display Error, If Table Has Not Any Record
                    if (!certificateNumberDataTable.data().any())
                    $('#certificate-number-data-table-error').removeClass('d-none');

                }));
            }

        } else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Menu Datatable
    $('#select-all-certificate-number').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-certificate-number tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = certificateNumberDataTable.row(row).index();

                rowData = (certificateNumberDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-certificate-number-dt').data('rowindex', arr);
                EnableDeleteOperation('certificate-number')
            });
        } else {
            EnableNewOperation('certificate-number')

            $('#tbl-certificate-number tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-certificate-number tbody').click("input[type=checkbox]", function () {
        $('#tbl-certificate-number input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = certificateNumberDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (certificateNumberDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('certificate-number');

                    $('#btn-update-certificate-number-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-certificate-number-dt').data('rowindex', rowData);
                    $('#btn-delete-certificate-number-dt').data('rowindex', arr);
                    $('#select-all-certificate-number').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-certificate-number tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('certificate-number');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('certificate-number');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('certificate-number');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-certificate-number').prop('checked', true);
        else
            $('#select-all-certificate-number').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-certificate-number > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        selectedRowIndex = certificateNumberDataTable.row(currentRow).index();

        if (selectedRowIndex >= 0) {
            rowData = certificateNumberDataTable.row(selectedRowIndex).data();

            arr.push({ arrayCloumn1: rowData[1] });

            $('#select-all-certificate-number').data('rowindex', arr);
            EnableDeleteOperation('certificate-number');
        }
    });

    // Validate Certificate Module
    function IsValidCertificateNumberDataTableModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local variable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        certificateGeneralLedgerId = $('#certificate-general-ledger-id option:selected').val();
        certificateGeneralLedgerIdText = $('#certificate-general-ledger-id option:selected').text();
        enableAutoCertificateNumber = $('#enable-auto-certificate-number').is(':checked');
        enableReGenerateUnusedCertificateNumber = $('#enable-regenerate-unused-certificate-number').is(':checked') ? 'True' : 'False';
        enableRandomCertificateNumber = $('#enable-random-certificate-number').is(':checked') ? 'True' : 'False';
        enableCustomizeCertificateNumber = $('#enable-customize-certificate-number').is(':checked') ? 'True' : 'False';
        enableDigitalCodeForCertificateNumber = $('#enable-digital-code-for-certificate-number').is(':checked') ? 'True' : 'False';

        certificateNumberMask = $('#certificate-number-mask option:selected')
         .map(function () {
             return $(this).val();
         }).get().join(',');

        certificateNumberMaskText = $('#certificate-number-mask option:selected')
         .map(function () {
             return $(this).text();
         }).get().join(',');

        startCertificateNumber = parseInt($('#start-certificate-number').val());
        endCertificateNumber = parseInt($('#end-certificate-number').val());
        certificateNumberIncrementBy = parseInt($('#certificate-number-increment-by').val());

        note = $('#note-certificate').val();
        reasonForModification = $('#reason-for-modification-certificate').val();

        if (note == '')
            note = 'None';

        if (reasonForModification == '')
            reasonForModification = 'None';

        if (certificateGeneralLedgerId == "") {
            result = false;
            $('#certificate-general-ledger-id-error').removeClass('d-none');
        } else
            $('#certificate-general-ledger-id-error').addClass('d-none');

        // Check If Auto Certificate Number Is Enabled
        if (enableAutoCertificateNumber) {

            if (certificateNumberMask === "") {
                result = false;
                $('#certificate-number-mask-error').removeClass('d-none');
            } else {
                $('#certificate-number-mask-error').addClass('d-none');
            }

            // Check if Start Certificate Number is a valid number
            if (isNaN(startCertificateNumber) === false) {

                minimum = parseInt($('#start-certificate-number').attr('min'));
                maximum = parseInt($('#start-certificate-number').attr('max'));

                if (parseInt(startCertificateNumber) < parseInt(minimum) || parseInt(startCertificateNumber) > parseInt(maximum)) {
                    result = false;
                    $('#start-certificate-number-error').removeClass('d-none');
                } else {
                    $('#start-certificate-number-error').addClass('d-none');
                }
            } else {
                // Show error message if startCertificateNumber is NaN
                result = false;
                $('#start-certificate-number-error').removeClass('d-none');
            }

            // Check if End Certificate Number is a valid number
            if (isNaN(endCertificateNumber) === false) {

                minimum = parseInt($('#end-certificate-number').attr('min'));
                maximum = parseInt($('#end-certificate-number').attr('max'));

                if (parseInt(endCertificateNumber) < parseInt(minimum) || parseInt(endCertificateNumber) > parseInt(maximum)) {
                    result = false;
                    $('#end-certificate-number-error').removeClass('d-none');
                } else {
                    $('#end-certificate-number-error').addClass('d-none');
                }
            } else {
                // Show error message if endCertificateNumber is NaN
                result = false;
                $('#end-certificate-number-error').removeClass('d-none');
            }

            // Check if Certificate Number Increment By is a valid number
            if (isNaN(certificateNumberIncrementBy) === false) {

                minimum = parseInt($('#certificate-number-increment-by').attr('min'));
                maximum = parseInt($('#certificate-number-increment-by').attr('max'));

                if (parseInt(certificateNumberIncrementBy) < parseInt(minimum) || parseInt(certificateNumberIncrementBy) > parseInt(maximum)) {
                    result = false;
                    $('#certificate-number-increment-by-error').removeClass('d-none');
                } else {
                    $('#certificate-number-increment-by-error').addClass('d-none');
                }
            } else {
                // Show error message if certificateNumberIncrementBy is NaN
                result = false;
                $('#certificate-number-increment-by-error').removeClass('d-none');
            }
        }

        else {
            certificateNumberMask = '00000000-0000-0000-0000-000000000000';
            certificateNumberMaskText = 'None';
            startCertificateNumber = 0;
            endCertificateNumber = 0;
            certificateNumberIncrementBy = 0;
        }

        return result;

    }

    // Hide Unnecessary Columns
    function HideCertificateNumberDataTableColumns() {
        certificateNumberDataTable.column(1).visible(false);
        certificateNumberDataTable.column(8).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Passbook Number - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#enable-auto-passbook-number').change(function () {
        debugger;
        if ($(this).is(':checked', true)) {
            $('#passbook-number-mask-error').addClass('d-none');
            $('#start-passbook-number-error').addClass('d-none');
            $('#end-passbook-number-error').addClass('d-none');
            $('#passbook-number-increment-by-error').addClass('d-none');
        }
    });

    // DataTable Add Button 
    $('#btn-add-passbook-number-dt').click(function () {
        debugger;
        event.preventDefault();
        editedPassbookGeneralLedgerId = '';

        SetPassbookGeneralLedgerTypeUniqueDropdownList();

        SetModalTitle('passbook-number', 'Add');

        enableAutoPassbookNumber = $('#enable-auto-passbook-number').is(':checked');
        if (!enableAutoPassbookNumber)
            $('#auto-passbook-number-block').addClass('d-none');
    });

    // DataTable Edit Button 
    $('#btn-edit-passbook-number-dt').click(function () {
        debugger;
        SetModalTitle('passbook-number', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            debugger;
            columnValues = $('#btn-edit-passbook-number-dt').data('rowindex');
            id = $('#passbook-number-modal').attr('id');
            myModal = $('#' + id).modal();

            editedPassbookGeneralLedgerId = columnValues[1];
            SetPassbookGeneralLedgerTypeUniqueDropdownList();
            // Display Value In Modal Inputs
            $('#passbook-general-ledger-id', myModal).val(columnValues[1]);

            if (columnValues[4] === 'True')
                $('#enable-regenerate-unused-passbook-number').prop('checked', true);
            else
                $('#enable-regenerate-unused-passbook-number').prop('checked', false);

            if (columnValues[3] === true)
                $('#enable-auto-passbook-number').prop('checked', true);
            else
                $('#enable-auto-passbook-number').prop('checked', false);

            if (columnValues[5] === 'True')
                $('#enable-random-passbook-number').prop('checked', true);
            else
                $('#enable-random-passbook-number').prop('checked', false);

            if (columnValues[6] === 'True')
                $('#enable-customize-passbook-number').prop('checked', true);
            else
                $('#enable-customize-passbook-number').prop('checked', false);

            if (columnValues[7] === 'True')
                $('#enable-digital-code-for-passbook-number').prop('checked', true);
            else
                $('#enable-digital-code-for-passbook-number').prop('checked', false);

            $('#passbook-number-mask', myModal).val(columnValues[8]);
            $('#start-passbook-number', myModal).val(columnValues[10]);
            $('#end-passbook-number', myModal).val(columnValues[11]);
            $('#passbook-number-increment-by', myModal).val(columnValues[12]);
            $('#note-passbook', myModal).val(columnValues[13]);
            $('#reason-for-modification-passbook', myModal).val(columnValues[14]);

            // Show Modals
            myModal.modal({ show: true });
        } else {
            $('#btn-edit-passbook-number-dt').addClass('read-only');
            $('#passbook-number-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-passbook-number-modal').click(function (event) {
        if (IsValidPassbookNumberDataTableModal()) {
            row = passbookNumberDataTable.row.add([
                        tag,
                        passbookGeneralLedgerId,
                        passbookGeneralLedgerIdText,
                        enableAutoPassbookNumber,
                        enableReGenerateUnusedPassbookNumber,
                        enableRandomPassbookNumber,
                        enableCustomizePassbookNumber,
                        enableDigitalCodeForPassbookNumber,
                        passbookNumberMask,
                        passbookNumberMaskText,
                        startPassbookNumber,
                        endPassbookNumber,
                        passbookNumberIncrementBy,
                        note,
                        reasonForModification
            ]).draw();

            // Error Message In Span
            $('#passbook-number-data-table-error').addClass('d-none');

            HidePassbookNumberDataTableColumns();

            passbookNumberDataTable.columns.adjust().draw();

            ClearModal('passbook-number');

            $('#passbook-number-modal').modal('hide');

            EnableNewOperation('passbook-number');
        }
    });

    // Modal update Button Event
    $('#btn-update-passbook-number-modal').click(function (event) {

        $('#select-all-passbook-number').prop('checked', false);
        if (IsValidPassbookNumberDataTableModal()) {
            passbookNumberDataTable.row(selectedRowIndex).data([
                         tag,
                         passbookGeneralLedgerId,
                         passbookGeneralLedgerIdText,
                         enableAutoPassbookNumber,
                         enableReGenerateUnusedPassbookNumber,
                         enableRandomPassbookNumber,
                         enableCustomizePassbookNumber,
                         enableDigitalCodeForPassbookNumber,
                         passbookNumberMask,
                         passbookNumberMaskText,
                         startPassbookNumber,
                         endPassbookNumber,
                         passbookNumberIncrementBy,
                         note,
                         reasonForModification
            ]).draw();
            HidePassbookNumberDataTableColumns();

            passbookNumberDataTable.columns.adjust().draw();

            $('#passbook-number-modal').modal('hide');

            EnableNewOperation('passbook-number');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-passbook-number-dt').click(function (event) {
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($("#tbl-passbook-number tbody input[type='checkbox']:checked").each(function () {
                    passbookNumberDataTable.row($("#tbl-passbook-number tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-passbook-number-dt').data('rowindex');
                    EnableNewOperation('passbook-number');

                     SetPassbookGeneralLedgerTypeUniqueDropdownList();

                    $('#select-all-passbook-number').prop('checked', false);
                    // Display Error, If Table Has Not Any Record
                    if (!passbookNumberDataTable.data().any())
                    $('#passbook-number-data-table-error').removeClass('d-none');

                }));
            }
        } else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Menu Datatable
    $('#select-all-passbook-number').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-passbook-number tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = passbookNumberDataTable.row(row).index();

                rowData = (passbookNumberDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-passbook-number-dt').data('rowindex', arr);
                EnableDeleteOperation('passbook-number')
            });
        } else {
            EnableNewOperation('passbook-number')

            $('#tbl-passbook-number tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-passbook-number tbody').click("input[type=checkbox]", function () {
        $('#tbl-passbook-number input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = passbookNumberDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (passbookNumberDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('passbook-number');

                    $('#btn-update-passbook-number-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-passbook-number-dt').data('rowindex', rowData);
                    $('#btn-delete-passbook-number-dt').data('rowindex', arr);
                    $('#select-all-passbook-number').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-passbook-number tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('passbook-number');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('passbook-number');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('passbook-number');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-passbook-number').prop('checked', true);
        else
            $('#select-all-passbook-number').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-passbook-number > tbody > tr').each(function () {
        selectedRowIndex = $(this).find('input[type=checkbox]').prop('checked');

        if (selectedRowIndex) {
            selectedRowIndex = $(this).find('input[type=checkbox]').closest('tr');

            rowData = passbookNumberDataTable.row(selectedRowIndex).data();

            arr.push({ arrayCloumn1: rowData[1] });

            $('#btn-delete-passbook-number-dt').data('rowindex', arr);
            EnableDeleteOperation('passbook-number');
        }
    });

    // Validate Passbook Module
    function IsValidPassbookNumberDataTableModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local variable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        passbookGeneralLedgerId = $('#passbook-general-ledger-id option:selected').val();
        passbookGeneralLedgerIdText = $('#passbook-general-ledger-id option:selected').text();
        enableAutoPassbookNumber = $('#enable-auto-passbook-number').is(':checked');
        enableReGenerateUnusedPassbookNumber = $('#enable-regenerate-unused-passbook-number').is(':checked') ? 'True' : 'False';
        enableRandomPassbookNumber = $('#enable-random-passbook-number').is(':checked') ? 'True' : 'False';
        enableCustomizePassbookNumber = $('#enable-customize-passbook-number').is(':checked') ? 'True' : 'False';
        enableDigitalCodeForPassbookNumber = $('#enable-digital-code-for-passbook-number').is(':checked') ? 'True' : 'False';

        passbookNumberMask = $('#passbook-number-mask option:selected')
         .map(function () {
             return $(this).val();
         }).get().join(',');

        passbookNumberMaskText = $('#passbook-number-mask option:selected')
         .map(function () {
             return $(this).text();
         }).get().join(',');

        startPassbookNumber = parseInt($('#start-passbook-number').val());
        endPassbookNumber = parseInt($('#end-passbook-number').val());
        passbookNumberIncrementBy = parseInt($('#passbook-number-increment-by').val());

        note = $('#note-passbook').val();
        reasonForModification = $('#reason-for-modification-passbook').val();

        if (note == '')
            note = 'None';

        if (reasonForModification == '')
            reasonForModification = 'None';

        if (passbookGeneralLedgerId == "") {
            result = false;
            $('#passbook-general-ledger-id-error').removeClass('d-none');
        } else
            $('#passbook-general-ledger-id-error').addClass('d-none');

        // Check If Auto Passbook Number Is Enabled
        if (enableAutoPassbookNumber) {

            if (passbookNumberMask === "") {
                result = false;
                $('#passbook-number-mask-error').removeClass('d-none');
            } else {
                $('#passbook-number-mask-error').addClass('d-none');
            }

            // Check if Start Passbook Number is a valid number
            if (isNaN(startPassbookNumber) === false) {

                minimum = parseInt($('#start-passbook-number').attr('min'));
                maximum = parseInt($('#start-passbook-number').attr('max'));

                if (parseInt(startPassbookNumber) < parseInt(minimum) || parseInt(startPassbookNumber) > parseInt(maximum)) {
                    result = false;
                    $('#start-passbook-number-error').removeClass('d-none');
                } else {
                    $('#start-passbook-number-error').addClass('d-none');
                }
            } else {
                // Show error message if startPassbookNumber is NaN
                result = false;
                $('#start-passbook-number-error').removeClass('d-none');
            }

            // Check if End Passbook Number is a valid number
            if (isNaN(endPassbookNumber) === false) {

                minimum = parseInt($('#end-passbook-number').attr('min'));
                maximum = parseInt($('#end-passbook-number').attr('max'));

                if (parseInt(endPassbookNumber) < parseInt(minimum) || parseInt(endPassbookNumber) > parseInt(maximum)) {
                    result = false;
                    $('#end-passbook-number-error').removeClass('d-none');
                } else {
                    $('#end-passbook-number-error').addClass('d-none');
                }
            } else {
                // Show error message if endPassbookNumber is NaN
                result = false;
                $('#end-passbook-number-error').removeClass('d-none');
            }

            // Check if Passbook Number Increment By is a valid number
            if (isNaN(passbookNumberIncrementBy) === false) {

                minimum = parseInt($('#passbook-number-increment-by').attr('min'));
                maximum = parseInt($('#passbook-number-increment-by').attr('max'));

                if (parseInt(passbookNumberIncrementBy) < parseInt(minimum) || parseInt(passbookNumberIncrementBy) > parseInt(maximum)) {
                    result = false;
                    $('#passbook-number-increment-by-error').removeClass('d-none');
                } else {
                    $('#passbook-number-increment-by-error').addClass('d-none');
                }
            } else {
                // Show error message if passbookNumberIncrementBy is NaN
                result = false;
                $('#passbook-number-increment-by-error').removeClass('d-none');
            }
        }

        else {
            passbookNumberMask = '00000000-0000-0000-0000-000000000000';
            passbookNumberMaskText = 'None';
            startPassbookNumber = 0;
            endPassbookNumber = 0;
            passbookNumberIncrementBy = 0;
        }

        return result;

    }

    // Hide Unnecessary Columns
    function HidePassbookNumberDataTableColumns() {
        passbookNumberDataTable.column(1).visible(false);
        passbookNumberDataTable.column(8).visible(false);
    }



    // @@@@@@@@@@@@@@@@@@@@@@@@@@@currency- DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-currency-dt').click(function () {
        event.preventDefault();
        SetModalTitle('currency', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-currency-dt').click(function () {
        SetModalTitle('currency', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-currency-dt').data('rowindex');
            id = $('#currency-modal').attr('id');
            myModal = $('#' + id).modal();

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
                let arr = datepart.split('-');

                let dd = arr[0];
                let mm = arr[1];
                let yyyy = arr[2];

                activationDate = [yyyy, mm, dd].join('-');
            }
            else
                activationDate = [year, month, date].join('-');


            // Get Only Expiry Date
            datepart = columnValues[4].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[4]
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
            datepart = columnValues[5].split(' ')[0];

            if (datepart.length == 0) {
                datepart = columnValues[5]
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

                closeDate = [yyyy, mm, dd].join('-');
            }
            else
                closeDate = [year2, month2, date2].join('-');


            // Display Value In Modal Inputs
            $('#business-office-currency-id', myModal).val(columnValues[1]);
            $('#activation-date-currency', myModal).val(activationDate);
            $('#expiry-date-currency', myModal).val(expiryDate);
            $('#close-date-currency', myModal).val(closeDate);
            $('#note-currency', myModal).val(columnValues[6]);
            $('#reason-for-modification-currency', myModal).val(columnValues[7]);



            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-currency-dt').addClass('read-only');
            $('#currency-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-currency-modal').click(function (event) {
        if (IsValidCurrencyDataTableModal()) {
            row = currencyDataTable.row.add([
                        tag,
                        currencyId,
                        currencyIdText,
                        activationDate,
                        expiryDate,
                        closeDate,
                        note,
                        reasonForModification,
            ]).draw();

            // Error Message In Span
            $('#currency-data-table-error').addClass('d-none');

            HideCurrencyDataTableColumns();

            currencyDataTable.columns.adjust().draw();

            ClearModal('currency');

            $('#currency-modal').modal('hide');

            EnableNewOperation('currency');
        }
    });

    // Modal update Button Event
    $('#btn-update-currency-modal').click(function (event) {

        $('#select-all-currency').prop('checked', false);
        if (IsValidCurrencyDataTableModal()) {
            currencyDataTable.row($(this).attr('rowindex')).data([
                        tag,
                        currencyId,
                        currencyIdText,
                        activationDate,
                        expiryDate,
                        closeDate,
                        note,
                        reasonForModification,
            ]).draw();
            // Error Message In Span
            $('#currency-validation span').html('');

            HideCurrencyDataTableColumns();

            currencyDataTable.columns.adjust().draw();

            $('#currency-modal').modal('hide');

            EnableNewOperation('currency');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-currency-dt').click(function (event) {
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-currency tbody input[type="checkbox"]:checked').each(function () {
                    currencyDataTable.row($('#tbl-currency tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-currency-dt').data('rowindex');
                    EnableNewOperation('currency');

                    $('#select-all-currency').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (!currencyDataTable.data().any())
                    $('#currency-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event -   
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
        isCheckedAll = $('#tbl-currency tbody input[type="checkbox"]');

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
    $('#tbl-currency > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (currencyDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#business-office-currency-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate  Fund Module
    function IsValidCurrencyDataTableModal() {

        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        currencyId = $('#business-office-currency-id option:selected').val();
        currencyIdText = $('#business-office-currency-id option:selected').text();
        activationDate = $('#activation-date-currency').val();
        expiryDate = $('#expiry-date-currency').val();
        closeDate = $('#close-date-currency').val();
        note = $('#note-currency').val();
        reasonForModification = $('#reason-for-modification-currency').val();

        if (note == "")
            note = "None";

        if (reasonForModification == "")
            reasonForModification = "None";


        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-currency');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-currency');

        if (currencyId == "") {
            result = false;
            $('#business-office-currency-id-error').removeClass('d-none');
        } else
            $('#business-office-currency-id-error').addClass('d-none');

        if (!isValidActivationDate) {
            result = false;
            $('#activation-date-currency-error').removeClass('d-none');
        } else
            $('#activation-date-currency-error').addClass('d-none');

        if (!isValidExpiryDate) {
            result = false;
            $('#expiry-date-currency-error').removeClass('d-none');
        } else
            $('#expiry-date-currency-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideCurrencyDataTableColumns() {
        currencyDataTable.column(1).visible(false);
    }

    // Page Loading Default Values
    function SetPageLoadingDefaultValues() {
        debugger;
        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();
    }


    // Application General Ledger Type Unique Dropdown
    function SetApplicationGeneralLedgerTypeUniqueDropdownList() {
        // Show All List Items
        $('#application-general-ledger-id').html('');
        $('#application-general-ledger-id').append(APPLICATION_GENERAL_LEDGER_DROPDOWN_LIST);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-application-number > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (applicationNumberDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null) {
                if (myColumnValues[1] != editedApplicationGeneralLedgerId)
                    $('#application-general-ledger-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
    }

    // Account General Ledger Type Unique Dropdown
    function SetAccountGeneralLedgerTypeUniqueDropdownList() {
        // Show All List Items
        $('#account-general-ledger-id').html('');
        $('#account-general-ledger-id').append(ACCOUNT_GENERAL_LEDGER_DROPDOWN_LIST);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-account-number > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (accountNumberDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null) {
                if (myColumnValues[1] != editedAccountGeneralLedgerId)
                    $('#account-general-ledger-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
    }

    // Agreement General Ledger Type Unique Dropdown
    function SetAgreementGeneralLedgerTypeUniqueDropdownList() {
        // Show All List Items
        $('#agreement-general-ledger-id').html('');
        $('#agreement-general-ledger-id').append(AGREEMENT_GENERAL_LEDGER_DROPDOWN_LIST);

        // Hide Added Joint Agreement DropdownList Items
        $('#tbl-agreement-number > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (agreementNumberDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null) {
                if (myColumnValues[1] != editedAgreementGeneralLedgerId)
                    $('#agreement-general-ledger-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
    }

    // Account General Ledger Type Unique Dropdown
    function SetCertificateGeneralLedgerTypeUniqueDropdownList() {
        // Show All List Items
        $('#certificate-general-ledger-id').html('');
        $('#certificate-general-ledger-id').append(CERTIFICATE_GENERAL_LEDGER_DROPDOWN_LIST);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-certificate-number > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (certificateNumberDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null) {
                if (myColumnValues[1] != editedCertificateGeneralLedgerId)
                    $('#certificate-general-ledger-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
    }

    // Account General Ledger Type Unique Dropdown
    function SetPassbookGeneralLedgerTypeUniqueDropdownList() {
        // Show All List Items
        $('#passbook-general-ledger-id').html('');
        $('#passbook-general-ledger-id').append(PASSBOOK_GENERAL_LEDGER_DROPDOWN_LIST);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-passbook-number > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (passbookNumberDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null) {
                if (myColumnValues[1] != editedPassbookGeneralLedgerId)
                    $('#passbook-general-ledger-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;

        // not add event.preventDefault
        if ($('form').valid()) {
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let passwordPolicyArray = new Array();
            let menuArray = new Array();
            let specialPermissionArray = new Array();
            let transactionLimitArray = new Array();
            let accountNumberArray = new Array();
            let applicationNumberArray = new Array();
            let certificateNumberArray = new Array();
            let currencyArray = new Array();
            let passbookNumberArray = new Array();

            // Normal Accordion-1 Validation
            if (!IsValidBusinessOfficeDetailAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-2 Validation
            if (!IsValidCooperativeRegistrationAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-3 Validation
            if (!IsValidTransactionParameterAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-4 Validation
            if (!IsValidMemberNumberAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-5 Validation
            if (!IsValidPersonInformationNumberAccordionInputs()) {
                isValidAllInputs = false;
            }


            // Normal Accordion-6 Validation
            if (!IsValidSharesCertificateNumberAccordionInputs()) {
                isValidAllInputs = false;
            }


            // Get Data Table Values In Business Office Password Policy  Array
            if (!$('#heading-password-policy').hasClass('d-none')) {
                if (passwordPolicyDataTable.data().any()) {
                    $('#password-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-password TBODY TR').each(function () {
                            currentRow = $(this).closest("tr");

                            columnValues = (passwordPolicyDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                debugger;
                                passwordPolicyArray.push(
                                    {
                                        "PasswordPolicyId": columnValues[1],
                                        "ActivationDate": columnValues[3],
                                        "ExpiryDate": columnValues[4],
                                        "CloseDate": columnValues[5],
                                        "Note": columnValues[6],
                                    });
                            }
                            else
                                return false;

                        });
                    }
                }
                else {
                    $('#password-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Get Data Table Values In Business Office Menu Array
            if (!$('#heading-menu').hasClass('d-none')) {
                if (menuDataTable.data().any()) {
                    $('#menu-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {


                        $('#tbl-menu TBODY TR').each(function () {

                            currentRow = $(this).closest("tr");

                            columnValues = (menuDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                menuArray.push(
                                    {
                                        'MenuId': columnValues[1],
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
                    $('#menu-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Get Data Table Values In Business Office Special Permission Arraysss
            if (!$('#heading-special-permission').hasClass('d-none')) {
                if (specialPermissionDataTable.data().any()) {
                    $('#special-permission-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $("#tbl-special-permission TBODY TR").each(function () {
                            currentRow = $(this).closest("tr");

                            columnValues = (specialPermissionDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {

                                specialPermissionArray.push(
                                    {
                                        'SpecialPermissionId': columnValues[1],
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
                    $('#special-permission-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Get Data Table Values In Business Office Transaction Limit  Array
            if (!$('#heading-transaction-limit').hasClass('d-none')) {
                if (transactionLimitDataTable.data().any()) {
                    $('#transaction-limit-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {


                        $('#tbl-transaction-limit TBODY TR').each(function () {
                            debugger;
                            currentRow = $(this).closest("tr");

                            columnValues = (transactionLimitDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {

                                transactionLimitArray.push(
                                    {
                                        'GeneralLedgerId': columnValues[1],
                                        'TransactionTypeId': columnValues[3],
                                        'CurrencyId': columnValues[5],
                                        'MinimumAmountLimitForTransaction': columnValues[7],
                                        'MaximumAmountLimitForTransaction': columnValues[8],
                                        'MaximumNumberOfBackDaysForTransaction': columnValues[9],
                                        'MinimumAmountLimitForVerification': columnValues[10],
                                        'MaximumAmountLimitForVerification': columnValues[11],
                                        'MaximumNumberOfBackDaysForVerification': columnValues[12],
                                        'MinimumAmountLimitForAutoVerification': columnValues[13],
                                        'MaximumAmountLimitForAutoVerification': columnValues[14],
                                        'MaximumNumberOfBackDaysForAutoVerification': columnValues[15],
                                        'EnableCashDenomination': columnValues[16],
                                        'ActivationDate': columnValues[17],
                                        'ExpiryDate': columnValues[18],
                                        'CloseDate': columnValues[19],
                                        'Note': columnValues[20],
                                        'ReasonForModification': columnValues[21]
                                    });
                            }
                            else
                                return false;

                        });
                    }
                }
                else {
                    $('#transaction-limit-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Get Data Table Values In Business Office Account Number  Array
            if (!$('#heading-account-number').hasClass('d-none')) {
                if (accountNumberDataTable.data().any()) {
                    $('#account-number-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {


                        $('#tbl-account-number TBODY TR').each(function () {
                            currentRow = $(this).closest("tr");

                            columnValues = (accountNumberDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {

                                accountNumberArray.push(
                                    {
                                        'GeneralLedgerId': columnValues[1],
                                        'EnableAutoAccountNumber': columnValues[3],
                                        'EnableReGenerateUnusedAccountNumber': columnValues[4],
                                        'EnableRandomAccountNumber': columnValues[5],
                                        'EnableCustomizeAccountNumber': columnValues[6],
                                        'EnableDigitalCodeForAccountNumber': columnValues[7],
                                        'AccountNumberMask': columnValues[8],
                                        'StartAccountNumber': columnValues[10],
                                        'EndAccountNumber': columnValues[11],
                                        'AccountNumberIncrementBy': columnValues[12],
                                        'Note': columnValues[13]
                                    });
                            }
                            else
                                return false;

                        });
                    }
                }
                else {
                    $('#account-number-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Get Data Table Values In Business Office Account Number  Array
            if (!$('#heading-agreement-number').hasClass('d-none')) {
                if (agreementNumberDataTable.data().any()) {
                    $('#agreement-number-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {


                        $('#tbl-agreement-number TBODY TR').each(function () {
                            currentRow = $(this).closest("tr");

                            columnValues = (agreementNumberDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {

                                agreementNumberArray.push(
                                    {
                                        'GeneralLedgerId': columnValues[1],
                                        'EnableAutoAgreementNumber': columnValues[3],
                                        'EnableReGenerateUnusedAgreementNumber': columnValues[4],
                                        'EnableRandomAgreementNumber': columnValues[5],
                                        'EnableCustomizeAgreementNumber': columnValues[6],
                                        'EnableDigitalCodeForAgreementNumber': columnValues[7],
                                        'AgreementNumberMask': columnValues[8],
                                        'StartAgreementNumber': columnValues[10],
                                        'EndAgreementNumber': columnValues[11],
                                        'AgreementNumberIncrementBy': columnValues[12],
                                        'Note': columnValues[13]
                                    });
                            }
                            else
                                return false;

                        });
                    }
                }
                else {
                    $('#agreement-number-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Get Data Table Values In Business Office Certificate Number Array
            if (!$('#heading-certificate-number').hasClass('d-none')) {
                if (certificateNumberDataTable.data().any()) {
                    $('#certificate-number-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {
                        $('#tbl-certificate-number TBODY TR').each(function () {
                            currentRow = $(this).closest("tr");

                            columnValues = (certificateNumberDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {

                                certificateNumberArray.push(
                                    {
                                        'GeneralLedgerId': columnValues[1],
                                        'EnableAutoCertificateNumber': columnValues[3],
                                        'EnableReGenerateUnusedCertificateNumber': columnValues[4],
                                        'EnableRandomCertificateNumber': columnValues[5],
                                        'EnableCustomizeCertificateNumber': columnValues[6],
                                        'EnableDigitalCodeForCertificateNumber': columnValues[7],
                                        'CertificateNumberMask': columnValues[8],
                                        'StartCertificateNumber': columnValues[10],
                                        'EndCertificateNumber': columnValues[11],
                                        'CertificateNumberIncrementBy': columnValues[12],
                                        'Note': columnValues[13]
                                    });
                            }
                            else
                                return false;

                        });
                    }
                }
                else {
                    $('#certificate-number-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }


            // Get Data Table Values In Business Office Application Number  Array
            if (!$('#heading-application-number').hasClass('d-none')) {
                if (applicationNumberDataTable.data().any()) {
                    $('#application-number-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {
                        $('#tbl-application-number TBODY TR').each(function () {

                            currentRow = $(this).closest("tr");

                            columnValues = (applicationNumberDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {

                                applicationNumberArray.push(
                                    {
                                        'GeneralLedgerId': columnValues[1],
                                        'EnableAutoApplicationNumber': columnValues[3],
                                        'EnableReGenerateUnusedApplicationNumber': columnValues[4],
                                        'EnableRandomApplicationNumber': columnValues[5],
                                        'EnableCustomizeApplicationNumber': columnValues[6],
                                        'EnableDigitalCodeForApplicationNumber': columnValues[7],
                                        'ApplicationNumberMask': columnValues[8],
                                        'StartApplicationNumber': columnValues[10],
                                        'EndApplicationNumber': columnValues[11],
                                        'ApplicationNumberIncrementBy': columnValues[12],
                                        'Note': columnValues[13]
                                    });
                            }
                            else
                                return false;

                        });
                    }
                }
                else {
                    $('#application-number-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Get Data Table Values In Business Office Passbook Number Array
            if (!$('#heading-passbook-number').hasClass('d-none')) {
                if (passbookNumberDataTable.data().any()) {
                    $('#passbook-number-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {
                        $('#tbl-passbook-number TBODY TR').each(function () {

                            currentRow = $(this).closest("tr");

                            columnValues = (passbookNumberDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {

                                passbookNumberArray.push(
                                    {
                                        'GeneralLedgerId': columnValues[1],
                                        'EnableAutoPassbookNumber': columnValues[3],
                                        'EnableReGenerateUnusedPassbookNumber': columnValues[4],
                                        'EnableRandomPassbookNumber': columnValues[5],
                                        'EnableCustomizePassbookNumber': columnValues[6],
                                        'EnableDigitalCodeForPassbookNumber': columnValues[7],
                                        'PassbookNumberMask': columnValues[8],
                                        'StartPassbookNumber': columnValues[10],
                                        'EndPassbookNumber': columnValues[11],
                                        'PassbookNumberIncrementBy': columnValues[12],
                                        'Note': columnValues[13]
                                    });
                            }
                            else
                                return false;

                        });
                    }
                }
                else {
                    $('#passbook-number-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }


            // Get Data Table Values In Business Office Currency Array
            if (!$('#heading-currency').hasClass('d-none')) {
                if (currencyDataTable.data().any()) {
                    $('#currency-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {


                        $('#tbl-currency  TBODY TR').each(function () {
                            currentRow = $(this).closest("tr");

                            columnValues = (currencyDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {

                                currencyArray.push(
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
                    $('#currency-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            if (isValidAllInputs) {
                debugger
                $.ajax({
                    url: saveDataTableUrl,
                    type: 'POST',
                    data: { '_businessOfficePasswordPolicy': passwordPolicyArray, '_businessOfficeMenu': menuArray, '_businessOfficeSpecialPermission': specialPermissionArray, '_businessOfficeTransactionLimit': transactionLimitArray, '_businessOfficeAccountNumber': accountNumberArray, '_businessOfficeCertificateNumber': certificateNumberArray, '_businessOfficeApplicationNumber': applicationNumberArray, '_businessOfficeAgreementNumber': agreementNumberArray, '_businessOfficePassbookNumber': passbookNumberArray, '_businessOfficeCurrency': currencyArray },
                    ContentType: "application/json; charset=utf-8",
                    dataType: "JSON",

                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert("An Error Has Occured While Save Data In Contact Details DataTable!!! Error Message - " + error.toString());
                    }
                })

            }
            else {
                alert('Oops, Something Went Wrong!, Please Provide Valid Data...........');
                event.preventDefault();
            }
        }
        else {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data....');
            event.preventDefault();
        }

    });
});