// Document Ready Function
'use strict'
$(document).ready(function () {
    debugger;
    const DISABLE_VALUE = 'D';
    const FLAT_AMOUNT = 'F';
    const FIXED_FLAT = 'FLAT';
    const MANDATORY_VALUE = 'M';
    const NUMBER_UNIT = 'N';
    const ALL = 'All';
    const NO_ROUNDING = 'NOR';

    // All Values Get From LoanType And Account Class (Both Table Has Same Values)
    const BUSINESS_LOAN = 'SBL';
    const CASH_CREDIT_LOAN = 'CCL';
    const CONSUMER_DURABLE_LOAN = 'CDL';
    const GOLD_LOAN = 'GDL';
    const GUARANTOR_LOAN = 'GRL';
    const HOME_LOAN = 'HML';
    const LOAN_AGAINST_FIXED_DEPOSITE = 'LAD';
    const EDUCATION_LOAN = 'EDU';
    const LOAN_AGAINST_PROPERTY = 'LAP';
    const VEHICLE_LOAN = 'VHL';

    // Constant For Dropdown
    const BUSINESS_OFFICE_DROPDOWN = $('#business-office-id').html();
    const BUSINESS_OFFICE_DROPDOWN_MULTI_SELECT_LIST = $('#business-office-id-multi-select-ul').html();
    const CHARGES_DETAIL_DROPDOWN = $('#charges-type-id').html();
    /*const GENERAL_LEDGER_DROPDOWN = $('#scheme-general-ledger-id').html();*/
    const PRE_VEHICLE_DROPDOWN = $('#pre-vehicle-type-id').html();
    const REPORT_FORMAT_DROPDOWN = $('#report-format-id').html();
    const VEHICLE_TYPE_DROPDOWN = $('#vehicle-type-id').html();
    const EDUCATIONAL_COURSE_DROPDOWN = $('#educational-course-id').html();
    const EDUCATIONAL_COURSE_DROPDOWN_MULTI_SELECT_LIST = $('#educational-course-id-multi-select-ul').html();
    const EDUCATIONAL_INSTITUTE_DROPDOWN = $('#institute-id').html();
    const EDUCATIONAL_INSTITUTE_DROPDOWN_MULTI_SELECT_LIST = $('#institute-id-multi-select-ul').html();

    let activationDate = '';
    let closeDate = '';
    let documentDropdownList = '';
    let dropdownListItems = '';
    let expiryDate = '';
    let id = '';
    let isAmendView = false;
    let isVerifyView = false;
    let isValidSchemeName = true;
    let loanSanctionAuthorityLimit;
    let loanTypeId = ''
    let note = '';
    let sysNameOfLoanType = '';
    let listItemCount = 0;

    // @@@@@@@@@@ Data Table Related Varible Declaration

    let arr = new Array();
    let checked;
    let columnValues;
    let currentRow;
    let isCheckedAll = false;
    let isChecked = false;
    let maximum = 0;
    let minimum = 0;
    let minimumLength = 0;
    let maximumLength = 0;
    let multiSelectCount = 0;
    let eventObjId = [];
    let len = 0;
    let myModal;
    let prevloanTypeId = '0';
    let result = true;
    let row;
    let rowData;
    let rowNum = 0;
    let selectedRowIndex;
    let tag = '';

    //Education Loan
    let educationalCourseId = '';
    let educationalCourseText = '';
    let educationalInstituteId = '';
    let educationalInstituteText = '';
    let editedEducationalCourseId = '';
    let editedEducationalInstituteId = '';

    // NoticeSchedule
    let isVisibleNoticeSchedule = false;

    // ReportFormat
    let editedReportFormatId = '';
    let reportFormatId = '';
    let reportFormatText = '';

    // TargetGroup
    let genderId = '';
    let occupationId = '';
    let requiredMember;
    let requiredMemberText;
    let targetGroupId = '';
    let targetGroupIdText = '';
    let valueId = '';
    let valueText = '';

    // NoticeSchedule
    let noticeTypeId = '';
    let noticeScheduleText = '';
    let communicationMediaId = '';
    let communicationMediaText = '';
    let scheduleId = '';
    let scheduleText = '';

    //SchemeTenure
    let tenure;
    let tenureText = '';
    let tenureUnit = '';
    let tenureUnitText = '';

    //Document
    let documentId = '';
    let documentIdText = '';
    let documentAllowedFileFormatsForLocalStorageId = '';
    let documentAllowedFileFormatsForLocalStorageText = '';
    let documentAllowedFileFormatsForDbId = '';
    let documentAllowedFileFormatsForDbText = '';
    let documentLocalStoragePath = '';
    let editedDocumentId = '';
    let enableDocumentUploadInDb = false;
    let enableDocumentUploadInLocalStorage = false;
    let isRequired = false;
    let maximumFileSizeForDocumentUploadInDb = 0;
    let maximumFileSizeForDocumentUploadInLocalStorage;

    // Loan charges
    let chargesApplyingTypeId = '';
    let chargesApplyingTypeIdText = '';
    let chargesGeneralLedgerId = '';
    let chargesGeneralLedgerText = '';
    let chargesPercentage = '';
    let defaultChargesAmount = 0;
    let editedChargesType = '';
    let isApplicableTax = false;
    let isOptional = false;
    let lendingChargesBaseId = '';
    let lendingChargesBaseIdText = '';
    let maximumChargesAmount = 0;
    let minimumChargesAmount = 0;

    // generalLedger
    let editedGeneralLedgerId = '';
    let generalLedgerId;
    let generalLedgerIdText;

    // business office                           
    let businessOfficeId = '';
    let businessOfficeIdText = '';
    let editedBusinessOfficeId = '';

    // LoanOverduesAction
    let loanRecoveryActionId = '';
    let loanRecoveryActionIdText = '';
    let minimumOverduesInstallment = 0;
    let maximumOverduesInstallment = 0;

    // Vehicle Type Loan
    let downPaymentPercentage = 0;
    let editedVehicleLoanId = '';
    let enableVehiclePhotoUploadInDb = false;
    let enableVehiclePhotoUploadInLocalStorage = false;
    let maximumFileSizeForVehiclePhotoUploadInDb = 0;
    let maximumFileSizeForVehiclePhotoUploadInLocalStorage = 0;
    let maximumLoanAmountWithoutExtraSecurity = 0;
    let maximumNumberOfPhoto = 0;
    let minimumNumberOfPhoto = 0;
    let vehiclePhotoAllowedFileFormatsForDbId = '';
    let vehiclePhotoAllowedFileFormatsForDbText = '';
    let vehiclePhotoAllowedFileFormatsForLSId = '';
    let vehiclePhotoAllowedFileFormatsForLSText = '';
    let vehiclePhotoUploadLocalStoragePath = '';
    let vehiclePhotoUpload = '';
    let vehiclePhotoUploadText = '';

    // Pre Owned Loan 
    let editedPreVehicleId = '';
    let enablePreOwnedUploadInDb = false;
    let enablePhotoUploadInLocalStoragePre = false;
    let enableVehicleInspection = false;
    let maximumLoanSanctionPercentage1 = 0;
    let maximumLoanSanctionPercentage2 = 0;
    let maximumLoanSanctionPercentage3 = 0;
    let maximumLoanSanctionPercentage4 = 0;
    let maximumTenure1 = 0;
    let maximumTenure2 = 0;
    let maximumTenure3 = 0;
    let maximumTenure4 = 0;
    let preOwnedAllowedFileFormatsForDbId = '';
    let preOwnedAllowedFileFormatsForDbText = '';
    let preOwnedAllowedFileFormatsForLocalStorageId = '';
    let preOwnedAllowedFileFormatsForLocalStorageText = '';
    let preOwnedLocalStoragePath = '';
    let preOwnedMaximumFileSizeDb = 0;
    let preOwnedMaximumFileSizeForLocalStorage = 0;
    let preOwnedMaximumNumberOfPhoto = 0;
    let preOwnedMinimumNumberOfPhoto = 0;
    let preOwnedPhotoUpload = '';
    let preOwnedPhotoUploadText = '';
    let vehicleLife1 = 0;
    let vehicleLife2 = 0;
    let vehicleLife3 = 0;
    let vehicleLife4 = 0;
    let vehicleTypeId = '';
    let vehicleTypeIdText = '';

    // SchemeLoanOverduesAction
    let margin = '';
    let nameOfItemId = '';
    let nameOfItemIdText = '';

    // Interest
    let byLawsMinimumFineInterest = 0;
    let byLawsMaximumFineInterest = 0;
    let interestMethodType = '';
    let selectedInterestMethodType = '';
    let generalLedgerDropdownListItemsByLoanType = '';

    // CreateDataTable
    let businessOfficeDataTable = CreateDataTable('business-office');
    let consumerDurableLoanDataTable = CreateDataTable('consumer-durable-loan');
    let documentDataTable = CreateDataTable('document');
    let generalLedgerDataTable = CreateDataTable('general-ledger');
    let loanChargesDataTable = CreateDataTable('charges');
    let loanOverduesActionDataTable = CreateDataTable('loan-overdues-action');
    let noticeScheduleDataTable = CreateDataTable('notice-schedule');
    let reportFormatDataTable = CreateDataTable('report-format-data-table');
    let targetGroupDataTable = CreateDataTable('target-group');
    let tenureListDataTable = CreateDataTable('tenure-list');
    let preOwnedVehicleDataTable = CreateDataTable('pre-owned-vehicle-loan');
    let vehicleTypeLoanDataTable = CreateDataTable('vehicle-type-loan');
    let educationalCourseDataTable = CreateDataTable('educational-course');
    let instituteDataTable = CreateDataTable('educational-institute');

    // *************** C A L L   P A G E   L O A D I N G   D E F A U L T   V A L U E S ***************

    SetPageLoadingDefaultValues();

    function SetDocumentUploadInput(_input) {
        // Document Upload Input Visibility Based On Selection i.e. Mandatory, Optional, Disable

        // eventObjId Is Collection Of All Accordions Document Value i.e. Mandatory, Optional, Disable
        // Using Naming Convention Based On eventObjId All Corrospondent Inputs Are Show And Hide
        debugger;

        // Document Upload Visibility Based On Selecting Parameter

        // Other Remaining Accordion Visibility
        // -db-ts = database toggle switch         -ls-ts = local storage toggle switch
        // -db-tf = database text fields           -ls-tf = local storage text fields
        // -ls-pt = Local Storage Path
        let documentUploadValue = '';

        if (_input === ALL) {
            eventObjId = ['document-upload', 'gold-photo-upload', 'vehicle-photo-upload', 'pre-owned-photo-upload'];
        }
        else {
            eventObjId = [];
            eventObjId.push(_input);
        }

        len = eventObjId.length

        for (let i = 0; i < len; i++) {
            if (eventObjId[i] === 'document-upload') {
                documentUploadValue = MANDATORY_VALUE;
            }
            else {
                documentUploadValue = $('input[id=' + eventObjId[i] + ']:checked').val();
            }

            if (documentUploadValue === DISABLE_VALUE || typeof documentUploadValue === 'undefined') {
                ResetFileUpload(eventObjId[i]);
            }
            else {
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
                    $('#' + eventObjId[i] + '-required-error').addClass('d-none');
                }
                else {
                    $('.' + eventObjId[i] + '-db-ms').prop('disabled', true);
                    $('.' + eventObjId[i] + '-db-ms > option').prop('selected', false);
                    $('.' + eventObjId[i] + '-db-tf').removeClass('mandatory-mark');
                    $('.' + eventObjId[i] + '-db-tf').prop('disabled', true);
                    $('.' + eventObjId[i] + '-db-tf').prop('min', 0);
                    $('.' + eventObjId[i] + '-db-tf').val(0);
                    $('#' + eventObjId[i] + '-allowed-file-format-db-error').addClass('d-none');
                    $('#' + eventObjId[i] + '-maximum-file-size-db-error').addClass('d-none');

                    $('.' + eventObjId[i] + '-ls-ts').prop('disabled', false);

                    objSelect2.trigger('change');
                    modalObjSelect2.trigger('change');
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
                    $('#' + eventObjId[i] + '-required-error').addClass('d-none');

                }
                else {
                    $('.' + eventObjId[i] + '-ls-ms').prop('disabled', true);
                    $('.' + eventObjId[i] + '-ls-ms > option').prop('selected', false);
                    $('.' + eventObjId[i] + '-ls-tf').removeClass('mandatory-mark');
                    $('.' + eventObjId[i] + '-ls-tf').prop('disabled', true);
                    $('.' + eventObjId[i] + '-ls-tf').attr('min', 0);
                    $('.' + eventObjId[i] + '-ls-tf').val(0);
                    $('.' + eventObjId[i] + '-ls-pt').prop('disabled', true);
                    $('.' + eventObjId[i] + '-ls-pt').removeClass('mandatory-mark');
                    $('.' + eventObjId[i] + '-ls-pt').val('');
                    $('#' + eventObjId[i] + '-allowed-file-format-ls-error').addClass('d-none');
                    $('#' + eventObjId[i] + '-maximum-file-size-ls-error').addClass('d-none');
                    $('#' + eventObjId[i] + '-local-storage-path-error').addClass('d-none');

                    $('.' + eventObjId[i] + '-db-ts').prop('disabled', false);

                    objSelect2.trigger('change');
                    modalObjSelect2.trigger('change');
                }
            }
        }
    }

    // Set All Required Default Values On Page Loading
    function SetPageLoadingDefaultValues() {
        debugger;
        if ($('#amend-view').length > 0) {
            isAmendView = true;
        }

        if ($('#verify-view').length > 0) {
            isVerifyView = true;
        }

        // Set Unique Dropdown List
        SetBusinessOfficeUniqueDropdownList();
        SetEducationalCourseUniqueDropdownList();
        SetEducationalInstituteUniqueDropdownList();
        SetChargesUniqueDropdownList();
        SetDocumentUniqueDropdownList();
        SetGeneralLedgerUniqueDropdownList();
        SetPreOwnedVehicleUniqueDropdownList();
        SetReportFormatUniqueDropdownList();
        SetVehicleTypeUniqueDropdownList();

        // Notice Schedule Visible If & Only If Visible Any One Of SMS / Email
        isVisibleNoticeSchedule = $('#enable-sms-service').is(':checked') ? true : false;

        if (isVisibleNoticeSchedule === false)
            isVisibleNoticeSchedule = $('#enable-email-service').is(':checked') ? true : false;

        if (isVisibleNoticeSchedule)
            $('#notice-schedule-card').removeClass('d-none');
        else
            $('#notice-schedule-card').addClass('d-none');

        // Round Interest Visiblity
        RoundingOfInterestChangeEventFunction();

        // Round Principal Visiblity
        RoundingOfPrincipalChangeEventFunction();

        // Tenure Visiblity
        if ($('#enable-tenure').is(':checked'))
            $('.tenure-list').addClass('d-none');

        // Tenure List Visiblity
        if ($('#enable-tenure-list').is(':checked'))
            $('#enable-tenure-input').addClass('d-none');

        // LoanTypeId
        loanTypeId = $('#loan-type-id option:selected').val();

        // LoanType
        SetLoanTypeParameter();
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   Change Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // On Any Document Upload Option Button Change Event (i.e. Every Mandatory, Optional, Disable)
    $('.document-upload').change(function () {
        // Get Id
        let eventObjId = this.id;

        // If Values Are Mandatory Or Optional - Make It Writable i.e. Remove Read-Only Class
        // -db-ts = database toggle switch         -ls-ts = local storage toggle switch
        // -db-tf = database text fields           -ls-tf = local storage text fields
        // -db-ms = database multiselect           -ls-ms = local storage multi select i.e file formats

        // If Not Disable i.e For Mandatory Or Optional
        if ($(this).val() !== DISABLE_VALUE) {
            SetDocumentUploadInput(eventObjId);
        }
        else {
            ResetFileUpload(eventObjId);
        }
    });

    // Auto Application Number Branchwise
    $('#enable-account-number-branchwise').change(function () {
        $('#account-number-accordion-title-error').addClass('d-none');
        $('#auto-account-number-block').addClass('d-none');
    });

    // Auto Agreement Number Branchwise
    $('#enable-agreement-number-branchwise').change(function () {
        $('#auto-agreement-number-block').addClass('d-none');
        $('#agreement-number-accordion-title-error').addClass('d-none');

    });

    // Change Event On Enable Agreement Number
    $('#enable-agreement-number').change(function () {
        IsValidAgreemetNumberAccordionInputs();

        $('#agreement-number-accordion-title-error').addClass('d-none');
        $('#auto-agreement-number-block').addClass('d-none');
        $('#agreement-number-branchwise-block').removeClass('d-none');
    });

    // Change Event On Enable Application
    $('#enable-application').change(function () {
        IsValidApplicationNumberAccordionInputs();

        $('#application-number-accordion-title-error').addClass('d-none');
        $('#auto-application-number-block').addClass('d-none');
        $('#application-number-branchwise-block').removeClass('d-none');
    });

    // Auto Application Number Branchwise
    $('#enable-application-number-branchwise').change(function () {
        $('#application-number-accordion-title-error').addClass('d-none');
        $('#auto-application-number-block').addClass('d-none');
    });

    // Auto Account Number
    $('#enable-auto-account-number').change(function () {
        IsValidCustomerAccountNumberAccordionInputs();
    });

    // Auto Agreement Number
    $('#enable-auto-agreement-number').change(function () {
        IsValidAgreemetNumberAccordionInputs();
    });

    // Auto Application Number
    $('#enable-auto-application-number').change(function () {
        IsValidApplicationNumberAccordionInputs();
    });

    // Auto Passbook Number
    $('#enable-auto-passbook-number').change(function () {
        IsValidPassbookDetailAccordionInputs();
    });

    // Enable Gold Photo
    $('#enable-gold-photo').change(function () {
        ResetFileUpload('gold-photo-upload');
    });

    function LoanInstallmentChangeEventFunction() {
        // Clear Values Of Effected Inputs Only On Create View
        if (isAmendView === false && isVerifyView === false) {
            $('#number-of-missed-installment').val('');
            $('#fine-days').val('');
        }

        if ($('#enable-loan-installment').is(':checked')) {
            $('#installment-repayment-schedule-accordions').removeClass('d-none');
            $('#fine-on-missed-installment-input').removeClass('d-none');

            $('#fine-days-input').addClass('d-none');
        }
        else {
            $('#installment-repayment-schedule-accordions').addClass('d-none');
            $('#fine-on-missed-installment-input').addClass('d-none');

            $('#fine-days-input').removeClass('d-none');
        }
    }

    // loan installment
    $('#enable-loan-installment').change(function () {
        LoanInstallmentChangeEventFunction();
    });

    // Auto Passbook Number Branchwise
    $('#enable-passbook-number-branchwise').change(function () {
        $('#passbook-detail-accordion-title-error').addClass('d-none');
        $('#auto-passbook-number-block').addClass('d-none');
    });

    // Change Event On Enable Passbook Detail
    $('#enable-passbook-detail').change(function (event) {
        IsValidPassbookDetailAccordionInputs();

        $('#passbook-detail-accordion-title-error').addClass('d-none');
        $('#auto-passbook-number-block').addClass('d-none');
        $('#passbook-number-branchwise-block').removeClass('d-none');
    });

    // EnableApplicableAllGeneralLedger
    $('#enable-applicable-all-general-ledgers').change(function (event) {
        if ($('[multiple = "multiple"]').hasClass('loan-against-deposit-input')) {
            $('.ms-options-wrap > button').text('Select Proper Values');
            $('.ms-options-wrap > .ms-options > ul input[type="checkbox"]').prop('checked', false);
            $('.ms-options-wrap > .ms-options > .ms-search input').val('');
            $('li').removeClass('selected');

            if ($('.ms-options-wrap > .ms-options > .ms-selectall').hasClass('global'))
                $('.ms-options-wrap > .ms-options > .ms-selectall').text('Select all');
        }
    });

    // Validation for Fine Interest Parameter
    $('#interest-method-id').focusout(function () {
        debugger;
        if (isVerifyView === false) {
            FineInterestParameter();
        }
    });

    // Validation for Interest Parameter in Focusout
    $('#interest-method-id-loan-interest').focusout(function () {
        debugger;
        if (isVerifyView === false) {
            interestParameter();
        }
    });

    // Notice Schedule
    $('.notice-schedule').change(function () {
        isVisibleNoticeSchedule = $('#enable-sms-service').is(':checked') ? true : false;

        if (isVisibleNoticeSchedule === false) {
            isVisibleNoticeSchedule = $('#enable-email-service').is(':checked') ? true : false;
            $('#notice-schedule-card').addClass('d-none');
        }
        else {
            $('#notice-schedule-card').removeClass('d-none');
        }
    });

    // Rounding Of Interest 
    $('.rounding-of-interest').change(function () {
        RoundingOfInterestChangeEventFunction();
    });

    // Rounding Of Principal 
    $('.rounding-of-principal').change(function () {
        RoundingOfPrincipalChangeEventFunction();
    });

    // Loan Fine Interest Parameter 
    $('#enable-loan-fine-interest-parameter').change(function () {
        debugger;
        selectedInterestMethodType = '';
        $('.fine-interest-unit-per').removeClass('d-none');
        $('.charged-duration-id').removeClass('d-none');
        $('.days-in-year').removeClass('d-none');
        $('.interest-calculation-id').removeClass('d-none');
    });

    // On Every Toggle Switch Button Change Event (i.e. Every Enable Upload In Database Storage)
    $('.toggle-switch-db').change(function () {
        debugger;
        let eventObjId = this.id;

        SetDocumentUploadInput(eventObjId.replace('-dbts', ''));
    });

    // On Every Toggle Switch Button Change Event (i.e. Every Enable Upload In Local Storage)
    $('.toggle-switch-ls').change(function () {
        debugger;
        let eventObjId = this.id;

        SetDocumentUploadInput(eventObjId.replace('-lsts', ''));
    });

    $('.vehicle-photo-upload-db-ts').change(function () {
        debugger;
        $('#vehicle-photo-upload-allowed-file-format-db-error').addClass('d-none');
        $('#vehicle-photo-upload-maximum-file-size-db-error').addClass('d-none');

    });

    $('.vehicle-photo-upload-ls-ts').change(function () {
        debugger;
        $('#vehicle-photo-upload-local-storage-path-error').addClass('d-none');
        $('#vehicle-photo-upload-allowed-file-format-ls-error').addClass('d-none');
        $('#vehicle-photo-upload-maximum-file-size-ls-error').addClass('d-none');
    });

    // Validation for Fine Interest Parameter
    function FineInterestParameter() {
        debugger;
        let interestMethod = $('#interest-method-id option:selected').val();

        $.get('/AccountChildAction/GetSysNameOfInterestMethodTypeById', { _interestMethodId: interestMethod, async: false }, function (interestMethodType, textStatus, jqXHR) {
            debugger;
            if (interestMethodType == FIXED_FLAT) {
                $('.fine-interest-unit-per').addClass('d-none');
                $('.charged-duration-id').addClass('d-none');
                $('.days-in-year').addClass('d-none');
                $('.interest-calculation-id').addClass('d-none');
            }
            else {
                $('.fine-interest-unit-per').removeClass('d-none');
                $('.charged-duration-id').removeClass('d-none');
                $('.days-in-year').removeClass('d-none');
                $('.interest-calculation-id').removeClass('d-none');
            }
            selectedInterestMethodType = interestMethodType;
        });
    }

    // Validation for Interest Parameter
    function interestParameter() {
        debugger;
        let interestMethodId = $('#interest-method-id-loan-interest option:selected').val();

        $.get('/AccountChildAction/GetSysNameOfInterestMethodTypeById', { _interestMethodId: interestMethodId, async: false }, function (interestMethodIdType, textStatus, jqXHR) {
            debugger;
            if (interestMethodIdType == 'COMPOUND')
                $('#interest-compounding').removeClass('d-none');
            else
                $('#interest-compounding').addClass('d-none');

        });

    }

    // Reset File Upload Of All Upload Configuration Or On Disable
    function ResetFileUpload(_uploadInputId) {
        // Document Upload Visibility Based On Selecting Parameter

        // Other Remaining Accordion Visibility
        // -db-ts = database toggle switch         -ls-ts = local storage toggle switch
        // -db-tf = database text fields           -ls-tf = local storage text fields
        // -ls-pt = Local Storage Path

        if (_uploadInputId === ALL) {
            eventObjId = ['document-upload', 'gold-photo-upload', 'vehicle-photo-upload', 'pre-owned-photo-upload'];
        }
        else {
            eventObjId = [];
            eventObjId.push(_uploadInputId);
        }

        len = eventObjId.length;

        for (let i = 0; i < len; i++) {
            if (_uploadInputId === 'document-upload') {
                $('.' + _uploadInputId + '-ls-ts').prop('disabled', false);
                $('.' + _uploadInputId + '-db-ts').prop('disabled', false);
            }
            else {
                $('.' + _uploadInputId + '-ls-ts').prop('checked', false);
                $('.' + _uploadInputId + '-db-ts').prop('checked', false);

                $('.' + _uploadInputId + '-ls-ts').prop('disabled', true);
                $('.' + _uploadInputId + '-db-ts').prop('disabled', true);
            }

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
            modalObjSelect2.trigger('change');
        }
    }

    //Round Interest If No Rounding Then Hide Interest
    function RoundingOfInterestChangeEventFunction() {
        if ($('.rounding-of-interest:checked').val() === NO_ROUNDING) {
            $('#interest-rounding-by-input').addClass('d-none');
            $('#interest-rounding-by').val(0);
        }
        else {
            $('#interest-rounding-by-input').removeClass('d-none');
        }
    }

    // Round Method If No Rounding Then Hide Nearest
    function RoundingOfPrincipalChangeEventFunction() {
        if ($('.rounding-of-principal:checked').val() === NO_ROUNDING) {
            $('#rounding-by-input').addClass('d-none');
            $('#principal-rounding-by').val(0);
        }
        else {
            $('#rounding-by-input').removeClass('d-none');
        }
    }

    // Set Document Dropdown List
    function SetDocumentDropdownList(_sysNameOfLoanType) {
        let documenId = $('#document-id option:selected').val();

        // Get Value For Only For Amend Operation
        let documentPageLoadId = $('#document-id option:selected').val();

        $.get('/DynamicDropdownList/GetDocumentDropdownListByLoanType', { _loanTypeSysName: _sysNameOfLoanType, async: false }, function (data) {
            debugger;

            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000">Select General Ledger</option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#document-id').html(dropdownListItems);

            documentDropdownList = $('#document-id').html();

            listItemCount = $('#document-id > option').not(':first').length;

            // Hide Document Accordion, If No Any Document Upload To Required
            if (listItemCount === 0) {
                $('#enable-document-upload-input').addClass('d-none');
            }
            else {
                if ($('#enable-document-upload-input').hasClass('disable-document-parameter') === false) {
                    $('#enable-document-upload-input').removeClass('d-none');
                }
            }

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#document-id').prop('selectedIndex', 1);
                $('#document-id').change();
            }
            else {
                if (isAmendView) {
                    $('#document-id').val(documentPageLoadId);
                }
            }
        });
    }

    // Set Genereral ledger Dropdown list By LoanType --Use GetGeneralLedgerDropdownListByAccountClassCode()
    function SetGeneraLedgerDropdownListByLoanType()
    {
        $.get('/DynamicDropdownList/GetGeneralLedgerDropdownListByAccountClassCode', { _accountClassCode: sysNameOfLoanType, async: false }, function (data)
        {
            debugger;
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select General Ledger --- </option>';

            $.each(data, function (index, selectListItemObj)
            {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#scheme-general-ledger-id').html(dropdownListItems);

            generalLedgerDropdownListItemsByLoanType = dropdownListItems;

            listItemCount = $('#scheme-general-ledger-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#scheme-general-ledger-id').prop('selectedIndex', 1);
                $('#scheme-general-ledger-id').change();
            }
        });
    }

    //Validation on By Laws Loan Schedule Parameter Entry
    function SetLoanTypeParameter() {
        debugger;
        // Loan Type Wise Show Hide Inputs
        $.get('/AccountChildAction/GetLoanTypeSysNameByLoanTypeId', { _loanTypeId: loanTypeId, async: false }, function (data, textStatus, jqXHR) {
            debugger;
            // Assign SysName To Global Variable
            sysNameOfLoanType = data;

            SetDocumentDropdownList(sysNameOfLoanType);

            SetGeneraLedgerDropdownListByLoanType();

            // Hide All Loan Type Dependent Input
            $('.optional-card').addClass('d-none');
            $('.optional-input').addClass('d-none');

            $('#shares-ratio-with-loan-input').addClass('d-none');

            // Cash Credit Loan
            if (sysNameOfLoanType === CASH_CREDIT_LOAN) {
                $('#cash-credit-loan-card').removeClass('d-none');
                $('#enable-cheque-book-input').removeClass('d-none');
                $('#enable-capture-cibil-score-input').removeClass('d-none');
                $('#enable-capture-debt-income-ratio-input').removeClass('d-none');
                $('#enable-existing-loan-detail-input').removeClass('d-none');
                $('#enable-swot-analysis-input').removeClass('d-none');
                $('#enable-past-credit-history-input').removeClass('d-none');
                $('#enable-legal-and-regulatory-compliance-input').removeClass('d-none');
                $('#enable-acquaintance-details-input').removeClass('d-none');

                $('#fine-on-missed-installment-input').addClass('d-none');
                $('#fine-days-input').removeClass('d-none');
                $('#person-asset-details').removeClass('d-none');

                // Passbook
                if ($('#enable-passbook-input').hasClass('disable-passbook-parameter') === false) {
                    $('#enable-passbook-input').removeClass('d-none');
                }

                // Document
                if ($('#enable-document-upload-input').hasClass('disable-document-parameter') === false) {
                    $('#enable-document-upload-input').removeClass('d-none');
                }

                // Funder
                if ($('#enable-funder').hasClass('disable-funder-parameter') === false) {
                    $('#enable-funder').removeClass('d-none');
                }
            }

            // Guarantor Loan    
            if (sysNameOfLoanType === GUARANTOR_LOAN) {
                $('#enable-standing-instruction-input').removeClass('d-none');
                $('#enable-field-investigation-input').removeClass('d-none');
                $('#enable-capture-cibil-score-input').removeClass('d-none');
                $('#enable-capture-debt-income-ratio-input').removeClass('d-none');
                $('#enable-post-dated-cheques-input').removeClass('d-none');
                $('#enable-payment-getway-input').removeClass('d-none');
                $('#enable-ecs-input').removeClass('d-none');
                $('#enable-settlement-account-input').removeClass('d-none');
                $('#enable-acquaintance-details-input').removeClass('d-none');

                $('#fine-on-missed-installment-input').removeClass('d-none');
                $('#fine-days-input').addClass('d-none');
                $('#person-asset-details').removeClass('d-none');

                // Passbook
                if ($('#enable-passbook-input').hasClass('disable-passbook-parameter') === false) {
                    $('#enable-passbook-input').removeClass('d-none');
                }

                // Pre Part Payment
                if ($('#enable-pre-part-payment-input').hasClass('disable-pre-part-payment') === false) {
                    $('#enable-pre-part-payment-input').removeClass('d-none');
                }

                // Pre Full Payment
                if ($('#enable-pre-full-payment-input').hasClass('disable-pre-full-payment') === false) {
                    $('#enable-pre-full-payment-input').removeClass('d-none');
                }

                // Interest Rebate
                if ($('#enable-rebate-interest-input').hasClass('disable-rebate-interest') === false) {
                    $('#enable-rebate-interest-input').removeClass('d-none');
                }

                // Document
                if ($('#enable-document-upload-input').hasClass('disable-document-parameter') === false) {
                    $('#enable-document-upload-input').removeClass('d-none');
                }

                $('#shares-ratio-with-loan-input').removeClass('d-none');
                $('#installment-repayment-schedule-accordions').removeClass('d-none');
            }

            // Vehicle Loan
            if (sysNameOfLoanType === VEHICLE_LOAN) {
                $('#vehicle-loan-card').removeClass('d-none');
                $('#pre-owned-vehicle-loan-card').removeClass('d-none');

                $('#enable-standing-instruction-input').removeClass('d-none');
                $('#enable-collateral-input').removeClass('d-none');
                $('#enable-field-investigation-input').removeClass('d-none');
                $('#enable-capture-cibil-score-input').removeClass('d-none');
                $('#enable-capture-debt-income-ratio-input').removeClass('d-none');
                $('#enable-valuation-input').removeClass('d-none');
                $('#enable-payment-getway-input').removeClass('d-none');
                $('#enable-ecs-input').removeClass('d-none');
                $('#enable-settlement-account-input').removeClass('d-none');
                $('#enable-acquaintance-details-input').removeClass('d-none');

                $('#fine-on-missed-installment-input').removeClass('d-none');
                $('#fine-days-input').addClass('d-none');
                $('#person-asset-details').removeClass('d-none');

                // Passbook
                if ($('#enable-passbook-input').hasClass('disable-passbook-parameter') === false) {
                    $('#enable-passbook-input').removeClass('d-none');
                }

                // Pre Part Payment
                if ($('#enable-pre-part-payment-input').hasClass('disable-pre-part-payment') === false) {
                    $('#enable-pre-part-payment-input').removeClass('d-none');
                }

                // Pre Full Payment / Fore Closure
                if ($('#enable-pre-full-payment-input').hasClass('disable-pre-full-payment') === false) {
                    $('#enable-pre-full-payment-input').removeClass('d-none');
                }

                // Interest Rebate
                if ($('#enable-rebate-interest-input').hasClass('disable-rebate-interest') === false) {
                    $('#enable-rebate-interest-input').removeClass('d-none');
                }

                // Document
                if ($('#enable-document-upload-input').hasClass('disable-document-parameter') === false) {
                    $('#enable-document-upload-input').removeClass('d-none');
                }

                $('#shares-ratio-with-loan-input').removeClass('d-none');
                $('#installment-repayment-schedule-accordions').removeClass('d-none');
            }

            // Consumer Durable Loan
            if (sysNameOfLoanType === CONSUMER_DURABLE_LOAN) {
                $('#consumer-durable-loan-card').removeClass('d-none');

                $('#enable-standing-instruction-input').removeClass('d-none');
                $('#enable-field-investigation-input').removeClass('d-none');
                $('#enable-capture-cibil-score-input').removeClass('d-none');
                $('#enable-capture-debt-income-ratio-input').removeClass('d-none');
                $('#enable-legal-solicitor-detail-input').removeClass('d-none');
                $('#enable-post-dated-cheques-input').removeClass('d-none');
                $('#enable-payment-getway-input').removeClass('d-none');
                $('#enable-ecs-input').removeClass('d-none');
                $('#enable-settlement-account-input').removeClass('d-none');
                $('#enable-acquaintance-details-input').removeClass('d-none');

                $('#fine-on-missed-installment-input').removeClass('d-none');
                $('#fine-days-input').addClass('d-none');
                $('#person-asset-details').removeClass('d-none');

                // Passbook
                if ($('#enable-passbook-input').hasClass('disable-passbook-parameter') === false) {
                    $('#enable-passbook-input').removeClass('d-none');
                }

                // Pre Part Payment
                if ($('#enable-pre-part-payment-input').hasClass('disable-pre-part-payment') === false) {
                    $('#enable-pre-part-payment-input').removeClass('d-none');
                }

                // Pre Full Payment / Fore Closure
                if ($('#enable-pre-full-payment-input').hasClass('disable-pre-full-payment') === false) {
                    $('#enable-pre-full-payment-input').removeClass('d-none');
                }

                // Interest Rebate
                if ($('#enable-rebate-interest-input').hasClass('disable-rebate-interest') === false) {
                    $('#enable-rebate-interest-input').removeClass('d-none');
                }

                // Document
                if ($('#enable-document-upload-input').hasClass('disable-document-parameter') === false) {
                    $('#enable-document-upload-input').removeClass('d-none');
                }

                $('#shares-ratio-with-loan-input').removeClass('d-none');
                $('#installment-repayment-schedule-accordions').removeClass('d-none');
            }

            // HOME
            if (sysNameOfLoanType === HOME_LOAN) {
                $('#home-loan-card').removeClass('d-none');
                $('#enable-standing-instruction-input').removeClass('d-none');
                $('#enable-collateral-input').removeClass('d-none');
                $('#enable-field-investigation-input').removeClass('d-none');
                $('#enable-capture-cibil-score-input').removeClass('d-none');
                $('#enable-capture-debt-income-ratio-input').removeClass('d-none');
                $('#enable-valuation-input').removeClass('d-none');
                $('#enable-payment-getway-input').removeClass('d-none');
                $('#enable-ecs-input').removeClass('d-none');
                $('#enable-settlement-account-input').removeClass('d-none');
                $('#enable-acquaintance-details-input').removeClass('d-none');

                $('#fine-on-missed-installment-input').removeClass('d-none');
                $('#fine-days-input').addClass('d-none');
                $('#person-asset-details').removeClass('d-none');

                // Passbook
                if ($('#enable-passbook-input').hasClass('disable-passbook-parameter') === false) {
                    $('#enable-passbook-input').removeClass('d-none');
                }

                // Pre Part Payment
                if ($('#enable-pre-part-payment-input').hasClass('disable-pre-part-payment') === false) {
                    $('#enable-pre-part-payment-input').removeClass('d-none');
                }

                // Pre Full Payment / Fore Closure
                if ($('#enable-pre-full-payment-input').hasClass('disable-pre-full-payment') === false) {
                    $('#enable-pre-full-payment-input').removeClass('d-none');
                }

                // Interest Rebate
                if ($('#enable-rebate-interest-input').hasClass('disable-rebate-interest') === false) {
                    $('#enable-rebate-interest-input').removeClass('d-none');
                }

                // Document
                if ($('#enable-document-upload-input').hasClass('disable-document-parameter') === false) {
                    $('#enable-document-upload-input').removeClass('d-none');
                }

                $('#shares-ratio-with-loan-input').removeClass('d-none');
                $('#installment-repayment-schedule-accordions').removeClass('d-none');
            }

            // Loan Against Property
            if (sysNameOfLoanType === LOAN_AGAINST_PROPERTY) {
                $('#loan-against-property-card').removeClass('d-none');

                $('#enable-standing-instruction-input').removeClass('d-none');
                $('#enable-collateral-input').removeClass('d-none');
                $('#enable-field-investigation-input').removeClass('d-none');
                $('#enable-capture-cibil-score-input').removeClass('d-none');
                $('#enable-capture-debt-income-ratio-input').removeClass('d-none');
                $('#enable-valuation-input').removeClass('d-none');
                $('#enable-payment-getway-input').removeClass('d-none');
                $('#enable-ecs-input').removeClass('d-none');
                $('#enable-settlement-account-input').removeClass('d-none');
                $('#enable-acquaintance-details-input').removeClass('d-none');

                $('#fine-on-missed-installment-input').removeClass('d-none');
                $('#fine-days-input').addClass('d-none');
                $('#person-asset-details').removeClass('d-none');

                // Passbook
                if ($('#enable-passbook-input').hasClass('disable-passbook-parameter') === false) {
                    $('#enable-passbook-input').removeClass('d-none');
                }

                // Pre Part Payment
                if ($('#enable-pre-part-payment-input').hasClass('disable-pre-part-payment') === false) {
                    $('#enable-pre-part-payment-input').removeClass('d-none');
                }

                // Pre Full Payment / Fore Closure
                if ($('#enable-pre-full-payment-input').hasClass('disable-pre-full-payment') === false) {
                    $('#enable-pre-full-payment-input').removeClass('d-none');
                }

                // Interest Rebate
                if ($('#enable-rebate-interest-input').hasClass('disable-rebate-interest') === false) {
                    $('#enable-rebate-interest-input').removeClass('d-none');
                }

                // Document
                if ($('#enable-document-upload-input').hasClass('disable-document-parameter') === false) {
                    $('#enable-document-upload-input').removeClass('d-none');
                }

                $('#shares-ratio-with-loan-input').removeClass('d-none');
                $('#installment-repayment-schedule-accordions').removeClass('d-none');
            }

            // Business Loan
            if (sysNameOfLoanType === BUSINESS_LOAN) {
                $('#business-loan-card').removeClass('d-none');

                $('#enable-standing-instruction-input').removeClass('d-none');
                $('#enable-field-investigation-input').removeClass('d-none');
                $('#enable-capture-cibil-score-input').removeClass('d-none');
                $('#enable-capture-debt-income-ratio-input').removeClass('d-none');
                $('#enable-legal-solicitor-detail-input').removeClass('d-none');
                $('#enable-post-dated-cheques-input').removeClass('d-none');
                $('#enable-payment-getway-input').removeClass('d-none');
                $('#enable-ecs-input').removeClass('d-none');
                $('#enable-settlement-account-input').removeClass('d-none');
                $('#enable-acquaintance-details-input').removeClass('d-none');

                $('#fine-on-missed-installment-input').removeClass('d-none');
                $('#fine-days-input').addClass('d-none');
                $('#person-asset-details').removeClass('d-none');

                // Passbook
                if ($('#enable-passbook-input').hasClass('disable-passbook-parameter') === false) {
                    $('#enable-passbook-input').removeClass('d-none');
                }

                // Pre Part Payment
                if ($('#enable-pre-part-payment-input').hasClass('disable-pre-part-payment') === false) {
                    $('#enable-pre-part-payment-input').removeClass('d-none');
                }

                // Pre Full Payment / Fore Closure
                if ($('#enable-pre-full-payment-input').hasClass('disable-pre-full-payment') === false) {
                    $('#enable-pre-full-payment-input').removeClass('d-none');
                }

                // Interest Rebate
                if ($('#enable-rebate-interest-input').hasClass('disable-rebate-interest') === false) {
                    $('#enable-rebate-interest-input').removeClass('d-none');
                }

                // Document
                if ($('#enable-document-upload-input').hasClass('disable-document-parameter') === false) {
                    $('#enable-document-upload-input').removeClass('d-none');
                }

                $('#shares-ratio-with-loan-input').removeClass('d-none');
                $('#installment-repayment-schedule-accordions').removeClass('d-none');
            }

            // Gold Loan
            if (sysNameOfLoanType === GOLD_LOAN) {
                $('#gold-loan-card').removeClass('d-none');

                $('#enable-standing-instruction-input').removeClass('d-none');
                $('#enable-valuation-input').removeClass('d-none');
                $('#enable-payment-getway-input').removeClass('d-none');
                $('#enable-ecs-input').removeClass('d-none');
                $('#enable-loan-installment-input').removeClass('d-none');
                $('#enable-distributor-input').removeClass('d-none');

                $('#fine-on-missed-installment-input').addClass('d-none');
                $('#fine-days-input').removeClass('d-none');

                // Pre Part Payment
                if ($('#enable-pre-part-payment-input').hasClass('disable-pre-part-payment') === false) {
                    $('#enable-pre-part-payment-input').removeClass('d-none');
                }

                // Pre Full Payment / Fore Closure
                if ($('#enable-pre-full-payment-input').hasClass('disable-pre-full-payment') === false) {
                    $('#enable-pre-full-payment-input').removeClass('d-none');
                }

                // Interest Rebate
                if ($('#enable-rebate-interest-input').hasClass('disable-rebate-interest') === false) {
                    $('#enable-rebate-interest-input').removeClass('d-none');
                }

                // Document
                if ($('#enable-document-upload-input').hasClass('disable-document-parameter') === false) {
                    $('#enable-document-upload-input').removeClass('d-none');
                }

                // Distributor
                if ($('#enable-distributor').hasClass('disable-distributor-parameter') === false) {
                    $('#enable-distributor').removeClass('d-none');
                }
            }

            // Loan Against Fixed Deposit
            if (sysNameOfLoanType === LOAN_AGAINST_FIXED_DEPOSITE) {
                $('#loan-against-deposit-card').removeClass('d-none');

                $('#enable-standing-instruction-input').removeClass('d-none');
                $('#enable-payment-getway-input').removeClass('d-none');
                $('#enable-ecs-input').removeClass('d-none');
                $('#enable-loan-installment-input').removeClass('d-none');

                $('#fine-on-missed-installment-input').addClass('d-none');
                $('#fine-days-input').removeClass('d-none');

                // Pre Part Payment
                if ($('#enable-pre-part-payment-input').hasClass('disable-pre-part-payment') === false) {
                    $('#enable-pre-part-payment-input').removeClass('d-none');
                }

                // Pre Full Payment / Fore Closure
                if ($('#enable-pre-full-payment-input').hasClass('disable-pre-full-payment') === false) {
                    $('#enable-pre-full-payment-input').removeClass('d-none');
                }

                // Interest Rebate
                if ($('#enable-rebate-interest-input').hasClass('disable-rebate-interest') === false) {
                    $('#enable-rebate-interest-input').removeClass('d-none');
                }
            }

            //Education Loan
            if (sysNameOfLoanType === EDUCATION_LOAN) {
                $('#education-card').removeClass('d-none');

                $('#minimum-age').attr('min', 18).attr('max', 35);
                $('#maximum-age').attr('min', 18).attr('max', 35);

                $('#enable-standing-instruction-input').removeClass('d-none');
                $('#enable-payment-getway-input').removeClass('d-none');
                $('#enable-ecs-input').removeClass('d-none');
                $('#installment-repayment-schedule-accordions').removeClass('d-none');

                $('#fine-on-missed-installment-input').removeClass('d-none');
                $('#fine-days-input').addClass('d-none');

                // Pre Part Payment
                if ($('#enable-pre-part-payment-input').hasClass('disable-pre-part-payment') === false) {
                    $('#enable-pre-part-payment-input').removeClass('d-none');
                }

                // Pre Full Payment / Fore Closure
                if ($('#enable-pre-full-payment-input').hasClass('disable-pre-full-payment') === false) {
                    $('#enable-pre-full-payment-input').removeClass('d-none');
                }

                // Interest Rebate
                if ($('#enable-rebate-interest-input').hasClass('disable-rebate-interest') === false) {
                    $('#enable-rebate-interest-input').removeClass('d-none');
                }
            }

            // ByLaws Loan Schedule Parameter
            $.get('/AccountChildAction/GetByLawsLoanScheduleParameterEntry', { _loanTypeId: loanTypeId, async: false }, function (byLawsLoanScheduleParameter, textStatus, jqXHR) {
                debugger;
                // Crete Key Value Pair For All Enable Inputs
                const ENABLE_INPUT =
                {
                    EnablePassbook: '#enable-passbook-input',
                    EnableValuation: '#enable-valuation-input',
                    EnableApplication: '#enable-application-input',
                    EnableInterestCapitalization: '#enable-capitalization-input'
                };

                // Iterate All Key And Visible Or Hide As Per Configuration
                for (let key in ENABLE_INPUT) {
                    // The hasOwnProperty() method of Object instances returns a boolean indicating whether 
                    // this object has the specified property as its own property (as opposed to inheriting it).
                    if (ENABLE_INPUT.hasOwnProperty(key)) {
                        const selector = ENABLE_INPUT[key];
                        const isVisible = byLawsLoanScheduleParameter[key];

                        // Check Visible / True 
                        if (isVisible) {
                            $(selector).removeClass('d-none');
                        }
                        else {
                            $(selector).addClass('d-none');
                        }
                    }
                }

                // Crete Key Value Pair For All Minimum Input 
                // Sequence Of Minimum And Maximum Must Be Same
                const MIN_INPUT =
                {
                    MinimumTenure: '#minimum-tenure',
                    //MinimumSanctionLoanAmountLimit: '#',
                    MinimumLoanAmountLimitForIndividual: '#minimum-loan-amount-for-individual',
                    MinimumLoanAmountLimitForGroup: '#minimum-loan-amount-for-group',
                    MinimumInterestRate: '#minimum-interest-rate',
                    MinimumFineInterestRate: '#rate-of-fine-interest',
                    MinimumNumberOfGuarantor: '#minimum-number-of-guarantors',

                    MinimumSanctioningLimitForBranchManager: '#minimum-loan-sanction-amount-manager',
                    MinimumSanctioningLimitForCommittee: '#minimum-loan-sanction-amount-committee',
                    MinimumSanctioningLimitForBoardOfDirector: '#minimum-loan-sanction-amount-bod',
                    MinimumSanctioningLimitForCEO: '#minimum-loan-sanction-amount-ceo',
                    MinimumSanctioningLimitForChairman: '#minimum-loan-sanction-amount-chairman',
                }

                // Crete Key Value Pair For All Maximum Input 
                // Sequence Of Minimum And Maximum Must Be Same
                const MAX_INPUT =
                {
                    MaximumTenure: '#maximum-tenure',
                    //MaximumSanctionLoanAmountLimit: '#',
                    MaximumLoanAmountLimitForIndividual: '#maximum-loan-amount-for-individual',
                    MaximumLoanAmountLimitForGroup: '#maximum-loan-amount-for-group',
                    MaximumInterestRate: '#maximum-interest-rate',
                    MaximumFineInterestRate: '#rate-of-fine-interest',
                    MaximumNumberOfGuarantor: '#maximum-number-of-guarantors',

                    MaximumSanctioningLimitForBranchManager: '#maximum-loan-sanction-amount-manager',
                    MaximumSanctioningLimitForCommittee: '#maximum-loan-sanction-amount-committee',
                    MaximumSanctioningLimitForBoardOfDirector: '#maximum-loan-sanction-amount-bod',
                    MaximumSanctioningLimitForCEO: '#maximum-loan-sanction-amount-ceo',
                    MaximumSanctioningLimitForChairman: '#maximum-loan-sanction-amount-chairman',
                }

                // Iterate All Keyas And Set Minimum Values As Per Configuration
                let minKeys = Object.keys(MIN_INPUT);
                let maxKeys = Object.keys(MAX_INPUT);

                // Applicable Only If Both Length Same
                if (minKeys.length == maxKeys.length) {
                    for (let i = 0; i < minKeys.length; i++) {
                        // The hasOwnProperty() method of Object instances returns a boolean indicating whether 
                        // this object has the specified property as its own property (as opposed to inheriting it).
                        if (MIN_INPUT.hasOwnProperty(minKeys[i])) {
                            let minSelector = MIN_INPUT[minKeys[i]];
                            const maxSelector = MAX_INPUT[maxKeys[i]];

                            // Set Minimum To Minimum
                            $(minSelector).attr('min', byLawsLoanScheduleParameter[minKeys[i]]);
                            // Set Maximum To Minimum
                            $(minSelector).attr('max', byLawsLoanScheduleParameter[maxKeys[i]]);

                            // Set Maximum To Maximum
                            $(maxSelector).attr('max', byLawsLoanScheduleParameter[maxKeys[i]]);
                        }
                    }
                }

                // Configure Max For Collateral Security Percentage Of Loan
                $('#required-collateral-security-percentage-of-loan').attr({ 'max': 99 });

                // Set attributes on Based on Condition
                //SetDynamicValuesByLawsLoanSchedule(byLawsLoanScheduleParameter);

                // Fine Interest
                byLawsMinimumFineInterest = parseFloat($('#rate-of-fine-interest').attr('min'));
                byLawsMaximumFineInterest = parseFloat($('#rate-of-fine-interest').attr('max'));

                // Guarantor Visibility
                if (parseInt(byLawsLoanScheduleParameter.MaximumNumberOfGuarantor) > 0) {
                    $('#enable-guarantor-detail-input').removeClass('d-none');
                    $('#enable-guarantor-detail').prop('checked', true);
                    $('#enable-guarantor-detail-input').addClass('read-only');
                }
                else {
                    $('#enable-guarantor-detail-input').addClass('d-none');
                    $('#enable-guarantor-detail').prop('checked', false);
                }

                TimePeriodUnitFocusOutEventFunction();

                // Call Following Methods For Only Amend View
                if (isAmendView || isVerifyView) {
                    NumberOfAccountUnitChangeEventFunction();
                    TurnOverUnitChangeEventFunction();
                    RateOfFineInterestUnitChangeEventFunction();

                    MaximumLoanSanctionAmountManagerFocusOutEventFunction();

                    MaximumLoanSanctionAmountCommitteeFocusOutEventFunction();

                    // Set Document Uplod Values As Per Db Of All Upload Controller
                    SetDocumentUploadInput(ALL);

                    // Validation on FineInterestParameter
                    FineInterestParameter();

                    // Loan Installment Change Event Function
                    LoanInstallmentChangeEventFunction();

                    // Reset To Work Normal Page Validations
                    //isAmendView = false;
                    // isVerifyView = false;
                }

                // Hide All Accordion Or Div Blocks Based On Toggle Switch
                SetToggleSwitchBasedAccordions();
            });
        });
    }

    //Target Group Data Table
    $('.gender').addClass('d-none');

    $('.occupation').addClass('d-none');

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#enable-applicable-all-universities').change(function ()
    {
        instituteDataTable.clear().draw();
        $('#applicable-all-universities-block').removeClass('d-none');
    });

    $('#enable-applicable-all-course').change(function ()
    {
        educationalCourseDataTable.clear().draw();
        $('#applicable-all-course-block').removeClass('d-none');
    });

    //Validation On Focusout in Tenure
    $('#time-period-unit-id').change(function () {
        debugger;
        if (isVerifyView === false) {
            $('#minimum-tenure').val('');
            $('#maximum-tenure').val('');
            $('#tenure-multiple-of').val('');
            $('#default-tenure').val('');
        }

        TimePeriodUnitFocusOutEventFunction();
    });

    // Enable Document Upload 
    $('#enable-document-upload').change(function () {
        documentDataTable.clear().draw();
    });

    // Enable Email, SMS Service 
    $('#enable-email-service, #enable-sms-service').change(function () {
        noticeScheduleDataTable.clear().draw();
    });

    // Enable Target Group
    $('#enable-target-group').change(function () {
        targetGroupDataTable.clear().draw();
        $('#target-group-data-table-error').addClass('d-none');
    });

    // Enable Tenure 
    $('#enable-tenure').change(function () {
        debugger;
        // Check if the checkbox is checked
        let isChecked = $(this).is(':checked');

        if (isChecked) {
            // Hide fields when enable-tenure is true
            $('#tenure-list-block').addClass('d-none');
            $('.tenure-list').addClass('d-none');
            $('.tenure-list').addClass('d-none');

            // Hide Error Msg And Clear Data Table
            $('#tenure-required-error').addClass('d-none');
            tenureListDataTable.clear().draw();
        }
        else {
            // Show fields when enable-tenure is false and enable tenure parameter list
            if ($('#enable-tenure-list-input').hasClass('disable-tenure-list-parameter') === false) {
                $('.tenure-list').removeClass('d-none');
                $('#tenure-list-block').addClass('d-none');
                $('#enable-tenure-list').prop('checked', false);
                $('.tenure-parameter-input').val('');
            }
        }
    });

    // Enable Tenure List
    $('#enable-tenure-list').change(function () {
        debugger;
        // Check if the checkbox is checked
        let isChecked = $(this).is(':checked');

        if (isChecked) {
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

    })

    function MaximumLoanSanctionAmountManagerFocusOutEventFunction() {
        let myMaxValue = parseFloat($('#maximum-loan-sanction-amount-manager').attr('max'));

        if (parseFloat($('#maximum-loan-sanction-amount-manager').val()) > parseFloat(myMaxValue)) {
            $('#maximum-loan-sanction-amount-manager').val(myMaxValue);
        }

        $('#minimum-loan-sanction-amount-committee').attr('min', parseFloat(myMaxValue) + 1);
    }

    // Maximum Loan Sanction Amount Manager
    $('#maximum-loan-sanction-amount-manager').focusout(function () {
        MaximumLoanSanctionAmountManagerFocusOutEventFunction();
    });

    function MaximumLoanSanctionAmountCommitteeFocusOutEventFunction() {
        let myMaxValue = parseFloat($('#maximum-loan-sanction-amount-committee').attr('max'));

        if (parseFloat($('#maximum-loan-sanction-amount-committee').val()) > parseFloat(myMaxValue)) {
            $('#maximum-loan-sanction-amount-committee').val(myMaxValue);
        }

        $('#minimum-loan-sanction-amount-bod').attr('min', parseFloat(myMaxValue) + 1);
    }

    // Maximum Loan Sanction Amount Comittee
    $('#maximum-loan-sanction-amount-committee').focusout(function () {
        MaximumLoanSanctionAmountCommitteeFocusOutEventFunction();
    });

    // Call Loan Type SetSchemeDropdownList Function
    $('#loan-type-id').focusout(function (event) {
        debugger;
        if (isVerifyView === false && isAmendView === false) {
            debugger;
            loanTypeId = $('#loan-type-id option:selected').val();

            // Change Setting If Loan Type Changed
            if (prevloanTypeId !== loanTypeId) {
                // Clear info if previous loan type was selected
                if (prevloanTypeId !== '0')
                    $('#loan-type-id-info').removeClass('d-none');

                // Set ByLawsLoanSanction And Loan Sanction Authority Parameter
                SetLoanTypeParameter();

                // Mark Out Select All Check Box Of All Datatables.
                $('input[name="select-all"]').prop('checked', false);

                // Clear Accordion Title Error Messages
                $('.accordion-title-error').addClass('d-none');

                // Make False All Toggle Switch
                $('.switch-input').prop('checked', false);

                // Hide All Accordion Based On Toggle Switch s
                //SetToggleSwitchBasedAccordions();

                //Clear all number fiels and textarea
                $('input[type="number"]').val('');

                //Clear all text fields except name of Scheme And their Translation Field
                $('input[type="text"]').not('#name-of-scheme, #trans-name-of-scheme, #alias-name, #trans-alias-name, #name-on-report, #trans-name-on-report').val('');

                //Clear all TextArea Except Note and their Translation Field
                $('textarea').val('');

                // Clear dropdowns and MultiSelect to the first option except Loan Type
                $('select').not('#loan-type-id').prop('selectedIndex', 0);

                //Clear Radio Input
                $('input[type="radio"]').prop('checked', false);

                // Show Tenure Inputs
                $('#enable-tenure-input').removeClass('d-none');

                if ($('#enable-tenure-list-input').hasClass('disable-tenure-list-parameter') === false) {
                    $('#enable-tenure-list-input').removeClass('d-none');
                }

                // Set Default Value None Of Empty Input
                $('.default-none').each(function () {
                    if ($(this).val().trim().length === 0)
                        $(this).val('None');
                })

                // Clear All Data Tables
                tenureListDataTable.clear().draw();
                documentDataTable.clear().draw();
                noticeScheduleDataTable.clear().draw();
                reportFormatDataTable.clear().draw();
                generalLedgerDataTable.clear().draw();
                businessOfficeDataTable.clear().draw();
                targetGroupDataTable.clear().draw();
                loanChargesDataTable.clear().draw();
                loanOverduesActionDataTable.clear().draw();
                vehicleTypeLoanDataTable.clear().draw();
                consumerDurableLoanDataTable.clear().draw();
                instituteDataTable.clear().draw();
                educationalCourseDataTable.clear().draw();

                // Hide Blocks
                $('#application-number-branchwise-block').removeClass('d-none');
                $('#account-number-branchwise-block').removeClass('d-none');
                $('#passbook-number-branchwise-block').removeClass('d-none');

                prevloanTypeId = loanTypeId;
            }
            else {
                $('#loan-type-id-info').addClass('d-none');
                prevloanTypeId = loanTypeId;
            }
        }
    });

    // Maximum Loan Sanction Percentage 1
    $('#maximum-loan-sanction-percentage-1').focusout(function () {
        debugger;
        $('#maximum-loan-sanction-percentage-2').attr('max', parseFloat($(this).val()));
    });

    // Minimum Business Experience
    $('#current-business-minimum-age').focusout(function () {
        debugger;
        $('#minimum-business-experience').attr('min', parseInt($(this).val()));
    });

    // Maximum Loan Sanction Percentage 2
    $('#maximum-loan-sanction-percentage-2').focusout(function () {
        debugger;
        $('#maximum-loan-sanction-percentage-3').attr('max', parseFloat($(this).val()));
    });

    // Maximum Loan Sanction Percentage 3
    $('#maximum-loan-sanction-percentage-3').focusout(function () {
        debugger;
        $('#maximum-loan-sanction-percentage-4').attr('max', parseFloat($(this).val()));
    });

    // Maximum Tenure 1
    $('#maximum-tenure-1').focusout(function () {
        debugger;
        $('#maximum-tenure-2').attr('max', parseFloat($(this).val()));
    });

    // Maximum Tenure 2
    $('#maximum-tenure-2').focusout(function () {
        debugger;
        $('#maximum-tenure-3').attr('max', parseFloat($(this).val()));
    });

    // Maximum Tenure 3
    $('#maximum-tenure-3').focusout(function () {
        debugger;
        $('#maximum-tenure-4').attr('max', parseFloat($(this).val()));
    });

    // Education Loan Mininum Fees Focus Out
    $('#minimum-fees').focusout(function () {
        $('#maximum-fees').attr('min', parseFloat($(this).val()));
    });

    // Minimum Addition Interest Rate
    $('#minimum-additional-interest-rate').focusout(function () {
        $('#minimum-interest-rate-third-person-deposit').attr('min', $(this).val());
    });

    // Super Valuation Held In Year 
    $('#gold-super-valuations-year').focusout(function () {
        debugger;
        let maximumTimePeriodBetweenTwoSuperValuations = Math.round(12 / parseInt($(this).val()))

        $('#gold-time-period-between-two-super-valuations').attr('max', maximumTimePeriodBetweenTwoSuperValuations);

        $('#gold-time-period-between-two-super-valuations').val(maximumTimePeriodBetweenTwoSuperValuations);
    });

    // Validate Unique Name of Scheme
    $('#name-of-scheme').focusout(function (event) {
        let nameOfScheme = $('#name-of-scheme').val();

        $.get('/AccountChildAction/IsUniqueLoanSchemeName', { _nameOfScheme: nameOfScheme, async: false }, function (data, textStatus, jqXHR) {
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

    // Vehicle Life 1
    $('#vehicle-life-1').focusout(function () {
        debugger;
        $('#vehicle-life-2').attr('min', parseInt($(this).val()) + 1);
    });

    // Vehicle Life 2
    $('#vehicle-life-2').focusout(function () {
        debugger;
        $('#vehicle-life-3').attr('min', parseInt($(this).val()) + 1);
    });

    // Vehicle Life 3
    $('#vehicle-life-3').focusout(function () {
        debugger;
        $('#vehicle-life-4').attr('min', parseInt($(this).val()) + 1);
    });

    // @@@@@@@@@@@@@@@@@@@@@@@@@@  FOCUSOUT FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Account Parameter Input Validation
    $('.account-parameter-input').focusout(function () {
        IsValidAccountParameterAccordionInputs();
    });

    // Application Number Parameter Input Validation
    $('.application-number-input').focusout(function () {
        IsValidApplicationNumberAccordionInputs();
    });

    // Account Number Parameter Input Validation
    $('.account-number-input').focusout(function () {
        IsValidCustomerAccountNumberAccordionInputs();
    });

    // Agreement Number Parameter Input Validation
    $('.agreement-number-input').focusout(function () {
        IsValidAgreemetNumberAccordionInputs();
    });

    // Loan Cash Credit Input Validation
    $('.cash-credit-loan-input').focusout(function () {
        IsValidCashCreditLoanAccordianInputs();
    });

    // Cash Credit Loan Change Event For Radio Button 
    $('.cash-credit-loan-radio-input').change(function () {
        IsValidCashCreditLoanAccordianInputs();
    });

    // Change Event Of Radio Button Home Loan Accordion Input Validation On Radio Button
    $('.collateral-insurance-radio-input').change(function () {
        IsValidHomeLoanAccordionInputs();
    });

    // Loan Distributor Input Validation
    $('.distributor-input').focusout(function () {
        IsValidLoanDistributorAccordionInputs();
    });

    // Limit Parameter Accordion Input Validation
    $('.gold-loan-input').focusout(function () {
        IsValidGoldLoanAccordionInputs();
    });

    // Limit Parameter Accordion Input Validation
    $('.gold-loan-input').change(function () {
        IsValidGoldLoanAccordionInputs();
    });

    // Change Event Of Radio Button Gold Loan Accordion Input Validation On Radio Button
    $('.gold-insurance-radio-input').change(function () {
        IsValidGoldLoanAccordionInputs();
    });

    // Home Loan Input Validation
    $('.home-loan-input').focusout(function () {
        IsValidHomeLoanAccordionInputs();
    });

    // Loan Interest  Input Validation
    $('.interest-input').focusout(function () {
        IsValidLoanInterestAccordionInputs();
    });

    // Payment Reminder Parameter Input Validation
    $('.loan-against-deposit-input').focusout(function () {
        IsValidLoanAgainstDepositAccordionInputs();
    });

    // Business loan Input Validation
    $('.business-loan-input').focusout(function () {
        IsValidBusinessLoanAccordionInputs();
    });

    // Loan Against Property Input Validation
    $('.loan-against-property-input').focusout(function () {
        IsValidLoanAgainstPropertyAccordionInputs();
    });

    // Change Event Of Radio Button Loan Against Property Accordion Input Validation On Radio Button
    $('.property-insurance-radio-input').focusout(function () {
        IsValidLoanAgainstPropertyAccordionInputs();
    });

    // Loan Arrear Accordion Input Validation
    $('.loan-arrear-input').focusout(function () {
        IsValidLoanArrearAccordionInputs();
    });

    // Change Event Of Radio Button Loan Arrear Accordion Input Validation On Radio Button
    $('.loan-arrear-radio-input').change(function () {
        IsValidLoanArrearAccordionInputs();
    });

    // Loan Funder Accordion Input Validation
    $('.loan-funder-input').focusout(function () {
        IsValidLoanFunderAccordionInputs();
    });

    // Loan Funder Accordion Input Validation
    $('.loan-installment-input').focusout(function () {
        IsValidLoanInstallmentAccordionInputs();
    });

    // Change Event Of Radio Button Loan Interest  Input Validation
    $('.loan-interest-radio-input').change(function () {
        IsValidLoanInterestAccordionInputs();
    });

    // Loan Interest Rebate Accordion Input Validation
    $('.loan-interest-rebate-input').focusout(function () {
        IsValidLoanInterestRebateAccordionInputs();
    });

    // Loan Interest Rebate Accordion Input Validation
    $('.loan-interest-rebate-input').change(function () {
        IsValidLoanInterestRebateAccordionInputs();
    });

    // Loan Repayment Validation On Input Focusout 
    $('.loan-repayment-input').focusout(function () {
        IsValidLoanRepaymentAccordionInputs();
    });

    // Loan Repayment Validation On Radio Button
    $('.loan-repayment-radio-input').change(function () {
        IsValidLoanRepaymentAccordionInputs();
    });

    // Loan Sanction Authority Accordion Input Validation
    $('.loan-sanction-authority-input').focusout(function () {
        IsValidSanctionAuthorityAccordionInputs();
    });

    // Loan Arrear Accordion Input Validation
    $('.loan-settlement-input').focusout(function () {
        IsValidSettlementAccountAccordionInputs();
    });

    // Number Of Account For Real Number
    $('#number-of-account').focusout(function () {
        if ($('.number-of-account-unit:checked').val() === NUMBER_UNIT) {
            $(this).val(Math.floor($(this).val()));
        }
    })

    // Target Estimation Accordion Input Validation
    $('.number-of-account-unit').change(function () {
        NumberOfAccountUnitChangeEventFunction();
    });

    // Enable Passbook Detail Input Validation
    $('.passbook-detail-input').focusout(function () {
        IsValidPassbookDetailAccordionInputs();
    });

    // Payment Reminder Parameter Input Validation
    $('.payment-reminder-input').focusout(function () {
        IsValidPaymentReminderAccordionInputs();
    });

    // Pre Full Payment Accordion Input Validation
    $('.pre-full-payment-input').focusout(function () {
        IsValidPreFullPaymentAccordionInputs();
    });

    // Pre Part Payment Accordion Input Validation
    $('.pre-part-payment-input').focusout(function () {
        IsValidPrePartPaymentAccordionInputs();
    });

    // Change Event Of Radio Button Loan Against Property Accordion Input Validation On Radio Button
    $('.property-insurance-radio-input').focusout(function () {
        IsValidLoanAgainstPropertyAccordionInputs();
    });

    // Rate Of Fine Interest Input Validation
    $('.rate-of-fine-interest-unit').change(function () {
        RateOfFineInterestUnitChangeEventFunction();
    });

    // Target Estimation Accordion Input Validation
    $('.target-estimation-input').focusout(function () {
        IsValidTargetEstimationAccordionInputs();
    });

    // Enable Tenure Accordion Input Validation
    $('.tenure-parameter-input').focusout(function () {
        IsValidTenureAccordionInputs();
    });

    // Lending Charges Dropdown 
    $('#lending-charges-base-id').focusout(function () {
        debugger;
        lendingChargesBaseIdText = $('#lending-charges-base-id option:selected').text();
        if (lendingChargesBaseIdText.includes('%') === true) {
            $('#charges-percentage-input').removeClass('d-none')
        }
        else {
            $('#charges-percentage-input').addClass('d-none')
            $('#charges-percentage').val(0);
        }
    });

    // Turn Over Amount
    $('.turn-over-unit').change(function () {
        TurnOverUnitChangeEventFunction();
    });

    // EducationLoan
    $('.education-loan-input').focusout(function () {
        debugger;
        IsValidEducationLoanAccordionInputs();
    });
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

    function RateOfFineInterestUnitChangeEventFunction() {
        if ($('.rate-of-fine-interest-unit:checked').val() === FLAT_AMOUNT) {
            $('#rate-of-fine-interest').attr('max', 99999);

            if ($('#rate-of-fine-interest').val() > 99999)
                $('#rate-of-fine-interest').val(99999);
        }
        else {
            $('#rate-of-fine-interest').attr('min', byLawsMinimumFineInterest);
            $('#rate-of-fine-interest').attr('max', byLawsMaximumFineInterest);

            if (parseFloat($('#rate-of-fine-interest').val()) > parseFloat(byLawsMinimumFineInterest))
                $('#rate-of-fine-interest').val(byLawsMaximumFineInterest);
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

    function TimePeriodUnitFocusOutEventFunction() {
        let timePeriodUnitId = $('#time-period-unit-id option:selected').val();

        // Fetch the time period unit name by its ID
        $.get('/AccountChildAction/GetTimePeriodUnitSysNameById', { _timePeriodUnitId: timePeriodUnitId, async: false }, function (sysTimePeriodUnit, textStatus, jqXHR) {
            debugger;
            if (sysTimePeriodUnit === 'Year') {
                // For Cash Credit Loan
                $('#minimum-tenure').attr('min', 1).attr('max', 99);
                $('#maximum-tenure').attr('max', 99);
            }
            else if (sysTimePeriodUnit == 'Month') {
                $('#minimum-tenure').attr('min', 1).attr('max', 12);
                $('#maximum-tenure').attr('max', 12);
            }
            else if (sysTimePeriodUnit == 'Day') {
                $('#minimum-tenure').attr('min', 1).attr('max', 31);
                $('#maximum-tenure').attr('max', 31);
            }
        })
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   FUNCTIONS FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // 1. Agreemet Number Accordion Input Validation
    function IsValidAgreemetNumberAccordionInputs() {
        debugger;
        multiSelectCount = 0;

        result = true;

        if ($('#enable-auto-agreement-number').is(':checked')) {
            debugger;
            let startAgreementNumber = parseInt($('#start-agreement-number').val());
            let endAgreementNumber = parseInt($('#end-agreement-number').val());
            let agreementNumberIncrementBy = parseInt($('#agreement-number-increment-by').val());

            multiSelectCount = parseInt($('#agreement-number-mask option:selected').length);

            // Agreement Number Mask
            if (multiSelectCount === 0)
                result = false;

            // Start Agreement Number
            if (isNaN(startAgreementNumber) === false) {
                minimum = parseInt($('#start-agreement-number').attr('min'));
                maximum = parseInt($('#start-agreement-number').attr('max'));

                if (parseInt(startAgreementNumber) < parseInt(minimum) || parseInt(startAgreementNumber) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // End Agreement Number
            if (isNaN(endAgreementNumber) === false) {
                minimum = parseInt($('#end-agreement-number').attr('min'));
                maximum = parseInt($('#end-agreement-number').attr('max'));

                if (parseInt(endAgreementNumber) < (parseInt(startAgreementNumber) + 100) || parseInt(endAgreementNumber) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Aggrement Number Increment By
            if (isNaN(agreementNumberIncrementBy) === false) {
                if (parseInt(agreementNumberIncrementBy) === 0 || parseInt(agreementNumberIncrementBy) > parseInt(((parseInt(endAgreementNumber) - parseInt(startAgreementNumber)) / 100)))
                    result = false;
            }
            else
                result = false;
        }

        if (result)
            $('#agreement-number-accordion-title-error').addClass('d-none');
        else
            $('#agreement-number-accordion-title-error').removeClass('d-none');

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
            $('#application-number-accordion-title-error').addClass('d-none');
        else
            $('#application-number-accordion-title-error').removeClass('d-none');

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
            $('#account-number-accordion-title-error').addClass('d-none');
        else
            $('#account-number-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 4. Account Parameter Accordion Input Validation
    function IsValidAccountParameterAccordionInputs() {
        debugger;

        let timePeriodForNewCustomerFlag = parseInt($('#time-period-for-new-customer-flag').val());
        let sharesRatioWithLoan = parseFloat($('#shares-ratio-with-loan').val());

        // Loan Amount Group
        let minimumLoanAmountForGroup = parseFloat($('#minimum-loan-amount-for-group').val());
        let maximumLoanAmountForGroup = parseFloat($('#maximum-loan-amount-for-group').val());
        let minimumLoanAmountForIndividual = parseFloat($('#minimum-loan-amount-for-individual').val());
        let maximumLoanAmountForIndividual = parseFloat($('#maximum-loan-amount-for-individual').val());

        // Maturity Date Override
        let enableMaturityDateOverride = $('#enable-maturity-date-override').is(':checked') ? true : false;
        let minimumOverridePeriod = parseInt($('#minimum-override-period').val());
        let maximumOverridePeriod = parseInt($('#maximum-override-period').val());

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

        // Guarantor Detail
        let enableGuarantorDetail = $('#enable-guarantor-detail').is(':checked') ? true : false;
        let minimumNumberOfGuarantors = parseInt($('#minimum-number-of-guarantors').val());
        let maximumNumberOfGuarantors = parseInt($('#maximum-number-of-guarantors').val());
        let defaultNumberOfGuarantors = parseInt($('#default-number-of-guarantors').val());
        let numberOfLoansLimitForSameGuarantors = parseInt($('#number-of-loans-limit-for-same-guarantors').val());

        // Age
        let minimumAge = parseInt($('#minimum-age').val());
        let maximumAge = parseInt($('#maximum-age').val());

        // Acquaintance Details
        let enableAcquaintanceDetails = $('#enable-acquaintance-details').is(':checked') ? true : false;
        let minimumAcquaintance = parseInt($('#minimum-acquaintance').val());
        let maximumAcquaintance = parseInt($('#maximum-acquaintance').val());

        // SWOT Analysis
        let enableSWOTAnalysis = $('#enable-swot-analysis').is(':checked') ? true : false;
        let minimumSWOTAnalysis = parseInt($('#minimum-swot-analysis').val());

        // Past Credit History
        let enablePastCreditHistory = $('#enable-past-credit-history').is(':checked') ? true : false;
        let minimumPastCreditHistory = parseInt($('#minimum-past-credit-history').val());

        // Legal And Regulatory Compliance
        let enableLegalAndRegulatoryCompliance = $('#enable-regulatory-compliance').is(':checked') ? true : false;
        let minimumLegalCompliance = parseInt($('#minimum-legal-regulatory-compliance').val());

        // CIBIL Score
        let enableCibilScore = $('#enable-capture-cibil-score').is(':checked') ? true : false;
        let minimumScore = parseInt($('#minimum-cibil-score').val());
        let maximumScore = parseInt($('#maximum-cibil-score').val());

        // Debit To Income Ratio
        let enableDITRatio = $('#enable-capture-debt-income-ratio').is(':checked') ? true : false;
        let minimumRatio = parseInt($('#minimum-debt-income-ratio').val());
        let maximumRatio = parseInt($('#maximum-debt-income-ratio').val());

        let result = true;

        // Time Period For New Customer Flag
        if (isNaN(timePeriodForNewCustomerFlag) === false) {
            minimum = parseInt($('#time-period-for-new-customer-flag').attr('min'));
            maximum = parseInt($('#time-period-for-new-customer-flag').attr('max'));

            if (parseInt(timePeriodForNewCustomerFlag) < parseInt(minimum) || parseInt(timePeriodForNewCustomerFlag) > parseInt(maximum))
                result = false;
        }
        else
            result = false;

        // Share Ratio With Loan
        if ($('#shares-ratio-with-loan-input').hasClass('d-none') === false) {
            if (isNaN(sharesRatioWithLoan) === false) {
                minimum = parseFloat($('#shares-ratio-with-loan').attr('min'));
                maximum = parseFloat($('#shares-ratio-with-loan').attr('max'));

                if (parseFloat(sharesRatioWithLoan) < parseFloat(minimum) || parseFloat(sharesRatioWithLoan) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        // Minimum Loan Amount For Group
        if (isNaN(minimumLoanAmountForGroup) === false) {
            // Minimum Loan Amount For Group
            minimum = parseFloat($('#minimum-loan-amount-for-group').attr('min'));
            maximum = parseFloat($('#minimum-loan-amount-for-group').attr('max'));

            if (parseFloat(minimumLoanAmountForGroup) < parseFloat(minimum) || parseFloat(minimumLoanAmountForGroup) > parseFloat(maximum))
                result = false;
        }
        else
            result = false;

        // Maximum Loan Amount For Group
        if (isNaN(maximumLoanAmountForGroup) === false) {
            minimum = parseFloat($('#maximum-loan-amount-for-group').attr('min'));
            maximum = parseFloat($('#maximum-loan-amount-for-group').attr('max'));

            if (parseFloat(maximumLoanAmountForGroup) < parseFloat(minimum) || parseFloat(maximumLoanAmountForGroup) > parseFloat(maximum))
                result = false;
        }
        else
            result = false;

        // Minimum Loan Amount For Individual
        if (isNaN(minimumLoanAmountForIndividual) === false) {
            minimum = parseFloat($('#minimum-loan-amount-for-individual').attr('min'));
            maximum = parseFloat($('#minimum-loan-amount-for-individual').attr('max'));

            if (parseFloat(minimumLoanAmountForIndividual) < parseFloat(minimum) || parseFloat(minimumLoanAmountForIndividual) > parseFloat(maximum))
                result = false;
        }
        else
            result = false;

        // Maximum Loan Amount For Individual
        if (isNaN(maximumLoanAmountForIndividual) === false) {
            minimum = parseFloat($('#maximum-loan-amount-for-individual').attr('min'));
            maximum = parseFloat($('#maximum-loan-amount-for-individual').attr('max'));

            if (parseFloat(maximumLoanAmountForIndividual) < parseFloat(minimum) || parseFloat(maximumLoanAmountForIndividual) > parseFloat(maximum))
                result = false;
        }
        else
            result = false;

        // Maturity Date Override
        if (enableMaturityDateOverride === true) {
            // Minimum Override Period
            if (isNaN(minimumOverridePeriod) === false) {
                minimum = parseInt($('#minimum-override-period').attr('min'));
                maximum = parseInt($('#minimum-override-period').attr('max'));

                if (parseInt(minimumOverridePeriod) < parseInt(minimum) || parseInt(minimumOverridePeriod) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Maximum Override Period
            if (isNaN(maximumOverridePeriod) === false) {
                minimum = parseInt($('#maximum-override-period').attr('min'));
                maximum = parseInt($('#maximum-override-period').attr('max'));

                if (parseInt(maximumOverridePeriod) < parseInt(minimum) || parseInt(maximumOverridePeriod) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;
        }

        // Number Of Joint Account Holding Limit
        if (enableNumberOfJointAccountHoldingLimit === true) {
            // Minimum Joint Account Holder
            if (isNaN(minimumJointAccountHolder) === false) {
                minimum = parseInt($('#minimum-joint-account-holder').attr('min'));
                maximum = parseInt($('#minimum-joint-account-holder').attr('max'));

                if (parseInt(minimumJointAccountHolder) < parseInt(minimum) || parseInt(minimumJointAccountHolder) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Maximum Joint Account Holder
            if (isNaN(maximumJointAccountHolder) === false) {
                minimum = parseInt($('#maximum-joint-account-holder').attr('min'));
                maximum = parseInt($('#maximum-joint-account-holder').attr('max'));

                if (parseInt(maximumJointAccountHolder) < parseInt(minimum) || parseInt(maximumJointAccountHolder) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Default Joint Account Holder
            if (isNaN(defaultJointAccountHolder) === false) {
                minimum = parseInt($('#default-joint-account-holder').attr('min'));
                maximum = parseInt($('#default-joint-account-holder').attr('max'));

                if (parseInt(defaultJointAccountHolder) < parseInt(minimum) || parseInt(defaultJointAccountHolder) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;
        }

        // Number Of Nominee Limit
        if (enableNumberOfNomineeLimit === true) {
            // minimum Nominee
            if (isNaN(minimumNominee) === false) {
                minimum = parseInt($('#minimum-nominee').attr('min'));
                maximum = parseInt($('#minimum-nominee').attr('max'));

                if (parseInt(minimumNominee) < parseInt(minimum) || parseInt(minimumNominee) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // maximum Nominee
            if (isNaN(maximumNominee) === false) {
                minimum = parseInt($('#maximum-nominee').attr('min'));
                maximum = parseInt($('#maximum-nominee').attr('max'));

                if (parseInt(maximumNominee) < parseInt(minimum) || parseInt(maximumNominee) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // default Nominee
            if (isNaN(defaultNominee) === false) {
                minimum = parseInt($('#default-nominee').attr('min'));
                maximum = parseInt($('#default-nominee').attr('max'));

                if (parseInt(defaultNominee) < parseInt(minimum) || parseInt(defaultNominee) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;
        }

        if (enableGuarantorDetail === true) {
            // minimum Number Of Guarantors 
            if (isNaN(minimumNumberOfGuarantors) === false) {
                minimum = parseInt($('#minimum-number-of-guarantors').attr('min'));
                maximum = parseInt($('#minimum-number-of-guarantors').attr('max'));

                if (parseInt(minimumNumberOfGuarantors) < parseInt(minimum) || parseInt(minimumNumberOfGuarantors) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // maximum Number Of Guarantors 
            if (isNaN(maximumNumberOfGuarantors) === false) {
                minimum = parseInt($('#maximum-number-of-guarantors').attr('min'));
                maximum = parseInt($('#maximum-number-of-guarantors').attr('max'));

                if (parseInt(maximumNumberOfGuarantors) < parseInt(minimum) || parseInt(maximumNumberOfGuarantors) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // default Number Of Guarantors
            if (isNaN(defaultNumberOfGuarantors) === false) {
                minimum = parseInt($('#default-number-of-guarantors').attr('min'));
                maximum = parseInt($('#default-number-of-guarantors').attr('max'));

                if (parseInt(defaultNumberOfGuarantors) < parseInt(minimum) || parseInt(defaultNumberOfGuarantors) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // number Of Loans Limit For Same Guarantors
            if (isNaN(numberOfLoansLimitForSameGuarantors) === false) {
                minimum = parseInt($('#number-of-loans-limit-for-same-guarantors').attr('min'));
                maximum = parseInt($('#number-of-loans-limit-for-same-guarantors').attr('max'));

                if (parseInt(numberOfLoansLimitForSameGuarantors) < parseInt(minimum) || parseInt(numberOfLoansLimitForSameGuarantors) > parseInt(maximum))
                    result = false;

            }
            else
                result = false;

            if ($('#eligibility-for-guarantor-id').prop('selectedIndex') < 1) {
                result = false;
            }
        }

        // Enable Acquaintance Details
        if (enableAcquaintanceDetails === true) {
            // Minimum Acquaintance
            if (isNaN(minimumAcquaintance) === false) {
                minimum = parseInt($('#minimum-acquaintance').attr('min'));
                maximum = parseInt($('#minimum-acquaintance').attr('max'));

                if (parseInt(minimumAcquaintance) < parseInt(minimum) || parseInt(minimumAcquaintance) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //  Maximum Acquaintance
            if (isNaN(maximumAcquaintance) === false) {
                minimum = parseInt($('#maximum-acquaintance').attr('min'));
                maximum = parseInt($('#maximum-acquaintance').attr('max'));

                if (parseInt(maximumAcquaintance) < parseInt(minimum) || parseInt(maximumAcquaintance) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        //Minimum Age
        if (isNaN(minimumAge) === false) {
            minimum = parseInt($('#minimum-age').attr('min'));
            maximum = parseInt($('#minimum-age').attr('max'));

            if (parseInt(minimumAge) < parseInt(minimum) || parseInt(minimumAge) > parseInt(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        //Maximum Age
        if (isNaN(maximumAge) === false) {
            minimum = parseInt($('#maximum-age').attr('min'));
            maximum = parseInt($('#maximum-age').attr('max'));

            if (parseInt(maximumAge) < parseInt(minimum) || parseInt(maximumAge) > parseInt(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Enable SWOT Analysis
        if (enableSWOTAnalysis === true) {
            // Minimum SWOT Analysis
            if (isNaN(minimumSWOTAnalysis) === false) {
                minimum = parseInt($('#minimum-swot-analysis').attr('min'));
                maximum = parseInt($('#minimum-swot-analysis').attr('max'));

                if (parseInt(minimumSWOTAnalysis) < parseInt(minimum) || parseInt(minimumSWOTAnalysis) > parseInt(maximum)) {
                    $('#minimum-swot-analysis-error').removeClass('d-none');
                    result = false;
                } else {
                    $('#minimum-swot-analysis-error').addClass('d-none');
                }
            }
            else {
                result = false;
            }
        }

        // Enable Past Credit History
        if (enablePastCreditHistory === true) {
            // Minimum Past Credit History
            if (isNaN(minimumPastCreditHistory) === false) {
                minimum = parseInt($('#minimum-past-credit-history').attr('min'));
                maximum = parseInt($('#minimum-past-credit-history').attr('max'));

                if (parseInt(minimumPastCreditHistory) < parseInt(minimum) || parseInt(minimumPastCreditHistory) > parseInt(maximum)) {
                    $('#minimum-past-credit-history-error').removeClass('d-none');
                    result = false;
                } else {
                    $('#minimum-past-credit-history-error').addClass('d-none');
                }
            }
            else {
                result = false;
            }
        }

        // Enable Legal And Regulatory Compliance
        if (enableLegalAndRegulatoryCompliance === true) {
            // Minimum Legal Compliance
            if (isNaN(minimumLegalCompliance) === false) {
                minimum = parseInt($('#minimum-legal-regulatory-compliance').attr('min'));
                maximum = parseInt($('#minimum-legal-regulatory-compliance').attr('max'));

                if (parseInt(minimumLegalCompliance) < parseInt(minimum) || parseInt(minimumLegalCompliance) > parseInt(maximum)) {
                    $('#minimum-legal-regulatory-compliance-error').removeClass('d-none');
                    result = false;
                }
                else {
                    $('#minimum-legal-regulatory-compliance-error').addClass('d-none');
                }
            }
            else {
                result = false;
            }
        }

        // Enable Cibil Score
        if (enableCibilScore === true) {
            //minimum Cibil Score
            if (isNaN(minimumScore) === false) {
                minimum = parseInt($('#minimum-cibil-score').attr('min'));
                maximum = parseInt($('#minimum-cibil-score').attr('max'));

                if (parseInt(minimumScore) < parseInt(minimum) || parseInt(minimumScore) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //maximum Cibil Score
            if (isNaN(maximumScore) === false) {
                minimum = parseInt($('#maximum-cibil-score').attr('min'));
                maximum = parseInt($('#maximum-cibil-score').attr('max'));

                if ((parseInt(maximumScore) < parseInt(minimum)) || parseInt(maximumScore) === 0 || parseInt(maximumScore) > parseInt(maximum)) {
                    result = false;
                    $('#maximum-cibil-score-error').removeClass('d-none');
                }
                else {
                    $('#maximum-cibil-score-error').addClass('d-none');
                }
            }
            else {
                result = false;
            }
        }

        // Enable Capture Debt To Income Ratio
        if (enableDITRatio === true) {
            //Minimum ratio
            if (isNaN(minimumRatio) === false) {
                minimum = parseInt($('#minimum-debt-income-ratio').attr('min'));
                maximum = parseInt($('#minimum-debt-income-ratio').attr('max'));

                if (parseInt(minimumRatio) < parseInt(minimum) || parseInt(minimumRatio) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //Maximum Ratio
            if (isNaN(maximumRatio) === false) {
                minimum = parseInt($('#maximum-debt-income-ratio').attr('min'));
                maximum = parseInt($('#maximum-debt-income-ratio').attr('max'));

                if (parseInt(maximumRatio) < parseInt(minimum) || parseInt(maximumRatio) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }


        if (result)
            $('#account-parameter-accordion-title-error').addClass('d-none');
        else
            $('#account-parameter-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 5. Tenure Accordion Input Validation
    function IsValidTenureAccordionInputs() {
        let minimumTenure = parseInt($('#minimum-tenure').val());
        let multiplesOf = parseInt($('#tenure-multiple-of').val());
        let maximumTenure = parseInt($('#maximum-tenure').val());
        let defaultTenure = parseInt($('#default-tenure').val());

        result = true;

        if ($('#enable-tenure').is(':checked')) {
            if ($('#time-period-unit-id').prop('selectedIndex') < 1) {
                result = false;
            }

            // Multiples  For Tenure 
            if (isNaN(multiplesOf) === false) {

                minimum = parseInt($('#tenure-multiple-of').attr('min'));
                maximum = parseInt($('#tenure-multiple-of').attr('max'));

                if (parseInt(multiplesOf) < parseInt(minimum) || parseInt(multiplesOf) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Default For Tenure 
            if (isNaN(defaultTenure) === false) {
                minimum = parseInt($('#default-tenure').attr('min'));
                maximum = parseInt($('#default-tenure').attr('max'));

                if (parseInt(defaultTenure) < parseInt(minimum) || parseInt(defaultTenure) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Maximum For Tenure 
            if (isNaN(maximumTenure) === false) {
                minimum = parseInt($('#maximum-tenure').attr('min'));
                maximum = parseInt($('#maximum-tenure').attr('max'));

                if (parseInt(maximumTenure) < parseInt(minimum) || parseInt(maximumTenure) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Minimum For Tenure 
            if (isNaN(minimumTenure) === false) {
                minimum = parseInt($('#minimum-tenure').attr('min'));
                maximum = parseInt($('#minimum-tenure').attr('max'));

                if (parseInt(minimumTenure) < parseInt(minimum) || parseInt(minimumTenure) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;
        }

        if (result)
            $('#tenure-accordion-title-error').addClass('d-none');
        else
            $('#tenure-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 6. Target Estimation Accordion Input Validation
    function IsValidTargetEstimationAccordionInputs() {
        result = true;

        if ($('#estimate-target-card').hasClass('d-none') === false) {
            let numberOfIncrementAccount = parseInt($('#number-of-account').val());
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

    // 7. Pre Part Payment Accordion Input Validation
    function IsValidPrePartPaymentAccordionInputs() {
        debugger;
        let minimumRepaymentOfEMIForPrePartPayment = parseInt($('#minimum-repayment-of-emi').val());
        let minimumMonthForPrePartPayment = parseInt($('#minimum-month-for-pre-part-payment').val());
        let maximumMonthForPrePartPayment = parseInt($('#maximum-month-for-pre-part-payment').val());
        let minimumPrePartPaymentAmount = parseFloat($('#minimum-pre-part-payment-amount').val());
        let maximumPrePartPaymentAmount = parseFloat($('#maximum-pre-part-payment-amount').val());
        let interestRate = parseFloat($('#pre-part-payment-interest-rate').val());
        let prePartPaymentRepetitionLimitInFinancialYear = parseInt($('#pre-part-payment-repetition-limit-in-financial-year').val());

        result = true;

        if ($('#enable-pre-part-payment').is(':checked')) {
            if ($('.pre-part-payment-based-on:checked').length === 0)
                result = false;

            //Repayment Of EMI For Pre part Payment
            if (isNaN(minimumRepaymentOfEMIForPrePartPayment) === false) {
                minimum = parseInt($('#minimum-repayment-of-emi').attr('min'));
                maximum = parseInt($('#minimum-repayment-of-emi').attr('max'));

                if (parseInt(minimumRepaymentOfEMIForPrePartPayment) < parseInt(minimum) || parseInt(minimumRepaymentOfEMIForPrePartPayment) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            //Minimum Month Pre Part Payment
            if (isNaN(minimumMonthForPrePartPayment) === false) {
                minimum = parseInt($('#minimum-month-for-pre-part-payment').attr('min'));
                maximum = parseInt($('#minimum-month-for-pre-part-payment').attr('max'));

                if (parseInt(minimumMonthForPrePartPayment) < parseInt(minimum) || parseInt(minimumMonthForPrePartPayment) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            //Maximum Month Pre Part Payment
            if (isNaN(maximumPrePartPaymentAmount) === false) {
                minimum = parseInt($('#maximum-month-for-pre-part-payment').attr('min'));
                maximum = parseInt($('#maximum-month-for-pre-part-payment').attr('max'));

                if (parseInt(maximumMonthForPrePartPayment) < parseInt(minimum) || parseInt(maximumMonthForPrePartPayment) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            //Interest Rate
            if (isNaN(interestRate) === false) {
                minimum = parseFloat($('#pre-part-payment-interest-rate').attr('min'));
                maximum = parseFloat($('#pre-part-payment-interest-rate').attr('max'));

                if (parseFloat(interestRate) < parseFloat(minimum) || parseFloat(interestRate) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            if ($('#interest-method-id-pre-part').prop('selectedIndex') < 1) {
                result = false;
            }

            //Payment Repetition Limit In Financial Year
            if (isNaN(prePartPaymentRepetitionLimitInFinancialYear) === false) {
                minimum = parseFloat($('#pre-part-payment-repetition-limit-in-financial-year').attr('min'));
                maximum = parseFloat($('#pre-part-payment-repetition-limit-in-financial-year').attr('max'));

                if (parseFloat(prePartPaymentRepetitionLimitInFinancialYear) < parseFloat(minimum) || parseFloat(prePartPaymentRepetitionLimitInFinancialYear) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            // Minimum Pre Part Payment Amount
            if (isNaN(minimumPrePartPaymentAmount) === false) {
                minimum = parseFloat($('#minimum-pre-part-payment-amount').attr('min'));
                maximum = parseFloat($('#minimum-pre-part-payment-amount').attr('max'));

                if (parseFloat(minimumPrePartPaymentAmount) < parseFloat(minimum) || parseFloat(minimumPrePartPaymentAmount) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            // Maximum Pre Part Payment Amount
            if (isNaN(maximumPrePartPaymentAmount) === false) {
                minimum = parseFloat($('#maximum-pre-part-payment-amount').attr('min'));
                maximum = parseFloat($('#maximum-pre-part-payment-amount').attr('max'));

                if (parseFloat(maximumPrePartPaymentAmount) < parseFloat(minimum) || parseFloat(maximumPrePartPaymentAmount) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

        }

        if (result)
            $('#pre-part-payment-accordion-title-error').addClass('d-none');
        else
            $('#pre-part-payment-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 8. Pre Full Payment Accordion Input Validation
    function IsValidPreFullPaymentAccordionInputs() {
        let minimumRepaymentOfEMI = parseInt($('#minimum-repayment-emi-full-payment').val());
        let minimumMonth = parseInt($('#minimum-month').val());
        let maximumMonth = parseInt($('#maximum-month').val());
        let interestRate = parseFloat($('#interest-rate').val());

        result = true;

        if ($('#enable-fore-closure').is(':checked')) {
            // Repayment Of EMI For Pre Full Payment
            if (isNaN(minimumRepaymentOfEMI) === false) {
                minimum = parseInt($('#minimum-repayment-emi-full-payment').attr('min'));
                maximum = parseInt($('#minimum-repayment-emi-full-payment').attr('max'));

                if (parseInt(minimumRepaymentOfEMI) < parseInt(minimum) || parseInt(minimumRepaymentOfEMI) > parseInt(maximum))
                    result = false;
            }

            // Minimum Month
            if (isNaN(minimumMonth) === false) {
                minimum = parseInt($('#minimum-month').attr('min'));
                maximum = parseInt($('#minimum-month').attr('max'));

                if (parseInt(minimumMonth) < parseInt(minimum) || parseInt(minimumMonth) > parseInt(maximum))
                    result = false;
            }

            // Maximum Month
            if (isNaN(maximumMonth) === false) {
                minimum = parseInt($('#maximum-month').attr('min'));
                maximum = parseInt($('#maximum-month').attr('max'));

                if (parseInt(maximumMonth) < parseInt(minimum) || parseInt(maximumMonth) > parseInt(maximum))
                    result = false;
            }

            // Interest Rate
            if (isNaN(interestRate) === false) {
                minimum = parseFloat($('#interest-rate').attr('min'));
                maximum = parseFloat($('#interest-rate').attr('max'));

                if (parseFloat(interestRate) < parseFloat(minimum) || parseFloat(interestRate) > parseFloat(maximum))
                    result = false;
            }

            if ($('#pre-full-interest-method-id').prop('selectedIndex') < 1) {
                result = false;
            }
        }

        if (result)
            $('#pre-full-payment-accordion-title-error').addClass('d-none');
        else
            $('#pre-full-payment-accordion-title-error').removeClass('d-none');

        return result;
    }

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
                else
                    result = false;

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
            $('#passbook-detail-accordion-title-error').addClass('d-none');
        else
            $('#passbook-detail-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 10. Loan Funder Accordion Input Validation
    function IsValidLoanFunderAccordionInputs() {
        let funderLoanFundingPercentage = parseFloat($('#funder-loan-funding-percentage').val());
        let funderInterestCommissions = parseFloat($('#funder-interest-commissions').val());

        result = true;

        if ($('#enable-funder').is(':checked')) {
            // Loan Funding Percentage 
            if (isNaN(funderLoanFundingPercentage) === false) {
                minimum = parseFloat($('#funder-loan-funding-percentage').attr('min'));
                maximum = parseFloat($('#funder-loan-funding-percentage').attr('max'));

                if (parseFloat(funderLoanFundingPercentage) < parseFloat(minimum) || parseFloat(funderLoanFundingPercentage) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            // Loan Funding Commissions
            if (isNaN(funderInterestCommissions) === false) {
                minimum = parseFloat($('#funder-interest-commissions').attr('min'));
                maximum = parseFloat($('#funder-interest-commissions').attr('max'));

                if (parseFloat(funderInterestCommissions) < parseFloat(minimum) || parseFloat(funderInterestCommissions) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;
        }


        if (result)
            $('#loan-funder-accordion-title-error').addClass('d-none');
        else
            $('#loan-funder-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 11. Loan Installment Accordion Input Validation
    function IsValidLoanInstallmentAccordionInputs() {
        let numberOfOverdueInstallmentRecovery = parseInt($('#number-of-overdue-installment-recovery').val());
        let minimumOverDuesInstallment = parseFloat($('#minimum-over-dues-installment').val());
        let maximumOverDuesInstallment = parseFloat($('#maximum-over-dues-installment').val());
        let defaultOverDuesInstallment = parseFloat($('#default-over-dues-installment').val());

        result = true;

        if ($('#installment-repayment-schedule-accordions').hasClass('d-none') === false) {
            if (isNaN(numberOfOverdueInstallmentRecovery) === false) {
                //Recovery From Linked Account
                minimum = parseInt($('#number-of-overdue-installment-recovery').attr('min'));
                maximum = parseInt($('#number-of-overdue-installment-recovery').attr('max'));

                if (parseInt(numberOfOverdueInstallmentRecovery) < parseInt(minimum) || parseInt(numberOfOverdueInstallmentRecovery) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            //Minimum Over Dues Installment
            if (isNaN(minimumOverDuesInstallment) === false) {
                minimum = parseFloat($('#minimum-over-dues-installment').attr('min'));
                maximum = parseFloat($('#minimum-over-dues-installment').attr('max'));

                if (parseFloat(minimumOverDuesInstallment) < parseFloat(minimum) || parseFloat(minimumOverDuesInstallment) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            //Maximum Over Dues Installment
            if (isNaN(maximumOverDuesInstallment) === false) {
                minimum = parseFloat($('#maximum-over-dues-installment').attr('min'));
                maximum = parseFloat($('#maximum-over-dues-installment').attr('max'));

                if (parseFloat(maximumOverDuesInstallment) < parseFloat(minimum) || parseFloat(maximumOverDuesInstallment) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            //Default Over Dues Installment
            if (isNaN(defaultOverDuesInstallment) === false) {
                minimum = parseFloat($('#default-over-dues-installment').attr('min'));
                maximum = parseFloat($('#default-over-dues-installment').attr('max'));

                if (parseFloat(defaultOverDuesInstallment) < parseFloat(minimum) || parseFloat(defaultOverDuesInstallment) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;
        }

        if (result)
            $('#loan-installment-accordion-title-error').addClass('d-none');
        else
            $('#loan-installment-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 12. Loan Sanction Authority Accordion Input Validation
    function IsValidSanctionAuthorityAccordionInputs() {
        let managerEmpoweredSanctionLoanAmountFrom = parseFloat($('#minimum-loan-sanction-amount-manager').val());
        let managerEmpoweredSanctionLoanAmountTo = parseFloat($('#maximum-loan-sanction-amount-manager').val());
        let committeeEmpoweredSanctionLoanAmountFrom = parseFloat($('#minimum-loan-sanction-amount-committee').val());
        let committeeEmpoweredSanctionLoanAmountTo = parseFloat($('#maximum-loan-sanction-amount-committee').val());
        let boardOfDirectorEmpoweredSanctionLoanAmountFrom = parseFloat($('#minimum-loan-sanction-amount-bod').val());
        let boardOfDirectorEmpoweredSanctionLoanAmountTo = parseFloat($('#maximum-loan-sanction-amount-bod').val());
        let ceoEmpoweredSanctionLoanAmountFrom = parseFloat($('#minimum-loan-sanction-amount-ceo').val());
        let ceoEmpoweredSanctionLoanAmountTo = parseFloat($('#maximum-loan-sanction-amount-ceo').val());
        let chairmanEmpoweredSanctionLoanAmountFrom = parseFloat($('#minimum-loan-sanction-amount-chairman').val());
        let chairmanEmpoweredSanctionLoanAmountTo = parseFloat($('#maximum-loan-sanction-amount-chairman').val());

        result = true;

        if ($('#loan-sanction-authority-card').hasClass('d-none') === false) {
            // Manager Empowered Sanction Loan Amount From
            if (isNaN(managerEmpoweredSanctionLoanAmountFrom) === false) {
                minimum = parseFloat($('#minimum-loan-sanction-amount-manager').attr('min'));
                maximum = parseFloat($('#minimum-loan-sanction-amount-manager').attr('max'));

                if (parseFloat(managerEmpoweredSanctionLoanAmountFrom) < parseFloat(minimum) || parseFloat(managerEmpoweredSanctionLoanAmountFrom) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Manager Empowered Sanction Loan Amount To
            if (isNaN(managerEmpoweredSanctionLoanAmountTo) === false) {
                //Recovery From Linked Account
                minimum = parseFloat($('#maximum-loan-sanction-amount-manager').attr('min'));
                maximum = parseFloat($('#maximum-loan-sanction-amount-manager').attr('max'));

                if (parseFloat(managerEmpoweredSanctionLoanAmountTo) < parseFloat(minimum) || parseFloat(managerEmpoweredSanctionLoanAmountTo) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Committee Empowered Sanction Loan Amount From
            if (isNaN(committeeEmpoweredSanctionLoanAmountFrom) === false) {
                minimum = parseFloat($('#minimum-loan-sanction-amount-committee').attr('min'));
                maximum = parseFloat($('#minimum-loan-sanction-amount-committee').attr('max'));

                if (parseFloat(committeeEmpoweredSanctionLoanAmountFrom) < parseFloat(minimum) || parseFloat(committeeEmpoweredSanctionLoanAmountFrom) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Committee Empowered Sanction Loan Amount TO
            if (isNaN(committeeEmpoweredSanctionLoanAmountTo) === false) {
                minimum = parseFloat($('#maximum-loan-sanction-amount-committee').attr('min'));
                maximum = parseFloat($('#maximum-loan-sanction-amount-committee').attr('max'));

                if (parseFloat(committeeEmpoweredSanctionLoanAmountTo) < parseFloat(minimum) || parseFloat(committeeEmpoweredSanctionLoanAmountTo) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Board Of Director (BOD) Empowered Sanction Loan Amount From
            if (isNaN(boardOfDirectorEmpoweredSanctionLoanAmountFrom) === false) {
                minimum = parseFloat($('#minimum-loan-sanction-amount-bod').attr('min'));
                maximum = parseFloat($('#minimum-loan-sanction-amount-bod').attr('max'));

                if (parseFloat(boardOfDirectorEmpoweredSanctionLoanAmountFrom) < parseFloat(minimum) || parseFloat(boardOfDirectorEmpoweredSanctionLoanAmountFrom) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Board Of Director (BOD) Empowered Sanction Loan Amount To
            if (isNaN(boardOfDirectorEmpoweredSanctionLoanAmountTo) === false) {
                minimum = parseFloat($('#maximum-loan-sanction-amount-bod').attr('min'));
                maximum = parseFloat($('#maximum-loan-sanction-amount-bod').attr('max'));

                if (parseFloat(boardOfDirectorEmpoweredSanctionLoanAmountTo) < parseFloat(minimum) || parseFloat(boardOfDirectorEmpoweredSanctionLoanAmountTo) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // CEO Empowered Sanction Loan Amount From
            if (isNaN(ceoEmpoweredSanctionLoanAmountFrom) === false) {
                minimum = parseFloat($('#minimum-loan-sanction-amount-ceo').attr('min'));
                maximum = parseFloat($('#minimum-loan-sanction-amount-ceo').attr('max'));

                if (parseFloat(ceoEmpoweredSanctionLoanAmountFrom) < parseFloat(minimum) || parseFloat(ceoEmpoweredSanctionLoanAmountFrom) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // CEO Empowered Sanction Loan Amount To
            if (isNaN(ceoEmpoweredSanctionLoanAmountTo) === false) {
                minimum = parseFloat($('#maximum-loan-sanction-amount-ceo').attr('min'));
                maximum = parseFloat($('#maximum-loan-sanction-amount-ceo').attr('max'));

                if (parseFloat(ceoEmpoweredSanctionLoanAmountTo) < parseFloat(minimum) || parseFloat(ceoEmpoweredSanctionLoanAmountTo) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Chairman Empowered Sanction Loan Amount From
            if (isNaN(chairmanEmpoweredSanctionLoanAmountFrom) === false) {
                minimum = parseFloat($('#minimum-loan-sanction-amount-chairman').attr('min'));
                maximum = parseFloat($('#minimum-loan-sanction-amount-chairman').attr('max'));

                if (parseFloat(chairmanEmpoweredSanctionLoanAmountFrom) < parseFloat(minimum) || parseFloat(chairmanEmpoweredSanctionLoanAmountFrom) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Chairman Empowered Sanction Loan Amount To
            if (isNaN(chairmanEmpoweredSanctionLoanAmountTo) === false) {
                minimum = parseFloat($('#maximum-loan-sanction-amount-chairman').attr('min'));
                maximum = parseFloat($('#maximum-loan-sanction-amount-chairman').attr('max'));

                if (parseFloat(chairmanEmpoweredSanctionLoanAmountTo) < parseFloat(minimum) || parseFloat(chairmanEmpoweredSanctionLoanAmountTo) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        if (result)
            $('#loan-sanction-authority-accordion-title-error').addClass('d-none');
        else
            $('#loan-sanction-authority-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 13. Loan Interest Rebate Accordion Input Validation
    function IsValidLoanInterestRebateAccordionInputs() {
        let minimumDuesInstallmentGracePeriodInDays = parseInt($('#minimum-dues-installment-grace-period-in-days').val());
        let maximumDuesInstallmentGracePeriodInDays = parseInt($('#maximum-dues-installment-grace-period-in-days').val());
        let minimumApplicableNumber = parseInt($('#minimum-applicable-number-installment-interest').val());
        let maximumApplicableNumber = parseInt($('#maximum-applicable-number-installment-interest').val());
        let interestRebatePercentage = parseFloat($('#interest-rebate-percentage').val());

        result = true;

        if ($('#enable-rebate').is(':checked')) {
            // General Ledger
            if ($('#general-ledger-id').prop('selectedIndex') < 1) {
                result = false;
            }

            // Interest Rebate Criteria
            if ($('#interest-rebate-criteria-id').prop('selectedIndex') < 1) {
                result = false;
            }

            // Interest Rebate Apply Frequency
            if ($('#interest-rebate-apply-frequency-id').prop('selectedIndex') < 1) {
                result = false;
            }

            // Minimum Installment Grace Period In Days
            if (isNaN(minimumDuesInstallmentGracePeriodInDays) === false) {
                minimum = parseInt($('#minimum-dues-installment-grace-period-in-days').attr('min'));
                maximum = parseInt($('#minimum-dues-installment-grace-period-in-days').attr('max'));

                if (parseInt(minimumDuesInstallmentGracePeriodInDays) < parseInt(minimum) || parseInt(minimumDuesInstallmentGracePeriodInDays) > parseInt(maximum))
                    result = false;
            }

            // Maximum Installment Grace Period In Days
            if (isNaN(maximumDuesInstallmentGracePeriodInDays) === false) {
                minimum = parseInt($('#maximum-dues-installment-grace-period-in-days').attr('min'));
                maximum = parseInt($('#maximum-dues-installment-grace-period-in-days').attr('max'));

                if (parseInt(maximumDuesInstallmentGracePeriodInDays) < parseInt(minimum) || parseInt(maximumDuesInstallmentGracePeriodInDays) > parseInt(maximum))
                    result = false;
            }

            // Minimum Applicable Number
            if (isNaN(minimumApplicableNumber) === false) {
                minimum = parseInt($('#minimum-applicable-number-installment-interest').attr('min'));
                maximum = parseInt($('#minimum-applicable-number-installment-interest').attr('max'));

                if (parseInt(minimumApplicableNumber) < parseInt(minimum) || parseInt(minimumApplicableNumber) > parseInt(maximum))
                    result = false;
            }

            // Maximum Applicable Number
            if (isNaN(maximumApplicableNumber) === false) {
                minimum = parseInt($('#maximum-applicable-number-installment-interest').attr('min'));
                maximum = parseInt($('#maximum-applicable-number-installment-interest').attr('max'));

                if (parseInt(maximumApplicableNumber) < parseInt(minimum) || parseInt(maximumApplicableNumber) > parseInt(maximum))
                    result = false;
            }

            // Interest Rebate Percentage
            if (isNaN(interestRebatePercentage) === false) {
                minimum = parseFloat($('#interest-rebate-percentage').attr('min'));
                maximum = parseFloat($('#interest-rebate-percentage').attr('max'));

                if (parseFloat(interestRebatePercentage) < parseFloat(minimum) || parseFloat(interestRebatePercentage) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;
        }

        if (result)
            $('#loan-interest-rebate-accordion-title-error').addClass('d-none');
        else
            $('#loan-interest-rebate-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 14. Loan Settlement Account Input Validation
    function IsValidSettlementAccountAccordionInputs() {
        debugger;

        let result = true;
        let settlementType = $('.settlement-type:checked').length;

        if ($('#enable-settlement-account').is(':checked')) {
            if ($('.settlement-type:checked').length === 0)
                result = false;
        }

        if (result)
            $('#loan-settlement-accordion-title-error').addClass('d-none');
        else
            $('#loan-settlement-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 15. Loan Arrear Accordion Input Validation
    function IsValidLoanArrearAccordionInputs() {
        let arrearsToleranceMinimumPeriod = parseInt($('#minimum-arrears-tolerance-period').val());
        let arrearsToleranceMaximumPeriod = parseInt($('#maximum-arrears-tolerance-period').val());
        let arrearsToleranceDefaultPeriod = parseInt($('#default-arrears-tolerance-period').val());
        let lockArrearsAccountAfterDays = parseInt($('#lock-arrears-account-after-days').val());
        let capChargesLimit = parseFloat($('#cap-charges-limit').val());

        result = true;

        if ($('#loan-arrear-parameter-card').hasClass('d-none') === false) {
            // Arrears Tolerance Minimum Period  
            if (isNaN(arrearsToleranceMinimumPeriod) === false) {
                minimum = parseFloat($('#minimum-arrears-tolerance-period').attr('min'));
                maximum = parseFloat($('#minimum-arrears-tolerance-period').attr('max'));

                if (parseInt(arrearsToleranceMinimumPeriod) < parseInt(minimum) || parseInt(arrearsToleranceMinimumPeriod) > parseInt(maximum))
                    result = false;
            }
            else {
                result = false;
            }
            // Arrears Tolerance Maximum Period
            if (isNaN(arrearsToleranceMaximumPeriod) === false) {
                minimum = parseInt($('#maximum-arrears-tolerance-period').attr('min'));
                maximum = parseInt($('#maximum-arrears-tolerance-period').attr('max'));

                if (parseInt(arrearsToleranceMaximumPeriod) < parseInt(minimum) || parseInt(arrearsToleranceMaximumPeriod) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Arrears Tolerance Default Period
            if (isNaN(arrearsToleranceDefaultPeriod) === false) {
                minimum = parseFloat($('#default-arrears-tolerance-period').attr('min'));
                maximum = parseFloat($('#default-arrears-tolerance-period').attr('max'));

                if (parseInt(arrearsToleranceDefaultPeriod) < parseInt(minimum) || parseInt(arrearsToleranceDefaultPeriod) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            if ($('.arrears-days-calculated-from:checked').length === 0)
                result = false;

            // Lock Arrears Account After Days
            if (isNaN(lockArrearsAccountAfterDays) === false) {
                minimum = parseInt($('#lock-arrears-account-after-days').attr('min'));
                maximum = parseInt($('#lock-arrears-account-after-days').attr('max'));

                if (parseInt(lockArrearsAccountAfterDays) < parseInt(minimum) || parseInt(lockArrearsAccountAfterDays) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Cap Charges Limit
            if (isNaN(capChargesLimit) === false) {
                minimum = parseFloat($('#cap-charges-limit').attr('min'));
                maximum = parseFloat($('#cap-charges-limit').attr('max'));

                if (parseFloat(capChargesLimit) < parseFloat(minimum) || parseFloat(capChargesLimit) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            if ($('.cap-charges-limit-on:checked').length === 0)
                result = false;

            if ($('.cap-type:checked').length === 0)
                result = false;
        }

        if (result)
            $('#loan-arrear-accordion-title-error').addClass('d-none');
        else
            $('#loan-arrear-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 16. Loan Interest Accordion Input Validation
    function IsValidLoanInterestAccordionInputs() {
        debugger;
        let interestCalculationHolidayPeriod = parseInt($('#interest-calculation-holiday-period').val());
        let minimumInterestRate = parseFloat($('#minimum-interest-rate').val());
        let maximumInterestRate = parseFloat($('#maximum-interest-rate').val());
        let rateOfFineInterest = parseFloat($('#rate-of-fine-interest').val());
        let enableLoanFineInterestParameter = $('#enable-loan-fine-interest-parameter').is(':checked') ? true : false;
        let enableLoanInterestProvisionParameter = $('#enable-loan-interest-provision-parameter').is(':checked') ? true : false;

        result = true;

        if ($('#general-ledger-id-interest-parameter').prop('selectedIndex') < 1) {
            result = false;
        }

        if ($('#interest-method-id-loan-interest').prop('selectedIndex') < 1) {
            result = false;
        }

        // Calculation Holiday Period
        if (isNaN(interestCalculationHolidayPeriod) === false) {
            minimum = parseInt($('#interest-calculation-holiday-period').attr('min'));
            maximum = parseInt($('#interest-calculation-holiday-period').attr('max'));

            if (parseInt(interestCalculationHolidayPeriod) < parseInt(minimum) || parseInt(interestCalculationHolidayPeriod) > parseInt(maximum))
                result = false;
        }
        else {
            result = false;
        }

        // Interest Type Dropdown
        if ($('#interest-type-id').prop('selectedIndex') < 1)
            result = false;

        // Interest Rate Charged Duration
        if ($('#interest-rate-charged-duration-id').prop('selectedIndex') < 1)
            result = false;

        // Days In Year Dropdown
        if ($('#days-in-year-id').prop('selectedIndex') < 1)
            result = false;

        // Lending Repayments Interest Calculation Dropdownlist
        if ($('#lending-repayments-interest-calculation-id').prop('selectedIndex') < 1)
            result = false;

        // Interest Applied From Radio Button
        if ($('.new-interest-applied-from:checked').length === 0)
            result = false;

        // Minimum Interest Rate
        if (isNaN(minimumInterestRate) === false) {
            minimum = parseFloat($('#minimum-interest-rate').attr('min'));
            maximum = parseFloat($('#minimum-interest-rate').attr('max'));

            if (parseFloat(minimumInterestRate) < parseFloat(minimum) || parseFloat(minimumInterestRate) > parseFloat(maximum))
                result = false;
        }
        else
            result = false;

        // Maximum Interest Rate
        if (isNaN(maximumInterestRate) === false) {
            minimum = parseFloat($('#maximum-interest-rate').attr('min'));
            maximum = parseFloat($('#maximum-interest-rate').attr('max'));

            if (parseFloat(maximumInterestRate) < parseFloat(minimum) || parseFloat(maximumInterestRate) > parseFloat(maximum))
                result = false;
        }
        else
            result = false;

        // Fine Interest
        if (enableLoanFineInterestParameter === true) {
            debugger;

            if ($('#interest-method-id').prop('selectedIndex') < 1)
                result = false;

            if ($('#general-ledger-id-fine-parameter').prop('selectedIndex') < 1)
                result == false;

            if (selectedInterestMethodType !== FIXED_FLAT) {
                if ($('#fine-interest-rate-charged-duration-id').prop('selectedIndex') < 1)
                    result = false;

                if ($('#fine-days-in-year-id').prop('selectedIndex') < 1)
                    result = false;

                if ($('#fine-lending-repayments-interest-calculation-id').prop('selectedIndex') < 1)
                    result = false;
            }

            // Fine On Days 
            if ($('#fine-days-input').hasClass('d-none') === false) {
                let fineDays = parseInt($('#fine-days').val());

                if (isNaN(fineDays) === false) {
                    //Fine Days 
                    minimum = parseInt($('#fine-days').attr('min'));
                    maximum = parseInt($('#fine-days').attr('max'));

                    if (parseInt(fineDays) < parseInt(minimum) || parseInt(fineDays) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Fine On Missed Installment
            if ($('#fine-on-missed-installment-input').hasClass('d-none') === false) {
                let numberOfMissedInstallment = parseInt($('#number-of-missed-installment').val());

                if (isNaN(numberOfMissedInstallment) === false) {
                    //Number Of Missed Installment
                    minimum = parseInt($('#number-of-missed-installment').attr('min'));
                    maximum = parseInt($('#number-of-missed-installment').attr('max'));

                    if (parseInt(numberOfMissedInstallment) < parseInt(minimum) || parseInt(numberOfMissedInstallment) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Rate Of Fine Interest Unit Radio Button
            if ($('.rate-of-fine-interest-unit:checked').length === 0) {
                result = false;
            }

            // Fine Interest Rate
            if (isNaN(rateOfFineInterest) === false) {
                minimum = parseFloat($('#rate-of-fine-interest').attr('min'));
                maximum = parseFloat($('#rate-of-fine-interest').attr('max'));

                if (parseFloat(rateOfFineInterest) < parseFloat(minimum) || parseFloat(rateOfFineInterest) > parseFloat(maximum))
                    result = false;
            }
            else {
                result = false;
            }
        }

        // Interest Provision
        if (enableLoanInterestProvisionParameter === true) {
            // General Ledger Of Interest Provision
            if ($('#general-ledger-id-interest-provision-parameter').prop('selectedIndex') < 1) {
                result = false;
            }

            // Interest Calculation Provision interest-provision-calculation-frequency-id
            if ($('#interest-calculation-freaquency-id').prop('selectedIndex') < 1) {
                result = false;
            }
        }

        if (result)
            $('#loan-interest-accordion-title-error').addClass('d-none');
        else
            $('#loan-interest-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 17. Loan Repayment Accordion Input Validation
    function IsValidLoanRepaymentAccordionInputs() {
        debugger;
        let minimumNumberOfInstallments = parseInt($('#minimum-number-of-installments').val());
        let maximumNumberOfInstallments = parseInt($('#maximum-number-of-installments').val());
        let defaultNumberOfInstallments = parseInt($('#default-number-of-installments').val());
        let principalRoundingBy = parseInt($('#principal-rounding-by').val());
        let roundPrincipal = $('.rounding-of-principal:checked').val();
        let interestRoundingBy = parseInt($('#interest-rounding-by').val());
        let roundInterest = $('.rounding-of-interest:checked').val();
        let roundSchedule = $('.rounding-of-repayment-schedule:checked').next('label').text();
        let minimumDaysConstraintsForFirstDueDate = parseInt($('#minimum-days-constraints-for-first-due-date').val());
        let maximumDaysConstraintsForFirstDueDate = parseInt($('#maximum-days-constraints-for-first-due-date').val());
        let defaultDaysConstraintsForFirstDueDate = parseInt($('#default-days-constraints-for-first-due-date').val());

        result = true;

        if ($('#installment-repayment-schedule-accordions').hasClass('d-none') === false) {
            // Repayment Schedule Method
            if ($('.repayment-scheduling-method:checked').length === 0) {
                result = false;
            }

            // Payment Interval Method
            if ($('.payment-interval-method:checked').length === 0) {
                result = false;
            }

            // Repayment Interval Frequency Dropdown
            if (parseInt($('#repayment-interval-frequency-id').prop('selectedIndex')) < 1) {
                result = false;
            }

            // Short Month Handling Method
            if ($('.short-month-handling-method:checked').length === 0) {
                result = false;
            }

            // Minimum Number Of Installment
            if (isNaN(minimumNumberOfInstallments) === false) {
                minimum = parseInt($('#minimum-number-of-installments').attr('min'));
                maximum = parseInt($('#minimum-number-of-installments').attr('max'));

                if (parseInt(minimumNumberOfInstallments) < parseInt(minimum) || parseInt(minimumNumberOfInstallments) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Maximum Number Of Installment
            if (isNaN(maximumNumberOfInstallments) === false) {
                minimum = parseInt($('#maximum-number-of-installments').attr('min'));
                maximum = parseInt($('#maximum-number-of-installments').attr('max'));

                if (parseInt(maximumNumberOfInstallments) < parseInt(minimum) || parseInt(maximumNumberOfInstallments) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Default Number Of Installment
            if (isNaN(defaultNumberOfInstallments) === false) {
                minimum = parseInt($('#default-number-of-installments').attr('min'));
                maximum = parseInt($('#default-number-of-installments').attr('max'));

                if (parseInt(defaultNumberOfInstallments) < parseInt(minimum) || parseInt(defaultNumberOfInstallments) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Grace Period
            if ($('.grace-period:checked').length === 0) {
                result = false;
            }

            // Rounding Of Repayment Amount
            if ($('.rounding-of-repayment-schedule:checked').length === 0) {
                result = false;
            }

            // Rounding Of Principal
            if ($('.rounding-of-principal:checked').length > 0) {
                if (roundPrincipal !== NO_ROUNDING) {
                    minimum = parseInt($('#principal-rounding-by').attr('min'));
                    maximum = parseInt($('#principal-rounding-by').attr('max'));

                    if (parseInt(principalRoundingBy) < parseInt(minimum) || parseInt(principalRoundingBy) > parseInt(maximum)) {
                        result = false;
                    }
                }
            }
            else {
                result = false;
            }

            // Rounding Of Interest
            if ($('.rounding-of-interest:checked').length > 0) {
                if (roundInterest !== NO_ROUNDING) {
                    minimum = parseInt($('#interest-rounding-by').attr('min'));
                    maximum = parseInt($('#interest-rounding-by').attr('max'));

                    if (parseInt(interestRoundingBy) < parseInt(minimum) || parseInt(interestRoundingBy) > parseInt(maximum)) {
                        result = false;
                    }
                }
            }
            else {
                result = false;
            }

            // Minimum Days Constraints For First Due Date
            if (isNaN(minimumDaysConstraintsForFirstDueDate) === false) {
                minimum = parseInt($('#minimum-days-constraints-for-first-due-date').attr('min'));
                maximum = parseInt($('#minimum-days-constraints-for-first-due-date').attr('max'));

                if (parseInt(minimumDaysConstraintsForFirstDueDate) < parseInt(minimum) || parseInt(minimumDaysConstraintsForFirstDueDate) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Maximum Days Constraints For First Due Date
            if (isNaN(maximumDaysConstraintsForFirstDueDate) === false) {
                minimum = parseInt($('#maximum-days-constraints-for-first-due-date').attr('min'));
                maximum = parseInt($('#maximum-days-constraints-for-first-due-date').attr('max'));

                if (parseInt(maximumDaysConstraintsForFirstDueDate) < parseInt(minimum) || parseInt(maximumDaysConstraintsForFirstDueDate) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Default Days Constraints For First DueDate
            if (isNaN(defaultDaysConstraintsForFirstDueDate) === false) {
                minimum = parseInt($('#default-days-constraints-for-first-due-date').attr('min'));
                maximum = parseInt($('#default-days-constraints-for-first-due-date').attr('max'));

                if (parseInt(defaultDaysConstraintsForFirstDueDate) < parseInt(minimum) || parseInt(defaultDaysConstraintsForFirstDueDate) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Repayment Fall On Non Working Day
            if ($('.repayment-fall-on-non-working-day:checked').length === 0) {
                result = false;
            }

            // Prepayment Recalculation Method
            if ($('.pre-payment-recalculation-method:checked').length === 0) {
                result = false;
            }
        }

        if (result)
            $('#loan-repayment-accordion-title-error').addClass('d-none');
        else
            $('#loan-repayment-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 18. Loan Distributor Accordion Input Validation
    function IsValidLoanDistributorAccordionInputs() {
        // Advance
        let enableAdvance = $('#enable-advance-distributor').is(':checked') ? true : false;
        let minimumAdvanceLimit = parseFloat($('#minimum-advance-limit').val());
        let maximumAdvanceLimit = parseFloat($('#maximum-advance-limit').val());

        // Distributor Interest Rate
        let enableDistributorInterestRate = $('#enable-distributor-interest-rate').is(':checked') ? true : false;
        let minimumDistributorInterestRate = parseFloat($('#minimum-distributor-interest-rate').val());
        let maximumDistributorInterestRate = parseFloat($('#maximum-distributor-interest-rate').val());
        let defaultDistributorInterestRate = parseFloat($('#default-distributor-interest-rate').val());

        result = true;

        if ($('#enable-distributor').is(':checked')) {
            // Advance
            if (enableAdvance === true) {
                // Minimum Number Of Installments
                if (isNaN(minimumAdvanceLimit) === false) {
                    minimum = parseFloat($('#minimum-advance-limit').attr('min'));
                    maximum = parseFloat($('#minimum-advance-limit').attr('max'));

                    if (parseFloat(minimumAdvanceLimit) < parseFloat(minimum) || parseFloat(minimumAdvanceLimit) > parseFloat(maximum))
                        result = false;
                }
                else
                    result = false;

                // Maximum Number Of Installments
                if (isNaN(maximumAdvanceLimit) === false) {
                    minimum = parseFloat($('#maximum-advance-limit').attr('min'));
                    maximum = parseFloat($('#maximum-advance-limit').attr('max'));

                    if (parseFloat(maximumAdvanceLimit) < parseFloat(minimum) || parseFloat(maximumAdvanceLimit) > parseFloat(maximum))
                        result = false;
                }
                else
                    result = false;
            }

            if (enableDistributorInterestRate === true) {
                // Minimum Number Of Distributor
                if (isNaN(minimumDistributorInterestRate) === false) {
                    minimum = parseFloat($('#minimum-distributor-interest-rate').attr('min'));
                    maximum = parseFloat($('#minimum-distributor-interest-rate').attr('max'));

                    if (parseFloat(minimumDistributorInterestRate) < parseFloat(minimum) || parseFloat(minimumDistributorInterestRate) > parseFloat(maximum))
                        result = false;
                }
                else
                    result = false;

                // Maximum Number Of Distributors
                if (isNaN(maximumDistributorInterestRate) === false) {
                    minimum = parseFloat($('#maximum-distributor-interest-rate').attr('min'));
                    maximum = parseFloat($('#maximum-distributor-interest-rate').attr('max'));

                    if (parseFloat(maximumDistributorInterestRate) < parseFloat(minimum) || parseFloat(maximumDistributorInterestRate) > parseFloat(maximum))
                        result = false;
                }
                else
                    result = false;

                // Default Number Of Distributor
                if (isNaN(defaultDistributorInterestRate) === false) {
                    minimum = parseFloat($('#default-distributor-interest-rate').attr('min'));
                    maximum = parseFloat($('#default-distributor-interest-rate').attr('max'));

                    if (parseFloat(defaultDistributorInterestRate) < parseFloat(minimum) || parseFloat(defaultDistributorInterestRate) > parseFloat(maximum))
                        result = false;
                }
                else
                    result = false;
            }

        }

        if (result)
            $('#distributor-accordion-title-error').addClass('d-none');
        else
            $('#distributor-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 19. Gold Loan Accordion Input Validation
    function IsValidGoldLoanAccordionInputs() {
        debugger;
        // Get input values and parse them as floats
        let minLtvRatio = parseInt($('#minimum-ltv-ratio').val());
        let maxLtvRatio = parseInt($('#maximum-ltv-ratio').val());
        let minGoldPhoto = parseInt($('#minimum-gold-photo').val());
        let maxGoldPhoto = parseInt($('#maximum-gold-photo').val());
        let goldInsuranceText = $('.gold-insurance:checked').next('label').text();
        let goldInsurance = $('.gold-insurance:checked').val();
        let goldLocalStoragePath = $('#gold-photo-upload-ls-path').val();
        let goldEnableDb = $('#gold-photo-upload-dbts').is(':checked');
        let goldEnableLocalStorage = $('#gold-photo-upload-lsts').is(':checked');
        let maximumFileSizeDB = parseInt($('#gold-photo-maximum-file-size-db').val());
        let maximumFileSizeLS = parseInt($('#gold-photo-maximum-file-size-ls').val());
        let goldPhotoUploadText = $('.gold-photo-upload:checked').next('label').text();
        let goldPhotoUpload = $('.gold-photo-upload:checked').val();
        let enableGoldPhoto = $('#enable-gold-photo').is(':checked');
        let goldAllowFileFormatLS = $('#gold-photo-allowed-file-format-ls').val();
        let goldPhotoAllowFileFormatDB = $('#gold-photo-allowed-file-format-db').val();
        let enableSuperValuation = $('#enable-gold-super-valuation').is(':checked');
        let superValuationsInYear = parseInt($('#gold-super-valuations-year').val());
        let timePeriodSuperValuations = Math.round(parseInt($('#gold-time-period-between-two-super-valuations').val()));

        result = true;

        if ($('#gold-loan-card').hasClass('d-none') === false) {
            // Check if any input is empty or not within specified range
            //Minimum Ltv Ratio
            if (isNaN(minLtvRatio) === false) {
                minimum = parseInt($('#minimum-ltv-ratio').attr('min'));
                maximum = parseInt($('#minimum-ltv-ratio').attr('max'));

                if (parseInt(minLtvRatio) < parseInt(minimum) || parseInt(minLtvRatio) > parseInt(maximum))
                    result = false;
            }
            else {
                result = false;
            }

            //Maximum Ltv Ratio
            if (isNaN(maxLtvRatio) === false) {
                minimum = parseInt($('#maximum-ltv-ratio').attr('min'));
                maximum = parseInt($('#maximum-ltv-ratio').attr('max'));

                // Set Default To 1 If Entered Value Is Zero
                if (parseInt(maxLtvRatio) === 0) {
                    maxLtvRatio = 1;
                    $('#maximum-ltv-ratio').val(maxLtvRatio);
                }

                if (parseInt(maxLtvRatio) < parseInt(minimum) || parseInt(maxLtvRatio) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Gold Insurance
            if ($('.gold-insurance:checked').length === 0) {
                result = false;
                $('#gold-insurance-error').removeClass('d-none');
            }
            else {
                $('#gold-insurance-error').addClass('d-none');
            }

            // Super Valuation
            if (enableSuperValuation) {
                if (isNaN(superValuationsInYear) === false) {
                    minimum = parseInt($('#gold-super-valuations-year').attr('min'));
                    maximum = parseInt($('#gold-super-valuations-year').attr('max'));

                    if (parseInt(superValuationsInYear) < parseInt(minimum) || parseInt(superValuationsInYear) > parseInt(maximum)) {
                        result = false;
                        $('#gold-time-period-between-two-super-valuations').val(0);
                    }
                }
                else {
                    result = false;
                    $('#gold-super-valuations-year-error').removeClass('d-none');
                }

                // Maximum Time Period Between Two Super Vaulations
                if (isNaN(timePeriodSuperValuations) === false) {
                    minimum = parseInt($('#gold-time-period-between-two-super-valuations').attr('min'));
                    maximum = parseInt($('#gold-time-period-between-two-super-valuations').attr('max'));

                    if (parseInt(timePeriodSuperValuations) < parseInt(minimum) || parseInt(timePeriodSuperValuations) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }


            // Gold Photo
            if (enableGoldPhoto) {
                if ($('.gold-photo-upload:checked').length === 0) {
                    result = false;
                    $('#gold-photo-upload-error').removeClass('d-none');
                }
                else {
                    $('#gold-photo-upload-error').addClass('d-none');
                }

                if (goldPhotoUpload === 'M' || goldPhotoUpload === 'O') {
                    $('#gold-photo-upload-error').addClass('d-none');

                    // Check Any One Option  Enabled (Database / Local Storage)
                    if (goldEnableDb === true || goldEnableLocalStorage === true) {
                        $('#gold-photo-upload-required-error').addClass('d-none')

                        // Check If Gold Upload In DB Is Enabled
                        if (goldEnableDb) {
                            //Gold Photo Allowed File Formats For Database
                            if (goldPhotoAllowFileFormatDB === '') {
                                result = false;
                            }

                            //Maximum File Size For Database
                            if (isNaN(maximumFileSizeDB) === false) {
                                minimum = parseInt($('#gold-photo-maximum-file-size-db').attr('min'));
                                maximum = parseInt($('#gold-photo-maximum-file-size-db').attr('max'));

                                if (parseInt(maximumFileSizeDB) < parseInt(minimum) || parseInt(maximumFileSizeDB) > parseInt(maximum)) {
                                    result = false;
                                }
                            }
                            else {
                                result = false;
                            }
                        }

                        // Check If Gold Photo Upload In Local storage Is Enabled
                        if (goldEnableLocalStorage) {
                            if (goldLocalStoragePath === '') {
                                result = false;
                            }

                            //Gold Photo Allowed File Formats For Local Storage
                            if (goldAllowFileFormatLS === '') {
                                result = false;
                            }

                            //Maximum File Size For Local Storage
                            if (isNaN(maximumFileSizeLS) === false) {
                                minimum = parseInt($('#gold-photo-maximum-file-size-ls').attr('min'));
                                maximum = parseInt($('#gold-photo-maximum-file-size-ls').attr('max'));

                                if (parseInt(maximumFileSizeLS) < parseInt(minimum) || parseInt(maximumFileSizeLS) > parseInt(maximum)) {
                                    result = false;
                                }
                            }
                            else {
                                result = false;
                            }
                        }
                    }
                    else {
                        result = false;
                        $('#gold-photo-upload-required-error').removeClass('d-none');
                    }

                }
                else {
                    $('#gold-photo-upload-error').removeClass('d-none');
                }

                //Min Gold photo
                if (isNaN(minGoldPhoto) === false) {
                    minimum = parseInt($('#minimum-gold-photo').attr('min'));
                    maximum = parseInt($('#minimum-gold-photo').attr('max'));

                    if (parseInt(minGoldPhoto) < parseInt(minimum) || parseInt(minGoldPhoto) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                //Max Gold Photo
                if (isNaN(maxGoldPhoto) === false) {
                    minimum = parseInt($('#maximum-gold-photo').attr('min'));
                    maximum = parseInt($('#maximum-gold-photo').attr('max'));

                    if (parseInt(maxGoldPhoto) < parseInt(minimum) || parseInt(maxGoldPhoto) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }
        }

        if (result) {
            $('#gold-photo-upload-error').addClass('d-none');
            $('#gold-photo-upload-required-error').addClass('d-none');
            $('#gold-loan-accordion-title-error').addClass('d-none');
        }
        else {
            $('#gold-loan-accordion-title-error').removeClass('d-none');
        }

        return result;
    }

    // 20. Payment Reminder Accordion Input Validation
    function IsValidPaymentReminderAccordionInputs() {
        debugger;
        // Enable Payment Due Reminder
        let enablePaymentDueReminder = $('#enable-payment-due-reminder').is(':checked') ? true : false;
        let startDaysBeforePaymentDueDate = parseInt($('#start-days-before-payment-due-date').val());
        let occursEveryDayForDueReminder = parseInt($('#every-day-for-due-reminder').val());
        let startDaysAfterPaymentDueDate = parseInt($('#start-days-after-payment-due-date').val());

        // Enable Missed Payment Reminder
        let enableMissedPaymentReminder = $('#enable-missed-payment-reminder').is(':checked') ? true : false;
        let everyDayMissedPaymentReminder = parseInt($('#every-day-missed-payment-reminder').val());
        let maximumumMissedPaymentReminders = parseInt($('#maximumum-missed-payment-reminders').val());

        // Multiple Disbursement
        let enableOverduesPaymentReminder = $('#enable-overdues-payment-reminder').is(':checked') ? true : false;
        let startDaysAfterOverduePaymentDate = parseInt($('#start-days-after-overdue-payment-date').val());
        let everyDayOverduePaymentReminder = parseInt($('#every-day-overdue-payment-reminder').val());
        let maximumumOverduePaymentReminders = parseInt($('#maximum-overdue-payment-reminders').val());

        //Recovery Agent
        let enableNPADeclarationReminder = $('#enable-npa-declaration-reminder').is(':checked') ? true : false;
        let startDaysAfterNPADeclarationDate = parseInt($('#start-days-after-npa-declaration').val());
        let everyDayForNPADeclarationReminder = parseInt($('#every-day-npa-declaration-reminder').val());
        let maximumumNPADeclarationReminders = parseInt($('#maximum-npa-declaration-reminders').val());

        result = true;

        // Payment Reminder
        if (enablePaymentDueReminder === true) {
            // Start Days Before Payment 
            if (isNaN(startDaysBeforePaymentDueDate) === false) {
                minimum = parseInt($('#start-days-before-payment-due-date').attr('min'));
                maximum = parseInt($('#start-days-before-payment-due-date').attr('max'));

                if (parseInt(startDaysBeforePaymentDueDate) < parseInt(minimum) || parseInt(startDaysBeforePaymentDueDate) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Occurs Every Day For Due Reminder
            if (isNaN(occursEveryDayForDueReminder) === false) {
                minimum = parseInt($('#every-day-for-due-reminder').attr('min'));
                maximum = parseInt($('#every-day-for-due-reminder').attr('max'));

                if (parseInt(occursEveryDayForDueReminder) < parseInt(minimum) || parseInt(occursEveryDayForDueReminder) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Start Days After Payment 
            if (isNaN(startDaysAfterPaymentDueDate) === false) {
                minimum = parseInt($('#start-days-after-payment-due-date').attr('min'));
                maximum = parseInt($('#start-days-after-payment-due-date').attr('max'));

                if (parseInt(startDaysAfterPaymentDueDate) < parseInt(minimum) || parseInt(startDaysAfterPaymentDueDate) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;
        }

        // Missed Payment Reminder
        if (enableMissedPaymentReminder === true) {
            if (isNaN(everyDayMissedPaymentReminder) === false) {
                minimum = parseInt($('#every-day-missed-payment-reminder').attr('min'));
                maximum = parseInt($('#every-day-missed-payment-reminder').attr('max'));

                if (parseInt(everyDayMissedPaymentReminder) < parseInt(minimum) || parseInt(everyDayMissedPaymentReminder) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            if (isNaN(maximumumMissedPaymentReminders) === false) {
                // timePeriodForNewCustomerFlag 
                minimum = parseInt($('#maximumum-missed-payment-reminders').attr('min'));
                maximum = parseInt($('#maximumum-missed-payment-reminders').attr('max'));

                if (parseInt(maximumumMissedPaymentReminders) < parseInt(minimum) || parseInt(maximumumMissedPaymentReminders) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;
        }

        // Overdues Payment Reminder
        if (enableOverduesPaymentReminder === true) {
            // Start Days After Overdue Payment Date
            if (isNaN(startDaysAfterOverduePaymentDate) === false) {
                minimum = parseInt($('#start-days-after-overdue-payment-date').attr('min'));
                maximum = parseInt($('#start-days-after-overdue-payment-date').attr('max'));

                if (parseInt(startDaysAfterOverduePaymentDate) < parseInt(minimum) || parseInt(startDaysAfterOverduePaymentDate) > parseInt(maximum))
                    result = false;
            }

            // Every Day After Overdue Payment Date
            if (isNaN(everyDayOverduePaymentReminder) === false) {
                minimum = parseInt($('#every-day-overdue-payment-reminder').attr('min'));
                maximum = parseInt($('#every-day-overdue-payment-reminder').attr('max'));

                if (parseInt(everyDayOverduePaymentReminder) < parseInt(minimum) || parseInt(everyDayOverduePaymentReminder) > parseInt(maximum))
                    result = false;
            }

            // Start Days After Overdue Payment Date
            if (isNaN(maximumumOverduePaymentReminders) === false) {
                minimum = parseInt($('#maximum-overdue-payment-reminders').attr('min'));
                maximum = parseInt($('#maximum-overdue-payment-reminders').attr('max'));

                if (parseInt(maximumumOverduePaymentReminders) < parseInt(minimum) || parseInt(maximumumOverduePaymentReminders) > parseInt(maximum))
                    result = false;
            }
            else {
                result = false;
            }
        }

        // NPA Declaration
        if (enableNPADeclarationReminder === true) {
            // Days After NPA Declaration Date 
            if (isNaN(startDaysAfterNPADeclarationDate) === false) {
                minimum = parseInt($('#start-days-after-npa-declaration').attr('min'));
                maximum = parseInt($('#start-days-after-npa-declaration').attr('max'));

                if (parseInt(startDaysAfterNPADeclarationDate) < parseInt(minimum) || parseInt(startDaysAfterNPADeclarationDate) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Minimum Loan Amount For Individual
            if (isNaN(everyDayForNPADeclarationReminder) === false) {
                minimum = parseInt($('#every-day-npa-declaration-reminder').attr('min'));
                maximum = parseInt($('#every-day-npa-declaration-reminder').attr('max'));

                if (parseInt(everyDayForNPADeclarationReminder) < parseInt(minimum) || parseInt(everyDayForNPADeclarationReminder) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Maximum Loan Amount For Individual
            if (isNaN(maximumumNPADeclarationReminders) === false) {
                minimum = parseInt($('#maximum-npa-declaration-reminders').attr('min'));
                maximum = parseInt($('#maximum-npa-declaration-reminders').attr('max'));

                if (parseInt(maximumumNPADeclarationReminders) < parseInt(minimum) || parseInt(maximumumNPADeclarationReminders) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;
        }

        if (result)
            $('#payment-reminder-accordion-title-error').addClass('d-none');
        else
            $('#payment-reminder-accordion-title-error').removeClass('d-none');

        return result;
    }

    // 21. Cash Credit Loan
    function IsValidCashCreditLoanAccordianInputs() {
        debugger;

        let sanctionLoanAndTurnOverProportion = parseInt($('#sanction-loan-and-turn-over-proportion').val());
        let marginBetweenStockAndWithdrawal = parseFloat($('#margin-between-stock-and-withdrawal').val());
        let balanceConfirmationCertificateTimePeriod = parseInt($('#balance-confirmation-certificate-time-period').val());
        let pastFinancialYearStatements = parseInt($('#past-financial-year-statements').val());
        let pastIncomeTaxReturnStatements = parseInt($('#past-income-tax-return-statements').val());
        let pastAssesmentOrders = parseInt($('#past-assesment-orders').val());
        let previousYearTurnOverText = $('.previous-year-turn-over:checked').next('label').text();
        let previousSecondYearTurnOverText = $('.second-year-turn-over:checked').next('label').text();
        let previousThirdYearTurnOverText = $('.third-year-turn-over:checked').next('label').text();
        let previousYearGrossProfitMarginText = $('.previous-year-gross-profit:checked').next('label').text();
        let previousSecondYearGrossProfitMarginText = $('.second-year-gross-profit:checked').next('label').text();
        let previousThirdYearGrossProfitMarginText = $('.third-year-gross-profit:checked').next('label').text();
        let previousYearNetProfitMarginText = $('.previous-year-net-profit:checked').next('label').text();
        let previousSecondYearNetProfitMarginText = $('.second-year-net-profit:checked').next('label').text();
        let previousThirdYearNetProfitMarginText = $('.third-year-net-profit:checked').next('label').text();
        let debtEquityRatioText = $('.debt-equity-ratio:checked').next('label').text();
        let workingCapitalCycleText = $('.working-capital-cycle:checked').next('label').text();

        result = true;

        if ($('#cash-credit-loan-card').hasClass('d-none') === false) {
            // Check if any input is empty or not within specified range
            // Sanction Loan And TurnOver Proportion
            if (isNaN(sanctionLoanAndTurnOverProportion) === false) {
                minimum = parseInt($('#sanction-loan-and-turn-over-proportion').attr('min'));
                maximum = parseInt($('#sanction-loan-and-turn-over-proportion').attr('max'));

                if (parseInt(sanctionLoanAndTurnOverProportion) < parseInt(minimum) || parseInt(sanctionLoanAndTurnOverProportion) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Margin Between Stock And With drawal
            if (isNaN(marginBetweenStockAndWithdrawal) === false) {
                minimum = parseFloat($('#margin-between-stock-and-withdrawal').attr('min'));
                maximum = parseFloat($('#margin-between-stock-and-withdrawal').attr('max'));

                if (parseFloat(marginBetweenStockAndWithdrawal) < parseFloat(minimum) || parseFloat(marginBetweenStockAndWithdrawal) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            // Balance Confirmation Certificate Time Period
            if (isNaN(balanceConfirmationCertificateTimePeriod) === false) {
                minimum = parseInt($('#balance-confirmation-certificate-time-period').attr('min'));
                maximum = parseInt($('#balance-confirmation-certificate-time-period').attr('max'));

                if (parseInt(balanceConfirmationCertificateTimePeriod) < parseInt(minimum) || parseInt(balanceConfirmationCertificateTimePeriod) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Past Financial Year Statements
            if (isNaN(pastFinancialYearStatements) === false) {
                minimum = parseInt($('#past-financial-year-statements').attr('min'));
                maximum = parseInt($('#past-financial-year-statements').attr('max'));

                if (parseInt(pastFinancialYearStatements) < parseInt(minimum) || parseInt(pastFinancialYearStatements) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Past Income Tax Return Statements
            if (isNaN(pastIncomeTaxReturnStatements) === false) {
                minimum = parseInt($('#past-income-tax-return-statements').attr('min'));
                maximum = parseInt($('#past-income-tax-return-statements').attr('max'));

                if (parseInt(pastIncomeTaxReturnStatements) < parseInt(minimum) || parseInt(pastIncomeTaxReturnStatements) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Past Assesment Orders
            if (isNaN(pastAssesmentOrders) === false) {
                minimum = parseInt($('#past-assesment-orders').attr('min'));
                maximum = parseInt($('#past-assesment-orders').attr('max'));

                if (parseInt(pastAssesmentOrders) < parseInt(minimum) || parseInt(pastAssesmentOrders) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            // Previous Year Turn Over
            if ($('.previous-year-turn-over:checked').length === 0) {
                result = false;
                $('#previous-year-turn-over-error').removeClass('d-none');
            }
            else {
                $('#previous-year-turn-over-error').addClass('d-none');
            }

            // Previous Second Year Turn Over
            if ($('.second-year-turn-over:checked').length === 0) {
                result = false;
                $('#second-year-turn-over-error').removeClass('d-none');
            }
            else {
                $('#second-year-turn-over-error').addClass('d-none');
            }

            // Previous Third Year Turn Over
            if ($('.third-year-turn-over:checked').length === 0) {
                result = false;
                $('#third-year-turn-over-error').removeClass('d-none');
            }
            else {
                $('#third-year-turn-over-error').addClass('d-none');
            }

            // Previous Year Gross Profit
            if ($('.previous-year-gross-profit:checked').length === 0) {
                result = false;
                $('#previous-year-gross-profit-error').removeClass('d-none');
            }
            else {
                $('#previous-year-gross-profit-error').addClass('d-none');
            }

            // Previous Second Year Gross Profit
            if ($('.second-year-gross-profit:checked').length === 0) {
                result = false;
                $('#second-year-gross-profit-error').removeClass('d-none');
            }
            else {
                $('#second-year-gross-profit-error').addClass('d-none');
            }

            // Previous Third Year Gross Profit
            if ($('.third-year-gross-profit:checked').length === 0) {
                result = false;
                $('#third-year-gross-profit-error').removeClass('d-none');
            }
            else {
                $('#third-year-gross-profit-error').addClass('d-none');
            }

            // Previous Year Net Profit
            if ($('.previous-year-net-profit:checked').length === 0) {
                result = false;
                $('#previous-year-net-profit-error').removeClass('d-none');
            }
            else {
                $('#previous-year-net-profit-error').addClass('d-none');
            }

            // Previous Second Year Net Profit
            if ($('.second-year-net-profit:checked').length === 0) {
                result = false;
                $('#second-year-net-profit-error').removeClass('d-none');
            }
            else {
                $('#second-year-net-profit-error').addClass('d-none');
            }

            // Previous Third Year Turn Over
            if ($('.third-year-net-profit:checked').length === 0) {
                result = false;
                $('#third-year-net-profit-error').removeClass('d-none');
            }
            else {
                $('#third-year-net-profit-error').addClass('d-none');
            }

            // Debt Equity Ratio
            if ($('.debt-equity-ratio:checked').length === 0) {
                result = false;
                $('#debt-equity-ratio-error').removeClass('d-none');
            }
            else {
                $('#debt-equity-ratio-error').addClass('d-none');
            }

            // Working Capital Cycle
            if ($('.working-capital-cycle:checked').length === 0) {
                result = false;
                $('#working-capital-cycle-error').removeClass('d-none');
            }
            else {
                $('#working-capital-cycle-error').addClass('d-none');
            }
        }

        // Show or hide error message based on result
        if (result)
            $('#cash-credit-loan-parameter-error').addClass('d-none');
        else
            $('#cash-credit-loan-parameter-error').removeClass('d-none');

        return result;
    }

    // 22. Business Loan
    function IsValidBusinessLoanAccordionInputs() {
        debugger;
        let minimumTurnOverAmount = parseFloat($('#minimum-turn-over-amount').val());
        let currentBusinessMinimumAge = parseInt($('#current-business-minimum-age').val());
        let minimumBusinessExperience = parseInt($('#minimum-business-experience').val());
        let captureProfitYears = parseInt($('#capture-previous-profit-making-years').val());
        let minimumAnnualIncome = parseFloat($('#minimum-annual-income').val());

        let result = true;

        if ($('#business-loan-card').hasClass('d-none') === false) {
            //minimum Turn Over Amount
            if (isNaN(minimumTurnOverAmount) === false) {
                minimum = parseFloat($('#minimum-turn-over-amount').attr('min'));
                maximum = parseFloat($('#minimum-turn-over-amount').attr('max'));

                if (parseFloat(minimumTurnOverAmount) < parseFloat(minimum) || parseFloat(minimumTurnOverAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //current Business Minimum Age
            if (isNaN(currentBusinessMinimumAge) === false) {
                minimum = parseInt($('#current-business-minimum-age').attr('min'));
                maximum = parseInt($('#current-business-minimum-age').attr('max'));

                if (parseInt(currentBusinessMinimumAge) < parseInt(minimum) || parseInt(currentBusinessMinimumAge) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //minimum Business Experience
            if (isNaN(minimumBusinessExperience) === false) {
                minimum = parseInt($('#minimum-business-experience').attr('min'));
                maximum = parseInt($('#minimum-business-experience').attr('max'));

                if (parseInt(minimumBusinessExperience) < parseInt(minimum) || parseInt(minimumBusinessExperience) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //capture Profit Years
            if (isNaN(captureProfitYears) === false) {
                minimum = parseInt($('#capture-previous-profit-making-years').attr('min'));
                maximum = parseInt($('#capture-previous-profit-making-years').attr('max'));

                if (parseInt(captureProfitYears) < parseInt(minimum) || parseInt(captureProfitYears) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //minimum Annual Income
            if (isNaN(minimumAnnualIncome) === false) {
                minimum = parseFloat($('#minimum-annual-income').attr('min'));
                maximum = parseFloat($('#minimum-annual-income').attr('max'));

                if (parseFloat(minimumAnnualIncome) < parseFloat(minimum) || parseFloat(minimumAnnualIncome) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        // Show or hide error message based on result
        if (result) {
            $('#business-loan-accordion-error').addClass('d-none');
        }
        else {
            $('#business-loan-accordion-error').removeClass('d-none');
        }

        return result;

    }

    // 23. Loan Against Deposit Accordion Input Validation
    function IsValidLoanAgainstDepositAccordionInputs() {
        debugger;
        let margin = parseFloat($('#margin-loan-against-deposit').val());
        let minimumDepositAgeForPledge = parseInt($('#minimum-deposit-age-pledge').val());
        let minimumDepositMaturityAgeForPledge = parseInt($('#minimum-deposit-maturity-age-pledge').val());
        let minimumAdditionalInterestRate = parseFloat($('#minimum-additional-interest-rate').val());
        let maximumAdditionalInterestRate = parseFloat($('#maximum-additional-interest-rate').val());

        let enableThirdPersonDepositAttachment = $('#enable-third-person-deposit-attachment').is(':checked') ? true : false;

        let minimumAdditionalInterestRateForThirdPersonDeposit = parseFloat($('#minimum-interest-rate-third-person-deposit').val());
        let maximumAdditionalInterestRateForThirdPersonDeposit = parseFloat($('#maximum-interest-rate-third-person-deposit').val());

        result = true;

        if ($('#loan-against-deposit-card').hasClass('d-none') === false) {
            if ($('.deposit-type:checked').length === 0)
                result = false;

            //Margin Loan Against Deposit
            if (isNaN(margin) === false) {
                minimum = parseFloat($('#margin-loan-against-deposit').attr('min'));
                maximum = parseFloat($('#margin-loan-against-deposit').attr('max'));

                if (parseFloat(margin) < parseFloat(minimum) || parseFloat(margin) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;


            //Minimum Deposit Age For Pledge
            if (isNaN(minimumDepositAgeForPledge) === false) {
                minimum = parseInt($('#minimum-deposit-age-pledge').attr('min'));
                maximum = parseInt($('#minimum-deposit-age-pledge').attr('max'));

                if (parseInt(minimumDepositAgeForPledge) < parseInt(minimum) || parseInt(minimumDepositAgeForPledge) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            //Minimum Deposit Maturity Age For Pledge
            if (isNaN(minimumDepositMaturityAgeForPledge) === false) {
                minimum = parseInt($('#minimum-deposit-maturity-age-pledge').attr('min'));
                maximum = parseInt($('#minimum-deposit-maturity-age-pledge').attr('max'));

                if (parseInt(minimumDepositMaturityAgeForPledge) < parseInt(minimum) || parseInt(minimumDepositMaturityAgeForPledge) > parseInt(maximum))
                    result = false;
            }
            else
                result = false;

            //Minimum Additional Interest Rate
            if (isNaN(minimumAdditionalInterestRate) === false) {
                minimum = parseFloat($('#minimum-additional-interest-rate').attr('min'));
                maximum = parseFloat($('#minimum-additional-interest-rate').attr('max'));

                if (parseFloat(minimumAdditionalInterestRate) < parseFloat(minimum) || parseFloat(minimumAdditionalInterestRate) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            //Maximum Additional Interest Rate
            if (isNaN(maximumAdditionalInterestRate) === false) {
                minimum = parseFloat($('#maximum-additional-interest-rate').attr('min'));
                maximum = parseFloat($('#maximum-additional-interest-rate').attr('max'));

                if (parseFloat(maximumAdditionalInterestRate) < parseFloat(minimumAdditionalInterestRate) || parseFloat(maximumAdditionalInterestRate) > parseFloat(maximum))
                    result = false;
            }
            else
                result = false;

            if ($('#interest-calculation-frequency-id').prop('selectedIndex') < 1) {
                result = false;
            }

            if (enableThirdPersonDepositAttachment === true) {
                if (isNaN(minimumAdditionalInterestRateForThirdPersonDeposit) === false) {
                    minimum = parseFloat($('#minimum-interest-rate-third-person-deposit').attr('min'));
                    maximum = parseFloat($('#minimum-interest-rate-third-person-deposit').attr('max'));

                    if (parseFloat(minimumAdditionalInterestRateForThirdPersonDeposit) < parseFloat(minimum) || parseFloat(minimumAdditionalInterestRateForThirdPersonDeposit) > parseFloat(maximum)) {
                        $('#third-person-deposit-error').removeClass('d-none');
                        result = false;
                    }
                }
                else
                    result = false;

                if (isNaN(maximumAdditionalInterestRateForThirdPersonDeposit) === false) {
                    minimum = parseFloat($('#maximum-interest-rate-third-person-deposit').attr('min'));
                    maximum = parseFloat($('#maximum-interest-rate-third-person-deposit').attr('max'));

                    if (parseFloat(maximumAdditionalInterestRateForThirdPersonDeposit) < parseFloat(minimum) || parseFloat(maximumAdditionalInterestRateForThirdPersonDeposit) > parseFloat(maximum))
                        result = false;
                }
                else
                    result = false;
            }
        }

        if (result)
            $('#loan-against-deposit-parameter-error').addClass('d-none');
        else
            $('#loan-against-deposit-parameter-error').removeClass('d-none');

        return result;
    }

    // 24. Home Loan Accordion Input Validation
    function IsValidHomeLoanAccordionInputs() {
        debugger;
        let enableMultipleDisbursement = $('#enable-multiple-disbursement').is(':checked');
        let maximumNumberOfTimeDisbursement = parseInt($('#maximum-number-of-time-disbursement').val());
        let minimumMoratoriumPeriod = parseInt($('#minimum-moratorium-period').val());
        let maximumMoratoriumPeriod = parseInt($('#maximum-moratorium-period').val());
        let minimumhomeLTVRatio = parseInt($('#minimum-ltv-ratio-home').val());
        let maximumhomeLTVRatio = parseInt($('#maximum-ltv-ratio-home').val());
        let collateralInsuranceText = $('.collateral-insurance:checked').next('label').text();
        let collateralInsurance = $('.collateral-insurance:checked').val();

        let result = true;

        if ($('#home-loan-card').hasClass('d-none') === false) {
            //Enable Multiple Disbursement
            if (enableMultipleDisbursement) {
                // maximum Number Of Time Disbursement
                if (isNaN(maximumNumberOfTimeDisbursement) === false) {
                    minimum = parseInt($('#maximum-number-of-time-disbursement').attr('min'));
                    maximum = parseInt($('#maximum-number-of-time-disbursement').attr('max'));

                    if (parseInt(maximumNumberOfTimeDisbursement) < parseInt(minimum) || parseInt(maximumNumberOfTimeDisbursement) > parseInt(maximum))
                        result = false;
                }
                else {
                    result = false;
                }
            }
            //Minimum Moratorium Period
            if (isNaN(minimumMoratoriumPeriod) === false) {
                minimum = parseInt($('#minimum-moratorium-period').attr('min'));
                maximum = parseInt($('#minimum-moratorium-period').attr('max'));

                if (parseInt(minimumMoratoriumPeriod) < parseInt(minimum) || parseInt(minimumMoratoriumPeriod) > parseInt(maximum)) {
                    result = false;
                }

            }
            else {
                result = false;
            }

            //maximum Moratorium Period
            if (isNaN(maximumMoratoriumPeriod) === false) {
                minimum = parseInt($('#maximum-moratorium-period').attr('min'));
                maximum = parseInt($('#maximum-moratorium-period').attr('max'));

                if (parseInt(maximumMoratoriumPeriod) < parseInt(minimum) || parseInt(maximumMoratoriumPeriod) > parseInt(maximum)) {
                    result = false;
                }

            }
            else {
                result = false;
            }

            //Minimum Ltv Ratio
            if (isNaN(minimumhomeLTVRatio) === false) {
                minimum = parseInt($('#minimum-ltv-ratio-home').attr('min'));
                maximum = parseInt($('#minimum-ltv-ratio-home').attr('max'));

                if (parseInt(minimumhomeLTVRatio) < parseInt(minimum) || parseInt(minimumhomeLTVRatio) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //Maximum Ltv Ratio
            if (isNaN(maximumhomeLTVRatio) === false) {
                minimum = parseInt($('#maximum-ltv-ratio-home').attr('min'));
                maximum = parseInt($('#maximum-ltv-ratio-home').attr('max'));

                // Set Default To 1 If Entered Value Is Zero
                if (parseInt(maximumhomeLTVRatio) === 0) {
                    maximumhomeLTVRatio = 1;
                    $('#maximum-ltv-ratio-home').val(maximumhomeLTVRatio);
                }

                if (parseInt(maximumhomeLTVRatio) < parseInt(minimum) || parseInt(maximumhomeLTVRatio) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            if ($('.collateral-insurance:checked').length === 0) {
                result = false;
                $('#collateral-insurance-error').removeClass('d-none');
            }
            else {
                $('#collateral-insurance-error').addClass('d-none');
            }
        }
        if (result) {
            $('#home-loan-accordion-title-error').addClass('d-none');
        }
        else {
            $('#home-loan-accordion-title-error').removeClass('d-none');
        }

        return result;
    }

    // 25. Loan Against Property Accordion
    function IsValidLoanAgainstPropertyAccordionInputs() {
        debugger;
        let minPropertyLTVRatio = parseInt($('#minimum-ltv-ratio-property').val());
        let maxPropertyLTVRatio = parseInt($('#maximum-ltv-ratio-property').val());
        let propertyInsuranceText = $('.property-insurance:checked').next('label').text();
        let propertyInsurance = $('.property-insurance:checked').val();

        let result = true;

        if ($('#loan-against-property-card').hasClass('d-none') === false) {
            //Minimum Ltv Ratio
            if (isNaN(minPropertyLTVRatio) === false) {
                minimum = parseInt($('#minimum-ltv-ratio-property').attr('min'));
                maximum = parseInt($('#minimum-ltv-ratio-property').attr('max'));

                if (parseInt(minPropertyLTVRatio) < parseInt(minimum) || parseInt(minPropertyLTVRatio) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //Maximum Ltv Ratio
            if (isNaN(maxPropertyLTVRatio) === false) {
                minimum = parseInt($('#maximum-ltv-ratio-property').attr('min'));
                maximum = parseInt($('#maximum-ltv-ratio-property').attr('max'));

                // Set Default To 1 If Entered Value Is Zero
                if (parseInt(maxPropertyLTVRatio) == 0) {
                    maxPropertyLTVRatio = 1;
                    $('#maximum-ltv-ratio-property').val(maxPropertyLTVRatio);
                }

                if (parseInt(maxPropertyLTVRatio) < parseInt(minimum) || (maxPropertyLTVRatio) == 0 || parseInt(maxPropertyLTVRatio) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //property Insurance
            if ($('.property-insurance:checked').length === 0) {
                result = false;
                $('#property-insurance-error').removeClass('d-none');
            }
            else {
                $('#property-insurance-error').addClass('d-none');
            }
        }
        if (result) {
            $('#loan-against-property-accordion-title-error').addClass('d-none');
        }
        else {
            $('#loan-against-property-accordion-title-error').removeClass('d-none');
        }

        return result;
    }

    // 26. Education Loan Accordion
    function IsValidEducationLoanAccordionInputs() {

        let minimumFees = parseFloat($('#minimum-fees').val());
        let maximumFees = parseFloat($('#maximum-fees').val());
        result = true;

        if ($('#education-card').hasClass('d-none') === false) {

            // minimum Fees
            if (isNaN(minimumFees) === false) {
                minimum = parseFloat($('#minimum-fees').attr('min'));
                maximum = parseFloat($('#minimum-fees').attr('max'));

                if (parseFloat(minimumFees) < parseFloat(minimum) || parseFloat(minimumFees) > parseFloat(maximum))
                    result = false;
            }
            else {
                result = false;
            }

            // maximum Fees
            if (isNaN(maximumFees) === false) {
                minimum = parseFloat($('#maximum-fees').attr('min'));
                maximum = parseFloat($('#maximum-fees').attr('max'));

                if (parseFloat(maximumFees) < parseFloat(minimum) || parseFloat(maximumFees) > parseFloat(maximum))
                    result = false;
            }
            else {
                result = false;
            }
        }

        if (result) {
            $('#loan-education-accordion-title-error').addClass('d-none');
        }
        else {
            $('#loan-education-accordion-title-error').removeClass('d-none');
        }
        return result;
    }

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@  scheme Tenure List - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-tenure-list-dt').click(function (event) {

        event.preventDefault();
        SetModalTitle('tenure-list', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-tenure-list-dt').click(function () {

        SetModalTitle('tenure-list', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-tenure-list-dt').data('rowindex');
            id = $('#tenure-list-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs
            $('#tenure', myModal).val(columnValues[1]);

            $('.tenure-unit[value="' + columnValues[2] + '"]').prop('checked', true);

            $('#tenure-text', myModal).val(columnValues[4]);
            $('#note-tenure-list', myModal).val(columnValues[5]);

            // Show Modals
            myModal.modal({ show: true });
        }

        else {
            $('#btn-edit-tenure-list-dt').addClass('read-only');
            $('#tenure-list-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-tenure-list-modal').click(function (event) {
        if (IsValidTenureListDataTableModal()) {
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

            // Error Message In Span
            $('#tenure-list-data-table-error').addClass('d-none');

            HideColumnsTenureListDataTable()

            tenureListDataTable.columns.adjust().draw();


            $('#tenure-list-modal').modal('hide');

            EnableNewOperation('tenure-list');
        }
    });

    // Modal Update Button Event
    $('#btn-update-tenure-list-modal').click(function (event) {
        $('#select-all-tenure-list').prop('checked', false);
        if (IsValidTenureListDataTableModal()) {
            tenureListDataTable.row(selectedRowIndex).data([
                tag,
                tenure,
                tenureUnit,
                tenureUnitText,
                tenureText,
                note,
            ]).draw();

            HideColumnsTenureListDataTable()

            tenureListDataTable.columns.adjust().draw();

            $('#tenure-list-modal').modal('hide');

            EnableNewOperation('tenure-list');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-tenure-list-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-tenure-list tbody input[type="checkbox"]:checked').each(function () {

                    tenureListDataTable.row($('#tbl-tenure-list tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();
                    rowData = $('#btn-delete-tenure-list-dt').data('rowindex');
                    EnableNewOperation('tenure-list');

                    $('#select-all-tenure-list').prop('checked', false);
                    if (!tenureListDataTable.data().any())
                        $('#tenure-list-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-tenure-list').click(function () {
        if ($(this).prop('checked')) {

            $('#tbl-tenure-list tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = tenureListDataTable.row(row).index();

                rowData = (tenureListDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });
                $('#btn-delete-tenure-list-dt').data('rowindex', arr);
                EnableDeleteOperation('tenure-list')
            });
        }
        else {
            EnableNewOperation('tenure-list')
            $('#tbl-tenure-list tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-tenure-list tbody').click('input[type="checkbox"]', function () {
        $('#tbl-tenure-list input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = tenureListDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (tenureListDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('tenure-list');

                    $('#btn-update-tenure-list-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-tenure-list-dt').data('rowindex', rowData);
                    $('#btn-delete-tenure-list-dt').data('rowindex', arr);
                    $('#select-all-tenure-list').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-tenure-list tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('tenure-list');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('tenure-list');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('tenure-list');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-tenure-list').prop('checked', true);
        else
            $('#select-all-tenure-list').prop('checked', false);
    });

    // Validate Tenure List Module
    function IsValidTenureListDataTableModal() {
        let result = true;

        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        tenure = parseInt($('#tenure').val());
        tenureUnit = $('.tenure-unit:checked').val();
        tenureUnitText = $('.tenure-unit:checked').next('label').text();
        tenureText = $('#tenure-text').val();
        note = $('#note-tenure-list').val();

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
            $('#tenure-error').removeClass('d-none');
            result = false;
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
    function HideColumnsTenureListDataTable() {
        tenureListDataTable.column(2).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Document  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-document-dt').click(function (event) {
        event.preventDefault();
        editedDocumentId = '';
        //Validation for Document Unique Id 
        SetDocumentUniqueDropdownList();
        SetModalTitle('document', 'Add');
        ResetFileUpload('document-upload');
    });

    // DataTable Edit Button 
    $('#btn-edit-document-dt').click(function () {
        SetModalTitle('document', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-document-dt').data('rowindex');

            editedDocumentId = columnValues[1];

            SetDocumentUniqueDropdownList();

            id = $('#document-modal').attr('id');
            myModal = $('#' + id).modal();

            $('#document-id', myModal).val(columnValues[1]);

            // Document Type
            $('#document-id', myModal).val(columnValues[1]);

            // Is Required
            $('#is-required', myModal).prop('checked', columnValues[3].toString().toLowerCase() === 'true' ? true : false);

            // Enable Document Upload In Db
            $('#document-upload-dbts', myModal).prop('checked', columnValues[4].toString().toLowerCase() === 'true' ? true : false);

            // Document Allowed File Formats For Db
            $('#document-upload-allowed-file-format-db', myModal).val(columnValues[5].split(','));

            // Maximum File Size For Document Upload In Db
            $('#document-upload-maximum-file-size-db', myModal).val(columnValues[7]);

            // Enable Document Upload In Local Storage
            $('#document-upload-lsts', myModal).prop('checked', columnValues[8].toString().toLowerCase() === 'true' ? true : false);

            // Document Local Storage Path
            $('#document-upload-local-storage-path', myModal).val(columnValues[9]);

            // Document Allowed File Formats For Local Storage
            $('#document-upload-allowed-file-format-ls', myModal).val(columnValues[10].split(','));

            // Maximum File Size For Document Upload In Local Storage
            $('#document-upload-maximum-file-size-ls', myModal).val(columnValues[12]);

            $('#note-scheme-document-type', myModal).val(columnValues[13]);

            SetDocumentUploadInput('document-upload');

            modalObjSelect2.trigger('change');

            // Show Modals
            $('#document-modal').modal('show');
        }
        else {
            $('#btn-edit-document-dt').addClass('read-only');
            $('#document-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-document-modal').click(function (event) {
        if (IsValidDocumentDataTableModal()) {
            row = documentDataTable.row.add([
                tag,
                documentId,
                documentIdText,
                isRequired,
                enableDocumentUploadInDb,
                documentAllowedFileFormatsForDbId,
                documentAllowedFileFormatsForDbText,
                maximumFileSizeForDocumentUploadInDb,
                enableDocumentUploadInLocalStorage,
                documentLocalStoragePath,
                documentAllowedFileFormatsForLocalStorageId,
                documentAllowedFileFormatsForLocalStorageText,
                maximumFileSizeForDocumentUploadInLocalStorage,
                note
            ]).draw();

            // Error Message In Span
            $('#document-type-error').addClass('d-none');

            HideDocumentDataTableColumns();

            documentDataTable.columns.adjust().draw();

            $('#document-modal').modal('hide');

            EnableNewOperation('document');
        }
    });

    // Modal update Button Event
    $('#btn-update-document-modal').click(function (event) {

        $('#select-all-document').prop('checked', false);
        if (IsValidDocumentDataTableModal()) {
            documentDataTable.row(selectedRowIndex).data([
                tag,
                documentId,
                documentIdText,
                isRequired,
                enableDocumentUploadInDb,
                documentAllowedFileFormatsForDbId,
                documentAllowedFileFormatsForDbText,
                maximumFileSizeForDocumentUploadInDb,
                enableDocumentUploadInLocalStorage,
                documentLocalStoragePath,
                documentAllowedFileFormatsForLocalStorageId,
                documentAllowedFileFormatsForLocalStorageText,
                maximumFileSizeForDocumentUploadInLocalStorage,
                note
            ]).draw();

            HideDocumentDataTableColumns();

            documentDataTable.columns.adjust().draw();

            $('#document-modal').modal('hide');

            EnableNewOperation('document');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-document-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-document tbody input[type="checkbox"]:checked').each(function () {
                    documentDataTable.row($('#tbl-document tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-document-dt').data('rowindex');
                    EnableNewOperation('document');

                    SetDocumentUniqueDropdownList();

                    $('#select-all-document').prop('checked', false);
                    if (!documentDataTable.data().any())
                        $('#document-type-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-document').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-document tbody input[type="checkbox"]').each(function (index) {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = documentDataTable.row(row).index();

                rowData = (documentDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-document-dt').data('rowindex', arr);
                EnableDeleteOperation('document');
            });
        }
        else {
            EnableNewOperation('document');

            $('#tbl-document tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-document tbody').click('input[type="checkbox"]', function () {
        $('#tbl-document input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = documentDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (documentDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('document');

                    $('#btn-update-document-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-document-dt').data('rowindex', rowData);
                    $('#btn-delete-document-dt').data('rowindex', arr);
                    $('#select-all-document').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-document tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('document');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('document');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('document');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-document').prop('checked', true);
        else
            $('#select-all-document').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidDocumentDataTableModal() {
        debugger;
        let result = true;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        documentId = $('#document-id option:selected').val();
        documentIdText = $('#document-id option:selected').text();
        isRequired = $('#is-required').is(':checked') ? true : false;
        enableDocumentUploadInDb = $('#document-upload-dbts').is(':checked');
        enableDocumentUploadInLocalStorage = $('#document-upload-lsts').is(':checked');
        note = $('#note-scheme-document-type').val();

        // Set Default Value, If Empty
        if (note == '')
            note = 'None';

        // DocumentTypeId
        if ($('#document-id').prop('selectedIndex') < 1) {
            result = false;
            $('#document-id-error').removeClass('d-none');
        }

        // Check Any One Option  Enabled (Database / Local Storage)
        if (enableDocumentUploadInDb === true || enableDocumentUploadInLocalStorage === true) {
            $('#document-upload-required-error').addClass('d-none');

            // If Database Storage Enabled For Photo Upload
            if (enableDocumentUploadInDb) {
                // DocumentAllowedFileFormatForDBId
                multiSelectCount = parseInt($('#document-upload-allowed-file-format-db option:selected').length);

                if (parseInt(multiSelectCount) > 0) {
                    $('#document-upload-allowed-file-format-db-error').addClass('d-none');

                    documentAllowedFileFormatsForDbId = $('#document-upload-allowed-file-format-db option:selected')
                        .map(function () { return $(this).val(); }).get().join(',');

                    documentAllowedFileFormatsForDbText = $('#document-upload-allowed-file-format-db option:selected')
                        .map(function () { return $(this).text(); }).get().join(',');
                }
                else {
                    result = false;
                    $('#document-upload-allowed-file-format-db-error').removeClass('d-none');
                }

                maximumFileSizeForDocumentUploadInDb = parseInt($('#document-upload-maximum-file-size-db').val());

                // Validate Maximum File Size
                if (isNaN(maximumFileSizeForDocumentUploadInDb) === false) {
                    minimum = parseInt($('#document-upload-maximum-file-size-db').attr('min'));
                    maximum = parseInt($('#document-upload-maximum-file-size-db').attr('max'));

                    if (parseInt(maximumFileSizeForDocumentUploadInDb) < parseInt(minimum) || parseInt(maximumFileSizeForDocumentUploadInDb) > parseInt(maximum)) {
                        result = false;
                        $('#document-upload-maximum-file-size-db-error').removeClass('d-none');
                    }

                }
                else {
                    $('#document-upload-maximum-file-size-db-error').removeClass('d-none');
                }
            }
            else {
                $('#document-upload-allowed-file-format-db-error').addClass('d-none');
                $('#document-upload-maximum-file-size-db-error').addClass('d-none');

                documentAllowedFileFormatsForDbId = 'None';
                documentAllowedFileFormatsForDbText = 'None';
                maximumFileSizeForDocumentUploadInDb = 0;
            }

            // If Local Storage Enabled For Photo Upload
            if (enableDocumentUploadInLocalStorage) {
                // DocumentAllowedFileFormatForLocalStorage
                multiSelectCount = parseInt($('#document-upload-allowed-file-format-ls option:selected').length);

                if (parseInt(multiSelectCount) > 0) {
                    $('#document-upload-allowed-file-format-ls-error').addClass('d-none');

                    documentAllowedFileFormatsForLocalStorageId = $('#document-upload-allowed-file-format-ls option:selected')
                        .map(function () { return $(this).val(); }).get().join(',');

                    documentAllowedFileFormatsForLocalStorageText = $('#document-upload-allowed-file-format-ls option:selected')
                        .map(function () { return $(this).text(); }).get().join(',');
                }
                else {
                    result = false;
                    $('#document-upload-allowed-file-format-ls-error').removeClass('d-none');
                }

                documentLocalStoragePath = $('#document-upload-local-storage-path').val();
                maximumFileSizeForDocumentUploadInLocalStorage = parseInt($('#document-upload-maximum-file-size-ls').val());

                // Validate Maximum File Size
                if (isNaN(maximumFileSizeForDocumentUploadInLocalStorage) === false) {
                    minimum = parseInt($('#document-upload-maximum-file-size-ls').attr('min'));
                    maximum = parseInt($('#document-upload-maximum-file-size-ls').attr('max'));

                    if (parseInt(maximumFileSizeForDocumentUploadInLocalStorage) < parseInt(minimum) || parseInt(maximumFileSizeForDocumentUploadInLocalStorage) > parseInt(maximum)) {
                        result = false;
                        $('#document-upload-maximum-file-size-ls-error').removeClass('d-none');
                    }

                }
                else {
                    result = false;
                    $('#document-upload-maximum-file-size-ls-error').removeClass('d-none');
                }

                // Validate Local Storage Path
                if (documentLocalStoragePath === '') {
                    result = false;
                    $('#document-upload-local-storage-path-error').removeClass('d-none');
                }
            }
            else {
                $('#document-upload-maximum-file-size-ls-error').addClass('d-none');
                $('#document-upload-allowed-file-format-ls-error').addClass('d-none');
                $('#document-upload-local-storage-path-error').addClass('d-none');

                documentAllowedFileFormatsForLocalStorageId = 'None';
                documentAllowedFileFormatsForLocalStorageText = 'None';
                documentLocalStoragePath = 'None';
                maximumFileSizeForDocumentUploadInLocalStorage = 0;
            }
        }
        else {
            result = false;
            $('#document-upload-required-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideDocumentDataTableColumns() {
        documentDataTable.column(1).visible(false);
        documentDataTable.column(5).visible(false);
        documentDataTable.column(10).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme Target Group  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-target-group-dt').click(function (event) {
        event.preventDefault();
        SetModalTitle('target-group', 'Add');
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
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-target-group-dt').click(function (event) {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
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

    //@@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme Report Format - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-report-format-data-table-dt').click(function (event) {

        event.preventDefault();

        editedReportFormatId = '';
        SetReportFormatUniqueDropdownList();

        SetModalTitle('report-format-data-table', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-report-format-data-table-dt').click(function () {

        SetModalTitle('report-format-data-table', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {

            columnValues = $('#btn-edit-report-format-data-table-dt').data('rowindex');

            editedReportFormatId = columnValues[1];

            SetReportFormatUniqueDropdownList();

            id = $('#report-format-data-table-modal').attr('id');
            myModal = $('#' + id).modal();
            // Display Value In Modal Inputs
            $('#report-format-id', myModal).val(columnValues[1]);
            $('#note-Report', myModal).val(columnValues[3]);

            // Show Modals
            myModal.modal({ show: true });
        }

        else {
            $('#btn-edit-report-format-data-table-dt').addClass('read-only');
            $('#report-format-data-table-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-report-format-data-table-modal').click(function (event) {
        if (IsValidReportFormatDataTableModal()) {
            row = reportFormatDataTable.row.add([
                tag,
                reportFormatId,
                reportFormatText,
                note
            ]).draw();

            // Error Message In Span
            $('#report-format-data-table-validation span').html('');

            HideReportFormatDataTableColumns();

            reportFormatDataTable.columns.adjust().draw();


            $('#report-format-data-table-modal').modal('hide');

            EnableNewOperation('report-format-data-table');
        }
    });

    // Modal update Button Event
    $('#btn-update-report-format-data-table-modal').click(function (event) {

        $('#select-all-report-format-data-table').prop('checked', false);

        if (IsValidReportFormatDataTableModal()) {
            reportFormatDataTable.row(selectedRowIndex).data([
                tag,
                reportFormatId,
                reportFormatText,
                note
            ]).draw();


            HideReportFormatDataTableColumns();

            reportFormatDataTable.columns.adjust().draw();

            $('#report-format-data-table-modal').modal('hide');
            EnableNewOperation('report-format-data-table');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-report-format-data-table-dt').click(function (event) {
        isChecked = $('#tbl-report-format-data-table tbody input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-report-format-data-table tbody input[type="checkbox"]:checked').each(function () {
                    reportFormatDataTable.row($('#report-format-data-table tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-report-format-data-table-dt').data('rowindex');
                    EnableNewOperation('report-format-data-table');

                    SetReportFormatUniqueDropdownList();

                    $('#select-all-report-format-data-table').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A CheckBox');
    });

    // Select All
    $('#select-all-report-format-data-table').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-report-format-data-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = reportFormatDataTable.row(row).index();
                rowData = (reportFormatDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-report-format-data-table-dt').data('rowindex', arr);
                EnableDeleteOperation('report-format-data-table');
            });
        }
        else {
            EnableNewOperation('report-format-data-table');
            $('#tbl-report-format-data-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event  
    $('#tbl-report-format-data-table tbody').click('input[type="checkbox"]', function () {
        $('#tbl-report-format-data-table input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = reportFormatDataTable.row(row).index();

                rowData = (reportFormatDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('report-format-data-table');

                $('#btn-update-report-format-data-table-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-report-format-data-table-dt').data('rowindex', rowData);
                $('#btn-delete-report-format-data-table-dt').data('rowindex', arr);
                $('#select-all-select-all-report-format-data-table').data('rowindex', arr);
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-report-format-data-table tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('report-format-data-table');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('report-format-data-table');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('report-format-data-table');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-report-format-data-table').prop('checked', true);
        else
            $('#select-all-report-format-data-table').prop('checked', false);
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

    // Hide Unnecessary Columns
    function HideReportFormatDataTableColumns() {
        reportFormatDataTable.column(1).visible(false);
    }

    //@@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme Notice Schedule  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearNoticeScheduleModalInputs();

    // DataTable Add Button 
    $('#btn-add-notice-schedule-dt').click(function (event) {

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
                note
            ]).draw();

            // Error Message In Span
            HideColumnsNoticeScheduleDataTable();

            noticeScheduleDataTable.columns.adjust().draw();


            $('#notice-schedule-modal').modal('hide');

            EnableNewOperation('notice-schedule');
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
                note
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
            alert('Please Select A CheckBox');
    });

    // Select All Check Box Click Event  
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

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme Loan Charges  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-charges-dt').click(function (event) {
        event.preventDefault();
        editedChargesType = '';
        SetChargesUniqueDropdownList();
        $('#charges-percentage-input').addClass('d-none')
        SetModalTitle('charges', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-charges-dt').click(function () {
        SetModalTitle('charges', 'Edit');
        debugger;
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-charges-dt').data('rowindex');

            editedChargesType = columnValues[1];

            SetChargesUniqueDropdownList();

            id = $("#charges-modal").attr('id');
            myModal = $('#' + id).modal();

            $('#maximum-charges-amount').attr('min', columnValues[8]);
            $('#default-charges-amount').attr('min', columnValues[8]);
            $('#default-charges-amount').attr('max', columnValues[9]);

            // Display Value In Modal Inputs
            $('#charges-type-id', myModal).val(columnValues[1]);
            $('#charges-general-ledger-id', myModal).val(columnValues[3]);
            $('#lending-charges-base-id', myModal).val(columnValues[5]);
            if ((columnValues[6].includes('%') === true)) {
                $('#charges-percentage-input').removeClass('d-none')
            }
            else {
                $('#charges-percentage-input').addClass('d-none')
            }

            $('#charges-percentage', myModal).val(columnValues[7]);
            $('#minimum-charges-amount', myModal).val(columnValues[8]);
            $('#maximum-charges-amount', myModal).val(columnValues[9]);
            $('#default-charges-amount', myModal).val(columnValues[10]);
            $('#is-applicable-tax').prop('checked', columnValues[11].toString().toLowerCase() === 'true' ? true : false);
            $('#is-optional').prop('checked', columnValues[12].toString().toLowerCase() === 'true' ? true : false);
            $('#note-charges', myModal).val(columnValues[13]);

            // Show Modal
            $('#charges-modal').modal('show');
        }
        else {
            $('#btn-edit-charges-dt').addClass('read-only');
            $('#charges-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-charges-modal').click(function (event) {
        debugger;
        if (IsValidChargesDataTableModal()) {
            row = loanChargesDataTable.row.add([
                tag,
                chargesApplyingTypeId,
                chargesApplyingTypeIdText,
                chargesGeneralLedgerId,
                chargesGeneralLedgerText,
                lendingChargesBaseId,
                lendingChargesBaseIdText,
                chargesPercentage,
                minimumChargesAmount,
                maximumChargesAmount,
                defaultChargesAmount,
                isApplicableTax,
                isOptional,
                note
            ]).draw();
            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            // Error Message In Span
            $('#charges-data-table-error').addClass('d-none');

            HideColumnsChargesDataTable();

            loanChargesDataTable.columns.adjust().draw();


            $('#charges-modal').modal('hide');

            EnableNewOperation('charges');
        }
    });

    // Modal Update Button Event
    $('#btn-update-charges-modal').click(function (event) {
        $('#select-all-charges').prop('checked', false);
        if (IsValidChargesDataTableModal()) {
            loanChargesDataTable.row(selectedRowIndex).data([
                tag,
                chargesApplyingTypeId,
                chargesApplyingTypeIdText,
                chargesGeneralLedgerId,
                chargesGeneralLedgerText,
                lendingChargesBaseId,
                lendingChargesBaseIdText,
                chargesPercentage,
                minimumChargesAmount,
                maximumChargesAmount,
                defaultChargesAmount,
                isApplicableTax,
                isOptional,
                note,
            ]).draw();

            HideColumnsChargesDataTable();

            loanChargesDataTable.columns.adjust().draw();

            $('#charges-modal').modal('hide');

            EnableNewOperation('charges');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-charges-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-charges tbody input[type="checkbox"]:checked').each(function () {
                    loanChargesDataTable.row($('#tbl-charges tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();
                    rowData = $('#btn-delete-charges-dt').data('rowindex');
                    EnableNewOperation('charges');

                    SetChargesUniqueDropdownList();

                    $('#select-all-charges').prop('checked', false);
                    if (!loanChargesDataTable.data().any())
                        $('#charges-data-table-error').removeClass('d-none');


                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Other Charges Datatable
    $('#select-all-charges').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-charges tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = loanChargesDataTable.row(row).index();

                rowData = (loanChargesDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-charges-dt').data('rowindex', arr);
                EnableDeleteOperation('charges');
            });
        }
        else {
            EnableNewOperation('charges');
            $('#charges tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-charges tbody').click('input[type="checkbox"]', function () {
        $('#tbl-charges input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = loanChargesDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (loanChargesDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('charges');

                    $('#btn-update-charges-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-charges-dt').data('rowindex', rowData);
                    $('#btn-delete-charges-dt').data('rowindex', arr);
                    $('#select-all-charges').data('rowindex', arr);
                }
            }
            //if (isChecked) {

            //    row = $(this).closest('tr');

            //    selectedRowIndex = loanChargesDataTable.row(row).index();

            //    rowData = (loanChargesDataTable.row(selectedRowIndex).data());
            //    arr.push({ arrayCloumn1: rowData[1] });

            //    EnableEditDeleteOperation('charges');

            //    $('#btn-update-charges-dt').attr('rowindex', selectedRowIndex);
            //    $('#btn-edit-charges-dt').data('rowindex', rowData);
            //    $('#btn-delete-charges-dt').data('rowindex', arr);
            //    $('#select-all-charges').data('rowindex', arr);
            //}
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('charges');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('charges');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('charges');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-charges').prop('checked', true);
        else
            $('#select-all-charges').prop('checked', false);
    });

    // Validate Charges Module
    function IsValidChargesDataTableModal() {
        let result = true;
        debugger;
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        chargesApplyingTypeId = $('#charges-type-id option:selected').val();
        chargesApplyingTypeIdText = $('#charges-type-id option:selected').text();
        chargesGeneralLedgerId = $('#charges-general-ledger-id option:selected').val();
        chargesGeneralLedgerText = $('#charges-general-ledger-id option:selected').text();
        lendingChargesBaseId = $('#lending-charges-base-id option:selected').val();
        lendingChargesBaseIdText = $('#lending-charges-base-id option:selected').text();
        chargesPercentage = parseFloat($('#charges-percentage').val());
        minimumChargesAmount = parseFloat($('#minimum-charges-amount').val());
        maximumChargesAmount = parseFloat($('#maximum-charges-amount').val());
        defaultChargesAmount = parseFloat($('#default-charges-amount').val());
        isApplicableTax = $('#is-applicable-tax').is(':checked') ? 'True' : 'False';
        isOptional = $('#is-optional').is(':checked') ? 'True' : 'False';
        note = $('#note-charges').val();

        // Set Default Value, If Empty
        if (note === '') {
            note = 'None';
        }

        //ChargesApplyingTypeId
        if ($('#charges-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#charges-type-id-error').removeClass('d-none');
        }

        //ChargesGeneralLedgerId
        if ($('#charges-general-ledger-id').prop('selectedIndex') < 1) {
            result = false;
            $('#charges-general-ledger-id-error').removeClass('d-none');
        }

        //LendingChargesBaseId
        if ($('#lending-charges-base-id').prop('selectedIndex') < 1) {
            result = false;
            $('#lending-charges-base-id-error').removeClass('d-none');
        }

        if ($('#charges-percentage-input').hasClass('d-none') === false) {
            // Percentage
            if (isNaN(chargesPercentage) === false) {
                minimum = parseFloat($('#charges-percentage').attr('min'));
                maximum = parseFloat($('#charges-percentage').attr('max'));

                if (parseFloat(chargesPercentage) < parseFloat(minimum) || parseFloat(chargesPercentage) > parseFloat(maximum))
                    result = false;
            }
            else {
                $('#charges-percentage-error').removeClass('d-none');
                result = false;
            }
        }

        // Validate Limit
        if (isNaN(minimumChargesAmount) === false) {
            minimum = parseFloat($('#minimum-charges-amount').attr('min'));
            maximum = parseFloat($('#maximum-charges-amount').attr('max'));

            if (parseFloat(minimumChargesAmount) < parseFloat(minimum) || parseFloat(minimumChargesAmount) > parseFloat(maximum)) {
                $('#minimum-charges-amount-error').removeClass('d-none');
                result = false;
            }
        }
        else {
            $('#minimum-charges-amount-error').removeClass('d-none');
            result = false;
        }

        if (isNaN(maximumChargesAmount) === false) {
            minimum = parseFloat($('#maximum-charges-amount').attr('min'));
            maximum = parseFloat($('#maximum-charges-amount').attr('max'));

            if (parseFloat(maximumChargesAmount) < parseFloat(minimum) || parseFloat(maximumChargesAmount) > parseFloat(maximum)) {
                $('#maximum-charges-amount-error').removeClass('d-none');
                result = false;
            }
        }
        else {
            $('#maximum-charges-amount-error').removeClass('d-none');
            result = false;
        }

        if (isNaN(defaultChargesAmount) === false) {
            minimum = parseFloat($('#default-charges-amount').attr('min'));
            maximum = parseFloat($('#default-charges-amount').attr('max'));

            if (parseFloat(defaultChargesAmount) < parseFloat(minimum) || parseFloat(defaultChargesAmount) > parseFloat(maximum)) {
                $('#default-charges-amount-error').removeClass('d-none');
                result = false;
            }
        }
        else {
            $('#default-charges-amount-error').removeClass('d-none');
            result = false;
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsChargesDataTable() {
        loanChargesDataTable.column(1).visible(false);
        loanChargesDataTable.column(3).visible(false);
        loanChargesDataTable.column(5).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@  Loan Overdues Action - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-loan-overdues-action-dt').click(function (event) {

        event.preventDefault();

        SetModalTitle('loan-overdues-action', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-loan-overdues-action-dt').click(function () {

        SetModalTitle('loan-overdues-action', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-loan-overdues-action-dt').data('rowindex');
            id = $('#loan-overdues-action-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs                                
            $('#minimum-overdues-installment', myModal).val(columnValues[1]);
            $('#maximum-overdues-installment').attr('min', columnValues[2]);

            $('#maximum-overdues-installment', myModal).val(columnValues[2]);
            $('#loan-recovery-action-id', myModal).val(columnValues[3]);
            $('#note-loan-overdues-action', myModal).val(columnValues[5]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-loan-overdues-action-dt').addClass('read-only');
            $('#loan-overdues-action-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-loan-overdues-action-modal').click(function (event) {
        if (IsValidLoanOverduesActionDataTableModal()) {
            row = loanOverduesActionDataTable.row.add([
                tag,
                minimumOverduesInstallment,
                maximumOverduesInstallment,
                loanRecoveryActionId,
                loanRecoveryActionIdText,
                note
            ]).draw();

            // Error Message In Span
            $('#overdues-action-data-table-error').addClass('d-none');

            HideLoanOverduesActionDataTableColumns();

            loanOverduesActionDataTable.columns.adjust().draw();


            $('#loan-overdues-action-modal').modal('hide');

            EnableNewOperation('loan-overdues-action');
        }
    });

    // Modal update Button Event
    $('#btn-update-loan-overdues-action-modal').click(function (event) {

        $('#select-all-loan-overdues-action').prop('checked', false);
        if (IsValidLoanOverduesActionDataTableModal()) {
            loanOverduesActionDataTable.row(selectedRowIndex).data([
                tag,
                minimumOverduesInstallment,
                maximumOverduesInstallment,
                loanRecoveryActionId,
                loanRecoveryActionIdText,
                note
            ]).draw();

            HideLoanOverduesActionDataTableColumns();

            loanOverduesActionDataTable.columns.adjust().draw();

            $('#loan-overdues-action-modal').modal('hide');

            EnableNewOperation('loan-overdues-action');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-loan-overdues-action-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-loan-overdues-action tbody input[type="checkbox"]:checked').each(function () {
                    loanOverduesActionDataTable.row($('#tbl-loan-overdues-action tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-loan-overdues-action-dt').data('rowindex');
                    EnableNewOperation('loan-overdues-action');

                    $('#select-all-loan-overdues-action').prop('checked', false);

                    if (!loanOverduesActionDataTable.data().any())
                        $('#overdues-action-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A CheckBox');
    });

    // Select All Check Box Click Event  
    $('#select-all-loan-overdues-action').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-loan-overdues-action tbody input[type="checkbox"]').each(function (index) {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                rowData = (loanOverduesActionDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-loan-overdues-action-dt').data('rowindex', arr);
                EnableDeleteOperation('loan-overdues-action');
            });
        }
        else {
            EnableNewOperation('loan-overdues-action');

            $('#tbl-loan-overdues-action tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-loan-overdues-action tbody').click('input[type="checkbox"]', function () {
        $('#tbl-loan-overdues-action input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = loanOverduesActionDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (loanOverduesActionDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('loan-overdues-action');

                    $('#btn-update-loan-overdues-action-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-loan-overdues-action-dt').data('rowindex', rowData);
                    $('#btn-delete-loan-overdues-action-dt').data('rowindex', arr);
                    $('#select-all-loan-overdues-action').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-loan-overdues-action tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('loan-overdues-action');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('loan-overdues-action');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('loan-overdues-action');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-loan-overdues-action').prop('checked', true);
        else
            $('#select-all-loan-overdues-action').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidLoanOverduesActionDataTableModal() {
        debugger;
        let result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        minimumOverduesInstallment = parseInt($('#minimum-overdues-installment').val());
        maximumOverduesInstallment = parseInt($('#maximum-overdues-installment').val());
        loanRecoveryActionId = $('#loan-recovery-action-id option:selected').val();
        loanRecoveryActionIdText = $('#loan-recovery-action-id option:selected').text();
        note = $('#note-loan-overdues-action').val();

        if (note === '')
            note = 'None';
        //LoanRecoveryActionId
        if ($('#loan-recovery-action-id').prop('selectedIndex') < 1) {
            result = false;
            $('#loan-recovery-action-id-error').removeClass('d-none');
        }

        // Validate Minimum Overdues Installment
        if (isNaN(minimumOverduesInstallment) === false) {
            minimum = parseInt($('#minimum-overdues-installment').attr('min'));
            maximum = parseInt($('#minimum-overdues-installment').attr('max'));

            if (parseInt(minimumOverduesInstallment) < parseInt(minimum) || parseInt(minimumOverduesInstallment) > parseInt(maximum)) {
                result = false;
                $('#minimum-overdues-installment-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#minimum-overdues-installment-error').removeClass('d-none');
        }

        // Validate Maximum Overdues Installment
        if (isNaN(maximumOverduesInstallment) === false) {
            minimum = parseInt($('#maximum-overdues-installment').attr('min'));
            maximum = parseInt($('#maximum-overdues-installment').attr('max'));

            if (parseInt(maximumOverduesInstallment) < parseInt(minimum) || parseInt(maximumOverduesInstallment) > parseInt(maximum)) {
                result = false;
                $('#maximum-overdues-installment-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-overdues-installment-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideLoanOverduesActionDataTableColumns() {
        loanOverduesActionDataTable.column(3).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ General Ledger - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-general-ledger-dt').click(function (event) {
        debugger;
        event.preventDefault();
        editedGeneralLedgerId = '';
        SetGeneralLedgerUniqueDropdownList();
        SetModalTitle('general-ledger', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-general-ledger-dt').click(function () {
        SetModalTitle('general-ledger', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-general-ledger-dt').data('rowindex');

            editedGeneralLedgerId = columnValues[1];

            SetGeneralLedgerUniqueDropdownList();

            id = $('#general-ledger-modal').attr('id');
            myModal = $('#' + id).modal();

            activationDate = new Date(columnValues[3]);
            expiryDate = new Date(columnValues[4]);
            closeDate = new Date(columnValues[5]);

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
            $('#general-ledger-data-table-error').addClass('d-none');

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
                        $('#general-ledger-data-table-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event  
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
        if (checked.length === 0)
            EnableNewOperation('general-ledger');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
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
        if (note === '')
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

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Business Office - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-business-office-dt').click(function (event) {
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
                businessOfficeIdText,
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
        if (checked.length === 0)
            EnableNewOperation('business-office');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
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

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@  Vehicle Loan - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-vehicle-type-loan-dt').click(function (event) {
        debugger;
        event.preventDefault();
        editedVehicleLoanId = '';
        SetVehicleTypeUniqueDropdownList();
        SetModalTitle('vehicle-type-loan', 'Add');
        ResetFileUpload('vehicle-photo-upload');
    });

    // DataTable Edit Button 
    $('#btn-edit-vehicle-type-loan-dt').click(function () {
        debugger;
        SetModalTitle('vehicle-type-loan', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-vehicle-type-loan-dt').data('rowindex');

            editedVehicleLoanId = columnValues[1];
            SetVehicleTypeUniqueDropdownList();

            id = $('#vehicle-type-loan-modal').attr('id');
            myModal = $('#' + id).modal();
            $('#maximum-number-of-photo-vehicle').attr('min', columnValues[16]);

            $('#vehicle-type-id', myModal).val(columnValues[1]);
            $('#down-payment-percentage-vehicle', myModal).val(columnValues[3]);
            $('#maximum-loan-amount-without-extra-security', myModal).val(columnValues[4]);
            // Display Value In Modal Inputs
            $('.vehicle-photo-upload[value="' + columnValues[5] + '"]').prop('checked', true);
            $('#vehicle-photo-upload-dbts').prop('checked', columnValues[7].toString().toLowerCase() === 'true' ? true : false);
            $('#vehicle-photo-upload-allowed-file-format-db', myModal).val(columnValues[8].split(','));
            $('#vehicle-photo-upload-maximum-file-size-db', myModal).val(columnValues[10]);
            $('#vehicle-photo-upload-lsts', myModal).prop('checked', columnValues[11].toString().toLowerCase() === 'true' ? true : false);

            $('#vehicle-photo-upload-local-storage-path', myModal).val(columnValues[12]);
            $('#vehicle-photo-upload-allowed-file-format-ls', myModal).val(columnValues[13].split(','));
            $('#vehicle-photo-upload-maximum-file-size-ls', myModal).val(columnValues[15]);
            $('#minimum-number-of-photo-vehicle', myModal).val(columnValues[16]);
            $('#maximum-number-of-photo-vehicle', myModal).val(columnValues[17]);
            $('#vehicle-loan-note', myModal).val(columnValues[18]);

            SetDocumentUploadInput('vehicle-photo-upload');

            // To Display Selected Item In Select List
            modalObjSelect2.trigger('change');

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-vehicle-type-loan-dt').addClass('read-only');
            $('#vehicle-type-loan-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-vehicle-type-loan-modal').click(function (event) {
        debugger;
        if (IsValidVehicleTypeLoanDataTableModal()) {
            row = vehicleTypeLoanDataTable.row.add([
                tag,
                vehicleTypeId,
                vehicleTypeIdText,
                downPaymentPercentage,
                maximumLoanAmountWithoutExtraSecurity,
                vehiclePhotoUpload,
                vehiclePhotoUploadText,
                enableVehiclePhotoUploadInDb,
                vehiclePhotoAllowedFileFormatsForDbId,
                vehiclePhotoAllowedFileFormatsForDbText,
                maximumFileSizeForVehiclePhotoUploadInDb,
                enableVehiclePhotoUploadInLocalStorage,
                vehiclePhotoUploadLocalStoragePath,
                vehiclePhotoAllowedFileFormatsForLSId,
                vehiclePhotoAllowedFileFormatsForLSText,
                maximumFileSizeForVehiclePhotoUploadInLocalStorage,
                minimumNumberOfPhoto,
                maximumNumberOfPhoto,
                note,
            ]).draw();

            // Error Message In Span
            $('#vehicle-type-loan-accordion-title-error').addClass('d-none');

            HideColumnsVehicleTypeLoanDataTable();

            vehicleTypeLoanDataTable.columns.adjust().draw();

            $('#vehicle-type-loan-modal').modal('hide');

            EnableNewOperation('vehicle-type-loan');
        }
    });

    // Modal update Button Event
    $('#btn-update-vehicle-type-loan-modal').click(function (event) {
        $('#select-all-vehicle-type-loan').prop('checked', false);
        if (IsValidVehicleTypeLoanDataTableModal()) {
            vehicleTypeLoanDataTable.row(selectedRowIndex).data([
                tag,
                vehicleTypeId,
                vehicleTypeIdText,
                downPaymentPercentage,
                maximumLoanAmountWithoutExtraSecurity,
                vehiclePhotoUpload,
                vehiclePhotoUploadText,
                enableVehiclePhotoUploadInDb,
                vehiclePhotoAllowedFileFormatsForDbId,
                vehiclePhotoAllowedFileFormatsForDbText,
                maximumFileSizeForVehiclePhotoUploadInDb,
                enableVehiclePhotoUploadInLocalStorage,
                vehiclePhotoUploadLocalStoragePath,
                vehiclePhotoAllowedFileFormatsForLSId,
                vehiclePhotoAllowedFileFormatsForLSText,
                maximumFileSizeForVehiclePhotoUploadInLocalStorage,
                minimumNumberOfPhoto,
                maximumNumberOfPhoto,
                note,
            ]).draw();

            HideColumnsVehicleTypeLoanDataTable();

            vehicleTypeLoanDataTable.columns.adjust().draw();

            $('#vehicle-type-loan-modal').modal('hide');

            EnableNewOperation('vehicle-type-loan');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-vehicle-type-loan-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-vehicle-type-loan tbody input[type="checkbox"]:checked').each(function () {
                    vehicleTypeLoanDataTable.row($('#tbl-vehicle-type-loan tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-vehicle-type-loan-dt').data('rowindex');
                    EnableNewOperation('vehicle-type-loan');

                    SetVehicleTypeUniqueDropdownList();

                    $('#select-all-vehicle-type-loan').prop('checked', false);

                    if (vehicleTypeLoanDataTable.data().any() === false)
                        $('#vehicle-type-loan-accordion-title-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select A CheckBox');
    });

    // Select All Check Box Click Event  
    $('#select-all-vehicle-type-loan').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-vehicle-type-loan tbody input[type="checkbox"]').each(function (index) {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                rowData = (vehicleTypeLoanDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-fund-dt').data('rowindex', arr);
                EnableDeleteOperation('vehicle-type-loan')
            });
        }
        else {
            EnableNewOperation('vehicle-type-loan')

            $('#tbl-vehicle-type-loan tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-vehicle-type-loan tbody').click('input[type="checkbox"]', function () {
        $('#tbl-vehicle-type-loan input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = vehicleTypeLoanDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (vehicleTypeLoanDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('vehicle-type-loan');

                    $('#btn-update-vehicle-type-loan-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-vehicle-type-loan-dt').data('rowindex', rowData);
                    $('#btn-delete-vehicle-type-loan-dt').data('rowindex', arr);
                    $('#select-all-vehicle-type-loan').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-vehicle-type-loan tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('vehicle-type-loan');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('vehicle-type-loan');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('vehicle-type-loan');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-vehicle-type-loan').prop('checked', true);
        else
            $('#select-all-vehicle-type-loan').prop('checked', false);
    });

    // Validate Recovery Action Module
    function IsValidVehicleTypeLoanDataTableModal() {
        debugger;
        let result = true;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        vehicleTypeId = $('#vehicle-type-id option:selected').val();
        vehicleTypeIdText = $('#vehicle-type-id option:selected').text();
        downPaymentPercentage = parseFloat($('#down-payment-percentage-vehicle').val());
        maximumLoanAmountWithoutExtraSecurity = parseFloat($('#maximum-loan-amount-without-extra-security').val());
        vehiclePhotoUploadText = $('.vehicle-photo-upload:checked').next('label').text();
        vehiclePhotoUpload = $('.vehicle-photo-upload:checked').val();
        minimumNumberOfPhoto = parseInt($('#minimum-number-of-photo-vehicle').val());
        maximumNumberOfPhoto = parseInt($('#maximum-number-of-photo-vehicle').val());
        note = $('#vehicle-loan-note').val();

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        // Vehicle Type
        if ($('#vehicle-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#vehicle-type-id-error').removeClass('d-none');
        }

        //DownPaymentPercentage
        if (isNaN(downPaymentPercentage) === false) {
            minimum = parseFloat($('#down-payment-percentage-vehicle').attr('min'));
            maximum = parseFloat($('#down-payment-percentage-vehicle').attr('max'));

            if (parseFloat(downPaymentPercentage) < parseFloat(minimum) || parseFloat(downPaymentPercentage) > parseFloat(maximum)) {
                result = false;
                $('#down-payment-percentage-vehicle-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#down-payment-percentage-vehicle-error').removeClass('d-none');
        }

        // MaximumLoanAmountWithoutExtraSecurity
        if (isNaN(maximumLoanAmountWithoutExtraSecurity) === false) {
            minimum = parseFloat($('#maximum-loan-amount-without-extra-security').attr('min'));
            maximum = parseFloat($('#maximum-loan-amount-without-extra-security').attr('max'));

            if (parseFloat(maximumLoanAmountWithoutExtraSecurity) < parseFloat(minimum) || parseFloat(maximumLoanAmountWithoutExtraSecurity) > parseFloat(maximum)) {
                result = false;
                $('#maximum-loan-amount-without-extra-security-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-loan-amount-without-extra-security-error').removeClass('d-none');
        }

        // VehiclePhotoUploadText
        if ($('.vehicle-photo-upload:checked').length === 0) {
            result = false;
            $('#vehicle-photo-upload-error').removeClass('d-none');
        }
        else if (vehiclePhotoUpload !== DISABLE_VALUE) {
            enableVehiclePhotoUploadInDb = $('#vehicle-photo-upload-dbts').is(':checked');
            enableVehiclePhotoUploadInLocalStorage = $('#vehicle-photo-upload-lsts').is(':checked');

            // Check Any One Option  Enabled (Database / Local Storage)
            if (enableVehiclePhotoUploadInDb === true || enableVehiclePhotoUploadInLocalStorage === true) {
                $('#vehicle-photo-upload-required-error').addClass('d-none');

                // If Database Storage Enabled For Photo Upload
                if (enableVehiclePhotoUploadInDb) {
                    multiSelectCount = parseInt($('#vehicle-photo-upload-allowed-file-format-db option:selected').length);

                    if (parseInt(multiSelectCount) > 0) {
                        $('#vehicle-photo-upload-allowed-file-format-db-error').addClass('d-none');

                        vehiclePhotoAllowedFileFormatsForDbId = $('#vehicle-photo-upload-allowed-file-format-db option:selected')
                            .map(function () { return $(this).val(); }).get().join(',');

                        vehiclePhotoAllowedFileFormatsForDbText = $('#vehicle-photo-upload-allowed-file-format-db option:selected')
                            .map(function () { return $(this).text(); }).get().join(',');
                    }
                    else {
                        result = false;
                        $('#vehicle-photo-upload-allowed-file-format-db-error').removeClass('d-none');
                    }

                    maximumFileSizeForVehiclePhotoUploadInDb = $('#vehicle-photo-upload-maximum-file-size-db').val();

                    // Validate Maximum File Size
                    if (isNaN(maximumFileSizeForVehiclePhotoUploadInDb) === false) {
                        minimum = parseInt($('#vehicle-photo-upload-maximum-file-size-db').attr('min'));
                        maximum = parseInt($('#vehicle-photo-upload-maximum-file-size-db').attr('max'));

                        if (parseInt(maximumFileSizeForVehiclePhotoUploadInDb) < parseInt(minimum) || parseInt(maximumFileSizeForVehiclePhotoUploadInDb) > parseInt(maximum)) {
                            result = false;
                            $('#vehicle-photo-upload-maximum-file-size-db-error').removeClass('d-none');
                        }
                    }
                    else {
                        $('#vehicle-photo-upload-maximum-file-size-db-error').removeClass('d-none');
                    }
                }
                else {
                    $('#vehicle-photo-upload-allowed-file-format-db-error').addClass('d-none');
                    $('#vehicle-photo-upload-maximum-file-size-db-error').addClass('d-none');

                    vehiclePhotoAllowedFileFormatsForDbId = 'None';
                    vehiclePhotoAllowedFileFormatsForDbText = 'None';
                    maximumFileSizeForVehiclePhotoUploadInDb = 0;
                }

                // If Local Storage Enabled For Photo Upload
                if (enableVehiclePhotoUploadInLocalStorage) {
                    vehiclePhotoUploadLocalStoragePath = $('#vehicle-photo-upload-local-storage-path').val();

                    if (vehiclePhotoUploadLocalStoragePath === '') {
                        $('#vehicle-photo-upload-local-storage-path').val('None');
                    }

                    multiSelectCount = parseInt($('#vehicle-photo-upload-allowed-file-format-ls option:selected').length);

                    if (parseInt(multiSelectCount) > 0) {
                        $('#vehicle-photo-upload-allowed-file-format-ls-error').addClass('d-none');

                        vehiclePhotoAllowedFileFormatsForLSId = $('#vehicle-photo-upload-allowed-file-format-ls option:selected')
                            .map(function () { return $(this).val(); }).get().join(',');

                        vehiclePhotoAllowedFileFormatsForLSText = $('#vehicle-photo-upload-allowed-file-format-ls option:selected')
                            .map(function () { return $(this).text(); }).get().join(',');
                    }
                    else {
                        result = false;
                        $('#vehicle-photo-upload-allowed-file-format-ls-error').removeClass('d-none');
                    }

                    maximumFileSizeForVehiclePhotoUploadInLocalStorage = $('#vehicle-photo-upload-maximum-file-size-ls').val();

                    // Validate Maximum File Size
                    if (isNaN(maximumFileSizeForVehiclePhotoUploadInLocalStorage) === false) {
                        minimum = parseInt($('#vehicle-photo-upload-maximum-file-size-ls').attr('min'));
                        maximum = parseInt($('#vehicle-photo-upload-maximum-file-size-ls').attr('max'));

                        if (parseInt(maximumFileSizeForVehiclePhotoUploadInLocalStorage) < parseInt(minimum) || parseInt(maximumFileSizeForVehiclePhotoUploadInLocalStorage) > parseInt(maximum)) {
                            result = false;
                            $('#vehicle-photo-upload-maximum-file-size-ls-error').removeClass('d-none');
                        }
                    }
                    else {
                        result = false;
                        $('#vehicle-photo-upload-maximum-file-size-ls-error').removeClass('d-none');
                    }
                }
                else {
                    $('#vehicle-photo-upload-allowed-file-format-ls-error').addClass('d-none');
                    $('#vehicle-photo-upload-maximum-file-size-ls-error').addClass('d-none');

                    vehiclePhotoAllowedFileFormatsForLSId = 'None';
                    vehiclePhotoAllowedFileFormatsForLSText = 'None';
                    vehiclePhotoUploadLocalStoragePath = 'None';
                    maximumFileSizeForVehiclePhotoUploadInLocalStorage = 0;
                }
            }
            else {
                result = false;
                $('#vehicle-photo-upload-required-error').removeClass('d-none');
            }
        }
        else {
            vehiclePhotoAllowedFileFormatsForDbId = 'None';
            vehiclePhotoAllowedFileFormatsForDbText = 'None';
            maximumFileSizeForVehiclePhotoUploadInDb = 0;

            vehiclePhotoAllowedFileFormatsForLSId = 'None';
            vehiclePhotoAllowedFileFormatsForLSText = 'None';
            vehiclePhotoUploadLocalStoragePath = 'None';
            maximumFileSizeForVehiclePhotoUploadInLocalStorage = 0;
        }

        // MinimumNumberOfPhoto
        if (isNaN(minimumNumberOfPhoto) === false) {
            minimum = parseInt($('#minimum-number-of-photo-vehicle').attr('min'));
            maximum = parseInt($('#minimum-number-of-photo-vehicle').attr('max'));

            if (parseInt(minimumNumberOfPhoto) < parseInt(minimum) || parseInt(minimumNumberOfPhoto) > parseInt(maximum)) {
                result = false;
                $('#minimum-number-of-photo-vehicle-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#minimum-number-of-photo-vehicle-error').removeClass('d-none');
        }

        // MaximumNumberOfPhoto
        if (isNaN(maximumNumberOfPhoto) === false) {
            minimum = parseInt($('#maximum-number-of-photo-vehicle').attr('min'));
            maximum = parseInt($('#maximum-number-of-photo-vehicle').attr('max'));

            if (parseInt(maximumNumberOfPhoto) < parseInt(minimum) || parseInt(maximumNumberOfPhoto) > parseInt(maximum)) {
                result = false;
                $('#maximum-number-of-photo-vehicle-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-number-of-photo-vehicle-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsVehicleTypeLoanDataTable() {
        vehicleTypeLoanDataTable.column(1).visible(false);
        vehicleTypeLoanDataTable.column(5).visible(false);
        vehicleTypeLoanDataTable.column(8).visible(false);
        vehicleTypeLoanDataTable.column(13).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@  Pre Owned Vehicle Loan - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-pre-owned-vehicle-loan-dt').click(function (event) {
        event.preventDefault();
        editedPreVehicleId = '';
        SetPreOwnedVehicleUniqueDropdownList();
        SetModalTitle('pre-owned-vehicle-loan', 'Add');
        ResetFileUpload('pre-owned-photo-upload');
    });

    // DataTable Edit Button 
    $('#btn-edit-pre-owned-vehicle-loan-dt').click(function () {
        debugger;
        SetModalTitle('pre-owned-vehicle-loan', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-pre-owned-vehicle-loan-dt').data('rowindex');

            editedPreVehicleId = columnValues[1];
            SetPreOwnedVehicleUniqueDropdownList();

            id = $('#pre-owned-vehicle-loan-modal').attr('id');
            myModal = $('#' + id).modal();

            // Assign Min Max Value
            $('#vehicle-life-2').attr('min', columnValues[5]);
            $('#maximum-loan-sanction-percentage-2').attr('max', columnValues[6]);
            $('#maximum-tenure-2').attr('max', columnValues[7]);

            $('#vehicle-life-3').attr('min', columnValues[8]);
            $('#maximum-loan-sanction-percentage-3').attr('max', columnValues[9]);
            $('#maximum-tenure-3').attr('max', columnValues[10]);

            $('#vehicle-life-4').attr('min', columnValues[11]);
            $('#maximum-loan-sanction-percentage-4').attr('max', columnValues[12]);
            $('#maximum-tenure-4').attr('max', columnValues[13]);

            $('#maximum-number-of-photo-pre-owned').attr('min', columnValues[28]);


            $('#pre-vehicle-type-id', myModal).val(columnValues[1]);
            $('#down-payment-percentage-pre-owned', myModal).val(columnValues[3]);

            $('#enable-vehicle-inspection').prop('checked', columnValues[4].toString().toLowerCase() === 'true' ? true : false);

            $('#vehicle-life-1', myModal).val(columnValues[5]);
            $('#maximum-loan-sanction-percentage-1', myModal).val(columnValues[6]);
            $('#maximum-tenure-1', myModal).val(columnValues[7]);
            $('#vehicle-life-2', myModal).val(columnValues[8]);
            $('#maximum-loan-sanction-percentage-2', myModal).val(columnValues[9]);
            $('#maximum-tenure-2', myModal).val(columnValues[10]);
            $('#vehicle-life-3', myModal).val(columnValues[11]);
            $('#maximum-loan-sanction-percentage-3', myModal).val(columnValues[12]);
            $('#maximum-tenure-3', myModal).val(columnValues[13]);
            $('#vehicle-life-4', myModal).val(columnValues[14]);
            $('#maximum-loan-sanction-percentage-4', myModal).val(columnValues[15]);
            $('#maximum-tenure-4', myModal).val(columnValues[16]);
            $('.pre-owned-photo-upload[value="' + columnValues[17] + '"]').prop('checked', true);
            $('#pre-owned-photo-upload-dbts').prop('checked', columnValues[19].toString().toLowerCase() === 'true' ? true : false);
            $('#pre-owned-photo-upload-allowed-file-format-db', myModal).val(columnValues[20].split(','));
            $('#pre-owned-photo-upload-maximum-file-size-db', myModal).val(columnValues[22]);
            $('#pre-owned-photo-upload-lsts', myModal).prop('checked', columnValues[23].toString().toLowerCase() === 'true' ? true : false);
            $('#pre-owned-photo-upload-local-storage-path', myModal).val(columnValues[24]);
            $('#pre-owned-photo-upload-allowed-file-format-ls', myModal).val(columnValues[25].split(','));
            $('#pre-owned-photo-upload-maximum-file-size-ls', myModal).val(columnValues[27]);
            $('#minimum-number-of-photo-pre-owned', myModal).val(columnValues[28]);
            $('#maximum-number-of-photo-pre-owned', myModal).val(columnValues[29]);
            $('#note-pre-owned', myModal).val(columnValues[30]);

            SetDocumentUploadInput('pre-owned-photo-upload');

            modalObjSelect2.trigger('change');

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-pre-owned-vehicle-loan-dt').addClass('read-only');
            $('#vehicle-pre-owned-vehicle-loan-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-pre-owned-vehicle-loan-modal').click(function (event) {
        debugger;
        if (IsValidPreOwnedVehicleDataTableModal()) {
            row = preOwnedVehicleDataTable.row.add([
                tag,
                vehicleTypeId,
                vehicleTypeIdText,
                downPaymentPercentage,
                enableVehicleInspection,
                vehicleLife1,
                maximumLoanSanctionPercentage1,
                maximumTenure1,
                vehicleLife2,
                maximumLoanSanctionPercentage2,
                maximumTenure2,
                vehicleLife3,
                maximumLoanSanctionPercentage3,
                maximumTenure3,
                vehicleLife4,
                maximumLoanSanctionPercentage4,
                maximumTenure4,
                preOwnedPhotoUpload,
                preOwnedPhotoUploadText,
                enablePreOwnedUploadInDb,
                preOwnedAllowedFileFormatsForDbId,
                preOwnedAllowedFileFormatsForDbText,
                preOwnedMaximumFileSizeDb,
                enablePhotoUploadInLocalStoragePre,
                preOwnedLocalStoragePath,
                preOwnedAllowedFileFormatsForLocalStorageId,
                preOwnedAllowedFileFormatsForLocalStorageText,
                preOwnedMaximumFileSizeForLocalStorage,
                preOwnedMinimumNumberOfPhoto,
                preOwnedMaximumNumberOfPhoto,
                note,
            ]).draw();

            // Error Message In Span
            $('#pre-owned-vehicle-loan-data-table-error').addClass('d-none');

            HideColumnsPreOwnedVehicleDataTable();

            preOwnedVehicleDataTable.columns.adjust().draw();

            $('#pre-owned-vehicle-loan-modal').modal('hide');

            EnableNewOperation('pre-owned-vehicle-loan');
        }
    });

    // Modal update Button Event
    $('#btn-update-pre-owned-vehicle-loan-modal').click(function (event) {
        $('#select-all-pre-owned-vehicle-loan').prop('checked', false);
        if (IsValidPreOwnedVehicleDataTableModal()) {
            preOwnedVehicleDataTable.row(selectedRowIndex).data([
                tag,
                vehicleTypeId,
                vehicleTypeIdText,
                downPaymentPercentage,
                enableVehicleInspection,
                vehicleLife1,
                maximumLoanSanctionPercentage1,
                maximumTenure1,
                vehicleLife2,
                maximumLoanSanctionPercentage2,
                maximumTenure2,
                vehicleLife3,
                maximumLoanSanctionPercentage3,
                maximumTenure3,
                vehicleLife4,
                maximumLoanSanctionPercentage4,
                maximumTenure4,
                preOwnedPhotoUpload,
                preOwnedPhotoUploadText,
                enablePreOwnedUploadInDb,
                preOwnedAllowedFileFormatsForDbId,
                preOwnedAllowedFileFormatsForDbText,
                preOwnedMaximumFileSizeDb,
                enablePhotoUploadInLocalStoragePre,
                preOwnedLocalStoragePath,
                preOwnedAllowedFileFormatsForLocalStorageId,
                preOwnedAllowedFileFormatsForLocalStorageText,
                preOwnedMaximumFileSizeForLocalStorage,
                preOwnedMinimumNumberOfPhoto,
                preOwnedMaximumNumberOfPhoto,
                note,
            ]).draw();

            HideColumnsPreOwnedVehicleDataTable();

            preOwnedVehicleDataTable.columns.adjust().draw();

            $('#pre-owned-vehicle-loan-modal').modal('hide');

            EnableNewOperation('pre-owned-vehicle-loan');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-pre-owned-vehicle-loan-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-pre-owned-vehicle-loan tbody input[type="checkbox"]:checked').each(function () {
                    preOwnedVehicleDataTable.row($('#tbl-pre-owned-vehicle-loan tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-pre-owned-vehicle-loan-dt').data('rowindex');
                    EnableNewOperation('pre-owned-vehicle-loan');

                    SetPreOwnedVehicleUniqueDropdownList();

                    $('#select-all-pre-owned-vehicle-loan').prop('checked', false);
                    if (!preOwnedVehicleDataTable.data().any())
                        $('#pre-owned-vehicle-loan-data-table-error').removeClass('d-none');


                }));
            }
        }
        else
            alert('Please Select A CheckBox');
    });

    // Select All Check Box Click Event  
    $('#select-all-pre-owned-vehicle-loan').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-pre-owned-vehicle-loan tbody input[type="checkbox"]').each(function (index) {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                rowData = (preOwnedVehicleDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-pre-owned-vehicle-loan-dt').data('rowindex', arr);
                EnableDeleteOperation('pre-owned-vehicle-loan')
            });
        }
        else {
            EnableNewOperation('pre-owned-vehicle-loan')

            $('#tbl-pre-owned-vehicle-loan tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-pre-owned-vehicle-loan tbody').click('input[type="checkbox"]', function () {
        $('#tbl-pre-owned-vehicle-loan input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = preOwnedVehicleDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (preOwnedVehicleDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('pre-owned-vehicle-loan');

                    $('#btn-update-pre-owned-vehicle-loan-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-pre-owned-vehicle-loan-dt').data('rowindex', rowData);
                    $('#btn-delete-pre-owned-vehicle-loan-dt').data('rowindex', arr);
                    $('#select-all-pre-owned-vehicle-loan').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-pre-owned-vehicle-loan tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('pre-owned-vehicle-loan');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('pre-owned-vehicle-loan');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('pre-owned-vehicle-loan');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-pre-owned-vehicle-loan').prop('checked', true);
        else
            $('#select-all-pre-owned-vehicle-loan').prop('checked', false);
    });

    // Validate Recovery Action Module
    function IsValidPreOwnedVehicleDataTableModal() {
        let result = true;
        debugger;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        vehicleTypeId = $('#pre-vehicle-type-id option:selected').val();
        vehicleTypeIdText = $('#pre-vehicle-type-id option:selected').text();
        downPaymentPercentage = parseFloat($('#down-payment-percentage-pre-owned').val());
        enableVehicleInspection = $('#enable-vehicle-inspection').is(':checked') ? 'True' : 'False';
        vehicleLife1 = parseInt($('#vehicle-life-1').val());
        maximumLoanSanctionPercentage1 = parseFloat($('#maximum-loan-sanction-percentage-1').val());
        maximumTenure1 = parseInt($('#maximum-tenure-1').val());
        vehicleLife2 = parseInt($('#vehicle-life-2').val());
        maximumLoanSanctionPercentage2 = parseFloat($('#maximum-loan-sanction-percentage-2').val());
        maximumTenure2 = parseInt($('#maximum-tenure-2').val());
        vehicleLife3 = parseInt($('#vehicle-life-3').val());
        maximumLoanSanctionPercentage3 = parseFloat($('#maximum-loan-sanction-percentage-3').val());
        maximumTenure3 = parseInt($('#maximum-tenure-3').val());
        vehicleLife4 = parseInt($('#vehicle-life-4').val());
        maximumLoanSanctionPercentage4 = parseFloat($('#maximum-loan-sanction-percentage-4').val());
        maximumTenure4 = parseInt($('#maximum-tenure-4').val());
        preOwnedPhotoUploadText = $('.pre-owned-photo-upload:checked').next('label').text();
        preOwnedPhotoUpload = $('.pre-owned-photo-upload:checked').val();
        enablePreOwnedUploadInDb = $('#pre-owned-photo-upload-dbts').is(':checked');
        preOwnedMinimumNumberOfPhoto = parseInt($('#minimum-number-of-photo-pre-owned').val());
        preOwnedMaximumNumberOfPhoto = parseInt($('#maximum-number-of-photo-pre-owned').val());
        note = $('#note-pre-owned').val();

        // Set Default Value If Empty
        if (note === '')
            note = 'None';

        // Vehicle Type Id
        if ($('#pre-vehicle-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#pre-vehicle-type-id-error').removeClass('d-none');
        }

        // Down Payment Percentage
        if (isNaN(downPaymentPercentage) === false) {
            minimum = parseFloat($('#down-payment-percentage-pre-owned').attr('min'));
            maximum = parseFloat($('#down-payment-percentage-pre-owned').attr('max'));

            if (parseFloat(downPaymentPercentage) < parseFloat(minimum) || parseFloat(downPaymentPercentage) > parseFloat(maximum)) {
                result = false;
                $('#down-payment-percentage-pre-owned-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#down-payment-percentage-pre-owned-error').removeClass('d-none');
        }

        //VehicleLife1
        if (isNaN(vehicleLife1) === false) {
            minimum = parseInt($('#vehicle-life-1').attr('min'));
            maximum = parseInt($('#vehicle-life-1').attr('max'));

            if (parseInt(vehicleLife1) < parseInt(minimum) || parseInt(vehicleLife1) > parseInt(maximum)) {
                result = false;
                $('#vehicle-life-1-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#vehicle-life-1-error').removeClass('d-none');
        }

        // Maximum Loan Sanction Percentage 1
        if (isNaN(maximumLoanSanctionPercentage1) === false) {
            minimum = parseFloat($('#maximum-loan-sanction-percentage-1').attr('min'));
            maximum = parseFloat($('#maximum-loan-sanction-percentage-1').attr('max'));

            if (parseFloat(maximumLoanSanctionPercentage1) < parseFloat(minimum) || parseFloat(maximumLoanSanctionPercentage1) > parseFloat(maximum)) {
                result = false;
                $('#maximum-loan-sanction-percentage-1-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-loan-sanction-percentage-1-error').removeClass('d-none');
        }

        // Maximum Tenure 1
        if (isNaN(maximumTenure1) === false) {
            minimum = parseFloat($('#maximum-tenure-1').attr('min'));
            maximum = parseFloat($('#maximum-tenure-1').attr('max'));

            if (parseFloat(maximumTenure1) < parseFloat(minimum) || parseFloat(maximumTenure1) > parseFloat(maximum)) {
                result = false;
                $('#maximum-tenure-1-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-tenure-1-error').removeClass('d-none');
        }

        // VehicleLife2
        if (isNaN(vehicleLife2) === false) {
            minimum = parseInt($('#vehicle-life-2').attr('min'));
            maximum = parseInt($('#vehicle-life-2').attr('max'));

            if (parseInt(vehicleLife2) < parseInt(minimum) || parseInt(vehicleLife2) > parseInt(maximum)) {
                result = false;
                $('#vehicle-life-2-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#vehicle-life-2-error').removeClass('d-none');
        }

        // Maximum Loan Sanction Percentage 2
        if (isNaN(maximumLoanSanctionPercentage2) === false) {
            minimum = parseFloat($('#maximum-loan-sanction-percentage-2').attr('min'));
            maximum = parseFloat($('#maximum-loan-sanction-percentage-2').attr('max'));
            if (parseFloat(maximumLoanSanctionPercentage2) < parseFloat(minimum) || parseFloat(maximumLoanSanctionPercentage2) > parseFloat(maximum)) {
                result = false;
                $('#maximum-loan-sanction-percentage-2-error').removeClass('d-none');
            }

        }
        else {
            result = false;
            $('#maximum-loan-sanction-percentage-2-error').removeClass('d-none');
        }

        // Maximum Tenure 2
        if (isNaN(maximumTenure2) === false) {
            minimum = parseInt($('#maximum-tenure-2').attr('min'));
            maximum = parseFloat($('#maximum-tenure-2').attr('max'));

            if (parseFloat(maximumTenure2) < parseFloat(minimum) || parseFloat(maximumTenure2) > parseFloat(maximum)) {
                result = false;
                $('#maximum-tenure-2-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-tenure-2-error').removeClass('d-none');
        }

        // VehicleLife3
        if (isNaN(vehicleLife3) === false) {
            minimum = parseInt($('#vehicle-life-3').attr('min'));
            maximum = parseInt($('#vehicle-life-3').attr('max'));

            if (parseInt(vehicleLife3) < parseInt(minimum) || parseInt(vehicleLife3) > parseInt(maximum)) {
                result = false;
                $('#vehicle-life-3-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#vehicle-life-3-error').removeClass('d-none');
        }

        // Maximum Loan Sanction Percentage 3
        if (isNaN(maximumLoanSanctionPercentage3) === false) {
            minimum = parseFloat($('#maximum-loan-sanction-percentage-3').attr('min'));
            maximum = parseFloat($('#maximum-loan-sanction-percentage-3').attr('max'));

            if (parseFloat(maximumLoanSanctionPercentage3) < parseFloat(minimum) || parseFloat(maximumLoanSanctionPercentage3) > parseFloat(maximum)) {
                result = false;
                $('#maximum-loan-sanction-percentage-3-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-loan-sanction-percentage-3-error').removeClass('d-none');
        }

        // Maximum Tenure 3
        if (isNaN(maximumTenure3) === false) {
            minimum = parseFloat($('#maximum-tenure-3').attr('min'));
            maximum = parseFloat($('#maximum-tenure-3').attr('max'));

            if (parseFloat(maximumTenure3) < parseFloat(minimum) || parseFloat(maximumTenure3) > parseFloat(maximum)) {
                result = false;
                $('#maximum-tenure-3-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-tenure-3-error').removeClass('d-none');
        }

        // VehicleLife4
        if (isNaN(vehicleLife4) === false) {
            minimum = parseInt($('#vehicle-life-4').attr('min'));
            maximum = parseInt($('#vehicle-life-4').attr('max'));

            if (parseInt(vehicleLife4) < parseInt(minimum) || parseInt(vehicleLife4) > parseInt(maximum)) {
                result = false;
                $('#vehicle-life-4-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#vehicle-life-4-error').removeClass('d-none');
        }

        //Maximum Loan Sanction Percentage 4
        if (isNaN(maximumLoanSanctionPercentage4) === false) {
            minimum = parseFloat($('#maximum-loan-sanction-percentage-4').attr('min'));
            maximum = parseFloat($('#maximum-loan-sanction-percentage-4').attr('max'));

            if (parseFloat(maximumLoanSanctionPercentage4) < parseFloat(minimum) || parseFloat(maximumLoanSanctionPercentage4) > parseFloat(maximum)) {
                result = false;
                $('#maximum-loan-sanction-percentage-4-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-loan-sanction-percentage-4-error').removeClass('d-none');
        }

        //Maximum Tenure 4
        if (isNaN(maximumTenure4) === false) {
            minimum = parseFloat($('#maximum-tenure-4').attr('min'));
            maximum = parseFloat($('#maximum-tenure-4').attr('max'));

            if (parseFloat(maximumTenure4) < parseFloat(minimum) || parseFloat(maximumTenure4) > parseFloat(maximum)) {
                result = false;
                $('#maximum-tenure-4-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-tenure-4-error').removeClass('d-none');
        }

        // PreOwnedPhotoUploadText
        if (parseInt(($('.pre-owned-photo-upload:checked')).length) === 0) {
            result = false;
            $('#pre-owned-photo-upload-error').removeClass('d-none');
        }
        //If Photo Upload Not Equals To Disable
        else if (preOwnedPhotoUpload !== DISABLE_VALUE) {
            enablePreOwnedUploadInDb = $('#pre-owned-photo-upload-dbts').is(':checked');
            enablePhotoUploadInLocalStoragePre = $('#pre-owned-photo-upload-lsts').is(':checked');

            // Check Any One Option  Enabled (Database / Local Storage)
            if (enablePreOwnedUploadInDb === true || enablePhotoUploadInLocalStoragePre === true) {
                $('#pre-owned-photo-upload-required-error').addClass('d-none');

                //If Database Storage Enable For Photo Upload
                if (enablePreOwnedUploadInDb) {
                    multiSelectCount = parseInt($('#pre-owned-photo-upload-allowed-file-format-db option:selected').length);

                    if (parseInt(multiSelectCount) > 0) {
                        if ($('#pre-owned-photo-upload-allowed-file-format-db').prop('selectedIndex') < 1) {
                            result = false;
                            $('#pre-owned-photo-upload-allowed-file-format-db-error').removeClass('d-none');
                        }

                        $('#pre-owned-photo-upload-allowed-file-format-db-error').addClass('d-none');

                        preOwnedAllowedFileFormatsForDbId = $('#pre-owned-photo-upload-allowed-file-format-db option:selected')
                            .map(function () { return $(this).val(); }).get().join(',');

                        // For the texts
                        preOwnedAllowedFileFormatsForDbText = $('#pre-owned-photo-upload-allowed-file-format-db option:selected')
                            .map(function () {
                                return $(this).text();
                            }).get().join(',');
                    }
                    else {
                        result = false;
                        $('#pre-owned-photo-upload-allowed-file-format-db-error').removeClass('d-none');
                    }

                    preOwnedMaximumFileSizeDb = $('#pre-owned-photo-upload-maximum-file-size-db').val();

                    //Validate Maximum File Size
                    if (isNaN(preOwnedMaximumFileSizeDb) === false) {
                        minimum = parseInt($('#pre-owned-photo-upload-maximum-file-size-db').attr('min'));
                        maximum = parseInt($('#pre-owned-photo-upload-maximum-file-size-db').attr('max'));

                        if (parseInt(preOwnedMaximumFileSizeDb) < parseInt(minimum) || parseInt(preOwnedMaximumFileSizeDb) > parseInt(maximum)) {
                            result = false;
                            $('#pre-owned-photo-upload-maximum-file-size-db-error').removeClass('d-none');
                        }
                    }
                    else {
                        result = false;
                        $('#pre-owned-photo-upload-maximum-file-size-db-error').removeClass('d-none');
                    }

                }
                else {
                    $('#pre-owned-photo-upload-maximum-file-size-db-error').addClass('d-none');
                    $('#pre-owned-photo-upload-allowed-file-format-db-error').addClass('d-none');

                    preOwnedAllowedFileFormatsForDbId = 'None';
                    preOwnedAllowedFileFormatsForDbText = 'None';
                    preOwnedMaximumFileSizeDb = 0;
                }

                //If Local Storage Enable For Photo Upload
                if (enablePhotoUploadInLocalStoragePre) {
                    preOwnedLocalStoragePath = $('#pre-owned-photo-upload-local-storage-path').val();

                    if (preOwnedLocalStoragePath === '') {
                        $('#pre-owned-photo-upload-local-storage-path').val('None');
                    }
                    //Local Storage Path
                    if (preOwnedLocalStoragePath === '') {
                        result = false;
                        $('#pre-owned-photo-upload-local-storage-path-error').removeClass('d-none');
                    }

                    multiSelectCount = parseInt($('#pre-owned-photo-upload-allowed-file-format-ls option:selected').length);

                    if (parseInt(multiSelectCount) > 0) {
                        if ($('#pre-owned-photo-upload-allowed-file-format-ls').prop('selectedIndex') < 1) {
                            result = false;
                            $('#pre-owned-photo-upload-allowed-file-format-ls-error').removeClass('d-none');
                        }

                        $('#pre-owned-photo-upload-allowed-file-format-ls-error').addClass('d-none');

                        preOwnedAllowedFileFormatsForLocalStorageId = $('#pre-owned-photo-upload-allowed-file-format-ls option:selected')
                            .map(function () {
                                return $(this).val();
                            }).get().join(',');

                        // For the texts
                        preOwnedAllowedFileFormatsForLocalStorageText = $('#pre-owned-photo-upload-allowed-file-format-ls option:selected')
                            .map(function () {
                                return $(this).text();
                            }).get().join(',');
                    }
                    else {
                        result = false;
                        $('#pre-owned-photo-upload-allowed-file-format-ls-error').removeClass('d-none');
                    }

                    preOwnedMaximumFileSizeForLocalStorage = $('#pre-owned-photo-upload-maximum-file-size-ls').val();

                    //Validate Maximum File Size For Local Storage
                    if (isNaN(preOwnedMaximumFileSizeForLocalStorage) === false) {
                        minimum = parseInt($('#pre-owned-photo-upload-maximum-file-size-ls').attr('min'));
                        maximum = parseInt($('#pre-owned-photo-upload-maximum-file-size-ls').attr('max'));

                        if (parseInt(preOwnedMaximumFileSizeForLocalStorage) < parseInt(minimum) || parseInt(preOwnedMaximumFileSizeForLocalStorage) > parseInt(maximum)) {
                            result = false;
                            $('#pre-owned-photo-upload-maximum-file-size-ls-error').removeClass('d-none');
                        }
                    }
                    else {
                        result = false;
                        $('#pre-owned-photo-upload-maximum-file-size-ls-error').removeClass('d-none');
                    }
                }
                else {
                    $('#pre-owned-photo-upload-local-storage-path-error').addClass('d-none');
                    $('#pre-owned-photo-upload-allowed-file-format-ls-error').addClass('d-none');
                    $('#pre-owned-photo-upload-maximum-file-size-ls-error').addClass('d-none');

                    preOwnedAllowedFileFormatsForLocalStorageId = 'None';
                    preOwnedAllowedFileFormatsForLocalStorageText = 'None';
                    preOwnedMaximumFileSizeForLocalStorage = 0;
                    preOwnedLocalStoragePath = 'None';
                }
            }
            else {
                result = false;
                $('#pre-owned-photo-upload-required-error').removeClass('d-none');
            }
        }
        else {
            preOwnedAllowedFileFormatsForDbId = 'None';
            preOwnedAllowedFileFormatsForDbText = 'None';
            preOwnedMaximumFileSizeDb = 0;

            preOwnedAllowedFileFormatsForLocalStorageId = 'None';
            preOwnedAllowedFileFormatsForLocalStorageText = 'None';
            preOwnedMaximumFileSizeForLocalStorage = 0;
            preOwnedLocalStoragePath = 'None';
        }

        //Minimum Number Of Photo
        if (isNaN(preOwnedMinimumNumberOfPhoto) === false) {
            minimum = parseInt($('#minimum-number-of-photo-pre-owned').attr('min'));
            maximum = parseInt($('#minimum-number-of-photo-pre-owned').attr('max'));

            if (parseInt(preOwnedMinimumNumberOfPhoto) < parseInt(minimum) || parseInt(preOwnedMinimumNumberOfPhoto) > parseInt(maximum)) {
                result = false;
                $('#minimum-number-of-photo-pre-owned-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#minimum-number-of-photo-pre-owned-error').removeClass('d-none');
        }

        //Maximum Number Of Photo
        if (isNaN(preOwnedMaximumNumberOfPhoto) === false) {
            minimum = parseInt($('#maximum-number-of-photo-pre-owned').attr('min'));
            maximum = parseInt($('#maximum-number-of-photo-pre-owned').attr('max'));

            if (parseInt(preOwnedMaximumNumberOfPhoto) < parseInt(minimum) || parseInt(preOwnedMaximumNumberOfPhoto) > parseInt(maximum)) {
                result = false;
                $('#maximum-number-of-photo-pre-owned-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#maximum-number-of-photo-pre-owned-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsPreOwnedVehicleDataTable() {
        preOwnedVehicleDataTable.column(1).visible(false);
        preOwnedVehicleDataTable.column(17).visible(false);
        preOwnedVehicleDataTable.column(20).visible(false);
        preOwnedVehicleDataTable.column(25).visible(false);
    }

    //Clear Error Massage For Multi Select List Of Main Page Input
    objSelect2.on('select2:close', function (e) {
        debugger;
        let myId = $(this).attr('id');

        // Account Number Mask
        if (myId === 'account-number-mask') {
            IsValidCustomerAccountNumberAccordionInputs();
        }

        // Application Number Mask
        if (myId === 'application-number-mask')
            IsValidApplicationNumberAccordionInputs();

        // Certificate Number Mask
        if (myId === 'certificate-number-mask') {
            IsValidCertificateNumberAccordionInputs();
        }

        // Passbook Number Mask
        if (myId === 'passbook-number-mask') {
            IsValidPassbookDetailAccordionInputs();
        }

        // Aggrement Number Mask
        if (myId === 'agreement-number-mask') {
            IsValidAgreemetNumberAccordionInputs();
        }

        // Gold Photo
        if (myId === 'gold-photo-allowed-file-format-db' || myId === 'gold-photo-allowed-file-format-ls') {
            IsValidGoldLoanAccordionInputs();
        }
    });

    // Clear Error Massage For Multi Select List Of Modal Input
    modalObjSelect2.on('select2:close', function (e) {
        debugger;
        let myId = $(this).attr('id');

        // Document
        if (myId === 'document-upload-allowed-file-format-db' || myId === 'document-upload-allowed-file-format-ls') {
            IsValidDocumentDataTableModal();
        }

        // Vehicle
        if (myId === 'vehicle-photo-upload-allowed-file-format-db' || myId === 'vehicle-photo-upload-allowed-file-format-ls') {
            IsValidVehicleTypeLoanDataTableModal();
        }

        // Preowned
        if (myId === 'pre-owned-photo-upload-allowed-file-format-db' || myId === 'pre-owned-photo-upload-allowed-file-format-ls') {
            IsValidPreOwnedVehicleDataTableModal();
        }
    });

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@  Consumer Durable Loan - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-consumer-durable-loan-dt').click(function (event) {
        debugger;
        event.preventDefault();
        SetModalTitle('consumer-durable-loan', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-consumer-durable-loan-dt').click(function () {
        SetModalTitle('consumer-durable-loan', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-consumer-durable-loan-dt').data('rowindex');
            id = $('#consumer-durable-loan-modal').attr('id');
            myModal = $('#' + id).modal();

            $('#durable-item-id', myModal).val(columnValues[1]);
            $('#margin-consumer-durable', myModal).val(columnValues[3]);

            // Show Modal
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-consumer-durable-loan-dt').addClass('read-only');
            $('#consumer-durable-loan-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-consumer-durable-loan-modal').click(function (event) {
        debugger;
        if (IsValidConsumerDurableLoanDataTableModal()) {
            row = consumerDurableLoanDataTable.row.add([
                tag,
                nameOfItemId,
                nameOfItemIdText,
                margin,
            ]).draw();
            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            // Error Message In Span
            $('#consumer-durable-loan-accordion-title-error').addClass('d-none');

            HideColumnsConsumerDurableLoanDataTable();

            consumerDurableLoanDataTable.columns.adjust().draw();


            $('#consumer-durable-loan-modal').modal('hide');

            EnableNewOperation('consumer-durable-loan');
        }
    });

    // Modal Update Button Event
    $('#btn-update-consumer-durable-loan-modal').click(function (event) {
        $('#select-all-consumer-durable-loan').prop('checked', false);
        if (IsValidConsumerDurableLoanDataTableModal()) {
            consumerDurableLoanDataTable.row(selectedRowIndex).data([
                tag,
                nameOfItemId,
                nameOfItemIdText,
                margin,
            ]).draw();

            HideColumnsConsumerDurableLoanDataTable();

            consumerDurableLoanDataTable.columns.adjust().draw();

            $('#consumer-durable-loan-modal').modal('hide');

            EnableNewOperation('consumer-durable-loan');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-consumer-durable-loan-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-consumer-durable-loan tbody input[type="checkbox"]:checked').each(function () {
                    consumerDurableLoanDataTable.row($('#tbl-consumer-durable-loan tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();
                    rowData = $('#btn-delete-consumer-durable-loan-dt').data('rowindex');
                    EnableNewOperation('consumer-durable-loan');

                    $('#select-all-consumer-durable-loan').prop('checked', false);
                    if (!consumerDurableLoanDataTable.data().any())
                        $('#consumer-durable-loan-accordion-title-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Other Charges Datatable
    $('#select-all-consumer-durable-loan').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-consumer-durable-loan tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = consumerDurableLoanDataTable.row(row).index();

                rowData = (consumerDurableLoanDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-consumer-durable-loan-dt').data('rowindex', arr);
                EnableDeleteOperation('consumer-durable-loan');
            });
        }
        else {
            EnableNewOperation('consumer-durable-loan');
            $('#consumer-durable-loan tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-consumer-durable-loan tbody').click('input[type="checkbox"]', function () {
        $('#tbl-consumer-durable-loan input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');

                selectedRowIndex = consumerDurableLoanDataTable.row(row).index();

                rowData = (consumerDurableLoanDataTable.row(selectedRowIndex).data());
                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('consumer-durable-loan');

                $('#btn-update-consumer-durable-loan-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-consumer-durable-loan-dt').data('rowindex', rowData);
                $('#btn-delete-consumer-durable-loan-dt').data('rowindex', arr);
                $('#select-all-consumer-durable-loan').data('rowindex', arr);
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('consumer-durable-loan');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('consumer-durable-loan');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('consumer-durable-loan');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-consumer-durable-loan').prop('checked', true);
        else
            $('#select-all-consumer-durable-loan').prop('checked', false);
    });

    // Validate Recovery Action Module
    function IsValidConsumerDurableLoanDataTableModal() {
        debugger;
        let result = true;
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        nameOfItemId = $('#durable-item-id option:selected').val();
        nameOfItemIdText = $('#durable-item-id option:selected').text();
        margin = parseFloat($('#margin-consumer-durable').val());

        // Name Of Item Id
        if ($('#durable-item-id').prop('selectedIndex') < 1) {
            result = false;
            $('#durable-item-id-error').removeClass('d-none');
        }

        // Validate Limit
        if (isNaN(margin) === false) {
            minimum = parseFloat($('#margin-consumer-durable').attr('min'));
            maximum = parseFloat($('#margin-consumer-durable').attr('max'));

            if (parseFloat(margin) < parseFloat(minimum) || parseFloat(margin) > parseFloat(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
            $('#margin-consumer-durable-error').addClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsConsumerDurableLoanDataTable() {
        consumerDurableLoanDataTable.column(1).visible(false);

    }

    //@@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme Educational Course  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-educational-course-dt').click(function (event) {
        debugger;
        event.preventDefault();
        editedEducationalCourseId = '';
        SetEducationalCourseUniqueDropdownList();
        SetModalTitle('educational-course', 'Add');
        $('#btn-add-educational-course-modal').removeClass('read-only')
        $('#educational-course-id').hide();
        $('#educational-course-id').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();
    });

    // DataTable Edit Button 
    $('#btn-edit-educational-course-dt').click(function () {
        debugger;
        SetModalTitle('educational-course', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-educational-course-dt').data('rowindex');

            editedEducationalCourseId = columnValues[1];

            SetEducationalCourseUniqueDropdownList();

            $('#educational-course-id').removeAttr('style');
            $('#educational-course-id').removeAttr('multiple');
            $('.ms-options-wrap').css('display', 'none');

            $('#educational-course-id', myModal).val(columnValues[1]);
            $('#note-educational-course', myModal).val(columnValues[3]);

            // Show Modal
            $('#educational-course-modal').modal('show');
        }
        else {
            $('#btn-edit-educational-course-dt').addClass('read-only');
            $('#educational-course-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-educational-course-modal').click(function (event) {
        debugger;
        let educationalCourseId = [];
        let educationalCourseText = [];

        $('#educational-course-id option:selected').each(function () {
            educationalCourseId.push($(this).val());
            educationalCourseText.push($(this).text());
        });

        if (IsValidEducationalCourseDataTableModal()) {
            debugger;
            $('#btn-add-educational-course-modal').addClass('read-only')
            for (let i = 0, j = 0; i < educationalCourseId.length, j < educationalCourseText.length; i++, j++) {
                row = educationalCourseDataTable.row.add([
                    tag,
                    educationalCourseId[i],
                    educationalCourseText[j],
                    note
                ]).draw();

                rowNum++;

                row.nodes().to$().attr('id', 'tr' + rowNum);

                // Error Message In Span
                $('#educational-course-accordion-error').addClass('d-none');

                HideColumnsEducationalCourseDataTable();

                educationalCourseDataTable.columns.adjust().draw();

                $('#educational-course-modal').modal('hide');

                EnableNewOperation('educational-course');
            }
        }
    });

    // Modal Update Button Event
    $('#btn-update-educational-course-modal').click(function (event) {
        $('#select-all-educational-course').prop('checked', false);
        if (IsValidEducationalCourseDataTableModal()) {
            educationalCourseDataTable.row(selectedRowIndex).data([
                tag,
                educationalCourseId,
                educationalCourseText,
                note
            ]).draw();

            // Hide the element with id 'educational-course-id'
            $('#educational-course-id').hide();
            // Set the 'multiple' attribute to 'multiple' for the element with id 'educational-course-id'
            $('#educational-course-id').attr('multiple', 'multiple');
            // Show elements with class 'ms-options-wrap'
            $('.ms-options-wrap').show();

            HideColumnsEducationalCourseDataTable()

            educationalCourseDataTable.columns.adjust().draw();

            $('#educational-course-modal').modal('hide');

            EnableNewOperation('educational-course');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-educational-course-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-educational-course tbody input[type="checkbox"]:checked').each(function () {
                    educationalCourseDataTable.row($('#tbl-educational-course tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();
                    rowData = $('#btn-delete-educational-course-dt').data('rowindex');
                    EnableNewOperation('educational-course');

                    // Hide the element with id 'educational-course-id'
                    $('#educational-course-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'educational-course-id'
                    $('#educational-course-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();

                    SetEducationalCourseUniqueDropdownList();

                    $('#select-all-educational-course').prop('checked', false);
                    // Display Required Error Message, If Table Has Not Any Record
                    if (educationalCourseDataTable.data().any() === false)
                        $('#educational-course-accordion-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-educational-course').click(function () {
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);

            $('#tbl-educational-course tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = educationalCourseDataTable.row(row).index();

                rowData = (educationalCourseDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-educational-course-dt').data('rowindex', arr);

                EnableDeleteOperation('educational-course');

            });
        }
        else {
            EnableNewOperation('educational-course');

            $('#tbl-educational-course tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);

                EnableNewOperation('educational-course');
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-educational-course tbody').click('input[type="checkbox"]', function () {
        $('#tbl-educational-course input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');

                selectedRowIndex = educationalCourseDataTable.row(row).index();

                rowData = (educationalCourseDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('educational-course');

                $('#btn-update-educational-course-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-educational-course-dt').data('rowindex', rowData);
                $('#btn-delete-educational-course-dt').data('rowindex', arr);
                $('#select-all-educational-course').data('rowindex', arr);
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-educational-course tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('educational-course');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('educational-course');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('educational-course');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-educational-course').prop('checked', true);
        else
            $('#select-all-educational-course').prop('checked', false);
    });

    // Validate Business Office Module
    function IsValidEducationalCourseDataTableModal() {
        debugger;

        let result = true;
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        educationalCourseId = $('#educational-course-id option:selected').val();
        educationalCourseText = $('#educational-course-id option:selected').text();
        note = $('#note-educational-course').val();

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        if (educationalCourseText === '') {
            result = false;
            $('#educational-course-id-error').removeClass('d-none');
        } else
            $('#educational-course-id-error').addClass('d-none');

        if ($('#educational-course-id-multi-select-ul').focusout(function () {
            debugger;
            if (educationalCourseId === '') {
                result = false;
                $('#educational-course-id-error').removeClass('d-none');
            } else
                $('#educational-course-id-error').addClass('d-none');
        }));


        if ($('.ms-selectall').focusout(function () {
            debugger;
            if (educationalCourseId === '') {
                result = false;
                $('#educational-course-id-error').removeClass('d-none');
            } else
                $('#educational-course-id-error').addClass('d-none');
        }));

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsEducationalCourseDataTable() {
        educationalCourseDataTable.column(1).visible(false);
    }

    //@@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme Educational Course  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-educational-institute-dt').click(function (event) {

        debugger;
        event.preventDefault();
        editedEducationalInstituteId = '';
        SetEducationalInstituteUniqueDropdownList();
        SetModalTitle('educational-institute', 'Add');
        $('#btn-add-educational-institute-modal').removeClass('read-only')
        $('#institute-id').hide();
        $('#institute-id').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();
    });

    // DataTable Edit Button 
    $('#btn-edit-educational-institute-dt').click(function () {

        debugger;
        SetModalTitle('educational-institute', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-educational-institute-dt').data('rowindex');

            editedEducationalInstituteId = columnValues[1];

            SetEducationalInstituteUniqueDropdownList();

            $('#institute-id').removeAttr('style');
            $('#institute-id').removeAttr('multiple');
            $('.ms-options-wrap').css('display', 'none');

            $('#institute-id', myModal).val(columnValues[1]);
            $('#note-educational-institute', myModal).val(columnValues[3]);

            // Show Modal
            $('#educational-institute-modal').modal('show');
        }
        else {
            $('#btn-edit-educational-institute-dt').addClass('read-only');
            $('#educational-institute-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-educational-institute-modal').click(function (event) {
        let educationalInstituteId = [];
        let educationalInstituteText = [];

        $('#institute-id option:selected').each(function () {
            educationalInstituteId.push($(this).val());
            educationalInstituteText.push($(this).text());
        });

        if (IsValidEducationalInstituteDataTableModal()) {
            $('#btn-add-educational-institute-modal').addClass('read-only')
            for (let i = 0, j = 0; i < educationalInstituteId.length, j < educationalInstituteText.length; i++, j++) {
                row = instituteDataTable.row.add([
                    tag,
                    educationalInstituteId[i],
                    educationalInstituteText[j],
                    note
                ]).draw();

                rowNum++;

                row.nodes().to$().attr('id', 'tr' + rowNum);

                // Error Message In Span
                $('#educational-institute-accordion-error').addClass('d-none');

                HideColumnsEducationalInstituteDataTable();

                instituteDataTable.columns.adjust().draw();

                $('#educational-institute-modal').modal('hide');

                EnableNewOperation('educational-institute');
            }
        }
    });

    // Modal Update Button Event
    $('#btn-update-educational-institute-modal').click(function (event) {
        $('#select-all-educational-institute').prop('checked', false);
        if (IsValidEducationalInstituteDataTableModal()) {
            instituteDataTable.row(selectedRowIndex).data([
                tag,
                educationalInstituteId,
                educationalInstituteText,
                note
            ]).draw();

            // Hide the element with id 'educational-institute-id'
            $('#institute-id').hide();
            // Set the 'multiple' attribute to 'multiple' for the element with id 'educational-institute-id'
            $('#institute-id').attr('multiple', 'multiple');
            // Show elements with class 'ms-options-wrap'
            $('.ms-options-wrap').show();

            HideColumnsEducationalCourseDataTable()

            instituteDataTable.columns.adjust().draw();

            $('#educational-institute-modal').modal('hide');

            EnableNewOperation('educational-institute');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-educational-institute-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-educational-institute tbody input[type="checkbox"]:checked').each(function () {
                    instituteDataTable.row($('#tbl-educational-institute tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();
                    rowData = $('#btn-delete-educational-institute-dt').data('rowindex');
                    EnableNewOperation('educational-institute');

                    // Hide the element with id 'educational-institute-id'
                    $('#institute-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'educational-institute-id'
                    $('#institute-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();

                    SetEducationalInstituteUniqueDropdownList();

                    $('#select-all-educational-institute').prop('checked', false);
                    // Display Required Error Message, If Table Has Not Any Record
                    if (instituteDataTable.data().any() === false)
                        $('#educational-institute-accordion-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-educational-institute').click(function () {
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);

            $('#tbl-educational-institute tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = instituteDataTable.row(row).index();

                rowData = (instituteDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-educational-institute-dt').data('rowindex', arr);

                EnableDeleteOperation('educational-institute');

            });
        }
        else {
            EnableNewOperation('educational-institute');

            $('#tbl-educational-institute tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);

                EnableNewOperation('educational-institute');
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-educational-institute tbody').click('input[type="checkbox"]', function () {
        $('#tbl-educational-institute input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');

                selectedRowIndex = instituteDataTable.row(row).index();

                rowData = (instituteDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('educational-institute');

                $('#btn-update-educational-institute-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-educational-institute-dt').data('rowindex', rowData);
                $('#btn-delete-educational-institute-dt').data('rowindex', arr);
                $('#select-all-educational-institute').data('rowindex', arr);
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-educational-institute tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('educational-institute');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('educational-institute');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('educational-institute');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-educational-institute').prop('checked', true);
        else
            $('#select-all-educational-institute').prop('checked', false);
    });

    // Validate Notice Schedule Module
    function IsValidEducationalInstituteDataTableModal() {
        // Get Modal Inputs In Local letiables
        let result = true;
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        educationalInstituteId = $('#institute-id option:selected').val();
        educationalInstituteText = $('#institute-id option:selected').text();
        note = $('#note-educational-institute').val();

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        if (educationalInstituteText === '') {
            result = false;
            $('#institute-id-error').removeClass('d-none');
        } else
            $('#institute-id-error').addClass('d-none');

        if ($('#institute-id-multi-select-ul').focusout(function () {
            debugger;
            if (educationalInstituteId === '') {
                result = false;
                $('#institute-id-error').removeClass('d-none');
            } else
                $('#institute-id-error').addClass('d-none');
        }));

        if ($('.ms-selectall').focusout(function () {
            debugger;
            if (educationalInstituteId === '') {
                result = false;
                $('#institute-id-error').removeClass('d-none');
            } else
                $('#institute-id-error').addClass('d-none');
        }));

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsEducationalInstituteDataTable() {
        instituteDataTable.column(1).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Unique Dropdown List @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    //Validation For Vehicle Type Id Unique
    function SetVehicleTypeUniqueDropdownList() {
        // Show All Dropdownlist Items
        $('#vehicle-type-id').html('');
        $('#vehicle-type-id').append(VEHICLE_TYPE_DROPDOWN)

        // Hide Inserted Dropdownlist Items
        $('#tbl-vehicle-type-loan > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');
            let myColumnValues = (vehicleTypeLoanDataTable.row(currentRow).data());

            if (typeof myColumnValues !== 'undefined' && myColumnValues !== null)
                if (myColumnValues[1] !== editedVehicleLoanId)
                    $('#vehicle-type-id').find('option[value="' + myColumnValues[1] + '"]').remove();
        });
    }

    // Validation For Pre Owned Vehicle Type Id Unique
    function SetPreOwnedVehicleUniqueDropdownList() {
        // Show All Dropdownlist Items
        $('#pre-vehicle-type-id').html('');
        $('#pre-vehicle-type-id').append(PRE_VEHICLE_DROPDOWN);

        // Hide Inserted Dropdownlist Items
        $('#tbl-pre-owned-vehicle-loan > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');
            let myColumnValues = (preOwnedVehicleDataTable.row(currentRow).data());

            if (typeof myColumnValues !== 'undefined' && myColumnValues !== null)
                if (myColumnValues[1] !== editedPreVehicleId)
                    $('#pre-vehicle-type-id').find('option[value="' + myColumnValues[1] + '"]').remove();
        });
    }

    // Validation For Charges Type Id Unique
    function SetChargesUniqueDropdownList() {
        // Show All Dropdownlist Items
        $('#charges-type-id').html('');
        $('#charges-type-id').append(CHARGES_DETAIL_DROPDOWN)

        // Hide Inserted Dropdownlist Items
        $('#tbl-charges > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');
            let myColumnValues = (loanChargesDataTable.row(currentRow).data());

            if (typeof myColumnValues !== 'undefined' && myColumnValues !== null) {
                if (myColumnValues[1] !== editedChargesType) {
                    $('#charges-type-id').find('option[value="' + myColumnValues[1] + '"]').remove();
                }
            }
        });
    }

    // Validation For Dcoument Id Unique
    function SetDocumentUniqueDropdownList() {
        // Show All Dropdownlist Items
        $('#document-id').html('');
        $('#document-id').append(documentDropdownList)

        // Hide Inserted Dropdownlist Items
        $('#tbl-document > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');
            let myColumnValues = (documentDataTable.row(currentRow).data());

            if (typeof myColumnValues !== 'undefined' && myColumnValues !== null) {
                if (myColumnValues[1] !== editedDocumentId) {
                    $('#document-id').find('option[value="' + myColumnValues[1] + '"]').remove();
                }
            }
        });
    }

    // Validation For Report Format Id Unique
    function SetReportFormatUniqueDropdownList() {
        // Show All List Items
        $('#report-format-id').html('');
        $('#report-format-id').append(REPORT_FORMAT_DROPDOWN);

        // Hide Added DropdownList Items
        $('#tbl-report-format-data-table > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');
            let myColumnValues = (reportFormatDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null)
                if (myColumnValues[1] != editedReportFormatId)
                    $('#report-format-id').find('option[value="' + myColumnValues[1] + '"]').remove();
        });
    }

    // Validation For Business Office Id Unique
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

    // Validation For Educational Course Id Unique
    function SetEducationalCourseUniqueDropdownList() {
        // Show All List Items
        $('#educational-course-id').html('');
        $('#educational-course-id-multi-select-ul').html('');

        $('#educational-course-id').html(EDUCATIONAL_COURSE_DROPDOWN);
        $('#educational-course-id-multi-select-ul').html(EDUCATIONAL_COURSE_DROPDOWN_MULTI_SELECT_LIST);

        // To Get All Table Records
        educationalCourseDataTable.page.len(-1).draw();

        // Hide Inserted Dropdownlist Items
        $('#tbl-educational-course > tbody > tr').each(function () {
            debugger;
            currentRow = $(this).closest('tr');
            let myColumnValues = (educationalCourseDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null) {
                if (myColumnValues[1] != editedEducationalCourseId) {
                    $('#educational-course-id').find('option[value="' + myColumnValues[1] + '"]').remove();
                    $('.ms-options-wrap').find('input[type="checkbox"][value="' + myColumnValues[1] + '"]').closest('li').remove();
                }
            }
        });
    }

    // Validation For Educational Institute Id Unique
    function SetEducationalInstituteUniqueDropdownList() {
        // Show All List Items
        $('#institute-id').html('');
        $('#educational-course-id-multi-select-ul').html('');

        $('#institute-id').html(EDUCATIONAL_INSTITUTE_DROPDOWN);
        $('#institute-id-multi-select-ul').html(EDUCATIONAL_INSTITUTE_DROPDOWN_MULTI_SELECT_LIST);

        // To Get All Table Records
        educationalCourseDataTable.page.len(-1).draw();

        // Hide Inserted Dropdownlist Items
        $('#tbl-educational-institute > tbody > tr').each(function () {
            debugger;
            currentRow = $(this).closest('tr');
            let myColumnValues = (instituteDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null) {
                if (myColumnValues[1] != editedEducationalInstituteId) {
                    $('#institute-id').find('option[value="' + myColumnValues[1] + '"]').remove();
                    $('.ms-options-wrap').find('input[type="checkbox"][value="' + myColumnValues[1] + '"]').closest('li').remove();
                }
            }
        });
    }

    // Validation For General Ledger Id Unique
    function SetGeneralLedgerUniqueDropdownList() {
        // Show All List Items
        $('#scheme-general-ledger-id').html('');
        $('#scheme-general-ledger-id').append(generalLedgerDropdownListItemsByLoanType);

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

    $('#btnsave').click(function (event) {
        debugger;

        let isValidAllInputs = true;

        let isValidTenureInput = $('#loan-type-id').is(':checked') ? true : ($('#enable-tenure').is(':checked') || $('#enable-tenure-list').is(':checked'));

        if ($('form').valid()) {

            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let tenureListArray = new Array();
            let DocumentArray = new Array();
            let noticeScheduleArray = new Array();
            let reportFormatArray = new Array();
            let generalLedgerArray = new Array();
            let businessOfficeArray = new Array();
            let targetGroupArray = new Array();
            let loanChargesArray = new Array();
            let loanOverduesActionArray = new Array();
            let vehicleTypeLoanArray = new Array();
            let consumerDurableLoanArray = new Array();
            let preownedVehicleArray = new Array();
            let educationalCourseArray = new Array();
            let educationalInstituteArray = new Array();

            //CreateDataTable
            tenureListDataTable.page.len(-1).draw();
            documentDataTable.page.len(-1).draw();
            noticeScheduleDataTable.page.len(-1).draw();
            reportFormatDataTable.page.len(-1).draw();
            generalLedgerDataTable.page.len(-1).draw();
            businessOfficeDataTable.page.len(-1).draw();
            targetGroupDataTable.page.len(-1).draw();
            loanChargesDataTable.page.len(-1).draw();
            loanOverduesActionDataTable.page.len(-1).draw();
            vehicleTypeLoanDataTable.page.len(-1).draw();
            consumerDurableLoanDataTable.page.len(-1).draw();
            educationalCourseDataTable.page.len(-1).draw();
            instituteDataTable.page.len(-1).draw();

            // Validate Scheme Name
            if (!isValidSchemeName) {
                isValidAllInputs = false;
                $('#name-of-scheme-error').removeClass('d-none');
            }
            else
                $('#name-of-scheme-error').addClass('d-none');

            // Validate Tenure
            if (!isValidTenureInput) {
                isValidAllInputs = false;
                $('#tenure-required-error').removeClass('d-none');
            }
            else
                $('#tenure-required-error').addClass('d-none');

            debugger;
            // Normal Accordion-1 Validation
            if (!IsValidAccountParameterAccordionInputs()) {
                debugger;
                isValidAllInputs = false;
            }

            // Normal Accordion-2 Validation
            if (!IsValidLoanDistributorAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-3 Validation
            if (!IsValidTenureAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-4 Validation
            if (!IsValidTargetEstimationAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-5 Validation
            if (!IsValidPrePartPaymentAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-6 Validation
            if (!IsValidPreFullPaymentAccordionInputs()) {
                isValidAllInputs = false;

            }

            // Normal Accordion-7 Validation
            if (!IsValidLoanFunderAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-8 Validation
            if (!IsValidLoanInstallmentAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion- Validation
            if (!IsValidSanctionAuthorityAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-9 Validation
            if (!IsValidLoanInterestRebateAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-9 Validation
            if (!IsValidSettlementAccountAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-10 Validation
            if (!IsValidLoanArrearAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-11 Validation
            if (!IsValidLoanInterestAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-12 Validation
            if (!IsValidPassbookDetailAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-13 Validation
            if (!IsValidLoanRepaymentAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-14 Validation
            if (!IsValidGoldLoanAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-15 Validation Error 
            if (!IsValidAgreemetNumberAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-16 Validation Error
            if (!IsValidApplicationNumberAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-17 Validation Error 
            if (!IsValidCustomerAccountNumberAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-18 Validation Error 
            if (!IsValidPaymentReminderAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-19 Validation Error 
            if (!IsValidCashCreditLoanAccordianInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-20 Validation Error 
            if (!IsValidBusinessLoanAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-21 Validation Error 
            if (!IsValidLoanAgainstDepositAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Normal Accordion-22 Validation Error 
            if (!IsValidEducationLoanAccordionInputs()) {
                isValidAllInputs = false;
            }

            // Create Array For educational course Data Table To Pass Data
            if ($('#applicable-all-course-block').hasClass('d-none') === false) {
                if (educationalCourseDataTable.data().any()) {

                    $('#educational-course-accordion-error').addClass('d-none');
                    if (isValidAllInputs) {
                        // Get Data Table Values In educational course Array
                        $('#tbl-educational-course > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (educationalCourseDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {


                                educationalCourseArray.push(
                                    {
                                        'EducationalCourseId': columnValues[1],
                                        'Note': columnValues[3]
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#educational-course-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For educational institute Data Table To Pass Data
            if ($('#applicable-all-universities-block').hasClass('d-none') === false) {
                if (instituteDataTable.data().any()) {

                    $('#educational-institute-accordion-error').addClass('d-none');
                    if (isValidAllInputs) {
                        // Get Data Table Values In educational Institute Array
                        $('#tbl-educational-institute > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (instituteDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {


                                educationalInstituteArray.push(
                                    {
                                        'InstituteId': columnValues[1],
                                        'Note': columnValues[3]
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#educational-institute-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For DocumentArray Data Table To Pass Data
            if (!$('#document-upload-block').hasClass('d-none')) {
                if (documentDataTable.data().any()) {
                    $('#document-type-error').addClass('d-none');
                    if (isValidAllInputs) {
                        // Get Data Table Values In Interest PayOut Frequency Array
                        $('#tbl-document > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');
                            columnValues = (documentDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                DocumentArray.push({

                                    'DocumentId': columnValues[1],
                                    'IsRequired': columnValues[3],
                                    'EnableDocumentUploadInDb': columnValues[4],
                                    'DocumentAllowedFileFormatsForDb': columnValues[6],
                                    'MaximumFileSizeForDocumentUploadInDb': columnValues[7],
                                    'EnableDocumentUploadInLocalStorage': columnValues[8],
                                    'DocumentLocalStoragePath': columnValues[9],
                                    'DocumentAllowedFileFormatsForLocalStorage': columnValues[11],
                                    'MaximumFileSizeForDocumentUploadInLocalStorage': columnValues[12],
                                    'Note': columnValues[13],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#document-type-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For Notice Schedule Data Table To Pass Data(Optional, Not Mandatory)
            if (noticeScheduleDataTable.data().any()) {
                if (isValidAllInputs) {
                    // Get Data Table Values In Notice Schedule Array
                    $('#tbl-notice-schedule > tbody > tr').each(function () {
                        currentRow = $(this).closest('tr');

                        columnValues = (noticeScheduleDataTable.row(currentRow).data());

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues != 'undefined' && columnValues != null) {
                            debugger;
                            noticeScheduleArray.push(
                                {
                                    'NoticeTypeId': columnValues[1],
                                    'CommunicationMediaId': columnValues[3],
                                    'ScheduleId': columnValues[5],
                                    'Note': columnValues[7]
                                });
                        }

                        else
                            return false;
                    });
                }
            }

            // Report Format Table(Optional, Not Mandatory)
            if (!$('#report-format-card').hasClass('d-none')) {
                if (reportFormatDataTable.data().any()) {
                    if (isValidAllInputs) {
                        $('#tbl-report-format-data-table > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (reportFormatDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                reportFormatArray.push(
                                    {
                                        'ReportTypeFormatId': columnValues[1],
                                        'Note': columnValues[3]
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
            }

            // Create Array For loanCharges Data Table To Pass Data
            if (!$('#loan-charges-card').hasClass('d-none')) {
                if (loanChargesDataTable.data().any()) {
                    $('#charges-data-table-error').addClass('d-none');
                    if (isValidAllInputs) {
                        // Get Data Table Values In Notice Schedule Array
                        $('#tbl-charges > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (loanChargesDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                loanChargesArray.push(
                                    {
                                        'ChargesTypeId': columnValues[1],
                                        'GeneralLedgerId': columnValues[3],
                                        'LendingChargesBaseId': columnValues[5],
                                        'ChargesPercentage': columnValues[7],
                                        'MinimumCharges': columnValues[8],
                                        'MaximumCharges': columnValues[9],
                                        'DefaultCharges': columnValues[10],
                                        'IsApplicableTax': columnValues[11],
                                        'IsOptional': columnValues[12],
                                        'Note': columnValues[13]
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#charges-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For Scheme Tenure List Data Table To Pass Data
            if ($('#enable-tenure-list').is(':checked')) {
                if (tenureListDataTable.data().any()) {
                    $('#tenure-list-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {
                        // Get Data Table Values In Loan Scheme Menu Array
                        $('#tbl-tenure-list > tbody > tr').each(function () {

                            currentRow = $(this).closest('tr');

                            columnValues = (tenureListDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                tenureListArray.push({
                                    'Tenure': columnValues[1],
                                    'TenureUnit': columnValues[2],
                                    'TenureText': columnValues[4],
                                    'Note': columnValues[5]
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#tenure-list-data-table-error').removeClass('d-none');
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

            // Create Array For General Ledger Data Table To Pass Data
            if (!$('#general-ledger-card').hasClass('d-none')) {
                if (generalLedgerDataTable.data().any()) {

                    $('#general-ledger-data-table-error').addClass('d-none');

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
                    $('#general-ledger-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For Business Office Data Table To Pass Data
            if (!$('#business-office-card').hasClass('d-none')) {
                if (businessOfficeDataTable.data().any()) {

                    $('#business-office-data-table-error').addClass('d-none');
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
                                        'Note': columnValues[6]
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

            // Create Array For Loan Overdues Action Table To Pass Data
            if (!$('#loan-overdues-action-card').hasClass('d-none')) {
                if (loanOverduesActionDataTable.data().any()) {

                    $('#overdues-action-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {
                        $('#tbl-loan-overdues-action > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (loanOverduesActionDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {

                                loanOverduesActionArray.push({
                                    'MinimumOverduesInstallment': columnValues[1],
                                    'MaximumOverduesInstallment': columnValues[2],
                                    'LoanRecoveryActionId': columnValues[3],
                                    'Note': columnValues[5]
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#overdues-action-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For Consumer Durable Loan Item To Pass Data
            if (!$('#consumer-durable-loan-card').hasClass('d-none')) {
                if (consumerDurableLoanDataTable.data().any()) {
                    debugger;

                    $('#consumer-durable-loan-accordion-title-error').addClass('d-none');

                    if (isValidAllInputs) {
                        $('#tbl-consumer-durable-loan > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (consumerDurableLoanDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {

                                consumerDurableLoanArray.push({
                                    'ConsumerDurableItemId': columnValues[1],
                                    'Margin': columnValues[3],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#consumer-durable-loan-accordion-title-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For DocumentArray Data Table To Pass Data
            if (!$('#vehicle-loan-card').hasClass('d-none')) {
                if (vehicleTypeLoanDataTable.data().any()) {
                    debugger
                    $('#vehicle-type-loan-accordion-title-error').addClass('d-none');
                    if (isValidAllInputs) {
                        debugger;
                        // Get Data Table Values In Interest PayOut Frequency Array
                        $('#tbl-vehicle-type-loan > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');
                            columnValues = (vehicleTypeLoanDataTable.row(currentRow).data());
                            debugger;
                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                debugger;
                                vehicleTypeLoanArray.push({

                                    'VehicleTypeId': columnValues[1],
                                    'DownPaymentPercentage': columnValues[3],
                                    'MaximumLoanAmountWithoutExtraSecurity': columnValues[4],
                                    'PhotoUpload': columnValues[5],
                                    'EnablePhotoUploadInDb': columnValues[7],
                                    'AllowedFileFormatsForDb': columnValues[8],
                                    'MaximumFileSizeForDb': columnValues[10],
                                    'EnablePhotoUploadInLocalStorage': columnValues[11],
                                    'StoragePath': columnValues[12],
                                    'AllowedFileFormatsForLocalStorage': columnValues[13],
                                    'MaximumFileSizeForLocalStorage': columnValues[15],
                                    'MinimumNumberOfPhoto': columnValues[16],
                                    'MaximumNumberOfPhoto': columnValues[17],
                                    'Note': columnValues[18],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#vehicle-type-loan-accordion-title-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For DocumentArray Data Table To Pass Data
            if (!$('#pre-owned-vehicle-loan-card').hasClass('d-none')) {
                if (preOwnedVehicleDataTable.data().any()) {
                    $('#pre-owned-vehicle-loan-accordion-title-error').addClass('d-none');
                    if (isValidAllInputs) {
                        // Get Data Table Values In Interest PayOut Frequency Array
                        $('#tbl-pre-owned-vehicle-loan > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');
                            columnValues = (preOwnedVehicleDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                preownedVehicleArray.push({

                                    'VehicleTypeId': columnValues[1],
                                    'DownPaymentPercentage': columnValues[3],
                                    'EnableVehicleInspection': columnValues[4],
                                    'VehicleLife1': columnValues[5],
                                    'MaximumLoanSanctionPercentage1': columnValues[6],
                                    'MaximumTenure1': columnValues[7],
                                    'VehicleLife2': columnValues[8],
                                    'MaximumLoanSanctionPercentage2': columnValues[9],
                                    'MaximumTenure2': columnValues[10],
                                    'VehicleLife3': columnValues[11],
                                    'MaximumLoanSanctionPercentage3': columnValues[12],
                                    'MaximumTenure3': columnValues[13],
                                    'VehicleLife4': columnValues[14],
                                    'MaximumLoanSanctionPercentage4': columnValues[15],
                                    'MaximumTenure4': columnValues[16],
                                    'PhotoUpload': columnValues[17],
                                    'EnablePhotoUploadInDb': columnValues[19],
                                    'AllowedFileFormatsForDb': columnValues[20],
                                    'MaximumFileSizeForDb': columnValues[22],
                                    'EnablePhotoUploadInLocalStorage': columnValues[23],
                                    'StoragePath': columnValues[24],
                                    'AllowedFileFormatsForLocalStorage': columnValues[25],
                                    'MaximumFileSizeForLocalStorage': columnValues[27],
                                    'MinimumNumberOfPhoto': columnValues[28],
                                    'MaximumNumberOfPhoto': columnValues[29],
                                    'Note': columnValues[30],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#pre-owned-vehicle-loan-accordion-title-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Call Cantroller Save Data Table Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: urlForDataTable,
                    type: 'POST',
                    dataType: 'json',
                    data: ({
                        '_tenureList': tenureListArray, '_document': DocumentArray, '_noticeSchedule': noticeScheduleArray,
                        '_reportFormat': reportFormatArray, '_schemeGeneralLedger': generalLedgerArray, '_businessOffice': businessOfficeArray, '_targetGroup': targetGroupArray,
                        '_loanCharges': loanChargesArray, '_loanOverduesAction': loanOverduesActionArray, '_vehicleTypeLoan': vehicleTypeLoanArray, '_preownedVehicleLoan': preownedVehicleArray, '_consumerDurableLoan': consumerDurableLoanArray, '_educationalCourse': educationalCourseArray, '_institute': educationalInstituteArray,
                    }),
                    ContentType: 'application/json; charset=utf-8',

                    success: function (data) {

                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured While Save Data In Contact Details DataTable!!! Error Message - ' + error.toString());
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
