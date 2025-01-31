
//function HideAccordion()
//{
//    let className = id.replace('enable', '.' + '-parameter-accordian');

//    if ($(this).is(':checked'))
//        $(className).removeClass('d-none');
//    else
//        $(className).addClass('d-none');
//}

'use strict'
$(document).ready(function ()
{
    debugger;
    // ************** D E C L A R A T I O N  ***************
    // String Literals

    // Constants
    const ALL = 'All';
    const MANDATORY_VALUE = 'M';
    const DISABLE_VALUE = 'D';
    const FLAT_AMOUNT = 'F';
    const NUMBER_UNIT = 'N';
    const PERCENTAGE = 'P';

    // All Values Get From Account Class 
    const DEMAND_DEPOSIT = 'DMN';
    const FIXED_DEPOSIT = 'FDP';
    const RECURRING_DEPOSIT = 'REC';

  
    // Constant For Dropdown
    const BUSINESS_OFFICE_DROPDOWN = $('#business-office-id').html();
    const BUSINESS_OFFICE_DROPDOWN_MULTI_SELECT_LIST = $('#business-office-id-multi-select-ul').html();
    const REPORT_FORMAT_DROPDOWN = $('#report-format-id').html();

    // Assing Value On Deposit Type Selection
    let GENERAL_LEDGER_DROPDOWN = '';

    let id = '';
    let activationDate;
    let expiryDate;
    let closeDate;
    let depositType = '';

    let eventObjId = [];
    let len = 0;

    let minimum = 0;
    let maximum = 0;
    let multiSelectCount = 0;
    let dropdownListItems = '';
    let listItemCount = 0;

    let isVerifyView = false;
    let isAmendView = false;

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
    let minimumLength = 0;
    let maximumLength = 0;
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;

    let arr = new Array();
    let result = true;

    let maxTurnOverLimit = 0;

    // TargetGroup
    let genderId = '';
    let occupationId = '';
    let requiredMember;
    let requiredMemberText;
    let targetGroupId = '';
    let targetGroupIdText = '';
    let valueId = '';
    let valueText = '';

    //Limit Accordion
    let retailAccountTurnOverLimit = 0;
    let corporateAccountTurnOverLimit = 0;

    // Deposit Agent Incentive
    let minimumCollectionAmount = 0;
    let maximumCollectionAmount = 0;
    let incentiveUnit = '';
    let incentiveUnitText = '';
    let incentive = 0;
    let roundingMethod = '';
    let roundingMethodText = '';
    let note = '';

    //InterestPayoutFrequency

    //NumberOfTransactionLimit
    let transactionType = '';
    let transactionTypeText = '';
    let minimumNumberOfTransaction = 0;
    let maximumNumberOfTransaction = 0;
    let timePeriodUnit = '';
    let timePeriodUnitText = '';

    //TransactionAmountLimit
    let minimumAmountLimit = 0;
    let maximumAmountLimit = 0;

    //ReportFormat
    let reportFormatId = '';
    let reportFormatText = '';
    let editedReportFormatId = '';

    //NoticeSchedule
    let noticeTypeId = '';
    let noticeScheduleText = '';
    let communicationMediaId = '';
    let communicationMediaText = '';
    let scheduleId = '';
    let scheduleText = '';

    //Charges
    let chargesGeneralLedgerId = '';
    let chargesGeneralLedgerText = '';
    let minimumChargesAmount = 0;
    let maximumChargesAmount = 0;
    let isTaxable = false;
    let tenure;
    let tenureUnit = '';
    let tenureUnitText = '';
    let tenureText = '';

    //ClosingChares
    let fromTimePeriodInDays = 0;
    let toTimePeriodInDays = 0;
    let isApplicableOnDeath = false;
    let isTimePeriodForBeforeClosure = false;

    // GeneralLedger
    let generalLedgerId = '';
    let generalLedgerIdText = '';
    let editedGeneralLedgerId = '';
    let generalLedgerDropdownListItemsByDepositType = '';

    //business office                           
    let businessOfficeId = '';
    let businessOfficeIdText = '';
    let editedBusinessOfficeId = '';

    // CreateDataTable
    let agentIncentiveDataTable = CreateDataTable('agent-incentive');
    let numberOfTransactionLimitDataTable = CreateDataTable('number-of-transaction-limit');
    let transactionAmountLimitDataTable = CreateDataTable('transaction-amount-limit');
    let reportTypeFormatDataTable = CreateDataTable('report-format');
    let noticeScheduleDataTable = CreateDataTable('notice-schedule');
    let closingChargesDataTable = CreateDataTable('closing-charges');
    let tenureListDataTable = CreateDataTable('scheme-tenure-list');
    let generalLedgerDataTable = CreateDataTable('general-ledger');
    let businessOfficeDataTable = CreateDataTable('business-office');
    let targetGroupDataTable = CreateDataTable('target-group');

    // *************** C A L L   P A G E   L O A D I N G   D E F A U L T   V A L U E S ***************

    SetPageLoadingDefaultValues();


    function SetDocumentUploadInput(_input)
    {
        // Document Upload Input Visibility Based On Selection i.e. Mandatory, Optional, Disable

        // eventObjId Is Collection Of All Accordions Document Value i.e. Mandatory, Optional, Disable
        // Using Naming Convention Based On eventObjId All Corrospondent Inputs Are Show And Hide
        debugger;
        let eventObjId = [];
        let len = 0;

        // Document Upload Visibility Based On Selecting Parameter

        // Other Remaining Accordion Visibility
        // -db-ts = database toggle switch         -ls-ts = local storage toggle switch
        // -db-tf = database text fields           -ls-tf = local storage text fields
        // -ls-pt = Local Storage Path
        let documentUploadValue = '';

        if (_input === ALL)
        {
            eventObjId = ['photo-document-upload', 'sign-document-upload'];
        }
        else
        {
            eventObjId.push(_input);
        }

        len = eventObjId.length;

        for (let i = 0; i < len; i++)
        {
            documentUploadValue = $('input[id=' + eventObjId[i] + ']:checked').val();

            if (documentUploadValue === DISABLE_VALUE || typeof documentUploadValue === 'undefined')
            {
                ResetFileUpload(eventObjId[i]);
            }
            else
            {
                // Hide Error
                $('#' + eventObjId[i] + '-error').addClass('d-none');

                // if Database Storage Is True 
                if ($('#' + eventObjId[i] + '-dbts').is(':checked')) {
                    $('.' + eventObjId[i] + '-db-ms').prop('disabled', false);
                    $('.' + eventObjId[i] + '-db-tf').addClass('mandatory-mark');
                    $('.' + eventObjId[i] + '-db-tf').prop('disabled', false);
                    $('.' + eventObjId[i] + '-db-tf').prop('min', 1);

                    $('.' + eventObjId[i] + '-ls-ts').prop('disabled', true);
                    $('.' + eventObjId + '-ls-ms').prop('disabled', true);
                    $('.' + eventObjId + '-ls-tf').prop('disabled', true);
                    $('.' + eventObjId + '-ls-pt').prop('disabled', true);
                }
                else {
                    $('.' + eventObjId[i] + '-db-ms').prop('disabled', true);
                    $('.' + eventObjId[i] + '-db-ms > option').prop('selected', false);
                    $('.' + eventObjId[i] + '-db-tf').removeClass('mandatory-mark');
                    $('.' + eventObjId[i] + '-db-tf').prop('disabled', true);
                    $('.' + eventObjId[i] + '-db-tf').prop('min', 0);
                    $('.' + eventObjId[i] + '-db-tf').val(0);

                    $('.' + eventObjId[i] + '-ls-ts').prop('disabled', false);

                    objSelect2.trigger('change');
                    //modalObjSelect2.trigger('change');
                }

                // if Local Storage Is True 
                if ($('#' + eventObjId[i] + '-lsts').is(':checked')) {
                    $('.' + eventObjId[i] + '-ls-ms').prop('disabled', false);
                    $('.' + eventObjId[i] + '-ls-tf').addClass('mandatory-mark');
                    $('.' + eventObjId[i] + '-ls-tf').prop('disabled', false);
                    $('.' + eventObjId[i] + '-ls-tf').prop('min', 1);
                    $('.' + eventObjId[i] + '-ls-pt').prop('disabled', false);
                    $('.' + eventObjId[i] + '-ls-pt').addClass('mandatory-mark');

                    $('.' + eventObjId[i] + '-db-ts').prop('disabled', true);
                    $('.' + eventObjId + '-db-ms').prop('disabled', true);
                    $('.' + eventObjId + '-db-tf').prop('disabled', true);
                }
                else
                {
                    $('.' + eventObjId[i] + '-ls-ms').prop('disabled', true);
                    $('.' + eventObjId[i] + '-ls-ms > option').prop('selected', false);
                    $('.' + eventObjId[i] + '-ls-tf').removeClass('mandatory-mark');
                    $('.' + eventObjId[i] + '-ls-tf').prop('disabled', true);
                    $('.' + eventObjId[i] + '-ls-tf').attr('min', 0);
                    $('.' + eventObjId[i] + '-ls-tf').val(0);
                    $('.' + eventObjId[i] + '-ls-pt').prop('disabled', true);
                    $('.' + eventObjId[i] + '-ls-pt').removeClass('mandatory-mark');
                    $('.' + eventObjId[i] + '-ls-pt').val('');

                    $('.' + eventObjId[i] + '-db-ts').prop('disabled', false);

                    objSelect2.trigger('change');
                    //modalObjSelect2.trigger('change');
                }
            }
        }
    }

    // Reset File Upload Of All Upload Configuration Or On Disable
    function ResetFileUpload(_uploadInputId)
    {
        // Document Upload Visibility Based On Selecting Parameter

        // Other Remaining Accordion Visibility
        // -db-ts = database toggle switch         -ls-ts = local storage toggle switch
        // -db-tf = database text fields           -ls-tf = local storage text fields
        // -ls-pt = Local Storage Path

        if (_uploadInputId === ALL)
        {
            eventObjId = ['photo-document-upload', 'sign-document-upload'];
        }
        else
        {
            eventObjId = [];
            eventObjId.push(_uploadInputId);
        }

        len = eventObjId.length;

        for (let i = 0; i < len; i++)
        {
            _uploadInputId = eventObjId[i];

            $('.' + _uploadInputId + '-ls-ts').prop('checked', false);
            $('.' + _uploadInputId + '-db-ts').prop('checked', false);

            $('.' + _uploadInputId + '-ls-ts').prop('disabled', true);
            $('.' + _uploadInputId + '-db-ts').prop('disabled', true);

            $('.' + _uploadInputId + '-ls-ms').prop('disabled', true);

            $('.' + _uploadInputId + "-ls-ms > option").prop('selected', false);

            $('.' + _uploadInputId + '-ls-pt').prop('disabled', true);
            $('.' + _uploadInputId + '-ls-pt').removeClass('mandatory-mark');
            $('.' + _uploadInputId + '-ls-pt').val('None');
            $('.' + _uploadInputId + '-ls-tf').prop('disabled', true);
            $('.' + _uploadInputId + '-ls-tf').removeClass('mandatory-mark');
            $('.' + _uploadInputId + '-ls-tf').attr('min', 0);
            $('.' + _uploadInputId + '-ls-tf').val('0');


            $('.' + _uploadInputId + '-db-ms').prop('disabled', true);

            $('.' + _uploadInputId + '-db-ms > option').prop('selected', false);

            $('.' + _uploadInputId + '-db-tf').prop('disabled', true);
            $('.' + _uploadInputId + '-db-tf').removeClass('mandatory-mark');
            $('.' + _uploadInputId + '-db-tf').attr('min', 0);
            $('.' + _uploadInputId + '-db-tf').val('0');

            $('.' + _uploadInputId + '-ls-tf').prop('disabled', true);
            $('.' + _uploadInputId + '-ls-pt').prop('disabled', true);
            $('.' + _uploadInputId + '-ls-ms').prop('disabled', true);

            $('.' + _uploadInputId + '-db-tf').prop('disabled', true);
            $('.' + _uploadInputId + '-db-ms').prop('disabled', true);

            // Clear Error Fro Ex - vehicle-photo-upload-error, vehicle-photo-upload-required-error
            $('#' + _uploadInputId + '-error').addClass('d-none');
            $('#' + _uploadInputId + '-required-error').addClass('d-none');

            // Its Object Of MultiSelect Required For Changing Effects Create At View Footer.
            objSelect2.trigger('change');
        }
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   C H A N G E    E V E N T  @@@@@@@@@@@@@@@@@@@@@@@@@@
    // On Any Document Upload Option Button Change Event (i.e. Every Mandatory, Optional, Disable)
    $('.document-upload').change(function ()
    {
        debugger;
        // Get Id
        let eventObjId = this.id;

        // If Values Are Mandatory Or Optional - Make It Writable i.e. Remove Read-Only Class
        // -db-ts = database toggle switch         -ls-ts = local storage toggle switch
        // -db-tf = database text fields           -ls-tf = local storage text fields
        // -db-ms = database multiselect           -ls-ms = local storage multi select i.e file formats

        // If Not Disable i.e For Mandatory Or Optional
        if ($(this).val() !== DISABLE_VALUE)
        {
            SetDocumentUploadInput(eventObjId);
        }
        else
        {
            ResetFileUpload(eventObjId);
        }
    });

    // On Every Toggle Switch Button Change Event (i.e. Every Enable Upload In Database Storage)
    $('.toggle-switch-db').change(function () {
        debugger;
        let eventObjId = this.id;

        SetDocumentUploadInput(eventObjId.replace('-dbts', ''));
    });

    // On Every Toggle Switch Button Change Event (i.e. Every Enable Upload In Local Storage)
    $('.toggle-switch-ls').change(function ()
    {
        debugger;
        let eventObjId = this.id;

        SetDocumentUploadInput(eventObjId.replace('-lsts', ''));
    });

    $('#enable-photo-sign').change(function () {
        ResetFileUpload(ALL);
    });

    // Interest Payout Day Other Validations
    $('#enable-periodic-interest-payout-option').change(function () {
        $('#interest-payout-day-other-input').addClass('d-none');
    });

    // Interest Payout Day Hide Nearest
    $('.interest-payout-day').change(function () {
        debugger;
        if ($('.interest-payout-day:checked').val() === 'CST')
        {
            $('#interest-payout-day-other-input').removeClass('d-none');
        }            
        else
        {
            $('#interest-payout-day-other-input').addClass('d-none');
            $('#interest-payout-day-other').val(1);
        }

    });

    // Require Extra Code To Hide Extra Code Block
    $('#enable-application').change(function (event)
    {
        $('#auto-application-number-block').addClass('d-none');
        $('#application-number-branchwise-block').removeClass('d-none');
    });

    // Auto Application Number Branchwise
    $('#enable-application-number-branchwise').change(function () {
        $('#application-number-accordion-error').addClass('d-none');
        $('#auto-application-number-block').addClass('d-none');
    });

    // Auto Account Number Branchwise
    $('#enable-account-number-branchwise').change(function () {
        $('#account-number-accordion-error').addClass('d-none');
        $('#auto-account-number-block').addClass('d-none');
    });

    // Auto Certificate Number Branchwise
    $('#enable-certificate-number-branchwise').change(function () {
        $('#certificate-number-accordion-error').addClass('d-none');
          $('#auto-certificate-number-block').addClass('d-none');
    });

    // Require Extra Code To Hide Extra Code Block
    $('#enable-passbook-detail').change(function (event)
    {
        $('#auto-passbook-number-block').addClass('d-none');
        $('#passbook-number-branchwise-block').removeClass('d-none');
    });

    // Auto Passbook Number Branchwise
    $('#enable-passbook-number-branchwise').change(function () {
        $('#passbook-detail-accordion-error').addClass('d-none');
        $('#auto-passbook-number-block').addClass('d-none');
    });

    // Agent Incentive
    $('#enable-agent-incentive').change(function ()
    {
        agentIncentiveDataTable.clear().draw();
    });

    // Deposit Type
    // Notice Schedule DataTable Validation
    $('.notice-schedule').change(function () {
        let isVisibleNoticeSchedule = $('#enable-sms-service').is(':checked') || $('#enable-email-service').is(':checked') ? true : false;

        if (isVisibleNoticeSchedule)
            $('#notice-schedule').removeClass('d-none');
        else
            $('#notice-schedule').addClass('d-none');
    })

    // Application Number For Toggle Switch Click
    $('.application-number-input').change(function () {
        IsValidApplicationNumberAccordionInputs();
    });

    // Application Number Parameter
    $('.application-number-input').focusout(function () {
        IsValidApplicationNumberAccordionInputs();
    });

    // Account Number For Toggle Switch Click
    $('.account-number-input').change(function () {
        IsValidCustomerAccountNumberAccordionInputs();
    });

    // Account Number Parameter
    $('.account-number-input').focusout(function () {
        IsValidCustomerAccountNumberAccordionInputs();
    });

    // Certificate Number Parameter For Toggle Switch Click
    $('.certificate-number-input').change(function () {
        IsValidCertificateNumberAccordionInputs();
    });

    // Account Certificate Parameter
    $('.certificate-number-input').focusout(function () {
        IsValidCertificateNumberAccordionInputs();
    });

    // Passbook Number Parameter For Toggle Switch Click
    $('.passbook-detail-input').change(function () {
        IsValidPassbookDetailAccordionInputs();
    });

    // Enable Passbook Detail Input Validation
    $('.passbook-detail-input').focusout(function () {
        IsValidPassbookDetailAccordionInputs();
    });

    // Closing Charges 
    $('#enable-closing-charges').change(function () {
        closingChargesDataTable.clear().draw();
    });

    // Transaction Amount Limit
    $('#enable-transaction-amount-limit').change(function () {
        transactionAmountLimitDataTable.clear().draw();
    });

    // Number Of Transaction Limit
    $('#enable-number-of-transaction-limit').change(function () {
        numberOfTransactionLimitDataTable.clear().draw();
    });

    // Account Renewal Parameter
    $('#enable-renewal-parameter').change(function () {
        $('#auto-renewal-block').addClass('d-none');
    });

    // Number Of Transaction Limit
    $('#enable-number-of-transaction-limit').change(function () {
        numberOfTransactionLimitDataTable.clear().draw();
    });

    // Agent Parameter
    $('#enable-agent-parameter').change(function ()
    {
        $('#agent-incentive-block').addClass('d-none');
        $('#commission-on-over-dues-installment-block').addClass('d-none');
        $('#commission-on-additional-investment-block').addClass('d-none');
        $('#is-required-security-block').addClass('d-none');
        $('#commision-deductable-on-foreclosure-block').addClass('d-none');
        $('#agent-commision-deductable-of-deceased-account-block').addClass('d-none');
    });

    // For Select 2 Focusout Input Validation
    objSelect2.on('select2:close', function (e)
    {
        debugger;
        let myId = $(this).attr('id');

        if (myId == 'account-number-mask')
            IsValidCustomerAccountNumberAccordionInputs();

        if (myId == 'application-number-mask')
            IsValidApplicationNumberAccordionInputs();

        if (myId == 'certificate-number-mask')
            IsValidCertificateNumberAccordionInputs();

        if (myId == 'passbook-number-mask')
            IsValidPassbookDetailAccordionInputs();
    })

    // Deposit Type Event Handling
    // For depositType
    $('.deposit-type').change(function (event)
    {
        debugger;
        // On Changing Deposit Type 
        // Clear All DataTables
        depositType = $('.deposit-type:checked').val();

        numberOfTransactionLimitDataTable.clear().draw();
        transactionAmountLimitDataTable.clear().draw();
        reportTypeFormatDataTable.clear().draw();
        agentIncentiveDataTable.clear().draw();
        noticeScheduleDataTable.clear().draw();
        closingChargesDataTable.clear().draw();
        tenureListDataTable.clear().draw();
        businessOfficeDataTable.clear().draw();
        generalLedgerDataTable.clear().draw();

        // Mark Out Select All Check Box Of All Datatables.
        $('input[name="select-all"]').prop('checked', false);

        // Clear Accordion Title Error Messages
        $('.accordion-title-error').addClass('d-none');
        $('#deposit-type-required-error').addClass('d-none');

        // Make False All Toggle Switch
        $('.switch-input').prop('checked', false);

        //Clear all number fiels and textarea
        $('input[type="number"]').val('');

        //Clear all TextArea Except Note and their Translation Field
        $('textarea').not('#note, #trans-note').val('');

        // Clear dropdowns and MultiSelect to the first option except deposit Type
        $('select').prop('selectedIndex', 0);

        //Clear all text fields except name of Scheme And their Translation Field
        $('input[type="text"]').not('#name-of-scheme, #trans-name-of-scheme, #alias-name, #trans-alias-name, #name-on-report, #trans-name-on-report').val('');

        // Clear radio input
        $('input[type="radio"]').not('.deposit-type').prop('checked', false);

        $('.demand-deposit').removeClass('d-none');
        $('.fixed-deposit').removeClass('d-none');
        $('.recurring-deposit').removeClass('d-none');

        // Demand Deposit Setting
        if ($('.deposit-type:checked').val() === DEMAND_DEPOSIT)
            $('.demand-deposit').addClass('d-none');

        // Demand Deposit Setting
        if ($('.deposit-type:checked').val() === FIXED_DEPOSIT)
            $('.fixed-deposit').addClass('d-none');

        // Demand Deposit Setting
        if ($('.deposit-type:checked').val() === RECURRING_DEPOSIT)
            $('.recurring-deposit').addClass('d-none');

        $('#application-number-branchwise-block').removeClass('d-none');
        $('#account-number-branchwise-block').removeClass('d-none');
        $('#certificate-number-branchwise-block').removeClass('d-none');

        // Set GeneralLedger Dropdown List
        SetGeneraLedgerDropdownListByDepositType();

        // Hide All Accordion Based On Toggle Switch
        SetToggleSwitchBasedAccordions();
    });

    // Set Genereral ledger Dropdown list By DepositType
    function SetGeneraLedgerDropdownListByDepositType()
    {
        debugger;
        /*let depositGLUrl = '';*/

        depositType = $('.deposit-type:checked').val();

        $('#scheme-general-ledger-id').html('');

        //// Demand Deposit
        //if (depositType === DEMAND_DEPOSIT)
        //{
        //    depositGLUrl = '/DynamicDropdownList/GetDemandDepositGeneralLedgerDropdownList';
        //}

        //// Fixed / Term Deposit
        //if (depositType === FIXED_DEPOSIT)
        //{
        //    depositGLUrl = '/DynamicDropdownList/GetTermDepositGeneralLedgerDropdownList';
        //}

        //// Recurring Deposit
        //if(depositType === RECURRING_DEPOSIT)
        //{
        //    depositGLUrl = '/DynamicDropdownList/GetRecurringDepositGeneralLedgerDropdownList';
        //}

        //$.get(depositGLUrl, { async: false }, function (data)
        //{
        //    dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select General Ledger --- </option>';

        //    $.each(data, function (index, selectListItemObj)
        //    {
        //        dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
        //    });

        //    $('#scheme-general-ledger-id').html(dropdownListItems);

        //    listItemCount = $('#scheme-general-ledger-id > option').not(':first').length;

        //    // Select Default First Record, If Dropdown Has Only One Record
        //    if (listItemCount == 1)
        //    {
        //        $('#scheme-general-ledger-id').prop('selectedIndex', 1);
        //    }

        //    GENERAL_LEDGER_DROPDOWN = $('#scheme-general-ledger-id').html();
        //});

        $.get('/DynamicDropdownList/GetGeneralLedgerDropdownListByAccountClassCode', { _accountClassCode: depositType, async: false }, function (data) {
            debugger;
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select General Ledger --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#scheme-general-ledger-id').html(dropdownListItems);

            generalLedgerDropdownListItemsByDepositType = dropdownListItems;

            listItemCount = $('#scheme-general-ledger-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#scheme-general-ledger-id').prop('selectedIndex', 1);
                $('#scheme-general-ledger-id').change();
            }
        });
    }
    

    // @@@@@@@@@@@@@@@@@@@@@@@@@@  F O C U S O U T   E V E N T  @@@@@@@@@@@@@@@@@@@@@@@@@@

    function TimePeriodUnitFocusOutEventFunction()
    {
        let timePeriodUnitId = $('#time-period-unit-id option:selected').val();

        // Fetch the time period unit name by its ID
        $.get('/AccountChildAction/GetTimePeriodUnitSysNameById', { _timePeriodUnitId: timePeriodUnitId, async: false }, function (sysTimePeriodUnit, textStatus, jqXHR)
        {
            debugger;
                if (sysTimePeriodUnit === 'Year')
                {
                    // For Cash Credit Loan
                    $('#minimum-tenure').attr('min', 1).attr('max', 99);
                    $('#maximum-tenure').attr('max', 99);
                }
                else if (sysTimePeriodUnit === 'Month')
                {
                    $('#minimum-tenure').attr('min', 1).attr('max', 12);
                    $('#maximum-tenure').attr('max', 12);
                }
                else if (sysTimePeriodUnit === 'Day')
                {
                    $('#minimum-tenure').attr('min', 1).attr('max', 31);
                    $('#maximum-tenure').attr('max', 31);
                }
        })
    }

    //Validation On Focusout in Tenure
    $('#time-period-unit-id').change(function ()
    {
        debugger;
        if (isVerifyView === false)
        {
            $('#minimum-tenure').val('');
            $('#maximum-tenure').val('');
            $('#tenure-multiple-of').val('');
            $('#default-tenure').val('');
        }

        TimePeriodUnitFocusOutEventFunction();
    });

    // -- Name Of Scheme Validation (Unique)
    $('#name-of-scheme').focusout(function (event)
    {
        let nameOfScheme = $('#name-of-scheme').val();

        $.get('/AccountChildAction/IsUniqueDepositSchemeName', { _nameOfScheme: nameOfScheme, async: false }, function (data, textStatus, jqXHR) {
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

    // Account Parameter Input Validation
    $('.account-parameter-input').focusout(function () {
        if (IsValidAccountParameterAccordionInputs())
            $('#account-parameter-accordion-error').addClass('d-none');
    });

    //ForSchemeInterestRate
    $('.rate-of-interest-unit').focusout(function () {
        $('#rate-of-interest').val('');
    });

    //SchemeDepositInterestParmeter 
    $('#deposit-amount-to').focusout(function () {
        $('#deposit-amount-to').attr('min', $('#deposit-amount-from').val());
    });

    $('#deposit-amount-from').focusout(function () {
        $('#deposit-amount-to').val('');
    });

    $('#rate-of-interest').focusout(function () {

        if ($('input[name="SchemeDepositInterestParameterViewModel.RateOfInterestUnit"]:checked') == 'PER') {
            $('#rate-of-interest').attr('min', -19);
            $('#rate-of-interest').attr('max', 19);
        }
        else {
            $('#rate-of-interest').attr('min', -999999);
            $('#rate-of-interest').attr('max', 999999);
        }
    });

    //Limit accordion Validation
    $('#retail-account-turn-over-limit').focusout(function () {
        let retailAccountTurnOverLimit = parseFloat($('#retail-account-turn-over-limit').val());

        // Allow Hundred Times Turn Over Limit
        maxTurnOverLimit = retailAccountTurnOverLimit * 100;
    })

    //Turn Over LimitValidation
    $('#turn-over-limit').focusout(function () {
        if (maxTurnOverLimit >= $(this).val()) {
            $('#turn-over-limit-error').removeClass('d-none');
        }
        else {
            $('#turn-over-limit-error').addClass('d-none');
        }

    })

    // Minimum Business Experience
    $('#minimum-collection-amount').focusout(function () {
        debugger;
        $('#maximum-collection-amount').attr('min', parseFloat($(this).val()));
    });

    $('#minimum-amount-limit').focusout(function () {
        debugger;
        $('#maximum-amount-limit').attr('min', parseFloat($(this).val()));
    });

    // RetailAccountTurnOverLimit Validation
    $('#retail-account-turn-over-limit').focusout(function ()
    {
        let retailAccountTurnOver = parseFloat($(this).val());
        let retailAccountTurnOverLimitMaximumSharesHolding = parseFloat(retailAccountTurnOverLimit);

        $('#turn-over-limit').val(parseFloat((retailAccountTurnOver * 100) + 1))

        if (retailAccountTurnOver <= retailAccountTurnOverLimitMaximumSharesHolding)
            $('#retail-account-turn-over-limit-error').addClass('d-none');
        else {
            $('#retail-account-turn-over-limit-error').removeClass('d-none')
        }
    })

    //// CorporateAccountTurnOverLimit Validation
    $('#corporate-account-turn-over-limit').focusout(function ()
    {
        let corporateAccount = parseFloat($(this).val());
        let corporateAccountMaximumSharesHolding = parseFloat(corporateAccountTurnOverLimit);

        if (corporateAccount <= corporateAccountMaximumSharesHolding)
            $('#corporate-account-turn-over-limit-error').addClass('d-none');
        else {
            $('#corporate-account-turn-over-limit-error').removeClass('d-none')
        }
    })

    // Target Group Data Table
    $('#target-group-id').focusout(function () {
        debugger;
        let targetId = $('#target-group-id option:selected').text();

        if (targetId.indexOf('Occupation') > -1) {
            $('.occupation').removeClass('d-none');
        }
        else {
            $('.occupation').addClass('d-none');
            $('#gender-id').val('');
        }

        if (targetId.indexOf('Gender') > -1) {
            $('.gender').removeClass('d-none');
        }
        else {
            $('.gender').addClass('d-none');
            $('#occupation-id').val('');
        }
    });

    // Attach change event listener to the checkbox
    $('#enable-tenure').change(function ()
    {
        debugger;
        // Check if the checkbox is checked
        let isChecked = $(this).is(':checked');

        if (isChecked)
        {
            // Hide fields when enable-tenure is true
            $('#tenure-list-block').addClass('d-none');
            $('.tenure-list').addClass('d-none');
            $('.tenure-list').addClass('d-none');
            $('#tenure-accordion-error').addClass('d-none');
            // Hide Error Msg And Clear Data Table
            $('#tenure-required-error').addClass('d-none');
            tenureListDataTable.clear().draw();
        }
        else {
            // Show fields when enable-tenure is false
            $('.tenure-list').removeClass('d-none');
            $('#tenure-list-block').addClass('d-none');
            $('#enable-tenure-list').prop('checked', false);
            $('.tenure-parameter-input').val('');
        }

    });

    // Attach change event listener to the checkbox
    $('#enable-tenure-list').change(function ()
    {
        debugger;
        // Check if the checkbox is checked
        let isChecked = $(this).is(':checked');

        if (isChecked)
        {
            // Hide fields when enable-tenure is true
            $('#tenure-block').addClass('d-none');
            $('#enable-tenure-input').addClass('d-none');

            // Hide Error Msg And Clear Data Table
            $('#tenure-required-error').addClass('d-none');
            tenureListDataTable.clear().draw();
        }
        else {
            // Show fields when enable-tenure is false
            $('#enable-tenure-input').removeClass('d-none');
            $('#enable-tenure').prop('checked', false);
            $('#tenure-block').addClass('d-none');
        }

    });

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

    // Agent Incentive Data Table
    $('.incentive-unit').change(function () {
        if ($(this).val() === 'F') {
            $('#incentive').attr('max', 99999);

            if ($('#incentive').val() > 99999)
                $('#incentive').val(99999);
        }
        else {
            $('#incentive').attr('max', 20);

            if ($('#incentive').val() > 20)
                $('#incentive').val(20);
        }
    });

    //Target Estimation Accordion Input Validation 
    $('.target-estimation-input').focusout(function () {
        IsValidTargetEstimationAccordionInputs();
    });

    //Target Estimation Accordion Input Validation 
    $('.target-estimation-radio-input').change(function () {
        IsValidTargetEstimationAccordionInputs();
    });

    // Limit Parameter Accordion Input Validation
    $('.limit-parameter-input').focusout(function () {
        IsValidLimitAccordionInputs();
    });

    // Demand Deposit Detail Accordion Input Validation
    $('.demand-deposit-input').focusout(function () {
        IsValidDemandDepositDetailAccordionInputs();
    });

    //Demand Deposit Detail Accordion Input Validation
    $('.photo-input').focusout(function () {
        IsValidPhotoInputs();
    });

    //Demand Deposit Detail Accordion Input Validation
    $('.sign-input').focusout(function () {
        IsValidSignInputs();
    });

    // Fixed Deposit Accordion Input Validation
    $('.fixed-deposit-input').focusout(function () {
        IsValidFixedDepositAccordionInputs();
    });

    // Enable Tenure Accordion Input Validation
    $('.tenure-parameter-input').focusout(function () {
        IsValidTenureAccordionInputs();
    });

    // Interest Rate Input Validation 
    $('.interest-parameter-input').focusout(function () {
        IsValidInterestRateAccordionInputs();
    });

    // Interest Rate Input Validation 
    $('.interest-parameter-radio-input').change(function () {
        IsValidInterestRateAccordionInputs();
    });

    // Enable Interest Provision Input Validation
    $('.interest-provision-input').focusout(function () {
        IsValidInterestProvisionAccordionInputs();
    });

    // Enable Passbook Detail Input Validation
    $('.passbook-detail-input').focusout(function () {
        IsValidPassbookDetailAccordionInputs();
    });

    // Agent Parameter Input Validation 
    $('.agent-input').focusout(function () {
        IsValidAgentParameterAccordionInputs();
    });

    // Agent Parameter Input Validation 
    $('.agent-radio-input').change(function () {
        IsValidAgentParameterAccordionInputs();
    });

    // Enable Installment Parameter Input Validation
    $('.installment-parameter-input').focusout(function () {
        IsValidInstallmentParameterAccordionInputs();
    });

    // Renewal Parameter Input Validation 
    $('.renewal-parameter-input').focusout(function () {
        IsValidRenewalParameterAccordionInputs();
    });

    // Renewal Parameter Input Validation 
    $('.renewal-parameter-radio-input').change(function () {
        IsValidRenewalParameterAccordionInputs();
    });

    // Account Closure Parameter
    $('.account-closure-input').focusout(function () {
        IsValidAccountClosureAccordionInputs();
    });

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   FUNCTIONS FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // 1.Tenure Accordion Input Validation
    function IsValidTenureAccordionInputs()
    {
        result = true;

        if ($('#enable-tenure').is(':checked'))
        {
            let minimumTenure = parseInt($('#minimum-tenure').val());
            let multiplesOf = parseInt($('#tenure-multiple-of').val());
            let maximumTenure = parseInt($('#maximum-tenure').val());
            let defaultTenure = parseInt($('#default-tenure').val());

            if ($('#time-period-unit-id').prop('selectedIndex') < 1)
            {
                result = false; 
            }

            //minimumTenure
            if (isNaN(minimumTenure) === false)
            {
                minimum = parseInt($('#minimum-tenure').attr('min'));
                maximum = parseInt($('#minimum-tenure').attr('max'));

                if (parseInt(minimumTenure) < parseInt(minimum) || parseInt(minimumTenure) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //multiplesOf
            if (isNaN(multiplesOf) === false)
            {
                minimum = parseInt($('#tenure-multiple-of').attr('min'));
                maximum = parseInt($('#tenure-multiple-of').attr('max'));

                if (parseInt(multiplesOf) < parseInt(minimum) || parseInt(multiplesOf) > parseInt(maximum))
                {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //maximumTenure
            if (isNaN(maximumTenure) === false) {
                minimum = parseInt($('#maximum-tenure').attr('min'));
                maximum = parseInt($('#maximum-tenure').attr('max'));

                if (parseInt(maximumTenure) < parseInt(minimum) || parseInt(maximumTenure) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //defaultTenure
            if (isNaN(defaultTenure) === false)
            {
                minimum = parseInt($('#default-tenure').attr('min'));
                maximum = parseInt($('#default-tenure').attr('max'));

                if (parseInt(defaultTenure) < parseInt(minimum) || parseInt(defaultTenure) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        if (result)
            $('#tenure-accordion-error').addClass('d-none');
        else
            $('#tenure-accordion-error').removeClass('d-none');

        return result;
    }

    // 5. Shares Certificate Parameter Accordion Input Validation
    function IsValidCertificateNumberAccordionInputs() {
        debugger;
        multiSelectCount = 0;

        result = true;

        if ($('#enable-auto-certificate-number').is(':checked')) {
            let startCertificateNumber = parseInt($('#start-certificate-number').val());
            let endCertificateNumber = parseInt($('#end-certificate-number').val());
            let certificateNumberIncrementBy = parseInt($('#certificate-number-increment-by').val());

            multiSelectCount = parseInt($('#certificate-number-mask option:selected').length);

            // Certificate Number Mask
            if (multiSelectCount === 0)
                result = false;

            // Start Certificate Number
            if (isNaN(startCertificateNumber) === false) {
                minimum = parseInt($('#start-certificate-number').attr('min'));
                maximum = parseInt($('#start-certificate-number').attr('max'));

                if (parseInt(startCertificateNumber) < parseInt(minimum) || parseInt(startCertificateNumber) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // End Certificate Number
            if (isNaN(endCertificateNumber) === false) {
                minimum = parseInt($('#end-certificate-number').attr('min'));
                maximum = parseInt($('#end-certificate-number').attr('max'));

                if ((parseInt(startCertificateNumber) + 100) > parseInt(endCertificateNumber) || parseInt(endCertificateNumber) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Certificate Number Increment By
            if (isNaN(certificateNumberIncrementBy) === false) {
                minimum = parseInt($('#application-number-increment-by').attr('min'));
                maximum = parseInt($('#application-number-increment-by').attr('max'));

                if (parseInt(certificateNumberIncrementBy) === 0 || parseInt(certificateNumberIncrementBy) > parseInt(((parseInt(endCertificateNumber) - parseInt(startCertificateNumber)) / 100)))
                    result = false;
            }
            else
                result = false;
        }

        if (result)
            $('#certificate-number-accordion-error').addClass('d-none');
        else
            $('#certificate-number-accordion-error').removeClass('d-none');

        return result;
    }

    // 2. Application Number Accordion Input Validation
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
            $('#application-number-accordion-error').addClass('d-none');
        else
            $('#application-number-accordion-error').removeClass('d-none');

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

            //Account Number Mask
            if (multiSelectCount === 0)
                result = false;

            //Start Application Number
            if (isNaN(startAccountNumber) === false) {
                minimum = parseInt($('#start-account-number').attr('min'));
                maximum = parseInt($('#start-account-number').attr('max'));

                if (parseInt(startAccountNumber) < parseInt(minimum) || parseInt(startAccountNumber) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            //End Account Number
            if (isNaN(endAccountNumber) === false) {
                minimum = parseInt($('#end-account-number').attr('min'));
                maximum = parseInt($('#end-account-number').attr('max'));

                if (parseInt(endAccountNumber) < (parseInt(startAccountNumber) + 100) || parseInt(endAccountNumber) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            //Account Number Increment By
            if (isNaN(accountNumberIncrementBy) === false) {
                minimum = parseInt($('#account-number-increment-by').attr('min'));
                maximum = parseInt($('#account-number-increment-by').attr('max'));

                if (parseInt(accountNumberIncrementBy) === 0 || parseInt(accountNumberIncrementBy) < parseInt(minimum))
                    result = false;
            }
            else
                result = false;
        }

        if (result)
            $('#account-number-accordion-error').addClass('d-none');
        else
            $('#account-number-accordion-error').removeClass('d-none');

        return result;
    }

    // 5. Account Parameter Accordion Input Validation
    function IsValidAccountParameterAccordionInputs() {
        let timePeriodForNewCustomerFlag = parseInt($('#time-period-for-new-customer-flag').val());
        let isApplicableForTaxExempt = $('#enable-is-applicable-for-tax-exempt').is(':checked') ? true : false;
        let maximumTaxExemptAmount = parseFloat($('#maximum-tax-exempt-amount').val());

        // Maturity Date Override
        let EnableMaturityDateOverride = $('#enable-maturity-date-override').is(':checked') ? true : false;
        let minimumOverridePeriod = parseInt($('#minimum-override-period').val());
        let maximumOverridePeriod = parseInt($('#maximum-override-period').val());

        //Number Of Joint Account Holding Limit
        let EnableNumberOfJointAccountHoldingLimit = $('#enable-number-of-joint-account-holding-limit').is(':checked') ? true : false;
        let minimumJointAccountHolder = parseInt($('#minimum-joint-account-holder').val());
        let maximumJointAccountHolder = parseInt($('#maximum-joint-account-holder').val());
        let defaultJointAccountHolder = parseInt($('#default-joint-account-holder').val());

        //Number Of Nominee Limit
        let EnableNumberOfNomineeLimit = $('#enable-number-of-nominee-limit').is(':checked') ? true : false;
        let minimumNominee = parseInt($('#minimum-nominee').val());
        let maximumNominee = parseInt($('#maximum-nominee').val());
        let defaultNominee = parseInt($('#default-nominee').val());

        result = true;

        if ($('#heading-account-parameter').hasClass('d-none') === false) {
            // Time Period For New Customer Flag
            if (isNaN(timePeriodForNewCustomerFlag) === false) {
                minimum = parseInt($('#time-period-for-new-customer-flag').attr('min'));
                maximum = parseInt($('#time-period-for-new-customer-flag').attr('max'));

                if (parseInt(timePeriodForNewCustomerFlag) < parseInt(minimum) || parseInt(timePeriodForNewCustomerFlag) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Maturity Date Override
            if (EnableMaturityDateOverride === true) {
                // Minimum Override Period
                if (isNaN(minimumOverridePeriod) === false) {
                    minimum = parseInt($('#minimum-override-period').attr('min'));
                    maximum = parseInt($('#minimum-override-period').attr('max'));

                    if (parseInt(minimumOverridePeriod) < parseInt(minimum) || parseInt(minimumOverridePeriod) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                // Maximum Override Period
                if (isNaN(maximumOverridePeriod) === false) {
                    minimum = parseInt($('#maximum-override-period').attr('min'));
                    maximum = parseInt($('#maximum-override-period').attr('max'));

                    if (parseInt(maximumOverridePeriod) < parseInt(minimum) || parseInt(maximumOverridePeriod) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            //  Joint Account Holding Limit
            if (EnableNumberOfJointAccountHoldingLimit === true) {
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

                    if (parseInt(maximumJointAccountHolder) < parseInt(minimum) || parseInt(maximumJointAccountHolder) > parseInt(maximum))
                        result = false;
                }
                else {
                    result = false;
                }

                // Default Joint Account Holder
                if (isNaN(defaultJointAccountHolder) === false) {
                    minimum = parseInt($('#default-joint-account-holder').attr('min'));
                    maximum = parseInt($('#default-joint-account-holder').attr('max'));

                    if (parseInt(defaultJointAccountHolder) < parseInt(minimum) || parseInt(defaultJointAccountHolder) > parseInt(maximum))
                        result = false;
                }
                else {
                    result = false;
                }
            }

            // Nominee Limit
            if (EnableNumberOfNomineeLimit === true) {
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
                        result = false
                    }
                }
                else {
                    result = false;
                }

                // Default Nominee
                if (isNaN(defaultNominee) === false) {
                    // Maximum Loan Amount For Individual
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

            if (isApplicableForTaxExempt === true)
            {
                if (isNaN(maximumTaxExemptAmount) === false)
                {
                    minimum = parseFloat($('#maximum-tax-exempt-amount').attr('min'));
                    maximum = parseFloat($('#maximum-tax-exempt-amount').attr('max'));

                    if (parseFloat(maximumTaxExemptAmount) < parseFloat(minimum) || parseFloat(maximumTaxExemptAmount) > parseFloat(maximum)) {
                        result = false
                    }
                }
                else {
                    result = false;
                }
            }
        }

        if (result)
            $('#account-parameter-accordion-error').addClass('d-none');
        else
            $('#account-parameter-accordion-error').removeClass('d-none');

        return result;
    }

    // 6.Demand Deposit Detail Accordion Input Validation
    function IsValidDemandDepositDetailAccordionInputs() {

        let initialAccountOpeningAmount = parseFloat($('#initial-account-opening-amount').val());
        let balanceAmount = parseFloat($('#balance-amount').val());

        // Reference Person Detail
        let enableReferencePersonDetail = $('#enable-reference-person-detail').is(':checked') ? true : false;
        let minimumNumberOfReferencePerson = parseInt($('#minimum-number-of-reference-person').val());
        let maximumNumberOfReferencePerson = parseInt($('#maximum-number-of-reference-person').val());

        // Sweep Out Declaration
        let enableSweepOut = $('#enable-sweep-out').is(':checked') ? true : false;
        let maximumAmountToTriggerSweep = parseFloat($('#maximum-amount-to-trigger-sweep').val());
        let minimumBalanceToTriggerSweepIn = parseFloat($('#minimum-balance-to-trigger-sweep-in').val());
        let minimumTermDepositAmount = parseFloat($('#minimum-term-deposit-amount').val());
        let maximumTermDepositAmount = parseFloat($('#maximum-term-deposit-amount').val());
        let minimumTermDepositTenure = parseInt($('#minimum-term-deposit-tenure').val());
        let maximumTermDepositTenure = parseInt($('#maximum-term-deposit-tenure').val());
        let defaultTermDepositTenure = parseInt($('#default-term-deposit-tenure').val());
        let maximumNumberOfSweepOut = parseInt($('#maximum-number-of-sweep-out').val());

        result = true;
        if ($('#demand-deposit-detail-card').hasClass('d-none') === false)
        {
            //initialAccountOpeningAmount
            if (isNaN(initialAccountOpeningAmount) === false)
            {
                minimum = parseFloat($('#initial-account-opening-amount').attr('min'));
                maximum = parseFloat($('#initial-account-opening-amount').attr('max'));

                if (parseFloat(initialAccountOpeningAmount) < parseFloat(minimum) || parseFloat(initialAccountOpeningAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //balanceAmount
            if (isNaN(balanceAmount) === false)
            {
                minimum = parseFloat($('#balance-amount').attr('min'));
                maximum = parseFloat($('#balance-amount').attr('max'));

                if (parseFloat(balanceAmount) < parseFloat(minimum) || parseFloat(balanceAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //balance type dropdown
            if ($('#balace-type-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //time period unit dropdown
            if ($('#time-Period-Unit-id').prop('selectedIndex') < 1) {
                result = false;
            }

        }

        if (enableReferencePersonDetail === true)
        {
            // Minimum Number Of Reference Person 
            if (isNaN(minimumNumberOfReferencePerson) === false)
            {
                minimum = parseInt($('#minimum-number-of-reference-person').attr('min'));
                maximum = parseInt($('#minimum-number-of-reference-person').attr('max'));

                if (parseInt(minimumNumberOfReferencePerson) < parseInt(minimum) || parseInt(minimumNumberOfReferencePerson) > parseInt(maximum))
                {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // maximumNumberOfReferencePerson
            if (isNaN(maximumNumberOfReferencePerson) === false)
            {
                minimum = parseInt($('#maximum-number-of-reference-person').attr('min'));
                maximum = parseInt($('#maximum-number-of-reference-person').attr('max'));

                if (parseInt(maximumNumberOfReferencePerson) < parseInt(minimum) || parseInt(maximumNumberOfReferencePerson) > parseInt(maximum))
                {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        if (enableSweepOut === true)
        {
            // Maximum Amount To Trigger Sweep 
            if (isNaN(maximumAmountToTriggerSweep) === false) {
                minimum = parseFloat($('#maximum-amount-to-trigger-sweep').attr('min'));
                maximum = parseFloat($('#maximum-amount-to-trigger-sweep').attr('max'));

                if (parseFloat(maximumAmountToTriggerSweep) < parseFloat(minimum) || parseFloat(maximumAmountToTriggerSweep) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Minimum Balance To Trigger Sweep In 
            if (isNaN(minimumBalanceToTriggerSweepIn) === false) {
                minimum = parseFloat($('#minimum-balance-to-trigger-sweep-in').attr('min'));
                maximum = parseFloat($('#minimum-balance-to-trigger-sweep-in').attr('max'));

                if (parseFloat(minimumBalanceToTriggerSweepIn) < parseFloat(minimum) || parseFloat(minimumBalanceToTriggerSweepIn) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Minimum Term Deposit Amount 
            if (isNaN(minimumTermDepositAmount) === false) {
                minimum = parseFloat($('#minimum-term-deposit-amount').attr('min'));
                maximum = parseFloat($('#minimum-term-deposit-amount').attr('max'));

                if (parseFloat(minimumTermDepositAmount) < parseFloat(minimum) || parseFloat(minimumTermDepositAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Maximum Term Deposit Amount 
            if (isNaN(maximumTermDepositAmount) === false) {
                minimum = parseFloat($('#maximum-term-deposit-amount').attr('min'));
                maximum = parseFloat($('#maximum-term-deposit-amount').attr('max'));

                if (parseFloat(maximumTermDepositAmount) < parseFloat(minimum) || parseFloat(maximumTermDepositAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Minimum Term Deposit Tenure 
            if (isNaN(minimumTermDepositTenure) === false) {
                minimum = parseInt($('#minimum-term-deposit-tenure').attr('min'));
                maximum = parseInt($('#minimum-term-deposit-tenure').attr('max'));

                if (parseInt(minimumTermDepositTenure) < parseInt(minimum) || parseInt(minimumTermDepositTenure) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Maximum Term Deposit Tenure 
            if (isNaN(maximumTermDepositTenure) === false)
            {
                minimum = parseInt($('#maximum-term-deposit-tenure').attr('min'));
                maximum = parseInt($('#maximum-term-deposit-tenure').attr('max'));

                if (parseInt(maximumTermDepositTenure) < parseInt(minimum) || parseInt(maximumTermDepositTenure) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Default Term Deposit Tenure 
            if (isNaN(defaultTermDepositTenure) === false)
            {
                minimum = parseInt($('#default-term-deposit-tenure').attr('min'));
                maximum = parseInt($('#default-term-deposit-tenure').attr('max'));

                if (parseInt(defaultTermDepositTenure) < parseInt(minimum) || parseInt(defaultTermDepositTenure) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Maximum Number Of Sweep Out 
            if (isNaN(maximumNumberOfSweepOut) === false)
            {
                minimum = parseInt($('#maximum-number-of-sweep-out').attr('min'));
                maximum = parseInt($('#maximum-number-of-sweep-out').attr('max'));

                if (parseInt(maximumNumberOfSweepOut) < parseInt(minimum) || parseInt(maximumNumberOfSweepOut) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Sweep Out Frequency 
            if ($('#sweep-out-frequency-id').prop('selectedIndex') < 1) {
                result = false;
            }
        }

        if (result)
            $('#demand-deposit-accordion-error').addClass('d-none');
        else
            $('#demand-deposit-accordion-error').removeClass('d-none');

        return result;
    }

    // 7.Limit Accordion Input Validation
    function IsValidLimitAccordionInputs() {
        let cashDepositLimit = parseFloat($('#cash-deposit-limit').val());
        let cashWithdrawalLimit = parseFloat($('#cash-withdrawal-limit').val());
        let retailAccountTurnOverLimit = parseFloat($('#retail-account-turn-over-limit').val());
        let corporateAccountTurnOverLimit = parseFloat($('#corporate-account-turn-over-limit').val());
        let retailHoldingAmount = parseFloat($('#retail-holding-portion').val());
        let corporateHoldingAmount = parseFloat($('#corporate-holding-portion').val());
        let turnOverLimit = parseFloat($('#turn-over-limit').val());

        result = true;
        if ($('#limit-parameter-card').hasClass('d-none') === false) {
            //cash Deposit Limit
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
            //cashWithdrawalLimit
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
            //retailAccountTurnOverLimit
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
            //corporateAccountTurnOverLimit
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

            //retailHoldingAmount
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
            //corporateHoldingAmount
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
            //turnOverLimit
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

    // 6. Target Estimation Accordion Input Validation
    function IsValidTargetEstimationAccordionInputs() {
        result = true;

        if ($('#estimate-target-card').hasClass('d-none') === false) {
            let numberOfIncrementAccount = parseFloat($('#number-of-account').val());
            let incrementAccountUnitText = $('.number-of-account-unit:checked').next('label').text();
            let turnOverAmount = parseFloat($('#turn-over-amount').val());
            let turnOverUnitText = $('.turn-over-unit:checked').next('label').text();

            result = true;

            if (isNaN(numberOfIncrementAccount) || incrementAccountUnitText === '')
                result = false;
            else {
                if (incrementAccountUnitText === 'Number') {
                    minimum = parseFloat($('#number-of-account').attr('min'));
                    maximum = parseFloat($('#number-of-account').attr('max'));

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

            if (isNaN(turnOverAmount) || turnOverUnitText === '')
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
            $('#estimate-target-accordion-error').addClass('d-none');
        else
            $('#estimate-target-accordion-error').removeClass('d-none');

        return result;
    }

    // 9. Fixed Deposit Accordion Input Validation
    function IsValidFixedDepositAccordionInputs() {
        let minimumDepositAmount = parseFloat($('#minimum-deposit-amount').val());
        let depositMultipleOfThereAfter = parseFloat($('#deposit-amount-multiple-of').val());
        let maximumDepositAmount = parseFloat($('#maximum-deposit-amount').val());

        result = true;

        if ($('#fixed-deposit-parameter-card').hasClass('d-none') === false) {
            // Minimum Deposit Amount
            if (isNaN(minimumDepositAmount) === false) {
                minimum = parseFloat($('#minimum-deposit-amount').attr('min'));
                maximum = parseFloat($('#minimum-deposit-amount').attr('max'));

                if (parseFloat(minimumDepositAmount) < parseFloat(minimum) || parseFloat(minimumDepositAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Maximum Deposit Amount 
            if (isNaN(maximumDepositAmount) === false) {
                minimum = parseFloat($('#maximum-deposit-amount').attr('min'));
                maximum = parseFloat($('#maximum-deposit-amount').attr('max'));

                if (parseFloat(maximumDepositAmount) < parseFloat(minimum) || parseFloat(maximumDepositAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Deposit Multiple Of ThereAfter 
            if (isNaN(depositMultipleOfThereAfter) === false) {
                minimum = parseFloat($('#deposit-amount-multiple-of').attr('min'));
                maximum = parseFloat($('#deposit-amount-multiple-of').attr('max'));

                if (parseFloat(depositMultipleOfThereAfter) > parseFloat(minimumDepositAmount)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        if (result)
            $('#fixed-deposit-accordion-error').addClass('d-none');
        else
            $('#fixed-deposit-accordion-error').removeClass('d-none');

        return result;
    }

    // 10. Interest Rate Accordion Input Validation
    function IsValidInterestRateAccordionInputs()
    {
        debugger;
        let prematureVoidInterestPeriod = 0;
        let interestCalculationStartingPeriod = 0;
        let minimumOverrideAmountLimit = 0;
        let maximumOverrideAmountLimit = 0;
        let interestPayoutDayOther = 0;
        let interestPayoutDayText;
        let minimumPeriodicInterestPayout = 0;
        let interestPayoutDay = 'NNN';

        //Validation For FIXED_DEPOSIT Type
        let minimumInterestRate = 0;
        let maximumInterestRate = 0;
        let postMatureVoidInterestPeriod = 0;
        let lessInterestRateForPrematurity = parseFloat($('#less-interest-rate-prematurity').val());

        result = true;

        if (depositType !== DEMAND_DEPOSIT) {
            minimumOverrideAmountLimit = parseFloat($('#minimum-override-amount-limit').val());
            maximumOverrideAmountLimit = parseFloat($('#maximum-override-amount-limit').val());
            interestPayoutDay = $('.interest-payout-day:checked').val();
            interestPayoutDayText = $('.interest-payout-day:checked').next('label').text();
            interestPayoutDayOther = parseInt($('#interest-payout-day-other').val());
            minimumPeriodicInterestPayout = parseInt($('#minimum-month-periodic-interest-payout').val());

            if (isNaN(interestPayoutDayOther))
                interestPayoutDayOther = 0;
        }

        if (depositType === FIXED_DEPOSIT) {
            minimumInterestRate = parseFloat($('#minimum-interest-rate').val());
            maximumInterestRate = parseFloat($('#maximum-interest-rate').val());
            lessInterestRateForPrematurity = parseFloat($('#less-interest-rate-prematurity').val());
            postMatureVoidInterestPeriod = parseInt($('#post-mature-void-interest-period').val());

        }

        prematureVoidInterestPeriod = parseInt($('#premature-void-interest-period').val());
        interestCalculationStartingPeriod = parseInt($('#interest-calculation-starting-period').val());

        // Interest Type
        if ($('#interest-type-id').prop('selectedIndex') < 1) {
            result = false;
        }

        // General Ledger
        if ($('#general-ledger-id').prop('selectedIndex') < 1) {
            result = false;
        }

        // Interest Rate Charged
        if ($('#interest-rate-charged-duration-id').prop('selectedIndex') < 1) {
            result = false;
        }

        // Premature Void Interest Period 
        if (isNaN(prematureVoidInterestPeriod) === false) {
            minimum = parseInt($('#premature-void-interest-period').attr('min'));
            maximum = parseInt($('#premature-void-interest-period').attr('max'));

            if (parseInt(prematureVoidInterestPeriod) < parseInt(minimum) || parseInt(prematureVoidInterestPeriod) > parseInt(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Interest Calculation Starting Period
        if (isNaN(interestCalculationStartingPeriod) === false) {
            minimum = parseInt($('#interest-calculation-starting-period').attr('min'));
            maximum = parseInt($('#interest-calculation-starting-period').attr('max'));

            if (parseInt(interestCalculationStartingPeriod) < parseInt(minimum) || parseInt(interestCalculationStartingPeriod) > parseInt(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // For Fixed Deposit Type
        if (depositType === FIXED_DEPOSIT) {
            // Minimum Interest Rate
            if (isNaN(minimumInterestRate) === false) {
                minimum = parseFloat($('#minimum-interest-rate').attr('min'));
                maximum = parseFloat($('#minimum-interest-rate').attr('max'));

                if (parseFloat(minimumInterestRate) < parseFloat(minimum) || parseFloat(minimumInterestRate) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Maximum Interest Rate
            if (isNaN(maximumInterestRate) === false) {
                minimum = parseFloat($('#maximum-interest-rate').attr('min'));
                maximum = parseFloat($('#maximum-interest-rate').attr('max'));

                if (parseFloat(maximumInterestRate) < parseFloat(minimum) || parseFloat(maximumInterestRate) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        // Interest Payout Validations
        if ($('#enable-periodic-interest-payout-option').is(':checked')) {
            if ($('.interest-payout-day:checked').length === 0) {
                result = false;
            }
            // Interest Payout Day
            if (interestPayoutDay === 'CST') {
                // Interest Payout Day Other
                if (isNaN(interestPayoutDayOther) === false) {
                    minimum = parseInt($('#interest-payout-day-other').attr('min'));
                    maximum = parseInt($('#interest-payout-day-other').attr('max'));

                    if (parseInt(interestPayoutDayOther) < parseInt(minimum) || parseInt(interestPayoutDayOther) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Minimum Periodic Interest Payout
            if (isNaN(minimumPeriodicInterestPayout) === false) {
                minimum = parseInt($('#minimum-month-periodic-interest-payout').attr('min'));
                maximum = parseInt($('#minimum-month-periodic-interest-payout').attr('max'));

                if (parseInt(maximumInterestRate) < parseInt(minimum) || parseInt(maximumInterestRate) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        // Periodic Interest Payout Amount Overrid Validation
        if ($('#enable-payout-interest-amount-override').is(':checked')) {
            // Minimum Override Amount Limit
            if (isNaN(minimumOverrideAmountLimit) === false) {
                minimum = parseFloat($('#minimum-override-amount-limit').attr('min'));
                maximum = parseFloat($('#minimum-override-amount-limit').attr('max'));

                if (parseFloat(minimumOverrideAmountLimit) < parseFloat(minimum) || parseFloat(minimumOverrideAmountLimit) > parseFloat(maximum)) {
                    result = false;
                }
            }

            // Maximum Overrde Amount Limit
            if (isNaN(maximumOverrideAmountLimit) === false) {
                minimum = parseFloat($('#maximum-override-amount-limit').attr('min'));
                maximum = parseFloat($('#maximum-override-amount-limit').attr('max'));

                if (parseFloat(maximumOverrideAmountLimit) < parseFloat(minimum) || parseFloat(maximumOverrideAmountLimit) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else
                result = false;
        }

        // Periodic Interest Payout Amount Overrid Validation
        if ($('#enable-premature-interest-calculation').is(':checked')) {
            // Less Interest Rate For Prematurity
            if (isNaN(lessInterestRateForPrematurity) === false) {
                minimum = parseFloat($('#less-interest-rate-prematurity').attr('min'));
                maximum = parseFloat($('#less-interest-rate-prematurity').attr('max'));

                if (parseFloat(lessInterestRateForPrematurity) < parseFloat(minimum) || parseFloat(lessInterestRateForPrematurity) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        // Periodic Interest Payout Amount Overrid Validation
        if ($('#enable-post-mature-interest-calculation').is(':checked')) {
            // Post Mature Void Interest Period
            if (isNaN(postMatureVoidInterestPeriod) === false) {
                minimum = parseInt($('#post-mature-void-interest-period').attr('min'));
                maximum = parseInt($('#post-mature-void-interest-period').attr('max'));

                if (parseInt(postMatureVoidInterestPeriod) < parseInt(minimum) || parseInt(postMatureVoidInterestPeriod) > (maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        // Interest Provision
        if ($('#enable-interest-provision').is(':checked'))
        {
            if ($('#general-ledger-id-interest-provision-parameter').prop('selectedIndex') < 1)
            {
                result = false;
            }

            if ($('#interest-calculation-freaquency-id').prop('selectedIndex') < 1)
            {
                result = false;
            }
        }

        if (result)
            $('#interest-rate-accordion-error').addClass('d-none');
        else
            $('#interest-rate-accordion-error').removeClass('d-none');

        return result;
    }

    //// 11.Interest Provision Accordion Input Validation
    //function IsValidInterestProvisionAccordionInputs() {
    //    result = true;

      

    //    if (result)
    //        $('#interest-provision-accordion-error').addClass('d-none');
    //    else
    //        $('#interest-provision-accordion-error').removeClass('d-none');

    //    return result;
    //}

    // 9. Passbook Detail Accordion Input Validation
    function IsValidPassbookDetailAccordionInputs() {
        result = true;

        if ($('#enable-passbook-detail').is(':checked')) {
            multiSelectCount = 0;

            if ($('#enable-auto-passbook-number').is(':checked')) {
                let startPassbookNumber = parseInt($('#start-passbook-number').val());
                let endPassbookNumber = parseInt($('#end-passbook-number').val());
                let passbookNumberIncrementBy = parseInt($('#passbook-number-increment-by').val());

                multiSelectCount = parseInt($('#passbook-number-mask option:selected').length);

                // Check For Not A Number (Nan)
                //Passbook Mask Number
                if (isNaN(multiSelectCount) === false) {
                    if (parseInt(multiSelectCount) === 0)
                        result = false;
                }
                else
                    result = false;

                //Start Passbook Number
                if (isNaN(startPassbookNumber) === false) {
                    minimum = parseInt($('#start-passbook-number').attr('min'));
                    maximum = parseInt($('#start-passbook-number').attr('max'));

                    if (parseInt(startPassbookNumber) < parseInt(minimum) || parseInt(startPassbookNumber) > parseInt(maximum))
                        result = false;
                }
                else {
                    result = false;
                }
                //End Passbook Number
                if (isNaN(endPassbookNumber) === false) {
                    minimum = parseInt($('#end-passbook-number').attr('min'));
                    maximum = parseInt($('#end-passbook-number').attr('max'));

                    if (parseInt(endPassbookNumber) < (parseInt(startPassbookNumber) + 100) || parseInt(endPassbookNumber) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;

                //Passbook Number Increment By
                if (isNaN(passbookNumberIncrementBy) === false) {
                    minimum = parseInt($('#passbook-number-increment-by').attr('min'));
                    maximum = parseInt($('#passbook-number-increment-by').attr('max'));

                    if (parseInt(passbookNumberIncrementBy) < parseInt(minimum) || parseInt(passbookNumberIncrementBy) > parseInt((parseInt(endPassbookNumber) - parseInt(startPassbookNumber)) / 100))
                        result = false;
                }
                else
                    result = false;

            }
        }

        if (result)
            $('#passbook-detail-accordion-error').addClass('d-none');
        else
            $('#passbook-detail-accordion-error').removeClass('d-none');

        return result;
    }

    // 13. Agent Accordion Input Validation
    function IsValidAgentParameterAccordionInputs() {
        result = true;

        if ($('#enable-agent-parameter').is(':checked')) {
            let agentCommissionPercentage = parseFloat($('#agent-commission-percentage').val());
            let collectionMarginOverSecurity = parseFloat($('#collection-margin-over-security').val());

            // Installment
            let enableCommissionOnOverDuesInstallment = $('#enable-commission-on-over-dues-installment').is(':checked') ? true : false;
            let minimumOverDuesInstallment = parseInt($('#minimum-over-dues-installment-agent-parameter').val());
            let maximumOverDuesInstallment = parseInt($('#maximum-over-dues-installment-agent-parameter').val());
            let defaultOverDuesInstallment = parseInt($('#default-over-dues-installment-agent-parameter').val());

            // Additional Installment
            let enableCommissionOnAdditionalInvestment = $('#enable-commission-on-additional-investment').is(':checked') ? true : false;
            let minimumAdditionalInstallment = parseInt($('#minimum-additional-installment').val());
            let maximumAdditionalInstallment = parseInt($('#maximum-additional-installment').val());

            // Is Required Security
            let isRequiredSecurity = $('#enable-is-required-security').is(':checked') ? true : false;
            let minimumSecurityAmount = parseFloat($('#minimum-security-amount').val());
            let maximumSecurityAmount = parseFloat($('#maximum-security-amount').val());
            let defaultSecurityAmount = parseFloat($('#default-security-amount').val());

            //  Agent Commission General Ledger
            if ($('#agent-commission-general-ledger').prop('selectedIndex') < 1) {
                result = false;
            }

            if (enableCommissionOnAdditionalInvestment === true) {
                // Minimum Additional Installment
                if (isNaN(minimumAdditionalInstallment) === false) {
                    minimum = parseInt($('#minimum-additional-installment').attr('min'));
                    maximum = parseInt($('#minimum-additional-installment').attr('max'));

                    if (parseInt(minimumAdditionalInstallment) < parseInt(minimum) || parseInt(minimumAdditionalInstallment) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                // Maximum Additional Installment
                if (isNaN(maximumAdditionalInstallment) === false) {
                    minimum = parseInt($('#maximum-additional-installment').attr('min'));
                    maximum = parseInt($('#maximum-additional-installment').attr('max'));

                    if (parseInt(maximumAdditionalInstallment) < parseInt(minimum) || parseInt(maximumAdditionalInstallment) > parseInt(maximum))
                        result = false;
                }
                else {
                    result = false;
                }
            }

            // Agent Commission Percentage
            if (isNaN(agentCommissionPercentage) === false) {
                minimum = parseFloat($('#agent-commission-percentage').attr('min'));
                maximum = parseFloat($('#agent-commission-percentage').attr('max'));

                if (parseFloat(agentCommissionPercentage) < parseFloat(minimum) || parseFloat(agentCommissionPercentage) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            if (isRequiredSecurity === true) {
                // Minimum Security Amount
                if (isNaN(minimumSecurityAmount) === false) {
                    minimum = parseFloat($('#minimum-security-amount').attr('min'));
                    maximum = parseFloat($('#minimum-security-amount').attr('max'));

                    if (parseFloat(minimumSecurityAmount) < parseFloat(minimum) || parseFloat(minimumSecurityAmount) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                // Maximum Security Amount
                if (isNaN(maximumSecurityAmount) === false) {
                    minimum = parseFloat($('#maximum-security-amount').attr('min'));
                    maximum = parseFloat($('#maximum-security-amount').attr('max'));

                    if (parseFloat(maximumSecurityAmount) < parseFloat(minimum) || parseFloat(maximumSecurityAmount) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                // Default Security Amount
                if (isNaN(defaultSecurityAmount) === false) {
                    minimum = parseFloat($('#default-security-amount').attr('min'));
                    maximum = parseFloat($('#default-security-amount').attr('max'));

                    if (parseFloat(defaultSecurityAmount) < parseFloat(minimum) || parseFloat(defaultSecurityAmount) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Collection Margin Over Security
            if (isNaN(collectionMarginOverSecurity) === false) {
                minimum = parseFloat($('#collection-margin-over-security').attr('min'));
                maximum = parseFloat($('#collection-margin-over-security').attr('max'));

                if (parseFloat(collectionMarginOverSecurity) < parseFloat(minimum) || parseFloat(collectionMarginOverSecurity) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Collection Settlement
            if ($('.collection-settlement:checked').length === 0) {
                result = false;
            }

            if (enableCommissionOnOverDuesInstallment === true) {
                // Minimum OverDues Installment
                if (isNaN(minimumOverDuesInstallment) === false) {
                    minimum = parseInt($('#minimum-over-dues-installment-agent-parameter').attr('min'));
                    maximum = parseInt($('#minimum-over-dues-installment-agent-parameter').attr('max'));

                    if (parseInt(minimumOverDuesInstallment) < parseInt(minimum) || parseInt(minimumOverDuesInstallment) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                // Maximum OverDues Installment
                if (isNaN(maximumOverDuesInstallment) === false) {
                    minimum = parseInt($('#maximum-over-dues-installment-agent-parameter').attr('min'));
                    maximum = parseInt($('#maximum-over-dues-installment-agent-parameter').attr('max'));

                    if (parseInt(maximumOverDuesInstallment) < parseInt(minimum) || parseInt(maximumOverDuesInstallment) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                // Default OverDues Installment
                if (isNaN(defaultOverDuesInstallment) === false) {
                    minimum = parseInt($('#default-over-dues-installment-agent-parameter').attr('min'));
                    maximum = parseInt($('#default-over-dues-installment-agent-parameter').attr('max'));

                    if (parseInt(defaultOverDuesInstallment) < parseInt(minimum) || parseInt(defaultOverDuesInstallment) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }
        }

        if (result)
            $('#agent-accordion-error').addClass('d-none');
        else
            $('#agent-accordion-error').removeClass('d-none');

        return result;
    }

    // 14. Installment Parameter Accordion Input Validation
    function IsValidInstallmentParameterAccordionInputs() {
        result = true;

        if ($('#installment-parameter-card').hasClass('d-none') === false) {
            let minimumInstallment = parseInt($('#minimum-installment').val());
            let maximumInstallment = parseInt($('#maximum-installment').val());
            let installmentMultipleOf = parseInt($('#installment-multiple-of').val());
            let duesInstallmentForDefault = parseInt($('#dues-installment-for-default').val());
            let numberOfOverdueInstallmentRecoveryFromLinkedAccount = parseInt($('#number-of-overdue-installment-recovery-from-linked-account').val());
            let fixedPenaltyAmount = parseFloat($('#fixed-penalty-amount').val());
            let penaltyAmountPerHunderd = parseFloat($('#penalty-amount-per-hunderd').val());
            let duesInstallmentForInactivityOfAccount = parseInt($('#dues-installment-for-inactivity-of-account').val());
            let revivePeriodForInactivityAccount = parseInt($('#revive-period-for-inactivity-account').val());

            // Minimum Installment
            if (isNaN(minimumInstallment) === false) {
                minimum = parseInt($('#minimum-installment').attr('min'));
                maximum = parseInt($('#minimum-installment').attr('max'));

                if (parseInt(minimumInstallment) < parseInt(minimum) || parseInt(minimumInstallment) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Maximum Installment
            if (isNaN(maximumInstallment) === false) {
                minimum = parseInt($('#maximum-installment').attr('min'));
                maximum = parseInt($('#maximum-installment').attr('max'));

                if (parseInt(maximumInstallment) < parseInt(minimum) || parseInt(maximumInstallment) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //  Installment Multiple Of
            if (isNaN(installmentMultipleOf) === false) {
                minimum = parseInt($('#installment-multiple-of').attr('min'));
                maximum = parseInt($('#installment-multiple-of').attr('max'));

                if (parseInt(installmentMultipleOf) < parseInt(minimum) || parseInt(installmentMultipleOf) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Dues Installment For Default
            if (isNaN(duesInstallmentForDefault) === false) {
                minimum = parseInt($('#dues-installment-for-default').attr('min'));
                maximum = parseInt($('#dues-installment-for-default').attr('max'));

                if (parseInt(duesInstallmentForDefault) < parseInt(minimum) || parseInt(duesInstallmentForDefault) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Number Of Overdue Installment Recovery From Linked Account
            if (isNaN(numberOfOverdueInstallmentRecoveryFromLinkedAccount) === false) {
                minimum = parseInt($('#number-of-overdue-installment-recovery-from-linked-account').attr('min'));
                maximum = parseInt($('#number-of-overdue-installment-recovery-from-linked-account').attr('max'));

                if (parseInt(numberOfOverdueInstallmentRecoveryFromLinkedAccount) < parseInt(minimum) || parseInt(numberOfOverdueInstallmentRecoveryFromLinkedAccount) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Fixed Penalty Amount
            if (isNaN(fixedPenaltyAmount) === false) {
                minimum = parseFloat($('#fixed-penalty-amount').attr('min'));
                maximum = parseFloat($('#fixed-penalty-amount').attr('max'));

                if (parseFloat(fixedPenaltyAmount) < parseFloat(minimum) || parseFloat(fixedPenaltyAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Penalty Amount Per Hunderd
            if (isNaN(penaltyAmountPerHunderd) === false) {
                minimum = parseFloat($('#penalty-amount-per-hunderd').attr('min'));
                maximum = parseFloat($('#penalty-amount-per-hunderd').attr('max'));

                if (parseFloat(penaltyAmountPerHunderd) < parseFloat(minimum) || parseFloat(penaltyAmountPerHunderd) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Dues Installment For In Activity Of Account
            if (isNaN(duesInstallmentForInactivityOfAccount) === false) {

                minimum = parseInt($('#dues-installment-for-inactivity-of-account').attr('min'));
                maximum = parseInt($('#dues-installment-for-inactivity-of-account').attr('max'));

                if (parseInt(duesInstallmentForInactivityOfAccount) < parseInt(minimum) || parseInt(duesInstallmentForInactivityOfAccount) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Revive Period For Inactivity Account
            if (isNaN(revivePeriodForInactivityAccount) === false) {
                minimum = parseInt($('#revive-period-for-inactivity-account').attr('min'));
                maximum = parseInt($('#revive-period-for-inactivity-account').attr('max'));

                if (parseInt(revivePeriodForInactivityAccount) < parseInt(minimum) || parseInt(revivePeriodForInactivityAccount) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        if (result)
            $('#installment-accordion-error').addClass('d-none');
        else
            $('#installment-accordion-error').removeClass('d-none');

        return result;
    }

    // 15. Renewal Parameter Accordion Input Validation
    function IsValidRenewalParameterAccordionInputs() {
        debugger;
        result = true;

        if ($('#enable-renewal-parameter').is(':checked')) {
            let maximumRenewalDurationAfterMaturityInDays = parseFloat($('#maximum-renewal-duration-after-maturity-in-days').val());
            let accountNumberOnRenewal = $('.account-number-on-renewal:checked').next('label').text();
            let certificateNumberOnRenewal = $('.certificate-number-on-renewal:checked').next('label').text();

            //  Maximum Renewal Duration After Maturity In Days
            if (isNaN(maximumRenewalDurationAfterMaturityInDays) === false) {
                minimum = parseFloat($('#maximum-renewal-duration-after-maturity-in-days').attr('min'));
                maximum = parseFloat($('#maximum-renewal-duration-after-maturity-in-days').attr('max'));

                if (parseFloat(maximumRenewalDurationAfterMaturityInDays) < parseFloat(minimum) || parseFloat(maximumRenewalDurationAfterMaturityInDays) > parseFloat(maximum))
                    result = false;
            }
            else {
                result = false;
            }

            // Account Number On Renewal
            if ($('.account-number-on-renewal:checked').length === 0) {
                result = false;
            }

            // Certificate Number On Renewal
            if ($('.certificate-number-on-renewal:checked').length === 0) {
                result = false;
            }

            if ($('#enable-auto-renewal').is(':checked')) {
                let minimumDurationForAutoRenewal = parseInt($('#minimum-duration-for-auto-renewal').val());
                let maximumDurationForAutoRenewal = parseInt($('#maximum-duration-for-auto-renewal').val());
                let defaultDurationForAutoRenewal = parseInt($('#default-duration-for-auto-renewal').val());

                // Minimum Duration For Auto Renewal
                if (isNaN(minimumDurationForAutoRenewal) === false) {
                    minimum = parseInt($('#minimum-duration-for-auto-renewal').attr('min'));
                    maximum = parseInt($('#minimum-duration-for-auto-renewal').attr('max'));

                    if (parseInt(minimumDurationForAutoRenewal) < parseInt(minimum) || parseInt(minimumDurationForAutoRenewal) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                // Maximum Duration For Auto Renewal
                if (isNaN(maximumDurationForAutoRenewal) === false) {
                    minimum = parseInt($('#maximum-duration-for-auto-renewal').attr('min'));
                    maximum = parseInt($('#maximum-duration-for-auto-renewal').attr('max'));

                    if (parseInt(maximumDurationForAutoRenewal) < parseInt(minimum) || parseInt(maximumDurationForAutoRenewal) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                // Default Duration For Auto Renewal
                if (isNaN(defaultDurationForAutoRenewal) === false) {
                    minimum = parseInt($('#default-duration-for-auto-renewal').attr('min'));
                    maximum = parseInt($('#default-duration-for-auto-renewal').attr('max'));

                    if (parseInt(defaultDurationForAutoRenewal) < parseInt(minimum) || parseInt(defaultDurationForAutoRenewal) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }
        }

        if (result)
            $('#account-renewal-accordion-error').addClass('d-none');
        else
            $('#account-renewal-accordion-error').removeClass('d-none');

        return result;
    }

    // 17. Account Closure Accordion Input Validation
    function IsValidAccountClosureAccordionInputs() {
        result = true;

        if ($('#deposit-account-closure-card').hasClass('d-none') === false) {
            if ($('#enable-extend-maturity').is(':checked')) {
                let minimumExtendPeriod = parseInt($('#minimum-extend-period').val());
                let maximumExtendPeriod = parseInt($('#maximum-extend-period').val());

                // Minimum Extend Period
                if (isNaN(minimumExtendPeriod) === false) {
                    minimum = parseInt($('#minimum-extend-period').attr('min'));
                    maximum = parseInt($('#minimum-extend-period').attr('max'));

                    if (parseInt(minimumExtendPeriod) < parseInt(minimum) || parseInt(minimumExtendPeriod) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                // Maximum Extend Period
                if (isNaN(maximumExtendPeriod) === false) {
                    minimum = parseInt($('#maximum-extend-period').attr('min'));
                    maximum = parseInt($('#maximum-extend-period').attr('max'));

                    if (parseInt(maximumExtendPeriod) < parseInt(minimum) || parseInt(maximumExtendPeriod) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }
        }

        if (result)
            $('#account-closure-accordion-error').addClass('d-none');
        else
            $('#account-closure-accordion-error').removeClass('d-none');

        return result;
    }

    // 18. Photo Input Validation
    function IsValidPhotoInputs()
    {
        let isEnabledAnyOneOption = true;
        let minFileSize = $('#maximum-size-photo-document').attr('min');
        let maxFileSize = $('#maximum-size-photo-document').attr('max');

        result = true;

        $('#photo-document-upload-error').addClass('d-none');

        // Validate Photo Document Upload
        if ($('#enable-photo-sign').is(':checked'))
        {
            // Validate Photo Document Upload
            if ($('#photo-document-upload:checked').next('label').text() != '' && ($('#photo-document-upload:checked').val() != DISABLE_VALUE || $('#sign-document-upload:checked').val() != DISABLE_VALUE))
            {
                // Check Whether Any One Storeged Is Enable Or Not
                isEnabledAnyOneOption = $('#photo-document-upload:checked').val() != DISABLE_VALUE ? $('#photo-document-upload-dbts').is(':checked') || $('#photo-document-upload-lsts').is(':checked') : true;

                if (isEnabledAnyOneOption)
                {
                    // If Storage Is DataBase
                    if ($('#photo-document-upload-dbts').is(':checked'))
                    {
                        if (parseInt($('#applicable-file-formats option:selected').length) == 0)
                            result = false;

                        if (parseInt($('#maximum-size-photo-document').val()) < parseInt(minFileSize) || parseInt($('#maximum-size-photo-document').val()) > parseInt(maxFileSize))
                            result = false;
                    }

                    // If Local Storage
                    if ($('#photo-document-upload-lsts').is(':checked'))
                    {
                        let minFileSize = $('#maximum-file-size-storage').attr('min');
                        let maxFileSize = $('#maximum-file-size-storage').attr('max');

                        if (parseInt($('#photo-document-local-storage-path').val().trim().length) < 3)
                            result = false;

                        if (parseInt($('#applicable-file-formats-photo option:selected').length) == 0)
                            result = false;

                        if (parseInt($('#maximum-file-size-storage').val()) < parseInt(minFileSize) || parseInt($('#maximum-file-size-storage').val()) > parseInt(maxFileSize))
                            result = false;
                    }

                    // Remove Sign Required Message If Upload Value Is Disable
                    if ($('#sign-document-upload:checked').val() === DISABLE_VALUE)
                        $('#sign-document-upload-error').addClass('d-none');
                }
                else
                    result = false;
            }
            else
                result = false;
        }

        if (result)
            $('#photo-document-upload-error').addClass('d-none');
        else
            $('#photo-document-upload-error').removeClass('d-none');

        return result;
    }

    // 18. Sign Input Validation
    function IsValidSignInputs()
    {
        let isEnabledAnyOneOption = true;
        let minFileSize = $('#maximum-file-size-sign').attr('min');
        let maxFileSize = $('#maximum-file-size-sign').attr('max');

        result = true;

        $('#sign-document-upload-error').addClass('d-none');

        // Validate Photo Document Upload
        if ($('#enable-photo-sign').is(':checked'))
        {
            // Validate Photo Document Upload
            if ($('#sign-document-upload:checked').next('label').text() != '' && ($('#photo-document-upload:checked').val() != DISABLE_VALUE || $('#sign-document-upload:checked').val() != DISABLE_VALUE))
            {
                // Check Whether Any One Storeged Is Enable Or Not
                isEnabledAnyOneOption = $('#sign-document-upload:checked').val() != DISABLE_VALUE ? $('#sign-document-upload-dbts').is(':checked') || $('#sign-document-upload-lsts').is(':checked') : true;

                if (isEnabledAnyOneOption)
                {
                    // If Storage Is DataBase
                    if ($('#sign-document-upload-dbts').is(':checked'))
                    {
                        if (parseInt($('#applicable-file-formats-sign option:selected').length) == 0)
                            result = false;

                        if (parseInt($('#maximum-file-size-sign').val()) < parseInt(minFileSize) || parseInt($('#maximum-file-size-sign').val()) > parseInt(maxFileSize))
                            result = false;
                    }

                    // If Local Storage
                    if ($('#sign-document-upload-lsts').is(':checked'))
                    {
                        let minFileSize = $('#maximum-file-size-ls').attr('min');
                        let maxFileSize = $('#maximum-file-size-ls').attr('max');

                        if (parseInt($('#sign-document-local-storage-path').val().trim().length) < 3)
                            result = false;

                        if (parseInt($('#applicable-file-formats-sign-ls option:selected').length) == 0)
                            result = false;

                        if (parseInt($('#maximum-file-size-ls').val()) < parseInt(minFileSize) || parseInt($('#maximum-file-size-ls').val()) > parseInt(maxFileSize))
                            result = false;
                    }

                    // Remove Sign Required Message If Upload Value Is Disable
                    if ($('#photo-document-upload:checked').val() === DISABLE_VALUE)
                        $('#photo-document-upload-error').addClass('d-none');

                }
                else
                    result = false;
            }
            else
                result = false;
        }

        if (result)
            $('#sign-document-upload-error').addClass('d-none');
        else
            $('#sign-document-upload-error').removeClass('d-none');

        return result;
    }

    function SetGeneralLedgerUniqueDropdownList()
    {
        // Show All List Items
        $('#scheme-general-ledger-id').html('');
        $('#scheme-general-ledger-id').append(generalLedgerDropdownListItemsByDepositType);

        // Hide Added DropdownList Items
        $('#tbl-general-ledger > tbody > tr').each(function ()
        {
            currentRow = $(this).closest('tr');
            let myColumnValues = (generalLedgerDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null)
                if (myColumnValues[1] != editedGeneralLedgerId)
                    $('#scheme-general-ledger-id').find('option[value="' + myColumnValues[1] + '"]').remove();
        });
    }

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    //@@@@@@@@@@@@@@@@@@@@@@@@@@  Deposit Agent Incentive -DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-agent-incentive-dt').click(function () {
        event.preventDefault();
        $('#incentive').attr('max', 99999);
        SetModalTitle('agent-incentive', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-agent-incentive-dt').click(function () {
        debugger;
        SetModalTitle('agent-incentive', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-agent-incentive-dt').data('rowindex');

            id = $('#agent-incentive-modal').attr('id');
            myModal = $('#' + id).modal();
            $('#maximum-collection-amount').attr('min', columnValues[1]);

            $('#minimum-collection-amount', myModal).val(columnValues[1]);
            $('#maximum-collection-amount', myModal).val(columnValues[2]);

            if ((columnValues[3]) === FLAT_AMOUNT) {
                $('#incentive').attr('max', 99999);
            }
            else {
                $('#incentive').attr('max', 20);
            }

            $('.incentive-unit[value="' + columnValues[3] + '"]').prop('checked', true);

            $('#incentive', myModal).val(columnValues[5]);

            $('.rounding-method[value="' + columnValues[6] + '"]').prop('checked', true);

            $('#agent-incentive-note', myModal).val(columnValues[8]);

            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-agent-incentive-dt').addClass('read-only');
            $('#agent-incentive-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-agent-incentive-modal').click(function (event) {

        if (IsValidDepositAgentIncentiveModal())
        {
            row = agentIncentiveDataTable.row.add([
                        tag,
                        minimumCollectionAmount,
                        maximumCollectionAmount,
                        incentiveUnit,
                        incentiveUnitText,
                        incentive,
                        roundingMethod,
                        roundingMethodText,
                        note,
            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);


            HideAgentIncentiveDataTableColumns()

            agentIncentiveDataTable.columns.adjust().draw();

            // Hide Required Error Message, If Table Has Not Any Record
            $('#agent-incentive-accordian-error').addClass('d-none');

            EnableNewOperation('agent-incentive');

            $('#agent-incentive-modal').modal('hide');
        }
    });

    // Modal Update Button Event
    $('#btn-update-agent-incentive-modal').click(function (event) {
        debugger;
        $('#select-all-agent-incentive').prop('checked', false);

        if (IsValidDepositAgentIncentiveModal()) {
            agentIncentiveDataTable.row(selectedRowIndex).data([
                tag,
                minimumCollectionAmount,
                maximumCollectionAmount,
                incentiveUnit,
                incentiveUnitText,
                incentive,
                roundingMethod,
                roundingMethodText,
                note,
            ]).draw();

            HideAgentIncentiveDataTableColumns();

            agentIncentiveDataTable.columns.adjust().draw();

            $('#agent-incentive-modal').modal('hide');

            EnableNewOperation('agent-incentive');
        }
    })

    // Modal Delete Button Event
    $('#btn-delete-agent-incentive-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-agent-incentive tbody input[type="checkbox"]:checked').each(function () {
                    agentIncentiveDataTable.row($('#tbl-agent-incentive tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                   rowData = $('#btn-delete-agent-incentive-dt').data('rowindex');

                    EnableNewOperation('agent-incentive');

                    $('#select-all-agent-incentive').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!agentIncentiveDataTable.data().any())
                    $('#agent-incentive-accordian-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event
    $('#select-all-agent-incentive').click(function () {
        if ($(this).prop('checked')) {
            // Check Mark All Checkboxes
            $('#tbl-agent-incentive tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = agentIncentiveDataTable.row(row).index();
                rowData = (agentIncentiveDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                // Bind All Rows To Delete Button / Operation
                $('#btn-delete-agent-incentive-dt').data('rowindex', arr);

                EnableDeleteOperation('agent-incentive')
            });
        }
        else {
            EnableNewOperation('agent-incentive');

            // Unmark All Checkboxes
            $('#tbl-agent-incentive tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-agent-incentive tbody').click('input[type="checkbox"]', function ()
    {
        debugger;
        // Get Each Row Of Table
        $('#tbl-agent-incentive input[type="checkbox"]:checked').each(function (index)
        {
            isChecked = $(this).prop('checked');

            if (isChecked)
            {
                row = $(this).closest('tr');

                selectedRowIndex = agentIncentiveDataTable.row(row).index();

                rowData = (agentIncentiveDataTable.row(selectedRowIndex).data());

                // ******* Clear Purpose Of td0 And Rename It, If Necessary
                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('agent-incentive');

                $('#btn-update-agent-incentive-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-agent-incentive-dt').data('rowindex', rowData);
                $('#btn-delete-agent-incentive-dt').data('rowindex', arr);
                $('#select-all-agent-incentive').data('rowindex', arr);

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-agent-incentive tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('agent-incentive');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('agent-incentive');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('agent-incentive');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-agent-incentive').prop('checked', true);
        else
            $('#select-all-agent-incentive').prop('checked', false);
    });

    //To page load table each row get value & dropdown value Hide 
    $('#tbl-agent-incentive > tbody > tr').each(function () {
        debugger;

        currentRow = $(this).closest('tr');
        columnValues = (agentIncentiveDataTable.row(currentRow).data());
        if (typeof columnValues != 'undefined' && columnValues != null) {

            $('#minimum-collection-amount').find("option[value='" + columnValues[0] + "']").hide();
        }
        else {
            return true;
        }

    });

    // Validate Agent Incentive Module
    function IsValidDepositAgentIncentiveModal() {
        result = true;

        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        minimumCollectionAmount = parseFloat($('#minimum-collection-amount').val());
        maximumCollectionAmount = parseFloat($('#maximum-collection-amount').val());
        incentiveUnit = $('.incentive-unit:checked').val();
        incentiveUnitText = $('.incentive-unit:checked').next('label').text();
        incentive = parseFloat($('#incentive').val());
        roundingMethod = $('.rounding-method:checked').val();
        roundingMethodText = $('.rounding-method:checked').next('label').text();
        note = $('#agent-incentive-note').val().trim();

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        //rounding Method
        if ($('.rounding-method:checked').length === 0) {
            result = false;
            $('#rounding-method-error').removeClass('d-none');
        }

        // Validate CollectionAmount
        if (isNaN(minimumCollectionAmount) === false) {
            minimum = parseFloat($('#minimum-collection-amount').attr('min'));
            maximum = parseFloat($('#minimum-collection-amount').attr('max'));

            if (parseFloat(minimumCollectionAmount) < parseFloat(minimum) || parseFloat(minimumCollectionAmount) > parseFloat(maximum)) {
                result = false;
                $('#minimum-collection-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#minimum-collection-amount-error').removeClass('d-none');
        }

        // Validate maximumCollectionAmount
        if (isNaN(maximumCollectionAmount) === false) {
            minimum = parseFloat($('#maximum-collection-amount').attr('min'));
            maximum = parseFloat($('#maximum-collection-amount').attr('max'));

            if (parseFloat(maximumCollectionAmount) < parseFloat(minimum) || parseFloat(maximumCollectionAmount) > parseFloat(maximum)) {
                result = false;
                $('#maximum-collection-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-collection-amount-error').removeClass('d-none');
        }

        // Clear error message before validation
        $('#incentive-error').addClass('d-none');

        // Validate SubscriptionAmountUnitText
        if ($('.incentive-unit:checked').length === 0) {
            result = false;
            $('#incentive-unit-error').removeClass('d-none');
            $('#incentive-error').removeClass('d-none');
        }
        else
        {
            $('#incentive-unit-error').addClass('d-none');

            if (incentiveUnit === 'F')
            {
                if(isNaN(incentive) ===false)
                {
                    minimum = parseFloat($('#incentive').attr('min'));
                    maximum = parseFloat($('#incentive').attr('max'));
                    if (parseFloat(incentive) < parseFloat(minimum) || parseFloat(incentive) > parseFloat(maximum)) {
                        result = false;
                        $('#incentive-error').removeClass('d-none');
                    }
                } 
                else {
                    result = false;
                    $('#incentive-error').removeClass('d-none');
                }
            } 
            else if (incentiveUnit === 'P')
            {
                if (isNaN(incentive) === false)
                {
                    minimum = parseFloat($('#incentive').attr('min'));
                    maximum = parseFloat($('#incentive').attr('max'));
                    if (parseFloat(incentive) < parseFloat(minimum) || parseFloat(incentive) > parseFloat(maximum)) {
                        result = false;
                        $('#incentive-error').removeClass('d-none');
                    }
                }
                else
                {
                    result = false;
                    $('#incentive-error').removeClass('d-none');
                }
            }
            else
            {
                result = false;
                $('#incentive-unit-error').removeClass('d-none');
                $('#incentive-error').removeClass('d-none');
            }
        }


        return result;
    }

    // Hide Unnecessary Columns
    function HideAgentIncentiveDataTableColumns() {
        agentIncentiveDataTable.column(3).visible(false);
        agentIncentiveDataTable.column(6).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@ Number Of Transaction Limit - DataTable Code @@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-number-of-transaction-limit-dt').click(function () {

        event.preventDefault();

        SetModalTitle('number-of-transaction-limit', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-number-of-transaction-limit-dt').click(function () {

        SetModalTitle('number-of-transaction-limit', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-number-of-transaction-limit-dt').data('rowindex');
            id = $('#number-of-transaction-limit-modal').attr('id');
            myModal = $('#' + id).modal();
            $('#maximum-number-of-transaction').attr('min', columnValues[3]);

            $('#transaction-type-id', myModal).val(columnValues[1]);
            $('#minimum-number-of-transaction', myModal).val(columnValues[3]);
            $('#maximum-number-of-transaction', myModal).val(columnValues[4]);
            $('#time-period-id-unit', myModal).val(columnValues[5]);
            $('#note-number-of-transaction-limit', myModal).val(columnValues[7]);
            myModal.modal({ show: true });
        }

        else {
            $('#btn-edit-number-of-transaction-limit-dt').addClass('read-only');
            $('#number-of-transaction-limit-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-number-of-transaction-limit-modal').click(function (event) {

        if (IsValidNumberOfTransactionLimitModal())
        {

            row = numberOfTransactionLimitDataTable.row.add([
               tag,
               transactionType,
               transactionTypeText,
               minimumNumberOfTransaction,
               maximumNumberOfTransaction,
               timePeriodUnit,
               timePeriodUnitText,
               note,
            ]).draw();


            HideColumnsNumberOfTransactionLimit();

            numberOfTransactionLimitDataTable.columns.adjust().draw();

            // Add Required Error Message, If Table Has Not Any Record
            $('#number-of-transaction-limit-accordian-error').addClass('d-none');

            EnableNewOperation('number-of-transaction-limit');

            $('#number-of-transaction-limit-modal').modal('hide');
        }
    });

    // Modal update Button Event
    $('#btn-update-number-of-transaction-limit-modal').click(function (event) {

        $('#select-all-number-of-transaction-limit').prop('checked', false);

        if (IsValidNumberOfTransactionLimitModal()) {
            numberOfTransactionLimitDataTable.row(selectedRowIndex).data([
                 tag,
                 transactionType,
                 transactionTypeText,
                 minimumNumberOfTransaction,
                 maximumNumberOfTransaction,
                 timePeriodUnit,
                 timePeriodUnitText,
                 note,
            ]).draw();

            HideColumnsNumberOfTransactionLimit();

            numberOfTransactionLimitDataTable.columns.adjust().draw();

            $('#number-of-transaction-limit-modal').modal('hide');

            EnableNewOperation('number-of-transaction-limit');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-number-of-transaction-limit-dt').click(function (event) {

        let isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-number-of-transaction-limit tbody input[type="checkbox"]:checked').each(function () {
                    numberOfTransactionLimitDataTable.row($('#tbl-number-of-transaction-limit tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                     rowData = $('#btn-delete-number-of-transaction-limit-dt').data('rowindex');
                      EnableNewOperation('number-of-transaction-limit');

                  $('#select-all-number-of-transaction-limit').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!numberOfTransactionLimitDataTable.data().any())
                    $('#number-of-transaction-limit-accordian-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event
    $('#select-all-number-of-transaction-limit').click(function () {
        if ($(this).prop('checked')) {

            $('#tbl-number-of-transaction-limit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);
                row = $(this).closest('tr');
                selectedRowIndex = numberOfTransactionLimitDataTable.row(row).index();
                rowData = (numberOfTransactionLimitDataTable.row(selectedRowIndex).data());
                arr.push({ arrayCloumn1: rowData[1] });
                $('#btn-delete-number-of-transaction-limit-dt').data('rowindex', arr);
                EnableDeleteOperation('number-of-transaction-limit');
            });
        }
        else {
            EnableNewOperation('number-of-transaction-limit')
            $('#tbl-number-of-transaction-limit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    //Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-number-of-transaction-limit tbody').click('input[type="checkbox"]', function ()
    {
        $('#tbl-number-of-transaction-limit input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked)
            {
                row = $(this).closest('tr');
                selectedRowIndex = numberOfTransactionLimitDataTable.row(row).index();
                rowData = (numberOfTransactionLimitDataTable.row(selectedRowIndex).data());
                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('number-of-transaction-limit');

                $('#btn-update-number-of-transaction-limit-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-number-of-transaction-limit-dt').data('rowindex', rowData);
                $('#btn-delete-number-of-transaction-limit-dt').data('rowindex', arr);
                $('#select-all-number-of-transaction-limit').data('rowindex', arr);
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-number-of-transaction-limit tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('number-of-transaction-limit');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('number-of-transaction-limit');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('number-of-transaction-limit');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-number-of-transaction-limit').prop('checked', true);
        else
            $('#select-all-number-of-transaction-limit').prop('checked', false);
    });

    //To page load table each row get value & dropdown value Hide 
    $('#tbl-number-of-transaction-limit > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (numberOfTransactionLimitDataTable.row(currentRow).data());
        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#transaction-type-id').find("option[value='" + columnValues[0] + "']").hide();
        else
            return true;
    });

    // Validate Number Of Transaction Limit Module
    function IsValidNumberOfTransactionLimitModal() {
        result = true;

        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        transactionType = $('#transaction-type-id option:selected').val();
        transactionTypeText = $('#transaction-type-id option:selected').text();
        minimumNumberOfTransaction = parseInt($('#minimum-number-of-transaction').val());
        maximumNumberOfTransaction = parseInt($('#maximum-number-of-transaction').val());
        timePeriodUnit = $('#time-period-id-unit option:selected').val();
        timePeriodUnitText = $('#time-period-id-unit option:selected').text();
        note = $('#note-number-of-transaction-limit').val().trim();

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        //transaction Type
        if ($('#transaction-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#transaction-type-id-error').removeClass('d-none');
        }
        else {
            $('#transaction-type-id-error').addClass('d-none');
        }

        // Validate minimumNumberOfTransaction
        if (isNaN(minimumNumberOfTransaction) === false) {
            minimum = parseInt($('#minimum-number-of-transaction').attr('min'));
            maximum = parseInt($('#minimum-number-of-transaction').attr('max'));

            if (parseInt(minimumNumberOfTransaction) < parseInt(minimum) || parseInt(minimumNumberOfTransaction) > parseInt(maximum)) {
                result = false;
                $('#minimum-number-of-transaction-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#minimum-number-of-transaction-error').removeClass('d-none');
        }
        // Validate maximumNumberOfTransaction
        if (isNaN(maximumNumberOfTransaction) === false) {
            minimum = parseInt($('#maximum-number-of-transaction').attr('min'));
            maximum = parseInt($('#maximum-number-of-transaction').attr('max'));

            if (parseInt(maximumNumberOfTransaction) < parseInt(minimum) || parseInt(maximumNumberOfTransaction) > parseInt(maximum)) {
                result = false;
                $('#maximum-number-of-transaction-error').removeClass('d-none');
            }
        }
        else
        {
            result = false;
            $('#maximum-number-of-transaction-error').removeClass('d-none');
        }


        if ($('#time-period-id-unit').prop('selectedIndex')<1) {
            result = false;
            $('#time-period-id-unit-error').removeClass('d-none');
        }
        else {
            $('#time-period-id-unit-error').addClass('d-none');
        }
        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsNumberOfTransactionLimit() {
        numberOfTransactionLimitDataTable.column(1).visible(false);
        numberOfTransactionLimitDataTable.column(5).visible(false);
    }

    //@@@@@@@@@@@@@@@@@@@@@@@@@@ Transaction Amount Limit -DataTable Code @@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-transaction-amount-limit-dt').click(function () {

        event.preventDefault();

        SetModalTitle('transaction-amount-limit', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-transaction-amount-limit-dt').click(function () {

        SetModalTitle('transaction-amount-limit', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-transaction-amount-limit-dt').data('rowindex');
            id = $('#transaction-amount-limit-modal').attr('id');
            myModal = $('#' + id).modal();

            $('#maximum-amount-limit').attr('min', columnValues[3]);

            $('#transaction-type-amount-id', myModal).val(columnValues[1]);
            $('#maximum-amount-limit', myModal).val(columnValues[4]);
            $('#note-transaction-amount-limit', myModal).val(columnValues[5]);
            $('#minimum-amount-limit', myModal).val(columnValues[3]);


            myModal.modal({ show: true });
        }

        else {
            $('#btn-edit-transaction-amount-limit-dt').addClass('read-only');
            $('#transaction-amount-limit-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-transaction-amount-limit-modal').click(function (event) {
        if (IsValidTransactionAmountLimitModal()) {

            row = transactionAmountLimitDataTable.row.add([
              tag,
              transactionType,
              transactionTypeText,
              minimumAmountLimit,
              maximumAmountLimit,
              note,
            ]).draw();



            HideColumnsTransactionAmountLimit();

            transactionAmountLimitDataTable.columns.adjust().draw();

            // Hide Table Required Data Error Message
            $('#transaction-amount-limit-accordian-error').addClass('d-none');

            $('#transaction-amount-limit-modal').modal('hide');

            EnableNewOperation('transaction-amount-limit');
        }
    });

    // Modal update Button Event
    $('#btn-update-transaction-amount-limit-modal').click(function (event) {

        $('#select-all-transaction-amount-limit').prop('checked', false);

        if (IsValidTransactionAmountLimitModal()) {
            transactionAmountLimitDataTable.row(selectedRowIndex).data([
               tag,
               transactionType,
               transactionTypeText,
               minimumAmountLimit,
               maximumAmountLimit,
               note,
            ]).draw();

            HideColumnsTransactionAmountLimit();

            transactionAmountLimitDataTable.columns.adjust().draw();

            $('#transaction-amount-limit-modal').modal('hide');
            EnableNewOperation('transaction-amount-limit');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-transaction-amount-limit-dt').click(function (event) {

        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-transaction-amount-limit tbody input[type="checkbox"]:checked').each(function () {
                    transactionAmountLimitDataTable.row($('input[type="checkbox"]:checked').parents('tr')).remove().draw();

                   rowData = $('#btn-delete-transaction-amount-limit-dt').data('rowindex');
                  EnableNewOperation('transaction-amount-limit');

                  $('#select-all-transaction-amount-limit').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!transactionAmountLimitDataTable.data().any())
                    $('#transaction-amount-limit-accordian-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event
    $('#select-all-transaction-amount-limit').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-transaction-amount-limit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);
                row = $(this).closest('tr');
                selectedRowIndex = transactionAmountLimitDataTable.row(row).index();
                rowData = (transactionAmountLimitDataTable.row(selectedRowIndex).data());
                arr.push({ arrayCloumn1: rowData[1] });
                $('#btn-delete-transaction-amount-limit-dt').data('rowindex', arr);
                EnableDeleteOperation('transaction-amount-limit');
            });
        }
        else {
            EnableNewOperation('transaction-amount-limit');
            $('#tbl-transaction-amount-limit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    //Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-transaction-amount-limit tbody').click('input[type="checkbox"]', function () {
        $('#tbl-transaction-amount-limit input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = transactionAmountLimitDataTable.row(row).index();
                rowData = (transactionAmountLimitDataTable.row(selectedRowIndex).data());
                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('transaction-amount-limit');

                $('#btn-update-transaction-amount-limit-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-transaction-amount-limit-dt').data('rowindex', rowData);
                $('#btn-delete-transaction-amount-limit-dt').data('rowindex', arr);
                $('#select-all-transaction-amount-limit').data('rowindex', arr);
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-transaction-amount-limit tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('transaction-amount-limit');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('transaction-amount-limit');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('transaction-amount-limit');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-transaction-amount-limit').prop('checked', true);
        else
            $('#select-all-transaction-amount-limit').prop('checked', false);
    });

    //To page load table each row get value & dropdown value Hide 
    $('#tbl-transaction-amount-limit > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (transactionAmountLimitDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#transaction-type-amount-id').find("option[value='" + columnValues[0] + "']").hide();
        else
            return true;
    });

    // Validate Transaction Amount Limit Module
    function IsValidTransactionAmountLimitModal() {

        result = true;

        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        transactionType = $('#transaction-type-amount-id option:selected').val();
        transactionTypeText = $('#transaction-type-amount-id option:selected').text();
        minimumAmountLimit = parseFloat($('#minimum-amount-limit').val());
        maximumAmountLimit = parseFloat($('#maximum-amount-limit').val());
        note = $('#note-transaction-amount-limit').val().trim();

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        if ($('#transaction-type-amount-id').prop('selectedIndex') < 1) {
            result = false;
            $('#transaction-type-amount-id-error').removeClass('d-none');
        } 

        // Validate Limit
        if (isNaN(minimumAmountLimit) === false) {
            minimum = parseFloat($('#minimum-amount-limit').attr('min'));
            maximum = parseFloat($('#minimum-amount-limit').attr('max'));

            if (parseFloat(minimumAmountLimit) < parseFloat(minimum) || parseFloat(minimumAmountLimit) > parseFloat(maximum)) {
                result = false;
                $('#minimum-amount-limit-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#minimum-amount-limit-error').removeClass('d-none');
        }
        // Validate maximumAmountLimit
        if (isNaN(maximumAmountLimit) === false) {
            minimum = parseFloat($('#maximum-amount-limit').attr('min'));
            maximum = parseFloat($('#maximum-amount-limit').attr('max'));

            if (parseFloat(maximumAmountLimit) < parseFloat(minimum) || parseFloat(maximumAmountLimit) > parseFloat(maximum)) {
                result = false;
                $('#maximum-amount-limit-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-amount-limit-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsTransactionAmountLimit() {
        transactionAmountLimitDataTable.column(1).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme Target Group  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-target-group-dt').click(function () {
        event.preventDefault();
        SetModalTitle('target-group', 'Add');
        $('.gender').addClass('d-none');
        $('.occupation').addClass('d-none');
    });

    // DataTable Edit Button 
    $('#btn-edit-target-group-dt').click(function () {
        SetModalTitle('target-group', 'Edit');
        $('.gender').addClass('d-none');
        $('.occupation').addClass('d-none');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-target-group-dt').data('rowindex');
            id = $('#target-group-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs
            $('#target-group-id', myModal).val(columnValues[1]);

            $('.gender').addClass('d-none');
            $('.occupation').addClass('d-none');

            // Gender
            if (columnValues[2].indexOf('Gender') !== -1) {
                $('.gender').removeClass('d-none');
                $('#gender-id', myModal).val(columnValues[3]);
            }

            // Occupation
            if (columnValues[2].indexOf('Occupation') !== -1) {
                $('.occupation').removeClass('d-none');
                $('#occupation-id', myModal).val(columnValues[3]);
            }

            // If neither Gender nor Occupation is selected, set their values to "None"
            if (columnValues[2].indexOf('Gender') !== 0 && columnValues[2].indexOf('Occupation') !== 0) {
                $('#gender-id', myModal).val('None');
                $('#occupation-id', myModal).val('None');
            }

            requiredMember = columnValues[5].split('--->');

            //// Display Value In Modal Inputs
            //$('input[name="SchemeTargetGroupViewModel.RequiredMembership"][value=' + columnValues[5] + ']').prop('checked', true);

            $('.required-membership[value="' + columnValues[5] + '"]').prop('checked', true);

            $('#note-target-group', myModal).val(columnValues[7]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-target-group-dt').addClass('read-only');
            $('#target-group-modal').modal('hide');
        }

        // Hide Selected Dropdown Id Column
        arr.map(function (obj) {
            $('#gender-id').find("option[value='" + obj.arrayCloumn1 + "']").hide();
            $('#occupation-id').find("option[value='" + obj.arrayCloumn1 + "']").hide();
        });
    });

    // Modal Add Button Event
    $('#btn-add-target-group-modal').click(function (event) {
        debugger;
        if (IsValidTargetGroupDataTableAddModal()) {
            row = targetGroupDataTable.row.add([
                tag,
                targetGroupId,
                targetGroupIdText,
                valueId,
                valueText,
                requiredMember,
                requiredMemberText,
                note,
            ]).draw();

            // Error Message In Span
            $('#target-group-data-table-error').addClass('d-none');

            HideTargetGroupDataTableColumns();

            targetGroupDataTable.columns.adjust().draw();

            $('#target-group-modal').modal('hide');

            EnableNewOperation('target-group');
        }

    });

    // Modal update Button Event
    $('#btn-update-target-group-modal').click(function (event) {
        debugger;
        $('#select-all-target-group').prop('checked', false);
        if (IsValidTargetGroupDataTableAddModal()) {
            targetGroupDataTable.row(selectedRowIndex).data([
                tag,
                targetGroupId,
                targetGroupIdText,
                valueId,
                valueText,
                requiredMember,
                requiredMemberText,
                note,
            ]).draw();

            HideTargetGroupDataTableColumns();

            targetGroupDataTable.columns.adjust().draw();

            $('#target-group-modal').modal('hide');

            EnableNewOperation('target-group');
            $('.gender').addClass('d-none');
            $('.occupation').addClass('d-none');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-target-group-dt').click(function (event) {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-target-group tbody input[type="checkbox"]:checked').each(function () {
                    targetGroupDataTable.row($('#tbl-target-group tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-target-group-dt').data('rowindex');
                    EnableNewOperation('target-group');

                    // Hide Selected Dropdown Id Column
                    arr.map(function (obj) {
                        $('#gender-id').find("option[value='" + obj.arrayCloumn1 + "']").show();
                        $('#occupation-id').find("option[value='" + obj.arrayCloumn1 + "']").show();

                        $('#gender-id').prop('selectedRowIndex', 0);
                        $('#occupation-id').prop('selectedRowIndex', 0);
                });

                    $('#select-all-target-group').prop('checked', false);
                    if (!targetGroupDataTable.data().any())
                        $('#target-group-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A CheckBox');
    });

    // Select All Check Box Click Event  
    $('#select-all-target-group').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-target-group tbody input[type="checkbox"]').each(function (index) {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = targetGroupDataTable.row(row).index();

                rowData = (targetGroupDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-target-group-dt').data('rowindex', arr);
                EnableDeleteOperation('target-group');
            });
        }
        else {
            EnableNewOperation('target-group');

            $('#tbl-target-group tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-target-group tbody').click('input[type="checkbox"]', function () {
        $('#tbl-target-group input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = targetGroupDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (targetGroupDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('target-group');
                    $('#btn-update-target-group-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-target-group-dt').data('rowindex', rowData);
                    $('#btn-delete-target-group-dt').data('rowindex', arr);
                    $('#select-all-target-group').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-target-group tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('target-group');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('target-group');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('target-group');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-target-group').prop('checked', true);
        else
            $('#select-all-target-group').prop('checked', false);
    });

    // Validate Target Group Module
    function IsValidTargetGroupDataTableAddModal() {
        debugger;
        let result = true;
        // Get Modal Inputs In Local Variable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        targetGroupId = $('#target-group-id option:selected').val();
        targetGroupIdText = $('#target-group-id option:selected').text();
        valueId = $('#value-id option:selected').val();
        valueText = $('#value-id option:selected').text();
        requiredMember = $('.required-membership:checked').val();
        requiredMemberText = $('.required-membership:checked').next('label').text();
        note = $('#note-target-group').val();

        if (note === '')
            note = 'None';

        // Check if either gender or occupation is selected
        if (targetGroupIdText.indexOf('Occupation') > -1) {
            // If gender is not selected, assign occupation values
            valueText = $('#occupation-id option:selected').text();
            valueId = ($('#occupation-id option:selected').val());
        }
        else {
            // If gender is selected, assign gender values
            valueText = $('#gender-id option:selected').text();
            valueId = ($('#gender-id option:selected').val());
        }

        // If neither Gender nor Occupation is selected, set their values to "None"
        if (targetGroupIdText.indexOf('Gender') !== 0 && targetGroupIdText.indexOf('Occupation') !== 0) {
            valueText = 'None';
            valueId = 'None';
        }

        // Target Group Id
        if ($('#target-group-id').prop('selectedIndex') < 1) {
            result = false;
            $('#target-group-id-error').removeClass('d-none');
        }

        if (valueId === '' || typeof valueId === 'undefined') {
            if (targetGroupIdText !== 'None') {
                result = false;
            }

            if (targetGroupIdText === 'Gender') {
                $('#gender-id-error').addClass('d-none');
            }
            else {
                result = false;
                $('#gender-id-error').removeClass('d-none');
            }

            if (targetGroupIdText === 'Occupation') {
                $('#occupation-id-error').addClass('d-none');
            }
            else {
                result = false;
                $('#occupation-id-error').removeClass('d-none');
            }

            if (targetGroupIdText === 'None') {
                valueText = 'None';
            }
        }

        if ($('.required-membership:checked').length === 0) {
            result = false;
            $('#required-membership-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideTargetGroupDataTableColumns() {
        targetGroupDataTable.column(1).visible(false);
        targetGroupDataTable.column(3).visible(false);
        targetGroupDataTable.column(5).visible(false);
    }

    //@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme ReportFormat - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-report-format-dt').click(function ()
    {
        event.preventDefault();
        editedReportFormatId = '';
        SetReportFormatUniqueDropdownList();
        SetModalTitle('report-format', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-report-format-dt').click(function ()
    {
        SetModalTitle('report-format', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
            columnValues = $('#btn-edit-report-format-dt').data('rowindex');

            editedReportFormatId = columnValues[1];

            SetReportFormatUniqueDropdownList();

            id = $('#report-format-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs
            $('#report-format-id', myModal).val(columnValues[1]);
            $('#note-Report', myModal).val(columnValues[3]);

            // Show Modals
            myModal.modal({ show: true });
        }

        else
        {
            $('#btn-edit-report-format-dt').addClass('read-only');
            $('#report-format-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-report-format-modal').click(function (event)
    {
        if (IsValidReportFormatDataTableModal()) {
            row = reportTypeFormatDataTable.row.add([
                           tag,
                           reportFormatId,
                           reportFormatText,
                           note,
            ]).draw();

            HideReportFormatDataTableColumns();

            reportTypeFormatDataTable.columns.adjust().draw();

            ClearModal('report-format');

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

            $('#report-format-modal').modal('hide');

            EnableNewOperation('report-format');

        }
    });

    // Modal Delete Button Event
    $('#btn-delete-report-format-dt').click(function (event)
    {
        isChecked = $('#tbl-report-format tbody input[type="checkbox"]').is(':checked');

        if (isChecked)
        {
            if (confirm('Are You Sure To Delete This Row?'))
            {
                if ($('#tbl-report-format tbody input[type="checkbox"]:checked').each(function ()
                {
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
    $('#tbl-report-format tbody').click('input[type="checkbox"]', function () {
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
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        reportFormatId = $('#report-format-id option:selected').val();
        reportFormatText = $('#report-format-id option:selected').text();
        note = $('#note-Report').val().trim();
        let result = true;
        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        if ($('#report-format-id').prop('selectedIndex') < 1) {
            $('#report-format-id-error').removeClass('d-none');
                result = false;
        }
        return result;
    }

    // Hide Unnecessary Columns
    function HideReportFormatDataTableColumns() {
        reportTypeFormatDataTable.column(1).visible(false);
    }

    //@@@@@@@@@@@@@@@@@@@@@@@@@@ Notice Schedule - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@

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
            $('#btn-notice-schedule-edit-dt').addClass('read-only');
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

            HideColumnsNoticeScheduleDataTable();

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


            HideColumnsNoticeScheduleDataTable();

            noticeScheduleDataTable.columns.adjust().draw();

            $('#notice-schedule-modal').modal('hide');
            EnableNewOperation('notice-schedule');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-notice-schedule-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
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
    $('#tbl-notice-schedule tbody').click('input[type="checkbox"]', function () {
        $('#tbl-notice-schedule input[type="checkbox"]:checked').each(function () {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');

                selectedRowIndex = noticeScheduleDataTable.row(row).index();

                rowData = (noticeScheduleDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('notice-schedule');

                $('#btn-update-notice-schedule-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-notice-schedule-dt').data('rowindex', rowData);
                $('#btn-delete-notice-schedule-dt').data('rowindex', arr);
                $('#select-all-notice-schedule').data('rowindex', arr);
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
    function HideColumnsNoticeScheduleDataTable() {
        noticeScheduleDataTable.column(1).visible(false);
        noticeScheduleDataTable.column(3).visible(false);
        noticeScheduleDataTable.column(5).visible(false);
    }

    //@@@@@@@@@@@@@@@@@@@@@@@@@@ Closing Charges Detail  - DataTables @@@@@@@@@@@@@@@@@@@@@@@@@@

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
            $('#maximum-charges-closing-amount').attr('min', columnValues[6]);

            $('#minimum-charges-closing-amount', myModal).val(columnValues[6]);
            $('#minimum-time-period-in-days', myModal).val(columnValues[3]);
            $('#closing-charges-general-ledger-id', myModal).val(columnValues[1]);
            $('#maximum-time-period-in-days', myModal).val(columnValues[4]);
            $('#is-time-period-for-before-closure', myModal).val(columnValues[5]);
            $('#maximum-charges-closing-amount', myModal).val(columnValues[7]);
            $('#is-taxable', myModal).val(columnValues[8]);
            $('#is-applicable-on-death', myModal).val(columnValues[9]);

            $('#is-time-period-for-before-closure').prop('checked', columnValues[5].toString().toLowerCase() === 'true' ? true : false);

            $('#is-taxable').prop('checked', columnValues[8].toString().toLowerCase() === 'true' ? true : false);

            $('#is-applicable-on-death').prop('checked', columnValues[9].toString().toLowerCase() === 'true' ? true : false);

            $('#note-closing-charges', myModal).val(columnValues[10]);

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
                        isTimePeriodForBeforeClosure,
                        minimumChargesAmount,
                        maximumChargesAmount,
                        isTaxable,
                        isApplicableOnDeath,
                        note,
            ]).draw();
            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideColumnsClosingChargesDataTable()

            closingChargesDataTable.columns.adjust().draw();

            $('#closing-charges-accordion-title-error').addClass('d-none');

            EnableNewOperation('closing-charges');

            $('#closing-charges-modal').modal('hide');
        }
    });

    // Modal Update Button Event
    $('#btn-update-closing-charges-modal').click(function (event)
    {
        $('#select-all-closing-charges').prop('checked', false);
        if (IsValidClosingChargesDataTableModal())
        {
            closingChargesDataTable.row(selectedRowIndex).data([
                            tag,
                            chargesGeneralLedgerId,
                            chargesGeneralLedgerText,
                            fromTimePeriodInDays,
                            toTimePeriodInDays,
                            isTimePeriodForBeforeClosure,
                            minimumChargesAmount,
                            maximumChargesAmount,
                            isTaxable,
                            isApplicableOnDeath,
                            note,
            ]).draw();

            HideColumnsClosingChargesDataTable()

            closingChargesDataTable.columns.adjust().draw();

            $('#closing-charges-modal').modal('hide');

            EnableNewOperation('closing-charges');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-closing-charges-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-closing-charges tbody input[type="checkbox"]:checked').each(function () {
                    closingChargesDataTable.row($('#tbl-closing-charges tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();
                rowData = $('#btn-delete-closing-charges-dt').data('rowindex');

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!closingChargesDataTable.data().any())
                   $('#closing-charges-accordion-title-error').removeClass('d-none');

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
            $('#tbl-closing-charges tbody input[type="checkbox"]').each(function (index) {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = closingChargesDataTable.row(row).index();
                rowData = (closingChargesDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });


                $('#btn-delete-closing-charges-dt').data('rowindex', arr);
                EnableDeleteOperation('closing-charges')
            });
        }
        else {
            EnableNewOperation('closing-charges')
            $('#tbl-closing-charges tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-closing-charges tbody').click('input[type="checkbox"]', function () {
        $('#tbl-closing-charges input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');
            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = closingChargesDataTable.row(row).index();

                rowData = (closingChargesDataTable.row(selectedRowIndex).data());
                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('closing-charges');

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
        isTimePeriodForBeforeClosure = $('#is-time-period-for-before-closure').is(':checked') ? 'True' : 'False';
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
        else
            $('#closing-charges-general-ledger-id-error').addClass('d-none');

        // From Time Period In Days
        if (isNaN(fromTimePeriodInDays) === false) {
            minimum = parseInt($('#minimum-time-period-in-days').attr('min'));
            maximum = parseInt($('#minimum-time-period-in-days').attr('max'));

            if (parseInt(fromTimePeriodInDays) < parseInt(minimum) || parseInt(fromTimePeriodInDays) > parseInt(maximum)) {
                result = false;
                $('#minimum-time-period-in-days-error').removeClass('d-none');
            }
            else {
                $('#minimum-time-period-in-days-error').addClass('d-none');
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
            else {
                $('#maximum-time-period-in-days-error').addClass('d-none');
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
    function HideColumnsClosingChargesDataTable() {
        closingChargesDataTable.column(1).visible(false);
    }

    //@@@@@@@@@@@@@@@@@@@@@@@@@@ Tenure List  - DataTables Code @@@@@@@@@@@@@@@@@@@@@@@@@@

    //ClearSchemeTenureListModalInputs();

    // DataTable Add Button 
    $('#btn-add-scheme-tenure-list-dt').click(function () {

        event.preventDefault();
        SetModalTitle('scheme-tenure-list', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-scheme-tenure-list-dt').click(function () {

        SetModalTitle('scheme-tenure-list', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-scheme-tenure-list-dt').data('rowindex');
            id = $('#scheme-tenure-list-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs
            $('#tenure', myModal).val(columnValues[1]);

            $('.tenure-unit[value="' + columnValues[2] + '"]').prop('checked', true);

            $('#tenure-text', myModal).val(columnValues[4]);
            $('#note-scheme-tenure-list', myModal).val(columnValues[5]);

            // Show Modals
            myModal.modal({ show: true });
        }

        else {
            $('#btn-edit-scheme-tenure-list-dt').addClass('read-only');
            $('#scheme-tenure-list-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-scheme-tenure-list-modal').click(function (event) {
        if (IsValidSchemeTenureListDataTableModal()) {
            row = tenureListDataTable.row.add([
                    tag,
                    tenure,
                    tenureUnit,
                    tenureUnitText,
                    tenureText,
                    note,
            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideColumnsSchemeTenureListDataTable()

            tenureListDataTable.columns.adjust().draw();

            // Hide Table Required Data Error Message
            $('#tenure-list-accordion-error').addClass('d-none');

            $('#scheme-tenure-list-modal').modal('hide');

            $('#scheme-tenure-list-data-error').addClass('d-none');

            EnableNewOperation('scheme-tenure-list');
        }
    });

    // Modal Update Button Event
    $('#btn-update-scheme-tenure-list-modal').click(function (event) {
        $('#select-all-scheme-tenure-list').prop('checked', false);
        if (IsValidSchemeTenureListDataTableModal()) {
            tenureListDataTable.row(selectedRowIndex).data([
              tag,
              tenure,
              tenureUnit,
              tenureUnitText,
              tenureText,
              note,
            ]).draw();

            HideColumnsSchemeTenureListDataTable()

            tenureListDataTable.columns.adjust().draw();

            $('#scheme-tenure-list-modal').modal('hide');

            EnableNewOperation('scheme-tenure-list');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-scheme-tenure-list-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-scheme-tenure-list tbody input[type="checkbox"]:checked').each(function () {

                   tenureListDataTable.row($('#tbl-scheme-tenure-list tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();
                   rowData = $('#btn-delete-scheme-tenure-list-dt').data('rowindex');
                   EnableNewOperation('scheme-tenure-list');

                  $('#select-all-scheme-tenure-list').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!tenureListDataTable.data().any())
                    $('#tenure-list-accordion-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-scheme-tenure-list').click(function () {
        if ($(this).prop('checked')) {

            $('#tbl-scheme-tenure-list tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = tenureListDataTable.row(row).index();

                rowData = (tenureListDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });
                $('#btn-delete-scheme-tenure-list-dt').data('rowindex', arr);
                EnableDeleteOperation('scheme-tenure-list')
            });
        }
        else {
            EnableNewOperation('scheme-tenure-list')
            $('#tbl-scheme-tenure-list tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-scheme-tenure-list tbody').click('input[type="checkbox"]', function () {
        $('#tbl-scheme-tenure-list input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = tenureListDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (tenureListDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('scheme-tenure-list');

                    $('#btn-update-scheme-tenure-list-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-scheme-tenure-list-dt').data('rowindex', rowData);
                    $('#btn-delete-scheme-tenure-list-dt').data('rowindex', arr);
                    $('#select-all-scheme-tenure-list').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-scheme-tenure-list tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('scheme-tenure-list');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('scheme-tenure-list');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('scheme-tenure-list');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-scheme-tenure-list').prop('checked', true);
        else
            $('#select-all-scheme-tenure-list').prop('checked', false);
    });

    // Validate Tenure List Module
    function IsValidSchemeTenureListDataTableModal() {
        let result = true;

        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        tenure = parseInt($('#tenure').val());
        tenureUnit = $('.tenure-unit:checked').val();
        tenureUnitText = $('.tenure-unit:checked').next('label').text();
        tenureText = $('#tenure-text').val();
        note = $('#note-scheme-tenure-list').val();

        // Set Default Value, If Empty
        if (note === '') {
            note = 'None';
        }

        // Validate Tenure
        if (isNaN(tenure) === false) {
            minimum = parseInt($('#tenure').attr('min'));
            maximum = parseInt($('#tenure').attr('max'));

            if (parseInt(tenure) < parseInt(minimum) || parseInt(tenure) > parseInt(maximum)) {
                $('#tenure-error').removeClass('d-none');
                result = false;
            }
        }
        else {
            result = false;
            $('#tenure-error').removeClass('d-none');
        }

        // Tenure Text
        if (isNaN(tenureText.length) === false) {
            minimumLength = parseInt($('#tenure-text').attr('minlength'));
            maximumLength = parseInt($('#tenure-text').attr('maxlength'));

            if (parseInt(tenureText.length) < parseInt(minimumLength) || parseInt(tenureText.length) > parseInt(maximumLength)) {
                result = false;
                $('#tenure-text-error').removeClass('d-none');
            }
        }

        // Tenure Unit
        if (parseInt($('.tenure-unit:checked').length) < 1) {
            result = false;
            $('#tenure-unit-error').removeClass('d-none');
        }

        return result;
    };

    // Hide Unnecessary Columns
    function HideColumnsSchemeTenureListDataTable() {
        tenureListDataTable.column(2).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme General Ledger - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-general-ledger-dt').click(function ()
    {
        event.preventDefault();
        editedGeneralLedgerId = '';
        SetGeneralLedgerUniqueDropdownList();
        SetModalTitle('general-ledger', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-general-ledger-dt').click(function () {
        SetModalTitle('general-ledger', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
            columnValues = $('#btn-edit-general-ledger-dt').data('rowindex');

            editedGeneralLedgerId = columnValues[1];
            id = $('#general-ledger-modal').attr('id');
            myModal = $('#' + id).modal();


            activationDate = new Date(columnValues[3]);
            expiryDate = new Date(columnValues[4]);
            closeDate = new Date(columnValues[5]);

            $('#activation-date-general-ledger', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-general-ledger', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-general-ledger', myModal).val(GetInputDateFormat(closeDate));

            SetGeneralLedgerUniqueDropdownList();

            $('#scheme-general-ledger-id', myModal).val(columnValues[1]);
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
        debugger;
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
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-general-ledger tbody input[type="checkbox"]:checked').each(function () {
                    generalLedgerDataTable.row($('#tbl-general-ledger tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();
                    //rowData = $('#btn-delete-general-ledger-dt').data('rowindex');
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
        if (note ==='')
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

    //@@@@@@@@@@@@@@@@@@@@@@@@@@ Business Office -Datatable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    //  ClearBusinessOfficeModalInputs();

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

            id = $('#business-office-modal').attr('id');
            editedBusinessOfficeId = columnValues[1];

            SetBusinessOfficeUniqueDropdownList();

            myModal = $('#' + id).modal();

            activationDate = new Date(columnValues[3]);
            expiryDate = new Date(columnValues[4]);
            closeDate = new Date(columnValues[5]);

            $('#business-office-id').removeAttr('style');
            $('#business-office-id').removeAttr('multiple');
            $('.ms-options-wrap').css('display', 'none');

            $('#business-office-id', myModal).val(columnValues[1]);
            $('#activation-date-business-office', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-business-office', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-business-office', myModal).val(GetInputDateFormat(closeDate));
            $('#note-business-office', myModal).val(columnValues[6]);

            // Show Selected Dropdown List Item
            $('#business-office-id').find('option[value="' + columnValues[1] + '"]').show();

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

        if (IsValidBusinessOfficeDataTableModal())
        {
            $('#btn-add-business-office-modal').addClass('read-only')
            for (let i = 0, j = 0; i < businessOfficeId.length, j < businessOfficeIdText.length; i++, j++) {
                row = businessOfficeDataTable.row.add([
                      tag,
                      businessOfficeId[i],
                      businessOfficeIdText[j],
                      activationDate,
                      expiryDate,
                      closeDate,
                      note,
                ]).draw();

                rowNum++;

                row.nodes().to$().attr('id', 'tr' + rowNum);


                HideColumnsBusinessOfficeDataTable()

                businessOfficeDataTable.columns.adjust().draw();

                // Hide Table Required Data Error Message
                $('#business-office-accordian-error').addClass('d-none');

                EnableNewOperation('business-office');

                $('#business-office-modal').modal('hide');
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
                           businessOfficeIdText,
                           activationDate,
                           expiryDate,
                           closeDate,
                           note,
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
            if (confirm('Are You Sure To Delete This Row?')) {
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

                    // Display Error, If Table Has Not Any Record
                    if (!businessOfficeDataTable.data().any())
                        $('#business-office-accordian-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event  
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

                EnableDeleteOperation('business-office')

            });
        }
        else {
            EnableNewOperation('business-office')

            $('#tbl-business-office tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);

                EnableNewOperation('business-office')
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-business-office tbody').click("input[type=checkbox]", function () {
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
        businessOfficeIdText = $('#business-office-id option:selected').text();
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

        if (businessOfficeIdText === '') {
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

    //  @@@@@@@@@@@@@@@@@@@@@@@@@@ F U N C T I O N S @@@@@@@@@@@@@@@@@@@@@@@@@@


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Set Page Loading Default Values @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Set All Required Default Values On Page Loading
    function SetPageLoadingDefaultValues()
    {
        if ($('#amend-view').length > 0) {
            isAmendView = true;
        }

        if ($('#verify-view').length > 0) {
            isVerifyView = true;
        }

        depositType = $('.deposit-type:checked').val();

        if (depositType === DEMAND_DEPOSIT) {
            $('.demand-deposit').addClass('d-none');
        }
        else if (depositType == FIXED_DEPOSIT) {
            $('.fixed-deposit').addClass('d-none');
        }
        else if (depositType == RECURRING_DEPOSIT) {
            $('.recurring-deposit').addClass('d-none');
        }

        // Tenure Visiblity
        if ($('#enable-tenure').is(':checked'))
            $('.tenure-list').addClass('d-none');

        // Tenure List Visiblity
        if ($('#enable-tenure-list').is(':checked'))
            $('#enable-tenure-input').addClass('d-none');

        // Agent Incentive Data Table
        if (agentIncentiveDataTable.data().any())
            $('#enable-agent-incentive').prop('checked', true);
        else
            $('#enable-agent-incentive').prop('checked', false);

        // Target Esitmation Validation
        $('#number-of-account').attr('max', 9999);
        $('#turn-over-amount').attr('max', 999999999);

        // Define change event
        if ($('.interest-payout-day:checked').val() === 'CST') {
            $('#interest-payout-day-other-input').removeClass('d-none');
        }
        else {
            $('#interest-payout-day-other-input').addClass('d-none');
            $('#interest-payout-day-other').val(1);
        }

        if ($('#maximum-override-amount-limit').val() > 0)
            $('#enable-payout-interest-amount-override').prop('checked', true);
        else
            $('#enable-payout-interest-amount-override').prop('checked', false);

        if ($('#is-required-security').is(':checked'))
            $('.security-amount').removeClass('d-none');
        else
            $('.security-amount').addClass('d-none');

        // Notice Schedule Visible If & Only If Visible Any One Of SMS / Email
        let isVisibleNoticeSchedule = $('#enable-sms-service').is(':checked') || $('#enable-email-service').is(':checked') ? true : false;

        if (isVisibleNoticeSchedule)
            $('#notice-schedule').removeClass('d-none');
        else
            $('#notice-schedule').addClass('d-none');

        $('.gender').addClass('d-none');
        $('.occupation').addClass('d-none');

        SetDocumentUploadInput(ALL);

        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();

        TimePeriodUnitFocusOutEventFunction();

        if (isAmendView || isVerifyView) {
            NumberOfAccountUnitChangeEventFunction();
            TurnOverUnitChangeEventFunction();

            // Reset To Work Normal Page Validations
            isAmendView = false;
        }
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

    function SetReportFormatUniqueDropdownList()
    {
        // Show All List Items
        $('#report-format-id').html('');
        $('#report-format-id').append(REPORT_FORMAT_DROPDOWN);

        // Hide Added DropdownList Items
        $('#tbl-report-format > tbody > tr').each(function ()
        {
            currentRow = $(this).closest('tr');
            let myColumnValues = (reportTypeFormatDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null)
                if (myColumnValues[1] != editedReportFormatId)
                    $('#report-format-id').find('option[value="' + myColumnValues[1] + '"]').remove();
        });
    }

    function SetBusinessOfficeUniqueDropdownList()
    {
        // Show All List Items
        $('#business-office-id').html('');
        $('#business-office-id-multi-select-ul').html('');

        $('#business-office-id').html(BUSINESS_OFFICE_DROPDOWN);
        $('#business-office-id-multi-select-ul').html(BUSINESS_OFFICE_DROPDOWN_MULTI_SELECT_LIST);

        // To Get All Table Records
        businessOfficeDataTable.page.len(-1).draw();

        // Hide Inserted Dropdownlist Items
        $('#tbl-business-office > tbody > tr').each(function ()
        {
            currentRow = $(this).closest('tr');
            let myColumnValues = (businessOfficeDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null)
            {
                if (myColumnValues[1] != editedBusinessOfficeId)
                {
                    $('#business-office-id').find('option[value="' + myColumnValues[1] + '"]').remove();
                    $('.ms-options-wrap').find('input[type="checkbox"][value="' + myColumnValues[1] + '"]').closest('li').remove();
                }
            }
        });
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function ()
    {
        debugger;
        let isValidAllInputs = true;

        let isValidTenureInput = $('.deposit-type:checked').val() == DEMAND_DEPOSIT ? true : $('#enable-tenure').is(':checked') || $('#enable-tenure-list').is(':checked');

        if ($('form').valid())
        {
            $('.lastrow').remove();

            // Return List Object, Hence Create Array
            let agentIncentiveArray = new Array();
            let numberOfTransactionLimitArray = new Array();
            let transactionAmountLimitArray = new Array();
            let reportTypeFormatArray = new Array();
            let noticeScheduleArray = new Array();
            let closingChargesArray = new Array();
            let tenureListArray = new Array();
            let generalLedgerArray = new Array();
            let businessOfficeArray = new Array();
            let targetGroupArray = new Array();

            agentIncentiveDataTable.page.len(-1).draw();
            numberOfTransactionLimitDataTable.page.len(-1).draw();
            transactionAmountLimitDataTable.page.len(-1).draw();
            reportTypeFormatDataTable.page.len(-1).draw();
            noticeScheduleDataTable.page.len(-1).draw();
            closingChargesDataTable.page.len(-1).draw();
            tenureListDataTable.page.len(-1).draw();
            generalLedgerDataTable.page.len(-1).draw();
            businessOfficeDataTable.page.len(-1).draw();
            targetGroupDataTable.page.len(-1).draw();

            // Validate Scheme Name
            if (!isValidSchemeName)
            {
                isValidAllInputs = false;
                $('#name-of-scheme-error').removeClass('d-none');
            }
            else
                $('#name-of-scheme-error').addClass('d-none');

            // Validate Deposit Type
            if (typeof $('.deposit-type:checked').val() == 'undefined')
            {
                isValidAllInputs = false;
                $('#deposit-type-required-error').removeClass('d-none');
            }
            else
                $('#deposit-type-required-error').addClass('d-none');

            // Validate Tenure
            if (!isValidTenureInput)
            {
                isValidAllInputs = false;
                $('#tenure-required-error').removeClass('d-none');
            }
            else
                $('#tenure-required-error').addClass('d-none');



            // Accordion 1 - Tenure Validation
            if (!IsValidTenureAccordionInputs())
                isValidAllInputs = false;

            // Accordion 2 - Application Parameter Validation, If Enable
            if (!IsValidApplicationNumberAccordionInputs())
                isValidAllInputs = false;

            // Accordion 3 - Customer Account Parameter Validation, If Enable
            if (!IsValidCustomerAccountNumberAccordionInputs())
                isValidAllInputs = false;

            // Accordion 4 - Account Parameter Validation, If Enable
            if (!IsValidAccountParameterAccordionInputs())
                isValidAllInputs = false;

            // Accordion 5 - Limit Validation, If Enable
            if (!IsValidLimitAccordionInputs())
                isValidAllInputs = false;

            // Accordion 6 - Demand Deposit Detail Validation, If Enable
            if (!IsValidDemandDepositDetailAccordionInputs())
                isValidAllInputs = false;

            // Accordion 7 - Target Estimation Validation, If Enable
            if (!IsValidTargetEstimationAccordionInputs())
                isValidAllInputs = false;

            // Accordion 8 - Certificate Parameter Validation, If Enable
            if (!IsValidCertificateNumberAccordionInputs())
                isValidAllInputs = false;

            // Accordion 9 - Interest Rate Validation, If Enable
            if (!IsValidInterestRateAccordionInputs())
                isValidAllInputs = false;

            //// Accordion 10 - Interest Provision Validation, If Enable
            //if (!IsValidInterestProvisionAccordionInputs())
            //    isValidAllInputs = false;

            // Accordion 11 - Passbook Detail Validation, If Enable
            if (!IsValidPassbookDetailAccordionInputs())
                isValidAllInputs = false;

            // Accordion 12 - Agent Parameter Validation, If Enable
            if (!IsValidAgentParameterAccordionInputs())
                isValidAllInputs = false;

            // Accordion 13 - Account Closure Validation, If Enable
            if (!IsValidAccountClosureAccordionInputs())
                isValidAllInputs = false;

            // Accordion 14 - Photo Sign Validation, If Enable
            if (!IsValidPhotoInputs())
                isValidAllInputs = false;

            if (!IsValidSignInputs())
                isValidAllInputs = false;

            // Accordion 15 - Installment Parameter Validation, If Enable
            if (!IsValidInstallmentParameterAccordionInputs())
                isValidAllInputs = false;

            // Accordion 16 - Fixed Deposit Validation, If Enable
            if (!IsValidFixedDepositAccordionInputs())
                isValidAllInputs = false;

            // Accordion 17 - Renewal Parameter Validation, If Enable
            if (!IsValidRenewalParameterAccordionInputs())
                isValidAllInputs = false;

            // Create Array For Notice Schedule Data Table To Pass Data(Optional, Not Mandatory)
            if ($('#enable-sms-service').is(':checked') || $('#enable-email-service').is(':checked')) {
                if (noticeScheduleDataTable.data().any()) {
                    if (isValidAllInputs) {

                        // Get Data Table Values In Notice Schedule Array
                        $('#tbl-notice-schedule > tbody > tr').each(function () {
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
                        //isValidAllInputs = true;
                    }
                }

            }

            // Create Array For Transaction Amount Limit Data Table To Pass Data
            if ($('#enable-transaction-amount-limit').is(':checked')) {
                if (transactionAmountLimitDataTable.data().any()) {

                    $('#transaction-amount-limit-accordian-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-transaction-amount-limit > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');
                            columnValues = (transactionAmountLimitDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {

                                transactionAmountLimitArray.push({
                                    'TransactionTypeId': columnValues[1],
                                    'MinimumAmountLimit': columnValues[3],
                                    'MaximumAmountLimit': columnValues[4],
                                    'Note': columnValues[5],
                                });
                            }
                            else
                                return false;
                        });
                        // isValidAllInputs = true;
                    }
                }
                else {
                    $('#transaction-amount-limit-accordian-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Report Format Table(Optional, Not Mandatory)
            if ($('#report-format-card').hasClass('d-none') === false) {
                if (reportTypeFormatDataTable.data().any()) {
                    if (isValidAllInputs) {

                        $('#tbl-report-format > tbody > tr').each(function () {
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

            // NumberOfTransactionLimitTable
            if ($('#enable-number-of-transaction-limit').is(':checked')) {
                if (numberOfTransactionLimitDataTable.data().any()) {

                    $('#number-of-transaction-limit-accordian-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-number-of-transaction-limit > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (numberOfTransactionLimitDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {

                                numberOfTransactionLimitArray.push({
                                    'TransactionTypeId': columnValues[1],
                                    'MinimumNumberOfTransaction': columnValues[3],
                                    'MaximumNumberOfTransaction': columnValues[4],
                                    'TimePeriodUnitId': columnValues[5],
                                    'Note': columnValues[7],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#number-of-transaction-limit-accordian-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For ClosingCharges Data Table To Pass Data
            if ($('#enable-closing-charges').is(':checked')) {
                if (closingChargesDataTable.data().any()) {
                    $('#closing-charges-accordion-title-error').addClass('d-none');

                    if (isValidAllInputs) {

                        // Get Data Table Values In Notice Schedule Array
                        $('#tbl-closing-charges > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (closingChargesDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                closingChargesArray.push(
                                 {
                                     'GeneralLedgerId': columnValues[1],
                                     'FromTimePeriodInDays': columnValues[3],
                                     'ToTimePeriodInDays': columnValues[4],
                                     'IsTimePeriodForBeforeClosure': columnValues[5],
                                     'MinimumChargesAmount': columnValues[6],
                                     'MaximumChargesAmount': columnValues[7],
                                     'IsTaxable': columnValues[8],
                                     'IsApplicableOnDeath': columnValues[9],
                                     'Note': columnValues[10],
                                 });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#closing-charges-accordion-title-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For Scheme Tenure List Data Table To Pass Data
            if ($('#enable-tenure-list').is(':checked')) {
                if (tenureListDataTable.data().any()) {
                    $('#tenure-list-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {

                        // Get Data Table Values In Loan Scheme Menu Array
                        $('#tbl-scheme-tenure-list > tbody > tr').each(function () {

                            currentRow = $(this).closest('tr');

                            columnValues = (tenureListDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                tenureListArray.push({
                                    'Tenure': columnValues[1],
                                    'TenureUnit': columnValues[2],
                                    'TenureText': columnValues[4],
                                    'Note': columnValues[5],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#tenure-list-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For targetGroupArray Table To Pass Data
            if ($('#enable-target-group').is(':checked')) {
                debugger;
                if (targetGroupDataTable.data().any()) {
                    $('#target-group-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {
                        $('#tbl-target-group > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (targetGroupDataTable.row(currentRow).data());

                            let genderId = '';
                            let occupationId = '';

                            // Check if either gender or occupation is selected
                            if (columnValues[2].indexOf('Occupation') !== -1) {
                                occupationId = columnValues[3];
                                genderId = '';
                            }

                            if (columnValues[2].indexOf('Gender') !== -1) {
                                genderId = columnValues[3];
                                occupationId = '';
                            }

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                debugger;
                                targetGroupArray.push({
                                    'TargetGroupId': columnValues[1],
                                    'GenderId': genderId,
                                    'OccupationId': occupationId,
                                    'RequiredMembership': columnValues[5],
                                    'Note': columnValues[7]
                                });
                            }
                            else
                                return false;
                        });

                    }
                }
                else {
                    $('#target-group-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For scheme Deposit Agent Incentive Table To Pass Data
            if ($('#enable-agent-incentive').is(':checked')) {

                if (agentIncentiveDataTable.data().any()) {
                    $('#agent-incentive-accordian-error').addClass('d-none');
                    if (isValidAllInputs) {
                        $('#tbl-agent-incentive > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (agentIncentiveDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {

                                agentIncentiveArray.push({
                                    'MinimumCollectionAmount': columnValues[1],
                                    'MaximumCollectionAmount': columnValues[2],
                                    'IncentiveUnit': columnValues[3],
                                    'Incentive': columnValues[5],
                                    'RoundingMethod': columnValues[6],
                                    'Note': columnValues[8],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#agent-incentive-accordian-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For General Ledger Data Table To Pass Data
            if ($('#general-ledger-card').hasClass('d-none') ===false ) {
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

            // Create Array For Business Office Data Table To Pass Data
            if ($('#business-office-card').hasClass('d-none') === false) {
                if (businessOfficeDataTable.data().any()) {

                    $('#business-office-accordian-error').addClass('d-none');

                    if (isValidAllInputs) {

                        // Get Data Table Values In Business Office Array
                        $('#tbl-business-office > tbody > tr').each(function () {
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
                        // isValidAllInputs = true;
                    }
                }
                else {
                    $('#business-office-accordian-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Call Controller Save Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: url1,
                    type: 'POST',
                    async: false,
                    data: {
                        '_schemeDepositAgentIncentive': agentIncentiveArray,
                        '_schemeNumberOfTransactionLimit': numberOfTransactionLimitArray,
                        '_schemeTransactionAmountLimit': transactionAmountLimitArray,
                        '_schemeReportFormat': reportTypeFormatArray,
                        '_schemeNoticeSchedule': noticeScheduleArray,
                        '_schemeGeneralLedger': generalLedgerArray,
                        '_schemeBusinessOffice': businessOfficeArray,
                        '_schemeClosingChargesDetail': closingChargesArray,
                        '_tenureList': tenureListArray,
                        '_targetGroup': targetGroupArray
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In PersonDepositAgentIncentive DataTable!!! Error Message - ' + error.toString());
                    }
                })
            }
            else {
                alert('Oops, Something Went Wrong!, Please Provide Valid Data');
                event.preventDefault();
            }
        }
        else
        {
            if (!isValidTenureInput)
                $('#tenure-required-error').removeClass('d-none');
            else
                $('#tenure-required-error').addClass('d-none');

            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }

    });
});

