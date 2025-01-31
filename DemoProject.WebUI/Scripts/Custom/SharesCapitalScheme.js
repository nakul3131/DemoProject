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

'use strict'
$(document).ready(function () {
    const FLAT_AMOUNT = 'F';
    const NUMBER_UNIT = 'N';


    // Constant For Dropdown
    const BUSINESS_OFFICE_DROPDOWN = $('#business-office-id').html();
    const BUSINESS_OFFICE_DROPDOWN_MULTI_SELECT_LIST = $('#business-office-id-multi-select-ul').html();
    const REPORT_FORMAT_DROPDOWN = $('#report-format-id').html();
    const GENERAL_LEDGER_DROPDOWN = $('#scheme-general-ledger-id').html();

    // DECLARATION - OF PAGE GLOBAL VARIABLE
    let isValidSchemeName = true;
    let note = '';
    let closeDate = '';
    let datepart = '';
    let activationDate = '';
    let expiryDate = '';
    let isVisibleNoticeSchedule = false;
    let isAmendView = false;
    let isVerifyView = false;

    // @@@@@@@@@@ Data Table Related Varible Declaration
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

    let arr = new Array();

    let minimum = 0;
    let maximum = 0;
    let multiSelectCount = 0;
    let maxTurnOverLimit = 0;

    //ClosingChares
    let fromTimePeriodInDays;
    let toTimePeriodInDays;
    let isApplicableOnDeath = false;

    // Transfer Charges
    let chargesGeneralLedgerId = '';
    let chargesGeneralLedgerText = '';
    let chargesBase;
    let chargesBaseText = '';
    let minimumChargesAmount = 0;
    let maximumChargesAmount = 0;
    let isTaxable = false;

    let result = true;

    //NoticeSchedule
    let noticeTypeId = '';
    let noticeScheduleText = '';
    let communicationMediaId = '';
    let communicationMediaText = '';
    let scheduleId = '';
    let scheduleText = '';

    //ReportFormat
    let reportFormatId = '';
    let reportFormatText = '';
    let editedReportFormatId = '';

    // business office                           
    let businessOfficeId = '';
    let businessOfficeText = '';
    let editedBusinessOfficeId = '';

    // generalLedger
    let generalLedgerId = '';
    let generalLedgerIdText = '';
    let editedGeneralLedgerId = '';

    // M A I N     P A G E     I N P U T     V A L I D A T I O N 
    let url = window.location.href;
    let index = url.lastIndexOf('/') + 1;
    let filenameWithExtension = url.substr(index);
    let filename = filenameWithExtension.split('?')[0]; // <-- added this line
    let winname = filename;

    // Create DataTables
    let closingChargesDataTable = CreateDataTable('closing-charges');
    let transferChargesDataTable = CreateDataTable('transfer-charges');
    let reportTypeFormatDataTable = CreateDataTable('report-format');
    let noticeScheduleDataTable = CreateDataTable('notice-schedule');
    let generalLedgerDataTable = CreateDataTable('general-ledger');
    let businessOfficeDataTable = CreateDataTable('business-office');

    // Load Default Values Of Pages (On Amend, Modify, Verify Operation)
    SetPageLoadingDefaultValues();

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   Change Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Application Number Inputs Show/Hide Based On Toogle Switch Selection
    // Require Extra Code To Hide Extra Code Block
    $('#enable-application').change(function (event) {
        $('#auto-application-number-block').addClass('d-none');
        $('#application-number-branchwise-block').removeClass('d-none');
    });

    $('#enable-application-number-branchwise').change(function () {
        $('#application-number-accordion-title-error').addClass('d-none');
        $('#auto-application-number-block').addClass('d-none');
    })

    // Enable Member Number Branchwise Inputs Show/Hide Based On Toggle Switch Selection
    $('#enable-member-number-branchwise').change(function () {
        $('#member-number-accordion-error').addClass('d-none');
        $('#auto-member-number-block').addClass('d-none');
    })

    // Enable Account Number Branchwise Inputs Show/Hide Based On Toggle Switch Selection
    $('#enable-account-number-branchwise').change(function () {
        $('#account-number-accordion-error').addClass('d-none');
        $('#auto-account-number-block').addClass('d-none');
    })

    // Shares Certificate Number Inputs Show/Hide Based On Toogle Switch Selection
    $('#enable-certificate-number-branchwise').change(function () {
        $('#certificate-number-accordion-error').addClass('d-none');
        $('#auto-certificate-number-block').addClass('d-none');
    })

    // Shares Certificate Number Inputs Show/Hide Based On Toogle Switch Selection
    $('#enable-nominee-shares-holding-percentage').change(function () {
        $('#nominee-shares-holding-percentage-block').addClass('d-none');
    })

    // Shares Certificate Number Inputs Show/Hide Based On Toogle Switch Selection
    $('#enable-number-of-nominee-limit').change(function () {
        $('#number-Of-nominee-limit-block').addClass('d-none');
    })

    // Shares Certificate Number Inputs Show/Hide Based On Toogle Switch Selection
    $('#enable-nominee-shares-holding-percentage').change(function () {
        $('#nominee-shares-holding-percentage-block').addClass('d-none');
    });

    // Enable Closing Charges Datatable Clear
    $('#enable-closing-charges').change(function () {
        closingChargesDataTable.clear().draw();
    });

    // Enable Transfer Charges Datatable Clear
    $('#enable-transfer-charges').change(function () {
        transferChargesDataTable.clear().draw();
    });

    // Round Method If No Rounding Then Hide Nearest
    $('.round-method').change(function () {
        NoRoundingChangeEventFunction();

    })

    // Notice Schedule Visible If & Only If Visible Any One Of SMS / Email
    $('.notice-schedule').change(function () {
        isVisibleNoticeSchedule = $('#enable-sms-service').is(':checked') || $('#enable-email-service').is(':checked') ? true : false;

        if (isVisibleNoticeSchedule)
            $('#notice-schedule-card').removeClass('d-none');
        else
            $('#notice-schedule-card').addClass('d-none');
    });

    // Number Of Joint Account Holder Inputs Show/Hide Based On Selection
    $('#enable-number-of-joint-account-holding-limit').change(function () {
        if ($(this).is(':checked')) {
            $('.joint-account-holder').removeClass('d-none');

            $('#minimum-joint-account-holder').val('');
            $('#maximum-joint-account-holder').val('');
            $('#default-joint-account-holder').val('');
        }
        else {
            $('.joint-account-holder').addClass('d-none');

            $('#minimum-joint-account-holder').val(0);
            $('#maximum-joint-account-holder').val(0);
            $('#default-joint-account-holder').val(0);
        }
    })

    // Nominee Details Show / Hide Based On Toggle Switch Selection
    $('#enable-number-of-nominee-limit').change(function () {
        if ($(this).is(':checked')) {
            $('.number-Of-nominee-limit').removeClass('d-none');

            $('#minimum-nominee').val('');
            $('#maximum-nominee').val('');
            $('#default-nominee').val('');
        }
        else {
            $('.number-Of-nominee-limit').addClass('d-none');

            $('#minimum-nominee').val(0);
            $('#maximum-nominee').val(0);
            $('#default-nominee').val(0);
        }
    })


    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // -- Name Of Scheme Validation (Unique)
    $('#name-of-scheme').focusout(function (event) {
        let nameOfScheme = $('#name-of-scheme').val();

        $.get('/AccountChildAction/IsUniqueSharesCapitalSchemeName', { _nameOfScheme: nameOfScheme, async: false }, function (data, textStatus, jqXHR) {
            if (data) {
                isValidSchemeName = true;
                $('#name-of-scheme-error').addClass('d-none');
            }
            else {
                isValidSchemeName = false;
                $('#name-of-scheme-error').removeClass('d-none');
            }
        });
    });

    $('#retail-account-turn-over-limit').focusout(function () {
        let retailAccountTurnOverLimit = parseFloat($('#retail-account-turn-over-limit').val());

        // Allow Hundred Times Turn Over Limit
        maxTurnOverLimit = parseFloat(retailAccountTurnOverLimit) * 100;

        $('#turn-over-limit').attr('min', parseFloat(maxTurnOverLimit));
    })

    // RetailAccountTurnOverLimit Validation
    $('#retail-account-turn-over-limit').focusout(function () {
        // retailMaximumSharesHoldingLimitAmount - Varialbe From View (i.e. From ViewModel)
        if (parseFloat($(this).val()) <= parseFloat(retailMaximumSharesHoldingLimitAmount))
            $('#retail-account-turn-over-limit-error').addClass('d-none');
        else
            $('#retail-account-turn-over-limit-error').removeClass('d-none')
    })

    // CorporateAccountTurnOverLimit Validation
    $('#corporate-account-turn-over-limit').focusout(function () {
        // corporateMaximumSharesHoldingLimitAmount - Varialbe From View (i.e. From ViewModel)
        if (parseFloat($(this).val()) <= parseFloat(corporateMaximumSharesHoldingLimitAmount))
            $('#corporate-account-turn-over-limit-error').addClass('d-none');
        else
            $('#corporate-account-turn-over-limit-error').removeClass('d-none')
    })

    // RetailHoldingAmountProportionToTotalAmount Validation
    $('#retail-holding-portion').focusout(function () {
        // toFixed(2) - method rounds the string to a specified number of decimals.
        // retailMaximumSharesHoldingLimitPercentage - Varialbe From View (i.e. From ViewModel)
        if (parseFloat($(this).val()).toFixed(2) <= parseFloat(retailMaximumSharesHoldingLimitPercentage))
            $('#retail-holding-portion-error').addClass('d-none');
        else
            $('#retail-holding-portion-error').removeClass('d-none')
    })

    // CorporateHoldingAmountProportionToTotalAmount Validation
    $('#corporate-holding-portion').focusout(function () {
        if (parseFloat($(this).val()).toFixed(2) <= parseFloat(corporateMaximumSharesHoldingLimitPercentage))
            $('#corporate-holding-portion-error').addClass('d-none');
        else
            $('#corporate-holding-portion-error').removeClass('d-none')
    })

    // Number Of Account For Real Number
    $('#number-of-account').focusout(function () {
        if ($('.number-of-account-unit:checked').val() === NUMBER_UNIT) {
            $(this).val(Math.floor($(this).val()));
        }
    })

    // Target Estimation Accordion Input Validation
    // Account Increment Unit Validation
    $('.number-of-account-unit').change(function () {
        NumberOfAccountUnitChangeEventFunction();
    });

    // Turn Over Amount
    $('.turn-over-unit').change(function () {
        TurnOverUnitChangeEventFunction();
    });


    // @@@@@@@@@@@@@@@@@@@@@@@@@@ MODAL INPUT ERROR CLEARING @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Application Number
    $('.application-number-input').focusout(function () {
        IsValidApplicationNumberAccordionInputs();
    });

    // Application Number For Toggle Switch Click
    $('.application-number-input').change(function () {
        IsValidApplicationNumberAccordionInputs();
    });

    // Member Number
    $('.member-number-input').focusout(function () {
        IsValidMemberNumberAccordionInputs()
    });

    // Member Number
    $('.member-number-input').change(function () {
        IsValidMemberNumberAccordionInputs()
    });

    // Certificate Number
    $('.certificate-number-input').focusout(function () {
        IsValidCertificateParameterAccordionInputs();
    });

    // Certificate Number
    $('.certificate-number-input').change(function () {
        IsValidCertificateParameterAccordionInputs();
    });

    // Account Number
    $('.account-number-input').focusout(function () {
        IsValidCustomerAccountNumberAccordionInputs();
    });

    // Account Number
    $('.account-number-input').change(function () {
        IsValidCustomerAccountNumberAccordionInputs();
    });

    // Account Parameter Input Validation
    $('.account-parameter-input').focusout(function () {
        IsValidAccountParameterAccordionInputs();
    });

    // Dividend Parameter Accordion Input Validation
    $('.dividend-parameter-input').focusout(function () {
        IsValidDividendParameterAccordionInputs();
    });

    // Dividend Parameter Accordion Input Validation For Radio Input
    $('.dividend-parameter-radio-input').change(function () {
        IsValidDividendParameterAccordionInputs();
    });

    // Target Estimation Accordion Input Validation
    $('.target-estimation-input').focusout(function () {
        IsValidTargetEstimationAccordionInputs();
    });

    // Limit Parameter Accordion Input Validation
    $('.limit-parameter-input').focusout(function () {
        IsValidLimitAccordionInputs();
    });


    // For Select 2 Focusout Input Validation
    objSelect2.on('select2:close', function (e) {
        debugger;
        let myId = $(this).attr('id');

        if (myId == 'account-number-mask')
            IsValidCustomerAccountNumberAccordionInputs();

        if (myId == 'application-number-mask')
            IsValidApplicationNumberAccordionInputs();

        if (myId == 'certificate-number-mask')
            IsValidCertificateParameterAccordionInputs();

        if (myId == 'member-number-mask')
            IsValidMemberNumberAccordionInputs();
    })

    function NoRoundingChangeEventFunction() {
        debugger;
        if ($('.round-method:checked').val() == 'NOR') {
            debugger;
            $('#round-nearest-input').addClass('d-none');
            $('#round-nearest').val(0)
        }
        else
            $('#round-nearest-input').removeClass('d-none');
    }

    function NumberOfAccountUnitChangeEventFunction() {
        if ($('.number-of-account-unit:checked').val() === NUMBER_UNIT) {
            $('#number-of-account').attr('max', 9999);
            $('#number-of-account').addClass('real-number');

            if ($('#number-of-account').val() > 9999)
                $('#number-of-account').val(9999);
        }
        else {
            $('#number-of-account').attr('max', 20);
            $('#number-of-account').removeClass('real-number');

            if ($('#number-of-account').val() > 20)
                $('#number-of-account').val(20);
        }

    };

    function TurnOverUnitChangeEventFunction() {
        if ($('.turn-over-unit:checked').val() === FLAT_AMOUNT) {
            $('#turn-over-amount').attr('max', 999999999);

            if (parseInt($('#turn-over-amount').val()) > 999999999)
                $('#turn-over-amount').val(999999999);
        }
        else {
            $('#turn-over-amount').attr('max', 20);

            if ($('#turn-over-amount').val() > 20)
                $('#turn-over-amount').val(20);
        }
    };

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   VALIDATION FUNCTIONS FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // 1. Application Number Accordion Input Validation
    function IsValidApplicationNumberAccordionInputs() {
        debugger;
        result = true;

        if ($('#enable-application').is(':checked')) {
            multiSelectCount = 0;

            if ($('#enable-auto-application-number').is(':checked')) {
                let startApplicationNumber = parseInt($('#start-application-number').val());
                let endApplicationNumber = parseInt($('#end-application-number').val());
                let applicationNumberIncrementBy = parseInt($('#application-number-increment-by').val());

                multiSelectCount = parseInt($('#application-number-mask option:selected').length);

                // Application Number Mask
                if (multiSelectCount === 0)
                    result = false;

                // Start Application Number
                if (isNaN(startApplicationNumber) === false) {
                    minimum = parseInt($('#start-application-number').attr('min'));
                    maximum = parseInt($('#start-application-number').attr('max'));

                    if (parseInt(startApplicationNumber) < parseInt(minimum) || parseInt(startApplicationNumber) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;

                // End Application Number
                if (isNaN(endApplicationNumber) === false) {
                    minimum = parseInt($('#end-application-number').attr('min'));
                    maximum = parseInt($('#end-application-number').attr('max'));

                    if ((parseInt(startApplicationNumber) + 100) > parseInt(endApplicationNumber) || parseInt(endApplicationNumber) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;

                // Application Number Increment By
                if (isNaN(applicationNumberIncrementBy) === false) {
                    minimum = parseInt($('#application-number-increment-by').attr('min'));
                    maximum = parseInt($('#application-number-increment-by').attr('max'));

                    if (parseInt(applicationNumberIncrementBy) === 0 || parseInt(applicationNumberIncrementBy) > parseInt(((parseInt(endApplicationNumber) - parseInt(startApplicationNumber)) / 100)))
                        result = false;
                }
                else
                    result = false;
            }
        }

        if (result)
            $('#application-number-accordion-title-error').addClass('d-none');
        else
            $('#application-number-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 2. Member Number Accordion Input Validation
    function IsValidMemberNumberAccordionInputs() {
        multiSelectCount = 0;
        result = true;

        if ($('#enable-auto-member-number').is(':checked')) {
            let startMemberNumber = parseInt($('#start-member-number').val());
            let endMemberNumber = parseInt($('#end-member-number').val());
            let memberNumberIncrementBy = parseInt($('#member-number-increment-by').val());

            multiSelectCount = parseInt($('#member-number-mask option:selected').length);

            // Member Number Mask
            if (multiSelectCount === 0)
                result = false;

            // Start Member Number
            if (isNaN(startMemberNumber) === false) {
                minimum = parseInt($('#start-member-number').attr('min'));
                maximum = parseInt($('#start-member-number').attr('max'));

                if (parseInt(startMemberNumber) < parseInt(minimum) || parseInt(startMemberNumber) > parseInt(maximum))
                {
                    result = false;
                }
            }
            else
                result = false;

            // End Member Number
            if (isNaN(endMemberNumber) === false) {
                minimum = parseInt($('#end-member-number').attr('min'));
                maximum = parseInt($('#end-member-number').attr('max'));

                if ((parseInt(startMemberNumber) + 100) > parseInt(endMemberNumber) || parseInt(endMemberNumber) > parseInt(maximum)) {
                    result = false;
                }
            }
            else
                result = false;

            // Member Number Increment By
            if (isNaN(memberNumberIncrementBy) === false) {
                minimum = parseInt($('#member-number-increment-by').attr('min'));
                maximum = parseInt($('#member-number-increment-by').attr('max'));

                if (parseInt(memberNumberIncrementBy) === 0 || parseInt(memberNumberIncrementBy) > parseInt(((parseInt(endMemberNumber) - parseInt(startMemberNumber)) / 100)))
                {
                    result = false;
                }
            }
            else
                result = false;
        }
        if (result)
            $('#member-number-accordion-error').addClass('d-none');
        else
            $('#member-number-accordion-error').removeClass('d-none');

        return result;
    }

    // 3. Customer Account Number Accordion Input Validation
    function IsValidCustomerAccountNumberAccordionInputs() {
        multiSelectCount = 0;
        result = true;

        if ($('#enable-auto-account-number').is(':checked')) {
            let startAccountNumber = parseInt($('#start-account-number').val());
            let endAccountNumber = parseInt($('#end-account-number').val());
            let accountNumberIncrementBy = parseInt($('#account-number-increment-by').val());

            multiSelectCount = parseInt($('#account-number-mask option:selected').length);

            // Account Number Mask
            if (multiSelectCount === 0)
                result = false;

            // Start Application Number
            if (isNaN(startAccountNumber) === false) {
                minimum = parseInt($('#start-account-number').attr('min'));
                maximum = parseInt($('#start-account-number').attr('max'));

                if (parseInt(startAccountNumber) < parseInt(minimum) || parseInt(startAccountNumber) > parseInt(maximum)) {
                    result = false
                };
            }
            else {
                result = false;
            }

            // End Account Number
            if (isNaN(endAccountNumber) === false) {
                minimum = parseInt($('#end-account-number').attr('min'));
                maximum = parseInt($('#end-account-number').attr('max'));

                if (parseInt(endAccountNumber) < (parseInt(startAccountNumber) + 100) || parseInt(endAccountNumber) > parseInt(maximum)) {
                    result = false
                };
            }
            else {
                result = false;
            }

            // Account Number Increment By
            if (isNaN(accountNumberIncrementBy) === false) {
                minimum = parseInt($('#account-number-increment-by').attr('min'));
                maximum = parseInt($('#account-number-increment-by').attr('max'));

                if (parseInt(accountNumberIncrementBy) === 0 || parseInt(accountNumberIncrementBy) < parseInt(minimum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        if (result)
            $('#account-number-accordion-error').addClass('d-none');
        else
            $('#account-number-accordion-error').removeClass('d-none');

        return result;
    }

    // 4. Account Parameter Accordion Input Validation
    function IsValidAccountParameterAccordionInputs() {
        debugger;
        let timePeriodForNewCustomerFlag = parseInt($('#time-period-for-new-customer-flag').val());

        // Nominee Shares HoldingPercentage
        let minimumNumberOfShares = parseInt($('#minimum-number-of-shares').val());
        let maximumNumberOfShares = parseInt($('#maximum-number-of-shares').val());
        let defaultNumberOfShares = parseInt($('#default-number-of-shares').val());

        // Number Of Joint Account Holding Limit
        let enableNumberOfJointAccountHoldingLimit = $('#enable-number-of-joint-account-holding-limit').is(':checked') ? true : false;
        let minimumJointAccountHolder = parseInt($('#minimum-joint-account-holder').val());
        let maximumJointAccountHolder = parseInt($('#maximum-joint-account-holder').val());
        let defaultJointAccountHolder = parseInt($('#default-joint-account-holder').val());

        // Number Of Nominee Limit
        let enableNumberOfNomineeLimit = $('#enable-number-of-nominee-limit').is(':checked') ? true : false;
        let minimumNominee = parseInt($('#minimum-nominee').val());
        let maximumNominee = parseInt($('#maximum-nominee').val());
        let defaultNominee = parseInt($('#default-nominee').val());

        result = true;

        // Time Period For New Customer Flag Validaton 
        if (isNaN(timePeriodForNewCustomerFlag) === false) {
            minimum = parseInt($('#time-period-for-new-customer-flag').attr('min'));
            maximum = parseInt($('#time-period-for-new-customer-flag').attr('max'));

            if (parseInt(timePeriodForNewCustomerFlag) < parseInt(minimum) || parseInt(timePeriodForNewCustomerFlag) > parseInt(maximum))
                result = false;
        }
        else {
            result = false;
        }

        // Minimum Number Of Shares
        if (isNaN(minimumNumberOfShares) === false) {
            minimum = parseInt($('#minimum-number-of-shares').attr('min'));
            maximum = parseInt($('#minimum-number-of-shares').attr('max'));

            if (parseInt(minimumNumberOfShares) < parseInt(minimum) || parseInt(minimumNumberOfShares) > parseInt(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Maximum Number Of Shares 
        if (isNaN(maximumNumberOfShares) === false) {
            minimum = parseInt($('#maximum-number-of-shares').attr('min'));
            maximum = parseInt($('#maximum-number-of-shares').attr('max'));

            if (parseInt(maximumNumberOfShares) < parseInt(minimum) || parseInt(maximumNumberOfShares) > parseInt(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Default Number Of Shares 
        if (isNaN(defaultNumberOfShares) === false) {
            minimum = parseInt($('#default-number-of-shares').attr('min'));
            maximum = parseInt($('#default-number-of-shares').attr('max'));

            if (parseInt(defaultNumberOfShares) < parseInt(minimum) || parseInt(defaultNumberOfShares) > parseInt(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Joint Account Holding Limit
        if (enableNumberOfJointAccountHoldingLimit === true) {
            // Minimum Joint Account Holder
            if (isNaN(minimumJointAccountHolder) === false) {
                minimum = parseInt($('#minimum-joint-account-holder').attr('min'));
                maximum = parseInt($('#minimum-joint-account-holder').attr('max'));

                if (parseInt(minimumJointAccountHolder) < parseInt(minimum) || parseInt(minimumJointAccountHolder) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Maximum Joint Account Holder
            if (isNaN(maximumJointAccountHolder) === false) {
                minimum = parseInt($('#maximum-joint-account-holder').attr('min'));
                maximum = parseInt($('#maximum-joint-account-holder').attr('max'));

                if (parseInt(maximumJointAccountHolder) < parseInt(minimum) || parseInt(maximumJointAccountHolder) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Default Joint Account Holder
            if (isNaN(defaultJointAccountHolder) === false) {
                minimum = parseInt($('#default-joint-account-holder').attr('min'));
                maximum = parseInt($('#default-joint-account-holder').attr('max'));

                if (parseInt(defaultJointAccountHolder) < parseInt(minimum) || parseInt(defaultJointAccountHolder) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        // Nominee Limit
        if (enableNumberOfNomineeLimit === true) {
            // Minimum Nominee
            if (isNaN(minimumNominee) === false) {
                minimum = parseInt($('#minimum-nominee').attr('min'));
                maximum = parseInt($('#minimum-nominee').attr('max'));

                if (parseInt(minimumNominee) < parseInt(minimum) || parseInt(minimumNominee) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Maximum Nominee
            if (isNaN(maximumNominee) === false) {
                minimum = parseInt($('#maximum-nominee').attr('min'));
                maximum = parseInt($('#maximum-nominee').attr('max'));

                if (parseInt(maximumNominee) < parseInt(minimum) || parseInt(maximumNominee) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Default Nominee
            if (isNaN(defaultNominee) === false) {
                minimum = parseInt($('#default-nominee').attr('min'));
                maximum = parseInt($('#default-nominee').attr('max'));

                if (parseInt(defaultNominee) < parseInt(minimum) || parseInt(defaultNominee) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        if (result)
            $('#account-parameter-accordion-error').addClass('d-none');
        else
            $('#account-parameter-accordion-error').removeClass('d-none');

        return result;
    }

    // 5. Shares Certificate Parameter Accordion Input Validation
    function IsValidCertificateParameterAccordionInputs() {
        debugger;
        multiSelectCount = 0;

        result = true;

        if ($('#enable-auto-certificate-number').is(':checked')) {
            let startCertificateNumber = parseInt($('#start-certificate-number').val());
            let endCertificateNumber = parseInt($('#end-certificate-number').val());
            let certificateNumberIncrementBy = parseInt($('#certificate-number-increment-by').val());

            multiSelectCount = parseInt($('#certificate-number-mask option:selected').length);

            // Certificate Number Mask
            if (multiSelectCount === 0) {
                result = false;
            }

            // Start Certificate Number
            if (isNaN(startCertificateNumber) === false) {
                minimum = parseInt($('#start-certificate-number').attr('min'));
                maximum = parseInt($('#start-certificate-number').attr('max'));

                if (parseInt(startCertificateNumber) < parseInt(minimum) || parseInt(startCertificateNumber) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // End Certificate Number
            if (isNaN(endCertificateNumber) === false) {
                minimum = parseInt($('#end-certificate-number').attr('min'));
                maximum = parseInt($('#end-certificate-number').attr('max'));

                if ((parseInt(startCertificateNumber) + 100) > parseInt(endCertificateNumber) || parseInt(endCertificateNumber) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Certificate Number Increment By
            if (isNaN(certificateNumberIncrementBy) === false) {
                minimum = parseInt($('#application-number-increment-by').attr('min'));
                maximum = parseInt($('#application-number-increment-by').attr('max'));

                if (parseInt(certificateNumberIncrementBy) === 0 || parseInt(certificateNumberIncrementBy) > parseInt(((parseInt(endCertificateNumber) - parseInt(startCertificateNumber)) / 100))) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        if (result) {
            $('#certificate-number-accordion-error').addClass('d-none');
        }
        else {
            $('#certificate-number-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // 6. Shares Dividend Parameter Accordion Input Validation
    function IsValidDividendParameterAccordionInputs() {
        debugger;
        let effectiveDate = $('#effective-date').val()
        let membershipAgeForDividend = parseInt($('#membership-age-for-dividend').val());
        let exMemberAgeForDividend = parseInt($('#ex-member-age-for-dividend').val());
        let minimumDividendPercentage = parseFloat($('#minimum-dividend-percentage').val());
        let maximumDividendPercentage = parseFloat($('#maximum-dividend-percentage').val());
        let timePeriodForUnclaimedDividend = parseInt($('#time-period-to-cease-unclaimed-dividend').val());
        let roundNearest = parseInt($('#round-nearest').val());
        let financialYearForSharesBalance = parseInt($('#financial-for-shares-balance').val());
        let dividendCalculationMethod = $('#dividend-calculation-method option:selected').text();
        let ceasedDividendAction = $('.ceased-dividend-action:checked').next('label').text();
        let guarantorDividendAction = $('.ceased-guarantor-dividend-action:checked').next('label').text()
        let roundMethod = $('.round-method:checked').val();
        let roundMethodText = $('.round-method:checked').next('label').text();

        result = true;

        let isValidEffectiveDate = IsValidInputDate('#effective-date');

        if ($('#enable-dividend-parameter').is(':checked')) {
            // Effective Date
            if (isValidEffectiveDate === false) {
                result = false;
            }

            // Financial Year For Shares Balance
            if (isNaN(financialYearForSharesBalance) === false) {
                // time Period Unclaimed Dividend
                minimum = parseInt($('#financial-for-shares-balance').attr('min'));
                maximum = parseInt($('#financial-for-shares-balance').attr('max'));

                if (parseInt(financialYearForSharesBalance) < parseInt(minimum) || parseInt(financialYearForSharesBalance) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Membership Age For Dividend
            if (isNaN(membershipAgeForDividend) === false) {
                minimum = parseInt($('#membership-age-for-dividend').attr('min'));
                maximum = parseInt($('#membership-age-for-dividend').attr('max'));

                if (parseInt(membershipAgeForDividend) < parseInt(minimum) || parseInt(membershipAgeForDividend) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Ex Member Age For Dividend
            if (isNaN(exMemberAgeForDividend) === false) {
                minimum = parseInt($('#ex-member-age-for-dividend').attr('min'));
                maximum = parseInt($('#ex-member-age-for-dividend').attr('max'));

                if (parseInt(exMemberAgeForDividend) < parseInt(minimum) || parseInt(exMemberAgeForDividend) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Minimum Dividend Percentage
            if (isNaN(minimumDividendPercentage) === false) {
                minimum = parseFloat($('#minimum-dividend-percentage').attr('min'));
                maximum = parseFloat($('#minimum-dividend-percentage').attr('max'));

                if (parseFloat(minimumDividendPercentage) < parseFloat(minimum) || parseFloat(minimumDividendPercentage) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Maximum Dividend Percentage
            if (isNaN(maximumDividendPercentage) === false) {
                minimum = parseFloat($('#maximum-dividend-percentage').attr('min'));
                maximum = parseFloat($('#maximum-dividend-percentage').attr('max'));

                if (parseFloat(maximumDividendPercentage) < parseFloat(minimum) || parseFloat(maximumDividendPercentage) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Dividend Calculation Method
            if ($('#dividend-calculation-method').prop('selectedIndex') < 1) {
                result = false;
            }

            // Round Method
            if ($('.round-method:checked').length === 0) {
                result = false;
            }

            // Round Nearest
            if (roundMethod !== 'NOR') {
                if (isNaN(roundNearest) === false) {
                    minimum = parseInt($('#round-nearest').attr('min'));
                    maximum = parseInt($('#round-nearest').attr('max'));
                    if (parseInt(roundNearest) < parseInt(minimum) || parseInt(roundNearest) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Time Period Unclaimed Dividend
            if (isNaN(timePeriodForUnclaimedDividend) == false) {
                minimum = parseInt($('#time-period-to-cease-unclaimed-dividend').attr('min'));
                maximum = parseInt($('#time-period-to-cease-unclaimed-dividend').attr('max'));

                if (parseInt(timePeriodForUnclaimedDividend) < parseInt(minimum) || parseInt(timePeriodForUnclaimedDividend) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Ceased Dividend Action
            if ($('.ceased-dividend-action:checked').length === 0) {
                result = false;
            }

            //  Guarantor Dividend Action
            if ($('.ceased-guarantor-dividend-action:checked').length === 0) {
                result = false;
            }
        }
        if (result)
            $('#dividend-accordion-error').addClass('d-none');
        else
            $('#dividend-accordion-error').removeClass('d-none');

        return result;
    }

    // 7. Shares Dividend Parameter Accordion Input Validation
    function IsValidTargetEstimationAccordionInputs() {
        result = true;

        if ($('#estimate-target-card').hasClass('d-none') === false) {
            let numberOfIncrementAccount = parseFloat($('#number-of-account').val());
            let incrementAccountUnitText = $('.number-of-account-unit:checked').next('label').text();
            let turnOverAmount = parseFloat($('#turn-over-amount').val());
            let turnOverUnitText = $('.turn-over-unit:checked').next('label').text();

            result = true;

            if (isNaN(numberOfIncrementAccount) || $('.number-of-account-unit:checked').length === 0)
                result = false;
            else {
                if (incrementAccountUnitText === 'Number') {
                    minimum = parseInt($('#number-of-account').attr('min'));
                    maximum = parseInt($('#number-of-account').attr('max'));

                    if (parseInt(numberOfIncrementAccount) < parseInt(minimum) || parseInt(numberOfIncrementAccount) > parseInt(maximum))
                        result = false;
                }

                if (incrementAccountUnitText === 'Percentage') {
                    minimum = parseFloat($('#number-of-account').attr('min'));
                    maximum = parseFloat($('#number-of-account').attr('max'));

                    if (parseFloat(numberOfIncrementAccount) < parseFloat(minimum) || parseFloat(numberOfIncrementAccount) > parseFloat(maximum))
                        result = false;
                }
            }

            if (isNaN(turnOverAmount) || $('.turn-over-unit:checked').length === 0)
                result = false;
            else {
                if (turnOverUnitText === 'Flat Amount') {
                    minimum = parseFloat($('#turn-over-amount').attr('min'));
                    maximum = parseFloat($('#turn-over-amount').attr('max'));

                    if (parseFloat(turnOverAmount) < parseFloat(minimum) || parseFloat(turnOverAmount) > parseFloat(maximum))
                        result = false;
                }

                if (turnOverUnitText === 'Percentage') {
                    minimum = parseFloat($('#turn-over-amount').attr('min'));
                    maximum = parseFloat($('#turn-over-amount').attr('max'));

                    if (parseFloat(turnOverAmount) < parseFloat(minimum) || parseFloat(turnOverAmount) > parseFloat(maximum))
                        result = false;
                }
            }
        }

        if (result)
            $('#target-accordion-title-error').addClass('d-none');
        else
            $('#target-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 8. Limit Parameter Accordion Input Validation
    function IsValidLimitAccordionInputs() {
        result = true;

        if ($('#limit-parameter-card').hasClass('d-none') === false) {
            debugger;
            let cashDepositLimit = parseFloat($('#cash-deposit-limit').val());
            let cashWithdrawalLimit = parseFloat($('#cash-withdrawal-limit').val());
            let retailAccountTurnOverLimit = parseFloat($('#retail-account-turn-over-limit').val());
            let corporateAccountTurnOverLimit = parseFloat($('#corporate-account-turn-over-limit').val());
            let retailHoldingAmount = parseFloat($('#retail-holding-portion').val());
            let corporateHoldingAmount = parseFloat($('#corporate-holding-portion').val());
            let turnOverLimit = parseFloat($('#turn-over-limit').val());

            // Allow Hundred Times Turn Over Limit
            maxTurnOverLimit = parseFloat(retailAccountTurnOverLimit) * 100;
            let retailMax = parseFloat($('#retail-account-turn-over-limit').attr('max'));
            let corporateMax = parseFloat($('#corporate-account-turn-over-limit').attr('max'));

            let retailMaxPercentage = parseFloat($('#retail-holding-portion').attr('max'));
            let corporateMaxPercentage = parseFloat($('#corporate-holding-portion').attr('max'));

            if (isNaN(cashDepositLimit) === false) {
                minimum = parseFloat($('#cash-deposit-limit').attr('min'));
                maximum = parseFloat($('#cash-deposit-limit').attr('max'));

                if (parseFloat(cashDepositLimit) < parseFloat(minimum) || parseFloat(cashDepositLimit) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            if (isNaN(cashWithdrawalLimit) === false) {
                minimum = parseFloat($('#cash-withdrawal-limit').attr('min'));
                maximum = parseFloat($('#cash-withdrawal-limit').attr('max'));

                if (parseFloat(cashWithdrawalLimit) < parseFloat(minimum) || parseFloat(cashWithdrawalLimit) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            if (isNaN(retailAccountTurnOverLimit) === false) {
                minimum = parseFloat($('#retail-account-turn-over-limit').attr('min'));
                maximum = parseFloat($('#retail-account-turn-over-limit').attr('max'));

                if (parseFloat(retailAccountTurnOverLimit) < parseFloat(minimum) || parseFloat(retailAccountTurnOverLimit) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            if (isNaN(corporateAccountTurnOverLimit) === false) {
                minimum = parseFloat($('#corporate-account-turn-over-limit').attr('min'));
                maximum = parseFloat($('#corporate-account-turn-over-limit').attr('max'));

                if (parseFloat(corporateAccountTurnOverLimit) < parseFloat(minimum) || parseFloat(corporateAccountTurnOverLimit) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            if (isNaN(retailHoldingAmount) === false) {
                minimum = parseFloat($('#retail-holding-portion').attr('min'));
                maximum = parseFloat($('#retail-holding-portion').attr('max'));

                if (parseFloat(retailHoldingAmount) < parseFloat(minimum) || parseFloat(retailHoldingAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            if (isNaN(corporateHoldingAmount) === false) {
                minimum = parseFloat($('#corporate-holding-portion').attr('min'));
                maximum = parseFloat($('#corporate-holding-portion').attr('max'));

                if (parseFloat(corporateHoldingAmount) < parseFloat(minimum) || parseFloat(corporateHoldingAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            if (isNaN(turnOverLimit) === false) {
                minimum = parseFloat($('#turn-over-limit').attr('min'));
                maximum = parseFloat($('#turn-over-limit').attr('max'));

                if (parseFloat(turnOverLimit) < parseFloat(minimum) || parseFloat(turnOverLimit) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        if (result)
            $('#limit-accordion-error').addClass('d-none');
        else
            $('#limit-accordion-error').removeClass('d-none');

        return result;
    }

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme Closing Charges - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-closing-charges-dt').click(function () {
        event.preventDefault();
        SetModalTitle('closing-charges', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-closing-charges-dt').click(function () {
        SetModalTitle('closing-charges', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-closing-charges-dt').data('rowindex');
            id = $('#closing-charges-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs
            $('#maximum-time-period-in-days').attr('min', columnValues[3]);
            $('#maximum-charges-closing-amount').attr('min', columnValues[5]);

            $('#minimum-charges-closing-amount', myModal).val(columnValues[5]);
            $('#minimum-time-period-in-days', myModal).val(columnValues[3]);
            $('#closing-charges-general-ledger-id', myModal).val(columnValues[1]);
            $('#maximum-time-period-in-days', myModal).val(columnValues[4]);
            $('#maximum-charges-closing-amount', myModal).val(columnValues[6]);
            $('#is-taxable', myModal).val(columnValues[7]);
            $('#is-applicable-on-death', myModal).val(columnValues[8]);
            $('#is-taxable').prop('checked', columnValues[7].toString().toLowerCase() === 'true' ? true : false);
            $('#is-applicable-on-death').prop('checked', columnValues[8].toString().toLowerCase() === 'true' ? true : false);

            $('#note-closing-charges', myModal).val(columnValues[9]);

            // Show Modal
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-closing-charges-dt').addClass('read-only');
            $('#closing-charges-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-closing-charges-modal').click(function (event) {
        if (IsValidClosingChargesDataTableModal()) {
            row = closingChargesDataTable.row.add([
                tag,
                chargesGeneralLedgerId,
                chargesGeneralLedgerText,
                fromTimePeriodInDays,
                toTimePeriodInDays,
                minimumChargesAmount,
                maximumChargesAmount,
                isTaxable,
                isApplicableOnDeath,
                note,
            ]).draw();
            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideClosingChargesDataTableColumns()

            closingChargesDataTable.columns.adjust().draw();

            $('#closing-charges-data-table-error').addClass('d-none');

            EnableNewOperation('closing-charges');

            $('#closing-charges-modal').modal('hide');
        }
    });

    // Modal Update Button Event
    $('#btn-update-closing-charges-modal').click(function (event) {
        $('#select-all-closing-charges').prop('checked', false);
        if (IsValidClosingChargesDataTableModal()) {
            closingChargesDataTable.row(selectedRowIndex).data([
                tag,
                chargesGeneralLedgerId,
                chargesGeneralLedgerText,
                fromTimePeriodInDays,
                toTimePeriodInDays,
                minimumChargesAmount,
                maximumChargesAmount,
                isTaxable,
                isApplicableOnDeath,
                note,
            ]).draw();

            HideClosingChargesDataTableColumns()

            closingChargesDataTable.columns.adjust().draw();

            EnableNewOperation('closing-charges');

            $('#closing-charges-modal').modal('hide');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-closing-charges-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-closing-charges tbody input[type="checkbox"]:checked').each(function () {
                    closingChargesDataTable.row($('#tbl-closing-charges tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    //rowData = $('#btn-delete-closing-charges-dt').data('rowindex');

                    // Display Required Error Message, If Table Has Not Any Record
                    if (!closingChargesDataTable.data().any())
                        $('#closing-charges-data-table-error').removeClass('d-none');

                    EnableNewOperation('closing-charges');

                    $('#select-all-closing-charges').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Other Charges Datatable
    $('#select-all-closing-charges').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-closing-charges tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = closingChargesDataTable.row(row).index();
                rowData = (closingChargesDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });
                $('#btn-delete-closing-charges-dt').data('rowindex', arr);

                EnableDeleteOperation('closing-charges');
            });
        }
        else {
            EnableNewOperation('closing-charges');

            $('#tbl-closing-charges tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-closing-charges tbody').click('input[type=checkbox]', function () {
        $('#tbl-closing-charges input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = closingChargesDataTable.row(row).index();
                rowData = (closingChargesDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('charges');

                $('#btn-update-closing-charges-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-closing-charges-dt').data('rowindex', rowData);
                $('#btn-delete-closing-charges-dt').data('rowindex', arr);
                $('#select-all-closing-charges').data('rowindex', arr);
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-closing-charges tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('closing-charges');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('closing-charges');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('closing-charges');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-closing-charges').prop('checked', true);
        else
            $('#select-all-closing-charges').prop('checked', false);
    });

    // Closing Charges Data-Table Modal
    function IsValidClosingChargesDataTableModal() {
        result = true;
        debugger;
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        chargesGeneralLedgerId = $('#closing-charges-general-ledger-id option:selected').val();
        chargesGeneralLedgerText = $('#closing-charges-general-ledger-id option:selected').text();
        fromTimePeriodInDays = parseInt($('#minimum-time-period-in-days').val());
        toTimePeriodInDays = parseInt($('#maximum-time-period-in-days').val());
        minimumChargesAmount = parseFloat($('#minimum-charges-closing-amount').val());
        maximumChargesAmount = parseFloat($('#maximum-charges-closing-amount').val());
        isTaxable = $('#is-taxable').is(':checked') ? 'True' : 'False';
        isApplicableOnDeath = $('#is-applicable-on-death').is(':checked') ? 'True' : 'False';
        note = $('#note-closing-charges').val();

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        // Charges General Ledger
        if ($('#closing-charges-general-ledger-id').prop('selectedIndex') < 1) {
            result = false;
            $('#closing-charges-general-ledger-id-error').removeClass('d-none');
        }

        // From Time Period In Days
        if (isNaN(fromTimePeriodInDays) === false) {
            minimum = parseInt($('#minimum-time-period-in-days').attr('min'));
            maximum = parseInt($('#minimum-time-period-in-days').attr('max'));

            if (parseInt(fromTimePeriodInDays) < parseInt(minimum) || parseInt(fromTimePeriodInDays) > parseInt(maximum)) {
                result = false;
                $('#minimum-time-period-in-days-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#minimum-time-period-in-days-error').removeClass('d-none');
        }

        // To Time Period In Days
        if (isNaN(toTimePeriodInDays) === false) {
            minimum = parseInt($('#maximum-time-period-in-days').attr('min'));
            maximum = parseInt($('#maximum-time-period-in-days').attr('max'));

            if (parseInt(toTimePeriodInDays) < parseInt(minimum) || parseInt(toTimePeriodInDays) > parseInt(maximum)) {
                result = false;
                $('#maximum-time-period-in-days-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-time-period-in-days-error').removeClass('d-none');
        }

        // Minimum Charges Amount
        if (isNaN(minimumChargesAmount) === false) {
            minimum = parseFloat($('#minimum-charges-closing-amount').attr('min'));
            maximum = parseFloat($('#minimum-charges-closing-amount').attr('max'));

            if (parseFloat(minimumChargesAmount) < parseFloat(minimum) || parseFloat(minimumChargesAmount) > parseFloat(maximum)) {
                result = false;
                $('#minimum-charges-closing-amount-error').removeClass('d-none');
            }
            else {
                $('#minimum-charges-closing-amount-error').addClass('d-none');
            }
        }
        else {
            result = false;
            $('#minimum-charges-closing-amount-error').removeClass('d-none');
        }

        // Maximum Charges Amount
        if (isNaN(maximumChargesAmount) === false) {
            minimum = parseFloat($('#maximum-charges-closing-amount').attr('min'));
            maximum = parseFloat($('#maximum-charges-closing-amount').attr('max'));

            if (parseFloat(maximumChargesAmount) < parseFloat(minimum) || parseFloat(maximumChargesAmount) > parseFloat(maximum)) {
                result = false;
                $('#maximum-charges-closing-amount-error').removeClass('d-none');
            }
            else {
                $('#maximum-charges-closing-amount-error').addClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-charges-closing-amount-error').removeClass('d-none');
        }
        return result;
    }

    // Hide Unnecessary Columns
    function HideClosingChargesDataTableColumns() {
        closingChargesDataTable.column(1).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme Transfer Charges - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-transfer-charges-dt').click(function () {
        event.preventDefault();
        SetModalTitle('transfer-charges', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-transfer-charges-dt').click(function () {
        SetModalTitle('transfer-charges', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-transfer-charges-dt').data('rowindex');
            id = $('#transfer-charges-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs
            $('#maximum-time-period-days').attr('min', columnValues[3]);
            $('#maximum-transfer-charges-amount').attr('min', columnValues[7]);

            $('#minimum-transfer-charges-amount', myModal).val(columnValues[7]);
            $('#minimum-time-period-days', myModal).val(columnValues[3]);

            $('#transfer-charges-general-ledger-id', myModal).val(columnValues[1]);
            $('#maximum-time-period-days', myModal).val(columnValues[4]);
            $('.charges-base[value="' + columnValues[5] + '"]').prop('checked', true);
            $('#maximum-transfer-charges-amount', myModal).val(columnValues[8]);
            $('#is-taxable-transfer', myModal).val(columnValues[9]);
            $('#is-applicable-on-death-transfer', myModal).val(columnValues[10]);
            $('#is-taxable-transfer').prop('checked', columnValues[9].toString().toLowerCase() === 'true' ? true : false);
            $('#is-applicable-on-death-transfer').prop('checked', columnValues[10].toString().toLowerCase() === 'true' ? true : false);

            $('#note-transfer-charges', myModal).val(columnValues[11]);

            // Show Modal
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-transfer-charges-dt').addClass('read-only');
            $('#transfer-charges-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-transfer-charges-modal').click(function (event) {
        if (IsValidTransferChargesDataTableModal()) {
            row = transferChargesDataTable.row.add([
                tag,
                chargesGeneralLedgerId,
                chargesGeneralLedgerText,
                fromTimePeriodInDays,
                toTimePeriodInDays,
                chargesBase,
                chargesBaseText,
                minimumChargesAmount,
                maximumChargesAmount,
                isTaxable,
                isApplicableOnDeath,
                note,
            ]).draw();
            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideTransferChargesDataTableColumns()

            transferChargesDataTable.columns.adjust().draw();

            // Remove Required Error Message 
            $('#transfer-charges-data-table-error').addClass('d-none');

            EnableNewOperation('transfer-charges');

            $('#transfer-charges-modal').modal('hide');
        }
    });

    // Modal Update Button Event
    $('#btn-update-transfer-charges-modal').click(function (event) {
        $('#select-all-transfer-charges').prop('checked', false);
        if (IsValidTransferChargesDataTableModal()) {
            transferChargesDataTable.row(selectedRowIndex).data([
                tag,
                chargesGeneralLedgerId,
                chargesGeneralLedgerText,
                fromTimePeriodInDays,
                toTimePeriodInDays,
                chargesBase,
                chargesBaseText,
                minimumChargesAmount,
                maximumChargesAmount,
                isTaxable,
                isApplicableOnDeath,
                note,
            ]).draw();

            HideTransferChargesDataTableColumns()

            transferChargesDataTable.columns.adjust().draw();

            EnableNewOperation('transfer-charges');

            $('#transfer-charges-modal').modal('hide');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-transfer-charges-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-transfer-charges tbody input[type="checkbox"]:checked').each(function () {
                    transferChargesDataTable.row($('#tbl-transfer-charges tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    //rowData = $('#btn-delete-transfer-charges-dt').data('rowindex');

                    // Display Required Error Message, If Table Has Not Any Record
                    if (!transferChargesDataTable.data().any())
                        $('#transfer-charges-data-table-error').removeClass('d-none');

                    EnableNewOperation('transfer-charges');

                    $('#select-all-transfer-charges').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Transfer Charges Datatable
    $('#select-all-transfer-charges').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-transfer-charges tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = transferChargesDataTable.row(row).index();
                rowData = (transferChargesDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });
                $('#btn-delete-transfer-charges-dt').data('rowindex', arr);

                EnableDeleteOperation('transfer-charges')
            });
        }
        else {
            EnableNewOperation('transfer-charges');
            $('#tbl-transfer-charges tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-transfer-charges tbody').click('input[type=checkbox]', function () {
        $('#tbl-transfer-charges input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = transferChargesDataTable.row(row).index();
                rowData = (transferChargesDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('transfer-charges');

                $('#btn-update-transfer-charges-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-transfer-charges-dt').data('rowindex', rowData);
                $('#btn-delete-transfer-charges-dt').data('rowindex', arr);
                $('#select-all-transfer-charges').data('rowindex', arr);
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-transfer-charges tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('transfer-charges');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('transfer-charges');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('transfer-charges');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-transfer-charges').prop('checked', true);
        else
            $('#select-all-transfer-charges').prop('checked', false);
    });

    // Transfer Charges Data-Table Modal
    function IsValidTransferChargesDataTableModal() {
        result = true;

        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        chargesGeneralLedgerId = $('#transfer-charges-general-ledger-id option:selected').val();
        chargesGeneralLedgerText = $('#transfer-charges-general-ledger-id option:selected').text();
        fromTimePeriodInDays = parseInt($('#minimum-time-period-days').val());
        toTimePeriodInDays = parseInt($('#maximum-time-period-days').val());
        chargesBase = $('.charges-base:checked').val();
        chargesBaseText = $('.charges-base:checked').next('label').text();
        minimumChargesAmount = parseFloat($('#minimum-transfer-charges-amount').val());
        maximumChargesAmount = parseFloat($('#maximum-transfer-charges-amount').val());
        isTaxable = $('#is-taxable-transfer').is(':checked') ? 'True' : 'False';
        isApplicableOnDeath = $('#is-applicable-on-death-transfer').is(':checked') ? 'True' : 'False';
        note = $('#note-transfer-charges').val();

        //  Note
        if (note == '')
            note = 'None';

        // Validate General Ledger
        if ($('#transfer-charges-general-ledger-id').prop('selectedIndex') < 1) {
            result = false;
            $('#transfer-charges-general-ledger-id-error').removeClass('d-none');
        }
        else
            $('#transfer-charges-general-ledger-id-error').addClass('d-none');

        // From Time Period In Days
        if (isNaN(fromTimePeriodInDays) === false) {
            minimum = parseInt($('#minimum-time-period-days').attr('min'));
            maximum = parseInt($('#minimum-time-period-days').attr('max'));

            if (parseInt(fromTimePeriodInDays) < parseInt(minimum) || parseInt(fromTimePeriodInDays) > parseInt(maximum)) {
                result = false;
                $('#minimum-time-period-days-error').removeClass('d-none');
            }
            else {
                $('#minimum-time-period-days-error').addClass('d-none');
            }
        }
        else {
            result = false;
            $('#minimum-time-period-days-error').removeClass('d-none');
        }

        // To Time Period In Days
        if (isNaN(toTimePeriodInDays) === false) {
            minimum = parseInt($('#maximum-time-period-days').attr('min'));
            maximum = parseInt($('#maximum-time-period-days').attr('max'));

            if (parseInt(toTimePeriodInDays) < parseInt(minimum) || parseInt(toTimePeriodInDays) > parseInt(maximum)) {
                result = false;
                $('#maximum-time-period-days-error').removeClass('d-none');
            }
            else {
                $('#maximum-time-period-days-error').addClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-time-period-days-error').removeClass('d-none');
        }

        // Charges Base
        if ($('.charges-base:checked').length === 0) {
            result = false;
            $('#charges-base-error').removeClass('d-none');
        }
        else
            $('#charges-base-error').addClass('d-none');

        // Minimum Charges Amount
        if (isNaN(minimumChargesAmount) === false) {
            minimum = parseFloat($('#minimum-transfer-charges-amount').attr('min'));
            maximum = parseFloat($('#minimum-transfer-charges-amount').attr('max'));

            if (parseFloat(minimumChargesAmount) < parseFloat(minimum) || parseFloat(minimumChargesAmount) > parseFloat(maximum)) {
                result = false;
                $('#minimum-transfer-charges-amount-error').removeClass('d-none');
            }
            else {
                $('#minimum-transfer-charges-amount-error').addClass('d-none');
            }
        }
        else {
            result = false;
            $('#minimum-transfer-charges-amount-error').removeClass('d-none');
        }

        // Maximum Charges Amount
        if (isNaN(maximumChargesAmount) === false) {
            minimum = parseFloat($('#maximum-transfer-charges-amount').attr('min'));
            maximum = parseFloat($('#maximum-transfer-charges-amount').attr('max'));

            if (parseFloat(maximumChargesAmount) < parseFloat(minimum) || parseFloat(maximumChargesAmount) > parseFloat(maximum)) {
                result = false;
                $('#maximum-transfer-charges-amount-error').removeClass('d-none');
            }
            else {
                $('#maximum-transfer-charges-amount-error').addClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-transfer-charges-amount-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideTransferChargesDataTableColumns() {
        transferChargesDataTable.column(1).visible(false);
        transferChargesDataTable.column(5).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme  Notice Schedule - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-notice-schedule-dt').click(function () {
        event.preventDefault();
        SetModalTitle('notice-schedule', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-notice-schedule-dt').click(function () {
        SetModalTitle('notice-schedule', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-notice-schedule-dt').data('rowindex');
            id = $('#notice-schedule-modal').attr('id');
            myModal = $('#' + id).modal();

            $('#notice-type-id', myModal).val(columnValues[1]);
            $('#comunication-media-id', myModal).val(columnValues[3]);
            $('#schedule-id', myModal).val(columnValues[5]);
            $('#note-notice-type', myModal).val(columnValues[7]);

            // Show Modal
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-notice-schedule-dt').addClass('read-only');
            $('#notice-schedule-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-notice-schedule-modal').click(function (event) {
        if (IsValidNoticeScheduleDataTableModal()) {
            row = noticeScheduleDataTable.row.add([
                tag,
                noticeTypeId,
                noticeScheduleText,
                communicationMediaId,
                communicationMediaText,
                scheduleId,
                scheduleText,
                note,
            ]).draw();

            HideNoticeScheduleDataTableColumns();

            noticeScheduleDataTable.columns.adjust().draw();

            EnableNewOperation('notice-schedule');

            $('#notice-schedule-modal').modal('hide');
        }
    });

    // Modal update Button Event
    $('#btn-update-notice-schedule-modal').click(function (event) {
        $('#select-all-notice-schedule').prop('checked', false);

        if (IsValidNoticeScheduleDataTableModal()) {
            noticeScheduleDataTable.row(selectedRowIndex).data([
                tag,
                noticeTypeId,
                noticeScheduleText,
                communicationMediaId,
                communicationMediaText,
                scheduleId,
                scheduleText,
                note,
            ]).draw();

            HideNoticeScheduleDataTableColumns();

            noticeScheduleDataTable.columns.adjust().draw();

            EnableNewOperation('notice-schedule');

            $('#notice-schedule-modal').modal('hide');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-notice-schedule-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete This Record?')) {
                if ($('#tbl-notice-schedule tbody input[type="checkbox"]:checked').each(function () {
                    noticeScheduleDataTable.row($('#tbl-notice-schedule tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();
                    rowData = $('#btn-delete-notice-schedule-dt').data('rowindex');

                    EnableNewOperation('notice-schedule');

                    $('#select-all-notice-schedule').prop('checked', false);
                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Notice Schedule Datatable
    $('#select-all-notice-schedule').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-notice-schedule tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = noticeScheduleDataTable.row(row).index();
                rowData = (noticeScheduleDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-notice-schedule-dt').data('rowindex', arr);

                EnableDeleteOperation('notice-schedule');
            });
        }
        else {
            EnableNewOperation('notice-schedule');

            $('#tbl-notice-schedule tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-notice-schedule tbody').click('input[type=checkbox]', function () {
        $('#tbl-notice-schedule input[type="checkbox"]:checked').each(function () {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                $('input[type="checkbox"]:checked').each(function (index) {
                    row = $(this).closest('tr');
                    selectedRowIndex = noticeScheduleDataTable.row(row).index();
                    rowData = (noticeScheduleDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('notice-schedule');

                    $('#btn-update-notice-schedule-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-notice-schedule-dt').data('rowindex', rowData);
                    $('#btn-delete-notice-schedule-dt').data('rowindex', arr);
                    $('#select-all-notice-schedule').data('rowindex', arr);
                })
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-notice-schedule tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('notice-schedule');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('notice-schedule');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('notice-schedule');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-notice-schedule').prop('checked', true);
        else
            $('#select-all-notice-schedule').prop('checked', false);
    });

    // Validate Notice Schedule Module
    function IsValidNoticeScheduleDataTableModal() {
        // Get Modal Inputs In Local letiables
        let result = true;
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        noticeTypeId = $('#notice-type-id option:selected').val();
        noticeScheduleText = $('#notice-type-id option:selected').text();
        communicationMediaId = $("#comunication-media-id option:selected").val();
        communicationMediaText = $('#comunication-media-id option:selected').text();
        scheduleId = $('#schedule-id option:selected').val();
        scheduleText = $('#schedule-id option:selected').text();
        note = $('#note-notice-type').val();

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        // NoticeTypeId
        if ($('#notice-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#notice-type-id-error').removeClass('d-none');
        }

        //CommunicationMediaId
        if ($('#comunication-media-id').prop('selectedIndex') < 1) {
            result = false;
            $('#comunication-media-id-error').removeClass('d-none');
        }

        //ScheduleId
        if ($('#schedule-id').prop('selectedIndex') < 1) {
            result = false;
            $('#schedule-id-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideNoticeScheduleDataTableColumns() {
        noticeScheduleDataTable.column(1).visible(false);
        noticeScheduleDataTable.column(3).visible(false);
        noticeScheduleDataTable.column(5).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme  Report Fromat  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-report-format-dt').click(function () {
        event.preventDefault();
        editedReportFormatId = ''
        SetReportFormatUniqueDropdownList();
        SetModalTitle('report-format', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-report-format-dt').click(function () {
        SetModalTitle('report-format', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-report-format-dt').data('rowindex');

            editedReportFormatId = columnValues[1];
            id = $('#report-format-modal').attr('id');
            myModal = $('#' + id).modal();

            SetReportFormatUniqueDropdownList();

            // Display Value In Modal Inputs
            $('#report-format-id', myModal).val(columnValues[1]);
            $('#note-Report', myModal).val(columnValues[3]);

            // Show Hidden Dropdown List Item
            $('#report-format-id').find('option[value="' + columnValues[1] + '"]').show();

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-report-format-dt').addClass('read-only');
            $('#report-format-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-report-format-modal').click(function (event) {
        if (IsValidReportFormatDataTableModal()) {
            row = reportTypeFormatDataTable.row.add([
                tag,
                reportFormatId,
                reportFormatText,
                note,
            ]).draw();

            HideReportFormatDataTableColumns();

            reportTypeFormatDataTable.columns.adjust().draw();

            EnableNewOperation('report-format');

            $('#report-format-modal').modal('hide');
        }
    });

    // Modal update Button Event
    $('#btn-update-report-format-modal').click(function (event) {
        $('#select-all-report-format').prop('checked', false);

        if (IsValidReportFormatDataTableModal()) {
            reportTypeFormatDataTable.row(selectedRowIndex).data([
                tag,
                reportFormatId,
                reportFormatText,
                note,
            ]).draw();

            HideReportFormatDataTableColumns();

            reportTypeFormatDataTable.columns.adjust().draw();

            EnableNewOperation('report-format');

            $('#report-format-modal').modal('hide');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-report-format-dt').click(function (event) {
        isChecked = $('#tbl-report-format tbody input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-report-format tbody input[type="checkbox"]:checked').each(function () {
                    reportTypeFormatDataTable.row($('#tbl-report-format tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-report-format-dt').data('rowindex');

                    EnableNewOperation('report-format');
                    SetReportFormatUniqueDropdownList();

                    $('#select-all-report-format').prop('checked', false);
                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All
    $('#select-all-report-format').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-report-format tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = reportTypeFormatDataTable.row(row).index();
                rowData = (reportTypeFormatDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-report-format-dt').data('rowindex', arr);
                EnableDeleteOperation('report-format');
            });
        }
        else {
            EnableNewOperation('report-format');

            $('#tbl-report-format tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-report-format tbody').click('input[type=checkbox]', function () {
        $('#tbl-report-format input[type="checkbox"]:checked').each(function () {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = reportTypeFormatDataTable.row(row).index();
                rowData = (reportTypeFormatDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('report-format');

                $('#btn-update-report-format-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-report-format-dt').data('rowindex', rowData);
                $('#btn-delete-report-format-dt').data('rowindex', arr);
                $('#select-all-report-format').data('rowindex', arr);
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-report-format tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('report-format');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('report-format');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('report-format');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-report-format').prop('checked', true);
        else
            $('#select-all-report-format').prop('checked', false);
    });

    // Validate Report Format Module
    function IsValidReportFormatDataTableModal() {
        let result = true;
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        reportFormatId = $('#report-format-id option:selected').val();
        reportFormatText = $('#report-format-id option:selected').text();
        note = $('#note-Report').val();


        // Set Default Value, If Empty
        if (note === '')
            note = 'None';
        //ReportFormatId
        if ($('#report-format-id').prop('selectedIndex') < 1) {
            result = false;
            $('#report-format-id-error').removeClass('d-none');
        }

        return result;
    }

    function HideReportFormatDataTableColumns() {
        reportTypeFormatDataTable.column(1).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme General Ledger - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-general-ledger-dt').click(function () {
        event.preventDefault();
        editedGeneralLedgerId = '';
        SetGeneralLedgerUniqueDropdownList();
        SetModalTitle('general-ledger', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-general-ledger-dt').click(function () {
        debugger;
        SetModalTitle('general-ledger', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {

            columnValues = $('#btn-edit-general-ledger-dt').data('rowindex');

            editedGeneralLedgerId = columnValues[1];
            id = $('#general-ledger-modal').attr('id');
            myModal = $('#' + id).modal();

            SetGeneralLedgerUniqueDropdownList();
            $('#scheme-general-ledger-id', myModal).val(columnValues[1]);

            activationDate = new Date(columnValues[3]);
            expiryDate = new Date(columnValues[4]);
            closeDate = new Date(columnValues[5]);

            $('#activation-date-general-ledger', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-general-ledger', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-general-ledger', myModal).val(GetInputDateFormat(closeDate));
            $('#note-general-ledger', myModal).val(columnValues[6]);

            // Show Selected Dropdown List Item
            $('#scheme-general-ledger-id').find('option[value="' + columnValues[1] + '"]').show();

            // Show Modal
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-general-ledger-dt').addClass('read-only');
            $('#general-ledger-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-general-ledger-modal').click(function (event) {
        if (IsValidGeneralLedgerDataTableModal()) {
            row = generalLedgerDataTable.row.add([
                tag,
                generalLedgerId,
                generalLedgerIdText,
                activationDate,
                expiryDate,
                closeDate,
                note,
            ]).draw();

            rowNum++;

            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideGeneralLedgerDataTableColumns();

            generalLedgerDataTable.columns.adjust().draw();

            // Hide Table Required Data Error Message
            $('#general-ledger-accordion-error').addClass('d-none');

            EnableNewOperation('general-ledger');

            $('#general-ledger-modal').modal('hide');
        }
    });

    // Modal Update Button Event
    $('#btn-update-general-ledger-modal').click(function (event) {
        $('#select-all-general-ledger').prop('checked', false);

        if (IsValidGeneralLedgerDataTableModal()) {
            generalLedgerDataTable.row(selectedRowIndex).data([
                tag,
                generalLedgerId,
                generalLedgerIdText,
                activationDate,
                expiryDate,
                closeDate,
                note,
            ]).draw();

            HideGeneralLedgerDataTableColumns();

            generalLedgerDataTable.columns.adjust().draw();

            EnableNewOperation('general-ledger');

            $('#general-ledger-modal').modal('hide');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-general-ledger-dt').click(function (event) {
        isChecked = $('#tbl-general-ledger tbody input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-general-ledger tbody input[type="checkbox"]:checked').each(function () {
                    generalLedgerDataTable.row($('#tbl-general-ledger tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();
                    //rowData = $('#btn-delete-general-ledger-dt').data('rowindex');
                    debugger;
                    EnableNewOperation('general-ledger');
                    SetGeneralLedgerUniqueDropdownList();

                    $('#select-all-general-ledger').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (!generalLedgerDataTable.data().any())
                        $('#general-ledger-accordion-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#select-all-general-ledger').click(function () {
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);

            $('#tbl-general-ledger tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = generalLedgerDataTable.row(row).index();
                rowData = (generalLedgerDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-general-ledger-dt').data('rowindex', arr);

                EnableDeleteOperation('general-ledger');
            });
        }
        else {
            EnableNewOperation('general-ledger');

            $('#tbl-general-ledger tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-general-ledger tbody').click('input[type=checkbox]', function () {
        $('#tbl-general-ledger input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = generalLedgerDataTable.row(row).index();
                rowData = (generalLedgerDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('general-ledger');

                $('#btn-update-general-ledger-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-general-ledger-dt').data('rowindex', rowData);
                $('#btn-delete-general-ledger-dt').data('rowindex', arr);
                $('#select-all-general-ledger').data('rowindex', arr);
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-general-ledger tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('general-ledger');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('general-ledger');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('general-ledger');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-general-ledger').prop('checked', true);
        else
            $('#select-all-general-ledger').prop('checked', false);
    });

    // Validate Agent Incentive Module
    function IsValidGeneralLedgerDataTableModal() {
        debugger;
        let result = true;
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        generalLedgerId = $('#scheme-general-ledger-id option:selected').val();
        generalLedgerIdText = $('#scheme-general-ledger-id option:selected').text();
        activationDate = $('#activation-date-general-ledger').val();
        expiryDate = $('#expiry-date-general-ledger').val();
        closeDate = $('#close-date-general-ledger').val();
        note = $('#note-general-ledger').val();

        // Set Default Value, If Empty
        if (note == '')
            note = 'None';

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-general-ledger');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-general-ledger');

        debugger;
        //General Ledger Id
        if ($('#scheme-general-ledger-id').prop('selectedIndex') < 1) {
            result = false;
            $('#scheme-general-ledger-id-error').removeClass('d-none');
        }


        //Activation date
        if (isValidActivationDate === false) {
            result = false;
            $('#activation-date-general-ledger-error').removeClass('d-none');
        }

        //Expiry Date
        if (isValidExpiryDate === false) {
            result = false;
            $('#expiry-date-general-ledger-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideGeneralLedgerDataTableColumns() {
        generalLedgerDataTable.column(1).visible(false);
        generalLedgerDataTable.column(5).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme  Business Office - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-business-office-dt').click(function () {
        event.preventDefault();
        editedBusinessOfficeId = '';
        SetBusinessOfficeUniqueDropdownList();
        SetModalTitle('business-office', 'Add');
        $('#btn-add-business-office-modal').removeClass('read-only')
        $('#business-office-id').hide();
        $('#business-office-id').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();
    });

    // DataTable Edit Button 
    $('#btn-edit-business-office-dt').click(function () {
        SetModalTitle('business-office', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-business-office-dt').data('rowindex');

            editedBusinessOfficeId = columnValues[1];
            SetBusinessOfficeUniqueDropdownList();

            id = $('#business-office-modal').attr('id');
            myModal = $('#' + id).modal();

            $('#business-office-id').removeAttr('style');
            $('#business-office-id').removeAttr('multiple');
            $('.ms-options-wrap').css('display', 'none');

            $('#business-office-id', myModal).val(columnValues[1]);
            activationDate = new Date(columnValues[3]);
            expiryDate = new Date(columnValues[4]);
            closeDate = new Date(columnValues[5]);

            $('#activation-date-business-office', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-business-office', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-business-office', myModal).val(GetInputDateFormat(closeDate));
            $('#note-business-office', myModal).val(columnValues[6]);

            // Show Modal
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-business-office-dt').addClass('read-only');
            $('#scheme-business-office-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-business-office-modal').click(function (event) {
        let businessOfficeId = [];
        let businessOfficeIdText = [];

        $('#business-office-id option:selected').each(function () {
            businessOfficeId.push($(this).val());
            businessOfficeIdText.push($(this).text());
        });

        if (IsValidBusinessOfficeDataTableModal()) {
            $('#btn-add-business-office-modal').addClass('read-only')
            for (let i = 0, j = 0; i < businessOfficeId.length, j < businessOfficeIdText.length; i++, j++) {
                row = businessOfficeDataTable.row.add([
                    tag,
                    businessOfficeId[i],
                    businessOfficeIdText[j],
                    activationDate,
                    expiryDate,
                    closeDate,
                    note
                ]).draw();

                rowNum++;

                row.nodes().to$().attr('id', 'tr' + rowNum);

                // Error Message In Span
                $('#business-office-data-table-error').addClass('d-none');

                HideColumnsBusinessOfficeDataTable();

                businessOfficeDataTable.columns.adjust().draw();

                $('#business-office-modal').modal('hide');

                EnableNewOperation('business-office');
            }
        }
    });

    // Modal Update Button Event
    $('#btn-update-business-office-modal').click(function (event) {
        $('#select-all-business-office').prop('checked', false);
        if (IsValidBusinessOfficeDataTableModal()) {
            businessOfficeDataTable.row(selectedRowIndex).data([
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

            HideColumnsBusinessOfficeDataTable()

            businessOfficeDataTable.columns.adjust().draw();

            $('#business-office-modal').modal('hide');

            EnableNewOperation('business-office');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-business-office-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-business-office tbody input[type="checkbox"]:checked').each(function () {
                    businessOfficeDataTable.row($('#tbl-business-office tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();
                    rowData = $('#btn-delete-business-office-dt').data('rowindex');
                    EnableNewOperation('business-office');

                    // Hide the element with id 'business-office-id'
                    $('#business-office-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
                    $('#business-office-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();

                    SetBusinessOfficeUniqueDropdownList();

                    $('#select-all-business-office').prop('checked', false);
                    // Display Required Error Message, If Table Has Not Any Record
                    if (!businessOfficeDataTable.data().any())
                        $('#business-office-data-table-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-business-office').click(function () {
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);

            $('#tbl-business-office tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = businessOfficeDataTable.row(row).index();

                rowData = (businessOfficeDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-business-office-dt').data('rowindex', arr);

                EnableDeleteOperation('business-office');

            });
        }
        else {
            EnableNewOperation('business-office');

            $('#tbl-business-office tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);

                EnableNewOperation('business-office');
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

                rowData = (businessOfficeDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('business-office');

                $('#btn-update-business-office-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-business-office-dt').data('rowindex', rowData);
                $('#btn-delete-business-office-dt').data('rowindex', arr);
                $('#select-all-business-office').data('rowindex', arr);
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-business-office tbody input[type="checkbox"]');

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

    // Validate Business Office Module
    function IsValidBusinessOfficeDataTableModal() {
        debugger;

        let result = true;
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        businessOfficeId = $('#business-office-id option:selected').val();
        businessOfficeText = $('#business-office-id option:selected').text();
        activationDate = $('#activation-date-business-office').val();
        expiryDate = $('#expiry-date-business-office').val();
        closeDate = $('#close-date-business-office').val();
        note = $('#note-business-office').val();

        // Set Default Value, If Empty
        if (note === '') {
            note = 'None';
        }

        let isValidActivationDate = IsValidInputDate('#activation-date-business-office');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-business-office');

        //Business Office Id
        //if ($('#business-office-id').prop('selectedIndex') < 1) {
        //    result = false;
        //    $('#business-office-id-error').removeClass('d-none');
        //}

        if (businessOfficeText == '') {
            result = false;
            $('#business-office-id-error').removeClass('d-none');
        } else
            $('#business-office-id-error').addClass('d-none');

            if ($('#business-office-id-multi-select-ul').focusout(function () {
                debugger;
                if (businessOfficeId === '') {
                    result = false;
                    $('#business-office-id-error').removeClass('d-none');
            } else
                    $('#business-office-id-error').addClass('d-none');
            }));

            if ($('.ms-selectall').focusout(function () {
               debugger;
              if (businessOfficeId === '') {
              result = false;
              $('#business-office-id-error').removeClass('d-none');
            } else
              $('#business-office-id-error').addClass('d-none');
            }));

        //Activation Date
        if (isValidActivationDate === false) {
            result = false;
            $('#activation-date-business-office-error').removeClass('d-none');
        }

        //Expiry Date
        if (isValidExpiryDate === false) {
            result = false;
            $('#expiry-date-business-office-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsBusinessOfficeDataTable() {
        businessOfficeDataTable.column(1).visible(false);
        businessOfficeDataTable.column(5).visible(false);
    }

    // Page Loading Default Values
    function SetPageLoadingDefaultValues() {
        if ($('#amend-view').length > 0) {
            isAmendView = true;
        }

        if ($('#verify-view').length > 0) {
            isVerifyView = true;
        }


        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();

        // Notice Schedule Visible If & Only If Visible Any One Of SMS / Email
        isVisibleNoticeSchedule = $('#enable-sms-service').is(':checked') || $('#enable-email-service').is(':checked') ? true : false;

        if (isVisibleNoticeSchedule)
            $('#notice-schedule-card').removeClass('d-none');
        else
            $('#notice-schedule-card').addClass('d-none');

        // Round Nearest Visiblity
        if ($('.round-method:checked').val() == 'NOR') {
            $('#round-nearest-input').addClass('d-none');
            $('#round-nearest').val(0)
        } else
            $('#round-nearest-input').removeClass('d-none');

        if (isAmendView || isVerifyView) {
            NumberOfAccountUnitChangeEventFunction();
            TurnOverUnitChangeEventFunction();

            // Reset To Work Normal Page Validations
            isAmendView = false;
            isVerifyView = false;
        }
    }

    function SetReportFormatUniqueDropdownList() {
        // Show All List Items
        $('#report-format-id').html('');
        $('#report-format-id').append(REPORT_FORMAT_DROPDOWN);

        // Hide Added DropdownList Items
        $('#tbl-report-format > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');
            let myColumnValues = (reportTypeFormatDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null)
                if (myColumnValues[1] != editedReportFormatId)
                    $('#report-format-id').find('option[value="' + myColumnValues[1] + '"]').remove();
        });
    }

    function SetBusinessOfficeUniqueDropdownList() {
        // Show All List Items
        $('#business-office-id').html('');
        $('#business-office-id-multi-select-ul').html('');

        $('#business-office-id').html(BUSINESS_OFFICE_DROPDOWN);
        $('#business-office-id-multi-select-ul').html(BUSINESS_OFFICE_DROPDOWN_MULTI_SELECT_LIST);

        // To Get All Table Records
        businessOfficeDataTable.page.len(-1).draw();

        // Hide Inserted Dropdownlist Items
        $('#tbl-business-office > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');
            let myColumnValues = (businessOfficeDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null) {
                if (myColumnValues[1] != editedBusinessOfficeId) {
                    $('#business-office-id').find('option[value="' + myColumnValues[1] + '"]').remove();
                    $('.ms-options-wrap').find('input[type="checkbox"][value="' + myColumnValues[1] + '"]').closest('li').remove();
                }
            }
        });
    }

    function SetGeneralLedgerUniqueDropdownList() {
        // Show All List Items
        $('#scheme-general-ledger-id').html('');
        $('#scheme-general-ledger-id').append(GENERAL_LEDGER_DROPDOWN);

        // Hide Added DropdownList Items
        $('#tbl-general-ledger > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');
            let myColumnValues = (generalLedgerDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null)
                if (myColumnValues[1] != editedGeneralLedgerId)
                    $('#scheme-general-ledger-id').find('option[value="' + myColumnValues[1] + '"]').remove();
        });
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;

        // Check Form And Accordion Validity
        if ($('form').valid() && isValidSchemeName) {
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let closingChargesArray = new Array();
            let transferChargesArray = new Array();
            let noticeScheduleArray = new Array();
            let reportTypeFormatArray = new Array();
            let generalLedgerArray = new Array();
            let businessOfficeArray = new Array();

            // To Get All Records Of Data Table
            closingChargesDataTable.page.len(-1).draw();
            transferChargesDataTable.page.len(-1).draw();
            noticeScheduleDataTable.page.len(-1).draw();
            reportTypeFormatDataTable.page.len(-1).draw();
            generalLedgerDataTable.page.len(-1).draw();
            businessOfficeDataTable.page.len(-1).draw();

            // Accordion 1 - Application Parameter Validation, If Enable
            if (!IsValidApplicationNumberAccordionInputs())
                isValidAllInputs = false;

            // Accordion 2 - Member Number Validation, If Visible
            if (!IsValidMemberNumberAccordionInputs())
                isValidAllInputs = false;

            // Accordion 3 - Account Number Validation
            if (!IsValidCustomerAccountNumberAccordionInputs())
                isValidAllInputs = false;

            // Accordion 4 - Account Parameter Validation
            if (!IsValidAccountParameterAccordionInputs())
                isValidAllInputs = false;

            // Accordion 5 - Banking Channel Validation -- Not Applicable

            // Accordion 6 - Shares Certificate Validation
            if (!IsValidCertificateParameterAccordionInputs())
                isValidAllInputs = false;

            // Accordion 7 - Dividend Parameter Validation
            if (!IsValidDividendParameterAccordionInputs())
                isValidAllInputs = false;


            // Accordion 8 - Closing Charges Data Table Validation
            if ($('#enable-closing-charges').is(':checked')) {
                if (closingChargesDataTable.data().any()) {
                    $('#closing-charges-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {
                        // Get Data Table Values In Notice Schedule Array
                        $('#tbl-closing-charges tbody tr').each(function () {
                            currentRow = $(this).closest('tr');
                            columnValues = (closingChargesDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                closingChargesArray.push(
                                    {
                                        'GeneralLedgerId': columnValues[1],
                                        'FromTimePeriodInDays': columnValues[3],
                                        'ToTimePeriodInDays': columnValues[4],
                                        'MinimumChargesAmount': columnValues[5],
                                        'MaximumChargesAmount': columnValues[6],
                                        'IsTaxable': columnValues[7],
                                        'IsApplicableOnDeath': columnValues[8],
                                        'Note': columnValues[9],
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#closing-charges-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Accordion 9 - Transfer Charges Data Table Validation
            if ($('#enable-transfer-charges').is(':checked')) {
                if (transferChargesDataTable.data().any()) {
                    $('#transfer-charges-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {
                        $('#tbl-transfer-charges tbody tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (transferChargesDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                transferChargesArray.push(
                                    {
                                        'GeneralLedgerId': columnValues[1],
                                        'FromTimePeriodInDays': columnValues[3],
                                        'ToTimePeriodInDays': columnValues[4],
                                        'ChargesBase': columnValues[5],
                                        'MinimumChargesAmount': columnValues[7],
                                        'MaximumChargesAmount': columnValues[8],
                                        'IsTaxable': columnValues[9],
                                        'IsApplicableOnDeath': columnValues[10],
                                        'Note': columnValues[11],
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#transfer-charges-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Accordion 11 - Notice Schedule Data Table Validation (Optional, Not Mandatory)
            if (noticeScheduleDataTable.data().any()) {
                $('#notice-data-table-error').addClass('d-none');

                if (isValidAllInputs) {
                    // Get Data Table Values In Notice Schedule Array
                    $('#tbl-notice-schedule tbody tr').each(function () {
                        currentRow = $(this).closest('tr');
                        columnValues = (noticeScheduleDataTable.row(currentRow).data());

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues != 'undefined' && columnValues != null) {
                            noticeScheduleArray.push(
                                {
                                    'NoticeTypeId': columnValues[1],
                                    'CommunicationMediaId': columnValues[3],
                                    'ScheduleId': columnValues[5],
                                    'Note': columnValues[7],
                                });
                        }
                        else
                            return false;
                    });
                }
            }

            // Accordion 12 - Report Format Data Table Validation (Optional, Not Mandatory)
            if (!$('#report-format-card').hasClass('d-none')) {
                if (reportTypeFormatDataTable.data().any()) {
                    if (isValidAllInputs) {
                        $('#tbl-report-format tbody tr').each(function () {
                            currentRow = $(this).closest('tr');
                            columnValues = (reportTypeFormatDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                reportTypeFormatArray.push(
                                    {
                                        'ReportTypeFormatId': columnValues[1],
                                        'Note': columnValues[3],
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
            }

            // Accordion 13 - Target Estimation Parameter Validation
            if (!IsValidTargetEstimationAccordionInputs())
                isValidAllInputs = false;

            // Accordion 14 - Limit Parameter Validation
            if (!IsValidLimitAccordionInputs())
                isValidAllInputs = false;

            // Accordion 15 - Create Array For General Ledger Data Table To Pass Data
            if (!$('#general-ledger-card').hasClass('d-none')) {
                if (generalLedgerDataTable.data().any()) {

                    $('#general-ledger-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {

                        // Get Data Table Values In Business Office Array
                        $('#tbl-general-ledger > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (generalLedgerDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {


                                generalLedgerArray.push(
                                    {
                                        'GeneralLedgerId': columnValues[1],
                                        'ActivationDate': columnValues[3],
                                        'ExpiryDate': columnValues[4],
                                        'CloseDate': columnValues[5],
                                        'Note': columnValues[6],
                                    });
                            }
                            else
                                return false;
                        });
                        // isValidAllInputs = true;
                    }
                }
                else {
                    $('#general-ledger-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Accordion 16 - Business Office Data Table Validation
            if (!$('#business-office-card').hasClass('d-none')) {
                if (businessOfficeDataTable.data().any()) {
                    $('#business-office-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {
                        $('#tbl-business-office tbody tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (businessOfficeDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                businessOfficeArray.push(
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
                    $('#business-office-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            if (isValidAllInputs) {
                //Call Cantroller Save Data Table Method 
                $.ajax(
                    {
                        url: saveDataTableUrl,
                        type: 'POST',
                        data: { '_schemeNoticeSchedule': noticeScheduleArray, '_schemeReportFormat': reportTypeFormatArray, '_schemeGeneralLedger': generalLedgerArray, '_schemeBusinessOffice': businessOfficeArray, '_schemeSharesTransferCharges': transferChargesArray, '_schemeClosingCharges': closingChargesArray },
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
